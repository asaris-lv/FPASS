using System;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Text;
using System.Diagnostics;

namespace de.pta.Component.DataAccess.Internal
{
	/// <summary>
	/// Processing for database type SQL. Preparing executable query statement, defining Connection object
	/// and execute query with database specific logic.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> M.Keller, PTA GmbH
	/// <b>Date:</b> Apr/02/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class DataBaseTypeSQL : DataBaseType
	{
	#region Members


	#endregion //End of Members

	#region  Constructors

		/// <summary>
		/// Constructor inherits from base (DataBaseType).
		/// </summary>
		public DataBaseTypeSQL(Query query) : base(query)
		{
			initialize();
		}

	#endregion //End of Constructors

	#region Initialization

		private void initialize()
		{
			// Initializes the members.
		}

	#endregion //End of Initialization

	#region Accessors 
	#endregion //End of Accessors

	#region Methods

		/// <summary>
		/// Preparing of the query statement (joins query definition with concrete current paramater list.
		/// </summary>
		override public void PrepareExecutableStatement()
		{
			String sqlStatement				= this.query.QueryDefinition.SelectStatement;
			ArrayList qryDefParameter		= this.query.QueryDefinition.Parameters;
			ArrayList qryDefParameterLists	= this.query.QueryDefinition.ParameterLists;
			ArrayList qryRequests			= this.query.ParameterList;
			ArrayList qryDefAttributeLists  = this.query.QueryDefinition.AttributeLists;
			ArrayList orderByList			= this.query.OrderByList;
			StringBuilder attList			= null;
			StringBuilder paraList			= null;

			// Get the attributes and replace them.
			foreach ( QryAttributeList qryAttributeList in qryDefAttributeLists )
			{
				attList = new StringBuilder();

				attList.Append(query.QueryDefinition.GetAttributeList(qryAttributeList.Id));
				sqlStatement = sqlStatement.Replace(qryAttributeList.Id, attList.ToString());
			}

			// Loop all parameter lists from the configuration.
			foreach ( QryParameterList qryParameterList in qryDefParameterLists )
			{
				paraList = new StringBuilder();

				bool bInsert		= false; // indicates whether speration is needed.
				bool bCode			= true;  // indicates if Code (e.g. "where ") is to insert.
				bool bParenthesis	= false; // indicates if a parenthesis is opened.

				if ( qryParameterList.Type.ToUpper().Equals(QueryDefinition.BY_CLAUSE) )
				{
					// Process all entries in the order list.
					foreach ( String str in orderByList )
					{
						// Add Code only once.
						if ( bCode )
						{
							paraList.Append(qryParameterList.Code);
							paraList.Append(" ");
							bCode = false;
						}

						// Insert a comma alway, but not for the first time.
						if ( bInsert )
						{
							paraList.Append(", ");
						}
						else
						{
							bInsert = true;
						}

						// Add the column name for the given id.
						paraList.Append(this.query.QueryDefinition.GetColumnNameForId(str));
					}

				}
				else if ( qryParameterList.Type.ToUpper().Equals(QueryDefinition.WHERE_CLAUSE) )
				{
					// Loop all parameters from the configuration
					foreach ( QryParameter qryParameter in qryDefParameter )
					{
						// The value of ParaList must correspond with the id of the parameter list.
						if ( qryParameter.ParaList.Equals(qryParameterList.Id) )
						{
							// Loop the given requests.
							foreach ( RequestParameter request in qryRequests )
							{
								// The id of the request must correspond with the id of the
								// parameter.
								if ( request.Id.Equals(qryParameter.Id) )
								{
									// If code is to insert
									if ( bCode )
									{
										paraList.Append(qryParameterList.Code);

										// If no parenthesis is already opened
										// open it.
										if ( !bParenthesis )
										{
											paraList.Append(" (");
											bParenthesis = true;
										}

										bCode = false; // code only once.
									}

									// If separation is needed, insert it.
									// But not for the first time.
									if ( bInsert )
									{
										if ( qryParameterList.Type.Equals(QueryDefinition.WHERE_CLAUSE) )
										{
											paraList.Append(" AND ");
										}
										if ( qryParameterList.Type.Equals(QueryDefinition.BY_CLAUSE) )
										{
											paraList.Append(", ");
										}
									}
									else
									{
										bInsert = true;
									}

									// Value.
									bool apostroph = qryParameter.DataType.ToUpper().Equals("TXT");
									paraList.Append(query.QueryDefinition.FormatParameter(
																			request.Id, 
																			request.GetParameters(apostroph), 
																			request.Operation));
								}
							} // Request parameters
						}
					} // Query parameters
				

					// If a parenthesis is opened, close one.
					if ( bParenthesis )
					{
						paraList.Append(" )");
					}
				}

				// Replace string in the sql statement.
				sqlStatement = sqlStatement.Replace(qryParameterList.Id, paraList.ToString());

			} // Parameter lists

			//Debug.WriteLine(sqlStatement);
			preparedStatement = sqlStatement;
		}

		/// <summary>
		/// Defining a connection to the specific database.
		/// </summary>
		override public void PrepareCurrentDBConnection()
		{
			DBConnectionInfo dbConnectionInfo =  DBConnectionInfoFactory.GetInstance().GetDBConnectionInfo(DBConnectionInfo.TYPE_SQL);
		
			dbConnection = new SqlConnection();
			dbConnection.ConnectionString = dbConnectionInfo.ConnectString;
		}

		/// <summary>
		/// Process the query statement against the database.
		/// </summary>
		/// <returns>
		/// A DataSet object that contains the first n rows of a query result
		/// </returns>
		override public DataSet ProcessExecutableStatement()
		{
			int  applicationMaxRecords	= DataAccessConfiguration.GetInstance().ApplicationMaxRecords;
			int  dataBlockSize			= DataAccessConfiguration.GetInstance().DataBlockSize;
			bool turboModeFlag			= DataAccessConfiguration.GetInstance().TurboModeFlag;
			int  turboModeMaxRecords	= DataAccessConfiguration.GetInstance().TurboModeMaxRecords;
			int  dataPageSize			= DataAccessConfiguration.GetInstance().DataPageSize;

			bool maxRecordsExceeded		= false;
			int currentPage				= 0;
			int currentRecord			= 0;
			DataRow rowData				= null;
			DataTable tableData			= null;
			DataTable tableStatusInfo	= null;
			DataRow rowStatusInfo		= null;
			DataSet ds					= this.query.QueryDefinition.TypedDataSetTemplate.Copy();

			// If there is a value greater than 0 for differentDataPageSizeUsed, it will be used.
			if ( query.QueryDefinition.DifferentDataPageSizeUsed > 0 ) 
			{
				dataPageSize = query.QueryDefinition.DifferentDataPageSizeUsed;
			}

			// If the database isn't already open, open it.
			if ( dbConnection.State != ConnectionState.Open )
			{
				try
				{
					dbConnection.Open();
				}
				catch ( Exception e )
				{
					throw new DataAccessException("ERROR_DATAACCESS_CANNOT_OPEN_DATABASE", e);
				}
			}

			// if query was not already preceseed, do it.
			if ( !queryProcessed )
			{
				processQuery();
			}

			// get tables and start with page one.
			tableData		= ds.Tables[QueryDefinition.TB_NAME_DATATABLE];
			tableStatusInfo = ds.Tables[QueryDefinition.TB_NAME_STATUSTABLE];
			currentPage		= 1;

			try
			{
				// Read the data
				while ( returnDataReader.Read() )
				{
					// one more record.
					++totalRecordsRead;
					++currentRecord;

					rowData = tableData.NewRow();
					rowData[QueryDefinition.PAGECOUNTER] = currentPage;

					// loop the fields of the reader and fill the columns.
					for ( int i=0; i<returnDataReader.FieldCount; ++i )
					{
						rowData[i+1] = returnDataReader.GetValue(i);
					}

					// Add row to table.
					tableData.Rows.Add(rowData);

					// test, if the maximum of records is exceeded.
					if ( totalRecordsRead >= applicationMaxRecords )
					{
						maxRecordsExceeded  = true;
						processingCompleted = true;
						break;
					}

					// test if the block is full.
					if ( currentRecord >= dataBlockSize )
						break;

					// verify if a new page is needed.
					if ( (currentRecord % dataPageSize) == 0 )
					{
						currentPage++;
					}

				} // while
			}
			catch ( Exception e )
			{
				throw new DataAccessException("ERROR_DATAACCESS_EXECUTING_SQL", e);
			}

			// if we left the while loop, although the block
			// isn't full, the processing is completed.
			if ( currentRecord < dataBlockSize )
			{
				processingCompleted = true;
			}

			// write status information, if at least one record exists.
			if (currentRecord > 0)
			{
				rowStatusInfo = tableStatusInfo.NewRow();
				rowStatusInfo[QueryDefinition.MAXPAGEINDATASET]		= currentPage.ToString();
				rowStatusInfo[QueryDefinition.MAXRECORDSINDATASET]	= currentRecord.ToString();
				rowStatusInfo[QueryDefinition.PROCESSINGCOMPLETED]	= processingCompleted.ToString();
				rowStatusInfo[QueryDefinition.MAXRECORDSEXCEEDED]   = maxRecordsExceeded.ToString();
				rowStatusInfo[QueryDefinition.TOTALRECORDSREAD]		= totalRecordsRead.ToString();
				rowStatusInfo[QueryDefinition.APPLICATIONMAXRECORDS]= applicationMaxRecords.ToString();
				
				tableStatusInfo.Rows.Add(rowStatusInfo);
			}

			// Close database connection if processing is completed or turboModeFlag
			// is set (read data only once).
			if ( processingCompleted || turboModeFlag )
			{
				//all completed - update number of records in query result
				QryResult qryResult = QryResultFactory.GetInstance().GetQryResult(this.query.QryResultID);
				qryResult.ReturnNumberOfRecords = totalRecordsRead;

				dbConnection.Close();
			}

			// return filled DataSet.
			return ds;
		}

		private void processQuery()
		{
			// Processes the SQL query.

			if ( dbConnection.State != ConnectionState.Open )
			{
				// throw an exception
				throw new DataAccessException("ERROR_DATAACCESS_NO_DB_CONNECTION");
			}

			// Prepare query command
			SqlCommand currentCommand  = new SqlCommand();
			currentCommand.CommandType = CommandType.Text;
			currentCommand.Connection  = (SqlConnection)dbConnection;

			// Get the executable query statement
			currentCommand.CommandText = preparedStatement;

			try
			{
				// Execute reader
				returnDataReader = currentCommand.ExecuteReader();
			}
			catch ( Exception e )
			{
				throw new DataAccessException("ERROR_DATAACCESS_EXECUTING_SQL", e);
			}

			queryProcessed = true;
		}

	#endregion
	}
}
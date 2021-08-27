using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Text;
using de.pta.Component.Common;

namespace de.pta.Component.DataAccess.Internal
{
	/// <summary>
	/// Processing for database type SQL. Preparing executable query statement, defining Connection object
	/// and execute query with database specific logic.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> A.Opierzynski, PTA GmbH
	/// <b>Date:</b> Apr/02/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class DataBaseTypeMDX : DataBaseType
	{
		#region Members

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Constructor inherits from base (DataBaseType).
		/// </summary>
		public DataBaseTypeMDX(Query query) : base(query)
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
			String mdxStatement				= this.query.QueryDefinition.SelectStatement;
			ArrayList qryDefParameter		= this.query.QueryDefinition.Parameters;
			ArrayList qryDefParameterLists	= this.query.QueryDefinition.ParameterLists;
			ArrayList qryRequests			= this.query.ParameterList;
			ArrayList qryDefAttributeLists  = this.query.QueryDefinition.AttributeLists;
			StringBuilder attList			= null;
			StringBuilder paraList			= null;

			// Get the attributes and replace them.
			foreach ( QryAttributeList qryAttributeList in qryDefAttributeLists )
			{
				attList = new StringBuilder();

				attList.Append(query.QueryDefinition.GetAttributeList(qryAttributeList.Id));
				mdxStatement = mdxStatement.Replace(qryAttributeList.Id, attList.ToString());
			}

			// Loop all parameter lists from the configuration.
			foreach ( QryParameterList qryParameterList in qryDefParameterLists )
			{
				paraList = new StringBuilder();

				bool bCode			= true;  // indicates if Code (e.g. "where ") is to insert.

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
									bCode = false; // code only once.
								}

								// Value.
								paraList.Append(request.GetParameters(false));
							}
						} // Request parameters
					}
				}

				// Replace string in the sql statement.
				mdxStatement = mdxStatement.Replace(qryParameterList.Id, paraList.ToString());

			} // Parameter lists

			Debug.WriteLine(mdxStatement);
			preparedStatement = mdxStatement;
		}

		/// <summary>
		/// Defining a connection to the specific database.
		/// </summary>
		override public void PrepareCurrentDBConnection()
		{
			// The connection is handled in the mdx access thread.
		}

		/// <summary>
		/// Process the query statement against the database.
		/// </summary>
		/// <param name="currentDBConnection">a generic connection, that implements the IDbConnection Interface</param>
		/// <param name="executableStatement">an executable query statement for a specific database</param>
		/// <returns>a DataSet object that contains the first n rows of a query result</returns>
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


					int count = returnDataReader.FieldCount - query.QueryDefinition.GetNumberOfAttributes();
					int col = 0;
					// loop the fields of the reader and fill the columns.
					for ( int i=count; i<returnDataReader.FieldCount; ++i )
					{
						rowData[++col] = returnDataReader.GetValue(i);
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

			// Close the reader, so the connection is freed, too.
			returnDataReader.Close();

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
			}

			// return filled DataSet.
			return ds;
		}

		private void processQuery()
		{
			// Processes the MDX query by the mdx access thread.

			SynchronizedQueue queue = null;
			MdxExchange mdxExchange = null;

			// Get the suitable queue object.
			queue = QueueFactory.GetInstance().GetQueue("MdxQueue");

			// Create MDX exchange object and set statement.
			mdxExchange = new MdxExchange();
			mdxExchange.MdxStatement = preparedStatement;

			// Send object for processing.
			queue.Send(mdxExchange);

			// Get the exchange object.
			mdxExchange = (MdxExchange)queue.GetOutput();

			// Check if an error occured.
			if ( mdxExchange.ErrorOccured )
			{
				// Extract exception
				throw new DataAccessException("ERROR_DATAACCESS_MDX_EXECUTION", mdxExchange.ExecuteException);
			}

			// Get the reader reader.
			returnDataReader = mdxExchange.DataReader;

			queryProcessed = true;
		}

		#endregion
	}
}
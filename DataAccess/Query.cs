using System;
using System.Data;
using System.Collections;

namespace de.pta.Component.DataAccess.Internal
{
	/// <summary>
	/// Class Query matches query definitions with concrete query parameters. Depending from
	/// database type different modes of query execution will processed.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> A.Opierzynski, PTA GmbH
	/// <b>Date:</b> Apr/02/2003
	///	<b>Remarks:</b> None
	///	<b>Changes:</b>
	///	<b>Date:</b> Aug/20/2003
	///	<b>Author:</b> A. Seibt, PTA GmbH
	///	<b>Remarks:</b> Access to Oracle Database added.
	/// </pre>
	/// </remarks>
	internal class Query
	{
		#region Members

		private String qryResultID;
		private QueryDefinition queryDefinition;
		private ArrayList parameterList;
		private ArrayList orderByList;
		private DataBaseType dataBaseType;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Constructor with query definiton, query result and concrete query parameters
		/// </summary>
		/// <param name="qryResultID">ID of a query result from Class QryResult</param>
		/// <param name="queryDefinition">the current defintion of a query</param>
		public Query(String qryResultID, QueryDefinition queryDefinition)
		{
			initialize();

			this.qryResultID		= qryResultID;
			this.queryDefinition	= queryDefinition;
		}

		#endregion //End of Constructors

		#region Initialization

		private void initialize()
		{
			// Initializes the members.
			qryResultID		= String.Empty;
			queryDefinition	= null;
			parameterList	= new ArrayList();
			orderByList		= new ArrayList();
			dataBaseType	= null;
		}

		#endregion //End of Initialization

		#region Accessors

		/// <summary>
		/// Accessor for qryResultID.
		/// </summary>
		public String QryResultID
		{
			get
			{
				return qryResultID;
			}
			set
			{
				qryResultID = value;
			}
		}
		/// <summary>
		/// Accessor for parameterList.
		/// </summary>
		public ArrayList ParameterList
		{
			get
			{
				return parameterList;
			}
			set
			{
				parameterList = value;
			}
		}

		/// <summary>
		/// Accessor for orderByList.
		/// </summary>
		public ArrayList OrderByList
		{
			get
			{
				return orderByList;
			}
			set
			{
				orderByList = value;
			}
		}

		/// <summary>
		/// Accessor for queryDefinition.
		/// </summary>
		public QueryDefinition QueryDefinition
		{
			get
			{
				return queryDefinition;
			}
			set
			{
				queryDefinition = value;
			}
		}
		/// <summary>
		/// Accessor for dataBaseType.
		/// </summary>
		public DataBaseType DataBaseType
		{
			get
			{
				return dataBaseType;
			}
			set
			{
				dataBaseType = value;
			}
		}

		#endregion //End of Accessors

		#region Methods

		/// <summary>
		/// Adds an parameter to the internal collection of parameters.
		/// </summary>
		/// <param name="id">Id</param>
		/// <param name="paraValue">Value</param>
		/// <param name="operation">Operation</param>
		public void AddParameter(String id, String paraValue, String operation)
		{
			bool foundExisting = false;

			// loop the parameterList and add the paraValue, if the id
			// alrerady exists.
			foreach ( RequestParameter parameter in parameterList )
			{
				if ( parameter.Id.Equals(id) )
				{
					foundExisting = true;

					// Add value to given id, only when the operation is "IN"
					if ( operation.ToUpper().Equals(QueryDefinition.TYPE_IN) )
					{
						parameter.AddParaValue(paraValue);
					}
				}
			}

			// if no existing entry is found add a new Entry.
			if ( !foundExisting )
			{
				RequestParameter parameter = new RequestParameter();

				parameter.Id		= id;
				parameter.AddParaValue(paraValue);
				parameter.Operation	= operation;

				parameterList.Add(parameter);
			}
		}

		/// <summary>
		/// Adds an order-by criterion to the internal collection.
		/// </summary>
		/// <param name="orderId">The id of the order by.</param>
		public void AddOrderBy(String orderId)
		{
			if ( !orderByList.Contains(orderId) )
			{
				orderByList.Add(orderId);
			}
		}

		private void setDatabaseType()
		{
			// Strategy Pattern - switch depending on defined database type
			// for each type of db have to exist a separate class inherits from DataBaseType

			switch (this.queryDefinition.DatabaseTypeInfo)
			{	
				case DBConnectionInfo.TYPE_ORACLE: // Oracle
					this.dataBaseType =  new DataBaseTypeOracle(this);
					break;

				case DBConnectionInfo.TYPE_SQL: // SQL
					this.dataBaseType =  new DataBaseTypeSQL(this);
					break;

				case DBConnectionInfo.TYPE_MDX: // MDX
					this.dataBaseType =  new DataBaseTypeMDX(this);
					break;	
					
				default:
					// Error - database type undefined
					throw new DataAccessException("ERROR_DATAACCESS_DB_TYPE_UNDEFINED");

			} // switch
		}

		/// <summary>
		/// This method is setting the database type, prepares an executable query statement and execute this statement against the database.
		/// </summary>
		/// <returns>a DataSet object that stores the first ...n records depending an application settings</returns>
		public DataSet ExecuteQuery()
		{
			DataSet returnDataSet = null;

			//setting the database type (defined in query definition)
			setDatabaseType();

			//preparing of the executable query statement
			dataBaseType.PrepareExecutableStatement();

			//returns a database connection that implements the IDbConnection Interface (but not opens the connection)
			dataBaseType.PrepareCurrentDBConnection();
			
			//executes the query statement and returns a DataSet with first ...n records
			returnDataSet = dataBaseType.ProcessExecutableStatement();
			
			return returnDataSet;
		}

		#endregion // Methods
	}
}

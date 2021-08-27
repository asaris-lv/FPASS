using System;
using System.Data;
using System.Collections;
using System.Threading;

namespace de.pta.Component.DataAccess.Internal
{
	/// <summary>
	/// Process the query. Intatiation of all necessary objects (QryResult, QueryDefinition and Query).
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> A.Opierzynski, PTA GmbH
	/// <b>Date:</b> Apr/02/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class QueryProcessor
	{
		#region  Members

		/// <summary>
		/// instance (Singleton)
		/// </summary>
		private static QueryProcessor instance;

		#endregion //End of Members

		#region  Constructors

		/// <summary>
		/// Private constructor assures that no further instance is created.
		/// </summary>
		protected QueryProcessor()
		{
			initialize();
		}

		#endregion //End of Constructors

		#region Initialization

		private void initialize()
		{
			// Initializes the members.
		}

		/// <summary>
		/// Singleton - returns exactly one instance of that class.
		/// </summary>
		/// <returns>Instance of a QueryProcessor</returns>
		public static QueryProcessor GetInstance()
		{
			if( instance == null )
			{
				instance = new QueryProcessor();
			}

			return instance;
		}

		#endregion //End of Initialization

		#region Accessors
		#endregion //End of Accessors

		#region Methods

		/// <summary>
		/// 
		/// </summary>
		/// <param name="userID">User ID of user request</param>
		/// <param name="sessionID">Session ID of user request</param>
		/// <param name="queryDefName">name for query to execute</param>
		/// <returns>
		/// The unique qryResultID to identify current result buffer.
		/// </returns>
		public String PrepareQueryResult(String userID, String sessionID, String queryDefName)
		{
			QryResult qryResult				= null;
			Query query						= null;
			QueryDefinition queryDefinition = null;

			// Creating a result buffer (session information for cleanup).
			String qryResultID = QryResultFactory.GetInstance().CreateQueryResult(sessionID);

			// Getting the result buffer object and set information
			qryResult					= QryResultFactory.GetInstance().GetQryResult(qryResultID);
			qryResult.UserID			= userID;
			qryResult.QueryDefName		= queryDefName;
			qryResult.QryResultStatus	= DataAccessManager.QryStatus.INIT;

			// Invoke path creation
			qryResult.SetPathNameForXMLTempData();
			qryResult.SetFileNameForXMLTempData();

			// Getting the query definition (user parameter independent)
			queryDefinition = DataAccessConfiguration.GetInstance().GetQueryDefForName(queryDefName);

			//getting a new query object
			query = new Query(qryResultID, queryDefinition);

			// Set the query.
			qryResult.LocalQuery = query;

			// Retrun result id.
			return qryResultID;
		}

		/// <summary>
		/// Sets an parameter for a query.
		/// </summary>
		/// <param name="qryResultID">Id of the query result.</param>
		/// <param name="paraId">Id of the parameter.</param>
		/// <param name="paraValue">Value of the parameter.</param>
		/// <param name="operation">Operation.</param>
		public void SetParameter(String qryResultID, String paraId, String paraValue, String operation)
		{
			QryResult qryResult = QryResultFactory.GetInstance().GetQryResult(qryResultID);

			if ( null != qryResult )
			{
				qryResult.LocalQuery.AddParameter(paraId, paraValue, operation);
			}
		}

		/// <summary>
		/// Sets an order-by criterion.
		/// </summary>
		/// <param name="qryResultID">Id of the query.</param>
		/// <param name="orderID">Id of order.</param>
		public void SetOrder(String qryResultID, String orderID)
		{
			QryResult qryResult = QryResultFactory.GetInstance().GetQryResult(qryResultID);

			if ( null != qryResult )
			{
				qryResult.LocalQuery.AddOrderBy(orderID);
			}
		}

		/// <summary>
		/// Executing a query with parameters against the database.
		/// </summary>
		/// <param name="userID">User ID of user request</param>
		/// <param name="sessionID">Session ID of user request</param>
		/// <param name="queryDefName">name for query to execute</param>
		/// <param name="parameterList">list of parameters from user request</param>
		/// <returns>
		/// The unique qryResultID to identify current result buffer from DataAccessManager.
		/// </returns>
		public void ExecuteQuery(String qryResultID)
		{
			QryResult qryResult = QryResultFactory.GetInstance().GetQryResult(qryResultID);

			if ( null != qryResult )
			{
				//execute the query (preparing statement, merging definition and parameters, executing)
				qryResult.QryResultStatus = DataAccessManager.QryStatus.SQLEXECUTING;
				DataSet returnDataSet = qryResult.LocalQuery.ExecuteQuery();
				
				if ( qryResult.LocalQuery.DataBaseType.TotalRecordsRead <= 0 )
				{
					qryResult.QryResultStatus = DataAccessManager.QryStatus.FINISHED_NODATA;
				}

				//store generated DataSet in result buffer
				qryResult.ReturnDataSet   = returnDataSet;

				// Only start a new thread if not turboMode ist set.
				if ( !DataAccessConfiguration.GetInstance().TurboModeFlag )
				{
					if ( qryResult.QryResultStatus != DataAccessManager.QryStatus.FINISHED_NODATA )
					{
						qryResult.QryResultStatus = DataAccessManager.QryStatus.XMLEXECUTING;
						XmlDataProcessor qryProc = new XmlDataProcessor(qryResult);
						Thread thread = new Thread(new ThreadStart(qryProc.Start));
						thread.Name = String.Format("XmlDataProc_{0}", qryResult.LocalQuery.QryResultID);
						thread.Start();
						qryResult.XmlProcessingThread = thread;
					}
				}
			}
		}


		#endregion
	}
}
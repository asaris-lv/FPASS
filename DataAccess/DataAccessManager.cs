using System;
using System.Data;
using System.Collections;
using System.IO;
using System.Diagnostics;
using System.Threading;

using de.pta.Component.DataAccess.Internal;

namespace de.pta.Component.DataAccess

{
	/// <summary>
	/// Boundary for data access. 
	/// Functionality: query execution, navigation between data blocks, query process status.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> A.Opierzynski, PTA GmbH
	/// <b>Date:</b> Apr/02/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	public class DataAccessManager
	{
	#region Members

		/// <summary>
		/// Status of query execution.
		/// </summary>
		public enum QryStatus : int 
		{ 
			INIT = 1, 
			SQLEXECUTING, 
			XMLEXECUTING, 
			FINISHED, 
			FINISHED_NODATA 
		};

		private static DataAccessManager instance;
		private bool configurationRead;
		private Thread mdxThread;


	#endregion //End of Members

	#region Constructors

		protected DataAccessManager()
		{
			// Constructs the object.

			initialize();
		}

	#endregion //End of Constructors

	#region Initialization

		private void initialize()
		{
			// Initialization of members. 
			configurationRead	= false;
			mdxThread			= null;
		}

		/// <summary>
		/// Singleton - returns exactly one instance of that class
		/// </summary>
		/// <returns>Instance of a DataAccessManager</returns>
		public static DataAccessManager GetInstance()
		{
			if( null == instance )
			{
				instance = new DataAccessManager();
			}

			return instance;
		}

		#endregion //End of Initialization

	#region Accessors 
	#endregion //End of Accessors

	#region Methods

		///<summary>
		/// Reads the configuration.
		/// </summary>
		public void ReadConfiguration() 
		{
			ReadConfiguration(true);
		}

		///<summary>
		/// Reads the configuration.
		/// </summary>
		/// <param name="pIsWebApplication">true, if application is a web application,
		/// false, if application is a windows application</param>
		public void ReadConfiguration(bool pIsWebApplication)
		{

			// read the configuration.
			DataAccessConfiguration.GetInstance().ReadConfiguration(pIsWebApplication);
			configurationRead = true;

			// Only start a new thread if not turboMode ist set.
			if ( !DataAccessConfiguration.GetInstance().TurboModeFlag )
			{
				// After the configuration was read, start the clean up.
				SessionStateManager.GetInstance().StartCleanUpThread();
			}
		}

		

		/// <summary>
		/// Prepares the query result.
		/// </summary>
		/// <param name="queryName">Name of the query.</param>
		/// <returns>An uniques result id.</returns>
		public String PrepareQueryResult(String queryName)
		{
			return PrepareQueryResult("Unknown", "4711", queryName);
		}


		/// <summary>
		/// Prepares the query result.
		/// </summary>
		/// <param name="userID">User id.</param>
		/// <param name="queryName">Name of the query.</param>
		/// <returns>An uniques result id.</returns>
		public String PrepareQueryResult(String userID, String queryName)
		{
			return PrepareQueryResult(userID, "4711", queryName);
		}


		/// <summary>
		/// Prepares the query result.
		/// </summary>
		/// <param name="userID">User id.</param>
		/// <param name="sessionID">Session Id</param>
		/// <param name="queryName">Name of the query.</param>
		/// <returns>An uniques result id.</returns>
		public String PrepareQueryResult(String userID, String sessionID, String queryName)
		{
			String qryResultId = String.Empty;

			if ( !configurationRead )
			{
				throw new DataAccessException("ERROR_DATAACCESS_NOTCONFIGERED");
			}

			// Get the user without domain, e.g. "MYDOM\USER" returns "USER".
			String user = getUserNameWithoutDomain(userID);

			// Prepare query.
			qryResultId = QueryProcessor.GetInstance().PrepareQueryResult(user, sessionID, queryName);

			// Get result object
			QryResult qryResult = QryResultFactory.GetInstance().GetQryResult(qryResultId);

			// Add session to session state manager.
			SessionStateManager.GetInstance().AddSession(sessionID, user, qryResult.XmlSessionPath);

			return qryResultId;
		}

		private String getUserNameWithoutDomain(String userID)
		{
			String user = "";
			int index = userID.IndexOf("\\"); // Index of the backslash between domain an user.

			// If a backslash is found extract only the user name,
			// otherwise take the provided string.
			if ( index >= 0 )
			{
				user = userID.Substring(index+1);
			}
			else
			{
				user = userID;
			}

			return user;
		}

		/// <summary>
		/// Sets a query parameter.
		/// </summary>
		/// <param name="qryResultId">Result id.</param>
		/// <param name="paraId">Id of the parameter.</param>
		/// <param name="paraValue">Value of the parameter.</param>
		/// <remarks>
		/// The operation is set to an empty string as default.
		/// </remarks>
		public void SetParameter(String qryResultId, String paraId, String paraValue)
		{
			SetParameter(qryResultId, paraId, paraValue, String.Empty);
		}

		/// <summary>
		/// Sets a query parameter.
		/// </summary>
		/// <param name="qryResultId">Result id.</param>
		/// <param name="paraId">Id of the parameter.</param>
		/// <param name="paraValue">Value of the parameter.</param>
		/// <param name="operation">Operation.</param>
		public void SetParameter(String qryResultId, String paraId, String paraValue, String operation)
		{
			if ( !configurationRead )
			{
				throw new DataAccessException("ERROR_DATAACCESS_NOTCONFIGERED");
			}

			QueryProcessor.GetInstance().SetParameter(qryResultId, paraId, paraValue, operation);
		}

		/// <summary>
		/// Sets an order-by criterion.
		/// </summary>
		/// <param name="qryResultId">Result id.</param>
		/// <param name="orderId">Order id.</param>
		public void SetOrder(String qryResultId, String orderId)
		{
			if ( !configurationRead )
			{
				throw new DataAccessException("ERROR_DATAACCESS_NOTCONFIGERED");
			}

			QueryProcessor.GetInstance().SetOrder(qryResultId, orderId);
		}

		/// <summary>
		/// Executes the query.
		/// </summary>
		/// <param name="qryResultId">Result id.</param>
		public void ExecuteQuery(String qryResultID)
		{
			if ( !configurationRead )
			{
				throw new DataAccessException("ERROR_DATAACCESS_NOTCONFIGERED");
			}

			QueryProcessor.GetInstance().ExecuteQuery(qryResultID);
		}

		/// <summary>
		/// With this functionality business logic can get the first data block of 
		/// a query result set (from all records a query returns).
		/// </summary>
		/// <param name="qryResultID">Unique query result ID</param>
		/// <returns>
		/// Returns the DataSet object that contains the first data block.
		/// </returns>
		public DataSet GetDataSetFirstBlock (String qryResultID)
		{
			QryResult qryResult = QryResultFactory.GetInstance().GetQryResult(qryResultID);

			if ( null == qryResult )
			{
				throw new DataAccessException("ERROR_DATAACCESS_QUERYRESULT_NOT_FOUND");
			}

			return qryResult.GetDataSetFirstBlock();
		}

		/// <summary>
		/// With this functionality business logic can get the next data block,
		/// forward from current block (the current block is handled in QryResult class).
		/// </summary>
		/// <param name="qryResultID">Unique query result ID</param>
		/// <returns>
		/// Returns the DataSet object that contains the next data block.
		/// </returns>
		public DataSet GetDataSetNextBlock (String qryResultID)
		{
			QryResult qryResult = QryResultFactory.GetInstance().GetQryResult(qryResultID);

			if ( null == qryResult )
			{
				throw new DataAccessException("ERROR_DATAACCESS_QUERYRESULT_NOT_FOUND");
			}

			return qryResult.GetDataSetNextBlock();
		}

		/// <summary>
		/// With this functionality business logic can get the previous data block 
		/// backwards from current block (the current block is handled in QryResult class).
		/// </summary>
		/// <param name="qryResultID">Unique query result ID</param>
		/// <returns>
		/// Returns the DataSet object that contains the previous data block.
		/// </returns>
		public DataSet GetDataSetPreviousBlock (String qryResultID)
		{
			QryResult qryResult = QryResultFactory.GetInstance().GetQryResult(qryResultID);

			if ( null == qryResult )
			{
				throw new DataAccessException("ERROR_DATAACCESS_QUERYRESULT_NOT_FOUND");
			}

			return qryResult.GetDataSetPreviousBlock();
		}

		/// <summary>
		/// With this functionality business logic can get the last data block of 
		/// a query result set (positioning to block stored in variable maxAvailableBlocks).
		/// </summary>
		/// <param name="qryResultID">Unique query result ID</param>
		/// <returns>
		/// Returns the DataSet object that contains the last available data block.
		/// </returns>
		public DataSet GetDataSetLastBlock (String qryResultID)
		{
			QryResult qryResult = QryResultFactory.GetInstance().GetQryResult(qryResultID);

			if ( null == qryResult )
			{
				throw new DataAccessException("ERROR_DATAACCESS_QUERYRESULT_NOT_FOUND");
			}
			
			return qryResult.GetDataSetLastBlock();
		}

		/// <summary>
		/// Querying the query processing status (processed, executed, block reading started,...)
		/// </summary>
		/// <param name="qryResultID">Unique query result ID</param>
		/// <returns>query processing status</returns>
		public QryStatus GetQryResultStatus (String qryResultID)
		{
			QryResult qryResult = QryResultFactory.GetInstance().GetQryResult(qryResultID);
			
			if ( null == qryResult )
			{
				throw new DataAccessException("ERROR_DATAACCESS_QUERYRESULT_NOT_FOUND");
			}

			return qryResult.QryResultStatus;
		}

		/// <summary>
		/// Querying the number of all processed records
		/// </summary>
		/// <param name="qryResultID">Unique query result ID</param>
		/// <returns>Number of all processed records</returns>
		public int GetReturnNumberOfRecords (String qryResultID)
		{
			QryResult qryResult = QryResultFactory.GetInstance().GetQryResult(qryResultID);
			
			if ( null == qryResult )
			{
				throw new DataAccessException("ERROR_DATAACCESS_QUERYRESULT_NOT_FOUND");
			}

			return qryResult.ReturnNumberOfRecords;
		}

		/// <summary>
		/// Querying the current max available processed block
		/// </summary>
		/// <param name="qryResultID">Unique query result ID</param>
		/// <returns>Max available processed block</returns>
		public int GetMaxAvailableBlocks (String qryResultID)
		{
			QryResult qryResult = QryResultFactory.GetInstance().GetQryResult(qryResultID);
			
			if ( null == qryResult )
			{
				throw new DataAccessException("ERROR_DATAACCESS_QUERYRESULT_NOT_FOUND");
			}

			return qryResult.MaxAvailableBlocks;
		}

		/// <summary>
		/// Querying the current block in UI
		/// </summary>
		/// <param name="qryResultID">Unique query result ID</param>
		/// <returns>current block in UI</returns>
		public int GetCurrentUIBlockNumber (String qryResultID)
		{
			QryResult qryResult = QryResultFactory.GetInstance().GetQryResult(qryResultID);
			
			if ( null == qryResult )
			{
				throw new DataAccessException("ERROR_DATAACCESS_QUERYRESULT_NOT_FOUND");
			}

			return qryResult.CurrentUIBlockNumber;
		}

		/// <summary>
		/// Gets an ArrayList that includes the absolute paths of the XML data files.
		/// </summary>
		/// <param name="qryResultID"></param>
		/// <returns>An ArrayList.</returns>
		public ArrayList GetXmlDataFileList(String qryResultID)
		{
			QryResult qryResult = QryResultFactory.GetInstance().GetQryResult(qryResultID);
			
			if ( null == qryResult )
			{
				throw new DataAccessException("ERROR_DATAACCESS_QUERYRESULT_NOT_FOUND");
			}

			return qryResult.GetPathArray();

		}

		/// <summary>
		/// Gets the user path to a provided query result id.
		/// </summary>
		/// <param name="qryResultID">Query result id.</param>
		/// <returns>An string containing a path.</returns>
		public String GetUserPath(String qryResultID)
		{
			QryResult qryResult = QryResultFactory.GetInstance().GetQryResult(qryResultID);
			
			if ( null == qryResult )
			{
				throw new DataAccessException("ERROR_DATAACCESS_QUERYRESULT_NOT_FOUND");
			}

			return qryResult.XmlUserPath;
		}


		/// <summary>
		/// Gets the user http path to a provided query result id.
		/// </summary>
		/// <param name="qryResultID">Query result id.</param>
		/// <returns>An string containing a path.</returns>
		public String GetUserPathHttp(String qryResultID)
		{
			QryResult qryResult = QryResultFactory.GetInstance().GetQryResult(qryResultID);
			
			if ( null == qryResult )
			{
				throw new DataAccessException("ERROR_DATAACCESS_QUERYRESULT_NOT_FOUND");
			}

			return qryResult.XmlUserPathHttp;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="qryResultID"></param>
		/// <returns></returns>
		public String GetXmlDataPath(String qryResultID)
		{
			QryResult qryResult = QryResultFactory.GetInstance().GetQryResult(qryResultID);
			
			if ( null == qryResult )
			{
				throw new DataAccessException("ERROR_DATAACCESS_QUERYRESULT_NOT_FOUND");
			}

			return qryResult.ReturnXMLFilePath;
		}

		/// <summary>
		/// Aborts the XML background processing of a query result, if one 
		/// exists and it is still alive.
		/// </summary>
		/// <param name="qryResultID">Unique query result ID</param>
		public void AbortXmlBackgroundProcessing(String qryResultID)
		{
			// get the corresponding query result.
			QryResult qryResult = QryResultFactory.GetInstance().GetQryResult(qryResultID);

			if ( null == qryResult )
			{
				throw new DataAccessException("ERROR_DATAACCESS_QUERYRESULT_NOT_FOUND");
			}
			
			// if there is a thread.
			if ( qryResult.XmlProcessingThread != null )
			{
				// if it is still alive, abort it.
				if ( qryResult.XmlProcessingThread.IsAlive )
				{
					// abort the thread.
					qryResult.XmlProcessingThread.Abort();
				}

				qryResult.XmlProcessingThread = null;
			}
		}

		/// <summary>
		/// Sets the session to an inactive state.
		/// </summary>
		/// <param name="sessionId">Session id.</param>
		/// <remarks>
		/// Setting the session to an inactive state means, that the session data will be
		/// deleted.
		/// </remarks>
		public void SetSessionInActive(String sessionId)
		{
			// Set the session inactive.
			SessionStateManager.GetInstance().SetInactive(sessionId);

			// Free memory from the result factory.
			QryResultFactory.GetInstance().RemoveQryResultBySession(sessionId);
		}

		/// <summary>
		/// Sets the excel processing in the session state to started.
		/// </summary>
		/// <param name="sessionId">The session id.</param>
		public void SetExcelProcessingStart(String sessionId)
		{
			SessionStateManager.GetInstance().SetExcelStarted(sessionId);
		}

		/// <summary>
		/// Sets the excel processing in the session state to finished.
		/// </summary>
		/// <param name="sessionId">The session id.</param>
		public void SetExcelProcessingEnd(String sessionId)
		{
			SessionStateManager.GetInstance().SetExcelFinished(sessionId);
		}
		/// <summary>
		/// Terminates the running MDX and cleanup thread
		/// </summary>
		public void TerminateRunningThreads()
		{
			if (null != mdxThread && mdxThread.IsAlive == true)
			{
								
				mdxThread.Abort();
				mdxThread = null;
			}
			SessionStateManager.GetInstance().StopCleanUpThread();
		}
		public String dbConnectionInfo()
		{   de.pta.Component.DataAccess.Internal.DBConnectionInfo dbConnectionInfo;
			dbConnectionInfo =  DBConnectionInfoFactory.GetInstance().GetDBConnectionInfo(DBConnectionInfo.TYPE_SQL);
			
			return dbConnectionInfo.ConnectString;
		}



	#endregion
	}
}

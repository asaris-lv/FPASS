using System;
using System.Collections;


namespace de.pta.Component.DataAccess.Internal
{
	/// <summary>
	/// Factory for query results. Each time the business logic calls ExecuteQuery there
	/// will create a new QryResult object that contains all necessary information for
	/// the current query context (e.g. the DataSet of current block in the retrieved data result).
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> A.Opierzynski, PTA GmbH
	/// <b>Date:</b> Apr/02/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class QryResultFactory
	{
		#region Members

		private static		QryResultFactory instance = null;
		private static int	qryResultIDPartOne = 0;
		private Hashtable	qryResults;
		private String		qryResultID;

		#endregion //End of Members

		#region Constructors

		protected QryResultFactory()
		{
			// Private constructor assures that no further instance is created.
			initialize();
		}

		#endregion //End of Constructors

		#region Initialization

		private void initialize()
		{
			// Initializes the members.
			qryResults		= new Hashtable();
			qryResultID		= "";
		}

		/// <summary>
		/// Singleton - returns exactly one instance of that class
		/// </summary>
		/// <returns>Instance of a QryResultFactory</returns>
		public static QryResultFactory GetInstance()
		{
			if( instance == null )
			{
				instance = new QryResultFactory();
			}

			return instance;
		}
		#endregion //End of Initialization

		#region Accessors
		#endregion //End of Accessors

		#region Methods

		/// <summary>
		/// Creation of a new QryResult object with a unique qryResultID. Result object is filled
		/// with initial values and the current Session ID and will be managed from QryResultFactory
		/// object.
		/// </summary>
		/// <param name="sessionID">current Session ID from HTTP session context</param>
		/// <returns>unique qryResultID for further using in other objects</returns>
		public String CreateQueryResult(String sessionID) 
		{
			//iteration of unique qryResultID
			++qryResultIDPartOne;
			qryResultID = sessionID + "_" + qryResultIDPartOne.ToString();

			//new QryResult object with initial values
			QryResult qryResult = new QryResult(qryResultID);

			qryResult.SessionID		  = sessionID;
			qryResult.QryResultStatus = DataAccessManager.QryStatus.INIT;

            //add new query Result object to list
			this.qryResults.Add (qryResultID, qryResult);
			return qryResultID;
		}

		/// <summary>
		/// Returns the query result object to the given query result id.
		/// </summary>
		/// <param name="qryResultID">Result id.</param></param>
		/// <returns>A reference to a query result object.</returns>
		public QryResult GetQryResult(String qryResultID) 
		{
			QryResult qryResult = (QryResult)qryResults[qryResultID];

			// Error - database connection object for specified ID not found
			if (qryResult == null)
			{
				throw new DataAccessException("ERROR_DATAACCESS_QRY_RESULT_NOT_FOUND");
			}

			return qryResult;
		}

		/// <summary>
		/// Removes the reference - if any exists - from the internal 
		/// collection of query results.
		/// </summary>
		/// <param name="qryResult">Result id.</param>
		public void RemoveQryResult(String qryResultID)
		{
			// If key exists, remove it.
			if ( qryResults.ContainsKey(qryResultID) )
			{
				qryResults.Remove(qryResultID);
			}
		}

		/// <summary>
		/// Removes all query results for the given session id.
		/// </summary>
		/// <param name="sessionId">The session id's query results to remove.</param>
		public void RemoveQryResultBySession(String sessionId)
		{
			QryResult qryResult  = null;
			ArrayList deleteList = new ArrayList();

			// Loop all entries of the query hashtable.
			// If the session id of a query result object matches to
			// the given session id, mark for deletion.
			IDictionaryEnumerator enumerator = qryResults.GetEnumerator();
			while ( enumerator.MoveNext() )
			{
				// Get QryResultObject.
				qryResult = (QryResult)enumerator.Value;

				// If the session id matches, delete the object.
				if ( qryResult.SessionID.Equals(sessionId) )
				{
					// Add to delete list.
					deleteList.Add(enumerator.Key);
				}
			}

			// Process the delete list.
			foreach ( String qryResultID in deleteList )
			{
				RemoveQryResult(qryResultID);
			}
		}

		#endregion
	}
}


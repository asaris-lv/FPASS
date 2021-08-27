using System;
using System.Threading;
using System.Collections;
using System.Text;
using System.Diagnostics;

namespace de.pta.Component.DataAccess.Internal
{
	/// <summary>
	/// Encapsulates the management of the session.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> M.Keller, PTA GmbH
	/// <b>Date:</b> Apr/09/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class SessionStateManager
	{
		#region Members

		private static SessionStateManager instance = null;
		private Hashtable sessions;
		private Thread cleanUp;

		#endregion //End of Members


		#region Constructors

		protected SessionStateManager()
		{
			// Protected Construction for singleton reasons.
			initialize();
		}

		#endregion //End of Constructors


		#region Initialization

		private void initialize()
		{
			// Initializes the members.
			sessions	= new Hashtable();
			cleanUp		= null;
		}

		/// <summary>
		/// Singleton - returns exactly one instance of that class
		/// </summary>
		/// <returns>Instance of a SessionStateManager</returns>
		public static SessionStateManager GetInstance()
		{
			if( null == instance )
			{
				instance = new SessionStateManager();
			}

			return instance;
		}


		#endregion //End of Initialization

		#region Accessors 
		#endregion //End of Accessors
		
		#region Methods


		/// <summary>
		/// Stops the clean up thread of the session state manager.
		/// </summary>
		public void StartCleanUpThread()
		{
			if ( null == cleanUp )
			{
				CleanUp clean = new CleanUp();
				cleanUp = new Thread(new ThreadStart(clean.Start));
				cleanUp.Name = "CleanUp";
				cleanUp.Start();

				// wait until impersonation is finsihed.
				clean.WaitForImpersonation();

				// If an error occured, get error text.
				if ( !clean.LastError.Equals(String.Empty) )
				{
					throw new DataAccessException(clean.LastError);
				}
			}
		}

		/// <summary>
		/// Stops the clean up thread of the session state manager.
		/// </summary>
		public void StopCleanUpThread()
		{
			if ( null != cleanUp )
			{
				if ( cleanUp.IsAlive )
				{
					cleanUp.Abort();
					cleanUp = null;
				}
			}
		}

		/// <summary>
		/// Adds a new session with default values to the internal session collection.
		/// </summary>
		/// <param name="sessionId">Id of the session.</param>
		/// <param name="userId">Id of the user.</param>
		/// <param name="xmlSessionPath">Path of the xml data.</param>
		public void AddSession(String sessionId, String userId, String xmlSessionPath)
		{
			AddSession(sessionId, userId, xmlSessionPath, true, true, DateTime.Now);
		}

		/// <summary>
		/// /// Adds a new session to the internal session collection.
		/// </summary>
		/// <param name="sessionId">Id of the session.</param>
		/// <param name="active">States if session is active.</param>
		/// <param name="xmlSessionPath">Path of the xml data.</param>
		/// <param name="excelFinished">States if excel processing is finished.</param>
		/// <param name="lastModified">The last modified Date.</param>
		public void AddSession(String sessionId, String userId, String xmlSessionPath, bool active, bool excelFinished, DateTime lastModified)
		{
			lock(this)
			{
				if ( !sessions.ContainsKey(sessionId) )
				{
					SessionState session = new SessionState();

					session.SessionId		= sessionId;
					session.User			= userId;
					session.XmlSessionPath	= xmlSessionPath;
					session.SessionActive	= active;
					session.ExcelFinished	= excelFinished;
					session.LastModified	= lastModified;

					sessions.Add(sessionId, session);
				}
			}
		}

		/// <summary>
		/// Removes a session state object from the internal collection.
		/// </summary>
		/// <param name="sessionId">Session id.</param>
		public void RemoveSession(String sessionId)
		{
			lock(this)
			{
				// Ensure that the object to delete is in the collection.
				if ( sessions.ContainsKey(sessionId) )
				{
					sessions.Remove(sessionId);
				}
			}
		}

		/// <summary>
		/// Removes all sessions from the internal collection.
		/// </summary>
		public void RemoveAllSessions()
		{
			lock(this)
			{
				sessions.Clear();
			}
		}

		/// <summary>
		/// Sets a session to the active state.
		/// </summary>
		/// <param name="sessionId">Session id</param>
		public void SetActive(String sessionId)
		{
			lock(this)
			{
				SessionState session = getSessionState(sessionId);

				if ( null != session )
				{
					session.SessionActive = true;
				}
			}
		}

		/// <summary>
		/// Sets a session to the inactive state.
		/// </summary>
		/// <param name="sessionId">Session id.</param>
		public void SetInactive(String sessionId)
		{
			lock(this)
			{
				SessionState session = getSessionState(sessionId);

				if ( null != session )
				{
					session.SessionActive = false;
				}
			}
		}

		/// <summary>
		/// Set the excel processing started flag for a session to true.
		/// </summary>
		/// <param name="sessionId">Session id</param>
		public void SetExcelStarted(String sessionId)
		{
			lock(this)
			{
				SessionState session = getSessionState(sessionId);

				if ( null != session )
				{
					session.ExcelFinished = false;
				}
			}
		}

		/// <summary>
		/// Set the excel processing started flag for a session to false.
		/// </summary>
		/// <param name="sessionId">Session id</param>
		public void SetExcelFinished(String sessionId)
		{
			lock(this)
			{
				SessionState session = getSessionState(sessionId);

				if ( null != session )
				{
					session.ExcelFinished = true;
				}
			}
		}

		/// <summary>
		/// Sets the last modified stamp for a session.
		/// </summary>
		/// <param name="sessionId">Session id</param>
		/// <param name="lastModified">Last modified stamp.</param>
		public void SetLastModified(String sessionId, DateTime lastModified)
		{
			lock(this)
			{
				SessionState session = getSessionState(sessionId);

				if ( null != session )
				{
					session.LastModified = lastModified;
				}
			}
		}

		/// <summary>
		/// Indicates, if a session is active or not.
		/// </summary>
		/// <param name="sessionId">The session id.</param>
		/// <returns>
		/// Returns true, if the session is active, otherwise false.
		/// </returns>
		public bool IsActive(String sessionId)
		{
			lock(this)
			{
				bool isActive = false;
				SessionState session = getSessionState(sessionId);

				if ( null != session )
				{
					isActive = session.SessionActive;
				}

				return isActive;
			}
		}

		/// <summary>
		/// Indicates, if the excel processing of a session is finished or not.
		/// </summary>
		/// <param name="sessionId">The session id.</param>
		/// <returns>
		/// Returns true, if the excel processing is finished, otherwise false.
		/// </returns>
		public bool IsExcelFinished(String sessionId)
		{
			lock(this)
			{
				bool isExcelFinished = false;
				SessionState session = getSessionState(sessionId);

				if ( null != session )
				{
					isExcelFinished = session.ExcelFinished;
				}

				return isExcelFinished;
			}
		}

		private SessionState getSessionState(String sessionId)
		{
			// Retrieves the corresponding session state object to the
			// given session id from the internal collection.
			// If no object is found in the collection null is returned.

			SessionState session = null;

			if ( sessions.ContainsKey(sessionId) )
			{
				session = (SessionState)sessions[sessionId];
			}

			return session;
		}

		/// <summary>
		/// Gets the path, where the data is stored.
		/// </summary>
		/// <param name="?">The session id.</param>
		/// <returns>
		/// A string including the path. If the session is not found an 
		/// empty string is returned.
		/// </returns>
		public String GetPath(String sessionId)
		{
			lock(this)
			{
				String path = String.Empty;
				SessionState session = getSessionState(sessionId);

				if ( null != session )
				{
					path = session.XmlSessionPath;
				}

				return path;
			}
		}

		/// <summary>
		/// Gets a list of sessions administated.
		/// </summary>
		/// <returns>An ArrayList that contains all sessions id.</returns>
		public ArrayList GetSessionList()
		{
			lock(this)
			{
				ArrayList		sessionList = new ArrayList(sessions.Count);
				SessionState	session		= null;
				IDictionaryEnumerator en	= sessions.GetEnumerator();

				while ( en.MoveNext() )
				{
					session = (SessionState)en.Value;
					sessionList.Add(session.SessionId);
				}

				return sessionList;
			}
		}

		#endregion //End of Methods

	}
}

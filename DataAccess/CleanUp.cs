using System;
using System.Threading;
using System.Collections;
using System.IO;
using System.Diagnostics;
using System.Security.Principal;

using de.pta.Component.Common;

namespace de.pta.Component.DataAccess.Internal
{
	/// <summary>
	/// Performs cleanup tasks, such as deleing files, that are no longer used.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> M.Keller, PTA GmbH
	/// <b>Date:</b> Apr/10/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class CleanUp : WorkerThread
	{
		#region Members

		private int				timeToWaitInSeconds;
		private DateTime		clearAllTime;
		private bool			isStarted;
		
		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Constructs the object.
		/// </summary>
		public CleanUp() : base()
		{
			initialize();
		}

		#endregion //End of Constructors

		#region Initialization

		private void initialize()
		{
			// Initializes the members.
			timeToWaitInSeconds = 600; // Default 10 min -> 600 sec

			// Set clearAllTime to actual day and default time.
			DateTime work       = DateTime.Now;
			clearAllTime		= new DateTime(work.Year, work.Month, work.Day, 23, 59, 00);
			isStarted			= true;
		}	

		#endregion //End of Initialization

		#region Accessors 

		/// <summary>
		/// Gets or sets the time to sleep the thread in seconds.
		/// </summary>
		public int TimeToWaitInSeconds
		{
			get
			{
				return timeToWaitInSeconds;
			}
			set
			{
				timeToWaitInSeconds = value;
			}
		}

		#endregion //End of Accessors

		#region Methods

		/// <summary>
		/// Starts the cleaning.
		/// </summary>
		override public void Start()
		{
			try
			{
				// Get the configuration details.
				String user			= DataAccessConfiguration.GetInstance().CleanUpUser;
				String host			= DataAccessConfiguration.GetInstance().CleanUpHost;
				String pwd			= DataAccessConfiguration.GetInstance().CleanUpPwd;
				timeToWaitInSeconds	= DataAccessConfiguration.GetInstance().TimeInSeconds;
				clearAllTime		= DateTime.Parse(DataAccessConfiguration.GetInstance().ClearAllTime);

				// Ensure that first master clean runs tomorrow.
				if ( clearAllTime <= DateTime.Now )
				{
					clearAllTime = clearAllTime.AddDays(1.0);
				}

				// Impersonate user.
				impersonation(user, host, pwd);

				// work...
				doTheWork();
			}
			catch (ThreadAbortException)
			{
				// TODO: clean up the clean up.
			}
		}

		private void doTheWork()
		{
			while ( true )
			{
				
				if ( (isStarted) || (clearAllTime <= DateTime.Now) )
				{
					isStarted = false;

					// Add one day, for the next run tomorrow.
					clearAllTime = clearAllTime.AddDays(1.0);

					// do the master clean...
					masterClean();
				}
				else
				{
					// check the sessions.
					checkSessions();
				}

				// wait to the next 
				wait();
			}
		}

		private void masterClean()
		{
			

			// Get the root path for the xml data and create a DircetoryInfo
			String root = DataAccessConfiguration.GetInstance().XmlTempPath;
			DirectoryInfo rootDir = new DirectoryInfo(root);

			// Only try to delete subdirectories, if the root directory
			// exists.
			if ( rootDir.Exists )
			{
				// Get all sub directories.
				DirectoryInfo[] dirArray = rootDir.GetDirectories();

				// Delete all subdirectories
				foreach (DirectoryInfo dirInfo in dirArray)
				{
					deleteDirectory(dirInfo.FullName, true);
				}
			}

			// Remove all sessions.
			SessionStateManager.GetInstance().RemoveAllSessions();
		}

		private void checkSessions()
		{
			
			bool deleteData = false;

			// Gets the actual list of sessions.
			ArrayList sessionList = SessionStateManager.GetInstance().GetSessionList();

			// loop the list.
			foreach ( String sessionId in sessionList )
			{
				deleteData = false;

				// If the session is no longer active.
				if ( !SessionStateManager.GetInstance().IsActive(sessionId) )
				{
					// If the session's excel processing is finished, as well.
					if ( SessionStateManager.GetInstance().IsExcelFinished(sessionId) )
					{
						deleteData = true;
					}
				}

				// Delete session data if necessary.
				if ( deleteData )
				{
					// Get the path to delete.
					String xmlPath = SessionStateManager.GetInstance().GetPath(sessionId);

					deleteDirectory(xmlPath, true);

					// Remove Session
					SessionStateManager.GetInstance().RemoveSession(sessionId);
				}
			}
		}

		private void deleteDirectory(String directory, bool recursiv)
		{
			// Deletes the given directory either recusiv or not.

			try
			{
				DirectoryInfo dirInfo = new DirectoryInfo(directory);

				if ( dirInfo.Exists )
				{
					// Delete 
					dirInfo.Delete(recursiv);
				}
			}
			catch (Exception e)
			{
				String msg = String.Format("Error deleting directory <{0}> - {1}.", directory, e.Message);
				Debug.WriteLine(msg);
//				throw new DataAccessException(msg, e);
			}
		}


		private void wait()
		{
			// Sleeps the thread for the given time.

			// Calculate the miliseconds
			int timeToWaitInMilliSeconds = timeToWaitInSeconds * 1000;

			// Sleep, zzzzzzz
			Thread.Sleep(timeToWaitInMilliSeconds);
		}

		#endregion
	}
}

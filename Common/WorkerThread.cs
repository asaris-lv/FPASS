using System;
using System.Security.Principal;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;

namespace de.pta.Component.Common
{
	/// <summary>
	/// Abstact base class for all worker threads.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> M.Keller, PTA GmbH
	/// <b>Date:</b> Apr/22/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	public abstract class WorkerThread
	{
		#region Members

		private const String	LOCALHOST	= "localhost";
//		private		WindowsImpersonationContext winImpersonationCtx;
		protected	String lastError;
		protected	AutoResetEvent impersEvent;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Default Constructor
		/// </summary>
		public WorkerThread()
		{
			initialize();
		}

		#endregion //End of Constructors

		#region Initialization

		private void initialize()
		{
//			winImpersonationCtx = null;
			lastError			= String.Empty;
			impersEvent			= new AutoResetEvent(false);
		}

		#endregion //End of Initialization
		
		#region Accessors

		/// <summary>
		/// Gets the last error code (read only).
		/// </summary>
		public String LastError
		{
			get
			{
				return lastError;
			}
		}

		#endregion //End of Accessors

		#region Methods

		// Import the windows DLL, which has the entry point for LogonUser.
		[DllImport("advapi32.dll", SetLastError=true) ]

		// Declare LogonUser. C++ return value BOOL is an int in C#.
		public static extern int  LogonUser(String lpszUsername, 
											String lpszDomain, 
											String lpszPassword, 
											int dwLogonType,
											int dwLogonProvider,
											out int phToken);

		// Import the windows DLL, which has the entry point for ImpersonateLoggedOnUser.
		[DllImport("advapi32.dll", SetLastError=true) ]

		// Declare ImpersonateLoggedOnUser. C++ return value BOOL is an int in C#.
		public static extern int ImpersonateLoggedOnUser(int hToken);

		// Import the windows DLL, which has the entry point for RevertToSelf.
		[DllImport("advapi32.dll", SetLastError=true) ]

		// Declare RevertToSelf. C++ return value BOOL is an int in C#.
		public static extern int RevertToSelf();


		protected void impersonation(String user, String domain, String pwd)
		{
			int token1		= 0;
			int loggedOn	= 0;
			int win32Error	= 0;
			String msg		= "";

			// If localhost is found, use the local machine name.
			if ( domain.ToLower().Equals(LOCALHOST) )
			{
				domain = Environment.MachineName;
			}

			// Logon user
			// Logon type:      LOGON32_LOGON_NETWORK_CLEARTEXT = 3
			//					LOGON32_LOGON_INTERACTIVE		= 2
			// Logon provider:= LOGON32_PROVIDER_DEFAULT        = 0
			// use logon type 2 to have enought rights.
			loggedOn = LogonUser(user, domain, pwd, 2, 0, out token1);

			// If logon failed, set last error.
			if ( 0 == loggedOn )
			{
				win32Error = Marshal.GetLastWin32Error();
				msg = String.Format("Impersonation (logon user) error: <{0}> (native Win 32 code). Use the DOS command 'NET HELPMSG message#' for further information.", win32Error);
				lastError = msg;
				Debug.WriteLine(msg);

				// As long as the method fails here the thread runs
				// under the "old" user.
			}
			else
			{
				lastError = String.Empty;

				// Don't use the .NET frame work to impersonate, becaus it is not
				// really designed for out task (MDX access). Use the native win32 
				// function instead.
				if ( 0 == ImpersonateLoggedOnUser(token1) )
				{
					win32Error = Marshal.GetLastWin32Error();
					msg = String.Format("Impersonation (impersonate logged on user)error: <{0}> (native Win 32 code). Use the DOS command 'NET HELPMSG message#' for further information.", win32Error);
					lastError = msg;
					Debug.WriteLine(win32Error);
				}

/*				// Create handle for token and create new Identity.
				IntPtr token2 = new IntPtr(token1);
				WindowsIdentity ident = new WindowsIdentity(token2);

				// Impersonate the new user.
				winImpersonationCtx = ident.Impersonate();
*/				
			}

			// Finished
			impersEvent.Set();
		}

		protected void undoImpersonation()
		{
			// Don't use the .NET frame work to undo impersonattion, becaus it is not
			// really designed for out task (MDX access). Use the native win32 
			// function instead.

			RevertToSelf();


/*			// Undo impersonation
			if ( null != winImpersonationCtx )
			{
				winImpersonationCtx.Undo();
			}
*/			
		}

		/// <summary>
		/// Waits until the impersonation of the thread is finsihed.
		/// </summary>
		public void WaitForImpersonation()
		{
			AutoResetEvent[] events = new AutoResetEvent[1];
			events[0] = impersEvent;

			// wait a few seconds.
			WaitHandle.WaitAll(events, 5000, false);
		}

		/// <summary>
		/// Abstact method for starting the thread.
		/// </summary>
		abstract public void Start();

		#endregion // End of Methods


	}
}

using System;
using System.IO;
using System.Windows.Forms;

using log4net;
using log4net.Config;

namespace de.pta.Component.Logging.Log4NetWrapper
{
	/// <summary>
	/// This class implements a wrapper to the Log4Net class library.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> A. Seibt, PTA GmbH
	/// <b>Date:</b> Aug/19/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	public class Logger
	{
		/// <summary>The configuration file for Log4Net.</summary>
		private const string LOG4NET_CONFIGURATION = @"\Configuration\log4net.xml";

		/// <summary>A logger for debugging purpose.</summary>
		private ILog mLog = null;



		/// <summary>
		/// Constructor.
		/// </summary>
		public Logger() : this(typeof(Logger))
		{
		}


		/// <summary>
		/// Constructor.
		/// </summary>
		public Logger(Type pClass)
		{
			InitializeLogger();
			mLog = LogManager.GetLogger(pClass);
		}


		/// <summary>
		/// Constructor.
		/// </summary>
		public Logger(string pLogger)
		{
			InitializeLogger();
			mLog = LogManager.GetLogger(pLogger);
		}


		private void InitializeLogger()
		{
			string log4netConfig = GetApplicationPath() + LOG4NET_CONFIGURATION;
			DOMConfigurator.Configure(new FileInfo(log4netConfig));
		}



		/// <summary>
		/// Returns the base directory for the application. Usually this is the
		/// directory where the .exe file of the application is stored.
		/// </summary>
		/// <returns>The application base directory</returns>
		/// <remarks>
		/// The XML configuration file for the application is stored in a subdirectory
		/// named "Configuration" below the application root directory. The root directory
		/// differs if the application is started from Visual Studio .NET (it's the
		/// \bin\Debug or \bin\Release directory. This method strips the "\bin..." part of
		/// the application root directory so that the position of the "Configuration"
		/// directory can be the same for production and development environment
		/// </remarks>
		private string GetApplicationPath()
		{
			// get the application path
			string appPath = Application.StartupPath;

			// if running under Visual Studio, application path is extended by ...\bin\debug (or release)
			// because the executable is stored there.
			//int pos = appPath.IndexOf("bin");

			// if running in production, the executable is stored in the base directory.
//			if(pos >= 0) 
//			{
//				// pos - 1 to strip the trailing "\"
//				appPath = appPath.Substring(0, pos - 1);
//			}
			return appPath;
		}


		/// <summary>
		/// Logs a message of type DEBUG.
		/// </summary>
		/// <param name="pMsg">The message text.</param>
		public void Debug(string pMsg) 
		{
			if(mLog.IsDebugEnabled) 
			{
				mLog.Debug(pMsg);
			}
		}


        /// <summary>
        /// Returns true if debug is enabled.
        /// </summary>
        /// <param name="pMsg">The message text.</param>
        public bool IsDebugEnabled
        {
            get { return (mLog.IsDebugEnabled); }          
        }
		
		
		/// <summary>
		/// Logs a message of type DEBUG with an exception.
		/// </summary>
		/// <param name="pMsg">The message text.</param>
		/// <param name="pException">The exception.</param>
		public void Debug(string pMsg, Exception pException) 
		{
			if(mLog.IsDebugEnabled) 
			{
				mLog.Debug(pMsg, pException);
			}
		}



		/// <summary>
		/// Logs a message of type INFO.
		/// </summary>
		/// <param name="pMsg">The message text.</param>
		public void Info(string pMsg) 
		{
			if(mLog.IsInfoEnabled) 
			{
				mLog.Info(pMsg);
			}
		}

		
		
		/// <summary>
		/// Logs a message of type INFO with an exception.
		/// </summary>
		/// <param name="pMsg">The message text.</param>
		/// <param name="pException">The exception.</param>
		public void Info(string pMsg, Exception pException) 
		{
			if(mLog.IsInfoEnabled) 
			{
				mLog.Info(pMsg, pException);
			}
		}



		/// <summary>
		/// Logs a message of type WARN.
		/// </summary>
		/// <param name="pMsg">The message text.</param>
		public void Warn(string pMsg) 
		{
			if(mLog.IsWarnEnabled) 
			{
				mLog.Warn(pMsg);
			}
		}

		
		
		/// <summary>
		/// Logs a message of type WARN with an exception.
		/// </summary>
		/// <param name="pMsg">The message text.</param>
		/// <param name="pException">The exception.</param>
		public void Warn(string pMsg, Exception pException) 
		{
			if(mLog.IsWarnEnabled) 
			{
				mLog.Warn(pMsg, pException);
			}
		}



		/// <summary>
		/// Logs a message of type ERROR.
		/// </summary>
		/// <param name="pMsg">The message text.</param>
		public void Error(string pMsg) 
		{
			if(mLog.IsErrorEnabled) 
			{
				mLog.Error(pMsg);
			}
		}

		
		
		/// <summary>
		/// Logs a message of type ERROR with an exception.
		/// </summary>
		/// <param name="pMsg">The message text.</param>
		/// <param name="pException"></param>
		public void Error(string pMsg, Exception pException) 
		{
			if(mLog.IsErrorEnabled) 
			{
				mLog.Error(pMsg, pException);
			}
		}

		/// <summary>
		/// Logs a message of type FATAL.
		/// </summary>
		/// <param name="pMsg">The message text.</param>
		public void Fatal(string pMsg) 
		{
			if(mLog.IsFatalEnabled) 
			{
				mLog.Fatal(pMsg);
			}
		}

		
		
		/// <summary>
		/// Logs a message of type FATAL with an exception.
		/// </summary>
		/// <param name="pMsg">The message text.</param>
		/// <param name="pException"></param>
		public void Fatal(string pMsg, Exception pException) 
		{
			if(mLog.IsFatalEnabled) 
			{
				mLog.Fatal(pMsg, pException);
			}
		}

	}
}

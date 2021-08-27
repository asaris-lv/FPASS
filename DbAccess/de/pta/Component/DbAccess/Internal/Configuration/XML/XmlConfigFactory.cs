using System;
using System.Windows.Forms;

using de.pta.Component.Common;
using de.pta.Component.DbAccess.Exceptions;
using de.pta.Component.Logging.Log4NetWrapper;

namespace de.pta.Component.DbAccess.Internal.Configuration.XML
{
	/// <summary>
	/// Assembles a configuration for the component based on an XML configuration file.
	/// </summary>
	/// <remarks>
	/// <para><b>History</b></para>
	/// <list type="table">
	/// <item>
	/// <term><b>Author:</b></term>
	/// <description>A. Seibt, PTA GmbH</description>
	/// </item>
	/// <item>
	/// <term><b>Date:</b></term>
	/// <description>Aug/29/2003</description>
	/// </item>
	/// <item>
	/// <term><b>Remarks:</b></term>
	/// <description>initial version</description>
	/// </item>
	/// </list>
	/// </remarks>
	internal class XmlConfigFactory : IConfigFactory
	{
		#region Members

		/// <summary>
		/// The section in Configuration.xml to process.
		/// </summary>
		private const string XML_SECTION = "application/configuration/DbAccess";

		/// <summary>
		/// Enables logging functionality.
		/// </summary>
		private static Logger mLog = new Logger(typeof(ConfigFactory));

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public XmlConfigFactory()
		{
			initialize();
		}

		#endregion //End of Constructors

		#region Initialization

		/// <summary>
		/// Initializes the members.
		/// </summary>
		private void initialize()
		{
		}	

		#endregion //End of Initialization

		#region Accessors 

		#endregion //End of Accessors

		#region Methods 

		#endregion // End of Methods

		#region IConfigFactory Members

		/// <summary>
		/// Returns the data access configuration for the application.
		/// </summary>
		/// <returns></returns>
		public DbAccessConfiguration GetConfiguration()
		{
			mLog.Debug("Creating new XmlMenuConfigProcessor");
			XmlConfigProcessor processor = new XmlConfigProcessor();
			ConfigReader cr = ConfigReader.GetInstance();
			cr.ApplicationRootPath = GetApplicationPath();
			try 
			{
				mLog.Debug("Reading configuration");
				cr.ReadConfig(XML_SECTION, processor);
				mLog.Info("Configuration successfully read");
				return DbAccessConfiguration.GetInstance();
			}
			catch(CommonXmlException e) 
			{
				mLog.Fatal("Error while reading configuration", e);
				throw new DbAccessConfigurationException("Invalid configuration file found", e);
			}
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

//			// if running under Visual Studio, application path is extended by ...\bin\debug (or release)
//			// because the executable is stored there.
//			//int pos = appPath.IndexOf("bin");
//
//			// if running in production, the executable is stored in the base directory.
//			// if(pos >= 0) 
//			//{
//				// pos - 1 to strip the trailing "\"
//			//	appPath = appPath.Substring(0, pos - 1);
			//}
			return appPath;
		}

		#endregion
	}
}

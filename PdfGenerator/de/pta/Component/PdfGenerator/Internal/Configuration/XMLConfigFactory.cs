using System;
using System.Windows.Forms;
using de.pta.Component.Common;
using de.pta.Component.PdfGenerator.Exceptions.Internal.Configuration;

namespace de.pta.Component.PdfGenerator.Internal.Configuration
{
	/// <summary>
	/// A factory to get an XML2PDFConfiguration read from an XML configuration file.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> M. Mehlem, PTA GmbH
	/// <b>Date:</b> Aug/28/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class XMLConfigFactory : ConfigFactory
	{
		#region Members

		/// <summary>
		/// The root node of the PdfGenerator configuration part.
		/// </summary>
		private const string XML_SECTION = "Application/Configuration/PdfGenerator";

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Default Constructor
		/// </summary>
		public XMLConfigFactory()
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

		/// <summary>
		/// Returns the configuration for the PdfGenerator component read from an XML configuration file, 
		/// analysed by the XMLConfigProcessor.
		/// </summary>
		/// <returns>A filled XML2PDFConfiguration.</returns>
		/// <exception cref="ConfigurationException">Thrown when analysing the configuration.xml file fails.</exception>
		public override XML2PDFConfiguration GetConfiguration()
		{
			// get the application path
			string appPath = Application.StartupPath;

			// if running under Visual Studio, application path is extended by ...\bin\debug (or release)
			// because the executable is stored there.
			int pos = appPath.IndexOf("bin");

			// if running in production, the executable is stored in the base directory.
			if(pos >= 0) 
			{
				appPath = appPath.Substring(0, pos - 1);
			}

			XMLConfigProcessor processor = new XMLConfigProcessor();
			ConfigReader cr = ConfigReader.GetInstance();
			cr.ApplicationRootPath = appPath;
			try 
			{
				cr.ReadConfig(XML_SECTION, processor);
				return XML2PDFConfiguration.GetInstance();
			}
			catch(CommonXmlException e) 
			{
				throw new ConfigurationException("Invalid configuration file found", e);
			}
		}

		#endregion // End of Methods


	}
}

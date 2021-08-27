using System;
using System.Windows.Forms;
using de.pta.Component.Common;
using de.pta.Component.PdfGenerator.Exceptions.Internal.Configuration;

namespace de.pta.Component.PdfGenerator.Internal.Configuration
{
	/// <summary>
	/// XMLConfigProcessor parses an application's XML configuration file and analyses the
	/// specified paths settings for generating PDF files from XML templates.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> M. Mehlem, PTA GmbH
	/// <b>Date:</b> Aug/28/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class XMLConfigProcessor : IConfigProcessor
	{
		#region Members

		// XML elements and attributes defining the PdfGenertor configuration
		private const string PDF_GENERATOR			= "PDFGENERATOR";
		private const string REPORTS				= "REPORTS";
		private const string REPORT 				= "REPORT";
		private const string REPORT_ID 				= "REPORTID";
		private const string REPORT_USER_PROFILES	= "REPORTUSERPROFILES";
		private const string USER_PROFILE			= "USERPROFILE";
		private const string PROFILE_ID				= "PROFILEID";
		private const string BASE_PATH				= "BASEPATH";
		private const string APPLICATION_ROOT		= "APPLICATIONROOT";
		private const string PDF_ROOT				= "PDFROOT";
		private const string ATTRIBUTES				= "ATTRIBUTES";
		private const string ATTRIBUTE				= "ATTRIBUTE";
		private const string LAYOUT_REQUIRED		= "LAYOUTREQUIRED";
		private const string DEFAULT_TEMPLATE_FILE	= "DEFAULTTEMPLATEFILE";
		private const string DEFAULT_LAYOUT_FILE	= "DEFAULTLAYOUTFILE";
		private const string DEFAULT_PDF_FILE		= "DEFAULTPDFFILE";
		private const string TEMPLATE_FILE			= "TEMPLATEFILE";
		private const string LAYOUT_FILE			= "LAYOUTFILE";
		private const string PDF_FILE				= "PDFFILE";
		private const string VALUE					= "VALUE";
		// Controle of the currently processed parent node
		private bool mPDFRootOpened;
		private bool mReportsRootOpened;
		private bool mReportOpened;
		private bool mReportUserProfilesOpened;
		private bool mUserProfileOpened;
		private bool mAttributesOpened;
		// The currently analysed report configuration
		private ReportConfiguration mReportConfig;
		// The currently analysed report user profile configuration
		private ReportUserProfile mReportUserProfile;

		// The configuration that will be filled by this processor
		private XML2PDFConfiguration mConfig;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Default Constructor
		/// </summary>
		public XMLConfigProcessor()
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
			// ToDo: Initialisieren 
			mConfig = XML2PDFConfiguration.GetInstance();
		}	

		#endregion //End of Initialization

		#region Accessors 

		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// Processes a ConfigNode of type 'block begin'.
		/// </summary>
		/// <param name="cNode">A currently analysed ConfigNode.</param>
		public void  ProcessConfigBlockBegin(ConfigNode cNode)
		{
			if ( cNode.NodeName.Equals(PDF_GENERATOR) )
			{
				// ListsOfValue opened
				mPDFRootOpened = true;
			}
			else if ( cNode.NodeName.Equals(REPORTS) )
			{
				// ListOfValue opened
				mReportsRootOpened = true;
			}
			else if ( cNode.NodeName.Equals(REPORT) )
			{
				// ListOfValue opened
				mReportOpened = true;
				mReportConfig = new ReportConfiguration();
			}
			else if ( cNode.NodeName.Equals(REPORT_USER_PROFILES) )
			{
				// ListOfValue opened
				mReportUserProfilesOpened = true;
			}
			else if ( cNode.NodeName.Equals(USER_PROFILE) )
			{
				// ListOfValue opened
				mUserProfileOpened = true;
				mReportUserProfile = new ReportUserProfile();
			}
			else if ( cNode.NodeName.Equals(ATTRIBUTES) )
			{
				// Attributes opened
				mAttributesOpened = true;
			}
			else
			{
				// Found something wrong!
			}
		}

		/// <summary>
		/// Processes a ConfigNode of type 'item'.
		/// </summary>
		/// <param name="cNode">A currently analysed ConfigNode.</param>
		/// <exception cref="ConfigurationException">Thrown when the PdfGenerator root node or the Reports node is not open when 
		/// invoking this method.</exception>
		public void  ProcessConfigItem(ConfigNode cNode)
		{
			// must be inside the REPORTS block
			if ( !mPDFRootOpened )
			{
				throw new ConfigurationException("Configuration analysis for PdfGenerator failed: <PdfGenerator> root node is closed");
			}
			// must be inside the REPORTS block
			if ( !mReportsRootOpened )
			{
				throw new ConfigurationException("Configuration analysis for PdfGenerator failed: <Reports> node is missing or closed");
			}
			// analysing the child elements of a REPORTS block
			if ( cNode.NodeName.Equals(BASE_PATH) && cNode.NodeAttributes.ContainsKey(VALUE) )
			{
				string configBasePath = cNode.NodeAttributes[VALUE].ToString();
				if(APPLICATION_ROOT.Equals(configBasePath.ToUpper()))
				{
					configBasePath = GetApplicationRoot();
					if(cNode.NodeAttributes.ContainsKey(PDF_ROOT))
					{
						configBasePath += cNode.NodeAttributes[PDF_ROOT].ToString();
					}
				}
				mConfig.BasePath = configBasePath;

			}
			// inside of a REPORT block
			if ( mReportOpened )
			{
				// analysing the child elements and attributes a REPORT block
				// ID of a report
				if ( cNode.NodeName.Equals(REPORT_ID) )
				{
					mReportConfig.Id = cNode.NodeValue;
				}
				// inside the attributes list of a report configuration
				if ( mAttributesOpened )
				{
					if ( cNode.NodeName.Equals(ATTRIBUTE) )
					{
						if ( cNode.NodeAttributes.ContainsKey(LAYOUT_REQUIRED) )
						{
							bool required = "TRUE".Equals(cNode.NodeAttributes[LAYOUT_REQUIRED].ToString().ToUpper());
							mReportConfig.LayoutRequired = required;
						}
						if ( cNode.NodeAttributes.ContainsKey(DEFAULT_TEMPLATE_FILE) )
						{
							mReportConfig.DefaultTemplateFile = cNode.NodeAttributes[DEFAULT_TEMPLATE_FILE].ToString();
						}
						if ( cNode.NodeAttributes.ContainsKey(DEFAULT_LAYOUT_FILE) )
						{
							mReportConfig.DefaultLayoutFile = cNode.NodeAttributes[DEFAULT_LAYOUT_FILE].ToString();
						}
						if ( cNode.NodeAttributes.ContainsKey(DEFAULT_PDF_FILE) )
						{
							mReportConfig.DefaultPDFFile = cNode.NodeAttributes[DEFAULT_PDF_FILE].ToString();
						}
					}
				}
				if( mReportUserProfilesOpened && mUserProfileOpened )
				{
					// analysing the child elements and attributes a user profile block
					// ID of a user profile
					if ( cNode.NodeName.Equals(PROFILE_ID) )
					{
						mReportUserProfile.ProfileId = cNode.NodeValue;
					}
					// inside the attributes list of a report configuration
					if ( mAttributesOpened )
					{
						if ( cNode.NodeName.Equals(ATTRIBUTE) )
						{
							if ( cNode.NodeAttributes.ContainsKey(TEMPLATE_FILE) )
							{
								mReportUserProfile.TemplateFile = cNode.NodeAttributes[TEMPLATE_FILE].ToString();
							}
							if ( cNode.NodeAttributes.ContainsKey(LAYOUT_FILE) )
							{
								mReportUserProfile.LayoutFile = cNode.NodeAttributes[LAYOUT_FILE].ToString();
							}
							if ( cNode.NodeAttributes.ContainsKey(PDF_FILE) )
							{
								mReportUserProfile.PDFFile = cNode.NodeAttributes[PDF_FILE].ToString();
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// Processes a ConfigNode of type 'block end'.
		/// </summary>
		/// <param name="cNode">A currently analysed ConfigNode.</param>
		public void  ProcessConfigBlockEnd(ConfigNode cNode)
		{
			if ( cNode.NodeName.Equals(PDF_GENERATOR) )
			{
				// ListsOfValues closed
				mPDFRootOpened = false;
				mConfig.AlreadyConfigured = true;
			}
			else if ( cNode.NodeName.Equals(REPORTS) )
			{
				// ListOfValue closed
				mReportsRootOpened = false;
			}
			else if ( cNode.NodeName.Equals(REPORT) )
			{
				// ListOfValue closed
				mReportOpened = false;
				// Adds the report configuration to the configuration list of the whole configuration
				if(null != mReportConfig)
				{
					XML2PDFConfiguration.GetInstance().AddReportConfiguration(mReportConfig.Id, mReportConfig);
					mReportConfig = null;
				}
			}
			else if ( cNode.NodeName.Equals(REPORT_USER_PROFILES) )
			{
				// ListOfValue closed
				mReportUserProfilesOpened = false;
			}
			else if ( cNode.NodeName.Equals(USER_PROFILE) )
			{
				// ListOfValue closed
				mUserProfileOpened = false;
				// Adds the report configuration to the configuration list of the whole configuration
				if(null != mReportConfig && null != mReportUserProfile)
				{
					mReportConfig.AddReportProfile(mReportUserProfile.ProfileId, mReportUserProfile);
					mReportUserProfile = null;
				}
			}
			else if ( cNode.NodeName.Equals(ATTRIBUTES) )
			{
				// Attributes closed
				mAttributesOpened = false;
			}
			else
			{
				// Found something wrong!
			}
		}

		/// <summary>
		/// Acquires the application's root path, used to find the XML template and layout files needed to 
		/// generate PDF documents.
		/// </summary>
		/// <returns>A string holding the application's installation root.</returns>
		private string GetApplicationRoot()
		{
			string appRoot = Application.StartupPath;

			// if running under Visual Studio, application path is extended by ...\bin\debug (or release)
			// because the executable is stored there.
			int pos = appRoot.IndexOf("bin");

			// if running in production, the executable is stored in the base directory.
			if(pos >= 0) 
			{
				appRoot = appRoot.Substring(0, pos - 1) + "\\";
			}
			return appRoot;
		}
		
		#endregion // End of Methods

	}
}

using System;
using System.Collections;

namespace de.pta.Component.PdfGenerator.Internal.Configuration
{
	/// <summary>
	/// XML2PDFConfiguration holds the configuration attributes used by the PdfGenerator component like 
	/// path settings for the used XML template and XSLT layout files and for the generated PDF file.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> M. Mehlem, PTA GmbH
	/// <b>Date:</b> Aug/28/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class XML2PDFConfiguration
	{
		#region Members
		
		/// <summary>
		/// The only instance of this class.
		/// </summary>
		private static XML2PDFConfiguration mInstance = null;

		/// <summary>
		/// Specifies whether this configuration is already established.
		/// </summary>
		private static bool mAlreadyConfigured = false;

		
		/// <summary>
		/// The base path of the source files and the generated PDF file.
		/// </summary>
		private string mBasePath;

		/// <summary>
		/// The list of report configurations.
		/// </summary>
		private Hashtable mReportConfigurations;

		
		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Constructor is declared private to match the singleton pattern.
		/// </summary>
		private XML2PDFConfiguration()
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
			mBasePath = string.Empty;
			mReportConfigurations = new Hashtable();
		}	

		/// <summary>
		/// Returns the only instance of the class.
		/// </summary>
		/// <returns>The only XML2PDFConfiguration instance that exists.</returns>
		public static XML2PDFConfiguration GetInstance() 
		{
			if(null == mInstance) 
			{
				mInstance = new XML2PDFConfiguration();
			}
			return mInstance;
		}

		#endregion //End of Initialization

		#region Accessors 

		
		/// <summary>
		/// Gets the list of report configurations.
		/// </summary>
		public Hashtable ReportConfigurations 
		{
			get { return mReportConfigurations; }
		}
			
		/// <summary>
		/// Gets or sets the base path of the source files and the generated PDF file.
		/// </summary>
		public string BasePath 
		{
			get { return mBasePath; }
			set { mBasePath = value; }
		}

		/// <summary>
		/// Gets or sets the boolean that indicates wehther this configuration is already established
		/// </summary>
		public bool AlreadyConfigured 
		{
			get { return mAlreadyConfigured; }
			set { mAlreadyConfigured = value; }
		}

		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// Adds a report configuration to the list of configurations.
		/// </summary>
		/// <param name="pReportId">The id of the report this configuration belongs to.</param>
		/// <param name="pReportConfiguration">The configuration of a report specified by id.</param>
		public void AddReportConfiguration(string pReportId, ReportConfiguration pReportConfiguration) 
		{
			this.ReportConfigurations.Add(pReportId, pReportConfiguration);
		}
		
		#endregion // End of Methods

	}
}

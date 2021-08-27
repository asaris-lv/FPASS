using System;
using System.Collections;

namespace de.pta.Component.PdfGenerator.Internal.Configuration
{
	/// <summary>
	/// ReportConfiguration holds the configuration attributes of one specific report to be generated 
	/// by the PdfGenerator component like path settings for the used XML template and XSLT layout 
	/// files and for the generated PDF file.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> M. Mehlem, PTA GmbH
	/// <b>Date:</b> Aug/28/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class ReportConfiguration
	{
		#region Members
		
		/// <summary>
		/// The id of the report this configuration belongs to.
		/// </summary>
		private string mId;

		/// <summary>
		/// The boolean specifying whether a layout file must be specified in the configuration.xml file
		/// for this ReportConfiguration.
		/// </summary>
		private bool mLayoutRequired;

		/// <summary>
		/// The XML template file name.
		/// </summary>
		private string mDefaultTemplateFile;

		/// <summary>
		/// The XSLT layout file name.
		/// </summary>
		private string mDefaultLayoutFile;

		/// <summary>
		/// The PDF file name.
		/// </summary>
		private string mDefaultPDFFile;

		/// <summary>
		/// The list of report user profiles.
		/// </summary>
		private Hashtable mReportUserProfiles;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Default Constructor.
		/// </summary>
		public ReportConfiguration()
		{
			//
			// TODO: Add constructor logic here
			//
			initialize();
		}

		#endregion //End of Constructors

		#region Initialization

		/// <summary>
		/// Initializes the members.
		/// </summary>
		private void initialize()
		{
			mReportUserProfiles = new Hashtable();
		}
		#endregion //End of Initialization

		#region Accessors 

		/// <summary>
		/// Gets or sets the id of the report this configuration belongs to.
		/// </summary>
		public string Id 
		{
			get { return mId; }
			set { mId = value; }
		}

		/// <summary>
		/// Gets or sets the boolean specifying whether a layout file must be specified 
		/// in the configuration.xml file for this ReportConfiguration.
		/// </summary>
		public bool LayoutRequired 
		{
			get { return mLayoutRequired; }
			set { mLayoutRequired = value; }
		}

		/// <summary>
		/// Gets or sets the XML template file name used as default.
		/// </summary>
		public string DefaultTemplateFile 
		{
			get { return mDefaultTemplateFile; }
			set { mDefaultTemplateFile = value; }
		}

		/// <summary>
		/// Gets or sets the XSLT layout file name used as default.
		/// </summary>
		public string DefaultLayoutFile 
		{
			get { return mDefaultLayoutFile; }
			set { mDefaultLayoutFile = value; }
		}

		/// <summary>
		/// Gets or sets the PDF file name used as default.
		/// </summary>
		public string DefaultPDFFile 
		{
			get { return mDefaultPDFFile; }
			set { mDefaultPDFFile = value; }
		}

		/// <summary>
		/// Gets the list of report user profiles.
		/// </summary>
		public Hashtable ReportUserProfiles 
		{
			get { return mReportUserProfiles; }
		}

		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// Adds a report user profile to the list of profiles.
		/// </summary>
		/// <param name="pProfileId">The id of the report user profile.</param>
		/// <param name="pReportUserProfile">The user profile specified by id.</param>
		public void AddReportProfile(string pProfileId, ReportUserProfile pReportUserProfile) 
		{
			this.ReportUserProfiles.Add(pProfileId, pReportUserProfile);
		}

		#endregion // End of Methods
	}
}

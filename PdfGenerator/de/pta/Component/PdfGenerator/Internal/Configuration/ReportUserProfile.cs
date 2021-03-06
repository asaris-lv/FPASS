using System;

namespace de.pta.Component.PdfGenerator.Internal.Configuration
{
	/// <summary>
	/// ReportUserProfile holds the configuration attributes of one specific report for a 
	/// specified user to be generated by the PdfGenerator component like path settings for 
	/// the used XML template and XSLT layout files and for the generated PDF file.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> M. Mehlem, PTA GmbH
	/// <b>Date:</b> Aug/29/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class ReportUserProfile
	{
		#region Members
		
		/// <summary>
		/// The id of the report this configuration belongs to.
		/// </summary>
		private string mProfileId;

		/// <summary>
		/// The XML template file name.
		/// </summary>
		private string mTemplateFile;

		/// <summary>
		/// The XSLT layout file name.
		/// </summary>
		private string mLayoutFile;

		/// <summary>
		/// The PDF file name.
		/// </summary>
		private string mPDFFile;

		
		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Default Constructor.
		/// </summary>
		public ReportUserProfile()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#endregion //End of Constructors

		#region Initialization
		#endregion //End of Initialization

		#region Accessors 

		/// <summary>
		/// Gets or sets the id of the report profile.
		/// </summary>
		public string ProfileId 
		{
			get { return mProfileId; }
			set { mProfileId = value; }
		}

		/// <summary>
		/// Gets or sets the XML template file name.
		/// </summary>
		public string TemplateFile 
		{
			get { return mTemplateFile; }
			set { mTemplateFile = value; }
		}

		/// <summary>
		/// Gets or sets the XSLT layout file name.
		/// </summary>
		public string LayoutFile 
		{
			get { return mLayoutFile; }
			set { mLayoutFile = value; }
		}

		/// <summary>
		/// Gets or sets the PDF file name.
		/// </summary>
		public string PDFFile 
		{
			get { return mPDFFile; }
			set { mPDFFile = value; }
		}

		#endregion //End of Accessors

		#region Methods 
		#endregion // End of Methods
	}
}

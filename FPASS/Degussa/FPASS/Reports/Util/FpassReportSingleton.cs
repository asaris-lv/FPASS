using System;
using System.Collections;

namespace Degussa.FPASS.Reports.Util
{
	/// <summary>
	/// holds parameters for exports and reports:
	/// - the index of the selected report
	/// - the name of the export file corresponding to the selected report
	/// - the ID of the selected coworker, wenn needed for a report or an export file
	/// - the where clause of the search, so that it can be reused for the reports
	/// </summary>
	/// <remarks>
	/// <para><b>History</b></para>
	/// <div class="tablediv">
	/// <table class="dtTABLE" cellspacing="0">
	///		<tr>
	///			<th width="20%">PTA GmbH</th>
	///			<th width="20%">12/01/2003</th>
	///			<th width="60%">Remarks</th>
	///		</tr>
	/// </table>
	/// </div>
	/// </remarks>
	public class FpassReportSingleton
	{
		#region Members

		private	static FpassReportSingleton mInstance = null;

		// report parameters
		private FpassReportParameters mReportParameters;
		private FpassExportParameters mExportParameters;
		private SortedList mSearchCriteria;
		
		private string mWhereSearchCriteria;
		private string mGroupByMain = "";

		#endregion Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		private FpassReportSingleton()
		{
			Initialize();
		}

		#endregion Constructors

		#region Initialization

		/// <summary>
		/// Initializes the members.
		/// </summary>
		private void Initialize()
		{
		}	

		#endregion Initialization

		#region Accessors

		/// <summary>
		/// Gets or sets current FpassReportParameters
		/// </summary>
		public FpassReportParameters ReportParameters
		{
			get
			{
				return mReportParameters;
			}
			set
			{
				mReportParameters = value;
			}
		}

		/// <summary>
		/// Gets or sets current FpassExportParameters
		/// </summary>
		public FpassExportParameters ExportParameters
		{
			get
			{
				return mExportParameters;
			}
			set
			{
				mExportParameters = value;
			}
		}

		/// <summary>
		/// Gets or sets list of search criteria
		/// </summary>
		public SortedList SearchCriteria
		{
			get
			{
				return mSearchCriteria;
			}
			set
			{
				mSearchCriteria = value;
			}
		}

		/// <summary>
		/// Gets or sets SQL Where clause containing serach criteria
		/// </summary>
		public string WhereSearchCriteria
		{
			get
			{
				return mWhereSearchCriteria;
			}
			set
			{
				mWhereSearchCriteria = value;
			}
		}

		/// <summary>
		/// Gets or sets SQL Group By clause 
		/// </summary>
		public string GroupByMain
		{
			get { return mGroupByMain; }
			set { mGroupByMain = value; }
		}

		#endregion Accessors

		#region Methods 

		/// <summary>
		/// Returns the one and only instance of this class.
		/// </summary>
		/// <returns>instance of FPASSFPASSControllSingleton</returns>
		public static FpassReportSingleton GetInstance() 
		{
			if ( null == mInstance ) 
			{
				mInstance = new FpassReportSingleton();
			}
			return mInstance;
		}

		#endregion Methods
	}
}
 
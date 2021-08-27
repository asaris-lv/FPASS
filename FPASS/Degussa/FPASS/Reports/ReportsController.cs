using System;
using System.Windows.Forms;
using de.pta.Component.Errorhandling;
using Degussa.FPASS.FPASSApplication;
using Degussa.FPASS.Gui;

namespace Degussa.FPASS.Reports
{
	/// <summary>
	/// A ReportsController is the controller of the MVC-triad FrmReport,
	/// ReportController and ReportModel.
	/// ReportsController extends from the FPASSBaseController.
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
	public class ReportsController : FPASSBaseController
	{
		#region Members

		/// <summary>
		/// used to hold the model of this triad. hold for convenience to avoid casting
		/// </summary>
		private	ReportsModel  mReportsModel;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public ReportsController()
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
			mDialogId = AllFPASSDialogs.REPORTS_DIALOG;
			mView = new FrmReport();
			mView.RegisterController(this);

			mModel = new ReportsModel();
			mModel.registerView(mView);

			mReportsModel = (ReportsModel)mModel;
		}	

		#endregion //End of Initialization

		#region Accessors 

		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// Shows help topic for current MVC
		/// </summary>
	    internal override void HandleEventShowHelp() 
		{
			Help.ShowHelp(mView, 
				Application.StartupPath + AllFPASSDialogs.FPASS_MASTER_HELPFILE, 
				HelpNavigator.Topic,
				AllFPASSDialogs.HELPTOPIC_REPORTS );
		}

		/// <summary>
		/// Handles loading controls in reports form for correct report
		/// </summary>
		internal void HandleReportValidationList()
		{
            mReportsModel.HandleReports();
		}

        /// <summary>
        /// Controls popup for excontractor search (attendance reports)
        /// Synchronises exco CheckedListBox in popup form with exco CheckedListBox in Reports form
        /// Note: Popup is independent of MVC triad and works with <see cref="ReportsController">this controller</see>
        /// </summary>
	    internal void HandlePopupSearchExco()
        {
            mView.Cursor = Cursors.WaitCursor;

            FrmPopupSearchExco frmExco = new FrmPopupSearchExco();
            frmExco.ReportsController = this;
            mReportsModel.SynchroniseExcoLists(((FrmReport)mView).ClbAttExContractor, frmExco.ClbAttExContractor);
            frmExco.ShowDialog();
            mView.Cursor = Cursors.Default;
        }

	    /// <summary>
        /// Synchronises CheckedListBox in Reports form with exco CheckedListBox in popup form
	    /// </summary>
        /// <param name="pFrmExco">popup form containing CheckedListBox</param>
        internal void HandleSynchroniseExcoLists(FrmPopupSearchExco pFrmExco)
        {
            try
            {
                mReportsModel.SynchroniseExcoLists(pFrmExco.ClbAttExContractor, ((FrmReport)mView).ClbAttExContractor);
            }
            catch (Exception ex)
            {
                ExceptionProcessor.GetInstance().Process(new UIErrorException(ex.Message, ex));
            }
        }


        /// <summary>
        /// Opens list of selected external contractors from given file
        /// and shows these in checklistbox
        /// </summary>
        /// <param name="pFrmExco">popup form containing CheckedListBox</param>
        internal void HandleOpenExcoList(FrmPopupSearchExco pFrmExco)
        {
            try
            {
                mReportsModel.LoadMyExcos(pFrmExco.ClbAttExContractor, pFrmExco.FileName);
            }
            catch (Exception ex)
            {
                ExceptionProcessor.GetInstance().Process(new UIErrorException(ex.Message, ex));
            }
        }

        /// <summary>
        /// Saves current list of selected external contractors to given file
        /// </summary>
        /// <param name="pFrmExco">popup form containing CheckedListBox</param>
        internal void HandleSaveExcoList(FrmPopupSearchExco pFrmExco)
        {
            try
            {
                mReportsModel.SaveMyExcos(pFrmExco.FileName);
            }
            catch (Exception ex)
            {
                ExceptionProcessor.GetInstance().Process(new UIErrorException(ex.Message, ex));
            }
        }

        /// <summary>
        /// Initialises the search
        /// </summary>
        internal void HandleSearch()
        {
            mReportsModel.HandleSearch();
        }

		/// <summary>
		/// Clears values in GUI controls and disables all
		/// </summary>
		internal void HandleClearMask()
		{
            mReportsModel.ClearFields();
		}

        /// <summary>
        /// Clears values in GUI specific to access fields
        /// </summary>
        internal void HandleAccessFields()
        {
            mReportsModel.ClearAccessFields();
        }

        internal void HandleAttendDates()
		{
            mReportsModel.CheckAttendDates();
        }

        internal void HandleDeliveryDates(string pFromDatTxt, string pUntilDatTxt)
        {
            mReportsModel.CheckDeliveryDates(pFromDatTxt, pUntilDatTxt);
        }


		/// <summary>
		/// Generates PDF report proper: 
		/// diverts to model.
		/// </summary>
		internal void HandleGenerateReport()
		{
            mReportsModel.GenerateReport();
		}

        /// <summary>
        /// Calls method in the model to get information required to generate CSV export
        /// </summary>
        internal void HandlePreGenerateExport()
        {
            mReportsModel.PreGenerateExport();
        }
	    
		/// <summary>
		/// Generates CSV export: calls method in the model
		/// </summary>
		internal void HandleGenerateExport()
		{
			mReportsModel.GenerateExport();
		}

		#endregion
	}
}

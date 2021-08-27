using System;
using System.Windows.Forms;
using System.Data;

using Degussa.FPASS.Gui;
using Degussa.FPASS.FPASSApplication;
using Degussa.FPASS.Util.Exceptions;
using Degussa.FPASS.Util.ErrorHandling;
using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.Db.DataSets;

using de.pta.Component.Errorhandling;

namespace Degussa.FPASS.Gui.Dialog.Pass
{
	/// <summary>
	/// An ExtendedSearchController is the controller of the MVC-triad 
	/// ExtendedSearchModel, ExtendedSearchController and FrmExtendedSearch.
	/// ExtendedSearchController extends from the FPASSBaseController.
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
	public class ExtendedSearchController : FPASSBaseController
	{
		#region Members

		/// <summary>
		/// used to hold the model of this triad. hold for convenience to avoid casting
		/// </summary>
		private	ExtendedSearchModel  mExtendedSearchModel;

        /// <summary>
        /// Flags whether or not the extended search returned results
        /// </summary>
        private bool mResultsFound;
		
		#endregion 

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public ExtendedSearchController()
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

			mDialogId = AllFPASSDialogs.SEARCH_COWORKER_DIALOG;
			mView = new FrmExtendedSearch();
			mView.RegisterController(this);

			mModel = new ExtendedSearchModel();
			mModel.registerView(mView);
			
			mExtendedSearchModel = (ExtendedSearchModel)mModel;
		}	

		#endregion //End of Initialization

		#region Accessors 

        /// <summary>
        /// Flags whether or not the extended search returned results
        /// </summary>
        public bool ResultsFound
        {
            get { return mResultsFound; }
        }

		#endregion 


		#region Methods 

		/// <summary>
		/// Show help topic for extended search
		/// </summary>
		internal override void HandleEventShowHelp() 
		{
			Help.ShowHelp( this.mView, 
				Application.StartupPath + AllFPASSDialogs.FPASS_MASTER_HELPFILE, 
				HelpNavigator.Topic,
				AllFPASSDialogs.HELPTOPIC_COWORKER_SEARCH );
		}

		/// <summary>
		/// Gets CoWorker results that meet the search criteria from extended search
		/// and passes the results to FrmCoworkerSummary
		/// If no reults (data table is null) throw warning and don't return to Summary. 
		/// Cannot search for strings with ' in as this is part of SQL string delimiter: catch ORA exception
		/// </summary>
        internal void HandleSearchCoworker()
        {
            mResultsFound = false;
            mView.Cursor = Cursors.WaitCursor;

            try
            {
                mExtendedSearchModel.CheckSearchCriteria();
                mExtendedSearchModel.GetCoWorkerSummary();

                ((SummaryController)mParent).SetResultsExtendedSearch(mExtendedSearchModel.ResultsTable, mExtendedSearchModel.ResultsArray);
                mResultsFound = (mExtendedSearchModel.ResultsTable != null);
                mExtendedSearchModel.ShowMessageInStatusBar(MessageSingleton.GetInstance().GetMessage(MessageSingleton.NO_RESULTS));
            }
            catch (System.Data.OracleClient.OracleException oraex)
            {
                // Error from SQL ' delimiter
                if (oraex.Code == 01756)
                {
                    throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.CANNOT_SEARCH_CHAR));
                }
                else
                {
                    throw new UIFatalException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR) + oraex.Message);
                }
            }

        }
		
		#endregion Methods

	}
}

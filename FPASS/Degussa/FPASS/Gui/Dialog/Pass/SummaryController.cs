using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

using de.pta.Component.Errorhandling;

using Degussa.FPASS.FPASSApplication;
using Degussa.FPASS.Gui;
using Degussa.FPASS.Reports.Util;
using Degussa.FPASS.Util;
using Degussa.FPASS.Util.Exceptions;
using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.Util.UserManagement;
using Degussa.FPASS.Util.Enums;


namespace Degussa.FPASS.Gui.Dialog.Pass
{
	/// <summary>
	/// A SummaryController is the controller of the MVC-triad SummaryModel,
	/// SummaryController and FrmSummaryCoWorker.
	/// SummaryController extends from the FPASSBaseController.
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
	public class SummaryController : FPASSBaseController
	{
		#region Members

		/// <summary>
		/// What type of search is being carried out? This depends on which form was opened from summary
		/// When summary comes back into focus, current data are refreshed by re-executing search
		/// </summary>
		private int mSearchState;

		internal const int NORMAL_SEARCH_PERFORMED = 1;
		internal const int EXTENDED_SEARCH_PERFORMED = 2;
		internal const int IDCARD_ZKS_SEARCH_PERFORMED = 3;
        internal const int IDCARD_USB_SEARCH_PERFORMED = 4;

		/// <summary>
		/// used to hold the model of this triad. hold for convenience to avoid casting
		/// </summary>
		private	SummaryModel mSummaryModel;

        /// <summary>
        /// used to hold the View of this triad. hold for convenience to avoid casting
        /// </summary>
        private FrmSummaryCoWorker mSummaryView;
	
		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public SummaryController()
		{
			initialize();
		}

		#endregion //End of Constructors

		#region Initialization

		/// <summary>
		/// Initializes the members, including view and model
		/// </summary>
		private void initialize()
		{
			mDialogId = AllFPASSDialogs.SUMMARY_COWORKER_DIALOG;
			mView = new FrmSummaryCoWorker();
			mView.RegisterController(this);

			mModel = new SummaryModel();
			mModel.registerView(mView);

			mSummaryModel = (SummaryModel)mModel;
            mSummaryView = (FrmSummaryCoWorker)mView;
			mSearchState = 0;			
		}	

		#endregion //End of Initialization

		#region Accessors 

		/// <summary>
		/// Sets or returns the search type: normal search, extended search, id card search etc.
		/// </summary>
		public int SearchState 
		{
			get { return mSearchState; }
			set { mSearchState = value; }
		}

        
		#endregion 

		#region Methods 

		/// <summary>
		/// Show current help topic
		/// </summary>
		internal override void HandleEventShowHelp() 
		{
			Help.ShowHelp(mView, 
				Application.StartupPath + AllFPASSDialogs.FPASS_MASTER_HELPFILE, 
				HelpNavigator.Topic,
				AllFPASSDialogs.HELPTOPIC_SUMMARY_COWORKER );
		}
		

		/// <summary>
		/// As Summary is always open (never reloaded), need a manual way of refreshing
		/// comboboxes. This is a reaction to pressing F5 in Summary.
		/// </summary>
		internal void HandleEventRefreshSummaryLists()
		{
			FPASSControllSingleton.GetInstance().ReInitializeListOfValues();
			RefreshMyLists();
		}

		/// <summary>
		/// Open next dialog to edit coworker with current ID
		/// </summary>
		/// <param name="pCurrentFFMAId">PK ID of current cwr</param>
		internal void HandleEventOpenProcessDialog(decimal pCurrentFFMAId) 
		{			
			try
			{
				try
				{
					CoWorkerController coWorkerController = (CoWorkerController)FPASSControllSingleton.GetInstance().GetDialog(AllFPASSDialogs.COWORKER_PROCESS_DIALOG);
					coWorkerController.SetCurrentFFMA(pCurrentFFMAId);
					FPASSControllSingleton.GetInstance().ShowModalDialog(this, coWorkerController);
				}
				catch (ActionCancelledException ace )
				{
					throw new UIWarningException(ace.Message);
					
				}
			}
			catch ( UIWarningException uwe )
			{
				ExceptionProcessor.GetInstance().Process(uwe);
			}
		}


		/// <summary>
		/// Open form for extended search
		/// Get archived excos and dependent exco stuff not belonging to component
		/// </summary>
		/// <exception cref="de.pta.Component.Errorhandling.UIWarningException">should no longer appear: should be caught in extended search</exception>
		internal void HandleEventOpenExtendedSearch() 
		{
			try 
			{
				RefreshMyLists();
				FPASSControllSingleton.GetInstance().ReInitializeListOfValues();
				FPASSControllSingleton.GetInstance().ShowModalDialog(this, AllFPASSDialogs.SEARCH_COWORKER_DIALOG);
			} 
			catch (UIWarningException uwe) 
			{
				ExceptionProcessor.GetInstance().Process(uwe);
			}
		}


		/// <summary>
		/// If current mandator has access to ZKS, then get ID card number from ZKS terminal
		/// Set search state, but do not carry out the search, only button "Suchen" does that.
		/// </summary>
		internal void HandleEventSearchByIdCard(string pIdCardType) 
		{
			mSummaryModel.ClearStatusBar();
			try 
			{
                if (UserManagementControl.getInstance().CurrentBOUserMndt.HasZKS)
                {
                    mSummaryView.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                    mSummaryModel.IDCardType = pIdCardType;

                    mSummaryModel.SearchByIdCardZKS();
                   // searchState = IDCARD_ZKS_SEARCH_PERFORMED; // normal search as of FPASS V5
                    mSearchState = NORMAL_SEARCH_PERFORMED; 
                }
			}
			catch (UIWarningException uwe)
			{
                mSummaryView.TxtSearchIDCard.Text = "";
				ExceptionProcessor.GetInstance().Process(uwe);
			}
            mSummaryView.Cursor = System.Windows.Forms.Cursors.Default;
		}


		/// <summary>
		/// Control logic for button "Suchen": coworker search
		/// </summary>
		internal void HandleEventSearch()  
		{		
			mSummaryModel.ClearStatusBar();
			try 
			{
                mSummaryView.Cursor = System.Windows.Forms.Cursors.WaitCursor;				
				mSummaryModel.GetCoWorkerSummary();
				mSearchState = NORMAL_SEARCH_PERFORMED;
			} 
			catch ( UIWarningException uwe ) 
			{              
                mSummaryView.BtnEdit.Enabled = false;
				mSummaryView.BtnPass.Enabled = false;
				ExceptionProcessor.GetInstance().Process(uwe);
			}
            mSummaryView.Cursor = System.Windows.Forms.Cursors.Default;
		}

		
		/// <summary>
        /// Shows results returned from Extended Search in the summary form
		/// Use ArrayList for working with BOs, DataTable to display (legacy..)
		/// Allows Paint event in datagrid to fire to allow CoWorker ID to be picked up
		/// </summary>
		/// <param name="pTabResults">Datatable of coworker results</param>
		/// <param name="pArlResults">Same results as arrylist</param>
		internal void SetResultsExtendedSearch(DataTable pTabResults, ArrayList pArlResults) 
		{
            mSummaryView.ClearFields();

			if (null != pTabResults)
			{
				mSearchState = EXTENDED_SEARCH_PERFORMED;
				mSummaryModel.CoWorkerList = pArlResults;
				mSummaryModel.SetResultsExtendedSearch(pTabResults);

				// To allow CoWorker ID to be picked up by Paint event in grid
                mSummaryView.GridIsLoading = false;
			}
            else
            {
                mSummaryModel.CoWorkerList = null;
                mSummaryModel.SetResultsExtendedSearch(pTabResults);
                mSummaryView.CurrentRowIndex = -1;
                mSummaryModel.ShowMessageInStatusBar(MessageSingleton.GetInstance().GetMessage(MessageSingleton.NO_RESULTS));
            }

            mSummaryView.BtnEdit.Enabled = (null != pTabResults);
            mSummaryView.BtnPass.Enabled = (null != pTabResults);
		}


		/// <summary>
		/// Generates pass document (Passierschein) for current coworker
		/// </summary>
		/// <param name="prmCoWorkerId">PK ID of current cwr</param>
		internal void HandleGeneratePass(decimal prmCoWorkerId)
		{
			try
			{		
				mSummaryModel.GenerateCWRReport(prmCoWorkerId, ReportNames.CWR_PASS);
			}
			catch (UIWarningException uwe)
			{
				ExceptionProcessor.GetInstance().Process(uwe);
			}
		}


		/// <summary>
		/// Re-Executes last coworker seach. Method is called when summary form gets focus 
        /// after other dialogs have been closed.
		/// 3 possible search states: 
		/// 1. Normal search in summary coworker form
		/// 2. If "Extended search" then use parameters that were selected from there.
		/// Here it is important NOT to close the form: MVC triad still exists in memory although form hidden.
		/// 3. Search for ID card in ZKS (button "Ausweisnr suchen").
		/// </summary>
		internal void StartAutomaticSearch() 
		{          
			if (mSearchState == NORMAL_SEARCH_PERFORMED) 
			{
				mSummaryModel.ClearStatusBar();
				try 
				{
					mSummaryView.Cursor = Cursors.WaitCursor;	
					mSummaryView.GridIsLoading = true;
					
					mSummaryModel.GetCoWorkerSummary();
					JumpToCurrentCoWorker();					
					mSummaryView.GridIsLoading = false;
				} 
				catch (UIWarningException ) 
				{				
					mSummaryView.CurrentRowIndex    = -1;
					mSummaryView.BtnEdit.Enabled = false;
					mSummaryView.BtnPass.Enabled    = false;					
				}
				finally
				{
					mSummaryView.Cursor = Cursors.Default;
				}
			} 
			else if (mSearchState == EXTENDED_SEARCH_PERFORMED) 
			{
				mSummaryModel.ClearStatusBar();
				try 
				{
					// Do extended search (form may be hidden but still exists at runtime)	
					mSummaryView.GridIsLoading = true;
					ExtendedSearchController exSearchController =  (ExtendedSearchController)FPASSControllSingleton.GetInstance().GetDialog(AllFPASSDialogs.SEARCH_COWORKER_DIALOG);

					exSearchController.HandleSearchCoworker();
					JumpToCurrentCoWorker();

					mSummaryView.GridIsLoading = false;
                    mSummaryView.BtnEdit.Enabled = true;
                    mSummaryView.BtnPass.Enabled = true;
				} 
				catch ( UIWarningException ) 
				{				
					mSummaryView.CurrentRowIndex   = -1;
                    mSummaryView.BtnEdit.Enabled = false;
                    mSummaryView.BtnPass.Enabled = false;					
				}				
			}
			else if (mSearchState == IDCARD_ZKS_SEARCH_PERFORMED)
			{
				HandleEventSearchByIdCard(IDCardTypes.Hitag2);
			}
            else if (mSearchState == IDCARD_USB_SEARCH_PERFORMED)
            {
                HandleEventSearchByIdCard(IDCardTypes.Hitag2);
            }
		}


		/// <summary>
		/// Jumps to row in datagrid containing coworker currently being edited
		/// </summary>
		private void JumpToCurrentCoWorker()
		{
			// if current row of coworker has disappeared (i.e. query returned fewer rows
			// then row index, set row index to 0
            if (mSummaryModel.CoWorkerList == null)
            {
                mSummaryView.CurrentRowIndex = -1;
                return;
            }

			int lastRowIndex = mSummaryModel.CoWorkerList.Count-1;

            if (mSummaryView.CurrentRowIndex > lastRowIndex)
			{
				mSummaryView.CurrentRowIndex = lastRowIndex;
			}

			// Jump to previously selected CWR if there is one ( -1 means none)
			if ( mSummaryView.CurrentRowIndex > -1 )
			{
                mSummaryView.JumpToCurrentCoWorker();
			}
		}

		#endregion

	}
}

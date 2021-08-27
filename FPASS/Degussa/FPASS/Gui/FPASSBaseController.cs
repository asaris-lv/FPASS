using System;
using System.Windows.Forms;

using Degussa.FPASS.Gui;
using Degussa.FPASS.Gui.Dialog.Pass;
using Degussa.FPASS.Util.ErrorHandling;
using Degussa.FPASS.Util.ListOfValues;
using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.FPASSApplication;
using Degussa.FPASS.Reports;

using de.pta.Component.Errorhandling;
using Degussa.FPASS.Util.SmartAct;

namespace Degussa.FPASS.Gui
{
	/// <summary>
	/// FPASSBaseController is the controller of the MVC-triad FPASSBaseView,
	/// FPASSBaseModel and FPASSBaseController.
	/// FPASSBaseController extends from the AbstractController.
	/// </summary>
	/// <remarks>
	/// <para><b>History</b></para>
	/// <div class="tablediv">
	/// <table class="dtTABLE" cellspacing="0">
	///		<tr>
	///			<th width="20%">Author</th>
	///			<th width="20%">Date</th>
	///			<th width="60%">Remarks</th>
	///		</tr>
	///		<tr>
	///			<td width="20%">Author</td>
	///			<td width="20%">month/day/year</td>
	///			<td width="60%">Comment</td>
	///		</tr>
	/// </table>
	/// </div>
	/// </remarks>
	public class FPASSBaseController : AbstractController
	{
		#region Members

        /// <summary>
        /// Instance of IdCardPoller used to control background process 
        /// to monitor CWR with IdCards from SmartAct
        /// </summary>
        protected IdCardPoller mIdCardPoller;

        protected string TitleMessage = "FPASS";

		#endregion 

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public FPASSBaseController()
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
		/// Closes FPASS application.
		/// </summary>
		internal void HandleEventCloseFPASS() 
		{
			FPASSControllSingleton.GetInstance().CloseApplication(this);
		}

		/// <summary>
		/// Shows reports dialog.
		/// </summary>
		internal void HandleEventOpenReportsDialog() 
		{
			mView.Cursor = System.Windows.Forms.Cursors.WaitCursor;
			FPASSControllSingleton.GetInstance().ShowModalDialog(this, AllFPASSDialogs.REPORTS_DIALOG);
			mView.Cursor = System.Windows.Forms.Cursors.Default;
		}

		/// <summary>
		/// Shows delete coworker summary dialog.
		/// Get archived excos and dependent exco stuff not belonging to component
		/// </summary>
		internal void HandleEventOpenDeleteDialog() 
		{
			RefreshMyLists();
			FPASSControllSingleton.GetInstance().ReInitializeListOfValues();
			FPASSControllSingleton.GetInstance().ShowModalDialog(this, AllFPASSDialogs.COWORKER_DELETE_DIALOG);
		}

		/// <summary>
		/// Shows administration dialog.
		/// 29.03.04: re-read listboxes from DB
		/// </summary>
		internal void HandleEventOpenAdministrationDialog() 
		{			
			FPASSControllSingleton.GetInstance().ReInitializeListOfValues();
			FPASSControllSingleton.GetInstance().ShowModalDialog(this, AllFPASSDialogs.ADMINISTRATION_DIALOG);
		}

		/// <summary>
		/// Shows history dialog.
		/// </summary>
		internal void HandleEventOpenHistoryDialog() 
		{
			FPASSControllSingleton.GetInstance().ShowModalDialog(this, AllFPASSDialogs.HISTORY_DIALOG);
		}

		/// <summary>
		/// Shows vehicle registration dialog.
		/// </summary>
		internal void HandleEventOpenVehicleDialog() 
		{
			FPASSControllSingleton.GetInstance().ShowModalDialog(this, AllFPASSDialogs.VEHICLE_DIALOG);
		}

		/// <summary>
		/// Shows standard help. Subclasses can override this method to show specific help.
		/// </summary>
		internal virtual void HandleEventShowHelp() 
		{
			Help.ShowHelp(this.mView, Application.StartupPath + AllFPASSDialogs.FPASS_MASTER_HELPFILE);
		}

		/// <summary>
		/// Shows help index.
		/// </summary>
		internal void HandleEventShowHelpIndex() 
		{
			Help.ShowHelpIndex(this.mView, Application.StartupPath + AllFPASSDialogs.FPASS_MASTER_HELPFILE);			
		}

		/// <summary>
		/// Shows infobox.
		/// </summary>
		internal void HandleEventShowInfobox()
		{
            FPASSAboutBox frmInfo = new FPASSAboutBox();
			frmInfo.ShowDialog();
		}


        /// <summary>
        /// Shows FPASSMessageBox
        /// </summary>
        internal void HandleEventShowFPASSMessageBox(string pMessage)
        {
            FPASSMessageBox frmMessageBox = new FPASSMessageBox(pMessage);
            frmMessageBox.ShowDialog();
        }

		/// <summary>
		/// Shows user dialog.
		/// </summary>
		internal void HandleEventOpenUserDialog() 
		{
			FPASSControllSingleton.GetInstance().ShowModalDialog(this, AllFPASSDialogs.USER_DIALOG);
		}

		/// <summary>
		/// Shows archive dialog.
		/// Refresh comboboxes before opening form
		/// Get archived excos and dependent exco stuff not belonging to component
		/// </summary>
		internal void HandleEventOpenArchiveDialog() 
		{
			FPASSLovsSingleton.GetInstance().LoadArchiveContractors();
			RefreshMyLists();
			FPASSControllSingleton.GetInstance().ReInitializeListOfValues();
			FPASSControllSingleton.GetInstance().ShowModalDialog(this, AllFPASSDialogs.ARCHIVE_DIALOG);
		}

		/// <summary>
		/// Shows dialog dynamic data.
		/// </summary>
		internal void HandleEventOpenDynamicDataDialog() 
		{
			FPASSControllSingleton.GetInstance().ReInitializeListOfValues();
			FPASSControllSingleton.GetInstance().ShowModalDialog(this, AllFPASSDialogs.DYNAMIC_DATA_DIALOG);
		}


		/// <summary>
		/// Shows search external contractor dialog.
		/// </summary>
		internal void HandleEventOpenSearchExternalContractorDialog() 
		{           
			FPASSControllSingleton.GetInstance().ReInitializeListOfValues();
			FPASSControllSingleton.GetInstance().ShowModalDialog(this, AllFPASSDialogs.SEARCH_EXTERNAL_CONTRACTOR_DIALOG);
		}


        /// <summary>
        /// Shows SearchID card reader dialog.
        /// </summary>
        internal void HandleEventOpenSearchIDCardReaderDialog()
        {
            FPASSControllSingleton.GetInstance().ReInitializeListOfValues();
            FPASSControllSingleton.GetInstance().ShowModalDialog(this, AllFPASSDialogs.SEARCH_IDCARD_READER_DIALOG);
        }


		/// <summary>
        /// This is the original menu item "FPASS > ZKS", unchanged in FPASS V5.
        /// Transfers all CWR with status N to ZKS.
		/// </summary>
		internal void HandleEventExportToZKS() 
		{
			try 
			{
				((FPASSBaseModel)mModel).ExportAllToZKS();  
			} 
			catch ( UIWarningException uwe ) 
			{
				ExceptionProcessor.GetInstance().Process(uwe);
			}
		}

        /// <summary>
        /// Shows new dialog PoupIdCard (FFMA mit neuem Ausweis) FPASS V5, independently of background process
        /// </summary>
        internal void HandleEventSmartActPopIdCard()
        {
            try
            {
                GetIdCardPoller();

                if (mIdCardPoller.IsTaskRunning)
                {
                    // Message if it's already running as background process
                    MessageBox.Show(MessageSingleton.GetInstance().GetMessage(MessageSingleton.SMARTACT_NOTIFY_RUNNING), TitleMessage, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                // Use condition in case there is nothing to show
                FPASSControllSingleton.GetInstance().ShowModalDialogCondition(this, AllFPASSDialogs.POPUP_COWORKER_IDCARD_DIALOG);


                // If there is nothing to show (i.e. no coworkers from SmartAct) then show message
                AbstractController controller = FPASSControllSingleton.GetInstance().GetDialog(AllFPASSDialogs.POPUP_COWORKER_IDCARD_DIALOG);
                if (!controller.ConditionIsMet())
                {
                    MessageBox.Show(MessageSingleton.GetInstance().GetMessage(MessageSingleton.SMARTACT_NO_DATA), TitleMessage, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
         
            }
            catch (UIWarningException uwe)
            {
                ExceptionProcessor.GetInstance().Process(uwe);
            }
        }

        /// <summary>
        /// This starts the background process to make dialog PopupIdCard (FFMA mit neuem Ausweis) 
        /// automatically pop up. FPASS V5.
        /// </summary>
        internal void HandleEventSmartActStart()
        {
            GetIdCardPoller();

            if (mIdCardPoller.IsTaskRunning)
            {
                // Message if it's already running
                MessageBox.Show(MessageSingleton.GetInstance().GetMessage(MessageSingleton.SMARTACT_NOTIFY_RUNNING), TitleMessage, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // Mega message box asks if user wants to continue
            if (DialogResult.Yes == MessageBox.Show(MessageSingleton.GetInstance().GetMessage(MessageSingleton.SMARTACT_NOTIFY_START),
                                    TitleMessage,
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question,
                                    MessageBoxDefaultButton.Button1,
                                    0,
                                    Application.StartupPath + AllFPASSDialogs.FPASS_MASTER_HELPFILE,
                                    HelpNavigator.Topic,
                                    AllFPASSDialogs.HELPTOPIC_SMARTACT_CWR))
            {
                try
                {
                    // Starts background task
                    mIdCardPoller.RunBackgroundTask();

                    ((FPASSBaseView)mView).StbBase.Panels[1].Text = "autom. Benachrichtigung läuft...";
                    ((FPASSBaseView)mView).MnuSmartActStart.Checked = true;
                    ((FPASSBaseView)mView).MnuIdCardPopup.Enabled = false;
                    ((FPASSBaseView)mView).MnuExportToZKS.Enabled = false;
                }
                catch (UIWarningException uwe)
                {
                    ExceptionProcessor.GetInstance().Process(uwe);
                }
            }
        }

        /// <summary>
        /// This stops the background process to stop dialog PopupIdCard (FFMA mit neuem Ausweis) 
        /// automatically popping up. FPASS V5.
        /// </summary>
        internal void HandleEventSmartActStop()
        {
            try
            {
                GetIdCardPoller();
                if (mIdCardPoller.IsTaskRunning)
                {
                    mIdCardPoller.CancelBackgroundTask();
                    ((FPASSBaseView)mView).StbBase.Panels[1].Text = "";
                }
            }
            catch (UIWarningException uwe)
            {
                ExceptionProcessor.GetInstance().Process(uwe);
            }
            finally
            {
                ((FPASSBaseView)mView).StbBase.Panels[1].Text = String.Empty;
                ((FPASSBaseView)mView).MnuSmartActStart.Checked = false;
                ((FPASSBaseView)mView).MnuIdCardPopup.Enabled = true;
                ((FPASSBaseView)mView).MnuExportToZKS.Enabled = true;
            }
        }

		/// <summary>
		/// Closes this dialog and forces parent dialog to show.
		/// ?? There is a method DestroyDialog(this).. would force re-instantiation of forms and thus refreshing of data
		/// </summary>
		internal void HandleCloseDialog() 
		{
			if ( null != mParent ) {
				mParent.PreShow();
			}
			FPASSControllSingleton.GetInstance().CloseDialog(this);
		}


		/// <summary>
		/// Shows search external contractor dialog.
		/// 08.03.2004: CowWorker dialogue can also be opened from CoWorker Delete; 
		/// if this was the calling dialogue then execute automatic search here
		/// </summary>
		internal void HandleEventBackToSummaryDialog() 
		{
			if ( mParent.GetType().Equals(typeof(SummaryController)) )
			{ 
				((SummaryController) mParent).StartAutomaticSearch();
				//RefreshMyLists();
			}
			else if ( mParent.GetType().Equals(typeof(DeleteController)) )
			{
				((DeleteController) mParent).StartAutomaticSearch();
				//RefreshMyLists(); refreshed by F5
			}
			FPASSControllSingleton.GetInstance().CloseDialog(this);
			
		}

		/// <summary>
		/// Forces a re-read of list of values from DB and re-fills comboboxes
		/// </summary>
		internal virtual void RefreshMyLists()
		{
			FPASSLovsSingleton.GetInstance().LoadActiveCoordinators();
			FPASSLovsSingleton.GetInstance().LoadContractors();
			FPASSLovsSingleton.GetInstance().LoadInvalidContractors();
			FillLists();
		}
		
		/// <summary>
		/// Prompts user to save changes.
		/// </summary>
		/// <returns>reaction of the user</returns>
		protected DialogResult SaveChangesWished()
		{
			return MessageBox.Show( MessageSingleton.GetInstance().GetMessage(
				MessageSingleton.PROMPT_SAVE), 
				TitleMessage,
				MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
		}

        /// <summary>
        /// Gets instance of SmartAct IdCard poller (monitors CWR with IdCards from SmartAct)
        /// </summary>
        private void GetIdCardPoller()
        {
            // Put in method in case way of getting it changes 
            mIdCardPoller = FPASSControllSingleton.GetInstance().IdCardPoller;
        }

		
		#endregion // End of Methods


	}
}

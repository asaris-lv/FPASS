using System;
using System.Windows.Forms;

using de.pta.Component.Errorhandling;

using Degussa.FPASS.Bo.Pass;
using Degussa.FPASS.Gui;
using Degussa.FPASS.FPASSApplication;
using Degussa.FPASS.Reports.Util;
using Degussa.FPASS.Util;
using Degussa.FPASS.Util.ErrorHandling;
using Degussa.FPASS.Util.Exceptions;
using Degussa.FPASS.Util.ListOfValues;
using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.Util.UserManagement;
using Degussa.FPASS.Util.Enums;

namespace Degussa.FPASS.Gui.Dialog.Pass
{
	/// <summary>
	/// A CoWorkerController is the model of the MVC-triad CoWorkerModel,
	/// CoWorkerController and FrmCoWorker.
	/// CoWorkerController extends from the FPASSBaseController.
	/// It handles the interaction between the related GUI element (the view) 
	/// and the related business object (the model) of the dialog triad.
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
	public class CoWorkerController : FPASSBaseController
	{
		#region Members

		private CoWorkerModel mCoWorkerModel;
		
		private int mMode;

		#endregion

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public CoWorkerController()
		{
			initialize();
		}

		
		/// <summary>
		/// Standard constructor.
		/// </summary>
		public CoWorkerController(int pMode)
		{
			mMode = pMode;
			mDialogId = AllFPASSDialogs.CWR_DIALOG_MODE_ARCHIVE;

			mView = new FrmCoWorker();
			mView.RegisterController(this);

			mModel = new CoWorkerModel();
			mModel.registerView(mView);

			mCoWorkerModel = (CoWorkerModel)mModel;

			((CoWorkerModel)mModel).Mode = mMode;
		}

		#endregion //End of Constructors

		#region Initialization

		/// <summary>
		/// Initializes the members.
		/// </summary>
		private void initialize()
		{

			mDialogId = AllFPASSDialogs.COWORKER_PROCESS_DIALOG;
			mView = new FrmCoWorker();
			mView.RegisterController(this);

			mModel = new CoWorkerModel();
			mModel.registerView(mView);

			mCoWorkerModel = (CoWorkerModel)mModel;
            TitleMessage = "FPASS";
		}	

		#endregion //End of Initialization

		#region Accessors 

		/// <summary>
		/// simple accessor
		/// </summary>
		public int Mode 
		{
			get 
			{
				return mCoWorkerModel.Mode;
			} 
			set 
			{
				mCoWorkerModel.Mode = value;
			}
		}
		
        /// <summary>
        /// Returns current DialogStatus from Model instance.
        /// </summary>
        public int DialogStatus
        {
            get
            {
                return mCoWorkerModel.Status;
            }
        }


        /// <summary>
        /// Gets or sets current ID card reader type from Model instance.
        /// Either Hitag2 or Mifare
        /// </summary>
        public string IDCardReaderType
        {
            get
            {
                return mCoWorkerModel.IDCardReaderType;
            }
            set 
            { 
                mCoWorkerModel.IDCardReaderType = value; 
            }
        }       

		#endregion 

		#region Methods

		/// <summary>
		/// Sets the title of the form
		/// </summary>
		/// <param name="pText">title</param>
		internal void SetTitle(String pText) 
		{
			((FrmCoWorker)mView).LblMask.Text = pText;
		}

		/// <summary>
		/// Fills boxes with archive data
		/// </summary>
		internal void FillArchiveLists() 
		{
			((FrmCoWorker)mView).FillArchiveLists();
		}

		/// <summary>
		/// Shows dialog specific help
		/// </summary>
		internal override void HandleEventShowHelp() 
		{
			Help.ShowHelp( this.mView, 
				Application.StartupPath + AllFPASSDialogs.FPASS_MASTER_HELPFILE, 
				HelpNavigator.Topic,
				AllFPASSDialogs.HELPTOPIC_COWORKER_PROCESS );
		}


		/// <summary>
		/// Locks CoWorker dialog, user input is disabled
		/// </summary>
		internal void LockCoWorkerDialog() 
		{
			mCoWorkerModel.Locked = true;
			((FrmCoWorker)mView).EnableInput(false);
		}

		/// <summary>
		/// Simple getter. 
		/// </summary>
		/// <returns>true if dialog is locked, false otherwise</returns>
		internal bool GetLockState() 
		{
			return mCoWorkerModel.Locked;
		}


		/// <summary>
		/// Sets the flag to indicate wtehre the dialog allows user input 
		/// </summary>
		/// <param name="pState"></param>
		internal void SetLockState(bool pState) 
		{
			mCoWorkerModel.Locked = pState;
		}

		/// <summary>
		/// 26.03.04: Re-read values from database for dependent comboboxes
		/// concerning Excontractor, coordinator and subcontractor.
		/// This method is only one responsible for filling these, not done in form
		/// </summary>
		internal override void RefreshMyLists()
		{
			FPASSLovsSingleton.GetInstance().LoadAssignments();	
			FPASSLovsSingleton.GetInstance().LoadActiveCoordinators();
			((FrmCoWorker) mView).FillReCoordinator("0");
			FPASSLovsSingleton.GetInstance().LoadContractors();
			((FrmCoWorker) mView).FillReExternalContractor("0");
			((FrmCoWorker) mView).FillCoSubcontractor();
		}

		/// <summary>
		/// Sets the coworker which is displayed in the dialog.
		/// 26.03.04: Must first refresh comboboxes for excontractor and coordinator
		/// by re-reading from the database, in case another FPASS session has created new entries
		/// Added ReInitializeListOfValues: refresh all cbxs from LOV component to be sure
		/// </summary>
		/// <param name="pCoWorkerID">id (pk) of the coworker</param>
		internal void SetCurrentFFMA(decimal  pCoWorkerID) 
		{
			try 
			{
				mCoWorkerModel.SetCurrentFFMA(pCoWorkerID);
				if ( pCoWorkerID > 0 ) 
				{
					mCoWorkerModel.Status = AllFPASSDialogs.DIALOG_STATUS_UPDATE;
				} 
				else 
				{
					mCoWorkerModel.Status = AllFPASSDialogs.DIALOG_STATUS_NEW;
				}
				// Re-read vals for dependent comboboxes EXCO and Coord
				// and Sub-contractor
				// Unless dialogue is in Archive mode
				if ( !this.Mode.Equals(AllFPASSDialogs.CWR_DIALOG_MODE_ARCHIVE ) )
				{					
					RefreshMyLists();
				}
				mCoWorkerModel.InitializeData();
			}
			catch (UIWarningException uwe)
			{
				ExceptionProcessor.GetInstance().Process(uwe);
			}
		}

		/// <summary>
		/// Controls reading of an id card number from a ZKS terminal.
		/// </summary>
        internal void HandleReadIdCardZKS(string pIdCardType) 
		{
            try
            {
                if (UserManagementControl.getInstance().CurrentBOUserMndt.HasZKS)
                {
                    mView.Cursor = Cursors.WaitCursor;
                    mCoWorkerModel.ReadIdCardZKS(pIdCardType);
                }
            }
            catch (UIWarningException uwe)
            {
                ExceptionProcessor.GetInstance().Process(uwe);
            }
            mView.Cursor = Cursors.Default;
		}
	
		/// <summary>
		/// Controls deleting of the id card numbers in ZKS 
		/// </summary>
		internal void HandleDeleteIdCardZKS() 
		{	
			try 
			{
				if (UserManagementControl.getInstance().CurrentBOUserMndt.HasZKS) 
				{
                    mView.Cursor = Cursors.WaitCursor;
					mCoWorkerModel.DeleteIdCardZKS();
				}
			}
			catch (UIWarningException uwe)
			{
				ExceptionProcessor.GetInstance().Process(uwe);
			}
                mView.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Handles the event when site security revokes access (Werkschutz, Button "Zutritt sperren")
        /// </summary>
        internal void HandleRevokeAccessSiSe()
        {
            var mCwrView = ((FrmCoWorker)mView);
   
            try
            {
                if (mCwrView.TxtSiSeAccessAuthorizationComment.Text == null || mCwrView.TxtSiSeAccessAuthorizationComment.Text.Length == 0)
                {
                    throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.COWORKER_REVOKE_COMMENT));
                }

                if (UserManagementControl.getInstance().CurrentBOUserMndt.HasZKS)
                {
                    mView.Cursor = Cursors.WaitCursor;
                    mCoWorkerModel.DeleteIdCardZKS();
                }
            }
            catch (UIWarningException uwe)
            {
                ExceptionProcessor.GetInstance().Process(uwe);
            }
            mView.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Saves current user's id card readers (Terminals)
        /// separately from main Save Cwr
        /// </summary>
        internal void HandleSaveIdReader()
        {
            try
            {
                mCoWorkerModel.UpdateUserIdCardReader(true);
                mCoWorkerModel.ShowMessageInStatusBar(MessageSingleton.GetInstance().GetMessage(MessageSingleton.SAVE_SUCCESS));
            }
            catch (UIWarningException uwe)
            {
                ExceptionProcessor.GetInstance().Process(uwe);
            }
            catch (UIErrorException ure)
            {
                ExceptionProcessor.GetInstance().Process(ure);
            }
            mView.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Exports data to Smartact (produces one line in the CSV export file).
        /// Method throws an execption if CWR has unsaved changes or does not have a photcard ID card
        /// </summary>
        internal void HandleSmartActExport()
        {
            mCoWorkerModel.ClearStatusBar();

            if (((FrmCoWorker)mView).RbtCoIdPhotoSmActNo.Checked)
            {
                throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.SMARTACT_EXP_NOID_ERR));
            }

            else if (mCoWorkerModel.CheckChanges())
                throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.SMARTACT_EXP_SAVE_ERR));
            else
            {
                mView.Cursor = System.Windows.Forms.Cursors.WaitCursor;

                // But only if CWR has access long (SmartAct only for access Long)
                // and changes in fields which are relevant for SmartAct
                mCoWorkerModel.ExportToSmartAct(mCoWorkerModel.CoWorkerId.ToString(), SmartActActions.Update, true);
                mCoWorkerModel.ShouldExportSmartAct = false;

                mCoWorkerModel.ShowMessageInStatusBar(MessageSingleton.GetInstance().GetMessage(MessageSingleton.SMARTACT_EXP_FPASS_OK));
                mView.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }

		/// <summary>
		/// Delegates user input (selection of a plant in listbox) 
		/// in Coordinator tab to the responsible coworker model
		/// </summary>
		internal void HandleLikCoPlantChanged() 
		{
			mCoWorkerModel.ChangeInPlants();
		}

        /// <summary>
        /// Delegates user input (checkbox changed)
        /// in Coordinator tab to the responsible coworker model
        /// </summary>
        internal void HandleCoPlantsAll()
        {
            mCoWorkerModel.AssignAllPlants();
        }


		/// <summary>
		/// Saves the changed data of the displayed coworker to database.
		/// Checks if all data is correct. Shows warning if not and asks user to
		/// correct wrong data.
		/// <exception cref="de.pta.Component.Errorhandling.UIFatalException">
		/// Is thrown if a database error occurs 
		/// </summary>
		internal void HandleSaveCoWorker() 
		{
			mView.Cursor = Cursors.WaitCursor;
			mCoWorkerModel.ClearStatusBar();
			try 
			{
				mCoWorkerModel.Save();
				mCoWorkerModel.Status = AllFPASSDialogs.DIALOG_STATUS_UPDATE;
				mCoWorkerModel.ShowMessageInStatusBar(MessageSingleton.GetInstance().GetMessage(MessageSingleton.SAVE_SUCCESS ) );
				mView.Cursor = System.Windows.Forms.Cursors.Default;
			}
			catch (UIWarningException uwe)
			{

                base.HandleEventShowFPASSMessageBox(uwe.Message); 
               //     ExceptionProcessor.GetInstance().Process(uwe);
               
				mView.Cursor = Cursors.Default;
			}
			catch ( ActionCancelledException ) 
			{
				mView.Cursor = Cursors.Default;
			}
		}


		/// <summary>
		/// Clears all fields in the coworker dialog.
		/// </summary>
		internal void HandleClearFields() 

		{
			mCoWorkerModel.ClearStatusBar();
			mCoWorkerModel.ClearFields();
			mCoWorkerModel.Status = AllFPASSDialogs.DIALOG_STATUS_NEW;
			SetCurrentFFMA(0);
		}

		/// <summary>
		/// Generates pass document as PDF.
		/// Before printing PDF, data is validated and saved. 
		/// <exception cref="de.pta.Component.Errorhandling.UIFatalException"> is thrown if a database error occurs 
		/// </summary>
		internal void HandlePrintPass() 
		{
            try
            {
                mCoWorkerModel.ClearStatusBar();
                mView.Cursor = Cursors.WaitCursor;
                
                mCoWorkerModel.Save();
                mCoWorkerModel.Status = AllFPASSDialogs.DIALOG_STATUS_UPDATE;
                mCoWorkerModel.GenerateCWRReport(mCoWorkerModel.CoWorkerId, ReportNames.CWR_PASS);
                
                mCoWorkerModel.ShowMessageInStatusBar(MessageSingleton.GetInstance().GetMessage(MessageSingleton.PRINTPASS_SUCCESS));
            }
            catch (UIWarningException uwe)
            {
                ExceptionProcessor.GetInstance().Process(uwe);
            }
            finally
            {
                mView.Cursor = Cursors.Default;
            }
		}

		/// <summary>
		/// Controls leaving of coworker dialog. 
		/// Shows user prompt if there are unsaved changes.
		/// </summary>
		internal void HandleLeaveCoWorker() 
		{
			DialogResult dialogResult;
            FrmCoWorker viewCoWorker = (FrmCoWorker)mView;

			((CoWorkerModel) mModel).ClearStatusBar();
			try {
				if ( ((CoWorkerModel) mModel).CheckChanges() ) 
				{
					dialogResult = this.SaveChangesWished();
					if ( dialogResult == DialogResult.Yes ) 
					{
						try {
                            viewCoWorker.Cursor = System.Windows.Forms.Cursors.WaitCursor;
							((CoWorkerModel) mModel).Save();
                            viewCoWorker.Cursor = System.Windows.Forms.Cursors.Default;
						}
						catch ( ActionCancelledException ) 
						{
                            viewCoWorker.Cursor = System.Windows.Forms.Cursors.Default;
						}

                       // viewCoWorker.BtnDelPassNumber.Enabled = true;
						HandleEventBackToSummaryDialog();
					} 
					else if ( dialogResult == DialogResult.No ) 
					{
                       // viewCoWorker.BtnDelPassNumber.Enabled = true;
						HandleEventBackToSummaryDialog();
					} 
					else if ( dialogResult == DialogResult.Cancel ) 
					{
						// swallow it
					}

				} 
				else 
				{
                   // viewCoWorker.BtnDelPassNumber.Enabled = true; 
					HandleEventBackToSummaryDialog();
				}
				
			}
			catch (UIWarningException uwe)
			{
                viewCoWorker.Cursor = System.Windows.Forms.Cursors.Default;
				ExceptionProcessor.GetInstance().Process(uwe);
                viewCoWorker.Cursor = System.Windows.Forms.Cursors.Default;
			}
		}

		/// <summary>
		/// Delegates user input (selection of a precautionary medical briefing for update ) 
		/// to the responsible coworker model.
		/// </summary>
		/// <param name="pPrecMedID">id of the selected precautionary medical briefing
		/// </param>
		internal void HandlePrecMedUpdate(decimal pPrecMedID) 
		{
			((CoWorkerModel) mModel).ChangePrecMed(pPrecMedID);
		}


		/// <summary>
		/// Delegates user input (selection of a new precautionary medical briefing 
		/// to the responsible coworker model.
		/// </summary>
		internal void HandlePrecMedCreate()
		{
			try
			{
				((CoWorkerModel)mModel).CreatePrecMed();
			}
			catch (UIWarningException uwe)
			{
				ExceptionProcessor.GetInstance().Process(uwe);
			}
		}


		/// <summary>
		/// Delegates delete of currently selected precautionary medical briefing
		/// to the responsible coworker model.
		/// </summary>
		internal void HandlePrecMedDelete() 
		{
			if (DeletePrecMedWished())
			{
                try
                {
                    mView.Cursor = Cursors.WaitCursor;
                    ((CoWorkerModel)mModel).DeletePrecMed();
                }
                catch (UIWarningException uwe)
                {
                    ExceptionProcessor.GetInstance().Process(uwe);
                }
                catch (System.Data.OracleClient.OracleException oraex)
                {
                    throw new UIFatalException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR)+ oraex.Message);
                }
                finally
                {
                    mView.Cursor = Cursors.Default;
                }
			}
		}

		/// <summary>
		/// Delegates user input (selection of an existing plant ) 
		/// in the plant list in register coordinator to responsible
		/// coworker model.
		/// Shows user prompt if the current user ( missing plant manager rights )
		/// is not allowed to edit this plant. 
		/// </summary>
		/// <param name="pPlantName">name of the selected plant</param>
		internal void HandleStartEditPlant(String pPlantName) 
		{
			try 
			{
				((CoWorkerModel) mModel).StartEditPlant(pPlantName);
			}
			catch (UIWarningException uwe)
			{
				ExceptionProcessor.GetInstance().Process(uwe);
			}
		}

        /// <summary>
        /// Handles checked changed on radiobuttons: delegate to model
        /// </summary>
		internal void HandleRadioButtons()
		{
			((CoWorkerModel) mModel).ValidateRadioButtonsEnableFields();
		}

        /// <summary>
        /// Handles case that radio button Lichtbildausweis SmartAct set to Yes:
        /// user must acknowledge choice
        /// </summary>   
        internal void HandleIdPhotoSmartActYes()
        {        
            FrmCoWorker viewCoWorker = (FrmCoWorker)mView;

            if (viewCoWorker.RbtCoIdPhotoSmActYes.Checked && viewCoWorker.TxtCoSmartActNo.Text.Length == 0)
            {
                ((CoWorkerModel)mModel).ClearStatusBar();

                // Should always be yes
                MessageBox.Show(MessageSingleton.GetInstance().GetMessage(MessageSingleton.SMARTACT_YES_CONFIRM), TitleMessage, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ((CoWorkerModel)mModel).ValidateIdCardPhoto();
            }
        }


        /// <summary>
        /// Handles changes in radio buttons for Id cards
        /// </summary>
		internal void HandleIdCardPhoto()
		{
            ((CoWorkerModel)mModel).ValidateIdCardPhoto();
		}


        /// <summary>
        /// Controls deleting of the id card numbers in SmartAct.
        /// If user explicitly chooses this step then photo id card from Smartact will be deleted in FPASS (and ZKS)
        /// </summary>
        internal void HandleDeleteIdCardSmartAct()
        {
            try
            {
                FrmCoWorker viewCoWorker = (FrmCoWorker)mView;

                // Make up info text for message
                var info1 = viewCoWorker.CbxCoPKI.Checked ? "mit" : "ohne";
                var info2 = viewCoWorker.CbxSiSeIdPhotoSmActRec.Checked ? "wurde" : "wurde nicht";

                DialogResult dialogResult = MessageBox.Show(string.Format(MessageSingleton.GetInstance().GetMessage(MessageSingleton.SMARTACT_NO_CONFIRM), info1, info2),
                                            TitleMessage,
                                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {                   
                    mView.Cursor = Cursors.WaitCursor;
                    mCoWorkerModel.DeleteIdCardSmartAct();
                }              
            }
            catch (UIWarningException uwe)
            {
                ExceptionProcessor.GetInstance().Process(uwe);
            }
            mView.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Handles changes on checkboxes for Acess on tab Recption (Empfang)
        /// </summary>
        internal void HandleAccessAuthReception()
        {
            ((CoWorkerModel)mModel).ValidateAccessAuthReception();
        }

        /// <summary>
        /// Handles changes on checkboxes for Acess on tab Site Security (Werkschutz)
        /// </summary>
		internal void HandleAccessAuthSiteSecurity()
		{
			((CoWorkerModel) mModel).ValidateAccessAuthSiteSecurity();
		}

		/// <summary>
		/// 28.04.04.: Reaction to gen PDF docu "Sicherheitsunterweisung"
		/// </summary>
		internal void HandleGenerateDocSafety()
		{
			((CoWorkerModel) mModel).GenerateDocSafety();
		}

		/// <summary>
		/// 30.04.04: New event to open popup form showing history of coordinatorsa current coworker was assigned to
		/// </summary>
		/// <exception cref="UIWarningException">if this is a new coworker since no coordinator history</exception>
		internal void HandlePopCoordHist()
		{
			try
			{
				mView.Cursor = Cursors.WaitCursor;
				if (mCoWorkerModel.Status == AllFPASSDialogs.DIALOG_STATUS_NEW)
				{
					throw new UIWarningException( MessageSingleton.GetInstance().GetMessage(MessageSingleton.INVALID_COORD_HIST) );
				}
				PopCoWorkerHistController popcontroller = (PopCoWorkerHistController) FPASSControllSingleton.GetInstance().GetDialog(AllFPASSDialogs.POPUP_COWORKER_COORD_HIST);
				popcontroller.SetCurrentFFMA( mCoWorkerModel.CoWorkerId );
				FPASSControllSingleton.GetInstance().ShowModalDialog ( this, popcontroller );
				mView.Cursor = Cursors.Default;
			}
			catch ( UIWarningException uwe )
			{
				mView.Cursor = Cursors.Default;
				ExceptionProcessor.GetInstance().Process(uwe);
			}
		}

		/// <summary>
        /// Shows the resp mask receipt (Maskenrückgabebeleg).
        /// Save changes first, then loads Acrobat Reader with "Print" dialog.
		/// </summary>
		internal void HandleRespMaskTicket()
		{
			mCoWorkerModel.ClearStatusBar();
			try 
			{
				if ( mCoWorkerModel.Status == AllFPASSDialogs.DIALOG_STATUS_NEW )
				{
					throw new UIWarningException( MessageSingleton.GetInstance().GetMessage(MessageSingleton.MASK_TICKET_NO_CWR) );
				}
				
				mView.Cursor = System.Windows.Forms.Cursors.WaitCursor;
				mCoWorkerModel.PromptMaskTicket = true;
				mCoWorkerModel.Save();
                mView.Cursor = System.Windows.Forms.Cursors.Default;
				
				mCoWorkerModel.ShowMessageInStatusBar(MessageSingleton.GetInstance().GetMessage(MessageSingleton.MASKTICKET_SUCCESS ) );				
			}
			catch (UIWarningException uwe)
			{
                mView.Cursor = System.Windows.Forms.Cursors.Default;
				ExceptionProcessor.GetInstance().Process(uwe);
			}
		}

		/// <summary>
		/// Prompts user to confirm current prec medical should be deleted
		/// (influences access authorization)
		/// </summary>
		/// <returns>true if delete wished, false if not</returns>
		private bool DeletePrecMedWished()
		{
			bool flgDelWished = false;

			if (MessageBox.Show(MessageSingleton.GetInstance().GetMessage(MessageSingleton.DELETE_MED_QUESTION),
                            TitleMessage,
				            MessageBoxButtons.YesNo, MessageBoxIcon.Question)
				== DialogResult.Yes) 
			{
				flgDelWished = true;
			}
			return flgDelWished;
		}

		#endregion 

	}
}

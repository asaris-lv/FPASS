using System;
using System.Windows.Forms;

using de.pta.Component.Errorhandling;

using Degussa.FPASS.Gui;
using Degussa.FPASS.FPASSApplication;
using Degussa.FPASS.Util.Exceptions;
using Degussa.FPASS.Util.ListOfValues;
using Degussa.FPASS.Util.Messages;

namespace Degussa.FPASS.Gui.Dialog.Pass
{
	/// <summary>
	/// A DeleteController is the view of the MVC-triad DeleteModel,
	/// DeleteController and FrmCoWorkerDelete.
	/// DeleteController extends from the FPASSBaseController.
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
	public class DeleteController : FPASSBaseController
	{
		#region Members

		/// <summary>
		/// Model belonging to this MVC triad
		/// </summary>
		private		DeleteModel  mDeleteModel;
		

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public DeleteController()
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
			mDialogId = AllFPASSDialogs.COWORKER_DELETE_DIALOG;
			mView = new FrmCoWorkerDelete();
			mView.RegisterController(this);

			mModel = new DeleteModel();
			mModel.registerView(mView);

			mDeleteModel = (DeleteModel)mModel;
		}	

		#endregion //End of Initialization

		#region Accessors 

		#endregion //End of Accessors

		#region Methods 

		internal override void HandleEventShowHelp() 
		{
			Help.ShowHelp( this.mView, 
				Application.StartupPath + AllFPASSDialogs.FPASS_MASTER_HELPFILE, 
				HelpNavigator.Topic,
				AllFPASSDialogs.HELPTOPIC_COWORKER_DELETE );
		}

		internal void HandleEventOpenSearchDialog() 
		{
			mDeleteModel.AutoSearch = false;
			this.HandleEventOpenSearchExternalContractorDialog();

		}

		/// <summary>
		/// New 08.03.2004: Datagrid on form should be refreshed if coworkers edited from DeleteDialog
		/// Do not show warning MsgBox if no search criteria chosen
		/// 25.05.04: Jump back to row index of prev selected coworker (unless has disappeared)
		/// </summary>
		internal void StartAutomaticSearch() 
		{
			((DeleteModel) mModel).ClearStatusBar();
			try 
			{
				try
				{
					// Execute coworker search by using model, jump to position of current CWR
					((FrmCoWorkerDelete) mView).Cursor = System.Windows.Forms.Cursors.WaitCursor;
					((FrmCoWorkerDelete) mView).GridIsLoading = true;
					
					mDeleteModel.GetCoWorkers();
					this.JumpToCurrentCoWorker();

					((FrmCoWorkerDelete) mView).GridIsLoading = false;
				}
				catch (System.Data.OracleClient.OracleException oraex)
				{
					// Error from SQL ' delimiter
					if ( oraex.Code == 01756 )
					{
						throw new UIWarningException (MessageSingleton.GetInstance().GetMessage
							(MessageSingleton.CANNOT_SEARCH_CHAR));
					}
					else
					{
						throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(
							MessageSingleton.FATAL_DB_ERROR) 
							+ oraex.Message );
					}
				}				
			} 
			catch ( UIWarningException ) 
			{	
				((FrmCoWorkerDelete)mView).BtnDeleteSummary.Enabled      = false;
				((FrmCoWorkerDelete)mView).BtnDeleteChoice.Enabled       = false;
				((FrmCoWorkerDelete)mView).BtnCoWorkerDetails.Enabled    = false;
				((FrmCoWorkerDelete)mView).BtnAccessAuthorization.Enabled= false;				
			}
			finally
			{
				((FrmCoWorkerDelete) mView).Cursor = System.Windows.Forms.Cursors.Default;
			}
		}

		

		/// <summary>
		/// New 19.03.2004: Need to execute automatic search when returning to Summary CWR
		/// </summary>
		internal void HandleEventLeaveFrmDelCWR()
		{
			if ( null  != mParent ) 
			{
				mParent.PreShow();
			}
			HandleEventBackToSummaryDialog();
		}
		
		/// <summary>
		/// New 31.03.04: Don't want to lose cbx selections but want to refresh comboboxes:
		/// Do this by pressing F5 in DeleteView, this is the event handler.
		/// </summary>
		internal void HandleEventRefreshDeleteViewLists()
		{
			FPASSControllSingleton.GetInstance().ReInitializeListOfValues();
			RefreshMyLists();
		}

		/// <summary>
		/// Button "Maske leeren": empty contents of form
		/// </summary>
		internal void HandleEventBtnFormEmpty()
		{
			((DeleteModel) mModel).ClearStatusBar();
			((DeleteModel) mModel).ClearFields();
			((FrmCoWorkerDelete)mView).BtnAccessAuthorization.Enabled = false;
			((FrmCoWorkerDelete)mView).BtnCoWorkerDetails.Enabled = false;
			((FrmCoWorkerDelete)mView).BtnDeleteChoice.Enabled = false;
			((FrmCoWorkerDelete)mView).BtnDeleteSummary.Enabled = false;
		}


		/// <summary>
		/// Execute coworker search 
		/// 03.03.2004: Cannot search for strings with ' in as this is part of SQL string delimiter: catch ORA exception
		/// Set AutoSeacrh to false here so atomatic refresh possible without getting MsgBox
		/// Disable buttons at foot of mask if no coworkeras found: nothing to edit
		/// </summary>
		internal void HandleEventBtnSearchDeleteCoWorker() 
		{
			((DeleteModel) mModel).ClearStatusBar();
			((DeleteModel) mModel).AutoSearch = false;

			try 
			{
				try
				{
					((FrmCoWorkerDelete) mView).Cursor = System.Windows.Forms.Cursors.WaitCursor;
					mDeleteModel.GetCoWorkers();
					((FrmCoWorkerDelete) mView).Cursor = System.Windows.Forms.Cursors.Default;
				}
				catch (System.Data.OracleClient.OracleException oraex)
				{
					// Error from SQL ' delimiter
					if ( oraex.Code == 01756 )
					{
						throw new UIWarningException (MessageSingleton.GetInstance().GetMessage
							(MessageSingleton.CANNOT_SEARCH_CHAR));
					}
					else
					{
						throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(
							MessageSingleton.FATAL_DB_ERROR) 
							+ oraex.Message );
					}
				}
				
			} 
			catch ( UIWarningException uwe ) 
			{	
				((FrmCoWorkerDelete)mView).BtnDeleteSummary.Enabled = false;
				((FrmCoWorkerDelete)mView).BtnDeleteChoice.Enabled= false;
				((FrmCoWorkerDelete)mView).BtnCoWorkerDetails.Enabled= false;
				((FrmCoWorkerDelete)mView).BtnAccessAuthorization.Enabled= false;
				((FrmCoWorkerDelete) mView).Cursor = System.Windows.Forms.Cursors.Default;
				ExceptionProcessor.GetInstance().Process(uwe);
			}
		}

		/// <summary>
		/// Show details of current coworker: open FrmCoWorker
		/// Commented out locking mechanism so that CWRs can be edited directly from DeleteForm
		/// 19.03.04 rare error: if user aatempts to edit coworker someone else has already deleted, 
		/// then catch ActionCancelledException
		/// </summary>
		/// <param name="pCurrentFFMAId"></param>
		internal void HandleEventOpenProcessDialog(decimal pCurrentFFMAId) 
		{	
			try
			{
				try
				{
					CoWorkerController coWorkerController = (CoWorkerController)FPASSControllSingleton.
						GetInstance().GetDialog(AllFPASSDialogs.COWORKER_PROCESS_DIALOG);
					coWorkerController.SetCurrentFFMA(pCurrentFFMAId);
	
					//	coWorkerController.LockCoWorkerDialog();
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
		/// Attempt to extend date valid until for selected coworker/s
		/// Refresh form via autom. search
		/// </summary>
		internal void HandleEventExtendValidUntil()
		{
			((DeleteModel) mModel).ClearStatusBar();
			try 
			{
				((FrmCoWorkerDelete) mView).Cursor = Cursors.WaitCursor;
				((DeleteModel)mModel).ExtendValidUntil();
				((FrmCoWorkerDelete) mView).Cursor = Cursors.Default;
			} 
			catch ( UIWarningException uwe ) 
			{
				((FrmCoWorkerDelete) mView).Cursor = Cursors.Default;
				ExceptionProcessor.GetInstance().Process(uwe);
			}
			((DeleteModel)mModel).AutoSearch = true;
			StartAutomaticSearch();
			
		}

		/// <summary>
		/// Delete selected coworker/s
		/// Refresh form via autom. search
		/// </summary>
		internal void HandleEventDeleteCoworker()
		{
			try 
			{
				((DeleteModel)mModel).DeleteCoworkers(true);
			} 
			catch ( UIWarningException uwe ) 
			{
//                ExceptionProcessor.GetInstance().Process(uwe);
                base.HandleEventShowFPASSMessageBox(uwe.Message);
            }
            catch (UIErrorException uee)
            {
                base.HandleEventShowFPASSMessageBox(uee.Message);
            }
            ((DeleteModel)mModel).AutoSearch = true;
			StartAutomaticSearch();
			
		}

		/// <summary>
		/// Delete all coworkers 
		/// MsgBox for confirmation
		/// Refresh form via autom. search
		/// </summary>
		internal void HandleEventDeleteAllCoworker()
		{
			if (DoDeleteWished())
			{
				try 
				{
					((DeleteModel)mModel).DeleteCoworkers(false);
				}
                catch (BaseUIException uwe)
                {
//                    ExceptionProcessor.GetInstance().Process(uwe);
                    base.HandleEventShowFPASSMessageBox(uwe.Message);
                }

				
				((DeleteModel)mModel).AutoSearch = true;
				StartAutomaticSearch();
			}
		}

		/// <summary>
		/// Prompts user to confirm that all CWR records should be deleted
		/// </summary>
		/// <returns>true if delete wished, false if not</returns>
		private bool DoDeleteWished()
		{
			bool flgDelWished = false;

			if ( DialogResult.Yes == MessageBox.Show(MessageSingleton.GetInstance().GetMessage(MessageSingleton.CWR_DELETE_ALL), TitleMessage, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
			{
				flgDelWished = true;
			}
			return flgDelWished;
		}

		/// <summary>
		/// New 25.05.04: Jump back to row in datagrid containing coworker currently being edited
		/// </summary>
		private void JumpToCurrentCoWorker()
		{
			// if current row of coworker has disappeared (i.e. query returned fewer rows
			// then row index, set row index to 0
			int lastRowIndex = mDeleteModel.HashCoWorkers.Count-1;

			if ( ((FrmCoWorkerDelete) mView).CurrentRowIndex > lastRowIndex )
			{
				((FrmCoWorkerDelete) mView).CurrentRowIndex = lastRowIndex;
			}

			// Jump to previously selected CWR if there is one ( -1 means none)
			if ( ((FrmCoWorkerDelete) mView).CurrentRowIndex > -1 )
			{
				((FrmCoWorkerDelete) mView).JumpToCurrentCoWorker();
			}
		}



		#endregion // End of Methods

	}
}

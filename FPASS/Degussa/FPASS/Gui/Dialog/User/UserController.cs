using System;
using System.Windows.Forms;

using Degussa.FPASS.Bo.Administration;
using Degussa.FPASS.Gui;
using Degussa.FPASS.FPASSApplication;
using Degussa.FPASS.Util.Exceptions;
using Degussa.FPASS.Util.Messages;

using de.pta.Component.Errorhandling;

namespace Degussa.FPASS.Gui.Dialog.User
{
	/// <summary>
	/// Summary description for UserController.
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
	public class UserController : FPASSBaseController
	{
		#region Members

		
		private BOUser mBOUser;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public UserController()
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
			mDialogId = AllFPASSDialogs.USER_DIALOG;
			mView = new FrmUser();
			mView.RegisterController(this);

			mModel = new UserModel();
			mModel.registerView(mView);
		}	

		#endregion //End of Initialization

		#region Accessors 

		public BOUser CurrentBOUser
		{
			get 
			{
				return mBOUser;
			}
			set 
			{
				mBOUser = value;
			}
		} 

		#endregion //End of Accessors

		#region Methods 

		internal override void HandleEventShowHelp() 
		{
			Help.ShowHelp(this.mView, 
				Application.StartupPath + AllFPASSDialogs.FPASS_MASTER_HELPFILE, 
				HelpNavigator.Topic,
				AllFPASSDialogs.HELPTOPIC_USER );
		}

		private bool DeleteUserWished()
		{
			// Prompts user to confirm delete
			bool flgDelWished = false;

			if ( MessageBox.Show(MessageSingleton.GetInstance().GetMessage(
				MessageSingleton.DELETE_USER_QUESTION), 
				TitleMessage,
				MessageBoxButtons.YesNo, MessageBoxIcon.Question)
				== DialogResult.Yes ) 
			{
				flgDelWished = true;
			}
			return flgDelWished;
		}


		/// <summary>
		/// Messagebox with YesNoCancel prompts user to save changes 
		/// If "cancel" is pressed, throw a warning (ActionCancelledException) to cancel whatever is being processed. 
		/// </summary>
		/// <returns></returns>
		private bool SaveUserChangesWished()
		{
			bool flgSaveWished = false;

			DialogResult dgres = base.SaveChangesWished();

			if ( dgres.Equals(DialogResult.Yes) )
			{
				flgSaveWished = true;
			}
			else if ( dgres.Equals(DialogResult.No) )
			{
				flgSaveWished = false;
			}
			else
			{
				throw new ActionCancelledException( "cancel" );
			}
			return flgSaveWished;
		}

		/// <summary>
		/// Show message in status bar for successful save
		/// </summary>
		private void ShowChangesSuccessful()
		{
			((UserModel)mModel).ShowMessageInStatusBar( MessageSingleton.GetInstance().GetMessage(
				MessageSingleton.SAVE_SUCCESS) );
		}

		/// <summary>
		/// Show message in status bar for successful delete
		/// </summary>
		private void ShowDelSuccessful()
		{
			((UserModel)mModel).ShowMessageInStatusBar( MessageSingleton.GetInstance().GetMessage(
				MessageSingleton.DELETE_SUCCESS) );
		}

		/// <summary>
		/// Clear status bar
		/// </summary>
		private void ClearSbMessages()
		{
			((UserModel)mModel).ClearStatusBar();
		}
		

		/// <summary>
		/// Clicking on control box in top left of window cannot be interrupted,
		/// no point in asking if save wished 28.11.03
		/// </summary>
		internal override void HandleEventUnControlledClose() 
		{
	
		}

		/// <summary>
		/// Called when a new search is carried out or form to be exited via button (controlled close)
		/// Prompt user to save changes
		/// </summary>
		private void HandleEventPromptForSave()
		{			
			if (((FrmUser)mView).ContentChanged)
			{
				// Prompt user to save changes
				if (SaveUserChangesWished()) 
				{
					this.ControlTheSave();
				}
			}
			((UserModel)mModel).ClearTextFields();
		}
		
		/// <summary>
		/// Handles event from form: Button "Save" clicked:
		/// catch Warniung Exception
		/// </summary>
		internal void HandleEventBtnSaveClick()
		{			
			this.ClearSbMessages();
			try
			{
				((FrmUser) mView).Cursor = System.Windows.Forms.Cursors.WaitCursor;
				this.ControlTheSave();
				((FrmUser) mView).Cursor = System.Windows.Forms.Cursors.Default;
			}
			catch (UIWarningException uwe)
			{
				((FrmUser) mView).Cursor = System.Windows.Forms.Cursors.Default;
				ExceptionProcessor.GetInstance().Process(uwe);
			}			
		}

		/// <summary>
		/// Central method responsible for saving: does not catch UIWarningexcepetion 
		/// as this must be handled separately for each event
		/// Make sure the content of form really has changed and diff. between INSERT and UPDATE
		/// </summary>
		private void ControlTheSave()
		{
			if ( ((FrmUser) mView).ContentChanged )
			{
				((FrmUser) mView).Cursor = System.Windows.Forms.Cursors.WaitCursor;
				if ( ((FrmUser)mView).CurrentFPASSUserID == -1 )
				{
					// It's an insert
					((UserModel)mModel).SaveNewUser();
				}
				else
				{
					// It's an update
					((UserModel)mModel).SaveChangesUser();
				}
				this.ShowChangesSuccessful();
				((FrmUser) mView).Cursor = System.Windows.Forms.Cursors.Default;
			}
		}

		/// <summary>
		/// Enables textfields for editing new user.
		/// Exception is that thrown when traing to save invalid data from previous operation
		/// </summary>
		internal void HandleEventBtnNewClick()
		{			
			this.ClearSbMessages();
			try
			{
				this.HandleEventPromptForSave();
				((UserModel)mModel).CreateNewUser();
			}
			catch ( ActionCancelledException )
			{
				((UserModel)mModel).ShowMessageInStatusBar( 
					MessageSingleton.GetInstance().GetMessage(MessageSingleton.ACTION_CANCELLED) );
			}	
			catch (UIWarningException uwe)
			{
				((FrmUser) mView).Cursor = System.Windows.Forms.Cursors.Default;
				ExceptionProcessor.GetInstance().Process(uwe);
			}	
		}

		internal void HandleEventBtnDeleteClick()
		{	
			this.ClearSbMessages();
			try
			{
				// If no record selected => error
				decimal currFormFPASSUserPKID = ((FrmUser)mView).CurrentFPASSUserID;
				decimal currFormUMUserPKID    = ((FrmUser)mView).CurrentUMUserID;
					
				if ( currFormFPASSUserPKID == -1 || currFormUMUserPKID == -1 )
				{
					throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.NO_USER_ROW));
				}								
				// Prompt & do delete
				else if ( DeleteUserWished() )
				{					
					((FrmUser) mView).Cursor = System.Windows.Forms.Cursors.WaitCursor;
					((UserModel)mModel).DeleteUser();								
					FPASSControllSingleton.GetInstance().ReInitializeListOfValues();
					this.ShowDelSuccessful();	
					((FrmUser) mView).Cursor = System.Windows.Forms.Cursors.Default;
				}
			}
			catch (UIWarningException uwe)
			{
				((FrmUser) mView).Cursor = System.Windows.Forms.Cursors.Default;
				ExceptionProcessor.GetInstance().Process(uwe);
			}		
		}

		/// <summary>
		/// Open next form FrmUserToRole and show the roles assigned to the current user
		/// Pass current BOUser to form
		/// </summary>
		internal void HandleEventOpenToUserRoleDialog() 
		{
			this.ClearSbMessages();
			try
			{
				if ( ((FrmUser)mView).CurrentFPASSUserID == -1 || ((FrmUser)mView).CurrentUMUserID == -1 )
				{
					throw new UIWarningException(
								MessageSingleton.GetInstance().GetMessage(MessageSingleton.NO_USER_ROW) );
				}
				else
				{
					UserToRoleController currUserToRoleController = 
						(UserToRoleController)FPASSControllSingleton.GetInstance().GetDialog(AllFPASSDialogs.USER_TO_ROLE_DIALOG);
					
					currUserToRoleController.CurrentBOUser = ((UserModel) mModel).CurrentBOUser;					
					currUserToRoleController.LoadUserToRoleData();
					
					FPASSControllSingleton.GetInstance().ShowModalDialog(this, AllFPASSDialogs.USER_TO_ROLE_DIALOG);
				}
			}
			catch (UIWarningException uwe)
			{
				ExceptionProcessor.GetInstance().Process(uwe);
			}
		}

		internal void HandleEventBtnClearFields()
		{			
			((UserModel)mModel).ClearTextFields();
			((UserModel)mModel).CancelSearchFields();
			((UserModel)mModel).SetCurrentUserIDToDefault();
		}


		/// <summary>
		/// "Zurück" Button in FrmUser
		/// Form is only closed when everything ok (also when asked to save is save is OK)
		/// Catch ActionCancelledExcpetion or UIWarningExcption (e.g. for invalid data) and don't close form 
		/// </summary>
		internal void HandleEventBtnBackClick()
		{	
			this.ClearSbMessages();	
			try
			{
				this.HandleEventPromptForSave();	
				((UserModel)mModel).CancelSearchFields();
				((UserModel)mModel).ClearTextFields();
				((UserModel)mModel).SetCurrentUserIDToDefault();
				this.HandleCloseDialog();
			}
			catch ( ActionCancelledException )
			{
				((UserModel)mModel).ShowMessageInStatusBar( 
						MessageSingleton.GetInstance().GetMessage(MessageSingleton.ACTION_CANCELLED) );
			}	
			catch (UIWarningException uwe)
			{
				ExceptionProcessor.GetInstance().Process(uwe);
			}	
		}

		/// <summary>
		/// Search for existing FPASS Users: button "Suchen" in GUI
		/// 03.03.2004: Cannot search for strings with ' in as this is part of SQL string delimiter: catch ORA exception
		/// </summary>
		internal void HandleEventBtnSearchClick()
		{			
			this.ClearSbMessages();
			try
			{
				try
				{
					this.HandleEventPromptForSave();
					((FrmUser) mView).Cursor = System.Windows.Forms.Cursors.WaitCursor;
					((UserModel)mModel).GetUsers(true);
					((FrmUser) mView).Cursor = System.Windows.Forms.Cursors.Default;
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
			catch ( ActionCancelledException )
			{
				((UserModel)mModel).ShowMessageInStatusBar( 
					MessageSingleton.GetInstance().GetMessage(MessageSingleton.ACTION_CANCELLED) );
			}	
			catch (UIWarningException uwe)
			{
				((FrmUser) mView).Cursor = System.Windows.Forms.Cursors.Default;
				ExceptionProcessor.GetInstance().Process(uwe);
			}
		}

		/// <summary>
		/// Empty textfields at top of Form (fields for Search )
		/// </summary>
		internal void HandleEventBtnSearchCancel()
		{			
			((UserModel)mModel).CancelSearchFields();
		}

		internal void HandleEventTableNavigated()
		{
			this.ClearSbMessages();
			((FrmUser) mView).Cursor = System.Windows.Forms.Cursors.WaitCursor;
			((UserModel)mModel).LoadIndividualUsers();
			((FrmUser) mView).Cursor = System.Windows.Forms.Cursors.Default;
		}

		internal void HandleEventPlantItemChecked()
		{
			// Has the assignment of plants changed?
			this.ClearSbMessages();
			((FrmUser)mView).CheckListBoxChanged = true;
			((FrmUser)mView).ContentChanged		 = true;
		}


		#endregion // End of Methods

	}
}

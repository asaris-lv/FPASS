using System;
using System.Windows.Forms;

using Degussa.FPASS.Bo.Administration;
using Degussa.FPASS.Gui;
using Degussa.FPASS.FPASSApplication;
using Degussa.FPASS.Util.Messages;

using de.pta.Component.Errorhandling;

namespace Degussa.FPASS.Gui.Dialog.User
{
	/// <summary>
	/// Summary description for UserToRoleController.
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
	public class UserToRoleController : FPASSBaseController
	{
		#region Members

		private int mCurrentUMUserPKID = -1;
		protected BOUser mBOUser;

		

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public UserToRoleController()
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
			mDialogId = AllFPASSDialogs.USER_TO_ROLE_DIALOG;
			mView = new FrmUserToRole();
			mView.RegisterController(this);

			mModel = new UserToRoleModel();
			mModel.registerView(mView);

            TitleMessage = "FPASS - Benutzerverwaltung";

			// Register typified datasets with model and views (UserControls)
			((UserToRoleModel)mModel).RegisterUserToRoleDataSet( ((FrmUserToRole) mView).CurrentDSRole );
		}	

		#endregion //End of Initialization

		#region Accessors 

		// Current UserBO
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
		
		public int CurrentUMUserPKID
		{
			get 
			{
				return mCurrentUMUserPKID;
			}
			set 
			{
				mCurrentUMUserPKID = value;
			}
		} 

		#endregion //End of Accessors

		#region Methods 

		internal override void HandleEventShowHelp() 
		{
			Help.ShowHelp(this.mView, 
				Application.StartupPath + AllFPASSDialogs.FPASS_MASTER_HELPFILE, 
				HelpNavigator.Topic,
				AllFPASSDialogs.HELPTOPIC_USER_TO_ROLE );
		}


		private void ShowChangesSuccessful()
		{
			((UserToRoleModel)mModel).ShowMessageInStatusBar( MessageSingleton.GetInstance().GetMessage(
				MessageSingleton.SAVE_SUCCESS) );
		}

		private void ShowDeleteSuccessful()
		{
			((UserToRoleModel)mModel).ShowMessageInStatusBar( MessageSingleton.GetInstance().GetMessage(
				MessageSingleton.DELETE_SUCCESS) );
		}

		private void ClearSbMessages()
		{
			((UserToRoleModel)mModel).ClearStatusBar();
		}

		public void LoadUserToRoleData()
		{
			this.ClearSbMessages();
			// RegisterUserBO with mod & form
			((FrmUserToRole) mView).Cursor = System.Windows.Forms.Cursors.WaitCursor;
			((UserToRoleModel)mModel).CurrentBOUser = this.CurrentBOUser;
			((FrmUserToRole) mView).CurrentBOUser = this.CurrentBOUser;

			// Load current user 
			((UserToRoleModel)mModel).LoadFormCurrentUser();

			// Fill LOV combobox and load dataset when mask is loaded
			((UserToRoleModel)mModel).FillFrmLOVRoles();
			((UserToRoleModel)mModel).GetRoles();
			((FrmUserToRole) mView).Cursor = System.Windows.Forms.Cursors.Default;
			
		}

		internal void HandleEventOpenRoleDialog() 
		{
			this.ClearSbMessages();
			FPASSControllSingleton.GetInstance().ShowModalDialog(this, AllFPASSDialogs.ROLE_DIALOG);
		}

        /// <summary>
        /// Handles button click on "Assign role"
        /// </summary>
		internal void HandleEventAssignmentNewRole() 
		{
			ClearSbMessages();
            try
            {
                if (mBOUser == null)
                {
                    throw new UIErrorException("Programmfehler: kein Benutzer gefunden");
                }
                else
                {
                    mView.Cursor = Cursors.WaitCursor;
                    ((UserToRoleModel)mModel).AssignNewRoleToUser();
                    FPASSControllSingleton.GetInstance().ReInitializeListOfValues();
                    ShowChangesSuccessful();               
                }
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

		internal void HandleEventDeleteRoleAssignment() 
		{
			this.ClearSbMessages();
			try
			{
				if ( ((FrmUserToRole) mView).CurrentUMRoleID == -1 || this.mBOUser == null )
				{
					throw new UIWarningException( MessageSingleton.GetInstance().GetMessage(MessageSingleton.NO_ROLE_ROW) );
				}
				else
				{
					((FrmUserToRole) mView).Cursor = System.Windows.Forms.Cursors.WaitCursor;
					((UserToRoleModel) mModel).DeleteRoleFromUser();
					this.ShowDeleteSuccessful();
					FPASSControllSingleton.GetInstance().ReInitializeListOfValues();
					((FrmUserToRole) mView).Cursor = System.Windows.Forms.Cursors.Default;
				}
			}
			catch (UIWarningException uwe)
			{
				((FrmUserToRole) mView).Cursor = System.Windows.Forms.Cursors.Default;
				ExceptionProcessor.GetInstance().Process(uwe);
			}
		}

		internal void HandleEventTableNavigated()
		{
			this.ClearSbMessages();
			((UserToRoleModel) mModel).LoadIndividualRoleDataSet();
		}

		#endregion // End of Methods

	}
}

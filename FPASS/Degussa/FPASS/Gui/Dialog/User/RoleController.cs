using System;
using System.Windows.Forms;

using Degussa.FPASS.Gui;
using Degussa.FPASS.FPASSApplication;
using Degussa.FPASS.Util.Messages;

using de.pta.Component.Errorhandling;

namespace Degussa.FPASS.Gui.Dialog.User
{
	/// <summary>
	/// Summary description for RoleController.
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
	public class RoleController : FPASSBaseController
	{
		#region Members

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public RoleController()
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

			mDialogId = AllFPASSDialogs.ROLE_DIALOG;
			mView = new FrmRole();
			mView.RegisterController(this);

			mModel = new RoleModel();
			mModel.registerView(mView);

			// Register typified datasets (attribute of form)with model and views (UserControls)
			((RoleModel) mModel).RegisterUserDataSet(((FrmRole)mView).DSUser);
		}	

		#endregion //End of Initialization

		#region Accessors 

		#endregion //End of Accessors

		#region Methods 

		internal override void HandleEventShowHelp() 
		{
			Help.ShowHelp(this.mView, 
				Application.StartupPath + AllFPASSDialogs.FPASS_MASTER_HELPFILE, 
				HelpNavigator.Topic,
				AllFPASSDialogs.HELPTOPIC_ROLE );
		}

		/// <summary>
		/// Show all the users assigned to the currently selected role
		/// </summary>
		internal void HandleEventBtnSearch()
		{
			try
			{
				((FrmRole) mView).Cursor = System.Windows.Forms.Cursors.WaitCursor;
				((RoleModel)mModel).GetUsers();
				((FrmRole) mView).Cursor = System.Windows.Forms.Cursors.Default;
			} 
			catch ( UIWarningException uwe ) 
			{
				((FrmRole) mView).Cursor = System.Windows.Forms.Cursors.Default;
				ExceptionProcessor.GetInstance().Process(uwe);
			}
		}

		internal void HandleEventBtnBack()
		{
			// Tidy up on leaving form
			((RoleModel)mModel).ClearTextFields();
		}

		#endregion // End of Methods

	}
}

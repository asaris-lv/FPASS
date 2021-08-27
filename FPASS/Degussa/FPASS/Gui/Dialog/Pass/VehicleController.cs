using System;
using System.Windows.Forms;

using de.pta.Component.Errorhandling;

using Degussa.FPASS.Util.Exceptions;
using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.Gui;
using Degussa.FPASS.FPASSApplication;
using Degussa.FPASS.Gui.Dialog;

namespace Degussa.FPASS.Gui.Dialog.Pass
{
	/// <summary>
	/// VehicleController is the controller 
	/// of the MVC-triad VehicleModel,
	/// VehicleController and FrmVehicle.
	/// VehicleController extends from the FPASSBaseController.
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
	public class VehicleController : FPASSBaseController
	{
		#region Members

		/// <summary>
		/// used to hold the model of this triad. hold for convenience to avoid casting
		/// </summary>
		private	VehicleModel  mVehicleModel;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public VehicleController()
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
			mDialogId = AllFPASSDialogs.VEHICLE_DIALOG;
			mView = new FrmVehicle();
			mView.RegisterController(this);

			mModel = new VehicleModel();
			mModel.registerView(mView);

			mVehicleModel = (VehicleModel)mModel;
		}	

		#endregion //End of Initialization

		#region Accessors 

		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// Show help topic for current form
		/// </summary>
		internal override void HandleEventShowHelp() 
		{
			Help.ShowHelp( this.mView, 
				Application.StartupPath + AllFPASSDialogs.FPASS_MASTER_HELPFILE, 
				HelpNavigator.Topic,
				AllFPASSDialogs.HELPTOPIC_COWORKER_VEHICLE );
		}

		
		/// <summary>
		/// Get coworkers with the selected vehicle access attributes (long, short, granted..)
		/// First disable buttons at foot of form
		/// </summary>
		internal void HandleEventBtnSearchVehicleAccess() 
		{
			((FrmVehicle) mView).DgrVehicle.DataSource = null;
			this.EnableButtons(false);
			try 
			{
				((FrmVehicle) mView).Cursor = System.Windows.Forms.Cursors.WaitCursor;
				mVehicleModel.GetVehicleAccess();
				((FrmVehicle) mView).Cursor = System.Windows.Forms.Cursors.Default;
				//((FrmVehicle) mView).BtnSave.Enabled = true;
			} 
			catch ( UIWarningException uwe ) 
			{	
				((FrmVehicle) mView).Cursor = System.Windows.Forms.Cursors.Default;
				((FrmVehicle) mView).BtnSave.Enabled = false;								
				ExceptionProcessor.GetInstance().Process(uwe);
			}
		}
		

		/// <summary>
		/// Load individual vehicle access attributes 
		/// for current coworker (selected in datagrid) in lower part of form
		/// </summary>
		/// <param name="pCwrID">PK ID of current cwr</param>
		internal void HandelEventFillVehicle(decimal pCwrID) 
		{
			((VehicleModel) mModel).ClearStatusBar();
			((VehicleModel) mModel).FillFields(pCwrID);
		}


		/// <summary>
		/// Empty and re-enable fields for search 
		/// </summary>
		internal void HandelEventEnableFields() 
		{
			((VehicleModel) mModel).ClearStatusBar();
			((VehicleModel) mModel).PreClose();
		}


		/// <summary>
		/// Button "Speichern": 
		/// save changes in vehicle access for current coworker, comes from fields in lower part of form  
		/// Do not know which type of access was granted or denied, user, date etc)
		/// Coworker results are refreshed by carrying out new search
		/// </summary>
		/// <param name="pCwrID">PK ID of current cwr</param>
		internal void HandelEventUpdateVehicle(decimal pCwrID) 
		{
			((VehicleModel) mModel).ClearStatusBar();
			try 
			{
				((FrmVehicle) mView).Cursor = System.Windows.Forms.Cursors.WaitCursor;
				((VehicleModel) mModel).ExtendAccessUpdate( pCwrID );
				((FrmVehicle) mView).Cursor = System.Windows.Forms.Cursors.Default;
				//((VehicleModel) mModel).ShowMessageInStatusBar( MessageSingleton.GetInstance().GetMessage(MessageSingleton.SAVE_SUCCESS) );
			} 
			catch ( UIWarningException uwe ) 
			{	
				ExceptionProcessor.GetInstance().Process(uwe);
			}
			this.HandleEventBtnSearchVehicleAccess();
		}

		
		/// <summary>
		/// Button "Einfahrt kurz akzeptieren"
		/// Short vehicle access is granted for selected coworker/s
		/// Coworker results are refreshed by carrying out new search
		/// </summary>
		/// <param name="pCwrID">PK ID of current cwr</param>
		internal void HandelEventUpdateVehicleSort(decimal pCwrID) 
		{
			((VehicleModel) mModel).ClearStatusBar();
			try 
			{
				((FrmVehicle) mView).Cursor = System.Windows.Forms.Cursors.WaitCursor;
				((VehicleModel) mModel).UpdateSelectedShortAccess(pCwrID);
				((FrmVehicle) mView).Cursor = System.Windows.Forms.Cursors.Default;
				((VehicleModel) mModel).ShowMessageInStatusBar( MessageSingleton.GetInstance().GetMessage(MessageSingleton.SAVE_SUCCESS) );
			} 
			catch ( UIWarningException uwe ) 
			{	
				ExceptionProcessor.GetInstance().Process(uwe);
			}
			this.HandleEventBtnSearchVehicleAccess();
		}

		
		/// <summary>
		/// Button "Einfahrt lang akzeptieren"
		/// Long vehicle access is granted for selected coworker/s
		/// Coworker results are refreshed by carrying out new search
		/// </summary>
		/// <param name="pCwrID">PK ID of current cwr</param>
		internal void HandelEventUpdateVehicleLong(decimal pCwrID) 
		{			
			((VehicleModel) mModel).ClearStatusBar();
			try 
			{
				((FrmVehicle) mView).Cursor = System.Windows.Forms.Cursors.WaitCursor;
				((VehicleModel) mModel).UpdateSelectedLongAccess( pCwrID );
				((FrmVehicle) mView).Cursor = System.Windows.Forms.Cursors.Default;
				((VehicleModel) mModel).ShowMessageInStatusBar( MessageSingleton.GetInstance().GetMessage(MessageSingleton.SAVE_SUCCESS) );
			} 
			catch ( UIWarningException uwe ) 
			{	
				ExceptionProcessor.GetInstance().Process(uwe);
			}
			this.HandleEventBtnSearchVehicleAccess();
		}


		/// <summary>
		/// Button "Ablehnen"
		/// Current coworkers are denied the desired vehicle access type
		/// Coworker results are refreshed by carrying out new search
		/// </summary>
		/// <param name="pCwrID">PK ID of current cwr</param>
		internal void HandelEventUpdateNotAcceptedVehicle(decimal pCwrID)
		{
			((VehicleModel) mModel).ClearStatusBar();
			try 
			{
				((FrmVehicle) mView).Cursor = System.Windows.Forms.Cursors.WaitCursor;
				((VehicleModel) mModel).UpdateSelectedAccess( pCwrID );
				((FrmVehicle) mView).Cursor = System.Windows.Forms.Cursors.Default;
				((VehicleModel) mModel).ShowMessageInStatusBar( MessageSingleton.GetInstance().GetMessage(MessageSingleton.SAVE_SUCCESS) );
			} 
			catch ( UIWarningException uwe ) 
			{	
				ExceptionProcessor.GetInstance().Process(uwe);
			}
			this.HandleEventBtnSearchVehicleAccess();
		}


		/// <summary>
		/// Enables or disables buttons at foot of form
		/// </summary>
		/// <param name="pState">Set Enabled to true or false</param>
		private void EnableButtons( bool pState ) 
		{
			((FrmVehicle) mView).BtnNotAccepted.Enabled	  = pState;
			((FrmVehicle) mView).BtnAcceptedLong.Enabled  = pState;
			((FrmVehicle) mView).BtnAcceptedShort.Enabled = pState;
			((FrmVehicle) mView).BtnSave.Enabled		  = pState;
		}

	
		/// <summary>
		/// if two or more CWR rows selected in datagrid the fields for short and long
		/// access are disabled
		/// </summary>
		internal void HandelEventDisableFields() 
		{
			((VehicleModel) mModel).DisableVehicleShortFields();
			((VehicleModel) mModel).DisableVehicleLongFields();
			((VehicleModel) mModel).DisableEnableButton();
		}

		/// <summary>
		/// if short access is not allowed the fields for long 
		/// access are enabled, on the other hand the fields are disabled
		/// </summary>
		/// 
		internal void HandleRadiobuttonsLongAccess()
		{
			((VehicleModel) mModel).HandleEnableDisableFieldsForLongAccess();
		}

		/// <summary>
		/// if long access is not allowed the fields for short 
		/// access are enabled, on the other hand the fields are disabled
		/// </summary>
		/// 
		internal void HandleRadiobuttonsShortAccess()
		{
			((VehicleModel) mModel).HandleEnableDisableFieldsForShortAccess();
		}

		#endregion // End of Methods
	}
}

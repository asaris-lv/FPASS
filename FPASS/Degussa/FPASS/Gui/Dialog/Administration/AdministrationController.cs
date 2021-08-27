using System;
using System.Windows.Forms;
using System.Data;

using Degussa.FPASS.Db.DataSets;
using Degussa.FPASS.Gui;
using Degussa.FPASS.Gui.Dialog.Pass;
using Degussa.FPASS.FPASSApplication;
using Degussa.FPASS.Util.Exceptions;
using Degussa.FPASS.Util.Messages;

using de.pta.Component.Errorhandling;

namespace Degussa.FPASS.Gui.Dialog.Administration
{
	/// <summary>
	/// An AdministrationController is the controller of the 
	/// MVC-triad AdministrationController, AdministrationModel
	/// and FrmAdministration.
	/// AdministrationController extends from the FPASSBaseController.
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
	public class AdministrationController : FPASSBaseController
	{
		#region Members
		
		/// <summary>
		/// Used to determine if a record has been selected in the current UserControl
		/// </summary>
		int mCurrentAdminID = -1;
		

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public AdministrationController()
		{
			initialize();
		}

		#endregion //End of Constructors

		#region Initialization

		/// <summary>
		/// Initializes the members.
		/// Register this controller with the view and model
		/// The USerControls sit on the main form, each has its own typified Dataset, these are instantiated from here
		/// </summary>
		private void initialize()
		{
			
			mDialogId = AllFPASSDialogs.ADMINISTRATION_DIALOG;
			mView = new FrmAdministration();
			mView.RegisterController(this);

			mModel = new AdministrationModel();
			mModel.registerView(mView);

		    TitleMessage = "FPASS - Verwaltung";

			((AdministrationModel)mModel).RegisterExcontractorDataSet(((FrmAdministration)mView).frmUCAdminExternalContractor1.PropDSExContractor);
			((AdministrationModel)mModel).RegisterExcoCoordDataSet(((FrmAdministration)mView).frmUCAdminCoordExco1.PropDSExcoCoord);
			((AdministrationModel)mModel).RegisterPrecMedTypeDataSet(((FrmAdministration)mView).frmUCAdminMedicalPrecautionary1.PropDSPrecMedType);
			((AdministrationModel)mModel).RegisterPlantDataSet(((FrmAdministration)mView).frmUCAdminPlant1.PropDSPlant);
			((AdministrationModel)mModel).RegisterDeptDataSet(((FrmAdministration)mView).frmUCAdminDepartment1.PropDSDepartment);
			((AdministrationModel)mModel).RegisterCraftDataSet(((FrmAdministration)mView).frmUCAdminCraft1.PropDSCraft);

		}	

		#endregion //End of Initialization

		#region Accessors 

		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// Handles closing-events of all dialogs in FPASS.
		/// Forces an controlled close by calling the Close()-Method 
		/// of the current triad if it's an uncontrolled close. 
		/// An uncontrolled close of a form can be fired by a user-click 
		/// on the exit-button on the top-right of a window.
		/// Can't do this reliably: not used 26.11.03
		/// </summary>
		internal override void HandleEventUnControlledClose() 
		{
			
		}

		/// <summary>
		/// Messagebox with YesNoCancel prompts user to save changes, overrides method in
		/// <see cref="Degussa.FPASS.Gui.FPASSBaseController"/>FPASSBaseController
		/// If "cancel" is pressed, throw a warning (ActionCancelledException) to cancel whatever is being processed. 
		/// </summary>
		/// <returns>true if changes should be saved, no to discard changes</returns>
		/// throws
		/// <exception cref="de.pta.Component.Errorhandling.ActionCancelledException">ActionCancelledException</exception>
		/// if user presses "Cancel"
		private bool SaveAdminChangesWished()
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
		/// Prompts user to confirm current record should be deleted
		/// </summary>
		/// <returns>true if delete wished, false if not</returns>
		private bool DoDeleteWished()
		{
			bool flgDelWished = false;

			if ( MessageBox.Show(MessageSingleton.GetInstance().GetMessage(
				MessageSingleton.DELETE_QUESTION), 
				TitleMessage,
				MessageBoxButtons.YesNo, MessageBoxIcon.Question)
				== DialogResult.Yes ) 
			{
				flgDelWished = true;
			}
			return flgDelWished;
		}

		/// <summary>
		/// Specific to an external contractor, if an exco is renamed a new record is created
		/// and all valid coworkers plus coordinators reassigend: confirm this should be carried out
		/// </summary>
		/// <returns>true if action wished, no to cancel action</returns>
		private bool RenameEXCOWished()
		{
			bool flgRenWished = false;

			if ( MessageBox.Show(MessageSingleton.GetInstance().GetMessage(
				MessageSingleton.ADMIN_EXCO_RENAME),
				TitleMessage,
				MessageBoxButtons.YesNo, MessageBoxIcon.Question)
				== DialogResult.Yes ) 
			{
				flgRenWished = true;
			}
			return flgRenWished;
		}
		

		/// <summary>
		/// Specific to assignment excontr. - coordinator, should current assign. be deleted?
		/// </summary>
		/// <returns>true if it should, false if not</returns>
		private bool DoDelAssigenExcoCoord()
		{
			bool flgDelWished = false;

			if ( MessageBox.Show(MessageSingleton.GetInstance().GetMessage(
				MessageSingleton.DEL_ASS_COORDINATOR),
				TitleMessage,
				MessageBoxButtons.YesNo, MessageBoxIcon.Question)
				== DialogResult.Yes ) 
			{
				flgDelWished = true;
			}
			return flgDelWished;
		}


		/// <summary>
		/// Show message in status bar
		/// </summary>
		private void ShowChangesSuccessful()
		{
			((AdministrationModel)mModel).ShowMessageInStatusBar( MessageSingleton.GetInstance().GetMessage(
				MessageSingleton.SAVE_SUCCESS) );
		}

		/// <summary>
		/// Show "Delete was successful" in status bar
		/// </summary>
		private void ShowDeleteSuccessful()
		{
			((AdministrationModel)mModel).ShowMessageInStatusBar( MessageSingleton.GetInstance().GetMessage(
				MessageSingleton.DELETE_SUCCESS) );
		}

		/// <summary>
		/// Show message to confirm action was cancelled
		/// </summary>
		private void ShowActionCancelled()
		{
			((AdministrationModel)mModel).ShowMessageInStatusBar( MessageSingleton.GetInstance().GetMessage(
				MessageSingleton.ACTION_CANCELLED) );
		}

		/// <summary>
		/// Clear status bar
		/// </summary>
		private void ClearSbMessages()
		{
			((AdministrationModel)mModel).ClearStatusBar();
		}

		#endregion  // Methods

		#region PopupExcoCoordHist

		/// <summary>
		/// 30.04.04: New event to open popup form showing history of coordinatorsa current coworker was assigned to
		/// </summary>
		internal void HandleEventPopExcoCoordHist()
		{
			try
			{
				mView.Cursor = Cursors.WaitCursor;
				decimal currExcoID = ((AdministrationModel) mModel).ExcoIDChosenForPopHist();
				if ( currExcoID == 0 )
				{
					throw new UIWarningException( MessageSingleton.GetInstance().GetMessage(
						MessageSingleton.REPORT_NO_EXCONTRACTOR) );
				}
				else
				{
					PopECODHistController popcontroller = (PopECODHistController) FPASSControllSingleton.
						GetInstance().GetDialog(AllFPASSDialogs.POPUP_EXCO_COORD_HIST);
					
					popcontroller.SetCurrentExcoID( currExcoID );
					FPASSControllSingleton.GetInstance().ShowModalDialog ( this, popcontroller );
					mView.Cursor = Cursors.Default;
				}
			}
			catch ( UIWarningException uwe )
			{
				mView.Cursor = Cursors.Default;
				ExceptionProcessor.GetInstance().Process(uwe);
			}
		}

		#endregion


		#region  Cancel_Button_Events


		/// <summary>
		/// Each UserControl has its own "Cancel" button but the action is generic: 
		/// discard all changes and clear all fields
		/// </summary>
		internal void HandleEventBtnCancelClick()
		{		
			this.ClearSbMessages();
			((AdministrationModel)mModel).DiscardAllChanges();
			((AdministrationModel)mModel).SetCurrentAdminIDToDefault();					
		}

		#endregion  

		#region BackButton_Events
		/// <summary>
		/// Each UserControl has its own "Zurück" button, this controller handles their Click events,
		/// Implemented individually for each UserControl but same logic
		/// The user is prompted to save changes if there are any unsaved, the Administration form is then closed
		/// Need to be able to stop the form closing in the case that changes have not been saved
		/// and the user presses "Cancel" when prompted to save
		/// This is the job of the ActionCancelledException: the current action is then aborted
		/// </summary>
		
		
		/// <summary>
		/// React to "Zurück" button pressed for UserControl External Contractor
		/// </summary>
		internal void HandleEventBackBtnExContractor()
		{
			this.ClearSbMessages();
			try
			{
				this.HandleEventTabExContractorExited();
				this.HandleCloseDialog();
			}
			catch ( ActionCancelledException) 
			{
				this.ShowActionCancelled();
			}
		}

		/// <summary>
		/// React to "Zurück" button pressed for UserControl External Contractor - Coordinator assignment
		/// </summary>
		internal void HandleEventBackBtnExcoCoord()
		{
			this.ClearSbMessages();
			try
			{
				this.HandleEventTabExcoCoordExited();
				this.HandleCloseDialog();
			}
			catch ( ActionCancelledException) 
			{
				this.ShowActionCancelled();
			}
		}

		/// <summary>
		/// React to "Zurück" button pressed for UserControl Precautionary Medical
		/// </summary>
		internal void HandleEventBackPrecMedical()
		{
			this.ClearSbMessages();
			try
			{
				this.HandleEventTabPrecMedicalExited();
				this.HandleCloseDialog();
			}
			catch ( ActionCancelledException) 
			{
				this.ShowActionCancelled();
			}
		}

		/// <summary>
		/// React to "Zurück" button pressed for UserControl Plant
		/// </summary>
		internal void HandleEventBackBtnPlant()
		{
			this.ClearSbMessages();
			try
			{
				this.HandleEventTabPlantExited();
				this.HandleCloseDialog();
			}
			catch ( ActionCancelledException) 
			{
				this.ShowActionCancelled();
			}
		}

		/// <summary>
		/// React to "Zurück" button pressed for UserControl Department
		/// </summary>
		internal void HandleEventBackBtnDepartment()
		{
			this.ClearSbMessages();
			try
			{
				this.HandleEventTabDepartmentExited();
				this.HandleCloseDialog();
			}
			catch ( ActionCancelledException) 
			{
				this.ShowActionCancelled();
			}
		}

		/// <summary>
		/// React to "Zurück" button pressed for UserControl Craft
		/// </summary>
		internal void HandleEventBackBtnCraft()
		{
			this.ClearSbMessages();
			try
			{
				this.HandleEventTabCraftExited();
				this.HandleCloseDialog();
			}
			catch ( ActionCancelledException) 
			{
				this.ShowActionCancelled();
			}
		}

		#endregion

		#region Leaving_Tab_Action_Events

		/// <summary>
		/// These events are called every time a new action is started 
		/// (e.g. closing the form or tab, carrying out a new search, creating a new record).
		/// If data has been changed (ask the User Tab) the user is prompted to save, then all fields on the form are emptied
		/// Implemented individually for each UserControl but same logic
		/// </summary>
		/// 

		/// <summary>
		/// UserControl External Contractor: either save or discard changes then clear fields
		/// </summary>
		internal void HandleEventTabExContractorExited()
		{
			if (((FrmAdministration)mView).frmUCAdminExternalContractor1.ContentChanged)
			{
				if (SaveAdminChangesWished()) 
				{
					this.HandleEventBtnSaveTabExContractor();
				}
				else
				{
					((AdministrationModel)mModel).DiscardAllChanges();
				}
			}
			((AdministrationModel)mModel).ClearDataSets();
			((AdministrationModel)mModel).ClearTextFields(true);	
		}

		/// <summary>
		/// UserControl Assignment External Contractor: either save or discard changes then clear fields
		/// </summary>
		internal void HandleEventTabExcoCoordExited()
		{
			if (((FrmAdministration)mView).frmUCAdminCoordExco1.ContentChanged)
			{
				if ( SaveAdminChangesWished() ) 
				{
					this.HandleEventBtnSaveTabCoordExternalCon();
				}
				else
				{
					((AdministrationModel)mModel).DiscardAllChanges();
				}
			}
			((AdministrationModel)mModel).ClearDataSets();
			((AdministrationModel)mModel).ClearTextFields(true);
		}

		/// <summary>
		/// UserControl Precaution Medical: either save or discard changes then clear fields
		/// </summary>
		internal void HandleEventTabPrecMedicalExited()
		{
			if (((FrmAdministration)mView).frmUCAdminMedicalPrecautionary1.ContentChanged)
			{
				if ( SaveAdminChangesWished() ) 
				{
					this.HandleEventBtnSaveTabMedical();
				}
				else
				{
					((AdministrationModel)mModel).DiscardAllChanges();
				}
			}
			((AdministrationModel)mModel).ClearDataSets();
			((AdministrationModel)mModel).ClearTextFields(true);
		}

		/// <summary>
		/// UserControl Plant: either save or discard changes then clear fields
		/// </summary>
		internal void HandleEventTabPlantExited()
		{
			if (((FrmAdministration)mView).frmUCAdminPlant1.ContentChanged)
			{
				if (SaveAdminChangesWished()) 
				{
					this.HandleEventBtnSaveTabPlant();
				}
				else
				{
					((AdministrationModel)mModel).DiscardAllChanges();
				}
			}
			((AdministrationModel)mModel).ClearDataSets();
			((AdministrationModel)mModel).ClearTextFields(true);		
		}

		/// <summary>
		/// UserControl Department: either save or discard changes then clear fields
		/// </summary>
		internal void HandleEventTabDepartmentExited()
		{
			if (((FrmAdministration)mView).frmUCAdminDepartment1.ContentChanged)
			{
				if (SaveAdminChangesWished()) 
				{
					this.HandleEventBtnSaveTabDept();
				}
				else
				{
					((AdministrationModel)mModel).DiscardAllChanges();
				}
			}
			((AdministrationModel)mModel).ClearDataSets();
			((AdministrationModel)mModel).ClearTextFields(true);	
		}

		/// <summary>
		/// UserControl Craft: either save or discard changes then clear fields
		/// </summary>
		internal void HandleEventTabCraftExited()
		{
			if (((FrmAdministration)mView).frmUCAdminCraft1.ContentChanged)
			{
				if (SaveAdminChangesWished()) 
				{
					this.HandleEventBtnSaveTabCraft();
				}	
				else
				{
					((AdministrationModel)mModel).DiscardAllChanges();
				}
			}
			((AdministrationModel)mModel).ClearDataSets();
			((AdministrationModel)mModel).ClearTextFields(true);	
		}


		#endregion 

		#region New_Button_Events

		/// <summary>
		/// Reaction to button "New", each UserControl has its own button.
		/// Reset form to default state then enable the relevant fields so new records can be created
		/// Implemented individually for each UserControl but same logic
		/// ActionCancelledException is used to abort the current action.
		/// </summary>

		/// <summary>
		/// Must be implemented since defined in abstract class
		/// </summary>
		internal void HandleEventBtnNewClick()
		{
		}

		/// <summary>
		/// UserControl External Contractor
		/// </summary>
		internal void HandleEventBtnNewExContractorClick()
		{
			try
			{
				this.ClearSbMessages();
				this.HandleEventTabExContractorExited();
				((AdministrationModel)mModel).SetCurrentAdminIDToDefault();
				((AdministrationModel)mModel).CreateNewExContractor();
			}
			catch ( ActionCancelledException) 
			{
				this.ShowActionCancelled();
			}
		}

		/// <summary>
		/// UserControl Assignment External Contractor
		/// </summary>
		internal void HandleEventBtnNewExcoCoordClick()
		{
			try
			{
				this.ClearSbMessages();
				this.HandleEventTabExcoCoordExited();
				((AdministrationModel)mModel).SetCurrentAdminIDToDefault();
				((AdministrationModel)mModel).CreateAssignExcoCoord();
			}
			catch ( ActionCancelledException) 
			{
				this.ShowActionCancelled();
			}
		}

		/// <summary>
		/// UserControl Precautionary Medical
		/// </summary>
		internal void HandleEventBtnNewPrecMedClick()
		{
			try
			{
				this.ClearSbMessages();
				this.HandleEventTabPrecMedicalExited();
				((AdministrationModel)mModel).SetCurrentAdminIDToDefault();
				((AdministrationModel)mModel).CreateNewPrecMedical();
			}
			catch ( ActionCancelledException) 
			{
				this.ShowActionCancelled();
			}
		}

		/// <summary>
		/// UserControl Plant
		/// </summary>
		internal void HandleEventBtnNewPlantClick()
		{
			try
			{
				this.ClearSbMessages();
				this.HandleEventTabPlantExited();
				((AdministrationModel)mModel).SetCurrentAdminIDToDefault();
				((AdministrationModel)mModel).CreateNewPlant();
			}
			catch ( ActionCancelledException) 
			{
				this.ShowActionCancelled();
			}
		}

		/// <summary>
		/// UserControl Department
		/// </summary>
		internal void HandleEventBtnNewDeptClick()
		{
			try
			{
				this.ClearSbMessages();
				this.HandleEventTabDepartmentExited();
				((AdministrationModel)mModel).SetCurrentAdminIDToDefault();
				((AdministrationModel)mModel).CreateNewDepartment();
			}
			catch ( ActionCancelledException) 
			{
				this.ShowActionCancelled();
			}
		}

		/// <summary>
		/// UserControl Craft
		/// </summary>
		internal void HandleEventBtnNewCraftClick()
		{
			try
			{
				this.ClearSbMessages();
				this.HandleEventTabCraftExited();
				((AdministrationModel)mModel).SetCurrentAdminIDToDefault();
				((AdministrationModel)mModel).CreateNewCraft();
			}
			catch ( ActionCancelledException) 
			{
				this.ShowActionCancelled();
			}
		}

		#endregion 

		#region Save_Button_Events

		/// <summary>
		/// Reaction to button "Save", implemented for each UserControl
		/// Ask if UserControl's content has changed, if not do nothing
		/// Differentiate in this Controller between a new recoed (INSERT)
		/// and existing record (UPDATE) by asking the form for PK of current record
		/// After successful save the combobox values (Componente ListOfValues) are refilled
		/// </summary>

		/// <summary>
		/// Save External Contractor
		/// If PK of current record is -1 it's an insert of a new record
		/// If it's an update and name of current ExContractor has been changed, create new record with new credentials
		/// and update old record (workings in model)
		/// If name has not been changed it's a normal update
		/// </summary>
		/// <exception cref="de.pta.Component.ErrorHandling.UIWarningException">UIWarningException</exception>thrown in Model 
		/// if no record selected for update or for database concurreny problems
		/// 15.01.04, 29.03.04: if Administration called from CoWorker form then must reload coworker data
		internal void HandleEventBtnSaveTabExContractor()
		{
			string insNewName;
			this.ClearSbMessages();
			try
			{
				if ( ((FrmAdministration)mView).frmUCAdminExternalContractor1.ContentChanged )
				{				
					insNewName	= ((FrmAdministration) mView).frmUCAdminExternalContractor1.TxtEditExternalContractor.Text;
					
					if ( ((FrmAdministration)mView).frmUCAdminExternalContractor1.CurrentAdminRec == -1 )
					{
					    // It's an insert
						((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.WaitCursor;
						decimal dret = ((AdministrationModel)mModel).SaveNewExContractor();	
                        ((AdministrationModel)mModel).LoadIndividualExContractor();

						((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.Default;						
						this.ShowChangesSuccessful();
					}
					else
					{				
						// If firm has been renamed, create a new record					
						if ( !((FrmAdministration) mView).frmUCAdminExternalContractor1.CurrentEXCOName.Equals(insNewName) ) 
						{
							if ( this.RenameEXCOWished() )
							{
								((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.WaitCursor;

								// 26.05.04: Make sure exco about to become invalid isnt going to clash with an already invalid exco (name of)
								((AdministrationModel)mModel).CheckExistenceOfInvalidExco( ((FrmAdministration) mView).frmUCAdminExternalContractor1.CurrentEXCOName );
								
								// If all is OK then rename EXCO
								((AdministrationModel)mModel).RenameExContractor();

								((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.Default;
								this.ShowChangesSuccessful();
							}
						}
						else
						{
							// Normal update
							((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.WaitCursor;
							((AdministrationModel)mModel).SaveChangesExContractor();
							((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.Default;
							this.ShowChangesSuccessful();
						}
					}
					// Refill all LOV lists
					FPASSControllSingleton.GetInstance().ReInitializeListOfValues();
					
					// Reload coworker cbxs if come from there
					if ( mParent.GetType().Equals(typeof(CoWorkerController)) )
					{
						((CoWorkerController) mParent).RefreshMyLists();
					}
				}
			}
			catch (UIWarningException uwe)
			{
				((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.Default;
				ExceptionProcessor.GetInstance().Process(uwe);
			}
		}

		/// <summary>
		/// Save new Assigments External Contractor - Coordinator (no updates to existing records)
		/// </summary>
		/// <exception cref="de.pta.Component.ErrorHandling.UIWarningException">UIWarningException</exception>thrown in Model 
		/// if no record selected for update or for database concurreny problems
		internal void HandleEventBtnSaveTabCoordExternalCon()
		{
			this.ClearSbMessages();
			try
			{
				((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.WaitCursor;
				((AdministrationModel)mModel).SaveNewAssignmentExCoCoord();

				// Refill all LOV lists except coworker
				FPASSControllSingleton.GetInstance().ReInitializeListOfValues();
				// Reload coworker cbxs if come from there
				if ( mParent.GetType().Equals(typeof(CoWorkerController)) )
				{
					((CoWorkerController) mParent).RefreshMyLists();
				}

				((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.Default;
				this.ShowChangesSuccessful();
			}
			catch (UIWarningException uwe)
			{
				((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.Default;
				ExceptionProcessor.GetInstance().Process(uwe);
			}
		}

		/// <summary>
		/// UserControl Prec Medical, save new record or update existing one
		/// </summary>
		/// <exception cref="de.pta.Component.ErrorHandling.UIWarningException">UIWarningException</exception>thrown in Model 
		/// if no record selected for update or for database concurreny problems
		internal void HandleEventBtnSaveTabMedical()
		{
			this.ClearSbMessages();
			try
			{
				if ( ((FrmAdministration)mView).frmUCAdminMedicalPrecautionary1.ContentChanged )
				{
					((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.WaitCursor;
					if ( ((FrmAdministration)mView).frmUCAdminMedicalPrecautionary1.CurrentAdminRec == -1 )
					{
						((AdministrationModel)mModel).SaveNewMedical();
						((FrmAdministration)mView).frmUCAdminMedicalPrecautionary1.CurrentAdminRec = -1;
					}
					else
					{
						((AdministrationModel)mModel).SaveChangesMedical();
					}
					// Refill all LOV lists
					FPASSControllSingleton.GetInstance().ReInitializeListOfValues();
					
					((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.Default;
					this.ShowChangesSuccessful();
				}
			}
			catch (UIWarningException uwe)
			{
				((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.Default;
				ExceptionProcessor.GetInstance().Process(uwe);
			}
		}

		/// <summary>
		/// UserControl Plant, save new record or update existing one
		/// </summary>
		/// <exception cref="de.pta.Component.ErrorHandling.UIWarningException">UIWarningException</exception>thrown in Model 
		/// if no record selected for update or for database concurreny problems
		internal void HandleEventBtnSaveTabPlant()
		{
			this.ClearSbMessages();
			try
			{
				if ( ((FrmAdministration)mView).frmUCAdminPlant1.ContentChanged )
				{
					((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.WaitCursor;
					if (((FrmAdministration)mView).frmUCAdminPlant1.CurrentAdminRec == -1)
					{
						((AdministrationModel)mModel).SaveNewPlant();
					}
					else
					{
						((AdministrationModel)mModel).SaveChangesPlant();
					}
					// Refill all LOV lists
					FPASSControllSingleton.GetInstance().ReInitializeListOfValues();
					
					((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.Default;
					this.ShowChangesSuccessful();
				}
			}
			catch (UIWarningException uwe)
			{
				((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.Default;
				ExceptionProcessor.GetInstance().Process(uwe);
			}
		}

		/// <summary>
		/// UserControl Department, save new record or update existing one
		/// </summary>
		/// <exception cref="de.pta.Component.ErrorHandling.UIWarningException">UIWarningException</exception>thrown in Model 
		/// if no record selected for update or for database concurreny problems
		internal void HandleEventBtnSaveTabDept()
		{
			this.ClearSbMessages();
			try
			{
				if (((FrmAdministration)mView).frmUCAdminDepartment1.ContentChanged )
				{
					((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.WaitCursor;
					if (((FrmAdministration)mView).frmUCAdminDepartment1.CurrentAdminRec == -1)
					{	
						// It's an insert.
						((AdministrationModel)mModel).SaveNewDepartment();
					}
					else
					{
						// It's an update
						((AdministrationModel)mModel).SaveChangesDepartment();
					}
					// Refill all LOV lists
					FPASSControllSingleton.GetInstance().ReInitializeListOfValues();
					
					((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.Default;
					this.ShowChangesSuccessful();
				}
			}
			catch (UIWarningException uwe)
			{
				((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.Default;
				ExceptionProcessor.GetInstance().Process(uwe);
			}			
		}

		/// <summary>
		/// UserControl Craft, save new record or update existing one
		/// </summary>
		/// <exception cref="de.pta.Component.ErrorHandling.UIWarningException">UIWarningException</exception>thrown in Model 
		/// if no record selected for update or for database concurreny problems
		internal void HandleEventBtnSaveTabCraft()
		{
			this.ClearSbMessages();
			try 
			{
				if (((FrmAdministration)mView).frmUCAdminCraft1.ContentChanged )
				{
					((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.WaitCursor;
					if (((FrmAdministration)mView).frmUCAdminCraft1.CurrentAdminRec == -1)
					{
						// It's an insert.
						((AdministrationModel)mModel).SaveNewCraft();
					}
					else
					{
						// It's an update
						((AdministrationModel)mModel).SaveChangesCraft();
					}
					// Refill all LOV lists
					FPASSControllSingleton.GetInstance().ReInitializeListOfValues();
					
					((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.Default;
					this.ShowChangesSuccessful();
				}
			}
			catch (UIWarningException uwe)
			{
				((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.Default;
				ExceptionProcessor.GetInstance().Process(uwe);
			}	
		}

		#endregion 

		#region Delete_Button_Events
		/// <summary>
		/// Events for Delete Buttons, each UserControl has its own button, logic is the same for each
		/// Prompt if delete wished, delete record and refill comboboxes
		/// </summary>
 
		/// <summary>
		/// UserControl External Contractor
		/// </summary>
		/// <exception cref="de.pta.Component.ErrorHandling.UIWarningException">UIWarningException</exception>thrown  
		/// if no record selected or for error if dependent data exist
		internal void HandleEventBtnDeleteTabExternalCon()
		{
			this.ClearSbMessages();

			mCurrentAdminID = ((FrmAdministration) mView).frmUCAdminExternalContractor1.CurrentAdminRec;
			try
			{
				if (mCurrentAdminID == -1)
				{
					throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(
						MessageSingleton.NO_ADMIN_ROW ));
				}
				else if ( DoDeleteWished() )
				{
					((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.WaitCursor;
					((AdministrationModel)mModel).DeleteExternalContractor();
					// Refill all LOV lists
					FPASSControllSingleton.GetInstance().ReInitializeListOfValues();
					
					((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.Default;
					this.ShowDeleteSuccessful();
				}
			}
			catch (UIWarningException uwe)
			{
				((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.Default;
				ExceptionProcessor.GetInstance().Process(uwe);
			}		
		}

		/// <summary>
		/// UserControl Assignment External Contractor
		/// Requires 2 IDs: ID of coordinator and ExContractor currently selected in grid
		/// </summary>
		/// <exception cref="de.pta.Component.ErrorHandling.UIWarningException">UIWarningException</exception>thrown  
		/// if no record selected or for error if dependent data exist
		/// 29.03.04: if Administration called from CoWorker form then must reload coworker data
		internal void HandleEventBtnDeleteTabCoordExternalCon()
		{
			this.ClearSbMessages();			
			int myCOORDVal = ((FrmAdministration) mView).frmUCAdminCoordExco1.CurrentCoordinatorID;
			int myEXCOVal  = ((FrmAdministration) mView).frmUCAdminCoordExco1.CurrentEXCORec;			
	
			try
			{
				if (myEXCOVal == -1 || myCOORDVal == -1)
				{
					throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(
						MessageSingleton.NO_ADMIN_ROW ));
				}
				else if ( this.DoDelAssigenExcoCoord() )
				{
					((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.WaitCursor;
					((AdministrationModel)mModel).DeleteCoordinatorExternalContractor();

					// Refill all LOV lists
					FPASSControllSingleton.GetInstance().ReInitializeListOfValues();
					
					if ( mParent.GetType().Equals(typeof(CoWorkerController) ) ) 
					{
						// must reload current coworker data
						((CoWorkerController) mParent).RefreshMyLists();						 
					} 
					((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.Default;
					this.ShowDeleteSuccessful();
				}
			}
			catch (UIWarningException uwe)
			{
				((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.Default;
				ExceptionProcessor.GetInstance().Process(uwe);
			}
		}

		/// <summary>
		/// UserControl Precaution Medical
		/// </summary>
		/// <exception cref="de.pta.Component.ErrorHandling.UIWarningException">UIWarningException</exception>thrown 
		/// if no record selected or for error if dependent data exist
		internal void HandleEventBtnDeleteTabMedical()
		{
			this.ClearSbMessages();
			try
			{
				mCurrentAdminID = ((FrmAdministration) mView).frmUCAdminMedicalPrecautionary1.CurrentAdminRec;
				if (mCurrentAdminID == -1)
				{
					throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(
						MessageSingleton.NO_ADMIN_ROW ));
				}
				else if ( this.DoDeleteWished() )
				{		
					((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.WaitCursor;
					((AdministrationModel)mModel).DeleteMedical();
					// Refill all LOV lists
					FPASSControllSingleton.GetInstance().ReInitializeListOfValues();
					
					((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.Default;
					this.ShowDeleteSuccessful();
				}
			}
			catch (UIWarningException uwe)
			{
				((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.Default;
				ExceptionProcessor.GetInstance().Process(uwe);
			}
		}

		/// <summary>
		/// UserControl Plant
		/// </summary>
		/// <exception cref="de.pta.Component.ErrorHandling.UIWarningException">UIWarningException</exception>thrown 
		/// if no record selected or for error if dependent data exist
		internal void HandleEventBtnDeleteTabPlant()
		{
			this.ClearSbMessages();
			try
			{
				mCurrentAdminID = ((FrmAdministration) mView).frmUCAdminPlant1.CurrentAdminRec;
				if (mCurrentAdminID == -1)
				{
					throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(
						MessageSingleton.NO_ADMIN_ROW ));
				}
				else if ( this.DoDeleteWished() )
				{	
					((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.WaitCursor;
					((AdministrationModel)mModel).DeletePlant();
					
					// Refill all LOV lists
					FPASSControllSingleton.GetInstance().ReInitializeListOfValues();
				
					((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.Default;
					this.ShowDeleteSuccessful();
				}
			}
			catch (UIWarningException uwe)
			{
				((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.Default;
				ExceptionProcessor.GetInstance().Process(uwe);
			}
		}

		/// <summary>
		/// UserControl Department
		/// </summary>
		/// <exception cref="de.pta.Component.ErrorHandling.UIWarningException">UIWarningException</exception>thrown 
		/// if no record selected or for error if dependent data exist
		internal void HandleEventBtnDeleteTabDept()
		{
			this.ClearSbMessages();
			try
			{
				this.mCurrentAdminID = ((FrmAdministration) mView).frmUCAdminDepartment1.CurrentAdminRec;
				if (mCurrentAdminID == -1)
				{
					throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(
						MessageSingleton.NO_ADMIN_ROW ));
				}
				else if ( this.DoDeleteWished() )
				{
					((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.WaitCursor;
					((AdministrationModel)mModel).DeleteDepartment();
					// Refill all LOV lists
					FPASSControllSingleton.GetInstance().ReInitializeListOfValues();
					
					((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.Default;
					this.ShowDeleteSuccessful();
				}
			}
			catch (UIWarningException uwe)
			{
				((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.Default;
				ExceptionProcessor.GetInstance().Process(uwe);
			}	
		}
	
		/// <summary>
		/// UserControl Craft
		/// </summary>
		/// <exception cref="de.pta.Component.ErrorHandling.UIWarningException">UIWarningException</exception>thrown 
		/// if no record selected or for error if dependent data exist
		internal void HandleEventBtnDeleteTabCraft()
		{
			this.ClearSbMessages();
			try
			{
				mCurrentAdminID = ((FrmAdministration) mView).frmUCAdminCraft1.CurrentAdminRec;	
				if (mCurrentAdminID == -1)
				{
					throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(
						MessageSingleton.NO_ADMIN_ROW ));
				}
				else if ( this.DoDeleteWished() )
				{
					((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.WaitCursor;
					((AdministrationModel)mModel).DeleteCraft();
					// Refill all LOV lists
					FPASSControllSingleton.GetInstance().ReInitializeListOfValues();
					
					((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.Default;
					this.ShowDeleteSuccessful();
				}
			}
			catch (UIWarningException uwe)
			{
				((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.Default;
				ExceptionProcessor.GetInstance().Process(uwe);
			}
		}

		#endregion 

		#region Table_Navigate_Events

		/// <summary>
		/// Reaction to navigation (selection of a record) in datagrid in current UserControl
		/// Current record in the dataset is loaded into textfields on base of UserControl for editing
		/// Implemented for each UserControl but same logic, each UserControl has its own typified dataset
		/// </summary>

		
		/// <summary>
		/// Load individual ExContractor
		/// If the current ExCo is invalid (i.e.) after being renamed, generate error and do not allow editing
		/// (invalid exocs currently not shown in search 10.12.2003)
		/// </summary>
		/// <exception cref="de.pta.Component.ErrorHandling.UIWarningException">UIWarningException</exception>thrown in Model 
		/// if selected ExCo is invalid
		internal void HandleEventDgrNavigateTabExCon()
		{	
			try
			{
				((AdministrationModel)mModel).ClearTextFields(false);
				((AdministrationModel)mModel).LoadIndividualExContractor();
			}
			catch (UIWarningException uwe)
			{
				if ( uwe.Message.IndexOf(
						MessageSingleton.GetInstance().GetMessage(
							MessageSingleton.ADMIN_EXCO_INVALID))
					!= -1 )
				{
					((AdministrationModel)mModel).ShowMessageInStatusBar( uwe.Message );
				}
				else
				{
					ExceptionProcessor.GetInstance().Process(uwe);
				}
			}
		}

		/// <summary>
		/// Load individual assignments ("Zuordnung Koord", after selecting row in datagrid
		/// </summary>
		internal void HandleEventDgrNavigateTabExConCoord()
		{	
			((AdministrationModel)mModel).LoadIndividualExcoCoordinators();
		}

		/// <summary>
		/// Load individual Precaution Medical
		/// </summary>
		internal void HandleEventDgrNavigateTabMedical()
		{	
			((AdministrationModel)mModel).ClearTextFields(false);
			((AdministrationModel)mModel).LoadIndividualPrecMed();
		}

		/// <summary>
		/// Load individual plant ("Betrieb")
		/// </summary>
		internal void HandleEventDgrNavigateTabPlant()
		{	
			((AdministrationModel)mModel).ClearTextFields(false);
			((AdministrationModel)mModel).LoadIndividualPlant();
		}

		/// <summary>
		/// Load individual dept ("Abteilung")
		/// </summary>
		internal void HandleEventDgrNavigateTabDept()
		{	
			((AdministrationModel)mModel).ClearTextFields(false);
			((AdministrationModel)mModel).LoadIndividualDepartment();
		}

		/// <summary>
		/// Load individual craft ("Gewerk")
		/// </summary>
		internal void HandleEventDgrNavigateTabCraft()
		{
			((AdministrationModel)mModel).ClearTextFields(false);
			((AdministrationModel)mModel).LoadIndividualCraft();
		}

		#endregion 

		#region Search_Button_Events

		/// <summary>
		/// Reaction to clicking Search Button, each User Control has its own button and HandleEvent method below
		/// Prompt user to save changes if content has changed
		/// Logic is same for each
		/// </summary>
	    /// <exception cref="de.pta.Component.ErrorHandling.UIWarningException">UIWarningException</exception>thrown in Model 
		/// if no results returned
		/// <exception cref="Degussa.FPASS.Util.Exceptions.ActionCancelledException">ActionCancelledException</exception>
		/// if save prompt was cancel, abort and don't carry out search

		/// <summary>
		/// Get the external contractors that meet the search criteria ("Fremdfirma")
		/// </summary>		
		internal void HandleEventBtnSearchTabExCon()
		{
			this.ClearSbMessages();
			
			try
			{	
				this.HandleEventTabExContractorExited();
				((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.WaitCursor;
				((AdministrationModel)mModel).SetCurrentAdminIDToDefault();
				((AdministrationModel)mModel).GetExternalContractors(true);
				((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.Default;
			} 
			catch ( UIWarningException uwe ) 
			{
				((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.Default;
				ExceptionProcessor.GetInstance().Process(uwe);
			}
			catch ( ActionCancelledException) 
			{
				this.ShowActionCancelled();
			}
		}

		/// <summary>
		/// Gets the external contractors & Coords that meet the search criteria ("Zuordnung Koord..")
		/// </summary>
		internal void HandleEventBtnSearchTabExConCoord()
		{
			this.ClearSbMessages();		
			try 
			{
				this.HandleEventTabExcoCoordExited();
				((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.WaitCursor;
				((AdministrationModel)mModel).SetCurrentAdminIDToDefault();
				((AdministrationModel)mModel).GetExtContractorsCoords();
				((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.Default;
			}
			catch ( UIWarningException uwe ) 
			{
				((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.Default;
				ExceptionProcessor.GetInstance().Process(uwe);
			}
			catch ( ActionCancelledException) 
			{
				this.ShowActionCancelled();
			}
		}

		/// <summary>
		/// Get the precaut. medicals that meet the search criteria ("Vorsorgeunt...")
		/// </summary>
		internal void HandleEventBtnSearchTabPrecMedical()
		{
			this.ClearSbMessages();
			
			try
			{
				this.HandleEventTabPrecMedicalExited();
				((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.WaitCursor;
				((AdministrationModel)mModel).SetCurrentAdminIDToDefault();
				((AdministrationModel)mModel).GetPrecautionaryMedicals();
				((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.Default;
			} 
			catch ( UIWarningException uwe ) 
			{
				((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.Default;
				ExceptionProcessor.GetInstance().Process(uwe);
			}
			catch ( ActionCancelledException) 
			{
				this.ShowActionCancelled();
			}
		}

		/// <summary>
		/// Get the plants that meet the search criteria ("Betrieb")
		/// </summary>
		internal void HandleEventBtnSearchTabPlant()
		{		
			this.ClearSbMessages();			
			try
			{
				this.HandleEventTabPlantExited();
				((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.WaitCursor;
				((AdministrationModel)mModel).SetCurrentAdminIDToDefault();
				((AdministrationModel)mModel).GetPlants();
				((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.Default;
			} 
			catch ( UIWarningException uwe ) 
			{
				((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.Default;
				ExceptionProcessor.GetInstance().Process(uwe);
			}
			catch ( ActionCancelledException) 
			{
				this.ShowActionCancelled();
			}
		}

		/// <summary>
		/// Get the departments that meet the search criteria ("Abteilung")
		/// </summary>
		internal void HandleEventBtnSearchTabDept()
		{ 
			this.ClearSbMessages();			
			try 
			{			
				this.HandleEventTabDepartmentExited();
				((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.WaitCursor;
				((AdministrationModel)mModel).SetCurrentAdminIDToDefault();
				((AdministrationModel)mModel).GetDepartments();
				((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.Default;
			} 
			catch ( UIWarningException uwe ) 
			{
				((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.Default;
				ExceptionProcessor.GetInstance().Process(uwe);
			}
			catch ( ActionCancelledException) 
			{
				this.ShowActionCancelled();
			}
		}

		/// <summary>
		/// Get the crafts that meet the search criteria ("Gewerk", button "Suchen")
		/// </summary>
		internal void HandleEventBtnSearchTabCraft()
		{
			this.ClearSbMessages();						
			try 
			{
				this.HandleEventTabCraftExited();
				((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.WaitCursor;
				((AdministrationModel)mModel).SetCurrentAdminIDToDefault();
				((AdministrationModel)mModel).GetCrafts();
				((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.Default;
			} 
			catch ( UIWarningException uwe ) 
			{
				((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.Default;
				ExceptionProcessor.GetInstance().Process(uwe);
			}
			catch ( ActionCancelledException) 
			{
				this.ShowActionCancelled();
			}
		}
		
		#endregion 

		#region Call_Form_Search_Excontractor

		/// <summary>
		/// Call Form SearchExcontractor to find a particular ExContractor.
		/// Can be called either from UserControl ExContractor or CoordinatorExContractor
		/// </summary>
		/// <param name="PControlID">ID of user control</param>
		internal void HandleEventCallFormSearchExContractor(int PControlID)
		{
			((FrmAdministration) mView).CalledSearchContractor = PControlID;
		}


		#endregion 

		#region Show_Help

		/// <summary>
		/// Override base method
		/// Call help topic specific to Administration
		/// </summary>
		internal override void HandleEventShowHelp() 
		{
			Help.ShowHelp(this.mView, 
				Application.StartupPath + AllFPASSDialogs.FPASS_MASTER_HELPFILE, 
				HelpNavigator.Topic,
				AllFPASSDialogs.HELPTOPIC_ADMIN );
		}

		#endregion 
	}
}

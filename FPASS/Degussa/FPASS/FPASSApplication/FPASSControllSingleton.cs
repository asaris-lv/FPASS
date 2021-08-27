using System;
using System.Collections;
using System.Windows.Forms;

using Degussa.FPASS.Util.UserManagement;
using Degussa.FPASS.Util.ListOfValues;
using Degussa.FPASS.Util.Exceptions;

using Degussa.FPASS.Reports;
using Degussa.FPASS.Gui;
using Degussa.FPASS.Gui.Mandator;
using Degussa.FPASS.Gui.Dialog.Pass;
using Degussa.FPASS.Gui.Dialog.Administration;
using Degussa.FPASS.Gui.Dialog.User;

using de.pta.Component.ListOfValues;
using de.pta.Component.Errorhandling;
using de.pta.Component.Common;
using Degussa.FPASS.Util.SmartAct;
using de.pta.Component.Logging.Log4NetWrapper;
using Degussa.FPASS.Util.ErrorHandling;
using Degussa.FPASS.Gui.Dialog.SmartAct;

namespace Degussa.FPASS.FPASSApplication
{
	/// <summary>
	/// Controls the application.
	/// Provides access to all dialogs of FPASS.
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
	public class FPASSControllSingleton
	{
		#region Members

		/// <summary>  
		/// used to hold the unique instance of FPASSControlSingleton
		///</summary>
		private	static FPASSControllSingleton mInstance = null;

		/// <summary>  
		/// Holds all dialogs which have already been displayed 
		///</summary>
		private Hashtable mDialogs;
		
        /// <summary>
        /// Instance of IdCardPoller used to control background process 
        /// to monitor CWR with IdCards from SmartAct
        /// </summary>
        private IdCardPoller mIdCardPoller;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Private constructor for singleton implementation, to control instantiation.
		/// </summary>
		private FPASSControllSingleton()
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
			mDialogs = new Hashtable();
		}	

		#endregion //End of Initialization


		#region Accessors 

        /// <summary>
        /// Returns Instance of IdCardPoller used to control background process 
        /// to monitor CWR with IdCards from SmartAct.
        /// Not thread-safe but does not need to be.
        /// </summary>
        public IdCardPoller IdCardPoller
        {
            get 
            {
                if (mIdCardPoller == null) mIdCardPoller = new IdCardPoller();
                return mIdCardPoller; 
            }
        } 

		#endregion 


		#region Methods 

		/// <summary>
		/// Returns the one and only instance of this class.
		/// </summary>
		/// <returns>instance of FPASSControllSingleton</returns>
		public static FPASSControllSingleton GetInstance() 
		{
			if ( null == mInstance ) 
			{
				mInstance = new FPASSControllSingleton();
			}
			return mInstance;
		}

		/// <summary>
		/// Verifies if the current windows user is authorized to use fpass
		/// </summary>
		/// <exception cref="de.pta.Component.Errorhandling.UIFatalException"> 
		/// is thrown if current user is not assigned to a valid fpass mandator, if an um error
		/// occured and if a database error occured</exception>
		/// <returns>true if user is authorized to use fpass, false otherwise</returns>
		internal bool CheckUser() 
		{
            
			try 
			{
				UserManagementControl.getInstance().ReadMandator();
			} 
			catch ( UIFatalException uife ) 
			{
				throw uife;
			}


			try {
				if ( UserManagementControl.getInstance().NumberOfMandators > 1 ) 
				{
					this.ShowMandatorDialog();
				}


				if ( UserManagementControl.getInstance().
					NumberOfMandators == 1 ) 
				{
					UserManagementControl.getInstance().VerifyLoginUser();
					UserManagementControl.getInstance().InitializeMandatorDependentUsermanagement();

					return true;
				} 
				else 
				{
					return false;
				}
			} 
			catch ( Exception e ) 
			{
				throw new UIFatalException("Fehler bei der Anmeldung: " + e.Message, e);
			}
		}

		/// <summary>
		/// Closes the application. Forces controlled close of the current dialog and all
		/// other hidden/covered dialogs.
		/// </summary>
		/// <param name="pController">controller of the current dialog</param>
		public void CloseApplication(AbstractController pController) 
		{
			if ( null != pController ) 
			{
				pController.Exit();
			}
			System.Windows.Forms.Application.Exit();
		}

		/// <summary>
		/// Shows the dialog as a modal dialog identified by the param pNextDialoId.
		/// Calls trigger-methods of the calling and the next dialog
		/// </summary>
		/// <param name="pCallingDialog"> controller of the current dialog</param>
		/// <param name="pNextDialog">controller of the dialog which is the next to display. 
		/// Unique id's are held in the class AllFPASSDialogs</param>
		public void ShowModalDialog(AbstractController pCallingDialog, AbstractController pNextDialog) 
		{
			pNextDialog.Parent = pCallingDialog;
			pCallingDialog.Hide();
			pNextDialog.ShowDialog();
		}

		/// <summary>
		/// Shows the dialog as a modal dialog identified by the param pNextDialoId.
		/// Calls trigger-methods of the calling and the next dialog.
		/// </summary>
		/// <param name="pCallingDialog"> controller of the current dialog</param>
		/// <param name="pNextDialogID">id of the dialog which is the next to display. 
		/// Unique id's are held in the class AllFPASSDialogs</param>
		public void ShowModalDialog(AbstractController pCallingDialog, int pNextDialogID) 
		{
			AbstractController NextDialog = GetDialog(pNextDialogID);
			NextDialog.Parent = pCallingDialog;
			
			pCallingDialog.Hide();
			NextDialog.ShowDialog();
		}


        /// <summary>
        /// Shows the dialog as a modal dialog identified by the param pNextDialoId.
        /// Calls trigger-methods of the calling and the next dialog.
        /// But will only show next dialog if given condition is met
        /// </summary>
        /// <param name="pCallingDialog"> controller of the current dialog</param>
        /// <param name="pNextDialogID">id of the dialog which is the next to display. 
        /// Unique id's are held in the class AllFPASSDialogs</param>
        public void ShowModalDialogCondition(AbstractController pCallingDialog, int pNextDialogID)
        {
            AbstractController NextDialog = GetDialog(pNextDialogID);
            NextDialog.Parent = pCallingDialog;

            // Re-queries model, allowing som update method to run if necessary
            NextDialog.RequeryModel();

            if (NextDialog.ConditionIsMet())
            {
                NextDialog.ShowDialog();
            }
        }


		/// <summary>
		/// Closes the given dialog.
		/// </summary>
		/// <param name="pCallingDialog">controller of the current dialog</param>
		public void CloseDialog(AbstractController pCallingDialog) 
		{
			pCallingDialog.Close();
		}

		/// <summary>
		/// Destroys the given dialog and frees all ressources used by this dialog.
		/// </summary>
		/// <param name="pCallingDialog">controller of the current dialog</param>
		public void DestroyDialog(AbstractController pCallingDialog) 
		{
			pCallingDialog.Destroy();
			mDialogs.Remove(pCallingDialog.DialogId);
			pCallingDialog = null;
			GC.Collect();
		}


		/// <summary>
		/// Gets the controller of the requested dialog. 
		/// </summary>
		/// <param name="pDialogID">Id of the dialog which is requested. 
		/// Unique id's are held in the <see cref="Degussa.FPASS.FPASSApplication">
		/// <code>AllFPASSDialogs</code></see> object.
		/// <returns>instance of AbstractController which was requested</returns>
		public AbstractController GetDialog(int pDialogID) 
		{	
			AbstractController	controller;
			controller = (AbstractController)mDialogs[pDialogID];
			
			if ( null ==  controller ) 
			{
				controller = CreateController(pDialogID);
			}
			return controller;
		}


		/// <summary>
		/// Fills all Comboboxes, Listboxes, etc. with up-to-date values.
		/// </summary>
		private void FillLists() 
		{
			IDictionaryEnumerator dialogsEnumerator = mDialogs.GetEnumerator();
			while ( dialogsEnumerator.MoveNext() ) 
			{
				String s = ((AbstractController)dialogsEnumerator.Value).ToString();
				((AbstractController)dialogsEnumerator.Value).FillLists();
			}
		}
		
		/// <summary>
		/// Reinitializes the ListOfValues Component. This must be done to read up-to-date values
		/// for all Comboboxes, Listboxes, etc.
		/// </summary>
		public void ReInitializeListOfValues() 
		{
			FPASSLovsSingleton.GetInstance().ReFill();
			LovSingleton.GetInstance().SetSqlRestriction(
				UserManagementControl.getInstance().CurrentMandatorID.ToString() );
			LovSingleton.GetInstance().ReadConfiguration();
			FillLists();
		}


		/// <summary>
		/// Factory method which is responsible to create all FPASS dialogs
		/// </summary>
		/// <param name="pDialogID">id of the dialog which is requested. 
		/// Unique id's are held in the <see cref="Degussa.FPASS.FPASSApplication">
		/// <code>AllFPASSDialogs</code></see> object.
		/// <returns>instance of AbstractController which was requested</returns>
		private AbstractController CreateController( int pDialogID ) 
		{
			AbstractController controller;

			if ( pDialogID.Equals(AllFPASSDialogs.SUMMARY_COWORKER_DIALOG) ) 
			{
				controller = new SummaryController();
			} 
			else if ( pDialogID.Equals(AllFPASSDialogs.COWORKER_PROCESS_DIALOG) ) 
			{
				controller = new CoWorkerController();
			} 
			else if ( pDialogID.Equals(AllFPASSDialogs.COWORKER_DELETE_DIALOG) ) 
			{
				controller = new DeleteController();
			} 
			else if ( pDialogID.Equals(AllFPASSDialogs.SEARCH_COWORKER_DIALOG) ) 
			{
				controller = new ExtendedSearchController();
			} 
			else if ( pDialogID.Equals(AllFPASSDialogs.REPORTS_DIALOG) ) 
			{
				controller = new ReportsController();
			} 
			else if ( pDialogID.Equals(AllFPASSDialogs.ADMINISTRATION_DIALOG) ) 
			{
				controller = new AdministrationController();
			} 
			else if ( pDialogID.Equals(AllFPASSDialogs.HISTORY_DIALOG) ) 
			{
				controller = new HistoryController();
			} 
			else if ( pDialogID.Equals(AllFPASSDialogs.SEARCH_EXTERNAL_CONTRACTOR_DIALOG) ) 
			{
				controller = new ExternalContractorSearchController();
			}

            // User assigns hos own id card reader
            else if (pDialogID.Equals(AllFPASSDialogs.SEARCH_IDCARD_READER_DIALOG))
            {
                controller = new IDCardReaderSearchController();
            }
            // Popup for coworkers with new Ids from SmartAct
            else if (pDialogID.Equals(AllFPASSDialogs.POPUP_COWORKER_IDCARD_DIALOG))
            {
                controller = new IdCardsController();
            }

			else if ( pDialogID.Equals(AllFPASSDialogs.USER_DIALOG) ) 
			{
				controller = new UserController();
			}
			else if ( pDialogID.Equals(AllFPASSDialogs.USER_TO_ROLE_DIALOG) ) 
			{
				controller = new UserToRoleController();
			}
			else if ( pDialogID.Equals(AllFPASSDialogs.ARCHIVE_DIALOG) ) 
			{
				controller = new ArchiveController();
			}

			else if ( pDialogID.Equals(AllFPASSDialogs.DYNAMIC_DATA_DIALOG) ) 
			{
				controller = new DynamicDataController();
			}

			// New 30.04: Create controllers for FFMA- Coord hist
			else if ( pDialogID.Equals(AllFPASSDialogs.POPUP_COWORKER_COORD_HIST) ) 
			{
				controller = new PopCoWorkerHistController();
			}

			// and Exco - Coordinator hist
			else if ( pDialogID.Equals(AllFPASSDialogs.POPUP_EXCO_COORD_HIST) ) 
			{
				controller = new PopECODHistController();
			}

			else if ( pDialogID.Equals(AllFPASSDialogs.ROLE_DIALOG) ) 
			{
				controller = new RoleController();
			}
			// creates the coworker dialog in archive mode ( locked completly for user
			// input, selects are made of the archive tables, views )
			else if ( pDialogID.Equals(AllFPASSDialogs.COWORKER_ARCHIVE_DIALOG) ) 
			{
				controller = new CoWorkerController(AllFPASSDialogs.CWR_DIALOG_MODE_ARCHIVE);
			}

			else if ( pDialogID.Equals(AllFPASSDialogs.VEHICLE_DIALOG) ) 
			{
				controller = new VehicleController();
			}

			else 
			{
				controller = new SummaryController();
			}
			mDialogs.Add(pDialogID, controller);
			return controller;
		}

		/// <summary>
		/// Starts the GUI and shows the start dialog (summary coworker dialog).
		/// </summary>
		/// <exception cref="de.pta.Component.Errorhandling.UIFatalException"> 
		/// is thrown if an unhandled error occured</exception>
		internal void ShowFPASS() 
		{
			try 
			{
				AbstractController controller;
				controller = GetDialog(AllFPASSDialogs.SUMMARY_COWORKER_DIALOG);
				controller.PreShow();

				System.Windows.Forms.Application.Run(controller.mView);
			} 
			catch ( Exception e ) 
			{
				throw new UIFatalException( e.Message, e);
			}
		}

		/// <summary>
		/// Shows the dialog, where current user can chosse the unique mandator for this 
		/// fpass session.
		/// </summary>
		private void ShowMandatorDialog() 
		{
			AbstractController controller = new MandatorController();
			controller.ShowDialog();
		}


		#endregion // End of Methods


	}
}


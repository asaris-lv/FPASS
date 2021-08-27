using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Text;
using System.Data.OleDb;

using de.pta.Component.DbAccess;
using de.pta.Component.Errorhandling;
using de.pta.Component.DbAccess.Exceptions;

using Degussa.FPASS.Bo.Pass;
using Degussa.FPASS.InterfaceZks;
using Degussa.FPASS.Bo;
using Degussa.FPASS.Gui;
using Degussa.FPASS.Reports.Util;
using Degussa.FPASS.Util.UserManagement;
using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.Util;
using Degussa.FPASS.Util.Exceptions;
using Degussa.FPASS.Util.ListOfValues;
using Degussa.FPASS.Db;
using Degussa.FPASS.Db.DataSets;
using Degussa.FPASS.FPASSApplication;
using Degussa.FPASS.Util.Enums;

namespace Degussa.FPASS.Gui.Dialog.Pass
{
	/// <summary>
	/// A CoWorkerModel is the model of the MVC-triad CoWorkerModel,
	/// CoWorkerController and FrmCoWorker.
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
	public class CoWorkerModel : FPASSBaseModel
	{

		#region Members

		/// <summary>
		/// The Mode of the dialog ( archive or edit mode )
		/// </summary>
		private int mMode = 0;

        /// <summary>
        /// Correctly typed instance of View belonging to this MVC triad.
        /// </summary>
        FrmCoWorker mCwrView;

		/// <summary>
		/// The current datetime
		/// </summary>
		private DateTime now = System.DateTime.Now;

		/// <summary>
		/// The Dialog status ( insert or update ) of the shown coworker dialog
		/// </summary>
		private int mDialogStatus;
	
		/// <summary>
		/// Holds the business objects of this dialog
		/// </summary>
		private ArrayList BOs;

		/// <summary>
		/// The CoWorker BO
		/// </summary>
		private CoWorker mCoWorker;

		/// <summary>
		/// The id (pk) of the current Coworker
		/// </summary>
		private decimal mCoWorkerId;

		/// <summary>
		/// The PrecautionaryMedicals BO, which creates and holds the assigned PrecautionaryMedicalBriefings
		/// </summary>
		private PrecautionaryMedicals mPrecautionaryMedicals;

		/// <summary>
		/// The Plants BO, which creates and holds the assigned plants
		/// </summary>
		private Plants mPlants;

		/// <summary>
		/// The AuthorizationFactory, which creates and holds the authorizations of a coworker
		/// </summary>
		private AuthorizationFactory mAuthorizationFactory;

		/// <summary>
		/// The BriefingFactory, which creates and holds the assigned briefings of a coworker
		/// </summary>
		private BriefingFactory mBriefingFactory;
		
		/// <summary>
		/// The VehicleRegistrationNumberFactory, which creates and holds the assigned VehicleRegistrationNumber of a coworker
		/// </summary>
		private VehicleRegistrationNumberFactory mVehicleRegistrationNumberFactory;
		
		/// <summary>
		/// The MaskFactory, which creates and holds the assigned Masks of a coworker
		/// </summary>
		private MaskFactory mMaskFactory;

		/// <summary>
		/// Flag indicating if an insert or update in zks is necessary
		/// </summary>
		private bool mZKSChanged;

        /// <summary>
        /// Flags whether or not there is a change relevant for SmartAct
        /// </summary>
        private bool mShouldExportSmartAct;

        /// <summary>
        /// Flags whether or not CWR has SmartAct photo id card directed (If true then  export to SmartAct required)
        /// </summary>
        private bool mIdPhotoSmActDirect;

        /// <summary>
        /// Flags whether or not CWR has Hitag photo id card directed 
        /// </summary>
        private bool mIdPhotoHitagDirect;

        /// <summary>
        /// Flags whether or not CWR has industrial safety briefing (Sicherheitsunterweisung Koordinator)
        /// </summary>
        private bool mIndSafetyBrfRecvd;

        /// <summary>
        /// Current CWR's valid until date
        /// </summary>
        private DateTime mValidUntil;

        /// <summary>
        /// Current CWR's access type
        /// </summary>
        private string mAccess;

        /// <summary>
        /// Current user's id card reader type
        /// </summary>
        private string mIDCardReaderType;

		/// <summary>
		/// holds the error messages, foreach error a message is appended
		/// </summary>
		private StringBuilder mErrorMessages;

		/// <summary>
		/// flag indicating if the dialog allows user input.
		/// </summary>
		private bool mLocked;
									
		/// <summary>
		/// flag to bring up Acrobat Reader print dialog for resp mask ticket 
		/// </summary>
		private bool mPromptMaskTicket = false;

		/// <summary>
		/// Flags if coworker currently has a resp mask
		/// </summary>
		private bool mHasMaskTecBos = false;
        private bool mHasMaskFlorix = false;

        /// Flags if an existing CWR photo should be deleted
      //  private bool mShouldDeletePhotoSmAct;

        /// <summary>
        /// identifier for update statement in configuration.xml for update to id card reader number
        /// </summary>
        private const string UPDATE_USER_READER = "UpdateUserIdCardReader";
		
		#endregion 

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public CoWorkerModel()
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
			mLocked = false;
		}	

		#endregion //End of Initialization

		#region Accessors 

        /// <summary>
        /// Flags if coworker currently has a resp mask in TecBos.
        /// TODO in a future version: make this more elegant
        /// </summary>       
        public bool HasMaskTecBos
		{
			get { return mHasMaskTecBos; }
			set { mHasMaskTecBos = value; }
		}

        /// <summary>
        /// Flags if coworker currently has a resp mask in Florix.
        /// TODO in a future version: make this more elegant
        /// </summary>       
        public bool HasMaskFlorix
        {
            get { return mHasMaskFlorix; }
            set { mHasMaskFlorix = value; }
        }

		public bool PromptMaskTicket
		{
			get 
			{
				return mPromptMaskTicket;
			}
			set 
			{
				mPromptMaskTicket = value;
			}
		}	
		
		/// <summary>
		/// simple accessor
		/// </summary>
		public bool Locked
		{
			get 
			{
				return mLocked;
			}
			set 
			{
				mLocked = value;
			}
		}

		/// <summary>
		/// simple accessor
		/// </summary>
		public int Mode 
		{
			get 
			{
				return mMode;
			} 
			set 
			{
			  mMode = value;
			}
		}

		

		/// <summary>
		/// simple accessor
		/// </summary>
		public int Status 
		{
			get 
			{
				return mDialogStatus;
			}
			set 
			{
				mDialogStatus = value;
			}
		} 

		/// <summary>
		/// simple getter
		/// </summary>
		public StringBuilder ErrorMessages 
		{
			get 
			{
				return mErrorMessages;
			}
		}

		/// <summary>
		/// Id of current coworker
		/// </summary>
		public decimal CoWorkerId
		{
			get 
			{
				return mCoWorkerId;
			}
			set 
			{
				mCoWorkerId = value;
			}
		}

        /// <summary>
        /// Stores current ID card reader type. Either Hitag2 or Mifare.
        /// </summary>
        public string IDCardReaderType
        {
            get { return mIDCardReaderType; }
            set { mIDCardReaderType = value; }
        }

		/// <summary>
        /// Flags whether or not there is a change for ZKS
		/// </summary>
		public bool ZKSChanged
		{
			get { return mZKSChanged; }
			set { mZKSChanged = value; }
		}

        /// <summary>
        /// Flags whether or not CWR has SmartAct photo id card directed (If true then export to SmartAct required)
        /// </summary>
        public bool IdPhotoSmActDirected
        {
            get { return mIdPhotoSmActDirect; }
            set { mIdPhotoSmActDirect = value; }
        }

        /// <summary>
        /// Flags whether or not CWR has photo id card (older Hitag chip) directed 
        /// </summary>
        public bool IdPhotoHitagDirected
        {
            get { return mIdPhotoHitagDirect; }
            set { mIdPhotoHitagDirect = value; }
        }

        /// <summary>
        /// Flags whether or not there is a change for SmartAct
        /// to be exported
        /// </summary>
        internal bool ShouldExportSmartAct
        {
            get { return mShouldExportSmartAct; }
            set { mShouldExportSmartAct = value; }
        }

        ///// <summary>
        ///// Flags if an existing CWR photo should be deleted 
        ///// (these originate from SmartAct)
        ///// </summary>
        //public bool ShouldDeletePhotoSmAct
        //{
        //    set
        //    {
        //        mShouldDeletePhotoSmAct = value;
        //    }
        //}


        /// <summary>
        /// Flags whether or not CWR has received industrial safety briefing (Sicherheitsunterweisung)
        /// (Sicherheitsunterweisung Koordinator erteilt).
        /// </summary>
        internal bool IndSafetyBrfRecvd
        {
            get { return mIndSafetyBrfRecvd; }
            set { mIndSafetyBrfRecvd = value; }
        }

		#endregion


		#region Methods 


		/// <summary>
		/// is called before a dialog is closed.
		/// bo's for the shown coworker are released
		/// </summary>
		internal override void PreClose()
		{
			BOs.Clear();
			mLocked = false;	
		}


		/// <summary>
		/// is called before the dialog is displayed.
		/// initializes the bo's and the data for the current coworker.
		/// </summary>
		internal override void PreShow()
		{          
		}


		/// <summary>
		/// Initializes all bo's used in coworker dialog.
		/// </summary>
		internal void InitializeData() 
		{
            mCwrView = (FrmCoWorker)mView;

			mErrorMessages = new StringBuilder();

			mZKSChanged = false;
				
			BOs = new ArrayList();

			mCoWorker = new CoWorker(this);
			BOs.Add(mCoWorker);

			mVehicleRegistrationNumberFactory = new VehicleRegistrationNumberFactory(this);
			BOs.AddRange(mVehicleRegistrationNumberFactory.GetVehicles());

			mAuthorizationFactory = new AuthorizationFactory(this);
			BOs.AddRange( mAuthorizationFactory.GetAuthorizations() );

			mBriefingFactory = new BriefingFactory(this);
			BOs.AddRange ( mBriefingFactory.GetBriefings() );

			mPrecautionaryMedicals = new PrecautionaryMedicals(this);
			BOs.Add(mPrecautionaryMedicals);

			mPlants = new Plants(this);
			BOs.Add(mPlants);

			mMaskFactory = new MaskFactory(this);
			BOs.AddRange(mMaskFactory.GetMasks());

			RegisterViewOnBOs();		
			ReadData();
		}

		/// <summary>
		/// registers the view on the bo's hold in this model
		/// </summary>
		internal void RegisterViewOnBOs() 
		{
			foreach ( AbstractCoWorkerBO bo in BOs ) 
			{
				bo.RegisterView(mView);
			}
		}


		/// <summary>
		/// Sets the current coworker the dialog has to display
		/// </summary>
		/// <param name="pCoWorkerID">id of the current Coworker</param>
		internal void SetCurrentFFMA(decimal pCoWorkerID) 
		{
			mCoWorkerId = pCoWorkerID;
		}
	

		/// <summary>
		/// checks if the content of the dialg was changed were by the user
		/// by delegating it to the bo's
		/// </summary>
		/// <returns>true if there are changes, false othwerwise
		/// </returns>
		internal bool CheckChanges() 
		{
			// no need to check if the dialog is locked for user input
			if (!mLocked) 
			{
				foreach ( AbstractCoWorkerBO bo in BOs ) 
				{
					bo.CopyOut();
					if (bo.Changed) 
					{
						return true;
					}
				}
				return false;
			} 
			else 
			{
				return false;
			}
		}


		/// <summary>
		/// Saves the changed data of this dialog by delegating the request to the BO's.
		/// Commits the transaction if the all SQL statements were successful.
		/// Exports to ZKS.
        /// Exports to SmarACt (CSV) if current CWR has a photo id card
		/// 13.07.04: changed exception handling so ID card can be removed in FPASS only
		/// <exception cref="de.pta.Component.Errorhandling.UIFatalException">
		/// Is thrown if a database error occurs 
		/// </summary>
		internal void Save() 
		{
            bool doExportSmartAct;

			try 
			{
				// Save data in FPASS
				this.Validate();
				this.CheckCoworkerExistence();
				this.StartTransaction();
				foreach (AbstractCoWorkerBO bo in BOs) 
				{
					bo.Save();
				}
				
                // Calculates Valid until date (gültig bis) in DB
                CalculateValidUntil();

                // Updates current user's id reader numbers (terminals)
                if (mCoWorker.IdReaderChanged)
                    UpdateUserIdCardReader(false);

                // commit
                CommitTransaction();

                // Re-reads CWR access
                ReadValidUntil();
                mCoWorker.ValidUntil = mValidUntil;
                mCoWorker.Access = mAccess;
			
                // Re-reads cwr id, in case it was a new cwr
				CoWorkerId = mCoWorker.CoWorkerID;

                // Coworker data always transfered to ZKS, unless Id cards missing or ID card has just been deleted
                if (!mCoWorker.IdCardDeleted)
                {
                    ExportCwrToZKS(CoWorkerId);
                    mZKSChanged = false;
                }

                // Export changes to SmartAct?
                if (mShouldExportSmartAct)
                {
                    // Restriction here: export to SmartAct only allowed:
                    // 1) when CWR has photo id card directed (Coordinator)
                    // 2) has received ind safety briefing from coordinator.
                    // Restriction (2) can be deactivated by parameter.
                    if (Globals.GetInstance().IndSafety4ExpSmartAct)
                        doExportSmartAct = mIdPhotoSmActDirect && mIndSafetyBrfRecvd;
                    else
                        doExportSmartAct = mIdPhotoSmActDirect;

                    if (doExportSmartAct)
                    {
                        // Exports the current coworker to interface file (CSV export FPASS to SMARTACT)
                        ExportToSmartAct(mCoWorker.CoWorkerID.ToString(), SmartActActions.Update, true);
                        mShouldExportSmartAct = false;
                    }
                }

                //// Delete Id card photo in FPASS (originally imported from SmartAct)
                //if (mShouldDeletePhotoSmAct)
                //{
                //    DeleteSmartActCwrPhoto(mCoWorker.CoWorkerID.ToString());
                //    mShouldDeletePhotoSmAct = false;
                //}
                
        
				// reload in GUI
				mCwrView.Cursor = System.Windows.Forms.Cursors.Default;
				ReloadChangedData();

				// Shows Adobe Acrobat Reader Print dialog to prompt user to print mask ticket
				// if mask has just been given back (delivered) 
                if (mPromptMaskTicket && (!mHasMaskTecBos || !mHasMaskFlorix))
				{
					mPromptMaskTicket = false;
					ShowPromptMaskTicket();				
				}
				else if (mPromptMaskTicket && (mHasMaskTecBos || mHasMaskFlorix))
				{
                    // If mask lent but not returned then error.
					mPromptMaskTicket = false;
					throw new UIWarningException( MessageSingleton.GetInstance().GetMessage(MessageSingleton.MASK_TICKET_NO_CWR) );				
				}			
			}
			catch (UIWarningException uwe)
			{
				// 13.07.04 Added this catch-block to catch warning exception thrown by ExportDataToZKS
				// for the specific case that an ID card number does not exist in FPASS but is already assigned
				// in another part of  ZKS system
				// CoWorker should be saved in FPASS but with no ID card number 
				
				if ( ! uwe.Message.Equals ( MessageSingleton.GetInstance().GetMessage(MessageSingleton.ZKS_NO_SUCCESS_GUI) ) )
				{
					// case 1: exception thrown by Save methods of BOs
					// Don't deal with exception here
                    throw new UIWarningException(uwe.Message);
				}
				else
				{
					// case 2: exception thrown by methods transferring data to ZKS 
					// Empty ID card number field in GUI and re-save changes to DB
					mCwrView.TxtReIDReaderHitag2.Text = String.Empty;
									
					this.StartTransaction();
				
					// Only the ID card number of current CoWorker object has been changed
					// Only need to re-save CoWorker object
					foreach ( AbstractCoWorkerBO bo in BOs ) 
					{
						if ( bo.GetType().Equals(typeof(CoWorker)) )
						{
							bo.CopyOut();
							bo.Save();
						}
					}				
					this.CommitTransaction();
					this.ReloadChangedData();

					mCwrView.BtnDelPassNumber.Enabled = true;
					mCwrView.Cursor = System.Windows.Forms.Cursors.Default;
					ExceptionProcessor.GetInstance().Process(uwe);			
				}
			}
			catch ( System.Data.OracleClient.OracleException oe ) 
			{
				// Oracle exceptions caught here rather than in BOs in order to react to possible
				// data conflicts caused by other sessions of FPASS
				
				mCwrView.Cursor = System.Windows.Forms.Cursors.Default;
				this.RollbackTransaction();

				// If stored function threw error or referential integrity failed,
				// interpret this as data conflict (eg cwr was deleted in another session)
				if ( oe.Code == 20001 || oe.Code == 02291)
				{
					foreach ( AbstractCoWorkerBO bo in BOs ) 
					{
						bo.Changed = false;
					}
					throw new UIWarningException ( MessageSingleton.GetInstance().GetMessage(MessageSingleton.COWORKER_NOTEXISTS) );
				}
				// 04.05.04: If briefing already exists and second session tries to insert in DB,
				// error due to uniqueness
				else if ( oe.Code == 00001 )
				{
					foreach ( AbstractCoWorkerBO bo in BOs ) 
					{
						bo.Changed = false;
					}
					throw new UIWarningException( MessageSingleton.GetInstance().GetMessage(
						MessageSingleton.COWORKER_ALREADY_BRIEF) );
				}
				else
				{
					throw new UIFatalException(MessageSingleton.GetInstance().GetMessage(
						MessageSingleton.FATAL_DB_ERROR), oe);
				}
			} 
			catch ( DbAccessException dba ) 
			{
				// DBAccess exception thrown from PTA component
				mCwrView.Cursor = System.Windows.Forms.Cursors.Default;
				this.RollbackTransaction();
				throw new UIFatalException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR), dba);
			}
		}


		/// <summary>
		/// Delegates user input (selection of an existing 
		/// precautionarymedicalbriefing) in the register premedical to the 
		/// resposible bo (mPrecautionaryMedicals)
		/// </summary>
		/// <param name="pPrecMedID">Id of the selected precautionarymedicalBriefing</param>
		internal void ChangePrecMed(decimal pPrecMedID) 
		{
			mPrecautionaryMedicals.SetChangedPrecMed(pPrecMedID);
		}

		/// <summary>
		/// Delegates user input (selection of a new precautionarymedicalbriefing ) 
		/// in the register premedical to the resposible bo
		/// (mPrecautionaryMedicals)
		/// </summary>
		internal void CreatePrecMed() 
		{
            if (mPrecautionaryMedicals.PrecMedicalCount >= Globals.GetInstance().PrecMedicalMaxAssign)
            {
                var myMsg = String.Format(MessageSingleton.GetInstance().GetMessage(MessageSingleton.PREC_MEDICAL_MAX_ASSIGNED), Globals.GetInstance().PrecMedicalMaxAssign);
                MessageBox.Show(myMsg, TitleMessage, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                mPrecautionaryMedicals.CreateNewPrecMed();

            }
        }

        /// <summary>
		/// Delegates user input (delete selected precautionarymedicalbriefing ) 
		/// in the register premedical to the resposible bo(mPrecautionaryMedicals)
		/// The access (validNUTIL) of the current CWR must also be re-calculated 
		/// and reloaded into foot of form
		/// Must turn off event "Leave field validFROM -Gültig bis" 
		/// cursor springs into this field when validUNTIL is re-caculated after prec medicals are deleted.
		/// 26.04.04: Forgot to call ZKS for deleted prec medical
		/// </summary>
		internal void DeletePrecMed() 
		{
			decimal currPrecMedTypeID = -1;		
			currPrecMedTypeID = Convert.ToDecimal( mCwrView.CboSiMedPrecautionaryMedical.SelectedValue);

			if ( currPrecMedTypeID > 0 )
			{
				try
				{							
					StartTransaction();
					mPrecautionaryMedicals.DeleteCurrentPrecMed();
					CalculateValidUntil();
					CommitTransaction();

					// Update coworker in ZKS 					
					ExportCwrToZKS(CoWorkerId);
					mZKSChanged  = false;
					
					ReadValidUntil();
                    mCoWorker.ValidUntil = mValidUntil;
					mPrecautionaryMedicals.ReloadChangedData();
                    mCoWorker.ShowStatusColor();
                    mCoWorker.ShowZKSColor();

					mCwrView.btnDelPrecMed.Enabled  = false;
					mCwrView.DatPassValidFrom.Leave -= new System.EventHandler( mCwrView.DatPassValidFrom_Leave );
					mCwrView.TxtPassValidUntil.Text = Convert.ToString(mCoWorker.ValidUntil).Substring(0,10);
					mCwrView.BtnSave.Focus();
					mCwrView.DatPassValidFrom.Leave += new System.EventHandler( mCwrView.DatPassValidFrom_Leave );
				
				}
				catch ( System.Data.OracleClient.OracleException oe ) 
				{					
					RollbackTransaction();
				
					// If stored procedure threw error, interpret this as data conflict (eg cwr was deleted)
					if ( oe.Code == 20001 || oe.Code == 02291)
					{
						throw new UIWarningException ( MessageSingleton.GetInstance().GetMessage(MessageSingleton.COWORKER_NOTEXISTS) );
					}
					else
					{
						throw new UIFatalException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR), oe);
					}
				} 
				catch ( DbAccessException dba ) 
				{
					RollbackTransaction();				
					throw new UIFatalException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR), dba);
				}
			}
		}


		/// <summary>
		/// Delegates user input (selection of a directed plant ) 
		/// in the register plants to the responsible bo
		/// (mPlants)
		/// </summary>
		/// <param name="pPlantName"></param>
		internal void StartEditPlant(String pPlantName) 
		{
			mPlants.StartEditPlant(pPlantName);
		}

		/// <summary>
		/// Delegates user input (selection of an existing plant ) 
		/// in the plant list in register coordinator to the resposible bo
		/// (mPlants)
		/// </summary>
		internal void ChangeInPlants() 
		{
			mPlants.ChangeInPlants();
		}

        /// <summary>
        /// Delegates to BO to assign all plants
        /// </summary>
        internal void AssignAllPlants()
        {
            mPlants.AssignAllPlants();
        }

		/// <summary>
		/// Read data of current coworker from the db.
		/// Gives each BO the PK iD of current coworker.
		/// Forces each BO to copy its data into the view 
		/// </summary>
		internal void ReadData() 
		{
			foreach (AbstractCoWorkerBO bo in BOs) 
			{
				bo.InitializeBO();
				bo.CopyIn();
				bo.Changed = false;
			}
			mZKSChanged = false;
			CheckCoordinatorAccess();
		}

		
		/// <summary>
		/// Call method in base model to open static document with the given filename
		/// Filename is fully qualified (complete path and name)
		/// </summary>
		internal void GenerateDocSafety()
		{
			GeneratePDFDoc(Globals.GetInstance().DocSafetyPathAndName);
		}

		
		/// <summary>
		/// Calls Acrobat Reader and show its "Print" dialog
		/// to prompt user to print Mask Ticket
		/// Mask ticket (PDF report) proves mask has been given back
		/// </summary>
		internal void ShowPromptMaskTicket()
		{
			bool origPreviewPass = UserManagementControl.getInstance().PreviewPass;
			UserManagementControl.getInstance().PreviewPass = false;				
			GenerateCWRReport(CoWorkerId, ReportNames.RESPMASK_RETURNED_TICKET);			
			UserManagementControl.getInstance().PreviewPass = origPreviewPass;			
		}

		/// <summary>
        /// Sets flag indicating that current coordinator is not allowed to edit current coworker,
        /// if cwr is not assigned to him or one of his external contractors.
		/// </summary>
        private void CheckCoordinatorAccess()
        {
            if (mDialogStatus.Equals(AllFPASSDialogs.DIALOG_STATUS_UPDATE))
            {
                if (!UserManagementControl.getInstance().CurrentUserIsInRole(UserManagementControl.ROLE_EDVADMIN)
                    && !UserManagementControl.getInstance().CurrentUserIsInRole(UserManagementControl.ROLE_VERWALTUNG)
                    && UserManagementControl.getInstance().CurrentUserIsInRole(UserManagementControl.ROLE_KOORDINATOR))
                {
                    // Does not have admin role but has coordinator role
                    if (!FPASSLovsSingleton.GetInstance().CoordIsAssignedContractor(UserManagementControl.getInstance().CurrentCoordinatorID, mCoWorker.ContractorID))
                    {
                        // Flags the fact that current user is not coordinator for current coworker                         
                        mCwrView.CoordinatorEditLocked = true;
                    }
                }
            }
        }


		/// <summary>
		/// checks if a coworker with the given surname, firstname, date of birth and
		/// place of birth already exsits in the database by delegating the request to 
		/// the CoWorker BO. Shows user prompt if the coworker exists.
		/// <exception cref="de.pta.Component.Errorhandling.UIWarningException">
		/// Is thrown if a user wants to cancel the save if the coworker already exists.
		/// </summary>
		private void CheckCoworkerExistence() 
		{
			if ( this.Status.Equals(AllFPASSDialogs.DIALOG_STATUS_NEW ) )
			{
				try 
				{
					mCoWorker.CheckCoWorkerExistence();
				}
				catch ( UIWarningException uwe ) 
				{
					mCwrView.Cursor = System.Windows.Forms.Cursors.Default;
					DialogResult dialogResult = MessageBox.Show(uwe.Message, "FPASS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					if ( dialogResult == DialogResult.Yes ) 
					{
						// do nothing 
					} 
					else if ( dialogResult == DialogResult.No ) 
					{
						throw new ActionCancelledException();
					}
				}
			}
		}
		

		/// <summary>
		/// Reloads changed data from database. This is done only for resp masks and
		/// precautionary medical briefings. 
		/// Precautionary medical briefings are reloaded to show correct data in grid
		/// when the grid is updated.
		/// If delivery date of respmask was set to current date or a deliv date of current date 
		/// or earlier was read from DB, then masks are not shown in GUI
		/// First remove both masks from BOs and re-read them from database
		/// 30.03.04: Re-read attributes of Coworker BO from database to get new Supervisor name etc
		/// </summary>
		private void ReloadChangedData() 
		{
			ArrayList copy = new ArrayList();
			copy.AddRange(BOs);

			foreach ( AbstractCoWorkerBO bo in copy ) 
			{
				Type ty = bo.GetType();
				if ( ty.IsSubclassOf(typeof(AbstractMask) ) )
				{
					BOs.Remove(bo);
				}
			}

			BOs.AddRange( mMaskFactory.GetMasks() );
			this.RegisterViewOnBOs();

			foreach ( AbstractCoWorkerBO bo in BOs ) 
			{
				if ( bo.GetType().Equals(typeof(CoWorker)) )
				{
					// If current BO is Coworker, Re-read attributes from database to get new Supervisor name etc
					((CoWorker) bo).InitializeBO();
				}
				bo.CopyIn();
			}
			mPrecautionaryMedicals.ReloadChangedData();
		}

		/// <summary>
		/// Transfers and validates user input in the BO's.
        /// Builds and returns a list of errors as <exception cref="de.pta.Component.Errorhandling.UIWarningException"></exception>.
        /// If ID card number was manually edited ask for confirmation. Returns error if field was emptied manually.
		/// </summary>
		internal void Validate() 
		{
			foreach ( AbstractCoWorkerBO bo in BOs ) 
			{
				bo.CopyOut();
				bo.Validate();
			}
			if ( mErrorMessages.Length > 1 ) 
			{
				String messages = mErrorMessages.ToString();
				mErrorMessages = new StringBuilder();				
                mView.Cursor = Cursors.Default;
				
                throw new UIWarningException("Die eingegebenen Werte sind nicht korrekt: \r\n" + messages);
			}
            // ValidateIdCardNumberChanged();
		}

        // superseded by check in CoWorker business object
        ///// <summary>
        ///// Validates field Ausweis-Nr if it was edited manually
        ///// Emptying field manually is not allowed, User must confirm value entered
        ///// </summary>
        //private void ValidateIdCardNumberChanged()
        //{
        //    if (mCwrView.IdCardNumberChanged)
        //    {
        //        string idNumber = mCwrView.TxtReIDReaderHitag2.Text;
               
        //        // Not allowed: user cleared ID field manually
        //        if (idNumber.Length == 0)
        //        {
        //            throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.IDCARD_EMPTY_NOTALLOWED));
        //        }
        //        else
        //        {
        //            // Confirms ID card no. should be saved if edited manually
        //            string completeMs = String.Format(MessageSingleton.GetInstance().GetMessage(MessageSingleton.IDCARD_CONFIRM_EDIT), idNumber);
                    
        //            DialogResult res = MessageBox.Show(completeMs, "FPASS", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
        //            if (res != DialogResult.OK)
        //            {
        //                throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.ACTION_CANCELLED));
        //            }
        //            else { mCwrView.IdCardNumberChanged = false; }
        //        }
        //    }
        //}
      
       
		/// <summary>
		/// Calculates date until which coworker is granted entry (is valid)
		/// by means of stored function in database
		/// </summary>
		private void CalculateValidUntil() 
		{
			IDbCommand  comm = null;

			// Create the command
			IProvider provider = DBSingleton.GetInstance().DataProvider;
			comm = provider.CreateCommand("SequenceDummy");

			comm.Transaction = this.CurrentTransaction;
			comm.Connection  = this.CurrentTransaction.Connection;

			comm.CommandType = CommandType.StoredProcedure;

			// Stored Proc can execute as command is open
			comm.CommandText = "SP_CALCULATEVALIDUNTIL( " 
							+ mCoWorker.CoWorkerID.ToString() 
							+ ", "
							+ UserManagementControl.getInstance().CurrentUserID.ToString() 
							+ ", "
							+ UserManagementControl.getInstance().CurrentMandatorID.ToString() 
							+ " )";

			comm.ExecuteNonQuery();
		}


		/// <summary>
        /// Re-Reads current coworker's validUNTIL date and access type from database
        /// (calculated by stored procedure so not otherwise accessible to FPASS)
		/// </summary>
		/// <returns>validUNTIL date for current coworker</returns>
		private void ReadValidUntil() 
		{
			IProvider provider  = DBSingleton.GetInstance().DataProvider;		
			IDbCommand mSelComm = provider.CreateCommand("SelectValidUntil");
		    
            provider.SetParameter(mSelComm, ":CWR_ID", mCoWorker.CoWorkerID );
			IDataReader mDR     = provider.GetReader(mSelComm);

			while (mDR.Read()) 
			{
				mValidUntil = Convert.ToDateTime(mDR["CWR_VALIDUNTIL"]);
                mAccess = Convert.ToString(mDR["CWR_ACCESS"]);
			}
			mDR.Close();
		}

       

        /// <summary>
        /// Saves the current id card reader numbers 
        /// (Hitag2 and Mifare) as an attribute of the current user.
        /// </summary>
        internal void UpdateUserIdCardReader(bool pStandalone)
        {
            IDbCommand mUpdComm = null;
            IProvider provider = DBSingleton.GetInstance().DataProvider;
            mUpdComm = provider.CreateCommand(UPDATE_USER_READER);

            if (pStandalone)
            {
                // This means the method is being called on its own,
                // need to copy out values and initialise transaction
                mCoWorker.CopyOutIdReader(IDCardTypes.Hitag2);
                mCoWorker.CopyOutIdReader(IDCardTypes.Mifare);

                // If reader numbers have not been changed then no need to save
                if (!mCoWorker.IdReaderChanged)
                    return;

                this.StartTransaction();
            }

            mUpdComm.Transaction = this.CurrentTransaction;
            mUpdComm.Connection = this.CurrentTransaction.Connection;

            provider.SetParameter(mUpdComm, ":USER_ID", UserManagementControl.getInstance().CurrentUserID);
            provider.SetParameter(mUpdComm, ":USER_IDCARDREADER_HITAG", mCoWorker.IDReaderHitag);
            provider.SetParameter(mUpdComm, ":USER_IDCARDREADER_MIFARE", mCoWorker.IDReaderMifare);
            provider.SetParameter(mUpdComm, ":USER_CHANGEUSER", UserManagementControl.getInstance().CurrentUserID);
            provider.SetParameter(mUpdComm, ":USER_TIMESTAMP", DateTime.Now);

            mUpdComm.ExecuteNonQuery();

            // Updates ID card reader numbers - user attributes in cache (UserManagement instance)
            UserManagementControl.getInstance().IDCardReaderHitag = mCoWorker.IDReaderHitag;
            UserManagementControl.getInstance().IDCardReaderMifare = mCoWorker.IDReaderMifare;

            mCoWorker.IdReaderChanged = false;


            if (pStandalone)
            {
                CommitTransaction();
            }
        }

		#endregion 


		#region ÜbergreifendeSteuerungUserControls

		internal void EnableFieldsArchiveFalse()
		{
			mCwrView.PnlTabCoordinator.Enabled = false;
			mCwrView.PnlTabPlant.Enabled = false;
			mCwrView.PnlTabReception.Enabled = false;
			mCwrView.PnlTabSafetyWork.Enabled = false;
			mCwrView.PnlTabSiteFireService.Enabled = false;
			mCwrView.PnlTabSiteMedical.Enabled = false;
			mCwrView.PnlTabSiteSecure.Enabled = false;
			mCwrView.PnlTabTechnical.Enabled = false;

			mCwrView.DatDeliveryDate.Enabled = false;
			mCwrView.DatPassValidFrom.Enabled = false;
		}

		internal void EnableFieldsArchiveTrue()
		{
			mCwrView.PnlTabCoordinator.Enabled = true;
			mCwrView.PnlTabPlant.Enabled = true;
			mCwrView.PnlTabReception.Enabled = true;
			mCwrView.PnlTabSafetyWork.Enabled = true;
			mCwrView.PnlTabSiteFireService.Enabled = true;
			mCwrView.PnlTabSiteMedical.Enabled = true;
			mCwrView.PnlTabSiteSecure.Enabled = true;
			mCwrView.PnlTabTechnical.Enabled = true;

			mCwrView.DatDeliveryDate.Enabled = true;
			mCwrView.DatPassValidFrom.Enabled = true;
		}

		/// <summary>
		/// Does what it says for the tabs on the CoWorker mask.
		/// 02.03.2004: Prec Meds are automatically received (erteilt), checkbox in GUI no longer needed
		/// 15.03.2005: patch to stop MaskReceived being enabled when other things changed
        /// 13.01.2015: FPASS V5 radio buttons for apprentice access
		/// </summary>
        internal void ValidateRadioButtonsEnableFields()
        {
            // Coordinator fireman briefing
            if (mCwrView.RbtCoFiremanYes.Checked)
            {
                mCwrView.RbtSiFiFireman.Checked = true;

                if (mCwrView.mSiteFireAuthorization ||
                    mCwrView.mEdvAdminAuthorization ||
                    mCwrView.mSystemAdminAuthorization)
                {
                    mCwrView.DatSiFiFiremanDoneOn.Enabled = true;
                    mCwrView.CbxSiFiFireman.Enabled = true;
                }
            }
            if (mCwrView.RbtCoFiremanNo.Checked)
            {
                mCwrView.RbtSiFiFireman.Checked = false;
                mCwrView.DatSiFiFiremanDoneOn.Enabled = false;
                mCwrView.CbxSiFiFireman.Enabled = false;
            }

            // Coordinator site security briefing
            if (mCwrView.RbtCoSiteSecurityBriefingYes.Checked)
            {
                mCwrView.RbtSiSeSiteSecBri.Checked = true;

                if (mCwrView.mSiteSecurityAuthorization ||
                    mCwrView.mSiteSecurityLeaderAuthorization ||
                    mCwrView.mEdvAdminAuthorization ||
                    mCwrView.mSystemAdminAuthorization)
                {
                    mCwrView.DatSiSeSiteSecBriRec.Enabled = true;
                    mCwrView.CbxSiSeSiteSecBriRec.Enabled = true;
                }
            }
            if (mCwrView.RbtCoSiteSecurityBriefingNo.Checked)
            {
                mCwrView.RbtSiSeSiteSecBri.Checked = false;
                mCwrView.DatSiSeSiteSecBriRec.Enabled = false;
                mCwrView.CbxSiSeSiteSecBriRec.Enabled = false;
            }

            // Coordinator breathing apparatus
            if (mCwrView.RbtCoBreathingApparatusYesG26_2.Checked)
            {
                mCwrView.RbtSiFiSiteSecurityBriefingG26_2.Checked = true;

                if (mCwrView.mSiteFireAuthorization ||
                    mCwrView.mEdvAdminAuthorization ||
                    mCwrView.mSystemAdminAuthorization)
                {
                    mCwrView.DatSiFiSiteSecurityBriefingDoneOnG26_2.Enabled = true;
                    mCwrView.CbxSiFiSiteSecurityBriefingDoneG26_2.Enabled = true;
                }
            }
            if (mCwrView.RbtCoBreathingApparatusNoG26_2.Checked)
            {
                mCwrView.RbtSiFiSiteSecurityBriefingG26_2.Checked = false;
                mCwrView.DatSiFiSiteSecurityBriefingDoneOnG26_2.Enabled = false;
                mCwrView.CbxSiFiSiteSecurityBriefingDoneG26_2.Enabled = false;
            }

            // Coordinator breathing apparatus
            if (mCwrView.RbtCoBreathingApparatusYesG26_3.Checked)
            {
                mCwrView.RbtSiFiSiteSecurityBriefingG26_3.Checked = true;

                if (mCwrView.mSiteFireAuthorization ||
                    mCwrView.mEdvAdminAuthorization ||
                    mCwrView.mSystemAdminAuthorization)
                {
                    mCwrView.DatSiFiSiteSecurityBriefingDoneOnG26_3.Enabled = true;
                    mCwrView.CbxSiFiSiteSecurityBriefingDoneG26_3.Enabled = true;
                }
            }
            if (mCwrView.RbtCoBreathingApparatusNoG26_3.Checked)
            {
                mCwrView.RbtSiFiSiteSecurityBriefingG26_3.Checked = false;
                mCwrView.DatSiFiSiteSecurityBriefingDoneOnG26_3.Enabled = false;
                mCwrView.CbxSiFiSiteSecurityBriefingDoneG26_3.Enabled = false;
            }

            // Coordinator cranes
            if (mCwrView.RbtCoCranesYes.Checked)
            {
                mCwrView.RbtSaAtWoCranesBriefing.Checked = true;

                if (mCwrView.mSafetyAtWorkAuthorization ||
                    mCwrView.mEdvAdminAuthorization ||
                    mCwrView.mSystemAdminAuthorization)
                {
                    mCwrView.DatSaAtWoCranesBriefingDoneOn.Enabled = true;
                    mCwrView.CbxSaAtWoCranesBriefingDone.Enabled = true;
                }
            }
            if (mCwrView.RbtCoCranesNo.Checked)
            {
                mCwrView.RbtSaAtWoCranesBriefing.Checked = false;
                mCwrView.DatSaAtWoCranesBriefingDoneOn.Enabled = false;
                mCwrView.CbxSaAtWoCranesBriefingDone.Enabled = false;
            }

            // Coordinator plant
            if (mCwrView.RbtCoDepartmentalBriefingYes.Checked)
            {
                mCwrView.RbtSaAtWoBriefing.Checked = true;

                if (mCwrView.mSafetyAtWorkAuthorization ||
                    mCwrView.mEdvAdminAuthorization ||
                    mCwrView.mSystemAdminAuthorization)
                {
                    mCwrView.DatSaAtWoSafetyAtWorkBriefingDone.Enabled = true;
                    mCwrView.CbxSaAtWoSafetyAtWorkBriefingDone.Enabled = true;
                }
            }
            if (mCwrView.RbtCoDepartmentalBriefingNo.Checked)
            {
                mCwrView.RbtSaAtWoBriefing.Checked = false;
                mCwrView.DatSaAtWoSafetyAtWorkBriefingDone.Enabled = false;
                mCwrView.CbxSaAtWoSafetyAtWorkBriefingDone.Enabled = false;
            }

            // Coordinator pallet lifter
            if (mCwrView.RbtCoPalletLifterYes.Checked)
            {
                mCwrView.RbtSaAtWoPalletLifterBriefing.Checked = true;

                if (mCwrView.mSafetyAtWorkAuthorization ||
                    mCwrView.mEdvAdminAuthorization ||
                    mCwrView.mSystemAdminAuthorization)
                {
                    mCwrView.DatSaAtWoPalletLifterBriefingDoneOn.Enabled = true;
                    mCwrView.CbxSaAtWoPalletLifterBriefingDone.Enabled = true;
                }
            }
            if (mCwrView.RbtCoPalletLifterNo.Checked)
            {
                mCwrView.RbtSaAtWoPalletLifterBriefing.Checked = false;
                mCwrView.DatSaAtWoPalletLifterBriefingDoneOn.Enabled = false;
                mCwrView.CbxSaAtWoPalletLifterBriefingDone.Enabled = false;
            }

            // Coordinator precautionary medical
            if (mCwrView.RbtCoPrecautionaryMedicalYes.Checked)
            {
                mCwrView.RbtSiMedPrecautionaryMedicalBriefing.Checked = true;

                if (mCwrView.mMedicalServiceAuthorization ||
                    mCwrView.mEdvAdminAuthorization ||
                    mCwrView.mSystemAdminAuthorization)
                {
                    mCwrView.DatSiMedValidUntil.Enabled = true;
                    mCwrView.CboSiMedPrecautionaryMedical.Enabled = true;
                    mCwrView.DgrSiMedPrecautionaryMedical.Enabled = true;
                    mCwrView.btnDelPrecMed.Enabled = true;
                }
            }
            if (mCwrView.RbtCoPrecautionaryMedicalNo.Checked)
            {
                mCwrView.RbtSiMedPrecautionaryMedicalBriefing.Checked = false;
                mCwrView.DatSiMedValidUntil.Enabled = false;
                mCwrView.CboSiMedPrecautionaryMedical.Enabled = false;
                mCwrView.DgrSiMedPrecautionaryMedical.Enabled = false;
                mCwrView.btnDelPrecMed.Enabled = false;
            }

            // Coordinator raisable platform
            if (mCwrView.RbtCoRaisablePlattformYes.Checked)
            {
                mCwrView.RbtTecBriefing.Checked = true;

                if (mCwrView.mTechDepartmentAuthorization ||
                    mCwrView.mEdvAdminAuthorization ||
                    mCwrView.mSystemAdminAuthorization)
                {
                    mCwrView.DatTecBriefingDoneOn.Enabled = true;
                    mCwrView.CbxTecRaisonalPlattform.Enabled = true;
                }
            }
            if (mCwrView.RbtCoRaisablePlattformNo.Checked)
            {
                mCwrView.RbtTecBriefing.Checked = false;
                mCwrView.DatTecBriefingDoneOn.Enabled = false;
                mCwrView.CbxTecRaisonalPlattform.Enabled = false;
            }


            bool isAllowed =
                mCwrView.CbxSiFiRespMaskBriefRec.Checked
                && (mCwrView.mSiteFireAuthorization
                || mCwrView.mEdvAdminAuthorization
                || mCwrView.mSystemAdminAuthorization);

            bool maskIsLent = mHasMaskTecBos || mHasMaskFlorix;


            // Fields to lend out resp masksare only enabled if no mask is already lent.
            // and respmask briefing was received
            mCwrView.TxtSiFiMaskNrLentFlo.Enabled = isAllowed && !maskIsLent;
            mCwrView.DatSiFiMaskLentOnFlo.Enabled = isAllowed && !maskIsLent;
            mCwrView.TxtSiFiMaskNrLentTec.Enabled = isAllowed && !maskIsLent;
            mCwrView.DatSiFiMaskLentOnTec.Enabled = isAllowed && !maskIsLent;

            // Can only return a mask if one is lent out.
            mCwrView.TxtSiFiMaskNrBackFlo.Enabled = isAllowed && mHasMaskFlorix;
            mCwrView.DatSiFiMaskBackOnFlo.Enabled = isAllowed && mHasMaskFlorix;
            mCwrView.TxtSiFiMaskNrBackTec.Enabled = isAllowed && mHasMaskTecBos;
            mCwrView.DatSiFiMaskBackOnTec.Enabled = isAllowed && mHasMaskTecBos;
            //mCwrView.BtnSiFiMaskTicket.Enabled = isAllowed && maskIsLent;

            // Button should always be enabled
            mCwrView.BtnSiFiMaskTicket.Enabled = isAllowed;

            // Respiratory mask briefing directed Y/N
            mCwrView.RbtSiFiRespMaskBriefDir.Checked = mCwrView.RbtCoRespiratoryMaskBriefingYes.Checked;

            // Coordinator vehicle entrance long
            if (mCwrView.RbtCoVehicleEntranceLongYes.Checked)
            {
                //Umsetzung auf Sise
                mCwrView.RbtSiSeVehicleEntranceLong.Checked = true;

                //Deaktivierung Short auf Koordinator
                mCwrView.RbtCoVehicleEntranceShortNo.Enabled = false;
                mCwrView.RbtCoVehicleEntranceShortYes.Enabled = false;
            }

            if (mCwrView.RbtCoVehicleEntranceLongNo.Checked)
            {
                //Umsetzung auf Sise
                mCwrView.RbtSiSeVehicleEntranceLong.Checked = false;

                //Aktivierung Short auf Koordinator
                mCwrView.RbtCoVehicleEntranceShortNo.Enabled = true;
                mCwrView.RbtCoVehicleEntranceShortYes.Enabled = true;
            }

            //Coordinator vehicle entrance short
            if (mCwrView.RbtCoVehicleEntranceShortYes.Checked)
            {
                //Umsetzung auf Sise
                mCwrView.RbtSiSeVehicleEntranceShort.Checked = true;

                //Deaktivierung Long auf Koordinator
                mCwrView.RbtCoVehicleEntranceLongNo.Enabled = false;
                mCwrView.RbtCoVehicleEntranceLongYes.Enabled = false;
            }

            if (mCwrView.RbtCoVehicleEntranceShortNo.Checked)
            {
                //Umsetzung auf Sise
                mCwrView.RbtSiSeVehicleEntranceShort.Checked = false;

                //Aktivierung Long auf Koordinator
                mCwrView.RbtCoVehicleEntranceLongNo.Enabled = true;
                mCwrView.RbtCoVehicleEntranceLongYes.Enabled = true;
            }

            // SiteSecurity
            if (mCwrView.RbtSiSeVehicleEntranceShortReceivedNo.Checked)
            {
                mCwrView.RbtSiSeVehicleEntranceLongNo.Enabled = true;
                mCwrView.RbtSiSeVehicleEntranceLongYes.Enabled = true;
                mCwrView.DatSiSeVehicleEntranceLongReceivedOn.Enabled = true;
            }

            if (mCwrView.RbtSiSeVehicleEntranceShortReceivedYes.Checked)
            {
                mCwrView.RbtSiSeVehicleEntranceLongNo.Enabled = false;
                mCwrView.RbtSiSeVehicleEntranceLongYes.Enabled = false;
                mCwrView.DatSiSeVehicleEntranceLongReceivedOn.Enabled = false;
            }

            if (mCwrView.RbtSiSeVehicleEntranceLongNo.Checked)
            {
                mCwrView.RbtSiSeVehicleEntranceShortReceivedNo.Enabled = true;
                mCwrView.RbtSiSeVehicleEntranceShortReceivedYes.Enabled = true;
                mCwrView.DatSiSeVehicleEntranceShortReceivedOn.Enabled = true;
            }

            if (mCwrView.RbtSiSeVehicleEntranceLongYes.Checked)
            {
                mCwrView.RbtSiSeVehicleEntranceShortReceivedNo.Enabled = false;
                mCwrView.RbtSiSeVehicleEntranceShortReceivedYes.Enabled = false;
                mCwrView.DatSiSeVehicleEntranceShortReceivedOn.Enabled = false;
            }

            if (mCwrView.RbtReAccessApprentYes.Checked)
            {
                // Apprentice access, Radio buttons "Zutritt auszubildende"
                // When "Ausbildender" set to yes then automatically sets valid until date to 3.5 years in the future
                mCwrView.DatReAccessApprent.Enabled = mCwrView.RbtReAccessApprentYes.Checked;

                if (mCwrView.RbtReAccessApprentYes.Checked)
                {
                    // Interval for expiry date comes from Globals (parameter)
                    // Then turn it into number of days.
                    double expireInterval = Convert.ToDouble(Globals.GetInstance().ApprenticeExpiry);
                    DateTime appUntil = DateTime.Now.AddDays(365 * expireInterval);

                    // Set to first day of month
                    DateTime firstAppMonth = new DateTime(appUntil.Year, appUntil.Month, 1).AddMonths(1);

                    // Subtract one day so we get last day of previous month and show this in DateTime Picker
                    mCwrView.DatReAccessApprent.Value = firstAppMonth.AddDays(-1);
                }
            }
            if (mCwrView.RbtReAccessApprentNo.Checked) mCwrView.DatReAccessApprent.Value = DateTime.Now;
        }


        internal void ValidateAccessAuthSiteSecurity()
		{
            if (mCwrView.RbtSiSeAccessAuthNo.Checked)
			{
                mCwrView.RbtReAccessAuthYes.Enabled = false;
                mCwrView.RbtReAccessAuthNo.Checked = true;
                mCwrView.RbtReAccessAuthYes.Checked = false;
                mCwrView.DatReAccessAuthorizationOn.Enabled = false;

			}
            if (mCwrView.RbtSiSeAccessAuthYes.Checked)
			{
                mCwrView.RbtReAccessAuthYes.Enabled = true;
                mCwrView.RbtReAccessAuthNo.Checked = false;
                mCwrView.RbtReAccessAuthYes.Checked = true;
                mCwrView.DatReAccessAuthorizationOn.Enabled = true;
			}
		}
		

        internal void ValidateAccessAuthReception()
		{
            mCwrView.RbtSiSeAccessAuthYes.Checked = mCwrView.RbtReAccessAuthYes.Checked;
            mCwrView.RbtSiSeAccessAuthNo.Checked = mCwrView.RbtReAccessAuthNo.Checked;
		}


        /// <summary>
        /// Deals with enabling/disabling fields to do with photo is cards (SmartAct and the older Hitag chips) 
        /// SmarctAct photo id cards are requested (angeordet) by the coordinators
        /// and site security grants them once CWR has been imported back from SmartAct.
        /// --
        /// It's still possible to receive old-style id card with photo and Hitag chip
        /// but with restrictions: 
        /// 1) id card has to have been directed by coordinator
        /// 2) no SmartAct id card has been directed/received 
        /// 3) existing CWR, not new 
        /// Otherwise old-style id card with photo and Hitag chip is inactive
        /// </summary>
        internal void ValidateIdCardPhoto()
        {
            // This is the Delete photo id card button
            if (mCwrView.mEdvAdminAuthorization || 
                mCwrView.mSystemAdminAuthorization || 
                mCwrView.mCoordinatorAuthorization)
            {
                mCwrView.BtnCoSmartActDel.Enabled = (mCoWorker.SmartActNo > 0);
            }


            // Coordinator id card photo YES (Lichtbildausweis)
            if (mCwrView.RbtCoIdPhotoSmActYes.Checked)
            {
                // Lichtbildausweis angeordnet is checked
                mCwrView.RbtSiSeIdPhotoSmAct.Checked = true;

                if (mCwrView.mSiteSecurityAuthorization ||
                    mCwrView.mSiteSecurityLeaderAuthorization ||
                    mCwrView.mEdvAdminAuthorization ||
                    mCwrView.mSystemAdminAuthorization || 
                    mCwrView.mCoordinatorAuthorization)
                {
                    // Only enable the fields to set Received (erteilt)
                    // if CWR has been imported back from SmartAct meaning he has a photo id card,
                    // and he has received industrial safety briefing from coordinator (Sicherheitsunterweisung)
                    mCwrView.DatSiSeIdPhotoSmActRec.Enabled = (mCoWorker.SmartActNo > 0 && mCwrView.CbxCoIndSafetyBrfRecvd.Checked && !mCwrView.CbxSiSeIdPhotoSmActRec.Checked);
                    mCwrView.CbxSiSeIdPhotoSmActRec.Enabled = (mCoWorker.SmartActNo > 0 && mCwrView.CbxCoIndSafetyBrfRecvd.Checked && !mCwrView.CbxSiSeIdPhotoSmActRec.Checked);

                    // Enable PKI fields if CWR not in SmartAct yet
                    mCwrView.CbxCoPKI.Enabled = true;
                    mCwrView.BtnCoADSSearch.Enabled = (mCwrView.CbxCoPKI.Checked);
                    mCwrView.TxtCoWindowsID.Enabled = (mCwrView.CbxCoPKI.Checked);
                
                    // Enables Export button if CWR has has received industrial safety briefing from coordinator (Sicherheitsunterweisung)
                    // and if parameter in DB requires this.
                    if (Globals.GetInstance().IndSafety4ExpSmartAct)
                        mCwrView.BtnCoSmartActExp.Enabled = mCwrView.CbxCoIndSafetyBrfRecvd.Checked;
                    else
                        mCwrView.BtnCoSmartActExp.Enabled = true;


                    // Once the CWR has a photo Id card then "Lichtbildausweis gewünscht" cannot be set to No.
                    // photo Id card - Lichtbildausweis must be deleted using the button.
                    mCwrView.RbtCoIdPhotoSmActNo.Enabled = (mCoWorker.SmartActNo == 0);
                }

                // old-style id card with photo and Hitag chip inactive
                mCwrView.CbxSiSeIdPhotoHitagRec.Enabled = false;
                mCwrView.DatSiSeIdPhotoHitagRec.Enabled = false;
                mCwrView.LblSiSeIdPhotoHitagRecHint.Visible = false;
            }

            // Coordinator id card photo NO
            // Means photo Id card no longer required. Clear fields.
            if (mCwrView.RbtCoIdPhotoSmActNo.Checked)
            {
                // PKI fields disabled
                mCwrView.CbxCoPKI.Enabled = false;
                mCwrView.BtnCoADSSearch.Enabled = false;
                mCwrView.TxtCoWindowsID.Enabled = false;

                // Cannot receive photo id card, no export to SmartAct
                mCwrView.RbtSiSeIdPhotoSmAct.Checked = false;
                mCwrView.DatSiSeIdPhotoSmActRec.Enabled = false;
                mCwrView.CbxSiSeIdPhotoSmActRec.Enabled = false;
                mCwrView.BtnCoSmartActExp.Enabled = false;

                // It's possible to receive old-style id card with photo and Hitag chip
                // but with restrictions: 
                // 1) id card has to have been directed by coordinator
                // 2) no SmartAct id card has been directed/received 
                // 3) existing CWR, not new 
                // Otherwise old-style id card with photo and Hitag chip is inactive
                if (this.Status.Equals(AllFPASSDialogs.DIALOG_STATUS_UPDATE))
                {
                    bool canGetIdPhotoHitag = mCwrView.CbxSiSeIdPhotoHitagRec.Checked || (mIdPhotoHitagDirect && !mCwrView.CbxSiSeIdPhotoSmActRec.Checked);
                    mCwrView.CbxSiSeIdPhotoHitagRec.Enabled = canGetIdPhotoHitag;
                    mCwrView.DatSiSeIdPhotoHitagRec.Enabled = canGetIdPhotoHitag;

                    // show a decent hint if old-style id card is still active.
                    mCwrView.LblSiSeIdPhotoHitagRecHint.Visible = canGetIdPhotoHitag;
                }
                else
                {
                    mCwrView.CbxSiSeIdPhotoHitagRec.Enabled = false;
                    mCwrView.DatSiSeIdPhotoHitagRec.Enabled = false;
                    mCwrView.LblSiSeIdPhotoHitagRecHint.Visible = false;
                }
            }
        }

		/// <summary>
		/// Clears fields in tabs "Reception" and parts of "Coordinator".
		/// Re-activates radio buttons controlling CWR Access yes/no
        /// Resets comboboxes Excontractor and coordinator (must use decimal val 0)
		/// </summary>
		internal void ClearFields()
		{
			mCwrView.TxtReFirstname.Text = "";
			mCwrView.TxtReSurname.Text = "";
			mCwrView.TxtRePlaceOfBirth.Text = "";
			mCwrView.TxtReDateOfBirth.Text = "";	
			mCwrView.CobReCoordinator.SelectedValue = Convert.ToDecimal(0);
			mCwrView.CobReExternalContractor.SelectedValue = Convert.ToDecimal(0);
			mCwrView.TxtReVehicleRegistrationNumber.Text = "";
			mCwrView.TxtReVehicleRegistrationNumberFour.Text = "";
			mCwrView.TxtReVehicleRegistrationNumberThree.Text = "";
			mCwrView.TxtReVehicleRegistrationNumberTwo.Text = "";
            mCwrView.TxtReIDCardNumHitag2.Text = "";
            mCwrView.TxtReIDCardNumMifareNo.Text = "";
            mCwrView.TxtCoPhone.Text = "";
            mCwrView.TxtCoWindowsID.Text = "";
            mCwrView.CbxCoPKI.Checked = false;
            mCwrView.CbxCoPKI.Enabled = false;
			mCwrView.TxtReIDReaderHitag2.Text = "";
            mCwrView.TxtReIDReaderMifare.Text = "";
            mCwrView.RbtReSafetyInstructionsYes.Checked = false;
            mCwrView.RbtReSafetyInstructionsNo.Checked = true;
			mCwrView.DatReSafetyInstructionsOn.Value = now;
            
			mCwrView.RbtReAccessAuthNo.Checked = true;
            mCwrView.RbtSiSeAccessAuthNo.Enabled = false;
            mCwrView.RbtReAccessAuthYes.Checked = false;
            mCwrView.RbtReAccessAuthYes.Enabled = true;
            mCwrView.DatReAccessAuthorizationOn.Value = now;

			mCwrView.RbtCoVehicleEntranceLongNo.Enabled = true;
			mCwrView.RbtCoVehicleEntranceLongNo.Checked = true;
			mCwrView.RbtCoVehicleEntranceLongYes.Enabled = true;
			mCwrView.RbtCoVehicleEntranceLongYes.Checked = false;
			mCwrView.RbtCoVehicleEntranceShortNo.Enabled = true;
			mCwrView.RbtCoVehicleEntranceShortNo.Checked = true;
			mCwrView.RbtCoVehicleEntranceShortYes.Enabled = true;
			mCwrView.RbtCoVehicleEntranceShortYes.Checked = false;

			this.Status = AllFPASSDialogs.DIALOG_STATUS_NEW;
		}

		#endregion 

		#region ZKSStuff

		/// <summary>
		/// Reads id card number from a ZKS terminal.
        /// Processes error messages from ZKS and returns message to FPASS
		/// </exception>
		/// </summary>
        internal void ReadIdCardZKS(string pIdCardType) 
		{
            //FrmCoWorker mCwrView = (FrmCoWorker)mView;
            string idCardFieldVal;
            string lastReadNumber;
            string returnCode;

            mZKSProxy.IDCardType = pIdCardType;

            // Connect to ZKS and get id card nr
			mZKSProxy.Connect();
			lastReadNumber = mZKSProxy.GetLastReadIdCardNo();
			returnCode = mZKSProxy.ReadIdCardNo();           
			mZKSProxy.Disconnect();

            Globals.GetInstance().Log.Info("ReadPassNo: Kommunikation mit ZKS fertig.");

            string msgNoId = MessageSingleton.GetInstance().GetMessage(MessageSingleton.ZKS_NO_IDCARDNO);
            string msgNoConn = MessageSingleton.GetInstance().GetMessage(MessageSingleton.ZKS_NO_CONNECT);
            string msgIdUsed = MessageSingleton.GetInstance().GetMessage(MessageSingleton.ZKS_NO_IDCARDNO_USED);

			if (returnCode.Equals(msgNoId) || returnCode.Equals(msgNoConn) || returnCode.Equals(msgIdUsed))
			{
                Globals.GetInstance().Log.Error("ReadPassNo: Fehler beim Lesen der Ausweis (2): Auswertung des Returncodes "
                 + "Returncode aus ZKS: " + returnCode
                 + "FPASS meldet: "
                 + MessageSingleton.GetInstance().GetMessage(MessageSingleton.ZKS_COMP_NOT_IN_DB));

                idCardFieldVal = String.Empty; 
				throw new UIWarningException(returnCode);
			}			
			else 
			{
                Globals.GetInstance().Log.Info("ReadPassNo: Positiver Fall. Returncode aus ZKS: " + returnCode);
                idCardFieldVal = returnCode;  
			}

			// a valid idcardno has been read from terminal
			// the button "Ausweisnummer entfernen" will be deactivated,
			// until the change has been accepted and saved
			// to avoid that coworker data is deleted in ZKS with an unvalid idcardno
            mCwrView.BtnDelPassNumber.Enabled = false;

            mCoWorker.ZKSReturnCode = Globals.DB_NO;
            mCoWorker.ShowZKSColor();

            // Puts Id card number into the correct field (mifare number or hitag2 number)
            if (pIdCardType == IDCardTypes.Mifare)
                mCwrView.TxtReIDCardNumMifareNo.Text = idCardFieldVal;
            else
                mCwrView.TxtReIDCardNumHitag2.Text = idCardFieldVal;
		}


		/// <summary>
        /// Removes current coworkers ID cards.
        /// CWR is deleted in ZKS, and Id card numbers are also deleted in FPASS, 
        /// except when CWR has a photo Id card (Lichtbildausweis).
		/// </summary>
		internal void DeleteIdCardZKS() 
		{
            // error messages if ZKS not available
            string msgNoId = MessageSingleton.GetInstance().GetMessage(MessageSingleton.ZKS_NO_IDCARDNO);
            string msgNoConn = MessageSingleton.GetInstance().GetMessage(MessageSingleton.ZKS_NO_CONNECT);
            string msgNoDelCwr = MessageSingleton.GetInstance().GetMessage(MessageSingleton.ZKS_NO_DELETE_NO_CWR);

            // Note that as of FPASS V5 the user no longer needs to have ZKS read Id card number from terminal.
			
			decimal prmCoWorkerId = mCoWorker.CoWorkerID; // ! Coworker created and not saved yet => has no id!
            string returnCode = "";

            if (mCoWorker.ZKSReturnCode == Globals.DB_YES)
            {
                // Only connects to ZKS if current coworker is stored there.
                // Returncode is left empty
                mZKSProxy.Connect();
                returnCode = mZKSProxy.Delete(prmCoWorkerId);
                mZKSProxy.Disconnect();
            }
            else
            {
                Globals.GetInstance().Log.Info("DeleteIdCard: Current Cwr is not in ZKS (ZKS code is 'N'), so did not call ZKS dll.");
            }

            if (returnCode.Equals(msgNoDelCwr))
			{
                Globals.GetInstance().Log.Error("DeleteIdCard: Fehler beim Löschen der Ausweisnummer aus ZKS. FPASS meldet: " + msgNoDelCwr);

                // The coworker has NOT been successfully deleted in ZKS (maybe because not there)
                // Ask user if IDcard number of current coworker should be deleted in FPASS anyway
                if (MessageBox.Show(returnCode, TitleMessage, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) 
				{
					DeleteIdCardFPASS(prmCoWorkerId);
				}				
			} 
			else if (returnCode.Equals(msgNoConn))
			{
                Globals.GetInstance().Log.Error("DeleteIdCard: Fehler bei der Verbindung zum ZKS: kann die Ausweisnummer nicht löschen. FPASS meldet: " + msgNoConn);

				// The coworker has NOT been successfully deleted in ZKS (no connect to ZKS)
				// the idcardno will NOT be deleted in FPASS
				throw new UIWarningException(returnCode);
			} 
			else 
			{
                Globals.GetInstance().Log.Info("DeleteIdCard: Positiver Fall.");

				// OK: coworker was deleted in ZKS: delete appropriate details in FPASS			
				DeleteIdCardFPASS(prmCoWorkerId);
			}
		} 


		/// <summary>
		/// Deletes ID card number in FPASS and updates GUI to reflect changes saved in database.
        /// Note: coworkers with photo Id cards from SmartAct (Lichtbildausweis, also access long) 
        /// are deleted in ZKS but Id NOT deleted in FPASS. 
        /// This is because id card number comes from SmartAct and can be re-used if coworker granted new access
		/// </summary>
		private void DeleteIdCardFPASS(decimal pPrmCoWorkerId)
		{
            // Whatever else has gone on, this flag has to be set. Means record has been deleted in ZKS
            // or it was never there.
            mCoWorker.ZKSReturnCode = Globals.DB_NO;

            // FPASS does not delete the id card numbers for coworkers with photo Id cards 
            // (Lichtbildausweise) from SmartAct
            // Id cards read from ZKS ARE deleted.
            if (mCoWorker.SmartActNo == 0)
            {
                mCwrView.TxtReIDCardNumHitag2.Text = String.Empty;
                mCwrView.TxtReIDCardNumMifareNo.Text = String.Empty;                
            }

            mCoWorker.IdCardDeleted = true;

            mCwrView.Cursor = Cursors.WaitCursor;
		
			// When ID card is deleted other authorizations and briefings are deleted
			// 1. Save current data on GUI => validUNTIL is calculated, DB updated
			// 2. Call procedure to delete briefings etc now that ID card number is gone
			// 3. Re-initialise BO's to reflect changes made by procedure
			// 4. Update GUI: briefings deleted which is not same as setting to 'N'.
			Save();
			DeleteIdCardDB(pPrmCoWorkerId);		
			InitializeData();

			// Reset fields in GUI (no other way of doing this: cannot
            // call methods to reset GUI as would upset currently loaded data
			// reception tab
			mCwrView.RbtReSafetyInstructionsNo.Checked = true;
            mCwrView.RbtReAccessAuthNo.Checked = true;
       
            //// Re-enable radio buttons for Empfang. Removed. Not correct to re-enable these
            //mCwrView.RbtReAccessAuthYes.Enabled = true;

            // TODO: whay is this necessary for vehicle entry?
            /// Co-ord tab: vehicle entry
            mCwrView.RbtCoVehicleEntranceLongNo.Checked = true;
            mCwrView.RbtCoVehicleEntranceShortNo.Checked = true;

            /// Site security tab:
            /// Access authorization, receive ID photocard brief and receive vehicle entry
            mCwrView.RbtSiSeVehicleEntranceLong.Checked = false;
            mCwrView.RbtSiSeVehicleEntranceShort.Checked = false;
            mCwrView.RbtSiSeVehicleEntranceLongYes.Checked = false;
            mCwrView.RbtSiSeVehicleEntranceLongNo.Checked = false;
            mCwrView.RbtSiSeVehicleEntranceShortReceivedYes.Checked = false;
            mCwrView.RbtSiSeVehicleEntranceShortReceivedNo.Checked = false;

            mCwrView.Cursor = Cursors.Default;
		}

        /// <summary>
        /// Removes current coworkers ID cards.
        /// CWR is deleted in ZKS, and Id card numbers are also deleted in FPASS, 
        /// except when CWR has a photo Id card (Lichtbildausweis).
		/// </summary>
        internal void DeleteIdCardSmartAct()
        {
            // Deletes SmartAct details, in particular personalnummer 
            //mCwrView.TxtCoSmartActNo.Text = "";
            mCoWorker.SmartActNo = 0;
            mCwrView.CbxSiSeIdPhotoSmActRec.Checked = false;

            // Set Gewünscht = Nein to fire checkedchanged event and reset fields
            // Model.ValidateIdCardPhoto()
            mCwrView.RbtCoIdPhotoSmActNo.Checked = true;

            // Create CSV export for SmartAct with Delete flag
            // Then delete Id cards in ZKS and FPASS. This will delete SmartAct too as SmartActNo = 0
            ExportToSmartAct(mCoWorkerId.ToString(), SmartActActions.Delete, true);
            DeleteIdCardZKS();
        }

		#endregion
	}
}

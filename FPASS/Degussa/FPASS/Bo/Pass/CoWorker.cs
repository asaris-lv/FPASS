using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.IO;

using de.pta.Component.DbAccess;
using de.pta.Component.Errorhandling;
using de.pta.Component.DbAccess.Exceptions;

using Degussa.FPASS.FPASSApplication;
using Degussa.FPASS.Db;
using Degussa.FPASS.Db.DataSets;
using Degussa.FPASS.Bo;
using Degussa.FPASS.Util;
using Degussa.FPASS.Util.Validation;
using Degussa.FPASS.Util.UserManagement;
using Degussa.FPASS.Util.ErrorHandling;
using Degussa.FPASS.Util.Exceptions;
using Degussa.FPASS.Gui.Dialog.Pass;

using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.Util.Enums;

namespace Degussa.FPASS.Bo.Pass
{
	/// <summary>
	/// Represents a coworker in fpass.
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
	public class CoWorker : AbstractCoWorkerBO
	{
		#region Members

        /// <summary>
        /// Flags whether or not the ID card number or the reader number have changed
        /// </summary>
        protected bool mIdCardChanged;
        private bool mIdCardReaderChanged;
              
		/// <summary>
		/// id (pk) of the coworker
		/// </summary>
		protected		decimal			mCoWorkerID;
		
		/// <summary>
		/// id of the external contractor the coworker belongs to
		/// </summary>
		protected		decimal			mContractorID;

		/// <summary>
		/// id of the coordinator the coworker belongs to
		/// </summary>
		protected		decimal			mCoordinatorID;

		/// <summary>
		/// id of the craft the coworker is assigned to
		/// </summary>
		protected		decimal			mCraftID;

		/// <summary>
		/// id of the mandator where this coworker was registered
		/// </summary>
		protected		decimal			mMandatorID;

		/// <summary>
		/// id of the subcontractor the coworker belongs to
		/// </summary>
		protected		decimal			mSubcontractorID;

		/// <summary>
		/// id of the department the coworker is assigned to
		/// </summary>
		protected		decimal			mDepartmentID;

		/// <summary>
		/// id of the coordinator who accepted the checkout of the coworker
		/// </summary>
		protected		decimal			mCheckOffCoodID;
		
		/// <summary>
		/// id of the coordinator who accepted the checkin of the coworker
		/// </summary>
		protected		decimal			mEntryCoodID;

		/// <summary>
		/// status of the coworker (valid or invalid)
		/// </summary>
		protected		String			mStatus;

		/// <summary>
		/// name of the supervisor the coworker belongs to
		/// </summary>
		protected		String			mSupervisorName;

		/// <summary>
		/// telephone number of the supervisor the coworker belongs to
		/// </summary>
		protected		String			mSupervisorTel;
		
		/// <summary>
		/// orderno the coworker is asigned to
		/// </summary>
		protected		String			mOrderNo;
		
		/// <summary>
		/// Coworker's id card numbers
		/// </summary>
		protected decimal mIDCardNumHitag;
        protected decimal mIDCardNumMifare;
           
        /// <summary>
        /// Current User's id card reader numbers.
        /// These are set in "FFMA bearbeiten".
        /// </summary>
        protected int mIDReaderHitag;
        protected int mIDReaderMifare;
       
        /// <summary>
        /// Set to true when user clicks "delete id card button":
        /// do not validate Id card fields before save
        /// </summary>
        private bool mIdCardDeleted;



		/// <summary>
		/// surname of the coworker
		/// </summary>
		protected		String			mSurname;

		/// <summary>
		/// firstname of the coworker
		/// </summary>
		protected		String			mFirstname;

		/// <summary>
		/// place of birth of the coworker
		/// </summary>
		protected		String			mPlaceOfBirth;

		/// <summary>
		/// technicalkey of the coworker
		/// </summary>
		protected		decimal			mTK;
		
		/// <summary>
		/// personal number of the coworker
		/// </summary>
		protected String mPersonalNO;
		
		/// <summary>
		/// date of birth of the coworker
		/// </summary>
		protected DateTime mDateOfBirth;
		
		/// <summary>
		/// date when the pass was delivered
		/// </summary>
		protected DateTime mDeliveryDate;

		/// <summary>
        /// valid from: access granted from this date
		/// </summary>
		protected DateTime mValidFrom;

		/// <summary>
		/// valid until: access granted until
		/// </summary>
		protected DateTime mValidUntil;

        /// <summary>
        /// Holds coworker photo
        /// </summary>
        private MemoryStream mBlobData;

		/// <summary>
		/// Holds date of first entrance of the coworker
		/// </summary>
		protected		String			mEntryDateString;

		/// <summary>
		/// checkout date of the coworker
		/// </summary>
		protected		String			mCheckOffDateString;

		/// <summary>
		/// flag indicating if coworker got a security pass
		/// </summary>
		protected		bool			mSecurityPass;

		/// <summary>
		/// flag indicating if coworker is over eighteen
		/// </summary>
		protected		bool			mOverEighteen;
		
		/// <summary>
		/// flag indicating if coworkers order is finished
		/// </summary>
		protected		bool			mOrderComplete;
		
		/// <summary>
		/// flag indicating if any percautionary medical was directed
		/// </summary>
		protected		bool			mHasPrecMedical;

		/// <summary>
		/// returncode indicating if export to zks failed or suceeded
		/// </summary>
		protected		String			mZKSReturnCode;

		/// <summary>
		/// timestamp holds date when coworker data was changed
		/// </summary>
		protected		DateTime		mTimeStamp;

		/// <summary>
		/// user who changed the coworkers data
		/// </summary>
		protected		String			mChangeUser;

        /// <summary>
        /// KonzernID (WindowsId)
        /// </summary>
        protected String mWindowsId;

        /// <summary>
        /// SmartAct personnel number
        /// </summary>
        protected decimal mSmartActNo;

        /// <summary>
        /// Is current CWR in SmartAct?
        /// </summary>
        protected bool mInSmartAct;
        /// <summary>
        /// Has CWR's id card got a PKI chip?
        /// </summary>
        protected bool mPki;
        protected String mTelephoneNumber;
        protected DateTime? mImportDate;

        /// <summary>
        /// Access time for the coworker
        /// added on 2014-07-16 for FPASS v5
        /// </summary>
        private String mAccess;

		/// <summary>
		/// command used for select-statements
		/// </summary>
		private			IDbCommand		mSelComm; 
	
		/// <summary>
		/// identifier for select statement in configuration.xml to get all coworker data
		/// </summary>
		private	const string SELECT_COWORKER = "SelectCoWorker";

		/// <summary>
		/// identifier for insert-statement in configuration.xml insert a coworker 
		/// </summary>
		private const string INSERT_COWORKER = "InsertCoWorker";
		
		/// <summary>
		/// identifier for update statement in configuration.xml to upadte a coworker
		/// </summary>
		private const string UPDATE_COWORKER = "UpdateCoWorker";

		/// <summary>
		/// identifier for select-statement in configuration.xml to get all archive coworker data
		/// </summary>
		private	const string SELECT_COWORKER_ARCHIVE = "SelectCoWorkerArchive";

        private const string SELECT_IDCARD_EXISTS = "SelectIdCardExists";

	
		#endregion 

		#region Constructors

		/// <summary>
		/// Simple Constructor
		/// </summary>
		/// <param name="pModel">model the bo belongs to</param>
		public CoWorker(CoWorkerModel pCoWorkerModel) : base (pCoWorkerModel)
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
			// Get DataProvider from DbAccess component
			mProvider = DBSingleton.GetInstance().DataProvider;
		}	

		#endregion //End of Initialization

		#region Accessors
			
		/// <summary>
		/// Gets or sets Id card number (Hitag2)
		/// </summary>
		public String IdCardNumHitag 
		{
			get 
			{
				return mIDCardNumHitag.ToString();
			}
			set 
			{
				if ( value.ToString().Length > 0 ) 
				{
					mIDCardNumHitag = Convert.ToDecimal( value );
				} 
				else 
				{
					mIDCardNumHitag = 0;
				}
			}
		}

        /// <summary>
        /// Gets or sets Id card number (Mifare)
        /// </summary>
        public String IDCardNumMifare
        {
            get
            {
                return mIDCardNumMifare.ToString();
            }
            set
            {
                if (value.ToString().Length > 0)
                {
                    mIDCardNumMifare = Convert.ToDecimal(value);
                }
                else
                {
                    mIDCardNumMifare = 0;
                }
            }
        }

        /// <summary>
        /// Set to true when user clicks "delete id card button":
        /// do not validate Id card fields before save
        /// </summary>
        public bool IdCardDeleted
        {
            get { return mIdCardDeleted; }
            set { mIdCardDeleted = value; }
        }

        /// <summary>
        /// Current User's id card reader number (Hitag). 
        /// These are set in "FFMA bearbeiten".
        /// </summary>
        public int IDReaderHitag
        {
            get { return mIDReaderHitag; }
            set { mIDReaderHitag = value; }
        }

        /// <summary>
        /// Current User's id card reader number (Mifare). 
        /// These are set in "FFMA bearbeiten".
        /// </summary>
        public int IDReaderMifare
        {
            get { return mIDReaderMifare; }
            set { mIDReaderMifare = value; }
        }

        /// <summary>
        /// Flags whether or not the ID card reader number has changed
        /// </summary>
        public bool IdReaderChanged
        {
            get { return mIdCardReaderChanged; }
            set { mIdCardReaderChanged = value; }
        }

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public decimal CoordinatorID 
		{
			get 
			{
				return mCoordinatorID;
			}
		}

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public decimal ContractorID 
		{
			get 
			{
				return mContractorID;
			}
		}

		/// <summary>
        /// valid until: access granted until
		/// </summary>
		internal DateTime ValidUntil 
		{
			get 
			{
				return mValidUntil;
			}
			set 
			{
				mValidUntil = value;
			}
		}


        /// <summary>
		/// Simple getter and setter.
		/// </summary>
		internal bool MedicalsDirected 
		{
			get 
			{
				return mHasPrecMedical;
			}
		}

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public String Status 
		{
			get 
			{
				return mStatus;
			}
			set 
			{
				mStatus = value;
			}
		}

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public decimal CoWorkerID 
		{
			get 
			{
				return mCoWorkerID;
			}
			set 
			{
				mCoWorkerID = value;
			}
		}

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public DateTime ValidFrom 
		{
			get 
			{
				return mValidFrom;
			}
		}

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public bool OrderComplete 
		{
			get 
			{
				return mOrderComplete;
			}
		}

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public DateTime CheckOffdate 
		{
			get 
			{
				return Convert.ToDateTime(mCheckOffDateString);
			}
		}

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public String CheckOffDateString 
		{
			get 
			{
				return mCheckOffDateString;
			}
		}

        /// <summary>
        /// Simple getter and setter.
        /// </summary>
        public String TelephoneNumber
        {
            get { return mTelephoneNumber; }
           
        }

        /// <summary>
        /// Returns the WindowsId (KonzernId)
        /// </summary>
        public String WindowsId
        {
            get { return mWindowsId; }          
        }

        /// <summary>
        /// Returns or Sets SmartAct PersonalNo
        /// </summary>
        public decimal SmartActNo
        {
            get { return mSmartActNo; }
            set { mSmartActNo = value; }      
        }

        /// <summary>
        /// Returns Mifare id card number
        /// </summary>
        public decimal MifareNo
        {
            get { return mIDCardNumMifare; }        
        }

        /// <summary>
        /// Returns PKI flag true/false
        /// </summary>
        public bool Pki
        {
            get { return mPki; }          
        }

        /// <summary>
        /// Simple getter and setter.
        /// </summary>
        public DateTime? ImportDate
        {
            get { return mImportDate; }            
        }

        /// <summary>
        /// Gets or sets CWRs current access model.
        /// </summary>
        public String Access
        {
            get { return mAccess; }
            set { mAccess = value; }
        }

        /// <summary>
        /// Gets or sets CWRs returncode in ZKS
        /// </summary>
        public String ZKSReturnCode
        {
            get { return mZKSReturnCode; }
            set { mZKSReturnCode = value; }
        }


		#endregion 

		#region Methods 

		/// <summary>
		/// Initializes the coworker. Checks if it's a new coworker or if coworker was already
		/// saved in db.
		/// </summary>
		internal override void InitializeBO() 
		{
			mCoWorkerID    = mCoWorkerModel.CoWorkerId;
			mIdCardChanged = false;
            mIdCardDeleted = false;
			mZKSReturnCode = Globals.DB_NO;

			if ( mCoWorkerModel.Status.Equals(AllFPASSDialogs.DIALOG_STATUS_NEW ) ) 
			{
				InitializeNewCoWorker();
			} 
			else if ( mCoWorkerModel.Status.Equals(AllFPASSDialogs.DIALOG_STATUS_UPDATE ) ) 
			{
				ReadBOFromDB();
			}
		}

		/// <summary>
		/// Validates the coworker before it is saved. 
		/// </summary>
		internal override void Validate()
		{
            if (IDCardInUseFPASS()) 
			{
				return;
			}

			if ( mSurname.Length < 1 ) 
			{
				mCoWorkerModel.ErrorMessages.Append(MessageSingleton.GetInstance().GetMessage( MessageSingleton.INVALID_TEXT_SURNAME ) ); 
			}
			if ( mFirstname.Length < 1 ) 
			{
				mCoWorkerModel.ErrorMessages.Append(MessageSingleton.GetInstance().GetMessage( MessageSingleton.INVALID_TEXT_FIRSTNAME) );
			}
			if ( mPlaceOfBirth.Length < 1 ) 
			{
				mCoWorkerModel.ErrorMessages.Append(MessageSingleton.GetInstance().GetMessage( MessageSingleton.INVALID_TEXT_BIRTH_PLACE ) );
			}
			if ( mCoordinatorID < 1 ) 
			{
				mCoWorkerModel.ErrorMessages.Append(MessageSingleton.GetInstance().GetMessage( MessageSingleton.INVALID_COMBOBOX_COORD ) );
			}
			if ( mContractorID < 1 ) 
			{
				mCoWorkerModel.ErrorMessages.Append(MessageSingleton.GetInstance().GetMessage( MessageSingleton.INVALID_COMBOBOX_CONTRACTOR) );
			}

            // KonzernId (Windows ID) is required field if "with PKI chip" true
            if (mPki && mWindowsId.Length < 1)
            {
                mCoWorkerModel.ErrorMessages.Append(MessageSingleton.GetInstance().GetMessage(MessageSingleton.INVALID_TEXT_WINDOWSID));
            }

			int compareDateIsSet = mCoWorkerModel.CompareDates(mDateOfBirth, DateTime.MinValue );
			if ( compareDateIsSet == 0 ) 
			{
				mCoWorkerModel.ErrorMessages.Append(MessageSingleton.GetInstance().GetMessage( MessageSingleton.INVALID_BIRTHDATE ) );
			}

			if ( mCheckOffCoodID != 0 &&  mOrderComplete ) 
			{
				if ( !StringValidation.GetInstance().IsDateString(mCheckOffDateString) ) 
				{
					mCoWorkerModel.ErrorMessages.Append(MessageSingleton.GetInstance().GetMessage( MessageSingleton.INVALID_CHECKOFF ) );
				}
			}
			if ( mCheckOffDateString.Length > 0 ) 
			{
				if ( !StringValidation.GetInstance().IsDateString(mCheckOffDateString) ) 
				{
					mCoWorkerModel.ErrorMessages.Append(MessageSingleton.GetInstance().GetMessage( MessageSingleton.INVALID_CHECKOFFDATE ) );
				} 
			}

			if ( mEntryDateString.Length > 0 ) 
			{
				if ( !StringValidation.GetInstance().IsDateString(mEntryDateString) ) 
				{
					mCoWorkerModel.ErrorMessages.Append(MessageSingleton.GetInstance().GetMessage( MessageSingleton.INVALID_CHECKINDATE ) );
				} 
			}

			if ( mCoWorkerModel.ErrorMessages.Length > 1 ) 
			{
				return;
			} 
			else 
			{
				CalculateOverEighteen();
			}
		}


		/// <summary>
		/// Saves current coworker to database, also determines whether to perform an insert or an update.
		/// DML statement is executed only when data has changed.
		/// <exception cref="de.pta.Component.Errorhandling.UIFatalException">
		/// Is thrown if dml statement failed.
		/// </exception>
		/// </summary>
		internal override void Save() 
		{		
			if ( mChanged ) 
			{
				mZKSReturnCode = Globals.DB_NO;
				
                if (mCoWorkerModel.Status.Equals(AllFPASSDialogs.DIALOG_STATUS_NEW ) ) 
				{
					InsertCoWorker();					
				} 
				else if ( mCoWorkerModel.Status.Equals(AllFPASSDialogs.DIALOG_STATUS_UPDATE ) ) 
				{
					UpdateCoWorker();
				} 
				mCoWorkerModel.ZKSChanged = true;

        		mChanged = false;
				mIdCardChanged = false;
                mIdCardReaderChanged = false;
				mCoWorkerModel.Status = AllFPASSDialogs.DIALOG_STATUS_UPDATE;	
			} 
		}


		/// <summary>
		/// Checks if a coworker with the given date of birth, place of birth,
		/// exco name and mandator id already exists in db. If it does, give warning message
		/// </summary>
		/// <exception cref="de.pta.Component.Errorhandling.UIWarningException">Is thrown if coworker already exists</exception>
		/// <exception cref="de.pta.Component.Errorhandling.UIFatalException">Is thrown if dml statement failed.</exception>	
		internal void CheckCoWorkerExistence() 
		{
			int recs = 0;
			try 
			{
				// Get DB provider, make an SQL statement, bind parameters and execute SQL
				IProvider provider  = DBSingleton.GetInstance().DataProvider;
				IDbCommand mCommSel = provider.CreateCommand("CheckCoWorkerExistence");

                provider.SetParameter(mCommSel, ":CWR_EXCO_ID", mContractorID);
                provider.SetParameter(mCommSel, ":CWR_SURNAME", mSurname.Trim().ToUpper());
                provider.SetParameter(mCommSel, ":CWR_MND_ID", UserManagementControl.getInstance().CurrentMandatorID);

				IDataReader mDR = mProvider.GetReader(mCommSel);
				while (mDR.Read())
				{				
					recs++;

				}
				mDR.Close();
	
				if ( recs > 0 ) 
				{
					throw new UIWarningException (MessageSingleton.GetInstance().GetMessage( MessageSingleton.COWORKER_EXISTS ) ) ;
				}
			}
			catch ( System.Data.OracleClient.OracleException oe ) 
			{
				mViewCoWorker.Cursor = Cursors.Default;
				throw new UIFatalException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR), oe);
			} 
			catch ( DbAccessException dba ) 
			{
				mViewCoWorker.Cursor = Cursors.Default;
				throw new UIFatalException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR), dba);
			}
		}

		/// <summary>
		/// Copies data from BO into GUI
		/// </summary>
		internal override void CopyIn()
		{
			CopyInReceptionTab();
			CopyInCoordinatorTab();
		}

		/// <summary>
		/// Copies data from BO into GUI, tab "Reception" (Empfang).
        /// Note: setting excontractor and coordinator will fire SelectedIndexChanged event on comboboxes.
        /// It is possible that excontractor is no longer in list if exco is invalid.
        /// Sets access status as colour: red: run out, yellow: within -indicator- days, green: more than -indicator- days
		/// </summary>
        private void CopyInReceptionTab()
        {
            mViewCoWorker.TxtReSurname.Text = mSurname;
            mViewCoWorker.TxtReFirstname.Text = mFirstname;
            mViewCoWorker.TxtRePlaceOfBirth.Text = mPlaceOfBirth;

            if (mDateOfBirth.CompareTo(DateTime.MinValue) > 0)
            {
                mViewCoWorker.TxtReDateOfBirth.Text = mDateOfBirth.ToString().Substring(0, 10);
            }
            else
            {
                mViewCoWorker.TxtReDateOfBirth.Text = String.Empty;
            }

            // Coworker photo from SmartAct
            if (mInSmartAct &&  null != mBlobData)
            {
                mBlobData.Position = 0;
                mViewCoWorker.CoWorkerPhotoBox.Image = Image.FromStream(mBlobData);
            }

            // Comboboxes Coordinator, Excontr
            mViewCoWorker.CobReCoordinator.SelectedValue = mCoordinatorID;
            mViewCoWorker.CobReExternalContractor.SelectedValue = mContractorID;

            //  Datefields at Foot of form
            mViewCoWorker.DatDeliveryDate.Value = mDeliveryDate;
            mViewCoWorker.DatPassValidFrom.Value = mValidFrom;
            mViewCoWorker.TxtPassValidUntil.Text = Convert.ToString(mValidUntil).Substring(0, 10);

            CopyInIdCard();

            // Disables fields if this CWR has a WindowsId or has been imported from SmartAct
            bool hasWindowsId = (mWindowsId.Length > 0);
            mViewCoWorker.TxtReFirstname.Enabled = !hasWindowsId && !mInSmartAct;            
            mViewCoWorker.TxtReSurname.Enabled = !hasWindowsId && !mInSmartAct; 
           
            ShowStatusColor();
            ShowZKSColor();
        }


		/// <summary>
		/// Shows data of the bo in the gui in register coordinator. Data is "copied in the gui".
		/// </summary>
		private void CopyInCoordinatorTab() 
		{
            // FPASS V5: new fields PKI und WindowsID
            mViewCoWorker.TxtCoPhone.Text = mTelephoneNumber;
            mViewCoWorker.TxtCoWindowsID.Text = mWindowsId;
            mViewCoWorker.TxtCoSmartActNo.Text = (mSmartActNo == 0 ? "" : mSmartActNo.ToString());
            mViewCoWorker.TxtCoFpassNo.Text = mPersonalNO;

            // PKI fields can only be edited before CWR exported to SmartAct
           // mViewCoWorker.CbxCoPKI.Enabled = !mInSmartAct && mPki;
            //mViewCoWorker.BtnCoADSSearch.Enabled = !mInSmartAct;
            // KonzernId only editable if CWR has a PKIid but not in SmartACt
            //mViewCoWorker.TxtCoWindowsID.Enabled = (mPki && !mInSmartAct);

            // Values of PKI fields
            mViewCoWorker.CbxCoPKI.Checked = mPki;
			mViewCoWorker.TxtCoSupervisor.Text = mSupervisorName;
			mViewCoWorker.TxtCoTelephoneNumber.Text = mSupervisorTel;
			mViewCoWorker.CboCoSubcontractor.SelectedValue = Convert.ToDecimal(mSubcontractorID);
			
			// Comboboxes Craft number & name are dependent on one another
            mViewCoWorker.CboCoCraftNumber.SelectedValue = mCraftID;
            mViewCoWorker.CboCoDepartment.SelectedValue = mDepartmentID;
			
			if (mSecurityPass)
			{
				mViewCoWorker.RbtCoSafetyPassYes.Checked = true;
			}
			else
			{
				mViewCoWorker.RbtCoSafetyPassNo.Checked = true;
			}

			// Order number
			mViewCoWorker.TxtCoOrderNumber.Text = mOrderNo;

			// Order Complete
			if (mOrderComplete)
			{
				mViewCoWorker.RbtCoOrderDoneYes.Checked = true;
			}
			else
			{
				mViewCoWorker.RbtCoOrderDoneNo.Checked = true;
			}

			if (mHasPrecMedical)
			{
				mViewCoWorker.RbtCoPrecautionaryMedicalYes.Checked = true;
			}
			else
			{
				mViewCoWorker.RbtCoPrecautionaryMedicalNo.Checked = true;
			}
			
			// CheckIn always same as validfrom
			mViewCoWorker.TxtCoCheckIn.Text  = this.mValidFrom.ToString().Substring(0,10);
			mViewCoWorker.TxtCoCheckOff.Text = this.mCheckOffDateString;
		}


        /// <summary>
        /// Has all logic for copying in id card values on Reception (Empfang) and Site Security (Werkschutz) tabls
        /// </summary>
        private void CopyInIdCard()
        {
            // ID cards and readers. If values are 0 show empty string
            mViewCoWorker.TxtReIDCardNumHitag2.Text = (mIDCardNumHitag == 0 ? "" : mIDCardNumHitag.ToString());
            mViewCoWorker.TxtReIDCardNumMifareNo.Text = (mIDCardNumMifare == 0 ? "" : mIDCardNumMifare.ToString());

            // Set reader numbers to "" if no values found
            mViewCoWorker.TxtReIDReaderHitag2.Text = (mIDReaderHitag == 0 ? "" : mIDReaderHitag.ToString());
            mViewCoWorker.TxtReIDReaderMifare.Text = (mIDReaderMifare == 0 ? "" : mIDReaderMifare.ToString());

            // Id card buttons are disabled if CWR has been imported back from SmartAct
            mViewCoWorker.BtnRePassNrHitag.Enabled = !mInSmartAct;
            mViewCoWorker.BtnRePassNrHitagUSB.Enabled = !mInSmartAct;

            mViewCoWorker.BtnRePassNrMifare.Enabled = !mInSmartAct;
            mViewCoWorker.BtnRePassNrMifareUSB.Enabled = !mInSmartAct;

            mViewCoWorker.BtnDelPassNumber.Enabled = mZKSReturnCode.Equals(Globals.DB_YES);

            // If Cwr is not in SmartAct then site security cannot grant photo id card
            // (not possible to set "Lichtbildausweis erhalten" to Y).
            // mViewCoWorker.CbxSiSeIdPhotoSmActRec.Enabled = mInSmartAct;
            // no point in doing this here, decided when IndustrialSafetyAuthorization is loaded
            // TODO in future release: separate loading data (BOs) and setting enbabe/disable fields base don data (one central method)
        }


        /// <summary>
        /// Shows access status colour of field "Valid until" depending on how long coworker has access in FPASS.
        /// (note status AllFPASSDialogs.CWR_DIALOG_MODE_ARCHIVE not set correctly, cannot grey out field in arch mode).
        /// </summary>
        internal void ShowStatusColor()
        {
            double accessPara = Convert.ToDouble(Globals.GetInstance().AccessIndicator);

            // Shows access status colour of field "Valid until"
            // Yellow when less than x days between ValidFrom and ValidUntil
            if (mValidUntil > DateTime.Now
                && mValidUntil > mValidFrom
                && mValidUntil <= mValidFrom.AddDays(accessPara))
            {
                mViewCoWorker.TxtPassValidUntil.BackColor = Color.Yellow;
                mViewCoWorker.TxtPassValidUntil.ForeColor = Color.Black;
            }
            // Yellow when less than x days between Now and ValidUntil
            else if (mValidUntil > DateTime.Now
                && mValidUntil > mValidFrom
                && mValidUntil <= DateTime.Now.AddDays(accessPara))
            {
                mViewCoWorker.TxtPassValidUntil.BackColor = Color.Yellow;
                mViewCoWorker.TxtPassValidUntil.ForeColor = Color.Black;
            }
            else if (
                (mValidUntil > DateTime.Now && mValidUntil > mValidFrom.AddDays(accessPara))
                || (mValidUntil > DateTime.Now && mValidUntil > DateTime.Now.AddDays(accessPara))
                )

            {
                // Green if ValidUntil far enough in the future
                mViewCoWorker.TxtPassValidUntil.BackColor = Color.FromArgb(0, 210, 0);
                mViewCoWorker.TxtPassValidUntil.ForeColor = Color.Black;
            }           
            else
            {
                mViewCoWorker.TxtPassValidUntil.BackColor = Color.Red;
                mViewCoWorker.TxtPassValidUntil.ForeColor = Color.White;
            }
        }


        /// <summary>
        /// Sets field ReturncodeZKS to green or red depending on J/N.
        /// </summary>
        internal void ShowZKSColor()
        {
            // Sets field ReturncodeZKS to green or red depending on J/N
            mViewCoWorker.TxtZKSRetCode.ForeColor = Color.White;

            if (mZKSReturnCode.Equals(Globals.DB_YES))
            {
                mViewCoWorker.TxtZKSRetCode.Text = Globals.DB_YES_SHOW;
                mViewCoWorker.TxtZKSRetCode.BackColor = Color.FromArgb(0, 210, 0);
                mViewCoWorker.TxtZKSRetCode.ForeColor = Color.Black;
            }
            else
            {
                mViewCoWorker.TxtZKSRetCode.Text = Globals.DB_NO_SHOW;
                mViewCoWorker.TxtZKSRetCode.BackColor = Color.Red;
                mViewCoWorker.TxtZKSRetCode.ForeColor = Color.White;
            }
        }

		/// <summary>
		/// Data is "copied out of the gui" in the bo. Checks if user made changes.
		/// </summary>
		internal override void CopyOut()
		{
			CopyOutReceptionTab();
			CopyOutCoordinatorTab();
		}

		/// <summary>
		/// Data is validated and copied out of the gui in register tab "Reception" in the bo.
		/// </summary>
		private void CopyOutReceptionTab() 
		{
            int compareDate = 0;

            if (!mViewCoWorker.TxtReSurname.Text.Equals(mSurname))
            {
                mSurname = mViewCoWorker.TxtReSurname.Text;
                mChanged = true;
                mCoWorkerModel.ShouldExportSmartAct = true;
            }

			if ( !mViewCoWorker.TxtReFirstname.Text.Equals(mFirstname) ) 
			{
				mFirstname = mViewCoWorker.TxtReFirstname.Text;
				mChanged = true;
                mCoWorkerModel.ShouldExportSmartAct = true;
			}

            // Validates date of birth
			if ( mViewCoWorker.TxtReDateOfBirth.Text.Length > 0 ) 
			{
				String dateText = mViewCoWorker.TxtReDateOfBirth.Text;
				if (StringValidation.GetInstance().IsDateString(dateText))
				{
                    DateTime d = Convert.ToDateTime(dateText);
					int compare = mCoWorkerModel.CompareDates(d, mDateOfBirth);
					if ( compare != 0 ) 
					{
                        mDateOfBirth = Convert.ToDateTime(dateText);
						mChanged = true;
					}
				} 
				else 
				{
					mCoWorkerModel.ErrorMessages.Append(MessageSingleton.GetInstance().
						GetMessage( MessageSingleton.INVALID_BIRTHDATE ));
				}
			}

			if ( !mViewCoWorker.TxtRePlaceOfBirth.Text.Equals(mPlaceOfBirth) ) 
			{
				mPlaceOfBirth = mViewCoWorker.TxtRePlaceOfBirth.Text;
				mChanged = true;
			}

			if ( !mCoordinatorID.Equals(Convert.ToDecimal(mViewCoWorker.CobReCoordinator.SelectedValue)) )
			{
				mCoordinatorID = Convert.ToDecimal( mViewCoWorker.CobReCoordinator.SelectedValue );
				mChanged = true;
                mCoWorkerModel.ShouldExportSmartAct = true;
			}

			if ( !mContractorID.Equals(Convert.ToDecimal(mViewCoWorker.CobReExternalContractor.SelectedValue) ) ) 
			{
				mContractorID = Convert.ToDecimal( mViewCoWorker.CobReExternalContractor.SelectedValue );
				mChanged = true;
                mCoWorkerModel.ShouldExportSmartAct = true;
			}

            compareDate = mCoWorkerModel.CompareDates(mViewCoWorker.DatPassValidFrom.Value, mValidFrom);
            if (compareDate != 0) 
			{
				// Set validFrom to date selected in GUI
			    // and current time
			    mValidFrom = mViewCoWorker.DatPassValidFrom.Value.Date;
			    TimeSpan timeSpan = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
			    
			    mValidFrom += timeSpan;
				mChanged = true;
			}

            compareDate = mCoWorkerModel.CompareDates(mViewCoWorker.DatDeliveryDate.Value, mDeliveryDate);
            if (compareDate != 0) 
			{
				mDeliveryDate = mViewCoWorker.DatDeliveryDate.Value;
				mChanged = true;
			}

            // Id card numbers and ZKS 
            // ---------------------------
			// Validate Hitag2 Id card number
            CopyOutIdCard(IDCardTypes.Hitag2);
      
            // Do same for Mifare Id card number
            CopyOutIdCard(IDCardTypes.Mifare);

            CopyOutIdReader(IDCardTypes.Hitag2);
            CopyOutIdReader(IDCardTypes.Mifare);

            // Clear memory where cwr photo was held
            mBlobData = null;
            mViewCoWorker.CoWorkerPhotoBox.Image = null;
		}

        
        /// <summary>
        /// Validates and Copies out values for Id card numbers
        /// </summary>
        /// <param name="pIdCardType">id card type: hitag2 or Mifare. See <see cref="IDCardTypes"/>IDCardTypes</param>
        private void CopyOutIdCard(string pIdCardType)
        {
            
            // Establish which Id card type is being validated
            decimal idCardNum;
            decimal prevIdCardNum = (pIdCardType == IDCardTypes.Mifare ? mIDCardNumMifare : mIDCardNumHitag);
            string idCardFieldVal = (pIdCardType == IDCardTypes.Mifare ? mViewCoWorker.TxtReIDCardNumMifareNo.Text : mViewCoWorker.TxtReIDCardNumHitag2.Text);
            string errMessage = (pIdCardType == IDCardTypes.Mifare ? "'Ausweisnummer (Mifare)'" : "'Ausweisnummer (Hitag2)'");

            
            // Id card numbers and ZKS 
            // ---------------------------
            // Validate Id card number.
            // Note that empty Id card is allowed.
            if (idCardFieldVal.Trim().Length > 0)
            {
                decimal result;

                if (!decimal.TryParse(idCardFieldVal, out result))
                {
                    // error if value in field is not numeric
                    string msgNum = MessageSingleton.GetInstance().GetMessage(MessageSingleton.VALUE_NOT_NUMERIC);
                    mCoWorkerModel.ErrorMessages.Append(String.Format(msgNum, errMessage));
                }
                else
                {
                    // Sets id card number if value has changed
                    idCardNum = Convert.ToDecimal(idCardFieldVal);

                    if (idCardNum != prevIdCardNum)
                    {
                        mChanged = true;
                        mIdCardChanged = true;

                        if (pIdCardType == IDCardTypes.Mifare)
                            mIDCardNumMifare = idCardNum;
                        else
                            mIDCardNumHitag = idCardNum;
                    }
                }
            }
            else if (idCardFieldVal.Trim().Length == 0 && prevIdCardNum > 0)
            {
                // the idcardno-text-field is empty AND the Coworker (BO) had an idcardno before
                // it means that the idcardno has been deleted (user cleared ID field manually)
                // This is not allowed if ZKS=Y, as it means Cwr has id card in ZKS but not in FPASS.
                if (mZKSReturnCode == Globals.DB_YES)
                {
                    mCoWorkerModel.ErrorMessages.Append(MessageSingleton.GetInstance().GetMessage(MessageSingleton.IDCARD_EMPTY_NOTALLOWED));
                }
                else
                {
                    mChanged = true;
                    mIdCardChanged = true;

                    if (pIdCardType == IDCardTypes.Mifare)
                        mIDCardNumMifare = 0;
                    else
                        mIDCardNumHitag = 0;
                }
            }
        }


        /// <summary>
        /// Saves the current user's id card reader numbers.
        /// </summary>
        internal void CopyOutIdReader(string pRdrType)
        {
            string msgNum;

            string rdrFrmText = (pRdrType == IDCardTypes.Mifare ? mViewCoWorker.TxtReIDReaderMifare.Text.Trim() : mViewCoWorker.TxtReIDReaderHitag2.Text.Trim());
            string errText = (pRdrType == IDCardTypes.Mifare ? "'Ausweislesernummer (Mifare)'" : "'Ausweislesernummer (Hitag2)'");


            // ID card readers
            // ------------------
            // ID card READER number for Hitag2
            if (rdrFrmText.Length > 0)
            {
                decimal result;

                if (!decimal.TryParse(rdrFrmText, out result))
                {
                    msgNum = MessageSingleton.GetInstance().GetMessage(MessageSingleton.VALUE_NOT_NUMERIC);
                    mCoWorkerModel.ErrorMessages.Append(String.Format(msgNum, errText));
                }
                else
                {
                    int rferNr = Convert.ToInt32(rdrFrmText);

                    if (pRdrType == IDCardTypes.Mifare && rferNr != mIDReaderMifare)
                    {
                        mIDReaderMifare = rferNr;
                        mIdCardReaderChanged = true;
                    }
                    else if (pRdrType == IDCardTypes.Hitag2 && rferNr != mIDReaderHitag)
                    {
                        mIDReaderHitag = rferNr;
                        mIdCardReaderChanged = true;
                    }
                }
            }
        }

       
		/// <summary>
		/// Data is copied out of the gui in register coordinator" in the bo.
		/// If user enters rubbish in CboCoSubcontractor, selected value is null => error
		/// </summary>
		private void CopyOutCoordinatorTab() 
		{
            if (!mViewCoWorker.TxtCoPhone.Text.Equals(mTelephoneNumber))
            {
                mTelephoneNumber = mViewCoWorker.TxtCoPhone.Text;
                mChanged = true;
            }

            if (!mViewCoWorker.TxtCoWindowsID.Text.Equals(mWindowsId))
            {
                mWindowsId = mViewCoWorker.TxtCoWindowsID.Text;
                mChanged = true;
                mCoWorkerModel.ShouldExportSmartAct = true;
            }

            // If SmartAct Nr has been emptied by button "Delete SmartAct id card".
            // Otherwise this field cannot be changed
            if (mViewCoWorker.TxtCoSmartActNo.Text.Length > 0 && mSmartActNo == 0)
            {
                mSmartActNo = 0;
            }
              

            // Signal export to SmartAct if "PKI" set to true
            if (mViewCoWorker.CbxCoPKI.Checked != mPki)
            {
                mPki = mViewCoWorker.CbxCoPKI.Checked;
                mChanged = true;

                if (mPki)
                    mCoWorkerModel.ShouldExportSmartAct = true;                 
            }
         
            if ( null != mViewCoWorker.CboCoSubcontractor.SelectedValue )
			{
				if ( ! this.mSubcontractorID.ToString().Equals(mViewCoWorker.CboCoSubcontractor.SelectedValue.ToString())) 
				{
					this.mSubcontractorID = Convert.ToDecimal(mViewCoWorker.CboCoSubcontractor.SelectedValue);
					mChanged = true;
				}
			}

			if ( !mViewCoWorker.TxtCoOrderNumber.Text.Equals(mOrderNo) ) 
			{
				mOrderNo = mViewCoWorker.TxtCoOrderNumber.Text;
				mChanged = true;
			}

			if ( !mDepartmentID.Equals(Convert.ToDecimal(mViewCoWorker.CboCoDepartment.SelectedValue)) )
			{
				mDepartmentID = Convert.ToDecimal( mViewCoWorker.CboCoDepartment.SelectedValue );
				mChanged = true;
			}

			if ( !mCraftID.Equals( Convert.ToDecimal(mViewCoWorker.CboCoCraftNumber.SelectedValue))
				|| !mCraftID.Equals( Convert.ToDecimal(mViewCoWorker.CboCoCraftName.SelectedValue)) )
			{
				mCraftID = Convert.ToDecimal( mViewCoWorker.CboCoCraftNumber.SelectedValue );
				mChanged = true;
			}

			// Radio buttons security pass (Sicherheitspass)
			if (mSecurityPass && mViewCoWorker.RbtCoSafetyPassNo.Checked)
			{
				mSecurityPass = false;
				mChanged = true;
			}
			else if ( !mSecurityPass && mViewCoWorker.RbtCoSafetyPassYes.Checked)
			{
				mSecurityPass = true;
				mChanged = true;
			}

			// Order number
			if ( !mViewCoWorker.TxtCoOrderNumber.Text.Equals(mOrderNo) )
			{
				mOrderNo = mViewCoWorker.TxtCoOrderNumber.Text;
				mChanged = true;
			}

			if (mHasPrecMedical && mViewCoWorker.RbtCoPrecautionaryMedicalNo.Checked)
			{
				mHasPrecMedical = false;
				mChanged = true;
			}
			else if (!mHasPrecMedical && mViewCoWorker.RbtCoPrecautionaryMedicalYes.Checked)
			{
				mHasPrecMedical = true;
				mChanged = true;
			}

			// Entry date always the same as validfrom
			mEntryDateString = ValidFrom.ToString().Substring(0,10);

			// Check on / off
			if ( !mEntryDateString.Equals(mViewCoWorker.TxtCoCheckIn.Text) )
			{
				mEntryDateString = mViewCoWorker.TxtCoCheckIn.Text;
				mEntryCoodID = UserManagementControl.getInstance().CurrentUserID;
				mChanged = true;
			}

			// Order Complete
			if (mOrderComplete && mViewCoWorker.RbtCoOrderDoneNo.Checked)
			{
				mOrderComplete = false;
				mCheckOffCoodID = UserManagementControl.getInstance().CurrentUserID;
				mChanged = true;
			}
			else if (!mOrderComplete && mViewCoWorker.RbtCoOrderDoneYes.Checked)
			{
				mOrderComplete = true;
				mCheckOffCoodID = UserManagementControl.getInstance().CurrentUserID;
				mChanged = true;
			}
			
			if (!mCheckOffDateString.Equals(mViewCoWorker.TxtCoCheckOff.Text)) 
			{
				mCheckOffDateString = mViewCoWorker.TxtCoCheckOff.Text;
				mCheckOffCoodID = UserManagementControl.getInstance().CurrentUserID;
				mChanged = true;
			}
		}


		/// <summary>
        /// Checks if current coworker's current idcarda (Hitag2, Mifare) are already in use in FPASS
		/// </summary>
		/// <returns>true if in use</returns>
		private bool IDCardInUseFPASS() 
		{
            if (mIdCardChanged)
            {
                try
                {
                    IProvider provider = DBSingleton.GetInstance().DataProvider;
                    IDbCommand mCommSel = provider.CreateCommand(SELECT_IDCARD_EXISTS);

                    provider.SetParameter(mCommSel, ":CWR_ID", mCoWorkerID);
                    provider.SetParameter(mCommSel, ":CWR_MND_ID", UserManagementControl.getInstance().CurrentMandatorID);
                    provider.SetParameter(mCommSel, ":CWR_IDCARDNO", this.mIDCardNumHitag);
                    provider.SetParameter(mCommSel, ":CWR_MIFARENO", this.mIDCardNumMifare);

                    IDataReader mDR = mProvider.GetReader(mCommSel);
                    int recs = 0;
                    string errMsg = "";

                    while (mDR.Read())
                    {
                        // Check Hitag2 number: if another CWR record has same idcard number as current CWR then create error message
                        if (!mDR["CWR_IDCARDNO"].Equals(DBNull.Value) && Convert.ToDecimal(mDR["CWR_IDCARDNO"]) ==  mIDCardNumHitag)
                            errMsg += String.Format(MessageSingleton.GetInstance().GetMessage(MessageSingleton.IDCARD_INUSE), "Hitag2-Nummer " + mIDCardNumHitag);

                        // Do same for Mifare number
                        if (!mDR["CWR_MIFARENO"].Equals(DBNull.Value) && Convert.ToDecimal(mDR["CWR_MIFARENO"]) == mIDCardNumMifare)
                            errMsg += String.Format(MessageSingleton.GetInstance().GetMessage(MessageSingleton.IDCARD_INUSE), "Mifare-Nummer " + mIDCardNumMifare);

                        recs++;
                    }
                    mDR.Close();

                    if (recs > 0)
                    {
                        mCoWorkerModel.ErrorMessages.Append(errMsg);
                        return true;
                    }
                    else return false;
                }
                catch (System.Data.OracleClient.OracleException oe)
                {
                    throw new UIFatalException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR), oe);
                }
                catch (DbAccessException dba)
                {
                    throw new UIFatalException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR), dba);
                }
            }
            else return false;
		}


        private void ReadCoWorkerPhoto()
        {
            mSelComm = null;
            mSelComm = mProvider.CreateCommand("SelectCoWorkerPhoto"); 

            mProvider.SetParameter(mSelComm, ":CWR_ID", mCoWorkerID);

            // Open data reader to get ExContractor data
            IDataReader mDR = mProvider.GetReader(mSelComm);
           
            long retval;                            // The bytes returned from GetBytes.
            long startIndex = 0;                    // The starting position in the BLOB output.

            // Loop thru records 
            // CWR id is the primary key so there should only ever be 1 record
            while (mDR.Read())
            {
                int bufferSize = Globals.GetInstance().MaxPhotoBufferSize * 1024;
                byte[] outbyte = new byte[bufferSize];  // The BLOB byte[] buffer to be filled by GetBytes.

                // Reset the starting byte for the new BLOB.
                startIndex = 0;

                // Read the bytes into outbyte[] and retain the number of bytes returned.
                retval = mDR.GetBytes(3, startIndex, outbyte, 0, bufferSize);

                mBlobData = new MemoryStream(outbyte);
            }

            mDR.Close();    
        }

		/// <summary>
		/// Fills coworker with data from db (coworker itself, table FPASS_COWORKER)
		/// Throw error if datareader finds no record for this ID
		/// </summary>
		/// <exception cref="de.pta.Component.Errorhandling.UIFatalException">Is thrown if dml statement failed.</exception>
		/// <exception cref="Degussa.FPASS.Util.Exceptions.ActionCancelled">is thrown if no coworker data 
		/// are found for the given CWR ID (e.g. deleted in another session). conflict
		/// Use this exception so as not to confuse with other warning exceptions</exception>
		private void ReadBOFromDB() 
		{
			int numrecs = 0;

			try 
			{
				mSelComm = null;
				
				if ( ! mCoWorkerModel.Mode.Equals(AllFPASSDialogs.CWR_DIALOG_MODE_ARCHIVE))
				{
					mSelComm = mProvider.CreateCommand(SELECT_COWORKER);
				} 
				else 
				{
					mSelComm = mProvider.CreateCommand(SELECT_COWORKER_ARCHIVE);
				}

				
				mProvider.SetParameter(mSelComm, ":CWR_ID", mCoWorkerID );

				// Open data reader to get ExContractor data
				IDataReader mDR = mProvider.GetReader(mSelComm);

				// Loop thru records and create an ArrayList of ExContractor BOs
				while (mDR.Read())
				{

                    mTK = Convert.ToDecimal(mDR["CWR_TK"]);																																																																																																					 
					mPersonalNO		=  mDR["CWR_PERSNO"].ToString();
                    mContractorID = Convert.ToDecimal(mDR["CWR_EXCO_ID"]);
                    mCoordinatorID = Convert.ToDecimal(mDR["CWR_ECOD_ID"]);
					
					// Handle DbNulls, e.g DeptID
                    mOrderNo = FormatReaderStringsForDisplay(mDR["CWR_ORDERNO"]);
                    mDepartmentID = FormatReaderNumsForDisplay(mDR["CWR_DEPT_ID"]);
                    mCraftID = FormatReaderNumsForDisplay(mDR["CWR_CRA_ID"]);
                    mSubcontractorID = FormatReaderNumsForDisplay(mDR["CWR_SUBE_ID"]);                   
                    mAccess = FormatReaderStringsForDisplay(mDR["CWR_ACCESS"]);

                    // ID cards
                    mIDCardNumHitag = FormatReaderNumsForDisplay(mDR["CWR_IDCARDNO"]);
                    mIDCardNumMifare = FormatReaderNumsForDisplay(mDR["CWR_MIFARENO"]);

                    // ID card reader numbers are part of current user's data
                    //------------------------
                    mIDReaderHitag = UserManagementControl.getInstance().IDCardReaderHitag;
                    mIDReaderMifare = UserManagementControl.getInstance().IDCardReaderMifare;

                    // PKI and Windows ID FPASS 5, July 24, 2014
                    mWindowsId          = FormatReaderStringsForDisplay(mDR["CWR_WINDOWS_ID"]);
                    mSmartActNo         = FormatReaderNumsForDisplay(mDR["CWR_SMARTACTNO"]);
                    mIDCardNumMifare    = FormatReaderNumsForDisplay(mDR["CWR_MIFARENO"]);
                    mTelephoneNumber    = FormatReaderStringsForDisplay(mDR["CWR_PHONE"]);

                    mInSmartAct = (mSmartActNo > 0);

                    if (mDR["CWR_PKI"].ToString().Equals(Globals.DB_YES))
                    {
                        mPki = true;
                    }
                    else
                    {
                        mPki = false;
                    }

                    if (mDR["CWR_IMPORTDATE"].Equals(DBNull.Value))
                    {
                        mImportDate = null;
                    }
                    else 
                    {
                        string testText = mDR["CWR_IMPORTDATE"].ToString().Substring(0, 10);
                        mImportDate = Convert.ToDateTime(testText);                 
                    }

                    	
					// Check on / off
					this.mEntryCoodID     = FormatReaderNumsForDisplay( mDR["CWR_ENTRYCOODID"]);
					this.mCheckOffCoodID  = FormatReaderNumsForDisplay( mDR["CWR_CHKOFFCOODID"]);

					if ( mDR["CWR_ENTRYDATECOOD"].Equals(DBNull.Value) )
					{
						mEntryDateString = String.Empty;
					}
					else
					{
						mEntryDateString = mDR["CWR_ENTRYDATECOOD"].ToString().Substring(0,10);
					}
					if ( mDR["CWR_CHKOFFDATECOOD"].Equals(DBNull.Value) )
					{
						mCheckOffDateString = String.Empty;
					}
					else
					{
						mCheckOffDateString = mDR["CWR_CHKOFFDATECOOD"].ToString().Substring(0,10);
					}

					if ( mDR["CWR_OVEREIGHTEEN_YN"].ToString().Equals(Globals.DB_YES) ) 
					{
						mOverEighteen = true;
					} 
					else 
					{
						mOverEighteen = false;
					}
					if ( mDR["CWR_PRECAUTIONMED_YN"].ToString().Equals(Globals.DB_YES) )
					{
						mHasPrecMedical = true;
					}
					else
					{
						mHasPrecMedical = false;
					}
					
					mSurname		= mDR["CWR_SURNAME"].ToString();
					mFirstname		= mDR["CWR_FIRSTNAME"].ToString();
					mDateOfBirth	= Convert.ToDateTime( mDR["CWR_DATEOFBIRTH"]);
					mFirstname		= mDR["CWR_FIRSTNAME"].ToString();
					mPlaceOfBirth	= mDR["CWR_PLACEOFBIRTH"].ToString();
					mDeliveryDate	= Convert.ToDateTime( mDR["CWR_DATECREATED"] );
					mValidFrom		= Convert.ToDateTime(mDR["CWR_VALIDFROM"] );
					mValidUntil		= Convert.ToDateTime(mDR["CWR_VALIDUNTIL"] );					
					mChangeUser		= mDR["CWR_CHANGEUSER"].ToString();
					mTimeStamp		= Convert.ToDateTime(mDR["CWR_TIMESTAMP"]);

					mSupervisorName = mDR["SUPERVISOR"].ToString();
					if ( mSupervisorName.Trim().Equals(",") )
					{
						mSupervisorName = String.Empty;
					}

					mSupervisorTel	= mDR["EXCO_TELEPHONENO"].ToString();
				
					
					if ( mDR["CWR_SECPASS_YN"].ToString().Equals(Globals.DB_YES) ) 
					{
						mSecurityPass = true;
					} 
					else 
					{
						mSecurityPass = false;
					}

					if ( mDR["CWR_ORDERCOMPLET_YN"].ToString().Equals(Globals.DB_YES)) 
					{
						mOrderComplete = true;
					} 
					else 
					{
						mOrderComplete = false;
					}
					mZKSReturnCode = mDR["CWR_RETURNCODE_ZKS"].ToString();

					this.mSupervisorTel  = FormatReaderStringsForDisplay ( mDR["EXCO_TELEPHONENO"]);

					numrecs++;
				}
		
				mDR.Close();

                if (mInSmartAct)
                {
                    // if coworker is in SmartAct then load his photo
                    ReadCoWorkerPhoto();
                }

			}
			catch (System.Data.OracleClient.OracleException oraex)
			{
				mViewCoWorker.Cursor = System.Windows.Forms.Cursors.Default;
				throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(
					MessageSingleton.FATAL_DB_ERROR) + oraex.Message );
			}
			catch ( DbAccessException dba ) 
			{
				mViewCoWorker.Cursor = System.Windows.Forms.Cursors.Default;
				throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(
					MessageSingleton.FATAL_DB_ERROR) + dba.Message );
			}

			// If more than one CWR, throw Fatal (PK ID not right)
			if ( numrecs > 1 ) 
			{
				throw new UIErrorException(MessageSingleton.GetInstance().GetMessage
					(MessageSingleton.FATAL_DB_ERROR));
			}
			// If no records founf for given CWR ID, coworker was deleted by another user
			else if ( numrecs == 0 )
			{
				throw new ActionCancelledException(MessageSingleton.GetInstance().GetMessage
					(MessageSingleton.COWORKER_NOTEXISTS));
			}
		}

		/// <summary>
		/// Fills current coworker with data, so that coworker is ready for user input.
		/// </summary>
		private void InitializeNewCoWorker() 
		{
			mCoWorkerID = mCoWorkerModel.GetNextValFromSeq("SEQ_COWORKER");
			mCoWorkerModel.CoWorkerId = mCoWorkerID;

			mPersonalNO = mCoWorkerModel.GetNextValFromSeq("SEQ_PERSNO").ToString();
			while ( mPersonalNO.Length < 8 ) 
			{
				mPersonalNO = "0" + mPersonalNO;
			}

			this.mZKSReturnCode = Globals.DB_NO;

			mValidUntil = DateTime.Now;
			mCheckOffDateString	= String.Empty;	
			mTimeStamp = DateTime.Now;
			mValidFrom = DateTime.Now;
			mDeliveryDate = DateTime.Now;
			mEntryDateString = mValidFrom.ToString().Substring(0,10);
			mSurname = String.Empty;
			mFirstname = String.Empty;
			mOrderNo = String.Empty;
			mPlaceOfBirth = String.Empty;
			mZKSReturnCode = String.Empty;
			mStatus = String.Empty;
            
            mWindowsId = String.Empty;
            mTelephoneNumber = String.Empty;
            mPki = false;
            mInSmartAct = false;
		}


		/// <summary>
		/// Updates coworker in db.
		/// </summary>
		private void UpdateCoWorker() 
		{			
			IDbCommand mUpdComm = null;
			mUpdComm = mProvider.CreateCommand(UPDATE_COWORKER);

			mUpdComm.Transaction = mCoWorkerModel.CurrentTransaction;
			mUpdComm.Connection = mCoWorkerModel.CurrentTransaction.Connection;
			mProvider.SetParameter( mUpdComm, ":CWR_ID", this.mCoWorkerID );		

			SetCoWorkerParaValues(mUpdComm);
			mUpdComm.ExecuteNonQuery();			
		}

		/// <summary>
		/// Inserts coworker in db.
		/// </summary>
		private void InsertCoWorker() 
		{			
			IDbCommand mInsertComm = null;
			// Create the select command & fill Data Reader 
			mInsertComm = mProvider.CreateCommand(INSERT_COWORKER);

			mInsertComm.Transaction = mCoWorkerModel.CurrentTransaction;
			mInsertComm.Connection = mCoWorkerModel.CurrentTransaction.Connection;

            mProvider.SetParameter(mInsertComm, ":CWR_ID", mCoWorkerID);
            mProvider.SetParameter(mInsertComm, ":CWR_VALIDUNTIL", DateTime.Now);
	
			SetCoWorkerParaValues(mInsertComm);
			mInsertComm.ExecuteNonQuery();
		}


		/// <summary>
		/// Sets values (parameters) in DML statement for coworker
		/// </summary>
		/// <param name="pCommand">Command where paramters have to be set</param>
		private void SetCoWorkerParaValues(IDbCommand pCurrCommand)
		{
			String overeighteen = Globals.DB_NO;
			String secPass = Globals.DB_NO;
			String orderComplete = Globals.DB_NO;
			String hasPreMedical = Globals.DB_NO;

            mProvider.SetParameter(pCurrCommand, ":CWR_TK", Globals.GetInstance().TKNumberRoot); 
			mProvider.SetParameter(pCurrCommand, ":CWR_PERSNO", mPersonalNO.Trim());
			mProvider.SetParameter(pCurrCommand, ":CWR_EXCO_ID", ContractorID);
			mProvider.SetParameter(pCurrCommand, ":CWR_ECOD_ID",  CoordinatorID);

            overeighteen = (mOverEighteen ? Globals.DB_YES : Globals.DB_NO);
            mProvider.SetParameter(pCurrCommand, ":CWR_OVEREIGHTEEN_YN", overeighteen);

			mProvider.SetParameter(pCurrCommand, ":CWR_SURNAME", mSurname.Trim() );
			mProvider.SetParameter(pCurrCommand, ":CWR_FIRSTNAME", mFirstname.Trim() );
			mProvider.SetParameter(pCurrCommand, ":CWR_DATEOFBIRTH", mDateOfBirth );
			mProvider.SetParameter(pCurrCommand, ":CWR_PLACEOFBIRTH", mPlaceOfBirth.Trim() );
			mProvider.SetParameter(pCurrCommand, ":CWR_DATECREATED", mDeliveryDate );
			mProvider.SetParameter(pCurrCommand, ":CWR_VALIDFROM", mValidFrom );
			mProvider.SetParameter(pCurrCommand, ":CWR_ENTRYDATECOOD", mValidFrom);

            secPass = (mSecurityPass ? Globals.DB_YES : Globals.DB_NO);
            mProvider.SetParameter(pCurrCommand, ":CWR_SECPASS_YN", secPass);
            
            orderComplete = (mOrderComplete ? Globals.DB_YES : Globals.DB_NO);
            mProvider.SetParameter(pCurrCommand, ":CWR_ORDERCOMPLET_YN", orderComplete);

            hasPreMedical = (mHasPrecMedical ? Globals.DB_YES : Globals.DB_NO);
            mProvider.SetParameter(pCurrCommand, ":CWR_PRECAUTIONMED_YN", hasPreMedical);

			mProvider.SetParameter(pCurrCommand, ":CWR_CHANGEUSER", UserManagementControl.getInstance().CurrentUserID );
			mProvider.SetParameter(pCurrCommand, ":CWR_TIMESTAMP", DateTime.Now );
			mProvider.SetParameter(pCurrCommand, ":CWR_MND_ID", UserManagementControl.getInstance().CurrentMandatorID );

			// nulls possible
			FormatCopyOutNumsForSave(pCurrCommand, ":CWR_DEPT_ID", mDepartmentID);
			FormatCopyOutNumsForSave(pCurrCommand, ":CWR_CRA_ID", mCraftID);
			FormatCopyOutNumsForSave(pCurrCommand, ":CWR_SUBE_ID", mSubcontractorID);			
			FormatCopyOutNumsForSave(pCurrCommand, ":CWR_ENTRYCOODID", mEntryCoodID);
			FormatCopyOutNumsForSave(pCurrCommand, ":CWR_CHKOFFCOODID", mCheckOffCoodID);
			FormatCopyOutStringsForSave(pCurrCommand, ":CWR_ORDERNO", mOrderNo);
			FormatCopyOutStringsForSave(pCurrCommand, ":CWR_RETURNCODE_ZKS", mZKSReturnCode);
            FormatCopyOutNumsForSave(pCurrCommand, ":CWR_IDCARDNO", mIDCardNumHitag);
            FormatCopyOutNumsForSave(pCurrCommand, ":CWR_MIFARENO", mIDCardNumMifare);

            mProvider.SetParameter(pCurrCommand, ":CWR_PHONE", mTelephoneNumber);
            mProvider.SetParameter(pCurrCommand, ":CWR_WINDOWS_ID", mWindowsId);
            FormatCopyOutNumsForSave(pCurrCommand, ":CWR_SMARTACTNO", mSmartActNo);

            if (mPki == true)
            {
                mProvider.SetParameter(pCurrCommand, ":CWR_PKI", "Y");
            }
            else {
                mProvider.SetParameter(pCurrCommand, ":CWR_PKI", "N");
            }

			
			if (mCheckOffDateString.Length > 0) 
			{
                mProvider.SetParameter(pCurrCommand, ":CWR_CHKOFFDATECOOD", Convert.ToDateTime(mCheckOffDateString));
			} 
			else 
			{
				mProvider.SetParameter(pCurrCommand, ":CWR_CHKOFFDATECOOD", DBNull.Value);
			}
		}

        /// <summary>
		/// Calculates if coworker is over eighteen and sets flag in coworker
		/// </summary>
		private void CalculateOverEighteen() 
		{
			DateTime today = DateTime.Today;
			DateTime birthDayPlusEighteen = this.mDateOfBirth.AddYears(18);
			if ( today.Date.CompareTo(birthDayPlusEighteen.Date) < 0 ) 
			{
				mOverEighteen = false;
			} 
			else 
			{
				mOverEighteen = true;
			}
		}
	
		#endregion // End of Methods
	}
}

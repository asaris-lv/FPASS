using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using de.pta.Component.DbAccess;
using de.pta.Component.DbAccess.Exceptions;
using de.pta.Component.Errorhandling;

using Degussa.FPASS.Util;
using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.Util.Exceptions;
using Degussa.FPASS.Util.UserManagement;
using Degussa.FPASS.Db;

using Degussa.FPASS.Bo.Administration;

using Degussa.FPASS.Gui.Dialog.Pass;
using Degussa.FPASS.FPASSApplication;

namespace Degussa.FPASS.Bo.Pass
{
	/// <summary>
    /// Base class of all respiratory masks in FPASS.
    /// If a mask briefing is granted for a coworker then he is entitled to a respiratory mask.
	/// </summary>
	/// <remarks>
	/// <para><b>History</b></para>
	/// <div class="tablediv">
	/// <table class="dtTABLE" cellspacing="0">
	///		<tr>
	///			<th width="20%">PTA GmbH</th>
	///			<th width="20%">08.05.2017</th>
	///			<th width="60%">Remarks</th>
	///		</tr>
	/// </table>
	/// </div>
	/// </remarks>
	public abstract class AbstractMask : AbstractCoWorkerBO
	{
		#region Members

		/// <summary>
		/// id (pk) of a mask
		/// </summary>
		protected decimal mMaskId;

		/// <summary>
		/// Date when mask was delivered/received to/from coworker
		/// </summary>
		protected DateTime mMaskDate;

		/// <summary>
		/// unique mask number of  lent mask
		/// </summary>
		protected String mMaskNoLent;

        /// <summary>
        /// unique mask number of returned mask
        /// </summary>
        protected String mMaskNoReturned;

		/// <summary>
		/// Type id of mask lent. Each subclass has its unique type id.
		/// </summary>
		protected decimal mMaskTypeLentID;

        /// <summary>
        /// Type id of mask return. Each subclass has its unique type id.
        /// </summary>
        protected decimal mMaskTypeReturnID;
		/// <summary>
		/// id (pk) of the coworker
		/// </summary>
		protected decimal mCoWorkerID;

		/// <summary>
		/// name of the user who delivered/received the mask
		/// </summary>
		protected String mUserName;

		/// <summary>
		/// id of the user who granted the briefing
		/// </summary>
		protected decimal mUserID;

		/// <summary>
		/// last change to a mask
		/// </summary>
		protected DateTime mTimestamp;

		/// <summary>
		/// Indicates whether this mask is in insert or update mode
		/// </summary>
		protected bool mInsert;

		/// <summary>
		/// DB command for querying mask system and doing inserts/updates
		/// </summary>
		protected IDbCommand mSelComm;


        [System.Obsolete("mMaskHasMaintDate is deprecated, please use TecBos instead.")]
        protected bool mMaskHasMaintDate = false;
        
        
        //[System.Obsolete("mNumRecordsReturned is deprecated, please use TecBos instead.")]
        //protected int mNumRecordsReturned = 0;

        // TODO: which of these is still required?
         /// <summary>
        /// Last maintenance date
        /// </summary>
        //protected DateTime mLastMaintDate; 
        
        protected DateTime? mNextMaintDate;
        
        // Date that previous mask was lent or returned
        protected DateTime? mPreviousMaskDate;

        protected string mMaskSystem = string.Empty;
        protected bool mLentActive = false;
        protected bool mReturnActive = false;
        private string SELECT_MASK_ARCHIVE = "SelectMaskArchive";
        private string MASK_CWR_PARAM = ":REMA_CWR_ID";
        private string REMA_RMTY_LENT_ID = ":REMA_RMTY_LENT_ID";
        private string REMA_RMTY_RETURN_ID = ":REMA_RMTY_RETURN_ID";
   
		// constants for SQL commands 
        private string INSERT_MASK = "InsertMask";
		private string UPDATE_MASK = "UpdateMask";
        private string MASK_PK_PARAM = ":REMA_ID";
		private string MASK_USERID_PARAM = ":REMA_USER_ID";
		private string MASK_MASKNO_PARAM = ":REMA_MASKNO";
		private string MASK_TIMESTAMP_PARAM = ":REMA_TIMESTAMP";
        private string MASK_DATE_PARAM = ":REMA_DATE";
        private string INSERT_MASK_ID_PARAM = ":REMA_ID";
        private string INSERT_CWRID_PARAM = ":REMA_CWR_ID";
        private string INSERT_TYPE_PARAM = ":REMA_RMTY_ID";
		
		/// <summary>
		/// SQL for validation: has CWR already got a mask?
		/// </summary>
		private string SELECT_MASK = "SelectMask";
		
		/// <summary>
		/// command used for db updates
		/// </summary>
        private IDbCommand mMaskCommand;


        // from MaskFactory
        // Member variables: attributes
        private ArrayList mMasksList;

        private Dictionary<DateTime, AbstractMask> mMasksLent;
        private Dictionary<DateTime, AbstractMask> mMasksReturned;

        // booleans indicating wether mask is lent or returnd
        protected bool mMaskIsLent; 
        protected bool mMaskIsReturned;

        protected BORespMask mRespMaskBO;


		#endregion 

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public AbstractMask(CoWorkerModel pCoWorkerModel) : base (pCoWorkerModel)
		{
			//initialize();
		}


        /// <summary>
        /// Constructor with pRespMaskBO.
        /// </summary>
        public AbstractMask(CoWorkerModel pCoWorkerModel, BORespMask pRespMaskBO) : base(pCoWorkerModel)
        {
            mRespMaskBO = pRespMaskBO;

        }


        /// <summary>
        /// Constructor with extra arguments, to activate Lent and Return for System
        /// </summary>
        /// <param name="pCoWorkerModel"></param>
        /// <param name="pMaskSystem">FLORIX or TECBOS</param>
        /// <param name="pLentActive">Parameter for Masksystem, which indicates if Lent is active.</param>
        /// <param name="pReturnActive">Parameter for Masksystem, which indicates if Return is active.</param>
        public AbstractMask(CoWorkerModel pCoWorkerModel,string pMaskSystem, bool pLentActive, bool pReturnActive)
            : base(pCoWorkerModel)
        {
            //initialize();
            mMaskSystem = pMaskSystem;
            // ToDo: hier mLentActive, mReturnActive aus der Config lesen, dann werden die Parameter nicht benötigt
            mLentActive = pLentActive;
            mReturnActive = pReturnActive;
        }

        /// <summary>
        /// Constructor with extra argument previous date a mask was lent or returned.
        /// </summary>
        //public AbstractMask(CoWorkerModel pCoWorkerModel, DateTime pPreviousDate) : base(pCoWorkerModel)
        //{
        //    initialize();
        //    mPreviousMaskDate = pPreviousDate;
        //}

		#endregion //End of Constructors

		#region Initialization

		/// <summary>
		/// Initializes the members.
		/// </summary>
		protected void initialize()
		{
			// Get DataProvider from DbAccess component
			mProvider = DBSingleton.GetInstance().DataProvider;
			mTimestamp = DateTime.Now;
			mUserID = UserManagementControl.getInstance().CurrentUserID;
			mUserName = String.Empty;
			mMaskDate = DateTime.Now;
			mCoWorkerID = mCoWorkerModel.CoWorkerId;
			mMaskNoLent = String.Empty;
            mMaskNoReturned = String.Empty;
			mInsert = true;			
		}

        /// <summary>
        /// Initialize with values from RespMaskBO
        /// </summary>
        protected void initializeFromRespMaskBO()
        {
            //initialize from mRespMaskBO  
            mMaskDate = (null != mRespMaskBO.MaskDate) ? Convert.ToDateTime(mRespMaskBO.MaskDate) : new DateTime(1900, 1, 1);
            mMaskId = mRespMaskBO.MaskId;
            mMaskNoLent = mRespMaskBO.MaskNo;
            mUserName = mRespMaskBO.UserName;
            mNextMaintDate = mRespMaskBO.NextMaintDate;
        
        }

		#endregion 

		#region Accessors 

		/// <summary>
		/// simple accessor
		/// </summary>
		public decimal MaskID 
		{
			get 
			{
				return mMaskId;
			}
			set 
			{
				mMaskId = value;
			}
		}

		/// <summary>
		/// Type of mask being lent
		/// </summary>
		public decimal MaskTypeLentID 
		{
			get 
			{
				return mMaskTypeLentID;
			}
			set 
			{
				mMaskTypeLentID = value;
			}
		}

		/// <summary>
        /// PK of CoWorker who has mask
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
		/// PK of user who granted the mask
		/// </summary>
		public decimal UserID 
		{
			get 
			{
				return mUserID;
			}
			set 
			{
				mUserID = value;
			}
		}

		/// <summary>
		/// Gets or sets date the mask was assigned to the coworker
		/// </summary>
		public DateTime MaskDate
		{
			get { return mMaskDate; }
            set { mMaskDate = value; }
		}

        ///// <summary>
        ///// Last maintenance date
        ///// </summary>
        //public DateTime LastMaintDate
        //{
        //    get { return mLastMaintDate; }
        //    set { mLastMaintDate = value; }
        //}

        /// <summary>
        /// Mask's next maintenance date
        /// </summary>
        public DateTime? NextMaintDate
        {
            get { return mNextMaintDate; }
            set { mNextMaintDate = value; }
        }

        /// <summary>
        /// Latest date that any previous mask was lent or returned
        /// </summary>
        public DateTime? PreviousMaskDate
        {
            get { return mPreviousMaskDate; }
            set { mPreviousMaskDate = value; }
        } 

		/// <summary>
		/// Mask number lent (is actually the barcode)
		/// </summary>
		public String MaskNoLent 
		{
            get { return mMaskNoLent; }
            set { mMaskNoLent = value; }
		}

        /// <summary>
        /// Mask number returned (is actually the barcode)
        /// </summary>
        public String MaskNoReturned
        {
            get { return mMaskNoReturned; }
            set { mMaskNoReturned = value; }
        }

		/// <summary>
		/// Name of FPASS user who assigned mask
		/// </summary>
		public string UserName 
		{
			get 
			{
				return mUserName;
			}
			set 
			{
				mUserName = value;
			}
		}

        /// <summary>
        /// boolean indicating wether mask is lent
        /// </summary>
        public bool MaskIsLent
        { 
            get 
            {
                if (mMaskNoLent.Length > 0 && mMaskNoReturned.Length == 0)
                {
                    mMaskIsLent = true;
                }
                else
                {
                    mMaskIsLent = false;
                }
                return mMaskIsLent; 
            }
            set { mMaskIsLent = value; }
        }


        /// <summary>
        /// boolean indicating wether mask is returned
        /// </summary>
        public bool MaskIsReturned
        {
            get 
            {
                if (mMaskNoReturned.Length > 0 && mMaskNoLent.Length > 0)
                {
                    mMaskIsReturned = true;
                }
                else
                {
                    mMaskIsReturned = false;
                }
                return mMaskIsReturned; 
            }
            set { mMaskIsReturned = value; }
        }



		#endregion 

		#region Methods 


		/// <summary>
		/// When BOs are constructed the coworker ID is not known
		/// "InitializeData" in CoworkerModel calls InitializeBO in all BOs to get current coworker ID
		/// </summary>
		internal override void InitializeBO()
		{		
			mCoWorkerID = mCoWorkerModel.CoWorkerId;		
		}

		/// <summary>
		/// Validate respmask info entered in GUI
		/// If this is a new assignment of mask to coworker (INSERT)
		/// get next PK id before BO is saved to DB
		/// For the respmasks extra checks are necessary: method CheckMaskAvailable.
		/// </summary>
		internal override void Validate() 
		{
			if ( mChanged )
			{
				//this.mMaskeDirected = true;
				this.CheckMaskAvailable();

                // Error if MaskNo in  LentFlo and LenTec filled 
                if (mViewCoWorker.TxtSiFiMaskNrLentTec.Text.Length > 0 && mViewCoWorker.TxtSiFiMaskNrLentFlo.Text.Length > 0)
                {
                    mCoWorkerModel.ErrorMessages.Append(MessageSingleton.GetInstance().GetMessage(MessageSingleton.MASK_ONLY_ONE_MASKSYSTEM_ALLOWED));
                }


				if (mInsert) 
				{
					InitializeNewBO();
				}				
			}
		}

		/// <summary>
		/// Assignment is only valid if checkbox in GUI ticked, else mask does not count as lent out
		/// </summary>
		/// <returns></returns>
		internal bool IsValid() 
		{
            //if ( mMaskeDirected && mMaskExecuted ) 
            //{
            //    return true;
            //} 
            //else 
            //{
            //    return false;
            //}
            return true;
		}

		/// <summary>
		/// Save info in BO to database: for a new mask assignment INSERT a record
		/// Changes to an exisiting record is UPDATE
		/// Get the name of the application user who made the changes
		/// <exception cref="de.pta.Component.Errorhandling.UIFatalException"> thrown if dml statement failed.
		/// If delivery date of mask was set to current date or earlier, must re-enable GUI fields for mask received
		/// as when these masks are reloaded after save they are no longer shown, no way of re-enabling fields
		/// </exception>
		/// </summary>
		internal override void Save() 
		{
			if (mChanged && mMaskNoLent.Length > 0) 
			{
				mCoWorkerModel.ZKSChanged = true;
				try
				{
					if (mInsert) 
					{
                        if (MaskIsLent) { InsertMaskLent(); }
                        if (MaskIsReturned) { InsertMaskReturn(); }
                        //InsertMask();
					} 
					else  
					{
						UpdateMask();
					} 
					mUserName = UserManagementControl.getInstance().CurrentOSUserName;
					mInsert = false;
				}
				catch ( DbAccessException dba ) 
				{
					throw new UIFatalException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR) + dba.Message );
				}

                // Hide GUI-Fields for Reload
                this.HideMaskGuiFields();



			}
			mChanged = false;
		}


        /// <summary>
        /// Hide all Mask GUI-Fields for Reload
        /// </summary>
        protected void HideMaskGuiFields()
        {
            // Hide GUI-Fields for Reload

            // TecBos Mask Lent
            mViewCoWorker.DatSiFiMaskLentOnTec.Visible = false;
            mViewCoWorker.TxtSiFiMaskLentByTec.Visible = false;
            mViewCoWorker.TxtSiFiMaskNrLentTec.Visible = false;
            mViewCoWorker.LblSiFiMaskLentTec.Visible = false;
            mViewCoWorker.LblSiFiMaskNrLentTec.Visible = false;
            mViewCoWorker.LblSiFiMaskMaintDateTec.Visible = false;
            mViewCoWorker.LblSiFiMaskLentOnTec.Visible = false;
            mViewCoWorker.LblSiFiMaskLentByTec.Visible = false;
            mViewCoWorker.TxtSiFiMaskMaintDateTec.Visible = false;

            // TecBos Mask Return
            mViewCoWorker.DatSiFiMaskBackOnTec.Visible = false;
            mViewCoWorker.TxtSiFiMaskBackByTec.Visible = false;
            mViewCoWorker.TxtSiFiMaskNrBackTec.Visible = false;
            mViewCoWorker.LblSiFiMaskBackTec.Visible = false;
            mViewCoWorker.LblSiFiMaskNrBackTec.Visible = false;
            mViewCoWorker.LblSiFiMaskBackOnTec.Visible = false;
            mViewCoWorker.LblSiFiMaskBackByTec.Visible = false;

            // Florix Mask Lent
            mViewCoWorker.DatSiFiMaskLentOnFlo.Visible = false;
            mViewCoWorker.TxtSiFiMaskLentByFlo.Visible = false;
            mViewCoWorker.TxtSiFiMaskNrLentFlo.Visible = false;
            mViewCoWorker.LblSiFiMaskLentFlo.Visible = false;
            mViewCoWorker.LblSiFiMaskNrLentFlo.Visible = false;
            mViewCoWorker.LblSiFiMaskLentOnFlo.Visible = false;
            mViewCoWorker.LblSiFiMaskLentByFlo.Visible = false;

            // Florix Mask Return
            mViewCoWorker.DatSiFiMaskBackOnFlo.Visible = false;
            mViewCoWorker.TxtSiFiMaskBackByFlo.Visible = false;
            mViewCoWorker.TxtSiFiMaskNrBackFlo.Visible = false;
            mViewCoWorker.LblSiFiMaskBackFlo.Visible = false;
            mViewCoWorker.LblSiFiMaskNrBackFlo.Visible = false;
            mViewCoWorker.LblSiFiMaskBackOnFlo.Visible = false;
            mViewCoWorker.LblSiFiMaskBackByFlo.Visible = false;

             
            bool isAllowed =
                mViewCoWorker.CbxSiFiRespMaskBriefRec.Checked
                && (mViewCoWorker.mSiteFireAuthorization
                    || mViewCoWorker.mEdvAdminAuthorization
                    || mViewCoWorker.mSystemAdminAuthorization);

            bool maskIsLent = mCoWorkerModel.HasMaskTecBos || mCoWorkerModel.HasMaskFlorix;          

            // Button should always be enabled
            mViewCoWorker.BtnSiFiMaskTicket.Enabled = isAllowed;
            mViewCoWorker.TxtSiFiMaskMaintDateTec.Text = mNextMaintDate.ToString();
        }


		/// <summary>
		/// As with every BO, get PK value from sequence before new BO is saved to DB
		/// </summary>
		protected void InitializeNewBO() 
		{
			mMaskId = mCoWorkerModel.GetNextValFromSeq("SEQ_RESPMASK");
		}

		
		/// <summary>
		/// Interface with the resp mask system: check if the given mask exists 
		/// This method is extended in the subclasses (MaskReceived & MaskReturned)
		/// </summary>
		protected virtual bool CheckMaskAvailable() 
		{
            return true;
		}

	

		/// <summary>
		/// Build & execute UPDATE command to save changes to BO info using transaction opened in CoWorkerModel
		/// Table fpass_respmask holds assignments respmask to coworker 
		/// Date is left empty if checkbox (= "valid") in GUI is not ticked, column INACTIVE is set to Y
		/// </summary>
        private void UpdateMask()
        {
            // check if mask is lent or returned
            string maskNo = string.Empty;
            if (MaskIsLent) { maskNo = mMaskNoLent; }
            if (MaskIsReturned) { maskNo = mMaskNoReturned; }
            
            mMaskCommand = mProvider.CreateCommand(UPDATE_MASK);

            mMaskCommand.Transaction = mCoWorkerModel.CurrentTransaction;
            mMaskCommand.Connection = mCoWorkerModel.CurrentTransaction.Connection;

            mProvider.SetParameter(mMaskCommand, MASK_USERID_PARAM, mUserID);
            mProvider.SetParameter(mMaskCommand, MASK_TIMESTAMP_PARAM, mTimestamp);
            mProvider.SetParameter(mMaskCommand, MASK_PK_PARAM, mMaskId);
            mProvider.SetParameter(mMaskCommand, MASK_MASKNO_PARAM, maskNo);
            mProvider.SetParameter(mMaskCommand, MASK_DATE_PARAM, mMaskDate);

            mMaskCommand.ExecuteNonQuery();        
        }


        /// <summary>
        /// Build & execute INSERT command to save new BO to DB using transaction opened in CoWorkerModel
        /// Table fpass_respmask holds assignments respmask to coworker.
        /// </summary>
        private void InsertMaskLentInsertSQL()
        {
            mMaskCommand = mProvider.CreateCommand(INSERT_MASK);

            mMaskCommand.Transaction = mCoWorkerModel.CurrentTransaction;
            mMaskCommand.Connection = mCoWorkerModel.CurrentTransaction.Connection;

            mProvider.SetParameter(mMaskCommand, INSERT_MASK_ID_PARAM, mMaskId);
            mProvider.SetParameter(mMaskCommand, INSERT_CWRID_PARAM, mCoWorkerID);
            mProvider.SetParameter(mMaskCommand, INSERT_TYPE_PARAM, mMaskTypeLentID);
            mProvider.SetParameter(mMaskCommand, MASK_MASKNO_PARAM, mMaskNoLent);
            mProvider.SetParameter(mMaskCommand, MASK_DATE_PARAM, mMaskDate);
            mProvider.SetParameter(mMaskCommand, MASK_USERID_PARAM, mUserID);
            mProvider.SetParameter(mMaskCommand, MASK_TIMESTAMP_PARAM, mTimestamp);

            mMaskCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// Build & execute INSERT command to save new BO to DB using transaction opened in CoWorkerModel
        /// Table fpass_respmask holds assignments respmask to coworker.
        /// </summary>
        private void InsertMaskReturnInsertSQL()
        {
            mMaskCommand = mProvider.CreateCommand(INSERT_MASK);

            mMaskCommand.Transaction = mCoWorkerModel.CurrentTransaction;
            mMaskCommand.Connection = mCoWorkerModel.CurrentTransaction.Connection;

            mProvider.SetParameter(mMaskCommand, INSERT_MASK_ID_PARAM, mMaskId);
            mProvider.SetParameter(mMaskCommand, INSERT_CWRID_PARAM, mCoWorkerID);
            mProvider.SetParameter(mMaskCommand, INSERT_TYPE_PARAM, mMaskTypeReturnID);
            mProvider.SetParameter(mMaskCommand, MASK_MASKNO_PARAM, mMaskNoReturned);
            mProvider.SetParameter(mMaskCommand, MASK_DATE_PARAM, mMaskDate);
            mProvider.SetParameter(mMaskCommand, MASK_USERID_PARAM, mUserID);
            mProvider.SetParameter(mMaskCommand, MASK_TIMESTAMP_PARAM, mTimestamp);

            mMaskCommand.ExecuteNonQuery();
        }


        /// <summary>
        /// Creates the CWR-mask assignment in fpass_respmask and updates TEcBos.
        /// Uses stored procedure in Oracle to do this.
        /// </summary>
        private void InsertMaskLent()
        {
            mMaskCommand = mProvider.CreateCommand("SequenceDummy");

            mMaskCommand.Transaction = mCoWorkerModel.CurrentTransaction;
            mMaskCommand.Connection = mCoWorkerModel.CurrentTransaction.Connection;

            mMaskCommand.CommandType = CommandType.StoredProcedure;

            mMaskCommand.CommandText = "SP_INSERT_FPASS_RESPMASK(P_REMA_CWR_ID => " + mCoWorkerID
                            + ", P_REMA_RMTY_ID => " + mMaskTypeLentID
                            + ", P_REMA_MASKNO => " + "'" + mMaskNoLent + "'"
                            + ", P_REMA_DATE => " + "to_date('" + mMaskDate + "', 'DD.MM.YYYY HH24:MI:SS')"
                            + ", P_REMA_USER_ID => " + mUserID
                            + ")";

            mMaskCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// Creates the CWR-mask assignment in fpass_respmask and updates TEcBos.
        /// Uses stored procedure in Oracle to do this.
        /// </summary>
        private void InsertMaskReturn()
        {
            mMaskCommand = mProvider.CreateCommand("SequenceDummy");

            mMaskCommand.Transaction = mCoWorkerModel.CurrentTransaction;
            mMaskCommand.Connection = mCoWorkerModel.CurrentTransaction.Connection;

            mMaskCommand.CommandType = CommandType.StoredProcedure;

            mMaskCommand.CommandText = "SP_INSERT_FPASS_RESPMASK(P_REMA_CWR_ID => " + mCoWorkerID
                            + ", P_REMA_RMTY_ID => " + mMaskTypeReturnID
                            + ", P_REMA_MASKNO => " + "'" + mMaskNoReturned + "'"
                            + ", P_REMA_DATE => " + "to_date('" + mMaskDate + "', 'DD.MM.YYYY HH24:MI:SS')"
                            + ", P_REMA_USER_ID => " + mUserID
                            + ")";

            mMaskCommand.ExecuteNonQuery();
        }


		#endregion 

        #region MethodsFromMaskFactory


        


//        /// <summary>
//        /// Return arraylist containing masks assigned to the given coworker.
//        /// Any masks that were lent out and returned in the past are filtered out.
//        /// </summary>
//        /// <returns>ArrayList of masks, these are assigned to Coworker model</returns>
//        internal ArrayList GetMasks()
//        {
//            mCoWorkerID = mCoWorkerModel.CurrentFFMAID;

//            // Get masks from DB
//            mMasksList = new ArrayList();
//            SelectMasks();

//            // Find out which masks CWR has.
//            int nrMasksLent = mMasksLent.Count();
//            int nrMasksReturned = mMasksReturned.Count();


//            if (nrMasksLent > nrMasksReturned)
//            {
//                // Assumption is CWR still has a resp mask, so there is one more mask lent out than returned         
//                // Get highest date of last mask lent out     
//                // and remember previous date in currently assigned mask
//                var lastLentDate = mMasksLent.Keys.Max();
//                AbstractMask lastLentMask = (AbstractMask)mMasksLent[lastLentDate];
//                lastLentMask.PreviousMaskDate = lastLentDate;
//                mMasksList.Add(lastLentMask);

//                // Is this mask from Florix or TecBos? Check TypeId
//                decimal maskTypeId = lastLentMask.MaskTypeLentID;

//                // Empty BO for Returned mask: if Florix mask was lent then use Florix BO,
//                // otherwise it's TecBos.
//                if (maskTypeId == Globals.RespMaskIdLentFlo)
//                {
//                    mCoWorkerModel.HasMaskFlorix = true;
//                    mMasksList.Add(new MaskFlorixReturned(mCoWorkerModel));
//                    // add empty masks
//                    //AddTecBosMasks(DateTime.MinValue);
//                }
//                else
//                {
//                    mCoWorkerModel.HasMaskTecBos = true;
//                    mMasksList.Add(new MaskTecBosReturned(mCoWorkerModel));
//                    // add empty masks
//                    //AddFlorixMasks(DateTime.MinValue);
//                }
//            }
//            else if (nrMasksLent == 0 && nrMasksReturned == 0)
//            {
//                // If no masks to be shown for current coworker, create empty BOs 
//                //AddTecBosMasks(DateTime.MinValue);
//                //AddFlorixMasks(DateTime.MinValue);
//            }
//            else
//            {
//                // If same number of masks were lent out as have been returned
//                // that means CWR has no resp masks outstanding. 
//                // Get latest date from returned masks
//                var lastReturnDate = mMasksReturned.Keys.Max();

//                if (lastReturnDate < DateTime.Now)
//                {
//                    // Mask was given back some time in the past, means none still lent out.
//                    // Re-initialize BOs and show empty fields.
//                    // but remember previous lend date.
//                    //AddTecBosMasks(lastReturnDate);
//                    //AddFlorixMasks(lastReturnDate);
//                }
//                else
//                {
//                    // If mask's return date is in the future,
//                    var lastLent = mMasksLent.Keys.Max();

//                    // Currently lent mask and  Returned mask that goes with it
//                    var lastLentMask = (AbstractMask)mMasksLent[lastLent];
//                    mMasksList.Add(lastLentMask);
//                    mMasksList.Add((AbstractMask)mMasksReturned[lastReturnDate]);

//                    // Which mask type is currently lent out?
//                    if (lastLentMask.MaskTypeLentID == Globals.RespMaskIdLentFlo)
//                    {
//                        mCoWorkerModel.HasMaskFlorix = true;
////                        AddTecBosMasks(DateTime.MinValue);  // add empty masks
//                    }
//                    else
//                    {
//                        mCoWorkerModel.HasMaskTecBos = true;
////                        AddFlorixMasks(DateTime.MinValue);  // add empty masks
//                    }
//                }
//            }

//            return mMasksList;
//        }

        /// <summary>
        /// Gets the masks assigned to the given coworker from the database.
        /// </summary>
        private void SelectMasks()
        {
            try
            {
                AbstractMask mask;

                mMasksLent = new Dictionary<DateTime, AbstractMask>();
                mMasksReturned = new Dictionary<DateTime, AbstractMask>();

                // Get right SQL command and set CWR id
                string comdName = mCoWorkerModel.Mode.Equals(AllFPASSDialogs.CWR_DIALOG_MODE_ARCHIVE) ? SELECT_MASK_ARCHIVE : SELECT_MASK;
                mSelComm = mProvider.CreateCommand(comdName);
                mProvider.SetParameter(mSelComm, MASK_CWR_PARAM, mCoWorkerID);
                mProvider.SetParameter(mSelComm, REMA_RMTY_LENT_ID, mMaskTypeLentID);
                mProvider.SetParameter(mSelComm, REMA_RMTY_RETURN_ID, mMaskTypeReturnID);

                // Open data reader to get assignments coworker - mask, create an arraylist of masks
                IDataReader mDR = mProvider.GetReader(mSelComm);
                while (mDR.Read())
                {
                    int maskTypeID = Convert.ToInt32(mDR["REMA_RMTY_ID"]);

                    switch (maskTypeID)
                    {
                        case Globals.RespMaskIdLentFlo:
                            // Coworker has been lent at least 1 mask 
                            mask = new MaskFlorixLent(mCoWorkerModel);
                            break;
                        case Globals.RespMaskIdLentTec:
                            // Coworker has been lent at least 1 mask 
                            mask = new MaskTecBosLent(mCoWorkerModel);
                            break;
                        case Globals.RespMaskIdReturnFlo:
                            mask = new MaskFlorixReturned(mCoWorkerModel);
                            break;
                        case Globals.RespMaskIdReturnTec:
                            mask = new MaskTecBosReturned(mCoWorkerModel);
                            break;
                        default:
                            throw new UIFatalException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR) + "Kein korrekter Atemschutzmaskentyp gefunden. (RespMaskType)");
                    }

                    // Get mask number, PK of user for DB and user nice name for display
                    mask.MaskID = Convert.ToDecimal(mDR["REMA_ID"]);
                    mask.MaskNoLent = mDR["REMA_MASKNO"].ToString();
                    mask.MaskDate = mDR["REMA_DATE"].Equals(DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(mDR["REMA_DATE"]);
                    // ToDo: NextMaintDate hier hinzulesen, da nicht mehr in view (nur für TecBos gefüllt)
                    //mask.NextMaintDate = mDR["REMA_NEXT_MAINTAIN"].Equals(DBNull.Value) ? null : new Nullable<DateTime>(Convert.ToDateTime(mDR["REMA_NEXT_MAINTAIN"]));
                    mask.NextMaintDate = null;
                    mask.UserID = Convert.ToDecimal(mDR["REMA_USER_ID"]);
                    mask.UserName = mDR["USERNICENAME"].ToString();

                    if (maskTypeID == Globals.RespMaskIdLentFlo || maskTypeID == Globals.RespMaskIdLentTec)
                        mMasksLent.Add(mask.MaskDate, mask);
                    else
                        mMasksReturned.Add(mask.MaskDate, mask);
                }
                mDR.Close();
            }
            catch (DbAccessException dba)
            {
                throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR) + dba.Message + " Objekt: " + this.ToString());
            }
            catch (System.Data.OracleClient.OracleException oraex)
            {
                throw new UIFatalException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR) + oraex.Message + " Objekt: " + this.ToString());
            }
        }


        #endregion
    }
}

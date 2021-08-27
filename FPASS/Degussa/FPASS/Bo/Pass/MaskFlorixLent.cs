using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

using de.pta.Component.DbAccess;
using de.pta.Component.Errorhandling;

using Degussa.FPASS.Util;
using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.Util.Exceptions;
using Degussa.FPASS.Util.UserManagement;
using Degussa.FPASS.Db;
using Degussa.FPASS.FPASSApplication;
using Degussa.FPASS.Bo.Administration;

using Degussa.FPASS.Gui.Dialog.Pass;

namespace Degussa.FPASS.Bo.Pass
{
	/// <summary>
    /// Business object for respiratory mask being lent to coworker, mask comes from Florix (older system)
	/// </summary>
	/// <remarks>
	/// <para><b>History</b></para>
	/// <div class="tablediv">
	/// <table class="dtTABLE" cellspacing="0">
	///		<tr>
	///			<th width="20%">PTA GmbH</th>
	///			<th width="20%">03.05.2017</th>
	///			<th width="60%">Remarks</th>
	///		</tr>
	/// </table>
	/// </div>
	/// </remarks>
	public class MaskFlorixLent : AbstractMask
	{
		#region Members
		
		// Used to get the latest date a mask was given back (05.05.2004)
		private  DateTime  mLastMaskDate;
        private string FINDLATESTMASKDATE = "SelectLatestMaskDate";
        private string MASK_CWRID_PARAM = ":REMA_CWR_ID";
        private string FIND_MASK_LENT_FPASS = "SelectMaskLent";
        private string MASK_NUMBER_PARA = ":REMA_MASKNO";

        // Validations for mask (florix & fpass)
        private string FIND_MASK_FLO = "SelectFlorixMask";
        private string PARA_MASK_BARCODE_FLO = ":FLO_BARCODE";

        // Dummy: shows who mask is lent to in error message
        private string mCoworkerName = "";

		#endregion 

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
        public MaskFlorixLent(CoWorkerModel pCoWorkerModel) : base(pCoWorkerModel)
		{
			initialize();
		}


        /// <summary>
        /// Constructor with BORespMask.
        /// </summary>
        public MaskFlorixLent(CoWorkerModel pCoWorkerModel, BORespMask pRespMaskBO)
            : base(pCoWorkerModel, pRespMaskBO)
        {
            initialize();

            //mMaskDate = (null != pRespMaskBO.MaskDate) ? Convert.ToDateTime(pRespMaskBO.MaskDate) : new DateTime(1900, 1, 1);
            //mMaskId = pRespMaskBO.MaskId;
            //mMaskNoLent = pRespMaskBO.MaskNo;
            //mNextMaintDate = pRespMaskBO.NextMaintDate;

        }


        /// <summary>
        /// Constructor with extra argument previous date a mask was lent 
        /// </summary>
        //public MaskFlorixLent(CoWorkerModel pCoWorkerModel, DateTime pPreviousDate) : base(pCoWorkerModel, pPreviousDate)
        //{
        //    initialize();
        //}

		#endregion 

		#region Initialization

		/// <summary>
		/// Initializes the members.
		/// </summary>
		private void initialize()
		{
            base.initialize();
            base.initializeFromRespMaskBO();
            mMaskTypeLentID = Globals.RespMaskIdLentFlo;
		}	

		#endregion 

		#region Methods 

		/// <summary>
		/// Fill GUI fields (tab "Werkfeuerwehr") for mask lent out (received)
		/// </summary>
		internal override void CopyIn() 
		{
            //if (!Globals.GetInstance().FlorixLentActive)
            //{
            //    mViewCoWorker.DatSiFiMaskLentOnFlo.Visible = false;
            //    mViewCoWorker.TxtSiFiMaskLentByFlo.Visible = false; 
            //    mViewCoWorker.TxtSiFiMaskNrLentFlo.Visible = false;
            //}
            //else
            //{
                // Florix Mask Lent
                mViewCoWorker.DatSiFiMaskLentOnFlo.Visible = true;
                mViewCoWorker.TxtSiFiMaskLentByFlo.Visible = true;
                mViewCoWorker.TxtSiFiMaskNrLentFlo.Visible = true;
                mViewCoWorker.LblSiFiMaskLentFlo.Visible = true;
                mViewCoWorker.LblSiFiMaskNrLentFlo.Visible = true;
                mViewCoWorker.LblSiFiMaskLentOnFlo.Visible = true;
                mViewCoWorker.LblSiFiMaskLentByFlo.Visible = true;
                
                mViewCoWorker.DatSiFiMaskLentOnFlo.Value = mMaskDate;
                mViewCoWorker.TxtSiFiMaskLentByFlo.Text = mUserName;
                mViewCoWorker.TxtSiFiMaskNrLentFlo.Text = mMaskNoLent;

                SetAuthorization();
            //}
        }

		/// <summary>
		/// Enables fields for lending resp mask.
		/// </summary>
		private void SetAuthorization() 
		{ 
            // Enable fields only if these conditions met
//            bool isLentEnabled = mViewCoWorker.CbxSiFiRespMaskBriefRec.Checked && !mCoWorkerModel.HasMaskTecBos && !mCoWorkerModel.HasMaskFlorix;
            bool isLentEnabled = mViewCoWorker.CbxSiFiRespMaskBriefRec.Checked &&  mRespMaskBO.IsReadOnly == false;
            // Reset read-only
            mViewCoWorker.TxtSiFiMaskNrLentFlo.ReadOnly = false;

            mViewCoWorker.TxtSiFiMaskNrLentFlo.Enabled = isLentEnabled;
            mViewCoWorker.DatSiFiMaskLentOnFlo.Enabled = isLentEnabled;

            // For special case that mask is lent out: make field read-only
//            if (!isLentEnabled && mViewCoWorker.CbxSiFiRespMaskBriefRec.Checked && mCoWorkerModel.HasMaskFlorix)
            if (!isLentEnabled && mViewCoWorker.CbxSiFiRespMaskBriefRec.Checked && mRespMaskBO.IsReadOnly)
            {
                mViewCoWorker.TxtSiFiMaskNrLentFlo.Enabled = true;
                mViewCoWorker.TxtSiFiMaskNrLentFlo.ReadOnly = true;
            }
        }

		/// <summary>
		/// Copies out fields concerning resp mask lent to CWR
		/// </summary>
		internal override void CopyOut() 
		{
			int dateCompared = mCoWorkerModel.CompareDates(mMaskDate, mViewCoWorker.DatSiFiMaskLentOnFlo.Value);

			if (dateCompared != 0) 
			{
				mMaskDate = mViewCoWorker.DatSiFiMaskLentOnFlo.Value;
				mChanged = true;
			} 

			if (!mViewCoWorker.TxtSiFiMaskLentByFlo.Text.Equals(mUserName)) 
			{
				mUserName = mViewCoWorker.TxtSiFiMaskLentByFlo.Text;
				mChanged = true;
			} 

			if (!mViewCoWorker.TxtSiFiMaskNrLentFlo.Text.Equals(mMaskNoLent)) 
			{
				mChanged = true;
				mMaskNoLent = mViewCoWorker.TxtSiFiMaskNrLentFlo.Text.Trim();
			}

			if (mChanged)
			{
				mUserID = UserManagementControl.getInstance().CurrentUserID;
				mUserName = UserManagementControl.getInstance().CurrentUserNiceName;
			}
		}

		/// <summary>
		/// Overrides method in AbstractMask
		/// Only validate in florix if a mask number entered in textbox
		/// If if tickbox checked and no mask number given, give warning.
		/// 04.03.2004: masks always has status received
		/// 05.05.04: Make sure coworker cannot lend out a mask before any entries in the past:
		/// new masks always chronological
		/// A mask can only be lent out if the mask briefing was received
		/// </summary>
		internal override void Validate()
		{			
			if (mMaskNoLent.Length > 0 && mChanged)
			{
				if ( !mViewCoWorker.CbxSiFiRespMaskBriefRec.Checked )
				{
					mViewCoWorker.TxtSiFiMaskNrLentFlo.Text = String.Empty;
					mCoWorkerModel.ErrorMessages.Append( MessageSingleton.GetInstance().GetMessage(MessageSingleton.MASK_NO_BRIEFING) );
				}
				
				// Get latest date for mask and check GUI date is after this
				this.GetLastMaskActionDate();
				if (mMaskDate < mLastMaskDate)
				{
					mCoWorkerModel.ErrorMessages.Append(MessageSingleton.GetInstance().GetMessage(MessageSingleton.MASK_IN_PAST));
				}
				else
				{
					base.Validate();

                    // If all is ok then set flag.
                    if (mCoWorkerModel.ErrorMessages.Length == 0) mCoWorkerModel.HasMaskFlorix = true;
				}
			}
		}

		/// <summary>
		/// First thing: if CWR already has 1 mask out, can't lend another one
		/// Interface with the FLORIX system: check if the given mask exists 
		/// query FLORIX with the given Masknumber (= barcode) and get the date of next maintainance
		/// If maintainance overdue or mask already out then it cannot be lent
        /// TODO: Florix is deprecated
		/// </summary>
		protected override bool CheckMaskAvailable() 
		{
            if (mCoWorkerModel.HasMaskFlorix)
			{
                // First thing: if CWR already has 1 mask out, can't lend another one
				mCoWorkerModel.ErrorMessages.Append( MessageSingleton.GetInstance().GetMessage(MessageSingleton.COWORKER_ALREADY_MASK));
			}
			else
			{
               // bool hasMaintDate = false;
                bool isAlreadyLent = true;
                int numRecs = 0;
   
                // Florix: make sure mask exists
                mSelComm = mProvider.CreateCommand(FIND_MASK_FLO);
                mProvider.SetParameter(mSelComm, PARA_MASK_BARCODE_FLO, this.mMaskNoLent);

                // Open data reader: gives 1 record as Max() selected
                IDataReader mDR = mProvider.GetReader(mSelComm);
                while (mDR.Read())
                {
                    if (mDR["FLO_MAINTAIN_DUE"].Equals(DBNull.Value))
                    {
                        // No date for maintainance, assume mask cannot be lent
                        mMaskHasMaintDate = false;
                    }
                    else
                    {
                        mNextMaintDate = Convert.ToDateTime(mDR["FLO_MAINTAIN_DUE"]);
                        mMaskHasMaintDate = true;
                    }
                    numRecs++;
                }
                mDR.Close();

                // If no barcode found then mask does not exist
                if (numRecs == 0)
                {
                    mCoWorkerModel.ErrorMessages.Append(string.Format(MessageSingleton.GetInstance().GetMessage(MessageSingleton.MASKNR_NOT_AVAIL), mMaskNoLent, "Florix"));
                }


				// If a mask was found: if its maintainance date overdue or missing then it cannot be lent
                if (numRecs > 0) 
				{				
					int ret = mNextMaintDate.Value.CompareTo(DateTime.Now);
					if ( 0 > ret || !this.mMaskHasMaintDate )
					{
						mCoWorkerModel.ErrorMessages.Append( MessageSingleton.GetInstance().GetMessage(MessageSingleton.MASK_MAINT_OVERDUE) );
					}
					else
					{
						// Check mask is not alreay lent
						if (MaskIsAlreadyLent())
						{
                            mCoWorkerModel.ErrorMessages.Append(string.Format(MessageSingleton.GetInstance().GetMessage(MessageSingleton.MASK_ALREADY_LENT), mMaskNoLent, mCoworkerName));
						}
					}
				}
			}
            return true;
		}



        /// <summary>
        /// Check in FPASS that the given mask is not lent out
        /// Query database view.
        /// If no record for the given mask then it has never been lent, mask not lent
        /// Get max receive & delivery date, if receive later than delivery then mask lent
        /// unless date of return is current date or earlier
        /// If delivery later than receive, and delivery date in the future then mask is lent, else mask not lent
        /// TODO: only Florix.
        /// </summary>
        ///
        /// <returns></returns>
        protected bool MaskIsAlreadyLent()
        {
            int numRecs = 0;
            DateTime lentDate = DateTime.Now;
            DateTime returnDate = DateTime.Now;           

            mSelComm = mProvider.CreateCommand(FIND_MASK_LENT_FPASS);
            mProvider.SetParameter(mSelComm, MASK_NUMBER_PARA, this.mMaskNoLent);
            IDataReader mDR = mProvider.GetReader(mSelComm);
            while (mDR.Read())
            {
                numRecs++;
                mCoworkerName = Convert.ToString(mDR["COWORKER"]);

                // If returndate is null then mask is already lent
                if (mDR["REMA_DATE_RET"].Equals(DBNull.Value))
                {
                    mDR.Close();
                    return true;
                }
                else
                {
                    lentDate = Convert.ToDateTime(mDR["REMA_DATE_LENT"]);
                    returnDate = Convert.ToDateTime(mDR["REMA_DATE_RET"]);                   
                }
            }
            mDR.Close();

            // SELECT returned no values: mask has never been lent
            if (numRecs == 0)
            {
                return false;
            }

            // New logic: date from GUI cane be in past but not future
            // If return date after receive date, check return date is before GUI date
            if (returnDate > lentDate)
            {

                // If mask is being lent out (parameter 1), then can already haven been lent to another FFMA,
                // have to check that GUI date is after last lend period			
                if (this.mMaskDate > returnDate)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                // receive date is later than return date: mask is lent
                return true;
            }
        }


		/// <summary>
		/// Makes sure masks are lent in chronological order
		/// Gets latest date of mask action (must actually be return as earlier checks to make sure
		/// CWR can only lend out 1 mask: select max from db
		/// If current date in GUI is earlier than this date, throw error
		/// </summary>
		/// <returns></returns>
		private void GetLastMaskActionDate()
		{
			mSelComm = null;		
			mLastMaskDate = Convert.ToDateTime("01.01.1900");
			
			try 
			{			
				mSelComm = mProvider.CreateCommand(FINDLATESTMASKDATE);		
				mProvider.SetParameter(mSelComm, MASK_CWRID_PARAM, mCoWorkerID);

				// Open data reader to get assignments ExContractor-mask, count how many of each
				IDataReader mDR = mProvider.GetReader(mSelComm);
				while (mDR.Read())
				{
					if ( !mDR["MAXMASKDATE"].Equals(DBNull.Value) )
					{
						mLastMaskDate = Convert.ToDateTime( mDR["MAXMASKDATE"] );
					}
				}
				mDR.Close();
			}	
			catch (System.Data.OracleClient.OracleException oraex)
			{
				throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(
					MessageSingleton.FATAL_DB_ERROR) 
					+ oraex.Message );
			}	
		}

		#endregion // End of Methods

	}
}

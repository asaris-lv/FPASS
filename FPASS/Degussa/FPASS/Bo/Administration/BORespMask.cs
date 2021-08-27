using System;
using Degussa.FPASS.Util;

namespace Degussa.FPASS.Bo.Administration
{
	/// <summary>
    /// Summary description for BORespMask.
    /// Holds the values of Respiration Mask
	/// </summary>
	/// <remarks>
	/// <para><b>History</b></para>
	/// <div class="tablediv">
	/// <table class="dtTABLE" cellspacing="0">
	///		<tr>
	///			<th width="20%">PTA GmbH</th>
	///			<th width="20%">13/10/2017</th>
	///			<th width="60%">Remarks</th>
	///		</tr>
	/// </table>
	/// </div>
	/// </remarks>
     
    public class BORespMask
    {

        #region Members

        private int mRemaId = 0;
        private int mRemaCwrId = 0;
        private int mRemaRmtyId = 0;
        private string mRmtySystem = String.Empty;
        private int mRemaUserId = 0;
        private string mRemaMaskNo = String.Empty;
        private DateTime? mRemaDate = null;
        private DateTime? mRemaTimestamp = null;
        private string mUsUserid = String.Empty;
        private string mUserNiceName = String.Empty;
        private DateTime? mNextMaintDate = null;
        private Boolean mIsReadOnly = false;
        private Boolean mHasChanged = false;

        #endregion //End of Members

        #region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
        public BORespMask()
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

        }

        #endregion //End of Initialization

        #region Accessors

        /// <summary>
        /// RespMaskEntryId (REMA_ID) from Database, technical id of lent or return entry
        /// </summary>
        public int MaskId
        {
            get
            {
                return mRemaId;
            }
            set
            {
                mRemaId = value;
            }
        }

        /// <summary>
        /// CoworkerId from Database
        /// </summary>
        public int RemaCwrId
        {
            get
            {
                return mRemaCwrId;
            }
            set
            {
                mRemaCwrId = value;
            }
        }

        /// <summary>
        /// MaskTypeId (REMA_RMTY_ID) from Database, e.g. 1 = FlorixLent, 2 = FlorixReturn, 11 = TecbosLent, 12 = TecbosReturn
        /// </summary>
        public int MaskTypeId
        {
            get
            {
                return mRemaRmtyId;
            }
            set
            {
                mRemaRmtyId = value;
            }
        }

        /// <summary>
        /// RmtySystem from Database, e.g. FLORIX, TECBOS
        /// </summary>
        public string RmtySystem
        {
            get
            {
                return mRmtySystem;
            }
            set
            {
                mRmtySystem = value;
            }
        }

        /// <summary>
        /// RemaUserId from Database
        /// </summary>
        public int RemaUserId
        {
            get
            {
                return mRemaUserId;
            }
            set
            {
                mRemaUserId = value;
            }
        }
 
        /// <summary>
        /// Masknumber, Barcode (REMA_MASKNO) from Database, e.g. A1234567
        /// </summary>
        public string MaskNo
        {
            get
            {
                return mRemaMaskNo;
            }
            set
            {
                mHasChanged = (mRemaMaskNo != value) ? true : false;
                mRemaMaskNo = value;
            }
        }

        /// <summary>
        /// Mask date (REMA_DATE) from Database, date when mask was lent or returned.
        /// If there is no date from database, DateTime.Now will be set.
        /// </summary>
        public DateTime? MaskDate
        {
            get
            {
                if (null == mRemaDate)
                {
                    mRemaDate = DateTime.Now;
                }
                return mRemaDate;
            }
            set
            {
                if (null != mRemaDate && null != value)
                {
                    if (Convert.ToDateTime(mRemaDate).Date.CompareTo(Convert.ToDateTime(value).Date) != 0)
                    {
                        mHasChanged = true;
                    }
                }
                else if (null == mRemaDate && null != value)
                {
                    mHasChanged = true;
                }

                mRemaDate = value;
            }
        }

        /// <summary>
        /// mRemaTimestamp from Database
        /// </summary>
        public DateTime? RemaTimestamp
        {
            get
            {
                return mRemaTimestamp;
            }
            set
            {
                mRemaTimestamp = value;
            }
        }

        /// <summary>
        /// Userid (US_USERID) from Database, e.g. w1234
        /// </summary>
        public string Userid
        {
            get
            {
                return mUsUserid;
            }
            set
            {
                mUsUserid = value;
            }
        }
 
        /// <summary>
        /// UserNiceName from Database, e.g. Mustermann, Max (1234)
        /// </summary>
        public string UserName
        {
            get
            {
                return mUserNiceName;
            }
            set
            {
                mUserNiceName = value;
            }
        }

        /// <summary>
        /// NextMaintDate from Database (only for Tecbos), not give for Florix
        /// </summary>
        public DateTime? NextMaintDate
        {
            get
            {
                return mNextMaintDate;
            }
            set
            {
                mNextMaintDate = value;
            }
        }
        
        /// <summary>
        /// Boolean indicating wether the mask belongs to FLORIX
        /// </summary>
        public Boolean IsMasksystemFlorix
        {
            get 
            {
                return (mRemaRmtyId == Globals.RespMaskIdLentFlo || mRemaRmtyId == Globals.RespMaskIdReturnFlo) ? true : false;
            }
        }

        /// <summary>
        /// Boolean indicating wether the mask belongs to TECBOS
        /// </summary>
        public Boolean IsMasksystemTecbos
        {
            get
            {
                return (mRemaRmtyId == Globals.RespMaskIdLentTec || mRemaRmtyId == Globals.RespMaskIdReturnTec) ? true : false;
            }
        }

        /// <summary>
        /// Boolean indicating wether the mask is lent
        /// </summary>
        public Boolean IsMaskLent
        {
            get
            {
                return (mRemaRmtyId == Globals.RespMaskIdLentTec || mRemaRmtyId == Globals.RespMaskIdLentFlo) ? true : false;
            }
        }

        /// <summary>
        /// Boolean indicating wether the mask is returned
        /// </summary>
        public Boolean IsMaskReturned
        {
            get
            {
                return (mRemaRmtyId == Globals.RespMaskIdReturnTec || mRemaRmtyId == Globals.RespMaskIdReturnFlo) ? true : false;
            }
        }


        /// <summary>
        /// Boolean indicating wether the mask is empty (not filled).
        /// </summary>
        public Boolean IsMaskEmpty
        {
            get
            {
                return (mRemaMaskNo == String.Empty) ? true : false;
            }
        }


        /// <summary>
        /// Boolean indicating wether the mask is readonly.
        /// true if MaskId > 0
        /// </summary>
        public Boolean IsReadOnly
        {
            get
            {
                return (0 < mRemaId) ? true : false; 
            }
        }


        /// <summary>
        /// Boolena indicating wether the values of MaskDate and MaskNo have changed.
        /// </summary>
        public Boolean HasChanged
        {
            get
            {
                return mHasChanged;
            }
            set 
            {
                mHasChanged = value;
            }
        }


        #endregion //End of Accessors

        #region Methods

        #endregion // End of Methods
    }
}




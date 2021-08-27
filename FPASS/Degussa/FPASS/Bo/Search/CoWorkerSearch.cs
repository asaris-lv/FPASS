using System;

using Degussa.FPASS.Util;

namespace Degussa.FPASS.Bo.Search
{
	/// <summary>
	/// Simple BO which acts as a container. Only used to display data in a grid. 
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
	public class CoWorkerSearch
	{
		#region Members

        // Constants for name of columns
        internal const string ID_COL = "FFMAID";
        internal const string SURNAME = "Surname";
        internal const string FIRSTNAME = "Firstname";
        internal const string PKI = "PKI";
        internal const string WINDOWS_ID = "WindowsId";
        internal const string SMARTACT_NO = "SmartActNo";
        internal const string ID_CARD_HITAG = "IdCardHitag";
        internal const string ID_CARD_MIFARE = "IdCardMifare";
        internal const string DATE_OF_BIRTH = "DateOfBirth";
        internal const string EXCONTRACTOR = "ExContractorName";
        internal const string SUPERVISOR = "SuperNameAndTel";
        internal const string SUBCONTRACTOR = "SubContractor";
        internal const string COORDINATOR = "CoordNameAndTel";
        internal const string VALID_UNTIL = "ValidUntil";
        internal const string STATUS = "Status";
        internal const string ACCESS = "Access";
        internal const string ZKS_RET = "ZKSReturncode";


        private string mStatus;
        /// <summary>
        /// name and telephone number of the supervisor the coworker belongs to
        /// </summary>
        private string mSuperNameAndTel;

        /// <summary>
        /// name and telephone number of the coordinator the coworker belongs to
        /// </summary>
        private string mCoordNameAndTel;

		/// <summary>
		/// id of coworker (db-pk)
		/// </summary>
        public decimal CoWorkerId { get; set; }

		/// <summary>
		/// coworker PERSNO in FPASS
		/// </summary>
        public decimal PersNo { get; set; }

        /// <summary>
        /// coworker PERSNO in SmartAct
        /// </summary>
        public string SmartActNo { get; set; }

		/// <summary>
		/// flag indicating wether the coworker is over eighteen or not
		/// </summary>
        public bool OverEighteen { get; set; }

		/// <summary>
		/// surname of a coworker
		/// </summary>
		public string Surname  { get; set; }
        
		/// <summary>
		/// firstname of a coworker
		/// </summary>
		public string Firstname { get; set; }

		/// <summary>
		/// date of birth of a coworker
		/// </summary>
		public string DateOfBirth { get; set; }

		/// <summary>
		/// place of birth of a coworker
		/// </summary>
		public string PlaceOfBirth { get; set; }

        /// <summary>
		/// Hitag id card number
		/// </summary>
        public decimal? IdCardNumHitag { get; set; }

        /// <summary>
		/// Mifare id card number
		/// </summary>
        public decimal? IdCardNumMifare { get; set; }

        /// <summary>
		/// WindowsId (KonzernId)
		/// </summary>
		public string WindowsId { get; set; }

        /// <summary>
		/// Does Id card have a PKI chip
		/// </summary>
		public bool PKIChip { get; set; }

		/// <summary>
		/// validuntil of coworker entrance
		/// </summary>
		public string ValidUntil { get; set; }

        /// <summary>
        /// Gets and sets name of external contractor
        /// </summary>
        public string ExContractorName  { get; set; }
      
        /// <summary>
        /// Debit number of external contractor the coworker belongs to
        /// </summary>
        public string ExContractorDebitNo { get; set; }

        /// <summary>
        /// Simple getter and setter.
        /// </summary>
        public string Supervisor { get; set; }
       
        /// <summary>
        /// Simple getter and setter.
        /// </summary>
        public string SupervisTel { get; set; }
       
        /// <summary>
        /// Simple getter and setter.
        /// </summary>
        public string SubContractor { get; set; }
        
        /// <summary>
        /// Simple getter and setter.
        /// </summary>
        public string Coordinator { get; set; }
        
        /// <summary>
        /// Simple getter and setter.
        /// </summary>
        public string CoordTel { get; set; }

        /// <summary>
        /// Current acces (long. middle, short)
        /// </summary>
        public string Access { get; set; }       

        /// <summary>
        /// Returncode from ZKS
        /// </summary>
        public string ZKSReturncode { get; set; }



		#endregion //End of Members


		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public CoWorkerSearch()
		{
			initialize();
		}

		#endregion 


		#region Initialization

		/// <summary>
		/// Initializes the members.
		/// </summary>
		private void initialize()
		{
			this.mStatus = "";
		}	

		#endregion 


		#region Accessors 

		
		
		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public string SuperNameAndTel
		{
			get 
			{
				if ( mSuperNameAndTel.Trim().StartsWith("(Tel.") ) 
				{
					return String.Empty;
				} 
				else 
				{
					return(mSuperNameAndTel);
				}
			}
			set 
			{
				mSuperNameAndTel = value;
			}
		}

		/// <summary>
        /// Gets and sets Coordinator's name and phone no.
		/// </summary>
		public string CoordNameAndTel
		{
			get 
			{
				return(mCoordNameAndTel);
			}
			set 
			{
				mCoordNameAndTel = value;
			}
		}

	
		/// <summary>
		/// Holds coworkers current status.
		/// Status is held in database and updated daily and by calculation of expiry date:
		/// need it to be accurate
		/// Last change 17.02.2005: status ALTDATEN (no bookings in ZKS for more than a year)
		/// not shown in FPASS, this was corrected
		/// </summary>
		public string Status 
		{
			get 
			{
                if (this.mStatus.Trim().Equals(Globals.STATUS_VALID))
                {
                    return "Gültig";
                }
                else if (this.mStatus.Trim().Equals(Globals.STATUS_OLD))
                {
                    return "Altdaten";
                }
                else if (!mStatus.Trim().Equals(Globals.PLACEHOLDER_ARCHIVE)
                && !mStatus.Trim().Equals(Globals.PLACEHOLDER_NONFPASS))
                {
                    return "Ungültig";
                }
                else return this.mStatus.Trim();
			}
			set 
			{
				this.mStatus = value;
			}
		}

		#endregion //End of Accessors



		#region Methods 

		/// <summary>
		/// Compares only the date of the given DateTime objects and 
		/// returns an indication of their relative values.
		/// </summary>
		/// <param name="pDate1">firstDate</param>
		/// <param name="pDate2">secondDate</param>
		/// <returns>0 if dates are equal, -1 if firstdate is relative less than second, 
		/// 1 otherwise</returns>
		private int CompareDates(DateTime pDate1, DateTime pDate2 ) 
		{
			int ret = pDate1.Date.CompareTo(pDate2.Date);
			return ret;
		}

		#endregion // End of Methods

	}
}

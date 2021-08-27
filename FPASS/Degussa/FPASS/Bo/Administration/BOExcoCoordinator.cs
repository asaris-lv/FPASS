using System;

namespace Degussa.FPASS.Bo.Administration
{
	/// <summary>
	/// Summary description for BOPrecMedical.
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
	public class BOExcoCoordinator
	{
		#region Members

		private decimal mEXCOID;
		private decimal mECODID;
		private string	mExContractor;
        private string mDebitNo;
		private string  mSubContractor;
		private string  mCoordinator;
		private string  mSupervisor;
	
		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public BOExcoCoordinator()
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
        /// Gets and sets ExContractor PK Id
		/// </summary>
		public decimal EXCOID
		{
			get 
			{
				return mEXCOID;
			}
			set 
			{
				mEXCOID = value;
			}
		} 

		/// <summary>
        /// Gets and sets ExContractor-Coord PK Id
		/// </summary>
		public decimal ECODID 
		{
			get 
			{
				return mECODID;
			}
			set 
			{
				mECODID = value;
			}
		} 
	
		/// <summary>
        /// Gets and sets ExContractor name
		/// </summary>
		public string ExContractor 
		{
			get 
			{
				return mExContractor;
			}
			set 
			{
				mExContractor = value;
			}
		}

        /// <summary>
        /// Gets and sets ExContractor debitno.
        /// </summary>
        public string DebitNo
        {
            get
            {
                return mDebitNo;
            }
            set
            {
                mDebitNo = value;
            }
        } 

		/// <summary>
        /// Gets and sets SubContractor debitno.
		/// </summary>
		public string SubContractor
		{
			get 
			{
				return mSubContractor;
			}
			set 
			{
				mSubContractor = value;
			}
		} 

		/// <summary>
        /// Gets and sets Coordinator as "surname, firstname"
		/// </summary>
		public string Coordinator
		{
			get 
			{
				return mCoordinator;
			}
			set 
			{
				mCoordinator = value;
			}
		} 

		/// <summary>
        /// Gets and sets Coordinator as "surname, firstname"
		/// </summary>
		public string Supervisor
		{
			get 
			{
				return mSupervisor;
			}
			set 
			{
				mSupervisor = value;
			}
		} 
			
		#endregion

		#region Methods 

		#endregion // End of Methods

	}
}

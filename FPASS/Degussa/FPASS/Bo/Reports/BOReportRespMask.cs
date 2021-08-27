using System;

namespace Degussa.FPASS.Bo.Reports
{
	/// <summary>
	/// Summary description for BOReportRespMask.
	/// </summary>
	/// <remarks>
	/// <para><b>History</b></para>
	/// <div class="tablediv">
	/// <table class="dtTABLE" cellspacing="0">
	///		<tr>
	///			<th width="20%">PTA GmbH</th>
	///			<th width="20%">29/04/2004</th>
	///			<th width="60%">Remarks</th>
	///		</tr>
	/// </table>
	/// </div>
	/// </remarks>
	public class BOReportRespMask
	{
		#region Members

		private decimal mCwrID;
        private string mMaskNo;
        private string mMaskSystem;
		private string	mMaskReceived;
		private string  mMaskDelivered;
		private string  mFFMA;
		private string  mCoordinator;
		private string  mTelCoordinator;
		private string  mExContractor;
		private string  mTelExContractor;
		private string  mMaskService;
	

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public BOReportRespMask()
		{
			
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

		public decimal CwrID 
		{
			get 
			{
				return mCwrID;
			}
			set 
			{
				mCwrID = value;
			}
		} 
	
		
		public string MaskNo 
		{
			get 
			{
				return mMaskNo;
			}
			set 
			{
				mMaskNo = value;
			}
		}

        public string MaskSystem 
        {
            get
            {
                return mMaskSystem;
            }
            set
            {
                mMaskSystem = value;
            }
        }

		public string MaskReceived 
		{
			get 
			{
				return mMaskReceived;
			}
			set 
			{
				mMaskReceived = value;
			}
		}

		public string MaskDelivered 
		{
			get 
			{
				return mMaskDelivered;
			}
			set 
			{
				mMaskDelivered = value;
			}
		}

		public string FFMA 
		{
			get 
			{
				return mFFMA;
			}
			set 
			{
				mFFMA = value;
			}
		}

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

		public string TelCoordinator 
		{
			get 
			{
				return mTelCoordinator;
			}
			set 
			{
				mTelCoordinator = value;
			}
		}

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

		public string TelExContractor 
		{
			get 
			{
				return mTelExContractor;
			}
			set 
			{
				mTelExContractor = value;
			}
		}

		public string MaskService 
		{
			get 
			{
				return mMaskService;
			}
			set 
			{
				mMaskService = value;
			}
		}

		
		#endregion //End of Accessors

		#region Methods 

		#endregion // End of Methods

		
	}
}

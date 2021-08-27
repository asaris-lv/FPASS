using System;

namespace Degussa.FPASS.Bo.Reports
{
	/// <summary>
	/// Summary description for BOReportsCheckList.
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
	public class BOReportsCheckList
	{
		#region Members
		
		private decimal mCHLSID;
		private decimal	mTK;
		private decimal	mPersNo;
		private string  mSurname;
		private string  mFirstname;
		private string  mValidFrom;
		private string  mValidUntil;
		private string  mAuthorised;
		private string  mExContractor;
		private string  mSubContractor;
		private string  mStatus; 
        private	decimal	mIDCardNoFpass;
		private	decimal	mIDCardNoZks;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public BOReportsCheckList()
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
		
		public decimal CHLSID 
		{
			get 
			{
				return mCHLSID;
			}
			set 
			{
				mCHLSID = value;
			}
		} 

		public decimal IDCardNoFpass 
		{
			get 
			{
				return mIDCardNoFpass;
			}
			set 
			{
				mIDCardNoFpass = value;
			}
		}
 
		public String IdCardNoFpassAsString 
		{
			get 
			{
				if ( mIDCardNoFpass > 0 ) 
				{
					return mIDCardNoFpass.ToString();
				} 
				else 
				{
					return String.Empty;
				}
			}
		}

		public decimal IDCardNoZks
		{
			get 
			{
				return mIDCardNoZks;
			}
			set 
			{
				mIDCardNoZks = value;
			}
		} 

		public String IdCardNoZKSAsString 
		{
			get 
			{
				if ( mIDCardNoZks > 0 ) 
				{
					return mIDCardNoZks.ToString();
				} 
				else 
				{
					return String.Empty;
				}
			}
		}
		
		public decimal TK 
		{
			get 
			{
				return mTK;
			}
			set 
			{
				mTK = value;
			}
		} 

		public decimal PersNo 
		{
			get 
			{
				return mPersNo;
			}
			set 
			{
				mPersNo = value;
			}
		} 
	
		
		public string Surname 
		{
			get 
			{
				return mSurname;
			}
			set 
			{
				mSurname = value;
			}
		}

		public string Firstname 
		{
			get 
			{
				return mFirstname;
			}
			set 
			{
				mFirstname = value;
			}
		}

		public string Authorised 
		{
			get 
			{
				return mAuthorised;
			}
			set 
			{
				mAuthorised = value;
			}
		}

		public string ValidFrom 
		{
			get 
			{
				return mValidFrom;
			}
			set 
			{
				mValidFrom = value;
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

		public string ValidUntil 
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

		public string Status 
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

		#endregion //End of Accessors

		#region Methods 

		#endregion // End of Methods


	}
}

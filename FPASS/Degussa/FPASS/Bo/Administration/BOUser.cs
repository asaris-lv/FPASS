using System;
using System.Collections;

namespace Degussa.FPASS.Bo.Administration
{
	/// <summary>
	/// Summary description for BOUser.
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
	public class BOUser
	{
		#region Members

		/// <summary>
		/// unique PK identifier from UM_USER table
		/// </summary>
		private decimal mUMPKIdentifier;
		/// <summary>
		/// unique PK identifier from FPASS_USER table
		/// </summary>
		private decimal mFpassPKIdentifier;
		/// <summary>
		/// General string attributes
		/// </summary>
		private string	mSurname;
		private string	mFirstname;
		private string	mFormattedName;
		private string	mApplUserID;
		private string	mTelephone;
		private string	mBothNamesTel;
		private string  mRoleNameAssigned;
		private int		mDeptID;
		private string  mDeptName;
		private int		mDomainID;
        private string  mDomainName;

		/// <summary>
		/// Used to note which roles assigned: coordinator & plant manager
		/// </summary>
		private bool    mIsCoordinator = false;
		private bool    mIsPlantManager = false;
		
		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public BOUser()
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
        /// Unique PK identifier from UM_USER table
		/// </summary>
		public decimal UMPKIdentifier
		{
			get 
			{
				return mUMPKIdentifier;
			}
			set 
			{
				mUMPKIdentifier = value;
			}
		} 

		/// <summary>
        /// Unique PK identifier from FPASS_USER table
		/// </summary>
		public decimal FPASSPKIdentifier
		{
			get 
			{
				return mFpassPKIdentifier;
			}
			set 
			{
				mFpassPKIdentifier = value;
			}
		} 
		
		/// <summary>
		/// Simple getter and setter.
		/// </summary>
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

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
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
		
		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public string UserFormattedName
		{
			get 
			{
				return mFormattedName;
			}
			set 
			{
				mFormattedName = value;
			}
		} 
		
		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public string ApplUserID
		{
			get 
			{
				return mApplUserID;
			}
			set 
			{
				mApplUserID = value;
			}
		} 

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public string Telephone
		{
			get 
			{
				return mTelephone;
			}
			set 
			{
				mTelephone = value;
			}
		} 

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public string RoleNameAssigned
		{
			get 
			{
				return mRoleNameAssigned;
			}
			set 
			{
				mRoleNameAssigned = value;
			}
		}

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public int DeptID
		{
			get 
			{
				return mDeptID;
			}
			set 
			{
				mDeptID = value;
			}
		} 

		/// <summary>
		/// Simple getter & setter
		/// </summary>
		public string DeptName
		{
			get 
			{
				return mDeptName;
			}
			set 
			{
				mDeptName = value;
			}
		} 

		/// <summary>
        /// Gets or sets user domain Id
		/// </summary>
		public int DomainID
		{
			get 
			{
				return mDomainID;
				}
			set 
			{
				mDomainID = value;
			}
		}

        /// <summary>
        /// Gets or sets user domain name
        /// </summary>
        public string DomainName
        {
            get
            {
                return mDomainName;
            }
            set
            {
                mDomainName = value;
            }
        } 

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public bool IsCoordinator
		{
			get 
			{
				return mIsCoordinator;
			}
			set 
			{
				mIsCoordinator = value;
			}
		}

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public bool IsPlantManager
		{
			get 
			{
				return mIsPlantManager;
			}
			set 
			{
				mIsPlantManager = value;
			}
		}


		#endregion //End of Accessors

		#region Methods 

		#endregion // End of Methods

	}
}

using System;

namespace de.pta.Component.N_UserManagement.Authorization
{
	/// <summary>
	/// Definition of an user. Users can be assigned to a user group or directly to an authorization role
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> R.Weiﬂ, PTA GmbH
	/// <b>Date:</b> Aug/12/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class User : AuthorizedEntity, IUser
	{
		#region Members

		/// <summary>
		/// First name of the user
		/// </summary>
		private	String		mFirstName;

		/// <summary>
		/// The User's id for login.
		/// </summary>
		private	String		mUserId;

		/// <summary>
		/// The user's password for login.
		/// </summary>
		private	String		mPassword;

		/// <summary>
		/// Indicator wether user logged in for the first time.
		/// </summary>
		private	bool		mFirstLogin;

		/// <summary>
		/// The user group the user belongs to.
		/// </summary>
		private	UserGroup	mAssignedUserGroup;

		#endregion //End of Members

		#region Constructors

		public User()
		{
			initialize();
		}

		#endregion //End of Constructors

		#region Initialization

		private void initialize()
		{
		}	

		#endregion //End of Initialization

		#region Accessors 

		/// <summary>
		/// Accessor for user's first name.
		/// </summary>
		public String FirstName
		{
			get
			{
				return mFirstName;
			}
			set
			{
				mFirstName = value;
			}
		}

		/// <summary>
		/// Accessor for user id.
		/// </summary>
		public String UserId
		{
			get
			{
				return mUserId;
			}
			set
			{
				mUserId = value;
			}
		}

		/// <summary>
		/// Accessor for user's password.
		/// </summary>
		public String Password
		{
			get
			{
				return mPassword;
			}
			set
			{
				mPassword = value;
			}
		}

		/// <summary>
		/// Accessor for flag wether user logged in for the first time.
		/// </summary>
		public bool FirstLogin
		{
			get
			{
				return mFirstLogin;
			}
			set
			{
				mFirstLogin = value;
			}
		}

		/// <summary>
		/// Accessor for user's user group.
		/// </summary>
		public IUserGroup AssignedUserGroup
		{
			get
			{
				return mAssignedUserGroup;
			}
			set
			{
				mAssignedUserGroup = (UserGroup)value;
			}
		}

		#endregion //End of Accessors

	}
}

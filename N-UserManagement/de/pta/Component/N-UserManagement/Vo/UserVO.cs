using System;

using de.pta.Component.N_UserManagement.Authorization;

namespace de.pta.Component.N_UserManagement.Vo 
{
	/// <summary>
	/// Value object for user objects from database
	/// </summary>
	/// <remarks><pre>
	/// <b>History</b>
	/// <b>Author:</b> R.Weiﬂ, PTA GmbH
	/// <b>Date:</b> Sep/25/2003
	/// <b>Remarks:</b> None
	/// </pre></remarks>
	public class UserVO : IUser 
	{

		#region Members

		/// <summary>
		/// First name of the user
		/// </summary>
		private String mFirstName;
		/// <summary>
		/// The User's id for login.
		/// </summary>
		private String mUserId;
		/// <summary>
		/// The user's password for login.
		/// </summary>
		private String mPassword;
		/// <summary>
		/// Indicator wether user logged in for the first time.
		/// </summary>
		private bool mFirstLogin;
		/// <summary>
		/// internal id of user
		/// </summary>
		private int mId;
		/// <summary>
		/// The user's name
		/// </summary>
		private String mName;
		/// <summary>
		/// internal id of user group assigned to user
		/// </summary>
		private int mUserGroupId;

		#endregion //End of Members

		#region Constructors

		public UserVO()
		{

		}

		#endregion //End of Constructors

		#region Initialization

		private void initialize()
		{

		}

		#endregion //End of Initialization

		#region Accessors 

		/// <summary>
		/// Accessor for internal id of user group assigned to user
		/// </summary>
		public int UserGroupId
		{
			get{
				return mUserGroupId;
			}
			set{
				mUserGroupId = value;
			}
		}

		/// <summary>
		/// Accessor for the user's name
		/// </summary>
		public String Name
		{
			get{
				return mName;
			}
			set{
				mName = value;
			}
		}

		/// <summary>
		/// Accessor for user's first name.
		/// </summary>
		public String FirstName{
			get{
				return mFirstName;
			}
			set{
				mFirstName = value;
			}
		}

		/// <summary>
		/// Accessor for user id.
		/// </summary>
		public String UserId{
			get{
				return mUserId;
			}
			set{
				mUserId = value;
			}
		}

		/// <summary>
		/// Accessor for user's password.
		/// </summary>
		public String Password{
			get{
				return mPassword;
			}
			set{
				mPassword = value;
			}
		}

		/// <summary>
		/// Accessor for flag wether user logged in for the first time.
		/// </summary>
		public bool FirstLogin{
			get{
				return mFirstLogin;
			}
			set{
				mFirstLogin = value;
			}
		}

		/// <summary>
		/// Accessor for internal id of user
		/// </summary>
		public int Id
		{
			get{
				return mId;
			}
			set{
				mId = value;
			}
		}

		#endregion //End of Accessors

	}//end UserVO
}//end namespace Vo
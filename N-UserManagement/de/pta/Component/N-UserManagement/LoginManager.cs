using System;

using de.pta.Component.N_UserManagement.Authorization;

namespace de.pta.Component.N_UserManagement
{
	/// <summary>
	/// Singleton providing services for login to he applicazion.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> R.Weiﬂ, PTA GmbH
	/// <b>Date:</b> Aug/14/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	public class LoginManager
	{
		#region Members

		/// <summary>
		/// Contains one single instance of LoginManager.
		/// </summary>
		static private LoginManager	mLoginManager;

		/// <summary>
		/// The userid a user typed at login.
		/// </summary>
		private String mLoginUserId;

		/// <summary>
		/// The password a user typed at login.
		/// </summary>
		private String mLoginPassword;

		private int mMandatorID;

		#endregion //End of Members

		#region Constructors

		private LoginManager()
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
		/// Returns the instance of the UserManager
		/// </summary>
		/// <returns>UserManager</returns>
		static public LoginManager getInstance()
		{
			if (mLoginManager == null)
			{
				mLoginManager = new LoginManager();
			}
			return mLoginManager;
		}


		/// <summary>
		/// Accessor for the userid a user typed at login.
		/// </summary>
		public String LoginUserId
		{
			get
			{
				return mLoginUserId;
			}
			set
			{
				mLoginUserId = value;
			}
		}

		/// <summary>
		/// Accessor for the password a user typed at login.
		/// </summary>
		public String LoginPassword
		{
			get
			{
				return mLoginPassword;
			}
			set
			{
				mLoginPassword = value;
			}
		}

		/// <summary>
		/// Accessor for mandator id.
		/// </summary>
		public int MandatorID
		{
			get
			{
				return mMandatorID;
			}
			set
			{
				mMandatorID = value;
			}
		}

		#endregion //End of Accessors


		#region Methods

		/// <summary>
		/// Verification of user login by delegating input to UserManager singleton
		/// Benz  Changed 17.10.03: due to project requirements
		/// roleList is not read automatically anymore when verifying login, cause there
		/// is no unique mandator at this moment
		/// </summary>
		/// <returns>
		/// <code>true</code>if userid and password are valid
		/// <code>false</code>if login is invalid
		/// </returns>
		public bool verifyLogin()
		{
			bool userVerified;

			userVerified = UserManager.getInstance().verifyLogin( this.LoginUserId, this.LoginPassword);
			return userVerified;
		}


		/// <summary>
		/// reads mandator dependet rolelist by delegating input to AuthorizationManager singleton
		/// pMandator id is hold in member mMandatorID of LoginManager
		/// </summary>
		/// <param name="pMandatorID"></param>
		public void ReadMandatorDependentRoleList(int pMandatorID) 
		{
			mMandatorID = pMandatorID;
			AuthorizationManager.getInstance().readRoleList((User)UserManager.getInstance().CurrentUser);
		}

		/// <summary>
		/// Verification of user login
		/// </summary>
		/// <param name="pLoginUserId">userid typed in by user</param>
		/// <param name="pLoginPassword">password typed in by user</param>
		/// <returns></returns>
		public bool verifyLogin(String pLoginUserId, String pLoginPassword)
		{
			this.LoginUserId = pLoginUserId;
			this.LoginPassword = pLoginPassword;
			return this.verifyLogin();
		}

		#endregion //End of Methods

	}
}

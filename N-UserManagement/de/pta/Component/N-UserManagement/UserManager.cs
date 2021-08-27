using System;
using System.Collections;

using de.pta.Component.N_UserManagement.DataAccess;
using de.pta.Component.N_UserManagement.Vo;
using de.pta.Component.N_UserManagement.Authorization;
using de.pta.Component.N_UserManagement.Exceptions;

namespace de.pta.Component.N_UserManagement
{
	/// <summary>
	/// Singleton providing services concerning a user. This includes the verfication of a user 
	/// and features for managing user properties.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> R.Weiﬂ, PTA GmbH
	/// <b>Date:</b> Aug/14/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	public class UserManager
	{
		#region Members

		/// <summary>
		/// Contains one single instance of UserManager.
		/// </summary>
		static private UserManager	mUserManager;

		/// <summary>
		/// The user currently logged in.
		/// </summary>
		private User	mCurrentUser;

		#endregion //End of Members

		#region Constructors

		private UserManager()
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
		static public UserManager getInstance()
		{
			if (mUserManager == null)
			{
				mUserManager = new UserManager();
			}
			return mUserManager;
		}

		/// <summary>
		/// Accessor for the user currently logged in.
		/// </summary>
		public IUser CurrentUser
		{
			get
			{
				return mCurrentUser;
			}
			set
			{
				mCurrentUser = (User)value;
			}
		}
		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// Verifies the login information a user typed and confirmed in a login dialog.
		/// </summary>
		/// <param name="pLoginUserId">The userid a user typed in.</param>
		/// <param name="pLoginPassword">The password a user typed in.</param>
		/// <returns>
		/// <code>true</code> if a user is logged in to the application.
		/// <code>false</code> if a user typed in an invalid userid or password.
		/// </returns>
		internal bool verifyLogin(String pLoginUserId, String pLoginPassword)
		{
			bool		userVerified = false;
			ArrayList	userValueObjects;
			User		userSearchCriteria;

			AdministrationManager dataAccess = AdministrationManager.getInstance();

			userSearchCriteria = new User();
			userSearchCriteria.UserId = pLoginUserId;
			userSearchCriteria.Password = pLoginPassword;

			userValueObjects = dataAccess.getUserByLogin(pLoginUserId, pLoginPassword);
			/* Check if user logged in correctly
			 */
			if (userValueObjects.Count == 0) 
			{
				this.CurrentUser = null;
			} 
			else if (userValueObjects.Count > 1)
			{
				throw new PasswordVerificationException("ERROR_MORE_THAN_ONE_USER");
			}
			else if (userValueObjects.Count == 1)
			{
				this.CurrentUser = new User();
				this.CurrentUser.Id = ((UserVO)userValueObjects[0]).Id;
				this.CurrentUser.Name = ((UserVO)userValueObjects[0]).Name;
				this.CurrentUser.FirstName = ((UserVO)userValueObjects[0]).FirstName;
				this.CurrentUser.UserId = ((UserVO)userValueObjects[0]).UserId;
				this.CurrentUser.Password = ((UserVO)userValueObjects[0]).Password;
				this.CurrentUser.FirstLogin = ((UserVO)userValueObjects[0]).FirstLogin;
			}


			if (this.CurrentUser == null) 
			{
				userVerified = false;
			}
			else 
			{
				userVerified = true;
			}

			return userVerified;
		}

		/// <summary>
		/// Changes the user's password. The old password is verified against the password given
		/// by current user object. The new password has to be confirmed by typing in the same
		/// password a second time.
		/// </summary>
		/// <param name="pOldPassword">The user's old password</param>
		/// <param name="pNewPassword">The user's new password.</param>
		/// <param name="pConfirmation">The confirmation of the new password.</param>
		/// <exception cref="de.pta.Component.N_UserManagement.Exception.UserManagementException">
		/// Thrown if there is no current user.
		/// </exception>
		/// <exception cref="de.pta.Component.N_UserManagement.Exception.PasswordVerificationException">
		/// Thrown if old password or password confirmation is invalid 
		/// </exception>
		public void changeUserPassword(String pOldPassword, String pNewPassword, String pConfirmation)
		{
			/* Check if a user is logged in
			 */
			if (this.CurrentUser == null) 
			{
				throw new UserManagementException("ERROR_NOT_LOGGED_IN");
			}

			/* Verify old password 
			 */
			if (pOldPassword.Equals(this.CurrentUser.Password))
			{
				/* Verify new password confirmation
				 */
				if (pConfirmation.Equals(pNewPassword)) 
				{
					mCurrentUser.Password = pNewPassword;
					/* Persist new password */
					// TODO
				}
				else
				{
					throw new PasswordVerificationException("ERROR_INVALID_PASSWORD_CONFIRMATION");
				}
			} 
			else 
			{
				throw new PasswordVerificationException("ERROR_INVALID_OLD_PASSWORD");
			}

		}

		#endregion // End of Methods
	}
}

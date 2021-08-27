using System;
using System.Collections;

using de.pta.Component.N_UserManagement.Authorization;
using de.pta.Component.N_UserManagement.Resource;
using de.pta.Component.N_UserManagement.Exceptions;
using de.pta.Component.N_UserManagement.DataAccess;

namespace de.pta.Component.N_UserManagement
{
	/// <summary>
	/// Singleton providing services concerning the authorizations a user. 
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> R.Weiﬂ, PTA GmbH
	/// <b>Date:</b> Aug/15/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	public class AuthorizationManager
	{
		#region Members

		/// <summary>
		/// Contains one single instance of AuthorizationManager.
		/// </summary>
		static private AuthorizationManager	mAuthorizationManager;

		/// <summary>
		/// The list of roles assigned to the user currently logged in.
		/// </summary>
		private ArrayList mRoleList;

		#endregion //End of Members

		#region Constructors

		private AuthorizationManager()
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
		/// Returns the instance of the AuthorizationManager
		/// </summary>
		/// <returns>AuthorizationManager</returns>
		static public AuthorizationManager getInstance()
		{
			if (mAuthorizationManager == null)
			{
				mAuthorizationManager = new AuthorizationManager();
			}
			return mAuthorizationManager;
		}

		/// <summary>
		/// Accessor for list of roles assigned to the user currently logged in.
		/// </summary>
		public ArrayList RoleList
		{
			get
			{
				return mRoleList;
			}
			set
			{
				mRoleList = value;
			}
		}

		#endregion //End of Accessors

		#region Methods

		/// <summary>
		/// Verification, if user is authorized to use the resource connected to with the id.
		/// If a resource is not included in any of the user's roles, the user is authorized 
		/// to use the resource.
		/// If a resource is found more than once within the user's roles the highest access right is
		/// valid.
		/// </summary>
		/// <param name="pId">The internal id of the resource that is asked for.</param>
		/// <returns>
		/// <code>true</code> if user is authorized.
		/// <code>false</code> if access denied.
		/// </returns>
		public bool isUserAuthorized(int pId)
		{
			IEnumerator		roleIterator;
			IEnumerator		authorizationIterator;
			Role			currentRole;
			de.pta.Component.N_UserManagement.Authorization.Authorization	
							currentAuthorization;

			// Fix 16.10.03: set userAuthorized = false;
			bool userAuthorized = false;

            if (null == this.RoleList)
                return false;

			roleIterator = this.RoleList.GetEnumerator();

			/* Step thru role list of current user
			 */
			while (roleIterator.MoveNext()) 
			{
				currentRole = (Role)roleIterator.Current;
				authorizationIterator = currentRole.Authorizations.GetEnumerator();

				/* Step thru authorization list of role and check if user is authorized for the resource
				 * with the given id
				 */
				while(authorizationIterator.MoveNext()) 
				{
					currentAuthorization = (de.pta.Component.N_UserManagement.Authorization.Authorization)
											authorizationIterator.Current;
					if (currentAuthorization.Resource.Id.Equals(pId))
					{
						/* Read out the access right to the resource. If the resource is found
						 * more than once the highest access right is valid
						 */
						userAuthorized = currentAuthorization.ResourceAccess || userAuthorized;
					}

				}
			}
			return userAuthorized;
		}

		/// <summary>
		/// Reads the role list for the current user from database.
		/// </summary>
		/// <param name="pUser">current user.</param>
		internal void readRoleList(User pUser) 
		{
			AdministrationManager dataAccess = AdministrationManager.getInstance();

			dataAccess.assignRolesToUser(pUser);
			this.RoleList = pUser.Roles;
			if (this.RoleList != null) 
			{
				dataAccess.assignResourcesToRoles(this.RoleList);
			}

		}

		/// <summary>
		/// Returns an authorization object containing the specific rights for dialog functions for create,
		/// update and delete
		/// </summary>
		/// <param name="pDialogId">The internal id of the dialog</param>
		/// <exception cref="UserManagementException">Thrown if id does not belong to a dialog</exception>
		/// <returns>Authorization object</returns>
		public IAuthorization getDialogAuthorization(int pDialogId) 
		{
			de.pta.Component.N_UserManagement.Authorization.Authorization dialogAuthorization = null;

			/* Iterate all roles of user
			 */
			foreach(Role currentRole in this.RoleList) 
			{
				/* Iterate all resources of current role
				 */
				foreach(de.pta.Component.N_UserManagement.Authorization.Authorization currentAuthorization in currentRole.Authorizations) 
				{
					if (currentAuthorization.Resource.Id.Equals(pDialogId)) 
					{
						/* Throw exception if resource is not of type dialog
						 */
						if (!(currentAuthorization.Resource is Dialog)) 
						{
							throw new UserManagementException("ERROR_NOT_A_DIALOG");
						} 
						else 
						{
							/* If the dialog is found more than once the highest access right is valid
							 */
							if (dialogAuthorization != null) 
							{
								dialogAuthorization.ResourceAccess = dialogAuthorization.ResourceAccess || currentAuthorization.ResourceAccess;
								dialogAuthorization.Create = dialogAuthorization.Create || currentAuthorization.Create;
								dialogAuthorization.Delete = dialogAuthorization.Delete || currentAuthorization.Delete;
								dialogAuthorization.Update = dialogAuthorization.Update || currentAuthorization.Update;
							} 
							else 
							{
								dialogAuthorization = currentAuthorization;
							}
						}
					} // End comparing id
				} // End resources of role
			} // End roles of user

			/* If dialog is not in list of user's roles or resoruces user will not have access to
			 * create, update and delete functions of the dialog
			 */
			if (dialogAuthorization == null) 
			{
				// Access rights are set to true by default within initialize method of Authorization class.
				dialogAuthorization = new de.pta.Component.N_UserManagement.Authorization.Authorization();
			}

			return dialogAuthorization;
		}

		#endregion // End of Methods
	}
}

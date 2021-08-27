using System;
using System.Collections;
using System.Data;

using de.pta.Component.N_UserManagement.Exceptions;
using de.pta.Component.N_UserManagement.Authorization;
using de.pta.Component.N_UserManagement.Vo;
using de.pta.Component.N_UserManagement.Resource;
using de.pta.Component.N_UserManagement.Internal;
using de.pta.Component.DataAccess;

namespace de.pta.Component.N_UserManagement.DataAccess
{
	/// <summary>
	/// Includes methods for data access for User management
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> R.Weiß, PTA GmbH
	/// <b>Date:</b> Aug/22/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	public class AdministrationManager
	{
		#region Members

		/// <summary>
		/// Contains one single instance of DatabaseAccess.
		/// </summary>
		static private AdministrationManager	mAdministrationManager;

		#endregion // End of Members

		#region Constructors

		private AdministrationManager()
		{
			this.initialize();
		}

		#endregion // End of constructors

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
		static public AdministrationManager getInstance()
		{
			if (mAdministrationManager == null)
			{
				mAdministrationManager = new AdministrationManager();
			}
			return mAdministrationManager;
		}

		#endregion //End of Accessors

		#region Methods

		/// <summary>
		/// Returns the list of resources
		/// </summary>
		/// <returns>Hashtable containing all resources</returns>
		public Hashtable getResources() 
		{
			ResourceBuilder	resourceBuilder;
			Hashtable		resourceList;

			resourceBuilder = UserManagementConfiguration.getInstance().getResourceBuilder();
			resourceList = resourceBuilder.getResources();
			return resourceList;
		}

		/// <summary>
		/// Returns the list of roles
		/// </summary>
		/// <param name="pRoleSearchCriteria">Criteria for filtering the result set</param>
		/// <returns>Hashtable of RoleVO referring to search criteria</returns>
		public Hashtable getRoles(IRole pRoleSearchCriteria) 
		{
			RoleBuilder	roleBuilder;
			Hashtable	roleList;

			roleBuilder = UserManagementConfiguration.getInstance().getRoleBuilder();
			roleList = roleBuilder.getRoles(pRoleSearchCriteria);
			return roleList;
		}

		/// <summary>
		/// Inserts a new role to the system
		/// </summary>
		/// <param name="pRole">The role to be inserted</param>
		public void insertRole(IRole pRole) 
		{
			RoleBuilder	roleBuilder;
			roleBuilder = UserManagementConfiguration.getInstance().getRoleBuilder();
			roleBuilder.insertRole(pRole);
		}

		/// <summary>
		/// Updates an existing role
		/// </summary>
		/// <param name="pRole">The role to be updated</param>
		public void updateRole(IRole pRole) 
		{
			RoleBuilder	roleBuilder;
			roleBuilder = UserManagementConfiguration.getInstance().getRoleBuilder();
			roleBuilder.updateRole(pRole);
		}

		/// <summary>
		/// Deletes an existing role
		/// </summary>
		/// <param name="pRole">The role to be deleted</param>
		public void deleteRole(IRole pRole) 
		{
			RoleBuilder	roleBuilder;
			roleBuilder = UserManagementConfiguration.getInstance().getRoleBuilder();
			roleBuilder.deleteRole(pRole);
		}

		/// <summary>
		/// Returns the users concerning the search criteria
		/// </summary>
		/// <param name="pUserId">Userid typed in</param>
		/// <param name="pPassword">Password typed in</param>
		/// <returns>ArrayList of UserVO objects</returns>
		internal ArrayList getUserByLogin(String pUserId, String pPassword)
		{
			UserBuilder	userBuilder;
			ArrayList	users;

			userBuilder = UserManagementConfiguration.getInstance().getUserBuilder();
			users = userBuilder.getUserByLogin(pUserId, pPassword);

			return users;
		}

		/// <summary>
		/// Returns the users concerning the search criteria
		/// </summary>
		/// <param name="pUserSearchCriteria">Search criteria for filtering user result set</param>
		/// <returns>Hashtable of UserVO objects</returns>
		public Hashtable getUsers(IUser pUserSearchCriteria)
		{
			UserBuilder	userBuilder;
			Hashtable	users;

			userBuilder = UserManagementConfiguration.getInstance().getUserBuilder();
			users = userBuilder.getUsers(pUserSearchCriteria);

			return users;
		}

		/// <summary>
		/// Inserts a new user to the system
		/// </summary>
		/// <param name="pUser">The user to be inserted</param>
		public void insertUser(IUser pUser) 
		{
			UserBuilder	userBuilder;
			userBuilder = UserManagementConfiguration.getInstance().getUserBuilder();
			userBuilder.insertUser(pUser);
		}

		/// <summary>
		/// Updates an existing user
		/// </summary>
		/// <param name="pUser">The user to be updated</param>
		public void updateUser(IUser pUser) 
		{
			UserBuilder	userBuilder;
			userBuilder = UserManagementConfiguration.getInstance().getUserBuilder();
			userBuilder.updateUser(pUser);
		}

		/// <summary>
		/// Deletes an existing user
		/// </summary>
		/// <param name="pUser">The user to be deleted</param>
		public void deleteUser(IUser pUser) 
		{
			UserBuilder	userBuilder;
			userBuilder = UserManagementConfiguration.getInstance().getUserBuilder();
			userBuilder.deleteUser(pUser);
		}

		/// <summary>
		/// Returns an array of assignments between users and roles referring to search criteria
		/// </summary>
		/// <param name="pSearchCriteria">The search criteria for filtering result set</param>
		/// <returns>Array of RoleToUserVO</returns>
		public ArrayList getUserRoleAssignments(RoleToUserVO pSearchCriteria)
		{
			ArrayList			assignments;
			RoleToUserBuilder	roleToUserBuilder;

			roleToUserBuilder = UserManagementConfiguration.getInstance().getRoleToUserBuilder();
			assignments = roleToUserBuilder.getAssignments(pSearchCriteria);

			return assignments;
		}

		/// <summary>
		/// Inserts a new assignment between role and user to the system
		/// </summary>
		/// <param name="pAssignment">The assignment to be inserted</param>
		public void insertUserRoleAssignment(RoleToUserVO pAssignment)
		{
			RoleToUserBuilder	roleToUserBuilder;

			roleToUserBuilder = UserManagementConfiguration.getInstance().getRoleToUserBuilder();
			roleToUserBuilder.insertAssignment(pAssignment);
		}

		/// <summary>
		/// Deletes an existing assignment between user and role
		/// </summary>
		/// <param name="pAssignment">The assignment to be deleted</param>
		public void deleteUserRoleAssignment(RoleToUserVO pAssignment)
		{
			RoleToUserBuilder	roleToUserBuilder;

			roleToUserBuilder = UserManagementConfiguration.getInstance().getRoleToUserBuilder();
			roleToUserBuilder.deleteAssignment(pAssignment);
		}

		/// <summary>
		/// Returns an array of authorizations of roles to resources referring to search criteria
		/// </summary>
		/// <param name="pSearchCriteria">The search criteria for filtering result set</param>
		/// <returns>Array of AuthorizationVO</returns>
		public ArrayList getAuthorizations(AuthorizationVO pSearchCriteria)
		{
			ArrayList				authorizations;
			AuthorizationBuilder	authorizationBuilder;

			authorizationBuilder = UserManagementConfiguration.getInstance().getAuthorizationBuilder();
			authorizations = authorizationBuilder.getAuthorizations(pSearchCriteria);

			return authorizations;
		}

		/// <summary>
		/// Insertes a new authorization of a role to exactly one resource
		/// </summary>
		/// <param name="pAuthorization">The authorization to be inserted</param>
		public void insertAuthorization(AuthorizationVO pAuthorization)
		{
			AuthorizationBuilder	authorizationBuilder;

			authorizationBuilder = UserManagementConfiguration.getInstance().getAuthorizationBuilder();
			authorizationBuilder.insertAuthorization(pAuthorization);
		}

		/// <summary>
		/// Updates an existing authorization of a role to exactly one resource
		/// </summary>
		/// <param name="pAuthorization">The authorization to be updated</param>
		public void updateAuthorization(AuthorizationVO pAuthorization)
		{
			AuthorizationBuilder	authorizationBuilder;

			authorizationBuilder = UserManagementConfiguration.getInstance().getAuthorizationBuilder();
			authorizationBuilder.updateAuthorization(pAuthorization);
		}

		/// <summary>
		/// Deletes an existing authorization of a role to exactly one resource
		/// </summary>
		/// <param name="pAuthorization">The authorization to be deleted</param>
		public void deleteAuthorization(AuthorizationVO pAuthorization)
		{
			AuthorizationBuilder	authorizationBuilder;

			authorizationBuilder = UserManagementConfiguration.getInstance().getAuthorizationBuilder();
			authorizationBuilder.deleteAuthorization(pAuthorization);
		}
		
		/// <summary>
		/// Returns the roles for a specific user.
		/// </summary>
		/// <param name="pUser">The user the roles are to be read to.</param>
		/// <returns>Array of RoleVO</returns>
		public ArrayList getUserRoles(IUser pUser) 
		{
			RoleToUserVO	searchCriteriaAssignment;
			ArrayList		assignments;
			ArrayList		userRoles = null;
			Hashtable		roles;

			/* Prepare search criteria to get assignments
			 */
			searchCriteriaAssignment = new RoleToUserVO();
			searchCriteriaAssignment.UserId = pUser.Id;

			assignments = this.getUserRoleAssignments(searchCriteriaAssignment);
			roles = this.getRoles(null);


			if (assignments != null) 
			{
				userRoles = new ArrayList();

				foreach(Object currentObject in assignments) 
				{
					userRoles.Add(roles[((RoleToUserVO)currentObject).RoleId]);
				}
			}

			return userRoles;
		}

		/// <summary>
		/// Retrieves the roles for a specific user and process the assignment to 
		/// the given user object.
		/// </summary>
		/// <param name="pUser">The user object the role are to be assigned to.</param>
		internal void assignRolesToUser(User pUser) 
		{
			ArrayList	roles;
			Role		userRole;

			// get all roles
			roles = this.getUserRoles(pUser);

			if (roles != null) 
			{
				// Assign roles to user
				foreach(RoleVO currentRole in roles) 
				{
					userRole = new Role();
					userRole.Id = currentRole.Id;
					userRole.Name = currentRole.Name;
					userRole.Description = currentRole.Description;
					userRole.addAuthorizedEntity(pUser);
					pUser.addRole(userRole);
				}
			}
		}

		/// <summary>
		/// Retrieves the resources and the corresponding authorization and assign them to the given roles.
		/// </summary>
		/// <param name="pRoles">The roles the resources and the authorization are to be acquired.</param>
		internal void assignResourcesToRoles(ArrayList pRoles) 
		{
			Hashtable		resources;
			ArrayList		authorizations;
			AuthorizationVO	searchCriteria;
			de.pta.Component.N_UserManagement.Authorization.Authorization authorization;

			// get all resources
			resources = this.getResources();

			foreach(Role role in pRoles)
			{
				// get authorization to role
				searchCriteria = new AuthorizationVO();
				searchCriteria.RoleId = role.Id;
				authorizations = this.getAuthorizations(searchCriteria);
				/* step thru authorizations and get corresponding resource and assign 
				 * resulting authorization object to role
				 */
				foreach(AuthorizationVO currentAuthorization in authorizations) 
				{
					authorization = new de.pta.Component.N_UserManagement.Authorization.Authorization();
					authorization.ResourceAccess = currentAuthorization.ResourceAccess;
					authorization.Create = currentAuthorization.Create;
					authorization.Update = currentAuthorization.Update;
					authorization.Delete = currentAuthorization.Delete;
					authorization.Resource = (IResource)resources[currentAuthorization.ResourceId];
					
					// Hallo Katja, hier der Fix von Rene, bitte überprüfen und einbauen.
					if (authorization.Resource == null) 
					{
						throw new DataAccessException("ERROR_RESOURCE_NOT_FOUND");
					}
					role.addAuthorization(authorization);
				}
			}
		}

		#endregion // End of Methods
	}
}

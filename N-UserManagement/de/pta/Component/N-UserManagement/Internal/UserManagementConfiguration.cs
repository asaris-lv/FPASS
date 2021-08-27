using System;
using System.Collections;
using System.Windows.Forms;

using de.pta.Component.Common;
using de.pta.Component.DataAccess;
using de.pta.Component.N_UserManagement.Exceptions;
using de.pta.Component.N_UserManagement.DataAccess;

namespace de.pta.Component.N_UserManagement.Internal
{
	/// <summary>
	/// Parsing the XML configuration file for entries concerning user management.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> R.Weiﬂ, PTA GmbH
	/// <b>Date:</b> Aug/18/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	public class UserManagementConfiguration
	{
		#region Members

		/// <summary>
		/// Contains one single instance of UserManagementConfiguration.
		/// </summary>
		static private UserManagementConfiguration	mUserManagementConfiguration;

		/// <summary>
		/// The standard password for users before first login.
		/// </summary>
		private String mFirstLoginPassword;

		/// <summary>
		/// The type of authorized entities can be assigned to a eole
		/// </summary>
		private String mAuthorizedEntities;

		/// <summary>
		/// Indicator if roles can be assigned to users.
		/// </summary>
		private bool mRolesToUser;

		/// <summary>
		/// Indicator if roles can be assigned to user groups.
		/// </summary>
		private bool mRolesToUserGroup;

		/// <summary>
		/// Section UserManagement in config file.
		/// </summary>
		private const string XML_SECTION = "application/configuration/UserManagement";

		/// <summary>
		/// List of mappings from abstract classes to subclasses via type field from database
		/// </summary>
		private Hashtable mAbstractClassMappings;

		/// <summary>
		/// Indicator if resources are stored in DB
		/// </summary>
		private bool mResourcesInDB;

		/// <summary>
		/// Indicator if resources are stored in XML file
		/// </summary>
		private bool mResourcesInXML;

		/// <summary>
		/// Indicator if authorizations are stored in DB
		/// </summary>
		private bool mAuthorizationsInDB;

		/// <summary>
		/// Indicator if authorizations are stored in XML file
		/// </summary>
		private bool mAuthorizationsInXML;

		/// <summary>
		/// Indicator if roles are stored in DB
		/// </summary>
		private bool mRolesInDB;

		/// <summary>
		/// Indicator if roles are stored in XML file
		/// </summary>
		private bool mRolesInXML;

		/// <summary>
		/// Indicator if user-role assignments are stored in DB
		/// </summary>
		private bool mRoleToUserInDB;

		/// <summary>
		/// Indicator if user-role assignments are stored in XML file
		/// </summary>
		private bool mRoleToUserInXML;

		/// <summary>
		/// Indicator if users are stored in DB
		/// </summary>
		private bool mUsersInDB;

		/// <summary>
		/// Indicator if users are stored in XML file
		/// </summary>
		private bool mUsersInXML;

		#endregion //End of Members

		#region Constructors
	
		private UserManagementConfiguration()
		{
			initialize();
		}

		#endregion //End of Constructors

		#region Initialization

		private void initialize()
		{
			this.AuthorizedEntities = null;
			this.FirstLoginPassword = null;
			this.RolesToUser = false;
			this.RolesToUserGroup = false;
		}

		#endregion //End of Initialization

		#region Accessors 

		/// <summary>
		/// Returns the instance of the UserManagementConfiguration
		/// </summary>
		/// <returns>UserManagementConfiguration</returns>
		static public UserManagementConfiguration getInstance()
		{
			if (mUserManagementConfiguration == null)
			{
				mUserManagementConfiguration = new UserManagementConfiguration();
			}
			return mUserManagementConfiguration;
		}

		/// <summary>
		/// Accessor for first login password
		/// </summary>
		public String FirstLoginPassword
		{
			get
			{
				return mFirstLoginPassword;
			}
			set
			{
				mFirstLoginPassword = value;
			}
		}

		/// <summary>
		/// Accessor for indicator if roles can be assigned to users
		/// </summary>
		public bool RolesToUser
		{
			get
			{
				return mRolesToUser;
			}
			set
			{
				mRolesToUser = value;
			}
		}

		/// <summary>
		/// Accessor for indicator if roles can be assigned to user groups.
		/// </summary>
		public bool RolesToUserGroup
		{
			get
			{
				return mRolesToUserGroup;
			}
			set
			{
				mRolesToUserGroup = value;
			}
		}
		/// <summary>
		/// Accessor for type of authorized entities can be assigned to a role
		/// </summary>
		internal String AuthorizedEntities
		{
			get
			{
				return mAuthorizedEntities;
			}
			set
			{
				mAuthorizedEntities = value;
			}
		}

		/// <summary>
		/// Accessor for list of mappings from abstract classes to subclasses via type field from database
		/// </summary>
		internal Hashtable AbstractClassMappings
		{
			get
			{
				return mAbstractClassMappings;
			}
			set
			{
				mAbstractClassMappings = value;
			}
		}

		/// <summary>
		/// Accessor for indicator if resources are stored in DB
		/// </summary>
		internal bool ResourcesInDB
		{
			get
			{
				return mResourcesInDB;
			}
			set
			{
				mResourcesInDB = value;
			}
		}

		/// <summary>
		/// Accessor for indicator if resources are stored in XML file
		/// </summary>
		internal bool ResourcesInXML
		{
			get
			{
				return mResourcesInXML;
			}
			set
			{
				mResourcesInXML = value;
			}
		}

		/// <summary>
		/// Accessor for indicator if authorizations are stored in DB
		/// </summary>
		internal bool AuthorizationsInDB
		{
			get
			{
				return mAuthorizationsInDB;
			}
			set
			{
				mAuthorizationsInDB = value;
			}
		}

		/// <summary>
		/// Accessor for indicator if authorizations are stored in XML file
		/// </summary>
		internal bool AuthorizationsInXML
		{
			get
			{
				return mAuthorizationsInXML;
			}
			set
			{
				mAuthorizationsInXML = value;
			}
		}

		/// <summary>
		/// Accessor for indicator if roles are stored in DB
		/// </summary>
		internal bool RolesInDB
		{
			get
			{
				return mRolesInDB;
			}
			set
			{
				mRolesInDB = value;
			}
		}

		/// <summary>
		/// Accessor for indicator if roles are stored in XML file
		/// </summary>
		internal bool RolesInXML
		{
			get
			{
				return mRolesInXML;
			}
			set
			{
				mRolesInXML = value;
			}
		}

		/// <summary>
		/// Accessor for indicator if user-role assignments are stored in DB
		/// </summary>
		internal bool RoleToUserInDB
		{
			get
			{
				return mRoleToUserInDB;
			}
			set
			{
				mRoleToUserInDB = value;
			}
		}

		/// <summary>
		/// Accessor for indicator if user-role assignments are stored in XML file
		/// </summary>
		internal bool RoleToUserInXML
		{
			get
			{
				return mRoleToUserInXML;
			}
			set
			{
				mRoleToUserInXML = value;
			}
		}

		/// <summary>
		/// Accessor for indicator if users are stored in DB
		/// </summary>
		internal bool UsersInDB
		{
			get
			{
				return mUsersInDB;
			}
			set
			{
				mUsersInDB = value;
			}
		}

		/// <summary>
		/// Accessor for indicator if users are stored in XML file
		/// </summary>
		internal bool UsersInXML
		{
			get
			{
				return mUsersInXML;
			}
			set
			{
				mUsersInXML = value;
			}
		}

		#endregion //End of Accessors

		#region Methods

		/// <summary>
		/// Reads out the configuration information via ConfigProcessor.
		/// </summary>
		public void initializeUserManagementConfiguration()
		{
			XmlUserManagementConfigProcessor	configProcessor;
			ConfigReader						configReader;

			try 
			{
				configProcessor = new XmlUserManagementConfigProcessor();
				configReader = ConfigReader.GetInstance();
				configReader.ApplicationRootPath = Application.StartupPath;
				configReader.ReadConfig(XML_SECTION, configProcessor);
				DataAccessManager.GetInstance().ReadConfiguration();
			} 
			catch (CommonXmlException e)
			{
				String t = e.Message;
				throw new UserManagementConfigurationException("ERROR_INVALID_CONFIGURATION", e);
			}
		}

		/// <summary>
		/// Returns the resource builder in dependency of storage type
		/// </summary>
		/// <returns>
		/// <code>RdbResourceBuilder</code> if storage is in database
		/// <code>XMLResourceBuilder</code> if storage is in xml file
		/// </returns>
		internal ResourceBuilder getResourceBuilder() 
		{
			if (this.ResourcesInDB &&
				!this.ResourcesInXML) 
			{
				return new RdbResourceBuilder();
			} 
			else if (this.ResourcesInXML &&
				!this.ResourcesInDB) 
			{
				return new XMLResourceBuilder();
			} 
			else 
			{
				throw new UserManagementConfigurationException("ERROR_RESOURCE_STORAGE_TYPE");
			}
		}

		/// <summary>
		/// Returns the authorization builder in dependency of storage type
		/// </summary>
		/// <returns>
		/// <code>RdbAuthorizationBuilder</code> if storage is in database
		/// <code>XMLAuthorizationBuilder</code> if storage is in xml file
		/// </returns>
		internal AuthorizationBuilder getAuthorizationBuilder() 
		{
			if (this.AuthorizationsInDB &&
				!this.AuthorizationsInXML) 
			{
				return new RdbAuthorizationBuilder();
			} 
			else if (this.AuthorizationsInXML &&
				!this.AuthorizationsInDB) 
			{
				return new XMLAuthorizationBuilder();
			} 
			else 
			{
				throw new UserManagementConfigurationException("ERROR_AUTHORIZATION_STORAGE_TYPE");
			}
		}

		/// <summary>
		/// Returns the role builder in dependency of storage type
		/// </summary>
		/// <returns>
		/// <code>RdbRoleBuilder</code> if storage is in database
		/// <code>XMLRoleBuilder</code> if storage is in xml file
		/// </returns>
		internal RoleBuilder getRoleBuilder() 
		{
			if (this.RolesInDB &&
				!this.RolesInXML) 
			{
				return new RdbRoleBuilder();
			} 
			else if (this.RolesInXML &&
				!this.RolesInDB) 
			{
				return new XMLRoleBuilder();
			} 
			else 
			{
				throw new UserManagementConfigurationException("ERROR_ROLE_STORAGE_TYPE");
			}
		}
		
		/// <summary>
		/// Returns the role to user builder in dependency of storage type
		/// </summary>
		/// <returns>
		/// <code>RdbRoleToUserBuilder</code> if storage is in database
		/// <code>XMLRoleToUserBuilder</code> if storage is in xml file
		/// </returns>
		internal RoleToUserBuilder getRoleToUserBuilder() 
		{
			if (this.RoleToUserInDB &&
				!this.RoleToUserInXML) 
			{
				return new RdbRoleToUserBuilder();
			} 
			else if (this.RoleToUserInXML &&
				!this.RoleToUserInDB) 
			{
				return new XMLRoleToUserBuilder();
			} 
			else 
			{
				throw new UserManagementConfigurationException("ERROR_ROLETOUSER_STORAGE_TYPE");
			}
		}
		
		/// <summary>
		/// Returns the user builder in dependency of storage type
		/// </summary>
		/// <returns>
		/// <code>RdbUserBuilder</code> if storage is in database
		/// <code>XMLUserBuilder</code> if storage is in xml file
		/// </returns>
		internal UserBuilder getUserBuilder() 
		{
			if (this.UsersInDB &&
				!this.UsersInXML) 
			{
				return new RdbUserBuilder();
			} 
			else if (this.UsersInXML &&
				!this.UsersInDB) 
			{
				return new XMLUserBuilder();
			} 
			else 
			{
				throw new UserManagementConfigurationException("ERROR_USER_STORAGE_TYPE");
			}
		}
		
		#endregion // End of Methods


	}
}

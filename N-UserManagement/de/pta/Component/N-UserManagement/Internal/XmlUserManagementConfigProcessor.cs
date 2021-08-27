using System;
using System.Collections;

using de.pta.Component.Common;
using de.pta.Component.N_UserManagement.Exceptions;

namespace de.pta.Component.N_UserManagement.Internal
{
	/// <summary>
	/// Parsing the XML configuration file for entries concerning user management configuration.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> R.Weiﬂ, PTA GmbH
	/// <b>Date:</b> Aug/18/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>

	internal class XmlUserManagementConfigProcessor  :   IConfigProcessor{

		#region Members

		/// <summary>
		/// The outmost element of user management structure in configuration file
		/// </summary>
		private readonly String USERMANAGEMENT = "UserManagement";
		/// <summary>
		/// Defines the standard password of all users before first login.
		/// </summary>
		private readonly String FIRSTLOGINPASSWORD = "FirstLoginPassword";
		/// <summary>
		/// Defines wether role assigment is done only for users or user groups or both.
		/// </summary>
		private readonly String AUTHORIZEDENTITIES = "AuthorizedEntities";
		/// <summary>
		/// Authorized entity is of type user.
		/// </summary>
		private readonly String AUTHORIZEDENTITY_USER = "User";
		/// <summary>
		/// Authorized entity is of type user group.
		/// </summary>
		private readonly String AUTHORIZEDENTITY_USERGROUP = "UserGroup";
		/// <summary>
		/// Authorized entity is of type user group or user.
		/// </summary>
		private readonly String AUTHORIZEDENTITY_ALL = "All";

		/// <summary>
		/// Key for standard password
		/// </summary>
		private readonly String PASSWORD = "Password";
		/// <summary>
		/// Key for authorized entity
		/// </summary>
		private readonly String AUTHORIZEDENTITY = "AuthorizedEntity";

		/// <summary>
		/// The outmost element of mapping abstract classes
		/// </summary>
		private readonly String ABSTRACTCLASSMAPPING = "AbstractClassMapping";

		/// <summary>
		/// Name of abstract class
		/// </summary>
		private readonly String ABSTRACTCLASS = "AbstractClass";

		/// <summary>
		/// Fieldname in database to indicate type of subclass
		/// </summary>
		private readonly String TYPEFIELD = "TypeFieldInTable";

		/// <summary>
		/// Map entry for mapping database type id to subclass
		/// </summary>
		private readonly String MAP = "Map";

		/// <summary>
		/// Type Id
		/// </summary>
		private readonly String MAPID = "Id";

		/// <summary>
		/// Name of Subclass
		/// </summary>
		private readonly String MAPSUBCLASS = "SubClass";

		/// <summary>
		/// XML entry for indicate resource storage
		/// </summary>
		private readonly String RESOURCESTORAGE = "ResourceStorage";

		/// <summary>
		/// XML entry for indicate authorization storage
		/// </summary>
		private readonly String AUTHORIZATIONSTORAGE = "AuthorizationStorage";

		/// <summary>
		/// XML entry for indicate role storage
		/// </summary>
		private readonly String ROLESTORAGE = "RoleStorage";

		/// <summary>
		/// XML entry for indicate user-role assignment storage
		/// </summary>
		private readonly String USERTOROLESTORAGE = "UserToRoleStorage";

		/// <summary>
		/// XML entry for indicate user storage
		/// </summary>
		private readonly String USERSTORAGE = "UserStorage";

		/// <summary>
		/// Attribute for definition of storage type
		/// </summary>
		private readonly String STORAGETYPE = "type";

		/// <summary>
		/// Attribute value for stotage in XML file
		/// </summary>
		private readonly String STORAGETYPE_XML = "XML";

		/// <summary>
		/// Attribute value for stotage in database
		/// </summary>
		private readonly String STORAGETYPE_DB = "DB";

		/// <summary>
		/// Indicator for start user management config reading
		/// </summary>
		private bool	mStartConfigReading = false;

		/// <summary>
		/// Indicator for start abstract class mapping config reading
		/// </summary>
		private bool	mStartAbstractMappingReading = false;

		/// <summary>
		/// Type of authorized entities from XML file
		/// </summary>
		private String	mAuthorizedEntities;

		/// <summary>
		/// Standard password from XML file
		/// </summary>
		private	String	mFirstLoginPassword;

		/// <summary>
		/// Mapping of abstract class to subclass via type field in database
		/// </summary>
		private AbstractClassMap mAbstractClassMap;

		/// <summary>
		/// List of mappings from abstract classes to subclasses via type field from database
		/// </summary>
		private Hashtable mAbstractClassMappings;

		/// <summary>
		/// Type of resource storage in XML file
		/// </summary>
		private String mResourceStorage;

		/// <summary>
		/// Type of authorization storage in XML file
		/// </summary>
		private String mAuthorizationStorage;

		/// <summary>
		/// Type of role storage in XML file
		/// </summary>
		private String mRoleStorage;

		/// <summary>
		/// Type of user-role assignment storage in XML file
		/// </summary>
		private String mUserToRoleStorage;

		/// <summary>
		/// Type of user storage in XML file
		/// </summary>
		private String mUserStorage;


		#endregion //End of Members

		#region Constructors

		public XmlUserManagementConfigProcessor()
		{
			initialize();
		}

		#endregion //End of Constructors

		#region Initialization

		private void initialize()
		{
		}	

		#endregion //End of Initialization

		#region Methods

		/// <summary>
		/// Processes a ConfigNode of type 'block begin'.
		/// </summary>
		/// <param name="pConfigNode"></param>
		public void ProcessConfigBlockBegin(ConfigNode pConfigNode)
		{
			if (pConfigNode.NodeName.Equals(USERMANAGEMENT.ToUpper()))
			{
				mStartConfigReading = true;
			}
			if (pConfigNode.NodeName.Equals(this.ABSTRACTCLASSMAPPING.ToUpper()))
			{
				mAbstractClassMappings = new Hashtable();
				mStartAbstractMappingReading = true;
			}
			if (pConfigNode.NodeName.Equals(this.ABSTRACTCLASS.ToUpper()))
			{
				mAbstractClassMap = new AbstractClassMap();
				this.processAbstractClass(pConfigNode);
			}
		}

		/// <summary>
		/// Processes a ConfigNode of type 'item'.
		/// </summary>
		/// <param name="pConfigNode"></param>
		public void ProcessConfigItem(ConfigNode pConfigNode)
		{

			if (mStartConfigReading) 
			{
				// First login password
				if (pConfigNode.NodeName.Equals(FIRSTLOGINPASSWORD.ToUpper()))
				{
					this.processFirstLoginPassword(pConfigNode);
				} 
					// authorized entities
				else if (pConfigNode.NodeName.Equals(AUTHORIZEDENTITIES.ToUpper())) 
				{
					this.processAuthorizedEntities(pConfigNode);
				}
					// resource storage type
				else if (pConfigNode.NodeName.Equals(RESOURCESTORAGE.ToUpper())) 
				{
					this.processResourceStorageType(pConfigNode);
				}
					// authorization storage type
				else if (pConfigNode.NodeName.Equals(this.AUTHORIZATIONSTORAGE.ToUpper())) 
				{
					this.processAuthorizationStorageType(pConfigNode);
				}
					// role storage type
				else if (pConfigNode.NodeName.Equals(ROLESTORAGE.ToUpper())) 
				{
					this.processRoleStorageType(pConfigNode);
				}
					// user to role storage type
				else if (pConfigNode.NodeName.Equals(this.USERTOROLESTORAGE.ToUpper())) 
				{
					this.processUserToRoleStorageType(pConfigNode);
				}
					// user storage type
				else if (pConfigNode.NodeName.Equals(this.USERSTORAGE.ToUpper())) 
				{
					this.processUserStorageType(pConfigNode);
				}
				// abstract mapping
				if (mStartAbstractMappingReading) 
				{
					if (pConfigNode.NodeName.Equals(MAP.ToUpper())) 
					{
						this.processMap(pConfigNode);
					}
				}
			}
		}

		/// <summary>
		/// Processes a ConfigNode of type 'block end'.
		/// </summary>
		/// <param name="pConfigNode">The current node to process.</param>
		public void ProcessConfigBlockEnd(ConfigNode pConfigNode)
		{
			/* Transfer config entries to singleton UserManagementCOnfiguration
			 */
			if (pConfigNode.NodeName.Equals(USERMANAGEMENT.ToUpper()))
			{
				UserManagementConfiguration.getInstance().AuthorizedEntities = mAuthorizedEntities;
				UserManagementConfiguration.getInstance().FirstLoginPassword = mFirstLoginPassword;

				/* Set indicators in singleton 
				 */
				if (mAuthorizedEntities.ToUpper().Equals(AUTHORIZEDENTITY_ALL.ToUpper())) 
				{
					UserManagementConfiguration.getInstance().RolesToUserGroup = true;
					UserManagementConfiguration.getInstance().RolesToUser = true;
				} 
				else if (mAuthorizedEntities.ToUpper().Equals(AUTHORIZEDENTITY_USER.ToUpper())) 
				{
					UserManagementConfiguration.getInstance().RolesToUserGroup = false;
					UserManagementConfiguration.getInstance().RolesToUser = true;
				}
				else if (mAuthorizedEntities.ToUpper().Equals(AUTHORIZEDENTITY_USERGROUP.ToUpper())) 
				{
					UserManagementConfiguration.getInstance().RolesToUserGroup = true;
					UserManagementConfiguration.getInstance().RolesToUser = false;
				}

				/* transfer storage types to singleton
				 */
				/* resource storage type
				 */
				if (this.mResourceStorage.ToUpper().Equals(this.STORAGETYPE_DB.ToUpper())) 
				{
					UserManagementConfiguration.getInstance().ResourcesInDB = true;
					UserManagementConfiguration.getInstance().ResourcesInXML = false;
				}
				else if (this.mResourceStorage.ToUpper().Equals(this.STORAGETYPE_XML.ToUpper())) 
				{
					UserManagementConfiguration.getInstance().ResourcesInDB = false;
					UserManagementConfiguration.getInstance().ResourcesInXML = true;
				}
				/* authorization storage type
				 */
				if (this.mAuthorizationStorage.ToUpper().Equals(this.STORAGETYPE_DB.ToUpper())) 
				{
					UserManagementConfiguration.getInstance().AuthorizationsInDB = true;
					UserManagementConfiguration.getInstance().AuthorizationsInXML = false;
				}
				else if (this.mAuthorizationStorage.ToUpper().Equals(this.STORAGETYPE_XML.ToUpper())) 
				{
					UserManagementConfiguration.getInstance().AuthorizationsInDB = false;
					UserManagementConfiguration.getInstance().AuthorizationsInXML = true;
				}
				/* role storage type
				 */
				if (this.mRoleStorage.ToUpper().Equals(this.STORAGETYPE_DB.ToUpper())) 
				{
					UserManagementConfiguration.getInstance().RolesInDB = true;
					UserManagementConfiguration.getInstance().RolesInXML = false;
				}
				else if (this.mRoleStorage.ToUpper().Equals(this.STORAGETYPE_XML.ToUpper())) 
				{
					UserManagementConfiguration.getInstance().RolesInDB = false;
					UserManagementConfiguration.getInstance().RolesInXML = true;
				}
				/* user to role storage type
				 */
				if (this.mUserToRoleStorage.ToUpper().Equals(this.STORAGETYPE_DB.ToUpper())) 
				{
					UserManagementConfiguration.getInstance().RoleToUserInDB = true;
					UserManagementConfiguration.getInstance().RoleToUserInXML = false;
				}
				else if (this.mUserToRoleStorage.ToUpper().Equals(this.STORAGETYPE_XML.ToUpper())) 
				{
					UserManagementConfiguration.getInstance().RoleToUserInDB = false;
					UserManagementConfiguration.getInstance().RoleToUserInXML = true;
				}
				/* user storage type
				 */
				if (this.mUserStorage.ToUpper().Equals(this.STORAGETYPE_DB.ToUpper())) 
				{
					UserManagementConfiguration.getInstance().UsersInDB = true;
					UserManagementConfiguration.getInstance().UsersInXML = false;
				}
				else if (this.mUserStorage.ToUpper().Equals(this.STORAGETYPE_XML.ToUpper())) 
				{
					UserManagementConfiguration.getInstance().UsersInDB = false;
					UserManagementConfiguration.getInstance().UsersInXML = true;
				}

				mStartConfigReading = false;
			}
			if (pConfigNode.NodeName.Equals(this.ABSTRACTCLASSMAPPING.ToUpper()))
			{
				UserManagementConfiguration.getInstance().AbstractClassMappings = this.mAbstractClassMappings;
				mStartAbstractMappingReading = false;
			}
			if (pConfigNode.NodeName.Equals(this.ABSTRACTCLASS.ToUpper()))
			{
				mAbstractClassMappings.Add(mAbstractClassMap.AbstractClassName, mAbstractClassMap);
			}
		}

		/// <summary>
		/// Processes the node for first login password
		/// </summary>
		/// <param name="pConfigNode">The current node to process.</param>
		private void processFirstLoginPassword(ConfigNode pConfigNode) 
		{
			if (pConfigNode.NodeAttributes.ContainsKey(PASSWORD.ToUpper()))
			{
				mFirstLoginPassword = Convert.ToString(pConfigNode.NodeAttributes[PASSWORD.ToUpper()]);
			}
		}

		/// <summary>
		/// Processes the node for resource storage type
		/// </summary>
		/// <param name="pConfigNode">The current node to process.</param>
		private void processResourceStorageType(ConfigNode pConfigNode) 
		{
			if (pConfigNode.NodeAttributes.ContainsKey(STORAGETYPE.ToUpper()))
			{
				mResourceStorage = Convert.ToString(pConfigNode.NodeAttributes[STORAGETYPE.ToUpper()]);
			}
		}

		/// <summary>
		/// Processes the node for authorization storage type
		/// </summary>
		/// <param name="pConfigNode">The current node to process.</param>
		private void processAuthorizationStorageType(ConfigNode pConfigNode) 
		{
			if (pConfigNode.NodeAttributes.ContainsKey(STORAGETYPE.ToUpper()))
			{
				mAuthorizationStorage = Convert.ToString(pConfigNode.NodeAttributes[STORAGETYPE.ToUpper()]);
			}
		}

		/// <summary>
		/// Processes the node for role storage type
		/// </summary>
		/// <param name="pConfigNode">The current node to process.</param>
		private void processRoleStorageType(ConfigNode pConfigNode) 
		{
			if (pConfigNode.NodeAttributes.ContainsKey(STORAGETYPE.ToUpper()))
			{
				mRoleStorage = Convert.ToString(pConfigNode.NodeAttributes[STORAGETYPE.ToUpper()]);
			}
		}

		/// <summary>
		/// Processes the node for user-role assignment storage type
		/// </summary>
		/// <param name="pConfigNode">The current node to process.</param>
		private void processUserToRoleStorageType(ConfigNode pConfigNode) 
		{
			if (pConfigNode.NodeAttributes.ContainsKey(STORAGETYPE.ToUpper()))
			{
				mUserToRoleStorage = Convert.ToString(pConfigNode.NodeAttributes[STORAGETYPE.ToUpper()]);
			}
		}

		/// <summary>
		/// Processes the node for user storage type
		/// </summary>
		/// <param name="pConfigNode">The current node to process.</param>
		private void processUserStorageType(ConfigNode pConfigNode) 
		{
			if (pConfigNode.NodeAttributes.ContainsKey(STORAGETYPE.ToUpper()))
			{
				mUserStorage = Convert.ToString(pConfigNode.NodeAttributes[STORAGETYPE.ToUpper()]);
			}
		}

		/// <summary>
		/// Processes the node for authorized entities
		/// </summary>
		/// <param name="pConfigNode">The current node to process.</param>
		private void processAuthorizedEntities(ConfigNode pConfigNode) 
		{

			if (pConfigNode.NodeAttributes.ContainsKey(AUTHORIZEDENTITY.ToUpper()))
			{
				mAuthorizedEntities = Convert.ToString(pConfigNode.NodeAttributes[AUTHORIZEDENTITY.ToUpper()]);

				/* Check type of authorized entity for correct syntax
				 */
				if (!(mAuthorizedEntities.ToUpper().Equals(AUTHORIZEDENTITY_USER.ToUpper())) &&
					!(mAuthorizedEntities.ToUpper().Equals(AUTHORIZEDENTITY_USERGROUP.ToUpper())) &&
					!(mAuthorizedEntities.ToUpper().Equals(AUTHORIZEDENTITY_ALL.ToUpper())))
				{
					throw new UserManagementConfigurationException("ERROR_INVALID_CONFIG_FOR_AUTHORIZED_ENTITY");
				}
			}
		}

		/// <summary>
		/// Processes the node for abstract class mapping
		/// </summary>
		/// <param name="pConfigNode"></param>
		private void processAbstractClass(ConfigNode pConfigNode) 
		{
			if (pConfigNode.NodeAttributes.ContainsKey(ABSTRACTCLASS.ToUpper())) 
			{
				this.mAbstractClassMap.AbstractClassName = Convert.ToString(pConfigNode.NodeAttributes[ABSTRACTCLASS.ToUpper()]);
			}
			if (pConfigNode.NodeAttributes.ContainsKey(TYPEFIELD.ToUpper())) 
			{
				this.mAbstractClassMap.TypeFieldName = Convert.ToString(pConfigNode.NodeAttributes[TYPEFIELD.ToUpper()]);
			}
		}

		/// <summary>
		/// Processes the node for mapping
		/// </summary>
		/// <param name="pConfigNode"></param>
		private void processMap(ConfigNode pConfigNode) 
		{
			String	mapId = null;
			String	mapSubclass = null;

			if (pConfigNode.NodeAttributes.ContainsKey(MAPID.ToUpper())) 
			{
				mapId = Convert.ToString(pConfigNode.NodeAttributes[MAPID.ToUpper()]);
			}
			if (pConfigNode.NodeAttributes.ContainsKey(MAPSUBCLASS.ToUpper())) 
			{
				mapSubclass = Convert.ToString(pConfigNode.NodeAttributes[MAPSUBCLASS.ToUpper()]);
			}
			this.mAbstractClassMap.AbstractMap.Add(mapId, mapSubclass);
		}

		#endregion // End of Methods
	}

}

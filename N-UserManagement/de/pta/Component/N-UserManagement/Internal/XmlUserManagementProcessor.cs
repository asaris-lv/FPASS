using System;
using System.Collections;

using de.pta.Component.Common;
using de.pta.Component.N_UserManagement.Exceptions;
using de.pta.Component.N_UserManagement.Authorization;
using de.pta.Component.N_UserManagement.Resource;
using de.pta.Component.N_UserManagement.Vo;

namespace de.pta.Component.N_UserManagement.Internal
{
	/// <summary>
	/// Parsing the XML configuration file for entries concerning user management.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> R.Weiﬂ, PTA GmbH
	/// <b>Date:</b> Sep/12/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class XmlUserManagementProcessor : IConfigProcessor 
	{

		#region Members

		/// <summary>
		/// The outmost tag of resource definitions
		/// </summary>
		private readonly String RESOURCES = "Resources";
		/// <summary>
		/// The outmost tag of dialog definitions
		/// </summary>
		private readonly String DIALOGS = "Dialogs";
		/// <summary>
		/// The outmost tag of report definitions
		/// </summary>
		private readonly String REPORTS = "Reports";
		/// <summary>
		/// The outmost tag of batch definitions
		/// </summary>
		private readonly String BATCHES = "Batches";
		/// <summary>
		/// The outmost tag of control definitions
		/// </summary>
		private readonly String CONTROLS = "Controls";
		/// <summary>
		/// Dialog definition
		/// </summary>
		private readonly String DIALOG = "Dialog";
		/// <summary>
		/// report definition
		/// </summary>
		private readonly String REPORT = "Report";
		/// <summary>
		/// Batch definition
		/// </summary>
		private readonly String BATCH = "Batch";
		/// <summary>
		/// Control definition
		/// </summary>
		private readonly String CONTROL = "Control";
		/// <summary>
		/// Attribute Element Id
		/// </summary>
		private readonly String ID = "Id";
		/// <summary>
		/// Attribute Element name
		/// </summary>
		private readonly String NAME = "Name";
		/// <summary>
		/// Attribute Element description
		/// </summary>
		private String DESCRIPTION = "Description";
		/// <summary>
		/// Attribute element id of parent resources. Only valid for controls tat belong to
		/// reports or dialogs.
		/// </summary>
		private readonly String PARENTID = "ParentId";
		/// <summary>
		/// Attribute element id of function that is called by a control.
		/// </summary>
		private readonly String CALLEDFUNCTIONID = "CalledFunctionId";
		/// <summary>
		/// The outmost tag of role definitons.
		/// </summary>
		private readonly String ROLES = "Roles";
		/// <summary>
		/// The tag of one role definiton. Contains the attributes id, name and
		/// description.
		/// </summary>
		private readonly String ROLE = "Role";
		/// <summary>
		/// The outmost tag of authorization definitons.
		/// </summary>
		private readonly String AUTHORIZATIONS = "Authorizations";
		/// <summary>
		/// The tag of one authorization definiton. Contains the attributes role id, , resource id, 
		/// and access rights of role to resource
		/// </summary>
		private readonly String AUTHORIZATION = "Authorization";
		/// <summary>
		/// Id of the resource assinged to an authorization
		/// </summary>
		private readonly String RESOURCEID = "ResourceID";
		/// <summary>
		/// Id of the role assiged to an authorization
		/// </summary>
		private readonly String ROLEID = "RoleID";
		/// <summary>
		/// Access right to a resource
		/// </summary>
		private readonly String ACCESS = "Access";
		/// <summary>
		/// Create right within a dialog
		/// </summary>
		private readonly String CREATE = "Create";
		/// <summary>
		/// Update right within a dialog
		/// </summary>
		private readonly String UPDATE = "Update";
		/// <summary>
		/// Delete right within a dialog
		/// </summary>
		private readonly String DELETE = "Delete";
		/// <summary>
		/// positive right
		/// </summary>
		private readonly String RIGHT_YES = "Yes";
		/// <summary>
		/// negative right
		/// </summary>
		private readonly String RIGHT_NO = "No";
		/// <summary>
		/// Contains the resources from xml file
		/// </summary>
		private Hashtable mResources;
		/// <summary>
		/// Contains the roles xml file
		/// </summary>
		private Hashtable mRoles;
		/// <summary>
		/// Contains the authorizations of roles to resources
		/// </summary>
		private ArrayList mAuthorizations;
		/// <summary>
		/// Flag for start reading resources
		/// </summary>
		private bool mStartResourceReading = false;
		/// <summary>
		/// Flag for start reading roles
		/// </summary>
		private bool mStartRoleReading = false;
		/// <summary>
		/// Flag for start reading authorizations
		/// </summary>
		private bool mStartAuthorizationReading = false;

		#endregion //End of Members

		#region Constructors

		public XmlUserManagementProcessor()
		{
			this.initialize();
		}

		#endregion //End of Constructors

		#region Initialization

		private void initialize()
		{
		}

		#endregion //End of Initialization

		#region Accessors

		/// <summary>
		/// Accessor authorizations of roles to resources
		/// </summary>
		public ArrayList Authorizations
		{
			get
			{
				return mAuthorizations;
			}
			set
			{
				mAuthorizations = value;
			}
		}

		/// <summary>
		/// Accessor for roles from xml file
		/// </summary>
		public Hashtable Roles
		{
			get
			{
				return mRoles;
			}
			set
			{
				mRoles = value;
			}
		}

		/// <summary>
		/// Accessor for resources from xml file
		/// </summary>
		public Hashtable Resources
		{
			get
			{
				return mResources;
			}
			set
			{
				mResources = value;
			}
		}

		#endregion //End of Accessors

		#region Methods

		/// <summary>
		/// Processes a ConfigNode of type 'block begin'.
		/// </summary>
		/// <param name="pConfigNode">The current node to process</param>
		public void ProcessConfigBlockBegin(ConfigNode pConfigNode)
		{
			// start reading of resources
			if (pConfigNode.NodeName.Equals(this.RESOURCES.ToUpper())) 
			{
				mStartResourceReading = true;
				mResources = new Hashtable();
			} 
				// start reading of roles 
			else if (pConfigNode.NodeName.Equals(this.ROLES.ToUpper())) 
			{
				mStartRoleReading = true;
				mRoles = new Hashtable();
			} 
				// start reading of authorizations
			else if (pConfigNode.NodeName.Equals(this.AUTHORIZATIONS.ToUpper())) 
			{
				mStartAuthorizationReading = true;
				mAuthorizations = new ArrayList();
			}
		}

		/// <summary>
		/// Processes a ConfigNode of type 'item'.
		/// </summary>
		/// <param name="pConfigNode">The current node to process</param>
		public void ProcessConfigItem(ConfigNode pConfigNode){
			Dialog	currentDialog;
			Report	currentReport;
			Batch	currentBatch;
			Control	currentControl;

			if (mStartResourceReading) 
			{
				// read dialog properties
				if (pConfigNode.NodeName.Equals(this.DIALOG.ToUpper())) 
				{
					currentDialog = new Dialog();
					this.processResource(pConfigNode, currentDialog);
					this.mResources.Add(currentDialog.Id, currentDialog);
				} 
					// read report properties
				else if (pConfigNode.NodeName.Equals(this.REPORT.ToUpper())) 
				{
					currentReport = new Report();
					this.processResource(pConfigNode, currentReport);
					this.mResources.Add(currentReport.Id, currentReport);
				}
					// read batch properties
				else if (pConfigNode.NodeName.Equals(this.BATCH.ToUpper())) 
				{
					currentBatch = new Batch();
					this.processResource(pConfigNode, currentBatch);
					this.mResources.Add(currentBatch.Id, currentBatch);
				}
					// read control properties
				else if (pConfigNode.NodeName.Equals(this.CONTROL.ToUpper())) 
				{
					currentControl = new Control();
					this.processResource(pConfigNode, currentControl);
					this.mResources.Add(currentControl.Id, currentControl);
				}
			} 
				// read role properties
			else if (mStartRoleReading) 
			{
				if (pConfigNode.NodeName.Equals(this.ROLE.ToUpper())) 
				{
					this.processRole(pConfigNode);
				}
			}
				// read authorization properties
			else if (this.mStartAuthorizationReading)
			{
				if (pConfigNode.NodeName.Equals(this.AUTHORIZATION.ToUpper())) 
				{
					this.processAuthorization(pConfigNode);
				}
			}
		}

		/// <summary>
		/// Processes a ConfigNode of type 'block end'.
		/// </summary>
		/// <param name="pConfigNode">The current node to process</param>
		public void ProcessConfigBlockEnd(ConfigNode pConfigNode){

			// end reading resources
			if (pConfigNode.NodeName.Equals(this.RESOURCES.ToUpper())) 
			{
				mStartResourceReading = false;
			} 
			// end reading roles
			else if (pConfigNode.NodeName.Equals(this.ROLES.ToUpper())) 
			{
				mStartRoleReading = false;
			}
				// end reading authorizations
			else if (pConfigNode.NodeName.Equals(this.AUTHORIZATIONS.ToUpper())) 
			{
				mStartAuthorizationReading = false;
			}
		}

		/// <summary>
		/// Processes the tags dialog, report, batch and control.
		/// </summary>
		/// <param name="pConfigNode">The current node to process</param>
		private void processResource(ConfigNode pConfigNode, de.pta.Component.N_UserManagement.Resource.Resource pResource){
			Control		currentControl;
			int			parentID;
			int			calledFunctionID;
			Dialog		parentDialog;
			Report		parentReport;
			Function	calledFunction;

			// transfer common resource attributes to resource object
			if (pConfigNode.NodeAttributes.ContainsKey(this.ID.ToUpper())) 
			{
				pResource.Id = Convert.ToInt32(pConfigNode.NodeAttributes[this.ID.ToUpper()]);
			}
			if (pConfigNode.NodeAttributes.ContainsKey(this.NAME.ToUpper())) 
			{
				pResource.Name = Convert.ToString(pConfigNode.NodeAttributes[this.NAME.ToUpper()]);
			}
			if (pConfigNode.NodeAttributes.ContainsKey(this.DESCRIPTION.ToUpper())) 
			{
				pResource.Description = Convert.ToString(pConfigNode.NodeAttributes[this.DESCRIPTION.ToUpper()]);
			}

			/* check if resource is control for getting parent resource (dialog or report) 
			 * and possibly assigned function call
			 */
			if (pResource is Control) 
			{
				currentControl = (Control)pResource;
				// get id of parent resource
				if (pConfigNode.NodeAttributes.ContainsKey(this.PARENTID.ToUpper())) 
				{
					parentID = Convert.ToInt32(pConfigNode.NodeAttributes[this.PARENTID.ToUpper()]);

					if (this.mResources[parentID] != null) 
					{
						// assign control to dialog
						if (this.mResources[parentID] is Dialog) 
						{
							parentDialog = (Dialog)this.mResources[parentID];
							parentDialog.addControl(currentControl);
							currentControl.Parent = parentDialog;
						} 
						// assign control to report
						else if (this.mResources[parentID] is Report) 
						{
							parentReport = (Report)this.mResources[parentID];
							parentReport.addControl(currentControl);
							currentControl.Parent = parentReport;
						}
						else
						{
							// Invalid resource type for parent. Parent is neither Dialog nor Report
							throw new XmlDataAccessException("ERROR_INVALID_PARENT_TYPE");
						}
					} 
					else 
					{
						// Parent to be assigned to control does not exist
						throw new XmlDataAccessException("ERROR_PARENT_DOES_NOT_EXIST");
					}
				}
				// Get id of function to be called by control. This is optional.
				if (pConfigNode.NodeAttributes.ContainsKey(this.CALLEDFUNCTIONID.ToUpper())) 
				{
					// get id id from xml file
					calledFunctionID = Convert.ToInt32(pConfigNode.NodeAttributes[this.CALLEDFUNCTIONID.ToUpper()]);
					if (calledFunctionID != 0) 
					{
						// if function exists assign it to control
						if (this.mResources[calledFunctionID] != null) 
						{
							calledFunction = (Function)this.mResources[calledFunctionID];
							currentControl.CalledFunction = calledFunction;
						} 
						else 
						{
							// Function to be assigned to control does not exist
							throw new XmlDataAccessException("ERROR_FUNCTION_DOES_NOT_EXIST");
						}
					}
				}
			}
		}

		/// <summary>
		/// Processes the tag role
		/// </summary>
		/// <param name="pConfigNode">The current node to process</param>
		private void processRole(ConfigNode pConfigNode){
			RoleVO role;

			role = new RoleVO();

			// Transfer role attribute to role object
			if (pConfigNode.NodeAttributes.ContainsKey(this.ID.ToUpper())) 
			{
				role.Id = Convert.ToInt32(pConfigNode.NodeAttributes[this.ID.ToUpper()]);
			}
			if (pConfigNode.NodeAttributes.ContainsKey(this.NAME.ToUpper())) 
			{
				role.Name = Convert.ToString(pConfigNode.NodeAttributes[this.NAME.ToUpper()]);
			}
			if (pConfigNode.NodeAttributes.ContainsKey(this.DESCRIPTION.ToUpper())) 
			{
				role.Description = Convert.ToString(pConfigNode.NodeAttributes[this.DESCRIPTION.ToUpper()]);
			}

			this.mRoles.Add(role.Id, role);
		}

		/// <summary>
		/// Processes the tag Authorization
		/// </summary>
		/// <param name="pConfigNode">The current node to process</param>
		private void processAuthorization(ConfigNode pConfigNode){
			String			currentRight;
			AuthorizationVO	authorization;

			/* Create new authorization object and assign it to current role remembered before while
			 * reading out role properties
			 */
			authorization = new AuthorizationVO();
			mAuthorizations.Add(authorization);

			/* extract role id from authorization tag
			 */
			if (pConfigNode.NodeAttributes.ContainsKey(this.ROLEID.ToUpper())) 
			{
				authorization.RoleId = Convert.ToInt32(pConfigNode.NodeAttributes[this.ROLEID.ToUpper()]);
			} 
			else 
			{
				throw new XmlDataAccessException("ERROR_MISSING_ATTRIBUTE_ROLE_ID");
			}
			/* extract resource id from authorization tag
			 */
			if (pConfigNode.NodeAttributes.ContainsKey(this.RESOURCEID.ToUpper())) 
			{
				authorization.ResourceId = Convert.ToInt32(pConfigNode.NodeAttributes[this.RESOURCEID.ToUpper()]);
			}
			else 
			{
				throw new XmlDataAccessException("ERROR_MISSING_ATTRIBUTE_RESOURCE_ID");
			}
			/* Get access rights from xml file and tranfer them to authorization object bound to role
			 * The rights for create, update and delete are only valid for dialogs
			 */
			if (pConfigNode.NodeAttributes.ContainsKey(this.ACCESS.ToUpper())) 
			{
				currentRight = Convert.ToString(pConfigNode.NodeAttributes[this.ACCESS.ToUpper()]);
				authorization.ResourceAccess = (currentRight.ToUpper().Equals(this.RIGHT_YES.ToUpper())) ? true : false;
			}
			else
			{
				throw new XmlDataAccessException("ERROR_MISSING_ATTRIBUTE_RESOURCE_ACCESS");
			}
			if (pConfigNode.NodeAttributes.ContainsKey(this.CREATE.ToUpper())) 
			{
				currentRight = Convert.ToString(pConfigNode.NodeAttributes[this.CREATE.ToUpper()]);
				authorization.Create = (currentRight.ToUpper().Equals(this.RIGHT_YES.ToUpper())) ? true : false;
			}
			if (pConfigNode.NodeAttributes.ContainsKey(this.UPDATE.ToUpper())) 
			{
				currentRight = Convert.ToString(pConfigNode.NodeAttributes[this.UPDATE.ToUpper()]);
				authorization.Update = (currentRight.ToUpper().Equals(this.RIGHT_YES.ToUpper())) ? true : false;
			}
			if (pConfigNode.NodeAttributes.ContainsKey(this.DELETE.ToUpper())) 
			{
				currentRight = Convert.ToString(pConfigNode.NodeAttributes[this.DELETE.ToUpper()]);
				authorization.Delete = (currentRight.ToUpper().Equals(this.RIGHT_YES.ToUpper())) ? true : false;
			}

		}

		#endregion // End of Methods

	}//end XmlUserManagementProcessor
}//end namespace Internal
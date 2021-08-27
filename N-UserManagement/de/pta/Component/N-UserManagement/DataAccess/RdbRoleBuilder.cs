using System;
using System.Collections;
using System.Data;

using de.pta.Component.N_UserManagement.Exceptions;
using de.pta.Component.N_UserManagement.Authorization;
using de.pta.Component.N_UserManagement.Vo;
using de.pta.Component.DataAccess;

namespace de.pta.Component.N_UserManagement.DataAccess
{
	/// <summary>
	/// class for getting Roles from database
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> R.Weiﬂ, PTA GmbH
	/// <b>Date:</b> Sep/12/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class RdbRoleBuilder : RoleBuilder 
	{

		#region Members

		/// <summary>
		/// Role query name
		/// </summary>
		private readonly String QUERYNAME_ROLES = "Roles";

		/// <summary>
		/// Shortname of role table
		/// </summary>
		private readonly String TABLESHORTNAME_ROLE = "Ro_";

		#endregion // End of Members

		#region Constructors

		public RdbRoleBuilder()
		{
			this.initialize();
		}

		#endregion // End of Constructors

		#region Initialize

		private void initialize()
		{
		}

		#endregion // End of initialize

		#region Methods

		/// <summary>
		/// Returns the list of roles referring to search criteria
		/// Changed 17.10.03: Benz due to project requirements
		/// Added mandator Paramter for the select
		/// </summary>
		/// <param name="pRoleSearchCriteria">Search Criteria</param>
		/// <returns>Array of RoleVO. Key is internal role id.</returns>
		public override Hashtable getRoles(IRole pRoleSearchCriteria)
		{
			DataSet		ds;
			String		qryResultId;
			ObjectMap	objectMap;
			ArrayList	objectMaps;
			ArrayList	rowsOfObjects;
			Hashtable	roles;
			RoleVO		currentRole;

			/* create map to transfrom dataset into objects
			 */
			objectMaps = new ArrayList();

			objectMap = new ObjectMap();
			objectMap.Type = typeof(RoleVO);
			objectMap.TableShortName = this.TABLESHORTNAME_ROLE;
			objectMaps.Add(objectMap);

			qryResultId = DataAccessManager.GetInstance().PrepareQueryResult("FPASS", "UM", QUERYNAME_ROLES);
			
			// Set the unique mandatorID due to project requirements for fpass, degussa
			DataAccessManager.GetInstance().SetParameter(qryResultId, "MandatorID", LoginManager.getInstance().MandatorID.ToString());
			
			if (pRoleSearchCriteria != null)
			{
				if (pRoleSearchCriteria.Id != 0) 
				{
					DataAccessManager.GetInstance().SetParameter(qryResultId, "Id", pRoleSearchCriteria.Id.ToString());
				}
				if (pRoleSearchCriteria.Name != null &&
					pRoleSearchCriteria.Name != "")
				{
					DataAccessManager.GetInstance().SetParameter(qryResultId, "Name", pRoleSearchCriteria.Name);
				}
				if (pRoleSearchCriteria.Description != null &&
					pRoleSearchCriteria.Description != "")
				{
					DataAccessManager.GetInstance().SetParameter(qryResultId, "Description", pRoleSearchCriteria.Description);
				}
			}
			DataAccessManager.GetInstance().ExecuteQuery(qryResultId);
			ds = DataAccessManager.GetInstance().GetDataSetFirstBlock(qryResultId);
			rowsOfObjects = RelationalObjectMapper.getInstance().mapDataSetToArrayList(objectMaps, ds);

			roles = new Hashtable();


			// Error 15.10.03: invalid cast (RoleVO)
			// fixed 15.10.03 added an second loop to iterate through the arraylist first
			foreach(ArrayList currentRow in rowsOfObjects) 
			{
				foreach(Object currentObject in currentRow) 
				{
					currentRole = (RoleVO)currentObject;
					roles.Add(currentRole.Id, currentRole);
				}
			}

			return roles;
		}

		/// <summary>
		/// Inserts a new role into the system.
		/// </summary>
		/// <param name="pRole">The role to be inserted</param>
		public override void insertRole(IRole pRole) 
		{
		}

		/// <summary>
		/// Updates an existing role into the system.
		/// </summary>
		/// <param name="pRole">The role to be updated</param>
		public override void updateRole(IRole pRole) 
		{
		}

		/// <summary>
		/// Deletes an existing role into the system.
		/// </summary>
		/// <param name="pRole">The role to be deleted</param>
		public override void deleteRole(IRole pRole) 
		{
		}

		#endregion // End of Methods

	}//end RdbRoleBuilder
}//end namespace Internal
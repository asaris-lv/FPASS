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
	/// class for getting user-Role assignments from database
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> R.Weiﬂ, PTA GmbH
	/// <b>Date:</b> Sep/29/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class RdbRoleToUserBuilder : RoleToUserBuilder 
	{

		#region Members

		/// <summary>
		/// Role query name
		/// </summary>
		private String QUERYNAME_ROLEASSIGNMENTS = "RoleAssignments";

		/// <summary>
		/// Shortname of table containing assignments of roles to auhtorized entities
		/// </summary>
		private String TABLESHORTNAME_ROLELINK = "Rl_";

		#endregion // End of Members

		#region Constructors

		public RdbRoleToUserBuilder()
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
		/// Returns the list of assignments between roles and users referring to search criteria
		/// </summary>
		/// <param name="pRoleSearchCriteria">Search Criteria</param>
		/// <returns>Array of RoleToUserVO.</returns>
		public override ArrayList getAssignments(RoleToUserVO pSearchCriteria)
		{
			DataSet			ds;
			ObjectMap		objectMap;
			ArrayList		objectMaps;
			ArrayList		rowsOfObjects;
			String			qryResultId;
			ArrayList		assignments;
			RoleToUserVO	currentAssignment;

			/* Create mapping for transform dataset into objects
			 */
			objectMaps = new ArrayList();

			objectMap = new ObjectMap();
			objectMap.Type = typeof(RoleToUserVO);
			objectMap.TableShortName = this.TABLESHORTNAME_ROLELINK;
			objectMaps.Add(objectMap);

			qryResultId = DataAccessManager.GetInstance().PrepareQueryResult("FPASS", "UM", QUERYNAME_ROLEASSIGNMENTS);
			/* Set search criteria
			 */
			if (pSearchCriteria.RoleId != 0) 
			{
				DataAccessManager.GetInstance().SetParameter(qryResultId, "RoleId", pSearchCriteria.RoleId.ToString());
			} 
			else 
			{
				// Set default search criteria for role id
				DataAccessManager.GetInstance().SetParameter(qryResultId, "RoleId", "0", ">=");
			}
			if (pSearchCriteria.UserId != 0) 
			{
				DataAccessManager.GetInstance().SetParameter(qryResultId, "AuthorizedEntityId", pSearchCriteria.UserId.ToString());
			} 
			else 
			{
				// Set default search criteria for role id
				DataAccessManager.GetInstance().SetParameter(qryResultId, "AuthorizedEntityId", "0", ">=");
			}
			DataAccessManager.GetInstance().ExecuteQuery(qryResultId);
			ds = DataAccessManager.GetInstance().GetDataSetFirstBlock(qryResultId);
			rowsOfObjects = RelationalObjectMapper.getInstance().mapDataSetToArrayList(objectMaps, ds);

			assignments = new ArrayList();
	
			foreach(ArrayList currentRow in rowsOfObjects) 
			{
				foreach(Object currentObject in currentRow) 
				{
					currentAssignment = (RoleToUserVO)currentObject;
					assignments.Add(currentAssignment);
				}
			}
			return assignments;
		}

		/// <summary>
		/// Inserts a new assignment into the system.
		/// </summary>
		/// <param name="pRoleToUser">The assignment to be inserted</param>
		public override void insertAssignment(RoleToUserVO pRoleToUser)
		{
		}

		/// <summary>
		/// Deletes an exsiting assignment into the system.
		/// </summary>
		/// <param name="pRoleToUser">The assignment to be deleted</param>
		public override void deleteAssignment(RoleToUserVO pRoleToUser)
		{
		}

		#endregion // End of Methods

	}//end RdbRoleToUserBuilder
}//end namespace DataAccess
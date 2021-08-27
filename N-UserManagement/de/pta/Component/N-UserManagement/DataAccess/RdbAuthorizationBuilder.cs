using System;
using System.Collections;
using System.Data;

using de.pta.Component.N_UserManagement.Exceptions;
using de.pta.Component.N_UserManagement.Authorization;
using de.pta.Component.N_UserManagement.Resource;
using de.pta.Component.N_UserManagement.Vo;
using de.pta.Component.DataAccess;

namespace de.pta.Component.N_UserManagement.DataAccess
{
	/// <summary>
	/// class for getting authorizations from database
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> R.Weiﬂ, PTA GmbH
	/// <b>Date:</b> Sep/26/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class RdbAuthorizationBuilder : AuthorizationBuilder 
	{

		#region Members

		/// <summary>
		/// Authorizaton query name
		/// </summary>
		private readonly String QUERYNAME_AUTHORIZATIONS = "Authorizations";

		/// <summary>
		/// Shortname of authorization table in database
		/// </summary>
		private readonly String TABLESHORTNAME_AUTHORIZATION = "Au_";

		#endregion // End of Members

		#region Constructors

		public RdbAuthorizationBuilder()
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
		/// Returns the list of authorizations referring to search criteria from database
		/// </summary>
		/// <param name="pAuthorizationSearchCriteria">Search Criteria</param>
		/// <returns>Array of AuthorizationVO</returns>
		public override ArrayList getAuthorizations(AuthorizationVO pAuthorizationSearchCriteria)
		{
			DataSet				ds;
			String				qryResultId;
			ObjectMap			objectMap;
			ArrayList			objectMaps;
			ArrayList			authorizations;
			ArrayList			authorizationVOs=new ArrayList();	//28.10.2003
			AuthorizationVO		currentAuthorizationVO;			//28.10.2003

			/* create map to transfrom dataset into objects
			 */
			objectMaps = new ArrayList();

			objectMap = new ObjectMap();
			objectMap.Type = typeof(AuthorizationVO);
			objectMap.TableShortName = this.TABLESHORTNAME_AUTHORIZATION;
			objectMaps.Add(objectMap);

			qryResultId = DataAccessManager.GetInstance().PrepareQueryResult("FPASS", "UM", QUERYNAME_AUTHORIZATIONS);
			if (pAuthorizationSearchCriteria != null)
			{
				if (pAuthorizationSearchCriteria.RoleId != 0) 
				{
					DataAccessManager.GetInstance().SetParameter(qryResultId, "RoleId", pAuthorizationSearchCriteria.RoleId.ToString());
				}
				if (pAuthorizationSearchCriteria.ResourceId != 0) 
				{
					DataAccessManager.GetInstance().SetParameter(qryResultId, "ResourceId", pAuthorizationSearchCriteria.ResourceId.ToString());
				}
			}
			DataAccessManager.GetInstance().ExecuteQuery(qryResultId);
			ds = DataAccessManager.GetInstance().GetDataSetFirstBlock(qryResultId);
			authorizations = RelationalObjectMapper.getInstance().mapDataSetToArrayList(objectMaps, ds);

			// error 17.10.03: invalid cast in Administration Manager
			//return authorizations;
			
			//HOT FIX: return not the full arraylist first , just the first element ( which is an arraylist itsself )
			//return (ArrayList)authorizations[0];

			// REAL FIX 28.10.2003: Build a new ArrayList consisting of AuthorizationVOs.
			foreach(ArrayList currentRow in authorizations) 
			{
				foreach(Object currentObject in currentRow) 
				{
					currentAuthorizationVO = (AuthorizationVO)currentObject;
					authorizationVOs.Add(currentAuthorizationVO);
				}
			}
			return authorizationVOs;
			
		}

		/// <summary>
		/// Inserts a new authorization into the system
		/// </summary>
		/// <param name="pAuthorization">The authorization to be inserted</param>
		public override void insertAuthorization(IAuthorization pAuthorization)
		{

		}

		/// <summary>
		/// Updates an existing authorization into the system.
		/// </summary>
		/// <param name="pAuthorization">The authorization to be updated</param>
		public override void updateAuthorization(IAuthorization pAuthorization)
		{

		}

		/// <summary>
		/// Deletes an existing authorization into the system.
		/// </summary>
		/// <param name="pAuthorization">The authorization to be deleted</param>
		public override void deleteAuthorization(IAuthorization pAuthorization)
		{

		}

		#endregion // End of Methods

	}//end RdbAuthorizationBuilder
}//end namespace DataAccess
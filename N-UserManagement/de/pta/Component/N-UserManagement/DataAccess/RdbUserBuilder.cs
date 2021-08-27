using System;
using System.Collections;
using System.Data;

using de.pta.Component.N_UserManagement.Authorization;
using de.pta.Component.N_UserManagement.Vo;
using de.pta.Component.DataAccess;

namespace de.pta.Component.N_UserManagement.DataAccess 
{
	/// <summary>
	/// class for getting user informations
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> R.Weiﬂ, PTA GmbH
	/// <b>Date:</b> Sep/18/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>

	internal class RdbUserBuilder : UserBuilder 
	{

		#region Members

		/// <summary>
		/// User query name
		/// </summary>
		private readonly String QUERYNAME_USERS = "Users";
		/// <summary>
		/// Shortname of user table
		/// </summary>
		private readonly String TABLESHORTNAME_USER = "Us_";

		#endregion // End of Members

		#region Constructors

		public RdbUserBuilder()
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
		/// Returns the users that can be identified by user id and password
		/// </summary>
		/// <param name="pUserId">Userid typed in</param>
		/// <param name="pPassword">Password typed in</param>
		/// <returns>Array of UserVO</returns>
		internal override ArrayList getUserByLogin(String pUserId, String pPassword) 
		{
			DataSet		ds;
			ObjectMap	objectMap;
			ArrayList	objectMaps;
			ArrayList	rowsOfObjects;
			String		qryResultId;
			ArrayList	users;
			UserVO		currentUser;

			/* Create mapping for transform dataset into objects
			 */
			objectMaps = new ArrayList();

			objectMap = new ObjectMap();
			objectMap.Type = typeof(UserVO);
			objectMap.TableShortName = this.TABLESHORTNAME_USER;
			objectMaps.Add(objectMap);

			qryResultId = DataAccessManager.GetInstance().PrepareQueryResult("FPASS", "UM", QUERYNAME_USERS);
			// Set default search criteria for id
			/* stefan benz 05.11.03 
			 * due to project requirements for fpass degussa
			 * userid was not unique, because fpass had to work with different mandators
			 * we used the technical pk to verify user-login
			 * */
			DataAccessManager.GetInstance().SetParameter(qryResultId, "Id", pUserId, "=");
	//		DataAccessManager.GetInstance().SetParameter(qryResultId, "UserId", pUserId, "=");
			DataAccessManager.GetInstance().SetParameter(qryResultId, "Password", pPassword, "=");
			DataAccessManager.GetInstance().ExecuteQuery(qryResultId);
			ds = DataAccessManager.GetInstance().GetDataSetFirstBlock(qryResultId);
			rowsOfObjects = RelationalObjectMapper.getInstance().mapDataSetToArrayList(objectMaps, ds);

			users = new ArrayList();
	
			foreach(ArrayList currentRow in rowsOfObjects) 
			{
				foreach(Object currentObject in currentRow) 
				{
					currentUser = (UserVO)currentObject;
					users.Add(currentUser);
				}
			}
			return users;
		}

		/// <summary>
		/// Returns the list of users referring to search criteria
		/// </summary>
		/// <param name="pUserSearchCriteria">Search Criteria</param>
		/// <returns>Array of UserVO. Key is internal user id.</returns>
		public override Hashtable getUsers(IUser pUserSearchCriteria)
		{
			DataSet		ds;
			ObjectMap	objectMap;
			ArrayList	objectMaps;
			ArrayList	rowsOfObjects;
			String		qryResultId;
			Hashtable	users;
			UserVO		currentUser;

			/* Create mapping for transform dataset into objects
			 */
			objectMaps = new ArrayList();

			objectMap = new ObjectMap();
			objectMap.Type = typeof(UserVO);
			objectMap.TableShortName = this.TABLESHORTNAME_USER;
			objectMaps.Add(objectMap);

			qryResultId = DataAccessManager.GetInstance().PrepareQueryResult("test", "0815", QUERYNAME_USERS);
			/* Set search criteria
			 */
			if (pUserSearchCriteria.Id != 0) 
			{
				DataAccessManager.GetInstance().SetParameter(qryResultId, "Id", pUserSearchCriteria.Id.ToString());
			} 
			else 
			{
				// Set default search criteria for id
				DataAccessManager.GetInstance().SetParameter(qryResultId, "Id", "0", ">=");
			}
			if (pUserSearchCriteria.Name != null &&
				pUserSearchCriteria.Name != "") 
			{
				DataAccessManager.GetInstance().SetParameter(qryResultId, "Name", pUserSearchCriteria.Name);
			}
			if (pUserSearchCriteria.FirstName != null &&
				pUserSearchCriteria.FirstName != "") 
			{
				DataAccessManager.GetInstance().SetParameter(qryResultId, "FirstName", pUserSearchCriteria.FirstName);
			}
			if (pUserSearchCriteria.UserId != null &&
				pUserSearchCriteria.UserId != "") 
			{
				DataAccessManager.GetInstance().SetParameter(qryResultId, "UserId", pUserSearchCriteria.UserId);
			}
			if (pUserSearchCriteria.Password != null &&
				pUserSearchCriteria.Password != "") 
			{
				DataAccessManager.GetInstance().SetParameter(qryResultId, "Password", pUserSearchCriteria.Password);
			}

			DataAccessManager.GetInstance().ExecuteQuery(qryResultId);
			ds = DataAccessManager.GetInstance().GetDataSetFirstBlock(qryResultId);
			rowsOfObjects = RelationalObjectMapper.getInstance().mapDataSetToArrayList(objectMaps, ds);

			users = new Hashtable();
	
			foreach(ArrayList currentRow in rowsOfObjects) 
			{
				foreach(Object currentObject in currentRow) 
				{
					currentUser = (UserVO)currentObject;
					users.Add(currentUser.Id, currentUser);
				}
			}
			return users;
		}

		/// <summary>
		/// Inserts a new user  into the system
		/// </summary>
		/// <param name="pUser">The user to be inserted</param>
		public override void insertUser(IUser pUser)
		{
		}

		/// <summary>
		/// Updates an existing user into the system.
		/// </summary>
		/// <param name="pUser">The user to be updated</param>
		public override void updateUser(IUser pUser)
		{
		}

		/// <summary>
		/// Deletes an existing user into the system. Only valid for rdb storage.
		/// </summary>
		/// <param name="pUser">The user to be deleted</param>
		public override void deleteUser(IUser pUser)
		{
		}
			
		#endregion // End of Methods


	}//end RdbUserBuilder
}//end namespace DataAccess
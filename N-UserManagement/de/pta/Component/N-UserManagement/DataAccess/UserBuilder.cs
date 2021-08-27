using System;
using System.Collections;

using de.pta.Component.N_UserManagement.Authorization;
using de.pta.Component.N_UserManagement.Exceptions;

namespace de.pta.Component.N_UserManagement.DataAccess
{
	/// <summary>
	/// Abstract class for getting Users from XML or database
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> R.Weiﬂ, PTA GmbH
	/// <b>Date:</b> Sep/29/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal abstract class UserBuilder 
	{

		#region Constructors

		public UserBuilder()
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
		internal abstract ArrayList getUserByLogin(String pUserId, String pPassword);

		/// <summary>
		/// Returns the list of users referring to search criteria
		/// </summary>
		/// <param name="pUserSearchCriteria">Search Criteria</param>
		/// <returns>Array of UserVO. Key is internal user id.</returns>
		public abstract Hashtable getUsers(IUser pUserSearchCriteria);

		/// <summary>
		/// Inserts a new user  into the system. Only valid for rdb storage
		/// </summary>
		/// <param name="pUser">The user to be inserted</param>
		public virtual void insertUser(IUser pUser)
		{
			//only valid for in case of database
			//storage
			throw new DataAccessException("ERROR_RDB_ONLY");
		}

		/// <summary>
		/// Updates an existing user into the system. Only valid for rdb storage
		/// </summary>
		/// <param name="pUser">The user to be updated</param>
		public virtual void updateUser(IUser pUser)
		{
			//only valid for in case of database
			//storage
			throw new DataAccessException("ERROR_RDB_ONLY");
		}

		/// <summary>
		/// Deletes an existing user into the system. Only valid for rdb storage
		/// </summary>
		/// <param name="pUser">The user to be deleted</param>
		public virtual void deleteUser(IUser pUser)
		{
			//only valid for in case of database
			//storage
			throw new DataAccessException("ERROR_RDB_ONLY");
		}

		#endregion // End of Methods

	}//end UserBuilder
}//end namespace DataAccess
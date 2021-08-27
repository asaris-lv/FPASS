using System;
using System.Collections;

using de.pta.Component.N_UserManagement.Authorization;
using de.pta.Component.N_UserManagement.Exceptions;

namespace de.pta.Component.N_UserManagement.DataAccess
{
	/// <summary>
	/// Abstract class for getting Roles from XML or database
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> R.Weiﬂ, PTA GmbH
	/// <b>Date:</b> Sep/12/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal abstract class RoleBuilder 
	{

		#region Constructors

		public RoleBuilder()
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
		/// </summary>
		/// <param name="pRoleSearchCriteria">Search Criteria</param>
		/// <returns>Array of RoleVO. Key is internal role id.</returns>
		public abstract Hashtable getRoles(IRole pRoleSearchCriteria);

		/// <summary>
		/// Inserts a new role into the system. Only valid for rdb storage
		/// </summary>
		/// <param name="pRole">The role to be inserted</param>
		public virtual void insertRole(IRole pRole) 
		{
			throw new DataAccessException("ERROR_RDB_ONLY");
		}

		/// <summary>
		/// Updates an existing role into the system. Only valid for rdb storage
		/// </summary>
		/// <param name="pRole">The role to be updated</param>
		public virtual void updateRole(IRole pRole)
		{
			throw new DataAccessException("ERROR_RDB_ONLY");
		}

		/// <summary>
		/// Deletes an existing role into the system. Only valid for rdb storage
		/// </summary>
		/// <param name="pRole">The role to be deleted</param>
		public virtual void deleteRole(IRole pRole)
		{
			throw new DataAccessException("ERROR_RDB_ONLY");
		}

		#endregion // End of Methods

	}
}
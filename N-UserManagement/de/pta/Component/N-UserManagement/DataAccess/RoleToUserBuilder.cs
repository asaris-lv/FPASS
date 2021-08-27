using System;
using System.Collections;

using de.pta.Component.N_UserManagement.Vo;
using de.pta.Component.N_UserManagement.Exceptions;

namespace de.pta.Component.N_UserManagement.DataAccess
{
	/// <summary>
	/// Abstract class for getting user-role assigments from XML or database
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> R.Weiﬂ, PTA GmbH
	/// <b>Date:</b> Sep/29/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal abstract class RoleToUserBuilder 
	{

		#region Constructors

		public RoleToUserBuilder()
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
		public abstract ArrayList getAssignments(RoleToUserVO pSearchCriteria);

		/// <summary>
		/// Inserts a new assignment into the system. Only valid for rdb storage
		/// </summary>
		/// <param name="pRoleToUser">The assignment to be inserted</param>
		public virtual void insertAssignment(RoleToUserVO pRoleToUser){
			throw new DataAccessException("ERROR_RDB_ONLY");
		}

		/// <summary>
		/// Deletes an exsiting assignment into the system. Only valid for rdb storage
		/// </summary>
		/// <param name="pRoleToUser">The assignment to be deleted</param>
		public virtual void deleteAssignment(RoleToUserVO pRoleToUser)
		{
			throw new DataAccessException("ERROR_RDB_ONLY");
		}
		#endregion // End of Methods

	}//end RoleToUserBuilder
}//end namespace DataAccess
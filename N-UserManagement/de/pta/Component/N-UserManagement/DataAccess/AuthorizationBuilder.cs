using System;
using System.Collections;

using de.pta.Component.N_UserManagement.Exceptions;
using de.pta.Component.N_UserManagement.Authorization;
using de.pta.Component.N_UserManagement.Vo;

namespace de.pta.Component.N_UserManagement.DataAccess
{
	/// <summary>
	/// Abstract class for getting authorizations from XML or database
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> R.Weiﬂ, PTA GmbH
	/// <b>Date:</b> Sep/12/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal abstract class AuthorizationBuilder 
	{

		#region Constructors

		public AuthorizationBuilder()
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
		/// Returns the list of authorizations referring to search criteria
		/// </summary>
		/// <param name="pAuthorizationSearchCriteria">Search Criteria</param>
		/// <returns>Array of AuthorizationVO</returns>
		public abstract ArrayList getAuthorizations(AuthorizationVO pAuthorizationSearchCriteria);

		/// <summary>
		/// Inserts a new authorization into the system. Only valid for rdb storage
		/// </summary>
		/// <param name="pAuthorization">The authorization to be inserted</param>
		public virtual void insertAuthorization(IAuthorization pAuthorization){
			throw new DataAccessException("ERROR_RDB_ONLY");
		}

		/// <summary>
		/// Updates an existing authorization into the system. Only valid for rdb storage
		/// </summary>
		/// <param name="pAuthorization">The authorization to be updated</param>
		public virtual void updateAuthorization(IAuthorization pAuthorization)
		{
			throw new DataAccessException("ERROR_RDB_ONLY");
		}

		/// <summary>
		/// Deletes an existing authorization into the system. Only valid for rdb storage
		/// </summary>
		/// <param name="pAuthorization">The authorization to be deleted</param>
		public virtual void deleteAuthorization(IAuthorization pAuthorization)
		{
			throw new DataAccessException("ERROR_RDB_ONLY");
		}

		#endregion // End of Methods

	}//end AuthorizationBuilder
}//end namespace DataAccess
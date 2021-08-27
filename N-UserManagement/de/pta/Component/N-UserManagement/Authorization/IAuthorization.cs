using System;

using de.pta.Component.N_UserManagement.Resource;

namespace de.pta.Component.N_UserManagement.Authorization
{
	/// <summary>
	/// Interface to give access to an authorization object.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> R.Weiﬂ, PTA GmbH
	/// <b>Date:</b> Aug/26/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	public interface IAuthorization
	{
		#region Accessors

		/// <summary>
		/// Accessor for access right to resource
		/// </summary>
		bool ResourceAccess
		{
			get;
			set;
		}

		/// <summary>
		/// Accessor for access right for record creation.
		/// </summary>
		bool Create
		{
			get;
			set;
		}

		/// <summary>
		/// Accessor for access right for record updates.
		/// </summary>
		bool Update
		{
			get;
			set;
		}

		/// <summary>
		/// Accessor for access right for record deletion.
		/// </summary>
		bool Delete
		{
			get;
			set;
		}

		#endregion // End of Accessors
	}
}

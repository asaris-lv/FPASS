using System;
using System.Collections;

namespace de.pta.Component.N_UserManagement.Authorization
{
	/// <summary>
	/// Interface to access user group object
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> R.Weiﬂ, PTA GmbH
	/// <b>Date:</b> Sep/11/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	public interface IUserGroup
	{
		#region Accessors 
		/// <summary>
		/// Accessor for internal id of a user group
		/// </summary>
		int Id
		{
			get;
			set;
		}

		/// <summary>
		/// Accessor for the name of a user group
		/// </summary>
		String Name
		{
			get;
			set;
		}

		/// <summary>
		/// Accessor for the description of a user group
		/// </summary>
		String Description
		{
			get;
			set;
		}

		/// <summary>
		/// Accessor for assigned roles
		/// </summary>
		ArrayList Roles
		{
			get;
			set;
		}

		#endregion //End of Accessors
	}
}

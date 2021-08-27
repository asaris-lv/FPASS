using System;
using System.Collections;

namespace de.pta.Component.N_UserManagement.Authorization
{
	/// <summary>
	/// Interface to access role object
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> R.Weiﬂ, PTA GmbH
	/// <b>Date:</b> Sep/11/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	public interface IRole
	{
		#region Accessors 
		/// <summary>
		/// Accessor for internal id of a role
		/// </summary>
		int Id
		{
			get;
			set;
		}

		/// <summary>
		/// Accessor for the name of a role
		/// </summary>
		String Name
		{
			get;
			set;
		}

		/// <summary>
		/// Accessor for the description of a role
		/// </summary>
		String Description
		{
			get;
			set;
		}

		#endregion //End of Accessors
	}
}

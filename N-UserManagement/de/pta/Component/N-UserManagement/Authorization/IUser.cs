using System;
using System.Collections;

namespace de.pta.Component.N_UserManagement.Authorization
{
	/// <summary>
	/// Interface to access user object
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> R.Weiﬂ, PTA GmbH
	/// <b>Date:</b> Aug/12/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	public interface IUser
	{
		#region Accessors 
		/// <summary>
		/// Accessor for internal id of a user
		/// </summary>
		int Id
		{
			get;
			set;
		}

		/// <summary>
		/// Accessor for the name of a user
		/// </summary>
		String Name
		{
			get;
			set;
		}

		/// <summary>
		/// Accessor for user's first name.
		/// </summary>
		String FirstName
		{
			get;
			set;
		}

		/// <summary>
		/// Accessor for user id.
		/// </summary>
		String UserId
		{
			get;
			set;
		}

		/// <summary>
		/// Accessor for user's password.
		/// </summary>
		String Password
		{
			get;
			set;
		}

		/// <summary>
		/// Accessor for flag wether user logged in for the first time.
		/// </summary>
		bool FirstLogin
		{
			get;
			set;
		}

		#endregion //End of Accessors
	}
}

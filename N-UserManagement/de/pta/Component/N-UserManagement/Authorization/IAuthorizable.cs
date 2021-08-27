using System;

namespace de.pta.Component.N_UserManagement.Authorization
{
	/// <summary>
	/// Interface to support automatic authorization verfication managed by user management component.
	/// To enable automatic verfication all elements to be authorized have to implement this interface, 
	/// e.g. all controls of a dialog or report.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> R.Weiﬂ, PTA GmbH
	/// <b>Date:</b> Aug/18/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	public interface IAuthorizable
	{
		#region Accessors
		/// <summary>
		/// Accessor for the id of an element to be authorized
		/// </summary>
		int ID
		{
			get;
		}
		#endregion // End of Accessors

		#region Methods

		/// <summary>
		/// Sets the authorization of an element. The implementation class has to implement the
		/// behaviour of the element dependent on the access right.
		/// </summary>
		/// <param name="pAuthorized">
		/// <code>true</code> user has access right to the element
		/// <code>false</code> access denied
		/// </param>
		void setAuthorization(bool pAuthorized);

		#endregion // End of Methods
	}
}

using System;

namespace de.pta.Component.N_UserManagement.Resource
{
	/// <summary>
	/// Interface to build a facade for all authorizable resources due 
	/// to implement a common handling of resource authorization.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> R.Weiﬂ, PTA GmbH
	/// <b>Date:</b> Aug/12/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	public interface IResource
	{
		#region Accessors 


		/// <summary>
		/// Accessor for the internal resource id.
		/// </summary>
		int Id
		{
			get;
			set;
		}

		/// <summary>
		/// Accessor for the resource name.
		/// </summary>
		String Name
		{
			get;
			set;
		}

		#endregion //End of Accessors
	}
}

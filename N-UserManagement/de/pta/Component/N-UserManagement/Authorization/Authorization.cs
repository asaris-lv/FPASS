using System;
using de.pta.Component.N_UserManagement.Resource;

namespace de.pta.Component.N_UserManagement.Authorization
{
	/// <summary>
	/// Definition of the access rights of a role to a resource.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> R.Weiﬂ, PTA GmbH
	/// <b>Date:</b> Aug/12/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class Authorization : IAuthorization
	{
		#region Members

		/// <summary>
		/// Access right to a resource
		/// </summary>
		private bool mResourceAccess;

		/// <summary>
		/// The Resource the authorization is valid for
		/// </summary>
		private IResource mResource;

		/// <summary>
		/// Create right for a dialog
		/// </summary>
		private bool mCreate;

		/// <summary>
		/// Update right for a dialog
		/// </summary>
		private bool mUpdate;

		/// <summary>
		/// Delete right for a dialog
		/// </summary>
		private bool mDelete;

		#endregion // End of Members

		#region Constructors

		public Authorization()
		{
			initialize();
		}

		#endregion // End of Constructors

		#region Initialization

		private void initialize()
		{
			this.ResourceAccess = false;
			this.Create = false;
			this.mDelete = false;
			this.Update = false;
		}	

		#endregion //End of Initialization

		#region Accessors

		/// <summary>
		/// Accessor for access right to resource
		/// </summary>
		public bool ResourceAccess
		{
			get
			{
				return mResourceAccess;
			}
			set
			{
				mResourceAccess = value;
			}
		}

		/// <summary>
		/// Accessor for the resource the authorization is valid for.
		/// </summary>
		public IResource Resource
		{
			get
			{
				return mResource;
			}
			set
			{
				mResource = value;
			}
		}

		/// <summary>
		/// Accessor for access right for record creation.
		/// </summary>
		public bool Create
		{
			get
			{
				return mCreate;
			}
			set
			{
				mCreate = value;
			}
		}

		/// <summary>
		/// Accessor for access right for record updates.
		/// </summary>
		public bool Update
		{
			get
			{
				return mUpdate;
			}
			set
			{
				mUpdate = value;
			}
		}

		/// <summary>
		/// Accessor for access right for record deletion.
		/// </summary>
		public bool Delete
		{
			get
			{
				return mDelete;
			}
			set
			{
				mDelete = value;
			}
		}

		#endregion // End of Accessors

	}
}

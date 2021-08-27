using System;

using de.pta.Component.N_UserManagement.Authorization;

namespace de.pta.Component.N_UserManagement.Vo {
	/// <summary>
	/// Value object for authorization objects from database
	/// </summary>
	/// <remarks><pre>
	/// <b>History</b>
	/// <b>Author:</b> R.Weiﬂ, PTA GmbH
	/// <b>Date:</b> Sep/25/2003
	/// <b>Remarks:</b> None
	/// </pre></remarks>
	public class AuthorizationVO : IAuthorization {

		#region Members

		/// <summary>
		/// Access right to a resource
		/// </summary>
		private bool mResourceAccess;
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
		/// <summary>
		/// internal role id the authorization belongs to
		/// </summary>
		private int mRoleId;
		/// <summary>
		/// internal resource id the authorization is valid for
		/// </summary>
		private int mResourceId;

		#endregion //End of Members

		#region Constructors

		public AuthorizationVO()
		{
			this.initialize();
		}

		#endregion //End of Constructors

		#region Initialization

		private void initialize()
		{
			this.mResourceAccess = true;
			this.mCreate  = true;
			this.mDelete = true;
			this.mUpdate = true;
		}

		#endregion //End of Initialization

		#region Accessors 

		/// <summary>
		/// Accessor for internal resource id the authorization is valid for
		/// </summary>
		public int ResourceId
		{
			get{
				return mResourceId;
			}
			set{
				mResourceId = value;
			}
		}

		/// <summary>
		/// Accessor for internal role id the authorization belongs to
		/// </summary>
		public int RoleId
		{
			get{
				return mRoleId;
			}
			set{
				mRoleId = value;
			}
		}

		/// <summary>
		/// Accessor for access right to resource
		/// </summary>
		public bool ResourceAccess{
			get{
				return mResourceAccess;
			}
			set{
				mResourceAccess = value;
			}
		}

		/// <summary>
		/// Accessor for access right for record creation.
		/// </summary>
		public bool Create{
			get{
				return mCreate;
			}
			set{
				mCreate = value;
			}
		}

		/// <summary>
		/// Accessor for access right for record updates.
		/// </summary>
		public bool Update{
			get{
				return mUpdate;
			}
			set{
				mUpdate = value;
			}
		}

		/// <summary>
		/// Accessor for access right for record deletion.
		/// </summary>
		public bool Delete{
			get{
				return mDelete;
			}
			set{
				mDelete = value;
			}
		}

		#endregion //End of Accessors

	}//end AuthorizationVO
}//end namespace Vo
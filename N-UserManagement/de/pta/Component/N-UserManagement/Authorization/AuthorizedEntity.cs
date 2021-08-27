using System;
using System.Collections;

namespace de.pta.Component.N_UserManagement.Authorization
{
	/// <summary>
	/// Abstract Base class for entities to be assigned to an authorization role. Authorized entities can
	/// be users or user groups
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> R.Weiﬂ, PTA GmbH
	/// <b>Date:</b> Aug/12/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal abstract class AuthorizedEntity
	{
		#region Members

		/// <summary>
		/// Internal id of an authorized entity.
		/// </summary>
		protected	int		mId;

		/// <summary>
		/// Name of an authorized entity.
		/// </summary>
		protected	String	mName;

		/// <summary>
		/// Roles assigned to the authorized entity.
		/// </summary>
		protected	ArrayList	mRoles;

		#endregion //End of Members

		#region Accessors 

		/// <summary>
		/// Accessor for internal id of an authorized entity.
		/// </summary>
		public int Id
		{
			get
			{
				return mId;
			}
			set
			{
				mId = value;
			}
		}

		/// <summary>
		/// Accessor for the name of an authorized entity.
		/// </summary>
		public String Name
		{
			get
			{
				return mName;
			}
			set
			{
				mName = value;
			}
		}

		/// <summary>
		/// Accessor for the roles assigned to the authorized entity.
		/// </summary>
		public ArrayList Roles
		{
			get
			{
				return mRoles;
			}
			set
			{
				mRoles = value;
			}
		}

		#endregion //End of Accessors

		#region Methods

		/// <summary>
		/// Assigns a role to the authorized entity.
		/// </summary>
		/// <param name="pRole">Role to be assigned.</param>
		public void addRole(Role pRole)
		{
			if (this.Roles == null)
			{
				mRoles = new ArrayList();
			}

			this.Roles.Add(pRole);
		}

		/// <summary>
		/// Unassigns a role from the authorized entity.
		/// </summary>
		/// <param name="pRole">Role to be unassigned.</param>
		public void deleteRole(Role pRole) 
		{
			if (this.Roles != null)
			{
				this.Roles.Remove(pRole);
			}
		}

		#endregion // End of Methods
	}
}

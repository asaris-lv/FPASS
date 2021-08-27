using System;
using System.Collections;

namespace de.pta.Component.N_UserManagement.Authorization
{
	/// <summary>
	/// Definition of an application role. Roles have assigned 
	/// resources assigned including the access right for a single resource.
	/// To authorize a user or a user group for resources the role is assigned to a user or a user group.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> R.Weiﬂ, PTA GmbH
	/// <b>Date:</b> Aug/12/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class Role : IRole
	{
		#region Members

		/// <summary>
		/// Internal id of a role.
		/// </summary>
		private	int		mId;

		/// <summary>
		/// Name of the role.
		/// </summary>
		private	String	mName;

		/// <summary>
		/// Description of the role.
		/// </summary>
		private	String	mDescription;

		/// <summary>
		/// Authorized Entities (User or User Group) assigned to the role.
		/// </summary>
		private ArrayList	mAuthorizedEntities;

		/// <summary>
		/// The authorizations for accessing the resources included inside the authorization objects.
		/// </summary>
		private ArrayList	mAuthorizations;

		#endregion //End of Members

		#region Constructors

		public Role()
		{
			initialize();
		}

		#endregion // End of Constructors

		#region Initialization

		private void initialize()
		{
		}	

		#endregion //End of Initialization

		#region Accessors 

		/// <summary>
		/// Accessor for internal id.
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
		/// Accessor for role name.
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
		/// Accessor for role description.
		/// </summary>
		public String Description
		{
			get
			{
				return mDescription;
			}
			set
			{
				mDescription = value;
			}
		}

		/// <summary>
		/// Accessor for the list of authorized entities assigned to this role.
		/// </summary>
		public ArrayList AuthorizedEntities
		{
			get
			{
				return mAuthorizedEntities;
			}
			set
			{
				mAuthorizedEntities = value;
			}
		}

		/// <summary>
		/// Accessor for the authorizations of the role.
		/// </summary>
		public ArrayList Authorizations
		{
			get
			{
				return mAuthorizations;
			}
			set
			{
				mAuthorizations = value;
			}
		}

		#endregion //End of Accessors

		#region Methods

		/// <summary>
		/// Assignes a new user or user group to the role.
		/// </summary>
		/// <param name="pAuthorizedEntity">new user or user group</param>
		public void addAuthorizedEntity(AuthorizedEntity pAuthorizedEntity)
		{
			if (this.AuthorizedEntities == null) 
			{
				mAuthorizedEntities = new ArrayList();
			}
			this.AuthorizedEntities.Add(pAuthorizedEntity);
		}

		/// <summary>
		/// Unassignment of a user or user group from the role.
		/// </summary>
		/// <param name="pAuthorizedEntity">user or user group to be unassigned.</param>
		public void deleteAuthorizedEntity(AuthorizedEntity pAuthorizedEntity)
		{
			if (this.AuthorizedEntities != null)
			{
				this.AuthorizedEntities.Remove(pAuthorizedEntity);
			}
		}

		/// <summary>
		/// Assigns an authorization to the role.
		/// </summary>
		/// <param name="pAuthorization">The authorization to be assigned.</param>
		public void addAuthorization(Authorization pAuthorization) 
		{
			if (this.Authorizations == null) 
			{
				mAuthorizations = new ArrayList();
			}
			this.Authorizations.Add(pAuthorization);
		}

		/// <summary>
		/// Unassigns an authorization from the role.
		/// </summary>
		/// <param name="pAuthorization">The authorization to be unassigned.</param>
		public void deleteAuthorization(Authorization pAuthorization)
		{
			if (this.Authorizations != null) 
			{
				this.Authorizations.Remove(pAuthorization);
			}
		}

		#endregion // End of Methods

	}
}

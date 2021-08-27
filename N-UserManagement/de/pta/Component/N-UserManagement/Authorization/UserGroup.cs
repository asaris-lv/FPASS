using System;
using System.Collections;

namespace de.pta.Component.N_UserManagement.Authorization
{
	/// <summary>
	/// Definition of a UserGroup. UserGroups can be assigned directly to an authorization role,
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> R.Weiﬂ, PTA GmbH
	/// <b>Date:</b> Aug/12/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class UserGroup : AuthorizedEntity, IUserGroup
	{
		#region Members

		/// <summary>
		/// Description of the user group.
		/// </summary>
		private	String		mDescription;

		/// <summary>
		/// Assigned users to the user group.
		/// </summary>
		private	ArrayList	mUsers;

		#endregion //End of Members

		#region Constructors

		public UserGroup()
		{
			initialize();
		}

		#endregion //End of Constructors

		#region Initialization

		private void initialize()
		{
		}	

		#endregion //End of Initialization

		#region Accessors 

		/// <summary>
		/// Accessor for the description of the user group.
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
		/// Accessor for the users assigned to the user group.
		/// </summary>
		public ArrayList Users
		{
			get
			{
				return mUsers;
			}
			set
			{
				mUsers = value;
			}
		}
		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// Adds a new user to the user group.
		/// </summary>
		/// <param name="pControl">New user.</param>
		public void addUser(User pUser) 
		{
			if (this.Users == null) 
			{
				mUsers = new ArrayList();
			}
			mUsers.Add(pUser);
		}

		/// <summary>
		/// Deletes a control from the dialog container.
		/// </summary>
		/// <param name="pControl">Control to be deleted.</param>
		public void deleteUser(User pUser)
		{
			if (this.Users != null)
			{
				mUsers.Remove(pUser);
			}
		}

		#endregion // End of Methods

	}
}

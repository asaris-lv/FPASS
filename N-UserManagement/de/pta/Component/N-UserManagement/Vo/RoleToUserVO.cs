namespace de.pta.Component.N_UserManagement.Vo 
{
	/// <summary>
	/// Value object for assignments of roles to user from database
	/// </summary>
	/// <remarks><pre>
	/// <b>History</b>
	/// <b>Author:</b> R.Weiﬂ, PTA GmbH
	/// <b>Date:</b> Sep/25/2003
	/// <b>Remarks:</b> None
	/// </pre></remarks>
	public class RoleToUserVO 
	{

		#region Members

		/// <summary>
		/// internal role id assigned to user
		/// </summary>
		private int mRoleId;
		/// <summary>
		/// internal user id assigned to role
		/// </summary>
		private int mUserId;

		#endregion //End of Members

		#region Constructors

		public RoleToUserVO()
		{

		}

		#endregion //End of Constructors

		#region Initialization

		private void initialize()
		{

		}

		#endregion //End of Initialization

		#region Accessors 

		/// <summary>
		/// Accessor for internal role id assigned to user
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
		/// Accessor for internal user id assigned to role
		/// </summary>
		public int UserId
		{
			get{
				return mUserId;
			}
			set{
				mUserId = value;
			}
		}

		#endregion //End of Accessors

	}//end RoleToUserVO
}//end namespace Vo
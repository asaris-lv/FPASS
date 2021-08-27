using System;


using de.pta.Component.N_UserManagement.Authorization;

namespace de.pta.Component.N_UserManagement.Vo 
{
	/// <summary>
	/// Value object for role objects from database
	/// </summary>
	/// <remarks><pre>
	/// <b>History</b>
	/// <b>Author:</b> R.Weiﬂ, PTA GmbH
	/// <b>Date:</b> Sep/25/2003
	/// <b>Remarks:</b> None
	/// </pre></remarks>
	public class RoleVO : IRole 
	{

		#region Members

		/// <summary>
		/// Internal id of a role.
		/// </summary>
		private int mId;
		/// <summary>
		/// Name of the role.
		/// </summary>
		private String mName;
		/// <summary>
		/// Description of the role.
		/// </summary>
		private String mDescription;
		

		#endregion //End of Members

		#region Constructors

		public RoleVO()
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
		/// Accessor for internal id.
		/// </summary>
		public int Id
		{
			get{
				return mId;
			}
			set{
				mId = value;
			}
		}

		/// <summary>
		/// Accessor for role name.
		/// </summary>
		public String Name{
			get{
				return mName;
			}
			set{
				mName = value;
			}
		}

		/// <summary>
		/// Accessor for role description.
		/// </summary>
		public String Description{
			get{
				return mDescription;
			}
			set{
				mDescription = value;
			}
		}


		#endregion //End of Accessors

	}//end RoleVO
}//end namespace Vo
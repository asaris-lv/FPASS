using System;


using de.pta.Component.N_UserManagement.Resource;


namespace de.pta.Component.N_UserManagement.Vo {
	/// <summary>
	/// Abstract base class for authorizable resources
	/// </summary>
	/// <remarks><pre>
	/// <b>History</b>
	/// <b>Author:</b> R.Weiﬂ, PTA GmbH
	/// <b>Date:</b> sEP/25/2003 	<b>Remarks:</b> None
	/// </pre></remarks>
	public class ResourceVO : IResource {

		#region Members
		/// <summary>
		/// Internal resource id.
		/// </summary>
		private int mId;
		/// <summary>
		/// Resource name.
		/// </summary>
		private String mName;
		/// <summary>
		/// Resource description.
		/// </summary>
		private String mDescription;

		#endregion //End of Members

		#region Constructors

		public ResourceVO()
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
		/// Accessor for the internal resource id.
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
		/// Accessor for the resource name.
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
		/// Accessor for the resource description
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

	}//end ResourceVO
}//end namespace Vo
using System;

namespace de.pta.Component.N_UserManagement.Resource
{
	/// <summary>
	/// Abstract base class for authorizable resources 
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> R.Weiﬂ, PTA GmbH
	/// <b>Date:</b> Aug/12/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal abstract class Resource : IResource
	{
		#region Members

		/// <summary>
		/// Internal resource id.
		/// </summary>
		protected	int		mId;

		/// <summary>
		/// Resource name.
		/// </summary>
		protected	String	mName;

		/// <summary>
		/// Resource description.
		/// </summary>
		protected	String	mDescription;

		#endregion //End of Members

		#region Accessors 

		/// <summary>
		/// Accessor for the internal resource id.
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
		/// Accessor for the resource name.
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
		/// Accessor for the resource description
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

		#endregion //End of Accessors

	}
}

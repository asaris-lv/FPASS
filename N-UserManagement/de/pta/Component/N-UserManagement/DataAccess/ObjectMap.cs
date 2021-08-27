using System;

namespace de.pta.Component.N_UserManagement.DataAccess
{
	/// <summary>
	/// Mapping between class type and table name
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> R.Weiﬂ, PTA GmbH
	/// <b>Date:</b> Aug/21/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class ObjectMap
	{
		#region Members

		/// <summary>
		/// Class type
		/// </summary>
		private Type	mType;

		/// <summary>
		/// short name of corresponding table
		/// </summary>
		private String	mTableShortName;

		#endregion // End of Members

		#region Cunstructors

		public ObjectMap()
		{
		}

		#endregion // End of Construstors

		#region Accessors

		/// <summary>
		/// Accessor for class type
		/// </summary>
		public Type Type
		{
			get
			{
				return mType;
			}
			set
			{
				mType = value;
			}
		}

		/// <summary>
		/// Accessor for short name of corresponding table
		/// </summary>
		public String TableShortName
		{
			get
			{
				return mTableShortName;
			}
			set
			{
				mTableShortName = value; 
			}
		}

		#endregion //End of Accessors
	}
}

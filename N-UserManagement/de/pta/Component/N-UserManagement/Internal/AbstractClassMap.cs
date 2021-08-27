using System;
using System.Collections;

namespace de.pta.Component.N_UserManagement.Internal
{
	/// <summary>
	/// Mapping of abstract class to subclass via type field in database
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> R.Weiﬂ, PTA GmbH
	/// <b>Date:</b> Aug/22/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class AbstractClassMap
	{
		#region Members

		/// <summary>
		/// Abstract Class Name
		/// </summary>
		private String mAbstractClassName;

		/// <summary>
		/// Name of type field to indicate concrete subclass
		/// </summary>
		private String	mTypeFieldName;

		/// <summary>
		/// List of type ids to corresponfding table
		/// </summary>
		private Hashtable mAbstractMap;

		#endregion // End of Members


		#region Constructors

		public AbstractClassMap()
		{
			this.initialize();
		}

		#endregion // End of Constructors

		#region Initialize

		private void initialize()
		{
			mAbstractMap = new Hashtable();
		}

		#endregion // End of initialize

		#region Accessors

		/// <summary>
		/// Accessor for name of abstract class
		/// </summary>
		public String AbstractClassName
		{
			get
			{
				return mAbstractClassName;
			}
			set
			{
				mAbstractClassName = value;
			}
		}

		/// <summary>
		/// Accessor for list of type ids to corresponfding table
		/// </summary>
		public Hashtable AbstractMap
		{
			get
			{
				return mAbstractMap;
			}
			set
			{
				mAbstractMap = value;
			}
		}

		/// <summary>
		/// Accessor for name of type field to indicate concrete subclass
		/// </summary>
		public String TypeFieldName
		{
			get
			{
				return mTypeFieldName;
			}
			set
			{
				mTypeFieldName = value;
			}
		}

		#endregion // End of Accessors
	}
}

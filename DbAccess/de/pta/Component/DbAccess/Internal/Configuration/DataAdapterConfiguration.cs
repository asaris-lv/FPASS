using System;
using System.Collections;

namespace de.pta.Component.DbAccess.Internal.Configuration
{
	/// <summary>
	/// This class acts as a value object. It holds the configuration
	/// data for a DataAdapter.
	/// </summary>
	/// <remarks>
	/// <para><b>History</b></para>
	/// <list type="table">
	/// <item>
	/// <term><b>Author:</b></term>
	/// <description>A. Seibt, PTA GmbH</description>
	/// </item>
	/// <item>
	/// <term><b>Date:</b></term>
	/// <description>Sep/01/2003</description>
	/// </item>
	/// <item>
	/// <term><b>Remarks:</b></term>
	/// <description>initial version</description>
	/// </item>
	/// </list>
	/// </remarks>
	public class DataAdapterConfiguration : BaseConfiguration
	{
		#region Members

		/// <summary>
		/// A Flag which denotes if the data adapter is readonly
		/// </summary>
		private bool      mReadOnly;

		/// <summary>
		/// Defines the default name of the source table for mapping in a data set.
		/// </summary>
		private string    mSourceTable;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public DataAdapterConfiguration() : base()
		{
			initialize();
		}

		#endregion //End of Constructors

		#region Initialization

		/// <summary>
		/// Initializes the members.
		/// </summary>
		private void initialize()
		{
			mReadOnly = false;
			// The default if Property DefaultSourceTableName not set in a DataAdapter
			mSourceTable = "Table"; 
		}	

		#endregion //End of Initialization

		#region Accessors 

		/// <summary>
		/// Gets or sets the readonly mode of the data adapter.
		/// </summary>
		public bool ReadOnly
		{
			get { return mReadOnly; }
			set { mReadOnly = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string SourceTable
		{
			get { return mSourceTable; }
			set { mSourceTable = value; }
		}

		#endregion //End of Accessors

		#region Methods 

		#endregion // End of Methods


	}
}

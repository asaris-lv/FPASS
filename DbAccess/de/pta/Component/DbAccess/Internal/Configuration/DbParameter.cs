using System;

namespace de.pta.Component.DbAccess.Internal.Configuration
{
	/// <summary>
	/// This class encapsulates the configuration data for a parameter of a SQL statement.
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
	public class DbParameter
	{
		#region Members

		/// <summary>
		/// The name of the parameter.
		/// </summary>
		private string mName;

		/// <summary>
		/// The type of the parameter.
		/// </summary>
		private string mDbType;

		/// <summary>
		/// The length of the parameter (only used for strings / varchars).
		/// </summary>
		private int    mLength;

		/// <summary>
		/// The number of digits of the parameter (only used for number / decimal).
		/// </summary>
		private byte   mPrecision;

		/// <summary>
		/// The number of digits after the decimal point for the parameter
		/// (only used for number / decimal).
		/// </summary>
		private byte   mScale;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public DbParameter()
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
			mName		   = null;
			mDbType    = null;
			mLength    = 0;
			mPrecision = 0;
			mScale     = 0;
		}	

		#endregion //End of Initialization

		#region Accessors 

		/// <summary>
		/// Gets or sets the name of the parameter.
		/// </summary>
		public string Name
		{
			get { return mName; }
			set { mName = value; }
		}

		/// <summary>
		/// Gets or sets the data type (in the database) of the parameter.
		/// </summary>
		public string DbType
		{
			get { return mDbType; }
			set { mDbType = value; }
		}

		/// <summary>
		/// Gets or sets the length of the parameter value. This property is only
		/// evaluated if the parameter has the DbType "varchar".
		/// </summary>
		public int Length
		{
			get { return mLength; }
			set { mLength = value; }
		}

		/// <summary>
		/// Gets or sets the number of digits of the parameter. This property is only
		/// evaluated if the parameter has the DbType "number".
		/// </summary>
		public byte Precision
		{
			get { return mPrecision; }
			set { mPrecision = value; }
		}

		/// <summary>
		/// gets or sets the number of decimal digits of the parameter. This property
		/// is only evaluated if the parameter has the DbType "number".
		/// </summary>
		public byte Scale
		{
			get { return mScale; }
			set { mScale = value; }
		}

		#endregion //End of Accessors

		#region Methods 

		#endregion // End of Methods


	}
}

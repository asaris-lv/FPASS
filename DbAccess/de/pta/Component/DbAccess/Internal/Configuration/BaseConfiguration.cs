using System;
using System.Collections;

namespace de.pta.Component.DbAccess.Internal.Configuration
{
	/// <summary>
	/// This is the base class for the configuration data of a data adapter or a command.
	/// It holds the configuration items which are common to a DataAdapter and a Command.
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
	/// <description>Sep/02/2003</description>
	/// </item>
	/// <item>
	/// <term><b>Remarks:</b></term>
	/// <description>initial version</description>
	/// </item>
	/// </list>
	/// </remarks>
	public class BaseConfiguration
	{
		#region Members

		/// <summary>
		/// The unique identifier for the command or data adapter
		/// </summary>
		private string    mId;

		/// <summary>
		/// The SQL associated with the command or data adapter
		/// </summary>
		private string    mSql;

		/// <summary>
		/// The parameters for the SQL
		/// </summary>
		private Hashtable mParameters;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public BaseConfiguration()
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
			mId         = "";
			mSql        = "";
			mParameters = new Hashtable();
		}	

		#endregion //End of Initialization

		#region Accessors 

		/// <summary>
		/// Gets or sets the unique identifier for the command.
		/// </summary>
		public string Id
		{
			get { return mId; }
			set { mId = value; }
		}

		/// <summary>
		/// Gets or sets the SQL associated with the command.
		/// </summary>
		public string Sql
		{
			get { return mSql; }
			set { mSql = value; }
		}

		/// <summary>
		/// Gets the list of parameters associated with the command
		/// </summary>
		public Hashtable Parameters
		{
			get { return mParameters; }
		}

		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// Adds a new parameter to the list of parameters.
		/// </summary>
		/// <param name="pParam">A DbParameter object</param>
		public void AddParameter(DbParameter pParam)
		{
			mParameters.Add(pParam.Name, pParam);
		}

		#endregion // End of Methods

	}
}

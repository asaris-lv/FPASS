using System;

using de.pta.Component.DbAccess.Enumerations;
using de.pta.Component.DbAccess.Exceptions;
using de.pta.Component.DbAccess.Internal;
using de.pta.Component.DbAccess.Internal.Configuration;

using de.pta.Component.Logging.Log4NetWrapper;

namespace de.pta.Component.DbAccess
{
	/// <summary>
	/// This class implements the Gang of Four Factory pattern to return objects which provides
	/// the access to different databases. The following databases are supported:
	/// <list type="bullet">
	/// <item><description>Microsoft Sql Server</description></item>
	/// <item><description>Oracle</description></item>
	/// <item><description>any OleDb compliant database</description></item>
	/// </list>
	/// All information required to access data from a database is stored in a XML definition
	/// file. In the future it will be possible to store the information in database tables.
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
	/// <description>Aug/29/2003</description>
	/// </item>
	/// <item>
	/// <term><b>Remarks:</b></term>
	/// <description>initial version</description>
	/// </item>
	/// </list>
	/// </remarks>
	public class ProviderFactory
	{
		#region Members

		/// <summary>
		/// Holds the only instance of the class.
		/// </summary>
		private static ProviderFactory mInstance = null;

		/// <summary>
		/// Holds the configuration data for database access.
		/// See <see cref="de.pta.Component.DbAccess.Internal.Configuration.DbAccessConfiguration">DbAccessConfiguration</see> for Details.
		/// </summary>
		private DbAccessConfiguration  mConfig   = null;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// The constructor is private because the class implements the Singleton pattern.
		/// </summary>
		/// <param name="pConfigType">The type of the configuration</param>
		/// <remarks>
		/// See the enumeration <see cref="de.pta.Component.DbAccess.Enumerations.DbAccessConfigType">DbAccessConfigType</see>
		/// for the supported configuration types. 
		/// </remarks>
		private ProviderFactory(DbAccessConfigType pConfigType)
		{
			IConfigFactory cf = ConfigFactory.GetFactory(pConfigType);
			mConfig = cf.GetConfiguration();
		}
		#endregion //End of Constructors

		#region Initialization
		#endregion //End of Initialization

		#region Accessors 
		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// Returns the only instance of the class.
		/// </summary>
		/// <param name="pConfigType">The type of configuration to read.</param>
		/// <returns></returns>
		/// <remarks>
		/// See the enumeration <see cref="de.pta.Component.DbAccess.Enumerations.DbAccessConfigType">DbAccessConfigType</see>
		/// for the supported configuration types. 
		/// </remarks>
		public static ProviderFactory GetInstance(DbAccessConfigType pConfigType)
		{
			if(mInstance == null)
			{
				mInstance = new ProviderFactory(pConfigType);
			}
			return mInstance;
		}

		/// <summary>
		/// Returns the concrete data provider to use in the application.
		/// </summary>
		/// <returns></returns>
		public IProvider GetProvider() 
		{
			if(null == mConfig)
			{
				return null;
			}
			switch(mConfig.ProviderType) 
			{
				case DbAccessProviderType.None:
					return null;
				case DbAccessProviderType.OleDb:
					return new OleDbProvider();
				case DbAccessProviderType.Oracle:
					return new OracleProvider();
				case DbAccessProviderType.OracleOdp:
					return null;
				case DbAccessProviderType.SqlClient:
					return new SqlProvider();
				default:
					return null;
			}
		}
		#endregion // End of Methods


	}
}

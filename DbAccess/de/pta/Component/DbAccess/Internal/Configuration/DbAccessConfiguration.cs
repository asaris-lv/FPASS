using System;
using System.Collections;

using de.pta.Component.DbAccess.Cryptography;
using de.pta.Component.DbAccess.Enumerations;

namespace de.pta.Component.DbAccess.Internal.Configuration
{
	/// <summary>
	/// The class acts as a container for the configuration data of the DbAccess component.
	/// It is designed as Singleton.
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
	public class DbAccessConfiguration
	{
		#region Members

		/// <summary>
		/// The only instance of this class.
		/// </summary>
		private static DbAccessConfiguration mInstance = null;

		/// <summary>
		/// The type of the database provider which implements the access to the database.
		/// </summary>
		/// <remarks>
		/// See enumeration <see cref="de.pta.Component.DbAccess.Enumerations.DbAccessProviderType"/>
		/// for supported databases.
		/// </remarks>
		private DbAccessProviderType mProviderType;

		/// <summary>
		/// The connection string used to acces the database.
		/// </summary>
		private string mConnectString;

		/// <summary>
		/// A flag which denotes if the connection string was stored encrypted in the XML.
		/// </summary>
		private bool   mConnectStringEncrypted;

		/// <summary>
		/// A list of data adapter configurations.
		/// </summary>
		private Hashtable mAdapterData;

		/// <summary>
		/// A list of command configurations.
		/// </summary>
		private Hashtable mCommandData;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		private DbAccessConfiguration()
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
			mProviderType           = DbAccessProviderType.None;
			mConnectString          = null;
			mConnectStringEncrypted = false;
			mAdapterData            = new Hashtable();
			mCommandData            = new Hashtable();
		}	

		#endregion //End of Initialization

		#region Accessors 

		/// <summary>
		/// Sets or returns the connection string.
		/// </summary>
		public string ConnectString 
		{
			get 
			{ 
				if(mConnectStringEncrypted)
				{
					return CryptoUtil.Decrypt(mConnectString); 
				}
				else
				{
					return mConnectString; 
				}
			}
			set { mConnectString = value; }
		}

		/// <summary>
		/// Sets or returns the data provider type.
		/// </summary>
		public DbAccessProviderType ProviderType
		{
			get { return mProviderType; }
			set { mProviderType = value; }
		}

		/// <summary>
		/// Sets or returns the encrypted-flag.
		/// </summary>
		public bool ConnectStringEncrypted
		{
			get { return mConnectStringEncrypted; }
			set { mConnectStringEncrypted = value; }
		}

		#endregion //End of Accessors

		#region Methods 
		
		/// <summary>
		/// Returns the only instance of the class
		/// </summary>
		/// <returns></returns>
		public static DbAccessConfiguration GetInstance() 
		{
			if(mInstance == null) 
			{
				mInstance = new DbAccessConfiguration();
			}
			return mInstance;
		}

		/// <summary>
		/// Adds the configuration data for a DataAdapter.
		/// </summary>
		/// <param name="pId">The unique id for the configuration</param>
		/// <param name="pConfig">A DataAdapterConfiguration object</param>
		public void AddAdapterData(string pId, DataAdapterConfiguration pConfig) 
		{
			mAdapterData.Add(pId, pConfig);
		}

		/// <summary>
		/// Returns the configuration data for a specific data adapter.
		/// </summary>
		/// <param name="pId">The unique identifier for the configuration.</param>
		/// <returns></returns>
		public DataAdapterConfiguration GetAdapterData(string pId) 
		{
			if(mAdapterData.ContainsKey(pId))
			{
				return (DataAdapterConfiguration)mAdapterData[pId];
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Adds the configuration data for a SQL command.
		/// </summary>
		/// <param name="pId">The unique id for the configuration</param>
		/// <param name="pConfig">A CommandConfiguration object.</param>
		public void AddCommandData(string pId, CommandConfiguration pConfig) 
		{
			mCommandData.Add(pId, pConfig);
		}

		/// <summary>
		/// Returns the configuration data for a specific command.
		/// </summary>
		/// <param name="pId">The unique identifier for the configuration.</param>
		/// <returns></returns>
		public CommandConfiguration GetCommandData(string pId) 
		{
			if(mCommandData.ContainsKey(pId))
			{
				return (CommandConfiguration)mCommandData[pId];
			}
			else
			{
				return null;
			}
		}

		#endregion // End of Methods


	}
}

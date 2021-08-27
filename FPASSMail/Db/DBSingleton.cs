using System;
using System.Configuration;

using de.pta.Component.DbAccess;
using de.pta.Component.DbAccess.Enumerations;
using de.pta.Component.DbAccess.Exceptions;

namespace Evonik.FPASSMail.Db
{
	/// <summary>
	/// Summary description for DBSingleton.
	/// </summary>
	/// <remarks>
	/// <para><b>History</b></para>
	/// <div class="tablediv">
	/// <table class="dtTABLE" cellspacing="0">
	///		<tr>
	///			<th width="20%">PTA GmbH</th>
	///			<th width="20%">12/01/2003</th>
	///			<th width="60%">Remarks</th>
	///		</tr>
	/// </table>
	/// </div>
	/// </remarks>
	public sealed class DBSingleton
	{
		#region Members

		private const string CONFIGURATION_TYPE    = "ConfigurationType";
		private const string CONFIG_TYPE_XML       = "xml";
		private const string CONFIG_TYPE_SQLSERVER = "sqlserver";
		private const string CONFIG_TYPE_ORACLE    = "oracle";
		private const string CONFIG_TYPE_OLEDB     = "oledb";

		//private static DBSingleton mInstance = null;
        //private static readonly DBSingleton mInstance = new DBSingleton();
        private static volatile DBSingleton mInstance;
        private static object syncRoot = new Object();

		private IProvider mProvider = null;

		#endregion //End of Members

        /// <summary>
        /// Calls only instance of DBSingleton. Thread-safe (see https://msdn.microsoft.com/en-us/library/ff650316.aspx)
        /// as can be used by FPASS main thread and background thread/s
        /// </summary>
        /// <returns></returns>
        public static DBSingleton GetInstance()
        {

            if (mInstance == null)
            {
                lock (syncRoot)
                {
                    if (mInstance == null)
                        mInstance = new DBSingleton();
                }
            }

            return mInstance;
        }
   
		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		private DBSingleton()
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
			// Get the database provider from the application settings file
            string configType = ((string) System.Configuration.ConfigurationManager.AppSettings[CONFIGURATION_TYPE]).ToLower();

			try 
			{
				// Get an instance of the database provider
				if(configType.Equals(CONFIG_TYPE_XML))
				{
					mProvider = ProviderFactory.GetInstance(DbAccessConfigType.Xml).GetProvider();
				}
				else if(configType.Equals(CONFIG_TYPE_SQLSERVER))
				{
					mProvider = ProviderFactory.GetInstance(DbAccessConfigType.RdbSqlServer).GetProvider();
				}
				else if(configType.Equals(CONFIG_TYPE_ORACLE))
				{
					mProvider = ProviderFactory.GetInstance(DbAccessConfigType.RdbOracle).GetProvider();
				}
				else if(configType.Equals(CONFIG_TYPE_OLEDB))
				{
					mProvider = ProviderFactory.GetInstance(DbAccessConfigType.RdbOleDb).GetProvider();
				}
				else
				{
                    throw new ConfigurationException("Invalid configuration type <" + configType + "> in app.settings.");
				}
			}
			catch(InvalidConfigFactoryException ie)
			{
                throw new ConfigurationException("Invalid connection type in Configuration.xml", ie);
			}
		}	

		#endregion //End of Initialization

		#region Accessors 

		public IProvider DataProvider
		{
			get { return mProvider; }
		}

		#endregion

		#region Methods 


		#endregion // End of Methods


	}
}

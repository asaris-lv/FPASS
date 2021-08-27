using System;
using System.Collections;
using System.Windows.Forms;
using de.pta.Component.Common;

namespace de.pta.Component.DataAccess.Internal
{
	/// <summary>
	/// Summary description for DataAccessConfiguration.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> A.Opierzynski, PTA GmbH
	/// <b>Date:</b> Apr/02/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class DataAccessConfiguration
	{
	#region Members

		private const String XML_NODE_DATAACCESS = "application/configuration/DataAccess";
		private static DataAccessConfiguration instance = null;

		private String		xmlTempPath;
		private String		xmlTempPathHttp;
		private int			applicationMaxRecords;
		private int			dataBlockSize;
		private int			dataPageSize;
		private bool		turboModeFlag;
		private int			turboModeMaxRecords;
		private Hashtable	queries;

		private String	cleanUpUser;
		private String	cleanUpHost;
		private String	cleanUpPwd;
		private int		timeInSeconds;
		private String	clearAllTime;

	#endregion //End of Members

	#region Constructors

		/// <summary>
		/// Private constructor assures that no further instance is created.
		/// </summary>
		protected DataAccessConfiguration()
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
			xmlTempPath				= "";
			xmlTempPathHttp			= "";
			applicationMaxRecords	= 0;
            dataBlockSize			= 0;
			dataPageSize			= 0;
			turboModeFlag			= false;
			turboModeMaxRecords		= 0;
			queries					= new Hashtable();

			CleanUpUser				= "";
			CleanUpHost				= "";
			CleanUpPwd				= "";
			timeInSeconds			= 0;
			clearAllTime			= "";
		}

		/// <summary>
		/// Singleton - returns exactly one instance of that class
		/// </summary>
		/// <returns>Instance of a QueryProcessor</returns>
		public static DataAccessConfiguration GetInstance()
		{
			if( instance == null )
			{
				instance = new DataAccessConfiguration();
			}

			return instance;
		}
	#endregion //End of Initialization

	#region Accessors 

		/// <summary>
		/// Accessor for xmlTempPath
		/// </summary>
		public String XmlTempPath
		{
			get
			{
				return xmlTempPath;
			}
			set
			{
				xmlTempPath = value;
			}
		}
		/// <summary>
		/// Accessor for xmlTempPathHttp
		/// </summary>
		public String XmlTempPathHttp
		{
			get
			{
				return xmlTempPathHttp;
			}
			set
			{
				xmlTempPathHttp = value;
			}
		}

		/// <summary>
		/// Accessor for applicationMaxRecords
		/// </summary>
		public int ApplicationMaxRecords
		{
			get
			{
				return applicationMaxRecords;
			}
			set
			{
				applicationMaxRecords = value;
			}
		}

		/// <summary>
		/// Accessor for dataBlockSize.
		/// </summary>
		public int DataBlockSize
		{
			get
			{
				return dataBlockSize;
			}
			set
			{
				this.dataBlockSize = value;
			}
		}

		/// <summary>
		/// Accessor for dataPageSize
		/// </summary>
		public int DataPageSize
		{
			get
			{
				return dataPageSize;
			}
			set
			{
				dataPageSize = value;
			}
		}

		/// <summary>
		/// Accessor for turboModeFlag
		/// </summary>
		public bool TurboModeFlag
		{
			get
			{
				return turboModeFlag;
			}
			set
			{
				turboModeFlag = value;
			}
		}
		
		/// <summary>
		/// Accessor for turboModeMaxRecords
		/// </summary>
		public int TurboModeMaxRecords
		{
			get
			{
				return turboModeMaxRecords;
			}
			set
			{
				turboModeMaxRecords = value;
			}
		}

		/// <summary>
		/// Gets or sets the user name for clean up.
		/// </summary>
		public String CleanUpUser
		{
			get
			{
				return cleanUpUser;
			}
			set
			{
				cleanUpUser = value;
			}
		}

		/// <summary>
		/// Gets or sets the host for clean up.
		/// </summary>
		public String CleanUpHost
		{
			get
			{
				return cleanUpHost;
			}
			set
			{
				cleanUpHost = value;
			}
		}

		/// <summary>
		/// Gets or sets the passwort for the user.
		/// </summary>
		public String CleanUpPwd
		{
			get
			{
				return cleanUpPwd;
			}
			set
			{
				cleanUpPwd = value;
			}
		}

		/// <summary>
		/// Gets or sets the time interval for clean up in seconds.
		/// </summary>
		public int TimeInSeconds
		{
			get
			{
				return timeInSeconds;
			}
			set
			{
				timeInSeconds = value;
			}
		}

		/// <summary>
		/// Gets or sets the time for the master clean up.
		/// </summary>
		public String ClearAllTime
		{
			get
			{
				return clearAllTime;
			}
			set
			{
				clearAllTime = value;
			}
		}

	#endregion //End of Accessors

	#region Methods

		/// <summary>
		/// Reads the configuration.
		/// </summary>
		/// <remarks>
		/// A data access config processor ist created, which implements the
		/// IConfigReader interface.
		/// </remarks>
		public void ReadConfiguration()
		{
			ReadConfiguration(true);
		}
		
		/// <summary>
		/// Reads the configuration.
		/// </summary>
		/// <remarks>
		/// A data access config processor ist created, which implements the
		/// IConfigReader interface.
		/// </remarks>
		public void ReadConfiguration(bool pIsWebApplication)
		{
			// read the configuration
			DataAccessConfigProcessor processor = new DataAccessConfigProcessor();
			
			ConfigReader cr = ConfigReader.GetInstance();
			if(!pIsWebApplication) 
			{
				// have to set base directory
				cr.ApplicationRootPath = GetApplicationPath();
			}
			ConfigReader.GetInstance().ReadConfig(XML_NODE_DATAACCESS, processor);

			// Do some setting switches, if necessary.
			performAdditionalTasks();
		}

		/// <summary>
		/// Performs some additional configuration tasks.
		/// </summary>
		/// <example>
		/// If the turbo mode is set, the value of turboModeMaxRecords overwrites
		/// the value of dataBlockSize.
		/// </example>
		private void performAdditionalTasks()
		{
			// If the turbo mode is set, the value of turboModeMaxRecords overwrites
			// the value of dataBlockSize.
			if ( turboModeFlag )
			{
				dataBlockSize = turboModeMaxRecords;
			}

			
			// more things to come...
		}

		/// <summary>
		/// Gets the query definition to the given query name.
		/// </summary>
		/// <param name="queryName">The name of the requested query.</param>
		/// <returns>A reference to the given query, if the query name is found in the queries otherwise null.</returns>
		public QueryDefinition GetQueryDefForName(String queryName)
		{
			QueryDefinition query = null;
			QueryDefinition clone = null;

			if ( queries.ContainsKey(queryName) )
			{
				query = (QueryDefinition)queries[queryName];
				clone = (QueryDefinition)query.Clone();
			}

			return clone;
		}

		/// <summary>
		/// Adds a new query definition to the collection of queries.
		/// </summary>
		/// <param name="queryDef">Reference to the query definition object.</param>
		/// <remarks>
		/// If the collection of queries contains already the given id, the reference
		/// to the query object is not added to the collection.
		/// </remarks>
		public void AddQueryDefinition(QueryDefinition queryDef)
		{
			// Add only if the query not yet exists.
			if ( !queries.ContainsKey(queryDef.QueryName) )
			{
				queryDef.MakeTypedDataSetTemplate();
				queries.Add(queryDef.QueryName, queryDef);
			}
		}

		/// <summary>
		/// Returns the base directory for the application. Usually this is the
		/// directory where the .exe file of the application is stored.
		/// </summary>
		/// <returns>The application base directory</returns>
		/// <remarks>
		/// The XML configuration file for the application is stored in a subdirectory
		/// named "Configuration" below the application root directory. The root directory
		/// differs if the application is started from Visual Studio .NET (it's the
		/// \bin\Debug or \bin\Release directory. This method strips the "\bin..." part of
		/// the application root directory so that the position of the "Configuration"
		/// directory can be the same for production and development environment
		/// </remarks>
		private string GetApplicationPath()
		{
			// get the application path
			string appPath = Application.StartupPath;

			// if running under Visual Studio, application path is extended by ...\bin\debug (or release)
			// because the executable is stored there.
			int pos = appPath.IndexOf("bin");

			// if running in production, the executable is stored in the base directory.
			if(pos >= 0) 
			{
				// pos - 1 to strip the trailing "\"
				appPath = appPath.Substring(0, pos - 1);
			}
			return appPath;
		}

		#endregion
	}
}
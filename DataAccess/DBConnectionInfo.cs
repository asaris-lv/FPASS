using System;

namespace de.pta.Component.DataAccess.Internal
{
	/// <summary>
	/// Summary description for DBConnection.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> A.Opierzynski, PTA GmbH
	/// <b>Date:</b> Apr/02/2003
	///	<b>Remarks:</b> None
	///	<b>Changes:</b>
	///	<b>Date:</b> Aug/20/2003
	///	<b>Author:</b> A. Seibt, PTA GmbH
	///	<b>Remarks:</b> Access to Oracle Database added.
	/// </pre>
	/// </remarks>
	internal class DBConnectionInfo
	{
	#region Members

		public const String TYPE_ORACLE		= "ORACLE";
		public const String TYPE_SQL			= "SQL";
		public const String TYPE_MDX			= "MDX";

		private String connectString;
		private String serverName;
		private String database;
		private String defaultUser;
		private String domain;
		private String defaultPassword;
		private String connectionId;

	#endregion //End of Members

	#region  Constructors

		/// <summary>
		/// Constructs the object.
		/// </summary>
		public DBConnectionInfo()
		{
			initialize();
		}

	#endregion //End of Constructors

	#region Initialization

		private void initialize()
		{
			// Initializes the members.
			connectString	= "";
			serverName		= "";
			database		= "";
			defaultUser		= "";
			domain			= "";
			defaultPassword	= "";
			connectionId	= "";
		}

	#endregion //End of Initialization

	#region Accessors 

		/// <summary>
		/// Accessor for ConnectString.
		/// </summary>
		public String ConnectString
		{
			get
			{
				return connectString;
			}
			set
			{
				connectString = value;
			}
		}

		/// <summary>
		/// Accessor for serverName.
		/// </summary>
		public String ServerName
		{
			get
			{
				return serverName;
			}
			set
			{
				serverName = value;
			}
		}

		/// <summary>
		/// Accessor for database.
		/// </summary>
		public String Database
		{
			get
			{
				return database;
			}
			set
			{
				database = value;
			}
		}

		/// <summary>
		/// Accessor for defaultUser.
		/// </summary>
		public String DefaultUser
		{
			get
			{
				return defaultUser;
			}
			set
			{
				defaultUser = value;
			}
		}

		/// <summary>
		/// Accessor for domain.
		/// </summary>
		public String Domain
		{
			get
			{
				return domain;
			}
			set
			{
				domain = value;
			}
		}

		/// <summary>
		/// Accessor for defaultPassword.
		/// </summary>
		public String DefaultPassword
		{
			get
			{
				return defaultPassword;
			}
			set
			{
				defaultPassword = value;
			}
		}

		/// <summary>
		/// Accessor for connectionId.
		/// </summary>
		public String ConnectionId
		{
			get
			{
				return connectionId;
			}
			set
			{
				connectionId = value;
			}
		}

	#endregion //End of Accessors

	#region Methods

		/// <summary>
		/// Builds a connect string from the given information, if the
		/// default connect string is empty.
		/// </summary>
		public void BuildConnectString()
		{
			// Build connect string only, when it is empty.
			if ( connectString.Trim().Equals(String.Empty) )
			{
				if ( connectionId.Equals(TYPE_MDX) )
				{
					// MDX
					buildMdxConnectString();
				}
				else if ( connectionId.Equals(TYPE_SQL) )
				{
					// SQL
					buildSqlConnectString();
				}
				else if ( connectionId.Equals(TYPE_ORACLE) )
				{
					// SQL
					buildOracleConnectString();
				}
				else
				{
					// wrong conection type.
				}
			}
		}

		private void buildMdxConnectString()
		{
			connectString =   "provider=msolap;Data Source=" + ServerName 
							+ ";initial catalog=" + Database + ";";
		}

		private void buildSqlConnectString()
		{
			connectString =   "Persist Security Info=False;User ID=" + DefaultUser
							+ ";Password=" + DefaultPassword
							+ ";Initial Catalog=" + Database
							+ ";Data Source=" + ServerName +";";
		}

		private void buildOracleConnectString()
		{
			connectString = "user id=" + DefaultUser + ";data source=" + ServerName + ";password=" + DefaultPassword;
		}

	#endregion

	}
}

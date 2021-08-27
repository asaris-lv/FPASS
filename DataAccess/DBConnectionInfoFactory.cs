using System;
using System.Collections;

namespace de.pta.Component.DataAccess.Internal
{
	/// <summary>
	/// Factory for Database dbConnectionInfos.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> A.Opierzynski, PTA GmbH
	/// <b>Date:</b> Apr/02/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class DBConnectionInfoFactory
	{
	#region Members

		private static DBConnectionInfoFactory instance = null;
		private Hashtable dbConnectionInfos;

	#endregion //End of Members

	#region  Constructors
	
		protected DBConnectionInfoFactory()
		{
			// Private constructor assures that no further instance is created.
			initialize();
		}

	#endregion //End of Constructors

	#region  Initialization

		private void initialize()
		{
			//initialization of connection list
			dbConnectionInfos = new Hashtable();
		}

		/// <summary>
		/// Singleton - returns exactly one instance of that class
		/// </summary>
		/// <returns>Instance of a DBConnectionInfoFactory</returns>
		public static DBConnectionInfoFactory GetInstance()
		{
			if( null == instance )
			{
				instance = new DBConnectionInfoFactory();
			}

			return instance;
		}
	#endregion //End of Initialization

	#region Accessors 
	#endregion //End of Accessors

	#region Methods

		/// <summary>
		/// Registration of a new connection object in DBConnectionInfoFactory.
		/// </summary>
		/// <param name="newDBConnectionInfo">connection object to register</param>
		/// <param name="newConnectionInfoID">ID of connection object to register</param>
		public void RegisterDBConnectionInfo(DBConnectionInfo newDBConnectionInfo, 
								  			 String newConnectionInfoID) 
		{
			// Add new connection object to list, if it doesn't already exists.
			if ( !dbConnectionInfos.ContainsKey(newConnectionInfoID) )
			{
				newDBConnectionInfo.BuildConnectString();
				dbConnectionInfos.Add(newConnectionInfoID,newDBConnectionInfo);
			}
		}

		/// <summary>
		/// Returns a database connection object for specified ID
		/// </summary>
		/// <param name="connectionInfoID">ID to specific database connection.</param>
		/// <returns>database connection object</returns>
		public DBConnectionInfo GetDBConnectionInfo(String connectionInfoID) 
		{
			DBConnectionInfo dbConnectionInfo = (DBConnectionInfo)dbConnectionInfos[connectionInfoID];

			// Error - database connection object for specified ID not found
			if ( null == dbConnectionInfo )
			{
				throw new DataAccessException("ERROR_DATAACCESS_DBCONNECTION_UNREGISTERED");
			}

			return dbConnectionInfo;
		}

		/// <summary>
		/// Gets the connect string for a connection.
		/// </summary>
		/// <param name="connectionInfoID">ID to specific database connection.</param>
		/// <returns>
		/// A string, that contains the connect string for the data source. If the 
		/// requested connection does not exist an empty string is returned.
		/// </returns>
		public String GetConnectString(String connectionInfoID)
		{
			String connectString = String.Empty;
			DBConnectionInfo dbConnectionInfo = null;

			// Ensure, requested connection exists.
			if ( dbConnectionInfos.ContainsKey(connectionInfoID) )
			{
				// Get the DBConnectionInfo object.
				dbConnectionInfo = (DBConnectionInfo)dbConnectionInfos[connectionInfoID];

				// copy connect string.
				connectString = dbConnectionInfo.ConnectString;
			}

			return connectString;
		}

		/// <summary>
		/// Gets the user name for a connection.
		/// </summary>
		/// <param name="connectionInfoID">ID to specific database connection.</param>
		/// <returns>
		/// A string containing the user name. If the requested connection does
		/// not exist an empty string is returned.
		/// </returns>
		public String GetUser(String connectionInfoID)
		{
			String user = String.Empty;
			DBConnectionInfo dbConnectionInfo = null;

			// Ensure, requested connection exists.
			if ( dbConnectionInfos.ContainsKey(connectionInfoID) )
			{
				// Get the DBConnectionInfo object.
				dbConnectionInfo = (DBConnectionInfo)dbConnectionInfos[connectionInfoID];

				// copy string.
				user = dbConnectionInfo.DefaultUser;
			}

			return user;
		}

		/// <summary>
		/// Gets the domain name.
		/// </summary>
		/// <param name="connectionInfoID">ID to specific database connection.</param>
		/// <returns>
		/// A string containing the domain. If the requested connection does
		/// not exist an empty string is returned.
		/// </returns>
		public String GetDomain(String connectionInfoID)
		{
			String domain = String.Empty;
			DBConnectionInfo dbConnectionInfo = null;

			// Ensure, requested connection exists.
			if ( dbConnectionInfos.ContainsKey(connectionInfoID) )
			{
				// Get the DBConnectionInfo object.
				dbConnectionInfo = (DBConnectionInfo)dbConnectionInfos[connectionInfoID];

				// copy string.
				domain = dbConnectionInfo.Domain;
			}

			return domain;
		}

		/// <summary>
		/// Gets the passwort.
		/// </summary>
		/// <param name="connectionInfoID">ID to specific database connection.</param>
		/// <returns>
		/// A string containing the password. If the requested connection does
		/// not exist an empty string is returned.
		/// </returns>
		public String GetPassword(String connectionInfoID)
		{
			String pwd = String.Empty;
			DBConnectionInfo dbConnectionInfo = null;

			// Ensure, requested connection exists.
			if ( dbConnectionInfos.ContainsKey(connectionInfoID) )
			{
				// Get the DBConnectionInfo object.
				dbConnectionInfo = (DBConnectionInfo)dbConnectionInfos[connectionInfoID];

				// copy string.
				pwd = dbConnectionInfo.DefaultPassword;
			}

			return pwd;
		}

	#endregion
	}
}


using System;

using de.pta.Component.DbAccess.Enumerations;
using de.pta.Component.Logging.Log4NetWrapper;
using de.pta.Component.DbAccess.Exceptions;
using de.pta.Component.DbAccess.Internal.Configuration.XML;

namespace de.pta.Component.DbAccess.Internal.Configuration
{
	/// <summary>
	/// This class creates an instance of a concrete configuration factory class.
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
	/// <description>Aug/24/2003</description>
	/// </item>
	/// <item>
	/// <term><b>Remarks:</b></term>
	/// <description>initial version</description>
	/// </item>
	/// </list>
	/// </remarks>
	internal class ConfigFactory
	{
		#region Members

		/// <summary>
		/// Provides logging functionality.
		/// </summary>
		private static Logger mLog = new Logger(typeof(ConfigFactory));

		#endregion //End of Members

		#region Constructors
		#endregion //End of Constructors

		#region Initialization
		#endregion //End of Initialization

		#region Accessors 
		#endregion //End of Accessors

		#region Methods 


		/// <summary>
		/// Returns the concrete Factory to read configurations.
		/// </summary>
		/// <param name="pType">The type of configuration to be read.</param>
		/// <returns></returns>
		public static IConfigFactory GetFactory(DbAccessConfigType pType) 
		{
			switch(pType) 
			{
				case DbAccessConfigType.Xml:
					mLog.Info("Using XML configuration file");
					return new XmlConfigFactory();
				case DbAccessConfigType.RdbSqlServer:
					mLog.Info("Using configuration stored in SQL-Server database");
					throw new InvalidConfigFactoryException();
				case DbAccessConfigType.RdbOracle:
					mLog.Info("Using configuration stored in Oracle database");
					throw new InvalidConfigFactoryException();
				default:
					mLog.Fatal("Invalid configuration type");
					throw new InvalidConfigFactoryException();
			}
		}

		#endregion // End of Methods
	}
}

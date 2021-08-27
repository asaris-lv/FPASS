using System;

namespace de.pta.Component.DbAccess.Enumerations
{
	/// <summary>
	/// All possible types of configuration.
	/// </summary>
	public enum DbAccessConfigType
	{
		/// <summary>The configuration is stored in a XML file</summary>
		Xml,
		/// <summary>The configuration is stored in a SQLServer database</summary>
		RdbSqlServer,
		/// <summary>The configuration is stored in a Oracle database</summary>
		RdbOracle,
		/// <summary>The configuration is stored in a database</summary>
		RdbOleDb
	}
}

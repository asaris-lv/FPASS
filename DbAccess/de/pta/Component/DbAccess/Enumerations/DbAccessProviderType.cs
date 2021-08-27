using System;

namespace de.pta.Component.DbAccess.Enumerations
{
	/// <summary>
	/// Enumerates the types of data providers supported by the component.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> A. Seibt, PTA GmbH
	/// <b>Date:</b> 22.08.2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	public enum DbAccessProviderType
	{
		/// <summary>
		/// Provider type not initialized.
		/// </summary>
		None,
		/// <summary>
		/// The OLE DB.NET data provider (<see cref="System.Data.OleDb"/>).
		/// </summary>
		OleDb,
		/// <summary>
		/// The SQL Server.NET data provider (<see cref="System.Data.SqlClient"/>).
		/// </summary>
		SqlClient,
		/// <summary>
		/// The Oracle.NET data provider (<see cref="System.Data.OracleClient"/>).
		/// </summary>
		Oracle,
		/// <summary>
		/// The Oracle ODP.NET data provider.
		/// </summary>
		OracleOdp
	}
}

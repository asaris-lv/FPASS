using System;

namespace de.pta.Component.PdfGenerator.Internal.Configuration
{
	/// <summary>
	/// All available types of configuration sources.
	/// Actually only XML configuration is implemented.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> M. Mehlem, PTA GmbH
	/// <b>Date:</b> Aug/28/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	public enum ConfigurationTypes
	{
		#region Members

		/// <summary>The configuration is stored in a XML file</summary>
		Xml,
		/// <summary>The configuration is stored in a SQLServer database - not yet implemented</summary>
		RdbSqlServer,
		/// <summary>The configuration is stored in a Oracle database - not yet implemented</summary>
		RdbOracle

		#endregion //End of Members
	}
}

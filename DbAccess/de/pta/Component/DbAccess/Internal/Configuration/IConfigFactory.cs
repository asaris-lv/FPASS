using System;

namespace de.pta.Component.DbAccess.Internal.Configuration
{
	/// <summary>
	/// All classes which reads configurations for the DbAccess component must implement this
	/// interface.
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
	internal interface IConfigFactory
	{
		/// <summary>
		/// Returns the configuration object for the component.
		/// </summary>
		/// <returns></returns>
		DbAccessConfiguration GetConfiguration();
	}
}

using System;

namespace de.pta.Component.DbAccess.Exceptions
{
	/// <summary>
	/// This exception is thrown, if the configuration data for a specific data adapter
	/// or command is missing (e.g. invalid identifier passed to the create method).
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
	/// <description>Sep/02/2003</description>
	/// </item>
	/// <item>
	/// <term><b>Remarks:</b></term>
	/// <description>initial version</description>
	/// </item>
	/// </list>
	/// </remarks>
	public class ConfigNotFoundException : DbAccessException
	{
		/// <summary>
		/// Standard constructor.
		/// </summary>
		public ConfigNotFoundException() : base()
		{
		}

		/// <summary>
		/// Instantiating while defining a error message.
		/// </summary>
		/// <param name="message">message associated with the exception</param>
		public ConfigNotFoundException(string message) : base(message)
		{
		}

		/// <summary>
		/// Instantiating with a defined message a associated inner exception.
		/// </summary>
		/// <param name="message">message associated with the exception</param>
		/// <param name="innerException">exception which caused this exception</param>
		public ConfigNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
		}

	}
}

using System;

using de.pta.Component.Errorhandling;

namespace de.pta.Component.DbAccess.Exceptions
{
	/// <summary>
	/// This exception is thrown, if an error occured in the DbAccess component.
	/// It is the base class for all exceptions defined for the component.
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
	public class DbAccessException : BaseComponentException
	{
		/// <summary>
		/// Standard constructor.
		/// </summary>
		public DbAccessException() : base()
		{
		}

		/// <summary>
		/// Instantiating while defining a error message.
		/// </summary>
		/// <param name="message">message associated with the exception</param>
		public DbAccessException(string message) : base(message)
		{
		}

		/// <summary>
		/// Instantiating with a defined message a associated inner exception.
		/// </summary>
		/// <param name="message">message associated with the exception</param>
		/// <param name="innerException">exception which caused this exception</param>
		public DbAccessException(string message, Exception innerException) : base(message, innerException)
		{
		}

	}
}

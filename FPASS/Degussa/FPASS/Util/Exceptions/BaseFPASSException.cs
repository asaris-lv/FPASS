using System;

using de.pta.Component.Errorhandling;

namespace Degussa.FPASS.Util.Exceptions
{

	/// <summary>
	/// This is the base class for all exceptions of the FPASS application.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b>PTA GmbH
	/// <b>Date:</b> Aug/29/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	public abstract class BaseFPASSException : BaseApplicationException
	{


		/// <summary>
		/// Standard constructor.
		/// </summary>
		public BaseFPASSException() : base() 
		{
		}

		/// <summary>
		/// Instantiating while defining a error message.
		/// </summary>
		/// <param name="message">message associated with the exception</param>
		public BaseFPASSException(string message) : base(message)
		{
		}

		/// <summary>
		/// Instantiating with a defined message a associated inner exception.
		/// </summary>
		/// <param name="message">message associated with the exception</param>
		/// <param name="innerException">exception which caused this exception</param>
		public BaseFPASSException(string message, Exception innerException) : base(message, innerException)
		{
		}

		/// <summary>
		/// Every Exception will result in one of the from 
		/// <see cref="de.pta.Component.ErrorHandling.BaseUIException"><code>BaseUIException</code></see>
		/// derived Exceptions. These will be processed by a Error Publisher.
		/// <seealso cref="de.pta.Component.ErrorHandling.UIErrorException"><code>UIErrorException</code></see>
		/// <seealso cref="de.pta.Component.ErrorHandling.UIWarningException"><code>UIWarningException</code></see>
		/// <seealso cref="de.pta.Component.ErrorHandling.UIFatalException"><code>UIFatalException</code></see>
		/// <seealso cref="de.pta.Component.ErrorHandling.IPublisher"><code>IPublisher</code></see>
		/// </summary>
		/// <returns>An instance of a <code>UIxxxException</code></returns>
		public abstract BaseUIException GetUIException();

	}
}
	
	

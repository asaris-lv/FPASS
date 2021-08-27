using System;
using de.pta.Component.Errorhandling;

namespace de.pta.Component.DataAccess.Internal
{
	/// <summary>
	/// Exception class for the logging comonent.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> A.Opierzynski, PTA GmbH
	/// <b>Date:</b> Apr/02/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class DataAccessException : BaseComponentException
	{
		#region Members
		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public DataAccessException() : base()
		{
		}

		/// <summary>
		/// Instantiating while defining a error message.
		/// </summary>
		/// <param name="message">message associated with the exception</param>
		public DataAccessException(String message) : base(message)
		{
		}

		/// <summary>
		/// Instantiating with a defined message a associated inner exception.
		/// </summary>
		/// <param name="message">message associated with the exception</param>
		/// <param name="innerException">exception which caused this exception</param>
		public DataAccessException(
			String message, 
			Exception innerException) 
			: base(message, innerException)
		{
		}

		#endregion //End of Constructors

		#region Initialization
		#endregion //End of Initialization

		#region Accessors 
		#endregion //End of Accessors

		#region Methods
		#endregion // Methods

	}
}

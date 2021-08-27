using System;


namespace de.pta.Component.Errorhandling
{

	/// <summary>
	/// An exception which will be displayed as a fatal on the UI.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> D.Hassloecher, PTA GmbH
	/// <b>Date:</b> Apr/02/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	public class UIFatalException : BaseUIException
	{
		#region Members
		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public UIFatalException() : base()
		{
			UIDelegate = UIFatalDelegate.GetInstance();
		}

		/// <summary>
		/// Instantiating while defining a error message.
		/// </summary>
		/// <param name="message">message associated with the exception</param>
		public UIFatalException(string message) : base(message)
		{
			UIDelegate = UIFatalDelegate.GetInstance();
		}

		#endregion //End of Constructors

		#region Initialization
		#endregion //End of Initialization

		#region Accessors 
		#endregion //End of Accessors

		#region Methods

		/// <summary>
		/// Instantiating with a defined message a associated inner exception.
		/// </summary>
		/// <param name="message">message associated with the exception</param>
		/// <param name="innerException">exception which caused this exception</param>
		public UIFatalException(string message, Exception innerException) : base(message, innerException)
		{
			UIDelegate = UIFatalDelegate.GetInstance();
		}

		#endregion // Methods
	}
}
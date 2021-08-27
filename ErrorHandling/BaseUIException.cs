using System;

namespace de.pta.Component.Errorhandling
{
	/// <summary>
	/// For all displayed exception this should be used as super class.
	/// </summary>
	/// <remarks>This class should never be used for throwing since it
	/// just contains the abstract property for delegating of the
	/// ui processing. Concrete delegators are defined in the
	/// derived classes. </remarks>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> D.Hassloecher, PTA GmbH
	/// <b>Date:</b> Apr/02/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	public class BaseUIException :  BaseApplicationException
	{
		#region Members

		private UIExceptionDelegate uiDelegate;	

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public BaseUIException() : base()
		{
			uiDelegate = null;
		}

		/// <summary>
		/// Instantiating while defining a error message.
		/// </summary>
		/// <param name="message">message associated with the exception</param>
		public BaseUIException(string message) : base(message)
		{
			uiDelegate = null;
		}

		/// <summary>
		/// Instantiating with a defined message a associated inner exception.
		/// </summary>
		/// <param name="message">message associated with the exception</param>
		/// <param name="innerException">exception which caused this exception</param>
		//public BaseUIException(string message, Exception innerException) : base(innerException is BaseApplicationException ? ((BaseApplicationException)innerException).MessageWithParam(innerException.Message):message, innerException)
		public BaseUIException(string message, Exception innerException) : base(message, innerException)
		{
			uiDelegate = null;
		}

		#endregion //End of Constructors

		#region Initialization
		#endregion //End of Initialization

		#region Accessors 

		/// <summary>
		/// Associated delegation class for publishing the exception to the ui.
		/// </summary>
		public UIExceptionDelegate UIDelegate 
		{
			get 
			{
				return uiDelegate;
			}
			set
			{
				uiDelegate = value;
			}
		}

		#endregion //End of Accessors

		#region Methods
		#endregion //Methods
		
	}
}
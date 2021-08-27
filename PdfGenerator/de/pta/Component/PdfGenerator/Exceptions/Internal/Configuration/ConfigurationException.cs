using System;
using de.pta.Component.Errorhandling;

namespace de.pta.Component.PdfGenerator.Exceptions.Internal.Configuration
{
	/// <summary>
	/// This exception is thrown, if the internal configuration of the PdfGenerator component
	/// from an XML configuration file fails.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> M. Mehlem, PTA GmbH
	/// <b>Date:</b> Aug/28/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class ConfigurationException : BaseComponentException
	{
		#region Members
		#endregion //End of Members

		#region Constructors
	
		/// <summary>
		/// Standard constructor.
		/// </summary>
		public ConfigurationException() : base()
		{
		}

		/// <summary>
		/// Instantiating while defining a error message.
		/// </summary>
		/// <param name="pMessage">message associated with the exception</param>
		public ConfigurationException(String pMessage) : base(pMessage)
		{
		}

		/// <summary>
		/// Instantiating with a defined message a associated inner exception.
		/// </summary>
		/// <param name="pMessage">message associated with the exception</param>
		/// <param name="pInnerException">exception which caused this exception</param>
		public ConfigurationException(String pMessage, Exception pInnerException) : base(pMessage, pInnerException)
		{
		}

		#endregion //End of Constructors

		#region Initialization
		#endregion //End of Initialization

		#region Accessors 
		#endregion //End of Accessors

		#region Methods
		#endregion //Methods
	}
}

using System;
using de.pta.Component.Errorhandling;

namespace de.pta.Component.PdfGenerator.Exceptions.Internal
{
	/// <summary>
	/// This exception is thrown, if the internal transforming process 
	/// - merging formatting information and reorganising data - of an XML data document 
	/// (file or stream) fails.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> M. Mehlem, PTA GmbH
	/// <b>Date:</b> Aug/25/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class TransformerException : BaseComponentException
	{
		#region Members
		#endregion //End of Members

		#region Constructors
	
		/// <summary>
		/// Standard constructor.
		/// </summary>
		public TransformerException() : base()
		{
		}

		/// <summary>
		/// Instantiating while defining a error message.
		/// </summary>
		/// <param name="pMessage">message associated with the exception</param>
		public TransformerException(String pMessage) : base(pMessage)
		{
		}

		/// <summary>
		/// Instantiating with a defined message a associated inner exception.
		/// </summary>
		/// <param name="pMessage">message associated with the exception</param>
		/// <param name="pInnerException">exception which caused this exception</param>
		public TransformerException(String pMessage, Exception pInnerException) : base(pMessage, pInnerException)
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

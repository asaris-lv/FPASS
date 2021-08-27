using System;
using de.pta.Component.Errorhandling;

namespace de.pta.Component.PdfGenerator.Exceptions
{
	/// <summary>
	/// This exception is thrown, if the generating process of a PDF document (file or stream) fails.
	/// This exception might hold an internal exception coming from the internal configuration, from the 
	/// internal transforming or from the internal generating process.
	/// Each internal process has its own internal exception that is not visibel outside the library of
	/// this PdfGenerator component. To enable access to the own message of the inner exception 
	/// XML2PDFException provides a public method <c>InnerMessage()</c> returning the original message of
	/// the inner exception, or <c>null</c> if no inner exception exists.
	/// A type check of the inner exception types can be made using the string constants GENERATOR_EXCEPTION,
	/// TRANSFORMER_EXCEPTION or CONFIGURATION_EXCEPTION of XML2PDFException that can be checked if equal to
	/// the type of the inner exception.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> M. Mehlem, PTA GmbH
	/// <b>Date:</b> Aug/25/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	public class XML2PDFException : BaseComponentException
	{
		#region Members

		/// <summary>The full path of the GeneratorException type to enable external type check.</summary>
		public static string GENERATOR_EXCEPTION		= new de.pta.Component.PdfGenerator.Exceptions.Internal.GeneratorException().GetType().ToString();
		/// <summary>The full path of the TransformerException type to enable external type check.</summary>
		public static string TRANSFORMER_EXCEPTION		= new de.pta.Component.PdfGenerator.Exceptions.Internal.TransformerException().GetType().ToString();
		/// <summary>The full path of the ConfigurationException type to enable external type check.</summary>
		public static string CONFIGURATION_EXCEPTION	= new de.pta.Component.PdfGenerator.Exceptions.Internal.Configuration.ConfigurationException().GetType().ToString();

		#endregion //End of Members

		#region Constructors
		/// <summary>
		/// Standard constructor.
		/// </summary>
		public XML2PDFException() : base()
		{
		}

		/// <summary>
		/// Instantiating while defining a error message.
		/// </summary>
		/// <param name="pMessage">message associated with the exception</param>
		public XML2PDFException(string pMessage) : base(pMessage)
		{
		}

		/// <summary>
		/// Instantiating with a defined message a associated inner exception.
		/// </summary>
		/// <param name="pMessage">message associated with the exception</param>
		/// <param name="pInnerException">exception which caused this exception</param>
		public XML2PDFException(string pMessage, Exception pInnerException) : base(pMessage, pInnerException)
		{
		}
		#endregion //End of Constructors

		#region Initialization
		#endregion //End of Initialization

		#region Accessors 
		#endregion //End of Accessors

		#region Methods

		/// <summary>
		/// Checks whether an inner exception exists and returns the message of the inner exception, or
		/// null if no inner exception exists.
		/// </summary>
		/// <returns>A string holding the message of the inner exception if an inner 
		/// exception exists, or null if the inner exception is null.</returns>
		public string InnerMessage()
		{
			return (this.InnerException != null) ? this.InnerException.Message : null;
		}

		#endregion //Methods
	}
}

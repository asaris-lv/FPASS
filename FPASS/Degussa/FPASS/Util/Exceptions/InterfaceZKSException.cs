using System;
using de.pta.Component.Errorhandling;

namespace Degussa.FPASS.Util.Exceptions
{
	/// <summary>
	/// Summary description for InterfaceZKSException.
	/// </summary>
	/// <remarks>
	/// <para><b>History</b></para>
	/// <div class="tablediv">
	/// <table class="dtTABLE" cellspacing="0">
	///		<tr>
	///			<th width="20%">Author</th>
	///			<th width="20%">Date</th>
	///			<th width="60%">Remarks</th>
	///		</tr>
	///		<tr>
	///			<td width="20%">Author</td>
	///			<td width="20%">month/day/year</td>
	///			<td width="60%">Comment</td>
	///		</tr>
	/// </table>
	/// </div>
	/// </remarks>
	public class InterfaceZKSException : BaseFPASSException
	{
		#region Members

		/// <summary>An id for a mesage read from a resource file.</summary>
		private int mMessageId;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public InterfaceZKSException() : base()
		{
			initialize();
		}

		/// <summary>
		/// Instantiiating an exception with a message id.
		/// </summary>
		/// <param name="pMessageId">The id of the message to show</param>
		public InterfaceZKSException(int pMessageId) : base()
		{
			mMessageId = pMessageId;
		}

		/// <summary>
		/// Instantiating while defining a error message.
		/// </summary>
		/// <param name="message">message associated with the exception</param>
		public InterfaceZKSException(string message) : base(message)
		{
		}

		/// <summary>
		/// Instantiating an exception with a mesage id and a default message text
		/// shown if the message id is not found.
		/// </summary>
		/// <param name="pMessageId">The id of the mesage to show</param>
		/// <param name="message">message associated with the exception</param>
		public InterfaceZKSException(int pMessageId, string pMessageText) : base(pMessageText)
		{
			mMessageId = pMessageId;
		}

		/// <summary>
		/// Instantiating with a defined message a associated inner exception.
		/// </summary>
		/// <param name="message">message associated with the exception</param>
		/// <param name="innerException">exception which caused this exception</param>
		public InterfaceZKSException(string message, Exception innerException) : base(message, innerException)
		{
		}
		/// <summary>
		/// Instantiating an exception with a mesage id and a default message text
		/// shown if the message id is not found and an associated inner exception.
		/// </summary>
		/// <param name="message">message associated with the exception</param>
		/// <param name="innerException">exception which caused this exception</param>
		public InterfaceZKSException(int pMessageId, string pMessageText, Exception pInnerException) : base(pMessageText, pInnerException)
		{
			mMessageId = pMessageId;
		}


		public override BaseUIException GetUIException() 
		{

			return null;
		}		

		#endregion //End of Constructors

		#region Initialization

		/// <summary>
		/// Initializes the members.
		/// </summary>
		private void initialize()
		{
		}	

		#endregion //End of Initialization

		#region Accessors 

		#endregion //End of Accessors

		#region Methods 

		#endregion // End of Methods


	}
}

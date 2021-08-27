using System;
using System.Collections;

using de.pta.Component.Errorhandling;

namespace Degussa.FPASS.Util.ErrorHandling
{
	/// <summary>
    /// Base class to implement the IPublisher interface 
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
	public abstract class BaseUIPublisher : UIExceptionDelegate, IPublisher
	{
		#region Members

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor
		/// </summary>
		public BaseUIPublisher() : base()
		{
		}

		#endregion //End of Constructors

		#region Initialization
		#endregion //End of Initialization

		#region Accessors 
		#endregion //End of Accessors

		#region Methods 

		public ArrayList NestedExceptionMsg(BaseUIException pException)
		{
			ArrayList messages = new ArrayList();
			messages.Add(pException.Message);
			if(null !=pException.InnerException)
			{
				messages.Add(this.GetMessage(pException));
				TranslateExceptionMsg(pException.InnerException, messages);
			}
			return messages;
		}

		private void TranslateExceptionMsg(Exception pException, ArrayList pMessages)
		{			
			if (pException is BaseApplicationException)
			{
				BaseApplicationException e = (BaseApplicationException)pException;
				pMessages.Add(this.GetMessage(e));
			}
			else
			{
				string msg = pException.Message + "\n" + pException.StackTrace;
				pMessages.Add(msg);
			}
			if(null != pException.InnerException)
			{
				TranslateExceptionMsg(pException.InnerException, pMessages);
			}
		}

		#endregion // End of Methods

		#region IPublisher Members

		/// <summary>
		/// Publishes the exception to the UI.
		/// </summary>
		public abstract override void Publish(BaseUIException exception);

		#endregion
	}
}

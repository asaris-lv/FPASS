using System;
using System.Collections;
using System.Windows.Forms;
using de.pta.Component.Logging.Log4NetWrapper;
using de.pta.Component.Errorhandling;

namespace Degussa.FPASS.Util.ErrorHandling
{
	/// <summary>
	/// UIErrorPublisher implements the method Publish, 
	/// that knows how to publish an Errormessage
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
	///			<td width="20%">A. Seibt, PTA GmbH</td>
	///			<td width="20%">Sep/22/2003</td>
	///			<td width="60%">initial version</td>
	///		</tr>
	/// </table>
	/// </div>
	/// </remarks>
	public class UIErrorPublisher : BaseUIPublisher
	{
		#region Members

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor
		/// </summary>
		public UIErrorPublisher() : base()
		{
		}

		#endregion //End of Constructors

		#region Initialization
		#endregion //End of Initialization

		#region Accessors 
		#endregion //End of Accessors

		#region Methods

		/// <summary>
		/// Knows how to publish a warning to the UI.
		/// </summary>
		public override void Publish(BaseUIException pException) 
		{
			ArrayList messages = this.NestedExceptionMsg(pException);
			messages.Add(pException.Message);
			LogMessages(messages);
			MessageBox.Show((string)messages[0], "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			Logger log = LoggingSingleton.GetInstance().Log;
			log.Fatal("StackTrace: " + pException.StackTrace);
		}


		private void LogMessages(ArrayList pMessages)
		{
			Logger log = LoggingSingleton.GetInstance().Log;
			for(int i = 0; i < pMessages.Count; i++)
			{
				log.Error((string)pMessages[i]);
			}
		}

		#endregion // Methods

	}
}

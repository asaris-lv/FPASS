using System;
using System.Collections;
using System.Windows.Forms;
using de.pta.Component.Logging.Log4NetWrapper;
using de.pta.Component.Errorhandling;

namespace Degussa.FPASS.Util.ErrorHandling
{
	/// <summary>
	/// UIWarningPublisher implements the method Publish, 
	/// that knows how to publish a warning
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
	public class UIWarningPublisher : BaseUIPublisher
	{
		#region Members

		private StatusBar mStatusBar = null;

		#endregion //End of Members

		#region Constructors

		public UIWarningPublisher() : base()
		{
			// Not public since outside no one is allowed to instantiate.
		}

		#endregion //End of Constructors

		#region Initialization


		#endregion //End of Initialization

		#region Accessors 
		#endregion //End of Accessors

		#region Methods
		public void SetStatusBar(StatusBar pstb)
		{
			this.mStatusBar = pstb;
		}


		/// <summary>
		/// Knows how to publish error warning to the UI.
		/// </summary>
		public override void Publish(BaseUIException pException) 
		{
			ArrayList messages = this.NestedExceptionMsg(pException);
			messages.Add(pException.Message);
			LogMessages(messages);
			MessageBox.Show((string)messages[0], "Warning", 
				MessageBoxButtons.OK, MessageBoxIcon.Information);
		}


		private void LogMessages(ArrayList pMessages)
		{
			Logger log = LoggingSingleton.GetInstance().Log;
			for(int i = 0; i < pMessages.Count; i++)
			{
				log.Warn((string)pMessages[i]);
			}
		}

		#endregion // Methods

	}
}
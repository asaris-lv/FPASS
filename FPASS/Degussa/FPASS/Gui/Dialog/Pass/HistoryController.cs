using System;
using System.Windows.Forms;

using de.pta.Component.Errorhandling;

using Degussa.FPASS.Gui;
using Degussa.FPASS.Gui.Dialog.Pass;
using Degussa.FPASS.FPASSApplication;
using Degussa.FPASS.Util.Exceptions;
using Degussa.FPASS.Util.Messages;

namespace Degussa.FPASS.Gui.Dialog.Pass
{
	/// <summary>
	/// A HistoryController is the controller of the MVC-triad HistoryModel,
	/// HistoryController and FrmHistory.
	/// HistoryController extends from the FPASSBaseController.
	/// </summary>
	/// <remarks>
	/// <para><b>History</b></para>
	/// <div class="tablediv">
	/// <table class="dtTABLE" cellspacing="0">
	///		<tr>
	///			<th width="20%">PTA GmbH</th>
	///			<th width="20%">12/01/2003</th>
	///			<th width="60%">Remarks</th>
	///		</tr>
	/// </table>
	/// </div>
	/// </remarks>
	public class HistoryController : FPASSBaseController
	{
		#region Members

		private		HistoryModel  mHistoryModel;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public HistoryController()
		{
			initialize();
		}

		#endregion //End of Constructors

		#region Initialization

		/// <summary>
		/// Initializes the members.
		/// </summary>
		private void initialize()
		{
			mDialogId = AllFPASSDialogs.HISTORY_DIALOG;
			mView = new FrmHistory();
			mView.RegisterController(this);

			mModel = new HistoryModel();
			mModel.registerView(mView);

			mHistoryModel = (HistoryModel)mModel;
		}	

		#endregion //End of Initialization

		#region Accessors 

		#endregion //End of Accessors

		#region Methods 


		internal override void HandleEventShowHelp() 
		{
			Help.ShowHelp(this.mView, 
						Application.StartupPath + AllFPASSDialogs.FPASS_MASTER_HELPFILE, 
						HelpNavigator.Topic,
						AllFPASSDialogs.HELPTOPIC_HISTORY);
		}

		/// <summary>
		/// Excute search for History records
		/// 03.03.2004: Cannot search for strings with ' in as this is part of SQL string delimiter: catch ORA exception
		/// </summary>
		internal void HandleEventBtnSearchHistory() 
		{
			try 
			{
				try
				{
					((HistoryModel) mModel).ValidateDateSearchCriteria();
					((HistoryModel) mModel).ClearStatusBar();
					((FrmHistory) mView).Cursor = System.Windows.Forms.Cursors.WaitCursor;
					mHistoryModel.GetHistory();
					((FrmHistory) mView).Cursor = System.Windows.Forms.Cursors.Default;
				}
				catch (System.Data.OracleClient.OracleException oraex)
				{
					// Error from SQL ' delimiter
					if ( oraex.Code == 01756 )
					{
						throw new UIWarningException (MessageSingleton.GetInstance().GetMessage
							(MessageSingleton.CANNOT_SEARCH_CHAR));
					}
					else
					{
						throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(
							MessageSingleton.FATAL_DB_ERROR) 
							+ oraex.Message );
					}
				}
			} 
			catch ( UIWarningException uwe ) 
			{
				((FrmHistory) mView).Cursor = System.Windows.Forms.Cursors.Default;
				ExceptionProcessor.GetInstance().Process(uwe);
			}
		}

		#endregion // End of Methods

	}
}

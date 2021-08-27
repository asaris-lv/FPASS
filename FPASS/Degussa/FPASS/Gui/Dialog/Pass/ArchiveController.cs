using System;
using System.Windows.Forms;

using de.pta.Component.Errorhandling;

using Degussa.FPASS.Util.Exceptions;
using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.Gui;
using Degussa.FPASS.FPASSApplication;

namespace Degussa.FPASS.Gui.Dialog.Pass
{
	/// <summary>
	/// An ArchiveController is the controller of the MVC-triad ArchiveModel,
	/// ArchiveController and FrmArchive.
	/// ArchiveController extends from the FPASSBaseController.
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
	public class ArchiveController : FPASSBaseController
	{
		#region Members

		/// <summary>
		/// used to hold the model of this triad. hold for convenience to avoid casting
		/// </summary>
		private	ArchiveModel  mArchiveModel;
	
		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public ArchiveController()
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
			mDialogId = AllFPASSDialogs.ARCHIVE_DIALOG;
			mView = new FrmArchive();
			mView.RegisterController(this);

			mModel = new ArchiveModel();
			mModel.registerView(mView);

			mArchiveModel = (ArchiveModel)mModel;
			
		}	

		#endregion //End of Initialization

		#region Accessors 

		#endregion //End of Accessors

		#region Methods 

		internal override void HandleEventShowHelp() 
		{
			Help.ShowHelp( this.mView, 
				Application.StartupPath + AllFPASSDialogs.FPASS_MASTER_HELPFILE, 
				HelpNavigator.Topic,
				AllFPASSDialogs.HELPTOPIC_ARCHIVE );
		}

		private void ClearSbMessages()
		{
			((ArchiveModel)mModel).ClearStatusBar();
		}

		internal void HandleEventOpenProcessDialog(decimal pCurrentFFMAId) 
		{			
			this.ClearSbMessages();
			CoWorkerController coWorkerController = (CoWorkerController)FPASSControllSingleton.
				GetInstance().GetDialog(AllFPASSDialogs.COWORKER_ARCHIVE_DIALOG);
			coWorkerController.FillArchiveLists();
			coWorkerController.SetCurrentFFMA(pCurrentFFMAId);
			coWorkerController.LockCoWorkerDialog();
			coWorkerController.SetTitle("Archivdaten eines FFMA");
			FPASSControllSingleton.GetInstance().ShowModalDialog(this, coWorkerController);
		}

		/// <summary>
		/// Handles button "Suchen" in the form
		/// 03.03.2004: Cannot search for strings with ' in as this is part of SQL string delimiter: catch ORA exception
		/// </summary>
		internal void HandleEventBtnSearchCoWorker() 
		{
			this.ClearSbMessages();
			
			try 
			{
				try
				{
					// Get the coworkers from archive that meet the search criteria
					mArchiveModel.GetCoWorkers();
				}
				catch (System.Data.OracleClient.OracleException oraex)
				{
					// Error from SQL ' delimiter
					if ( oraex.Code == 01756 )
					{
						((FrmArchive) mView).Cursor = System.Windows.Forms.Cursors.Default;
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
				((FrmArchive)mView).BtnDetails.Enabled = false;
				((FrmArchive) mView).Cursor = System.Windows.Forms.Cursors.Default;
				 ExceptionProcessor.GetInstance().Process(uwe);
				((ArchiveModel)mModel).ShowMessageInStatusBar( uwe.Message );
			}
		}

		/// <summary>
		/// New 02.03.2004:
		/// Empties search fields and datagrid.
		/// </summary>
		internal void HandleEventClearForm()
		{
			mArchiveModel.ClearFields();
		}

		#endregion // End of Methods

	}
}

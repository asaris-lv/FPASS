using System;
using System.Windows.Forms;

using de.pta.Component.Errorhandling;

using Degussa.FPASS.Gui;
using Degussa.FPASS.FPASSApplication;
using Degussa.FPASS.Util.Exceptions;
using Degussa.FPASS.Util.Messages;


namespace Degussa.FPASS.Gui.Dialog.Pass
{
	/// <summary>
	/// An ExternalContractorSearchController is the controller 
	/// of the MVC-triad ExternalContractorSearchModel,
	/// ExternalContractorSearchController and FrmExternalSearchContractor.
	/// ExternalContractorSearchController extends from the FPASSBaseController.
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
	public class ExternalContractorSearchController : FPASSBaseController
	{
		#region Members

		private		ExternalContractorSearchModel  mContractorSearchModel;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public ExternalContractorSearchController()
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
			mDialogId = AllFPASSDialogs.SEARCH_EXTERNAL_CONTRACTOR_DIALOG;
			mView = new FrmSearchExternalContractor();
			mView.RegisterController(this);

			mModel = new ExternalContractorSearchModel();
			mModel.registerView(mView);

			mContractorSearchModel = (ExternalContractorSearchModel)mModel;
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
				AllFPASSDialogs.HELPTOPIC_EXCONTR_SEARCH );
		}

		
		/// <summary>
		/// Handle "Suchen" button click: get the external contractor/s data from DB
		/// If no results, make sure buttons at foot of form stay disabled
		/// 03.03.2004: Cannot search for strings with ' in as this is part of SQL string delimiter: catch ORA exception
		/// </summary>
		internal void HandleEventBtnSearchExContractor() 
		{
			try 
			{
				try
				{
					mContractorSearchModel.GetExContractor();
				}
				catch (System.Data.OracleClient.OracleException oraex)
				{
					// Error from SQL ' delimiter
					if ( oraex.Code == 01756 )
					{
						((FrmSearchExternalContractor) mView).Cursor = Cursors.Default;
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
				((FrmSearchExternalContractor) mView).BtnAssume.Enabled = false;			
				ExceptionProcessor.GetInstance().Process(uwe);
			}
		}

		
		/// <summary>
		/// Give selected external contractor back to controller of view from which the external contractor search was opened
		/// </summary>
		/// <param name="pContractorID">ID of selected coworker</param>
		internal void HandleEventGiveBackContractor(String pContractorID) 
		{
			mParent.SetSelectedContractorID(pContractorID);
			this.HandleCloseDialog();
			
		}

		#endregion // End of Methods

	}
}

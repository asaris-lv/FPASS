using System;
using System.Windows.Forms;
using System.Data;

using Degussa.FPASS.Db.DataSets;
using Degussa.FPASS.Gui;
using Degussa.FPASS.Gui.Dialog.Pass;
using Degussa.FPASS.FPASSApplication;
using Degussa.FPASS.Util.Exceptions;
using Degussa.FPASS.Util.Messages;

using de.pta.Component.Errorhandling;

namespace Degussa.FPASS.Gui.Dialog.SmartAct
{
    /// <summary>
	/// Logic for notifying FPASS of CWR with new id cards from SmartAct 
	/// IdCardsController extends from the FPASSBaseController.
	/// </summary>
	/// <remarks>
	/// <para><b>History</b></para>
	/// <div class="tablediv">
	/// <table class="dtTABLE" cellspacing="0">
	///		<tr>
	///			<th width="20%">PTA GmbH</th>
	///			<th width="20%">12/02/2015</th>
	///			<th width="60%">Remarks</th>
	///		</tr>
	/// </table>
	/// </div>
	/// </remarks>
	public class IdCardsController : FPASSBaseController
	{
		#region Members
	
        /// <summary>
        /// used to hold the model of this triad. hold for convenience to avoid casting
        /// </summary>
        private IdCardsModel mIdCardsModel;

		#endregion 

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
        public IdCardsController()
		{
			initialize();
		}

		#endregion //End of Constructors

		#region Initialization

		/// <summary>
		/// Initializes the members.
		/// Register this controller with the view and model
		/// The USerControls sit on the main form, each has its own typified Dataset, these are instantiated from here
		/// </summary>
		private void initialize()
		{
            mDialogId = AllFPASSDialogs.POPUP_COWORKER_IDCARD_DIALOG;
            mView = new FrmIdCardsPopup();
            mView.RegisterController(this);

            mModel = new IdCardsModel();
            mModel.registerView(mView);

            mIdCardsModel = (IdCardsModel)mModel;
		}	

		#endregion //End of Initialization


		#region Accessors 

		#endregion //End of Accessors

		#region Methods 

        /// <summary>
        /// Offers a way of re-triggering some method in the Model, 
        /// for example re-executing a query before the View is shown
        /// </summary>
        /// <returns></returns>
        internal override void RequeryModel()
        {
            mIdCardsModel.GetNewCoworkersForZKS();
        }

        /// <summary>
        /// Can be used to implement some condition and show dialog (the View instance)
        /// only when this condition is true
        /// </summary>
        /// <returns></returns>
        internal override bool ConditionIsMet()
        {
            return mIdCardsModel.HasCoWorkers;
        }

        /// <summary>
        /// Event so that user can reload all coworkers in table on demand
        /// </summary>
        internal void HandleEventUserRefresh()
        {
            try
            { 
                mView.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                mIdCardsModel.ReloadCoworkerTable();
            }
            catch (UIWarningException uwe)
            {
                ExceptionProcessor.GetInstance().Process(uwe);
            }
            catch (UIErrorException ure)
            {
                ExceptionProcessor.GetInstance().Process(ure);
            }
            mView.Cursor = System.Windows.Forms.Cursors.Default;

        }
        
        /// <summary>
		/// Handles closing-events of all dialogs in FPASS.
		/// Forces an controlled close by calling the Close()-Method 
		/// of the current triad if it's an uncontrolled close. 
		/// An uncontrolled close of a form can be fired by a user-click 
		/// on the exit-button on the top-right of a window.
		/// Can't do this reliably: not used 26.11.03
		/// </summary>
		internal void HandleEventHide() 
		{
            this.Hide();
		}

		
		#endregion  // Methods

        #region Show_Help

		/// <summary>
		/// Override base method
		/// Call help topic specific to Administration
		/// </summary>
		internal override void HandleEventShowHelp() 
		{
            Help.ShowHelp(this.mView,
                Application.StartupPath + AllFPASSDialogs.FPASS_MASTER_HELPFILE,
                HelpNavigator.Topic,
                AllFPASSDialogs.HELPTOPIC_SMARTACT_CWR);
		}

		#endregion 
	}
}

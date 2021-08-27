using System;
using System.Collections;
using System.Data;
using System.Globalization;


using de.pta.Component.DbAccess;
using de.pta.Component.ListOfValues;
using de.pta.Component.DbAccess.Exceptions;
using de.pta.Component.Errorhandling;
using Degussa.FPASS.Gui;
using Degussa.FPASS.Db;
using Degussa.FPASS.Util.UserManagement;
using System.Collections.Generic;
using Degussa.FPASS.Util.Messages;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;
using Degussa.FPASS.Bo.Pass;
using Degussa.FPASS.InterfaceZks;
using Degussa.FPASS.FPASSApplication;
using Degussa.FPASS.Util;
using de.pta.Component.Logging.Log4NetWrapper;


namespace Degussa.FPASS.Util.SmartAct
{
	/// <summary>
	/// Logic for notifying FPASS of CWR with new id cards from SmartAct 
	/// This class is for management of the background task management
	/// </summary>
	/// <remarks>
	/// <para><b>History</b></para>
	/// <div class="tablediv">
	/// <table class="dtTABLE" cellspacing="0">
	///		<tr>
	///			<th width="20%">PTA GmbH</th>
	///			<th width="20%">18/02/2015</th>
	///			<th width="60%">Remarks</th>
	///		</tr>
	/// </table>
	/// </div>
	/// </remarks>
	public class IdCardPoller
	{
		#region Members
	
        /// <summary>
        /// Calling controller instance
        /// </summary>
        private AbstractController mController;

        /// <summary>
        /// BackgroundWorker instance
        /// </summary>
        private BackgroundWorker mBgWorker;

        /// <summary>
        /// Sleep interval for background process
        /// </summary>
        private Int32 mSleepInterval;

        /// <summary>
        /// for MsgBox
        /// </summary>
        protected const string TitleMessage = "FPASS";

		#endregion 


		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
        public IdCardPoller()
		{
			initialize();
		}

		#endregion 

		#region Initialization

		/// <summary>
		/// Initializes the members.
		/// The Model, View and Controller instances are instantiated here but the View is not shown yet, 
        /// this is the part that is scheduled. 
        /// View is only shown if the DB query (select coworkers) gives results.
		/// </summary>
		private void initialize()
		{         
            // SummaryCoworker is the calling dialog, this is why it is loaded here
            // Calling dialogue is then required to show PopIdCard as Modal
            mController = FPASSControllSingleton.GetInstance().GetDialog(AllFPASSDialogs.SUMMARY_COWORKER_DIALOG);

            // Get Interval from FPASS parameters. Measured in milliseconds.
            mSleepInterval = 300000;

            try
            {
                mSleepInterval = Convert.ToInt32(Globals.GetInstance().SmartActIdCardInterval);
            }
            catch
            {
                Globals.GetInstance().Log.Error("Parameter SmartActIdCardInterval falsch: nicht vom Datentyp int32 ." + this.ToString());
                Globals.GetInstance().Log.Error("Maximaler erlaubter Wert Int32 liegt bei 2.147.483.647; hexadecimal 0x7FFFFFFF.");
                Globals.GetInstance().Log.Error("FPASS macht mit Default-Wert weiter." + mSleepInterval);
            }
		}	

		#endregion

		#region Accessors 

        /// <summary>
        /// returns true if background task is running
        /// </summary>
        internal bool IsTaskRunning
        {
            get
            {
                return (mBgWorker != null && mBgWorker.IsBusy);
            }
        }

		#endregion 

		#region Methods 

        /// <summary>
        /// Starts the background task for showing the PopupIdCard dialog at regular intervals.
        /// </summary>
        internal void RunBackgroundTask()
        {
            StartBackgroundTask(false);
        }


        /// <summary>
        /// Cancels the background task for showing the PopupIdCard dialog at regular intervals.
        /// </summary>
        internal void CancelBackgroundTask()
        {
            // Cancel the background task.
            // First check if taks is not running, otherwise exception
            if (IsTaskRunning)
                mBgWorker.CancelAsync();
        }


        /// <summary>
        /// Starts the background task for showing the PopupIdCard dialog at regular intervals.
        /// Accepts cancellation arguments
        /// </summary>
        /// <param name="pError"></param>
        private void StartBackgroundTask(bool pError)
        {
            // Gets logger
            Logger log = Globals.GetInstance().Log;


            mBgWorker = new BackgroundWorker();
            //mBgWorker.WorkerReportsProgress = true;
            mBgWorker.WorkerSupportsCancellation = true;

            log.Warn("Hintergrundprozess zur autm. Benachrichtigung über FFMA mit neuem Ausweis wird gestartet...");

            mBgWorker.DoWork += (sender, args) =>
            {
                // This is the actual work the background process does
                // Executes SQL to check for new CWR. If any are there then show dialog.
                // Throttle of 1000 iterations in loop
                for (int i = 0; i != 1000; ++i)
                {
                    // Check for cancellation.
                    if (mBgWorker.CancellationPending)
                    {
                        args.Cancel = true;
                        return;
                    }

                    // Give things a chance to start
                    Thread.Sleep(3000);

                    // Condition has to be true, in this case CWR found.
                    FPASSControllSingleton.GetInstance().ShowModalDialogCondition(mController, AllFPASSDialogs.POPUP_COWORKER_IDCARD_DIALOG);

                    // Sleep for interval in ms
                    Thread.Sleep(mSleepInterval);

                    if (pError)
                    {
                        throw new InvalidOperationException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.SMARTACT_NOTIFY_CANCEL)); 
                    }

                    // Dummy result
                    args.Result = "(OK)";
                } 

            };   // end goes to

      
            // This is registration of the event delegate: 
            // what to do when background process completed.
            mBgWorker.RunWorkerCompleted += (sender, args) =>
            {
                string cwrText = MessageSingleton.GetInstance().GetMessage(MessageSingleton.SMARTACT_NOTIFY_STOP);

                // Display results.
                if (args.Error != null)
                {
                    MessageBox.Show(cwrText + args.Error.ToString(), TitleMessage, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    log.Warn(cwrText + args.Error.ToString());
                }
                else if (args.Cancelled)
                    MessageBox.Show(cwrText, TitleMessage, MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show(cwrText + args.Result, TitleMessage, MessageBoxButtons.OK, MessageBoxIcon.Information);

                log.Warn(cwrText);

            };    // end goes to
         

            // Background task actually starts here: async
            mBgWorker.RunWorkerAsync();
         
        }


		#endregion 
	}
}

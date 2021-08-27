using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Collections.Generic;

using de.pta.Component.DbAccess;
using de.pta.Component.ListOfValues;
using de.pta.Component.DbAccess.Exceptions;
using de.pta.Component.Errorhandling;

using Degussa.FPASS.Db;
using Degussa.FPASS.Util.UserManagement;
using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.Bo.Pass;
using Degussa.FPASS.InterfaceZks;
using Degussa.FPASS.Gui;
using de.pta.Component.Logging.Log4NetWrapper;
using Degussa.FPASS.Util;
using Degussa.FPASS.Bo.Search;

namespace Degussa.FPASS.Gui.Dialog.SmartAct
{
	/// <summary>
	/// Logic for notifying FPASS of CWR with new id cards from SmartAct 
	/// IdCardsModel extends from the AbstractModel.
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
	public class IdCardsModel : FPASSBaseModel
	{
		#region Members
		
		/// <summary>
		/// Provider for database access
		/// </summary>
		private IProvider mProvider;

        private DataTable mTable;

        static string SELECT_COWORKER_ALL = "SelectZksAllCoWorkers";
        static string SELECT_COWORKER_ALL_PARAMETER = ":CWR_MND_ID";

        /// <summary>
        /// Shortcut to avoid casting: View from MVC triad
        /// </summary>
        private FrmIdCardsPopup mFrmIdCards;

        /// <summary>
        /// Holds list of cwr ids to be transferred to ZKS
        /// </summary>
        private List<string> mCoWorkerIds; 

		#endregion 

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
        public IdCardsModel()
		{
			initialize();
		}

		#endregion 

		#region Initialization

		/// <summary>
		/// Initializes the members as required.
        /// Do not get data yet.
		/// </summary>
		private void initialize()
		{      
            mCoWorkerIds = new List<string>(); 
            
            mProvider = DBSingleton.GetInstance().DataProvider;
            InitializeDataTable();

            mZKSProxy = new FpassZks();
		}	

		#endregion 


		#region Accessors 

        /// <summary>
        /// Returns true if model has >0 coworkers with new id card
        /// </summary>
        internal bool HasCoWorkers
        {
            get
            {
                return (mCoWorkerIds.Count > 0);
            }
        }


		#endregion 


        #region Methods
        
        
        /// <summary>
        /// PreShow called when MVC triad is loaded: 
        /// By this time data has been initialised (Requerid)
        /// </summary>
        internal override void PreShow()
        {
            // Set view instance. Cannot do it in initialize() as model does not yet know view
            mFrmIdCards = (FrmIdCardsPopup)mView;

            ClearStatusBar();

            // Data has already been loaded from DB, bind it to DataGrid in View now that it has been instantiated
            BindCoWorkersToGrid();
        }

        /// <summary>
        /// initialises DataTable used to show CWR in Gui
        /// </summary>
        private void InitializeDataTable()
        {
            mTable = new DataTable("RTTabCoWorker");
            mTable.Columns.Add(new DataColumn(CoWorkerSearch.ID_COL));
            mTable.Columns.Add(new DataColumn(CoWorkerSearch.SURNAME));
            mTable.Columns.Add(new DataColumn(CoWorkerSearch.FIRSTNAME));
            mTable.Columns.Add(new DataColumn(CoWorkerSearch.WINDOWS_ID));
            mTable.Columns.Add(new DataColumn(CoWorkerSearch.ID_CARD_HITAG));
            mTable.Columns.Add(new DataColumn(CoWorkerSearch.ID_CARD_MIFARE));
            mTable.Columns.Add(new DataColumn(CoWorkerSearch.VALID_UNTIL));
            mTable.Columns.Add(new DataColumn(CoWorkerSearch.EXCONTRACTOR));
            mTable.Columns.Add(new DataColumn(CoWorkerSearch.COORDINATOR));
        }

        /// <summary>
        /// Gets coworkers with at least one ID card (Mifare or Hitag) and ZKS returncode N.
        /// These are treated as "FFMA mit neuem Ausweis", i.e. they have just got their Id card from SDmartAct and are not yet in ZKS.
        /// Uses DataTable (Attributes of BOCoworker) to hold results
        /// </summary>
        internal void GetNewCoworkersForZKS()
        {
            // Gets logger
            Logger log = Globals.GetInstance().Log;

            // List of coworker BOs for display in GUI
            List<CoWorkerSearch> coWorkers = new List<CoWorkerSearch>();

            // empties list of cwr Ids to update
            // and existing Rows in DataTable in case previously filled
            mCoWorkerIds.Clear();
            mTable.Rows.Clear();

            DataRow row;
            object colWindId;
            object colHitag;
            object colMifare;
            
            // Id of CWR's coordinator
            decimal cwrCoordId;
            // ID of coordinator user
            decimal coordId = 0; 

            bool isRestricted = false;


            // if current user is a coordinator then only show his coworkers
            if (UserManagementControl.getInstance().CurrentUserIsInRole(UserManagementControl.ROLE_KOORDINATOR)
                && !UserManagementControl.getInstance().CurrentUserIsInRole(UserManagementControl.ROLE_EDVADMIN)
                && !UserManagementControl.getInstance().CurrentUserIsInRole(UserManagementControl.ROLE_VERWALTUNG))
            {
                isRestricted = true;
                coordId = UserManagementControl.getInstance().CurrentCoordinatorID;
            }


            try
            {
                // Give us some info              
                log.Warn("Führe die Suche nach FFMA mit neuem Ausweis aus.....");

                // Creates select command & fills Data Reader               
                IDbCommand selComm = mProvider.CreateCommand(SELECT_COWORKER_ALL);

                // Parameters for the command: current mandator id
                decimal mandatorId = Convert.ToDecimal(UserManagementControl.getInstance().CurrentMandatorID);
                mProvider.SetParameter(selComm, SELECT_COWORKER_ALL_PARAMETER, mandatorId);
                IDataReader mDR = mProvider.GetReader(selComm);
                
                // Loop thru records and create BOs, hold in List and display in Datatable
                while (mDR.Read())
                {
                    colWindId = mDR["CWR_WINDOWS_ID"];
                    colHitag = mDR["CWR_IDCARDNO"];
                    colMifare = mDR["CWR_MIFARENO"];
                    cwrCoordId = Convert.ToDecimal(mDR["VWC_ID"]);

                    if (!isRestricted || cwrCoordId == coordId)
                    {
                        mCoWorkerBO = new CoWorkerSearch();
                        row = mTable.NewRow();

                        mCoWorkerBO.CoWorkerId = Convert.ToDecimal(mDR["CWR_ID"]);
                        mCoWorkerBO.WindowsId = (colWindId.Equals(DBNull.Value) ? String.Empty : Convert.ToString(colWindId));
                        mCoWorkerBO.Surname = mDR["CWR_SURNAME"].ToString();
                        mCoWorkerBO.Firstname = mDR["CWR_FIRSTNAME"].ToString();
                        mCoWorkerBO.IdCardNumHitag = (colHitag.Equals(DBNull.Value) ? null : new Nullable<decimal>(Convert.ToDecimal(colHitag)));
                        mCoWorkerBO.IdCardNumMifare = (colMifare.Equals(DBNull.Value) ? null : new Nullable<decimal>(Convert.ToDecimal(colMifare)));
                        mCoWorkerBO.Status = mDR["CWR_STATUS"].ToString();
                        mCoWorkerBO.ExContractorName = mDR["EXCO_NAME"].ToString();
                        mCoWorkerBO.ValidUntil = mDR["CWR_VALIDUNTIL"].ToString();
                        mCoWorkerBO.CoordNameAndTel = mDR["VWC_BOTHNAMESTEL"].ToString();


                        // create a DataRow for each CoworkerBO
                        coWorkers.Add(mCoWorkerBO);
                        row.ItemArray = new object[9] {mCoWorkerBO.CoWorkerId,                        
                        mCoWorkerBO.Surname,
                        mCoWorkerBO.Firstname,
                        mCoWorkerBO.WindowsId,
                        mCoWorkerBO.IdCardNumHitag,
                        mCoWorkerBO.IdCardNumMifare,
                        mCoWorkerBO.ValidUntil,
                        mCoWorkerBO.ExContractorName,                      
                        mCoWorkerBO.CoordNameAndTel };

                        mTable.Rows.Add(row);

                        // Remember cwr Ids for Insert to ZKS later
                        mCoWorkerIds.Add(Convert.ToString(mDR["CWR_ID"]));
                    }

                }
                mDR.Close();

                log.Warn("Suche nach FFMA mit neuem Ausweis erfolgreich ausgeführt. " + mTable.Rows.Count + " Einträge gefunden.");
            }
            catch (Exception oraex)
            {
                // TODO Deal with this woth exception handling inside or outsid thread? 
                throw new UIFatalException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR) + oraex.Message);
            }
        }


        /// <summary>
        /// Offers a simple way to re-qery database on demand and reload all coworkers in table
        /// </summary>
        internal void ReloadCoworkerTable()
        {
            // Reloads table of CWR in GUI
            GetNewCoworkersForZKS();
            BindCoWorkersToGrid();
        }

        /// <summary>
        /// Exports all coworkers in Id list to ZKS
        /// </summary>
        internal override void ExportAllToZKS()
        {
            mZKSProxy.CoWorkerIdList = mCoWorkerIds;

            mFrmIdCards.Cursor = System.Windows.Forms.Cursors.WaitCursor;

            try
            {
                // Exports list of CWRs to ZKS
                base.ExportAllToZKS();

                // Reloads table of CWR in GUI
                ReloadCoworkerTable();
            }
            catch (UIWarningException uwe)
            {
                ExceptionProcessor.GetInstance().Process(uwe);
            }
            mFrmIdCards.Cursor = System.Windows.Forms.Cursors.Default;
        }

       

        /// <summary>
        /// Binds the CoWorker DataTable to the DataGrid in the View.
        /// </summary>
        private void BindCoWorkersToGrid()
        {
            mFrmIdCards.DgrCoWorker.DataSource = null;

            int rowCnt = mTable.Rows.Count;

            // If coworker records were returned
            if (rowCnt > 0)
            {
                // Enable buttons and bind datagrid in Form to datatable
                mFrmIdCards.DgrCoWorker.DataSource = mTable;
                ShowMessageInStatusBar("Meldung: " + rowCnt + " Fremdfirmenmitarbeiter gefunden");
            }
            else
            {
                // No rows returned so none can be selected
                //mFrmIdCards.CurrentRowIndex = -1;
                // Disable buttons and show message                    
                this.ShowMessageInStatusBar(MessageSingleton.GetInstance().GetMessage(MessageSingleton.NO_RESULTS));
            }
            mFrmIdCards.btnGetAll.Enabled = (rowCnt > 0);
            //mFrmIdCards.BtnGetSingle.Enabled = (rowCnt > 0);  
        }

        
		#endregion 

	}
}

using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

using de.pta.Component.Errorhandling;
using de.pta.Component.DbAccess;
using de.pta.Component.ListOfValues;

using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.Util.Exceptions;
using Degussa.FPASS.Util.UserManagement;
using Degussa.FPASS.Bo.Pass;
using Degussa.FPASS.Db;
using Degussa.FPASS.Util.Enums;
using Degussa.FPASS.Bo.Search;

namespace Degussa.FPASS.Gui.Dialog.Pass
{
	/// <summary>
	/// A SummaryModel is the model of the MVC-triad SummaryModel,
	/// SummaryController and FrmSummaryCoWorker.
	/// SummaryModel extends from the AbstractModel.
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
	public class SummaryModel : FPASSBaseModel
	{
		#region Members

		/// <summary>
		/// Names of database queries and their parameters
		/// </summary>
		private const string COWORKER_QUERY		= "SummaryCoWorker";
		private const string SURNAME_PARA		= ":CWR_SURNAME";
		private const string FIRSTNAME_PARA		= ":CWR_FIRSTNAME";
		private const string COORDNAME_PARA		= ":VWC_ID";
		private const string SUBCONTRACTOR_PARA	= ":SUBCON";
		private const string SUPERVISOR_PARA	= ":SUPERNAME";
		private const string CONTRACTOR_PARA	= ":EXTCON";

		/// <summary>
		/// Holds values of search parameters from gui
		/// </summary>
		private string mSurname;
		private string mFirstname;
		private string mCoordinatorId;
		private string mSubcontrName;
		private string mContractorId;
		private string mIdCardMifareNo = String.Empty;
        private string mIdCardHitagNo = String.Empty;
		private string mSupervisor;

		/// <summary>
		/// ArrayList is filled and held here 
		/// </summary>
		private ArrayList mCoworkerList;
		

        /// <summary>
        /// Current ID card type. Defaults to Hitag2
        /// </summary>
        private string mIDCardType;

        private FrmSummaryCoWorker mSummaryView;

		#endregion

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public SummaryModel()
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
            
		}	

		#endregion //End of Initialization

		#region Accessors 

		/// <summary>
		/// Simple getter and setter: can fill arraylist e.g. from extended search
		/// </summary>
		public ArrayList CoWorkerList
		{
			get 
			{
				return mCoworkerList;
			}
			set 
			{
				mCoworkerList = value;
			}
		}

        /// <summary>
        /// Current ID card type. Defaults to Hitag2
        /// </summary>
        public string IDCardType
        {
            get { return mIDCardType; }
            set { mIDCardType = value; }
        }


		#endregion //End of Accessors

		#region Methods 

        internal override void PreShow()
        {
            base.PreShow();
            mSummaryView = (FrmSummaryCoWorker)mView;
        }


		/// <summary>
		/// Binds datagrid in Summary form to datatable containing results (i.e. coworker data) from extended search.
		/// </summary>
		/// <param name="pTabResults">DataTable of coworker attributes</param>
		internal void SetResultsExtendedSearch(DataTable pTabResults) 
		{
			mSummaryView.DgrCoWorker.DataSource = null;
			
			if ( null != pTabResults )
			{
				mSummaryView.DgrCoWorker.DataSource = pTabResults;
				ShowMessageInStatusBar("Meldung: " + pTabResults.Rows.Count + " Fremdfirmenmitarbeiter gefunden.");
			}
			else
			{
				// Reset defaults
				mSummaryView.CurrentRowIndex = -1;
			}
		}


		/// <summary>
		/// Reads current id card number (Hitag or Mifare) from ZKS terminal.
		/// </summary>
		/// <exception cref="de.pta.Component.Errorhandling.UIWarningException">thrown if no connection to ZKS
		/// or ID cardno could not be read</exception>
		internal void SearchByIdCardZKS() 
		{
            mZKSProxy.IDCardType = mIDCardType;
            mZKSProxy.Connect();
			String id = mZKSProxy.ReadIdCardNo();
			mZKSProxy.Disconnect();

			if ( id.Equals(MessageSingleton.GetInstance().GetMessage(MessageSingleton.ZKS_NO_IDCARDNO) ) ) 
			{
				throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.ZKS_NO_IDCARDNO));
			}
			else if  (id.Equals(MessageSingleton.GetInstance().GetMessage(MessageSingleton.ZKS_NO_CONNECT) ) ) 
			{
				throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.ZKS_NO_CONNECT));
			}
			else 
			{
                if (mIDCardType == IDCardTypes.Mifare)
                    mIdCardMifareNo = id;
                else
                    mIdCardHitagNo = id;

				// Don't do coworker search
				// SearchCoWorkers();

                mSummaryView.TxtSearchIDCard.Text = id;
			}
		}


        /// <summary>
        /// Read current idcard number from usb (textfield) and search in FPASS for coworker with this IDCARDNO
        /// </summary>
        /// <exception cref="de.pta.Component.Errorhandling.UIWarningException">thrown if no connection to ZKS
        /// or ID cardno could not be read</exception>
        internal void SearchByIdCardUSB()
        {
            String id = mSummaryView.TxtSearchIDCard.Text;

            if (mIDCardType == IDCardTypes.Mifare)
                mIdCardMifareNo = id;
            else
                mIdCardHitagNo = id;

            // Does normal coworker search
            GetCoWorkerSummary();

            mIdCardHitagNo = String.Empty;
            mIdCardMifareNo = String.Empty;
        }


		/// <summary>
		/// Selects via SQL all CoWorkers which match the given search criteria 
		/// for each DB record a coworker value object is created and filled
		/// These are held in a Datatable which is bound to datagrid to allow sorting	
		/// Arraylist of coworker BOs was original implementation, kept for compatibility
		/// No warning exception if no results found as search can also be executed automatically
		/// 03.03.2004: Cannot search for strings with ' in as this is part of SQL string delimiter: ctach ORA exception
		/// </summary>
		public void GetCoWorkerSummary() 
		{
            mSummaryView.DgrCoWorker.DataSource = null;
			mCoworkerList = new ArrayList();

			// read the search criteria from the gui
			CopyOutSearchCriteria();

			try
			{
				// Get DataProvider from DbAccess component, create select command & fill Data Reader 
				IProvider mProvider = DBSingleton.GetInstance().DataProvider;
				IDbCommand selComm  = mProvider.CreateCommand(COWORKER_QUERY);

				selComm.CommandText = GenerateWhereClause( selComm.CommandText );
				IDataReader mDR = mProvider.GetReader(selComm);

				// Create a datatable at runtime: this is bound to datagrid to allow sorting			
				DataRow row;
                DataTable table = new DataTable("RTTabCoWorker");
                table.Columns.Add(new DataColumn(CoWorkerSearch.ID_COL));
                table.Columns.Add(new DataColumn(CoWorkerSearch.SURNAME));
                table.Columns.Add(new DataColumn(CoWorkerSearch.FIRSTNAME));
                table.Columns.Add(new DataColumn(CoWorkerSearch.DATE_OF_BIRTH, typeof(System.DateTime)));
                table.Columns.Add(new DataColumn(CoWorkerSearch.VALID_UNTIL));
                table.Columns.Add(new DataColumn(CoWorkerSearch.EXCONTRACTOR));
                table.Columns.Add(new DataColumn(CoWorkerSearch.SUPERVISOR));
                table.Columns.Add(new DataColumn(CoWorkerSearch.SUBCONTRACTOR));
                table.Columns.Add(new DataColumn(CoWorkerSearch.COORDINATOR));
                table.Columns.Add(new DataColumn(CoWorkerSearch.STATUS));
                table.Columns.Add(new DataColumn(CoWorkerSearch.ZKS_RET));
		
				// Loop thru records and create BOs, hold in arraylist and display in Datatable
				int i = 0;
				while (mDR.Read())
				{
					mCoWorkerBO	= new CoWorkerSearch();
					row = table.NewRow();

					mCoWorkerBO.CoWorkerId = Convert.ToDecimal(mDR["CWR_ID"]);
					mCoWorkerBO.Surname	= mDR["CWR_SURNAME"].ToString();
					mCoWorkerBO.Firstname = mDR["CWR_FIRSTNAME"].ToString();
					mCoWorkerBO.DateOfBirth	= Convert.ToDateTime(mDR["CWR_DATEOFBIRTH"]).Date.ToString("dd.MM.yyyy");
					mCoWorkerBO.ValidUntil = mDR["CWR_VALIDUNTIL"].ToString();
					mCoWorkerBO.ExContractorName = mDR["EXTCON"].ToString();
					mCoWorkerBO.Supervisor = mDR["SUPERNAME"].ToString();
					mCoWorkerBO.SupervisTel	= mDR["SUPERTEL"].ToString();
					mCoWorkerBO.SubContractor = mDR["SUBCON"].ToString();
					mCoWorkerBO.Coordinator	= mDR["VWC_SURNAME"].ToString();
					mCoWorkerBO.CoordTel = mDR["VWC_TEL"].ToString();
					mCoWorkerBO.Status	= mDR["CWR_STATUS"].ToString();
					mCoWorkerBO.ZKSReturncode = mDR["CWR_RETURNCODE_ZKS"].ToString();

					mCoWorkerBO.SuperNameAndTel = mCoWorkerBO.Supervisor + "  (Tel. " + mCoWorkerBO.SupervisTel + ")";

					mCoWorkerBO.CoordNameAndTel = mCoWorkerBO.Coordinator + "  (Tel. " + mCoWorkerBO.CoordTel + ")";
								
					mCoworkerList.Add(mCoWorkerBO);
					row.ItemArray = new object[11] {mCoWorkerBO.CoWorkerId,
													   mCoWorkerBO.Surname,
													   mCoWorkerBO.Firstname,
													   mCoWorkerBO.DateOfBirth,
													   mCoWorkerBO.ValidUntil,
													   mCoWorkerBO.ExContractorName,
													   mCoWorkerBO.SuperNameAndTel,		
													   mCoWorkerBO.SubContractor, 
													   mCoWorkerBO.CoordNameAndTel, 
													   mCoWorkerBO.Status,
													   mCoWorkerBO.ZKSReturncode};
					table.Rows.Add(row);
					i ++;

				}
				mDR.Close();

				// If coworker records were returned
				if ( mCoworkerList.Count > 0 ) 
				{
					// Enable buttons and bind datagrid in Form to datatable
                    mSummaryView.DgrCoWorker.DataSource = table;
					ShowMessageInStatusBar( "Meldung: " + mCoworkerList.Count + " Fremdfirmenmitarbeiter gefunden" );
				} 
				else 
				{
					// No rows returned so none can be selected
                    mSummaryView.CurrentRowIndex = -1;
					ShowMessageInStatusBar( MessageSingleton.GetInstance().GetMessage(MessageSingleton.NO_RESULTS) );
				}
                mSummaryView.BtnEdit.Enabled = (mCoworkerList.Count > 0);
                mSummaryView.BtnPass.Enabled = (mCoworkerList.Count > 0);
			}
			catch (System.Data.OracleClient.OracleException oraex)
			{
				// Error from SQL ' delimiter
				if ( oraex.Code == 01756 )
				{
					throw new UIWarningException (MessageSingleton.GetInstance().GetMessage(MessageSingleton.CANNOT_SEARCH_CHAR));
				}
				else
				{
					throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR) + oraex.Message );
				}
			}
		}

		
		/// <summary>
		/// Comboboxes for search: Load external contractors assigned to currently selected coordinator
		/// </summary>
		public void HandleAssignmentExContractorToCoordinator()
		{
			mCoordinatorId = this.GetSelectedIDFromCbo(mSummaryView.CboSearchCoordinator).Trim();

			ArrayList externalContractor = new ArrayList();
			externalContractor.Add(new LovItem("0", ""));
			externalContractor.AddRange(LovSingleton.GetInstance().GetSubList(null, "FPASS_EXCONTRACTOR", "EXCO_NAME", ""));			
			mSummaryView.CboSearchExternalContractor.DataSource = externalContractor;
			mSummaryView.CboSearchExternalContractor.DisplayMember = "ItemValue";
			mSummaryView.CboSearchExternalContractor.ValueMember = "Id";

		}


		/// <summary>
		/// Generate WHERE clause of SQL text according to which search criteria set
		/// 24.05.2004: add ORDER BY clause at very end of SQL statement
		/// </summary>
		/// <param name="pSelect">first part of SQL text</param>
		/// <returns>Complete statement text including WHERE clause</returns>
		private String GenerateWhereClause(String pSelect) 
		{
			String whereClause = "";

			whereClause = " WHERE CWR_MND_ID = " + UserManagementControl.getInstance().CurrentMandatorID;

			if (mIdCardHitagNo.Length > 0) 
			{
				whereClause = whereClause + " AND CWR_IDCARDNO = '" + this.mIdCardHitagNo + "' "; 
				//return pSelect + whereClause;
			}
            if (mIdCardMifareNo.Length > 0)
            {
                whereClause = whereClause + " AND CWR_MIFARENO = '" + this.mIdCardMifareNo + "' ";
                //return pSelect + whereClause;
            }


			if ( this.mCoordinatorId.Length > 0 ) 
			{
				whereClause = whereClause + " AND VWC_ID = " +  this.mCoordinatorId;
			}

			// The PK ID of the external contractor identifies the exco
			if ( this.mContractorId.Length > 0 ) 
			{
				whereClause = whereClause + " AND CWR_EXCO_ID = " + this.mContractorId; 
			}

			if ( this.mSurname.Length > 0 ) 
			{
				whereClause = whereClause + " AND UPPER (CWR_SURNAME) LIKE '" + this.mSurname.ToUpper().Trim() + "' "; 
			}

			if ( this.mFirstname.Length > 0 ) 
			{
				whereClause = whereClause + " AND UPPER (CWR_FIRSTNAME) LIKE '" + this.mFirstname.ToUpper().Trim() + "' "; 
			}

			// The PK ID of the external contractor identifies the supervisor (same DB table)
			if ( this.mSupervisor.Length > 0 ) 
			{
				whereClause = whereClause + " AND CWR_EXCO_ID = " + this.mSupervisor;
			}

			if ( this.mSubcontrName.Length > 0 ) 
			{
				whereClause = whereClause + " AND SUBCON = '" + this.mSubcontrName + "' "; 
			}

			// add ORDER BY clause at very end of SQL statement
			// NLS Sort German to get A, a, ...Z
			whereClause = whereClause + " ORDER BY NLSSORT(EXTCON, 'NLS_SORT=GERMAN'), " 
									  + "NLSSORT(CWR_SURNAME, 'NLS_SORT=GERMAN'), " 
									  + "NLSSORT(CWR_FIRSTNAME, 'NLS_SORT=GERMAN')";
	
			return pSelect + whereClause;

		}


		/// <summary>
		/// Reads the search parameter values from gui
		/// </summary>
		/// <exception cref="de.pta.Component.Errorhandling.UIWarningException">thrown if no search parameters filled</exception>
		private void CopyOutSearchCriteria() 
		{
			bool foundCriteria = false;
            string errMessage = "'Ausweisnummer'";
            mIdCardMifareNo = "";
            mIdCardHitagNo = "";


            // Make sure id card number is numeric
            String id = mSummaryView.TxtSearchIDCard.Text.Trim();
            decimal result;

            if (id.Length > 0 && !decimal.TryParse(id, out result))
            {
                // error if value in field is not numeric
                string msgNum = MessageSingleton.GetInstance().GetMessage(MessageSingleton.VALUE_NOT_NUMERIC);
                throw new UIWarningException(String.Format(msgNum, errMessage));
            }


            if (mSummaryView.RbtSearchMifare.Checked)
            {
                mIDCardType = IDCardTypes.Mifare;
                mIdCardMifareNo = id;
            }
            else
            {
                mIDCardType = IDCardTypes.Hitag2;
                mIdCardHitagNo = id;
            }

			mSurname = mSummaryView.TxtSearchSurname.Text.Trim().Replace("*", "%");
			mFirstname = mSummaryView.TxtSearchFirstname.Text.Trim().Replace("*", "%");			
			
            // Get selected IDs from comboboxes
            mCoordinatorId = this.GetSelectedIDFromCbo(mSummaryView.CboSearchCoordinator).Trim();			
			mContractorId = this.GetSelectedIDFromCbo(mSummaryView.CboSearchExternalContractor).Trim();
            mSubcontrName = this.GetSelectedValueFromCbo(mSummaryView.CboSearchSubcontractor);			
			mSupervisor = this.GetSelectedIDFromCbo( mSummaryView.CboSearchSupervisor );						
                      

            // See if search criteria found
            foundCriteria = (mSurname.Length > 0 || mFirstname.Length > 0 
                || mCoordinatorId.Length > 0 || mContractorId.Length > 0 
                || mSubcontrName.Length > 0 || mSupervisor.Length > 0 
                || mIdCardHitagNo.Length > 0 || mIdCardMifareNo.Length > 0);

           			
			if (!foundCriteria) 
			{
                // Throw error if no search criteria set
				mSummaryView.Cursor = System.Windows.Forms.Cursors.Default;
				throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.NO_SEARCH_CRITERIA));
			} 
		}

		#endregion // End of Methods

	}
}

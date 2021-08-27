using System;
using System.Collections;
using System.Data;
using System.Data.OracleClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using de.pta.Component.DbAccess;
using de.pta.Component.DbAccess.Exceptions;
using de.pta.Component.Errorhandling;
using de.pta.Component.ListOfValues;
using Degussa.FPASS.Bo.Administration;
using Degussa.FPASS.Bo.Pass;
using Degussa.FPASS.Bo.Reports;
using Degussa.FPASS.Bo.Search;
using Degussa.FPASS.Db;
using Degussa.FPASS.FPASSApplication;
using Degussa.FPASS.Gui;
using Degussa.FPASS.Reports.Util;
using Degussa.FPASS.Util;
using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.Util.UserManagement;
using Degussa.FPASS.Util.Validation;

namespace Degussa.FPASS.Reports
{
	/// <summary>
	/// A ReportsModel is the model of the MVC-triad FrmReport,
	/// ReportController and ReportModel.
	/// ReportsModel extends from the AbstractModel.
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
	public class ReportsModel : FPASSBaseModel
	{
		#region Members
	
		// this is is just used as a help: instance of current View 
		private FrmReport mReportView;

		// instance of DataProvider from de.pta.Component.DbAccess
		private IProvider mProvider;

		// current results type: do we show corokers, excontractors etc in reults grid
		private ReportNames.ResultTypes mResultsType;

		// Name of report query in Config.xml
		private string mQueryName = "";

		// flags whether user must select a cwr in results grid
		private bool mMustSelectCwr = false;

		//private  String mDateFrom;
		//private  String mDateUntil;
		private  bool mNoSearchCriteriaAllowed = false;

		// Where clause used for searches in GUI
		private string mWhereClause = "";

		// Order By in GUI
		private string mOrderBy = "";

		private  bool	mwhereSet	 = false;
		
		/// <summary>
		/// Holds search parameters from GUI
		/// </summary>
		private string mOrdernoParameter;
		private string mCraftParameter;
		private string mPlantParameter;
		private string mDepartmentParameter;
        private string mExContractorParameter; // Name of one or more excontractors
		private string mSupervisorParameter; // holds only the ID of the supervisor
		private string mSupervisorNameParameter; // holds the name of the supervisor
		private string mCoordinatorParameter;
		private string mSubContractorParameter;
		private string mSurnameParameter;
		private string mFirstnameParameter;
		private bool mAccessauthorizeYesParameter;
		private bool mAccessauthorizeNoParameter;
		private bool mStatusGueltigParameter;
		private bool mStatusUngueltigParameter;
		private string mDynamicDataFromParameter;
		private string mDynamicDataUntilParameter;
		private	string mDelDateFromParameter;
		private	string mDelDateUntilParameter;
		private	string mValidFromParameter;
		private	string mValidUntilParameter;
		private string mMantenanceFromPara;
		private string mMantenanceUntilPara;
		private string mMaskNoParameter;
		private bool mMaskDelivered;
		private bool mMaskReceived;
		private string mNoBookXDaysParameter;

        /// <summary>
        /// Name of file with list of excontractor names
        /// </summary>
        private string mFileName = Globals.GetInstance().ReportsBasePath + Globals.REPORTS_MYEXCO_FILE;

		// List of all excontractors selected in GUI checkboxlist
		private ArrayList mExContractorNames;

		private const string SEARCHCRITERIA_CWRID = "CoWorkerId";
		
		// Query to get Plantmanager and plants
		private const string PLANTMANAGER_QUERY	= "PlantManager";

		// Checklist query
		private const string CHECKLIST_QUERY = "CwrCheckList";
		
		// Query to get RespMask 
		private const string RESPMASK_QUERY	= "CWRRespmaskReports";
		
		// Query to get ExContractors and coordinators
		private const string EXCONTRACTOR_QUERY	= "ExContractorReports";
		
		private const string CON_STATUS_OK = "Die Erstellung des Reports war erfolgreich.";
		private const string CON_STATUS_ERROR = " Die Erstellung des Reports wurde abgebrochen.";
		private const string CON_STATUS_WAIT = "Der Report wird erstellt - bitte haben Sie einen Moment Geduld";

		#endregion

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public ReportsModel()
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
        /// Gets or sets name of file with external contractor list
        /// </summary>
        public string FileName
        {
            get { return mFileName; }
            set { mFileName = value; }
        }

		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// On closing the mask the default values are restored
		/// </summary>
		internal override void PreClose()
		{
			((FrmReport) mView).CboReport.Text = null;
			((FrmReport) mView).SbpMessage.Text = String.Empty;
			ClearFields();
			mResultsType = 0;
			SetResultsGridVisible();
		}
	
		/// <summary>
		/// Checks that date values entered by user are valid.
		/// Makes sure "no booking since x days" is numeric.
		/// For Attendance reports: cannot select more than a 366 day period
		/// </summary>	
		internal void CheckSearchCriteria() 
		{
			mReportView = (FrmReport) mView;
            CheckAttendDates();
            CheckDeliveryDates(mReportView.TxtDeliveryDateFrom.Text.Trim(), mReportView.TxtDeliveryDateUntil.Text.Trim());
            CheckDeliveryDates(mReportView.TxtValidityFrom.Text.Trim(), mReportView.TxtValidityUntil.Text.Trim());
            CheckDeliveryDates(mReportView.TxtMaintenanceFrom.Text.Trim(), mReportView.TxtMaintenanceUntil.Text.Trim());        
 
			if (mReportView.DatMonthFrom.Value.AddDays(366) < mReportView.DatMonthUntil.Value)
			{
				throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.REPORT_ATTENDANCE_PERIOD) );
			}

            if (mReportName == ReportNames.CWR_BOOKINGS_EXCO)
            {
                if (mReportView.CobExternalContractor.Text == "")
                {
                    throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.REPORT_NO_EXCONTRACTOR));
                }
            }
			else if (mReportName == ReportNames.CWR_EXPIRYDATE)
			{
                double result;
                string tryBookDays = ((FrmReport)mView).txtXDays.Text;

                if (tryBookDays.Length > 0 &&
                !double.TryParse(tryBookDays, NumberStyles.Any,
                                NumberFormatInfo.InvariantInfo,
                                out result)
                )
                {
                    throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.REPORT_NO_BOOKXDAYS));
                }	
                if (mReportView.TxtValidityFrom.Text == "" && mReportView.TxtValidityUntil.Text == "")
				{
					throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.REPORT_VALIDUNTIL));
				}							
			}
			else if (mReportName == ReportNames.CWR_NO_BOOKING)
			{
				if (mReportView.txtXDays.Text.Equals(""))
				{
					throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.REPORT_NO_BOOKXDAYS));
				}						
			}
		}

        /// <summary>
        /// Checks attendance dates (dynamic data) are correct. From cannot be before To
        /// </summary>
        internal void CheckAttendDates()
        {
            mReportView = (FrmReport)mView;

            if (mReportView.DatMonthFrom.Text.Trim().Length > 0 && mReportView.DatMonthUntil.Text.Trim().Length > 0)
            {
                if (mReportView.DatMonthFrom.Value.Date > mReportView.DatMonthUntil.Value.Date)
                {
                    throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.DYNDATA_FROMTILLDATE)); 
                }
            }
        }

        /// <summary>
        /// Checks given dates (passed as strings) are correct. From cannot be before To
        /// </summary>
        internal void CheckDeliveryDates(string pFromDatTxt, string pUntilDatTxt)
        {
            mReportView = (FrmReport)mView;
 
            if (pFromDatTxt.Length > 0 && !StringValidation.GetInstance().IsDateString(pFromDatTxt))
            {
                throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.INVALID_FULLDATE));
            }

            if (pUntilDatTxt.Length > 0 && !StringValidation.GetInstance().IsDateString(pUntilDatTxt))
            {
                throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.INVALID_FULLDATE));
            }

            if (pFromDatTxt.Length > 0 && pUntilDatTxt.Length > 0 
                && !StringValidation.GetInstance().IsDateValid(pFromDatTxt, pUntilDatTxt))
            {
                throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.DYNDATA_FROMTILLDATE));
            }
        }

		/// <summary>
		/// Resets dialogue before it is reshown.
		/// </summary>
		internal override void PreShow()
		{
			mResultsType = 0;
			SetResultsGridVisible();
            TimePicker();
			((FrmReport) mView).BtnSearch.Enabled = false;
		}

		/// <summary>
		/// Calculates first and the last day of the current months
		/// and fills booking date fields (Bewegungsdaten) with these values
		/// </summary>
		internal void TimePicker()
		{
			DateTime Currentdate = DateTime.Now;
			int daysOfMonth = new GregorianCalendar().GetDaysInMonth(Currentdate.Year, Currentdate.Month);
			string lastOfMonth = daysOfMonth.ToString() + "." + Convert.ToDateTime(Currentdate).ToString("MM.yyyy");
			string firstOfMonth = "01." + Convert.ToDateTime(Currentdate).ToString("MM.yyyy");
			((FrmReport)mView).DatMonthFrom.Value = Convert.ToDateTime(firstOfMonth);
			((FrmReport)mView).DatMonthUntil.Value = Convert.ToDateTime(lastOfMonth);
		}


        /// <summary>
        /// Gets user's selection of external contractors from a textfile on J:\appsuer\FPASS
        /// and loads into Checklistbox "Fremdfirmen". Accepts checklistbox object and filename
        /// </summary>
        /// <param name="pExcoListBox"></param>
        /// <param name="pFileName"></param>
        internal void LoadMyExcos(CheckedListBox pExcoListBox, string pFileName)
        {
            mFileName = pFileName;
            LoadMyExcos(pExcoListBox);
        }

        /// <summary>
        /// Gets user's selection of external contractors from a textfile on J:\appsuer\FPASS
        /// and loads into Checklistbox "Fremdfirmen"
        /// </summary>
        internal void LoadMyExcos(CheckedListBox pExcoListBox)
        {
            string excoEntry;
            ArrayList excoList = new ArrayList();
            LovItem item;
            int ctr = 0;

            // Do nowt if no file
            if (!File.Exists(mFileName))
                return;
            
            try
            {
                StreamReader strmExcos = File.OpenText(mFileName);

                excoEntry = strmExcos.ReadLine();
                excoList.Add(excoEntry);

                // Get excos from textfile
                while (excoEntry != null)
                {
                    excoEntry = strmExcos.ReadLine();
                    if (null != excoEntry)
                    {
                        // Trim and remove trailing ";", add to list
                        excoEntry = excoEntry.Trim();
                        //excoEntry = excoEntry.Remove(excoEntry.Length - 1, 1);
                        excoList.Add(excoEntry.Trim());
                    }
                }
                strmExcos.Close();


                // Compare with entries in checklistbox
                ArrayList listBoxItems = new ArrayList(pExcoListBox.Items);
 
                foreach (object obj in listBoxItems)
                {
                    item = (LovItem)obj;

                    foreach (string excoName in excoList)
                    {
                        if (item.ItemValue == excoName)
                        {
                            pExcoListBox.SetItemChecked(ctr, true);
                            break;
                        }
                    }
                    ctr++;
                }
            }
            catch (Exception ex)
            {
                ShowMessageInStatusBar("Datei " + Globals.REPORTS_MYEXCO_FILE + "konnte nicht gelesen werden. " + ex.Message);
            }
        }


        /// <summary>
        /// Saves user's selection of external contractors to a textfile
        /// (from Checklistbox "Fremdfirmen" under heading "Anwesenheit")
        /// </summary>
        /// <param name="pFileName">filename to use</param>
        internal void SaveMyExcos(string pFileName)
        {
            mFileName = pFileName;
            SaveMyExcos();
        }

        /// <summary>
        /// Saves user's selection of external contractors to a textfile
        /// (from Checklistbox "Fremdfirmen" under heading "Anwesenheit")
        /// </summary>
        internal void SaveMyExcos()
        {
            LovItem item;

            try
            {
                StreamWriter strmExcos = File.CreateText(mFileName);

                // Get checked entries from checklistbox
                foreach (object obj in ((FrmReport)mView).ClbAttExContractor.CheckedItems)
                {
                    item = (LovItem)obj;
                    strmExcos.WriteLine(item.ItemValue);
                }
                strmExcos.Close();
            }
            catch (Exception ex)
            {
                ShowMessageInStatusBar("Datei " + mFileName + "konnte nicht geschrieben werden. " + ex.Message);
            }
        }
		

	    /// <summary>
        /// Synchronises two CheckedListBoxes
	    /// </summary>
	    /// <param name="pExcoListBoxSrc">source</param>
	    /// <param name="pExcoListBoxTar">target</param>
        internal void SynchroniseExcoLists(CheckedListBox pExcoListBoxSrc, CheckedListBox pExcoListBoxTar)
        {
            
            // Clear target
            ArrayList listChecked = new ArrayList(pExcoListBoxTar.CheckedIndices);
            foreach (int pos in listChecked)
            {
                pExcoListBoxTar.SetItemChecked(pos, false);
            }
           
            // Synchronise target
            foreach (int pos in pExcoListBoxSrc.CheckedIndices)
            {
                pExcoListBoxTar.SetItemChecked(pos, true);
            } 
        }
	    
		/// <summary>
		/// Activates search fields and UserControl for results, depending on which report was chosen
		/// </summary>
		internal void HandleReports()
		{
			mReportName = ((FrmReport) mView).CboReport.Text;
            ClearSearches();
            mOrderBy = "";

			((FrmReport) mView).BtnSearch.Enabled = true;
			
			OrderControlsSetBack();
			EnableControlsByReport();
			mMustSelectCwr = false;

			switch (mReportName)
			{
				case ReportNames.EMPTY:
					mResultsType = 0;
					SetResultsGridVisible();
					((FrmReport) mView).BtnSearch.Enabled = false;
					((FrmReport) mView).BtnGenerateReport.Enabled = false;
					((FrmReport)mView).BtnGenerateExport.Enabled = false;
					((FrmReport) mView).SbpMessage.Text = String.Empty;
					break;
							
				case ReportNames.CHECKLIST:			
					mResultsType = ReportNames.ResultTypes.CHECKLIST;
					break;
					
				case ReportNames.PLANTS:						
					mResultsType = ReportNames.ResultTypes.PLANT;
                    mQueryName = PLANTMANAGER_QUERY;
                    mOrderBy = " ORDER BY PL_NAME";
					break;
					
				case ReportNames.EXCO_COORDINATOR:
					mResultsType = ReportNames.ResultTypes.EXCONTRACTOR;
                    mQueryName = EXCONTRACTOR_QUERY;
                    mOrderBy = " ORDER BY EXCO_NAME, VWC_SURNAME";
					break;
				
				case ReportNames.CWR_ALL_DATA:
					mResultsType = ReportNames.ResultTypes.COWORKER;
					mQueryName	= "CoWorkerReports";
                    mOrderBy = " ORDER BY CWR_SURNAME,CWR_FIRSTNAME";
					mNoSearchCriteriaAllowed = true;
					mMustSelectCwr = true;
					break;
					
				case ReportNames.CWR_BOOKINGS:
					mResultsType = ReportNames.ResultTypes.COWORKER;
					mQueryName	= "CWRDynDateReports";
                    mOrderBy = " ORDER BY CWR_SURNAME,CWR_FIRSTNAME";
					mNoSearchCriteriaAllowed = false;
					mMustSelectCwr = true;
					break;
					
				case ReportNames.EXCO_BOOKINGS_SUM:										
					mResultsType = ReportNames.ResultTypes.EXCOBOOKING;
					mQueryName = "CWRSumCoworkerAExcoReports";
                    mOrderBy = "";
					mNoSearchCriteriaAllowed = false;
					break;
					
				case ReportNames.CWR_ATTEND_DETAIL:										
					mResultsType = ReportNames.ResultTypes.ATTENDANCE;
					mQueryName	= "CWRAttendanceDetailReports";
                    mOrderBy = " ORDER BY VATT_SURNAME,VATT_FIRSTNAME";
                    mOrderBy = "";
					mNoSearchCriteriaAllowed = false;
                    LoadMyExcos(((FrmReport)mView).ClbAttExContractor);
					break;
					
				case ReportNames.CWR_ATTENDANCE:					
					mResultsType = ReportNames.ResultTypes.ATTENDANCE;
					mQueryName	= "CWRAttendanceSumsReports";
                    mOrderBy = " ORDER BY VATT_SURNAME,VATT_FIRSTNAME";
                    mOrderBy = "";
					mNoSearchCriteriaAllowed = false;
                    LoadMyExcos(((FrmReport)mView).ClbAttExContractor);
					break;
					
				case ReportNames.EXCO_ATTENDANCE:					
					mResultsType = ReportNames.ResultTypes.EXCOBOOKING;
					mQueryName	= "EXCOAttendanceSumsReports";
                    mOrderBy = " ORDER BY EXCO_NAME";
					mNoSearchCriteriaAllowed = false;
                    LoadMyExcos(((FrmReport)mView).ClbAttExContractor);
					break;
					
				case ReportNames.CWR_EXPIRYDATE:					
					mResultsType = ReportNames.ResultTypes.COWORKER;
					mQueryName	= "CoWorkerReports";
					mNoSearchCriteriaAllowed = false;
					break;
					
				case ReportNames.CWR_NO_BOOKING:
					mResultsType = ReportNames.ResultTypes.COWORKER;
					mQueryName	= "CoWorkerReportsNoBook";
					mNoSearchCriteriaAllowed = false;
					break;
					
				case ReportNames.CWR_DELETELIST:	
					mResultsType = ReportNames.ResultTypes.COWORKER;
					mQueryName	= "CWRDelCwrReports";
					mNoSearchCriteriaAllowed = true;
					break;
					
				case ReportNames.CWR_PASS:	
					mResultsType = ReportNames.ResultTypes.COWORKER;
					mQueryName	= "CoWorkerReports";
                    mOrderBy = " ORDER BY CWR_SURNAME, CWR_FIRSTNAME";
					mNoSearchCriteriaAllowed = false;
					mMustSelectCwr = true;
					break;
							
				case ReportNames.CWR_CHANGEHIST:
					mResultsType = ReportNames.ResultTypes.COWORKER;
					mQueryName	= "CoWorkerReports";
                    mOrderBy = " ORDER BY CWR_SURNAME, CWR_FIRSTNAME";
					mNoSearchCriteriaAllowed = false;
					mMustSelectCwr = true;
					break;
								
				case ReportNames.RESPMASKS:
					mResultsType = ReportNames.ResultTypes.RESPMASK;
					mNoSearchCriteriaAllowed = false;
                    mQueryName = RESPMASK_QUERY;
                    mOrderBy = " ORDER BY MASKDATELENT DESC, MASKDATERETURN DESC, CWR_SURNAME";
					break;
					
				case ReportNames.CWR_BOOKINGS_EXCO:																
					mResultsType = ReportNames.ResultTypes.COWORKER;
					mQueryName	= "CWRDynDateReports";
                    mOrderBy = " ORDER BY CWR_SURNAME, CWR_FIRSTNAME";
					mNoSearchCriteriaAllowed = false;
					break;
			}
			SetResultsGridVisible();
			OrderControlsTabs();
		}
	
		#region EnableElements
		
		/// <summary>
		/// Enables GUI controls by which report name is currently selected
		/// Diables all to begin with.
		/// </summary>
		public void EnableControlsByReport()
		{
			mReportView = (FrmReport) mView;
			mReportView.CobCoordinator.Enabled = false;
			mReportView.CobExternalContractor.Enabled = false;
			mReportView.CboCraft.Enabled = false;
			mReportView.CboDepartment.Enabled = false;
			mReportView.TxtFirstname.Enabled = false;
			mReportView.CboOrderNumber.Enabled = false;
			mReportView.CboPlant.Enabled = false;
			mReportView.CboSubcontractor.Enabled = false;
			mReportView.CboSupervisor.Enabled = false;
			mReportView.TxtSurname.Enabled = false;
			mReportView.DatMonthFrom.Enabled = false;
			mReportView.DatMonthUntil.Enabled = false;
			mReportView.TxtValidityUntil.Enabled = false;
			mReportView.TxtValidityFrom.Enabled = false;
			mReportView.TxtDeliveryDateFrom.Enabled = false;
			mReportView.TxtDeliveryDateUntil.Enabled = false;
			mReportView.RbtAccessAuthorizationNo.Enabled = false;
			mReportView.RbtAccessAuthorizationYes.Enabled = false;
			mReportView.RbtStatusYes.Enabled = false;
			mReportView.RbtStatusNo.Enabled = false;
            mReportView.BtnClearAcRadio.Enabled = false;
			mReportView.TxtMaskNo.Enabled = false;
			mReportView.TxtMaintenanceFrom.Enabled = false;
			mReportView.TxtMaintenanceUntil.Enabled = false;
			mReportView.txtXDays.Enabled = false;
			mReportView.CbxDelivered.Enabled = false;
			mReportView.CbxReceived.Enabled = false;
			mReportView.ClbAttExContractor.Enabled = false;
            mReportView.btnPopupExcos.Enabled = false;

			switch (mReportName)
			{
				case ReportNames.EMPTY:
					break;
				case ReportNames.CWR_ATTEND_DETAIL:
				case ReportNames.CWR_ATTENDANCE:
					mReportView.TxtSurname.Enabled = true;
					mReportView.TxtFirstname.Enabled = true;
					mReportView.DatMonthFrom.Enabled = true;
					mReportView.DatMonthUntil.Enabled = true;
					mReportView.ClbAttExContractor.Enabled = true;
                    mReportView.btnPopupExcos.Enabled = true;
					break;
				case ReportNames.EXCO_ATTENDANCE:
					mReportView.DatMonthFrom.Enabled = true;
					mReportView.DatMonthUntil.Enabled = true;
					mReportView.ClbAttExContractor.Enabled = true;
                    mReportView.btnPopupExcos.Enabled = true;
					break;
				case ReportNames.CHECKLIST:
					mReportView.TxtSurname.Enabled = true;
					mReportView.TxtFirstname.Enabled = true;
					mReportView.RbtAccessAuthorizationNo.Enabled = true;
					mReportView.RbtAccessAuthorizationYes.Enabled = true;
					mReportView.TxtValidityUntil.Enabled = true;
					mReportView.TxtValidityFrom.Enabled = true;
					mReportView.CobExternalContractor.Enabled = true;
					mReportView.CboSubcontractor.Enabled = true;	
					break;
				case ReportNames.PLANTS:
					mReportView.CboPlant.Enabled = true;
					break;
				case ReportNames.EXCO_COORDINATOR:
					mReportView.CobExternalContractor.Enabled = true;
					mReportView.CboSupervisor.Enabled = true;
					mReportView.CobCoordinator.Enabled = true;
					mReportView.CboSubcontractor.Enabled = true;
					break;
				case ReportNames.CWR_BOOKINGS_EXCO:
					mReportView.CobExternalContractor.Enabled = true;
					mReportView.DatMonthFrom.Enabled = true;
					mReportView.DatMonthUntil.Enabled = true;
					break;
				case ReportNames.CWR_BOOKINGS:
					mReportView.TxtSurname.Enabled = true;
					mReportView.TxtFirstname.Enabled = true;
					mReportView.RbtAccessAuthorizationNo.Enabled = true;
					mReportView.RbtAccessAuthorizationYes.Enabled = true;
					mReportView.RbtStatusYes.Enabled = true;
					mReportView.RbtStatusNo.Enabled = true;
                    mReportView.BtnClearAcRadio.Enabled = true;
					mReportView.DatMonthFrom.Enabled = true;
					mReportView.DatMonthUntil.Enabled = true;
					mReportView.TxtDeliveryDateFrom.Enabled = true;
					mReportView.TxtDeliveryDateUntil.Enabled = true;
					mReportView.TxtValidityUntil.Enabled = true;
					mReportView.TxtValidityFrom.Enabled = true;
					mReportView.CobExternalContractor.Enabled = true;
					mReportView.CboSupervisor.Enabled = true;
					mReportView.CobCoordinator.Enabled = true;
					mReportView.CboSubcontractor.Enabled = true;
					mReportView.CboOrderNumber.Enabled = true;		
					mReportView.CboCraft.Enabled = true;
					mReportView.CboDepartment.Enabled = true;					
					mReportView.CboPlant.Enabled = true;		
					break;
				case ReportNames.EXCO_BOOKINGS_SUM:					
					mReportView.DatMonthFrom.Enabled = true;
					mReportView.DatMonthUntil.Enabled = true;					
					mReportView.CobExternalContractor.Enabled = true;
					mReportView.CboSupervisor.Enabled = true;
					mReportView.CobCoordinator.Enabled = true;
					mReportView.CboSubcontractor.Enabled = true;
					break;
				case ReportNames.CWR_CHANGEHIST:
				case ReportNames.CWR_ALL_DATA:
				case ReportNames.CWR_EXPIRYDATE:
				case ReportNames.CWR_DELETELIST:
				case ReportNames.CWR_PASS:
					mReportView.TxtSurname.Enabled = true;
					mReportView.TxtFirstname.Enabled = true;
					mReportView.RbtAccessAuthorizationNo.Enabled = true;
					mReportView.RbtAccessAuthorizationYes.Enabled = true;
					mReportView.RbtStatusYes.Enabled = true;
					mReportView.RbtStatusNo.Enabled = true;
                    mReportView.BtnClearAcRadio.Enabled = true;
					mReportView.TxtDeliveryDateFrom.Enabled = true;
					mReportView.TxtDeliveryDateUntil.Enabled = true;
					mReportView.TxtValidityUntil.Enabled = true;
					mReportView.TxtValidityFrom.Enabled = true;
					mReportView.CobExternalContractor.Enabled = true;
					mReportView.CboSupervisor.Enabled = true;
					mReportView.CobCoordinator.Enabled = true;
					mReportView.CboSubcontractor.Enabled = true;
					mReportView.CboOrderNumber.Enabled = true;		
					mReportView.CboCraft.Enabled = true;
					mReportView.CboDepartment.Enabled = true;					
					mReportView.CboPlant.Enabled = true;
					break;				
				case ReportNames.CWR_NO_BOOKING:
					mReportView.TxtSurname.Enabled = true;
					mReportView.TxtFirstname.Enabled = true;
					mReportView.txtXDays.Enabled = true;
					mReportView.CobExternalContractor.Enabled = true;
					mReportView.CobCoordinator.Enabled = true;
					break;
				case ReportNames.RESPMASKS:
					mReportView.TxtSurname.Enabled = true;
					mReportView.TxtFirstname.Enabled = true;
					mReportView.RbtAccessAuthorizationNo.Enabled = true;
					mReportView.RbtAccessAuthorizationYes.Enabled = true;
					mReportView.RbtStatusYes.Enabled = true;
					mReportView.RbtStatusNo.Enabled = true;
                    mReportView.BtnClearAcRadio.Enabled = true;
					mReportView.CobExternalContractor.Enabled = true;
					mReportView.CboSupervisor.Enabled = true;
					mReportView.CobCoordinator.Enabled = true;
					mReportView.CboSubcontractor.Enabled = true;
					mReportView.CboOrderNumber.Enabled = true;		
					mReportView.CboCraft.Enabled = true;
					mReportView.CboDepartment.Enabled = true;					
					mReportView.CboPlant.Enabled = true;					
					mReportView.TxtMaintenanceFrom.Enabled = true;
					mReportView.TxtMaintenanceUntil.Enabled = true;
					mReportView.TxtMaskNo.Enabled = true;
					mReportView.CbxDelivered.Enabled = true;
					mReportView.CbxReceived.Enabled = true;
					break;			
			}
		}

		#endregion EnableElements

		/// <summary>
		/// Clears all search field values in the Reports form
		/// </summary>
		internal void ClearFields()
		{
			mReportView = (FrmReport) mView;
			mReportView.SbpMessage.Text = String.Empty;
			mReportView.CobCoordinator.SelectedValue = 0;
			mReportView.CobCoordinator.Text = "";
			mReportView.CobExternalContractor.SelectedValue = 0;
			mReportView.CobExternalContractor.Text = "";
			mReportView.CboCraft.SelectedValue = 0;
			mReportView.CboCraft.Text = "";
			mReportView.CboDepartment.SelectedValue = 0;
			mReportView.CboDepartment.Text = "";
			mReportView.CboOrderNumber.SelectedValue = 0;
			mReportView.CboOrderNumber.Text = "";
			mReportView.CboPlant.SelectedValue = 0;
			mReportView.CboPlant.Text = "";
			mReportView.CboSubcontractor.SelectedValue = 0;
			mReportView.CboSubcontractor.Text = "";
			mReportView.CboSupervisor.SelectedValue = 0;
			mReportView.CboSupervisor.Text = "";
			mReportView.TxtFirstname.Text = String.Empty;
			mReportView.TxtSurname.Text = String.Empty;

			mReportView.TxtValidityUntil.Text = String.Empty;
			mReportView.TxtValidityFrom.Text = String.Empty;
			mReportView.TxtDeliveryDateFrom.Text = String.Empty;
			mReportView.TxtDeliveryDateUntil.Text = String.Empty;

            ClearAccessFields();
			mReportView.TxtMaskNo.Text = String.Empty;
			mReportView.TxtMaintenanceFrom.Text = String.Empty;
			mReportView.TxtMaintenanceUntil.Text = String.Empty;
			mReportView.txtXDays.Text = String.Empty;
			mReportView.CbxDelivered.Checked = false;
			mReportView.CbxReceived.Checked = false;
			mReportView.ClbAttExContractor.ClearSelected();
			for (int z = 0; z< mReportView.ClbAttExContractor.Items.Count; z++)
			{
				mReportView.ClbAttExContractor.SetItemChecked(z, false);
			}

            ClearSearches();
			
			//Set the dynamic data
			TimePicker();

			mWhereClause = String.Empty;
		}


        internal void ClearAccessFields()
        {
            mReportView.RbtAccessAuthorizationNo.Checked = false;
            mReportView.RbtAccessAuthorizationNo.TabStop = true;
            mReportView.RbtAccessAuthorizationYes.Checked = false;
            mReportView.RbtAccessAuthorizationYes.TabStop = true;
            mReportView.RbtStatusYes.Checked = false;
            mReportView.RbtStatusYes.TabStop = true;
            mReportView.RbtStatusNo.Checked = false;
            mReportView.RbtStatusNo.TabStop = true;
        }

        /// <summary>
        /// Clears search parameters and results.
        /// Note: values remain in search fields but are removed from search parameters
        /// </summary>
        private void ClearSearches()
        {
            // Clear search paramters
            mSurnameParameter = "";
            mFirstnameParameter = "";
            mAccessauthorizeYesParameter = false;
            mAccessauthorizeNoParameter = false;
            mStatusGueltigParameter = false;
            mStatusUngueltigParameter = false;
            mNoBookXDaysParameter = "";
            mDynamicDataFromParameter = "";    
            mDynamicDataUntilParameter = "";
            mDelDateFromParameter = "";
            mDelDateUntilParameter = "";
            mValidFromParameter = "";
            mValidUntilParameter = "";

            mExContractorParameter  = "";
            mSupervisorParameter = "";
            mSupervisorNameParameter = "";
            mCoordinatorParameter  = "";
            mSubContractorParameter  = "";
            mOrdernoParameter = "";
            mCraftParameter = "";
            mPlantParameter = ""; 
            mDepartmentParameter = "";
            mMantenanceFromPara = "";
            mMantenanceUntilPara = "";
            mMaskNoParameter = "";
            mMaskDelivered = false;
            mMaskReceived = false;

            // Disable buttons
            mReportView.BtnGenerateExport.Enabled = false;
            mReportView.BtnGenerateReport.Enabled = false;

            // Clear datagrids
            mReportView.ucReportChecklist1.DgrChecklist.DataSource = null;
            mReportView.ucReportCoWorker1.DgrReportCoWorker.DataSource = null;
            mReportView.ucReportExConBooking.DgrReportExContr.DataSource = null;
            mReportView.ucReportExContractor1.DgrReportExContractor.DataSource = null;
            mReportView.ucReportPlant.DgrReportPlant.DataSource = null;
            mReportView.ucReportAttendance1.DgrReportCoWorker.DataSource = null;
            mReportView.ucReportRespMask1.DgrReportRepMask.DataSource = null;

            mReportView.SbpMessage.Text = String.Empty;
            mWhereClause = String.Empty;
        }

        #region SearchGUIGeneral

        /// <summary>
		/// Shows UserControl containing search results corresponding to result type.
		/// Only one UserControl is visible at a time
		/// </summary>
		internal void SetResultsGridVisible()
		{
			mReportView = (FrmReport) mView;

			mReportView.ucReportPlant.Visible = false;
			mReportView.ucReportExContractor1.Visible = false;
			mReportView.ucReportCoWorker1.Visible = false;
			mReportView.ucReportChecklist1.Visible = false;
			mReportView.ucReportExConBooking.Visible = false;
			mReportView.ucReportAttendance1.Visible = false;
			mReportView.ucReportRespMask1.Visible = false;

			mReportView.ucReportPlant.Visible = (mResultsType == ReportNames.ResultTypes.PLANT);
			mReportView.ucReportExContractor1.Visible = (mResultsType == ReportNames.ResultTypes.EXCONTRACTOR);
			mReportView.ucReportCoWorker1.Visible = (mResultsType == ReportNames.ResultTypes.COWORKER);
			mReportView.ucReportChecklist1.Visible = (mResultsType == ReportNames.ResultTypes.CHECKLIST);
			mReportView.ucReportExConBooking.Visible = (mResultsType == ReportNames.ResultTypes.EXCOBOOKING);
			mReportView.ucReportAttendance1.Visible = (mResultsType == ReportNames.ResultTypes.ATTENDANCE);
			mReportView.ucReportRespMask1.Visible = (mResultsType == ReportNames.ResultTypes.RESPMASK);	
		}

		/// <summary>
		/// Resets tab order of GUI controls
		/// </summary>
		public void OrderControlsSetBack()
		{
			mReportView = (FrmReport)mView;
			mReportView.PnlReport.TabIndex = 0;
			mReportView.CboReport.TabIndex = 0;
			mReportView.CboReport.TabStop = false;

			mReportView.PnlCoWorker.TabIndex = 0;
			mReportView.TxtSurname.TabIndex = 0;
			mReportView.TxtSurname.TabStop = false;
			mReportView.TxtFirstname.TabIndex = 0;
			mReportView.TxtFirstname.TabStop = false;
			mReportView.PnlAccessAuthorization.TabIndex = 0;
			mReportView.RbtAccessAuthorizationYes.TabIndex = 0;
			mReportView.RbtAccessAuthorizationYes.TabStop = false;
			mReportView.RbtAccessAuthorizationNo.TabIndex = 0;
			mReportView.RbtAccessAuthorizationNo.TabStop = false;
			mReportView.PnlStatus.TabIndex = 0;
			mReportView.RbtStatusYes.TabIndex = 0;
			mReportView.RbtStatusYes.TabStop = false;
			mReportView.RbtStatusNo.TabIndex = 0;
			mReportView.RbtStatusNo.TabStop = false;
			mReportView.DatMonthFrom.TabIndex = 0;
			mReportView.DatMonthFrom.TabStop = false;
			mReportView.DatMonthUntil.TabIndex = 0;
			mReportView.DatMonthUntil.TabStop = false;

			mReportView.PnlPass.TabIndex = 0;
			mReportView.TxtValidityFrom.TabIndex = 0;
			mReportView.TxtValidityFrom.TabStop = false;
			mReportView.TxtValidityUntil.TabIndex = 0;
			mReportView.TxtValidityUntil.TabStop = false;
			mReportView.TxtDeliveryDateFrom.TabIndex = 0;
			mReportView.TxtDeliveryDateFrom.TabStop = false;
			mReportView.TxtDeliveryDateUntil.TabIndex = 0;
			mReportView.TxtDeliveryDateUntil.TabStop = false;

			mReportView.PnlExternalContractor.TabIndex = 0;
			mReportView.CobExternalContractor.TabIndex = 0;
			mReportView.CobExternalContractor.TabStop = false;
			mReportView.CboSubcontractor.TabIndex = 0;
			mReportView.CboSubcontractor.TabStop = false;
			mReportView.CobCoordinator.TabIndex = 0;
			mReportView.CobCoordinator.TabStop = false;
			mReportView.CboSupervisor.TabIndex = 0;
			mReportView.CboSupervisor.TabStop = false;

			mReportView.PnlSite.TabIndex = 0;
			mReportView.CboCraft.TabIndex = 0;
			mReportView.CboCraft.TabStop = false;
			mReportView.CboDepartment.TabIndex = 0;
			mReportView.CboDepartment.TabStop = false;
			mReportView.CboOrderNumber.TabIndex = 0;
			mReportView.CboOrderNumber.TabStop = false;
			mReportView.CboPlant.TabIndex = 0;
			mReportView.CboPlant.TabStop = false;
			
			mReportView.PnlRespMask.TabIndex = 0;
			mReportView.TxtMaskNo.TabIndex = 0;
			mReportView.TxtMaskNo.TabStop = false;
			mReportView.TxtMaintenanceFrom.TabIndex = 0;
			mReportView.TxtMaintenanceFrom.TabStop = false;
			mReportView.TxtMaintenanceUntil.TabIndex = 0;
			mReportView.TxtMaintenanceUntil.TabStop = false;

			mReportView.PnlAttendance.TabIndex = 0;
			mReportView.ClbAttExContractor.TabIndex = 0;
			mReportView.ClbAttExContractor.TabStop = false;
            mReportView.btnPopupExcos.TabIndex = 0;
            mReportView.btnPopupExcos.TabStop = false;
			
			mReportView.BtnSearch.TabIndex = 0;
			mReportView.BtnSearch.TabStop = false;
			mReportView.BtnGenerateReport.TabIndex = 0;
			mReportView.BtnGenerateReport.TabStop = false;
			mReportView.BtnBackTo.TabIndex = 0;
			mReportView.BtnBackTo.TabStop = false;
		}
		

		/// <summary>
		/// Sets tab order of all GUI controls (sets tab stop even if ccontrol disabled)
		/// </summary>
		private void OrderControlsTabs()
		{
			mReportView = (FrmReport) mView;
			int tabCounter = 0;
			
			mReportView.PnlReport.TabIndex = tabCounter++;
			mReportView.CboReport.TabIndex = tabCounter++;
			mReportView.CboReport.TabStop = true;

			mReportView.PnlCoWorker.TabIndex = tabCounter++;
			mReportView.TxtSurname.TabIndex = tabCounter++;
			mReportView.TxtSurname.TabStop = true;
			mReportView.TxtFirstname.TabIndex = tabCounter++;
			mReportView.TxtFirstname.TabStop = true;
			mReportView.PnlAccessAuthorization.TabIndex = tabCounter++;
			mReportView.RbtAccessAuthorizationYes.TabIndex = tabCounter++;
			mReportView.RbtAccessAuthorizationYes.TabStop = true;
			mReportView.RbtAccessAuthorizationNo.TabIndex = tabCounter++;
			mReportView.RbtAccessAuthorizationNo.TabStop = true;
			mReportView.PnlStatus.TabIndex = tabCounter++;
			mReportView.RbtStatusYes.TabIndex = tabCounter++;
			mReportView.RbtStatusYes.TabStop = true;
			mReportView.RbtStatusNo.TabIndex = tabCounter++;
			mReportView.RbtStatusNo.TabStop = true;
			mReportView.txtXDays.TabStop = true;
			mReportView.txtXDays.TabIndex = tabCounter++;
			mReportView.DatMonthFrom.TabIndex = tabCounter++;
			mReportView.DatMonthFrom.TabStop = true;
			mReportView.DatMonthUntil.TabIndex = tabCounter++;
			mReportView.DatMonthUntil.TabStop = true;

			mReportView.PnlPass.TabIndex = tabCounter++;;
			mReportView.TxtDeliveryDateFrom.TabIndex = tabCounter++;
			mReportView.TxtDeliveryDateFrom.TabStop = true;
			mReportView.TxtDeliveryDateUntil.TabIndex = tabCounter++;
			mReportView.TxtDeliveryDateUntil.TabStop = true;
			mReportView.TxtValidityFrom.TabIndex = tabCounter++;
			mReportView.TxtValidityFrom.TabStop = true;
			mReportView.TxtValidityUntil.TabIndex = tabCounter++;
			mReportView.TxtValidityUntil.TabStop = true;

			mReportView.PnlExternalContractor.TabIndex = tabCounter++;
			mReportView.CobExternalContractor.TabIndex = tabCounter++;
			mReportView.CobExternalContractor.TabStop = true;
			mReportView.CboSupervisor.TabIndex = tabCounter++;
			mReportView.CboSupervisor.TabStop = true;
			mReportView.CobCoordinator.TabIndex = tabCounter++;
			mReportView.CobCoordinator.TabStop = true;
			mReportView.CboSubcontractor.TabIndex = tabCounter++;
			mReportView.CboSubcontractor.TabStop = true;

			mReportView.PnlSite.TabIndex = tabCounter++;;
			mReportView.CboOrderNumber.TabIndex = tabCounter++;
			mReportView.CboOrderNumber.TabStop = true;
			mReportView.CboCraft.TabIndex = tabCounter++;
			mReportView.CboCraft.TabStop = true;
			mReportView.CboPlant.TabIndex = tabCounter++;
			mReportView.CboPlant.TabStop = true;
			mReportView.CboDepartment.TabIndex = tabCounter++;
			mReportView.CboDepartment.TabStop = true;

			mReportView.PnlRespMask.TabIndex = tabCounter++;
			mReportView.TxtMaintenanceFrom.TabIndex = tabCounter++;
			mReportView.TxtMaintenanceFrom.TabStop = true;
			mReportView.TxtMaintenanceUntil.TabIndex = tabCounter++;
			mReportView.TxtMaintenanceUntil.TabStop = true;
			mReportView.TxtMaskNo.TabIndex = tabCounter++;
			mReportView.TxtMaskNo.TabStop = true;
            mReportView.CbxDelivered.TabIndex = tabCounter++;
            mReportView.CbxDelivered.TabStop = true;
            mReportView.CbxReceived.TabIndex = tabCounter++;
            mReportView.CbxReceived.TabStop = true;

			mReportView.PnlAttendance.TabIndex = tabCounter++;
			mReportView.ClbAttExContractor.TabIndex = tabCounter++;
			mReportView.ClbAttExContractor.TabStop = true;
            mReportView.btnPopupExcos.TabIndex = tabCounter++;
            mReportView.btnPopupExcos.TabStop = true;

			if (mReportView.ucReportCoWorker1.Visible)
			{
				mReportView.ucReportCoWorker1.TabIndex = tabCounter++;
				mReportView.ucReportCoWorker1.TabStop = true;
				mReportView.ucReportCoWorker1.DgrReportCoWorker.TabIndex= tabCounter++;
				mReportView.ucReportCoWorker1.DgrReportCoWorker.TabStop = true;
			}
			else if (mReportView.ucReportAttendance1.Visible)
			{
				mReportView.ucReportAttendance1.TabIndex = tabCounter++;
				mReportView.ucReportAttendance1.TabStop = true;
				mReportView.ucReportAttendance1.DgrReportCoWorker.TabIndex= tabCounter++;
				mReportView.ucReportAttendance1.DgrReportCoWorker.TabStop = true;
			}
			else if (mReportView.ucReportChecklist1.Visible)
			{
				mReportView.ucReportChecklist1.TabIndex = tabCounter++;
				mReportView.ucReportChecklist1.TabStop = true;
				mReportView.ucReportChecklist1.DgrChecklist.TabIndex= tabCounter++;
				mReportView.ucReportChecklist1.DgrChecklist.TabStop = true;
			}
			else if (mReportView.ucReportExConBooking.Visible)
			{
				mReportView.ucReportExConBooking.TabIndex = tabCounter++;
				mReportView.ucReportExConBooking.TabStop = true;
				mReportView.ucReportExConBooking.DgrReportExContr.TabIndex= tabCounter++;
				mReportView.ucReportExConBooking.DgrReportExContr.TabStop = true;
			}
			else if (mReportView.ucReportExContractor1.Visible)
			{
				mReportView.ucReportExContractor1.TabIndex = tabCounter++;
				mReportView.ucReportExContractor1.TabStop = true;
				mReportView.ucReportExContractor1.DgrReportExContractor.TabIndex= tabCounter++;
				mReportView.ucReportExContractor1.DgrReportExContractor.TabStop = true;
			}
			else if (mReportView.ucReportPlant.Visible)
			{
				mReportView.ucReportPlant.TabIndex = tabCounter++;
				mReportView.ucReportPlant.TabStop = true;
				mReportView.ucReportPlant.DgrReportPlant.TabIndex= tabCounter++;
				mReportView.ucReportPlant.DgrReportPlant.TabStop = true;
			}
			else if (mReportView.ucReportRespMask1.Visible)
			{
				mReportView.ucReportRespMask1.TabIndex = tabCounter++;
				mReportView.ucReportRespMask1.TabStop = true;
				mReportView.ucReportRespMask1.DgrReportRepMask.TabIndex= tabCounter++;
				mReportView.ucReportRespMask1.DgrReportRepMask.TabStop = true;
			}

			mReportView.BtnGenerateExport.TabIndex = tabCounter++;
			mReportView.BtnGenerateExport.TabStop =true;
			mReportView.BtnClearMask.TabIndex = tabCounter++;
			mReportView.BtnClearMask.TabStop =true;
			mReportView.BtnSearch.TabIndex = tabCounter++;
			mReportView.BtnSearch.TabStop = true;
			mReportView.BtnGenerateReport.TabIndex = tabCounter++;
			mReportView.BtnGenerateReport.TabStop = true;
			mReportView.BtnBackTo.TabIndex = tabCounter++;
			mReportView.BtnBackTo.TabStop = true;
		}

        /// <summary>
        /// Reads values of GUI search fields and sets appropriate search parameters
        /// </summary>
        private void CopyOutSearchCriteria()
        {
            mReportView = (FrmReport)mView;

            if (mReportView.TxtSurname.Enabled) 
            { mSurnameParameter = mReportView.TxtSurname.Text.Trim().Replace("*", "%"); }
            
            if (mReportView.TxtFirstname.Enabled) 
            { mFirstnameParameter = mReportView.TxtFirstname.Text.Trim().Replace("*", "%");}

            if (mReportView.RbtAccessAuthorizationYes.Enabled) 
            { mAccessauthorizeYesParameter = mReportView.RbtAccessAuthorizationYes.Checked; }

            if (mReportView.RbtAccessAuthorizationNo.Enabled) 
            { mAccessauthorizeNoParameter = mReportView.RbtAccessAuthorizationNo.Checked; }

            if (mReportView.RbtStatusYes.Enabled) 
            { mStatusGueltigParameter = mReportView.RbtStatusYes.Checked; }

            if (mReportView.RbtStatusNo.Enabled) 
            { mStatusUngueltigParameter = mReportView.RbtStatusNo.Checked; }

            if (mReportView.txtXDays.Enabled) 
            { mNoBookXDaysParameter = mReportView.txtXDays.Text; }

            if (mReportView.DatMonthFrom.Enabled) 
            { mDynamicDataFromParameter = mReportView.DatMonthFrom.Text; }

            if (mReportView.DatMonthUntil.Enabled) 
            { mDynamicDataUntilParameter = mReportView.DatMonthUntil.Text; }

            if (mReportView.TxtDeliveryDateFrom.Enabled) 
            { mDelDateFromParameter = mReportView.TxtDeliveryDateFrom.Text; }

            if (mReportView.TxtDeliveryDateUntil.Enabled) 
            { mDelDateUntilParameter = mReportView.TxtDeliveryDateUntil.Text; }

            if (mReportView.TxtValidityFrom.Enabled) 
            { mValidFromParameter = mReportView.TxtValidityFrom.Text; }

            if (mReportView.TxtValidityUntil.Enabled) 
            { mValidUntilParameter = mReportView.TxtValidityUntil.Text; }

            if (mReportView.CobExternalContractor.Enabled) 
            { mExContractorParameter = GetSelectedValueFromCbo(mReportView.CobExternalContractor); }

            if (mReportView.CboSupervisor.Enabled) 
            { 
                mSupervisorParameter = GetSelectedIDFromCbo(mReportView.CboSupervisor);
                mSupervisorNameParameter = GetSelectedValueFromCbo(mReportView.CboSupervisor);
            }

            if (mReportView.CobCoordinator.Enabled)
            { mCoordinatorParameter = GetSelectedValueFromCbo(mReportView.CobCoordinator); }

            if (mReportView.CboSubcontractor.Enabled) 
            { mSubContractorParameter = GetSelectedValueFromCbo(mReportView.CboSubcontractor); }

            if (mReportView.CboOrderNumber.Enabled) 
            { mOrdernoParameter = GetSelectedValueFromCbo(mReportView.CboOrderNumber); }

            if (mReportView.CboCraft.Enabled) 
            { mCraftParameter = GetSelectedValueFromCbo(mReportView.CboCraft); }

            if (mReportView.CboPlant.Enabled) 
            { mPlantParameter = GetSelectedValueFromCbo(mReportView.CboPlant); }

            if (mReportView.CboDepartment.Enabled) 
            { mDepartmentParameter = GetSelectedValueFromCbo(mReportView.CboDepartment); }	

            if (mReportView.TxtMaintenanceFrom.Enabled)
            { mMantenanceFromPara = mReportView.TxtMaintenanceFrom.Text; }

            if (mReportView.TxtMaintenanceUntil.Enabled)
            { mMantenanceUntilPara = mReportView.TxtMaintenanceUntil.Text; }

            if (mReportView.TxtMaskNo.Enabled)
            { mMaskNoParameter = mReportView.TxtMaskNo.Text.Trim(); }

            if (mReportView.CbxDelivered.Enabled)
            { mMaskDelivered = mReportView.CbxDelivered.Checked; }

            if (mReportView.CbxReceived.Enabled)
            { mMaskReceived = mReportView.CbxReceived.Checked; }	

            if (mReportView.ClbAttExContractor.Enabled) 
            {
                // Get list of external contractors
                mExContractorNames = new ArrayList();
                foreach (object obj in mReportView.ClbAttExContractor.CheckedItems)
                {
                    LovItem item = (LovItem)obj;
                    mExContractorNames.Add(item.ItemValue);
                }
            }
        }


        /// <summary>
        /// Begins search by determining what type of results are expected and calling correct search
        /// </summary>
        internal void HandleSearch()
        { 
            CheckSearchCriteria();
            CopyOutSearchCriteria();

            switch (mResultsType)
            {
                case ReportNames.ResultTypes.ATTENDANCE:
                    SaveMyExcos();
                    GetAttendance();
                    break;

                case ReportNames.ResultTypes.CHECKLIST:
                    GetCheckList();
                    break;

                case ReportNames.ResultTypes.COWORKER:
                    GetCoWorker();
                    break;

                case ReportNames.ResultTypes.EXCOBOOKING:
                    if (mReportName == ReportNames.EXCO_ATTENDANCE)
                        SaveMyExcos();
                    
                    GetExCoBookings();
                    break;

                case ReportNames.ResultTypes.EXCONTRACTOR:
                    GetExContractor();
                    break;

                case ReportNames.ResultTypes.PLANT:
                    GetPlantManager();
                    break;

                case ReportNames.ResultTypes.RESPMASK:
                    GetRespMask();
                    break;
            }
        }

		#endregion

		#region Reports

		/// <summary>
		/// Ensures that user has selected at least one coworker in GUI results grid.
		/// Only available for <see cref="ReportNames.ResultTypes.COWORKER"/>.
		/// Creates list of selected coworker surnames for display
		/// </summary>
		private decimal SetSelectedCoWorkers()
		{
			mReportView = (FrmReport) mView;
			decimal cwrId;
			mSurnameParameter = "";

			if (mResultsType == ReportNames.ResultTypes.COWORKER)
			{
				// saves Id of first Coworker so we know user has selected
				cwrId = mReportView.ucReportCoWorker1.CoWorkerID;
		
				if (0 >= cwrId && mMustSelectCwr)
				{
					// no CoWorker selected in the datagrid
					throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.REPORT_PASS));
				}
				else
				{
                    DataTable table = (DataTable)mReportView.ucReportCoWorker1.DgrReportCoWorker.DataSource;
				   
                    if (table.Rows.Count > 999)
                    {
                        throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.TOO_MANY_SEARCHVALS));
                    }
 
					// for all rows of results grid (cannot just count visible rows)
					for (int rowIndex = 0; rowIndex < table.Rows.Count; rowIndex++)
					{
						// Remove criteria from old selection
						if (FpassReportSingleton.GetInstance().SearchCriteria.ContainsKey(SEARCHCRITERIA_CWRID + rowIndex.ToString()))
						{
							FpassReportSingleton.GetInstance().SearchCriteria.Remove(SEARCHCRITERIA_CWRID + rowIndex.ToString());
						}
						
						// Add coworker Id at current row index
						if (mReportView.ucReportCoWorker1.DgrReportCoWorker.IsSelected(rowIndex))
						{
							FpassReportSingleton.GetInstance().SearchCriteria.Add(
								SEARCHCRITERIA_CWRID + rowIndex.ToString(), 
								mReportView.ucReportCoWorker1.DgrReportCoWorker[rowIndex, 0]);
						}
					}
				}
			}
			else throw new UIErrorException("Systemfehler: aktuell können nur diskrete FFMA im Grid selektiert werden.");
			
			return cwrId;
		}


		/// <summary>
		/// Generates PDF report corresponding to the selected report in the combobox "Report auswählen"
		/// </summary>
		internal void GenerateReport()
		{
			mReportView = (FrmReport) mView;
            decimal cwrId = 0;
		
			if (mMustSelectCwr
				|| mReportName == ReportNames.CWR_BOOKINGS_EXCO)
			{
				cwrId = SetSelectedCoWorkers();
			}

			// creates report parameters and saves them
			mPDFReportParameters = new FpassReportParameters(mReportName, cwrId);
			FpassReportSingleton.GetInstance().ReportParameters = mPDFReportParameters;

			mPDFReportParameters.SearchCriteria = FpassReportSingleton.GetInstance().SearchCriteria;

			try
			{
				ShowMessageInStatusBar(CON_STATUS_WAIT);
				Cursor.Current = Cursors.WaitCursor;
				Cursor.Show();

                // sets standard data
                // ---------------------------
                SetReportStandardData();

				// evaluates where clause
				// ---------------------------
				string whereMain = mPDFReportParameters.WhereMain;
				
				// "mainWhereClause" has been inititalized where the parameters object has been created
				// after a report has been selected and before a search has been started
				// and contains only the second, fixed part of the where clause for the main select command
				string whereSearchCriteria = FpassReportSingleton.GetInstance().WhereSearchCriteria;
				
				// a report must then been generated with a combination of the two where-clauses
				if ( !(whereMain.Equals("")) )
				{
					// a where clause has been initialized in the report parameter
					if (0 > whereMain.IndexOf("WHERE"))
					{
						 // the where clause in the report parameters has no WHERE keyword
						if (whereSearchCriteria.Equals(""))
						{
							// there is no where clause corresponding to search criteria
							whereSearchCriteria = "WHERE";
						}
						else
						{
							// there is a where clause corresponding to search criteria, which already contains the WHERE keyword
							whereSearchCriteria += " AND";
						}
					}
					else
					{
						whereSearchCriteria = ""; // search criteria must not be used
					}
				}

				// modifies where clause
				mPDFReportParameters.WhereMain = whereSearchCriteria + whereMain;


				// creates corresponding empty report with the parameters
				//-------------------------------------------------------
				FpassReport pdfReport = new FpassReport(mPDFReportParameters);

				// generates pdf report
				//---------------------
				pdfReport.Generate();
				ShowMessageInStatusBar(CON_STATUS_OK);			  			    
			} 
			catch (UIWarningException uwex)
			{
				((FrmReport)mView).SbpMessage.Text = CON_STATUS_ERROR;
				Globals.GetInstance().Log.Fatal("Report-Exception", uwex);
				throw new UIWarningException(uwex.Message, uwex);
			}
			catch (Exception ex)
			{
				((FrmReport)mView).SbpMessage.Text = CON_STATUS_ERROR;
				Globals.GetInstance().Log.Fatal("Report-Exception", ex);
				throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.REPORT_ERROR), ex);
			}
			finally
			{				
				Cursor.Current = Cursors.Default;
			}

            // Attendance reports: check if we need to export an error list
            if (mReportName == ReportNames.CWR_ATTEND_DETAIL
                || mReportName == ReportNames.CWR_ATTENDANCE
                || mReportName == ReportNames.EXCO_ATTENDANCE)
            {
                GenerateExportAttendancePersNo();
            }
		}

	    /// <summary>
        /// Gets information required to generate CSV export
	    /// </summary>
        internal void PreGenerateExport()
	    {
	        mReportView = (FrmReport)mView;

	        // name of selected report
	        mReportName = mReportView.CboReport.Text;
	        decimal prmCoWorkerId = 0;


            if (mReportName == ReportNames.CWR_BOOKINGS)
	        {
	            // the user must select a CoWorker to generate this report
	            prmCoWorkerId = mReportView.ucReportCoWorker1.CoWorkerID;

	            if (-1 == prmCoWorkerId)
	            {
	                // no CoWorker selected in the datagrid
	                throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.REPORT_PASS));
	            }
	        }
            else if (mReportName == ReportNames.CWR_BOOKINGS_EXCO)
	        {
	            // the user can select a CoWorker to generate this export-file
	            prmCoWorkerId = mReportView.ucReportCoWorker1.CoWorkerID;
	        }

	        // creates export parameters and saves parameters
	        //--------------------------
            FpassExportParameters prmExportParameters = new FpassExportParameters(mReportName, prmCoWorkerId);
	        FpassReportSingleton.GetInstance().ExportParameters = prmExportParameters;

	        mReportView.saveFileDialog.FileName = prmExportParameters.FileName;
	        mReportView.saveFileDialog.ShowDialog();
	    }
	    
		/// <summary>
		/// Generates and populates CSV file corresponding to currently selected report
		/// </summary>
		internal void GenerateExport()
		{
			try
			{
				((FrmReport)mView).SbpMessage.Text = CON_STATUS_WAIT;
				Cursor.Current = Cursors.WaitCursor;
				Cursor.Show();
				
				// intializes export parameters corresponding to the reports from the model
				FpassExportParameters exportParameters = FpassReportSingleton.GetInstance().ExportParameters;
				
				// evaluates search criteria
				string mainWhereClause = exportParameters.MainWhereClause;
				string whereClause = FpassReportSingleton.GetInstance().WhereSearchCriteria;
				// mainWhereClause has been inititalized where the parameters object has been created
				// and contains only the second, fixed part of the where clause for the main select command
				if ( !(mainWhereClause.Equals("")) )
				{
					// a where clause has been initialized in the export parameter
					if (0 > mainWhereClause.IndexOf("WHERE")) // Where Clause in parameters has no WHERE keyword
						if (whereClause.Equals("")) // there is no where clause corresponding to search criteria
							whereClause = "WHERE"; // add only WHERE keyword
						else // there is a where clause corresponding to search criteria, which already contains the WHERE keyword
							whereClause = whereClause + " AND"; // combination of two where clauses
					else
						whereClause = ""; // search criteria must not be used
				}
				// sets where clause
				exportParameters.MainWhereClause = whereClause + mainWhereClause;

				// creates export with the parameters
				FpassExport csvExport = new FpassExport(exportParameters);

				// generates csv file from export
				csvExport.Generate();
				((FrmReport)mView).SbpMessage.Text = CON_STATUS_OK;

			}
			catch (UIWarningException uwex)
			{
				((FrmReport)mView).SbpMessage.Text = CON_STATUS_ERROR;
				Globals.GetInstance().Log.Fatal("Export-Exception", uwex);
				throw new UIWarningException(uwex.Message, uwex);
			}
			catch (Exception ex)
			{
				((FrmReport)mView).SbpMessage.Text = CON_STATUS_ERROR;
				Globals.GetInstance().Log.Fatal("Export-Exception", ex);
				throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.EXPORT_ERROR), ex);
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		    
		    // Attendance reports: check if we need to export an error list
            if (mReportName == ReportNames.CWR_ATTEND_DETAIL
                || mReportName == ReportNames.CWR_ATTENDANCE
                || mReportName == ReportNames.EXCO_ATTENDANCE)
            {
                GenerateExportAttendancePersNo();
            }
		}

	    /// <summary>
	    /// Exports a list of bookings where coworker persno is not unique: 
	    /// this notifies user that calculated attendance is incomplete.
        /// Note: this methods initialises its own <see cref="FpassExportParameters"/> instance so as not to overwrite main report/export parameters
        /// Also creates its own where clause
        /// TODO: in a future release: standardise columns in report views so search criteria map to columns in standardised way
	    /// </summary>
        private void GenerateExportAttendancePersNo()
	    {
	        try
	        {
                ShowMessageInStatusBar(MessageSingleton.GetInstance().GetMessage(MessageSingleton.EXPORT_ATTEND_CHECK));
                Cursor.Current = Cursors.WaitCursor;
                Cursor.Show();
	            
	            // Initialise parameters, Set report filename to J:/appsuser
	            FpassExportParameters exportParameters = new FpassExportParameters(ReportNames.CWR_ATTENDANCE_PERSNO, 0);
	            exportParameters.FileName = Globals.GetInstance().ReportsBasePath + "\\" + ReportFilenames.CWR_ATTENDANCE_PERSNO;
 
	            // Build Where clause from search criteria 
                string specificWhere = " WHERE VATT_CWR_MND_ID = " + UserManagementControl.getInstance().CurrentMandatorID;

	            // Coworker
	            if (mSurnameParameter.Length > 0)
	            {
                    specificWhere += " AND VATT_SURNAME = '" + mSurnameParameter + "'";
	            }
	            if (mFirstnameParameter.Length > 0)
	            {
                    specificWhere += " AND VATT_FIRSTNAME = '" + mFirstnameParameter + "'";
	            }
	            
                // List of multiple excontractors 
                if (mExContractorNames != null && mExContractorNames.Count > 0)
                {
                    // Maximum 1000 elements allowed in list IN (..)
                    // Breaks list into blocks of 1000
                    // SQL AND and OR must look like this: AND (exco IN(..) OR exco IN(..) )
                    specificWhere += " AND (VATT_EXCONTRACTOR IN (";

                    for (int i = 0; i < mExContractorNames.Count; i++)
                    {
                        /// Block of 1000 is finished, break up SQL statement
                        /// index number 999
                        /// index number 1998 
                        /// etc
                        if (0 != i && 0 == i % 999)
                        {
                            specificWhere += "'" + mExContractorNames[i].ToString()
                                            + "')"
                                            + " OR VATT_EXCONTRACTOR IN (";
                        }
                        else specificWhere += "'" + mExContractorNames[i].ToString() + "',";

                        mExContractorParameter += " " + mExContractorNames[i].ToString() + ",";
                    }
                    
                    // cut off last comma and close with ")"
                    mExContractorParameter = mExContractorParameter.Remove(mExContractorParameter.Length - 1, 1);
                    specificWhere = specificWhere.Remove(specificWhere.Length - 1, 1);
                    specificWhere += ")) ";
                }
 
	            // Booking times
	            specificWhere +=
	                " AND TO_DATE(VATT_BOOKINGDATE, 'DD.MM.YYYY HH24:MI:SS', 'nls_date_language=German') >= TO_DATE('" +
	                mDynamicDataFromParameter + "', 'DD.MM.YYYY HH24:MI:SS', 'nls_date_language=German')";
	            specificWhere +=
	                " AND TO_DATE(VATT_BOOKINGDATE, 'DD.MM.YYYY HH24:MI:SS', 'nls_date_language=German') <= TO_DATE('" +
                    mDynamicDataUntilParameter + "', 'DD.MM.YYYY HH24:MI:SS', 'nls_date_language=German')";

	            exportParameters.MainWhereClause = specificWhere;
        
	            // creates export with the parameters and generates csv file
	            FpassExport csvExport = new FpassExport(exportParameters); 
	            csvExport.Generate();

                if (csvExport.RowsReturned > 0)
                {
                    // Can open help topic with this messagebox
                    MessageBox.Show(
                        MessageSingleton.GetInstance().GetMessage(MessageSingleton.EXPORT_ATTEND_PERSNO),
                        TitleMessage,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button1,
                        0,
                        Application.StartupPath + AllFPASSDialogs.FPASS_MASTER_HELPFILE,
				        HelpNavigator.Topic,
                        AllFPASSDialogs.HELPTOPIC_REPORTS_ATTENDANCE);
                        

                    if (File.Exists(csvExport.ExportFile))
                    {
                        // Opens CSV file (usually with text editor)
                        ProcessStartInfo info = new ProcessStartInfo();
                        info.FileName = csvExport.ExportFile;
                        Process.Start(info);
                    }
                }
	            
	            else
                {
                    // bit of a quickfix: deletes the file if it contains no rows
                    // TODO: improve this in a future release
                    if (File.Exists(csvExport.ExportFile))
                    {
                        try
                        {
                            File.Delete(csvExport.ExportFile);
                        }
                        catch 
                        { // nowt
                        }
                    }
                    
                }
	            
                ShowMessageInStatusBar(CON_STATUS_OK);
	        }
	        catch (UIWarningException uwex)
	        {
	            ShowMessageInStatusBar(CON_STATUS_ERROR);
	            Globals.GetInstance().Log.Fatal("Export-Exception", uwex);
	            throw new UIWarningException(uwex.Message, uwex);
	        }
	        catch (Exception ex)
	        {
	            ShowMessageInStatusBar(CON_STATUS_ERROR);
	            Globals.GetInstance().Log.Fatal("Export-Exception", ex);
	            throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.EXPORT_ERROR), ex);
	        }
	        finally
	        {
	            Cursor.Current = Cursors.Default;
	        }
	    }
	    
		#endregion Reports

		#region ReportPlantManager

		/// <summary>
		/// Selects Plants and Managers which match the given search criteria:
		/// SELECT statement executed in database
		/// Last change 25.02.2004
		/// Implement sorting in datagrid! DataTable created at runtime
		/// Commented out arraylist
		/// </summary>
		/// <exception cref="UIWarningException">when no search criteria or no results returned</exception>
		public void GetPlantManager() 
		{
			int j = 0;
			mView.Cursor = Cursors.WaitCursor;
			((FrmReport) mView).ucReportPlant.DgrReportPlant.DataSource = null;
			
			try
			{				
				mProvider = DBSingleton.GetInstance().DataProvider;
				IDbCommand selComm  = mProvider.CreateCommand(mQueryName);
				
				selComm.CommandText = SetWhereClausePlant(selComm.CommandText );
                selComm.CommandText += mOrderBy;
			
				// Create datatable at runtime: this is bound to datagrid to allow sorting			
				DataRow row;
				DataTable table = new DataTable("RTTabPlantMan");
				table.Columns.Add( new DataColumn("USPLID") );
				table.Columns.Add( new DataColumn("Name") );
				table.Columns.Add( new DataColumn("Firstname") );
				table.Columns.Add( new DataColumn("Plant") );

				// Open data reader to get PlantManager data
				IDataReader mDR = mProvider.GetReader(selComm);

				while (mDR.Read())
				{
					BOReportPlantManager mBOReportPlantManager = new BOReportPlantManager();
					row	= table.NewRow();

					mBOReportPlantManager.USPLID			= Convert.ToDecimal(mDR["USPL_ID"]);
					mBOReportPlantManager.Name				= mDR["US_NAME"].ToString();
					mBOReportPlantManager.Firstname			= mDR["US_FIRSTNAME"].ToString();
					mBOReportPlantManager.Plant				= mDR["PL_NAME"].ToString();
					
					// arlPlantManager.Add(mBOReportPlantManager);
					row.ItemArray = new object[4] {mBOReportPlantManager.USPLID,
													mBOReportPlantManager.Name,
													mBOReportPlantManager.Firstname,
													mBOReportPlantManager.Plant};

					table.Rows.Add(row);
					j ++;
				}
				mDR.Close();

				// Bind datagrid in Form to datatable, show message in statusbar
				if ( j > 0 ) 
				{
					mView.Cursor = Cursors.Default;
					((FrmReport) mView).BtnGenerateReport.Enabled = true;
					((FrmReport) mView).BtnGenerateExport.Enabled = true;
	
					((FrmReport) mView).ucReportPlant.DgrReportPlant.DataSource = table;
					ShowMessageInStatusBar( "Meldung: " + 	j + " Zuordnungen gefunden" );
				} 
				else 
				{
					mView.Cursor = Cursors.Default;
					((FrmReport) mView).BtnGenerateReport.Enabled = false;
					((FrmReport) mView).BtnGenerateExport.Enabled = false;
					ShowMessageInStatusBar( MessageSingleton.GetInstance().GetMessage(MessageSingleton.NO_RESULTS) );
				}
			}
			catch (OracleException oraex)
			{	
				mView.Cursor = Cursors.Default;
				throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR) 
					+ oraex.Message );
			}

			catch (DbAccessException odr)
			{
				mView.Cursor = Cursors.Default;
				throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR) 
					+ odr.Message );
			}		
		}

		/// <summary>
		/// Generates SQL Where clause for report type Plant
		/// </summary>
		/// <param name="pSelect"></param>
		/// <returns></returns>
		private string SetWhereClausePlant(string pSelect) 
		{
			mWhereClause = " WHERE PL_MND_ID = " + UserManagementControl.getInstance().CurrentMandatorID;

			if (mPlantParameter.Length > 1) 
			{
				mWhereClause += " AND PL_NAME = '" + mPlantParameter + "' "; 
			}

			SaveSearchCriteria(mWhereClause);
			return pSelect + mWhereClause;
		}

		#endregion // End of ReportPlantManager

		#region ReportExContractor

		/// <summary>
		/// Returns ExContractors which match the given search criteria
		/// for generation of report "Excontractors with coordinators"
		/// </summary>
		/// <exception cref="UIWarningException">when no search criteria or no results returned</exception>
		public void GetExContractor() 
		{
			int j = 0;
			mView.Cursor = Cursors.WaitCursor;
			((FrmReport) mView).ucReportExContractor1.DgrReportExContractor.DataSource = null;

			mProvider = DBSingleton.GetInstance().DataProvider;
            IDbCommand selComm = mProvider.CreateCommand(mQueryName);
			selComm.CommandText = SetWhereClauseExContractor(selComm.CommandText);
            selComm.CommandText += mOrderBy;
		
			DataRow row;
			DataTable table = new DataTable("RTTabExContractor");
			table.Columns.Add(new DataColumn("ECODID") );
			table.Columns.Add(new DataColumn("EXCOID") );
			table.Columns.Add(new DataColumn("ExContractor") );
            table.Columns.Add(new DataColumn("Debitno"));
			table.Columns.Add(new DataColumn("Coordinator") );
			table.Columns.Add(new DataColumn("Supervisor") );
			table.Columns.Add(new DataColumn("Subcontractor") );

			// Open data reader to get ExContractor data
			IDataReader mDR = mProvider.GetReader(selComm);

			while (mDR.Read())
			{
				BOExcoCoordinator mBOExcoCoordinator = new BOExcoCoordinator();
				row	= table.NewRow();

				mBOExcoCoordinator.ECODID = Convert.ToDecimal(mDR["ECEC_ECOD_ID"]);
				mBOExcoCoordinator.EXCOID = Convert.ToDecimal(mDR["ECEC_EXCO_ID"]);
				mBOExcoCoordinator.ExContractor = mDR["EXCO_NAME"].ToString();
                mBOExcoCoordinator.DebitNo = mDR["EXCO_DEBITNO"].ToString();
				mBOExcoCoordinator.Coordinator = mDR["COORDINATOR"].ToString();
				mBOExcoCoordinator.Supervisor = mDR["SUPERVISOR"].ToString();
				mBOExcoCoordinator.SubContractor = mDR["SUBCONTRACTOR"].ToString();
				
				row.ItemArray = new object[7] {mBOExcoCoordinator.ECODID,
												  mBOExcoCoordinator.EXCOID,
												  mBOExcoCoordinator.ExContractor,
                                                  mBOExcoCoordinator.DebitNo,
												  mBOExcoCoordinator.Coordinator,
												  mBOExcoCoordinator.Supervisor,
												  mBOExcoCoordinator.SubContractor};
				table.Rows.Add(row);
				j ++;
			}
			mDR.Close();

			// Bind data grid in Form to DataTable
			if ( j > 0 ) 
			{
				mView.Cursor = Cursors.Default;
				((FrmReport)mView).BtnGenerateReport.Enabled = true;
				((FrmReport)mView).BtnGenerateExport.Enabled = true;
				((FrmReport) mView).ucReportExContractor1.DgrReportExContractor.DataSource = table;	
				ShowMessageInStatusBar("Meldung: " + j + " Zuordnungen gefunden");
			} 
			else 
			{
				mView.Cursor = Cursors.Default;
				((FrmReport) mView).BtnGenerateReport.Enabled = false;
				((FrmReport) mView).BtnGenerateExport.Enabled = false;
				ShowMessageInStatusBar(MessageSingleton.GetInstance().GetMessage(MessageSingleton.NO_RESULTS));
			}
		}

        /// <summary>
        /// Sets SQL Where clause for search on Excontractors
        /// </summary>
        /// <param name="pSelect"></param>
        /// <returns></returns>
		private string SetWhereClauseExContractor(String pSelect) 
		{
			mWhereClause = " WHERE EXCO_MND_ID = " + UserManagementControl.getInstance().CurrentMandatorID;

			if ( mExContractorParameter.Length > 0 ) 
			{
				mWhereClause += " AND EXCO_NAME = '" +  mExContractorParameter + "' ";
			}
			if ( mSupervisorParameter.Length > 1 ) 
			{
				// Get supervisor using ID of ext. contractor
				mWhereClause += " AND ECEC_EXCO_ID = " + mSupervisorParameter; 
			}
			if ( mCoordinatorParameter.Length > 1 ) 
			{
				mWhereClause += " AND COORDINATOR = '" + mCoordinatorParameter + "' "; 
			}
			if ( mSubContractorParameter.Length > 1 ) 
			{
				mWhereClause += " AND SUBCONTRACTOR = '" + mSubContractorParameter + "' "; 
			}
			SaveSearchCriteria(mWhereClause);
			return pSelect + mWhereClause;
		}

		#endregion
		
		#region ReportCoworker

		/// <summary>
		/// Selects Coworkers from DB and loads them into datagrid
		/// Hashtable is used to ensure each record appears only once in gui (view can return a cwr more than once)
		/// DataTable created at runtime and filled from hashtable to allow sorting in grid
		/// </summary>
		/// <exception cref="UIWarningException">when no search criteria or no results returned</exception>
		public void GetCoWorker() 
		{
			int j = 0;
			mView.Cursor = Cursors.WaitCursor;
			((FrmReport) mView).ucReportCoWorker1.DgrReportCoWorker.DataSource = null;
		    
			/// Get DataProvider from DbAccess component, Create dummy  select command
			/// Build SQL WHERE using search criteria
			mProvider = DBSingleton.GetInstance().DataProvider;
			IDbCommand selComm  = mProvider.CreateCommand(mQueryName);			
			
            selComm.CommandText = 
				SetWhereClauseCoworker(selComm.CommandText );
            selComm.CommandText += mOrderBy;
			

			// Check if in all HashTables equal ID's exist
			Hashtable Resultstable = new Hashtable();

			// Create datatable at runtime: this is bound to datagrid to allow sorting			
			DataRow row;
			DataTable table = new DataTable("RTTabCoWorker");
			table.Columns.Add(new DataColumn(CoWorkerSearch.ID_COL));
			table.Columns.Add(new DataColumn(CoWorkerSearch.SURNAME));
			table.Columns.Add(new DataColumn(CoWorkerSearch.FIRSTNAME));
			table.Columns.Add(new DataColumn(CoWorkerSearch.DATE_OF_BIRTH, typeof(DateTime)));
			table.Columns.Add(new DataColumn(CoWorkerSearch.ACCESS));
			table.Columns.Add(new DataColumn(CoWorkerSearch.STATUS));
            table.Columns.Add(new DataColumn(CoWorkerSearch.VALID_UNTIL, typeof(DateTime)));			
			table.Columns.Add(new DataColumn("ExContractorName"));
			table.Columns.Add(new DataColumn("SuperNameAndTel"));		
			table.Columns.Add(new DataColumn("SubContractor"));
			table.Columns.Add(new DataColumn("CoordNameAndTel"));	
			table.Columns.Add(new DataColumn(CoWorkerSearch.ZKS_RET));
				
			// Open data reader to get CoWorker data
			IDataReader mDR = mProvider.GetReader(selComm);

			// Loop thru records
			while (mDR.Read())
			{
				mCoWorkerBO = new CoWorkerSearch();

				mCoWorkerBO.CoWorkerId = Convert.ToDecimal(mDR["CWR_ID"]);
				mCoWorkerBO.Surname = mDR["CWR_SURNAME"].ToString();
				mCoWorkerBO.Firstname = mDR["CWR_FIRSTNAME"].ToString();
				mCoWorkerBO.Access = mDR["CWR_ACCESS"].ToString();
				mCoWorkerBO.DateOfBirth = Convert.ToDateTime(mDR["CWR_DATEOFBIRTH"]).ToString("dd.MM.yyyy");
				mCoWorkerBO.Status = mDR["CWR_STATUS"].ToString();
				try 
				{
					mCoWorkerBO.ValidUntil = Convert.ToDateTime(mDR["CWR_VALIDUNTIL"]).ToString("dd.MM.yyyy");
				}
				catch ( InvalidCastException ) 
				{
					// Swallow it
				}
				mCoWorkerBO.ExContractorName	= mDR["EXTCON"].ToString();
				mCoWorkerBO.SuperNameAndTel		= mDR["SUPERVISOR"].ToString()+ " " + "(" + mDR["SUPERTEL"].ToString()+ ")";
				mCoWorkerBO.SubContractor		= mDR["SUBCON"].ToString();
				mCoWorkerBO.CoordNameAndTel		= mDR["COORDINATOR"].ToString()+ " " + "(" + mDR["VWC_TEL"].ToString()+ ")";
				mCoWorkerBO.ZKSReturncode		= mDR["CWR_RETURNCODE_ZKS"].ToString();
							
				// Add record to hashtable.
				// Some records appear in DB view more than once, hashtable ensures each entry appears only once on form
				if ( ! Resultstable.ContainsKey(mCoWorkerBO.CoWorkerId ) ) 
				{
					Resultstable.Add(mCoWorkerBO.CoWorkerId, mCoWorkerBO);
				}
			}
			mDR.Close();

			foreach ( CoWorkerSearch bco in Resultstable.Values )
			{
				row	= table.NewRow();
				row.ItemArray = new object[12] {bco.CoWorkerId,
												   bco.Surname,
												   bco.Firstname,
												   bco.DateOfBirth,
												   bco.Access,
												   bco.Status,
												   bco.ValidUntil,
												   bco.ExContractorName,
												   bco.SuperNameAndTel,		
												   bco.SubContractor, 
												   bco.CoordNameAndTel, 												   
												   bco.ZKSReturncode};
				table.Rows.Add(row);
				j ++;
			}
	 
			if ( j > 0 ) 
			{	
				// Bind data grid in Form to datatable		
				((FrmReport)mView).BtnGenerateReport.Enabled = true;
				((FrmReport)mView).BtnGenerateExport.Enabled = true;

				((FrmReport) mView).ucReportCoWorker1.DgrReportCoWorker.DataSource = table;					
				mView.Cursor = Cursors.Default;
				ShowMessageInStatusBar( "Meldung: " + 	j + " Fremdfirmenmitarbeiter gefunden" );
			} 
			else 
			{			
				// No results
				((FrmReport) mView).BtnGenerateReport.Enabled = false;
				((FrmReport) mView).BtnGenerateExport.Enabled = false;

				mView.Cursor = Cursors.Default;
				ShowMessageInStatusBar( MessageSingleton.GetInstance().GetMessage(MessageSingleton.NO_RESULTS) );
			}
		}

		/// <summary>
		/// Generates SQL WHERE text for reports based on coworker
		/// WHERE ... is always current mandant (value from FPASS Wesseling)
		/// AND .. all other search criteria
		/// Date strings formatted with to_date to avoid ORA-01843 in Degussa Ora Client env.
		/// </summary>
		/// <param name="pSelect">select statement as read from Configuration</param>
		/// <returns></returns>
		private string SetWhereClauseCoworker(string pSelect) 
		{
			mWhereClause = " WHERE CWR_MND_ID = " + UserManagementControl.getInstance().CurrentMandatorID;

			if (mSurnameParameter.Length > 0 ) 
			{
				mWhereClause = mWhereClause + " AND UPPER(CWR_SURNAME) LIKE '" +  mSurnameParameter.Trim().ToUpper() + "%' ";
			}
			if ( mFirstnameParameter.Length > 1 ) 
			{
				mWhereClause = mWhereClause + " AND UPPER(CWR_FIRSTNAME) LIKE '" + mFirstnameParameter.ToUpper() + "%' "; 
			}
			if (mAccessauthorizeYesParameter) 
			{
				mWhereClause = mWhereClause + " AND RATH_RECEPTAUTHO_YN  = 'Y'"; 
			}
			if (mAccessauthorizeNoParameter) 
			{
				mWhereClause = mWhereClause + " AND (RATH_RECEPTAUTHO_YN  = 'N' OR RATH_RECEPTAUTHO_YN IS NULL )"; 
			}
			if (mStatusGueltigParameter) 
			{
				mWhereClause = mWhereClause + " AND CWR_STATUS  = 'GÜLTIG'"; 
			}
			if (mStatusUngueltigParameter) 
			{
				mWhereClause = mWhereClause + " AND CWR_STATUS  = 'UNGÜLTIG'"; 
			}
			if ( mDelDateFromParameter.Length > 1 &&  mDelDateUntilParameter.Length > 1) 
			{
				mWhereClause = mWhereClause + " AND TRUNC(CWR_DATECREATED) BETWEEN TO_DATE('" 
											+ mDelDateFromParameter 
											+ "', 'DD.MM.YYYY') AND TO_DATE( '" 
											+ mDelDateUntilParameter 
											+ "', 'DD.MM.YYYY')";
			}
			if ( mDelDateFromParameter.Length > 1 &&  mDelDateUntilParameter.Length == 0 ) 
			{
				mWhereClause = mWhereClause + " AND TRUNC(CWR_DATECREATED) >= TO_DATE('" 
										  + mDelDateFromParameter 
										  + "', 'DD.MM.YYYY') ";
			}
			if ( mDelDateFromParameter.Length == 0 &&  mDelDateUntilParameter.Length > 1 ) 
			{
				mWhereClause = mWhereClause + " AND TRUNC(CWR_DATECREATED) <= TO_DATE('"  
											+ mDelDateUntilParameter 
											+ "', 'DD.MM.YYYY') ";
			} 
			if ( mValidFromParameter.Length > 1 &&  mValidUntilParameter.Length > 1) 
			{
				mWhereClause = mWhereClause + " AND TRUNC(CWR_VALIDUNTIL) BETWEEN TO_DATE('"
											+ mValidFromParameter 
											+ "', 'DD.MM.YYYY') AND TO_DATE( '" 
											+ mValidUntilParameter 
											+ "', 'DD.MM.YYYY') ";
			}
			if ( mValidFromParameter.Length > 1 &&  mValidUntilParameter.Length == 0 ) 
			{
				mWhereClause = mWhereClause + " AND TRUNC(CWR_VALIDUNTIL) >= TO_DATE('" 
											+ mValidFromParameter 
											+ "', 'DD.MM.YYYY') ";
			}
			if ( mValidFromParameter.Length == 0 &&  mValidUntilParameter.Length > 1 ) 
			{
				mWhereClause = mWhereClause + " AND TRUNC(CWR_VALIDUNTIL) <= TO_DATE('"  
											+ mValidUntilParameter 
											+ "', 'DD.MM.YYYY') ";
			} 
			if ( mExContractorParameter.Length > 0 ) 
			{
				mWhereClause = mWhereClause + " AND EXTCON = '" +  mExContractorParameter + "' ";
			}

			if ( mSupervisorParameter.Length > 1 ) 
			{
				// Get supervisor using ID of ext. contractor
				mWhereClause = mWhereClause + " AND CWR_EXCO_ID = " + mSupervisorParameter; 
			}
			if ( mCoordinatorParameter.Length > 1 ) 
			{
				mWhereClause = mWhereClause + " AND COORDINATOR = '" + mCoordinatorParameter + "' "; 
			}
			if ( mSubContractorParameter.Length > 1 ) 
			{
				mWhereClause = mWhereClause + " AND SUBCON = '" + mSubContractorParameter + "' "; 
			}
			if ( mOrdernoParameter.Length > 0 ) 
			{
				mWhereClause = mWhereClause + " AND CWR_ORDERNO = '" +  mOrdernoParameter + "' ";
			}
			if ( mCraftParameter.Length > 1 ) 
			{
				mWhereClause = mWhereClause + " AND CRA_CRAFTNOTATION = '" + mCraftParameter + "' "; 
			}
			if ( mPlantParameter.Length > 1 ) 
			{
				mWhereClause = mWhereClause + " AND PL_NAME = '" + mPlantParameter + "' "; 
			}
			if ( mDepartmentParameter.Length > 1 ) 
			{
				mWhereClause = mWhereClause + " AND DEPT_DEPARTMENT = '" + mDepartmentParameter + "' "; 
			}
			
			mReportView = (FrmReport)mView;
			if (mReportView.DatMonthFrom.Enabled)
			{
				if ( mReportView.CboReport.Text == ReportNames.CWR_BOOKINGS
					|| mReportView.CboReport.Text == ReportNames.EXCO_BOOKINGS_SUM
					|| mReportView.CboReport.Text == ReportNames.CWR_BOOKINGS_EXCO )
				{
					mWhereClause += " AND TRUNC(DYFP_DATE) BETWEEN TO_DATE('" 
						+ mDynamicDataFromParameter 
						+ "', 'DD.MM.YYYY') AND TO_DATE( '" 
						+ mDynamicDataUntilParameter 
						+ "', 'DD.MM.YYYY')";
				}				
			}
			if ( mNoBookXDaysParameter.Length > 0 )
			{
				mWhereClause += " AND TRUNC(LASTBOOK) <= TRUNC(SYSDATE) -" 
								+ mNoBookXDaysParameter;
			}

			SaveSearchCriteria(mWhereClause);
			return pSelect + mWhereClause;
		}

		#endregion // End of ReportCoworker

		#region ReportCheckList

		/// <summary>
		/// Returns Coworkers which match the given search criteria and show datagrid in UserControl for Checklist
		/// 23.01.04: catch exception if error in getting decimal PersNo (not all in ZKS have a persNo)
		/// 25.02.04:
		/// Implement sorting in datagrid! DataTable created at runtime, Commented out arraylist
		/// Default data type for the columns is String, Decimal and DateTime are also used here so that sorting is correct
		/// </summary>
		/// <exception cref="UIWarningException">when no search criteria or no results returned</exception>
		public void GetCheckList() 
		{
			int j = 0;
			mView.Cursor = Cursors.WaitCursor;
			((FrmReport) mView).ucReportChecklist1.DgrChecklist.DataSource = null;

			// read the search criteria from the gui
			//CopyOutSearchCriteriaCheckList();
            //CopyOutSearchCriteria();

			// Get DataProvider from DbAccess component, Create the select command, Build SQL WHERE
			mProvider = DBSingleton.GetInstance().DataProvider;
			IDbCommand selComm  = mProvider.CreateCommand(CHECKLIST_QUERY);
			
            selComm.CommandText = SetWhereClauseCheckList(selComm.CommandText );
            selComm.CommandText += mOrderBy;

		
			// Create datatable at runtime: this is bound to datagrid to allow sorting			
			DataRow row;
			DataTable table = new DataTable("RTTabCheckList");
			table.Columns.Add( new DataColumn("CHLSID") );
			table.Columns.Add( new DataColumn("Status") );
			table.Columns.Add( new DataColumn("TK", typeof(Decimal)) );
			table.Columns.Add( new DataColumn("PersNo") );
			table.Columns.Add( new DataColumn("MifareNoFpass", typeof(Decimal)) );
			table.Columns.Add( new DataColumn("MifareNoZks", typeof(Decimal)) );
			table.Columns.Add( new DataColumn("Surname") );
			table.Columns.Add( new DataColumn("FirstName") );
			table.Columns.Add( new DataColumn("ValidFrom", typeof(DateTime)) );	
			table.Columns.Add( new DataColumn("ValidUntil", typeof(DateTime)) );	
			table.Columns.Add( new DataColumn("Authorised") );	
			table.Columns.Add( new DataColumn("ExContractor") );
			table.Columns.Add( new DataColumn("SubContractor") );		

			// Open data reader to get Checklist data
			IDataReader mDR = mProvider.GetReader(selComm);

			// Loop thru records and create CheckList BO for each record
			while (mDR.Read())
			{
				BOReportsCheckList mBOCheckList	= new BOReportsCheckList();
				row								= table.NewRow();

				mBOCheckList.CHLSID				= Convert.ToDecimal(mDR["CHLS_ID"]);
				mBOCheckList.Surname			= mDR["CHLS_SURNAME"].ToString();
				mBOCheckList.Firstname			= mDR["CHLS_FIRSTNAME"].ToString();
				mBOCheckList.ExContractor		= mDR["CHLS_EXCONTRACTOR"].ToString();

                // Show Mifare ID card numbers and convert to decimal for easy sorting
                if (!mDR["CHLS_IDCARDMIFAREFPASS"].Equals(DBNull.Value)) 
				{
                    mBOCheckList.IDCardNoFpass = Convert.ToDecimal(mDR["CHLS_IDCARDMIFAREFPASS"]);
				}
                if (!mDR["CHLS_IDCARDMIFAREZKS"].Equals(DBNull.Value)) 
				{
                    mBOCheckList.IDCardNoZks = Convert.ToDecimal(mDR["CHLS_IDCARDMIFAREZKS"]);
				}
				
				try 
				{
					mBOCheckList.PersNo			= Convert.ToDecimal(mDR["CHLS_PERSNO"]);
				} 
				catch ( Exception ) 
				{
					mBOCheckList.PersNo			= 0;
				}

				mBOCheckList.Status				= mDR["CHLS_STATUS"].ToString();
				mBOCheckList.SubContractor		= mDR["CHLS_SUBCONTRACTOR"].ToString();
				
				try 
				{
					mBOCheckList.TK				= Convert.ToDecimal(mDR["CHLS_TK"]);
				} 
				catch ( Exception ) 
				{
					mBOCheckList.TK				= 0;
				}
				try 
				{
					mBOCheckList.ValidFrom		= Convert.ToDateTime(mDR["CHLS_VALIDEFROM"]).ToString("dd.MM.yyyy");
				} 
				catch ( InvalidCastException ) 
				{

				}
				try 
				{
					mBOCheckList.ValidUntil		= Convert.ToDateTime(mDR["CHLS_VALIDEUNTIL"]).ToString("dd.MM.yyyy");
				}
				catch ( InvalidCastException ) 
				{
				
				}
				mBOCheckList.Authorised			= mDR["CHLS_AUTHORISED_YN"].ToString();			
				
				//arlCheckList.Add(mBOCheckList);
				// Add attributes of current BO to Row
				row.ItemArray = new object[13] {mBOCheckList.CHLSID,
											    mBOCheckList.Status,
											    mBOCheckList.TK,
												mBOCheckList.PersNo,
												mBOCheckList.IDCardNoFpass,
												mBOCheckList.IDCardNoZks,
												mBOCheckList.Surname,
												mBOCheckList.Firstname,
												mBOCheckList.ValidFrom,
											    mBOCheckList.ValidUntil,
												mBOCheckList.Authorised,
												mBOCheckList.ExContractor,
												mBOCheckList.SubContractor};	
				table.Rows.Add(row);
				j ++;
			}
			mDR.Close();

			// Bind data grid in Form to DataTable
			if ( j > 0 ) 
			{
				mView.Cursor = Cursors.Default;
				((FrmReport) mView).BtnGenerateReport.Enabled = true;
				((FrmReport) mView).BtnGenerateExport.Enabled = true;

				((FrmReport) mView).ucReportChecklist1.DgrChecklist.DataSource = table;	
				ShowMessageInStatusBar( "Meldung: " + 	j + " Fremdfirmenmitarbeiter gefunden" );
			} 
			else 
			{
				mView.Cursor = Cursors.Default;
				((FrmReport) mView).BtnGenerateReport.Enabled = false;
				((FrmReport) mView).BtnGenerateExport.Enabled = false;
				ShowMessageInStatusBar( MessageSingleton.GetInstance().GetMessage(MessageSingleton.NO_RESULTS) );
			}
		}

		
		/// <summary>
		/// Generates SQL WHERE clause for SELECT statement for Checklist Reoprt 
		/// ("Bestandsvergleich FPASS ZKS")
		/// 15.12.03: Mandator dependency is required, added mnd_id to SQL WHERE  
		/// 16.03.04: Use to_date in SQL WHERE to avoid ORA-01843
		/// </summary>
		/// <param name="pSelect">main part of SQL string</param>
		/// <returns>SQL string inclusive of WHERE clause</returns>
		private String SetWhereClauseCheckList(string pSelect) 
		{
			mWhereClause = " WHERE CHLS_MND_ID = " + UserManagementControl.getInstance().CurrentMandatorID.ToString();
			mwhereSet = true;
			
			if ( mSurnameParameter.Length > 0 ) 
			{
				if ( mwhereSet ) 
				{
					mWhereClause = mWhereClause + " AND ";
				} 
				mWhereClause = mWhereClause + "UPPER(CHLS_SURNAME) LIKE '" +  mSurnameParameter.Trim().ToUpper() + "%' ";
				mwhereSet = true;
			}

			if ( mFirstnameParameter.Length > 1 ) 
			{
				if ( mwhereSet ) 
				{
					mWhereClause = mWhereClause + " AND ";
				} 
				mWhereClause = mWhereClause + "UPPER(CHLS_FIRSTNAME) LIKE '" + mFirstnameParameter.Trim().ToUpper() + "%' "; 
				mwhereSet = true;
			}

			if ( mAccessauthorizeYesParameter) 
			{
				if ( mwhereSet ) 
				{
					mWhereClause = mWhereClause + " AND ";
				} 
				mWhereClause = mWhereClause + "CHLS_AUTHORISED_YN  = 'Y'"; 
				mwhereSet = true;
			}

			if ( mAccessauthorizeNoParameter) 
			{
				if ( mwhereSet ) 
				{
					mWhereClause = mWhereClause + " AND ";
				}
				mWhereClause = mWhereClause + "CHLS_AUTHORISED_YN  = 'N'"; 
				mwhereSet = true;
			}

			if ( mValidFromParameter.Length > 1 &&  mValidUntilParameter.Length > 1) 
			{
				if ( mwhereSet ) 
				{
					mWhereClause = mWhereClause + " AND ";
				}
				mWhereClause = mWhereClause + "TRUNC(CHLS_VALIDEUNTIL) BETWEEN TO_DATE('" 
											+ mValidFromParameter 
											+ "', 'DD.MM.YYYY' ) AND TO_DATE( '"  
											+ mValidUntilParameter 
											+ "', 'DD.MM.YYYY' )"; 
				mwhereSet = true;
			}

			if ( mValidFromParameter.Length > 1 &&  mValidUntilParameter.Length == 0 ) 
			{
				if ( mwhereSet ) 
				{
					mWhereClause = mWhereClause + " AND ";
				}
				mWhereClause = mWhereClause + "TRUNC(CHLS_VALIDEUNTIL) >= TO_DATE('" 
											+ mValidFromParameter 
											+ "', 'DD.MM.YYYY' ) ";
				mwhereSet = true;
			}

			if ( mValidFromParameter.Length == 0 &&  mValidUntilParameter.Length > 1 ) 
			{
				if ( mwhereSet ) 
				{
					mWhereClause = mWhereClause + " AND ";
				}
				mWhereClause = mWhereClause + "TRUNC(CHLS_VALIDEUNTIL) <= TO_DATE('"  
										+ mValidUntilParameter 
										+ "', 'DD.MM.YYYY' ) ";
				mwhereSet = true;
			} 

			if ( mExContractorParameter.Length > 0 ) 
			{
				if ( mwhereSet ) 
				{
					mWhereClause = mWhereClause + " AND ";
				}
				mWhereClause = mWhereClause + "CHLS_EXCONTRACTOR = '" +  mExContractorParameter + "' ";
				mwhereSet = true;
			}

			if ( mSubContractorParameter.Length > 1 ) 
			{
				if ( mwhereSet ) 
				{
					mWhereClause = mWhereClause + " AND ";
				}
				mWhereClause = mWhereClause + "CHLS_SUBCONTRACTOR = '" + mSubContractorParameter + "' "; 
				mwhereSet = true;
			}
			SaveSearchCriteria(mWhereClause);
			return pSelect + mWhereClause;
		}
	    
		#endregion ReportChecklist

		#region ReportExConBookings

		/// <summary>
		/// Reads search parameters from GUI for report for external contractor bookings
		/// and creates SQL Select with correct search parameters.
		/// </summary>
		/// <param name="pCommand">current SQL command</param>
		private void SetSelectExContrBookings(IDbCommand pCommand)
		{
            mReportView = (FrmReport)mView;
			mWhereClause = " WHERE EXCO_MND_ID = " + UserManagementControl.getInstance().CurrentMandatorID;
	
			if (mExContractorParameter.Length > 0) 
			{
                mWhereClause += " AND EXCO_NAME = '" + mExContractorParameter + "' ";
			}
			if (mSupervisorParameter.Length > 1) 
			{
				// Get supervisor using ID of ext. contractor
                mWhereClause += " AND EXCO_ID = " + mSupervisorParameter; 
			}
			if (mCoordinatorParameter.Length > 1) 
			{
                mWhereClause += " AND COORDINATOR = '" + mCoordinatorParameter + "' "; 
			}
			if (mSubContractorParameter.Length > 1) 
			{
                mWhereClause += " AND SUBCONTRACTOR = '" + mSubContractorParameter + "' "; 
			}

            mWhereClause += " AND DYFP_DATE BETWEEN TO_DATE('"
			              + mDynamicDataFromParameter
			              + "', 'DD.MM.YYYY', 'nls_date_language=German') AND TO_DATE('"
			              + mDynamicDataUntilParameter
			              + "', 'DD.MM.YYYY', 'nls_date_language=German')";

            SaveSearchCriteria(mWhereClause);
            pCommand.CommandText += mWhereClause + mOrderBy;
		}

		/// <summary>
		/// Returns and displays ExContractor data which match the given search criteria
		/// Implements sorting in datagrid by creating DataTable created at runtime
		/// </summary>
		/// <exception cref="UIWarningException">when no search criteria or no results returned</exception>
		public void GetExCoBookings() 
		{
			int j = 0;
			mView.Cursor = Cursors.WaitCursor;
			((FrmReport) mView).ucReportExConBooking.DgrReportExContr.DataSource = null;
			
			try
			{
				mProvider = DBSingleton.GetInstance().DataProvider;
				IDbCommand selComm  = mProvider.CreateCommand(mQueryName);
				
				if (mReportName == ReportNames.EXCO_BOOKINGS_SUM)
				{
					SetSelectExContrBookings(selComm);
				}
				else if (mReportName == ReportNames.EXCO_ATTENDANCE)
				{
					SetSelectExCoAttendance(selComm);
				}
				else
				{
					throw new UIErrorException("Systemfehler: falscher Report-Aufruf. " + this.ToString());
				}

				DataRow row;
				DataTable table = new DataTable("TableExConBooking");
				table.Columns.Add( new DataColumn("ExcoID") );
				table.Columns.Add( new DataColumn("ExcoName") );
				table.Columns.Add( new DataColumn("ExcoDebitNo") );
				table.Columns.Add( new DataColumn("ExcoSupervisor") );
				table.Columns.Add( new DataColumn("ExcoTelephone") );				
				table.Columns.Add( new DataColumn("ExcoStreet") );
				table.Columns.Add( new DataColumn("ExcoPostcode") );
				table.Columns.Add( new DataColumn("ExcoCity") );
				
				IDataReader mDR = mProvider.GetReader(selComm);
				while (mDR.Read())
				{
					BOReportExContractor mBOReportExContr = new BOReportExContractor();
					row = table.NewRow();

					mBOReportExContr.ExcoID			= Convert.ToDecimal(mDR["EXCO_ID"]);
					mBOReportExContr.ExcoName		= mDR["EXCO_NAME"].ToString();
					mBOReportExContr.ExcoDebitNo	= mDR["EXCO_DEBITNO"].ToString();
					mBOReportExContr.ExcoSupervisor = mDR["EXCO_SUPERVISOR"].ToString();
					mBOReportExContr.ExcoTelephone  = mDR["EXCO_TELEPHONENO"].ToString();
					mBOReportExContr.ExcoStreet		= mDR["EXCO_STREET"].ToString();
					mBOReportExContr.ExcoPostcode	= mDR["EXCO_POSTCODE"].ToString();
					mBOReportExContr.ExcoCity		= mDR["EXCO_CITY"].ToString();

					// Create array containing BO attributes and add to datatable
					row.ItemArray = new object[8] {mBOReportExContr.ExcoID,
													mBOReportExContr.ExcoName,
													mBOReportExContr.ExcoDebitNo,
													mBOReportExContr.ExcoSupervisor,
													mBOReportExContr.ExcoTelephone,
													mBOReportExContr.ExcoStreet,
													mBOReportExContr.ExcoPostcode,
													mBOReportExContr.ExcoCity};
					table.Rows.Add(row);
					j ++;					
				}
				mDR.Close();

				// Bind data grid in Form to DataTable if > 0 rows returned
				if ( j > 0 ) 
				{
					mView.Cursor = Cursors.Default;
					((FrmReport) mView).BtnGenerateReport.Enabled = true;
					((FrmReport) mView).BtnGenerateExport.Enabled = true;
					((FrmReport) mView).ucReportExConBooking.DgrReportExContr.DataSource = table;
					ShowMessageInStatusBar( "Meldung: " + j + " Fremdfirmen gefunden" );
				} 
				else 
				{
					mView.Cursor = Cursors.Default;
					((FrmReport) mView).BtnGenerateReport.Enabled = false;
					((FrmReport) mView).BtnGenerateExport.Enabled = false;
					ShowMessageInStatusBar( MessageSingleton.GetInstance().GetMessage(MessageSingleton.NO_RESULTS) );
				}
			}
			catch (OracleException oraex)
			{	
				mView.Cursor = Cursors.Default;
				throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR) 
					+ oraex.Message );
			}
			catch (DbAccessException odr)
			{
				mView.Cursor = Cursors.Default;
				throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR) 
					+ odr.Message );
			}		
		}

		/// <summary>
		/// Gets search parameters from GUI and generates where clause for report Leistungsverrechnung FF (attendance by exco).
		/// This report is different because search results are grouped within selected dates at runtime
		/// TODO in future release: combine all these SetWhere methods into one generic
		/// </summary>
		/// <param name="pCommand">current <see cref="IDbCommand"/>COMMAND</param>
		/// <returns></returns>
		private void SetSelectExCoAttendance(IDbCommand pCommand)
		{
			mReportView = (FrmReport) mView;
			mExContractorParameter = "";
            
            // Make WHERE clause. 
            mWhereClause = " WHERE EXCO_MND_ID = " + UserManagementControl.getInstance().CurrentMandatorID;

			// Make IN list AND xxx IN ('A', 'B',..)
			// also fill mExContractorParameter search parameter with list of names
			if (mExContractorNames != null && mExContractorNames.Count > 0)
			{
                // Maximum 1000 elements allowed in list IN (..)
                // Breaks list into blocks of 1000
                // SQL AND and OR must look like this: AND (exco IN(..) OR exco IN(..) )
                mWhereClause += " AND (EXCO_NAME IN (";
                for (int i = 0; i < mExContractorNames.Count; i++)
                {
                    /// Block of 1000 is finished, break up SQL statement
                    /// index number 999
                    /// index number 1998 
                    /// etc
                    if (0 != i && 0 == i % 99)
                    {
                        mWhereClause += "'" + mExContractorNames[i].ToString() 
                                        + "')"
                                        + " OR EXCO_NAME IN (";
                    }
                    else mWhereClause += "'" + mExContractorNames[i].ToString() + "',";
                    
                    mExContractorParameter += " " + mExContractorNames[i].ToString() + ",";
                }
			    
				// cut off last comma and close with ")"
				mExContractorParameter = mExContractorParameter.Remove(mExContractorParameter.Length - 1, 1);
                mWhereClause = mWhereClause.Remove(mWhereClause.Length - 1, 1);
                mWhereClause += "))";
			}

			// date from and until
            mWhereClause += " AND EXCO_DAY BETWEEN to_date('"
				+ mDynamicDataFromParameter
				+ "', 'DD.MM.YYYY', 'nls_date_language=German') AND to_date('"
				+ mDynamicDataUntilParameter
				+ "', 'DD.MM.YYYY', 'nls_date_language=German')";

            SaveSearchCriteria(mWhereClause);

			// Note use of GROUP BY
			string groupBy = " GROUP BY EXCO_ID, EXCO_MND_ID, EXCO_NAME, EXCO_DEBITNO, EXCO_SUPERVISOR, EXCO_TELEPHONENO, EXCO_STREET, EXCO_POSTCODE, EXCO_CITY";

            pCommand.CommandText += mWhereClause + groupBy + mOrderBy;
		}

		#endregion 

		#region ReportRespMask

		/// <summary>
		/// Selects Coworkers from DB and loads them into datagrid
		/// DataTable created at runtime and filled from hashtable to allow sorting in grid
		/// Make sure columns containing dates are of datatype Date so they can be sorted properly
		/// </summary>
		/// <exception cref="UIWarningException">when no search criteria or no results returned</exception>
		public void GetRespMask() 
		{
			int j = 0;
            mReportView = (FrmReport)mView;
			mView.Cursor = Cursors.WaitCursor;
            mReportView.ucReportRespMask1.DgrReportRepMask.DataSource = null;

			try
			{	
				mProvider = DBSingleton.GetInstance().DataProvider;
				IDbCommand selComm  = mProvider.CreateCommand(mQueryName);
				
                selComm.CommandText = SetWhereClauseRespMask(selComm.CommandText);
                selComm.CommandText += mOrderBy;

				// Create datatable at runtime: this is bound to datagrid to allow sorting			
				DataRow row;
				DataTable table = new DataTable("RTTabRespMask");
				table.Columns.Add(new DataColumn("CwrID"));
                table.Columns.Add(new DataColumn("MaskNo"));
                table.Columns.Add(new DataColumn("MaskSystem"));
				table.Columns.Add(new DataColumn("MaskReceived", typeof(DateTime) ));
				table.Columns.Add(new DataColumn("MaskDelivered", typeof(DateTime) ));
				table.Columns.Add(new DataColumn("FFMA"));
				table.Columns.Add(new DataColumn("Coordinator"));
				table.Columns.Add(new DataColumn("TelCoordinator"));			
				table.Columns.Add(new DataColumn("ExContractor"));
				table.Columns.Add(new DataColumn("TelExContractor"));
                table.Columns.Add(new DataColumn("MaskService", typeof(DateTime)));
							
				// Open data reader to get RespMask data
				IDataReader mDR = mProvider.GetReader(selComm);

				// Loop thru records
				while (mDR.Read())
				{
					BOReportRespMask mBORespMask = new BOReportRespMask();
					row = table.NewRow();

					mBORespMask.CwrID = Convert.ToDecimal(mDR["CWR_ID"]);
					mBORespMask.MaskNo = mDR["REMA_MASKNO"].ToString();
                    mBORespMask.MaskSystem = mDR["MASKSYSTEM"].ToString();
					try
					{
                        mBORespMask.MaskReceived = Convert.ToDateTime(mDR["MASKDATELENT"]).ToString("dd.MM.yyyy HH:mm:ss");
					}
					catch (InvalidCastException)
					{
						// Swallow it
					}

					try
					{
                        mBORespMask.MaskDelivered = Convert.ToDateTime(mDR["MASKDATERETURN"]).ToString("dd.MM.yyyy HH:mm:ss");
					}
					catch (InvalidCastException)
					{
						// Swallow it
					}
                    mBORespMask.FFMA = mDR["COWORKER"].ToString();
                    mBORespMask.Coordinator = mDR["COORDINATOR"].ToString();
					mBORespMask.TelCoordinator = mDR["VWC_TEL"].ToString();
					mBORespMask.ExContractor = mDR["EXTCON"].ToString();
					mBORespMask.TelExContractor = mDR["SUPERTEL"].ToString();
					try
					{
                        mBORespMask.MaskService = Convert.ToDateTime(mDR["MASKNEXTMAINTAIN"]).ToString("dd.MM.yyyy");
					}
					catch (InvalidCastException)
					{
						// Swallow it
					}
							
					// Create array containig BO attributes and add to datatable
					row.ItemArray = new object[11] {mBORespMask.CwrID,
													   mBORespMask.MaskNo,
                                                       mBORespMask.MaskSystem,
													   mBORespMask.MaskReceived,
													   mBORespMask.MaskDelivered,
													   mBORespMask.FFMA,
													   mBORespMask.Coordinator,
													   mBORespMask.TelCoordinator,
													   mBORespMask.ExContractor,
													   mBORespMask.TelExContractor,
													   mBORespMask.MaskService};
				
					table.Rows.Add(row);
					j ++;
				}
				mDR.Close();


                mReportView.BtnGenerateReport.Enabled = (j > 0);
                mReportView.BtnGenerateExport.Enabled = (j > 0);
                mView.Cursor = Cursors.Default;

				if (j > 0) 
				{	
					// Bind data grid in Form to datatable		
                    mReportView.ucReportRespMask1.DgrReportRepMask.DataSource = table;										
					ShowMessageInStatusBar("Meldung: " + j + " Fremdfirmenmitarbeiter gefunden");
				} 
				else 
				{
					ShowMessageInStatusBar(MessageSingleton.GetInstance().GetMessage(MessageSingleton.NO_RESULTS));
				}
			}
			catch (OracleException oraex)
			{	
				mView.Cursor = Cursors.Default;
				throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR) + oraex.Message );
			}
			catch (DbAccessException odr)
			{
				mView.Cursor = Cursors.Default;
				throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR) + odr.Message );
			}
		}

		/// <summary>
		/// Generates SQL Where text for reports to do with resp masks.
		/// Note that date strings have to be correctly formatted to avoid ORA-01843 
		/// </summary>
		/// <param name="pSelect"></param>
		/// <returns></returns>
		private string SetWhereClauseRespMask(string pSelect) 
		{			
			mWhereClause = " WHERE CWR_MND_ID = " + UserManagementControl.getInstance().CurrentMandatorID;
			
			if ( mSurnameParameter.Length > 0 ) 
			{
				mWhereClause += " AND UPPER(CWR_SURNAME) LIKE '" +  mSurnameParameter.Trim().ToUpper() + "%' ";
			}
			if ( mFirstnameParameter.Length > 1 ) 
			{
				mWhereClause += " AND UPPER(CWR_FIRSTNAME) LIKE '" + mFirstnameParameter.ToUpper() + "%' "; 
			}
			if ( mAccessauthorizeYesParameter) 
			{
				mWhereClause += " AND RATH_RECEPTAUTHO_YN  = 'Y'"; 
			}
			if ( mAccessauthorizeNoParameter ) 
			{
				mWhereClause += " AND (RATH_RECEPTAUTHO_YN = 'N' OR RATH_RECEPTAUTHO_YN IS NULL)"; 
			}
			if ( mStatusGueltigParameter ) 
			{
				mWhereClause += " AND CWR_STATUS  = 'GÜLTIG'"; 
			}
			if ( mStatusUngueltigParameter ) 
			{
				mWhereClause += " AND CWR_STATUS  = 'UNGÜLTIG'"; 
			}			
			if ( mExContractorParameter.Length > 0 ) 
			{
				mWhereClause += " AND EXTCON = '" +  mExContractorParameter + "' ";
			}
			if ( mSupervisorParameter.Length > 1 ) 
			{
				// Get supervisor using ID of ext. contractor
				mWhereClause += " AND CWR_EXCO_ID = " + mSupervisorParameter; 
			}
			if ( mCoordinatorParameter.Length > 1 ) 
			{
				mWhereClause += " AND COORDINATOR = '" + mCoordinatorParameter + "' "; 
			}
			if ( mSubContractorParameter.Length > 1 ) 
			{
				mWhereClause += " AND SUBCON = '" + mSubContractorParameter + "' "; 
			}
			if ( mOrdernoParameter.Length > 0 ) 
			{
				mWhereClause += " AND CWR_ORDERNO = '" +  mOrdernoParameter + "' ";
			}
			if ( mCraftParameter.Length > 1 ) 
			{
				mWhereClause += " AND CRA_CRAFTNOTATION = '" + mCraftParameter + "' "; 
			}
			if ( mPlantParameter.Length > 1 ) 
			{
				mWhereClause += " AND PL_NAME = '" + mPlantParameter + "' "; 
			}
			if ( mDepartmentParameter.Length > 1 ) 
			{
				mWhereClause += " AND DEPT_DEPARTMENT = '" + mDepartmentParameter + "' "; 
			}

			if ( mMantenanceFromPara.Length > 1 &&  mMantenanceUntilPara.Length > 1) 
			{
                mWhereClause += " AND TRUNC(MASKNEXTMAINTAIN) BETWEEN TO_DATE('" 
					+ mMantenanceFromPara  
					+ "', 'DD.MM.YYYY' ) AND TO_DATE( '"  
					+ mMantenanceUntilPara 
					+ "', 'DD.MM.YYYY')";
			}

			if ( mMantenanceFromPara.Length > 1 &&  mMantenanceUntilPara.Length == 0 ) 
			{
                mWhereClause += " AND TRUNC(MASKNEXTMAINTAIN) >= TO_DATE('"  
					+ mMantenanceFromPara 
					+ "', 'DD.MM.YYYY' ) "; 
			}

			if ( mMantenanceFromPara.Length == 0 &&  mMantenanceUntilPara.Length > 1 ) 
			{
                mWhereClause += " AND TRUNC(MASKNEXTMAINTAIN) <= TO_DATE('" 
					+ mMantenanceUntilPara 
					+ "', 'DD.MM.YYYY' ) "; 
			} 

			if ( mMaskNoParameter.Length > 1 ) 
			{
				mWhereClause += " AND UPPER (REMA_MASKNO) = '" + mMaskNoParameter.Trim().ToUpper() + "' "; 
			}
			if (mMaskReceived.Equals(true) && mMaskDelivered.Equals(false))
			{
                mWhereClause += " AND MASKDATELENT IS NOT NULL AND MASKDATERETURN IS NULL";
			}
			if (mMaskDelivered.Equals(true)&& mMaskReceived.Equals(false))
			{
                mWhereClause += " AND MASKDATERETURN IS NOT NULL";
			}

			SaveSearchCriteria(mWhereClause);
			return pSelect + mWhereClause;
		}

		#endregion

		#region ReportAttendance

		/// <summary>
		/// Generates SQL WHERE text for reports based on coworker
		/// WHERE ... is always current mandant (value from FPASS Wesseling)
		/// AND .. all other search criteria
		/// Deals with selection of multiple excontractors by making list AND xxx IN ('A', 'B',..)
		/// </summary>
		/// <param name="pSelect">select statement as read from Configuration</param>
		/// <returns></returns>
		private string SetWhereClauseAttendance(string pSelect) 
		{
			mExContractorParameter = "";
            mWhereClause = " WHERE VATT_CWR_MND_ID = " + UserManagementControl.getInstance().CurrentMandatorID;

			if (mSurnameParameter.Length > 0) 
			{
				mWhereClause += " AND UPPER(VATT_SURNAME) LIKE '" +  mSurnameParameter.Trim().ToUpper() + "%' ";
			}
			if (mFirstnameParameter.Length > 1) 
			{
				mWhereClause += " AND UPPER(VATT_FIRSTNAME) LIKE '" + mFirstnameParameter.ToUpper() + "%' "; 
			}
			// Make IN list AND xxx IN ('A', 'B',..)
			// also fill mExContractorParameter search parameter with list of names
			if (mExContractorNames != null && mExContractorNames.Count > 0)
			{
                // Maximum 1000 elements allowed in list IN (..)
                // Breaks list into blocks of 1000
                // SQL AND and OR must look like this: AND (exco IN(..) OR exco IN(..) )
                mWhereClause += " AND (VATT_EXCONTRACTOR IN (";
			    
                for (int i = 0; i < mExContractorNames.Count; i++)
                {
                    /// Block of 1000 is finished, break up SQL statement
                    /// index number 999
                    /// index number 1998 
                    /// etc
                    if (0 != i && 0 == i % 999)
                    {
                        mWhereClause += "'" + mExContractorNames[i].ToString()
                                        + "')"
                                        + " OR VATT_EXCONTRACTOR IN (";
                    }
                    else mWhereClause += "'" + mExContractorNames[i].ToString() + "',";

                    mExContractorParameter += " " + mExContractorNames[i].ToString() + ",";
                }
			    
				// cut off last comma and close with ")"
				mExContractorParameter = mExContractorParameter.Remove(mExContractorParameter.Length - 1, 1);
				mWhereClause = mWhereClause.Remove(mWhereClause.Length - 1, 1);
				mWhereClause += ")) ";
			}

			StringBuilder datesBuilder = new StringBuilder();

			switch (mReportName)
			{
				// Make the dates for ATTENDANCE_DETAIL:
				// difficulty is that non-paired bookings have to be shown (E set, A null or vice versa)
				case ReportNames.CWR_ATTEND_DETAIL:

					datesBuilder.Append(" AND (");
					// paired bookings between dateFrom and dateUntil
					datesBuilder.Append("(VATT_ENTRYDATE >= TO_DATE('" + mDynamicDataFromParameter + "', 'DD.MM.YYYY HH24:MI:SS', 'nls_date_language=German') ");
					datesBuilder.Append("AND VATT_EXITDATE <= TO_DATE('" +  mDynamicDataUntilParameter + "', 'DD.MM.YYYY HH24:MI:SS', 'nls_date_language=German') +1 ) ");
					// or E's between dateFrom and dateUntil where A null
					datesBuilder.Append("OR ( VATT_ENTRYDATE >= TO_DATE('" + mDynamicDataFromParameter + "', 'DD.MM.YYYY HH24:MI:SS', 'nls_date_language=German')");
					datesBuilder.Append("AND VATT_ENTRYDATE <= TO_DATE('" + mDynamicDataUntilParameter + "', 'DD.MM.YYYY HH24:MI:SS', 'nls_date_language=German') +1 "); 
					datesBuilder.Append("AND VATT_EXITDATE is null)");
					// or A's between dateFrom and dateUntil where E null
					datesBuilder.Append("OR ( VATT_EXITDATE >= TO_DATE('" + mDynamicDataFromParameter + "', 'DD.MM.YYYY HH24:MI:SS', 'nls_date_language=German') ");
					datesBuilder.Append("AND VATT_EXITDATE <= TO_DATE('"+ mDynamicDataUntilParameter + "', 'DD.MM.YYYY HH24:MI:SS', 'nls_date_language=German') +1 ");
					datesBuilder.Append("AND VATT_ENTRYDATE is null)");
					datesBuilder.Append(")");

					mWhereClause += datesBuilder.ToString();
					break;

				case ReportNames.CWR_ATTENDANCE:
                    mWhereClause += " AND to_date(VATT_DAY, 'DD.MM.YYYY HH24:MI:SS', 'nls_date_language=German') >= to_date('" 
						+ mDynamicDataFromParameter 
						+ "', 'DD.MM.YYYY HH24:MI:SS', 'nls_date_language=German') "
                        + "AND to_date(VATT_DAY, 'DD.MM.YYYY HH24:MI:SS', 'nls_date_language=German') <= to_date('" 
						+ mDynamicDataUntilParameter 
						+ "', 'DD.MM.YYYY HH24:MI:SS', 'nls_date_language=German') ";
					break;
				default:
					throw new UIErrorException("Fehler: falscher Reporttyp gerufen. SetWhereClauseAttendance(string pSelect)" + this.ToString());
			}

			SaveSearchCriteria(mWhereClause);
			return pSelect + mWhereClause;
		}

		/// <summary>
		/// Selects coworker attendance records from DB and loads them into datagrid
		/// Hashtable is used to ensure each record appears only once in gui (view can return a cwr more than once)
		/// Unique key is Degussa PERSNO and not technical FPASS ID
		/// </summary>
		public void GetAttendance() 
		{
			int j = 0;
			mView.Cursor = Cursors.WaitCursor;
			((FrmReport) mView).ucReportAttendance1.DgrReportCoWorker.DataSource = null;

			mProvider = DBSingleton.GetInstance().DataProvider;
			IDbCommand selComm  = mProvider.CreateCommand(mQueryName);			
			
            // Build SQL WHERE using search criteria
            selComm.CommandText = 
				SetWhereClauseAttendance(selComm.CommandText);
            selComm.CommandText += mOrderBy;
         

			// Check if in all HashTables equal ID's exist
			Hashtable searchResults = new Hashtable();

			// Create datatable at runtime: this is bound to datagrid to allow sorting			
			DataRow row;
			DataTable table = new DataTable("RTTabCoWorker");
			table.Columns.Add(new DataColumn(CoWorkerSearch.ID_COL) );
			table.Columns.Add(new DataColumn(CoWorkerSearch.SURNAME));
            table.Columns.Add(new DataColumn(CoWorkerSearch.FIRSTNAME));
			table.Columns.Add(new DataColumn(CoWorkerSearch.DATE_OF_BIRTH, typeof(DateTime)));
			table.Columns.Add(new DataColumn(CoWorkerSearch.ACCESS));
            table.Columns.Add(new DataColumn(CoWorkerSearch.STATUS));
			table.Columns.Add(new DataColumn(CoWorkerSearch.VALID_UNTIL, typeof(DateTime)));			
			table.Columns.Add(new DataColumn("ExContractorName"));
			table.Columns.Add(new DataColumn("ExContractorDebitNo"));
			table.Columns.Add(new DataColumn("SubContractor"));
			table.Columns.Add(new DataColumn("CoordNameAndTel"));	
			table.Columns.Add(new DataColumn(CoWorkerSearch.ZKS_RET));
				
			// Open data reader to get CoWorker data
			IDataReader mDR = mProvider.GetReader(selComm);

			while (mDR.Read())
			{
				mCoWorkerBO = new CoWorkerSearch();

				mCoWorkerBO.CoWorkerId = Convert.ToDecimal(mDR["VATT_CWR_ID"]);
				mCoWorkerBO.PersNo = Convert.ToDecimal(mDR["VATT_PERSNUMBER"]);
				mCoWorkerBO.Surname = mDR["VATT_SURNAME"].ToString();
				mCoWorkerBO.Firstname = mDR["VATT_FIRSTNAME"].ToString();
				mCoWorkerBO.Access = mDR["VATT_ACCESS"].ToString();
				if (!mDR["VATT_DATEOFBIRTH"].Equals(DBNull.Value) )
				{
					mCoWorkerBO.DateOfBirth = Convert.ToDateTime(mDR["VATT_DATEOFBIRTH"]).ToString("dd.MM.yyyy");
				}
				mCoWorkerBO.Status = mDR["VATT_STATUS"].ToString();
				mCoWorkerBO.CoordNameAndTel = mDR["VATT_COORDINATORNAMETEL"].ToString();
				mCoWorkerBO.ExContractorName = mDR["VATT_EXCONTRACTOR"].ToString();
				mCoWorkerBO.ExContractorDebitNo = mDR["VATT_DEBITNO"].ToString();
				mCoWorkerBO.SubContractor = mDR["VATT_SUBCONTRACTOR"].ToString();
				mCoWorkerBO.ZKSReturncode = mDR["VATT_RETURNCODEZKS"].ToString();
							
				// Add record to hashtable if not already there. Key is PERSNO
				if (!searchResults.ContainsKey(mCoWorkerBO.PersNo) ) 
				{
					searchResults.Add(mCoWorkerBO.PersNo, mCoWorkerBO);
				}
			}					
			mDR.Close();

			foreach ( CoWorkerSearch bco in searchResults.Values )
			{
				row	= table.NewRow();
				row.ItemArray = new object[12] {bco.CoWorkerId,
												   bco.Surname,
												   bco.Firstname,
												   bco.DateOfBirth,
												   bco.Access,
												   bco.Status,
												   bco.ValidUntil,
												   bco.ExContractorName,
												   bco.ExContractorDebitNo,	
												   bco.SubContractor, 
												   bco.CoordNameAndTel, 												   
												   bco.ZKSReturncode};
				table.Rows.Add(row);
				j ++;
			}
	 
			if ( j > 0 ) 
			{	
				// Bind data grid in Form to datatable		
				((FrmReport)mView).BtnGenerateReport.Enabled = true;
				((FrmReport)mView).BtnGenerateExport.Enabled = true;

				((FrmReport) mView).ucReportAttendance1.DgrReportCoWorker.DataSource = table;					
				mView.Cursor = Cursors.Default;
				ShowMessageInStatusBar( "Meldung: " + 	j + " Fremdfirmenmitarbeiter gefunden" );
			} 
			else 
			{			
				// No results
				((FrmReport) mView).BtnGenerateReport.Enabled = false;
				((FrmReport) mView).BtnGenerateExport.Enabled = false;

				mView.Cursor = Cursors.Default;
				ShowMessageInStatusBar( MessageSingleton.GetInstance().GetMessage(MessageSingleton.NO_RESULTS) );
			}
		}

		#endregion

        #region SearchCriteria

        /// <summary>
		/// Saves current search criteria as a <see cref="SortedList"/> so they can be used 
		/// during PDF report generation.
		/// Saves Where clause for SQL statement
		/// </summary>
		/// <param name="prmWhereClause"></param>
		private void SaveSearchCriteria(string prmWhereClause)
		{
			// Where clause containing search criteria as text
			FpassReportSingleton.GetInstance().WhereSearchCriteria = prmWhereClause;

			string maskState = "";
			
			try
			{
				// fills the sorted list with the new search criteria
				SortedList searchCriteria = new SortedList();

				searchCriteria.Add("Surname",	mSurnameParameter);
				searchCriteria.Add("Firstname", mFirstnameParameter);

				if (mAccessauthorizeYesParameter)
				{
					searchCriteria.Add("Accessauthorize", "Ja"); //FFMA-Zutrittsberechtigt																						
				}
				else if (mAccessauthorizeNoParameter)
				{
					searchCriteria.Add("Accessauthorize", "Nein"); //FFMA-Zutrittsberechtigt																						
				}
				else
				{
					searchCriteria.Add("Accessauthorize", ""); //FFMA-Zutrittsberechtigt																						
				}

				if (mStatusGueltigParameter)
				{
					searchCriteria.Add("Status", "Gültig"); ///FFMA-Status																				
				}
				else if (mStatusUngueltigParameter)
				{
					searchCriteria.Add("Status", "Ungültig"); //FFMA-Status																					
				}
				else
				{
					searchCriteria.Add("Status", ""); //FFMA-Status																				
				}
		
				searchCriteria.Add("DynamicDataFrom",	mDynamicDataFromParameter); //FFMA-Bewegungsdaten Gültigkeit von
				searchCriteria.Add("DynamicDataUntil",	mDynamicDataUntilParameter);//FFMA-Bewegungsdaten Gültigkeit bis
				searchCriteria.Add("DelDateFrom",	mDelDateFromParameter);	//Passierschein-Ausgabedatum von
				searchCriteria.Add("DelDateUntil",	mDelDateUntilParameter);	//Passierschein-Ausgabedatum bis
				searchCriteria.Add("ValidFrom",		mValidFromParameter);		//Passierschein-Gültigkeit von
				searchCriteria.Add("ValidUntil",	mValidUntilParameter);		//Passierschein-Gültigkeit bis
				searchCriteria.Add("ExContractor",	mExContractorParameter);	//Fremdfirma-Fremdfirma
				searchCriteria.Add("ExContractorList",	mExContractorParameter);	//Fremdfirma-Fremdfirma
				searchCriteria.Add("Supervisor",	mSupervisorNameParameter); //Fremdfirma-Baustellenleiter
				searchCriteria.Add("Coordinator",	mCoordinatorParameter);	//Fremdfirma-Koordinator
				searchCriteria.Add("SubContractor", mSubContractorParameter);	//Fremdfirma-Subfirma
				searchCriteria.Add("Orderno",		mOrdernoParameter);		//Werk-Auftragsnr
				searchCriteria.Add("Craft",			mCraftParameter);			//Werk-Gewerk
				searchCriteria.Add("Plant",			mPlantParameter);			//Werk-Betrieb
				searchCriteria.Add("Department",	mDepartmentParameter);		//Werk-Abteilung
				searchCriteria.Add("MaintenanceFrom",	mMantenanceFromPara);	//Atemschutzmaske-Wartung von
				searchCriteria.Add("MaintenanceUntil",	mMantenanceUntilPara); //Atemschutzmaske-Wartung bis
				searchCriteria.Add("MaskNo",		mMaskNoParameter);			//Atemschutzmaske-Maskennummer
				searchCriteria.Add("NoBookXDays",	mNoBookXDaysParameter);	// Keine Buchung seit x Tagen

				if (mMaskReceived.Equals(true) && mMaskDelivered.Equals(false))
				{
					maskState = "verliehen";
				} 
				else if (mMaskDelivered.Equals(true)&& mMaskReceived.Equals(false))
				{
					maskState = "abgegeben";
				} 
				searchCriteria.Add("MaskState",	maskState);

				FpassReportSingleton.GetInstance().SearchCriteria = searchCriteria;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Message: " + ex.Message + " StackTrace: " + ex.StackTrace + " Source: " + ex.Source);
			}
		}
		
		#endregion

        #endregion
    }
}	


using System.Collections;
using de.pta.Component.Errorhandling;
using Degussa.FPASS.Util.Messages;

namespace Degussa.FPASS.Reports.Util
{
	/// <summary>
	/// Holds search parameters, Ids and SQL used to generate the PDF report.
	/// Note: NOT used for search in Reports GUI.
	/// </summary>
	/// <remarks>
	/// <para><b>History</b></para>
	/// <div class="tablediv">
	/// <table class="dtTABLE" cellspacing="0">
	///		<tr>
	///			<th width="20%">S. Bossu, PTA GmbH</th>
	///			<th width="20%">01/12/2003</th>
	///			<th width="60%">Creation</th>
	///		</tr>
	///		<tr>
	///			<th width="20%">N. Mundy, PTA GmbH</th>
	///			<th width="20%">11/04/2008</th>
	///			<th width="60%">Updated for version 4.5</th>
	///		</tr>
	/// </table>
	/// </div>
	/// </remarks>
	public class FpassReportParameters
	{
		#region Members
		
		private int mVersionNo;
		private string mFileName = "";

		 // id of the main select command
		private string mMainSqlCommandId = "";

		// where clause to add to the main select command
		private string mMainWhere = ""; 

		// order by clause to add to the main select command: search results in GUI
		private string mMainOrderBy = ""; 

		// Group by clause to add to the main select command
		private string mMainGroupBy = ""; 

		private string [] mSubSqlCommandIds = new string [1];
		private string [,] mSubWhereClauses = new string [1,2];
		private string subWhereClause = "";
        private string [] mSubOrderBys;

		private SortedList mSearchCriteria = new SortedList();
		private SortedList mStandardValues = new SortedList();

		private const string SEARCHCRITERIA_CWRID = "CoWorkerId";

		#endregion Members
		
		#region Constructors

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="prmReportName"></param>
		/// <param name="prmCoWorkerId"></param>
		public FpassReportParameters(string prmReportName, decimal prmCoWorkerId)
		{
			initialize(prmReportName, prmCoWorkerId);
		}

		#endregion Constructors	
		
		#region Initialization

		/// <summary>
		/// Initialises parameter list: values depend on which report selected
		/// </summary>
		/// <param name="prmReportName"></param>
		/// <param name="prmRowId"></param>
		private void initialize(string prmReportName, decimal prmRowId)
		{
			// gets the search criteria for the report
			SearchCriteria = FpassReportSingleton.GetInstance().SearchCriteria;
			
			// sets parameters for the selected Report				
			switch (prmReportName)
			{
				case ReportNames.CHECKLIST:
					mFileName = ReportFilenames.CHECKLIST;
					mVersionNo = 1;
					mMainSqlCommandId = "CwrCheckList";
					mMainOrderBy = "ORDER BY CHLS_PERSNO";
					break;

				case ReportNames.PLANTS:
					mFileName = ReportFilenames.PLANTS;
					mVersionNo = 1;
					mMainSqlCommandId = "PlantManager";
					mMainOrderBy = "ORDER BY US_NAME";
					break;

				case ReportNames.EXCO_COORDINATOR:
					mFileName = ReportFilenames.EXCO_COORDINATOR;
					mVersionNo = 1;
					mMainSqlCommandId = "ExContractorReports";
					mMainOrderBy = "ORDER BY EXCO_NAME, VWC_SURNAME";
					break;

                case ReportNames.CWR_CHANGEHIST:
                    mFileName = ReportFilenames.CWR_CHANGEHIST;
                    mVersionNo = 1;

                    mMainSqlCommandId = "ReportCoworkerRoot";
                    mMainWhere = BuildMainWhere("WHERE CWR_ID", prmRowId);
                    mMainOrderBy = "ORDER BY CWR_SURNAME, CWR_FIRSTNAME";

                    mSubSqlCommandIds = new string[1] { "ReportFFMAAenderungshistorieSub" };
                    subWhereClause += " WHERE VH3_CWR_ID = ";
                    mSubWhereClauses = new string[1, 2] { { subWhereClause, "CWR_ID" } };
                    mSubOrderBys = new string[1] { " ORDER BY VH3_CHANGEDATE desc" };
                    break;

				case ReportNames.CWR_ALL_DATA: 
					mFileName = ReportFilenames.CWR_ALL_DATA;
					mVersionNo = 1;
					mMainSqlCommandId = "CWRAllDataReports";

					// for this report a CoWorker must have been selected in the datagrid
                    mMainWhere = BuildMainWhere("WHERE CWR_ID", prmRowId);
                    mMainOrderBy = "ORDER BY CWR_SURNAME, CWR_FIRSTNAME";

					mSubSqlCommandIds = new string [5] {"ReportVehregno", 
														   "CWRPlantManagerReports", 
														   "CWRPrecmedAllDataReports", 
														   "CWRPlantReports", 
														   "CWRDynamicDataReports"}; 
													
					mSubWhereClauses = new string [5,2] { {"WHERE VRNO_CWR_ID = ", "CWR_ID"}, 
														{"WHERE CWPL_CWR_ID = ", "CWR_ID"}, 
														{"WHERE PMED_CWR_ID = ", "CWR_ID"}, 
														{"WHERE CWPL_CWR_ID = ", "CWR_ID"}, 
														{"WHERE DYFP_CWR_ID = ", "CWR_ID"} }; 
					break;

				case ReportNames.CWR_BOOKINGS:
					mFileName = ReportFilenames.CWR_BOOKINGS;
					mVersionNo = 1;				
                    mMainSqlCommandId = "ReportCoworkerRoot";
                    mMainWhere = BuildMainWhere("WHERE CWR_ID", prmRowId);
                    mMainOrderBy = "ORDER BY CWR_SURNAME, CWR_FIRSTNAME";

                    // Subselect returns individual bookings
                    // Note: must get dateFrom and Until from search criteria
                    string booDateFrom = FpassReportSingleton.GetInstance().SearchCriteria["DynamicDataFrom"].ToString();
                    string booDateUntil = FpassReportSingleton.GetInstance().SearchCriteria["DynamicDataUntil"].ToString();

                    mSubSqlCommandIds = new string[1] { "ReportFFMABookingsSub" };
                    subWhereClause += " WHERE TRUNC(DYFP_DATE) BETWEEN TO_DATE('"
                        + booDateFrom
                        + "', 'DD.MM.YYYY') AND TO_DATE( '"
                        + booDateUntil
                        + "', 'DD.MM.YYYY') AND CWR_ID = ";
                    mSubWhereClauses = new string[1, 2] { { subWhereClause, "CWR_ID" } };
                    mSubOrderBys = new string[1] { " ORDER BY DYFP_DATE, DYFP_TIME" };
					break;

                case ReportNames.CWR_BOOKINGS_EXCO: // "Fremdfirmen (Bewegungsdaten Fremdfirmenmitarbeiter)":
                    mFileName = ReportFilenames.CWR_BOOKINGS_EXCO;
                    mVersionNo = 1;
                    mMainSqlCommandId = "CWRDynamicDataExcoReportsLight";

                    // for this report one or more Coworkers can be selected in results grid (they do not have to be)
                    // Main Where (with cwr id) and where from search criteria are added together
                    mMainWhere = BuildMainWhere(" CWR_ID", prmRowId);

                    mMainOrderBy = "ORDER BY CWR_SURNAME";
                    mSubSqlCommandIds = new string[1] { "CWRDynamicDataReports" };

                    // The sub sqlcommandid that selects the data for each coworker must have a where clause
                    // corresponding to the selected period of time in the search criteria
                    subWhereClause = "WHERE (TRUNC(DYFP_DATE) BETWEEN TO_DATE('" + SearchCriteria["DynamicDataFrom"].ToString() + "', 'DD.MM.YYYY') ";
                    subWhereClause += "AND TO_DATE('" + SearchCriteria["DynamicDataUntil"].ToString() + "', 'DD.MM.YYYY') ";
                    subWhereClause += ") AND DYFP_CWR_ID = ";
                    mSubWhereClauses = new string[1, 2] { { subWhereClause, "CWR_ID" } };
                    break;
				
				case ReportNames.EXCO_BOOKINGS_SUM:
					mFileName = ReportFilenames.EXCO_BOOKINGS_SUM;
					mVersionNo = 1;
					mMainSqlCommandId = "CWRSumCoworkerAExcoReports";
					mMainOrderBy = "ORDER BY EXCO_NAME";
					mSubSqlCommandIds = new string [1] {"CWRDynamicDataSumDaysReports"};
					
					// The sub sqlcommandid that selects the coworkers and their sum of days must have a where clause
					// corresponding to the selected period of time in the search criteria
					subWhereClause = "WHERE ( TRUNC(DYFP_DATE) BETWEEN TO_DATE('" + SearchCriteria["DynamicDataFrom"].ToString() + "', 'DD.MM.YYYY') ";
					subWhereClause += " AND TO_DATE('" + SearchCriteria["DynamicDataUntil"].ToString() + "', 'DD.MM.YYYY', 'nls_date_language=German') ";
					
					// HAVING after GROUP BY not efficient as grouping all coworkers rather than by current EXCO!
					// have to put EXCO_ID in where-clause before the group by
					subWhereClause += ") AND CWR_EXCO_ID = ";
					mSubWhereClauses = new string [1,2] { {subWhereClause, "EXCO_ID"} };
					break;

				case ReportNames.CWR_ATTEND_DETAIL:
					mFileName = ReportFilenames.CWR_ATTEND_DETAIL;
					mVersionNo = 1;
					mMainSqlCommandId = "CWRAttendanceDetailReports";
					mMainOrderBy = "ORDER BY VATT_EXCONTRACTOR, VATT_SURNAME, VATT_FIRSTNAME, VATT_ENTRYDATE";
					break;

				case ReportNames.CWR_ATTENDANCE:
					mFileName = ReportFilenames.CWR_ATTENDANCE;
					mVersionNo = 1;
					mMainSqlCommandId = "CWRAttendanceSumsReports";
                    mMainOrderBy = "ORDER BY VATT_EXCONTRACTOR, "
                                    + "VATT_SURNAME, VATT_FIRSTNAME, "
                                    + "to_date(VATT_DAY, 'DD.MM.YYYY', 'nls_date_language=German')";
									
					break;

				case ReportNames.EXCO_ATTENDANCE:
					mFileName = ReportFilenames.EXCO_ATTENDANCE;
					mMainSqlCommandId = "EXCOAttendanceSumsReports";
					mMainOrderBy = "ORDER BY EXCO_NAME";
					mMainGroupBy = " GROUP BY EXCO_ID, EXCO_MND_ID, EXCO_NAME, EXCO_DEBITNO, EXCO_SUPERVISOR, EXCO_TELEPHONENO, EXCO_STREET, EXCO_POSTCODE, EXCO_CITY";
					break;

				case ReportNames.CWR_EXPIRYDATE:
					mFileName = ReportFilenames.CWR_EXPIRYDATE;
					mVersionNo = 1;
					mMainSqlCommandId = "CoWorkerReports";
					mMainOrderBy = "ORDER BY CWR_PERSNO";
					break;
				
				case ReportNames.CWR_NO_BOOKING:
					mFileName = ReportFilenames.CWR_NO_BOOKING;
					mVersionNo = 1;
					mMainSqlCommandId = "CoWorkerReportsNoBook";
					mMainOrderBy = "ORDER BY CWR_PERSNO";
					break;

				case ReportNames.CWR_DELETELIST:
					mFileName = ReportFilenames.CWR_DELETELIST;
					mVersionNo = 1;
					mMainSqlCommandId = "CWRDelCwrReports";
					mMainOrderBy = "ORDER BY CWR_PERSNO";
					break;

					/// not used since 29.04.2004: there is only one report "Passierschein" 
					/// corresponds to Variante 2
//				case "Passierschein Variante 1":
//					mFileName = "Passierschein";
//					mVersionNo = 1;
//					mMainSqlCommandId = "CWRFpassReports";
//					// for this report a CoWorker must have been selected in the datagrid
//					// no where clause corresponding to search criteria will be added to this where clause
//					mMainWhereClause = "WHERE CWR_ID = " + prmRowId.ToString();
//					mSubSqlCommandIds = new string [3] {"ReportVehregno", 
//														"ReportMaxPlant", 
//														"CWRPrecautionmedReports"};
//					mSubWhereClauses = new string [3,2] {{"WHERE VRNO_CWR_ID = ", "CWR_ID"}, 
//														{"WHERE CWPL_CWR_ID = ", "CWR_ID"}, 
//														{"WHERE PMED_CWR_ID = ", "CWR_ID"}} ;
//					break;	
//
//				case "Passierschein Variante 2":
//					mFileName = "Passierschein";
//					mVersionNo = 2;
//					mMainSqlCommandId = "CWRFpassReports";
//					// for this report a CoWorker must have been selected in the datagrid
//					// no where clause corresponding to search criteria will be added to this where clause
//					mMainWhereClause = "WHERE CWR_ID = " + prmRowId.ToString();
//					mSubSqlCommandIds = new string [3] {"ReportVehregno", 
//														"CWRPlantReports", 
//														"CWRPrecautionmedReports"};
//					mSubWhereClauses = new string [3,2] {{"WHERE VRNO_CWR_ID = ", "CWR_ID"}, 
//														{"WHERE CWPL_CWR_ID = ", "CWR_ID"}, 
//														{"WHERE PMED_CWR_ID = ", "CWR_ID"}} ;
//					break;
//	
				
				case ReportNames.CWR_PASS:
					mFileName = ReportFilenames.CWR_PASS;
					mVersionNo = 3;
					mMainSqlCommandId = "CWRFpassReports";

					// for this report a CoWorker must have been selected in the datagrid			
                    mMainWhere = BuildMainWhere("WHERE CWR_ID", prmRowId);
                    mMainOrderBy = "ORDER BY CWR_SURNAME, CWR_FIRSTNAME";

					// Sub Report Respiratory Mask
					mSubSqlCommandIds = new string [4] {"ReportVehregno", 
														"CWRPlantReports", 
														"CWRRespMaskSubReports", 
														"CWRPrecautionmedReports"};
					mSubWhereClauses = new string [4,2] {{"WHERE VRNO_CWR_ID = ", "CWR_ID"}, 
														{"WHERE CWPL_CWR_ID = ", "CWR_ID"}, 
														{"WHERE MBC_CWR_ID = ", "CWR_ID"},
														{"WHERE PMED_CWR_ID = ", "CWR_ID"}} ;
					break;

				
				// "Maskenrueckgabebeleg": PDF to be handed out as ticket to prove resp mask was given back (called from FFMA Erfassung, not from Reports dialog) 
				case ReportNames.RESPMASK_RETURNED_TICKET:
					mFileName = ReportFilenames.RESPMASK_RETURNED_TICKET;
					mVersionNo = 1;
					mMainSqlCommandId = "CWRFpassReports";
					
					// for this report a single CoWorker is required
					mMainWhere = "WHERE CWR_ID = " + prmRowId.ToString();
                    mMainOrderBy = "";
					
					mSubSqlCommandIds = new string [3] {"ReportVehregno", 
														   "CWRRespMaskSubReports", 
														   "CWRPrecautionmedReports"};
					mSubWhereClauses = new string [3,2] {{"WHERE VRNO_CWR_ID = ", "CWR_ID"}, 
														{"WHERE MBC_CWR_ID = ", "CWR_ID"},
														{"WHERE PMED_CWR_ID = ", "CWR_ID"}} ;
					break;	
			
				case ReportNames.RESPMASKS:
					mFileName = ReportFilenames.RESPMASKS;
					mVersionNo = 1;
					mMainSqlCommandId = "CWRRespmaskReports";
                    mMainOrderBy = "ORDER BY MASKDATELENT DESC, MASKDATERETURN DESC, CWR_SURNAME";
					break;

				default:
					throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.REPORT_NO_REPORT));
			}
		} 
		
		#endregion Initialization
		
		#region Accessors

		public int VersionNo
		{
			get
			{
				return mVersionNo;
			}
		}

		public string MainSqlCommandId
		{
			get
			{
				return mMainSqlCommandId;
			}
		}

		/// <summary>
		/// Additional Where clause containing selected CWR id
		/// </summary>
		public string WhereMain
		{
			get
			{
				return mMainWhere;
			}
			set
			{
				mMainWhere = value;
			}
		}

		/// <summary>
		/// Returns Order By to be used in report
		/// </summary>
		public string OrderByMain
		{
			get
			{
				return mMainOrderBy;
			}
		}

		/// <summary>
		/// Returns Group By to be used in report. 
		/// </summary>
		public string GroupByMain
		{
			get { return mMainGroupBy; }
		}

		public string[] SubSqlCommandIds
		{
			get
			{
				return mSubSqlCommandIds;
			}
		}

        /// <summary>
        /// Returns Where clauses used in subselects
        /// </summary>
		public string[,] SubWhereClauses
		{
			get
			{
				return mSubWhereClauses;
			}
		}

        /// <summary>
        /// Returns Order By clauses used in subselects
        /// </summary>
        public string[] SubOrderBys
        {
            get
            {
                return mSubOrderBys;
            }
        }

		public SortedList SearchCriteria
		{
			get
			{
				return mSearchCriteria;
			}
			set
			{
				mSearchCriteria = value;
			}
		}

		public SortedList StandardValues
		{
			get
			{
				return mStandardValues;
			}
		}

		public string FileName
		{
			get
			{
				return mFileName;
			}
		}

		#endregion Accessors

		#region Methods

       /// <summary>
       /// Builds Where string like this: "CWR IN (101,102,.. )". This currently contains list of coworker IDs
       /// </summary>
       /// <param name="pWhereCwrID">name of CWR column</param>
       /// <param name="prmRowId">first cwrid value wot was found (signals that user has selected a value)</param>
        /// <returns>Where string like this: "CWR IN (101,102,.. )"</returns>
        private string BuildMainWhere(string pWhereCwrID, decimal prmRowId)
		{
            string whereClause;

            if (prmRowId < 1)
            {
                return "";
            }
            else
            {             
                // Search criteria have not been filled, module not called from Reports GUI
                if (SearchCriteria.Keys.Count == 0)
                {
                    whereClause = pWhereCwrID + " = " + prmRowId;
                }
                else
                {
                    whereClause = " IN (";

                    foreach (string searchCriteriumName in SearchCriteria.Keys)
                    {
                        if (searchCriteriumName.StartsWith(SEARCHCRITERIA_CWRID))
                        {
                            whereClause += SearchCriteria[searchCriteriumName].ToString();
                            whereClause += ",";
                        }
                    }

                    // cut off last comma and close with ")"
                    whereClause = whereClause.Remove(whereClause.Length - 1, 1);
                    whereClause += ") ";

                    // this is a strange case: a Coworker has been selected and unselected while STRG pushed
                    // result: prmRowId > 0 but there is no ID saved, so that where clause = " ()"!
                    if (6 > whereClause.Length)
                    {
                        whereClause = pWhereCwrID = " = " + prmRowId;
                    }
                    whereClause = pWhereCwrID + whereClause;
                }
                return whereClause;
            }
		}

		#endregion Methods

	}
}

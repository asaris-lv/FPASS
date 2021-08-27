using de.pta.Component.Errorhandling;
using Degussa.FPASS.Util.Messages;

namespace Degussa.FPASS.Reports.Util
{
	/// <summary>
	/// Contains all needed parameters for the export depending on the selected report
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
	public class FpassExportParameters
	{
		#region Members

		// id of the main select command
		private string mMainSqlCommandId = ""; 
		
		// where clause to add to the main select command
		private string mMainWhereClause = ""; 

		// order by clause to add to the main select command
		private string mMainOrderBy = "";
		
		// Group by clause to add to the main select command
		private string mMainGroupBy = ""; 

		private string mSubSqlCommandId = ""; // id of the secondary select command (sub block)
		private string mSubWhereClauseSubField = ""; // where clause to add to the secondary select command, contains a foreign key
		private string mSubWhereClauseMainField = ""; // foreign key field in the main select command
		private string subWhereClause = "";

		private string mFileName = "";
		private SortedDictionary mExportFields = new SortedDictionary(); // List of all columns to export

		#endregion Members
		
		#region Constructors

		public FpassExportParameters(string prmReportName, decimal prmCoWorkerId)
		{
			Initialize(prmReportName, prmCoWorkerId);
		}

		#endregion Constructors	

		#region Initialization

		/// <summary>
		/// Initialises specific export information for the selected report.
        /// Export is not available for all reports, this is why some don't appear here
		/// </summary>
		/// <param name="prmReportName">name of selected report</param>
		/// <param name="prmCoWorkerId">current CWR id</param>
		private void Initialize(string prmReportName, decimal prmCoWorkerId)
		{
			switch (prmReportName)
			{
				case ReportNames.CHECKLIST:
					mFileName = ReportFilenames.CHECKLIST;
					mMainSqlCommandId = "CwrCheckList";
					mMainOrderBy = "ORDER BY CHLS_PERSNO";
					mExportFields.Add("CHLS_PERSNO",		"PersonalNr FFMA");
					mExportFields.Add("CHLS_IDCARDNOFPASS",	"AusweisNr FPASS");
					mExportFields.Add("CHLS_IDCARDNOZKS",	"AusweisNr ZKS");
					mExportFields.Add("CHLS_SURNAME",		"Nachname FFMA");
					mExportFields.Add("CHLS_FIRSTNAME",		"Vorname FFMA");
					mExportFields.Add("CHLS_VALIDEFROM",	"Zutrittsberechtigt von");
					mExportFields.Add("CHLS_VALIDEUNTIL",	"Zutrittsberechtigt bis");
					mExportFields.Add("CHLS_AUTHORISED_YN",	"Zutrittsberechtigt j/n");
					mExportFields.Add("ONLY_FPASS",			"FFMA nur in FPASS j/n");
					mExportFields.Add("ONLY_ZKS",			"FFMA nur in ZKS j/n");
					mExportFields.Add("DIFFERENT_NO",		"AusweisNr unterschiedlich");
					mExportFields.Add("CHLS_STATUS",		""); // this field has no title because it won't be exported but only used for a calculation
					break;

				case ReportNames.PLANTS:
					mFileName = ReportFilenames.PLANTS;
					mMainSqlCommandId = "PlantManager";
					mMainOrderBy = "ORDER BY PL_NAME, US_NAME";
					mExportFields.Add("PL_NAME",		"Name Betrieb");
					mExportFields.Add("US_NAME",		"Nachname Meister");
					mExportFields.Add("US_FIRSTNAME",	"Vorname Meister");
					mExportFields.Add("USER_TEL",		"TelefonNr Meister");
					break;

				case ReportNames.EXCO_COORDINATOR: 
					mFileName = ReportFilenames.EXCO_COORDINATOR;
					mMainSqlCommandId = "ExContractorReports";
					mMainOrderBy = "ORDER BY EXCO_NAME, VWC_SURNAME";
					mExportFields.Add("EXCO_NAME",			"Name Fremdfirma");
                    mExportFields.Add("EXCO_DEBITNO",       "Debit-Nr.");
					mExportFields.Add("EXCO_STREET",		"Strasse");
					mExportFields.Add("EXCO_CITY",			"Ort");
					mExportFields.Add("EXCO_POSTCODE",		"PLZ");
					mExportFields.Add("EXCO_SUPERSURNAME",	"Baustellenleiter Nachname");
					mExportFields.Add("EXCO_SUPERFIRSTNAME","Baustellenleiter Vorname");
					mExportFields.Add("EXCO_TELEPHONENO",	"TelNr Baustellenleiter");
					mExportFields.Add("EXCO_FAX",			"FaxNr Baustellenleiter");
					mExportFields.Add("EXCO_MOBILEPHONE",	"MobileNr Baustellenleiter");
					mExportFields.Add("VWC_SURNAME",		"Koordinator Nachname");
					mExportFields.Add("VWC_FIRSTNAME",		"Koordinator Vorname");
					mExportFields.Add("VWC_TEL",			"TelNr Koordinator");
					mExportFields.Add("SUBCONTRACTOR",		"Subfirma");
					break;

				case ReportNames.CWR_BOOKINGS: // "Fremdfirmenmitarbeiter mit Bewegungen":
					mFileName = ReportFilenames.CWR_BOOKINGS;
					mMainSqlCommandId = "CWRDynDateReports";
					mMainWhereClause = " CWR_ID = " + prmCoWorkerId.ToString(); // a coworker has been selected
					mMainOrderBy = "ORDER BY CWR_PERSNO, DYFP_DATE, DYFP_TIME";
					mExportFields.Add("CWR_PERSNO",		"PersonalNr FFMA");
					mExportFields.Add("CWR_IDCARDNO",	"AusweisNr FFMA");
					mExportFields.Add("CWR_SURNAME",	"Nachname FFMA");
					mExportFields.Add("CWR_FIRSTNAME",	"Vorname FFMA");
					mExportFields.Add("EXTCON",			"Name Fremdfirma");
					mExportFields.Add("DYFP_DATE",		"Datum");
					mExportFields.Add("DYFP_TIME",		"Zeit");
					mExportFields.Add("DYFP_ENTRY",		"Eingang / Ausgang");
					break;

				case ReportNames.EXCO_BOOKINGS_SUM: // "Fremdfirmenmitarbeiter mit Bewegungen (Summe)":
					mFileName = ReportFilenames.EXCO_BOOKINGS_SUM;
					mMainSqlCommandId = "CWRSumCoworkerAExcoReports";
					mMainOrderBy = "ORDER BY EXCO_NAME";
					mSubSqlCommandId = "CWRDynamicDataSumDaysReports";
				
					// The sub sqlcommandid that selects the coworkers and their sum of days must have a where clause
					// corresponding to the selected period of time in the search criteria
					subWhereClause = "WHERE ( TRUNC(DYFP_DATE) BETWEEN TO_DATE('" + FpassReportSingleton.GetInstance().SearchCriteria["DynamicDataFrom"].ToString() + "', 'DD.MM.YYYY') ";
					subWhereClause += " AND TO_DATE('" + FpassReportSingleton.GetInstance().SearchCriteria["DynamicDataUntil"].ToString() + "', 'DD.MM.YYYY') ";
					subWhereClause += ") GROUP BY CWR_ID, CWR_SURNAME, CWR_FIRSTNAME, CWR_PERSNO, CWR_IDCARDNO, CWR_EXCO_ID, EXTCON HAVING CWR_EXCO_ID = ";
					mSubWhereClauseSubField = subWhereClause;
					mSubWhereClauseMainField = "EXCO_ID";
					mExportFields.Add("EXCO_NAME",		"Name Fremdfirma");
					mExportFields.Add("CWR_PERSNO",		"PersonalNr FFMA");
					mExportFields.Add("CWR_SURNAME",	"Nachname FFMA");
					mExportFields.Add("CWR_FIRSTNAME",	"Vorname FFMA");
					mExportFields.Add("SUMDAYSONECWR",	"Anzahl Tage FFMA");
					mExportFields.Add("SUMDAYS",		"Anzahl Tage FFMA der Fremdfirma");
					break;		

				case ReportNames.CWR_ATTEND_DETAIL:
					mFileName = ReportFilenames.CWR_ATTEND_DETAIL;
					mMainSqlCommandId = "CWRAttendanceDetailReports";
					mMainOrderBy = "ORDER BY VATT_EXCONTRACTOR, VATT_ENTRYDATE, VATT_SURNAME, VATT_FIRSTNAME";
					
					mExportFields.Add("VATT_EXCONTRACTOR",	"Name Fremdfirma");
					mExportFields.Add("VATT_PERSNUMBER",	"PersonalNr FFMA");
					mExportFields.Add("VATT_SURNAME",		"Nachname FFMA");
					mExportFields.Add("VATT_FIRSTNAME",		"Vorname FFMA");
					mExportFields.Add("VATT_COORDINATORNAMETEL", "Koordinator");
					mExportFields.Add("VATT_ENTRYDATE",		"Eingang");
					mExportFields.Add("VATT_EXITDATE",		"Ausgang");
					mExportFields.Add("VATT_ATTENDANCETIME","Anwesenheitszeit");
					mExportFields.Add("VATT_ATTENDANCEHOURS", "Stunden");
					mExportFields.Add("VATT_OVERLIMIT",		"Ueber Limit j/n");
					break;

				case ReportNames.CWR_ATTENDANCE:
					mFileName = ReportFilenames.CWR_ATTENDANCE;
					mMainSqlCommandId = "CWRAttendanceSumsReports";
					mMainOrderBy = "ORDER BY VATT_EXCONTRACTOR, "
					               + "to_date(VATT_DAY, 'DD.MM.YYYY', 'nls_date_language=German'), "
					               + "VATT_SURNAME, VATT_FIRSTNAME";
					
					mExportFields.Add("VATT_EXCONTRACTOR",	"Name Fremdfirma");
					mExportFields.Add("VATT_PERSNUMBER",	"PersonalNr FFMA");
					mExportFields.Add("VATT_SURNAME",		"Nachname FFMA");
					mExportFields.Add("VATT_FIRSTNAME",		"Vorname FFMA");
					mExportFields.Add("VATT_COORDINATORNAMETEL", "Koordinator");
					mExportFields.Add("VATT_DEBITNO",		"FF-Debitnr.");
					mExportFields.Add("VATT_DAY",			"Tag");
					mExportFields.Add("VATT_ATTENDANCESUM", "Tagessumme (Stunden)");
					break;

				case ReportNames.EXCO_ATTENDANCE:
					mFileName = ReportFilenames.EXCO_ATTENDANCE;
					mMainSqlCommandId = "EXCOAttendanceSumsReports";
					mMainOrderBy = "ORDER BY EXCO_NAME";
					mMainGroupBy = " GROUP BY EXCO_ID, EXCO_MND_ID, EXCO_NAME, EXCO_DEBITNO, EXCO_SUPERVISOR, EXCO_TELEPHONENO, EXCO_STREET, EXCO_POSTCODE, EXCO_CITY";

                    mExportFields.Add("EXCO_NAME", "Fremdfirma");
                    mExportFields.Add("EXCO_DEBITNO", "Debit-Nr.");
                    mExportFields.Add("EXCO_SUPERVISOR", "Baustellenleiter");
                    mExportFields.Add("EXCO_TELEPHONENO", "Telefonnummer");
                    mExportFields.Add("EXCO_POSTCODE", "PLZ");
                    mExportFields.Add("EXCO_CITY", "Stadt");
                    mExportFields.Add("EXCO_COUNTPERS", "Anzahl Personen");
                    mExportFields.Add("EXCO_SUMMEDHOURS", "Gesamtstunden");
                    break;

                case ReportNames.CWR_ATTENDANCE_PERSNO:
                    
			        // Note: this export is generated automatically if system finds double PersNr
			        mFileName = ReportFilenames.CWR_ATTENDANCE_PERSNO;
                    mMainSqlCommandId = "CWRAttendancePersNoReports";
                    mMainOrderBy = "ORDER BY VATT_EXCONTRACTOR, VATT_PERSNUMBER, VATT_BOOKINGDATE";
                    mMainGroupBy = "";			        
                    mExportFields.Add("VATT_EXCONTRACTOR", "Fremdfirma");
                    mExportFields.Add("VATT_PERSNUMBER", "PersonalNr FFMA");
                    mExportFields.Add("VATT_SURNAME", "Nachname FFMA");
                    mExportFields.Add("VATT_FIRSTNAME", "Vorname FFMA");
                    mExportFields.Add("VATT_BOOKINGDATE", "Buchungsdatum");
                    mExportFields.Add("VATT_BOOKING", "Buchung");
                    break;			       
			        
				case ReportNames.CWR_EXPIRYDATE:
					mFileName = ReportFilenames.CWR_EXPIRYDATE;
					mMainSqlCommandId = "CoWorkerReports";
					mMainOrderBy = "ORDER BY CWR_PERSNO";

					mExportFields.Add("CWR_PERSNO",			"PersonalNr FFMA");
					mExportFields.Add("CWR_SURNAME",		"Nachname FFMA");
					mExportFields.Add("CWR_FIRSTNAME",		"Vorname FFMA");
					mExportFields.Add("EXTCON",				"Name Fremdfirma");
					mExportFields.Add("CWR_VALIDFROM",		"Zutrittsberechtigt von");
					mExportFields.Add("CWR_VALIDUNTIL",		"Zutrittsberechtigt bis");
					mExportFields.Add("RATH_RECEPTAUTHO_YN","Zutrittsberechtigt j/n");
					mExportFields.Add("COORDSURNAME",		"Koordinator Nachname");
					mExportFields.Add("COORDFIRSTNAME",		"Koordinator Vorname");
					mExportFields.Add("VWC_TEL",			"TelNr Koordinator");
					break;

				case ReportNames.CWR_NO_BOOKING:
					mFileName = ReportFilenames.CWR_NO_BOOKING;
					mMainSqlCommandId = "CoWorkerReportsNoBook";

					mMainOrderBy = "ORDER BY CWR_PERSNO";
					mExportFields.Add("CWR_PERSNO",			"PersonalNr FFMA");
					mExportFields.Add("CWR_SURNAME",		"Nachname FFMA");
					mExportFields.Add("CWR_FIRSTNAME",		"Vorname FFMA");
					mExportFields.Add("EXTCON",				"Name Fremdfirma");
					mExportFields.Add("CWR_VALIDFROM",		"Zutrittsberechtigt von");
					mExportFields.Add("CWR_VALIDUNTIL",		"Zutrittsberechtigt bis");
					mExportFields.Add("LASTBOOK",			"Datum letzter Buchung");
					mExportFields.Add("COORDSURNAME",		"Koordinator Nachname");
					mExportFields.Add("COORDFIRSTNAME",		"Koordinator Vorname");
					mExportFields.Add("VWC_TEL",			"TelNr Koordinator");
					break;
				
				case ReportNames.CWR_DELETELIST:
					mFileName = ReportFilenames.CWR_DELETELIST;
					mMainSqlCommandId = "CWRDelCwrReports";
					mMainOrderBy = "ORDER BY CWR_PERSNO";
					mExportFields.Add("CWR_PERSNO",			"PersonalNr FFMA");
					mExportFields.Add("CWR_SURNAME",		"Nachname FFMA");
					mExportFields.Add("CWR_FIRSTNAME",		"Vorname FFMA");
					mExportFields.Add("EXTCON",				"Name Fremdfirma");
					mExportFields.Add("CWR_VALIDFROM",		"Zutrittsberechtigt von");
					mExportFields.Add("CWR_VALIDUNTIL",		"Zutrittsberechtigt bis");
					mExportFields.Add("RATH_RECEPTAUTHO_YN","Zutrittsberechtigt j/n");
					mExportFields.Add("COORDSURNAME",		"Koordinator Nachname");
					mExportFields.Add("COORDFIRSTNAME",		"Koordinator Vorname");
					mExportFields.Add("VWC_TEL",			"TelNr Koordinator");
					break;

//				case "Passierschein Variante 1":
					// no export

//				case "Passierschein Variante 2":
					// no export

				case ReportNames.RESPMASKS: 
					mFileName = ReportFilenames.RESPMASKS;
					mMainSqlCommandId = "CWRRespmaskReports";
                    mMainOrderBy = "ORDER BY REMA_MASKNO, MASKDATELENT DESC, MASKDATERETURN DESC";
					mExportFields.Add("REMA_MASKNO",		"MaskenNr");
					mExportFields.Add("MASKDATELENT",		"Verleihdatum Maske");
					mExportFields.Add("MASKDATERETURN",		"Rückgabedatum Maske");
					mExportFields.Add("CWR_SURNAME",		"Nachname FFMA");
					mExportFields.Add("CWR_FIRSTNAME",		"Vorname FFMA");
					mExportFields.Add("COORDSURNAME",		"Koordinator Nachname");
					mExportFields.Add("COORDFIRSTNAME",		"Koordinator Vorname");
					mExportFields.Add("VWC_TEL",			"TelNr Koordinator");
					mExportFields.Add("EXTCON",				"Name Fremdfirma");
					mExportFields.Add("SUPERVISOR",			"Baustellenleiter Name");
					mExportFields.Add("SUPERTEL",			"TelNr Baustellenleiter");
					mExportFields.Add("MASKNEXTMAINTAIN",	"Nächstes Wartungsdatum");
					break;

				case ReportNames.CWR_BOOKINGS_EXCO: // "Fremdfirmen (Bewegungsdaten Fremdfirmenmitarbeiter)":
					mFileName = ReportFilenames.CWR_BOOKINGS_EXCO;
					mMainSqlCommandId = "CWRDynamicDataExcoReports";
					// for this report a Coworker can have been selected in the datagrid
					// in this case a where clause corresponding to search criteria will be added at the beginning of this where clause
					if (0 < prmCoWorkerId)
						mMainWhereClause = " CWR_ID = " + prmCoWorkerId.ToString();
					mMainOrderBy = " ORDER BY CWR_SURNAME";
					mSubSqlCommandId = "CWRDynamicDataReports";

					// The sub sqlcommandid that selects the data for each coworker must have a where clause
					// corresponding to the selected period of time in the search criteria
					// 25.03.04: Must suppy datestring in SQL in to_date() form so that DB interprets it correctly
					subWhereClause = "WHERE ( TRUNC(DYFP_DATE) BETWEEN TO_DATE('" + FpassReportSingleton.GetInstance().SearchCriteria["DynamicDataFrom"].ToString() + "', 'DD.MM.YYYY') ";
					subWhereClause += " AND TO_DATE('" + FpassReportSingleton.GetInstance().SearchCriteria["DynamicDataUntil"].ToString() + "', 'DD.MM.YYYY') ";
					subWhereClause += ") AND DYFP_CWR_ID = ";
					mSubWhereClauseSubField = subWhereClause;
					mSubWhereClauseMainField = "CWR_ID";
					mExportFields.Add("CWR_PERSNO",		"PersonalNr FFMA");
					mExportFields.Add("CWR_IDCARDNO",	"AusweisNr FFMA");
					mExportFields.Add("CWR_SURNAME",	"Nachname FFMA");
					mExportFields.Add("CWR_FIRSTNAME",	"Vorname FFMA");
					mExportFields.Add("EXTCON",			"Name Fremdfirma");
					mExportFields.Add("DYFP_DATE",		"Datum");
					mExportFields.Add("DYFP_TIME",		"Zeit");
					mExportFields.Add("DYFP_ENTRY",		"Eingang / Ausgang");
					break;
	
//				case "Änderungshistorie FFMA":
					// no export
				
				default: // no report or report without export possibility
					throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.EXPORT_NO_EXPORT));
			}
		}
		
		#endregion Initialization

		#region Accessors

		public string MainSqlCommandId
		{
			get
			{
				return mMainSqlCommandId;
			}
		}

		public string MainWhereClause
		{
			get
			{
				return mMainWhereClause;
			}
			set
			{
				mMainWhereClause = value;
			}
		}

		public string MainOrderBy
		{
			get
			{
				return mMainOrderBy;
			}
		}

		public string SubSqlCommandId
		{
			get
			{
				return mSubSqlCommandId;
			}
		}

		public string SubWhereClauseSubField
		{
			get
			{
				return mSubWhereClauseSubField;
			}
		}

		public string SubWhereClauseMainField
		{
			get
			{
				return mSubWhereClauseMainField;
			}
		}

		public string FileName
		{
			get
			{
				return mFileName;
			}
			set
			{
				mFileName = value;
			}
		}

		public SortedDictionary ExportFields
		{
			get
			{
				return mExportFields;
			}
		}

		public string MainGroupBy
		{
			get { return mMainGroupBy; }
		}

		#endregion Accessors

	}
} 

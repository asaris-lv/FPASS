using System;

namespace Degussa.FPASS.FPASSApplication
{
	
	/// <summary>
	/// Holds the unique id's, for all dialogs, the dialog status and the dialog modes in FPASS.
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
	public class AllFPASSDialogs
	{
		#region Members

		/// <summary>
		/// unique id for dialog summary coworker
		/// </summary>
		public		const	int				SUMMARY_COWORKER_DIALOG = 1001;
		public		const	int				SEARCH_COWORKER_DIALOG = 1002;
		public		const	int				COWORKER_PROCESS_DIALOG = 1010;
		public		const	int				COWORKER_DELETE_DIALOG = 1011;
		public		const	int				ADMINISTRATION_DIALOG = 1030;
		public		const	int				REPORTS_DIALOG = 1040;
		public		const	int				MANDATOR_DIALOG = 1050;
		public		const	int				HISTORY_DIALOG = 1060;
		public		const	int				SEARCH_EXTERNAL_CONTRACTOR_DIALOG = 1070;
		public		const	int				USER_DIALOG = 1080;
		public		const	int				USER_TO_ROLE_DIALOG = 1090;
		public		const	int				ROLE_DIALOG = 1100;
		public		const	int				ARCHIVE_DIALOG = 1110;
		public		const	int				DYNAMIC_DATA_DIALOG = 1120;
		public		const	int				COWORKER_ARCHIVE_DIALOG = 1130;
		public		const	int				VEHICLE_DIALOG = 1140;
		public		const	int				POPUP_COWORKER_COORD_HIST = 1301;
		public		const	int				POPUP_EXCO_COORD_HIST = 1302;

		public		const	int				DIALOG_STATUS_NEW = 1;
		public		const	int				DIALOG_STATUS_UPDATE = 2;

		public		const	int				CWR_DIALOG_MODE_ARCHIVE = 1;

        public const int SEARCH_IDCARD_READER_DIALOG = 1401;
        public const int POPUP_COWORKER_IDCARD_DIALOG = 1402;

		// Names of the help topics associated with each form
        public const string FPASS_MASTER_HELPFILE = "\\OnlineHilfe\\FPASSHilfe5.chm";
		public const string HELPTOPIC_SUMMARY_COWORKER = "OnlineHilfe\\Online_hilfe_FFMA_Suchen.htm";
		public const string HELPTOPIC_COWORKER_SEARCH = "OnlineHilfe\\Online_hilfe_FFMA_Erweiterte_Suche.htm";
		public const string HELPTOPIC_COWORKER_PROCESS = "OnlineHilfe\\Online_hilfe_FFMA_erfassen.htm";
		public const string HELPTOPIC_COWORKER_DELETE = "OnlineHilfe\\Online_hilfe_FFMA_löschen.htm";
		public const string HELPTOPIC_COWORKER_VEHICLE = "OnlineHilfe\\Online_hilfe_FFMA_Kfz_Zutritt.htm";
		public const string HELPTOPIC_ADMIN	= "FPASS_Verwaltung.htm";	    
		public const string HELPTOPIC_REPORTS = "OnlineHilfe\\Online_hilfe_Reporting.htm";
        public const string HELPTOPIC_REPORTS_ATTENDANCE = "OnlineHilfe\\Online_hilfe_Reporting_Anw.htm#PersNo";
		public const string HELPTOPIC_MANDANT = "OnlineHilfe\\Online_hilfe_Mandant_auswaehlen.htm";
		public const string HELPTOPIC_HISTORY = "OnlineHilfe\\Online_hilfe_FPASS_Aenderungshistorie_anzeigen.htm";
		public const string HELPTOPIC_EXCONTR_SEARCH = "OnlineHilfe\\Online_hilfe_Fremdfirma_suchen.htm";
		public const string HELPTOPIC_USER = "OnlineHilfe\\Online_hilfe_Benutzer_pflegen.htm";
		public const string HELPTOPIC_USER_TO_ROLE = "OnlineHilfe\\Online_hilfe_Benutzer_Rolle_zuordnen.htm";
		public const string HELPTOPIC_ROLE = "OnlineHilfe\\Online_hilfe_Uebersicht_Rolle_User.htm";
		public const string HELPTOPIC_ARCHIVE = "OnlineHilfe\\Online_hilfe_FFMA_Archiv_bearbeiten.htm";
		public const string HELPTOPIC_DYNAMIC_DATA = "OnlineHilfe\\Online_hilfe_FPASS_Zugangsdaten_FFMA_anzeigen.htm";
		public const string HELPTOPIC_ARCHIVE_COWORKER = "OnlineHilfe\\Online_hilfe_FFMA_Archiv_bearbeiten.htm";
        public const string HELPTOPIC_BOOKINGSLOAD = "OnlineHilfe\\Online_hilfe_FFMA_Archiv_bearbeiten.htm";
        public const string HELPTOPIC_SMARTACT_CWR = "OnlineHilfe\\Online_hilfe_FFMA_NeuerAusweis.htm";       

		#endregion Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public AllFPASSDialogs()
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

		#endregion //End of Accessors

		#region Methods 

		#endregion // End of Methods


	}
}

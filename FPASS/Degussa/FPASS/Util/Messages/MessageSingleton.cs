using System;
using System.Text;
using System.Collections;

using Degussa.FPASS.Util.UserManagement;

namespace Degussa.FPASS.Util.Messages
{
	/// <summary>
	/// Holds messages used in fpass and provides access to these messages.
	/// </summary>
	/// <remarks>
	/// <para><b>History</b></para>
	/// <div class="tablediv">
	/// <table class="dtTABLE" cellspacing="0">
	///		<tr>
	///			<th width="20%">Author</th>
	///			<th width="20%">Date</th>
	///			<th width="60%">Remarks</th>
	///		</tr>
	///		<tr>
	///			<td width="20%">Author</td>
	///			<td width="20%">month/day/year</td>
	///			<td width="60%">Comment</td>
	///		</tr>
	/// </table>
	/// </div>
	/// </remarks>
	public class MessageSingleton
	{
		#region Members

		/// <summary>  
		/// unique instance of MessageSingleton 
		///</summary>
		private	static MessageSingleton mInstance = null;
		/// <summary>
		/// holds messages, identifier (int) as key, messages (strings) as values
		/// </summary>
		private Hashtable mMessages;
		internal const int ERROR_ON_APPLICATION_START = 1;
		internal const int NO_SEARCH_CRITERIA	= 2;
		internal const int NO_RESULTS			= 3;
		
		internal const int APPLICATION_EXIT	    = 5;
		internal const int INVALID_USER		    = 6;
		
		internal const int NO_VALID_DATE		= 8;
		internal const int NO_FUTURE_VALID	    = 9;
		internal const int NO_BEFORE_VALIDFROM  = 10;
		internal const int NO_PAST_VALIDFROM	= 11;
		internal const int ACTION_CANCELLED     = 12;
        internal const int TOO_MANY_SEARCHVALS  = 14;
        internal const int VALUE_NOT_NUMERIC    = 15;

		// CoWorker Summary, delete etc
		internal const int NO_CWR_EXCO_OR_COORD  = 21;
		internal const int EXTEND_CWR_EXCO_INVAL = 22;
		internal const int CANNOT_SEARCH_CHAR    = 23;
		internal const int DYNDATA_FROMTILLDATE  = 24;

		// System & User Administration
		internal const int NO_ADMIN_ROW		     = 50;
		internal const int NO_ADMIN_TO_SAVE	     = 51;
		internal const int PROMPT_SAVE			    = 52;
		internal const int NO_EXCO_COORD_RESULT     = 53;
		internal const int ADMIN_EXCO_DUPLICATE     = 530;
		internal const int ADMIN_EXCO_RENAME        = 531;
		internal const int ADMIN_EXCO_CANNOT_RENAME = 532;
		internal const int ADMIN_EXCO_DEP_DATA = 533;
		internal const int ADMIN_EXCO_DELETE   = 534;
		internal const int ADMIN_EXCO_INVALID  = 535;
        internal const int ADMIN_PLANT_EXISTS = 536;
		internal const int NO_ADMIN_ASSIGN		= 54;
		internal const int ADMIN_ASSIGN_DUPL   = 55;
		internal const int ADMIN_UPDATE_CONFL  = 56;
		internal const int ADMIN_DEPENDENT_DATA= 57;
		internal const int NO_MANDATOR_SELECTED= 58;
		internal const int NO_USER_ROW			= 59;
		internal const int NO_ROLE_ROW			= 60;
		internal const int NO_ROLES_ASSIGNED	= 61;
		internal const int ROLE_ASSIGN_DUPL     = 62;
		internal const int NO_USERMAN_COMBO	    = 63;
		internal const int REVOKE_PLANTMANAGER = 65;
		internal const int DELETE_COORDINATOR  = 660;
		internal const int REVOKE_COORDINATOR  = 661;
		internal const int DEL_ASS_COORDINATOR = 662;
		internal const int COORD_DEP_CWR_USER  = 663;
		internal const int COORD_DEP_CWR_ROLE  = 664;
		internal const int COORD_DEP_CWR_ASS   = 665;
		internal const int COORD_DEP_CWR_END   = 667;
		internal const int COORD_REASS_ERROR   = 668;
		internal const int USER_NOT_UNIQUE     = 67;
		internal const int NO_SAVE			    = 68;
		internal const int COWORKER_NOTEXISTS  = 69;
        internal const int COWORKER_REVOKE_COMMENT  = 70;

		internal const int SAVE_SUCCESS		    = 90;
		internal const int MUST_SAVE_FIRST		= 91;
		internal const int DELETE_SUCCESS		= 92;
		internal const int DELETE_QUESTION		= 93;
		internal const int DELETE_USER_QUESTION = 94;
		internal const int PRINTPASS_SUCCESS	= 95;
		internal const int MASKTICKET_SUCCESS	= 96;
		
		internal const int DELETE_MED_QUESTION  = 98;

		internal const int FATAL_CONFIG_ERROR   = 99;
		internal const int FATAL_DB_ERROR       = 100;
		internal const int INITIALIZATION_ERROR = 101;
		internal const int FATAL_VERSION_ERROR  = 102;

		// Reports/Export
		internal const int REPORT_DATECREATED  = 200;
		internal const int REPORT_VALIDUNTIL   = 201;
		internal const int REPORT_PASS = 202;
		internal const int REPORT_ACROBAT_READER = 203;
		internal const int REPORT_DIRECTORY_ERROR = 204;
		internal const int REPORT_ERROR = 205;
		internal const int REPORT_NO_REPORT = 206;
		internal const int REPORT_NO_CONTRACTOR = 207;
		internal const int REPORT_NO_EXCONTRACTOR = 208;
		internal const int REPORT_NO_EXCONTRDYNDATA = 209;
		internal const int REPORT_NO_DATA_FOUND = 210;
		internal const int REPORT_WRONG_VERSION = 211;
		internal const int REPORT_NO_BOOKXDAYS  = 212;
		internal const int REPORT_ATTENDANCE_PERIOD = 213;
		internal const int EXPORT_ERROR   = 215;
		internal const int EXPORT_NO_EXPORT = 216;
        internal const int EXPORT_ATTEND_CHECK = 217;
        internal const int EXPORT_ATTEND_PERSNO = 218;
        
	
		// ID cards: Interface FPASS <> ZKS and SmartAct
		internal const int ZKS_OK = 701;
        internal const int ZKS_NO_CONNECT = 702;
        
        internal const int ZKS_NO_INSERT_UPDATE = 704;
        internal const int ZKS_NO_UPDATE = 705;
        internal const int ZKS_NO_UPDATE_INSERT = 706;
        internal const int ZKS_NO_DELETE_NO_CWR = 707;
        internal const int ZKS_NO_IDCARDNO = 708;
        internal const int ZKS_NO_IDCARDNO_USED = 709;
        internal const int ZKS_NO_INSERT_ALL = 710;
        internal const int ZKS_COMP_NOT_IN_DB = 711;
        internal const int ZKS_NO_UPDATE_IDCARDNO = 712;
        internal const int ZKS_OK_DELETE = 713;
        internal const int ZKS_NO_DELETE_NO_IDCARD = 714;
        internal const int ZKS_NO_DELETE_NO_IDCARDNO = 715;
        internal const int ZKS_NO_SUCCESS_GUI = 716;
        internal const int IDCARD_EMPTY_NOTALLOWED = 717;
        internal const int IDCARD_CONFIRM_EDIT = 718;
        internal const int IDCARD_INUSE = 719;

        internal const int ZKS_SAVE_INVALID = 721;
        internal const int ZKS_DEL_INVALID = 722;
        internal const int ZKS_NO_HITAG = 723;
        internal const int ZKS_NO_MIFARE = 724;
        internal const int ZKS_ACCESS_EXPIRED = 725;

        // ZKS (Neue Ausweise)
        internal const int SMARTACT_EXP_FPASS_OK = 741;
        internal const int SMARTACT_EXP_FPASS_ERR = 742;
        internal const int SMARTACT_EXP_SAVE_ERR = 743;
        internal const int SMARTACT_EXP_NOID_ERR = 744;
        internal const int INVALID_TEXT_WINDOWSID = 745;
        internal const int SMARTACT_NOTIFY_START = 746;
        internal const int SMARTACT_NOTIFY_STOP = 747;
        internal const int SMARTACT_NOTIFY_CANCEL = 748;
        internal const int SMARTACT_NOTIFY_RUNNING = 749;
        internal const int SMARTACT_NO_DATA = 750;
        internal const int SMARTACT_YES_CONFIRM = 751;
        internal const int SMARTACT_NO_CONFIRM = 752;

        // Delete Coworker (L�schliste)
        internal const int CWR_DELETE_ALL = 771;
        internal const int CWR_DELETE_SUCCESS = 772;
        internal const int CWR_DELETE_STATUS_ERR = 773;
        internal const int CWR_DELETE_ERR = 774;

		// Respira. Masks Florix & FPASS
        internal const int MASK_TICKET_NO_CWR = 801;
        internal const int MASKNR_NOT_FOUND = 802;
        internal const int MASKNR_NOT_AVAIL = 803;
        internal const int MASK_MAINT_OVERDUE = 804;
        internal const int MASK_ALREADY_LENT = 805;
        internal const int MASK_RETURN_NOT_LENT = 806;
        internal const int NO_MASKNR = 807;
        internal const int MASK_WRONG_RETURN_DATE = 808;
        internal const int MASK_RETURN_NR_NOT_SAME = 809;
        internal const int COWORKER_ALREADY_MASK = 810;
        internal const int MASK_IN_PAST = 811;
        internal const int MASK_NO_BRIEFING = 812;
		internal const int MASK_ONLY_ONE_MASKSYSTEM_ALLOWED = 813;

		internal const int PREC_MEDICAL_MAX_ASSIGNED = 814;

		// Query ADS
		internal const int ADS_ERR_NEED_PWD = 831;
        internal const int ADS_WARN_NO_USER = 832;
        internal const int ADS_ERR_EMPTY_FIELD = 833;

		// Edit coworker
		internal const int INVALID_SELECT			= 250;
		internal const int INVALID_ACCESS_EXTEND	= 251;

		internal const int INVALID_COMBOBOX_COORD  = 253;
		internal const int INVALID_COMBOBOX_CONTRACTOR = 254;
		internal const int INVALID_TEXT_SURNAME	= 255;
		internal const int INVALID_TEXT_FIRSTNAME	= 256;
		internal const int INVALID_TEXT_BIRTH_PLACE = 257;
		internal const int INVALID_BIRTHDATE		= 258;
		internal const int INVALID_PRECMED			= 259;
		internal const int INVALID_FULLDATE		    = 260;
		internal const int INVALID_MYDATE			= 261;
		internal const int INVALID_CHECKOFF		    = 262;
		internal const int INVALID_CHECKOFFDATE	    = 263;
		internal const int COWORKER_EXISTS			= 266;
		internal const int INVALID_CHECKINDATE		= 267;

		internal const int INVALID_PLANTMANAGER	    = 269;
		internal const int INVALID_PLANTDIRECTED	= 270;			
		internal const int INVALID_EDIT_PLANT = 271;
        internal const int INVALID_ZKS_PLANT = 272;
		internal const int INVALID_COORD_HIST = 273;
		internal const int COWORKER_ALREADY_BRIEF = 274;

		//Mask Vehicle Access
		internal const int ACCESS_VALID = 301;

		#endregion

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		private MessageSingleton()
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
			mMessages = new Hashtable();
			mMessages.Add(APPLICATION_EXIT, "Bitte benachrichtigen Sie den Systemadministrator.");
			mMessages.Add(ERROR_ON_APPLICATION_START, "Fehler bei Anwendungsstart.");
			mMessages.Add(NO_SEARCH_CRITERIA, "Bitte geben Sie mindestens ein Suchkriterium bei der Suche an.");
			mMessages.Add(NO_RESULTS, "Ihre Suche brachte kein Ergebnis.");
			mMessages.Add(ACTION_CANCELLED, "Aktion abgebrochen.");
            mMessages.Add(TOO_MANY_SEARCHVALS, "Sie haben mehr als 1000 Werte f�r die Suche ausgew�hlt. Bitte w�hlen Sie weniger aus.");
            mMessages.Add(VALUE_NOT_NUMERIC, "Bitte geben Sie ins Feld {0} einen numerischen Wert ein!");
			mMessages.Add(NO_CWR_EXCO_OR_COORD, "Sie m�ssen diesem Fremdfirmenmitarbeiter einen Koordinator und eine Fremdfirma zuordnen!");
			mMessages.Add(EXTEND_CWR_EXCO_INVAL, "Die Fremdfirma dieses Fremdfirmenmitarbeiters hat den Status 'Ung�ltig', Zutrittsverl�ngerung nicht m�glich.");
			mMessages.Add(CANNOT_SEARCH_CHAR, "Sie haben ein ung�ltiges Zeichen angegeben (z.B \" ' \"). Bitte korrigieren Sie Ihre Eingabe.");
			mMessages.Add(DYNDATA_FROMTILLDATE, "Das Datum der Buchung 'von' muss vor dem Datum 'bis' liegen!"); 
			mMessages.Add(NO_ADMIN_ROW, "Bitte w�hlen Sie eine Zeile aus der Tabelle aus (ev. erst eine Suche durchf�hren)!");
			mMessages.Add(NO_ADMIN_TO_SAVE, "Sie m�ssen alle Pflichtfelder ausf�llen, um �nderungen zu speichern.");
			mMessages.Add(PROMPT_SAVE, "M�chten Sie Ihre �nderungen speichern?");
			mMessages.Add(NO_EXCO_COORD_RESULT, "F�r diese Kombination Fremdfirma - Koordinator liegen keine Zuordnungen vor.");
			mMessages.Add(ADMIN_EXCO_DUPLICATE, "Eine Fremdfirma mit dem angegebenen Namen existiert bereits. Bitte nutzen Sie einen anderen Namen   oder verwerfen Sie Ihre �nderungen ('Abbrechen').");
			mMessages.Add(ADMIN_EXCO_RENAME, "Wenn Sie diese Fremdfirma umbenennen, erh�lt sie automatisch den Status 'Ung�ltig'. Eine Neue mit   dem neuen Namen wird angelegt. M�chten Sie fortfahren?");
			mMessages.Add(ADMIN_EXCO_CANNOT_RENAME, "Es existiert noch eine Firma mit dem alten Namen. L�schen Sie bitte alle ung�ltigen FFMA, die dieser Firma zugeordnet sind."
													+ " Sie k�nnen diese Firma anschlie�end aber erst morgen umbenennen, weil die Firmenliste heute Nacht aktualisiert werden muss." 
													+ " Bitte brechen Sie den Vorgang mit dem Button 'Abbrechen' ab.");
			mMessages.Add(ADMIN_EXCO_DELETE, "Wenn Sie diese Fremdfirma l�schen, wird sie nicht archiviert. M�chten Sie fortfahren?");
			mMessages.Add(ADMIN_EXCO_DEP_DATA, "Diese Fremdfirma kann nicht gel�scht werden, da sie von anderen Daten referenziert wird.");
			mMessages.Add(ADMIN_EXCO_INVALID, "Diese Fremdfirma hat den Status 'Ung�ltig' und darf nicht editiert werden.");
            mMessages.Add(ADMIN_PLANT_EXISTS, "Ein Betrieb mit diesem Namen existiert bereits. Bitte korrigieren Sie Ihre Eingabe.");
			mMessages.Add(NO_ADMIN_ASSIGN, "Bitte w�hlen Sie erst eine Fremdfirma und einen Koordinatoren aus!");
			mMessages.Add(ADMIN_ASSIGN_DUPL, "Diese Zuordnung Fremdfirma - Koordinator existiert bereits! Bitte brechen Sie mit dem Button 'Ab-   brechen' ab.");
			mMessages.Add(ADMIN_UPDATE_CONFL, "Dieser Vorgang wurde wegen eines Zugriffskonfliktes abgebrochen. Bitte aktualisieren Sie Ihre Daten in der �bersicht �ber den Button 'Suchen'.");
			mMessages.Add(ADMIN_DEPENDENT_DATA, "Die aktuelle Zeile kann nicht gel�scht werden, da sie von anderen Daten referenziert wird.");
			mMessages.Add(NO_USER_ROW, "Bitte w�hlen Sie erst einen Benutzer aus!");
			mMessages.Add(NO_ROLES_ASSIGNED, "Dem aktuellen Benutzer sind keine Rollen zugeordnet.");
			mMessages.Add(ROLE_ASSIGN_DUPL, "Diese Rolle wurde dem aktuellen Benutzer bereits zugeordnet!");
			mMessages.Add(NO_USERMAN_COMBO, "Bitte w�hlen Sie eine Abteilung und eine Dom�ne aus!");
			mMessages.Add(REVOKE_PLANTMANAGER, "Sie k�nnen dem aktuellen Benutzer die Rolle 'Betriebsmeister' nur entziehen, indem Sie in der Maske 'Userverwaltung' die Zuordnungen zu den Betrieben aufheben."); 
			mMessages.Add(DELETE_COORDINATOR, "Der aktuelle Benutzer ist auch Koordinator. Eventuell m�ssen Sie w�hrend des Vorgangs die zugeordneten Fremdfirmenmitarbeiter auf einen anderen Koordinator umgeh�ngen. M�chten Sie fortfahren?");
			mMessages.Add(REVOKE_COORDINATOR, "Wenn Sie diesem Benutzer die Rolle 'Koordinator' entziehen, m�ssen alle zugeordneten Fremdfirmenmitarbeiter auf einen anderen Koordinator umgeh�ngt werden. M�chten Sie fortfahren?");
			mMessages.Add(DEL_ASS_COORDINATOR, "Wenn Sie diese Zuordnung aufheben, m�ssen Sie w�hrend des Vorgangs eventuell alle zugeordneten Fremdfirmenmitarbeiter auf einen anderen Koordinator umgeh�ngen. M�chten Sie fortfahren?");
			mMessages.Add(COORD_DEP_CWR_USER, "Der aktuelle Benutzer kann nicht gel�scht werden, weil keine alternativen Koordinatoren f�r die Mitarbeiter der Fremdfirma ");
			mMessages.Add(COORD_DEP_CWR_ROLE, "Eine Entziehung der Rolle 'Koordinator' ist f�r diesen Benutzer nicht m�glich, da keine alternativen Koordinatoren f�r die Mitarbeiter der Fremdfirma ");
			mMessages.Add(COORD_DEP_CWR_ASS, "Die aktuelle Zuordnung kann nicht gel�scht werden, weil keine alternativen Koordinatoren f�r die Mitarbeiter dieser Fremdfirma ");
			mMessages.Add(COORD_DEP_CWR_END, " vorliegen.");
			mMessages.Add(COORD_REASS_ERROR, "Die Zuordnungen der Fremdfirmenmitarbeiter zu diesem Koordinator konnten nicht umgeh�ngt werden. Der Vorgang wurde abgebrochen.");
			mMessages.Add(USER_NOT_UNIQUE, "Ein Benutzer mit dem angegebenen Loginnamen (Feld 'User') und der aktuellen Dom�ne existiert schon!");
			mMessages.Add(NO_ROLE_ROW, "Bitte w�hlen Sie erst eine Rolle aus!");
			mMessages.Add(INVALID_USER, "User \"" + UserManagementControl.getInstance().CurrentOSUserDom 
												  + "\\" + UserManagementControl.getInstance().CurrentOSUserName
												  +  "\" ist nicht berechtigt, FPASS zu starten!"); 
			
			mMessages.Add(NO_MANDATOR_SELECTED, "Bitte w�hlen Sie einen eindeutigen Mandaten aus." );
			mMessages.Add(SAVE_SUCCESS, "Ihre �nderungen wurden erfolgreich gespeichert.");
			mMessages.Add(MUST_SAVE_FIRST, "Bitte speichern Sie zuerst Ihre �nderungen.");
			mMessages.Add(DELETE_SUCCESS, "Das L�schen war erfolgreich.");
			mMessages.Add(DELETE_QUESTION, "M�chten Sie die ausgew�hlte Zeile wirklich l�schen?");
			mMessages.Add(DELETE_USER_QUESTION, "M�chten Sie den ausgew�hlten Benutzer wirklich l�schen?");			
			mMessages.Add(DELETE_MED_QUESTION, "M�chten Sie die ausgew�hlte Vorsorgeuntersuchung wirklich l�schen? Dies hat eventuelle Auswirkungen auf den Zutritt des Mitarbeiters.");
			mMessages.Add(FATAL_CONFIG_ERROR, "Die Konfigurationsdatei ist ung�ltig oder konnte nicht gelesen werden. Die Anwendung wird beendet.");
			mMessages.Add(FATAL_DB_ERROR, "Fehler bei der Kommunikation mit der Datenbank.");
			mMessages.Add(NO_VALID_DATE, "Das gew�hlte Datum liegt vor dem aktuellen Tagesdatum. Bitte korrigieren Sie!");
			mMessages.Add(NO_FUTURE_VALID, "Das gew�hlte Datum darf nicht in der Zukunft liegen. Bitte korrigieren Sie!");
			mMessages.Add(NO_PAST_VALIDFROM, "Das gew�hlte Datum 'G�ltig von' liegt vor dem Tagesdatum.. Bitte korrigieren Sie!");
			mMessages.Add(NO_BEFORE_VALIDFROM, "Das gew�hlte Datum liegt vor dem Datum 'G�ltig von'. Bitte korrigieren Sie!");
            mMessages.Add(NO_SAVE, "Ein Speichern der �nderungen ist nicht notwendig.");
            mMessages.Add(COWORKER_NOTEXISTS, "Dieser Fremdfirmenmitarbeiter wurde bereits gel�scht und kann nicht mehr bearbeitet werden.");
            mMessages.Add(COWORKER_REVOKE_COMMENT, "Bitte tragen Sie einen Kommentar ein, bevor Sie den Zutritt dieses FFMA sperren.");

			// Reports/Export
			mMessages.Add(REPORT_DATECREATED, "Bitte geben Sie in den Feldern � Bewegungsdaten Von � Bis� im Format TT.MM.JJJJ einen Zeitraum ein!");
			mMessages.Add(REPORT_VALIDUNTIL, "Bitte geben Sie in den Feldern � Passierschein g�ltig von � bis� im Format TT.MM.JJJJ einen Zeitraum ein!");
			mMessages.Add(REPORT_PASS, "Bitte w�hlen Sie einen Fremdfirmenmitarbeiter in der Tabelle aus.");
			mMessages.Add(REPORT_DIRECTORY_ERROR, "Der Report kann nicht erstellt werden: Das Report-Verzeichnis '{0}' ist nicht vollst�ndig.");
			mMessages.Add(REPORT_ACROBAT_READER, "Der Report wurde erstellt, kann aber nicht angezeigt werden: Acrobat Reader konnte nicht gestartet werden, um den pdf-Report anzuzeigen. Acrobat Reader ist vielleicht nicht richtig installiert oder falsch registriert. Bitte installieren Sie Acrobat Reader auf Ihrem Rechner neu oder fragen Sie Ihren System-Administrator.");
			mMessages.Add(REPORT_ERROR, "Die Erstellung des Reports wurde aufgrund eines unbekannten Fehlers abgebrochen. Bitte versuchen Sie es sp�ter noch einmal, den Report zu erstellen, oder benachrichtigen Sie Ihren System-Administrator.");
			mMessages.Add(REPORT_NO_REPORT, "Der Report kann nicht erstellt werden.");
			mMessages.Add(REPORT_NO_CONTRACTOR, "Bitte w�hlen Sie eine Fremdfirma in der Tabelle aus.");
			mMessages.Add(REPORT_NO_EXCONTRACTOR, "Bitte w�hlen Sie eine Fremdfirma als Suchkriterium aus.");
			mMessages.Add(REPORT_NO_EXCONTRDYNDATA, "Bitte w�hlen Sie eine Fremdfirma und einen Zeitraum f�r die Bewegungsdaten als Sunchkriterien aus!");
			mMessages.Add(REPORT_NO_DATA_FOUND, "Der Report kann nicht erstellt werden. Der FFMA wurde in der Datenbank nicht gefunden. Eventuell hat ein anderer Anwender den FFMA gel�scht. Bitte f�hren Sie die Suche erneut aus.");
			mMessages.Add(REPORT_WRONG_VERSION, "Die Anwendung verwendet veraltete Reportvorlagen. Bitte wenden Sie sich an Ihren Systemadministrator");
			mMessages.Add(REPORT_NO_BOOKXDAYS, "Bitte geben Sie im Feld 'Keine Buchung seit .. Tagen' einen numerischen Wert ein!");
			mMessages.Add(REPORT_ATTENDANCE_PERIOD, "Die von Ihnen ausgew�hlte Periode 'Bewegungsdaten von/bis' darf nicht l�nger als 366 Tage sein!");
			mMessages.Add(EXPORT_ERROR, "Die Erstellung der Export-Datei wurde aufgrund eines unbekannten Fehlers abgebrochen. Bitte versuchen Sie es sp�ter noch einmal, die Daten zu exportieren, oder benachrichtigen Sie Ihren System-Administrator.");
			mMessages.Add(EXPORT_NO_EXPORT, "Der von Ihnen ausgew�hlte Report kann nicht exportiert werden.");
            mMessages.Add(EXPORT_ATTEND_CHECK, "Pr�fen nach doppelten PersNr in diesen Anwesenheitszeiten...");
            mMessages.Add(EXPORT_ATTEND_PERSNO, "Die im Report angezeigten Anwesenheitszeiten sind unvollst�ndig, da mindestens eine Personalnummer  bei den Fremdfirmenmitarbeitern aus ZKS nicht eindeutig ist. "
                                                + "F�r diese Mitarbeiter wurden keine Anwesenheitszeiten berechnet. \n\n"
                                                + "Es handelt sich in diesem Fall um Fremdfirmenmitarbeiter im ZKS, die in FPASS nicht vorhanden sind. \n\n"
                                                + "FPASS wird anschlie�end einer Liste der entsprechenden Buchungen (Bewegungsdaten aus ZKS) �ffnen, die Sie als Datei speichern k�nnen.");
		    
			// Ausweisnummern, Interface FPASS - ZKS
			mMessages.Add(ZKS_OK, "Transaktion in ZKS erfolgreich abgeschlossen");
			mMessages.Add(ZKS_NO_CONNECT, "Die Verbindung zu ZKS bzw. zum Ausleseterminal konnte nicht aufgebaut werden. \n Versuchen Sie es sp�ter noch einmal, die Verbindung aufzubauen oder fragen Sie Ihren System-Administrator.");
            mMessages.Add(ZKS_SAVE_INVALID, "Der Fremdfirmenmitarbeiter mit der Personalnummer {0} konnte nicht nach ZKS exportiert werden.");
            mMessages.Add(ZKS_DEL_INVALID, "Der Fremdfirmenmitarbeiter mit der Personalnummer {0} konnte in ZKS nicht gel�scht werden.");
            mMessages.Add(ZKS_NO_HITAG, " Der Hitag-Ausweis fehlt in FPASS.");
            mMessages.Add(ZKS_NO_MIFARE, " Der Mifare-Ausweis fehlt in FPASS.");
            mMessages.Add(ZKS_ACCESS_EXPIRED, " Der Zutritt des Fremdfirmenmitarbeiters ist abgelaufen.");

            mMessages.Add(ZKS_NO_INSERT_UPDATE, "Die neuen Fremdfirmenmitarbeiter-Daten konnten in ZKS nicht hinzugef�gt werden: Dieser Mitarbeiter  ist in ZKS bereits vorhanden. Die Daten konnten aber in ZKS nicht aktualisiert werden. Versuchen Sie es sp�ter erneut, die Daten zu aktualisieren oder fragen Sie Ihren System-Administrator.");
			mMessages.Add(ZKS_NO_UPDATE, "Die Fremdfirmenmitarbeiter-Daten konnten in ZKS nicht aktualisiert werden: Der Mitarbeiter hat keine Ausweisnummer in FPASS.");
			mMessages.Add(ZKS_NO_UPDATE_INSERT, "Die Fremdfirmenmitarbeiter-Daten konnten in ZKS nicht aktualisiert werden: Dieser Mitarbeiter ist in ZKS nicht vorhanden. Ein neuer Mitarbeiter konnte jedoch nicht hinzugef�gt werden. Versuchen Sie es sp�ter noch einmal, die Daten zu aktualisieren oder fragen Sie Ihren System-Administrator.");
			mMessages.Add(ZKS_NO_DELETE_NO_CWR, "Die Ausweisnummer konnte in ZKS nicht gel�scht werden, da der Fremdfirmenmitarbeiter in ZKS nicht   gefunden wurde. Wollen Sie die Ausweisnummer in FPASS trotzdem l�schen?");
			mMessages.Add(ZKS_NO_IDCARDNO, "Die Ausweisnummer konnte aus dem Terminal nicht gelesen werden. Versuchen Sie es sp�ter noch einmal, die Ausweisnummer auszulesen oder fragen Sie Ihren System-Administrator");
			mMessages.Add(ZKS_NO_IDCARDNO_USED,"Die aus dem Terminal gelesene Ausweisnummer wurde bereits f�r einen Fremdfirmenmitarbeiter verwendet  Versuchen Sie es sp�ter noch einmal, eine andere Ausweisnummer auszulesen oder fragen Sie Ihren System-Administrator.");
			mMessages.Add(ZKS_NO_INSERT_ALL, "Nicht alle FPASS-Daten konnten in ZKS hinzugef�gt / aktualisiert werden. Betroffene Anzahl von Fremdfirmenmitarbeitern: ");
			mMessages.Add(ZKS_COMP_NOT_IN_DB, "Ihr FPASS-Rechner kann mit dem ZKS-Server nicht kommunizieren (die Terminalnummer wurde nicht gefunden). Bitte benachrichtigen Sie den Systemadministrator.");
			mMessages.Add(ZKS_NO_UPDATE_IDCARDNO, "Die Ausweisnummer des Fremdfirmenmitarbeiters konnte in ZKS nicht aktualisiert werden. Versuchen Sie es sp�ter noch einmal, die Daten zu aktualisieren oder fragen Sie Ihren System-Administrator.");
			mMessages.Add(ZKS_OK_DELETE, "Der Fremdfirmenmitarbeiter wurde in ZKS gel�scht. In FPASS wird die Ausweisnummer erst nach Speicherung der Fremdfirmenmitarbeiterdaten gel�scht.");
			mMessages.Add(ZKS_NO_DELETE_NO_IDCARD, "Die Fremdfirmenmitarbeiterdaten d�rfen nicht in ZKS gel�scht werden: Die am Terminal gelesene Ausweisnummer entspricht nicht der Ausweisnummer des Fremdfirmenmitarbeiters in FPASS.");
			mMessages.Add(ZKS_NO_DELETE_NO_IDCARDNO, "Die Ausweisnummer konnte nicht gel�scht werden (eventuell ist ZKS nicht verf�gbar). Versuchen Siees sp�ter noch einmal oder fragen Sie Ihren System-Administrator.");
			mMessages.Add(ZKS_NO_SUCCESS_GUI, "Die Fremdfirmenmitarbeiterdaten konnten nicht nach ZKS �bertragen werden, da die Ausweisnummer in ZKS eventuell schon vergeben ist. Der Fremdfirmenmitarbeiter wurde in FPASS ohne Ausweisnummer gespeichert. Versuchen Sie es sp�ter erneut, die Daten zu �bertragen oder fragen Sie Ihren System-Administrator.");
            mMessages.Add(IDCARD_EMPTY_NOTALLOWED, "Eine Ausweisnummer kann nicht manuell gel�scht werden. Bitte benutzen Sie den Button 'Ausweis-Nr entfernen' daf�r.");
            mMessages.Add(IDCARD_CONFIRM_EDIT, "Sie haben die Ausweisnummer '{0}' manuell eingetragen. Dr�cken Sie 'OK', um den FFMA mit dieser Ausweisnummer zu speichern, 'Abbrechen' um das Speichern abzubrechen.");
            mMessages.Add(IDCARD_INUSE, "Die Ausweisnummer ({0}) wurde bereits vergeben.");

            // SmartAct, Id photocards (Neue Ausweise) and WindowsId (KonzerId) 
            mMessages.Add(SMARTACT_EXP_FPASS_OK, "Der Export des aktuellen FFMA nach SmartAct war erfolgreich.");
            mMessages.Add(SMARTACT_EXP_FPASS_ERR, "Beim Export des aktuellen FFMA nach SmartAct ist ein Fehler aufgetreten. \n Bitte benachrichtigen Sie den Systemadministrator. \n\n");
            mMessages.Add(SMARTACT_EXP_SAVE_ERR, "Bitte speichern Sie den Fremdfirmenmitarbeiter, bevor Sie den Export nach SmartAct bet�tigen.");
            mMessages.Add(SMARTACT_EXP_NOID_ERR, "Dieser Fremdfirmenmitarbeiter hat keinen Lichtbildausweis (angeordnet oder erteilt. Ein Export nach SmartAct ist nicht m�glich.");
            mMessages.Add(INVALID_TEXT_WINDOWSID, "Das Feld 'KonzernID' muss gef�llt werden, wenn der Ausweis des FFMA einen PKI-Chip hat.");
            mMessages.Add(SMARTACT_NOTIFY_START, "Mit diesem Schritt starten Sie die automatische Benachrichtigung �ber Fremdfirmenmitarbeiter, die einen neuen Ausweis aus SmartAct erhalten haben. \n FPASS zeigt in regelm��igen Abst�nden daf�r ein Popup-Fenster an. Die �berwachung kann jederzeit beendet werden. \n Wollen Sie fortfahren?");
            mMessages.Add(SMARTACT_NOTIFY_STOP, "Automatische Benachrichtigung �ber die Fremdfirmenmitarbeiter mit neuem Ausweis beendet.");
            mMessages.Add(SMARTACT_NOTIFY_CANCEL,"(Hintergrundprozess wurde vom Benutzer abgebrochen).");
            mMessages.Add(SMARTACT_NOTIFY_RUNNING, "Die automatische Benachrichtigung �ber Fremdfirmenmitarbeiter mit neuem Ausweis l�uft bereits.");
            mMessages.Add(SMARTACT_NO_DATA, "Aktuell sind keine Fremdfirmenmitarbeiter mit einem neuen Ausweis aus SmartAct da, die nicht auch in ZKS vorhanden sind (Nichts zu �bertragen).");
            mMessages.Add(SMARTACT_YES_CONFIRM, "Mit diesem Schritt veranlassen Sie, dass ein Lichtbildwausweis f�r den aktuellen FFMA erstellt wird.");
            mMessages.Add(SMARTACT_NO_CONFIRM, "Es wurde bereits ein Lichtbildausweis {0} PKI erstellt, er {1} ausgegeben. \n Soll dieser Ausweis endg�ltig gel�scht werden?");

            // Coworker delete
            mMessages.Add(CWR_DELETE_ALL, "M�chten Sie alle Fremdfirmenmitarbeiter wirklich l�schen?");
            mMessages.Add(CWR_DELETE_SUCCESS, "Die Archivierung der Fremdfirmenmitarbeiter ist fertig."); 
            mMessages.Add(CWR_DELETE_STATUS_ERR, "Die angezeigten Fremdfirmenmitarbeiter mit dem Status 'G�ltig' k�nnen nicht archiviert werden.");
            mMessages.Add(CWR_DELETE_ERR, "Bei der Archivierung sind Fehler aufgetreten.");

            // Query ADS
            mMessages.Add(ADS_ERR_NEED_PWD, "Ohne Angabe des Passworts ist eine Abfrage gegen das Active Directory nicht m�glich.");
            mMessages.Add(ADS_WARN_NO_USER, "Keine Benutzer gefunden.");
            mMessages.Add(ADS_ERR_EMPTY_FIELD, "Bitte geben Sie einen Text an, nach dem im Active Directory gesucht werden soll.");


			// Interface Florix
			mMessages.Add(MASK_TICKET_NO_CWR, "Sie k�nnen keinen Maskenr�ckgabebeleg drucken, da der aktuelle Fremdfirmenmitarbeiter eine Maske noch ausgeliehen hat.");
            mMessages.Add(MASKNR_NOT_FOUND, "Die Maske mit der Nummer '{0}' wurde im System {1} nicht gefunden.");
            mMessages.Add(MASKNR_NOT_AVAIL, "Die Maske mit der Nummer '{0}' ist im System {1} nicht verf�gbar.");
			mMessages.Add(MASK_MAINT_OVERDUE, "Die Maske kann nicht ausgeliehen werden, weil deren Wartungsdatum abgelaufen ist.");
            mMessages.Add(MASK_ALREADY_LENT, "Die Atemschutzmaske mit der Nummer '{0}' wurde der Person {1} �ber FPASS bereits ausgeliehen, und ist deshalb nicht verf�gbar."); 
            mMessages.Add(MASK_RETURN_NOT_LENT, "Die Atemschutzmaske mit der genannten Maskennummer ist bereits zur�ckgegeben worden.");
			mMessages.Add(NO_MASKNR, "Bitte geben Sie eine Maskennummer ein.");
			mMessages.Add(MASK_WRONG_RETURN_DATE, "Das R�ckgabedatum der Atemschutzmaske liegt vor dem Ausgabedatum. Bitte pr�fen Sie Ihre Eingabe.");
			mMessages.Add(MASK_RETURN_NR_NOT_SAME, "Die ausgeliehenen und abgegebenen Masken m�ssen identische Nummern haben.");
			mMessages.Add(COWORKER_ALREADY_MASK, "Der Fremdfirmenmitarbeiter hat bereits eine Maske ausgeliehen.");
			mMessages.Add(MASK_IN_PAST, "Sie k�nnen zum angegebenen Datum keine Maske ausliehen. Der Fremdfirmenmitarbeiter hat nach dem angegebenen Datum bereits Masken ausgeliehen.");
			mMessages.Add(MASK_NO_BRIEFING, "Die Maske kann nur ausgegeben werden, wenn die Belehrung Atemschutzmasken erteilt wurde.");
            mMessages.Add(MASK_ONLY_ONE_MASKSYSTEM_ALLOWED, "Es darf nur eine Maske ausgeliehen werden. (TecBos ODER Florix).");
			mMessages.Add(PREC_MEDICAL_MAX_ASSIGNED, "Sie k�nnen jedem FFMA maximal {0} Vorsorgeuntersuchungen zuordnen. Bitte pr�fen Sie ggf. Ihre Eingabe.");

			// Validations et al
			mMessages.Add(INVALID_SELECT, "Datenbank-Fehler in der Anwendung FPASS.");
			mMessages.Add(INVALID_ACCESS_EXTEND, "Der Zutritt konnte nicht f�r alle Fremdfirmenmitarbeiter verl�ngert werden." );			
			mMessages.Add(INVALID_BIRTHDATE, "Das Feld 'Geburtsdatum' muss mit einem g�ltigem Datum im Format TT.MM.JJJJ gef�llt sein. Das Datum  darf nicht in der Zukunft liegen.");
			mMessages.Add(INVALID_COMBOBOX_COORD, "Ein Koordinator muss ausgew�hlt werden.");
			mMessages.Add(INVALID_COMBOBOX_CONTRACTOR, "Eine Fremdfirma muss ausgew�hlt werden.");
			mMessages.Add(INVALID_TEXT_SURNAME, "Das Feld 'Vorname' muss gef�llt werden.");
			mMessages.Add(INVALID_TEXT_FIRSTNAME, "Das Feld 'Nachname' muss gef�llt werden.");
			mMessages.Add(INVALID_TEXT_BIRTH_PLACE, "Das Feld 'Geburtsort' muss gef�llt werden.");
			mMessages.Add(INVALID_PRECMED, "Eine Vorsorgeuntersuchung muss ausgew�hlt sein, wenn sie erteilt werden soll."); 
			mMessages.Add(INVALID_MYDATE, "Das Feld f�r das Bewegungsdatum muss im Format TT.MM.JJJJ sein." );
			mMessages.Add(INVALID_FULLDATE, "Das Datum muss im Format TT.MM.JJJJ gef�llt sein." );
			mMessages.Add(INVALID_CHECKOFF, "Wird ein Auftrag als erledigt gekennzeichnet, muss ein Datum angegeben werden.");
			mMessages.Add(PRINTPASS_SUCCESS, "Passierschein gedruckt!" );
			mMessages.Add(MASKTICKET_SUCCESS, "Ihre �nderungen wurden gespeichert und der Maskenr�ckgabebeleg gedruckt." );
			mMessages.Add(INVALID_CHECKOFFDATE, "Das eingetragene Datum im Feld 'Abmeldung am' muss das Format TT.MM.JJJJ haben und darf nicht in der Vergangenheit liegen." );
			mMessages.Add(INITIALIZATION_ERROR, "Fehler beim Initialisieren der Anwendung.");
			mMessages.Add(FATAL_VERSION_ERROR, "Diese Version von FPASS (Version "
							+ Globals.GetInstance().FPASSApplicationVersion
							+ ") ist nicht mehr aktuell.");
			
			mMessages.Add(COWORKER_EXISTS, "Ein Fremdfirmenmitarbeiter mit diesen Daten existiert bereits. Ber�cksichtigt werden die Kriterien  Name der Fremdfirma und Nachname des Fremdfirmenmitarbeiters." 
							+ " Bitte versuchen Sie zun�chst den Fremdfirmenmitarbeiter �ber die Suchfunktion zu finden."
							+ " Wollen Sie Ihre Angaben trotzdem speichern?");
			mMessages.Add(ACCESS_VALID, "Bitte w�hlen Sie einen Fremdfirmenmitarbeiter in der Tabelle aus." );
			mMessages.Add(INVALID_CHECKINDATE, "Das eingetragene Datum im Feld 'Anmeldung am' muss das Format TT.MM.JJJJ haben und darf nicht in der Vergangenheit liegen." );
			mMessages.Add(INVALID_PLANTMANAGER, "Sie k�nnen nur Belehrungen f�r die Betriebe erteilen, denen Sie als Betriebsmeister zugeordnet sind");
			mMessages.Add(INVALID_PLANTDIRECTED, "F�r diesen Betrieb ist keine Belehrung mehr angeordnet.");
            mMessages.Add(INVALID_EDIT_PLANT, "Der ausgew�hlte Betrieb kann nicht mehr bearbeitet werden, weil die Anordnung der Belehrung wider-  rufen wurde.");
			mMessages.Add(INVALID_ZKS_PLANT, "Der ausgew�hlte Betrieb wurde aus  ZKS importiert und kann daher in FPASS nicht bearbeitet werden.");
			mMessages.Add(INVALID_COORD_HIST, "Ein/e neue/r Fremdfirmenmitarbeiter/in hat keine Historie der zugeordneten Koordinatoren!");
			mMessages.Add(COWORKER_ALREADY_BRIEF, "Dem aktuellen Fremdfirmenmitarbeiter wurde diese Belehrung bereits angeordnet. Bitte gehen Sie ohne zu speichern zur�ck zur �bersicht und �ffnen den FFMA erneut.");
            
           
		}	

		#endregion //End of Initialization

		#region Accessors 

		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// Returns the one and only instance of this class.
		/// </summary>
		/// <returns>instance of MessageSingleton</returns>
		public static MessageSingleton GetInstance() 
		{
			if ( null == mInstance ) 
			{
				mInstance = new MessageSingleton();
			}
			return mInstance;
		}

		/// <summary>
		/// simple getter
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public String GetMessage(int key) 
		{
			String message = (String)mMessages[key];
			//return FormatMessage( message );
            return message;
		}


		/// <summary>
		/// Ensures that a message has lineseparators all 150 characters ( to display them in a messagebox )
		/// </summary>
		/// <param name="pMessage">given message</param>
		/// <returns></returns>
		private String	FormatMessage(String pMessage) 
		{
            StringBuilder sb;

            sb = new StringBuilder(pMessage);

            while (sb.Length < 151)
            {
                sb.Append(" ");
            }
            sb.Insert(150, "\r\n");

            return sb.ToString();
		} 


		#endregion // End of Methods


	}
}

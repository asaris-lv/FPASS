using System;
using System.Text;
using System.Collections;


namespace Evonik.FPASSMail.Util.Messages
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

        internal const int MSG_EMAIL_INACTIVE = 16;
        internal const int MSG_EMAIL_SUCCESS = 17;

       

		internal const int NO_CWR_EXCO_OR_COORD  = 21;
		internal const int EXTEND_CWR_EXCO_INVAL = 22;
		internal const int CANNOT_SEARCH_CHAR    = 23;
		internal const int DYNDATA_FROMTILLDATE  = 24;

        internal const int FATAL_CONFIG_ERROR = 99;
        internal const int FATAL_DB_ERROR = 100;
        internal const int INITIALIZATION_ERROR = 101;

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
            mMessages.Add(TOO_MANY_SEARCHVALS, "Sie haben mehr als 1000 Werte für die Suche ausgewählt. Bitte wählen Sie weniger aus.");
            mMessages.Add(VALUE_NOT_NUMERIC, "Bitte geben Sie ins Feld {0} einen numerischen Wert ein!");
			mMessages.Add(NO_CWR_EXCO_OR_COORD, "Sie müssen diesem Fremdfirmenmitarbeiter einen Koordinator und eine Fremdfirma zuordnen!");
			mMessages.Add(EXTEND_CWR_EXCO_INVAL, "Die Fremdfirma dieses Fremdfirmenmitarbeiters hat den Status 'Ungültig', Zutrittsverlängerung nicht möglich.");
			mMessages.Add(CANNOT_SEARCH_CHAR, "Sie haben ein ungültiges Zeichen angegeben (z.B \" ' \"). Bitte korrigieren Sie Ihre Eingabe.");
			mMessages.Add(DYNDATA_FROMTILLDATE, "Das Datum der Buchung 'von' muss vor dem Datum 'bis' liegen!");


            mMessages.Add(MSG_EMAIL_INACTIVE, "Der E-Mail Versand ist deaktiviert. Bitte wenden Sie sich bei Problemen an Ihren Systemadministrator.");
            mMessages.Add(MSG_EMAIL_SUCCESS, "Eine E-Mail mit dem Betreff '{0}' wurde erfolgreich an {1} versendet."); 
			mMessages.Add(FATAL_CONFIG_ERROR, "Die Konfigurationsdatei ist ungültig oder konnte nicht gelesen werden. Die Anwendung wird beendet.");
			mMessages.Add(FATAL_DB_ERROR, "Fehler bei der Kommunikation mit der Datenbank.");
            mMessages.Add(INITIALIZATION_ERROR, "Fehler beim Initialisieren der Anwendung.");
		}	

		#endregion

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
            return message;
		}

		#endregion 
	}
}

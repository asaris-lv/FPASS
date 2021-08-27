using System;

namespace Degussa.FPASS.Reports.Util
{
	/// <summary>
	/// Holds attributes to tell us what type of report and what its name is
	/// </summary>
	/// <remarks>
	/// <para><b>History</b></para>
	/// <div class="tablediv">
	/// <table class="dtTABLE" cellspacing="0">
	///		<tr>
	///			<th width="20%">N. Mundy, PTA GmbH</th>
	///			<th width="20%">07/04/2008</th>
	///			<th width="60%">Remarks</th>
	///		</tr>
	/// </table>
	/// </div>
	/// </remarks>
	public sealed class ReportNames
	{		
		// Defines Report names
		internal const string EMPTY = "";
		internal const string CHECKLIST = "Bestandsvergleichsliste (FPASS vs. ZKS)";
		internal const string PLANTS = "Betriebe mit Meistern";
		internal const string EXCO_COORDINATOR = "Fremdfirma mit zuständigen Koordinatoren";
		internal const string CWR_ALL_DATA = "Fremdfirmenmitarbeiter mit allen Daten";
		internal const string CWR_BOOKINGS = "Fremdfirmenmitarbeiter mit Bewegungen";
		internal const string EXCO_BOOKINGS_SUM = "Fremdfirmenmitarbeiter mit Bewegungen (Summe)";
		internal const string CWR_BOOKINGS_EXCO = "FFMA mit Bewegungen (Fremdfirma / Zeitraum)";
		internal const string CWR_ATTEND_DETAIL = "Anwesenheitszeiten FFMA Detail";
		internal const string CWR_ATTENDANCE = "Anwesenheitszeiten FFMA";
		internal const string EXCO_ATTENDANCE = "Leistungsverrechnung FF";
        internal const string CWR_ATTENDANCE_PERSNO = "Anwesenheitszeiten FFMA Doppelte Persnr";
	    internal const string CWR_EXPIRYDATE = "FFMA mit Ablaufdatum";
		internal const string CWR_NO_BOOKING = "FFMA mit gültigem Zutritt lang – ohne Buchung im ZKS seit x Tagen";
		internal const string CWR_DELETELIST = "FFMA Löschliste";
		internal const string CWR_PASS = "Passierschein";
		internal const string CWR_CHANGEHIST = "Änderungshistorie FFMA";
		internal const string RESPMASKS = "Atemschutzmasken";
		// Used in FrmCoWorker
		internal const string RESPMASK_RETURNED_TICKET = "Maskenrueckgabebeleg";
		
		// Defines what search results come out
		internal enum ResultTypes
		{
			PLANT = 2,
			EXCONTRACTOR = 4,
			COWORKER = 8,
			CHECKLIST = 16,
			EXCOBOOKING = 32,
			ATTENDANCE = 64,
			RESPMASK = 128
		}
	}
}

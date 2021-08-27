using System;

namespace Degussa.FPASS.Reports.Util
{
	/// <summary>
	/// Stores report and export filenames.
	/// </summary>
	/// <remarks>
	/// <para><b>History</b></para>
	/// <div class="tablediv">
	/// <table class="dtTABLE" cellspacing="0">
	///		<tr>
	///			<th width="20%">N. Mundy, PTA GmbH</th>
	///			<th width="20%">10/04/2008</th>
	///			<th width="60%">Remarks</th>
	///		</tr>
	/// </table>
	/// </div>
	/// </remarks>
	public sealed class ReportFilenames
	{
		// Defines Report filenames
		internal const string CHECKLIST = "BestandsvergleichFpassZks";
		internal const string PLANTS = "BetriebeMitMeistern";
		internal const string EXCO_COORDINATOR = "FremdfirmenMitKoordinatoren";
		internal const string CWR_ALL_DATA = "FFMAAlleDaten";
		internal const string CWR_BOOKINGS = "FFMAMitBewegungen";
		internal const string EXCO_BOOKINGS_SUM = "FFMAMitBewegungenSumme";
		internal const string CWR_BOOKINGS_EXCO = "FFMAMitBewegungenFremdfirma";
		internal const string CWR_ATTEND_DETAIL = "FFMAAnwesenheitszeitenDetail";
		internal const string CWR_ATTENDANCE = "FFMAAnwesenheitszeiten";
		internal const string EXCO_ATTENDANCE = "LeistungsverrechnungFF";
        internal const string CWR_ATTENDANCE_PERSNO = "AnwesenheitszeitenDoppeltePersnr";
		internal const string CWR_EXPIRYDATE = "FFMANachAblaufdatum";
		internal const string CWR_NO_BOOKING = "FFMABuchungSeit";
		internal const string CWR_DELETELIST = "FFMALoeschliste";
		internal const string CWR_PASS = "Passierschein";
		internal const string CWR_CHANGEHIST = "FFMAAenderungshistorie";
		internal const string RESPMASKS = "Atemschutzmasken";
		// Used in FrmCoWorker
		internal const string RESPMASK_RETURNED_TICKET = "Maskenrueckgabebeleg";
	}
}

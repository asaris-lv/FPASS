using System;

namespace Degussa.FPASS.Util.Validation
{
	/// <summary>
	/// Summary description for ValidateAccessAuthorization.
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
	public class ValidateAccessAuthorization
	{
		#region Members
	
		//gültig von Passierschein
		private string mTxtPassValidFrom;
		
		//gültig bis passierschein
		private string mTxtPassValidUntil;

		//Belehrung as werk durch koordinator
		private string mCbxCoPlBriefingDone;

		//lichtbildausweis
		private string mCbxSiSeIdentityCardRecieved;

		//vorsorgeuntersuchung gültigkeitsdatum
		private string mDatSiMedValidUntil;

		//unterweisung atemschutzmaske
		private string mCbxSiFiRespiratoryMaskBriefingDone;

		//atemschutzmaske erhalten
		private string mCbxSiFiRespiratoryMaskReceived;

		//belehrung atemschutzgeräteträger g26.2
		private string mCbxSiFiSiteSecurityBriefingDoneG26_2;

		//belehrung atemschutzgeräteträger g26.3
		private string mCbxSiFiSiteSecurityBriefingDoneG26_3;

		//rückgabedatum atemschutzmaske
		private string mDatSiFiRespiratoryMaskDeliveredOn;

		//auftrag erledigt
		private string mRbtCoOrderDoneYes;

		//abmeldedatum
		private string mDatCoCheckOff;

		//zutrittsberechtigung empfang
		private string mRbtReAccessAuthorizationNo;

		//Zutrittsarten, alle nur für werkszutritt ZU FUSS
		private string mEntryShort;
		private string mEntryMiddle;
		private string mEntryLong;


		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public ValidateAccessAuthorization()
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

		internal void GenerateAccessAuthorization()
		{

		}

		#endregion // End of Methods


	}
}

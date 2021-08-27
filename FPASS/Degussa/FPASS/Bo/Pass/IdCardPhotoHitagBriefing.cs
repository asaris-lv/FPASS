using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

using de.pta.Component.DbAccess;
using de.pta.Component.Errorhandling;

using Degussa.FPASS.Util;
using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.Util.Exceptions;
using Degussa.FPASS.Util.UserManagement;
using Degussa.FPASS.Db;
using Degussa.FPASS.FPASSApplication;

using Degussa.FPASS.Gui.Dialog.Pass;

namespace Degussa.FPASS.Bo.Pass
{
	/// <summary>
	/// Represents ordering and receiving a photo identity card (Lichtbildausweis). It's not a really briefing in but it can
	/// be handled as briefing and in fpass application and in the db. A coworker can get a photo idcard,
	/// if it was directed by his coordinator. The id card is given to him by site security.
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
    ///			<td width="20%">PTA</td>
    ///			<td width="20%">12.01.2003</td>
    ///			<td width="60%">Created</td>
    ///		</tr>
    /// </table>
	/// </div>
	/// </remarks>
	public class IdCardPhotoHitagBriefing : AbstractBriefing
	{
		#region Members

		#endregion 

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public IdCardPhotoHitagBriefing(CoWorkerModel pCoWorkerModel) : base (pCoWorkerModel)
		{
			initialize();
		}

		#endregion 

		#region Initialization

		/// <summary>
		/// Initializes the members.
		/// </summary>
		private void initialize()
		{
            mBriefingTypeID = Globals.GetInstance().IdCardPhotoHitagID;
		}	

		#endregion

		#region Methods 

		/// <summary>
        ///  Copies data from BO into GUI. Site security (Werkschutz) tab. 
        ///  This is the older id card with photo and Hitag2 chip.
        ///  Coordinator can no longer specify this type of id card, so showing legacy information.
		/// </summary>
        internal override void CopyIn()
        {
            // If briefing has been revoked, reset date in GUI
            if (!mReceived)
            {
                mBriefingDate = DateTime.Now;
            }

            // Lichtbildausweis Hitag: when received and who did it? 
            mViewCoWorker.DatSiSeIdPhotoHitagRec.Value = mBriefingDate;
            mViewCoWorker.TxtSiSeIdPhotoHitagRecBy.Text = mUserName;
            mViewCoWorker.CbxSiSeIdPhotoHitagRec.Checked = mReceived;

            mCoWorkerModel.IdPhotoHitagDirected = mDirected;

            mChanged = false;
        }

		/// <summary>
        /// Copies data out of Site security (Werkschutz) tab into this BO.
		/// Saves whether or not id card is received Y/N and date of receipt.
        /// This is older Hitag2 id card with photo so not relevant for SmartAct.
		/// </summary>
		internal override void CopyOut() 
		{
			int dateCompared = mCoWorkerModel.CompareDates(mBriefingDate, mViewCoWorker.DatSiSeIdPhotoHitagRec.Value);

			if (dateCompared != 0)
			{
				mChanged = true;
				mBriefingDate = mViewCoWorker.DatSiSeIdPhotoHitagRec.Value;
				SetWhoChangedMeReceived();
			} 

			if (!mViewCoWorker.TxtSiSeIdPhotoHitagRecBy.Text.Equals(mUserName)) 
			{
				mChanged = true;
			} 

			// Only save user who changed status of received Y/N
			if (mViewCoWorker.CbxSiSeIdPhotoHitagRec.Checked != mReceived) 
			{
				mChanged = true;
				mReceived = mViewCoWorker.CbxSiSeIdPhotoHitagRec.Checked;
				SetWhoChangedMeReceived();
			}	   
		}

		#endregion 
	}
}

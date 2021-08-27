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
	/// Represents ordering and receiving a photo identity card (Lichtbildausweis) from SmartAct. It's not a really briefing in but it can
    /// be handled as briefing and in fpass application and in the db. A coworker can get a photo id card, directed by his coordinator, this is then printed in SmartAct.
	/// The id card is given to him by site security.
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
    ///			<td width="20%">N. Mundy</td>
    ///			<td width="20%">25.04.2016</td>
    ///			<td width="60%">Created</td>
    ///		</tr>
    /// </table>
	/// </div>
	/// </remarks>
	public class IdCardPhotoSmActBriefing : AbstractBriefing
	{
		#region Members    
		#endregion

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
        public IdCardPhotoSmActBriefing(CoWorkerModel pCoWorkerModel)
            : base(pCoWorkerModel)
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
            mBriefingTypeID = Globals.GetInstance().IdCardPhotoSmActID;
		}	

		#endregion 

		#region Methods 

		/// <summary>
		/// Copies data from BO into GUI. Coordinator and site security (Werkschutz) tabs. 
		/// </summary>
		internal override void CopyIn() 
		{
            // If briefing has been revoked, reset date in GUI
            if (!mReceived)
            {
                this.mBriefingDate = DateTime.Now;
            }		
	
            // Received (Erhalten) is on site security tab (Werkschutz)
            // Received on what date and by who.
            mViewCoWorker.CbxSiSeIdPhotoSmActRec.Checked = mReceived;
            mViewCoWorker.DatSiSeIdPhotoSmActRec.Value = mBriefingDate;
            mViewCoWorker.TxtSiSeIdPhotoSmActRecBy.Text = mUserName;
            
            mChanged = false;

            // Directed (angeordnet/gewünscht) is on Coordinator tab.
            mViewCoWorker.RbtCoIdPhotoSmActYes.Checked = mDirected;
            mCoWorkerModel.IdPhotoSmActDirected = mDirected;
		}

		/// <summary>
        /// Copies data out of Coordinator and Site security (Werkschutz) tabs into this BO.
        /// Saves whether or not id card is also directed (Coordinator: gewünscht Y/N),
        /// and received Y/N and date of receipt and who haned out id card.
        /// Method checks for changes that are relevant for SmartAct as this is the id card with photo from SmartAct.
		/// </summary>
		internal override void CopyOut() 
		{
            int dateCompared = mCoWorkerModel.CompareDates(mBriefingDate, mViewCoWorker.DatSiSeIdPhotoSmActRec.Value);

            if (dateCompared != 0)
            {
                mChanged = true;
                mBriefingDate = mViewCoWorker.DatSiSeIdPhotoSmActRec.Value;
                SetWhoChangedMeReceived();
            }

            if (!mViewCoWorker.TxtSiSeIdPhotoSmActRecBy.Text.Equals(mUserName))
            {
                mChanged = true;
            } 

            // Only save user who changed status of received Y/N
            if (mViewCoWorker.CbxSiSeIdPhotoSmActRec.Checked != mReceived)
            {
                mChanged = true;
                mReceived = mViewCoWorker.CbxSiSeIdPhotoSmActRec.Checked;
                SetWhoChangedMeReceived();

                // Flag for changes in SmartAct
                if (mViewCoWorker.CbxSiSeIdPhotoSmActRec.Checked)
                {
                    mCoWorkerModel.ShouldExportSmartAct = true;
                    mCoWorkerModel.IdPhotoSmActDirected = true;
                }
            }

            // Coordinator tab: is there a change in directed (gewünscht Y/N)
            if (mViewCoWorker.RbtCoIdPhotoSmActYes.Checked != mDirected)
            {
                mChanged = true;
                mDirected = mViewCoWorker.RbtCoIdPhotoSmActYes.Checked;

                // Flag for changes in SmartAct
                if (mDirected)
                {
                    mCoWorkerModel.ShouldExportSmartAct = true; 
                }
                mCoWorkerModel.IdPhotoSmActDirected = mDirected;
            }       
		}

		#endregion 

	}
}

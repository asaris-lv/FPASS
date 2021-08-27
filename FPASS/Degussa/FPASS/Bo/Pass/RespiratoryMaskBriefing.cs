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
	/// Represents a Respiratory mask briefing (Atemschutzunterweisung)
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
	public class RespiratoryMaskBriefing : AbstractBriefing
	{
		#region Members

		#endregion Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public RespiratoryMaskBriefing(CoWorkerModel pCoWorkerModel) : base (pCoWorkerModel)
		{
			initialize();
		}

		#endregion Constructors

		#region Initialization

		/// <summary>
		/// Initializes the members.
		/// </summary>
		private void initialize()
		{
			mBriefingTypeID = Globals.GetInstance().BriefRespiratoryMaskID;
		}	

		#endregion Initialization

		#region Accessors 

		#endregion Accessors

		#region Methods 

		/// <summary>
		/// Copies BO attributes into GUI tab page "site fire service"
		/// </summary>
		internal override void CopyIn() 
		{
			if (!mReceived)
			{
				mBriefingDate = DateTime.Now;
			}
            if (!mDirected)
            {
                mDirectedBriefingDate = DateTime.Now;
            }

            // Received
            mViewCoWorker.CbxSiFiRespMaskBriefRec.Checked = mReceived;
            mViewCoWorker.DatSiFiRespMaskBriefRecOn.Value = mBriefingDate;
            mViewCoWorker.TxtSiFiRespMaskBriefRecBy.Text = mUserName;

            // Directed: Tab Coordinator
			mViewCoWorker.RbtCoRespiratoryMaskBriefingYes.Checked = mDirected;
            // Directed: Tab "site fire service"
            mViewCoWorker.DatSiFiRespMaskBriefDirOn.Value = mDirectedBriefingDate;
            mViewCoWorker.TxtSiFiRespMaskBriefDirBy.Text = mDirectedUserName;
            
            mChanged = false;	
		}

		/// <summary>
		/// Copies GUI values into BO attributes.
        /// Saves Directed UserId and date if Directed Y/N or date of Directed changed
		/// Saves Received UserId and date if received Y/N or date of reception changed
		/// </summary>
		internal override void CopyOut() 
		{
            int dateCompared;

            // Save user who changed date received
			dateCompared = mCoWorkerModel.CompareDates(mBriefingDate, mViewCoWorker.DatSiFiRespMaskBriefRecOn.Value);
		
			if (dateCompared != 0) 
			{
				mChanged = true;
				mBriefingDate = mViewCoWorker.DatSiFiRespMaskBriefRecOn.Value;
				SetWhoChangedMeReceived();
			} 
            // Save user who changed status of received Y/N
			if (mViewCoWorker.CbxSiFiRespMaskBriefRec.Checked != mReceived) 
			{
				mChanged = true;
				mReceived = mViewCoWorker.CbxSiFiRespMaskBriefRec.Checked;
				SetWhoChangedMeReceived();
			}

			if (mViewCoWorker.TxtSiFiRespMaskBriefRecBy.Text != mUserName)
			{
				mChanged = true;
			}

            // Save user who changed status of Directed Y/N
			if (mViewCoWorker.RbtCoRespiratoryMaskBriefingYes.Checked != mDirected) 
			{
				mChanged = true;
				mDirected = mViewCoWorker.RbtCoRespiratoryMaskBriefingYes.Checked;
                SetWhoChangedMeDirected();
			}
		}

		#endregion Methods
	}
}

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
    /// Summary description for ApprenticeBriefing.
	/// New briefing in FPASS V5 for Apprentice Access to some date in the future
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
    ///			<td width="20%">19.12.2014</td>
	///			<td width="60%">Created</td>
	///		</tr>
	/// </table>
	/// </div>
	/// </remarks>
	public class ApprenticeBriefing : AbstractBriefing
	{
		#region Members
  
		#endregion 

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
        public ApprenticeBriefing(CoWorkerModel pCoWorkerModel)
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
			this.mBriefingTypeID = Globals.GetInstance().BriefApprentice;
		}	

		#endregion 

		
        #region Accessors 


		#endregion 

		#region Methods 


		/// <summary>
		/// Shows data of the bo in the gui in the tab page "Empfang"
		/// Data is "copied in the gui".
		/// </summary>
		internal override void CopyIn()
		{
            mViewCoWorker.RbtReAccessApprentYes.Checked = mDirected;
            mViewCoWorker.RbtReAccessApprentNo.Checked = !mDirected;            
            mViewCoWorker.TxtReAccessApprentBy.Text = mUserName;
            if (mDirected)
            {
                mViewCoWorker.DatReAccessApprent.Value = mBriefingDate;
            }
            else mViewCoWorker.DatReAccessApprent.Value = DateTime.Now;

			mChanged = false;			
		}


		/// <summary>
		/// Copies data out of GUI
        /// Note that mBriefingDate contains the requested "valid Until" date for apprentice access 
        /// This briefing is directed (angeordnet) and received (Erteilt) at the same time.
		/// </summary>
		internal override void CopyOut()
		{
            int compareDate = 0;

            // access model for apprentices (Auszubildende)
            if (mDirected != mViewCoWorker.RbtReAccessApprentYes.Checked)
            {
                mDirected = mViewCoWorker.RbtReAccessApprentYes.Checked;
                mReceived = mDirected;
                SetWhoChangedMeReceived();	
                mChanged = true;
            }

            if (!mViewCoWorker.TxtReAccessApprentBy.Text.Equals(this.mUserName))
            {
                mChanged = true;
            } 

            if (mDirected)
            {
                compareDate = mCoWorkerModel.CompareDates(mViewCoWorker.DatReAccessApprent.Value, mBriefingDate);
                if (compareDate != 0)
                {
                    mBriefingDate = mViewCoWorker.DatReAccessApprent.Value;
                    SetWhoChangedMeReceived();	
                    mChanged = true;
                }
            }
		}

		#endregion

	}
}

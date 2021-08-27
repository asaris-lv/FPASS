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
	/// Summary description for SafetyAtWork_Briefing.
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
	public class SafetyAtWork_Briefing : AbstractBriefing
	{
		#region Members

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public SafetyAtWork_Briefing(CoWorkerModel pCoWorkerModel) : base (pCoWorkerModel)
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
			this.mBriefingTypeID = Globals.GetInstance().BriefSafetyAtWorkID;
		}	

		#endregion //End of Initialization

		#region Accessors 

		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// Tab safety at work
		/// Last change 08.03.2004: If briefing revoked, reset date
		/// </summary>
		internal override void CopyIn() 
		{
			// New: If briefing revoked, reset date as NULL in DB
			if ( !mReceived )
			{
				this.mBriefingDate = DateTime.Now;
			}			
			mViewCoWorker.DatSaAtWoSafetyAtWorkBriefingDone.Value = this.mBriefingDate;
			mChanged = false;
			
			mViewCoWorker.TxtSaAtWoSafetyAtWorkBriefingDoneBy.Text = this.mUserName;
			mViewCoWorker.CbxSaAtWoSafetyAtWorkBriefingDone.Checked = this.mReceived;

			// register coordinator
			mViewCoWorker.RbtCoDepartmentalBriefingYes.Checked = this.mDirected;
		}

		/// <summary>
		/// Copies GUI values into BO
		/// 17.05.04: Save User if received Y/N or date of reception changed (no user or date for assignment)
		/// </summary>
		internal override void CopyOut() 
		{
			int dateCompared = mCoWorkerModel.CompareDates(this.mBriefingDate,
				mViewCoWorker.DatSaAtWoSafetyAtWorkBriefingDone.Value);

			if ( dateCompared != 0 ) 
			{
				mChanged = true;
				this.mBriefingDate = mViewCoWorker.DatSaAtWoSafetyAtWorkBriefingDone.Value;
				SetWhoChangedMeReceived();
			} 

			if ( ! mViewCoWorker.TxtSaAtWoSafetyAtWorkBriefingDoneBy.Text.
				Equals(this.mUserName) ) 
			{
				mChanged = true;
			} 

			/// 28.04.04: Only save user who changed status of received Y/N
			if ( mViewCoWorker.CbxSaAtWoSafetyAtWorkBriefingDone.Checked != 
				mReceived ) 
			{
				mChanged = true;
				this.mReceived = mViewCoWorker.CbxSaAtWoSafetyAtWorkBriefingDone.Checked;
				SetWhoChangedMeReceived();
			}

			if ( mViewCoWorker.RbtCoDepartmentalBriefingYes.Checked !=
				this.mDirected ) 
			{
				mChanged = true;
				this.mDirected = mViewCoWorker.RbtCoDepartmentalBriefingYes.Checked;
			}
		}

		#endregion // End of Methods

	}
}

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
	/// Represents a briefing for use of cranes.
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
	public class CranesBriefing : AbstractBriefing
	{
		#region Members

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public CranesBriefing(CoWorkerModel pCoWorkerModel) : base (pCoWorkerModel)
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
			this.mBriefingTypeID = Globals.GetInstance().BriefCranesID;
		}	

		#endregion //End of Initialization

		#region Accessors 

		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// Shows data of the bo in the gui in the tab page Safety at work service (Arbeitssicherheit). 
		/// Data is "copied in the gui".
		/// </summary>
		internal override void CopyIn() 
		{
			// register safety at work
			// New: If briefing revoked, reset date
			if ( !mReceived )
			{
				this.mBriefingDate = DateTime.Now;
			}			
			mViewCoWorker.DatSaAtWoCranesBriefingDoneOn.Value = this.mBriefingDate;
			mChanged = false;
		
			mViewCoWorker.TxtSaAtWoCranesBriefingDoneBy.Text = this.mUserName;
			mViewCoWorker.CbxSaAtWoCranesBriefingDone.Checked = this.mReceived;

			// register coordinator
			mViewCoWorker.RbtCoCranesYes.Checked = this.mDirected;
		}


		/// <summary>
		/// Data is "copied out of the gui" in tab page Safety at work service (Arbeitssicherheit). 
		/// 17.05.04: Save User if received Y/N or date of reception changed (no user or date for assignment)
		/// </summary>
		internal override void CopyOut() 
		{
			int dateCompared = mCoWorkerModel.CompareDates(this.mBriefingDate,
				mViewCoWorker.DatSaAtWoCranesBriefingDoneOn.Value);

			mCoWorkerModel.CompareDates(this.mBriefingDate, mViewCoWorker.DatSaAtWoCranesBriefingDoneOn.Value );

			if ( dateCompared != 0 ) 
			{
				mChanged = true;
				this.mBriefingDate = mViewCoWorker.DatSaAtWoCranesBriefingDoneOn.Value;
				SetWhoChangedMeReceived();	
			} 

			if ( ! mViewCoWorker.TxtSaAtWoCranesBriefingDoneBy.Text.
				Equals(this.mUserName) ) 
			{
				mChanged = true;
			} 

			/// 28.04.04: Only save user who changed status of received Y/N
			if ( mViewCoWorker.CbxSaAtWoCranesBriefingDone.Checked != 
				mReceived ) 
			{
				mChanged = true;
				this.mReceived = mViewCoWorker.CbxSaAtWoCranesBriefingDone.Checked;
				SetWhoChangedMeReceived();
			}

			if ( mViewCoWorker.RbtCoCranesYes.Checked !=
				this.mDirected ) 
			{
				mChanged = true;
				this.mDirected = mViewCoWorker.RbtCoCranesYes.Checked;
			}
		}

		#endregion // End of Methods

	}
}

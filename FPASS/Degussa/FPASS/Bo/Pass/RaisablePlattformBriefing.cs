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
	/// Summary description for RaisablePlattformBriefing.
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
	public class RaisablePlattformBriefing : AbstractBriefing
	{
		#region Members

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public RaisablePlattformBriefing(CoWorkerModel pCoWorkerModel) : base (pCoWorkerModel)
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
			this.mBriefingTypeID = Globals.GetInstance().BriefRaisablePlatformID;
		}	

		#endregion //End of Initialization

		#region Accessors 

		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// register technical department
		/// Last change 08.03.2004
		/// If briefing was received, save ID of user who revokes it, set date to default 
		/// </summary>
		internal override void CopyIn() 
		{
			// New: If briefing revoked, reset date
			if ( !mReceived )
			{
				this.mBriefingDate = DateTime.Now;
			}			
			mViewCoWorker.DatTecBriefingDoneOn.Value = this.mBriefingDate;
			mChanged = false;
		
			mViewCoWorker.TxtTecBriefingDoneBy.Text = this.mUserName;
			mViewCoWorker.CbxTecRaisonalPlattform.Checked = this.mReceived;

			// register coordinator
			mViewCoWorker.RbtCoRaisablePlattformYes.Checked = this.mDirected;
			if ( this.mDirected ) 
			{
				if ( mViewCoWorker.mTechDepartmentAuthorization ) 
				{
					mViewCoWorker.PnlTabTechnical.Enabled = true;
				}
			}
		}

		/// <summary>
		/// Data is "copied out of the gui" 
		/// 17.05.04: Save User if received Y/N or date of reception changed (no user or date for assignment)
		/// </summary>
		internal override void CopyOut() 
		{
			int dateCompared = mCoWorkerModel.CompareDates(this.mBriefingDate,
				mViewCoWorker.DatTecBriefingDoneOn.Value);

			if ( dateCompared != 0 ) 
			{
				mChanged = true;
				this.mBriefingDate = mViewCoWorker.DatTecBriefingDoneOn.Value;
				SetWhoChangedMeReceived();
			} 

			if ( ! mViewCoWorker.TxtTecBriefingDoneBy.Text.
				Equals(this.mUserName) ) 
			{
				mChanged = true;
			} 

			/// 28.04.04: Only save user who changed status of received Y/N
			if ( mViewCoWorker.CbxTecRaisonalPlattform.Checked != 
				mReceived ) 
			{
				mChanged = true;
				this.mReceived = mViewCoWorker.CbxTecRaisonalPlattform.Checked;
				SetWhoChangedMeReceived();
			}

			if ( mViewCoWorker.RbtCoRaisablePlattformYes.Checked !=
				this.mDirected ) 
			{
				mChanged = true;
				this.mDirected = mViewCoWorker.RbtCoRaisablePlattformYes.Checked;
			}
		}

		#endregion // End of Methods

	}
}

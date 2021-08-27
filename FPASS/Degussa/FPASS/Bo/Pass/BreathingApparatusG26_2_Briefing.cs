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
	/// Represents a briefing for a breathings Apparatus G26_2.
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
	public class BreathingApparatusG26_2_Briefing : AbstractBriefing
	{
		#region Members

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public BreathingApparatusG26_2_Briefing(CoWorkerModel pCoWorkerModel) : base (pCoWorkerModel)
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
			this.mBriefingTypeID = Globals.GetInstance().BriefBreathingAppG26_2_ID;
		}	

		#endregion //End of Initialization

		#region Accessors 

		#endregion //End of Accessors

		#region Methods 


		/// <summary>
		/// Shows data of the bo in the gui in the register Site Fire Service. 
		/// Data is "copied in the gui".
		/// Last change 08.03.2004 
		/// Save ID of user who issues or revokes briefing, no date saved if revoked
		/// </summary>
		internal override void CopyIn() 
		{
			// register site fire service
			
			// New: If briefing revoked, reset date
			if ( !mReceived )
			{
				this.mBriefingDate = DateTime.Now;
			}			
			mViewCoWorker.DatSiFiSiteSecurityBriefingDoneOnG26_2.Value = this.mBriefingDate;
			mChanged = false;
					
			mViewCoWorker.TxtSiFiSiteSecurityBriefingDoneByG26_2.Text  = this.mUserName;
			mViewCoWorker.CbxSiFiSiteSecurityBriefingDoneG26_2.Checked = this.mReceived;

			// register coordinator
			mViewCoWorker.RbtCoBreathingApparatusYesG26_2.Checked = this.mDirected;
		}

		/// <summary>
		/// Data is "copied out of the gui" in register Site Fire Service into the bo.
		/// 17.05.04: Save User if received Y/N or date of reception changed (no user or date for assignment)
		/// </summary>
		internal override void CopyOut() 
		{
			int dateCompared = mCoWorkerModel.CompareDates(this.mBriefingDate,
				mViewCoWorker.DatSiFiSiteSecurityBriefingDoneOnG26_2.Value);

			if (  dateCompared != 0  ) 
			{
				mChanged = true;
				this.mBriefingDate = mViewCoWorker.DatSiFiSiteSecurityBriefingDoneOnG26_2.Value;
				SetWhoChangedMeReceived();				
			} 

			if ( ! mViewCoWorker.TxtSiFiSiteSecurityBriefingDoneByG26_2.Text.
				Equals(this.mUserName) ) 
			{
				mChanged = true;
			} 
	
			/// 28.04.04: Only save user who changed status of received Y/N
			if ( ! mViewCoWorker.CbxSiFiSiteSecurityBriefingDoneG26_2.Checked == 
				mReceived ) 
			{
				mChanged = true;
				this.mReceived = mViewCoWorker.CbxSiFiSiteSecurityBriefingDoneG26_2.Checked;
				SetWhoChangedMeReceived();
			}

			if ( ! mViewCoWorker.RbtCoBreathingApparatusYesG26_2.Checked ==
				this.mDirected ) 
			{
				mChanged = true;
				this.mDirected = mViewCoWorker.RbtCoBreathingApparatusYesG26_2.Checked;
			}
		}

		#endregion // End of Methods

	}
}

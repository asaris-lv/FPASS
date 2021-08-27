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

using Degussa.FPASS.Gui.Dialog.Pass;

namespace Degussa.FPASS.Bo.Pass
{
	/// <summary>
	/// A Coworker gets his safety authoriztaion when from reception. It's called
	/// "Sicherheitshinweis vom Empfang erhalten" in the gui on tab reception.
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
	public class SafetyAuhorization : AbstractAuthorization
	{
		#region Members

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public SafetyAuhorization(CoWorkerModel pCoWorkerModel) : base (pCoWorkerModel)
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
			this.mAuthorizationTypeID = Globals.GetInstance().SafetyAuthorizationID;
		}	

		#endregion //End of Initialization

		#region Accessors 

		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// Copy BO attributes into GUI
		/// Last change 08.03.2004: if Autho date is NULL in DB, set val in GUI to Now
		/// </summary>
		internal override void CopyIn() 
		{
			// If authorization revoked then reset date
			if (!mAuthorizationExecuted)
			{
				mAuthorizationDate = DateTime.Now;
			}		
	
			mViewCoWorker.DatReSafetyInstructionsOn.Value = mAuthorizationDate;
			mChanged = false;

			mViewCoWorker.TxtReSafetyInstructionsBy.Text = mUserName;

			mViewCoWorker.RbtReSafetyInstructionsYes.Checked = mAuthorizationExecuted;
		}

		/// <summary>
		/// Standard CopyOut method, data is "copied out of the gui" into the bo.
		/// 17.05.04: Save User if received Y/N or date of reception changed (no user or date for assignment)
		/// Last change 07.03.2005: 
		/// swap user OS name for nice name shown in GUI
		/// </summary>
		internal override void CopyOut() 
		{
			int dateCompared = mCoWorkerModel.CompareDates(this.mAuthorizationDate,
				mViewCoWorker.DatReSafetyInstructionsOn.Value);

			if ( dateCompared != 0 ) 
			{
				mChanged = true;
				this.mAuthorizationDate = mViewCoWorker.DatReSafetyInstructionsOn.Value;
				SetWhoChangedMeReceived();
			} 

			if ( ! mViewCoWorker.TxtReSafetyInstructionsBy.Text.Equals(this.mUserName) ) 
			{
				mChanged = true;
			} 

			if ( ! mViewCoWorker.RbtReSafetyInstructionsYes.Checked == mAuthorizationExecuted ) 
			{
				mChanged = true;
				this.mAuthorizationExecuted = mViewCoWorker.RbtReSafetyInstructionsYes.Checked;
				SetWhoChangedMeReceived();
			}
		}

		#endregion // End of Methods

	}
}

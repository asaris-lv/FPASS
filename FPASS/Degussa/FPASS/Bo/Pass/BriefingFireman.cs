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
	/// Summary description for BriefingFireman.
	/// New briefing 28.04.04 "Brandsicherheitsposten"
	/// </summary>
	/// <remarks>
	/// <para><b>History</b></para>
	/// <div class="tablediv">
	/// <table class="dtTABLE" cellspacing="0">
	///		<tr>
	///			<th width="20%">Author</th>
	///			<th width="20%">New 28.04.04</th>
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
	public class BriefingFireman : AbstractBriefing
	{
		#region Members

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public BriefingFireman(CoWorkerModel pCoWorkerModel) : base (pCoWorkerModel)
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
			this.mBriefingTypeID = Globals.GetInstance().BriefFireman;
		}	

		#endregion //End of Initialization

		#region Accessors 

		#endregion //End of Accessors

		#region Methods 


		/// <summary>
		/// Shows data of the bo in the gui in the tab page "Fire service" (Werkfeuerwehr) 
		/// Data is "copied in the gui".
		/// </summary>
		internal override void CopyIn()
		{
			// If briefing revoked, reset date
			if ( !mReceived )
			{
				this.mBriefingDate = DateTime.Now;
			}
			
			mViewCoWorker.DatSiFiFiremanDoneOn.Value = mBriefingDate;
			mChanged = false;

			mViewCoWorker.TxtSiFiFiremanDoneBy.Text = this.mUserName;
			mViewCoWorker.CbxSiFiFireman.Checked    = this.mReceived;

			// Tab register Coordinator
			mViewCoWorker.RbtCoFiremanYes.Checked = this.mDirected;
		}


		/// <summary>
		/// Copies data out of GUI
		/// Only get Username if status "Received" /Erteilt or date is changed, only this USerID is of interest
		/// </summary>
		internal override void CopyOut()
		{
			
			int dateCompared = mCoWorkerModel.CompareDates(this.mBriefingDate,
				mViewCoWorker.DatSiFiFiremanDoneOn.Value);

			if ( dateCompared != 0 ) 
			{
				mChanged = true;
				this.mBriefingDate = mViewCoWorker.DatSiFiFiremanDoneOn.Value;
				SetWhoChangedMeReceived();	
			} 

			if ( ! mViewCoWorker.TxtSiFiFiremanDoneBy.Text.Equals(this.mUserName) ) 
			{
				mChanged = true;
			} 

			/// Only save user who changed status of "received"
			if ( mViewCoWorker.CbxSiFiFireman.Checked != mReceived ) 
			{
				mChanged = true;
				this.mReceived = mViewCoWorker.CbxSiFiFireman.Checked;
				SetWhoChangedMeReceived();	
			}

			/// Checkbox "angeordnet" on tab Coordinator
			if ( mViewCoWorker.RbtCoFiremanYes.Checked !=
				this.mDirected ) 
			{
				mChanged = true;
				this.mDirected = mViewCoWorker.RbtCoFiremanYes.Checked;
			}
		}



		#endregion // End of Methods


	}
}

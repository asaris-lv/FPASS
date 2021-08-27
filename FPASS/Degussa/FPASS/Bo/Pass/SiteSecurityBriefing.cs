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
	/// Summary description for SiteSecurityBriefing.
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
	public class SiteSecurityBriefing : AbstractBriefing
	{
		#region Members

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public SiteSecurityBriefing(CoWorkerModel pCoWorkerModel) : base (pCoWorkerModel)
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
			this.mBriefingTypeID = Globals.GetInstance().BriefSiteSecurityID;
		}	

		#endregion //End of Initialization

		#region Accessors 

		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// register site security
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
			mViewCoWorker.DatSiSeSiteSecBriRec.Value = this.mBriefingDate;
			mChanged = false;

			mViewCoWorker.TxtSiSeSiteSecBriRecBy.Text = this.mUserName;
			mViewCoWorker.CbxSiSeSiteSecBriRec.Checked = this.mReceived;

			// register coordinator
			mViewCoWorker.RbtCoSiteSecurityBriefingYes.Checked = this.mDirected;
		}

		/// <summary>
		/// Data is "copied out of the gui" in tab page safety at work service (Arbeitssicherheit) 
		/// 17.05.04: Save User if received Y/N or date of reception changed (no user or date for assignment)
		/// </summary>
		internal override void CopyOut() 
		{
			int dateCompared = mCoWorkerModel.CompareDates(this.mBriefingDate,
				mViewCoWorker.DatSiSeSiteSecBriRec.Value);
			if ( dateCompared != 0  ) 
			{
				mChanged = true;
				this.mBriefingDate = mViewCoWorker.DatSiSeSiteSecBriRec.Value;
				SetWhoChangedMeReceived();
			} 

			if ( ! mViewCoWorker.TxtSiSeSiteSecBriRecBy.Text.
				Equals(this.mUserName) ) 
			{
				mChanged = true;
			} 

			/// 28.04.04: Only save user who changed status of received Y/N
			if ( mViewCoWorker.CbxSiSeSiteSecBriRec.Checked != 
				mReceived ) 
			{
				mChanged = true;
				this.mReceived = mViewCoWorker.CbxSiSeSiteSecBriRec.Checked;
				SetWhoChangedMeReceived();
			}

			if ( mViewCoWorker.RbtCoSiteSecurityBriefingYes.Checked !=
				this.mDirected ) 
			{
				mChanged = true;
				this.mDirected = mViewCoWorker.RbtCoSiteSecurityBriefingYes.Checked;
			}
		}

		#endregion // End of Methods

	}
}

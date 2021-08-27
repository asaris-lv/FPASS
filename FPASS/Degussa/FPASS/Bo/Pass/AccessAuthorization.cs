using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

using de.pta.Component.DbAccess;
using de.pta.Component.Errorhandling;

using Degussa.FPASS.Db;
using Degussa.FPASS.FPASSApplication;
using Degussa.FPASS.Gui.Dialog.Pass;
using Degussa.FPASS.Util;
using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.Util.Exceptions;
using Degussa.FPASS.Util.UserManagement;


namespace Degussa.FPASS.Bo.Pass
{
	/// <summary>
	/// AccessAuthorization is shown in the gui as an "Zutrittsberechtigung" on the tabs reception ("Empfang")
	/// and site security "Werkschutz".  It can be granted by reception but it can be revoked by site security
	/// if the coworker misbehaved. 
	/// If it was revoked once it can only be granted again only by site security.
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
	public class AccessAuthorization : AbstractAuthorization
	{
		#region Members

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public AccessAuthorization(CoWorkerModel pCoWorkerModel) : base (pCoWorkerModel)
		{
			initialize();
		}

		#endregion //End of Constructors

		#region Initialization

		/// <summary>
		/// Initializes the members. 
		/// AuthorizationsType is set.
		/// </summary>
		private void initialize()
		{
			this.mAuthorizationTypeID = Globals.GetInstance().AccessAuthorID;
		}	

		#endregion //End of Initialization

		#region Accessors 

		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// Shows data of the bo in the gui. Data is "copied in the gui" in the tabs reception and site security. 
		/// If authorization was revoked once by site security user,  input (radio buttons) are disabled in tab reception
		/// </summary>
		internal override void CopyIn() 
		{
            // If authorization revoked then reset date
			if ( !mAuthorizationExecuted )
			{
				mAuthorizationDate = DateTime.Now;
			}			
			mViewCoWorker.DatReAccessAuthorizationOn.Value = mAuthorizationDate;
			mChanged = false;

            mViewCoWorker.RbtReAccessAuthYes.Checked = mAuthorizationExecuted;
            mViewCoWorker.TxtSiSeAccessAuthorizationComment.Text = mComment;

            // To get round the fact that the authorization is no longer deleted, just updated.
            // When not authorized then set username to empty
            if (!mAuthorizationExecuted)
                mUserName = string.Empty;

            mViewCoWorker.TxtReAccessAuthorizationBy.Text = mUserName;            

			SetAuthorization();
		}


		/// <summary>
		/// Standard CopyOut method, data is "copied out of the gui" into the bo.
		/// AccessAuthorization deals with reception auth (type 2) and SiteSecúrity auth (type 5)
		/// 27.02.2004: Changed logic: Only when coordinator grants access is the coworker valid and the expiry date (validUNTIL) set correctly 
		/// 08.03.2004: Save ID of user who issues or revokes briefing, no date saved if revoked
		/// 09.03.2004: Ensure correct authorization type is saved: 2 for Reception, 5 for Site Sec.
		/// 13.05.04: User Name was missing
		/// Last change 07.03.2005: 
		/// swap user OS name for nice name shown in GUI
		/// </summary>
		internal override void CopyOut() 
		{
			int dateCompared = mCoWorkerModel.CompareDates(this.mAuthorizationDate,
				mViewCoWorker.DatReAccessAuthorizationOn.Value);
			
			if (dateCompared != 0) 
			{
				mAuthorizationDate = mViewCoWorker.DatReAccessAuthorizationOn.Value;
				mChanged = true;
			} 

			if (!mViewCoWorker.TxtReAccessAuthorizationBy.Text.Equals(mUserName)) 
			{
				mChanged = true;
			}

            // If Site Security (Werkschutz) removed this CWR's access then update the Authorization entry. 
            // (only Werkschutz kann remove access) then save SiteSec ID (5) to DB
            // If Reception has granted access then save ID 2 for Reception
            if (mViewCoWorker.RbtReAccessAuthYes.Checked != mAuthorizationExecuted)
            {
                mChanged = true;

                if (mViewCoWorker.RbtSiSeAccessAuthNo.Checked)
                {
                    mAuthorizationTypeID = Globals.GetInstance().AccessAuthorSiteSecurityID;
                    mAuthorizationDate = DateTime.Now;
                }
                else
                {
                    mAuthorizationTypeID = Globals.GetInstance().AccessAuthorID;
                }
                // GUI events ensure that radio button on Tab "Recpetion" is checked if radiobutton in Site Sec is changed
				mAuthorizationExecuted = mViewCoWorker.RbtReAccessAuthYes.Checked;
            }

			if (!mViewCoWorker.TxtSiSeAccessAuthorizationComment.Text.Equals(mComment)) 
			{
				mComment = mViewCoWorker.TxtSiSeAccessAuthorizationComment.Text;
				mChanged = true;
			}

			// Save ID of user who made last change
			// User Nice name shown in GUI
			if ( mChanged )
			{
				SetWhoChangedMeReceived();				
			}
		}


		/// <summary>
		/// Checks if this authorization was once revkoed by site security and then disables user input in tab reception
		/// </summary>
		private void SetAuthorization() 
		{
            // Access Auth = No is always disabled
            mViewCoWorker.RbtReAccessAuthNo.Enabled = false;
            mViewCoWorker.RbtSiSeAccessAuthNo.Enabled = false;

            mViewCoWorker.RbtReAccessAuthYes.Checked = mAuthorizationExecuted;
            mViewCoWorker.RbtSiSeAccessAuthYes.Checked = mAuthorizationExecuted;

            mViewCoWorker.DatReAccessAuthorizationOn.Enabled = mAuthorizationExecuted;

            // If Site security granted access then disable user input on Registration tab (Empfang)
            if (mAuthorizationTypeID.Equals(Globals.GetInstance().AccessAuthorSiteSecurityID))
            {
                mViewCoWorker.RbtReAccessAuthYes.Enabled = mAuthorizationExecuted;
            }
            else if (mAuthorizationTypeID.Equals(Globals.GetInstance().AccessAuthorID))
            {
                mViewCoWorker.RbtReAccessAuthYes.Enabled = !mAuthorizationExecuted;
            }

		}

		#endregion // End of Methods
	}
}

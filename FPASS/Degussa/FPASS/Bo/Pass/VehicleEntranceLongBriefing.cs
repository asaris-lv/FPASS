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
	/// Summary description for VehicleEntranceLongBriefing.
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
	public class VehicleEntranceLongBriefing : AbstractBriefing
	{
		#region Members

		/// <summary>
		/// 15.03.2004: did coordinator change Wish? Don't save userID
		/// </summary>
		private bool mDontSaveWishCoordID = false;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public VehicleEntranceLongBriefing(CoWorkerModel pCoWorkerModel) : base (pCoWorkerModel)
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
			this.mBriefingTypeID = Globals.GetInstance().BriefVehicleEntranceLongID;
		}	

		#endregion //End of Initialization

		#region Accessors 

		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// Copies BO attributes in to GUI// register site security
		/// Last change 08.03.2004:
		/// 3 status: granted, refused, not processed
		/// </summary>
		internal override void CopyIn() 
		{
			// Status "not processed" means no date & no user saved
			// else, veh entrance was refused, show refusal date in GUI
			if ( !mReceived && mBriefDateWasNull )
			{
				this.mBriefingDate = DateTime.Now;
				this.mUserName     = String.Empty;
			}
			// else, veh entrance was refused, show refusal date in GUI
			mViewCoWorker.DatSiSeVehicleEntranceLongReceivedOn.Value = this.mBriefingDate;
			mViewCoWorker.TxtSiSeVehicleEntranceLongReceivedBy.Text = this.mUserName;
			

			// Check the appropriate button if veh entrance was granted or refused
			// (presence of date means entry has been edited)
			if ( mReceived && !mBriefDateWasNull )
			{
				mViewCoWorker.RbtSiSeVehicleEntranceLongYes.Checked = true;
			}
			else if ( !mReceived && !mBriefDateWasNull )
			{
				mViewCoWorker.RbtSiSeVehicleEntranceLongNo.Checked = true;
			}
			
			// register coordinator
			mViewCoWorker.RbtCoVehicleEntranceLongYes.Checked = this.mDirected;
			mChanged = false;
		}

		/// <summary>
		/// Data is "copied out of the gui" in tab page "Werkschutz"
		/// 08.03.2004: Save ID of user who altered veh entrance: 3 status: granted, refused, "not processed"
		/// 15.03.2004: unless it was tzhe coordinator who changed the wish, in this case don't save coordinator-UserID
		/// </summary>
		internal override void CopyOut() 
		{
			int dateCompared = mCoWorkerModel.CompareDates(this.mBriefingDate,
				mViewCoWorker.DatSiSeVehicleEntranceLongReceivedOn.Value);

			/// 1st part: If thw Wish Y/N was changed (tab Coordinator), don't save UserID
			if ( ! mViewCoWorker.RbtCoVehicleEntranceLongYes.Checked ==
				this.mDirected ) 
			{
				mChanged			   = true;
				mDontSaveWishCoordID   = true;
				this.mDirected = mViewCoWorker.RbtCoVehicleEntranceLongYes.Checked;
			}
			
			/// Rest is on site security tab, if changed then must save userID of SiteSecurity user
			if ( dateCompared != 0  ) 
			{
				this.mBriefingDate = mViewCoWorker.DatSiSeVehicleEntranceLongReceivedOn.Value;
				mChanged			 = true;
				mDontSaveWishCoordID = false;
			} 

			if ( ! mViewCoWorker.TxtSiSeVehicleEntranceLongReceivedBy.Text.
				Equals(this.mUserName) ) 
			{
				this.mUserName = mViewCoWorker.TxtSiSeVehicleEntranceLongReceivedBy.Text;
				mChanged			 = true;
				mDontSaveWishCoordID = false;
			} 

			/// If veh access accepted
			if ( ! mViewCoWorker.RbtSiSeVehicleEntranceLongYes.Checked == 
				mReceived ) 
			{
				this.mReceived = mViewCoWorker.RbtSiSeVehicleEntranceLongYes.Checked;
				mChanged			 = true;
				mDontSaveWishCoordID = false;
			}

			/// If veh access was not processed but is now denied
			if ( mViewCoWorker.RbtSiSeVehicleEntranceLongNo.Checked && mBriefDateWasNull )
			{
				this.mReceived		 = false;
				mChanged			 = true;
				mDontSaveWishCoordID = false;
			}
			
			/// Save ID and nice name of user who made last change
			/// ID saved to database
			/// User nice name for display in GUI: this is different from the other briefings
			if ( mChanged )
			{	
				if ( !mDontSaveWishCoordID )
				{
					this.SetWhoChangedMeReceived();
					this.mBriefDateWasNull = false;	
				}
				mDontSaveWishCoordID = false;
			}	
		}

		/// <summary>
        /// Sets parameter values in DML statement.
		/// Overrides method in AbstractBriefing with same name as logic a little different:
		/// 3 status: granted, refused, not processed
		/// If directedYN = "N", no wish
		/// If directedYN = "Y" and receivedYN = "N" and mBriefingDate is NULL, veh entrance not processed
		/// If directedYN = "Y" and receivedYN = "Y" veh entrance is granted (date automatically set)
		/// If directedYN = "Y" and receivedYN = "N" and date is set, veh entrance was denied.
        /// Parameters LASTUSER_PARAM and TIMESTAMP_PARAM hold user who made last change and its timestamp 
        /// Parameters DIRECTEDUSERID_PARAM, DIRECTEDYN_PARAM and DIRECTEDBRIEFDATE_PARAM are uninteresting here
		/// </summary>
		/// <param name="pCommand"></param>
		protected override void SetParameters(IDbCommand pCommand) 
		{
			String directedYN = "N";
			String receivedYN = "Y";
			
			mProvider.SetParameter(pCommand, PK_PARAM, mBriefingID);
			mProvider.SetParameter(pCommand, CWRID_PARAM, mCoWorkerID);
			mProvider.SetParameter(pCommand, BRIEFTYPE_PARAM, mBriefingTypeID);
			mProvider.SetParameter(pCommand, USERID_PARAM, mUserID);
            mProvider.SetParameter(pCommand, DIRECTEDUSERID_PARAM, mDirectedUserID);

			mProvider.SetParameter( pCommand, LASTUSER_PARAM, UserManagementControl.getInstance().CurrentUserID );
            mProvider.SetParameter(pCommand, TIMESTAMP_PARAM, mTimeStamp);

			if ( mBriefDateWasNull) 
			{			
				mProvider.SetParameter(pCommand, BRIEFDATE_PARAM, DBNull.Value ); 
			}
			else if ( mChanged && !mBriefDateWasNull) 
			{
				mProvider.SetParameter(pCommand, BRIEFDATE_PARAM, mBriefingDate ); 	
			} 		
			
            // Status directed
            directedYN = (mDirected ? "Y" : "N");
            mProvider.SetParameter(pCommand, DIRECTEDYN_PARAM, directedYN);
            mProvider.SetParameter(pCommand, DIRECTEDBRIEFDATE_PARAM, DBNull.Value);
				
			if ( mReceived ) 
			{
				receivedYN ="N";
			}
			mProvider.SetParameter(pCommand, RECEIVEDYN_PARAM, receivedYN);
		}

		#endregion // End of Methods

	}
}

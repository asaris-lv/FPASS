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
	/// Summary description for VehicleEntranceShortBriefing.
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
	public class VehicleEntranceShortBriefing : AbstractBriefing
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
		public VehicleEntranceShortBriefing(CoWorkerModel pCoWorkerModel) : base (pCoWorkerModel)
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
			this.mBriefingTypeID = Globals.GetInstance().BriefVehicleEntranceShortID;
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
			mViewCoWorker.DatSiSeVehicleEntranceShortReceivedOn.Value = this.mBriefingDate;
			mViewCoWorker.TxtSiSeVehicleEntranceShortReceivedBy.Text  = this.mUserName;
			
			// Check the appropriate button if veh entrance was granted or refused
			// (presence of date means entry has been edited)
			if ( mReceived && !mBriefDateWasNull )
			{
				mViewCoWorker.RbtSiSeVehicleEntranceShortReceivedYes.Checked = true;
			}
			else if ( !mReceived && !mBriefDateWasNull )
			{
				mViewCoWorker.RbtSiSeVehicleEntranceShortReceivedNo.Checked = true;
			}
			
			// register coordinator
			mViewCoWorker.RbtCoVehicleEntranceShortYes.Checked = this.mDirected;
			mChanged = false;
		}

		/// <summary>
		/// Data is "copied out of the gui" in tab page "Werkschutz"
		/// Save ID of user who altered veh entrance: 3 status: granted, refused, not processed
		/// 15.03.2004: unless it was the coordinator who changed the wish, in this case don't save coordinator-UserID
		/// </summary>
		internal override void CopyOut() 
		{
			int dateCompared = mCoWorkerModel.CompareDates(this.mBriefingDate,
				mViewCoWorker.DatSiSeVehicleEntranceShortReceivedOn.Value);

			/// 1st part: If thw Wish Y/N was changed (tab Coordinator), don't save UserID
			if ( ! mViewCoWorker.RbtCoVehicleEntranceShortYes.Checked ==
				this.mDirected ) 
			{
				mChanged			 = true;
				mDontSaveWishCoordID = true;
				this.mDirected = mViewCoWorker.RbtCoVehicleEntranceShortYes.Checked;
			}
			
			/// Rest is on site security tab, if changed then must save userID of SiteSecurity user
			if ( dateCompared != 0  ) 
			{
				this.mBriefingDate = mViewCoWorker.DatSiSeVehicleEntranceShortReceivedOn.Value;
				mChanged			 = true;
				mDontSaveWishCoordID = false;
			} 

			if ( ! mViewCoWorker.TxtSiSeVehicleEntranceShortReceivedBy.Text.
				Equals(this.mUserName) ) 
			{
				this.mUserName = mViewCoWorker.TxtSiSeVehicleEntranceShortReceivedBy.Text;
				mChanged			 = true;
				mDontSaveWishCoordID = false;
			} 

			/// If veh access accepted
			if ( ! mViewCoWorker.RbtSiSeVehicleEntranceShortReceivedYes.Checked == 
				mReceived ) 
			{				
				this.mReceived = mViewCoWorker.RbtSiSeVehicleEntranceShortReceivedYes.Checked;
				mChanged			 = true;
				mDontSaveWishCoordID = false;
			}

			/// If veh access was not processed but is now denied
			if ( mViewCoWorker.RbtSiSeVehicleEntranceShortReceivedNo.Checked &&	mBriefDateWasNull )
			{
				this.mReceived		 = false;
				mChanged			 = true;
				mDontSaveWishCoordID = false;
			}
		
			// Save ID and name of user who made last change
			// ID saved to database
			// Get UserName and for writing to GUI: this is different from the other briefings
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

			mProvider.SetParameter(pCommand, LASTUSER_PARAM, UserManagementControl.getInstance().CurrentUserID );
            mProvider.SetParameter(pCommand, TIMESTAMP_PARAM, mTimeStamp);

			if ( mBriefDateWasNull) 
			{			
				mProvider.SetParameter(pCommand, BRIEFDATE_PARAM, DBNull.Value ); 
			}
			else if ( mChanged && !mBriefDateWasNull) 
			{
				mProvider.SetParameter(pCommand, BRIEFDATE_PARAM, mBriefingDate ); 	
			} 
			
            //if ( mDirected ) 
            //{
            //    directedYN ="Y";
            //}
            //mProvider.SetParameter(pCommand, DIRECTEDYN_PARAM, directedYN);

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

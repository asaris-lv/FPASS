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
	/// Summary description for MaskReceived.
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
	public class MaskReceived : AbstractMask
	{
		#region Members
		
		// Used to get the latest date a mask was given back (05.05.2004)
		private  DateTime  mLastMaskDate;	
		private	 String	   FINDLATESTMASKDATE      = "SelectLatestMaskDate";
		private	 String	   MASK_CWRID_PARAM		   = ":REMA_CWR_ID";

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public MaskReceived(CoWorkerModel pCoWorkerModel) : base (pCoWorkerModel)
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
			this.mMaskTypeID = Globals.GetInstance().RespMaskIdLent;
		}	

		#endregion //End of Initialization

		#region Accessors 

		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// Fill GUI fields (tab "Werkfeuerwehr") for mask lent out (received)
		/// </summary>
		internal override void CopyIn() 
		{
			mViewCoWorker.DatSiFiMaskLentOnFlo.Value = this.mMaskDate;
			mViewCoWorker.TxtSiFiMaskLentByFlo.Text  = this.mUserName;
			mViewCoWorker.TxtSiFiMaskNrLentFlo.Text    = this.mMaskNo;
			// out 03.04: mask always has status "received"
			// mViewCoWorker.CbxSiFiRespiratoryMaskReceived.Checked = this.mMaskExecuted;
			SetAuthorization();
		}

		/// <summary>
		/// Last change 15.01.2004
		/// Disable GUI fields for mask received so no further masks can be lent out
		/// Mask delivered is always loaded after mask received and re-enables fields if req'd
		/// </summary>
		private void SetAuthorization() 
		{			
			// disable fields for entering masn nr if no briefing received
			// or a mask has already been lent
			if ( !mViewCoWorker.CbxSiFiRespMaskBriefRec.Checked || 
				mMaskNo.Length > 0 && mMaskExecuted )
			{
				mViewCoWorker.DatSiFiMaskLentOnFlo.Enabled = false;
				// out 03.04: mask always has status "received"
				///mViewCoWorker.CbxSiFiRespiratoryMaskReceived.Enabled   = false;
				mViewCoWorker.TxtSiFiMaskNrLentFlo.Enabled   = false;
			}
		}

		/// <summary>
		/// Get details for mask lent out (received) from GUI
		/// 09.03.2004: Mask always has status "received", save ID of user who made last change
		/// 03.05.2004: Don't save changes if mask briefing is not assigned (Tab Cord)
		/// </summary>
		internal override void CopyOut() 
		{
			int dateCompared = mCoWorkerModel.CompareDates(this.mMaskDate,
				mViewCoWorker.DatSiFiMaskLentOnFlo.Value);

			if ( dateCompared != 0  ) 
			{
				this.mMaskDate = mViewCoWorker.DatSiFiMaskLentOnFlo.Value;
				mChanged = true;
			} 

			if ( ! mViewCoWorker.TxtSiFiMaskLentByFlo.Text.
				Equals(this.mUserName) ) 
			{
				this.mUserName = mViewCoWorker.TxtSiFiMaskLentByFlo.Text;
				mChanged = true;
			} 

			this.mMaskExecuted = true;
			

			if ( ! mViewCoWorker.TxtSiFiMaskNrLentFlo.Text.Equals(
				this.MaskNo ) ) 
			{
				mChanged = true;
				this.mMaskNo = mViewCoWorker.TxtSiFiMaskNrLentFlo.Text.Trim();
			}
			/// Show in GUI User nice name wot made last change 
			/// Allow mask ticket to be printed (at least one mask now received)
			if ( mChanged )
			{
				this.mUserID   = UserManagementControl.getInstance().CurrentUserID;
				this.mUserName = UserManagementControl.getInstance().CurrentUserNiceName;
				mCoWorkerModel.NoMaskYet = false;
			}
		}

		/// <summary>
		/// Overrides method in AbstractMask
		/// Only validate in florix if a mask number entered in textbox
		/// If if tickbox checked and no mask number given, give warning.
		/// 04.03.2004: masks always has status received
		/// 05.05.04: Make sure coworker cannot lend out a mask before any entries in the past:
		/// new masks always chronological
		/// A mask can only be lent out if the mask briefing was received
		/// </summary>
		internal override void Validate()
		{			
			if ( mMaskNo.Length > 0 && mChanged )
			{
				if ( !mViewCoWorker.CbxSiFiRespMaskBriefRec.Checked )
				{
					mViewCoWorker.TxtSiFiMaskNrLentFlo.Text = String.Empty;
					mCoWorkerModel.ErrorMessages.Append( MessageSingleton.GetInstance().GetMessage(
						MessageSingleton.MASK_NO_BRIEFING) );

				}
				
				// Get latest date for mask and check GUI date is after this
				this.GetLastMaskActionDate();
				if ( this.mMaskDate < mLastMaskDate )
				{
					mCoWorkerModel.ErrorMessages.Append( MessageSingleton.GetInstance().GetMessage(
						MessageSingleton.MASK_IN_PAST) );
				}
				else
				{
					base.Validate();
				}
			}

//			else if (mMaskExecuted && mMaskNo.Length < 1 )
//			{
//				mCoWorkerModel.ErrorMessages.Append( MessageSingleton.GetInstance().
//					GetMessage(MessageSingleton.NO_MASKNR) );
//			}
		}

		/// <summary>
		/// First thing: if CWR already has 1 mask out, can't lend another one
		/// Interface with the FLORIX system: check if the given mask exists 
		/// query FLORIX with the given Masknumber (= barcode) and get the date of next maintainance
		/// (super class)
		/// If maintainance overdue or mask already out then it cannot be lent
		/// </summary>
		protected override void CheckMaskAvailable() 
		{
			// 03.05.04: First thing: if CWR already has 1 mask out, can't lend another one
			if ( CoWorkerAlreadyHasMaskReceived() )
			{
				mCoWorkerModel.ErrorMessages.Append( MessageSingleton.GetInstance().GetMessage(
					MessageSingleton.COWORKER_ALREADY_MASK) );
			}
			else
			{
				base.CheckMaskAvailable();

				// If a mask was found: if its maintainance date overdue or missing then it cannot be lent
				if ( this.mNumRecordsReturned > 0) 
				{				
					int ret = mMaskNextMaintDate.CompareTo(DateTime.Now);
					if ( 0 > ret || !this.mMaskHasMaintDate )
					{
						mCoWorkerModel.ErrorMessages.Append( MessageSingleton.GetInstance().
							GetMessage(MessageSingleton.FLO_MASK_MAINT_OVERDUE) );
					}
					else
					{
						// Check mask is not alreay lent
						if ( base.MaskIsAlreadyLent() )
						{
							mCoWorkerModel.ErrorMessages.Append( MessageSingleton.GetInstance().
								GetMessage(MessageSingleton.MASK_ALREADY_LENT) );
						}
					}
				}
			}
		}


		/// <summary>
		/// Makes sure masks are lent in chronological order
		/// Gets latest date of mask action (must actually be return as earlier checks to make sure
		/// CWR can only lend out 1 mask: select max from db
		/// If current date in GUI is earlier than this date, throw error
		/// </summary>
		/// <returns></returns>
		private void GetLastMaskActionDate()
		{
			mSelComm = null;		
			mLastMaskDate = Convert.ToDateTime("01.01.1900");
			
			try 
			{			
				mSelComm = mProvider.CreateCommand(FINDLATESTMASKDATE);		
				mProvider.SetParameter(mSelComm, MASK_CWRID_PARAM, mCoWorkerID);

				// Open data reader to get assignments ExContractor-mask, count how many of each
				IDataReader mDR = mProvider.GetReader(mSelComm);
				while (mDR.Read())
				{
					if ( !mDR["MAXMASKDATE"].Equals(DBNull.Value) )
					{
						mLastMaskDate = Convert.ToDateTime( mDR["MAXMASKDATE"] );
					}
				}
				mDR.Close();
			}	
			catch (System.Data.OracleClient.OracleException oraex)
			{
				throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(
					MessageSingleton.FATAL_DB_ERROR) 
					+ oraex.Message );
			}	
		}

		#endregion // End of Methods

	}
}

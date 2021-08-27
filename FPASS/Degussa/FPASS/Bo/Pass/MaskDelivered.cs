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
	/// Summary description for MaskDelivered.
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
	public class MaskDelivered : AbstractMask
	{
		#region Members

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public MaskDelivered(CoWorkerModel pCoWorkerModel) : base (pCoWorkerModel)
		{
			initialize();
		}

		#endregion //End of Constructors

		#region Initialization

		/// <summary>
		/// Initializes the members.
		/// The MaskTypeID comes from the DB table fpass_respmasktype, read in class "Globals"
		/// </summary>
		private void initialize()
		{
			this.mMaskTypeID = Globals.GetInstance().BriefRespiratoryMaskIDReturned;
		}	

		#endregion //End of Initialization

		#region Accessors 

		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// Fill GUI fields (tab "Werkfeuerwehr") for Mask returned (delivered)
		/// </summary>
		internal override void CopyIn() 
		{
			mViewCoWorker.DatSiFiMaskBackOnFlo.Value	= this.mMaskDate;
			mViewCoWorker.TxtSiFiMaskBackByFlo.Text	= this.mUserName;
			mViewCoWorker.TxtSiFiMaskNrBackFlo.Text		= this.mMaskNo;
			//mViewCoWorker.CbxSiFiRespiratoryMaskDelivered.Checked	= this.mMaskExecuted;						
			SetAuthorization();
		}

		/// <summary>
		/// If returned (delivered) mask is validated (checkbox ticked) and delivery date of mask is current date or earlier,
		/// re-enable "mask received" fields so that further masks can then be lent out
		/// </summary>
		private void SetAuthorization()
		{			
			if ( mMaskNo.Length > 0 && mMaskExecuted )
			{
				if ( 0 <= mCoWorkerModel.CompareDates(DateTime.Now, this.mMaskDate ) )
				{
					mViewCoWorker.DatSiFiMaskLentOnFlo.Enabled = true;
					//mViewCoWorker.CbxSiFiRespiratoryMaskReceived.Enabled   = true;
					mViewCoWorker.TxtSiFiMaskNrLentFlo.Enabled   = true;			
				}			
				else if ( 0 < mCoWorkerModel.CompareDates(this.mMaskDate, mViewCoWorker.DatSiFiMaskLentOnFlo.Value) )
				{
					mViewCoWorker.DatSiFiMaskLentOnFlo.Enabled = false;
					//mViewCoWorker.CbxSiFiRespiratoryMaskReceived.Enabled = false;
					mViewCoWorker.TxtSiFiMaskNrLentFlo.Enabled = false;
				}
			}
		}

		
		/// <summary>
		/// Get details for mask returned (delivered) from GUI
		/// Return is only valid if the checkbox is ticked
		/// 04.03.2004: Mask always has status "received", save ID of user who made last change
		/// 03.05.2004: Don't save changes if mask briefing is not assigned (Tab Cord)
		/// 23.02.2005: Button MaskTicket (Rückgabebeleg): print ticket to prove mask given back
		/// </summary>
		internal override void CopyOut() 
		{
			int dateCompared = mCoWorkerModel.CompareDates(this.mMaskDate,
				mViewCoWorker.DatSiFiMaskBackOnFlo.Value);

			if ( dateCompared != 0 ) 
			{
				this.mMaskDate = mViewCoWorker.DatSiFiMaskBackOnFlo.Value;
				mChanged = true;
			} 

			if ( ! mViewCoWorker.TxtSiFiMaskBackByFlo.Text.
				Equals(this.mUserName) ) 
			{
				this.mUserName = mViewCoWorker.TxtSiFiMaskBackByFlo.Text;
				mChanged = true;
			} 

			this.mMaskExecuted = true;
//			if ( ! mViewCoWorker.CbxSiFiRespiratoryMaskDelivered.Checked == 
//				mMaskExecuted ) 
//			{
//				mChanged = true;
//				this.mMaskExecuted = mViewCoWorker.CbxSiFiRespiratoryMaskDelivered.Checked;
//			}

			if ( ! mViewCoWorker.TxtSiFiMaskNrBackFlo.Text.
				Equals(this.mMaskNo) ) 
			{
				this.mMaskNo = mViewCoWorker.TxtSiFiMaskNrBackFlo.Text;
				mChanged = true;
			} 

			if ( ! mViewCoWorker.TxtSiFiMaskNrBackFlo.Text.Equals(
				this.MaskNo ) ) 
			{
				mChanged = true;
				this.mMaskNo = mViewCoWorker.TxtSiFiMaskNrBackFlo.Text;
			}

			
			if ( mChanged )
			{
				/// Get UserID wot made last change
				/// Show user nice name in GUI
				this.mUserID   = UserManagementControl.getInstance().CurrentUserID;
				this.mUserName = UserManagementControl.getInstance().CurrentUserNiceName;
				/// Bring up Acrobat Reader "print" dialog (MaskTicket)
				/// when changes are saved
				mCoWorkerModel.PromptMaskTicket   = true;
				mCoWorkerModel.NoMaskYet = false;
			}

		}	


		/// <summary>
		/// Validate user input for the return (delivery) of a respmask
		/// If checkbox ticked but no number given, generate error 04.03.2004: Mask always has status Received
		/// If delivery date earlier than date received, generate error 
		/// If delivered mask number different from mask nuumber received, generate error
		/// 03.05.04: If mask has already been given back (by another session of FPASS), generate error
		/// </summary>
		internal override void Validate()
		{
			if ( mMaskNo.Length > 0 )
			{
				if ( mMaskExecuted 
					&& mViewCoWorker.DatSiFiMaskBackOnFlo.Value.Date < mViewCoWorker.DatSiFiMaskLentOnFlo.Value.Date )
				{
					mCoWorkerModel.ErrorMessages.Append( MessageSingleton.GetInstance().
						GetMessage(MessageSingleton.MASK_WRONG_DELIV_DATE ) );
				}
				else if ( mMaskExecuted
					&& ! mViewCoWorker.TxtSiFiMaskNrLentFlo.Text.Trim().Equals(mViewCoWorker.TxtSiFiMaskNrBackFlo.Text.Trim()) )
				{
					mCoWorkerModel.ErrorMessages.Append( MessageSingleton.GetInstance().
						GetMessage(MessageSingleton.MASK_REC_DELV_NOT_SAME ) );
				}
				else if ( mMaskExecuted && !base.CoWorkerAlreadyHasMaskReceived() )
				{
					mCoWorkerModel.ErrorMessages.Append( MessageSingleton.GetInstance().
						GetMessage(MessageSingleton.MASK_RETURN_NOT_LENT ) );
				}
				else
				{
					base.Validate();
				}
			}
		}

		#endregion // End of Methods

	}
}

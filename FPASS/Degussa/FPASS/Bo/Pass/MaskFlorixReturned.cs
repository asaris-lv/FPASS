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
using Degussa.FPASS.Bo.Administration;

using Degussa.FPASS.Gui.Dialog.Pass;

namespace Degussa.FPASS.Bo.Pass
{
	/// <summary>
    ///  Business object for respiratory mask being returned, mask comes from Florix
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
	public class MaskFlorixReturned : AbstractMask
	{
		#region Members

        // Mask which was lent and should be returned here
        private BORespMask mRespMaskLentByCoworker;		
        
        #endregion Members
        

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
        public MaskFlorixReturned(CoWorkerModel pCoWorkerModel) : base(pCoWorkerModel)
		{
			initialize();
		}


                /// <summary>
        /// Constructor with BORespMask.
        /// </summary>
        public MaskFlorixReturned(CoWorkerModel pCoWorkerModel, BORespMask pRespMaskBO, BORespMask pRespMaskLentByCoworker)
            : base(pCoWorkerModel, pRespMaskBO)
        {
            mRespMaskLentByCoworker = pRespMaskLentByCoworker;
            initialize();

            //mMaskDate = (null != pRespMaskBO.MaskDate) ? Convert.ToDateTime(pRespMaskBO.MaskDate) : new DateTime(1900, 1, 1);
            //mMaskId = pRespMaskBO.MaskId;
            //mMaskNoLent = pRespMaskBO.MaskNo;
            //mNextMaintDate = pRespMaskBO.NextMaintDate;

        }

		#endregion 

		#region Initialization

		/// <summary>
		/// Initializes the members.
		/// The MaskTypeID comes from the DB table fpass_respmasktype, read in class "Globals"
		/// </summary>
		private void initialize()
		{
            base.initialize();
            base.initializeFromRespMaskBO();
            mMaskTypeReturnID = Globals.RespMaskIdReturnFlo;
            mMaskNoLent = mRespMaskLentByCoworker.MaskNo;
		}	

		#endregion 
	
		#region Methods 

		/// <summary>
		/// Fill GUI fields (tab "Werkfeuerwehr") for Mask returned (delivered)
		/// </summary>
        internal override void CopyIn()
        {
            if (!Globals.GetInstance().FlorixReturnActive)
            {
                mViewCoWorker.DatSiFiMaskBackOnFlo.Visible = false;
                mViewCoWorker.TxtSiFiMaskBackByFlo.Visible = false;
                mViewCoWorker.TxtSiFiMaskNrBackFlo.Visible = false;
            }
            else
            {
                mViewCoWorker.DatSiFiMaskBackOnFlo.Visible = true;
                mViewCoWorker.TxtSiFiMaskBackByFlo.Visible = true;
                mViewCoWorker.TxtSiFiMaskNrBackFlo.Visible = true;
                mViewCoWorker.LblSiFiMaskBackFlo.Visible = true;
                mViewCoWorker.LblSiFiMaskNrBackFlo.Visible = true;
                mViewCoWorker.LblSiFiMaskBackOnFlo.Visible = true;
                mViewCoWorker.LblSiFiMaskBackByFlo.Visible = true;
                
                mViewCoWorker.DatSiFiMaskBackOnFlo.Value = mMaskDate;
                mViewCoWorker.TxtSiFiMaskBackByFlo.Text = mUserName;
//                mViewCoWorker.TxtSiFiMaskNrBackFlo.Text = mMaskNoReturned;
                mViewCoWorker.TxtSiFiMaskNrBackFlo.Text = mMaskNoReturned;
                SetAuthorization();
            }
        }

		/// <summary>
		/// If returned (delivered) mask is validated (checkbox ticked) cwr still has a resp mask then enable fields to give this mask back.
		/// </summary>
        private void SetAuthorization()
        {
            // If Briefing has been receieved and CWR has a mask lent out then enable fields
            var canReturnMask = mViewCoWorker.CbxSiFiRespMaskBriefRec.Checked && mCoWorkerModel.HasMaskFlorix;

            mViewCoWorker.DatSiFiMaskBackOnFlo.Enabled = canReturnMask;
            mViewCoWorker.TxtSiFiMaskNrBackFlo.Enabled = canReturnMask;
        }
		
		/// <summary>
        /// Copies out fields concerning resp mask returned to CWR
		/// </summary>
		internal override void CopyOut() 
		{

            // Get values from screen into BO
            mRespMaskBO.MaskDate = mViewCoWorker.DatSiFiMaskBackOnFlo.Value;
            mRespMaskBO.MaskNo = mViewCoWorker.TxtSiFiMaskNrBackFlo.Text;

            if (mRespMaskBO.HasChanged)
            {
                mMaskDate = Convert.ToDateTime(mRespMaskBO.MaskDate);
                mMaskNoReturned = mRespMaskBO.MaskNo;
                mChanged = true;
            }
            
            
            //int dateCompared = mCoWorkerModel.CompareDates(mMaskDate, mViewCoWorker.DatSiFiMaskBackOnFlo.Value);

            //if (dateCompared != 0) 
            //{
            //    mMaskDate = mViewCoWorker.DatSiFiMaskBackOnFlo.Value;
            //    mChanged = true;
            //} 

            //if (!mViewCoWorker.TxtSiFiMaskBackByFlo.Text.Equals(mUserName)) 
            //{
            //    mUserName = mViewCoWorker.TxtSiFiMaskBackByFlo.Text;
            //    mChanged = true;
            //} 
 
            //if (!mViewCoWorker.TxtSiFiMaskNrBackFlo.Text.Equals(mMaskNoReturned)) 
            //{
            //    mMaskNoReturned = mViewCoWorker.TxtSiFiMaskNrBackFlo.Text;
            //    mChanged = true;
            //} 
			
			if (mChanged)
			{
				// Get UserID wot made last change and show user nice name in GUI
				mUserID = UserManagementControl.getInstance().CurrentUserID;
				mUserName = UserManagementControl.getInstance().CurrentUserNiceName;
				
                // Bring up Acrobat Reader "print" dialog (MaskTicket) when changes are saved
				mCoWorkerModel.PromptMaskTicket = true;
				
			}
		}	


		/// <summary>
		/// Validate user input for the return (delivery) of a respmask.
		/// </summary>
		internal override void Validate()
		{
			if ( mMaskNoReturned.Length > 0 )
			{
                mInsert = (mMaskId == 0);

				if (mViewCoWorker.DatSiFiMaskBackOnFlo.Value.Date < mViewCoWorker.DatSiFiMaskLentOnFlo.Value.Date )
				{
                    // Return date is before lent date
					mCoWorkerModel.ErrorMessages.Append(MessageSingleton.GetInstance().GetMessage(MessageSingleton.MASK_WRONG_RETURN_DATE));
				}
				else if (!mViewCoWorker.TxtSiFiMaskNrLentFlo.Text.Trim().Equals(mViewCoWorker.TxtSiFiMaskNrBackFlo.Text.Trim()))
				{
                    // Error if mask numbers are not the same
                    // TODO: does this have to be like this?
					mCoWorkerModel.ErrorMessages.Append( MessageSingleton.GetInstance().GetMessage(MessageSingleton.MASK_RETURN_NR_NOT_SAME));
				}
                else if (!mCoWorkerModel.HasMaskFlorix)
				{
                    // Error if CWR does not have a resp mask to give back
					mCoWorkerModel.ErrorMessages.Append( MessageSingleton.GetInstance().GetMessage(MessageSingleton.MASK_RETURN_NOT_LENT ) );
				}
				else
				{
					base.Validate();
				}
			}
		}

		#endregion 

	}
}

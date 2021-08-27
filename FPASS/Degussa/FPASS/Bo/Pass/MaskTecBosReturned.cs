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
    ///  Business object for respiratory mask being returned, mask comes from TecBos
    /// </summary>
    /// <remarks>
    /// <para><b>History</b></para>
    /// <div class="tablediv">
    /// <table class="dtTABLE" cellspacing="0">
    ///		<tr>
    ///			<th width="20%">PTA GmbH</th>
    ///			<th width="20%">09.05.2017</th>
    ///			<th width="60%">Remarks</th>
    ///		</tr>
    /// </table>
    /// </div>
    /// </remarks>
    public class MaskTecBosReturned : AbstractMask
    {
        #region Members

        // Mask which was lent and should be returned here
        private BORespMask mRespMaskLentByCoworker;

        #endregion

        #region Constructors

        /// <summary>
        /// Standard constructor.
        /// </summary>
        public MaskTecBosReturned(CoWorkerModel pCoWorkerModel) : base(pCoWorkerModel)
        {
            initialize();
        }


        /// <summary>
        /// Constructor with BORespMask.
        /// </summary>
        public MaskTecBosReturned(CoWorkerModel pCoWorkerModel, BORespMask pRespMaskBO, BORespMask pRespMaskLentByCoworker)
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
            mMaskTypeReturnID = Globals.RespMaskIdReturnTec;
            base.initializeFromRespMaskBO();
            mMaskNoLent = mRespMaskLentByCoworker.MaskNo;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Fill GUI fields (tab "Werkfeuerwehr") for Mask returned (delivered)
        /// </summary>
        internal override void CopyIn()
        {
            if (!Globals.GetInstance().TecBosReturnActive)
            {
                mViewCoWorker.DatSiFiMaskBackOnTec.Visible = false;
                mViewCoWorker.TxtSiFiMaskBackByTec.Visible = false;
                mViewCoWorker.TxtSiFiMaskNrBackTec.Visible = false;
            }
            else
            {
                // TecBos Mask Return
                mViewCoWorker.DatSiFiMaskBackOnTec.Visible = true;
                mViewCoWorker.TxtSiFiMaskBackByTec.Visible = true;
                mViewCoWorker.TxtSiFiMaskNrBackTec.Visible = true;
                mViewCoWorker.LblSiFiMaskBackTec.Visible = true;
                mViewCoWorker.LblSiFiMaskNrBackTec.Visible = true;
                mViewCoWorker.LblSiFiMaskBackOnTec.Visible = true;
                mViewCoWorker.LblSiFiMaskBackByTec.Visible = true;
                
                mViewCoWorker.DatSiFiMaskBackOnTec.Value = mMaskDate;
                mViewCoWorker.TxtSiFiMaskBackByTec.Text = mUserName;
                mViewCoWorker.TxtSiFiMaskNrBackTec.Text = mMaskNoReturned;
                SetAuthorization();
            }

            // show fields for TecBosLent if coworker has MaskTecBos even it TecBosLent ist deactivated
            if (!Globals.GetInstance().TecBosLentActive && mCoWorkerModel.HasMaskTecBos)
            {
                // TecBos Mask Lent
                mViewCoWorker.DatSiFiMaskLentOnTec.Visible = true;
                mViewCoWorker.TxtSiFiMaskLentByTec.Visible = true;
                mViewCoWorker.TxtSiFiMaskNrLentTec.Visible = true;
                mViewCoWorker.LblSiFiMaskLentTec.Visible = true;
                mViewCoWorker.LblSiFiMaskNrLentTec.Visible = true;
                mViewCoWorker.LblSiFiMaskMaintDateTec.Visible = true;
                mViewCoWorker.LblSiFiMaskLentOnTec.Visible = true;
                mViewCoWorker.LblSiFiMaskLentByTec.Visible = true;
                mViewCoWorker.TxtSiFiMaskMaintDateTec.Visible = true;

                mViewCoWorker.DatSiFiMaskLentOnTec.Value = mMaskDate;
                mViewCoWorker.TxtSiFiMaskLentByTec.Text = mUserName;
                mViewCoWorker.TxtSiFiMaskNrLentTec.Text = mMaskNoLent;
                mViewCoWorker.TxtSiFiMaskMaintDateTec.Text = mNextMaintDate.HasValue ? mNextMaintDate.Value.ToString().Substring(0, 10) : string.Empty;

                mViewCoWorker.TxtSiFiMaskNrLentTec.Enabled = false;

                SetAuthorization();
            }

        }

        /// <summary>
        /// If returned mask is validated (checkbox ticked) cwr still has a resp mask then enable fields to give this mask back.
        /// </summary>
        private void SetAuthorization()
        {            
            // If Briefing has been receieved and CWR has a mask lent out then enable fields
//            var canReturnMask = mViewCoWorker.CbxSiFiRespMaskBriefRec.Checked && mCoWorkerModel.HasMaskTecBos;
            var canReturnMask = mViewCoWorker.CbxSiFiRespMaskBriefRec.Checked && mRespMaskBO.IsReadOnly == false;
            
            mViewCoWorker.DatSiFiMaskBackOnTec.Enabled = canReturnMask;
            mViewCoWorker.TxtSiFiMaskNrBackTec.Enabled = canReturnMask;
        }

        /// <summary>
        /// Copies out fields concerning resp mask returned to CWR
        /// </summary>
        internal override void CopyOut()
        {

            // Get values from screen into BO
            mRespMaskBO.MaskDate = mViewCoWorker.DatSiFiMaskBackOnTec.Value;
            mRespMaskBO.MaskNo = mViewCoWorker.TxtSiFiMaskNrBackTec.Text;

            if (mRespMaskBO.HasChanged)
            {
                mMaskDate = Convert.ToDateTime(mRespMaskBO.MaskDate);
                mMaskNoReturned = mRespMaskBO.MaskNo;
                mChanged = true;

            }
            
            
            //int dateCompared = mCoWorkerModel.CompareDates(mMaskDate, mViewCoWorker.DatSiFiMaskBackOnTec.Value);

            //if (dateCompared != 0)
            //{
            //    mMaskDate = mViewCoWorker.DatSiFiMaskBackOnTec.Value;
            //    mChanged = true;
            //}
          
            //if (!mViewCoWorker.TxtSiFiMaskNrBackTec.Text.Equals(mMaskNoReturned))
            //{
            //    mMaskNoReturned = mViewCoWorker.TxtSiFiMaskNrBackTec.Text;
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
            if (mMaskNoReturned.Length > 0)
            {
                mInsert = (mMaskId == 0);

                if (mViewCoWorker.DatSiFiMaskBackOnTec.Value.Date < mViewCoWorker.DatSiFiMaskLentOnTec.Value.Date)
                {
                    // Return date is before lent date
                    mCoWorkerModel.ErrorMessages.Append(MessageSingleton.GetInstance().GetMessage(MessageSingleton.MASK_WRONG_RETURN_DATE));
                }
                else if (!mViewCoWorker.TxtSiFiMaskNrLentTec.Text.Trim().Equals(mViewCoWorker.TxtSiFiMaskNrBackTec.Text.Trim()))
                {
                    // Error if mask numbers are not the same
                    // TODO: does this have to be like this?
                    mCoWorkerModel.ErrorMessages.Append(MessageSingleton.GetInstance().GetMessage(MessageSingleton.MASK_RETURN_NR_NOT_SAME));
                }
                else if (!mCoWorkerModel.HasMaskTecBos)
                {
                    // Error if CWR does not have a resp mask to give back
                    mCoWorkerModel.ErrorMessages.Append(MessageSingleton.GetInstance().GetMessage(MessageSingleton.MASK_RETURN_NOT_LENT));
                }
                else if (mChanged)
                {
                    mCoWorkerModel.HasMaskTecBos = false; 
                    if (mInsert) InitializeNewBO();
                }
            }
        }

        #endregion
    }
}

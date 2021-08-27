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
using System.Drawing;

namespace Degussa.FPASS.Bo.Pass
{
    /// <summary>
    /// Business object for respiratory mask being lent to coworker.
    /// Masks come from TecBos
    /// </summary>
    /// <remarks>
    /// <para><b>History</b></para>
    /// <div class="tablediv">
    /// <table class="dtTABLE" cellspacing="0">
    ///		<tr>
    ///			<th width="20%">PTA GmbH</th>
    ///			<th width="20%">03.05.2017</th>
    ///			<th width="60%">Remarks</th>
    ///		</tr>
    /// </table>
    /// </div>
    /// </remarks>
    public class MaskTecBos : AbstractMask
    {
        #region Members
       
        // Validations for mask (florix & fpass)
        private string SEL_MASK_AVAILBL = "SelectMaskAvailable";
		private string PARA_MASK_BARCODE_TEC = ":GER_BARCODE";

        #endregion 

        #region Constructors

        /// <summary>
        /// Standard constructor.
        /// </summary>
        public MaskTecBos(CoWorkerModel pCoWorkerModel)  : base(pCoWorkerModel)
        {
            initialize();
        }


        public MaskTecBos(CoWorkerModel pCoWorkerModel, string pMaskSystem, bool pLentActive, bool pReturnActive)
            : base(pCoWorkerModel)
        {
            initialize();
        }

         /// Constructor with extra argument previous date a mask was lent 
        /// </summary>
        //public MaskTecBosLent(CoWorkerModel pCoWorkerModel, DateTime pPreviousDate) : base(pCoWorkerModel, pPreviousDate)
        //{
        //    initialize();
        //}

        #endregion

        #region Initialization

        /// <summary>
        /// Initializes the members.
        /// </summary>
        private void initialize()
        {
            mMaskTypeLentID = Globals.RespMaskIdLentTec;
            mMaskTypeReturnID = Globals.RespMaskIdReturnTec;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Fill GUI fields (tab "Brandschutz") for mask lent out from TecBos
        /// </summary>
        internal override void CopyIn()
        {
            if (!Globals.GetInstance().TecBosLentActive)
            {
                mViewCoWorker.DatSiFiMaskLentOnTec.Visible = false;
                mViewCoWorker.TxtSiFiMaskLentByTec.Visible = false;
                mViewCoWorker.TxtSiFiMaskNrLentTec.Visible = false;
                mViewCoWorker.TxtSiFiMaskMaintDateTec.Visible = false;
            }
            else
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

                ShowStatusNextMaint();
                SetAuthorization();
            }

            // Return
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
            }


        }

        /// <summary>
        /// Enables fields for lending resp mask.
        /// </summary>
        private void SetAuthorization()
        {
            // Enable fields only if these conditions met
            bool isLentEnabled = mViewCoWorker.CbxSiFiRespMaskBriefRec.Checked && !mCoWorkerModel.HasMaskTecBos && !mCoWorkerModel.HasMaskFlorix;

            // Reset read-only
            mViewCoWorker.TxtSiFiMaskNrLentTec.ReadOnly = false;

            mViewCoWorker.TxtSiFiMaskNrLentTec.Enabled = isLentEnabled;
            mViewCoWorker.DatSiFiMaskLentOnTec.Enabled = isLentEnabled;

            // For special case that mask is lent out: make field read-only
            if (!isLentEnabled && mViewCoWorker.CbxSiFiRespMaskBriefRec.Checked && mCoWorkerModel.HasMaskTecBos)
            {
                mViewCoWorker.TxtSiFiMaskNrLentTec.Enabled = true;
                mViewCoWorker.TxtSiFiMaskNrLentTec.ReadOnly = true;
            }

            // return
            // If Briefing has been receieved and CWR has a mask lent out then enable fields
            var canReturnMask = mViewCoWorker.CbxSiFiRespMaskBriefRec.Checked && mCoWorkerModel.HasMaskTecBos;

            mViewCoWorker.DatSiFiMaskBackOnTec.Enabled = canReturnMask;
            mViewCoWorker.TxtSiFiMaskNrBackTec.Enabled = canReturnMask;

        }

        /// <summary>
        /// Changes colour of field "Next maint date" to show when this has expired.
        /// </summary>
        private void ShowStatusNextMaint()
        {
            if (mNextMaintDate.HasValue)
            {
                double accessPara = Convert.ToDouble(Globals.GetInstance().AccessIndicator);

                // Green when more than x days until maintenance date expires
                if (mNextMaintDate.Value > DateTime.Now
                    && mNextMaintDate.Value > DateTime.Now.AddDays(accessPara))
                {
                    mViewCoWorker.TxtSiFiMaskMaintDateTec.BackColor = Color.FromArgb(0, 210, 0);
                    mViewCoWorker.TxtSiFiMaskMaintDateTec.ForeColor = Color.Black;
                }
                // Yellow when less than x days until maintenance date expires
                else if (mNextMaintDate.Value > DateTime.Now
                    && mNextMaintDate.Value <= DateTime.Now.AddDays(accessPara))
                {
                    mViewCoWorker.TxtSiFiMaskMaintDateTec.BackColor = Color.Yellow;
                    mViewCoWorker.TxtSiFiMaskMaintDateTec.ForeColor = Color.Black;
                }
                // Red: it's expired               
                else
                {
                    mViewCoWorker.TxtSiFiMaskMaintDateTec.BackColor = Color.Red;
                    mViewCoWorker.TxtSiFiMaskMaintDateTec.ForeColor = Color.White;
                }
            }
            else
            {
                // Rest to default
                mViewCoWorker.TxtSiFiMaskMaintDateTec.BackColor = SystemColors.Control;
                mViewCoWorker.TxtSiFiMaskMaintDateTec.ForeColor = Color.Black;
            }
        }


        /// <summary>
        /// Copies out fields concerning resp mask lent to CWR
        /// </summary>
        internal override void CopyOut()
        {
            int dateComparedMaskLentOnTec = mCoWorkerModel.CompareDates(mMaskDate, mViewCoWorker.DatSiFiMaskLentOnTec.Value);

            if (dateComparedMaskLentOnTec != 0)
            {
                // "Date lent on" Hashtable been changed
                mMaskDate = mViewCoWorker.DatSiFiMaskLentOnTec.Value;
                mChanged = true;
            }

            if (!mViewCoWorker.TxtSiFiMaskNrLentTec.Text.Equals(mMaskNoLent))
            {
                mChanged = true;
                mMaskNoLent = mViewCoWorker.TxtSiFiMaskNrLentTec.Text.Trim();
            }

            if (mChanged)
            {
                mUserID = UserManagementControl.getInstance().CurrentUserID;
                mUserName = UserManagementControl.getInstance().CurrentUserNiceName;
            }

            // return
            int dateComparedMaskBackOnTec = mCoWorkerModel.CompareDates(mMaskDate, mViewCoWorker.DatSiFiMaskBackOnTec.Value);

            if (dateComparedMaskBackOnTec != 0)
            {
                mMaskDate = mViewCoWorker.DatSiFiMaskBackOnTec.Value;
                mChanged = true;
            }

            if (!mViewCoWorker.TxtSiFiMaskNrBackTec.Text.Equals(mMaskNoReturned))
            {
                mMaskNoReturned = mViewCoWorker.TxtSiFiMaskNrBackTec.Text;
                mChanged = true;
            }

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
        /// Validates the current mask assignment (lending).
        /// If mask date in or it is not available then validation fails.
        /// </summary>
        internal override void Validate()
        {
            if (mMaskNoLent.Length > 0 && mChanged)
            {
                if (!mViewCoWorker.CbxSiFiRespMaskBriefRec.Checked)
                {
                    mViewCoWorker.TxtSiFiMaskNrLentTec.Text = String.Empty;
                    mCoWorkerModel.ErrorMessages.Append(MessageSingleton.GetInstance().GetMessage(MessageSingleton.MASK_NO_BRIEFING));
                }

                // Get latest date for mask and check GUI date is after this
                if (mPreviousMaskDate.HasValue && mPreviousMaskDate > mMaskDate)
                {
                    mCoWorkerModel.ErrorMessages.Append(MessageSingleton.GetInstance().GetMessage(MessageSingleton.MASK_IN_PAST));
                }
                else if (mChanged)
                {
                    // Check if mask available, if it is then everything is OK
                    if (CheckMaskAvailable())
                    {
                        mCoWorkerModel.HasMaskTecBos = true; 
                        if (mInsert) InitializeNewBO();
                    }
                }
            }

            // return
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

        /// <summary>
        /// Queries TecBos data to find out if given mask is available.
        /// If maintainance overdue or mask already lent out then it cannot be lent
        /// </summary>
        protected override bool CheckMaskAvailable()
        {
            if (mCoWorkerModel.HasMaskTecBos)
            {
                // CWR already has one mask lent out. Could be in Florix or TecBos
                mCoWorkerModel.ErrorMessages.Append(MessageSingleton.GetInstance().GetMessage(MessageSingleton.COWORKER_ALREADY_MASK));
                return false;
            }
            
            bool maintDateOK = false;
            bool isAlreadyLent = true;
            int numRecs = 0;

			// Query the respmask available DB View
			mSelComm = mProvider.CreateCommand(SEL_MASK_AVAILBL);
			mProvider.SetParameter(mSelComm, PARA_MASK_BARCODE_TEC, mMaskNoLent);

			// Open data reader: grouping Max in SQL means one row.
			IDataReader mDR = mProvider.GetReader(mSelComm);
			while (mDR.Read())
			{
                numRecs ++;

                // Get maintenance dates last one and next one.
                mNextMaintDate = mDR["GER_NEXT_MAINTAIN"].Equals(DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(mDR["GER_NEXT_MAINTAIN"]);

                // Check next maintenance date
                // Next maintainance date must be bigger than validFrom + AccessMiddle
                // (at this stage we don't know if CWR has short medium long access)
                // Read ValidFrom directly out of Form.
                var validFrom = mViewCoWorker.DatPassValidFrom.Value;
                maintDateOK = (mNextMaintDate.HasValue && mNextMaintDate.Value > validFrom.AddDays(Globals.GetInstance().AccessMiddle));
				
                // Is mask already lent?
                mPreviousMaskDate = mDR["REMA_DATE_LENT"].Equals(DBNull.Value) ? null : new Nullable<DateTime>(Convert.ToDateTime(mDR["REMA_DATE_LENT"]));
                var previousReturnDate = mDR["REMA_DATE_RET"].Equals(DBNull.Value) ? null : new Nullable<DateTime>(Convert.ToDateTime(mDR["REMA_DATE_RET"]));

                if (!mPreviousMaskDate.HasValue)
                {
                    isAlreadyLent = false;
                }
                else if (!previousReturnDate.HasValue || previousReturnDate.Value > DateTime.Now || mPreviousMaskDate.Value > previousReturnDate.Value)
                {
                    // Mask is still lent out because there is no return date, or latest return date is before latest lent date
                    isAlreadyLent = true;
                }
                else isAlreadyLent = false;
			}
			mDR.Close();


            if (numRecs == 0)
            {
                // If no barcode found then mask does not exist
                var errMsg = string.Format(MessageSingleton.GetInstance().GetMessage(MessageSingleton.MASKNR_NOT_FOUND), mMaskNoLent, "TecBos");
                mCoWorkerModel.ErrorMessages.Append(errMsg);
                return false;
            }
            if (!maintDateOK)
            {
                // If its maintainance date overdue or missing then it cannot be lent
                mCoWorkerModel.ErrorMessages.Append(MessageSingleton.GetInstance().GetMessage(MessageSingleton.MASK_MAINT_OVERDUE));
                return false;
            }
			if (isAlreadyLent)
			{
                // Mask is already lent.
				mCoWorkerModel.ErrorMessages.Append(MessageSingleton.GetInstance().GetMessage(MessageSingleton.MASK_ALREADY_LENT));
                return false;
			}

            // IF we have got to here then all is ok.
            return true;
        }

        #endregion

    }
}

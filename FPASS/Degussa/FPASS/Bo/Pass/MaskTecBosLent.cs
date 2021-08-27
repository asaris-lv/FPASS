using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

using de.pta.Component.DbAccess;
using de.pta.Component.DbAccess.Exceptions;
using de.pta.Component.Errorhandling;

using Degussa.FPASS.Util;
using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.Util.Exceptions;
using Degussa.FPASS.Util.UserManagement;
using Degussa.FPASS.Db;
using Degussa.FPASS.FPASSApplication;
using Degussa.FPASS.Bo.Administration;

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
    public class MaskTecBosLent : AbstractMask
    {
        #region Members
       
        // Validations for mask (florix & fpass)
        private string SEL_MASK_AVAILBL = "SelectMaskAvailable";
		private string PARA_MASK_BARCODE_TEC = ":GER_BARCODE";
        private string SELECT_MASK_TECBOS_NEXTMAINTDATE = "SelectMaskTecbosNextMaintDate";

        #endregion 

        #region Constructors

        /// <summary>
        /// Standard constructor.
        /// </summary>
        public MaskTecBosLent(CoWorkerModel pCoWorkerModel)  : base(pCoWorkerModel)
        {
            initialize();
        }


        
        /// <summary>
        /// Constructor with mask lent.
        /// </summary>
        public MaskTecBosLent(CoWorkerModel pCoWorkerModel, BORespMask pRespMaskBO) : base(pCoWorkerModel, pRespMaskBO)
        {
            initialize();

            //mMaskDate = (null != pRespMaskBO.MaskDate) ? Convert.ToDateTime(pRespMaskBO.MaskDate) : new DateTime(1900, 1, 1);
            //mMaskId = pRespMaskBO.MaskId;
            //mMaskNoLent = pRespMaskBO.MaskNo;
 

        }


        #endregion

        #region Initialization

        /// <summary>
        /// Initializes the members.
        /// </summary>
        private void initialize()
        {
            base.initialize();
            mMaskTypeLentID = Globals.RespMaskIdLentTec;

            base.initializeFromRespMaskBO();
            //after base intitialization
            mNextMaintDate = GetMaskTecbosNextMaintDate();
            mRespMaskBO.NextMaintDate = mNextMaintDate;
        }

        #endregion

        #region Methods


        /// <summary>
        /// Get maintenance date from database
        /// </summary>
        /// <returns></returns>
        private DateTime? GetMaskTecbosNextMaintDate()
        {
            DateTime? nextMaintDateFromTecbos = null;

            try
            {
                // Get right SQL command and set CWR id
                string comdName = SELECT_MASK_TECBOS_NEXTMAINTDATE;
                mSelComm = mProvider.CreateCommand(comdName);
                mProvider.SetParameter(mSelComm, PARA_MASK_BARCODE_TEC, mMaskNoLent);

                // Open data reader to get assignments coworker - mask lent, 
                IDataReader mDR = mProvider.GetReader(mSelComm);

                while (mDR.Read())
                {
                    if (mDR["NDATUM"].Equals(DBNull.Value))
                    {
                        nextMaintDateFromTecbos = null;
                    }
                    else
                    {
                        nextMaintDateFromTecbos = Convert.ToDateTime(mDR["NDATUM"]);
                    }
                    
                }
                mDR.Close();
            }
            catch (DbAccessException dba)
            {
                throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR) + dba.Message + " Objekt: " + this.ToString());
            }
            catch (System.Data.OracleClient.OracleException oraex)
            {
                throw new UIFatalException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR) + oraex.Message + " Objekt: " + this.ToString());
            }


            return nextMaintDateFromTecbos;
        }


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
        }

        /// <summary>
        /// Enables fields for lending resp mask.
        /// </summary>
        private void SetAuthorization()
        {
            // Enable fields only if these conditions met
//            bool isLentEnabled = mViewCoWorker.CbxSiFiRespMaskBriefRec.Checked && !mCoWorkerModel.HasMaskTecBos && !mCoWorkerModel.HasMaskFlorix;
            bool isLentEnabled = mViewCoWorker.CbxSiFiRespMaskBriefRec.Checked && mRespMaskBO.IsReadOnly == false;


            // Reset read-only
            mViewCoWorker.TxtSiFiMaskNrLentTec.ReadOnly = false;

            mViewCoWorker.TxtSiFiMaskNrLentTec.Enabled = isLentEnabled;
            mViewCoWorker.DatSiFiMaskLentOnTec.Enabled = isLentEnabled;

            // For special case that mask is lent out: make field read-only
            //if (!isLentEnabled && mViewCoWorker.CbxSiFiRespMaskBriefRec.Checked && mCoWorkerModel.HasMaskTecBos)
            if (!isLentEnabled && mViewCoWorker.CbxSiFiRespMaskBriefRec.Checked && mRespMaskBO.IsReadOnly )
                {
                mViewCoWorker.TxtSiFiMaskNrLentTec.Enabled = true;
                mViewCoWorker.TxtSiFiMaskNrLentTec.ReadOnly = true;
            }
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
            int dateCompared = mCoWorkerModel.CompareDates(mMaskDate, mViewCoWorker.DatSiFiMaskLentOnTec.Value);

            if (dateCompared != 0)
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

            bool isAvailable = false;
            bool maintDateOK = false;
            bool isAlreadyLent = true;
            int numRecs = 0;
            string cwrName = "";
            string msgTecBos = "";
           
			// Query the respmask available DB View
			mSelComm = mProvider.CreateCommand(SEL_MASK_AVAILBL);
			mProvider.SetParameter(mSelComm, PARA_MASK_BARCODE_TEC, mMaskNoLent);

			// Open data reader: grouping Max in SQL means one row.
			IDataReader mDR = mProvider.GetReader(mSelComm);
			while (mDR.Read())
			{
                numRecs ++;

                var blockedDate = mDR["GESPERRTDATUM"].Equals(DBNull.Value) ? String.Empty : Convert.ToString(mDR["GESPERRTDATUM"]).Substring(0, 10);
                var blockedReason = Convert.ToString(mDR["GESPERRTGRUND"]);
                int functionOK = Convert.ToInt16(mDR["FUNKTIONSTUECHTIG"]);
                int toDispose = Convert.ToInt16(mDR["AUSGEMUSTERT"]);
                var whereMask = Convert.ToString(mDR["STANDORT1"]);
                cwrName = Convert.ToString(mDR["COWORKER"]);

                // Check if mask is available in TecBos according to business rules.
                // TODO in next version: remove hard-coding
                isAvailable = blockedDate.Length == 0 
                                && blockedReason.Length == 0 
                                && functionOK == 1 
                                && toDispose == 0 && whereMask.StartsWith(Globals.GetInstance().TecBosMaskFree);

                
                // Get maintenance dates last one and next one.
                mNextMaintDate = mDR["GER_NEXT_MAINTAIN"].Equals(DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(mDR["GER_NEXT_MAINTAIN"]);

                // Check next maintenance date
                // Next maintainance date must be bigger than validFrom + reserve time
                // (at this stage we don't know if CWR has short medium long access so we can't use that)
                // Read ValidFrom directly out of Form.
                var validFrom = mViewCoWorker.DatPassValidFrom.Value;
                maintDateOK = (mNextMaintDate.HasValue && mNextMaintDate.Value > validFrom.AddDays(Globals.GetInstance().RespMaskReserveTime));

                msgTecBos = "\n Angaben aus TecBos: \n"
                                + "- Nächstes Wartungsdatum: " + mNextMaintDate.ToString().Substring(0, 10) + "\n"
                                + "- Zeitpuffer in Tagen: " + Globals.GetInstance().RespMaskReserveTime.ToString() + "\n"
                                + "- Datum Gesperrt: " + (blockedDate.Length == 0 ? "-" : blockedDate) + "\n"
                                + "- Grund gesperrt: " + (blockedReason.Length == 0 ? "-" : blockedReason) + "\n"
                                + "- Funktionstüchtig: " + functionOK + "\n"
                                + "- Ausgemustert: " + toDispose + "\n"
                                + "- Standort: " + whereMask;


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
            else if (isAlreadyLent)
            {
                // Mask is already lent.             
                var errMsg = string.Format(MessageSingleton.GetInstance().GetMessage(MessageSingleton.MASK_ALREADY_LENT), mMaskNoLent, cwrName);
                mCoWorkerModel.ErrorMessages.Append(errMsg);
                return false;
            }          
            else if (!isAvailable)
            {
                // Not available in TecBos due to business rules
                var errMsg = string.Format(MessageSingleton.GetInstance().GetMessage(MessageSingleton.MASKNR_NOT_AVAIL), mMaskNoLent, "TecBos");
                mCoWorkerModel.ErrorMessages.Append(errMsg); 
                mCoWorkerModel.ErrorMessages.Append(msgTecBos);
                return false;
            }
            else if (!maintDateOK)
            {
                // If its maintainance date overdue or missing then it cannot be lent
                mCoWorkerModel.ErrorMessages.Append(MessageSingleton.GetInstance().GetMessage(MessageSingleton.MASK_MAINT_OVERDUE));
                mCoWorkerModel.ErrorMessages.Append(msgTecBos);
                return false;
            }
          
            // IF we have got to here then all is ok.
            return true;
        }

        #endregion

    }
}

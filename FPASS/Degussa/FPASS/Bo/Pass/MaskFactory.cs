using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Linq;

using de.pta.Component.DbAccess;
using de.pta.Component.Errorhandling;
using de.pta.Component.DbAccess.Exceptions;

using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.Util.Exceptions;
using Degussa.FPASS.Util.UserManagement;
using Degussa.FPASS.Util;
using Degussa.FPASS.Db;
using Degussa.FPASS.Bo.Administration;

using Degussa.FPASS.Gui.Dialog.Pass;
using Degussa.FPASS.FPASSApplication;

namespace Degussa.FPASS.Bo.Pass
{
	/// <summary>
	/// Read configuration an initialize mask-systems (TecBos, FLORIX)
	/// </summary>
	/// <remarks>
	/// <para><b>History</b></para>
	/// <div class="tablediv">
	/// <table class="dtTABLE" cellspacing="0">
	///		<tr>
	///			<th width="20%">PTA GmbH</th>
	///			<th width="20%">26/09/2017</th>
	///			<th width="60%">Remarks</th>
	///		</tr>
	/// </table>
	/// </div>
	/// </remarks>
	public class MaskFactory
	{
		#region Members



		private CoWorkerModel mCoWorkerModel;
        private IProvider mProvider;
		private IDbCommand mSelComm;
		private decimal mCoWorkerID;

        private BORespMask mRespMaskLentByCoworker;
        private BORespMask mRespMaskReturnedByCoworker;


        private string SELECT_MASK_CWR_LENT = "SelectMaskCoworkerLent";
        private string SELECT_MASK_CWR_LENT_ARCHIVE = "SelectMaskCoworkerLentArchive";

        private string SELECT_MASK_CWR_RETURNED = "SelectMaskCoworkerReturned";
        private string SELECT_MASK_CWR_RETURNED_ARCHIVE = "SelectMaskCoworkerReturnedArchive";

        
        private string MASK_CWR_PARAM = ":REMA_CWR_ID";

        private ArrayList mMasksList;

		#endregion

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public MaskFactory(CoWorkerModel pModel)
		{
			mCoWorkerModel = pModel;
			initialize();
		}

		#endregion 

		#region Initialization

		/// <summary>
		/// Initializes the members.
		/// Empty BOs are initialized if a new coworkwer is being created
		/// </summary>
		private void initialize()
		{
			// Get DataProvider from DbAccess component
			mProvider = DBSingleton.GetInstance().DataProvider;
			mCoWorkerID = mCoWorkerModel.CoWorkerId;

//            ReadMaskLentByCoworkerFromDb();

 		}


		#endregion 

		#region Methods 

        /// <summary>
        /// Get the lent mask for coworker from database.
        /// </summary>
        /// <returns>Object with the lent mask. If no mask was lent object has default values (MaskId = 0)</returns>
        private void ReadMaskLentByCoworkerFromDb()
        {
            mRespMaskLentByCoworker = new BORespMask();

            try
            {
                // Get right SQL command and set CWR id
                string comdName = mCoWorkerModel.Mode.Equals(AllFPASSDialogs.CWR_DIALOG_MODE_ARCHIVE) ? SELECT_MASK_CWR_LENT_ARCHIVE : SELECT_MASK_CWR_LENT;
                mSelComm = mProvider.CreateCommand(comdName);
                mProvider.SetParameter(mSelComm, MASK_CWR_PARAM, mCoWorkerID);

                // Open data reader to get assignments coworker - mask lent, 
                IDataReader mDR = mProvider.GetReader(mSelComm);
                while (mDR.Read())
                {

                    mRespMaskLentByCoworker.MaskTypeId = Convert.ToInt32(mDR["REMA_RMTY_ID"]);
                    // Get mask number, PK of user for DB and user nice name for display
                    mRespMaskLentByCoworker.MaskId = Convert.ToInt32(mDR["REMA_ID"]);
                    mRespMaskLentByCoworker.MaskNo = mDR["REMA_MASKNO"].ToString();
                    mRespMaskLentByCoworker.MaskDate = mDR["REMA_DATE"].Equals(DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(mDR["REMA_DATE"]);
                    mRespMaskLentByCoworker.NextMaintDate = null;
                    mRespMaskLentByCoworker.Userid = mDR["REMA_USER_ID"].ToString();
                    mRespMaskLentByCoworker.UserName = mDR["USERNICENAME"].ToString();
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

        }

        /// <summary>
        /// Get the last returned mask for coworker from database.
        /// </summary>
        /// <returns>Object with the returned mask. If no mask was returned object has default values (MaskId = 0)</returns>
        private void ReadMaskReturnedByCoworkerFromDb()
        {
            mRespMaskReturnedByCoworker = new BORespMask();

            try
            {
                // Get right SQL command and set CWR id
                string comdName = mCoWorkerModel.Mode.Equals(AllFPASSDialogs.CWR_DIALOG_MODE_ARCHIVE) ? SELECT_MASK_CWR_RETURNED_ARCHIVE : SELECT_MASK_CWR_RETURNED;
                mSelComm = mProvider.CreateCommand(comdName);
                mProvider.SetParameter(mSelComm, MASK_CWR_PARAM, mCoWorkerID);

                // Open data reader to get assignments coworker - mask lent, 
                IDataReader mDR = mProvider.GetReader(mSelComm);
                while (mDR.Read())
                {

                    mRespMaskReturnedByCoworker.MaskTypeId = Convert.ToInt32(mDR["REMA_RMTY_ID"]);
                    // Get mask number, PK of user for DB and user nice name for display
                    mRespMaskReturnedByCoworker.MaskId = Convert.ToInt32(mDR["REMA_ID"]);
                    mRespMaskReturnedByCoworker.MaskNo = mDR["REMA_MASKNO"].ToString();
                    mRespMaskReturnedByCoworker.MaskDate = mDR["REMA_DATE"].Equals(DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(mDR["REMA_DATE"]);
                    mRespMaskReturnedByCoworker.NextMaintDate = null;
                    mRespMaskReturnedByCoworker.Userid = mDR["REMA_USER_ID"].ToString();
                    mRespMaskReturnedByCoworker.UserName = mDR["USERNICENAME"].ToString();
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

        }



        /// <summary>
        /// Get a list of masks for coworker and add them to CoWorkerModell
        /// </summary>
        /// <returns></returns>
        internal ArrayList GetMasks()
        {
            
            //mCoWorkerModel.v
            
            ReadMaskLentByCoworkerFromDb();

            mMasksList = new ArrayList();

            //this.InitMaskSystems();

            // Activate TecBosLent 
            // if activated in config
            // and no Florix mask is lent (avoind lending to masks in different systems)
            if ((Globals.GetInstance().TecBosLentActive && !mRespMaskLentByCoworker.IsMasksystemFlorix) || mRespMaskLentByCoworker.IsMasksystemTecbos)
            {
                // Tecbos mask lent
                if (mRespMaskLentByCoworker.IsMasksystemTecbos)
                {
                    mCoWorkerModel.HasMaskTecBos = true;
                    mMasksList.Add(new MaskTecBosLent(mCoWorkerModel, mRespMaskLentByCoworker));
                }
                else
                {
                    mCoWorkerModel.HasMaskTecBos = false;
                    mMasksList.Add(new MaskTecBosLent(mCoWorkerModel, new BORespMask()));
                }
            }

            // TecBos Return
            // if activated in config
            // and TecBos-Mask is lent, so it can be returnd
            if (Globals.GetInstance().TecBosReturnActive && mRespMaskLentByCoworker.IsMasksystemTecbos)
            {
                mMasksList.Add(new MaskTecBosReturned(mCoWorkerModel, new BORespMask(), mRespMaskLentByCoworker));
            }


            // Floxix Lent 
            // if activated in config
            // and no tecbos mask is lent (avoind lending to masks in different systems)
            if ((Globals.GetInstance().FlorixLentActive && !mRespMaskLentByCoworker.IsMasksystemTecbos) || mRespMaskLentByCoworker.IsMasksystemFlorix)
            {
                // Florix mask lent
                if (mRespMaskLentByCoworker.IsMasksystemFlorix)
                {
                    mCoWorkerModel.HasMaskFlorix = true;
                    mMasksList.Add(new MaskFlorixLent(mCoWorkerModel, mRespMaskLentByCoworker));
                }
                else
                {
                    mCoWorkerModel.HasMaskFlorix = false;
                    mMasksList.Add(new MaskFlorixLent(mCoWorkerModel, new BORespMask()));                
                }

                //MaskTecBosLent maskTecBosLent = new MaskTecBosLent(mCoWorkerModel );
            }

            // Florix  Return
            // if activated in config
            // and Florix-Mask is lent, so it can be returnd
            if (Globals.GetInstance().FlorixReturnActive && mRespMaskLentByCoworker.IsMasksystemFlorix)
            {
                mMasksList.Add(new MaskFlorixReturned(mCoWorkerModel, new BORespMask(), mRespMaskLentByCoworker));
            }

            return mMasksList;
        }

        
		#endregion 

	}
}

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

using Degussa.FPASS.Gui.Dialog.Pass;

namespace Degussa.FPASS.Bo.Pass
{
	/// <summary>
	/// Base class of all briefings in fpass.
	/// A coworker can get several briefings. A briefing must be directed by
	/// a coordinator before it can be received.
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
	public class AbstractBriefing : AbstractCoWorkerBO
	{
		#region Members

		/// <summary>
		/// id (pk) of cwr-briefing assignment
		/// </summary>
		protected decimal mBriefingID;

        // <summary>
		/// Type id of the briefing. Each subclass has its unique type id.
		/// </summary>
		protected decimal mBriefingTypeID;

        /// <summary>
		/// flag indicating wether a briefing was directed
		/// </summary>
		protected bool mDirected;

        // date when briefing was directed (assigned)	
        protected DateTime mDirectedBriefingDate;

        // name of user who set status directed
        protected String mDirectedUserName;

        // id of user who set status directed
        protected decimal mDirectedUserID;

		/// <summary>
		/// flag indicating wether a briefing was received
		/// </summary>
		protected bool mReceived;

		// date when briefing was received	
		protected DateTime mBriefingDate;

        // name of user who set status received
        protected String mUserName;
        
        // id of user who set status received
		protected decimal mUserID;

		protected decimal mCoWorkerID;
		
        // timestamp of last change
        protected DateTime mTimeStamp;

		protected bool mIsValid;
		protected bool mIsInsert;

		/// <summary>
		/// Flag indicating wether briefing date was null before user changed it.
		/// Used here because DateTime can not be null, but null must be saved in db, 
		/// if user did not grant a briefing.
		/// </summary>
		protected bool mBriefDateWasNull;

		// constants for dml statements
		protected String UPDATE_BRIEFING = "UpdateBriefing";
		protected String INSERT_BRIEFING = "InsertBriefing";

		protected String PK_PARAM = ":CWBR_ID";
		protected String CWRID_PARAM = ":CWBR_CWR_ID";
		protected String BRIEFTYPE_PARAM = ":CWBR_BRF_ID";	
		protected String DIRECTEDYN_PARAM = ":CWBR_BRIEFING_YN";
        protected String DIRECTEDUSERID_PARAM = ":CWBR_DIRECTUSER_ID";
        protected String DIRECTEDBRIEFDATE_PARAM = ":CWBR_DIRECTBRIEFINGDATE";
        protected String RECEIVEDYN_PARAM = ":CWBR_INACTIVE_YN";
		protected String USERID_PARAM = ":CWBR_USER_ID";
        protected String BRIEFDATE_PARAM = ":CWBR_BRIEFINGDATE";
		protected String TIMESTAMP_PARAM = ":CWBR_TIMESTAMP";
		protected String LASTUSER_PARAM  = ":CWBR_LASTUSER_ID";

		/// <summary>
		/// command used for db inserts and updates
		/// </summary>
		private IDbCommand mSqlCommand;

		#endregion Members

		#region Constructors

		/// <summary>
		/// Simple constructor.
		/// </summary>
		public AbstractBriefing(CoWorkerModel pCoWorkerModel) : base (pCoWorkerModel)
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
			// Get DataProvider from DbAccess component
			mProvider = DBSingleton.GetInstance().DataProvider;

			mCoWorkerID = mCoWorkerModel.CoWorkerId;
			mTimeStamp = DateTime.Now;
            mReceived = false;
			mUserID = UserManagementControl.getInstance().CurrentUserID;
			mUserName = String.Empty;
			mBriefingDate = DateTime.Now;
			mDirected = false;
            mDirectedUserID = UserManagementControl.getInstance().CurrentUserID;
            mDirectedUserName = String.Empty;
            mDirectedBriefingDate = DateTime.Now;
			mIsValid = false;
			mIsInsert = true; 
			mBriefDateWasNull = true;
		}	

		#endregion Initialization

		#region Accessors 

		/// <summary>
        /// Gets or sets PK ID of current coworker-briefing assignment
		/// </summary>
		public decimal BriefingID 
		{
			get 
			{
				return mBriefingID;
			}
			set 
			{
				mBriefingID = value;
			}
		}

        // <summary>
		/// Gets or sets PK ID of current briefing
		/// </summary>
		public decimal BriefingTypeID 
		{
			get 
			{
				return mBriefingTypeID;
			}
			set 
			{
				mBriefingTypeID = value;
			}
		}

        /// <summary>
        /// Gets or sets status "Directed" (Angeordnet)
        /// </summary>
        public bool Directed
        {
            get
            {
                return mDirected;
            }
            set
            {
                mDirected = value;
            }
        }

        /// <summary>
        /// Gets or sets DirectedBriefingDate: Only used for status "Directed" (Angeordnet)
        /// </summary>
        public DateTime DirectedBriefingDate
        {
            get
            {
                return mDirectedBriefingDate;
            }
            set
            {
                mDirectedBriefingDate = value;
            }
        }

        /// <summary>
        /// Gets or sets name of user who set status "directed" (Angeordnet)
        /// </summary>
        public String DirectedUserName
        {
            get
            {
                return mDirectedUserName;
            }
            set
            {
                mDirectedUserName = value;
            }
        }

        /// <summary>
        /// Gets or sets ID of user who set status "directed" (Angeordnet)
        /// </summary>
        public decimal DirectedUserID
        {
            get
            {
                return mDirectedUserID;
            }
            set
            {
                mDirectedUserID = value;
            }
        }

        /// <summary>
        /// Gets or sets status "received" (Erteilt)
        /// </summary>
        public bool Received
        {
            get
            {
                return mReceived;
            }
            set
            {
                mReceived = value;
            }
        }

		/// <summary>
        /// Gets or sets BriefingDate: Only used for status "received" (Erteilt)
		/// </summary>
		public DateTime BriefingDate
		{
			get 
			{
				return mBriefingDate;
			}
			set 
			{
				mBriefingDate = value;
			}
		}

		/// <summary>
        /// Whether briefing date was null: if yes briefing not received
		/// </summary>
		public bool BriefDateWasNull
		{
			get 
			{
				return mBriefDateWasNull;
			}
			set 
			{
				mBriefDateWasNull = value;
			}
		}

        /// <summary>
        /// Gets or sets name of user who set status "received" (Erteilt)
        /// </summary>
        public String UserName
        {
            get
            {
                return mUserName;
            }
            set
            {
                mUserName = value;
            }
        }

        /// <summary>
        /// Gets or sets ID of user who set status "received" (Erteilt)
        /// </summary>
        public decimal UserID
        {
            get
            {
                return mUserID;
            }
            set
            {
                mUserID = value;
            }
        }

		/// <summary>
        /// Gets or sets current CoWorker ID
		/// </summary>
		public decimal CoWorkerID 
		{
			get 
			{
				return mCoWorkerID;
			}
			set 
			{
				mCoWorkerID = value;
			}
		}

		/// <summary>
        /// Gets or sets timestamp of last change. 
		/// </summary>
		public DateTime TimeStamp 
		{
			get 
			{
				return mTimeStamp;
			}
			set 
			{
				mTimeStamp = value;
			}
		}

		/// <summary>
		/// Whether or not briefing is new: insert into database table
		/// </summary>
		public bool IsInsert 
		{
			get 
			{
				return mIsInsert;
			}
			set 
			{
				mIsInsert = value;
			}
		}

		#endregion Accessors

		#region Methods 

		/// <summary>
		/// Initializes a briefing.
		/// </summary>
		internal override void InitializeBO() 
		{
			mCoWorkerID = mCoWorkerModel.CoWorkerId;
		}

			
		/// <summary>
		/// Validates a briefing.
		/// Gets sequence value from db when bo is in insert mode and has changed.
		/// <exception cref="de.pta.Component.Errorhandling.UIFatalException">
		/// Is thrown if select statement failed.
		/// </summary>
        internal override void Validate()
        {
            try
            {
                if (mChanged && mIsInsert)
                {
                    mBriefingID = mCoWorkerModel.GetNextValFromSeq("SEQ_CWRBRIEFING");
                }
            }
            catch (DbAccessException)
            {
                throw new UIFatalException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR));
            }
            catch (System.Data.OracleClient.OracleException oraex)
            {
                throw new UIFatalException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR) + oraex.Message);
            }
        }

		/// <summary>
		/// Saves a briefing to db. Knows whether to perform an insert or an update.
		/// DML statement is executed only when data has changed.
		/// 15.03.2004: Vehicle Entrance briefings have one extra status "not processed": the UserName 
		/// of last user who made changes is dealt with in the BOs themselves 
		/// </summary>
		/// <exception cref="de.pta.Component.Errorhandling.UIFatalException">
		/// Is thrown if dml statement failed.
		/// </exception>
		internal override void Save() 
		{
			decimal vehShortEntranceID = Globals.GetInstance().BriefVehicleEntranceShortID;
			decimal vehLongEntranceID  = Globals.GetInstance().BriefVehicleEntranceLongID;

			try 
			{
				if (mChanged) 
				{
					mCoWorkerModel.ZKSChanged = true;
					if (mIsInsert) 
					{
						InsertBriefing();
					} 
					else  
					{
						UpdateBriefing();
					}
					
                    if (!mReceived) 
					{
						// show no name in GUI view when briefing has not been granted
						// except for Vehicle Entrance briefings, which have a different logic
						if ( mBriefingTypeID != vehShortEntranceID && mBriefingTypeID != vehLongEntranceID )
						{
							mUserName = String.Empty;
						}
					}
                    if (!mDirected)
                    {
                        mDirectedUserName = String.Empty;
                    }
					mChanged = false;
					mIsInsert = false;
				} 
			}
			// Don't catch Oracle exception here: do it in Coworker model to be able to react to particular errors thrown by DB
			catch (DbAccessException dae)
			{
				throw new UIFatalException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR) + dae.Message );
			}	
		}


		/// <summary>
		/// Updates briefing in db.
		/// </summary>
		private void UpdateBriefing() 
		{
			mIsInsert = false;
            mSqlCommand = null;

            mSqlCommand = mProvider.CreateCommand(UPDATE_BRIEFING);
            mSqlCommand.Transaction = mCoWorkerModel.CurrentTransaction;
            mSqlCommand.Connection = mCoWorkerModel.CurrentTransaction.Connection;

            SetParameters(mSqlCommand);
            mSqlCommand.ExecuteNonQuery();
		}


		/// <summary>
		/// Inserts briefing in db.
		/// </summary>
		private void InsertBriefing() 
		{
            mSqlCommand = null;

            mSqlCommand = mProvider.CreateCommand(INSERT_BRIEFING);
            mSqlCommand.Transaction = mCoWorkerModel.CurrentTransaction;
            mSqlCommand.Connection = mCoWorkerModel.CurrentTransaction.Connection;

            SetParameters(mSqlCommand);
            mSqlCommand.ExecuteNonQuery();
		}


		/// <summary>
        /// Sets parameter values in DML statement.
        /// LASTUSER_PARAM: user who made the last change, so that it shows up in the History.
        /// TIMESTAMP_PARAM: timestamp of last change.
        /// RECEIVEDYN_PARAM: briefing inactive Y/N, N means received.
        /// USERID_PARAM: id of user who set status "received" or changed received date.
        /// BRIEFDATE_PARAM: date that status "received" was set (null if only "directed").
        /// DIRECTEDUSERID_PARAM: user who directed briefing
        /// DIRECTEDYN_PARAM: briefing directed Y/N.
        /// DIRECTEDBRIEFDATE_PARAM: date briefing was directed
		/// </summary>
		/// <param name="pCommand">Command where parameters have to be set</param>
		protected virtual void SetParameters(IDbCommand pCommand) 
		{
			String directedYN = "N";
			String receivedYN = "Y";

			// user and timestamp of last change
			mProvider.SetParameter(pCommand, LASTUSER_PARAM, UserManagementControl.getInstance().CurrentUserID);
            mProvider.SetParameter(pCommand, TIMESTAMP_PARAM, mTimeStamp);

			mProvider.SetParameter(pCommand, PK_PARAM, mBriefingID);
			mProvider.SetParameter(pCommand, CWRID_PARAM, mCoWorkerID);
			mProvider.SetParameter(pCommand, BRIEFTYPE_PARAM, mBriefingTypeID);
			mProvider.SetParameter(pCommand, USERID_PARAM, mUserID);
            mProvider.SetParameter(pCommand, DIRECTEDUSERID_PARAM, mDirectedUserID);

            // Sets status received
            receivedYN = (mReceived ? "N" : "Y");
            mProvider.SetParameter(pCommand, RECEIVEDYN_PARAM, receivedYN);
  
			if (!mReceived ) 
			{
                // Deletes date received if briefing not received
				mProvider.SetParameter(pCommand, BRIEFDATE_PARAM, DBNull.Value ); 
				mBriefDateWasNull = true;
			} 
			else 
			{
				mProvider.SetParameter(pCommand, BRIEFDATE_PARAM, mBriefingDate); 
				mBriefDateWasNull = false;
			}
			
            // Status directed
            directedYN = (mDirected ? "Y" : "N");
            mProvider.SetParameter(pCommand, DIRECTEDYN_PARAM, directedYN);

            if (!mDirected)
            {
                mProvider.SetParameter(pCommand, DIRECTEDBRIEFDATE_PARAM, DBNull.Value);  
            }
            else
            {
                mProvider.SetParameter(pCommand, DIRECTEDBRIEFDATE_PARAM, mDirectedBriefingDate);        
            }
		}

        /// <summary>
        /// Saves current userId and user nice name if briefing status "Directed" (Angeordnet) or date directed was changed
        /// </summary>
        protected void SetWhoChangedMeDirected()
        {
            mDirectedUserID = UserManagementControl.getInstance().CurrentUserID;
            mDirectedUserName = UserManagementControl.getInstance().CurrentUserNiceName;
        }

		/// <summary>
        /// Saves current userId and user nice name if briefing status "Received" (Erteilt) or date received was changed
		/// </summary>
		protected void SetWhoChangedMeReceived()
		{
			mUserID   = UserManagementControl.getInstance().CurrentUserID;
			mUserName = UserManagementControl.getInstance().CurrentUserNiceName;
		}

		#endregion Methods


	}
}

using System;
using System.Data;
using Degussa.FPASS.Db;
using Degussa.FPASS.Gui.Dialog.Pass;
using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.Util.UserManagement;

namespace Degussa.FPASS.Bo.Pass
{
	/// <summary>
	/// Business object for details of individual precautionary medical
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
	public class PrecautionaryMedicalBriefing : AbstractCoWorkerBO
	{
		#region Members

		protected			decimal			mPrecMedID;
		protected			DateTime		mPrecMedDate;
		protected			decimal			mPrecMedTypeID;
		protected			String			mNotationPlusType;
		protected			String			mNotation;
		protected			String			mType;
		protected			DateTime		mValidUntil;
		protected			bool			mReceived;
		protected			decimal			mCoWorkerID;
		protected			String			mUserName;
		protected			decimal			mUserID;
		protected			DateTime		mTimeStamp;	
		protected			bool			mIsValid;

		private			bool				mInsert;

		/// <summary>
		/// constants for insert, update and delete commands
		/// </summary>
		private				String			INSERT_PRECMED = "InsertPrecautionaryMedical";
		private				String			UPDATE_PRECMED = "UpdatePrecautionaryMedical";
		private				String			DELETE_PRECMED = "DeletePrecautionaryMedical";
		
		/// <summary>
		/// Command objects: insert, update, delete
		/// </summary>
		private				IDbCommand		mInsertComm;
		private				IDbCommand		mUpdateComm; 
		private				IDbCommand		mDeleteComm;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public PrecautionaryMedicalBriefing(CoWorkerModel pCoWorkerModel) : base (pCoWorkerModel)
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
			mUserID = UserManagementControl.getInstance().CurrentUserID;
		}	

		#endregion //End of Initialization

		#region Accessors

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public String Status 
		{
			get 
			{
				if ( mReceived ) 
				{
					return "Erteilt";
				} 
				else 
				{
					return "Nicht erteilt";
				}
			}
		}

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public decimal PrecMedID 
		{
			get 
			{
				return mPrecMedID;
			}
			set 
			{

				mPrecMedID = value;
			}
		}

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public decimal PrecMedTypeID 
		{
			get 
			{
				return mPrecMedTypeID;
			}
			set 
			{
				mPrecMedTypeID = value;
			}
		}

		/// <summary>
		/// Simple getter and setter.
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
		/// Simple getter and setter.
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
		/// Simple getter and setter.
		/// </summary>
		public DateTime ValidUntil 
		{
			get 
			{
				return mValidUntil;
			} 
			set 
			{
				mValidUntil = value;
			}
		}

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public String ValidUntilAsString 
		{
			get 
			{
				return mValidUntil.ToString().Substring(0,10);
			}
		}

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public String PrecMedDateAsString 
		{
			get 
			{
				return mPrecMedDate.ToString().Substring(0,10);
			}
		}

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public DateTime PrecMedDate 
		{
			get 
			{
				return mPrecMedDate;
			} 
			set 
			{
				mPrecMedDate = value;
			}
		}

		/// <summary>
		/// Simple getter and setter.
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
		/// Simple getter and setter.
		/// </summary>
		public String NotationPlusType 
		{
			get 
			{
				return mNotationPlusType;
			}
			set 
			{	
				mNotationPlusType = value;
			}
		}

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public String Notation 
		{
			get 
			{
				return mNotation;
			}
			set 
			{	
				mNotation = value;
			}
		}

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public String Type 
		{
			get 
			{
				return mType;
			}
			set 
			{	
				mType = value;
			}
		}

		/// <summary>
		/// Simple getter and setter.
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
		/// Simple getter and setter.
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
		/// Simple getter and setter.
		/// </summary>
		public bool IsValidPrecMed 
		{
			get 
			{
				return mIsValid;
			}
			set 
			{	
				mIsValid = value;
			}
		}

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public bool IsInsert 
		{
			get 
			{
				return mInsert;
			}
			set 
			{
				mInsert = value;
			}
		}
 
		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// Copies BO attributes into GUI (medical service "WD" tab)
		/// Prec Meds always have status received (erteilt), Date executed always set to current date
		/// </summary>
		internal override void CopyIn() 
		{
			if ( !mReceived )
			{
				mPrecMedDate = DateTime.Now;
			}			
			mChanged = false;
			
			mViewCoWorker.TxtSiMedExecutedBy.Text = mUserName;
			mViewCoWorker.CboSiMedPrecautionaryMedical.SelectedValue = mPrecMedTypeID;
			mViewCoWorker.DatSiMedValidUntil.Value = mValidUntil;
		}

		
		/// <summary>
		/// Copies values out of GUI: name of medical and valid until date.
		/// Prec Meds always have status received (erteilt), Date executed always set to current date
		/// </summary>
		internal override void CopyOut() 
		{
			int dateComparedValidUntil = mCoWorkerModel.CompareDates(mValidUntil, mViewCoWorker.DatSiMedValidUntil.Value);
			mReceived = true;

            if (!mViewCoWorker.CboSiMedPrecautionaryMedical.SelectedValue.Equals(mPrecMedTypeID))
            {
                mPrecMedTypeID = Convert.ToDecimal(mViewCoWorker.CboSiMedPrecautionaryMedical.SelectedValue);
                mChanged = true;
            }
			if (dateComparedValidUntil != 0) 
			{
				mValidUntil = mViewCoWorker.DatSiMedValidUntil.Value;
                mPrecMedDate = DateTime.Now;
				mUserID = UserManagementControl.getInstance().CurrentUserID;
				mUserName = UserManagementControl.getInstance().CurrentUserNiceName;
				mChanged = true;
			} 			
		}

		internal override void PreClose()
		{
		}

		/// <summary>
		/// Initialises new medical object and shows it in GUI
		/// </summary>
		internal void InitializeNew() 
		{
			mPrecMedTypeID = Convert.ToDecimal(mViewCoWorker.CboSiMedPrecautionaryMedical.SelectedValue);
			mUserName = String.Empty;
			mPrecMedDate = DateTime.Now;
			mReceived = false;
			mIsValid = false;
			mNotationPlusType = String.Empty;
			mValidUntil = DateTime.Now;
			mInsert = true;
			CopyIn();
		}

		/// <summary>
		/// Validates business object before saving
		/// </summary>
		internal override void Validate() 
		{
			if ( PrecMedTypeID == 0 && mReceived ) 
			{
				mCoWorkerModel.ErrorMessages.Append(MessageSingleton.GetInstance().
					GetMessage(MessageSingleton.INVALID_PRECMED) );
			}

			if ( mChanged && mInsert) 
			{
				mPrecMedID = mCoWorkerModel.GetNextValFromSeq("SEQ_PRECAUTIONMED");
			}
		}

		internal bool IsReceived() 
		{
			if ( mReceived  ) 
			{
				return true;
			} 
			else 
			{
				return false;
			}
		}


		/// <summary>
		/// Deletes the assigned precautionary medical by deleting in the database
		/// Use the open connection and transaction from CWR model 
		/// </summary>
		internal void Remove() 
		{
			mDeleteComm = null;
			mDeleteComm = mProvider.CreateCommand(DELETE_PRECMED);

			mDeleteComm.Transaction = mCoWorkerModel.CurrentTransaction;
			mDeleteComm.Connection = mCoWorkerModel.CurrentTransaction.Connection;

			// Set parameters: current coworker, current prec med
			mProvider.SetParameter(mDeleteComm, ":PMED_CWR_ID", mCoWorkerID);
			mProvider.SetParameter(mDeleteComm, ":PMED_PMTY_ID", mPrecMedTypeID);

			mDeleteComm.ExecuteNonQuery();
		}

		/// <summary>
		/// Save new prec medical or changes to existing object
		/// ChangeinZKS is set to true as expiry date of prec med influences CWR access
		/// </summary>
		internal override void Save() 
		{
			if ( mChanged ) 
			{
				mCoWorkerModel.ZKSChanged = true;
				mTimeStamp = DateTime.Now;
				mUserID = UserManagementControl.getInstance().CurrentUserID;
				if ( mInsert) 
				{
					InsertPrecautionaryMedical();
				} 
				else 
				{
					UpdatePrecautionaryMedical();
				}
				
				mUserName = UserManagementControl.getInstance().CurrentUserNiceName;
				mViewCoWorker.TxtSiMedExecutedBy.Text = String.Empty;
				
				mChanged = false;
				mInsert = false;
			} 
		}

		/// <summary>
		/// Sets parameters for given SQL statement
		/// </summary>
		/// <param name="pCommand">given SQL statement</param>
		private void SetParameters(IDbCommand pCommand) 
		{
			String receivedYN = "Y";

			mProvider.SetParameter(pCommand, ":PMED_ID", mPrecMedID);
			mProvider.SetParameter(pCommand, ":PMED_TIMESTAMP", mTimeStamp);
			mProvider.SetParameter(pCommand, ":PMED_USER_ID", mUserID);
			mProvider.SetParameter(pCommand, ":PMED_CWR_ID", mCoWorkerID);
			mProvider.SetParameter(pCommand, ":PMED_PMTY_ID", mPrecMedTypeID);

			if ( mReceived ) 
			{
				receivedYN ="N";
			}
			mProvider.SetParameter(pCommand, ":PMED_INACTIVE_YN", receivedYN);

			if ( mReceived ) 
			{
				mProvider.SetParameter(pCommand, ":PMED_EXECUTEDON", mPrecMedDate); 
			} 
			else 
			{
				mProvider.SetParameter(pCommand, ":PMED_EXECUTEDON", DBNull.Value); 
			}

			if ( mReceived ) 
			{
				mProvider.SetParameter(pCommand, ":PMED_VALIDUNTIL", mValidUntil);
			} 
			else 
			{
				mProvider.SetParameter(pCommand, ":PMED_VALIDUNTIL", DBNull.Value); 

			}
		}

		/// <summary>
		/// Updates current precmedical with SQL
		/// Uses open connection from current transaction
		/// </summary>
		private void UpdatePrecautionaryMedical() 
		{
			mUpdateComm = null;
			mUpdateComm = mProvider.CreateCommand(UPDATE_PRECMED);

			mUpdateComm.Transaction = mCoWorkerModel.CurrentTransaction;
			mUpdateComm.Connection = mCoWorkerModel.CurrentTransaction.Connection;

			SetParameters(mUpdateComm);

			mUpdateComm.ExecuteNonQuery();
		}

		/// <summary>
		/// Insert current precmedical with SQL
		/// Uses open connection from current transaction
		/// </summary>
		private void InsertPrecautionaryMedical() 
		{
			mInsertComm = null;
			mInsertComm = mProvider.CreateCommand(INSERT_PRECMED);

			mInsertComm.Transaction = mCoWorkerModel.CurrentTransaction;
			mInsertComm.Connection = mCoWorkerModel.CurrentTransaction.Connection;
			
			SetParameters(mInsertComm);

			mInsertComm.ExecuteNonQuery();
		}


		#endregion // End of Methods
	}
}

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
	/// Summary description for VehicleRegistrationNumber.
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
	public class VehicleRegistrationNumber : AbstractCoWorkerBO
	{
		#region Members

		protected			decimal			mCoWorkerID;
		protected			decimal			mVehicleId;
		protected			string			mVehicleNumber;
		protected			decimal			mChangeUser;
		protected			string			mUserName;
		protected			DateTime		mTimeStamp;
		protected			int				mVehicleFieldID;
		protected			bool			mInsert;

		// constants for update
		private				String			UPDATE_VEHREGNO = "UpdateVehicleRegistrationNumber";

		private				String			UPDATE_PK_PARAM = ":VRNO_ID";
		private				String			UPDATE_VEHICLENUMBER_PARAM = ":VRNO_VEHREGNO";
		private				String			UPDATE_USERID_PARAM = ":VRNO_CHANGEUSER";    
		private				String			UPDATE_TIMESTAMP_PARAM = ":VRNO_TIMESTAMP";      

		// constants for insert
		private				String			INSERT_VEHREGNO = "InsertVehicleRegistrationNumber";
		private				String			INSERT_CWRID_PARAM = ":VRNO_CWR_ID";
		private				String			INSERT_PK_PARAM = ":VRNO_ID";
		private				String			INSERT_VEHICLENUMBER_PARAM = ":VRNO_VEHREGNO";
		private				String			INSERT_USERID_PARAM = ":VRNO_CHANGEUSER";    
		private				String			INSERT_TIMESTAMP_PARAM = ":VRNO_TIMESTAMP";  

		private				IDbCommand		mUpdateComm;
		private				IDbCommand		mInsertComm;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public VehicleRegistrationNumber(CoWorkerModel pCoWorkerModel) : base (pCoWorkerModel)
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
			
			mInsert = true;
			mCoWorkerID = mCoWorkerModel.CoWorkerId;
			mTimeStamp = DateTime.Now;
			mChangeUser = UserManagementControl.getInstance().CurrentUserID;
			mUserName = String.Empty;

			mVehicleNumber = String.Empty;
		}	

		#endregion //End of Initialization

		#region Accessors 
		
		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public int VehicleFieldID 
		{
			get 
			{
				return mVehicleFieldID;
			}
			set 
			{
				mVehicleFieldID = value;
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
		public decimal VehicleId 
		{
			get 
			{
				return mVehicleId;
			}
			set 
			{
				mVehicleId = value;
			}
		}

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public string VehicleNumber
		{
			get 
			{
				return(mVehicleNumber);
			}
			set 
			{
				mVehicleNumber = value;
			}
		}

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public decimal ChangeUser 
		{
			get 
			{
				return mChangeUser;
			}
			set 
			{
				mChangeUser = value;
			}
		}

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public string UserName
		{
			get 
			{
				return(mUserName);
			}
			set 
			{
				mUserName = value;
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

		internal override void PreClose()
		{

		}

		internal override void InitializeBO() 
		{
			mCoWorkerID = mCoWorkerModel.CoWorkerId;
		}

		internal override void Validate() 
		{
			if ( mChanged && mInsert ) 
			{
				this.InitializeNewBO();
			}
		}

		internal override void Save() 
		{
			if ( mChanged ) 
			{
				mCoWorkerModel.ZKSChanged = true;
				mTimeStamp = DateTime.Now;
				mChangeUser = UserManagementControl.getInstance().CurrentUserID;

				if ( this.mInsert  ) 
				{
					this.InsertVehicleNo();
				} 
				else  
				{
					this.UpdateVehicleNo();
				}
				mChanged = false;
				mInsert = false;
			} 
		}

		private  void InitializeNewBO() 
		{
			mVehicleId = mCoWorkerModel.GetNextValFromSeq("SEQ_CWRBRIEFING");
		}

		private void UpdateVehicleNo() 
		{
			mUpdateComm = null;

			// Create the select command & fill Data Reader 
			mUpdateComm = mProvider.CreateCommand(UPDATE_VEHREGNO);

			mUpdateComm.Transaction = mCoWorkerModel.CurrentTransaction;
			mUpdateComm.Connection = mCoWorkerModel.CurrentTransaction.Connection;

			mProvider.SetParameter(mUpdateComm, UPDATE_PK_PARAM, mVehicleId);
			mProvider.SetParameter(mUpdateComm, UPDATE_VEHICLENUMBER_PARAM, mVehicleNumber);
			mProvider.SetParameter(mUpdateComm, UPDATE_USERID_PARAM, mChangeUser);
			mProvider.SetParameter(mUpdateComm, UPDATE_TIMESTAMP_PARAM, mTimeStamp);

			mUpdateComm.ExecuteNonQuery();
		}

		private void InsertVehicleNo() 
		{
			mInsertComm = null;

			// Create the select command & fill Data Reader 
			mInsertComm = mProvider.CreateCommand(INSERT_VEHREGNO);

			mInsertComm.Transaction = mCoWorkerModel.CurrentTransaction;
			mInsertComm.Connection = mCoWorkerModel.CurrentTransaction.Connection;

			mProvider.SetParameter(mInsertComm, INSERT_CWRID_PARAM, mCoWorkerID);
			mProvider.SetParameter(mInsertComm, INSERT_PK_PARAM, mVehicleId);
			mProvider.SetParameter(mInsertComm, INSERT_VEHICLENUMBER_PARAM, mVehicleNumber);
			mProvider.SetParameter(mInsertComm, INSERT_USERID_PARAM, mChangeUser);
			mProvider.SetParameter(mInsertComm, INSERT_TIMESTAMP_PARAM, mTimeStamp); 

			mInsertComm.ExecuteNonQuery();
		}

		internal override void CopyIn() 
		{
			if (this.mVehicleFieldID == 1)
			{
				mViewCoWorker.TxtReVehicleRegistrationNumber.Text = this.mVehicleNumber;
			}
			
			if (this.mVehicleFieldID == 2)
			{
				mViewCoWorker.TxtReVehicleRegistrationNumberTwo.Text = this.mVehicleNumber;
			}
			
			if (this.mVehicleFieldID == 3)
			{
				mViewCoWorker.TxtReVehicleRegistrationNumberThree.Text = this.mVehicleNumber;
			}
			
			if (this.mVehicleFieldID == 4)
			{
				mViewCoWorker.TxtReVehicleRegistrationNumberFour.Text = this.mVehicleNumber;
			}
		}

		internal override void CopyOut() 
		{
			if (this.mVehicleFieldID == 1)
			{
				if ( ! mViewCoWorker.TxtReVehicleRegistrationNumber.Text.
					Equals(this.mVehicleNumber) ) 
				{
					this.mVehicleNumber = mViewCoWorker.TxtReVehicleRegistrationNumber.Text;
					mChanged = true;
				} 
			}

			if (this.mVehicleFieldID == 2)
			{
				if ( ! mViewCoWorker.TxtReVehicleRegistrationNumberTwo.Text.
					Equals(this.mVehicleNumber) ) 
				{
					this.mVehicleNumber = mViewCoWorker.TxtReVehicleRegistrationNumberTwo.Text;
					mChanged = true;
				} 
			}

			if (this.mVehicleFieldID == 3)
			{
				if ( ! mViewCoWorker.TxtReVehicleRegistrationNumberThree.Text.
						Equals(this.mVehicleNumber) ) 
				{
					this.mVehicleNumber = mViewCoWorker.TxtReVehicleRegistrationNumberThree.Text;
					mChanged = true;
				} 
			}

			if (this.mVehicleFieldID == 4)
			{
				if ( ! mViewCoWorker.TxtReVehicleRegistrationNumberFour.Text.
					Equals(this.mVehicleNumber) ) 
				{
					this.mVehicleNumber = mViewCoWorker.TxtReVehicleRegistrationNumberFour.Text;
					mChanged = true;
				} 
			}
		}

		#endregion // End of Methods

	}
}

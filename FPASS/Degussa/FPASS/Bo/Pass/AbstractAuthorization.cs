using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

using de.pta.Component.DbAccess;
using de.pta.Component.Errorhandling;
using de.pta.Component.DbAccess.Exceptions;

using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.Util.Exceptions;
using Degussa.FPASS.Util.UserManagement;
using Degussa.FPASS.Db;

using Degussa.FPASS.Gui.Dialog.Pass;
using Degussa.FPASS.FPASSApplication;

namespace Degussa.FPASS.Bo.Pass
{
	/// <summary>
	/// Base class of all authorizations in fpass.
	/// A coworker can get several authorizations.
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
	public class AbstractAuthorization : AbstractCoWorkerBO
	{
		#region Members

		/// <summary>
		/// id (pk) of an authorization
		/// </summary>
		protected			decimal			mAuthorizationID;
		
		/// <summary>
		/// date when the authorization was granted
		/// </summary>
		protected			DateTime		mAuthorizationDate;
		
		/// <summary>
		/// flag wether the authorization was granted or not
		/// </summary>
		protected			bool			mAuthorizationExecuted;
		
		/// <summary>
		/// Type id of the authorization. Each subclass has its unique type id.
		/// </summary>
		protected			decimal			mAuthorizationTypeID;
		
		/// <summary>
		/// id (pk) of the coworker
		/// </summary>
		protected			decimal  		mCoWorkerID;
		
		/// <summary>
		/// name of the user who granted this authorization
		/// </summary>
		protected			String			mUserName;
		
		/// <summary>
		/// id of the user who granted this authorization
		/// </summary>
		protected			decimal			mUserID;
		
		/// <summary>
		/// last change to this authorization
		/// </summary>
		protected			DateTime		mTimestamp;
		
		/// <summary>
		/// flag wether this authorization is valid or not
		/// </summary>
		protected			bool			mIsValid;

		/// <summary>
		/// comment related to an authorization
		/// </summary>
		protected			String			mComment;

		/// <summary>
		/// flag indicating wether this authorization in insert or update mode
		/// </summary>
		protected			bool			mIsInsert;


		// constants for sql statements
		private				String			UPDATE_AUTHORIZATION = "UpdateAuthorization";
		private				String			INSERT_AUTHORIZATION = "InsertAuthorization";
		private				String			AUTHO_ID_PARAM = ":RATH_ID";
		private				String			CWRID_PARAM = ":RATH_CWR_ID";
		private				String			TYPE_PARAM = ":RATH_RATT_ID";
		private				String			USERID_PARAM = ":RATH_USER_ID";
		private				String			YN_PARAM = ":RATH_RECEPTAUTHO_YN";
		private				String			DATE_PARAM = ":RATH_RECEPTAUTHODATE";
		private				String			COMMENT_PARAM  = ":RATH_COMMENT";
		private				String			TIMESTAMP_PARAM = ":RATH_TIMESTAMP";

		/// <summary>
		/// command used for db updates
		/// </summary>
		private				IDbCommand		mUpdateComm;
		/// <summary>
		/// command user for db inserts
		/// </summary>
		private				IDbCommand		mInsertComm;

		

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Simple constructor
		/// </summary>
		/// <param name="pCoWorkerModel">model of this bo</param>
		public AbstractAuthorization(CoWorkerModel pCoWorkerModel) : base (pCoWorkerModel)
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

			mTimestamp = DateTime.Now;
			mUserID = UserManagementControl.getInstance().CurrentUserID;
			mUserName = String.Empty;
			mAuthorizationDate = DateTime.Now;
			mAuthorizationExecuted = false;
			mIsValid = false;
			mCoWorkerID = mCoWorkerModel.CoWorkerId;
			mComment = String.Empty;
			mIsInsert = true;
			
		}	

		#endregion //End of Initialization

		#region Accessors 

		/// <summary>
		/// simple accessor
		/// </summary>
		internal decimal AuthorizationID 
		{
			get 
			{
				return mAuthorizationID;
			} 
			set 
			{
				mAuthorizationID = value;
			}
		}

		/// <summary>
		/// simple accessor
		/// </summary>
		internal DateTime AuthorizationDate 
		{
			get 
			{
				return mAuthorizationDate;
			} 
			set 
			{
				mAuthorizationDate = value;
			}
		}

		/// <summary>
		/// simple accessor
		/// </summary>
		internal bool AuthorizationExecuted 
		{
			get 
			{
				return mAuthorizationExecuted;
			} 
			set 
			{
				mAuthorizationExecuted = value;
			}
		}

		/// <summary>
		/// simple accessor
		/// </summary>
		internal decimal AuthorizationTypeID 
		{
			get 
			{
				return mAuthorizationTypeID;
			} 
			set 
			{
				mAuthorizationTypeID = value;
			}
		}

		/// <summary>
		/// simple accessor
		/// </summary>
		internal decimal CoWorkerID 
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
		/// simple accessor
		/// </summary>
		internal decimal UserID 
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
		/// simple accessor
		/// </summary>
		internal String UserName 
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
		/// simple accessor
		/// </summary>
		internal DateTime Timestamp 
		{
			get 
			{
				return mTimestamp;
			} 
			set 
			{
				mTimestamp = value;
			}
		}


		/// <summary>
		/// simple accessor
		/// </summary>
		internal String Comment 
		{
			get 
			{
				return mComment;
			} 
			set 
			{
				mComment = value;
			}
		}

		/// <summary>
		/// simple accessor
		/// </summary>
		internal bool IsInsert 
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


		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// Initializes an authorization.
		/// </summary>
		internal override void InitializeBO() 
		{
			mCoWorkerID = mCoWorkerModel.CoWorkerId;
		}


		/// <summary>
		/// Saves an authorization to db. Knows wether to perform an insert
		/// or an update.
		/// DML statement is executed only when data has changed.
		/// <exception cref="de.pta.Component.Errorhandling.UIFatalException">
		/// Is thrown if dml statement failed.
		/// </exception>
		/// </summary>
		internal override void Save() 
		{
			try 
			{
				if ( mChanged ) 
				{
					mCoWorkerModel.ZKSChanged = true;
					if ( mIsInsert ) 
					{
						this.InsertAuthorization();
					} 
					else  
					{
						this.UpdateAuthorization();
					} 
					// Only save username if change to received Y/N: do this in subclasses
					//this.mUserName = UserManagementControl.getInstance().CurrentOSUserName;
					mChanged = false;
					mIsInsert = false;
				} 
			} 
			catch (DbAccessException)
			{
				throw new UIFatalException(MessageSingleton.GetInstance().GetMessage(
					MessageSingleton.FATAL_DB_ERROR)
					+ " Objekt: " + this.ToString() );
			}	
		}


		/// <summary>
		/// Validates an authorization.
		/// Gets sequence value from db when bo is in insert mode.
		/// <exception cref="de.pta.Component.Errorhandling.UIFatalException">
		/// Is thrown if select statement failed.
		/// </summary>
		internal override void Validate() 
		{
			try {
				if ( mChanged && mIsInsert ) 
				{
					mAuthorizationID = mCoWorkerModel.GetNextValFromSeq("SEQ_RECEPTIONAUTHORIZE");
				}
			} 
			catch (DbAccessException)
			{
				throw new UIFatalException(MessageSingleton.GetInstance().GetMessage(
					MessageSingleton.FATAL_DB_ERROR)
					+ " Objekt: " + this.ToString() );
			}	
			catch (System.Data.OracleClient.OracleException oraex)
			{
				throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(
					MessageSingleton.FATAL_DB_ERROR) 
					+ oraex.Message 
					+ " Objekt: " + this.ToString());
			}	
		}


		/// <summary>
		/// Updates authorization in db.
		/// </summary>
		private void UpdateAuthorization() 
		{
			mUpdateComm = null;

			// Create the select command & fill Data Reader 
			mUpdateComm = mProvider.CreateCommand(UPDATE_AUTHORIZATION);

			mUpdateComm.Transaction = mCoWorkerModel.CurrentTransaction;
			mUpdateComm.Connection = mCoWorkerModel.CurrentTransaction.Connection;

			this.SetParameters(mUpdateComm);

			mUpdateComm.ExecuteNonQuery();
			
		}


		/// <summary>
		/// Inserts authorization in db.
		/// </summary>
		private void InsertAuthorization() 
		{
			
			mInsertComm = null;
		
			// Create the select command & fill Data Reader 
			mInsertComm = mProvider.CreateCommand(INSERT_AUTHORIZATION);

			mInsertComm.Transaction = mCoWorkerModel.CurrentTransaction;
			mInsertComm.Connection = mCoWorkerModel.CurrentTransaction.Connection;

			this.SetParameters(mInsertComm);

			mInsertComm.ExecuteNonQuery();
		}


		/// <summary>
		/// Sets values (parameters) in dml statement
		/// </summary>
		/// <param name="pCommand">Command where paramters have to be set</param>
		private void SetParameters(IDbCommand pCommand) 
		{
			String	boolStringYN = "N";

			if ( mAuthorizationExecuted ) 
			{
				boolStringYN = "Y";	
			}

			mProvider.SetParameter(pCommand, AUTHO_ID_PARAM, mAuthorizationID);
			mProvider.SetParameter(pCommand, CWRID_PARAM, mCoWorkerID);
			mProvider.SetParameter(pCommand, TYPE_PARAM, mAuthorizationTypeID);
			mProvider.SetParameter(pCommand, USERID_PARAM, mUserID);
			mProvider.SetParameter(pCommand, YN_PARAM, boolStringYN);
			if ( mAuthorizationExecuted ) 
			{
				mProvider.SetParameter(pCommand, DATE_PARAM, mAuthorizationDate);
			} 
			else 
			{
				mProvider.SetParameter(pCommand, DATE_PARAM, DBNull.Value );
			}
			mProvider.SetParameter(pCommand, TIMESTAMP_PARAM, mTimestamp);
			if ( mComment.Length > 0 ) 
			{
				mProvider.SetParameter(pCommand, COMMENT_PARAM, mComment.Trim());
			} 
			else 
			{
				mProvider.SetParameter(pCommand, COMMENT_PARAM, DBNull.Value );
			}
		}

		/// <summary>
		/// Saves ID and nice name of current user if authorization was changed
		///  - usually only if status Received (Erteilt).
		/// </summary>
		protected void SetWhoChangedMeReceived()
		{
			this.mUserID   = UserManagementControl.getInstance().CurrentUserID;
			this.mUserName = UserManagementControl.getInstance().CurrentUserNiceName;
		}

		#endregion // End of Methods


	}
}

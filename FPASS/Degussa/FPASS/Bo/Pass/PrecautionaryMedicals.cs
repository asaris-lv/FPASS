using System;
using System.Collections;
using System.Data;
using System.Data.OracleClient;
using de.pta.Component.DbAccess;
using de.pta.Component.DbAccess.Exceptions;
using de.pta.Component.Errorhandling;
using Degussa.FPASS.Db;
using Degussa.FPASS.FPASSApplication;
using Degussa.FPASS.Gui.Dialog.Pass;
using Degussa.FPASS.Util;
using Degussa.FPASS.Util.Messages;

namespace Degussa.FPASS.Bo.Pass
{
	/// <summary>
	/// PrecautionaryMedicals is responsible for loading <see cref="PrecautionaryMedicalBriefing"/> instances
	/// also for creating, deleting etc at a higher level
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
	public class PrecautionaryMedicals : AbstractCoWorkerBO
	{
		#region Members

		private ArrayList mPrecMedicalList;
		private decimal	mCoWorkerID;
		private PrecautionaryMedicalBriefing mCurrentPrecMed;
		
		#endregion 

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public PrecautionaryMedicals(CoWorkerModel pCoWorkerModel) : base (pCoWorkerModel)
		{
			initialize();
		}

		#endregion 

		#region Initialization

		/// <summary>
		/// Initializes the members.
		/// </summary>
		private void initialize()
		{			
			// Get DataProvider from DbAccess component
			mProvider = DBSingleton.GetInstance().DataProvider;
			mCoWorkerID = mCoWorkerModel.CoWorkerId;
		}	

		#endregion

		#region Accessors 

		internal int PrecMedicalCount
		{
			get
			{
				return mPrecMedicalList.Count;
			}
		}

		#endregion

		#region Methods 

		/// <summary>
		/// Initialisies BO: gets data
		/// </summary>
		internal override void InitializeBO() 
		{
			mCoWorkerID = mCoWorkerModel.CoWorkerId;
			SelectAllPrecMeds();
			CopyIn();
		}

		/// <summary>
		/// Reads in all prec medicals assigned to current CWR
		/// Disable controls in GUI: at this point no medical is selected
		/// Fill Datagrid with already assigned medicals
		/// </summary>
		internal override void CopyIn() 
		{
			mCurrentPrecMed = null;	
			mViewCoWorker.DatSiMedValidUntil.Value = DateTime.Now;
			mViewCoWorker.CboSiMedPrecautionaryMedical.SelectedValue = 0;

			mViewCoWorker.DgrSiMedPrecautionaryMedical.DataSource = null;
			if ( mPrecMedicalList.Count > 0 ) 
			{
				mViewCoWorker.DgrSiMedPrecautionaryMedical.DataSource = mPrecMedicalList;
			} 
			mViewCoWorker.btnDelPrecMed.Enabled = false;
		}


		/// <summary>
		/// Creates a new PrecMedical BO to hold the newly assigned precautionary medical
		/// (get ID of prec med type from combobox)
		/// Prec Meds are always have status received (erteilt), Must therefore set mChanged to true so new precmed autom. saved
		/// </summary>
		internal void CreateNewPrecMed() 
		{
			
			decimal pPrecMedTypeID = -1;
			pPrecMedTypeID = Convert.ToDecimal(mViewCoWorker.CboSiMedPrecautionaryMedical.SelectedValue);

			if (pPrecMedTypeID > 0) 
			{
				foreach (PrecautionaryMedicalBriefing predMed in mPrecMedicalList) 
				{
					if (predMed.PrecMedTypeID == pPrecMedTypeID) 
					{
						SetChangedPrecMed(predMed.PrecMedID);
						return;
					}
				}
				mCurrentPrecMed = new PrecautionaryMedicalBriefing(mCoWorkerModel);
				mCurrentPrecMed.RegisterView(mViewCoWorker);
				mCurrentPrecMed.InitializeNew();
				mCurrentPrecMed.Changed = true;
			} 
			else 
			{
				mCurrentPrecMed  = null;
				mViewCoWorker.btnDelPrecMed.Enabled = false;
			}
		}


		/// <summary>
		/// Deletes currently selected precmedical in database and re-reads list of assigned prec medicals
		/// </summary>
		internal void DeleteCurrentPrecMed() 
		{
			decimal pPrecMedTypeID = -1;
			pPrecMedTypeID = Convert.ToDecimal(mViewCoWorker.CboSiMedPrecautionaryMedical.SelectedValue);

			if ( pPrecMedTypeID >  0 ) 
			{
				mCurrentPrecMed.Remove();
			} 
			else 
			{
				mCurrentPrecMed  = null;
				mViewCoWorker.btnDelPrecMed.Enabled = false;
			}
		}

		/// <summary>
		/// Forces current prec med BO to save itself to DB
		/// </summary>
		internal override void Save() 
		{
			if ( null != mCurrentPrecMed ) 
			{
				mCurrentPrecMed.Save();
			}
		}

		/// <summary>
		/// Re-reads the prec meds assigned to the current coworker
		/// </summary>
		internal void ReloadChangedData()
		{
			SelectAllPrecMeds();
			CopyIn();
			if ( null != mCurrentPrecMed ) 
			{
				SetChangedPrecMed(mCurrentPrecMed.PrecMedID);
			}
		}

		/// <summary>
		/// Current medical has been changed: get values from GUI
		/// </summary>
		/// <param name="pPrecMedID">ID of current medical</param>
		internal void SetChangedPrecMed(decimal pPrecMedID) 
		{
			foreach ( PrecautionaryMedicalBriefing predMed in mPrecMedicalList ) 
			{
				if ( predMed.PrecMedID == pPrecMedID ) 
				{
					mCurrentPrecMed = predMed;
					mCurrentPrecMed.IsInsert = false;
					predMed.CopyIn();
					mViewCoWorker.btnDelPrecMed.Enabled = true;
					break;
				}
			}
		}

		/// <summary>
		/// Validates currently selected <see cref="PrecautionaryMedicalBriefing"/> instance
		/// </summary>
		internal override void Validate()
		{
			if ( null != mCurrentPrecMed ) 
			{
				mCurrentPrecMed.Validate();
			}
		}

		/// <summary>
		/// Copies out data of currently selected <see cref="PrecautionaryMedicalBriefing"/> instance
		/// </summary>
		internal override void CopyOut() 
		{
			if ( null != mCurrentPrecMed ) 
			{
				mCurrentPrecMed.CopyOut();
			}
		}

		/// <summary>
		/// Reads all prec medicals (name, date etc) assigned to current coworker
		/// SQL select with DataReader in database
		/// </summary>
		private void SelectAllPrecMeds() 
		{
			try 
			{
				IDbCommand comm = null;
				PrecautionaryMedicalBriefing precMed;
				mPrecMedicalList = new ArrayList();

				IProvider provider = DBSingleton.GetInstance().DataProvider;

				if ( ! mCoWorkerModel.Mode.Equals( AllFPASSDialogs.CWR_DIALOG_MODE_ARCHIVE ) )
				{
					comm = provider.CreateCommand("SelectPrecautionaryMedical");
				} 
				else 
				{
					comm = provider.CreateCommand("SelectPrecautionaryMedicalArchive");
				}
				provider.SetParameter(comm, ":PMED_CWR_ID", mCoWorkerID );
				
				/// Open data reader to get Precmed data
				IDataReader mDR = provider.GetReader(comm);

				while (mDR.Read())
				{
					precMed = new PrecautionaryMedicalBriefing(mCoWorkerModel);

					/// User FK ID to save in database
					/// Nice name for display
					precMed.PrecMedID = Convert.ToDecimal( mDR["PMED_ID"] );
					precMed.UserID = Convert.ToDecimal( mDR["PMED_USER_ID"] );
					precMed.UserName = mDR["USERNICENAME"].ToString();
					try 
					{
						precMed.PrecMedDate = Convert.ToDateTime( mDR["PMED_EXECUTEDON"] );
					} 
					catch ( InvalidCastException ) 
					{
						precMed.PrecMedDate = DateTime.Now;	
					}
					
					precMed.TimeStamp = Convert.ToDateTime( mDR["PMED_TIMESTAMP"] );
					if ( mDR["PMED_INACTIVE_YN"].ToString().Equals("N") ) 
					{
						precMed.Received = true;
					}
					try 
					{
						precMed.ValidUntil = Convert.ToDateTime( mDR["PMED_VALIDUNTIL"] );
					} 
					catch ( InvalidCastException ) 
					{
						precMed.ValidUntil = DateTime.Now;	
					}
					precMed.Type = mDR["PMTY_TYPE"].ToString();
					precMed.Notation = mDR["PMTY_NOTATION"].ToString();
					precMed.NotationPlusType = precMed.Type +", " +  precMed.Notation;
					if ( mDR["PMED_PMTY_ID"].Equals(DBNull.Value ) ) 
					{
						precMed.PrecMedTypeID = 0;
					} 
					else 
					{
						precMed.PrecMedTypeID = Convert.ToDecimal( mDR["PMED_PMTY_ID"] );
					}

					precMed.RegisterView(mViewCoWorker);

					mPrecMedicalList.Add(precMed);
				}

				// Close reader
				mDR.Close();
			} 
			catch (DbAccessException dba)
			{
				throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR), dba);
			}	
			catch (OracleException oraex)
			{
				throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR) + oraex.Message, oraex);
			}
		}

		#endregion

	}
}

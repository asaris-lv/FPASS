using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

using de.pta.Component.DbAccess;
using de.pta.Component.Errorhandling;

using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.Util.Exceptions;
using Degussa.FPASS.Util.UserManagement;
using Degussa.FPASS.Db;
using Degussa.FPASS.FPASSApplication;

using Degussa.FPASS.Util.ListOfValues;

using Degussa.FPASS.Gui.Dialog.Pass;
using Degussa.FPASS.Util;

namespace Degussa.FPASS.Bo.Pass
{
	/// <summary>
	/// Represents a Plant breifing for a specific coworker in FPASS
	/// </summary>
	/// <remarks>
	/// <para><b>History</b></para>
	/// <div class="tablediv">
	/// <table class="dtTABLE" cellspacing="0">
	///		<tr>
	///			<th width="20%">PTA GmbH</th>
	///			<th width="20%">06.10.2003</th>
	///			<th width="60%">Remarks</th>
	///		</tr>
	/// </table>
	/// </div>
	/// </remarks>
	public class Plant : AbstractCoWorkerBO
	{
		#region Members

		private decimal mPlantID;		
		private decimal mPlantNameID;
		private string mPlantName;
		private decimal mCoWorkerID;
		private decimal mUserID;
		private string mUserName;
		private DateTime mPlantDate;
		private bool mPlantDirected;
		private bool mPlantReceived;
        private DateTime mValidUntil;
		private DateTime mTimeStamp;
		private decimal mPlantManagerID;
        private string mSource;

  
		private bool mInsert;

		private IDbCommand mUpdateComm;
		private IDbCommand mInsertComm;
		
		#endregion Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public Plant(CoWorkerModel pCoWorkerModel) : base (pCoWorkerModel)
		{
			initialize();
		}

		#endregion Constructors

		#region Initialization

		/// <summary>
		/// Initializes the members.
		/// </summary>
		private void initialize()
		{
			mCoWorkerID = mCoWorkerModel.CoWorkerId;
			mProvider = DBSingleton.GetInstance().DataProvider;
		}	

		#endregion Initialization

		#region Accessors 

		/// <summary>
		/// Gets or sets unique PK id of cwr-plant assignment record
		/// </summary>
		public decimal PlantID 
		{
			get 
			{
				return mPlantID;
			}
			set 
			{
				mPlantID = value;
			}
		}

        /// <summary>
        /// Gets or sets unique PK id of plant itself
        /// </summary>
        public decimal PlantNameID
        {
            get
            {
                return mPlantNameID;
            }
            set
            {
                mPlantNameID = value;
            }
        }

        /// <summary>
        /// Gets or sets "directed" status ("Angeordnet")
        /// </summary>
        public bool PlantDirected
        {
            get
            {
                return mPlantDirected;
            }
            set
            {
                mPlantDirected = value;
            }
        }

		/// <summary>
        /// Gets or sets "directed" status ("Angeordnet") as string
		/// </summary>
		public string DirectedAsString
		{
			get 
			{
				if ( mPlantDirected ) 
				{
					return "Angeordnet";
				} 
				else 
				{
					return "Widerrufen";
				}
			}
		}

        /// <summary>
        /// Gets or sets "received" status ("Erteilt")
        /// </summary>
        public bool PlantReceived
        {
            get
            {
                return mPlantReceived;
            }
            set
            {
                mPlantReceived = value;
            }
        }


        /// <summary>
        /// Gets or sets "received" status ("Erteilt") as string
        /// </summary>
        public string ReceivedAsString
        {
            get
            {
                if (mPlantReceived)
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
        /// Gets or sets unique PK id of coworker
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
        /// Gets or sets unique PK id of changeuser
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
        /// Gets or sets unique PK id of plant manager
		/// </summary>
		public decimal PlantManagerID 
		{
			get 
			{
				return mPlantManagerID;
			}
			set 
			{
				mPlantManagerID = value;
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
        /// Gets or sets plant-cwr assignment date
        /// </summary>
        public DateTime PlantDate
        {
            get
            {
                return mPlantDate;
            }
            set
            {
                mPlantDate = value;
            }
        }

        /// <summary>
        /// Gets or sets plant-cwr assignment date as string.
        /// Only interesting if plant assignment has status "received"
        /// </summary>
        public String PlantDateAsString
        {
            get
            {
                if (mPlantReceived)
                {
                    return mPlantDate.ToString().Substring(0, 10);
                }
                else
                {
                    return String.Empty;
                }
            }
        }

        /// <summary>
        /// Gets or sets plant-cwr assignment valid until date
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
        /// Gets or sets plant-cwr assignment valid until date as string.
        /// Only interesting if plant assignment has status "received"
        /// </summary>
        public String ValidUntilAsString
        {
            get
            {
                if (mPlantReceived)
                {
                    return mValidUntil.ToString().Substring(0, 10);
                }
                else
                {
                    return String.Empty;
                }
            }
        }

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public bool Insert 
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

		/// <summary>
		/// Gets or sets plant name
		/// </summary>
		public String PlantName 
		{
			get 
			{
				return mPlantName;
			}
			set 
			{
				mPlantName = value;
			}
		}

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public String UserName 
		{
			get 
			{
				if ( mPlantReceived ) 
				{
					return mUserName;
				} 
				else 
				{
					return String.Empty;
				}
			}
			set 
			{
				mUserName = value;
			}
		}


        /// <summary>
        /// Where this plant came from: FPASS or ZKS import
        /// </summary>
        public string Source
        {
            get { return mSource; }
            set { mSource = value; }
        } 
    

        /// <summary>
        /// Was this plant imported from ZKS?
        /// </summary>
        public bool ZKSImported
        {
            get { return (mSource == Globals.PLANT_SOURCE_ZKS); }          
        }


		#endregion Accessors

		#region Methods 


		/// <summary>
		/// Initializes this bo.
		/// </summary>
		internal override void InitializeBO()
		{
			mCoWorkerID = mCoWorkerModel.CoWorkerId;
			mPlantDate = DateTime.Now;
            mValidUntil = DateTime.Now;
			mPlantDirected = true;
			mPlantReceived = false;
			mInsert = true;
			mChanged = true;
			mUserName = String.Empty;
            mSource = Globals.PLANT_SOURCE_FPASS;
		}


		/// <summary>
		/// Copies data from GUI tab page plant (Betrieb) into BO.
        /// For plants this is done for each entry in datagrid as it is edited
		/// </summary>
		internal override void CopyOut() 
		{
            int dateCompared;

			// Only save user who granted plant
			if (mPlantReceived != mViewCoWorker.CbxPlBriefingDone.Checked ) 
			{
				mPlantReceived = mViewCoWorker.CbxPlBriefingDone.Checked;
				mChanged = true;
				mUserID = UserManagementControl.getInstance().CurrentUserID;
				mUserName = UserManagementControl.getInstance().CurrentUserNiceName;
			}

			if (mViewCoWorker.TxtPlPlantname.Text != mPlantName)
			{
				mChanged = true;	
			}
			
			dateCompared = mCoWorkerModel.CompareDates(mPlantDate, mViewCoWorker.DatPlBriefingDoneOn.Value);
			if (dateCompared != 0 ) 
			{
				mPlantDate = mViewCoWorker.DatPlBriefingDoneOn.Value;
				mChanged = true;
				mUserID = UserManagementControl.getInstance().CurrentUserID;
				mUserName = UserManagementControl.getInstance().CurrentUserNiceName;
			}

            dateCompared = mCoWorkerModel.CompareDates(mValidUntil, mViewCoWorker.DatPlBriefingValidUntil.Value);
            if (dateCompared != 0)
            {
                mValidUntil = mViewCoWorker.DatPlBriefingValidUntil.Value;
                mChanged = true;
                mUserID = UserManagementControl.getInstance().CurrentUserID;
                mUserName = UserManagementControl.getInstance().CurrentUserNiceName;
            }

			if (mPlantDirected && mInsert ) 
			{
				mChanged = true;
			}
		}

		/// <summary>
		/// Shows data of the bo in the gui in the tab page plant (Betrieb) 
		/// Data is "copied in the gui". It's copied in the edit fields where this plant
		/// can be changed by user.
		/// </summary>
        internal override void CopyIn()
        {
            mViewCoWorker.RbtPlBriefing.Checked = mPlantDirected;
            mViewCoWorker.CbxPlBriefingDone.Checked = mPlantReceived;
            mViewCoWorker.TxtPlBriefingDoneBy.Text = UserName;
            mViewCoWorker.TxtPlPlantname.Text = mPlantName;
            mViewCoWorker.DatPlBriefingDoneOn.Value = mPlantDate;
            mViewCoWorker.DatPlBriefingValidUntil.Value = mValidUntil;
            mViewCoWorker.ChkPlZKSImport.Checked = ZKSImported;

            bool isEditAllowed =
                (!ZKSImported)
                && (
                    mViewCoWorker.mPlantAuthorization
                    || mViewCoWorker.mSystemAdminAuthorization
                    || mViewCoWorker.mEdvAdminAuthorization);
           
            mViewCoWorker.DatPlBriefingDoneOn.Enabled = isEditAllowed;
            mViewCoWorker.CbxPlBriefingDone.Enabled = isEditAllowed;
            mViewCoWorker.DatPlBriefingValidUntil.Enabled = isEditAllowed;
        }

        /// <summary>
        /// Validates data in BO
        /// </summary>
		internal override void Validate()
		{
			if ( !mPlantReceived ) 
			{
				mUserName = String.Empty;
				PlantDate = DateTime.Now;
			}
		}

		/// <summary>
		/// Saves an plant to db. Knows whether to perform an insert
		/// or an update.
		/// DML statement is executed only when data has changed.
		/// <exception cref="de.pta.Component.Errorhandling.UIFatalException">
		/// Is thrown if dml statement failed.
		/// </exception>
		/// </summary>
		internal override void Save() 
		{
			if (mChanged) 
			{
				mCoWorkerModel.ZKSChanged = true;
                mTimeStamp = DateTime.Now;
				
                if (mInsert) 
				{
					InsertPlant();
				} 
				else  
				{
					UpdatePlant();
				}
				mChanged = false;
				mInsert = false;
			} 
		}

		/// <summary>
		/// Updates plant in db.
		/// <exception cref="de.pta.Component.Errorhandling.UIFatalException">
		/// Is thrown if dml statement failed.
		/// </exception>
		/// </summary>
		private void UpdatePlant() 
		{
			mUpdateComm = null;

			// Create the select command & fill Data Reader 
			mUpdateComm = mProvider.CreateCommand("UpdatePlants");

			mUpdateComm.Transaction = mCoWorkerModel.CurrentTransaction;
			mUpdateComm.Connection = mCoWorkerModel.CurrentTransaction.Connection;

			SetParameters(mUpdateComm);
			mUpdateComm.ExecuteNonQuery();
		}


		/// <summary>
		/// Inserts plant in db.
		/// <exception cref="de.pta.Component.Errorhandling.UIFatalException">
		/// Is thrown if dml statement failed.
		/// 28.04.04: Added UserID as removed in Save above
		/// </exception>
		/// </summary>
		private void InsertPlant() 
		{
			// Need this to save UserID first time a plant is saved
			mUserID = UserManagementControl.getInstance().CurrentUserID;
			
			mInsertComm = null;

			// Create the select command & fill Data Reader 
			mInsertComm = mProvider.CreateCommand("InsertPlants");

			mInsertComm.Transaction = mCoWorkerModel.CurrentTransaction;
			mInsertComm.Connection = mCoWorkerModel.CurrentTransaction.Connection;

			SetParameters(mInsertComm);
			mInsertComm.ExecuteNonQuery();
		}

		/// <summary>
		/// Sets values (parameters) in dml statement.
		/// </summary>
		/// <param name="pCommand">Command where paramters have to be set</param>
		private void SetParameters(IDbCommand pCommand) 
		{
			String directedYN = "N";
			String receivedYN = "Y";     
			
			mProvider.SetParameter(pCommand, ":CWPL_ID", mPlantID);
			mProvider.SetParameter(pCommand, ":CWPL_PL_ID", mPlantNameID );
			mProvider.SetParameter(pCommand, ":CWPL_CWR_ID", mCoWorkerID);
			mProvider.SetParameter(pCommand, ":CWPL_TIMESTAMP", mTimeStamp);
			mProvider.SetParameter(pCommand, ":CWPL_USER_ID", mUserID);

            // Set Plant Date and Valid Until
            if (mPlantReceived)
            {
                mProvider.SetParameter(pCommand, ":CWPL_PLANTDATE", mPlantDate);
                mProvider.SetParameter(pCommand, ":CWPL_VALIDUNTIL", mValidUntil); 
            }
            else
            {
                mProvider.SetParameter(pCommand, ":CWPL_PLANTDATE", DBNull.Value);
                mProvider.SetParameter(pCommand, ":CWPL_VALIDUNTIL", DBNull.Value); 
            }

            directedYN = (mPlantDirected ? "Y" : "N");
            mProvider.SetParameter(pCommand, ":CWPL_PLANT_YN", directedYN);

            // note: inactive No means plant has been received
            receivedYN = (mPlantReceived ? "N" : "Y");
            mProvider.SetParameter(pCommand, ":CWPL_INACTIVE_YN", receivedYN); 
		}

		#endregion Methods

	}
}

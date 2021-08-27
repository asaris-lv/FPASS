using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

using de.pta.Component.DbAccess;
using de.pta.Component.Errorhandling;

using Degussa.FPASS.Util.Messages; 
using Degussa.FPASS.Util.Exceptions;
using Degussa.FPASS.Util.UserManagement;
using Degussa.FPASS.Util.ListOfValues;
using Degussa.FPASS.Db;
using Degussa.FPASS.FPASSApplication;

using Degussa.FPASS.Gui.Dialog.Pass;

namespace Degussa.FPASS.Bo.Pass
{
	/// <summary>
	/// Holds all plants of this coworker. Acts as a container for plants.
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
	public class Plants : AbstractCoWorkerBO
	{
		#region Members

		/// <summary>
		/// holds all plants
		/// </summary>
		private Hashtable mAllPlants;

		/// <summary>
		/// id (pk) of the current coworker
		/// </summary>
		private decimal mCoWorkerID;

		/// <summary>
		/// used to hold plants
		/// </summary>
		private DataTable mTable;

        /// <summary>
        /// used to display plants in the grid
        /// </summary>
        private DataView mDataView;
		
		/// <summary>
		/// plant which is currently edited by user
		/// </summary>
		private Plant mCurrentPlant;
		
		/// <summary>
		/// constant for select of plants
		/// </summary>
		private const String SELECT_PLANTS = "SelectPlants";
		
		/// <summary>
		/// constant for select plants from archive
		/// </summary>
        private const String SELECT_PLANTS_ARCHIVE = "SelectPlantsArchive";
		
		/// <summary>
		/// constant for deleting a plant
		/// </summary>
        private const String DEL_CWR_PLANT = "DeletePlants";
		

		#endregion Members

		#region Constructors

		/// <summary>
		/// Simple constructor. Sets reference to the model.
		/// </summary>
		public Plants(CoWorkerModel pCoWorkerModel) : base (pCoWorkerModel)
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
			mAllPlants = new Hashtable();
		}	

		#endregion Initialization

		#region Accessors 

		#endregion Accessors

		#region Methods 


		/// <summary>
		/// Initiliazes this bo. All directed plants are red from db.
		/// </summary>
		internal override void InitializeBO() 
		{
			mCurrentPlant = null;
			ClearInputFields();
			mAllPlants = new Hashtable();
			SelectAllPlants();
		}


		/// <summary>
		/// Data of all plant is copied in the gui. Fills the checked listbox as well as the grid.
		/// </summary>
		internal override void CopyIn() 
		{
            mViewCoWorker.DgrPlPlant.DataSource = mDataView; 
			CopyInLikBox();
			SetAuthorization();
		}


		/// <summary>
		/// Changes plants as a reaction to a click in the checked list box on tab
		/// page reception. If a plant was directed it's added. If a direction is revoked the
		/// corresponding plant is updated.
		/// </summary>
		internal void ChangeInPlants() 
		{
			Plant plant;

			String plantName = mViewCoWorker.LiKCoPlant.SelectedItem.ToString();

			plant = FindPlantByName(plantName);  // findet plant in bereits bearbeiteten plants

			if ( null != plant )
			{
				UpdatePlant(plant);
				plant.Changed = true;
				if ( plant.Equals(mCurrentPlant) ) 
				{
					mCurrentPlant.CopyIn();
					mCurrentPlant.Changed = true;
				}
			} 
			else 
			{
				AddNewPlant(plantName);
			}
			SetAuthorization();
		}

		
		/// <summary>
		/// Copies plant which was selected in the grid from the grid into the editing fields.
		/// Plant which was edited before is copied out and changes are shown in the grid.
		/// Checks plant manager access rights and shows message if user is not allowd to edit this plant.
		/// 27.04.04: If plant is first removed in listbox on coordinator's tab (directed set to false) and is then selected and  
		///			status "received" is revoked, plant stays in the arraylist => nullpointer on selecting it
		///			If plant is not directed but is "Received", throw exception when trying to select it in grid
		///	24.06.04: took out exception thrown when user is not plant manager: show message in status bar
		///	background is that coordinators are allowed to viere plants in datagrid
		/// </summary>
		/// <param name="pPlantName"></param>
		internal void StartEditPlant(String pPlantName) 
		{
			mCoWorkerModel.ClearStatusBar();
			
			if ( null != mCurrentPlant ) 
			{
				mCurrentPlant.CopyOut();
				UpdateRow(FindRowInTable(mCurrentPlant), mCurrentPlant);
			}

			Plant pl = FindPlantByName(pPlantName);
			
			if (!pl.PlantDirected && pl.PlantReceived)
			{
                // Error if a non-directed plant is selected (still shown in list as it has status "received")
				mCurrentPlant = null;
				ClearInputFields();
				throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.INVALID_EDIT_PLANT));
			}
            else if (pl.ZKSImported)
            {
                // The plant was imported from ZKS, so briefings are read-only in FPASS.
                pl.CopyIn();
                mCurrentPlant = pl;
                mCoWorkerModel.ShowMessageInStatusBar(MessageSingleton.GetInstance().GetMessage(MessageSingleton.INVALID_ZKS_PLANT));
            }
			else if (CheckPlantManagerAccess(pl)) 
			{
                // Plant manager can only edit plants he is assigned to
				pl.CopyIn();
				mCurrentPlant = pl;
			}
			else 
			{
                // Plant cannot be edited as current user is not plant manager for this plant.
				mCurrentPlant = null;
				ClearInputFields();
				mCoWorkerModel.ShowMessageInStatusBar(MessageSingleton.GetInstance().GetMessage(MessageSingleton.INVALID_PLANTMANAGER));
			}
		}


		/// <summary>
		/// Saves all changed plants in db.
		/// </summary>
		internal override void Save() 
		{
			foreach (Plant pl in mAllPlants.Values) 
			{
				pl.Save();
			}
			mCurrentPlant = null;
			ClearInputFields();
		}


		/// <summary>
		/// Validates all plants.
		/// </summary>
		internal override void Validate()
		{
			foreach (Plant pl in mAllPlants.Values) 
			{
				pl.Validate();
			}
		}

		/// <summary>
		/// Data is "copied out of the gui" in tab page plant 
		/// </summary>
		internal override void CopyOut() 
		{
			if ( null != mCurrentPlant ) 
			{
				mCurrentPlant.CopyOut();
				UpdateRow (FindRowInTable(mCurrentPlant), mCurrentPlant );
			}

			// is done here to check changes , this is always done by calling copyout
			this.mChanged = false;
			foreach (Plant pl in mAllPlants.Values) 
			{
				if (pl.Changed ) 
				{
					this.mChanged = true;
					return;
				}
			}
		}

        /// <summary>
        /// Assigns all available plants in one go. 
        /// If plant is already in list then make sure it is directed ("angeordnet")
        /// </summary>
        internal void AssignAllPlants()
        {
            string[] availablePlants = FPASSLovsSingleton.GetInstance().GetPlants();      
            IEnumerator enumerator = availablePlants.GetEnumerator();

			while (enumerator.MoveNext()) 
			{
                string plName = (string)enumerator.Current;

                if (!mAllPlants.ContainsKey(plName))
                {
                    AddNewPlant(plName);
                }
            }

            foreach (Plant pl in mAllPlants.Values)
            {
                if (!pl.PlantDirected)
                {
                    pl.PlantDirected = true;
                    pl.Changed = true;
                    UpdateRow(FindRowInTable(pl), pl);
                }
            }

            CopyInLikBox();
            SetAuthorization();
        }

		/// <summary>
		/// If a plant is assigned but not received it must be removed from list for current coworker
		/// rather than set to status "REVOKED".
		/// Also record must be removed from database, PK of assignment record (not PK of plant) is used.
		/// Standard approach: use open connection of dummy datareader
		/// </summary>
		/// <param name="pPlant"></param>
		private void RemovePlant(Plant pPlant) 
		{
			// Remove plant from display table and arraylist
			mAllPlants.Remove(pPlant.PlantName);
			mTable.Rows.Remove(FindRowInTable(pPlant));
			mTable.AcceptChanges();

			// remove assigned plant from DB table
			IDbCommand dummyCommand = mProvider.CreateCommand("SequenceDummy");
			IDbCommand delPlCommand = mProvider.CreateCommand(DEL_CWR_PLANT);

			mProvider.SetParameter(delPlCommand, ":CWPL_ID", pPlant.PlantID);
			IDataReader dummyReader = mProvider.GetReader(dummyCommand);
			delPlCommand.Connection = dummyCommand.Connection;
			int ret = delPlCommand.ExecuteNonQuery();

			dummyReader.Close();

			if ( null != mCurrentPlant )   // Betrieb in der Bearbeitungszeile
			{
				if ( mCurrentPlant.PlantName.Equals(pPlant.PlantName ) )
				{
					mCurrentPlant = null;
					ClearInputFields();
				}
			}
		}


		/// <summary>
		/// A new plant is directed and is added to the plants list and the data table and 
		/// is displayed in the grid on tab page plants.
		/// </summary>
		/// <param name="pPlantName">name of the plant which is added.</param>
		private void AddNewPlant(String pPlantName) 
		{
			Plant plant = new Plant(mCoWorkerModel);
			plant.RegisterView(mViewCoWorker);
			plant.InitializeBO();
			plant.PlantName = pPlantName;
			plant.PlantNameID = FPASSLovsSingleton.GetInstance().GetPlantId(pPlantName);
			plant.PlantID = mCoWorkerModel.GetNextValFromSeq("SEQ_CWRPLANT");
			mAllPlants.Add(plant.PlantName, plant);
			AddNewRow(plant);
		}


		/// <summary>
		/// Checks if current user is allwod to edit this plant.
		/// </summary>
		/// <param name="pPlantToCheck">plant to check</param>
		/// <returns>true if current user can edit this plant, false otherwise</returns>
		private bool CheckPlantManagerAccess(Plant pPlantToCheck) 
		{
			if ( UserManagementControl.getInstance().CurrentUserIsInRole(UserManagementControl.ROLE_EDVADMIN) 
				|| UserManagementControl.getInstance().CurrentUserIsInRole(UserManagementControl.ROLE_VERWALTUNG) ) 
			{
                // a user with one of these roles can do whatever he wants
				return true; 
			} 
			else 
			{
				if ( UserManagementControl.getInstance().CurrentUserIsInRole(UserManagementControl.ROLE_BETRIEBSMEISTER) ) 
				{
					return FPASSLovsSingleton.GetInstance().PlantPlantMangerAreAssigned(
						pPlantToCheck.PlantNameID, UserManagementControl.getInstance().
						CurrentUserID );
				} 
				else 
				{
                    // no right to edit plants when you are not a plantmanager
					return false;
				}
			}
		}

		/// <summary>
		/// Changes direction state of the given plant. (If a plant was directed, direction is revoked
		/// or vice versa) and updates or removes plant assignment
		/// </summary>
		/// <param name="pPlant"></param>
		private void UpdatePlant(Plant pPlant) 
		{
            pPlant.PlantDirected = !pPlant.PlantDirected;

			if ( !pPlant.PlantDirected && !pPlant.PlantReceived ) 
			{
				// löschen wenn nicht mehr angeordnet und noch nicht erteilt
                RemovePlant(pPlant); 
			} 
			else 
			{
				UpdateRow(FindRowInTable(pPlant), pPlant);
			}
		}


		/// <summary>
		/// Copies data data from plant in the corresponding datarow.
		/// </summary>
		/// <param name="pDataRow">corresponding row</param>
		/// <param name="pPlant">plant which is copied</param>
		private void UpdateRow(DataRow pDataRow, Plant pPlant) 
		{
			pDataRow.BeginEdit();
			pDataRow["PlantID"]  = pPlant.PlantID;
			pDataRow["PlantName"] = pPlant.PlantName;
			pDataRow["PlantDateAsString"] = pPlant.PlantDateAsString;
			pDataRow["UserName"] = pPlant.UserName;
            pDataRow["ValidUntilAsString"] = pPlant.ValidUntilAsString;
			pDataRow["ReceivedAsString"] = pPlant.ReceivedAsString;
			pDataRow["DirectedAsString"] = pPlant.DirectedAsString;

			pDataRow.EndEdit();
		}


		/// <summary>
		/// Gets data row of the given plant
		/// </summary>
		/// <param name="pPlant">plabnt itsself</param>
		/// <returns>requested datarow</returns>
		private DataRow FindRowInTable(Plant pPlant) 
		{
			return mTable.Rows.Find(pPlant.PlantID);
		}


		/// <summary>
		/// Gets plant by name
		/// </summary>
		/// <param name="pPlantName">name of the plant</param>
		/// <returns>rewuested plant</returns>
		private Plant FindPlantByName(String pPlantName) 
		{
			return (Plant) mAllPlants[pPlantName];
		}

		/// <summary>
        /// Fills datatable and initinitailies dataview which is displayed in the grid
		/// </summary>
		private void InitializeDataTable() 
		{
			mTable = new DataTable("Plants");
			
			mTable.Columns.Add(new DataColumn("PlantID"));
			mTable.Columns.Add(new DataColumn("PlantName"));
            mTable.Columns.Add(new DataColumn("PlantDateAsString"));
			mTable.Columns.Add(new DataColumn("UserName"));
            mTable.Columns.Add(new DataColumn("PlantSource"));
            mTable.Columns.Add(new DataColumn("ValidUntilAsString"));
			mTable.Columns.Add(new DataColumn("ReceivedAsString"));
			mTable.Columns.Add(new DataColumn("DirectedAsString"));

			mTable.PrimaryKey = new DataColumn[] {mTable.Columns["PlantID"]};
			
			IDictionaryEnumerator enumerator = mAllPlants.GetEnumerator();
			while (enumerator.MoveNext())
			{
				AddNewRow((Plant)enumerator.Value);
			}

            mDataView = new DataView(mTable);
            mDataView.Sort = "PlantName";
		}


		/// <summary>
		/// Adds a directed plant to the dataTable to display it in the grid.
		/// </summary>
		/// <param name="pPlant">plant to add the datatable</param>
		private void AddNewRow(Plant pPlant) 
		{
			DataRow row	= mTable.NewRow();
			row.ItemArray = new object[8] {
											  pPlant.PlantID,
											  pPlant.PlantName,
											  pPlant.PlantDateAsString,
											  pPlant.UserName,
                                              pPlant.Source,
                                              pPlant.ValidUntilAsString,
											  pPlant.ReceivedAsString,
											  pPlant.DirectedAsString,
			};
			mTable.Rows.Add(row);
		}



		/// <summary>
		/// Clears input fields on tab page plant
		/// </summary>
		private void ClearInputFields() 
		{
			mViewCoWorker.TxtPlPlantname.Text = String.Empty;
			mViewCoWorker.DatPlBriefingDoneOn.Value = DateTime.Now;
			mViewCoWorker.TxtPlBriefingDoneBy.Text = String.Empty;
			mViewCoWorker.RbtPlBriefing.Enabled = false;
			mViewCoWorker.DatPlBriefingDoneOn.Enabled = false;
			mViewCoWorker.CbxPlBriefingDone.Enabled = false;
			mViewCoWorker.CbxPlBriefingDone.Checked = false;
			mViewCoWorker.RbtPlBriefing.Checked = false;
            mViewCoWorker.DatPlBriefingValidUntil.Enabled = false;
            mViewCoWorker.ChkPlZKSImport.Checked = false;
            mViewCoWorker.ChkPlZKSImport.Enabled = false;
		}


		/// <summary>
		/// Enables or disables user input on tab page eplant plant. User input is allowd
		/// if user has role edvadmin, sysadmin, or plant manager.
		/// Also enables datagrid for coordinator so he can scroll thru (CoordinatorAuthorization)
		/// </summary>
		private void SetAuthorization() 
		{
            bool isAllowed = 
                    mViewCoWorker.mPlantAuthorization
                    || mViewCoWorker.mCoordinatorAuthorization
                    || mViewCoWorker.mSystemAdminAuthorization
                    || mViewCoWorker.mEdvAdminAuthorization;

            mViewCoWorker.DgrPlPlant.Enabled = isAllowed && (mAllPlants.Count > 0);	
		}


		/// <summary>
		/// Checks all directed plants for this coworker in the checked list
		/// box on tab page coordinator.
		/// </summary>
		private void CopyInLikBox() 
		{
			IDictionaryEnumerator enumerator = mAllPlants.GetEnumerator();
			while ( enumerator.MoveNext() ) 
			{
				Plant pl = (Plant)enumerator.Value;
				if (pl.PlantDirected) 
				{
					int pos = GetPosInLikBox(pl);
					if (pos != -1) 
					{
						mViewCoWorker.LiKCoPlant.SetItemChecked(pos, true);
					}	
				}
			}
		}

		/// <summary>
		/// Returns index of plant in checked list box on tab page coordinator
		/// </summary>
		/// <param name="pPlant"></param>
		/// <returns>index of the given plant</returns>
		private int GetPosInLikBox(Plant pPlant) 
		{
			return mViewCoWorker.LiKCoPlant.Items.IndexOf(pPlant.PlantName);
		}

		/// <summary>
		/// Loads all assigned and directed plants for current coworker from database
		/// </summary>
		private void SelectAllPlants() 
		{
			IDbCommand comm = null;
			Plant plant;
			IProvider provider = DBSingleton.GetInstance().DataProvider;

			// Create the select command & fill Data Reader 
			if (!mCoWorkerModel.Mode.Equals(AllFPASSDialogs.CWR_DIALOG_MODE_ARCHIVE))
			{
				comm = provider.CreateCommand(SELECT_PLANTS);
			} 
			else 
			{
				comm = provider.CreateCommand(SELECT_PLANTS_ARCHIVE);
			}
			provider.SetParameter(comm, ":CWPL_CWR_ID", mCoWorkerID);
			
			IDataReader mDR = provider.GetReader(comm);

			while (mDR.Read())
			{
				plant = new Plant(mCoWorkerModel);

                plant.PlantDirected = (mDR["CWPL_PLANT_YN"].ToString() == "Y");
				plant.PlantID =	Convert.ToDecimal(mDR["CWPL_ID"]);
				plant.PlantNameID = Convert.ToDecimal(mDR["CWPL_PL_ID"]);
				plant.UserID = Convert.ToDecimal(mDR["CWPL_USER_ID"]);
                plant.PlantName = mDR["PL_NAME"].ToString();

                if (!mCoWorkerModel.Mode.Equals(AllFPASSDialogs.CWR_DIALOG_MODE_ARCHIVE))
                {
                    plant.Source = mDR["PL_SOURCE"].ToString();
                }
            
                if (mDR["CWPL_INACTIVE_YN"].ToString().Equals("N")) 
				{
					plant.PlantReceived = true;
                    try
                    { plant.PlantDate = Convert.ToDateTime(mDR["CWPL_PLANTDATE"]); }
                    catch
                    { }
                    plant.UserName = mDR["USERNICENAME"].ToString();
                    try
                    { plant.ValidUntil = Convert.ToDateTime(mDR["CWPL_VALIDUNTIL"]); }
                    catch
                    { plant.ValidUntil = DateTime.Now; }
				} 
				else 
				{
					plant.PlantReceived = false;
                    plant.PlantDate = DateTime.Now;
                    plant.UserName = String.Empty;
                    plant.ValidUntil = DateTime.Now;
				}
                
				plant.Insert = false;
				plant.RegisterView(mViewCoWorker);
				mAllPlants.Add(plant.PlantName, plant);
			}
			mDR.Close();

			InitializeDataTable();
		}

		#endregion Methods
	}
}

using System;
using System.Collections;
using System.Data;
using de.pta.Component.DbAccess;
using de.pta.Component.ListOfValues;
using Degussa.FPASS.Db;
using Degussa.FPASS.Util.UserManagement;

namespace Degussa.FPASS.Util.ListOfValues
{
	/// <summary>
	/// Holds and reads data for comboboxes and provides access to this data.
	/// These are lists that cannot be filled using the standard ListOfValues pta component.
	/// Class uses typified list-of-value items for FPASS
	/// </summary>
	/// <remarks>
	/// <para><b>History</b></para>
	/// <div class="tablediv">
	/// <table class="dtTABLE" cellspacing="0">
	///		<tr>
	///			<th width="20%">Author</th>
	///			<th width="20%">Date</th>
	///			<th width="60%">Remarks</th>
	///		</tr>
	///		<tr>
	///			<td width="20%">PTA GmbH</td>
	///			<td width="20%">2003-2008</td>
	///			<td width="60%">Comment</td>
	///		</tr>
	/// </table>
	/// </div>
	/// </remarks>
	public class FPASSLovsSingleton
	{
		#region Members

		/// <summary>  
		/// Used to hold the unique instance of FPASSLovsSingleton
		///</summary>
		private static FPASSLovsSingleton mInstance = null;

		/// <summary>
		/// Holds all craftLovItems <see cref="Degussa.FPASS.Util.ListOfValues">
		/// <code>CraftLovItem</code></see>
		/// </summary>
		private ArrayList	 mArlCrafts;
		/// <summary>
		/// Holds all craftLovItems <see cref="Degussa.FPASS.Util.ListOfValues">
		/// <code>CraftLovItem</code></see>
		/// </summary>
		private Hashtable	 mHttCrafts;

		/// <summary>
		/// Holds all plants. PlantId is used as key, plantname as value;
		/// </summary>
		private	Hashtable	 mPlants;

		/// <summary>
		/// Holds all plant-plantmanager assigmnents. plant id plus plantmanager id is used as key, an empty 
		/// String as value. Is used  to check if an assigment already exists.
		/// </summary>
		private	Hashtable	 mPlantManagers;
		/// <summary>
		/// Holds all assigmnents <see cref="Degussa.FPASS.Util.ListOfValues">
		/// <code>Assignment</code></see>
		/// </summary>
		private ArrayList	 mAssignments;
		/// <summary>
		/// Holds all coordinator-contractor assigmnents. Coordinator id plus contractor id is used as key, an empty 
		/// String as value. Is used  to check if an assigment already exists. Accees via 
		/// hashtable is faster than iteration over mAssignments
		/// </summary>
		private	Hashtable	 mCoordCons;
		/// <summary>
		/// Holds all ContractorLovItems<see cref="Degussa.FPASS.Util.ListOfValues">
		/// <code>CoordLovItem</code></see>, representing active contractors . 
		/// Id of a contractor is used as key, object itself as value.
		/// </summary>
		private Hashtable    mContractors;

		/// <summary>
		/// Holds all ContractorLovItems<see cref="Degussa.FPASS.Util.ListOfValues">
		/// <code>CoordLovItem</code></see>, representing invalid external contractors 
		/// Id of a contractor is used as key, object itself as value.
		/// </summary>
		private Hashtable    mInvalidContractors;
		/// <summary>
		/// Stores ContractorLovItems<see cref="Degussa.FPASS.Util.ListOfValues">
		/// representing invalid external contractors imported wih cwr booking data (dynamic data)
		/// </summary>
		private Hashtable    mNonFPASSContractors;
		/// <summary>
		/// Holds all CoordLovItems<see cref="Degussa.FPASS.Util.ListOfValues">
		/// <code>CoordLovItem</code></see>, representing active coordinators. 
		/// Id of a coordinator is used as key, object itsself as value.
		/// </summary>
		private Hashtable    mCoordinators;

		/// <summary>
		///  Holds all ContractorLovItems<see cref="Degussa.FPASS.Util.ListOfValues">
		/// <code>CoordLovItem</code></see>, representing inactive coordinators from archive. 
		/// </summary>
		private ArrayList    mContractorsArchive;


		/// Holds all CoordLovItems<see cref="Degussa.FPASS.Util.ListOfValues">
		/// <code>CoordLovItem</code></see>, representing active coordinators. 
		private ArrayList	mAllCoordinators;

		/// <summary>
		/// Holds all SupervisorLovItems<see cref="Degussa.FPASS.Util.ListOfValues">
		/// <code>SupervisorLovItem</code></see>, representing inactive and active supervisors. 
		/// </summary>
		private ArrayList    mAllSupervisors;

		/// <summary>
		/// used to hold the unique mandator id of this session
		/// </summary>
		private int mCurrentMandID;


		/// <summary>
		/// Instance of inner class ContractorComparer. Allows sorting of contractors by name
		/// </summary>
		private ContractorComparer  mContractorComparer;

		/// <summary>
		/// Instance of inner class CoordinatorComparer. Allows sorting of coordinators by name
		/// </summary>
		private CoordinatorComparer mCoordinatorComparer;
		

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public FPASSLovsSingleton()
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
			mCoordinatorComparer = new CoordinatorComparer();
			mContractorComparer	 = new ContractorComparer();
			mCurrentMandID  = UserManagementControl.getInstance().CurrentMandatorID;
			
			mAssignments		= new ArrayList();
			mContractors		= new Hashtable();
			mCoordinators		= new Hashtable();
			mInvalidContractors = new Hashtable();
			mNonFPASSContractors = new Hashtable();
			mCoordCons			= new Hashtable();
			mContractorsArchive = new ArrayList();
			mAllCoordinators	= new ArrayList();
			mAllSupervisors		= new ArrayList();

			LoadCraftLovItems();
			LoadPlants();
			LoadPlantManagers();
			LoadAssignments();
			LoadContractors();
			LoadInvalidContractors();
			LoadNonFPASSContractors();
			LoadActiveCoordinators();
			LoadAllCoordinators();
			LoadArchiveContractors();
			LoadAllSupervisors();
		}	

		#endregion //End of Initialization

		#region Accessors 

		internal ArrayList ContractorsArchive 
		{
			get 
			{
				return mContractorsArchive;
			}
		}

		internal ArrayList AllCoordinators
		{
			get 
			{
				return mAllCoordinators;
			}
		}


		internal ArrayList GetArrayListCrafts
		{
			get 
			{
				return mArlCrafts;
			}
		}

		internal ArrayList CraftNotations
		{
			get 
			{
				ArrayList craftNotations = new ArrayList();
				craftNotations.AddRange(mArlCrafts);
				craftNotations.Sort(new CraftNotationComparer());
				return craftNotations;
			}
		}

		internal ArrayList CraftNumbers
		{
			get 
			{
				ArrayList craftNumbers = new ArrayList();
				craftNumbers.AddRange(mArlCrafts);
				craftNumbers.Sort(new CraftNumberComparer());
				return craftNumbers;
			}
		}

		internal ArrayList AllSupervisors
		{
			get 
			{
				return mAllSupervisors;
			}
		}

		/// <summary>
		/// Returns ContractorLovItems
		/// representing invalid external contractors imported wih cwr booking data (dynamic data)
		/// </summary>
		internal Hashtable NonFPASSContractors
		{
			get { return mNonFPASSContractors; }
		}

		#endregion

		#region Methods 


		/// <summary>
		/// Returns the one and only instance of this class.
		/// </summary>
		/// <returns>instance of FPASSLovsSingleton</returns>
		public static FPASSLovsSingleton GetInstance()
		{
			if(null == mInstance)
			{
				mInstance = new FPASSLovsSingleton();
			}
			return mInstance;
		}

		/// <summary>
		/// Verifies if the given coordinator is assigned to the given contractor
		/// </summary>
		/// <param name="pCoordID">id of the requested coordinator</param>
		/// <param name="pContractorID">id of the requested contractor</param>
		/// <returns>true if assignments exists, false otherwise</returns>
		internal bool CoordIsAssignedContractor(decimal pCoordID, decimal pContractorID) 
		{
			return mCoordCons.ContainsKey(pCoordID.ToString() + pContractorID.ToString() ) ;
		}


		/// <summary>
		/// Verifies if the given plantmanager is assigned to the given plant
		/// </summary>
		/// <returns>true if assignments exists, false otherwise</returns>
		internal bool PlantPlantMangerAreAssigned(decimal pPlantNameID, decimal pPlantManagerID) 
		{
			return mPlantManagers.ContainsKey(pPlantNameID.ToString().Trim() + 
				pPlantManagerID.ToString().Trim() ) ;
		}

		/// <summary>
		/// reads all up-to-date data from db
		/// </summary>
		internal void ReFill() 
		{
			LoadAssignments();
			LoadActiveCoordinators();
			LoadContractors();
			LoadInvalidContractors();
			LoadCraftLovItems();
			LoadPlants();
			LoadPlantManagers();
		}

		/// <summary>
		/// gets id (pk) for the given plant name
		/// </summary>
		/// <param name="pPlantName">name of the requested plant</param>
		/// <returns>id (pk) for the given plant name</returns>
		internal decimal GetPlantId(String pPlantName) 
		{
			return Convert.ToDecimal( mPlants[pPlantName] );
		}

		
		/// <summary>
		/// Gets names of all plants
		/// </summary>
		/// <returns>names of all plants</returns>
		internal String[] GetPlants() 
		{
			ArrayList plants = new ArrayList(mPlants.Keys);
			plants.Sort(new PlantsComparer());

			String[] plantArray = new String[plants.Count];
			plants.CopyTo(plantArray);

			return plantArray;
		}


		/// <summary>
		/// Gets a list of all valid and invalid contractors
		/// Only active contractors are assigned to a coordinator
		/// </summary>
		/// <param name="pCoordinatorID">id (pk) of the coordinator</param>
		/// <returns>List containing all ContractorLovItems which are assigned to the given
		/// coordinator</returns>
		internal ArrayList GetValInvalContractors(decimal pCoordinatorID) 
		{ 
			ArrayList results = new ArrayList();

			if ( 0 == pCoordinatorID ) 
			{
				results.AddRange( mContractors.Values );
				foreach (object obj in mInvalidContractors.Values )
				{
					results.Add( (ContractorLovItem)obj );
				}
			} 
			else 
			{
				foreach ( Assignment assign in mAssignments ) 
				{
					if ( assign.CoordinatorID == pCoordinatorID ) 
					{
						if ( null != mContractors[assign.ContractorID] ) 
						{
							results.Add( mContractors[assign.ContractorID] );
						}
					}
				}
			}
			
			ContractorLovItem contractorLovItem = new ContractorLovItem();
			contractorLovItem.ContractorID = 0;
			contractorLovItem.ContractorName =  "";
			results.Add( contractorLovItem );	
			results.Sort(mContractorComparer);

			return results;
		}

		/// <summary>
		/// Gets all contractors which are assigned to the given
		/// coordinator
		/// </summary>
		/// <param name="pCoordinatorID">id (pk) of the coordinator</param>
		/// <returns>List containing all ContractorLovItems which are assigned to the given
		/// coordinator</returns>
		internal ArrayList GetContractors(decimal pCoordinatorID) 
		{ 
			ArrayList		results = new ArrayList();

			if ( 0 == pCoordinatorID ) 
			{
				results.AddRange( mContractors.Values );
			} 
			else 
			{
				foreach ( Assignment assign in mAssignments ) 
				{
					if ( assign.CoordinatorID == pCoordinatorID ) 
					{
						if ( null != mContractors[assign.ContractorID] ) 
						{
							results.Add( mContractors[assign.ContractorID] );
						}
					}
				}
			}
			
			ContractorLovItem contractorLovItem = new ContractorLovItem();
			contractorLovItem.ContractorID = 0;
			contractorLovItem.ContractorName =  "";
			results.Add( contractorLovItem );	
			results.Sort(mContractorComparer);

			return results;
		}


		/// <summary>
		/// Gets all coordinators which are assigned to the given
		/// contractor
		/// </summary>
		/// <param name="pContractorID">id (pk) of the contractor</param>
		/// <returns>List containing all CoordLovItems which are assigned to the given
		/// contractor</returns>
		internal ArrayList GetCoordinators(decimal pContractorID) 
		{ 
			ArrayList		results =  new ArrayList();

			if ( 0 == pContractorID ) 
			{
				results.AddRange( mCoordinators.Values );
			} 
			else 
			{
				foreach ( Assignment assign in mAssignments ) 
				{
					if ( assign.ContractorID == pContractorID ) 
					{
						if ( null != mCoordinators[assign.CoordinatorID] ) 
						{
							results.Add( mCoordinators[assign.CoordinatorID] );
						}
					}
				}
			}
			CoordLovItem coordLovItem = new CoordLovItem();
			coordLovItem.CoordID = 0;
			coordLovItem.CoordFullNameTel =  "";
			results.Add(coordLovItem );
			results.Sort(mCoordinatorComparer);

			return results;
		}


		/// <summary>
		/// Reads all craft data from db 
		/// </summary>
		private void LoadCraftLovItems()
		{
			CraftLovItem mCraftLovItem;
			mHttCrafts = new Hashtable();
			
			// Get provider
			IProvider mProvider = DBSingleton.GetInstance().DataProvider;

			// Create the select command & fill Data Reader 
			IDbCommand selComm = mProvider.CreateCommand("CraftsList");
			mProvider.SetParameter(selComm, ":CRA_MND_ID", mCurrentMandID);

			IDataReader mDR = mProvider.GetReader(selComm);
			
			while (mDR.Read()) 
			{
				mCraftLovItem = new CraftLovItem();
				
				mCraftLovItem.CraftID		= Convert.ToDecimal(mDR["CRA_ID"]);
				mCraftLovItem.CraftNumber	= Convert.ToString(mDR["CRA_CRAFTNO"]);
				mCraftLovItem.CraftNotation = Convert.ToString(mDR["CRA_CRAFTNOTATION"]);
				
				mHttCrafts.Add(mCraftLovItem.CraftID, mCraftLovItem);
			}
			mDR.Close();

			mCraftLovItem = new CraftLovItem();
			mCraftLovItem.CraftID = 0;
			mCraftLovItem.CraftNotation = "";
			mCraftLovItem.CraftNumber = "";

			mArlCrafts = new ArrayList();
			mArlCrafts.Add(mCraftLovItem);

			mArlCrafts.AddRange( mHttCrafts.Values );
		}


		/// <summary>
		/// Reads all plant data from db 
		/// </summary>
		private void LoadPlants()
		{
			mPlants = new Hashtable();

			// Get provider
			IProvider mProvider = DBSingleton.GetInstance().DataProvider;

			// Create the select command & fill Data Reader 
			IDbCommand selComm = mProvider.CreateCommand("PlantsList");
			
			mProvider.SetParameter(selComm, ":PL_MND_ID", mCurrentMandID);

			IDataReader mDR = mProvider.GetReader(selComm);

			decimal plantID;
			String	plantName; 
			
			while (mDR.Read()) 
			{
				plantName = mDR["PL_NAME"].ToString();
				plantID = Convert.ToDecimal( mDR["PL_ID"]);
				mPlants.Add(plantName, plantID );
			}
			mDR.Close();
		}


		private void LoadPlantManagers() 
		{
			mPlantManagers = new Hashtable();

			// Get provider
			IProvider mProvider = DBSingleton.GetInstance().DataProvider;

			// Create the select command & fill Data Reader 
			IDbCommand selComm = mProvider.CreateCommand("SelectPlantManagers");
			
			mProvider.SetParameter(selComm, ":PL_MND_ID", mCurrentMandID);

			IDataReader mDR = mProvider.GetReader(selComm);

			decimal plantID;
			decimal	plantManagerID; 
			
			while (mDR.Read()) 
			{
				plantID = Convert.ToDecimal( mDR["uspl_pl_id"]);
				plantManagerID = Convert.ToDecimal( mDR["uspl_user_id"]);
				mPlantManagers.Add(plantID.ToString().Trim()+  plantManagerID.ToString().Trim(), String.Empty );
			}
			mDR.Close();
		}

		/// <summary>
		/// Reads all coordinator-contractor assignment data from db 
		/// </summary>
		internal void LoadAssignments() 
		{
			mAssignments.Clear();

			Assignment myAssignment;
			mCoordCons = new Hashtable();
			// Get provider
			IProvider mProvider = DBSingleton.GetInstance().DataProvider;

			// Create the select command & fill Data Reader 
			IDbCommand selComm = mProvider.CreateCommand("SelectAssignments");

			IDataReader mDR = mProvider.GetReader(selComm);
			
			// Loop thru records and fill hashtable with assignments
			while (mDR.Read()) 
			{
				myAssignment = new Assignment();
				myAssignment.ContractorID = Convert.ToDecimal( mDR["ECEC_EXCO_ID"]);
				myAssignment.CoordinatorID = Convert.ToDecimal( mDR["ECEC_ECOD_ID"]);
				mAssignments.Add(myAssignment);

				mCoordCons.Add(myAssignment.CoordinatorID.ToString() + 
					myAssignment.ContractorID.ToString(), "");

			}
			mDR.Close();
		}


		/// <summary>
		/// Reads all active coordinators from db
		/// </summary>
		internal void LoadActiveCoordinators() 
		{
			CoordLovItem coordLovItem;
			mCoordinators.Clear();

			// Get provider
			IProvider mProvider = DBSingleton.GetInstance().DataProvider;

			// Create the select command & fill Data Reader 
			IDbCommand selComm = mProvider.CreateCommand("SelectCoordinators");

			mProvider.SetParameter(selComm, ":EXCO_MND_ID", mCurrentMandID);

			IDataReader mDR = mProvider.GetReader(selComm);

			// Loop thru records and fill hashtable with coordinators
			while (mDR.Read()) 
			{
				coordLovItem = new CoordLovItem();
				coordLovItem.CoordID = Convert.ToDecimal( mDR["ECEC_ECOD_ID"]);
				coordLovItem.CoordFullNameTel = mDR["UM_BOTHNAMESTEL"].ToString();
				mCoordinators.Add(coordLovItem.CoordID, coordLovItem );
			}
			mDR.Close();
			
		}

		/// <summary>
		/// Reads all active external contractors from db
		/// </summary>
		internal void LoadContractors() 
		{
			ContractorLovItem  contractorLovItem;
			mContractors.Clear();

			// Get provider
			IProvider mProvider = DBSingleton.GetInstance().DataProvider;
 
			IDbCommand selComm = mProvider.CreateCommand("SelectContractors");
			mProvider.SetParameter(selComm, ":EXCO_MND_ID", mCurrentMandID);
			IDataReader mDR = mProvider.GetReader(selComm);
		
			// Loop thru records and fill hashtable with EXCOS
			while (mDR.Read()) 
			{
				contractorLovItem = new ContractorLovItem();
				contractorLovItem.ContractorID = Convert.ToDecimal( mDR["EXCO_ID"]);
				contractorLovItem.ContractorName = mDR["EXCO_NAME"].ToString();
				mContractors.Add(contractorLovItem.ContractorID, contractorLovItem );
			}
			mDR.Close();
		}

		/// <summary>
		/// Reads external contractors with status INVALD
		/// </summary>
		internal void LoadInvalidContractors() 
		{
			ContractorLovItem   contractorLovItem;

			mInvalidContractors.Clear();

			IProvider mProvider = DBSingleton.GetInstance().DataProvider;
			IDbCommand selComm = mProvider.CreateCommand("SelectInvalidContractors");
			mProvider.SetParameter(selComm, ":EXCO_MND_ID", mCurrentMandID);

			IDataReader mDR = mProvider.GetReader(selComm);
		
			// Loop thru records and fill hashtable with invalid EXCOS
			while (mDR.Read()) 
			{
				contractorLovItem = new ContractorLovItem();
				contractorLovItem.ContractorID = Convert.ToDecimal( mDR["EXCO_ID"]);
				contractorLovItem.ContractorName = mDR["EXCO_NAME"].ToString();
				mInvalidContractors.Add(contractorLovItem.ContractorID, contractorLovItem );
			}
			mDR.Close();
		}

		/// <summary>
		/// Reads external contractors which were imported with coworkers booking data (dynamic data).
		/// but where coworker was not found in actve FPASS data. 
		/// Currently no mandant and no Excontractor PK ID available
		/// </summary>
		internal void LoadNonFPASSContractors() 
		{
			LovItem lovItem;
			decimal dummyCtr = 0;
			mNonFPASSContractors.Clear();

			IProvider mProvider = DBSingleton.GetInstance().DataProvider;
			IDbCommand selComm = mProvider.CreateCommand("SelectNonFpassContractors");
			IDataReader mDR = mProvider.GetReader(selComm);
		
			while (mDR.Read()) 
			{
				lovItem = new LovItem();
				lovItem.Id = dummyCtr.ToString();
				lovItem.ItemValue = mDR["EXCO_NAME"].ToString();
				mNonFPASSContractors.Add(lovItem.Id, lovItem );
				dummyCtr++;
			}
			mDR.Close();
		}

		/// <summary>
		/// Reads all archived external contractors from db
		/// </summary>
		internal void LoadArchiveContractors() 
		{
			ContractorLovItem   contractorLovItem;

			mContractorsArchive.Clear();

			IProvider mProvider = DBSingleton.GetInstance().DataProvider;
			IDbCommand selComm = mProvider.CreateCommand("SelectContractorsArchive");
			mProvider.SetParameter(selComm, ":AECO_AMND_ID", mCurrentMandID);

			IDataReader mDR = mProvider.GetReader(selComm);
		
			while (mDR.Read()) 
			{
				contractorLovItem = new ContractorLovItem();
				contractorLovItem.ContractorID = Convert.ToDecimal( mDR["AECO_ID"]);
				contractorLovItem.ContractorName = mDR["AECO_NAME"].ToString();
				mContractorsArchive.Add(contractorLovItem);
			}
			mDR.Close();
		}


		/// <summary>
		/// Reads all active coordinators from db
		/// </summary>
		private void LoadAllCoordinators() 
		{
			CoordLovItem	coordLovItem;

			mAllCoordinators.Clear();

			IProvider mProvider = DBSingleton.GetInstance().DataProvider; 
			IDbCommand selComm = mProvider.CreateCommand("SelectAllActiveCoordinators");
			mProvider.SetParameter(selComm, ":VWC_MND_ID", mCurrentMandID);

			IDataReader mDR = mProvider.GetReader(selComm);

			while (mDR.Read()) 
			{
				coordLovItem = new CoordLovItem();
				coordLovItem.CoordID = Convert.ToDecimal( mDR["VWC_ID"]);
				coordLovItem.CoordFullNameTel = mDR["VWC_BOTHNAMES"].ToString();
				mAllCoordinators.Add(coordLovItem);
			}
			mDR.Close();
			
			mDR = null;
			selComm = null;
			selComm = mProvider.CreateCommand("SelectArchiveCoordinators");

			mProvider.SetParameter(selComm, ":AVWC_MND_ID", mCurrentMandID);

			mDR = mProvider.GetReader(selComm);

			while (mDR.Read()) 
			{
				coordLovItem = new CoordLovItem();
				coordLovItem.CoordID = Convert.ToDecimal( mDR["AVWC_ID"]);
				coordLovItem.CoordFullNameTel = mDR["AVWC_BOTHNAMES"].ToString();
				mAllCoordinators.Add(coordLovItem);
			}
			mDR.Close();

			coordLovItem = new CoordLovItem();
			coordLovItem.CoordID = 0;
			coordLovItem.CoordFullNameTel =  "";
			mAllCoordinators.Add(coordLovItem);

			mAllCoordinators.Sort(mCoordinatorComparer);			
		}


		/// <summary>
		/// Fill LOV with both productive and archived supervisors (used eg. ComboBOx "Baustellenleiter" in FrmArchive)
		/// </summary>
		private void LoadAllSupervisors() 
		{
			SupervisorLovItem superLovItem;

			mAllSupervisors.Clear();

			IProvider mProvider = DBSingleton.GetInstance().DataProvider;
			IDbCommand selComm = mProvider.CreateCommand("SelectAllActiveSupervisors");
			mProvider.SetParameter(selComm, ":EXCO_MND_ID", mCurrentMandID);

			IDataReader mDR = mProvider.GetReader(selComm);

			// Get all supervisors from active external contractors
			while (mDR.Read()) 
			{
				superLovItem						= new SupervisorLovItem();
				superLovItem.EXCOID					= Convert.ToDecimal( mDR["EXCO_ID"]);
				superLovItem.SuperBothNamesAndEXCO	= mDR["SUPERVISOR"].ToString();
				mAllSupervisors.Add(superLovItem);
			}
			mDR.Close();
			
			mDR = null;
			selComm = null;
			selComm = mProvider.CreateCommand("SelectArchiveSupervisors");

			mProvider.SetParameter(selComm, ":AECO_AMND_ID", mCurrentMandID);

			mDR = mProvider.GetReader(selComm);

			// Get all supervisors from archived external contractors
			while (mDR.Read()) 
			{
				superLovItem						= new SupervisorLovItem();
				superLovItem.EXCOID					= Convert.ToDecimal( mDR["AECO_ID"]);
				superLovItem.SuperBothNamesAndEXCO	= mDR["SUPERVISOR"].ToString();
				mAllSupervisors.Add(superLovItem);
			}
			mDR.Close();

			// Add dummy empty value to dropdown list
			superLovItem						= new SupervisorLovItem();
			superLovItem.EXCOID					= 0;
			superLovItem.SuperBothNamesAndEXCO  = "";
			mAllSupervisors.Add(superLovItem);

			mAllSupervisors.Sort( new SupervisorComparer() );			
		}
		
		#endregion // End of Methods


		#region InnerClasses
		

		/// <summary>
		/// Allows sorting of coordinators
		/// </summary>
		public class CoordinatorComparer : IComparer
		{
			/// <summary>
			/// Compares two CoordLovItems by Name.
			/// </summary>
			/// <param name="pFirst">A first CoordLovItem</param>
			/// <param name="pSecond">A second CoordLovItem</param>
			/// <returns>-1 if the first object is less then the second, 0 if 
			/// objects are equal, 1 if the second object is less then the first.</returns>
			public int Compare(object pFirst, object pSecond)
			{
				try
				{
					CoordLovItem coord1 = (CoordLovItem) pFirst;
					CoordLovItem coord2 = (CoordLovItem) pSecond;
					return coord1.CoordFullNameTel.CompareTo(coord2.CoordFullNameTel );
				}
				catch(Exception)
				{
					return 0;
				}
			}
		}


		/// <summary>
		/// Allows sorting of supervisors
		/// </summary>
		public class SupervisorComparer : IComparer
		{
			/// <summary>
			/// Compares two SupervisorLovItems by Name.
			/// </summary>
			/// <param name="pFirst">A first SupervisorLovItem</param>
			/// <param name="pSecond">A second SupervisorLovItem</param>
			/// <returns>-1 if the first object is less then the second, 0 if 
			/// objects are equal, 1 if the second object is less then the first.</returns>
			public int Compare(object pFirst, object pSecond)
			{
				try
				{
					SupervisorLovItem sup1 = (SupervisorLovItem) pFirst;
					SupervisorLovItem sup2 = (SupervisorLovItem) pSecond;
					return sup1.SuperBothNamesAndEXCO.CompareTo( sup2.SuperBothNamesAndEXCO );
				}
				catch(Exception)
				{
					return 0;
				}
			}
		}
		
		
		/// <summary>
		/// Allows sorting of contractors
		/// </summary>
		public class ContractorComparer : IComparer
		{

			/// <summary>
			/// Compares two ContractorLovItems by Name.
			/// </summary>
			/// <param name="pFirst">A first ContractorLovItem</param>
			/// <param name="pSecond">A second ContractorLovItem</param>
			/// <returns>-1 if the first object is less then the second, 0 if 
			/// objects are equal, 1 if the second object is less then the first.</returns>
			public int Compare(object pFirst, object pSecond)
			{
				try
				{
					ContractorLovItem contractor1 = (ContractorLovItem) pFirst;
					ContractorLovItem contractor2 = (ContractorLovItem) pSecond;
					return contractor1.ContractorName.CompareTo(contractor2.ContractorName);
					
				}
				catch(Exception)
				{
					return 0;
				}
			}
		}
		
		/// <summary>
		/// Allwos sorting of crafts by notation
		/// </summary>
		public class CraftNotationComparer : IComparer
		{
			/// <summary>
			/// Compares two CraftLovItems by notation.
			/// </summary>
			/// <param name="pFirst">A first CraftLovItem</param>
			/// <param name="pSecond">A second CraftLovItem</param>
			/// <returns>-1 if the first object is less then the second, 0 if 
			/// objects are equal, 1 if the second object is less then the first.</returns>
			public int Compare(object pFirst, object pSecond)
			{
				try
				{
					CraftLovItem craft1 = (CraftLovItem) pFirst;
					CraftLovItem craft2 = (CraftLovItem) pSecond;
					return craft1.CraftNotation.
						CompareTo(craft2.CraftNotation);
					
				}
				catch(Exception)
				{
					return 0;
				}
			}
		}


		/// <summary>
		/// Allwos sorting of crafts by number
		/// </summary>
		public class CraftNumberComparer : IComparer
		{
			/// <summary>
			/// Compares two CraftLovItems by Number.
			/// </summary>
			/// <param name="pFirst">A first CraftLovItem</param>
			/// <param name="pSecond">A second CraftLovItem</param>
			/// <returns>-1 if the first object is less then the second, 0 if 
			/// objects are equal, 1 if the second object is less then the first.</returns>
			public int Compare(object pFirst, object pSecond)
			{
				try
				{
					CraftLovItem craft1 = (CraftLovItem) pFirst;
					CraftLovItem craft2 = (CraftLovItem) pSecond;
					return craft1.CraftNumber.
						CompareTo(craft2.CraftNumber);
					
				}
				catch(Exception)
				{
					return 0;
				}
			}
		}


		/// <summary>
		/// Allwos sorting of crafts by number
		/// </summary>
		public class PlantsComparer : IComparer
		{
			/// <summary>
			/// Compares two plants by name.
			/// </summary>
			/// <param name="pFirst"></param>
			/// <param name="pSecond"></param>
			/// <returns>-1 if the first object is less then the second, 0 if 
			/// objects are equal, 1 if the second object is less then the first.</returns>
			public int Compare(object pFirst, object pSecond)
			{
				try
				{
					return pFirst.ToString().CompareTo(pSecond.ToString());
					
				}
				catch(Exception)
				{
					return 0;
				}
			}
		}
		#endregion //End of InnerClasses



	}
}

using System;
using System.Collections;
using System.Data;

using Degussa.FPASS.Bo.Administration;
using Degussa.FPASS.Db;
using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.Util.UserManagement;

using de.pta.Component.DbAccess;
using de.pta.Component.Errorhandling;

namespace Degussa.FPASS.Gui.Dialog.User
{
	/// <summary>
	/// Summary description for RoleCoordinatorSingleton.
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
	public class RoleCoordinatorSingleton
	{
		#region Members

		// This class is a singleton, it calls the popup form FrmPromptCoordinator
		private static RoleCoordinatorSingleton mInstance = null;
		private FrmPromptCoordinator mFrmPrompt;

		// DB commands and parameters for commands
		private IProvider mProvider;
		private IDbCommand selComm;
		private IDbCommand callSPComm;
		private IDataReader mDR;
		private const string COORDS_QUERY	     = "SelectCoordsForReassign";
		private const string COORD_ID_QUERY	     = "SelectCurrrentCoordID";
		private const string EXCO_CWR_QUERY      = "GetDependentExcoIDs";
		private const string KNOW_EXCO_CWR_QUERY = "CountDependentCwrIDs";
		private const string SP_REM_COORD        = "sp_remove_coordinator";
		private const string SP_REM_ASS_EXCOCOORD= "sp_deleteecec";

		private const string EXCO_FIND_PARA	   = ":ECEC_EXCO_ID";
		private const string COORD_USER_PARA   = ":ECOD_USER_ID";
		private const string CWR_COORD_PARA    = ":CWR_ECOD_ID";
		private const string CWR_EXCO_PARA     = ":CWR_EXCO_ID";

		// IDs: new coord, coord to be removed, current external contractor
		private decimal mCoordRemoveID;
		private decimal mCoordReAssignID;
		private decimal mCurrentEXCOID;
		private string  mCurrentEXCOName = String.Empty;

		// BO holds data of current assignment coord-exco
		// Arraylist holds alternative coordinators for given exco, hashtable is wrapper
		private BOExcoCoordinator mBOExcoCoordinator;
		private ArrayList	 mArlEXCOIDs;
		private ArrayList	 mArlCoordinators;
		private Hashtable	 mHttCoordinators;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public RoleCoordinatorSingleton()
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
			mProvider = DBSingleton.GetInstance().DataProvider;
		}	

		#endregion //End of Initialization

		#region Accessors 

		public decimal CoordRemoveID 
		{
			get 
			{
				return mCoordRemoveID;
			}
			set 
			{
				mCoordRemoveID = value;
			}
		} 
	
		public decimal CoordReAssignID 
		{
			get 
			{
				return mCoordReAssignID;
			}
			set 
			{
				mCoordReAssignID = value;
			}
		}

		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// Get instance of this singleton
		/// </summary>
		/// <returns></returns>
		public static RoleCoordinatorSingleton GetInstance()
		{
			if(null == mInstance)
			{
				mInstance = new RoleCoordinatorSingleton();
			}
			return mInstance;
		}

		/// <summary>
		/// Get the coordinator ID corresponding to the given FPASS User ID
		/// (if a user is deleted and it is a coordinator)
		/// </summary>
		/// <param name="pFASSUser"></param>
		/// <returns>ID of coordinator</returns>
		internal decimal GetCurrentCoordID(decimal pFASSUser)
		{
			decimal currCoordID = 0;
			selComm = mProvider.CreateCommand(COORD_ID_QUERY);
			mProvider.SetParameter(selComm, COORD_USER_PARA, pFASSUser);
		
			mDR					= mProvider.GetReader(selComm);
			while ( mDR.Read() )
			{
				currCoordID = Convert.ToDecimal(mDR["ECOD_ID"]);
			}
			mDR.Close();
			return currCoordID;

		}

		/// <summary>
		/// Produces a hashtable containing all the alternative coordinators for the given external contractor
		/// If no coworkers are assigned to this exco's then then the coordinator can be deleted
		/// The coordinator objects are held in an arraylist, the hashtable uses the PK of the exco as its key
		/// and the arraylist of alternative coordinators as the value part.
		/// If there are no other alternative coordinators for the given exco, the name of the exco forms the value part
		/// ..to allow the exco name to be shown in error messages			
		/// </summary>
		/// <param name="pCoordID">PK ID of the coordinator to be removed</param>
		/// <param name="pEXCOID">PK ID of the external contractor</param>
		/// <returns>Hashtable of alternative coords by exco or null if no coworkers are assigned to this coordinator</returns>
		internal Hashtable GetAlternativeCoordinators(decimal pCoordID, decimal pEXCOID)
		{	
			this.mCoordReAssignID	   = 0;
			this.mCoordRemoveID        = pCoordID;
			this.mCurrentEXCOID        = pEXCOID;
			this.mCurrentEXCOName      = String.Empty;
			
			// Ask how many coworkers assigned to this exco / coordinator combination
			decimal depCoworkers = this.AreDepCoWorkers();

			// If no coworkers are assigned to this coordinator, ok
			if ( depCoworkers == 0 )
			{
				return null;
			}
			else
			{
				ArrayList arlWrapper = new ArrayList();
				mHttCoordinators     = new Hashtable();

				// Get the alternative coords for the given exco
				mArlCoordinators	= GetMyCoordinators(mCurrentEXCOID);

				// If no alternative coordinators for this EXCOID, return name of exco
				if ( mArlCoordinators.Count == 0 && !this.mCurrentEXCOName.Equals(String.Empty)  )
				{
					string currEXCOName = this.mCurrentEXCOName;
					mHttCoordinators.Add(mCurrentEXCOID, currEXCOName);
				} 
				else
				{
					mHttCoordinators.Add(mCurrentEXCOID, mArlCoordinators);
				}
				return mHttCoordinators;
			}		
		}

		/// <summary>
		/// Produces a hashtable containing all the alternative coordinators for all the external contractors
		/// for the coworkers to which the given coordinator is assigned
		/// The coworkers assigned to the coordinator are scanned to get the exco ID's
		/// The coordinator objects are held in an arraylist, the hashtable uses the PK of the exco as its key
		/// and the arraylist of alternative coordinators as the value part.
		/// If there are no other alternative coordinators for the given exco, the name of the exco forms the value part
		/// ..to allow the exco name to be shown in error messages		
		/// </summary>
		/// <param name="pCoordID">PK ID of the coordinator to be removed</param>
		/// <returns>Hashtable of alternative coords by exco or null if no coworkers are assigned to this coordinator</returns>
		internal Hashtable GetAlternativeCoordinators(decimal pCoordID)
		{	
			//this.mCurrentFPASSUserPKID = pFPASSUserPKID;
			this.mCoordReAssignID	   = 0;
			this.mCoordRemoveID        = pCoordID;
			this.mCurrentEXCOName      = String.Empty;
			
			// Get all excontractor ids
			this.GetAllEXCOIDs();

			// If no coworkers are assigned to this coordinator, ok
			if ( mArlEXCOIDs.Count == 0 )
			{
				return null;
			}
			else
			{
				// Fill hashtable, key is id of exco, value is hashtable of alternative coordins
				ArrayList arlWrapper = new ArrayList();
				mHttCoordinators     = new Hashtable();
		
				foreach (decimal j in mArlEXCOIDs)
				{
					mArlCoordinators = GetMyCoordinators(j);

					// If no alternative coordinators for this EXCOID, return name of exco
					if ( mArlCoordinators.Count == 0 && !this.mCurrentEXCOName.Equals(String.Empty)  )
					{
						string currEXCOName = this.mCurrentEXCOName;
						mHttCoordinators.Add(j, currEXCOName);
					} 
					else
					{
						mHttCoordinators.Add(j, mArlCoordinators);
					}
				}
				return mHttCoordinators;
			}		
		}


		/// <summary>
		/// Reassign the coworkers of a given external contractor to an alternative coordinator for this exco
		/// Show the popup form <see cref="Degussa.FPASS.Gui.Dialog.User.FrmPromptCoordinator"/>FrmPromptCoordinator 
		/// to prompt the user for an alternative coordinator.
		/// Calls a stored procedure in the database to archive the old (coord to exco) assignments and deletes the productive assignments
		/// If there are no more assignments and the parameter pFlgRemoveBoth is true, then archive the coordinator as well.
		/// (coordinator should not be archived if it is only an assignment coord-exco being deleted)
		/// </summary>
		/// <param name="pFlgRemoveBoth">Whether to archive the coordinator if there are no more assignments</param>
		/// <param name="pRemoveCoorID">PK of the coord to be deleted</param>
		/// <param name="pCurrEXCOID">PK of the exco to show alternative coords for</param>
		/// <param name="pAltCoords">Arraylist of alternative coords for the given exco</param>
		/// <param name="pDummyComm">an open connection, the stored proc is part of an open trasaction</param>
		/// <param name="pDummyTrans">open transaction</param>
		/// <returns></returns>
		internal bool ReAssignCoWorkers( bool pFlgRemoveBoth,
										decimal pRemoveCoorID, 
										decimal pCurrEXCOID, 
										ArrayList pAltCoords, 
										IDbCommand pDummyComm,
										IDbTransaction pDummyTrans)
		{						
			bool flg_Success = false;
			string delBoth;

			this.mCoordRemoveID		   = pRemoveCoorID;
			this.mCoordReAssignID	   = 0;
			this.mCurrentEXCOID        = pCurrEXCOID;
			this.mArlCoordinators      = pAltCoords;

			this.LoadPrompt();

			if ( mFrmPrompt.CurrentCoordID == 0)
			{
				this.mCoordReAssignID = 0;
				mFrmPrompt.Dispose();
				flg_Success = false;

			}
			else
			{
				this.mCoordReAssignID = mFrmPrompt.CurrentCoordID;
				mFrmPrompt.Dispose();

				// check if both the assignment and the coordinator itself are to be deleted
				if ( pFlgRemoveBoth )
				{
					delBoth = "Y";
				}
				else
				{
					delBoth = "N";
				}
		
				callSPComm = mProvider.CreateCommand("SequenceDummy");
				callSPComm.CommandText = SP_REM_COORD 
					+ "( " 
					+ this.mCoordReAssignID 
					+ ", " 
					+ this.mCoordRemoveID 
					+ ", " 
					+ this.mCurrentEXCOID
					+ ", " 
					+ UserManagementControl.getInstance().CurrentUserID.ToString()
					+ ", "
					+ "'"
					+ delBoth
					+ "'"
					+ ")";
				
				callSPComm.CommandType = CommandType.StoredProcedure;
				callSPComm.Connection  = pDummyComm.Connection;
				callSPComm.Transaction = pDummyTrans;
				int ret				   = callSPComm.ExecuteNonQuery();
				flg_Success = true;
			}
			return flg_Success;
		}

		/// <summary>
		/// Count how many cowokers are assigned to the given coordinator (mCoordRemoveID) and exco (mCurrentEXCOID)
		/// </summary>
		/// <returns>how many cowokers</returns>
		private decimal AreDepCoWorkers()
		{
			decimal foundid = 0;
			
			selComm				= mProvider.CreateCommand(KNOW_EXCO_CWR_QUERY);
			mProvider.SetParameter(selComm, CWR_EXCO_PARA, this.mCurrentEXCOID);
			mProvider.SetParameter(selComm, CWR_COORD_PARA, this.mCoordRemoveID);

			mDR				= mProvider.GetReader(selComm);
			while ( mDR.Read() )
			{
				foundid	= Convert.ToDecimal(mDR["result"]);
			}
			mDR.Close();
			return foundid;
		}

		
		/// <summary>
		/// Gets the alternative coordinators for the current external contractor
		/// Create and fill a value object for each coordinator
		/// Pack them in an arraylist
		/// The current coordinator (to be deleted) is among the results of the DB SELECT, in this case don't add 
		/// it to the list of alternative coordinators, rather note the name of the current exco in case there are no alternative coords.
		/// </summary>
		/// <param name="pCurrEXCOID">PK ID of current excontractor</param>
		/// <returns>arraylist of alternative coordinators or null if there are none</returns>
		private ArrayList GetMyCoordinators(decimal pCurrEXCOID)
		{			
			decimal numRecs  = 0;
			mCurrentEXCOName = String.Empty;
			selComm			 = mProvider.CreateCommand(COORDS_QUERY);
			mProvider.SetParameter(selComm, EXCO_FIND_PARA, pCurrEXCOID);

			ArrayList currArlDepCoords = new ArrayList();

			mDR = mProvider.GetReader(selComm);
			while ( mDR.Read() )
			{
				mBOExcoCoordinator = new BOExcoCoordinator();

				mBOExcoCoordinator.ECODID		= Convert.ToDecimal(mDR["ECEC_ECOD_ID"]);				
				mBOExcoCoordinator.Coordinator	= Convert.ToString(mDR["UM_BOTHNAMESTEL"]);
				mBOExcoCoordinator.ExContractor = Convert.ToString(mDR["EXCO_NAME"]);
	
				// If this is the coordinator to be deleted, don't add it to arraylist
				if ( mBOExcoCoordinator.ECODID == mCoordRemoveID )
				{
					// Name of exco used later
					mCurrentEXCOName = mBOExcoCoordinator.ExContractor;
				}
				else
				{
					currArlDepCoords.Add(mBOExcoCoordinator);
				}
				numRecs++;
			}
			mDR.Close();

			if ( numRecs == 0 )
			{
				// There are no other coordinators
				return null;
			}
			else
			{
				return currArlDepCoords;
			}
		}

		/// <summary>
		/// Fill arraylist with PKs of all excontractors to which dependent coworkers of this coordinator are assigned
		/// </summary>
		private void GetAllEXCOIDs()
		{
			decimal foundEXCOID = 0;
			mArlEXCOIDs = new ArrayList();
			
			selComm	= mProvider.CreateCommand(EXCO_CWR_QUERY);
			mProvider.SetParameter(selComm, CWR_COORD_PARA, mCoordRemoveID);

			mDR	= mProvider.GetReader(selComm);
			while ( mDR.Read() )
			{
				foundEXCOID	= Convert.ToDecimal(mDR["CWR_EXCO_ID"]);
				mArlEXCOIDs.Add(foundEXCOID);
			}
			mDR.Close();
		}

		/// <summary>
		/// loads the prompt <see cref="Degussa.FPASS.Gui.Dialog.User.FrmPromptCoordinator"/>FrmPromptCoordinator
		/// </summary>
		private void LoadPrompt()
		{
			mBOExcoCoordinator = (BOExcoCoordinator) this.mArlCoordinators[0];

			string promptTxt = "Dem aktuellen Koordinator sind noch Mitarbeiter der Firma " 
				+ mBOExcoCoordinator.ExContractor
				+ " zugeordnet. "
				+ "Bitte wählen Sie für diese Firma einen der folgenden alternativen Koordinatoren aus.";
									
			mFrmPrompt = new FrmPromptCoordinator();
			mFrmPrompt.ArlCoordinators = this.mArlCoordinators;
			mFrmPrompt.FillCoordinators();
			mFrmPrompt.lblPrompt.Text  = promptTxt;
	
			mFrmPrompt.ShowDialog();
		}


		#endregion // End of Methods
	}
}

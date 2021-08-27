using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

using de.pta.Component.DbAccess;
using de.pta.Component.Errorhandling;

using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.Util.Exceptions;
using Degussa.FPASS.Util.UserManagement;
using Degussa.FPASS.Util;
using Degussa.FPASS.Db;

using Degussa.FPASS.Gui.Dialog.Pass;
using Degussa.FPASS.FPASSApplication;

namespace Degussa.FPASS.Bo.Pass
{
	/// <summary>
	/// Implementation of factory for all briefings. Provides methods to build
	/// instances of all types of briefings. Acts like a container for briefings.
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
	public class BriefingFactory
	{
		#region Members

		/// <summary>
		/// used to hold all created briefings
		/// </summary>
		private Hashtable mAllBriefings;

		/// <summary>
		/// reference to the model, which instantiated the factory
		/// </summary>
		private CoWorkerModel mCoWorkerModel;

		/// <summary>
		/// provides db access
		/// </summary>
		private IProvider mProvider;

		// constants for select 
		private String SELECT_BRIEFING = "SelectBriefing";
		private String SELECT_BRIEFING_ARCHIVE = "SelectBriefingArchive";
		private String BRIEF_CWR_PARAM = ":CWBR_CWR_ID";
		
		#endregion Members

		#region Constructors

		/// <summary>
		/// Simple Constructor
		/// </summary>
		/// <param name="pModel">model which instantiates the factory</param>
		public BriefingFactory(CoWorkerModel pModel)
		{
			mCoWorkerModel = pModel;
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
		}	

		#endregion //End of Initialization

		#region Accessors 

		#endregion //End of Accessors

		#region Methods 


		/// <summary>
		/// Gets all briefings needed for a coworker. Checks what type of briefings
		/// were saved to db and reads the appropriate data from db. Briefings which were
		/// not granted once and therefore are not in db are created as new briefings.
		/// </summary>
		/// <returns>List containing all Authorizations</returns>
		internal ArrayList GetBriefings() 
		{
			AbstractBriefing aBriefing;

			mAllBriefings = new Hashtable();
			ArrayList briefingsList = new ArrayList();

			aBriefing = new CranesBriefing(mCoWorkerModel);
			mAllBriefings.Add(aBriefing.BriefingTypeID, aBriefing);

            // newer id card with photo (Lichtbildausweis) from SmartAct
            aBriefing = new IdCardPhotoSmActBriefing(mCoWorkerModel);
            mAllBriefings.Add(aBriefing.BriefingTypeID, aBriefing);

            // older id card with photo (Lichtbildausweis)
			aBriefing = new IdCardPhotoHitagBriefing(mCoWorkerModel);
			mAllBriefings.Add(aBriefing.BriefingTypeID, aBriefing);

			aBriefing = new PalletLifterBriefing(mCoWorkerModel);
			mAllBriefings.Add(aBriefing.BriefingTypeID, aBriefing);

			aBriefing = new RaisablePlattformBriefing(mCoWorkerModel);
			mAllBriefings.Add(aBriefing.BriefingTypeID, aBriefing);

			aBriefing = new RespiratoryMaskBriefing(mCoWorkerModel);
			mAllBriefings.Add(aBriefing.BriefingTypeID, aBriefing);

			aBriefing = new SiteSecurityBriefing(mCoWorkerModel);
			mAllBriefings.Add(aBriefing.BriefingTypeID, aBriefing);

			aBriefing = new VehicleEntranceLongBriefing(mCoWorkerModel);
			mAllBriefings.Add(aBriefing.BriefingTypeID, aBriefing);

			aBriefing = new VehicleEntranceShortBriefing(mCoWorkerModel);
			mAllBriefings.Add(aBriefing.BriefingTypeID, aBriefing);

			aBriefing = new BreathingApparatusG26_2_Briefing(mCoWorkerModel);
			mAllBriefings.Add(aBriefing.BriefingTypeID, aBriefing);

			aBriefing = new BreathingApparatusG26_3_Briefing(mCoWorkerModel);
			mAllBriefings.Add(aBriefing.BriefingTypeID, aBriefing);

			aBriefing = new SafetyAtWork_Briefing(mCoWorkerModel);
			mAllBriefings.Add(aBriefing.BriefingTypeID, aBriefing);

			// Fireman briefing
			aBriefing = new BriefingFireman(mCoWorkerModel);
			mAllBriefings.Add(aBriefing.BriefingTypeID, aBriefing);

            // Apprentice briefing
            aBriefing = new ApprenticeBriefing(mCoWorkerModel);
            mAllBriefings.Add(aBriefing.BriefingTypeID, aBriefing);

			this.ReadBriefingFromDB();

			IDictionaryEnumerator enu = mAllBriefings.GetEnumerator();
			while ( enu.MoveNext() ) 
			{
				briefingsList.Add(enu.Value);
			}

			return briefingsList;
		}

		/// <summary>
		/// Gets a briefing. Checks given pTypeID to decide which 
		/// subclass of AbstractBriefing is requested.
		/// </summary>
		/// <param name="pTypeID">id which identifies type of briefing</param>
		/// <returns>requested briefing</returns>
		internal AbstractBriefing GetBriefing(decimal pTypeID) 
		{
			return (AbstractBriefing)mAllBriefings[pTypeID];
		}

		/// <summary>
		/// Reads all brefings (except Vehicle entrance) from DB for the given coworker.	
        /// Column mappings:
        /// CWBR_USER_ID: id of user who set status "received".
        /// USERNICENAME: nice name of user who set status "received".
        /// CWBR_BRIEFINGDATE: date that status "received" was set.
        /// CWBR_BRIEFING_YN: briefing assigned Y/N.
        /// CWBR_INACTIVE_YN: briefing inactive Y/N, N means received.
		/// </summary>
		/// <exception cref="de.pta.Component.Errorhandling.UIFatalException">Is thrown if select statement fails.</exception>
		private void ReadBriefingFromDB() 
		{
			AbstractBriefing briefing;
			IDbCommand mSelComm = null;

			decimal vehShortEntranceID = Globals.GetInstance().BriefVehicleEntranceShortID;
			decimal vehLongEntranceID  = Globals.GetInstance().BriefVehicleEntranceLongID;

			if (!mCoWorkerModel.Mode.Equals(AllFPASSDialogs.CWR_DIALOG_MODE_ARCHIVE))
			{
				mSelComm = mProvider.CreateCommand(SELECT_BRIEFING);
			} 
			else 
			{
				mSelComm = mProvider.CreateCommand(SELECT_BRIEFING_ARCHIVE);
			}
			
			mProvider.SetParameter(mSelComm, BRIEF_CWR_PARAM, mCoWorkerModel.CoWorkerId);
			IDataReader mDR = mProvider.GetReader(mSelComm);

			while (mDR.Read())
			{               
				decimal dec = Convert.ToDecimal(mDR["CWBR_BRF_ID"]);
				briefing = GetBriefing(Convert.ToDecimal(mDR["CWBR_BRF_ID"]));
                briefing.IsInsert = false;
                briefing.BriefingID = Convert.ToDecimal(mDR["CWBR_ID"]);
				
                // Info about Received
                briefing.UserID = Convert.ToDecimal(mDR["CWBR_USER_ID"]);
                
                String boolExecutedYN = mDR["CWBR_INACTIVE_YN"].ToString();
                if (boolExecutedYN.Equals("N"))
                {
                    briefing.Received = true;
                }

                // UserName is set if briefing received or if current briefing is a vehicle entrance type
                if (briefing.Received
                    || briefing.BriefingTypeID == vehShortEntranceID
                    || briefing.BriefingTypeID == vehLongEntranceID)
                {
                    briefing.UserName = mDR["USERNICENAME"].ToString();
                }
                else
                {
                    briefing.UserName = String.Empty;
                }
				try 
				{
					briefing.BriefingDate = Convert.ToDateTime(mDR["CWBR_BRIEFINGDATE"]);
					briefing.BriefDateWasNull = false;
				} 
				catch { briefing.BriefingDate = DateTime.Now; }
								
				// Info about Directed
                String boolDirectedYN = mDR["CWBR_BRIEFING_YN"].ToString();
                if (boolDirectedYN.Equals("Y"))
                {
                    briefing.Directed = true;
                }

                // User who set status Directed: currently only used for Respmask briefings
                if (briefing.BriefingTypeID == Globals.GetInstance().BriefRespiratoryMaskID
                    && briefing.Directed 
                    && !mDR["CWBR_DIRECTUSER_ID"].Equals(DBNull.Value))
                {
                    briefing.DirectedUserID = Convert.ToDecimal(mDR["CWBR_DIRECTUSER_ID"]);
                    briefing.DirectedUserName = mDR["DIRECTUSERNICENAME"].ToString();
                    try { briefing.DirectedBriefingDate = Convert.ToDateTime(mDR["CWBR_DIRECTBRIEFINGDATE"]); }
                    catch { briefing.DirectedBriefingDate = DateTime.Now; }	
                }
                else
                {
                    briefing.DirectedUserName = String.Empty;
                }			
			}
			mDR.Close();
		}
	
		#endregion Methods
	}
}

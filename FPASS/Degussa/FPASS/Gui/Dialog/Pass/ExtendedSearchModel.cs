using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

using de.pta.Component.DbAccess;
using de.pta.Component.ListOfValues;
using de.pta.Component.DbAccess.Exceptions;
using de.pta.Component.Errorhandling;

using Degussa.FPASS.Bo.Pass;
using Degussa.FPASS.Db;
using Degussa.FPASS.FPASSApplication;
using Degussa.FPASS.Gui;
using Degussa.FPASS.Util.Exceptions;
using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.Util.UserManagement;
using Degussa.FPASS.Bo.Search;

namespace Degussa.FPASS.Gui.Dialog.Pass
{
	/// <summary>
	/// An ExtendedSearchModel is the model of the MVC-triad ExtendedSearchModel,
	/// ExtendedSearchController and FrmExtendedSearch.
	/// ExtendedSearchModel extends from the AbstractModel.
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
	public class ExtendedSearchModel : FPASSBaseModel
	{
		#region Members

		//Database access
		private const string COWORKER_QUERY			= "ExtendedSearchCWR";
		private const string CWRBRIEFING_QUERY		= "ExtendedSearchCWBR";
		private const string RECEPTIONAUTHO_QUERY	= "ExtendedSearchRATH";
		private const string PLANTMASKMED_QUERY		= "ExtendedSearchPLMEDMASK";

		//Hashtable Parameter
		private Hashtable mCoworkerDict;			
		private Hashtable mCwrbriefing;		
		private Hashtable mReceptionauthorize;	
		private Hashtable mPlantMaskMed;

		private	bool mCoworkerTrue;
		private	bool mCwrbriefingTrue;
		private	bool mReceptionauthorizeTrue;
		private	bool mPlantMaskMedTrue;


		private bool mSearchCriteriaCwr;
		private	bool mSearchCriteriaCwrbriefing;
		private	bool mSearchCriteriaReceptionauthor;
		private	bool mSearchCriteriaPlantMaskMed;

		private decimal mCoworkerID;
		private decimal mCoworkerIDForPlantMaskMed;
		private decimal mCoworkerIDForReception;
		private decimal mCoworkerIDForBriefing;

		// holds the search parameters from gui
		private string		 mVehicleRegNumber;
		private string		 mPlaceOfBirth;
		private string		 mSurname;
		private string		 mFirstname;
		private string		 mDateOfBirth;
		private string		 mOrderNumber;
		private string		 mSupervisor;
		private string		 mPhone;

		private string		 mTxtSiteSecurityBriefingBy;
		private string		 mTxtIndustrialSafetyBriefingBy;
		private string		 mTxtSafetyAtWorkServiceBriefingBy;
		private string mTxtIdPhotoHitagBy;
        private string mTxtIdPhotoSmActBy;
		private string		 mTxtBreathingApparatusG262BriefingBy;
		private string		 mTxtBreathingApparatusG263BriefingBy;
		private string		 mTxtPalletLifterBy;
		private string		 mTxtRaisablePlatformBy;
		private string		 mTxtCranesBy;
		private string		 mTxtCheckOffBy;
		private string		 mTxtCheckInBy;
		private string		 mTxtMaskNumberDelivered;
		private string		 mTxtMaskNumberRecieve;
		private string		 mTxtBriefingPlantBy;
		private string		 mTxtRespiratoryMaskBy;
		private string		 mTxtAccessAuthorizationBy;
		private string		 mTxtSafetyInstructionsBy;
		private string		 mTxtVehicleShortBy;
		private string		 mTxtVehicleLongBy;
		private string		 mTxtFiremanBriefingBy;

		private string mTxtAccessAuthorizationOn;
		private string mTxtVehicleShortOn;
		private string mTxtVehicleLongOn;
		private string mTxtValidFrom;
		private string mTxtValidUntil;
		private string mTxtDeliveryDate;
		private string mTxtIndustrialSafetyBriefingOn;
		private string mTxtSafetyAtWorkServiceBriefingOn;
		private string mTxtSiteSecurityBriefingOn;
		private string mTxtIdPhotoHitagOn;
        private string mTxtIdPhotoSmActOn;
		private string mTxtBreathingApparatusG262BriefingOn;
		private string mTxtBreathingApparatusG263BriefingOn;
		private string mTxtPalletLifterOn;
		private string mTxtRaisablePlatformOn;
		private string mTxtCranesOn;
		private string mTxtRespiratoryMaskOn;
		private string mTxtBriefingPlantOn;
		private string mTxtCheckOffOn;
		private string mTxtCheckInOn;
		private string mTxtSafetyInstructionsOn;
		private string mTxtFiremanBriefingOn;

		private string mIDCardNoHitag;
        private string mIDCardNoMifare;
        private string mPersNrSmAct;
        private string mPersNrFPASS;
		private string mCboCoordinator;
		private string mCboExternalContractor;
		private string mCboCraftNumber;
		private string mCboSubcontractor;
		private string mCboPlantOne;
		private string mCboPlantTwo;
		private string mCboPlantThree;
		private string mCboDepartment;
		private string mCboPrecautionaryMedical;
		private string mCboStatus;

		private bool		 mRbtSafetyInstructionsYes;
		private bool		 mRbtSafetyInstructionsNo;
		private bool		 mRbtRaisablePlatformYes;
		private bool		 mRbtRaisablePlatformNo;
		private bool		 mRbtBreathingApparatusG262BriefingYes;
		private bool		 mRbtBreathingApparatusG262BriefingNo;
		private bool		 mRbtBreathingApparatusG263BriefingYes;
		private bool		 mRbtBreathingApparatusG263BriefingNo;
		private bool		 mRbtPalletLifterNo;
		private bool		 mRbtPalletLifterYes;
		private bool		 mRbtCranesYes;
		private bool		 mRbtCranesNo;
		private bool		 mRbtAccessAuthorizationYes;
		private bool		 mRbtAccessAuthorizationNo;
		private bool		 mRbtVehicleShortCoYes;
		private bool		 mRbtVehicleShortCoNo;
		private bool		 mRbtVehicleShortYes;
		private bool		 mRbtVehicleShortNo;
		private bool		 mRbtVehicleLongCoYes;
		private bool		 mRbtVehicleLongCoNo;
		private bool		 mRbtVehicleLongYes;
		private bool		 mRbtVehicleLongNo;
		private bool mRbtIdPhotoHitagYes;
		private bool mRbtIdPhotoHitagNo;
        private bool mRbtIdPhotoSmActYes;
        private bool mRbtIdPhotoSmActNo;
		private bool		 mRbtSafetyAtWorkServiceBriefingYes;
		private bool		 mRbtSafetyAtWorkServiceBriefingNo;
		private bool		 mRbtSiteSecurityBriefingNo;
		private bool		 mRbtSiteSecurityBriefingYes;
		private bool		 mRbtIndustrialSafetyBriefingYes;
		private bool		 mRbtIndustrialSafetyBriefingNo;
		private bool		 mRbtRespiratoryMaskNo;
		private bool		 mRbtRespiratoryMaskYes;
		private bool		 mRbtPrecautionaryMedicalYes;
		private bool		 mRbtPrecautionaryMedicalNo;
		private bool		 mRbtBriefingPlantNo;
		private bool		 mRbtBriefingPlantYes;
		private bool		 mRbtFiremanBriefingNo;
		private bool		 mRbtFiremanBriefingYes;


		// ArrayList is filled here and given to form
		private ArrayList arlCoWorker;

		// DataTable is filled here and given to form
		private DataTable mTable;

        private FrmExtendedSearch mViewXtend;

		// CoWorker value object
		private CoWorkerSearch mBOCoWorker;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public ExtendedSearchModel()
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
		}	

		#endregion //End of Initialization

		#region Accessors 

		// ArrayList for working
		public ArrayList ResultsArray
		{
			get 
			{
				return arlCoWorker;
			}
		}

		// DataTAble for display
		public DataTable ResultsTable
		{
			get 
			{
				return mTable;
			}
		}

		#endregion //End of Accessors

		#region Methods 

		#region CoWorker

		/// <summary>
		/// Execute SQL query to get IDs of all coworkers that match search criteria 
		/// given in search fields in GUI dealing with general coworker attributes
		/// (Database view: VW_FPASS_EXTENDEDSEARCHCWR)
		/// Step 1: read given values of search fields in GUI & generate WHERE clause of SQL text
		/// Step 2: if search criteria have been selected, create & execute SQL query on DB
		/// Add each coworker ID in resultset to hashtable
		/// </summary>
		private void GetCoWorkerIds() 
		{
			CopyOutSearchCriteriaCoWorker();
			
			/*if search criteria selected the coworker hashtable is filled
			 */
			if (mSearchCriteriaCwr)
			{
				IProvider mProvider = DBSingleton.GetInstance().DataProvider;
				IDbCommand selComm  = mProvider.CreateCommand(COWORKER_QUERY);
				selComm.CommandText = GenerateWhereClauseCwr(selComm.CommandText);
			
				// Instantiate HashTable
				mCoworkerDict = new Hashtable();

				// Open data reader and get CoWorker IDs
				IDataReader mDR = mProvider.GetReader(selComm);
				while (mDR.Read())
				{
					mCoworkerID = Convert.ToDecimal(mDR["CWR_ID"]);
					if (mCoworkerDict[mCoworkerID] == null)
					{
						mCoworkerDict.Add(mCoworkerID, mDR["CWR_ID"]);
					}
				}
				mDR.Close();
		
				// Set flag: were any coworkers found?
				if ( mCoworkerDict.Count > 0 ) 
				{
					mCoworkerTrue = true;	
				} 
				else 
				{
					mCoworkerTrue = false;
				}
			}
		}


		/// <summary>
		/// Get values for search criteria:
		/// those values given in search fields in block in GUI dealing with general coworker attributes
		/// If no search criteria chosen, set flag to that effect
		/// 17.02.2005: new field "Ausweisnummer"
		/// Update 07.03.2005: Changed logic of flag: 
		/// As soon as any search criterion is found, flag noSearchCriteria is set to false (-1)
		/// Also added wildcards to all txt fields
		/// </summary>
		private void CopyOutSearchCriteriaCoWorker() 
		{
            mViewXtend = (FrmExtendedSearch)mView;

            int	noSearchCriteria = 1;
			mSearchCriteriaCwr = true;
			
			mCboStatus = this.GetSelectedValueFromCbo(mViewXtend.CboStatus).Trim();
			if ( mCboStatus.Length > 0 ) 
			{
				noSearchCriteria = -1;
			}

			mCboCoordinator = this.GetSelectedValueFromCbo(mViewXtend.CboCoordinator).Trim();
			if ( mCboCoordinator.Length > 0 ) 
			{
				noSearchCriteria = -1;
			}

			mCboCraftNumber = this.GetSelectedValueFromCbo(mViewXtend.CboCraftNumber).Trim();
			if ( mCboCraftNumber.Length > 0 ) 
			{
				noSearchCriteria = -1;
			}

			mCboDepartment = this.GetSelectedValueFromCbo(mViewXtend.CboDepartment);
			if ( mCboDepartment.Length > 0 ) 
			{
				noSearchCriteria = -1;
			}

			mCboExternalContractor = this.GetSelectedValueFromCbo(mViewXtend.CboExternalContractor);
			if ( mCboExternalContractor.Length > 0 ) 
			{
				noSearchCriteria = -1;
			}

			mCboSubcontractor = this.GetSelectedValueFromCbo(mViewXtend.CboSubcontractor);
			if ( mCboSubcontractor.Length > 0 ) 
			{
				noSearchCriteria = -1;
			}

			this.mVehicleRegNumber = mViewXtend.TxtVehicleRegNumber.Text.Trim().Replace("*","%");
			if ( mVehicleRegNumber.Length > 0 ) 
			{
				noSearchCriteria = -1;
			}

			this.mPlaceOfBirth = mViewXtend.TxtPlaceOfBirth.Text.Trim().Replace("*","%");
			if ( mPlaceOfBirth.Length > 0 ) 
			{
				noSearchCriteria = -1;
			}

			this.mSurname = mViewXtend.TxtSurname.Text.Trim().Replace("*","%");
			if ( mSurname.Length > 0 ) 
			{
				noSearchCriteria = -1;
			}

			this.mFirstname = mViewXtend.TxtFirstname.Text.Trim().Replace("*","%");
			if ( mFirstname.Length > 0 ) 
			{
				noSearchCriteria = -1;
			}

			this.mDateOfBirth = mViewXtend.TxtDateOfBirth.Text.Trim();
			if ( mDateOfBirth.Length > 0 ) 
			{
				noSearchCriteria = -1;
			}

			this.mOrderNumber = mViewXtend.TxtOrderNumber.Text.Trim().Replace("*","%");
			if ( mOrderNumber.Length > 0 ) 
			{
				noSearchCriteria = -1;
			}

			this.mSupervisor = mViewXtend.TxtSupervisor.Text.Trim().Replace("*","%");
			if ( mSupervisor.Length > 0 ) 
			{
				noSearchCriteria = -1;
			}

			this.mPhone = mViewXtend.TxtPhone.Text.Trim().Replace("*","%");
			if ( mPhone.Length > 0 ) 
			{
				noSearchCriteria = -1;
			}

			this.mTxtCheckOffBy = mViewXtend.TxtCheckOffBy.Text.Trim().Replace("*","%");
			if ( mTxtCheckOffBy.Length > 0 ) 
			{
				noSearchCriteria = -1;
			}

			this.mTxtCheckInBy = mViewXtend.TxtCheckInBy.Text.Trim().Replace("*","%");
			if ( mTxtCheckInBy.Length > 0 ) 
			{
				noSearchCriteria = -1;
			}

			this.mTxtValidFrom = mViewXtend.TxtValidFrom.Text.Trim();
			if ( mTxtValidFrom.Length > 0 ) 
			{
				noSearchCriteria = -1;
			}

			this.mTxtValidUntil = mViewXtend.TxtValidUntil.Text.Trim();
			if ( mTxtValidUntil.Length > 0 ) 
			{
				noSearchCriteria = -1;
			}

			this.mTxtDeliveryDate = mViewXtend.TxtDeliveryDate.Text.Trim();
			if ( mTxtDeliveryDate.Length > 0 ) 
			{
				noSearchCriteria = -1;
			}

			this.mTxtCheckOffOn = mViewXtend.TxtCheckOffOn.Text.Trim();
			if ( mTxtCheckOffOn.Length > 0 ) 
			{
				noSearchCriteria = -1;
			}

			this.mTxtCheckInOn = mViewXtend.TxtCheckInOn.Text.Trim();
			if ( mTxtCheckInOn.Length > 0 ) 
			{
				noSearchCriteria = -1;
			}

			// ID card number with wildcard (Hitag2)
			mIDCardNoHitag = mViewXtend.TxtIDCardNoHitag.Text.Trim().Replace("*","%");
			if ( mIDCardNoHitag.Length > 0 ) 
			{
				noSearchCriteria = -1;
			}

            // ID card number with wildcard (Mifare)
            mIDCardNoMifare = ((FrmExtendedSearch)mView).TxtIDCardNoMifare.Text.Trim().Replace("*", "%");
            if (mIDCardNoMifare.Length > 0)
            {
                noSearchCriteria = -1;
            }

            // Pers number SmartAct
            mPersNrSmAct = mViewXtend.TxtPersNrSmAct.Text.Trim().Replace("*", "%");
            if (mPersNrSmAct.Length > 0)
            {
                noSearchCriteria = -1;
            }

            // Pers number FAPSS
            mPersNrFPASS = mViewXtend.TxtPersNrFPASS.Text.Trim().Replace("*", "%");
            if (mPersNrFPASS.Length > 0)
            {
                noSearchCriteria = -1;
            }

			/// Changed logic 07.03.2005 after error:
			/// only interested if any search criterion set, not how many
			if ( noSearchCriteria == 1 )
				mSearchCriteriaCwr = false;		
		}


		/// <summary>
		/// Execute SQL query to get IDs of all coworkers that match search criteria 
		/// given in search fields in GUI dealing with general coworker attributes
		/// (Database view: VW_FPASS_EXTENDEDSEARCHCWR)
		/// Step 1: read given values of search fields in GUI & generate WHERE clause of SQL text
		/// Step 2: if search criteria have been selected, create & execute SQL query on DB
		/// Last change 17.02.2005: New attribute ID card number
		/// patch 11.03.2005: change .Length > 1 to .Length > = so '*' is considered in search
		/// <summary>
		private String GenerateWhereClauseCwr(String pSelect) 
		{
			string whereClause = " WHERE CWR_MND_ID = " 
								+ UserManagementControl.getInstance().CurrentMandatorID.ToString();
			
			if ( this.mTxtCheckInBy.Length > 0) 
			{
				whereClause = whereClause + " AND UPPER(ENTRYUSER) LIKE '" + this.mTxtCheckInBy.ToUpper() + "' "; 
			}

			if ( this.mTxtCheckInOn.Length > 0) 
			{
				whereClause = whereClause + " AND TRUNC(CWR_ENTRYDATECOOD) = TO_DATE('" 
					+ this.mTxtCheckInOn 
					+ "', 'DD.MM.YYYY') ";
			}

			if ( this.mTxtCheckOffBy.Length > 0) 
			{
				whereClause = whereClause + " AND UPPER(CHKUSER) LIKE '" + this.mTxtCheckOffBy.ToUpper() + "' "; 
			}

			if ( this.mTxtCheckOffOn.Length > 0) 
			{
				whereClause = whereClause + " AND TRUNC(CWR_CHKOFFDATECOOD) = TO_DATE('" 
					+ this.mTxtCheckOffOn 
					+ "', 'DD.MM.YYYY') ";
			}

			if ( this.mDateOfBirth.Length > 0) 
			{
				whereClause = whereClause + " AND TRUNC(CWR_DATEOFBIRTH) = TO_DATE('" 
					+ this.mDateOfBirth 
					+ "', 'DD.MM.YYYY') ";
			}

			if ( this.mTxtDeliveryDate.Length > 0) 
			{
				whereClause = whereClause + " AND TRUNC(CWR_DATECREATED) = TO_DATE('" 
					+ this.mTxtDeliveryDate 
					+ "', 'DD.MM.YYYY') ";
			}

			if ( this.mFirstname.Length > 0) 
			{
				whereClause = whereClause + " AND UPPER(CWR_FIRSTNAME) LIKE '" + this.mFirstname.ToUpper() + "' "; 
			}

			if ( this.mOrderNumber.Length > 0) 
			{
				whereClause = whereClause + " AND CWR_ORDERNO LIKE '" + this.mOrderNumber + "' "; 
			}

			if ( this.mPhone.Length > 0) 
			{
				whereClause = whereClause + " AND SUPERTEL LIKE '" + this.mPhone + "' "; 
			}

			if ( this.mPlaceOfBirth.Length > 0) 
			{
				whereClause = whereClause + " AND CWR_PLACEOFBIRTH LIKE '" + this.mPlaceOfBirth + "' "; 
			}

			if ( this.mSupervisor.Length > 0) 
			{
				whereClause = whereClause + " AND UPPER(SUPERVISOR) LIKE '" + this.mSupervisor.ToUpper() + "' "; 
			}

			if ( this.mSurname.Length > 0) 
			{
				whereClause = whereClause + " AND UPPER(CWR_SURNAME) LIKE '" + this.mSurname.ToUpper() + "' "; 
			}

			// Compare VALID FROM & VALID UNTIL dates
			if ( this.mTxtValidFrom.Length > 0) 
			{
				whereClause = whereClause 
							+ " AND TRUNC(CWR_VALIDFROM) >= to_date('" 
							+ this.mTxtValidFrom 
							+ "', 'DD.MM.YYYY')"; 
			}
			if ( this.mTxtValidUntil.Length > 0) 
			{
				whereClause = whereClause 
							+ " AND TRUNC(CWR_VALIDUNTIL) <= to_date('" 
							+ this.mTxtValidUntil 
							+ "', 'DD.MM.YYYY')"; 
			}

			if ( this.mVehicleRegNumber.Length > 0) 
			{
				whereClause = whereClause + " AND VRNO_VEHREGNO LIKE '" + this.mVehicleRegNumber + "' "; 
			}

			if ( this.mCboCoordinator.Length > 0) 
			{
				string coord = this.mCboCoordinator;
				int position = coord.IndexOf("(");
				this.mCboCoordinator = coord.Substring(0, position -1).Trim();

				whereClause = whereClause + " AND COORDINATOR = '" + this.mCboCoordinator + "'";
			}

			if ( this.mCboExternalContractor.Length > 0) 
			{
				whereClause = whereClause + " AND EXTCON = '" + this.mCboExternalContractor + "' "; 
			}

			if ( this.mCboCraftNumber.Length > 0) 
			{
				whereClause = whereClause + " AND CRA_CRAFTNO = '" + this.mCboCraftNumber + "' "; 
			}

			if ( this.mCboSubcontractor.Length > 0) 
			{
				whereClause = whereClause + " AND SUBCON = '" + this.mCboSubcontractor + "' "; 
			}

			if ( this.mCboDepartment.Length > 0) 
			{
				whereClause = whereClause + " AND DEPT_DEPARTMENT = '" + this.mCboDepartment + "' "; 
			}

			if ( this.mCboStatus.Length > 0) 
			{
				whereClause = whereClause + " AND CWR_STATUS = '" + this.mCboStatus + "' "; 
			}

			// ID card number Hitag2
			if ( this.mIDCardNoHitag.Length > 0)
			{
				whereClause += " AND CWR_IDCARDNO LIKE '" + this.mIDCardNoHitag + "' ";
			}

            // ID card number Mifare
            if (this.mIDCardNoMifare.Length > 0)
            {
                whereClause += " AND CWR_MIFARENO LIKE '" + this.mIDCardNoMifare + "' ";
            }

            // Persno.
            if (this.mPersNrSmAct.Length > 0)
            {
                whereClause += " AND CWR_SMARTACTNO LIKE '" + this.mPersNrSmAct + "' ";
            }

            if (this.mPersNrFPASS.Length > 0)
            {
                whereClause += " AND CWR_PERSNO LIKE '" + this.mPersNrFPASS + "' ";
            }

			return pSelect + whereClause;
		}


		#endregion // End of CoWorker

		#region Receptionauthorize

		/// <summary>
		/// Execute SQL query to get IDs of all coworkers that match search criteria 
		/// given in search fields in GUI dealing with coworker receptionauthorize attributes
		/// (Database view: VW_FPASS_RECEPTIONAUTHORIZE)
		/// Step 1: read given values of search fields in GUI & generate WHERE clause of SQL text
		/// Step 2: if search criteria have been selected, create & execute SQL query on DB
		/// Add each coworker ID in resultset to hashtable
		/// </summary>
		private void GetRecAuthorizeIds() 
		{
			this.CopyOutSearchCriteriaRecAuthorize();

			/*if search criteria selected the receptionauthorize hashtable will be filled
			 */
			if(mSearchCriteriaReceptionauthor) 
			{
				// Get DataProvider from DbAccess component
				IProvider mProvider = DBSingleton.GetInstance().DataProvider;
				IDbCommand selComm  = mProvider.CreateCommand(RECEPTIONAUTHO_QUERY);			
				selComm.CommandText = GenerateWhereClauseReceptauthorize(selComm.CommandText);
			
				// Instantiate HashTable
				mReceptionauthorize	= new Hashtable();

				// Open data reader to get CoWorker data, Loop thru records and create HashTable of CoWorker IDs
				IDataReader mDR = mProvider.GetReader(selComm);
				while (mDR.Read())
				{
					mCoworkerIDForReception = Convert.ToDecimal(mDR["CWR_ID"]);
					if (mReceptionauthorize[mCoworkerIDForReception] == null)
					{
						mReceptionauthorize.Add(mCoworkerIDForReception, mDR["CWR_ID"]);
					}
				}
				mDR.Close();
			
				// Set flag: were any coworkers found?
				if ( mReceptionauthorize.Count > 0 ) 
				{
					mReceptionauthorizeTrue = true;	
				} 
				else 
				{
					mReceptionauthorizeTrue = false;
				}
			}
		}


		/// <summary>
		/// Generate text for the SQL WHERE clause (DB View VW_FPASS_RECEPTIONAUTHORIZE): 
		/// The more values in the GUI fields the more search criteria are set
		/// Mandant ID not set here, therefore initial "WHERE" added in CopyOut above
		/// Put in to_date() 17.03.04
		/// patch 11.03.2005: change .Length > 1 to .Length > = so '*' is considered in search
		/// </summary>
		/// <param name="pSelect">String of SQL statement, WHERE clause is appended to this</param>
		/// <returns>SQL String including WHERE clause</returns>
		/// </summary>
		private String GenerateWhereClauseReceptauthorize(String pSelect) 
		{
			string whereClause = " WHERE CWR_MND_ID = " 
				+ UserManagementControl.getInstance().CurrentMandatorID.ToString();

			// Radiobuttons
			if ( this.mRbtAccessAuthorizationNo == true) 
			{
				whereClause = whereClause + " AND (ACCESS_YN  = 'N' OR ACCESS_YN IS NULL)" ; 
			}

			if ( this.mRbtAccessAuthorizationYes == true) 
			{
				whereClause = whereClause + " AND ACCESS_YN  = 'Y'"; 
			}

			if ( this.mRbtSafetyInstructionsNo == true) 
			{
				whereClause = whereClause + " AND (SECURITY_YN  = 'N' OR SECURITY_YN IS NULL)"; 
			}

			if ( this.mRbtSafetyInstructionsYes == true) 
			{
				whereClause = whereClause + " AND SECURITY_YN  = 'Y'"; 
			}
		
			if ( this.mRbtIndustrialSafetyBriefingNo == true) 
			{
				whereClause = whereClause + " AND (ISBSITE_YN  = 'N' OR ISBSITE_YN IS NULL)"; 
			}

			if ( this.mRbtIndustrialSafetyBriefingYes == true) 
			{
				whereClause = whereClause + " AND ISBSITE_YN  = 'Y'"; 
			}

			// Textboxes			
			if ( this.mTxtAccessAuthorizationBy.Length > 0 ) 
			{
				whereClause = whereClause + " AND UPPER(ACCESSUSER) LIKE '" + this.mTxtAccessAuthorizationBy.ToUpper() + "'"; 
			}

			if ( this.mTxtAccessAuthorizationOn.Length > 0 ) 
			{
				whereClause = whereClause + " AND TRUNC(ACCESSDATE) = TO_DATE('" 
					+ this.mTxtAccessAuthorizationOn 
					+ "', 'DD.MM.YYYY')"; 
			}

			if ( this.mTxtSafetyInstructionsBy.Length > 0 ) 
			{
				whereClause = whereClause + " AND UPPER(SECURITYUSER) LIKE '" + this.mTxtSafetyInstructionsBy.ToUpper() + "' "; 
			}

			if ( this.mTxtSafetyInstructionsOn.Length > 0 ) 
			{
				whereClause = whereClause + " AND TRUNC(SECURITYDATE) = TO_DATE('" 
					+ this.mTxtSafetyInstructionsOn 
					+ "', 'DD.MM.YYYY') "; 
			}

			if ( this.mTxtIndustrialSafetyBriefingBy.Length > 0 ) 
			{
				whereClause = whereClause + " AND UPPER(ISBSITEUSER) LIKE '" + this.mTxtIndustrialSafetyBriefingBy.ToUpper() + "' "; 
			}

			if ( this.mTxtIndustrialSafetyBriefingOn.Length > 0) 
			{
				whereClause = whereClause + " AND TRUNC(ISBSITEDATE) = TO_DATE('" 
					+ this.mTxtIndustrialSafetyBriefingOn 
					+ "', 'DD.MM.YYYY') "; 
			}
			return pSelect + whereClause;
		}


		/// <summary>
		/// Get values for search criteria:
		/// given in search fields in block in GUI dealing with coworker reception authorized attributes
		/// Set flag if no search criteria for coworker reception authorized attributes
		/// Update 07.03.2005: Changed logic of flag: 
		/// As soon as any search criterion is found, flag noSearchCriteria is set to false (-1)
		/// </summary>
		private void CopyOutSearchCriteriaRecAuthorize() 
		{
			// Re-initialise variables
			int	noSearchCriteria		   = 1;
			mSearchCriteriaReceptionauthor = true;

			this.mTxtAccessAuthorizationOn = ((FrmExtendedSearch) mView).TxtAccessAuthorizationOn.Text;
			if ( mTxtAccessAuthorizationOn.Length > 0 ) 
			{
				noSearchCriteria = -1;
			}

			this.mTxtAccessAuthorizationBy = ((FrmExtendedSearch) mView).TxtAccessAuthorizationBy.Text.Trim().Replace("*","%");
			if ( mTxtAccessAuthorizationBy.Length > 0 ) 
			{
				noSearchCriteria = -1;
			}
			
			this.mRbtAccessAuthorizationYes = ((FrmExtendedSearch) mView).RbtAccessAuthorizationYes.Checked;
			if ( mRbtAccessAuthorizationYes ) 
			{
				noSearchCriteria = -1;
			}

			this.mRbtAccessAuthorizationNo = ((FrmExtendedSearch) mView).RbtAccessAuthorizationNo.Checked;
			if ( mRbtAccessAuthorizationNo ) 
			{
				noSearchCriteria = -1;	
			}	

			this.mRbtSafetyInstructionsYes = ((FrmExtendedSearch) mView).RbtSafetyInstructionsYes.Checked;
			if ( mRbtSafetyInstructionsYes ) 
			{
				noSearchCriteria = -1;
			}

			this.mRbtSafetyInstructionsNo = ((FrmExtendedSearch) mView).RbtSafetyInstructionsNo.Checked;
			if ( mRbtSafetyInstructionsNo ) 
			{
				noSearchCriteria = -1;
			}

			this.mTxtSafetyInstructionsOn = ((FrmExtendedSearch) mView).TxtSafetyInstructionsOn.Text.Trim();
			if ( mTxtSafetyInstructionsOn.Length > 0 ) 
			{
				noSearchCriteria = -1;
			}

			this.mTxtSafetyInstructionsBy = ((FrmExtendedSearch) mView).TxtSafetyInstructionsBy.Text.Trim().Replace("*","%");
			if ( mTxtSafetyInstructionsBy.Length > 0 ) 
			{
				noSearchCriteria = -1;
			}

            //this.mRbtSignaturYes = ((FrmExtendedSearch) mView).RbtSignaturYes.Checked;
            //if ( mRbtSignaturYes ) 
            //{
            //    noSearchCriteria = -1;
            //}

            //this.mRbtSignaturNo = ((FrmExtendedSearch) mView).RbtSignaturNo.Checked;
            //if ( mRbtSignaturNo ) 
            //{
            //    noSearchCriteria = -1;
            //}

            //this.mTxtSignatureOn = ((FrmExtendedSearch) mView).TxtSignatureOn.Text.Trim();
            //if ( mTxtSignatureOn.Length > 0 ) 
            //{
            //    noSearchCriteria = -1;
            //}

            //this.mTxtSignatureBy = ((FrmExtendedSearch) mView).TxtSignatureBy.Text.Trim().Replace("*","%");
            //if ( mTxtSignatureBy.Length > 0 ) 
            //{
            //    noSearchCriteria = -1;
            //}

			this.mTxtIndustrialSafetyBriefingBy = ((FrmExtendedSearch) mView).TxtIndustrialSafetyBriefingBy.Text.Trim().Replace("*","%");
			if ( mTxtIndustrialSafetyBriefingBy.Length > 0 ) 
			{
				noSearchCriteria = -1;
			}

			this.mRbtIndustrialSafetyBriefingYes = ((FrmExtendedSearch) mView).RbtIndustrialSafetyBriefingYes.Checked;
			if ( mRbtIndustrialSafetyBriefingYes ) 
			{
				noSearchCriteria = -1;
			}

			this.mTxtIndustrialSafetyBriefingOn = ((FrmExtendedSearch) mView).TxtIndustrialSafetyBriefingOn.Text.Trim();
			if ( mTxtIndustrialSafetyBriefingOn.Length > 0 ) 
			{
				noSearchCriteria = -1;
			}

			this.mRbtIndustrialSafetyBriefingNo = ((FrmExtendedSearch) mView).RbtIndustrialSafetyBriefingNo.Checked;
			if ( mRbtIndustrialSafetyBriefingNo ) 
			{
				noSearchCriteria = -1;
			}

			// Only interested if any search criterium set, not how many
			if ( noSearchCriteria == 1 )
				mSearchCriteriaReceptionauthor = false; 		
		}

		
		#endregion // End of Receptionauthorize
		
		#region CwrBriefing

		
		/// <summary>
		/// Execute SQL query to get IDs of all coworkers that match search criteria 
		/// given in search fields in GUI dealing with coworker briefings
		/// (Database view: VW_FPASS_CWRBRIEFING)
		/// Step 1: read given values of search fields in GUI & generate WHERE clause of SQL text
		/// Step 2: if search criteria have been selected, create & execute SQL query on DB
		/// Add each coworker ID in resultset to hashtable
		/// </summary>
		private void GetCwrBriefingIds() 
		{
			this.CopyOutSearchCriteriaCwrBriefing();

			/*if search criteria selected the cwrbriefing hashtable will be filled
			 */
			if(mSearchCriteriaCwrbriefing)
			{
				// Get DataProvider, Create the select command & fill Data Reader 
				IProvider mProvider = DBSingleton.GetInstance().DataProvider;
				IDbCommand selComm  = mProvider.CreateCommand(CWRBRIEFING_QUERY);			
				selComm.CommandText = GenerateWhereClauseBriefing(selComm.CommandText);
			
				// Instantiate HashTable
				mCwrbriefing	= new Hashtable();

				// Open data reader, create an HashTable of CoWorker IDs
				IDataReader mDR = mProvider.GetReader(selComm); 

				while (mDR.Read())
				{
					mCoworkerIDForBriefing = Convert.ToDecimal(mDR["CWR_ID"]);
					if (mCwrbriefing[mCoworkerIDForBriefing] == null)
					{
						mCwrbriefing.Add(mCoworkerIDForBriefing, mDR["CWR_ID"]);
					}
				}
				mDR.Close();

				// Set flag: were any coworker IDs found?
				if ( mCwrbriefing.Count > 0 ) 
				{
					mCwrbriefingTrue = true;	
				} 
				else 
				{
					mCwrbriefingTrue = false;
				}
			}
		}

		
		/// <summary>
		/// Generate text for SQL WHERE clause (DB View VW_FPASS_CWRBRIEFING): 
		/// The more values in the GUI fields the more search criteria are set
		/// (Mandant ID is always set)
		/// Put to_date() in 16.03.04
		/// patch 11.03.2005: change txtbox.Length > 1 to .Length > 0 so '*' is considered in search
		/// </summary>
		/// <param name="pSelect">String of SQL statement, WHERE clause is appended to this</param>
		/// <returns>SQL String including WHERE clause</returns>
		/// </summary>
		private String GenerateWhereClauseBriefing(String pSelect) 
		{
			String	whereClause = "";
			whereClause = " WHERE CWR_MND_ID = '" + UserManagementControl.getInstance().CurrentMandatorID +"'";
			
			// Radiobuttons
			if ( this.mRbtBreathingApparatusG262BriefingNo == true) 
			{
				whereClause = whereClause + " AND WEARBRIAPPARG26_2YN  = 'Y'";
			}

			if ( this.mRbtBreathingApparatusG262BriefingYes == true) 
			{
				whereClause = whereClause + " AND WEARBRIAPPARG26_2YN  = 'N'"; 
			}

			if ( this.mRbtBreathingApparatusG263BriefingNo == true) 
			{
				whereClause = whereClause + " AND WEARBRIAPPARG26_3YN  = 'Y'"; 
			}

			if ( this.mRbtBreathingApparatusG263BriefingYes == true) 
			{
				whereClause = whereClause + " AND WEARBRIAPPARG26_3YN  = 'N'"; 
			}

			if ( this.mRbtFiremanBriefingNo == true) 
			{
				whereClause = whereClause + " AND FIREBRIEFYN  = 'Y'"; 
			}

			if ( this.mRbtFiremanBriefingYes == true) 
			{
				whereClause = whereClause + " AND FIREBRIEFYN  = 'N'"; 
			}
			
			if ( this.mRbtCranesNo == true) 
			{
				whereClause = whereClause + " AND CRANELIFTGEAR_YN  = 'Y'"; 
			}

			if ( this.mRbtCranesYes == true) 
			{
				whereClause = whereClause + " AND CRANELIFTGEAR_YN  = 'N'"; 
			}

			if (mRbtIdPhotoHitagNo) 
			{
				whereClause = whereClause + " AND IDENTIFYCARD_YN  = 'Y'"; 
			}

			if (mRbtIdPhotoHitagYes) 
			{
				whereClause = whereClause + " AND IDENTIFYCARD_YN  = 'N'"; 
			}

            if (mRbtIdPhotoSmActNo)
            {
                whereClause = whereClause + " AND IDSMACTACT_YN  = 'Y'";
            }

            if (mRbtIdPhotoSmActYes)
            {
                whereClause = whereClause + " AND IDSMACTACT_YN  = 'N'";
            }

			if ( this.mRbtPalletLifterNo == true) 
			{
				whereClause = whereClause + " AND PALLETLIFTER_YN  = 'Y'"; 
			}

			if ( this.mRbtPalletLifterYes == true) 
			{
				whereClause = whereClause + " AND PALLETLIFTER_YN  = 'N'"; 
			}

			if ( this.mRbtRaisablePlatformNo == true) 
			{
				whereClause = whereClause + " AND RAISEABLEPLATF_YN  = 'Y'"; 
			}

			if ( this.mRbtRaisablePlatformYes == true) 
			{ 
				whereClause = whereClause + " AND RAISEABLEPLATF_YN  = 'N'"; 
			}

			if ( this.mRbtRespiratoryMaskNo == true) 
			{
				whereClause = whereClause + " AND RESPSITEFIRE_YN  = 'Y'"; 
			}

			if ( this.mRbtRespiratoryMaskYes == true) 
			{
				whereClause = whereClause + " AND RESPSITEFIRE_YN  = 'N'"; 
			}

			if ( this.mRbtSafetyAtWorkServiceBriefingNo == true) 
			{
				whereClause = whereClause + " AND DEPARTBRIEF_YN  = 'Y'"; 
			}

			if ( this.mRbtSafetyAtWorkServiceBriefingYes == true) 
			{
				whereClause = whereClause + " AND DEPARTBRIEF_YN  = 'N'"; 
			}

			if ( this.mRbtSiteSecurityBriefingNo == true) 
			{
				whereClause = whereClause + " AND SITESECURITYBR_YN  = 'Y'"; 
			}

			if ( this.mRbtSiteSecurityBriefingYes == true) 
			{
				whereClause = whereClause + " AND SITESECURITYBR_YN  = 'N'"; 
			}

			if ( this.mRbtVehicleLongCoNo == true) 
			{
				whereClause = whereClause + " AND LONGGWDESIRE  = 'N'"; 
			}

			if ( this.mRbtVehicleLongCoYes == true) 
			{
				whereClause = whereClause + " AND LONGGWDESIRE  = 'Y'"; 
			}

			if ( this.mRbtVehicleLongNo == true) 
			{
				whereClause = whereClause + " AND LONGGATEWAY  = 'Y'"; 
			}

			if ( this.mRbtVehicleLongYes == true) 
			{
				whereClause = whereClause + " AND LONGGATEWAY  = 'N'"; 
			}

			if ( this.mRbtVehicleShortCoNo == true) 
			{
				whereClause = whereClause + " AND SHORTGWDESIRE  = 'N'"; 
			}

			if ( this.mRbtVehicleShortCoYes == true) 
			{
				whereClause = whereClause + " AND SHORTGWDESIRE  = 'Y'"; 
			}

			if ( this.mRbtVehicleShortNo == true) 
			{
				whereClause = whereClause + " AND SHORTGATEWAY  = 'Y'"; 
			}

			if ( this.mRbtVehicleShortYes == true) 
			{
				whereClause = whereClause + " AND SHORTGATEWAY  = 'N'"; 
			}


			// Textboxes
			if ( this.mTxtBreathingApparatusG262BriefingBy.Length > 0 ) 
			{
				whereClause = whereClause + " AND UPPER(WEARBRIAPPARUSERG26_2) LIKE '" + this.mTxtBreathingApparatusG262BriefingBy.ToUpper() + "' "; 
			}

			if ( this.mTxtBreathingApparatusG262BriefingOn.Length > 0 ) 
			{
				whereClause = whereClause + " AND TRUNC(WEARBRIAPPARDATEG26_2) = TO_DATE('"
					+ this.mTxtBreathingApparatusG262BriefingOn 
					+  "', 'DD.MM.YYYY') ";
			}

			if ( this.mTxtBreathingApparatusG263BriefingBy.Length > 0 ) 
			{
				whereClause = whereClause + " AND UPPER(WEARBRIAPPARUSERG26_3) LIKE '" + this.mTxtBreathingApparatusG263BriefingBy.ToUpper() + "' "; 
			}

			if ( this.mTxtBreathingApparatusG263BriefingOn.Length > 0 ) 
			{
				whereClause = whereClause + " AND TRUNC(WEARBRIAPPARDATEG26_3) = TO_DATE('"
					+ this.mTxtBreathingApparatusG263BriefingOn 
					+ "', 'DD.MM.YYYY') ";
			}
			
			if ( this.mTxtFiremanBriefingBy.Length > 0 ) 
			{
				whereClause = whereClause + " AND UPPER(FIREBRIEFUSER) LIKE '" + this.mTxtFiremanBriefingBy.ToUpper() + "' "; 
			}

			if ( this.mTxtFiremanBriefingOn.Length > 0 ) 
			{
				whereClause = whereClause + " AND TRUNC(FIREBRIEFDATE) = TO_DATE('"
					+ this.mTxtFiremanBriefingOn
					+ "', 'DD.MM.YYYY') ";
			}

			if ( this.mTxtCranesBy.Length > 0 ) 
			{
				whereClause = whereClause + " AND UPPER(CRANELIFTGEARUSER) LIKE '" + this.mTxtCranesBy.ToUpper() + "' "; 
			}

			if ( this.mTxtCranesOn.Length > 0 ) 
			{
				whereClause = whereClause + " AND TRUNC(CRANELIFTGEARDATE) = TO_DATE('"
					+ this.mTxtCranesOn 
					+ "', 'DD.MM.YYYY') ";
			}

			if (mTxtIdPhotoHitagBy.Length > 0) 
			{
				whereClause = whereClause + " AND UPPER(IDENTIFYCARDUSER) LIKE '" + mTxtIdPhotoHitagBy.ToUpper() + "' "; 
			}

			if (mTxtIdPhotoHitagOn.Length > 0) 
			{
				whereClause = whereClause + " AND TRUNC(IDENTIFYCARDDATE) = TO_DATE('" + mTxtIdPhotoHitagOn +  "', 'DD.MM.YYYY') ";
			}

            if (mTxtIdPhotoSmActBy.Length > 0)
            {
                whereClause = whereClause + " AND UPPER(IDSMACTACTUSER) LIKE '" + mTxtIdPhotoSmActBy.ToUpper() + "' ";
            }

            if (mTxtIdPhotoSmActOn.Length > 0)
            {
                whereClause = whereClause + " AND TRUNC(IDENTIFYCARDDATE) = TO_DATE('" + mTxtIdPhotoSmActOn + "', 'DD.MM.YYYY') ";
            }

			if ( this.mTxtPalletLifterBy.Length > 0 ) 
			{
				whereClause = whereClause + " AND UPPER(PALLETLIFTERUSER) LIKE '" + this.mTxtPalletLifterBy.ToUpper() + "' "; 
			}

			if ( this.mTxtPalletLifterOn.Length > 0 ) 
			{
				whereClause = whereClause + " AND TRUNC(PALLETLIFTERDATE) = TO_DATE('"
					+ this.mTxtPalletLifterOn 
					+ "', 'DD.MM.YYYY') ";
			}

			if ( this.mTxtRaisablePlatformBy.Length > 0 ) 
			{
				whereClause = whereClause + " AND UPPER(RAISEABLEPLATFUSER) LIKE '" + this.mTxtRaisablePlatformBy.ToUpper() + "' "; 
			}

			if ( this.mTxtRaisablePlatformOn.Length > 0 ) 
			{
				whereClause = whereClause + " AND TRUNC(RAISEABLEPLATFDATE) = TO_DATE('"
					+ this.mTxtRaisablePlatformOn 
					+ "', 'DD.MM.YYYY') ";
			}
			
			if ( this.mTxtRespiratoryMaskBy.Length > 0 ) 
			{
				whereClause = whereClause + " AND UPPER(RESPSITEFIREUSER) LIKE '" + this.mTxtRespiratoryMaskBy.ToUpper() + "' "; 
			}

			if ( this.mTxtRespiratoryMaskOn.Length > 0 ) 
			{
				whereClause = whereClause + " AND TRUNC(RESPSITEFIREDATE) = TO_DATE('"
					+ this.mTxtRespiratoryMaskOn 
					+ "', 'DD.MM.YYYY') ";
			}

			if ( this.mTxtSafetyAtWorkServiceBriefingBy.Length > 0 ) 
			{
				whereClause = whereClause + " AND UPPER(DEPARTBRIEFUSER) LIKE '" + this.mTxtSafetyAtWorkServiceBriefingBy.ToUpper() + "' "; 
			}

			if ( this.mTxtSafetyAtWorkServiceBriefingOn.Length > 0 ) 
			{
				whereClause = whereClause + " AND TRUNC(DEPARTBRIEFDATE) = TO_DATE('"
					+ this.mTxtSafetyAtWorkServiceBriefingOn 
					+ "', 'DD.MM.YYYY') ";
			}

			if ( this.mTxtSiteSecurityBriefingBy.Length > 0 ) 
			{
				whereClause = whereClause + " AND UPPER(SITESECURITYBRUSER) LIKE '" + this.mTxtSiteSecurityBriefingBy.ToUpper() + "' "; 
			}

			if ( this.mTxtSiteSecurityBriefingOn.Length > 0 ) 
			{
				whereClause = whereClause + " AND TRUNC(SITESECURITYBRDATE) = TO_DATE('"
					+ this.mTxtSiteSecurityBriefingOn 
					+ "', 'DD.MM.YYYY') ";
			}
			
			if ( this.mTxtVehicleLongBy.Length > 0 ) 
			{
				whereClause = whereClause + " AND UPPER(LONGGWUSER) LIKE '" + this.mTxtVehicleLongBy.ToUpper() + "' "; 
			}

			if ( this.mTxtVehicleLongOn.Length > 0 ) 
			{
				whereClause = whereClause + " AND TRUNC(LONGGWDATE) = TO_DATE('"
					+ this.mTxtVehicleLongOn 
					+ "', 'DD.MM.YYYY') ";
			}
			
			if ( this.mTxtVehicleShortBy.Length > 0 ) 
			{
				whereClause = whereClause + " AND UPPER(SHORTGWUSER) LIKE '" + this.mTxtVehicleShortBy.ToUpper() + "' "; 
			}

			if ( this.mTxtVehicleShortOn.Length > 0 ) 
			{
				whereClause = whereClause + " AND TRUNC(SHORTGWDATE) = TO_DATE('"
					+ this.mTxtVehicleShortOn 
					+ "', 'DD.MM.YYYY') ";
			}
			return pSelect + whereClause;
		}


		
		/// <summary>
		/// Get values for search criteria:
		/// given in search fields in block in GUI dealing with coworker briefing attributes
		/// Update 03.03.2005: Changed logic of flag: 
		/// As soon as any search criterion is found, flag noSearchCriteria is set to false (-1)
		/// </summary>
		private void CopyOutSearchCriteriaCwrBriefing() 
		{
			int	noSearchCriteria = 1;
			mSearchCriteriaCwrbriefing = true;

            mViewXtend = (FrmExtendedSearch)mView;

			mTxtVehicleShortBy = mViewXtend.TxtVehicleShortBy.Text.Trim().Replace("*","%");
			if ( mTxtVehicleShortBy.Length > 0 ) 
			{
				noSearchCriteria = -1;
			}

			mTxtVehicleShortOn = mViewXtend.TxtVehicleShortOn.Text.Trim();
			if ( mTxtVehicleShortOn.Length > 0 ) 
			{
				noSearchCriteria = -1;
			}

			mTxtVehicleLongOn = mViewXtend.TxtVehicleLongOn.Text.Trim();
			if ( mTxtVehicleLongOn.Length > 0 ) 
			{
				noSearchCriteria = -1;
			}

			mTxtVehicleLongBy = mViewXtend.TxtVehicleLongBy.Text.Trim().Replace("*","%");
			if ( mTxtVehicleLongBy.Length > 0 ) 
			{
				noSearchCriteria = -1;
			}

			mTxtSiteSecurityBriefingBy = mViewXtend.TxtSiteSecurityBriefingBy.Text.Trim().Replace("*","%");
			if ( mTxtSiteSecurityBriefingBy.Length > 0 ) 
			{
				noSearchCriteria = -1;
			}

			mTxtSafetyAtWorkServiceBriefingBy = mViewXtend.TxtSafetyAtWorkServiceBriefingBy.Text.Trim().Replace("*","%");
			if ( mTxtSafetyAtWorkServiceBriefingBy.Length > 0 ) 
			{
				noSearchCriteria = -1;
			}

			mTxtIdPhotoHitagBy = mViewXtend.TxtIdHitagBy.Text.Trim().Replace("*","%");
			if (mTxtIdPhotoHitagBy.Length > 0) 
			{
				noSearchCriteria = -1;
			}

            mTxtIdPhotoSmActBy = mViewXtend.TxtIdSmActBy.Text.Trim().Replace("*", "%");
            if (mTxtIdPhotoSmActBy.Length > 0)
            {
                noSearchCriteria = -1;
            }

			mTxtBreathingApparatusG262BriefingBy = mViewXtend.TxtBreathingApparatusG262BriefingBy.Text.Trim().Replace("*","%");
			if ( mTxtBreathingApparatusG262BriefingBy.Length > 0 ) 
			{
				noSearchCriteria = -1;
			}

			mTxtBreathingApparatusG263BriefingBy = mViewXtend.TxtBreathingApparatusBriefingG263By.Text.Trim().Replace("*","%");
			if ( mTxtBreathingApparatusG263BriefingBy.Length > 0 ) 
			{
				noSearchCriteria = -1;
			}

			mTxtFiremanBriefingBy = mViewXtend.TxtFireBriefingBy.Text.Trim().Replace("*","%");
			if ( mTxtFiremanBriefingBy.Length > 0 ) 
			{
				noSearchCriteria = -1;
			}

			mTxtPalletLifterBy = mViewXtend.TxtPalletLifterBy.Text.Trim().Replace("*","%");
			if ( mTxtPalletLifterBy.Length > 0 ) 
			{
				noSearchCriteria = -1;
			}

			mTxtRaisablePlatformBy = mViewXtend.TxtRaisablePlatformBy.Text.Trim().Replace("*","%");
			if ( mTxtRaisablePlatformBy.Length > 0 ) 
			{
				noSearchCriteria = -1;
			}
			
			mTxtCranesBy = mViewXtend.TxtCranesBy.Text.Trim().Replace("*","%");
			if ( mTxtCranesBy.Length > 0 ) 
			{
				noSearchCriteria = -1;
			}

			mTxtRespiratoryMaskBy = mViewXtend.TxtRespiratoryMaskBy.Text.Trim().Replace("*","%");
			if ( mTxtRespiratoryMaskBy.Length > 0 ) 
			{
				noSearchCriteria = -1;
			}
			
			mRbtRaisablePlatformYes = mViewXtend.RbtRaisablePlatformYes.Checked;
			if ( mRbtRaisablePlatformYes ) 
			{
				noSearchCriteria = -1;
			}

			mRbtRaisablePlatformNo = mViewXtend.RbtRaisablePlatformNo.Checked;
			if ( mRbtRaisablePlatformNo ) 
			{
				noSearchCriteria = -1;	
			}	

			mRbtBreathingApparatusG262BriefingYes = mViewXtend.RbtBreathingApparatusG262BriefingYes.Checked;
			if ( mRbtBreathingApparatusG262BriefingYes ) 
			{
				noSearchCriteria = -1;
			}

			mRbtBreathingApparatusG262BriefingNo = mViewXtend.RbtBreathingApparatusG262BriefingNo.Checked;
			if ( mRbtBreathingApparatusG262BriefingNo ) 
			{
				noSearchCriteria = -1;
			}

			mRbtBreathingApparatusG263BriefingYes = mViewXtend.RbtBreathingApparatusG263BriefingYes.Checked;
			if ( mRbtBreathingApparatusG263BriefingYes ) 
			{
				noSearchCriteria = -1;
			}

			mRbtBreathingApparatusG263BriefingNo = mViewXtend.RbtBreathingApparatusG263BriefingNo.Checked;
			if ( mRbtBreathingApparatusG263BriefingNo ) 
			{
				noSearchCriteria = -1;
			}

			mRbtFiremanBriefingYes = mViewXtend.RbtFireBriefingYes.Checked;
			if ( mRbtFiremanBriefingYes ) 
			{
				noSearchCriteria = -1;
			}

			mRbtFiremanBriefingNo = mViewXtend.RbtFireBriefingNo.Checked;
			if ( mRbtFiremanBriefingNo ) 
			{
				noSearchCriteria = -1;
			}

			mRbtPalletLifterYes = mViewXtend.RbtPalletLifterYes.Checked;
			if ( mRbtPalletLifterYes ) 
			{
				noSearchCriteria = -1;
			}

			mRbtPalletLifterNo = mViewXtend.RbtPalletLifterNo.Checked;
			if ( mRbtPalletLifterNo ) 
			{
				noSearchCriteria = -1;
			}

			mRbtCranesYes = mViewXtend.RbtCranesYes.Checked;
			if ( mRbtCranesYes ) 
			{
				noSearchCriteria = -1;
			}

			mRbtCranesNo = mViewXtend.RbtCranesNo.Checked;
			if ( mRbtCranesNo ) 
			{
				noSearchCriteria = -1;
			}

			mRbtVehicleShortCoYes = mViewXtend.RbtVehicleShortCoYes.Checked;
			if ( mRbtVehicleShortCoYes ) 
			{
				noSearchCriteria = -1;
			}

			mRbtVehicleShortCoNo = mViewXtend.RbtVehicleShortCoNo.Checked;
			if ( mRbtVehicleShortCoNo ) 
			{
				noSearchCriteria = -1;
			}

			mRbtVehicleShortYes = mViewXtend.RbtVehicleShortYes.Checked;
			if ( mRbtVehicleShortYes ) 
			{
				noSearchCriteria = -1;
			}

			mRbtVehicleShortNo = mViewXtend.RbtVehicleShortNo.Checked;
			if ( mRbtVehicleShortNo ) 
			{
				noSearchCriteria = -1;
			}

			mRbtVehicleLongCoYes = mViewXtend.RbtVehicleLongCoYes.Checked;
			if ( mRbtVehicleLongCoYes ) 
			{
				noSearchCriteria = -1;
			}

			mRbtVehicleLongCoNo = mViewXtend.RbtVehicleLongCoNo.Checked;
			if ( mRbtVehicleLongCoNo ) 
			{
				noSearchCriteria = -1;
			}

			mRbtVehicleLongYes = mViewXtend.RbtVehicleLongYes.Checked;
			if ( mRbtVehicleLongYes ) 
			{
				noSearchCriteria = -1;
			}

			mRbtVehicleLongNo = mViewXtend.RbtVehicleLongNo.Checked;
			if ( mRbtVehicleLongNo ) 
			{
				noSearchCriteria = -1;
			}

			mRbtIdPhotoHitagYes = mViewXtend.RbtIdHitagYes.Checked;
			if ( mRbtIdPhotoHitagYes ) 
			{
				noSearchCriteria = -1;
			}

			mRbtIdPhotoHitagNo = mViewXtend.RbtIdHitagNo.Checked;
			if ( mRbtIdPhotoHitagNo ) 
			{
				noSearchCriteria = -1;	
			}

            mRbtIdPhotoSmActYes = mViewXtend.RbtIdSmActYes.Checked;
            if (mRbtIdPhotoSmActYes)
            {
                noSearchCriteria = -1;
            }

            mRbtIdPhotoSmActNo = mViewXtend.RbtIdSmActNo.Checked;
            if (mRbtIdPhotoSmActNo)
            {
                noSearchCriteria = -1;
            }	

			mRbtSafetyAtWorkServiceBriefingYes = mViewXtend.RbtSafetyAtWorkServiceBriefingYes.Checked;
			if ( mRbtSafetyAtWorkServiceBriefingYes ) 
			{
				noSearchCriteria = -1;
			}

			mRbtSafetyAtWorkServiceBriefingNo = mViewXtend.RbtSafetyAtWorkServiceBriefingNo.Checked;
			if ( mRbtSafetyAtWorkServiceBriefingNo ) 
			{
				noSearchCriteria = -1;
			}

			mRbtSiteSecurityBriefingNo = mViewXtend.RbtSiteSecurityBriefingNo.Checked;
			if ( mRbtSiteSecurityBriefingNo ) 
			{
				noSearchCriteria = -1;
			}

			mRbtSiteSecurityBriefingYes = mViewXtend.RbtSiteSecurityBriefingYes.Checked;
			if ( mRbtSiteSecurityBriefingYes ) 
			{
				noSearchCriteria = -1;
			}

			mRbtRespiratoryMaskNo = mViewXtend.RbtRespiratoryMaskNo.Checked;
			if ( mRbtRespiratoryMaskNo ) 
			{
				noSearchCriteria = -1;
			}

			mRbtRespiratoryMaskYes = mViewXtend.RbtRespiratoryMaskYes.Checked;
			if ( mRbtRespiratoryMaskYes ) 
			{
				noSearchCriteria = -1;	
			}	

			mTxtSafetyAtWorkServiceBriefingOn = mViewXtend.TxtSafetyAtWorkServiceBriefingOn.Text.Trim();
			if ( mTxtSafetyAtWorkServiceBriefingOn.Length > 0 ) 
			{
				noSearchCriteria = -1;
			}

			mTxtSiteSecurityBriefingOn = mViewXtend.TxtSiteSecurityBriefingOn.Text.Trim();
			if ( mTxtSiteSecurityBriefingOn.Length > 0 ) 
			{
				noSearchCriteria = -1;
			}

			mTxtIdPhotoHitagOn = mViewXtend.TxtIdHitagOn.Text.Trim();
			if ( mTxtIdPhotoHitagOn.Length > 0 ) 
			{
				noSearchCriteria = -1;
			}

            mTxtIdPhotoSmActOn = mViewXtend.TxtIdSmActOn.Text.Trim();
            if (mTxtIdPhotoSmActOn.Length > 0)
            {
                noSearchCriteria = -1;
            }

			mTxtBreathingApparatusG262BriefingOn = mViewXtend.TxtBreathingApparatusBriefingg262On.Text.Trim();
			if ( mTxtBreathingApparatusG262BriefingOn.Length > 0 ) 
			{
				noSearchCriteria = -1;
			}

			mTxtBreathingApparatusG263BriefingOn = mViewXtend.TxtBreathingApparatusBriefingG263On.Text.Trim();
			if ( mTxtBreathingApparatusG263BriefingOn.Length > 0 ) 
			{
				noSearchCriteria = -1;
			}

			// New 29.04
			mTxtFiremanBriefingOn = mViewXtend.TxtFireBriefingOn.Text.Trim();
			if ( mTxtFiremanBriefingOn.Length > 0 ) 
			{
				noSearchCriteria = -1;
			}

			mTxtPalletLifterOn = mViewXtend.TxtPalletLifterOn.Text.Trim();
			if ( mTxtPalletLifterOn.Length > 0 ) 
			{
				noSearchCriteria = -1;
			}

			mTxtRaisablePlatformOn = mViewXtend.TxtRaisablePlatformOn.Text.Trim();
			if ( mTxtRaisablePlatformOn.Length > 0 ) 
			{
				noSearchCriteria = -1;
			}

			mTxtCranesOn = mViewXtend.TxtCranesOn.Text.Trim();
			if ( mTxtCranesOn.Length > 0 ) 
			{
				noSearchCriteria = -1;
			}

			mTxtRespiratoryMaskOn = mViewXtend.TxtRespiratoryMaskOn.Text.Trim();
			if ( mTxtRespiratoryMaskOn.Length > 0 ) 
			{
				noSearchCriteria = -1;
			}

			/// Changed logic 03.03.2005 after error:
			/// only interested if any search criterion set, not how many
			if ( noSearchCriteria == 1)
				mSearchCriteriaCwrbriefing = false;

//			// Set flag false if no search criteria selected
//			if ( noSearchCriteria == 48 ) 
//			{
//				mSearchCriteriaCwrbriefing = false;
//			} 			
		}


		#endregion // End of CwrBriefing

		#region PlantMaskMed


		/// <summary>
		/// Execute SQL query to get IDs of all coworkers that match the plants, medicals & respmask numbers given in GUI
		/// (Database view: VW_FPASS_PLANTMASKMED)
		/// Step 1: read given values of search fields in GUI & generate WHERE clause of SQL text
		/// Step 2: if search criteria have been selected, create & execute SQL query on DB
		/// Add each coworker ID in resultset to hashtable
		/// </summary>
		private void GetPlantMaskMedIds() 
		{
			this.CopyOutSearchCriteriaPlantMaskMed();

			/*if search criteria selected the plantmaskmed hashtable is filled
			 */
			if(mSearchCriteriaPlantMaskMed)
			{
				// Get DataProvider, Create select command 
				IProvider mProvider = DBSingleton.GetInstance().DataProvider;
				IDbCommand selComm  = mProvider.CreateCommand(PLANTMASKMED_QUERY);			
				selComm.CommandText = GenerateWhereClausePlantMaskMed(selComm.CommandText);
			
				// Instantiate HashTable
				mPlantMaskMed	= new Hashtable();

				// Open data reader, create HashTable of CoWorker IDs
				IDataReader mDR = mProvider.GetReader(selComm);

				while (mDR.Read())
				{	
					mCoworkerIDForPlantMaskMed = Convert.ToDecimal(mDR["CWR_ID"]);
					if (mPlantMaskMed[mCoworkerIDForPlantMaskMed] == null)
					{
						mPlantMaskMed.Add(mCoworkerIDForPlantMaskMed, mDR["CWR_ID"]);
					}
				}
				mDR.Close();

				// Set flag: were any coworkers found?
				if ( mPlantMaskMed.Count > 0 ) 
				{
					mPlantMaskMedTrue = true;	
				} 
				else 
				{
					mPlantMaskMedTrue = false;
				}
			}
		}


		/// <summary>
		/// Generate text for the SQL WHERE clause (DB View VW_FPASS_PLANTMASKMED): 
		/// The more values in the GUI fields the more search criteria are set
		/// (Mandant ID is always set)
		/// Up to 3 plants can be selected, build SQL WHERE accordingly
		/// 08.03.04: Get ID of prec medical rather than name
		/// 16.03.04: Put in to_date()
		/// patch 11.03.2005: change .Length > 1 to .Length > = so '*' is considered in search
		/// </summary>
		/// <param name="pSelect">String of SQL statement, WHERE clause is appended to this</param>
		/// <returns>SQL String including WHERE clause</returns>
		/// </summary>
		private String GenerateWhereClausePlantMaskMed(String pSelect) 
		{
			// Radiobuttons
			String	whereClause = "";
			String  whereClausePlant1 = "";
			String  whereClausePlant2 = "";
			String  s  = "";
			String  QueryPlant2 = "";
			String  QueryPlant3 = "";

			whereClause = " WHERE CWR_MND_ID = '" + UserManagementControl.getInstance().CurrentMandatorID +"'";

			if ( this.mRbtBriefingPlantNo == true) 
			{
				whereClause = whereClause + " AND CWPL_INACTIVE_YN  = 'Y'"; 
			}

			if ( this.mRbtBriefingPlantYes == true) 
			{
				whereClause = whereClause + " AND CWPL_INACTIVE_YN  = 'N'"; 
			}

			if ( this.mRbtPrecautionaryMedicalNo == true) 
			{
				whereClause = whereClause + " AND PREMEDEXECUTED  = 'Y'"; 
			}

			if ( this.mRbtPrecautionaryMedicalYes == true) 
			{
				whereClause = whereClause + " AND PREMEDEXECUTED  = 'N'"; 
			}

			// Textboxes
			if ( this.mTxtBriefingPlantBy.Length > 0 ) 
			{
				whereClause = whereClause + " AND UPPER(CWPLUSER) LIKE '" + this.mTxtBriefingPlantBy.ToUpper() + "' "; 
			}

			if ( this.mTxtBriefingPlantOn.Length > 0 ) 
			{
				whereClause = whereClause + " AND TRUNC(CWPL_PLANTDATE) = TO_DATE('"
					+ this.mTxtBriefingPlantOn 
					+ "', 'DD.MM.YYYY') "; 
			}

            // Logic for checking if masks open
            bool isLentOpen = false;

            if (this.mTxtMaskNumberRecieve.Length > 0)
            {
                // Mask lent
                whereClause = whereClause + " AND MASKNORECIEVE LIKE '" + this.mTxtMaskNumberRecieve + "' ";
                isLentOpen = true;
            }

			if ( this.mTxtMaskNumberDelivered.Length > 0 ) 
			{
				whereClause = whereClause + " AND MASKNODELIVERED LIKE '" + this.mTxtMaskNumberDelivered + "' ";
                if (isLentOpen) isLentOpen = false;
			}

            if (isLentOpen)
                whereClause = whereClause + " AND MASKNODELIVERED is null "; 


			if ( this.mCboPrecautionaryMedical.Length > 0 ) 
			{
				whereClause = whereClause + " AND PMTY_ID = " + this.mCboPrecautionaryMedical; 
			}

			if ( this.mCboPlantOne.Length > 0 ) 
			{
				whereClausePlant1 = whereClause + " AND PL_NAME = '" + this.mCboPlantOne + "' "; 
			}

			if (this.mCboPlantTwo.Length > 0 ) 
			{
				if ( this.mCboPlantOne.Length > 0) 
				{
					QueryPlant2 = pSelect + whereClausePlant1 + " INTERSECT " + pSelect  + whereClause + " AND PL_NAME =  '" + this.mCboPlantTwo + "'";
 				}
				else
				{
					whereClausePlant2 = whereClause + " AND PL_NAME = '" + this.mCboPlantTwo + "' "; 
				}
			}

			if ( this.mCboPlantThree.Length > 0 ) 
			{
				if (QueryPlant2!="")
				{
					QueryPlant3 = QueryPlant2 + " INTERSECT " + pSelect  + whereClause + " AND PL_NAME =  '" + this.mCboPlantThree + "'";
				}
				else if ( this.mCboPlantTwo.Length > 0) 
				{
					QueryPlant3 = pSelect + whereClausePlant2 + " INTERSECT " + pSelect  + whereClause + " AND PL_NAME =  '" + this.mCboPlantThree + "'";
				}
				else
				{
					whereClause = whereClause + " AND PL_NAME = '" + this.mCboPlantThree + "' "; 
				}
			}
			
			/* check how many plants are selected (up to 3), if it is true the select 
			 * commands must be join with an intersect
			 */
			
			if ( whereClausePlant1 != "" )
			{
				s = pSelect + whereClausePlant1;
			}
			else if (whereClausePlant2 != "")
			{
				s = pSelect + whereClausePlant2;
			}
			else if (whereClausePlant1=="" && whereClausePlant2=="")
			{
				s = pSelect + whereClause;
			}
			
			if (QueryPlant3!="")
			{
				return QueryPlant3;
			}
			else
			{
				if (QueryPlant2!="")
				{
					return QueryPlant2;
				}
				else
				{
					return s;
				}
			}
		}
		


		/// <summary>
		/// Get values for search criteria:
		/// given in search fields plants, medicals & respmask numbers in GUI
		/// Set flag if no search criteria for general coworker attributes
		/// 08.03.04: Get ID of prec medical rather than name
		/// </summary>
		private void CopyOutSearchCriteriaPlantMaskMed() 
		{
			int	noSearchCriteria		= 0;
			mSearchCriteriaPlantMaskMed = true;

			mCboPrecautionaryMedical = this.GetSelectedIDFromCbo(((FrmExtendedSearch) mView).CboPrecautionaryMedical).Trim();
			if ( mCboPrecautionaryMedical.Length < 1 ) 
			{
				noSearchCriteria ++;
			}


			this.mTxtMaskNumberDelivered = ((FrmExtendedSearch) mView).TxtMaskNumberDelivered.Text.Trim().Replace("*","%");
			if ( mTxtMaskNumberDelivered.Length < 1 ) 
			{
				noSearchCriteria ++;
			}

			this.mTxtMaskNumberRecieve = ((FrmExtendedSearch) mView).TxtMaskNumberRecieve.Text.Trim().Replace("*","%");
			if ( mTxtMaskNumberRecieve.Length < 1 ) 
			{
				noSearchCriteria ++;
			}

			this.mTxtBriefingPlantBy = ((FrmExtendedSearch) mView).TxtBriefingPlantBy.Text.Trim().Replace("*","%");
			if ( mTxtBriefingPlantBy.Length < 1 ) 
			{
				noSearchCriteria ++;
			}
		
			this.mRbtPrecautionaryMedicalYes = ((FrmExtendedSearch) mView).RbtPrecautionaryMedicalYes.Checked;
			if ( mRbtPrecautionaryMedicalYes == false ) 
			{
				noSearchCriteria ++;
			}

			this.mRbtPrecautionaryMedicalNo = ((FrmExtendedSearch) mView).RbtPrecautionaryMedicalNo.Checked;
			if ( mRbtPrecautionaryMedicalNo == false ) 
			{
				noSearchCriteria ++;
			}

			this.mRbtBriefingPlantNo = ((FrmExtendedSearch) mView).RbtBriefingPlantNo.Checked;
			if ( mRbtBriefingPlantNo == false ) 
			{
				noSearchCriteria ++;
			}

			this.mRbtBriefingPlantYes = ((FrmExtendedSearch) mView).RbtBriefingPlantYes.Checked;
			if ( mRbtBriefingPlantYes == false ) 
			{
				noSearchCriteria ++;
			}
		
			this.mTxtBriefingPlantOn = ((FrmExtendedSearch) mView).TxtBriefingPlantOn.Text.Trim();
			if ( mTxtBriefingPlantOn.Length < 1 ) 
			{
				noSearchCriteria ++;
			}

			this.mCboPlantOne = this.GetSelectedValueFromCbo(((FrmExtendedSearch) mView).CboPlantOne).Trim();
			if ( mCboPlantOne.Length < 1 ) 
			{
				noSearchCriteria ++;
			}
			
			mCboPlantTwo = this.GetSelectedValueFromCbo(((FrmExtendedSearch) mView).CboPlantTwo).Trim();
			if ( mCboPlantTwo.Length < 1 ) 
			{
				noSearchCriteria ++;
			}

			mCboPlantThree = this.GetSelectedValueFromCbo(((FrmExtendedSearch) mView).CboPlantThree).Trim();
			if ( mCboPlantThree.Length < 1 ) 
			{
				noSearchCriteria ++;
			}

			// Set flag false if no plants, medicals & respmask search criteria
			if ( noSearchCriteria == 12) 
			{
				mSearchCriteriaPlantMaskMed = false;
			} 		
		}


		#endregion // End of PlantMaskMed

		#region CoWorkersOverview

		
		/// <summary>
		/// Checks that valid dates have been entered in textbox searchfields 
		/// (in some cases, e.g. Btn Search via shortcut, Leave event does not fire.)
		/// Call super method.
		/// </summary>
		internal void CheckSearchCriteria() 
		{
            mViewXtend = (FrmExtendedSearch)mView;

			CheckDateString(mViewXtend.TxtAccessAuthorizationOn.Text );
			CheckDateString(mViewXtend.TxtBreathingApparatusBriefingg262On.Text );
			CheckDateString(mViewXtend.TxtBreathingApparatusBriefingG263On.Text );
			CheckDateString(mViewXtend.TxtFireBriefingOn.Text );
			CheckDateString(mViewXtend.TxtBriefingPlantOn.Text );
			CheckDateString(mViewXtend.TxtCheckInOn.Text );
			CheckDateString(mViewXtend.TxtCheckOffOn.Text );
			CheckDateString(mViewXtend.TxtCranesOn.Text );
			CheckDateString(mViewXtend.TxtDateOfBirth.Text );
			CheckDateString(mViewXtend.TxtDeliveryDate.Text );
			CheckDateString(mViewXtend.TxtIdSmActOn.Text);
            CheckDateString(mViewXtend.TxtIdHitagOn.Text);
			CheckDateString(mViewXtend.TxtIndustrialSafetyBriefingOn.Text );
			CheckDateString(mViewXtend.TxtPalletLifterOn.Text );
			CheckDateString(mViewXtend.TxtRaisablePlatformOn.Text );
			CheckDateString(mViewXtend.TxtRespiratoryMaskOn.Text );
			CheckDateString(mViewXtend.TxtSafetyAtWorkServiceBriefingOn.Text );
			CheckDateString(mViewXtend.TxtSafetyInstructionsOn.Text );
			CheckDateString(mViewXtend.TxtSiteSecurityBriefingOn.Text );
			CheckDateString(mViewXtend.TxtValidFrom.Text );
			CheckDateString(mViewXtend.TxtValidUntil.Text );
			CheckDateString(mViewXtend.TxtVehicleLongOn.Text );
			CheckDateString(mViewXtend.TxtVehicleShortOn.Text );
		}
		
		/// <summary>
		/// "Entry point" for the model
		/// Basically a maximum of 4 queries is carried out depending which search parameters have been set (fields in GUI)
		/// Correspondingly max. 4 hashtables of coworker IDs are created.
		/// Show only coworker data for the coworker IDs that appear in all hashtables
		/// 1st step: get all search criteria and execute corresponding SQL queries
		/// 2nd step: get coworker IDs that appear in all 4 resultsets
		/// 3rd step: build SQL statement for overview, select coworkers with IDs from step 2
		/// 4th step: create DataTable at runtime, load datareader results into hashtable
		/// fill DataTable with coworker data from hashtable and bind to datgrid in GUI
		/// </summary>
		internal void GetCoWorkerSummary() 
		{	
			ArrayList queryResult;			
			int numRecs = 0;
			mTable = null;	
						
			// Read all search criteria from GUI and get all coworker IDs
			GetAllCoWorkersOverview();

			// Get DataProvider from DbAccess component
			IProvider mProvider = DBSingleton.GetInstance().DataProvider;

			// If query results found then search for coworker
			queryResult = this.getQueryResult();

			if (queryResult.Count != 0)
			{
				// Instantiate Arraylist
				arlCoWorker = new ArrayList();
		
				// Create datatable at runtime: this is bound to datagrid to allow sorting			
				DataRow row;
				mTable = new DataTable("RTTabCoWorker");

                mTable.Columns.Add(new DataColumn(CoWorkerSearch.ID_COL));
                mTable.Columns.Add(new DataColumn(CoWorkerSearch.SURNAME));
                mTable.Columns.Add(new DataColumn(CoWorkerSearch.FIRSTNAME));
                mTable.Columns.Add(new DataColumn(CoWorkerSearch.DATE_OF_BIRTH, typeof(System.DateTime)));
                mTable.Columns.Add(new DataColumn(CoWorkerSearch.VALID_UNTIL));
                mTable.Columns.Add(new DataColumn(CoWorkerSearch.EXCONTRACTOR));
                mTable.Columns.Add(new DataColumn(CoWorkerSearch.SUPERVISOR));
                mTable.Columns.Add(new DataColumn(CoWorkerSearch.SUBCONTRACTOR));
                mTable.Columns.Add(new DataColumn(CoWorkerSearch.COORDINATOR));
                mTable.Columns.Add(new DataColumn(CoWorkerSearch.STATUS));
                mTable.Columns.Add(new DataColumn(CoWorkerSearch.ZKS_RET));

				// Create the select command & fill Data Reader 
				IDbCommand selComm = mProvider.CreateCommand(COWORKER_QUERY);
				selComm.CommandText = GenerateWhereClauseCwrOverview(selComm.CommandText, queryResult);
				IDataReader mDR = mProvider.GetReader(selComm);

				// Loop thru records and create an ArrayList of CoWorker BOs
				while (mDR.Read())
				{
					mBOCoWorker = new CoWorkerSearch();
				
					mBOCoWorker.CoWorkerId = Convert.ToDecimal(mDR["CWR_ID"]);
					mBOCoWorker.Surname = mDR["CWR_SURNAME"].ToString();
					mBOCoWorker.Firstname  = mDR["CWR_FIRSTNAME"].ToString();
					mBOCoWorker.DateOfBirth = Convert.ToDateTime(mDR["CWR_DATEOFBIRTH"]).Date.ToString("dd.MM.yyyy");
					mBOCoWorker.ValidUntil = mDR["CWR_VALIDUNTIL"].ToString();
					mBOCoWorker.ExContractorName = mDR["EXTCON"].ToString();
					mBOCoWorker.Supervisor = mDR["SUPERVISOR"].ToString();
					mBOCoWorker.SupervisTel = mDR["SUPERTEL"].ToString();
					mBOCoWorker.SubContractor = mDR["SUBCON"].ToString();
					mBOCoWorker.Coordinator	= mDR["COORDINATOR"].ToString();
					mBOCoWorker.CoordTel = mDR["VWC_TEL"].ToString();
					mBOCoWorker.Status = mDR["CWR_STATUS"].ToString();
					mBOCoWorker.ZKSReturncode = mDR["CWR_RETURNCODE_ZKS"].ToString();
					mBOCoWorker.SuperNameAndTel = mBOCoWorker.Supervisor + "  (Tel. " + mBOCoWorker.SupervisTel + ")";
					mBOCoWorker.CoordNameAndTel = mBOCoWorker.Coordinator + "  (Tel. " + mBOCoWorker.CoordTel + ")";

					arlCoWorker.Add(mBOCoWorker);

					// Create new row in datatable 
					// Fill datarow with array containg BO attributes 
					row	= mTable.NewRow();
					
					row.ItemArray = new object[11] {mBOCoWorker.CoWorkerId,
													mBOCoWorker.Surname,
													mBOCoWorker.Firstname,
													mBOCoWorker.DateOfBirth,
													mBOCoWorker.ValidUntil,
													mBOCoWorker.ExContractorName,
													mBOCoWorker.SuperNameAndTel,		
													mBOCoWorker.SubContractor, 
													mBOCoWorker.CoordNameAndTel, 
													mBOCoWorker.Status,
													mBOCoWorker.ZKSReturncode
													};
					mTable.Rows.Add(row);
					numRecs ++;
				}
				mDR.Close();
			}
		}

		/// <summary>
		/// From here the 4 hashtables containing the coworker IDs are filled
		/// Step 1: empty the hashtables
		/// Step 2: get coworkers IDs which meet search criteria for each block in GUI
		/// </summary>
		private void GetAllCoWorkersOverview() 
		{
			mCoworkerDict = null;
			mReceptionauthorize = null;
			mPlantMaskMed = null;
			mCwrbriefing = null;

			GetCoWorkerIds();
			GetRecAuthorizeIds();
			GetCwrBriefingIds();
			GetPlantMaskMedIds();			
		}


		/// <summary>
		/// Build SQL WHERE clause for statement to get all coworker data for coworkers with the given IDs
		/// "WHERE CWR_IN IN (x,y,z)"
		/// (Database view: VW_FPASS_EXTERNALSEARCHCWR)
		/// 
		/// Patch 14.02.2005:
		/// if more than 1000 IDs in arrylist then SQL fails
		/// ORA-01795: Höchstzahl von Ausdrücken in einer Liste ist 1000
		/// Workaround:
		/// split list of IDs using OR clause in SQL
		/// </summary>
		/// <param name="pSelect">1st part of SQL text</param>
		/// <param name="pIdList">Arraylist containing coworker IDS to be selected</param>
		/// <returns></returns>
		private String GenerateWhereClauseCwrOverview(String pSelect, ArrayList pIdList) 
		{
			String	whereClause = "";
			String s = null;
			whereClause = " WHERE CWR_ID IN (";

			
			/// Append ids as search criteria to query string
			/// 14.02.2005:
			/// if arraylist contains more than 1000 entries 
			/// then list must be split, else SQL fails
			/// WHERE CWR_ID IN (n,n,n,n)
			/// OR CWR_ID IN (n,n,n) etc
			/// 
			
			for (int i = 0; i < pIdList.Count; i++)
			{
				/// Block of 1000 is finished, break up SQL statement
				/// index number 999
				/// index number 1998 
				/// etc
				if ( 0 != i && 0 == i % 999 )
				{
					whereClause = whereClause 
						+ pIdList[i].ToString()
						+ ")"
						+  " OR CWR_ID IN (";
				}
				else
				{
					whereClause = whereClause + pIdList[i].ToString();
					if (i == pIdList.Count - 1) 
					{
						/// Add ")" to query string for last id 
						whereClause = whereClause + ")";
					} 
					else
					{
						/// Add "," to query string for all ids except last id
						whereClause = whereClause + ",";
					}
					}
			}			
			s = pSelect + whereClause;
				

			// Add ORDER BY clause to very end
			// NLS Sort German to get A, a, ...Z
			s = s + " ORDER BY NLSSORT(EXTCON, 'NLS_SORT=GERMAN'), " 
				+ "NLSSORT(CWR_SURNAME, 'NLS_SORT=GERMAN'), " 
				+ "NLSSORT(CWR_FIRSTNAME, 'NLS_SORT=GERMAN')";

			return s;
		}


		/// <summary>
		/// At this stage a max. of 4 hashtables of coworker IDs have been filled
		/// Find the first hashtable which is not null, for each coworker ID in this first hashtable,
		/// if this ID exists in all other hashtables containing query records
		/// then add ID to new arraylist of results
		/// </summary>
		/// <returns>arraylist of coworker IDs</returns>
		private ArrayList getQueryResult() 
		{
			ArrayList result;
			Hashtable hashtableToIterate;
			hashtableToIterate = new Hashtable();
			bool		resultFoundCwr;
			bool		resultFoundRecept;
			bool		resultFoundPlantMM;
			bool		resultFoundCwrBri;

			if (this.mCoworkerDict != null) 
			{
				hashtableToIterate = this.mCoworkerDict;
			} 
			else if (this.mReceptionauthorize != null) 
			{
				hashtableToIterate = this.mReceptionauthorize;
			}
			else if (this.mPlantMaskMed != null) 
			{
				hashtableToIterate = this.mPlantMaskMed;
			}
			else if (this.mCwrbriefing != null) 
			{
				hashtableToIterate = this.mCwrbriefing;
			} 
			else
			{
				throw new UIWarningException(MessageSingleton.GetInstance().GetMessage
					(MessageSingleton.NO_SEARCH_CRITERIA));
			}

			result = new ArrayList();

			/* Iterate thru first result hashtable
			 */
			foreach(decimal cwrId in hashtableToIterate.Keys) 
			{
				// Which hashtables also contain this ID
				resultFoundCwr		= lookUpResultCwr(cwrId, this.mCoworkerDict);
				resultFoundRecept	= lookUpResultRecept(cwrId, this.mReceptionauthorize);
				resultFoundPlantMM	= lookUpResultPlantMM(cwrId, this.mPlantMaskMed);
				resultFoundCwrBri	= lookUpResultCwrBri(cwrId, this.mCwrbriefing);
				
				/* remember current id if it is found in each hashtable containing query data
				 */
				if (resultFoundCwr && resultFoundRecept&& resultFoundPlantMM && resultFoundCwrBri) 
				{
					result.Add(cwrId);
				}
			}
			return result;
		}


		/// <summary>
		/// Checks whether current hashtable (IDs as query results for coworker general attributes)
		/// contains the given coworker ID
		/// </summary>
		/// <param name="pCwrId">coworker ID to check</param>
		/// <param name="hashtableToLookup">hashtable to search</param>
		/// <returns>true if coworker ID found, false if not</returns>
		private bool lookUpResultCwr(decimal pCwrId, Hashtable hashtableToLookup) 
		{
			bool resultFoundCwr;
			
			if (hashtableToLookup != null) 
			{
				if (hashtableToLookup[pCwrId] == null)
				{
					/* CwrID is not found in hashtable
					 */
					resultFoundCwr = false;
				} 
				else
				{
					/* CwrID found in hashtable
					 */
					resultFoundCwr = true;
				}
			} 
			else
			{
				/* if hashtable is null result can be set to true as 
				 * the contents of current hashtable can be ignored: 
				 * no search criteria were set before.
				 */
				resultFoundCwr = true;
			}
			return resultFoundCwr;
		}


		/// <summary>
		/// Checks whether current hashtable (IDs as query results for coworker reception authorized details)
		/// contains the given coworker ID
		/// </summary>
		/// <param name="pCwrId">coworker ID to check</param>
		/// <param name="hashtableToLookup">hashtable to search</param>
		/// <returns>true if coworker ID found, false if not</returns>
		private bool lookUpResultRecept(decimal pCwrId, Hashtable hashtableToLookup) 
		{
			bool resultFoundRecept;
			
			if (hashtableToLookup != null) 
			{
				if (hashtableToLookup[pCwrId] == null)
				{
					/* CwrID is not found in hashtable
					 */
					resultFoundRecept = false;
				} 
				else
				{
					/* CwrID found in hashtable
					 */
					resultFoundRecept = true;
				}
			} 
			else
			{
				/* if hashtable is null result can be set to true as 
				 * the contents of current hashtable can be ignored:
				 * no search criteria were set before.
				 */
				resultFoundRecept = true;
			}

			return resultFoundRecept;
		}


		/// <summary>
		/// Checks whether current hashtable (IDs as query results for coworker plants, medicals & respmask)
		/// contains the given coworker ID
		/// </summary>
		/// <param name="pCwrId">coworker ID to check</param>
		/// <param name="hashtableToLookup">hashtable to search</param>
		/// <returns>true if coworker ID found, false if not</returns>
		private bool lookUpResultPlantMM(decimal pCwrId, Hashtable hashtableToLookup) 
		{
			bool resultFoundPlantMM;
			
			if (hashtableToLookup != null) 
			{
				if (hashtableToLookup[pCwrId] == null)
				{
					/* CwrID is not found in hashtable
					 */
					resultFoundPlantMM = false;
				} 
				else
				{
					/* CwrID found in hashtable
					 */
					resultFoundPlantMM = true;
				}
			} 
			else
			{
				/* if hashtable is null result can be set to true as 
				 * the contents of current hashtable can be ignored:
				 * no search criteria were set before.
				 */
				resultFoundPlantMM = true;
			}

			return resultFoundPlantMM;
		}


		/// <summary>
		/// Checks whether current hashtable (IDs as query results for coworker briefings)
		/// contains the given coworker ID
		/// </summary>
		/// <param name="pCwrId">coworker ID to check</param>
		/// <param name="hashtableToLookup">hashtable to search</param>
		/// <returns>true if coworker ID found, false if not</returns>
		private bool lookUpResultCwrBri(decimal pCwrId, Hashtable hashtableToLookup) 
		{
			bool resultFoundCwrBri;
			
			if (hashtableToLookup != null) 
			{
				if (hashtableToLookup[pCwrId] == null)
				{
					/* CwrID is not found in hashtable
					 */
					resultFoundCwrBri = false;
				} 
				else
				{
					/* CwrID found in hashtable
					 */
					resultFoundCwrBri = true;
				}
			} 
			else
			{
				/* if hashtable is null result can be set to true as 
				 * the contents of current hashtable can be ignored:
				 * no search criteria were set before.
				 */
				resultFoundCwrBri = true;
			}

			return resultFoundCwrBri;
		}
		#endregion // End of CoWorkersOverview

		
		#endregion // End of Methods

	}
}

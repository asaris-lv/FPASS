using System;
using System.Collections;
using System.Data;

using de.pta.Component.Errorhandling;
using de.pta.Component.N_UserManagement;
using de.pta.Component.N_UserManagement.DataAccess;
using de.pta.Component.N_UserManagement.Internal;
using de.pta.Component.N_UserManagement.Vo;
using de.pta.Component.N_UserManagement.Authorization;
using de.pta.Component.DbAccess;

using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.Util.Exceptions;
using Degussa.FPASS.Bo.Pass;
using Degussa.FPASS.Db;
using Degussa.FPASS.Bo.Mandator;
using de.pta.Component.Logging.Log4NetWrapper;
using Degussa.FPASS.Util.ErrorHandling;


namespace Degussa.FPASS.Util.UserManagement
{
	/// <summary>
	/// Provides methods to verify user login, user rights and access to the pta 
	/// component usermanagement.
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
	///			<td width="20%">Author</td>
	///			<td width="20%">month/day/year</td>
	///			<td width="60%">Comment</td>
	///		</tr>
	/// </table>
	/// </div>
	/// </remarks>
	public class UserManagementControl
	{

		#region Members

        // Role codes
		public const String ROLE_KOORDINATOR = "Koordinator";
		public const String ROLE_EDVADMIN = "ADMIN";
		public const String ROLE_EMPFANG = "Empfang";
		public const String ROLE_WERKSARZT = "WD";
		public const String ROLE_BETRIEBSMEISTER = "Betriebsmeister";
		public const String ROLE_ARBEITSSICHERHEIT = "AS";
		public const String ROLE_WERKFEUERWEHR = "WF";
		public const String ROLE_WERKSCHUTZ = "WS";
		public const String ROLE_WERKSCHUTZ_LEITER = "WSLeitung";
		public const String ROLE_VERWALTUNG = "Verwalter";
		public const String ROLE_TECHNISCHE_ABTEILUNG = "TA-S";
        public const String ROLE_AUSWEIS = "AUSWEIS";

        // Dialog Ids as declared in Configuration.xml
		public const int MANDANT_DIALOG = 100;
		public const int REPORTS_DIALOG = 101;
		public const int HISTORY_DIALOG = 102;
		public const int SEARCH_EXCONTRACTOR_DIALOG = 103;
		public const int REPORTS_PASS_ONE = 104;
		public const int REPORTS_PASS_TWO = 105;
		public const int REPORTS_CWR_BOOKINGS = 106;
		public const int REPORTS_CWR_ALL_DATA = 107;
		public const int REPORTS_EXCONTRACTOR_WITH_COORD = 108;
		public const int REPORTS_CHECKLIST = 109;
		public const int REPORTS_EXPIRYDATE = 110;
		public const int REPORTS_DELETELIST = 111;
		public const int REPORTS_PLANTS = 112;
		public const int REPORTS_EXCO_BOOKINGS_SUM = 113;
		public const int REPORTS_RESPMASKS = 114;
		public const int REPORTS_RESPMASK_PER_MAINTENANCE = 115;
		public const int REPORTS_CWR_BOOKINGS_EXCO = 116;
		public const int REPORTS_CWR_CHANGEHIST = 117;
		public const int REPORTS_FFMABOOKINGSINCE = 118;
		public const int REPORTS_CWRATTENDANCEDETAIL = 119;
		public const int REPORTS_CWRATTENDANCE = 120;
		public const int REPORTS_EXCO_ATTENDANCE = 121;

		public const int ADMINISTRATION_DIALOG = 201;
		public const int ADMIN_ASSIGNMENT_COORD_EXCO_DIALOG = 202;
		public const int ADMIN_PLANT_DIALOG = 203;
		public const int ADMIN_CRAFT_DIALOG = 204;
		public const int ADMIN_EXCONTRACTOR_DIALOG = 205;
		public const int ADMIN_DEPARTMENT_DIALOG = 206;
		public const int ADMIN_MEDICAL_PREC_DIALOG = 207;

		public const int COWORKER_SUMMARY_DIALOG = 301;
		public const int COWORKER_REGISTER_DIALOG = 302;
		public const int COWORKER_RECEPTION_DIALOG = 303;
		public const int COWORKER_COORDINATOR_DIALOG = 304;
		public const int COWORKER_SITE_SECURITY_DIALOG = 305;
		public const int COWORKER_MEDICAL_SERVICE_DIALOG = 306;
		public const int COWORKER_SITE_FIRE_SERVICE_DIALOG = 307;
		public const int COWORKER_PLANT_DIALOG = 308;
		public const int COWORKER_SAFETY_AT_WORK_DIALOG = 309;
		public const int COWORKER_TEC_DEPARTMENT_DIALOG = 310;
		public const int COWORKER_SITE_SECURITY_LEADER_DIALOG = 311;
		public const int COWORKER_VEHICLE_NO_SITE_SECURITY = 312;
		
		public const int COWORKER_ARCHIVE_DIALOG = 401;
		public const int COWORKER_DELETELIST_DIALOG = 402;
		public const int COWORKER_EXTENDED_SEARCH_DIALOG = 403;
		public const int COWORKER_DYNAMIC_DATA_DIALOG = 404;

		public const int USER_REGISTER_DIALOG = 501;
		public const int USER_ROLE_DIALOG = 502;
		public const int USER_USER_TO_ROLE_DIALOG = 503;

		public const int FPASS_TO_ZKS = 601;

        public const int COWORKER_SUMMARY_BTN_REGISTER_DETAILS = 313;
        public const int COWORKER_FIELD_IDCARDNO = 314;
        public const int COWORKER_FIELD_APPRENTICE = 315;

		private String USER_PASSWORD = "FPASS";
		private String MANDATOR_QUERY = "Mandator";
		private String FPASSUSERID_QUERY = "FPASSUserID";
		private String FPASSUSERPARAMETERS = "FPASSUserParameter";
		
		/// <summary>  
		/// Used to hold the unique instance of UserManagementControl
		///</summary>
		private static UserManagementControl mInstance = null;
		/// <summary>
		/// Holds the number of mandator the current windows user is assigned to
		/// </summary>
		private int mNumberOfMandators;
		/// <summary>
		/// Holds the current fpass user id 
		/// </summary>
		private int mCurrentUserID;
		/// <summary>
		/// Windows username of the current user
		/// </summary>
		private String mCurrentOSUserName;
		/// <summary>
		/// Proper name of current user 
		/// </summary>
		private String mCurrentUserNiceName;

		/// <summary>
		/// Windows username of the current user's domain
		/// </summary>
		private String mCurrentOSUserDom;

        /// <summary>
        /// Current user's ID card reader numbers: Hitag and Mifare
        /// </summary>
        private int mCurrentIDCardReaderHitag;

        private int mCurrentIDCardReaderMifare;  

		/// <summary>
		/// Holds all <see cref="Degussa.FPASS.Bo.Mandator"><code>
		/// BOUserMndt</code></see> of the current User
		/// </summary>
		private ArrayList mUsersWithMandatorInfo;
		/// <summary>
		/// Holds all <see cref="Degussa.FPASS.Bo.Mandator"><code>
		/// rolenames</code></see> of the current User
		/// </summary>
		private ArrayList mRoles;
		/// <summary>
		/// The current <see cref="Degussa.FPASS.Bo.Mandator"><code>
		/// BOUserMndt</code></see> the user chose for this session of fpass
		/// </summary>
		private BOUserMndt mCurrentBOUserMndt;
		/// <summary>
		/// Used to hold the CoordinatorId of the current user, if the current user is in this role
		/// </summary>
		private decimal mCurrentCoordinatorID;

		#endregion

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		private UserManagementControl()
		{
			initialize();
		}

		#endregion

		#region Initialization

		/// <summary>
		/// Initializes the members.
		/// Gets current domain from environment: 
		/// Combination domain, user and mandator is unique
		/// </summary>
		private void initialize()
		{
			mCurrentBOUserMndt = new BOUserMndt();
			mCurrentOSUserName = Environment.UserName;
			mCurrentOSUserDom  = Environment.UserDomainName;
		}	

		#endregion Initialization

		#region Accessors 

		/// <summary>
		/// gets the unique mandator id of the current user. 
		/// Returns -1  if the current user has not selected a 
		/// unique mandator for this session of FPASS
		/// </summary>
		public int CurrentMandatorID 
		{
			get 
			{
				return mCurrentBOUserMndt.MandatorID;
			}
			set 
			{
				mCurrentBOUserMndt.MandatorID = value;
			}
		}

		/// <summary>
		/// Gets or sets number of mandants current user is assigned to
		/// </summary>
		public int NumberOfMandators 
		{
			get 
			{
				return mNumberOfMandators;
			}
			set 
			{
				mNumberOfMandators = value;
			}
		}


		/// <summary>
		/// Returns PK ID of current coordinator
		/// </summary>
		public decimal CurrentCoordinatorID 
		{
			get 
			{
				return mCurrentCoordinatorID;
			}
			set 
			{
				mCurrentCoordinatorID = value;
			}
		}

		/// <summary>
		/// Gets the ArrayList if there is more than one Mandator
		/// </summary>
		public ArrayList UsersWithMandatorInfo 
		{
			get 
			{
				return mUsersWithMandatorInfo;
			}
		}
		
		/// <summary>
		/// Gets the ArrayList if there is more than one Role
		/// </summary>
		public ArrayList UsersRoles 
		{
			get 
			{
				return mRoles;
			}
		}


		/// <summary>
		/// Gets or sets business object corresponding to current mandant
		/// </summary>
		public BOUserMndt CurrentBOUserMndt 
		{
			get 
			{
				return mCurrentBOUserMndt;
			}
			set 
			{
				mCurrentBOUserMndt = value;
			}
		}

		/// <summary>
		/// Gets or sets name of current mandant
		/// </summary>
		public String CurrentMandatorName 
		{
			get 
			{
				return mCurrentBOUserMndt.MandatorName;
			}
			set 
			{
				mCurrentBOUserMndt.MandatorName = value;
			}
		}
	

		/// <summary>
		/// Gets or sets unique fpass user id of the current user. 
		/// </summary>
		public int CurrentUserID 
		{
			get 
			{
				return mCurrentUserID;
			}
			set 
			{
				mCurrentUserID = value;
			}
		}

		/// <summary>
		/// Gets or sets nice name of current user
		/// in the form 'Surname, First name (tel number)'
		/// </summary>
		public string CurrentUserNiceName
		{
			get 
			{
				return mCurrentUserNiceName;
			}
			set 
			{
				mCurrentUserNiceName = value;
			}
		}

	
		/// <summary>
		/// Gets name of currently logged on user
		/// </summary>
		public String CurrentOSUserName 
		{
			get 
			{
				return mCurrentOSUserName;
			}
		}

		/// <summary>
		/// Gets domain of currently logged on user
		/// </summary>
		public String CurrentOSUserDom 
		{
			get 
			{
				return mCurrentOSUserDom;
			}
		}

        /// <summary>
        /// Holds user's id card reader no Hitag
        /// </summary>
        public int IDCardReaderHitag
        {
            get { return mCurrentIDCardReaderHitag; }
            set { mCurrentIDCardReaderHitag = value; }
        }
        
        /// <summary>
        /// Holds user's id card reader no Mifare
        /// </summary>
        public int IDCardReaderMifare
        {
            get { return mCurrentIDCardReaderMifare; }
            set { mCurrentIDCardReaderMifare = value; }
        }


        /// <summary>
        /// Should FPASS show popup with CWR with id cards from SmartAct?
        /// </summary>
        public bool ShowIdCardsSmartAct
        {
            get { return false; }
            set {  
            }
        }
               


		public bool	PreviewPass
		{ 
			get 
			{
				return CurrentBOUserMndt.PreviewPass;
			}
			set 
			{
				CurrentBOUserMndt.PreviewPass = value;
			}
		}


		public int NumberOfPrintedPass
		{ 
			get 
			{
				return CurrentBOUserMndt.NumberOfPrintedPass;
			}
		}

		public int EntranceFlowTime
		{ 
			get 
			{
				return CurrentBOUserMndt.EntranceFlowTime;
			}
		}
		

		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// Returns the one and only instance of this class.
		/// </summary>
		/// <returns>instance of UserManagementControl</returns>
		public static UserManagementControl getInstance() 
		{
			if ( null == mInstance ) 
			{
				mInstance = new UserManagementControl();
			} 
			return mInstance;
		}

	

		/// <summary>
		/// Verifies if user is auhtorized to access the requested
		/// resource.
		/// </summary>
		/// <exception cref="de.pta.Component.Errorhandling.UIFatalException"> is
		/// thrown if an error occured</exception>
		/// <param name="pID"></param>
		/// <returns>true if user is auhtorized to access the requested
		/// resource, false otherwise</returns>
		public bool GetAuthorization(int pRessourceID) 
		{	
			try 
			{
				return AuthorizationManager.getInstance().isUserAuthorized(pRessourceID);
			} 
			catch ( Exception e ) 
			{
				throw new UIFatalException("UM-Fehler beim Prüfen der Berechtigungen", e);
			}
		}

		/// <summary>
		/// Verifies current user and initielizes all user dependet data.
		/// </summary>
		/// <exception cref="de.pta.Component.Errorhandling.UIFatalException"> is
		/// thrown user is not authorized to use fpass.</exception>
		public void VerifyLoginUser()  
		{
			if ( ! LoginManager.getInstance().verifyLogin( 
				this.CurrentBOUserMndt.UserID.ToString(), USER_PASSWORD) )
			{
				throw new UIFatalException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.INVALID_USER));
			}
			ReadFPASSUserID();
			ReadFPASSUserParameters();
			ReadCoordinatorID();
		}

		/// <summary>
		/// Initializes pta component usermanagement with the id of the current mandator
		/// </summary>
		internal void InitializeMandatorDependentUsermanagement() 
		{
			LoginManager.getInstance().ReadMandatorDependentRoleList(CurrentMandatorID);
		}

		/// <summary>
		/// Verifies if the current user has in given role
		/// </summary>
		/// <param name="pRoleName">rolename to verify</param>
		/// <returns>true if user is in given role, false otherwise</returns>
		/// <exception cref="de.pta.Component.Errorhandling.UIFatalException"> is
		/// thrown if an unhandled error occured.</exception>
		internal bool CurrentUserIsInRole(String pRoleName) 
		{
			try 
			{
				if ( null == mRoles ) 
				{
					ReadRoles();	
				}
				if ( mRoles.Contains(pRoleName.ToUpper().Trim() ) ) 
				{
					return true;
				} 
				else 
				{
					return false;
				}
			}  
			catch ( Exception e ) 
			{
				throw new UIFatalException("FPASS-Fehler beim Prüfen der Rollenzugehörigkeit", e);
			}
		}

		/// <summary>
		/// Reads the roles of the current user
		/// </summary>
		private void ReadRoles() 
		{
			mRoles = new ArrayList();
			
			foreach ( RoleVO roleVO in AdministrationManager.getInstance().getUserRoles(
				UserManager.getInstance().CurrentUser) ) 
			{				
				mRoles.Add(roleVO.Name.ToString().Trim().ToUpper());
			}	
		}


		/// <summary>
		/// Reads the mandators for the current user
		/// 31.03.04: changed SQL query, read combination of domain + "\" + user
		/// </summary>
		/// <exception cref="de.pta.Component.Errorhandling.UIFatalException"> is
		/// thrown if current user has no mandator assigned</exception>
		internal void ReadMandator() 
		{
           
			// set to ensure that if no rec is found
			int recs = 0;
			BOUserMndt  boUserMndt;
			
			mUsersWithMandatorInfo = new ArrayList();

			// Get DataProvider from DbAccess component
			IProvider mProvider = DBSingleton.GetInstance().DataProvider;

			// Create the select command & fill Data Reader 
			IDbCommand selComm = mProvider.CreateCommand(MANDATOR_QUERY);


			// mProvider.SetParameter(selComm, ":USER_NAME", mCurrentOSUserName.ToUpper());
			// Read "<domain>\<user>"
            var currUser = mCurrentOSUserDom.ToUpper()
									+ "\\"
									+ mCurrentOSUserName.ToUpper();

			mProvider.SetParameter(selComm, ":USERWITHDOMAIN", currUser);

			// Open data reader to get Mandator data
			IDataReader mDR = mProvider.GetReader(selComm);

       
			// Loop thru records and create an ArrayList of Mandator BOs
			while (mDR.Read())
			{
				recs++;
				boUserMndt = new BOUserMndt();
				
				boUserMndt.UserID = Convert.ToDecimal( mDR[0] );
				boUserMndt.MandatorID = mDR.GetInt32(1);
				boUserMndt.MandatorName = mDR[2].ToString();
				if ( mDR[3].ToString().Equals("Y" ) ) 
				{		
					boUserMndt.HasZKS = true;
				} 
				else 
				{
					boUserMndt.HasZKS = false;
				}
				mUsersWithMandatorInfo.Add(boUserMndt);
			}
			// Close reader
			mDR.Close();

			if ( recs == 0 ) 
			{
				throw new UIFatalException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.INVALID_USER));
			}
			if ( recs == 1 ) 
			{
				mCurrentBOUserMndt = ((BOUserMndt) mUsersWithMandatorInfo[0]);
			}

			mNumberOfMandators = recs;
		}

		/// <summary>
		/// Reads fpass user id for the current user 
		/// FPASS V3: 24.02.2005: get User Nice Name (Surname, First name, tel) from database
		/// FPASS V5: added fields for ID card readers 
		/// </summary>
		/// /// <exception cref="de.pta.Component.Errorhandling.UIFatalException"> is
		/// thrown if current user has no right to use fpass</exception>
		private void ReadFPASSUserID() 
		{
			// Get DataProvider from DbAccess component
			IProvider mProvider = DBSingleton.GetInstance().DataProvider;

			// Create the select command & fill Data Reader 
			IDbCommand selComm = mProvider.CreateCommand(FPASSUSERID_QUERY);

			mProvider.SetParameter(selComm, ":USER_US_ID", CurrentBOUserMndt.UserID);

			// Open data reader to get User data
			IDataReader mDR = mProvider.GetReader(selComm);

			// Loop thru records and create an ArrayList of User BOs
			int recs = 0;
			while (mDR.Read())
			{
				recs++;
				mCurrentUserID = mDR.GetInt32(0);
				mCurrentUserNiceName = mDR.GetString(1);

                // Id card reader numbers are not always set 
                if (mDR[2].Equals(DBNull.Value))
                    mCurrentIDCardReaderHitag = 0;
                else  mCurrentIDCardReaderHitag = mDR.GetInt32(2);
                        
                if (mDR[3].Equals(DBNull.Value))
                    mCurrentIDCardReaderMifare = 0;
                else  mCurrentIDCardReaderMifare = mDR.GetInt32(3);
            }

			// Close reader
			mDR.Close();

			if ( recs != 1 ) 
			{	
				throw new UIFatalException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.INVALID_USER));
			}
		}

		/// <summary>
		/// Reads fpass user id for the current user 
		/// </summary>
		/// /// <exception cref="de.pta.Component.Errorhandling.UIFatalException"> is
		/// thrown if current user has no right to use fpass</exception>
		private void ReadFPASSUserParameters() 
		{
			String boolPreview = "Y";
			// Get DataProvider from DbAccess component
			IProvider mProvider = DBSingleton.GetInstance().DataProvider;

			// Create the select command & fill Data Reader 
			IDbCommand selComm = mProvider.CreateCommand(FPASSUSERPARAMETERS);

			mProvider.SetParameter(selComm, ":UPAR_USER_ID", mCurrentUserID);

			// Open data reader to get User data
			IDataReader mDR = mProvider.GetReader(selComm);


			while (mDR.Read())
			{
				if ( ! mDR["UPAR_NAUMBEROFPASS"].Equals(DBNull.Value ) ) 
				{
					mCurrentBOUserMndt.NumberOfPrintedPass = 
						Convert.ToInt32( mDR["UPAR_NAUMBEROFPASS"] );
				}
				boolPreview = mDR["UPAR_PRINTPREVIEW_YN"].ToString();
				
				if ( boolPreview.Equals("N") )
				{
					mCurrentBOUserMndt.PreviewPass = false;
				}
			}
			// Close reader
			mDR.Close();
		}

		/// <summary>
		/// Reads coordinatorid user id for the current user ( if user has role coordinator )
		/// </summary>
		private void ReadCoordinatorID() 
		{
			// Get DataProvider from DbAccess component
			IProvider mProvider = DBSingleton.GetInstance().DataProvider;

			// Create the select command & fill Data Reader 
			IDbCommand selComm = mProvider.CreateCommand("COORDINATORID_QUERY");

			mProvider.SetParameter(selComm, ":ECOD_USER_ID", mCurrentUserID);

			// Open data reader to get User data
			IDataReader mDR = mProvider.GetReader(selComm);

			mCurrentCoordinatorID = -1;
			// Loop thru records
			while (mDR.Read())
			{
				mCurrentCoordinatorID = mDR.GetInt32(0);
			}
			// Close reader
			mDR.Close();
		}

		#endregion // End of Methods
	}
}

using System;
using System.Collections;
using System.Data;
using System.Data.OracleClient;
using System.Reflection;
using System.Windows.Forms;

using de.pta.Component.DbAccess;
using de.pta.Component.DbAccess.Exceptions;
using de.pta.Component.Errorhandling;
using de.pta.Component.Logging.Log4NetWrapper;

using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.Util.ErrorHandling;
using Degussa.FPASS.Util.Exceptions;
using Degussa.FPASS.Util.UserManagement;
using Degussa.FPASS.Db;

using Degussa.FPASS.Gui.Dialog.Pass;
using FPASS.Degussa.FPASS.Util.Exceptions;

namespace Degussa.FPASS.Util
{
	/// <summary>
	/// Holds Global variables and calculation parameters. 
    /// Some of these are constants, others are read from database
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
	///			<td width="60%">Creation</td>
	///		</tr>
	/// </table>
	/// </div>
	/// </remarks>
	public class Globals
	{
		#region Members
		
        // myself
		private	static Globals	mInstance;
	
        // Current version of FPASS, as given in AssemblyInfo and database table fpass_parameterfpass
        private string mFPASSApplicationVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        private	string mFPASSDatabaseVersion    = "0";
        
        // Name of database currently connected to
        private string mDatabaseName = "";

        // Name of database user
        private string mDatabaseUser = "";

		// FPASS Parameters for calculation
		// these are read from database table fpass_parameterfpass
        private bool mVersionCheckActive = true;
		private double mParAccessShort = 0;
		private double mParAccessMiddle = 0;
		private double mParAccessLong = 0;
		private double mParCoolPeriodBrief = 0;
		private double mParCoolPeriodOrder = 0;
		private double mParValidAfterChkOff	= 0;
		private double mParTimeOnEntry = 0;
		private double mParVehEntryShort = 0;
        private decimal mParAccessIndicator = 0;
		
        // TK root for coworkers (usually 97)
        private decimal mTKNumberRoot = 0;

		// Path for temporary Reports files
		private string mReportsBasePath;

		// Name and Path for PDF document "Sicherheitsunterweisung"
		private string mDocSafetyPathAndName;

		// Briefings. These keys match FPASS_BRIEFING
        // TODO: why are these defined as variables? Could just as easily be constants
		private decimal mBriefSafetyAtWorkID		= 22;
		private decimal mBriefSiteSecurityID		= 24; 			
		private decimal mBriefBreathingAppG26_2_ID	= 47;
		private decimal mBriefBreathingAppG26_3_ID	= 41;
		private decimal mBriefPalletLifterID		= 42;
		private decimal mBriefRaisablePlatformID	= 43;			
		private decimal mBriefCranesID				= 44;
		private decimal mBriefRespiratoryMaskID		= 46;
		private decimal mIdCardPhotoHitagID		    = 50;
		private decimal mBriefFireman				= 53;
        private decimal mBriefApprentice            = 54;
        private decimal mIdCardPhotoSmActID         = 55;

		// IDs of (vehicle entry)  (also fpass_briefing)
		private decimal mBriefVehicleEntranceShortID = 48;
		private decimal mBriefVehicleEntranceLongID  = 49;

		// IDs of authorizations (fpass_recauthorizetype)
		private decimal mAuthoIndSafetySiteID     = 1;
		private decimal mAccessAuthID			  = 2;
		private decimal mAccessAuthSiteSecurityID = 5;
		private decimal mSafetyAuthID			  = 21;
		private decimal mSignatureAuthID		  = 4;
		private decimal mParkingAuthID			  = 6;

        private decimal mPlantManagerRoleID = 0;

        /// <summary>
        /// Returns PK id of Respiratory mask received (table RespmaskType)
        /// </summary>
        public const int RespMaskIdLentFlo = 1;
        public const int RespMaskIdLentTec = 11;  
        /// <summary>
        /// Returns PK id of Respiratory mask returned
        /// </summary>
        public const int RespMaskIdReturnFlo = 2;
        public const int RespMaskIdReturnTec = 12;

        // Parameter to switch on/off interface logging (1 or 0).
        private bool mLogActive;
        // Parameter to define how long the logfiles are saved.
        private decimal mLogCollectDays;
        // target directory of the interface from FPASS to Smartact
        private string mTargetDirectoryFpassToSmartAct; //PARF_TARGETDIR_FPASS_SMARTACT
        // target file of the interface from FPASS to Smartact
        private string mTargetFileFpassToSmartAct;
        // log directory of the interface from FPASS to Smartact
        private string mLogDirectoryFpassToSmartAct; //PARF_LOGDIR_FPASS_SMARTACT
        // log file of the interface from FPASS to Smartact
        private string mLogFileFpassToSmartAct; //PARF_LOGFILE_FPASS_SMARTACT
        // prefix for interface file (update, new)
        private string mPersnoPrefix; //PARF_PERSNO_PREFIX
        // prefix for interface file (update, new)
        private string mPersnoPrefixDel; //PARF_PERSNO_PREFIX_DEL
        // Length of time in years until access for apprentices expires
        private decimal mApprenticeExpiry;
        // Interval for showing automatic popup for Ids from SmartACT
        private decimal mSmartActIdCardInterval;
        // Maximimum number of hours that CWR are allowed to be on-site (attendance)
        private decimal mAttendHoursLimit;

        /// <summary>
        /// Is logic for using Hitag2 and Mifare chips in the id cards active or not
        /// </summary>
        private bool mHitagActive;
        private bool mMifareActive;                
        /// <summary>
        /// Are Hitag2 and Mifare id cards required fields or not (values 0 or 1)
        /// </summary>
        private bool mHitagRequiredField;
        private bool mMifareRequiredField;

        /// <summary>
        /// When parameter is true then export to SmartAct is only available
        /// when the coordinator has granted the industrial safety briefing.
        /// "Sicherheitsunterweisung erfolgt am" on Koordinator page
        /// </summary>
        private bool mIndSafety4ExpSmartAct;

        /// <summary>
        /// Resp masks: are the interfaces to TecBos and Florix for lent and return active? (values 0 or 1)
        /// </summary>
        private bool mTecBosLentActive;
        private bool mTecBosReturnActive;
        private bool mFlorixLentActive;
        private bool mFlorixReturnActive;
        private string mTecBosMaskFree;
        private int mRespMaskReserveTime;

        private int mMaxPhotoBufferSize;

        /// <summary>
        /// Max no. of precautionary medicals that can be assigned (they are transferred to ZKS)
        /// </summary>
        private int mPrecMedicalMaxAssign;


        // Database access
        private IProvider mProvider;
		private IDbCommand mCommSel;
		private IDataReader mDR;

        // Constants
		private const string FPASS_PARA_QUERY = "GlobalsFPASSPara";
		private const string FPASS_PARA_MND_PARA = ":PARF_MND_ID";    
		private const string FPASS_ROLEPLA_QUERY = "GlobalsRoleByMandator";
		private const string FPASS_ROLEPLA_MND_PARA	= ":MND_ID";
		private const string FPASS_ROLEPLA_NAME_PARA = ":UM_ROLE_NAME";
        
        // Reports
        private const string REPORTS_DATA_DIR = "\\data\\";
        private const string REPORTS_TEMPLA_DIR = "\\template\\";
        // references the entry path of Acrobat Reader in the registry ("FoxitReader.Document\\shell\\open\\command" works as well)
        private const string REPORTS_REGISTRY = "AcroExch.Document\\shell\\open\\command";
        
        public const string ACESSSHORTTEXT = "short";
        public const string ACESSSMIDDLETEXT = "middle";
        public const string ACESSLONGTEXT = "long";
        public const string STATUS_VALID = "GÜLTIG";
        public const string STATUS_INVALID = "UNGÜLTIG";
        public const string STATUS_OLD = "ALTDATEN";
        public const string PLACEHOLDER_EMPTY = "<leer>";
        public const string PLACEHOLDER_ARCHIVE = "<Archiv>";
        public const string PLACEHOLDER_NONFPASS = "<xxx>";
        public const string REPORTS_MYEXCO_FILE = "\\myFF.txt";

        /// <summary>
        /// Constants for values "Y" and "N" in DB
        /// </summary>
        public const string DB_YES = "Y";
        public const string DB_NO = "N";
        /// <summary>
        /// Versions to show in dialoge (Ja/Nein)
        /// </summary>
        public const string DB_YES_SHOW = "J";
        public const string DB_NO_SHOW = "N";

        /// <summary>
        /// Constants for Plants import from ZKS (Betriebe)
        /// </summary>
        public const string PLANT_SOURCE_FPASS = "FPASS";
        public const string PLANT_SOURCE_ZKS = "ZKS";


        /// Default vals for User parameters
        /// <summary>
        /// Coworkers are shown in deletelist mEntranceFlowTime days before validUNTIL expires 
        /// </summary>
        private	int	mParaEntranceFlowTime = 10;
		/// <summary>
		/// determines how many pass(es) are printed automatically id current user wants to print a pass
		/// </summary>
		private	int mParaNumberOfPrintedPass = 1;
		/// <summary>
		/// flag indicating if user is can see generated pass in acrobat before printing
		/// default is Yes
		/// </summary>
		private	string mParaPrintPreview = "Y";

		// Logger instance
		private	Logger mLog;

		#endregion Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		private Globals()
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
			mLog = LoggingSingleton .GetInstance().Log;
		}	

		#endregion //End of Initialization

		#region Accessors 

		/// <summary>
		/// Returns current version of FPASS Client application
		/// </summary>
		public string FPASSApplicationVersion
		{
			get {return mFPASSApplicationVersion;}			
		}

		/// <summary>
		/// Returns current version of FPASS in Database
		/// </summary>
		public string FPASSDatabaseVersion
		{
			get { return mFPASSDatabaseVersion; }
		}

        /// <summary>
        /// Returns name of database currently connected to
        /// </summary>
        public string FPASSDatabaseName
        {
            get { return mDatabaseName; }
        }

        /// <summary>
        /// Returns name of current database user
        /// </summary>
        public string FPASSDatabaseUser
        {
            get { return mDatabaseUser; }
        }


        /// <summary>
        /// Returns value of access indicator: displays access status of coworker
        /// </summary>
        public decimal AccessIndicator
        {
            get { return mParAccessIndicator; }
        }

		/// <summary>
		/// Returns Logger instance
		/// </summary>
        public Logger Log 
		{
			get 
			{
				return mLog;
			}
		}

        /// <summary>
        /// Returns value of parameter VersionCheckActive
        /// </summary>
        public bool VersionCheckActive
        {
            get { return mVersionCheckActive; }
        }

        /// <summary>
        /// Returns value of parameter Access Short
        /// </summary>
        public double AccessShort 
		{
			get 
			{
				return mParAccessShort -1;
			}
		}

        /// <summary>
        /// Returns value of parameter
        /// </summary>
		public double AccessMiddle 
		{
			get 
			{
				return mParAccessMiddle -1;
			}
		}

        /// <summary>
        /// Returns value of parameter
        /// </summary>
		public double AccessLong 
		{
			get 
			{
				return mParAccessLong -1;
			}
		}

		public double CoolPeriodBrief
		{
			get 
			{
				return mParCoolPeriodBrief;
			}
		}


        /// <summary>
        /// Returns the maximum allowed byte size of coworker photos 
        /// </summary>
        public int MaxPhotoBufferSize
        {
            get { return mMaxPhotoBufferSize; }
        }


        /// <summary>
        /// Max no. of precautionary medicals that can be assigned (they are transferred to ZKS)
        /// </summary>
        public int PrecMedicalMaxAssign
        {
            get { return mPrecMedicalMaxAssign; }
        }
 

        public double VehEntryShort
		{
			get 
			{
				return mParVehEntryShort -1;
			}
		}

        /// <summary>
        /// Returns technical key for external coworkers (usually 97, defined in ZKS)
        /// </summary>
        public decimal TKNumberRoot
		{
			get 
			{
				return mTKNumberRoot;
			}
		}

        /// <summary>
        /// Returns PK id of this briefing
        /// </summary>
		public decimal BriefSafetyAtWorkID
		{
			get  
			{
				return mBriefSafetyAtWorkID;
			}
		}

        /// <summary>
        /// Returns PK id of this briefing
        /// </summary>
		public decimal BriefSiteSecurityID
		{
			get  
			{
				return mBriefSiteSecurityID;
			}
		}

        /// <summary>
        /// Returns PK id of this briefing
        /// </summary>
		public decimal BriefBreathingAppG26_2_ID
		{
			get  
			{
				return mBriefBreathingAppG26_2_ID;
			}
		}

        /// <summary>
        /// Returns PK id of this briefeing
        /// </summary>
		public decimal BriefBreathingAppG26_3_ID
		{
			get  
			{
				return mBriefBreathingAppG26_3_ID;
			}
		}

        /// <summary>
        /// Returns PK id of this briefeing
        /// </summary>
		public decimal BriefPalletLifterID
		{
			get  
			{
				return mBriefPalletLifterID;
			}
		}

        /// <summary>
        /// Returns PK id of this briefeing
        /// </summary>
		public decimal BriefRaisablePlatformID
		{
			get  
			{
				return mBriefRaisablePlatformID;
			}
		}

        /// <summary>
        /// Returns PK id of this briefeing
        /// </summary>
		public decimal BriefCranesID
		{
			get  
			{
				return mBriefCranesID;
			}
		}

        /// <summary>
        /// Returns PK id of this briefeing
        /// </summary>
		public decimal BriefRespiratoryMaskID
		{
			get { return mBriefRespiratoryMaskID; }
		}

        /// <summary>
        /// Returns PK id of Id card photo briefing (Lichtbildausweis, older Hitag chip, not SmartAct)
        /// </summary>
		public decimal IdCardPhotoHitagID
		{
			get { return mIdCardPhotoHitagID; }
		}

        /// <summary>
        /// Returns PK id of Id card photo SmartAct briefing  (Lichtbildausweis from SmartAct)
        /// </summary>
        public decimal IdCardPhotoSmActID
        {
            get { return mIdCardPhotoSmActID; }
        }

		public decimal AuthoIndSafetySiteID
		{
			get { return mAuthoIndSafetySiteID; }
		}

        /// <summary>
        /// Returns PK id of this briefeing
        /// </summary>
		public decimal AccessAuthorID
		{
			get { return mAccessAuthID; }
		}

        /// <summary>
        /// Returns PK id of this briefeing
        /// </summary>
		public decimal AccessAuthorSiteSecurityID
		{
			get { return mAccessAuthSiteSecurityID; }
		}

        /// <summary>
        /// Returns PK id of this briefeing
        /// </summary>
		public decimal SafetyAuthorizationID
		{
			get { return mSafetyAuthID; }
		}

        /// <summary>
        /// Returns PK id of this briefeing
        /// </summary>
		public decimal SignatureAuthorID
		{
			get { return mSignatureAuthID; }
		}
		
		public decimal ParkingExternAuthID
		{
			get { return mParkingAuthID; }
		}

        /// <summary>
        /// Returns PK id of this briefeing
        /// </summary>
		public decimal BriefFireman
		{
			get  
			{
				return mBriefFireman;
			}
		}

        /// <summary>
        /// Returns PK id of this briefeing
        /// </summary>
        public decimal BriefApprentice
        {
            get
            {
                return mBriefApprentice;
            }
        }

        /// <summary>
        /// Returns PK id of Vehicle access Long
        /// </summary>
        public decimal BriefVehicleEntranceLongID
		{
			get  
			{
				return mBriefVehicleEntranceLongID;
			}
		}

        /// <summary>
        /// Returns PK id of Vehicle access Short
        /// </summary>
		public decimal BriefVehicleEntranceShortID
		{
			get { return mBriefVehicleEntranceShortID; }
		}

        
        /// <summary>
        /// Returns roleID for plant manager
        /// </summary>
        public decimal PlantManagerRoleID
		{
			get  
			{
				return mPlantManagerRoleID;
			}
		}

        /// <summary>
        /// Returns whether logic for using Hitag2 chips in the id cards active or not
        /// </summary>
        public bool HitagActive
        {
            get
            {
                return mHitagActive;
            }
        }

        /// <summary>
        /// Returns whether logic for using Mifare chips in the id cards active or not
        /// </summary>
        public bool MifareActive
        {
            get
            {
                return mMifareActive;
            }
        }


        /// <summary>
        /// Returns whether Hitag2 id card is required field or not 
        /// </summary>
        public bool HitagRequiredField
        {
            get
            {
                return mHitagRequiredField;
            }
        }

        /// <summary>
        /// Returns whether Mifare id card is required field or not
        /// </summary>
        public bool MifareRequiredField
        {
            get
            {
                return mMifareRequiredField;

            }
        }

        /// <summary>
        /// When parameter is true then export to SmartAct is only available
        /// when the coordinator has granted the industrial safety briefing.
        /// "Sicherheitsunterweisung erfolgt am" on Koordinator page
        /// </summary>
        public bool IndSafety4ExpSmartAct
        {
            get { return mIndSafety4ExpSmartAct; }
            set { mIndSafety4ExpSmartAct = value; }
        }

        /// <summary>
        /// Length of time in years until access for apprentices expires
        /// </summary>
        public decimal ApprenticeExpiry
        {
            get { return mApprenticeExpiry; }
            set { mApprenticeExpiry = value; }
        }


        /// <summary>
        /// Interval for showing automatic popup for Ids from SmartACT
        /// </summary>
        public decimal SmartActIdCardInterval
        {
            get { return mSmartActIdCardInterval; }
            set { mSmartActIdCardInterval = value; }
        }
 
        /// <summary>
        /// Maximimum number of hours that CWR are allowed to be on-site
        /// </summary>
        public decimal AttendHoursLimit
        {
            get { return mAttendHoursLimit; }
            set { mAttendHoursLimit = value; }
        }


        /// <summary>
        /// Returns Base path for report files
        /// </summary>
        public string ReportsBasePath
		{
			get { return mReportsBasePath; }
		}

        /// <summary>
        /// Returns Data path for report files
        /// </summary>
        public string ReportsDataPath
        {
            get { return mReportsBasePath + REPORTS_DATA_DIR; }
        }

        /// <summary>
        /// Returns path for report templates
        /// </summary>
        public string ReportsTemplatePath
        {
            get { return mReportsBasePath + REPORTS_TEMPLA_DIR; }
        }

        /// <summary>
        /// Returns registry subkey to Acrobat Reader to read PDF reports
        /// </summary>
        public string ReportsReaderKey
        {
            get { return REPORTS_REGISTRY; }
        }

        /// <summary>
        /// Returns Name and Path for file "Sicherheitsunterweisung"
        /// </summary>
        public string DocSafetyPathAndName
		{
			get { return mDocSafetyPathAndName; }
		}

		/// <summary>
		/// Coworkers are shown in deletelist mEntranceFlowTime days before validUNTIL expires 
		/// </summary>
		public int ParaEntranceFlowTime
		{
			get { return mParaEntranceFlowTime; }
		}

		public int ParaNumberOfPrintedPass
		{
			get { return mParaNumberOfPrintedPass; }
		}

		public string ParaPrintPreview
		{
			get  { return mParaPrintPreview; }
		}

        /// <summary>
        /// Resp masks: is the interface to TecBos for lent active? (values 0 or 1)
        /// </summary>
        public bool TecBosLentActive
        {
            get { return mTecBosLentActive; }
        }

        /// <summary>
        /// Resp masks: is the interface to TecBos for return active? (values 0 or 1)
        /// </summary>
        public bool TecBosReturnActive
        {
            get { return mTecBosReturnActive; }
        }

        /// <summary>
        /// Resp masks: is the interface to Florix for lent active? (values 0 or 1)
        /// </summary>
        public bool FlorixLentActive
        {
            get { return mFlorixLentActive; }
        }

        /// <summary>
        /// Resp masks: is the interface to Florix for return active? (values 0 or 1)
        /// </summary>
        public bool FlorixReturnActive
        {
            get { return mFlorixReturnActive; }
        }

        /// <summary>
        /// Resp masks: search string "freie Maske": denotes that resp mask is free to be lent in TecBos
        /// </summary>
        public string TecBosMaskFree
        {
            get { return mTecBosMaskFree; }
        }


        /// <summary>
        /// Resp masks: Time period added to next maintainence date in days. To lend a mask this must be bigger than now + this time period
        /// </summary>
        public int RespMaskReserveTime
        {
            get { return mRespMaskReserveTime; }
        }
        
        #endregion Accessors


		#region Methods 

		/// <summary>
		/// Returns the one and only instance of this class.
		/// </summary>
		/// <returns>instance of FPASSFPASSControllSingleton</returns>
		public static Globals GetInstance() 
		{
			if ( null == mInstance ) 
			{
				mInstance = new Globals();
			}
			return mInstance;
		}


		/// <summary>
		/// Gets name of default printer on current machine
		/// </summary>
		/// <returns></returns>
		public String GetDefaultPrinter() 
		{
			System.Drawing.Printing.PrintDocument printDocument1 = new System.Drawing.Printing.PrintDocument();
			return printDocument1.PrinterSettings.PrinterName;
		}

		/// <summary>
		/// Gets the required values from the database (mand dependent)
		/// </summary>
		public void GetValuesFromDatabase()
		{
			mProvider = DBSingleton.GetInstance().DataProvider;
			GetFPASSParameters();
			GetFPASSRolePlaManagerID();
		}

		/// <summary>
		/// Reads the parameter values out of database table FPASS_ParameterFPASS. These can be changed.
        /// FPASSV5 has updated logic: parameters stored as key/value pairs for Mandant 21 (Wesseling) and 22
        /// TODO in a future release: make this more elegant with FactoryPattern?
		/// </summary>
        private void GetFPASSParameters()
        {
            try
            {
                mCommSel = mProvider.CreateCommand(FPASS_PARA_QUERY);
                mProvider.SetParameter(mCommSel, FPASS_PARA_MND_PARA, UserManagementControl.getInstance().CurrentMandatorID);
                mDR = mProvider.GetReader(mCommSel);

                string keyName = "";
                string testVal = "";

                while (mDR.Read())
                {
                    keyName = mDR["PARF_KEY"].ToString();

                    switch (keyName)
                    {
                        case "PARF_VERSION_CHECK_ACTIVE":
                            {
                                testVal = mDR["PARF_VALUE"].ToString();
                                mVersionCheckActive = (testVal == "1");
                                break;
                            }
                        case "PARF_ACCESSSHORT":
                            {
                                mParAccessShort = Convert.ToDouble(mDR["PARF_VALUE"]);
                                break;
                            }
                        case "PARF_ACCESSMIDDLE":
                            {
                                mParAccessMiddle = Convert.ToDouble(mDR["PARF_VALUE"]);
                                break;
                            }
                        case "PARF_ACCESSLONG":
                            {
                                mParAccessLong = Convert.ToDouble(mDR["PARF_VALUE"]);
                                break;
                            }
                        case "PARF_COOLPERIODBRIEF":
                            {
                                mParCoolPeriodBrief = Convert.ToDouble(mDR["PARF_VALUE"]);
                                break;
                            }
                        case "PARF_PIC_MAXKB":
                            {
                                mMaxPhotoBufferSize = Convert.ToInt32(mDR["PARF_VALUE"]);
                                break;
                            }
                        case "PARF_GATEWAYSHORT":
                            {
                                mParVehEntryShort = Convert.ToDouble(mDR["PARF_VALUE"]);
                                break;
                            }
                        case "PARF_TK":
                            {
                                mTKNumberRoot = Convert.ToDecimal(mDR["PARF_VALUE"]);
                                break;
                            }
                        case "PARF_ACCESSINDICATOR":
                            {
                                mParAccessIndicator = Convert.ToDecimal(mDR["PARF_VALUE"]);
                                break;
                            }
                        case "PARF_REPORTSPATH":
                            {
                                mReportsBasePath = mDR["PARF_VALUE"].ToString();
                                break;
                            }
                        case "PARF_DOCSAFETY":
                            {
                                mDocSafetyPathAndName = mDR["PARF_VALUE"].ToString();
                                break;
                            }
                        case "PARF_FPASSVERSION":
                            {
                                mFPASSDatabaseVersion = mDR["PARF_VALUE"].ToString();
                                break;
                            }
                        case "PARF_TARGETDIR_FPASS_SMARTACT":
                            {
                                mTargetDirectoryFpassToSmartAct = mDR["PARF_VALUE"].ToString();
                                break;
                            }
                        case "PARF_TARGETFILE_FPASS_SMARTACT":
                            {
                                mTargetFileFpassToSmartAct = mDR["PARF_VALUE"].ToString();
                                break;
                            }
                        case "PARF_LOGDIR_FPASS_SMARTACT":
                            {
                                mLogDirectoryFpassToSmartAct = mDR["PARF_VALUE"].ToString();
                                break;
                            }
                        case "PARF_LOGFILE_FPASS_SMARTACT":
                            {
                                mLogFileFpassToSmartAct = mDR["PARF_VALUE"].ToString();
                                break;
                            }
                        case "PARF_LOG_AKTIV":
                            {
                                testVal = mDR["PARF_VALUE"].ToString();
                                mLogActive = (testVal == "1");
                                break;
                            }
                        case "PARF_LOG_COLLECT_DAYS":
                            {
                                mLogCollectDays = Convert.ToDecimal(mDR["PARF_VALUE"]);
                                break;
                            }
                        case "PARF_PERSNO_PREFIX":
                            {
                                mPersnoPrefix = mDR["PARF_VALUE"].ToString();
                                break;
                            }
                        case "PARF_PERSNO_PREFIX_DEL":
                            {
                                mPersnoPrefixDel = mDR["PARF_VALUE"].ToString();
                                break;
                            }
                        case "PARF_HITAG2_ACTIVE":
                            {
                                testVal = mDR["PARF_VALUE"].ToString();
                                mHitagActive = (testVal == "1");
                                break;
                            }
                        case "PARF_MIFARE_ACTIVE":
                            {
                                testVal = mDR["PARF_VALUE"].ToString();
                                mMifareActive = (testVal == "1");
                                break;
                            }
                        case "PARF_HITAG2_REQ_FIELD":
                            {
                                testVal = mDR["PARF_VALUE"].ToString();
                                mHitagRequiredField = (testVal == "1");
                                break;
                            }
                        case "PARF_MIFARE_REQ_FIELD":
                            {
                                testVal = mDR["PARF_VALUE"].ToString();
                                mMifareRequiredField = (testVal == "1");
                                break;
                            }
                        case "PARF_APPRENT_EXPIRE":
                            {
                                mApprenticeExpiry = Convert.ToDecimal(mDR["PARF_VALUE"]);
                                break;
                            }
                        case "PARF_SMARTACT_IDCARD_INTERVAL":
                            {
                                mSmartActIdCardInterval = Convert.ToDecimal(mDR["PARF_VALUE"]);
                                break;
                            }
                        case "PARF_HOURSLIMIT":
                            {
                                mAttendHoursLimit = Convert.ToDecimal(mDR["PARF_VALUE"]);
                                break;
                            }
                        case "PARF_IND_SAFE_EXPORTSMACT":
                            {
                                testVal = mDR["PARF_VALUE"].ToString();
                                mIndSafety4ExpSmartAct = (testVal == "1");
                                break;
                            }
                       case "PARF_FPASS_FLORIX_LENTACTIVE":
                        {
                            testVal = mDR["PARF_VALUE"].ToString();
                            mFlorixLentActive = (testVal == "1");
                            break;
                        }
                       case "PARF_FPASS_FLORIX_RETURNACTIVE":
                        {
                            testVal = mDR["PARF_VALUE"].ToString();
                            mFlorixReturnActive = (testVal == "1");
                            break;
                        }
                       case "PARF_FPASS_TECBOS_LENTACTIVE":
                        {
                            testVal = mDR["PARF_VALUE"].ToString();
                            mTecBosLentActive = (testVal == "1");
                            break;
                        }
                       case "PARF_FPASS_TECBOS_RETURNACTIVE":
                        {
                            testVal = mDR["PARF_VALUE"].ToString();
                            mTecBosReturnActive = (testVal == "1");
                            break;
                        }
                       case "PARF_TECBOS_FREE":
                        {
                            mTecBosMaskFree = mDR["PARF_VALUE"].ToString();
                            break;
                        }
                       case "PARF_PARF_MASK_MAINT_RESERVE":
                        {
                            mRespMaskReserveTime = Convert.ToInt32(mDR["PARF_VALUE"]);
                            break;
                        }
                        case "PARF_MEDICAL_MAX_ASSIGN":
                            {
                                mPrecMedicalMaxAssign = Convert.ToInt32(mDR["PARF_VALUE"]);
                                break;
                            }
                    }
                }

                if (mCommSel.Connection is OracleConnection)
                {
                    // Get database and DB User name
                    mDatabaseName = ((OracleConnection) mCommSel.Connection).DataSource;

                    // Username not exposed, get it from connect string
                    // First cut off "userid= from beginning, then cut off everyrthing after first ;
                    string conTxt = mCommSel.Connection.ConnectionString.Replace("user id=", "");
                    int firstSep = conTxt.IndexOf(';', 0);
                    mDatabaseUser = conTxt.Substring(0, firstSep);

                }
                else mDatabaseName = "nicht Oracle";

                mDR.Close();
            }
            catch (OracleException oraex)
            {
                throw new UIFatalException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR) + oraex.Message);
            }
            catch (DbAccessException)
            {
                throw new UIFatalException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR));
            }
            catch (Exception gex)
            {
                throw new UIFatalException(gex.Message, gex);
            }
        }

		/// <summary>
		/// Reads ID of role "Plant Manager" out of database (dependent on mandator)
		/// </summary>
		private void GetFPASSRolePlaManagerID()
		{
			try
			{
				mCommSel = mProvider.CreateCommand(FPASS_ROLEPLA_QUERY);
				mProvider.SetParameter(mCommSel, FPASS_ROLEPLA_MND_PARA, UserManagementControl.getInstance().CurrentMandatorID);
                mProvider.SetParameter(mCommSel, FPASS_ROLEPLA_NAME_PARA, UserManagementControl.ROLE_BETRIEBSMEISTER);
				mDR = mProvider.GetReader(mCommSel);

				while (mDR.Read())
				{				
					mPlantManagerRoleID = Convert.ToDecimal( mDR["RL_ROLEID"] );
				}
				mDR.Close();		
			}
			catch (OracleException oraex)
			{
				throw new UIFatalException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR) + oraex.Message);
			}
			catch ( DbAccessException)
			{
                throw new UIFatalException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR));
			}
		}

		#endregion Methods
	}
}

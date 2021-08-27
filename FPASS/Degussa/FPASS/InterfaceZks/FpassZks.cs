using System;
using System.Runtime.InteropServices; // to use external DLLs
using System.Text; // to use the StringBuilder class
using System.Data; // IProvider, IDbCommand, IDataReader
using System.Collections; // SortedList
using System.Windows.Forms;

using Degussa.FPASS.Db; // DBSingleton
using Degussa.FPASS.Util.ErrorHandling;
using Degussa.FPASS.Util.Messages; // MessageSingleton
using Degussa.FPASS.Util.Exceptions;
using Degussa.FPASS.Util.ProgressUtil; // progressWindow
using Degussa.FPASS.Util.UserManagement;
using Degussa.FPASS.Util;

using de.pta.Component.DbAccess;
using de.pta.Component.DbAccess.Exceptions;
using de.pta.Component.Errorhandling;
using de.pta.Component.Common;
using Degussa.FPASS.Util.Enums;
using System.Collections.Generic;

namespace Degussa.FPASS.InterfaceZks
{

	/// <summary>
	/// Interface between PTA-FPASS and Interflex-ZKS
	/// uses Interflex DLL "C:\WINNT\System32\6010DLL.dll"
	/// </summary>
	public class FpassZks
	{
		
		#region Declaration DLL-Functions

		// ----------------------------------------	
		// long in C = 32 bit integer = Int32 in C#
		// long in C# = 64 bit integer
		// ----------------------------------------

		// open connection to server
		[DllImport("6010dll.dll",
			 EntryPoint = "Open6010", SetLastError = true, CharSet = CharSet.Ansi, 
			 ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
		private static extern Int32 Open6020(
			[In, MarshalAs(UnmanagedType.LPStr)] StringBuilder prmHost,		// IN: Name of the ZKS Server (localhost or computer name)
			Int32 prmPort,													// IN: Port-No of the Zks Server
			[In, MarshalAs(UnmanagedType.LPStr)] StringBuilder prmUser,		// IN: User Login
			[In, MarshalAs(UnmanagedType.LPStr)] StringBuilder prmPwd 		// IN: User Password
			); // OUT: returncode: <0 = error, >0 = success


		// close connection to server
		[DllImport("6010DLL.dll", 
			 EntryPoint = "Close6010", SetLastError = true, CharSet = CharSet.Ansi,
             ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
		private static extern Int32 Close6020();	// OUT: returncode: <0 = error, >0 = success


		// INSERT CoWorker in ZKS
		[DllImport("6010DLL.dll", 
			 EntryPoint = "InsPstamm", SetLastError = true, CharSet = CharSet.Ansi,
             ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
		private static extern Int32 InsPstamm(
			[In, MarshalAs(UnmanagedType.LPStr)] StringBuilder prmCoWorkerData	// IN: Coworker main data
			); // OUT: returncode: <0 = Error, >0 = Number of manipulated rows


		// UPDATE CoWorker in ZKS
		[DllImport("6010DLL.dll", 
			 EntryPoint = "UpdPstamm", SetLastError = true, CharSet = CharSet.Ansi,
             ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
		private static extern Int32 UpdPstamm(
			[In, MarshalAs(UnmanagedType.LPStr)] StringBuilder prmKey,			// IN: CoWorker key (TK + PersNo)
			[In, MarshalAs(UnmanagedType.LPStr)] StringBuilder prmDaten			// IN: CoWorker data to update
			); // returncode: <0 = Error, >0 = Number of manipulated rows


		// DELETE in ZKS
		[DllImport("6010DLL.dll", 
			 EntryPoint = "DelPstamm", SetLastError = true, CharSet = CharSet.Ansi,
             ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
		private static extern Int32 DelPstamm(
			[In, MarshalAs(UnmanagedType.LPStr)] StringBuilder prmDaten			// IN: Keys (TK + PersNo) of CoWorkers to delete
			); // OUT: returncode: <0 = Error, >0 = Number of manipulated rows

			
		// Access to terminal to read CardIdNo
		[DllImport("6010DLL.dll", 
			 EntryPoint = "GetAWNR", SetLastError = true, CharSet = CharSet.Ansi,
             ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
		private static extern Int32 GetAWNR(
			[In, Out, MarshalAs(UnmanagedType.LPStr)] StringBuilder prmBuffer,		// IN / OUT: String Buffer, holds the returned CardId without leading 0
			Int32 prmTermNo														// IN: Terminal-No
			); // OUT: returncode: <0 = error, >0 = success

		#endregion
	
		#region Members

        /// <summary>
        /// Current ID card type. Defaults to Hitag2
        /// </summary>
        private string mIDCardType = IDCardTypes.Hitag2;
        
		private StringBuilder mHost;
		private Int32 mPortNo;
		private StringBuilder mUser;
		private StringBuilder mPassword;
		private Int32 mTerminalNo;
		
        /// <summary>
        /// Returncode of DLL-function call
        /// </summary>
		internal Int32 mResult;
		
        /// <summary>
        /// Id card number as read from ZKS terminal (can be Hitag or Mifare)
        /// </summary>
        private StringBuilder mIdCardNo;

		internal SortedList mCoWorkerTable;

        /// <summary>
        /// Holds list of cwr Ids to be sent to ZKS
        /// </summary>
        private List<string> mCoWorkerIdList;

        /// <summary>
        /// Mandator Id
        /// </summary>
        private decimal mMandatorId;

        /// <summary>
        /// Hitag id card number
        /// </summary>
        private decimal? mIdCardNumHitag;
        /// <summary>
        /// Valid ID card number?
        /// </summary>
        private bool mIdCardHitagOK;       
        /// <summary>
        /// Mifare id card number
        /// </summary>
        private decimal? mIdCardNumMifare;
        /// <summary>
        /// Valid ID card number?
        /// </summary>
        private bool mIdCardMifareOK;
        /// <summary>
        /// Valid Until date
        /// </summary>
        //private DateTime? mValidUntil;

        // Full start and end date of main authorization 
        private DateTime mCoWorkerValidFrom;
        private DateTime mCoWorkerValidUntil;

        /// <summary>
        /// Id card last read from ZKS terminal
        /// </summary>
        private string mLastIdCardNo;
        /// <summary>
        /// Holds result of last operation
        /// </summary>
        private string mMessageResult;

		private IDbTransaction mCurrentTransaction;
		static string SELECT_FOR_TRANSACTION = "TransactionDummy";


        /// <summary>
        /// Prefix used to identify data for CwrPlant entries (Betreibliche Unterweisung) in datastring for ZKS
        /// </summary>
        private const string PLANT_PREFIX = "BU_BetrNr";

        /// <summary>
        /// Prefix used to identify data for precautionary medical entries (Vorsorgeuntersuchungen) in datastring for ZKS
        /// </summary>
        private const string PRECMED_PREFIX = "VSU";

        // List of all SqlCommandIds and their parameters
        private const string SELECT_COWORKER = "SelectZksCoWorker";
		private const string SELECT_COWORKER_PARAMETER = ":CWR_ID";
		private const string SELECT_AUTHORIZATION = "SelectAccessAuthorization";
		private const string SELECT_AUTHORIZATION_PARAMETER = ":RATH_CWR_ID";
		private const string SELECT_VEHICLEREGNO = "SelectVehicleRegistrationNumber";
		private const string SELECT_VEHICLEREGNO_PARAMETER = ":VRNO_CWR_ID";
		private const string SELECT_ZKSPARAMETER = "SelectZksParameter";
		private const string PARAMETER_MANDANT = ":PARZ_MND_ID";
		private const string SELECT_VEHICLEAUTHORIZATION = "SelectZksVehicleAuth";
		private const string SELECT_VEHICLEAUTHORIZATION_PARAMETER = ":CWBR_CWR_ID";
		private const string SELECT_TERMINALPARAMETER = "SelectZksTerminalParameter";
		private const string SELECT_TERMINALPARAMETER_PARAMETER = ":TER_COMPUTERNAME";
		private const string SELECT_COWORKER_ALL = "SelectZksAllCoWorkers";
		private const string SELECT_COWORKER_ALL_PARAMETER = ":CWR_MND_ID";
		private const string UPDATE_COWORKER = "UpdateCWRZKS";
		private const string UPDATE_COWORKER_PARAMETER1 = ":CWR_ID";
		private const string UPDATE_COWORKER_PARAMETER2 = ":CWR_RETURNCODE_ZKS";
		private const string UPDATE_COWORKER_PARAMETER3 = ":CWR_CHANGEUSER";
		private const string UPDATE_COWORKER_PARAMETER4 = ":CWR_TIMESTAMP";
		private const string SELECT_SHORTVEHACCESS = "SelectShortGateway";
		private const string SELECT_SHORTVEHACCESS_PARAMETER = ":PARF_MND_ID";
        private const string SELECT_COWORKER_PLANTS_ZKS = "SelectZksCoworkerPlants";
        private const string SELECT_COWORKER_PLANTS_ZKS_CWR_ID = ":CWPL_CWR_ID";
        private const string SELECT_COWORKER_PRECMED_ZKS = "SelectZksPrecMedicals";
        private const string SELECT_COWORKER_PRECMED_ZKS_CWR_ID = ":PMED_CWR_ID";

        #endregion Members


        #region Constructors

        public FpassZks()
		{
			this.Initialize();
		}

		#endregion Constructors

        #region Accessors

        /// <summary>
        /// Current ID card type. Defaults to Hitag2
        /// </summary>
        internal string IDCardType
        {
            get { return mIDCardType; }
            set { mIDCardType = value; }
        }

        /// <summary>
        /// List of cwr Ids to be sent to ZKS
        /// </summary>
        internal List<string> CoWorkerIdList
        {
            get { return mCoWorkerIdList; }
            set { mCoWorkerIdList = value; }
        }

        /// <summary>
        /// Holds result of last operation
        /// </summary>
        internal string MessageResult
        {
            get { return mMessageResult; }
            set { mMessageResult = value; }
        }

        #endregion

        #region Initialization

        private void Initialize()
		{
		}

		#endregion Initialization

		#region Methods
		
		/// <summary>
		/// Connect to ZKS
		/// </summary>
		/// <returns>error string</returns>
		public string Connect()
		{	
			try
			{

                // Get id card reader nr (terninal number) from Usermanagement, but how do we know whether it's Hitag or Mifare?
                if (mIDCardType == IDCardTypes.Mifare)
                {
                    mTerminalNo = UserManagementControl.getInstance().IDCardReaderMifare;
                }
                else mTerminalNo = UserManagementControl.getInstance().IDCardReaderHitag;
               

				// gets connection parameters corresponding to the computername of the current user
				IProvider zksDataProvider = DBSingleton.GetInstance().DataProvider;

				// Creates the select commands
				IDbCommand selectCmd = zksDataProvider.CreateCommand(SELECT_TERMINALPARAMETER);
                zksDataProvider.SetParameter(selectCmd, SELECT_TERMINALPARAMETER_PARAMETER, Environment.MachineName.ToUpper());

				// Opens data reader to get data from database with the select command
				IDataReader zksDataReader = zksDataProvider.GetReader(selectCmd);

				// reads rows
				while (zksDataReader.Read())
				{
					// initializes Connection parameters
					mHost = new StringBuilder(zksDataReader["TER_HOSTNAME"].ToString().ToUpper());
					mPortNo = Convert.ToInt32(zksDataReader["TER_PORTNO"].ToString());

                    // If no value saved in Usermanagement then get default value from FPASS_Terminal
                    if (mTerminalNo == 0)
                    {
                        mTerminalNo = Convert.ToInt32(zksDataReader["TER_TERMINALNO"].ToString());
                    }

					mLastIdCardNo = zksDataReader["TER_LASTIDCARDNO"].ToString();

                    // FPASS V5 username/password no longer required
                    mPassword = new StringBuilder(String.Empty); 
                    mUser = new StringBuilder(String.Empty); 
				}

				// Close the reader
				zksDataReader.Close();

				// opens a connection to ZKS
                Globals.GetInstance().Log.Info("Öffne verbindung zum ZKS über die dll...");

				mResult = Open6020(this.mHost, this.mPortNo, this.mUser, this.mPassword);  
				if (0 > mResult)
				{
                    Globals.GetInstance().Log.Error("Die Verbindung zum ZKS zum Host und Port "
                       + this.mHost + ":" + this.mPortNo
                       + " konnte nicht geöffnet werden."
                       + " Fehlercode der ZKS-Schnittstelle: " + mResult);

                    return MessageSingleton.GetInstance().GetMessage(MessageSingleton.ZKS_NO_CONNECT);
				}
				else
				{
                    Globals.GetInstance().Log.Info("Result der Verbindung zum ZKS: " + mResult);
                    return MessageSingleton.GetInstance().GetMessage(MessageSingleton.ZKS_OK); 
				}
			}			
			catch (Exception e)
			{
				Globals.GetInstance().Log.Error(e.Message + "Stack:" + e.StackTrace);
				return MessageSingleton.GetInstance().GetMessage(MessageSingleton.ZKS_NO_CONNECT) + "Exception: " + e.Message + e.StackTrace;
			}
		}


		/// <summary>
		/// Disconnects from ZKS
		/// </summary>
		/// <returns>error string</returns>
		public string Disconnect()
		{
			try
			{
				if (0 > mResult)
				{
					return MessageSingleton.GetInstance().GetMessage(MessageSingleton.ZKS_NO_CONNECT);
				}
				else
				{
					return MessageSingleton.GetInstance().GetMessage(MessageSingleton.ZKS_OK);
				}
			}			
			catch (Exception)
			{			
				return MessageSingleton.GetInstance().GetMessage(MessageSingleton.ZKS_NO_CONNECT);
			}
		}

        #region ReadIdCardMethods

        /// <summary>
		/// Reads an Id Card Number from a terminal
		/// </summary>
		/// <param name="prmTerminalNo"></param>
		/// <returns>error string or read number</returns>
		private string ReadIdCardNo(Int32 prmTerminalNo)
		{
			// Variables
			mIdCardNo = new StringBuilder();
			string idCardNo;
			
			try
			{
                Globals.GetInstance().Log.Info("Lese Ausweisnummer aus ZKS...");
				mResult = GetAWNR(mIdCardNo, prmTerminalNo);
                if (0 > mResult)
                {
                    idCardNo = MessageSingleton.GetInstance().GetMessage(MessageSingleton.ZKS_NO_IDCARDNO);
                    
                    Globals.GetInstance().Log.Error("Die Ausweisnummer konnte aus dem Terminal "
                        + prmTerminalNo
                        + " nicht gelesen werden."
                        + " Fehlercode der ZKS-Schnittstelle: " + mResult);
                    return MessageSingleton.GetInstance().GetMessage(MessageSingleton.ZKS_NO_DELETE_NO_CWR);
                }
                else
                {
                    Globals.GetInstance().Log.Info("Ergebinis Lese Ausweisnummer aus ZKS " + mResult);

                    idCardNo = mIdCardNo.ToString();
                    Globals.GetInstance().Log.Info("Demzufolge ist die IDCardNummer " + idCardNo);
                }

				return idCardNo;
			}
			catch (Exception)
			{
				return MessageSingleton.GetInstance().GetMessage(MessageSingleton.ZKS_NO_IDCARDNO);
				
			}
		}


		/// <summary>
		/// read id number from default terminal
		/// </summary>
		/// <returns></returns>
		public string ReadIdCardNo()
		{
			return ReadIdCardNo(mTerminalNo);
		}


        /// <summary>
        /// returns the latest card number that has been read from the terminal
        /// </summary>
        /// <returns>latest read card number</returns>
        public string GetLastReadIdCardNo()
        {
            return mLastIdCardNo;
        }

        #endregion ReadIdCards

        /// <summary>
		/// Inserts coworker data from FPASS into ZKS by calling insert or update functions in external 6010 DLL
		/// </summary>
		/// <param name="prmCoWorkerId">Id of coworker to be loaded and sent to ZKS</param>
        /// <param name="pAlreadyInit">true if coworker data have already been loaded, no need to do it again</param>
		/// <returns>error string</returns>
		public string Insert(decimal prmCoWorkerId, bool pAlreadyInit)
		{
			// Variables
			string coWorkerKeyString;
			string coWorkerDataString;
			StringBuilder coWorkerData;
			StringBuilder coWorkerKey;

            try
            {
                // builds data string from the Database if not already happened
                // if coworker data have already been loaded then no need to do it again
                if (!pAlreadyInit) 
                    this.GetDataFromDb(prmCoWorkerId);


                // Checks to see if CWR has access (Main authorization). If not, then show error and do not export to ZKS                
                if (!mCoWorkerTable["RATH_RECEPTAUTHO_YN"].ToString().ToUpper().Equals(Globals.DB_YES))
                {
                    Globals.GetInstance().Log.Error("Error: the current coworker (PersNr " 
                        + CompletePersNo(mCoWorkerTable["CWR_PERSNO"].ToString())
                        + "cannot be exported to ZKS because access has not been granted in FPASS.");
                    return String.Empty;
                }



                // initializes ZKS input string
                string inputString = this.GetDataStringMain();
                coWorkerDataString = this.GetKeyStringInsert() + inputString;
                coWorkerData = new StringBuilder(coWorkerDataString);


                if (LoggingSingleton.GetInstance().IsDebugEnabled)
                {
                    // For testing purposes
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(Globals.GetInstance().ReportsDataPath + "\\WriteZKSString.txt"))
                    {
                        var line = coWorkerDataString.Replace(" ", "\r\n");
                        file.WriteLine(line);
                    }
                }

                // calls insert
                this.mResult = InsPstamm(coWorkerData);

                if (0 > this.mResult)
                {
                    // CoWorker is already in ZKS database => tries update with same data
                    // initializes input parameters
                    coWorkerKeyString = GetKeyStringUpdateWithTK();
                    coWorkerDataString = inputString;
                    coWorkerKey = new StringBuilder(coWorkerKeyString);
                    coWorkerData = new StringBuilder(coWorkerDataString);

                    mResult = UpdPstamm(coWorkerKey, coWorkerData);

                    if (0 > this.mResult)
                    {
                        Globals.GetInstance().Log.Error(
                                String.Format(MessageSingleton.GetInstance().GetMessage(MessageSingleton.ZKS_SAVE_INVALID), CompletePersNo(mCoWorkerTable["CWR_PERSNO"].ToString())
                                 + " Fehlercode der ZKS-Schnittstelle: " + mResult));

                        // if this is returncode 101 or 301 (errors)
                        if (this.mResult == -101 || this.mResult == -301)
                        {
                            throw new InterfaceZKSException(0);
                        }
                        else
                        {
                            return MessageSingleton.GetInstance().GetMessage(MessageSingleton.ZKS_NO_INSERT_UPDATE);
                        }
                    }
                    else
                    {
                        return MessageSingleton.GetInstance().GetMessage(MessageSingleton.ZKS_OK);
                    }
                }
                else
                {
                    return MessageSingleton.GetInstance().GetMessage(MessageSingleton.ZKS_OK);
                }
            }
            catch (InterfaceZKSException ize)
            {
                Globals.GetInstance().Log.Error(
                        String.Format(MessageSingleton.GetInstance().GetMessage(MessageSingleton.ZKS_SAVE_INVALID), CompletePersNo(mCoWorkerTable["CWR_PERSNO"].ToString())
                        + " Exception aus ZKS: " + ize.Message + ize.StackTrace));
                return MessageSingleton.GetInstance().GetMessage(MessageSingleton.ZKS_SAVE_INVALID);
               
            }
            catch (Exception iye)
            {
                Globals.GetInstance().Log.Error(
                        String.Format(MessageSingleton.GetInstance().GetMessage(MessageSingleton.ZKS_SAVE_INVALID), CompletePersNo(mCoWorkerTable["CWR_PERSNO"].ToString())
                    + " Exception aus ZKS: " + iye.Message + iye.StackTrace));
                return MessageSingleton.GetInstance().GetMessage(MessageSingleton.ZKS_NO_CONNECT);
            }
		}

 
		/// <summary>
		/// update coworker idcard-no from FPASS in ZKS
		/// </summary>
		/// <param name="prmCoWorkerId"></param>
		/// <returns>string error</returns>
		public string Update(decimal prmCoWorkerId)
		{
			// Variables
			string coWorkerKeyString;
			string coWorkerDataString;
			StringBuilder coWorkerData;
			StringBuilder coWorkerKey;

			try
			{
				// builds data string from the Database
				this.GetDataFromDb(prmCoWorkerId); // Member mIdCardNo will be set and is a empty string

				// CoWorker is already in ZKS database => tries to update only idcardno
				// initializes input parameters
				coWorkerKeyString  = this.GetKeyStringUpdateWithTK();
				coWorkerDataString = this.GetDataStringWithIdCardNo();
				coWorkerKey        = new StringBuilder(coWorkerKeyString);
				coWorkerData       = new StringBuilder(coWorkerDataString);

                mResult = UpdPstamm(coWorkerKey, coWorkerData);
				if (0 > this.mResult)
				{
					Globals.GetInstance().Log.Error( "Der Fremdfirmenmitarbeiter mit der Personalnummer "
						+ this.CompletePersNo(mCoWorkerTable["CWR_PERSNO"].ToString())
						+ " und dem technischen Schlüssel "
						+ mCoWorkerTable["CWR_TK"].ToString()
						+ " konnte nicht nach ZKS exportiert werden." 
						+ " Fehlercode der ZKS-Schnittstelle: " + mResult );
					return MessageSingleton.GetInstance().GetMessage(MessageSingleton.ZKS_NO_UPDATE_IDCARDNO);
				} 
				else
				{
					return MessageSingleton.GetInstance().GetMessage(MessageSingleton.ZKS_OK);
				} 
			} 		
			catch (Exception)
			{
				Globals.GetInstance().Log.Error( "Der Fremdfirmenmitarbeiter mit der Personalnummer "
					+ this.CompletePersNo(mCoWorkerTable["CWR_PERSNO"].ToString())
					+ " und dem technischen Schlüssel "
					+ mCoWorkerTable["CWR_TK"].ToString()
					+ " konnte nicht nach ZKS exportiert werden." 
					+ MessageSingleton.GetInstance().GetMessage(MessageSingleton.ZKS_NO_CONNECT) );
				return MessageSingleton.GetInstance().GetMessage(MessageSingleton.ZKS_NO_CONNECT);
			} 
		} 
		

		/// <summary>
		/// delete coworker data in ZKS
		/// </summary>
		/// <param name="prmCoWorkerId"></param>
		/// <returns>error code</returns>
		public string Delete(decimal prmCoWorkerId)
		{
			// Variables
			string coWorkerKeyString;
			StringBuilder coWorkerKey;

			try
			{
				// builds data string from the Business Object
				this.GetDataFromDb(prmCoWorkerId);
				coWorkerKeyString = this.GetKeyStringDelete();

				// initializes input parameter
				coWorkerKey = new StringBuilder(coWorkerKeyString);

				// calls delete
                Globals.GetInstance().Log.Info("Lösche Ausweisnummer aus ZKS ......");
				this.mResult = DelPstamm(coWorkerKey);

				if (0 > this.mResult)
				{
                    Globals.GetInstance().Log.Error(
                            String.Format(MessageSingleton.GetInstance().GetMessage(MessageSingleton.ZKS_DEL_INVALID), 
                                    CompletePersNo(mCoWorkerTable["CWR_PERSNO"].ToString())
                                    ) 
                            + " Fehlercode der ZKS-Schnittstelle: " + mResult);

					return MessageSingleton.GetInstance().GetMessage(MessageSingleton.ZKS_NO_DELETE_NO_CWR);
				} 
				else
				{                    
                    Globals.GetInstance().Log.Info("Ergebinis Delete aus ZKS " + mResult);
					return MessageSingleton.GetInstance().GetMessage(MessageSingleton.ZKS_OK);
				} 
			} 
			catch (Exception zex)
			{
				Globals.GetInstance().Log.Error(
                            String.Format(MessageSingleton.GetInstance().GetMessage(MessageSingleton.ZKS_DEL_INVALID), 
                                    CompletePersNo(mCoWorkerTable["CWR_PERSNO"].ToString())
                                    ) 
                    + zex.Message);

                return MessageSingleton.GetInstance().GetMessage(MessageSingleton.ZKS_NO_CONNECT);
			} 
		} 


        /// <summary>
        /// Checks that current CWR has correct number of Id cards filled (one of Hitag Mifare or both)
        /// See also DB parameters HitagActive/MifareActive).
        /// and CWR access has not expired (validUntil date)
        /// Returns true if id cards are filled correctly, otherwise CWR cannot be sent to ZKS
        /// </summary>
        /// <returns></returns>
        internal bool CoworkerValidZKS()
        {
            bool idHitagEmpty = mIdCardNumHitag.ToString().Equals("") || mIdCardNumHitag.ToString().Equals("-");
            bool idMifareEmpty = mIdCardNumMifare.ToString().Equals("") || mIdCardNumMifare.ToString().Equals("-");
            bool noLongerValid = mCoWorkerValidUntil < DateTime.Now;

            MessageSingleton msgSinglet = MessageSingleton.GetInstance();
            mMessageResult = String.Format(msgSinglet.GetMessage(MessageSingleton.ZKS_SAVE_INVALID), mCoWorkerTable["CWR_PERSNO"].ToString());


            // 1st check: cannot copy to ZKS if CWR no longer valid  or no Id cards filled out
            if (noLongerValid || (idHitagEmpty && idMifareEmpty))
            {
                // Build error messages for case that CWR access has expired and both Id cards are empty
                if (noLongerValid)                  
                    mMessageResult += msgSinglet.GetMessage(MessageSingleton.ZKS_ACCESS_EXPIRED);
                if (idHitagEmpty && idMifareEmpty) 
                    mMessageResult += msgSinglet.GetMessage(MessageSingleton.ZKS_NO_HITAG) + msgSinglet.GetMessage(MessageSingleton.ZKS_NO_MIFARE);

                return false;
            }


            // 2nd check: Parameters determine which Id fields are required
            // Check Hitag values: if parameter "hitag required" is true then id card cannot be empty
            if (Globals.GetInstance().HitagActive && Globals.GetInstance().HitagRequiredField)
            {
                mIdCardHitagOK = !idHitagEmpty;
            }
            else mIdCardHitagOK = true;

            // Do same for Mifare
            if (Globals.GetInstance().MifareActive && Globals.GetInstance().MifareRequiredField)
            {
                mIdCardMifareOK = !idMifareEmpty;
            }
            else mIdCardMifareOK = true;


            // Builds the error message if CWR does not have correct Id cards filled according to DB parameters HitagActive/MifareActive
            if (!mIdCardHitagOK)
                mMessageResult += msgSinglet.GetMessage(MessageSingleton.ZKS_NO_HITAG);
            if (!mIdCardMifareOK)
                mMessageResult += msgSinglet.GetMessage(MessageSingleton.ZKS_NO_MIFARE);

            return mIdCardHitagOK && mIdCardMifareOK;
        }

		/// <summary>
		/// Get coworker-dependent data from various DB tables (different SQL SELECTS)
		/// and initialize and fill Sorted List with coworker attributes
		/// </summary>
		/// <param name="pCoWorkerId"></param>
		internal void GetDataFromDb(decimal pCoWorkerId)
		{
			// initializes sorted list. CoWorker data will be put in this list
			mCoWorkerTable = new SortedList();

			// fills Sorted List with CoWorker data
			LoadData(SELECT_COWORKER, SELECT_COWORKER_PARAMETER, pCoWorkerId);

			// fills Sorted List with Access Authorization data (RATH_RATT_ID = 2 xor RATH_RATT_ID = 5, "Zutrittsberechtigung") -> 0-1 row
			LoadData(SELECT_AUTHORIZATION, SELECT_AUTHORIZATION_PARAMETER, pCoWorkerId);

			// fills Sorted List with Vehicle data -> 0-n rows
			LoadData(SELECT_VEHICLEREGNO, SELECT_VEHICLEREGNO_PARAMETER, pCoWorkerId);

            // MandatorId
            decimal mndId = Convert.ToDecimal(this.mCoWorkerTable["CWR_MND_ID"]);

            // Loads ZKS parameters
            LoadZksParameters(SELECT_ZKSPARAMETER, PARAMETER_MANDANT, mndId);

			// fills Sorted List with a value which indicates whether a vehicle authorization has been granted or not
			// CWBR_BRF_ID = 48 ("KFZ-Zutritt kurz") or CWBR_BRF_ID = 49 ("KFZ-Zutritt lang")
			// only one can be active, so that the other one is inactive; only the active one will be selected -> 0-1 row
			LoadData(SELECT_VEHICLEAUTHORIZATION, SELECT_VEHICLEAUTHORIZATION_PARAMETER, pCoWorkerId);

            // fills List with ZKS-plants having validuntil-dates for the coworker from fpass_cwr_plant
            LoadDataCoworkerPlants(pCoWorkerId);

            LoadDataCoworkerMedicals(pCoWorkerId);


        }

        /// <summary>
        /// Gets ZKS parameters (values for access models) from database 
        /// </summary>
        private void LoadZksParameters(string prmSqlCommandId, string prmParamName, decimal prmParamValue)
        {
            // Get DataProvider from DbAccess component & Creates select command
            IProvider zksDataProvider = DBSingleton.GetInstance().DataProvider;
            IDbCommand selectCmd = zksDataProvider.CreateCommand(prmSqlCommandId);

            // Set filter on mandant id
            zksDataProvider.SetParameter(selectCmd, prmParamName, prmParamValue);

            // Opens data reader to get data from database with the select command
            IDataReader zksDataReader = zksDataProvider.GetReader(selectCmd);

            while (zksDataReader.Read())
            {
                // saves parameter key & value in the sorted list
                if (!zksDataReader["PARZ_VALUE"].Equals(DBNull.Value))
                    mCoWorkerTable.Add(zksDataReader["PARZ_KEY"].ToString(), zksDataReader["PARZ_VALUE"].ToString());
            }

            zksDataReader.Close();
        }


		/// <summary>
		/// Fills SortedList (member variable mCoWorkerTable) with coworker attributes
		/// attributes are read from coworker database tables depending on name of SQL command passed to method
		/// </summary>
		/// <param name="prmSqlCommandId">ID of SQL SELECT command (as defined in Configuration.xml)</param>
		/// <param name="prmParamName">SQL Selection parameter (eg PK of coworker) to filter results</param>
		/// <param name="prmParamValue">Value of this parameter</param>
		private void LoadData(string prmSqlCommandId, string prmParamName, decimal prmParamValue)
		{
			// Variables
			int columnNo;
			int columnCount = 0;
			int rows = 0;
			string columnValue = "";
			string columnName;
            IDataReader zksDataReader = null;

            try
			{
				// Get DataProvider from DbAccess component & Creates select command
				IProvider zksDataProvider = DBSingleton.GetInstance().DataProvider;
				IDbCommand selectCmd = zksDataProvider.CreateCommand(prmSqlCommandId);

				// creates a parameter in the sql command as needed for it to be executed
				zksDataProvider.SetParameter(selectCmd, prmParamName, prmParamValue);

				// Opens data reader to get data from database with the select command
				zksDataReader = zksDataProvider.GetReader(selectCmd);
				columnCount = zksDataReader.FieldCount;
				
				while (zksDataReader.Read())
				{
					// a CoWorker can have more than one registration number => several rows
					if (prmSqlCommandId.Equals(SELECT_VEHICLEREGNO))
					{
						// a concatenation of all registration numbers must be saved in one field of the sorted list
                        columnValue = columnValue + zksDataReader["VRNO_VEHREGNO"] + " ";
					} 
					else
					{
						// reads values in columns
						for (columnNo = 0; columnNo < columnCount; columnNo++)
						{
							// gets column name & value from db
							columnName = zksDataReader.GetName(columnNo);

							if (prmSqlCommandId.Equals(SELECT_VEHICLEAUTHORIZATION))
							{
								// there can be 2 rows (a short and a long vehicle authorization)
								// adds the number of the row (0 or 1) at the end of the key to avoid double keys
								columnName += rows.ToString();
							}

							if (null == zksDataReader.GetValue(columnNo))
								columnValue = "-";
							else
								columnValue = zksDataReader.GetValue(columnNo).ToString();

							if (columnValue.Equals("")) // value in db was null or ""
								columnValue = "-"; // DLL functions do not correct work with an empty string

                            // Gets Id card numbers from DB
                            if (columnName.Equals("CWR_IDCARDNO"))
                            {
                                object colIdCd = zksDataReader[columnName];
                                mIdCardNumHitag = (colIdCd.Equals(DBNull.Value) ? null : new Nullable<decimal>(Convert.ToDecimal(colIdCd)));
                            }
                            if (columnName.Equals("CWR_MIFARENO"))
                            {
                                object colIdCd = zksDataReader[columnName];
                                mIdCardNumMifare = (colIdCd.Equals(DBNull.Value) ? null : new Nullable<decimal>(Convert.ToDecimal(colIdCd)));
                            }
                            if (columnName.Equals("CWR_VALIDUNTIL"))
                            {
                                object colIdCd = zksDataReader[columnName];
                                mCoWorkerValidUntil = Convert.ToDateTime(colIdCd);
                            }

                            // saves column name & value in the sorted list
							mCoWorkerTable.Add(columnName, columnValue);
						} 
					}

 
					// one more row has been read
					rows++;				
				}

                // inserts the string containing all registration numbers in the sorted list
                if (prmSqlCommandId.Equals(SELECT_VEHICLEREGNO))
				{
					// a concatenation of all registration numbers must be saved in one field of the sorted list
					columnName = "VRNO_VEHREGNO";
                    columnValue = columnValue.Trim();
					if (columnValue.Equals(""))
					{
						// no registration number found (rows = 0)
						columnValue = "-"; // DLL functions do not work correctly with an empty string
					}
					mCoWorkerTable.Add(columnName, columnValue);
				} 
				else if (0 == rows)
				{
					// no data found
					// reads column names and sets an empty string as value for each field in the sorted list
					for (columnNo = 0; columnNo < columnCount; columnNo++)
					{
						// gets column name & value from db
						columnName  = zksDataReader.GetName(columnNo);
						columnValue = "-";
						mCoWorkerTable.Add(columnName, columnValue);
					}
				}
				zksDataReader.Close();

			} 			
			catch (Exception ex)
			{
                if (null != zksDataReader && !zksDataReader.IsClosed) zksDataReader.Close();

                throw new UIErrorException("prmSqlCommandId = " + prmSqlCommandId + Environment.NewLine + ex.Message, ex);
			}
		}

        /// <summary>
        /// Add entries for CwrPlant ("Betriebliche Unterweisung") to mCoWorkerTable.
        /// </summary>
        /// <param name="prmParamValue">ParamValue</param>
        private void LoadDataCoworkerPlants(decimal prmParamValue)
        {

            // Variables
            string columnValue = "";
            string columnName = "";

            try
            {
                // Get DataProvider from DbAccess component & Creates select command
                IProvider zksDataProvider = DBSingleton.GetInstance().DataProvider;
                IDbCommand selectCmd = zksDataProvider.CreateCommand(SELECT_COWORKER_PLANTS_ZKS);

                // creates a parameter in the sql command as needed for it to be executed
                zksDataProvider.SetParameter(selectCmd, SELECT_COWORKER_PLANTS_ZKS_CWR_ID, prmParamValue);

                // Opens data reader to get data from database with the select command
                IDataReader zksDataReader = zksDataProvider.GetReader(selectCmd);

                while (zksDataReader.Read())
                {
                    // Get Plant_Number and ValidDate
                    columnName = PLANT_PREFIX + zksDataReader.GetValue(0).ToString();
                    columnValue = zksDataReader.GetValue(1).ToString();

                    // saves column name & value in the sorted list
                    mCoWorkerTable.Add(columnName, columnValue);
                }
                zksDataReader.Close();
            }
            catch (Exception ex)
            {
                throw new UIErrorException("LoadDataCoworkerPlants" + " " + ex.Message, ex);
            }       
        }


        /// <summary>
        /// Loads data from DB for current coworker's precuationary medicals (could be up to 10 granted)
        /// </summary>
        /// <param name="prmParamValue"></param>
        private void LoadDataCoworkerMedicals(decimal prmParamValue)
        {

            // Variables
            string columnValue = "";
            string columnName = "";

            try
            {
                // Get DataProvider from DbAccess component & Creates select command
                IProvider zksDataProvider = DBSingleton.GetInstance().DataProvider;
                IDbCommand selectCmd = zksDataProvider.CreateCommand(SELECT_COWORKER_PRECMED_ZKS);

                // creates a parameter in the sql command as needed for it to be executed
                zksDataProvider.SetParameter(selectCmd, SELECT_COWORKER_PRECMED_ZKS_CWR_ID, prmParamValue);

                // Opens data reader to get data from database with the select command
                IDataReader zksDataReader = zksDataProvider.GetReader(selectCmd);

                string sKey;
                int rowctr = 1;
                while (zksDataReader.Read())
                {
                    // Get prec med details.                  
                    for (int colnr = 1; colnr<4; colnr++)
                    {
                        switch (colnr) 
                        {
                            case 1:
                                sKey = "TYP";
                                break;
                            case 2:
                                sKey = "NAME";
                                break;
                            case 3:
                                sKey = "DATUM";
                                break;
                            default:
                                sKey = "X";
                                break;
                        }

                        // Type, name, validuntil date
                        columnName = PRECMED_PREFIX + rowctr.ToString("00") + sKey;
                        columnValue = Convert.ToString(zksDataReader[colnr]);

                        // saves column name & value in the sorted list
                        mCoWorkerTable.Add(columnName, columnValue);
                    }
                    rowctr++;
                }
                zksDataReader.Close();
            }
            catch (Exception ex)
            {
                throw new UIErrorException("LoadDataCoworkerMedicals" + " " + ex.Message, ex);
            }
        }


        /// <summary>
        /// Builds second part of datastring for current coworker, this is sent to ZKS
        /// Further field values in sorted list (mCoWorkerTable) are transferred into a data string
        /// </summary>
        /// <returns></returns>
        internal string GetDataStringMain()
		{
			string dataString = "";			
			
			// Full start and end date of main authorization 
            mCoWorkerValidFrom = Convert.ToDateTime(mCoWorkerTable["CWR_VALIDFROM"].ToString());
            mCoWorkerValidUntil = Convert.ToDateTime(mCoWorkerTable["CWR_VALIDUNTIL"].ToString());

	
			// dates main authorisation for ZKS
			string dateFromMain = "";
			string timeFromMain = "";
            string dateUntilMain = "";
			string timeUntilMain = "";
	
			
			// decides what to transfer to ZKS
			//--------------------------------
            
            // start and end dates of the main authorization
            // Extract date & time as transferred as separate fields         
            dateFromMain = ExtractDate(mCoWorkerTable["CWR_VALIDFROM"].ToString());
            timeFromMain = ExtractTime(mCoWorkerTable["CWR_VALIDFROM"].ToString());
            dateUntilMain = ExtractDate(mCoWorkerTable["CWR_VALIDUNTIL"].ToString());
            timeUntilMain = ExtractTime(mCoWorkerTable["CWR_VALIDUNTIL"].ToString());

            if (0 < mCoWorkerValidFrom.CompareTo(mCoWorkerValidUntil))
			{
				// if the main authorization starts after its end then the start must be equal to the end
				dateFromMain = dateUntilMain;
				timeFromMain = timeUntilMain;
                mCoWorkerValidFrom = mCoWorkerValidUntil;
			}

          
			// transfers the field values in the sorted list into a data string
			//-----------------------------------------------------------------
			// String = Interflex String Format -> PARAM="Value"
			// Long / Short = Interflex Long / Short Format -> PARAM=Value
			// Boolean = Interflex Boolean -> PARAM=0 or PARAM=1
			// Date / Time = Interflex Date / Time Format -> PARAM="DDMMYYYY" or PARAM="HHMMSS"

			// Beware!!! 
			// dataString must have no space at its beginning!!!
			// -> causes Update to save wrong data in ZKS and however to answer OK!!!)
            dataString = "AWNR=\"" + mCoWorkerTable["CWR_IDCARDNO"].ToString() + "\""		// string
                        + " AWNRMIFARE=\""  + mCoWorkerTable["CWR_MIFARENO"].ToString()           + "\""      // Mifarenummer 
                        + " STPINFO31=\""   + mCoWorkerTable["CWR_WINDOWS_ID"].ToString()         + "\""      // KonzernId 
						+ " NACHNAME=\""	+ mCoWorkerTable["CWR_SURNAME"].ToString()			+ "\""		// string
						+ " VORNAME=\""		+ mCoWorkerTable["CWR_FIRSTNAME"].ToString()			+ "\""		// string
						+ " KU12=\""		+ mCoWorkerTable["VRNO_VEHREGNO"].ToString()			+ "\""		// string
						+ " KU14=\""		+ mCoWorkerTable["EXCONTRACTOR"].ToString()			+ "\""		// string
						+ " KU19=\""		+ mCoWorkerTable["SUBEXCONTRACTOR"].ToString()		+ "\""		// string
						+ " KU20=\""		+ mCoWorkerTable["SUPERVISOR"].ToString()				+ "\""		// string
						+ " KU25=\""		+ mCoWorkerTable["USER_NAME"].ToString()				+ "\""		// string
						+ " KU23=\""		+ mCoWorkerTable["CWR_PLACEOFBIRTH"].ToString()		+ "\""		// string
						+ " KU92=\""		+ this.ExtractDate(mCoWorkerTable["CWR_DATEOFBIRTH"].ToString())	+ "\""	// date
						+ " TRANSDATE=\""	+ this.ExtractDate(mCoWorkerTable["CWR_DATECREATED"].ToString())	+ "\""	// date
						+ " BERDATEVON=\""	+ dateFromMain	+ "\""
						+ " BERTIMEVON=\""	+ timeFromMain	+ "\""
						+ " BERDATEBIS=\""	+ dateUntilMain	+ "\""
						+ " BERTIMEBIS=\""	+ timeUntilMain	+ "\""
						+ " BUCBERECHTIGT=\"" + this.StringToBoolean(mCoWorkerTable["RATH_RECEPTAUTHO_YN"].ToString()) + "\""
						+ " ABTEILUNG=\""	+ mCoWorkerTable["DEPT_DEPARTMENT"].ToString() + "\""	// string
						+ " VERFOLGSTUF="	+ mCoWorkerTable["PARZ_ZONENTRACING"].ToString()		// long
						+ " ZEIMO="			+ mCoWorkerTable["PARZ_TIMEMODEL1"].ToString()		// long
						+ " ZUGMO="			+ mCoWorkerTable["PARZ_ACCESSMODEL1"].ToString()		// long
						+ " ZUGMODATEVON=\""  + "\""	
						+ " ZUGMOTIMEVON=\""  + "\""	
						+ " ZUGMODATEBIS=\"" + "\""
						+ " ZUGMOTIMEBIS=\"" + "\"";
			

            dataString += this.GetDataStringVehicleAccess();
            dataString += this.GetDataStringParkExternal();
            dataString += this.GetDataStringOptionalAccessModels();




            // add entries for CwrPlant 
            // and precautionary medicals
            // BU_BetrNr1="18.06.2017" BU_BetrNr12="14.04.2017" 
            foreach (object key in mCoWorkerTable.Keys)
            {
                if (key.ToString().StartsWith(PLANT_PREFIX) || key.ToString().StartsWith(PRECMED_PREFIX))
                {
                    dataString += " " + key.ToString() + "=\"" + mCoWorkerTable[key].ToString() +"\"";
                }
            }


			// the last parameters to transfer
			// PARZ_BUK ("Buchungskreis") must be 4 characters long with leading 0
			string parz_buk = mCoWorkerTable["PARZ_BUK"].ToString();
			while (4 > parz_buk.Length)
			{
				 parz_buk = "0" + parz_buk;
			}

			dataString += " PSTYP=" + mCoWorkerTable["PARZ_TYPE"].ToString()			
						+ " KU02=\"" + parz_buk + "\""		
						+ " KU03=\"" + mCoWorkerTable["PARZ_PLACE"].ToString() + "\"";	

			return dataString;		
		}



        /// <summary>
        /// Returns the data string with vehicle access information.
        /// Currently this is access model number 2 in ZKS
        /// </summary>
        private string GetDataStringVehicleAccess()
        {
            // ID for a short vehicle authorization
            const string SHORT_VEH_BRIEFING = "48";

            bool shortVehGranted = false;
            bool longVehGranted = false;
            bool shortVehWished = false;
            bool longVehWished = false;
            bool foundVehicleBriefing = false;

            // briefing date read from database
            string dateTimeVehBriefing = "";

            // Start and and date for access model 2 
            string dateFromVeh = "";
            string dateUntilVeh = "";
            string timeFromVeh = "";
            string timeUntilVeh = "";

            // Vehicle access authorization: there can be up to two authorizations rows from the database (for both short and long authorization)
            for (int row = 0; row < 2; row++)
            {
                if (mCoWorkerTable.ContainsKey("CWBR_BRF_ID" + row.ToString()))
                {
                    foundVehicleBriefing = true;

                    if (!(mCoWorkerTable["CWBR_BRIEFINGDATE" + row.ToString()].ToString().Equals("-")))
                    {
                        if (mCoWorkerTable["CWBR_INACTIVE_YN" + row.ToString()].ToString().ToUpper().Equals("N"))
                        {
                            // a granted authorization has been found (there can be only one)
                            // whether this authorization has been wished or not doesn't matter
                            dateTimeVehBriefing = mCoWorkerTable["CWBR_BRIEFINGDATE" + row.ToString()].ToString();
                            shortVehGranted = (mCoWorkerTable["CWBR_BRF_ID" + row.ToString()].ToString().Equals(SHORT_VEH_BRIEFING));
                            longVehGranted = !shortVehGranted;

                        }
                        else if (mCoWorkerTable["CWBR_BRIEFING_YN" + row.ToString()].ToString().ToUpper().Equals("Y"))
                        {
                            // a wished authorization has been found (either not granted or revoked)
                            // the access model 2 must be transfered to ZKS with the system date and time so that it will not be valid in ZKS
                            shortVehWished = (mCoWorkerTable["CWBR_BRF_ID" + row.ToString()].ToString().Equals(SHORT_VEH_BRIEFING));
                            longVehWished = !shortVehWished;
                        }
                    }
                }
            }


            if (foundVehicleBriefing)
            {
                // Set dates etc for the vehicle authorizations
                // If access is granted, time is always 00:00-24:00
                // If access is granted (never given or given then revoked), time is always 00:00-00:01
                if (shortVehGranted)
                {
                    // Vehicle access Short granted
                    string[] periods = ValidateAccessModelPeriod(dateTimeVehBriefing);
                    dateFromVeh = periods[0];
                    timeFromVeh = "00:00:00"; 

                    // calculates end date of the short vehicle auhorization
                    // short authorization for a vehicle is a DB parameter 
                    DateTime fullDateFromVeh = Convert.ToDateTime(dateFromVeh + " " + timeFromVeh);
                    dateTimeVehBriefing = Convert.ToString(fullDateFromVeh.AddDays(Globals.GetInstance().VehEntryShort));

                    // authorization always ends with foot auth.
                    dateUntilVeh = ExtractDate(dateTimeVehBriefing);

                    // Time Until is always set to 24:00:00 on last day
                    timeUntilVeh = "24:00:00"; 

                    /*
                    timeUntilVeh = ExtractTime(dateTimeVehBriefing);
                    DateTime fullDateUntilVeh = Convert.ToDateTime(dateUntilVeh + " " + timeUntilVeh);

                    // If short vehicle authorization period does not match main authorization (on foot)
                    if ((0 < fullDateFromVeh.CompareTo(mCoWorkerValidUntil)) || (0 < mCoWorkerValidFrom.CompareTo(fullDateUntilVeh)))
                    {
                        dateUntilVeh = dateFromVeh;
                        timeUntilVeh = timeFromVeh;
                    }
                     * */
                }
                else if (longVehGranted)
                {
                    // Vehicle access Long granted
                    // Same as Id card dates
                    string[] periods = ValidateAccessModelPeriod(dateTimeVehBriefing);
                    dateFromVeh = "";
                    timeFromVeh = "00:00:00";
                    dateUntilVeh = ""; 
                    timeUntilVeh = "24:00:00";

                }
                else 
                {
                    // vehicle authorization has not been granted - probably revoked.
                    // FPASS cannot delete the access model in ZKS so it updates it with current date.
                    dateFromVeh = "";  
                    timeFromVeh = "00:00:00";
                    dateUntilVeh = ""; 
                    timeUntilVeh = "00:01:00";
                }
            }
            else
            {
                // There are no vehicle access records, means never wished
                dateFromVeh = "";   
                dateUntilVeh = "";   
                timeFromVeh = "00:00:00";
                timeUntilVeh = "00:01:00";
            }


            /*
             *   // Optional terminal groups are always granted, so time is always 00:00-24:00
            while (mCoWorkerTable.ContainsKey(keyStringFpass))
            {
                keyStringZks = keyCtrZks.ToString();

                dataString += " ZUGMO" + keyStringZks + "=" + mCoWorkerTable[keyStringFpass].ToString()
                            + " ZUGMODATEVON" + keyStringZks + "=\"" + "\""
                            + " ZUGMOTIMEVON" + keyStringZks + "=\"00:00:00\""
                            + " ZUGMODATEBIS" + keyStringZks + "=\"" + "\""
                            + " ZUGMOTIMEBIS" + keyStringZks + "=\"24:00:00\"";        
*/


            // Return the data string
            return " ZUGMO1=" + mCoWorkerTable["PARZ_ACCESSMODEL2"].ToString()
                        + " ZUGMODATEVON1=\"" + "\""
                        + " ZUGMOTIMEVON1=\"" + timeFromVeh + "\""
                        + " ZUGMODATEBIS1=\"" + dateUntilVeh + "\""
                        + " ZUGMOTIMEBIS1=\"" + timeUntilVeh + "\"";
        }


        /// <summary>
        /// Fills the data string with parking information "P extern".
        /// Currently this is access model number 3 in ZKS
        /// </summary>
        private string GetDataStringParkExternal()
        {
            // for P external authorization
            bool pExternGranted = false;
            //bool pExternRevoked = false;

            string dateTimePExternAuth = "";

            // Start and and date for access model 3 
            string dateFromPExtern = "";
            string dateUntilPExtern = "";
            string timeFromPExtern = "";
            string timeUntilPExtern = "";

            if (mCoWorkerTable.ContainsKey("RATH_RECEPTAUTHO_YN6"))
            {
                // has P external been granted? (Erteilt) ("Angeordnet"-assigned- gibt es nicht)
                pExternGranted = (mCoWorkerTable["RATH_RECEPTAUTHO_YN6"].ToString() == Globals.DB_YES);
                //pExternRevoked = (mCoWorkerTable["RATH_RECEPTAUTHO_YN6"].ToString() == Globals.DB_NO);
            }


            // Validation            
            if (pExternGranted)
            {
                // Authorization P external: start and end date are always the same as coworker access
                // Set times to 00:00 - 24:00
                // dateTimePExternAuth = mCoWorkerTable["RATH_RECEPTAUTHODATE6"].ToString();

                //string[] periods = ValidateAccessModelPeriod(dateTimePExternAuth);
                dateFromPExtern = ""; 
                timeFromPExtern = "00:00:00";
                dateUntilPExtern = ""; 
                timeUntilPExtern = "24:00:00"; 
            }
            else 
            {
                // P external has been revoked or was never given: have to update access model in ZKS as it cannot be deleted
                dateFromPExtern = "";
                timeFromPExtern = "00:00:00";
                dateUntilPExtern = "";
                timeUntilPExtern = "00:01:00"; 
            }


            
            var dataString = " ZUGMO2=" + mCoWorkerTable["PARZ_ACCESSMODEL3"].ToString()
                            + " ZUGMODATEVON2=\"" + dateFromPExtern + "\""
                            + " ZUGMOTIMEVON2=\"" + timeFromPExtern + "\""
                            + " ZUGMODATEBIS2=\"" + dateUntilPExtern + "\""
                            + " ZUGMOTIMEBIS2=\"" + timeUntilPExtern + "\"";

            return dataString;         
        }


        /// <summary>
        /// Fills the data string with information for access models 4 to 10
        /// Maps keys in ZKS ZUGMO2= to access models from DB PARZ_ACCESSMODEL3
        /// Note ZKS keys are counted 3..9 and FPASS counts 4..10
        /// These are optional
        /// </summary>
        private string GetDataStringOptionalAccessModels()
        {
            var accessModelFpass = "PARZ_ACCESSMODEL";
            var keyCtrFpass = 4;
            string keyStringFpass = accessModelFpass + keyCtrFpass.ToString();

            string dataString = "";
            int keyCtrZks = keyCtrFpass - 1;
            string keyStringZks;

            // Start and and date for access model
            string[] periods = ValidateAccessModelPeriod(mCoWorkerValidFrom.ToString());
     

            // TODO: update .net so we can do string interpolation!
            // Optional terminal groups are always granted, so time is always 00:00-24:00
            while (mCoWorkerTable.ContainsKey(keyStringFpass))
            {
                keyStringZks = keyCtrZks.ToString();

                dataString += " ZUGMO" + keyStringZks + "=" + mCoWorkerTable[keyStringFpass].ToString()
                            + " ZUGMODATEVON" + keyStringZks + "=\"" + "\""
                            + " ZUGMOTIMEVON" + keyStringZks + "=\"00:00:00\""
                            + " ZUGMODATEBIS" + keyStringZks + "=\"" + "\""
                            + " ZUGMOTIMEBIS" + keyStringZks + "=\"24:00:00\"";        

                keyCtrZks++;
                keyCtrFpass++;
                keyStringFpass = accessModelFpass + keyCtrFpass.ToString();
            }
            return dataString;
        }



		/// <summary>
		/// Validates start and end times given for new authorization (e.g. vehicle, P external)
		/// against start and end time of main authorization (on foot)
		/// </summary>
		/// <param name="pDateTimeAuthModel">begin date/time of new auth</param>
		/// <returns></returns>
		private string[] ValidateAccessModelPeriod(string pDateTimeAuthModel)
		{
            string dateUntilMain = mCoWorkerValidUntil.ToString("dd.MM.yyyy");
            string timeUntilMain = mCoWorkerValidUntil.ToString("HH:mm:ss");
			
			string dateFromModel;
			string timeFromModel;
			string dateUntilModel; 
			string timeUntilModel;
		
			// get start date of new authorization
			dateFromModel = this.ExtractDate(pDateTimeAuthModel);
			DateTime fullDateAuthModel = Convert.ToDateTime(pDateTimeAuthModel);
				
			if ( !(0 < fullDateAuthModel.CompareTo(DateTime.Now)) )
			{
				timeFromModel = this.ExtractTime(pDateTimeAuthModel);
			} 
			else
			{
				// if the authorization starts in the future then it starts at 00:00:00
				timeFromModel = "00:00:00";
			}

			// gets the end date of auhorization = as long as the main authorization (on foot)
			dateUntilModel = dateUntilMain;				
			timeUntilModel = timeUntilMain;
			
			// Validation of authorization period
			// must be within main authorization period (on foot)
            if (0 < fullDateAuthModel.CompareTo(mCoWorkerValidUntil))
			{						
				dateUntilModel = dateFromModel;
				timeUntilModel = timeFromModel;
			}
			else
			{
                if (0 < mCoWorkerValidFrom.CompareTo(fullDateAuthModel))
				{
					// if the new authorization starts before the start date of the main authorization it will be truncated
                    dateFromModel = mCoWorkerValidFrom.ToString("dd.MM.yyyy");
                    timeFromModel = mCoWorkerValidFrom.ToString("HH:mm:ss");		
				}
			} 			
			return new string[4] {dateFromModel, timeFromModel, dateUntilModel, timeUntilModel};			
		}
		
		/// <summary>
		/// extract the date from a datetime string
		/// if prmString is not a DateTime or a date, no extraction occurs
		/// prmDateTime = STRING = "TT.MM.JJJJ HH:MM:SS", first character has position 0 in the string!
		/// such a datetime or a date contains more than 9 characters and a dot at position 2 and position 5
		/// </summary>
		/// <param name="prmDateTime"></param>
		/// <returns></returns>
		private string ExtractDate(string prmDateTime)
		{
			// Variables
			string date = prmDateTime;

			if (10 <= date.Length)
			{
				if ( (date.Substring(2,1).Equals(".")) && (date.Substring(5,1).Equals(".")) )
					date = date.Substring(0, 10); // "TT.MM.JJJJ" = 10 characters from 1. character = position 0
			}
			return date;
		} 


		/// <summary>
		/// extract the time from a datetime string
		/// if prmString is not a DateTime or a date, no extraction occurs
		/// prmDateTime = STRING = "TT.MM.JJJJ HH:MM:SS", first character has position 0 in the string!
		/// such a datetime with a time contains more than 18 characters and a double dot at position 13 and position 16
		/// </summary>
		/// <param name="prmDateTime"></param>
		/// <returns></returns>
		private string ExtractTime(string prmDateTime)
		{
			// Variables
			string time = prmDateTime;

			if (19 <= time.Length)
			{
				if ( (time.Substring(13,1).Equals(":")) && (time.Substring(16,1).Equals(":")) )
					time = time.Substring(11, 8); // "HH:MM:SS" = 8 characters from 12. character = position 11
			}
			return time;
		} 


		/// <summary>
		/// Converts a character into a boolean
		/// </summary>
		/// <param name="prmChar"></param>
		/// <returns></returns>
		private short StringToBoolean(string prmChar)
		{
			if (prmChar.Equals(Globals.DB_YES))
				return 1;
			else
				return 0; // "N" or something else (like "-")
		} 


		/// <summary>
		/// set terminal to be read
		/// </summary>
		/// <param name="prmTermNo">Terminal number</param>
		public void SetTerminal(int prmTermNo)
		{
			this.mTerminalNo = Convert.ToInt32(prmTermNo);
		} 


		/// <summary>
		/// get the terminal number
		/// </summary>
		/// <returns>terminal number set</returns>
		public int GetTerminal()
		{
			return Convert.ToInt16(this.mTerminalNo);
		}


        /// <summary>
        /// Adds leading 0 to a personal number for it to be 8 characters long (required by ZKS)
        /// </summary>
        /// <param name="mPersNo"></param>
        /// <returns></returns>
		private string CompletePersNo(string mPersNo)
		{
			string s = mPersNo;
			while (8 > s.Length)
			{
				s = "0" + s;
			}
			return s;
		}

        /// <summary>
        /// Fills list of CWR is: these have ReturncodeZKS=N and need re-sending to ZKS
        /// </summary>
        internal void FillCoWorkerIdList()
        {
            mCoWorkerIdList = new List<string>();          

            // Gets DataProvider from DbAccess component
            IProvider zksDataProvider = DBSingleton.GetInstance().DataProvider;

            // Creates the select commands
            IDbCommand selectCmd = zksDataProvider.CreateCommand(SELECT_COWORKER_ALL);

            // Parameter for the command = current mandator id
            mMandatorId = Convert.ToDecimal(UserManagementControl.getInstance().CurrentMandatorID);
            zksDataProvider.SetParameter(selectCmd, SELECT_COWORKER_ALL_PARAMETER, mMandatorId);

            // Opens data reader
            IDataReader zksDataReader = zksDataProvider.GetReader(selectCmd);

            // reads number of rows and saves the ids of the coworker to insert / update
            //--------------------------------------------------------------------------
            while (zksDataReader.Read())
            {
                // gets read id and saves it in the list
                mCoWorkerIdList.Add(zksDataReader["CWR_ID"].ToString());
                //rows++;
            }

            zksDataReader.Close();

        }


		/// <summary>
		/// tries to insert all coworker data again.
		/// these data could not be inserted / updated in ZKS as they were inserted or updated in FPASS
		/// </summary>
		/// <returns>error string</returns>
		internal string InsertAll()
		{
			// Variables
			const string CON_TITLE = "FPASS-Daten werden in ZKS übertragen...";
			const string CON_TITLE_UPDATE = "FPASS-Daten werden aktualisiert...";
			string message;
			string result = "";		
			
			int rows = 0; // number of rows to be inserted in ZKS
			int restRows = 0; // number of rows that were not successfully inserted / updated in ZKS
    
			ArrayList CoWorkerIdToUpdateList = new ArrayList();		
			FrmProgress progressWindow = new FrmProgress();

			try
			{
                // If list of CWR ids has not yet been initialised then do it now
                if (mCoWorkerIdList == null)
                {
                    FillCoWorkerIdList();
                }

   
				// is there coworkers to insert / update?
				//---------------------------------------
                if (mCoWorkerIdList.Count > 0)
				{
					// Opens a progress bar window whose length corresponds to the number of rows to be read
					progressWindow.Open(CON_TITLE, rows, 1);

					// reads data
					foreach (string id in mCoWorkerIdList)
					{
						progressWindow.PerformStep();

                        // Check CWR is valid
                        this.GetDataFromDb(Convert.ToDecimal(id));
                        if (this.CoworkerValidZKS())
                        {
                            // read CoWorkerId and try to insert / update the coworker
                            result = this.Insert(Convert.ToDecimal(id), true);

                            if (!(result.Equals(MessageSingleton.GetInstance().GetMessage(MessageSingleton.ZKS_OK))))
                                restRows++;
                            else
                                CoWorkerIdToUpdateList.Add(id); // these coworkers must be updated later
                        }
					}

					// Close the progress window
					progressWindow.Close();
				} 
				else
				{
					restRows = rows;
				} 


				// In FPASS, updates coworkers that have been successful inserted / updated in ZKS
				//--------------------------------------------------------------------------------
				// Opens a progress bar window whose length corresponds to the number of rows to be read
				progressWindow = new FrmProgress();
				progressWindow.Open(CON_TITLE_UPDATE, (rows - restRows), 1);

				foreach (string id in CoWorkerIdToUpdateList)
				{
					progressWindow.PerformStep();
				
					// Updates field CWR_RETURNCODE_ZKS from "N" to "Y"
                    UpdateCoWorker(Convert.ToDecimal(id));
				} 

				// Close the progress window
				progressWindow.Close();

				// have all CoWorkers been inserted / updated?
				//--------------------------------------------
				if (0 < restRows)
				{
					// NO
					// not all rows could be inserted / updated into ZKS
					message = MessageSingleton.GetInstance().GetMessage(MessageSingleton.ZKS_NO_INSERT_ALL) + restRows.ToString();
				}
				else
				{
					// YES
					// all rows have been successfully inserted / updated in ZKS
					message = MessageSingleton.GetInstance().GetMessage(MessageSingleton.ZKS_OK);
				}
				
				return message;
			} 		
			catch (Exception)
			{
				return MessageSingleton.GetInstance().GetMessage(MessageSingleton.ZKS_NO_INSERT_ALL) + "mindestens " + restRows.ToString();
			} 
			finally
			{
				if (progressWindow.IsOpen()) progressWindow.Close();
			}
		}


        /// <summary>
        /// Builds first part of data string to be transferred to ZKS for a new coworker (INSERT)
        /// transfer field values in the sorted list into a data string
        /// a personal number must be 8 characters long in ZKS
        /// a technical key must be 2 characters long in ZKS
        /// Beware!!! 
        /// the dataString must have one space at its end in order to be well concatenated with the second part of the data string later
        /// </summary>
        /// <returns></returns>
        internal string GetKeyStringInsert()
        {
            return "KU01=\"" + mCoWorkerTable["CWR_TK"].ToString().Substring(0, 2) + "\""
                + " PERSNR=\"" + this.CompletePersNo(mCoWorkerTable["CWR_PERSNO"].ToString()) + "\" ";
        }


        /// <summary>
        /// Updates a coworder's status after he has been successfully inserted / updated in ZKS
        /// </summary>
        /// <param name="prmCoWorkerId"></param>
		private void UpdateCoWorker(decimal prmCoWorkerId)
		{
			try
			{				
				IProvider provider = DBSingleton.GetInstance().DataProvider;
				IDbCommand updateCommand = provider.CreateCommand(UPDATE_COWORKER);
				
				// starts a transaction
				this.StartTransaction();
				updateCommand.Transaction = mCurrentTransaction;
				updateCommand.Connection = mCurrentTransaction.Connection;

				// Parameter for the command
				provider.SetParameter( updateCommand, UPDATE_COWORKER_PARAMETER1, prmCoWorkerId );			
				provider.SetParameter( updateCommand, UPDATE_COWORKER_PARAMETER2, "Y" );
				provider.SetParameter( updateCommand, UPDATE_COWORKER_PARAMETER3, UserManagementControl.getInstance().CurrentUserID );
				provider.SetParameter( updateCommand, UPDATE_COWORKER_PARAMETER4, DateTime.Now );
				
				// executes command
				updateCommand.ExecuteNonQuery();

				// commits action
				this.CommitTransaction();
			}
			catch (Exception ex)
			{
				throw new UIErrorException("", ex);
			}
		}

        /// <summary>
        /// uses a reader based on a dummy select command to get and start a transaction
        /// </summary>
		private void StartTransaction() 
		{
			// uses a reader based on a dummy select command to get and start a transaction
			IProvider provider = DBSingleton.GetInstance().DataProvider;
			IDbCommand dummySelectCommand = provider.CreateCommand(SELECT_FOR_TRANSACTION);
			provider.GetReader(dummySelectCommand);

			this.mCurrentTransaction = provider.GetTransaction(dummySelectCommand);
		} 


        /// <summary>
        /// commits insert / update / delete in the database
        /// </summary>
		private void CommitTransaction() 
		{ 
			this.mCurrentTransaction.Commit();
		}

        #endregion Methods

        #region TestMethods
        
        /// <summary>
        /// Builds data string to be transferred to ZKS for an existing coworker, i.e. already in FPASS and ZKS
        /// for the 2 attributes personal number and card id
        /// in the sorted list
        /// 17.05.2004: function not used anymore, GetKeyStringUpdateWithTK() used instead
        /// </summary>
        /// <returns></returns>
        internal string GetKeyStringUpdate()
        {
            return "AWNR=\"" + mCoWorkerTable["CWR_IDCARDNO"].ToString() + "\""
                + " AND PERSNR=\"" + this.CompletePersNo(mCoWorkerTable["CWR_PERSNO"].ToString()) + "\"";
        }


        /// <summary>
        /// Builds data string to be transferred to ZKS for an existing coworker, i.e. already in FPASS and ZKS
        /// for the 2 attributes personal number and technical id (Teilkonzern)
        /// in the sorted list 
        /// </summary>
        /// <returns></returns>
        internal string GetKeyStringUpdateWithTK()
        {
            return "KU01=\"" + mCoWorkerTable["CWR_TK"].ToString().Substring(0, 2) + "\""			// numstring = string
                + " AND PERSNR=\"" + this.CompletePersNo(mCoWorkerTable["CWR_PERSNO"].ToString()) + "\"";	// string	
        }


        /// <summary>
        /// Gets string with coworker key and ID card:
        /// coworkers without idcardno will be deleted
        /// </summary>
        /// <returns></returns>
        private string GetKeyStringDelete()
        {
            return this.GetKeyStringUpdateWithTK();
        }

        /// <summary>
        /// 14.05.2004: test function / not used in production
        /// Builds second part of datastring for current coworker, this is sent to ZKS
        /// only the field value for the idcardno has to be transferred into this data string
        /// </summary>
        /// <returns></returns>
        internal string GetDataStringWithIdCardNo()
        {
            return "AWNR=\"\"" + " NACHNAME=\"" + mCoWorkerTable["CWR_SURNAME"].ToString() + "\""; // empty string for the idcardno
        }

        /// <summary>
        ///  Test method to update all CWR in list without contacting ZKS
        /// </summary>
        internal void UpdateTest()
        {
            foreach (string id in mCoWorkerIdList)
            {
                // Updates field CWR_RETURNCODE_ZKS from "N" to "Y"
                UpdateCoWorker(Convert.ToDecimal(id));
            }
        }

        #endregion TestMethods
    }
}
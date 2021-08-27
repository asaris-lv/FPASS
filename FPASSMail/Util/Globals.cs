using de.pta.Component.DbAccess;
using de.pta.Component.DbAccess.Exceptions;
using de.pta.Component.Errorhandling;
using de.pta.Component.Logging.Log4NetWrapper;
using Evonik.FPASSMail.Db;
using Evonik.FPASSMail.Util.Messages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Evonik.FPASSMail.Util
{
    class Globals
    {
        #region Members

        // myself
        private static Globals mInstance;

        // Current version of FPASS, as given in AssemblyInfo and database table fpass_parameterfpass
        private string mFPASSApplicationVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        private string mFPASSDatabaseVersion = "0";

        private Int16 mParReminderTime;

        // Database access
        private IProvider mProvider;
        private IDbCommand mCommSel;
        private IDataReader mDR;

        // Constants
        private const string FPASS_PARA_QUERY = "GlobalsFPASSPara";
        private const string FPASS_PARA_MND_PARA = ":PARF_MND_ID";
        private const string FPASS_ROLEPLA_QUERY = "GlobalsRoleByMandator";
        private const string FPASS_ROLEPLA_MND_PARA = ":MND_ID";
        private const string FPASS_ROLEPLA_NAME_PARA = ":UM_ROLE_NAME";

        // This appl is only for Wesseling
        private const int MND_WESSELING = 21;

        // Logger instance
        private Logger mLog;

        #endregion

        /// <summary>
        /// Private constructor as this is a singleton
        /// </summary>
        private Globals()
        {
            initialize();
        }


        /// <summary>
        /// Initializes the members.
        /// </summary>
        private void initialize()
        {
            mLog = new Logger("FPASS");
            GetFPASSParameters();
        }

        #region Accessors

        /// <summary>
        /// Returns Logger instance
        /// </summary>
        internal Logger Log { get { return mLog; } }

        /// <summary>
        /// Returns current version of FPASS Client application
        /// </summary>
        public string FPASSApplicationVersion
        {
            get { return mFPASSApplicationVersion; }
        }

        /// <summary>
        /// Returns current version of FPASS in Database
        /// </summary>
        public string FPASSDatabaseVersion
        {
            get { return mFPASSDatabaseVersion; }
        }

        /// <summary>
        /// Returns reminder time (days before access runs out)
        /// </summary>
        public Int16 ReminderTime
        {
            get { return mParReminderTime; }
        }


        /// <summary>
        /// Returns the one and only instance of this class.
        /// </summary>
        /// <returns>instance of FPASSFPASSControllSingleton</returns>
        internal static Globals GetInstance()
        {
            if (null == mInstance)
            {
                mInstance = new Globals();
            }
            return mInstance;
        }

         #endregion

        /// <summary>
        /// Reads the parameter values out of database table FPASS_ParameterFPASS. These can be changed.
        /// FPASSV5 has updated logic: parameters stored as key/value pairs for Mandant 21 (Wesseling) and 22
        /// TODO in a future release: make this more elegant with FactoryPattern?
        /// </summary>
        private void GetFPASSParameters()
        {
            try
            {
                mProvider = DBSingleton.GetInstance().DataProvider;

                mCommSel = mProvider.CreateCommand(FPASS_PARA_QUERY);
                mProvider.SetParameter(mCommSel, FPASS_PARA_MND_PARA, MND_WESSELING);
                mDR = mProvider.GetReader(mCommSel);

                string keyName = "";

                while (mDR.Read())
                {
                    keyName = mDR["PARF_KEY"].ToString();

                    switch (keyName)
                    {
                        case "PARF_REMINDERTIME":
                            {
                                mParReminderTime = Convert.ToInt16(mDR["PARF_VALUE"]);
                                break;
                            }
                        case "PARF_FPASSVERSION":
                            {
                                mFPASSDatabaseVersion = mDR["PARF_VALUE"].ToString();
                                break;
                            }
                        default:
                            break;
 
                    }
                }

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

    }
}

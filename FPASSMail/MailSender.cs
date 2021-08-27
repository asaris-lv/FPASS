using de.pta.Component.DbAccess;
using de.pta.Component.Errorhandling;
using de.pta.Component.Logging.Log4NetWrapper;
using Evonik.FPASSLdap;
using Evonik.FPASSMail.Db;
using Evonik.FPASSMail.Model;
using Evonik.FPASSMail.Util;
using Evonik.FPASSMail.Util.Messages;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;


namespace Evonik.FPASSMail
{
    class MailSender
    {
        #region Members

        private Dictionary<string, Coordinator> mCoordinDict;
        // private Dictionary<decimal, ExternalContractor> mExcoDict;
       // private Dictionary<decimal, CoWorker> mCoWorkerDict;
        //private List<CoWorker> mCoWorkerList;
        private Logger logger = Globals.GetInstance().Log;

        #endregion

        #region Methods

        /// <summary>
        /// Does the main work: gets list of coordinators and coworkers from DB
        /// Gets coordinator's email addresses
        /// Sends mails out.
        /// </summary>
        internal void RemindAll()
        {
            try
            {
                Globals.GetInstance().Log.Info("Starting MailSender....");

                GetCoordinators();

                // If coworker records were returned
                // then get their e-mail addresses and send the mails.
                if (mCoordinDict.Count > 0)
                {
                    GetEMailAddresses();
                    SendReminders();

                    Globals.GetInstance().Log.Fatal("FPASSMail finished successfully.");
                }
                else
                {
                    // Log the fact that no coordinator records were found in the database.
                    Globals.GetInstance().Log.Fatal("Did not find any co-ordinators to mail (no workers with access about to run out). Process finished successfully.");
                    
                }
            }     
            catch (BaseUIException bex)
            {
                // log how many
                logger.Fatal("A business error occurred: " + bex.Message + bex.StackTrace);
                Globals.GetInstance().Log.Fatal("FPASSMail finished with errors.");
            }
            catch (System.Data.OracleClient.OracleException oraex)
            {
                logger.Fatal(MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR) + oraex.Message);
            }
            catch (Exception ex)
            {
                logger.Fatal("A system error occurred: " + ex.Message + ex.StackTrace);
                Globals.GetInstance().Log.Fatal("FPASSMail finished with errors.");
            }
        }


        /// <summary>
        /// Gets list of coordinators to remind by accessing database view. 
        /// Each coordinator object contains a list of external contractors
        /// Each exco has a list of CWR whose access is about to run out.
        /// (CWR has a reference to coordinator and to exco).
        /// </summary>
        private void GetCoordinators()
        {
            CoWorker coWorker;
            Coordinator coordinator;
            ExternalContractor exco;
            string userId;
            decimal excoId;
            decimal cwrId;

            mCoordinDict = new Dictionary<string, Coordinator>();
         

            logger.Info("Getting list of coordinators with coworkers whose access is about to run out....");
            var remindDays = Globals.GetInstance().ReminderTime.ToString();
            logger.Info("Number of days reminder (DB parameter) currently: " + remindDays + " days.");


            // Get DataProvider from DbAccess component, create select command & fill Data Reader 
            IProvider mProvider = DBSingleton.GetInstance().DataProvider;
            IDbCommand selComm = mProvider.CreateCommand("CoordinatorsRemind");
            IDataReader mDR = mProvider.GetReader(selComm);


            // Loop thru records in DataReader and create list of coworker objects.
            int rec = 0;

            while (mDR.Read())
            {
                userId = mDR["US_USERID"].ToString();
                excoId = Convert.ToDecimal( mDR["EXCO_ID"]);
                cwrId = Convert.ToDecimal(mDR["CWR_ID"]);
                rec++;

                // 1.
                // Check against list of unique coordinators (Dictionary)
                // If current coordinator is not there then create a new one.
                // Dictionary key is userid (KonzernId)
                if (!mCoordinDict.Keys.Contains(userId))
                {
                    coordinator = new Coordinator();
                    coordinator.UserId = userId;
                    coordinator.Surname = mDR["US_NAME"].ToString();

                    logger.Debug(String.Format("Created coordinator {0} with userId {1}", coordinator.Surname, userId));

                    // Add coordinator to list
                    mCoordinDict.Add(coordinator.UserId, coordinator);
                    
                }
                else
                {
                    // Get current coworker's coordinator from dictionary 
                    // and assign objects.
                    coordinator = mCoordinDict.FirstOrDefault(q => q.Key == userId).Value;
                }


                // 2.
                // Check against current coordinator's list of External contractors.
                // if exco from current record not there then add it.
                if (!coordinator.ExcoDict.Keys.Contains(excoId))
                {
                    exco = new ExternalContractor();
                    exco.ExcoId = excoId;
                    exco.Name = mDR["EXCO_NAME"].ToString();

                    logger.Debug(String.Format("Created external contractor {0} with Id {1}", exco.Name, excoId));

                    // add to list
                    coordinator.ExcoDict.Add(excoId, exco);
                }
                else
                {
                    // Get current coworker's exco from dictionary 
                    // and assign objects.
                    exco =  coordinator.ExcoDict.FirstOrDefault(q => q.Key == excoId).Value;
                }


                // 3.
                // If the coworker is not assigned to the current coordinator's current excontractor then add it.
                // Note: direct assignment between CWR and coordinator is not relevant here.
                if (!exco.CoWorkerDict.Keys.Contains(cwrId))
                {
                    coWorker = new CoWorker();
                    coWorker.CrwId = Convert.ToDecimal(mDR["CWR_ID"]);
                    coWorker.Surname = mDR["CWR_SURNAME"].ToString();
                    coWorker.Firstname = mDR["CWR_FIRSTNAME"].ToString();
                    coWorker.ValidUntil = Convert.ToDateTime(mDR["CWR_VALIDUNTIL"]);
                    coWorker.IdCardNumber = mDR["CWR_SMARTACTNO"].ToString();
                    coWorker.ExcoId = excoId;
                    coWorker.UserIdCoord = userId;

                    // Coworker's own Coordinator
                    coWorker.CoordSurname = mDR["CWR_COORD_NAME"].ToString();
                    coWorker.CoordFirstname = mDR["CWR_COORD_FIRSTNAME"].ToString();
                    coWorker.CoordTelNo = mDR["CWR_COORD_TELNO"].ToString();

                    exco.CoWorkerDict.Add(cwrId, coWorker);
                 }
            }

            mDR.Close();

            // log how many
            logger.Info(String.Format("Found {0} coordinator/s and {1} records altogether.", mCoordinDict.Count(), rec));         
        }				

        /// <summary>
        /// Queries Active Directory to get coordinators' email addresses based on their userid (KonzernId)
        /// Surnames etc come from FPASS, not ADS.
        /// </summary>
        private void GetEMailAddresses()
        {
            // TODO in a future release: implement user/password to query ADS.
            // Use same encryption as database connectstring in Config.xml
            // bool mCredentialsNeeded = Convert.ToBoolean(ConfigurationManager.AppSettings["LdapSearchDialog.CredentialsNeeded"]);
            //  mLdapSearch.LdapBind(mUserName, mUserPassword);

            logger.Info("About to query ADS for coordinators' e-mail addresses.....");

            // initialize objects
            var mLdapSearch = new LdapSearch();
           

            // If application is running in test mode (PTA) then use dummy user
            // PTA's ADS system does not habe Evonik coordintor data in it.
            string txtSearchTerm = Convert.ToString(ConfigurationManager.AppSettings["Email.TestUser"]);
            bool isTest = (null != txtSearchTerm); 


            foreach (Coordinator coord in mCoordinDict.Values)
            {
                // Sets search term: coordinator's userid.
                if (!isTest)
                    txtSearchTerm = coord.UserId;
                else logger.Warn(String.Format("A test user was found in the configuration file ([Email.TestUser]={0}). This will be used to query ADS.", txtSearchTerm));

                // Execute ADS search
                ActiveDirectoryObject adsEntry = mLdapSearch.SearchOne(txtSearchTerm);

                if (null != adsEntry)
                {
                    coord.EMailAddress = adsEntry.EmailAddress;
                }
                else
                {
                    // Did not find an entry in ADS: warning
                    logger.Error(String.Format("Did not find an entry in ADS with userId {0}. Please check if this is correct.", txtSearchTerm));
                }
            }

            logger.Info("Finished querying ADS for coordinators' e-mail addresses.");
        }

        /// <summary>
        /// Does subtitutions in mail text (name of CWR etc.)
        /// and sends e-mail to each coordinator. 
        /// </summary>     
        private void SendReminders()
        {
            SmtpClient client;
            MailMessage message;
            string msgBaseText;
       
            // Load mail attributes
            bool mailActive = Convert.ToBoolean(ConfigurationManager.AppSettings["Email.Active"]);
            var sender = Convert.ToString(ConfigurationManager.AppSettings["Email.Sender"]);
            var subject = Convert.ToString(ConfigurationManager.AppSettings["Email.Subject"]);           
            var remindDays = Globals.GetInstance().ReminderTime.ToString();
            string individualText;
            logger.Info("About to get e-mail text and send e-mail messages...");


            StringBuilder namesText = new StringBuilder();
            
            // Get base e-mail test from resource file.        
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "Evonik.FPASSMail.Resources.ReminderMail.html";
            logger.Debug("Reading email text from file " + resourceName);


            // Read resources file
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                msgBaseText = reader.ReadToEnd();
            }

            
            // Initialise SMTP client
            logger.Debug("Initialising SMTP client....");
            client = new SmtpClient();
            logger.Debug(String.Format("SMTP client initialised; Host {0} on port {1}.", client.Host, client.Port));

            
            if (!mailActive)
            {
                // Mail not active, log this fact
                logger.Warn(MessageSingleton.GetInstance().GetMessage(MessageSingleton.MSG_EMAIL_INACTIVE));
            }


            // Generate e-mail for each coordinator
            foreach (Coordinator coord in mCoordinDict.Values)
            {
                // This probably shouldn't happen: no email address found
                if (coord.EMailAddress == null)
                {
                    logger.Error(String.Format("No e-mail address found for coordinator {0}. Cannot send message. ", coord.Surname));
                    continue;
                }

                // Begin individual mail text with greeting.
                // Greeting text says "Dear Mr X". Substitute X for coord's name
                individualText = msgBaseText.Replace("{COORDINATOR}", coord.Surname);

                // Substitute "X number of days". 
                individualText = individualText.Replace("{TAGE}", remindDays);


                // Creates sub-heading for each external contractor that the current coordinator is resposible for.
                foreach (ExternalContractor exco in coord.ExcoDict.Values)
                {                   
                    // Go thru List of coworkers in Exco
                    // to build HTML table rows
                    foreach (CoWorker myCwr in exco.CoWorkerDict.Values)
                    {

                        /*
                        * Coworker table heading has this format:
                        * ----------------------------
                        *  <td>Fermdfirmenmitarbeiter</td>
                           <td>Gültig bis</td>
                           <td>Fremdfirma</td>
                           <td>Koordinator des FFMA</td>
                        * 
                         */
                        string cwrCell = "<td>{0}</td>";
                       
                        var cwrNiceName = myCwr.Surname + ", " + myCwr.Firstname;
                        var validNiceName =  myCwr.ValidUntil.ToShortDateString() + " " + myCwr.ValidUntil.ToShortTimeString();
                        var coordNiceName = myCwr.CoordSurname + ", " + myCwr.CoordFirstname + " (" + myCwr.CoordTelNo  + ")";

                        namesText.Append("<tr>" 
                           + string.Format(cwrCell, cwrNiceName) + string.Format(cwrCell, validNiceName) + string.Format(cwrCell, exco.Name) + string.Format(cwrCell, coordNiceName)
                           + "</tr>");

                    }
                }
                
                // And substitute placeholder with CWR list.
                individualText = individualText.Replace("{COWORKER_LIST}", namesText.ToString());

                
                // Logging debug here
                logger.Debug("Current coordinator: " + coord.Surname);
                logger.Debug("Found following list of coworkers: " + namesText.ToString());

                if (mailActive)
                {
                    try
                    {
                        logger.Debug(String.Format("Mail is active, sending reminder to {0} ", coord.EMailAddress));

                        // Create E-Mail message and use SMTP client to send it
                        message = new MailMessage(sender, coord.EMailAddress, subject, individualText);
                        message.IsBodyHtml = true;
                        message.BodyEncoding = System.Text.Encoding.UTF8;

                        client.Send(message);

                        logger.Info(String.Format(
                            MessageSingleton.GetInstance().GetMessage(MessageSingleton.MSG_EMAIL_SUCCESS), subject, coord.EMailAddress)
                            );
                    }
                    catch (Exception mex)
                    {
                        // This is designed to catch exceptions resulting from malformed e-mail addresses, mailbox not reached etc.
                        // for each coordinator
                        logger.Error(String.Format("An error occurred while trying to send a message to coordinator {0} (e-mail address {1}). ", coord.Surname, coord.EMailAddress), mex);

                    }

                }

                namesText.Clear();                  
            }

            logger.Info("Finished sending e-mail messages.");

        }

        #endregion
    }
}

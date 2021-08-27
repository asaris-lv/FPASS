using System;
using System.Text.RegularExpressions;
using System.Collections;
using System.Configuration;
using System.DirectoryServices.Protocols;
using System.Collections.Generic;
using System.DirectoryServices;
using de.pta.Component.Logging.Log4NetWrapper;


namespace Evonik.FPASSLdap
{
	/// <summary>
	/// Holds functionality for query Windows Active Directory
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
	///			<td width="20%">Jul/23/2014</td>
	///			<td width="60%">Creation</td>
	///		</tr>
	/// </table>
	/// </div>
	/// </remarks>
	public class LdapSearch
    {
        #region Members

        public const string SEARCH_WINDOWS_ID = "WINDOWS_ID";
        public const string SEARCH_NAME = "NAME";

		private string mLdapServerName;
        private int    mLdapServerPort;
        private bool   mLdapUseSSL;
        private int    mLdapConnAuthType;
        private int    mLdapLdapProtocolVersion ;
        private string mLdapInvalidCredentialsString;
		private string mLdapBindUserPrefix;
		private string mLdapBindUserBaseDn;

        private DirectoryEntry mDirectoryEntry;
        private ActiveDirectoryObject mADSObject;
        
        private string mLdapSearchDomain;
        //private bool   mLdapUseSpecialUser;
        //private string mLdapSpecialUserName;
        //private string mLdapSpecialUserPassword;
        
		private string mInfoMessage;
        public string ErrorMessage { get; private set; }

        private LdapConnection mLdapConn;
        private Logger mLog = new Logger("FPASS");

		#endregion Members

        #region Accessors

        /// <summary>
        /// InfoMessage with DebugInfo
        /// </summary>
        public string InfoMessage
        {
            get { return mInfoMessage; }
            private set { mInfoMessage = value; }
        }

        #endregion Accessors


		#region Constructors

		/// <summary>
		/// Standard Constructor for LDAP search.
		/// </summary>
		public LdapSearch()
		{
			this.Initialize();
		} 

		#endregion Constructors

        #region Methods
        
        #region Inititialization

        /// <summary>
		/// Initialize the members, LDAP configuration from app.config
		/// </summary>
		private void Initialize()
		{
            //read settings from config file
            mLdapServerName                 = ConfigurationManager.AppSettings["LdapSearch.ServerName"];
            mLdapServerPort                 = Convert.ToInt32(ConfigurationManager.AppSettings["LdapSearch.ServerPort"]);
            mLdapUseSSL                     = Convert.ToBoolean(ConfigurationManager.AppSettings["LdapSearch.SSL"]);
            mLdapConnAuthType               = Convert.ToInt32(ConfigurationManager.AppSettings["LdapSearch.ConnAuthType"]);
            mLdapLdapProtocolVersion        = Convert.ToInt32(ConfigurationManager.AppSettings["LdapSearch.LdapProtocolVersion"]);
            mLdapInvalidCredentialsString   = ConfigurationManager.AppSettings["LdapSearch.InvalidCredentialsString"];
            mLdapBindUserPrefix             = ConfigurationManager.AppSettings["LdapSearch.BindUserPrefix"];
            mLdapBindUserBaseDn             = ConfigurationManager.AppSettings["LdapSearch.BindUserBaseDn"];
            mLdapSearchDomain               = ConfigurationManager.AppSettings["LdapSearch.SearchDomain"];


            mLog.Debug("Initializing LDAP Search....");
            mLog.Debug("LdapSearch.ServerName" + "\t" + mLdapServerName);
            mLog.Debug("LdapSearch.ServerPort" + "\t" + mLdapServerPort);          
            mLog.Debug("LdapSearch.SearchDomain" + "\t" + mLdapSearchDomain);
		}

		#endregion Inititialization	

		/// <summary>
        /// Bind/Connect to LDAP-Server with System.DirectoryServices.Protocols
		/// </summary>
		public bool LdapBind(string pUserName, string pUserPwd)
		{
			bool bindOk = false;
            
			string bindUserDn = String.Empty;

            if (0 < mLdapBindUserPrefix.Length)
            {
                bindUserDn = mLdapBindUserPrefix + "=" + pUserName + "," + mLdapBindUserBaseDn;
            }
            else
            {
                bindUserDn = pUserName;
            }

            try
            {
                mLog.Debug(String.Format("About to contact ADS server {0} on port {1}...", mLdapServerName, mLdapServerPort));
                LdapDirectoryIdentifier ldapIdentifier = new LdapDirectoryIdentifier(mLdapServerName, mLdapServerPort);

                mLdapConn = new LdapConnection(ldapIdentifier);
                mLdapConn.Credential = new System.Net.NetworkCredential(bindUserDn, pUserPwd);

                // 0 - Anonymous: Gibt an, dass die Verbindung ohne eine Übergabe von Anmeldeinformationen hergestellt werden soll.Der Wert ist gleich 0.
                // 1 - Basic: Gibt an, dass die Standardauthentifizierung für die Verbindung verwendet werden soll.Der Wert ist gleich 1.
                // 2 - Negotiate: Gibt an, dass die Negotiate-Authentifizierung von Microsoft für die Verbindung verwendet werden soll.Der Wert ist gleich 2.
                // 3 - Ntlm: Gibt an, dass die Windows NT-Abfrage/Rückmeldung-Authentifizierung (NTML - Windows NT Challenge/Response) für die Verbindung verwendet werden soll.Der Wert ist gleich 3.
                // 4 - Digest: Gibt an, dass die Digest Access-Authentifizierung für die Verbindung verwendet werden soll.Der Wert ist gleich 4.
                // 5 - Sicily: Gibt an, dass ein Aushandlungsmechanismus (Sicily) verwendet wird, um MSN, DPA oder NTLM auszuwählen.Er sollte nur für LDAPv2-Server verwendet werden.Der Wert ist gleich 5.
                // 6 - Dpa: Gibt an, dass die verteilte Kennwortauthentifizierung (DPA - Distributed Password Authentication) für die Verbindung verwendet werden soll.Der Wert ist gleich 6.
                // 7 - Msn: Gibt die Authentifizierung durch den Microsoft-Netzwerkauthentifizierungsdienst an.Der Wert ist gleich 7.
                // 8 - External: Gibt an, dass eine externe Methode zur Authentifizierung der Verbindung verwendet wird.Der Wert ist gleich 8.
                // 9 - Kerberos: Gibt an, dass die Kerberos-Authentifizierung für die Verbindung verwendet werden soll.Der Wert ist gleich 9.
            
                mLdapConn.AuthType = (AuthType) mLdapConnAuthType;

                //  mLdapConn.SessionOptions.ProtocolVersion = 3; // BCS
                mLdapConn.SessionOptions.ProtocolVersion = mLdapLdapProtocolVersion; // 

                if (mLdapUseSSL)
                {
                    mLdapConn.SessionOptions.VerifyServerCertificate = new VerifyServerCertificateCallback((con, cer) => true);
                    mLdapConn.SessionOptions.SecureSocketLayer = true;
                }

                // Connect to Ldap Server
                mLdapConn.Bind();

                bindOk = true;
            }
            catch (System.Runtime.InteropServices.COMException comEx)
            {
                bindOk = false;
                ErrorMessage = comEx.Message;
                mLog.Error("A COM error occurred: " + ErrorMessage);
            }
            catch (LdapException lex)
            {
                
                bindOk = false;
                ErrorMessage = lex.Message;

                // check if credentials are invalid
                if (lex.Message.Contains("credentials") || lex.Message.Contains("Anmeldeinformationen"))
                {
                    ErrorMessage = "Anmeldung fehlgeschlagen!";
                }

                mLog.Error("An LDAP error occurred: " + lex.Message + lex.StackTrace);
            }
            catch (Exception ex)
            {
                bindOk = false;
                ErrorMessage = ex.Message;
                mLog.Error("A system error occurred: " + ex.Message + ex.StackTrace);
            }
            
			this.LdapDisconnect();        
			return bindOk;	
		}


		/// <summary>
		/// Disconnect from LDAP server.
		/// </summary>
		public void LdapDisconnect()
		{
			try
			{
                if (null != mLdapConn)
                {
                    mLdapConn.Dispose();
                }
			}
			catch (Exception ex)
			{
				mInfoMessage += "Error in LdapDisconnect: " + Environment.NewLine;
                mInfoMessage += ex.Message + Environment.NewLine;  

                ErrorMessage = ex.Message;
			}

		}

        /// <summary>
        /// Search for Users whose name or account name contains a given search string.
        /// </summary>
        /// <param name="pSearchTerm">the string to search for</param>
        /// <returns>a list of the found users</returns>
        public List<ActiveDirectoryObject> SearchAll(string pSearchTerm, string pSearchType)
        {
            DirectoryEntry searchRoot = new DirectoryEntry(mLdapSearchDomain);
            DirectorySearcher ds = new DirectorySearcher(searchRoot);
            SearchResultCollection sr;

            mLog.Info("About to execute ADS search ....");

            ds.Sort = new SortOption("givenName", SortDirection.Ascending);
            ds.SearchScope = System.DirectoryServices.SearchScope.Subtree;
            ds.PageSize = 500;

        
            // Current search is for WindowsId or UserId
            // Note "*" character added to search string
            ds.Filter = "(&(objectCategory=person)(objectClass=user) (|(sAMAccountName={0}*)(sn={0}*)(givenName={0}*)))".Replace("{0}", pSearchTerm);
            mLog.Info("Current ADS search Filter: " + ds.Filter);

            sr = ds.FindAll();
            mLog.Info("The search returned " + sr.Count + " records.");
          

            //cycle through results and collect users caught by the search string
            List<ActiveDirectoryObject> foundUsers = new List<ActiveDirectoryObject>();
            foreach (SearchResult rs in sr) 
            {
                mLog.Debug("Creating DirectoryEntry...");

                mDirectoryEntry = rs.GetDirectoryEntry();
                SetDirectoryObject();

                foundUsers.Add(mADSObject);
            }
            return foundUsers;
        }


        /// <summary>
        /// Executes an ADS search where one single record is expected as the search result.
        /// </summary>
        /// <param name="pSearchTerm">the string to search for</param>
        /// <returns>One ActiveDirectoryObject</returns>
        public ActiveDirectoryObject SearchOne(string pSearchTerm)
        {
            DirectoryEntry searchRoot = new DirectoryEntry(mLdapSearchDomain);
            DirectorySearcher ds = new DirectorySearcher(searchRoot);
         
            mLog.Info("About to execute ADS search ....");

            // Search for one WindowsId or UserId
            ds.Filter = "(&(objectCategory=person)(objectClass=user) (|(sAMAccountName={0})(sn={0})))".Replace("{0}", pSearchTerm);
            mLog.Info("Current ADS search Filter: " + ds.Filter);

            var sr = ds.FindOne();
           
            if (sr != null)
            {
                mLog.Debug("Creating DirectoryEntry...");

                mDirectoryEntry = sr.GetDirectoryEntry();                
                SetDirectoryObject();
            
                return mADSObject;
            }
            return null;
        }

        /// <summary>
        /// Sets DirectoryObject properties
        /// </summary>
        private void SetDirectoryObject()
        {
            object propertyToCheck = null;
            mADSObject = new ActiveDirectoryObject();


            if (mDirectoryEntry.Properties["givenName"] != null)
            {
                propertyToCheck = mDirectoryEntry.Properties["givenName"].Value;
                mADSObject.CommonName = propertyToCheck.ToString();
            }
            if (mDirectoryEntry.Properties["distinguishedName"] != null)
            {
                propertyToCheck = mDirectoryEntry.Properties["distinguishedName"].Value;
                mADSObject.DinstinguishName = propertyToCheck.ToString();
            }
            if (mDirectoryEntry.Properties["sn"] != null)
            {
                propertyToCheck = mDirectoryEntry.Properties["sn"].Value;
                mADSObject.Surname = propertyToCheck.ToString();
            }
            if (mDirectoryEntry.Properties["sAMAccountName"] != null)
            {
                propertyToCheck = mDirectoryEntry.Properties["sAMAccountName"].Value;
                mADSObject.SamAccountName = propertyToCheck.ToString();
            }
            if (mDirectoryEntry.Properties["telephoneNumber"] != null)
            {
                propertyToCheck = mDirectoryEntry.Properties["telephoneNumber"].Value;
                if (propertyToCheck != null) mADSObject.TelephoneNumber = propertyToCheck.ToString();
            }
            if (mDirectoryEntry.Properties["mail"] != null)
            {
                propertyToCheck = mDirectoryEntry.Properties["mail"].Value;
                if (propertyToCheck != null) mADSObject.EmailAddress = propertyToCheck.ToString();
            }

            mLog.Debug("DirectoryEntry has givenName..." + mDirectoryEntry.Properties["givenName"].Value);
            mLog.Debug("DirectoryEntry has distinguishedName.." + mDirectoryEntry.Properties["distinguishedName"].Value);
            mLog.Debug("DirectoryEntry has sAMAccountName.." + mDirectoryEntry.Properties["sAMAccountName"].Value);
        }

		#endregion Methods

	}
}

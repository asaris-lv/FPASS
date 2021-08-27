using System;
using System.Text.RegularExpressions;
using System.Collections;
using System.Configuration;
using System.DirectoryServices.Protocols;
using System.Collections.Generic;
using System.DirectoryServices;
using de.pta.Component.Logging.Log4NetWrapper;


namespace Degussa.FPASS.Util.ActiveDirectory
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
		#region Constants

        public const string SEARCH_WINDOWS_ID = "WINDOWS_ID";
        public const string SEARCH_NAME = "NAME";

		#endregion Constants
		
		#region Members


		private string mLdapServerName;
        private int    mLdapServerPort;
        private bool   mLdapUseSSL;
        private int    mLdapConnAuthType;
        private int    mLdapLdapProtocolVersion ;
        private string mLdapInvalidCredentialsString;
		private string mLdapBindUserPrefix;
		private string mLdapBindUserBaseDn;
        
        private string mLdapSearchDomain;
        private bool   mLdapUseSpecialUser;
        private string mLdapSpecialUserName;
        private string mLdapSpecialUserPassword;
        

		private string   mInfoMessage;
        public string ErrorMessage { get; private set; }

        private LdapConnection mLdapConn;

		#endregion Members

        #region Accessors


        #endregion

		#region Constructors

		/// <summary>
		/// Standard Constructor for LDAP search.
		/// </summary>
		public LdapSearch()
		{
			this.Initialize();
		} 

		#endregion Constructors

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

            //mLdapUseSpecialUser             = Convert.ToBoolean(ConfigurationManager.AppSettings["LdapSearch.UseSpecialUser"]);
            //mLdapSpecialUserName            = ConfigurationManager.AppSettings["LdapSearch.SpecialUserName"]; 
            //mLdapSpecialUserPassword        = ConfigurationManager.AppSettings["LdapSearch.SpecialUserPassword"]; 

            //Console.WriteLine("Initializing LDAP Search....");
            //Console.WriteLine("LdapSearch.ServerName"               + "\t"  + mLdapServerName              );
            //Console.WriteLine("LdapSearch.ServerPort"               + "\t"  + mLdapServerPort              );
            //Console.WriteLine("LdapSearch.SSL"                      + "\t"  + mLdapUseSSL                  );
            //Console.WriteLine("LdapSearch.ConnAuthType"             + "\t"  + mLdapConnAuthType            );
            //Console.WriteLine("LdapSearch.LdapProtocolVersion"      + "\t"  + mLdapLdapProtocolVersion     );
            //Console.WriteLine("LdapSearch.InvalidCredentialsString" + "\t"  + mLdapInvalidCredentialsString);
            //Console.WriteLine("LdapSearch.BindUserPrefix"           + "\t"  + mLdapBindUserPrefix          );
            //Console.WriteLine("LdapSearch.BindUserBaseDn"           + "\t"  + mLdapBindUserBaseDn          );
            //Console.WriteLine("LdapSearch.SearchDomain"             + "\t"  + mLdapSearchDomain            );
		}

		#endregion Inititialization

		#region Methods


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

            //mInfoMessage = "Start System.DirectoryServices.Protocols" + Environment.NewLine;
            //mInfoMessage += "Connect Start to " + mLdapServerName + ":" + mLdapServerPort + " Use SSL = " + mLdapUseSSL.ToString() + Environment.NewLine;
            //mInfoMessage += " bindUserDn: " + bindUserDn + Environment.NewLine;

            try
            {
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

                //mInfoMessage += " ConnectionURL: " + mLdapServerName + ":" + mLdapServerPort + " Use SSL = " + mLdapUseSSL.ToString() + " " + Environment.NewLine;

                // Connect to Ldap Server
                mLdapConn.Bind();

                bindOk = true;
            }
            catch (System.Runtime.InteropServices.COMException comEx)
            {
                bindOk = false;
                ErrorMessage = comEx.Message;
              
            }
            catch (System.DirectoryServices.Protocols.LdapException dsLdapEx)
            {
                
                bindOk = false;
                ErrorMessage = dsLdapEx.Message;

                // check if credentials are invalid
                if (dsLdapEx.Message.Contains("credentials") || dsLdapEx.Message.Contains("Anmeldeinformationen"))
                {
                    ErrorMessage = "Anmeldung fehlgeschlagen!";
                }
               
            }
            catch (Exception ex)
            {
                bindOk = false;
                ErrorMessage = ex.Message;              
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
        public List<ActiveDirectoryObject> Search(string pSearchTerm, string pSearchType)
        {
            DirectoryEntry searchRoot = new DirectoryEntry(mLdapSearchDomain);
            DirectorySearcher ds = new DirectorySearcher(searchRoot);

            Logger log = Globals.GetInstance().Log;
            log.Warn("Führe gleich eine Suche im ADS aus.");

            //if (pSearchType == SEARCH_WINDOWS_ID)
            //{           
            //    ds.Filter = "(&(objectCategory=person)(objectClass=user)(sn={0}*))".Replace("{0}", pSearchTerm);

            //    log.Warn("Suche nach KONZERNID");
            //    log.Warn("Aktueller Filter für die Suche: " + ds.Filter);
            //}
            //else
            //{
            //    ds.Filter = "(&(objectCategory=person)(objectClass=user) (|(sAMAccountName={0}*)(sn={0}*)(givenName={0}*)))".Replace("{0}", pSearchTerm);

            //    log.Warn("Suche nach NAMEN");
            //    log.Warn("Aktueller Filter für die Suche: " + ds.Filter);
            //}
            
            // TODO: current problem at Evonik, which filter works?
            ds.Filter = "(&(objectCategory=person)(objectClass=user) (|(sAMAccountName={0}*)(sn={0}*)(givenName={0}*)))".Replace("{0}", pSearchTerm);
            log.Warn("Aktueller Filter für die Suche: " + ds.Filter);

            ds.Sort = new SortOption("givenName", SortDirection.Ascending);
            ds.SearchScope = System.DirectoryServices.SearchScope.Subtree;
            ds.PageSize = 500;

            SearchResultCollection sr = ds.FindAll();

            log.Warn("Die Suche hat " + sr.Count + " Treffer gefunden.");

            //cycle through results and collect users caught by the search string
            List<ActiveDirectoryObject> foundUsers = new List<ActiveDirectoryObject>();
            foreach (SearchResult rs in sr) 
            {               
                string commonName = "";
                string displayName = "";
                string surName = "";
                string sAMAccountName = "";
                string distinguishedName = "";
                string telephoneNumber = "";


                log.Warn("Erstelle DirectoryEntry...");

                DirectoryEntry de = rs.GetDirectoryEntry();
                object propertyToCheck = null;

                propertyToCheck = de.Properties["givenName"].Value;
                if (propertyToCheck != null) commonName = propertyToCheck.ToString();
                propertyToCheck = de.Properties["displayName"].Value;
                if (propertyToCheck != null) displayName = propertyToCheck.ToString();
                propertyToCheck = de.Properties["sn"].Value;
                if (propertyToCheck != null) surName = propertyToCheck.ToString();
                propertyToCheck = de.Properties["sAMAccountName"].Value;
                if (propertyToCheck != null) sAMAccountName = propertyToCheck.ToString();
                propertyToCheck = de.Properties["distinguishedName"].Value;
                if (propertyToCheck != null) distinguishedName = propertyToCheck.ToString();
                propertyToCheck = de.Properties["telephoneNumber"].Value;
                if (propertyToCheck != null) telephoneNumber = propertyToCheck.ToString();


                log.Warn("DirectoryEntry hat givenName..." + de.Properties["givenName"].Value);
                log.Warn("DirectoryEntry hat distinguishedName.." + de.Properties["distinguishedName"].Value);
                log.Warn("DirectoryEntry hat sAMAccountName.." + de.Properties["sAMAccountName"].Value);

                ActiveDirectoryObject ado = new ActiveDirectoryObject(
                                                displayName,
                                                sAMAccountName,
                                                commonName,
                                                surName,
                                                distinguishedName,
                                                telephoneNumber);
                foundUsers.Add(ado);
            }

            return foundUsers;
        }

		#endregion Methods

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

	}
}

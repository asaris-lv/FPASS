using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using System.Configuration;
using de.pta.Component.Errorhandling;
using Degussa.FPASS.Util.Messages;
using Evonik.FPASSLdap;

namespace Degussa.FPASS.Gui.Dialog.ActiveDirectory
{
    public partial class FrmSearchLDAPWin : Form
    {
        #region Members

        private LdapSearch mLdapSearch;
        private ActiveDirectoryObject mADSObject;
        

        private string mUserPassword = "";
        private string mUserName = "";
        private bool mCredentialsNeeded { get; set; }
        private bool mUserAuthorized { get; set; }

        internal bool OKPressed { get; private set; }

        internal ActiveDirectoryObject SelectedUser { get { return mADSObject; } }
  

        #endregion

        public FrmSearchLDAPWin()
        {
            InitializeComponent();
            
            //read data from config file
            BtnOK.Enabled = false;
            mCredentialsNeeded = Convert.ToBoolean(ConfigurationManager.AppSettings["LdapSearchDialog.CredentialsNeeded"]);
            
            //initialize objects
            mLdapSearch = new LdapSearch();
        }

     
        #region Methods

        internal void SetSearchUser(string pUserName)
        {
            txtSearchTerm.Text = pUserName;
        }

        /// <summary>
        /// Executes LDAP search
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchLDAP()
        {
            mUserAuthorized = false;

            // Check to see if user is authorised to access ADS server.
            if ((mUserPassword == "" || mUserName == "" || mUserAuthorized == false) && mCredentialsNeeded)
            {
                WindowsPasswordDialog pwd = new WindowsPasswordDialog();
                pwd.ShowDialog();

                if (pwd.OKPressed)
                {
                    //authorize
                    mUserPassword = pwd.Password;
                    mUserName = Environment.UserName;
                    mLdapSearch.LdapBind(mUserName, mUserPassword);

                    //are there errors?
                    if (mLdapSearch.ErrorMessage != "")
                    {
                        MessageBox.Show(mLdapSearch.ErrorMessage);
                        return;
                    }
                    mUserAuthorized = true;
                }
                else { MessageBox.Show(MessageSingleton.GetInstance().GetMessage(MessageSingleton.ADS_ERR_NEED_PWD), "FPASS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            }
            else
            {
                mUserAuthorized = true;
            }

            // Execute ADS search
            List<ActiveDirectoryObject> results;

            if (txtSearchTerm.Text != "" && mUserAuthorized)
            {
                Cursor.Current = Cursors.WaitCursor;

                string searchType = LdapSearch.SEARCH_WINDOWS_ID;

                // Do LDAP bind
                mLdapSearch.LdapBind(mUserName, mUserPassword);

                // Execute LDAP search and get results
                results = mLdapSearch.SearchAll(txtSearchTerm.Text, searchType);

                liSearchResults.DataSource = results;
                liSearchResults.DisplayMember = "ConcatinatedName";
                Cursor.Current = Cursors.Default;

                if (results.Count == 0)
                {
                    BtnOK.Enabled = false;
                    throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.ADS_WARN_NO_USER));                    
                }
                else
                {
                    BtnOK.Enabled = true;
                }
            }
            else
            {
                throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.ADS_ERR_EMPTY_FIELD));
            }
        }
    
        #endregion

        #region Events

        /// <summary>
        /// Executes LDAP search
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                SearchLDAP();
            }
            catch (UIWarningException uwe)
            {
                ExceptionProcessor.GetInstance().Process(uwe);
            }
            catch (Exception gex)
            {
                // All other errors caught here: do not end FPASS
                ExceptionProcessor.GetInstance().Process(new UIErrorException(gex.Message, gex));
            }
        }

        /// <summary>
        /// Hands results back to calling dialogue and closes this one
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOK_Click(object sender, EventArgs e)
        {
            mADSObject = (ActiveDirectoryObject)liSearchResults.SelectedItem;
            this.Close();
        }

        private void LdapUserSearchDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            mLdapSearch.LdapDisconnect();
        }


        private void BtnCancel_Click(object sender, EventArgs e)
        {
            mADSObject = null;
            this.Close();
        }

        private void lb_SearchResults_DoubleClick(object sender, EventArgs e)
        {
            mADSObject = (ActiveDirectoryObject)liSearchResults.SelectedItem;
            this.Close();
        }

        #endregion
    }
}

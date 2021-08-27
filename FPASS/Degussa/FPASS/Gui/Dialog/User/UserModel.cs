using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

using Degussa.FPASS.Bo.Administration;
using Degussa.FPASS.Db;
using Degussa.FPASS.Db.DataSets;
using Degussa.FPASS.Gui;
using Degussa.FPASS.FPASSApplication;
using Degussa.FPASS.Util;
using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.Util.UserManagement;

using de.pta.Component.DbAccess;
using de.pta.Component.ListOfValues;
using de.pta.Component.DbAccess.Exceptions;
using de.pta.Component.Errorhandling;

namespace Degussa.FPASS.Gui.Dialog.User
{
	/// <summary>
	/// Summary description for UserModel.
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
	public class UserModel : FPASSBaseModel
	{
		#region Members

		// Name of SQL command to fill BO
		private const string MANAGEUSER_QUERY		= "SelectUserByRole";
		private const string ASSIGNEDPLANTS_QUERY	= "SelectPlantByUser";

		private const string INSERT_UM_USERS		= "InsertManageUMUsers";
		private const string INSERT_FPASS_USERS     = "InsertManageFPASSUsers";
		private const string ASSIGNENEWUSERMAND     = "InsertUserToMandant";
        private const string INSERT_USER_PAR_TABLE  = "InsertUserParameterTable";
		private const string ASSIGNNEWPLANTS        = "AssignNewPlants";
		private const string ADDROLEPLANTMANAGER    = "AddRolePlantManager";
		private const string UPDATE_UM_USERS		= "UpdateManageUMUsers";
		private const string UPDATE_FPASS_USERS		= "UpdateManageFPASSUsers";
		private const string DELETE_UM_USERS		= "DeleteManageUMUsers";
		private const string DELETE_FPASS_USERS     = "DeleteManageFPASSUsers";
		private const string DELETEUSERPLANTS		= "DeleteUserPlants";
		private const string SP_ARCH_ROLELINK		= "SP_archivcheckrl";
			
		// Search parameters
		private const string USER_MAND_PARA			     = ":MND_ID";
		private const string MANAGEUSER_USERSURNAME_PARA = ":UM_USERSURNAME";
		private const string MANAGEUSER_FIRSTNAME_PARA	 = ":UM_FIRSTNAME";
		private const string MANAGEUSER_USERAPPLID_PARA	 = ":UM_USERAPPLID"; 
		private const string ASSIGNPLANT_USER_PARA       = ":USPL_USER_ID";
		
		// Primary key values used for insert		
		private decimal mUMUserPKVal		   = 0;
		private decimal mFPASSUserPKVal		   = 0;
		private decimal mUserToPlantPKVal	   = 0;
		private decimal mUserToPlantFKUserVal  = 0;
		private decimal mUserToPlantFKPlantVal = 0;
		private decimal mCurrentCoordID		   = 0;
		int ret;

		// Values for Inserts/updates
		private const string  US_PASSWORD_VAL   = "FPASS";  
		private const string  US_FIRSTLOGIN_VAL = "1";
		private const int     US_DELETED_FALSE	= 0; 
		private const int     US_DELETED_TRUE	= 1;	

		// Check User Login ID unique
		private const string  USER_UNIQUE_QUERY      = "CheckUserApplIDUnique";
		private const string  USER_UNIQUE_MND_PARA   = ":USM_MND_ID";
		private const string  USER_UNIQUE_APPID_PARA = ":USERWITHDOMAIN";
	
		// DataProvider from PTA DbAccess component
		private IProvider mProvider;

		/// <summary>
		/// Plants
		/// </summary>
		private string mStrPlantMaster    = Globals.DB_NO;
		private bool   mIsAlreadyPlMaster = false;

		// insert & update values
		private string mNewSurname;
		private string mNewFirstName;
		private	string mNewTEL;
		private	string mNewUserID;
		private	string mNewDomainName;
		private string mNewDeptName;
		private int    mNewDeptID;
		private int    mNewDomainID;
	
		// Used for loading & displaying
		private BOUser    mBOUser       = null;
		private Hashtable mHttUSERShow;
		private ArrayList mArlUsersShow;
		private ArrayList arlLOVPlants;
		private ArrayList arlAssignedPlants;
		private ArrayList arlPlantsNewUser;
	
		private Hashtable mHttLOVPlants;
		private Hashtable mHttUSERShowToPlantPKs;
		// if user to be deleted is coordinator
		private Hashtable httAlternativeCoordsByEXCO;

		/// <summary>
		/// Search parameters: surname, firstname, loginID
		/// Department new 12.12.03
		/// </summary>
		private string mParameterUserSurname   = String.Empty;
		private string mParameterUserFirstName = String.Empty;
		private string mParameterUserApplID	   = String.Empty;
		private string mParameterUserDept	   = String.Empty;
		private string mSearchUserSqlWhere	   = String.Empty;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public UserModel()
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
			// Instantiate DbAccess
			mProvider = DBSingleton.GetInstance().DataProvider;
		}	


		#endregion //End of Initialization

		#region Accessors 

		public BOUser CurrentBOUser
		{		
			get 
			{
				return mBOUser;
			}
			set 
			{
				mBOUser = value;
			}		
		}	

		#endregion //End of Accessors

		#region Methods 

		#region General

		/// <summary>
		/// After the form for the assignment of roles FrmRoleToUser is closed, FrmUser gets focus
		/// Re-excute search using current search parameters
		/// </summary>
		internal override void PreShow()
		{
			SetCurrentUserIDToDefault();			
			if ( null != mBOUser )
			{
				try
				{					
					ClearTextFields();
					GetUsers(false);
				}
				catch (UIWarningException uwe)
				{
					ExceptionProcessor.GetInstance().Process(uwe);
				}
			}
			else
			{
				ClearTextFields();
			}
		}


		/// <summary>
		/// Refresh the datagrid after a new user is saved or changes to an existing user
		/// For existing user currently no refresh (code out 15.12.03)
		/// For new user create BO with new details and create datatable to display in grid
		/// Without rereading the user details from the DB no easy way to update the assigned roles (eg Plant Manager, Coordinator, .. removed) 
		/// For each user the assigned plants must be loaded, those which the user is manager of appear ticked
		/// </summary>
		internal void RefreshBODisplay()
		{			
			// If this was an update carry out a new search to reload new details: set DB search parameters
			if ( mBOUser != null )
			{							
				/// 12.12.03: datagrid not auomatically refreshed now that BOs appear in datatable:
				/// Information is correct in database, update this BO in code
				if ( null !=mNewSurname && null != mNewFirstName && null != mNewUserID )
				{
					mBOUser.ApplUserID    = mNewUserID;
					mBOUser.Surname		  = mNewSurname;
					mBOUser.Firstname     = mNewFirstName;
					mBOUser.Telephone			  = mNewTEL;
					mBOUser.UserFormattedName = mBOUser.Surname
						+ ", "
						+ mNewFirstName
						+ " ("
						+ mBOUser.Telephone	
						+ ")";
					mBOUser.DeptName	  = mNewDeptName;
				}
				//				
				// out 15.12.03: no search
				//				mParameterUserApplID    = mBOUser.UserApplUserID + '%';
				//				mParameterUserSurname	= mBOUser.UserSurname	 + '%';
				//			    mParameterUserFirstName = mBOUser.UserFirstName  + '%';
				//				this.GetUsers(false);
				//
				//				// So that current user stays in scope
				//				((FrmUser) mView).CurrentFPASSUserID = mFPASSUserPKVal;
				//				((FrmUser) mView).CurrentUMUserID    = mUMUserPKVal;
				//				
				//				// Show correct vals in combobox Dept and Domaine
				//				LovItem itemDept    = this.GetLovItem(mBOUser.UserDeptID, ((FrmUser) mView).CboEditDepartment);
				//				LovItem itemDomaine = this.GetLovItem(mBOUser.UserDomaineID, ((FrmUser) mView).CboEditDomain);		
				//				((FrmUser) mView).CboEditDepartment.SelectedItem = itemDept;
				//				((FrmUser) mView).CboEditDomain.SelectedItem     = itemDomaine;
				// END out 15.12.03
			}
			else
			{
				// For new user, create BO with new details and display in grid
				this.mBOUser			  = new BOUser();
				mBOUser.FPASSPKIdentifier = mFPASSUserPKVal;
				mBOUser.UMPKIdentifier    = mUMUserPKVal;
				mBOUser.Surname       = mNewSurname;
				mBOUser.Firstname	  = mNewFirstName;
				mBOUser.Telephone			  = mNewTEL;
				mBOUser.UserFormattedName = mNewSurname 
					+ ", " 
					+ mNewFirstName 
					+ " (" 
					+ mNewTEL 
					+ ")";
				mBOUser.ApplUserID    = mNewUserID;
				mBOUser.DeptID		  = mNewDeptID;
				mBOUser.DeptName	  = mNewDeptName;
				mBOUser.DomainID	  = mNewDomainID;
				
				if ( this.mStrPlantMaster.Equals(Globals.DB_YES) )
				{
					mBOUser.IsPlantManager   = true;
                    mBOUser.RoleNameAssigned = UserManagementControl.ROLE_BETRIEBSMEISTER;
				}
				else
				{
					mBOUser.IsPlantManager   = false;
					mBOUser.RoleNameAssigned = String.Empty;
				}

				mArlUsersShow = new ArrayList();
				mHttUSERShow  = new Hashtable();
				mArlUsersShow.Add(mBOUser);
				mHttUSERShow.Add(mFPASSUserPKVal, mBOUser);
				
				// Set current ID in form so that roles can be directly assigned
				DisplayUsersInDataTable();
				((FrmUser)mView).CurrentUMUserID    = mUMUserPKVal;
				((FrmUser)mView).CurrentFPASSUserID = mFPASSUserPKVal;


				mParameterUserApplID    = mBOUser.ApplUserID + '%';
				mParameterUserSurname	= mBOUser.Surname	 + '%';
				mParameterUserFirstName = mBOUser.Firstname  + '%';
				mParameterUserDept      = mBOUser.DeptID.ToString();
			}
		}


		/// <summary>
		/// Clears contents of form: textfields, comboboxes and datagrids
		/// Need to assign dummy datasource to datagrid as not properly emptied (Bug .NET?)
		/// </summary>
		internal void ClearTextFields()
		{			
			
			this.ClearStatusBar();
			((FrmUser) mView).TxtEditSurname.DataBindings.Clear();
			((FrmUser) mView).TxtEditFirstname.DataBindings.Clear();
			((FrmUser) mView).TxtEditUser.DataBindings.Clear();
			((FrmUser) mView).TxtEditPhone.DataBindings.Clear();
			((FrmUser) mView).TxtEditSurname.Text   = "";
			((FrmUser) mView).TxtEditFirstname.Text = "";
			((FrmUser) mView).TxtEditUser.Text  = "";
			((FrmUser) mView).TxtEditPhone.Text = "";
			((FrmUser) mView).CboEditDepartment.Text = "";
			((FrmUser) mView).CboEditDomain.Text = "";
			((FrmUser) mView).CboCoCraft.SelectedItem = "";
			((FrmUser) mView).TxtEditSurname.Enabled = false;
			((FrmUser) mView).TxtEditFirstname.Enabled = false;
			((FrmUser) mView).TxtEditUser.Enabled = false;
			((FrmUser) mView).TxtEditPhone.Enabled = false;
			((FrmUser) mView).CboEditDepartment.Enabled = false;
			((FrmUser) mView).CboEditDomain.Enabled = false;
			((FrmUser) mView).CboCoCraft.Enabled = false;
			((FrmUser) mView).LikEditPlant.Items.Clear();			
			((FrmUser) mView).LikEditPlant.Enabled = false;
			
			((FrmUser) mView).DgrUser.DataSource = null;
			((FrmUser) mView).DgrUser.DataBindings.Clear();

			((FrmUser) mView).ContentChanged = false;

			this.mBOUser = null;
			this.mArlUsersShow = null;
			this.mHttLOVPlants = null;
			this.mHttUSERShow = null;		
		}

		/// <summary>
		/// Empty the fields at top of Form (responsible for Search)
		/// </summary>
		internal void CancelSearchFields()
		{	
			((FrmUser) mView).TxtSearchFirstname.Text			= String.Empty;
			((FrmUser) mView).TxtSearchSurname.Text				= String.Empty;
			((FrmUser) mView).TxtSearchUser.Text				= String.Empty;
			((FrmUser) mView).CboSearchDepartment.SelectedIndex = 0;			
		}

		internal void SetCurrentUserIDToDefault()
		{
			((FrmUser) mView).DgrUser.DataSource = null;
			((FrmUser) mView).CurrentUMUserID = -1;
			((FrmUser) mView).CurrentFPASSUserID = -1;
		}

	
		private void GetLOVPlants()
		{
			// populate Arrylist of available plants
			arlLOVPlants = new ArrayList();
			arlLOVPlants = LovSingleton.GetInstance().GetRootList(null, "FPASS_PLANT_FPASS", "PL_NAME");
			arlLOVPlants.Reverse();		

         //   arlLOVPlants = LovSingleton.GetInstance().SetSqlRestriction

			// Use string key to get ID later
			mHttLOVPlants = new Hashtable();
			foreach (LovItem item in arlLOVPlants)
			{
				mHttLOVPlants.Add(item.ItemValue, item.Id);
			}
		}

		/// <summary>
		/// Gets values of textfields and cbos from GUI
		/// Make sure compulsory fields contain a value
		/// throw UIWarningException if this is not the case
		/// </summary>
		private void CheckTextFields()
		{
			mNewSurname	  = ((FrmUser) mView).TxtEditSurname.Text;
			mNewFirstName = ((FrmUser) mView).TxtEditFirstname.Text;
			mNewUserID	  = ((FrmUser) mView).TxtEditUser.Text;
			mNewTEL		  = ((FrmUser) mView).TxtEditPhone.Text;

			if (mNewSurname.Length < 1 || mNewFirstName.Length < 1 || mNewUserID.Length < 1 || mNewTEL.Length <1)
			{
				((FrmUser) mView).Cursor = System.Windows.Forms.Cursors.Default;
				throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(
					MessageSingleton.NO_ADMIN_TO_SAVE ));
			}

			// Get vals from comboboxes
			string strNewDeptID     = this.GetSelectedIDFromCbo(((FrmUser)mView).CboEditDepartment);
			mNewDeptName			= this.GetSelectedValueFromCbo(((FrmUser)mView).CboEditDepartment);
			string strNewDomainID   = this.GetSelectedIDFromCbo(((FrmUser)mView).CboEditDomain);
			mNewDomainName			= this.GetSelectedValueFromCbo(((FrmUser)mView).CboEditDomain);

			// If nothing selected, throw warning
			if (strNewDeptID.Length < 1 || strNewDomainID.Length < 1)
			{
				((FrmUser) mView).Cursor = System.Windows.Forms.Cursors.Default;
				throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(
					MessageSingleton.NO_ADMIN_TO_SAVE ));
			}
			else
			{
				try
				{
					// If cannot be converted to integer then assume a data error => fatal
					mNewDeptID   = Convert.ToInt32(strNewDeptID);
					mNewDomainID = Convert.ToInt32(strNewDomainID);	
				}
				catch ( Exception )
				{
					throw new UIFatalException(MessageSingleton.GetInstance().GetMessage(
						MessageSingleton.FATAL_DB_ERROR ));            
				}
			}
		}

		internal void NewPlantItemAdded()
		{						
			((FrmUser)mView).CheckListBoxChanged = true;
		}
	

		#endregion

		#region GetUsers

		/// <summary>
		/// Get Users from database
		/// The results of the User search are loaded into an arraylist of BOs
		/// This arraylist is then filtered into a hashtable of unique users plus highest role (31.03:using PK identifier )
		/// An arraylist of unique users is then bound to the datagrid in the GUI.
		/// </summary>
		internal void GetUsers(bool pSetSearch)
		{
			ArrayList arlAllUsersFromDB;
			decimal   currUMUniqueIdentifier;
			string    currRoleNameAssigned;

			((FrmUser) mView).DgrUser.DataBindings.Clear();

			// Set current user to null
			if ( mBOUser != null)
			{
				mBOUser = null;
			}
			((FrmUser) mView).CurrentFPASSUserID = -1;
			((FrmUser) mView).CurrentUMUserID    = -1;

			// read the search criteria from the gui
			if ( pSetSearch )
			{
				this.GetSQLSearchCriteria();
			}
			
			// Execute SELECT
			arlAllUsersFromDB = SelectAllUsersFromDB();

			// Compare the assigned roles: if 2 entries occur for the same user but with different roles, 
			// show the highest role only. Verwalter > Koordinator > others
			// Hashtable of UserBOs used later to identify users
	
			mArlUsersShow = new ArrayList();
			mHttUSERShow  = new Hashtable();
			Hashtable	uniqueUsers = new Hashtable();
			
			if ( arlAllUsersFromDB.Count > 0 ) 
			{
				foreach (BOUser outerBO in arlAllUsersFromDB)
				{
					currUMUniqueIdentifier = outerBO.UMPKIdentifier;
					if ( uniqueUsers[currUMUniqueIdentifier] == null  )  
					{
						uniqueUsers.Add(outerBO.UMPKIdentifier, outerBO);
					}
				}
				foreach (BOUser uniqueBO in uniqueUsers.Values)
				{
					currUMUniqueIdentifier = uniqueBO.UMPKIdentifier;
					currRoleNameAssigned   = uniqueBO.RoleNameAssigned;

					foreach (BOUser innerBO in arlAllUsersFromDB)
					{
						if ( innerBO.UMPKIdentifier == currUMUniqueIdentifier && innerBO.RoleNameAssigned.Equals(UserManagementControl.ROLE_KOORDINATOR) ) 
						{
							uniqueBO.RoleNameAssigned = UserManagementControl.ROLE_KOORDINATOR;
							uniqueBO.IsCoordinator    = true;
						}
					}
				}
				foreach (BOUser uniqueBO in uniqueUsers.Values)
				{
					currUMUniqueIdentifier = uniqueBO.UMPKIdentifier;
					currRoleNameAssigned   = uniqueBO.RoleNameAssigned;

					foreach (BOUser innerBO in arlAllUsersFromDB)
					{
						if ( innerBO.UMPKIdentifier == currUMUniqueIdentifier && innerBO.RoleNameAssigned.Equals(UserManagementControl.ROLE_VERWALTUNG) )
						{
							uniqueBO.RoleNameAssigned = UserManagementControl.ROLE_VERWALTUNG;
						}
					}
				}
				// ArrayList with unique users and their highest role is shown
				mArlUsersShow = new ArrayList(uniqueUsers.Values);

				// Sort arraylist according to user name
				mArlUsersShow.Sort(new UserNameComparer());
						
				foreach (BOUser realBO in mArlUsersShow)
				{					
					mHttUSERShow.Add(realBO.FPASSPKIdentifier, realBO);	
				}
				
				// 12.12.03: bind datatble to grid to allow sorting
				DisplayUsersInDataTable();
			} 
			else 
			{
				((FrmUser) mView).DgrUser.DataSource = null;
				this.ShowMessageInStatusBar( MessageSingleton.GetInstance().GetMessage(MessageSingleton.NO_RESULTS));
			}
		}

		/// <summary>
		/// Create a datatable at runtime: this is bound to datagrid to allow sorting	
		/// Means that changes are not shown in grid straightaway
		/// </summary>
		private void DisplayUsersInDataTable()
		{	
			int     i = 0;
			DataRow row;
			DataTable table = new DataTable("RTTabUsers");
			table.Columns.Add(new DataColumn("FPASSPKIdentifier"));
			table.Columns.Add(new DataColumn("UMPKIdentifier"));
			table.Columns.Add(new DataColumn("ApplUserID"));
            table.Columns.Add(new DataColumn("DomainName"));
			table.Columns.Add(new DataColumn("UserFormattedName"));
			table.Columns.Add(new DataColumn("RoleNameAssigned"));
			table.Columns.Add(new DataColumn("DeptName"));
			
		
			foreach ( BOUser bo2 in mArlUsersShow )
			{
				row	= table.NewRow();
				row.ItemArray = new object[7] { bo2.FPASSPKIdentifier,
												bo2.UMPKIdentifier, 
												bo2.ApplUserID,
                                                bo2.DomainName,
												bo2.UserFormattedName,
												bo2.RoleNameAssigned,
												bo2.DeptName };

				table.Rows.Add(row);
				i ++;
			}
			this.ShowMessageInStatusBar( "Meldung: " + i + " Benutzer gefunden" );
			((FrmUser) mView).DgrUser.DataSource = table;
		}
	

		/// <summary>
		/// Execute SQL query to get users from database
		/// Map attributes from DB to UserBO attributes
		/// At this stage users appear more than once in results as one record for each assigned role 
		/// </summary>
		/// <returns>ArrayList containing all users</returns>
		private ArrayList SelectAllUsersFromDB()
		{
			ArrayList arlResults = new ArrayList();
			
			// Create select command & fill Data Reader, Parameters are always set
			IDbCommand selComm = mProvider.CreateCommand(MANAGEUSER_QUERY);
			mProvider.SetParameter(selComm, USER_MAND_PARA, UserManagementControl.getInstance().CurrentMandatorID.ToString());
			mProvider.SetParameter(selComm, MANAGEUSER_USERSURNAME_PARA, mParameterUserSurname.Trim().ToUpper());
			mProvider.SetParameter(selComm, MANAGEUSER_FIRSTNAME_PARA,   mParameterUserFirstName.Trim().ToUpper());
			mProvider.SetParameter(selComm, MANAGEUSER_USERAPPLID_PARA,  mParameterUserApplID.Trim().ToUpper());

			selComm.CommandText += mSearchUserSqlWhere;

			try
			{
				IDataReader drResults = mProvider.GetReader(selComm);
				while (drResults.Read())
				{
					mBOUser = new BOUser();

					mBOUser.UMPKIdentifier    = Convert.ToInt32(drResults["UM_USER_ID"]);
					mBOUser.FPASSPKIdentifier = Convert.ToInt32(drResults["FPASS_USER_ID"]);
					mBOUser.Surname		  = Convert.ToString(drResults["UM_USERSURNAME"]);
					mBOUser.Firstname     = Convert.ToString(drResults["UM_FIRSTNAME"]);
					mBOUser.Telephone			  = Convert.ToString(drResults["USER_TEL"]);
					mBOUser.UserFormattedName = Convert.ToString(drResults["BOTHNAMESTEL"]);
					mBOUser.ApplUserID    = Convert.ToString(drResults["UM_USERAPPLID"]);
					mBOUser.DeptID		  = Convert.ToInt32(drResults["FPASS_DEPT_ID"]);
					mBOUser.DeptName	  = Convert.ToString(drResults["DEPT_DEPARTMENT"]);
					mBOUser.DomainID     = Convert.ToInt32(drResults["FPASS_UDOM_ID"]);
                    mBOUser.DomainName = Convert.ToString(drResults["FPASS_UDOM_NAME"]);  
					mBOUser.RoleNameAssigned = Convert.ToString(drResults["RO_DESCRIPTION"]);

					if ( Convert.ToString(drResults["USER_PLANTMASTER_YN"]).Equals(Globals.DB_YES) )
					{
						mBOUser.IsPlantManager = true;
					}
					else
					{
						mBOUser.IsPlantManager = false;
					}

					// Need to know if current user is coordinator (for later)
					if ( mBOUser.RoleNameAssigned.Equals(UserManagementControl.ROLE_KOORDINATOR) )
					{
						mBOUser.IsCoordinator = true;
					}
					arlResults.Add(mBOUser);
				}
				drResults.Close();
				return arlResults;
			}
			catch (System.Data.OracleClient.OracleException oraex)
			{
				throw new UIFatalException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR) + oraex.Message);
			}
			catch (DbAccessException)
			{
				throw new UIFatalException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR) );
			}
		}
									 

		/// <summary>
		/// Get search criteria from values given in GUI
		/// Replace '*' with "", append Oracle wildcard '%' to strings.
		/// Department is an ID from the combobox, need to generate extra SQL WHERE clause
		/// </summary>
		private void GetSQLSearchCriteria()
		{
			this.mSearchUserSqlWhere	 = String.Empty;
			this.mParameterUserSurname	 = String.Empty;
			this.mParameterUserFirstName = String.Empty;
			this.mParameterUserApplID	 = String.Empty;
			this.mParameterUserDept      = String.Empty;
		
			mParameterUserSurname	= ((FrmUser)mView).TxtSearchSurname.Text;
			mParameterUserFirstName = ((FrmUser)mView).TxtSearchFirstname.Text;
			mParameterUserApplID	= ((FrmUser)mView).TxtSearchUser.Text;
			mParameterUserDept		= GetSelectedIDFromCbo( ((FrmUser)mView).CboSearchDepartment );

			if ( mParameterUserSurname.IndexOf("*") != -1 )
			{
				mParameterUserSurname = this.mParameterUserSurname.Replace("*", "%");
			}
			else
			{
				this.mParameterUserSurname = this.mParameterUserSurname + "%";
			}

			if ( mParameterUserFirstName.IndexOf("*") != -1)
			{
				mParameterUserFirstName = this.mParameterUserFirstName.Replace("*", "%");
			}
			else
			{
				this.mParameterUserFirstName = this.mParameterUserFirstName + "%";
			}

			if ( mParameterUserApplID.IndexOf("*") != -1 )
			{
				mParameterUserApplID = this.mParameterUserApplID.Replace("*", "%");
			}
			else
			{
				this.mParameterUserApplID = this.mParameterUserApplID + "%";
			}

			if ( mParameterUserDept.Length > 0 )
			{
				mSearchUserSqlWhere = " AND FPASS_DEPT_ID = "
									+ mParameterUserDept;
			}
			mSearchUserSqlWhere = mSearchUserSqlWhere 
								    + " ORDER BY UM_USERSURNAME";
		}

		
		#endregion // Region GetUsers
		
		#region LoadIndividualUser

		/// <summary>
		/// One user for edit is displayed in the textfields at the base of the form.
		/// BOs and sql commands are used in this model rather than datasets.
		/// For each user the assigned plants must be loaded, those which the user is manager of appear ticked
		/// </summary>
		internal void LoadIndividualUsers()
		{	
			try
			{
				// Clear listbox
				((FrmUser) mView).LikEditPlant.DataBindings.Clear();
				
				// Get ID of UserBO currently selected in grid
				mFPASSUserPKVal = ((FrmUser) mView).CurrentFPASSUserID;
				mUMUserPKVal    = ((FrmUser) mView).CurrentUMUserID;
				
				// Get UserBO correspoding to this ID
				if ( null == mBOUser || null == mHttUSERShow)
				{
					mBOUser = new BOUser();
				}
				else
				{
					mBOUser = (BOUser)mHttUSERShow[mFPASSUserPKVal];
				}
				
				int currDeptID    = mBOUser.DeptID;
				int currDomaineID = mBOUser.DomainID;

				// Load attributes into textfields
				((FrmUser) mView).TxtEditSurname.Text   = mBOUser.Surname;
				((FrmUser) mView).TxtEditFirstname.Text = mBOUser.Firstname;
				((FrmUser) mView).TxtEditUser.Text      = mBOUser.ApplUserID;
				((FrmUser) mView).TxtEditPhone.Text     = mBOUser.Telephone;

				// Locate current datarow & show vals in combobox
				LovItem itemDept    = this.GetLovItem(currDeptID, ((FrmUser) mView).CboEditDepartment);
				LovItem itemDomaine = this.GetLovItem(currDomaineID, ((FrmUser) mView).CboEditDomain);		
				((FrmUser) mView).CboEditDepartment.SelectedItem = itemDept;
				((FrmUser) mView).CboEditDomain.SelectedItem     = itemDomaine;

				// Fill List box with assigned plants
				this.LoadAssignedPlants();

				((FrmUser) mView).TxtEditSurname.Enabled = true;
				((FrmUser) mView).TxtEditFirstname.Enabled = true;
				((FrmUser) mView).TxtEditUser.Enabled = true;
				((FrmUser) mView).TxtEditPhone.Enabled = true;
				((FrmUser) mView).CboEditDepartment.Enabled = true;
				((FrmUser) mView).CboEditDomain.Enabled = true;
				((FrmUser) mView).CboCoCraft.Enabled = true;
				((FrmUser) mView).LikEditPlant.Enabled = true;
				((FrmUser) mView).ContentChanged = false;
			}
			catch (System.Data.OracleClient.OracleException oraex)
			{
				throw new UIFatalException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR) + oraex.Message );
			
			}
			catch (DbAccessException odr)
			{
				throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR) + odr.Message );
			}
		}


		/// <summary>
		/// Load the plants to which the currently seleced user is assigened (ie master of)
		/// Shown in checkbox after selecting a user.
		/// Only shows plants with status Valid
		/// </summary>
		private void LoadAssignedPlants()
		{					
			mIsAlreadyPlMaster = false;

			/// Get the plant IDs for the current user from the database
			IDbCommand plComm = mProvider.CreateCommand(ASSIGNEDPLANTS_QUERY);
			mProvider.SetParameter(plComm, ASSIGNPLANT_USER_PARA, mFPASSUserPKVal);

			arlAssignedPlants = new ArrayList();
			
			// Open data reader to get plants assigned to current user
			IDataReader drResults = mProvider.GetReader(plComm);

            while (drResults.Read())
            {
                mIsAlreadyPlMaster = true;

                int assPlantID = Convert.ToInt32(drResults["USPL_PL_ID"]);
                string assPlantName = Convert.ToString(drResults["PL_NAME"]);
                arlAssignedPlants.Add(assPlantName);
            }
			drResults.Close();

			// Clear the checklistbox
			((FrmUser) mView).LikEditPlant.Items.Clear();		
			
			// Get the plants, tick those for which the current user is master, and display in ListBox
			this.GetLOVPlants();

			// Tick those plants in the LOV the user is assigned to (= master of)	
			if ( arlAssignedPlants.Count == 0)
			{
				foreach (LovItem item in arlLOVPlants)
				{			
					// Show unchecked
					((FrmUser)mView).LikEditPlant.Items.Add(item.ItemValue);
				}
			}			
			else
			{	
				ArrayList arlShowPlants = new ArrayList(arlLOVPlants);
				
				foreach (LovItem item in arlLOVPlants)
				{
					foreach (string assPlantName in arlAssignedPlants)
					{
						if (assPlantName.Equals(item.ItemValue))
						{
							// Show checked
							((FrmUser)mView).LikEditPlant.Items.Add(item.ItemValue, true);
							arlShowPlants.Remove(item);
						}
					}
				}
				foreach (LovItem item in arlShowPlants)
				{						
					// Show un checked
					((FrmUser)mView).LikEditPlant.Items.Add(item.ItemValue);		
				}			
			}
			arlAssignedPlants = null;
		}

		#endregion



		/// <summary>
		/// Enable textfields and load plants to create new user
		/// </summary>
		internal void CreateNewUser()
		{
			this.SetCurrentUserIDToDefault();
			this.ClearTextFields();
			((FrmUser) mView).TxtEditSurname.Enabled = true;
			((FrmUser) mView).TxtEditFirstname.Enabled = true;
			((FrmUser) mView).TxtEditUser.Enabled = true;
			((FrmUser) mView).TxtEditPhone.Enabled = true;
			((FrmUser) mView).CboEditDepartment.Enabled = true;
			((FrmUser) mView).CboEditDomain.Enabled = true;
			((FrmUser) mView).CboCoCraft.Enabled = true;
			((FrmUser) mView).LikEditPlant.Text = "";
			((FrmUser) mView).LikEditPlant.Enabled = true;

			// Get the entire LOV of plants & show them
			this.GetLOVPlants();
			foreach (LovItem item in arlLOVPlants)
			{
				((FrmUser)mView).LikEditPlant.Items.Add(item.ItemValue);	
			}
			((FrmUser) mView).LikEditPlant.Enabled = true;
			((FrmUser) mView).ContentChanged = false;
		}

		
		/// <summary>
		/// Save details of new user
		/// Check the login ID (operating system login) is unique for the given mandator
		/// Generate PKs for the user, user-mand, user-plant, user-role "plant Manager" DB tables:
		/// must do this before transaction is opened
		/// Execute inserts to DB tables, if user has plants assigend then assign role "plant Manager"
		/// </summary>
		internal void SaveNewUser()
		{
			IDbCommand nextSeq;
			IDbCommand insComm;
			IDbCommand insComm2;
			IDbCommand insComm3;
			IDbCommand insComm4;
			IDbCommand dummyComm;
			IDataReader drTransaction = null;
			IDbTransaction trans      = null;
			decimal currUserParaTabPK = 0;
	     
			// Get values of GUI fields and check their correctness
			this.CheckTextFields();

			try
			{
				// Check if the new user login ID is unique for the currenrt mandator
				if ( !IsUserApplIDUnique() )
				{
					//  user loginname (ApplID) is not unique
					((FrmUser) mView).Cursor = System.Windows.Forms.Cursors.Default;
					throw new UIWarningException( 
						MessageSingleton.GetInstance().GetMessage(MessageSingleton.USER_NOT_UNIQUE) );				
				}
				
				// Create command for selecting new PK val from sequence
				nextSeq = mProvider.CreateCommand("SequenceDummy");					
				
				// Create command for insert & Bind PK & parameters for UM_USER
				insComm      = mProvider.CreateCommand( INSERT_UM_USERS );
				mUMUserPKVal = this.GetNextValFromSeq( "SEQ_USER" );
				mProvider.SetParameter( insComm, ":US_ID", mUMUserPKVal );				
				this.SetNewUMUserValues(insComm, US_DELETED_FALSE);

				// Create command for fpass_user
				insComm2		= mProvider.CreateCommand( INSERT_FPASS_USERS );
				mFPASSUserPKVal = this.GetNextValFromSeq( "SEQ_USERFPASS" );
				mProvider.SetParameter(insComm2, ":USER_ID", mFPASSUserPKVal);

				// Check if new plants have been assigned: attribute Is Plant Master
				this.GetNewAssignedPlantIDs();

				if (arlPlantsNewUser.Count !=0)
				{
					this.mStrPlantMaster = Globals.DB_YES;						
				}
				else
				{
					this.mStrPlantMaster = Globals.DB_NO;
				}
				// Set all other parameters for FPASS_USER
				this.SetNewFPASSValues(insComm2, mUMUserPKVal, mStrPlantMaster);	

				// Assign Fpass User to mandator
				insComm3 = mProvider.CreateCommand(ASSIGNENEWUSERMAND);						
				this.SetFPASSUserToMandatorValues(insComm3, mFPASSUserPKVal);

				// Add record to UserParameter: create command and get PK
				insComm4          = mProvider.CreateCommand( INSERT_USER_PAR_TABLE );
				currUserParaTabPK = this.GetNextValFromSeq( "SEQ_USERPARAMETER" );
				mProvider.SetParameter( insComm4, ":UPAR_ID", currUserParaTabPK );
				this.SetFPASSUserParameterTabValues( insComm4, mFPASSUserPKVal );
			
				// Get PK values for the n:n table user to plant 
				this.mHttUSERShowToPlantPKs = new Hashtable();
				this.GetAssignedPlantPKs(nextSeq);				
								
				// Open the transaction using dummy command & connection
				dummyComm     = mProvider.CreateCommand("SequenceDummy");
				drTransaction = mProvider.GetReader(dummyComm);
				trans         = mProvider.GetTransaction(dummyComm);

				// Use the open dummy connection for Insert
				// UM_USER
				insComm.Connection  = dummyComm.Connection;
				insComm.Transaction = trans;
				ret = insComm.ExecuteNonQuery();
				
				// FPASS_USER
				insComm2.Connection  = dummyComm.Connection;
				insComm2.Transaction = trans;
				ret = insComm2.ExecuteNonQuery();

				// Add the assignment user to mandator
				insComm3.Connection  = dummyComm.Connection;
				insComm3.Transaction = trans;
				ret = insComm3.ExecuteNonQuery();

				// FPASS User Parameters
				insComm4.Connection  = dummyComm.Connection;
				insComm4.Transaction = trans;
				ret = insComm4.ExecuteNonQuery();
	
				// Check if new plants have been assigned
				if (arlPlantsNewUser.Count !=0)
				{
					// Get selected values from checklistbox plants
					int j = 0;
					foreach (int insNewPlantFKVal in arlPlantsNewUser)
					{																
						// Foreign key is from FPASS_USER
						mUserToPlantFKUserVal  = mFPASSUserPKVal;
						mUserToPlantFKPlantVal = insNewPlantFKVal;
						
						this.SaveAssignedPlants(Convert.ToInt32(mHttUSERShowToPlantPKs[j]), dummyComm, trans);
						j ++;
					}

					// If this is a new user with new plants assigned, it must also receive role Plant Master.
					this.AddRolePlantManager(mUMUserPKVal, dummyComm, trans);

				}
				// Tidy up and restore defaults
				trans.Commit();
				drTransaction.Close();				

				// Must update datagrid: new user should stay in scope so that roles can be assigned in UserToRole Mask
				((FrmUser)mView).CurrentFPASSUserID  = mFPASSUserPKVal;
				((FrmUser)mView).ContentChanged      = false;
				((FrmUser)mView).CheckListBoxChanged = false;

				// Must update datagrid
				this.RefreshBODisplay();

			}
			catch (UIWarningException uie)
			{
				throw new UIWarningException(uie.Message);
			}	
			catch (System.Data.OracleClient.OracleException oraex)
			{
				if (trans != null && drTransaction != null)
				{
					trans.Rollback();
					drTransaction.Close();
				}
				if ( oraex.Code == 20000 )
				{
					// This is the error code when the user loginname (ApplID) is not unique
					// Combination mandator - login id must be unique
					throw new UIWarningException( 
						MessageSingleton.GetInstance().GetMessage(MessageSingleton.USER_NOT_UNIQUE) );
				}
				else
				{
					throw new UIFatalException( 
						MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR) 
						+ oraex.Message );
				}
			}

			catch (DbAccessException odr)
			{
				if (trans != null && drTransaction != null)
				{
					trans.Rollback();
					drTransaction.Close();
				}
				throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(
											MessageSingleton.FATAL_DB_ERROR) 
											+ odr.Message );
			}
		}

		/// <summary>
		/// Save changes (update) to the current user
		/// Re-check if user login ID unique
		/// Check the assigned plants, if these have changed, delete all existing and reassign appropriate
		/// Role "Plant Manager" automatically assigned if user has plants assiigned to him 
		/// </summary>
		internal void SaveChangesUser()
		{
			// Get current FPASS user and UM User ID (could also do this from current UserBO)
			mFPASSUserPKVal = ((FrmUser)mView).CurrentFPASSUserID;
			mUMUserPKVal    = ((FrmUser)mView).CurrentUMUserID;

			IDbCommand nextSeq;
			IDbCommand updComm;
			IDbCommand updComm2;
			IDbCommand dummyComm;
			IDataReader drTransaction = null;
			IDbTransaction trans	= null;
			mStrPlantMaster			= Globals.DB_NO;

			// Check fields have been filled
			this.CheckTextFields();

			try
			{			
				// Check if the user login id has been changed: 
				// if so then Combination mandator - domain name and login name must be unique
				if ( !mNewUserID.Equals(mBOUser.ApplUserID) 
					|| !mNewDomainID.Equals(mBOUser.DomainID) )
				{
					if ( !IsUserApplIDUnique() )
					{
						((FrmUser) mView).Cursor = System.Windows.Forms.Cursors.Default;
						throw new UIWarningException( 
							MessageSingleton.GetInstance().GetMessage(MessageSingleton.USER_NOT_UNIQUE) );				
					}
				}
								
				// Create commands for updates: UM_USER 
				updComm = mProvider.CreateCommand( UPDATE_UM_USERS );
				mProvider.SetParameter( updComm, ":US_ID", mUMUserPKVal );				
				this.SetChangedUMUserValues( updComm, US_DELETED_FALSE );

				/// Create command for fpass_user
				updComm2 = mProvider.CreateCommand( UPDATE_FPASS_USERS );
				mProvider.SetParameter( updComm2, ":USER_ID", mFPASSUserPKVal );

				/// Check if user has plants assigned
				this.GetNewAssignedPlantIDs();

				if ( arlPlantsNewUser.Count > 0 )
				{
					/// Has plants assigned: is plant manager
					mStrPlantMaster			   = Globals.DB_YES;
					mBOUser.IsPlantManager = true;
				
					if ( ((FrmUser)mView).CheckListBoxChanged )
					{
						/// Get PK values for the n:n table user to plant plant
						/// for insert into DB table below
						this.mHttUSERShowToPlantPKs = new Hashtable();
						nextSeq = mProvider.CreateCommand("SequenceDummy");
						this.GetAssignedPlantPKs(nextSeq);	
					}
				}
				else
				{
					/// If user had plants but has none after the update, 
					/// is no longer Plant Manager
					mStrPlantMaster			   = Globals.DB_NO;
					mBOUser.IsPlantManager = false;

				}
				this.SetNewFPASSValues(updComm2, mUMUserPKVal, mStrPlantMaster);

				// Open the transaction with dummy command & connection
				dummyComm     = mProvider.CreateCommand("SequenceDummy");
				drTransaction = mProvider.GetReader(dummyComm);
				trans         = mProvider.GetTransaction(dummyComm);

				// Use the open dummy connection for Update
				// UM_USER
				updComm.Connection  = dummyComm.Connection;
				updComm.Transaction = trans;
				ret = updComm.ExecuteNonQuery();

				// FPASS_USER
				updComm2.Connection  = dummyComm.Connection;
				updComm2.Transaction = trans;
				ret = updComm2.ExecuteNonQuery();

				/// Patched 02.03.2005: If plant assignemts have changed, delete all plants and reassign
				/// Delete all existing plants: all plants ticked in the GUI are reassigned
				if ( ((FrmUser)mView).CheckListBoxChanged )
				{
					this.DeleteAssignedPlants(dummyComm, trans);

					if (arlPlantsNewUser.Count !=0)
					{					
						int j = 0;
						foreach (int insNewPlantFKVal in arlPlantsNewUser)
						{																
							/// Foreign key is from FPASS_USER
							mUserToPlantFKUserVal  = mFPASSUserPKVal;
							mUserToPlantFKPlantVal = insNewPlantFKVal;
						
							this.SaveAssignedPlants(Convert.ToInt32(mHttUSERShowToPlantPKs[j]), dummyComm, trans);
							j ++;
						}
						/// If this is a user with new plants assigned (none before update)
						/// then it must also receive role Plant manager.
						if ( mBOUser.IsPlantManager && !mIsAlreadyPlMaster )
						{
							this.AddRolePlantManager(mUMUserPKVal, dummyComm, trans);
							mBOUser.IsPlantManager = true;
						}				
					}
					else
					{				
						/// If user had plants before this update and now has none 
						/// then delete assignment of role "Plant manager"
						this.DeleteRolePlantManager(mUMUserPKVal, dummyComm, trans);					
					}
				}
				/// End of patch

				// Tidy up and restore defaults
				arlPlantsNewUser = null;
				trans.Commit();
				drTransaction.Close();

				((FrmUser) mView).ContentChanged      = false;
				((FrmUser) mView).CheckListBoxChanged = false;

				// Must update datagrid
				this.RefreshBODisplay();

			}
			catch (UIWarningException uie)
			{
				throw new UIWarningException(uie.Message);
			}	
			catch (System.Data.OracleClient.OracleException oraex)
			{
				if (trans != null && drTransaction != null)
				{
					trans.Rollback();
					drTransaction.Close();
				}				
				throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR) 
					+ oraex.Message );
			}
			catch (DbAccessException odr)
			{
				if (trans != null && drTransaction != null)
				{
					trans.Rollback();
					drTransaction.Close();
				}
				throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR) 
					+ odr.Message );
			}	
		}

		/// <summary>
		/// User is deleted by setting DELETED = true in the database
		/// Done by stored procedure
		/// If the user is a coordinator any coworkers assigned to him must be reassigned,
		/// after which the coordinator data & assignment to excontractor are archived.
		/// </summary>
		internal void DeleteUser()
		{
			// Get current FAPSS user and UM User ID
			mFPASSUserPKVal = ((FrmUser)mView).CurrentFPASSUserID;
			mUMUserPKVal    = ((FrmUser)mView).CurrentUMUserID;
			
			string currEXCCOName;
			IDbCommand delComm2;
			IDbCommand dummyComm;
			IDataReader drTransaction = null;
			IDbTransaction trans = null;

			bool flg_CoordDep = true;
			bool flgSuccess   = false;

			try
			{	
				// Delete the user (set inactive) via Stored Proc so that details are archived
				delComm2			 = mProvider.CreateCommand("SequenceDummy");
				delComm2.CommandType = CommandType.StoredProcedure;
				delComm2.CommandText = "SP_SetDeleteFlagUser "
									+ "( "
									+ mUMUserPKVal
									+ ", "
									+ UserManagementControl.getInstance().CurrentUserID
									+ ")";

				/// if the current user is a coordinator, their dependent coworkers have to be reassigned.
				if ( mBOUser.IsCoordinator )
				{
					/// get coordinator id
					mCurrentCoordID = RoleCoordinatorSingleton.GetInstance().GetCurrentCoordID(mFPASSUserPKVal);

					httAlternativeCoordsByEXCO = new Hashtable();
					httAlternativeCoordsByEXCO = RoleCoordinatorSingleton.GetInstance().GetAlternativeCoordinators(mCurrentCoordID);
					
					if ( null == httAlternativeCoordsByEXCO )
					{
						// the coordinator has no dependent coworkers: ok to delete
						flg_CoordDep = false;
					}
				}

				// Open dummy command & connection to create transaction
				dummyComm     = mProvider.CreateCommand("SequenceDummy");
				drTransaction = mProvider.GetReader(dummyComm);
				trans         = mProvider.GetTransaction(dummyComm);


				// If user is a coordinator, assign dependent coworkers to other coordinator
				if ( mBOUser.IsCoordinator && flg_CoordDep && null != httAlternativeCoordsByEXCO )
				{
					foreach ( object obj in httAlternativeCoordsByEXCO.Keys )
					{
						decimal k     = Convert.ToDecimal(obj);
						// If entry in hashtable contains an arraylist of cooeindators, then ok
						object objEntry = httAlternativeCoordsByEXCO[k];

						// If entry contains a string (name of exoc to which there are no other coords assigned), error
						if ( objEntry.GetType().ToString().Equals("System.String") )
						{
							currEXCCOName = objEntry.ToString().Trim();
							throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.COORD_DEP_CWR_USER) + currEXCCOName + " vorliegen.");
		
						}
					}
					// Else prompt user to choose an alternative coordinator for the coworkers from each excontractor & reassign
					foreach ( object obj in httAlternativeCoordsByEXCO.Keys )
					{						
						// k ist the current EXCOID
						decimal k     = Convert.ToDecimal(obj);
						ArrayList alt = (ArrayList) httAlternativeCoordsByEXCO[k];
				
						flgSuccess = RoleCoordinatorSingleton.GetInstance().ReAssignCoWorkers(
																	true,
																	mCurrentCoordID, 
																	k, 
																	alt, 
																	dummyComm,
																	trans);
						// Coordinators were not successfully reassigned
						if ( !flgSuccess )
						{
							throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.COORD_REASS_ERROR));
						}
					}
				}

				// Use the open dummy connection for Insert
				delComm2.Connection  = dummyComm.Connection;
				delComm2.Transaction = trans;
				ret = delComm2.ExecuteNonQuery();
		
				// Tidy up and execute new search
				// Must also export newly assigned coworkers to ZKS
				trans.Commit();
				drTransaction.Close();


                /*
                 * No longer required, coordinator data is not exported to ZKS
				// If user was a coordinator, remove transaction and export to ZKS
				if ( mBOUser.IsCoordinator && flgSuccess )
				{
					try
					{
						trans.Dispose();
						base.ExportAllToZKS();
					}
					catch ( UIWarningException )
					{
						// do nothing
					}
				} 
                */

                ClearTextFields();
				SetCurrentUserIDToDefault();
				GetUsers(false);
			}
			catch (UIWarningException uie)
			{
				try
				{
					if (trans != null && drTransaction != null)
					{
						trans.Rollback();
						drTransaction.Close();
					}
				}
				catch ( Exception)
				{
					// Swallow: if connection already closed etc
				}
				throw new UIWarningException(uie.Message);
			}	
			catch (System.Data.OracleClient.OracleException oraex)
			{
				if (trans != null && drTransaction != null)
				{
					trans.Rollback();
					drTransaction.Close();
				}
				throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR) 
					+ oraex.Message );
			}		
		}


		/// <summary>
		/// Each user can be assigned to (= master of) more than 1 plant.
		/// Items are stored in member arraylist: for each plant selected in checkbox, add its ID to the arraylist
		/// Cannot easily determine if items in checklistbox have been checked or unchecked:
		/// therefore recreate the assignment user to plant.
		/// </summary>
		internal void GetNewAssignedPlantIDs()
		{		
			// Get IDs of plants checked in listbox: add to arraylist
			arlPlantsNewUser = new ArrayList();
			
			if ( ((FrmUser)mView).LikEditPlant.CheckedItems.Count != 0)
			{
				for(int x = 0; x <= ((FrmUser)mView).LikEditPlant.CheckedItems.Count - 1 ; x++)
				{
					string newUserPlantName =  ((FrmUser)mView).LikEditPlant.CheckedItems[x].ToString();
					arlPlantsNewUser.Add(Convert.ToInt32(mHttLOVPlants[newUserPlantName]));					
				}					
			}
		}

		/// <summary>
		/// Generate as many unique PK values as there are assigned plants for this user
		/// Must do this before the transaction is opened
		/// </summary>
		private void GetAssignedPlantPKs(IDbCommand pseqCommand)
		{
			// Check if new plants have been assigned
			if (arlPlantsNewUser.Count != 0)
			{				
				// Get selected values from checklistbox plants
				int j = 0;
				foreach (int insNewPlantID in arlPlantsNewUser)
				{				
					mUserToPlantPKVal = this.GetNextValFromSeq("SEQ_USERPLANT");
					mHttUSERShowToPlantPKs.Add(j, mUserToPlantPKVal);
					j++;
				}
			}
		}

		/// <summary>
		/// Execute SQL command to assign plants to current user
		/// </summary>
		/// <param name="pNewUserToPlantPKVal">current open connection</param>
		/// <param name="pCommConnection">current open transaction</param>
		/// <param name="pTrans"></param>
		private void SaveAssignedPlants(int pNewUserToPlantPKVal,  IDbCommand pCommConnection, IDbTransaction pTrans)
		{			
			
			// Create command for adding the plants
			IDbCommand commAssignPlant = mProvider.CreateCommand(ASSIGNNEWPLANTS);
							
			mProvider.SetParameter(commAssignPlant, ":USPL_ID", pNewUserToPlantPKVal);
			// Bind other parameters including FKs
			this.SetNewPlantToFPASSUserValues(commAssignPlant, mUserToPlantFKPlantVal, mUserToPlantFKUserVal);

			commAssignPlant.Connection = pCommConnection.Connection;
			commAssignPlant.Transaction = pTrans;
			ret = commAssignPlant.ExecuteNonQuery();
			commAssignPlant = null;
			
		}


		/// <summary>
		/// Execute SQL command to delete all plants assigned to current user
		/// </summary>
		/// <param name="pConnConnect">current open connection</param>
		/// <param name="pTrans">current open transaction</param>
		private void DeleteAssignedPlants(IDbCommand pConnConnect, IDbTransaction pTrans)
		{
			IDbCommand commDelPlant;
			int ret;

			// Create command for deleting the plants
			commDelPlant = mProvider.CreateCommand(DELETEUSERPLANTS);
			mProvider.SetParameter(commDelPlant, ":USPL_USER_ID", mFPASSUserPKVal);		
				
			commDelPlant.Connection  = pConnConnect.Connection;
			commDelPlant.Transaction = pTrans;
			ret = commDelPlant.ExecuteNonQuery();		
			commDelPlant = null;
		}


		/// <summary>
		/// Assign the role Plant Manager to the current user
		/// Parameters allow command to be executed as part of open transaction
		/// ID of role Plant Manager is mandat- dependent, only known at runtime
		/// </summary>
		/// <param name="pUMUserToRoleUMUserFKVal"></param>
		/// <param name="pConnConnect"></param>
		/// <param name="pTrans"></param>
		private void AddRolePlantManager(decimal pUMUserToRoleUMUserFKVal, IDbCommand pConnConnect, IDbTransaction pTrans)
		{
			// If new plants have been assigned to the user, it automatically receives the role "plant manager"
			IDbCommand commAddRole;
			int ret;
				
			commAddRole = mProvider.CreateCommand(ADDROLEPLANTMANAGER);								
			mProvider.SetParameter(commAddRole, ":RL_ROLEID", Globals.GetInstance().PlantManagerRoleID );
			mProvider.SetParameter(commAddRole, ":RL_AUTHORIZEDENTITYID", pUMUserToRoleUMUserFKVal);
			mProvider.SetParameter(commAddRole, ":RL_CHANGEUSER", UserManagementControl.getInstance().CurrentUserID);
			mProvider.SetParameter(commAddRole, ":RL_TIMESTAMP", System.DateTime.Now);
			
			commAddRole.Connection = pConnConnect.Connection;
			commAddRole.Transaction = pTrans;
			ret = commAddRole.ExecuteNonQuery();
			commAddRole = null;
		}

		/// <summary>
		/// Deletes & archives the assignment of role "Plant Manager" to the current user.
		/// Every time the assignment of plants to user is changed, all the plants plus the role are deleted
		/// and the new and the role "Plant Manager" are reassigned
		/// Parameters allow command to be executed as part of open transaction
		/// </summary>
		/// <param name="pUMUserToRoleUMUserFKVal"></param>
		/// <param name="pConnConnect"></param>
		/// <param name="pTrans"></param>
		private void DeleteRolePlantManager(decimal pUMUserToRoleUMUserFKVal, IDbCommand pConnConnect, IDbTransaction pTrans)
		{			
			// If a user has been changed, delete the role "plant manager": reassign afterwards
			IDbCommand commDelRole;
			int ret;
				

			decimal currentUMRolePKID = Globals.GetInstance().PlantManagerRoleID;
			// Create command for deleting the role "Plant Manager": stored procedure allows the old assignment to be archived
			commDelRole = mProvider.CreateCommand("SequenceDummy");					
			commDelRole.CommandText = SP_ARCH_ROLELINK
				+ "( " 
				+ currentUMRolePKID
				+ pUMUserToRoleUMUserFKVal
				+ ", " 
				+ UserManagementControl.getInstance().CurrentUserID
				+ ")";
				
			commDelRole.CommandType = System.Data.CommandType.StoredProcedure;
			commDelRole.Connection  = pConnConnect.Connection;
			commDelRole.Transaction = pTrans;
			ret = commDelRole.ExecuteNonQuery();
		}

		private void SetNewUMUserValues(IDbCommand pCommand, int pDeletedVal)
		{
			mProvider.SetParameter(pCommand, ":US_NAME",       mNewSurname);
			mProvider.SetParameter(pCommand, ":US_FIRSTNAME",  mNewFirstName);
			mProvider.SetParameter(pCommand, ":US_PASSWORD",   US_PASSWORD_VAL);
			mProvider.SetParameter(pCommand, ":US_FIRSTLOGIN", US_FIRSTLOGIN_VAL);
			mProvider.SetParameter(pCommand, ":US_USERID",     mNewUserID);
			mProvider.SetParameter(pCommand, ":US_DELETED",    pDeletedVal );
			mProvider.SetParameter(pCommand, ":US_CHANGEUSER", UserManagementControl.getInstance().CurrentUserID);
			mProvider.SetParameter(pCommand, ":US_TIMESTAMP",  DateTime.Now);
		}

		private void SetChangedUMUserValues(IDbCommand pCommand, int pDeletedVal)
		{	
			mProvider.SetParameter(pCommand, ":US_NAME",       mNewSurname);
			mProvider.SetParameter(pCommand, ":US_FIRSTNAME",  mNewFirstName);
			mProvider.SetParameter(pCommand, ":US_USERID",     mNewUserID);
			mProvider.SetParameter(pCommand, ":US_DELETED",    pDeletedVal );
			mProvider.SetParameter(pCommand, ":US_CHANGEUSER", UserManagementControl.getInstance().CurrentUserID);
			mProvider.SetParameter(pCommand, ":US_TIMESTAMP",  DateTime.Now);
		}
	
	
		private void SetNewFPASSValues(IDbCommand pCommand, decimal pUMUserFKVal, string pIsPlantMaster)
		{	
			mProvider.SetParameter(pCommand, ":USER_US_ID",      pUMUserFKVal);
			mProvider.SetParameter(pCommand, ":USER_DEPT_ID",    mNewDeptID);
			mProvider.SetParameter(pCommand, ":USER_UDOM_ID",    mNewDomainID);
			mProvider.SetParameter(pCommand, ":USER_TEL",        mNewTEL);
			mProvider.SetParameter(pCommand, ":USER_PLANTMASTER_YN", pIsPlantMaster);
			mProvider.SetParameter(pCommand, ":USER_DELETE_YN",  Globals.DB_NO);
			mProvider.SetParameter(pCommand, ":USER_NAME",       mNewUserID);
			mProvider.SetParameter(pCommand, ":USER_CHANGEUSER", UserManagementControl.getInstance().CurrentUserID);
			mProvider.SetParameter(pCommand, ":USER_TIMESTAMP",  DateTime.Now);
		}

		/// <summary>
		/// Set SQL parameters for INSERT statement: User - mandator record
		/// </summary>
		/// <param name="pCommand"></param>
		/// <param name="pFPASSUserFKVal"></param>
		private void SetFPASSUserToMandatorValues(IDbCommand pCommand, decimal pFPASSUserFKVal)
		{	
			mProvider.SetParameter(pCommand, ":USM_MND_ID",     UserManagementControl.getInstance().CurrentMandatorID.ToString());
			mProvider.SetParameter(pCommand, ":USM_USER_ID",    pFPASSUserFKVal);
			mProvider.SetParameter(pCommand, ":USM_CHANGEUSER", UserManagementControl.getInstance().CurrentUserID);
			mProvider.SetParameter(pCommand, ":USM_TIMESTAMP",  DateTime.Now);
		}

		/// <summary>
		/// Set SQL parameters for INSERT statement: Users FPASS parameters record
		/// </summary>
		/// <param name="pCommand"></param>
		/// <param name="pFPASSUserFKVal"></param>
		private void SetFPASSUserParameterTabValues(IDbCommand pCommand, decimal pFPASSUserFKVal)
		{	
			mProvider.SetParameter(pCommand, ":UPAR_ENTRANCEFLOWTIME", Globals.GetInstance().ParaEntranceFlowTime );
			mProvider.SetParameter(pCommand, ":UPAR_NAUMBEROFPASS", Globals.GetInstance().ParaNumberOfPrintedPass );
			mProvider.SetParameter(pCommand, ":UPAR_PRINTPREVIEW_YN", Globals.GetInstance().ParaPrintPreview );
			mProvider.SetParameter(pCommand, ":UPAR_CHANGEUSER", UserManagementControl.getInstance().CurrentUserID);
			mProvider.SetParameter(pCommand, ":UPAR_TIMESTAMP", DateTime.Now);
			mProvider.SetParameter(pCommand, ":UPAR_USER_ID", pFPASSUserFKVal);
			mProvider.SetParameter(pCommand, ":UPAR_MND_ID", UserManagementControl.getInstance().CurrentMandatorID.ToString());
		}

		private void SetNewPlantToFPASSUserValues(IDbCommand pCommand, decimal pPlantFKVal, decimal pFPASSUserFKVal)
		{					                
			mProvider.SetParameter(pCommand, ":USPL_PL_ID", pPlantFKVal);
			mProvider.SetParameter(pCommand, ":USPL_USER_ID", pFPASSUserFKVal);
			mProvider.SetParameter(pCommand, ":USPL_CHANGEUSER", UserManagementControl.getInstance().CurrentUserID);
			mProvider.SetParameter(pCommand, ":USPL_TIMESTAMP", DateTime.Now);
		}

		/// <summary>
		/// Is the combination of mandator and domain name + user unqiue?
		/// Query the database: if more than 0 records returned, userID is not unique
		/// </summary>
		/// <returns>false if the user login id already exists</returns>
		private bool IsUserApplIDUnique()
		{			
			int    numRecs	= 0;
			string strNewUserWithDomain = mNewDomainName + "\\" + mNewUserID;

			IDbCommand selComm = mProvider.CreateCommand(USER_UNIQUE_QUERY);

			mProvider.SetParameter( selComm, USER_UNIQUE_MND_PARA,   UserManagementControl.getInstance().CurrentMandatorID.ToString() );
			//mProvider.SetParameter( selComm, USER_UNIQUE_APPID_PARA, mNewUserID.ToUpper() );
			mProvider.SetParameter( selComm, USER_UNIQUE_APPID_PARA, strNewUserWithDomain.ToUpper() );
			
			IDataReader drResults = mProvider.GetReader(selComm);

			while (drResults.Read())
			{
				numRecs++;
			}
			drResults.Close();

			if ( numRecs > 0 )
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		#endregion // End of Methods

		#region SortUsers

		// Test, not in use
//		private void SortUsersByName(bool pAsc)
//		{
//			if ( null != mArlUsersShow )
//			{
//				mArlUsersShow.Sort(new UserNameComparer() );
//			}				
//		}
//
//		private void SortUsersByApplID(bool pAsc)
//		{
//		}
//
//		private void SortUserByShownRole()
//		{
//
//		}


		#endregion

		#region InnerClasses
		/// <summary>
		/// Internal comparer class to sort the mandatory field names 
		/// </summary>
		public class UserNameComparer : IComparer
		{
			/// <summary>
			/// Compares two Controls to sort them by tab index.
			/// </summary>
			/// <param name="pFirst">A first Control to be compared by tab index.</param>
			/// <param name="pSecond">A second Control to be compared by tab index.</param>
			/// <returns>-1 if the tab index of the first is less then the second, 0 if 
			/// both tab indicees are equal, 1 if the second tab index is less then the first.</returns>
			public int Compare(object pFirst, object pSecond)
			{
				try
				{
					BOUser currBO1 = (BOUser) pFirst;
					BOUser currBO2 = (BOUser) pSecond;
					return currBO1.UserFormattedName.CompareTo(currBO2.UserFormattedName);
				}
				catch(Exception)
				{
					return 0;
				}
			}
		}
		
		/// <summary>
		/// Sort users according to login ID (appl ID)
		/// </summary>
		public class UserApplIDComparer : IComparer
		{
			/// <summary>
			/// Compares two Controls to sort them by tab index.
			/// </summary>
			/// <param name="pFirst">A first Control to be compared by tab index.</param>
			/// <param name="pSecond">A second Control to be compared by tab index.</param>
			/// <returns>-1 if the tab index of the first is less then the second, 0 if 
			/// both tab indicees are equal, 1 if the second tab index is less then the first.</returns>
			public int Compare(object pFirst, object pSecond)
			{
				try
				{
					BOUser currBO1 = (BOUser) pFirst;
					BOUser currBO2 = (BOUser) pSecond;
					return currBO1.ApplUserID.CompareTo(currBO2.ApplUserID);
				}
				catch(Exception)
				{
					return 0;
				}
			}
		}
						  
		#endregion //End of InnerClasses

	}
}

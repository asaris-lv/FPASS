using System;
using System.Collections;
using System.Data;

using Degussa.FPASS.Bo.Administration;
using Degussa.FPASS.Gui;
using Degussa.FPASS.FPASSApplication;
using Degussa.FPASS.Db;
using Degussa.FPASS.Db.DataSets;
using Degussa.FPASS.Gui.Dialog.Administration.UserControls;
using Degussa.FPASS.Util;
using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.Util.Exceptions;
using Degussa.FPASS.Util.UserManagement;
using Degussa.FPASS.Util.Validation;

using de.pta.Component.DbAccess;
using de.pta.Component.DbAccess.Exceptions;
using de.pta.Component.Errorhandling;
using de.pta.Component.ListOfValues;

namespace Degussa.FPASS.Gui.Dialog.User
{
	/// <summary>
    /// Contains the functionality required to assign users to roles
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
	public class UserToRoleModel : FPASSBaseModel
	{
		#region Members

		// For database access
		private IProvider      mProvider;	
		private IDbCommand	   mSelRoleComm;
		private IDbDataAdapter mDataAdapterUserToRole;
		private DSRole		   mDSRole;

		private DataRow		   mCurrentRoleRow;

		private const string USERTOROLE_ADA_ID      = "DSAssignRole";
		private const string USERTOROLE_TABLE		= "UM_ROLELINK";
		private const string USERTOROLE_UMUSER_PARA = ":RL_AUTHORIZEDENTITYID";
		private const string USERTOROLE_MAND_PARA   = ":MND_ID";
		private const string USERTOROLE_QUERY       = "SelectRoleByUser";
		private const string ADDROLECOORD_SQL		= "AddRoleCoordinator";  
		private const string SP_ARCH_ROLELINK		= "SP_archivcheckrl";

		private decimal mCurrentUMUserPKID;
		private decimal mCurrentFPASSUserPKID;
		private decimal mCurrentUMRolePKID;
		private decimal mCurrentCoordID;

		private string mCurrentRoleName;

		private BOUser mBOUser;

		private Hashtable httAlternativeCoordsByEXCO;

		#endregion Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public UserToRoleModel()
		{
			initialize();
		}

		#endregion Constructors

		#region Initialization

		/// <summary>
        /// Initializes the members (Instantiate DataAdpators)
		/// </summary>
		private void initialize()
		{
			mProvider = DBSingleton.GetInstance().DataProvider;
			mDataAdapterUserToRole = mProvider.CreateDataAdapter(USERTOROLE_ADA_ID);
			mSelRoleComm = mProvider.CreateCommand(USERTOROLE_QUERY);	
		}	

		#endregion Initialization

		#region Accessors 

        /// <summary>
        /// Gets or sets current User BO
        /// </summary>
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

		#endregion Accessors

		#region Methods 

		/// <summary>
		/// Set selected role to to zero (default)
		/// </summary>
		internal override void PreShow()
		{
			SetCurrentRoleIDToDefault();
		}

		internal void RegisterUserToRoleDataSet(DSRole pDSRole)
		{
			mDSRole = pDSRole;
		}

		/// <summary>
		/// Fill textbox in FrmUserToRole (thismView) with name of current user. BO is passed from FrmUser
		/// </summary>
		internal void LoadFormCurrentUser()
		{
			((FrmUserToRole) mView).TxtUser.Text = mBOUser.UserFormattedName;
		}

		/// <summary>
		/// Set role IDs back to zero when form gets focus
		/// </summary>
		private void SetCurrentRoleIDToDefault()
		{
			((FrmUserToRole) mView).CurrentRoleName = "";
			((FrmUserToRole) mView).CurrentUMRoleID = -1;
			mCurrentUMRolePKID = -1;
		}

		/// <summary>
		/// Fill dropdownlist for assignment of new roles.
		/// "Plant manager" does not appear in list as this role is assigned when user gets plants assigned
		/// </summary>
		internal void FillFrmLOVRoles()
		{
            ArrayList arrRolesShow = LovSingleton.GetInstance().GetRootList(null, "FPASS_ROLEBYMAND", "UM_ROLEFORMAT");
            arrRolesShow.Add(new LovItem("0", ""));
			
            LovItem plantManItem = null;
            foreach (LovItem item in arrRolesShow)
            {
                if (item.ItemValue.Trim() == UserManagementControl.ROLE_BETRIEBSMEISTER)
                {
                    plantManItem = item; 
                    break;
                }
            }

            if (null != plantManItem)
            { arrRolesShow.Remove(plantManItem); }
			arrRolesShow.Reverse();

			((FrmUserToRole) mView).CboAssignRole.DataSource = arrRolesShow;
			((FrmUserToRole) mView).CboAssignRole.DisplayMember = "ItemValue";
			((FrmUserToRole) mView).CboAssignRole.ValueMember = "DecId";
		}

		/// <summary>
		/// selects the roles to which the current user is already assigned
		/// loads these into typified dataset & shows in bound datagrid in GUI
		/// the Insert & delete statements in the datadaptor are different from the SELECT
		/// Throws <exception cref="de.pta.Component.Errorhandling">UIWarningException, UIFatalException</exception>
		/// and <exception cref="System.Data.ConstraintException">System.Data.ConstraintException</exception> if role already assigned
		/// </summary>
		internal void GetRoles()
		{
			int numrecs;
			decimal currUMUserFKVal = mBOUser.UMPKIdentifier;

			mDSRole.Clear();
				
			// Bind parameters to SELECT command and make sure it uses same connection as adaptor, assign new SELECT
			mProvider.SetParameter(mSelRoleComm, USERTOROLE_UMUSER_PARA, currUMUserFKVal);
			mSelRoleComm.Connection = mDataAdapterUserToRole.InsertCommand.Connection;
			mDataAdapterUserToRole.SelectCommand = mSelRoleComm;
	
			try
			{
				numrecs = mProvider.FillDataSet(USERTOROLE_ADA_ID, mDataAdapterUserToRole, mDSRole);
				if (numrecs < 1)
				{
					((FrmUserToRole) mView).BtnDeleteAssignment.Enabled = false;
					ShowMessageInStatusBar( MessageSingleton.GetInstance().GetMessage(MessageSingleton.NO_ROLES_ASSIGNED));
				}
				else
				{			
					foreach (DataRow dr in mDSRole.UM_ROLELINK)
					{
						for (int i = 0; i < mDSRole.Tables[USERTOROLE_TABLE].Columns.Count; i++)
						{
							if (dr[i].Equals(DBNull.Value))
							{
								dr[i] = "";
							}
						}				
					} 
					((FrmUserToRole) mView).DgrRoles.DataSource = mDSRole;
					((FrmUserToRole) mView).DgrRoles.DataMember = USERTOROLE_TABLE;	
					((FrmUserToRole) mView).BtnDeleteAssignment.Enabled = true;
				}
			}
			catch (System.Data.OracleClient.OracleException oraex )
			{
				throw new UIFatalException(MessageSingleton.GetInstance().GetMessage
					(MessageSingleton.FATAL_DB_ERROR) 
					+ oraex.Message 
					+ " Objekt: " + this.ToString() );
			}
			catch (DbAccessException odr)
			{
				throw new UIFatalException(MessageSingleton.GetInstance().GetMessage
					(MessageSingleton.FATAL_DB_ERROR)
					+ odr.Message
					+ " Objekt: " + this.ToString() );
			}
		}

		/// <summary>
		/// Load individual role row by getting PK of datarow so its assignment can be deleted
		/// The roles themselves are never edited
		/// </summary>
		internal void LoadIndividualRoleDataSet()
		{		
			// Ask View for the PK ID of the current row
			mCurrentUMRolePKID	  = ((FrmUserToRole) mView).CurrentUMRoleID;
			mCurrentUMUserPKID	  = this.mBOUser.UMPKIdentifier;
			mCurrentFPASSUserPKID = this.mBOUser.FPASSPKIdentifier;

			object[] arrKeys = new object[] {mCurrentUMRolePKID, mCurrentUMUserPKID};	

			mDSRole.UM_ROLELINK.PrimaryKey = new DataColumn[] { mDSRole.UM_ROLELINK.RL_ROLEIDColumn, mDSRole.UM_ROLELINK.RL_AUTHORIZEDENTITYIDColumn };
			mCurrentRoleRow = mDSRole.UM_ROLELINK.Rows.Find(arrKeys);
		}

		/// <summary>
		/// Assign new role (selected from combobox) to user by adding a new datarow to the dataset
		/// UM_ROLELINK is updated via DataAdaptor, the Insert & delete statements in the datadaptor are different from the SELECT
		/// throws <exception cref="de.pta.Component.Errorhandling.UIWarningException">UIWarningException</exception> 
		/// if nothig selected in combobox
		/// </summary>
		internal void AssignNewRoleToUser()
		{
			string strUMRoleID;
			string insCurrRoleNameDesc;

			if (null != ((FrmUserToRole) mView).CboAssignRole.SelectedValue)
			{
                strUMRoleID = GetSelectedIDFromCbo(((FrmUserToRole)mView).CboAssignRole);
                insCurrRoleNameDesc = GetSelectedValueFromCbo(((FrmUserToRole)mView).CboAssignRole);

				if (strUMRoleID.Equals(String.Empty) || insCurrRoleNameDesc.Equals(String.Empty))
				{
					throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.NO_ROLE_ROW ));
				}
			} 
			else 
			{
				throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.NO_ROLE_ROW ));
			}
	
			mCurrentUMRolePKID = Convert.ToInt32(strUMRoleID);
			mCurrentUMUserPKID  = mBOUser.UMPKIdentifier;

			if ( null != mDSRole && null != mDataAdapterUserToRole)
			{
				try
				{	
					this.mDSRole.UM_ROLELINK.AddUM_ROLELINKRow(mCurrentUMRolePKID,
						mCurrentUMUserPKID,
						UserManagementControl.getInstance().CurrentUserID,
						DateTime.Now,
						insCurrRoleNameDesc,
						insCurrRoleNameDesc);
					this.mDataAdapterUserToRole.Update(mDSRole);
					this.mDSRole.AcceptChanges();
									
					((FrmUserToRole) mView).DgrRoles.DataBindings.Clear();
					((FrmUserToRole) mView).DgrRoles.DataSource = mDSRole;
					((FrmUserToRole) mView).DgrRoles.DataMember = USERTOROLE_TABLE;	
					((FrmUserToRole) mView).BtnDeleteAssignment.Enabled = true;
					((FrmUserToRole) mView).CboAssignRole.SelectedIndex = 0;
					this.SetCurrentRoleIDToDefault();
				}
				catch (System.Data.ConstraintException)
				{
					throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.ROLE_ASSIGN_DUPL) );
				}	
				catch ( System.Data.OracleClient.OracleException oraex)
				{
					throw new UIFatalException( MessageSingleton.GetInstance().GetMessage
						(MessageSingleton.FATAL_DB_ERROR) 
						+ oraex.Message 
						+ " Objekt: " + this.ToString() );
				}
			}
		}


		/// <summary>
		/// Delete assigment of role to user
		/// The assignment of role "Plant Manager" can only be deleted in FrmUser by removing all plants
		/// If role is "Koordinator" all dependent coworkers for each external contractor have to be reassigned to an
		/// alternative coordinator for the given excontractor
		/// If the coordinator is assigned to an excontractor but has no coworkers assigned, the assignment coord-exco is 
		/// deleted in the stored proc here
		/// </summary>
		internal void DeleteRoleFromUser()
		{
			bool flg_CoordDep    = true;
			IDbTransaction trans = null;
			IDbCommand dummyComm = null;
			bool flgSuccess      = false;

			// Get ID and name of role currently selected in grid plus current userid
			mCurrentUMRolePKID = ((FrmUserToRole) mView).CurrentUMRoleID;
			mCurrentUMUserPKID = mBOUser.UMPKIdentifier;
			mCurrentRoleName   = ((FrmUserToRole) mView).CurrentRoleName;

            if (mCurrentRoleName.Trim() == UserManagementControl.ROLE_BETRIEBSMEISTER)
			{
                throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.REVOKE_PLANTMANAGER));
			}
			else if (null != mCurrentRoleRow)
			{							
				try
				{
					// Open transaction
					mDataAdapterUserToRole.DeleteCommand.Connection.Open();
					trans = mProvider.GetTransaction(mDataAdapterUserToRole);
					mDataAdapterUserToRole.DeleteCommand.Transaction = trans;

					dummyComm  = mProvider.CreateCommand("SequenceDummy");
					dummyComm.Connection = mDataAdapterUserToRole.DeleteCommand.Connection;
					dummyComm.Transaction = trans;

					if (mCurrentRoleName.Equals(UserManagementControl.ROLE_KOORDINATOR))
					{
						// If current role is "Coordinator, the coworkers assigned to this user must be reassigned.
						/// get coordinator id
						mCurrentCoordID = RoleCoordinatorSingleton.GetInstance().GetCurrentCoordID(mCurrentFPASSUserPKID);

						httAlternativeCoordsByEXCO = new Hashtable();
						httAlternativeCoordsByEXCO = RoleCoordinatorSingleton.GetInstance().GetAlternativeCoordinators(mCurrentCoordID);
						
						if ( null == httAlternativeCoordsByEXCO )
						{
							// the coordinator has no dependent coworkers: ok to delete
							flg_CoordDep = false;
						}

						// If user is a coordinator, assign dependent coworkers to other coordinator
						if ( flg_CoordDep && null != httAlternativeCoordsByEXCO )
						{
							foreach ( object obj in httAlternativeCoordsByEXCO.Keys )
							{
								decimal k     = Convert.ToDecimal(obj);
								// If entry in hashtable contains an arraylist of cooeindators, then ok
								object objEntry = httAlternativeCoordsByEXCO[k];

								// If entry contains a string (name of exoc to which there are no other coords assigned), error
								if ( objEntry.GetType().ToString().Equals("System.String") )
								{
									((FrmUserToRole) mView).Cursor = System.Windows.Forms.Cursors.Default;
									throw new UIWarningException ( 
										MessageSingleton.GetInstance().GetMessage(MessageSingleton.COORD_DEP_CWR_ROLE) 
										+ objEntry.ToString().Trim()
										+ " vorliegen." );
								}
							}

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
					}
				
					// Update dataset
					mCurrentRoleRow.Delete();
					// The record in um_rolelink is deleted via stored procedure rather than thru the data adaptor
					// since the assigment of role Coordinator must be archived 17.11.03
					IDbCommand cmdCallSP = mProvider.CreateCommand("SequenceDummy");
					
					cmdCallSP.CommandText = SP_ARCH_ROLELINK
											+ "( " 
											+ mCurrentUMRolePKID
											+ mCurrentUMUserPKID
											+ ", " 
											+ UserManagementControl.getInstance().CurrentUserID
											+ ")";
				
					cmdCallSP.CommandType = System.Data.CommandType.StoredProcedure;
					cmdCallSP.Connection  = dummyComm.Connection;
					cmdCallSP.Transaction = trans;
					int ret = cmdCallSP.ExecuteNonQuery();

					this.mDSRole.AcceptChanges();
					trans.Commit();
					dummyComm.Connection.Close();
					mDataAdapterUserToRole.DeleteCommand.Connection.Close();

                    // Update in ZKS
                    // No longer required
                    //// If user was a coordinator, remove transaction and export to ZKS
                    //if (mCurrentRoleName.Equals(UserManagementControl.ROLE_KOORDINATOR) && flgSuccess)
                    //{
                    //    try
                    //    {
                    //        trans.Dispose();
                    //        base.ExportAllToZKS();
                    //    }
                    //    catch ( UIWarningException )
                    //    {
                    //        // do nothing
                    //    }
                    //}
					this.SetCurrentRoleIDToDefault();	
		
				}
				catch (UIWarningException uwe)
				{
					if (trans != null && dummyComm != null)
					{
						trans.Rollback();
						dummyComm.Connection.Close();
					}
					throw new UIWarningException( uwe.Message );
				}	
				catch (DBConcurrencyException)
				{
					if (trans != null && dummyComm != null)
					{
						trans.Rollback();
						dummyComm.Connection.Close();
					}
					throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.ADMIN_UPDATE_CONFL ));
				}	
				catch ( System.Data.OracleClient.OracleException oraex)
				{
					if (trans != null && dummyComm != null)
					{
						trans.Rollback();
						dummyComm.Connection.Close();
					}
					throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR) + oraex.Message);
				}
			}
		}

		#endregion Methods

	}
}

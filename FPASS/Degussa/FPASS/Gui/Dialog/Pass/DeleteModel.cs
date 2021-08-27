using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Windows.Forms;

using de.pta.Component.DbAccess;
using de.pta.Component.ListOfValues;
using de.pta.Component.Errorhandling;

using Degussa.FPASS.Util;
using Degussa.FPASS.Util.ListOfValues;
using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.Util.Exceptions;
using Degussa.FPASS.Util.UserManagement;
using Degussa.FPASS.Bo.Pass;
using Degussa.FPASS.Db;
using System.Data.OracleClient;
using Degussa.FPASS.Util.Enums;
using System.Collections.Generic;
using Degussa.FPASS.Bo.Search;

namespace Degussa.FPASS.Gui.Dialog.Pass
{
	/// <summary>
	/// A DeleteModel is the model of the MVC-triad DeleteModel,
	/// DeleteController and FrmCoWorkerDelete.
	/// DeleteModel extends from the FPASSBaseModel.
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
	public class DeleteModel : FPASSBaseModel
	{
		#region Members
		
		/// <summary>
		/// Constants: names of database commands and Stati
		/// </summary>
		private const string COWORKER_QUERY		    = "CoWorkerDelete";
		private const string SP_VALIDUNTIL_UPDATE	= "SP_EXTENDACCESS";
		private const string ACCESS_TYPE_LONG	    = Globals.ACESSLONGTEXT; 

		/// <summary>
		/// Database objects
		/// </summary>
		IDbCommand  updComm;
		IDbCommand  dummyComm;
		IDataReader	dummyReader = null;
		/// <summary>
		/// hold the search parameters from gui, also current user and mandator
		/// </summary>
		private string		 mSurnameParameter;
		private string		 mFirstnameParameter;
		private string   	 mCoordinatorIDParameter;
		private string		 mSubcontractorParameter;
		private string		 mSupervisorParameter;
		private string		 mContractorParameter;
		private int			 mUserID     = UserManagementControl.getInstance().CurrentUserID;
		private int			 mMandatorID = UserManagementControl.getInstance().CurrentMandatorID;
		
		/// <summary>
		/// Holds all coworkers
		/// </summary>
		private Hashtable mCoworkerDict;
		
		/// <summary>
		/// Used to check if entire selection successfully updated
		/// </summary>
		private bool mAllSuccess = true;
		/// <summary>
		/// StringBuilder holds names of coworkers who could not be extended
		/// </summary>
		private StringBuilder mSbErrors;
		private string		  mErrorMess = "Der Zutritt konnte für folgende Mitarbeiter nicht verlängert werden: \n"; 

		private bool mAutoSearch = true;

        /// strongly-typed View instance belonging to this Model
        /// </summary>
        FrmCoWorkerDelete mViewDel;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public DeleteModel()
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
		}	

		#endregion //End of Initialization

		#region Accessors 

		internal bool AutoSearch 
		{
			get 
			{
				return mAutoSearch;
			}
			set 
			{
				mAutoSearch = value;
			}
		} 

		/// <summary>
		/// 25.05.04: to access hashtable containing CWR records
		/// </summary>
		internal Hashtable HashCoWorkers
		{
			get
			{
				return mCoworkerDict;
			}
		}

		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// PreShow called when mask loaded: 
		/// search is automatically executed and shows all invalid coworkers
		/// and those nearing expiry date (user parameter for this)
		/// 03.03.2004: Cannot search for strings with ' in as this is part of SQL string delimiter: catch ORA exception to be sure
		/// mAutoSearch set to false when first search by button in GUI, 
		/// otherwise MsgBox "Suchkritereien" when form is refreshed after editing FFMA
		/// </summary>
		internal override void PreShow()
		{
            mViewDel = (FrmCoWorkerDelete)mView;
			ClearStatusBar();

            mViewDel.BtnAccessAuthorization.Enabled = false;
            mViewDel.BtnCoWorkerDetails.Enabled = false;
            mViewDel.BtnDeleteChoice.Enabled = false;
            mViewDel.BtnDeleteSummary.Enabled = false;
			
			if ( mAutoSearch ) 
			{
				try 
				{
					this.GetCoWorkers();
				} 
				catch ( UIWarningException uwe ) 
				{
					ExceptionProcessor.GetInstance().Process(uwe);
				}
				catch (System.Data.OracleClient.OracleException oraex)
				{
					// Error from SQL ' delimiter
					if ( oraex.Code == 01756 )
					{
						throw new UIWarningException (MessageSingleton.GetInstance().GetMessage
							(MessageSingleton.CANNOT_SEARCH_CHAR));
					}
					else
					{
						throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(
							MessageSingleton.FATAL_DB_ERROR) 
							+ oraex.Message );
					}
				}
			}
            mViewDel.Cursor = System.Windows.Forms.Cursors.Default;
		}


		/// <summary>
		/// Called as MVC is closed
		/// Set AutoSearch to true so search is executed by next opening
		/// </summary>
		internal override void PreClose()
		{
			((FrmCoWorkerDelete) mView).DgrDeleteCoWorker.DataSource = null;
			this.ClearStatusBar();
			this.ClearFields();
			mAutoSearch = true;
			this.ClearStatusBar();
		}

		
		/// <summary>
		/// Empty contents of form
		/// 16.03.04: took out disabling of datagrid, scrollbar error (Paint event asks how many vis rows). (Bug .NET)
		/// </summary>
		internal void ClearFields()
		{
            mViewDel = (FrmCoWorkerDelete)mView;
			ClearStatusBar();
			mViewDel.Cbocoordinator.SelectedValue        = 0;
			mViewDel.CboExternalContractor.SelectedValue = 0;
			mViewDel.CboSearchSupervisor.SelectedValue   = 0;
			mViewDel.CboSubcontractor.SelectedValue      = 0;
			mViewDel.TxtFirstname.Text = String.Empty;
			mViewDel.TxtSurname.Text = String.Empty;

			mViewDel.DgrDeleteCoWorker.DataSource = null;
		}


		#region ExtendValidUntil	
		
		/// <summary>
		/// Extends and calculates new validUNTIL date of each coworker selected in datagrid.
		/// Logic for this is in database stored procedure.
		/// If database returns an error for any coworker continue with processing but show warning at end
		/// </summary>
		internal void ExtendValidUntil()
		{
            mViewDel = (FrmCoWorkerDelete)mView;
			mAllSuccess = true;
			mSbErrors   = new StringBuilder();
			
			for (int j = 0; j < this.mCoworkerDict.Count; j++)
			{
                if (mViewDel.DgrDeleteCoWorker.IsSelected(j))
				{
                    int currCwrID = Convert.ToInt32(mViewDel.DgrDeleteCoWorker[j, 0].ToString());

					UpdateAccess(currCwrID);
                    mViewDel.DgrDeleteCoWorker.UnSelect(j);
				}
			}
			if (!mAllSuccess)
			{
				throw new UIWarningException(mErrorMess + mSbErrors.ToString());
			}
		}

		/// <summary>
		/// Extends access of coworker with the given ID
		/// If a coworker does not have access type LONG or if the excontractor is valid, or if site security or Reception explicitly took away access
		/// then database returns an error. Else update coworker in ZKS
		/// </summary>
		/// <exception cref="de.pta.Component.Errorhandling.UIWarningException">UIWarningException if database reutrns an error for any coworker</exception>
		/// <param name="currCwrID">PK of current coworker</param>
		private void UpdateAccess(int currCwrID) 
		{					
			try
			{			
				// Get DataProvider from DbAccess component, Open dummy datareader to use its connection
				IProvider mProvider = DBSingleton.GetInstance().DataProvider;
				dummyComm			= mProvider.CreateCommand("SequenceDummy");
				dummyReader			= mProvider.GetReader(dummyComm);	

				updComm				= mProvider.CreateCommand("SequenceDummy");
				updComm.CommandType = CommandType.StoredProcedure;
				updComm.CommandText = SP_VALIDUNTIL_UPDATE
									+ "( "
									+ currCwrID.ToString()
									+ ", "
									+  mUserID.ToString()
									+ ", "
									+ mMandatorID.ToString()
									+ ")";
									
				updComm.Connection = dummyComm.Connection;			
				updComm.ExecuteNonQuery();

				this.ExportCwrToZKS(currCwrID);
			}
			catch (System.Data.OracleClient.OracleException oraex)
			{				
				// Is this the exception when the excontractor is invalid, 
				// when the cwr access type is not 'long' or when site security or Reception explicitly denied access?
				if ( oraex.Code == 20006 || oraex.Code == 20007 || oraex.Code == 20008 || oraex.Code == 20009 )
				{
					int    endOF   = oraex.Message.IndexOf("MEND");
					string cwrMess = oraex.Message.Substring(10, endOF-10);
					mSbErrors.Append( cwrMess + "\n" );
					mAllSuccess = false;
				}
				else if ( oraex.Code == 20001)
					
				{
					// If stored function CALCVALIDUNTIL threw error, interpret this as data conflict (eg cwr was deleted)
					throw new UIWarningException ( MessageSingleton.GetInstance().GetMessage(
						MessageSingleton.ADMIN_UPDATE_CONFL) );
				}
				else
				{
					throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(
						MessageSingleton.FATAL_DB_ERROR) 
						+ oraex.Message );
				}
			}			
		}

		#endregion // End of ExtendValidUntil

		#region DeleteCoworker

		/// <summary>
		/// Deletes all coworkers currently displayed in the Form 
        /// (or only selected ones, depending on parameter), 
        /// whereby coworkers with status Valid cannot be deleted:
		/// only status Altdaten (no ZKS bookings for >1 year) Invalid can be deleted.
        /// Produces an error message if CWR has not given back his respiratory mask.
        /// This currently uses Exceptios. TODO in future release: enhance DBAccess component 
        /// to allow the use of in/out parameters.
		/// </summary>
		internal void DeleteCoworkers(bool pSelectedOnly)
		{
			mViewDel = (FrmCoWorkerDelete)mView;
            string cwrId;
            string cwrStatus;
            CoWorkerSearch boCwr;

            // List of coworkers to delete
            List<CoWorkerSearch> cwrBoList = new List<CoWorkerSearch>();

            // Do we have error messages?
            bool hasErrors = false;
            // Holds error messages
            mSbErrors = new StringBuilder();

            string mndId = UserManagementControl.getInstance().CurrentMandatorID.ToString();

			ClearStatusBar();
            mViewDel.Cursor = System.Windows.Forms.Cursors.WaitCursor;

			try
			{
                // 1. First loop through all selected Cwrs in the datagrid to get a list of coworkers for delete.
                // Note that ValueObject is used as the SmartAct number is required for checking if delete record required for SmartAct (see 2. below)
                for (int j = 0; j < mCoworkerDict.Count; j++)
                {

                    if (pSelectedOnly && !mViewDel.DgrDeleteCoWorker.IsSelected(j))
                    {
                        // If routine should only delete selected CWRs then exit loop if current CWR is not selected
                        continue;
                    }
                    
                    cwrId = mViewDel.DgrDeleteCoWorker[j, 0].ToString();
                    boCwr = (CoWorkerSearch)mCoworkerDict[Convert.ToDecimal(cwrId)];
                    cwrStatus = boCwr.Status.ToUpper();

                    if (cwrStatus.Equals(Globals.STATUS_VALID))
                    {
                        // CWR ist still valid, add to error message.
                        hasErrors = true;
                        mSbErrors.Append(MessageSingleton.GetInstance().GetMessage(MessageSingleton.CWR_DELETE_STATUS_ERR) + "\n");
                    }
                    else
                    {
                        // Add Cwr Id to Id list to delete
                        cwrBoList.Add(boCwr);
                    }
                    mViewDel.DgrDeleteCoWorker.UnSelect(j);
                }	



                // 2. Assuming there are 1 or more coworkers to delete:
                // execute stored procedure for each Id
                if (cwrBoList.Count > 0)
                {
                    // One transaction for all selected coworkers
                    StartTransaction();
   
                    // Loops over all coworker BOs in the current list
                    foreach (CoWorkerSearch cwrDelete in cwrBoList)
                    {
                        try
                        {
                            // Get cwrId from current BO
                            cwrId = cwrDelete.CoWorkerId.ToString();

                            // Calls procedure to delete/archice current Cwr
                            ArchiveCwrDB(cwrId, false);

                            // Looks inside current cwr BO for SmartAct number. 
                            // If this is not null then Cwr is in SmartACt, so create a DELETE record in the export file
                            if (cwrDelete.SmartActNo == Globals.DB_YES)
                            {
                                // Creates DELETE record for SmartAct
                                ExportToSmartAct(cwrId, SmartActActions.Delete, false);
                            }

                        }
                        catch (OracleException oraex)
                        {
                            // Not an especially elegant way of doing it as bound to an Oracle exception
                            // and using exceptions to control program flow but DbAccess component does not allow
                            // in/out parameters to be created and assigned to a strored proc. 
                            // TODO in future release: change view to get flag for "has resp mask"
                            // Exception is caught inside its own try-catch block so procedure carries on.
                            if (oraex.Code == 20002)
                            {
                                // 20002 is the code for when respiratory mask not returned

                                // Tody up error message: ged rid of message from PL/SQL
                                int ed = oraex.Message.IndexOf("ORA-06512");
                                string cwrMess = oraex.Message.Substring(0, ed);
                                cwrMess = cwrMess.Replace("ORA-20002: ", "");
                                mSbErrors.Append("\n" + cwrMess);
                                hasErrors = true;
                            }
                            else
                            {
                                // In all other cases abort.
                                throw oraex;
                            }
                        }

                    }	// end of for loop	

                    
                    CommitTransaction();
                    ShowMessageInStatusBar(MessageSingleton.GetInstance().GetMessage(MessageSingleton.CWR_DELETE_SUCCESS));
                }

                if (hasErrors)
                    {
                        throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.CWR_DELETE_ERR) + mSbErrors.ToString());
                    }
			}
			catch (System.Data.OracleClient.OracleException gex)
			{
                throw new UIErrorException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.CWR_DELETE_ERR) + gex.Message);
			}
            mViewDel.Cursor = System.Windows.Forms.Cursors.Default;
		}


		#endregion 

		#region CoWorkerDeleteSearch
	
		/// <summary>
		/// Sets search parameters by reading values from GUI
		/// <exception cref="de.pta.Component.Errorhandling.UIWarningException">UIWarningException 
		/// when no criteria chosen</exception>
		/// </summary>
		private void CopyOutSearchCriteria() 
		{
			int			noSearchCriteria = 0;

			this.mSurnameParameter = ((FrmCoWorkerDelete) mView).TxtSurname.Text.Trim().Replace("*", "%");
			if ( mSurnameParameter.Length < 1 ) 
			{
				this.mSurnameParameter = "%";
				noSearchCriteria ++;
			}

			this.mFirstnameParameter = ((FrmCoWorkerDelete) mView).TxtFirstname.Text.Trim().Replace("*", "%");
			if ( mFirstnameParameter.Length < 1 ) 
			{
				noSearchCriteria ++;
			}

			mCoordinatorIDParameter = this.GetSelectedIDFromCbo(((FrmCoWorkerDelete) mView).Cbocoordinator).Trim();
			if ( mCoordinatorIDParameter.Length < 1 ) 
			{
				noSearchCriteria ++;
			}

			mContractorParameter = this.GetSelectedValueFromCbo(((FrmCoWorkerDelete) mView).CboExternalContractor).Trim();
			if ( mContractorParameter.Length < 1 )
			{
				noSearchCriteria ++;
			}

			mSubcontractorParameter = this.GetSelectedValueFromCbo(((FrmCoWorkerDelete) mView).CboSubcontractor).Trim();
			if ( mSubcontractorParameter.Length < 1 )
			{
				noSearchCriteria ++;
			}

			mSupervisorParameter = this.GetSelectedIDFromCbo( ((FrmCoWorkerDelete) mView).CboSearchSupervisor );
			if ( mSupervisorParameter.Length < 1 )
			{
				noSearchCriteria ++;
			}

			if ( noSearchCriteria == 6 && ! mAutoSearch ) 
			{
				((FrmCoWorkerDelete) mView).Cursor = System.Windows.Forms.Cursors.WaitCursor;
				throw new UIWarningException(MessageSingleton.GetInstance().GetMessage
					(MessageSingleton.NO_SEARCH_CRITERIA));
			} 
		}

		
		/// <summary>
		/// Gets coworker records from database and display in datatable bound to datagrid
		/// Need to assign dummy datasource to datagrid as not properly emptied (Bug .NET?)
		/// The UIWarningException is used to show a message when search returned no records
		/// 24.02.2004: DataTable Column DateOfBirth and ValidUNTIL changed to type DateTime so can be sorted chronologically (default String)
		/// </summary>
		internal void GetCoWorkers() 
		{
            mViewDel = (FrmCoWorkerDelete)mView;
			mViewDel.DgrDeleteCoWorker.DataSource = null;

			// Read search criteria from gui
			CopyOutSearchCriteria();

		     // Create select command & fill Data Reader 
			IProvider mProvider = DBSingleton.GetInstance().DataProvider;		
			IDbCommand selComm  = mProvider.CreateCommand(COWORKER_QUERY);
			selComm.CommandText = GenerateWhereClause( selComm.CommandText );

			// Instantiate hashtable in order to identify CoWorker later by PK
			mCoworkerDict = new Hashtable();

			// Create a datatable at runtime: this is bound to datagrid to allow sorting (name "RTTabCoWorker")
			DataRow row;
			DataTable table = new DataTable("RTTabCoWorker");
            table.Columns.Add(new DataColumn(CoWorkerSearch.ID_COL));
            table.Columns.Add(new DataColumn(CoWorkerSearch.SURNAME));
            table.Columns.Add(new DataColumn(CoWorkerSearch.FIRSTNAME));
            table.Columns.Add(new DataColumn(CoWorkerSearch.DATE_OF_BIRTH, typeof(System.DateTime)));
            table.Columns.Add(new DataColumn(CoWorkerSearch.VALID_UNTIL, typeof(System.DateTime)));
            table.Columns.Add(new DataColumn(CoWorkerSearch.EXCONTRACTOR));
            table.Columns.Add(new DataColumn(CoWorkerSearch.SMARTACT_NO));
            table.Columns.Add(new DataColumn(CoWorkerSearch.SUBCONTRACTOR));
            table.Columns.Add(new DataColumn(CoWorkerSearch.COORDINATOR));
            table.Columns.Add(new DataColumn(CoWorkerSearch.STATUS));
            table.Columns.Add(new DataColumn(CoWorkerSearch.ACCESS));

           
			IDataReader mDR = mProvider.GetReader(selComm);
		
			while (mDR.Read())
			{
				mCoWorkerBO = new CoWorkerSearch();
				row = table.NewRow();

				mCoWorkerBO.CoWorkerId = Convert.ToDecimal(mDR["CWR_ID"]);
				mCoWorkerBO.Surname	= mDR["CWR_SURNAME"].ToString();
				mCoWorkerBO.Firstname = mDR["CWR_FIRSTNAME"].ToString();
				mCoWorkerBO.DateOfBirth = Convert.ToDateTime(mDR["CWR_DATEOFBIRTH"]).Date.ToString("dd.MM.yyyy");
				mCoWorkerBO.ValidUntil= Convert.ToDateTime(mDR["CWR_VALIDUNTIL"]).Date.ToString("dd.MM.yyyy");
				mCoWorkerBO.ExContractorName = mDR["EXTCON"].ToString();
				mCoWorkerBO.Supervisor = mDR["SUPERNAME"].ToString();
				mCoWorkerBO.SupervisTel = mDR["SUPERTEL"].ToString();
				mCoWorkerBO.SubContractor = mDR["SUBCON"].ToString();
				mCoWorkerBO.Coordinator = mDR["VWC_SURNAME"].ToString();
				mCoWorkerBO.CoordTel = mDR["VWC_TEL"].ToString();
				mCoWorkerBO.Status = mDR["CWR_STATUS"].ToString();
				mCoWorkerBO.Access = mDR["CWR_ACCESS"].ToString();

                // If SmartAct nr is there then show "Yes"
                mCoWorkerBO.SmartActNo = (mDR["CWR_SMARTACTNO"].Equals(DBNull.Value) ? Globals.DB_NO : Globals.DB_YES);

                //mCoWorkerBO.SuperNameAndTel = mCoWorkerBO.Supervisor 
                //    + "  (Tel. " + mCoWorkerBO.SupervisTel + ")";

				mCoWorkerBO.CoordNameAndTel = mCoWorkerBO.Coordinator 
					+ "  (Tel. " + mCoWorkerBO.CoordTel + ")";
					
				mCoworkerDict.Add(mCoWorkerBO.CoWorkerId, mCoWorkerBO);	
		
				// Add row to data table
				row.ItemArray = new object[11] { mCoWorkerBO.CoWorkerId,
												   mCoWorkerBO.Surname,
												   mCoWorkerBO.Firstname,
												   mCoWorkerBO.DateOfBirth,
												   mCoWorkerBO.ValidUntil,
												   mCoWorkerBO.ExContractorName,
												   mCoWorkerBO.SmartActNo,		
												   mCoWorkerBO.SubContractor, 
												   mCoWorkerBO.CoordNameAndTel, 
												   mCoWorkerBO.Status,
												   mCoWorkerBO.Access };
				table.Rows.Add(row);
			}
			mDR.Close();

						
			// Bind data grid in Form to the datatable
			if (mCoworkerDict.Count > 0) 
			{	
				mViewDel.DgrDeleteCoWorker.DataSource = table;
				ShowMessageInStatusBar("Meldung: " + mCoworkerDict.Count + " Fremdfirmenmitarbeiter gefunden");
			} 
			else 
			{
				// No rows returned
				mViewDel.CurrentRowIndex = -1;
				ShowMessageInStatusBar(MessageSingleton.GetInstance().GetMessage (MessageSingleton.NO_RESULTS));
			}
            mViewDel.BtnDeleteSummary.Enabled = (mCoworkerDict.Count > 0);
            mViewDel.BtnDeleteChoice.Enabled = (mCoworkerDict.Count > 0);
            mViewDel.BtnCoWorkerDetails.Enabled = (mCoworkerDict.Count > 0);
            mViewDel.BtnAccessAuthorization.Enabled = (mCoworkerDict.Count > 0);
		}


		/// <summary>
		/// Dynamically generate WHERE clause of SQL statement to get CoWorker records.
		/// The more values selected in the GUI the more criteria are appended to the SQL string
		/// If the current user is a coordinator he only gets to see his own coworkers
		/// 25.05.04: Sort results by excontracotr name, coworker surname and firstname
		/// </summary>
		/// <param name="pSelect">the original SELECT statement</param>
		/// <returns>SELECT statement + complete WHERE clause + ORDER BY</returns>
		private String GenerateWhereClause(String pSelect) 
		{
			String	whereClause = "";

			whereClause = " WHERE CWR_MND_ID = " + UserManagementControl.getInstance().CurrentMandatorID; 

			if ( this.mCoordinatorIDParameter.Length > 0 ) 
			{
				whereClause = whereClause + " AND VWC_ID = " +  this.mCoordinatorIDParameter;
			}

			if ( this.mContractorParameter.Length > 0 ) 
			{
				whereClause = whereClause + " AND EXTCON = '" + this.mContractorParameter + "' "; 
			}

			if ( this.mSurnameParameter.Length > 0 ) 
			{
				whereClause = whereClause + " AND UPPER (CWR_SURNAME) LIKE '" + this.mSurnameParameter.ToUpper() + "' "; 
			}

			if ( this.mFirstnameParameter.Length > 0 ) 
			{
				whereClause = whereClause + " AND UPPER (CWR_FIRSTNAME) LIKE '" + this.mFirstnameParameter.ToUpper() + "' "; 
			}

			if ( this.mSupervisorParameter.Length > 0 ) 
			{
				whereClause = whereClause + " AND CWR_EXCO_ID = " + this.mSupervisorParameter;
			}

			if ( this.mSubcontractorParameter.Length > 0 ) 
			{
				whereClause = whereClause + " AND SUBCON = '" + this.mSubcontractorParameter + "' "; 
			}

			if ( UserManagementControl.getInstance().CurrentUserIsInRole(
				UserManagementControl.ROLE_KOORDINATOR) &&  ! 
				UserManagementControl.getInstance().CurrentUserIsInRole(
				UserManagementControl.ROLE_EDVADMIN) && !
				UserManagementControl.getInstance().CurrentUserIsInRole(
				UserManagementControl.ROLE_VERWALTUNG) ) 
			{
				whereClause = whereClause + " AND (CWR_EXCO_ID IN (SELECT ECEC_EXCO_ID"
					+ " FROM FPASS_EXCOECOD WHERE ECEC_ECOD_ID IN (SELECT ECOD_ID "
					+ " FROM FPASS_EXCOCOORDINATOR WHERE ECOD_USER_ID = '"					
					+   UserManagementControl.getInstance().CurrentUserID + "'))"
					+ " OR CWR_SUBE_ID IN (SELECT ECEC_EXCO_ID"
						+ " FROM FPASS_EXCOECOD WHERE ECEC_ECOD_ID IN (SELECT ECOD_ID "
						+ " FROM FPASS_EXCOCOORDINATOR WHERE ECOD_USER_ID = '"					
						+ UserManagementControl.getInstance().CurrentUserID + "')))"; 
			}
			
			// Add ORDER BY to very end
			whereClause = whereClause + " ORDER BY EXTCON, CWR_SURNAME, CWR_FIRSTNAME";

			return pSelect + whereClause;

		}

		#endregion // End of CoWorkerDelete

		#endregion // End of Methods

	}
}

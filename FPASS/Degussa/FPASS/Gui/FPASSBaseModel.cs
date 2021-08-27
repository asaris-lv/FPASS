using System;
using System.Data;
using System.Collections;
using System.Windows.Forms;

using de.pta.Component.ListOfValues;
using de.pta.Component.Errorhandling;
using de.pta.Component.DbAccess;
using de.pta.Component.DbAccess.Exceptions;

using Degussa.FPASS.InterfaceZks;
using Degussa.FPASS.Db;
using Degussa.FPASS.Util;
using Degussa.FPASS.Util.ListOfValues;
using Degussa.FPASS.Util.UserManagement;
using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.Util.Validation;
using Degussa.FPASS.Reports.Util;
using Degussa.FPASS.Bo.Search;

namespace Degussa.FPASS.Gui
{
	/// <summary>
	/// FPASSBaseModel is the model of the MVC-triad FPASSBaseView,
	/// FPASSBaseModel and FPASSBaseController.
	/// FPASSBaseModel extends from the FPASSBaseModel.
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
	///			<td width="20%">Author</td>
	///			<td width="20%">month/day/year</td>
	///			<td width="60%">Comment</td>
	///		</tr>
	/// </table>
	/// </div>
	/// </remarks>
	public class FPASSBaseModel : AbstractModel
	{
		#region Members

		/// <summary>
		/// Name of SQL statement given in Config.xml: connection of dummy statement is used for transaction
		/// </summary>
		private	const string SEQ_COMMAND = "SequenceDummy";

		/// <summary>
		/// ZKS server
		/// 		/// </summary>
		protected FpassZks mZKSProxy;

		/// <summary>
		/// The Transcation used for dml-statements
		/// </summary>
		private IDbTransaction mDbTransaction;

        /// <summary>
        /// The SQL command object as a holder
        /// </summary>
        private IDbCommand mCommand;

		/// <summary>
		/// The SQL-String used to open a transaction
		/// </summary>
		private string SQL_TRANSACTION = "TransactionDummy";

        /// <summary>
        /// CoWorker value object used to hold coworker search results
        /// </summary>
        protected CoWorkerSearch mCoWorkerBO;

        /// <summary>
        /// for MsgBox
        /// </summary>
        protected string TitleMessage = "FPASS";

        /// <summary>
        /// Name of current report
        /// </summary>
        protected string mReportName;

        /// <summary>
        /// instance of FpassReportParameters, used to generate PDF
        /// </summary>
        protected FpassReportParameters mPDFReportParameters;

        #endregion 

        #region Constructors

        /// <summary>
		/// Standard constructor.
		/// </summary>
		public FPASSBaseModel()
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
			mZKSProxy = new FpassZks();
		}	

		#endregion //End of Initialization

		#region Accessors 

		/// <summary>
		/// simple getter
		/// </summary>
		public IDbTransaction CurrentTransaction 
		{
			get 
			{
				return mDbTransaction;
			}
		}

		#endregion //End of Accessors

		#region Methods 

        #region MethodsZKS

        /// <summary>
		/// Insert all coworker data from FPASS to ZKS if ZKS is installed for the current mandator.
		/// </summary>
		/// <exception cref="de.pta.Component.Errorhandling.UIWarningException">if ZKS returncode not 0 (success)</exception>
		internal virtual void ExportAllToZKS() 
		{
			if ( UserManagementControl.getInstance().CurrentBOUserMndt.HasZKS ) 
			{
				String connect = mZKSProxy.Connect(); 

				if ( connect.Equals(MessageSingleton.GetInstance().GetMessage(MessageSingleton.ZKS_NO_CONNECT) ) ) 
				{
					throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.ZKS_NO_CONNECT) );
				}
				String returncode = mZKSProxy.InsertAll();
				mZKSProxy.Disconnect();
				if ( !returncode.Equals("") ) 
				{
					throw new UIWarningException(returncode);
				}
			}
		}


		/// <summary>
		/// Starts export to ZKS if ZKS is installed for the current mandator.  
		/// Updates FPASS tables if export was successful.
		/// </summary>
		/// <param name="pCoWorkerId">id of the current coworker to export</param>
		protected void ExportCwrToZKS(decimal pCoWorkerId) 
		{
            String returnCode;

            try
            {
                if (UserManagementControl.getInstance().CurrentBOUserMndt.HasZKS)
                {

                    // Builds coworker data string from the Database
                    // Allows us to get ID card numbers and validUntil to check if this coworker should be transferred to ZKS
                    mZKSProxy.GetDataFromDb(pCoWorkerId);

                    if (!mZKSProxy.CoworkerValidZKS())
                    {
                        // Coworker cannot be transferred to ZKS
                        // CWR has incorrect ID card numbers or access is expired
                        // Log a warning but no message to GUI
                        Globals.GetInstance().Log.Warn(mZKSProxy.MessageResult);
                        return;
                    }
                    else
                    {
                        // to be sure
                        UpdateZKSReturnCode(pCoWorkerId, Globals.DB_NO);
                    }

                    // Transfers CWR to ZKS: first open the connection
                    returnCode = mZKSProxy.Connect();

                    if (returnCode == MessageSingleton.GetInstance().GetMessage(MessageSingleton.ZKS_OK))
                    {
                        // Connection to ZKS ok, insert coworker and disconnect
                        returnCode = mZKSProxy.Insert(pCoWorkerId, false);
                        mZKSProxy.Disconnect();

                        if (returnCode.Equals(MessageSingleton.GetInstance().GetMessage(MessageSingleton.ZKS_OK)))
                        {
                            UpdateZKSReturnCode(pCoWorkerId, Globals.DB_YES);
                        }
                        else
                        {
                            UpdateZKSReturnCode(pCoWorkerId, Globals.DB_NO);
                        }
                    }
                    else
                    {
                        // Log warning; no connect to ZKS
                        Globals.GetInstance().Log.Error(MessageSingleton.GetInstance().GetMessage(MessageSingleton.ZKS_NO_CONNECT));
                        // TODO: is this necessary?
                        UpdateZKSReturnCode(pCoWorkerId, Globals.DB_NO);
                    }
                }
            }
            catch (UIWarningException uwe)
            {
                ExceptionProcessor.GetInstance().Process(uwe);
            }
		}

		/// <summary>
		/// Updates returncode from ZKS in database for current coworker
		/// (column in DB table FPASS_COWORKER)
		/// </summary>
		/// <param name="pCoWorkerID"></param>
		/// <param name="pValue"></param>
		/// <exception cref="System.Data.OracleClient.OracleException"></exception>
		/// <exception cref="de.pta.Component.DbAccess.Exceptions.DbAccessException">if database returns error</exception>
		protected void UpdateZKSReturnCode(decimal pCoWorkerID, String pValue) 
		{
			try 
			{
				this.StartTransaction();
				IProvider provider = DBSingleton.GetInstance().DataProvider;

				IDbCommand updateComm  = provider.CreateCommand("UpdateCWRZKS");
				updateComm.Transaction = this.CurrentTransaction;
				updateComm.Connection  = this.CurrentTransaction.Connection;

				provider.SetParameter( updateComm, ":CWR_ID", pCoWorkerID );			
				provider.SetParameter( updateComm, ":CWR_RETURNCODE_ZKS", pValue);
				provider.SetParameter( updateComm, ":CWR_CHANGEUSER", UserManagementControl.
										getInstance().CurrentUserID );
				provider.SetParameter( updateComm, ":CWR_TIMESTAMP", DateTime.Now );

				updateComm.ExecuteNonQuery();

				this.CommitTransaction();
			}
			catch ( System.Data.OracleClient.OracleException oe ) 
			{
				if ( null != this.CurrentTransaction)
				{
					this.RollbackTransaction();
				}
				throw new UIFatalException(MessageSingleton.GetInstance().GetMessage(
											MessageSingleton.FATAL_DB_ERROR)
											+ oe.Message );

			} 
			catch ( DbAccessException dba ) 
			{
				if ( null != this.CurrentTransaction)
				{
					this.RollbackTransaction();
				}
				throw new UIFatalException(MessageSingleton.GetInstance().GetMessage(
											MessageSingleton.FATAL_DB_ERROR)
											+ dba.Message );
			}			 
		}

        #endregion

        #region MethodsSmartAct

        /// <summary>
        /// Exports current Coworker to SmartAct (writes data to CSV file - one line).
        /// Calls the DB stored procedure to do this. 
        /// Note: cwr photo is deleted inside procedure if called with Action <see cref="SmartActMode"/>
        /// </summary>
        /// <param name="pCwrId">Current Cwr Id</param>
        /// <param name="pAction">Action to carry out: U for update for D for delete. If D is given then line prefix set to FPASSDEL</param>
        /// <param name="pCreateTransaction">if set tor true, create and commit transaction for this call. Otherwise assumes transaction is already open</param>
        internal void ExportToSmartAct(string pCwrId, char pAction, bool pCreateTransaction)
        {          
            try
            {
                if (pCreateTransaction)
                {
                    StartTransaction();
                }

                IProvider provider = DBSingleton.GetInstance().DataProvider;

                mCommand.Transaction = this.CurrentTransaction;
                mCommand.Connection = this.CurrentTransaction.Connection;
                mCommand.CommandType = CommandType.StoredProcedure;

                // Execute Stored Proc to create the CSV record for export
                mCommand.CommandText = "SP_INTERFACE_FPASS_TO_SMARTACT( "
                                + pCwrId
                                + ", "
                                + UserManagementControl.getInstance().CurrentMandatorID.ToString()
                                + ", "
                                + "'" + pAction + "'"
                                + " )";

                mCommand.ExecuteNonQuery();

                if (pCreateTransaction)
                {
                    CommitTransaction();
                }
            }
            catch (Exception cex)
            {
                // let FPASS save coworker and provide an error message
                if (pCreateTransaction)
                {
                    RollbackTransaction();
                }

                string errMsg = MessageSingleton.GetInstance().GetMessage(MessageSingleton.SMARTACT_EXP_FPASS_ERR) + cex.Message;
                ExceptionProcessor.GetInstance().Process(new UIErrorException(errMsg, cex));
            }
        }

      
        #endregion

        #region MethodsDeleteCwr

        /// <summary>
        /// Deletes current Coworker by moving all data to archive tables in DB
        /// Calls the DB stored procedure to do this.
        /// </summary>
        /// <param name="pCwrId">Current Cwr Id</param>
        /// <param name="pCreateTransaction">if set tor true, create and commit transaction for this call. Otherwise assumes transaction is already open</param>
        internal void ArchiveCwrDB(string pCwrId, bool pCreateTransaction)
        {
            if (pCreateTransaction)
            {
                StartTransaction();
            }

            mCommand.Transaction = this.CurrentTransaction;
            mCommand.Connection = this.CurrentTransaction.Connection;
            mCommand.CommandType = CommandType.StoredProcedure;

            mCommand.CommandText = String.Format("SP_ARCHIVCWR( {0}, {1})", pCwrId, UserManagementControl.getInstance().CurrentUserID.ToString());
            mCommand.ExecuteNonQuery();

            if (pCreateTransaction)
            {
                CommitTransaction();
            }
        }   
        

        /// <summary>
        /// Deletes coworker details when ID card number is deleted:
        /// access authorisations are revoked, coworker is set to invalid
        /// </summary>
        /// <param name="pCoWorkerID"></param>
		protected void DeleteIdCardDB(decimal pCoWorkerID ) 
		{
			IDbCommand cmdCallSP;
			IProvider  provider;
			
			try 
			{
				this.StartTransaction();
				provider  = DBSingleton.GetInstance().DataProvider;
				cmdCallSP = provider.CreateCommand("SequenceDummy");
							
				cmdCallSP.CommandText  = "SP_DELETEDETAILSIDCARD( " 
											+ pCoWorkerID
											+ ", " 
											+ UserManagementControl.getInstance().CurrentUserID
											+ ")";
			
				cmdCallSP.CommandType = System.Data.CommandType.StoredProcedure;
							
				cmdCallSP.Transaction = this.CurrentTransaction;
				cmdCallSP.Connection  = this.CurrentTransaction.Connection;
			    cmdCallSP.ExecuteNonQuery();

				CommitTransaction();
			}
			catch ( System.Data.OracleClient.OracleException oe ) 
			{
				if ( null != this.CurrentTransaction)
				{
					this.RollbackTransaction();
				}
				throw new UIFatalException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR) + oe.Message);

			} 
			catch ( DbAccessException dba ) 
			{
				if ( null != this.CurrentTransaction)
				{
					this.RollbackTransaction();
				}
				throw new UIFatalException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR) + dba.Message );
			}			 
		}

        #endregion

        #region Methods4Database

        /// <summary>
		/// opens a transaction by executing a dummy transaction statement
		/// </summary>
		protected void StartTransaction() 
		{
			IProvider provider = DBSingleton.GetInstance().DataProvider;
			mCommand = provider.CreateCommand(SQL_TRANSACTION);
			//provider.GetReader(mCommand);
			mDbTransaction = provider.GetTransaction(mCommand);
		}

		/// <summary>
		/// commits the current transaction
		/// </summary>
		protected void CommitTransaction() 
		{
			mDbTransaction.Commit();

			if (mDbTransaction.Connection != null && mDbTransaction.Connection.State == ConnectionState.Open)
			{
				mDbTransaction.Connection.Close();
			}
		}

		/// <summary>
		/// rolls back the current transaction
		/// </summary>
		protected void RollbackTransaction() 
		{
			try 
			{
				mDbTransaction.Rollback();
			} 
			catch ( Exception ) 
			{
				// just swallow it 
			}
		}

        /// <summary>
        /// Gets next value from given database sequence to ensure 
        /// PK value for new database record is unique
        /// (should be a sequence for each FPASS database table)
        /// </summary>
        /// <param name="pSeqName">name of current DB sequence</param>
        /// <returns>next value to be used for PK</returns>
        /// <exception cref="System.Data.OracleClient.OracleException"></exception>
        /// <exception cref="de.pta.Component.DbAccess.Exceptions.DbAccessException">if database returns error</exception>
        internal decimal GetNextValFromSeq(string pSeqName)
        {
            decimal readPKVal = 0;

            IProvider provider = DBSingleton.GetInstance().DataProvider;
            IDbCommand seqComm = provider.CreateCommand(SEQ_COMMAND);
            seqComm.CommandText = "SELECT " + pSeqName + ".nextval FROM dual";

            // Get sequence ID from DataReader
            try
            {
                IDataReader reader = provider.GetReader(seqComm);
                while (reader.Read())
                {
                    readPKVal = reader.GetDecimal(0);
                }
                reader.Close();
                return readPKVal;
            }
            catch (System.Data.OracleClient.OracleException oaer)
            {
                throw new UIFatalException(MessageSingleton.GetInstance().GetMessage(
                    MessageSingleton.FATAL_DB_ERROR) + oaer.Message);
            }

            catch (DbAccessException dae)
            {
                throw new UIFatalException(MessageSingleton.GetInstance().GetMessage(
                    MessageSingleton.FATAL_DB_ERROR) + dae.Message);
            }
        }

        #endregion Methods4Database

        #region MethodsReports

        /// <summary>
		/// Generates PDF report for one coworker.
		/// <param name="prmCoWorkerId">id of current coworker</param>
        /// <param name="prmReportName">name of report to generate (either Pass - Passierchen or MaskTicket- Maskenrückgabebeleg)</param>
		internal void GenerateCWRReport(decimal prmCoWorkerId, string prmReportName)
		{
			try
			{
                // member SearchCriteria of the report singleton must be initialized with an empty SortedList
                // because the report generation needs it
                // (In the report dialog it is initialized by the search that always occur before the generation of the report)
                FpassReportSingleton.GetInstance().SearchCriteria = new SortedList();

                mReportName = prmReportName;

                // intializes report parameters
                mPDFReportParameters = new FpassReportParameters(mReportName, prmCoWorkerId);

                SetReportStandardData();

				// creates pass report with the parameters
                FpassReport pdfPass = new FpassReport(mPDFReportParameters);
				pdfPass.Generate();
			}	
			catch (UIFatalException ufe)
			{
				throw new UIFatalException("", ufe);
			}
			catch (UIWarningException uwe)
			{
				throw new UIWarningException(uwe.Message, uwe);
			}
		}

        /// <summary>
        /// Sets report standard data that are valid for all reports
        /// e.g. report title, user name and department
        /// </summary>
        protected void SetReportStandardData()
        {          
            int userId;
            string userLoginName;
            string userNiceName;
            string mandator;
            string userDepartment;

            IDbCommand selectCommand;
            IDataReader reportDataReader;

            // sets title
            if (mReportName == ReportNames.CWR_NO_BOOKING)
            {
                string xName = mReportName.Replace("x", mPDFReportParameters.SearchCriteria["NoBookXDays"].ToString());
                mPDFReportParameters.StandardValues.Add("ReportTitle", xName);
            }
            else mPDFReportParameters.StandardValues.Add("ReportTitle", mReportName);

            // current mandator
            mandator = UserManagementControl.getInstance().CurrentMandatorName;
            mPDFReportParameters.StandardValues.Add("MND_MANDATOR", mandator);

            // login name of the current user, used in filename of PDF
            userLoginName = UserManagementControl.getInstance().CurrentOSUserName;
            mPDFReportParameters.StandardValues.Add("US_USERID", userLoginName);

            // Nice name of current user, Used for display in report footer
            userNiceName = UserManagementControl.getInstance().CurrentUserNiceName;
            mPDFReportParameters.StandardValues.Add("USERNICENAME", userNiceName);

            // Get department of current user, Get user PK ID
            userId = UserManagementControl.getInstance().CurrentUserID;
            IProvider reportDataProvider = DBSingleton.GetInstance().DataProvider;

            // Create select command for department and add where clause 
            selectCommand = reportDataProvider.CreateCommand("UserDepartmentReports");
            selectCommand.CommandText = selectCommand.CommandText + " WHERE USER_ID = " + userId;

            reportDataReader = reportDataProvider.GetReader(selectCommand);

            if (reportDataReader.Read())
            {
                // the 3rd field of the query is the user department
                userDepartment = reportDataReader.GetString(2);
            }
            else userDepartment = "";

            reportDataReader.Close();
            mPDFReportParameters.StandardValues.Add("DEPT_DEPARTMENT", userDepartment);

            // Get max. hours limit for Attendance reports
            if (mReportName == ReportNames.CWR_ATTEND_DETAIL)
            {
                mPDFReportParameters.StandardValues.Add("CWAP_HOURSLIMIT", Globals.GetInstance().AttendHoursLimit);
            }
        }	


		/// <summary>
		/// Opens a static document saved somewhere on filesystem
		/// </summary>
		/// <param name="pPathAndFilename"></param>
		internal void GeneratePDFDoc(string pPathAndFilename)
		{
			FPASSDocument fDoc = new FPASSDocument();
			fDoc.FullNameAndPath = pPathAndFilename;
			fDoc.ShowPDF();
		}

        #endregion

        #region MethodsValidation

        /// <summary>
        /// Make sure given string is a correct date
        /// </summary>
        /// <param name="pValue"></param>
        internal void CheckDateString(String pValue)
        {
            if (pValue.Length > 0)
            {
                if (!StringValidation.GetInstance().IsDateString(pValue))
                {
                    throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.INVALID_FULLDATE));
                }
            }
        }

        /// <summary>
        /// Ensures string does not contain ' as this is quotation mark in SQL
        /// Could check for other nasties here as well...
        /// </summary>
        /// <param name="pValue"></param>
        internal void CheckSpecialCharacterString(String pValue)
        {
            if (pValue.Length > 0)
            {
                if (-1 < pValue.IndexOf("'"))
                {
                    throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.CANNOT_SEARCH_CHAR));
                }
            }
        }


		/// <summary>
		/// get the selected Value from the given combobox, filled by de.pta.Component.ListOfValue
		/// </summary>
		/// <param name="pComboBox">current combobox</param>
		/// <returns>value part (usually string) of selected item</returns>
		protected String GetSelectedValueFromCbo(ComboBox pComboBox) 
		{
			String text = pComboBox.Text;
			if ( null == text ) 
			{
				text = "";
			}
			return text;
		}

		/// <summary>
		/// Get the selected Id (pk) from the given combobox, filled by de.pta.Component.ListOfValue
		/// </summary>
		/// <param name="pComboBox">current combobox</param>
		/// <returns>ID of selected item</returns>
		protected String GetSelectedIDFromCbo(ComboBox pComboBox) 
		{
			String id = "";
			if ( null != pComboBox.SelectedItem ) 
			{
				String name = pComboBox.SelectedItem.GetType().Name;

				if ( name.Equals("CoordLovItem") ) 
				{
					id = ((CoordLovItem) pComboBox.SelectedItem).CoordID.ToString();
					if (  id.Equals("0") ) 
					{
						id = "";
					}
				}
				if ( name.Equals("ContractorLovItem") ) 
				{
					id = ((ContractorLovItem) pComboBox.SelectedItem).ContractorID.ToString();
					if (  id.Equals("0") ) 
					{
						id = "";
					}
				}
				if ( name.Equals("SupervisorLovItem") ) 
				{
					id = ((SupervisorLovItem) pComboBox.SelectedItem).EXCOID.ToString();
					if (  id.Equals("0") ) 
					{
						id = "";
					}
				}
				if ( name.Equals("LovItem") ) 
				{
					id = ((LovItem) pComboBox.SelectedItem).Id;
					if (  id.Equals("0") ) 
					{
						id = "";
					}
				}
			} 
			else 
			{
				id = "";	
			}
			return id;
		}


		/// <summary>
		/// Get ListOfValue item from current combobox using current LovItem ID
		/// </summary>
		/// <param name="pID">current LovItem ID (usually PK)</param>
		/// <param name="pComboBox">current combobox</param>
		/// <returns>LOVItem</returns>
		protected LovItem GetLovItem(int pID, ComboBox pComboBox) 
		{
			String id = pID.ToString();

			foreach ( LovItem lovitem in (ArrayList)pComboBox.DataSource ) 
			{
				if ( lovitem.Id.Equals(id) ) 
				{
					return lovitem;
				}
			}

			return null;
		}

        /// <summary>
        /// Compares two dates, which is later?
        /// </summary>
        /// <param name="pDate1">1st date to compare</param>
        /// <param name="pDate2">2nd date to compare</param>
        /// <returns>1 if first date later than second date, -1 if first earlier, 0 if same</returns>
        internal int CompareDates(DateTime pDate1, DateTime pDate2)
        {
            int ret = pDate1.Date.CompareTo(pDate2.Date);
            return ret;
        }

        #endregion MethodsValidation

        #region MethodsGUI

        /// <summary>
		/// Writes the given Messages in the status bar in the baseview
		/// Examples of Messages are after a save, this record cannot be edited, etc
		/// </summary>
		internal void ShowMessageInStatusBar(string pMessageTxt) 
		{
			// Check: wot is maximum length of text?
			((FPASSBaseView) mView).StbBase.Panels[2].Text = pMessageTxt;
		
		}

		/// <summary>
		/// Deletes the current text from the status bar in the baseview
		/// </summary>
		internal void ClearStatusBar() 
		{
			// Check: wot is maximum length of text?
			((FPASSBaseView) mView).StbBase.Panels[2].Text = String.Empty;
        }

        #endregion MethodsGUI

        #endregion // End of Methods

    }
}


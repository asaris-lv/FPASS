using System;
using System.Collections;
using System.Data;
using System.Globalization;

using Degussa.FPASS.FPASSApplication;
using Degussa.FPASS.Bo.Administration;
using Degussa.FPASS.Db;
using Degussa.FPASS.Db.DataSets;
using Degussa.FPASS.Gui.Dialog.Administration.UserControls;
using Degussa.FPASS.Gui.Dialog.User;
using Degussa.FPASS.Util;
using Degussa.FPASS.Util.ListOfValues;
using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.Util.Exceptions;
using Degussa.FPASS.Util.UserManagement;
using Degussa.FPASS.Util.Validation;

using de.pta.Component.DbAccess;
using de.pta.Component.ListOfValues;
using de.pta.Component.DbAccess.Exceptions;
using de.pta.Component.Errorhandling;


namespace Degussa.FPASS.Gui.Dialog.Administration
{
	/// <summary>
	/// An AdministrationModel is the model of the 
	/// MVC-triad AdministrationController, AdministrationModel
	/// and FrmAdministration.
	/// AdministrationModel extends from the AbstractModel.
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
	public class AdministrationModel : FPASSBaseModel
	{
		#region Members
		
		/// <summary>
		/// Provider for database access
		/// </summary>
		private IProvider      mProvider;
		/// <summary>
		/// DataSets are used for editing in the Administration UserControls:
		/// each has its own typified DataSet mirroring the underlying DB table	
		/// </summary>
		private DSExContractor mDSExContractor;
		private DSExcoCoord    mDSExcoCoord;
		private DSPrecMedType  mDSPrecMedType;
		private DSPlant		   mDSPlant;
		private DSDepartment   mDSDepartment;
		private DSCraft        mDSCraft;
		/// <summary>
		/// Typified DataRows related to DataSets
		/// </summary>
		private DataRow mCurrentExcontractorRow;
		private DataRow mCurrentExcoCoordRow;
		private DataRow mCurrentPrecMedRow;
		private DataRow mCurrentPlantRow;
		private DataRow mCurrentDeptRow;
		/// <summary>
		/// Craft is implemented differently from the rest: when a new record is created a new DataRow object is produced
		/// </summary>
		private DataRow mNewCraftRow;
		private DataRow mCurrentCraftRow;
		/// <summary>
		/// Data Adaptors used 1:1 with Typified datasets
		/// </summary>
		private IDbDataAdapter mDataAdapterExcontractor;
		private IDbDataAdapter mDataAdapterExcoCoord;
		private IDbDataAdapter mDataAdapterPrecMedical;
		private IDbDataAdapter mDataAdapterPlant;
		private IDbDataAdapter mDataAdapterDept;
		private IDbDataAdapter mDataAdapterCraft;	
		/// <summary>
		/// If data rows have been altered
		/// </summary>
		private const string DS_ROW_UNCHANGED = "Unchanged";
		/// <summary>
		/// DataAdpator IDs as defined in Confioguration.xml
		/// </summary>
		private const string EXCO_ADA_ID    = "DSExContractor";
		private const string EXCOCOORD_ADA_ID = "DSExCoCoordinator";
		private const string PRECMED_ADA_ID = "DSPrecMedicalType";
		private const string PLANT_ADA_ID   = "DSPlant";
		private const string DEPT_ADA_ID    = "DSDepartment";
		private const string CRAFT_ADA_ID   = "DSAdminCraft";
		private const string EXCOCOORD_QUERY="SelectAdminExcoCoord";
		/// <summary>
		/// Tablenames are used in Data Adaptors
		/// </summary>
		private const string EXCO_TABLE    = "FPASS_EXCONTRACTOR";
		private const string ECEC_TABLE	   = "FPASS_EXCOECOD";
		private const string PRECMED_TABLE = "FPASS_PRECMEDTYPE";
		private const string PLANT_TABLE   = "FPASS_PLANT";
		private const string DEPT_TABLE    = "FPASS_DEPARTMENT";
		private const string CRAFT_TABLE   = "FPASS_CRAFT";
		/// <summary>
		/// Name of Primary Keys used to get current row off dataset
		/// </summary>
		private const string EXCO_PK_COL    = "EXCO_ID";
		private const string PRECMED_PK_COL = "PMTY_ID";
		private const string PLANT_PK_COL	= "PL_ID";
		private const string DEPT_PK_COL	= "DEPT_ID";
		private const string CRAFT_PK_COL	= "CRA_ID";
		/// <summary>
		/// Name of mandator database parameters used in DataAdaptor SELECT SQL
		/// </summary>
		private const string EXCO_MAND_PARA		= ":EXCO_MND_ID";
		private const string PRECMED_MAND_PARA  = ":PMTY_MND_ID";
		private const string PLANT_MAND_PARA	= ":PL_MND_ID";
		private const string DEPT_MAND_PARA		= ":DEPT_MND_ID";
		private const string CRAFT_MAND_PARA	= ":CRA_MND_ID";
		/// <summary>
		/// currently selected Admin record (required for updates/deletes)
		/// </summary>
		private int mCurrentAdminID = -1; 
		/// <summary>
		/// Current User
		/// </summary>
		private int mCurrentUserID; 
		/// <summary>
		/// current mandator, ID and as string
		/// </summary>
		private int mCurrentMandID;
		private string mCurrentMandString;
		/// <summary>
		/// Search parameters values: assigned to the value selected comboboxes in GUI
		/// </summary>		
		private String  mEXCONameParameter;
		private String  mEXCOCityParameter;
		private String  mSuperParameter;		
		private String  mAssExcoParameter;
		private String  mAssCoordParameter;
		private String  mPrecMedKindParameter;
		private String  mPrecMedNameParameter;
		private String  mPlantParameter;
		private String  mDeptParameter;
		private String	mCraftNotaParameter;
		private String	mCraftNumberParameter;
		/// <summary>
		/// Used in dynaic generation of SQL WHER-clause text (excontractor, exco-coord, craft)
		/// </summary>
		private String  mExcontractorSqlWhere;
		private String  mExcoCoordSqlWhere;
		private String  mCraftSqlWhere;
		/// <summary>
		/// Deletes are executed via stored procedure, names here
		/// </summary>
		private IDbCommand cmdCallSP;
		private const string SP_RENAME_EXCO = "SP_EXCONTRACTOR_RENAME";
		private const string SP_DELEXCO     = "SP_DELETEEXCO ";
		private const string SP_ARCHEXCOCOORD = "SP_CHECKARCHIVECEC";
		private const string SP_DELPRECMED  = "SP_DELETEPMTY ";
		private const string SP_DELPLANT    = "SP_DELETEPL ";
		private const string SP_DELDEPT     = "SP_DELETEDEPT ";
		private const string SP_DELCRAFT    = "SP_DELETECRA ";
		/// <summary>
		/// Used for SQL to make sure exco is not going to be renamed to an exco already in table (status INVALID)
		/// </summary>
		private const string CHECKDUPLINVAL      = "CheckDuplicateInvalidExco";
		private const string EXCO_NAME_SQL_PARA  = ":EXCO_NAME";
	
		/// <summary>
		/// Used to store alternative coordinators for a given exco
		/// </summary>
		private Hashtable    httAlternativeCoordsByEXCO;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public AdministrationModel()
		{
			initialize();
		}

		#endregion //End of Constructors

		#region Initialization

		/// <summary>
		/// Initializes the members.
		/// Instantiate DataAdpators, excontractor and craft have to be done dynamically because of WHERE-SQL text
		/// Make command object (used to execute stored procedures)
		/// Get current User & Mandator ID (DB changes are logged in History table)
		/// </summary>
		private void initialize()
		{
			mProvider = DBSingleton.GetInstance().DataProvider;
 
			mDataAdapterExcoCoord   = mProvider.CreateDataAdapter(EXCOCOORD_ADA_ID);
			mDataAdapterPrecMedical = mProvider.CreateDataAdapter(PRECMED_ADA_ID);
			mDataAdapterPlant		= mProvider.CreateDataAdapter(PLANT_ADA_ID);
			cmdCallSP				= mProvider.CreateCommand("SequenceDummy");
			
			mCurrentUserID			= UserManagementControl.getInstance().CurrentUserID;
			mCurrentMandID			= UserManagementControl.getInstance().CurrentMandatorID;
			mCurrentMandString		= UserManagementControl.getInstance().CurrentMandatorID.ToString();			
		}	

		#endregion //End of Initialization

		#region Accessors 

		/// <summary>
		/// new Craft DataRow
		/// </summary>
		public DataRow NewCraftRow
		{
			get 
			{
				return mNewCraftRow;
			}
			set 
			{
				mNewCraftRow = value;
			}
		} 

		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// Set PK of current record in each UserControl to -1: 
		/// i.e. no record selected
		/// </summary>
		public void SetCurrentAdminIDToDefault()
		{
			((FrmAdministration) mView).frmUCAdminExternalContractor1.CurrentAdminRec			= -1;
			((FrmAdministration) mView).frmUCAdminCoordExco1.CurrentEXCORec	= -1;
			((FrmAdministration) mView).frmUCAdminCoordExco1.CurrentCoordinatorID = -1;
			((FrmAdministration) mView).frmUCAdminMedicalPrecautionary1.CurrentAdminRec			= -1;
			((FrmAdministration) mView).frmUCAdminPlant1.CurrentAdminRec						= -1;
			((FrmAdministration) mView).frmUCAdminDepartment1.CurrentAdminRec					= -1;
			((FrmAdministration) mView).frmUCAdminCraft1.CurrentAdminRec						= -1;
		}

	
		/// <summary>
		/// Register typified datasets with model: each UserControl has its own
		/// </summary>
		/// <param name="pDSExContractor"></param>
		internal void RegisterExcontractorDataSet(DSExContractor pDSExContractor) 
		{
			this.mDSExContractor = pDSExContractor;
		}
		internal void RegisterExcoCoordDataSet(DSExcoCoord pDSExcoCoord) 
		{
			this.mDSExcoCoord = pDSExcoCoord;
		}
		internal void RegisterPrecMedTypeDataSet(DSPrecMedType pDSPrecMedType) 
		{
			this.mDSPrecMedType = pDSPrecMedType;
		}
		internal void RegisterPlantDataSet(DSPlant pDSPlant) 
		{
			this.mDSPlant = pDSPlant;
		}
		internal void RegisterDeptDataSet(DSDepartment pDSDepartment) 
		{
			this.mDSDepartment = pDSDepartment;
		}
		internal void RegisterCraftDataSet(DSCraft pDSCraft) 
		{
			this.mDSCraft = pDSCraft;
		}

		/// <summary>
		/// Call a stored proc to delete the current row.
		/// Each Admin DB table has its own
		/// </summary>
		/// <param name="pCurrentAdminID">PK of row to delete</param>
		/// <param name="pSPName">Name of procedure to execute</param>
		/// <param name="pAdapter">Adaptor of typified dataset</param>
		private void DeleteViaStoredProcedure( int pCurrentAdminID, string pSPName, IDbDataAdapter pAdapter )
		{
			this.cmdCallSP.CommandText  = pSPName + "( " 
										+ pCurrentAdminID
										+ ", " 
										+ this.mCurrentUserID
										+ ")";
			
			cmdCallSP.CommandType = System.Data.CommandType.StoredProcedure;
			cmdCallSP.Connection  = pAdapter.DeleteCommand.Connection;
			cmdCallSP.Connection.Open();
			int ret = cmdCallSP.ExecuteNonQuery();
			cmdCallSP.Connection.Close();
		}

		/// <summary>
		/// Empty the datasets and clear databindings
		/// </summary>
		internal void ClearDataSets()
		{
			this.mDSExContractor.Clear();
			((FrmAdministration) mView).frmUCAdminExternalContractor1.DgrExternalContractor.DataBindings.Clear();

			this.mDSExcoCoord.Clear();
			((FrmAdministration) mView).frmUCAdminCoordExco1.DgrAssignment.DataBindings.Clear();
			
			this.mDSPrecMedType.Clear();
			((FrmAdministration) mView).frmUCAdminMedicalPrecautionary1.DataBindings.Clear();
			
			this.mDSPlant.Clear();
			((FrmAdministration) mView).frmUCAdminPlant1.DgrPlant.DataBindings.Clear();

			this.mDSDepartment.Clear();
			((FrmAdministration) mView).frmUCAdminDepartment1.DgrDepartment.DataBindings.Clear();

			this.mDSCraft.Clear();
			((FrmAdministration) mView).frmUCAdminCraft1.DataBindings.Clear();
		}

		/// <summary>
		/// Discard user changes by discarding changes made in dataset data and clearing fields
		/// </summary>
		internal void DiscardAllChanges()
		{
			if (this.mDSExContractor != null)
			{
				mDSExContractor.RejectChanges();
			}
			if ( this.mDSExcoCoord != null )
			{
				mDSExcoCoord.RejectChanges();
			}
			if (this.mDSPrecMedType != null)
			{
				mDSPrecMedType.RejectChanges();
			}	
			if (this.mDSPlant != null)
			{
				mDSPlant.RejectChanges();
			}	
			if (this.mDSDepartment != null)
			{
				mDSDepartment.RejectChanges();
			}			
			if (this.mDSCraft != null)
			{
				mDSCraft.RejectChanges();
			}
			if (this.mNewCraftRow != null)
			{
				mNewCraftRow = null;
			}
			this.ClearDataSets();
			this.ClearTextFields(true);
		}


		/// <summary>
		/// Clears content and databindings of all textfields in the user controls	
		/// and sets masks' property "Content Changed" to false (i.e. no changes)
		/// </summary>
		/// <param name="pDisable">Whether or not to diasble textfields</param>
		internal void ClearTextFields(bool pDisable)
		{			
			// Fields in UserControl "Fremdfirma" 
			ArrayList arrTxtFields = new ArrayList();
			arrTxtFields.Add( ((FrmAdministration) mView).frmUCAdminExternalContractor1.TxtEditExternalContractor);
            arrTxtFields.Add(((FrmAdministration)mView).frmUCAdminExternalContractor1.TxtEditDebitNo);
			arrTxtFields.Add( ((FrmAdministration) mView).frmUCAdminExternalContractor1.TxtEditStreet);
			arrTxtFields.Add( ((FrmAdministration) mView).frmUCAdminExternalContractor1.TxtEditPostalCode);
			arrTxtFields.Add( ((FrmAdministration) mView).frmUCAdminExternalContractor1.TxtEditCity);
			arrTxtFields.Add( ((FrmAdministration) mView).frmUCAdminExternalContractor1.TxtEditCountry);
			arrTxtFields.Add( ((FrmAdministration) mView).frmUCAdminExternalContractor1.TxtEditSupervisorSurname);
			arrTxtFields.Add( ((FrmAdministration) mView).frmUCAdminExternalContractor1.TxtEditSupervisorFirstname);
			arrTxtFields.Add( ((FrmAdministration) mView).frmUCAdminExternalContractor1.TxtEditPhone);
			arrTxtFields.Add( ((FrmAdministration) mView).frmUCAdminExternalContractor1.TxtEditFax );
			arrTxtFields.Add( ((FrmAdministration) mView).frmUCAdminExternalContractor1.TxtEditMobil );
			// Fields in UserControl "Vorsorgeuntersuchung"
			arrTxtFields.Add( ((FrmAdministration) mView).frmUCAdminMedicalPrecautionary1.TxtEditKind );
			arrTxtFields.Add( ((FrmAdministration) mView).frmUCAdminMedicalPrecautionary1.TxtEditPrecautionaryMedical );
			arrTxtFields.Add( ((FrmAdministration) mView).frmUCAdminMedicalPrecautionary1.TxtEditHelp );
			// Fields in UserControl "Betrieb"
			arrTxtFields.Add( ((FrmAdministration) mView).frmUCAdminPlant1.TxtEditPlant );
			// Plant
			arrTxtFields.Add( ((FrmAdministration) mView).frmUCAdminDepartment1.TxtEditDepartment );
			// Craft
			arrTxtFields.Add( ((FrmAdministration) mView).frmUCAdminCraft1.TxtEditCraftNumber ); 
			arrTxtFields.Add( ((FrmAdministration) mView).frmUCAdminCraft1.TxtEditCraft ); 
				
			foreach (System.Windows.Forms.TextBox tlr in arrTxtFields)
			{
				tlr.DataBindings.Clear();
				tlr.Clear();
				if ( pDisable )
				{
					tlr.Enabled = false;
				}
			}
			
			// UserControl "Zuordnung Fremdfirma" Clear assignment comboboxes
			((FrmAdministration) mView).frmUCAdminCoordExco1.CboAssignmentExternalContractor.Text	 = String.Empty;
			((FrmAdministration) mView).frmUCAdminCoordExco1.LikAssignmentCoordinator.Text			 = String.Empty;
			((FrmAdministration) mView).frmUCAdminCoordExco1.CboAssignmentExternalContractor.Enabled = false;
			((FrmAdministration) mView).frmUCAdminCoordExco1.LikAssignmentCoordinator.Enabled		 = false;

			((FrmAdministration)mView).frmUCAdminExternalContractor1.ContentChanged   = false;
			((FrmAdministration)mView).frmUCAdminCoordExco1.ContentChanged = false;
			((FrmAdministration)mView).frmUCAdminMedicalPrecautionary1.ContentChanged = false;
			((FrmAdministration)mView).frmUCAdminPlant1.ContentChanged				  = false;
			((FrmAdministration) mView).frmUCAdminDepartment1.ContentChanged		  = false;
			((FrmAdministration) mView).frmUCAdminCraft1.ContentChanged				  = false;

		}	


		#endregion // End of Methods
	
		#region ExternalContractor_Methods

		/// <summary>
		/// Methods relation to administration of ExternalContractors
		/// </summary>
		/// 

		
		
		/// <summary>
		/// Gets the Excontractor records that meet the search criteria and loads them into a dataset
		/// DataAdaptor for excontractor has to be re-generated in the search as SQL WHERE generated dynamically
		/// </summary>
		/// <param name="pUseCboVals">If search parameter values should be re-read from comboboxes</param>
		/// <exception cref="de.pta.Component.ErrorHandling.UIWarningException">UIWarningException</exception>
		/// is thrown if no results returned
		internal void GetExternalContractors(bool pUseCboVals) 
		{			
			int numrecs;
			
			if (pUseCboVals)
			{
				SetExcontractorWhereClause();
			}
			mDataAdapterExcontractor = mProvider.CreateDataAdapter(EXCO_ADA_ID); 
			mDataAdapterExcontractor.SelectCommand.CommandText += mExcontractorSqlWhere;
			mProvider.SetParameter(mDataAdapterExcontractor, EXCO_MAND_PARA, mCurrentMandString);
			
			try
			{
				numrecs = mProvider.FillDataSet(EXCO_ADA_ID, mDataAdapterExcontractor, mDSExContractor);
				if ( numrecs < 1 ) 
				{
					throw new UIWarningException(MessageSingleton.GetInstance().GetMessage
						(MessageSingleton.NO_RESULTS));
				}
				else
				{
					// Format nulls to make them look nice and bind datatable to grid
					this.FormatForDisplayExcoDataSet();
					((FrmAdministration) mView).frmUCAdminExternalContractor1.DgrExternalContractor.DataSource = mDSExContractor;
					((FrmAdministration) mView).frmUCAdminExternalContractor1.DgrExternalContractor.DataMember = EXCO_TABLE;
				}				
			}
			catch (DbAccessException)
			{
				throw new UIFatalException(MessageSingleton.GetInstance().GetMessage
					(MessageSingleton.FATAL_DB_ERROR));
			}
			catch (System.Data.OracleClient.OracleException oraex)
			{
				throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(
					MessageSingleton.FATAL_DB_ERROR) + oraex.Message );
			}				
		}

		/// <summary>
		/// Dynamically generate WHERE clause for the SELECT statement of the excontractor data adapter
		/// For each search parameter an extra condition is added to the SQL text
		/// No WHERE clause if nothing selected in comboboxes: show all results
		/// </summary>
		private void SetExcontractorWhereClause() 
		{
			int	noSearchCriteria = 0;
			mExcontractorSqlWhere = String.Empty;		
			mEXCONameParameter = String.Empty;
			mEXCOCityParameter = String.Empty;
			mSuperParameter	   = String.Empty;
			mEXCONameParameter = GetSelectedValueFromCbo(((FrmAdministration) mView).frmUCAdminExternalContractor1.CboSearchExternalContractor);
			mEXCOCityParameter = GetSelectedValueFromCbo(((FrmAdministration) mView).frmUCAdminExternalContractor1.CboSearchCity);
			mSuperParameter    = GetSelectedIDFromCbo(((FrmAdministration) mView).frmUCAdminExternalContractor1.CboSearchSupervisor);
		

			if ( mEXCONameParameter.Length < 1 ) 
			{				
				noSearchCriteria ++;
			}
			else
			{
				mExcontractorSqlWhere = mExcontractorSqlWhere
										+ " AND EXCO_NAME LIKE "
										+ "'"
										+ mEXCONameParameter 
										+ "%"
										+ "'";
			}

			if ( mEXCOCityParameter.Length < 1 ) 
			{
				noSearchCriteria ++;
			}
			else
			{
				mExcontractorSqlWhere = mExcontractorSqlWhere
										+ " AND EXCO_CITY LIKE "
										+ "'"
										+ mEXCOCityParameter 
										+ "%"
										+ "'";
			}

			if ( mSuperParameter.Length < 1 ) 
			{
				noSearchCriteria ++;
			}
			else
			{
				// This is the EXCO_ID: supervisor name is a column in the table
				mExcontractorSqlWhere = mExcontractorSqlWhere
										+ " AND EXCO_ID = "
										+ mSuperParameter; 
			}

			// Add ORDER BY, make sure sorted A, a, ...Z
			mExcontractorSqlWhere = mExcontractorSqlWhere +  " ORDER BY NLSSORT(EXCO_NAME, 'NLS_SORT=GERMAN')";
			if ( noSearchCriteria == 3 ) 
			{
				mExcontractorSqlWhere = " ORDER BY NLSSORT(EXCO_NAME, 'NLS_SORT=GERMAN')";
			} 
		}

		/// <summary>
		/// Load individual external contractor for edit: 
		/// get primary key of currently selected ExContractor select datarow by PK from dataset 
		/// Bind data to textfields in GUI
		/// </summary>
		internal void LoadIndividualExContractor()
		{
            FrmUCAdminExternalContractor frmExco = ((FrmAdministration)mView).frmUCAdminExternalContractor1;			
			int myPKVal = frmExco.CurrentAdminRec;
            
			// individual editing: 
			mDSExContractor.Tables[EXCO_TABLE].PrimaryKey = new DataColumn[] {mDSExContractor.Tables[EXCO_TABLE].Columns[EXCO_PK_COL] };
			mCurrentExcontractorRow = mDSExContractor.Tables[EXCO_TABLE].Rows.Find(myPKVal);

			if(null != mCurrentExcontractorRow)
			{
				// If the current EXCO is invalid (UNGÜLTIG) it can't be edited
				if ( mCurrentExcontractorRow["EXCO_STATUS"].ToString().Equals(Globals.STATUS_INVALID) )
				{
					ClearTextFields(true);
					SetCurrentAdminIDToDefault();
					throw new UIWarningException(
						MessageSingleton.GetInstance().GetMessage(MessageSingleton.ADMIN_EXCO_INVALID) );
				}						

				mCurrentExcontractorRow.BeginEdit();

				// Enable textfields
				frmExco.TxtEditExternalContractor.Enabled = true;
                frmExco.TxtEditDebitNo.Enabled = true;
				frmExco.TxtEditStreet.Enabled = true;
				frmExco.TxtEditPostalCode.Enabled = true;
				frmExco.TxtEditCity.Enabled = true;
				frmExco.TxtEditCountry.Enabled = true;
				frmExco.TxtEditSupervisorSurname.Enabled = true;
				frmExco.TxtEditSupervisorFirstname.Enabled = true;
				frmExco.TxtEditPhone.Enabled = true;
				frmExco.TxtEditFax.Enabled = true;
				frmExco.TxtEditMobil.Enabled = true;
					
				// Bind fields of dataset to textfields
				frmExco.TxtEditExternalContractor.DataBindings.Add("Text", mCurrentExcontractorRow, "EXCO_NAME");
                frmExco.TxtEditDebitNo.DataBindings.Add("Text", mCurrentExcontractorRow, "EXCO_DEBITNO");
                frmExco.TxtEditStreet.DataBindings.Add("Text", mCurrentExcontractorRow, "EXCO_STREET");
                frmExco.TxtEditPostalCode.DataBindings.Add("Text", mCurrentExcontractorRow, "EXCO_POSTCODE");
                frmExco.TxtEditCity.DataBindings.Add("Text", mCurrentExcontractorRow, "EXCO_CITY");
                frmExco.TxtEditCountry.DataBindings.Add("Text", mCurrentExcontractorRow, "EXCO_COUNTRY");
                frmExco.TxtEditSupervisorSurname.DataBindings.Add("Text", mCurrentExcontractorRow, "EXCO_SUPERSURNAME");
                frmExco.TxtEditSupervisorFirstname.DataBindings.Add("Text", mCurrentExcontractorRow, "EXCO_SUPERFIRSTNAME");
                frmExco.TxtEditPhone.DataBindings.Add("Text", mCurrentExcontractorRow, "EXCO_TELEPHONENO");
                frmExco.TxtEditFax.DataBindings.Add("Text", mCurrentExcontractorRow, "EXCO_FAX");
                frmExco.TxtEditMobil.DataBindings.Add("Text", mCurrentExcontractorRow, "EXCO_MOBILEPHONE");

				// Data in form is unchanged at this stage
                frmExco.ContentChanged = false;	
			}
		}


		/// <summary>
		/// Enable textfields to enter new data
		/// </summary>
		internal void CreateNewExContractor()
		{
            FrmUCAdminExternalContractor frmExco = ((FrmAdministration)mView).frmUCAdminExternalContractor1;
			
			frmExco.TxtEditExternalContractor.Enabled = true;
            frmExco.TxtEditDebitNo.Enabled = true;
			frmExco.TxtEditStreet.Enabled = true;
			frmExco.TxtEditPostalCode.Enabled = true;
			frmExco.TxtEditCity.Enabled = true;
			frmExco.TxtEditCountry.Enabled = true;
			frmExco.TxtEditSupervisorSurname.Enabled = true;
			frmExco.TxtEditSupervisorFirstname.Enabled = true;
			frmExco.TxtEditPhone.Enabled = true;
			frmExco.TxtEditFax.Enabled = true;
			frmExco.TxtEditMobil.Enabled = true;

            frmExco.ContentChanged = false;
		}


		/// <summary>
		/// Creates new Excontractor record.
        /// If an existing Exco has been renamed, a new record is created.
		/// Only name is compulsory, other fields are optional.
        /// Catches <see cref="OracleException"/> which is thrown when user creates an excontractor
        /// with the same name as an existing contractor
		/// </summary>
		/// <returns>PK of new Exco</returns>
		internal decimal SaveNewExContractor()
		{			
			decimal newPKVal = 0;
            FrmUCAdminExternalContractor frmExco = ((FrmAdministration)mView).frmUCAdminExternalContractor1;
			
			// Get values from textboxes
			string excoName = frmExco.TxtEditExternalContractor.Text;
			if (excoName.Length < 1)
			{		
				FormatForDisplayExcoDataSet();
				throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.NO_ADMIN_TO_SAVE ));				
			}
			else
			{
				CheckSpecialCharacterString(excoName);
			}

			string excoStreet = frmExco.TxtEditStreet.Text.Trim();            
			string excoPost = frmExco.TxtEditPostalCode.Text.Trim();	
			string excoCity = frmExco.TxtEditCity.Text.Trim();
			string excoCountry = frmExco.TxtEditCountry.Text.Trim();
			string excoSuperSur = frmExco.TxtEditSupervisorSurname.Text.Trim();
			string excoSuperFir = frmExco.TxtEditSupervisorFirstname.Text.Trim();		
			string excoTEL = frmExco.TxtEditPhone.Text.Trim();			
			string excoFAX = frmExco.TxtEditFax.Text.Trim();
			string excoMob = frmExco.TxtEditMobil.Text.Trim();

            // Ensures debitno numeric
            string excoDebitNo = frmExco.TxtEditDebitNo.Text.Trim();       
            int result;

            if (excoDebitNo.Length > 0 && !int.TryParse(excoDebitNo, out result))
            {
                string msgNum = MessageSingleton.GetInstance().GetMessage(MessageSingleton.VALUE_NOT_NUMERIC);
                throw new UIWarningException(String.Format(msgNum, "'Debit-Nr'"));
            }	

            CheckSpecialCharacterString(excoStreet);        
            CheckSpecialCharacterString(excoPost);
            CheckSpecialCharacterString(excoCity);
            CheckSpecialCharacterString(excoCountry);
            CheckSpecialCharacterString(excoSuperSur);
            CheckSpecialCharacterString(excoSuperFir);
            CheckSpecialCharacterString(excoTEL);
            CheckSpecialCharacterString(excoFAX);
            CheckSpecialCharacterString(excoMob);
			
			// Create data adapter
			mDataAdapterExcontractor = mProvider.CreateDataAdapter(EXCO_ADA_ID);

			if(null != mDSExContractor && null != mDataAdapterExcontractor)
			{								
				try
				{
					// Get unique PK value from sequence associated with this table
					newPKVal = Convert.ToInt32(GetNextValFromSeq("seq_excontractor"));

					// add new row to the existing ExContractor dataset
					mDSExContractor.FPASS_EXCONTRACTOR.AddFPASS_EXCONTRACTORRow(
						newPKVal,
						mCurrentMandID,
						excoName,
                        excoDebitNo,
						excoCity,
						excoPost,
						excoCountry,
						excoStreet,
						excoSuperFir,
						excoSuperSur,
						excoTEL,
						excoFAX,
						excoMob,
						Globals.STATUS_VALID,
                        mCurrentUserID,
						DateTime.Now);

					FormatForSaveExcoDataSet();
					mDataAdapterExcontractor.Update(mDSExContractor);
					mDSExContractor.AcceptChanges();

					frmExco.CurrentAdminRec = Convert.ToInt32(newPKVal);
					frmExco.CurrentEXCOName = excoName;
					
					FormatForDisplayExcoDataSet();
					frmExco.DgrExternalContractor.DataSource = mDSExContractor;
					frmExco.DgrExternalContractor.DataMember = EXCO_TABLE;
					frmExco.ContentChanged = false;

					return newPKVal;
				}
				catch (DbAccessException)
				{
					throw new UIFatalException(MessageSingleton.GetInstance().GetMessage
						(MessageSingleton.FATAL_DB_ERROR));
				}
				catch (System.Data.OracleClient.OracleException oraex)
				{
					// New 13.05.04: Oracle error from unique constraint on exco name and mand id 
					// Could have created constraint in dataset...
					if ( oraex.Code == 00001 )
					{
						this.mDSExContractor.RejectChanges();
						this.FormatForDisplayExcoDataSet();
						throw new UIWarningException( MessageSingleton.GetInstance().GetMessage(
							MessageSingleton.ADMIN_EXCO_DUPLICATE) );
					}
					else
					{
						throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(
							MessageSingleton.FATAL_DB_ERROR) + oraex.Message );
					}
				}	
			}
			else
			{
				throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(
					MessageSingleton.NO_ADMIN_TO_SAVE ));
			}
		}

		/// <summary>
		/// Save changes to credentials of excontractor (not including name, this has a separate method)
		/// Update dataset and save to database
		/// </summary>
		/// <exception cref="System.Data.DBConcurrencyException">DBConcurrencyException</exception>
		/// is thrown if records in database diffrent form those in dataset (ie if another user has updated)
		internal void SaveChangesExContractor()
		{
            FrmUCAdminExternalContractor frmExco = ((FrmAdministration)mView).frmUCAdminExternalContractor1;

			mCurrentAdminID	= frmExco.CurrentAdminRec;
			string excoName = frmExco.TxtEditExternalContractor.Text;

			if ( excoName.Length < 1 )
			{
				throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(
					MessageSingleton.NO_ADMIN_TO_SAVE ));				
			}
			else
			{
				CheckSpecialCharacterString(excoName);
			}
			// Make sure strings do not contain ' (would be better in own method)
			string excoStreet   = frmExco.TxtEditStreet.Text.Trim();			
			string excoPost	  = frmExco.TxtEditPostalCode.Text.Trim();			
			string excoCity	  = frmExco.TxtEditCity.Text.Trim();			
			string excoCountry  = frmExco.TxtEditCountry.Text.Trim();			
			string excoSuperSur = frmExco.TxtEditSupervisorSurname.Text.Trim();
			string excoSuperFir = frmExco.TxtEditSupervisorFirstname.Text.Trim();			
			string excoTEL	  = frmExco.TxtEditPhone.Text.Trim();			
			string excoFAX	  = frmExco.TxtEditFax.Text.Trim();			
			string excoMob	  = frmExco.TxtEditMobil.Text.Trim();

            // Ensures debitno numeric (long int 19 figures)
            // FPAS V5: leave as string
            string excoDebitNo = frmExco.TxtEditDebitNo.Text.Trim();
            //long result;

            //if (excoDebitNo.Length > 0 && !Int64.TryParse(excoDebitNo, out result))
            //{  
            //    string msgNum = MessageSingleton.GetInstance().GetMessage(MessageSingleton.VALUE_NOT_NUMERIC);
            //    throw new UIWarningException(String.Format(msgNum, "'Debit-Nr'"));
            //}	
 
            CheckSpecialCharacterString(excoStreet);
            CheckSpecialCharacterString(excoPost);
            CheckSpecialCharacterString(excoCity);
            CheckSpecialCharacterString(excoCountry);           
            CheckSpecialCharacterString(excoSuperSur);
            CheckSpecialCharacterString(excoSuperFir);
            CheckSpecialCharacterString(excoTEL);
            CheckSpecialCharacterString(excoFAX);
            CheckSpecialCharacterString(excoMob);
            CheckSpecialCharacterString(excoDebitNo);

			mDataAdapterExcontractor = mProvider.CreateDataAdapter(EXCO_ADA_ID);

			// Set non- compulsory fields to DbNull
			FormatForSaveExcoDataSet();

			try
			{
				if(null != mDSExContractor && null != mDataAdapterExcontractor && null != mCurrentExcontractorRow )
				{	
					// For each row in the ExContractor dataset check if the datarow has been changed
					foreach (DataRow dlr in mDSExContractor.FPASS_EXCONTRACTOR)
					{
						if (dlr.RowState.ToString().Equals(DS_ROW_UNCHANGED) == false) 
						{
							dlr["EXCO_CHANGEUSER"] = this.mCurrentUserID;
							dlr["EXCO_TIMESTAMP"] = DateTime.Now;
							dlr.EndEdit();			
						}					
					}	
					mDataAdapterExcontractor.Update(mDSExContractor);
					mDSExContractor.AcceptChanges();	
					FormatForDisplayExcoDataSet();
					frmExco.ContentChanged = false;	
				}		
			}
			catch (DBConcurrencyException)
			{
				throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(
					MessageSingleton.ADMIN_UPDATE_CONFL ));
			}
			catch (System.Data.OracleClient.OracleException oraex)
			{
				throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(
					MessageSingleton.FATAL_DB_ERROR) 
					+ oraex.Message );
			}	
			catch (DbAccessException)
			{
				throw new UIFatalException(MessageSingleton.GetInstance().GetMessage
					(MessageSingleton.FATAL_DB_ERROR));
			}
		}

		/// <summary>
		/// If an existing Excontractor is renamed, a new record is created in the DB and the existing is set to INVALID
		/// All assigned coworkers whose entry (validUNTIL) is still valid have to be reassigned to the new contractor (SP)
		/// If no more workers are assigned the excontractor is archived
		/// New search is carried out to get new EXCO record
		/// </summary>
		internal void RenameExContractor()
		{
			mDataAdapterExcontractor = mProvider.CreateDataAdapter(EXCO_ADA_ID);
			mCurrentAdminID = ((FrmAdministration) mView).frmUCAdminExternalContractor1.CurrentAdminRec;

			try
			{
				// Existing record should not be updated
				mDSExContractor.RejectChanges();
				
				if(null != mDSExContractor && null != mDataAdapterExcontractor && null != mCurrentExcontractorRow )
				{	
					// Create a new exco record, get its PK value
					decimal newPKVal = this.SaveNewExContractor();

					// Call a stored proc to update of status and reassign co-workers and coordinators	
					cmdCallSP.CommandText = SP_RENAME_EXCO + "( " 
						+ mCurrentAdminID
						+ ", " 
						+ newPKVal
						+ ", " 
						+ mCurrentUserID
						+ ")";
				
					cmdCallSP.CommandType = System.Data.CommandType.StoredProcedure;
					cmdCallSP.Connection  = mDataAdapterExcontractor.InsertCommand.Connection;
					cmdCallSP.Connection.Open();
					int ret = cmdCallSP.ExecuteNonQuery();
					cmdCallSP.Connection.Close();

					// Re- init form and carry out new search
					FPASSControllSingleton.GetInstance().ReInitializeListOfValues();
					ClearDataSets();	
					ClearTextFields(true);
					SetCurrentAdminIDToDefault();					
					mExcontractorSqlWhere = " AND EXCO_ID = " + newPKVal;
					GetExternalContractors(false);
				}
			}
			catch (System.Data.OracleClient.OracleException oraex)
			{			
				throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(
				MessageSingleton.FATAL_DB_ERROR) + oraex.Message );			
			}
			catch (DbAccessException)
			{
				throw new UIFatalException(MessageSingleton.GetInstance().GetMessage
					(MessageSingleton.FATAL_DB_ERROR));
			}
		}


		/// <summary>
		/// Delete current external contractor
		/// Row is deleted from dataset but delete at database level is executed via stored procedure
		/// After deleting, form is emptied to avoid problems with inconsistent datasets 11.12.03
		/// </summary>
		/// <exception cref="System.Data.OracleClient.OracleException">OracleException</exception> 
		/// ORA-20001 is returned by stored procedure if dependent data exist (normally exco must be archived) 
		internal void DeleteExternalContractor()
		{
			mDataAdapterExcontractor = mProvider.CreateDataAdapter(EXCO_ADA_ID);
			mCurrentAdminID			 = ((FrmAdministration) mView).frmUCAdminExternalContractor1.CurrentAdminRec;

			// Current datarow must be the one currently selected in Grid.					
			if (null != mCurrentExcontractorRow)
			{
				try
				{
					DeleteViaStoredProcedure(mCurrentAdminID, SP_DELEXCO, mDataAdapterExcontractor);
					
					mCurrentExcontractorRow.Delete();			
					mDSExContractor.AcceptChanges();	
					SetCurrentAdminIDToDefault();
					DiscardAllChanges();	
				}
				catch (System.Data.OracleClient.OracleException oraex)
				{
					if ( cmdCallSP.Connection != null )
					{
						cmdCallSP.Connection.Close();
					}
					// Is this an exception from dependent data?
					if ( oraex.Message.IndexOf("20001") != -1 )
					{
						throw new UIWarningException( MessageSingleton.GetInstance().GetMessage(
							MessageSingleton.ADMIN_EXCO_DEP_DATA) );
					}
					else
					{
						throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(
							MessageSingleton.FATAL_DB_ERROR) + oraex.Message );
					}
				}
				catch (DbAccessException)
				{
					throw new UIFatalException(MessageSingleton.GetInstance().GetMessage
						(MessageSingleton.FATAL_DB_ERROR));
				}
			}	
		}

		/// <summary>
		/// Need this to make sure the excontractor being renamed is not going to damage unique constraint
		/// (the exco becomes INVALID and may then clash with name of an existing INVALID exco)
		/// Would be better if constraint in database did not extend over STATUS....
		/// </summary>
		internal void CheckExistenceOfInvalidExco(string pNameToBeChanged)
		{
			int recs = 0;
			try 
			{
				// Get DB provider, make an SQL statement, bind parameters and execute SQL
				IProvider provider  = DBSingleton.GetInstance().DataProvider;
				IDbCommand mCommSel = provider.CreateCommand(CHECKDUPLINVAL);
		
				provider.SetParameter(mCommSel, EXCO_NAME_SQL_PARA,  pNameToBeChanged );
				provider.SetParameter(mCommSel, EXCO_MAND_PARA,  UserManagementControl.getInstance().CurrentMandatorID);

				IDataReader mDR = mProvider.GetReader(mCommSel);
				while (mDR.Read())
				{
					recs++;
				}
				mDR.Close();
	
				// Will always find one record as this is current exco
				if ( recs > 1 ) 
				{
					throw new UIWarningException (MessageSingleton.GetInstance().
						GetMessage( MessageSingleton.ADMIN_EXCO_CANNOT_RENAME ) ) ;
				}
			}
			catch ( System.Data.OracleClient.OracleException oe ) 
			{
				
				throw new UIFatalException(MessageSingleton.GetInstance().GetMessage(
					MessageSingleton.FATAL_DB_ERROR), oe);
			} 
			catch ( DbAccessException dba ) 
			{
				throw new UIFatalException(MessageSingleton.GetInstance().GetMessage(
					MessageSingleton.FATAL_DB_ERROR), dba);
			}
		}

		/// <summary>
		/// Converts NULLS in the data records to ""
		/// </summary>
		private void FormatForDisplayExcoDataSet()
		{
			foreach (DataRow dr in mDSExContractor.FPASS_EXCONTRACTOR)
			{
				for (int i = 0; i < mDSExContractor.Tables["FPASS_EXCONTRACTOR"].Columns.Count; i++)
				{
					if ( dr[i].Equals(" ") )
					{					
						dr[i] = "";
					}
                    else if (dr[i].Equals(DBNull.Value))
                    {
                        dr[i] = "";
                    }
				}				
			}
		}

		/// <summary>
        /// Convert empty strings (e.g. non- compulsory from textfields) to DBNull for database fields
		/// </summary>
		private void FormatForSaveExcoDataSet()
		{
			foreach (DataRow dr in mDSExContractor.FPASS_EXCONTRACTOR)
			{
				for (int i = 0; i < mDSExContractor.Tables["FPASS_EXCONTRACTOR"].Columns.Count; i++)
				{
					if ( dr[i].Equals("") )
					{					
						dr[i] = DBNull.Value;
					}
				}				
			} 
		}


		#endregion // End of ExternalContractor_Methods

		#region AssignmentExcontCoord_Methods


		/// <summary>
		/// Gets ID of exco selected in "FFirma" combobox, use this to open Coord History popup
		/// </summary>
		/// <returns></returns>
		internal decimal ExcoIDChosenForPopHist()
		{
			string strCurrExcoID = this.GetSelectedIDFromCbo( ((FrmAdministration) mView).frmUCAdminCoordExco1.CboSearchExternalContractor );
			try
			{
				return Convert.ToDecimal( strCurrExcoID );
			}
			catch (Exception )
			{
				return 0;
			}

		}

		/// <summary>
		/// selects assignments which match given search criteria and fills DataSet for excontractor-coordinator assignments
		/// Dataset is filled via customized SQL statement querying a view, the orig insert/update/del statements in the adapter are used
		/// throws <exception cref="de.pta.Component.ErrorHandling.UIWarningException"> UIWarningException</exception> 
		/// when when select returns no results
		/// </summary>
		public void GetExtContractorsCoords() 
		{		
			int numrecs;
			mExcoCoordSqlWhere = String.Empty;
			this.mDSExcoCoord.Clear();
			((FrmAdministration) mView).frmUCAdminCoordExco1.DgrAssignment.DataBindings.Clear();

			this.SetExcoCoordWhereClause();

			// Make the new SQL statement: SELECT from a join view and add WHERE clause
			IDbCommand selComm		= mProvider.CreateCommand(EXCOCOORD_QUERY);
			selComm.CommandText		= selComm.CommandText + mExcoCoordSqlWhere;
			mProvider.SetParameter(selComm, EXCO_MAND_PARA, 
								UserManagementControl.getInstance().CurrentMandatorID.ToString()); 
		
			// Make sure SELECT command uses same connection as adaptor and assign it to adapter
			selComm.Connection					= mDataAdapterExcoCoord.InsertCommand.Connection;
			mDataAdapterExcoCoord.SelectCommand = selComm;
	
			try
			{
				numrecs = mProvider.FillDataSet(EXCOCOORD_ADA_ID, mDataAdapterExcoCoord, mDSExcoCoord);
				if ( numrecs < 1 ) 
				{
					throw new UIWarningException(MessageSingleton.GetInstance().GetMessage
						(MessageSingleton.NO_EXCO_COORD_RESULT));
				}
				else
				{
					((FrmAdministration) mView).frmUCAdminCoordExco1.DgrAssignment.DataSource = mDSExcoCoord;
					((FrmAdministration) mView).frmUCAdminCoordExco1.DgrAssignment.DataMember = "FPASS_EXCOECOD";
				}
			}
			catch (System.Data.OracleClient.OracleException oraex)
			{
				throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(
					MessageSingleton.FATAL_DB_ERROR) + oraex.Message );
			}
			catch (DbAccessException)
			{
				throw new UIFatalException(MessageSingleton.GetInstance().GetMessage
					(MessageSingleton.FATAL_DB_ERROR));
			}	
		}


		/// <summary>
		/// Dynamically generates the WHERE clause of the SELECT statement according to the values slected in the comboboxes
		/// throws <exception cref=""> UIWarningException</exception> when user changes the content of the combobox 
		/// No WHERE clause if nothing selected in comboboxes: show all results
		/// </summary>
		private void SetExcoCoordWhereClause() 
		{
			int			noSearchCriteria = 0;

			// Get IDs in this case from the ComboBoxes (pta LOV Component)
			this.mAssExcoParameter = this.GetSelectedIDFromCbo( ((FrmAdministration) mView).frmUCAdminCoordExco1.CboSearchExternalContractor );
			if ( mAssExcoParameter.Length > 0 )
			{
				mExcoCoordSqlWhere = mExcoCoordSqlWhere
					+ " AND ECEC_EXCO_ID = "
					+ mAssExcoParameter;
			}
			else
			{
				this.mAssExcoParameter = String.Empty;
				noSearchCriteria ++;
			}

			this.mAssCoordParameter = this.GetSelectedIDFromCbo( ((FrmAdministration) mView).frmUCAdminCoordExco1.LikSearchCoordinator );
			if ( mAssCoordParameter.Length > 0 )
			{
				mExcoCoordSqlWhere = mExcoCoordSqlWhere
					+ " AND ECEC_ECOD_ID = "
					+ mAssCoordParameter;
			}
			else
			{
				this.mAssCoordParameter = String.Empty;
				noSearchCriteria ++;
			}
			mExcoCoordSqlWhere = mExcoCoordSqlWhere + " ORDER BY NLSSORT(EXCO_NAME, 'NLS_SORT=GERMAN')";

			if ( noSearchCriteria == 2 ) 
			{
				mExcoCoordSqlWhere = " ORDER BY NLSSORT(EXCO_NAME, 'NLS_SORT=GERMAN')";
			} 
		}

		/// <summary>
		/// Load one assigment into the comboboxes at foot of form: there is no update of assigments, only insert & delete
		/// If current external contractor is invalid, LovIten is null, show string "INVALID" in combobox. EXCOID is right
		/// </summary>
		public void LoadIndividualExcoCoordinators()
		{
			// Get ID of ExContractor currently selected in grid
			int myEXCOVal = ((FrmAdministration) mView).frmUCAdminCoordExco1.CurrentEXCORec;
			// Get ID of coordinator currently selected in grid
			int myCOORDVal = ((FrmAdministration) mView).frmUCAdminCoordExco1.CurrentCoordinatorID;
			
			object[] arrKeys = new object[] {myCOORDVal, myEXCOVal};

			// Get current datarow
			mDSExcoCoord.FPASS_EXCOECOD.PrimaryKey = new DataColumn[] {mDSExcoCoord.FPASS_EXCOECOD.ECEC_ECOD_IDColumn, mDSExcoCoord.FPASS_EXCOECOD.ECEC_EXCO_IDColumn};
			mCurrentExcoCoordRow = mDSExcoCoord.FPASS_EXCOECOD.Rows.Find(arrKeys);

			// Locate current datarow & show vals in combobox
			LovItem itemEXCO  = this.GetLovItem(myEXCOVal, ((FrmAdministration) mView).frmUCAdminCoordExco1.CboAssignmentExternalContractor);
			LovItem itemCOORD = this.GetLovItem(myCOORDVal, ((FrmAdministration) mView).frmUCAdminCoordExco1.LikAssignmentCoordinator);

			if ( null != itemEXCO )
			{
				((FrmAdministration) mView).frmUCAdminCoordExco1.CboAssignmentExternalContractor.Text = "";
				((FrmAdministration) mView).frmUCAdminCoordExco1.CboAssignmentExternalContractor.SelectedItem = itemEXCO;
			}
			else
			{
				// 01.03.04
				((FrmAdministration) mView).frmUCAdminCoordExco1.CboAssignmentExternalContractor.Text = "Ungültige Fremdfirma";
			}
			((FrmAdministration) mView).frmUCAdminCoordExco1.LikAssignmentCoordinator.SelectedItem   = itemCOORD;			
			((FrmAdministration) mView).frmUCAdminCoordExco1.CboAssignmentExternalContractor.Enabled = true;
			((FrmAdministration) mView).frmUCAdminCoordExco1.LikAssignmentCoordinator.Enabled        = true;	
			((FrmAdministration)mView).frmUCAdminExternalContractor1.ContentChanged = false;
			
			
		}

	
		/// <summary>
		/// Enable comboboxes to allow new assignments
		/// </summary>
		internal void CreateAssignExcoCoord()
		{			
			((FrmAdministration)mView).frmUCAdminCoordExco1.CboAssignmentExternalContractor.Enabled = true;
			((FrmAdministration)mView).frmUCAdminCoordExco1.LikAssignmentCoordinator.Enabled = true;
			((FrmAdministration)mView).frmUCAdminCoordExco1.ContentChanged = false;
		}

		/// <summary>
		/// Save new assignment exco - coordinator by creating new datarow and adding it to typfied dataset
		/// Get IDs of excontractor); and coordinator from combo boxes
		/// </summary>
		/// <exception cref="de.pta.Component.ErrorHandling.UIWarningException"> UIWarningException</exception> 
		/// thrown if either of the comboboxes is empty
		/// <exception cref="System.Data.DBConcurrencyException">DBConcurrencyException</exception>
		/// is thrown if this assignment already exists
		/// <exception cref="System.Data.OracleClient.OracleException">OracleException</exception> 
		/// ORA-00001 is thrown if this assignment already exists and dataset currently empty
		internal void SaveNewAssignmentExCoCoord()
		{
			string txtNewEXCOID  = "";
			string txtNewCOORDID = "";
			if ( ((FrmAdministration) mView).frmUCAdminCoordExco1.CboAssignmentExternalContractor.SelectedItem != null )
			{
				txtNewEXCOID  = this.GetSelectedIDFromCbo( ((FrmAdministration) mView).frmUCAdminCoordExco1.CboAssignmentExternalContractor);
			}
			if ( ((FrmAdministration) mView).frmUCAdminCoordExco1.LikAssignmentCoordinator.SelectedItem != null )
			{
				txtNewCOORDID = this.GetSelectedIDFromCbo( ((FrmAdministration) mView).frmUCAdminCoordExco1.LikAssignmentCoordinator);
			}
			
			// If either ID is empty (nothing chosen) then raise exception
			if (txtNewEXCOID.Length < 1 || txtNewCOORDID.Length < 1)
			{
				throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(
					MessageSingleton.NO_ADMIN_ASSIGN ));
			}

			// Convert to correct format for new row
			int insNewEXCOID  = Convert.ToInt32(txtNewEXCOID);
			int insNewCOORDID = Convert.ToInt32(txtNewCOORDID);
			// (these 2 needed as new datarow appears in grid)
			string currEXCOName = this.GetSelectedValueFromCbo( ((FrmAdministration) mView).frmUCAdminCoordExco1.CboAssignmentExternalContractor );
			string currSUPER = this.GetSelectedValueFromCbo( ((FrmAdministration) mView).frmUCAdminCoordExco1.LikAssignmentCoordinator );

			if(null != mDSExcoCoord && null != mDataAdapterExcoCoord)
			{
				try
				{				
					mDSExcoCoord.FPASS_EXCOECOD.AddFPASS_EXCOECODRow(this.mCurrentUserID,
						DateTime.Now,
						insNewCOORDID,
						insNewEXCOID,
						currEXCOName,
						currSUPER);	
						
					this.mDataAdapterExcoCoord.Update(mDSExcoCoord);
					this.mDSExcoCoord.AcceptChanges();
					
					this.ClearTextFields(true);
					((FrmAdministration) mView).frmUCAdminCoordExco1.DgrAssignment.DataSource = mDSExcoCoord;
					((FrmAdministration) mView).frmUCAdminCoordExco1.DgrAssignment.DataMember = ECEC_TABLE;
					((FrmAdministration) mView).frmUCAdminCoordExco1.ContentChanged = false;
		
				}
				catch (System.Data.ConstraintException)
				{
					//((FrmAdministration) mView).frmUCAdminCoordinatorExternalContractor1.DgrAssignment.DataSource = null;
					((FrmAdministration) mView).frmUCAdminCoordExco1.ContentChanged = false;
					throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(
						MessageSingleton.ADMIN_ASSIGN_DUPL));
				}
				catch (System.Data.OracleClient.OracleException oraex)
				{
					if ( oraex.Code == 00001 )
					{
						//((FrmAdministration) mView).frmUCAdminCoordinatorExternalContractor1.DgrAssignment.DataSource = null;
						((FrmAdministration) mView).frmUCAdminCoordExco1.ContentChanged = false;
						throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(
						MessageSingleton.ADMIN_ASSIGN_DUPL));
					}
					else
					{
						throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(
							MessageSingleton.FATAL_DB_ERROR) + oraex.Message );
					}
				}
				catch (DbAccessException)
				{
					throw new UIFatalException(MessageSingleton.GetInstance().GetMessage
						(MessageSingleton.FATAL_DB_ERROR));
				}
				this.SetCurrentAdminIDToDefault();
				this.ClearTextFields(true);
			}		
		}

		/// <summary>
		/// If assignment coordinator - excontractor is deleted the coworkers assigned to this coordinator 
		/// for the given exco have to be reassigend
		/// If there are no alternative coordinators for given exco, then generate error.
		/// After deleting, form is emptied to avoid problems with inconsistent datasets 11.12.03
		/// </summary>
		/// <exception cref="System.Data.OracleClient.OracleException">OracleException</exception> 
		/// ORA-20003 is returned by stored procedure if dependent data exist ( should not normally occur)
		/// <exception cref="de.pta.Component.ErrorHandling.UIWarningException"> UIWarningException</exception> 
		/// thrown if coworkers not successfully reassigned
		/// <exception cref="System.Data.DBConcurrencyException">DBConcurrencyException</exception>
		/// thrown if records in database diffrent form those in dataset (ie if another user has updated)
		internal void DeleteCoordinatorExternalContractor()
		{
			IDbTransaction trans = null;
			IDbCommand dummyComm = null;
			bool flgCoordDep	 = true;
			bool flgSuccess      = true;
			
			// Get ID of coordinator & ExContractor currently selected in grid
			int myCOORDVal		= ((FrmAdministration) mView).frmUCAdminCoordExco1.CurrentCoordinatorID;		
			int myEXCOVal		= ((FrmAdministration) mView).frmUCAdminCoordExco1.CurrentEXCORec;
			decimal decCOORDVal = Convert.ToDecimal(myCOORDVal);
			decimal decEXCOVal	= Convert.ToDecimal(myEXCOVal);
			
			try
			{				
				if (null != mCurrentExcoCoordRow)
				{
					// Hashtable of the alternative coordinators
					httAlternativeCoordsByEXCO = new Hashtable();
					httAlternativeCoordsByEXCO = RoleCoordinatorSingleton.GetInstance().GetAlternativeCoordinators(decCOORDVal, decEXCOVal);
				
					if ( null == httAlternativeCoordsByEXCO )
					{
						// the coordinator has no dependent coworkers: ok to delete assignment
						flgCoordDep = false;
					}
					

					// Assign dependent coworkers to alternative coordinator
					// Check if alternative coordinators are available
					if ( flgCoordDep && null != httAlternativeCoordsByEXCO )
					{
						foreach ( object obj in httAlternativeCoordsByEXCO.Keys )
						{
							decimal k     = Convert.ToDecimal(obj);
							// If entry in hashtable contains an arraylist of cooeindators, then ok
							object objEntry = httAlternativeCoordsByEXCO[k];

							// If entry contains a string (name of exoc to which there are no other coords assigned), error
							if ( objEntry.GetType().ToString().Equals("System.String") )
							{
								((FrmAdministration) mView).frmUCAdminCoordExco1.ContentChanged = false;
								throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.COORD_DEP_CWR_ASS) + " vorliegen.");
							}
						}
					}
					// Open transaction
					mDataAdapterExcoCoord.DeleteCommand.Connection.Open();
					trans = mProvider.GetTransaction(mDataAdapterExcoCoord);
					mDataAdapterExcoCoord.DeleteCommand.Transaction = trans;

					dummyComm  = mProvider.CreateCommand("SequenceDummy");
					dummyComm.Connection = mDataAdapterExcoCoord.DeleteCommand.Connection;
					dummyComm.Transaction = trans;

					if ( flgCoordDep && null != httAlternativeCoordsByEXCO )
					{
						foreach ( object obj in httAlternativeCoordsByEXCO.Keys )
						{						
							// k ist the current EXCOID
							decimal k     = Convert.ToDecimal(obj);
							ArrayList alt = (ArrayList) httAlternativeCoordsByEXCO[k];
				
							// Reassign coworkers and delete exco - coord assignment
							flgSuccess = RoleCoordinatorSingleton.GetInstance().ReAssignCoWorkers(
								false,
								decCOORDVal, 
								k, 
								alt, 
								dummyComm,
								trans);

							
							if ( !flgSuccess )
							{
								// Coordinators were not successfully reassigned
								if (trans != null && dummyComm != null)
								{
									trans.Rollback();
									dummyComm.Connection.Close();
									mDataAdapterExcoCoord.DeleteCommand.Connection.Close();
								}
								((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.Default;
								((FrmAdministration) mView).frmUCAdminCoordExco1.ContentChanged = false;
								throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.COORD_REASS_ERROR));
							}
						}
					}
					else
					{
						// Delete assigment via stored procedure so that the assignment can be archived
						this.cmdCallSP.CommandText = SP_ARCHEXCOCOORD 
							+ "( " 
							+ myCOORDVal
							+ ", " 
							+ myEXCOVal
							+ ", " 
							+ this.mCurrentUserID
							+ ")";
				
						cmdCallSP.CommandType = System.Data.CommandType.StoredProcedure;
						cmdCallSP.Connection  = mDataAdapterExcoCoord.DeleteCommand.Connection;
						cmdCallSP.Transaction = dummyComm.Transaction;
						int ret = cmdCallSP.ExecuteNonQuery();								
					}

					// Update dataset if coworkers were successfully reassigned (no DB change)
					if ( flgSuccess )
					{													
						mCurrentExcoCoordRow.Delete();
						this.mDSExcoCoord.AcceptChanges();
						this.SetCurrentAdminIDToDefault();
						this.ClearTextFields(true);
						trans.Commit();
						dummyComm.Connection.Close();
						mDataAdapterExcoCoord.DeleteCommand.Connection.Close();
						
						/*
                         * No longer required, coordinator data is not exported to ZKS
                        // Update all CWRs in ZKS
						// Remove transaction and export to ZKS				
						try
						{
							trans.Dispose();
							base.ExportAllToZKS();
						}
						catch ( UIWarningException )
						{
							// do nothing
						} */
					}
				}
			}
			catch (DBConcurrencyException)
			{
				((FrmAdministration) mView).frmUCAdminCoordExco1.ContentChanged = false;
				if (trans != null && dummyComm != null)
				{
					trans.Rollback();
					dummyComm.Connection.Close();
					mDataAdapterExcoCoord.DeleteCommand.Connection.Close();
				}
				throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.ADMIN_UPDATE_CONFL));
			}	
			catch (System.Data.OracleClient.OracleException oraex)
			{					
				if (trans != null && dummyComm != null)
				{
					trans.Rollback();
					dummyComm.Connection.Close();
					mDataAdapterExcoCoord.DeleteCommand.Connection.Close();
				}
				// Is this an exception from dependent data?
				if ( oraex.Code == 20003 )
				{
					throw new UIWarningException( MessageSingleton.GetInstance().GetMessage(MessageSingleton.ADMIN_DEPENDENT_DATA));
				}
				else
				{
					throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(
						MessageSingleton.FATAL_DB_ERROR) 
						+ oraex.Message );				
				}
			}
			catch (DbAccessException)
			{
				throw new UIFatalException(MessageSingleton.GetInstance().GetMessage
					(MessageSingleton.FATAL_DB_ERROR));
			}
		}

		#endregion // End of AssignmentExcontCoord_Methods


		#region PrecautionaryMedical_Methods
		
		/// <summary>
		/// Methods concerning administration of precautionary medicals
		/// </summary>

		/// <summary>
		/// Gets Prec Medical base data records that meet the search criteria and loads them into a dataset
		/// </summary>
		/// <exception cref="de.pta.Component.ErrorHandling.UIWarningException">UIWarningException</exception>
		/// thrown if no results returned
		public void GetPrecautionaryMedicals() 
		{			
			int numrecs;
			this.mDSPrecMedType.Clear();			
			this.SetPrecMedSearchCriteria();

			mProvider.SetParameter(mDataAdapterPrecMedical, PRECMED_MAND_PARA, 
				mCurrentMandString);
			mProvider.SetParameter(mDataAdapterPrecMedical, ":PMTY_TYPE", this.mPrecMedKindParameter);
			mProvider.SetParameter(mDataAdapterPrecMedical, ":PMTY_NOTATION", this.mPrecMedNameParameter);
			
			try
			{
				numrecs = mProvider.FillDataSet(PRECMED_ADA_ID, mDataAdapterPrecMedical, mDSPrecMedType);
				if ( numrecs < 1 ) 
				{
					throw new UIWarningException(MessageSingleton.GetInstance().GetMessage
						(MessageSingleton.NO_RESULTS));
				}
				else
				{
					// Must specify what to do if columns contain NULLS  else System.Reflection.TargetInvocationException
					this.FormatForDisplayPrecMedDataSet();
					// Bind to datagrid
					((FrmAdministration) mView).frmUCAdminMedicalPrecautionary1.DgrPrecautionaryMedical.DataSource = mDSPrecMedType;
					((FrmAdministration) mView).frmUCAdminMedicalPrecautionary1.DgrPrecautionaryMedical.DataMember = PRECMED_TABLE;
				}
			}			
			catch (System.Data.OracleClient.OracleException oraex)
			{
				throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(
					MessageSingleton.FATAL_DB_ERROR) + oraex.Message );
			}
			catch (DbAccessException)
			{
				throw new UIFatalException(MessageSingleton.GetInstance().GetMessage
					(MessageSingleton.FATAL_DB_ERROR));
			}			
		}


		/// <summary>
		/// reads search parameters from GUI
		/// No WHERE clause if nothing selected in comboboxes: show all results
		/// </summary>
		private void SetPrecMedSearchCriteria() 
		{
			this.mPrecMedKindParameter = this.GetSelectedValueFromCbo(((FrmAdministration) mView).frmUCAdminMedicalPrecautionary1.CboSearchKind);
			this.mPrecMedNameParameter = this.GetSelectedValueFromCbo(((FrmAdministration) mView).frmUCAdminMedicalPrecautionary1.CboSearchPrecautionaryMedical);
			
			if ( mPrecMedKindParameter.Length < 1 ) 
			{
				this.mPrecMedKindParameter = "%";
			}
			if ( mPrecMedNameParameter.Length < 1 ) 
			{
				this.mPrecMedNameParameter = "%";
			}
		}


		/// <summary>
		/// Load individual prec medical for edit: 
		/// get primary key of currently selected medical: select datarow by PK from dataset 
		/// Bind data to textfields in GUI
		/// </summary>
		public void LoadIndividualPrecMed()
		{
			int myPKVal = ((FrmAdministration) mView).frmUCAdminMedicalPrecautionary1.CurrentAdminRec;
		
			mDSPrecMedType.FPASS_PRECMEDTYPE.PrimaryKey = new DataColumn[] {mDSPrecMedType.FPASS_PRECMEDTYPE.Columns[PRECMED_PK_COL] };
			mCurrentPrecMedRow = mDSPrecMedType.FPASS_PRECMEDTYPE.Rows.Find(myPKVal);
		
			// bind datarow to text fields in User Control ("Vorsorgeuntersuch..")
			if(null != mCurrentPrecMedRow)
			{
				mCurrentPrecMedRow.BeginEdit();

				((FrmAdministration) mView).frmUCAdminMedicalPrecautionary1.TxtEditKind.DataBindings.Add("Text", mCurrentPrecMedRow, "PMTY_TYPE");
				((FrmAdministration) mView).frmUCAdminMedicalPrecautionary1.TxtEditPrecautionaryMedical.DataBindings.Add("Text", mCurrentPrecMedRow, "PMTY_NOTATION");
				((FrmAdministration) mView).frmUCAdminMedicalPrecautionary1.TxtEditHelp.DataBindings.Add("Text", mCurrentPrecMedRow, "PMTY_HELPFILE");
				//Enable textfields
				((FrmAdministration)mView).frmUCAdminMedicalPrecautionary1.TxtEditHelp.Enabled = true;
				((FrmAdministration)mView).frmUCAdminMedicalPrecautionary1.TxtEditKind.Enabled = true;
				((FrmAdministration)mView).frmUCAdminMedicalPrecautionary1.TxtEditPrecautionaryMedical.Enabled = true;
				((FrmAdministration)mView).frmUCAdminMedicalPrecautionary1.ContentChanged = false;
		
			}
		}

		/// <summary>
		/// Enable textfields to allow new prec medical details to be entered
		/// </summary>
		internal void CreateNewPrecMedical()
		{			
			((FrmAdministration)mView).frmUCAdminMedicalPrecautionary1.TxtEditHelp.Enabled = true;
			((FrmAdministration)mView).frmUCAdminMedicalPrecautionary1.TxtEditKind.Enabled = true;
			((FrmAdministration)mView).frmUCAdminMedicalPrecautionary1.TxtEditPrecautionaryMedical.Enabled = true;
			((FrmAdministration)mView).frmUCAdminMedicalPrecautionary1.ContentChanged = false;
		}


		/// <summary>
		/// Save new Prec Med details by getting new PK values from sequence,
		/// creating new DataRow, adding to dataset and saving to database
		/// </summary>
		/// <exception cref="de.pta.Component.ErrorHandling.UIWarningException"> UIWarningException</exception> 
		/// thrown if any compulsory textfields are empty
		internal void SaveNewMedical()
		{
			string insNewPrecMedKind = ((FrmAdministration) mView).frmUCAdminMedicalPrecautionary1.TxtEditKind.Text;
			string insNewPrecMedNota = ((FrmAdministration) mView).frmUCAdminMedicalPrecautionary1.TxtEditPrecautionaryMedical.Text;
			string insNewPrecMedHelp = ((FrmAdministration) mView).frmUCAdminMedicalPrecautionary1.TxtEditHelp.Text;

			if (insNewPrecMedKind.Length < 1 || insNewPrecMedNota.Length < 1)
			{
				throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(
					MessageSingleton.NO_ADMIN_TO_SAVE ));
			}
			else
			{
				CheckSpecialCharacterString(insNewPrecMedKind);
				CheckSpecialCharacterString(insNewPrecMedNota);
				CheckSpecialCharacterString(insNewPrecMedHelp);
			}
			
			if(null != mDSPrecMedType && null != mDataAdapterPrecMedical)
			{
				decimal newPKVal = 0;				
				try
				{
					// Get sequence ID from DataReader
					newPKVal = Convert.ToInt32( this.GetNextValFromSeq("seq_precmedtype") );

					// add new row to the existing dataset
					mDSPrecMedType.FPASS_PRECMEDTYPE.AddFPASS_PRECMEDTYPERow(this.mCurrentUserID,
						DateTime.Now,
						newPKVal,
						this.mCurrentMandID,
						insNewPrecMedKind,
						insNewPrecMedNota, 
						insNewPrecMedHelp);

					this.FormatForSavePrecMedDataSet();
					this.mDataAdapterPrecMedical.Update(mDSPrecMedType);
					this.mDSPrecMedType.AcceptChanges();
					this.FormatForDisplayPrecMedDataSet();
					this.ClearTextFields(true);
					
					// 27.05.04: reload
					this.FormatForDisplayPrecMedDataSet();
					((FrmAdministration) mView).frmUCAdminMedicalPrecautionary1.DgrPrecautionaryMedical.DataSource = mDSPrecMedType;
					((FrmAdministration) mView).frmUCAdminMedicalPrecautionary1.DgrPrecautionaryMedical.DataMember = PRECMED_TABLE;
					((FrmAdministration) mView).frmUCAdminMedicalPrecautionary1.ContentChanged = false;
				}
				catch (System.Data.OracleClient.OracleException oraex)
				{
					throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(
						MessageSingleton.FATAL_DB_ERROR) + oraex.Message );
				}	
				catch (DbAccessException)
				{
					throw new UIFatalException(MessageSingleton.GetInstance().GetMessage
						(MessageSingleton.FATAL_DB_ERROR));
				}
			}
		}


		/// <summary>
		/// Save changes to credentials of prec medcal 
		/// Update dataset and save to database
		/// </summary>
		/// <exception cref="de.pta.Component.ErrorHandling.UIWarningException"> UIWarningException</exception> 
		/// thrown if any compulsory textfields are empty
		/// <exception cref="System.Data.DBConcurrencyException">DBConcurrencyException</exception>
		/// thrown if records in database diffrent form those in dataset (ie if another user has updated)
		internal void SaveChangesMedical()
		{
			string insNewPrecMedKind = ((FrmAdministration) mView).frmUCAdminMedicalPrecautionary1.TxtEditKind.Text;
			string insNewPrecMedNota = ((FrmAdministration) mView).frmUCAdminMedicalPrecautionary1.TxtEditPrecautionaryMedical.Text;
			string insNewPrecMedHelp = ((FrmAdministration) mView).frmUCAdminMedicalPrecautionary1.TxtEditHelp.Text;

			// If textfields are empty generate a warning
			if (insNewPrecMedKind.Length < 1 || insNewPrecMedNota.Length < 1)
			{
				throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(
					MessageSingleton.NO_ADMIN_TO_SAVE ));
			}
			else
			{
				// Else make sure no funny characters
				CheckSpecialCharacterString(insNewPrecMedKind);
				CheckSpecialCharacterString(insNewPrecMedNota);
				CheckSpecialCharacterString(insNewPrecMedHelp);
			}
				
			// Execute the Update on the adaptor for the mDSPrecMedType DataSet
			if(null != mDSPrecMedType && null != mDataAdapterPrecMedical && null != mCurrentPrecMedRow )
			{				
				// For each row in the mDSPrecMedType dataset check if the datarow has been changed
				foreach (DataRow dlr in mDSPrecMedType.FPASS_PRECMEDTYPE)
				{
					if (dlr.RowState.ToString().Equals(DS_ROW_UNCHANGED) == false) 
					{
						dlr["pmty_changeuser"] = this.mCurrentUserID;
						dlr["pmty_timestamp"]  = DateTime.Now;
						dlr.EndEdit();
					}
				}
				try
				{
					this.FormatForSavePrecMedDataSet();
					this.mDataAdapterPrecMedical.Update(mDSPrecMedType);
					this.mDSPrecMedType.AcceptChanges();
					this.FormatForDisplayPrecMedDataSet();
					((FrmAdministration) mView).frmUCAdminMedicalPrecautionary1.ContentChanged = false;
				}
				catch (DBConcurrencyException)
				{
					((FrmAdministration) mView).frmUCAdminPlant1.ContentChanged = false;
					throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(
						MessageSingleton.ADMIN_UPDATE_CONFL ));
				}	
				catch (System.Data.OracleClient.OracleException oraex)
				{
					throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(
						MessageSingleton.FATAL_DB_ERROR) + oraex.Message );
				}
				catch (DbAccessException)
				{
					throw new UIFatalException(MessageSingleton.GetInstance().GetMessage
						(MessageSingleton.FATAL_DB_ERROR));
				}
			}
		}

		/// <summary>
		/// Convert empty strings to DbNull before written to database
		/// </summary>
		private void FormatForSavePrecMedDataSet()
		{
			foreach (DataRow dr in mDSPrecMedType.FPASS_PRECMEDTYPE)
			{
				for (int i = 0; i < mDSPrecMedType.Tables["FPASS_PRECMEDTYPE"].Columns.Count; i++)
				{
					if ( dr[i].ToString().Length < 1)
					{					
						dr[i] = DBNull.Value;
					}
				}				
			} 
		}

		/// <summary>
		/// Convert DbNull to empty string so they look nice in DataGrid
		/// </summary>
		private void FormatForDisplayPrecMedDataSet()
		{
			foreach (DataRow dr in mDSPrecMedType.FPASS_PRECMEDTYPE)
			{
				for (int i = 0; i < mDSPrecMedType.Tables["FPASS_PRECMEDTYPE"].Columns.Count; i++)
				{
					if ( dr[i].Equals(DBNull.Value) )
					{					
						dr[i] = "";
					}
				}				
			} 
		}


		/// <summary>
		/// Delete current prec medical
		/// Row is deleted from dataset but delete at database level is executed via stored procedure
		/// Current datarow must be the one currently selected in Grid.
		/// After deleting, form is emptied to avoid problems with inconsistent datasets 11.12.03
		/// </summary>
		/// <exception cref="System.Data.OracleClient.OracleException">OracleException</exception> 
		/// ORA-20001 is returned by stored procedure if dependent data exist
		internal void DeleteMedical()
		{
			mCurrentAdminID = ((FrmAdministration) mView).frmUCAdminMedicalPrecautionary1.CurrentAdminRec;
								
			if (null != mCurrentPrecMedRow)
			{
				try
				{
					// Delete row in DB via stored procedure, not via dataset
					this.DeleteViaStoredProcedure(mCurrentAdminID, SP_DELPRECMED, mDataAdapterPrecMedical);				
					mCurrentPrecMedRow.Delete();	
					this.mDSPrecMedType.AcceptChanges();	
					this.SetCurrentAdminIDToDefault();
					//this.ClearTextFields(true);
					DiscardAllChanges();					
				}
				catch (DBConcurrencyException)
				{
					((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.Default;
					throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(
						MessageSingleton.ADMIN_UPDATE_CONFL ));
				}	
				catch (System.Data.OracleClient.OracleException oraex)
				{
					if ( cmdCallSP.Connection != null )
					{
						((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.Default;
						cmdCallSP.Connection.Close();
					}
					// Is this an exception from dependent data?
					if ( oraex.Message.IndexOf("20001") != -1 )
					{
						throw new UIWarningException( MessageSingleton.GetInstance().GetMessage(
							MessageSingleton.ADMIN_DEPENDENT_DATA) );
					}
					else
					{
						throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(
							MessageSingleton.FATAL_DB_ERROR) + oraex.Message );
					}
				}
				catch (DbAccessException)
				{
					throw new UIFatalException(MessageSingleton.GetInstance().GetMessage
						(MessageSingleton.FATAL_DB_ERROR));
				}
			}	
		}


		#endregion // End of PrecautionaryMedicals

		#region Plant_Methods

		/// <summary>
		/// Mo methods for formaating the dataset as all database table fields are compulsory
		/// </summary>
		
		/// <summary>
		/// selects the Plants which match the given search criteria and fills the DataSet mDSPlant
		/// DbNulls formatted to "" for display
		/// </summary>
		/// <exception cref="de.pta.Component.ErrorHandling.UIWarningException">UIWarningException</exception>
		/// thrown if no results returned
		public void GetPlants() 
		{
			int numrecs;
	
			this.SetPlantSearchCriteria();
			
			mProvider.SetParameter(mDataAdapterPlant, PLANT_MAND_PARA, UserManagementControl.getInstance().CurrentMandatorID.ToString());
			mProvider.SetParameter(mDataAdapterPlant, ":PL_NAME", mPlantParameter);

			try
			{
				numrecs = mProvider.FillDataSet(PLANT_ADA_ID, mDataAdapterPlant, mDSPlant);
				if ( numrecs < 1 ) 
				{
					throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.NO_RESULTS));
				}
				else
				{				
					foreach (DataRow dr in mDSPlant.FPASS_PLANT)
					{
						for (int i = 0; i < mDSPlant.Tables["FPASS_PLANT"].Columns.Count; i++)
						{
							if (dr[i].Equals(DBNull.Value))
							{
								dr[i] = "";
							}
						}				
					}
					((FrmAdministration) mView).frmUCAdminPlant1.DgrPlant.DataSource = mDSPlant;
					((FrmAdministration) mView).frmUCAdminPlant1.DgrPlant.DataMember = PLANT_TABLE;
				}
			}
			catch (System.Data.OracleClient.OracleException oraex)
			{
				throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(
					MessageSingleton.FATAL_DB_ERROR) + oraex.Message );
			}
			catch (DbAccessException)
			{
				throw new UIFatalException(MessageSingleton.GetInstance().GetMessage
					(MessageSingleton.FATAL_DB_ERROR));
			}	
		}


		/// <summary>
		/// Set parameter values according to ID chosen in combobox
		/// No WHERE clause if nothing selected in comboboxes: show all results
		/// </summary>
		private void SetPlantSearchCriteria() 
		{
			this.mPlantParameter = this.GetSelectedValueFromCbo(((FrmAdministration) mView).frmUCAdminPlant1.CboSearchPlant);
			if ( mPlantParameter.Length < 1 ) 
			{
				this.mPlantParameter = "%";
			}
		}

		/// <summary>
		/// Load individual plant for edit: 
		/// get primary key of currently selected plant by selecting datarow by PK from dataset 
		/// Bind data to textfields in GUI
		/// </summary>
		public void LoadIndividualPlant()
		{
            var frmAdminView = mView as FrmAdministration;
            int myPKVal = frmAdminView.frmUCAdminPlant1.CurrentAdminRec; 
            
            frmAdminView.frmUCAdminPlant1.ContentChanged = false;
            frmAdminView.frmUCAdminPlant1.TxtEditPlant.Enabled = false;            
		
			// individual editing: get primary key of currently selected craft
			mDSPlant.Tables[PLANT_TABLE].PrimaryKey = new DataColumn[] {mDSPlant.Tables[PLANT_TABLE].Columns[PLANT_PK_COL] };
			mCurrentPlantRow = mDSPlant.Tables[PLANT_TABLE].Rows.Find(myPKVal);
  
			if (null != mCurrentPlantRow )
            {					
                frmAdminView.frmUCAdminPlant1.TxtEditPlant.DataBindings.Add("Text", mCurrentPlantRow, "PL_NAME");
               
                // Plant row can only be edited if its source is "FPASS"
                // (plants imported from ZKS are read-only).
                if ((mCurrentPlantRow as DSPlant.FPASS_PLANTRow).PL_SOURCE == Globals.PLANT_SOURCE_FPASS)
                {
                    mCurrentPlantRow.BeginEdit();
                    frmAdminView.frmUCAdminPlant1.TxtEditPlant.Enabled = true;                   
                }
			}				
		}

		/// <summary>
		/// Enable textfield "name" to enter new details
		/// </summary>
		internal void CreateNewPlant()
		{			
			((FrmAdministration)mView).frmUCAdminPlant1.TxtEditPlant.Enabled = true;
		}

		/// <summary>
		/// Saves inserts to typified dataset for plants. Get new PK values from sequence,
		/// creating new DataRow, adding to dataset and saving to database
		/// </summary>
		/// <exception cref="de.pta.Component.ErrorHandling.UIWarningException"> UIWarningException</exception> 
		/// thrown if any compulsory textfields are empty
		/// </summary>
		internal void SaveNewPlant()
		{
			string insNewPlant = ((FrmAdministration) mView).frmUCAdminPlant1.TxtEditPlant.Text;			
			if (insNewPlant.Length < 1)
			{
				throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.NO_ADMIN_TO_SAVE ));
			}
			else
			{
                // Make sure name does not already exist
                string[] existingPlants = FPASSLovsSingleton.GetInstance().GetPlants();
                IEnumerator enumerator = existingPlants.GetEnumerator();

                while (enumerator.MoveNext())
                {
                    string plName = (string)enumerator.Current;
                    if (insNewPlant == plName)
                    {
                        throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.ADMIN_PLANT_EXISTS));
                    }
                }
				CheckSpecialCharacterString(insNewPlant);
			}
			
			if(null != mDSPlant && null != mDataAdapterPlant)
			{
				decimal newPKVal = 0;
				
				try
				{
					// Get new PK from sequence: DataReader
					newPKVal = Convert.ToInt32( this.GetNextValFromSeq("seq_plant") );

					mDSPlant.FPASS_PLANT.AddFPASS_PLANTRow(
                        mCurrentUserID,
						DateTime.Now,
						newPKVal,
						mCurrentMandID,
						insNewPlant,
                        Globals.STATUS_VALID, 
                        Globals.PLANT_SOURCE_FPASS);

					this.mDataAdapterPlant.Update(mDSPlant);
					this.mDSPlant.AcceptChanges();
					this.ClearTextFields(true);
					// 27.05.04: reload record
					((FrmAdministration) mView).frmUCAdminPlant1.DgrPlant.DataSource = mDSPlant;
					((FrmAdministration) mView).frmUCAdminPlant1.DgrPlant.DataMember = PLANT_TABLE;
					((FrmAdministration) mView).frmUCAdminPlant1.ContentChanged = false;
				}
				catch (System.Data.OracleClient.OracleException oraex)
				{
					throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR) + oraex.Message );
				}
				catch (DbAccessException)
				{
					throw new UIFatalException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR));
				}
			}	
		}


		/// <summary>
		/// Saves updates to typified dataset for plants.
		/// </summary>
		/// <exception cref="System.Data.DBConcurrencyException">DBConcurrencyException</exception>
		/// is thrown if records in database diffrent form those in dataset (ie if another user has updated)
		/// <exception cref="de.pta.Component.ErrorHandling.UIWarningException"> UIWarningException</exception> 
		/// thrown if any compulsory textfields are empty
		internal void SaveChangesPlant()
		{
			string insNewPlant = ((FrmAdministration) mView).frmUCAdminPlant1.TxtEditPlant.Text;

			if (insNewPlant.Length < 1)
			{
				throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.NO_ADMIN_TO_SAVE ));
			}
			else
			{
                // Make sure name does not already exist
                string[] existingPlants = FPASSLovsSingleton.GetInstance().GetPlants();
                IEnumerator enumerator = existingPlants.GetEnumerator();

                while (enumerator.MoveNext())
                {
                    string plName = (string)enumerator.Current;
                    if (insNewPlant == plName)
                    {
                        throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.ADMIN_PLANT_EXISTS));
                    }
                }
				CheckSpecialCharacterString(insNewPlant);
			}

			mCurrentAdminID = ((FrmAdministration) mView).frmUCAdminPlant1.CurrentAdminRec;

			if(null != mDSPlant && null != mDataAdapterPlant  && null != mCurrentPlantRow )
			{				
				// For each row in the Plant dataset check if the datarow has been changed
				foreach (DataRow dlr in mDSPlant.FPASS_PLANT)
				{
					if (dlr.RowState.ToString().Equals(DS_ROW_UNCHANGED) == false) 
					{
						dlr["pl_changeuser"] = this.mCurrentUserID;
						dlr["pl_timestamp"] = DateTime.Now;
						dlr.EndEdit();
					}
				}
				try
				{
					this.mDataAdapterPlant.Update(mDSPlant);
					this.mDSPlant.AcceptChanges();	
					((FrmAdministration) mView).frmUCAdminPlant1.ContentChanged = false;
				}
				catch (DBConcurrencyException)
				{
					((FrmAdministration) mView).frmUCAdminPlant1.ContentChanged = false;
					throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.ADMIN_UPDATE_CONFL ));
				}	
				catch (System.Data.OracleClient.OracleException oraex)
				{
					throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR) + oraex.Message );
				}
				catch (DbAccessException)
				{
					throw new UIFatalException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR));
				}
			}
		}

		/// <summary>
		/// Delete current plant
		/// Row is deleted from dataset but delete at database level is executed via stored procedure
		/// After deleting, form is emptied to avoid problems with inconsistent datasets 11.12.03
		/// </summary>
		/// <exception cref="System.Data.OracleClient.OracleException">OracleException</exception> 
		/// ORA-20001 is returned by stored procedure if dependent data exist 
		public void DeletePlant()
		{
			mCurrentAdminID = ((FrmAdministration) mView).frmUCAdminPlant1.CurrentAdminRec;					
			if (null != mCurrentPlantRow)
			{
				try
				{
					this.DeleteViaStoredProcedure(mCurrentAdminID, SP_DELPLANT, mDataAdapterPlant);
					
					mCurrentPlantRow.Delete();
					this.mDSPlant.AcceptChanges();	
					this.SetCurrentAdminIDToDefault();
					DiscardAllChanges();
				}
				catch (DBConcurrencyException)
				{
					throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.ADMIN_UPDATE_CONFL ));
				}
				catch (System.Data.OracleClient.OracleException oraex)
				{
					if ( cmdCallSP.Connection != null )
					{
						cmdCallSP.Connection.Close();
					}
					// Is this an exception from dependent data?
					if ( oraex.Message.IndexOf("20001") != -1 )
					{
						throw new UIWarningException( MessageSingleton.GetInstance().GetMessage(MessageSingleton.ADMIN_DEPENDENT_DATA) );
					}
					else
					{
						throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR) + oraex.Message );
					}
				}
				catch (DbAccessException)
				{
					throw new UIFatalException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR ));
				}	
			}	
		}


		#endregion // Plant_Methods


		#region Department_Methods	
		/// <summary>
		/// Methods for administration of departments
		/// </summary> 

		/// <summary>
		/// Loads the department selected in the datagrid & displays in textboxes in lower part of form.
		/// Ask Form/View for the PK ID of current row, select datarow by PK from dataset 
		/// </summary>
		public void LoadIndividualDepartment()
		{		
			int myPKVal = ((FrmAdministration) mView).frmUCAdminDepartment1.CurrentAdminRec;

			mDSDepartment.FPASS_DEPARTMENT.PrimaryKey = new DataColumn[] {mDSDepartment.FPASS_DEPARTMENT.Columns[DEPT_PK_COL] };
			mCurrentDeptRow = mDSDepartment.FPASS_DEPARTMENT.Rows.Find(myPKVal);

			if(null != mCurrentDeptRow)
			{
				mCurrentDeptRow.BeginEdit();
				((FrmAdministration) mView).frmUCAdminDepartment1.TxtEditDepartment.Enabled = true;
				((FrmAdministration) mView).frmUCAdminDepartment1.TxtEditDepartment.DataBindings.Add("Text", mCurrentDeptRow, "DEPT_DEPARTMENT");
				((FrmAdministration) mView).frmUCAdminDepartment1.ContentChanged = false;
			}	
		}

		/// <summary>
		/// selects the Departments which match given search criteria and fill DataSet mDSDepartment (mandant-dependent as always)
		/// </summary>
		/// <exception cref="de.pta.Component.ErrorHandling.UIWarningException">UIWarningException</exception>
		/// thrown if no results returned
		public void GetDepartments()
		{				
			int numrecs;

			this.mDSDepartment.Clear();			
			this.SetDeptWhereClause();

			mProvider.SetParameter(mDataAdapterDept, DEPT_MAND_PARA, 
				UserManagementControl.getInstance().CurrentMandatorID.ToString());
			
			try
			{
				numrecs = mProvider.FillDataSet(DEPT_ADA_ID, mDataAdapterDept, mDSDepartment);
				if ( numrecs < 1 ) 
				{
					throw new UIWarningException(MessageSingleton.GetInstance().GetMessage
						(MessageSingleton.NO_RESULTS));
				}
				else
				{
					// Format NULLS to avoid exceptions
					foreach (DataRow dr in mDSDepartment.FPASS_DEPARTMENT)
					{
						for (int i = 0; i < mDSDepartment.Tables["FPASS_DEPARTMENT"].Columns.Count; i++)
						{
							if (dr[i].Equals(DBNull.Value))
							{
								dr[i] = "";
							}
						}				
					}
					// Bind to datagrid
					((FrmAdministration) mView).frmUCAdminDepartment1.DgrDepartment.DataSource = mDSDepartment;
					((FrmAdministration) mView).frmUCAdminDepartment1.DgrDepartment.DataMember = DEPT_TABLE;
				}
			}
			catch (System.Data.OracleClient.OracleException oraex)
			{
				throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(
					MessageSingleton.FATAL_DB_ERROR) + oraex.Message );
			}
			catch (DbAccessException)
			{
				throw new UIFatalException(MessageSingleton.GetInstance().GetMessage
					(MessageSingleton.FATAL_DB_ERROR));
			}			
		}

		/// <summary>
		/// WHERE clause for SQL select on department is generated dynamically: use text from textbox
		/// If no value selected show all records
		/// </summary>
		private void SetDeptWhereClause()
		{
			mDataAdapterDept  = mProvider.CreateDataAdapter(DEPT_ADA_ID);

			//this.mDeptParameter = GetSelectedIDFromCbo(((FrmAdministration) mView).frmUCAdminDepartment1.CboSearchDepartment);
            mDeptParameter = ((FrmAdministration)mView).frmUCAdminDepartment1.TxtSearchDepartment.Text;
            mDeptParameter = mDeptParameter.Replace("*", "%").ToUpper();

			if ( mDeptParameter.Length < 1 )
			{
				mDataAdapterDept.SelectCommand.CommandText = mDataAdapterDept.SelectCommand.CommandText
															+ " ORDER BY NLSSORT(DEPT_DEPARTMENT, 'NLS_SORT=GERMAN')";
			}
			else
			{
				mDataAdapterDept.SelectCommand.CommandText = mDataAdapterDept.SelectCommand.CommandText
                                                            + " AND UPPER(DEPT_DEPARTMENT) LIKE '"
															+ mDeptParameter
                                                            + "' ORDER BY NLSSORT(DEPT_DEPARTMENT, 'NLS_SORT=GERMAN')";		
			}
		}


		/// <summary>
		/// Enable textfield to edit new department
		/// </summary>
		internal void CreateNewDepartment()
		{			
			((FrmAdministration)mView).frmUCAdminDepartment1.TxtEditDepartment.Enabled = true;
		}

		/// <summary>
		/// Save new Department details by getting new PK values from sequence,
		/// creating new DataRow, adding to dataset and saving to database
		/// </summary>
		/// <exception cref="de.pta.Component.ErrorHandling.UIWarningException"> UIWarningException</exception> 
		/// thrown if any compulsory textfields are empty
		internal void SaveNewDepartment()
		{
			// Get vals from textboxes			
			string insNewDepartment = ((FrmAdministration) mView).frmUCAdminDepartment1.TxtEditDepartment.Text;
			if (insNewDepartment.Length < 1)
			{
				throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(
					MessageSingleton.NO_ADMIN_TO_SAVE ));
			}
			else
			{
				CheckSpecialCharacterString(insNewDepartment);
			}

			// Create data adapter
			mDataAdapterDept = mProvider.CreateDataAdapter(DEPT_ADA_ID);
			
			decimal newPKVal = 0;
			try
			{
				// Get sequence val for PK from DataReader
				newPKVal = Convert.ToInt32( this.GetNextValFromSeq("seq_department"));

				mDSDepartment.FPASS_DEPARTMENT.AddFPASS_DEPARTMENTRow(
                    mCurrentUserID,
					DateTime.Now,
					newPKVal,
					mCurrentMandID,
					insNewDepartment,
                    Globals.STATUS_VALID);

				this.mDataAdapterDept.Update(mDSDepartment);
				this.mDSDepartment.AcceptChanges();		
				this.ClearTextFields(true);
				((FrmAdministration) mView).frmUCAdminDepartment1.DgrDepartment.DataSource = mDSDepartment;
				((FrmAdministration) mView).frmUCAdminDepartment1.DgrDepartment.DataMember = DEPT_TABLE;
				((FrmAdministration) mView).frmUCAdminDepartment1.ContentChanged = false;
			}
			catch (System.Data.OracleClient.OracleException oraex)
			{
				throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR) + oraex.Message );
			}
			catch (DbAccessException)
			{
				throw new UIFatalException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR));
			}
		}

		/// <summary>
		/// Save changes to credentials of department
		/// Update dataset and save to database
		/// </summary>
		/// <exception cref="System.Data.DBConcurrencyException">DBConcurrencyException</exception>
		/// is thrown if records in database diffrent form those in dataset (ie if another user has updated)
		/// <exception cref="de.pta.Component.ErrorHandling.UIWarningException"> UIWarningException</exception> 
		/// thrown if any compulsory textfields are empty
		internal void SaveChangesDepartment()
		{
	
			string insNewDepartment = ((FrmAdministration) mView).frmUCAdminDepartment1.TxtEditDepartment.Text;
			if (insNewDepartment.Length < 1)
			{
				throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(
					MessageSingleton.NO_ADMIN_TO_SAVE ));
			}
			else
			{
				CheckSpecialCharacterString(insNewDepartment);
			}
			
			// Get the ID of the record currently selected in the datagrid
			mCurrentAdminID = ((FrmAdministration) mView).frmUCAdminDepartment1.CurrentAdminRec;

		    // Create data adpater
			mDataAdapterDept  = mProvider.CreateDataAdapter(DEPT_ADA_ID);

			if(null != mDSDepartment && null != mDataAdapterDept && null != mCurrentDeptRow )
			{								
				// For each row in the Dept dataset check if the datarow has been changed
				foreach (DataRow dlr in mDSDepartment.FPASS_DEPARTMENT)
				{
					if (dlr.RowState.ToString().Equals(DS_ROW_UNCHANGED) == false) 
					{
						dlr["dept_changeuser"] = this.mCurrentUserID;
						dlr["dept_timestamp"] = DateTime.Now;
						dlr.EndEdit();					
					}
				}
				try
				{
					this.mDataAdapterDept.Update(mDSDepartment);
					this.mDSDepartment.AcceptChanges();	
					((FrmAdministration) mView).frmUCAdminDepartment1.ContentChanged = false;
				}
				catch (DBConcurrencyException)
				{
					throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(
						MessageSingleton.ADMIN_UPDATE_CONFL ));
				}	
				catch (System.Data.OracleClient.OracleException oraex)
				{
					throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(
						MessageSingleton.FATAL_DB_ERROR) + oraex.Message );
				}
				catch (DbAccessException)
				{
					throw new UIFatalException(MessageSingleton.GetInstance().GetMessage
						(MessageSingleton.FATAL_DB_ERROR));
				}
			}
		}

		/// <summary>
		/// Deletes the dept with the current ID by calling a DB stored procedure. 
		/// This allows the action with the current userID to be recorded.
		/// Row is deleted from dataset but delete at database level is executed via stored procedure
		/// After deleting, form is emptied to avoid problems with inconsistent datasets 11.12.03
		/// </summary>
		/// <exception cref="System.Data.OracleClient.OracleException">OracleException</exception> 
		/// ORA-20001 is returned by stored procedure if dependent data exist
		public void DeleteDepartment()
		{
			mDataAdapterDept     = mProvider.CreateDataAdapter(DEPT_ADA_ID);
			this.mCurrentAdminID = ((FrmAdministration) mView).frmUCAdminDepartment1.CurrentAdminRec;
					
			((FrmAdministration) mView).frmUCAdminDepartment1.ContentChanged = false;
			if (null != mCurrentDeptRow)
			{
				try
				{
					// Call Stored Proc
					this.DeleteViaStoredProcedure(mCurrentAdminID, SP_DELDEPT, mDataAdapterDept);
					// Update dataset
					mCurrentDeptRow.Delete();
					this.mDSDepartment.AcceptChanges();					
					this.SetCurrentAdminIDToDefault();
					// this.ClearTextFields(true);
					DiscardAllChanges();	

				}
				catch (DBConcurrencyException)
				{
					throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(
						MessageSingleton.ADMIN_UPDATE_CONFL ));
				}
				catch (System.Data.OracleClient.OracleException oraex )
				{
					if ( cmdCallSP.Connection != null )
					{
						cmdCallSP.Connection.Close();
					}
					// Is this an exception from dependent data?
					if ( oraex.Message.IndexOf("20001") != -1 )
					{
						throw new UIWarningException( MessageSingleton.GetInstance().GetMessage(
							MessageSingleton.ADMIN_DEPENDENT_DATA) );
					}
					else
					{
						throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(
							MessageSingleton.FATAL_DB_ERROR) + oraex.Message );
					}
				}
				catch (DbAccessException)
				{
					throw new UIFatalException(MessageSingleton.GetInstance().GetMessage
						(MessageSingleton.FATAL_DB_ERROR));
				}
			}	
		}

		#endregion // End of Department_Methods

		#region Craft_Methods
			
		
		/// <summary>
		/// selects the Crafts which match given search criteria and fills typified DataSet DSCraft
		/// Recreate data adaptor for each search so that SQL is correct
		/// </summary>
		/// <exception cref="de.pta.Component.ErrorHandling.UIWarningException">UIWarningException</exception>
		/// thrown if no results returned
		internal void GetCrafts() 
		{		
			int numrecs;
			this.mDSCraft.Clear();
			this.mDataAdapterCraft = null;

			this.SetCraftWhereClause();

			mDataAdapterCraft = mProvider.CreateDataAdapter(CRAFT_ADA_ID);
			mDataAdapterCraft.SelectCommand.CommandText = mDataAdapterCraft.SelectCommand.CommandText
				+ mCraftSqlWhere;
			mProvider.SetParameter(mDataAdapterCraft, CRAFT_MAND_PARA,
				UserManagementControl.getInstance().CurrentMandatorID.ToString());

			try
			{
				numrecs = mProvider.FillDataSet(CRAFT_ADA_ID, mDataAdapterCraft, mDSCraft);				
				if ( numrecs < 1 ) 
				{
					throw new UIWarningException(MessageSingleton.GetInstance().GetMessage
						(MessageSingleton.NO_RESULTS));
				}
				else
				{
					// Format NULLS to avoid exceptions
					foreach (DataRow dr in mDSCraft.FPASS_CRAFT)
					{
						for (int i = 0; i < mDSCraft.Tables["FPASS_CRAFT"].Columns.Count; i++)
						{
							if (dr[i].Equals(DBNull.Value))
							{
								dr[i] = "";
							}
						}				
					} 					
					((FrmAdministration) mView).frmUCAdminCraft1.DgrCraft.DataSource = mDSCraft;
					((FrmAdministration) mView).frmUCAdminCraft1.DgrCraft.DataMember = "FPASS_CRAFT";
				}
			}
			catch (System.Data.OracleClient.OracleException oraex)
			{
				throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(
					MessageSingleton.FATAL_DB_ERROR) + oraex.Message );
			}
			catch (DbAccessException)
			{
				throw new UIFatalException(MessageSingleton.GetInstance().GetMessage
					(MessageSingleton.FATAL_DB_ERROR));
			}
		}

	
		/// <summary>
		/// Dynamically generate WHERE clause for the SELECT statement of the Craft data adapter
		/// For each search parameter an extra condition is added to the SQL text: in this case it is always the PK craft ID
		/// Binding parameters to SQL from the combobox string values doesn't work
		/// </summary>
		private void SetCraftWhereClause() 
		{
			int	noSearchCriteria	   = 0;
			this.mCraftSqlWhere		   = String.Empty;
			this.mCraftNumberParameter = String.Empty;
			this.mCraftNotaParameter   = String.Empty;

			this.mCraftNumberParameter = this.GetSelectedIDFromCbo( ((FrmAdministration) mView).frmUCAdminCraft1.CboSearchCraftNumber );
			this.mCraftNotaParameter   = this.GetSelectedIDFromCbo( ((FrmAdministration) mView).frmUCAdminCraft1.CboSearchCraft );

			if ( mCraftNumberParameter.Length < 1 ) 
			{				
				noSearchCriteria ++;
			}
			else
			{
				mCraftSqlWhere = mCraftSqlWhere
					+ " AND CRA_ID = "
					+ mCraftNumberParameter;
			}

			if ( mCraftNotaParameter.Length < 1 ) 
			{
				noSearchCriteria ++;
			}
			else
			{
				mCraftSqlWhere = mCraftSqlWhere
					+ " AND CRA_ID = "
					+ mCraftNotaParameter;
			}
			mCraftSqlWhere = mCraftSqlWhere +  " ORDER BY NLSSORT(CRA_CRAFTNO, 'NLS_SORT=GERMAN')";

			if ( noSearchCriteria == 2 ) 
			{
				mCraftSqlWhere = " ORDER BY NLSSORT(CRA_CRAFTNO, 'NLS_SORT=GERMAN')";
			} 
		}


		/// <summary>
		/// Load one record from the dataset for individual editing
		/// get primary key of currently selected Craft: select datarow by PK from dataset 
		/// </summary>
		internal void LoadIndividualCraft()
		{			
			int myPKVal = ((FrmAdministration) mView).frmUCAdminCraft1.CurrentAdminRec;
		
			mDSCraft.Tables[CRAFT_TABLE].PrimaryKey = new DataColumn[] {mDSCraft.Tables[CRAFT_TABLE].Columns[CRAFT_PK_COL] };
			mCurrentCraftRow = mDSCraft.Tables[CRAFT_TABLE].Rows.Find(myPKVal);

			if(null != mCurrentCraftRow)
			{
				mCurrentCraftRow.BeginEdit();
				
				((FrmAdministration) mView).frmUCAdminCraft1.TxtEditCraftNumber.DataBindings.Add("Text", mCurrentCraftRow, "CRA_CRAFTNO");
				((FrmAdministration) mView).frmUCAdminCraft1.TxtEditCraft.DataBindings.Add("Text", mCurrentCraftRow, "CRA_CRAFTNOTATION");
				((FrmAdministration)mView).frmUCAdminCraft1.TxtEditCraft.Enabled       = true;
				((FrmAdministration)mView).frmUCAdminCraft1.TxtEditCraftNumber.Enabled = true;

				((FrmAdministration) mView).frmUCAdminCraft1.ContentChanged = false;
			}
		}

		/// <summary>
		/// Implementation is different from the other Admin modules: here a new datarow is created before any
		/// new values are entered, in all other modules the new datarow is not created until the Save button is pressed
		/// </summary>
		internal void CreateNewCraft()
		{			
			mDataAdapterCraft = mProvider.CreateDataAdapter(CRAFT_ADA_ID);
			if(null != mDSCraft && null != mDataAdapterCraft)
			{			
				((FrmAdministration)mView).frmUCAdminCraft1.TxtEditCraft.Enabled       = true;
				((FrmAdministration)mView).frmUCAdminCraft1.TxtEditCraftNumber.Enabled = true;
				
				if (null == mNewCraftRow)
				{
					// Get unique PK value from sequence associated with this table: hard-coded command
					decimal newPKVal = 0;
						
					// Get sequence ID from DataReader
					newPKVal = Convert.ToInt32( this.GetNextValFromSeq("seq_craft") );

					mNewCraftRow = mDSCraft.FPASS_CRAFT.NewFPASS_CRAFTRow();
					mNewCraftRow["CRA_CHANGEUSER"] = UserManagementControl.getInstance().CurrentUserID;
					mNewCraftRow["CRA_TIMESTAMP"] = DateTime.Now;
					mNewCraftRow["CRA_ID"] = newPKVal;
					mNewCraftRow["CRA_MND_ID"] = UserManagementControl.getInstance().CurrentMandatorID;
					mNewCraftRow["CRA_CRAFTNO"] = "";
					mNewCraftRow["CRA_CRAFTNOTATION"] = "";
				}
			}
		}

		/// <summary>
		/// Save the new craft record by saving new DataRow to database
		/// Must check new datarow has been created
		/// </summary>
		/// <exception cref="de.pta.Component.ErrorHandling.UIWarningException"> UIWarningException</exception> 
		/// thrown if any compulsory textfields are empty
		internal void SaveNewCraft()
		{ 
			if (mNewCraftRow == null)
			{
				this.CreateNewCraft();
			}
					
			string insNewCraft   = ((FrmAdministration) mView).frmUCAdminCraft1.TxtEditCraft.Text;
			string insNewCraftNo = ((FrmAdministration) mView).frmUCAdminCraft1.TxtEditCraftNumber.Text;			
			if (insNewCraft.Length < 1 || insNewCraftNo.Length < 1)
			{
				throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(
					MessageSingleton.NO_ADMIN_TO_SAVE ));
			}
			else
			{
				CheckSpecialCharacterString( insNewCraft );
				CheckSpecialCharacterString( insNewCraftNo );
			}

			if (null != mNewCraftRow)
			{
				try
				{
					mNewCraftRow["CRA_CRAFTNO"]       = insNewCraftNo;
					mNewCraftRow["CRA_CRAFTNOTATION"] = insNewCraft;
					mDSCraft.FPASS_CRAFT.Rows.Add(mNewCraftRow);
					this.mDataAdapterCraft.Update(mDSCraft);
					this.mDSCraft.AcceptChanges();

					mNewCraftRow = null;
					this.ClearTextFields(true);

					this.ClearTextFields(true);
					((FrmAdministration) mView).frmUCAdminCraft1.DgrCraft.DataSource = mDSCraft;
					((FrmAdministration) mView).frmUCAdminCraft1.DgrCraft.DataMember = CRAFT_TABLE;

					((FrmAdministration) mView).frmUCAdminCraft1.ContentChanged = false;
				}
				catch (System.Data.OracleClient.OracleException oraex)
				{
					throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(
						MessageSingleton.FATAL_DB_ERROR) + oraex.Message );
				}
				catch (DbAccessException)
				{
					throw new UIFatalException(MessageSingleton.GetInstance().GetMessage
						(MessageSingleton.FATAL_DB_ERROR));
				}	
			}
		}


		/// <summary>
		/// Saves updates to typified dataset for crafts.
		/// </summary>
		/// <exception cref="System.Data.DBConcurrencyException">DBConcurrencyException</exception>
		/// is thrown if records in database diffrent form those in dataset (ie if another user has updated)
		/// <exception cref="de.pta.Component.ErrorHandling.UIWarningException"> UIWarningException</exception> 
		/// thrown if any compulsory textfields are empty
		internal void SaveChangesCraft()
		{
			mDataAdapterCraft    = mProvider.CreateDataAdapter(CRAFT_ADA_ID);

			string insNewCraft   = ((FrmAdministration) mView).frmUCAdminCraft1.TxtEditCraft.Text;
			string insNewCraftNo = ((FrmAdministration) mView).frmUCAdminCraft1.TxtEditCraftNumber.Text;			

			if (insNewCraft.Length < 1 || insNewCraftNo.Length < 1)
			{
				throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(
					MessageSingleton.NO_ADMIN_TO_SAVE ));
			}
			else
			{
				CheckSpecialCharacterString( insNewCraft );
				CheckSpecialCharacterString( insNewCraftNo );
			}
			
			if(null != mDSCraft && null != mDataAdapterCraft && null != mCurrentCraftRow )
			{				
				if (mCurrentCraftRow.RowState.ToString().Equals(DS_ROW_UNCHANGED) == false) 
				{
					mCurrentCraftRow["CRA_CRAFTNO"] = insNewCraftNo;
					mCurrentCraftRow["CRA_CRAFTNOTATION"] = insNewCraft;
					mCurrentCraftRow["CRA_CHANGEUSER"] = this.mCurrentUserID;
					mCurrentCraftRow["CRA_TIMESTAMP"] = DateTime.Now;
					mCurrentCraftRow.EndEdit();						
				}
				try
				{
					this.mDataAdapterCraft.Update(mDSCraft);
					this.mDSCraft.AcceptChanges();	
					((FrmAdministration) mView).frmUCAdminCraft1.ContentChanged = false;
				}
				catch (DBConcurrencyException)
				{
					((FrmAdministration) mView).frmUCAdminCraft1.ContentChanged = false;
					throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(
						MessageSingleton.ADMIN_UPDATE_CONFL ));
				}
				catch (System.Data.OracleClient.OracleException oraex)
				{
					throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(
						MessageSingleton.FATAL_DB_ERROR) + oraex.Message );
				}
				catch (DbAccessException)
				{
					throw new UIFatalException(MessageSingleton.GetInstance().GetMessage
						(MessageSingleton.FATAL_DB_ERROR));
				}		 
			}
		}
		
		/// <summary>
		/// Delete current craft
		/// Row is deleted from dataset but delete at database level is executed via stored procedure
		/// After deleting, form is emptied to avoid problems with inconsistent datasets 11.12.03
		/// </summary>
		/// <exception cref="System.Data.OracleClient.OracleException">OracleException</exception> 
		/// ORA-20001 is returned by stored procedure if dependent data exist
		/// <exception cref="System.Data.DBConcurrencyException">DBConcurrencyException</exception>
		/// thrown if records in database diffrent form those in dataset (ie if another user has updated)
		internal void DeleteCraft()
		{
			mDataAdapterCraft = mProvider.CreateDataAdapter(CRAFT_ADA_ID);
			mCurrentAdminID   = ((FrmAdministration) mView).frmUCAdminCraft1.CurrentAdminRec;
		
			// Check this: current datarow must be the one currently selected in Grid.			
			if (mCurrentAdminID != -1 && null != mCurrentCraftRow)
			{
				try
				{
					// Call Stored Proc
					this.DeleteViaStoredProcedure(mCurrentAdminID, SP_DELCRAFT, mDataAdapterCraft);	
					
					mCurrentCraftRow.Delete();
					this.mDSCraft.AcceptChanges();						
					this.SetCurrentAdminIDToDefault();
					((FrmAdministration) mView).Cursor = System.Windows.Forms.Cursors.Default;
					// this.ClearTextFields(true);
					DiscardAllChanges();	
				}
				catch (DBConcurrencyException)
				{
					throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(
						MessageSingleton.ADMIN_UPDATE_CONFL ));
				}	
				catch (System.Data.OracleClient.OracleException oraex )
				{
					if ( cmdCallSP.Connection != null )
					{
						cmdCallSP.Connection.Close();
					}
					// Is this an exception from dependent data?
					if ( oraex.Message.IndexOf("20001") != -1 )
					{
						throw new UIWarningException( MessageSingleton.GetInstance().GetMessage(
							MessageSingleton.ADMIN_DEPENDENT_DATA) );
					}
					else
					{
						throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(
							MessageSingleton.FATAL_DB_ERROR) + oraex.Message );
					}
				}
				catch (DbAccessException)
				{
					throw new UIFatalException(MessageSingleton.GetInstance().GetMessage
						(MessageSingleton.FATAL_DB_ERROR));
				}
			}
		}

		#endregion // End of Craft_Methods

	}
}

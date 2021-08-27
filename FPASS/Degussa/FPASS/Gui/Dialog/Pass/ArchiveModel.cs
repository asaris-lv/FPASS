using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

using de.pta.Component.DbAccess;
using de.pta.Component.Errorhandling;
using de.pta.Component.ListOfValues;


using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.Util.UserManagement;
using Degussa.FPASS.Util.Exceptions;
using Degussa.FPASS.Bo.Pass;
using Degussa.FPASS.Db;
using Degussa.FPASS.Bo.Search;

namespace Degussa.FPASS.Gui.Dialog.Pass
{
	/// <summary>
	/// An ArchiveModel is the model of the MVC-triad ArchiveModel,
	/// ArchiveController and FrmArchive.
	/// ArchiveModel extends from the FPASSBaseModel.
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
	public class ArchiveModel : FPASSBaseModel
	{
		#region Members

		//Database access
		private const string ARCHIVE_QUERY		= "ArchiveCoWorker";

		// holds the search parameters from gui
		private string		 mSurnameParameter;
		private string		 mFirstnameParameter;
		private string   	 mCoordinatorIDParameter;
		private string		 mSubcontractorParameter;
		private string		 mSupervisorParameter;
		private string		 mContractorParameter;
		
		// ArrayList is filled here and given to form
		private ArrayList arlCOWORKER;
		

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public ArchiveModel()
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

		#endregion //End of Accessors

		#region Methods 


		/// <summary>
		/// Standard PreClose method
		/// Last change 02.03.2004: added call to ClearFields
		/// </summary>
		internal override void PreClose()
		{
			ClearStatusBar();
			ClearFields();
		}


		/// <summary>
		/// New 02.03.2004
		/// Empty contents of form
		/// Need to disable datagrid as not properly emptied (Bug .NET)
		/// </summary>
		internal void ClearFields()
		{
			this.ClearStatusBar();

			// Clear cbxs and textfields in form
			((FrmArchive) mView).CboSearchCoordinator.SelectedValue = 0;
			((FrmArchive) mView).CboSearchExternalContractor.SelectedValue = 0;
			((FrmArchive) mView).CboSearchSubcontractor.SelectedValue = 0;
			((FrmArchive) mView).CboSearchSupervisor.SelectedValue = 0;
			((FrmArchive) mView).TxtSearchFirstname.Text = "";
			((FrmArchive) mView).TxtSearchSurname.Text   = "";

			//Disable buttons
			((FrmArchive) mView).BtnDetails.Enabled = false;

			// Clear datagrid
			((FrmArchive) mView).DgrCoWorker.DataSource = null;
			((FrmArchive) mView).DgrCoWorker.DataBindings.Clear();
			((FrmArchive) mView).DgrCoWorker.Enabled = false;
		}
		
		
		/// <summary>
		/// selects the CoWorker which match the given search criteria
		/// <exception cref=""> throws FPASSWarningException when user changes 
		/// the content of the comboboxes, when no search criteria is set
		/// and when search had no results</exception>
		/// 24.02.2004: DataTable Column DateOfBirth changed to type DateTime so can be sorted chronologically (default String)
		/// 02.03.2004: added enable/disable button "CWR details"
		/// </summary>
		internal void GetCoWorkers() 
		{
			((FrmArchive) mView).Cursor = System.Windows.Forms.Cursors.WaitCursor;
			((FrmArchive) mView).DgrCoWorker.DataSource = null;

			// read the search criteria from the gui
			this.CopyOutSearchCriteria();

			// Get DataProvider from DbAccess component
			IProvider mProvider = DBSingleton.GetInstance().DataProvider;

			// Create the select command & fill Data Reader 
			IDbCommand selComm = mProvider.CreateCommand(ARCHIVE_QUERY);
			selComm.CommandText = GenerateWhereClause( selComm.CommandText );
		
			// Instantiate Arraylist
			arlCOWORKER = new ArrayList();

			// Create a datatable at runtime: this is bound to datagrid to allow sorting
			// Last change 24.02.2004
			// Column DateOfBirth changed to type DateTime so can be sorted chronologically (default String)
			DataRow row;
			DataTable table = new DataTable("RTTabCoWorker");	// name of table as in Table Mappings
            table.Columns.Add(new DataColumn(CoWorkerSearch.ID_COL));
            table.Columns.Add(new DataColumn(CoWorkerSearch.SURNAME));
            table.Columns.Add(new DataColumn(CoWorkerSearch.FIRSTNAME));
            table.Columns.Add(new DataColumn(CoWorkerSearch.DATE_OF_BIRTH, typeof(System.DateTime)));
            table.Columns.Add(new DataColumn(CoWorkerSearch.VALID_UNTIL));
            table.Columns.Add(new DataColumn(CoWorkerSearch.EXCONTRACTOR));
            table.Columns.Add(new DataColumn(CoWorkerSearch.SUPERVISOR));
            table.Columns.Add(new DataColumn(CoWorkerSearch.SUBCONTRACTOR));
            table.Columns.Add(new DataColumn(CoWorkerSearch.COORDINATOR));
            table.Columns.Add(new DataColumn(CoWorkerSearch.STATUS));
            table.Columns.Add(new DataColumn(CoWorkerSearch.ZKS_RET));

			// Open data reader to get CoWorker data
			IDataReader mDR = mProvider.GetReader(selComm);

			// Loop thru records and create an ArrayList of CoWorker BOs
			int i = 0;
			while (mDR.Read())
			{
				mCoWorkerBO = new CoWorkerSearch();
				row = table.NewRow();

				mCoWorkerBO.CoWorkerId = Convert.ToDecimal(mDR["ACWR_ID"]);
				mCoWorkerBO.Surname	= mDR["AVWS_SURNAME"].ToString();
				mCoWorkerBO.Firstname = mDR["AVWS_FIRSTNAME"].ToString();
				mCoWorkerBO.DateOfBirth = Convert.ToDateTime( mDR["AVWS_DATEOFBIRTH"]).Date.ToString("dd.MM.yyyy");
				mCoWorkerBO.ValidUntil = mDR["AVWS_VALIDUNTIL"].ToString();
				mCoWorkerBO.ExContractorName = mDR["AVWS_EXCONAME"].ToString();
				mCoWorkerBO.Supervisor = mDR["AVWS_SUPERNAME"].ToString();
				mCoWorkerBO.SupervisTel	= mDR["AVWS_SUPERTEL"].ToString();
				mCoWorkerBO.SubContractor = mDR["AVWS_SUBNAME"].ToString();
				mCoWorkerBO.Coordinator	= mDR["AVWS_COORD_SURNAME"].ToString();
				mCoWorkerBO.CoordTel = mDR["AVWS_COORD_TEL"].ToString();
				mCoWorkerBO.Status= mDR["AVWS_STATUS"].ToString();
				mCoWorkerBO.ZKSReturncode = mDR["AVWS_RETURNCODE_ZKS"].ToString();

				mCoWorkerBO.SuperNameAndTel = mCoWorkerBO.Supervisor 
					+ "  (Tel. " + mCoWorkerBO.SupervisTel + ")";

				mCoWorkerBO.CoordNameAndTel = mCoWorkerBO.Coordinator 
					+ "  (Tel. " + mCoWorkerBO.CoordTel + ")";
				
				arlCOWORKER.Add(mCoWorkerBO);	
		
				// Create new row & write BO attributes in
				// (arraylist kept for backward compatibility)
				row.ItemArray = new object[11] {mCoWorkerBO.CoWorkerId,
												   mCoWorkerBO.Surname,
												   mCoWorkerBO.Firstname,
												   mCoWorkerBO.DateOfBirth,
												   mCoWorkerBO.ValidUntil,
												   mCoWorkerBO.ExContractorName,
												   mCoWorkerBO.SuperNameAndTel,		
												   mCoWorkerBO.SubContractor, 
												   mCoWorkerBO.CoordNameAndTel, 
												   mCoWorkerBO.Status,
												   mCoWorkerBO.ZKSReturncode};
				table.Rows.Add(row);
				i ++;
			}
			mDR.Close();
		

			if ( arlCOWORKER.Count > 0 ) 
			{
				((FrmArchive) mView).DgrCoWorker.DataSource = table;
				((FrmArchive) mView).DgrCoWorker.Enabled    = true;
				((FrmArchive) mView).BtnDetails.Enabled     = true;	
				this.ShowMessageInStatusBar( "Meldung: " + arlCOWORKER.Count +  " archivierte Datensätze gefunden" );
			} 
			else 
			{
				((FrmArchive) mView).BtnDetails.Enabled = false;
				ShowMessageInStatusBar(MessageSingleton.GetInstance().GetMessage (MessageSingleton.NO_RESULTS));
			}
			((FrmArchive) mView).Cursor = System.Windows.Forms.Cursors.Default;
		}


		internal CoWorkerSearch GetCoWorker(decimal pCurrentFFMAId) 
		{
			mCoWorkerBO = new CoWorkerSearch();
			
			if ( null != arlCOWORKER ) 
			{
				foreach	(CoWorkerSearch boCoWorker in arlCOWORKER) 
				{
					if ( boCoWorker.CoWorkerId.Equals(pCurrentFFMAId) ) 
					{
						mCoWorkerBO = boCoWorker;
						break;
					}
				} 
			}

			return mCoWorkerBO;
		}

		
		/// <summary>
		/// reads the search parameters from the gui
		/// <exception cref=""> throws FPASSWarningException when user changes the content of the combobox</exception>
		/// </summary>
		private void CopyOutSearchCriteria() 
		{
			int			noSearchCriteria = 0;

			this.mSurnameParameter = ((FrmArchive) mView).TxtSearchSurname.Text.Trim().Replace("*", "%");
			if ( mSurnameParameter.Length < 1 ) 
			{
				noSearchCriteria ++;
			}

			this.mFirstnameParameter = ((FrmArchive) mView).TxtSearchFirstname.Text.Trim().Replace("*", "%");
			if ( mFirstnameParameter.Length < 1 ) 
			{
				noSearchCriteria ++;
			}

			this.mCoordinatorIDParameter = this.GetSelectedIDFromCbo(((FrmArchive) mView).CboSearchCoordinator).Trim();
			if ( mCoordinatorIDParameter.Length < 1 ) 
			{
				noSearchCriteria ++;
			}

			mContractorParameter = this.GetSelectedValueFromCbo(((FrmArchive) mView).CboSearchExternalContractor).Trim();
			if ( mContractorParameter.Length < 1 )
			{
				noSearchCriteria ++;
			}

			mSubcontractorParameter = this.GetSelectedValueFromCbo(((FrmArchive) mView).CboSearchSubcontractor).Trim();
			if ( mSubcontractorParameter.Length < 1 )
			{
				noSearchCriteria ++;
			}

			// mSupervisorParameter = this.GetSelectedValueFromCbo( ((FrmArchive) mView).CboSearchSupervisor );
			mSupervisorParameter = this.GetSelectedIDFromCbo( ((FrmArchive) mView).CboSearchSupervisor );
			if ( mSupervisorParameter.Length < 1 )
			{
				noSearchCriteria ++;
			}

			if ( noSearchCriteria == 6 ) 
			{
				((FrmArchive) mView).Cursor = System.Windows.Forms.Cursors.Default;
				throw new UIWarningException(MessageSingleton.GetInstance().GetMessage
					(MessageSingleton.NO_SEARCH_CRITERIA));
			} 
		}


		private String GenerateWhereClause(String pSelect) 
		{
			String	whereClause = "";
		
			whereClause = " WHERE AVWS_MND_ID = " + UserManagementControl.getInstance().CurrentMandatorID;

			if ( this.mCoordinatorIDParameter.Length > 0 ) 
			{
				whereClause = whereClause + " AND AVWS_COORD_ID = " +  this.mCoordinatorIDParameter;
			}

			if ( this.mContractorParameter.Length > 1 ) 
			{
				whereClause = whereClause + " AND AVWS_EXCONAME = '" + this.mContractorParameter + "' "; 
			}

			if ( this.mSurnameParameter.Length > 1 ) 
			{
				whereClause = whereClause + " AND UPPER (AVWS_SURNAME) LIKE '" + this.mSurnameParameter.ToUpper() + "' "; 
			}

			if ( this.mFirstnameParameter.Length > 1 ) 
			{
				whereClause = whereClause + " AND UPPER (AVWS_FIRSTNAME) LIKE '" + this.mFirstnameParameter.ToUpper() + "' "; 
			}

			// Use the EXCO_ID of the current supervisor as search parameter in SQL
			if ( this.mSupervisorParameter.Length > 1 ) 
			{
				whereClause = whereClause + " AND AVWS_EXCO_ID = " + this.mSupervisorParameter; 
			}

			if ( this.mSubcontractorParameter.Length > 1 ) 
			{
				whereClause = whereClause + " AND AVWS_SUBNAME = '" + this.mSubcontractorParameter + "' "; 
			}

			// Add ORDER BY to very end 
			// Use NLS Sort German to get A, a, ...Z
			// and return
			string s = pSelect + whereClause;			
				   s = s + " ORDER BY NLSSORT(AVWS_EXCONAME, 'NLS_SORT=GERMAN'), " 
						 + "NLSSORT(AVWS_SURNAME, 'NLS_SORT=GERMAN'), " 
						 + "NLSSORT(AVWS_FIRSTNAME, 'NLS_SORT=GERMAN')";

			return s;
		}

		#endregion // End of Methods

	}
}

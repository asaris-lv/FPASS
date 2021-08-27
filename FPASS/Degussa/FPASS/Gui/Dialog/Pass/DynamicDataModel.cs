using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

using de.pta.Component.DbAccess;
using de.pta.Component.ListOfValues;
using de.pta.Component.Errorhandling;

using Degussa.FPASS.Bo.Pass;
using Degussa.FPASS.Db;
using Degussa.FPASS.FPASSApplication;
using Degussa.FPASS.Util;
using Degussa.FPASS.Util.Exceptions;
using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.Util.UserManagement;
using Degussa.FPASS.Bo.Search;

namespace Degussa.FPASS.Gui.Dialog.Pass
{
	/// <summary>
	/// DynamicDataModel is the model 
	/// of the MVC-triad DynamicDataModel,
	/// DynamicDataController and FrmDynamicData.
	/// DynamicDataModel extends from the FPASSBaseModel.
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
	public class DynamicDataModel : FPASSBaseModel
	{
		#region Members

		/// <summary>
		/// Database query used in search
		/// </summary>
		private const string DYNAMIC_DATA_QUERY		= "DynamicTables";
		/// <summary>
		/// holds the search parameters from gui
		/// </summary>
		private string			mFirstname;
		private string			mSurname;
		private string			mExcontractor;
		private string			mDateFrom;
		private string   		mDateUntil;
		private string			mEntry;
		/// <summary>
		/// Value object holds data for current db record
		/// </summary>
		private DynamicDataSearch	mBODynamicData;

		
		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public DynamicDataModel()
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
		/// New 26.03.04: Refresh Excontractor combobox by re-reading from DB
		/// </summary>
		internal override void PreShow()
		{
			((FrmDynamicData) mView).FillExcontractor();
		}
		
		
		/// <summary>
		/// Returns and displays Dynamic Data records according the given search criteria
		/// Fill datatable at runtime and bind to grid to allow sorts
		/// 24.02.2004: DataTable Column BookingDate changed to type DateTime so can be sorted chronologically (default String)
		/// PersNo as Decimal for same reason
		/// </summary>
		/// <exception cref="de.pta.Component.Errorhandling.UIWarningException">when no search criteria are set and when search returns no results</exception>
		internal void GetDynamicData() 
		{
			int numRecs = 0;
			((FrmDynamicData) mView).DgrDynamicData.DataSource = null;

			// read the search criteria from the gui
			this.CopyOutSearchCriteria();

			// Get DataProvider from DbAccess component
			IProvider mProvider = DBSingleton.GetInstance().DataProvider;

			// Create the select command & fill Data Reader 
			IDbCommand selComm = mProvider.CreateCommand(DYNAMIC_DATA_QUERY);
			selComm.CommandText = GenerateWhereClause( selComm.CommandText );

			// Create a datatable at runtime: this is bound to datagrid to allow sorting			
			DataRow row;
			DataTable table = new DataTable("RTTabDynData");
			table.Columns.Add( new DataColumn("DynamicId") );
			table.Columns.Add( new DataColumn( "BookDate", typeof(System.DateTime) ) );
			table.Columns.Add( new DataColumn("BookTime"));
			table.Columns.Add( new DataColumn("Entry"));
			table.Columns.Add( new DataColumn( "Persno", typeof(System.Decimal) ) );
			table.Columns.Add( new DataColumn("Surname") );
			table.Columns.Add( new DataColumn("Firstname") );
			table.Columns.Add( new DataColumn("Excontractor") );		
			
			// Get Reader
			IDataReader mDR = mProvider.GetReader(selComm);
		
			// Loop thru records and build data table, bind this to datagrid in GUI
			while (mDR.Read())
			{
				mBODynamicData				= new DynamicDataSearch();
				row							= table.NewRow();

				mBODynamicData.DynamicId	= Convert.ToDecimal( mDR["DYFP_ID"] );
				mBODynamicData.Excontractor	= mDR["EXCO_NAME"].ToString();
				mBODynamicData.PersNo		= Convert.ToDecimal( mDR["CWR_PERSNO"] );
				mBODynamicData.Firstname	= mDR["CWR_FIRSTNAME"].ToString();
				mBODynamicData.Surname		= mDR["CWR_SURNAME"].ToString();
				mBODynamicData.Date			= Convert.ToDateTime(mDR["DYFP_DATE"]).ToString("dd.MM.yyyy");
				mBODynamicData.Time			= mDR["DYFP_TIME"].ToString();
				mBODynamicData.Entry		= mDR["DYFP_ENTRY"].ToString();
	
				row.ItemArray = new object[8] {mBODynamicData.DynamicId,
												  mBODynamicData.Date,
												  mBODynamicData.Time,
												  mBODynamicData.Entry, 
												  mBODynamicData.PersNo,
												  mBODynamicData.Surname,
												  mBODynamicData.Firstname, 
												  mBODynamicData.Excontractor
											  };
				table.Rows.Add(row);
				numRecs ++;
			}
			mDR.Close();

			
			if ( numRecs > 0 ) 
			{	
				// Bind data grid in Form to datatable
				((FrmDynamicData) mView).DgrDynamicData.DataSource = table;
				this.ShowMessageInStatusBar("Meldung: " + numRecs + " Bewegungsdatensätze gefunden");
			} 
			else 
			{
				// No search results to show
				this.ShowMessageInStatusBar( MessageSingleton.GetInstance().GetMessage
					(MessageSingleton.NO_RESULTS) );
			}
		}


		/// <summary>
		/// reads the search parameters from the gui
		/// 09.03.2004: Get Time From & Until with DateTimePickers in GUI
		/// <exception cref=""> throws FPASSWarningException when user changes the content of the combobox</exception>
		/// </summary>
		private void CopyOutSearchCriteria() 
		{
			int			noSearchCriteria = 0;

			mDateFrom  = String.Empty;
			mDateUntil = String.Empty;

			mExcontractor = this.GetSelectedValueFromCbo(((FrmDynamicData) mView).CboExcontractor).Trim();
			if ( mExcontractor.Length < 1 )
			{
				noSearchCriteria ++;
			}

			mEntry = this.GetSelectedValueFromCbo( ((FrmDynamicData) mView).CboKind );
			
			if ( mEntry.Length > 0 )
			{
				mEntry = mEntry.Substring(0,1);				
			}
			else
			{
				noSearchCriteria ++;
			}

			mFirstname = ((FrmDynamicData) mView).TxtFirstname.Text.Trim().Replace("*", "%");;
			if ( mFirstname.Length < 1 )
			{
				noSearchCriteria ++;
			}

			mSurname = ((FrmDynamicData) mView).TxtSurname.Text.Trim().Replace("*", "%");;
			if ( mSurname.Length < 1 )
			{
				noSearchCriteria ++;
			}

			//18.03.2004
			//The Search for the dynamic data must be allways used 
			mDateFrom  = ((FrmDynamicData) mView).DatBookingFrom.Value.ToString().Substring(0, 10);
			mDateUntil = ((FrmDynamicData) mView).DatBookingUntil.Value.ToString().Substring(0, 10);


			// 09.03.2004
			// If a val was set in dateTimePickers, use it, else no search crit
			/*if ( ((FrmDynamicData) mView).IsSearchDateInUse )
			{
				mDateFrom = ((FrmDynamicData) mView).DatBookingFrom.Value.ToString().Substring(0, 10);
			}
			else
			{
				noSearchCriteria ++;
			}

			if ( ((FrmDynamicData) mView).IsSearchDateInUse )
			{
				mDateUntil = ((FrmDynamicData) mView).DatBookingUntil.Value.ToString().Substring(0, 10);
			}
			else
			{
				noSearchCriteria ++;
			}*/

			if ( noSearchCriteria == 6 ) 
			{
				((FrmDynamicData) mView).Cursor = System.Windows.Forms.Cursors.WaitCursor;
				throw new UIWarningException(MessageSingleton.GetInstance().GetMessage
					(MessageSingleton.NO_SEARCH_CRITERIA));
			} 
		}


		/// <summary>
		/// Dynamically generates WHERE clause for SQL statement. The more values set in the GUI the more
		/// search criteria are appended to the SQL text.
		/// Mandant ID is always required ( new 08.12.2003)
		/// </summary>
		/// <param name="pSelect"></param>
		/// <returns></returns>
		private String GenerateWhereClause(String pSelect) 
		{
			string whereClause = " WHERE CWR_MND_ID = " + UserManagementControl.getInstance().CurrentMandatorID.ToString();

			if ( this.mEntry.Length > 0 ) 
			{
				whereClause = whereClause + " AND DYFP_ENTRY = '" +  this.mEntry + "' ";
			}

			if ( this.mExcontractor.Length > 0 ) 
			{
				whereClause = whereClause + " AND EXCO_NAME = '" +  this.mExcontractor + "' ";
			}

			if ( this.mFirstname.Length > 0 ) 
			{
				whereClause = whereClause + " AND UPPER (CWR_FIRSTNAME) LIKE '" +  this.mFirstname.ToUpper() + "' ";
			}

			if ( this.mSurname.Length > 0 ) 
			{
				whereClause = whereClause + " AND UPPER (CWR_SURNAME) LIKE '" +  this.mSurname.ToUpper() + "' ";
			}

			if ( this.mDateFrom.Length > 1 &&  this.mDateUntil.Length > 1) 
			{
				whereClause = whereClause + " AND DYFP_DATE BETWEEN TO_DATE( '"  
											+ this.mDateFrom 
											+ "', 'DD.MM.YYYY' ) AND TO_DATE('"  
											+ this.mDateUntil 
											+ "', 'DD.MM.YYYY') ";
			}

			/*if ( this.mDateFrom.Length > 1 &&  this.mDateUntil.Length == 0 ) 
			{
				whereClause = whereClause + " AND DYFP_DATE >= TO_DATE('" 
											+ this.mDateFrom 
											+ "', 'DD.MM.YYYY') ";
			}

			if ( this.mDateFrom.Length == 0 &&  this.mDateUntil.Length > 1 ) 
			{
				whereClause = whereClause + " AND DYFP_DATE <= TO_DATE('" 
											+ this.mDateUntil 
											+ "', 'DD.MM.YYYY') ";
			} */

			return pSelect + whereClause;

		}

		#endregion // End of Methods

	}
}

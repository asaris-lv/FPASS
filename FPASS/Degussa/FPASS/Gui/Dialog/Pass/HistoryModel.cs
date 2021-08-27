using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

using de.pta.Component.DbAccess;
using de.pta.Component.Errorhandling;
using de.pta.Component.ListOfValues;

using Degussa.FPASS.Util;
using Degussa.FPASS.Util.Validation;
using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.Util.UserManagement;
using Degussa.FPASS.Util.Exceptions;
using Degussa.FPASS.Bo.Pass;
using Degussa.FPASS.Db;
using Degussa.FPASS.Bo.Search;

namespace Degussa.FPASS.Gui.Dialog.Pass
{
	/// <summary>
	/// A HistoryModel is the model of the MVC-triad HistoryModel,
	/// HistoryController and FrmHistory.
	/// HistoryModel extends from the FPASSBaseModel.
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
	public class HistoryModel : FPASSBaseModel
	{
		#region Members

		//Database access
		private const string HISTORY_QUERY = "HistoryTables";

		// holds the search parameters from gui
		private string		 mTable;
		private string		 mDateFrom;
		private string   	 mDateUntil;
		
		// Declare History BO to hold data
		private HistorySearch mBOHistory;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public HistoryModel()
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
		/// reads the search parameters from the gui
		/// <exception cref=""> throws FPASSWarningException when user changes the content of the combobox</exception>
		/// </summary>
		private void CopyOutSearchCriteria() 
		{

			int			noSearchCriteria = 0;

			mTable = this.GetSelectedValueFromCbo(((FrmHistory) mView).CboTable);
			if ( mTable.Length < 1 )
			{
				this.mTable = "%";
				noSearchCriteria ++;
			}

			mDateFrom = ((FrmHistory) mView).TxtFrom.Text;
			if ( mDateFrom.Length < 1 )
			{
				this.mDateFrom = "%";
				noSearchCriteria ++;
			}

			mDateUntil = ((FrmHistory) mView).TxtUntil.Text;
			if ( mDateUntil.Length < 1 )
			{
				this.mDateUntil = "%";
				noSearchCriteria ++;
			}

			if ( noSearchCriteria == 3 ) 
			{
				((FrmHistory) mView).Cursor = System.Windows.Forms.Cursors.WaitCursor;
				throw new UIWarningException(MessageSingleton.GetInstance().GetMessage
					(MessageSingleton.NO_SEARCH_CRITERIA));
			} 
		}


		/// <summary>
		/// Checks if format in date fields is correct
		/// </summary>
		public void ValidateDateSearchCriteria() 
		{

			if ( ((FrmHistory) mView).TxtFrom.Text.Length > 0 )
			{
				if ( ! StringValidation.GetInstance().IsDateString (((FrmHistory) mView).TxtFrom.Text) ) 
				{
					throw new UIWarningException(MessageSingleton.GetInstance().
						GetMessage(MessageSingleton.INVALID_FULLDATE) );		
				}
			}


			if ( ((FrmHistory) mView).TxtUntil.Text.Length > 0 )
			{
				if ( ! StringValidation.GetInstance().IsDateString (((FrmHistory) mView).TxtUntil.Text) ) 
				{
					throw new UIWarningException(MessageSingleton.GetInstance().
							GetMessage(MessageSingleton.INVALID_FULLDATE) );
				}
			}
		}


		/// <summary>
		/// Selects history records from FPASS_HIST for the chosen DB table and displays results
		/// DataTable used to display results in DataGrid and allow sorting of results
		/// Last change 17.02.2004
		/// Default data type for the columns is String, Decimal and DateTime are also used here so that sorting is correct
		/// </summary>
		/// <exception cref=""> FPASSWarningException</exception> when no search criteria set
		/// or when search returns no results.
		public void GetHistory() 
		{			
			int numRecs = 0;
			((FrmHistory) mView).DgrHistory.DataSource = null;

			// read the search criteria from the gui
			this.CopyOutSearchCriteria();

			// Get DataProvider from DbAccess component
			IProvider mProvider = DBSingleton.GetInstance().DataProvider;

			// Create the select command & fill Data Reader 
			IDbCommand selComm = mProvider.CreateCommand(HISTORY_QUERY);
			selComm.CommandText = GenerateWhereClause( selComm.CommandText );
					
			// Create a datatable at runtime: this is bound to datagrid to allow sorting			
			DataRow row;
			DataTable table = new DataTable("RTTabHistory");
			table.Columns.Add( new DataColumn("HISTID") );
			table.Columns.Add( new DataColumn("UserName") );
			table.Columns.Add( new DataColumn("TableName") );
			table.Columns.Add( new DataColumn("ColumnName") );
			table.Columns.Add( new DataColumn( "RowId", typeof(System.Decimal) ) );
			table.Columns.Add( new DataColumn("OldValue") );		
			table.Columns.Add( new DataColumn("NewValue") );
			// Need this column to be of type DateTime so it can be sorted chronologically				
			table.Columns.Add( new DataColumn( "ChangeDate", typeof(System.DateTime) ) );
			table.Columns.Add( new DataColumn("Description") );

			// Get reader
			IDataReader mDR = mProvider.GetReader(selComm);
				
			// Loop thru records and create a datatable of of history BOs
			while (mDR.Read())
			{
				mBOHistory					= new HistorySearch();
				row							= table.NewRow();

				mBOHistory.HISTID  			= Convert.ToDecimal(mDR["HIST_ID"]);
				mBOHistory.UserId			= Convert.ToDecimal(mDR["HIST_USER_ID"]);
				mBOHistory.UserName			= mDR["USERNICENAME"].ToString();
				mBOHistory.ChangeDate		= Convert.ToDateTime( mDR["HIST_CHANGEDATE"] );
				mBOHistory.TableName		= mDR["HIST_TABLENAME"].ToString();
				mBOHistory.ColumnName		= mDR["HIST_COLUMNNAME"].ToString();
				mBOHistory.RowId			= Convert.ToDecimal(mDR["HIST_ROWID"]);
				mBOHistory.OldValue			= mDR["HIST_OLDVALUE"].ToString();
				mBOHistory.NewValue			= mDR["HIST_NEWVALUE"].ToString();
				mBOHistory.Description		= mDR["HIST_DESCRIPTION"].ToString();
				
				row.ItemArray = new object[9] {mBOHistory.HISTID,
												mBOHistory.UserName,
												mBOHistory.TableName,
												mBOHistory.ColumnName,
												mBOHistory.RowId,
												mBOHistory.OldValue,
												mBOHistory.NewValue,
												mBOHistory.ChangeDate, 
												mBOHistory.Description
												};
				table.Rows.Add(row);
				numRecs ++;
			}
			mDR.Close();

			// Bind datagrid in Form to datatable (ArrayList no longer used)
			if ( numRecs > 0 ) 
			{
				((FrmHistory) mView).DgrHistory.DataSource = table;
				this.ShowMessageInStatusBar("Meldung: " + numRecs + " historisierte Datensätze gefunden");
			} 
			else 
			{
				this.ShowMessageInStatusBar( MessageSingleton.GetInstance().GetMessage
											(MessageSingleton.NO_RESULTS) );
			}
		}

		/// <summary>
		/// Dynamically builds WHERE clause for the SQL SELECT to get the history data
		/// Use LIKE 'ss%' for table name and to_date() to convert date strings and avoid ORA-01843
		/// </summary>
		/// <param name="pSelect"></param>
		/// <returns></returns>
		private String GenerateWhereClause(String pSelect) 
		{	
			string	whereClause = "";
		
			whereClause = " WHERE USM_MND_ID =  " + UserManagementControl.getInstance().CurrentMandatorID;

			if ( this.mTable.Length > 0 ) 
			{
				whereClause = whereClause + " AND HIST_TABLENAME LIKE '" +  this.mTable + "' ";
			}

			if ( this.mDateFrom.Length > 2 &&  this.mDateUntil.Length > 2 ) 
			{
				whereClause = whereClause + " AND TRUNC(HIST_CHANGEDATE) BETWEEN TO_DATE('" 
											+ this.mDateFrom 
											+ "', 'DD.MM.YYYY' ) AND TO_DATE('"  
											+ this.mDateUntil 
											+ "', 'DD.MM.YYYY') "; 
			}

			if ( this.mDateFrom.Length > 2 &&  this.mDateUntil.Length < 2 ) 
			{
				whereClause = whereClause + " AND TRUNC(HIST_CHANGEDATE) >= TO_DATE('" 
											+ this.mDateFrom 
											+ "', 'DD.MM.YYYY' ) ";
			}

			if ( this.mDateFrom.Length < 2 &&  this.mDateUntil.Length > 2 ) 
			{
				whereClause = whereClause + " AND TRUNC(HIST_CHANGEDATE) <= TO_DATE('" 
											+ this.mDateUntil 
											+ "', 'DD.MM.YYYY') ";
			} 
			return pSelect + whereClause;
		}

		#endregion // End of Methods

	}
}

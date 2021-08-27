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


namespace Degussa.FPASS.Gui.Dialog.Administration
{
	/// <summary>
	/// Summary description for PopECODHistModel.
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
	public class PopECODHistModel : FPASSBaseModel
	{
		#region Members

		/// <summary>
		/// Names of database queries and their parameters
		/// </summary>
		private const string EXCO_COORD_HIST_QUERY = "ExcoCoordHist";
		private const string EXCOID_PARA		   = ":vh2_exco_id";
		/// <summary>
		/// ID of current excontractor
		/// </summary>
		private decimal mEXCOID;
		/// <summary>
		/// Name of current excontractor
		/// </summary>
		private string mExcoName;
		

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public PopECODHistModel()
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

		internal decimal ExContractorID
		{
			set 
			{
				mEXCOID = value;
			}
		}

		internal string ExContractorName
		{
			set 
			{
				mExcoName = value;
			}
		}

		#endregion //End of Accessors

		#region Methods 

		internal override void PreShow()
		{
			GetExcoCoordHistory();
		}


		/// <summary>
		/// Does an SQL Select on the database view, builds a DataTable of records and binds this to datagrid in popup form
		/// In this model no value objects (BOs) are required as results are only displayed, not edited
		/// Therefore fields of DataReader are written directly into DataTable.
		/// DB view returns strings for the 2 date entries to make life easier
		/// </summary>
		private void GetExcoCoordHistory() 
		{
			string lblMessageTxt     = "Koordinatoren der Fremdfirma  ";
			string displayExcoName   = String.Empty;
			object strCurrDeleteDate;
			int    i = 0;
			((FrmPopExcoCoordHist) mView).dgrCoordHist.DataSource = null;
			
			try
			{
				// Get DataProvider from DbAccess component, create select command
				IProvider mProvider = DBSingleton.GetInstance().DataProvider;
				IDbCommand selComm  = mProvider.CreateCommand(EXCO_COORD_HIST_QUERY);
				mProvider.SetParameter( selComm, EXCOID_PARA, mEXCOID );
				
				// Create a datatable at runtime: this is bound to datagrid to allow sorting			
				DataRow row;
				DataTable table = new DataTable("RTTabCoordHist");
				table.Columns.Add(new DataColumn("CoordNameAndTel"));
				table.Columns.Add(new DataColumn("RespFrom", typeof(System.DateTime) ));
				table.Columns.Add(new DataColumn("ChangeUserName"));
				table.Columns.Add(new DataColumn("RespUntil", typeof(System.DateTime) ));
				table.Columns.Add(new DataColumn("DeleteUserName"));
				
				// fill Data Reader 
				IDataReader mDR     = mProvider.GetReader(selComm);			
				while (mDR.Read())
				{
					row				  = table.NewRow();
					strCurrDeleteDate = null;
					displayExcoName    = mDR["VH2_EXCO_NAME"].ToString();
					
					// If value is null in DB, leave it empty for display
					if ( !mDR["VH2_RESPDATEUNTIL"].Equals(DBNull.Value) && !mDR["VH2_RESPDATEUNTIL"].Equals(string.Empty))
					{
						try 
						{
							strCurrDeleteDate = mDR["VH2_RESPDATEUNTIL"].ToString();
						}
						catch
						{
							Console.WriteLine(mDR["VH2_RESPDATEUNTIL"]);
						}
					}
		
					row.ItemArray = new object[5] {Convert.ToString( mDR["VH2_BOTHNAMESTEL"] ),
														Convert.ToString( mDR["VH2_RESPDATEFROM"] ),
														Convert.ToString( mDR["VH2_CREATEDBY"] ),
														strCurrDeleteDate,
														Convert.ToString( mDR["VH2_DELETEDBY"] ),
					};
				
					table.Rows.Add(row);
					i ++;
				}

				mDR.Close();

				// Bind data grid in Form to datatable
				if ( i > 0 ) 
				{					    
					((FrmPopExcoCoordHist) mView).dgrCoordHist.DataSource  = table;
					((FrmPopExcoCoordHist) mView).dgrCoordHist.CaptionText = lblMessageTxt + displayExcoName; 
				} 
			}
			catch (System.Data.OracleClient.OracleException oraex)
			{			
				throw new UIFatalException( MessageSingleton.GetInstance().GetMessage(
					MessageSingleton.FATAL_DB_ERROR) 
					+ oraex.Message );			
			}
		}

		#endregion // End of Methods


	}
}

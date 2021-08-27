using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

using de.pta.Component.Errorhandling;
using de.pta.Component.DbAccess;
using de.pta.Component.ListOfValues;

using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.Util.Exceptions;
using Degussa.FPASS.Util.UserManagement;
using Degussa.FPASS.Bo.Pass;
using Degussa.FPASS.Db;

namespace Degussa.FPASS.Gui.Dialog.Pass
{
	/// <summary>
	/// Summary description for PopCoWorkerHistModel.
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
	public class PopCoWorkerHistModel : FPASSBaseModel
	{
		#region Members

		/// <summary>
		/// Names of database queries and their parameters
		/// </summary>
		private const string CWR_COORD_HIST_QUERY = "CoWorkerCoordHist";
		private const string CWRID_PARA		      = ":VH1_CWR_ID";
		/// <summary>
		/// ID of current coworker
		/// </summary>
		private decimal mCoworkerID;
	

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public PopCoWorkerHistModel()
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

		internal decimal CoWorkerID
		{
			set 
			{
				mCoworkerID = value;
			}
		}

		#endregion //End of Accessors

		#region Methods 


		internal override void PreShow()
		{
			GetCWRCoordHistory();
		}

		/// <summary>
		/// Does an SQL Select on the database view, builds a DataTable of records and binds this to datagrid in popup form
		/// In this model no value objects (BOs) are required as results are only displayed, not edited
		/// Therefore fields of DataReader are written directly into DataTable.
		/// DB view returns strings for the 2 date entries to make life easier
		/// Update 07.03.2005: Show nice name of change user
		/// </summary>
		internal void GetCWRCoordHistory() 
		{
			string currCoWorkerName  = String.Empty;
			string lblMessageTxt     = "Koordinatoren des Fremdfirmenmitarbeiters  ";
			object currRespDateUntil; 
			((FrmPopCoWorkerHist) mView).dgrCoordHist.DataSource = null;
			
			try
			{
				/// Get DataProvider from DbAccess component, create select command 
				IProvider mProvider = DBSingleton.GetInstance().DataProvider;

				IDbCommand selComm  = mProvider.CreateCommand(CWR_COORD_HIST_QUERY);
				mProvider.SetParameter( selComm, CWRID_PARA, mCoworkerID );
				
				/// Create datatable at runtime: this is bound to datagrid to allow sorting			
				DataRow row;
				DataTable table = new DataTable("RTTabAssigntCwrCoord");
				table.Columns.Add(new DataColumn("Coordinator"));
				table.Columns.Add(new DataColumn("RespDateFrom", typeof(System.DateTime) ));
				table.Columns.Add(new DataColumn("RespDateUntil", typeof(System.DateTime) ));
				table.Columns.Add(new DataColumn("ChangeDate", typeof(System.DateTime) ));
				table.Columns.Add(new DataColumn("UserName"));

				/// fill Data Reader 
				/// Loop thru records and display in Datatable
				IDataReader mDR     = mProvider.GetReader(selComm);
				int i = 0;
				while (mDR.Read())
				{
					row				  = table.NewRow();
					currCoWorkerName  = mDR["VH1_CWRBOTHNAMES"].ToString();
					currRespDateUntil = null;
					
					// If value is null in DB, leave it empty for display
					if ( !mDR["VH1_RESPDATEUNTIL"].Equals(DBNull.Value) && !mDR["VH1_RESPDATEUNTIL"].Equals(string.Empty))
					{
						try 
						{
							currRespDateUntil = mDR["VH1_RESPDATEUNTIL"].ToString();
						}
						catch
						{
							Console.WriteLine(mDR["VH1_RESPDATEUNTIL"]);
						}
					}
						
					row.ItemArray = new object[5] { mDR["VH1_COORD"].ToString(),
													  mDR["VH1_RESPDATEFROM"].ToString(),
													  currRespDateUntil,
													  mDR["VH1_CHANGEDATE"].ToString(),
													  mDR["VH1_USERNICENAME"].ToString() };

					table.Rows.Add(row);
					i ++;

				}
				mDR.Close();					

				// Bind data grid in Form to datatable
				if ( i > 0 ) 
				{
					// Show name of current coworker in Form
					((FrmPopCoWorkerHist) mView).dgrCoordHist.CaptionText = lblMessageTxt + currCoWorkerName;
					((FrmPopCoWorkerHist) mView).dgrCoordHist.DataSource = table;
				} 
				else 
				{
					((FrmPopCoWorkerHist) mView).dgrCoordHist.CaptionText = "Keine Historiendaten gefunden.";
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

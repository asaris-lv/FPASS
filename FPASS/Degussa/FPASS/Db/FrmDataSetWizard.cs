using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Degussa.FPASS.Db.DataSets
{
	/// <summary>
	/// Summary description for FrmDataSetWizard.
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
	public class FrmDataSetWizard : System.Windows.Forms.Form
	{
		private System.Data.OleDb.OleDbConnection oleDbConnection1;
		private System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter1;
		private System.Data.OleDb.OleDbDataAdapter tempDAExContractor;
		private System.Data.OleDb.OleDbConnection oleDbConnection2;
		private System.Data.OleDb.OleDbDataAdapter tempDAPlant;
		private System.Data.OleDb.OleDbDataAdapter tempDADepartment;
		private System.Data.OleDb.OleDbDataAdapter tempDAPrecMedType;
		private Degussa.FPASS.Db.DataSets.DSPlant dsPlant1;
		private Degussa.FPASS.Db.DataSets.DSExContractor dsExContractor1;
		private Degussa.FPASS.Db.DataSets.DSDepartment dsDepartment1;
		private Degussa.FPASS.Db.DataSets.DSPrecMedType dsPrecMedType1;
		private System.Data.OleDb.OleDbDataAdapter tempDACraft;
		private Degussa.FPASS.Db.DataSets.DSCraft dsCraft1;
		private System.Data.OleDb.OleDbDataAdapter tempDAExcoCoord;
		private System.Data.OleDb.OleDbConnection oleDbConnection3;
		private Degussa.FPASS.Db.DataSets.DSExcoCoord dsExcoCoord1;
		private System.Data.OleDb.OleDbCommand oleDbSelectCommand3;
		private System.Data.OleDb.OleDbCommand oleDbInsertCommand3;
		private System.Data.OleDb.OleDbCommand oleDbUpdateCommand3;
		private System.Data.OleDb.OleDbCommand oleDbDeleteCommand3;
		private System.Data.OleDb.OleDbCommand oleDbSelectCommand5;
		private System.Data.OleDb.OleDbCommand oleDbInsertCommand5;
		private System.Data.OleDb.OleDbCommand oleDbUpdateCommand5;
		private System.Data.OleDb.OleDbCommand oleDbDeleteCommand5;
		private System.Data.OleDb.OleDbCommand oleDbSelectCommand4;
		private System.Data.OleDb.OleDbCommand oleDbInsertCommand4;
		private System.Data.OleDb.OleDbCommand oleDbUpdateCommand4;
		private System.Data.OleDb.OleDbCommand oleDbDeleteCommand4;
		private System.Data.OleDb.OleDbCommand oleDbSelectCommand2;
		private System.Data.OleDb.OleDbCommand oleDbInsertCommand2;
		private System.Data.OleDb.OleDbCommand oleDbUpdateCommand2;
		private System.Data.OleDb.OleDbCommand oleDbDeleteCommand2;
		private System.Data.OleDb.OleDbCommand oleDbSelectCommand7;
		private System.Data.OleDb.OleDbCommand oleDbInsertCommand7;
		private System.Data.OleDb.OleDbCommand oleDbUpdateCommand6;
		private System.Data.OleDb.OleDbCommand oleDbDeleteCommand6;
		private System.Data.OleDb.OleDbCommand oleDbSelectCommand6;
		private System.Data.OleDb.OleDbCommand oleDbInsertCommand6;
		private System.Data.OleDb.OleDbCommand oleDbUpdateCommand7;
		private System.Data.OleDb.OleDbCommand oleDbDeleteCommand7;
		private System.Data.OracleClient.OracleDataAdapter tempDAUserByRole;
		private System.Data.OracleClient.OracleConnection oracleConnection1;
		private Degussa.FPASS.Db.DataSets.DSUser dsUser1;
		private System.Data.OracleClient.OracleDataAdapter tempDARoleByUser;
		private Degussa.FPASS.Db.DataSets.DSRole dsRole1;
		private System.Data.OleDb.OleDbDataAdapter tempDAHistory;
		private System.Data.OleDb.OleDbCommand oleDbSelectCommand8;
		private System.Data.OleDb.OleDbCommand oleDbInsertCommand8;
		private Degussa.FPASS.Db.DataSets.DSHistory dsHistory1;
		private System.Data.OracleClient.OracleDataAdapter tempDADUMMY;
		private System.Data.OracleClient.OracleCommand oracleSelectCommand3;
		private System.Data.OracleClient.OracleCommand oracleInsertCommand3;
		private System.Data.OracleClient.OracleCommand oracleUpdateCommand2;
		private System.Data.OracleClient.OracleCommand oracleDeleteCommand2;
		private Degussa.FPASS.Db.DSDummy dsDummy2;
		private System.Data.OleDb.OleDbCommand oleDbSelectCommand1;
		private System.Data.OleDb.OleDbCommand oleDbInsertCommand1;
		private System.Data.OleDb.OleDbCommand oleDbUpdateCommand1;
		private System.Data.OleDb.OleDbCommand oleDbDeleteCommand1;
		private System.Data.OleDb.OleDbCommand oleDbSelectCommand9;
		private System.Data.OleDb.OleDbCommand oleDbInsertCommand9;
		private System.Data.OleDb.OleDbCommand oleDbUpdateCommand8;
		private System.Data.OleDb.OleDbCommand oleDbDeleteCommand8;
		private System.Data.OleDb.OleDbDataAdapter tempDAReceptionAuthorize;
		private System.Data.OracleClient.OracleCommand oracleSelectCommand2;
		private System.Data.OracleClient.OracleCommand oracleInsertCommand2;
		private System.Data.OracleClient.OracleCommand oracleUpdateCommand1;
		private System.Data.OracleClient.OracleCommand oracleDeleteCommand1;
		private System.Data.OracleClient.OracleCommand oracleSelectCommand1;
		private System.Data.OracleClient.OracleCommand oracleInsertCommand1;
		#region Members

		#endregion //End of Members

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FrmDataSetWizard()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			InitView();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.oleDbConnection1 = new System.Data.OleDb.OleDbConnection();
			this.oleDbDataAdapter1 = new System.Data.OleDb.OleDbDataAdapter();
			this.oleDbDeleteCommand1 = new System.Data.OleDb.OleDbCommand();
			this.oleDbInsertCommand1 = new System.Data.OleDb.OleDbCommand();
			this.oleDbSelectCommand1 = new System.Data.OleDb.OleDbCommand();
			this.oleDbUpdateCommand1 = new System.Data.OleDb.OleDbCommand();
			this.oleDbConnection3 = new System.Data.OleDb.OleDbConnection();
			this.tempDAExContractor = new System.Data.OleDb.OleDbDataAdapter();
			this.oleDbDeleteCommand6 = new System.Data.OleDb.OleDbCommand();
			this.oleDbInsertCommand7 = new System.Data.OleDb.OleDbCommand();
			this.oleDbSelectCommand7 = new System.Data.OleDb.OleDbCommand();
			this.oleDbUpdateCommand6 = new System.Data.OleDb.OleDbCommand();
			this.oleDbConnection2 = new System.Data.OleDb.OleDbConnection();
			this.tempDAPlant = new System.Data.OleDb.OleDbDataAdapter();
			this.oleDbDeleteCommand2 = new System.Data.OleDb.OleDbCommand();
			this.oleDbInsertCommand2 = new System.Data.OleDb.OleDbCommand();
			this.oleDbSelectCommand2 = new System.Data.OleDb.OleDbCommand();
			this.oleDbUpdateCommand2 = new System.Data.OleDb.OleDbCommand();
			this.tempDADepartment = new System.Data.OleDb.OleDbDataAdapter();
			this.oleDbDeleteCommand3 = new System.Data.OleDb.OleDbCommand();
			this.oleDbInsertCommand3 = new System.Data.OleDb.OleDbCommand();
			this.oleDbSelectCommand3 = new System.Data.OleDb.OleDbCommand();
			this.oleDbUpdateCommand3 = new System.Data.OleDb.OleDbCommand();
			this.tempDAPrecMedType = new System.Data.OleDb.OleDbDataAdapter();
			this.oleDbDeleteCommand4 = new System.Data.OleDb.OleDbCommand();
			this.oleDbInsertCommand4 = new System.Data.OleDb.OleDbCommand();
			this.oleDbSelectCommand4 = new System.Data.OleDb.OleDbCommand();
			this.oleDbUpdateCommand4 = new System.Data.OleDb.OleDbCommand();
			this.dsPlant1 = new Degussa.FPASS.Db.DataSets.DSPlant();
			this.dsExContractor1 = new Degussa.FPASS.Db.DataSets.DSExContractor();
			this.dsDepartment1 = new Degussa.FPASS.Db.DataSets.DSDepartment();
			this.dsPrecMedType1 = new Degussa.FPASS.Db.DataSets.DSPrecMedType();
			this.tempDACraft = new System.Data.OleDb.OleDbDataAdapter();
			this.oleDbDeleteCommand5 = new System.Data.OleDb.OleDbCommand();
			this.oleDbInsertCommand5 = new System.Data.OleDb.OleDbCommand();
			this.oleDbSelectCommand5 = new System.Data.OleDb.OleDbCommand();
			this.oleDbUpdateCommand5 = new System.Data.OleDb.OleDbCommand();
			this.dsCraft1 = new Degussa.FPASS.Db.DataSets.DSCraft();
			this.tempDAExcoCoord = new System.Data.OleDb.OleDbDataAdapter();
			this.oleDbDeleteCommand7 = new System.Data.OleDb.OleDbCommand();
			this.oleDbInsertCommand6 = new System.Data.OleDb.OleDbCommand();
			this.oleDbSelectCommand6 = new System.Data.OleDb.OleDbCommand();
			this.oleDbUpdateCommand7 = new System.Data.OleDb.OleDbCommand();
			this.dsExcoCoord1 = new Degussa.FPASS.Db.DataSets.DSExcoCoord();
			this.tempDAUserByRole = new System.Data.OracleClient.OracleDataAdapter();
			this.oracleConnection1 = new System.Data.OracleClient.OracleConnection();
			this.dsUser1 = new Degussa.FPASS.Db.DataSets.DSUser();
			this.tempDARoleByUser = new System.Data.OracleClient.OracleDataAdapter();
			this.oracleDeleteCommand1 = new System.Data.OracleClient.OracleCommand();
			this.oracleInsertCommand2 = new System.Data.OracleClient.OracleCommand();
			this.oracleSelectCommand2 = new System.Data.OracleClient.OracleCommand();
			this.oracleUpdateCommand1 = new System.Data.OracleClient.OracleCommand();
			this.dsRole1 = new Degussa.FPASS.Db.DataSets.DSRole();
			this.tempDAHistory = new System.Data.OleDb.OleDbDataAdapter();
			this.oleDbInsertCommand8 = new System.Data.OleDb.OleDbCommand();
			this.oleDbSelectCommand8 = new System.Data.OleDb.OleDbCommand();
			this.dsHistory1 = new Degussa.FPASS.Db.DataSets.DSHistory();
			this.tempDADUMMY = new System.Data.OracleClient.OracleDataAdapter();
			this.oracleDeleteCommand2 = new System.Data.OracleClient.OracleCommand();
			this.oracleInsertCommand3 = new System.Data.OracleClient.OracleCommand();
			this.oracleSelectCommand3 = new System.Data.OracleClient.OracleCommand();
			this.oracleUpdateCommand2 = new System.Data.OracleClient.OracleCommand();
			this.dsDummy2 = new Degussa.FPASS.Db.DSDummy();
			this.tempDAReceptionAuthorize = new System.Data.OleDb.OleDbDataAdapter();
			this.oleDbDeleteCommand8 = new System.Data.OleDb.OleDbCommand();
			this.oleDbInsertCommand9 = new System.Data.OleDb.OleDbCommand();
			this.oleDbSelectCommand9 = new System.Data.OleDb.OleDbCommand();
			this.oleDbUpdateCommand8 = new System.Data.OleDb.OleDbCommand();
			this.oracleSelectCommand1 = new System.Data.OracleClient.OracleCommand();
			this.oracleInsertCommand1 = new System.Data.OracleClient.OracleCommand();
			((System.ComponentModel.ISupportInitialize)(this.dsPlant1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dsExContractor1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dsDepartment1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dsPrecMedType1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dsCraft1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dsExcoCoord1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dsUser1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dsRole1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dsHistory1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dsDummy2)).BeginInit();
			// 
			// oleDbConnection1
			// 
			this.oleDbConnection1.ConnectionString = "Provider=\"OraOLEDB.Oracle.1\";User ID=FPASSv1;Password=fpass; Data Source=koeln8i;" +
				"Extended Properties=;Persist Security Info=False";
			// 
			// oleDbDataAdapter1
			// 
			this.oleDbDataAdapter1.DeleteCommand = this.oleDbDeleteCommand1;
			this.oleDbDataAdapter1.InsertCommand = this.oleDbInsertCommand1;
			this.oleDbDataAdapter1.SelectCommand = this.oleDbSelectCommand1;
			this.oleDbDataAdapter1.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																										new System.Data.Common.DataTableMapping("Table", "FPASS_COWORKER", new System.Data.Common.DataColumnMapping[] {
																																																						  new System.Data.Common.DataColumnMapping("CWR_ID", "CWR_ID"),
																																																						  new System.Data.Common.DataColumnMapping("CWR_TK", "CWR_TK"),
																																																						  new System.Data.Common.DataColumnMapping("CWR_PERSNO", "CWR_PERSNO"),
																																																						  new System.Data.Common.DataColumnMapping("CWR_EXCO_ID", "CWR_EXCO_ID"),
																																																						  new System.Data.Common.DataColumnMapping("CWR_DEPT_ID", "CWR_DEPT_ID"),
																																																						  new System.Data.Common.DataColumnMapping("CWR_CRA_ID", "CWR_CRA_ID"),
																																																						  new System.Data.Common.DataColumnMapping("CWR_ECOD_ID", "CWR_ECOD_ID"),
																																																						  new System.Data.Common.DataColumnMapping("CWR_SUBE_ID", "CWR_SUBE_ID"),
																																																						  new System.Data.Common.DataColumnMapping("CWR_OVEREIGHTEEN_YN", "CWR_OVEREIGHTEEN_YN"),
																																																						  new System.Data.Common.DataColumnMapping("CWR_IDCARDNO", "CWR_IDCARDNO"),
																																																						  new System.Data.Common.DataColumnMapping("CWR_SURNAME", "CWR_SURNAME"),
																																																						  new System.Data.Common.DataColumnMapping("CWR_FIRSTNAME", "CWR_FIRSTNAME"),
																																																						  new System.Data.Common.DataColumnMapping("CWR_DATEOFBIRTH", "CWR_DATEOFBIRTH"),
																																																						  new System.Data.Common.DataColumnMapping("CWR_PLACEOFBIRTH", "CWR_PLACEOFBIRTH"),
																																																						  new System.Data.Common.DataColumnMapping("CWR_DATECREATED", "CWR_DATECREATED"),
																																																						  new System.Data.Common.DataColumnMapping("CWR_VALIDFROM", "CWR_VALIDFROM"),
																																																						  new System.Data.Common.DataColumnMapping("CWR_VALIDUNTIL", "CWR_VALIDUNTIL"),
																																																						  new System.Data.Common.DataColumnMapping("CWR_ORDERNO", "CWR_ORDERNO"),
																																																						  new System.Data.Common.DataColumnMapping("CWR_SECPASS_YN", "CWR_SECPASS_YN"),
																																																						  new System.Data.Common.DataColumnMapping("CWR_ENTRYDATECOOD", "CWR_ENTRYDATECOOD"),
																																																						  new System.Data.Common.DataColumnMapping("CWR_ENTRYCOODID", "CWR_ENTRYCOODID"),
																																																						  new System.Data.Common.DataColumnMapping("CWR_CHKOFFDATECOOD", "CWR_CHKOFFDATECOOD"),
																																																						  new System.Data.Common.DataColumnMapping("CWR_CHKOFFCOODID", "CWR_CHKOFFCOODID"),
																																																						  new System.Data.Common.DataColumnMapping("CWR_ORDERCOMPLET_YN", "CWR_ORDERCOMPLET_YN"),
																																																						  new System.Data.Common.DataColumnMapping("CWR_CHANGEUSER", "CWR_CHANGEUSER"),
																																																						  new System.Data.Common.DataColumnMapping("CWR_TIMESTAMP", "CWR_TIMESTAMP"),
																																																						  new System.Data.Common.DataColumnMapping("CWR_RETURNCODE_ZKS", "CWR_RETURNCODE_ZKS"),
																																																						  new System.Data.Common.DataColumnMapping("CWR_ACCESS", "CWR_ACCESS"),
																																																						  new System.Data.Common.DataColumnMapping("CWR_STATUS", "CWR_STATUS"),
																																																						  new System.Data.Common.DataColumnMapping("CWR_MND_ID", "CWR_MND_ID")})});
			this.oleDbDataAdapter1.UpdateCommand = this.oleDbUpdateCommand1;
			// 
			// oleDbDeleteCommand1
			// 
			this.oleDbDeleteCommand1.CommandText = @"DELETE FROM FPASS_COWORKER WHERE (CWR_ID = ?) AND (CWR_ACCESS = ? OR ? IS NULL AND CWR_ACCESS IS NULL) AND (CWR_CHANGEUSER = ?) AND (CWR_CHKOFFCOODID = ? OR ? IS NULL AND CWR_CHKOFFCOODID IS NULL) AND (CWR_CHKOFFDATECOOD = ? OR ? IS NULL AND CWR_CHKOFFDATECOOD IS NULL) AND (CWR_CRA_ID = ? OR ? IS NULL AND CWR_CRA_ID IS NULL) AND (CWR_DATECREATED = ?) AND (CWR_DATEOFBIRTH = ?) AND (CWR_DEPT_ID = ? OR ? IS NULL AND CWR_DEPT_ID IS NULL) AND (CWR_ECOD_ID = ?) AND (CWR_ENTRYCOODID = ? OR ? IS NULL AND CWR_ENTRYCOODID IS NULL) AND (CWR_ENTRYDATECOOD = ? OR ? IS NULL AND CWR_ENTRYDATECOOD IS NULL) AND (CWR_EXCO_ID = ?) AND (CWR_FIRSTNAME = ?) AND (CWR_IDCARDNO = ? OR ? IS NULL AND CWR_IDCARDNO IS NULL) AND (CWR_MND_ID = ?) AND (CWR_ORDERCOMPLET_YN = ?) AND (CWR_ORDERNO = ? OR ? IS NULL AND CWR_ORDERNO IS NULL) AND (CWR_OVEREIGHTEEN_YN = ?) AND (CWR_PERSNO = ?) AND (CWR_PLACEOFBIRTH = ?) AND (CWR_RETURNCODE_ZKS = ? OR ? IS NULL AND CWR_RETURNCODE_ZKS IS NULL) AND (CWR_SECPASS_YN = ?) AND (CWR_STATUS = ? OR ? IS NULL AND CWR_STATUS IS NULL) AND (CWR_SUBE_ID = ? OR ? IS NULL AND CWR_SUBE_ID IS NULL) AND (CWR_SURNAME = ?) AND (CWR_TIMESTAMP = ?) AND (CWR_TK = ?) AND (CWR_VALIDFROM = ?) AND (CWR_VALIDUNTIL = ?)";
			this.oleDbDeleteCommand1.Connection = this.oleDbConnection1;
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CWR_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_ACCESS", System.Data.OleDb.OleDbType.VarChar, 10, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CWR_ACCESS", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_ACCESS1", System.Data.OleDb.OleDbType.VarChar, 10, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CWR_ACCESS", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_CHANGEUSER", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CWR_CHANGEUSER", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_CHKOFFCOODID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CWR_CHKOFFCOODID", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_CHKOFFCOODID1", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CWR_CHKOFFCOODID", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_CHKOFFDATECOOD", System.Data.OleDb.OleDbType.DBTimeStamp, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CWR_CHKOFFDATECOOD", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_CHKOFFDATECOOD1", System.Data.OleDb.OleDbType.DBTimeStamp, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CWR_CHKOFFDATECOOD", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_CRA_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CWR_CRA_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_CRA_ID1", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CWR_CRA_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_DATECREATED", System.Data.OleDb.OleDbType.DBTimeStamp, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CWR_DATECREATED", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_DATEOFBIRTH", System.Data.OleDb.OleDbType.DBTimeStamp, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CWR_DATEOFBIRTH", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_DEPT_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CWR_DEPT_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_DEPT_ID1", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CWR_DEPT_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_ECOD_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CWR_ECOD_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_ENTRYCOODID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CWR_ENTRYCOODID", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_ENTRYCOODID1", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CWR_ENTRYCOODID", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_ENTRYDATECOOD", System.Data.OleDb.OleDbType.DBTimeStamp, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CWR_ENTRYDATECOOD", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_ENTRYDATECOOD1", System.Data.OleDb.OleDbType.DBTimeStamp, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CWR_ENTRYDATECOOD", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_EXCO_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CWR_EXCO_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_FIRSTNAME", System.Data.OleDb.OleDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CWR_FIRSTNAME", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_IDCARDNO", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(15)), ((System.Byte)(0)), "CWR_IDCARDNO", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_IDCARDNO1", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(15)), ((System.Byte)(0)), "CWR_IDCARDNO", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_MND_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CWR_MND_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_ORDERCOMPLET_YN", System.Data.OleDb.OleDbType.VarChar, 1, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CWR_ORDERCOMPLET_YN", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_ORDERNO", System.Data.OleDb.OleDbType.VarChar, 40, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CWR_ORDERNO", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_ORDERNO1", System.Data.OleDb.OleDbType.VarChar, 40, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CWR_ORDERNO", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_OVEREIGHTEEN_YN", System.Data.OleDb.OleDbType.VarChar, 1, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CWR_OVEREIGHTEEN_YN", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_PERSNO", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(8)), ((System.Byte)(0)), "CWR_PERSNO", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_PLACEOFBIRTH", System.Data.OleDb.OleDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CWR_PLACEOFBIRTH", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_RETURNCODE_ZKS", System.Data.OleDb.OleDbType.VarChar, 2, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CWR_RETURNCODE_ZKS", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_RETURNCODE_ZKS1", System.Data.OleDb.OleDbType.VarChar, 2, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CWR_RETURNCODE_ZKS", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_SECPASS_YN", System.Data.OleDb.OleDbType.VarChar, 1, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CWR_SECPASS_YN", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_STATUS", System.Data.OleDb.OleDbType.VarChar, 20, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CWR_STATUS", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_STATUS1", System.Data.OleDb.OleDbType.VarChar, 20, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CWR_STATUS", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_SUBE_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CWR_SUBE_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_SUBE_ID1", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CWR_SUBE_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_SURNAME", System.Data.OleDb.OleDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CWR_SURNAME", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_TIMESTAMP", System.Data.OleDb.OleDbType.DBTimeStamp, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CWR_TIMESTAMP", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_TK", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(3)), ((System.Byte)(0)), "CWR_TK", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_VALIDFROM", System.Data.OleDb.OleDbType.DBTimeStamp, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CWR_VALIDFROM", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_VALIDUNTIL", System.Data.OleDb.OleDbType.DBTimeStamp, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CWR_VALIDUNTIL", System.Data.DataRowVersion.Original, null));
			// 
			// oleDbInsertCommand1
			// 
			this.oleDbInsertCommand1.CommandText = @"INSERT INTO FPASS_COWORKER(CWR_ID, CWR_TK, CWR_PERSNO, CWR_EXCO_ID, CWR_DEPT_ID, CWR_CRA_ID, CWR_ECOD_ID, CWR_SUBE_ID, CWR_OVEREIGHTEEN_YN, CWR_IDCARDNO, CWR_SURNAME, CWR_FIRSTNAME, CWR_DATEOFBIRTH, CWR_PLACEOFBIRTH, CWR_DATECREATED, CWR_VALIDFROM, CWR_VALIDUNTIL, CWR_ORDERNO, CWR_SECPASS_YN, CWR_ENTRYDATECOOD, CWR_ENTRYCOODID, CWR_CHKOFFDATECOOD, CWR_CHKOFFCOODID, CWR_ORDERCOMPLET_YN, CWR_CHANGEUSER, CWR_TIMESTAMP, CWR_RETURNCODE_ZKS, CWR_ACCESS, CWR_STATUS, CWR_MND_ID) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
			this.oleDbInsertCommand1.Connection = this.oleDbConnection1;
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CWR_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_TK", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(3)), ((System.Byte)(0)), "CWR_TK", System.Data.DataRowVersion.Current, null));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_PERSNO", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(8)), ((System.Byte)(0)), "CWR_PERSNO", System.Data.DataRowVersion.Current, null));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_EXCO_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CWR_EXCO_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_DEPT_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CWR_DEPT_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_CRA_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CWR_CRA_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_ECOD_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CWR_ECOD_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_SUBE_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CWR_SUBE_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_OVEREIGHTEEN_YN", System.Data.OleDb.OleDbType.VarChar, 1, "CWR_OVEREIGHTEEN_YN"));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_IDCARDNO", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(15)), ((System.Byte)(0)), "CWR_IDCARDNO", System.Data.DataRowVersion.Current, null));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_SURNAME", System.Data.OleDb.OleDbType.VarChar, 30, "CWR_SURNAME"));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_FIRSTNAME", System.Data.OleDb.OleDbType.VarChar, 30, "CWR_FIRSTNAME"));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_DATEOFBIRTH", System.Data.OleDb.OleDbType.DBTimeStamp, 5472, "CWR_DATEOFBIRTH"));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_PLACEOFBIRTH", System.Data.OleDb.OleDbType.VarChar, 30, "CWR_PLACEOFBIRTH"));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_DATECREATED", System.Data.OleDb.OleDbType.DBTimeStamp, 5472, "CWR_DATECREATED"));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_VALIDFROM", System.Data.OleDb.OleDbType.DBTimeStamp, 5472, "CWR_VALIDFROM"));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_VALIDUNTIL", System.Data.OleDb.OleDbType.DBTimeStamp, 5472, "CWR_VALIDUNTIL"));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_ORDERNO", System.Data.OleDb.OleDbType.VarChar, 40, "CWR_ORDERNO"));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_SECPASS_YN", System.Data.OleDb.OleDbType.VarChar, 1, "CWR_SECPASS_YN"));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_ENTRYDATECOOD", System.Data.OleDb.OleDbType.DBTimeStamp, 5472, "CWR_ENTRYDATECOOD"));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_ENTRYCOODID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CWR_ENTRYCOODID", System.Data.DataRowVersion.Current, null));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_CHKOFFDATECOOD", System.Data.OleDb.OleDbType.DBTimeStamp, 5472, "CWR_CHKOFFDATECOOD"));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_CHKOFFCOODID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CWR_CHKOFFCOODID", System.Data.DataRowVersion.Current, null));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_ORDERCOMPLET_YN", System.Data.OleDb.OleDbType.VarChar, 1, "CWR_ORDERCOMPLET_YN"));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_CHANGEUSER", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CWR_CHANGEUSER", System.Data.DataRowVersion.Current, null));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_TIMESTAMP", System.Data.OleDb.OleDbType.DBTimeStamp, 5472, "CWR_TIMESTAMP"));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_RETURNCODE_ZKS", System.Data.OleDb.OleDbType.VarChar, 2, "CWR_RETURNCODE_ZKS"));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_ACCESS", System.Data.OleDb.OleDbType.VarChar, 10, "CWR_ACCESS"));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_STATUS", System.Data.OleDb.OleDbType.VarChar, 20, "CWR_STATUS"));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_MND_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CWR_MND_ID", System.Data.DataRowVersion.Current, null));
			// 
			// oleDbSelectCommand1
			// 
			this.oleDbSelectCommand1.CommandText = @"SELECT CWR_ID, CWR_TK, CWR_PERSNO, CWR_EXCO_ID, CWR_DEPT_ID, CWR_CRA_ID, CWR_ECOD_ID, CWR_SUBE_ID, CWR_OVEREIGHTEEN_YN, CWR_IDCARDNO, CWR_SURNAME, CWR_FIRSTNAME, CWR_DATEOFBIRTH, CWR_PLACEOFBIRTH, CWR_DATECREATED, CWR_VALIDFROM, CWR_VALIDUNTIL, CWR_ORDERNO, CWR_SECPASS_YN, CWR_ENTRYDATECOOD, CWR_ENTRYCOODID, CWR_CHKOFFDATECOOD, CWR_CHKOFFCOODID, CWR_ORDERCOMPLET_YN, CWR_CHANGEUSER, CWR_TIMESTAMP, CWR_RETURNCODE_ZKS, CWR_ACCESS, CWR_STATUS, CWR_MND_ID FROM FPASS_COWORKER";
			this.oleDbSelectCommand1.Connection = this.oleDbConnection1;
			// 
			// oleDbUpdateCommand1
			// 
			this.oleDbUpdateCommand1.CommandText = "UPDATE FPASS_COWORKER SET CWR_ID = ?, CWR_TK = ?, CWR_PERSNO = ?, CWR_EXCO_ID = ?" +
				", CWR_DEPT_ID = ?, CWR_CRA_ID = ?, CWR_ECOD_ID = ?, CWR_SUBE_ID = ?, CWR_OVEREIG" +
				"HTEEN_YN = ?, CWR_IDCARDNO = ?, CWR_SURNAME = ?, CWR_FIRSTNAME = ?, CWR_DATEOFBI" +
				"RTH = ?, CWR_PLACEOFBIRTH = ?, CWR_DATECREATED = ?, CWR_VALIDFROM = ?, CWR_VALID" +
				"UNTIL = ?, CWR_ORDERNO = ?, CWR_SECPASS_YN = ?, CWR_ENTRYDATECOOD = ?, CWR_ENTRY" +
				"COODID = ?, CWR_CHKOFFDATECOOD = ?, CWR_CHKOFFCOODID = ?, CWR_ORDERCOMPLET_YN = " +
				"?, CWR_CHANGEUSER = ?, CWR_TIMESTAMP = ?, CWR_RETURNCODE_ZKS = ?, CWR_ACCESS = ?" +
				", CWR_STATUS = ?, CWR_MND_ID = ? WHERE (CWR_ID = ?) AND (CWR_ACCESS = ? OR ? IS " +
				"NULL AND CWR_ACCESS IS NULL) AND (CWR_CHANGEUSER = ?) AND (CWR_CHKOFFCOODID = ? " +
				"OR ? IS NULL AND CWR_CHKOFFCOODID IS NULL) AND (CWR_CHKOFFDATECOOD = ? OR ? IS N" +
				"ULL AND CWR_CHKOFFDATECOOD IS NULL) AND (CWR_CRA_ID = ? OR ? IS NULL AND CWR_CRA" +
				"_ID IS NULL) AND (CWR_DATECREATED = ?) AND (CWR_DATEOFBIRTH = ?) AND (CWR_DEPT_I" +
				"D = ? OR ? IS NULL AND CWR_DEPT_ID IS NULL) AND (CWR_ECOD_ID = ?) AND (CWR_ENTRY" +
				"COODID = ? OR ? IS NULL AND CWR_ENTRYCOODID IS NULL) AND (CWR_ENTRYDATECOOD = ? " +
				"OR ? IS NULL AND CWR_ENTRYDATECOOD IS NULL) AND (CWR_EXCO_ID = ?) AND (CWR_FIRST" +
				"NAME = ?) AND (CWR_IDCARDNO = ? OR ? IS NULL AND CWR_IDCARDNO IS NULL) AND (CWR_" +
				"MND_ID = ?) AND (CWR_ORDERCOMPLET_YN = ?) AND (CWR_ORDERNO = ? OR ? IS NULL AND " +
				"CWR_ORDERNO IS NULL) AND (CWR_OVEREIGHTEEN_YN = ?) AND (CWR_PERSNO = ?) AND (CWR" +
				"_PLACEOFBIRTH = ?) AND (CWR_RETURNCODE_ZKS = ? OR ? IS NULL AND CWR_RETURNCODE_Z" +
				"KS IS NULL) AND (CWR_SECPASS_YN = ?) AND (CWR_STATUS = ? OR ? IS NULL AND CWR_ST" +
				"ATUS IS NULL) AND (CWR_SUBE_ID = ? OR ? IS NULL AND CWR_SUBE_ID IS NULL) AND (CW" +
				"R_SURNAME = ?) AND (CWR_TIMESTAMP = ?) AND (CWR_TK = ?) AND (CWR_VALIDFROM = ?) " +
				"AND (CWR_VALIDUNTIL = ?)";
			this.oleDbUpdateCommand1.Connection = this.oleDbConnection1;
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CWR_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_TK", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(3)), ((System.Byte)(0)), "CWR_TK", System.Data.DataRowVersion.Current, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_PERSNO", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(8)), ((System.Byte)(0)), "CWR_PERSNO", System.Data.DataRowVersion.Current, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_EXCO_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CWR_EXCO_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_DEPT_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CWR_DEPT_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_CRA_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CWR_CRA_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_ECOD_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CWR_ECOD_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_SUBE_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CWR_SUBE_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_OVEREIGHTEEN_YN", System.Data.OleDb.OleDbType.VarChar, 1, "CWR_OVEREIGHTEEN_YN"));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_IDCARDNO", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(15)), ((System.Byte)(0)), "CWR_IDCARDNO", System.Data.DataRowVersion.Current, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_SURNAME", System.Data.OleDb.OleDbType.VarChar, 30, "CWR_SURNAME"));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_FIRSTNAME", System.Data.OleDb.OleDbType.VarChar, 30, "CWR_FIRSTNAME"));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_DATEOFBIRTH", System.Data.OleDb.OleDbType.DBTimeStamp, 5472, "CWR_DATEOFBIRTH"));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_PLACEOFBIRTH", System.Data.OleDb.OleDbType.VarChar, 30, "CWR_PLACEOFBIRTH"));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_DATECREATED", System.Data.OleDb.OleDbType.DBTimeStamp, 5472, "CWR_DATECREATED"));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_VALIDFROM", System.Data.OleDb.OleDbType.DBTimeStamp, 5472, "CWR_VALIDFROM"));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_VALIDUNTIL", System.Data.OleDb.OleDbType.DBTimeStamp, 5472, "CWR_VALIDUNTIL"));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_ORDERNO", System.Data.OleDb.OleDbType.VarChar, 40, "CWR_ORDERNO"));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_SECPASS_YN", System.Data.OleDb.OleDbType.VarChar, 1, "CWR_SECPASS_YN"));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_ENTRYDATECOOD", System.Data.OleDb.OleDbType.DBTimeStamp, 5472, "CWR_ENTRYDATECOOD"));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_ENTRYCOODID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CWR_ENTRYCOODID", System.Data.DataRowVersion.Current, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_CHKOFFDATECOOD", System.Data.OleDb.OleDbType.DBTimeStamp, 5472, "CWR_CHKOFFDATECOOD"));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_CHKOFFCOODID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CWR_CHKOFFCOODID", System.Data.DataRowVersion.Current, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_ORDERCOMPLET_YN", System.Data.OleDb.OleDbType.VarChar, 1, "CWR_ORDERCOMPLET_YN"));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_CHANGEUSER", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CWR_CHANGEUSER", System.Data.DataRowVersion.Current, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_TIMESTAMP", System.Data.OleDb.OleDbType.DBTimeStamp, 5472, "CWR_TIMESTAMP"));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_RETURNCODE_ZKS", System.Data.OleDb.OleDbType.VarChar, 2, "CWR_RETURNCODE_ZKS"));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_ACCESS", System.Data.OleDb.OleDbType.VarChar, 10, "CWR_ACCESS"));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_STATUS", System.Data.OleDb.OleDbType.VarChar, 20, "CWR_STATUS"));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CWR_MND_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CWR_MND_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CWR_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_ACCESS", System.Data.OleDb.OleDbType.VarChar, 10, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CWR_ACCESS", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_ACCESS1", System.Data.OleDb.OleDbType.VarChar, 10, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CWR_ACCESS", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_CHANGEUSER", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CWR_CHANGEUSER", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_CHKOFFCOODID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CWR_CHKOFFCOODID", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_CHKOFFCOODID1", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CWR_CHKOFFCOODID", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_CHKOFFDATECOOD", System.Data.OleDb.OleDbType.DBTimeStamp, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CWR_CHKOFFDATECOOD", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_CHKOFFDATECOOD1", System.Data.OleDb.OleDbType.DBTimeStamp, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CWR_CHKOFFDATECOOD", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_CRA_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CWR_CRA_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_CRA_ID1", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CWR_CRA_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_DATECREATED", System.Data.OleDb.OleDbType.DBTimeStamp, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CWR_DATECREATED", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_DATEOFBIRTH", System.Data.OleDb.OleDbType.DBTimeStamp, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CWR_DATEOFBIRTH", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_DEPT_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CWR_DEPT_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_DEPT_ID1", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CWR_DEPT_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_ECOD_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CWR_ECOD_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_ENTRYCOODID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CWR_ENTRYCOODID", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_ENTRYCOODID1", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CWR_ENTRYCOODID", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_ENTRYDATECOOD", System.Data.OleDb.OleDbType.DBTimeStamp, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CWR_ENTRYDATECOOD", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_ENTRYDATECOOD1", System.Data.OleDb.OleDbType.DBTimeStamp, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CWR_ENTRYDATECOOD", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_EXCO_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CWR_EXCO_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_FIRSTNAME", System.Data.OleDb.OleDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CWR_FIRSTNAME", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_IDCARDNO", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(15)), ((System.Byte)(0)), "CWR_IDCARDNO", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_IDCARDNO1", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(15)), ((System.Byte)(0)), "CWR_IDCARDNO", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_MND_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CWR_MND_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_ORDERCOMPLET_YN", System.Data.OleDb.OleDbType.VarChar, 1, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CWR_ORDERCOMPLET_YN", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_ORDERNO", System.Data.OleDb.OleDbType.VarChar, 40, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CWR_ORDERNO", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_ORDERNO1", System.Data.OleDb.OleDbType.VarChar, 40, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CWR_ORDERNO", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_OVEREIGHTEEN_YN", System.Data.OleDb.OleDbType.VarChar, 1, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CWR_OVEREIGHTEEN_YN", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_PERSNO", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(8)), ((System.Byte)(0)), "CWR_PERSNO", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_PLACEOFBIRTH", System.Data.OleDb.OleDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CWR_PLACEOFBIRTH", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_RETURNCODE_ZKS", System.Data.OleDb.OleDbType.VarChar, 2, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CWR_RETURNCODE_ZKS", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_RETURNCODE_ZKS1", System.Data.OleDb.OleDbType.VarChar, 2, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CWR_RETURNCODE_ZKS", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_SECPASS_YN", System.Data.OleDb.OleDbType.VarChar, 1, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CWR_SECPASS_YN", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_STATUS", System.Data.OleDb.OleDbType.VarChar, 20, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CWR_STATUS", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_STATUS1", System.Data.OleDb.OleDbType.VarChar, 20, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CWR_STATUS", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_SUBE_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CWR_SUBE_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_SUBE_ID1", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CWR_SUBE_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_SURNAME", System.Data.OleDb.OleDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CWR_SURNAME", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_TIMESTAMP", System.Data.OleDb.OleDbType.DBTimeStamp, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CWR_TIMESTAMP", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_TK", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(3)), ((System.Byte)(0)), "CWR_TK", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_VALIDFROM", System.Data.OleDb.OleDbType.DBTimeStamp, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CWR_VALIDFROM", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CWR_VALIDUNTIL", System.Data.OleDb.OleDbType.DBTimeStamp, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CWR_VALIDUNTIL", System.Data.DataRowVersion.Original, null));
			// 
			// oleDbConnection3
			// 
			this.oleDbConnection3.ConnectionString = "Provider=\"MSDAORA.1\";User ID=fpassV1;Data Source=KOELN8I;Password=fpass";
			// 
			// tempDAExContractor
			// 
			this.tempDAExContractor.DeleteCommand = this.oleDbDeleteCommand6;
			this.tempDAExContractor.InsertCommand = this.oleDbInsertCommand7;
			this.tempDAExContractor.SelectCommand = this.oleDbSelectCommand7;
			this.tempDAExContractor.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																										 new System.Data.Common.DataTableMapping("Table", "FPASS_EXCONTRACTOR", new System.Data.Common.DataColumnMapping[] {
																																																							   new System.Data.Common.DataColumnMapping("EXCO_CHANGEUSER", "EXCO_CHANGEUSER"),
																																																							   new System.Data.Common.DataColumnMapping("EXCO_TIMESTAMP", "EXCO_TIMESTAMP"),
																																																							   new System.Data.Common.DataColumnMapping("EXCO_ID", "EXCO_ID"),
																																																							   new System.Data.Common.DataColumnMapping("EXCO_MND_ID", "EXCO_MND_ID"),
																																																							   new System.Data.Common.DataColumnMapping("EXCO_NAME", "EXCO_NAME"),
																																																							   new System.Data.Common.DataColumnMapping("EXCO_CITY", "EXCO_CITY"),
																																																							   new System.Data.Common.DataColumnMapping("EXCO_POSTCODE", "EXCO_POSTCODE"),
																																																							   new System.Data.Common.DataColumnMapping("EXCO_COUNTRY", "EXCO_COUNTRY"),
																																																							   new System.Data.Common.DataColumnMapping("EXCO_STREET", "EXCO_STREET"),
																																																							   new System.Data.Common.DataColumnMapping("EXCO_SUPERFIRSTNAME", "EXCO_SUPERFIRSTNAME"),
																																																							   new System.Data.Common.DataColumnMapping("EXCO_SUPERSURNAME", "EXCO_SUPERSURNAME"),
																																																							   new System.Data.Common.DataColumnMapping("EXCO_TELEPHONENO", "EXCO_TELEPHONENO"),
																																																							   new System.Data.Common.DataColumnMapping("EXCO_FAX", "EXCO_FAX"),
																																																							   new System.Data.Common.DataColumnMapping("EXCO_MOBILEPHONE", "EXCO_MOBILEPHONE"),
																																																							   new System.Data.Common.DataColumnMapping("EXCO_STATUS", "EXCO_STATUS")})});
			this.tempDAExContractor.UpdateCommand = this.oleDbUpdateCommand6;
			// 
			// oleDbDeleteCommand6
			// 
			this.oleDbDeleteCommand6.CommandText = @"DELETE FROM FPASS_EXCONTRACTOR WHERE (EXCO_ID = ?) AND (EXCO_CHANGEUSER = ?) AND (EXCO_CITY = ? OR ? IS NULL AND EXCO_CITY IS NULL) AND (EXCO_COUNTRY = ? OR ? IS NULL AND EXCO_COUNTRY IS NULL) AND (EXCO_FAX = ? OR ? IS NULL AND EXCO_FAX IS NULL) AND (EXCO_MND_ID = ?) AND (EXCO_MOBILEPHONE = ? OR ? IS NULL AND EXCO_MOBILEPHONE IS NULL) AND (EXCO_NAME = ?) AND (EXCO_POSTCODE = ? OR ? IS NULL AND EXCO_POSTCODE IS NULL) AND (EXCO_STATUS = ? OR ? IS NULL AND EXCO_STATUS IS NULL) AND (EXCO_STREET = ? OR ? IS NULL AND EXCO_STREET IS NULL) AND (EXCO_SUPERFIRSTNAME = ? OR ? IS NULL AND EXCO_SUPERFIRSTNAME IS NULL) AND (EXCO_SUPERSURNAME = ? OR ? IS NULL AND EXCO_SUPERSURNAME IS NULL) AND (EXCO_TELEPHONENO = ? OR ? IS NULL AND EXCO_TELEPHONENO IS NULL) AND (EXCO_TIMESTAMP = ?)";
			this.oleDbDeleteCommand6.Connection = this.oleDbConnection3;
			this.oleDbDeleteCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EXCO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "EXCO_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EXCO_CHANGEUSER", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "EXCO_CHANGEUSER", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EXCO_CITY", System.Data.OleDb.OleDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EXCO_CITY", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EXCO_CITY1", System.Data.OleDb.OleDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EXCO_CITY", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EXCO_COUNTRY", System.Data.OleDb.OleDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EXCO_COUNTRY", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EXCO_COUNTRY1", System.Data.OleDb.OleDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EXCO_COUNTRY", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EXCO_FAX", System.Data.OleDb.OleDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EXCO_FAX", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EXCO_FAX1", System.Data.OleDb.OleDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EXCO_FAX", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EXCO_MND_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "EXCO_MND_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EXCO_MOBILEPHONE", System.Data.OleDb.OleDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EXCO_MOBILEPHONE", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EXCO_MOBILEPHONE1", System.Data.OleDb.OleDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EXCO_MOBILEPHONE", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EXCO_NAME", System.Data.OleDb.OleDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EXCO_NAME", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EXCO_POSTCODE", System.Data.OleDb.OleDbType.Decimal, 10, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EXCO_POSTCODE", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EXCO_POSTCODE1", System.Data.OleDb.OleDbType.Decimal, 10, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EXCO_POSTCODE", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EXCO_STATUS", System.Data.OleDb.OleDbType.VarChar, 20, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EXCO_STATUS", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EXCO_STATUS1", System.Data.OleDb.OleDbType.VarChar, 20, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EXCO_STATUS", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EXCO_STREET", System.Data.OleDb.OleDbType.VarChar, 40, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EXCO_STREET", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EXCO_STREET1", System.Data.OleDb.OleDbType.VarChar, 40, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EXCO_STREET", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EXCO_SUPERFIRSTNAME", System.Data.OleDb.OleDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EXCO_SUPERFIRSTNAME", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EXCO_SUPERFIRSTNAME1", System.Data.OleDb.OleDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EXCO_SUPERFIRSTNAME", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EXCO_SUPERSURNAME", System.Data.OleDb.OleDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EXCO_SUPERSURNAME", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EXCO_SUPERSURNAME1", System.Data.OleDb.OleDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EXCO_SUPERSURNAME", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EXCO_TELEPHONENO", System.Data.OleDb.OleDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EXCO_TELEPHONENO", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EXCO_TELEPHONENO1", System.Data.OleDb.OleDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EXCO_TELEPHONENO", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EXCO_TIMESTAMP", System.Data.OleDb.OleDbType.DBTimeStamp, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EXCO_TIMESTAMP", System.Data.DataRowVersion.Original, null));
			// 
			// oleDbInsertCommand7
			// 
			this.oleDbInsertCommand7.CommandText = @"INSERT INTO FPASS_EXCONTRACTOR(EXCO_CHANGEUSER, EXCO_TIMESTAMP, EXCO_ID, EXCO_MND_ID, EXCO_NAME, EXCO_CITY, EXCO_POSTCODE, EXCO_COUNTRY, EXCO_STREET, EXCO_SUPERFIRSTNAME, EXCO_SUPERSURNAME, EXCO_TELEPHONENO, EXCO_FAX, EXCO_MOBILEPHONE, EXCO_STATUS) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
			this.oleDbInsertCommand7.Connection = this.oleDbConnection3;
			this.oleDbInsertCommand7.Parameters.Add(new System.Data.OleDb.OleDbParameter("EXCO_CHANGEUSER", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "EXCO_CHANGEUSER", System.Data.DataRowVersion.Current, null));
			this.oleDbInsertCommand7.Parameters.Add(new System.Data.OleDb.OleDbParameter("EXCO_TIMESTAMP", System.Data.OleDb.OleDbType.DBTimeStamp, 0, "EXCO_TIMESTAMP"));
			this.oleDbInsertCommand7.Parameters.Add(new System.Data.OleDb.OleDbParameter("EXCO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "EXCO_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbInsertCommand7.Parameters.Add(new System.Data.OleDb.OleDbParameter("EXCO_MND_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "EXCO_MND_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbInsertCommand7.Parameters.Add(new System.Data.OleDb.OleDbParameter("EXCO_NAME", System.Data.OleDb.OleDbType.VarChar, 30, "EXCO_NAME"));
			this.oleDbInsertCommand7.Parameters.Add(new System.Data.OleDb.OleDbParameter("EXCO_CITY", System.Data.OleDb.OleDbType.VarChar, 30, "EXCO_CITY"));
			this.oleDbInsertCommand7.Parameters.Add(new System.Data.OleDb.OleDbParameter("EXCO_POSTCODE", System.Data.OleDb.OleDbType.Decimal, 10, "EXCO_POSTCODE"));
			this.oleDbInsertCommand7.Parameters.Add(new System.Data.OleDb.OleDbParameter("EXCO_COUNTRY", System.Data.OleDb.OleDbType.VarChar, 30, "EXCO_COUNTRY"));
			this.oleDbInsertCommand7.Parameters.Add(new System.Data.OleDb.OleDbParameter("EXCO_STREET", System.Data.OleDb.OleDbType.VarChar, 40, "EXCO_STREET"));
			this.oleDbInsertCommand7.Parameters.Add(new System.Data.OleDb.OleDbParameter("EXCO_SUPERFIRSTNAME", System.Data.OleDb.OleDbType.VarChar, 30, "EXCO_SUPERFIRSTNAME"));
			this.oleDbInsertCommand7.Parameters.Add(new System.Data.OleDb.OleDbParameter("EXCO_SUPERSURNAME", System.Data.OleDb.OleDbType.VarChar, 30, "EXCO_SUPERSURNAME"));
			this.oleDbInsertCommand7.Parameters.Add(new System.Data.OleDb.OleDbParameter("EXCO_TELEPHONENO", System.Data.OleDb.OleDbType.VarChar, 30, "EXCO_TELEPHONENO"));
			this.oleDbInsertCommand7.Parameters.Add(new System.Data.OleDb.OleDbParameter("EXCO_FAX", System.Data.OleDb.OleDbType.VarChar, 30, "EXCO_FAX"));
			this.oleDbInsertCommand7.Parameters.Add(new System.Data.OleDb.OleDbParameter("EXCO_MOBILEPHONE", System.Data.OleDb.OleDbType.VarChar, 30, "EXCO_MOBILEPHONE"));
			this.oleDbInsertCommand7.Parameters.Add(new System.Data.OleDb.OleDbParameter("EXCO_STATUS", System.Data.OleDb.OleDbType.VarChar, 20, "EXCO_STATUS"));
			// 
			// oleDbSelectCommand7
			// 
			this.oleDbSelectCommand7.CommandText = "SELECT EXCO_CHANGEUSER, EXCO_TIMESTAMP, EXCO_ID, EXCO_MND_ID, EXCO_NAME, EXCO_CIT" +
				"Y, EXCO_POSTCODE, EXCO_COUNTRY, EXCO_STREET, EXCO_SUPERFIRSTNAME, EXCO_SUPERSURN" +
				"AME, EXCO_TELEPHONENO, EXCO_FAX, EXCO_MOBILEPHONE, EXCO_STATUS FROM FPASS_EXCONT" +
				"RACTOR";
			this.oleDbSelectCommand7.Connection = this.oleDbConnection3;
			// 
			// oleDbUpdateCommand6
			// 
			this.oleDbUpdateCommand6.CommandText = @"UPDATE FPASS_EXCONTRACTOR SET EXCO_CHANGEUSER = ?, EXCO_TIMESTAMP = ?, EXCO_ID = ?, EXCO_MND_ID = ?, EXCO_NAME = ?, EXCO_CITY = ?, EXCO_POSTCODE = ?, EXCO_COUNTRY = ?, EXCO_STREET = ?, EXCO_SUPERFIRSTNAME = ?, EXCO_SUPERSURNAME = ?, EXCO_TELEPHONENO = ?, EXCO_FAX = ?, EXCO_MOBILEPHONE = ?, EXCO_STATUS = ? WHERE (EXCO_ID = ?) AND (EXCO_CHANGEUSER = ?) AND (EXCO_CITY = ? OR ? IS NULL AND EXCO_CITY IS NULL) AND (EXCO_COUNTRY = ? OR ? IS NULL AND EXCO_COUNTRY IS NULL) AND (EXCO_FAX = ? OR ? IS NULL AND EXCO_FAX IS NULL) AND (EXCO_MND_ID = ?) AND (EXCO_MOBILEPHONE = ? OR ? IS NULL AND EXCO_MOBILEPHONE IS NULL) AND (EXCO_NAME = ?) AND (EXCO_POSTCODE = ? OR ? IS NULL AND EXCO_POSTCODE IS NULL) AND (EXCO_STATUS = ? OR ? IS NULL AND EXCO_STATUS IS NULL) AND (EXCO_STREET = ? OR ? IS NULL AND EXCO_STREET IS NULL) AND (EXCO_SUPERFIRSTNAME = ? OR ? IS NULL AND EXCO_SUPERFIRSTNAME IS NULL) AND (EXCO_SUPERSURNAME = ? OR ? IS NULL AND EXCO_SUPERSURNAME IS NULL) AND (EXCO_TELEPHONENO = ? OR ? IS NULL AND EXCO_TELEPHONENO IS NULL) AND (EXCO_TIMESTAMP = ?)";
			this.oleDbUpdateCommand6.Connection = this.oleDbConnection3;
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("EXCO_CHANGEUSER", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "EXCO_CHANGEUSER", System.Data.DataRowVersion.Current, null));
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("EXCO_TIMESTAMP", System.Data.OleDb.OleDbType.DBTimeStamp, 0, "EXCO_TIMESTAMP"));
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("EXCO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "EXCO_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("EXCO_MND_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "EXCO_MND_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("EXCO_NAME", System.Data.OleDb.OleDbType.VarChar, 30, "EXCO_NAME"));
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("EXCO_CITY", System.Data.OleDb.OleDbType.VarChar, 30, "EXCO_CITY"));
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("EXCO_POSTCODE", System.Data.OleDb.OleDbType.Decimal, 10, "EXCO_POSTCODE"));
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("EXCO_COUNTRY", System.Data.OleDb.OleDbType.VarChar, 30, "EXCO_COUNTRY"));
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("EXCO_STREET", System.Data.OleDb.OleDbType.VarChar, 40, "EXCO_STREET"));
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("EXCO_SUPERFIRSTNAME", System.Data.OleDb.OleDbType.VarChar, 30, "EXCO_SUPERFIRSTNAME"));
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("EXCO_SUPERSURNAME", System.Data.OleDb.OleDbType.VarChar, 30, "EXCO_SUPERSURNAME"));
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("EXCO_TELEPHONENO", System.Data.OleDb.OleDbType.VarChar, 30, "EXCO_TELEPHONENO"));
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("EXCO_FAX", System.Data.OleDb.OleDbType.VarChar, 30, "EXCO_FAX"));
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("EXCO_MOBILEPHONE", System.Data.OleDb.OleDbType.VarChar, 30, "EXCO_MOBILEPHONE"));
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("EXCO_STATUS", System.Data.OleDb.OleDbType.VarChar, 20, "EXCO_STATUS"));
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EXCO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "EXCO_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EXCO_CHANGEUSER", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "EXCO_CHANGEUSER", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EXCO_CITY", System.Data.OleDb.OleDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EXCO_CITY", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EXCO_CITY1", System.Data.OleDb.OleDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EXCO_CITY", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EXCO_COUNTRY", System.Data.OleDb.OleDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EXCO_COUNTRY", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EXCO_COUNTRY1", System.Data.OleDb.OleDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EXCO_COUNTRY", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EXCO_FAX", System.Data.OleDb.OleDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EXCO_FAX", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EXCO_FAX1", System.Data.OleDb.OleDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EXCO_FAX", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EXCO_MND_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "EXCO_MND_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EXCO_MOBILEPHONE", System.Data.OleDb.OleDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EXCO_MOBILEPHONE", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EXCO_MOBILEPHONE1", System.Data.OleDb.OleDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EXCO_MOBILEPHONE", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EXCO_NAME", System.Data.OleDb.OleDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EXCO_NAME", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EXCO_POSTCODE", System.Data.OleDb.OleDbType.Decimal, 10, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EXCO_POSTCODE", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EXCO_POSTCODE1", System.Data.OleDb.OleDbType.Decimal, 10, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EXCO_POSTCODE", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EXCO_STATUS", System.Data.OleDb.OleDbType.VarChar, 20, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EXCO_STATUS", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EXCO_STATUS1", System.Data.OleDb.OleDbType.VarChar, 20, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EXCO_STATUS", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EXCO_STREET", System.Data.OleDb.OleDbType.VarChar, 40, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EXCO_STREET", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EXCO_STREET1", System.Data.OleDb.OleDbType.VarChar, 40, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EXCO_STREET", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EXCO_SUPERFIRSTNAME", System.Data.OleDb.OleDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EXCO_SUPERFIRSTNAME", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EXCO_SUPERFIRSTNAME1", System.Data.OleDb.OleDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EXCO_SUPERFIRSTNAME", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EXCO_SUPERSURNAME", System.Data.OleDb.OleDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EXCO_SUPERSURNAME", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EXCO_SUPERSURNAME1", System.Data.OleDb.OleDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EXCO_SUPERSURNAME", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EXCO_TELEPHONENO", System.Data.OleDb.OleDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EXCO_TELEPHONENO", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EXCO_TELEPHONENO1", System.Data.OleDb.OleDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EXCO_TELEPHONENO", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EXCO_TIMESTAMP", System.Data.OleDb.OleDbType.DBTimeStamp, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EXCO_TIMESTAMP", System.Data.DataRowVersion.Original, null));
			// 
			// oleDbConnection2
			// 
			this.oleDbConnection2.ConnectionString = "Provider=\"OraOLEDB.Oracle.1\";User ID=FPASSV1;Data Source=koeln8i;Extended Propert" +
				"ies=;Persist Security Info=True;Password=FPASS";
			// 
			// tempDAPlant
			// 
			this.tempDAPlant.DeleteCommand = this.oleDbDeleteCommand2;
			this.tempDAPlant.InsertCommand = this.oleDbInsertCommand2;
			this.tempDAPlant.SelectCommand = this.oleDbSelectCommand2;
			this.tempDAPlant.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																								  new System.Data.Common.DataTableMapping("Table", "FPASS_PLANT", new System.Data.Common.DataColumnMapping[] {
																																																				 new System.Data.Common.DataColumnMapping("PL_CHANGEUSER", "PL_CHANGEUSER"),
																																																				 new System.Data.Common.DataColumnMapping("PL_TIMESTAMP", "PL_TIMESTAMP"),
																																																				 new System.Data.Common.DataColumnMapping("PL_ID", "PL_ID"),
																																																				 new System.Data.Common.DataColumnMapping("PL_MND_ID", "PL_MND_ID"),
																																																				 new System.Data.Common.DataColumnMapping("PL_NAME", "PL_NAME")})});
			this.tempDAPlant.UpdateCommand = this.oleDbUpdateCommand2;
			// 
			// oleDbDeleteCommand2
			// 
			this.oleDbDeleteCommand2.CommandText = "DELETE FROM FPASS_PLANT WHERE (PL_ID = ?) AND (PL_CHANGEUSER = ?) AND (PL_MND_ID " +
				"= ?) AND (PL_NAME = ?) AND (PL_TIMESTAMP = ?)";
			this.oleDbDeleteCommand2.Connection = this.oleDbConnection2;
			this.oleDbDeleteCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PL_ID", System.Data.OleDb.OleDbType.Double, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "PL_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PL_CHANGEUSER", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "PL_CHANGEUSER", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PL_MND_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "PL_MND_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PL_NAME", System.Data.OleDb.OleDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "PL_NAME", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PL_TIMESTAMP", System.Data.OleDb.OleDbType.DBTimeStamp, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "PL_TIMESTAMP", System.Data.DataRowVersion.Original, null));
			// 
			// oleDbInsertCommand2
			// 
			this.oleDbInsertCommand2.CommandText = "INSERT INTO FPASS_PLANT(PL_CHANGEUSER, PL_TIMESTAMP, PL_ID, PL_MND_ID, PL_NAME) V" +
				"ALUES (?, ?, ?, ?, ?)";
			this.oleDbInsertCommand2.Connection = this.oleDbConnection2;
			this.oleDbInsertCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("PL_CHANGEUSER", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "PL_CHANGEUSER", System.Data.DataRowVersion.Current, null));
			this.oleDbInsertCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("PL_TIMESTAMP", System.Data.OleDb.OleDbType.DBTimeStamp, 5472, "PL_TIMESTAMP"));
			this.oleDbInsertCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("PL_ID", System.Data.OleDb.OleDbType.Double, 5472, "PL_ID"));
			this.oleDbInsertCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("PL_MND_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "PL_MND_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbInsertCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("PL_NAME", System.Data.OleDb.OleDbType.VarChar, 30, "PL_NAME"));
			// 
			// oleDbSelectCommand2
			// 
			this.oleDbSelectCommand2.CommandText = "SELECT PL_CHANGEUSER, PL_TIMESTAMP, PL_ID, PL_MND_ID, PL_NAME FROM FPASS_PLANT";
			this.oleDbSelectCommand2.Connection = this.oleDbConnection2;
			// 
			// oleDbUpdateCommand2
			// 
			this.oleDbUpdateCommand2.CommandText = "UPDATE FPASS_PLANT SET PL_CHANGEUSER = ?, PL_TIMESTAMP = ?, PL_ID = ?, PL_MND_ID " +
				"= ?, PL_NAME = ? WHERE (PL_ID = ?) AND (PL_CHANGEUSER = ?) AND (PL_MND_ID = ?) A" +
				"ND (PL_NAME = ?) AND (PL_TIMESTAMP = ?)";
			this.oleDbUpdateCommand2.Connection = this.oleDbConnection2;
			this.oleDbUpdateCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("PL_CHANGEUSER", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "PL_CHANGEUSER", System.Data.DataRowVersion.Current, null));
			this.oleDbUpdateCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("PL_TIMESTAMP", System.Data.OleDb.OleDbType.DBTimeStamp, 5472, "PL_TIMESTAMP"));
			this.oleDbUpdateCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("PL_ID", System.Data.OleDb.OleDbType.Double, 5472, "PL_ID"));
			this.oleDbUpdateCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("PL_MND_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "PL_MND_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbUpdateCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("PL_NAME", System.Data.OleDb.OleDbType.VarChar, 30, "PL_NAME"));
			this.oleDbUpdateCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PL_ID", System.Data.OleDb.OleDbType.Double, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "PL_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PL_CHANGEUSER", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "PL_CHANGEUSER", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PL_MND_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "PL_MND_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PL_NAME", System.Data.OleDb.OleDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "PL_NAME", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PL_TIMESTAMP", System.Data.OleDb.OleDbType.DBTimeStamp, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "PL_TIMESTAMP", System.Data.DataRowVersion.Original, null));
			// 
			// tempDADepartment
			// 
			this.tempDADepartment.DeleteCommand = this.oleDbDeleteCommand3;
			this.tempDADepartment.InsertCommand = this.oleDbInsertCommand3;
			this.tempDADepartment.SelectCommand = this.oleDbSelectCommand3;
			this.tempDADepartment.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																									   new System.Data.Common.DataTableMapping("Table", "FPASS_DEPARTMENT", new System.Data.Common.DataColumnMapping[] {
																																																						   new System.Data.Common.DataColumnMapping("DEPT_CHANGEUSER", "DEPT_CHANGEUSER"),
																																																						   new System.Data.Common.DataColumnMapping("DEPT_TIMESTAMP", "DEPT_TIMESTAMP"),
																																																						   new System.Data.Common.DataColumnMapping("DEPT_ID", "DEPT_ID"),
																																																						   new System.Data.Common.DataColumnMapping("DEPT_MND_ID", "DEPT_MND_ID"),
																																																						   new System.Data.Common.DataColumnMapping("DEPT_DEPARTMENT", "DEPT_DEPARTMENT")})});
			this.tempDADepartment.UpdateCommand = this.oleDbUpdateCommand3;
			// 
			// oleDbDeleteCommand3
			// 
			this.oleDbDeleteCommand3.CommandText = "DELETE FROM FPASS_DEPARTMENT WHERE (DEPT_ID = ?) AND (DEPT_CHANGEUSER = ?) AND (D" +
				"EPT_DEPARTMENT = ?) AND (DEPT_MND_ID = ?) AND (DEPT_TIMESTAMP = ?)";
			this.oleDbDeleteCommand3.Connection = this.oleDbConnection3;
			this.oleDbDeleteCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_DEPT_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "DEPT_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_DEPT_CHANGEUSER", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "DEPT_CHANGEUSER", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_DEPT_DEPARTMENT", System.Data.OleDb.OleDbType.VarChar, 35, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "DEPT_DEPARTMENT", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_DEPT_MND_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "DEPT_MND_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_DEPT_TIMESTAMP", System.Data.OleDb.OleDbType.DBTimeStamp, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "DEPT_TIMESTAMP", System.Data.DataRowVersion.Original, null));
			// 
			// oleDbInsertCommand3
			// 
			this.oleDbInsertCommand3.CommandText = "INSERT INTO FPASS_DEPARTMENT(DEPT_CHANGEUSER, DEPT_TIMESTAMP, DEPT_ID, DEPT_MND_I" +
				"D, DEPT_DEPARTMENT) VALUES (?, ?, ?, ?, ?)";
			this.oleDbInsertCommand3.Connection = this.oleDbConnection3;
			this.oleDbInsertCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("DEPT_CHANGEUSER", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "DEPT_CHANGEUSER", System.Data.DataRowVersion.Current, null));
			this.oleDbInsertCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("DEPT_TIMESTAMP", System.Data.OleDb.OleDbType.DBTimeStamp, 0, "DEPT_TIMESTAMP"));
			this.oleDbInsertCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("DEPT_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "DEPT_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbInsertCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("DEPT_MND_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "DEPT_MND_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbInsertCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("DEPT_DEPARTMENT", System.Data.OleDb.OleDbType.VarChar, 35, "DEPT_DEPARTMENT"));
			// 
			// oleDbSelectCommand3
			// 
			this.oleDbSelectCommand3.CommandText = "SELECT DEPT_CHANGEUSER, DEPT_TIMESTAMP, DEPT_ID, DEPT_MND_ID, DEPT_DEPARTMENT FRO" +
				"M FPASS_DEPARTMENT";
			this.oleDbSelectCommand3.Connection = this.oleDbConnection3;
			// 
			// oleDbUpdateCommand3
			// 
			this.oleDbUpdateCommand3.CommandText = "UPDATE FPASS_DEPARTMENT SET DEPT_CHANGEUSER = ?, DEPT_TIMESTAMP = ?, DEPT_ID = ?," +
				" DEPT_MND_ID = ?, DEPT_DEPARTMENT = ? WHERE (DEPT_ID = ?) AND (DEPT_CHANGEUSER =" +
				" ?) AND (DEPT_DEPARTMENT = ?) AND (DEPT_MND_ID = ?) AND (DEPT_TIMESTAMP = ?)";
			this.oleDbUpdateCommand3.Connection = this.oleDbConnection3;
			this.oleDbUpdateCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("DEPT_CHANGEUSER", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "DEPT_CHANGEUSER", System.Data.DataRowVersion.Current, null));
			this.oleDbUpdateCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("DEPT_TIMESTAMP", System.Data.OleDb.OleDbType.DBTimeStamp, 0, "DEPT_TIMESTAMP"));
			this.oleDbUpdateCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("DEPT_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "DEPT_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbUpdateCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("DEPT_MND_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "DEPT_MND_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbUpdateCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("DEPT_DEPARTMENT", System.Data.OleDb.OleDbType.VarChar, 35, "DEPT_DEPARTMENT"));
			this.oleDbUpdateCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_DEPT_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "DEPT_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_DEPT_CHANGEUSER", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "DEPT_CHANGEUSER", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_DEPT_DEPARTMENT", System.Data.OleDb.OleDbType.VarChar, 35, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "DEPT_DEPARTMENT", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_DEPT_MND_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "DEPT_MND_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_DEPT_TIMESTAMP", System.Data.OleDb.OleDbType.DBTimeStamp, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "DEPT_TIMESTAMP", System.Data.DataRowVersion.Original, null));
			// 
			// tempDAPrecMedType
			// 
			this.tempDAPrecMedType.DeleteCommand = this.oleDbDeleteCommand4;
			this.tempDAPrecMedType.InsertCommand = this.oleDbInsertCommand4;
			this.tempDAPrecMedType.SelectCommand = this.oleDbSelectCommand4;
			this.tempDAPrecMedType.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																										new System.Data.Common.DataTableMapping("Table", "FPASS_PRECMEDTYPE", new System.Data.Common.DataColumnMapping[] {
																																																							 new System.Data.Common.DataColumnMapping("PMTY_CHANGEUSER", "PMTY_CHANGEUSER"),
																																																							 new System.Data.Common.DataColumnMapping("PMTY_TIMESTAMP", "PMTY_TIMESTAMP"),
																																																							 new System.Data.Common.DataColumnMapping("PMTY_ID", "PMTY_ID"),
																																																							 new System.Data.Common.DataColumnMapping("PMTY_MND_ID", "PMTY_MND_ID"),
																																																							 new System.Data.Common.DataColumnMapping("PMTY_TYPE", "PMTY_TYPE"),
																																																							 new System.Data.Common.DataColumnMapping("PMTY_NOTATION", "PMTY_NOTATION"),
																																																							 new System.Data.Common.DataColumnMapping("PMTY_HELPFILE", "PMTY_HELPFILE")})});
			this.tempDAPrecMedType.UpdateCommand = this.oleDbUpdateCommand4;
			// 
			// oleDbDeleteCommand4
			// 
			this.oleDbDeleteCommand4.CommandText = "DELETE FROM FPASS_PRECMEDTYPE WHERE (PMTY_ID = ?) AND (PMTY_CHANGEUSER = ?) AND (" +
				"PMTY_HELPFILE = ? OR ? IS NULL AND PMTY_HELPFILE IS NULL) AND (PMTY_MND_ID = ?) " +
				"AND (PMTY_NOTATION = ?) AND (PMTY_TIMESTAMP = ?) AND (PMTY_TYPE = ?)";
			this.oleDbDeleteCommand4.Connection = this.oleDbConnection3;
			this.oleDbDeleteCommand4.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PMTY_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "PMTY_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand4.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PMTY_CHANGEUSER", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "PMTY_CHANGEUSER", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand4.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PMTY_HELPFILE", System.Data.OleDb.OleDbType.VarChar, 100, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "PMTY_HELPFILE", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand4.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PMTY_HELPFILE1", System.Data.OleDb.OleDbType.VarChar, 100, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "PMTY_HELPFILE", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand4.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PMTY_MND_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "PMTY_MND_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand4.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PMTY_NOTATION", System.Data.OleDb.OleDbType.VarChar, 100, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "PMTY_NOTATION", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand4.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PMTY_TIMESTAMP", System.Data.OleDb.OleDbType.DBTimeStamp, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "PMTY_TIMESTAMP", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand4.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PMTY_TYPE", System.Data.OleDb.OleDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "PMTY_TYPE", System.Data.DataRowVersion.Original, null));
			// 
			// oleDbInsertCommand4
			// 
			this.oleDbInsertCommand4.CommandText = "INSERT INTO FPASS_PRECMEDTYPE(PMTY_CHANGEUSER, PMTY_TIMESTAMP, PMTY_ID, PMTY_MND_" +
				"ID, PMTY_TYPE, PMTY_NOTATION, PMTY_HELPFILE) VALUES (?, ?, ?, ?, ?, ?, ?)";
			this.oleDbInsertCommand4.Connection = this.oleDbConnection3;
			this.oleDbInsertCommand4.Parameters.Add(new System.Data.OleDb.OleDbParameter("PMTY_CHANGEUSER", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "PMTY_CHANGEUSER", System.Data.DataRowVersion.Current, null));
			this.oleDbInsertCommand4.Parameters.Add(new System.Data.OleDb.OleDbParameter("PMTY_TIMESTAMP", System.Data.OleDb.OleDbType.DBTimeStamp, 0, "PMTY_TIMESTAMP"));
			this.oleDbInsertCommand4.Parameters.Add(new System.Data.OleDb.OleDbParameter("PMTY_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "PMTY_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbInsertCommand4.Parameters.Add(new System.Data.OleDb.OleDbParameter("PMTY_MND_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "PMTY_MND_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbInsertCommand4.Parameters.Add(new System.Data.OleDb.OleDbParameter("PMTY_TYPE", System.Data.OleDb.OleDbType.VarChar, 30, "PMTY_TYPE"));
			this.oleDbInsertCommand4.Parameters.Add(new System.Data.OleDb.OleDbParameter("PMTY_NOTATION", System.Data.OleDb.OleDbType.VarChar, 100, "PMTY_NOTATION"));
			this.oleDbInsertCommand4.Parameters.Add(new System.Data.OleDb.OleDbParameter("PMTY_HELPFILE", System.Data.OleDb.OleDbType.VarChar, 100, "PMTY_HELPFILE"));
			// 
			// oleDbSelectCommand4
			// 
			this.oleDbSelectCommand4.CommandText = "SELECT PMTY_CHANGEUSER, PMTY_TIMESTAMP, PMTY_ID, PMTY_MND_ID, PMTY_TYPE, PMTY_NOT" +
				"ATION, PMTY_HELPFILE FROM FPASS_PRECMEDTYPE";
			this.oleDbSelectCommand4.Connection = this.oleDbConnection3;
			// 
			// oleDbUpdateCommand4
			// 
			this.oleDbUpdateCommand4.CommandText = @"UPDATE FPASS_PRECMEDTYPE SET PMTY_CHANGEUSER = ?, PMTY_TIMESTAMP = ?, PMTY_ID = ?, PMTY_MND_ID = ?, PMTY_TYPE = ?, PMTY_NOTATION = ?, PMTY_HELPFILE = ? WHERE (PMTY_ID = ?) AND (PMTY_CHANGEUSER = ?) AND (PMTY_HELPFILE = ? OR ? IS NULL AND PMTY_HELPFILE IS NULL) AND (PMTY_MND_ID = ?) AND (PMTY_NOTATION = ?) AND (PMTY_TIMESTAMP = ?) AND (PMTY_TYPE = ?)";
			this.oleDbUpdateCommand4.Connection = this.oleDbConnection3;
			this.oleDbUpdateCommand4.Parameters.Add(new System.Data.OleDb.OleDbParameter("PMTY_CHANGEUSER", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "PMTY_CHANGEUSER", System.Data.DataRowVersion.Current, null));
			this.oleDbUpdateCommand4.Parameters.Add(new System.Data.OleDb.OleDbParameter("PMTY_TIMESTAMP", System.Data.OleDb.OleDbType.DBTimeStamp, 0, "PMTY_TIMESTAMP"));
			this.oleDbUpdateCommand4.Parameters.Add(new System.Data.OleDb.OleDbParameter("PMTY_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "PMTY_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbUpdateCommand4.Parameters.Add(new System.Data.OleDb.OleDbParameter("PMTY_MND_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "PMTY_MND_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbUpdateCommand4.Parameters.Add(new System.Data.OleDb.OleDbParameter("PMTY_TYPE", System.Data.OleDb.OleDbType.VarChar, 30, "PMTY_TYPE"));
			this.oleDbUpdateCommand4.Parameters.Add(new System.Data.OleDb.OleDbParameter("PMTY_NOTATION", System.Data.OleDb.OleDbType.VarChar, 100, "PMTY_NOTATION"));
			this.oleDbUpdateCommand4.Parameters.Add(new System.Data.OleDb.OleDbParameter("PMTY_HELPFILE", System.Data.OleDb.OleDbType.VarChar, 100, "PMTY_HELPFILE"));
			this.oleDbUpdateCommand4.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PMTY_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "PMTY_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand4.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PMTY_CHANGEUSER", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "PMTY_CHANGEUSER", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand4.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PMTY_HELPFILE", System.Data.OleDb.OleDbType.VarChar, 100, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "PMTY_HELPFILE", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand4.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PMTY_HELPFILE1", System.Data.OleDb.OleDbType.VarChar, 100, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "PMTY_HELPFILE", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand4.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PMTY_MND_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "PMTY_MND_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand4.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PMTY_NOTATION", System.Data.OleDb.OleDbType.VarChar, 100, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "PMTY_NOTATION", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand4.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PMTY_TIMESTAMP", System.Data.OleDb.OleDbType.DBTimeStamp, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "PMTY_TIMESTAMP", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand4.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PMTY_TYPE", System.Data.OleDb.OleDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "PMTY_TYPE", System.Data.DataRowVersion.Original, null));
			// 
			// dsPlant1
			// 
			this.dsPlant1.DataSetName = "DSPlant";
			this.dsPlant1.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// dsExContractor1
			// 
			this.dsExContractor1.DataSetName = "DSExContractor";
			this.dsExContractor1.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// dsDepartment1
			// 
			this.dsDepartment1.DataSetName = "DSDepartment";
			this.dsDepartment1.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// dsPrecMedType1
			// 
			this.dsPrecMedType1.DataSetName = "DSPrecMedType";
			this.dsPrecMedType1.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// tempDACraft
			// 
			this.tempDACraft.DeleteCommand = this.oleDbDeleteCommand5;
			this.tempDACraft.InsertCommand = this.oleDbInsertCommand5;
			this.tempDACraft.SelectCommand = this.oleDbSelectCommand5;
			this.tempDACraft.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																								  new System.Data.Common.DataTableMapping("Table", "FPASS_CRAFT", new System.Data.Common.DataColumnMapping[] {
																																																				 new System.Data.Common.DataColumnMapping("CRA_CHANGEUSER", "CRA_CHANGEUSER"),
																																																				 new System.Data.Common.DataColumnMapping("CRA_TIMESTAMP", "CRA_TIMESTAMP"),
																																																				 new System.Data.Common.DataColumnMapping("CRA_ID", "CRA_ID"),
																																																				 new System.Data.Common.DataColumnMapping("CRA_MND_ID", "CRA_MND_ID"),
																																																				 new System.Data.Common.DataColumnMapping("CRA_CRAFTNO", "CRA_CRAFTNO"),
																																																				 new System.Data.Common.DataColumnMapping("CRA_CRAFTNOTATION", "CRA_CRAFTNOTATION")})});
			this.tempDACraft.UpdateCommand = this.oleDbUpdateCommand5;
			// 
			// oleDbDeleteCommand5
			// 
			this.oleDbDeleteCommand5.CommandText = "DELETE FROM FPASS_CRAFT WHERE (CRA_ID = ?) AND (CRA_CHANGEUSER = ?) AND (CRA_CRAF" +
				"TNO = ?) AND (CRA_CRAFTNOTATION = ?) AND (CRA_MND_ID = ?) AND (CRA_TIMESTAMP = ?" +
				")";
			this.oleDbDeleteCommand5.Connection = this.oleDbConnection2;
			this.oleDbDeleteCommand5.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CRA_ID", System.Data.OleDb.OleDbType.Double, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CRA_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand5.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CRA_CHANGEUSER", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CRA_CHANGEUSER", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand5.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CRA_CRAFTNO", System.Data.OleDb.OleDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CRA_CRAFTNO", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand5.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CRA_CRAFTNOTATION", System.Data.OleDb.OleDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CRA_CRAFTNOTATION", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand5.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CRA_MND_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CRA_MND_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand5.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CRA_TIMESTAMP", System.Data.OleDb.OleDbType.DBTimeStamp, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CRA_TIMESTAMP", System.Data.DataRowVersion.Original, null));
			// 
			// oleDbInsertCommand5
			// 
			this.oleDbInsertCommand5.CommandText = "INSERT INTO FPASS_CRAFT(CRA_CHANGEUSER, CRA_TIMESTAMP, CRA_ID, CRA_MND_ID, CRA_CR" +
				"AFTNO, CRA_CRAFTNOTATION) VALUES (?, ?, ?, ?, ?, ?)";
			this.oleDbInsertCommand5.Connection = this.oleDbConnection2;
			this.oleDbInsertCommand5.Parameters.Add(new System.Data.OleDb.OleDbParameter("CRA_CHANGEUSER", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CRA_CHANGEUSER", System.Data.DataRowVersion.Current, null));
			this.oleDbInsertCommand5.Parameters.Add(new System.Data.OleDb.OleDbParameter("CRA_TIMESTAMP", System.Data.OleDb.OleDbType.DBTimeStamp, 5472, "CRA_TIMESTAMP"));
			this.oleDbInsertCommand5.Parameters.Add(new System.Data.OleDb.OleDbParameter("CRA_ID", System.Data.OleDb.OleDbType.Double, 5472, "CRA_ID"));
			this.oleDbInsertCommand5.Parameters.Add(new System.Data.OleDb.OleDbParameter("CRA_MND_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CRA_MND_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbInsertCommand5.Parameters.Add(new System.Data.OleDb.OleDbParameter("CRA_CRAFTNO", System.Data.OleDb.OleDbType.VarChar, 30, "CRA_CRAFTNO"));
			this.oleDbInsertCommand5.Parameters.Add(new System.Data.OleDb.OleDbParameter("CRA_CRAFTNOTATION", System.Data.OleDb.OleDbType.VarChar, 30, "CRA_CRAFTNOTATION"));
			// 
			// oleDbSelectCommand5
			// 
			this.oleDbSelectCommand5.CommandText = "SELECT CRA_CHANGEUSER, CRA_TIMESTAMP, CRA_ID, CRA_MND_ID, CRA_CRAFTNO, CRA_CRAFTN" +
				"OTATION FROM FPASS_CRAFT";
			this.oleDbSelectCommand5.Connection = this.oleDbConnection2;
			// 
			// oleDbUpdateCommand5
			// 
			this.oleDbUpdateCommand5.CommandText = @"UPDATE FPASS_CRAFT SET CRA_CHANGEUSER = ?, CRA_TIMESTAMP = ?, CRA_ID = ?, CRA_MND_ID = ?, CRA_CRAFTNO = ?, CRA_CRAFTNOTATION = ? WHERE (CRA_ID = ?) AND (CRA_CHANGEUSER = ?) AND (CRA_CRAFTNO = ?) AND (CRA_CRAFTNOTATION = ?) AND (CRA_MND_ID = ?) AND (CRA_TIMESTAMP = ?)";
			this.oleDbUpdateCommand5.Connection = this.oleDbConnection2;
			this.oleDbUpdateCommand5.Parameters.Add(new System.Data.OleDb.OleDbParameter("CRA_CHANGEUSER", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CRA_CHANGEUSER", System.Data.DataRowVersion.Current, null));
			this.oleDbUpdateCommand5.Parameters.Add(new System.Data.OleDb.OleDbParameter("CRA_TIMESTAMP", System.Data.OleDb.OleDbType.DBTimeStamp, 5472, "CRA_TIMESTAMP"));
			this.oleDbUpdateCommand5.Parameters.Add(new System.Data.OleDb.OleDbParameter("CRA_ID", System.Data.OleDb.OleDbType.Double, 5472, "CRA_ID"));
			this.oleDbUpdateCommand5.Parameters.Add(new System.Data.OleDb.OleDbParameter("CRA_MND_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CRA_MND_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbUpdateCommand5.Parameters.Add(new System.Data.OleDb.OleDbParameter("CRA_CRAFTNO", System.Data.OleDb.OleDbType.VarChar, 30, "CRA_CRAFTNO"));
			this.oleDbUpdateCommand5.Parameters.Add(new System.Data.OleDb.OleDbParameter("CRA_CRAFTNOTATION", System.Data.OleDb.OleDbType.VarChar, 30, "CRA_CRAFTNOTATION"));
			this.oleDbUpdateCommand5.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CRA_ID", System.Data.OleDb.OleDbType.Double, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CRA_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand5.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CRA_CHANGEUSER", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CRA_CHANGEUSER", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand5.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CRA_CRAFTNO", System.Data.OleDb.OleDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CRA_CRAFTNO", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand5.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CRA_CRAFTNOTATION", System.Data.OleDb.OleDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CRA_CRAFTNOTATION", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand5.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CRA_MND_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CRA_MND_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand5.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CRA_TIMESTAMP", System.Data.OleDb.OleDbType.DBTimeStamp, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CRA_TIMESTAMP", System.Data.DataRowVersion.Original, null));
			// 
			// dsCraft1
			// 
			this.dsCraft1.DataSetName = "DSCraft";
			this.dsCraft1.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// tempDAExcoCoord
			// 
			this.tempDAExcoCoord.DeleteCommand = this.oleDbDeleteCommand7;
			this.tempDAExcoCoord.InsertCommand = this.oleDbInsertCommand6;
			this.tempDAExcoCoord.SelectCommand = this.oleDbSelectCommand6;
			this.tempDAExcoCoord.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																									  new System.Data.Common.DataTableMapping("Table", "FPASS_EXCOECOD", new System.Data.Common.DataColumnMapping[] {
																																																						new System.Data.Common.DataColumnMapping("ECEC_CHANGEUSER", "ECEC_CHANGEUSER"),
																																																						new System.Data.Common.DataColumnMapping("ECEC_TIMESTAMP", "ECEC_TIMESTAMP"),
																																																						new System.Data.Common.DataColumnMapping("ECEC_ECOD_ID", "ECEC_ECOD_ID"),
																																																						new System.Data.Common.DataColumnMapping("ECEC_EXCO_ID", "ECEC_EXCO_ID")})});
			this.tempDAExcoCoord.UpdateCommand = this.oleDbUpdateCommand7;
			// 
			// oleDbDeleteCommand7
			// 
			this.oleDbDeleteCommand7.CommandText = "DELETE FROM FPASS_EXCOECOD WHERE (ECEC_ECOD_ID = ?) AND (ECEC_EXCO_ID = ?) AND (E" +
				"CEC_CHANGEUSER = ?) AND (ECEC_TIMESTAMP = ?)";
			this.oleDbDeleteCommand7.Connection = this.oleDbConnection3;
			this.oleDbDeleteCommand7.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_ECEC_ECOD_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "ECEC_ECOD_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand7.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_ECEC_EXCO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "ECEC_EXCO_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand7.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_ECEC_CHANGEUSER", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "ECEC_CHANGEUSER", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand7.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_ECEC_TIMESTAMP", System.Data.OleDb.OleDbType.DBTimeStamp, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ECEC_TIMESTAMP", System.Data.DataRowVersion.Original, null));
			// 
			// oleDbInsertCommand6
			// 
			this.oleDbInsertCommand6.CommandText = "INSERT INTO FPASS_EXCOECOD(ECEC_CHANGEUSER, ECEC_TIMESTAMP, ECEC_ECOD_ID, ECEC_EX" +
				"CO_ID) VALUES (?, ?, ?, ?)";
			this.oleDbInsertCommand6.Connection = this.oleDbConnection3;
			this.oleDbInsertCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("ECEC_CHANGEUSER", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "ECEC_CHANGEUSER", System.Data.DataRowVersion.Current, null));
			this.oleDbInsertCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("ECEC_TIMESTAMP", System.Data.OleDb.OleDbType.DBTimeStamp, 0, "ECEC_TIMESTAMP"));
			this.oleDbInsertCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("ECEC_ECOD_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "ECEC_ECOD_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbInsertCommand6.Parameters.Add(new System.Data.OleDb.OleDbParameter("ECEC_EXCO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "ECEC_EXCO_ID", System.Data.DataRowVersion.Current, null));
			// 
			// oleDbSelectCommand6
			// 
			this.oleDbSelectCommand6.CommandText = "SELECT ECEC_CHANGEUSER, ECEC_TIMESTAMP, ECEC_ECOD_ID, ECEC_EXCO_ID FROM FPASS_EXC" +
				"OECOD";
			this.oleDbSelectCommand6.Connection = this.oleDbConnection3;
			// 
			// oleDbUpdateCommand7
			// 
			this.oleDbUpdateCommand7.CommandText = "UPDATE FPASS_EXCOECOD SET ECEC_CHANGEUSER = ?, ECEC_TIMESTAMP = ?, ECEC_ECOD_ID =" +
				" ?, ECEC_EXCO_ID = ? WHERE (ECEC_ECOD_ID = ?) AND (ECEC_EXCO_ID = ?) AND (ECEC_C" +
				"HANGEUSER = ?) AND (ECEC_TIMESTAMP = ?)";
			this.oleDbUpdateCommand7.Connection = this.oleDbConnection3;
			this.oleDbUpdateCommand7.Parameters.Add(new System.Data.OleDb.OleDbParameter("ECEC_CHANGEUSER", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "ECEC_CHANGEUSER", System.Data.DataRowVersion.Current, null));
			this.oleDbUpdateCommand7.Parameters.Add(new System.Data.OleDb.OleDbParameter("ECEC_TIMESTAMP", System.Data.OleDb.OleDbType.DBTimeStamp, 0, "ECEC_TIMESTAMP"));
			this.oleDbUpdateCommand7.Parameters.Add(new System.Data.OleDb.OleDbParameter("ECEC_ECOD_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "ECEC_ECOD_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbUpdateCommand7.Parameters.Add(new System.Data.OleDb.OleDbParameter("ECEC_EXCO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "ECEC_EXCO_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbUpdateCommand7.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_ECEC_ECOD_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "ECEC_ECOD_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand7.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_ECEC_EXCO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "ECEC_EXCO_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand7.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_ECEC_CHANGEUSER", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "ECEC_CHANGEUSER", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand7.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_ECEC_TIMESTAMP", System.Data.OleDb.OleDbType.DBTimeStamp, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ECEC_TIMESTAMP", System.Data.DataRowVersion.Original, null));
			// 
			// dsExcoCoord1
			// 
			this.dsExcoCoord1.DataSetName = "DSExcoCoord";
			this.dsExcoCoord1.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// tempDAUserByRole
			// 
			this.tempDAUserByRole.InsertCommand = this.oracleInsertCommand1;
			this.tempDAUserByRole.SelectCommand = this.oracleSelectCommand1;
			this.tempDAUserByRole.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																									   new System.Data.Common.DataTableMapping("Table", "VW_FPASS_USERBYROLE", new System.Data.Common.DataColumnMapping[] {
																																																							  new System.Data.Common.DataColumnMapping("UM_USER_ID", "UM_USER_ID"),
																																																							  new System.Data.Common.DataColumnMapping("FPASS_USER_ID", "FPASS_USER_ID"),
																																																							  new System.Data.Common.DataColumnMapping("BOTHNAMESTEL", "BOTHNAMESTEL"),
																																																							  new System.Data.Common.DataColumnMapping("UM_USERAPPLID", "UM_USERAPPLID")})});
			// 
			// oracleConnection1
			// 
			this.oracleConnection1.ConnectionString = "user id=fpassV1;data source=KOELN8I;persist security info=False";
			// 
			// dsUser1
			// 
			this.dsUser1.DataSetName = "DSUser";
			this.dsUser1.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// tempDARoleByUser
			// 
			this.tempDARoleByUser.DeleteCommand = this.oracleDeleteCommand1;
			this.tempDARoleByUser.InsertCommand = this.oracleInsertCommand2;
			this.tempDARoleByUser.SelectCommand = this.oracleSelectCommand2;
			this.tempDARoleByUser.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																									   new System.Data.Common.DataTableMapping("Table", "UM_ROLELINK", new System.Data.Common.DataColumnMapping[] {
																																																					  new System.Data.Common.DataColumnMapping("RL_ROLEID", "RL_ROLEID"),
																																																					  new System.Data.Common.DataColumnMapping("RL_AUTHORIZEDENTITYID", "RL_AUTHORIZEDENTITYID"),
																																																					  new System.Data.Common.DataColumnMapping("RL_CHANGEUSER", "RL_CHANGEUSER"),
																																																					  new System.Data.Common.DataColumnMapping("RL_TIMESTAMP", "RL_TIMESTAMP")})});
			this.tempDARoleByUser.UpdateCommand = this.oracleUpdateCommand1;
			// 
			// oracleDeleteCommand1
			// 
			this.oracleDeleteCommand1.CommandText = "DELETE FROM UM_ROLELINK WHERE (RL_AUTHORIZEDENTITYID = :Original_RL_AUTHORIZEDENT" +
				"ITYID) AND (RL_ROLEID = :Original_RL_ROLEID) AND (RL_CHANGEUSER = :Original_RL_C" +
				"HANGEUSER) AND (RL_TIMESTAMP = :Original_RL_TIMESTAMP)";
			this.oracleDeleteCommand1.Connection = this.oracleConnection1;
			this.oracleDeleteCommand1.Parameters.Add(new System.Data.OracleClient.OracleParameter(":Original_RL_AUTHORIZEDENTITYID", System.Data.OracleClient.OracleType.Number, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "RL_AUTHORIZEDENTITYID", System.Data.DataRowVersion.Original, null));
			this.oracleDeleteCommand1.Parameters.Add(new System.Data.OracleClient.OracleParameter(":Original_RL_ROLEID", System.Data.OracleClient.OracleType.Number, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "RL_ROLEID", System.Data.DataRowVersion.Original, null));
			this.oracleDeleteCommand1.Parameters.Add(new System.Data.OracleClient.OracleParameter(":Original_RL_CHANGEUSER", System.Data.OracleClient.OracleType.Number, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "RL_CHANGEUSER", System.Data.DataRowVersion.Original, null));
			this.oracleDeleteCommand1.Parameters.Add(new System.Data.OracleClient.OracleParameter(":Original_RL_TIMESTAMP", System.Data.OracleClient.OracleType.DateTime, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "RL_TIMESTAMP", System.Data.DataRowVersion.Original, null));
			// 
			// oracleInsertCommand2
			// 
			this.oracleInsertCommand2.CommandText = "INSERT INTO UM_ROLELINK(RL_ROLEID, RL_AUTHORIZEDENTITYID, RL_CHANGEUSER, RL_TIMES" +
				"TAMP) VALUES (:RL_ROLEID, :RL_AUTHORIZEDENTITYID, :RL_CHANGEUSER, :RL_TIMESTAMP)" +
				"";
			this.oracleInsertCommand2.Connection = this.oracleConnection1;
			this.oracleInsertCommand2.Parameters.Add(new System.Data.OracleClient.OracleParameter(":RL_ROLEID", System.Data.OracleClient.OracleType.Number, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "RL_ROLEID", System.Data.DataRowVersion.Current, null));
			this.oracleInsertCommand2.Parameters.Add(new System.Data.OracleClient.OracleParameter(":RL_AUTHORIZEDENTITYID", System.Data.OracleClient.OracleType.Number, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "RL_AUTHORIZEDENTITYID", System.Data.DataRowVersion.Current, null));
			this.oracleInsertCommand2.Parameters.Add(new System.Data.OracleClient.OracleParameter(":RL_CHANGEUSER", System.Data.OracleClient.OracleType.Number, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "RL_CHANGEUSER", System.Data.DataRowVersion.Current, null));
			this.oracleInsertCommand2.Parameters.Add(new System.Data.OracleClient.OracleParameter(":RL_TIMESTAMP", System.Data.OracleClient.OracleType.DateTime, 0, "RL_TIMESTAMP"));
			// 
			// oracleSelectCommand2
			// 
			this.oracleSelectCommand2.CommandText = "SELECT RL_ROLEID, RL_AUTHORIZEDENTITYID, RL_CHANGEUSER, RL_TIMESTAMP FROM UM_ROLE" +
				"LINK";
			this.oracleSelectCommand2.Connection = this.oracleConnection1;
			// 
			// oracleUpdateCommand1
			// 
			this.oracleUpdateCommand1.CommandText = @"UPDATE UM_ROLELINK SET RL_ROLEID = :RL_ROLEID, RL_AUTHORIZEDENTITYID = :RL_AUTHORIZEDENTITYID, RL_CHANGEUSER = :RL_CHANGEUSER, RL_TIMESTAMP = :RL_TIMESTAMP WHERE (RL_AUTHORIZEDENTITYID = :Original_RL_AUTHORIZEDENTITYID) AND (RL_ROLEID = :Original_RL_ROLEID) AND (RL_CHANGEUSER = :Original_RL_CHANGEUSER) AND (RL_TIMESTAMP = :Original_RL_TIMESTAMP)";
			this.oracleUpdateCommand1.Connection = this.oracleConnection1;
			this.oracleUpdateCommand1.Parameters.Add(new System.Data.OracleClient.OracleParameter(":RL_ROLEID", System.Data.OracleClient.OracleType.Number, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "RL_ROLEID", System.Data.DataRowVersion.Current, null));
			this.oracleUpdateCommand1.Parameters.Add(new System.Data.OracleClient.OracleParameter(":RL_AUTHORIZEDENTITYID", System.Data.OracleClient.OracleType.Number, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "RL_AUTHORIZEDENTITYID", System.Data.DataRowVersion.Current, null));
			this.oracleUpdateCommand1.Parameters.Add(new System.Data.OracleClient.OracleParameter(":RL_CHANGEUSER", System.Data.OracleClient.OracleType.Number, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "RL_CHANGEUSER", System.Data.DataRowVersion.Current, null));
			this.oracleUpdateCommand1.Parameters.Add(new System.Data.OracleClient.OracleParameter(":RL_TIMESTAMP", System.Data.OracleClient.OracleType.DateTime, 0, "RL_TIMESTAMP"));
			this.oracleUpdateCommand1.Parameters.Add(new System.Data.OracleClient.OracleParameter(":Original_RL_AUTHORIZEDENTITYID", System.Data.OracleClient.OracleType.Number, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "RL_AUTHORIZEDENTITYID", System.Data.DataRowVersion.Original, null));
			this.oracleUpdateCommand1.Parameters.Add(new System.Data.OracleClient.OracleParameter(":Original_RL_ROLEID", System.Data.OracleClient.OracleType.Number, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "RL_ROLEID", System.Data.DataRowVersion.Original, null));
			this.oracleUpdateCommand1.Parameters.Add(new System.Data.OracleClient.OracleParameter(":Original_RL_CHANGEUSER", System.Data.OracleClient.OracleType.Number, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "RL_CHANGEUSER", System.Data.DataRowVersion.Original, null));
			this.oracleUpdateCommand1.Parameters.Add(new System.Data.OracleClient.OracleParameter(":Original_RL_TIMESTAMP", System.Data.OracleClient.OracleType.DateTime, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "RL_TIMESTAMP", System.Data.DataRowVersion.Original, null));
			// 
			// dsRole1
			// 
			this.dsRole1.DataSetName = "DSRole";
			this.dsRole1.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// tempDAHistory
			// 
			this.tempDAHistory.InsertCommand = this.oleDbInsertCommand8;
			this.tempDAHistory.SelectCommand = this.oleDbSelectCommand8;
			this.tempDAHistory.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																									new System.Data.Common.DataTableMapping("Table", "FPASS_HIST", new System.Data.Common.DataColumnMapping[] {
																																																				  new System.Data.Common.DataColumnMapping("HIST_ID", "HIST_ID"),
																																																				  new System.Data.Common.DataColumnMapping("HIST_USER_ID", "HIST_USER_ID"),
																																																				  new System.Data.Common.DataColumnMapping("HIST_CHANGEDATE", "HIST_CHANGEDATE"),
																																																				  new System.Data.Common.DataColumnMapping("HIST_TABLENAME", "HIST_TABLENAME"),
																																																				  new System.Data.Common.DataColumnMapping("HIST_COLUMNNAME", "HIST_COLUMNNAME"),
																																																				  new System.Data.Common.DataColumnMapping("HIST_ROWID", "HIST_ROWID"),
																																																				  new System.Data.Common.DataColumnMapping("HIST_OLDVALUE", "HIST_OLDVALUE"),
																																																				  new System.Data.Common.DataColumnMapping("HIST_NEWVALUE", "HIST_NEWVALUE"),
																																																				  new System.Data.Common.DataColumnMapping("HIST_DESCRIPTION", "HIST_DESCRIPTION"),
																																																				  new System.Data.Common.DataColumnMapping("ROWID", "ROWID")})});
			// 
			// oleDbInsertCommand8
			// 
			this.oleDbInsertCommand8.CommandText = "INSERT INTO FPASS_HIST(HIST_ID, HIST_USER_ID, HIST_CHANGEDATE, HIST_TABLENAME, HI" +
				"ST_COLUMNNAME, HIST_ROWID, HIST_OLDVALUE, HIST_NEWVALUE, HIST_DESCRIPTION) VALUE" +
				"S (?, ?, ?, ?, ?, ?, ?, ?, ?)";
			this.oleDbInsertCommand8.Connection = this.oleDbConnection2;
			this.oleDbInsertCommand8.Parameters.Add(new System.Data.OleDb.OleDbParameter("HIST_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "HIST_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbInsertCommand8.Parameters.Add(new System.Data.OleDb.OleDbParameter("HIST_USER_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "HIST_USER_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbInsertCommand8.Parameters.Add(new System.Data.OleDb.OleDbParameter("HIST_CHANGEDATE", System.Data.OleDb.OleDbType.DBTimeStamp, 5472, "HIST_CHANGEDATE"));
			this.oleDbInsertCommand8.Parameters.Add(new System.Data.OleDb.OleDbParameter("HIST_TABLENAME", System.Data.OleDb.OleDbType.VarChar, 30, "HIST_TABLENAME"));
			this.oleDbInsertCommand8.Parameters.Add(new System.Data.OleDb.OleDbParameter("HIST_COLUMNNAME", System.Data.OleDb.OleDbType.VarChar, 30, "HIST_COLUMNNAME"));
			this.oleDbInsertCommand8.Parameters.Add(new System.Data.OleDb.OleDbParameter("HIST_ROWID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "HIST_ROWID", System.Data.DataRowVersion.Current, null));
			this.oleDbInsertCommand8.Parameters.Add(new System.Data.OleDb.OleDbParameter("HIST_OLDVALUE", System.Data.OleDb.OleDbType.VarChar, 100, "HIST_OLDVALUE"));
			this.oleDbInsertCommand8.Parameters.Add(new System.Data.OleDb.OleDbParameter("HIST_NEWVALUE", System.Data.OleDb.OleDbType.VarChar, 100, "HIST_NEWVALUE"));
			this.oleDbInsertCommand8.Parameters.Add(new System.Data.OleDb.OleDbParameter("HIST_DESCRIPTION", System.Data.OleDb.OleDbType.VarChar, 100, "HIST_DESCRIPTION"));
			// 
			// oleDbSelectCommand8
			// 
			this.oleDbSelectCommand8.CommandText = "SELECT HIST_ID, HIST_USER_ID, HIST_CHANGEDATE, HIST_TABLENAME, HIST_COLUMNNAME, H" +
				"IST_ROWID, HIST_OLDVALUE, HIST_NEWVALUE, HIST_DESCRIPTION, ROWID FROM FPASS_HIST" +
				"";
			this.oleDbSelectCommand8.Connection = this.oleDbConnection2;
			// 
			// dsHistory1
			// 
			this.dsHistory1.DataSetName = "DSHistory";
			this.dsHistory1.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// tempDADUMMY
			// 
			this.tempDADUMMY.DeleteCommand = this.oracleDeleteCommand2;
			this.tempDADUMMY.InsertCommand = this.oracleInsertCommand3;
			this.tempDADUMMY.SelectCommand = this.oracleSelectCommand3;
			this.tempDADUMMY.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																								  new System.Data.Common.DataTableMapping("Table", "FPASS_DUMMY", new System.Data.Common.DataColumnMapping[] {
																																																				 new System.Data.Common.DataColumnMapping("DUMMY_ID", "DUMMY_ID"),
																																																				 new System.Data.Common.DataColumnMapping("SURNAME", "SURNAME"),
																																																				 new System.Data.Common.DataColumnMapping("FIRSTNAME", "FIRSTNAME"),
																																																				 new System.Data.Common.DataColumnMapping("COORD_ID", "COORD_ID"),
																																																				 new System.Data.Common.DataColumnMapping("CONTRACTOR_ID", "CONTRACTOR_ID")})});
			this.tempDADUMMY.UpdateCommand = this.oracleUpdateCommand2;
			// 
			// oracleDeleteCommand2
			// 
			this.oracleDeleteCommand2.CommandText = @"DELETE FROM FPASS_DUMMY WHERE (DUMMY_ID = :Original_DUMMY_ID) AND (CONTRACTOR_ID = :Original_CONTRACTOR_ID) AND (COORD_ID = :Original_COORD_ID OR :Original_COORD_ID IS NULL AND COORD_ID IS NULL) AND (FIRSTNAME = :Original_FIRSTNAME OR :Original_FIRSTNAME IS NULL AND FIRSTNAME IS NULL) AND (SURNAME = :Original_SURNAME)";
			this.oracleDeleteCommand2.Connection = this.oracleConnection1;
			this.oracleDeleteCommand2.Parameters.Add(new System.Data.OracleClient.OracleParameter(":Original_DUMMY_ID", System.Data.OracleClient.OracleType.Number, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "DUMMY_ID", System.Data.DataRowVersion.Original, null));
			this.oracleDeleteCommand2.Parameters.Add(new System.Data.OracleClient.OracleParameter(":Original_CONTRACTOR_ID", System.Data.OracleClient.OracleType.Number, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CONTRACTOR_ID", System.Data.DataRowVersion.Original, null));
			this.oracleDeleteCommand2.Parameters.Add(new System.Data.OracleClient.OracleParameter(":Original_COORD_ID", System.Data.OracleClient.OracleType.Number, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "COORD_ID", System.Data.DataRowVersion.Original, null));
			this.oracleDeleteCommand2.Parameters.Add(new System.Data.OracleClient.OracleParameter(":Original_FIRSTNAME", System.Data.OracleClient.OracleType.VarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "FIRSTNAME", System.Data.DataRowVersion.Original, null));
			this.oracleDeleteCommand2.Parameters.Add(new System.Data.OracleClient.OracleParameter(":Original_SURNAME", System.Data.OracleClient.OracleType.VarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "SURNAME", System.Data.DataRowVersion.Original, null));
			// 
			// oracleInsertCommand3
			// 
			this.oracleInsertCommand3.CommandText = "INSERT INTO FPASS_DUMMY(DUMMY_ID, SURNAME, FIRSTNAME, COORD_ID, CONTRACTOR_ID) VA" +
				"LUES (:DUMMY_ID, :SURNAME, :FIRSTNAME, :COORD_ID, :CONTRACTOR_ID)";
			this.oracleInsertCommand3.Connection = this.oracleConnection1;
			this.oracleInsertCommand3.Parameters.Add(new System.Data.OracleClient.OracleParameter(":DUMMY_ID", System.Data.OracleClient.OracleType.Number, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "DUMMY_ID", System.Data.DataRowVersion.Current, null));
			this.oracleInsertCommand3.Parameters.Add(new System.Data.OracleClient.OracleParameter(":SURNAME", System.Data.OracleClient.OracleType.VarChar, 50, "SURNAME"));
			this.oracleInsertCommand3.Parameters.Add(new System.Data.OracleClient.OracleParameter(":FIRSTNAME", System.Data.OracleClient.OracleType.VarChar, 50, "FIRSTNAME"));
			this.oracleInsertCommand3.Parameters.Add(new System.Data.OracleClient.OracleParameter(":COORD_ID", System.Data.OracleClient.OracleType.Number, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "COORD_ID", System.Data.DataRowVersion.Current, null));
			this.oracleInsertCommand3.Parameters.Add(new System.Data.OracleClient.OracleParameter(":CONTRACTOR_ID", System.Data.OracleClient.OracleType.Number, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CONTRACTOR_ID", System.Data.DataRowVersion.Current, null));
			// 
			// oracleSelectCommand3
			// 
			this.oracleSelectCommand3.CommandText = "SELECT DUMMY_ID, SURNAME, FIRSTNAME, COORD_ID, CONTRACTOR_ID FROM FPASS_DUMMY";
			this.oracleSelectCommand3.Connection = this.oracleConnection1;
			// 
			// oracleUpdateCommand2
			// 
			this.oracleUpdateCommand2.CommandText = @"UPDATE FPASS_DUMMY SET DUMMY_ID = :DUMMY_ID, SURNAME = :SURNAME, FIRSTNAME = :FIRSTNAME, COORD_ID = :COORD_ID, CONTRACTOR_ID = :CONTRACTOR_ID WHERE (DUMMY_ID = :Original_DUMMY_ID) AND (CONTRACTOR_ID = :Original_CONTRACTOR_ID) AND (COORD_ID = :Original_COORD_ID OR :Original_COORD_ID IS NULL AND COORD_ID IS NULL) AND (FIRSTNAME = :Original_FIRSTNAME OR :Original_FIRSTNAME IS NULL AND FIRSTNAME IS NULL) AND (SURNAME = :Original_SURNAME)";
			this.oracleUpdateCommand2.Connection = this.oracleConnection1;
			this.oracleUpdateCommand2.Parameters.Add(new System.Data.OracleClient.OracleParameter(":DUMMY_ID", System.Data.OracleClient.OracleType.Number, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "DUMMY_ID", System.Data.DataRowVersion.Current, null));
			this.oracleUpdateCommand2.Parameters.Add(new System.Data.OracleClient.OracleParameter(":SURNAME", System.Data.OracleClient.OracleType.VarChar, 50, "SURNAME"));
			this.oracleUpdateCommand2.Parameters.Add(new System.Data.OracleClient.OracleParameter(":FIRSTNAME", System.Data.OracleClient.OracleType.VarChar, 50, "FIRSTNAME"));
			this.oracleUpdateCommand2.Parameters.Add(new System.Data.OracleClient.OracleParameter(":COORD_ID", System.Data.OracleClient.OracleType.Number, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "COORD_ID", System.Data.DataRowVersion.Current, null));
			this.oracleUpdateCommand2.Parameters.Add(new System.Data.OracleClient.OracleParameter(":CONTRACTOR_ID", System.Data.OracleClient.OracleType.Number, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CONTRACTOR_ID", System.Data.DataRowVersion.Current, null));
			this.oracleUpdateCommand2.Parameters.Add(new System.Data.OracleClient.OracleParameter(":Original_DUMMY_ID", System.Data.OracleClient.OracleType.Number, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "DUMMY_ID", System.Data.DataRowVersion.Original, null));
			this.oracleUpdateCommand2.Parameters.Add(new System.Data.OracleClient.OracleParameter(":Original_CONTRACTOR_ID", System.Data.OracleClient.OracleType.Number, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "CONTRACTOR_ID", System.Data.DataRowVersion.Original, null));
			this.oracleUpdateCommand2.Parameters.Add(new System.Data.OracleClient.OracleParameter(":Original_COORD_ID", System.Data.OracleClient.OracleType.Number, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "COORD_ID", System.Data.DataRowVersion.Original, null));
			this.oracleUpdateCommand2.Parameters.Add(new System.Data.OracleClient.OracleParameter(":Original_FIRSTNAME", System.Data.OracleClient.OracleType.VarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "FIRSTNAME", System.Data.DataRowVersion.Original, null));
			this.oracleUpdateCommand2.Parameters.Add(new System.Data.OracleClient.OracleParameter(":Original_SURNAME", System.Data.OracleClient.OracleType.VarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "SURNAME", System.Data.DataRowVersion.Original, null));
			// 
			// dsDummy2
			// 
			this.dsDummy2.DataSetName = "DSDummy";
			this.dsDummy2.Locale = new System.Globalization.CultureInfo("de-DE");
			// 
			// tempDAReceptionAuthorize
			// 
			this.tempDAReceptionAuthorize.DeleteCommand = this.oleDbDeleteCommand8;
			this.tempDAReceptionAuthorize.InsertCommand = this.oleDbInsertCommand9;
			this.tempDAReceptionAuthorize.SelectCommand = this.oleDbSelectCommand9;
			this.tempDAReceptionAuthorize.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																											   new System.Data.Common.DataTableMapping("Table", "FPASS_RECEPTIONAUTHORIZE", new System.Data.Common.DataColumnMapping[] {
																																																										   new System.Data.Common.DataColumnMapping("RATH_ID", "RATH_ID"),
																																																										   new System.Data.Common.DataColumnMapping("RATH_CWR_ID", "RATH_CWR_ID"),
																																																										   new System.Data.Common.DataColumnMapping("RATH_RATT_ID", "RATH_RATT_ID"),
																																																										   new System.Data.Common.DataColumnMapping("RATH_USER_ID", "RATH_USER_ID"),
																																																										   new System.Data.Common.DataColumnMapping("RATH_RECEPTAUTHO_YN", "RATH_RECEPTAUTHO_YN"),
																																																										   new System.Data.Common.DataColumnMapping("RATH_RECEPTAUTHODATE", "RATH_RECEPTAUTHODATE"),
																																																										   new System.Data.Common.DataColumnMapping("RATH_TIMESTAMP", "RATH_TIMESTAMP")})});
			this.tempDAReceptionAuthorize.UpdateCommand = this.oleDbUpdateCommand8;
			// 
			// oleDbDeleteCommand8
			// 
			this.oleDbDeleteCommand8.CommandText = @"DELETE FROM FPASS_RECEPTIONAUTHORIZE WHERE (RATH_ID = ?) AND (RATH_CWR_ID = ?) AND (RATH_RATT_ID = ?) AND (RATH_RECEPTAUTHODATE = ? OR ? IS NULL AND RATH_RECEPTAUTHODATE IS NULL) AND (RATH_RECEPTAUTHO_YN = ?) AND (RATH_TIMESTAMP = ?) AND (RATH_USER_ID = ?)";
			this.oleDbDeleteCommand8.Connection = this.oleDbConnection2;
			this.oleDbDeleteCommand8.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_RATH_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "RATH_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand8.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_RATH_CWR_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "RATH_CWR_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand8.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_RATH_RATT_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "RATH_RATT_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand8.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_RATH_RECEPTAUTHODATE", System.Data.OleDb.OleDbType.DBTimeStamp, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "RATH_RECEPTAUTHODATE", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand8.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_RATH_RECEPTAUTHODATE1", System.Data.OleDb.OleDbType.DBTimeStamp, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "RATH_RECEPTAUTHODATE", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand8.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_RATH_RECEPTAUTHO_YN", System.Data.OleDb.OleDbType.VarChar, 1, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "RATH_RECEPTAUTHO_YN", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand8.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_RATH_TIMESTAMP", System.Data.OleDb.OleDbType.DBTimeStamp, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "RATH_TIMESTAMP", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand8.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_RATH_USER_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "RATH_USER_ID", System.Data.DataRowVersion.Original, null));
			// 
			// oleDbInsertCommand9
			// 
			this.oleDbInsertCommand9.CommandText = "INSERT INTO FPASS_RECEPTIONAUTHORIZE(RATH_ID, RATH_CWR_ID, RATH_RATT_ID, RATH_USE" +
				"R_ID, RATH_RECEPTAUTHO_YN, RATH_RECEPTAUTHODATE, RATH_TIMESTAMP) VALUES (?, ?, ?" +
				", ?, ?, ?, ?)";
			this.oleDbInsertCommand9.Connection = this.oleDbConnection2;
			this.oleDbInsertCommand9.Parameters.Add(new System.Data.OleDb.OleDbParameter("RATH_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "RATH_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbInsertCommand9.Parameters.Add(new System.Data.OleDb.OleDbParameter("RATH_CWR_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "RATH_CWR_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbInsertCommand9.Parameters.Add(new System.Data.OleDb.OleDbParameter("RATH_RATT_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "RATH_RATT_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbInsertCommand9.Parameters.Add(new System.Data.OleDb.OleDbParameter("RATH_USER_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "RATH_USER_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbInsertCommand9.Parameters.Add(new System.Data.OleDb.OleDbParameter("RATH_RECEPTAUTHO_YN", System.Data.OleDb.OleDbType.VarChar, 1, "RATH_RECEPTAUTHO_YN"));
			this.oleDbInsertCommand9.Parameters.Add(new System.Data.OleDb.OleDbParameter("RATH_RECEPTAUTHODATE", System.Data.OleDb.OleDbType.DBTimeStamp, 5472, "RATH_RECEPTAUTHODATE"));
			this.oleDbInsertCommand9.Parameters.Add(new System.Data.OleDb.OleDbParameter("RATH_TIMESTAMP", System.Data.OleDb.OleDbType.DBTimeStamp, 5472, "RATH_TIMESTAMP"));
			// 
			// oleDbSelectCommand9
			// 
			this.oleDbSelectCommand9.CommandText = "SELECT RATH_ID, RATH_CWR_ID, RATH_RATT_ID, RATH_USER_ID, RATH_RECEPTAUTHO_YN, RAT" +
				"H_RECEPTAUTHODATE, RATH_TIMESTAMP FROM FPASS_RECEPTIONAUTHORIZE";
			this.oleDbSelectCommand9.Connection = this.oleDbConnection2;
			// 
			// oleDbUpdateCommand8
			// 
			this.oleDbUpdateCommand8.CommandText = @"UPDATE FPASS_RECEPTIONAUTHORIZE SET RATH_ID = ?, RATH_CWR_ID = ?, RATH_RATT_ID = ?, RATH_USER_ID = ?, RATH_RECEPTAUTHO_YN = ?, RATH_RECEPTAUTHODATE = ?, RATH_TIMESTAMP = ? WHERE (RATH_ID = ?) AND (RATH_CWR_ID = ?) AND (RATH_RATT_ID = ?) AND (RATH_RECEPTAUTHODATE = ? OR ? IS NULL AND RATH_RECEPTAUTHODATE IS NULL) AND (RATH_RECEPTAUTHO_YN = ?) AND (RATH_TIMESTAMP = ?) AND (RATH_USER_ID = ?)";
			this.oleDbUpdateCommand8.Connection = this.oleDbConnection2;
			this.oleDbUpdateCommand8.Parameters.Add(new System.Data.OleDb.OleDbParameter("RATH_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "RATH_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbUpdateCommand8.Parameters.Add(new System.Data.OleDb.OleDbParameter("RATH_CWR_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "RATH_CWR_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbUpdateCommand8.Parameters.Add(new System.Data.OleDb.OleDbParameter("RATH_RATT_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "RATH_RATT_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbUpdateCommand8.Parameters.Add(new System.Data.OleDb.OleDbParameter("RATH_USER_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "RATH_USER_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbUpdateCommand8.Parameters.Add(new System.Data.OleDb.OleDbParameter("RATH_RECEPTAUTHO_YN", System.Data.OleDb.OleDbType.VarChar, 1, "RATH_RECEPTAUTHO_YN"));
			this.oleDbUpdateCommand8.Parameters.Add(new System.Data.OleDb.OleDbParameter("RATH_RECEPTAUTHODATE", System.Data.OleDb.OleDbType.DBTimeStamp, 5472, "RATH_RECEPTAUTHODATE"));
			this.oleDbUpdateCommand8.Parameters.Add(new System.Data.OleDb.OleDbParameter("RATH_TIMESTAMP", System.Data.OleDb.OleDbType.DBTimeStamp, 5472, "RATH_TIMESTAMP"));
			this.oleDbUpdateCommand8.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_RATH_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "RATH_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand8.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_RATH_CWR_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "RATH_CWR_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand8.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_RATH_RATT_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "RATH_RATT_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand8.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_RATH_RECEPTAUTHODATE", System.Data.OleDb.OleDbType.DBTimeStamp, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "RATH_RECEPTAUTHODATE", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand8.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_RATH_RECEPTAUTHODATE1", System.Data.OleDb.OleDbType.DBTimeStamp, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "RATH_RECEPTAUTHODATE", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand8.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_RATH_RECEPTAUTHO_YN", System.Data.OleDb.OleDbType.VarChar, 1, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "RATH_RECEPTAUTHO_YN", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand8.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_RATH_TIMESTAMP", System.Data.OleDb.OleDbType.DBTimeStamp, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "RATH_TIMESTAMP", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand8.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_RATH_USER_ID", System.Data.OleDb.OleDbType.Decimal, 5472, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "RATH_USER_ID", System.Data.DataRowVersion.Original, null));
			// 
			// oracleSelectCommand1
			// 
			this.oracleSelectCommand1.CommandText = "SELECT UM_USER_ID, FPASS_USER_ID, BOTHNAMESTEL, UM_USERAPPLID FROM VW_FPASS_USERB" +
				"YROLE";
			this.oracleSelectCommand1.Connection = this.oracleConnection1;
			// 
			// oracleInsertCommand1
			// 
			this.oracleInsertCommand1.CommandText = "INSERT INTO VW_FPASS_USERBYROLE(UM_USER_ID, FPASS_USER_ID, BOTHNAMESTEL, UM_USERA" +
				"PPLID) VALUES (:UM_USER_ID, :FPASS_USER_ID, :BOTHNAMESTEL, :UM_USERAPPLID)";
			this.oracleInsertCommand1.Connection = this.oracleConnection1;
			this.oracleInsertCommand1.Parameters.Add(new System.Data.OracleClient.OracleParameter(":UM_USER_ID", System.Data.OracleClient.OracleType.Number, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "UM_USER_ID", System.Data.DataRowVersion.Current, null));
			this.oracleInsertCommand1.Parameters.Add(new System.Data.OracleClient.OracleParameter(":FPASS_USER_ID", System.Data.OracleClient.OracleType.Number, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "FPASS_USER_ID", System.Data.DataRowVersion.Current, null));
			this.oracleInsertCommand1.Parameters.Add(new System.Data.OracleClient.OracleParameter(":BOTHNAMESTEL", System.Data.OracleClient.OracleType.VarChar, 135, "BOTHNAMESTEL"));
			this.oracleInsertCommand1.Parameters.Add(new System.Data.OracleClient.OracleParameter(":UM_USERAPPLID", System.Data.OracleClient.OracleType.VarChar, 50, "UM_USERAPPLID"));
			// 
			// FrmDataSetWizard
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Name = "FrmDataSetWizard";
			this.Text = "FrmDataSetWizard";
			((System.ComponentModel.ISupportInitialize)(this.dsPlant1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dsExContractor1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dsDepartment1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dsPrecMedType1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dsCraft1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dsExcoCoord1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dsUser1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dsRole1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dsHistory1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dsDummy2)).EndInit();

		}
		#endregion

		#region Accessors 

		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// Initialize input fields.
		/// </summary>
		private void InitView()
		{
			// TODO: Add your initialization code here
		}
		#endregion // End of Methods



	}
}

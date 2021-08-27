using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Text; // to use the StringBuilder class

using Degussa.FPASS.Db; // DBSingleton
using Degussa.FPASS.InterfaceZks;
using Degussa.FPASS.Util.ProgressUtil;
using Degussa.FPASS.Util.Messages; // MessageSingleton
using Degussa.FPASS.Util.UserManagement;

using de.pta.Component.DbAccess;
using System.IO;

namespace Degussa.FPASS.InterfaceZks
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class frmMain : System.Windows.Forms.Form
	{
		#region Members

		private System.Windows.Forms.TextBox txtPersNo;
		private System.Windows.Forms.TextBox txtSurname;
		private System.Windows.Forms.TextBox txtFirstname;
		private System.Windows.Forms.TextBox txtCreatedOn;
		private System.Windows.Forms.TextBox txtValidUntilDate;
		private System.Windows.Forms.TextBox txtValidUntilTime;
		private System.Windows.Forms.TextBox txtAuthorizationModel1;
		private System.Windows.Forms.TextBox txtAuthorizationModel2;
		private System.Windows.Forms.TextBox txtType;
		private System.Windows.Forms.TextBox txtBuk;
		private System.Windows.Forms.TextBox txtSite;
		private System.Windows.Forms.Label lblKey;
		private System.Windows.Forms.Label lblPersNo;
		private System.Windows.Forms.Label lblIdNo;
		private System.Windows.Forms.Label lblSurname;
		private System.Windows.Forms.Label lblFirstname;
		private System.Windows.Forms.Label lblVehicleNo;
		private System.Windows.Forms.Label lblFirm;
		private System.Windows.Forms.Label lblSubfirm;
		private System.Windows.Forms.Label lblCreatedOn;
		private System.Windows.Forms.Label lblValidAsFromDate;
		private System.Windows.Forms.Label lblValidUntilDate;
		private System.Windows.Forms.Label lblAuthorized;
		private System.Windows.Forms.Label lblZone;
		private System.Windows.Forms.Label lblTimeModel1;
		private System.Windows.Forms.Label lblAuthorizationModel1;
		private System.Windows.Forms.Label lblType;
		private System.Windows.Forms.Label lblValidAsFromTime;
		private System.Windows.Forms.Label lblValidUntilTime;
		private System.Windows.Forms.Label lblBuk;
		private System.Windows.Forms.Label lblSite;
		private System.Windows.Forms.Label lblAuthorizationModel2;
		private System.Windows.Forms.Button btnIns;
		private System.Windows.Forms.Button btnUpd;
		private System.Windows.Forms.Button btnDel;
		private System.Windows.Forms.Button btnZks;
		private System.Windows.Forms.Panel pnlInputZks;
		private System.Windows.Forms.Panel pnlOutputZks;
		private System.Windows.Forms.Label lblReturnCodeZks;
		
		#endregion Members

		private System.Windows.Forms.Label lblTitle1;
        private System.Windows.Forms.Label lblWantedResult;
		private System.Windows.Forms.Button btnGetCoWorker;
		private System.Windows.Forms.Button btnShowCoWorker;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.TextBox txtZone;
		private System.Windows.Forms.TextBox txtSecured;
		private System.Windows.Forms.TextBox txtTimeModel;
		private System.Windows.Forms.TextBox txtValidFromTime;
		private System.Windows.Forms.TextBox txtValidFromDate;
		private System.Windows.Forms.TextBox txtSubExco;
		private System.Windows.Forms.TextBox txtExco;
		private System.Windows.Forms.TextBox txtVehicleRegNo;
		private System.Windows.Forms.TextBox txtIdCardNo;
		private System.Windows.Forms.TextBox txtTK;
		private System.Windows.Forms.TextBox txtReturnCode;
		private System.Windows.Forms.TextBox txtCoord;
		private System.Windows.Forms.TextBox txtSupervisor;
		private System.Windows.Forms.TextBox txtBirthDate;
		private System.Windows.Forms.TextBox txtBirthPlace;
		private System.Windows.Forms.TextBox txtDept;
		private System.Windows.Forms.TextBox txtDataString;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtReadIdCardNo;
		private System.Windows.Forms.TextBox txtKeyStringInsert;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtKeyStringUpdate;
		private System.Windows.Forms.TextBox txtAuthorizationModel1DateUntil;
		private System.Windows.Forms.TextBox txtAuthorizationModel1TimeUntil;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtTermNo;
		private System.Windows.Forms.Button btnOpenConnection;
		private System.Windows.Forms.Button btnCloseConnection;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtStatus;
		private System.Windows.Forms.Button btnGetForZks;
		private System.Windows.Forms.Button btnInsertAll;
		private System.Windows.Forms.Button btnProgress;
		private System.Windows.Forms.Button btnSystem;
		private System.Windows.Forms.TextBox txtAuthorizationModel2DateTime0;
		private System.Windows.Forms.TextBox txtAuthorizationModel2DateTime1;
		private System.Windows.Forms.Button btnDelIdCardNo;
        private TextBox txtCoWorkerId;
        private Button btnRenameTempl;

		#region Initialize

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmMain()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#endregion Initialize

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.pnlInputZks = new System.Windows.Forms.Panel();
            this.txtCoWorkerId = new System.Windows.Forms.TextBox();
            this.btnDelIdCardNo = new System.Windows.Forms.Button();
            this.btnInsertAll = new System.Windows.Forms.Button();
            this.btnGetForZks = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.txtAuthorizationModel2DateTime0 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtAuthorizationModel2DateTime1 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtAuthorizationModel1DateUntil = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtAuthorizationModel1TimeUntil = new System.Windows.Forms.TextBox();
            this.btnShowCoWorker = new System.Windows.Forms.Button();
            this.btnGetCoWorker = new System.Windows.Forms.Button();
            this.lblWantedResult = new System.Windows.Forms.Label();
            this.lblAuthorizationModel2 = new System.Windows.Forms.Label();
            this.lblSite = new System.Windows.Forms.Label();
            this.lblBuk = new System.Windows.Forms.Label();
            this.lblValidUntilTime = new System.Windows.Forms.Label();
            this.lblValidAsFromTime = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.lblAuthorizationModel1 = new System.Windows.Forms.Label();
            this.lblTimeModel1 = new System.Windows.Forms.Label();
            this.lblZone = new System.Windows.Forms.Label();
            this.lblAuthorized = new System.Windows.Forms.Label();
            this.lblValidUntilDate = new System.Windows.Forms.Label();
            this.lblValidAsFromDate = new System.Windows.Forms.Label();
            this.lblCreatedOn = new System.Windows.Forms.Label();
            this.lblSubfirm = new System.Windows.Forms.Label();
            this.lblFirm = new System.Windows.Forms.Label();
            this.lblVehicleNo = new System.Windows.Forms.Label();
            this.lblFirstname = new System.Windows.Forms.Label();
            this.lblSurname = new System.Windows.Forms.Label();
            this.lblIdNo = new System.Windows.Forms.Label();
            this.lblPersNo = new System.Windows.Forms.Label();
            this.lblKey = new System.Windows.Forms.Label();
            this.txtSite = new System.Windows.Forms.TextBox();
            this.txtBuk = new System.Windows.Forms.TextBox();
            this.txtType = new System.Windows.Forms.TextBox();
            this.txtAuthorizationModel2 = new System.Windows.Forms.TextBox();
            this.txtAuthorizationModel1 = new System.Windows.Forms.TextBox();
            this.txtZone = new System.Windows.Forms.TextBox();
            this.txtSecured = new System.Windows.Forms.TextBox();
            this.txtTimeModel = new System.Windows.Forms.TextBox();
            this.txtValidUntilTime = new System.Windows.Forms.TextBox();
            this.txtValidUntilDate = new System.Windows.Forms.TextBox();
            this.txtValidFromTime = new System.Windows.Forms.TextBox();
            this.txtValidFromDate = new System.Windows.Forms.TextBox();
            this.txtCreatedOn = new System.Windows.Forms.TextBox();
            this.txtSubExco = new System.Windows.Forms.TextBox();
            this.txtExco = new System.Windows.Forms.TextBox();
            this.txtVehicleRegNo = new System.Windows.Forms.TextBox();
            this.txtFirstname = new System.Windows.Forms.TextBox();
            this.txtSurname = new System.Windows.Forms.TextBox();
            this.txtIdCardNo = new System.Windows.Forms.TextBox();
            this.txtPersNo = new System.Windows.Forms.TextBox();
            this.txtTK = new System.Windows.Forms.TextBox();
            this.txtDept = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtBirthDate = new System.Windows.Forms.TextBox();
            this.txtBirthPlace = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCoord = new System.Windows.Forms.TextBox();
            this.txtSupervisor = new System.Windows.Forms.TextBox();
            this.btnIns = new System.Windows.Forms.Button();
            this.btnUpd = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnZks = new System.Windows.Forms.Button();
            this.pnlOutputZks = new System.Windows.Forms.Panel();
            this.btnSystem = new System.Windows.Forms.Button();
            this.btnProgress = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.btnCloseConnection = new System.Windows.Forms.Button();
            this.btnOpenConnection = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTermNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtKeyStringUpdate = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtReadIdCardNo = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtDataString = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtKeyStringInsert = new System.Windows.Forms.TextBox();
            this.lblReturnCodeZks = new System.Windows.Forms.Label();
            this.txtReturnCode = new System.Windows.Forms.TextBox();
            this.lblTitle1 = new System.Windows.Forms.Label();
            this.btnRenameTempl = new System.Windows.Forms.Button();
            this.pnlInputZks.SuspendLayout();
            this.pnlOutputZks.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlInputZks
            // 
            this.pnlInputZks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlInputZks.Controls.Add(this.txtCoWorkerId);
            this.pnlInputZks.Controls.Add(this.btnDelIdCardNo);
            this.pnlInputZks.Controls.Add(this.btnInsertAll);
            this.pnlInputZks.Controls.Add(this.btnGetForZks);
            this.pnlInputZks.Controls.Add(this.label13);
            this.pnlInputZks.Controls.Add(this.txtAuthorizationModel2DateTime0);
            this.pnlInputZks.Controls.Add(this.label14);
            this.pnlInputZks.Controls.Add(this.txtAuthorizationModel2DateTime1);
            this.pnlInputZks.Controls.Add(this.label11);
            this.pnlInputZks.Controls.Add(this.txtAuthorizationModel1DateUntil);
            this.pnlInputZks.Controls.Add(this.label12);
            this.pnlInputZks.Controls.Add(this.txtAuthorizationModel1TimeUntil);
            this.pnlInputZks.Controls.Add(this.btnShowCoWorker);
            this.pnlInputZks.Controls.Add(this.btnGetCoWorker);
            this.pnlInputZks.Controls.Add(this.lblWantedResult);
            this.pnlInputZks.Controls.Add(this.lblAuthorizationModel2);
            this.pnlInputZks.Controls.Add(this.lblSite);
            this.pnlInputZks.Controls.Add(this.lblBuk);
            this.pnlInputZks.Controls.Add(this.lblValidUntilTime);
            this.pnlInputZks.Controls.Add(this.lblValidAsFromTime);
            this.pnlInputZks.Controls.Add(this.lblType);
            this.pnlInputZks.Controls.Add(this.lblAuthorizationModel1);
            this.pnlInputZks.Controls.Add(this.lblTimeModel1);
            this.pnlInputZks.Controls.Add(this.lblZone);
            this.pnlInputZks.Controls.Add(this.lblAuthorized);
            this.pnlInputZks.Controls.Add(this.lblValidUntilDate);
            this.pnlInputZks.Controls.Add(this.lblValidAsFromDate);
            this.pnlInputZks.Controls.Add(this.lblCreatedOn);
            this.pnlInputZks.Controls.Add(this.lblSubfirm);
            this.pnlInputZks.Controls.Add(this.lblFirm);
            this.pnlInputZks.Controls.Add(this.lblVehicleNo);
            this.pnlInputZks.Controls.Add(this.lblFirstname);
            this.pnlInputZks.Controls.Add(this.lblSurname);
            this.pnlInputZks.Controls.Add(this.lblIdNo);
            this.pnlInputZks.Controls.Add(this.lblPersNo);
            this.pnlInputZks.Controls.Add(this.lblKey);
            this.pnlInputZks.Controls.Add(this.txtSite);
            this.pnlInputZks.Controls.Add(this.txtBuk);
            this.pnlInputZks.Controls.Add(this.txtType);
            this.pnlInputZks.Controls.Add(this.txtAuthorizationModel2);
            this.pnlInputZks.Controls.Add(this.txtAuthorizationModel1);
            this.pnlInputZks.Controls.Add(this.txtZone);
            this.pnlInputZks.Controls.Add(this.txtSecured);
            this.pnlInputZks.Controls.Add(this.txtTimeModel);
            this.pnlInputZks.Controls.Add(this.txtValidUntilTime);
            this.pnlInputZks.Controls.Add(this.txtValidUntilDate);
            this.pnlInputZks.Controls.Add(this.txtValidFromTime);
            this.pnlInputZks.Controls.Add(this.txtValidFromDate);
            this.pnlInputZks.Controls.Add(this.txtCreatedOn);
            this.pnlInputZks.Controls.Add(this.txtSubExco);
            this.pnlInputZks.Controls.Add(this.txtExco);
            this.pnlInputZks.Controls.Add(this.txtVehicleRegNo);
            this.pnlInputZks.Controls.Add(this.txtFirstname);
            this.pnlInputZks.Controls.Add(this.txtSurname);
            this.pnlInputZks.Controls.Add(this.txtIdCardNo);
            this.pnlInputZks.Controls.Add(this.txtPersNo);
            this.pnlInputZks.Controls.Add(this.txtTK);
            this.pnlInputZks.Controls.Add(this.txtDept);
            this.pnlInputZks.Controls.Add(this.label7);
            this.pnlInputZks.Controls.Add(this.label10);
            this.pnlInputZks.Controls.Add(this.label8);
            this.pnlInputZks.Controls.Add(this.txtBirthDate);
            this.pnlInputZks.Controls.Add(this.txtBirthPlace);
            this.pnlInputZks.Controls.Add(this.label5);
            this.pnlInputZks.Controls.Add(this.label6);
            this.pnlInputZks.Controls.Add(this.txtCoord);
            this.pnlInputZks.Controls.Add(this.txtSupervisor);
            this.pnlInputZks.Controls.Add(this.btnIns);
            this.pnlInputZks.Controls.Add(this.btnUpd);
            this.pnlInputZks.Controls.Add(this.btnDel);
            this.pnlInputZks.Location = new System.Drawing.Point(8, 47);
            this.pnlInputZks.Name = "pnlInputZks";
            this.pnlInputZks.Size = new System.Drawing.Size(488, 593);
            this.pnlInputZks.TabIndex = 0;
            // 
            // txtCoWorkerId
            // 
            this.txtCoWorkerId.Location = new System.Drawing.Point(120, 20);
            this.txtCoWorkerId.Name = "txtCoWorkerId";
            this.txtCoWorkerId.Size = new System.Drawing.Size(100, 20);
            this.txtCoWorkerId.TabIndex = 74;
            // 
            // btnDelIdCardNo
            // 
            this.btnDelIdCardNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelIdCardNo.Location = new System.Drawing.Point(384, 512);
            this.btnDelIdCardNo.Name = "btnDelIdCardNo";
            this.btnDelIdCardNo.Size = new System.Drawing.Size(96, 72);
            this.btnDelIdCardNo.TabIndex = 73;
            this.btnDelIdCardNo.Text = "DEL IDCARDNO";
            this.btnDelIdCardNo.Click += new System.EventHandler(this.btnDelIdCardNo_Click);
            // 
            // btnInsertAll
            // 
            this.btnInsertAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInsertAll.Location = new System.Drawing.Point(384, 392);
            this.btnInsertAll.Name = "btnInsertAll";
            this.btnInsertAll.Size = new System.Drawing.Size(96, 72);
            this.btnInsertAll.TabIndex = 72;
            this.btnInsertAll.Text = " INS ALL";
            this.btnInsertAll.Click += new System.EventHandler(this.btnInsertAll_Click);
            // 
            // btnGetForZks
            // 
            this.btnGetForZks.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetForZks.Location = new System.Drawing.Point(272, 96);
            this.btnGetForZks.Name = "btnGetForZks";
            this.btnGetForZks.Size = new System.Drawing.Size(208, 48);
            this.btnGetForZks.TabIndex = 71;
            this.btnGetForZks.Text = "Get FFMA-IDs to be inserted";
            this.btnGetForZks.Click += new System.EventHandler(this.btnGetForZks_Click);
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(152, 512);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(48, 16);
            this.label13.TabIndex = 68;
            this.label13.Text = "Datum0";
            // 
            // txtAuthorizationModel2DateTime0
            // 
            this.txtAuthorizationModel2DateTime0.Location = new System.Drawing.Point(200, 512);
            this.txtAuthorizationModel2DateTime0.MaxLength = 10;
            this.txtAuthorizationModel2DateTime0.Name = "txtAuthorizationModel2DateTime0";
            this.txtAuthorizationModel2DateTime0.Size = new System.Drawing.Size(168, 20);
            this.txtAuthorizationModel2DateTime0.TabIndex = 67;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(152, 536);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(48, 16);
            this.label14.TabIndex = 70;
            this.label14.Text = "Datum1";
            // 
            // txtAuthorizationModel2DateTime1
            // 
            this.txtAuthorizationModel2DateTime1.Location = new System.Drawing.Point(200, 536);
            this.txtAuthorizationModel2DateTime1.MaxLength = 8;
            this.txtAuthorizationModel2DateTime1.Name = "txtAuthorizationModel2DateTime1";
            this.txtAuthorizationModel2DateTime1.Size = new System.Drawing.Size(168, 20);
            this.txtAuthorizationModel2DateTime1.TabIndex = 69;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(160, 488);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(24, 16);
            this.label11.TabIndex = 64;
            this.label11.Text = "bis";
            // 
            // txtAuthorizationModel1DateUntil
            // 
            this.txtAuthorizationModel1DateUntil.Location = new System.Drawing.Point(184, 488);
            this.txtAuthorizationModel1DateUntil.MaxLength = 10;
            this.txtAuthorizationModel1DateUntil.Name = "txtAuthorizationModel1DateUntil";
            this.txtAuthorizationModel1DateUntil.Size = new System.Drawing.Size(64, 20);
            this.txtAuthorizationModel1DateUntil.TabIndex = 63;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(264, 488);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(24, 16);
            this.label12.TabIndex = 66;
            this.label12.Text = "um";
            // 
            // txtAuthorizationModel1TimeUntil
            // 
            this.txtAuthorizationModel1TimeUntil.Location = new System.Drawing.Point(296, 488);
            this.txtAuthorizationModel1TimeUntil.MaxLength = 8;
            this.txtAuthorizationModel1TimeUntil.Name = "txtAuthorizationModel1TimeUntil";
            this.txtAuthorizationModel1TimeUntil.Size = new System.Drawing.Size(72, 20);
            this.txtAuthorizationModel1TimeUntil.TabIndex = 65;
            // 
            // btnShowCoWorker
            // 
            this.btnShowCoWorker.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowCoWorker.Location = new System.Drawing.Point(384, 16);
            this.btnShowCoWorker.Name = "btnShowCoWorker";
            this.btnShowCoWorker.Size = new System.Drawing.Size(96, 72);
            this.btnShowCoWorker.TabIndex = 57;
            this.btnShowCoWorker.Text = "show FFMA";
            this.btnShowCoWorker.Click += new System.EventHandler(this.btnShowCoWorker_Click);
            // 
            // btnGetCoWorker
            // 
            this.btnGetCoWorker.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetCoWorker.Location = new System.Drawing.Point(272, 16);
            this.btnGetCoWorker.Name = "btnGetCoWorker";
            this.btnGetCoWorker.Size = new System.Drawing.Size(96, 72);
            this.btnGetCoWorker.TabIndex = 56;
            this.btnGetCoWorker.Text = "Get FFMA-IDs";
            this.btnGetCoWorker.Click += new System.EventHandler(this.btnGetCoWorker_Click);
            // 
            // lblWantedResult
            // 
            this.lblWantedResult.Enabled = false;
            this.lblWantedResult.Location = new System.Drawing.Point(16, 23);
            this.lblWantedResult.Name = "lblWantedResult";
            this.lblWantedResult.Size = new System.Drawing.Size(104, 24);
            this.lblWantedResult.TabIndex = 53;
            this.lblWantedResult.Text = "PK Id des FFMA";
            // 
            // lblAuthorizationModel2
            // 
            this.lblAuthorizationModel2.Location = new System.Drawing.Point(16, 512);
            this.lblAuthorizationModel2.Name = "lblAuthorizationModel2";
            this.lblAuthorizationModel2.Size = new System.Drawing.Size(96, 16);
            this.lblAuthorizationModel2.TabIndex = 43;
            this.lblAuthorizationModel2.Text = "Zutrittsmodell 2";
            // 
            // lblSite
            // 
            this.lblSite.Location = new System.Drawing.Point(264, 560);
            this.lblSite.Name = "lblSite";
            this.lblSite.Size = new System.Drawing.Size(48, 16);
            this.lblSite.TabIndex = 42;
            this.lblSite.Text = "Standort";
            // 
            // lblBuk
            // 
            this.lblBuk.Location = new System.Drawing.Point(160, 560);
            this.lblBuk.Name = "lblBuk";
            this.lblBuk.Size = new System.Drawing.Size(32, 16);
            this.lblBuk.TabIndex = 41;
            this.lblBuk.Text = "Buk";
            // 
            // lblValidUntilTime
            // 
            this.lblValidUntilTime.Location = new System.Drawing.Point(240, 440);
            this.lblValidUntilTime.Name = "lblValidUntilTime";
            this.lblValidUntilTime.Size = new System.Drawing.Size(24, 16);
            this.lblValidUntilTime.TabIndex = 40;
            this.lblValidUntilTime.Text = "um";
            // 
            // lblValidAsFromTime
            // 
            this.lblValidAsFromTime.Location = new System.Drawing.Point(240, 416);
            this.lblValidAsFromTime.Name = "lblValidAsFromTime";
            this.lblValidAsFromTime.Size = new System.Drawing.Size(24, 16);
            this.lblValidAsFromTime.TabIndex = 39;
            this.lblValidAsFromTime.Text = "um";
            // 
            // lblType
            // 
            this.lblType.Location = new System.Drawing.Point(16, 560);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(100, 16);
            this.lblType.TabIndex = 38;
            this.lblType.Text = "Typ";
            // 
            // lblAuthorizationModel1
            // 
            this.lblAuthorizationModel1.Location = new System.Drawing.Point(16, 488);
            this.lblAuthorizationModel1.Name = "lblAuthorizationModel1";
            this.lblAuthorizationModel1.Size = new System.Drawing.Size(100, 16);
            this.lblAuthorizationModel1.TabIndex = 36;
            this.lblAuthorizationModel1.Text = "Zutrittsmodell 1";
            // 
            // lblTimeModel1
            // 
            this.lblTimeModel1.Location = new System.Drawing.Point(280, 464);
            this.lblTimeModel1.Name = "lblTimeModel1";
            this.lblTimeModel1.Size = new System.Drawing.Size(56, 16);
            this.lblTimeModel1.TabIndex = 35;
            this.lblTimeModel1.Text = "Zeitmodell";
            // 
            // lblZone
            // 
            this.lblZone.Location = new System.Drawing.Point(152, 464);
            this.lblZone.Name = "lblZone";
            this.lblZone.Size = new System.Drawing.Size(96, 16);
            this.lblZone.TabIndex = 34;
            this.lblZone.Text = "Zonenverfolgung";
            // 
            // lblAuthorized
            // 
            this.lblAuthorized.Location = new System.Drawing.Point(16, 464);
            this.lblAuthorized.Name = "lblAuthorized";
            this.lblAuthorized.Size = new System.Drawing.Size(100, 16);
            this.lblAuthorized.TabIndex = 33;
            this.lblAuthorized.Text = "Zutrittsberechtigt";
            // 
            // lblValidUntilDate
            // 
            this.lblValidUntilDate.Location = new System.Drawing.Point(16, 440);
            this.lblValidUntilDate.Name = "lblValidUntilDate";
            this.lblValidUntilDate.Size = new System.Drawing.Size(100, 16);
            this.lblValidUntilDate.TabIndex = 32;
            this.lblValidUntilDate.Text = "Gültig bis";
            // 
            // lblValidAsFromDate
            // 
            this.lblValidAsFromDate.Location = new System.Drawing.Point(16, 416);
            this.lblValidAsFromDate.Name = "lblValidAsFromDate";
            this.lblValidAsFromDate.Size = new System.Drawing.Size(100, 16);
            this.lblValidAsFromDate.TabIndex = 31;
            this.lblValidAsFromDate.Text = "Gütlig Von";
            // 
            // lblCreatedOn
            // 
            this.lblCreatedOn.Location = new System.Drawing.Point(16, 392);
            this.lblCreatedOn.Name = "lblCreatedOn";
            this.lblCreatedOn.Size = new System.Drawing.Size(100, 16);
            this.lblCreatedOn.TabIndex = 30;
            this.lblCreatedOn.Text = "Erfassdatum";
            // 
            // lblSubfirm
            // 
            this.lblSubfirm.Location = new System.Drawing.Point(16, 248);
            this.lblSubfirm.Name = "lblSubfirm";
            this.lblSubfirm.Size = new System.Drawing.Size(100, 16);
            this.lblSubfirm.TabIndex = 29;
            this.lblSubfirm.Text = "Name Subfirma";
            // 
            // lblFirm
            // 
            this.lblFirm.Location = new System.Drawing.Point(16, 224);
            this.lblFirm.Name = "lblFirm";
            this.lblFirm.Size = new System.Drawing.Size(100, 16);
            this.lblFirm.TabIndex = 28;
            this.lblFirm.Text = "Name Fremdfirma";
            // 
            // lblVehicleNo
            // 
            this.lblVehicleNo.Location = new System.Drawing.Point(16, 200);
            this.lblVehicleNo.Name = "lblVehicleNo";
            this.lblVehicleNo.Size = new System.Drawing.Size(100, 16);
            this.lblVehicleNo.TabIndex = 27;
            this.lblVehicleNo.Text = "Kfz-Kennzeichen";
            // 
            // lblFirstname
            // 
            this.lblFirstname.Location = new System.Drawing.Point(16, 176);
            this.lblFirstname.Name = "lblFirstname";
            this.lblFirstname.Size = new System.Drawing.Size(100, 16);
            this.lblFirstname.TabIndex = 26;
            this.lblFirstname.Text = "Vorname";
            // 
            // lblSurname
            // 
            this.lblSurname.Location = new System.Drawing.Point(16, 152);
            this.lblSurname.Name = "lblSurname";
            this.lblSurname.Size = new System.Drawing.Size(100, 16);
            this.lblSurname.TabIndex = 25;
            this.lblSurname.Text = "Nachname";
            // 
            // lblIdNo
            // 
            this.lblIdNo.Location = new System.Drawing.Point(16, 120);
            this.lblIdNo.Name = "lblIdNo";
            this.lblIdNo.Size = new System.Drawing.Size(100, 16);
            this.lblIdNo.TabIndex = 24;
            this.lblIdNo.Text = "Ausweisnummer";
            // 
            // lblPersNo
            // 
            this.lblPersNo.Location = new System.Drawing.Point(16, 80);
            this.lblPersNo.Name = "lblPersNo";
            this.lblPersNo.Size = new System.Drawing.Size(72, 16);
            this.lblPersNo.TabIndex = 23;
            this.lblPersNo.Text = "PersonalNr";
            // 
            // lblKey
            // 
            this.lblKey.Location = new System.Drawing.Point(16, 56);
            this.lblKey.Name = "lblKey";
            this.lblKey.Size = new System.Drawing.Size(100, 16);
            this.lblKey.TabIndex = 22;
            this.lblKey.Text = "TK";
            // 
            // txtSite
            // 
            this.txtSite.Location = new System.Drawing.Point(320, 560);
            this.txtSite.MaxLength = 4;
            this.txtSite.Name = "txtSite";
            this.txtSite.Size = new System.Drawing.Size(48, 20);
            this.txtSite.TabIndex = 21;
            // 
            // txtBuk
            // 
            this.txtBuk.Location = new System.Drawing.Point(200, 560);
            this.txtBuk.MaxLength = 3;
            this.txtBuk.Name = "txtBuk";
            this.txtBuk.Size = new System.Drawing.Size(24, 20);
            this.txtBuk.TabIndex = 20;
            // 
            // txtType
            // 
            this.txtType.Location = new System.Drawing.Point(120, 560);
            this.txtType.MaxLength = 3;
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(24, 20);
            this.txtType.TabIndex = 19;
            // 
            // txtAuthorizationModel2
            // 
            this.txtAuthorizationModel2.Location = new System.Drawing.Point(120, 512);
            this.txtAuthorizationModel2.MaxLength = 3;
            this.txtAuthorizationModel2.Name = "txtAuthorizationModel2";
            this.txtAuthorizationModel2.Size = new System.Drawing.Size(32, 20);
            this.txtAuthorizationModel2.TabIndex = 18;
            // 
            // txtAuthorizationModel1
            // 
            this.txtAuthorizationModel1.Location = new System.Drawing.Point(120, 488);
            this.txtAuthorizationModel1.MaxLength = 3;
            this.txtAuthorizationModel1.Name = "txtAuthorizationModel1";
            this.txtAuthorizationModel1.Size = new System.Drawing.Size(32, 20);
            this.txtAuthorizationModel1.TabIndex = 17;
            // 
            // txtZone
            // 
            this.txtZone.Location = new System.Drawing.Point(248, 464);
            this.txtZone.MaxLength = 3;
            this.txtZone.Name = "txtZone";
            this.txtZone.Size = new System.Drawing.Size(24, 20);
            this.txtZone.TabIndex = 16;
            // 
            // txtSecured
            // 
            this.txtSecured.Location = new System.Drawing.Point(120, 464);
            this.txtSecured.MaxLength = 1;
            this.txtSecured.Name = "txtSecured";
            this.txtSecured.Size = new System.Drawing.Size(24, 20);
            this.txtSecured.TabIndex = 14;
            // 
            // txtTimeModel
            // 
            this.txtTimeModel.Location = new System.Drawing.Point(336, 464);
            this.txtTimeModel.MaxLength = 3;
            this.txtTimeModel.Name = "txtTimeModel";
            this.txtTimeModel.Size = new System.Drawing.Size(32, 20);
            this.txtTimeModel.TabIndex = 13;
            // 
            // txtValidUntilTime
            // 
            this.txtValidUntilTime.Location = new System.Drawing.Point(264, 440);
            this.txtValidUntilTime.MaxLength = 8;
            this.txtValidUntilTime.Name = "txtValidUntilTime";
            this.txtValidUntilTime.Size = new System.Drawing.Size(112, 20);
            this.txtValidUntilTime.TabIndex = 12;
            // 
            // txtValidUntilDate
            // 
            this.txtValidUntilDate.Location = new System.Drawing.Point(120, 440);
            this.txtValidUntilDate.MaxLength = 10;
            this.txtValidUntilDate.Name = "txtValidUntilDate";
            this.txtValidUntilDate.Size = new System.Drawing.Size(112, 20);
            this.txtValidUntilDate.TabIndex = 11;
            // 
            // txtValidFromTime
            // 
            this.txtValidFromTime.Location = new System.Drawing.Point(264, 416);
            this.txtValidFromTime.MaxLength = 8;
            this.txtValidFromTime.Name = "txtValidFromTime";
            this.txtValidFromTime.Size = new System.Drawing.Size(112, 20);
            this.txtValidFromTime.TabIndex = 10;
            // 
            // txtValidFromDate
            // 
            this.txtValidFromDate.Location = new System.Drawing.Point(120, 416);
            this.txtValidFromDate.MaxLength = 10;
            this.txtValidFromDate.Name = "txtValidFromDate";
            this.txtValidFromDate.Size = new System.Drawing.Size(112, 20);
            this.txtValidFromDate.TabIndex = 9;
            // 
            // txtCreatedOn
            // 
            this.txtCreatedOn.Location = new System.Drawing.Point(120, 392);
            this.txtCreatedOn.MaxLength = 20;
            this.txtCreatedOn.Name = "txtCreatedOn";
            this.txtCreatedOn.Size = new System.Drawing.Size(200, 20);
            this.txtCreatedOn.TabIndex = 8;
            // 
            // txtSubExco
            // 
            this.txtSubExco.Location = new System.Drawing.Point(120, 248);
            this.txtSubExco.MaxLength = 30;
            this.txtSubExco.Name = "txtSubExco";
            this.txtSubExco.Size = new System.Drawing.Size(256, 20);
            this.txtSubExco.TabIndex = 7;
            // 
            // txtExco
            // 
            this.txtExco.Location = new System.Drawing.Point(120, 224);
            this.txtExco.MaxLength = 30;
            this.txtExco.Name = "txtExco";
            this.txtExco.Size = new System.Drawing.Size(256, 20);
            this.txtExco.TabIndex = 6;
            // 
            // txtVehicleRegNo
            // 
            this.txtVehicleRegNo.Location = new System.Drawing.Point(120, 200);
            this.txtVehicleRegNo.MaxLength = 30;
            this.txtVehicleRegNo.Name = "txtVehicleRegNo";
            this.txtVehicleRegNo.Size = new System.Drawing.Size(256, 20);
            this.txtVehicleRegNo.TabIndex = 5;
            // 
            // txtFirstname
            // 
            this.txtFirstname.Location = new System.Drawing.Point(120, 176);
            this.txtFirstname.MaxLength = 30;
            this.txtFirstname.Name = "txtFirstname";
            this.txtFirstname.Size = new System.Drawing.Size(256, 20);
            this.txtFirstname.TabIndex = 4;
            // 
            // txtSurname
            // 
            this.txtSurname.Location = new System.Drawing.Point(120, 152);
            this.txtSurname.MaxLength = 30;
            this.txtSurname.Name = "txtSurname";
            this.txtSurname.Size = new System.Drawing.Size(256, 20);
            this.txtSurname.TabIndex = 3;
            // 
            // txtIdCardNo
            // 
            this.txtIdCardNo.Location = new System.Drawing.Point(120, 120);
            this.txtIdCardNo.MaxLength = 10;
            this.txtIdCardNo.Name = "txtIdCardNo";
            this.txtIdCardNo.Size = new System.Drawing.Size(96, 20);
            this.txtIdCardNo.TabIndex = 2;
            // 
            // txtPersNo
            // 
            this.txtPersNo.Location = new System.Drawing.Point(120, 80);
            this.txtPersNo.MaxLength = 8;
            this.txtPersNo.Name = "txtPersNo";
            this.txtPersNo.Size = new System.Drawing.Size(96, 20);
            this.txtPersNo.TabIndex = 1;
            // 
            // txtTK
            // 
            this.txtTK.Location = new System.Drawing.Point(120, 56);
            this.txtTK.MaxLength = 3;
            this.txtTK.Name = "txtTK";
            this.txtTK.Size = new System.Drawing.Size(24, 20);
            this.txtTK.TabIndex = 0;
            // 
            // txtDept
            // 
            this.txtDept.Location = new System.Drawing.Point(120, 368);
            this.txtDept.MaxLength = 30;
            this.txtDept.Name = "txtDept";
            this.txtDept.Size = new System.Drawing.Size(256, 20);
            this.txtDept.TabIndex = 59;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(16, 344);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 16);
            this.label7.TabIndex = 58;
            this.label7.Text = "Geburtsdatum";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(16, 368);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 16);
            this.label10.TabIndex = 61;
            this.label10.Text = "Abteilung";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(16, 320);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(104, 16);
            this.label8.TabIndex = 57;
            this.label8.Text = "Geburtsort";
            // 
            // txtBirthDate
            // 
            this.txtBirthDate.Location = new System.Drawing.Point(120, 344);
            this.txtBirthDate.MaxLength = 20;
            this.txtBirthDate.Name = "txtBirthDate";
            this.txtBirthDate.Size = new System.Drawing.Size(200, 20);
            this.txtBirthDate.TabIndex = 56;
            // 
            // txtBirthPlace
            // 
            this.txtBirthPlace.Location = new System.Drawing.Point(120, 320);
            this.txtBirthPlace.MaxLength = 30;
            this.txtBirthPlace.Name = "txtBirthPlace";
            this.txtBirthPlace.Size = new System.Drawing.Size(256, 20);
            this.txtBirthPlace.TabIndex = 55;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(16, 296);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 16);
            this.label5.TabIndex = 54;
            this.label5.Text = "Koordinator";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(16, 272);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 16);
            this.label6.TabIndex = 53;
            this.label6.Text = "Baustellenleiter";
            // 
            // txtCoord
            // 
            this.txtCoord.Location = new System.Drawing.Point(120, 296);
            this.txtCoord.MaxLength = 30;
            this.txtCoord.Name = "txtCoord";
            this.txtCoord.Size = new System.Drawing.Size(256, 20);
            this.txtCoord.TabIndex = 52;
            // 
            // txtSupervisor
            // 
            this.txtSupervisor.Location = new System.Drawing.Point(120, 272);
            this.txtSupervisor.MaxLength = 30;
            this.txtSupervisor.Name = "txtSupervisor";
            this.txtSupervisor.Size = new System.Drawing.Size(256, 20);
            this.txtSupervisor.TabIndex = 51;
            // 
            // btnIns
            // 
            this.btnIns.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIns.Location = new System.Drawing.Point(384, 152);
            this.btnIns.Name = "btnIns";
            this.btnIns.Size = new System.Drawing.Size(96, 72);
            this.btnIns.TabIndex = 47;
            this.btnIns.Text = "INS";
            this.btnIns.Click += new System.EventHandler(this.btnIns_Click);
            // 
            // btnUpd
            // 
            this.btnUpd.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpd.Location = new System.Drawing.Point(384, 232);
            this.btnUpd.Name = "btnUpd";
            this.btnUpd.Size = new System.Drawing.Size(96, 72);
            this.btnUpd.TabIndex = 48;
            this.btnUpd.Text = "UPD";
            this.btnUpd.Click += new System.EventHandler(this.btnUpd_Click);
            // 
            // btnDel
            // 
            this.btnDel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDel.Location = new System.Drawing.Point(384, 312);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(96, 72);
            this.btnDel.TabIndex = 49;
            this.btnDel.Text = "DEL";
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnZks
            // 
            this.btnZks.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnZks.Location = new System.Drawing.Point(200, 104);
            this.btnZks.Name = "btnZks";
            this.btnZks.Size = new System.Drawing.Size(152, 48);
            this.btnZks.TabIndex = 50;
            this.btnZks.Text = "Terminal lesen";
            this.btnZks.Click += new System.EventHandler(this.btnZks_Click);
            // 
            // pnlOutputZks
            // 
            this.pnlOutputZks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlOutputZks.Controls.Add(this.btnRenameTempl);
            this.pnlOutputZks.Controls.Add(this.btnSystem);
            this.pnlOutputZks.Controls.Add(this.btnProgress);
            this.pnlOutputZks.Controls.Add(this.label4);
            this.pnlOutputZks.Controls.Add(this.txtStatus);
            this.pnlOutputZks.Controls.Add(this.btnCloseConnection);
            this.pnlOutputZks.Controls.Add(this.btnOpenConnection);
            this.pnlOutputZks.Controls.Add(this.label3);
            this.pnlOutputZks.Controls.Add(this.txtTermNo);
            this.pnlOutputZks.Controls.Add(this.label2);
            this.pnlOutputZks.Controls.Add(this.txtKeyStringUpdate);
            this.pnlOutputZks.Controls.Add(this.label1);
            this.pnlOutputZks.Controls.Add(this.txtReadIdCardNo);
            this.pnlOutputZks.Controls.Add(this.label16);
            this.pnlOutputZks.Controls.Add(this.txtDataString);
            this.pnlOutputZks.Controls.Add(this.label15);
            this.pnlOutputZks.Controls.Add(this.txtKeyStringInsert);
            this.pnlOutputZks.Controls.Add(this.lblReturnCodeZks);
            this.pnlOutputZks.Controls.Add(this.txtReturnCode);
            this.pnlOutputZks.Controls.Add(this.btnZks);
            this.pnlOutputZks.Location = new System.Drawing.Point(504, 47);
            this.pnlOutputZks.Name = "pnlOutputZks";
            this.pnlOutputZks.Size = new System.Drawing.Size(368, 593);
            this.pnlOutputZks.TabIndex = 46;
            // 
            // btnSystem
            // 
            this.btnSystem.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSystem.Location = new System.Drawing.Point(16, 500);
            this.btnSystem.Name = "btnSystem";
            this.btnSystem.Size = new System.Drawing.Size(152, 32);
            this.btnSystem.TabIndex = 77;
            this.btnSystem.Text = "System";
            this.btnSystem.Click += new System.EventHandler(this.btnSystem_Click);
            // 
            // btnProgress
            // 
            this.btnProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProgress.Location = new System.Drawing.Point(200, 500);
            this.btnProgress.Name = "btnProgress";
            this.btnProgress.Size = new System.Drawing.Size(152, 32);
            this.btnProgress.TabIndex = 76;
            this.btnProgress.Text = "Progressbar";
            this.btnProgress.Click += new System.EventHandler(this.btnProgress_Click);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 24);
            this.label4.TabIndex = 75;
            this.label4.Text = "Status Connection";
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(96, 40);
            this.txtStatus.MaxLength = 3;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(256, 20);
            this.txtStatus.TabIndex = 74;
            // 
            // btnCloseConnection
            // 
            this.btnCloseConnection.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCloseConnection.Location = new System.Drawing.Point(200, 64);
            this.btnCloseConnection.Name = "btnCloseConnection";
            this.btnCloseConnection.Size = new System.Drawing.Size(152, 32);
            this.btnCloseConnection.TabIndex = 73;
            this.btnCloseConnection.Text = "Disconnect";
            this.btnCloseConnection.Click += new System.EventHandler(this.btnCloseConnection_Click);
            // 
            // btnOpenConnection
            // 
            this.btnOpenConnection.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenConnection.Location = new System.Drawing.Point(16, 64);
            this.btnOpenConnection.Name = "btnOpenConnection";
            this.btnOpenConnection.Size = new System.Drawing.Size(152, 32);
            this.btnOpenConnection.TabIndex = 72;
            this.btnOpenConnection.Text = "Connect";
            this.btnOpenConnection.Click += new System.EventHandler(this.btnOpenConnection_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 16);
            this.label3.TabIndex = 71;
            this.label3.Text = "Terminal";
            // 
            // txtTermNo
            // 
            this.txtTermNo.Location = new System.Drawing.Point(96, 104);
            this.txtTermNo.MaxLength = 3;
            this.txtTermNo.Name = "txtTermNo";
            this.txtTermNo.Size = new System.Drawing.Size(40, 20);
            this.txtTermNo.TabIndex = 70;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 184);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 32);
            this.label2.TabIndex = 69;
            this.label2.Text = "Key String Update";
            // 
            // txtKeyStringUpdate
            // 
            this.txtKeyStringUpdate.Location = new System.Drawing.Point(96, 184);
            this.txtKeyStringUpdate.MaxLength = 300;
            this.txtKeyStringUpdate.Multiline = true;
            this.txtKeyStringUpdate.Name = "txtKeyStringUpdate";
            this.txtKeyStringUpdate.Size = new System.Drawing.Size(256, 48);
            this.txtKeyStringUpdate.TabIndex = 68;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 24);
            this.label1.TabIndex = 67;
            this.label1.Text = "ausgelesene Ausweisnummer";
            // 
            // txtReadIdCardNo
            // 
            this.txtReadIdCardNo.Location = new System.Drawing.Point(96, 128);
            this.txtReadIdCardNo.MaxLength = 10;
            this.txtReadIdCardNo.Name = "txtReadIdCardNo";
            this.txtReadIdCardNo.Size = new System.Drawing.Size(96, 20);
            this.txtReadIdCardNo.TabIndex = 66;
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(8, 240);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(88, 16);
            this.label16.TabIndex = 65;
            this.label16.Text = "Data String";
            // 
            // txtDataString
            // 
            this.txtDataString.Location = new System.Drawing.Point(96, 240);
            this.txtDataString.MaxLength = 35000;
            this.txtDataString.Multiline = true;
            this.txtDataString.Name = "txtDataString";
            this.txtDataString.Size = new System.Drawing.Size(256, 240);
            this.txtDataString.TabIndex = 64;
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(8, 160);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(88, 16);
            this.label15.TabIndex = 63;
            this.label15.Text = "Key String Insert";
            // 
            // txtKeyStringInsert
            // 
            this.txtKeyStringInsert.Location = new System.Drawing.Point(96, 160);
            this.txtKeyStringInsert.MaxLength = 300;
            this.txtKeyStringInsert.Multiline = true;
            this.txtKeyStringInsert.Name = "txtKeyStringInsert";
            this.txtKeyStringInsert.Size = new System.Drawing.Size(256, 20);
            this.txtKeyStringInsert.TabIndex = 62;
            // 
            // lblReturnCodeZks
            // 
            this.lblReturnCodeZks.Location = new System.Drawing.Point(8, 16);
            this.lblReturnCodeZks.Name = "lblReturnCodeZks";
            this.lblReturnCodeZks.Size = new System.Drawing.Size(88, 16);
            this.lblReturnCodeZks.TabIndex = 48;
            this.lblReturnCodeZks.Text = "Return Code";
            // 
            // txtReturnCode
            // 
            this.txtReturnCode.Location = new System.Drawing.Point(96, 16);
            this.txtReturnCode.MaxLength = 2;
            this.txtReturnCode.Name = "txtReturnCode";
            this.txtReturnCode.Size = new System.Drawing.Size(256, 20);
            this.txtReturnCode.TabIndex = 46;
            // 
            // lblTitle1
            // 
            this.lblTitle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle1.Location = new System.Drawing.Point(8, 8);
            this.lblTitle1.Name = "lblTitle1";
            this.lblTitle1.Size = new System.Drawing.Size(864, 32);
            this.lblTitle1.TabIndex = 53;
            this.lblTitle1.Text = "Schnittstelle FPASS <-> ZKS - Test Interflex-DLL";
            this.lblTitle1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnRenameTempl
            // 
            this.btnRenameTempl.Location = new System.Drawing.Point(200, 548);
            this.btnRenameTempl.Name = "btnRenameTempl";
            this.btnRenameTempl.Size = new System.Drawing.Size(152, 28);
            this.btnRenameTempl.TabIndex = 78;
            this.btnRenameTempl.Text = "Rename Templates";
            this.btnRenameTempl.UseVisualStyleBackColor = true;
            this.btnRenameTempl.Click += new System.EventHandler(this.btnRenameTempl_Click);
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(880, 645);
            this.Controls.Add(this.lblTitle1);
            this.Controls.Add(this.pnlOutputZks);
            this.Controls.Add(this.pnlInputZks);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmMain";
            this.pnlInputZks.ResumeLayout(false);
            this.pnlInputZks.PerformLayout();
            this.pnlOutputZks.ResumeLayout(false);
            this.pnlOutputZks.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		#region Events

        /// <summary>
        /// Insert a row in Zks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void btnIns_Click(object sender, System.EventArgs e)
		{
			string result;
			decimal coWorkerId;
			FpassZks zksSystem = new FpassZks();

			// gets data
			coWorkerId = Convert.ToDecimal(txtCoWorkerId.Text);
            result = zksSystem.Insert(coWorkerId, false);
			MessageBox.Show("Insert: " + result);
			this.txtReturnCode.Text = result;
		} // btnIns_Click()


        /// <summary>
        ///  Update a row in Zks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void btnUpd_Click(object sender, System.EventArgs e)
		{
			string result;
			decimal coWorkerId;
			FpassZks zksSystem = new FpassZks();

			// gets data
            coWorkerId = Convert.ToDecimal(txtCoWorkerId.Text);
            result = zksSystem.Insert(coWorkerId, false);
			this.txtReturnCode.Text = result;
			MessageBox.Show("Update: " + result);		
		}  // btnUpd_Click()


        /// <summary>
        ///  Delete a row in Zks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void btnDel_Click(object sender, System.EventArgs e)
		{
			string result;
			decimal coWorkerId;
			FpassZks zksSystem = new FpassZks();

			// gets data
            coWorkerId = Convert.ToDecimal(txtCoWorkerId.Text);
			result = zksSystem.Delete(coWorkerId);
			this.txtReturnCode.Text = result;
			MessageBox.Show("Delete: " + result);
					
		} 

        /// <summary>
        ///  liest Ausweisnummer am Leseterminal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void btnZks_Click(object sender, System.EventArgs e)
		{
			string result;
			FpassZks zksSystem = new FpassZks();
			
			if (this.txtStatus.Text.Equals("Connected"))
			{
				result = zksSystem.Disconnect();
				if (result.Equals(MessageSingleton.GetInstance().GetMessage(MessageSingleton.ZKS_OK)))
					this.txtStatus.Text = "Disconnected";
				else
					this.txtStatus.Text = "Not disconnected";
			}
			// reconnect
			result = zksSystem.Connect();
			if (result.Equals(MessageSingleton.GetInstance().GetMessage(MessageSingleton.ZKS_OK)))
				this.txtStatus.Text = "Connected";
			else
				this.txtStatus.Text = "Not connected";
			// read IdCardNo
			this.txtReadIdCardNo.Text = zksSystem.ReadIdCardNo();
			// show Terminal number
			this.txtTermNo.Text = zksSystem.GetTerminal().ToString();
			// gets data
//			if ((this.txtTermNo.Text).Equals(""))
//			{
//				this.txtReadIdCardNo.Text = zksSystem.ReadIdCardNo();
//			}
//			else
//			{
//				this.txtReadIdCardNo.Text = zksSystem.ReadIdCardNo(Convert.ToInt32(this.txtTermNo.Text));
//			}
		}


		private void btnGetCoWorker_Click(object sender, System.EventArgs e)
		{
            //int columnCount;
			
            //// Gets DataProvider from DbAccess component
            //IProvider testDataProvider = DBSingleton.GetInstance().DataProvider;
            //// Creates the select commands
            //IDbCommand selectCmd = testDataProvider.CreateCommand("CoWorkerReports");
            //// Opens data reader to get data from database with the select command
            //IDataReader testDataReader = testDataProvider.GetReader(selectCmd);
			
            //// gets the number of columns to read for each row
            //columnCount = testDataReader.FieldCount;
            //ArrayList coWorkerIdList = new ArrayList();

            //// reads Ids
            //while (testDataReader.Read())
            //{
            //    coWorkerIdList.Add(testDataReader["CWR_ID"]);
            //}
            //this.cboCoWorkerId.DataSource = coWorkerIdList;
	
            //// Close the reader
            //testDataReader.Close();

		}  //btnGetCoWorker_Click()


        /// <summary>
        /// Shows coworker data string.
        /// TODO: not tested since FPASS V1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void btnShowCoWorker_Click(object sender, System.EventArgs e)
		{
			decimal coWorkerId;
			FpassZks zksSystem = new FpassZks();
			
			// gets data
			coWorkerId = Convert.ToDecimal(txtCoWorkerId.Text);
			zksSystem.GetDataFromDb(coWorkerId);
			SortedList cwkTable = zksSystem.mCoWorkerTable;

			// shows data
			this.txtTK.Text = cwkTable["CWR_ID"].ToString();
			this.txtPersNo.Text = cwkTable["CWR_PERSNO"].ToString();
			this.txtIdCardNo.Text = cwkTable["CWR_IDCARDNO"].ToString();
			this.txtSurname.Text = cwkTable["CWR_SURNAME"].ToString();
			this.txtFirstname.Text = cwkTable["CWR_FIRSTNAME"].ToString();
			this.txtVehicleRegNo.Text = cwkTable["VRNO_VEHREGNO"].ToString();
			this.txtExco.Text = cwkTable["EXCONTRACTOR"].ToString();
			this.txtSubExco.Text = cwkTable["SUBEXCONTRACTOR"].ToString();
			this.txtSupervisor.Text = cwkTable["SUPERVISOR"].ToString();
			this.txtCoord.Text = cwkTable["USER_NAME"].ToString();
			this.txtBirthPlace.Text = cwkTable["CWR_PLACEOFBIRTH"].ToString();
			this.txtBirthDate.Text = cwkTable["CWR_DATEOFBIRTH"].ToString();
			this.txtCreatedOn.Text = cwkTable["CWR_DATECREATED"].ToString();
			this.txtValidFromDate.Text = cwkTable["CWR_VALIDFROM"].ToString();
			this.txtValidFromTime.Text = cwkTable["CWR_VALIDFROM"].ToString();
			this.txtValidUntilDate.Text = cwkTable["CWR_VALIDUNTIL"].ToString();
			this.txtValidUntilTime.Text = cwkTable["CWR_VALIDUNTIL"].ToString();
			this.txtSecured.Text = cwkTable["RATH_RECEPTAUTHO_YN"].ToString();
			this.txtDept.Text = cwkTable["DEPT_DEPARTMENT"].ToString();
			this.txtZone.Text = cwkTable["PARZ_ZONENTRACING"].ToString();
			this.txtTimeModel.Text = cwkTable["PARZ_TIMEMODEL1"].ToString();
			this.txtAuthorizationModel1.Text = cwkTable["PARZ_ACCESSMODEL1"].ToString();
			this.txtAuthorizationModel1DateUntil.Text = "-";
			this.txtAuthorizationModel1TimeUntil.Text = "-";
			this.txtAuthorizationModel2.Text = cwkTable["PARZ_ACCESSMODEL2"].ToString();
			if (cwkTable.ContainsKey("CWBR_BRF_ID0"))
				this.txtAuthorizationModel2DateTime0.Text = cwkTable["CWBR_BRF_ID0"].ToString()
															+ "+" + cwkTable["CWBR_BRIEFING_YN0"].ToString()
															+ "+" + cwkTable["CWBR_INACTIVE_YN0"].ToString()
															+ "+" + cwkTable["CWBR_BRIEFINGDATE0"].ToString();
			else
				this.txtAuthorizationModel2DateTime0.Text = "kein KFZ-Zutritt";
			if (cwkTable.ContainsKey("CWBR_BRF_ID1"))
				this.txtAuthorizationModel2DateTime1.Text = cwkTable["CWBR_BRF_ID1"].ToString()
														+ "+" + cwkTable["CWBR_BRIEFING_YN1"].ToString()
														+ "+" + cwkTable["CWBR_INACTIVE_YN1"].ToString()
														+ "+" + cwkTable["CWBR_BRIEFINGDATE1"].ToString();
			else
				this.txtAuthorizationModel2DateTime1.Text = "kein KFZ-Zutritt";
			this.txtType.Text = cwkTable["PARZ_TYPE"].ToString();
			this.txtBuk.Text = cwkTable["PARZ_BUK"].ToString();
			this.txtSite.Text = cwkTable["PARZ_PLACE"].ToString();

			this.txtKeyStringInsert.Text = zksSystem.GetKeyStringInsert();
			this.txtKeyStringUpdate.Text = zksSystem.GetKeyStringUpdate() + "/n/n" + zksSystem.GetKeyStringUpdateWithTK();
			this.txtDataString.Text = zksSystem.GetDataStringMain() + "/n/n" + zksSystem.GetDataStringWithIdCardNo();

		} 


		private void btnOpenConnection_Click(object sender, System.EventArgs e)
		{
			string result;
			FpassZks zksSystem = new FpassZks();
			
			// connection
			result = zksSystem.Connect();
			this.txtReturnCode.Text = result;
			//MessageBox.Show("Connection: " + result);
			if (result.Equals(MessageSingleton.GetInstance().GetMessage(MessageSingleton.ZKS_OK)))
				this.txtStatus.Text = "Connected";
			else
				this.txtStatus.Text = "Not Connected";
		}

		private void btnCloseConnection_Click(object sender, System.EventArgs e)
		{
			string result;
			FpassZks zksSystem = new FpassZks();
			
			// disconnection
			result = zksSystem.Disconnect();
			this.txtReturnCode.Text = result;
			//MessageBox.Show("Disconnection: " + result);
			if (result.Equals(MessageSingleton.GetInstance().GetMessage(MessageSingleton.ZKS_OK)))
				this.txtStatus.Text = "Disconnected";
			else
				this.txtStatus.Text = "Not Disconnected";
		}


		private void btnGetForZks_Click(object sender, System.EventArgs e)
		{
			int columnCount;
			decimal MandatorId;
			
			// Gets DataProvider from DbAccess component
			IProvider testDataProvider = DBSingleton.GetInstance().DataProvider;
			// Creates the select commands
			IDbCommand selectCmd = testDataProvider.CreateCommand("SelectCoWorkerToInsert");

			// Parameter for the command = actual mandator id
			MandatorId = Convert.ToDecimal(UserManagementControl.getInstance().CurrentMandatorID);
			testDataProvider.SetParameter(selectCmd, ":CWR_MND_ID", MandatorId);

			// Opens data reader to get data from database with the select command
			IDataReader testDataReader = testDataProvider.GetReader(selectCmd);
			
			// gets the number of columns to read for each row
			columnCount = testDataReader.FieldCount;
			ArrayList coWorkerIdList = new ArrayList();

            //// reads Ids
            //while (testDataReader.Read())
            //{
            //    coWorkerIdList.Add(testDataReader["CWR_ID"]);
            //}
            //this.cboCoWorkerId.DataSource = coWorkerIdList;
	
			// Close the reader
			testDataReader.Close();
		}


		private void btnInsertAll_Click(object sender, System.EventArgs e)
		{
			string result;
			FpassZks zksSystem = new FpassZks();

			// gets data
			result = zksSystem.InsertAll();
			MessageBox.Show("Insert All: " + result);	
			this.txtReturnCode.Text = result;
		}


		private void btnProgress_Click(object sender, System.EventArgs e)
		{
			FrmProgress myBar = new FrmProgress();
//			Timer clock = new Timer();
			myBar.Open("Test Progressbar", 10, 1);
			for (int i=0; i< 10; i++)
			{
//				clock.Interval = 1000; //1000 ms = 1 s
//				clock.Start();
				myBar.PerformStep();
				MessageBox.Show(i.ToString());
			}
			myBar.Close();
		}

		private void btnSystem_Click(object sender, System.EventArgs e)
		{
			MessageBox.Show("MachineName: " + Environment.MachineName + " - UserDomainName: " + Environment.UserDomainName); // test
		} 

		

		private void btnDelIdCardNo_Click(object sender, System.EventArgs e)
		{
			string result;
			decimal coWorkerId;
			FpassZks zksSystem = new FpassZks();

			// gets data
            coWorkerId = Convert.ToDecimal(txtCoWorkerId.Text);
			result = zksSystem.Update(coWorkerId);
			this.txtReturnCode.Text = result;
			MessageBox.Show("Update: " + result);
        }

        #endregion Events

        private void btnRenameTempl_Click(object sender, EventArgs e)
        {
            DirectoryInfo d = new DirectoryInfo(@"C:\Projekte\FPASS_V51_trunc\FPASSReports");
            FileInfo[] infos = d.GetFiles();
            foreach (FileInfo f in infos)
            {
                File.Move(f.FullName, f.FullName.ToString().Replace("5.0", "5.1"));
            }
        }

    } 
} 

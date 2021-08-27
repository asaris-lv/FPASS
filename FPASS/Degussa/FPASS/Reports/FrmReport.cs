using System;
using System.Collections;
using System.ComponentModel;
using System.Data.OracleClient;
using System.Windows.Forms;
using de.pta.Component.Errorhandling;
using de.pta.Component.ListOfValues;
using Degussa.FPASS.Gui;
using Degussa.FPASS.InterfaceZks;
using Degussa.FPASS.Reports.UserControls;
using Degussa.FPASS.Reports.Util;
using Degussa.FPASS.Util;
using Degussa.FPASS.Util.ListOfValues;
using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.Util.UserManagement;
using Degussa.FPASS.Util.Validation;

namespace Degussa.FPASS.Reports
{
	/// <summary>
	/// A FrmReport is the view of the MVC-triad FrmReport,
	/// ReportController and ReportModel.
	/// FrmReport extends from the FPASSBaseView.
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
    public class FrmReport : FPASSBaseView
    {
        #region Members

        internal Button BtnGenerateReport;
        internal Label LblMask;
        internal Panel PnlReport;
        internal ComboBox CboReport;
        internal Label LblReport;
        internal Button BtnBackTo;
        internal Label LblDepartment;
        internal Label LblPlant;
        internal Label LblCraftNumber;
        internal Label LblSubcontractor;
        internal Label LblSupervisor;
        internal Label LblOrderNumber;
        internal Label LblPass;
        internal Label LblCoWorker;
        internal Panel PnlPass;
        internal Label LblValidityUntil;
        internal Label LblDeliveryDateUntil;
        internal Label LblValidityFrom;
        internal Label LblDeliveryDate;
        internal Label LblValidity;
        internal Label LblDeliveryDateFrom;
        internal Panel PnlCoWorker;
        internal Label LblFirstname;
        internal Label LblSurname;
        internal Label LblSite;
        internal Label LblPanelExternalContractor;
        internal Panel PnlExternalContractor;
        internal ComboBox CboSubcontractor;
        internal ComboBox CboSupervisor;
        internal ComboBox CobCoordinator;
        internal Label LblExternalContractor;
        internal Label LblCoordinator;
        internal ComboBox CobExternalContractor;
        internal Panel PnlSite;
        internal ComboBox CboPlant;
        internal ComboBox CboDepartment;
        internal ComboBox CboOrderNumber;
        internal ComboBox CboCraft;
        internal Label LblAccessAuthorization;
        internal Label LblStatus;
        internal Panel PnlAccessAuthorization;
        internal RadioButton RbtAccessAuthorizationNo;
        internal RadioButton RbtAccessAuthorizationYes;
        internal Panel PnlStatus;
        internal RadioButton RbtStatusNo;
        internal RadioButton RbtStatusYes;
        internal Label LblMonth;
        internal Button BtnSearch;
        private IContainer components;
        private Panel PnlUserControls;

        internal UCReportPlant ucReportPlant;
        internal UCReportCoWorker ucReportCoWorker1;
        internal UCReportExContractor ucReportExContractor1;
        internal UCReportAttendance ucReportAttendance1;
        internal UCReportRespMask ucReportRespMask1;
        internal UCReportChecklist ucReportChecklist1;
        internal UcReportExConBooking ucReportExConBooking;

        internal TextBox TxtDeliveryDateFrom;
        internal TextBox TxtDeliveryDateUntil;
        internal TextBox TxtValidityUntil;
        internal TextBox TxtValidityFrom;
        internal Panel PnlRespMask;
        internal Label LblRespMask;
        internal TextBox TxtMaintenanceUntil;
        internal TextBox TxtMaintenanceFrom;
        internal Label LblMaintenanceUntil;
        internal Label LblMaintenanceFrom;
        internal Label LblMaintenanceDate;
        internal TextBox TxtMaskNo;
        internal Label LblMaskNo;
        internal TextBox TxtSurname;
        internal TextBox TxtFirstname;
        internal Button BtnClearMask;
        internal Button BtnGenerateExport;
        internal SaveFileDialog saveFileDialog;
        private ToolTip TooExport;
        private ToolTip TooClearMask;
        private ToolTip TooSearch;
        private ToolTip TooReport;
        private ToolTip TooBackTo;
        internal Label LblMonthUntil;
        internal DateTimePicker DatMonthFrom;
        internal DateTimePicker DatMonthUntil;
        internal Panel PnlAttendance;
        internal CheckedListBox ClbAttExContractor;
        internal Label LblAttExContractor;
        internal Label LblAttTitle;

        /// <summary>
        /// holds the id of the current business object selected in the displayed table
        /// </summary>
        //int  mCurrentId = -1;
        internal CheckBox CbxReceived;
        internal CheckBox CbxDelivered;

        internal Label lblNoBook;
        internal TextBox txtXDays;
        internal Label lblNoBook2;
        internal Button btnPopupExcos;
        private ToolTip TooPopupExco;

        bool mCorrectFormat = true;
        internal Button BtnClearAcRadio;
        private ToolTip TooClearAcRadio;

        /// <summary>
        /// Id of currently selected ext contractor (used in checkboxlist Mehrfach FF)
        /// </summary>
        private int mSelectedExcoId = 0;

        #endregion //End of Members

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public FrmReport()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            FillLists();
            InitView();

            SetAuthorization();
        }
        #endregion //End of Constructors

        #region Initialization

        /// <summary>
        /// Initializes the members.
        /// </summary>
        private void Initialize()
        {
            MnuFunction.Enabled = false;
            MnuReports.Enabled = false;
        }

        #endregion //End of Initialization

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReport));
            this.PnlReport = new System.Windows.Forms.Panel();
            this.CboReport = new System.Windows.Forms.ComboBox();
            this.LblReport = new System.Windows.Forms.Label();
            this.LblCoWorker = new System.Windows.Forms.Label();
            this.LblSite = new System.Windows.Forms.Label();
            this.LblPanelExternalContractor = new System.Windows.Forms.Label();
            this.LblPass = new System.Windows.Forms.Label();
            this.PnlPass = new System.Windows.Forms.Panel();
            this.TxtValidityUntil = new System.Windows.Forms.TextBox();
            this.TxtValidityFrom = new System.Windows.Forms.TextBox();
            this.TxtDeliveryDateUntil = new System.Windows.Forms.TextBox();
            this.TxtDeliveryDateFrom = new System.Windows.Forms.TextBox();
            this.LblValidityUntil = new System.Windows.Forms.Label();
            this.LblDeliveryDateUntil = new System.Windows.Forms.Label();
            this.LblValidityFrom = new System.Windows.Forms.Label();
            this.LblDeliveryDate = new System.Windows.Forms.Label();
            this.LblValidity = new System.Windows.Forms.Label();
            this.LblDeliveryDateFrom = new System.Windows.Forms.Label();
            this.PnlCoWorker = new System.Windows.Forms.Panel();
            this.lblNoBook2 = new System.Windows.Forms.Label();
            this.lblNoBook = new System.Windows.Forms.Label();
            this.txtXDays = new System.Windows.Forms.TextBox();
            this.DatMonthUntil = new System.Windows.Forms.DateTimePicker();
            this.DatMonthFrom = new System.Windows.Forms.DateTimePicker();
            this.LblMonthUntil = new System.Windows.Forms.Label();
            this.TxtFirstname = new System.Windows.Forms.TextBox();
            this.TxtSurname = new System.Windows.Forms.TextBox();
            this.LblMonth = new System.Windows.Forms.Label();
            this.PnlStatus = new System.Windows.Forms.Panel();
            this.RbtStatusNo = new System.Windows.Forms.RadioButton();
            this.RbtStatusYes = new System.Windows.Forms.RadioButton();
            this.PnlAccessAuthorization = new System.Windows.Forms.Panel();
            this.RbtAccessAuthorizationNo = new System.Windows.Forms.RadioButton();
            this.RbtAccessAuthorizationYes = new System.Windows.Forms.RadioButton();
            this.LblStatus = new System.Windows.Forms.Label();
            this.LblAccessAuthorization = new System.Windows.Forms.Label();
            this.LblFirstname = new System.Windows.Forms.Label();
            this.LblSurname = new System.Windows.Forms.Label();
            this.PnlExternalContractor = new System.Windows.Forms.Panel();
            this.CboSubcontractor = new System.Windows.Forms.ComboBox();
            this.CboSupervisor = new System.Windows.Forms.ComboBox();
            this.LblSubcontractor = new System.Windows.Forms.Label();
            this.LblSupervisor = new System.Windows.Forms.Label();
            this.CobCoordinator = new System.Windows.Forms.ComboBox();
            this.LblExternalContractor = new System.Windows.Forms.Label();
            this.LblCoordinator = new System.Windows.Forms.Label();
            this.CobExternalContractor = new System.Windows.Forms.ComboBox();
            this.PnlSite = new System.Windows.Forms.Panel();
            this.CboDepartment = new System.Windows.Forms.ComboBox();
            this.CboPlant = new System.Windows.Forms.ComboBox();
            this.CboCraft = new System.Windows.Forms.ComboBox();
            this.CboOrderNumber = new System.Windows.Forms.ComboBox();
            this.LblDepartment = new System.Windows.Forms.Label();
            this.LblPlant = new System.Windows.Forms.Label();
            this.LblCraftNumber = new System.Windows.Forms.Label();
            this.LblOrderNumber = new System.Windows.Forms.Label();
            this.BtnGenerateReport = new System.Windows.Forms.Button();
            this.BtnBackTo = new System.Windows.Forms.Button();
            this.LblMask = new System.Windows.Forms.Label();
            this.BtnSearch = new System.Windows.Forms.Button();
            this.PnlUserControls = new System.Windows.Forms.Panel();
            this.ucReportRespMask1 = new Degussa.FPASS.Reports.UserControls.UCReportRespMask();
            this.ucReportChecklist1 = new Degussa.FPASS.Reports.UserControls.UCReportChecklist();
            this.ucReportCoWorker1 = new Degussa.FPASS.Reports.UserControls.UCReportCoWorker();
            this.ucReportExContractor1 = new Degussa.FPASS.Reports.UserControls.UCReportExContractor();
            this.ucReportAttendance1 = new Degussa.FPASS.Reports.UserControls.UCReportAttendance();
            this.ucReportPlant = new Degussa.FPASS.Reports.UserControls.UCReportPlant();
            this.ucReportExConBooking = new Degussa.FPASS.Reports.UserControls.UcReportExConBooking();
            this.PnlRespMask = new System.Windows.Forms.Panel();
            this.CbxReceived = new System.Windows.Forms.CheckBox();
            this.CbxDelivered = new System.Windows.Forms.CheckBox();
            this.TxtMaskNo = new System.Windows.Forms.TextBox();
            this.LblMaskNo = new System.Windows.Forms.Label();
            this.TxtMaintenanceUntil = new System.Windows.Forms.TextBox();
            this.TxtMaintenanceFrom = new System.Windows.Forms.TextBox();
            this.LblMaintenanceUntil = new System.Windows.Forms.Label();
            this.LblMaintenanceFrom = new System.Windows.Forms.Label();
            this.LblMaintenanceDate = new System.Windows.Forms.Label();
            this.LblRespMask = new System.Windows.Forms.Label();
            this.BtnClearMask = new System.Windows.Forms.Button();
            this.BtnGenerateExport = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.TooExport = new System.Windows.Forms.ToolTip(this.components);
            this.TooClearMask = new System.Windows.Forms.ToolTip(this.components);
            this.TooSearch = new System.Windows.Forms.ToolTip(this.components);
            this.TooReport = new System.Windows.Forms.ToolTip(this.components);
            this.TooBackTo = new System.Windows.Forms.ToolTip(this.components);
            this.PnlAttendance = new System.Windows.Forms.Panel();
            this.btnPopupExcos = new System.Windows.Forms.Button();
            this.LblAttExContractor = new System.Windows.Forms.Label();
            this.ClbAttExContractor = new System.Windows.Forms.CheckedListBox();
            this.LblAttTitle = new System.Windows.Forms.Label();
            this.TooPopupExco = new System.Windows.Forms.ToolTip(this.components);
            this.BtnClearAcRadio = new System.Windows.Forms.Button();
            this.TooClearAcRadio = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.SbpMessage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sbpenvironment)).BeginInit();
            this.PnlReport.SuspendLayout();
            this.PnlPass.SuspendLayout();
            this.PnlCoWorker.SuspendLayout();
            this.PnlStatus.SuspendLayout();
            this.PnlAccessAuthorization.SuspendLayout();
            this.PnlExternalContractor.SuspendLayout();
            this.PnlSite.SuspendLayout();
            this.PnlUserControls.SuspendLayout();
            this.PnlRespMask.SuspendLayout();
            this.PnlAttendance.SuspendLayout();
            this.SuspendLayout();
            // 
            // StbBase
            // 
            this.StbBase.TabIndex = 100;
            // 
            // PnlReport
            // 
            this.PnlReport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlReport.Controls.Add(this.CboReport);
            this.PnlReport.Controls.Add(this.LblReport);
            this.PnlReport.Location = new System.Drawing.Point(8, 48);
            this.PnlReport.Name = "PnlReport";
            this.PnlReport.Size = new System.Drawing.Size(1254, 44);
            this.PnlReport.TabIndex = 0;
            // 
            // CboReport
            // 
            this.CboReport.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboReport.ItemHeight = 15;
            this.CboReport.Location = new System.Drawing.Point(112, 8);
            this.CboReport.Name = "CboReport";
            this.CboReport.Size = new System.Drawing.Size(408, 23);
            this.CboReport.Sorted = true;
            this.CboReport.TabIndex = 1;
            this.CboReport.TabStop = false;
            this.CboReport.SelectedIndexChanged += new System.EventHandler(this.CboReport_SelectedIndexChanged);
            // 
            // LblReport
            // 
            this.LblReport.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblReport.Location = new System.Drawing.Point(24, 11);
            this.LblReport.Name = "LblReport";
            this.LblReport.Size = new System.Drawing.Size(72, 16);
            this.LblReport.TabIndex = 7;
            this.LblReport.Text = "Report";
            // 
            // LblCoWorker
            // 
            this.LblCoWorker.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCoWorker.Location = new System.Drawing.Point(16, 96);
            this.LblCoWorker.Name = "LblCoWorker";
            this.LblCoWorker.Size = new System.Drawing.Size(152, 16);
            this.LblCoWorker.TabIndex = 141;
            this.LblCoWorker.Text = " Fremdfirmenmitarbeiter";
            // 
            // LblSite
            // 
            this.LblSite.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSite.Location = new System.Drawing.Point(580, 327);
            this.LblSite.Name = "LblSite";
            this.LblSite.Size = new System.Drawing.Size(48, 17);
            this.LblSite.TabIndex = 140;
            this.LblSite.Text = " Werk";
            // 
            // LblPanelExternalContractor
            // 
            this.LblPanelExternalContractor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblPanelExternalContractor.Location = new System.Drawing.Point(340, 186);
            this.LblPanelExternalContractor.Name = "LblPanelExternalContractor";
            this.LblPanelExternalContractor.Size = new System.Drawing.Size(80, 16);
            this.LblPanelExternalContractor.TabIndex = 139;
            this.LblPanelExternalContractor.Text = " Fremdfirma";
            // 
            // LblPass
            // 
            this.LblPass.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblPass.Location = new System.Drawing.Point(16, 184);
            this.LblPass.Name = "LblPass";
            this.LblPass.Size = new System.Drawing.Size(96, 16);
            this.LblPass.TabIndex = 138;
            this.LblPass.Text = " Passierschein";
            // 
            // PnlPass
            // 
            this.PnlPass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlPass.Controls.Add(this.TxtValidityUntil);
            this.PnlPass.Controls.Add(this.TxtValidityFrom);
            this.PnlPass.Controls.Add(this.TxtDeliveryDateUntil);
            this.PnlPass.Controls.Add(this.TxtDeliveryDateFrom);
            this.PnlPass.Controls.Add(this.LblValidityUntil);
            this.PnlPass.Controls.Add(this.LblDeliveryDateUntil);
            this.PnlPass.Controls.Add(this.LblValidityFrom);
            this.PnlPass.Controls.Add(this.LblDeliveryDate);
            this.PnlPass.Controls.Add(this.LblValidity);
            this.PnlPass.Controls.Add(this.LblDeliveryDateFrom);
            this.PnlPass.Location = new System.Drawing.Point(8, 192);
            this.PnlPass.Name = "PnlPass";
            this.PnlPass.Size = new System.Drawing.Size(313, 128);
            this.PnlPass.TabIndex = 0;
            // 
            // TxtValidityUntil
            // 
            this.TxtValidityUntil.Enabled = false;
            this.TxtValidityUntil.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtValidityUntil.Location = new System.Drawing.Point(195, 92);
            this.TxtValidityUntil.Name = "TxtValidityUntil";
            this.TxtValidityUntil.Size = new System.Drawing.Size(90, 21);
            this.TxtValidityUntil.TabIndex = 0;
            this.TxtValidityUntil.TabStop = false;
            this.TxtValidityUntil.Leave += new System.EventHandler(this.TxtValidityUntil_Leave);
            // 
            // TxtValidityFrom
            // 
            this.TxtValidityFrom.Enabled = false;
            this.TxtValidityFrom.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtValidityFrom.Location = new System.Drawing.Point(64, 92);
            this.TxtValidityFrom.Name = "TxtValidityFrom";
            this.TxtValidityFrom.Size = new System.Drawing.Size(90, 21);
            this.TxtValidityFrom.TabIndex = 0;
            this.TxtValidityFrom.TabStop = false;
            this.TxtValidityFrom.Leave += new System.EventHandler(this.TxtValidityFrom_Leave);
            // 
            // TxtDeliveryDateUntil
            // 
            this.TxtDeliveryDateUntil.Enabled = false;
            this.TxtDeliveryDateUntil.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtDeliveryDateUntil.Location = new System.Drawing.Point(195, 40);
            this.TxtDeliveryDateUntil.Name = "TxtDeliveryDateUntil";
            this.TxtDeliveryDateUntil.Size = new System.Drawing.Size(90, 21);
            this.TxtDeliveryDateUntil.TabIndex = 0;
            this.TxtDeliveryDateUntil.TabStop = false;
            this.TxtDeliveryDateUntil.Leave += new System.EventHandler(this.TxtDeliveryDateUntil_Leave);
            // 
            // TxtDeliveryDateFrom
            // 
            this.TxtDeliveryDateFrom.Enabled = false;
            this.TxtDeliveryDateFrom.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtDeliveryDateFrom.Location = new System.Drawing.Point(64, 40);
            this.TxtDeliveryDateFrom.Name = "TxtDeliveryDateFrom";
            this.TxtDeliveryDateFrom.Size = new System.Drawing.Size(90, 21);
            this.TxtDeliveryDateFrom.TabIndex = 0;
            this.TxtDeliveryDateFrom.TabStop = false;
            this.TxtDeliveryDateFrom.Leave += new System.EventHandler(this.TxtDeliveryDateFrom_Leave);
            // 
            // LblValidityUntil
            // 
            this.LblValidityUntil.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblValidityUntil.Location = new System.Drawing.Point(163, 91);
            this.LblValidityUntil.Name = "LblValidityUntil";
            this.LblValidityUntil.Size = new System.Drawing.Size(32, 23);
            this.LblValidityUntil.TabIndex = 134;
            this.LblValidityUntil.Text = "bis";
            // 
            // LblDeliveryDateUntil
            // 
            this.LblDeliveryDateUntil.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblDeliveryDateUntil.Location = new System.Drawing.Point(163, 40);
            this.LblDeliveryDateUntil.Name = "LblDeliveryDateUntil";
            this.LblDeliveryDateUntil.Size = new System.Drawing.Size(32, 23);
            this.LblDeliveryDateUntil.TabIndex = 132;
            this.LblDeliveryDateUntil.Text = "bis";
            // 
            // LblValidityFrom
            // 
            this.LblValidityFrom.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblValidityFrom.Location = new System.Drawing.Point(24, 91);
            this.LblValidityFrom.Name = "LblValidityFrom";
            this.LblValidityFrom.Size = new System.Drawing.Size(32, 23);
            this.LblValidityFrom.TabIndex = 130;
            this.LblValidityFrom.Text = "von";
            // 
            // LblDeliveryDate
            // 
            this.LblDeliveryDate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblDeliveryDate.Location = new System.Drawing.Point(24, 16);
            this.LblDeliveryDate.Name = "LblDeliveryDate";
            this.LblDeliveryDate.Size = new System.Drawing.Size(96, 23);
            this.LblDeliveryDate.TabIndex = 114;
            this.LblDeliveryDate.Text = "Ausgabedatum";
            // 
            // LblValidity
            // 
            this.LblValidity.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblValidity.Location = new System.Drawing.Point(24, 68);
            this.LblValidity.Name = "LblValidity";
            this.LblValidity.Size = new System.Drawing.Size(88, 23);
            this.LblValidity.TabIndex = 127;
            this.LblValidity.Text = "Gültigkeit";
            // 
            // LblDeliveryDateFrom
            // 
            this.LblDeliveryDateFrom.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblDeliveryDateFrom.Location = new System.Drawing.Point(24, 40);
            this.LblDeliveryDateFrom.Name = "LblDeliveryDateFrom";
            this.LblDeliveryDateFrom.Size = new System.Drawing.Size(32, 23);
            this.LblDeliveryDateFrom.TabIndex = 128;
            this.LblDeliveryDateFrom.Text = "von";
            // 
            // PnlCoWorker
            // 
            this.PnlCoWorker.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlCoWorker.Controls.Add(this.BtnClearAcRadio);
            this.PnlCoWorker.Controls.Add(this.lblNoBook2);
            this.PnlCoWorker.Controls.Add(this.lblNoBook);
            this.PnlCoWorker.Controls.Add(this.txtXDays);
            this.PnlCoWorker.Controls.Add(this.DatMonthUntil);
            this.PnlCoWorker.Controls.Add(this.DatMonthFrom);
            this.PnlCoWorker.Controls.Add(this.LblMonthUntil);
            this.PnlCoWorker.Controls.Add(this.TxtFirstname);
            this.PnlCoWorker.Controls.Add(this.TxtSurname);
            this.PnlCoWorker.Controls.Add(this.LblMonth);
            this.PnlCoWorker.Controls.Add(this.PnlStatus);
            this.PnlCoWorker.Controls.Add(this.PnlAccessAuthorization);
            this.PnlCoWorker.Controls.Add(this.LblStatus);
            this.PnlCoWorker.Controls.Add(this.LblAccessAuthorization);
            this.PnlCoWorker.Controls.Add(this.LblFirstname);
            this.PnlCoWorker.Controls.Add(this.LblSurname);
            this.PnlCoWorker.Location = new System.Drawing.Point(8, 104);
            this.PnlCoWorker.Name = "PnlCoWorker";
            this.PnlCoWorker.Size = new System.Drawing.Size(1254, 76);
            this.PnlCoWorker.TabIndex = 0;
            // 
            // lblNoBook2
            // 
            this.lblNoBook2.BackColor = System.Drawing.SystemColors.Control;
            this.lblNoBook2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoBook2.Location = new System.Drawing.Point(994, 16);
            this.lblNoBook2.Name = "lblNoBook2";
            this.lblNoBook2.Size = new System.Drawing.Size(40, 23);
            this.lblNoBook2.TabIndex = 138;
            this.lblNoBook2.Text = "Tagen";
            // 
            // lblNoBook
            // 
            this.lblNoBook.BackColor = System.Drawing.SystemColors.Control;
            this.lblNoBook.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoBook.Location = new System.Drawing.Point(743, 17);
            this.lblNoBook.Name = "lblNoBook";
            this.lblNoBook.Size = new System.Drawing.Size(122, 23);
            this.lblNoBook.TabIndex = 137;
            this.lblNoBook.Text = "Keine Buchung seit";
            // 
            // txtXDays
            // 
            this.txtXDays.Enabled = false;
            this.txtXDays.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtXDays.Location = new System.Drawing.Point(892, 14);
            this.txtXDays.Name = "txtXDays";
            this.txtXDays.Size = new System.Drawing.Size(88, 21);
            this.txtXDays.TabIndex = 136;
            // 
            // DatMonthUntil
            // 
            this.DatMonthUntil.CustomFormat = "dd.MM.yyyy";
            this.DatMonthUntil.Enabled = false;
            this.DatMonthUntil.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatMonthUntil.Location = new System.Drawing.Point(1046, 40);
            this.DatMonthUntil.Name = "DatMonthUntil";
            this.DatMonthUntil.Size = new System.Drawing.Size(80, 20);
            this.DatMonthUntil.TabIndex = 135;
            this.DatMonthUntil.Leave += new System.EventHandler(this.DatMonthUntil_Leave);
            // 
            // DatMonthFrom
            // 
            this.DatMonthFrom.CustomFormat = "dd.MM.yyyy";
            this.DatMonthFrom.Enabled = false;
            this.DatMonthFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatMonthFrom.Location = new System.Drawing.Point(892, 40);
            this.DatMonthFrom.Name = "DatMonthFrom";
            this.DatMonthFrom.Size = new System.Drawing.Size(88, 20);
            this.DatMonthFrom.TabIndex = 134;
            this.DatMonthFrom.Leave += new System.EventHandler(this.DatMonthFrom_Leave);
            // 
            // LblMonthUntil
            // 
            this.LblMonthUntil.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMonthUntil.Location = new System.Drawing.Point(994, 41);
            this.LblMonthUntil.Name = "LblMonthUntil";
            this.LblMonthUntil.Size = new System.Drawing.Size(25, 23);
            this.LblMonthUntil.TabIndex = 133;
            this.LblMonthUntil.Text = "bis";
            // 
            // TxtFirstname
            // 
            this.TxtFirstname.Enabled = false;
            this.TxtFirstname.Location = new System.Drawing.Point(112, 40);
            this.TxtFirstname.Name = "TxtFirstname";
            this.TxtFirstname.Size = new System.Drawing.Size(220, 20);
            this.TxtFirstname.TabIndex = 128;
            // 
            // TxtSurname
            // 
            this.TxtSurname.Enabled = false;
            this.TxtSurname.Location = new System.Drawing.Point(112, 14);
            this.TxtSurname.Name = "TxtSurname";
            this.TxtSurname.Size = new System.Drawing.Size(220, 20);
            this.TxtSurname.TabIndex = 127;
            // 
            // LblMonth
            // 
            this.LblMonth.BackColor = System.Drawing.SystemColors.Control;
            this.LblMonth.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMonth.Location = new System.Drawing.Point(743, 42);
            this.LblMonth.Name = "LblMonth";
            this.LblMonth.Size = new System.Drawing.Size(128, 23);
            this.LblMonth.TabIndex = 126;
            this.LblMonth.Text = "Bewegungsdaten von";
            // 
            // PnlStatus
            // 
            this.PnlStatus.Controls.Add(this.RbtStatusNo);
            this.PnlStatus.Controls.Add(this.RbtStatusYes);
            this.PnlStatus.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PnlStatus.Location = new System.Drawing.Point(499, 40);
            this.PnlStatus.Name = "PnlStatus";
            this.PnlStatus.Size = new System.Drawing.Size(200, 24);
            this.PnlStatus.TabIndex = 0;
            // 
            // RbtStatusNo
            // 
            this.RbtStatusNo.Enabled = false;
            this.RbtStatusNo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtStatusNo.Location = new System.Drawing.Point(96, 0);
            this.RbtStatusNo.Name = "RbtStatusNo";
            this.RbtStatusNo.Size = new System.Drawing.Size(88, 24);
            this.RbtStatusNo.TabIndex = 0;
            this.RbtStatusNo.Text = "Ungültig";
            // 
            // RbtStatusYes
            // 
            this.RbtStatusYes.Enabled = false;
            this.RbtStatusYes.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtStatusYes.Location = new System.Drawing.Point(20, 0);
            this.RbtStatusYes.Name = "RbtStatusYes";
            this.RbtStatusYes.Size = new System.Drawing.Size(76, 24);
            this.RbtStatusYes.TabIndex = 0;
            this.RbtStatusYes.Text = "Gültig";
            // 
            // PnlAccessAuthorization
            // 
            this.PnlAccessAuthorization.Controls.Add(this.RbtAccessAuthorizationNo);
            this.PnlAccessAuthorization.Controls.Add(this.RbtAccessAuthorizationYes);
            this.PnlAccessAuthorization.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PnlAccessAuthorization.Location = new System.Drawing.Point(499, 11);
            this.PnlAccessAuthorization.Name = "PnlAccessAuthorization";
            this.PnlAccessAuthorization.Size = new System.Drawing.Size(200, 24);
            this.PnlAccessAuthorization.TabIndex = 0;
            // 
            // RbtAccessAuthorizationNo
            // 
            this.RbtAccessAuthorizationNo.Enabled = false;
            this.RbtAccessAuthorizationNo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtAccessAuthorizationNo.Location = new System.Drawing.Point(96, 0);
            this.RbtAccessAuthorizationNo.Name = "RbtAccessAuthorizationNo";
            this.RbtAccessAuthorizationNo.Size = new System.Drawing.Size(57, 24);
            this.RbtAccessAuthorizationNo.TabIndex = 0;
            this.RbtAccessAuthorizationNo.Text = "Nein";
            // 
            // RbtAccessAuthorizationYes
            // 
            this.RbtAccessAuthorizationYes.Enabled = false;
            this.RbtAccessAuthorizationYes.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtAccessAuthorizationYes.Location = new System.Drawing.Point(20, 0);
            this.RbtAccessAuthorizationYes.Name = "RbtAccessAuthorizationYes";
            this.RbtAccessAuthorizationYes.Size = new System.Drawing.Size(40, 24);
            this.RbtAccessAuthorizationYes.TabIndex = 0;
            this.RbtAccessAuthorizationYes.Text = "Ja";
            // 
            // LblStatus
            // 
            this.LblStatus.BackColor = System.Drawing.SystemColors.Control;
            this.LblStatus.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblStatus.Location = new System.Drawing.Point(380, 40);
            this.LblStatus.Name = "LblStatus";
            this.LblStatus.Size = new System.Drawing.Size(96, 23);
            this.LblStatus.TabIndex = 122;
            this.LblStatus.Text = "Status";
            // 
            // LblAccessAuthorization
            // 
            this.LblAccessAuthorization.BackColor = System.Drawing.SystemColors.Control;
            this.LblAccessAuthorization.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblAccessAuthorization.Location = new System.Drawing.Point(380, 16);
            this.LblAccessAuthorization.Name = "LblAccessAuthorization";
            this.LblAccessAuthorization.Size = new System.Drawing.Size(112, 23);
            this.LblAccessAuthorization.TabIndex = 121;
            this.LblAccessAuthorization.Text = "Zutrittsberechtigt";
            // 
            // LblFirstname
            // 
            this.LblFirstname.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblFirstname.Location = new System.Drawing.Point(24, 40);
            this.LblFirstname.Name = "LblFirstname";
            this.LblFirstname.Size = new System.Drawing.Size(95, 23);
            this.LblFirstname.TabIndex = 106;
            this.LblFirstname.Text = "Vorname";
            // 
            // LblSurname
            // 
            this.LblSurname.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSurname.Location = new System.Drawing.Point(24, 16);
            this.LblSurname.Name = "LblSurname";
            this.LblSurname.Size = new System.Drawing.Size(80, 23);
            this.LblSurname.TabIndex = 105;
            this.LblSurname.Text = "Nachname";
            // 
            // PnlExternalContractor
            // 
            this.PnlExternalContractor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlExternalContractor.Controls.Add(this.CboSubcontractor);
            this.PnlExternalContractor.Controls.Add(this.CboSupervisor);
            this.PnlExternalContractor.Controls.Add(this.LblSubcontractor);
            this.PnlExternalContractor.Controls.Add(this.LblSupervisor);
            this.PnlExternalContractor.Controls.Add(this.CobCoordinator);
            this.PnlExternalContractor.Controls.Add(this.LblExternalContractor);
            this.PnlExternalContractor.Controls.Add(this.LblCoordinator);
            this.PnlExternalContractor.Controls.Add(this.CobExternalContractor);
            this.PnlExternalContractor.Location = new System.Drawing.Point(331, 192);
            this.PnlExternalContractor.Name = "PnlExternalContractor";
            this.PnlExternalContractor.Size = new System.Drawing.Size(398, 128);
            this.PnlExternalContractor.TabIndex = 0;
            // 
            // CboSubcontractor
            // 
            this.CboSubcontractor.Enabled = false;
            this.CboSubcontractor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboSubcontractor.Location = new System.Drawing.Point(136, 88);
            this.CboSubcontractor.MaxLength = 30;
            this.CboSubcontractor.Name = "CboSubcontractor";
            this.CboSubcontractor.Size = new System.Drawing.Size(220, 23);
            this.CboSubcontractor.Sorted = true;
            this.CboSubcontractor.TabIndex = 0;
            this.CboSubcontractor.TabStop = false;
            // 
            // CboSupervisor
            // 
            this.CboSupervisor.AccessibleRole = System.Windows.Forms.AccessibleRole.List;
            this.CboSupervisor.Enabled = false;
            this.CboSupervisor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboSupervisor.Location = new System.Drawing.Point(136, 40);
            this.CboSupervisor.Name = "CboSupervisor";
            this.CboSupervisor.Size = new System.Drawing.Size(220, 23);
            this.CboSupervisor.Sorted = true;
            this.CboSupervisor.TabIndex = 0;
            this.CboSupervisor.TabStop = false;
            // 
            // LblSubcontractor
            // 
            this.LblSubcontractor.BackColor = System.Drawing.SystemColors.Control;
            this.LblSubcontractor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSubcontractor.Location = new System.Drawing.Point(24, 88);
            this.LblSubcontractor.Name = "LblSubcontractor";
            this.LblSubcontractor.Size = new System.Drawing.Size(96, 23);
            this.LblSubcontractor.TabIndex = 111;
            this.LblSubcontractor.Text = "Subfirma";
            // 
            // LblSupervisor
            // 
            this.LblSupervisor.BackColor = System.Drawing.SystemColors.Control;
            this.LblSupervisor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSupervisor.Location = new System.Drawing.Point(24, 40);
            this.LblSupervisor.Name = "LblSupervisor";
            this.LblSupervisor.Size = new System.Drawing.Size(96, 23);
            this.LblSupervisor.TabIndex = 110;
            this.LblSupervisor.Text = "Baustellenleiter";
            // 
            // CobCoordinator
            // 
            this.CobCoordinator.Enabled = false;
            this.CobCoordinator.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CobCoordinator.Location = new System.Drawing.Point(136, 64);
            this.CobCoordinator.MaxLength = 30;
            this.CobCoordinator.Name = "CobCoordinator";
            this.CobCoordinator.Size = new System.Drawing.Size(220, 23);
            this.CobCoordinator.Sorted = true;
            this.CobCoordinator.TabIndex = 0;
            this.CobCoordinator.TabStop = false;
            // 
            // LblExternalContractor
            // 
            this.LblExternalContractor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblExternalContractor.Location = new System.Drawing.Point(24, 16);
            this.LblExternalContractor.Name = "LblExternalContractor";
            this.LblExternalContractor.Size = new System.Drawing.Size(88, 23);
            this.LblExternalContractor.TabIndex = 107;
            this.LblExternalContractor.Text = "Fremdfirma";
            // 
            // LblCoordinator
            // 
            this.LblCoordinator.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCoordinator.Location = new System.Drawing.Point(24, 64);
            this.LblCoordinator.Name = "LblCoordinator";
            this.LblCoordinator.Size = new System.Drawing.Size(88, 23);
            this.LblCoordinator.TabIndex = 108;
            this.LblCoordinator.Text = "Koordinator";
            // 
            // CobExternalContractor
            // 
            this.CobExternalContractor.AccessibleRole = System.Windows.Forms.AccessibleRole.List;
            this.CobExternalContractor.Enabled = false;
            this.CobExternalContractor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CobExternalContractor.Location = new System.Drawing.Point(136, 16);
            this.CobExternalContractor.Name = "CobExternalContractor";
            this.CobExternalContractor.Size = new System.Drawing.Size(220, 23);
            this.CobExternalContractor.Sorted = true;
            this.CobExternalContractor.TabIndex = 0;
            this.CobExternalContractor.TabStop = false;
            // 
            // PnlSite
            // 
            this.PnlSite.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlSite.Controls.Add(this.CboDepartment);
            this.PnlSite.Controls.Add(this.CboPlant);
            this.PnlSite.Controls.Add(this.CboCraft);
            this.PnlSite.Controls.Add(this.CboOrderNumber);
            this.PnlSite.Controls.Add(this.LblDepartment);
            this.PnlSite.Controls.Add(this.LblPlant);
            this.PnlSite.Controls.Add(this.LblCraftNumber);
            this.PnlSite.Controls.Add(this.LblOrderNumber);
            this.PnlSite.Location = new System.Drawing.Point(574, 334);
            this.PnlSite.Name = "PnlSite";
            this.PnlSite.Size = new System.Drawing.Size(688, 82);
            this.PnlSite.TabIndex = 0;
            // 
            // CboDepartment
            // 
            this.CboDepartment.AccessibleRole = System.Windows.Forms.AccessibleRole.List;
            this.CboDepartment.Enabled = false;
            this.CboDepartment.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboDepartment.Location = new System.Drawing.Point(434, 39);
            this.CboDepartment.Name = "CboDepartment";
            this.CboDepartment.Size = new System.Drawing.Size(220, 23);
            this.CboDepartment.Sorted = true;
            this.CboDepartment.TabIndex = 0;
            this.CboDepartment.TabStop = false;
            // 
            // CboPlant
            // 
            this.CboPlant.Enabled = false;
            this.CboPlant.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboPlant.Location = new System.Drawing.Point(434, 15);
            this.CboPlant.MaxLength = 30;
            this.CboPlant.Name = "CboPlant";
            this.CboPlant.Size = new System.Drawing.Size(220, 23);
            this.CboPlant.Sorted = true;
            this.CboPlant.TabIndex = 0;
            this.CboPlant.TabStop = false;
            // 
            // CboCraft
            // 
            this.CboCraft.AccessibleRole = System.Windows.Forms.AccessibleRole.List;
            this.CboCraft.Enabled = false;
            this.CboCraft.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboCraft.Location = new System.Drawing.Point(105, 40);
            this.CboCraft.Name = "CboCraft";
            this.CboCraft.Size = new System.Drawing.Size(220, 23);
            this.CboCraft.Sorted = true;
            this.CboCraft.TabIndex = 0;
            this.CboCraft.TabStop = false;
            // 
            // CboOrderNumber
            // 
            this.CboOrderNumber.Enabled = false;
            this.CboOrderNumber.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboOrderNumber.Location = new System.Drawing.Point(105, 16);
            this.CboOrderNumber.MaxLength = 30;
            this.CboOrderNumber.Name = "CboOrderNumber";
            this.CboOrderNumber.Size = new System.Drawing.Size(220, 23);
            this.CboOrderNumber.Sorted = true;
            this.CboOrderNumber.TabIndex = 0;
            this.CboOrderNumber.TabStop = false;
            // 
            // LblDepartment
            // 
            this.LblDepartment.BackColor = System.Drawing.SystemColors.Control;
            this.LblDepartment.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblDepartment.Location = new System.Drawing.Point(341, 39);
            this.LblDepartment.Name = "LblDepartment";
            this.LblDepartment.Size = new System.Drawing.Size(96, 23);
            this.LblDepartment.TabIndex = 118;
            this.LblDepartment.Text = "Abteilung";
            // 
            // LblPlant
            // 
            this.LblPlant.BackColor = System.Drawing.SystemColors.Control;
            this.LblPlant.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblPlant.Location = new System.Drawing.Point(341, 15);
            this.LblPlant.Name = "LblPlant";
            this.LblPlant.Size = new System.Drawing.Size(96, 23);
            this.LblPlant.TabIndex = 117;
            this.LblPlant.Text = "Betrieb";
            // 
            // LblCraftNumber
            // 
            this.LblCraftNumber.BackColor = System.Drawing.SystemColors.Control;
            this.LblCraftNumber.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCraftNumber.Location = new System.Drawing.Point(12, 40);
            this.LblCraftNumber.Name = "LblCraftNumber";
            this.LblCraftNumber.Size = new System.Drawing.Size(96, 23);
            this.LblCraftNumber.TabIndex = 116;
            this.LblCraftNumber.Text = "Gewerk";
            // 
            // LblOrderNumber
            // 
            this.LblOrderNumber.BackColor = System.Drawing.SystemColors.Control;
            this.LblOrderNumber.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblOrderNumber.Location = new System.Drawing.Point(12, 16);
            this.LblOrderNumber.Name = "LblOrderNumber";
            this.LblOrderNumber.Size = new System.Drawing.Size(96, 23);
            this.LblOrderNumber.TabIndex = 109;
            this.LblOrderNumber.Text = "Auftrags-Nr.";
            // 
            // BtnGenerateReport
            // 
            this.BtnGenerateReport.Enabled = false;
            this.BtnGenerateReport.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnGenerateReport.Location = new System.Drawing.Point(980, 860);
            this.BtnGenerateReport.Name = "BtnGenerateReport";
            this.BtnGenerateReport.Size = new System.Drawing.Size(128, 30);
            this.BtnGenerateReport.TabIndex = 151;
            this.BtnGenerateReport.TabStop = false;
            this.BtnGenerateReport.Text = "&Report generieren";
            this.TooReport.SetToolTip(this.BtnGenerateReport, "Erzeugt einen Report");
            this.BtnGenerateReport.Click += new System.EventHandler(this.BtnGenerateReport_Click);
            // 
            // BtnBackTo
            // 
            this.BtnBackTo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBackTo.Location = new System.Drawing.Point(1124, 860);
            this.BtnBackTo.Name = "BtnBackTo";
            this.BtnBackTo.Size = new System.Drawing.Size(128, 30);
            this.BtnBackTo.TabIndex = 152;
            this.BtnBackTo.TabStop = false;
            this.BtnBackTo.Text = "&Zurück";
            this.TooBackTo.SetToolTip(this.BtnBackTo, "Zurück zur Übersichtsmaske");
            this.BtnBackTo.Click += new System.EventHandler(this.BtnBackTo_Click);
            // 
            // LblMask
            // 
            this.LblMask.BackColor = System.Drawing.Color.White;
            this.LblMask.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMask.Location = new System.Drawing.Point(502, 8);
            this.LblMask.Name = "LblMask";
            this.LblMask.Size = new System.Drawing.Size(243, 32);
            this.LblMask.TabIndex = 76;
            this.LblMask.Text = "FPASS - Reports";
            // 
            // BtnSearch
            // 
            this.BtnSearch.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSearch.Location = new System.Drawing.Point(836, 861);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(128, 30);
            this.BtnSearch.TabIndex = 150;
            this.BtnSearch.TabStop = false;
            this.BtnSearch.Text = "&Suche";
            this.TooSearch.SetToolTip(this.BtnSearch, "Löst den Suchvorgang aus");
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // PnlUserControls
            // 
            this.PnlUserControls.Controls.Add(this.ucReportRespMask1);
            this.PnlUserControls.Controls.Add(this.ucReportChecklist1);
            this.PnlUserControls.Controls.Add(this.ucReportCoWorker1);
            this.PnlUserControls.Controls.Add(this.ucReportExContractor1);
            this.PnlUserControls.Controls.Add(this.ucReportAttendance1);
            this.PnlUserControls.Controls.Add(this.ucReportPlant);
            this.PnlUserControls.Controls.Add(this.ucReportExConBooking);
            this.PnlUserControls.Location = new System.Drawing.Point(8, 420);
            this.PnlUserControls.Name = "PnlUserControls";
            this.PnlUserControls.Size = new System.Drawing.Size(1260, 434);
            this.PnlUserControls.TabIndex = 144;
            // 
            // ucReportRespMask1
            // 
            this.ucReportRespMask1.Location = new System.Drawing.Point(0, 4);
            this.ucReportRespMask1.Name = "ucReportRespMask1";
            this.ucReportRespMask1.Size = new System.Drawing.Size(1254, 420);
            this.ucReportRespMask1.TabIndex = 2;
            // 
            // ucReportChecklist1
            // 
            this.ucReportChecklist1.CheckListID = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.ucReportChecklist1.Location = new System.Drawing.Point(0, 4);
            this.ucReportChecklist1.Name = "ucReportChecklist1";
            this.ucReportChecklist1.Size = new System.Drawing.Size(1254, 420);
            this.ucReportChecklist1.TabIndex = 0;
            this.ucReportChecklist1.TabStop = false;
            // 
            // ucReportCoWorker1
            // 
            this.ucReportCoWorker1.CoWorkerID = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.ucReportCoWorker1.Location = new System.Drawing.Point(0, 4);
            this.ucReportCoWorker1.Name = "ucReportCoWorker1";
            this.ucReportCoWorker1.Size = new System.Drawing.Size(1254, 420);
            this.ucReportCoWorker1.TabIndex = 0;
            this.ucReportCoWorker1.TabStop = false;
            this.ucReportCoWorker1.Visible = false;
            // 
            // ucReportExContractor1
            // 
            this.ucReportExContractor1.ExContractorID = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.ucReportExContractor1.Location = new System.Drawing.Point(0, 4);
            this.ucReportExContractor1.Name = "ucReportExContractor1";
            this.ucReportExContractor1.Size = new System.Drawing.Size(1254, 420);
            this.ucReportExContractor1.TabIndex = 0;
            this.ucReportExContractor1.TabStop = false;
            this.ucReportExContractor1.Visible = false;
            // 
            // ucReportAttendance1
            // 
            this.ucReportAttendance1.CoWorkerID = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.ucReportAttendance1.Location = new System.Drawing.Point(0, 4);
            this.ucReportAttendance1.Name = "ucReportAttendance1";
            this.ucReportAttendance1.Size = new System.Drawing.Size(1254, 420);
            this.ucReportAttendance1.TabIndex = 0;
            this.ucReportAttendance1.TabStop = false;
            this.ucReportAttendance1.Visible = false;
            // 
            // ucReportPlant
            // 
            this.ucReportPlant.Location = new System.Drawing.Point(0, 4);
            this.ucReportPlant.Name = "ucReportPlant";
            this.ucReportPlant.PlantID = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.ucReportPlant.Size = new System.Drawing.Size(1254, 420);
            this.ucReportPlant.TabIndex = 0;
            this.ucReportPlant.TabStop = false;
            this.ucReportPlant.Visible = false;
            // 
            // ucReportExConBooking
            // 
            this.ucReportExConBooking.Location = new System.Drawing.Point(0, 4);
            this.ucReportExConBooking.Name = "ucReportExConBooking";
            this.ucReportExConBooking.Size = new System.Drawing.Size(1254, 420);
            this.ucReportExConBooking.TabIndex = 1;
            // 
            // PnlRespMask
            // 
            this.PnlRespMask.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlRespMask.Controls.Add(this.CbxReceived);
            this.PnlRespMask.Controls.Add(this.CbxDelivered);
            this.PnlRespMask.Controls.Add(this.TxtMaskNo);
            this.PnlRespMask.Controls.Add(this.LblMaskNo);
            this.PnlRespMask.Controls.Add(this.TxtMaintenanceUntil);
            this.PnlRespMask.Controls.Add(this.TxtMaintenanceFrom);
            this.PnlRespMask.Controls.Add(this.LblMaintenanceUntil);
            this.PnlRespMask.Controls.Add(this.LblMaintenanceFrom);
            this.PnlRespMask.Controls.Add(this.LblMaintenanceDate);
            this.PnlRespMask.Location = new System.Drawing.Point(8, 334);
            this.PnlRespMask.Name = "PnlRespMask";
            this.PnlRespMask.Size = new System.Drawing.Size(560, 82);
            this.PnlRespMask.TabIndex = 145;
            // 
            // CbxReceived
            // 
            this.CbxReceived.Enabled = false;
            this.CbxReceived.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CbxReceived.Location = new System.Drawing.Point(438, 16);
            this.CbxReceived.Name = "CbxReceived";
            this.CbxReceived.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.CbxReceived.Size = new System.Drawing.Size(96, 16);
            this.CbxReceived.TabIndex = 143;
            this.CbxReceived.Text = "Verliehen";
            this.CbxReceived.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CbxDelivered
            // 
            this.CbxDelivered.Enabled = false;
            this.CbxDelivered.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CbxDelivered.Location = new System.Drawing.Point(438, 42);
            this.CbxDelivered.Name = "CbxDelivered";
            this.CbxDelivered.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.CbxDelivered.Size = new System.Drawing.Size(96, 24);
            this.CbxDelivered.TabIndex = 142;
            this.CbxDelivered.Text = "Abgegeben";
            this.CbxDelivered.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TxtMaskNo
            // 
            this.TxtMaskNo.Enabled = false;
            this.TxtMaskNo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtMaskNo.Location = new System.Drawing.Point(195, 46);
            this.TxtMaskNo.Name = "TxtMaskNo";
            this.TxtMaskNo.Size = new System.Drawing.Size(224, 21);
            this.TxtMaskNo.TabIndex = 140;
            this.TxtMaskNo.TabStop = false;
            // 
            // LblMaskNo
            // 
            this.LblMaskNo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMaskNo.Location = new System.Drawing.Point(16, 46);
            this.LblMaskNo.Name = "LblMaskNo";
            this.LblMaskNo.Size = new System.Drawing.Size(111, 23);
            this.LblMaskNo.TabIndex = 141;
            this.LblMaskNo.Text = "Maskennummer";
            // 
            // TxtMaintenanceUntil
            // 
            this.TxtMaintenanceUntil.Enabled = false;
            this.TxtMaintenanceUntil.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtMaintenanceUntil.Location = new System.Drawing.Point(329, 17);
            this.TxtMaintenanceUntil.Name = "TxtMaintenanceUntil";
            this.TxtMaintenanceUntil.Size = new System.Drawing.Size(90, 21);
            this.TxtMaintenanceUntil.TabIndex = 136;
            this.TxtMaintenanceUntil.TabStop = false;
            this.TxtMaintenanceUntil.Leave += new System.EventHandler(this.TxtMaintenanceUntil_Leave);
            // 
            // TxtMaintenanceFrom
            // 
            this.TxtMaintenanceFrom.Enabled = false;
            this.TxtMaintenanceFrom.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtMaintenanceFrom.Location = new System.Drawing.Point(195, 16);
            this.TxtMaintenanceFrom.Name = "TxtMaintenanceFrom";
            this.TxtMaintenanceFrom.Size = new System.Drawing.Size(90, 21);
            this.TxtMaintenanceFrom.TabIndex = 135;
            this.TxtMaintenanceFrom.TabStop = false;
            this.TxtMaintenanceFrom.Leave += new System.EventHandler(this.TxtMaintenanceFrom_Leave);
            // 
            // LblMaintenanceUntil
            // 
            this.LblMaintenanceUntil.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMaintenanceUntil.Location = new System.Drawing.Point(300, 17);
            this.LblMaintenanceUntil.Name = "LblMaintenanceUntil";
            this.LblMaintenanceUntil.Size = new System.Drawing.Size(32, 23);
            this.LblMaintenanceUntil.TabIndex = 139;
            this.LblMaintenanceUntil.Text = "bis";
            // 
            // LblMaintenanceFrom
            // 
            this.LblMaintenanceFrom.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMaintenanceFrom.Location = new System.Drawing.Point(163, 17);
            this.LblMaintenanceFrom.Name = "LblMaintenanceFrom";
            this.LblMaintenanceFrom.Size = new System.Drawing.Size(32, 23);
            this.LblMaintenanceFrom.TabIndex = 138;
            this.LblMaintenanceFrom.Text = "von";
            // 
            // LblMaintenanceDate
            // 
            this.LblMaintenanceDate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMaintenanceDate.Location = new System.Drawing.Point(16, 16);
            this.LblMaintenanceDate.Name = "LblMaintenanceDate";
            this.LblMaintenanceDate.Size = new System.Drawing.Size(104, 16);
            this.LblMaintenanceDate.TabIndex = 137;
            this.LblMaintenanceDate.Text = "Wartungsdatum";
            // 
            // LblRespMask
            // 
            this.LblRespMask.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblRespMask.Location = new System.Drawing.Point(16, 326);
            this.LblRespMask.Name = "LblRespMask";
            this.LblRespMask.Size = new System.Drawing.Size(120, 16);
            this.LblRespMask.TabIndex = 146;
            this.LblRespMask.Text = "Atemschutzmaske";
            // 
            // BtnClearMask
            // 
            this.BtnClearMask.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClearMask.Location = new System.Drawing.Point(692, 860);
            this.BtnClearMask.Name = "BtnClearMask";
            this.BtnClearMask.Size = new System.Drawing.Size(128, 30);
            this.BtnClearMask.TabIndex = 149;
            this.BtnClearMask.TabStop = false;
            this.BtnClearMask.Text = "&Maske leeren";
            this.TooClearMask.SetToolTip(this.BtnClearMask, "Verwirft alle bereits eingebenen Suchkriterien");
            this.BtnClearMask.Click += new System.EventHandler(this.BtnClearMask_Click);
            // 
            // BtnGenerateExport
            // 
            this.BtnGenerateExport.Enabled = false;
            this.BtnGenerateExport.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnGenerateExport.Location = new System.Drawing.Point(548, 860);
            this.BtnGenerateExport.Name = "BtnGenerateExport";
            this.BtnGenerateExport.Size = new System.Drawing.Size(128, 30);
            this.BtnGenerateExport.TabIndex = 148;
            this.BtnGenerateExport.TabStop = false;
            this.BtnGenerateExport.Text = "&Export generieren";
            this.TooExport.SetToolTip(this.BtnGenerateExport, "Erzeugt ein Export-File");
            this.BtnGenerateExport.Click += new System.EventHandler(this.BtnGenerateExport_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.AddExtension = false;
            this.saveFileDialog.Filter = "csv-Dateien (*.csv)|*.csv";
            this.saveFileDialog.Title = "Export-Datei speichern unter... (Dateiname ohne Erweiterung eingeben)";
            this.saveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog_FileOk);
            // 
            // PnlAttendance
            // 
            this.PnlAttendance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlAttendance.Controls.Add(this.btnPopupExcos);
            this.PnlAttendance.Controls.Add(this.LblAttExContractor);
            this.PnlAttendance.Controls.Add(this.ClbAttExContractor);
            this.PnlAttendance.Location = new System.Drawing.Point(739, 192);
            this.PnlAttendance.Name = "PnlAttendance";
            this.PnlAttendance.Size = new System.Drawing.Size(523, 129);
            this.PnlAttendance.TabIndex = 153;
            // 
            // btnPopupExcos
            // 
            this.btnPopupExcos.Enabled = false;
            this.btnPopupExcos.Image = ((System.Drawing.Image)(resources.GetObject("btnPopupExcos.Image")));
            this.btnPopupExcos.Location = new System.Drawing.Point(67, 84);
            this.btnPopupExcos.Name = "btnPopupExcos";
            this.btnPopupExcos.Size = new System.Drawing.Size(30, 30);
            this.btnPopupExcos.TabIndex = 110;
            this.TooPopupExco.SetToolTip(this.btnPopupExcos, "Öffnet die Liste der Fremdfirmen in einem Popup");
            this.btnPopupExcos.UseVisualStyleBackColor = true;
            this.btnPopupExcos.Click += new System.EventHandler(this.btnPopupExcos_Click);
            // 
            // LblAttExContractor
            // 
            this.LblAttExContractor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblAttExContractor.Location = new System.Drawing.Point(12, 19);
            this.LblAttExContractor.Name = "LblAttExContractor";
            this.LblAttExContractor.Size = new System.Drawing.Size(82, 23);
            this.LblAttExContractor.TabIndex = 108;
            this.LblAttExContractor.Text = "Fremdfirmen";
            // 
            // ClbAttExContractor
            // 
            this.ClbAttExContractor.CheckOnClick = true;
            this.ClbAttExContractor.Enabled = false;
            this.ClbAttExContractor.Location = new System.Drawing.Point(105, 20);
            this.ClbAttExContractor.Name = "ClbAttExContractor";
            this.ClbAttExContractor.Size = new System.Drawing.Size(290, 94);
            this.ClbAttExContractor.TabIndex = 0;
            this.ClbAttExContractor.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ClbAttExContractor_ItemCheck);
            this.ClbAttExContractor.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ClbAttExContractor_KeyUp);
            this.ClbAttExContractor.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.ClbAttExContractor_PreviewKeyDown);
            // 
            // LblAttTitle
            // 
            this.LblAttTitle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblAttTitle.Location = new System.Drawing.Point(746, 186);
            this.LblAttTitle.Name = "LblAttTitle";
            this.LblAttTitle.Size = new System.Drawing.Size(193, 18);
            this.LblAttTitle.TabIndex = 154;
            this.LblAttTitle.Text = "Fremdfirmen Mehrfachauswahl";
            // 
            // BtnClearAcRadio
            // 
            this.BtnClearAcRadio.Enabled = false;
            this.BtnClearAcRadio.Image = ((System.Drawing.Image)(resources.GetObject("BtnClearAcRadio.Image")));
            this.BtnClearAcRadio.Location = new System.Drawing.Point(677, 40);
            this.BtnClearAcRadio.Name = "BtnClearAcRadio";
            this.BtnClearAcRadio.Size = new System.Drawing.Size(30, 23);
            this.BtnClearAcRadio.TabIndex = 139;
            this.TooClearAcRadio.SetToolTip(this.BtnClearAcRadio, "Leert die Felder Zutrittsberechtigt und Status");
            this.BtnClearAcRadio.UseVisualStyleBackColor = true;
            this.BtnClearAcRadio.Click += new System.EventHandler(this.BtnClearAcRadio_Click);
            // 
            // FrmReport
            // 
            this.AcceptButton = this.BtnSearch;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(1270, 971);
            this.Controls.Add(this.LblSite);
            this.Controls.Add(this.BtnGenerateExport);
            this.Controls.Add(this.BtnClearMask);
            this.Controls.Add(this.BtnSearch);
            this.Controls.Add(this.LblAttTitle);
            this.Controls.Add(this.LblCoWorker);
            this.Controls.Add(this.LblMask);
            this.Controls.Add(this.LblRespMask);
            this.Controls.Add(this.PnlAttendance);
            this.Controls.Add(this.PnlRespMask);
            this.Controls.Add(this.BtnBackTo);
            this.Controls.Add(this.LblPanelExternalContractor);
            this.Controls.Add(this.LblPass);
            this.Controls.Add(this.BtnGenerateReport);
            this.Controls.Add(this.PnlReport);
            this.Controls.Add(this.PnlCoWorker);
            this.Controls.Add(this.PnlSite);
            this.Controls.Add(this.PnlPass);
            this.Controls.Add(this.PnlExternalContractor);
            this.Controls.Add(this.PnlUserControls);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmReport";
            this.Text = "FPASS - Reports";
            this.Controls.SetChildIndex(this.PnlUserControls, 0);
            this.Controls.SetChildIndex(this.LblBaseHead, 0);
            this.Controls.SetChildIndex(this.PnlExternalContractor, 0);
            this.Controls.SetChildIndex(this.PnlPass, 0);
            this.Controls.SetChildIndex(this.PnlSite, 0);
            this.Controls.SetChildIndex(this.PnlCoWorker, 0);
            this.Controls.SetChildIndex(this.PnlReport, 0);
            this.Controls.SetChildIndex(this.BtnGenerateReport, 0);
            this.Controls.SetChildIndex(this.LblPass, 0);
            this.Controls.SetChildIndex(this.LblPanelExternalContractor, 0);
            this.Controls.SetChildIndex(this.BtnBackTo, 0);
            this.Controls.SetChildIndex(this.PnlRespMask, 0);
            this.Controls.SetChildIndex(this.PnlAttendance, 0);
            this.Controls.SetChildIndex(this.LblRespMask, 0);
            this.Controls.SetChildIndex(this.LblMask, 0);
            this.Controls.SetChildIndex(this.LblCoWorker, 0);
            this.Controls.SetChildIndex(this.LblAttTitle, 0);
            this.Controls.SetChildIndex(this.BtnSearch, 0);
            this.Controls.SetChildIndex(this.BtnClearMask, 0);
            this.Controls.SetChildIndex(this.BtnGenerateExport, 0);
            this.Controls.SetChildIndex(this.StbBase, 0);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            this.Controls.SetChildIndex(this.LblSite, 0);
            ((System.ComponentModel.ISupportInitialize)(this.SbpMessage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sbpenvironment)).EndInit();
            this.PnlReport.ResumeLayout(false);
            this.PnlPass.ResumeLayout(false);
            this.PnlPass.PerformLayout();
            this.PnlCoWorker.ResumeLayout(false);
            this.PnlCoWorker.PerformLayout();
            this.PnlStatus.ResumeLayout(false);
            this.PnlAccessAuthorization.ResumeLayout(false);
            this.PnlExternalContractor.ResumeLayout(false);
            this.PnlSite.ResumeLayout(false);
            this.PnlUserControls.ResumeLayout(false);
            this.PnlRespMask.ResumeLayout(false);
            this.PnlRespMask.PerformLayout();
            this.PnlAttendance.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
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
            MnuFunction.Enabled = false;
            MnuReports.Enabled = false;
        }

        private ReportsController GetMyController()
        {
            return (ReportsController)mController;
        }

        #region FillComboxes

        /// <summary>
        /// Fills dropdownlists
        /// </summary>
        internal override void FillLists()
        {
            FillCoordinator();
            FillCraft();
            FillDepartment();
            FillExContractor();
            FillOrderNumber();
            FillPlant();
            FillSubcontractor();
            FillSupervisor();
        }

        /// <summary>
        /// Fills external contractor lists:
        /// Fills "Fremdfirma" dropdownlist with active excontractors
        /// Fills checklistbox under "Anwesenheit" with active excontractors AND excos not known to FPASS
        /// Need two lists otherwise Ghost effects
        /// </summary>
        public void FillExContractor()
        {
            ArrayList fpassContractors = LovSingleton.GetInstance().GetRootList(null, "FPASS_EXCONTRACTOR", "EXCO_NAME");
            ArrayList bothContractors = new ArrayList(fpassContractors);

            // List of active FPASS excontractors for normal dropdownlist
            fpassContractors.Add(new LovItem("0", ""));
            fpassContractors.Reverse();
            CobExternalContractor.DataSource = fpassContractors;
            CobExternalContractor.DisplayMember = "ItemValue";
            CobExternalContractor.ValueMember = "Id";

            // Get list of excontractors not known to FPASS
            // and add to list
            bothContractors.AddRange(FPASSLovsSingleton.GetInstance().NonFPASSContractors.Values);
            bothContractors.Add(new LovItem("0", Globals.PLACEHOLDER_EMPTY));

            // Show this list in checkboxlist
            ClbAttExContractor.DataSource = bothContractors;
            ClbAttExContractor.DisplayMember = "ItemValue";
            ClbAttExContractor.ValueMember = "Id";
        }

        /// <summary>
        /// Fills Supervisor list ("Baustellenleiter")
        /// </summary>
        public void FillSupervisor()
        {
            ArrayList supervisor = new ArrayList();
            supervisor.Add(new LovItem("0", ""));
            supervisor.AddRange(LovSingleton.GetInstance().GetRootList(null, "FPASS_SUPERVISOR", "SUPERVISOR"));
            CboSupervisor.DataSource = supervisor;
            CboSupervisor.DisplayMember = "ItemValue";
            CboSupervisor.ValueMember = "Id";
        }

        public void FillCoordinator()
        {
            ArrayList coordinator = new ArrayList();
            coordinator.Add(new LovItem("0", ""));
            coordinator.AddRange(LovSingleton.GetInstance().GetRootList(null, "VW_FPASS_COORDINATORS", "VWC_BOTHNAMES"));
            CobCoordinator.DataSource = coordinator;
            CobCoordinator.DisplayMember = "ItemValue";
            CobCoordinator.ValueMember = "Id";
        }

        public void FillSubcontractor()
        {
            ArrayList subcontractor = new ArrayList();
            subcontractor = LovSingleton.GetInstance().GetRootList(null, "FPASS_EXCONTRACTOR", "EXCO_NAME");
            subcontractor.Add(new LovItem("0", ""));
            subcontractor.Reverse();
            CboSubcontractor.DataSource = subcontractor;
            CboSubcontractor.DisplayMember = "ItemValue";
            CboSubcontractor.ValueMember = "Id";
        }

        /// <summary>
        /// Fills combobox with unique order numbers.
        /// Have to work with PK CWR_ID in database, get order nos for all coworkers.
        /// Nulls and double entries are filtered out by using hashtable
        /// </summary>
        public void FillOrderNumber()
        {
            ArrayList ordernumber = new ArrayList();
            ArrayList displayOrderNum = new ArrayList();
            Hashtable uniqueOrdernum = new Hashtable();
            ordernumber = LovSingleton.GetInstance().GetRootList(null, "FPASS_COWORKER", "CWR_ORDERNO");

            foreach (LovItem lovi in ordernumber)
            {
                if (lovi.ItemValue != null && !lovi.ItemValue.Equals(String.Empty))
                {
                    if (null == uniqueOrdernum[lovi.ItemValue])
                    {
                        uniqueOrdernum.Add(lovi.ItemValue, lovi);
                    }
                }
            }
            displayOrderNum.AddRange(uniqueOrdernum.Values);
            displayOrderNum.Add(new LovItem("0", ""));
            displayOrderNum.Reverse();
            CboOrderNumber.DataSource = displayOrderNum;
            CboOrderNumber.DisplayMember = "ItemValue";
            CboOrderNumber.ValueMember = "DecId";
        }

        public void FillCraft()
        {
            ArrayList craft = new ArrayList();
            craft = LovSingleton.GetInstance().GetRootList(null, "FPASS_CRAFT", "CRA_CRAFTNOTATION");
            craft.Add(new LovItem("0", ""));
            craft.Reverse();
            CboCraft.DataSource = craft;
            CboCraft.DisplayMember = "ItemValue";
            CboCraft.ValueMember = "Id";
        }

        public void FillPlant()
        {
            ArrayList plant = new ArrayList();
            plant = LovSingleton.GetInstance().GetRootList(null, "FPASS_PLANT", "PL_NAME");
            plant.Add(new LovItem("0", ""));
            plant.Reverse();
            CboPlant.DataSource = plant;
            CboPlant.DisplayMember = "ItemValue";
            CboPlant.ValueMember = "Id";
        }

        public void FillDepartment()
        {
            ArrayList department = new ArrayList();
            department = LovSingleton.GetInstance().GetRootList(null, "FPASS_DEPARTMENT", "DEPT_DEPARTMENT");
            department.Add(new LovItem("0", ""));
            department.Reverse();
            CboDepartment.DataSource = department;
            CboDepartment.DisplayMember = "ItemValue";
            CboDepartment.ValueMember = "Id";
        }
        #endregion

        /// <summary>
        /// Adds available reports to selection list if current user is authorised to view them
        /// </summary>
        private void SetAuthorization()
        {
            ArrayList reportNames = new ArrayList();
            reportNames.Add(String.Empty);

            if (UserManagementControl.getInstance().GetAuthorization(
                UserManagementControl.REPORTS_CWR_ALL_DATA))
            {
                reportNames.Add(ReportNames.CWR_ALL_DATA);
            }
            if (UserManagementControl.getInstance().GetAuthorization(
                UserManagementControl.REPORTS_CHECKLIST))
            {
                if (UserManagementControl.getInstance().CurrentBOUserMndt.HasZKS)
                {
                    reportNames.Add(ReportNames.CHECKLIST);
                }
            }
            if (UserManagementControl.getInstance().GetAuthorization(
                UserManagementControl.REPORTS_PLANTS))
            {
                reportNames.Add(ReportNames.PLANTS);
            }
            if (UserManagementControl.getInstance().GetAuthorization(
                UserManagementControl.REPORTS_EXCONTRACTOR_WITH_COORD))
            {
                reportNames.Add(ReportNames.EXCO_COORDINATOR);
            }
            if (UserManagementControl.getInstance().GetAuthorization(
                UserManagementControl.REPORTS_CWR_BOOKINGS))
            {
                if (UserManagementControl.getInstance().CurrentBOUserMndt.HasZKS)
                {
                    reportNames.Add(ReportNames.CWR_BOOKINGS);
                }
            }
            if (UserManagementControl.getInstance().GetAuthorization(
                UserManagementControl.REPORTS_EXCO_BOOKINGS_SUM))
            {
                if (UserManagementControl.getInstance().CurrentBOUserMndt.HasZKS)
                {
                    reportNames.Add(ReportNames.EXCO_BOOKINGS_SUM);
                }
            }
            if (UserManagementControl.getInstance().GetAuthorization(
                UserManagementControl.REPORTS_CWR_BOOKINGS_EXCO))
            {
                if (UserManagementControl.getInstance().CurrentBOUserMndt.HasZKS)
                {
                    reportNames.Add(ReportNames.CWR_BOOKINGS_EXCO);
                }
            }
            if (UserManagementControl.getInstance().GetAuthorization(
                UserManagementControl.REPORTS_CWRATTENDANCEDETAIL))
            {
                if (UserManagementControl.getInstance().CurrentBOUserMndt.HasZKS)
                {
                    reportNames.Add(ReportNames.CWR_ATTEND_DETAIL);
                }
            }
            if (UserManagementControl.getInstance().GetAuthorization(
                UserManagementControl.REPORTS_CWRATTENDANCE))
            {
                if (UserManagementControl.getInstance().CurrentBOUserMndt.HasZKS)
                {
                    reportNames.Add(ReportNames.CWR_ATTENDANCE);
                }
            }
            if (UserManagementControl.getInstance().GetAuthorization(
                UserManagementControl.REPORTS_EXCO_ATTENDANCE))
            {
                if (UserManagementControl.getInstance().CurrentBOUserMndt.HasZKS)
                {
                    reportNames.Add(ReportNames.EXCO_ATTENDANCE);
                }
            }
            if (UserManagementControl.getInstance().GetAuthorization(
                UserManagementControl.REPORTS_EXPIRYDATE))
            {
                reportNames.Add(ReportNames.CWR_EXPIRYDATE);
            }
            if (UserManagementControl.getInstance().GetAuthorization(
                UserManagementControl.REPORTS_FFMABOOKINGSINCE))
            {
                if (UserManagementControl.getInstance().CurrentBOUserMndt.HasZKS)
                {
                    reportNames.Add(ReportNames.CWR_NO_BOOKING);
                }
            }
            if (UserManagementControl.getInstance().GetAuthorization(
                UserManagementControl.REPORTS_DELETELIST))
            {
                reportNames.Add(ReportNames.CWR_DELETELIST);
            }
            if (UserManagementControl.getInstance().GetAuthorization(
                UserManagementControl.REPORTS_PASS_TWO))
            {
                reportNames.Add(ReportNames.CWR_PASS);
            }
            if (UserManagementControl.getInstance().GetAuthorization(
                UserManagementControl.REPORTS_RESPMASKS))
            {
                reportNames.Add(ReportNames.RESPMASKS);
            }
            if (UserManagementControl.getInstance().GetAuthorization(
                UserManagementControl.REPORTS_CWR_CHANGEHIST))
            {
                reportNames.Add(ReportNames.CWR_CHANGEHIST);
            }

            CboReport.DataSource = reportNames;
        }

        #endregion // End of Methods

        #region Events

        /// <summary>
        /// Raised on click on "Zurück"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBackTo_Click(object sender, EventArgs e)
        {
            ((ReportsController)mController).HandleCloseDialog();
        }

        /// <summary>
        /// Raised when user selects a new value in dropdownlist: list of available reports
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mController != null)
            {
                ((ReportsController)mController).HandleReportValidationList();
            }
        }


        /// <summary>
        /// Raised when button "Suchen" is clicked: calls appropriate SQL search.
        /// Catches <see cref="OracleException"/> which can occur for strings with ' in as this is part of SQL string delimiter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    GetMyController().HandleSearch();
                }
                catch (OracleException oraex)
                {
                    // Error from SQL ' delimiter
                    if (oraex.Code == 01756)
                    {
                        Cursor = Cursors.Default;
                        throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.CANNOT_SEARCH_CHAR));
                    }
                    else
                    {
                        throw new UIFatalException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.FATAL_DB_ERROR) + oraex.Message);
                    }
                }
            }
            catch (UIWarningException uwe)
            {
                ExceptionProcessor.GetInstance().Process(uwe);
            }
        }


        /// <summary>
        /// Calls the event handler for the button "Report generieren" in the controller
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnGenerateReport_Click(object sender, EventArgs e)
        {
            string prmReportName = CboReport.Text;

            try
            {               
                GetMyController().HandleGenerateReport();
            }
            catch (UIWarningException uwe)
            {
                ExceptionProcessor.GetInstance().Process(uwe);
            }
        }

        /// <summary> 
        /// Raised for button "Export generieren",
        /// calls event handler in the controller
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnGenerateExport_Click(object sender, EventArgs e)
        {
            try
            {
                GetMyController().HandlePreGenerateExport();
            }
            catch (UIWarningException uwe)
            {
                ExceptionProcessor.GetInstance().Process(uwe);
            }
            catch (Exception ex)
            {
                ExceptionProcessor.GetInstance().Process(ex);
            }
        }


        /// <summary>
        /// Raised when user clicks OK in save dialogue
        /// Generates export proper
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                // uses given file + path, completes suggested csv file name with extension
                FpassReportSingleton.GetInstance().ExportParameters.FileName = saveFileDialog.FileName;
                GetMyController().HandleGenerateExport();
            }
            catch (UIWarningException uwe)
            {
                ExceptionProcessor.GetInstance().Process(uwe);
            }
            catch (Exception ex)
            {
                ExceptionProcessor.GetInstance().Process(ex);
            }
        }

        /// <summary>
        /// Leave event for Date Time Picker: makes sure date from is smaller then date until
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>		
        private void TxtDeliveryDateFrom_Leave(object sender, EventArgs e)
        {
            try
            {
                GetMyController().HandleDeliveryDates(TxtDeliveryDateFrom.Text.Trim(), TxtDeliveryDateUntil.Text.Trim());
            }
            catch (UIWarningException uwe)
            {
                ExceptionProcessor.GetInstance().Process(uwe);
                TxtDeliveryDateFrom.Focus();
            }
        }

        /// <summary>
        /// Leave event for Date Time Picker: makes sure date from is smaller then date until
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtDeliveryDateUntil_Leave(object sender, EventArgs e)
        {
            try
            {
                GetMyController().HandleDeliveryDates(TxtDeliveryDateFrom.Text.Trim(), TxtDeliveryDateUntil.Text.Trim());
            }
            catch (UIWarningException uwe)
            {
                ExceptionProcessor.GetInstance().Process(uwe);
                TxtDeliveryDateUntil.Focus();
            }
        }

        /// <summary>
        /// Leave event. Checks that given text is a valid date and To is not earlier than From
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtValidityFrom_Leave(object sender, EventArgs e)
        {
            try
            {
                GetMyController().HandleDeliveryDates(TxtValidityFrom.Text.Trim(), TxtValidityUntil.Text.Trim());
            }
            catch (UIWarningException uwe)
            {
                ExceptionProcessor.GetInstance().Process(uwe);
                TxtValidityFrom.Focus();
            }
        }


        /// <summary>
        /// Leave event. Checks that given text is a valid date and To is not earlier than From
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtValidityUntil_Leave(object sender, EventArgs e)
        {
            try
            {
                GetMyController().HandleDeliveryDates(TxtValidityFrom.Text.Trim(), TxtValidityUntil.Text.Trim());
            }
            catch (UIWarningException uwe)
            {
                ExceptionProcessor.GetInstance().Process(uwe);
                TxtValidityUntil.Focus();
            }
        }

        /// <summary>
        /// Leave event for Date Time Picker (Buchungsdaten von): makes sure date from is smaller then date until
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatMonthUntil_Leave(object sender, EventArgs e)
        {
            if (DatMonthUntil.Text.Trim().Length > 0)
            {
                if (DatMonthFrom.Value.Date > DatMonthUntil.Value.Date)
                {
                    try
                    {
                        throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.DYNDATA_FROMTILLDATE));
                    }
                    catch (UIWarningException uwe)
                    {
                        ExceptionProcessor.GetInstance().Process(uwe);
                        DatMonthUntil.Focus();
                    }
                }
            }
        }

        /// <summary>
        /// Leave event for Date Time Picker (Buchungsdaten von): makes sure date from is smaller then date until
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatMonthFrom_Leave(object sender, EventArgs e)
        {
            if (DatMonthFrom.Text.Trim().Length > 0)
            {
                if (DatMonthFrom.Value.Date > DatMonthUntil.Value.Date)
                {
                    try
                    {
                        throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.DYNDATA_FROMTILLDATE));
                    }
                    catch (UIWarningException uwe)
                    {
                        ExceptionProcessor.GetInstance().Process(uwe);
                        DatMonthUntil.Focus();
                        DatMonthUntil.Value = DatMonthFrom.Value;
                    }
                }
            }
        }

        /// <summary>
        /// Leave event. Checks that given text is a valid date and To is not earlier than From
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtMaintenanceFrom_Leave(object sender, EventArgs e)
        {
            try
            {
                GetMyController().HandleDeliveryDates(TxtMaintenanceFrom.Text.Trim(), TxtMaintenanceUntil.Text.Trim());
            }
            catch (UIWarningException uwe)
            {
                ExceptionProcessor.GetInstance().Process(uwe);
                TxtValidityUntil.Focus();
            }
        }

        /// <summary>
        /// Leave event. Checks that given text is a valid date and To is not earlier than From
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtMaintenanceUntil_Leave(object sender, EventArgs e)
        {
            try
            {
                GetMyController().HandleDeliveryDates(TxtMaintenanceFrom.Text.Trim(), TxtMaintenanceUntil.Text.Trim());
            }
            catch (UIWarningException uwe)
            {
                ExceptionProcessor.GetInstance().Process(uwe);
                TxtValidityUntil.Focus();
            }
        }

        /// <summary>
        /// Raised when user clicks "Maske leeren"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClearMask_Click(object sender, EventArgs e)
        {
            GetMyController().HandleClearMask();
        }

        /// <summary>
        /// Opens a popup with a larger version of checklistbox "Fremdfirmen"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPopupExcos_Click(object sender, EventArgs e)
        {
            GetMyController().HandlePopupSearchExco();
        }

        private void BtnClearAcRadio_Click(object sender, EventArgs e)
        {
            GetMyController().HandleAccessFields();
        }

        #region EventsMehrfachFF


        /// <summary>
        /// FPASS V5: 
        /// This and the following two event handlers are a big workround for when user presses spacebar to tick an item (its checkbox)
        /// Without the workaround the cursor always jumps back to the first item in the list (item 0), 
        /// some kind of re-load but have not been able to find out when or why this fires.
        /// This event gets the index of the item selected when the user pressed the spacebar.
        /// Both PreviewKeyDown and KeyUp required
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClbAttExContractor_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            mSelectedExcoId = ClbAttExContractor.SelectedIndex;
            if (e.KeyCode == Keys.Space)
            {
                ClbAttExContractor.SelectedIndex = mSelectedExcoId;
            }
        }


        /// <summary>
        /// FPASS V5: 
        /// This and the following two event handlers are a big workround for when user presses spacebar to tick an item (its checkbox)
        /// Without the workaround the cursor always jumps back to the first item in the list (item 0), 
        /// some kind of re-load but have not been able to find out when or why this fires.
        /// This event gets the index of the item selected when the user pressed the spacebar.
        /// Both PreviewKeyDown and KeyUp required
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClbAttExContractor_KeyUp(object sender, KeyEventArgs e)
        {
            mSelectedExcoId = ClbAttExContractor.SelectedIndex;
            if (e.KeyCode == Keys.Space)
            {
                ClbAttExContractor.SelectedIndex = mSelectedExcoId;
            }
        }

        /// <summary>
        /// Raised when item's check state is changed. 
        /// If cursor has jumped back to item 0 then put it back where it was when user pressed space
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClbAttExContractor_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (ClbAttExContractor.SelectedIndex == 0)
            {
                // This does not change item 0's checkstate
                // so commented out
                //this.ClbAttExContractor.ItemCheck -= new System.Windows.Forms.ItemCheckEventHandler(this.ClbAttExContractor_ItemCheck);
                //ClbAttExContractor.SetItemCheckState(0, CheckState.Unchecked);
                //this.ClbAttExContractor.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ClbAttExContractor_ItemCheck);
                ClbAttExContractor.SelectedIndex = mSelectedExcoId;
            }
        }

        #endregion EventsMehrfachFF

       
        #endregion Events
    }
    
}

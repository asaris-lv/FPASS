using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using de.pta.Component.Errorhandling;
using de.pta.Component.ListOfValues;
using Degussa.FPASS.Util.ListOfValues;
using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.Util.UserManagement;
using Degussa.FPASS.InterfaceZks;
using Degussa.FPASS.Util.Enums;
using Degussa.FPASS.FPASSApplication;

namespace Degussa.FPASS.Gui.Dialog.Pass
{
	/// <summary>
	/// A FrmSummaryCoWorker is the view of the MVC-triad SummaryModel,
	/// SummaryController and FrmSummaryCoWorker.
	/// FrmSummaryCoWorker extends from the FPASSBaseView.
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
	public class FrmSummaryCoWorker : FPASSBaseView
	{
		#region Members

		//panels
		internal Panel PnlSearch;

		//labels
		internal Label LblSearchExternalContractor;
		internal Label LblSearchFirstname;
		internal Label LblSearchCoordinator;
		internal Label LblSearchSupervisor;
		internal Label LblSearchSubcontractor;
		internal Label LblSearch;
		internal Label LblMaskTitle;
		internal Label LblSearchSurname;

		//textboxes
		internal TextBox TxtSearchFirstname;
		internal TextBox TxtSearchSurname;

		//comboboxes
		internal ComboBox CboSearchCoordinator;
		internal ComboBox CboSearchExternalContractor;
		internal ComboBox CboSearchSubcontractor;
		internal ComboBox CboSearchSupervisor;

		//buttons
		internal Button BtnExtendedSearch;
		internal Button BtnRegisterDetails;
		internal Button BtnEdit;
		internal Button BtnPass;
        internal Button BtnHelp;
		internal Button BtnExit;
		internal Button BtnSearchExternalContractor;
		internal Button BtnSearch;

		//tooltips
		private ToolTip TooSearchExContractor;
		private ToolTip TooSearch;
		private ToolTip TooExtendedSearch;
		private ToolTip TooSearchZKS;
		private ToolTip TooPass;
		private ToolTip TooProcess;
		private ToolTip TooRegisterDetails;
		private ToolTip TooExit;

		//Tables
		internal DataGridTableStyle DgrTableStyleSummary;
		public DataGrid DgrCoWorker;
		internal DataTable TblSaetze = new DataTable();
		internal DataGridTextBoxColumn DgrTextBoxExternalContractor;
		internal DataGridTextBoxColumn DgrTextBoxSubcontractor;
		internal DataGridTextBoxColumn DgrTextBoxSurname;
		internal DataGridTextBoxColumn DgrTextBoxFirstname;
		internal DataGridTextBoxColumn DgrTextBoxDateOfBirth;
		internal DataGridTextBoxColumn DgrTextBoxSupervisor;
		internal DataGridTextBoxColumn DgrTextBoxCoWorkerID;
		internal DataGridTextBoxColumn DgrTextBoxStatus;
		internal DataGridTextBoxColumn DgrTextBoxZKSCode;
		internal DataGridTextBoxColumn DgrTextBoxCoordinator;

		//access authorization
		internal		bool				mPlantAuthorization;
		internal		bool				mSafetyAtWorkAuthorization;
		internal		bool				mSiteFireAuthorization;
		internal		bool				mMedicalServiceAuthorization;
		internal		bool				mTechDepartmentAuthorization;
		internal		bool				mSiteSecurityAuthorization;
		internal		bool				mSiteSecurityLeaderAuthorization;
		internal		bool				mSysAdminAuthorization;
		internal		bool				mEdvAdminAuthorization;
		internal		bool				mReceptionAuthorization;
		internal		bool				mCoordinatorAuthorization;

		//Other
		private IContainer components;
		private MenuItem MnuRefreshSummary;

		/// <summary>
		/// holds id of current Coworker value object selected in datagrid
		/// </summary>
		private decimal mCurrentFFMAId;

		/// <summary>
		/// Is application currently starting? (form loaded for first time)
		/// </summary>
		private bool	mApplicationStartUp;
		
		/// <summary>
		/// Used to stop Paint event on datagrid firing when grid or form is being loaded
		/// </summary>
        private bool gridIsLoading = true;
        private GroupBox gbDummy;
        internal RadioButton RbtSearchMifare;
        internal RadioButton RbtSearchHitag;
        internal Button BtnSearchUSB;
        internal TextBox TxtSearchIDCard;
        internal Button BtnSearchZKS;
        internal Label label1;
        internal Label LblIdCardNr;
        private ToolTip TooSearchUSB;
        private Button BtnTestZKS;
		/// <summary>
		/// 25.05.04: Used to store which line in datagrid is selected (NOT CWR ID!)
		/// </summary>
		private int     mCurrentRowIndex;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor
		/// Fill all comboboxes
		/// Set authorization flags for the appl roles (what is user allowed to do?)
		/// Authorizations on GUI controls set as each form loaded 
		/// </summary>
		public FrmSummaryCoWorker()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			
			Initialize();
			
			FillLists();

			// Set flags
			SafetyAtWorkAuthorization();
			SiteFireAuthorization();
			SiteSecurityAuthorization();
			SiteSecurityLeaderAuthorization();
			MedicalServiceAuthorization();
			PlantAuthorization();
			TechDepartmentAuthorization();
			SysAdminAuthorization();
			EdvAdminAuthorization();
			ReceptionAuthorization();
			CoordinatorAuthorization();

			// Set authorizations on current form
			SetAuthorization();
		}

		#endregion //End of Constructors

		#region Initialization

		/// <summary>
		/// Initializes the members.
		/// No coworker should currently be selected
		/// </summary>
		private void Initialize()
		{
			mApplicationStartUp = true;
			mCurrentFFMAId      = -1;
			mCurrentRowIndex    = -1;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSummaryCoWorker));
            this.DgrCoWorker = new System.Windows.Forms.DataGrid();
            this.DgrTableStyleSummary = new System.Windows.Forms.DataGridTableStyle();
            this.DgrTextBoxCoWorkerID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxStatus = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxSurname = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxFirstname = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxDateOfBirth = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxExternalContractor = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxSubcontractor = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxCoordinator = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxSupervisor = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxZKSCode = new System.Windows.Forms.DataGridTextBoxColumn();
            this.LblMaskTitle = new System.Windows.Forms.Label();
            this.BtnSearch = new System.Windows.Forms.Button();
            this.LblSearchSurname = new System.Windows.Forms.Label();
            this.LblSearchExternalContractor = new System.Windows.Forms.Label();
            this.LblSearchFirstname = new System.Windows.Forms.Label();
            this.CboSearchCoordinator = new System.Windows.Forms.ComboBox();
            this.TxtSearchSurname = new System.Windows.Forms.TextBox();
            this.LblSearchCoordinator = new System.Windows.Forms.Label();
            this.TxtSearchFirstname = new System.Windows.Forms.TextBox();
            this.CboSearchExternalContractor = new System.Windows.Forms.ComboBox();
            this.CboSearchSubcontractor = new System.Windows.Forms.ComboBox();
            this.LblSearchSupervisor = new System.Windows.Forms.Label();
            this.LblSearchSubcontractor = new System.Windows.Forms.Label();
            this.PnlSearch = new System.Windows.Forms.Panel();
            this.BtnTestZKS = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.LblIdCardNr = new System.Windows.Forms.Label();
            this.RbtSearchMifare = new System.Windows.Forms.RadioButton();
            this.RbtSearchHitag = new System.Windows.Forms.RadioButton();
            this.BtnSearchUSB = new System.Windows.Forms.Button();
            this.TxtSearchIDCard = new System.Windows.Forms.TextBox();
            this.BtnSearchZKS = new System.Windows.Forms.Button();
            this.gbDummy = new System.Windows.Forms.GroupBox();
            this.BtnSearchExternalContractor = new System.Windows.Forms.Button();
            this.CboSearchSupervisor = new System.Windows.Forms.ComboBox();
            this.BtnExtendedSearch = new System.Windows.Forms.Button();
            this.LblSearch = new System.Windows.Forms.Label();
            this.BtnRegisterDetails = new System.Windows.Forms.Button();
            this.BtnEdit = new System.Windows.Forms.Button();
            this.BtnPass = new System.Windows.Forms.Button();
            this.BtnExit = new System.Windows.Forms.Button();
            this.BtnHelp = new System.Windows.Forms.Button();
            this.TooSearchExContractor = new System.Windows.Forms.ToolTip(this.components);
            this.TooSearch = new System.Windows.Forms.ToolTip(this.components);
            this.TooExtendedSearch = new System.Windows.Forms.ToolTip(this.components);
            this.TooSearchZKS = new System.Windows.Forms.ToolTip(this.components);
            this.TooPass = new System.Windows.Forms.ToolTip(this.components);
            this.TooProcess = new System.Windows.Forms.ToolTip(this.components);
            this.TooRegisterDetails = new System.Windows.Forms.ToolTip(this.components);
            this.TooExit = new System.Windows.Forms.ToolTip(this.components);
            this.MnuRefreshSummary = new System.Windows.Forms.MenuItem();
            this.TooSearchUSB = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.SbpMessage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sbpenvironment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgrCoWorker)).BeginInit();
            this.PnlSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // DgrCoWorker
            // 
            this.DgrCoWorker.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DgrCoWorker.CaptionBackColor = System.Drawing.Color.SteelBlue;
            this.DgrCoWorker.CaptionFont = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DgrCoWorker.CaptionForeColor = System.Drawing.Color.White;
            this.DgrCoWorker.CaptionText = "Fremdfirmenmitarbeiter";
            this.DgrCoWorker.DataMember = "";
            this.DgrCoWorker.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DgrCoWorker.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DgrCoWorker.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.DgrCoWorker.Location = new System.Drawing.Point(16, 203);
            this.DgrCoWorker.Name = "DgrCoWorker";
            this.DgrCoWorker.ReadOnly = true;
            this.DgrCoWorker.Size = new System.Drawing.Size(1242, 653);
            this.DgrCoWorker.TabIndex = 11;
            this.DgrCoWorker.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.DgrTableStyleSummary});
            this.DgrCoWorker.Paint += new System.Windows.Forms.PaintEventHandler(this.DgrCoWorker_Paint);
            this.DgrCoWorker.DoubleClick += new System.EventHandler(this.DgrCoWorker_DoubleClick);
            // 
            // DgrTableStyleSummary
            // 
            this.DgrTableStyleSummary.DataGrid = this.DgrCoWorker;
            this.DgrTableStyleSummary.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.DgrTextBoxCoWorkerID,
            this.DgrTextBoxStatus,
            this.DgrTextBoxSurname,
            this.DgrTextBoxFirstname,
            this.DgrTextBoxDateOfBirth,
            this.DgrTextBoxExternalContractor,
            this.DgrTextBoxSubcontractor,
            this.DgrTextBoxCoordinator,
            this.DgrTextBoxSupervisor,
            this.DgrTextBoxZKSCode});
            this.DgrTableStyleSummary.HeaderFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DgrTableStyleSummary.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.DgrTableStyleSummary.MappingName = "RTTabCoWorker";
            this.DgrTableStyleSummary.SelectionForeColor = System.Drawing.SystemColors.ControlLightLight;
            // 
            // DgrTextBoxCoWorkerID
            // 
            this.DgrTextBoxCoWorkerID.Format = "";
            this.DgrTextBoxCoWorkerID.FormatInfo = null;
            this.DgrTextBoxCoWorkerID.HeaderText = "CWR_ID";
            this.DgrTextBoxCoWorkerID.MappingName = "FFMAID";
            this.DgrTextBoxCoWorkerID.Width = 1;
            // 
            // DgrTextBoxStatus
            // 
            this.DgrTextBoxStatus.Format = "";
            this.DgrTextBoxStatus.FormatInfo = null;
            this.DgrTextBoxStatus.HeaderText = "Status";
            this.DgrTextBoxStatus.MappingName = "Status";
            this.DgrTextBoxStatus.NullText = "";
            this.DgrTextBoxStatus.Width = 80;
            // 
            // DgrTextBoxSurname
            // 
            this.DgrTextBoxSurname.Format = "";
            this.DgrTextBoxSurname.FormatInfo = null;
            this.DgrTextBoxSurname.HeaderText = "Nachname";
            this.DgrTextBoxSurname.MappingName = "Surname";
            this.DgrTextBoxSurname.NullText = "";
            this.DgrTextBoxSurname.Width = 148;
            // 
            // DgrTextBoxFirstname
            // 
            this.DgrTextBoxFirstname.Format = "";
            this.DgrTextBoxFirstname.FormatInfo = null;
            this.DgrTextBoxFirstname.HeaderText = "Vorname";
            this.DgrTextBoxFirstname.MappingName = "Firstname";
            this.DgrTextBoxFirstname.NullText = "";
            this.DgrTextBoxFirstname.Width = 148;
            // 
            // DgrTextBoxDateOfBirth
            // 
            this.DgrTextBoxDateOfBirth.Format = "";
            this.DgrTextBoxDateOfBirth.FormatInfo = null;
            this.DgrTextBoxDateOfBirth.HeaderText = "Geb.-Datum";
            this.DgrTextBoxDateOfBirth.MappingName = "DateOfBirth";
            this.DgrTextBoxDateOfBirth.NullText = "";
            this.DgrTextBoxDateOfBirth.Width = 90;
            // 
            // DgrTextBoxExternalContractor
            // 
            this.DgrTextBoxExternalContractor.Format = "";
            this.DgrTextBoxExternalContractor.FormatInfo = null;
            this.DgrTextBoxExternalContractor.HeaderText = "Fremdfirma";
            this.DgrTextBoxExternalContractor.MappingName = "ExContractorName";
            this.DgrTextBoxExternalContractor.NullText = "";
            this.DgrTextBoxExternalContractor.Width = 150;
            // 
            // DgrTextBoxSubcontractor
            // 
            this.DgrTextBoxSubcontractor.Format = "";
            this.DgrTextBoxSubcontractor.FormatInfo = null;
            this.DgrTextBoxSubcontractor.HeaderText = "Subfirma";
            this.DgrTextBoxSubcontractor.MappingName = "SubContractor";
            this.DgrTextBoxSubcontractor.NullText = "";
            this.DgrTextBoxSubcontractor.Width = 150;
            // 
            // DgrTextBoxCoordinator
            // 
            this.DgrTextBoxCoordinator.Format = "";
            this.DgrTextBoxCoordinator.FormatInfo = null;
            this.DgrTextBoxCoordinator.HeaderText = "Koordinator";
            this.DgrTextBoxCoordinator.MappingName = "CoordNameAndTel";
            this.DgrTextBoxCoordinator.NullText = "";
            this.DgrTextBoxCoordinator.Width = 210;
            // 
            // DgrTextBoxSupervisor
            // 
            this.DgrTextBoxSupervisor.Format = "";
            this.DgrTextBoxSupervisor.FormatInfo = null;
            this.DgrTextBoxSupervisor.HeaderText = "Baustellenleiter";
            this.DgrTextBoxSupervisor.MappingName = "SuperNameAndTel";
            this.DgrTextBoxSupervisor.NullText = "";
            this.DgrTextBoxSupervisor.Width = 190;
            // 
            // DgrTextBoxZKSCode
            // 
            this.DgrTextBoxZKSCode.Format = "";
            this.DgrTextBoxZKSCode.FormatInfo = null;
            this.DgrTextBoxZKSCode.HeaderText = "ZKS";
            this.DgrTextBoxZKSCode.MappingName = "ZKSReturncode";
            this.DgrTextBoxZKSCode.Width = 40;
            // 
            // LblMaskTitle
            // 
            this.LblMaskTitle.BackColor = System.Drawing.Color.White;
            this.LblMaskTitle.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMaskTitle.Location = new System.Drawing.Point(534, 5);
            this.LblMaskTitle.Name = "LblMaskTitle";
            this.LblMaskTitle.Size = new System.Drawing.Size(256, 32);
            this.LblMaskTitle.TabIndex = 19;
            this.LblMaskTitle.Text = "FPASS - Übersicht";
            // 
            // BtnSearch
            // 
            this.BtnSearch.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSearch.Location = new System.Drawing.Point(1075, 13);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(145, 30);
            this.BtnSearch.TabIndex = 14;
            this.BtnSearch.Text = "&Suchen";
            this.TooSearch.SetToolTip(this.BtnSearch, "Löst den Suchvorgang aus");
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // LblSearchSurname
            // 
            this.LblSearchSurname.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSearchSurname.Location = new System.Drawing.Point(364, 21);
            this.LblSearchSurname.Name = "LblSearchSurname";
            this.LblSearchSurname.Size = new System.Drawing.Size(72, 16);
            this.LblSearchSurname.TabIndex = 10;
            this.LblSearchSurname.Text = "Nachname";
            // 
            // LblSearchExternalContractor
            // 
            this.LblSearchExternalContractor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSearchExternalContractor.Location = new System.Drawing.Point(11, 58);
            this.LblSearchExternalContractor.Name = "LblSearchExternalContractor";
            this.LblSearchExternalContractor.Size = new System.Drawing.Size(72, 16);
            this.LblSearchExternalContractor.TabIndex = 5;
            this.LblSearchExternalContractor.Text = "Fremdfirma";
            // 
            // LblSearchFirstname
            // 
            this.LblSearchFirstname.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSearchFirstname.Location = new System.Drawing.Point(364, 58);
            this.LblSearchFirstname.Name = "LblSearchFirstname";
            this.LblSearchFirstname.Size = new System.Drawing.Size(72, 16);
            this.LblSearchFirstname.TabIndex = 11;
            this.LblSearchFirstname.Text = "Vorname";
            // 
            // CboSearchCoordinator
            // 
            this.CboSearchCoordinator.AccessibleRole = System.Windows.Forms.AccessibleRole.List;
            this.CboSearchCoordinator.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboSearchCoordinator.ItemHeight = 15;
            this.CboSearchCoordinator.Location = new System.Drawing.Point(95, 18);
            this.CboSearchCoordinator.Name = "CboSearchCoordinator";
            this.CboSearchCoordinator.Size = new System.Drawing.Size(210, 23);
            this.CboSearchCoordinator.TabIndex = 1;
            // 
            // TxtSearchSurname
            // 
            this.TxtSearchSurname.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSearchSurname.Location = new System.Drawing.Point(439, 18);
            this.TxtSearchSurname.Name = "TxtSearchSurname";
            this.TxtSearchSurname.Size = new System.Drawing.Size(210, 21);
            this.TxtSearchSurname.TabIndex = 4;
            // 
            // LblSearchCoordinator
            // 
            this.LblSearchCoordinator.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSearchCoordinator.Location = new System.Drawing.Point(11, 21);
            this.LblSearchCoordinator.Name = "LblSearchCoordinator";
            this.LblSearchCoordinator.Size = new System.Drawing.Size(72, 16);
            this.LblSearchCoordinator.TabIndex = 7;
            this.LblSearchCoordinator.Text = "Koordinator";
            // 
            // TxtSearchFirstname
            // 
            this.TxtSearchFirstname.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSearchFirstname.Location = new System.Drawing.Point(439, 55);
            this.TxtSearchFirstname.Name = "TxtSearchFirstname";
            this.TxtSearchFirstname.Size = new System.Drawing.Size(210, 21);
            this.TxtSearchFirstname.TabIndex = 5;
            // 
            // CboSearchExternalContractor
            // 
            this.CboSearchExternalContractor.AccessibleRole = System.Windows.Forms.AccessibleRole.List;
            this.CboSearchExternalContractor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboSearchExternalContractor.ItemHeight = 15;
            this.CboSearchExternalContractor.Location = new System.Drawing.Point(95, 55);
            this.CboSearchExternalContractor.Name = "CboSearchExternalContractor";
            this.CboSearchExternalContractor.Size = new System.Drawing.Size(210, 23);
            this.CboSearchExternalContractor.TabIndex = 2;
            // 
            // CboSearchSubcontractor
            // 
            this.CboSearchSubcontractor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboSearchSubcontractor.ItemHeight = 15;
            this.CboSearchSubcontractor.Location = new System.Drawing.Point(785, 55);
            this.CboSearchSubcontractor.Name = "CboSearchSubcontractor";
            this.CboSearchSubcontractor.Size = new System.Drawing.Size(210, 23);
            this.CboSearchSubcontractor.TabIndex = 7;
            // 
            // LblSearchSupervisor
            // 
            this.LblSearchSupervisor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSearchSupervisor.Location = new System.Drawing.Point(684, 20);
            this.LblSearchSupervisor.Name = "LblSearchSupervisor";
            this.LblSearchSupervisor.Size = new System.Drawing.Size(250, 21);
            this.LblSearchSupervisor.TabIndex = 15;
            this.LblSearchSupervisor.Text = "Baustellenleiter";
            // 
            // LblSearchSubcontractor
            // 
            this.LblSearchSubcontractor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSearchSubcontractor.Location = new System.Drawing.Point(684, 58);
            this.LblSearchSubcontractor.Name = "LblSearchSubcontractor";
            this.LblSearchSubcontractor.Size = new System.Drawing.Size(250, 21);
            this.LblSearchSubcontractor.TabIndex = 8;
            this.LblSearchSubcontractor.Text = "Subfirma";
            // 
            // PnlSearch
            // 
            this.PnlSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlSearch.Controls.Add(this.BtnTestZKS);
            this.PnlSearch.Controls.Add(this.label1);
            this.PnlSearch.Controls.Add(this.LblIdCardNr);
            this.PnlSearch.Controls.Add(this.RbtSearchMifare);
            this.PnlSearch.Controls.Add(this.RbtSearchHitag);
            this.PnlSearch.Controls.Add(this.BtnSearchUSB);
            this.PnlSearch.Controls.Add(this.TxtSearchIDCard);
            this.PnlSearch.Controls.Add(this.BtnSearchZKS);
            this.PnlSearch.Controls.Add(this.gbDummy);
            this.PnlSearch.Controls.Add(this.BtnSearchExternalContractor);
            this.PnlSearch.Controls.Add(this.CboSearchSupervisor);
            this.PnlSearch.Controls.Add(this.BtnExtendedSearch);
            this.PnlSearch.Controls.Add(this.BtnSearch);
            this.PnlSearch.Controls.Add(this.LblSearchSurname);
            this.PnlSearch.Controls.Add(this.LblSearchExternalContractor);
            this.PnlSearch.Controls.Add(this.LblSearchFirstname);
            this.PnlSearch.Controls.Add(this.CboSearchCoordinator);
            this.PnlSearch.Controls.Add(this.TxtSearchSurname);
            this.PnlSearch.Controls.Add(this.LblSearchCoordinator);
            this.PnlSearch.Controls.Add(this.TxtSearchFirstname);
            this.PnlSearch.Controls.Add(this.CboSearchExternalContractor);
            this.PnlSearch.Controls.Add(this.CboSearchSubcontractor);
            this.PnlSearch.Controls.Add(this.LblSearchSupervisor);
            this.PnlSearch.Controls.Add(this.LblSearchSubcontractor);
            this.PnlSearch.Location = new System.Drawing.Point(16, 55);
            this.PnlSearch.Name = "PnlSearch";
            this.PnlSearch.Size = new System.Drawing.Size(1242, 139);
            this.PnlSearch.TabIndex = 0;
            // 
            // BtnTestZKS
            // 
            this.BtnTestZKS.Location = new System.Drawing.Point(95, 102);
            this.BtnTestZKS.Name = "BtnTestZKS";
            this.BtnTestZKS.Size = new System.Drawing.Size(134, 23);
            this.BtnTestZKS.TabIndex = 85;
            this.BtnTestZKS.Text = "Aufruf ZKS dll testen";
            this.BtnTestZKS.UseVisualStyleBackColor = true;
            this.BtnTestZKS.Visible = false;
            this.BtnTestZKS.Click += new System.EventHandler(this.BtnTestZKS_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(684, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 21);
            this.label1.TabIndex = 84;
            this.label1.Text = "Ausweistyp";
            // 
            // LblIdCardNr
            // 
            this.LblIdCardNr.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblIdCardNr.Location = new System.Drawing.Point(364, 105);
            this.LblIdCardNr.Name = "LblIdCardNr";
            this.LblIdCardNr.Size = new System.Drawing.Size(72, 21);
            this.LblIdCardNr.TabIndex = 83;
            this.LblIdCardNr.Text = "Ausweisnr.";
            // 
            // RbtSearchMifare
            // 
            this.RbtSearchMifare.AutoSize = true;
            this.RbtSearchMifare.Location = new System.Drawing.Point(845, 104);
            this.RbtSearchMifare.Name = "RbtSearchMifare";
            this.RbtSearchMifare.Size = new System.Drawing.Size(54, 17);
            this.RbtSearchMifare.TabIndex = 10;
            this.RbtSearchMifare.Text = "Mifare";
            this.RbtSearchMifare.UseVisualStyleBackColor = true;
            // 
            // RbtSearchHitag
            // 
            this.RbtSearchHitag.AutoSize = true;
            this.RbtSearchHitag.Checked = true;
            this.RbtSearchHitag.Location = new System.Drawing.Point(785, 104);
            this.RbtSearchHitag.Name = "RbtSearchHitag";
            this.RbtSearchHitag.Size = new System.Drawing.Size(56, 17);
            this.RbtSearchHitag.TabIndex = 9;
            this.RbtSearchHitag.TabStop = true;
            this.RbtSearchHitag.Text = "Hitag2";
            this.RbtSearchHitag.UseVisualStyleBackColor = true;
            // 
            // BtnSearchUSB
            // 
            this.BtnSearchUSB.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSearchUSB.Location = new System.Drawing.Point(1075, 101);
            this.BtnSearchUSB.Name = "BtnSearchUSB";
            this.BtnSearchUSB.Size = new System.Drawing.Size(145, 25);
            this.BtnSearchUSB.TabIndex = 12;
            this.BtnSearchUSB.Tag = "";
            this.BtnSearchUSB.Text = "USB-Leser";
            this.TooSearchUSB.SetToolTip(this.BtnSearchUSB, "Liest die Ausweisnummer über den USB-Leser und führt die Suche aus");
            this.BtnSearchUSB.Click += new System.EventHandler(this.BtnSearchUSB_Click);
            // 
            // TxtSearchIDCard
            // 
            this.TxtSearchIDCard.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSearchIDCard.Location = new System.Drawing.Point(438, 102);
            this.TxtSearchIDCard.Name = "TxtSearchIDCard";
            this.TxtSearchIDCard.Size = new System.Drawing.Size(210, 21);
            this.TxtSearchIDCard.TabIndex = 8;
            this.TxtSearchIDCard.TabStop = false;
            // 
            // BtnSearchZKS
            // 
            this.BtnSearchZKS.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSearchZKS.Location = new System.Drawing.Point(921, 101);
            this.BtnSearchZKS.Name = "BtnSearchZKS";
            this.BtnSearchZKS.Size = new System.Drawing.Size(145, 25);
            this.BtnSearchZKS.TabIndex = 11;
            this.BtnSearchZKS.Tag = "";
            this.BtnSearchZKS.Text = "ZKS-Terminal";
            this.TooSearchZKS.SetToolTip(this.BtnSearchZKS, "Liest die Ausweisnummer über den Ausweis-Terminal aus");
            this.BtnSearchZKS.Click += new System.EventHandler(this.BtnSearchZKS_Click);
            // 
            // gbDummy
            // 
            this.gbDummy.Location = new System.Drawing.Point(14, 88);
            this.gbDummy.Name = "gbDummy";
            this.gbDummy.Size = new System.Drawing.Size(1214, 3);
            this.gbDummy.TabIndex = 16;
            this.gbDummy.TabStop = false;
            // 
            // BtnSearchExternalContractor
            // 
            this.BtnSearchExternalContractor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSearchExternalContractor.Location = new System.Drawing.Point(313, 56);
            this.BtnSearchExternalContractor.Name = "BtnSearchExternalContractor";
            this.BtnSearchExternalContractor.Size = new System.Drawing.Size(21, 21);
            this.BtnSearchExternalContractor.TabIndex = 3;
            this.BtnSearchExternalContractor.Text = "?&F";
            this.TooSearchExContractor.SetToolTip(this.BtnSearchExternalContractor, "Öffnet die Maske Fremdfirmensuche");
            this.BtnSearchExternalContractor.Click += new System.EventHandler(this.BtnSearchExternalContractor_Click);
            // 
            // CboSearchSupervisor
            // 
            this.CboSearchSupervisor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboSearchSupervisor.ItemHeight = 15;
            this.CboSearchSupervisor.Location = new System.Drawing.Point(785, 18);
            this.CboSearchSupervisor.Name = "CboSearchSupervisor";
            this.CboSearchSupervisor.Size = new System.Drawing.Size(210, 23);
            this.CboSearchSupervisor.TabIndex = 6;
            // 
            // BtnExtendedSearch
            // 
            this.BtnExtendedSearch.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExtendedSearch.Location = new System.Drawing.Point(1075, 50);
            this.BtnExtendedSearch.Name = "BtnExtendedSearch";
            this.BtnExtendedSearch.Size = new System.Drawing.Size(145, 30);
            this.BtnExtendedSearch.TabIndex = 15;
            this.BtnExtendedSearch.Text = "Er&weiterte Suche";
            this.TooExtendedSearch.SetToolTip(this.BtnExtendedSearch, "Öffnet die Maske Erweiterte Suche");
            this.BtnExtendedSearch.Click += new System.EventHandler(this.BtnExtendedSearch_Click);
            // 
            // LblSearch
            // 
            this.LblSearch.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSearch.Location = new System.Drawing.Point(21, 47);
            this.LblSearch.Name = "LblSearch";
            this.LblSearch.Size = new System.Drawing.Size(48, 16);
            this.LblSearch.TabIndex = 18;
            this.LblSearch.Text = " Suche";
            // 
            // BtnRegisterDetails
            // 
            this.BtnRegisterDetails.Enabled = false;
            this.BtnRegisterDetails.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnRegisterDetails.Location = new System.Drawing.Point(946, 860);
            this.BtnRegisterDetails.Name = "BtnRegisterDetails";
            this.BtnRegisterDetails.Size = new System.Drawing.Size(150, 32);
            this.BtnRegisterDetails.TabIndex = 19;
            this.BtnRegisterDetails.Tag = "";
            this.BtnRegisterDetails.Text = "FFMA &Neu Erfassen";
            this.TooRegisterDetails.SetToolTip(this.BtnRegisterDetails, "Neuerfassung eines Fremdfirmenmitarbeiters");
            this.BtnRegisterDetails.Click += new System.EventHandler(this.BtnRegisterDetails_Click);
            // 
            // BtnEdit
            // 
            this.BtnEdit.Enabled = false;
            this.BtnEdit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnEdit.Location = new System.Drawing.Point(788, 860);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(150, 32);
            this.BtnEdit.TabIndex = 18;
            this.BtnEdit.Tag = "";
            this.BtnEdit.Text = "FFMA &Bearbeiten";
            this.TooProcess.SetToolTip(this.BtnEdit, "Bearbeiten des ausgewählten Fremdfirmenmitarbeiters");
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // BtnPass
            // 
            this.BtnPass.Enabled = false;
            this.BtnPass.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPass.Location = new System.Drawing.Point(630, 860);
            this.BtnPass.Name = "BtnPass";
            this.BtnPass.Size = new System.Drawing.Size(150, 32);
            this.BtnPass.TabIndex = 17;
            this.BtnPass.Tag = "";
            this.BtnPass.Text = "&Passierschein";
            this.TooPass.SetToolTip(this.BtnPass, "Druckt den Passierschein");
            this.BtnPass.Click += new System.EventHandler(this.BtnPass_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.Location = new System.Drawing.Point(1104, 860);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(150, 32);
            this.BtnExit.TabIndex = 20;
            this.BtnExit.Tag = "";
            this.BtnExit.Text = "Anw&endung beenden";
            this.TooExit.SetToolTip(this.BtnExit, "Anwendung beenden");
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnHelp
            // 
            this.BtnHelp.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnHelp.Location = new System.Drawing.Point(200, 664);
            this.BtnHelp.Name = "BtnHelp";
            this.BtnHelp.Size = new System.Drawing.Size(1, 1);
            this.BtnHelp.TabIndex = 25;
            this.BtnHelp.Tag = "";
            this.BtnHelp.Text = "&Hilfe";
            // 
            // MnuRefreshSummary
            // 
            this.MnuRefreshSummary.Index = -1;
            this.MnuRefreshSummary.Shortcut = System.Windows.Forms.Shortcut.F5;
            this.MnuRefreshSummary.Text = "Listen aktualisieren";
            this.MnuRefreshSummary.Click += new System.EventHandler(this.MnuRefreshSummary_Click);
            // 
            // FrmSummaryCoWorker
            // 
            this.AcceptButton = this.BtnSearch;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(1264, 959);
            this.Controls.Add(this.LblMaskTitle);
            this.Controls.Add(this.BtnHelp);
            this.Controls.Add(this.LblSearch);
            this.Controls.Add(this.PnlSearch);
            this.Controls.Add(this.DgrCoWorker);
            this.Controls.Add(this.BtnExit);
            this.Controls.Add(this.BtnEdit);
            this.Controls.Add(this.BtnPass);
            this.Controls.Add(this.BtnRegisterDetails);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmSummaryCoWorker";
            this.Text = "FPASS - Übersicht Fremdfirmenmitarbeiter";
            this.Activated += new System.EventHandler(this.FrmSummaryCoWorker_Activated);
            this.Controls.SetChildIndex(this.BtnRegisterDetails, 0);
            this.Controls.SetChildIndex(this.BtnPass, 0);
            this.Controls.SetChildIndex(this.BtnEdit, 0);
            this.Controls.SetChildIndex(this.BtnExit, 0);
            this.Controls.SetChildIndex(this.DgrCoWorker, 0);
            this.Controls.SetChildIndex(this.PnlSearch, 0);
            this.Controls.SetChildIndex(this.LblSearch, 0);
            this.Controls.SetChildIndex(this.BtnHelp, 0);
            this.Controls.SetChildIndex(this.LblBaseHead, 0);
            this.Controls.SetChildIndex(this.StbBase, 0);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            this.Controls.SetChildIndex(this.LblMaskTitle, 0);
            ((System.ComponentModel.ISupportInitialize)(this.SbpMessage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sbpenvironment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgrCoWorker)).EndInit();
            this.PnlSearch.ResumeLayout(false);
            this.PnlSearch.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		#region Accessors 

		/// <summary>
		/// Simple accessor: needed to trigger paint event from Controller (ie after Extended Search)
		/// </summary>
		internal bool GridIsLoading 
		{
			get 
			{
				return gridIsLoading;
			}
			set 
			{
				gridIsLoading = value;
			}
		} 

		/// <summary>
		/// 24.05.04: to access which row currently selected
		/// </summary>
		internal int CurrentRowIndex
		{
			get
			{
				return mCurrentRowIndex;
			}
			set
			{
				mCurrentRowIndex = value;
			}
		}

		#endregion //End of Accessors

		#region Methods 
		
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


		/// <summary>
		/// Called before form is hidden, as always
		/// </summary>
		internal override void PreHide()
		{
			this.SbpMessage.Text = null;
		}	

		
		/// <summary>
		/// Refill comboboxes and listboxes with default values
		/// </summary>
		internal override void FillLists()
		{
			FillCoordinator("0");
			FillExternalContractor("0");
			FillSupervisor();
			FillSubcontractor();
		}

		
		/// <summary>
		/// Refill combobox External Contractor 
		/// if returning to this form from External Contractor search (FrmExternalContractorSearch)
		/// A search is automatically carried out according to PK ID of current exco
		/// </summary>
		/// <param name="pContractorID"></param>
		internal override void ReFillContractorList(String pContractorID) 
		{
			GetMyController().SearchState = SummaryController.NORMAL_SEARCH_PERFORMED;
			FillCoordinator("0");
			FillExternalContractor("0");
			CboSearchExternalContractor.SelectedValue =  
				Convert.ToDecimal( pContractorID );
			this.BtnSearch_Click(this, null);
		}
		
	
		/// <summary>
		/// Clear all textfields and comboboxes used in search
		/// Disable buttons "Passierschein", "Bearbeiten" at foot of form: no coworker in scope
		/// </summary>
		internal void ClearFields()
		{
			this.CboSearchExternalContractor.SelectedValue = 0;
			this.CboSearchCoordinator.SelectedValue = 0;
			this.CboSearchSubcontractor.SelectedValue = 0;
			this.CboSearchSupervisor.SelectedValue = 0;
			this.TxtSearchFirstname.Text = String.Empty;
			this.TxtSearchSurname.Text = String.Empty;
			DgrCoWorker.DataSource = null;
			this.BtnPass.Enabled = false;
			this.BtnEdit.Enabled = false;
		}

		
		/// <summary>
		/// This form is the first to be shown: the "start form"
		/// Load authorizations for the various application roles: who is allowed to do what?
		/// This is implemented by enabling or disabling the controls (buttons etc) on the forms
		/// This form is always loaded, these methods are also called from elsewhere
		/// </summary>
		/// <returns>true if allowed, false if not</returns>
		internal bool PlantAuthorization()
		{
			mPlantAuthorization = UserManagementControl.getInstance().
				GetAuthorization(UserManagementControl.COWORKER_SUMMARY_BTN_REGISTER_DETAILS);

			return mPlantAuthorization;
		}

		internal bool SafetyAtWorkAuthorization()
		{
			mSafetyAtWorkAuthorization = UserManagementControl.getInstance().
				GetAuthorization(UserManagementControl.COWORKER_SUMMARY_BTN_REGISTER_DETAILS);

			return mSafetyAtWorkAuthorization;
		}

		internal bool SiteFireAuthorization()
		{
			mSiteFireAuthorization = UserManagementControl.getInstance().
				GetAuthorization(UserManagementControl.COWORKER_SUMMARY_BTN_REGISTER_DETAILS);

			return mSiteFireAuthorization;
		}

		internal bool MedicalServiceAuthorization()
		{
			mMedicalServiceAuthorization = UserManagementControl.getInstance().
				GetAuthorization(UserManagementControl.COWORKER_SUMMARY_BTN_REGISTER_DETAILS);

			return mMedicalServiceAuthorization;
		}

		internal bool TechDepartmentAuthorization()
		{
			mTechDepartmentAuthorization = UserManagementControl.getInstance().
				GetAuthorization(UserManagementControl.COWORKER_SUMMARY_BTN_REGISTER_DETAILS);

			return mTechDepartmentAuthorization;
		}

		internal bool SysAdminAuthorization()
		{
			mSysAdminAuthorization = UserManagementControl.getInstance().
				GetAuthorization(UserManagementControl.COWORKER_SUMMARY_BTN_REGISTER_DETAILS);

			return mSysAdminAuthorization;
		}

		internal bool EdvAdminAuthorization()
		{
			mEdvAdminAuthorization = UserManagementControl.getInstance().
				GetAuthorization(UserManagementControl.COWORKER_SUMMARY_BTN_REGISTER_DETAILS);

			return mEdvAdminAuthorization;
		}

		internal bool SiteSecurityAuthorization()
		{
			mSiteSecurityAuthorization = UserManagementControl.getInstance().
				GetAuthorization(UserManagementControl.COWORKER_SUMMARY_BTN_REGISTER_DETAILS);

			return mSiteSecurityAuthorization;
		}

		internal bool SiteSecurityLeaderAuthorization()
		{
			mSiteSecurityLeaderAuthorization = UserManagementControl.getInstance().
				GetAuthorization(UserManagementControl.COWORKER_SUMMARY_BTN_REGISTER_DETAILS);

			return mSiteSecurityLeaderAuthorization;
		}

		internal bool ReceptionAuthorization()
		{
			mReceptionAuthorization = UserManagementControl.getInstance().
				GetAuthorization(UserManagementControl.COWORKER_SUMMARY_BTN_REGISTER_DETAILS);

			return mReceptionAuthorization;
		}

		internal bool CoordinatorAuthorization()
		{
			mCoordinatorAuthorization = UserManagementControl.getInstance().
				GetAuthorization(UserManagementControl.COWORKER_SUMMARY_BTN_REGISTER_DETAILS);

			return mCoordinatorAuthorization;
		}

		
		/// <summary>
		/// fills combobox supervisor
		/// </summary>
		public void FillSupervisor()
		{
			ArrayList supervisor = new ArrayList(); 
			supervisor.Add(new LovItem("0", ""));
			supervisor.AddRange( LovSingleton.GetInstance().GetRootList(null, "FPASS_SUPERVISOR", "SUPERVISOR"));
			this.CboSearchSupervisor.DataSource = supervisor;
			this.CboSearchSupervisor.DisplayMember = "ItemValue";
			this.CboSearchSupervisor.ValueMember = "DecId";
		}

		
		/// <summary>
		/// fills combobox subcontractor
		/// </summary>
		public void FillSubcontractor()
		{
			this.CboSearchSubcontractor.DataSource = null;
			ArrayList subcontractor = new ArrayList();
			subcontractor.Add(new LovItem("0", ""));
			subcontractor.AddRange( LovSingleton.GetInstance().GetRootList(null, "FPASS_EXCONTRACTOR", "EXCO_NAME") );			
			this.CboSearchSubcontractor.DataSource = subcontractor;
			this.CboSearchSubcontractor.DisplayMember = "ItemValue";
			this.CboSearchSubcontractor.ValueMember = "DecId";
		}


		/// <summary>
		/// fills combobox externalContractor: valid and invalid excos shown
		/// </summary>
		/// <param name="pID"></param>
		public void FillExternalContractor(String pID)
		{
			this.CboSearchExternalContractor.DataSource = null;
			this.CboSearchExternalContractor.DataSource = FPASSLovsSingleton.GetInstance().GetValInvalContractors(0);
			this.CboSearchExternalContractor.DisplayMember = "ContractorName";
			this.CboSearchExternalContractor.ValueMember = "ContractorID";
			
		}

		
		/// <summary>
		/// fills combobox coordinator
		/// </summary>
		/// <param name="pID"></param>
		public void FillCoordinator(String pID)
		{
			this.CboSearchCoordinator.DataSource = null;
			this.CboSearchCoordinator.DataSource = this.GetCoordinatorList(pID);
			this.CboSearchCoordinator.DisplayMember = "CoordFullNameTel";
			this.CboSearchCoordinator.ValueMember = "CoordID";
		}

		/// <summary>
		/// New 24.05.04:
		/// after an automatic search has been carried out, the cursor should jump to the position of
		/// previously selected CWR.
		/// Do this by using the current row index of datagrid, no matter if CWR not same
		/// </summary>
		internal void JumpToCurrentCoWorker()
		{			
			this.DgrCoWorker.CurrentRowIndex = mCurrentRowIndex;
		}
		
		/// <summary>
		/// Get PK id of currently selected coworker
		/// by finding out which row in grid is selected: coworker PK is 0th column
		/// 24.05.04: also save current row index in datagrid to spring back to this coworker later
		/// </summary>
		private void TableNavigated()
		{
			//int rowIndex = this.DgrCoWorker.CurrentRowIndex;
			mCurrentRowIndex = this.DgrCoWorker.CurrentRowIndex;
			if ( -1 < mCurrentRowIndex )
			{
				mCurrentFFMAId = Convert.ToDecimal(this.DgrCoWorker[mCurrentRowIndex, 0].ToString());
			}
		}

		/// <summary>
		/// Get controller (MVC triad) to which this form belongs: the controller controls the flow of logic
		/// </summary>
		/// <returns></returns>
		private SummaryController GetMyController() 
		{
			return ((SummaryController)mController);
		}


		/// <summary>
		/// Which users are allowed to edit existing coworkers? Set button "FFMA bearbeiten" enabled or disabled
		/// </summary>
		private void SetAuthorization() 
		{
			this.Enabled = true;

			if (mSysAdminAuthorization || mEdvAdminAuthorization || mReceptionAuthorization || mCoordinatorAuthorization)
			{
				this.BtnRegisterDetails.Enabled = true;
			}
			else 
			{
				this.BtnRegisterDetails.Enabled = false;
			}
		}


		#endregion Methods // End of Methods

		#region Events

		/// <summary>
		/// Is invoked when the datagrid is clicked on or sorted, replaces earlier versions with Enter or CurrentCellChanged
		/// but is also during loading of the form, when the grid gets focus, etc, use boolean variable to decide when to execute further
		/// Only useful when the grid contains data
		/// </summary>
		private void DgrCoWorker_Paint(object sender, PaintEventArgs e)
		{
			if ( !gridIsLoading )
			{
				if ( null != this.DgrCoWorker.DataSource && this.DgrCoWorker.VisibleRowCount > 0 )
				{
					this.TableNavigated();
				}
			}
		}


		/// <summary>
		/// Button "FFMA bearbeiten": edit current coworker
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnEdit_Click(object sender, EventArgs e)
		{
			GetMyController().HandleEventOpenProcessDialog(mCurrentFFMAId);
		}


		/// <summary>
		/// "Anwendung beenden": close FPASS
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnExit_Click(object sender, EventArgs e)
		{
			this.BtnRegisterDetails.Enabled = false;
			this.BtnPass.Enabled = false;
			GetMyController().HandleEventCloseFPASS();
		}


		/// <summary>
		/// Button "FFMA neu erfassen": create new coworker
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnRegisterDetails_Click(object sender, EventArgs e)
		{
			GetMyController().HandleEventOpenProcessDialog(-1);
		}


		/// <summary>
		/// Button "Erweiterte Suche"
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnExtendedSearch_Click(object sender, EventArgs e)
		{
			GetMyController().HandleEventOpenExtendedSearch();
		}


		/// <summary>
		/// Button "?" to search for a specific external contractor
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnSearchExternalContractor_Click(object sender, EventArgs e)
		{
			GetMyController().HandleEventOpenSearchExternalContractorDialog();
		}
		
		
		/// <summary>
		/// Reset current ID and row index
		/// Button "Suchen": execute coworker search
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnSearch_Click(object sender, EventArgs e)
		{
			mCurrentFFMAId   = -1;
			mCurrentRowIndex = -1;
			gridIsLoading    = true;
			GetMyController().HandleEventSearch();
			gridIsLoading    = false;
		}


		/// <summary>
		/// Button "Passierschein"
		/// generated report: Pass version 1, user must select a CoWorker to generate this report
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <exception cref="de.pta.Component.Errorhandling.UIWarningException">if no CoWorker selected in datagrid</exception> 
		private void BtnPass_Click(object sender, EventArgs e)
		{
			if (-1 == this.mCurrentFFMAId)
			{
				throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.REPORT_PASS));
			}
			else
			{			
				this.GetMyController().HandleGeneratePass(this.mCurrentFFMAId);
			}
		} 


		/// <summary>
		/// On loading this from: if current user is a coordinator or admin, load Coworker Delete form on top of summary
		/// If user is plant safety manager show Vehicle Entry form on top of summary
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FrmSummaryCoWorker_Activated(object sender, EventArgs e)
		{
			if ( mApplicationStartUp && UserManagementControl.getInstance().
				CurrentUserIsInRole( UserManagementControl.ROLE_KOORDINATOR ) 
				&& ! UserManagementControl.getInstance().CurrentUserIsInRole(
					UserManagementControl.ROLE_EDVADMIN ) 
				&& ! UserManagementControl.getInstance().CurrentUserIsInRole(
				UserManagementControl.ROLE_VERWALTUNG )) 
			{
				mApplicationStartUp = false;
				GetMyController().HandleEventOpenDeleteDialog();
			}

			if ( mApplicationStartUp && UserManagementControl.getInstance().
				CurrentUserIsInRole( UserManagementControl.ROLE_WERKSCHUTZ_LEITER ) ) 
			{
				mApplicationStartUp = false;
				GetMyController().HandleEventOpenVehicleDialog();
			}
		}

		private void MnuRefreshSummary_Click(object sender, EventArgs e)
		{
			GetMyController().HandleEventRefreshSummaryLists();
		}	
		

        /// <summary>
        /// Double-click on datagrid opens current coworker for edit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgrCoWorker_DoubleClick(object sender, EventArgs e)
        {
            if (mCurrentFFMAId > 0)
                GetMyController().HandleEventOpenProcessDialog(mCurrentFFMAId);
        }


       
        /// <summary>
        /// Button "Ausweis-Nr suchen": gets ID card number from ZKS terminal and puts it into "Ausweisnummer" field.
        /// Does not carry out a search.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearchZKS_Click(object sender, EventArgs e)
        {
            mCurrentFFMAId = -1;
            mCurrentRowIndex = -1;
            gridIsLoading = true;

            if (RbtSearchMifare.Checked)
                GetMyController().HandleEventSearchByIdCard(IDCardTypes.Mifare);
            else
                GetMyController().HandleEventSearchByIdCard(IDCardTypes.Hitag2);
            
            gridIsLoading = false;
        }

        /// <summary>
        /// Currently just positions cursor in "Ausweisnummer" field so USB reader writes to correct field in FPASS.
        /// Does not carry out a search.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearchUSB_Click(object sender, EventArgs e)
        {
            TxtSearchIDCard.Focus();

            /*
             * Don't carry out a search
            mCurrentFFMAId   = -1;
			mCurrentRowIndex = -1;
			gridIsLoading    = true;

            if (RbtSearchMifare.Checked)
                GetMyController().HandleEventSearchByIdCard(IDCardTypes.Mifare, IDReaderTypes.USB);
            else
                GetMyController().HandleEventSearchByIdCard(IDCardTypes.Hitag2, IDReaderTypes.USB);
			gridIsLoading    = false;
             * */
        }

        
        #endregion Events

        /// <summary>
        /// Calls Windows form to test ZKS dll
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnTestZKS_Click(object sender, EventArgs e)
        {
            // test mode for the interflex interface
            //--------------------------------------
            frmMain testForm = new frmMain();
            testForm.Show();
        }
    }
}

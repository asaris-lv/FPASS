using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;

using de.pta.Component.ListOfValues;
using de.pta.Component.Common;
using de.pta.Component.Errorhandling;
using de.pta.Component.DbAccess;

using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.Util.Exceptions;
using Degussa.FPASS.Gui.Dialog;
using Degussa.FPASS.Util.ListOfValues;
using Degussa.FPASS.Util.UserManagement;


namespace Degussa.FPASS.Gui.Dialog.Pass
{
	/// <summary>
	/// FrmCoWorkerDelete is the view of the MVC-triad DeleteModel,
	/// DeleteController and FrmCoWorkerDelete.
	/// FrmCoWorkerDelete extends from the FPASSBaseView.
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
	public class FrmCoWorkerDelete : FPASSBaseView
	{
		#region Members
		
		//panels
		internal System.Windows.Forms.Panel PnlSearch;

		//labels
		internal System.Windows.Forms.Label LblSurname;
		internal System.Windows.Forms.Label LblExternalContractor;
		internal System.Windows.Forms.Label LblFirstname;
		internal System.Windows.Forms.Label LblSupervisor;
		internal System.Windows.Forms.Label LblSubcontractor;
		internal System.Windows.Forms.Label LblSearch;
		internal System.Windows.Forms.Label LblCoordinator;
		internal System.Windows.Forms.Label LblMaskTitle;

		//textboxes
		internal System.Windows.Forms.TextBox TxtSurname;
		internal System.Windows.Forms.TextBox TxtFirstname;

		//comboboxes
		internal System.Windows.Forms.ComboBox CboExternalContractor;
		internal System.Windows.Forms.ComboBox CboSubcontractor;
		internal System.Windows.Forms.ComboBox CboSearchSupervisor;
		internal System.Windows.Forms.ComboBox Cbocoordinator;

		//buttons
		internal System.Windows.Forms.Button BtnDeleteChoice;
		internal System.Windows.Forms.Button BtnSearch;
		internal System.Windows.Forms.Button BtnBackTo;
		internal System.Windows.Forms.Button BtnAccessAuthorization;
		internal System.Windows.Forms.Button BtnCoWorkerDetails;
		internal System.Windows.Forms.Button BtnDeleteSummary;
		internal System.Windows.Forms.Button BtnSearchExternalContractor;
		internal System.Windows.Forms.Button BtnClearMask;

		//tooltips
		private System.Windows.Forms.ToolTip TooSearch;
		private System.Windows.Forms.ToolTip TooClearMask;
		private System.Windows.Forms.ToolTip TooDetails;
		private System.Windows.Forms.ToolTip TooAccessOK;
		private System.Windows.Forms.ToolTip TooDeleteList;
		private System.Windows.Forms.ToolTip TooDeleteChoice;
		private System.Windows.Forms.ToolTip TooBackTo;
		private System.Windows.Forms.ToolTip TooExContractor;

		//tables
		internal DataTable TblSaetze = new DataTable();
		internal System.Windows.Forms.DataGrid DgrDeleteCoWorker;
		internal System.Windows.Forms.DataGridTableStyle DgrTableStyleDeleteCoworker;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxCoWorkerID;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxFirstname;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxDateOfBirth;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxExternalContractor;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxSubcontractor;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxSurenameCoord;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxSmartAct;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxSurname;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxStatu;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxAccess;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxValidUntil;

		//others
		internal System.Windows.Forms.ImageList imageList1;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.MenuItem MnuRefreshDeleteCwr;
		
		// For comboboxes
		private bool mCboCoordinatorIsLeading;
		private bool mCboContractorIsLeading;
		
		/// <summary>
		/// holds the id of the current business object selected in the displayed table
		/// </summary>
		private decimal mCurrentFFMAId = -1;
		/// <summary>
		/// Used to stop Paint event on datagrid firing when grid is being loaded
		/// </summary>
		private bool	gridIsLoading			= true;
		/// <summary>
		/// 25.05.04: Used to store which line in datagrid is selected (NOT CWR ID!)
		/// </summary>
		private int     mCurrentRowIndex;
        private Label LblHintSmActDelete;
        private Panel PnlHintSmActDelete;
        private Panel panel2;

		/// <summary>
		/// 24.06.04: Is application currently starting? (form loaded for first time)
		/// </summary>
		private bool	mApplicationStartUp;

		#endregion //End of Members


		#region Constructors

		public FrmCoWorkerDelete()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			InitView();
			
			SetAuthorization();

			// Allow Paint event & sorting on datagrid (this is filled when form is opened)
			this.gridIsLoading = false;
		}	

		
		#endregion //End of Constructors
		
		#region Initialization

		/// <summary>
		/// initialises members
		/// 24.06.04: mApplicationStartUp if veh access should be called at appl start
		/// </summary>
		private void InitView() 
		{
			mApplicationStartUp = true;
			MnuFunction.Enabled = false;
            MnuZKS.Enabled = false;
			MnuReports.Enabled = false;
			mCurrentFFMAId = -1;

			FillLists();
		}


		/* taken out 24.06.04 as not called
		/// <summary>
		/// Initializes the members.
		/// </summary>
		private void Initialize()
		{
			MnuFunction.Enabled = false;
			MnuReports.Enabled = false;
			mCurrentFFMAId = -1;
		}	
		*/
		
		#endregion //End of Initialization
		
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCoWorkerDelete));
            this.DgrDeleteCoWorker = new System.Windows.Forms.DataGrid();
            this.DgrTableStyleDeleteCoworker = new System.Windows.Forms.DataGridTableStyle();
            this.DgrTextBoxCoWorkerID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxStatu = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxAccess = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxSurname = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxFirstname = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxDateOfBirth = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxValidUntil = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxExternalContractor = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxSubcontractor = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxSurenameCoord = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxSmartAct = new System.Windows.Forms.DataGridTextBoxColumn();
            this.LblMaskTitle = new System.Windows.Forms.Label();
            this.BtnSearch = new System.Windows.Forms.Button();
            this.LblSurname = new System.Windows.Forms.Label();
            this.LblExternalContractor = new System.Windows.Forms.Label();
            this.LblFirstname = new System.Windows.Forms.Label();
            this.Cbocoordinator = new System.Windows.Forms.ComboBox();
            this.TxtSurname = new System.Windows.Forms.TextBox();
            this.LblCoordinator = new System.Windows.Forms.Label();
            this.TxtFirstname = new System.Windows.Forms.TextBox();
            this.CboExternalContractor = new System.Windows.Forms.ComboBox();
            this.CboSubcontractor = new System.Windows.Forms.ComboBox();
            this.LblSupervisor = new System.Windows.Forms.Label();
            this.LblSubcontractor = new System.Windows.Forms.Label();
            this.PnlSearch = new System.Windows.Forms.Panel();
            this.BtnClearMask = new System.Windows.Forms.Button();
            this.BtnSearchExternalContractor = new System.Windows.Forms.Button();
            this.CboSearchSupervisor = new System.Windows.Forms.ComboBox();
            this.LblSearch = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.BtnDeleteChoice = new System.Windows.Forms.Button();
            this.BtnBackTo = new System.Windows.Forms.Button();
            this.BtnDeleteSummary = new System.Windows.Forms.Button();
            this.BtnAccessAuthorization = new System.Windows.Forms.Button();
            this.BtnCoWorkerDetails = new System.Windows.Forms.Button();
            this.TooSearch = new System.Windows.Forms.ToolTip(this.components);
            this.TooClearMask = new System.Windows.Forms.ToolTip(this.components);
            this.TooDetails = new System.Windows.Forms.ToolTip(this.components);
            this.TooAccessOK = new System.Windows.Forms.ToolTip(this.components);
            this.TooDeleteList = new System.Windows.Forms.ToolTip(this.components);
            this.TooDeleteChoice = new System.Windows.Forms.ToolTip(this.components);
            this.TooBackTo = new System.Windows.Forms.ToolTip(this.components);
            this.TooExContractor = new System.Windows.Forms.ToolTip(this.components);
            this.MnuRefreshDeleteCwr = new System.Windows.Forms.MenuItem();
            this.LblHintSmActDelete = new System.Windows.Forms.Label();
            this.PnlHintSmActDelete = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.SbpMessage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sbpenvironment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgrDeleteCoWorker)).BeginInit();
            this.PnlSearch.SuspendLayout();
            this.PnlHintSmActDelete.SuspendLayout();
            this.SuspendLayout();
            // 
            // LblBaseHead
            // 
            this.LblBaseHead.Size = new System.Drawing.Size(1272, 40);
            this.LblBaseHead.Text = "gggggg";
            // 
            // MnuFile
            // 
            this.MnuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MnuRefreshDeleteCwr});
            // 
            // DgrDeleteCoWorker
            // 
            this.DgrDeleteCoWorker.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DgrDeleteCoWorker.CaptionBackColor = System.Drawing.Color.SteelBlue;
            this.DgrDeleteCoWorker.CaptionFont = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DgrDeleteCoWorker.CaptionText = "Fremdfirmenmitarbeiter";
            this.DgrDeleteCoWorker.DataMember = "";
            this.DgrDeleteCoWorker.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.DgrDeleteCoWorker.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.DgrDeleteCoWorker.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.DgrDeleteCoWorker.Location = new System.Drawing.Point(11, 169);
            this.DgrDeleteCoWorker.Name = "DgrDeleteCoWorker";
            this.DgrDeleteCoWorker.ReadOnly = true;
            this.DgrDeleteCoWorker.Size = new System.Drawing.Size(1245, 642);
            this.DgrDeleteCoWorker.TabIndex = 10;
            this.DgrDeleteCoWorker.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.DgrTableStyleDeleteCoworker});
            this.DgrDeleteCoWorker.Paint += new System.Windows.Forms.PaintEventHandler(this.DgrDeleteCoWorker_Paint);
            this.DgrDeleteCoWorker.DoubleClick += new System.EventHandler(this.DgrDeleteCoWorker_DoubleClick);
            // 
            // DgrTableStyleDeleteCoworker
            // 
            this.DgrTableStyleDeleteCoworker.DataGrid = this.DgrDeleteCoWorker;
            this.DgrTableStyleDeleteCoworker.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.DgrTextBoxCoWorkerID,
            this.DgrTextBoxStatu,
            this.DgrTextBoxAccess,
            this.DgrTextBoxSurname,
            this.DgrTextBoxFirstname,
            this.DgrTextBoxDateOfBirth,
            this.DgrTextBoxValidUntil,
            this.DgrTextBoxExternalContractor,
            this.DgrTextBoxSubcontractor,
            this.DgrTextBoxSurenameCoord,
            this.DgrTextBoxSmartAct});
            this.DgrTableStyleDeleteCoworker.HeaderFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DgrTableStyleDeleteCoworker.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.DgrTableStyleDeleteCoworker.MappingName = "RTTabCoWorker";
            // 
            // DgrTextBoxCoWorkerID
            // 
            this.DgrTextBoxCoWorkerID.Format = "";
            this.DgrTextBoxCoWorkerID.FormatInfo = null;
            this.DgrTextBoxCoWorkerID.MappingName = "FFMAID";
            this.DgrTextBoxCoWorkerID.NullText = "";
            this.DgrTextBoxCoWorkerID.Width = 1;
            // 
            // DgrTextBoxStatu
            // 
            this.DgrTextBoxStatu.Format = "";
            this.DgrTextBoxStatu.FormatInfo = null;
            this.DgrTextBoxStatu.HeaderText = "Status";
            this.DgrTextBoxStatu.MappingName = "Status";
            this.DgrTextBoxStatu.NullText = "";
            this.DgrTextBoxStatu.Width = 80;
            // 
            // DgrTextBoxAccess
            // 
            this.DgrTextBoxAccess.Format = "";
            this.DgrTextBoxAccess.FormatInfo = null;
            this.DgrTextBoxAccess.HeaderText = "Zutritt";
            this.DgrTextBoxAccess.MappingName = "Access";
            this.DgrTextBoxAccess.NullText = "";
            this.DgrTextBoxAccess.Width = 60;
            // 
            // DgrTextBoxSurname
            // 
            this.DgrTextBoxSurname.Format = "";
            this.DgrTextBoxSurname.FormatInfo = null;
            this.DgrTextBoxSurname.HeaderText = "Nachname";
            this.DgrTextBoxSurname.MappingName = "Surname";
            this.DgrTextBoxSurname.NullText = "";
            this.DgrTextBoxSurname.Width = 140;
            // 
            // DgrTextBoxFirstname
            // 
            this.DgrTextBoxFirstname.Format = "";
            this.DgrTextBoxFirstname.FormatInfo = null;
            this.DgrTextBoxFirstname.HeaderText = "Vorname";
            this.DgrTextBoxFirstname.MappingName = "Firstname";
            this.DgrTextBoxFirstname.NullText = "";
            this.DgrTextBoxFirstname.Width = 130;
            // 
            // DgrTextBoxDateOfBirth
            // 
            this.DgrTextBoxDateOfBirth.Format = "";
            this.DgrTextBoxDateOfBirth.FormatInfo = null;
            this.DgrTextBoxDateOfBirth.HeaderText = "Geburtsdatum";
            this.DgrTextBoxDateOfBirth.MappingName = "DateOfBirth";
            this.DgrTextBoxDateOfBirth.NullText = "";
            this.DgrTextBoxDateOfBirth.Width = 90;
            // 
            // DgrTextBoxValidUntil
            // 
            this.DgrTextBoxValidUntil.Format = "";
            this.DgrTextBoxValidUntil.FormatInfo = null;
            this.DgrTextBoxValidUntil.HeaderText = "Zutritt-Gültigbis";
            this.DgrTextBoxValidUntil.MappingName = "ValidUntil";
            this.DgrTextBoxValidUntil.NullText = "";
            this.DgrTextBoxValidUntil.Width = 85;
            // 
            // DgrTextBoxExternalContractor
            // 
            this.DgrTextBoxExternalContractor.Format = "";
            this.DgrTextBoxExternalContractor.FormatInfo = null;
            this.DgrTextBoxExternalContractor.HeaderText = "Fremdfirma";
            this.DgrTextBoxExternalContractor.MappingName = "ExContractorName";
            this.DgrTextBoxExternalContractor.NullText = "";
            this.DgrTextBoxExternalContractor.Width = 200;
            // 
            // DgrTextBoxSubcontractor
            // 
            this.DgrTextBoxSubcontractor.Format = "";
            this.DgrTextBoxSubcontractor.FormatInfo = null;
            this.DgrTextBoxSubcontractor.HeaderText = "Subfirma";
            this.DgrTextBoxSubcontractor.MappingName = "SubContractor";
            this.DgrTextBoxSubcontractor.NullText = "";
            this.DgrTextBoxSubcontractor.Width = 160;
            // 
            // DgrTextBoxSurenameCoord
            // 
            this.DgrTextBoxSurenameCoord.Format = "";
            this.DgrTextBoxSurenameCoord.FormatInfo = null;
            this.DgrTextBoxSurenameCoord.HeaderText = "Koordinator";
            this.DgrTextBoxSurenameCoord.MappingName = "CoordNameAndTel";
            this.DgrTextBoxSurenameCoord.NullText = "";
            this.DgrTextBoxSurenameCoord.Width = 180;
            // 
            // DgrTextBoxSmartAct
            // 
            this.DgrTextBoxSmartAct.Format = "";
            this.DgrTextBoxSmartAct.FormatInfo = null;
            this.DgrTextBoxSmartAct.HeaderText = "SmartAct";
            this.DgrTextBoxSmartAct.MappingName = "SmartActNo";
            this.DgrTextBoxSmartAct.NullText = "";
            this.DgrTextBoxSmartAct.Width = 60;
            // 
            // LblMaskTitle
            // 
            this.LblMaskTitle.BackColor = System.Drawing.Color.White;
            this.LblMaskTitle.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMaskTitle.Location = new System.Drawing.Point(444, 8);
            this.LblMaskTitle.Name = "LblMaskTitle";
            this.LblMaskTitle.Size = new System.Drawing.Size(344, 32);
            this.LblMaskTitle.TabIndex = 19;
            this.LblMaskTitle.Text = "FPASS - Löschen FFMA";
            // 
            // BtnSearch
            // 
            this.BtnSearch.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSearch.Location = new System.Drawing.Point(1073, 18);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(145, 30);
            this.BtnSearch.TabIndex = 8;
            this.BtnSearch.Text = "&Suchen";
            this.TooSearch.SetToolTip(this.BtnSearch, "Löst den Suchvorgang aus");
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // LblSurname
            // 
            this.LblSurname.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSurname.Location = new System.Drawing.Point(370, 27);
            this.LblSurname.Name = "LblSurname";
            this.LblSurname.Size = new System.Drawing.Size(72, 16);
            this.LblSurname.TabIndex = 10;
            this.LblSurname.Text = "Nachname";
            // 
            // LblExternalContractor
            // 
            this.LblExternalContractor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblExternalContractor.Location = new System.Drawing.Point(24, 59);
            this.LblExternalContractor.Name = "LblExternalContractor";
            this.LblExternalContractor.Size = new System.Drawing.Size(72, 16);
            this.LblExternalContractor.TabIndex = 5;
            this.LblExternalContractor.Text = "Fremdfirma";
            // 
            // LblFirstname
            // 
            this.LblFirstname.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblFirstname.Location = new System.Drawing.Point(370, 59);
            this.LblFirstname.Name = "LblFirstname";
            this.LblFirstname.Size = new System.Drawing.Size(72, 16);
            this.LblFirstname.TabIndex = 11;
            this.LblFirstname.Text = "Vorname";
            // 
            // Cbocoordinator
            // 
            this.Cbocoordinator.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cbocoordinator.ItemHeight = 15;
            this.Cbocoordinator.Location = new System.Drawing.Point(104, 25);
            this.Cbocoordinator.Name = "Cbocoordinator";
            this.Cbocoordinator.Size = new System.Drawing.Size(210, 23);
            this.Cbocoordinator.TabIndex = 1;
            this.Cbocoordinator.SelectedIndexChanged += new System.EventHandler(this.Cbocoordinator_SelectedIndexChanged);
            // 
            // TxtSurname
            // 
            this.TxtSurname.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSurname.Location = new System.Drawing.Point(445, 25);
            this.TxtSurname.Name = "TxtSurname";
            this.TxtSurname.Size = new System.Drawing.Size(210, 21);
            this.TxtSurname.TabIndex = 4;
            // 
            // LblCoordinator
            // 
            this.LblCoordinator.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCoordinator.Location = new System.Drawing.Point(690, 59);
            this.LblCoordinator.Name = "LblCoordinator";
            this.LblCoordinator.Size = new System.Drawing.Size(72, 16);
            this.LblCoordinator.TabIndex = 7;
            this.LblCoordinator.Text = "Subfirma";
            // 
            // TxtFirstname
            // 
            this.TxtFirstname.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtFirstname.Location = new System.Drawing.Point(445, 57);
            this.TxtFirstname.Name = "TxtFirstname";
            this.TxtFirstname.Size = new System.Drawing.Size(210, 21);
            this.TxtFirstname.TabIndex = 5;
            // 
            // CboExternalContractor
            // 
            this.CboExternalContractor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboExternalContractor.ItemHeight = 15;
            this.CboExternalContractor.Location = new System.Drawing.Point(104, 57);
            this.CboExternalContractor.Name = "CboExternalContractor";
            this.CboExternalContractor.Size = new System.Drawing.Size(210, 23);
            this.CboExternalContractor.TabIndex = 2;
            this.CboExternalContractor.SelectedIndexChanged += new System.EventHandler(this.CboExternalContractor_SelectedIndexChanged);
            // 
            // CboSubcontractor
            // 
            this.CboSubcontractor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboSubcontractor.ItemHeight = 15;
            this.CboSubcontractor.Location = new System.Drawing.Point(796, 57);
            this.CboSubcontractor.Name = "CboSubcontractor";
            this.CboSubcontractor.Size = new System.Drawing.Size(210, 23);
            this.CboSubcontractor.TabIndex = 7;
            // 
            // LblSupervisor
            // 
            this.LblSupervisor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSupervisor.Location = new System.Drawing.Point(690, 27);
            this.LblSupervisor.Name = "LblSupervisor";
            this.LblSupervisor.Size = new System.Drawing.Size(96, 16);
            this.LblSupervisor.TabIndex = 15;
            this.LblSupervisor.Text = "Baustellenleiter";
            // 
            // LblSubcontractor
            // 
            this.LblSubcontractor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSubcontractor.Location = new System.Drawing.Point(24, 27);
            this.LblSubcontractor.Name = "LblSubcontractor";
            this.LblSubcontractor.Size = new System.Drawing.Size(72, 18);
            this.LblSubcontractor.TabIndex = 8;
            this.LblSubcontractor.Text = "Koordinator";
            // 
            // PnlSearch
            // 
            this.PnlSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlSearch.Controls.Add(this.BtnClearMask);
            this.PnlSearch.Controls.Add(this.BtnSearchExternalContractor);
            this.PnlSearch.Controls.Add(this.CboSearchSupervisor);
            this.PnlSearch.Controls.Add(this.BtnSearch);
            this.PnlSearch.Controls.Add(this.LblSurname);
            this.PnlSearch.Controls.Add(this.LblExternalContractor);
            this.PnlSearch.Controls.Add(this.LblFirstname);
            this.PnlSearch.Controls.Add(this.Cbocoordinator);
            this.PnlSearch.Controls.Add(this.TxtSurname);
            this.PnlSearch.Controls.Add(this.LblCoordinator);
            this.PnlSearch.Controls.Add(this.TxtFirstname);
            this.PnlSearch.Controls.Add(this.CboExternalContractor);
            this.PnlSearch.Controls.Add(this.CboSubcontractor);
            this.PnlSearch.Controls.Add(this.LblSupervisor);
            this.PnlSearch.Controls.Add(this.LblSubcontractor);
            this.PnlSearch.Location = new System.Drawing.Point(11, 56);
            this.PnlSearch.Name = "PnlSearch";
            this.PnlSearch.Size = new System.Drawing.Size(1245, 104);
            this.PnlSearch.TabIndex = 0;
            // 
            // BtnClearMask
            // 
            this.BtnClearMask.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClearMask.Location = new System.Drawing.Point(1073, 58);
            this.BtnClearMask.Name = "BtnClearMask";
            this.BtnClearMask.Size = new System.Drawing.Size(145, 30);
            this.BtnClearMask.TabIndex = 9;
            this.BtnClearMask.Text = "&Maske leeren";
            this.TooSearch.SetToolTip(this.BtnClearMask, "Löscht alle bereits eingegebenen Suchkriterien");
            this.BtnClearMask.Click += new System.EventHandler(this.BtnClearMask_Click);
            // 
            // BtnSearchExternalContractor
            // 
            this.BtnSearchExternalContractor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSearchExternalContractor.Location = new System.Drawing.Point(321, 59);
            this.BtnSearchExternalContractor.Name = "BtnSearchExternalContractor";
            this.BtnSearchExternalContractor.Size = new System.Drawing.Size(21, 21);
            this.BtnSearchExternalContractor.TabIndex = 3;
            this.BtnSearchExternalContractor.Text = "? &F";
            this.TooExContractor.SetToolTip(this.BtnSearchExternalContractor, "Öffnet die Maske Fremdfirmensuche");
            this.BtnSearchExternalContractor.Click += new System.EventHandler(this.BtnSearchExternalContractor_Click);
            // 
            // CboSearchSupervisor
            // 
            this.CboSearchSupervisor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboSearchSupervisor.ItemHeight = 15;
            this.CboSearchSupervisor.Location = new System.Drawing.Point(796, 25);
            this.CboSearchSupervisor.Name = "CboSearchSupervisor";
            this.CboSearchSupervisor.Size = new System.Drawing.Size(210, 23);
            this.CboSearchSupervisor.Sorted = true;
            this.CboSearchSupervisor.TabIndex = 6;
            // 
            // LblSearch
            // 
            this.LblSearch.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSearch.Location = new System.Drawing.Point(16, 48);
            this.LblSearch.Name = "LblSearch";
            this.LblSearch.Size = new System.Drawing.Size(48, 16);
            this.LblSearch.TabIndex = 18;
            this.LblSearch.Text = "Suche";
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // BtnDeleteChoice
            // 
            this.BtnDeleteChoice.Enabled = false;
            this.BtnDeleteChoice.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDeleteChoice.Location = new System.Drawing.Point(945, 864);
            this.BtnDeleteChoice.Name = "BtnDeleteChoice";
            this.BtnDeleteChoice.Size = new System.Drawing.Size(150, 32);
            this.BtnDeleteChoice.TabIndex = 14;
            this.BtnDeleteChoice.Tag = "";
            this.BtnDeleteChoice.Text = "Auswahl &löschen";
            this.TooDeleteChoice.SetToolTip(this.BtnDeleteChoice, "Löscht die markierten Datensätze");
            this.BtnDeleteChoice.Click += new System.EventHandler(this.BtnDeleteChoice_Click);
            // 
            // BtnBackTo
            // 
            this.BtnBackTo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBackTo.Location = new System.Drawing.Point(1101, 864);
            this.BtnBackTo.Name = "BtnBackTo";
            this.BtnBackTo.Size = new System.Drawing.Size(150, 32);
            this.BtnBackTo.TabIndex = 15;
            this.BtnBackTo.Tag = "";
            this.BtnBackTo.Text = "&Zurück";
            this.TooBackTo.SetToolTip(this.BtnBackTo, "Zurück zur Übersichtsmaske");
            this.BtnBackTo.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnDeleteSummary
            // 
            this.BtnDeleteSummary.Enabled = false;
            this.BtnDeleteSummary.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDeleteSummary.Location = new System.Drawing.Point(789, 864);
            this.BtnDeleteSummary.Name = "BtnDeleteSummary";
            this.BtnDeleteSummary.Size = new System.Drawing.Size(150, 32);
            this.BtnDeleteSummary.TabIndex = 13;
            this.BtnDeleteSummary.Tag = "";
            this.BtnDeleteSummary.Text = "&Gesamtliste löschen";
            this.TooDeleteList.SetToolTip(this.BtnDeleteSummary, "Löscht die gesamte Liste");
            this.BtnDeleteSummary.Click += new System.EventHandler(this.BtnDeleteSummary_Click);
            // 
            // BtnAccessAuthorization
            // 
            this.BtnAccessAuthorization.Enabled = false;
            this.BtnAccessAuthorization.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAccessAuthorization.Location = new System.Drawing.Point(573, 864);
            this.BtnAccessAuthorization.Name = "BtnAccessAuthorization";
            this.BtnAccessAuthorization.Size = new System.Drawing.Size(210, 32);
            this.BtnAccessAuthorization.TabIndex = 12;
            this.BtnAccessAuthorization.Tag = "";
            this.BtnAccessAuthorization.Text = "Zutrittsberechtigung &verlängern";
            this.TooAccessOK.SetToolTip(this.BtnAccessAuthorization, "Verlängert die Zutrittsberechtigung des ausgewählten Fremdfirmenmitarbeiters");
            this.BtnAccessAuthorization.Click += new System.EventHandler(this.BtnAccessAuthorization_Click);
            // 
            // BtnCoWorkerDetails
            // 
            this.BtnCoWorkerDetails.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCoWorkerDetails.Location = new System.Drawing.Point(357, 864);
            this.BtnCoWorkerDetails.Name = "BtnCoWorkerDetails";
            this.BtnCoWorkerDetails.Size = new System.Drawing.Size(210, 32);
            this.BtnCoWorkerDetails.TabIndex = 11;
            this.BtnCoWorkerDetails.Tag = "";
            this.BtnCoWorkerDetails.Text = "&Details Fremdfirmenmitarbeiter";
            this.TooDetails.SetToolTip(this.BtnCoWorkerDetails, "Zeigt weitere Daten des ausgewählten Fremdfirmenmitarbeiters an");
            this.BtnCoWorkerDetails.Click += new System.EventHandler(this.BtnCoWorkerDetails_Click);
            // 
            // MnuRefreshDeleteCwr
            // 
            this.MnuRefreshDeleteCwr.Index = 1;
            this.MnuRefreshDeleteCwr.Shortcut = System.Windows.Forms.Shortcut.F5;
            this.MnuRefreshDeleteCwr.Text = "Listen aktualisieren";
            this.MnuRefreshDeleteCwr.Click += new System.EventHandler(this.MnuRefreshDeleteCwr_Click);
            // 
            // LblHintSmActDelete
            // 
            this.LblHintSmActDelete.AutoSize = true;
            this.LblHintSmActDelete.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblHintSmActDelete.ForeColor = System.Drawing.Color.White;
            this.LblHintSmActDelete.Location = new System.Drawing.Point(6, 6);
            this.LblHintSmActDelete.Name = "LblHintSmActDelete";
            this.LblHintSmActDelete.Size = new System.Drawing.Size(768, 15);
            this.LblHintSmActDelete.TabIndex = 20;
            this.LblHintSmActDelete.Text = "Warnung:  Das Löschen von FFMA mit Lichtbildausweis (\'SmartAct\' gleich \'Y\') führt" +
    " auch zum Löschen dieser FFMA im Ausweissystem.";
            // 
            // PnlHintSmActDelete
            // 
            this.PnlHintSmActDelete.BackColor = System.Drawing.Color.SteelBlue;
            this.PnlHintSmActDelete.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlHintSmActDelete.Controls.Add(this.LblHintSmActDelete);
            this.PnlHintSmActDelete.Location = new System.Drawing.Point(12, 817);
            this.PnlHintSmActDelete.Name = "PnlHintSmActDelete";
            this.PnlHintSmActDelete.Size = new System.Drawing.Size(1244, 30);
            this.PnlHintSmActDelete.TabIndex = 21;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Location = new System.Drawing.Point(13, 856);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1243, 1);
            this.panel2.TabIndex = 23;
            // 
            // FrmCoWorkerDelete
            // 
            this.AcceptButton = this.BtnSearch;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(1272, 966);
            this.Controls.Add(this.LblMaskTitle);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.PnlHintSmActDelete);
            this.Controls.Add(this.BtnCoWorkerDetails);
            this.Controls.Add(this.BtnAccessAuthorization);
            this.Controls.Add(this.BtnDeleteSummary);
            this.Controls.Add(this.LblSearch);
            this.Controls.Add(this.BtnBackTo);
            this.Controls.Add(this.BtnDeleteChoice);
            this.Controls.Add(this.PnlSearch);
            this.Controls.Add(this.DgrDeleteCoWorker);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmCoWorkerDelete";
            this.Text = "FPASS - Löschen FFMA";
            this.Activated += new System.EventHandler(this.FrmCoWorkerDelete_Activated);
            this.Controls.SetChildIndex(this.DgrDeleteCoWorker, 0);
            this.Controls.SetChildIndex(this.PnlSearch, 0);
            this.Controls.SetChildIndex(this.BtnDeleteChoice, 0);
            this.Controls.SetChildIndex(this.BtnBackTo, 0);
            this.Controls.SetChildIndex(this.LblSearch, 0);
            this.Controls.SetChildIndex(this.BtnDeleteSummary, 0);
            this.Controls.SetChildIndex(this.BtnAccessAuthorization, 0);
            this.Controls.SetChildIndex(this.BtnCoWorkerDetails, 0);
            this.Controls.SetChildIndex(this.PnlHintSmActDelete, 0);
            this.Controls.SetChildIndex(this.LblBaseHead, 0);
            this.Controls.SetChildIndex(this.StbBase, 0);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.LblMaskTitle, 0);
            ((System.ComponentModel.ISupportInitialize)(this.SbpMessage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sbpenvironment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgrDeleteCoWorker)).EndInit();
            this.PnlSearch.ResumeLayout(false);
            this.PnlSearch.PerformLayout();
            this.PnlHintSmActDelete.ResumeLayout(false);
            this.PnlHintSmActDelete.PerformLayout();
            this.ResumeLayout(false);

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

		#endregion

		#region Accessors 

		/// <summary>
		/// To access PK ID of current coworker
		/// </summary>
		internal decimal CurrentFFMAId
		{
			get 
			{
				return mCurrentFFMAId;
			}
			set 
			{
				mCurrentFFMAId = value;
			}
		}

		/// <summary>
		/// 25.05.04: to access which row currently selected
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

		/// <summary>
		/// Accessor 25.05.04: needed to trigger paint event from Controller
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

		#endregion //End of Accessors

		#region Methods

		internal override void ReFillContractorList(String pContractorID) 
		{
			FillCoordinatorSearch("0");
			FillExternalContractorSearch("0");
			CboExternalContractor.SelectedValue =  
				Convert.ToDecimal( pContractorID );
			this.BtnSearch_Click(this, null);
		}


		internal override void FillLists() 
		{
			FillCoordinatorSearch("0");
			FillExternalContractorSearch("0");
			FillSubContractorSearch();
			FillSupervisor();
		}

		/// <summary>
		/// fills combobox supervisor
		/// </summary>
		internal void FillSupervisor()
		{
			ArrayList supervisor = new ArrayList(); 
			supervisor.Add(new LovItem("0", ""));
			supervisor.AddRange( LovSingleton.GetInstance().GetRootList(null, "FPASS_SUPERVISOR", "SUPERVISOR"));
			this.CboSearchSupervisor.DataSource = supervisor;
			this.CboSearchSupervisor.DisplayMember = "ItemValue";
			this.CboSearchSupervisor.ValueMember = "Id";
		}

		/// <summary>
		/// Fills combobox "Fremdfirma"
		/// For the delete list both valid and invalid contractors must be shown
		/// </summary>
		/// <param name="pID"></param>
		internal void FillExternalContractorSearch(String pID)
		{
			this.CboExternalContractor.DataSource    = this.GetContractorValInvalList(pID);
			this.CboExternalContractor.DisplayMember = "ContractorName";
			this.CboExternalContractor.ValueMember   = "ContractorID";
		}

		/// <summary>
		/// fills combobox coordinator
		/// </summary>
		/// <param name="pID"></param>
		internal void FillCoordinatorSearch(String pID)
		{
			this.Cbocoordinator.DataSource = this.GetCoordinatorList(pID);
			this.Cbocoordinator.DisplayMember = "CoordFullNameTel";
			this.Cbocoordinator.ValueMember = "CoordID";
			
		}

		/// <summary>
		/// New 24.05.04:
		/// after an automatic search has been carried out, the cursor should jump to the position of
		/// previously selected CWR.
		/// Do this by using the current row index of datagrid, no matter if CWR not same
		/// </summary>
		internal void JumpToCurrentCoWorker()
		{			
			this.DgrDeleteCoWorker.CurrentRowIndex = mCurrentRowIndex;
		}

		/// <summary>
		/// Every time datagrid is navigated: get ID of currently selected cwr 
		/// 25.05.04: Get row index in grid of current coworker
		/// </summary>
		private void TableNavigated()
		{
			mCurrentRowIndex = this.DgrDeleteCoWorker.CurrentRowIndex;
			if(-1 < mCurrentRowIndex)
			{
				mCurrentFFMAId = Convert.ToDecimal(this.DgrDeleteCoWorker[mCurrentRowIndex, 0].ToString());
			}
		}
		

		/// <summary>
		/// Fills combobox Subcontractor.
		/// These are not coordinator dependent
		/// Done over LOV Singleton to get valid and invalid contractors.
		/// </summary>
		private void FillSubContractorSearch()
		{			
			this.CboSubcontractor.DataSource    = this.GetContractorValInvalList("0");
			this.CboSubcontractor.DisplayMember = "ContractorName";
			this.CboSubcontractor.ValueMember   = "ContractorID";
		}	

		private DeleteController GetMyController() 
		{
			return (DeleteController)mController;
		}

		/// <summary>
		/// Empty
		/// </summary>
		private void SetAuthorization() 
		{

		}

		#endregion // End of Methods

		#region Events

		/// <summary>
		/// Need to do automatic search on SummaryCWR
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnCancel_Click(object sender, System.EventArgs e)
		{
			this.GetMyController().HandleEventLeaveFrmDelCWR();
		}

		private void BtnSearch_Click(object sender, System.EventArgs e)
		{
			this.mCurrentRowIndex = -1;
			this.gridIsLoading = true;
			this.GetMyController().HandleEventBtnSearchDeleteCoWorker();
			this.gridIsLoading = false;
		}

		/// <summary>
		/// 13.11.03
		/// Is invoked when the datagrid is navigated by the user or grid is sorted
		/// but also many times when grid is being loaded, hence boolean variable
		private void DgrDeleteCoWorker_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			if ( !gridIsLoading )
			{
				if ( null != this.DgrDeleteCoWorker.DataSource && this.DgrDeleteCoWorker.VisibleRowCount > 0 )
				{
					this.TableNavigated();
				}
			}	
		}
		
		private void BtnDeleteChoice_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventDeleteCoworker();
		}

		private void BtnDeleteSummary_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventDeleteAllCoworker();
		}

		private void BtnAccessAuthorization_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventExtendValidUntil();
		}

        /// <summary>
        /// Raised when user clicks on "Details FFMA"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void BtnCoWorkerDetails_Click(object sender, System.EventArgs e)
		{
			try 
			{
				if (mCurrentFFMAId > 0)
				{
					GetMyController().HandleEventOpenProcessDialog(mCurrentFFMAId);
				}
				else
				{
					throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.REPORT_PASS));
				}
			} 
			catch ( UIWarningException uwe ) 
			{
				ExceptionProcessor.GetInstance().Process(uwe);
			}			
		}

		private void CboExternalContractor_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			
		}

		private void Cbocoordinator_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			
		}

		private void BtnSearchExternalContractor_Click(object sender, System.EventArgs e)
		{
			this.DgrDeleteCoWorker.DataSource = null;
			GetMyController().HandleEventOpenSearchDialog();
		}

		private void BtnClearMask_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventBtnFormEmpty();
		}

		private void MnuRefreshDeleteCwr_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventRefreshDeleteViewLists();
		}
		
		/// <summary>
		/// New 24.06.04: if current user is coordinator and leader site security, 
		/// (but not ADMIN or Verwalter)
		/// open vehicle access dialog from here (on top of CoWorkerDelete, kind of cascading logic)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FrmCoWorkerDelete_Activated(object sender, System.EventArgs e)
		{
			if ( mApplicationStartUp 
				&& UserManagementControl.getInstance().
				CurrentUserIsInRole( UserManagementControl.ROLE_KOORDINATOR )
				&& UserManagementControl.getInstance().
				CurrentUserIsInRole( UserManagementControl.ROLE_WERKSCHUTZ_LEITER )
				&& ! UserManagementControl.getInstance().CurrentUserIsInRole(
				UserManagementControl.ROLE_EDVADMIN ) 
				&& ! UserManagementControl.getInstance().CurrentUserIsInRole(
				UserManagementControl.ROLE_VERWALTUNG )
				)			
			{
				mApplicationStartUp = false;
				GetMyController().HandleEventOpenVehicleDialog();
			}			
		}


        /// <summary>
        /// Double-click on datagrid opens current coworker for edit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgrDeleteCoWorker_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (mCurrentFFMAId > 0)
                {
                    GetMyController().HandleEventOpenProcessDialog(mCurrentFFMAId);
                }
                else
                {
                    throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.REPORT_PASS));
                }
            }
            catch (UIWarningException uwe)
            {
                ExceptionProcessor.GetInstance().Process(uwe);
            }
        }

        #endregion // End of Events
    }
	
}


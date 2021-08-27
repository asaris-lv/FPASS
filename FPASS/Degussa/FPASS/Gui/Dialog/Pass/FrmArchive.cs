using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;

using System.Diagnostics;
using Microsoft.Win32;
using System.Text.RegularExpressions;

using de.pta.Component.ListOfValues;
using de.pta.Component.Common;

using Degussa.FPASS.Gui.Dialog;
using Degussa.FPASS.Util.ListOfValues;
using Degussa.FPASS.Util.UserManagement;


namespace Degussa.FPASS.Gui.Dialog.Pass
{
	/// <summary>
	/// FrmArchive is the view of the MVC-triad ArchiveModel,
	/// ArchiveController and FrmArchive.
	/// FrmArchive extends from the FPASSBaseView.
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
	public class FrmArchive : FPASSBaseView
	{
		#region Members

		//panels
		internal System.Windows.Forms.Panel PnlSearch;

		//labels
		internal System.Windows.Forms.Label LblSearchExternalContractor;
		internal System.Windows.Forms.Label LblSearchFirstname;
		internal System.Windows.Forms.Label LblSearchCoordinator;
		internal System.Windows.Forms.Label LblSearchSupervisor;
		internal System.Windows.Forms.Label LblSearchSubcontractor;
		internal System.Windows.Forms.Label LblSearch;
		internal System.Windows.Forms.Label LblMaskTitle;
		internal System.Windows.Forms.Label LblSearchSurname;

		//textboxes
		internal System.Windows.Forms.TextBox TxtSearchFirstname;
		internal System.Windows.Forms.TextBox TxtSearchSurname;

		//comboboxes
		internal System.Windows.Forms.ComboBox CboSearchCoordinator;
		internal System.Windows.Forms.ComboBox CboSearchExternalContractor;
		internal System.Windows.Forms.ComboBox CboSearchSubcontractor;
		internal System.Windows.Forms.ComboBox CboSearchSupervisor;

		//buttons
		internal System.Windows.Forms.Button BtnSearch;
		internal System.Windows.Forms.Button BtnHelp;
		internal System.Windows.Forms.Button BtnSearchExternalContractor;
		internal System.Windows.Forms.Button BtnDetails;
		internal System.Windows.Forms.Button BtnBackTo;
		internal System.Windows.Forms.Button BtnClearMask;

		//tooltips
		private System.Windows.Forms.ToolTip TooSearch;
		private System.Windows.Forms.ToolTip TooClearMask;
		private System.Windows.Forms.ToolTip TooDetails;
		private System.Windows.Forms.ToolTip TooBackTo;
		private System.Windows.Forms.ToolTip TooExContractor;

		//Tables
		internal DataTable TblSaetze = new DataTable();
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxExternalContractor;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxSubcontractor;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxSurname;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxVorname;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxDateOfBirth;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxSupervisor;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxCoWorkerID;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxStatus;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxSurnameCoordinator;
		internal System.Windows.Forms.DataGrid DgrCoWorker;
		internal System.Windows.Forms.DataGridTableStyle DgrTableStyleSummary;

		//Other
		private System.ComponentModel.IContainer components;

		/// <summary>
		/// holds the id of the current business object selected in the displayed table
		/// </summary>
		private decimal mCurrentFFMAId;

		// Used to stop Paint event on datagrid firing when grid is being loaded
		private  bool	gridIsLoading			= true;

		private  bool mCboCoordinatorIsLeading;
		private  bool mCboContractorIsLeading;

		#endregion //End of Members

		#region Constructors

		public FrmArchive()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			
			Initialize();
			//fills comboboxes and listboxes
			FillLists();

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
            MnuZKS.Enabled = false;
			MnuReports.Enabled = false;

			mCurrentFFMAId = -1;
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
            this.DgrCoWorker = new System.Windows.Forms.DataGrid();
            this.DgrTableStyleSummary = new System.Windows.Forms.DataGridTableStyle();
            this.DgrTextBoxCoWorkerID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxStatus = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxSurname = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxVorname = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxDateOfBirth = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxExternalContractor = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxSubcontractor = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxSurnameCoordinator = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxSupervisor = new System.Windows.Forms.DataGridTextBoxColumn();
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
            this.BtnClearMask = new System.Windows.Forms.Button();
            this.BtnSearchExternalContractor = new System.Windows.Forms.Button();
            this.CboSearchSupervisor = new System.Windows.Forms.ComboBox();
            this.LblSearch = new System.Windows.Forms.Label();
            this.BtnDetails = new System.Windows.Forms.Button();
            this.BtnBackTo = new System.Windows.Forms.Button();
            this.BtnHelp = new System.Windows.Forms.Button();
            this.TooSearch = new System.Windows.Forms.ToolTip(this.components);
            this.TooClearMask = new System.Windows.Forms.ToolTip(this.components);
            this.TooDetails = new System.Windows.Forms.ToolTip(this.components);
            this.TooBackTo = new System.Windows.Forms.ToolTip(this.components);
            this.TooExContractor = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.SbpMessage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sbpenvironment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgrCoWorker)).BeginInit();
            this.PnlSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // LblBaseHead
            // 
            this.LblBaseHead.Size = new System.Drawing.Size(1264, 40);
            // 
            // DgrCoWorker
            // 
            this.DgrCoWorker.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DgrCoWorker.CaptionBackColor = System.Drawing.Color.SteelBlue;
            this.DgrCoWorker.CaptionFont = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DgrCoWorker.CaptionText = "Fremdfirmenmitarbeiter - Archiv";
            this.DgrCoWorker.DataMember = "";
            this.DgrCoWorker.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.DgrCoWorker.Location = new System.Drawing.Point(8, 176);
            this.DgrCoWorker.Name = "DgrCoWorker";
            this.DgrCoWorker.ReadOnly = true;
            this.DgrCoWorker.Size = new System.Drawing.Size(1244, 653);
            this.DgrCoWorker.TabIndex = 10;
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
            this.DgrTextBoxVorname,
            this.DgrTextBoxDateOfBirth,
            this.DgrTextBoxExternalContractor,
            this.DgrTextBoxSubcontractor,
            this.DgrTextBoxSurnameCoordinator,
            this.DgrTextBoxSupervisor});
            this.DgrTableStyleSummary.HeaderFont = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DgrTableStyleSummary.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.DgrTableStyleSummary.MappingName = "RTTabCoWorker";
            this.DgrTableStyleSummary.PreferredColumnWidth = 200;
            this.DgrTableStyleSummary.ReadOnly = true;
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
            this.DgrTextBoxStatus.Width = 90;
            // 
            // DgrTextBoxSurname
            // 
            this.DgrTextBoxSurname.Format = "";
            this.DgrTextBoxSurname.FormatInfo = null;
            this.DgrTextBoxSurname.HeaderText = "Nachname";
            this.DgrTextBoxSurname.MappingName = "Surname";
            this.DgrTextBoxSurname.NullText = "";
            this.DgrTextBoxSurname.Width = 150;
            // 
            // DgrTextBoxVorname
            // 
            this.DgrTextBoxVorname.Format = "";
            this.DgrTextBoxVorname.FormatInfo = null;
            this.DgrTextBoxVorname.HeaderText = "Vorname";
            this.DgrTextBoxVorname.MappingName = "Firstname";
            this.DgrTextBoxVorname.NullText = "";
            this.DgrTextBoxVorname.Width = 150;
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
            // DgrTextBoxExternalContractor
            // 
            this.DgrTextBoxExternalContractor.Format = "";
            this.DgrTextBoxExternalContractor.FormatInfo = null;
            this.DgrTextBoxExternalContractor.HeaderText = "Fremdfirma";
            this.DgrTextBoxExternalContractor.MappingName = "ExContractorName";
            this.DgrTextBoxExternalContractor.NullText = "";
            this.DgrTextBoxExternalContractor.Width = 180;
            // 
            // DgrTextBoxSubcontractor
            // 
            this.DgrTextBoxSubcontractor.Format = "";
            this.DgrTextBoxSubcontractor.FormatInfo = null;
            this.DgrTextBoxSubcontractor.HeaderText = "Subfirma";
            this.DgrTextBoxSubcontractor.MappingName = "SubContractor";
            this.DgrTextBoxSubcontractor.NullText = "";
            this.DgrTextBoxSubcontractor.Width = 180;
            // 
            // DgrTextBoxSurnameCoordinator
            // 
            this.DgrTextBoxSurnameCoordinator.Format = "";
            this.DgrTextBoxSurnameCoordinator.FormatInfo = null;
            this.DgrTextBoxSurnameCoordinator.HeaderText = "Koordinator";
            this.DgrTextBoxSurnameCoordinator.MappingName = "CoordNameAndTel";
            this.DgrTextBoxSurnameCoordinator.NullText = "";
            this.DgrTextBoxSurnameCoordinator.Width = 180;
            // 
            // DgrTextBoxSupervisor
            // 
            this.DgrTextBoxSupervisor.Format = "";
            this.DgrTextBoxSupervisor.FormatInfo = null;
            this.DgrTextBoxSupervisor.HeaderText = "Bauleiter";
            this.DgrTextBoxSupervisor.MappingName = "SuperNameAndTel";
            this.DgrTextBoxSupervisor.NullText = "";
            this.DgrTextBoxSupervisor.Width = 170;
            // 
            // LblMaskTitle
            // 
            this.LblMaskTitle.BackColor = System.Drawing.Color.White;
            this.LblMaskTitle.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMaskTitle.Location = new System.Drawing.Point(515, 8);
            this.LblMaskTitle.Name = "LblMaskTitle";
            this.LblMaskTitle.Size = new System.Drawing.Size(256, 32);
            this.LblMaskTitle.TabIndex = 19;
            this.LblMaskTitle.Text = "FPASS - Archiv";
            // 
            // BtnSearch
            // 
            this.BtnSearch.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSearch.Location = new System.Drawing.Point(1091, 18);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(145, 30);
            this.BtnSearch.TabIndex = 8;
            this.BtnSearch.Text = "&Suchen";
            this.TooSearch.SetToolTip(this.BtnSearch, "Startet den Suchvorgang");
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // LblSearchSurname
            // 
            this.LblSearchSurname.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSearchSurname.Location = new System.Drawing.Point(393, 32);
            this.LblSearchSurname.Name = "LblSearchSurname";
            this.LblSearchSurname.Size = new System.Drawing.Size(72, 16);
            this.LblSearchSurname.TabIndex = 10;
            this.LblSearchSurname.Text = "Nachname";
            // 
            // LblSearchExternalContractor
            // 
            this.LblSearchExternalContractor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSearchExternalContractor.Location = new System.Drawing.Point(21, 64);
            this.LblSearchExternalContractor.Name = "LblSearchExternalContractor";
            this.LblSearchExternalContractor.Size = new System.Drawing.Size(72, 16);
            this.LblSearchExternalContractor.TabIndex = 5;
            this.LblSearchExternalContractor.Text = "Fremdfirma";
            // 
            // LblSearchFirstname
            // 
            this.LblSearchFirstname.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSearchFirstname.Location = new System.Drawing.Point(393, 64);
            this.LblSearchFirstname.Name = "LblSearchFirstname";
            this.LblSearchFirstname.Size = new System.Drawing.Size(72, 16);
            this.LblSearchFirstname.TabIndex = 11;
            this.LblSearchFirstname.Text = "Vorname";
            // 
            // CboSearchCoordinator
            // 
            this.CboSearchCoordinator.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboSearchCoordinator.ItemHeight = 15;
            this.CboSearchCoordinator.Location = new System.Drawing.Point(101, 30);
            this.CboSearchCoordinator.Name = "CboSearchCoordinator";
            this.CboSearchCoordinator.Size = new System.Drawing.Size(210, 23);
            this.CboSearchCoordinator.TabIndex = 1;
            // 
            // TxtSearchSurname
            // 
            this.TxtSearchSurname.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSearchSurname.Location = new System.Drawing.Point(468, 30);
            this.TxtSearchSurname.Name = "TxtSearchSurname";
            this.TxtSearchSurname.Size = new System.Drawing.Size(210, 21);
            this.TxtSearchSurname.TabIndex = 4;
            // 
            // LblSearchCoordinator
            // 
            this.LblSearchCoordinator.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSearchCoordinator.Location = new System.Drawing.Point(21, 32);
            this.LblSearchCoordinator.Name = "LblSearchCoordinator";
            this.LblSearchCoordinator.Size = new System.Drawing.Size(72, 16);
            this.LblSearchCoordinator.TabIndex = 7;
            this.LblSearchCoordinator.Text = "Koordinator";
            // 
            // TxtSearchFirstname
            // 
            this.TxtSearchFirstname.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSearchFirstname.Location = new System.Drawing.Point(468, 62);
            this.TxtSearchFirstname.Name = "TxtSearchFirstname";
            this.TxtSearchFirstname.Size = new System.Drawing.Size(210, 21);
            this.TxtSearchFirstname.TabIndex = 5;
            // 
            // CboSearchExternalContractor
            // 
            this.CboSearchExternalContractor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboSearchExternalContractor.ItemHeight = 15;
            this.CboSearchExternalContractor.Location = new System.Drawing.Point(101, 62);
            this.CboSearchExternalContractor.Name = "CboSearchExternalContractor";
            this.CboSearchExternalContractor.Size = new System.Drawing.Size(210, 23);
            this.CboSearchExternalContractor.TabIndex = 2;
            this.CboSearchExternalContractor.SelectedIndexChanged += new System.EventHandler(this.CboSearchExternalContractor_SelectedIndexChanged);
            // 
            // CboSearchSubcontractor
            // 
            this.CboSearchSubcontractor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboSearchSubcontractor.ItemHeight = 15;
            this.CboSearchSubcontractor.Location = new System.Drawing.Point(823, 62);
            this.CboSearchSubcontractor.Name = "CboSearchSubcontractor";
            this.CboSearchSubcontractor.Size = new System.Drawing.Size(210, 23);
            this.CboSearchSubcontractor.Sorted = true;
            this.CboSearchSubcontractor.TabIndex = 7;
            // 
            // LblSearchSupervisor
            // 
            this.LblSearchSupervisor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSearchSupervisor.Location = new System.Drawing.Point(723, 32);
            this.LblSearchSupervisor.Name = "LblSearchSupervisor";
            this.LblSearchSupervisor.Size = new System.Drawing.Size(96, 16);
            this.LblSearchSupervisor.TabIndex = 15;
            this.LblSearchSupervisor.Text = "Baustellenleiter";
            // 
            // LblSearchSubcontractor
            // 
            this.LblSearchSubcontractor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSearchSubcontractor.Location = new System.Drawing.Point(723, 64);
            this.LblSearchSubcontractor.Name = "LblSearchSubcontractor";
            this.LblSearchSubcontractor.Size = new System.Drawing.Size(72, 18);
            this.LblSearchSubcontractor.TabIndex = 8;
            this.LblSearchSubcontractor.Text = "Subfirma";
            // 
            // PnlSearch
            // 
            this.PnlSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlSearch.Controls.Add(this.BtnClearMask);
            this.PnlSearch.Controls.Add(this.BtnSearchExternalContractor);
            this.PnlSearch.Controls.Add(this.CboSearchSupervisor);
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
            this.PnlSearch.Location = new System.Drawing.Point(5, 56);
            this.PnlSearch.Name = "PnlSearch";
            this.PnlSearch.Size = new System.Drawing.Size(1257, 104);
            this.PnlSearch.TabIndex = 0;
            // 
            // BtnClearMask
            // 
            this.BtnClearMask.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClearMask.Location = new System.Drawing.Point(1091, 58);
            this.BtnClearMask.Name = "BtnClearMask";
            this.BtnClearMask.Size = new System.Drawing.Size(145, 30);
            this.BtnClearMask.TabIndex = 9;
            this.BtnClearMask.Text = "&Maske leeren";
            this.TooClearMask.SetToolTip(this.BtnClearMask, "Verwirft alle die bereits eingegebenen Suchkriterien");
            this.BtnClearMask.Click += new System.EventHandler(this.BtnClearMask_Click);
            // 
            // BtnSearchExternalContractor
            // 
            this.BtnSearchExternalContractor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSearchExternalContractor.Location = new System.Drawing.Point(317, 64);
            this.BtnSearchExternalContractor.Name = "BtnSearchExternalContractor";
            this.BtnSearchExternalContractor.Size = new System.Drawing.Size(21, 21);
            this.BtnSearchExternalContractor.TabIndex = 3;
            this.BtnSearchExternalContractor.Text = "?&F";
            this.TooExContractor.SetToolTip(this.BtnSearchExternalContractor, "Öffnet die Maske Fremdfirmensuche");
            this.BtnSearchExternalContractor.Click += new System.EventHandler(this.BtnSearchExternalContractor_Click);
            // 
            // CboSearchSupervisor
            // 
            this.CboSearchSupervisor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboSearchSupervisor.ItemHeight = 15;
            this.CboSearchSupervisor.Location = new System.Drawing.Point(823, 30);
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
            this.LblSearch.Size = new System.Drawing.Size(88, 16);
            this.LblSearch.TabIndex = 18;
            this.LblSearch.Text = " Suche Archiv";
            // 
            // BtnDetails
            // 
            this.BtnDetails.Enabled = false;
            this.BtnDetails.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDetails.Location = new System.Drawing.Point(968, 856);
            this.BtnDetails.Name = "BtnDetails";
            this.BtnDetails.Size = new System.Drawing.Size(140, 32);
            this.BtnDetails.TabIndex = 11;
            this.BtnDetails.Tag = "";
            this.BtnDetails.Text = "&Details FFMA";
            this.TooDetails.SetToolTip(this.BtnDetails, "Zeigt weitere Daten des archivierten Fremdfirmenmitarbeiters an");
            this.BtnDetails.Click += new System.EventHandler(this.BtnDetails_Click);
            // 
            // BtnBackTo
            // 
            this.BtnBackTo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBackTo.Location = new System.Drawing.Point(1120, 856);
            this.BtnBackTo.Name = "BtnBackTo";
            this.BtnBackTo.Size = new System.Drawing.Size(140, 32);
            this.BtnBackTo.TabIndex = 12;
            this.BtnBackTo.Tag = "";
            this.BtnBackTo.Text = "&Zurück";
            this.TooBackTo.SetToolTip(this.BtnBackTo, "Zuürck zur Übersichtsmaske");
            this.BtnBackTo.Click += new System.EventHandler(this.BtnBackTo_Click);
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
            // FrmArchive
            // 
            this.AcceptButton = this.BtnSearch;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(1264, 921);
            this.Controls.Add(this.LblMaskTitle);
            this.Controls.Add(this.BtnHelp);
            this.Controls.Add(this.BtnBackTo);
            this.Controls.Add(this.LblSearch);
            this.Controls.Add(this.BtnDetails);
            this.Controls.Add(this.PnlSearch);
            this.Controls.Add(this.DgrCoWorker);
            this.Name = "FrmArchive";
            this.Text = "FPASS - Übersicht Fremdfirmenmitarbeiter";
            this.Controls.SetChildIndex(this.DgrCoWorker, 0);
            this.Controls.SetChildIndex(this.PnlSearch, 0);
            this.Controls.SetChildIndex(this.BtnDetails, 0);
            this.Controls.SetChildIndex(this.LblSearch, 0);
            this.Controls.SetChildIndex(this.BtnBackTo, 0);
            this.Controls.SetChildIndex(this.BtnHelp, 0);
            this.Controls.SetChildIndex(this.LblBaseHead, 0);
            this.Controls.SetChildIndex(this.StbBase, 0);
            this.Controls.SetChildIndex(this.LblMaskTitle, 0);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
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

		#endregion //End of Accessors

		#region Methods 


		internal override void FillLists()
		{ 
			FillSupervisor();
			FillSubcontractor("0");
			FillCoordinator("0");
			FillExternalContractor("0");
		}


		private void SetAuthorization() 
		{

		}

		internal override void PreClose()
		{
			mCboContractorIsLeading = false;
			mCboCoordinatorIsLeading = false;
			ClearFields();

		}

		internal override void PreShow() 
		{
			
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

		
		private ArchiveController GetMyController() 
		{
			return ((ArchiveController)mController);
		}

		//clear fields
		private void ClearFields()
		{
			this.CboSearchCoordinator.Text		  = String.Empty;
			this.CboSearchExternalContractor.Text = String.Empty;
			this.CboSearchSubcontractor.Text = String.Empty;
			this.CboSearchSupervisor.Text  = String.Empty;
			this.TxtSearchFirstname.Text = String.Empty;
			this.TxtSearchSurname.Text = String.Empty;
			this.DgrCoWorker.DataSource	= null;
		}

		//fills combobox externalContractor: values from production and from archive
		public void FillExternalContractor(String pID)
		{
			ArrayList	allContractors = new ArrayList();
			allContractors.AddRange( this.GetContractorList(pID) );
			allContractors.AddRange ( FPASSLovsSingleton.GetInstance().ContractorsArchive );
			this.CboSearchExternalContractor.DataSource = allContractors;
			this.CboSearchExternalContractor.DisplayMember = "ContractorName";
			this.CboSearchExternalContractor.ValueMember = "ContractorID";
		}

		//fills combobox supervisor: values from production and from archive
		public void FillSupervisor()
		{
			this.CboSearchSupervisor.DataSource    = FPASSLovsSingleton.GetInstance().AllSupervisors;
			this.CboSearchSupervisor.DisplayMember = "SuperBothNamesAndEXCO";
			this.CboSearchSupervisor.ValueMember   = "EXCOID";

		}

		//fills combobox coordinator: values from production and from archive
		public void FillCoordinator(String pID)
		{
			this.CboSearchCoordinator.DataSource = FPASSLovsSingleton.GetInstance().AllCoordinators;
			this.CboSearchCoordinator.DisplayMember = "CoordFullNameTel";
			this.CboSearchCoordinator.ValueMember = "CoordID";
		}

		//fills combobox subcontractor: values from production and from archive
		public void FillSubcontractor(String pSubContractorID)
		{
			ArrayList allSubContractors = new ArrayList();
			allSubContractors.AddRange( this.GetContractorList(pSubContractorID) );
			allSubContractors.AddRange ( FPASSLovsSingleton.GetInstance().ContractorsArchive );
			this.CboSearchSubcontractor.DataSource = allSubContractors;
			this.CboSearchSubcontractor.DisplayMember = "ContractorName";
			this.CboSearchSubcontractor.ValueMember = "ContractorID";
		}

		private void TableNavigated()
		{
			int rowIndex = this.DgrCoWorker.CurrentRowIndex;
			if(-1 < rowIndex)
			{
				mCurrentFFMAId = Convert.ToDecimal(this.DgrCoWorker[rowIndex, 0].ToString());
			}
		}

		internal override void ReFillContractorList(String pContractorID) 
		{
			FillCoordinator("0");
			FillExternalContractor("0");
			CboSearchExternalContractor.SelectedValue =  
				Convert.ToDecimal( pContractorID );
		}

		#endregion Methods // End of Methods

		#region Events

		/// <summary>
		/// Is invoked when the datagrid was navigated by the user. 
		/// After selection of another data row the current id of the selected business object within the
		/// table must be updated.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DgrCoWorker_CurrentCellChanged(object sender, System.EventArgs e)
		{
			this.TableNavigated();
		}

		/// <summary>
		/// 13.11.03
		/// Is invoked when the datagrid is navigated by the user or grid is sorted
		/// but also many times when grid is being loaded, hence boolean variable
		/// </summary>
		private void DgrCoWorker_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			if ( !gridIsLoading )
			{
				if ( null != this.DgrCoWorker.DataSource && this.DgrCoWorker.VisibleRowCount > 0 )
				{
					this.TableNavigated();
				}
			}	
		}

		private void BtnDetails_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventOpenProcessDialog(mCurrentFFMAId);
		}

		private void BtnBackTo_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleCloseDialog();
		}

		private void BtnSearchExternalContractor_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventOpenSearchExternalContractorDialog();
		}
		
		private void BtnSearch_Click(object sender, System.EventArgs e)
		{
			mCurrentFFMAId = -1;
			gridIsLoading = true;
			GetMyController().HandleEventBtnSearchCoWorker();
			gridIsLoading = false;
		}

		
		private void CboSearchExternalContractor_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		/// <summary>
		/// Clears fields in form.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnClearMask_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventClearForm();

		}

        /// <summary>
        /// When user double-clicks on grid: opens current coworker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgrCoWorker_DoubleClick(object sender, EventArgs e)
        {
            GetMyController().HandleEventOpenProcessDialog(mCurrentFFMAId);
        }

        #endregion Events

    }
}

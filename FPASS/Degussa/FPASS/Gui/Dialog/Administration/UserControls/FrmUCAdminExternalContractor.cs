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
using Degussa.FPASS.Db.DataSets;
using Degussa.FPASS.Util.Exceptions;
using Degussa.FPASS.Util.UserManagement;


namespace Degussa.FPASS.Gui.Dialog.Administration.UserControls
{
	/// <summary>
	/// A FrmUCAdminExternalContractor is one of the usercontrols 
	/// of the FrmAdministration.
	/// FrmUCAdminExternalContractor extends from the System.Windows.Forms.UserControl.
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
	public class FrmUCAdminExternalContractor : System.Windows.Forms.UserControl
	{
		#region Members

		//panels
		internal System.Windows.Forms.Panel PnlSearch;
		internal System.Windows.Forms.Panel PnlEdit;

		//labels
		internal System.Windows.Forms.Label LblSearch;
		internal System.Windows.Forms.Label LblSearchSupervisor;
		internal System.Windows.Forms.Label LblSearchCity;
		internal System.Windows.Forms.Label LblSearchExternalContractor;
		internal System.Windows.Forms.Label LblEdit;
		internal System.Windows.Forms.Label LblEditMobil;
		internal System.Windows.Forms.Label LblEditFax;
		internal System.Windows.Forms.Label LblEditPhone;
		internal System.Windows.Forms.Label LblEditSupervisorFirstname;
		internal System.Windows.Forms.Label LblEditSupervisorSurname;
		internal System.Windows.Forms.Label LblEditStreet;
		internal System.Windows.Forms.Label LblEditCountry;
		internal System.Windows.Forms.Label LblEditPostalCode;
		internal System.Windows.Forms.Label LblEditCity;
		internal System.Windows.Forms.Label LblEditExternalContractor;
	
		//textboxes
		internal System.Windows.Forms.TextBox TxtEditStreet;
		internal System.Windows.Forms.TextBox TxtEditCountry;
		internal System.Windows.Forms.TextBox TxtEditPostalCode;
		internal System.Windows.Forms.TextBox TxtEditCity;
		internal System.Windows.Forms.TextBox TxtEditExternalContractor;
		internal System.Windows.Forms.TextBox TxtEditMobil;
		internal System.Windows.Forms.TextBox TxtEditFax;
		internal System.Windows.Forms.TextBox TxtEditPhone;
		internal System.Windows.Forms.TextBox TxtEditSupervisorFirstname;
		internal System.Windows.Forms.TextBox TxtEditSupervisorSurname;

		//comboboxes
		internal System.Windows.Forms.ComboBox CboSearchSupervisor;
		internal System.Windows.Forms.ComboBox CboSearchCity;
		internal System.Windows.Forms.ComboBox CboSearchExternalContractor;

		//buttons
		internal System.Windows.Forms.Button BtnSearch;
		internal System.Windows.Forms.Button BtnSearchExternalContractor;
		internal System.Windows.Forms.Button BtnCancel;
		internal System.Windows.Forms.Button BtnBackTo;
		internal System.Windows.Forms.Button BtnSave;
		internal System.Windows.Forms.Button BtnNew;
		internal System.Windows.Forms.Button BtnDelete;

		//tooltips
		private System.Windows.Forms.ToolTip TooSearch;
		private System.Windows.Forms.ToolTip TooBackTo;
		private System.Windows.Forms.ToolTip TooCancel;
		private System.Windows.Forms.ToolTip TooDelete;
		private System.Windows.Forms.ToolTip TooSave;
		private System.Windows.Forms.ToolTip TooNew;
		private System.Windows.Forms.ToolTip TooExContractor;

		//tables
		internal System.Windows.Forms.DataGrid DgrExternalContractor;
		private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn2;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn3;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn4;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn5;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn6;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn7;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn8;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn9;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn10;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn11;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn12;

		//other
		protected DSExContractor mDSExContractor;
		private System.ComponentModel.IContainer components;

		// Need to know which UserControl called FrmSearchExcontractor
		private int mSourceCallSearchExco = 1;

		/// <summary>
		/// holds the id of the current admin object selected in the displayed table
		/// </summary>
		private int mCurrentAdminRec = -1;
		/// <summary>
		/// holds the id of the current excontractor selected in the displayed table
		/// </summary>
		private string mCurrentEXCOName = "";
		/// <summary>
		/// Have data on form changed? This has an accessor and is checked in <see cref="AdministrationModel"/> AdministrationModel
		/// </summary>
		private bool mContentChanged = false;

		/// <summary>
		/// Used to stop CellChanged event on datagrid firing when grid is sorted
		/// </summary>
		private bool mGridIsSorted = false;
        internal TextBox TxtEditDebitNo;
        internal Label LblEditDebitNo;
        internal Label label1;

		/// <summary>
		/// used to hold the Controller of this triad
		/// </summary>
		internal	AbstractController	mController;

		#endregion //End of Members

		#region Constructors
		public FrmUCAdminExternalContractor()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

	//		InitView();

		}

		private void InitView() 
		{
			
		}

		#endregion // End of Constructors

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.LblSearch = new System.Windows.Forms.Label();
            this.PnlSearch = new System.Windows.Forms.Panel();
            this.CboSearchSupervisor = new System.Windows.Forms.ComboBox();
            this.LblSearchSupervisor = new System.Windows.Forms.Label();
            this.BtnSearch = new System.Windows.Forms.Button();
            this.CboSearchCity = new System.Windows.Forms.ComboBox();
            this.CboSearchExternalContractor = new System.Windows.Forms.ComboBox();
            this.LblSearchCity = new System.Windows.Forms.Label();
            this.LblSearchExternalContractor = new System.Windows.Forms.Label();
            this.BtnSearchExternalContractor = new System.Windows.Forms.Button();
            this.DgrExternalContractor = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn3 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn4 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn5 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn6 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn7 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn11 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn8 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn9 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn10 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn12 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.LblEdit = new System.Windows.Forms.Label();
            this.PnlEdit = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtEditDebitNo = new System.Windows.Forms.TextBox();
            this.LblEditDebitNo = new System.Windows.Forms.Label();
            this.TxtEditStreet = new System.Windows.Forms.TextBox();
            this.TxtEditCountry = new System.Windows.Forms.TextBox();
            this.TxtEditPostalCode = new System.Windows.Forms.TextBox();
            this.TxtEditCity = new System.Windows.Forms.TextBox();
            this.TxtEditExternalContractor = new System.Windows.Forms.TextBox();
            this.LblEditMobil = new System.Windows.Forms.Label();
            this.TxtEditMobil = new System.Windows.Forms.TextBox();
            this.LblEditSupervisorFirstname = new System.Windows.Forms.Label();
            this.LblEditFax = new System.Windows.Forms.Label();
            this.LblEditSupervisorSurname = new System.Windows.Forms.Label();
            this.TxtEditFax = new System.Windows.Forms.TextBox();
            this.LblEditPhone = new System.Windows.Forms.Label();
            this.TxtEditSupervisorSurname = new System.Windows.Forms.TextBox();
            this.TxtEditSupervisorFirstname = new System.Windows.Forms.TextBox();
            this.TxtEditPhone = new System.Windows.Forms.TextBox();
            this.LblEditStreet = new System.Windows.Forms.Label();
            this.LblEditCountry = new System.Windows.Forms.Label();
            this.LblEditPostalCode = new System.Windows.Forms.Label();
            this.LblEditCity = new System.Windows.Forms.Label();
            this.LblEditExternalContractor = new System.Windows.Forms.Label();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.BtnBackTo = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnNew = new System.Windows.Forms.Button();
            this.BtnDelete = new System.Windows.Forms.Button();
            this.TooSearch = new System.Windows.Forms.ToolTip(this.components);
            this.TooBackTo = new System.Windows.Forms.ToolTip(this.components);
            this.TooCancel = new System.Windows.Forms.ToolTip(this.components);
            this.TooDelete = new System.Windows.Forms.ToolTip(this.components);
            this.TooSave = new System.Windows.Forms.ToolTip(this.components);
            this.TooNew = new System.Windows.Forms.ToolTip(this.components);
            this.TooExContractor = new System.Windows.Forms.ToolTip(this.components);
            this.PnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgrExternalContractor)).BeginInit();
            this.PnlEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // LblSearch
            // 
            this.LblSearch.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.LblSearch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LblSearch.Location = new System.Drawing.Point(48, 11);
            this.LblSearch.Name = "LblSearch";
            this.LblSearch.Size = new System.Drawing.Size(48, 16);
            this.LblSearch.TabIndex = 31;
            this.LblSearch.Text = "Suche";
            // 
            // PnlSearch
            // 
            this.PnlSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlSearch.Controls.Add(this.CboSearchSupervisor);
            this.PnlSearch.Controls.Add(this.LblSearchSupervisor);
            this.PnlSearch.Controls.Add(this.BtnSearch);
            this.PnlSearch.Controls.Add(this.CboSearchCity);
            this.PnlSearch.Controls.Add(this.CboSearchExternalContractor);
            this.PnlSearch.Controls.Add(this.LblSearchCity);
            this.PnlSearch.Controls.Add(this.LblSearchExternalContractor);
            this.PnlSearch.Controls.Add(this.BtnSearchExternalContractor);
            this.PnlSearch.Font = new System.Drawing.Font("Arial", 9F);
            this.PnlSearch.Location = new System.Drawing.Point(32, 19);
            this.PnlSearch.Name = "PnlSearch";
            this.PnlSearch.Size = new System.Drawing.Size(1187, 64);
            this.PnlSearch.TabIndex = 0;
            // 
            // CboSearchSupervisor
            // 
            this.CboSearchSupervisor.Location = new System.Drawing.Point(770, 22);
            this.CboSearchSupervisor.Name = "CboSearchSupervisor";
            this.CboSearchSupervisor.Size = new System.Drawing.Size(210, 23);
            this.CboSearchSupervisor.TabIndex = 4;
            // 
            // LblSearchSupervisor
            // 
            this.LblSearchSupervisor.Font = new System.Drawing.Font("Arial", 9F);
            this.LblSearchSupervisor.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LblSearchSupervisor.Location = new System.Drawing.Point(671, 24);
            this.LblSearchSupervisor.Name = "LblSearchSupervisor";
            this.LblSearchSupervisor.Size = new System.Drawing.Size(97, 23);
            this.LblSearchSupervisor.TabIndex = 62;
            this.LblSearchSupervisor.Text = "Baustellenleiter";
            // 
            // BtnSearch
            // 
            this.BtnSearch.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSearch.Location = new System.Drawing.Point(1055, 17);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(100, 30);
            this.BtnSearch.TabIndex = 5;
            this.BtnSearch.Text = "&Suche";
            this.TooSearch.SetToolTip(this.BtnSearch, "Löst den Suchvorgang aus");
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // CboSearchCity
            // 
            this.CboSearchCity.Location = new System.Drawing.Point(426, 22);
            this.CboSearchCity.Name = "CboSearchCity";
            this.CboSearchCity.Size = new System.Drawing.Size(210, 23);
            this.CboSearchCity.TabIndex = 3;
            // 
            // CboSearchExternalContractor
            // 
            this.CboSearchExternalContractor.Location = new System.Drawing.Point(97, 22);
            this.CboSearchExternalContractor.Name = "CboSearchExternalContractor";
            this.CboSearchExternalContractor.Size = new System.Drawing.Size(219, 23);
            this.CboSearchExternalContractor.TabIndex = 1;
            // 
            // LblSearchCity
            // 
            this.LblSearchCity.Font = new System.Drawing.Font("Arial", 9F);
            this.LblSearchCity.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LblSearchCity.Location = new System.Drawing.Point(391, 24);
            this.LblSearchCity.Name = "LblSearchCity";
            this.LblSearchCity.Size = new System.Drawing.Size(29, 23);
            this.LblSearchCity.TabIndex = 61;
            this.LblSearchCity.Text = "Ort";
            // 
            // LblSearchExternalContractor
            // 
            this.LblSearchExternalContractor.Font = new System.Drawing.Font("Arial", 9F);
            this.LblSearchExternalContractor.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LblSearchExternalContractor.Location = new System.Drawing.Point(21, 24);
            this.LblSearchExternalContractor.Name = "LblSearchExternalContractor";
            this.LblSearchExternalContractor.Size = new System.Drawing.Size(136, 23);
            this.LblSearchExternalContractor.TabIndex = 60;
            this.LblSearchExternalContractor.Text = "Fremdfirma";
            // 
            // BtnSearchExternalContractor
            // 
            this.BtnSearchExternalContractor.Location = new System.Drawing.Point(322, 24);
            this.BtnSearchExternalContractor.Name = "BtnSearchExternalContractor";
            this.BtnSearchExternalContractor.Size = new System.Drawing.Size(21, 21);
            this.BtnSearchExternalContractor.TabIndex = 2;
            this.BtnSearchExternalContractor.Text = "?&F";
            this.TooExContractor.SetToolTip(this.BtnSearchExternalContractor, "Öffnet die Maske Fremdfirmensuche");
            this.BtnSearchExternalContractor.Click += new System.EventHandler(this.BtnSearchExternalContractor_Click);
            // 
            // DgrExternalContractor
            // 
            this.DgrExternalContractor.AllowSorting = false;
            this.DgrExternalContractor.CaptionBackColor = System.Drawing.Color.SteelBlue;
            this.DgrExternalContractor.DataMember = "";
            this.DgrExternalContractor.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.DgrExternalContractor.Location = new System.Drawing.Point(32, 99);
            this.DgrExternalContractor.Name = "DgrExternalContractor";
            this.DgrExternalContractor.PreferredColumnWidth = 100;
            this.DgrExternalContractor.ReadOnly = true;
            this.DgrExternalContractor.Size = new System.Drawing.Size(1187, 264);
            this.DgrExternalContractor.TabIndex = 6;
            this.DgrExternalContractor.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.dataGridTableStyle1});
            this.DgrExternalContractor.CurrentCellChanged += new System.EventHandler(this.DgrExternalContractor_CurrentCellChanged);
            this.DgrExternalContractor.Enter += new System.EventHandler(this.DgrExternalContractor_Enter);
            this.DgrExternalContractor.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DgrExternalContractor_MouseDown);
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.DataGrid = this.DgrExternalContractor;
            this.dataGridTableStyle1.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.dataGridTextBoxColumn1,
            this.dataGridTextBoxColumn2,
            this.dataGridTextBoxColumn3,
            this.dataGridTextBoxColumn4,
            this.dataGridTextBoxColumn5,
            this.dataGridTextBoxColumn6,
            this.dataGridTextBoxColumn7,
            this.dataGridTextBoxColumn11,
            this.dataGridTextBoxColumn8,
            this.dataGridTextBoxColumn9,
            this.dataGridTextBoxColumn10,
            this.dataGridTextBoxColumn12});
            this.dataGridTableStyle1.HeaderFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGridTableStyle1.MappingName = "FPASS_EXCONTRACTOR";
            this.dataGridTableStyle1.PreferredColumnWidth = 90;
            this.dataGridTableStyle1.ReadOnly = true;
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            this.dataGridTextBoxColumn1.HeaderText = "EXCO_ID";
            this.dataGridTextBoxColumn1.MappingName = "EXCO_ID";
            this.dataGridTextBoxColumn1.NullText = "";
            this.dataGridTextBoxColumn1.Width = 0;
            // 
            // dataGridTextBoxColumn2
            // 
            this.dataGridTextBoxColumn2.Format = "";
            this.dataGridTextBoxColumn2.FormatInfo = null;
            this.dataGridTextBoxColumn2.HeaderText = "Name";
            this.dataGridTextBoxColumn2.MappingName = "EXCO_NAME";
            this.dataGridTextBoxColumn2.NullText = "";
            this.dataGridTextBoxColumn2.Width = 130;
            // 
            // dataGridTextBoxColumn3
            // 
            this.dataGridTextBoxColumn3.Format = "";
            this.dataGridTextBoxColumn3.FormatInfo = null;
            this.dataGridTextBoxColumn3.HeaderText = "Debit-Nr.";
            this.dataGridTextBoxColumn3.MappingName = "EXCO_DEBITNO";
            this.dataGridTextBoxColumn3.NullText = "";
            this.dataGridTextBoxColumn3.Width = 95;
            // 
            // dataGridTextBoxColumn4
            // 
            this.dataGridTextBoxColumn4.Format = "";
            this.dataGridTextBoxColumn4.FormatInfo = null;
            this.dataGridTextBoxColumn4.HeaderText = "Ort";
            this.dataGridTextBoxColumn4.MappingName = "EXCO_CITY";
            this.dataGridTextBoxColumn4.NullText = "";
            this.dataGridTextBoxColumn4.Width = 120;
            // 
            // dataGridTextBoxColumn5
            // 
            this.dataGridTextBoxColumn5.Format = "";
            this.dataGridTextBoxColumn5.FormatInfo = null;
            this.dataGridTextBoxColumn5.HeaderText = "PLZ";
            this.dataGridTextBoxColumn5.MappingName = "EXCO_POSTCODE";
            this.dataGridTextBoxColumn5.NullText = "";
            this.dataGridTextBoxColumn5.Width = 95;
            // 
            // dataGridTextBoxColumn6
            // 
            this.dataGridTextBoxColumn6.Format = "";
            this.dataGridTextBoxColumn6.FormatInfo = null;
            this.dataGridTextBoxColumn6.HeaderText = "Strasse";
            this.dataGridTextBoxColumn6.MappingName = "EXCO_STREET";
            this.dataGridTextBoxColumn6.NullText = "";
            this.dataGridTextBoxColumn6.Width = 150;
            // 
            // dataGridTextBoxColumn7
            // 
            this.dataGridTextBoxColumn7.Format = "";
            this.dataGridTextBoxColumn7.FormatInfo = null;
            this.dataGridTextBoxColumn7.HeaderText = "Bauleiter Nachname";
            this.dataGridTextBoxColumn7.MappingName = "EXCO_SUPERSURNAME";
            this.dataGridTextBoxColumn7.NullText = "";
            this.dataGridTextBoxColumn7.Width = 120;
            // 
            // dataGridTextBoxColumn11
            // 
            this.dataGridTextBoxColumn11.Format = "";
            this.dataGridTextBoxColumn11.FormatInfo = null;
            this.dataGridTextBoxColumn11.HeaderText = "Bauleiter Vorname";
            this.dataGridTextBoxColumn11.MappingName = "EXCO_SUPERFIRSTNAME";
            this.dataGridTextBoxColumn11.NullText = "";
            this.dataGridTextBoxColumn11.Width = 120;
            // 
            // dataGridTextBoxColumn8
            // 
            this.dataGridTextBoxColumn8.Format = "";
            this.dataGridTextBoxColumn8.FormatInfo = null;
            this.dataGridTextBoxColumn8.HeaderText = "Telefon";
            this.dataGridTextBoxColumn8.MappingName = "EXCO_TEL";
            this.dataGridTextBoxColumn8.NullText = "";
            this.dataGridTextBoxColumn8.Width = 110;
            // 
            // dataGridTextBoxColumn9
            // 
            this.dataGridTextBoxColumn9.Format = "";
            this.dataGridTextBoxColumn9.FormatInfo = null;
            this.dataGridTextBoxColumn9.HeaderText = "Fax";
            this.dataGridTextBoxColumn9.MappingName = "EXCO_FAX";
            this.dataGridTextBoxColumn9.NullText = "";
            this.dataGridTextBoxColumn9.Width = 110;
            // 
            // dataGridTextBoxColumn10
            // 
            this.dataGridTextBoxColumn10.Format = "";
            this.dataGridTextBoxColumn10.FormatInfo = null;
            this.dataGridTextBoxColumn10.HeaderText = "Mobil";
            this.dataGridTextBoxColumn10.MappingName = "EXCO_MOBILEPHONE";
            this.dataGridTextBoxColumn10.NullText = "";
            this.dataGridTextBoxColumn10.Width = 110;
            // 
            // dataGridTextBoxColumn12
            // 
            this.dataGridTextBoxColumn12.Format = "";
            this.dataGridTextBoxColumn12.FormatInfo = null;
            this.dataGridTextBoxColumn12.HeaderText = "Status";
            this.dataGridTextBoxColumn12.MappingName = "EXCO_STATUS";
            this.dataGridTextBoxColumn12.NullText = "";
            this.dataGridTextBoxColumn12.Width = 85;
            // 
            // LblEdit
            // 
            this.LblEdit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.LblEdit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LblEdit.Location = new System.Drawing.Point(48, 371);
            this.LblEdit.Name = "LblEdit";
            this.LblEdit.Size = new System.Drawing.Size(56, 16);
            this.LblEdit.TabIndex = 30;
            this.LblEdit.Text = "Eingabe";
            // 
            // PnlEdit
            // 
            this.PnlEdit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlEdit.Controls.Add(this.label1);
            this.PnlEdit.Controls.Add(this.TxtEditDebitNo);
            this.PnlEdit.Controls.Add(this.LblEditDebitNo);
            this.PnlEdit.Controls.Add(this.TxtEditStreet);
            this.PnlEdit.Controls.Add(this.TxtEditCountry);
            this.PnlEdit.Controls.Add(this.TxtEditPostalCode);
            this.PnlEdit.Controls.Add(this.TxtEditCity);
            this.PnlEdit.Controls.Add(this.TxtEditExternalContractor);
            this.PnlEdit.Controls.Add(this.LblEditMobil);
            this.PnlEdit.Controls.Add(this.TxtEditMobil);
            this.PnlEdit.Controls.Add(this.LblEditSupervisorFirstname);
            this.PnlEdit.Controls.Add(this.LblEditFax);
            this.PnlEdit.Controls.Add(this.LblEditSupervisorSurname);
            this.PnlEdit.Controls.Add(this.TxtEditFax);
            this.PnlEdit.Controls.Add(this.LblEditPhone);
            this.PnlEdit.Controls.Add(this.TxtEditSupervisorSurname);
            this.PnlEdit.Controls.Add(this.TxtEditSupervisorFirstname);
            this.PnlEdit.Controls.Add(this.TxtEditPhone);
            this.PnlEdit.Controls.Add(this.LblEditStreet);
            this.PnlEdit.Controls.Add(this.LblEditCountry);
            this.PnlEdit.Controls.Add(this.LblEditPostalCode);
            this.PnlEdit.Controls.Add(this.LblEditCity);
            this.PnlEdit.Controls.Add(this.LblEditExternalContractor);
            this.PnlEdit.Font = new System.Drawing.Font("Arial", 9F);
            this.PnlEdit.Location = new System.Drawing.Point(32, 379);
            this.PnlEdit.Name = "PnlEdit";
            this.PnlEdit.Size = new System.Drawing.Size(1187, 152);
            this.PnlEdit.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9F);
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(668, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 18);
            this.label1.TabIndex = 42;
            this.label1.Text = "Baustellenleiter:";
            // 
            // TxtEditDebitNo
            // 
            this.TxtEditDebitNo.Enabled = false;
            this.TxtEditDebitNo.Font = new System.Drawing.Font("Arial", 9F);
            this.TxtEditDebitNo.Location = new System.Drawing.Point(106, 41);
            this.TxtEditDebitNo.MaxLength = 20;
            this.TxtEditDebitNo.Name = "TxtEditDebitNo";
            this.TxtEditDebitNo.Size = new System.Drawing.Size(210, 21);
            this.TxtEditDebitNo.TabIndex = 9;
            this.TxtEditDebitNo.Enter += new System.EventHandler(this.TxtEditDebitNo_Enter);
            // 
            // LblEditDebitNo
            // 
            this.LblEditDebitNo.Font = new System.Drawing.Font("Arial", 9F);
            this.LblEditDebitNo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LblEditDebitNo.Location = new System.Drawing.Point(20, 44);
            this.LblEditDebitNo.Name = "LblEditDebitNo";
            this.LblEditDebitNo.Size = new System.Drawing.Size(80, 23);
            this.LblEditDebitNo.TabIndex = 41;
            this.LblEditDebitNo.Text = "Debit-Nr.";
            // 
            // TxtEditStreet
            // 
            this.TxtEditStreet.Enabled = false;
            this.TxtEditStreet.Font = new System.Drawing.Font("Arial", 9F);
            this.TxtEditStreet.Location = new System.Drawing.Point(106, 67);
            this.TxtEditStreet.MaxLength = 40;
            this.TxtEditStreet.Name = "TxtEditStreet";
            this.TxtEditStreet.Size = new System.Drawing.Size(210, 21);
            this.TxtEditStreet.TabIndex = 10;
            this.TxtEditStreet.Enter += new System.EventHandler(this.TxtEditStreet_Enter);
            // 
            // TxtEditCountry
            // 
            this.TxtEditCountry.Enabled = false;
            this.TxtEditCountry.Font = new System.Drawing.Font("Arial", 9F);
            this.TxtEditCountry.Location = new System.Drawing.Point(426, 15);
            this.TxtEditCountry.MaxLength = 30;
            this.TxtEditCountry.Name = "TxtEditCountry";
            this.TxtEditCountry.Size = new System.Drawing.Size(210, 21);
            this.TxtEditCountry.TabIndex = 13;
            this.TxtEditCountry.Enter += new System.EventHandler(this.TxtEditCountry_Enter);
            // 
            // TxtEditPostalCode
            // 
            this.TxtEditPostalCode.Enabled = false;
            this.TxtEditPostalCode.Font = new System.Drawing.Font("Arial", 9F);
            this.TxtEditPostalCode.Location = new System.Drawing.Point(106, 93);
            this.TxtEditPostalCode.MaxLength = 10;
            this.TxtEditPostalCode.Name = "TxtEditPostalCode";
            this.TxtEditPostalCode.Size = new System.Drawing.Size(210, 21);
            this.TxtEditPostalCode.TabIndex = 11;
            this.TxtEditPostalCode.Enter += new System.EventHandler(this.TxtEditPostalCode_Enter);
            // 
            // TxtEditCity
            // 
            this.TxtEditCity.Enabled = false;
            this.TxtEditCity.Font = new System.Drawing.Font("Arial", 9F);
            this.TxtEditCity.Location = new System.Drawing.Point(106, 119);
            this.TxtEditCity.MaxLength = 30;
            this.TxtEditCity.Name = "TxtEditCity";
            this.TxtEditCity.Size = new System.Drawing.Size(210, 21);
            this.TxtEditCity.TabIndex = 12;
            this.TxtEditCity.Enter += new System.EventHandler(this.TxtEditCity_Enter);
            // 
            // TxtEditExternalContractor
            // 
            this.TxtEditExternalContractor.Enabled = false;
            this.TxtEditExternalContractor.Font = new System.Drawing.Font("Arial", 9F);
            this.TxtEditExternalContractor.Location = new System.Drawing.Point(106, 15);
            this.TxtEditExternalContractor.MaxLength = 30;
            this.TxtEditExternalContractor.Name = "TxtEditExternalContractor";
            this.TxtEditExternalContractor.Size = new System.Drawing.Size(210, 21);
            this.TxtEditExternalContractor.TabIndex = 8;
            this.TxtEditExternalContractor.Enter += new System.EventHandler(this.TxtEditExternalContractor_Enter);
            // 
            // LblEditMobil
            // 
            this.LblEditMobil.Font = new System.Drawing.Font("Arial", 9F);
            this.LblEditMobil.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LblEditMobil.Location = new System.Drawing.Point(367, 99);
            this.LblEditMobil.Name = "LblEditMobil";
            this.LblEditMobil.Size = new System.Drawing.Size(53, 23);
            this.LblEditMobil.TabIndex = 39;
            this.LblEditMobil.Text = "Mobil";
            // 
            // TxtEditMobil
            // 
            this.TxtEditMobil.Enabled = false;
            this.TxtEditMobil.Font = new System.Drawing.Font("Arial", 9F);
            this.TxtEditMobil.Location = new System.Drawing.Point(426, 96);
            this.TxtEditMobil.MaxLength = 30;
            this.TxtEditMobil.Name = "TxtEditMobil";
            this.TxtEditMobil.Size = new System.Drawing.Size(210, 21);
            this.TxtEditMobil.TabIndex = 16;
            this.TxtEditMobil.Enter += new System.EventHandler(this.TxtEditMobil_Enter);
            // 
            // LblEditSupervisorFirstname
            // 
            this.LblEditSupervisorFirstname.Font = new System.Drawing.Font("Arial", 9F);
            this.LblEditSupervisorFirstname.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LblEditSupervisorFirstname.Location = new System.Drawing.Point(668, 71);
            this.LblEditSupervisorFirstname.Name = "LblEditSupervisorFirstname";
            this.LblEditSupervisorFirstname.Size = new System.Drawing.Size(80, 23);
            this.LblEditSupervisorFirstname.TabIndex = 33;
            this.LblEditSupervisorFirstname.Text = "- Vorname";
            // 
            // LblEditFax
            // 
            this.LblEditFax.Font = new System.Drawing.Font("Arial", 9F);
            this.LblEditFax.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LblEditFax.Location = new System.Drawing.Point(367, 72);
            this.LblEditFax.Name = "LblEditFax";
            this.LblEditFax.Size = new System.Drawing.Size(39, 23);
            this.LblEditFax.TabIndex = 37;
            this.LblEditFax.Text = "Fax";
            // 
            // LblEditSupervisorSurname
            // 
            this.LblEditSupervisorSurname.Font = new System.Drawing.Font("Arial", 9F);
            this.LblEditSupervisorSurname.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LblEditSupervisorSurname.Location = new System.Drawing.Point(668, 44);
            this.LblEditSupervisorSurname.Name = "LblEditSupervisorSurname";
            this.LblEditSupervisorSurname.Size = new System.Drawing.Size(80, 23);
            this.LblEditSupervisorSurname.TabIndex = 22;
            this.LblEditSupervisorSurname.Text = "- Nachname";
            // 
            // TxtEditFax
            // 
            this.TxtEditFax.Enabled = false;
            this.TxtEditFax.Font = new System.Drawing.Font("Arial", 9F);
            this.TxtEditFax.Location = new System.Drawing.Point(426, 69);
            this.TxtEditFax.MaxLength = 30;
            this.TxtEditFax.Name = "TxtEditFax";
            this.TxtEditFax.Size = new System.Drawing.Size(210, 21);
            this.TxtEditFax.TabIndex = 15;
            this.TxtEditFax.Enter += new System.EventHandler(this.TxtEditFax_Enter);
            // 
            // LblEditPhone
            // 
            this.LblEditPhone.Font = new System.Drawing.Font("Arial", 9F);
            this.LblEditPhone.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LblEditPhone.Location = new System.Drawing.Point(367, 45);
            this.LblEditPhone.Name = "LblEditPhone";
            this.LblEditPhone.Size = new System.Drawing.Size(53, 23);
            this.LblEditPhone.TabIndex = 35;
            this.LblEditPhone.Text = "Telefon";
            // 
            // TxtEditSupervisorSurname
            // 
            this.TxtEditSupervisorSurname.Enabled = false;
            this.TxtEditSupervisorSurname.Font = new System.Drawing.Font("Arial", 9F);
            this.TxtEditSupervisorSurname.Location = new System.Drawing.Point(770, 41);
            this.TxtEditSupervisorSurname.MaxLength = 30;
            this.TxtEditSupervisorSurname.Name = "TxtEditSupervisorSurname";
            this.TxtEditSupervisorSurname.Size = new System.Drawing.Size(210, 21);
            this.TxtEditSupervisorSurname.TabIndex = 17;
            this.TxtEditSupervisorSurname.TextChanged += new System.EventHandler(this.TxtEditSupervisorSurname_TextChanged);
            this.TxtEditSupervisorSurname.Enter += new System.EventHandler(this.TxtEditSupervisorSurname_Enter);
            // 
            // TxtEditSupervisorFirstname
            // 
            this.TxtEditSupervisorFirstname.Enabled = false;
            this.TxtEditSupervisorFirstname.Font = new System.Drawing.Font("Arial", 9F);
            this.TxtEditSupervisorFirstname.Location = new System.Drawing.Point(770, 68);
            this.TxtEditSupervisorFirstname.MaxLength = 30;
            this.TxtEditSupervisorFirstname.Name = "TxtEditSupervisorFirstname";
            this.TxtEditSupervisorFirstname.Size = new System.Drawing.Size(210, 21);
            this.TxtEditSupervisorFirstname.TabIndex = 18;
            this.TxtEditSupervisorFirstname.TextChanged += new System.EventHandler(this.TxtEditSupervisorFirstname_TextChanged);
            this.TxtEditSupervisorFirstname.Enter += new System.EventHandler(this.TxtEditSupervisorFirstname_Enter);
            // 
            // TxtEditPhone
            // 
            this.TxtEditPhone.Enabled = false;
            this.TxtEditPhone.Font = new System.Drawing.Font("Arial", 9F);
            this.TxtEditPhone.Location = new System.Drawing.Point(426, 42);
            this.TxtEditPhone.MaxLength = 30;
            this.TxtEditPhone.Name = "TxtEditPhone";
            this.TxtEditPhone.Size = new System.Drawing.Size(210, 21);
            this.TxtEditPhone.TabIndex = 14;
            this.TxtEditPhone.Enter += new System.EventHandler(this.TxtEditPhone_Enter);
            // 
            // LblEditStreet
            // 
            this.LblEditStreet.Font = new System.Drawing.Font("Arial", 9F);
            this.LblEditStreet.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LblEditStreet.Location = new System.Drawing.Point(20, 70);
            this.LblEditStreet.Name = "LblEditStreet";
            this.LblEditStreet.Size = new System.Drawing.Size(80, 23);
            this.LblEditStreet.TabIndex = 18;
            this.LblEditStreet.Text = "Straße";
            // 
            // LblEditCountry
            // 
            this.LblEditCountry.Font = new System.Drawing.Font("Arial", 9F);
            this.LblEditCountry.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LblEditCountry.Location = new System.Drawing.Point(367, 18);
            this.LblEditCountry.Name = "LblEditCountry";
            this.LblEditCountry.Size = new System.Drawing.Size(53, 23);
            this.LblEditCountry.TabIndex = 21;
            this.LblEditCountry.Text = "Land";
            // 
            // LblEditPostalCode
            // 
            this.LblEditPostalCode.Font = new System.Drawing.Font("Arial", 9F);
            this.LblEditPostalCode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LblEditPostalCode.Location = new System.Drawing.Point(20, 96);
            this.LblEditPostalCode.Name = "LblEditPostalCode";
            this.LblEditPostalCode.Size = new System.Drawing.Size(80, 23);
            this.LblEditPostalCode.TabIndex = 19;
            this.LblEditPostalCode.Text = "PLZ";
            // 
            // LblEditCity
            // 
            this.LblEditCity.Font = new System.Drawing.Font("Arial", 9F);
            this.LblEditCity.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LblEditCity.Location = new System.Drawing.Point(20, 122);
            this.LblEditCity.Name = "LblEditCity";
            this.LblEditCity.Size = new System.Drawing.Size(80, 23);
            this.LblEditCity.TabIndex = 20;
            this.LblEditCity.Text = "Ort";
            // 
            // LblEditExternalContractor
            // 
            this.LblEditExternalContractor.Font = new System.Drawing.Font("Arial", 9F);
            this.LblEditExternalContractor.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LblEditExternalContractor.Location = new System.Drawing.Point(20, 18);
            this.LblEditExternalContractor.Name = "LblEditExternalContractor";
            this.LblEditExternalContractor.Size = new System.Drawing.Size(80, 23);
            this.LblEditExternalContractor.TabIndex = 17;
            this.LblEditExternalContractor.Text = "Fremdfirma";
            // 
            // BtnCancel
            // 
            this.BtnCancel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Location = new System.Drawing.Point(995, 553);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(104, 30);
            this.BtnCancel.TabIndex = 22;
            this.BtnCancel.Tag = "";
            this.BtnCancel.Text = "&Abbrechen";
            this.TooCancel.SetToolTip(this.BtnCancel, "Verwirft die bereits eingegebenen Daten");
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnBackTo
            // 
            this.BtnBackTo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBackTo.Location = new System.Drawing.Point(1115, 553);
            this.BtnBackTo.Name = "BtnBackTo";
            this.BtnBackTo.Size = new System.Drawing.Size(104, 30);
            this.BtnBackTo.TabIndex = 23;
            this.BtnBackTo.Tag = "";
            this.BtnBackTo.Text = "&Zurück";
            this.TooBackTo.SetToolTip(this.BtnBackTo, "Zurück zur Übersichtsmaske");
            this.BtnBackTo.Click += new System.EventHandler(this.BtnBackTo_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Location = new System.Drawing.Point(757, 553);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(104, 30);
            this.BtnSave.TabIndex = 20;
            this.BtnSave.Tag = "";
            this.BtnSave.Text = "Speiche&rn";
            this.TooSave.SetToolTip(this.BtnSave, "Speichert den Datensatz");
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnNew
            // 
            this.BtnNew.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnNew.Location = new System.Drawing.Point(635, 553);
            this.BtnNew.Name = "BtnNew";
            this.BtnNew.Size = new System.Drawing.Size(104, 30);
            this.BtnNew.TabIndex = 19;
            this.BtnNew.Tag = "";
            this.BtnNew.Text = "&Neu";
            this.TooNew.SetToolTip(this.BtnNew, "Legt einen neuen Datensatz an");
            this.BtnNew.Click += new System.EventHandler(this.BtnNew_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelete.Location = new System.Drawing.Point(876, 553);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(104, 30);
            this.BtnDelete.TabIndex = 21;
            this.BtnDelete.Tag = "";
            this.BtnDelete.Text = "&Löschen";
            this.TooDelete.SetToolTip(this.BtnDelete, "Löscht den markierten Datensatz");
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // FrmUCAdminExternalContractor
            // 
            this.Controls.Add(this.BtnDelete);
            this.Controls.Add(this.BtnNew);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnBackTo);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.LblSearch);
            this.Controls.Add(this.PnlSearch);
            this.Controls.Add(this.DgrExternalContractor);
            this.Controls.Add(this.LblEdit);
            this.Controls.Add(this.PnlEdit);
            this.Name = "FrmUCAdminExternalContractor";
            this.Size = new System.Drawing.Size(1258, 816);
            this.Leave += new System.EventHandler(this.FrmUCAdminExternalContractor_Leave);
            this.PnlSearch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DgrExternalContractor)).EndInit();
            this.PnlEdit.ResumeLayout(false);
            this.PnlEdit.PerformLayout();
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

		public DSExContractor PropDSExContractor
		{
			get 
			{
				return mDSExContractor;
			}
			set 
			{
				mDSExContractor = value;
			}
		} 

		// ID of currently selected AdminRecord in grid
		public int CurrentAdminRec
		{
			get 
			{
				return mCurrentAdminRec;
			}
			set 
			{
				mCurrentAdminRec = value;
			}
		} 

		public string CurrentEXCOName
		{
			get 
			{
				return mCurrentEXCOName;
			}
			set 
			{
				mCurrentEXCOName = value;
			}
		} 

		// Flag if changes have been made
		public bool ContentChanged
		{
			get 
			{
				return mContentChanged;
			}
			set 
			{
				mContentChanged = value;
			}
		} 

		#endregion //End of Accessors

		#region Methods
 
		public void CreateDataSet() 
		{
			mDSExContractor = new DSExContractor();
		}

		/// <summary>
		/// Fill comboboxes
		/// ExternalContractor
		/// Supervisor
		/// City
		/// </summary>
		internal void FillLists() 
		{
			FillExternalContractor();
			FillSupervisor();
			FillCity();
		}


        /// <summary>
        /// Returns typed instance of current controller
        /// </summary>
        /// <returns></returns>
		private AdministrationController GetMyController() 
		{
			return (AdministrationController)mController;
		}

		/// <summary>
		/// Fill combobox ExternalContractor
		/// </summary>
		private void FillExternalContractor()
		{
			ArrayList externalContractor = new ArrayList(); 
			externalContractor.Add(new LovItem("0",""));
			externalContractor.AddRange(LovSingleton.GetInstance().GetRootList(null, "FPASS_EXCONTRACTOR", "EXCO_NAME"));
			CboSearchExternalContractor.DataSource = externalContractor;
			CboSearchExternalContractor.DisplayMember = "ItemValue";
			CboSearchExternalContractor.ValueMember = "DecId";
		}

		/// <summary>
		/// Fill combobox Supervisor
		/// </summary>
		private void FillSupervisor()
		{
			ArrayList supervisor = new ArrayList(); 
			supervisor.Add(new LovItem("0",""));
			supervisor.AddRange(LovSingleton.GetInstance().GetRootList(null, "FPASS_SUPERVISOR", "SUPERVISOR"));
			CboSearchSupervisor.DataSource = supervisor;
			CboSearchSupervisor.DisplayMember = "ItemValue";
			CboSearchSupervisor.ValueMember = "DecId";
		}

		/// <summary>
		/// Fill combobox City
		/// No ID bound as only interested in the string
		/// </summary>
		private void FillCity()
		{
			ArrayList city = new ArrayList(); 
			city.Add(new LovItem("0",""));
			city.AddRange(LovSingleton.GetInstance().GetRootList(null, "FPASS_EXCONTRACTORCITY", "EXCO_CITY"));
			CboSearchCity.DataSource = city;
			CboSearchCity.DisplayMember = "ItemValue";
		}

		/// <summary>
		/// Get the PK id of the current excontractor record
		/// .. that is the data record currently selected in the datagrid
		/// and load record for editing
		/// </summary>
		private void TableNavigated()
		{
			int rowIndex = this.DgrExternalContractor.CurrentRowIndex;
			if(-1 < rowIndex)
			{
				mCurrentAdminRec = Convert.ToInt32(this.DgrExternalContractor[rowIndex, 0].ToString());
				mCurrentEXCOName = this.DgrExternalContractor[rowIndex, 1].ToString();
			}
			GetMyController().HandleEventDgrNavigateTabExCon();
		}

		private void TextFieldsChanged()
		{
			mContentChanged = true;
		}

		/// <summary>
		/// Call Search Excontractor form. Need to know which UserControl did calling
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnSearchExternalContractor_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventCallFormSearchExContractor(mSourceCallSearchExco);
			GetMyController().HandleEventOpenSearchExternalContractorDialog();
		}

        /// <summary>
        /// Raised when user clicks on button "Suchen"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void BtnSearch_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventBtnSearchTabExCon();
		}

		/// <summary>
		/// Clear textboxes in foot of form
		/// </summary>
		public void ClearFields()
		{
			ArrayList arrTxtFields = new ArrayList();
			arrTxtFields.Add(TxtEditExternalContractor);
            arrTxtFields.Add(TxtEditDebitNo);
			arrTxtFields.Add(TxtEditStreet);
			arrTxtFields.Add(TxtEditPostalCode);
			arrTxtFields.Add(TxtEditCity);
			arrTxtFields.Add(TxtEditCountry);
			arrTxtFields.Add(TxtEditSupervisorSurname);
			arrTxtFields.Add(TxtEditSupervisorFirstname);
			arrTxtFields.Add(TxtEditPhone);
			arrTxtFields.Add(TxtEditFax );
			arrTxtFields.Add(TxtEditMobil );
				
			foreach (System.Windows.Forms.TextBox tlr in arrTxtFields)
			{
				tlr.DataBindings.Clear();
				tlr.Text = "";
				tlr.Enabled = false;
			}
			mContentChanged  = false;
			mCurrentAdminRec = -1;

		}

		/// <summary>
		/// Not used 12.12.2003
		/// </summary>
		private void SetAuthorization() 
		{
		}

		public void RegisterController(AbstractController aAbstractController) 
		{
			this.mController = aAbstractController;
		}

		#endregion // End of Methods

		#region Events

		
		private void BtnBackTo_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventBackBtnExContractor();
		}

		/// <summary>
		/// Fired when this tab (UserControl) is left and user has executed search, edited etc on this tab
		/// Check not DesignMode necessary due to bugs in IDE Designer
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <exception cref="Degussa.FPASS.Util.Exceptions.ActionCancelledException">ActionCancelledException is thrown
		/// if user cancels prompt to save changes (should stop tab being left but currently doesn't)</exception>
		private void FrmUCAdminExternalContractor_Leave(object sender, System.EventArgs e)
		{
			if ( ! DesignMode ) 
			{
				try
				{
					GetMyController().HandleEventTabExContractorExited();
				}
				catch ( ActionCancelledException )
				{
					// Swallow
				}
			}
		}

		/// <summary>
		/// Fired when datagrid is entered, allows PK ID(s) of current record to be read
		/// Idea is, if there is only one record returned then CurrentCellChanged does not fire.
		/// Paint is not used here as it is fired too many times during load ( also during fill datagrid once dataset is filled)
		/// Cannot tell if contents of form have been changed by user
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DgrExternalContractor_Enter(object sender, System.EventArgs e)
		{
			if ( DgrExternalContractor.VisibleRowCount > 0 )
			{
				TableNavigated();
			}		
		}

		/// <summary>
		/// Fired each time a record is selected in  datagrid, allows PK ID(s) of current record to be read
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DgrExternalContractor_CurrentCellChanged(object sender, System.EventArgs e)
		{
			if ( !mGridIsSorted )
			{
				if ( DgrExternalContractor.VisibleRowCount > 1 )
				{
					TableNavigated();
				}
			}
		}

		/// <summary>
		/// This event fires when the column header is clicked, i.e. when the grid is sorted.
		/// Put pointer on first row (index 0)
		/// Discard currently selected record, user has to re-click to select 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DgrExternalContractor_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			mGridIsSorted = true;
			if ( DgrExternalContractor.VisibleRowCount > 1 )
			{
				DataGrid.HitTestInfo hti  = DgrExternalContractor.HitTest(e.X, e.Y);
				DataGrid.HitTestType type = hti.Type;
				switch(type)       
				{         
					case System.Windows.Forms.DataGrid.HitTestType.ColumnHeader:
						DgrExternalContractor.CurrentRowIndex = 0;
						ClearFields();
						break;				
				}
			}
			mGridIsSorted = false;
		}

		private void BtnSave_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventBtnSaveTabExContractor();
		}

		private void BtnCancel_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventBtnCancelClick();		
		}

		private void BtnNew_Click(object sender, System.EventArgs e)
		{	
			GetMyController().HandleEventBtnNewExContractorClick();
		}

		private void BtnDelete_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventBtnDeleteTabExternalCon();
		}

        /// <summary>
        /// Raised when user enters field. Signifies Data has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void TxtEditExternalContractor_Enter(object sender, System.EventArgs e)
		{
			TextFieldsChanged();
		}

        /// <summary>
        /// Raised when user enters field. Signifies Data has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtEditDebitNo_Enter(object sender, System.EventArgs e)
        {
            TextFieldsChanged();
        }

        /// <summary>
        /// Raised when user enters field. Signifies Data has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void TxtEditStreet_Enter(object sender, System.EventArgs e)
		{
			TextFieldsChanged();
		}

        /// <summary>
        /// Raised when user enters field. Signifies Data has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void TxtEditPostalCode_Enter(object sender, System.EventArgs e)
		{
			TextFieldsChanged();
		}

        /// <summary>
        /// Raised when user enters field. Signifies Data has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void TxtEditCity_Enter(object sender, System.EventArgs e)
		{
			TextFieldsChanged();
		}

        /// <summary>
        /// Raised when user enters field. Signifies Data has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void TxtEditCountry_Enter(object sender, System.EventArgs e)
		{
			TextFieldsChanged();
		}

        /// <summary>
        /// Raised when user enters field. Signifies Data has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void TxtEditSupervisorSurname_TextChanged(object sender, System.EventArgs e)
		{
			TextFieldsChanged();
		}

        /// <summary>
        /// Raised when user enters field. Signifies Data has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtEditSupervisorFirstname_TextChanged(object sender, System.EventArgs e)
		{
			TextFieldsChanged();
		}

        /// <summary>
        /// Raised when user enters field. Signifies Data has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void TxtEditSupervisorSurname_Enter(object sender, System.EventArgs e)
		{
			TextFieldsChanged();
		}

        /// <summary>
        /// Raised when user enters field. Signifies Data has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void TxtEditSupervisorFirstname_Enter(object sender, System.EventArgs e)
		{
			TextFieldsChanged();
		}

        /// <summary>
        /// Raised when user enters field. Signifies Data has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void TxtEditPhone_Enter(object sender, System.EventArgs e)
		{
			TextFieldsChanged();
		}

        /// <summary>
        /// Raised when user enters field. Signifies Data has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void TxtEditFax_Enter(object sender, System.EventArgs e)
		{
			TextFieldsChanged();
		}

        /// <summary>
        /// Raised when user enters field. Signifies Data has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void TxtEditMobil_Enter(object sender, System.EventArgs e)
		{
			TextFieldsChanged();
		}


		#endregion // End of Events	

		
	
	}
}

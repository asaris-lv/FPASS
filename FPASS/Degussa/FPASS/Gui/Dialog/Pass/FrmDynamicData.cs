using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;

using Degussa.FPASS.Util.UserManagement;
using Degussa.FPASS.Util.ListOfValues;
using Degussa.FPASS.Util.Validation;
using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.Db.DataSets;

using de.pta.Component.ListOfValues;
using de.pta.Component.Common;
using de.pta.Component.Errorhandling;

namespace Degussa.FPASS.Gui.Dialog.Pass
{
	/// <summary>
	/// FrmDynamicData is the view of the MVC-triad DynamicDataModel,
	/// DynamicDataController and FrmDynamicData.
	/// FrmDynamicData extends from the FPASSBaseView.
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

	public class FrmDynamicData : FPASSBaseView
	{

		#region Members

		//panels
		internal System.Windows.Forms.Panel PnlSearch;

		//labels
		internal System.Windows.Forms.Label LblDate;
		internal System.Windows.Forms.Label LblKind;
		internal System.Windows.Forms.Label LblFrom;
		internal System.Windows.Forms.Label LblUntil;
		internal System.Windows.Forms.Label LblSurname;
		internal System.Windows.Forms.Label LblFirstname;
		internal System.Windows.Forms.Label LblMask;
		internal System.Windows.Forms.Label LblExcontractor;

		//textboxes
		internal System.Windows.Forms.TextBox TxtSurname;
		internal System.Windows.Forms.TextBox TxtFirstname;

		//comboboxes
		internal System.Windows.Forms.ComboBox CboKind;
		internal System.Windows.Forms.ComboBox CboExcontractor;

		//buttons
		internal System.Windows.Forms.Button BtnClearMask;
		internal System.Windows.Forms.Button BtnSearch;
		internal System.Windows.Forms.Button BtnBackTo;

		//tooltips
		private System.Windows.Forms.ToolTip TooClearMask;
		private System.Windows.Forms.ToolTip TooSearch;
		private System.Windows.Forms.ToolTip TooBackTo;

		//tables
		internal System.Windows.Forms.DataGrid DgrDynamicData;
		internal System.Windows.Forms.DataGridTextBoxColumn DataGridTextBoxColumnSurname;
		internal System.Windows.Forms.DataGridTextBoxColumn DataGridTextBoxColumnFirstname;
		internal System.Windows.Forms.DataGridTextBoxColumn DataGridTextBoxColumnDate;
		internal System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumnEntry;
		internal System.Windows.Forms.DataGridTableStyle DgrTableStyleDynamicData;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumnExcontractor;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumnID;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumnPersNO;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumnTime;
		internal System.Windows.Forms.DateTimePicker DatBookingFrom;
		internal System.Windows.Forms.DateTimePicker DatBookingUntil;

		//other
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Used to flag if val in DateTimePickers has been selected
		/// </summary>
		private bool			mSearchDateInUse = false;
		

		#endregion // End of Members

		#region Constructors

		public FrmDynamicData()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			MnuFunction.Enabled = false;
            MnuZKS.Enabled = false;
			MnuReports.Enabled = false;
		
			FillExcontractor();
			SetAuthorization();
		}

		#endregion // End of Construction

		#region Designer generated code

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


		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.BtnBackTo = new System.Windows.Forms.Button();
            this.PnlSearch = new System.Windows.Forms.Panel();
            this.DatBookingUntil = new System.Windows.Forms.DateTimePicker();
            this.DatBookingFrom = new System.Windows.Forms.DateTimePicker();
            this.CboExcontractor = new System.Windows.Forms.ComboBox();
            this.LblExcontractor = new System.Windows.Forms.Label();
            this.BtnClearMask = new System.Windows.Forms.Button();
            this.LblUntil = new System.Windows.Forms.Label();
            this.LblFrom = new System.Windows.Forms.Label();
            this.CboKind = new System.Windows.Forms.ComboBox();
            this.LblKind = new System.Windows.Forms.Label();
            this.LblDate = new System.Windows.Forms.Label();
            this.BtnSearch = new System.Windows.Forms.Button();
            this.LblSurname = new System.Windows.Forms.Label();
            this.LblFirstname = new System.Windows.Forms.Label();
            this.TxtSurname = new System.Windows.Forms.TextBox();
            this.TxtFirstname = new System.Windows.Forms.TextBox();
            this.DgrDynamicData = new System.Windows.Forms.DataGrid();
            this.DgrTableStyleDynamicData = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumnID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumnPersNO = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DataGridTextBoxColumnDate = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumnTime = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumnEntry = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DataGridTextBoxColumnSurname = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DataGridTextBoxColumnFirstname = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumnExcontractor = new System.Windows.Forms.DataGridTextBoxColumn();
            this.LblMask = new System.Windows.Forms.Label();
            this.TooClearMask = new System.Windows.Forms.ToolTip(this.components);
            this.TooSearch = new System.Windows.Forms.ToolTip(this.components);
            this.TooBackTo = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.SbpMessage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sbpenvironment)).BeginInit();
            this.PnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgrDynamicData)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnBackTo
            // 
            this.BtnBackTo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBackTo.Location = new System.Drawing.Point(1114, 852);
            this.BtnBackTo.Name = "BtnBackTo";
            this.BtnBackTo.Size = new System.Drawing.Size(140, 32);
            this.BtnBackTo.TabIndex = 10;
            this.BtnBackTo.Tag = "";
            this.BtnBackTo.Text = "&Zurück";
            this.TooBackTo.SetToolTip(this.BtnBackTo, "Zurück zur Übersichtsmaske");
            this.BtnBackTo.Click += new System.EventHandler(this.BtnBackTo_Click);
            // 
            // PnlSearch
            // 
            this.PnlSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlSearch.Controls.Add(this.DatBookingUntil);
            this.PnlSearch.Controls.Add(this.DatBookingFrom);
            this.PnlSearch.Controls.Add(this.CboExcontractor);
            this.PnlSearch.Controls.Add(this.LblExcontractor);
            this.PnlSearch.Controls.Add(this.BtnClearMask);
            this.PnlSearch.Controls.Add(this.LblUntil);
            this.PnlSearch.Controls.Add(this.LblFrom);
            this.PnlSearch.Controls.Add(this.CboKind);
            this.PnlSearch.Controls.Add(this.LblKind);
            this.PnlSearch.Controls.Add(this.LblDate);
            this.PnlSearch.Controls.Add(this.BtnSearch);
            this.PnlSearch.Controls.Add(this.LblSurname);
            this.PnlSearch.Controls.Add(this.LblFirstname);
            this.PnlSearch.Controls.Add(this.TxtSurname);
            this.PnlSearch.Controls.Add(this.TxtFirstname);
            this.PnlSearch.Location = new System.Drawing.Point(7, 57);
            this.PnlSearch.Name = "PnlSearch";
            this.PnlSearch.Size = new System.Drawing.Size(1255, 134);
            this.PnlSearch.TabIndex = 0;
            // 
            // DatBookingUntil
            // 
            this.DatBookingUntil.CustomFormat = "dd.MM.yyyy";
            this.DatBookingUntil.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DatBookingUntil.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatBookingUntil.Location = new System.Drawing.Point(576, 54);
            this.DatBookingUntil.Name = "DatBookingUntil";
            this.DatBookingUntil.Size = new System.Drawing.Size(88, 21);
            this.DatBookingUntil.TabIndex = 34;
            this.DatBookingUntil.Leave += new System.EventHandler(this.DatBookingUntil_Leave);
            // 
            // DatBookingFrom
            // 
            this.DatBookingFrom.CustomFormat = "dd.MM.yyyy";
            this.DatBookingFrom.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DatBookingFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatBookingFrom.Location = new System.Drawing.Point(576, 22);
            this.DatBookingFrom.Name = "DatBookingFrom";
            this.DatBookingFrom.Size = new System.Drawing.Size(88, 21);
            this.DatBookingFrom.TabIndex = 33;
            this.DatBookingFrom.Leave += new System.EventHandler(this.DatBookingFrom_Leave);
            // 
            // CboExcontractor
            // 
            this.CboExcontractor.AccessibleRole = System.Windows.Forms.AccessibleRole.List;
            this.CboExcontractor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboExcontractor.ItemHeight = 15;
            this.CboExcontractor.Items.AddRange(new object[] {
            "",
            "Eingang",
            "Ausgang"});
            this.CboExcontractor.Location = new System.Drawing.Point(114, 86);
            this.CboExcontractor.Name = "CboExcontractor";
            this.CboExcontractor.Size = new System.Drawing.Size(210, 23);
            this.CboExcontractor.TabIndex = 3;
            // 
            // LblExcontractor
            // 
            this.LblExcontractor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblExcontractor.Location = new System.Drawing.Point(34, 88);
            this.LblExcontractor.Name = "LblExcontractor";
            this.LblExcontractor.Size = new System.Drawing.Size(72, 16);
            this.LblExcontractor.TabIndex = 32;
            this.LblExcontractor.Text = "Fremdfirma";
            // 
            // BtnClearMask
            // 
            this.BtnClearMask.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClearMask.Location = new System.Drawing.Point(1087, 64);
            this.BtnClearMask.Name = "BtnClearMask";
            this.BtnClearMask.Size = new System.Drawing.Size(145, 30);
            this.BtnClearMask.TabIndex = 8;
            this.BtnClearMask.Text = "&Maske leeren";
            this.TooClearMask.SetToolTip(this.BtnClearMask, "Löscht alle bereits eingegebenen Suchkriterien");
            this.BtnClearMask.Click += new System.EventHandler(this.BtnClearMask_Click);
            // 
            // LblUntil
            // 
            this.LblUntil.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblUntil.Location = new System.Drawing.Point(544, 56);
            this.LblUntil.Name = "LblUntil";
            this.LblUntil.Size = new System.Drawing.Size(32, 16);
            this.LblUntil.TabIndex = 27;
            this.LblUntil.Text = "bis";
            // 
            // LblFrom
            // 
            this.LblFrom.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblFrom.Location = new System.Drawing.Point(544, 24);
            this.LblFrom.Name = "LblFrom";
            this.LblFrom.Size = new System.Drawing.Size(32, 16);
            this.LblFrom.TabIndex = 26;
            this.LblFrom.Text = "von";
            // 
            // CboKind
            // 
            this.CboKind.AccessibleRole = System.Windows.Forms.AccessibleRole.List;
            this.CboKind.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboKind.ItemHeight = 15;
            this.CboKind.Items.AddRange(new object[] {
            "",
            "Eingang",
            "Ausgang"});
            this.CboKind.Location = new System.Drawing.Point(819, 22);
            this.CboKind.Name = "CboKind";
            this.CboKind.Size = new System.Drawing.Size(165, 23);
            this.CboKind.TabIndex = 6;
            // 
            // LblKind
            // 
            this.LblKind.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblKind.Location = new System.Drawing.Point(708, 24);
            this.LblKind.Name = "LblKind";
            this.LblKind.Size = new System.Drawing.Size(120, 16);
            this.LblKind.TabIndex = 24;
            this.LblKind.Text = "Art der Buchung";
            // 
            // LblDate
            // 
            this.LblDate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblDate.Location = new System.Drawing.Point(416, 24);
            this.LblDate.Name = "LblDate";
            this.LblDate.Size = new System.Drawing.Size(120, 16);
            this.LblDate.TabIndex = 21;
            this.LblDate.Text = "Datum der Buchung";
            // 
            // BtnSearch
            // 
            this.BtnSearch.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSearch.Location = new System.Drawing.Point(1087, 24);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(145, 30);
            this.BtnSearch.TabIndex = 7;
            this.BtnSearch.Text = "&Suchen";
            this.TooSearch.SetToolTip(this.BtnSearch, "Löst den Suchvorgang aus");
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // LblSurname
            // 
            this.LblSurname.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSurname.Location = new System.Drawing.Point(34, 24);
            this.LblSurname.Name = "LblSurname";
            this.LblSurname.Size = new System.Drawing.Size(72, 16);
            this.LblSurname.TabIndex = 10;
            this.LblSurname.Text = "Nachname";
            // 
            // LblFirstname
            // 
            this.LblFirstname.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblFirstname.Location = new System.Drawing.Point(34, 56);
            this.LblFirstname.Name = "LblFirstname";
            this.LblFirstname.Size = new System.Drawing.Size(72, 16);
            this.LblFirstname.TabIndex = 11;
            this.LblFirstname.Text = "Vorname";
            // 
            // TxtSurname
            // 
            this.TxtSurname.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSurname.Location = new System.Drawing.Point(114, 22);
            this.TxtSurname.Name = "TxtSurname";
            this.TxtSurname.Size = new System.Drawing.Size(210, 21);
            this.TxtSurname.TabIndex = 1;
            // 
            // TxtFirstname
            // 
            this.TxtFirstname.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtFirstname.Location = new System.Drawing.Point(114, 54);
            this.TxtFirstname.Name = "TxtFirstname";
            this.TxtFirstname.Size = new System.Drawing.Size(210, 21);
            this.TxtFirstname.TabIndex = 2;
            // 
            // DgrDynamicData
            // 
            this.DgrDynamicData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DgrDynamicData.CaptionBackColor = System.Drawing.Color.SteelBlue;
            this.DgrDynamicData.CaptionFont = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DgrDynamicData.CaptionText = "Fremdfirmenmitarbeiter-Bewegungsdaten";
            this.DgrDynamicData.DataMember = "";
            this.DgrDynamicData.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.DgrDynamicData.Location = new System.Drawing.Point(7, 206);
            this.DgrDynamicData.Name = "DgrDynamicData";
            this.DgrDynamicData.ReadOnly = true;
            this.DgrDynamicData.Size = new System.Drawing.Size(1247, 638);
            this.DgrDynamicData.TabIndex = 9;
            this.DgrDynamicData.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.DgrTableStyleDynamicData});
            // 
            // DgrTableStyleDynamicData
            // 
            this.DgrTableStyleDynamicData.DataGrid = this.DgrDynamicData;
            this.DgrTableStyleDynamicData.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.dataGridTextBoxColumnID,
            this.dataGridTextBoxColumnPersNO,
            this.DataGridTextBoxColumnDate,
            this.dataGridTextBoxColumnTime,
            this.dataGridTextBoxColumnEntry,
            this.DataGridTextBoxColumnSurname,
            this.DataGridTextBoxColumnFirstname,
            this.dataGridTextBoxColumnExcontractor});
            this.DgrTableStyleDynamicData.HeaderFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DgrTableStyleDynamicData.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.DgrTableStyleDynamicData.MappingName = "RTTabDynData";
            this.DgrTableStyleDynamicData.PreferredColumnWidth = 101;
            this.DgrTableStyleDynamicData.ReadOnly = true;
            // 
            // dataGridTextBoxColumnID
            // 
            this.dataGridTextBoxColumnID.Format = "";
            this.dataGridTextBoxColumnID.FormatInfo = null;
            this.dataGridTextBoxColumnID.HeaderText = "DYFP_ID";
            this.dataGridTextBoxColumnID.MappingName = "DynamicId";
            this.dataGridTextBoxColumnID.Width = 0;
            // 
            // dataGridTextBoxColumnPersNO
            // 
            this.dataGridTextBoxColumnPersNO.Format = "";
            this.dataGridTextBoxColumnPersNO.FormatInfo = null;
            this.dataGridTextBoxColumnPersNO.HeaderText = "Personalr";
            this.dataGridTextBoxColumnPersNO.MappingName = "Persno";
            this.dataGridTextBoxColumnPersNO.NullText = "";
            this.dataGridTextBoxColumnPersNO.Width = 110;
            // 
            // DataGridTextBoxColumnDate
            // 
            this.DataGridTextBoxColumnDate.Format = "";
            this.DataGridTextBoxColumnDate.FormatInfo = null;
            this.DataGridTextBoxColumnDate.HeaderText = "Buchungsdatum";
            this.DataGridTextBoxColumnDate.MappingName = "BookDate";
            this.DataGridTextBoxColumnDate.Width = 120;
            // 
            // dataGridTextBoxColumnTime
            // 
            this.dataGridTextBoxColumnTime.Format = "";
            this.dataGridTextBoxColumnTime.FormatInfo = null;
            this.dataGridTextBoxColumnTime.HeaderText = "Uhrzeit Buchung";
            this.dataGridTextBoxColumnTime.MappingName = "BookTime";
            this.dataGridTextBoxColumnTime.Width = 120;
            // 
            // dataGridTextBoxColumnEntry
            // 
            this.dataGridTextBoxColumnEntry.Format = "";
            this.dataGridTextBoxColumnEntry.FormatInfo = null;
            this.dataGridTextBoxColumnEntry.HeaderText = "Buchungsart";
            this.dataGridTextBoxColumnEntry.MappingName = "Entry";
            this.dataGridTextBoxColumnEntry.Width = 85;
            // 
            // DataGridTextBoxColumnSurname
            // 
            this.DataGridTextBoxColumnSurname.Format = "";
            this.DataGridTextBoxColumnSurname.FormatInfo = null;
            this.DataGridTextBoxColumnSurname.HeaderText = "Nachname";
            this.DataGridTextBoxColumnSurname.MappingName = "Surname";
            this.DataGridTextBoxColumnSurname.Width = 220;
            // 
            // DataGridTextBoxColumnFirstname
            // 
            this.DataGridTextBoxColumnFirstname.Format = "";
            this.DataGridTextBoxColumnFirstname.FormatInfo = null;
            this.DataGridTextBoxColumnFirstname.HeaderText = "Vorname";
            this.DataGridTextBoxColumnFirstname.MappingName = "Firstname";
            this.DataGridTextBoxColumnFirstname.Width = 220;
            // 
            // dataGridTextBoxColumnExcontractor
            // 
            this.dataGridTextBoxColumnExcontractor.Format = "";
            this.dataGridTextBoxColumnExcontractor.FormatInfo = null;
            this.dataGridTextBoxColumnExcontractor.HeaderText = "Fremdfirma";
            this.dataGridTextBoxColumnExcontractor.MappingName = "Excontractor";
            this.dataGridTextBoxColumnExcontractor.Width = 250;
            // 
            // LblMask
            // 
            this.LblMask.BackColor = System.Drawing.Color.White;
            this.LblMask.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMask.Location = new System.Drawing.Point(424, 8);
            this.LblMask.Name = "LblMask";
            this.LblMask.Size = new System.Drawing.Size(360, 32);
            this.LblMask.TabIndex = 76;
            this.LblMask.Text = "FPASS - Bewegungsdaten";
            // 
            // FrmDynamicData
            // 
            this.AcceptButton = this.BtnSearch;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(1264, 921);
            this.Controls.Add(this.LblMask);
            this.Controls.Add(this.BtnBackTo);
            this.Controls.Add(this.PnlSearch);
            this.Controls.Add(this.DgrDynamicData);
            this.Name = "FrmDynamicData";
            this.Text = "FPASS - Bewegungsdaten";
            this.Controls.SetChildIndex(this.LblBaseHead, 0);
            this.Controls.SetChildIndex(this.StbBase, 0);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            this.Controls.SetChildIndex(this.DgrDynamicData, 0);
            this.Controls.SetChildIndex(this.PnlSearch, 0);
            this.Controls.SetChildIndex(this.BtnBackTo, 0);
            this.Controls.SetChildIndex(this.LblMask, 0);
            ((System.ComponentModel.ISupportInitialize)(this.SbpMessage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sbpenvironment)).EndInit();
            this.PnlSearch.ResumeLayout(false);
            this.PnlSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgrDynamicData)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		#region Accessors 

		/// <summary>
		/// Getter: was a val chosen in DateTimePickers?
		/// </summary>
		internal bool IsSearchDateInUse
		{
			get 
			{
				return mSearchDateInUse;
			}
		}

		#endregion
	
		#region Methods

		internal override void PreShow()
		{
			ClearFields();
			SbpMessage.Text = String.Empty;
		}


		internal override void PreClose()
		{
			ClearFields();
			SbpMessage.Text = String.Empty;
		}

		/// <summary>
		/// fills combobox excontractor
		/// </summary>
		internal void FillExcontractor()
		{
			ArrayList excontractor = new ArrayList();
			excontractor.Add(new LovItem("0", ""));
			excontractor.AddRange( LovSingleton.GetInstance().GetRootList(null, "FPASS_EXCONTRACTOR", "EXCO_NAME") );			
			this.CboExcontractor.DataSource = excontractor;
			this.CboExcontractor.DisplayMember = "ItemValue";
			this.CboExcontractor.ValueMember = "Id";
		}
		
		/// <summary>
		/// Resets search fields & datagrid in form.
		/// Value in DateTimePickers set to current date
		/// </summary>
		private void ClearFields()
		{
			mSearchDateInUse = false;
			this.CboExcontractor.SelectedValue = 0;
			this.CboExcontractor.Text  = String.Empty;
			this.CboKind.Text		   = String.Empty;
			TimePicker();
			this.TxtFirstname.Text	   = String.Empty;
			this.TxtFirstname.Text	   = String.Empty;
			this.TxtSurname.Text	   = String.Empty;
			this.DgrDynamicData.DataSource = null;
		}

		/// <summary>
		/// calculate the first and the last day of the current months
		/// and fill the dynamic data fields with this values
		/// </summary>
		public void TimePicker()
		{
			DateTime currentdate = DateTime.Now;
			int daysOfMonth = new GregorianCalendar().GetDaysInMonth(currentdate.Year, currentdate.Month);
			string lastOfMonth = daysOfMonth.ToString() + "." + Convert.ToDateTime(currentdate).ToString("MM.yyyy");
			string firstOfMonth = "01." + Convert.ToDateTime(currentdate).ToString("MM.yyyy");
			DatBookingFrom.Value = Convert.ToDateTime(firstOfMonth);
			DatBookingUntil.Value = Convert.ToDateTime(lastOfMonth);
		}

		/// <summary>
		/// Empty 09.03.2004
		/// </summary>
		private void SetAuthorization() 
		{

		}

		/// <summary>
		/// Get controller associated with this form (current MVC)
		/// </summary>
		/// <returns></returns>
		private DynamicDataController GetMyController() 
		{
			return ((DynamicDataController)mController);
		}

		/// <summary>
		/// 09.03.2004: Make sure DateFrom is before DateUntil when leaving datetime fields
		/// </summary>
		private void CheckFromAndUntilDates()
		{
			try 
			{
				if ( this.DatBookingFrom.Value.Date > this.DatBookingUntil.Value.Date )
				{
					throw new UIWarningException(MessageSingleton.GetInstance().
						GetMessage(MessageSingleton.DYNDATA_FROMTILLDATE) );					
				}					
			} 
			catch ( UIWarningException uwe ) 
			{
				this.DatBookingUntil.Value = this.DatBookingFrom.Value;
				ExceptionProcessor.GetInstance().Process(uwe);
			}	
		}
		

		#endregion

		#region Events

		/// <summary>
		/// Reaction to "Maske leeren": use local method
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnClearMask_Click(object sender, System.EventArgs e)
		{
			ClearFields();
		}

		private void BtnSearch_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventBtnSearchDynamicData();		
		}

		private void BtnBackTo_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleCloseDialog();
		}

		private void BtnSearchExternalContractor_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventOpenSearchExternalContractorDialog();
		}

		/// <summary>
		/// 09.03.2004: Make sure DateFrom is before DateUntil
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DatBookingFrom_Leave(object sender, System.EventArgs e)
		{			
			mSearchDateInUse = true;
			CheckFromAndUntilDates();	
		}

		/// <summary>
		///  09.03.2004: Make sure DateUntil is after DateFrom
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DatBookingUntil_Leave(object sender, System.EventArgs e)
		{
			mSearchDateInUse = true;
			CheckFromAndUntilDates();	
		}

		
		#endregion

	}
}


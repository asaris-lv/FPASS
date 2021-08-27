using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using de.pta.Component.Errorhandling;

using Degussa.FPASS.Gui.Dialog;
using Degussa.FPASS.Util.UserManagement;
using Degussa.FPASS.Util.Validation;
using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.Db;

namespace Degussa.FPASS.Gui.Dialog.Pass
{
	/// <summary>
	/// FrmHistory is the view of the MVC-triad HistoryModel,
	/// HistoryController and FrmHistory.
	/// FrmHistory extends from the FPASSBaseView.
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
	public class FrmHistory : FPASSBaseView
	{

		#region Members

		//panels
		internal System.Windows.Forms.Panel PnlSearch;

		//labels
		internal System.Windows.Forms.Label LblMask;
		internal System.Windows.Forms.Label LblTable;
		internal System.Windows.Forms.Label LblFrom;
		internal System.Windows.Forms.Label LblUntil;

		//textboxes
		internal System.Windows.Forms.TextBox TxtUntil;
		internal System.Windows.Forms.TextBox TxtFrom;

		//comboboxes
		internal System.Windows.Forms.ComboBox CboTable;

		//buttons
		internal System.Windows.Forms.Button BtnBackTo;
		internal System.Windows.Forms.Button BtnClearMask;
		internal System.Windows.Forms.Button BtnSearch;

		//tooltips
		private System.Windows.Forms.ToolTip TooSearch;
		private System.Windows.Forms.ToolTip TooClearMask;
		private System.Windows.Forms.ToolTip TooBackTo;

		//tables
		public System.Windows.Forms.DataGrid DgrHistory;
		private System.Windows.Forms.DataGridTableStyle DgrTableStyleHistory;
		private System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxTable;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxFieldname;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxRowId;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxOldValue;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxNewValue;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxTimestamp;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxDescription;
		internal System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxUserName;

		//other
		private DateTime now = System.DateTime.Now;

		private System.ComponentModel.IContainer components;

		/// <summary>
		/// bool, give true if the date formate is correct 
		/// </summary>
		private bool mCorrectFormat = true;

		#endregion //End of Members

		#region Constructors

		public FrmHistory()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			InitView();

			SetAuthorization();
		}

		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.BtnSearch = new System.Windows.Forms.Button();
            this.DgrHistory = new System.Windows.Forms.DataGrid();
            this.DgrTableStyleHistory = new System.Windows.Forms.DataGridTableStyle();
            this.DgrTextBoxTable = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxFieldname = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxRowId = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxOldValue = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxNewValue = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxUserName = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxTimestamp = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxDescription = new System.Windows.Forms.DataGridTextBoxColumn();
            this.BtnClearMask = new System.Windows.Forms.Button();
            this.CboTable = new System.Windows.Forms.ComboBox();
            this.LblTable = new System.Windows.Forms.Label();
            this.LblFrom = new System.Windows.Forms.Label();
            this.PnlSearch = new System.Windows.Forms.Panel();
            this.TxtUntil = new System.Windows.Forms.TextBox();
            this.TxtFrom = new System.Windows.Forms.TextBox();
            this.LblUntil = new System.Windows.Forms.Label();
            this.BtnBackTo = new System.Windows.Forms.Button();
            this.LblMask = new System.Windows.Forms.Label();
            this.TooSearch = new System.Windows.Forms.ToolTip(this.components);
            this.TooClearMask = new System.Windows.Forms.ToolTip(this.components);
            this.TooBackTo = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.SbpMessage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sbpenvironment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgrHistory)).BeginInit();
            this.PnlSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // LblBaseHead
            // 
            this.LblBaseHead.Size = new System.Drawing.Size(1264, 40);
            // 
            // BtnSearch
            // 
            this.BtnSearch.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSearch.Location = new System.Drawing.Point(1090, 17);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(145, 30);
            this.BtnSearch.TabIndex = 4;
            this.BtnSearch.Text = "&Suchen";
            this.TooSearch.SetToolTip(this.BtnSearch, "Löst den Suchvorgang aus");
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // DgrHistory
            // 
            this.DgrHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DgrHistory.CaptionBackColor = System.Drawing.Color.SteelBlue;
            this.DgrHistory.CaptionFont = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DgrHistory.CaptionText = "Änderungshistorie";
            this.DgrHistory.DataMember = "";
            this.DgrHistory.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.DgrHistory.Location = new System.Drawing.Point(7, 176);
            this.DgrHistory.Name = "DgrHistory";
            this.DgrHistory.ReadOnly = true;
            this.DgrHistory.Size = new System.Drawing.Size(1245, 657);
            this.DgrHistory.TabIndex = 6;
            this.DgrHistory.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.DgrTableStyleHistory});
            // 
            // DgrTableStyleHistory
            // 
            this.DgrTableStyleHistory.DataGrid = this.DgrHistory;
            this.DgrTableStyleHistory.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.DgrTextBoxTable,
            this.dataGridTextBoxFieldname,
            this.dataGridTextBoxRowId,
            this.dataGridTextBoxOldValue,
            this.dataGridTextBoxNewValue,
            this.dataGridTextBoxUserName,
            this.dataGridTextBoxTimestamp,
            this.dataGridTextBoxDescription});
            this.DgrTableStyleHistory.HeaderFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DgrTableStyleHistory.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.DgrTableStyleHistory.MappingName = "RTTabHistory";
            this.DgrTableStyleHistory.PreferredColumnWidth = 170;
            this.DgrTableStyleHistory.ReadOnly = true;
            // 
            // DgrTextBoxTable
            // 
            this.DgrTextBoxTable.Format = "";
            this.DgrTextBoxTable.FormatInfo = null;
            this.DgrTextBoxTable.HeaderText = "Tabelle";
            this.DgrTextBoxTable.MappingName = "TableName";
            this.DgrTextBoxTable.ReadOnly = true;
            this.DgrTextBoxTable.Width = 130;
            // 
            // dataGridTextBoxFieldname
            // 
            this.dataGridTextBoxFieldname.Format = "";
            this.dataGridTextBoxFieldname.FormatInfo = null;
            this.dataGridTextBoxFieldname.HeaderText = "Spaltenname";
            this.dataGridTextBoxFieldname.MappingName = "ColumnName";
            this.dataGridTextBoxFieldname.ReadOnly = true;
            this.dataGridTextBoxFieldname.Width = 130;
            // 
            // dataGridTextBoxRowId
            // 
            this.dataGridTextBoxRowId.Format = "";
            this.dataGridTextBoxRowId.FormatInfo = null;
            this.dataGridTextBoxRowId.HeaderText = "ZeilenID";
            this.dataGridTextBoxRowId.MappingName = "RowId";
            this.dataGridTextBoxRowId.ReadOnly = true;
            this.dataGridTextBoxRowId.Width = 80;
            // 
            // dataGridTextBoxOldValue
            // 
            this.dataGridTextBoxOldValue.Format = "";
            this.dataGridTextBoxOldValue.FormatInfo = null;
            this.dataGridTextBoxOldValue.HeaderText = "Alter Wert";
            this.dataGridTextBoxOldValue.MappingName = "OldValue";
            this.dataGridTextBoxOldValue.NullText = "";
            this.dataGridTextBoxOldValue.ReadOnly = true;
            this.dataGridTextBoxOldValue.Width = 130;
            // 
            // dataGridTextBoxNewValue
            // 
            this.dataGridTextBoxNewValue.Format = "";
            this.dataGridTextBoxNewValue.FormatInfo = null;
            this.dataGridTextBoxNewValue.HeaderText = "Neuer Wert";
            this.dataGridTextBoxNewValue.MappingName = "NewValue";
            this.dataGridTextBoxNewValue.NullText = "";
            this.dataGridTextBoxNewValue.ReadOnly = true;
            this.dataGridTextBoxNewValue.Width = 130;
            // 
            // dataGridTextBoxUserName
            // 
            this.dataGridTextBoxUserName.Format = "";
            this.dataGridTextBoxUserName.FormatInfo = null;
            this.dataGridTextBoxUserName.HeaderText = "User Name ";
            this.dataGridTextBoxUserName.MappingName = "UserName";
            this.dataGridTextBoxUserName.NullText = "";
            this.dataGridTextBoxUserName.Width = 110;
            // 
            // dataGridTextBoxTimestamp
            // 
            this.dataGridTextBoxTimestamp.Format = "";
            this.dataGridTextBoxTimestamp.FormatInfo = null;
            this.dataGridTextBoxTimestamp.HeaderText = "Änderungsdatum";
            this.dataGridTextBoxTimestamp.MappingName = "ChangeDate";
            this.dataGridTextBoxTimestamp.ReadOnly = true;
            this.dataGridTextBoxTimestamp.Width = 110;
            // 
            // dataGridTextBoxDescription
            // 
            this.dataGridTextBoxDescription.Format = "";
            this.dataGridTextBoxDescription.FormatInfo = null;
            this.dataGridTextBoxDescription.HeaderText = "Beschreibung";
            this.dataGridTextBoxDescription.MappingName = "Description";
            this.dataGridTextBoxDescription.NullText = "";
            this.dataGridTextBoxDescription.Width = 130;
            // 
            // BtnClearMask
            // 
            this.BtnClearMask.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClearMask.Location = new System.Drawing.Point(1090, 57);
            this.BtnClearMask.Name = "BtnClearMask";
            this.BtnClearMask.Size = new System.Drawing.Size(145, 30);
            this.BtnClearMask.TabIndex = 5;
            this.BtnClearMask.Text = "&Maske leeren";
            this.TooClearMask.SetToolTip(this.BtnClearMask, "Löscht alle bereits eingegebenen Suchkriterien");
            this.BtnClearMask.Click += new System.EventHandler(this.BtnClearMask_Click);
            // 
            // CboTable
            // 
            this.CboTable.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboTable.ItemHeight = 15;
            this.CboTable.Items.AddRange(new object[] {
            "",
            "FPASS_BRIEFING",
            "FPASS_COWORKER",
            "FPASS_CRAFT",
            "FPASS_CWRBRIEFING",
            "FPASS_CWRPLANT",
            "FPASS_DEPARTMENT",
            "FPASS_DYNFPASS",
            "FPASS_EXCONTRACTOR",
            "FPASS_MANDATOR",
            "FPASS_PARAMETERFPASS",
            "FPASS_PARAMETERZKS",
            "FPASS_PLANT",
            "FPASS_PRECAUTIONMED",
            "FPASS_PRECMEDTYPE",
            "FPASS_RECAUTHORIZETYPE",
            "FPASS_RECEPTIONAUTHORIZE",
            "FPASS_RESPMASK",
            "FPASS_RESPMASKTYPE",
            "FPASS_ROLE",
            "FPASS_USER",
            "FPASS_USERDOMAINE",
            "FPASS_USERMAN",
            "FPASS_USERPARAMETER",
            "FPASS_VEHREGNO",
            "UM_ROLE",
            "UM_USER"});
            this.CboTable.Location = new System.Drawing.Point(83, 38);
            this.CboTable.Name = "CboTable";
            this.CboTable.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.CboTable.Size = new System.Drawing.Size(304, 23);
            this.CboTable.Sorted = true;
            this.CboTable.TabIndex = 1;
            // 
            // LblTable
            // 
            this.LblTable.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTable.Location = new System.Drawing.Point(23, 40);
            this.LblTable.Name = "LblTable";
            this.LblTable.Size = new System.Drawing.Size(72, 16);
            this.LblTable.TabIndex = 7;
            this.LblTable.Text = "Tabelle";
            // 
            // LblFrom
            // 
            this.LblFrom.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblFrom.Location = new System.Drawing.Point(464, 40);
            this.LblFrom.Name = "LblFrom";
            this.LblFrom.Size = new System.Drawing.Size(80, 18);
            this.LblFrom.TabIndex = 8;
            this.LblFrom.Text = "Zeitraum von";
            // 
            // PnlSearch
            // 
            this.PnlSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlSearch.Controls.Add(this.TxtUntil);
            this.PnlSearch.Controls.Add(this.TxtFrom);
            this.PnlSearch.Controls.Add(this.LblUntil);
            this.PnlSearch.Controls.Add(this.CboTable);
            this.PnlSearch.Controls.Add(this.LblTable);
            this.PnlSearch.Controls.Add(this.LblFrom);
            this.PnlSearch.Controls.Add(this.BtnSearch);
            this.PnlSearch.Controls.Add(this.BtnClearMask);
            this.PnlSearch.Location = new System.Drawing.Point(7, 56);
            this.PnlSearch.Name = "PnlSearch";
            this.PnlSearch.Size = new System.Drawing.Size(1253, 104);
            this.PnlSearch.TabIndex = 0;
            // 
            // TxtUntil
            // 
            this.TxtUntil.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtUntil.Location = new System.Drawing.Point(680, 38);
            this.TxtUntil.Name = "TxtUntil";
            this.TxtUntil.Size = new System.Drawing.Size(80, 21);
            this.TxtUntil.TabIndex = 3;
            this.TxtUntil.Leave += new System.EventHandler(this.TxtUntil_Leave);
            // 
            // TxtFrom
            // 
            this.TxtFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtFrom.Location = new System.Drawing.Point(552, 38);
            this.TxtFrom.Name = "TxtFrom";
            this.TxtFrom.Size = new System.Drawing.Size(80, 21);
            this.TxtFrom.TabIndex = 2;
            this.TxtFrom.Leave += new System.EventHandler(this.TxtFrom_Leave);
            // 
            // LblUntil
            // 
            this.LblUntil.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblUntil.Location = new System.Drawing.Point(648, 40);
            this.LblUntil.Name = "LblUntil";
            this.LblUntil.Size = new System.Drawing.Size(32, 18);
            this.LblUntil.TabIndex = 31;
            this.LblUntil.Text = "bis";
            // 
            // BtnBackTo
            // 
            this.BtnBackTo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBackTo.Location = new System.Drawing.Point(1115, 862);
            this.BtnBackTo.Name = "BtnBackTo";
            this.BtnBackTo.Size = new System.Drawing.Size(145, 30);
            this.BtnBackTo.TabIndex = 7;
            this.BtnBackTo.Text = "&Zurück";
            this.TooBackTo.SetToolTip(this.BtnBackTo, "Zurück zur Übersichtsmaske");
            this.BtnBackTo.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // LblMask
            // 
            this.LblMask.BackColor = System.Drawing.Color.White;
            this.LblMask.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMask.Location = new System.Drawing.Point(450, 8);
            this.LblMask.Name = "LblMask";
            this.LblMask.Size = new System.Drawing.Size(376, 32);
            this.LblMask.TabIndex = 143;
            this.LblMask.Text = "FPASS - Änderungshistorie";
            // 
            // FrmHistory
            // 
            this.AcceptButton = this.BtnSearch;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(1264, 921);
            this.Controls.Add(this.LblMask);
            this.Controls.Add(this.BtnBackTo);
            this.Controls.Add(this.PnlSearch);
            this.Controls.Add(this.DgrHistory);
            this.Name = "FrmHistory";
            this.Text = "FPASS - Änderungshistorie";
            this.Controls.SetChildIndex(this.DgrHistory, 0);
            this.Controls.SetChildIndex(this.PnlSearch, 0);
            this.Controls.SetChildIndex(this.BtnBackTo, 0);
            this.Controls.SetChildIndex(this.LblBaseHead, 0);
            this.Controls.SetChildIndex(this.StbBase, 0);
            this.Controls.SetChildIndex(this.LblMask, 0);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.SbpMessage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sbpenvironment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgrHistory)).EndInit();
            this.PnlSearch.ResumeLayout(false);
            this.PnlSearch.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		#region Accessors 

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
		/// Initialize input fields.
		/// </summary>
		private void InitView()
		{
			MnuFunction.Enabled = false;
            MnuZKS.Enabled = false;
			MnuReports.Enabled = false;
		}

		
		/// <summary>
		/// As elsewhere, called before form is closed
		/// </summary>
		internal override void PreClose()
		{
			 ClearFields();
			 DgrHistory.DataSource = null;
			 SbpMessage.Text = String.Empty;
		}


		/// <summary>
		/// Get controller to which this view belongs
		/// </summary>
		/// <returns></returns>
		private HistoryController GetMyController() 
		{
			return (HistoryController)mController;
		}

		
		/// <summary>
		/// Empty 18.01.04
		/// </summary>
		private void SetAuthorization() 
		{

		}

		/// <summary>
		/// Clear search fields
		/// </summary>
		private void ClearFields()
		{
			this.CboTable.Text = String.Empty;
			this.TxtFrom.Text  = String.Empty;
			this.TxtUntil.Text = String.Empty;
		}

		#endregion // End of Methods

		#region Events

		
		/// <summary>
		/// Button "Zurück"
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnCancel_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleCloseDialog();
		}

		
		/// <summary>
		/// Button "Suchen"
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnSearch_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventBtnSearchHistory();
		}

		
		/// <summary>
		/// Button "Maske leeren"
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnClearMask_Click(object sender, System.EventArgs e)
		{
			ClearFields();
			DgrHistory.DataSource = null;
		}

		/// <summary>
		/// Make sure a correct date is entered unless field is empty
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TxtFrom_Leave(object sender, System.EventArgs e)
		{
			mCorrectFormat = true;
			if ( this.TxtFrom.Text.Length > 0 )
			{
				if ( ! StringValidation.GetInstance().IsDateString (this.TxtFrom.Text.Trim()) ) 
				{
					try 
					{
						throw new UIWarningException(MessageSingleton.GetInstance().
							GetMessage(MessageSingleton.INVALID_FULLDATE) );
							
					} 
					catch ( UIWarningException uwe ) 
					{
						ExceptionProcessor.GetInstance().Process(uwe);
						TxtFrom.Focus();
						mCorrectFormat = false;
					}
				}
				if (TxtUntil.Text.Trim().Length > 0 && mCorrectFormat == true)
				{
					if (!StringValidation.GetInstance().IsDateValid(TxtFrom.Text.Trim(), TxtUntil.Text.Trim()))
					{
						try 
						{
							throw new UIWarningException(MessageSingleton.GetInstance().
								GetMessage(MessageSingleton.DYNDATA_FROMTILLDATE) );
						} 
						catch ( UIWarningException uwe ) 
						{
							ExceptionProcessor.GetInstance().Process(uwe);
							TxtUntil.Focus();
							TxtUntil.Text = TxtFrom.Text.Trim();
						}
					}
				}
			}
		}

		/// <summary>
		/// Make sure a correct date is entered unless field is empty
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TxtUntil_Leave(object sender, System.EventArgs e)
		{
			mCorrectFormat = true;
			if ( this.TxtUntil.Text.Length > 0 )
			{
				if ( ! StringValidation.GetInstance().IsDateString (this.TxtUntil.Text.Trim()) ) 
				{
					try 
					{
						throw new UIWarningException(MessageSingleton.GetInstance().
							GetMessage(MessageSingleton.INVALID_FULLDATE) );
							
					} 
					catch ( UIWarningException uwe ) 
					{
						ExceptionProcessor.GetInstance().Process(uwe);
						TxtUntil.Focus();
						mCorrectFormat = false;
					}
				}
				if (TxtFrom.Text.Trim().Length > 0 && mCorrectFormat == true)
				{
					if (!StringValidation.GetInstance().IsDateValid(TxtFrom.Text.Trim(), TxtUntil.Text.Trim()))
					{
						try 
						{
							throw new UIWarningException(MessageSingleton.GetInstance().
								GetMessage(MessageSingleton.DYNDATA_FROMTILLDATE) );
						} 
						catch ( UIWarningException uwe ) 
						{
							ExceptionProcessor.GetInstance().Process(uwe);
							TxtUntil.Focus();
						}
					}
				}
			}
		}

		#endregion // End of Events

	}
}

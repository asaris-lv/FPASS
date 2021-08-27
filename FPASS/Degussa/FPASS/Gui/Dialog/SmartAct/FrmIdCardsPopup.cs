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

using Degussa.FPASS.Gui.Dialog;
using Degussa.FPASS.Util.UserManagement;

using de.pta.Component.ListOfValues;
using de.pta.Component.Common;
using Degussa.FPASS.Gui;

namespace Degussa.FPASS.Gui.Dialog.SmartAct
{
	/// <summary>
	/// View shows list of coworkers with new photo id cards from SmartAct who have not yet been transferred to ZKS
    /// Standard model-view-controller pattern for FPASS.
    /// FrmIdCardsPopup extends from the FPASSBaseView.
	/// </summary>
	/// <remarks>
	/// <para><b>History</b></para>
	/// <div class="tablediv">
	/// <table class="dtTABLE" cellspacing="0">
	///		<tr>
	///			<th width="20%">PTA GmbH</th>
	///			<th width="20%">12/02/2015</th>
	///			<th width="60%">Remarks</th>
	///		</tr>
	/// </table>
	/// </div>
	/// </remarks>
	public class FrmIdCardsPopup : FPASSBaseView
	{
		#region Members
		
		//panels
		internal System.Windows.Forms.Panel PnlSearch;
		
		//labels
		internal System.Windows.Forms.Label LblSearch;
		internal System.Windows.Forms.Label LblMask;
		internal System.Windows.Forms.Label LblHintSmartAct;

        //textboxes

		//buttons
        internal System.Windows.Forms.Button BtnCancel;

		//tooltips
		private System.Windows.Forms.ToolTip TooSearch;
		private System.Windows.Forms.ToolTip TooClearMask;
		private System.Windows.Forms.ToolTip TooOk;
		private System.Windows.Forms.ToolTip TooBackTo;

		//tables
		public System.Windows.Forms.DataGrid DgrCoWorker;
		internal System.Windows.Forms.DataGridTableStyle DgrTableStyleCoWorker;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxCoWorkerID;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxSurname;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxFirstname;
        internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxWindowsID;
        internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxHitag;
        internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxMifare;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxValidUntil;
        internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxExcontractor;
        internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxCoord;
		

		//other
		private System.ComponentModel.IContainer components;
        internal Button btnGetAll;
        internal Label lblHintZKS;
        internal Button BtnrefreshList;
        internal Label LblAndZKS;

	
		#endregion //End of Members

		#region Constructors

        public FrmIdCardsPopup()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			InitView();

			SetAuthorization();
		}

		# endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.LblSearch = new System.Windows.Forms.Label();
            this.PnlSearch = new System.Windows.Forms.Panel();
            this.btnGetAll = new System.Windows.Forms.Button();
            this.lblHintZKS = new System.Windows.Forms.Label();
            this.LblAndZKS = new System.Windows.Forms.Label();
            this.LblHintSmartAct = new System.Windows.Forms.Label();
            this.DgrCoWorker = new System.Windows.Forms.DataGrid();
            this.DgrTableStyleCoWorker = new System.Windows.Forms.DataGridTableStyle();
            this.DgrTextBoxCoWorkerID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxSurname = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxFirstname = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxWindowsID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxHitag = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxMifare = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxValidUntil = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxExcontractor = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxCoord = new System.Windows.Forms.DataGridTextBoxColumn();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.LblMask = new System.Windows.Forms.Label();
            this.TooSearch = new System.Windows.Forms.ToolTip(this.components);
            this.TooClearMask = new System.Windows.Forms.ToolTip(this.components);
            this.TooOk = new System.Windows.Forms.ToolTip(this.components);
            this.TooBackTo = new System.Windows.Forms.ToolTip(this.components);
            this.BtnrefreshList = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.SbpMessage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sbpenvironment)).BeginInit();
            this.PnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgrCoWorker)).BeginInit();
            this.SuspendLayout();
            // 
            // LblBaseHead
            // 
            this.LblBaseHead.Size = new System.Drawing.Size(784, 40);
            // 
            // MnuFile
            // 
            this.MnuFile.Enabled = false;
            // 
            // MnuFunction
            // 
            this.MnuFunction.Enabled = false;
            // 
            // MnuReports
            // 
            this.MnuReports.Enabled = false;
            // 
            // LblSearch
            // 
            this.LblSearch.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSearch.Location = new System.Drawing.Point(-227, 68);
            this.LblSearch.Name = "LblSearch";
            this.LblSearch.Size = new System.Drawing.Size(76, 16);
            this.LblSearch.TabIndex = 20;
            this.LblSearch.Text = " Suche";
            // 
            // PnlSearch
            // 
            this.PnlSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlSearch.Controls.Add(this.btnGetAll);
            this.PnlSearch.Controls.Add(this.lblHintZKS);
            this.PnlSearch.Location = new System.Drawing.Point(6, 366);
            this.PnlSearch.Name = "PnlSearch";
            this.PnlSearch.Size = new System.Drawing.Size(780, 50);
            this.PnlSearch.TabIndex = 0;
            // 
            // btnGetAll
            // 
            this.btnGetAll.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetAll.Location = new System.Drawing.Point(610, 8);
            this.btnGetAll.Name = "btnGetAll";
            this.btnGetAll.Size = new System.Drawing.Size(155, 30);
            this.btnGetAll.TabIndex = 77;
            this.btnGetAll.Tag = "";
            this.btnGetAll.Text = "Alle &FFMA übertragen";
            this.TooOk.SetToolTip(this.btnGetAll, "Übernimmt alle FFMA und sendet diese nach ZKS");
            this.btnGetAll.Click += new System.EventHandler(this.btnGetAll_Click);
            // 
            // lblHintZKS
            // 
            this.lblHintZKS.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHintZKS.Location = new System.Drawing.Point(8, 16);
            this.lblHintZKS.Name = "lblHintZKS";
            this.lblHintZKS.Size = new System.Drawing.Size(262, 18);
            this.lblHintZKS.TabIndex = 6;
            this.lblHintZKS.Text = "Alle FFMA in der Liste nach ZKS übertragen:";
            // 
            // LblAndZKS
            // 
            this.LblAndZKS.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblAndZKS.Location = new System.Drawing.Point(12, 447);
            this.LblAndZKS.Name = "LblAndZKS";
            this.LblAndZKS.Size = new System.Drawing.Size(610, 18);
            this.LblAndZKS.TabIndex = 78;
            this.LblAndZKS.Text = "aber im ZKS-System noch nicht vorhanden sind.";
            // 
            // LblHintSmartAct
            // 
            this.LblHintSmartAct.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblHintSmartAct.Location = new System.Drawing.Point(12, 429);
            this.LblHintSmartAct.Name = "LblHintSmartAct";
            this.LblHintSmartAct.Size = new System.Drawing.Size(723, 18);
            this.LblHintSmartAct.TabIndex = 5;
            this.LblHintSmartAct.Text = "Hinweis: Diese Liste enthält die Fremdfirmenmitarbeiter, die einen neuen Lichtbil" +
    "dausweis aus SmartAct erhalten haben,";
            // 
            // DgrCoWorker
            // 
            this.DgrCoWorker.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DgrCoWorker.CaptionBackColor = System.Drawing.Color.SteelBlue;
            this.DgrCoWorker.CaptionFont = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DgrCoWorker.CaptionText = "Fremdfirmenmitarbeiter";
            this.DgrCoWorker.DataMember = "";
            this.DgrCoWorker.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.DgrCoWorker.Location = new System.Drawing.Point(10, 51);
            this.DgrCoWorker.Name = "DgrCoWorker";
            this.DgrCoWorker.ReadOnly = true;
            this.DgrCoWorker.Size = new System.Drawing.Size(763, 301);
            this.DgrCoWorker.TabIndex = 4;
            this.DgrCoWorker.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.DgrTableStyleCoWorker});
            this.DgrCoWorker.CurrentCellChanged += new System.EventHandler(this.DgrExternalContractor_CurrentCellChanged);
            this.DgrCoWorker.Enter += new System.EventHandler(this.DgrExternalContractor_Enter);
            // 
            // DgrTableStyleCoWorker
            // 
            this.DgrTableStyleCoWorker.DataGrid = this.DgrCoWorker;
            this.DgrTableStyleCoWorker.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.DgrTextBoxCoWorkerID,
            this.DgrTextBoxSurname,
            this.DgrTextBoxFirstname,
            this.DgrTextBoxWindowsID,
            this.DgrTextBoxHitag,
            this.DgrTextBoxMifare,
            this.DgrTextBoxValidUntil,
            this.DgrTextBoxExcontractor,
            this.DgrTextBoxCoord});
            this.DgrTableStyleCoWorker.HeaderFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DgrTableStyleCoWorker.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.DgrTableStyleCoWorker.MappingName = "RTTabCoWorker";
            // 
            // DgrTextBoxCoWorkerID
            // 
            this.DgrTextBoxCoWorkerID.Format = "";
            this.DgrTextBoxCoWorkerID.FormatInfo = null;
            this.DgrTextBoxCoWorkerID.HeaderText = "CWR_ID";
            this.DgrTextBoxCoWorkerID.MappingName = "FFMAID";
            this.DgrTextBoxCoWorkerID.NullText = "";
            this.DgrTextBoxCoWorkerID.Width = 1;
            // 
            // DgrTextBoxSurname
            // 
            this.DgrTextBoxSurname.Format = "";
            this.DgrTextBoxSurname.FormatInfo = null;
            this.DgrTextBoxSurname.HeaderText = "Nachname";
            this.DgrTextBoxSurname.MappingName = "Surname";
            this.DgrTextBoxSurname.NullText = "";
            this.DgrTextBoxSurname.Width = 75;
            // 
            // DgrTextBoxFirstname
            // 
            this.DgrTextBoxFirstname.Format = "";
            this.DgrTextBoxFirstname.FormatInfo = null;
            this.DgrTextBoxFirstname.HeaderText = "Vorname";
            this.DgrTextBoxFirstname.MappingName = "Firstname";
            this.DgrTextBoxFirstname.NullText = "";
            this.DgrTextBoxFirstname.Width = 75;
            // 
            // DgrTextBoxWindowsID
            // 
            this.DgrTextBoxWindowsID.Format = "";
            this.DgrTextBoxWindowsID.FormatInfo = null;
            this.DgrTextBoxWindowsID.HeaderText = "KonzernID";
            this.DgrTextBoxWindowsID.MappingName = "WindowsId";
            this.DgrTextBoxWindowsID.NullText = "";
            this.DgrTextBoxWindowsID.Width = 75;
            // 
            // DgrTextBoxHitag
            // 
            this.DgrTextBoxHitag.Format = "";
            this.DgrTextBoxHitag.FormatInfo = null;
            this.DgrTextBoxHitag.HeaderText = "IdCardHitag";
            this.DgrTextBoxHitag.MappingName = "IdCardHitag";
            this.DgrTextBoxHitag.NullText = "";
            this.DgrTextBoxHitag.Width = 80;
            // 
            // DgrTextBoxMifare
            // 
            this.DgrTextBoxMifare.Format = "";
            this.DgrTextBoxMifare.FormatInfo = null;
            this.DgrTextBoxMifare.HeaderText = "IdCardMifare";
            this.DgrTextBoxMifare.MappingName = "IdCardMifare";
            this.DgrTextBoxMifare.NullText = "";
            this.DgrTextBoxMifare.Width = 80;
            // 
            // DgrTextBoxValidUntil
            // 
            this.DgrTextBoxValidUntil.Format = "";
            this.DgrTextBoxValidUntil.FormatInfo = null;
            this.DgrTextBoxValidUntil.HeaderText = "Gültig bis";
            this.DgrTextBoxValidUntil.MappingName = "ValidUntil";
            this.DgrTextBoxValidUntil.NullText = "";
            this.DgrTextBoxValidUntil.Width = 65;
            // 
            // DgrTextBoxExcontractor
            // 
            this.DgrTextBoxExcontractor.Format = "";
            this.DgrTextBoxExcontractor.FormatInfo = null;
            this.DgrTextBoxExcontractor.HeaderText = "Fremdfirma";
            this.DgrTextBoxExcontractor.MappingName = "ExContractorName";
            this.DgrTextBoxExcontractor.NullText = "";
            this.DgrTextBoxExcontractor.Width = 150;
            // 
            // DgrTextBoxCoord
            // 
            this.DgrTextBoxCoord.Format = "";
            this.DgrTextBoxCoord.FormatInfo = null;
            this.DgrTextBoxCoord.HeaderText = "Koordinator";
            this.DgrTextBoxCoord.MappingName = "CoordNameAndTel";
            this.DgrTextBoxCoord.NullText = "";
            this.DgrTextBoxCoord.Width = 160;
            // 
            // BtnCancel
            // 
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Location = new System.Drawing.Point(614, 464);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(155, 30);
            this.BtnCancel.TabIndex = 6;
            this.BtnCancel.Tag = "";
            this.BtnCancel.Text = "&Schließen";
            this.TooBackTo.SetToolTip(this.BtnCancel, "Diese Maske ohne Änderungen schließen");
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // LblMask
            // 
            this.LblMask.BackColor = System.Drawing.Color.White;
            this.LblMask.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMask.Location = new System.Drawing.Point(221, 6);
            this.LblMask.Name = "LblMask";
            this.LblMask.Size = new System.Drawing.Size(376, 32);
            this.LblMask.TabIndex = 76;
            this.LblMask.Text = "FFMA mit neuem Ausweis";
            // 
            // BtnrefreshList
            // 
            this.BtnrefreshList.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnrefreshList.Location = new System.Drawing.Point(453, 464);
            this.BtnrefreshList.Name = "BtnrefreshList";
            this.BtnrefreshList.Size = new System.Drawing.Size(155, 30);
            this.BtnrefreshList.TabIndex = 79;
            this.BtnrefreshList.Tag = "";
            this.BtnrefreshList.Text = "&Aktualisieren";
            this.TooBackTo.SetToolTip(this.BtnrefreshList, "Liste der FFMA aktualisieren");
            this.BtnrefreshList.Click += new System.EventHandler(this.BtnrefreshList_Click);
            // 
            // FrmIdCardsPopup
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.BtnCancel;
            this.ClientSize = new System.Drawing.Size(784, 512);
            this.Controls.Add(this.BtnrefreshList);
            this.Controls.Add(this.LblHintSmartAct);
            this.Controls.Add(this.LblAndZKS);
            this.Controls.Add(this.DgrCoWorker);
            this.Controls.Add(this.LblMask);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.LblSearch);
            this.Controls.Add(this.PnlSearch);
            this.MaximumSize = new System.Drawing.Size(800, 550);
            this.MinimumSize = new System.Drawing.Size(800, 550);
            this.Name = "FrmIdCardsPopup";
            this.Text = "FPASS - FFMA mit neuem Ausweis";
            this.Controls.SetChildIndex(this.LblBaseHead, 0);
            this.Controls.SetChildIndex(this.PnlSearch, 0);
            this.Controls.SetChildIndex(this.LblSearch, 0);
            this.Controls.SetChildIndex(this.BtnCancel, 0);
            this.Controls.SetChildIndex(this.LblMask, 0);
            this.Controls.SetChildIndex(this.StbBase, 0);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            this.Controls.SetChildIndex(this.DgrCoWorker, 0);
            this.Controls.SetChildIndex(this.LblAndZKS, 0);
            this.Controls.SetChildIndex(this.LblHintSmartAct, 0);
            this.Controls.SetChildIndex(this.BtnrefreshList, 0);
            ((System.ComponentModel.ISupportInitialize)(this.SbpMessage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sbpenvironment)).EndInit();
            this.PnlSearch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DgrCoWorker)).EndInit();
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
			MnuReports.Enabled = false;
		}

		internal override void PreShow() 
		{
			
		}

		internal override void PreClose()
		{
			this.DgrCoWorker.DataSource=null;
		}

        /// <summary>
        /// is called before a dialog is hidden/covered. empty implementation because subclasses
        /// have to implement their individual logic
        /// </summary>
        internal override void PreHide()
        {
            this.DgrCoWorker.DataSource = null;
            this.Hide();
        }

		/// <summary>
		/// Get controller for this MVC triad
		/// </summary>
		/// <returns></returns>
		private IdCardsController GetMyController() 
		{
            return (IdCardsController)mController;
		}

		/// <summary>
		/// 
		/// </summary>
		private void SetAuthorization() 
		{

		}


		/// <summary>
		/// Datagrid has been navigated: get PK ID of seleczted Excontractor by finding out 
		/// which row in grid is selected: PK is 0th column
		/// </summary>
		private void TableNavigated()
		{
            //int rowIndex = this.DgrCoWorker.CurrentRowIndex;
            //if(-1 < rowIndex)
            //{
            //    mExContractorID = Convert.ToDecimal(this.DgrCoWorker[rowIndex, 0].ToString());
            //    this.BtnGetSingle.Enabled = true;
            //}				  
		}

		#endregion // End of Methods

		#region Events


		/// <summary>
		/// Button "Schliessen"
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnCancel_Click(object sender, System.EventArgs e)
		{
            GetMyController().HandleEventHide();

			// Cannot use GetMyController().HandleEventCloseDialog(); as PreShow etc on calling dialogue (FrmSummaryCoworker) not required and also not thread-safe
		}

		/// <summary>
		/// Have entered datagrid: get ID of selected exco
		/// Should fire if only one record present 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DgrExternalContractor_Enter(object sender, System.EventArgs e)
		{
			if ( this.DgrCoWorker.CurrentRowIndex == 0 )
			{
				TableNavigated();
			}
		}


		/// <summary>
		/// Moved to different cell in datagrid: get ID of selected exco 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DgrExternalContractor_CurrentCellChanged(object sender, System.EventArgs e)
		{
			if (this.DgrCoWorker.VisibleRowCount > 1)
			{
				TableNavigated();
			}
		}

    
        /// <summary>
        /// Transfers all selected CWr to ZKS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetAll_Click(object sender, EventArgs e)
        {
            IdCardsController controller = GetMyController();

            controller.HandleEventExportToZKS();
            if (!controller.ConditionIsMet())
            {
                controller.HandleEventHide();
            }
        }

        private void BtnrefreshList_Click(object sender, EventArgs e)
        {
            GetMyController().HandleEventUserRefresh();
        }

        #endregion
    }
}

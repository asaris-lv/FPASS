namespace Degussa.FPASS.Reports
{
	partial class FrmPopupSearchExco
	{
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Verwendete Ressourcen bereinigen.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Vom Windows Form-Designer generierter Code

		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung.
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPopupSearchExco));
            this.LblAttExContractor = new System.Windows.Forms.Label();
            this.BtnOK = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.pnlSearchExco = new System.Windows.Forms.Panel();
            this.BtnSelectAll = new System.Windows.Forms.Button();
            this.ClbAttExContractor = new System.Windows.Forms.CheckedListBox();
            this.TooOK = new System.Windows.Forms.ToolTip(this.components);
            this.TooCancel = new System.Windows.Forms.ToolTip(this.components);
            this.PnlOtherLists = new System.Windows.Forms.Panel();
            this.lblHelpSave = new System.Windows.Forms.Label();
            this.lblHelpOpn = new System.Windows.Forms.Label();
            this.BtnSaveList = new System.Windows.Forms.Button();
            this.BtnOpenList = new System.Windows.Forms.Button();
            this.lblOtherLists = new System.Windows.Forms.Label();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.lblHelpSearch = new System.Windows.Forms.Label();
            this.lblSearch = new System.Windows.Forms.Label();
            this.openFileExco = new System.Windows.Forms.OpenFileDialog();
            this.saveFileExco = new System.Windows.Forms.SaveFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsLblAction = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlSearchExco.SuspendLayout();
            this.PnlOtherLists.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // LblAttExContractor
            // 
            this.LblAttExContractor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblAttExContractor.Location = new System.Drawing.Point(7, 7);
            this.LblAttExContractor.Name = "LblAttExContractor";
            this.LblAttExContractor.Size = new System.Drawing.Size(148, 18);
            this.LblAttExContractor.TabIndex = 110;
            this.LblAttExContractor.Text = "Meine Fremdfirmenliste";
            // 
            // BtnOK
            // 
            this.BtnOK.Location = new System.Drawing.Point(229, 46);
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.Size = new System.Drawing.Size(107, 25);
            this.BtnOK.TabIndex = 111;
            this.BtnOK.Text = "Über&nehmen";
            this.TooOK.SetToolTip(this.BtnOK, "Übernimmt die ausgewählten FF und schließt das Popup");
            this.BtnOK.UseVisualStyleBackColor = true;
            this.BtnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(167, 473);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(135, 25);
            this.BtnCancel.TabIndex = 112;
            this.BtnCancel.Text = "&Abbrechen";
            this.TooOK.SetToolTip(this.BtnCancel, "Schließt das Popup, ohne die Auswahl zu übernehmen");
            this.BtnCancel.UseVisualStyleBackColor = true;
            // 
            // pnlSearchExco
            // 
            this.pnlSearchExco.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSearchExco.Controls.Add(this.BtnSelectAll);
            this.pnlSearchExco.Controls.Add(this.LblAttExContractor);
            this.pnlSearchExco.Controls.Add(this.BtnCancel);
            this.pnlSearchExco.Controls.Add(this.ClbAttExContractor);
            this.pnlSearchExco.Location = new System.Drawing.Point(4, 4);
            this.pnlSearchExco.Name = "pnlSearchExco";
            this.pnlSearchExco.Size = new System.Drawing.Size(313, 509);
            this.pnlSearchExco.TabIndex = 113;
            // 
            // BtnSelectAll
            // 
            this.BtnSelectAll.Location = new System.Drawing.Point(10, 473);
            this.BtnSelectAll.Name = "BtnSelectAll";
            this.BtnSelectAll.Size = new System.Drawing.Size(135, 25);
            this.BtnSelectAll.TabIndex = 111;
            this.BtnSelectAll.Text = "Alle/keine auswählen";
            this.BtnSelectAll.UseVisualStyleBackColor = true;
            this.BtnSelectAll.Click += new System.EventHandler(this.BtnSelectAll_Click);
            // 
            // ClbAttExContractor
            // 
            this.ClbAttExContractor.CheckOnClick = true;
            this.ClbAttExContractor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.ClbAttExContractor.Items.AddRange(new object[] {
            "dummy1",
            "dummy2"});
            this.ClbAttExContractor.Location = new System.Drawing.Point(10, 29);
            this.ClbAttExContractor.Name = "ClbAttExContractor";
            this.ClbAttExContractor.Size = new System.Drawing.Size(290, 436);
            this.ClbAttExContractor.TabIndex = 2;
            this.ClbAttExContractor.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ClbAttExContractor_ItemCheck);
            this.ClbAttExContractor.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ClbAttExContractor_KeyUp);
            this.ClbAttExContractor.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.ClbAttExContractor_PreviewKeyDown);
            // 
            // PnlOtherLists
            // 
            this.PnlOtherLists.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlOtherLists.Controls.Add(this.lblHelpSave);
            this.PnlOtherLists.Controls.Add(this.lblHelpOpn);
            this.PnlOtherLists.Controls.Add(this.BtnSaveList);
            this.PnlOtherLists.Controls.Add(this.BtnOpenList);
            this.PnlOtherLists.Controls.Add(this.lblOtherLists);
            this.PnlOtherLists.Location = new System.Drawing.Point(327, 4);
            this.PnlOtherLists.Name = "PnlOtherLists";
            this.PnlOtherLists.Size = new System.Drawing.Size(355, 146);
            this.PnlOtherLists.TabIndex = 114;
            // 
            // lblHelpSave
            // 
            this.lblHelpSave.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHelpSave.Location = new System.Drawing.Point(17, 91);
            this.lblHelpSave.Name = "lblHelpSave";
            this.lblHelpSave.Size = new System.Drawing.Size(195, 51);
            this.lblHelpSave.TabIndex = 114;
            this.lblHelpSave.Text = "Die Selektionen unter \"Meine Fremdfirmenliste\" in einer Datei speichern";
            // 
            // lblHelpOpn
            // 
            this.lblHelpOpn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHelpOpn.Location = new System.Drawing.Point(17, 34);
            this.lblHelpOpn.Name = "lblHelpOpn";
            this.lblHelpOpn.Size = new System.Drawing.Size(195, 51);
            this.lblHelpOpn.TabIndex = 113;
            this.lblHelpOpn.Text = "Eine bestehende Fremdfirmenliste aus einer Datei öffnen";
            // 
            // BtnSaveList
            // 
            this.BtnSaveList.Location = new System.Drawing.Point(229, 100);
            this.BtnSaveList.Name = "BtnSaveList";
            this.BtnSaveList.Size = new System.Drawing.Size(107, 25);
            this.BtnSaveList.TabIndex = 112;
            this.BtnSaveList.Text = "Liste &speichern";
            this.BtnSaveList.UseVisualStyleBackColor = true;
            this.BtnSaveList.Click += new System.EventHandler(this.BtnSaveList_Click);
            // 
            // BtnOpenList
            // 
            this.BtnOpenList.Location = new System.Drawing.Point(229, 46);
            this.BtnOpenList.Name = "BtnOpenList";
            this.BtnOpenList.Size = new System.Drawing.Size(107, 25);
            this.BtnOpenList.TabIndex = 111;
            this.BtnOpenList.Text = "&Liste öffnen";
            this.BtnOpenList.UseVisualStyleBackColor = true;
            this.BtnOpenList.Click += new System.EventHandler(this.BtnOpenList_Click);
            // 
            // lblOtherLists
            // 
            this.lblOtherLists.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOtherLists.Location = new System.Drawing.Point(7, 7);
            this.lblOtherLists.Name = "lblOtherLists";
            this.lblOtherLists.Size = new System.Drawing.Size(273, 18);
            this.lblOtherLists.TabIndex = 110;
            this.lblOtherLists.Text = "Weitere Aktionen";
            // 
            // pnlSearch
            // 
            this.pnlSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSearch.Controls.Add(this.lblHelpSearch);
            this.pnlSearch.Controls.Add(this.lblSearch);
            this.pnlSearch.Controls.Add(this.BtnOK);
            this.pnlSearch.Location = new System.Drawing.Point(327, 426);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(355, 87);
            this.pnlSearch.TabIndex = 115;
            // 
            // lblHelpSearch
            // 
            this.lblHelpSearch.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHelpSearch.Location = new System.Drawing.Point(17, 33);
            this.lblHelpSearch.Name = "lblHelpSearch";
            this.lblHelpSearch.Size = new System.Drawing.Size(195, 51);
            this.lblHelpSearch.TabIndex = 113;
            this.lblHelpSearch.Text = "Die Selektionen aus \"Meine Fremdfirmenliste\" für die Suche übernehemen";
            // 
            // lblSearch
            // 
            this.lblSearch.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.Location = new System.Drawing.Point(7, 7);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(273, 18);
            this.lblSearch.TabIndex = 110;
            this.lblSearch.Text = "Auswahl übernehmen";
            // 
            // saveFileExco
            // 
            this.saveFileExco.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileExco_FileOk);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsLblAction});
            this.statusStrip1.Location = new System.Drawing.Point(9, 521);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(676, 22);
            this.statusStrip1.TabIndex = 116;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsLblAction
            // 
            this.tsLblAction.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tsLblAction.Name = "tsLblAction";
            this.tsLblAction.Size = new System.Drawing.Size(51, 17);
            this.tsLblAction.Text = "Meldung";
            // 
            // FrmPopupSearchExco
            // 
            this.AcceptButton = this.BtnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.BtnCancel;
            this.ClientSize = new System.Drawing.Size(694, 552);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.PnlOtherLists);
            this.Controls.Add(this.pnlSearchExco);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmPopupSearchExco";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Fremfirmen suchen";
            this.TooCancel.SetToolTip(this, "Schließt den Dialog, ohne die Änderungen zu übernehmen");
            this.TooOK.SetToolTip(this, "Übernimmt die selektierten FF aus Suchkriterien");
            this.pnlSearchExco.ResumeLayout(false);
            this.PnlOtherLists.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        internal System.Windows.Forms.Label LblAttExContractor;
        private System.Windows.Forms.Button BtnOK;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Panel pnlSearchExco;
        private System.Windows.Forms.ToolTip TooOK;
        private System.Windows.Forms.ToolTip TooCancel;
        internal System.Windows.Forms.CheckedListBox ClbAttExContractor;
        private System.Windows.Forms.Button BtnSelectAll;
        private System.Windows.Forms.Panel PnlOtherLists;
        private System.Windows.Forms.Button BtnSaveList;
        private System.Windows.Forms.Button BtnOpenList;
        internal System.Windows.Forms.Label lblOtherLists;
        internal System.Windows.Forms.Label lblHelpSave;
        internal System.Windows.Forms.Label lblHelpOpn;
        private System.Windows.Forms.Panel pnlSearch;
        internal System.Windows.Forms.Label lblHelpSearch;
        internal System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.OpenFileDialog openFileExco;
        private System.Windows.Forms.SaveFileDialog saveFileExco;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsLblAction;


    }
}

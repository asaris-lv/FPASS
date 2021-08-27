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

namespace Degussa.FPASS.Gui.Dialog.Pass
{
	/// <summary>
	/// FrmSearchExternalContractor is the view 
	/// of the MVC-triad ExternalContractorSearchModel,
	/// ExternalContractorSearchController and FrmExternalSearchContractor.
	/// FrmSearchExternalContractor extends from the FPASSBaseView.
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
	public class FrmSearchExternalContractor : FPASSBaseView
	{
		#region Members
		
		//panels
		internal System.Windows.Forms.Panel PnlSearch;
		
		//labels
		internal System.Windows.Forms.Label LblSearch;
		internal System.Windows.Forms.Label LblMask;
		internal System.Windows.Forms.Label LblExternalContractor;

		//textboxes
		internal System.Windows.Forms.TextBox TxtExternalContractor;

		//buttons
		internal System.Windows.Forms.Button BtnBackTo;
        internal System.Windows.Forms.Button BtnAssume;
		internal System.Windows.Forms.Button BtnSearch;
		internal System.Windows.Forms.Button BtnClearMask;

		//tooltips
		private System.Windows.Forms.ToolTip TooSearch;
		private System.Windows.Forms.ToolTip TooClearMask;
		private System.Windows.Forms.ToolTip TooOk;
		private System.Windows.Forms.ToolTip TooBackTo;

		//tables
		public System.Windows.Forms.DataGrid DgrExternalContractor;
		internal System.Windows.Forms.DataGridTableStyle DgrTableStyleExContractor;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxEXCOID;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxName;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxStreet;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxPostcode;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxCity;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxCountry;

		//other
		private System.ComponentModel.IContainer components;

		/// <summary>
		/// holds the id of the current business object selected in the displayed table
		/// </summary>
		private decimal mExContractorID = -1;

		#endregion //End of Members

		#region Constructors

		public FrmSearchExternalContractor()
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
            this.BtnClearMask = new System.Windows.Forms.Button();
            this.TxtExternalContractor = new System.Windows.Forms.TextBox();
            this.BtnSearch = new System.Windows.Forms.Button();
            this.LblExternalContractor = new System.Windows.Forms.Label();
            this.DgrExternalContractor = new System.Windows.Forms.DataGrid();
            this.DgrTableStyleExContractor = new System.Windows.Forms.DataGridTableStyle();
            this.DgrTextBoxEXCOID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxName = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxStreet = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxPostcode = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxCity = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxCountry = new System.Windows.Forms.DataGridTextBoxColumn();
            this.BtnBackTo = new System.Windows.Forms.Button();
            this.BtnAssume = new System.Windows.Forms.Button();
            this.LblMask = new System.Windows.Forms.Label();
            this.TooSearch = new System.Windows.Forms.ToolTip(this.components);
            this.TooClearMask = new System.Windows.Forms.ToolTip(this.components);
            this.TooOk = new System.Windows.Forms.ToolTip(this.components);
            this.TooBackTo = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.SbpMessage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sbpenvironment)).BeginInit();
            this.PnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgrExternalContractor)).BeginInit();
            this.SuspendLayout();
            // 
            // LblBaseHead
            // 
            this.LblBaseHead.Size = new System.Drawing.Size(1008, 40);
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
            this.PnlSearch.Controls.Add(this.BtnClearMask);
            this.PnlSearch.Controls.Add(this.TxtExternalContractor);
            this.PnlSearch.Controls.Add(this.BtnSearch);
            this.PnlSearch.Controls.Add(this.LblExternalContractor);
            this.PnlSearch.Location = new System.Drawing.Point(11, 56);
            this.PnlSearch.Name = "PnlSearch";
            this.PnlSearch.Size = new System.Drawing.Size(992, 64);
            this.PnlSearch.TabIndex = 0;
            // 
            // BtnClearMask
            // 
            this.BtnClearMask.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClearMask.Location = new System.Drawing.Point(840, 16);
            this.BtnClearMask.Name = "BtnClearMask";
            this.BtnClearMask.Size = new System.Drawing.Size(136, 32);
            this.BtnClearMask.TabIndex = 3;
            this.BtnClearMask.Text = "&Maske leeren";
            this.TooClearMask.SetToolTip(this.BtnClearMask, "Löscht alle bereits eingegebenen Suchkriterien");
            this.BtnClearMask.Click += new System.EventHandler(this.BtnClearMask_Click);
            // 
            // TxtExternalContractor
            // 
            this.TxtExternalContractor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtExternalContractor.Location = new System.Drawing.Point(104, 22);
            this.TxtExternalContractor.Name = "TxtExternalContractor";
            this.TxtExternalContractor.Size = new System.Drawing.Size(286, 21);
            this.TxtExternalContractor.TabIndex = 1;
            // 
            // BtnSearch
            // 
            this.BtnSearch.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSearch.Location = new System.Drawing.Point(688, 16);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(136, 32);
            this.BtnSearch.TabIndex = 2;
            this.BtnSearch.Text = "&Suchen";
            this.TooSearch.SetToolTip(this.BtnSearch, "Löst den Suchvorgang aus");
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // LblExternalContractor
            // 
            this.LblExternalContractor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblExternalContractor.Location = new System.Drawing.Point(24, 24);
            this.LblExternalContractor.Name = "LblExternalContractor";
            this.LblExternalContractor.Size = new System.Drawing.Size(72, 16);
            this.LblExternalContractor.TabIndex = 5;
            this.LblExternalContractor.Text = "Fremdfirma";
            // 
            // DgrExternalContractor
            // 
            this.DgrExternalContractor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DgrExternalContractor.CaptionBackColor = System.Drawing.Color.SteelBlue;
            this.DgrExternalContractor.CaptionFont = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DgrExternalContractor.CaptionText = "Fremdfirma";
            this.DgrExternalContractor.DataMember = "";
            this.DgrExternalContractor.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.DgrExternalContractor.Location = new System.Drawing.Point(11, 134);
            this.DgrExternalContractor.Name = "DgrExternalContractor";
            this.DgrExternalContractor.ReadOnly = true;
            this.DgrExternalContractor.Size = new System.Drawing.Size(992, 507);
            this.DgrExternalContractor.TabIndex = 4;
            this.DgrExternalContractor.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.DgrTableStyleExContractor});
            this.DgrExternalContractor.CurrentCellChanged += new System.EventHandler(this.DgrExternalContractor_CurrentCellChanged);
            this.DgrExternalContractor.DoubleClick += new System.EventHandler(this.DgrExternalContractor_DoubleClick);
            this.DgrExternalContractor.Enter += new System.EventHandler(this.DgrExternalContractor_Enter);
            // 
            // DgrTableStyleExContractor
            // 
            this.DgrTableStyleExContractor.DataGrid = this.DgrExternalContractor;
            this.DgrTableStyleExContractor.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.DgrTextBoxEXCOID,
            this.DgrTextBoxName,
            this.DgrTextBoxStreet,
            this.DgrTextBoxPostcode,
            this.DgrTextBoxCity,
            this.DgrTextBoxCountry});
            this.DgrTableStyleExContractor.HeaderFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DgrTableStyleExContractor.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.DgrTableStyleExContractor.MappingName = "ArrayList";
            // 
            // DgrTextBoxEXCOID
            // 
            this.DgrTextBoxEXCOID.Format = "";
            this.DgrTextBoxEXCOID.FormatInfo = null;
            this.DgrTextBoxEXCOID.HeaderText = "EXCOID";
            this.DgrTextBoxEXCOID.MappingName = "PropAdminBOID";
            this.DgrTextBoxEXCOID.NullText = "";
            this.DgrTextBoxEXCOID.Width = 1;
            // 
            // DgrTextBoxName
            // 
            this.DgrTextBoxName.Format = "";
            this.DgrTextBoxName.FormatInfo = null;
            this.DgrTextBoxName.HeaderText = "Fremdfirma";
            this.DgrTextBoxName.MappingName = "PropAdminBOName";
            this.DgrTextBoxName.NullText = "";
            this.DgrTextBoxName.Width = 250;
            // 
            // DgrTextBoxStreet
            // 
            this.DgrTextBoxStreet.Format = "";
            this.DgrTextBoxStreet.FormatInfo = null;
            this.DgrTextBoxStreet.HeaderText = "Strasse";
            this.DgrTextBoxStreet.MappingName = "PropexcoStreet";
            this.DgrTextBoxStreet.NullText = "";
            this.DgrTextBoxStreet.Width = 200;
            // 
            // DgrTextBoxPostcode
            // 
            this.DgrTextBoxPostcode.Format = "";
            this.DgrTextBoxPostcode.FormatInfo = null;
            this.DgrTextBoxPostcode.HeaderText = "PLZ";
            this.DgrTextBoxPostcode.MappingName = "PropexcoPostcode";
            this.DgrTextBoxPostcode.NullText = "";
            this.DgrTextBoxPostcode.Width = 110;
            // 
            // DgrTextBoxCity
            // 
            this.DgrTextBoxCity.Format = "";
            this.DgrTextBoxCity.FormatInfo = null;
            this.DgrTextBoxCity.HeaderText = "Stadt";
            this.DgrTextBoxCity.MappingName = "PropexcoCity";
            this.DgrTextBoxCity.NullText = "";
            this.DgrTextBoxCity.Width = 200;
            // 
            // DgrTextBoxCountry
            // 
            this.DgrTextBoxCountry.Format = "";
            this.DgrTextBoxCountry.FormatInfo = null;
            this.DgrTextBoxCountry.HeaderText = "Land";
            this.DgrTextBoxCountry.MappingName = "PropexcoCountry";
            this.DgrTextBoxCountry.NullText = "";
            this.DgrTextBoxCountry.Width = 200;
            // 
            // BtnBackTo
            // 
            this.BtnBackTo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBackTo.Location = new System.Drawing.Point(856, 659);
            this.BtnBackTo.Name = "BtnBackTo";
            this.BtnBackTo.Size = new System.Drawing.Size(140, 32);
            this.BtnBackTo.TabIndex = 6;
            this.BtnBackTo.Tag = "";
            this.BtnBackTo.Text = "&Zurück";
            this.TooBackTo.SetToolTip(this.BtnBackTo, "Zurück zur Übersichtsmaske");
            this.BtnBackTo.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnAssume
            // 
            this.BtnAssume.Enabled = false;
            this.BtnAssume.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAssume.Location = new System.Drawing.Point(704, 659);
            this.BtnAssume.Name = "BtnAssume";
            this.BtnAssume.Size = new System.Drawing.Size(140, 32);
            this.BtnAssume.TabIndex = 5;
            this.BtnAssume.Tag = "";
            this.BtnAssume.Text = "&Übernehmen";
            this.TooOk.SetToolTip(this.BtnAssume, "Übernimmt die ausgewählte Fremdfirma");
            this.BtnAssume.Click += new System.EventHandler(this.BtnRegisterDetails_Click);
            // 
            // LblMask
            // 
            this.LblMask.BackColor = System.Drawing.Color.White;
            this.LblMask.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMask.Location = new System.Drawing.Point(299, 8);
            this.LblMask.Name = "LblMask";
            this.LblMask.Size = new System.Drawing.Size(376, 32);
            this.LblMask.TabIndex = 76;
            this.LblMask.Text = "FPASS - Suche Fremdfirma";
            // 
            // FrmSearchExternalContractor
            // 
            this.AcceptButton = this.BtnSearch;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(1008, 712);
            this.Controls.Add(this.LblMask);
            this.Controls.Add(this.BtnBackTo);
            this.Controls.Add(this.BtnAssume);
            this.Controls.Add(this.DgrExternalContractor);
            this.Controls.Add(this.LblSearch);
            this.Controls.Add(this.PnlSearch);
            this.MaximumSize = new System.Drawing.Size(1024, 750);
            this.MinimumSize = new System.Drawing.Size(1024, 750);
            this.Name = "FrmSearchExternalContractor";
            this.Text = "FPASS - Suche Fremdfirma";
            this.Controls.SetChildIndex(this.PnlSearch, 0);
            this.Controls.SetChildIndex(this.LblSearch, 0);
            this.Controls.SetChildIndex(this.DgrExternalContractor, 0);
            this.Controls.SetChildIndex(this.BtnAssume, 0);
            this.Controls.SetChildIndex(this.BtnBackTo, 0);
            this.Controls.SetChildIndex(this.LblBaseHead, 0);
            this.Controls.SetChildIndex(this.StbBase, 0);
            this.Controls.SetChildIndex(this.LblMask, 0);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.SbpMessage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sbpenvironment)).EndInit();
            this.PnlSearch.ResumeLayout(false);
            this.PnlSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgrExternalContractor)).EndInit();
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
			this.BtnAssume.Enabled = false;
			this.TxtExternalContractor.Focus();

		}

		internal override void PreClose()
		{
			this.DgrExternalContractor.DataSource=null;
			this.TxtExternalContractor.Text= String.Empty;
			this.TxtExternalContractor.Focus();
		}


		/// <summary>
		/// Get controller for this MVC triad
		/// </summary>
		/// <returns></returns>
		private ExternalContractorSearchController GetMyController() 
		{
			return (ExternalContractorSearchController)mController;
		}


		/// <summary>
		/// Empty 19.01.04
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
			int rowIndex = this.DgrExternalContractor.CurrentRowIndex;
			if(-1 < rowIndex)
			{
				mExContractorID = Convert.ToDecimal(this.DgrExternalContractor[rowIndex, 0].ToString());
				this.BtnAssume.Enabled = true;
			}				  
		}

		#endregion // End of Methods

		#region Events


		/// <summary>
		/// Button "Zurück"
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnExit_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleCloseDialog();
		}


		/// <summary>
		/// Button "Übernehmen"
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnRegisterDetails_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventGiveBackContractor(Convert.ToString(mExContractorID));
		}


		/// <summary>
		/// Button "Suchen"
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnSearch_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventBtnSearchExContractor();
		}


		/// <summary>
		/// Have entered datagrid: get ID of selected exco
		/// Should fire if only one record present 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DgrExternalContractor_Enter(object sender, System.EventArgs e)
		{
			if ( this.DgrExternalContractor.CurrentRowIndex == 0 )
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
			if (this.DgrExternalContractor.VisibleRowCount > 1)
			{
				TableNavigated();
			}
		}

        /// <summary>
        /// Double-click on datagrid gives back current exco id
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgrExternalContractor_DoubleClick(object sender, EventArgs e)
        {
            if (mExContractorID >0)
                GetMyController().HandleEventGiveBackContractor(Convert.ToString(mExContractorID));
        }

		/// <summary>
		/// Clear textbox at top of form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnClearMask_Click(object sender, System.EventArgs e)
		{
			this.TxtExternalContractor.Text = "";
		}


		#endregion // End of Events	
	}
}

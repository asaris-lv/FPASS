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
	/// FrmSearchIDCardReader extends FPASSBaseView.
	/// </summary>
	/// <remarks>
	/// <para><b>History</b></para>
	/// <div class="tablediv">
	/// <table class="dtTABLE" cellspacing="0">
	///		<tr>
	///			<th width="20%">PTA GmbH</th>
	///			<th width="20%">18/11/2014</th>
	///			<th width="60%">Search dialogue for selecting an ID card reader by its number</th>
	///		</tr>
	/// </table>
	/// </div>
	/// </remarks>
	public class FrmSearchIDCardReader : FPASSBaseView
	{
		#region Members

		
		//labels
		internal System.Windows.Forms.Label LblSearch;
        internal System.Windows.Forms.Label LblMask;

		//buttons
		internal System.Windows.Forms.Button BtnBack;
        internal System.Windows.Forms.Button BtnGetReaderNum;

		//tooltips
		private System.Windows.Forms.ToolTip TooSearch;
		private System.Windows.Forms.ToolTip TooClearMask;
		private System.Windows.Forms.ToolTip TooOk;
		private System.Windows.Forms.ToolTip TooBackTo;

		//tables
		public System.Windows.Forms.DataGrid DgrIDCardReader;
		internal System.Windows.Forms.DataGridTableStyle DgrTableStyleIDCardReader;
        internal System.Windows.Forms.DataGridTextBoxColumn DgrTxtRdrNum;
        internal System.Windows.Forms.DataGridTextBoxColumn DgrTxtRdrType;
        internal System.Windows.Forms.DataGridTextBoxColumn DgrTxtDescription;

		//other
		private System.ComponentModel.IContainer components;

		/// <summary>
		/// Current ID card reader number (not its PK)
		/// </summary>
        private int mIDCardReaderNo = -1;
        private string mIDCardReaderType = null;

    
		#endregion //End of Members

		#region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public FrmSearchIDCardReader()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			InitView();
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
            this.DgrIDCardReader = new System.Windows.Forms.DataGrid();
            this.DgrTableStyleIDCardReader = new System.Windows.Forms.DataGridTableStyle();
            this.DgrTxtRdrNum = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTxtRdrType = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTxtDescription = new System.Windows.Forms.DataGridTextBoxColumn();
            this.BtnBack = new System.Windows.Forms.Button();
            this.BtnGetReaderNum = new System.Windows.Forms.Button();
            this.LblMask = new System.Windows.Forms.Label();
            this.TooSearch = new System.Windows.Forms.ToolTip(this.components);
            this.TooClearMask = new System.Windows.Forms.ToolTip(this.components);
            this.TooOk = new System.Windows.Forms.ToolTip(this.components);
            this.TooBackTo = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.SbpMessage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sbpenvironment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgrIDCardReader)).BeginInit();
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
            // DgrIDCardReader
            // 
            this.DgrIDCardReader.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DgrIDCardReader.CaptionBackColor = System.Drawing.Color.SteelBlue;
            this.DgrIDCardReader.CaptionFont = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DgrIDCardReader.CaptionText = "Liste der Ausweisleser";
            this.DgrIDCardReader.DataMember = "";
            this.DgrIDCardReader.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.DgrIDCardReader.Location = new System.Drawing.Point(12, 66);
            this.DgrIDCardReader.Name = "DgrIDCardReader";
            this.DgrIDCardReader.ReadOnly = true;
            this.DgrIDCardReader.Size = new System.Drawing.Size(992, 567);
            this.DgrIDCardReader.TabIndex = 4;
            this.DgrIDCardReader.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.DgrTableStyleIDCardReader});
            this.DgrIDCardReader.Paint += new System.Windows.Forms.PaintEventHandler(this.DgrIDCardReader_Paint);
            this.DgrIDCardReader.DoubleClick += new System.EventHandler(this.DgrIDCardReader_DoubleClick);
            // 
            // DgrTableStyleIDCardReader
            // 
            this.DgrTableStyleIDCardReader.DataGrid = this.DgrIDCardReader;
            this.DgrTableStyleIDCardReader.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.DgrTxtRdrNum,
            this.DgrTxtRdrType,
            this.DgrTxtDescription});
            this.DgrTableStyleIDCardReader.HeaderFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DgrTableStyleIDCardReader.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.DgrTableStyleIDCardReader.MappingName = "ArrayList";
            // 
            // DgrTxtRdrNum
            // 
            this.DgrTxtRdrNum.Format = "";
            this.DgrTxtRdrNum.FormatInfo = null;
            this.DgrTxtRdrNum.HeaderText = "Ausweislesernummer";
            this.DgrTxtRdrNum.MappingName = "ReaderNumber";
            this.DgrTxtRdrNum.NullText = "";
            this.DgrTxtRdrNum.Width = 115;
            // 
            // DgrTxtRdrType
            // 
            this.DgrTxtRdrType.Format = "";
            this.DgrTxtRdrType.FormatInfo = null;
            this.DgrTxtRdrType.HeaderText = "Ausweislesertyp";
            this.DgrTxtRdrType.MappingName = "ReaderType";
            this.DgrTxtRdrType.NullText = "";
            this.DgrTxtRdrType.Width = 200;
            // 
            // DgrTxtDescription
            // 
            this.DgrTxtDescription.Format = "";
            this.DgrTxtDescription.FormatInfo = null;
            this.DgrTxtDescription.HeaderText = "Beschreibung";
            this.DgrTxtDescription.MappingName = "Description";
            this.DgrTxtDescription.NullText = "";
            this.DgrTxtDescription.Width = 600;
            // 
            // BtnBack
            // 
            this.BtnBack.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBack.Location = new System.Drawing.Point(856, 651);
            this.BtnBack.Name = "BtnBack";
            this.BtnBack.Size = new System.Drawing.Size(140, 32);
            this.BtnBack.TabIndex = 6;
            this.BtnBack.Tag = "";
            this.BtnBack.Text = "&Zurück";
            this.TooBackTo.SetToolTip(this.BtnBack, "Zurück zur Bearbeitungsmaske");
            this.BtnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // BtnGetReaderNum
            // 
            this.BtnGetReaderNum.Enabled = false;
            this.BtnGetReaderNum.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnGetReaderNum.Location = new System.Drawing.Point(704, 651);
            this.BtnGetReaderNum.Name = "BtnGetReaderNum";
            this.BtnGetReaderNum.Size = new System.Drawing.Size(140, 32);
            this.BtnGetReaderNum.TabIndex = 5;
            this.BtnGetReaderNum.Tag = "";
            this.BtnGetReaderNum.Text = "&Übernehmen";
            this.TooOk.SetToolTip(this.BtnGetReaderNum, "Übernimmt den ausgewählten Ausweisleser.");
            this.BtnGetReaderNum.Click += new System.EventHandler(this.BtnGetReaderNum_Click);
            // 
            // LblMask
            // 
            this.LblMask.BackColor = System.Drawing.Color.White;
            this.LblMask.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMask.Location = new System.Drawing.Point(312, 7);
            this.LblMask.Name = "LblMask";
            this.LblMask.Size = new System.Drawing.Size(396, 32);
            this.LblMask.TabIndex = 76;
            this.LblMask.Text = "FPASS - Suche Ausweisleser";
            // 
            // FrmSearchIDCardReader
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(1008, 712);
            this.Controls.Add(this.BtnGetReaderNum);
            this.Controls.Add(this.LblMask);
            this.Controls.Add(this.LblSearch);
            this.Controls.Add(this.DgrIDCardReader);
            this.Controls.Add(this.BtnBack);
            this.MaximumSize = new System.Drawing.Size(1024, 750);
            this.MinimumSize = new System.Drawing.Size(1024, 750);
            this.Name = "FrmSearchIDCardReader";
            this.Text = "FPASS - Suche Ausweisleser";
            this.Load += new System.EventHandler(this.BtnSearch_Click);
            this.Controls.SetChildIndex(this.BtnBack, 0);
            this.Controls.SetChildIndex(this.DgrIDCardReader, 0);
            this.Controls.SetChildIndex(this.LblSearch, 0);
            this.Controls.SetChildIndex(this.LblBaseHead, 0);
            this.Controls.SetChildIndex(this.StbBase, 0);
            this.Controls.SetChildIndex(this.LblMask, 0);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            this.Controls.SetChildIndex(this.BtnGetReaderNum, 0);
            ((System.ComponentModel.ISupportInitialize)(this.SbpMessage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sbpenvironment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgrIDCardReader)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		#region Accessors 

        /// <summary>
        /// Current ID card type
        /// Hitag2 oder Mifare
        /// </summary>
        public string IDCardReaderType
        {
            get { return mIDCardReaderType; }
            set { mIDCardReaderType = value; }
        }

		#endregion

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
		/// Initialize input fields
		/// </summary>
		private void InitView()
		{
			MnuFunction.Enabled = false;
			MnuReports.Enabled = false;
		}

        /// <summary>
        /// Standard PreShow
        /// </summary>
		internal override void PreShow() 
		{
			this.BtnGetReaderNum.Enabled = false;
		}


        /// <summary>
        /// Standard PreClose
        /// </summary>
		internal override void PreClose()
		{
			this.DgrIDCardReader.DataSource=null;			
		}


		/// <summary>
		/// Gets Controller for this MVC triad
		/// </summary>
		/// <returns></returns>
		private IDCardReaderSearchController GetMyController() 
		{
            return (IDCardReaderSearchController)mController;
		}


		/// <summary>
		/// Datagrid has been navigated: get PK ID of seleczted IDCardReader by finding out 
		/// which row in grid is selected: PK is 0th column
		/// </summary>
		private void TableNavigated()
		{
            int rowIndex = this.DgrIDCardReader.CurrentRowIndex;
            if (-1 < rowIndex)
            {
                mIDCardReaderNo = Convert.ToInt32(this.DgrIDCardReader[rowIndex, 0].ToString());
                mIDCardReaderType = this.DgrIDCardReader[rowIndex, 1].ToString();
                this.BtnGetReaderNum.Enabled = true;
            }				  
		}

		#endregion // End of Methods

		#region Events

		/// <summary>
		/// Button "Zurück"
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void BtnBack_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleCloseDialog();
		}

		/// <summary>
		/// Button "Übernehmen": gives Id back to parent controller
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void BtnGetReaderNum_Click(object sender, System.EventArgs e)
		{
            GetMyController().HandleEventGiveBackIDCardReader(mIDCardReaderNo, mIDCardReaderType);
		}


		/// <summary>
        /// Raised when user clicks Button "Suchen" and on dialogue's Load event. 
        /// Works out which Id card reader type should be used and executes search.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnSearch_Click(object sender, System.EventArgs e)
		{
          
            // Gets and casts parent Controller in order to get at mIDCardReaderType
            AbstractController parentController = GetMyController().Parent;
            CoWorkerController coWorkerController = (CoWorkerController)parentController;

            mIDCardReaderType = coWorkerController.IDCardReaderType;
            GetMyController().SearchIDCardReaders(mIDCardReaderType);
		}

		/// <summary>
		/// Double-click has same effect has button "Übernehmen"
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void DgrIDCardReader_DoubleClick(object sender, EventArgs e)
        {
            if (mIDCardReaderNo > 0)
                GetMyController().HandleEventGiveBackIDCardReader(mIDCardReaderNo, mIDCardReaderType);
        }
        

        private void DgrIDCardReader_Paint(object sender, PaintEventArgs e)
        {
            if (this.DgrIDCardReader.VisibleRowCount > 0)
            {
                TableNavigated();
            }
        }

        #endregion // End of Events
    }
}

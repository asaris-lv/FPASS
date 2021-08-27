using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Degussa.FPASS.Gui.Dialog.Pass
{
	/// <summary>
	/// Summary description for FrmPopCoWorkerHist.
	/// </summary>
	/// <remarks>
	/// <para><b>History</b></para>
	/// <div class="tablediv">
	/// <table class="dtTABLE" cellspacing="0">
	///		<tr>
	///			<th width="20%">Author</th>
	///			<th width="20%">Date</th>
	///			<th width="60%">Remarks</th>
	///		</tr>
	///		<tr>
	///			<td width="20%">Author</td>
	///			<td width="20%">month/day/year</td>
	///			<td width="60%">Comment</td>
	///		</tr>
	/// </table>
	/// </div>
	/// </remarks>
	public class FrmPopCoWorkerHist : FPASSBaseView
	{
		private System.Windows.Forms.Button btnClose;
		internal System.Windows.Forms.DataGrid dgrCoordHist;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxHistID;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxCoordinator;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxRespDateFrom;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxRespDateUntil;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxChangeDate;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxUserName;
		internal System.Windows.Forms.Label LblMaskTitle;
		internal System.Windows.Forms.DataGridTableStyle DgrAssignCwrCoord;
		#region Members

		#endregion //End of Members

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FrmPopCoWorkerHist()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			InitView();
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.dgrCoordHist = new System.Windows.Forms.DataGrid();
            this.DgrAssignCwrCoord = new System.Windows.Forms.DataGridTableStyle();
            this.DgrTextBoxHistID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxCoordinator = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxRespDateFrom = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxRespDateUntil = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxChangeDate = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxUserName = new System.Windows.Forms.DataGridTextBoxColumn();
            this.btnClose = new System.Windows.Forms.Button();
            this.LblMaskTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.SbpMessage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sbpenvironment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgrCoordHist)).BeginInit();
            this.SuspendLayout();
            // 
            // LblBaseHead
            // 
            this.LblBaseHead.Size = new System.Drawing.Size(642, 40);
            // 
            // dgrCoordHist
            // 
            this.dgrCoordHist.CaptionBackColor = System.Drawing.Color.SteelBlue;
            this.dgrCoordHist.CaptionText = "Koordinatoren";
            this.dgrCoordHist.DataMember = "";
            this.dgrCoordHist.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgrCoordHist.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dgrCoordHist.Location = new System.Drawing.Point(8, 48);
            this.dgrCoordHist.Name = "dgrCoordHist";
            this.dgrCoordHist.ReadOnly = true;
            this.dgrCoordHist.Size = new System.Drawing.Size(624, 208);
            this.dgrCoordHist.TabIndex = 0;
            this.dgrCoordHist.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.DgrAssignCwrCoord});
            // 
            // DgrAssignCwrCoord
            // 
            this.DgrAssignCwrCoord.DataGrid = this.dgrCoordHist;
            this.DgrAssignCwrCoord.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.DgrTextBoxHistID,
            this.DgrTextBoxCoordinator,
            this.DgrTextBoxRespDateFrom,
            this.DgrTextBoxRespDateUntil,
            this.DgrTextBoxChangeDate,
            this.DgrTextBoxUserName});
            this.DgrAssignCwrCoord.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DgrAssignCwrCoord.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.DgrAssignCwrCoord.MappingName = "RTTabAssigntCwrCoord";
            // 
            // DgrTextBoxHistID
            // 
            this.DgrTextBoxHistID.Format = "";
            this.DgrTextBoxHistID.FormatInfo = null;
            this.DgrTextBoxHistID.HeaderText = "HistID";
            this.DgrTextBoxHistID.MappingName = "HISTID";
            this.DgrTextBoxHistID.NullText = "";
            this.DgrTextBoxHistID.Width = 1;
            // 
            // DgrTextBoxCoordinator
            // 
            this.DgrTextBoxCoordinator.Format = "";
            this.DgrTextBoxCoordinator.FormatInfo = null;
            this.DgrTextBoxCoordinator.HeaderText = "Koordinator";
            this.DgrTextBoxCoordinator.MappingName = "Coordinator";
            this.DgrTextBoxCoordinator.NullText = "";
            this.DgrTextBoxCoordinator.Width = 160;
            // 
            // DgrTextBoxRespDateFrom
            // 
            this.DgrTextBoxRespDateFrom.Format = "dd.MM.yyyy HH:mm:ss";
            this.DgrTextBoxRespDateFrom.FormatInfo = null;
            this.DgrTextBoxRespDateFrom.HeaderText = "Zust‰ndig von";
            this.DgrTextBoxRespDateFrom.MappingName = "RespDateFrom";
            this.DgrTextBoxRespDateFrom.NullText = "";
            this.DgrTextBoxRespDateFrom.Width = 105;
            // 
            // DgrTextBoxRespDateUntil
            // 
            this.DgrTextBoxRespDateUntil.Format = "dd.MM.yyyy HH:mm:ss";
            this.DgrTextBoxRespDateUntil.FormatInfo = null;
            this.DgrTextBoxRespDateUntil.HeaderText = "Zust‰ndig bis";
            this.DgrTextBoxRespDateUntil.MappingName = "RespDateUntil";
            this.DgrTextBoxRespDateUntil.NullText = "";
            this.DgrTextBoxRespDateUntil.Width = 105;
            // 
            // DgrTextBoxChangeDate
            // 
            this.DgrTextBoxChangeDate.Format = "dd.MM.yyyy HH:mm:ss";
            this.DgrTextBoxChangeDate.FormatInfo = null;
            this.DgrTextBoxChangeDate.HeaderText = "Angelegt am";
            this.DgrTextBoxChangeDate.MappingName = "ChangeDate";
            this.DgrTextBoxChangeDate.NullText = "";
            this.DgrTextBoxChangeDate.Width = 105;
            // 
            // DgrTextBoxUserName
            // 
            this.DgrTextBoxUserName.Format = "";
            this.DgrTextBoxUserName.FormatInfo = null;
            this.DgrTextBoxUserName.HeaderText = "Angelegt durch";
            this.DgrTextBoxUserName.MappingName = "UserName";
            this.DgrTextBoxUserName.NullText = "";
            this.DgrTextBoxUserName.Width = 110;
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(263, 264);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(120, 30);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "&Schlieﬂen";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // LblMaskTitle
            // 
            this.LblMaskTitle.BackColor = System.Drawing.Color.White;
            this.LblMaskTitle.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMaskTitle.Location = new System.Drawing.Point(178, 8);
            this.LblMaskTitle.Name = "LblMaskTitle";
            this.LblMaskTitle.Size = new System.Drawing.Size(286, 32);
            this.LblMaskTitle.TabIndex = 79;
            this.LblMaskTitle.Text = "FPASS - Koordinatoren";
            // 
            // FrmPopCoWorkerHist
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(642, 299);
            this.Controls.Add(this.LblMaskTitle);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgrCoordHist);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximumSize = new System.Drawing.Size(648, 325);
            this.Menu = null;
            this.Name = "FrmPopCoWorkerHist";
            this.Text = "FPASS - Historie";
            this.Controls.SetChildIndex(this.dgrCoordHist, 0);
            this.Controls.SetChildIndex(this.btnClose, 0);
            this.Controls.SetChildIndex(this.LblBaseHead, 0);
            this.Controls.SetChildIndex(this.StbBase, 0);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            this.Controls.SetChildIndex(this.LblMaskTitle, 0);
            ((System.ComponentModel.ISupportInitialize)(this.SbpMessage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sbpenvironment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgrCoordHist)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		#region Accessors 

		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// Initialize input fields.
		/// </summary>
		private void InitView()
		{

		}

		/// <summary>
		/// Get instance of associated controller in current MVC
		/// </summary>
		/// <returns></returns>
		private PopCoWorkerHistController GetMyController() 
		{
			return ((PopCoWorkerHistController) mController);
		}
		
		#endregion // End of Methods

		#region Events

		/// <summary>
		/// Don't want to close this using MVC logic as would call PreShow in Coworker form
		/// which would lose unsaved changes etc 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		#endregion

	}
}

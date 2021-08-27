using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Degussa.FPASS.Util.UserManagement;
using Degussa.FPASS.Util.ListOfValues;
using Degussa.FPASS.Util.Validation;
using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.Db.DataSets;

using de.pta.Component.ListOfValues;
using de.pta.Component.Common;
using de.pta.Component.Errorhandling;

namespace Degussa.FPASS.Gui.Dialog.Administration
{
	/// <summary>
	/// Summary description for FrmPopExcoCoordHist.
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
	public class FrmPopExcoCoordHist : FPASSBaseView
	{
		private System.Windows.Forms.Button btnClose;
		internal System.Windows.Forms.DataGrid dgrCoordHist;
		internal System.Windows.Forms.DataGridTableStyle DgrTableStyleAssignExContCoord;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxCoordNameAndTel;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxRespFrom;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxRespUntil;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxChangeUserName;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxDeleteUserName;
		internal System.Windows.Forms.Label LblMaskTitle;
		#region Members

		#endregion //End of Members

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FrmPopExcoCoordHist()
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
            this.btnClose = new System.Windows.Forms.Button();
            this.dgrCoordHist = new System.Windows.Forms.DataGrid();
            this.DgrTableStyleAssignExContCoord = new System.Windows.Forms.DataGridTableStyle();
            this.DgrTextBoxCoordNameAndTel = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxRespFrom = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxRespUntil = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxChangeUserName = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxDeleteUserName = new System.Windows.Forms.DataGridTextBoxColumn();
            this.LblMaskTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.SbpMessage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sbpenvironment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgrCoordHist)).BeginInit();
            this.SuspendLayout();
            // 
            // LblBaseHead
            // 
            this.LblBaseHead.Size = new System.Drawing.Size(632, 40);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(248, 250);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(120, 30);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "&Schließen";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dgrCoordHist
            // 
            this.dgrCoordHist.CaptionBackColor = System.Drawing.Color.SteelBlue;
            this.dgrCoordHist.CaptionText = "Koordinatoren";
            this.dgrCoordHist.DataMember = "";
            this.dgrCoordHist.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dgrCoordHist.Location = new System.Drawing.Point(8, 48);
            this.dgrCoordHist.Name = "dgrCoordHist";
            this.dgrCoordHist.ReadOnly = true;
            this.dgrCoordHist.Size = new System.Drawing.Size(624, 199);
            this.dgrCoordHist.TabIndex = 3;
            this.dgrCoordHist.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.DgrTableStyleAssignExContCoord});
            // 
            // DgrTableStyleAssignExContCoord
            // 
            this.DgrTableStyleAssignExContCoord.DataGrid = this.dgrCoordHist;
            this.DgrTableStyleAssignExContCoord.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.DgrTextBoxCoordNameAndTel,
            this.DgrTextBoxRespFrom,
            this.DgrTextBoxRespUntil,
            this.DgrTextBoxChangeUserName,
            this.DgrTextBoxDeleteUserName});
            this.DgrTableStyleAssignExContCoord.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DgrTableStyleAssignExContCoord.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.DgrTableStyleAssignExContCoord.MappingName = "RTTabCoordHist";
            this.DgrTableStyleAssignExContCoord.PreferredColumnWidth = 100;
            this.DgrTableStyleAssignExContCoord.ReadOnly = true;
            // 
            // DgrTextBoxCoordNameAndTel
            // 
            this.DgrTextBoxCoordNameAndTel.Format = "";
            this.DgrTextBoxCoordNameAndTel.FormatInfo = null;
            this.DgrTextBoxCoordNameAndTel.HeaderText = "Koordinator";
            this.DgrTextBoxCoordNameAndTel.MappingName = "CoordNameAndTel";
            this.DgrTextBoxCoordNameAndTel.NullText = "";
            this.DgrTextBoxCoordNameAndTel.Width = 180;
            // 
            // DgrTextBoxRespFrom
            // 
            this.DgrTextBoxRespFrom.Format = "dd.MM.yyyy HH:mm:ss";
            this.DgrTextBoxRespFrom.FormatInfo = null;
            this.DgrTextBoxRespFrom.HeaderText = "Zuständig von";
            this.DgrTextBoxRespFrom.MappingName = "RespFrom";
            this.DgrTextBoxRespFrom.NullText = "";
            this.DgrTextBoxRespFrom.Width = 101;
            // 
            // DgrTextBoxRespUntil
            // 
            this.DgrTextBoxRespUntil.Format = "dd.MM.yyyy HH:mm:ss";
            this.DgrTextBoxRespUntil.FormatInfo = null;
            this.DgrTextBoxRespUntil.HeaderText = "Zuständig bis";
            this.DgrTextBoxRespUntil.MappingName = "RespUntil";
            this.DgrTextBoxRespUntil.NullText = "";
            this.DgrTextBoxRespUntil.Width = 101;
            // 
            // DgrTextBoxChangeUserName
            // 
            this.DgrTextBoxChangeUserName.Format = "";
            this.DgrTextBoxChangeUserName.FormatInfo = null;
            this.DgrTextBoxChangeUserName.HeaderText = "Angelegt durch";
            this.DgrTextBoxChangeUserName.MappingName = "ChangeUserName";
            this.DgrTextBoxChangeUserName.NullText = "";
            this.DgrTextBoxChangeUserName.Width = 101;
            // 
            // DgrTextBoxDeleteUserName
            // 
            this.DgrTextBoxDeleteUserName.Format = "";
            this.DgrTextBoxDeleteUserName.FormatInfo = null;
            this.DgrTextBoxDeleteUserName.HeaderText = "Gelöscht durch";
            this.DgrTextBoxDeleteUserName.MappingName = "DeleteUserName";
            this.DgrTextBoxDeleteUserName.NullText = "";
            this.DgrTextBoxDeleteUserName.Width = 102;
            // 
            // LblMaskTitle
            // 
            this.LblMaskTitle.BackColor = System.Drawing.Color.White;
            this.LblMaskTitle.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMaskTitle.Location = new System.Drawing.Point(181, 8);
            this.LblMaskTitle.Name = "LblMaskTitle";
            this.LblMaskTitle.Size = new System.Drawing.Size(281, 32);
            this.LblMaskTitle.TabIndex = 78;
            this.LblMaskTitle.Text = "FPASS - Koordinatoren";
            // 
            // FrmPopExcoCoordHist
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(632, 291);
            this.Controls.Add(this.LblMaskTitle);
            this.Controls.Add(this.dgrCoordHist);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximumSize = new System.Drawing.Size(648, 325);
            this.Menu = null;
            this.MinimumSize = new System.Drawing.Size(648, 325);
            this.Name = "FrmPopExcoCoordHist";
            this.Text = "FPASS - Historie";
            this.Controls.SetChildIndex(this.btnClose, 0);
            this.Controls.SetChildIndex(this.dgrCoordHist, 0);
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

		private PopECODHistController GetMyController() 
		{
			return ((PopECODHistController) mController);
		}

		#endregion // End of Methods

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			 this.Close();
		}

	}
}

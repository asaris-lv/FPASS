using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using System.Data;
using System.Data.OleDb;

using Degussa.FPASS.Bo.Administration;
using Degussa.FPASS.Db.DataSets;
using Degussa.FPASS.Gui;
using Degussa.FPASS.Util.UserManagement;

using de.pta.Component.ListOfValues;
using de.pta.Component.Common;


namespace Degussa.FPASS.Gui.Dialog.User
{
	/// <summary>
	/// FrmUserToRole is the view of the MVC-triad UserToRoleModel,
	/// UserToRoleController and FrmUserToRole.
	/// FrmUserToRole extends from the FPASSBaseView.
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
	public class FrmUserToRole : FPASSBaseView
	{	

		#region Members

		//panels
		internal System.Windows.Forms.Panel PnlAssignmentRole;

		//labels
		internal System.Windows.Forms.Label LblMask;
		internal System.Windows.Forms.Label LblUser;
        internal System.Windows.Forms.Label LblAssignRole;

		//textboxes
		internal System.Windows.Forms.TextBox TxtUser;

		//comboboxes
		internal System.Windows.Forms.ComboBox CboAssignRole;

		//buttons
		internal System.Windows.Forms.Button BtnSummaryRoles;
		internal System.Windows.Forms.Button BtnAssign;
		internal System.Windows.Forms.Button BtnDeleteAssignment;
		internal System.Windows.Forms.Button BtnBackTo;

		//tooltips
		private System.Windows.Forms.ToolTip TooAssign;
		private System.Windows.Forms.ToolTip TooUndoAssign;
		private System.Windows.Forms.ToolTip TooSummaryRoles;
		private System.Windows.Forms.ToolTip TooBackTo;

		//tables
		public System.Windows.Forms.DataGrid DgrRoles;
		private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn2;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn3;

		//other
		private System.ComponentModel.IContainer components;

		internal  ArrayList roleUser = new ArrayList();

		// typified dataset
		protected DSRole mDSRole;

		protected int    mCurrentUMRoleID   = -1;
		protected string mCurrentRoleName;
        internal Label LblTitleAssignRole;
		protected BOUser mBOUser;

		#endregion //End of Members

		#region Constructors

		public FrmUserToRole()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Create dataset, load LOVs etc
			InitView();

			MnuFunction.Enabled = false;
			MnuReports.Enabled = false;
		}

		#endregion //End of Constructors

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.LblUser = new System.Windows.Forms.Label();
            this.BtnBackTo = new System.Windows.Forms.Button();
            this.CboAssignRole = new System.Windows.Forms.ComboBox();
            this.PnlAssignmentRole = new System.Windows.Forms.Panel();
            this.LblAssignRole = new System.Windows.Forms.Label();
            this.BtnAssign = new System.Windows.Forms.Button();
            this.BtnDeleteAssignment = new System.Windows.Forms.Button();
            this.TxtUser = new System.Windows.Forms.TextBox();
            this.LblMask = new System.Windows.Forms.Label();
            this.DgrRoles = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn3 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.BtnSummaryRoles = new System.Windows.Forms.Button();
            this.TooAssign = new System.Windows.Forms.ToolTip(this.components);
            this.TooUndoAssign = new System.Windows.Forms.ToolTip(this.components);
            this.TooSummaryRoles = new System.Windows.Forms.ToolTip(this.components);
            this.TooBackTo = new System.Windows.Forms.ToolTip(this.components);
            this.LblTitleAssignRole = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.SbpMessage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sbpenvironment)).BeginInit();
            this.PnlAssignmentRole.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgrRoles)).BeginInit();
            this.SuspendLayout();
            // 
            // LblUser
            // 
            this.LblUser.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblUser.Location = new System.Drawing.Point(24, 81);
            this.LblUser.Name = "LblUser";
            this.LblUser.Size = new System.Drawing.Size(48, 16);
            this.LblUser.TabIndex = 48;
            this.LblUser.Text = "User";
            // 
            // BtnBackTo
            // 
            this.BtnBackTo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBackTo.Location = new System.Drawing.Point(1098, 851);
            this.BtnBackTo.Name = "BtnBackTo";
            this.BtnBackTo.Size = new System.Drawing.Size(150, 30);
            this.BtnBackTo.TabIndex = 8;
            this.BtnBackTo.Text = "&Zurück";
            this.TooUndoAssign.SetToolTip(this.BtnBackTo, "Zurück zur Maske Benutzerverwaltung");
            this.BtnBackTo.Click += new System.EventHandler(this.button8_Click);
            // 
            // CboAssignRole
            // 
            this.CboAssignRole.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboAssignRole.Location = new System.Drawing.Point(72, 38);
            this.CboAssignRole.Name = "CboAssignRole";
            this.CboAssignRole.Size = new System.Drawing.Size(488, 23);
            this.CboAssignRole.Sorted = true;
            this.CboAssignRole.TabIndex = 4;
            // 
            // PnlAssignmentRole
            // 
            this.PnlAssignmentRole.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlAssignmentRole.Controls.Add(this.CboAssignRole);
            this.PnlAssignmentRole.Controls.Add(this.LblAssignRole);
            this.PnlAssignmentRole.Location = new System.Drawing.Point(20, 748);
            this.PnlAssignmentRole.Name = "PnlAssignmentRole";
            this.PnlAssignmentRole.Size = new System.Drawing.Size(1231, 88);
            this.PnlAssignmentRole.TabIndex = 3;
            // 
            // LblAssignRole
            // 
            this.LblAssignRole.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblAssignRole.Location = new System.Drawing.Point(16, 40);
            this.LblAssignRole.Name = "LblAssignRole";
            this.LblAssignRole.Size = new System.Drawing.Size(64, 21);
            this.LblAssignRole.TabIndex = 53;
            this.LblAssignRole.Text = "Rolle";
            // 
            // BtnAssign
            // 
            this.BtnAssign.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAssign.Location = new System.Drawing.Point(618, 851);
            this.BtnAssign.Name = "BtnAssign";
            this.BtnAssign.Size = new System.Drawing.Size(150, 30);
            this.BtnAssign.TabIndex = 5;
            this.BtnAssign.Text = "Z&uordnen";
            this.TooAssign.SetToolTip(this.BtnAssign, "Ausgewählte Rolle wird dem User zugeordnet");
            this.BtnAssign.Click += new System.EventHandler(this.BtnAssign_Click);
            // 
            // BtnDeleteAssignment
            // 
            this.BtnDeleteAssignment.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDeleteAssignment.Location = new System.Drawing.Point(778, 851);
            this.BtnDeleteAssignment.Name = "BtnDeleteAssignment";
            this.BtnDeleteAssignment.Size = new System.Drawing.Size(150, 30);
            this.BtnDeleteAssignment.TabIndex = 6;
            this.BtnDeleteAssignment.Text = "Zuordnung &aufheben";
            this.TooBackTo.SetToolTip(this.BtnDeleteAssignment, "Die bestehende Zuordnung der ausgewählten Rolle zum aktiven User wird aufgehoben");
            this.BtnDeleteAssignment.Click += new System.EventHandler(this.BtnDeleteAssignment_Click);
            // 
            // TxtUser
            // 
            this.TxtUser.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtUser.Location = new System.Drawing.Point(80, 79);
            this.TxtUser.Name = "TxtUser";
            this.TxtUser.ReadOnly = true;
            this.TxtUser.Size = new System.Drawing.Size(240, 21);
            this.TxtUser.TabIndex = 1;
            this.TxtUser.TabStop = false;
            // 
            // LblMask
            // 
            this.LblMask.BackColor = System.Drawing.Color.White;
            this.LblMask.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMask.Location = new System.Drawing.Point(452, 8);
            this.LblMask.Name = "LblMask";
            this.LblMask.Size = new System.Drawing.Size(352, 32);
            this.LblMask.TabIndex = 77;
            this.LblMask.Text = "FPASS - Zuordnung Rolle";
            // 
            // DgrRoles
            // 
            this.DgrRoles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DgrRoles.CaptionBackColor = System.Drawing.Color.SteelBlue;
            this.DgrRoles.CaptionFont = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DgrRoles.CaptionText = "Zugeordnete Rollen";
            this.DgrRoles.DataMember = "";
            this.DgrRoles.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.DgrRoles.Location = new System.Drawing.Point(20, 151);
            this.DgrRoles.Name = "DgrRoles";
            this.DgrRoles.ReadOnly = true;
            this.DgrRoles.Size = new System.Drawing.Size(1231, 579);
            this.DgrRoles.TabIndex = 2;
            this.DgrRoles.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.dataGridTableStyle1});
            this.DgrRoles.Paint += new System.Windows.Forms.PaintEventHandler(this.DgrRoles_Paint);
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.DataGrid = this.DgrRoles;
            this.dataGridTableStyle1.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.dataGridTextBoxColumn1,
            this.dataGridTextBoxColumn3,
            this.dataGridTextBoxColumn2});
            this.dataGridTableStyle1.HeaderFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGridTableStyle1.MappingName = "UM_ROLELINK";
            this.dataGridTableStyle1.PreferredColumnWidth = 1215;
            this.dataGridTableStyle1.ReadOnly = true;
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            this.dataGridTextBoxColumn1.HeaderText = "RL_ROLEID";
            this.dataGridTextBoxColumn1.MappingName = "RL_ROLEID";
            this.dataGridTextBoxColumn1.Width = 0;
            // 
            // dataGridTextBoxColumn3
            // 
            this.dataGridTextBoxColumn3.Format = "";
            this.dataGridTextBoxColumn3.FormatInfo = null;
            this.dataGridTextBoxColumn3.HeaderText = "RoleName";
            this.dataGridTextBoxColumn3.MappingName = "UM_ROLE_NAME";
            this.dataGridTextBoxColumn3.Width = 0;
            // 
            // dataGridTextBoxColumn2
            // 
            this.dataGridTextBoxColumn2.Format = "";
            this.dataGridTextBoxColumn2.FormatInfo = null;
            this.dataGridTextBoxColumn2.HeaderText = "Rolle";
            this.dataGridTextBoxColumn2.MappingName = "UM_ROLEFORMAT";
            this.dataGridTextBoxColumn2.Width = 1230;
            // 
            // BtnSummaryRoles
            // 
            this.BtnSummaryRoles.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSummaryRoles.Location = new System.Drawing.Point(938, 851);
            this.BtnSummaryRoles.Name = "BtnSummaryRoles";
            this.BtnSummaryRoles.Size = new System.Drawing.Size(150, 30);
            this.BtnSummaryRoles.TabIndex = 7;
            this.BtnSummaryRoles.Text = "Übersicht &Rollen";
            this.TooSummaryRoles.SetToolTip(this.BtnSummaryRoles, "Öffnet die Maske Übersicht aller Rollen");
            this.BtnSummaryRoles.Click += new System.EventHandler(this.BtnSummaryRoles_Click);
            // 
            // LblTitleAssignRole
            // 
            this.LblTitleAssignRole.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTitleAssignRole.Location = new System.Drawing.Point(29, 742);
            this.LblTitleAssignRole.Name = "LblTitleAssignRole";
            this.LblTitleAssignRole.Size = new System.Drawing.Size(112, 16);
            this.LblTitleAssignRole.TabIndex = 78;
            this.LblTitleAssignRole.Text = " Zuordnung Rolle";
            // 
            // FrmUserToRole
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(1270, 971);
            this.Controls.Add(this.LblTitleAssignRole);
            this.Controls.Add(this.PnlAssignmentRole);
            this.Controls.Add(this.LblMask);
            this.Controls.Add(this.BtnSummaryRoles);
            this.Controls.Add(this.DgrRoles);
            this.Controls.Add(this.TxtUser);
            this.Controls.Add(this.LblUser);
            this.Controls.Add(this.BtnBackTo);
            this.Controls.Add(this.BtnAssign);
            this.Controls.Add(this.BtnDeleteAssignment);
            this.Name = "FrmUserToRole";
            this.Text = "Benutzerverwaltung - Zuordnung Rolle zu User";
            this.Leave += new System.EventHandler(this.FrmUserToRole_Leave);
            this.Controls.SetChildIndex(this.BtnDeleteAssignment, 0);
            this.Controls.SetChildIndex(this.BtnAssign, 0);
            this.Controls.SetChildIndex(this.BtnBackTo, 0);
            this.Controls.SetChildIndex(this.LblUser, 0);
            this.Controls.SetChildIndex(this.TxtUser, 0);
            this.Controls.SetChildIndex(this.DgrRoles, 0);
            this.Controls.SetChildIndex(this.BtnSummaryRoles, 0);
            this.Controls.SetChildIndex(this.LblBaseHead, 0);
            this.Controls.SetChildIndex(this.StbBase, 0);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            this.Controls.SetChildIndex(this.LblMask, 0);
            this.Controls.SetChildIndex(this.PnlAssignmentRole, 0);
            this.Controls.SetChildIndex(this.LblTitleAssignRole, 0);
            ((System.ComponentModel.ISupportInitialize)(this.SbpMessage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sbpenvironment)).EndInit();
            this.PnlAssignmentRole.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DgrRoles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		#region Accessors 

		// ID of current role
		public int CurrentUMRoleID
		{
			get 
			{
				return mCurrentUMRoleID;
			}
			set 
			{
				mCurrentUMRoleID = value;
			}
		} 

		// Name of current role
		public string CurrentRoleName
		{
			get 
			{
				return mCurrentRoleName;
			}
			set 
			{
				mCurrentRoleName = value;
			}
		} 

		// Current UserBO: used in Set
		public BOUser CurrentBOUser
		{
			get 
			{
				return mBOUser;
			}
			set 
			{
				mBOUser = value;
			}
		} 
		
		public DSRole CurrentDSRole
		{
			get 
			{
				return mDSRole;
			}
			set 
			{
				mDSRole = value;
			}
		} 

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

		private void InitView()
		{
			this.CreateDataSet();
		}

		private void CreateDataSet() 
		{
			mDSRole = new DSRole();
		}


		/// <summary>
		/// Not used, Combobox filled from model
		/// </summary>
		internal override void FillLists() 
		{
		
		}

		private void FrmUserToRole_Load_1(object sender, System.EventArgs e)
		{
			roleUser.Clear();
		}

		internal void SetAuthorization() 
		{

		}


		private void TableNavigated()
		{	
			int rowIndex = this.DgrRoles.CurrentRowIndex;
			if(-1 < rowIndex)
			{				
				mCurrentUMRoleID = Convert.ToInt32(this.DgrRoles[rowIndex, 0].ToString());
				mCurrentRoleName = (this.DgrRoles[rowIndex, 1].ToString());
			}
			((UserToRoleController)mController).HandleEventTableNavigated();
		}

		#endregion // End of Methods

		#region Events

	
		private void BtnSummaryRoles_Click(object sender, System.EventArgs e)
		{
			((UserToRoleController)mController).HandleEventOpenRoleDialog();
		}

		private void BtnDeleteAssignment_Click(object sender, System.EventArgs e)
		{
			((UserToRoleController)mController).HandleEventDeleteRoleAssignment();
		}

		private void BtnAssign_Click(object sender, System.EventArgs e)
		{
			((UserToRoleController)mController).HandleEventAssignmentNewRole();
		}

		// supsedes current cell changed & enter events 14.11.03
		// When datagrid is sorted or a row selected, this event id fired, but also many times during load
		private void DgrRoles_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			if ( this.DgrRoles.DataSource != null && this.DgrRoles.VisibleRowCount > 0)
			{
				this.TableNavigated();
			}
		
		}

		private void FrmUserToRole_Leave(object sender, System.EventArgs e)
		{
			if ( ! DesignMode )
			{
				((UserToRoleController)mController).HandleCloseDialog();
			}
		}

		/// <summary>
		/// This is button "Zurück"
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button8_Click(object sender, System.EventArgs e)
		{
			((UserToRoleController)mController).HandleCloseDialog();
		}

		#endregion // End of events

	}
}

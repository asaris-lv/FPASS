using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Degussa.FPASS.Bo.Administration;
using Degussa.FPASS.Db.DataSets;
using Degussa.FPASS.Util.UserManagement;

using de.pta.Component.ListOfValues;

namespace Degussa.FPASS.Gui.Dialog.User
{
	/// <summary>
	/// FrmRole is the view of the MVC-triad RoleModel,
	/// RoleController and FrmRole.
	/// FrmRole extends from the FPASSBaseView.
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
	public class FrmRole : FPASSBaseView
	{
		#region Members

		//labels
		internal System.Windows.Forms.Label LblRole;
		internal System.Windows.Forms.Label LblMask;

		//comboboxes
		internal System.Windows.Forms.ComboBox CboRole;

		//buttons
		internal System.Windows.Forms.Button BtnBackTo;
		internal System.Windows.Forms.Button BtnSearch;

		//tooltips
		private System.Windows.Forms.ToolTip TooSearch;
		private System.Windows.Forms.ToolTip TooBackTo;

		//tables
		public System.Windows.Forms.DataGrid DgrUserHaveRole;
		private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn2;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn3;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn4;

		// Typified DataSet
		protected DSUser mDSUSer;

		private System.ComponentModel.IContainer components;

		#endregion //End of Members

		#region Constructors

		public FrmRole()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			InitView();
			
			// initialize LOVs
			this.FillLists();
		}

		#endregion endregion of constructors

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.DgrUserHaveRole = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn4 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn3 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.LblRole = new System.Windows.Forms.Label();
            this.CboRole = new System.Windows.Forms.ComboBox();
            this.BtnBackTo = new System.Windows.Forms.Button();
            this.LblMask = new System.Windows.Forms.Label();
            this.BtnSearch = new System.Windows.Forms.Button();
            this.TooSearch = new System.Windows.Forms.ToolTip(this.components);
            this.TooBackTo = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.SbpMessage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sbpenvironment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgrUserHaveRole)).BeginInit();
            this.SuspendLayout();
            // 
            // DgrUserHaveRole
            // 
            this.DgrUserHaveRole.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DgrUserHaveRole.CaptionBackColor = System.Drawing.Color.SteelBlue;
            this.DgrUserHaveRole.CaptionFont = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DgrUserHaveRole.CaptionText = "Zugeordnete User";
            this.DgrUserHaveRole.DataMember = "";
            this.DgrUserHaveRole.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.DgrUserHaveRole.Location = new System.Drawing.Point(24, 144);
            this.DgrUserHaveRole.Name = "DgrUserHaveRole";
            this.DgrUserHaveRole.ReadOnly = true;
            this.DgrUserHaveRole.Size = new System.Drawing.Size(1222, 733);
            this.DgrUserHaveRole.TabIndex = 4;
            this.DgrUserHaveRole.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.dataGridTableStyle1});
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.DataGrid = this.DgrUserHaveRole;
            this.dataGridTableStyle1.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.dataGridTextBoxColumn1,
            this.dataGridTextBoxColumn2,
            this.dataGridTextBoxColumn4,
            this.dataGridTextBoxColumn3});
            this.dataGridTableStyle1.HeaderFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGridTableStyle1.MappingName = "VW_FPASS_USERBYROLE";
            this.dataGridTableStyle1.PreferredColumnWidth = 768;
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            this.dataGridTextBoxColumn1.HeaderText = "UM_USER_ID";
            this.dataGridTextBoxColumn1.MappingName = "UM_USER_ID";
            this.dataGridTextBoxColumn1.Width = 0;
            // 
            // dataGridTextBoxColumn2
            // 
            this.dataGridTextBoxColumn2.Format = "";
            this.dataGridTextBoxColumn2.FormatInfo = null;
            this.dataGridTextBoxColumn2.HeaderText = "FPASS_USER_ID";
            this.dataGridTextBoxColumn2.MappingName = "FPASS_USER_ID";
            this.dataGridTextBoxColumn2.Width = 0;
            // 
            // dataGridTextBoxColumn4
            // 
            this.dataGridTextBoxColumn4.Format = "";
            this.dataGridTextBoxColumn4.FormatInfo = null;
            this.dataGridTextBoxColumn4.HeaderText = "UserID";
            this.dataGridTextBoxColumn4.MappingName = "UM_USERAPPLID";
            this.dataGridTextBoxColumn4.Width = 200;
            // 
            // dataGridTextBoxColumn3
            // 
            this.dataGridTextBoxColumn3.Format = "";
            this.dataGridTextBoxColumn3.FormatInfo = null;
            this.dataGridTextBoxColumn3.HeaderText = "Benutzername & Telefon";
            this.dataGridTextBoxColumn3.MappingName = "BOTHNAMESTEL";
            this.dataGridTextBoxColumn3.Width = 950;
            // 
            // LblRole
            // 
            this.LblRole.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblRole.Location = new System.Drawing.Point(24, 96);
            this.LblRole.Name = "LblRole";
            this.LblRole.Size = new System.Drawing.Size(48, 16);
            this.LblRole.TabIndex = 79;
            this.LblRole.Text = "Rolle";
            // 
            // CboRole
            // 
            this.CboRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboRole.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboRole.Items.AddRange(new object[] {
            "",
            "Produktion",
            "Verwaltung"});
            this.CboRole.Location = new System.Drawing.Point(80, 94);
            this.CboRole.Name = "CboRole";
            this.CboRole.Size = new System.Drawing.Size(469, 23);
            this.CboRole.Sorted = true;
            this.CboRole.TabIndex = 1;
            // 
            // BtnBackTo
            // 
            this.BtnBackTo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBackTo.Location = new System.Drawing.Point(1118, 837);
            this.BtnBackTo.Name = "BtnBackTo";
            this.BtnBackTo.Size = new System.Drawing.Size(120, 30);
            this.BtnBackTo.TabIndex = 5;
            this.BtnBackTo.Text = "&Zurück";
            this.TooBackTo.SetToolTip(this.BtnBackTo, "Zurück zur Maske Zuordnung Rolle zu User");
            this.BtnBackTo.Click += new System.EventHandler(this.BtnBackTo_Click);
            // 
            // LblMask
            // 
            this.LblMask.BackColor = System.Drawing.Color.White;
            this.LblMask.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMask.Location = new System.Drawing.Point(310, 8);
            this.LblMask.Name = "LblMask";
            this.LblMask.Size = new System.Drawing.Size(400, 32);
            this.LblMask.TabIndex = 85;
            this.LblMask.Text = "FPASS - Zugeordnete User";
            // 
            // BtnSearch
            // 
            this.BtnSearch.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSearch.Location = new System.Drawing.Point(1118, 89);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(120, 30);
            this.BtnSearch.TabIndex = 3;
            this.BtnSearch.Text = "&Suchen";
            this.TooSearch.SetToolTip(this.BtnSearch, "Löst den Suchvorgang aus");
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // FrmRole
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(1270, 971);
            this.Controls.Add(this.BtnSearch);
            this.Controls.Add(this.LblMask);
            this.Controls.Add(this.BtnBackTo);
            this.Controls.Add(this.CboRole);
            this.Controls.Add(this.DgrUserHaveRole);
            this.Controls.Add(this.LblRole);
            this.Name = "FrmRole";
            this.Text = "FrmRole";
            this.Controls.SetChildIndex(this.LblRole, 0);
            this.Controls.SetChildIndex(this.DgrUserHaveRole, 0);
            this.Controls.SetChildIndex(this.LblBaseHead, 0);
            this.Controls.SetChildIndex(this.StbBase, 0);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            this.Controls.SetChildIndex(this.CboRole, 0);
            this.Controls.SetChildIndex(this.BtnBackTo, 0);
            this.Controls.SetChildIndex(this.LblMask, 0);
            this.Controls.SetChildIndex(this.BtnSearch, 0);
            ((System.ComponentModel.ISupportInitialize)(this.SbpMessage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sbpenvironment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgrUserHaveRole)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		#region Accessors 

		public DSUser DSUser
		{
			get 
			{
				return mDSUSer;
			}
			set 
			{
				mDSUSer = value;
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

		public void CreateDataSet() 
		{
			mDSUSer = new DSUser();
		}

		/// <summary>
		/// Initialize input fields.
		/// </summary>
		private void InitView()
		{
			MnuFunction.Enabled = false;
			MnuReports.Enabled = false;


			this.CreateDataSet();
		}

		internal override void FillLists() 
		{
			this.FillRole();
		}

		private void FillRole() 
		{
			ArrayList arrRoles = new ArrayList(); 
			arrRoles = LovSingleton.GetInstance().GetRootList(null, "FPASS_ROLEBYMAND", "UM_ROLEFORMAT");
			arrRoles.Add(new LovItem("0", ""));
			arrRoles.Reverse();
			this.CboRole.DataSource = arrRoles;
			this.CboRole.DisplayMember = "ItemValue";
			this.CboRole.ValueMember = "DecId";
		}

		private  void SetAuthorization() 
		{

		}

		#endregion // End of Methods

		#region Events

		private void BtnBackTo_Click(object sender, System.EventArgs e)
		{
			((RoleController)mController).HandleEventBtnBack();
			((RoleController)mController).HandleCloseDialog();
		}

		private void BtnSearch_Click(object sender, System.EventArgs e)
		{
			((RoleController)mController).HandleEventBtnSearch();
		}

		#endregion // End of Events

	}
}

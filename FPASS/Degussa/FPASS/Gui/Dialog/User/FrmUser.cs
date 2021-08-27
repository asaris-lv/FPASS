using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using System.Data;
using System.Data.OleDb;

using Degussa.FPASS.Db;
using Degussa.FPASS.Db.DataSets;
using Degussa.FPASS.Gui;
using Degussa.FPASS.Util.UserManagement;
using Degussa.FPASS.FPASSApplication;

using de.pta.Component.ListOfValues;

namespace Degussa.FPASS.Gui.Dialog.User
{
	/// <summary>
	/// FrmUser is the view of the MVC-triad UserModel,
	/// UserController and FrmUser.
	/// FrmUser extends from the FPASSBaseView.
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
	public class FrmUser : FPASSBaseView
	{

		#region Members

		//panels
		internal System.Windows.Forms.Panel PnlSearch;
		internal System.Windows.Forms.Panel PnlEdit;

		//labels
		internal System.Windows.Forms.Label LblCoTelephoneNumber;
		internal System.Windows.Forms.Label LblCoCraft;
		internal System.Windows.Forms.Label LblMask;
		internal System.Windows.Forms.Label LblEditUser;
		internal System.Windows.Forms.Label LblEditFirstname;
		internal System.Windows.Forms.Label LblEditSurname;
		internal System.Windows.Forms.Label LblSearchFirstname;
		internal System.Windows.Forms.Label LblSearchUser;
		internal System.Windows.Forms.Label LblSearchSurname;
		internal System.Windows.Forms.Label LblEditPhone;
		internal System.Windows.Forms.Label LblEditDomain;
		internal System.Windows.Forms.Label LblEditDepartment;
		internal System.Windows.Forms.Label LblEditPlant;
		internal System.Windows.Forms.Label LblSearch;
		internal System.Windows.Forms.Label LblEdit;

		//textboxes
		internal System.Windows.Forms.TextBox TxtCoTelephoneNumber;
		internal System.Windows.Forms.TextBox TxtEditFirstname;
		internal System.Windows.Forms.TextBox TxtEditSurname;
		internal System.Windows.Forms.TextBox TxtEditUser;
		internal System.Windows.Forms.TextBox TxtSearchSurname;
		internal System.Windows.Forms.TextBox TxtSearchFirstname;
		internal System.Windows.Forms.TextBox TxtSearchUser;
		internal System.Windows.Forms.TextBox TxtEditPhone;

		//comboboxes
		internal System.Windows.Forms.ComboBox CboCoCraft;
		internal System.Windows.Forms.ComboBox CboEditDepartment;
		internal System.Windows.Forms.ComboBox CboEditDomain;
		internal System.Windows.Forms.ComboBox CboSearchDepartment;
		internal System.Windows.Forms.Label label1;

		//listboxes
		internal System.Windows.Forms.CheckedListBox LikEditPlant;

		//buttons
		internal System.Windows.Forms.Button BtnSearch;
		internal System.Windows.Forms.Button BtnSearchCancel;
		internal System.Windows.Forms.Button BtnBackTo;
		internal System.Windows.Forms.Button BtnDelete;
		internal System.Windows.Forms.Button BtnAssignRole;
		internal System.Windows.Forms.Button BtnSave;
		internal System.Windows.Forms.Button BtnNewUser;
		internal System.Windows.Forms.Button BtnClearFormFields;

		//tooltips
		private System.Windows.Forms.ToolTip TooSearch;
		private System.Windows.Forms.ToolTip TooCancel;
		private System.Windows.Forms.ToolTip TooNew;
		private System.Windows.Forms.ToolTip TooSave;
		private System.Windows.Forms.ToolTip TooDelete;
		private System.Windows.Forms.ToolTip TooRole;
		private System.Windows.Forms.ToolTip TooClearMask;
		private System.Windows.Forms.ToolTip ClearBackTo;
        internal DataGrid DgrUser;

        //tables
		private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
		private System.Windows.Forms.DataGridTextBoxColumn dgrTxtUserId;
		private System.Windows.Forms.DataGridTextBoxColumn dgrTxtNameTel;
        private System.Windows.Forms.DataGridTextBoxColumn dgrTxtDomain;
		private System.Windows.Forms.DataGridTextBoxColumn dgrTxtApplUserId;
		private System.Windows.Forms.DataGridTextBoxColumn dgrTxtRoleName;
		private System.Windows.Forms.DataGridTextBoxColumn dgrTxtFPASSUserId;
		private System.Windows.Forms.DataGridTextBoxColumn dgrTxtDeptName;
		/// <summary>
		/// Typified DataSet
		/// </summary>
		protected DSUser mDSUSer;
		/// <summary>
		/// Current UM? and FPASS UserIDs
		/// </summary>
		private decimal mCurrentUMUserID = -1;
		private decimal mCurrentFPASSUserID = -1;
		/// <summary>
		/// Content of form changed
		/// </summary>
		private bool mContentChanged = false;
		private bool mCheckListBoxChanged = false;
		/// <summary>
		/// Is datagrid being sorted? Don't get current IDs
		/// </summary>
		private bool mGridSorted = false;

        // Flags whether in the middle of loading user
        private bool mDuringLoadUser = true;

		private System.ComponentModel.IContainer components;

		#endregion //End of Members

		#region Constructors

		public FrmUser()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			MnuFunction.Enabled = false;
			MnuReports.Enabled = false;

			this.FillLists();
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
            this.TxtEditUser = new System.Windows.Forms.TextBox();
            this.BtnSearchCancel = new System.Windows.Forms.Button();
            this.TxtEditFirstname = new System.Windows.Forms.TextBox();
            this.TxtEditSurname = new System.Windows.Forms.TextBox();
            this.BtnSearch = new System.Windows.Forms.Button();
            this.LblEditUser = new System.Windows.Forms.Label();
            this.LblEditFirstname = new System.Windows.Forms.Label();
            this.LblEditSurname = new System.Windows.Forms.Label();
            this.PnlSearch = new System.Windows.Forms.Panel();
            this.CboSearchDepartment = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtSearchSurname = new System.Windows.Forms.TextBox();
            this.TxtSearchFirstname = new System.Windows.Forms.TextBox();
            this.TxtSearchUser = new System.Windows.Forms.TextBox();
            this.LblSearchFirstname = new System.Windows.Forms.Label();
            this.LblSearchUser = new System.Windows.Forms.Label();
            this.LblSearchSurname = new System.Windows.Forms.Label();
            this.PnlEdit = new System.Windows.Forms.Panel();
            this.LikEditPlant = new System.Windows.Forms.CheckedListBox();
            this.TxtEditPhone = new System.Windows.Forms.TextBox();
            this.LblEditPhone = new System.Windows.Forms.Label();
            this.CboEditDomain = new System.Windows.Forms.ComboBox();
            this.LblEditDomain = new System.Windows.Forms.Label();
            this.CboEditDepartment = new System.Windows.Forms.ComboBox();
            this.CboCoCraft = new System.Windows.Forms.ComboBox();
            this.LblEditDepartment = new System.Windows.Forms.Label();
            this.LblEditPlant = new System.Windows.Forms.Label();
            this.LblCoTelephoneNumber = new System.Windows.Forms.Label();
            this.LblCoCraft = new System.Windows.Forms.Label();
            this.TxtCoTelephoneNumber = new System.Windows.Forms.TextBox();
            this.BtnBackTo = new System.Windows.Forms.Button();
            this.BtnDelete = new System.Windows.Forms.Button();
            this.BtnAssignRole = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.LblSearch = new System.Windows.Forms.Label();
            this.LblEdit = new System.Windows.Forms.Label();
            this.LblMask = new System.Windows.Forms.Label();
            this.DgrUser = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dgrTxtFPASSUserId = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dgrTxtUserId = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dgrTxtApplUserId = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dgrTxtDomain = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dgrTxtNameTel = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dgrTxtDeptName = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dgrTxtRoleName = new System.Windows.Forms.DataGridTextBoxColumn();
            this.BtnNewUser = new System.Windows.Forms.Button();
            this.BtnClearFormFields = new System.Windows.Forms.Button();
            this.TooSearch = new System.Windows.Forms.ToolTip(this.components);
            this.TooCancel = new System.Windows.Forms.ToolTip(this.components);
            this.TooNew = new System.Windows.Forms.ToolTip(this.components);
            this.TooSave = new System.Windows.Forms.ToolTip(this.components);
            this.TooDelete = new System.Windows.Forms.ToolTip(this.components);
            this.TooRole = new System.Windows.Forms.ToolTip(this.components);
            this.TooClearMask = new System.Windows.Forms.ToolTip(this.components);
            this.ClearBackTo = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.SbpMessage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sbpenvironment)).BeginInit();
            this.PnlSearch.SuspendLayout();
            this.PnlEdit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgrUser)).BeginInit();
            this.SuspendLayout();
            // 
            // TxtEditUser
            // 
            this.TxtEditUser.AccessibleRole = System.Windows.Forms.AccessibleRole.PageTabList;
            this.TxtEditUser.Enabled = false;
            this.TxtEditUser.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtEditUser.Location = new System.Drawing.Point(138, 88);
            this.TxtEditUser.MaxLength = 50;
            this.TxtEditUser.Name = "TxtEditUser";
            this.TxtEditUser.Size = new System.Drawing.Size(210, 21);
            this.TxtEditUser.TabIndex = 10;
            this.TxtEditUser.Enter += new System.EventHandler(this.TxtEditUser_Enter);
            // 
            // BtnSearchCancel
            // 
            this.BtnSearchCancel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSearchCancel.Location = new System.Drawing.Point(1070, 55);
            this.BtnSearchCancel.Name = "BtnSearchCancel";
            this.BtnSearchCancel.Size = new System.Drawing.Size(145, 30);
            this.BtnSearchCancel.TabIndex = 5;
            this.BtnSearchCancel.Text = "&Abbrechen";
            this.TooCancel.SetToolTip(this.BtnSearchCancel, "Löscht alle bereits eingegebenen Suchkriterien");
            this.BtnSearchCancel.Click += new System.EventHandler(this.BtnSearchCancel_Click);
            // 
            // TxtEditFirstname
            // 
            this.TxtEditFirstname.Enabled = false;
            this.TxtEditFirstname.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtEditFirstname.Location = new System.Drawing.Point(138, 56);
            this.TxtEditFirstname.MaxLength = 50;
            this.TxtEditFirstname.Name = "TxtEditFirstname";
            this.TxtEditFirstname.Size = new System.Drawing.Size(210, 21);
            this.TxtEditFirstname.TabIndex = 9;
            this.TxtEditFirstname.Enter += new System.EventHandler(this.TxtEditFirstname_Enter);
            // 
            // TxtEditSurname
            // 
            this.TxtEditSurname.Enabled = false;
            this.TxtEditSurname.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtEditSurname.Location = new System.Drawing.Point(138, 24);
            this.TxtEditSurname.MaxLength = 50;
            this.TxtEditSurname.Name = "TxtEditSurname";
            this.TxtEditSurname.Size = new System.Drawing.Size(210, 21);
            this.TxtEditSurname.TabIndex = 8;
            this.TxtEditSurname.Enter += new System.EventHandler(this.TxtEditSurname_Enter);
            // 
            // BtnSearch
            // 
            this.BtnSearch.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSearch.Location = new System.Drawing.Point(1070, 15);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(145, 30);
            this.BtnSearch.TabIndex = 4;
            this.BtnSearch.Text = "&Suchen";
            this.TooSearch.SetToolTip(this.BtnSearch, "Löst den Suchvorgang aus");
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // LblEditUser
            // 
            this.LblEditUser.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblEditUser.Location = new System.Drawing.Point(45, 90);
            this.LblEditUser.Name = "LblEditUser";
            this.LblEditUser.Size = new System.Drawing.Size(88, 21);
            this.LblEditUser.TabIndex = 49;
            this.LblEditUser.Text = "User";
            // 
            // LblEditFirstname
            // 
            this.LblEditFirstname.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblEditFirstname.Location = new System.Drawing.Point(45, 58);
            this.LblEditFirstname.Name = "LblEditFirstname";
            this.LblEditFirstname.Size = new System.Drawing.Size(104, 21);
            this.LblEditFirstname.TabIndex = 48;
            this.LblEditFirstname.Text = "Vorname";
            // 
            // LblEditSurname
            // 
            this.LblEditSurname.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblEditSurname.Location = new System.Drawing.Point(45, 26);
            this.LblEditSurname.Name = "LblEditSurname";
            this.LblEditSurname.Size = new System.Drawing.Size(74, 21);
            this.LblEditSurname.TabIndex = 47;
            this.LblEditSurname.Text = "Nachname";
            // 
            // PnlSearch
            // 
            this.PnlSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlSearch.Controls.Add(this.CboSearchDepartment);
            this.PnlSearch.Controls.Add(this.label1);
            this.PnlSearch.Controls.Add(this.TxtSearchSurname);
            this.PnlSearch.Controls.Add(this.TxtSearchFirstname);
            this.PnlSearch.Controls.Add(this.TxtSearchUser);
            this.PnlSearch.Controls.Add(this.LblSearchFirstname);
            this.PnlSearch.Controls.Add(this.LblSearchUser);
            this.PnlSearch.Controls.Add(this.LblSearchSurname);
            this.PnlSearch.Controls.Add(this.BtnSearchCancel);
            this.PnlSearch.Controls.Add(this.BtnSearch);
            this.PnlSearch.Location = new System.Drawing.Point(14, 56);
            this.PnlSearch.Name = "PnlSearch";
            this.PnlSearch.Size = new System.Drawing.Size(1238, 104);
            this.PnlSearch.TabIndex = 0;
            // 
            // CboSearchDepartment
            // 
            this.CboSearchDepartment.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboSearchDepartment.Location = new System.Drawing.Point(456, 55);
            this.CboSearchDepartment.Name = "CboSearchDepartment";
            this.CboSearchDepartment.Size = new System.Drawing.Size(264, 23);
            this.CboSearchDepartment.TabIndex = 56;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(388, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 21);
            this.label1.TabIndex = 57;
            this.label1.Text = "Abteilung";
            // 
            // TxtSearchSurname
            // 
            this.TxtSearchSurname.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSearchSurname.Location = new System.Drawing.Point(112, 23);
            this.TxtSearchSurname.MaxLength = 50;
            this.TxtSearchSurname.Name = "TxtSearchSurname";
            this.TxtSearchSurname.Size = new System.Drawing.Size(210, 21);
            this.TxtSearchSurname.TabIndex = 1;
            // 
            // TxtSearchFirstname
            // 
            this.TxtSearchFirstname.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSearchFirstname.Location = new System.Drawing.Point(456, 23);
            this.TxtSearchFirstname.MaxLength = 50;
            this.TxtSearchFirstname.Name = "TxtSearchFirstname";
            this.TxtSearchFirstname.Size = new System.Drawing.Size(210, 21);
            this.TxtSearchFirstname.TabIndex = 2;
            // 
            // TxtSearchUser
            // 
            this.TxtSearchUser.AccessibleRole = System.Windows.Forms.AccessibleRole.PageTabList;
            this.TxtSearchUser.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSearchUser.Location = new System.Drawing.Point(112, 55);
            this.TxtSearchUser.MaxLength = 30;
            this.TxtSearchUser.Name = "TxtSearchUser";
            this.TxtSearchUser.Size = new System.Drawing.Size(210, 21);
            this.TxtSearchUser.TabIndex = 3;
            // 
            // LblSearchFirstname
            // 
            this.LblSearchFirstname.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSearchFirstname.Location = new System.Drawing.Point(388, 24);
            this.LblSearchFirstname.Name = "LblSearchFirstname";
            this.LblSearchFirstname.Size = new System.Drawing.Size(104, 21);
            this.LblSearchFirstname.TabIndex = 54;
            this.LblSearchFirstname.Text = "Vorname";
            // 
            // LblSearchUser
            // 
            this.LblSearchUser.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSearchUser.Location = new System.Drawing.Point(32, 56);
            this.LblSearchUser.Name = "LblSearchUser";
            this.LblSearchUser.Size = new System.Drawing.Size(48, 21);
            this.LblSearchUser.TabIndex = 55;
            this.LblSearchUser.Text = "User";
            // 
            // LblSearchSurname
            // 
            this.LblSearchSurname.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSearchSurname.Location = new System.Drawing.Point(32, 24);
            this.LblSearchSurname.Name = "LblSearchSurname";
            this.LblSearchSurname.Size = new System.Drawing.Size(74, 21);
            this.LblSearchSurname.TabIndex = 53;
            this.LblSearchSurname.Text = "Nachname";
            // 
            // PnlEdit
            // 
            this.PnlEdit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlEdit.Controls.Add(this.LikEditPlant);
            this.PnlEdit.Controls.Add(this.TxtEditPhone);
            this.PnlEdit.Controls.Add(this.LblEditPhone);
            this.PnlEdit.Controls.Add(this.CboEditDomain);
            this.PnlEdit.Controls.Add(this.LblEditDomain);
            this.PnlEdit.Controls.Add(this.CboEditDepartment);
            this.PnlEdit.Controls.Add(this.TxtEditSurname);
            this.PnlEdit.Controls.Add(this.TxtEditFirstname);
            this.PnlEdit.Controls.Add(this.TxtEditUser);
            this.PnlEdit.Controls.Add(this.CboCoCraft);
            this.PnlEdit.Controls.Add(this.LblEditDepartment);
            this.PnlEdit.Controls.Add(this.LblEditPlant);
            this.PnlEdit.Controls.Add(this.LblCoTelephoneNumber);
            this.PnlEdit.Controls.Add(this.LblCoCraft);
            this.PnlEdit.Controls.Add(this.TxtCoTelephoneNumber);
            this.PnlEdit.Controls.Add(this.LblEditFirstname);
            this.PnlEdit.Controls.Add(this.LblEditUser);
            this.PnlEdit.Controls.Add(this.LblEditSurname);
            this.PnlEdit.Location = new System.Drawing.Point(14, 683);
            this.PnlEdit.Name = "PnlEdit";
            this.PnlEdit.Size = new System.Drawing.Size(1236, 128);
            this.PnlEdit.TabIndex = 7;
            // 
            // LikEditPlant
            // 
            this.LikEditPlant.CheckOnClick = true;
            this.LikEditPlant.Enabled = false;
            this.LikEditPlant.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LikEditPlant.Location = new System.Drawing.Point(957, 25);
            this.LikEditPlant.Name = "LikEditPlant";
            this.LikEditPlant.Size = new System.Drawing.Size(225, 84);
            this.LikEditPlant.Sorted = true;
            this.LikEditPlant.TabIndex = 14;
            this.LikEditPlant.ThreeDCheckBoxes = true;
            this.LikEditPlant.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.LikEditPlant_ItemCheck);
            // 
            // TxtEditPhone
            // 
            this.TxtEditPhone.Enabled = false;
            this.TxtEditPhone.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtEditPhone.Location = new System.Drawing.Point(517, 54);
            this.TxtEditPhone.MaxLength = 30;
            this.TxtEditPhone.Name = "TxtEditPhone";
            this.TxtEditPhone.Size = new System.Drawing.Size(210, 21);
            this.TxtEditPhone.TabIndex = 12;
            this.TxtEditPhone.Enter += new System.EventHandler(this.TxtEditPhone_Enter);
            // 
            // LblEditPhone
            // 
            this.LblEditPhone.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblEditPhone.Location = new System.Drawing.Point(438, 56);
            this.LblEditPhone.Name = "LblEditPhone";
            this.LblEditPhone.Size = new System.Drawing.Size(104, 21);
            this.LblEditPhone.TabIndex = 53;
            this.LblEditPhone.Text = "Telefon";
            // 
            // CboEditDomain
            // 
            this.CboEditDomain.Enabled = false;
            this.CboEditDomain.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboEditDomain.Location = new System.Drawing.Point(517, 86);
            this.CboEditDomain.Name = "CboEditDomain";
            this.CboEditDomain.Size = new System.Drawing.Size(264, 23);
            this.CboEditDomain.TabIndex = 13;
            this.CboEditDomain.Enter += new System.EventHandler(this.CboEditDomain_Enter);
            // 
            // LblEditDomain
            // 
            this.LblEditDomain.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblEditDomain.Location = new System.Drawing.Point(438, 88);
            this.LblEditDomain.Name = "LblEditDomain";
            this.LblEditDomain.Size = new System.Drawing.Size(96, 21);
            this.LblEditDomain.TabIndex = 51;
            this.LblEditDomain.Text = "Domäne";
            // 
            // CboEditDepartment
            // 
            this.CboEditDepartment.Enabled = false;
            this.CboEditDepartment.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboEditDepartment.Location = new System.Drawing.Point(517, 22);
            this.CboEditDepartment.Name = "CboEditDepartment";
            this.CboEditDepartment.Size = new System.Drawing.Size(264, 23);
            this.CboEditDepartment.TabIndex = 11;
            this.CboEditDepartment.Enter += new System.EventHandler(this.CboEditDepartment_Enter);
            // 
            // CboCoCraft
            // 
            this.CboCoCraft.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboCoCraft.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboCoCraft.Location = new System.Drawing.Point(445, -81);
            this.CboCoCraft.MaxLength = 30;
            this.CboCoCraft.Name = "CboCoCraft";
            this.CboCoCraft.Size = new System.Drawing.Size(180, 23);
            this.CboCoCraft.Sorted = true;
            this.CboCoCraft.TabIndex = 40;
            // 
            // LblEditDepartment
            // 
            this.LblEditDepartment.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblEditDepartment.Location = new System.Drawing.Point(438, 24);
            this.LblEditDepartment.Name = "LblEditDepartment";
            this.LblEditDepartment.Size = new System.Drawing.Size(96, 21);
            this.LblEditDepartment.TabIndex = 38;
            this.LblEditDepartment.Text = "Abteilung";
            // 
            // LblEditPlant
            // 
            this.LblEditPlant.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblEditPlant.Location = new System.Drawing.Point(861, 27);
            this.LblEditPlant.Name = "LblEditPlant";
            this.LblEditPlant.Size = new System.Drawing.Size(88, 40);
            this.LblEditPlant.TabIndex = 37;
            this.LblEditPlant.Text = "Ist Meister für Betrieb";
            // 
            // LblCoTelephoneNumber
            // 
            this.LblCoTelephoneNumber.Location = new System.Drawing.Point(341, -47);
            this.LblCoTelephoneNumber.Name = "LblCoTelephoneNumber";
            this.LblCoTelephoneNumber.Size = new System.Drawing.Size(96, 47);
            this.LblCoTelephoneNumber.TabIndex = 36;
            this.LblCoTelephoneNumber.Text = "Telefon-Nr. Koordinator";
            // 
            // LblCoCraft
            // 
            this.LblCoCraft.Location = new System.Drawing.Point(344, -79);
            this.LblCoCraft.Name = "LblCoCraft";
            this.LblCoCraft.Size = new System.Drawing.Size(96, 79);
            this.LblCoCraft.TabIndex = 35;
            this.LblCoCraft.Text = "Gewerk";
            // 
            // TxtCoTelephoneNumber
            // 
            this.TxtCoTelephoneNumber.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCoTelephoneNumber.Location = new System.Drawing.Point(445, -41);
            this.TxtCoTelephoneNumber.MaxLength = 30;
            this.TxtCoTelephoneNumber.Name = "TxtCoTelephoneNumber";
            this.TxtCoTelephoneNumber.Size = new System.Drawing.Size(180, 21);
            this.TxtCoTelephoneNumber.TabIndex = 33;
            // 
            // BtnBackTo
            // 
            this.BtnBackTo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBackTo.Location = new System.Drawing.Point(1130, 852);
            this.BtnBackTo.Name = "BtnBackTo";
            this.BtnBackTo.Size = new System.Drawing.Size(120, 30);
            this.BtnBackTo.TabIndex = 20;
            this.BtnBackTo.Text = "&Zurück";
            this.ClearBackTo.SetToolTip(this.BtnBackTo, "Zurück zur Übersichtsmaske");
            this.BtnBackTo.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelete.Location = new System.Drawing.Point(721, 852);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(120, 30);
            this.BtnDelete.TabIndex = 17;
            this.BtnDelete.Text = "User &löschen";
            this.TooDelete.SetToolTip(this.BtnDelete, "Löscht den markierten Datensatz");
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnAssignRole
            // 
            this.BtnAssignRole.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAssignRole.Location = new System.Drawing.Point(857, 852);
            this.BtnAssignRole.Name = "BtnAssignRole";
            this.BtnAssignRole.Size = new System.Drawing.Size(120, 30);
            this.BtnAssignRole.TabIndex = 18;
            this.BtnAssignRole.Tag = "";
            this.BtnAssignRole.Text = "R&ollen zuordnen";
            this.TooRole.SetToolTip(this.BtnAssignRole, "Öffnet die Maske Zuordnung User zu Rolle");
            this.BtnAssignRole.Click += new System.EventHandler(this.BtnAssignRole_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Location = new System.Drawing.Point(585, 852);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(120, 30);
            this.BtnSave.TabIndex = 16;
            this.BtnSave.Text = "Use&r speichern";
            this.TooSave.SetToolTip(this.BtnSave, "Speichert den markierten Datensatz");
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click_1);
            // 
            // LblSearch
            // 
            this.LblSearch.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSearch.Location = new System.Drawing.Point(40, 48);
            this.LblSearch.Name = "LblSearch";
            this.LblSearch.Size = new System.Drawing.Size(48, 16);
            this.LblSearch.TabIndex = 79;
            this.LblSearch.Text = " Suche";
            // 
            // LblEdit
            // 
            this.LblEdit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblEdit.Location = new System.Drawing.Point(22, 676);
            this.LblEdit.Name = "LblEdit";
            this.LblEdit.Size = new System.Drawing.Size(56, 16);
            this.LblEdit.TabIndex = 80;
            this.LblEdit.Text = "Eingabe";
            // 
            // LblMask
            // 
            this.LblMask.BackColor = System.Drawing.Color.White;
            this.LblMask.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMask.Location = new System.Drawing.Point(414, 8);
            this.LblMask.Name = "LblMask";
            this.LblMask.Size = new System.Drawing.Size(400, 32);
            this.LblMask.TabIndex = 81;
            this.LblMask.Text = "FPASS - Benutzerverwaltung";
            // 
            // DgrUser
            // 
            this.DgrUser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DgrUser.CaptionBackColor = System.Drawing.Color.SteelBlue;
            this.DgrUser.CaptionFont = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DgrUser.CaptionText = "Benutzer";
            this.DgrUser.DataMember = "";
            this.DgrUser.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.DgrUser.Location = new System.Drawing.Point(12, 178);
            this.DgrUser.Name = "DgrUser";
            this.DgrUser.ReadOnly = true;
            this.DgrUser.Size = new System.Drawing.Size(1246, 489);
            this.DgrUser.TabIndex = 6;
            this.DgrUser.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.dataGridTableStyle1});
            this.DgrUser.CurrentCellChanged += new System.EventHandler(this.DgrUser_CurrentCellChanged);
            this.DgrUser.Enter += new System.EventHandler(this.DgrUser_Enter);
            this.DgrUser.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DgrUser_MouseDown);
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.DataGrid = this.DgrUser;
            this.dataGridTableStyle1.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.dgrTxtFPASSUserId,
            this.dgrTxtUserId,
            this.dgrTxtApplUserId,
            this.dgrTxtDomain,
            this.dgrTxtNameTel,
            this.dgrTxtDeptName,
            this.dgrTxtRoleName});
            this.dataGridTableStyle1.HeaderFont = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGridTableStyle1.MappingName = "RTTabUsers";
            this.dataGridTableStyle1.PreferredColumnWidth = 242;
            this.dataGridTableStyle1.ReadOnly = true;
            // 
            // dgrTxtFPASSUserId
            // 
            this.dgrTxtFPASSUserId.Format = "";
            this.dgrTxtFPASSUserId.FormatInfo = null;
            this.dgrTxtFPASSUserId.HeaderText = "FPASSUSERID";
            this.dgrTxtFPASSUserId.MappingName = "FPASSPKIdentifier";
            this.dgrTxtFPASSUserId.Width = 0;
            // 
            // dgrTxtUserId
            // 
            this.dgrTxtUserId.Format = "";
            this.dgrTxtUserId.FormatInfo = null;
            this.dgrTxtUserId.HeaderText = "FPASS_USER_ID";
            this.dgrTxtUserId.MappingName = "UMPKIdentifier";
            this.dgrTxtUserId.Width = 0;
            // 
            // dgrTxtApplUserId
            // 
            this.dgrTxtApplUserId.Format = "";
            this.dgrTxtApplUserId.FormatInfo = null;
            this.dgrTxtApplUserId.HeaderText = "UserName";
            this.dgrTxtApplUserId.MappingName = "ApplUserID";
            this.dgrTxtApplUserId.Width = 220;
            // 
            // dgrTxtDomain
            // 
            this.dgrTxtDomain.Format = "";
            this.dgrTxtDomain.FormatInfo = null;
            this.dgrTxtDomain.HeaderText = "Domäne";
            this.dgrTxtDomain.MappingName = "DomainName";
            this.dgrTxtDomain.Width = 220;
            // 
            // dgrTxtNameTel
            // 
            this.dgrTxtNameTel.Format = "";
            this.dgrTxtNameTel.FormatInfo = null;
            this.dgrTxtNameTel.HeaderText = "Name und Telefonnummer";
            this.dgrTxtNameTel.MappingName = "UserFormattedName";
            this.dgrTxtNameTel.Width = 280;
            // 
            // dgrTxtDeptName
            // 
            this.dgrTxtDeptName.Format = "";
            this.dgrTxtDeptName.FormatInfo = null;
            this.dgrTxtDeptName.HeaderText = "Abteilung";
            this.dgrTxtDeptName.MappingName = "DeptName";
            this.dgrTxtDeptName.Width = 252;
            // 
            // dgrTxtRoleName
            // 
            this.dgrTxtRoleName.Format = "";
            this.dgrTxtRoleName.FormatInfo = null;
            this.dgrTxtRoleName.HeaderText = "Höchste zugeordnete Rolle";
            this.dgrTxtRoleName.MappingName = "RoleNameAssigned";
            this.dgrTxtRoleName.Width = 262;
            // 
            // BtnNewUser
            // 
            this.BtnNewUser.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnNewUser.Location = new System.Drawing.Point(449, 852);
            this.BtnNewUser.Name = "BtnNewUser";
            this.BtnNewUser.Size = new System.Drawing.Size(120, 30);
            this.BtnNewUser.TabIndex = 15;
            this.BtnNewUser.Text = "&Neuer User";
            this.TooNew.SetToolTip(this.BtnNewUser, "Legt einen neuen Datensatz an");
            this.BtnNewUser.Click += new System.EventHandler(this.BtnNewUser_Click);
            // 
            // BtnClearFormFields
            // 
            this.BtnClearFormFields.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClearFormFields.Location = new System.Drawing.Point(993, 852);
            this.BtnClearFormFields.Name = "BtnClearFormFields";
            this.BtnClearFormFields.Size = new System.Drawing.Size(120, 30);
            this.BtnClearFormFields.TabIndex = 19;
            this.BtnClearFormFields.Tag = "";
            this.BtnClearFormFields.Text = "&Maske leeren";
            this.TooClearMask.SetToolTip(this.BtnClearFormFields, "Verwirft alle bereits eingegebenen Daten");
            this.BtnClearFormFields.Click += new System.EventHandler(this.BtnClearFormFields_Click);
            // 
            // FrmUser
            // 
            this.AcceptButton = this.BtnSearch;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(1270, 971);
            this.Controls.Add(this.DgrUser);
            this.Controls.Add(this.BtnClearFormFields);
            this.Controls.Add(this.BtnNewUser);
            this.Controls.Add(this.LblMask);
            this.Controls.Add(this.LblSearch);
            this.Controls.Add(this.LblEdit);
            this.Controls.Add(this.BtnBackTo);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.BtnDelete);
            this.Controls.Add(this.BtnAssignRole);
            this.Controls.Add(this.PnlSearch);
            this.Controls.Add(this.PnlEdit);
            this.Name = "FrmUser";
            this.Text = "Benutzerverwaltung - User bearbeiten";
            this.Controls.SetChildIndex(this.PnlEdit, 0);
            this.Controls.SetChildIndex(this.PnlSearch, 0);
            this.Controls.SetChildIndex(this.LblBaseHead, 0);
            this.Controls.SetChildIndex(this.BtnAssignRole, 0);
            this.Controls.SetChildIndex(this.BtnDelete, 0);
            this.Controls.SetChildIndex(this.StbBase, 0);
            this.Controls.SetChildIndex(this.BtnSave, 0);
            this.Controls.SetChildIndex(this.BtnBackTo, 0);
            this.Controls.SetChildIndex(this.LblEdit, 0);
            this.Controls.SetChildIndex(this.LblSearch, 0);
            this.Controls.SetChildIndex(this.LblMask, 0);
            this.Controls.SetChildIndex(this.BtnNewUser, 0);
            this.Controls.SetChildIndex(this.BtnClearFormFields, 0);
            this.Controls.SetChildIndex(this.DgrUser, 0);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.SbpMessage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sbpenvironment)).EndInit();
            this.PnlSearch.ResumeLayout(false);
            this.PnlSearch.PerformLayout();
            this.PnlEdit.ResumeLayout(false);
            this.PnlEdit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgrUser)).EndInit();
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

		public decimal CurrentUMUserID
		{
			get 
			{
				return mCurrentUMUserID;
			}
			set 
			{
				mCurrentUMUserID = value;
			}
		}

		public decimal CurrentFPASSUserID
		{
			get 
			{
				return mCurrentFPASSUserID;
			}
			set 
			{
				mCurrentFPASSUserID = value;
			}
		}

		public bool ContentChanged
		{
			get 
			{
				return mContentChanged;
			}
			set 
			{
				mContentChanged = value;
			}
		}

		public bool CheckListBoxChanged
		{
			get 
			{
				return mCheckListBoxChanged;
			}
			set 
			{
				mCheckListBoxChanged = value;
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
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		/// <summary>
		/// not used 20.11.2003
		/// </summary>
		private  void SetAuthorization() 
		{

		}

		/// <summary>
		/// Fill comboboxes.
		/// These appear at foot of form and are used to assign vals to user
		/// </summary>
		internal override void FillLists()
		{
			FillDepartmentsSearch();
			FillDepartmentsAssign();
			FillDomaines();
		}

		/// <summary>
		/// Clear textbox in foot of form
		/// </summary>
		internal void ClearFields()
		{
			this.TxtEditSurname.DataBindings.Clear();
			this.TxtEditFirstname.DataBindings.Clear();
			this.TxtEditUser.DataBindings.Clear();
			this.TxtEditPhone.DataBindings.Clear();
			this.LikEditPlant.Items.Clear();	

			TxtEditSurname.Text    = "";
			TxtEditFirstname.Text  = "";
			TxtEditUser.Text	   = "";
			TxtEditPhone.Text      = "";
			CboEditDepartment.Text = "";
			CboEditDomain.Text     = "";
			CboCoCraft.SelectedItem = "";
			TxtEditSurname.Enabled    = false;
			TxtEditFirstname.Enabled  = false;
			TxtEditUser.Enabled       = false;
			TxtEditPhone.Enabled      = false;
			CboEditDepartment.Enabled = false;
			CboEditDomain.Enabled     = false;
			CboCoCraft.Enabled        = false;					
			LikEditPlant.Enabled      = false;
						
			mCurrentFPASSUserID = -1;
			mCurrentUMUserID	= -1;  
			mContentChanged		= false;
		}

		/// <summary>
		/// Combobox Department in foot of Form:
		/// Search for useres belonging to specific dept
		/// </summary>
		private void FillDepartmentsSearch()
		{
			ArrayList arrDeptsSearch = new ArrayList(); 
			arrDeptsSearch = LovSingleton.GetInstance().GetRootList(null, "FPASS_DEPARTMENT", "DEPT_DEPARTMENT");
			arrDeptsSearch.Reverse();
			arrDeptsSearch.Add(new LovItem("0", ""));
			arrDeptsSearch.Reverse();
			this.CboSearchDepartment.DataSource = arrDeptsSearch;
			this.CboSearchDepartment.DisplayMember = "ItemValue";
			this.CboSearchDepartment.ValueMember = "DecId";
		}

		/// <summary>
		/// Combobox Department in top of Form:
		/// assign dept to current user
		/// </summary>
		private void FillDepartmentsAssign()
		{
			ArrayList arrDeptsAssign = new ArrayList(); 
			arrDeptsAssign = LovSingleton.GetInstance().GetRootList(null, "FPASS_DEPARTMENT", "DEPT_DEPARTMENT");
			arrDeptsAssign.Reverse();
			arrDeptsAssign.Add(new LovItem("0", ""));
			arrDeptsAssign.Reverse();
			this.CboEditDepartment.DataSource = arrDeptsAssign;
			this.CboEditDepartment.DisplayMember = "ItemValue";
			this.CboEditDepartment.ValueMember = "DecId";
		}

		/// <summary>
		/// Combobox Domain
		/// </summary>
		private void FillDomaines()
		{
			ArrayList arrDomaines = new ArrayList(); 
			arrDomaines = LovSingleton.GetInstance().GetRootList(null, "FPASS_USERDOMAINE", "UDOM_NAME");
			arrDomaines.Reverse();
			arrDomaines.Add(new LovItem("0", ""));
			arrDomaines.Reverse();
			this.CboEditDomain.DataSource = arrDomaines;
			this.CboEditDomain.DisplayMember = "ItemValue";
			this.CboEditDomain.ValueMember = "DecId";
		}

		/// <summary>
		/// Get UM User and FPASS User PK IDs for currently selected record
		/// </summary>
		private void TableNavigated()
		{
            mDuringLoadUser = true;
			int rowIndex = this.DgrUser.CurrentRowIndex;

			if(-1 < rowIndex)
			{				
				mCurrentFPASSUserID = Convert.ToInt32(this.DgrUser[rowIndex, 0].ToString());
				mCurrentUMUserID    = Convert.ToInt32(this.DgrUser[rowIndex, 1].ToString());
			}
			((UserController)mController).HandleEventTableNavigated();
            mDuringLoadUser = false;
		}

		/// <summary>
		/// not fired 12.12.03 - shouldnt be used as not allowed to create new instance: use triad
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button3_Click(object sender, System.EventArgs e)
		{
//			FrmUserToRole fm = new FrmUserToRole();
//			fm.ShowDialog();
		}

		#endregion // End of Methods

		#region Events


		/// <summary>
		/// Fires when moving between cells in datagrid: 
		/// get PK vals UMUser and FPASSUser and load current record (UserBO)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DgrUser_CurrentCellChanged(object sender, System.EventArgs e)
		{
			if ( !mGridSorted )
			{
				if ( this.DgrUser.DataSource != null &&  this.DgrUser.VisibleRowCount > 1 )
				{
					this.TableNavigated();
				}
			}
		}

		/// <summary>
		/// Fires when datagrid entered: get PK vals UMUser and FPASSUser and load current record (UserBO)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DgrUser_Enter(object sender, System.EventArgs e)
		{
			if ( this.DgrUser.DataSource != null && this.DgrUser.VisibleRowCount > 0 )
			{
				this.TableNavigated();
			}
		}

		/// <summary>
		/// Event fires when column header clicked, i.e. when grid is sorted.
		/// Discard currently selected record, user has to re-click to select 
		/// Put pointer on first row (index 0)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DgrUser_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			mGridSorted = true;
			if ( this.DgrUser.DataSource != null && this.DgrUser.VisibleRowCount > 1 )
			{
				DataGrid.HitTestInfo hti  = DgrUser.HitTest(e.X, e.Y);
				DataGrid.HitTestType type = hti.Type;
				switch(type)       
				{         
					case System.Windows.Forms.DataGrid.HitTestType.ColumnHeader:
						DgrUser.CurrentRowIndex = 0;
						ClearFields();
						break;				
				}
			}
			mGridSorted = false;
		}

		/// <summary>
		/// Event for Button New
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnNewUser_Click(object sender, System.EventArgs e)
		{
			((UserController)mController).HandleEventBtnNewClick();
            mDuringLoadUser = false;
		}


		/// <summary>
		/// Button "BackTo" ("Zurück" ): used to be called Cancel: close the form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnCancel_Click(object sender, System.EventArgs e)
		{
			((UserController)mController).HandleEventBtnBackClick();
            mDuringLoadUser = true;
		}

		/// <summary>
		/// Event for click on button "Search"
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnSearch_Click(object sender, System.EventArgs e)
		{
			((UserController)mController).HandleEventBtnSearchClick();
            mDuringLoadUser = true;
		}

		/// <summary>
		/// Event for button "Save"
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnSave_Click_1(object sender, System.EventArgs e)
		{
			((UserController)mController).HandleEventBtnSaveClick();
            mDuringLoadUser = true;
		}

		/// <summary>
		/// Button "Abbrechen" at top of form (Search): empty the search fields
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnSearchCancel_Click(object sender, System.EventArgs e)
		{
			((UserController)mController).HandleEventBtnSearchCancel();
		}

		/// <summary>
		/// Button "Maske leeren": empty fields for User Edit
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnClearFormFields_Click(object sender, System.EventArgs e)
		{
			((UserController)mController).HandleEventBtnClearFields();
		}


		/// <summary>
		/// Open form to show assigned roles for current user
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnAssignRole_Click(object sender, System.EventArgs e)
		{
			((UserController)mController).HandleEventOpenToUserRoleDialog();
		}

		/// <summary>
		/// Delete - Button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnDelete_Click(object sender, System.EventArgs e)
		{
			((UserController)mController).HandleEventBtnDeleteClick();
		}

		/// <summary>
		/// Fires if entries in checklistbox "Plant" are ticked or unticked: flag if assigned plants are altered
		/// Also fires when existing user is being loaded: in this case no reaction
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LikEditPlant_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
		{
            if (!mDuringLoadUser)
            {
                ((UserController)mController).HandleEventPlantItemChecked();
            }
		}

		/// <summary>
		/// If user enters textfields (i.e. to edit data), assume that contents of form have changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TxtEditSurname_Enter(object sender, System.EventArgs e)
		{
			this.mContentChanged = true;
		}

		private void TxtEditFirstname_Enter(object sender, System.EventArgs e)
		{
			this.mContentChanged = true;
		}

		private void TxtEditUser_Enter(object sender, System.EventArgs e)
		{
			this.mContentChanged = true;
		}

		private void CboEditDepartment_Enter(object sender, System.EventArgs e)
		{
			this.mContentChanged = true;
		}

		private void TxtEditPhone_Enter(object sender, System.EventArgs e)
		{
			this.mContentChanged = true;
		}

		private void CboEditDomain_Enter(object sender, System.EventArgs e)
		{
			this.mContentChanged = true;
		}


		#endregion // End of Methods

		
	}
}

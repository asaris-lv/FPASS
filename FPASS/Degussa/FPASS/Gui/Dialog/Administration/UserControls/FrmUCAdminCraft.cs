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

using de.pta.Component.ListOfValues;
using de.pta.Component.Common;

using Degussa.FPASS.Db.DataSets;
using Degussa.FPASS.Util.Exceptions;
using Degussa.FPASS.Util.UserManagement;


namespace Degussa.FPASS.Gui.Dialog.Administration.UserControls
{
	/// <summary>
	/// A FrmUCAdminCraft is one of the usercontrols 
	/// of the FrmAdministration.
	/// FrmUCAdminCraft extends from the System.Windows.Forms.UserControl.
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
	public class FrmUCAdminCraft : System.Windows.Forms.UserControl
	{

		#region Members

		//panels
		internal System.Windows.Forms.Panel PnlSearch;
		internal System.Windows.Forms.Panel PnlEdit;

		//labels
		internal System.Windows.Forms.Label LblSearch;
		internal System.Windows.Forms.Label LblSearchCraftNumber;
		internal System.Windows.Forms.Label LblSearchCraft;
		internal System.Windows.Forms.Label LblEdit;
		internal System.Windows.Forms.Label LblEditCraftNumber;
		internal System.Windows.Forms.Label LblEditCraft;

		//textboxes
		internal System.Windows.Forms.TextBox TxtEditCraftNumber;
		internal System.Windows.Forms.TextBox TxtEditCraft;

		//comboboxes
		internal System.Windows.Forms.ComboBox CboSearchCraftNumber;
		internal System.Windows.Forms.ComboBox CboSearchCraft;

		//buttons
		internal System.Windows.Forms.Button BtnSearch;
		internal System.Windows.Forms.Button BtnCancel;
		internal System.Windows.Forms.Button BtnBackTo;
		internal System.Windows.Forms.Button BtnDelete;
		internal System.Windows.Forms.Button BtnSave;
		internal System.Windows.Forms.Button BtnNew;

		//tooltips
		private System.Windows.Forms.ToolTip TooSearch;
		private System.Windows.Forms.ToolTip TooNew;
		private System.Windows.Forms.ToolTip TooSave;
		private System.Windows.Forms.ToolTip TooDelete;
		private System.Windows.Forms.ToolTip TooCancel;
		private System.Windows.Forms.ToolTip TooBackTo;

		//tables
		internal System.Windows.Forms.DataGrid DgrCraft;
		private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn2;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn3;

		/// <summary>
		/// typified Craft Dataset object
		/// </summary>
		protected DSCraft mDSCraft;
		private System.ComponentModel.IContainer components;

		/// <summary>
		/// holds the id of the current craft record selected in the displayed table
		/// </summary>
		private int mCurrentAdminRec = -1;
		/// <summary>
		/// Have data on form changed? This has an accessor and is checked in <see cref="AdministrationModel"/> AdministrationModel
		/// </summary>
		private bool mContentChanged = false;
		/// <summary>
		/// used to hold the Controller of this triad
		/// </summary>
		internal	AbstractController	mController;
		/// <summary>
		/// Used to determine if grid being sorted, don't fire event CellChanged
		/// </summary>
		private bool mGridIsSorted = false;

		#endregion //End of Members
		
		#region Constructors
		public FrmUCAdminCraft()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

		//	InitView();

		}

		private void InitView() 
		{
			
		}

		#endregion // End of Constructors

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.LblSearch = new System.Windows.Forms.Label();
			this.PnlSearch = new System.Windows.Forms.Panel();
			this.CboSearchCraftNumber = new System.Windows.Forms.ComboBox();
			this.LblSearchCraftNumber = new System.Windows.Forms.Label();
			this.BtnSearch = new System.Windows.Forms.Button();
			this.CboSearchCraft = new System.Windows.Forms.ComboBox();
			this.LblSearchCraft = new System.Windows.Forms.Label();
			this.DgrCraft = new System.Windows.Forms.DataGrid();
			this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
			this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataGridTextBoxColumn3 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.LblEdit = new System.Windows.Forms.Label();
			this.PnlEdit = new System.Windows.Forms.Panel();
			this.LblEditCraftNumber = new System.Windows.Forms.Label();
			this.TxtEditCraftNumber = new System.Windows.Forms.TextBox();
			this.TxtEditCraft = new System.Windows.Forms.TextBox();
			this.LblEditCraft = new System.Windows.Forms.Label();
			this.BtnCancel = new System.Windows.Forms.Button();
			this.BtnBackTo = new System.Windows.Forms.Button();
			this.BtnDelete = new System.Windows.Forms.Button();
			this.BtnSave = new System.Windows.Forms.Button();
			this.BtnNew = new System.Windows.Forms.Button();
			this.TooSearch = new System.Windows.Forms.ToolTip(this.components);
			this.TooNew = new System.Windows.Forms.ToolTip(this.components);
			this.TooSave = new System.Windows.Forms.ToolTip(this.components);
			this.TooDelete = new System.Windows.Forms.ToolTip(this.components);
			this.TooCancel = new System.Windows.Forms.ToolTip(this.components);
			this.TooBackTo = new System.Windows.Forms.ToolTip(this.components);
			this.PnlSearch.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DgrCraft)).BeginInit();
			this.PnlEdit.SuspendLayout();
			this.SuspendLayout();
			// 
			// LblSearch
			// 
			this.LblSearch.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
			this.LblSearch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.LblSearch.Location = new System.Drawing.Point(48, 11);
			this.LblSearch.Name = "LblSearch";
			this.LblSearch.Size = new System.Drawing.Size(48, 16);
			this.LblSearch.TabIndex = 41;
			this.LblSearch.Text = "Suche";
			// 
			// PnlSearch
			// 
			this.PnlSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PnlSearch.Controls.Add(this.CboSearchCraftNumber);
			this.PnlSearch.Controls.Add(this.LblSearchCraftNumber);
			this.PnlSearch.Controls.Add(this.BtnSearch);
			this.PnlSearch.Controls.Add(this.CboSearchCraft);
			this.PnlSearch.Controls.Add(this.LblSearchCraft);
			this.PnlSearch.Font = new System.Drawing.Font("Arial", 9F);
			this.PnlSearch.Location = new System.Drawing.Point(32, 19);
			this.PnlSearch.Name = "PnlSearch";
			this.PnlSearch.Size = new System.Drawing.Size(928, 64);
			this.PnlSearch.TabIndex = 0;
			// 
			// CboSearchCraftNumber
			// 
			this.CboSearchCraftNumber.Location = new System.Drawing.Point(112, 22);
			this.CboSearchCraftNumber.Name = "CboSearchCraftNumber";
			this.CboSearchCraftNumber.Size = new System.Drawing.Size(180, 23);
			this.CboSearchCraftNumber.TabIndex = 1;
			// 
			// LblSearchCraftNumber
			// 
			this.LblSearchCraftNumber.Font = new System.Drawing.Font("Arial", 9F);
			this.LblSearchCraftNumber.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.LblSearchCraftNumber.Location = new System.Drawing.Point(40, 24);
			this.LblSearchCraftNumber.Name = "LblSearchCraftNumber";
			this.LblSearchCraftNumber.Size = new System.Drawing.Size(72, 23);
			this.LblSearchCraftNumber.TabIndex = 23;
			this.LblSearchCraftNumber.Text = "Gewerk-Nr.";
			// 
			// BtnSearch
			// 
			this.BtnSearch.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.BtnSearch.Location = new System.Drawing.Point(792, 16);
			this.BtnSearch.Name = "BtnSearch";
			this.BtnSearch.Size = new System.Drawing.Size(100, 30);
			this.BtnSearch.TabIndex = 3;
			this.BtnSearch.Text = "&Suche";
			this.TooSearch.SetToolTip(this.BtnSearch, "Löst den Suchvorgang aus");
			this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
			// 
			// CboSearchCraft
			// 
			this.CboSearchCraft.Location = new System.Drawing.Point(468, 22);
			this.CboSearchCraft.Name = "CboSearchCraft";
			this.CboSearchCraft.Size = new System.Drawing.Size(280, 23);
			this.CboSearchCraft.TabIndex = 2;
			// 
			// LblSearchCraft
			// 
			this.LblSearchCraft.Font = new System.Drawing.Font("Arial", 9F);
			this.LblSearchCraft.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.LblSearchCraft.Location = new System.Drawing.Point(340, 24);
			this.LblSearchCraft.Name = "LblSearchCraft";
			this.LblSearchCraft.Size = new System.Drawing.Size(120, 23);
			this.LblSearchCraft.TabIndex = 21;
			this.LblSearchCraft.Text = "Gewerkbezeichnung";
			// 
			// DgrCraft
			// 
			this.DgrCraft.AllowSorting = false;
			this.DgrCraft.CaptionBackColor = System.Drawing.Color.SteelBlue;
			this.DgrCraft.DataMember = "";
			this.DgrCraft.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.DgrCraft.Location = new System.Drawing.Point(32, 99);
			this.DgrCraft.Name = "DgrCraft";
			this.DgrCraft.ReadOnly = true;
			this.DgrCraft.Size = new System.Drawing.Size(928, 336);
			this.DgrCraft.TabIndex = 4;
			this.DgrCraft.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
																								 this.dataGridTableStyle1});
			this.DgrCraft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DgrCraft_MouseDown);
			this.DgrCraft.CurrentCellChanged += new System.EventHandler(this.DgrCraft_CurrentCellChanged);
			this.DgrCraft.Enter += new System.EventHandler(this.DgrCraft_Enter);
			// 
			// dataGridTableStyle1
			// 
			this.dataGridTableStyle1.DataGrid = this.DgrCraft;
			this.dataGridTableStyle1.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
																												  this.dataGridTextBoxColumn1,
																												  this.dataGridTextBoxColumn2,
																												  this.dataGridTextBoxColumn3});
			this.dataGridTableStyle1.HeaderFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.dataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGridTableStyle1.MappingName = "FPASS_CRAFT";
			this.dataGridTableStyle1.PreferredColumnWidth = 628;
			this.dataGridTableStyle1.ReadOnly = true;
			// 
			// dataGridTextBoxColumn1
			// 
			this.dataGridTextBoxColumn1.Format = "";
			this.dataGridTextBoxColumn1.FormatInfo = null;
			this.dataGridTextBoxColumn1.MappingName = "CRA_ID";
			this.dataGridTextBoxColumn1.ReadOnly = true;
			this.dataGridTextBoxColumn1.Width = 0;
			// 
			// dataGridTextBoxColumn2
			// 
			this.dataGridTextBoxColumn2.Format = "";
			this.dataGridTextBoxColumn2.FormatInfo = null;
			this.dataGridTextBoxColumn2.HeaderText = "Gewerknummer";
			this.dataGridTextBoxColumn2.MappingName = "CRA_CRAFTNO";
			this.dataGridTextBoxColumn2.ReadOnly = true;
			this.dataGridTextBoxColumn2.Width = 300;
			// 
			// dataGridTextBoxColumn3
			// 
			this.dataGridTextBoxColumn3.Format = "";
			this.dataGridTextBoxColumn3.FormatInfo = null;
			this.dataGridTextBoxColumn3.HeaderText = "Bezeichnung des Gewerks";
			this.dataGridTextBoxColumn3.MappingName = "CRA_CRAFTNOTATION";
			this.dataGridTextBoxColumn3.ReadOnly = true;
			this.dataGridTextBoxColumn3.Width = 628;
			// 
			// LblEdit
			// 
			this.LblEdit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
			this.LblEdit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.LblEdit.Location = new System.Drawing.Point(48, 443);
			this.LblEdit.Name = "LblEdit";
			this.LblEdit.Size = new System.Drawing.Size(56, 16);
			this.LblEdit.TabIndex = 40;
			this.LblEdit.Text = "Eingabe";
			// 
			// PnlEdit
			// 
			this.PnlEdit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PnlEdit.Controls.Add(this.LblEditCraftNumber);
			this.PnlEdit.Controls.Add(this.TxtEditCraftNumber);
			this.PnlEdit.Controls.Add(this.TxtEditCraft);
			this.PnlEdit.Controls.Add(this.LblEditCraft);
			this.PnlEdit.Font = new System.Drawing.Font("Arial", 9F);
			this.PnlEdit.Location = new System.Drawing.Point(32, 451);
			this.PnlEdit.Name = "PnlEdit";
			this.PnlEdit.Size = new System.Drawing.Size(928, 80);
			this.PnlEdit.TabIndex = 5;
			// 
			// LblEditCraftNumber
			// 
			this.LblEditCraftNumber.Font = new System.Drawing.Font("Arial", 9F);
			this.LblEditCraftNumber.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.LblEditCraftNumber.Location = new System.Drawing.Point(40, 32);
			this.LblEditCraftNumber.Name = "LblEditCraftNumber";
			this.LblEditCraftNumber.Size = new System.Drawing.Size(72, 23);
			this.LblEditCraftNumber.TabIndex = 24;
			this.LblEditCraftNumber.Text = "Gewerk-Nr.";
			// 
			// TxtEditCraftNumber
			// 
			this.TxtEditCraftNumber.Enabled = false;
			this.TxtEditCraftNumber.Font = new System.Drawing.Font("Arial", 9F);
			this.TxtEditCraftNumber.Location = new System.Drawing.Point(120, 30);
			this.TxtEditCraftNumber.MaxLength = 30;
			this.TxtEditCraftNumber.Name = "TxtEditCraftNumber";
			this.TxtEditCraftNumber.Size = new System.Drawing.Size(180, 21);
			this.TxtEditCraftNumber.TabIndex = 6;
			this.TxtEditCraftNumber.Text = "";
			this.TxtEditCraftNumber.Enter += new System.EventHandler(this.TxtEditCraftNumber_Enter);
			// 
			// TxtEditCraft
			// 
			this.TxtEditCraft.Enabled = false;
			this.TxtEditCraft.Font = new System.Drawing.Font("Arial", 9F);
			this.TxtEditCraft.Location = new System.Drawing.Point(488, 30);
			this.TxtEditCraft.MaxLength = 50;
			this.TxtEditCraft.Name = "TxtEditCraft";
			this.TxtEditCraft.Size = new System.Drawing.Size(280, 21);
			this.TxtEditCraft.TabIndex = 7;
			this.TxtEditCraft.Text = "";
			this.TxtEditCraft.Enter += new System.EventHandler(this.TxtEditCraft_Enter);
			// 
			// LblEditCraft
			// 
			this.LblEditCraft.Font = new System.Drawing.Font("Arial", 9F);
			this.LblEditCraft.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.LblEditCraft.Location = new System.Drawing.Point(360, 32);
			this.LblEditCraft.Name = "LblEditCraft";
			this.LblEditCraft.Size = new System.Drawing.Size(136, 23);
			this.LblEditCraft.TabIndex = 21;
			this.LblEditCraft.Text = "Gewerkbezeichnung";
			// 
			// BtnCancel
			// 
			this.BtnCancel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.BtnCancel.Location = new System.Drawing.Point(736, 552);
			this.BtnCancel.Name = "BtnCancel";
			this.BtnCancel.Size = new System.Drawing.Size(104, 30);
			this.BtnCancel.TabIndex = 11;
			this.BtnCancel.Tag = "";
			this.BtnCancel.Text = "&Abbrechen";
			this.TooCancel.SetToolTip(this.BtnCancel, "Verwirft die bereits eingegebenen Daten");
			this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
			// 
			// BtnBackTo
			// 
			this.BtnBackTo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.BtnBackTo.Location = new System.Drawing.Point(856, 552);
			this.BtnBackTo.Name = "BtnBackTo";
			this.BtnBackTo.Size = new System.Drawing.Size(104, 30);
			this.BtnBackTo.TabIndex = 12;
			this.BtnBackTo.Tag = "";
			this.BtnBackTo.Text = "&Zurück";
			this.TooBackTo.SetToolTip(this.BtnBackTo, "Zurück zur Übersichtsmaske");
			this.BtnBackTo.Click += new System.EventHandler(this.BtnBackTo_Click);
			// 
			// BtnDelete
			// 
			this.BtnDelete.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.BtnDelete.Location = new System.Drawing.Point(616, 552);
			this.BtnDelete.Name = "BtnDelete";
			this.BtnDelete.Size = new System.Drawing.Size(104, 30);
			this.BtnDelete.TabIndex = 10;
			this.BtnDelete.Tag = "";
			this.BtnDelete.Text = "&Löschen";
			this.TooDelete.SetToolTip(this.BtnDelete, "Löscht den markierten Datensatz");
			this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
			// 
			// BtnSave
			// 
			this.BtnSave.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.BtnSave.Location = new System.Drawing.Point(496, 552);
			this.BtnSave.Name = "BtnSave";
			this.BtnSave.Size = new System.Drawing.Size(104, 30);
			this.BtnSave.TabIndex = 9;
			this.BtnSave.Tag = "";
			this.BtnSave.Text = "Speiche&rn";
			this.TooSave.SetToolTip(this.BtnSave, "Speichert den Datensatz");
			this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
			// 
			// BtnNew
			// 
			this.BtnNew.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.BtnNew.Location = new System.Drawing.Point(376, 552);
			this.BtnNew.Name = "BtnNew";
			this.BtnNew.Size = new System.Drawing.Size(104, 30);
			this.BtnNew.TabIndex = 8;
			this.BtnNew.Tag = "";
			this.BtnNew.Text = "&Neu";
			this.TooNew.SetToolTip(this.BtnNew, "Legt einen neuen Datensatz an");
			this.BtnNew.Click += new System.EventHandler(this.BtnNew_Click);
			// 
			// FrmUCAdminCraft
			// 
			this.Controls.Add(this.BtnNew);
			this.Controls.Add(this.BtnCancel);
			this.Controls.Add(this.BtnBackTo);
			this.Controls.Add(this.BtnDelete);
			this.Controls.Add(this.BtnSave);
			this.Controls.Add(this.LblSearch);
			this.Controls.Add(this.PnlSearch);
			this.Controls.Add(this.DgrCraft);
			this.Controls.Add(this.LblEdit);
			this.Controls.Add(this.PnlEdit);
			this.Name = "FrmUCAdminCraft";
			this.Size = new System.Drawing.Size(992, 616);
			this.Leave += new System.EventHandler(this.FrmUCAdminCraft_Leave);
			this.PnlSearch.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.DgrCraft)).EndInit();
			this.PnlEdit.ResumeLayout(false);
			this.ResumeLayout(false);

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

		#endregion

		#region Accessors 


		public DSCraft PropDSCraft
		{
			get 
			{
				return mDSCraft;
			}
			set 
			{
				mDSCraft = value;
			}
		} 


		// ID of currently selected AdminRecord in grid
		public int CurrentAdminRec
		{
			get 
			{
				return mCurrentAdminRec;
			}
			set 
			{
				mCurrentAdminRec = value;
			}
		}
 
		// Flag if changes have been made
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

		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// MVC triad has to know its typified dataset: instantiated during initialization of FrmAdmin
		/// <see cref="using Degussa.FPASS.Gui.Dialog.Administration.FrmAdministration"/>
		/// </summary>
		internal void CreateDataSet() 
		{
			mDSCraft = new DSCraft();
		}

		/// <summary>
		/// Fill comboboxes: values in search criteria
		/// </summary>
		internal void FillLists() 
		{
			this.FillCraft();
			this.FillCraftNo();
		}

		/// <summary>
		/// Register active controller with this view
		/// </summary>
		/// <param name="aAbstractController"></param>
		internal void RegisterController(AbstractController aAbstractController) 
		{
			this.mController = aAbstractController;
		}

		/// <summary>
		/// Clear textbox in foot of form
		/// </summary>
		internal void ClearFields()
		{
			this.TxtEditCraft.Text       = "";
			this.TxtEditCraftNumber.Text = "";
			this.TxtEditCraft.DataBindings.Clear();
			this.TxtEditCraftNumber.DataBindings.Clear();
			this.TxtEditCraft.Enabled       = false;
			this.TxtEditCraftNumber.Enabled = false;
			mContentChanged  = false;
			mCurrentAdminRec = -1;
		}

		/// <summary>
		/// Get active controller (is instantiated at same time as this Form)
		/// </summary>
		/// <returns></returns>
		private AdministrationController GetMyController() 
		{
			return (AdministrationController)mController;
		}

		/// <summary>
		/// Combobox CraftNotation
		/// </summary>
		private void FillCraft() 
		{
			ArrayList craftNumber = new ArrayList(); 
			craftNumber.Add(new LovItem("0", ""));
			craftNumber.AddRange(LovSingleton.GetInstance().GetRootList(null, "FPASS_CRAFT", "CRA_CRAFTNO"));
			this.CboSearchCraftNumber.DataSource = craftNumber;
			this.CboSearchCraftNumber.DisplayMember = "ItemValue";
			this.CboSearchCraftNumber.ValueMember = "DecId";
		}

		/// <summary>
		/// Cbx Craft numbers
		/// </summary>
		private void FillCraftNo() 
		{
			ArrayList craftName = new ArrayList();
			craftName.Add(new LovItem("0", ""));
			craftName.AddRange( LovSingleton.GetInstance().GetRootList(null, "FPASS_CRAFT", "CRA_CRAFTNOTATION") );		
			craftName.Add(new LovItem("0", ""));
			this.CboSearchCraft.DataSource = craftName;
			this.CboSearchCraft.DisplayMember = "ItemValue";
			this.CboSearchCraft.ValueMember = "DecId";
		}

		/// <summary>
		/// Get the PK id of the current craft record
		/// .. that is the data record currently selected in the datagrid
		/// and load record for editing
		/// </summary>
		private void TableNavigated()
		{
			int rowIndex = this.DgrCraft.CurrentRowIndex;
			if(-1 < rowIndex)
			{
				mCurrentAdminRec = Convert.ToInt32(this.DgrCraft[rowIndex, 0].ToString());
			}
			GetMyController().HandleEventDgrNavigateTabCraft();
		}

		/// <summary>
		/// If user enters any textfield on form, assume content has been edited & changed
		/// </summary>
		private void TextFieldsChanged()
		{
			this.mContentChanged = true;
		}

		/// <summary>
		/// Commented out 28.11.2003
		/// </summary>
		private void SetAuthorization() 
		{
		//	this.Enabled = UserManagementControl.getInstance().
		//		GetAuthorization(UserManagementControl.ADMIN_CRAFT_DIALOG);
		}

		
		#endregion // End of Methods

		#region Events  
		

		/// <summary>
		/// Button "Suchen"
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnSearch_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventBtnSearchTabCraft();
		}


		/// <summary>
		/// Fired when datagrid is entered, allows PK ID(s) of current record to be read
		/// Idea is, if there is only one record returned then CurrentCellChanged does not fire.
		/// Paint is not used here as it is fired too many times during load ( also during fill datagrid once dataset is filled)
		/// Cannot tell if contents of form have been changed by user
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DgrCraft_Enter(object sender, System.EventArgs e)
		{
			if ( this.DgrCraft.VisibleRowCount > 0 )
			{
				this.TableNavigated();
			}			
		}

		/// <summary>
		/// Fired each time a record is selected in  datagrid, allows PK ID(s) of current record to be read
		/// unless grid in process of being sorted
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DgrCraft_CurrentCellChanged(object sender, System.EventArgs e)
		{
			if ( !mGridIsSorted )
			{
				if ( this.DgrCraft.VisibleRowCount > 1 )
				{
					this.TableNavigated();
				}
			}
		}


		private void DgrCraft_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			mGridIsSorted = true;
			if ( this.DgrCraft.VisibleRowCount > 1 )
			{
				DataGrid.HitTestInfo hti  = DgrCraft.HitTest(e.X, e.Y);
				DataGrid.HitTestType type = hti.Type;
				switch(type)       
				{         
					case System.Windows.Forms.DataGrid.HitTestType.ColumnHeader:
						this.DgrCraft.CurrentRowIndex = 0;
						ClearFields();
						break;				
				}
			}
			mGridIsSorted = false;		
		}
	

		private void BtnSave_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventBtnSaveTabCraft();
		}

		private void BtnDelete_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventBtnDeleteTabCraft();
		}

		private void BtnCancel_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventBtnCancelClick();
		}

		private void BtnNew_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventBtnNewCraftClick();		
		}

		private void BtnBackTo_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventBackBtnCraft();	
		}

		/// <summary>
		/// Fired when this tab (UserControl) is left and user has executed search, edited etc on this tab
		/// Check not DesignMode necessary due to bugs in IDE Designer
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <exception cref="Degussa.FPASS.Util.Exceptions.ActionCancelledException">ActionCancelledException is thrown
		/// if user cancels prompt to save changes (should stop tab being left but currently doesn't)</exception>
		private void FrmUCAdminCraft_Leave(object sender, System.EventArgs e)
		{
			if ( ! DesignMode ) 
			{
				try
				{
					GetMyController().HandleEventTabCraftExited();
				}
				catch ( ActionCancelledException )
				{
					// Do nowt
				}
			}
		}

		/// <summary>
		/// Assume contents of form have changed if user enters textfield (edit...)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TxtEditCraftNumber_Enter(object sender, System.EventArgs e)
		{
			this.TextFieldsChanged();	
		}

		/// <summary>
		/// Assume contents of form have changed if user enters textfield (edit...)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TxtEditCraft_Enter(object sender, System.EventArgs e)
		{
			this.TextFieldsChanged();	
		}

		#endregion // End of Events		

	
	}
}

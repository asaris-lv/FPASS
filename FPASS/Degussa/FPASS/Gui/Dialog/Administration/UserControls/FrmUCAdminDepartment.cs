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
	/// A FrmUCAdminDepartment is one of the usercontrols 
	/// of the FrmAdministration.
	/// FrmUCAdminDepartment extends from the System.Windows.Forms.UserControl.
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
	public class FrmUCAdminDepartment : System.Windows.Forms.UserControl
	{

		#region Members

		//panels
		internal System.Windows.Forms.Panel PnlSearch;
		internal System.Windows.Forms.Panel PnlEdit;

		//labels
		internal System.Windows.Forms.Label LblSearch;
		internal System.Windows.Forms.Label LblSearchDepartment;
		internal System.Windows.Forms.Label LblEdit;
		internal System.Windows.Forms.Label LblEditDepartment;

		//textboxes
		internal System.Windows.Forms.TextBox TxtEditDepartment;

        //comboboxes

		//buttons
		internal System.Windows.Forms.Button BtnCancel;
		internal System.Windows.Forms.Button BtnBackTo;
		internal System.Windows.Forms.Button BtnDelete;
		internal System.Windows.Forms.Button BtnSave;
		internal System.Windows.Forms.Button BtnNew;
		internal System.Windows.Forms.Button BtnSearch;

		//tooltips
		private System.Windows.Forms.ToolTip TooSearch;
		private System.Windows.Forms.ToolTip TooBackTo;
		private System.Windows.Forms.ToolTip TooCancel;
		private System.Windows.Forms.ToolTip TooDelete;
		private System.Windows.Forms.ToolTip TooSave;
		private System.Windows.Forms.ToolTip TooNew;

		//tables
		internal System.Windows.Forms.DataGrid DgrDepartment;
		private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn2;

		/// <summary>
		/// Typified dataset specific to Department
		/// </summary>
		protected DSDepartment mDSDepartment;
		/// <summary>
		/// holds the id of the current admin object selected in the displayed table
		/// </summary>
		private int mCurrentAdminRec = -1;
		/// <summary>
		/// Have data on form changed? This has an accessor and is checked in <see cref="AdministrationModel"/> AdministrationModel
		/// </summary>
		private bool mContentChanged = false;
		/// <summary>
		/// IDE
		/// </summary>
		private System.ComponentModel.IContainer components;
		/// <summary>
		/// used to hold the Controller of this triad
		/// </summary>
        internal AbstractController mController;
        protected internal TextBox TxtSearchDepartment;
		/// <summary>
		/// Used to stop CellChanged event firing when grid sorted
		/// </summary>
		private bool mGridSorted = false;

		#endregion //End of Members

		#region Constructors
		public FrmUCAdminDepartment()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

//			InitView();

			
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
            this.TxtSearchDepartment = new System.Windows.Forms.TextBox();
            this.BtnSearch = new System.Windows.Forms.Button();
            this.LblSearchDepartment = new System.Windows.Forms.Label();
            this.DgrDepartment = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.LblEdit = new System.Windows.Forms.Label();
            this.PnlEdit = new System.Windows.Forms.Panel();
            this.TxtEditDepartment = new System.Windows.Forms.TextBox();
            this.LblEditDepartment = new System.Windows.Forms.Label();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.BtnBackTo = new System.Windows.Forms.Button();
            this.BtnDelete = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnNew = new System.Windows.Forms.Button();
            this.TooSearch = new System.Windows.Forms.ToolTip(this.components);
            this.TooBackTo = new System.Windows.Forms.ToolTip(this.components);
            this.TooCancel = new System.Windows.Forms.ToolTip(this.components);
            this.TooDelete = new System.Windows.Forms.ToolTip(this.components);
            this.TooSave = new System.Windows.Forms.ToolTip(this.components);
            this.TooNew = new System.Windows.Forms.ToolTip(this.components);
            this.PnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgrDepartment)).BeginInit();
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
            this.LblSearch.TabIndex = 39;
            this.LblSearch.Text = "Suche";
            // 
            // PnlSearch
            // 
            this.PnlSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlSearch.Controls.Add(this.TxtSearchDepartment);
            this.PnlSearch.Controls.Add(this.BtnSearch);
            this.PnlSearch.Controls.Add(this.LblSearchDepartment);
            this.PnlSearch.Font = new System.Drawing.Font("Arial", 9F);
            this.PnlSearch.Location = new System.Drawing.Point(32, 19);
            this.PnlSearch.Name = "PnlSearch";
            this.PnlSearch.Size = new System.Drawing.Size(1187, 64);
            this.PnlSearch.TabIndex = 0;
            // 
            // TxtSearchDepartment
            // 
            this.TxtSearchDepartment.Location = new System.Drawing.Point(128, 21);
            this.TxtSearchDepartment.Name = "TxtSearchDepartment";
            this.TxtSearchDepartment.Size = new System.Drawing.Size(347, 21);
            this.TxtSearchDepartment.TabIndex = 22;
            // 
            // BtnSearch
            // 
            this.BtnSearch.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSearch.Location = new System.Drawing.Point(1055, 17);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(100, 30);
            this.BtnSearch.TabIndex = 2;
            this.BtnSearch.Text = "&Suche";
            this.TooSearch.SetToolTip(this.BtnSearch, "Löst den Suchvorgang aus");
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // LblSearchDepartment
            // 
            this.LblSearchDepartment.Font = new System.Drawing.Font("Arial", 9F);
            this.LblSearchDepartment.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LblSearchDepartment.Location = new System.Drawing.Point(40, 24);
            this.LblSearchDepartment.Name = "LblSearchDepartment";
            this.LblSearchDepartment.Size = new System.Drawing.Size(72, 16);
            this.LblSearchDepartment.TabIndex = 21;
            this.LblSearchDepartment.Text = "Abteilung";
            // 
            // DgrDepartment
            // 
            this.DgrDepartment.AllowSorting = false;
            this.DgrDepartment.CaptionBackColor = System.Drawing.Color.SteelBlue;
            this.DgrDepartment.DataMember = "";
            this.DgrDepartment.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.DgrDepartment.Location = new System.Drawing.Point(32, 99);
            this.DgrDepartment.Name = "DgrDepartment";
            this.DgrDepartment.ReadOnly = true;
            this.DgrDepartment.Size = new System.Drawing.Size(1187, 336);
            this.DgrDepartment.TabIndex = 3;
            this.DgrDepartment.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.dataGridTableStyle1});
            this.DgrDepartment.CurrentCellChanged += new System.EventHandler(this.DgrDepartment_CurrentCellChanged);
            this.DgrDepartment.Enter += new System.EventHandler(this.DgrDepartment_Enter);
            this.DgrDepartment.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DgrDepartment_MouseDown);
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.DataGrid = this.DgrDepartment;
            this.dataGridTableStyle1.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.dataGridTextBoxColumn1,
            this.dataGridTextBoxColumn2});
            this.dataGridTableStyle1.HeaderFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGridTableStyle1.MappingName = "FPASS_DEPARTMENT";
            this.dataGridTableStyle1.PreferredColumnWidth = 950;
            this.dataGridTableStyle1.ReadOnly = true;
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            this.dataGridTextBoxColumn1.HeaderText = "DEPT_ID";
            this.dataGridTextBoxColumn1.MappingName = "DEPT_ID";
            this.dataGridTextBoxColumn1.Width = 0;
            // 
            // dataGridTextBoxColumn2
            // 
            this.dataGridTextBoxColumn2.Format = "";
            this.dataGridTextBoxColumn2.FormatInfo = null;
            this.dataGridTextBoxColumn2.HeaderText = "Abteilung";
            this.dataGridTextBoxColumn2.MappingName = "DEPT_DEPARTMENT";
            this.dataGridTextBoxColumn2.Width = 1135;
            // 
            // LblEdit
            // 
            this.LblEdit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.LblEdit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LblEdit.Location = new System.Drawing.Point(48, 443);
            this.LblEdit.Name = "LblEdit";
            this.LblEdit.Size = new System.Drawing.Size(56, 16);
            this.LblEdit.TabIndex = 38;
            this.LblEdit.Text = "Eingabe";
            // 
            // PnlEdit
            // 
            this.PnlEdit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlEdit.Controls.Add(this.TxtEditDepartment);
            this.PnlEdit.Controls.Add(this.LblEditDepartment);
            this.PnlEdit.Font = new System.Drawing.Font("Arial", 9F);
            this.PnlEdit.Location = new System.Drawing.Point(32, 451);
            this.PnlEdit.Name = "PnlEdit";
            this.PnlEdit.Size = new System.Drawing.Size(1187, 80);
            this.PnlEdit.TabIndex = 4;
            // 
            // TxtEditDepartment
            // 
            this.TxtEditDepartment.Enabled = false;
            this.TxtEditDepartment.Font = new System.Drawing.Font("Arial", 9F);
            this.TxtEditDepartment.Location = new System.Drawing.Point(128, 30);
            this.TxtEditDepartment.MaxLength = 50;
            this.TxtEditDepartment.Name = "TxtEditDepartment";
            this.TxtEditDepartment.Size = new System.Drawing.Size(347, 21);
            this.TxtEditDepartment.TabIndex = 5;
            this.TxtEditDepartment.Enter += new System.EventHandler(this.TxtEditDepartment_Enter);
            // 
            // LblEditDepartment
            // 
            this.LblEditDepartment.Font = new System.Drawing.Font("Arial", 9F);
            this.LblEditDepartment.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LblEditDepartment.Location = new System.Drawing.Point(40, 32);
            this.LblEditDepartment.Name = "LblEditDepartment";
            this.LblEditDepartment.Size = new System.Drawing.Size(136, 23);
            this.LblEditDepartment.TabIndex = 21;
            this.LblEditDepartment.Text = "Abteilung";
            // 
            // BtnCancel
            // 
            this.BtnCancel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Location = new System.Drawing.Point(993, 552);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(104, 30);
            this.BtnCancel.TabIndex = 9;
            this.BtnCancel.Tag = "";
            this.BtnCancel.Text = "&Abbrechen";
            this.TooCancel.SetToolTip(this.BtnCancel, "Verwirft die bereits eingegebenen Daten");
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnBackTo
            // 
            this.BtnBackTo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBackTo.Location = new System.Drawing.Point(1113, 552);
            this.BtnBackTo.Name = "BtnBackTo";
            this.BtnBackTo.Size = new System.Drawing.Size(104, 30);
            this.BtnBackTo.TabIndex = 10;
            this.BtnBackTo.Tag = "";
            this.BtnBackTo.Text = "&Zurück";
            this.TooBackTo.SetToolTip(this.BtnBackTo, "Zurück zur Übersichtsmaske");
            this.BtnBackTo.Click += new System.EventHandler(this.BtnBackTo_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelete.Location = new System.Drawing.Point(873, 552);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(104, 30);
            this.BtnDelete.TabIndex = 8;
            this.BtnDelete.Tag = "";
            this.BtnDelete.Text = "&Löschen";
            this.TooDelete.SetToolTip(this.BtnDelete, "Löscht den markierten Datensatz");
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Location = new System.Drawing.Point(753, 552);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(104, 30);
            this.BtnSave.TabIndex = 7;
            this.BtnSave.Tag = "";
            this.BtnSave.Text = "Speiche&rn";
            this.TooSave.SetToolTip(this.BtnSave, "Speichert den Datensatz");
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnNew
            // 
            this.BtnNew.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnNew.Location = new System.Drawing.Point(633, 552);
            this.BtnNew.Name = "BtnNew";
            this.BtnNew.Size = new System.Drawing.Size(104, 30);
            this.BtnNew.TabIndex = 6;
            this.BtnNew.Tag = "";
            this.BtnNew.Text = "&Neu";
            this.TooNew.SetToolTip(this.BtnNew, "Legt einen neuen Datensatz an");
            this.BtnNew.Click += new System.EventHandler(this.BtnNew_Click);
            // 
            // FrmUCAdminDepartment
            // 
            this.Controls.Add(this.BtnNew);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnBackTo);
            this.Controls.Add(this.BtnDelete);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.LblSearch);
            this.Controls.Add(this.PnlSearch);
            this.Controls.Add(this.DgrDepartment);
            this.Controls.Add(this.LblEdit);
            this.Controls.Add(this.PnlEdit);
            this.Name = "FrmUCAdminDepartment";
            this.Size = new System.Drawing.Size(1258, 816);
            this.Leave += new System.EventHandler(this.FrmUCAdminDepartment_Leave);
            this.PnlSearch.ResumeLayout(false);
            this.PnlSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgrDepartment)).EndInit();
            this.PnlEdit.ResumeLayout(false);
            this.PnlEdit.PerformLayout();
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

		public DSDepartment PropDSDepartment
		{
			get 
			{
				return mDSDepartment;
			}
			set 
			{
				mDSDepartment = value;
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

		public void CreateDataSet() 
		{
			mDSDepartment = new DSDepartment();
		}

		/// <summary>
		/// Fill combobox "dept" for search
		/// </summary>
		internal void FillLists() 
		{
			this.FillDepartment();
		}
 
		private AdministrationController GetMyController() 
		{
			return (AdministrationController)mController;
		}

		/// <summary>
		/// Fill combobox "dept" for search
		/// </summary>
		private void FillDepartment()
		{
            // No longer required, changed to textbox
            //ArrayList department = new ArrayList(); 
            //department.Add(new LovItem("0",""));
            //department.AddRange (LovSingleton.GetInstance().GetRootList(null, "FPASS_DEPARTMENT", "DEPT_DEPARTMENT") );	
            //this.CboSearchDepartment.DataSource = department;
            //this.CboSearchDepartment.DisplayMember = "ItemValue";
            //this.CboSearchDepartment.ValueMember = "DecId";
		}

		/// <summary>
		/// Get the PK id of the current department record
		/// .. that is the data record currently selected in the datagrid
		/// and load record for editing
		/// </summary>
		private void TableNavigated()
		{
			int rowIndex = this.DgrDepartment.CurrentRowIndex;
			if(-1 < rowIndex)
			{
				mCurrentAdminRec = Convert.ToInt32(this.DgrDepartment[rowIndex, 0].ToString());
			}
			GetMyController().HandleEventDgrNavigateTabDept();
		}
		
		/// <summary>
		/// Clear textfield in foot of form, discard current record
		/// </summary>
		public void ClearFields()
		{
			this.TxtEditDepartment.Text = "";
			this.TxtEditDepartment.DataBindings.Clear();
			this.TxtEditDepartment.Enabled = false;
			mContentChanged  = false;
			mCurrentAdminRec = -1;
		}

		/// <summary>
		/// Not used 12.12.03
		/// </summary>
		private void SetAuthorization() 
		{
		//	this.Enabled = UserManagementControl.getInstance().
		//		GetAuthorization(UserManagementControl.ADMIN_DEPARTMENT_DIALOG);
		}

		public void RegisterController(AbstractController aAbstractController) 
		{
			this.mController = aAbstractController;
		}

		#endregion // End of Methods

		#region Events
		
		/// <summary>
		/// Button Search "Suchen"
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnSearch_Click(object sender, System.EventArgs e)
		{		
			GetMyController().HandleEventBtnSearchTabDept();
		}

		/// <summary>
		/// Fired each time a record is selected in  datagrid, allows PK ID(s) of current record to be read
		/// Do not fire if grid in process of being sorted
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DgrDepartment_CurrentCellChanged(object sender, System.EventArgs e)
		{
			if ( !mGridSorted )
			{
				if ( this.DgrDepartment.VisibleRowCount > 1 )
				{
					this.TableNavigated();
				}
			}
		}

		/// <summary>
		/// Fired when datagrid is entered, allows PK ID(s) of current record to be read
		/// Idea is, if there is only one record returned then CurrentCellChanged does not fire.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DgrDepartment_Enter(object sender, System.EventArgs e)
		{
			if ( this.DgrDepartment.VisibleRowCount > 0 )
			{
				this.TableNavigated();
			}	
		}
		
		/// <summary>
		/// Button "Zurück"
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnBackTo_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventBackBtnDepartment();
		}

		/// <summary>
		/// Fired when this tab (UserControl) is left and user has executed search, edited etc on this tab
		/// Check not DesignMode necessary due to bugs in IDE Designer
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <exception cref="Degussa.FPASS.Util.Exceptions.ActionCancelledException">ActionCancelledException is thrown
		/// if user cancels prompt to save changes (should stop tab being left but currently doesn't)</exception>
		private void FrmUCAdminDepartment_Leave(object sender, System.EventArgs e)
		{
			if ( ! DesignMode ) 
			{
				try
				{
					GetMyController().HandleEventTabDepartmentExited();
				}
				catch ( ActionCancelledException )
				{
					// Do nowt
				}
			}
		}

		/// <summary>
		/// This event fires when the column header is clicked, i.e. when the grid is sorted.
		/// Discard currently selected record, user has to re-click to select 
		/// Put pointer on first row (index 0)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DgrDepartment_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			mGridSorted = true;
			if ( this.DgrDepartment.VisibleRowCount > 1 )
			{
				DataGrid.HitTestInfo hti  = DgrDepartment.HitTest(e.X, e.Y);
				DataGrid.HitTestType type = hti.Type;
				switch(type)       
				{         
					case System.Windows.Forms.DataGrid.HitTestType.ColumnHeader:
						DgrDepartment.CurrentRowIndex = 0;
						ClearFields();
						break;				
				}
			}
			mGridSorted = false;		
		}

		private void BtnSave_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventBtnSaveTabDept();
		}

		private void BtnDelete_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventBtnDeleteTabDept();
		}

		private void BtnCancel_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventBtnCancelClick();
		}

		private void BtnNew_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventBtnNewDeptClick();
		}

		private void TxtEditDepartment_Enter(object sender, System.EventArgs e)
		{
			this.ContentChanged = true;
		}

		#endregion // End of Events

		
	}
}

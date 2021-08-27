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
	/// A FrmUCAdminPlant is one of the usercontrols 
	/// of the FrmAdministration.
	/// FrmUCAdminPlant extends from the System.Windows.Forms.UserControl.
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
	public class FrmUCAdminPlant : System.Windows.Forms.UserControl
	{
		#region Members

		//panels
		internal System.Windows.Forms.Panel PnlSearch;
		internal System.Windows.Forms.Panel PnlEdit;

		//labels
		internal System.Windows.Forms.Label LblSearch;
		internal System.Windows.Forms.Label LblSearchPlant;
		internal System.Windows.Forms.Label LblEdit;
		internal System.Windows.Forms.Label LblEditPlant;

		//textboxes
		internal System.Windows.Forms.TextBox TxtEditPlant;

		//comboboxes
		internal System.Windows.Forms.ComboBox CboSearchPlant;

		//buttons
		internal System.Windows.Forms.Button BtnSearch;
		internal System.Windows.Forms.Button BtnCancel;
		internal System.Windows.Forms.Button BtnBackTo;
		internal System.Windows.Forms.Button BtnDelete;
		internal System.Windows.Forms.Button BtnSave;
		internal System.Windows.Forms.Button BtnNew;

		//tooltips
		private System.Windows.Forms.ToolTip TooSearch;
		private System.Windows.Forms.ToolTip TooBackTo;
		private System.Windows.Forms.ToolTip TooCancel;
		private System.Windows.Forms.ToolTip TooDelete;
		private System.Windows.Forms.ToolTip TooSave;
		private System.Windows.Forms.ToolTip TooNew;

		//tables
		internal System.Windows.Forms.DataGrid DgrPlant;
		private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn2;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;

		/// <summary>
		/// Instance of typified Plant Dataset
		/// </summary>
		protected DSPlant mDSPlant;
		/// <summary>
		/// Have data on form changed? This has an accessor and is checked in <see cref="AdministrationModel"/> AdministrationModel
		/// </summary>
		private bool mContentChanged = false;
		/// <summary>
		/// created by IDE
		/// </summary>
		private System.ComponentModel.IContainer components;
		/// <summary>
		/// holds the id of the current admin object selected in the displayed table
		/// </summary>
		private int mCurrentAdminRec = -1;

		private bool mGridSorted	 = false;
        internal Label LblPlantHint;
        private DataGridTextBoxColumn dataGridTextBoxColumn3;

		/// <summary>
		/// used to hold the Controller of this triad
		/// </summary>
		internal	AbstractController	mController;
		
		#endregion //End of Members
		
		#region Constructors
		public FrmUCAdminPlant()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			//InitView();

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
            this.BtnSearch = new System.Windows.Forms.Button();
            this.CboSearchPlant = new System.Windows.Forms.ComboBox();
            this.LblSearchPlant = new System.Windows.Forms.Label();
            this.DgrPlant = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn3 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.LblEdit = new System.Windows.Forms.Label();
            this.PnlEdit = new System.Windows.Forms.Panel();
            this.LblPlantHint = new System.Windows.Forms.Label();
            this.TxtEditPlant = new System.Windows.Forms.TextBox();
            this.LblEditPlant = new System.Windows.Forms.Label();
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
            ((System.ComponentModel.ISupportInitialize)(this.DgrPlant)).BeginInit();
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
            this.LblSearch.TabIndex = 37;
            this.LblSearch.Text = "Suche";
            // 
            // PnlSearch
            // 
            this.PnlSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlSearch.Controls.Add(this.BtnSearch);
            this.PnlSearch.Controls.Add(this.CboSearchPlant);
            this.PnlSearch.Controls.Add(this.LblSearchPlant);
            this.PnlSearch.Font = new System.Drawing.Font("Arial", 9F);
            this.PnlSearch.Location = new System.Drawing.Point(32, 19);
            this.PnlSearch.Name = "PnlSearch";
            this.PnlSearch.Size = new System.Drawing.Size(1187, 64);
            this.PnlSearch.TabIndex = 0;
            // 
            // BtnSearch
            // 
            this.BtnSearch.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSearch.Location = new System.Drawing.Point(1056, 17);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(100, 30);
            this.BtnSearch.TabIndex = 2;
            this.BtnSearch.Text = "&Suche";
            this.TooSearch.SetToolTip(this.BtnSearch, "Löst den Suchvorgang aus");
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // CboSearchPlant
            // 
            this.CboSearchPlant.Location = new System.Drawing.Point(128, 22);
            this.CboSearchPlant.Name = "CboSearchPlant";
            this.CboSearchPlant.Size = new System.Drawing.Size(417, 23);
            this.CboSearchPlant.TabIndex = 1;
            // 
            // LblSearchPlant
            // 
            this.LblSearchPlant.Font = new System.Drawing.Font("Arial", 9F);
            this.LblSearchPlant.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LblSearchPlant.Location = new System.Drawing.Point(27, 24);
            this.LblSearchPlant.Name = "LblSearchPlant";
            this.LblSearchPlant.Size = new System.Drawing.Size(72, 23);
            this.LblSearchPlant.TabIndex = 21;
            this.LblSearchPlant.Text = "Betrieb";
            // 
            // DgrPlant
            // 
            this.DgrPlant.AllowSorting = false;
            this.DgrPlant.CaptionBackColor = System.Drawing.Color.SteelBlue;
            this.DgrPlant.DataMember = "";
            this.DgrPlant.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.DgrPlant.Location = new System.Drawing.Point(30, 99);
            this.DgrPlant.Name = "DgrPlant";
            this.DgrPlant.ReadOnly = true;
            this.DgrPlant.Size = new System.Drawing.Size(1189, 336);
            this.DgrPlant.TabIndex = 3;
            this.DgrPlant.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.dataGridTableStyle1});
            this.DgrPlant.CurrentCellChanged += new System.EventHandler(this.DgrPlant_CurrentCellChanged_1);
            this.DgrPlant.Enter += new System.EventHandler(this.DgrPlant_Enter);
            this.DgrPlant.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DgrPlant_MouseDown);
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.DataGrid = this.DgrPlant;
            this.dataGridTableStyle1.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.dataGridTextBoxColumn1,
            this.dataGridTextBoxColumn2,
            this.dataGridTextBoxColumn3});
            this.dataGridTableStyle1.HeaderFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGridTableStyle1.MappingName = "FPASS_PLANT";
            this.dataGridTableStyle1.PreferredColumnWidth = 800;
            this.dataGridTableStyle1.ReadOnly = true;
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            this.dataGridTextBoxColumn1.HeaderText = "PL_ID";
            this.dataGridTextBoxColumn1.MappingName = "PL_ID";
            this.dataGridTextBoxColumn1.Width = 0;
            // 
            // dataGridTextBoxColumn2
            // 
            this.dataGridTextBoxColumn2.Format = "";
            this.dataGridTextBoxColumn2.FormatInfo = null;
            this.dataGridTextBoxColumn2.HeaderText = "Betrieb";
            this.dataGridTextBoxColumn2.MappingName = "PL_NAME";
            this.dataGridTextBoxColumn2.Width = 975;
            // 
            // dataGridTextBoxColumn3
            // 
            this.dataGridTextBoxColumn3.Format = "";
            this.dataGridTextBoxColumn3.FormatInfo = null;
            this.dataGridTextBoxColumn3.HeaderText = "Herkunft";
            this.dataGridTextBoxColumn3.MappingName = "PL_SOURCE";
            this.dataGridTextBoxColumn3.ReadOnly = true;
            this.dataGridTextBoxColumn3.Width = 165;
            // 
            // LblEdit
            // 
            this.LblEdit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.LblEdit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LblEdit.Location = new System.Drawing.Point(48, 443);
            this.LblEdit.Name = "LblEdit";
            this.LblEdit.Size = new System.Drawing.Size(56, 16);
            this.LblEdit.TabIndex = 36;
            this.LblEdit.Text = "Eingabe";
            // 
            // PnlEdit
            // 
            this.PnlEdit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlEdit.Controls.Add(this.LblPlantHint);
            this.PnlEdit.Controls.Add(this.TxtEditPlant);
            this.PnlEdit.Controls.Add(this.LblEditPlant);
            this.PnlEdit.Font = new System.Drawing.Font("Arial", 9F);
            this.PnlEdit.Location = new System.Drawing.Point(32, 451);
            this.PnlEdit.Name = "PnlEdit";
            this.PnlEdit.Size = new System.Drawing.Size(1187, 80);
            this.PnlEdit.TabIndex = 4;
            // 
            // LblPlantHint
            // 
            this.LblPlantHint.Font = new System.Drawing.Font("Arial", 9F);
            this.LblPlantHint.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LblPlantHint.Location = new System.Drawing.Point(572, 31);
            this.LblPlantHint.Name = "LblPlantHint";
            this.LblPlantHint.Size = new System.Drawing.Size(584, 23);
            this.LblPlantHint.TabIndex = 39;
            this.LblPlantHint.Text = "Hinweis: Betriebe mit Herkunft \"ZKS\" werden im ZKS verwaltet.";
            this.LblPlantHint.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // TxtEditPlant
            // 
            this.TxtEditPlant.Enabled = false;
            this.TxtEditPlant.Font = new System.Drawing.Font("Arial", 9F);
            this.TxtEditPlant.Location = new System.Drawing.Point(128, 28);
            this.TxtEditPlant.MaxLength = 50;
            this.TxtEditPlant.Name = "TxtEditPlant";
            this.TxtEditPlant.Size = new System.Drawing.Size(417, 21);
            this.TxtEditPlant.TabIndex = 5;
            this.TxtEditPlant.Enter += new System.EventHandler(this.TxtEditPlant_Enter);
            // 
            // LblEditPlant
            // 
            this.LblEditPlant.Font = new System.Drawing.Font("Arial", 9F);
            this.LblEditPlant.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LblEditPlant.Location = new System.Drawing.Point(27, 30);
            this.LblEditPlant.Name = "LblEditPlant";
            this.LblEditPlant.Size = new System.Drawing.Size(136, 23);
            this.LblEditPlant.TabIndex = 21;
            this.LblEditPlant.Text = "Betriebsname";
            // 
            // BtnCancel
            // 
            this.BtnCancel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Location = new System.Drawing.Point(995, 551);
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
            this.BtnBackTo.Location = new System.Drawing.Point(1115, 551);
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
            this.BtnDelete.Location = new System.Drawing.Point(875, 551);
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
            this.BtnSave.Location = new System.Drawing.Point(755, 551);
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
            this.BtnNew.Location = new System.Drawing.Point(635, 551);
            this.BtnNew.Name = "BtnNew";
            this.BtnNew.Size = new System.Drawing.Size(104, 30);
            this.BtnNew.TabIndex = 6;
            this.BtnNew.Tag = "";
            this.BtnNew.Text = "&Neu";
            this.TooNew.SetToolTip(this.BtnNew, "Legt einen neuen Datensatz an");
            this.BtnNew.Click += new System.EventHandler(this.BtnNew_Click);
            // 
            // FrmUCAdminPlant
            // 
            this.Controls.Add(this.BtnNew);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnBackTo);
            this.Controls.Add(this.BtnDelete);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.LblSearch);
            this.Controls.Add(this.PnlSearch);
            this.Controls.Add(this.DgrPlant);
            this.Controls.Add(this.LblEdit);
            this.Controls.Add(this.PnlEdit);
            this.Name = "FrmUCAdminPlant";
            this.Size = new System.Drawing.Size(1258, 816);
            this.Leave += new System.EventHandler(this.FrmUCAdminPlant_Leave);
            this.PnlSearch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DgrPlant)).EndInit();
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

		public DSPlant PropDSPlant
		{
			get 
			{
				return mDSPlant;
			}
			set 
			{
				mDSPlant = value;
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
		/// Instantiates typifed dataset during construction of Controller
		/// </summary>
		public void CreateDataSet() 
		{
			mDSPlant = new DSPlant();
		}

		/// <summary>
		/// Fills combobox Plant
		/// </summary>
		internal void FillLists() 
		{
			FillPlant();
		}


		private AdministrationController GetMyController() 
		{
			return (AdministrationController)mController;
		}

		/// <summary>
		/// Get the PK id of the current plant record
		/// .. that is the data record currently selected in the datagrid
		/// and load record for editing
		/// </summary>
		private void TableNavigated()
		{
			int rowIndex = this.DgrPlant.CurrentRowIndex;
			if(-1 < rowIndex)
			{
				mCurrentAdminRec = Convert.ToInt32(this.DgrPlant[rowIndex, 0].ToString());
			}
			GetMyController().HandleEventDgrNavigateTabPlant();
		}

		/// <summary>
		/// Fill combobox Plant
		/// </summary>
		private void FillPlant(){
			ArrayList plant = new ArrayList(); 
			plant.Add(new LovItem("0",""));
			plant.AddRange(LovSingleton.GetInstance().GetRootList(null, "FPASS_PLANT", "PL_NAME") );
			this.CboSearchPlant.DataSource    = plant;
			this.CboSearchPlant.DisplayMember = "ItemValue";
			this.CboSearchPlant.ValueMember   = "DecId";
		}
	
		
		/// <summary>
		/// commented out 11.11.03: kann weg
		/// </summary>
		private void SetAuthorization() 
		{
		//	this.Enabled = UserManagementControl.getInstance().
		//		GetAuthorization(UserManagementControl.ADMIN_PLANT_DIALOG);
		}

		internal void RegisterController(AbstractController aAbstractController) 
		{
			this.mController = aAbstractController;
		}

		/// <summary>
		/// Clear textbox in foot of form
		/// </summary>
		internal void ClearFields()
		{
			this.TxtEditPlant.DataBindings.Clear();
			this.TxtEditPlant.Text    = "";
			this.TxtEditPlant.Enabled = false;
			mCurrentAdminRec		  = -1;
			mContentChanged			  = false;
		}

		#endregion // End of Methods

		#region Events

		private void BtnSearch_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventBtnSearchTabPlant();
		}

		/// <summary>
		/// Fired when datagrid is entered, allows PK ID(s) of current record to be read
		/// Idea is, if there is only one record returned then CurrentCellChanged does not fire.
		/// Paint is not used here as it is fired too many times during load ( also during fill datagrid once dataset is filled)
		/// Cannot tell if contents of form have been changed by user
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DgrPlant_Enter(object sender, System.EventArgs e)
		{
			if ( this.DgrPlant.VisibleRowCount > 0 )
			{
				this.TableNavigated();
			}	
		}

		/// <summary>
		/// Fired each time a record is selected in  datagrid, allows PK ID(s) of current record to be read
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DgrPlant_CurrentCellChanged_1(object sender, System.EventArgs e)
		{
			if ( !mGridSorted )
			{
				if ( this.DgrPlant.VisibleRowCount > 1 )
				{
					this.TableNavigated();
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
		private void DgrPlant_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			mGridSorted = true;
			if ( this.DgrPlant.VisibleRowCount > 1 )
			{
				DataGrid.HitTestInfo hti  = DgrPlant.HitTest(e.X, e.Y);
				DataGrid.HitTestType type = hti.Type;
				switch(type)       
				{         
					case System.Windows.Forms.DataGrid.HitTestType.ColumnHeader:
						DgrPlant.CurrentRowIndex = 0;
						ClearFields();
						break;				
				}
			}
			mGridSorted = false;
		}

		/// <summary>
		/// Textfields: if entered by user (i.e. to edit data) assume contents of form have changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnBackTo_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventBackBtnPlant();
		}

		private void BtnSave_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventBtnSaveTabPlant();
		}

		private void BtnDelete_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventBtnDeleteTabPlant();
		}

		private void BtnCancel_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventBtnCancelClick();
		}

		private void BtnNew_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventBtnNewPlantClick();
		}

		private void TxtEditPlant_Enter(object sender, System.EventArgs e)
		{
			this.ContentChanged = true;
		}	

		/// <summary>
		/// Fired when this tab (UserControl) is left and user has executed search, edited etc on this tab
		/// Check not DesignMode necessary due to bugs in IDE Designer
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <exception cref="Degussa.FPASS.Util.Exceptions.ActionCancelledException">ActionCancelledException is thrown
		/// if user cancels prompt to save changes (should stop tab being left but currently doesn't)</exception>
		private void FrmUCAdminPlant_Leave(object sender, System.EventArgs e)
		{
			if ( ! DesignMode ) 
			{
				try
				{
					GetMyController().HandleEventTabPlantExited();
				}
				catch ( ActionCancelledException )
				{
					// Do nowt
				}
			}
		}
	
		#endregion // End of Events

		
	}
}

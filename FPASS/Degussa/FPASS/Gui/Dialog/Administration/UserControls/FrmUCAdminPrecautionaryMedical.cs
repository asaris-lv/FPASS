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
	/// A FrmUCAdminMedicalPrecautionary is one of the usercontrols 
	/// of the FrmAdministration.
	/// FrmUCAdminMedicalPrecautionary extends from the System.Windows.Forms.UserControl.
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
	public class FrmUCAdminMedicalPrecautionary : System.Windows.Forms.UserControl
	{

		#region Members

		//panels
		internal System.Windows.Forms.Panel PnlSearch;
		internal System.Windows.Forms.Panel PnlEdit;

		//labels
		internal System.Windows.Forms.Label LblSearch;
		internal System.Windows.Forms.Label LblSearchPrecautionaryMedical;
		internal System.Windows.Forms.Label LblEdit;
		internal System.Windows.Forms.Label LblEditKind;
		internal System.Windows.Forms.Label LblEditHelp;
		internal System.Windows.Forms.Label LblEditPrecautionaryMedical;
		internal System.Windows.Forms.Label LblSearchKind;

		//textboxes
		internal System.Windows.Forms.TextBox TxtEditKind;
		internal System.Windows.Forms.TextBox TxtEditHelp;
		internal System.Windows.Forms.TextBox TxtEditPrecautionaryMedical;

		//comboboxes
		internal System.Windows.Forms.ComboBox CboSearchPrecautionaryMedical;
		internal System.Windows.Forms.ComboBox CboSearchKind;

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
		internal System.Windows.Forms.DataGrid DgrPrecautionaryMedical;
		private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn2;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn3;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn4;

		/// <summary>
		/// Instance of typified PrecMedical dataset
		/// </summary>
		protected DSPrecMedType mDSPrecMedType;
		/// <summary>
		/// IDE
		/// </summary>
		private System.ComponentModel.IContainer components;

		/// <summary>
		/// holds the id of the current PrecMedical record selected in the displayed table
		/// </summary>
		private int mCurrentAdminRec = -1;
		/// <summary>
		/// Have data on form changed? This has an accessor and is checked in <see cref="AdministrationModel"/> AdministrationModel
		/// </summary>
		private bool mContentChanged = false;
		/// <summary>
		/// Used when grid is sorted, stup CurrentCellChanged event
		/// </summary>
		private bool mGridIsSorted = false;

		/// <summary>
		/// used to hold the Controller of this triad
		/// </summary>
		internal	AbstractController	mController;

		#endregion //End of Members

		#region Constructors

		public FrmUCAdminMedicalPrecautionary()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

		}

		private void InitView() 
		{
		//	FillLists();
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
			this.CboSearchKind = new System.Windows.Forms.ComboBox();
			this.LblSearchKind = new System.Windows.Forms.Label();
			this.BtnSearch = new System.Windows.Forms.Button();
			this.CboSearchPrecautionaryMedical = new System.Windows.Forms.ComboBox();
			this.LblSearchPrecautionaryMedical = new System.Windows.Forms.Label();
			this.DgrPrecautionaryMedical = new System.Windows.Forms.DataGrid();
			this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
			this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataGridTextBoxColumn3 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataGridTextBoxColumn4 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.LblEdit = new System.Windows.Forms.Label();
			this.PnlEdit = new System.Windows.Forms.Panel();
			this.TxtEditKind = new System.Windows.Forms.TextBox();
			this.LblEditKind = new System.Windows.Forms.Label();
			this.LblEditHelp = new System.Windows.Forms.Label();
			this.TxtEditHelp = new System.Windows.Forms.TextBox();
			this.LblEditPrecautionaryMedical = new System.Windows.Forms.Label();
			this.TxtEditPrecautionaryMedical = new System.Windows.Forms.TextBox();
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
			((System.ComponentModel.ISupportInitialize)(this.DgrPrecautionaryMedical)).BeginInit();
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
			this.PnlSearch.Controls.Add(this.CboSearchKind);
			this.PnlSearch.Controls.Add(this.LblSearchKind);
			this.PnlSearch.Controls.Add(this.BtnSearch);
			this.PnlSearch.Controls.Add(this.CboSearchPrecautionaryMedical);
			this.PnlSearch.Controls.Add(this.LblSearchPrecautionaryMedical);
			this.PnlSearch.Font = new System.Drawing.Font("Arial", 9F);
			this.PnlSearch.Location = new System.Drawing.Point(32, 19);
			this.PnlSearch.Name = "PnlSearch";
			this.PnlSearch.Size = new System.Drawing.Size(928, 64);
			this.PnlSearch.TabIndex = 0;
			// 
			// CboSearchKind
			// 
			this.CboSearchKind.Location = new System.Drawing.Point(56, 22);
			this.CboSearchKind.Name = "CboSearchKind";
			this.CboSearchKind.Size = new System.Drawing.Size(112, 23);
			this.CboSearchKind.TabIndex = 1;
			// 
			// LblSearchKind
			// 
			this.LblSearchKind.Font = new System.Drawing.Font("Arial", 9F);
			this.LblSearchKind.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.LblSearchKind.Location = new System.Drawing.Point(32, 24);
			this.LblSearchKind.Name = "LblSearchKind";
			this.LblSearchKind.Size = new System.Drawing.Size(24, 23);
			this.LblSearchKind.TabIndex = 26;
			this.LblSearchKind.Text = "Art";
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
			// CboSearchPrecautionaryMedical
			// 
			this.CboSearchPrecautionaryMedical.Location = new System.Drawing.Point(344, 22);
			this.CboSearchPrecautionaryMedical.Name = "CboSearchPrecautionaryMedical";
			this.CboSearchPrecautionaryMedical.Size = new System.Drawing.Size(408, 23);
			this.CboSearchPrecautionaryMedical.TabIndex = 2;
			// 
			// LblSearchPrecautionaryMedical
			// 
			this.LblSearchPrecautionaryMedical.Font = new System.Drawing.Font("Arial", 9F);
			this.LblSearchPrecautionaryMedical.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.LblSearchPrecautionaryMedical.Location = new System.Drawing.Point(208, 24);
			this.LblSearchPrecautionaryMedical.Name = "LblSearchPrecautionaryMedical";
			this.LblSearchPrecautionaryMedical.Size = new System.Drawing.Size(136, 23);
			this.LblSearchPrecautionaryMedical.TabIndex = 25;
			this.LblSearchPrecautionaryMedical.Text = "Vorsorgeuntersuchung";
			// 
			// DgrPrecautionaryMedical
			// 
			this.DgrPrecautionaryMedical.AllowSorting = false;
			this.DgrPrecautionaryMedical.CaptionBackColor = System.Drawing.Color.SteelBlue;
			this.DgrPrecautionaryMedical.DataMember = "";
			this.DgrPrecautionaryMedical.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.DgrPrecautionaryMedical.Location = new System.Drawing.Point(32, 99);
			this.DgrPrecautionaryMedical.Name = "DgrPrecautionaryMedical";
			this.DgrPrecautionaryMedical.ReadOnly = true;
			this.DgrPrecautionaryMedical.Size = new System.Drawing.Size(928, 312);
			this.DgrPrecautionaryMedical.TabIndex = 4;
			this.DgrPrecautionaryMedical.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
																												this.dataGridTableStyle1});
			this.DgrPrecautionaryMedical.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DgrPrecautionaryMedical_MouseDown);
			this.DgrPrecautionaryMedical.CurrentCellChanged += new System.EventHandler(this.DgrPrecautionaryMedical_CurrentCellChanged);
			this.DgrPrecautionaryMedical.Enter += new System.EventHandler(this.DgrPrecautionaryMedical_Enter);
			// 
			// dataGridTableStyle1
			// 
			this.dataGridTableStyle1.DataGrid = this.DgrPrecautionaryMedical;
			this.dataGridTableStyle1.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
																												  this.dataGridTextBoxColumn1,
																												  this.dataGridTextBoxColumn2,
																												  this.dataGridTextBoxColumn3,
																												  this.dataGridTextBoxColumn4});
			this.dataGridTableStyle1.HeaderFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.dataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGridTableStyle1.MappingName = "FPASS_PRECMEDTYPE";
			this.dataGridTableStyle1.PreferredColumnWidth = 400;
			this.dataGridTableStyle1.ReadOnly = true;
			// 
			// dataGridTextBoxColumn1
			// 
			this.dataGridTextBoxColumn1.Format = "";
			this.dataGridTextBoxColumn1.FormatInfo = null;
			this.dataGridTextBoxColumn1.HeaderText = "PMTY_ID";
			this.dataGridTextBoxColumn1.MappingName = "PMTY_ID";
			this.dataGridTextBoxColumn1.Width = 0;
			// 
			// dataGridTextBoxColumn2
			// 
			this.dataGridTextBoxColumn2.Format = "";
			this.dataGridTextBoxColumn2.FormatInfo = null;
			this.dataGridTextBoxColumn2.HeaderText = "Art";
			this.dataGridTextBoxColumn2.MappingName = "PMTY_TYPE";
			this.dataGridTextBoxColumn2.Width = 90;
			// 
			// dataGridTextBoxColumn3
			// 
			this.dataGridTextBoxColumn3.Format = "";
			this.dataGridTextBoxColumn3.FormatInfo = null;
			this.dataGridTextBoxColumn3.HeaderText = "Bezeichnung";
			this.dataGridTextBoxColumn3.MappingName = "PMTY_NOTATION";
			this.dataGridTextBoxColumn3.Width = 400;
			// 
			// dataGridTextBoxColumn4
			// 
			this.dataGridTextBoxColumn4.Format = "";
			this.dataGridTextBoxColumn4.FormatInfo = null;
			this.dataGridTextBoxColumn4.HeaderText = "Hilfetext";
			this.dataGridTextBoxColumn4.MappingName = "PMTY_HELPFILE";
			this.dataGridTextBoxColumn4.Width = 400;
			// 
			// LblEdit
			// 
			this.LblEdit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
			this.LblEdit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.LblEdit.Location = new System.Drawing.Point(48, 419);
			this.LblEdit.Name = "LblEdit";
			this.LblEdit.Size = new System.Drawing.Size(56, 16);
			this.LblEdit.TabIndex = 38;
			this.LblEdit.Text = "Eingabe";
			// 
			// PnlEdit
			// 
			this.PnlEdit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PnlEdit.Controls.Add(this.TxtEditKind);
			this.PnlEdit.Controls.Add(this.LblEditKind);
			this.PnlEdit.Controls.Add(this.LblEditHelp);
			this.PnlEdit.Controls.Add(this.TxtEditHelp);
			this.PnlEdit.Controls.Add(this.LblEditPrecautionaryMedical);
			this.PnlEdit.Controls.Add(this.TxtEditPrecautionaryMedical);
			this.PnlEdit.Font = new System.Drawing.Font("Arial", 9F);
			this.PnlEdit.Location = new System.Drawing.Point(32, 427);
			this.PnlEdit.Name = "PnlEdit";
			this.PnlEdit.Size = new System.Drawing.Size(928, 104);
			this.PnlEdit.TabIndex = 5;
			// 
			// TxtEditKind
			// 
			this.TxtEditKind.Enabled = false;
			this.TxtEditKind.Font = new System.Drawing.Font("Arial", 9F);
			this.TxtEditKind.Location = new System.Drawing.Point(184, 14);
			this.TxtEditKind.MaxLength = 10;
			this.TxtEditKind.Name = "TxtEditKind";
			this.TxtEditKind.Size = new System.Drawing.Size(144, 21);
			this.TxtEditKind.TabIndex = 6;
			this.TxtEditKind.Text = "";
			this.TxtEditKind.Enter += new System.EventHandler(this.TxtEditKind_Enter);
			// 
			// LblEditKind
			// 
			this.LblEditKind.Font = new System.Drawing.Font("Arial", 9F);
			this.LblEditKind.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.LblEditKind.Location = new System.Drawing.Point(32, 16);
			this.LblEditKind.Name = "LblEditKind";
			this.LblEditKind.Size = new System.Drawing.Size(24, 16);
			this.LblEditKind.TabIndex = 24;
			this.LblEditKind.Text = "Art";
			// 
			// LblEditHelp
			// 
			this.LblEditHelp.Font = new System.Drawing.Font("Arial", 9F);
			this.LblEditHelp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.LblEditHelp.Location = new System.Drawing.Point(32, 64);
			this.LblEditHelp.Name = "LblEditHelp";
			this.LblEditHelp.Size = new System.Drawing.Size(56, 16);
			this.LblEditHelp.TabIndex = 23;
			this.LblEditHelp.Text = "Hilfetext";
			// 
			// TxtEditHelp
			// 
			this.TxtEditHelp.Enabled = false;
			this.TxtEditHelp.Font = new System.Drawing.Font("Arial", 9F);
			this.TxtEditHelp.Location = new System.Drawing.Point(184, 62);
			this.TxtEditHelp.MaxLength = 100;
			this.TxtEditHelp.Name = "TxtEditHelp";
			this.TxtEditHelp.Size = new System.Drawing.Size(704, 21);
			this.TxtEditHelp.TabIndex = 8;
			this.TxtEditHelp.Text = "";
			this.TxtEditHelp.Enter += new System.EventHandler(this.TxtEditHelp_Enter);
			// 
			// LblEditPrecautionaryMedical
			// 
			this.LblEditPrecautionaryMedical.Font = new System.Drawing.Font("Arial", 9F);
			this.LblEditPrecautionaryMedical.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.LblEditPrecautionaryMedical.Location = new System.Drawing.Point(32, 40);
			this.LblEditPrecautionaryMedical.Name = "LblEditPrecautionaryMedical";
			this.LblEditPrecautionaryMedical.Size = new System.Drawing.Size(136, 16);
			this.LblEditPrecautionaryMedical.TabIndex = 21;
			this.LblEditPrecautionaryMedical.Text = "Vorsorgeuntersuchung";
			// 
			// TxtEditPrecautionaryMedical
			// 
			this.TxtEditPrecautionaryMedical.Enabled = false;
			this.TxtEditPrecautionaryMedical.Font = new System.Drawing.Font("Arial", 9F);
			this.TxtEditPrecautionaryMedical.Location = new System.Drawing.Point(184, 38);
			this.TxtEditPrecautionaryMedical.MaxLength = 100;
			this.TxtEditPrecautionaryMedical.Name = "TxtEditPrecautionaryMedical";
			this.TxtEditPrecautionaryMedical.Size = new System.Drawing.Size(704, 21);
			this.TxtEditPrecautionaryMedical.TabIndex = 7;
			this.TxtEditPrecautionaryMedical.Text = "";
			this.TxtEditPrecautionaryMedical.Enter += new System.EventHandler(this.TxtEditPrecautionaryMedical_Enter);
			// 
			// BtnCancel
			// 
			this.BtnCancel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.BtnCancel.Location = new System.Drawing.Point(736, 552);
			this.BtnCancel.Name = "BtnCancel";
			this.BtnCancel.Size = new System.Drawing.Size(104, 30);
			this.BtnCancel.TabIndex = 12;
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
			this.BtnBackTo.TabIndex = 13;
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
			this.BtnDelete.TabIndex = 11;
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
			this.BtnSave.TabIndex = 10;
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
			this.BtnNew.TabIndex = 9;
			this.BtnNew.Tag = "";
			this.BtnNew.Text = "&Neu";
			this.TooNew.SetToolTip(this.BtnNew, "Legt einen neuen Datensatz an");
			this.BtnNew.Click += new System.EventHandler(this.BtnNew_Click);
			// 
			// FrmUCAdminMedicalPrecautionary
			// 
			this.Controls.Add(this.BtnNew);
			this.Controls.Add(this.BtnCancel);
			this.Controls.Add(this.BtnBackTo);
			this.Controls.Add(this.BtnDelete);
			this.Controls.Add(this.BtnSave);
			this.Controls.Add(this.LblSearch);
			this.Controls.Add(this.PnlSearch);
			this.Controls.Add(this.DgrPrecautionaryMedical);
			this.Controls.Add(this.LblEdit);
			this.Controls.Add(this.PnlEdit);
			this.Name = "FrmUCAdminMedicalPrecautionary";
			this.Size = new System.Drawing.Size(992, 616);
			this.Leave += new System.EventHandler(this.FrmUCAdminMedicalPrecautionary_Leave);
			this.PnlSearch.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.DgrPrecautionaryMedical)).EndInit();
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

		public DSPrecMedType PropDSPrecMedType
		{
			get 
			{
				return mDSPrecMedType;
			}
			set 
			{
				mDSPrecMedType = value;
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
			mDSPrecMedType = new DSPrecMedType();
		}

		internal void FillLists()
		{
			
			//PrecautionaryMedical notation
			FillPrecautionaryMedicalNotation();

			//Kind of PrecautionaryMedical
			FillPrecautionaryMedicalType();
		}


		
		private AdministrationController GetMyController() 
		{
			return (AdministrationController)mController;
		}

		private void FillPrecautionaryMedicalNotation()
		{
			ArrayList precautionaryMedical = new ArrayList(); 
			precautionaryMedical.Add(new LovItem("0", ""));
			precautionaryMedical.AddRange(LovSingleton.GetInstance().GetRootList(null, "FPASS_PRECMEDNOTA", "PMTY_NOTATION") );
			this.CboSearchPrecautionaryMedical.DataSource = precautionaryMedical;
			this.CboSearchPrecautionaryMedical.DisplayMember = "ItemValue";
			this.CboSearchPrecautionaryMedical.ValueMember = "DecId";
		}

		private void FillPrecautionaryMedicalType()
		{
			ArrayList precautionaryMedicalType = new ArrayList(); 
			precautionaryMedicalType.Add(new LovItem("0", ""));
			precautionaryMedicalType.AddRange(LovSingleton.GetInstance().GetRootList(null, "FPASS_PRECMEDTYPE", "PMTY_TYPE"));			
			this.CboSearchKind.DataSource = precautionaryMedicalType;
			this.CboSearchKind.DisplayMember = "ItemValue";
			// Only use string from combobox as search criterium
		}

		/// <summary>
		/// Get the PK id of the current prec medical record
		/// .. that is the data record currently selected in the datagrid
		/// and load record for editing
		/// </summary>
		private void TableNavigated()
		{
			int rowIndex = this.DgrPrecautionaryMedical.CurrentRowIndex;
			if(-1 < rowIndex)
			{
				mCurrentAdminRec= Convert.ToInt32(this.DgrPrecautionaryMedical[rowIndex, 0].ToString());
			}
			GetMyController().HandleEventDgrNavigateTabMedical();
		}

		/// <summary>
		/// Clear textbox in foot of form
		/// </summary>
		public void ClearFields()
		{
			this.TxtEditHelp.Text = "";
			this.TxtEditKind.Text = "";
			this.TxtEditPrecautionaryMedical.Text = "";
			this.TxtEditHelp.DataBindings.Clear();
			this.TxtEditKind.DataBindings.Clear();
			this.TxtEditPrecautionaryMedical.DataBindings.Clear();
			this.TxtEditHelp.Enabled = false;
			this.TxtEditKind.Enabled = false;
			this.TxtEditPrecautionaryMedical.Enabled = false;
			mContentChanged  = false;
			mCurrentAdminRec = -1;
		}

		private void SetAuthorization() 
		{
		//	this.Enabled = UserManagementControl.getInstance().
		//		GetAuthorization(UserManagementControl.ADMIN_MEDICAL_PREC_DIALOG);
		}

		public void RegisterController(AbstractController aAbstractController) 
		{
			this.mController = aAbstractController;
		}

		#endregion // End of Methods

		#region Events
		private void BtnSearch_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventBtnSearchTabPrecMedical();
		}

		/// <summary>
		/// Fired each time a record is selected in  datagrid, allows PK ID(s) of current record to be read
		/// Paint is not used here as it is fired too many times during load ( also during fill datagrid once dataset is filled)
		/// Cannot tell if contents of form have been changed by user
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DgrPrecautionaryMedical_CurrentCellChanged(object sender, System.EventArgs e)
		{
			if ( !mGridIsSorted )
			{
				if ( this.DgrPrecautionaryMedical.VisibleRowCount > 1 )
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
		private void DgrPrecautionaryMedical_Enter(object sender, System.EventArgs e)
		{
			if ( this.DgrPrecautionaryMedical.VisibleRowCount > 0 )
			{
				this.TableNavigated();
			}
		}

		/// <summary>
		/// Event fires when column header is clicked, i.e. when grid is sorted.
		/// Put pointer on first row (index 0)
		/// Discard currently selected record, user has to re-click to select 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DgrPrecautionaryMedical_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			mGridIsSorted = true;
			if ( this.DgrPrecautionaryMedical.VisibleRowCount > 1 )
			{
				DataGrid.HitTestInfo hti  = DgrPrecautionaryMedical.HitTest(e.X, e.Y);
				DataGrid.HitTestType type = hti.Type;
				switch(type)       
				{         
					case System.Windows.Forms.DataGrid.HitTestType.ColumnHeader:
						this.DgrPrecautionaryMedical.CurrentRowIndex = 0;
						ClearFields();
						break;				
				}
			}
			mGridIsSorted = false;
		}


		private void BtnBackTo_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventBackPrecMedical();
		}

		private void BtnSave_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventBtnSaveTabMedical();
		}

		private void BtnDelete_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventBtnDeleteTabMedical();
		}

		private void BtnCancel_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventBtnCancelClick();
		}

		private void BtnNew_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventBtnNewPrecMedClick();
		}


		private void TxtEditHelp_Enter(object sender, System.EventArgs e)
		{
			this.ContentChanged = true;
		}

		private void TxtEditPrecautionaryMedical_Enter(object sender, System.EventArgs e)
		{
			this.ContentChanged = true;
		}

		private void TxtEditKind_Enter(object sender, System.EventArgs e)
		{
			this.ContentChanged = true;
		}

		private void FrmUCAdminMedicalPrecautionary_Leave(object sender, System.EventArgs e)
		{		
			/// <summary>
			/// Fired when this tab (UserControl) is left and user has executed search, edited etc on this tab
			/// Check not DesignMode necessary due to bugs in IDE Designer
			/// </summary>
			/// <param name="sender"></param>
			/// <param name="e"></param>
			/// <exception cref="Degussa.FPASS.Util.Exceptions.ActionCancelledException">ActionCancelledException is thrown
			/// if user cancels prompt to save changes (should stop tab being left but currently doesn't)</exception>
			if ( ! DesignMode ) 
			{
				try
				{
					GetMyController().HandleEventTabPrecMedicalExited();
				}
				catch ( ActionCancelledException )
				{
					// Catch & do nowt
				}
			}		
		}

		#endregion // End of Events


	}
}

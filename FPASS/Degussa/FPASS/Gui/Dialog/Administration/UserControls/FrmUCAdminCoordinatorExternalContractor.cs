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
	/// A FrmUCAdminCoordinatorExternalContractor is one of the usercontrols 
	/// of the FrmAdministration.
	/// FrmUCAdminCoordinatorExternalContractor extends from the System.Windows.Forms.UserControl.
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
	public class FrmUCAdminCoordinatorExternalContractor : System.Windows.Forms.UserControl
	{

		#region Members

		//labels
		internal System.Windows.Forms.Label LblAssignment;
		internal System.Windows.Forms.Label LblAssignmentExternalContractor;
		internal System.Windows.Forms.Label LblSearch;
		internal System.Windows.Forms.Label LblSearchCoordinator;
		internal System.Windows.Forms.Label LbSearchExternalContractor;
		internal System.Windows.Forms.Label LblAssignmentCoordinator;

		//panels
		internal System.Windows.Forms.Panel PnlAssignment;
		internal System.Windows.Forms.Panel PnlSearch;

		//comboboxes
		internal System.Windows.Forms.ComboBox CboSearchExternalContractor;
		internal System.Windows.Forms.ComboBox CboAssignmentExternalContractor;
		internal System.Windows.Forms.ComboBox LikSearchCoordinator;
		internal System.Windows.Forms.ComboBox LikAssignmentCoordinator;

		//buttons
		internal System.Windows.Forms.Button BtnSearch;
		internal System.Windows.Forms.Button BtnSearchExternalContractor;
		internal System.Windows.Forms.Button BtnCancel;
		internal System.Windows.Forms.Button BtnBackTo;
		internal System.Windows.Forms.Button BtnDelete;
		internal System.Windows.Forms.Button BtnSave;
		internal System.Windows.Forms.Button BtnNew;

		//tooltips
		private System.Windows.Forms.ToolTip TooExContractor;
		private System.Windows.Forms.ToolTip TooNew;
		private System.Windows.Forms.ToolTip TooSave;
		private System.Windows.Forms.ToolTip TooDelete;
		private System.Windows.Forms.ToolTip TooCancel;
		private System.Windows.Forms.ToolTip TooBackTo;
		private System.Windows.Forms.ToolTip TooSearch;

		//tables
		internal System.Windows.Forms.DataGrid DgrAssignment;
		private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn2;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn3;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn4;

		//other
		protected DSExcoCoord mDSExcoCoord;
		private System.ComponentModel.IContainer components;

		/// <summary>
		/// PK ID of the Coordinator of the currently selected assign. record
		/// </summary>
		private int mCurrentCoordinatorID = -1;
		/// <summary>
		/// holds the id of the current exco assignm. record selected in the displayed table
		/// </summary>
		private int mCurrentEXCORec = -1;
		/// <summary>
		/// used to hold the Controller of this triad
		/// </summary>
		internal	AbstractController	mController;	
		/// <summary>
		/// Have data on form changed? This has an accessor and is checked in <see cref="AdministrationModel"/> AdministrationModel
		/// </summary>
		private bool mContentChanged = false;
		/// <summary>
		/// Stop CurrentCellChanged firing on grid when grid sorted
		/// </summary>
		private bool mGridIsSorted = true;
		internal System.Windows.Forms.Button btnPopHist;		
		/// <summary>
		/// Need to know which UserControl called FrmSearchExcontractor
		/// </summary>
		private int mSourceCallSearchExco = 2;

		#endregion //End of Members

		#region Constructors

		public FrmUCAdminCoordinatorExternalContractor()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FrmUCAdminCoordinatorExternalContractor));
			this.LblAssignment = new System.Windows.Forms.Label();
			this.PnlAssignment = new System.Windows.Forms.Panel();
			this.LikAssignmentCoordinator = new System.Windows.Forms.ComboBox();
			this.CboAssignmentExternalContractor = new System.Windows.Forms.ComboBox();
			this.LblAssignmentCoordinator = new System.Windows.Forms.Label();
			this.LblAssignmentExternalContractor = new System.Windows.Forms.Label();
			this.DgrAssignment = new System.Windows.Forms.DataGrid();
			this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
			this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataGridTextBoxColumn3 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataGridTextBoxColumn4 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.LblSearch = new System.Windows.Forms.Label();
			this.PnlSearch = new System.Windows.Forms.Panel();
			this.btnPopHist = new System.Windows.Forms.Button();
			this.LikSearchCoordinator = new System.Windows.Forms.ComboBox();
			this.BtnSearchExternalContractor = new System.Windows.Forms.Button();
			this.BtnSearch = new System.Windows.Forms.Button();
			this.CboSearchExternalContractor = new System.Windows.Forms.ComboBox();
			this.LblSearchCoordinator = new System.Windows.Forms.Label();
			this.LbSearchExternalContractor = new System.Windows.Forms.Label();
			this.BtnCancel = new System.Windows.Forms.Button();
			this.BtnBackTo = new System.Windows.Forms.Button();
			this.BtnDelete = new System.Windows.Forms.Button();
			this.BtnSave = new System.Windows.Forms.Button();
			this.BtnNew = new System.Windows.Forms.Button();
			this.TooExContractor = new System.Windows.Forms.ToolTip(this.components);
			this.TooNew = new System.Windows.Forms.ToolTip(this.components);
			this.TooSave = new System.Windows.Forms.ToolTip(this.components);
			this.TooDelete = new System.Windows.Forms.ToolTip(this.components);
			this.TooCancel = new System.Windows.Forms.ToolTip(this.components);
			this.TooBackTo = new System.Windows.Forms.ToolTip(this.components);
			this.TooSearch = new System.Windows.Forms.ToolTip(this.components);
			this.PnlAssignment.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DgrAssignment)).BeginInit();
			this.PnlSearch.SuspendLayout();
			this.SuspendLayout();
			// 
			// LblAssignment
			// 
			this.LblAssignment.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
			this.LblAssignment.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.LblAssignment.Location = new System.Drawing.Point(48, 440);
			this.LblAssignment.Name = "LblAssignment";
			this.LblAssignment.Size = new System.Drawing.Size(72, 16);
			this.LblAssignment.TabIndex = 64;
			this.LblAssignment.Text = "Zuordnung";
			// 
			// PnlAssignment
			// 
			this.PnlAssignment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PnlAssignment.Controls.Add(this.LikAssignmentCoordinator);
			this.PnlAssignment.Controls.Add(this.CboAssignmentExternalContractor);
			this.PnlAssignment.Controls.Add(this.LblAssignmentCoordinator);
			this.PnlAssignment.Controls.Add(this.LblAssignmentExternalContractor);
			this.PnlAssignment.Font = new System.Drawing.Font("Arial", 9F);
			this.PnlAssignment.Location = new System.Drawing.Point(32, 448);
			this.PnlAssignment.Name = "PnlAssignment";
			this.PnlAssignment.Size = new System.Drawing.Size(928, 80);
			this.PnlAssignment.TabIndex = 7;
			// 
			// LikAssignmentCoordinator
			// 
			this.LikAssignmentCoordinator.Enabled = false;
			this.LikAssignmentCoordinator.ItemHeight = 15;
			this.LikAssignmentCoordinator.Location = new System.Drawing.Point(480, 30);
			this.LikAssignmentCoordinator.Name = "LikAssignmentCoordinator";
			this.LikAssignmentCoordinator.Size = new System.Drawing.Size(280, 23);
			this.LikAssignmentCoordinator.TabIndex = 9;
			this.LikAssignmentCoordinator.Enter += new System.EventHandler(this.LikAssignmentCoordinator_Enter);
			// 
			// CboAssignmentExternalContractor
			// 
			this.CboAssignmentExternalContractor.Enabled = false;
			this.CboAssignmentExternalContractor.ItemHeight = 15;
			this.CboAssignmentExternalContractor.Location = new System.Drawing.Point(128, 30);
			this.CboAssignmentExternalContractor.Name = "CboAssignmentExternalContractor";
			this.CboAssignmentExternalContractor.Size = new System.Drawing.Size(180, 23);
			this.CboAssignmentExternalContractor.TabIndex = 8;
			this.CboAssignmentExternalContractor.Enter += new System.EventHandler(this.CboAssignmentExternalContractor_Enter);
			// 
			// LblAssignmentCoordinator
			// 
			this.LblAssignmentCoordinator.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.LblAssignmentCoordinator.Location = new System.Drawing.Point(392, 32);
			this.LblAssignmentCoordinator.Name = "LblAssignmentCoordinator";
			this.LblAssignmentCoordinator.Size = new System.Drawing.Size(112, 21);
			this.LblAssignmentCoordinator.TabIndex = 56;
			this.LblAssignmentCoordinator.Text = "Koordinator";
			// 
			// LblAssignmentExternalContractor
			// 
			this.LblAssignmentExternalContractor.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.LblAssignmentExternalContractor.Location = new System.Drawing.Point(40, 32);
			this.LblAssignmentExternalContractor.Name = "LblAssignmentExternalContractor";
			this.LblAssignmentExternalContractor.Size = new System.Drawing.Size(112, 21);
			this.LblAssignmentExternalContractor.TabIndex = 55;
			this.LblAssignmentExternalContractor.Text = "Fremdfirma";
			// 
			// DgrAssignment
			// 
			this.DgrAssignment.AllowSorting = false;
			this.DgrAssignment.CaptionBackColor = System.Drawing.Color.SteelBlue;
			this.DgrAssignment.DataMember = "";
			this.DgrAssignment.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.DgrAssignment.Location = new System.Drawing.Point(32, 112);
			this.DgrAssignment.Name = "DgrAssignment";
			this.DgrAssignment.ReadOnly = true;
			this.DgrAssignment.Size = new System.Drawing.Size(928, 312);
			this.DgrAssignment.TabIndex = 6;
			this.DgrAssignment.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
																									  this.dataGridTableStyle1});
			this.DgrAssignment.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DgrAssignment_MouseDown);
			this.DgrAssignment.CurrentCellChanged += new System.EventHandler(this.DgrAssignment_CurrentCellChanged);
			this.DgrAssignment.Enter += new System.EventHandler(this.DgrAssignment_Enter);
			// 
			// dataGridTableStyle1
			// 
			this.dataGridTableStyle1.DataGrid = this.DgrAssignment;
			this.dataGridTableStyle1.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
																												  this.dataGridTextBoxColumn1,
																												  this.dataGridTextBoxColumn2,
																												  this.dataGridTextBoxColumn3,
																												  this.dataGridTextBoxColumn4});
			this.dataGridTableStyle1.HeaderFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.dataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGridTableStyle1.MappingName = "FPASS_EXCOECOD";
			this.dataGridTableStyle1.PreferredColumnWidth = 450;
			this.dataGridTableStyle1.ReadOnly = true;
			// 
			// dataGridTextBoxColumn1
			// 
			this.dataGridTextBoxColumn1.Format = "";
			this.dataGridTextBoxColumn1.FormatInfo = null;
			this.dataGridTextBoxColumn1.HeaderText = "ECEC_EXCO_ID";
			this.dataGridTextBoxColumn1.MappingName = "ECEC_EXCO_ID";
			this.dataGridTextBoxColumn1.Width = 0;
			// 
			// dataGridTextBoxColumn2
			// 
			this.dataGridTextBoxColumn2.Format = "";
			this.dataGridTextBoxColumn2.FormatInfo = null;
			this.dataGridTextBoxColumn2.HeaderText = "ECEC_ECOD_ID";
			this.dataGridTextBoxColumn2.MappingName = "ECEC_ECOD_ID";
			this.dataGridTextBoxColumn2.Width = 0;
			// 
			// dataGridTextBoxColumn3
			// 
			this.dataGridTextBoxColumn3.Format = "";
			this.dataGridTextBoxColumn3.FormatInfo = null;
			this.dataGridTextBoxColumn3.HeaderText = "Fremdfirma";
			this.dataGridTextBoxColumn3.MappingName = "EXCO_NAME";
			this.dataGridTextBoxColumn3.Width = 450;
			// 
			// dataGridTextBoxColumn4
			// 
			this.dataGridTextBoxColumn4.Format = "";
			this.dataGridTextBoxColumn4.FormatInfo = null;
			this.dataGridTextBoxColumn4.HeaderText = "Name des Koordinators";
			this.dataGridTextBoxColumn4.MappingName = "UM_BOTHNAMESTEL";
			this.dataGridTextBoxColumn4.Width = 450;
			// 
			// LblSearch
			// 
			this.LblSearch.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
			this.LblSearch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.LblSearch.Location = new System.Drawing.Point(48, 11);
			this.LblSearch.Name = "LblSearch";
			this.LblSearch.Size = new System.Drawing.Size(48, 16);
			this.LblSearch.TabIndex = 63;
			this.LblSearch.Text = "Suche";
			// 
			// PnlSearch
			// 
			this.PnlSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PnlSearch.Controls.Add(this.btnPopHist);
			this.PnlSearch.Controls.Add(this.LikSearchCoordinator);
			this.PnlSearch.Controls.Add(this.BtnSearchExternalContractor);
			this.PnlSearch.Controls.Add(this.BtnSearch);
			this.PnlSearch.Controls.Add(this.CboSearchExternalContractor);
			this.PnlSearch.Controls.Add(this.LblSearchCoordinator);
			this.PnlSearch.Controls.Add(this.LbSearchExternalContractor);
			this.PnlSearch.Font = new System.Drawing.Font("Arial", 9F);
			this.PnlSearch.Location = new System.Drawing.Point(32, 16);
			this.PnlSearch.Name = "PnlSearch";
			this.PnlSearch.Size = new System.Drawing.Size(928, 80);
			this.PnlSearch.TabIndex = 0;
			// 
			// btnPopHist
			// 
			this.btnPopHist.Image = ((System.Drawing.Image)(resources.GetObject("btnPopHist.Image")));
			this.btnPopHist.Location = new System.Drawing.Point(349, 32);
			this.btnPopHist.Name = "btnPopHist";
			this.btnPopHist.Size = new System.Drawing.Size(35, 22);
			this.btnPopHist.TabIndex = 3;
			this.TooExContractor.SetToolTip(this.btnPopHist, "Öffnet die Historie aller Koordinatoren der aktuellen Fremdfirma");
			this.btnPopHist.Click += new System.EventHandler(this.btnPopHist_Click);
			// 
			// LikSearchCoordinator
			// 
			this.LikSearchCoordinator.ItemHeight = 15;
			this.LikSearchCoordinator.Location = new System.Drawing.Point(480, 30);
			this.LikSearchCoordinator.Name = "LikSearchCoordinator";
			this.LikSearchCoordinator.Size = new System.Drawing.Size(280, 23);
			this.LikSearchCoordinator.TabIndex = 4;
			// 
			// BtnSearchExternalContractor
			// 
			this.BtnSearchExternalContractor.Location = new System.Drawing.Point(320, 32);
			this.BtnSearchExternalContractor.Name = "BtnSearchExternalContractor";
			this.BtnSearchExternalContractor.Size = new System.Drawing.Size(22, 22);
			this.BtnSearchExternalContractor.TabIndex = 2;
			this.BtnSearchExternalContractor.Text = "?&F";
			this.TooExContractor.SetToolTip(this.BtnSearchExternalContractor, "Öffnet die Maske Fremdfirmensuche");
			this.BtnSearchExternalContractor.Click += new System.EventHandler(this.BtnSearchExternalContractor_Click);
			// 
			// BtnSearch
			// 
			this.BtnSearch.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.BtnSearch.Location = new System.Drawing.Point(792, 24);
			this.BtnSearch.Name = "BtnSearch";
			this.BtnSearch.Size = new System.Drawing.Size(100, 30);
			this.BtnSearch.TabIndex = 5;
			this.BtnSearch.Text = "&Suche";
			this.TooSearch.SetToolTip(this.BtnSearch, "Löst den Suchvorgang aus");
			this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
			// 
			// CboSearchExternalContractor
			// 
			this.CboSearchExternalContractor.ItemHeight = 15;
			this.CboSearchExternalContractor.Location = new System.Drawing.Point(128, 30);
			this.CboSearchExternalContractor.Name = "CboSearchExternalContractor";
			this.CboSearchExternalContractor.Size = new System.Drawing.Size(180, 23);
			this.CboSearchExternalContractor.TabIndex = 1;
			// 
			// LblSearchCoordinator
			// 
			this.LblSearchCoordinator.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.LblSearchCoordinator.Location = new System.Drawing.Point(405, 32);
			this.LblSearchCoordinator.Name = "LblSearchCoordinator";
			this.LblSearchCoordinator.Size = new System.Drawing.Size(80, 21);
			this.LblSearchCoordinator.TabIndex = 56;
			this.LblSearchCoordinator.Text = "Koordinator";
			// 
			// LbSearchExternalContractor
			// 
			this.LbSearchExternalContractor.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.LbSearchExternalContractor.Location = new System.Drawing.Point(40, 32);
			this.LbSearchExternalContractor.Name = "LbSearchExternalContractor";
			this.LbSearchExternalContractor.Size = new System.Drawing.Size(112, 21);
			this.LbSearchExternalContractor.TabIndex = 55;
			this.LbSearchExternalContractor.Text = "Fremdfirma";
			// 
			// BtnCancel
			// 
			this.BtnCancel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.BtnCancel.Location = new System.Drawing.Point(736, 552);
			this.BtnCancel.Name = "BtnCancel";
			this.BtnCancel.Size = new System.Drawing.Size(104, 30);
			this.BtnCancel.TabIndex = 13;
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
			this.BtnBackTo.TabIndex = 14;
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
			this.BtnDelete.TabIndex = 12;
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
			this.BtnSave.TabIndex = 11;
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
			this.BtnNew.TabIndex = 10;
			this.BtnNew.Tag = "";
			this.BtnNew.Text = "&Neu";
			this.TooNew.SetToolTip(this.BtnNew, "Legt einen neuen Datensatz an");
			this.BtnNew.Click += new System.EventHandler(this.BtnNew_Click);
			// 
			// FrmUCAdminCoordinatorExternalContractor
			// 
			this.Controls.Add(this.LblAssignment);
			this.Controls.Add(this.BtnNew);
			this.Controls.Add(this.BtnCancel);
			this.Controls.Add(this.BtnBackTo);
			this.Controls.Add(this.BtnDelete);
			this.Controls.Add(this.BtnSave);
			this.Controls.Add(this.PnlAssignment);
			this.Controls.Add(this.DgrAssignment);
			this.Controls.Add(this.LblSearch);
			this.Controls.Add(this.PnlSearch);
			this.Name = "FrmUCAdminCoordinatorExternalContractor";
			this.Size = new System.Drawing.Size(992, 616);
			this.Leave += new System.EventHandler(this.FrmUCAdminCoordinatorExternalContractor_Leave);
			this.PnlAssignment.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.DgrAssignment)).EndInit();
			this.PnlSearch.ResumeLayout(false);
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

		public DSExcoCoord PropDSExcoCoord
		{
			get 
			{
				return mDSExcoCoord;
			}
			set 
			{
				mDSExcoCoord = value;
			}
		} 

		// ID of currently selected ExContractor in grid
		public int CurrentCoordinatorID
		{
			get 
			{
				return mCurrentCoordinatorID;
			}
			set 
			{
				mCurrentCoordinatorID = value;
			}
		} 

		// ID of currently selected Coordinator in grid
		public int CurrentEXCORec
		{
			get 
			{
				return mCurrentEXCORec;
			}
			set 
			{
				mCurrentEXCORec = value;
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
		public void CreateDataSet() 
		{
			mDSExcoCoord = new DSExcoCoord();
		}

		/// <summary>
		/// Fills all comboboxes
		/// ExternalContractor search
		/// ExternalContractor assignment
		/// Coordinator search
		/// Coordinator assignment
		/// </summary>
		internal void FillLists() 
		{			
			FillExternalContractorSearch();		
			FillExternalContractorAssignment();
			FillCoordinatorSearch();
			FillCoordinatorAssignment();
		}

		private AdministrationController GetMyController() 
		{
			return (AdministrationController)mController;
		}

		/// <summary>
		/// Fill combobox ExternalContractor search (top of Control)
		/// </summary>
		private void FillExternalContractorSearch()
		{
			ArrayList externalContractorSearch = new ArrayList();
			externalContractorSearch.Add(new LovItem("0",""));
			externalContractorSearch.AddRange( LovSingleton.GetInstance().GetRootList(null, "FPASS_EXCONTRACTOR", "EXCO_NAME") );
			this.CboSearchExternalContractor.DataSource = externalContractorSearch;
			this.CboSearchExternalContractor.DisplayMember = "ItemValue";
			this.CboSearchExternalContractor.ValueMember = "DecId";	
		}

		/// <summary>
		/// Excon (foot of UserControl)
		/// </summary>
		private void FillExternalContractorAssignment()
		{
			ArrayList externalContractor = new ArrayList(); 
			externalContractor.Add(new LovItem("0",""));
			externalContractor.AddRange( LovSingleton.GetInstance().GetRootList(null, "FPASS_EXCONTRACTOR", "EXCO_NAME"));
			this.CboAssignmentExternalContractor.DataSource = externalContractor;
			this.CboAssignmentExternalContractor.DisplayMember = "ItemValue";
			this.CboAssignmentExternalContractor.ValueMember = "DecId";	
		}

		/// <summary>
		/// Coord (top of UserControl)
		/// </summary>
		private void FillCoordinatorSearch()
		{
			ArrayList coordinatorSearch = new ArrayList(); 
			coordinatorSearch.Add(new LovItem("0",""));
			coordinatorSearch.AddRange( LovSingleton.GetInstance().GetRootList(null, "VW_FPASS_COORDINATORS", "VWC_BOTHNAMESTEL") );			
			this.LikSearchCoordinator.DataSource = coordinatorSearch;
			this.LikSearchCoordinator.DisplayMember = "ItemValue";
			this.LikSearchCoordinator.ValueMember = "DecId";
		}

		/// <summary>
		/// Coord assign
		/// </summary>
		private void FillCoordinatorAssignment()
		{
			ArrayList coordinator = new ArrayList(); 
			coordinator.Add(new LovItem("0",""));
			coordinator.AddRange( LovSingleton.GetInstance().GetRootList(null, "VW_FPASS_COORDINATORS", "VWC_BOTHNAMESTEL") );	
			this.LikAssignmentCoordinator.DataSource = coordinator;
			this.LikAssignmentCoordinator.DisplayMember = "ItemValue";
			this.LikAssignmentCoordinator.ValueMember = "DecId";	
		}

		/// <summary>
		/// Get the PK (coord ID and EXCO ID) of the assignment
		/// .. that is the data record currently selected in the datagrid
		/// and load record for editing
		/// </summary>
		private void TableNavigated()
		{
			int rowIndex = this.DgrAssignment.CurrentRowIndex;
			if(-1 < rowIndex)
			{
				mCurrentEXCORec       = Convert.ToInt32(this.DgrAssignment[rowIndex, 0].ToString());
				mCurrentCoordinatorID = Convert.ToInt32(this.DgrAssignment[rowIndex, 1].ToString());
			}
			GetMyController().HandleEventDgrNavigateTabExConCoord();
		}

		/// <summary>
		/// Commented out 02.12.2003
		/// </summary>
		private void SetAuthorization() 
		{
		//	this.Enabled = UserManagementControl.getInstance().
		//		GetAuthorization(UserManagementControl.ADMIN_ASSIGNMENT_COORD_EXCO_DIALOG);
		}

		/// <summary>
		/// Empty comboboxes at foot of form and discard current record
		/// </summary>
		public void ClearFields()
		{		
			CboAssignmentExternalContractor.SelectedIndex = 0;
			LikAssignmentCoordinator.SelectedIndex        = 0;
			CboAssignmentExternalContractor.Enabled = false;
			LikAssignmentCoordinator.Enabled		= false;	
			mContentChanged		  = false;
			mCurrentCoordinatorID = -1;
			mCurrentEXCORec		  = -1;
		}

		/// <summary>
		/// Register Controller with form
		/// </summary>
		/// <param name="aAbstractController"></param>
		public void RegisterController(AbstractController aAbstractController) 
		{
			this.mController = aAbstractController;
		}

		#endregion // End of Methods

		#region Events
			
		/// <summary>
		/// Call dialogue to get a particular excontractor
		/// Need to know which user control called form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnSearchExternalContractor_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventCallFormSearchExContractor(mSourceCallSearchExco);
			GetMyController().HandleEventOpenSearchExternalContractorDialog();		
		}

		/// <summary>
		/// Reaction to button "Suchen"
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnSearch_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventBtnSearchTabExConCoord();	
		}


		/// <summary>
		/// Fired when datagrid is entered, allows PK ID(s) of current record to be read
		/// Idea is, if there is only one record returned then CurrentCellChanged does not fire.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DgrAssignment_Enter(object sender, System.EventArgs e)
		{
			if ( this.DgrAssignment.VisibleRowCount > 0 )
			{
				this.TableNavigated();
			}		
		}

		/// <summary>
		/// Fired when datagrid is entered, allows PK ID(s) of current record to be read
		/// Paint is not used here as it is fired too many times during load ( also during fill datagrid once dataset is filled)
		/// Cannot tell if contents of form have been changed by user
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DgrAssignment_CurrentCellChanged(object sender, System.EventArgs e)
		{
			if ( !mGridIsSorted )
			{
				if ( this.DgrAssignment.VisibleRowCount > 1 )
				{
					this.TableNavigated();
				}
			}
		}

		/// <summary>
		/// Event fires when column header is clicked, i.e. when grid is sorted.
		/// Put pointer on first row (index 0)
		/// Discard currently selected record, user has to re-click to select 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DgrAssignment_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			mGridIsSorted = true;
			if ( this.DgrAssignment.VisibleRowCount > 1 )
			{
				DataGrid.HitTestInfo hti  = DgrAssignment.HitTest(e.X, e.Y);
				DataGrid.HitTestType type = hti.Type;
				switch(type)       
				{         
					case System.Windows.Forms.DataGrid.HitTestType.ColumnHeader:
						this.DgrAssignment.CurrentRowIndex = 0;
						ClearFields();
						break;				
				}
			}
			mGridIsSorted = false;	
		}
		

		/// <summary>
		/// Return to form that called this one (Button "Zurück")
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnBackTo_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventBackBtnExcoCoord();
		}

		/// <summary>
		/// Save current assignment
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnSave_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventBtnSaveTabCoordExternalCon();
		}

		/// <summary>
		/// Del current assignment.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnDelete_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventBtnDeleteTabCoordExternalCon();
		}

		/// <summary>
		/// Cancel changes
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnCancel_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventBtnCancelClick();
		}

		/// <summary>
		/// Button "Neu": enable textfields etc
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnNew_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventBtnNewExcoCoordClick();
		}

		/// <summary>
		/// Fired when this tab (UserControl) is left and user has executed search, edited etc on this tab
		/// Check not DesignMode necessary due to bugs in IDE Designer
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <exception cref="Degussa.FPASS.Util.Exceptions.ActionCancelledException">ActionCancelledException is thrown
		/// if user cancels prompt to save changes (should stop tab being left but currently doesn't)</exception>
		private void FrmUCAdminCoordinatorExternalContractor_Leave(object sender, System.EventArgs e)
		{
			if ( ! DesignMode ) 
			{
				try 
				{
					GetMyController().HandleEventTabExcoCoordExited();
				}
				catch ( ActionCancelledException )
				{
					// Swallow
				}
			}
		}

		/// <summary>
		/// Have the form data changed? Assume they have if combobox entered => new value selected
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CboAssignmentExternalContractor_Enter(object sender, System.EventArgs e)
		{
			this.ContentChanged = true;
		}

		/// <summary>
		/// Have the form data changed? Assume they have if combobox entered => new value selected
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LikAssignmentCoordinator_Enter(object sender, System.EventArgs e)
		{
			this.ContentChanged = true;
		}

		/// <summary>
		/// New 30.04.04: Opens small popup showing hist of coordinators assigned to current exco
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnPopHist_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventPopExcoCoordHist();
		}

		#endregion // End of Events
	}
}

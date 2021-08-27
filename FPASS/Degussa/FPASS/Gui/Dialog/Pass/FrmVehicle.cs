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
using de.pta.Component.Errorhandling;
using de.pta.Component.DbAccess;

using Degussa.FPASS.Gui.Dialog;
using Degussa.FPASS.Util.ListOfValues;
using Degussa.FPASS.Util.UserManagement;
using Degussa.FPASS.Bo.Pass;
using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.Util.Exceptions;

namespace Degussa.FPASS.Gui.Dialog.Pass
{
	/// <summary>
	/// A FrmVehicle is the view of the MVC-triad VehicleModel,
	/// SummaryVehicle and FrmVehicle.
	/// FrmVehicle extends from the FPASSBaseView.
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
	public class FrmVehicle : Degussa.FPASS.Gui.FPASSBaseView
	{
		#region Members

		//panels
		internal System.Windows.Forms.Panel PnlVehicleEntranceShort;
		internal System.Windows.Forms.Panel PnlVehicleEntranceShortControl;
		internal System.Windows.Forms.Label PnlVehicleEntranceShortReceived;
		internal System.Windows.Forms.Panel PnlVehicleEntranceLong;
		internal System.Windows.Forms.Panel PnlVehicleEntranceLongControl;
		internal System.Windows.Forms.Panel PnlSearch;
		internal System.Windows.Forms.Panel PnlSearchVehicleNotRecievedNo;
		internal System.Windows.Forms.Panel PnlSearchVehicleNotRecievedYes;
		internal System.Windows.Forms.Panel PnlSearchVehicleNo;
		internal System.Windows.Forms.Panel PnlSearchVehicleYes;
		internal System.Windows.Forms.Panel pnlSearchVehicleLongNoExecuted;
		internal System.Windows.Forms.Panel pnlSearchVehicleShortNoExecuted;

		//labels
		internal System.Windows.Forms.Label LblVehicleEntranceShortReceivedBy;
		internal System.Windows.Forms.Label LblVehicleEntranceShortReceivedOn;
		internal System.Windows.Forms.Label LblVehicleEntranceShort;
		internal System.Windows.Forms.Label LblVehicleEntranceLongReceivedBy;
		internal System.Windows.Forms.Label LblVehicleEntranceLongReceivedOn;
		internal System.Windows.Forms.Label LblVehicleEntranceLong;
		internal System.Windows.Forms.Label LblVehicleEntranceLongReceived;
		internal System.Windows.Forms.Label LblSearch;
		internal System.Windows.Forms.Label LblSearchVehicleNotRecieved;
		internal System.Windows.Forms.Label LblSearchVehicle;
		internal System.Windows.Forms.Label LblMask;
		internal System.Windows.Forms.Label LblVehicleAccessNotExecute;

		//textboxes
		internal System.Windows.Forms.TextBox TxtVehicleEntranceShortReceivedBy;
		internal System.Windows.Forms.TextBox TxtVehicleEntranceLongReceivedBy;

		//datetimepicker
		internal System.Windows.Forms.DateTimePicker DatVehicleEntranceShortReceivedOn;
		internal System.Windows.Forms.DateTimePicker DatVehicleEntranceLongReceivedOn;

		//radiobuttons
		internal System.Windows.Forms.RadioButton RbtVehicleEntranceShort;
		internal System.Windows.Forms.RadioButton RbtVehicleEntranceLongNo;
		internal System.Windows.Forms.RadioButton RbtVehicleEntranceLongYes;
		internal System.Windows.Forms.RadioButton RbtVehicleEntranceShortReceivedNo;
		internal System.Windows.Forms.RadioButton RbtVehicleEntranceShortReceivedYes;
		internal System.Windows.Forms.RadioButton RbtVehicleEntranceLong;
		internal System.Windows.Forms.RadioButton RbtSearchVehicleNotRecievedNo;
		internal System.Windows.Forms.RadioButton RbtSearchVehicleNotRecievedYes;
		internal System.Windows.Forms.RadioButton RbtSearchVehicleNo;
		internal System.Windows.Forms.RadioButton RbtSearchVehicleYes;
		internal System.Windows.Forms.RadioButton rbtSearchVehicleLongNoExecuted;
		internal System.Windows.Forms.RadioButton rbtSearchVehicleShortNoExecuted;

		//buttons
		internal System.Windows.Forms.Button BtnSummary;
		internal System.Windows.Forms.Button BtnSave;
		internal System.Windows.Forms.Button BtnSearch;
		internal System.Windows.Forms.Button BtnClearMask;
		internal System.Windows.Forms.Button BtnAcceptedShort;
		internal System.Windows.Forms.Button BtnAcceptedLong;
		internal System.Windows.Forms.Button BtnNotAccepted;

		//tooltips
		private System.Windows.Forms.ToolTip TooSearch;
		private System.Windows.Forms.ToolTip TooClearMask;
		private System.Windows.Forms.ToolTip TooNoVehicleEntrance;
		private System.Windows.Forms.ToolTip TooEntranceLong;
		private System.Windows.Forms.ToolTip TooBackTo;
		private System.Windows.Forms.ToolTip TooEntranceShort;
		private System.Windows.Forms.ToolTip TooSave;

		//tables
		internal System.Windows.Forms.DataGrid DgrVehicle;
		internal System.Windows.Forms.DataGridTableStyle DgrTableStyleVehicleAccess;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxCwrID;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxSurName;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxFirstName;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxExcon;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxVehicleShortDesire;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxVehicleShortAllowed;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxVehicleLongDesire;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxVehicleLongAllowed;
		/// <summary>
		/// How many records returned from DB: used as success flag
		/// </summary>
		private decimal mRecordsFound;

		private System.ComponentModel.IContainer components = null;
		/// <summary>
		/// current selected coworker ID
		/// </summary>
		private decimal mCwrID = -1;
		/// <summary>
		/// selected rowcount
		/// </summary>
		private int mRowCount = 0;
		internal System.Windows.Forms.Panel pnlSearchVehicleLongAccepted;
		internal System.Windows.Forms.RadioButton rbtSearchVehicleLongAccepted;
		internal System.Windows.Forms.Label LblVehicleAccessAccepted;
		internal System.Windows.Forms.Panel pnlSearchVehicleShortAccepted;
		internal System.Windows.Forms.RadioButton rbtSearchVehicleShortAccepted;

		/// <summary>
		/// 04.03.2004: Used to determine if grid being sorted, don't fire event CellChanged
		/// </summary>
		private bool mGridIsSorted = false;

		#endregion //End of Members

		#region Constructors

		public FrmVehicle()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			Initialize();

		}

		#endregion //End of Constructors

		#region Initialization

		/// <summary>
		/// Initializes the members.
		/// </summary>
		private void Initialize()
		{
			MnuFunction.Enabled = false;
			MnuReports.Enabled = false;
		}	

		#endregion //End of Initialization

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.DgrVehicle = new System.Windows.Forms.DataGrid();
            this.DgrTableStyleVehicleAccess = new System.Windows.Forms.DataGridTableStyle();
            this.DgrTextBoxCwrID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxSurName = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxFirstName = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxExcon = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxVehicleShortDesire = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxVehicleShortAllowed = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxVehicleLongDesire = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxVehicleLongAllowed = new System.Windows.Forms.DataGridTextBoxColumn();
            this.PnlSearch = new System.Windows.Forms.Panel();
            this.pnlSearchVehicleLongAccepted = new System.Windows.Forms.Panel();
            this.rbtSearchVehicleLongAccepted = new System.Windows.Forms.RadioButton();
            this.LblVehicleAccessAccepted = new System.Windows.Forms.Label();
            this.pnlSearchVehicleShortAccepted = new System.Windows.Forms.Panel();
            this.rbtSearchVehicleShortAccepted = new System.Windows.Forms.RadioButton();
            this.pnlSearchVehicleLongNoExecuted = new System.Windows.Forms.Panel();
            this.rbtSearchVehicleLongNoExecuted = new System.Windows.Forms.RadioButton();
            this.LblVehicleAccessNotExecute = new System.Windows.Forms.Label();
            this.BtnClearMask = new System.Windows.Forms.Button();
            this.BtnSearch = new System.Windows.Forms.Button();
            this.PnlSearchVehicleNotRecievedNo = new System.Windows.Forms.Panel();
            this.RbtSearchVehicleNotRecievedNo = new System.Windows.Forms.RadioButton();
            this.PnlSearchVehicleNo = new System.Windows.Forms.Panel();
            this.RbtSearchVehicleNo = new System.Windows.Forms.RadioButton();
            this.LblSearchVehicleNotRecieved = new System.Windows.Forms.Label();
            this.LblSearchVehicle = new System.Windows.Forms.Label();
            this.PnlSearchVehicleYes = new System.Windows.Forms.Panel();
            this.RbtSearchVehicleYes = new System.Windows.Forms.RadioButton();
            this.PnlSearchVehicleNotRecievedYes = new System.Windows.Forms.Panel();
            this.RbtSearchVehicleNotRecievedYes = new System.Windows.Forms.RadioButton();
            this.pnlSearchVehicleShortNoExecuted = new System.Windows.Forms.Panel();
            this.rbtSearchVehicleShortNoExecuted = new System.Windows.Forms.RadioButton();
            this.LblSearch = new System.Windows.Forms.Label();
            this.PnlVehicleEntranceShort = new System.Windows.Forms.Panel();
            this.PnlVehicleEntranceShortControl = new System.Windows.Forms.Panel();
            this.RbtVehicleEntranceShortReceivedNo = new System.Windows.Forms.RadioButton();
            this.RbtVehicleEntranceShortReceivedYes = new System.Windows.Forms.RadioButton();
            this.DatVehicleEntranceShortReceivedOn = new System.Windows.Forms.DateTimePicker();
            this.LblVehicleEntranceShortReceivedBy = new System.Windows.Forms.Label();
            this.TxtVehicleEntranceShortReceivedBy = new System.Windows.Forms.TextBox();
            this.LblVehicleEntranceShortReceivedOn = new System.Windows.Forms.Label();
            this.LblVehicleEntranceShort = new System.Windows.Forms.Label();
            this.RbtVehicleEntranceShort = new System.Windows.Forms.RadioButton();
            this.PnlVehicleEntranceShortReceived = new System.Windows.Forms.Label();
            this.PnlVehicleEntranceLong = new System.Windows.Forms.Panel();
            this.PnlVehicleEntranceLongControl = new System.Windows.Forms.Panel();
            this.RbtVehicleEntranceLongNo = new System.Windows.Forms.RadioButton();
            this.RbtVehicleEntranceLongYes = new System.Windows.Forms.RadioButton();
            this.DatVehicleEntranceLongReceivedOn = new System.Windows.Forms.DateTimePicker();
            this.LblVehicleEntranceLongReceivedBy = new System.Windows.Forms.Label();
            this.TxtVehicleEntranceLongReceivedBy = new System.Windows.Forms.TextBox();
            this.LblVehicleEntranceLongReceivedOn = new System.Windows.Forms.Label();
            this.LblVehicleEntranceLong = new System.Windows.Forms.Label();
            this.RbtVehicleEntranceLong = new System.Windows.Forms.RadioButton();
            this.LblVehicleEntranceLongReceived = new System.Windows.Forms.Label();
            this.BtnSummary = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.LblMask = new System.Windows.Forms.Label();
            this.BtnAcceptedShort = new System.Windows.Forms.Button();
            this.BtnAcceptedLong = new System.Windows.Forms.Button();
            this.BtnNotAccepted = new System.Windows.Forms.Button();
            this.TooSearch = new System.Windows.Forms.ToolTip(this.components);
            this.TooClearMask = new System.Windows.Forms.ToolTip(this.components);
            this.TooNoVehicleEntrance = new System.Windows.Forms.ToolTip(this.components);
            this.TooEntranceLong = new System.Windows.Forms.ToolTip(this.components);
            this.TooBackTo = new System.Windows.Forms.ToolTip(this.components);
            this.TooEntranceShort = new System.Windows.Forms.ToolTip(this.components);
            this.TooSave = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.SbpMessage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sbpenvironment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgrVehicle)).BeginInit();
            this.PnlSearch.SuspendLayout();
            this.pnlSearchVehicleLongAccepted.SuspendLayout();
            this.pnlSearchVehicleShortAccepted.SuspendLayout();
            this.pnlSearchVehicleLongNoExecuted.SuspendLayout();
            this.PnlSearchVehicleNotRecievedNo.SuspendLayout();
            this.PnlSearchVehicleNo.SuspendLayout();
            this.PnlSearchVehicleYes.SuspendLayout();
            this.PnlSearchVehicleNotRecievedYes.SuspendLayout();
            this.pnlSearchVehicleShortNoExecuted.SuspendLayout();
            this.PnlVehicleEntranceShort.SuspendLayout();
            this.PnlVehicleEntranceShortControl.SuspendLayout();
            this.PnlVehicleEntranceLong.SuspendLayout();
            this.PnlVehicleEntranceLongControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // DgrVehicle
            // 
            this.DgrVehicle.CaptionBackColor = System.Drawing.Color.SteelBlue;
            this.DgrVehicle.CaptionText = "KFZ-Zutritt mit zugehörigen Fremdfirmenmitarbeitern";
            this.DgrVehicle.DataMember = "";
            this.DgrVehicle.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.DgrVehicle.Location = new System.Drawing.Point(13, 167);
            this.DgrVehicle.Name = "DgrVehicle";
            this.DgrVehicle.ReadOnly = true;
            this.DgrVehicle.Size = new System.Drawing.Size(1246, 531);
            this.DgrVehicle.TabIndex = 15;
            this.DgrVehicle.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.DgrTableStyleVehicleAccess});
            this.DgrVehicle.CurrentCellChanged += new System.EventHandler(this.DgrVehicle_CurrentCellChanged);
            this.DgrVehicle.Click += new System.EventHandler(this.DgrVehicle_Click);
            this.DgrVehicle.Paint += new System.Windows.Forms.PaintEventHandler(this.DgrVehicle_Paint);
            // 
            // DgrTableStyleVehicleAccess
            // 
            this.DgrTableStyleVehicleAccess.DataGrid = this.DgrVehicle;
            this.DgrTableStyleVehicleAccess.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.DgrTextBoxCwrID,
            this.DgrTextBoxSurName,
            this.DgrTextBoxFirstName,
            this.DgrTextBoxExcon,
            this.DgrTextBoxVehicleShortDesire,
            this.DgrTextBoxVehicleShortAllowed,
            this.DgrTextBoxVehicleLongDesire,
            this.DgrTextBoxVehicleLongAllowed});
            this.DgrTableStyleVehicleAccess.HeaderFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DgrTableStyleVehicleAccess.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.DgrTableStyleVehicleAccess.MappingName = "VehicleTable";
            this.DgrTableStyleVehicleAccess.ReadOnly = true;
            // 
            // DgrTextBoxCwrID
            // 
            this.DgrTextBoxCwrID.Format = "";
            this.DgrTextBoxCwrID.FormatInfo = null;
            this.DgrTextBoxCwrID.HeaderText = "CWRID";
            this.DgrTextBoxCwrID.MappingName = "CwrID";
            this.DgrTextBoxCwrID.NullText = "";
            this.DgrTextBoxCwrID.Width = 0;
            // 
            // DgrTextBoxSurName
            // 
            this.DgrTextBoxSurName.Format = "";
            this.DgrTextBoxSurName.FormatInfo = null;
            this.DgrTextBoxSurName.HeaderText = "Nachname";
            this.DgrTextBoxSurName.MappingName = "SurName";
            this.DgrTextBoxSurName.NullText = "";
            this.DgrTextBoxSurName.Width = 160;
            // 
            // DgrTextBoxFirstName
            // 
            this.DgrTextBoxFirstName.Format = "";
            this.DgrTextBoxFirstName.FormatInfo = null;
            this.DgrTextBoxFirstName.HeaderText = "Vorname";
            this.DgrTextBoxFirstName.MappingName = "FirstName";
            this.DgrTextBoxFirstName.NullText = "";
            this.DgrTextBoxFirstName.Width = 160;
            // 
            // DgrTextBoxExcon
            // 
            this.DgrTextBoxExcon.Format = "";
            this.DgrTextBoxExcon.FormatInfo = null;
            this.DgrTextBoxExcon.HeaderText = "Fremdfirma";
            this.DgrTextBoxExcon.MappingName = "ExcoName";
            this.DgrTextBoxExcon.NullText = "";
            this.DgrTextBoxExcon.Width = 220;
            // 
            // DgrTextBoxVehicleShortDesire
            // 
            this.DgrTextBoxVehicleShortDesire.Format = "";
            this.DgrTextBoxVehicleShortDesire.FormatInfo = null;
            this.DgrTextBoxVehicleShortDesire.HeaderText = "Einfahrt Kurz (gewünscht)";
            this.DgrTextBoxVehicleShortDesire.MappingName = "VehicleShortDesire";
            this.DgrTextBoxVehicleShortDesire.NullText = "";
            this.DgrTextBoxVehicleShortDesire.Width = 150;
            // 
            // DgrTextBoxVehicleShortAllowed
            // 
            this.DgrTextBoxVehicleShortAllowed.Format = "";
            this.DgrTextBoxVehicleShortAllowed.FormatInfo = null;
            this.DgrTextBoxVehicleShortAllowed.HeaderText = "Einfahrt Kurz (gewährt)";
            this.DgrTextBoxVehicleShortAllowed.MappingName = "VehicleShortAllowed";
            this.DgrTextBoxVehicleShortAllowed.NullText = "test";
            this.DgrTextBoxVehicleShortAllowed.Width = 150;
            // 
            // DgrTextBoxVehicleLongDesire
            // 
            this.DgrTextBoxVehicleLongDesire.Format = "";
            this.DgrTextBoxVehicleLongDesire.FormatInfo = null;
            this.DgrTextBoxVehicleLongDesire.HeaderText = "Einfahrt Lang (gewünscht)";
            this.DgrTextBoxVehicleLongDesire.MappingName = "VehicleLongDesire";
            this.DgrTextBoxVehicleLongDesire.NullText = "";
            this.DgrTextBoxVehicleLongDesire.Width = 150;
            // 
            // DgrTextBoxVehicleLongAllowed
            // 
            this.DgrTextBoxVehicleLongAllowed.Format = "";
            this.DgrTextBoxVehicleLongAllowed.FormatInfo = null;
            this.DgrTextBoxVehicleLongAllowed.HeaderText = "Einfahrt Lang (gewährt)";
            this.DgrTextBoxVehicleLongAllowed.MappingName = "VehicleLongAllowed";
            this.DgrTextBoxVehicleLongAllowed.NullText = "";
            this.DgrTextBoxVehicleLongAllowed.Width = 150;
            // 
            // PnlSearch
            // 
            this.PnlSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlSearch.Controls.Add(this.pnlSearchVehicleLongAccepted);
            this.PnlSearch.Controls.Add(this.LblVehicleAccessAccepted);
            this.PnlSearch.Controls.Add(this.pnlSearchVehicleShortAccepted);
            this.PnlSearch.Controls.Add(this.pnlSearchVehicleLongNoExecuted);
            this.PnlSearch.Controls.Add(this.LblVehicleAccessNotExecute);
            this.PnlSearch.Controls.Add(this.BtnClearMask);
            this.PnlSearch.Controls.Add(this.BtnSearch);
            this.PnlSearch.Controls.Add(this.PnlSearchVehicleNotRecievedNo);
            this.PnlSearch.Controls.Add(this.PnlSearchVehicleNo);
            this.PnlSearch.Controls.Add(this.LblSearchVehicleNotRecieved);
            this.PnlSearch.Controls.Add(this.LblSearchVehicle);
            this.PnlSearch.Controls.Add(this.PnlSearchVehicleYes);
            this.PnlSearch.Controls.Add(this.PnlSearchVehicleNotRecievedYes);
            this.PnlSearch.Controls.Add(this.pnlSearchVehicleShortNoExecuted);
            this.PnlSearch.Location = new System.Drawing.Point(13, 56);
            this.PnlSearch.Name = "PnlSearch";
            this.PnlSearch.Size = new System.Drawing.Size(1246, 97);
            this.PnlSearch.TabIndex = 3;
            // 
            // pnlSearchVehicleLongAccepted
            // 
            this.pnlSearchVehicleLongAccepted.Controls.Add(this.rbtSearchVehicleLongAccepted);
            this.pnlSearchVehicleLongAccepted.Location = new System.Drawing.Point(704, 38);
            this.pnlSearchVehicleLongAccepted.Name = "pnlSearchVehicleLongAccepted";
            this.pnlSearchVehicleLongAccepted.Size = new System.Drawing.Size(72, 32);
            this.pnlSearchVehicleLongAccepted.TabIndex = 17;
            // 
            // rbtSearchVehicleLongAccepted
            // 
            this.rbtSearchVehicleLongAccepted.Location = new System.Drawing.Point(16, 5);
            this.rbtSearchVehicleLongAccepted.Name = "rbtSearchVehicleLongAccepted";
            this.rbtSearchVehicleLongAccepted.Size = new System.Drawing.Size(56, 24);
            this.rbtSearchVehicleLongAccepted.TabIndex = 12;
            this.rbtSearchVehicleLongAccepted.Text = "lang";
            // 
            // LblVehicleAccessAccepted
            // 
            this.LblVehicleAccessAccepted.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblVehicleAccessAccepted.Location = new System.Drawing.Point(656, 18);
            this.LblVehicleAccessAccepted.Name = "LblVehicleAccessAccepted";
            this.LblVehicleAccessAccepted.Size = new System.Drawing.Size(120, 24);
            this.LblVehicleAccessAccepted.TabIndex = 15;
            this.LblVehicleAccessAccepted.Text = "Kfz - akzeptiert";
            // 
            // pnlSearchVehicleShortAccepted
            // 
            this.pnlSearchVehicleShortAccepted.Controls.Add(this.rbtSearchVehicleShortAccepted);
            this.pnlSearchVehicleShortAccepted.Location = new System.Drawing.Point(640, 38);
            this.pnlSearchVehicleShortAccepted.Name = "pnlSearchVehicleShortAccepted";
            this.pnlSearchVehicleShortAccepted.Size = new System.Drawing.Size(64, 32);
            this.pnlSearchVehicleShortAccepted.TabIndex = 16;
            // 
            // rbtSearchVehicleShortAccepted
            // 
            this.rbtSearchVehicleShortAccepted.Location = new System.Drawing.Point(16, 5);
            this.rbtSearchVehicleShortAccepted.Name = "rbtSearchVehicleShortAccepted";
            this.rbtSearchVehicleShortAccepted.Size = new System.Drawing.Size(56, 24);
            this.rbtSearchVehicleShortAccepted.TabIndex = 10;
            this.rbtSearchVehicleShortAccepted.Text = "kurz";
            // 
            // pnlSearchVehicleLongNoExecuted
            // 
            this.pnlSearchVehicleLongNoExecuted.Controls.Add(this.rbtSearchVehicleLongNoExecuted);
            this.pnlSearchVehicleLongNoExecuted.Location = new System.Drawing.Point(512, 38);
            this.pnlSearchVehicleLongNoExecuted.Name = "pnlSearchVehicleLongNoExecuted";
            this.pnlSearchVehicleLongNoExecuted.Size = new System.Drawing.Size(72, 32);
            this.pnlSearchVehicleLongNoExecuted.TabIndex = 11;
            // 
            // rbtSearchVehicleLongNoExecuted
            // 
            this.rbtSearchVehicleLongNoExecuted.Location = new System.Drawing.Point(16, 5);
            this.rbtSearchVehicleLongNoExecuted.Name = "rbtSearchVehicleLongNoExecuted";
            this.rbtSearchVehicleLongNoExecuted.Size = new System.Drawing.Size(56, 24);
            this.rbtSearchVehicleLongNoExecuted.TabIndex = 12;
            this.rbtSearchVehicleLongNoExecuted.Text = "lang";
            // 
            // LblVehicleAccessNotExecute
            // 
            this.LblVehicleAccessNotExecute.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblVehicleAccessNotExecute.Location = new System.Drawing.Point(464, 18);
            this.LblVehicleAccessNotExecute.Name = "LblVehicleAccessNotExecute";
            this.LblVehicleAccessNotExecute.Size = new System.Drawing.Size(120, 24);
            this.LblVehicleAccessNotExecute.TabIndex = 8;
            this.LblVehicleAccessNotExecute.Text = "Kfz - nicht bearbeitet";
            // 
            // BtnClearMask
            // 
            this.BtnClearMask.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClearMask.Location = new System.Drawing.Point(1089, 53);
            this.BtnClearMask.Name = "BtnClearMask";
            this.BtnClearMask.Size = new System.Drawing.Size(136, 30);
            this.BtnClearMask.TabIndex = 14;
            this.BtnClearMask.Text = "&Maske leeren";
            this.TooClearMask.SetToolTip(this.BtnClearMask, "Löscht alle bereits eingegebenen Suchkriterien");
            this.BtnClearMask.Click += new System.EventHandler(this.BtnClearMask_Click);
            // 
            // BtnSearch
            // 
            this.BtnSearch.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSearch.Location = new System.Drawing.Point(1089, 13);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(136, 30);
            this.BtnSearch.TabIndex = 13;
            this.BtnSearch.Text = "&Suchen";
            this.TooSearch.SetToolTip(this.BtnSearch, "Löst den Suchvorgang aus");
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // PnlSearchVehicleNotRecievedNo
            // 
            this.PnlSearchVehicleNotRecievedNo.Controls.Add(this.RbtSearchVehicleNotRecievedNo);
            this.PnlSearchVehicleNotRecievedNo.Location = new System.Drawing.Point(312, 38);
            this.PnlSearchVehicleNotRecievedNo.Name = "PnlSearchVehicleNotRecievedNo";
            this.PnlSearchVehicleNotRecievedNo.Size = new System.Drawing.Size(72, 32);
            this.PnlSearchVehicleNotRecievedNo.TabIndex = 7;
            // 
            // RbtSearchVehicleNotRecievedNo
            // 
            this.RbtSearchVehicleNotRecievedNo.Location = new System.Drawing.Point(16, 5);
            this.RbtSearchVehicleNotRecievedNo.Name = "RbtSearchVehicleNotRecievedNo";
            this.RbtSearchVehicleNotRecievedNo.Size = new System.Drawing.Size(56, 24);
            this.RbtSearchVehicleNotRecievedNo.TabIndex = 8;
            this.RbtSearchVehicleNotRecievedNo.Text = "lang";
            // 
            // PnlSearchVehicleNo
            // 
            this.PnlSearchVehicleNo.Controls.Add(this.RbtSearchVehicleNo);
            this.PnlSearchVehicleNo.Location = new System.Drawing.Point(112, 38);
            this.PnlSearchVehicleNo.Name = "PnlSearchVehicleNo";
            this.PnlSearchVehicleNo.Size = new System.Drawing.Size(72, 32);
            this.PnlSearchVehicleNo.TabIndex = 4;
            // 
            // RbtSearchVehicleNo
            // 
            this.RbtSearchVehicleNo.Location = new System.Drawing.Point(16, 5);
            this.RbtSearchVehicleNo.Name = "RbtSearchVehicleNo";
            this.RbtSearchVehicleNo.Size = new System.Drawing.Size(56, 24);
            this.RbtSearchVehicleNo.TabIndex = 4;
            this.RbtSearchVehicleNo.Text = "lang";
            // 
            // LblSearchVehicleNotRecieved
            // 
            this.LblSearchVehicleNotRecieved.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSearchVehicleNotRecieved.Location = new System.Drawing.Point(264, 18);
            this.LblSearchVehicleNotRecieved.Name = "LblSearchVehicleNotRecieved";
            this.LblSearchVehicleNotRecieved.Size = new System.Drawing.Size(112, 24);
            this.LblSearchVehicleNotRecieved.TabIndex = 2;
            this.LblSearchVehicleNotRecieved.Text = "Kfz - abgelehnt";
            // 
            // LblSearchVehicle
            // 
            this.LblSearchVehicle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSearchVehicle.Location = new System.Drawing.Point(64, 18);
            this.LblSearchVehicle.Name = "LblSearchVehicle";
            this.LblSearchVehicle.Size = new System.Drawing.Size(80, 24);
            this.LblSearchVehicle.TabIndex = 1;
            this.LblSearchVehicle.Text = "Kfz - Wunsch";
            // 
            // PnlSearchVehicleYes
            // 
            this.PnlSearchVehicleYes.Controls.Add(this.RbtSearchVehicleYes);
            this.PnlSearchVehicleYes.Location = new System.Drawing.Point(48, 38);
            this.PnlSearchVehicleYes.Name = "PnlSearchVehicleYes";
            this.PnlSearchVehicleYes.Size = new System.Drawing.Size(64, 32);
            this.PnlSearchVehicleYes.TabIndex = 1;
            // 
            // RbtSearchVehicleYes
            // 
            this.RbtSearchVehicleYes.Location = new System.Drawing.Point(16, 5);
            this.RbtSearchVehicleYes.Name = "RbtSearchVehicleYes";
            this.RbtSearchVehicleYes.Size = new System.Drawing.Size(56, 24);
            this.RbtSearchVehicleYes.TabIndex = 2;
            this.RbtSearchVehicleYes.Text = "kurz";
            // 
            // PnlSearchVehicleNotRecievedYes
            // 
            this.PnlSearchVehicleNotRecievedYes.Controls.Add(this.RbtSearchVehicleNotRecievedYes);
            this.PnlSearchVehicleNotRecievedYes.Location = new System.Drawing.Point(248, 38);
            this.PnlSearchVehicleNotRecievedYes.Name = "PnlSearchVehicleNotRecievedYes";
            this.PnlSearchVehicleNotRecievedYes.Size = new System.Drawing.Size(64, 32);
            this.PnlSearchVehicleNotRecievedYes.TabIndex = 5;
            // 
            // RbtSearchVehicleNotRecievedYes
            // 
            this.RbtSearchVehicleNotRecievedYes.Location = new System.Drawing.Point(16, 5);
            this.RbtSearchVehicleNotRecievedYes.Name = "RbtSearchVehicleNotRecievedYes";
            this.RbtSearchVehicleNotRecievedYes.Size = new System.Drawing.Size(56, 24);
            this.RbtSearchVehicleNotRecievedYes.TabIndex = 6;
            this.RbtSearchVehicleNotRecievedYes.Text = "kurz";
            // 
            // pnlSearchVehicleShortNoExecuted
            // 
            this.pnlSearchVehicleShortNoExecuted.Controls.Add(this.rbtSearchVehicleShortNoExecuted);
            this.pnlSearchVehicleShortNoExecuted.Location = new System.Drawing.Point(448, 38);
            this.pnlSearchVehicleShortNoExecuted.Name = "pnlSearchVehicleShortNoExecuted";
            this.pnlSearchVehicleShortNoExecuted.Size = new System.Drawing.Size(64, 32);
            this.pnlSearchVehicleShortNoExecuted.TabIndex = 9;
            // 
            // rbtSearchVehicleShortNoExecuted
            // 
            this.rbtSearchVehicleShortNoExecuted.Location = new System.Drawing.Point(16, 5);
            this.rbtSearchVehicleShortNoExecuted.Name = "rbtSearchVehicleShortNoExecuted";
            this.rbtSearchVehicleShortNoExecuted.Size = new System.Drawing.Size(56, 24);
            this.rbtSearchVehicleShortNoExecuted.TabIndex = 10;
            this.rbtSearchVehicleShortNoExecuted.Text = "kurz";
            // 
            // LblSearch
            // 
            this.LblSearch.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSearch.Location = new System.Drawing.Point(32, 48);
            this.LblSearch.Name = "LblSearch";
            this.LblSearch.Size = new System.Drawing.Size(48, 16);
            this.LblSearch.TabIndex = 9;
            this.LblSearch.Text = "Suche";
            // 
            // PnlVehicleEntranceShort
            // 
            this.PnlVehicleEntranceShort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlVehicleEntranceShort.Controls.Add(this.PnlVehicleEntranceShortControl);
            this.PnlVehicleEntranceShort.Controls.Add(this.DatVehicleEntranceShortReceivedOn);
            this.PnlVehicleEntranceShort.Controls.Add(this.LblVehicleEntranceShortReceivedBy);
            this.PnlVehicleEntranceShort.Controls.Add(this.TxtVehicleEntranceShortReceivedBy);
            this.PnlVehicleEntranceShort.Controls.Add(this.LblVehicleEntranceShortReceivedOn);
            this.PnlVehicleEntranceShort.Controls.Add(this.LblVehicleEntranceShort);
            this.PnlVehicleEntranceShort.Controls.Add(this.RbtVehicleEntranceShort);
            this.PnlVehicleEntranceShort.Controls.Add(this.PnlVehicleEntranceShortReceived);
            this.PnlVehicleEntranceShort.Location = new System.Drawing.Point(15, 715);
            this.PnlVehicleEntranceShort.Name = "PnlVehicleEntranceShort";
            this.PnlVehicleEntranceShort.Size = new System.Drawing.Size(610, 127);
            this.PnlVehicleEntranceShort.TabIndex = 16;
            // 
            // PnlVehicleEntranceShortControl
            // 
            this.PnlVehicleEntranceShortControl.Controls.Add(this.RbtVehicleEntranceShortReceivedNo);
            this.PnlVehicleEntranceShortControl.Controls.Add(this.RbtVehicleEntranceShortReceivedYes);
            this.PnlVehicleEntranceShortControl.Location = new System.Drawing.Point(339, 38);
            this.PnlVehicleEntranceShortControl.Name = "PnlVehicleEntranceShortControl";
            this.PnlVehicleEntranceShortControl.Size = new System.Drawing.Size(240, 32);
            this.PnlVehicleEntranceShortControl.TabIndex = 18;
            // 
            // RbtVehicleEntranceShortReceivedNo
            // 
            this.RbtVehicleEntranceShortReceivedNo.Checked = true;
            this.RbtVehicleEntranceShortReceivedNo.Enabled = false;
            this.RbtVehicleEntranceShortReceivedNo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtVehicleEntranceShortReceivedNo.Location = new System.Drawing.Point(120, 5);
            this.RbtVehicleEntranceShortReceivedNo.Name = "RbtVehicleEntranceShortReceivedNo";
            this.RbtVehicleEntranceShortReceivedNo.Size = new System.Drawing.Size(96, 24);
            this.RbtVehicleEntranceShortReceivedNo.TabIndex = 20;
            this.RbtVehicleEntranceShortReceivedNo.TabStop = true;
            this.RbtVehicleEntranceShortReceivedNo.Text = "Abgelehnt";
            this.RbtVehicleEntranceShortReceivedNo.Click += new System.EventHandler(this.RbtVehicleEntranceShortReceivedNo_Click);
            // 
            // RbtVehicleEntranceShortReceivedYes
            // 
            this.RbtVehicleEntranceShortReceivedYes.Enabled = false;
            this.RbtVehicleEntranceShortReceivedYes.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtVehicleEntranceShortReceivedYes.Location = new System.Drawing.Point(16, 5);
            this.RbtVehicleEntranceShortReceivedYes.Name = "RbtVehicleEntranceShortReceivedYes";
            this.RbtVehicleEntranceShortReceivedYes.Size = new System.Drawing.Size(88, 24);
            this.RbtVehicleEntranceShortReceivedYes.TabIndex = 19;
            this.RbtVehicleEntranceShortReceivedYes.Text = "Genehmigt";
            this.RbtVehicleEntranceShortReceivedYes.Click += new System.EventHandler(this.RbtVehicleEntranceShortReceivedYes_Click);
            // 
            // DatVehicleEntranceShortReceivedOn
            // 
            this.DatVehicleEntranceShortReceivedOn.CustomFormat = "dd.MM.yyyy";
            this.DatVehicleEntranceShortReceivedOn.Enabled = false;
            this.DatVehicleEntranceShortReceivedOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DatVehicleEntranceShortReceivedOn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatVehicleEntranceShortReceivedOn.Location = new System.Drawing.Point(172, 87);
            this.DatVehicleEntranceShortReceivedOn.Name = "DatVehicleEntranceShortReceivedOn";
            this.DatVehicleEntranceShortReceivedOn.Size = new System.Drawing.Size(88, 21);
            this.DatVehicleEntranceShortReceivedOn.TabIndex = 21;
            this.DatVehicleEntranceShortReceivedOn.Value = new System.DateTime(2004, 3, 11, 0, 0, 0, 0);
            // 
            // LblVehicleEntranceShortReceivedBy
            // 
            this.LblVehicleEntranceShortReceivedBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblVehicleEntranceShortReceivedBy.Location = new System.Drawing.Point(310, 86);
            this.LblVehicleEntranceShortReceivedBy.Name = "LblVehicleEntranceShortReceivedBy";
            this.LblVehicleEntranceShortReceivedBy.Size = new System.Drawing.Size(40, 16);
            this.LblVehicleEntranceShortReceivedBy.TabIndex = 100;
            this.LblVehicleEntranceShortReceivedBy.Text = "durch";
            // 
            // TxtVehicleEntranceShortReceivedBy
            // 
            this.TxtVehicleEntranceShortReceivedBy.Enabled = false;
            this.TxtVehicleEntranceShortReceivedBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtVehicleEntranceShortReceivedBy.Location = new System.Drawing.Point(357, 84);
            this.TxtVehicleEntranceShortReceivedBy.Name = "TxtVehicleEntranceShortReceivedBy";
            this.TxtVehicleEntranceShortReceivedBy.ReadOnly = true;
            this.TxtVehicleEntranceShortReceivedBy.Size = new System.Drawing.Size(210, 21);
            this.TxtVehicleEntranceShortReceivedBy.TabIndex = 22;
            // 
            // LblVehicleEntranceShortReceivedOn
            // 
            this.LblVehicleEntranceShortReceivedOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblVehicleEntranceShortReceivedOn.Location = new System.Drawing.Point(131, 86);
            this.LblVehicleEntranceShortReceivedOn.Name = "LblVehicleEntranceShortReceivedOn";
            this.LblVehicleEntranceShortReceivedOn.Size = new System.Drawing.Size(35, 19);
            this.LblVehicleEntranceShortReceivedOn.TabIndex = 97;
            this.LblVehicleEntranceShortReceivedOn.Text = "am";
            // 
            // LblVehicleEntranceShort
            // 
            this.LblVehicleEntranceShort.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblVehicleEntranceShort.Location = new System.Drawing.Point(43, 14);
            this.LblVehicleEntranceShort.Name = "LblVehicleEntranceShort";
            this.LblVehicleEntranceShort.Size = new System.Drawing.Size(248, 15);
            this.LblVehicleEntranceShort.TabIndex = 96;
            this.LblVehicleEntranceShort.Text = "Gewünscht Kfz-Einfahrt kurz ";
            // 
            // RbtVehicleEntranceShort
            // 
            this.RbtVehicleEntranceShort.Enabled = false;
            this.RbtVehicleEntranceShort.Location = new System.Drawing.Point(16, 14);
            this.RbtVehicleEntranceShort.Name = "RbtVehicleEntranceShort";
            this.RbtVehicleEntranceShort.Size = new System.Drawing.Size(24, 16);
            this.RbtVehicleEntranceShort.TabIndex = 17;
            // 
            // PnlVehicleEntranceShortReceived
            // 
            this.PnlVehicleEntranceShortReceived.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PnlVehicleEntranceShortReceived.Location = new System.Drawing.Point(131, 46);
            this.PnlVehicleEntranceShortReceived.Name = "PnlVehicleEntranceShortReceived";
            this.PnlVehicleEntranceShortReceived.Size = new System.Drawing.Size(200, 16);
            this.PnlVehicleEntranceShortReceived.TabIndex = 94;
            this.PnlVehicleEntranceShortReceived.Text = "Kfz-Einfahrt kurz gewährt";
            // 
            // PnlVehicleEntranceLong
            // 
            this.PnlVehicleEntranceLong.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlVehicleEntranceLong.Controls.Add(this.PnlVehicleEntranceLongControl);
            this.PnlVehicleEntranceLong.Controls.Add(this.DatVehicleEntranceLongReceivedOn);
            this.PnlVehicleEntranceLong.Controls.Add(this.LblVehicleEntranceLongReceivedBy);
            this.PnlVehicleEntranceLong.Controls.Add(this.TxtVehicleEntranceLongReceivedBy);
            this.PnlVehicleEntranceLong.Controls.Add(this.LblVehicleEntranceLongReceivedOn);
            this.PnlVehicleEntranceLong.Controls.Add(this.LblVehicleEntranceLong);
            this.PnlVehicleEntranceLong.Controls.Add(this.RbtVehicleEntranceLong);
            this.PnlVehicleEntranceLong.Controls.Add(this.LblVehicleEntranceLongReceived);
            this.PnlVehicleEntranceLong.Location = new System.Drawing.Point(636, 715);
            this.PnlVehicleEntranceLong.Name = "PnlVehicleEntranceLong";
            this.PnlVehicleEntranceLong.Size = new System.Drawing.Size(610, 127);
            this.PnlVehicleEntranceLong.TabIndex = 23;
            // 
            // PnlVehicleEntranceLongControl
            // 
            this.PnlVehicleEntranceLongControl.Controls.Add(this.RbtVehicleEntranceLongNo);
            this.PnlVehicleEntranceLongControl.Controls.Add(this.RbtVehicleEntranceLongYes);
            this.PnlVehicleEntranceLongControl.Location = new System.Drawing.Point(339, 38);
            this.PnlVehicleEntranceLongControl.Name = "PnlVehicleEntranceLongControl";
            this.PnlVehicleEntranceLongControl.Size = new System.Drawing.Size(224, 32);
            this.PnlVehicleEntranceLongControl.TabIndex = 25;
            // 
            // RbtVehicleEntranceLongNo
            // 
            this.RbtVehicleEntranceLongNo.Checked = true;
            this.RbtVehicleEntranceLongNo.Enabled = false;
            this.RbtVehicleEntranceLongNo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtVehicleEntranceLongNo.Location = new System.Drawing.Point(104, 5);
            this.RbtVehicleEntranceLongNo.Name = "RbtVehicleEntranceLongNo";
            this.RbtVehicleEntranceLongNo.Size = new System.Drawing.Size(96, 24);
            this.RbtVehicleEntranceLongNo.TabIndex = 27;
            this.RbtVehicleEntranceLongNo.TabStop = true;
            this.RbtVehicleEntranceLongNo.Text = "Abgelehnt";
            this.RbtVehicleEntranceLongNo.Click += new System.EventHandler(this.RbtVehicleEntranceLongNo_Click);
            // 
            // RbtVehicleEntranceLongYes
            // 
            this.RbtVehicleEntranceLongYes.Enabled = false;
            this.RbtVehicleEntranceLongYes.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtVehicleEntranceLongYes.Location = new System.Drawing.Point(0, 5);
            this.RbtVehicleEntranceLongYes.Name = "RbtVehicleEntranceLongYes";
            this.RbtVehicleEntranceLongYes.Size = new System.Drawing.Size(88, 24);
            this.RbtVehicleEntranceLongYes.TabIndex = 26;
            this.RbtVehicleEntranceLongYes.Text = "Genehmigt";
            this.RbtVehicleEntranceLongYes.Click += new System.EventHandler(this.RbtVehicleEntranceLongYes_Click);
            // 
            // DatVehicleEntranceLongReceivedOn
            // 
            this.DatVehicleEntranceLongReceivedOn.CustomFormat = "dd.MM.yyyy";
            this.DatVehicleEntranceLongReceivedOn.Enabled = false;
            this.DatVehicleEntranceLongReceivedOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DatVehicleEntranceLongReceivedOn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatVehicleEntranceLongReceivedOn.Location = new System.Drawing.Point(172, 87);
            this.DatVehicleEntranceLongReceivedOn.Name = "DatVehicleEntranceLongReceivedOn";
            this.DatVehicleEntranceLongReceivedOn.Size = new System.Drawing.Size(88, 21);
            this.DatVehicleEntranceLongReceivedOn.TabIndex = 28;
            this.DatVehicleEntranceLongReceivedOn.Value = new System.DateTime(2004, 3, 11, 0, 0, 0, 0);
            // 
            // LblVehicleEntranceLongReceivedBy
            // 
            this.LblVehicleEntranceLongReceivedBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblVehicleEntranceLongReceivedBy.Location = new System.Drawing.Point(310, 86);
            this.LblVehicleEntranceLongReceivedBy.Name = "LblVehicleEntranceLongReceivedBy";
            this.LblVehicleEntranceLongReceivedBy.Size = new System.Drawing.Size(40, 16);
            this.LblVehicleEntranceLongReceivedBy.TabIndex = 104;
            this.LblVehicleEntranceLongReceivedBy.Text = "durch";
            // 
            // TxtVehicleEntranceLongReceivedBy
            // 
            this.TxtVehicleEntranceLongReceivedBy.Enabled = false;
            this.TxtVehicleEntranceLongReceivedBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtVehicleEntranceLongReceivedBy.Location = new System.Drawing.Point(357, 84);
            this.TxtVehicleEntranceLongReceivedBy.Name = "TxtVehicleEntranceLongReceivedBy";
            this.TxtVehicleEntranceLongReceivedBy.ReadOnly = true;
            this.TxtVehicleEntranceLongReceivedBy.Size = new System.Drawing.Size(210, 21);
            this.TxtVehicleEntranceLongReceivedBy.TabIndex = 29;
            // 
            // LblVehicleEntranceLongReceivedOn
            // 
            this.LblVehicleEntranceLongReceivedOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblVehicleEntranceLongReceivedOn.Location = new System.Drawing.Point(131, 89);
            this.LblVehicleEntranceLongReceivedOn.Name = "LblVehicleEntranceLongReceivedOn";
            this.LblVehicleEntranceLongReceivedOn.Size = new System.Drawing.Size(33, 16);
            this.LblVehicleEntranceLongReceivedOn.TabIndex = 101;
            this.LblVehicleEntranceLongReceivedOn.Text = "am";
            // 
            // LblVehicleEntranceLong
            // 
            this.LblVehicleEntranceLong.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblVehicleEntranceLong.Location = new System.Drawing.Point(43, 14);
            this.LblVehicleEntranceLong.Name = "LblVehicleEntranceLong";
            this.LblVehicleEntranceLong.Size = new System.Drawing.Size(280, 15);
            this.LblVehicleEntranceLong.TabIndex = 98;
            this.LblVehicleEntranceLong.Text = "Gewünscht Kfz-Einfahrt lang ";
            // 
            // RbtVehicleEntranceLong
            // 
            this.RbtVehicleEntranceLong.Enabled = false;
            this.RbtVehicleEntranceLong.Location = new System.Drawing.Point(16, 14);
            this.RbtVehicleEntranceLong.Name = "RbtVehicleEntranceLong";
            this.RbtVehicleEntranceLong.Size = new System.Drawing.Size(24, 16);
            this.RbtVehicleEntranceLong.TabIndex = 24;
            // 
            // LblVehicleEntranceLongReceived
            // 
            this.LblVehicleEntranceLongReceived.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblVehicleEntranceLongReceived.Location = new System.Drawing.Point(131, 46);
            this.LblVehicleEntranceLongReceived.Name = "LblVehicleEntranceLongReceived";
            this.LblVehicleEntranceLongReceived.Size = new System.Drawing.Size(168, 16);
            this.LblVehicleEntranceLongReceived.TabIndex = 94;
            this.LblVehicleEntranceLongReceived.Text = "Kfz-Einfahrt lang gewährt";
            // 
            // BtnSummary
            // 
            this.BtnSummary.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSummary.Location = new System.Drawing.Point(1093, 860);
            this.BtnSummary.Name = "BtnSummary";
            this.BtnSummary.Size = new System.Drawing.Size(165, 30);
            this.BtnSummary.TabIndex = 34;
            this.BtnSummary.Text = "&Zurück";
            this.TooBackTo.SetToolTip(this.BtnSummary, "Zurück zur Übersichts");
            this.BtnSummary.Click += new System.EventHandler(this.BtnSummary_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Enabled = false;
            this.BtnSave.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Location = new System.Drawing.Point(917, 860);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(165, 30);
            this.BtnSave.TabIndex = 33;
            this.BtnSave.Tag = "";
            this.BtnSave.Text = "Speiche&rn";
            this.TooSave.SetToolTip(this.BtnSave, "Speichern");
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // LblMask
            // 
            this.LblMask.BackColor = System.Drawing.Color.White;
            this.LblMask.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMask.Location = new System.Drawing.Point(489, 8);
            this.LblMask.Name = "LblMask";
            this.LblMask.Size = new System.Drawing.Size(268, 32);
            this.LblMask.TabIndex = 76;
            this.LblMask.Text = "FPASS - Kfz-Zutritt";
            // 
            // BtnAcceptedShort
            // 
            this.BtnAcceptedShort.Enabled = false;
            this.BtnAcceptedShort.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAcceptedShort.Location = new System.Drawing.Point(733, 860);
            this.BtnAcceptedShort.Name = "BtnAcceptedShort";
            this.BtnAcceptedShort.Size = new System.Drawing.Size(175, 30);
            this.BtnAcceptedShort.TabIndex = 32;
            this.BtnAcceptedShort.Tag = "";
            this.BtnAcceptedShort.Text = "Einfahrt &Kurz akzeptieren";
            this.TooEntranceShort.SetToolTip(this.BtnAcceptedShort, "Kfz-Einfahrt Kurz akzeptieren");
            this.BtnAcceptedShort.Click += new System.EventHandler(this.BtnAcceptedShort_Click);
            // 
            // BtnAcceptedLong
            // 
            this.BtnAcceptedLong.Enabled = false;
            this.BtnAcceptedLong.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAcceptedLong.Location = new System.Drawing.Point(549, 860);
            this.BtnAcceptedLong.Name = "BtnAcceptedLong";
            this.BtnAcceptedLong.Size = new System.Drawing.Size(175, 30);
            this.BtnAcceptedLong.TabIndex = 31;
            this.BtnAcceptedLong.Tag = "";
            this.BtnAcceptedLong.Text = "Einfahrt &Lang akzeptieren";
            this.TooEntranceLong.SetToolTip(this.BtnAcceptedLong, "Kfz-Einfahrt Lang akzeptieren");
            this.BtnAcceptedLong.Click += new System.EventHandler(this.BtnAcceptedLong_Click);
            // 
            // BtnNotAccepted
            // 
            this.BtnNotAccepted.Enabled = false;
            this.BtnNotAccepted.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnNotAccepted.Location = new System.Drawing.Point(373, 860);
            this.BtnNotAccepted.Name = "BtnNotAccepted";
            this.BtnNotAccepted.Size = new System.Drawing.Size(165, 30);
            this.BtnNotAccepted.TabIndex = 30;
            this.BtnNotAccepted.Tag = "";
            this.BtnNotAccepted.Text = "Einfahrt &ablehnen";
            this.TooNoVehicleEntrance.SetToolTip(this.BtnNotAccepted, "Gewünschte Kfz-Einfahrt ablehnen");
            this.BtnNotAccepted.Click += new System.EventHandler(this.BtnNotAccepted_Click);
            // 
            // FrmVehicle
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(1272, 953);
            this.Controls.Add(this.DgrVehicle);
            this.Controls.Add(this.BtnNotAccepted);
            this.Controls.Add(this.BtnAcceptedLong);
            this.Controls.Add(this.BtnAcceptedShort);
            this.Controls.Add(this.LblMask);
            this.Controls.Add(this.BtnSummary);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.PnlVehicleEntranceLong);
            this.Controls.Add(this.PnlVehicleEntranceShort);
            this.Controls.Add(this.LblSearch);
            this.Controls.Add(this.PnlSearch);
            this.Name = "FrmVehicle";
            this.Text = "FPASS - Kfz-Zutritt";
            this.Controls.SetChildIndex(this.PnlSearch, 0);
            this.Controls.SetChildIndex(this.LblSearch, 0);
            this.Controls.SetChildIndex(this.LblBaseHead, 0);
            this.Controls.SetChildIndex(this.StbBase, 0);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            this.Controls.SetChildIndex(this.PnlVehicleEntranceShort, 0);
            this.Controls.SetChildIndex(this.PnlVehicleEntranceLong, 0);
            this.Controls.SetChildIndex(this.BtnSave, 0);
            this.Controls.SetChildIndex(this.BtnSummary, 0);
            this.Controls.SetChildIndex(this.LblMask, 0);
            this.Controls.SetChildIndex(this.BtnAcceptedShort, 0);
            this.Controls.SetChildIndex(this.BtnAcceptedLong, 0);
            this.Controls.SetChildIndex(this.BtnNotAccepted, 0);
            this.Controls.SetChildIndex(this.DgrVehicle, 0);
            ((System.ComponentModel.ISupportInitialize)(this.SbpMessage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sbpenvironment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgrVehicle)).EndInit();
            this.PnlSearch.ResumeLayout(false);
            this.pnlSearchVehicleLongAccepted.ResumeLayout(false);
            this.pnlSearchVehicleShortAccepted.ResumeLayout(false);
            this.pnlSearchVehicleLongNoExecuted.ResumeLayout(false);
            this.PnlSearchVehicleNotRecievedNo.ResumeLayout(false);
            this.PnlSearchVehicleNo.ResumeLayout(false);
            this.PnlSearchVehicleYes.ResumeLayout(false);
            this.PnlSearchVehicleNotRecievedYes.ResumeLayout(false);
            this.pnlSearchVehicleShortNoExecuted.ResumeLayout(false);
            this.PnlVehicleEntranceShort.ResumeLayout(false);
            this.PnlVehicleEntranceShort.PerformLayout();
            this.PnlVehicleEntranceShortControl.ResumeLayout(false);
            this.PnlVehicleEntranceLong.ResumeLayout(false);
            this.PnlVehicleEntranceLong.PerformLayout();
            this.PnlVehicleEntranceLongControl.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		#region Accessors 

		/// <summary>
		/// Getter and setter: can tell form how many records found
		/// </summary>
		public decimal RecordsFound 
		{
			get 
			{
				return mRecordsFound;
			}
			set 
			{
				mRecordsFound = value;
			}
		} 

		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// Empty 19.01.04
		/// </summary>
		private void SetAuthorization() 
		{

		}

		/// <summary>
		/// Clears search fields on leaving form
		/// </summary>
		internal override void PreClose()
		{
			ClearMask();
		}

		
		/// <summary>
		/// Empty 19.01.04
		/// </summary>
		internal override void PreShow() 
		{
		}

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
		/// Get controller belonging to current triad
		/// </summary>
		/// <returns></returns>
		private VehicleController GetMyController() 
		{
			return ((VehicleController)mController);
		}

		
		/// <summary>
		/// Clears searchfields then all fields on mask and sets them to default, disables buttons at foot of form
		/// </summary>
		private void ClearMask()
		{
			((VehicleController) mController).HandelEventEnableFields();

			this.RbtSearchVehicleNo.Checked = false;
			this.RbtSearchVehicleNotRecievedNo.Checked = false;
			this.RbtSearchVehicleNotRecievedYes.Checked = false;
			this.RbtSearchVehicleYes.Checked = false;
			this.rbtSearchVehicleShortNoExecuted.Checked = false;
			this.rbtSearchVehicleLongNoExecuted.Checked = false;
			this.rbtSearchVehicleLongAccepted.Checked = false;
			this.rbtSearchVehicleShortAccepted.Checked = false;
			this.DgrVehicle.DataSource = null;
			this.BtnAcceptedLong.Enabled =false;
			this.BtnAcceptedShort.Enabled = false;
			this.BtnNotAccepted.Enabled = false;
			ClearFields();
			
		}

		
		/// <summary>
		/// Clears datagrid and all fields on mask and sets them to default, disables buttons at foot of form
		/// </summary>
		private void ClearFields()
		{
			this.BtnSave.Enabled = false;
			this.RbtVehicleEntranceShort.Checked = false;
			this.RbtVehicleEntranceShortReceivedYes.Checked = false;
			this.RbtVehicleEntranceShortReceivedNo.Checked = true;
			// this.DatVehicleEntranceShortReceivedOn.Value = Convert.ToDateTime("06.10.2003");
			this.DatVehicleEntranceShortReceivedOn.Value = DateTime.Now;
			this.TxtVehicleEntranceShortReceivedBy.Text = String.Empty;
			this.RbtVehicleEntranceLong.Checked = false;
			this.RbtVehicleEntranceLongYes.Checked = false;
			this.RbtVehicleEntranceLongNo.Checked = true;
			// this.DatVehicleEntranceLongReceivedOn.Value = Convert.ToDateTime("06.10.2003");
			this.DatVehicleEntranceLongReceivedOn.Value = DateTime.Now;
			this.TxtVehicleEntranceLongReceivedBy.Text = String.Empty;

		}

		//controls clicked datasets in table
		
		/// <summary>
		/// Called when datagrid navigated: get PK/s of selected coworker records (0th column in grid)
		/// Check if more then one row is selected. If this is the case, fields for short and long acces will be disabled 
		/// </summary>
		public void TableClick()
		{
			mRowCount = 0;
			int rowIndex = this.DgrVehicle.CurrentRowIndex;
			if(-1 < rowIndex)
			{
				mCwrID = Convert.ToDecimal(this.DgrVehicle[rowIndex, 0].ToString());
				 
				for (int j = 0; j < mRecordsFound; j++)
				{
					if ( DgrVehicle.IsSelected(j) )
					{
						mRowCount= mRowCount + 1;
					}
				}
				if (mRowCount > 1)
				{
					((VehicleController) mController).HandelEventDisableFields();
					ClearFields();
				}
				else
				{
					((VehicleController) mController).HandelEventFillVehicle(mCwrID);
				}
			}
		}

		/// <summary>
		/// Checks whether the mouse position is currently in a column header
		/// of the specified DataGrid, invoking a sorting event on the grid.
		/// This method should be invoked only in an event of the specified 
		/// DataGrid, like the click event.
		/// </summary>
		/// <param name="pGrid">The checked DataGrid.</param>
		/// <returns>TRUE if the HitTest method of the specified DataGrid returns
		/// HitTestType "ColumnHeader", FALSE otherwise.</returns>
		public bool MouseInColumnHeader(DataGrid pGrid, int pX, int pY)
		{
			DataGrid.HitTestInfo hti = pGrid.HitTest(pGrid.PointToClient(new Point(pX, pY)));
			DataGrid.HitTestType type = hti.Type;
			switch(type)       
			{         
				case System.Windows.Forms.DataGrid.HitTestType.ColumnHeader:   
					return true;
				default:            
					return false;
			}
		}
	
		#endregion

		#region Events

		
		/// <summary>
		/// Button "Suchen"
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnSearch_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventBtnSearchVehicleAccess();
		}

		
		/// <summary>
		/// Button "Maske leeren"
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnClearMask_Click(object sender, System.EventArgs e)
		{
			ClearMask();
		}

		
		/// <summary>
		/// Button "Speichern"
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <exception cref="de.pta.Component.Errorhandling.UIWarningException">if no coworker selected</exception>
		private void BtnSave_Click(object sender, System.EventArgs e)
		{
			try 
			{
				if (mCwrID > 0)
				{
					GetMyController().HandelEventUpdateVehicle(mCwrID);
				}
				else
				{
					throw new UIWarningException(MessageSingleton.GetInstance().
						GetMessage(MessageSingleton.ACCESS_VALID));
				}
			} 
			catch ( UIWarningException uwe ) 
			{
				ExceptionProcessor.GetInstance().Process(uwe);
			}
		}

		
		/// <summary>
		/// Button "Zurück"
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnSummary_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleCloseDialog();
		}

		
		/// <summary>
		/// Button "Einfahrt kurz akzeptieren"
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <exception cref="de.pta.Component.Errorhandling.UIWarningException">if no coworker selected</exception>
		private void BtnAcceptedShort_Click(object sender, System.EventArgs e)
		{
			try 
			{
				if (mCwrID > 0)
				{
					GetMyController().HandelEventUpdateVehicleSort(mCwrID);
				}
				else
				{
					throw new UIWarningException(MessageSingleton.GetInstance().
						GetMessage(MessageSingleton.ACCESS_VALID));
				}
			} 
			catch ( UIWarningException uwe ) 
			{
				ExceptionProcessor.GetInstance().Process(uwe);
			}
			
		}

		
		/// <summary>
		/// Button "Ablehnen"
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <exception cref="de.pta.Component.Errorhandling.UIWarningException">if no coworker selected</exception>
		private void BtnNotAccepted_Click(object sender, System.EventArgs e)
		{
			try 
			{
				if (mCwrID > 0)
				{
					GetMyController().HandelEventUpdateNotAcceptedVehicle(mCwrID);
				}
				else
				{
					throw new UIWarningException(MessageSingleton.GetInstance().
						GetMessage(MessageSingleton.ACCESS_VALID));
				}
			} 
			catch ( UIWarningException uwe ) 
			{
				ExceptionProcessor.GetInstance().Process(uwe);
			}
		}

		
		/// <summary>
		/// Button "Einfahrt lang akzeptieren" 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <exception cref="de.pta.Component.Errorhandling.UIWarningException">if no coworker selected</exception>
		private void BtnAcceptedLong_Click(object sender, System.EventArgs e)
		{
			try 
			{
				if (mCwrID > 0)
				{
					GetMyController().HandelEventUpdateVehicleLong(mCwrID);
				}
				else
				{
					throw new UIWarningException(MessageSingleton.GetInstance().
						GetMessage(MessageSingleton.ACCESS_VALID));
				}
			} 
			catch ( UIWarningException uwe ) 
			{
				ExceptionProcessor.GetInstance().Process(uwe);
			}
		}

		
		/// <summary>
		/// If datagrid clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DgrVehicle_Click(object sender, System.EventArgs e)
		{	
			if(this.MouseInColumnHeader(DgrVehicle, MousePosition.X, MousePosition.Y))
			{
				mGridIsSorted = true;
			}
			else
			{
				DgrVehicle.Refresh();
				TableClick();
			}
		}

		
		/// <summary>
		/// If pointer moves to a differnt cell in datagrid, get CWR ID
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DgrVehicle_CurrentCellChanged(object sender, System.EventArgs e)
		{
			if ( !mGridIsSorted )
			{
				TableClick();
			}
		}
		
		/// <summary>
		/// if long access desired but short is accepted, the long access is automaticly
		/// not accepted
		/// Disabled fields for long access 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RbtVehicleEntranceShortReceivedYes_Click(object sender, System.EventArgs e)
		{
			if (RbtVehicleEntranceLong.Checked == true)
			{
				RbtVehicleEntranceLongNo.Checked = true;
			}
			GetMyController().HandleRadiobuttonsLongAccess();
		}

		/// <summary>
		/// Enabled fields for long access
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RbtVehicleEntranceShortReceivedNo_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleRadiobuttonsLongAccess();
		}

		/// <summary>
		/// if short access desired but long is accepted, the short access is automaticly
		/// not accepted
		/// Disabled fields for short access
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RbtVehicleEntranceLongYes_Click(object sender, System.EventArgs e)
		{
			if (RbtVehicleEntranceShort.Checked == true)
			{
				RbtVehicleEntranceShortReceivedNo.Checked = true;
			}
			GetMyController().HandleRadiobuttonsShortAccess();		
		}
		
		/// <summary>
		/// Enabled fields for short access
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RbtVehicleEntranceLongNo_Click(object sender, System.EventArgs e)
		{	
			GetMyController().HandleRadiobuttonsShortAccess();
		}	

		/// <summary>
		/// Set the focus on the first row of the datagrid and get the correctly 
		/// CowerkerID
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DgrVehicle_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			if(mGridIsSorted)
			{
				int rowIndex = this.DgrVehicle.CurrentRowIndex;
				if(-1 < rowIndex)
				{
					mCwrID = Convert.ToDecimal(this.DgrVehicle[rowIndex, 0].ToString());
				}
				this.DgrVehicle.Select(0);
				this.DgrVehicle.CurrentRowIndex = 0;
				this.TableClick();
				mGridIsSorted = false;
			}
		}

		#endregion	

		
	}
}


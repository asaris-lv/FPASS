using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Degussa.FPASS.Util;
using Degussa.FPASS.Util.ListOfValues;
using Degussa.FPASS.Util.UserManagement;

namespace Degussa.FPASS.Gui
{
	/// FPASSBaseView is the view of the MVC-triad FPASSBaseView,
	/// FPASSBaseModel and FPASSBaseController.
	/// FPASSBaseView extends from the BaseView.
	/// FPASSBaseView is the parent view of all views in FPASS. The general layout of all view 
	/// is defined here , but an instance of FPASSBaseView is never shown on its own.
	/// Catches the events of the main-menue and delgates them to the 
	/// FPASSBaseController for event-handling.
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
	public class FPASSBaseView : BaseView
	{
		#region Members

		public		StatusBar			StbBase;
		public		Label			LblBaseHead;
		public		StatusBarPanel SbpMessage;
		public		PictureBox pictureBox1;
		protected	MenuItem MnuFile;
		protected	MenuItem MnuExit;
		protected	MenuItem MnuFunction;
		protected	MenuItem MnuCoWorker;
		protected	MenuItem MnuDelete;
		protected	MenuItem MnuArchive;
		protected	MenuItem MnuDynamicData;
		protected	MenuItem MnuAdministration;
		protected	MenuItem MnuUserAdministration;
		protected	MenuItem MnuReports;
		protected	MenuItem MnuHelp;
		protected	MenuItem MnuContent;
		protected	MenuItem MnuIndex;
		protected	MenuItem MnuInfo;
		protected	MainMenu MnuMain;
		protected	MenuItem MnuHistory;
		public		StatusBarPanel Sbpenvironment;
		private		StatusBarPanel SbpMandator;
		private		StatusBarPanel		SbpUser;
		private		DataTable				dataTable1;
        private IContainer components;

        internal MenuItem MnuReport;
        internal MenuItem MnuExportToZKS;
        internal MenuItem MnuVehicle;
        internal MenuItem MnuZKS;
        internal MenuItem MnuIdCardPopup;
        internal MenuItem MnuSmartActStart;
        internal MenuItem MnuSmartActStop;
        

        #endregion Members
		
        #region Constructors

		/// <summary>
		/// Default constructor
		/// </summary>
        public FPASSBaseView()
        {
            InitializeComponent();

            InitView();
            SetAuthorization();
        }

		#endregion

		#region Accessors 

		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// Sets authorization on menus.
		/// </summary>
		private void SetAuthorization() 
		{
			this.MnuAdministration.Enabled = UserManagementControl.getInstance().GetAuthorization(UserManagementControl.ADMINISTRATION_DIALOG);
			this.MnuDelete.Enabled = UserManagementControl.getInstance().GetAuthorization(UserManagementControl.COWORKER_DELETELIST_DIALOG);
			this.MnuHistory.Enabled = UserManagementControl.getInstance().GetAuthorization(UserManagementControl.HISTORY_DIALOG);
			this.MnuUserAdministration.Enabled = UserManagementControl.getInstance().GetAuthorization(UserManagementControl.USER_REGISTER_DIALOG);
			this.MnuVehicle.Enabled = UserManagementControl.getInstance().GetAuthorization(UserManagementControl.COWORKER_VEHICLE_NO_SITE_SECURITY);
			this.MnuExportToZKS.Enabled = UserManagementControl.getInstance().GetAuthorization(UserManagementControl.FPASS_TO_ZKS);
		}

		/// <summary>
		/// Gets all coordinators which are assigned to the given
		/// contractor. Request is delegated to 
		/// <see cref="Degussa.FPASS.Util.ListOfValues"><code>FPASSLovsSingleton</code>
		/// </see> object.
		/// </summary>
        /// <param name="pContractorID">id (pk) of the contractor</param>
		/// <returns>List containing all CoordLovItems which are assigned to the given
		/// contractor</returns>
		internal ArrayList GetCoordinatorList(String pContractorID) 
		{
			return FPASSLovsSingleton.GetInstance().GetCoordinators(Convert.ToDecimal(pContractorID));
		}


		/// <summary>
		/// Gets all contractors which are assigned to the given
		/// coordinator. Request is delegated to 
		/// <see cref="Degussa.FPASS.Util.ListOfValues"><code>FPASSLovsSingleton</code>
		/// </see> object.
		/// </summary>
		/// </summary>
		/// <param name="pCoordinatorID">id (pk) of the coordinator</param>
		/// <returns>List containing all ContractorLovItems which are assigned to the given
		/// coordinator</returns>
		internal ArrayList GetContractorList(String pCoordinatorID) 
		{
			return FPASSLovsSingleton.GetInstance().GetContractors(Convert.ToDecimal(pCoordinatorID) );
		}


		/// <summary>
		/// Gets all valid and invalid contractors 
		/// Only valid contractors are assigned to a coorrdinator. 
		/// Request is delegated to 
		/// <see cref="Degussa.FPASS.Util.ListOfValues"><code>FPASSLovsSingleton</code>
		/// </see> object.
		/// </summary>
		/// </summary>
		/// <param name="pCoordinatorID">id (pk) of the coordinator</param>
		/// <returns>List containing all ContractorLovItems which are assigned to the given
		/// coordinator</returns>
		internal ArrayList GetContractorValInvalList(String pCoordinatorID) 
		{
			return FPASSLovsSingleton.GetInstance().GetValInvalContractors(Convert.ToDecimal(pCoordinatorID) );
		}

		/// <summary>
		/// Initializes fields: in this case user/database and mandant info in status bar
		/// </summary>
		private void InitView()
		{
			SbpMandator.Text = UserManagementControl.getInstance().CurrentMandatorName;
            SbpUser.Text =
                UserManagementControl.getInstance().CurrentOSUserName
                + "/"
                + Globals.GetInstance().FPASSDatabaseUser
                + "@"
                + Globals.GetInstance().FPASSDatabaseName;
		}

		/// <summary>
		/// Gets typed controller instance for this dialog (M-V-C)
		/// </summary>
		/// <returns>responsible controller </returns>
		private FPASSBaseController GetMyController() 
		{
			return ((FPASSBaseController)mController);
		}

		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FPASSBaseView));
            this.LblBaseHead = new System.Windows.Forms.Label();
            this.StbBase = new System.Windows.Forms.StatusBar();
            this.SbpUser = new System.Windows.Forms.StatusBarPanel();
            this.Sbpenvironment = new System.Windows.Forms.StatusBarPanel();
            this.SbpMessage = new System.Windows.Forms.StatusBarPanel();
            this.SbpMandator = new System.Windows.Forms.StatusBarPanel();
            this.dataTable1 = new System.Data.DataTable();
            this.MnuMain = new System.Windows.Forms.MainMenu(this.components);
            this.MnuFile = new System.Windows.Forms.MenuItem();
            this.MnuExit = new System.Windows.Forms.MenuItem();
            this.MnuFunction = new System.Windows.Forms.MenuItem();
            this.MnuCoWorker = new System.Windows.Forms.MenuItem();
            this.MnuDelete = new System.Windows.Forms.MenuItem();
            this.MnuArchive = new System.Windows.Forms.MenuItem();
            this.MnuDynamicData = new System.Windows.Forms.MenuItem();
            this.MnuVehicle = new System.Windows.Forms.MenuItem();
            this.MnuZKS = new System.Windows.Forms.MenuItem();
            this.MnuExportToZKS = new System.Windows.Forms.MenuItem();
            this.MnuIdCardPopup = new System.Windows.Forms.MenuItem();
            this.MnuSmartActStart = new System.Windows.Forms.MenuItem();
            this.MnuSmartActStop = new System.Windows.Forms.MenuItem();
            this.MnuAdministration = new System.Windows.Forms.MenuItem();
            this.MnuUserAdministration = new System.Windows.Forms.MenuItem();
            this.MnuHistory = new System.Windows.Forms.MenuItem();
            this.MnuReports = new System.Windows.Forms.MenuItem();
            this.MnuReport = new System.Windows.Forms.MenuItem();
            this.MnuHelp = new System.Windows.Forms.MenuItem();
            this.MnuContent = new System.Windows.Forms.MenuItem();
            this.MnuIndex = new System.Windows.Forms.MenuItem();
            this.MnuInfo = new System.Windows.Forms.MenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.SbpUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sbpenvironment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SbpMessage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SbpMandator)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // LblBaseHead
            // 
            this.LblBaseHead.BackColor = System.Drawing.Color.White;
            this.LblBaseHead.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblBaseHead.Location = new System.Drawing.Point(0, 0);
            this.LblBaseHead.Name = "LblBaseHead";
            this.LblBaseHead.Size = new System.Drawing.Size(1264, 40);
            this.LblBaseHead.TabIndex = 0;
            this.LblBaseHead.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // StbBase
            // 
            this.StbBase.Dock = System.Windows.Forms.DockStyle.None;
            this.StbBase.Location = new System.Drawing.Point(4, 897);
            this.StbBase.Name = "StbBase";
            this.StbBase.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.SbpUser,
            this.Sbpenvironment,
            this.SbpMessage,
            this.SbpMandator});
            this.StbBase.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StbBase.ShowPanels = true;
            this.StbBase.Size = new System.Drawing.Size(1258, 24);
            this.StbBase.SizingGrip = false;
            this.StbBase.TabIndex = 1;
            // 
            // SbpUser
            // 
            this.SbpUser.Name = "SbpUser";
            this.SbpUser.Width = 260;
            // 
            // Sbpenvironment
            // 
            this.Sbpenvironment.Name = "Sbpenvironment";
            this.Sbpenvironment.Width = 250;
            // 
            // SbpMessage
            // 
            this.SbpMessage.Name = "SbpMessage";
            this.SbpMessage.Width = 500;
            // 
            // SbpMandator
            // 
            this.SbpMandator.Name = "SbpMandator";
            this.SbpMandator.Width = 255;
            // 
            // dataTable1
            // 
            this.dataTable1.TableName = "Table1";
            // 
            // MnuMain
            // 
            this.MnuMain.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MnuFile,
            this.MnuFunction,
            this.MnuReports,
            this.MnuHelp});
            // 
            // MnuFile
            // 
            this.MnuFile.Index = 0;
            this.MnuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MnuExit});
            this.MnuFile.Text = "Datei";
            // 
            // MnuExit
            // 
            this.MnuExit.Index = 0;
            this.MnuExit.Shortcut = System.Windows.Forms.Shortcut.F4;
            this.MnuExit.Text = "Beenden";
            this.MnuExit.Click += new System.EventHandler(this.MnuExit_Click);
            // 
            // MnuFunction
            // 
            this.MnuFunction.Index = 1;
            this.MnuFunction.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MnuCoWorker,
            this.MnuZKS,
            this.MnuAdministration,
            this.MnuUserAdministration,
            this.MnuHistory});
            this.MnuFunction.Text = "Funktionen";
            // 
            // MnuCoWorker
            // 
            this.MnuCoWorker.Index = 0;
            this.MnuCoWorker.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MnuDelete,
            this.MnuArchive,
            this.MnuDynamicData,
            this.MnuVehicle});
            this.MnuCoWorker.Text = "FFMA";
            // 
            // MnuDelete
            // 
            this.MnuDelete.Index = 0;
            this.MnuDelete.Shortcut = System.Windows.Forms.Shortcut.F3;
            this.MnuDelete.Text = "Löschen";
            this.MnuDelete.Click += new System.EventHandler(this.MnuDelete_Click);
            // 
            // MnuArchive
            // 
            this.MnuArchive.Index = 1;
            this.MnuArchive.Shortcut = System.Windows.Forms.Shortcut.F6;
            this.MnuArchive.Text = "Archiv";
            this.MnuArchive.Click += new System.EventHandler(this.MnuArchive_Click);
            // 
            // MnuDynamicData
            // 
            this.MnuDynamicData.Index = 2;
            this.MnuDynamicData.Shortcut = System.Windows.Forms.Shortcut.F7;
            this.MnuDynamicData.Text = "Bewegungsdaten";
            this.MnuDynamicData.Click += new System.EventHandler(this.MnuDynamicData_Click);
            // 
            // MnuVehicle
            // 
            this.MnuVehicle.Index = 3;
            this.MnuVehicle.Shortcut = System.Windows.Forms.Shortcut.F2;
            this.MnuVehicle.Text = "Kfz-Zutritt";
            this.MnuVehicle.Click += new System.EventHandler(this.MnuVehicle_Click);
            // 
            // MnuZKS
            // 
            this.MnuZKS.Index = 1;
            this.MnuZKS.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MnuExportToZKS,
            this.MnuIdCardPopup,
            this.MnuSmartActStart,
            this.MnuSmartActStop});
            this.MnuZKS.Text = "FPASS > ZKS";
            // 
            // MnuExportToZKS
            // 
            this.MnuExportToZKS.Enabled = false;
            this.MnuExportToZKS.Index = 0;
            this.MnuExportToZKS.Shortcut = System.Windows.Forms.Shortcut.F8;
            this.MnuExportToZKS.Text = "1) FFMA mit neuem Ausweis sofort übertragen";
            this.MnuExportToZKS.Click += new System.EventHandler(this.MnuExportToZKS_Click);
            // 
            // MnuIdCardPopup
            // 
            this.MnuIdCardPopup.Index = 1;
            this.MnuIdCardPopup.Shortcut = System.Windows.Forms.Shortcut.CtrlF8;
            this.MnuIdCardPopup.Text = "2) FFMA mit neuem Ausweis ansehen";
            this.MnuIdCardPopup.Click += new System.EventHandler(this.MnuIdCardPopup_Click);
            // 
            // MnuSmartActStart
            // 
            this.MnuSmartActStart.Index = 2;
            this.MnuSmartActStart.Shortcut = System.Windows.Forms.Shortcut.CtrlF6;
            this.MnuSmartActStart.Text = "3) autom. Benachrichtigung zu (2) starten";
            this.MnuSmartActStart.Click += new System.EventHandler(this.MnuSmartActStart_Click);
            // 
            // MnuSmartActStop
            // 
            this.MnuSmartActStop.Index = 3;
            this.MnuSmartActStop.Shortcut = System.Windows.Forms.Shortcut.CtrlF7;
            this.MnuSmartActStop.Text = "4) autom. Benachrichtigung zu (2) beenden";
            this.MnuSmartActStop.Click += new System.EventHandler(this.MnuSmartActStop_Click);
            // 
            // MnuAdministration
            // 
            this.MnuAdministration.Index = 2;
            this.MnuAdministration.Shortcut = System.Windows.Forms.Shortcut.F9;
            this.MnuAdministration.Text = "Verwaltung";
            this.MnuAdministration.Click += new System.EventHandler(this.MnuAdministration_Click);
            // 
            // MnuUserAdministration
            // 
            this.MnuUserAdministration.Index = 3;
            this.MnuUserAdministration.Shortcut = System.Windows.Forms.Shortcut.F10;
            this.MnuUserAdministration.Text = "Benutzerverwaltung";
            this.MnuUserAdministration.Click += new System.EventHandler(this.MnuUserAdministration_Click);
            // 
            // MnuHistory
            // 
            this.MnuHistory.Index = 4;
            this.MnuHistory.Shortcut = System.Windows.Forms.Shortcut.F11;
            this.MnuHistory.Text = "Änderungshistorie";
            this.MnuHistory.Click += new System.EventHandler(this.MnuHistory_Click);
            // 
            // MnuReports
            // 
            this.MnuReports.Index = 2;
            this.MnuReports.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MnuReport});
            this.MnuReports.Text = "Reports";
            // 
            // MnuReport
            // 
            this.MnuReport.Index = 0;
            this.MnuReport.Shortcut = System.Windows.Forms.Shortcut.F12;
            this.MnuReport.Text = "Reports";
            this.MnuReport.Click += new System.EventHandler(this.MnuReport_Click);
            // 
            // MnuHelp
            // 
            this.MnuHelp.Index = 3;
            this.MnuHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MnuContent,
            this.MnuIndex,
            this.MnuInfo});
            this.MnuHelp.Text = "Hilfe";
            // 
            // MnuContent
            // 
            this.MnuContent.Index = 0;
            this.MnuContent.Shortcut = System.Windows.Forms.Shortcut.F1;
            this.MnuContent.Text = "Inhalt";
            this.MnuContent.Click += new System.EventHandler(this.MnuContent_Click);
            // 
            // MnuIndex
            // 
            this.MnuIndex.Index = 1;
            this.MnuIndex.Text = "Index";
            this.MnuIndex.Click += new System.EventHandler(this.MnuIndex_Click);
            // 
            // MnuInfo
            // 
            this.MnuInfo.Index = 2;
            this.MnuInfo.Text = "Info";
            this.MnuInfo.Click += new System.EventHandler(this.MnuInfo_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(159, 37);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // FPASSBaseView
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(1264, 921);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.StbBase);
            this.Controls.Add(this.LblBaseHead);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Menu = this.MnuMain;
//            this.MinimumSize = new System.Drawing.Size(1278, 978);
            this.MinimumSize = new System.Drawing.Size(1278, 998);
            this.Name = "FPASSBaseView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.SbpUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sbpenvironment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SbpMessage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SbpMandator)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

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


		#endregion

		#region Events 

		private void MnuExit_Click(object sender, EventArgs e)
		{
			GetMyController().HandleEventCloseFPASS();
		}

		private void MnuReport_Click(object sender, EventArgs e)
		{
			GetMyController().HandleEventOpenReportsDialog();
		}

		private void MnuDelete_Click(object sender, EventArgs e)
		{
			GetMyController().HandleEventOpenDeleteDialog();
		}

		private void MnuArchive_Click(object sender, EventArgs e)
		{
			GetMyController().HandleEventOpenArchiveDialog();
		}

		private void MnuDynamicData_Click(object sender, EventArgs e)
		{
			GetMyController().HandleEventOpenDynamicDataDialog();
		}

		private void MnuAdministration_Click(object sender, EventArgs e)
		{
			GetMyController().HandleEventOpenAdministrationDialog();
		}

		private void MnuHistory_Click(object sender, EventArgs e)
		{
			GetMyController().HandleEventOpenHistoryDialog();
		}

		private void MnuContent_Click(object sender, EventArgs e)
		{
			GetMyController().HandleEventShowHelp();
		}

		private void MnuIndex_Click(object sender, EventArgs e)
		{
			GetMyController().HandleEventShowHelpIndex();		
		}

		private void MnuUserAdministration_Click(object sender, EventArgs e)
		{
			GetMyController().HandleEventOpenUserDialog();
		}

		
		private void MnuVehicle_Click(object sender, EventArgs e)
		{
			GetMyController().HandleEventOpenVehicleDialog();
		}

        /// <summary>
        /// This is the original menu item "FPASS > ZKS", unchanged in FPASS V5.
        /// Transfers all CWR with status N to ZKS.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MnuExportToZKS_Click(object sender, EventArgs e)
        {
            GetMyController().HandleEventExportToZKS();
        }

        /// <summary>
        /// Shows new dialog PopupIdCard (FFMA mit neuem Ausweis) FPASS V5.
        /// </summary>
        private void MnuIdCardPopup_Click(object sender, EventArgs e)
        {
            GetMyController().HandleEventSmartActPopIdCard();
        }

        /// <summary>
        /// This starts the background process to make dialog PopupIdCard (FFMA mit neuem Ausweis) 
        /// automatically pop up. FPASS V5.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MnuSmartActStart_Click(object sender, EventArgs e)
        {
            GetMyController().HandleEventSmartActStart();
        }

        /// <summary>
        /// This stops the background process to stop dialog PopupIdCard (FFMA mit neuem Ausweis) 
        /// automatically pop up. FPASS V5.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MnuSmartActStop_Click(object sender, EventArgs e)
        {
            GetMyController().HandleEventSmartActStop();
        }

        /// <summary>
        /// Opens Infobox "About..."
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MnuInfo_Click(object sender, EventArgs e)
        {
            GetMyController().HandleEventShowInfobox();
        }

        #endregion // End of Events		
	}
}

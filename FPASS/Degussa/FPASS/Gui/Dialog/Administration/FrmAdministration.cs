using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Degussa.FPASS.Gui.Dialog;
using Degussa.FPASS.Util.UserManagement;

namespace Degussa.FPASS.Gui.Dialog.Administration
{
	/// <summary>
	/// A FrmAdministration is the view of the 
	/// MVC-triad AdministrationController, AdministrationModel
	/// and FrmAdministration.
	/// FrmAdministration extends from the FPASSBaseView.
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
	public class FrmAdministration : FPASSBaseView
	{

		#region Members

		//labels
		internal System.Windows.Forms.Label LblMaskTitle;

		// main tabcontrol
		internal System.Windows.Forms.TabControl tabControlAdministration;

		// sub tabpages, filled by user controls
		internal System.Windows.Forms.TabPage TabExternalContractor;
		internal System.Windows.Forms.TabPage TabExternalContractorCoordinator;
		internal System.Windows.Forms.TabPage TabMedicalSiteService;
		internal System.Windows.Forms.TabPage TabPlant;
		internal System.Windows.Forms.TabPage TabDepartment;
		internal System.Windows.Forms.TabPage TabCraft;

		//user controls
		internal Degussa.FPASS.Gui.Dialog.Administration.UserControls.FrmUCAdminExternalContractor frmUCAdminExternalContractor1;
		internal Degussa.FPASS.Gui.Dialog.Administration.UserControls.FrmUCAdminDepartment frmUCAdminDepartment1;
		internal Degussa.FPASS.Gui.Dialog.Administration.UserControls.FrmUCAdminCoordinatorExternalContractor frmUCAdminCoordExco1;
		internal Degussa.FPASS.Gui.Dialog.Administration.UserControls.FrmUCAdminMedicalPrecautionary frmUCAdminMedicalPrecautionary1;
		internal Degussa.FPASS.Gui.Dialog.Administration.UserControls.FrmUCAdminPlant frmUCAdminPlant1;
		internal Degussa.FPASS.Gui.Dialog.Administration.UserControls.FrmUCAdminCraft frmUCAdminCraft1;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private int mCalledSearchContractor;

		#endregion //End of Members

		#region Constructors 

		public FrmAdministration()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			
			InitView();

			FillLists();

			SetAuthorization();
		}

		#endregion //End of Constructors

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.tabControlAdministration = new System.Windows.Forms.TabControl();
            this.TabExternalContractor = new System.Windows.Forms.TabPage();
            this.frmUCAdminExternalContractor1 = new Degussa.FPASS.Gui.Dialog.Administration.UserControls.FrmUCAdminExternalContractor();
            this.TabDepartment = new System.Windows.Forms.TabPage();
            this.frmUCAdminDepartment1 = new Degussa.FPASS.Gui.Dialog.Administration.UserControls.FrmUCAdminDepartment();
            this.TabExternalContractorCoordinator = new System.Windows.Forms.TabPage();
            this.frmUCAdminCoordExco1 = new Degussa.FPASS.Gui.Dialog.Administration.UserControls.FrmUCAdminCoordinatorExternalContractor();
            this.TabMedicalSiteService = new System.Windows.Forms.TabPage();
            this.frmUCAdminMedicalPrecautionary1 = new Degussa.FPASS.Gui.Dialog.Administration.UserControls.FrmUCAdminMedicalPrecautionary();
            this.TabPlant = new System.Windows.Forms.TabPage();
            this.frmUCAdminPlant1 = new Degussa.FPASS.Gui.Dialog.Administration.UserControls.FrmUCAdminPlant();
            this.TabCraft = new System.Windows.Forms.TabPage();
            this.frmUCAdminCraft1 = new Degussa.FPASS.Gui.Dialog.Administration.UserControls.FrmUCAdminCraft();
            this.LblMaskTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.SbpMessage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sbpenvironment)).BeginInit();
            this.tabControlAdministration.SuspendLayout();
            this.TabExternalContractor.SuspendLayout();
            this.TabDepartment.SuspendLayout();
            this.TabExternalContractorCoordinator.SuspendLayout();
            this.TabMedicalSiteService.SuspendLayout();
            this.TabPlant.SuspendLayout();
            this.TabCraft.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlAdministration
            // 
            this.tabControlAdministration.Controls.Add(this.TabExternalContractor);
            this.tabControlAdministration.Controls.Add(this.TabDepartment);
            this.tabControlAdministration.Controls.Add(this.TabExternalContractorCoordinator);
            this.tabControlAdministration.Controls.Add(this.TabMedicalSiteService);
            this.tabControlAdministration.Controls.Add(this.TabPlant);
            this.tabControlAdministration.Controls.Add(this.TabCraft);
            this.tabControlAdministration.Font = new System.Drawing.Font("Arial", 9F);
            this.tabControlAdministration.ItemSize = new System.Drawing.Size(71, 18);
            this.tabControlAdministration.Location = new System.Drawing.Point(6, 50);
            this.tabControlAdministration.Name = "tabControlAdministration";
            this.tabControlAdministration.SelectedIndex = 0;
            this.tabControlAdministration.Size = new System.Drawing.Size(1258, 816);
            this.tabControlAdministration.TabIndex = 3;
            // 
            // TabExternalContractor
            // 
            this.TabExternalContractor.Controls.Add(this.frmUCAdminExternalContractor1);
            this.TabExternalContractor.Location = new System.Drawing.Point(4, 22);
            this.TabExternalContractor.Name = "TabExternalContractor";
            this.TabExternalContractor.Size = new System.Drawing.Size(1250, 790);
            this.TabExternalContractor.TabIndex = 1;
            this.TabExternalContractor.Text = "Fremdfirma";
            // 
            // frmUCAdminExternalContractor1
            // 
            this.frmUCAdminExternalContractor1.ContentChanged = false;
            this.frmUCAdminExternalContractor1.CurrentAdminRec = -1;
            this.frmUCAdminExternalContractor1.CurrentEXCOName = "";
            this.frmUCAdminExternalContractor1.Location = new System.Drawing.Point(0, 0);
            this.frmUCAdminExternalContractor1.Name = "frmUCAdminExternalContractor1";
            this.frmUCAdminExternalContractor1.PropDSExContractor = null;
            this.frmUCAdminExternalContractor1.Size = new System.Drawing.Size(1258, 816);
            this.frmUCAdminExternalContractor1.TabIndex = 0;
            // 
            // TabDepartment
            // 
            this.TabDepartment.Controls.Add(this.frmUCAdminDepartment1);
            this.TabDepartment.Location = new System.Drawing.Point(4, 22);
            this.TabDepartment.Name = "TabDepartment";
            this.TabDepartment.Size = new System.Drawing.Size(1250, 790);
            this.TabDepartment.TabIndex = 5;
            this.TabDepartment.Text = "Abteilung";
            this.TabDepartment.Visible = false;
            // 
            // frmUCAdminDepartment1
            // 
            this.frmUCAdminDepartment1.ContentChanged = false;
            this.frmUCAdminDepartment1.CurrentAdminRec = -1;
            this.frmUCAdminDepartment1.Location = new System.Drawing.Point(0, 0);
            this.frmUCAdminDepartment1.Name = "frmUCAdminDepartment1";
            this.frmUCAdminDepartment1.PropDSDepartment = null;
            this.frmUCAdminDepartment1.Size = new System.Drawing.Size(1258, 816);
            this.frmUCAdminDepartment1.TabIndex = 0;
            // 
            // TabExternalContractorCoordinator
            // 
            this.TabExternalContractorCoordinator.Controls.Add(this.frmUCAdminCoordExco1);
            this.TabExternalContractorCoordinator.Location = new System.Drawing.Point(4, 22);
            this.TabExternalContractorCoordinator.Name = "TabExternalContractorCoordinator";
            this.TabExternalContractorCoordinator.Size = new System.Drawing.Size(1250, 790);
            this.TabExternalContractorCoordinator.TabIndex = 2;
            this.TabExternalContractorCoordinator.Text = "Zuordnung Koordinator / Fremdfirma";
            this.TabExternalContractorCoordinator.Visible = false;
            // 
            // frmUCAdminCoordExco1
            // 
            this.frmUCAdminCoordExco1.ContentChanged = false;
            this.frmUCAdminCoordExco1.CurrentCoordinatorID = -1;
            this.frmUCAdminCoordExco1.CurrentEXCORec = -1;
            this.frmUCAdminCoordExco1.Location = new System.Drawing.Point(0, 0);
            this.frmUCAdminCoordExco1.Name = "frmUCAdminCoordExco1";
            this.frmUCAdminCoordExco1.PropDSExcoCoord = null;
            this.frmUCAdminCoordExco1.Size = new System.Drawing.Size(992, 616);
            this.frmUCAdminCoordExco1.TabIndex = 0;
            // 
            // TabMedicalSiteService
            // 
            this.TabMedicalSiteService.Controls.Add(this.frmUCAdminMedicalPrecautionary1);
            this.TabMedicalSiteService.Location = new System.Drawing.Point(4, 22);
            this.TabMedicalSiteService.Name = "TabMedicalSiteService";
            this.TabMedicalSiteService.Size = new System.Drawing.Size(1250, 790);
            this.TabMedicalSiteService.TabIndex = 3;
            this.TabMedicalSiteService.Text = "Vorsorgeuntersuchung";
            this.TabMedicalSiteService.Visible = false;
            // 
            // frmUCAdminMedicalPrecautionary1
            // 
            this.frmUCAdminMedicalPrecautionary1.ContentChanged = false;
            this.frmUCAdminMedicalPrecautionary1.CurrentAdminRec = -1;
            this.frmUCAdminMedicalPrecautionary1.Location = new System.Drawing.Point(0, 0);
            this.frmUCAdminMedicalPrecautionary1.Name = "frmUCAdminMedicalPrecautionary1";
            this.frmUCAdminMedicalPrecautionary1.PropDSPrecMedType = null;
            this.frmUCAdminMedicalPrecautionary1.Size = new System.Drawing.Size(992, 616);
            this.frmUCAdminMedicalPrecautionary1.TabIndex = 0;
            // 
            // TabPlant
            // 
            this.TabPlant.Controls.Add(this.frmUCAdminPlant1);
            this.TabPlant.Location = new System.Drawing.Point(4, 22);
            this.TabPlant.Name = "TabPlant";
            this.TabPlant.Size = new System.Drawing.Size(1250, 790);
            this.TabPlant.TabIndex = 4;
            this.TabPlant.Text = "Betrieb";
            this.TabPlant.Visible = false;
            // 
            // frmUCAdminPlant1
            // 
            this.frmUCAdminPlant1.ContentChanged = false;
            this.frmUCAdminPlant1.CurrentAdminRec = -1;
            this.frmUCAdminPlant1.Location = new System.Drawing.Point(0, 0);
            this.frmUCAdminPlant1.Name = "frmUCAdminPlant1";
            this.frmUCAdminPlant1.PropDSPlant = null;
            this.frmUCAdminPlant1.Size = new System.Drawing.Size(1258, 816);
            this.frmUCAdminPlant1.TabIndex = 0;
            // 
            // TabCraft
            // 
            this.TabCraft.Controls.Add(this.frmUCAdminCraft1);
            this.TabCraft.Location = new System.Drawing.Point(4, 22);
            this.TabCraft.Name = "TabCraft";
            this.TabCraft.Size = new System.Drawing.Size(1250, 790);
            this.TabCraft.TabIndex = 6;
            this.TabCraft.Text = "Gewerk";
            this.TabCraft.Visible = false;
            // 
            // frmUCAdminCraft1
            // 
            this.frmUCAdminCraft1.ContentChanged = false;
            this.frmUCAdminCraft1.CurrentAdminRec = -1;
            this.frmUCAdminCraft1.Location = new System.Drawing.Point(0, 0);
            this.frmUCAdminCraft1.Name = "frmUCAdminCraft1";
            this.frmUCAdminCraft1.PropDSCraft = null;
            this.frmUCAdminCraft1.Size = new System.Drawing.Size(992, 616);
            this.frmUCAdminCraft1.TabIndex = 0;
            // 
            // LblMaskTitle
            // 
            this.LblMaskTitle.BackColor = System.Drawing.Color.White;
            this.LblMaskTitle.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMaskTitle.Location = new System.Drawing.Point(361, 8);
            this.LblMaskTitle.Name = "LblMaskTitle";
            this.LblMaskTitle.Size = new System.Drawing.Size(290, 32);
            this.LblMaskTitle.TabIndex = 77;
            this.LblMaskTitle.Text = "FPASS - Verwaltung";
            // 
            // FrmAdministration
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(1270, 966);
            this.Controls.Add(this.LblMaskTitle);
            this.Controls.Add(this.tabControlAdministration);
            this.Name = "FrmAdministration";
            this.Text = "FPASS - Verwaltung";
            this.Controls.SetChildIndex(this.LblBaseHead, 0);
            this.Controls.SetChildIndex(this.StbBase, 0);
            this.Controls.SetChildIndex(this.tabControlAdministration, 0);
            this.Controls.SetChildIndex(this.LblMaskTitle, 0);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.SbpMessage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sbpenvironment)).EndInit();
            this.tabControlAdministration.ResumeLayout(false);
            this.TabExternalContractor.ResumeLayout(false);
            this.TabDepartment.ResumeLayout(false);
            this.TabExternalContractorCoordinator.ResumeLayout(false);
            this.TabMedicalSiteService.ResumeLayout(false);
            this.TabPlant.ResumeLayout(false);
            this.TabCraft.ResumeLayout(false);
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

		public int CalledSearchContractor
		{
			get 
			{
				return mCalledSearchContractor;
			}
			set 
			{
				mCalledSearchContractor = value;
			}
		} 

		#endregion //End of Accessors

		#region Methods 

		public override void RegisterController(AbstractController pAbstractController) 
		{
			base.RegisterController(pAbstractController);
			frmUCAdminExternalContractor1.RegisterController(pAbstractController);
			frmUCAdminMedicalPrecautionary1.RegisterController(pAbstractController);
			frmUCAdminPlant1.RegisterController(pAbstractController);
			frmUCAdminDepartment1.RegisterController(pAbstractController);
			frmUCAdminCraft1.RegisterController(pAbstractController);
			frmUCAdminCoordExco1.RegisterController(pAbstractController);
		}


		private void SetAuthorization() 
		{
			frmUCAdminCoordExco1.Enabled = UserManagementControl.
				getInstance().GetAuthorization(UserManagementControl.ADMIN_ASSIGNMENT_COORD_EXCO_DIALOG);
			frmUCAdminCraft1.Enabled = UserManagementControl.
				getInstance().GetAuthorization(UserManagementControl.ADMIN_CRAFT_DIALOG);
			frmUCAdminDepartment1.Enabled = UserManagementControl.
				getInstance().GetAuthorization(UserManagementControl.ADMIN_DEPARTMENT_DIALOG);
			frmUCAdminExternalContractor1.Enabled = UserManagementControl.
				getInstance().GetAuthorization(UserManagementControl.ADMIN_EXCONTRACTOR_DIALOG);
			frmUCAdminMedicalPrecautionary1.Enabled = UserManagementControl.
				getInstance().GetAuthorization(UserManagementControl.ADMIN_MEDICAL_PREC_DIALOG);
			frmUCAdminPlant1.Enabled = UserManagementControl.
				getInstance().GetAuthorization(UserManagementControl.ADMIN_PLANT_DIALOG);
		}

		
		/// <summary>
		/// is called before a dialog is displayed. 
		/// </summary>
		internal override void PreShow() 
		{

		}

		/// <summary>
		/// is called before a dialog is hidden/covered.
		/// </summary>
		internal override void PreHide() 
		{

		}

		internal override void FillLists() 
		{
			frmUCAdminExternalContractor1.FillLists();
			frmUCAdminMedicalPrecautionary1.FillLists();
			frmUCAdminPlant1.FillLists();
			frmUCAdminDepartment1.FillLists();
			frmUCAdminCraft1.FillLists();
			frmUCAdminCoordExco1.FillLists();
		}


		/// <summary>
		/// Decide which UserControl called the popup to search for an external contractor
		/// set the selected value in the correct combobox
		/// </summary>
		/// <param name="pContractorID"></param>
		internal override void ReFillContractorList(String pContractorID) 
		{

			if ( mCalledSearchContractor == 1 ) 
			{
				this.frmUCAdminExternalContractor1.CboSearchExternalContractor.SelectedValue = Convert.ToDecimal( pContractorID );
			} 
			else if  ( mCalledSearchContractor == 2 )  
			{
				this.frmUCAdminCoordExco1.CboSearchExternalContractor.SelectedValue = Convert.ToDecimal( pContractorID );
			}
		}


		/// <summary>
		/// Initialize input fields.
		/// </summary>
		private void InitView()
		{	
			MnuFunction.Enabled = false;
            MnuZKS.Enabled = false;
			MnuReports.Enabled = false;

//			// Instantiate datasets for UserControls
			frmUCAdminExternalContractor1.CreateDataSet();
			frmUCAdminCoordExco1.CreateDataSet();
			frmUCAdminMedicalPrecautionary1.CreateDataSet();
			frmUCAdminPlant1.CreateDataSet();
			frmUCAdminDepartment1.CreateDataSet();			
			frmUCAdminCraft1.CreateDataSet();
		}

		private AdministrationController GetMyController() 
		{
			return (AdministrationController)mController;
		}

		#endregion // End of Methods

		#region Events

		private void BtnBackTo_Click(object sender, System.EventArgs e)
		{
			((AdministrationController)mController).HandleCloseDialog();
		}


		private void BtnCancel_Click(object sender, System.EventArgs e)
		{
			GetMyController().HandleEventBtnCancelClick();
		}

		#endregion // End of Events		

	}
}

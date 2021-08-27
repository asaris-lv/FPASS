using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using de.pta.Component.ListOfValues;
using de.pta.Component.Common;
using de.pta.Component.Errorhandling;

using Degussa.FPASS.Util;
using Degussa.FPASS.Util.ListOfValues;
using Degussa.FPASS.Util.Validation;
using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.Util.ErrorHandling;
using Degussa.FPASS.Util.UserManagement;

namespace Degussa.FPASS.Gui.Dialog.Pass
{
	/// <summary>
	/// FrmExtendedSearch is the view of the MVC-triad ExtendedSearchModel,
	/// ExtendedSearchController and FrmExtendedSearch.
	/// FrmExtendedSearch extends from the FPASSBaseView.
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

	public class FrmExtendedSearch : FPASSBaseView
	{
		#region Members

		//panels
		internal System.Windows.Forms.Panel PnlMedical;
		internal System.Windows.Forms.Panel PnlMask;
        internal System.Windows.Forms.Panel PnlPass;
		internal System.Windows.Forms.Panel PnlCheckInOff;
		internal System.Windows.Forms.Panel PnlBreathingApparatusBriefingG263;
		internal System.Windows.Forms.Panel PnlDataExternalContractor;
		internal System.Windows.Forms.Panel PnlBriefingSite;
		internal System.Windows.Forms.Panel PnlCtlRbtSafetyAtWork;
		internal System.Windows.Forms.Panel PnlCtlRbtRespiratoryMask;
		internal System.Windows.Forms.Panel PnlCtlRbtPrecautionaryMedical;
		internal System.Windows.Forms.Panel PnlCtlRbtPalletLifter;
		internal System.Windows.Forms.Panel PnlCtlRbtRaisablePlatform;
		internal System.Windows.Forms.Panel PnlCtlRbtCranes;
		internal System.Windows.Forms.Panel PnlCtlRbtBriefingPlant;
		internal System.Windows.Forms.Panel PnlCtlRbtBriefingSafetyAtWork;
		internal System.Windows.Forms.Panel PnlCtlRbtBriefingSiteSecurity;
		internal System.Windows.Forms.Panel PnlCtlRbtIdPhotoHitag;
		internal System.Windows.Forms.Panel PnlCtlRbtBreathingApparatus;
        internal System.Windows.Forms.Panel PnlCtlRbtSafetyInstructions;
		internal System.Windows.Forms.Panel PnlDataExternalSearch;
		internal System.Windows.Forms.Panel PnlAccessAuthorization;
		internal System.Windows.Forms.Panel PnlVehicleLong;
		internal System.Windows.Forms.Panel PnlVehicleShortCo;
		internal System.Windows.Forms.Panel PnlVehicleShort;
		internal System.Windows.Forms.Panel PnlVehicleLongCo;

		//labels
		private System.Windows.Forms.Label LblHeadline;
		internal System.Windows.Forms.Label LblCoordinator;
		internal System.Windows.Forms.Label LblVehicleRegNumber;
		internal System.Windows.Forms.Label LblPlaceOfBirth;
		internal System.Windows.Forms.Label LblDateOfBirth;
		internal System.Windows.Forms.Label LblFirstname;
		internal System.Windows.Forms.Label LblSurname;
		internal System.Windows.Forms.Label LblExternalContractor;
		internal System.Windows.Forms.Label LblSupervisor;
		internal System.Windows.Forms.Label LblSubcontractor;
		internal System.Windows.Forms.Label LblOrderNumber;
		internal System.Windows.Forms.Label LblCraftNumber;
		internal System.Windows.Forms.Label LblPhone;
		internal System.Windows.Forms.Label LblDepartment;
		internal System.Windows.Forms.Label LblPlant;
		internal System.Windows.Forms.Label LblPalletLifter;
		internal System.Windows.Forms.Label LblCranes;
		internal System.Windows.Forms.Label LblRaisablePlatform;
		internal System.Windows.Forms.Label LblCheckInBy;
		internal System.Windows.Forms.Label LblCheckOffOn;
		internal System.Windows.Forms.Label LblCheckOffBy;
		internal System.Windows.Forms.Label LblSiteSecurityBriefingBy;
		internal System.Windows.Forms.Label LblSiteSecurityBriefingOn;
		internal System.Windows.Forms.Label LblSafetyAtWorkServiceBriefingBy;
		internal System.Windows.Forms.Label LblIdHitagBy;
		internal System.Windows.Forms.Label LblPalletLifterBy;
		internal System.Windows.Forms.Label LblRaisablePlatformBy;
		internal System.Windows.Forms.Label LblCranesBy;
		internal System.Windows.Forms.Label LblBriefingPlantBy;
		internal System.Windows.Forms.Label LblRespiratoryMaskBy;
		internal System.Windows.Forms.Label LblIndustrialSafetyBriefingBy;
		internal System.Windows.Forms.Label LblIdHitagOn;
		internal System.Windows.Forms.Label LblBreathingApparatusBriefingOn;
		internal System.Windows.Forms.Label LblPalletLifterOn;
		internal System.Windows.Forms.Label LblRaisablePlatformOn;
		internal System.Windows.Forms.Label LblCranesOn;
		internal System.Windows.Forms.Label LblBriefingPlantOn;
		internal System.Windows.Forms.Label LblRespiratoryMaskOn;
		internal System.Windows.Forms.Label LblSafetyAtWorkServiceBriefingOn;
		internal System.Windows.Forms.Label LblIndustrialSafetyBriefingOn;
		internal System.Windows.Forms.Label LblDelivered;
		internal System.Windows.Forms.Label LblRecieve;
		internal System.Windows.Forms.Label LblPrecautionaryMedical;
		internal System.Windows.Forms.Label LblRespiratoryMask;
		internal System.Windows.Forms.Label LblBriefingPlant;
		internal System.Windows.Forms.Label LblIdPhotoHitag;
		internal System.Windows.Forms.Label LblMaskNumberRecieve;
		internal System.Windows.Forms.Label LblSiteSecurityBriefing;
		internal System.Windows.Forms.Label LblSafetyAtWorkServiceBriefing;
		internal System.Windows.Forms.Label LblMaskNumberDelivered;
		internal System.Windows.Forms.Label LblIndustrialSafetyBriefing;
		internal System.Windows.Forms.Label LblDeliveryDate;
		internal System.Windows.Forms.Label LblSafetyInstructions;
		internal System.Windows.Forms.Label LblValidUntil;
        internal System.Windows.Forms.Label LblValidFrom;
		internal System.Windows.Forms.Label LblCheckInOn;
		internal System.Windows.Forms.Label LblVehicleShort;
		internal System.Windows.Forms.Label LblVehicleShortCo;
		internal System.Windows.Forms.Label LblVehicleLong;
		internal System.Windows.Forms.Label LblVehicleLongCo;
		internal System.Windows.Forms.Label LblAccessAuthorizationBy;
		internal System.Windows.Forms.Label LblAccessAuthorizationOn;
        internal System.Windows.Forms.Label LblSafetyInstructionsBy;
        internal System.Windows.Forms.Label LblSafetyInstructionsOn;
		internal System.Windows.Forms.Label LblBreathingApparatusBriefingG263;
		internal System.Windows.Forms.Label LblBreathingApparatusBriefingG263By;
		internal System.Windows.Forms.Label LblBreathingApparatusBriefingG263On;
		internal System.Windows.Forms.Label LblAccessAuthorization;
		internal System.Windows.Forms.Label LblBreathingApparatusBriefingG262;
		internal System.Windows.Forms.Label LblBreathingApparatusG262BriefingBy;
		internal System.Windows.Forms.Label LblVehicleShortBy;
		internal System.Windows.Forms.Label LblVehicleLongBy;
		internal System.Windows.Forms.Label LblVehicleShortOn;
		internal System.Windows.Forms.Label LblVehicleLongOn;
		internal System.Windows.Forms.Label LblStatus;
		internal System.Windows.Forms.Label LblMaskTitle;

		//textboxes
		internal System.Windows.Forms.TextBox TxtVehicleRegNumber;
		internal System.Windows.Forms.TextBox TxtPlaceOfBirth;
		internal System.Windows.Forms.TextBox TxtSurname;
		internal System.Windows.Forms.TextBox TxtFirstname;
		internal System.Windows.Forms.TextBox TxtDateOfBirth;
		internal System.Windows.Forms.TextBox TxtSupervisor;
		internal System.Windows.Forms.TextBox TxtOrderNumber;
		internal System.Windows.Forms.TextBox TxtPhone;
		internal System.Windows.Forms.TextBox TxtCheckInOn;
		internal System.Windows.Forms.TextBox TxtCheckOffOn;
		internal System.Windows.Forms.TextBox TxtBriefingPlantOn;
		internal System.Windows.Forms.TextBox TxtCranesOn;
		internal System.Windows.Forms.TextBox TxtRespiratoryMaskOn;
		internal System.Windows.Forms.TextBox TxtRaisablePlatformOn;
		internal System.Windows.Forms.TextBox TxtSiteSecurityBriefingOn;
		internal System.Windows.Forms.TextBox TxtIdHitagOn;
		internal System.Windows.Forms.TextBox TxtPalletLifterOn;
		internal System.Windows.Forms.TextBox TxtSafetyAtWorkServiceBriefingOn;
		internal System.Windows.Forms.TextBox TxtIndustrialSafetyBriefingOn;
		internal System.Windows.Forms.TextBox TxtCheckOffBy;
		internal System.Windows.Forms.TextBox TxtCheckInBy;
		internal System.Windows.Forms.TextBox TxtSiteSecurityBriefingBy;
		internal System.Windows.Forms.TextBox TxtIndustrialSafetyBriefingBy;
		internal System.Windows.Forms.TextBox TxtSafetyAtWorkServiceBriefingBy;
		internal System.Windows.Forms.TextBox TxtIdHitagBy;
		internal System.Windows.Forms.TextBox TxtPalletLifterBy;
		internal System.Windows.Forms.TextBox TxtRaisablePlatformBy;
		internal System.Windows.Forms.TextBox TxtCranesBy;
		internal System.Windows.Forms.TextBox TxtBriefingPlantBy;
		internal System.Windows.Forms.TextBox TxtRespiratoryMaskBy;
		internal System.Windows.Forms.TextBox TxtMaskNumberDelivered;
		internal System.Windows.Forms.TextBox TxtMaskNumberRecieve;
		internal System.Windows.Forms.TextBox TxtDeliveryDate;
		internal System.Windows.Forms.TextBox TxtValidFrom;
		internal System.Windows.Forms.TextBox TxtValidUntil;
		internal System.Windows.Forms.TextBox TxtAccessAuthorizationOn;
        internal System.Windows.Forms.TextBox TxtSafetyInstructionsOn;
        internal System.Windows.Forms.TextBox TxtAccessAuthorizationBy;
		internal System.Windows.Forms.TextBox TxtSafetyInstructionsBy;
		internal System.Windows.Forms.TextBox TxtBreathingApparatusBriefingG263On;
		internal System.Windows.Forms.TextBox TxtVehicleLongOn;
		internal System.Windows.Forms.TextBox TxtVehicleShortOn;
		internal System.Windows.Forms.TextBox TxtBreathingApparatusBriefingg262On;
		internal System.Windows.Forms.TextBox TxtBreathingApparatusBriefingG263By;
		internal System.Windows.Forms.TextBox TxtBreathingApparatusG262BriefingBy;
		internal System.Windows.Forms.TextBox TxtVehicleShortBy;
		internal System.Windows.Forms.TextBox TxtVehicleLongBy;
        internal TextBox TxtPersNrFPASS;
        internal TextBox TxtPersNrSmAct;

		//comboboxes
		internal System.Windows.Forms.ComboBox CboCoordinator;
		internal System.Windows.Forms.ComboBox CboExternalContractor;
		internal System.Windows.Forms.ComboBox CboSubcontractor;
		internal System.Windows.Forms.ComboBox CboCraftNumber;
		internal System.Windows.Forms.ComboBox CboDepartment;
		internal System.Windows.Forms.ComboBox CboStatus;
		internal System.Windows.Forms.ComboBox CboPlantThree;
		internal System.Windows.Forms.ComboBox CboPlantTwo;
		internal System.Windows.Forms.ComboBox CboPlantOne;
		internal System.Windows.Forms.ComboBox CboPrecautionaryMedical;

		//radiobuttons
		internal System.Windows.Forms.RadioButton RbtIndustrialSafetyBriefingYes;
		internal System.Windows.Forms.RadioButton RbtIndustrialSafetyBriefingNo;
		internal System.Windows.Forms.RadioButton RbtRespiratoryMaskNo;
		internal System.Windows.Forms.RadioButton RbtRespiratoryMaskYes;
		internal System.Windows.Forms.RadioButton RbtPrecautionaryMedicalYes;
		internal System.Windows.Forms.RadioButton RbtPrecautionaryMedicalNo;
		internal System.Windows.Forms.RadioButton RbtPalletLifterYes;
		internal System.Windows.Forms.RadioButton RbtPalletLifterNo;
		internal System.Windows.Forms.RadioButton RbtRaisablePlatformYes;
		internal System.Windows.Forms.RadioButton RbtRaisablePlatformNo;
		internal System.Windows.Forms.RadioButton RbtCranesYes;
		internal System.Windows.Forms.RadioButton RbtCranesNo;
		internal System.Windows.Forms.RadioButton RbtBriefingPlantNo;
		internal System.Windows.Forms.RadioButton RbtBriefingPlantYes;
		internal System.Windows.Forms.RadioButton RbtSafetyAtWorkServiceBriefingYes;
		internal System.Windows.Forms.RadioButton RbtSafetyAtWorkServiceBriefingNo;
		internal System.Windows.Forms.RadioButton RbtSiteSecurityBriefingNo;
		internal System.Windows.Forms.RadioButton RbtSiteSecurityBriefingYes;
		internal System.Windows.Forms.RadioButton RbtIdHitagYes;
		internal System.Windows.Forms.RadioButton RbtIdHitagNo;
		internal System.Windows.Forms.RadioButton RbtSafetyInstructionsYes;
        internal System.Windows.Forms.RadioButton RbtSafetyInstructionsNo;
		internal System.Windows.Forms.RadioButton RbtBreathingApparatusG263BriefingYes;
		internal System.Windows.Forms.RadioButton RbtBreathingApparatusG263BriefingNo;
		internal System.Windows.Forms.RadioButton RbtAccessAuthorizationYes;
		internal System.Windows.Forms.RadioButton RbtAccessAuthorizationNo;
		internal System.Windows.Forms.RadioButton RbtBreathingApparatusG262BriefingYes;
		internal System.Windows.Forms.RadioButton RbtBreathingApparatusG262BriefingNo;
		internal System.Windows.Forms.RadioButton RbtVehicleLongNo;
		internal System.Windows.Forms.RadioButton RbtVehicleLongYes;
		internal System.Windows.Forms.RadioButton RbtVehicleShortCoYes;
		internal System.Windows.Forms.RadioButton RbtVehicleShortCoNo;
		internal System.Windows.Forms.RadioButton RbtVehicleShortYes;
		internal System.Windows.Forms.RadioButton RbtVehicleShortNo;
		internal System.Windows.Forms.RadioButton RbtVehicleLongCoNo;
		internal System.Windows.Forms.RadioButton RbtVehicleLongCoYes;

		//buttons
		internal System.Windows.Forms.Button BtnClear;
		internal System.Windows.Forms.Button BtnCancel;
		internal System.Windows.Forms.Button ButSearch;

		//tooltips
		private System.Windows.Forms.ToolTip TooSearch;
		private System.Windows.Forms.ToolTip TooClearMask;
		private System.Windows.Forms.ToolTip TooBackTo;

		//Others
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// For the comboboxes "coordinator" and "ext contractor": 
		/// load exco depending on coordinator and vice-versa
		/// </summary>
		private bool mCboCoordinatorIsLeading;
		private bool mCboContractorIsLeading;
		internal System.Windows.Forms.Label LblFireBriefing;
		internal System.Windows.Forms.TextBox TxtFireBriefingOn;
		internal System.Windows.Forms.Panel panel1;
		internal System.Windows.Forms.RadioButton RbtFireBriefingYes;
		internal System.Windows.Forms.RadioButton RbtFireBriefingNo;
		internal System.Windows.Forms.Label label2;
		internal System.Windows.Forms.TextBox TxtFireBriefingBy;
		internal System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label LblIDCardNoHitag;
		internal System.Windows.Forms.TextBox TxtIDCardNoHitag;
        private GroupBox gpBriefings;
        internal Panel pnlIdCards;
        private GroupBox gpIdCards;
        internal TextBox TxtIDCardNoMifare;
        private Label LblIDCardNoMifare;
        private GroupBox gpPrecMed;
        private GroupBox gpPass;
        private GroupBox gpCheckInOff;
        private GroupBox gpMask;
        internal Panel PnlPersNr;
        private GroupBox gpPersNr;
        private Label lblPersNrSmAct;
        private Label lblPersNrFPASS;
        internal Label LblIdPhotoSmAct;
        internal Label LblIdSmActOn;
        internal Panel PnlCtlRbtIdPhotoSmAct;
        internal RadioButton RbtIdSmActYes;
        internal RadioButton RbtIdSmActNo;
        internal TextBox TxtIdSmActBy;
        internal TextBox TxtIdSmActOn;
        internal Label LblIdSmActBy;

		/// <summary>
		/// bool, give true if the date formate is correct 
		/// </summary>
		private bool mCorrectFormat = true;

		#endregion //End of Members

		#region Constructors

		public FrmExtendedSearch()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// Add any initialization after the InitializeComponent call
			MnuFunction.Enabled = false;
            MnuZKS.Enabled = false;
			MnuReports.Enabled = false;

			FillLists();
			SetAuthorization();
		}

		#endregion // End of Construction

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmExtendedSearch));
            this.LblHeadline = new System.Windows.Forms.Label();
            this.PnlDataExternalContractor = new System.Windows.Forms.Panel();
            this.LblStatus = new System.Windows.Forms.Label();
            this.CboStatus = new System.Windows.Forms.ComboBox();
            this.CboPlantThree = new System.Windows.Forms.ComboBox();
            this.CboPlantTwo = new System.Windows.Forms.ComboBox();
            this.CboPlantOne = new System.Windows.Forms.ComboBox();
            this.LblCoordinator = new System.Windows.Forms.Label();
            this.CboCoordinator = new System.Windows.Forms.ComboBox();
            this.LblVehicleRegNumber = new System.Windows.Forms.Label();
            this.LblPlaceOfBirth = new System.Windows.Forms.Label();
            this.LblDateOfBirth = new System.Windows.Forms.Label();
            this.LblFirstname = new System.Windows.Forms.Label();
            this.LblSurname = new System.Windows.Forms.Label();
            this.TxtVehicleRegNumber = new System.Windows.Forms.TextBox();
            this.TxtPlaceOfBirth = new System.Windows.Forms.TextBox();
            this.TxtSurname = new System.Windows.Forms.TextBox();
            this.TxtFirstname = new System.Windows.Forms.TextBox();
            this.TxtDateOfBirth = new System.Windows.Forms.TextBox();
            this.LblExternalContractor = new System.Windows.Forms.Label();
            this.CboExternalContractor = new System.Windows.Forms.ComboBox();
            this.TxtSupervisor = new System.Windows.Forms.TextBox();
            this.LblSupervisor = new System.Windows.Forms.Label();
            this.CboSubcontractor = new System.Windows.Forms.ComboBox();
            this.LblSubcontractor = new System.Windows.Forms.Label();
            this.TxtOrderNumber = new System.Windows.Forms.TextBox();
            this.LblOrderNumber = new System.Windows.Forms.Label();
            this.CboCraftNumber = new System.Windows.Forms.ComboBox();
            this.LblCraftNumber = new System.Windows.Forms.Label();
            this.TxtPhone = new System.Windows.Forms.TextBox();
            this.LblPhone = new System.Windows.Forms.Label();
            this.LblDepartment = new System.Windows.Forms.Label();
            this.CboDepartment = new System.Windows.Forms.ComboBox();
            this.LblPlant = new System.Windows.Forms.Label();
            this.TxtIDCardNoHitag = new System.Windows.Forms.TextBox();
            this.LblIDCardNoHitag = new System.Windows.Forms.Label();
            this.PnlBriefingSite = new System.Windows.Forms.Panel();
            this.gpBriefings = new System.Windows.Forms.GroupBox();
            this.LblIdPhotoSmAct = new System.Windows.Forms.Label();
            this.LblFireBriefing = new System.Windows.Forms.Label();
            this.TxtFireBriefingOn = new System.Windows.Forms.TextBox();
            this.LblIdSmActOn = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.RbtFireBriefingYes = new System.Windows.Forms.RadioButton();
            this.RbtFireBriefingNo = new System.Windows.Forms.RadioButton();
            this.LblSafetyInstructions = new System.Windows.Forms.Label();
            this.PnlCtlRbtIdPhotoSmAct = new System.Windows.Forms.Panel();
            this.RbtIdSmActYes = new System.Windows.Forms.RadioButton();
            this.RbtIdSmActNo = new System.Windows.Forms.RadioButton();
            this.TxtIdSmActBy = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.PnlCtlRbtSafetyInstructions = new System.Windows.Forms.Panel();
            this.RbtSafetyInstructionsYes = new System.Windows.Forms.RadioButton();
            this.RbtSafetyInstructionsNo = new System.Windows.Forms.RadioButton();
            this.TxtIdSmActOn = new System.Windows.Forms.TextBox();
            this.TxtFireBriefingBy = new System.Windows.Forms.TextBox();
            this.LblIndustrialSafetyBriefing = new System.Windows.Forms.Label();
            this.LblIdSmActBy = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.LblSafetyAtWorkServiceBriefing = new System.Windows.Forms.Label();
            this.LblVehicleShort = new System.Windows.Forms.Label();
            this.LblSiteSecurityBriefing = new System.Windows.Forms.Label();
            this.LblVehicleShortCo = new System.Windows.Forms.Label();
            this.LblIdPhotoHitag = new System.Windows.Forms.Label();
            this.PnlVehicleLong = new System.Windows.Forms.Panel();
            this.RbtVehicleLongNo = new System.Windows.Forms.RadioButton();
            this.RbtVehicleLongYes = new System.Windows.Forms.RadioButton();
            this.LblBriefingPlant = new System.Windows.Forms.Label();
            this.PnlVehicleShortCo = new System.Windows.Forms.Panel();
            this.RbtVehicleShortCoYes = new System.Windows.Forms.RadioButton();
            this.RbtVehicleShortCoNo = new System.Windows.Forms.RadioButton();
            this.LblRespiratoryMask = new System.Windows.Forms.Label();
            this.PnlVehicleShort = new System.Windows.Forms.Panel();
            this.RbtVehicleShortYes = new System.Windows.Forms.RadioButton();
            this.RbtVehicleShortNo = new System.Windows.Forms.RadioButton();
            this.LblIndustrialSafetyBriefingOn = new System.Windows.Forms.Label();
            this.PnlVehicleLongCo = new System.Windows.Forms.Panel();
            this.RbtVehicleLongCoNo = new System.Windows.Forms.RadioButton();
            this.RbtVehicleLongCoYes = new System.Windows.Forms.RadioButton();
            this.LblSafetyAtWorkServiceBriefingOn = new System.Windows.Forms.Label();
            this.LblVehicleLong = new System.Windows.Forms.Label();
            this.LblRespiratoryMaskOn = new System.Windows.Forms.Label();
            this.LblVehicleLongCo = new System.Windows.Forms.Label();
            this.LblBriefingPlantOn = new System.Windows.Forms.Label();
            this.TxtAccessAuthorizationOn = new System.Windows.Forms.TextBox();
            this.LblCranesOn = new System.Windows.Forms.Label();
            this.TxtSafetyInstructionsOn = new System.Windows.Forms.TextBox();
            this.LblRaisablePlatformOn = new System.Windows.Forms.Label();
            this.LblPalletLifterOn = new System.Windows.Forms.Label();
            this.LblAccessAuthorizationBy = new System.Windows.Forms.Label();
            this.LblBreathingApparatusBriefingOn = new System.Windows.Forms.Label();
            this.TxtAccessAuthorizationBy = new System.Windows.Forms.TextBox();
            this.LblIdHitagOn = new System.Windows.Forms.Label();
            this.LblAccessAuthorizationOn = new System.Windows.Forms.Label();
            this.LblVehicleLongOn = new System.Windows.Forms.Label();
            this.LblSafetyInstructionsBy = new System.Windows.Forms.Label();
            this.LblVehicleShortOn = new System.Windows.Forms.Label();
            this.LblIndustrialSafetyBriefingBy = new System.Windows.Forms.Label();
            this.TxtSafetyInstructionsBy = new System.Windows.Forms.TextBox();
            this.TxtVehicleLongBy = new System.Windows.Forms.TextBox();
            this.TxtVehicleShortBy = new System.Windows.Forms.TextBox();
            this.LblSafetyInstructionsOn = new System.Windows.Forms.Label();
            this.TxtRespiratoryMaskBy = new System.Windows.Forms.TextBox();
            this.TxtBriefingPlantBy = new System.Windows.Forms.TextBox();
            this.LblBreathingApparatusBriefingG263 = new System.Windows.Forms.Label();
            this.TxtCranesBy = new System.Windows.Forms.TextBox();
            this.TxtBreathingApparatusBriefingG263On = new System.Windows.Forms.TextBox();
            this.TxtRaisablePlatformBy = new System.Windows.Forms.TextBox();
            this.PnlBreathingApparatusBriefingG263 = new System.Windows.Forms.Panel();
            this.RbtBreathingApparatusG263BriefingYes = new System.Windows.Forms.RadioButton();
            this.RbtBreathingApparatusG263BriefingNo = new System.Windows.Forms.RadioButton();
            this.TxtPalletLifterBy = new System.Windows.Forms.TextBox();
            this.LblBreathingApparatusBriefingG263By = new System.Windows.Forms.Label();
            this.TxtBreathingApparatusG262BriefingBy = new System.Windows.Forms.TextBox();
            this.TxtBreathingApparatusBriefingG263By = new System.Windows.Forms.TextBox();
            this.TxtIdHitagBy = new System.Windows.Forms.TextBox();
            this.LblBreathingApparatusBriefingG263On = new System.Windows.Forms.Label();
            this.TxtSafetyAtWorkServiceBriefingBy = new System.Windows.Forms.TextBox();
            this.PnlCtlRbtSafetyAtWork = new System.Windows.Forms.Panel();
            this.RbtIndustrialSafetyBriefingYes = new System.Windows.Forms.RadioButton();
            this.RbtIndustrialSafetyBriefingNo = new System.Windows.Forms.RadioButton();
            this.TxtIndustrialSafetyBriefingBy = new System.Windows.Forms.TextBox();
            this.PnlAccessAuthorization = new System.Windows.Forms.Panel();
            this.RbtAccessAuthorizationYes = new System.Windows.Forms.RadioButton();
            this.RbtAccessAuthorizationNo = new System.Windows.Forms.RadioButton();
            this.LblVehicleLongBy = new System.Windows.Forms.Label();
            this.LblAccessAuthorization = new System.Windows.Forms.Label();
            this.LblVehicleShortBy = new System.Windows.Forms.Label();
            this.LblPalletLifter = new System.Windows.Forms.Label();
            this.LblRespiratoryMaskBy = new System.Windows.Forms.Label();
            this.LblBreathingApparatusBriefingG262 = new System.Windows.Forms.Label();
            this.LblBriefingPlantBy = new System.Windows.Forms.Label();
            this.LblCranes = new System.Windows.Forms.Label();
            this.LblCranesBy = new System.Windows.Forms.Label();
            this.LblRaisablePlatform = new System.Windows.Forms.Label();
            this.LblRaisablePlatformBy = new System.Windows.Forms.Label();
            this.TxtBriefingPlantOn = new System.Windows.Forms.TextBox();
            this.LblPalletLifterBy = new System.Windows.Forms.Label();
            this.TxtCranesOn = new System.Windows.Forms.TextBox();
            this.LblBreathingApparatusG262BriefingBy = new System.Windows.Forms.Label();
            this.TxtVehicleLongOn = new System.Windows.Forms.TextBox();
            this.LblIdHitagBy = new System.Windows.Forms.Label();
            this.TxtVehicleShortOn = new System.Windows.Forms.TextBox();
            this.LblSafetyAtWorkServiceBriefingBy = new System.Windows.Forms.Label();
            this.TxtRespiratoryMaskOn = new System.Windows.Forms.TextBox();
            this.LblSiteSecurityBriefingOn = new System.Windows.Forms.Label();
            this.TxtRaisablePlatformOn = new System.Windows.Forms.TextBox();
            this.TxtSiteSecurityBriefingBy = new System.Windows.Forms.TextBox();
            this.TxtSiteSecurityBriefingOn = new System.Windows.Forms.TextBox();
            this.LblSiteSecurityBriefingBy = new System.Windows.Forms.Label();
            this.TxtIdHitagOn = new System.Windows.Forms.TextBox();
            this.PnlCtlRbtBreathingApparatus = new System.Windows.Forms.Panel();
            this.RbtBreathingApparatusG262BriefingYes = new System.Windows.Forms.RadioButton();
            this.RbtBreathingApparatusG262BriefingNo = new System.Windows.Forms.RadioButton();
            this.TxtBreathingApparatusBriefingg262On = new System.Windows.Forms.TextBox();
            this.PnlCtlRbtIdPhotoHitag = new System.Windows.Forms.Panel();
            this.RbtIdHitagYes = new System.Windows.Forms.RadioButton();
            this.RbtIdHitagNo = new System.Windows.Forms.RadioButton();
            this.TxtPalletLifterOn = new System.Windows.Forms.TextBox();
            this.PnlCtlRbtBriefingSiteSecurity = new System.Windows.Forms.Panel();
            this.RbtSiteSecurityBriefingNo = new System.Windows.Forms.RadioButton();
            this.RbtSiteSecurityBriefingYes = new System.Windows.Forms.RadioButton();
            this.TxtSafetyAtWorkServiceBriefingOn = new System.Windows.Forms.TextBox();
            this.PnlCtlRbtBriefingSafetyAtWork = new System.Windows.Forms.Panel();
            this.RbtSafetyAtWorkServiceBriefingYes = new System.Windows.Forms.RadioButton();
            this.RbtSafetyAtWorkServiceBriefingNo = new System.Windows.Forms.RadioButton();
            this.TxtIndustrialSafetyBriefingOn = new System.Windows.Forms.TextBox();
            this.PnlCtlRbtBriefingPlant = new System.Windows.Forms.Panel();
            this.RbtBriefingPlantNo = new System.Windows.Forms.RadioButton();
            this.RbtBriefingPlantYes = new System.Windows.Forms.RadioButton();
            this.PnlCtlRbtRespiratoryMask = new System.Windows.Forms.Panel();
            this.RbtRespiratoryMaskNo = new System.Windows.Forms.RadioButton();
            this.RbtRespiratoryMaskYes = new System.Windows.Forms.RadioButton();
            this.PnlCtlRbtCranes = new System.Windows.Forms.Panel();
            this.RbtCranesYes = new System.Windows.Forms.RadioButton();
            this.RbtCranesNo = new System.Windows.Forms.RadioButton();
            this.PnlCtlRbtPalletLifter = new System.Windows.Forms.Panel();
            this.RbtPalletLifterYes = new System.Windows.Forms.RadioButton();
            this.RbtPalletLifterNo = new System.Windows.Forms.RadioButton();
            this.PnlCtlRbtRaisablePlatform = new System.Windows.Forms.Panel();
            this.RbtRaisablePlatformYes = new System.Windows.Forms.RadioButton();
            this.RbtRaisablePlatformNo = new System.Windows.Forms.RadioButton();
            this.TxtMaskNumberDelivered = new System.Windows.Forms.TextBox();
            this.TxtMaskNumberRecieve = new System.Windows.Forms.TextBox();
            this.LblDelivered = new System.Windows.Forms.Label();
            this.LblRecieve = new System.Windows.Forms.Label();
            this.LblMaskNumberRecieve = new System.Windows.Forms.Label();
            this.LblMaskNumberDelivered = new System.Windows.Forms.Label();
            this.CboPrecautionaryMedical = new System.Windows.Forms.ComboBox();
            this.BtnClear = new System.Windows.Forms.Button();
            this.TxtCheckInOn = new System.Windows.Forms.TextBox();
            this.TxtCheckOffOn = new System.Windows.Forms.TextBox();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.TxtCheckOffBy = new System.Windows.Forms.TextBox();
            this.TxtCheckInBy = new System.Windows.Forms.TextBox();
            this.LblCheckInBy = new System.Windows.Forms.Label();
            this.LblCheckOffOn = new System.Windows.Forms.Label();
            this.LblCheckOffBy = new System.Windows.Forms.Label();
            this.PnlCtlRbtPrecautionaryMedical = new System.Windows.Forms.Panel();
            this.RbtPrecautionaryMedicalYes = new System.Windows.Forms.RadioButton();
            this.RbtPrecautionaryMedicalNo = new System.Windows.Forms.RadioButton();
            this.ButSearch = new System.Windows.Forms.Button();
            this.LblPrecautionaryMedical = new System.Windows.Forms.Label();
            this.LblDeliveryDate = new System.Windows.Forms.Label();
            this.TxtDeliveryDate = new System.Windows.Forms.TextBox();
            this.LblValidUntil = new System.Windows.Forms.Label();
            this.LblValidFrom = new System.Windows.Forms.Label();
            this.TxtValidFrom = new System.Windows.Forms.TextBox();
            this.TxtValidUntil = new System.Windows.Forms.TextBox();
            this.LblCheckInOn = new System.Windows.Forms.Label();
            this.PnlDataExternalSearch = new System.Windows.Forms.Panel();
            this.PnlPersNr = new System.Windows.Forms.Panel();
            this.gpPersNr = new System.Windows.Forms.GroupBox();
            this.TxtPersNrFPASS = new System.Windows.Forms.TextBox();
            this.lblPersNrSmAct = new System.Windows.Forms.Label();
            this.lblPersNrFPASS = new System.Windows.Forms.Label();
            this.TxtPersNrSmAct = new System.Windows.Forms.TextBox();
            this.pnlIdCards = new System.Windows.Forms.Panel();
            this.gpIdCards = new System.Windows.Forms.GroupBox();
            this.TxtIDCardNoMifare = new System.Windows.Forms.TextBox();
            this.LblIDCardNoMifare = new System.Windows.Forms.Label();
            this.PnlMedical = new System.Windows.Forms.Panel();
            this.gpPrecMed = new System.Windows.Forms.GroupBox();
            this.PnlMask = new System.Windows.Forms.Panel();
            this.gpMask = new System.Windows.Forms.GroupBox();
            this.PnlPass = new System.Windows.Forms.Panel();
            this.gpPass = new System.Windows.Forms.GroupBox();
            this.PnlCheckInOff = new System.Windows.Forms.Panel();
            this.gpCheckInOff = new System.Windows.Forms.GroupBox();
            this.LblMaskTitle = new System.Windows.Forms.Label();
            this.TooSearch = new System.Windows.Forms.ToolTip(this.components);
            this.TooClearMask = new System.Windows.Forms.ToolTip(this.components);
            this.TooBackTo = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.SbpMessage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sbpenvironment)).BeginInit();
            this.PnlDataExternalContractor.SuspendLayout();
            this.PnlBriefingSite.SuspendLayout();
            this.gpBriefings.SuspendLayout();
            this.panel1.SuspendLayout();
            this.PnlCtlRbtIdPhotoSmAct.SuspendLayout();
            this.PnlCtlRbtSafetyInstructions.SuspendLayout();
            this.PnlVehicleLong.SuspendLayout();
            this.PnlVehicleShortCo.SuspendLayout();
            this.PnlVehicleShort.SuspendLayout();
            this.PnlVehicleLongCo.SuspendLayout();
            this.PnlBreathingApparatusBriefingG263.SuspendLayout();
            this.PnlCtlRbtSafetyAtWork.SuspendLayout();
            this.PnlAccessAuthorization.SuspendLayout();
            this.PnlCtlRbtBreathingApparatus.SuspendLayout();
            this.PnlCtlRbtIdPhotoHitag.SuspendLayout();
            this.PnlCtlRbtBriefingSiteSecurity.SuspendLayout();
            this.PnlCtlRbtBriefingSafetyAtWork.SuspendLayout();
            this.PnlCtlRbtBriefingPlant.SuspendLayout();
            this.PnlCtlRbtRespiratoryMask.SuspendLayout();
            this.PnlCtlRbtCranes.SuspendLayout();
            this.PnlCtlRbtPalletLifter.SuspendLayout();
            this.PnlCtlRbtRaisablePlatform.SuspendLayout();
            this.PnlCtlRbtPrecautionaryMedical.SuspendLayout();
            this.PnlDataExternalSearch.SuspendLayout();
            this.PnlPersNr.SuspendLayout();
            this.gpPersNr.SuspendLayout();
            this.pnlIdCards.SuspendLayout();
            this.gpIdCards.SuspendLayout();
            this.PnlMedical.SuspendLayout();
            this.gpPrecMed.SuspendLayout();
            this.PnlMask.SuspendLayout();
            this.gpMask.SuspendLayout();
            this.PnlPass.SuspendLayout();
            this.gpPass.SuspendLayout();
            this.PnlCheckInOff.SuspendLayout();
            this.gpCheckInOff.SuspendLayout();
            this.SuspendLayout();
            // 
            // LblHeadline
            // 
            this.LblHeadline.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.LblHeadline.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblHeadline.Location = new System.Drawing.Point(296, 8);
            this.LblHeadline.Name = "LblHeadline";
            this.LblHeadline.Size = new System.Drawing.Size(376, 32);
            this.LblHeadline.TabIndex = 58;
            this.LblHeadline.Text = "FPASS - Erweiterte Suche";
            this.LblHeadline.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PnlDataExternalContractor
            // 
            this.PnlDataExternalContractor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PnlDataExternalContractor.Controls.Add(this.LblStatus);
            this.PnlDataExternalContractor.Controls.Add(this.CboStatus);
            this.PnlDataExternalContractor.Controls.Add(this.CboPlantThree);
            this.PnlDataExternalContractor.Controls.Add(this.CboPlantTwo);
            this.PnlDataExternalContractor.Controls.Add(this.CboPlantOne);
            this.PnlDataExternalContractor.Controls.Add(this.LblCoordinator);
            this.PnlDataExternalContractor.Controls.Add(this.CboCoordinator);
            this.PnlDataExternalContractor.Controls.Add(this.LblVehicleRegNumber);
            this.PnlDataExternalContractor.Controls.Add(this.LblPlaceOfBirth);
            this.PnlDataExternalContractor.Controls.Add(this.LblDateOfBirth);
            this.PnlDataExternalContractor.Controls.Add(this.LblFirstname);
            this.PnlDataExternalContractor.Controls.Add(this.LblSurname);
            this.PnlDataExternalContractor.Controls.Add(this.TxtVehicleRegNumber);
            this.PnlDataExternalContractor.Controls.Add(this.TxtPlaceOfBirth);
            this.PnlDataExternalContractor.Controls.Add(this.TxtSurname);
            this.PnlDataExternalContractor.Controls.Add(this.TxtFirstname);
            this.PnlDataExternalContractor.Controls.Add(this.TxtDateOfBirth);
            this.PnlDataExternalContractor.Controls.Add(this.LblExternalContractor);
            this.PnlDataExternalContractor.Controls.Add(this.CboExternalContractor);
            this.PnlDataExternalContractor.Controls.Add(this.TxtSupervisor);
            this.PnlDataExternalContractor.Controls.Add(this.LblSupervisor);
            this.PnlDataExternalContractor.Controls.Add(this.CboSubcontractor);
            this.PnlDataExternalContractor.Controls.Add(this.LblSubcontractor);
            this.PnlDataExternalContractor.Controls.Add(this.TxtOrderNumber);
            this.PnlDataExternalContractor.Controls.Add(this.LblOrderNumber);
            this.PnlDataExternalContractor.Controls.Add(this.CboCraftNumber);
            this.PnlDataExternalContractor.Controls.Add(this.LblCraftNumber);
            this.PnlDataExternalContractor.Controls.Add(this.TxtPhone);
            this.PnlDataExternalContractor.Controls.Add(this.LblPhone);
            this.PnlDataExternalContractor.Controls.Add(this.LblDepartment);
            this.PnlDataExternalContractor.Controls.Add(this.CboDepartment);
            this.PnlDataExternalContractor.Controls.Add(this.LblPlant);
            this.PnlDataExternalContractor.Location = new System.Drawing.Point(8, 44);
            this.PnlDataExternalContractor.Name = "PnlDataExternalContractor";
            this.PnlDataExternalContractor.Size = new System.Drawing.Size(1254, 171);
            this.PnlDataExternalContractor.TabIndex = 0;
            // 
            // LblStatus
            // 
            this.LblStatus.BackColor = System.Drawing.SystemColors.Control;
            this.LblStatus.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblStatus.Location = new System.Drawing.Point(793, 38);
            this.LblStatus.Name = "LblStatus";
            this.LblStatus.Size = new System.Drawing.Size(72, 16);
            this.LblStatus.TabIndex = 26;
            this.LblStatus.Text = "Status";
            // 
            // CboStatus
            // 
            this.CboStatus.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboStatus.ItemHeight = 15;
            this.CboStatus.Location = new System.Drawing.Point(873, 36);
            this.CboStatus.Name = "CboStatus";
            this.CboStatus.Size = new System.Drawing.Size(200, 23);
            this.CboStatus.TabIndex = 15;
            // 
            // CboPlantThree
            // 
            this.CboPlantThree.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboPlantThree.ItemHeight = 15;
            this.CboPlantThree.Location = new System.Drawing.Point(873, 108);
            this.CboPlantThree.Name = "CboPlantThree";
            this.CboPlantThree.Size = new System.Drawing.Size(200, 23);
            this.CboPlantThree.TabIndex = 18;
            // 
            // CboPlantTwo
            // 
            this.CboPlantTwo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboPlantTwo.ItemHeight = 15;
            this.CboPlantTwo.Location = new System.Drawing.Point(873, 84);
            this.CboPlantTwo.Name = "CboPlantTwo";
            this.CboPlantTwo.Size = new System.Drawing.Size(200, 23);
            this.CboPlantTwo.TabIndex = 17;
            // 
            // CboPlantOne
            // 
            this.CboPlantOne.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboPlantOne.ItemHeight = 15;
            this.CboPlantOne.Location = new System.Drawing.Point(873, 60);
            this.CboPlantOne.Name = "CboPlantOne";
            this.CboPlantOne.Size = new System.Drawing.Size(200, 23);
            this.CboPlantOne.TabIndex = 16;
            // 
            // LblCoordinator
            // 
            this.LblCoordinator.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCoordinator.Location = new System.Drawing.Point(34, 135);
            this.LblCoordinator.Name = "LblCoordinator";
            this.LblCoordinator.Size = new System.Drawing.Size(80, 21);
            this.LblCoordinator.TabIndex = 22;
            this.LblCoordinator.Text = "Koordinator";
            // 
            // CboCoordinator
            // 
            this.CboCoordinator.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboCoordinator.ItemHeight = 15;
            this.CboCoordinator.Location = new System.Drawing.Point(130, 133);
            this.CboCoordinator.Name = "CboCoordinator";
            this.CboCoordinator.Size = new System.Drawing.Size(200, 23);
            this.CboCoordinator.TabIndex = 6;
            // 
            // LblVehicleRegNumber
            // 
            this.LblVehicleRegNumber.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblVehicleRegNumber.Location = new System.Drawing.Point(400, 111);
            this.LblVehicleRegNumber.Name = "LblVehicleRegNumber";
            this.LblVehicleRegNumber.Size = new System.Drawing.Size(112, 21);
            this.LblVehicleRegNumber.TabIndex = 15;
            this.LblVehicleRegNumber.Text = "Kfz-Kennzeichen";
            // 
            // LblPlaceOfBirth
            // 
            this.LblPlaceOfBirth.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblPlaceOfBirth.Location = new System.Drawing.Point(34, 87);
            this.LblPlaceOfBirth.Name = "LblPlaceOfBirth";
            this.LblPlaceOfBirth.Size = new System.Drawing.Size(96, 21);
            this.LblPlaceOfBirth.TabIndex = 14;
            this.LblPlaceOfBirth.Text = "Geburtsort";
            // 
            // LblDateOfBirth
            // 
            this.LblDateOfBirth.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblDateOfBirth.Location = new System.Drawing.Point(34, 63);
            this.LblDateOfBirth.Name = "LblDateOfBirth";
            this.LblDateOfBirth.Size = new System.Drawing.Size(96, 21);
            this.LblDateOfBirth.TabIndex = 13;
            this.LblDateOfBirth.Text = "Geburtsdatum";
            // 
            // LblFirstname
            // 
            this.LblFirstname.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblFirstname.Location = new System.Drawing.Point(34, 39);
            this.LblFirstname.Name = "LblFirstname";
            this.LblFirstname.Size = new System.Drawing.Size(96, 21);
            this.LblFirstname.TabIndex = 12;
            this.LblFirstname.Text = "Vorname";
            // 
            // LblSurname
            // 
            this.LblSurname.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSurname.Location = new System.Drawing.Point(34, 15);
            this.LblSurname.Name = "LblSurname";
            this.LblSurname.Size = new System.Drawing.Size(96, 21);
            this.LblSurname.TabIndex = 11;
            this.LblSurname.Text = "Nachname";
            // 
            // TxtVehicleRegNumber
            // 
            this.TxtVehicleRegNumber.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtVehicleRegNumber.Location = new System.Drawing.Point(512, 109);
            this.TxtVehicleRegNumber.Name = "TxtVehicleRegNumber";
            this.TxtVehicleRegNumber.Size = new System.Drawing.Size(200, 21);
            this.TxtVehicleRegNumber.TabIndex = 11;
            // 
            // TxtPlaceOfBirth
            // 
            this.TxtPlaceOfBirth.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPlaceOfBirth.Location = new System.Drawing.Point(130, 85);
            this.TxtPlaceOfBirth.Name = "TxtPlaceOfBirth";
            this.TxtPlaceOfBirth.Size = new System.Drawing.Size(200, 21);
            this.TxtPlaceOfBirth.TabIndex = 4;
            // 
            // TxtSurname
            // 
            this.TxtSurname.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSurname.Location = new System.Drawing.Point(130, 13);
            this.TxtSurname.Name = "TxtSurname";
            this.TxtSurname.Size = new System.Drawing.Size(200, 21);
            this.TxtSurname.TabIndex = 1;
            // 
            // TxtFirstname
            // 
            this.TxtFirstname.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtFirstname.Location = new System.Drawing.Point(130, 37);
            this.TxtFirstname.Name = "TxtFirstname";
            this.TxtFirstname.Size = new System.Drawing.Size(200, 21);
            this.TxtFirstname.TabIndex = 2;
            // 
            // TxtDateOfBirth
            // 
            this.TxtDateOfBirth.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtDateOfBirth.Location = new System.Drawing.Point(130, 61);
            this.TxtDateOfBirth.Name = "TxtDateOfBirth";
            this.TxtDateOfBirth.Size = new System.Drawing.Size(200, 21);
            this.TxtDateOfBirth.TabIndex = 3;
            this.TxtDateOfBirth.Leave += new System.EventHandler(this.TxtDateOfBirth_Leave);
            // 
            // LblExternalContractor
            // 
            this.LblExternalContractor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblExternalContractor.Location = new System.Drawing.Point(34, 111);
            this.LblExternalContractor.Name = "LblExternalContractor";
            this.LblExternalContractor.Size = new System.Drawing.Size(80, 21);
            this.LblExternalContractor.TabIndex = 1;
            this.LblExternalContractor.Text = "Fremdfirma";
            // 
            // CboExternalContractor
            // 
            this.CboExternalContractor.AllowDrop = true;
            this.CboExternalContractor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboExternalContractor.ItemHeight = 15;
            this.CboExternalContractor.Location = new System.Drawing.Point(130, 109);
            this.CboExternalContractor.Name = "CboExternalContractor";
            this.CboExternalContractor.Size = new System.Drawing.Size(200, 23);
            this.CboExternalContractor.TabIndex = 5;
            // 
            // TxtSupervisor
            // 
            this.TxtSupervisor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSupervisor.Location = new System.Drawing.Point(512, 13);
            this.TxtSupervisor.Name = "TxtSupervisor";
            this.TxtSupervisor.Size = new System.Drawing.Size(200, 21);
            this.TxtSupervisor.TabIndex = 7;
            // 
            // LblSupervisor
            // 
            this.LblSupervisor.BackColor = System.Drawing.SystemColors.Control;
            this.LblSupervisor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSupervisor.Location = new System.Drawing.Point(400, 15);
            this.LblSupervisor.Name = "LblSupervisor";
            this.LblSupervisor.Size = new System.Drawing.Size(96, 16);
            this.LblSupervisor.TabIndex = 5;
            this.LblSupervisor.Text = "Baustellenleiter";
            // 
            // CboSubcontractor
            // 
            this.CboSubcontractor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboSubcontractor.ItemHeight = 15;
            this.CboSubcontractor.Location = new System.Drawing.Point(512, 37);
            this.CboSubcontractor.Name = "CboSubcontractor";
            this.CboSubcontractor.Size = new System.Drawing.Size(200, 23);
            this.CboSubcontractor.TabIndex = 8;
            // 
            // LblSubcontractor
            // 
            this.LblSubcontractor.BackColor = System.Drawing.SystemColors.Control;
            this.LblSubcontractor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSubcontractor.Location = new System.Drawing.Point(400, 39);
            this.LblSubcontractor.Name = "LblSubcontractor";
            this.LblSubcontractor.Size = new System.Drawing.Size(96, 16);
            this.LblSubcontractor.TabIndex = 6;
            this.LblSubcontractor.Text = "Subfirma";
            // 
            // TxtOrderNumber
            // 
            this.TxtOrderNumber.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtOrderNumber.Location = new System.Drawing.Point(512, 85);
            this.TxtOrderNumber.Name = "TxtOrderNumber";
            this.TxtOrderNumber.Size = new System.Drawing.Size(200, 21);
            this.TxtOrderNumber.TabIndex = 10;
            // 
            // LblOrderNumber
            // 
            this.LblOrderNumber.BackColor = System.Drawing.SystemColors.Control;
            this.LblOrderNumber.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblOrderNumber.Location = new System.Drawing.Point(400, 87);
            this.LblOrderNumber.Name = "LblOrderNumber";
            this.LblOrderNumber.Size = new System.Drawing.Size(96, 16);
            this.LblOrderNumber.TabIndex = 0;
            this.LblOrderNumber.Text = "Auftrags-Nr.";
            // 
            // CboCraftNumber
            // 
            this.CboCraftNumber.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboCraftNumber.ItemHeight = 15;
            this.CboCraftNumber.Location = new System.Drawing.Point(512, 61);
            this.CboCraftNumber.Name = "CboCraftNumber";
            this.CboCraftNumber.Size = new System.Drawing.Size(200, 23);
            this.CboCraftNumber.TabIndex = 9;
            // 
            // LblCraftNumber
            // 
            this.LblCraftNumber.BackColor = System.Drawing.SystemColors.Control;
            this.LblCraftNumber.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCraftNumber.Location = new System.Drawing.Point(400, 63);
            this.LblCraftNumber.Name = "LblCraftNumber";
            this.LblCraftNumber.Size = new System.Drawing.Size(96, 16);
            this.LblCraftNumber.TabIndex = 1;
            this.LblCraftNumber.Text = "Gewerk-Nr.";
            // 
            // TxtPhone
            // 
            this.TxtPhone.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPhone.Location = new System.Drawing.Point(512, 133);
            this.TxtPhone.Name = "TxtPhone";
            this.TxtPhone.Size = new System.Drawing.Size(200, 21);
            this.TxtPhone.TabIndex = 12;
            // 
            // LblPhone
            // 
            this.LblPhone.BackColor = System.Drawing.SystemColors.Control;
            this.LblPhone.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblPhone.Location = new System.Drawing.Point(400, 135);
            this.LblPhone.Name = "LblPhone";
            this.LblPhone.Size = new System.Drawing.Size(96, 16);
            this.LblPhone.TabIndex = 4;
            this.LblPhone.Text = "Telefon";
            // 
            // LblDepartment
            // 
            this.LblDepartment.BackColor = System.Drawing.SystemColors.Control;
            this.LblDepartment.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblDepartment.Location = new System.Drawing.Point(793, 14);
            this.LblDepartment.Name = "LblDepartment";
            this.LblDepartment.Size = new System.Drawing.Size(72, 16);
            this.LblDepartment.TabIndex = 3;
            this.LblDepartment.Text = "Abteilung";
            // 
            // CboDepartment
            // 
            this.CboDepartment.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboDepartment.ItemHeight = 15;
            this.CboDepartment.Location = new System.Drawing.Point(873, 12);
            this.CboDepartment.Name = "CboDepartment";
            this.CboDepartment.Size = new System.Drawing.Size(200, 23);
            this.CboDepartment.TabIndex = 14;
            // 
            // LblPlant
            // 
            this.LblPlant.BackColor = System.Drawing.SystemColors.Control;
            this.LblPlant.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblPlant.Location = new System.Drawing.Point(793, 62);
            this.LblPlant.Name = "LblPlant";
            this.LblPlant.Size = new System.Drawing.Size(64, 16);
            this.LblPlant.TabIndex = 2;
            this.LblPlant.Text = "Betrieb";
            // 
            // TxtIDCardNoHitag
            // 
            this.TxtIDCardNoHitag.Font = new System.Drawing.Font("Arial", 9F);
            this.TxtIDCardNoHitag.Location = new System.Drawing.Point(221, 19);
            this.TxtIDCardNoHitag.Name = "TxtIDCardNoHitag";
            this.TxtIDCardNoHitag.Size = new System.Drawing.Size(195, 21);
            this.TxtIDCardNoHitag.TabIndex = 13;
            // 
            // LblIDCardNoHitag
            // 
            this.LblIDCardNoHitag.Font = new System.Drawing.Font("Arial", 9F);
            this.LblIDCardNoHitag.Location = new System.Drawing.Point(14, 22);
            this.LblIDCardNoHitag.Name = "LblIDCardNoHitag";
            this.LblIDCardNoHitag.Size = new System.Drawing.Size(166, 23);
            this.LblIDCardNoHitag.TabIndex = 27;
            this.LblIDCardNoHitag.Text = "Ausweisnummer Hitag2";
            // 
            // PnlBriefingSite
            // 
            this.PnlBriefingSite.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlBriefingSite.Controls.Add(this.gpBriefings);
            this.PnlBriefingSite.Location = new System.Drawing.Point(20, 233);
            this.PnlBriefingSite.Name = "PnlBriefingSite";
            this.PnlBriefingSite.Size = new System.Drawing.Size(741, 550);
            this.PnlBriefingSite.TabIndex = 0;
            // 
            // gpBriefings
            // 
            this.gpBriefings.Controls.Add(this.LblIdPhotoSmAct);
            this.gpBriefings.Controls.Add(this.LblFireBriefing);
            this.gpBriefings.Controls.Add(this.TxtFireBriefingOn);
            this.gpBriefings.Controls.Add(this.LblIdSmActOn);
            this.gpBriefings.Controls.Add(this.panel1);
            this.gpBriefings.Controls.Add(this.LblSafetyInstructions);
            this.gpBriefings.Controls.Add(this.PnlCtlRbtIdPhotoSmAct);
            this.gpBriefings.Controls.Add(this.TxtIdSmActBy);
            this.gpBriefings.Controls.Add(this.label2);
            this.gpBriefings.Controls.Add(this.PnlCtlRbtSafetyInstructions);
            this.gpBriefings.Controls.Add(this.TxtIdSmActOn);
            this.gpBriefings.Controls.Add(this.TxtFireBriefingBy);
            this.gpBriefings.Controls.Add(this.LblIndustrialSafetyBriefing);
            this.gpBriefings.Controls.Add(this.LblIdSmActBy);
            this.gpBriefings.Controls.Add(this.label3);
            this.gpBriefings.Controls.Add(this.LblSafetyAtWorkServiceBriefing);
            this.gpBriefings.Controls.Add(this.LblVehicleShort);
            this.gpBriefings.Controls.Add(this.LblSiteSecurityBriefing);
            this.gpBriefings.Controls.Add(this.LblVehicleShortCo);
            this.gpBriefings.Controls.Add(this.LblIdPhotoHitag);
            this.gpBriefings.Controls.Add(this.PnlVehicleLong);
            this.gpBriefings.Controls.Add(this.LblBriefingPlant);
            this.gpBriefings.Controls.Add(this.PnlVehicleShortCo);
            this.gpBriefings.Controls.Add(this.LblRespiratoryMask);
            this.gpBriefings.Controls.Add(this.PnlVehicleShort);
            this.gpBriefings.Controls.Add(this.LblIndustrialSafetyBriefingOn);
            this.gpBriefings.Controls.Add(this.PnlVehicleLongCo);
            this.gpBriefings.Controls.Add(this.LblSafetyAtWorkServiceBriefingOn);
            this.gpBriefings.Controls.Add(this.LblVehicleLong);
            this.gpBriefings.Controls.Add(this.LblRespiratoryMaskOn);
            this.gpBriefings.Controls.Add(this.LblVehicleLongCo);
            this.gpBriefings.Controls.Add(this.LblBriefingPlantOn);
            this.gpBriefings.Controls.Add(this.TxtAccessAuthorizationOn);
            this.gpBriefings.Controls.Add(this.LblCranesOn);
            this.gpBriefings.Controls.Add(this.TxtSafetyInstructionsOn);
            this.gpBriefings.Controls.Add(this.LblRaisablePlatformOn);
            this.gpBriefings.Controls.Add(this.LblPalletLifterOn);
            this.gpBriefings.Controls.Add(this.LblAccessAuthorizationBy);
            this.gpBriefings.Controls.Add(this.LblBreathingApparatusBriefingOn);
            this.gpBriefings.Controls.Add(this.TxtAccessAuthorizationBy);
            this.gpBriefings.Controls.Add(this.LblIdHitagOn);
            this.gpBriefings.Controls.Add(this.LblAccessAuthorizationOn);
            this.gpBriefings.Controls.Add(this.LblVehicleLongOn);
            this.gpBriefings.Controls.Add(this.LblSafetyInstructionsBy);
            this.gpBriefings.Controls.Add(this.LblVehicleShortOn);
            this.gpBriefings.Controls.Add(this.LblIndustrialSafetyBriefingBy);
            this.gpBriefings.Controls.Add(this.TxtSafetyInstructionsBy);
            this.gpBriefings.Controls.Add(this.TxtVehicleLongBy);
            this.gpBriefings.Controls.Add(this.TxtVehicleShortBy);
            this.gpBriefings.Controls.Add(this.LblSafetyInstructionsOn);
            this.gpBriefings.Controls.Add(this.TxtRespiratoryMaskBy);
            this.gpBriefings.Controls.Add(this.TxtBriefingPlantBy);
            this.gpBriefings.Controls.Add(this.LblBreathingApparatusBriefingG263);
            this.gpBriefings.Controls.Add(this.TxtCranesBy);
            this.gpBriefings.Controls.Add(this.TxtBreathingApparatusBriefingG263On);
            this.gpBriefings.Controls.Add(this.TxtRaisablePlatformBy);
            this.gpBriefings.Controls.Add(this.PnlBreathingApparatusBriefingG263);
            this.gpBriefings.Controls.Add(this.TxtPalletLifterBy);
            this.gpBriefings.Controls.Add(this.LblBreathingApparatusBriefingG263By);
            this.gpBriefings.Controls.Add(this.TxtBreathingApparatusG262BriefingBy);
            this.gpBriefings.Controls.Add(this.TxtBreathingApparatusBriefingG263By);
            this.gpBriefings.Controls.Add(this.TxtIdHitagBy);
            this.gpBriefings.Controls.Add(this.LblBreathingApparatusBriefingG263On);
            this.gpBriefings.Controls.Add(this.TxtSafetyAtWorkServiceBriefingBy);
            this.gpBriefings.Controls.Add(this.PnlCtlRbtSafetyAtWork);
            this.gpBriefings.Controls.Add(this.TxtIndustrialSafetyBriefingBy);
            this.gpBriefings.Controls.Add(this.PnlAccessAuthorization);
            this.gpBriefings.Controls.Add(this.LblVehicleLongBy);
            this.gpBriefings.Controls.Add(this.LblAccessAuthorization);
            this.gpBriefings.Controls.Add(this.LblVehicleShortBy);
            this.gpBriefings.Controls.Add(this.LblPalletLifter);
            this.gpBriefings.Controls.Add(this.LblRespiratoryMaskBy);
            this.gpBriefings.Controls.Add(this.LblBreathingApparatusBriefingG262);
            this.gpBriefings.Controls.Add(this.LblBriefingPlantBy);
            this.gpBriefings.Controls.Add(this.LblCranes);
            this.gpBriefings.Controls.Add(this.LblCranesBy);
            this.gpBriefings.Controls.Add(this.LblRaisablePlatform);
            this.gpBriefings.Controls.Add(this.LblRaisablePlatformBy);
            this.gpBriefings.Controls.Add(this.TxtBriefingPlantOn);
            this.gpBriefings.Controls.Add(this.LblPalletLifterBy);
            this.gpBriefings.Controls.Add(this.TxtCranesOn);
            this.gpBriefings.Controls.Add(this.LblBreathingApparatusG262BriefingBy);
            this.gpBriefings.Controls.Add(this.TxtVehicleLongOn);
            this.gpBriefings.Controls.Add(this.LblIdHitagBy);
            this.gpBriefings.Controls.Add(this.TxtVehicleShortOn);
            this.gpBriefings.Controls.Add(this.LblSafetyAtWorkServiceBriefingBy);
            this.gpBriefings.Controls.Add(this.TxtRespiratoryMaskOn);
            this.gpBriefings.Controls.Add(this.LblSiteSecurityBriefingOn);
            this.gpBriefings.Controls.Add(this.TxtRaisablePlatformOn);
            this.gpBriefings.Controls.Add(this.TxtSiteSecurityBriefingBy);
            this.gpBriefings.Controls.Add(this.TxtSiteSecurityBriefingOn);
            this.gpBriefings.Controls.Add(this.LblSiteSecurityBriefingBy);
            this.gpBriefings.Controls.Add(this.TxtIdHitagOn);
            this.gpBriefings.Controls.Add(this.PnlCtlRbtBreathingApparatus);
            this.gpBriefings.Controls.Add(this.TxtBreathingApparatusBriefingg262On);
            this.gpBriefings.Controls.Add(this.PnlCtlRbtIdPhotoHitag);
            this.gpBriefings.Controls.Add(this.TxtPalletLifterOn);
            this.gpBriefings.Controls.Add(this.PnlCtlRbtBriefingSiteSecurity);
            this.gpBriefings.Controls.Add(this.TxtSafetyAtWorkServiceBriefingOn);
            this.gpBriefings.Controls.Add(this.PnlCtlRbtBriefingSafetyAtWork);
            this.gpBriefings.Controls.Add(this.TxtIndustrialSafetyBriefingOn);
            this.gpBriefings.Controls.Add(this.PnlCtlRbtBriefingPlant);
            this.gpBriefings.Controls.Add(this.PnlCtlRbtRespiratoryMask);
            this.gpBriefings.Controls.Add(this.PnlCtlRbtCranes);
            this.gpBriefings.Controls.Add(this.PnlCtlRbtPalletLifter);
            this.gpBriefings.Controls.Add(this.PnlCtlRbtRaisablePlatform);
            this.gpBriefings.Location = new System.Drawing.Point(12, 6);
            this.gpBriefings.Name = "gpBriefings";
            this.gpBriefings.Size = new System.Drawing.Size(719, 533);
            this.gpBriefings.TabIndex = 81;
            this.gpBriefings.TabStop = false;
            this.gpBriefings.Text = "Belehrungen";
            // 
            // LblIdPhotoSmAct
            // 
            this.LblIdPhotoSmAct.BackColor = System.Drawing.SystemColors.Control;
            this.LblIdPhotoSmAct.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblIdPhotoSmAct.Location = new System.Drawing.Point(131, 154);
            this.LblIdPhotoSmAct.Name = "LblIdPhotoSmAct";
            this.LblIdPhotoSmAct.Size = new System.Drawing.Size(208, 16);
            this.LblIdPhotoSmAct.TabIndex = 117;
            this.LblIdPhotoSmAct.Text = "Lichtbildausweis SmartAct";
            // 
            // LblFireBriefing
            // 
            this.LblFireBriefing.BackColor = System.Drawing.SystemColors.Control;
            this.LblFireBriefing.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblFireBriefing.Location = new System.Drawing.Point(131, 261);
            this.LblFireBriefing.Name = "LblFireBriefing";
            this.LblFireBriefing.Size = new System.Drawing.Size(144, 16);
            this.LblFireBriefing.TabIndex = 199;
            this.LblFireBriefing.Text = "Brandsicherheitsposten";
            // 
            // TxtFireBriefingOn
            // 
            this.TxtFireBriefingOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtFireBriefingOn.Location = new System.Drawing.Point(419, 261);
            this.TxtFireBriefingOn.Name = "TxtFireBriefingOn";
            this.TxtFireBriefingOn.Size = new System.Drawing.Size(72, 21);
            this.TxtFireBriefingOn.TabIndex = 49;
            this.TxtFireBriefingOn.Leave += new System.EventHandler(this.TxtFireBriefingOn_Leave);
            // 
            // LblIdSmActOn
            // 
            this.LblIdSmActOn.BackColor = System.Drawing.SystemColors.Control;
            this.LblIdSmActOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblIdSmActOn.Location = new System.Drawing.Point(387, 154);
            this.LblIdSmActOn.Name = "LblIdSmActOn";
            this.LblIdSmActOn.Size = new System.Drawing.Size(32, 16);
            this.LblIdSmActOn.TabIndex = 120;
            this.LblIdSmActOn.Text = "am";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.RbtFireBriefingYes);
            this.panel1.Controls.Add(this.RbtFireBriefingNo);
            this.panel1.Location = new System.Drawing.Point(8, 254);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(112, 32);
            this.panel1.TabIndex = 198;
            // 
            // RbtFireBriefingYes
            // 
            this.RbtFireBriefingYes.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtFireBriefingYes.Location = new System.Drawing.Point(8, 8);
            this.RbtFireBriefingYes.Name = "RbtFireBriefingYes";
            this.RbtFireBriefingYes.Size = new System.Drawing.Size(40, 16);
            this.RbtFireBriefingYes.TabIndex = 48;
            this.RbtFireBriefingYes.Text = "Ja";
            // 
            // RbtFireBriefingNo
            // 
            this.RbtFireBriefingNo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtFireBriefingNo.Location = new System.Drawing.Point(48, 8);
            this.RbtFireBriefingNo.Name = "RbtFireBriefingNo";
            this.RbtFireBriefingNo.Size = new System.Drawing.Size(48, 16);
            this.RbtFireBriefingNo.TabIndex = 0;
            this.RbtFireBriefingNo.Text = "Nein";
            // 
            // LblSafetyInstructions
            // 
            this.LblSafetyInstructions.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSafetyInstructions.Location = new System.Drawing.Point(131, 27);
            this.LblSafetyInstructions.Name = "LblSafetyInstructions";
            this.LblSafetyInstructions.Size = new System.Drawing.Size(200, 16);
            this.LblSafetyInstructions.TabIndex = 44;
            this.LblSafetyInstructions.Text = "Sicherheitshinweis Empfang";
            // 
            // PnlCtlRbtIdPhotoSmAct
            // 
            this.PnlCtlRbtIdPhotoSmAct.Controls.Add(this.RbtIdSmActYes);
            this.PnlCtlRbtIdPhotoSmAct.Controls.Add(this.RbtIdSmActNo);
            this.PnlCtlRbtIdPhotoSmAct.Location = new System.Drawing.Point(8, 148);
            this.PnlCtlRbtIdPhotoSmAct.Name = "PnlCtlRbtIdPhotoSmAct";
            this.PnlCtlRbtIdPhotoSmAct.Size = new System.Drawing.Size(96, 24);
            this.PnlCtlRbtIdPhotoSmAct.TabIndex = 116;
            // 
            // RbtIdSmActYes
            // 
            this.RbtIdSmActYes.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtIdSmActYes.Location = new System.Drawing.Point(8, 5);
            this.RbtIdSmActYes.Name = "RbtIdSmActYes";
            this.RbtIdSmActYes.Size = new System.Drawing.Size(40, 16);
            this.RbtIdSmActYes.TabIndex = 36;
            this.RbtIdSmActYes.Text = "Ja";
            // 
            // RbtIdSmActNo
            // 
            this.RbtIdSmActNo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtIdSmActNo.Location = new System.Drawing.Point(48, 5);
            this.RbtIdSmActNo.Name = "RbtIdSmActNo";
            this.RbtIdSmActNo.Size = new System.Drawing.Size(48, 16);
            this.RbtIdSmActNo.TabIndex = 0;
            this.RbtIdSmActNo.Text = "Nein";
            // 
            // TxtIdSmActBy
            // 
            this.TxtIdSmActBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtIdSmActBy.Location = new System.Drawing.Point(555, 154);
            this.TxtIdSmActBy.Name = "TxtIdSmActBy";
            this.TxtIdSmActBy.Size = new System.Drawing.Size(128, 21);
            this.TxtIdSmActBy.TabIndex = 38;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(507, 261);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 16);
            this.label2.TabIndex = 203;
            this.label2.Text = "durch";
            // 
            // PnlCtlRbtSafetyInstructions
            // 
            this.PnlCtlRbtSafetyInstructions.Controls.Add(this.RbtSafetyInstructionsYes);
            this.PnlCtlRbtSafetyInstructions.Controls.Add(this.RbtSafetyInstructionsNo);
            this.PnlCtlRbtSafetyInstructions.Location = new System.Drawing.Point(16, 19);
            this.PnlCtlRbtSafetyInstructions.Name = "PnlCtlRbtSafetyInstructions";
            this.PnlCtlRbtSafetyInstructions.Size = new System.Drawing.Size(88, 24);
            this.PnlCtlRbtSafetyInstructions.TabIndex = 0;
            // 
            // RbtSafetyInstructionsYes
            // 
            this.RbtSafetyInstructionsYes.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtSafetyInstructionsYes.Location = new System.Drawing.Point(0, 8);
            this.RbtSafetyInstructionsYes.Name = "RbtSafetyInstructionsYes";
            this.RbtSafetyInstructionsYes.Size = new System.Drawing.Size(40, 16);
            this.RbtSafetyInstructionsYes.TabIndex = 21;
            this.RbtSafetyInstructionsYes.Text = "Ja";
            // 
            // RbtSafetyInstructionsNo
            // 
            this.RbtSafetyInstructionsNo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtSafetyInstructionsNo.Location = new System.Drawing.Point(40, 8);
            this.RbtSafetyInstructionsNo.Name = "RbtSafetyInstructionsNo";
            this.RbtSafetyInstructionsNo.Size = new System.Drawing.Size(48, 16);
            this.RbtSafetyInstructionsNo.TabIndex = 0;
            this.RbtSafetyInstructionsNo.Text = "Nein";
            // 
            // TxtIdSmActOn
            // 
            this.TxtIdSmActOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtIdSmActOn.Location = new System.Drawing.Point(419, 154);
            this.TxtIdSmActOn.Name = "TxtIdSmActOn";
            this.TxtIdSmActOn.Size = new System.Drawing.Size(72, 21);
            this.TxtIdSmActOn.TabIndex = 37;
            this.TxtIdSmActOn.Leave += new System.EventHandler(this.TxtIdSmActOn_Leave);
            // 
            // TxtFireBriefingBy
            // 
            this.TxtFireBriefingBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtFireBriefingBy.Location = new System.Drawing.Point(555, 261);
            this.TxtFireBriefingBy.Name = "TxtFireBriefingBy";
            this.TxtFireBriefingBy.Size = new System.Drawing.Size(128, 21);
            this.TxtFireBriefingBy.TabIndex = 50;
            // 
            // LblIndustrialSafetyBriefing
            // 
            this.LblIndustrialSafetyBriefing.BackColor = System.Drawing.SystemColors.Control;
            this.LblIndustrialSafetyBriefing.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblIndustrialSafetyBriefing.Location = new System.Drawing.Point(131, 78);
            this.LblIndustrialSafetyBriefing.Name = "LblIndustrialSafetyBriefing";
            this.LblIndustrialSafetyBriefing.Size = new System.Drawing.Size(224, 16);
            this.LblIndustrialSafetyBriefing.TabIndex = 7;
            this.LblIndustrialSafetyBriefing.Text = "Unterweisung Arbeitssicherheit Werk";
            // 
            // LblIdSmActBy
            // 
            this.LblIdSmActBy.BackColor = System.Drawing.SystemColors.Control;
            this.LblIdSmActBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblIdSmActBy.Location = new System.Drawing.Point(507, 154);
            this.LblIdSmActBy.Name = "LblIdSmActBy";
            this.LblIdSmActBy.Size = new System.Drawing.Size(40, 16);
            this.LblIdSmActBy.TabIndex = 121;
            this.LblIdSmActBy.Text = "durch";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(387, 261);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 16);
            this.label3.TabIndex = 202;
            this.label3.Text = "am";
            // 
            // LblSafetyAtWorkServiceBriefing
            // 
            this.LblSafetyAtWorkServiceBriefing.BackColor = System.Drawing.SystemColors.Control;
            this.LblSafetyAtWorkServiceBriefing.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSafetyAtWorkServiceBriefing.Location = new System.Drawing.Point(131, 102);
            this.LblSafetyAtWorkServiceBriefing.Name = "LblSafetyAtWorkServiceBriefing";
            this.LblSafetyAtWorkServiceBriefing.Size = new System.Drawing.Size(224, 16);
            this.LblSafetyAtWorkServiceBriefing.TabIndex = 8;
            this.LblSafetyAtWorkServiceBriefing.Text = "Belehrung durch Abteilung SUW-AS";
            // 
            // LblVehicleShort
            // 
            this.LblVehicleShort.BackColor = System.Drawing.SystemColors.Control;
            this.LblVehicleShort.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblVehicleShort.Location = new System.Drawing.Point(131, 446);
            this.LblVehicleShort.Name = "LblVehicleShort";
            this.LblVehicleShort.Size = new System.Drawing.Size(232, 16);
            this.LblVehicleShort.TabIndex = 195;
            this.LblVehicleShort.Text = "Kfz-Einfahrt kurz gewährt";
            // 
            // LblSiteSecurityBriefing
            // 
            this.LblSiteSecurityBriefing.BackColor = System.Drawing.SystemColors.Control;
            this.LblSiteSecurityBriefing.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiteSecurityBriefing.Location = new System.Drawing.Point(131, 126);
            this.LblSiteSecurityBriefing.Name = "LblSiteSecurityBriefing";
            this.LblSiteSecurityBriefing.Size = new System.Drawing.Size(208, 16);
            this.LblSiteSecurityBriefing.TabIndex = 12;
            this.LblSiteSecurityBriefing.Text = "Werksicherheitsbelehrung";
            // 
            // LblVehicleShortCo
            // 
            this.LblVehicleShortCo.BackColor = System.Drawing.SystemColors.Control;
            this.LblVehicleShortCo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblVehicleShortCo.Location = new System.Drawing.Point(131, 423);
            this.LblVehicleShortCo.Name = "LblVehicleShortCo";
            this.LblVehicleShortCo.Size = new System.Drawing.Size(232, 16);
            this.LblVehicleShortCo.TabIndex = 194;
            this.LblVehicleShortCo.Text = "Kfz-Einfahrt kurz gewünscht";
            // 
            // LblIdPhotoHitag
            // 
            this.LblIdPhotoHitag.BackColor = System.Drawing.SystemColors.Control;
            this.LblIdPhotoHitag.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblIdPhotoHitag.Location = new System.Drawing.Point(131, 178);
            this.LblIdPhotoHitag.Name = "LblIdPhotoHitag";
            this.LblIdPhotoHitag.Size = new System.Drawing.Size(208, 16);
            this.LblIdPhotoHitag.TabIndex = 13;
            this.LblIdPhotoHitag.Text = "Lichtbildausweis Hitag";
            // 
            // PnlVehicleLong
            // 
            this.PnlVehicleLong.Controls.Add(this.RbtVehicleLongNo);
            this.PnlVehicleLong.Controls.Add(this.RbtVehicleLongYes);
            this.PnlVehicleLong.Location = new System.Drawing.Point(8, 494);
            this.PnlVehicleLong.Name = "PnlVehicleLong";
            this.PnlVehicleLong.Size = new System.Drawing.Size(96, 16);
            this.PnlVehicleLong.TabIndex = 0;
            // 
            // RbtVehicleLongNo
            // 
            this.RbtVehicleLongNo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtVehicleLongNo.Location = new System.Drawing.Point(48, 0);
            this.RbtVehicleLongNo.Name = "RbtVehicleLongNo";
            this.RbtVehicleLongNo.Size = new System.Drawing.Size(48, 16);
            this.RbtVehicleLongNo.TabIndex = 0;
            this.RbtVehicleLongNo.Text = "Nein";
            // 
            // RbtVehicleLongYes
            // 
            this.RbtVehicleLongYes.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtVehicleLongYes.Location = new System.Drawing.Point(8, 0);
            this.RbtVehicleLongYes.Name = "RbtVehicleLongYes";
            this.RbtVehicleLongYes.Size = new System.Drawing.Size(40, 16);
            this.RbtVehicleLongYes.TabIndex = 69;
            this.RbtVehicleLongYes.Text = "Ja";
            // 
            // LblBriefingPlant
            // 
            this.LblBriefingPlant.BackColor = System.Drawing.SystemColors.Control;
            this.LblBriefingPlant.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblBriefingPlant.Location = new System.Drawing.Point(131, 363);
            this.LblBriefingPlant.Name = "LblBriefingPlant";
            this.LblBriefingPlant.Size = new System.Drawing.Size(208, 16);
            this.LblBriefingPlant.TabIndex = 24;
            this.LblBriefingPlant.Text = "Belehrung durch Betrieb";
            // 
            // PnlVehicleShortCo
            // 
            this.PnlVehicleShortCo.Controls.Add(this.RbtVehicleShortCoYes);
            this.PnlVehicleShortCo.Controls.Add(this.RbtVehicleShortCoNo);
            this.PnlVehicleShortCo.Location = new System.Drawing.Point(8, 411);
            this.PnlVehicleShortCo.Name = "PnlVehicleShortCo";
            this.PnlVehicleShortCo.Size = new System.Drawing.Size(112, 32);
            this.PnlVehicleShortCo.TabIndex = 0;
            // 
            // RbtVehicleShortCoYes
            // 
            this.RbtVehicleShortCoYes.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtVehicleShortCoYes.Location = new System.Drawing.Point(8, 10);
            this.RbtVehicleShortCoYes.Name = "RbtVehicleShortCoYes";
            this.RbtVehicleShortCoYes.Size = new System.Drawing.Size(40, 16);
            this.RbtVehicleShortCoYes.TabIndex = 65;
            this.RbtVehicleShortCoYes.Text = "Ja";
            // 
            // RbtVehicleShortCoNo
            // 
            this.RbtVehicleShortCoNo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtVehicleShortCoNo.Location = new System.Drawing.Point(48, 10);
            this.RbtVehicleShortCoNo.Name = "RbtVehicleShortCoNo";
            this.RbtVehicleShortCoNo.Size = new System.Drawing.Size(48, 16);
            this.RbtVehicleShortCoNo.TabIndex = 0;
            this.RbtVehicleShortCoNo.Text = "Nein";
            // 
            // LblRespiratoryMask
            // 
            this.LblRespiratoryMask.BackColor = System.Drawing.SystemColors.Control;
            this.LblRespiratoryMask.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblRespiratoryMask.Location = new System.Drawing.Point(131, 388);
            this.LblRespiratoryMask.Name = "LblRespiratoryMask";
            this.LblRespiratoryMask.Size = new System.Drawing.Size(208, 16);
            this.LblRespiratoryMask.TabIndex = 22;
            this.LblRespiratoryMask.Text = "Atemschutzmaskenunterweisung";
            // 
            // PnlVehicleShort
            // 
            this.PnlVehicleShort.Controls.Add(this.RbtVehicleShortYes);
            this.PnlVehicleShort.Controls.Add(this.RbtVehicleShortNo);
            this.PnlVehicleShort.Location = new System.Drawing.Point(8, 437);
            this.PnlVehicleShort.Name = "PnlVehicleShort";
            this.PnlVehicleShort.Size = new System.Drawing.Size(112, 32);
            this.PnlVehicleShort.TabIndex = 0;
            // 
            // RbtVehicleShortYes
            // 
            this.RbtVehicleShortYes.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtVehicleShortYes.Location = new System.Drawing.Point(8, 8);
            this.RbtVehicleShortYes.Name = "RbtVehicleShortYes";
            this.RbtVehicleShortYes.Size = new System.Drawing.Size(40, 16);
            this.RbtVehicleShortYes.TabIndex = 66;
            this.RbtVehicleShortYes.Text = "Ja";
            // 
            // RbtVehicleShortNo
            // 
            this.RbtVehicleShortNo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtVehicleShortNo.Location = new System.Drawing.Point(48, 8);
            this.RbtVehicleShortNo.Name = "RbtVehicleShortNo";
            this.RbtVehicleShortNo.Size = new System.Drawing.Size(48, 16);
            this.RbtVehicleShortNo.TabIndex = 0;
            this.RbtVehicleShortNo.Text = "Nein";
            // 
            // LblIndustrialSafetyBriefingOn
            // 
            this.LblIndustrialSafetyBriefingOn.BackColor = System.Drawing.SystemColors.Control;
            this.LblIndustrialSafetyBriefingOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblIndustrialSafetyBriefingOn.Location = new System.Drawing.Point(387, 78);
            this.LblIndustrialSafetyBriefingOn.Name = "LblIndustrialSafetyBriefingOn";
            this.LblIndustrialSafetyBriefingOn.Size = new System.Drawing.Size(32, 16);
            this.LblIndustrialSafetyBriefingOn.TabIndex = 55;
            this.LblIndustrialSafetyBriefingOn.Text = "am";
            // 
            // PnlVehicleLongCo
            // 
            this.PnlVehicleLongCo.Controls.Add(this.RbtVehicleLongCoNo);
            this.PnlVehicleLongCo.Controls.Add(this.RbtVehicleLongCoYes);
            this.PnlVehicleLongCo.Location = new System.Drawing.Point(8, 472);
            this.PnlVehicleLongCo.Name = "PnlVehicleLongCo";
            this.PnlVehicleLongCo.Size = new System.Drawing.Size(96, 16);
            this.PnlVehicleLongCo.TabIndex = 0;
            // 
            // RbtVehicleLongCoNo
            // 
            this.RbtVehicleLongCoNo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtVehicleLongCoNo.Location = new System.Drawing.Point(48, 0);
            this.RbtVehicleLongCoNo.Name = "RbtVehicleLongCoNo";
            this.RbtVehicleLongCoNo.Size = new System.Drawing.Size(48, 16);
            this.RbtVehicleLongCoNo.TabIndex = 0;
            this.RbtVehicleLongCoNo.Text = "Nein";
            // 
            // RbtVehicleLongCoYes
            // 
            this.RbtVehicleLongCoYes.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtVehicleLongCoYes.Location = new System.Drawing.Point(8, 0);
            this.RbtVehicleLongCoYes.Name = "RbtVehicleLongCoYes";
            this.RbtVehicleLongCoYes.Size = new System.Drawing.Size(40, 16);
            this.RbtVehicleLongCoYes.TabIndex = 64;
            this.RbtVehicleLongCoYes.Text = "Ja";
            // 
            // LblSafetyAtWorkServiceBriefingOn
            // 
            this.LblSafetyAtWorkServiceBriefingOn.BackColor = System.Drawing.SystemColors.Control;
            this.LblSafetyAtWorkServiceBriefingOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSafetyAtWorkServiceBriefingOn.Location = new System.Drawing.Point(387, 102);
            this.LblSafetyAtWorkServiceBriefingOn.Name = "LblSafetyAtWorkServiceBriefingOn";
            this.LblSafetyAtWorkServiceBriefingOn.Size = new System.Drawing.Size(32, 16);
            this.LblSafetyAtWorkServiceBriefingOn.TabIndex = 56;
            this.LblSafetyAtWorkServiceBriefingOn.Text = "am";
            // 
            // LblVehicleLong
            // 
            this.LblVehicleLong.BackColor = System.Drawing.SystemColors.Control;
            this.LblVehicleLong.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblVehicleLong.Location = new System.Drawing.Point(131, 495);
            this.LblVehicleLong.Name = "LblVehicleLong";
            this.LblVehicleLong.Size = new System.Drawing.Size(208, 16);
            this.LblVehicleLong.TabIndex = 196;
            this.LblVehicleLong.Text = "Kfz-Einfahrt lang gewährt";
            // 
            // LblRespiratoryMaskOn
            // 
            this.LblRespiratoryMaskOn.BackColor = System.Drawing.SystemColors.Control;
            this.LblRespiratoryMaskOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblRespiratoryMaskOn.Location = new System.Drawing.Point(387, 388);
            this.LblRespiratoryMaskOn.Name = "LblRespiratoryMaskOn";
            this.LblRespiratoryMaskOn.Size = new System.Drawing.Size(32, 16);
            this.LblRespiratoryMaskOn.TabIndex = 57;
            this.LblRespiratoryMaskOn.Text = "am";
            // 
            // LblVehicleLongCo
            // 
            this.LblVehicleLongCo.BackColor = System.Drawing.SystemColors.Control;
            this.LblVehicleLongCo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblVehicleLongCo.Location = new System.Drawing.Point(131, 473);
            this.LblVehicleLongCo.Name = "LblVehicleLongCo";
            this.LblVehicleLongCo.Size = new System.Drawing.Size(208, 16);
            this.LblVehicleLongCo.TabIndex = 197;
            this.LblVehicleLongCo.Text = "Kfz-Einfahrt lang gewünscht";
            // 
            // LblBriefingPlantOn
            // 
            this.LblBriefingPlantOn.BackColor = System.Drawing.SystemColors.Control;
            this.LblBriefingPlantOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblBriefingPlantOn.Location = new System.Drawing.Point(387, 363);
            this.LblBriefingPlantOn.Name = "LblBriefingPlantOn";
            this.LblBriefingPlantOn.Size = new System.Drawing.Size(32, 16);
            this.LblBriefingPlantOn.TabIndex = 58;
            this.LblBriefingPlantOn.Text = "am";
            // 
            // TxtAccessAuthorizationOn
            // 
            this.TxtAccessAuthorizationOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtAccessAuthorizationOn.Location = new System.Drawing.Point(419, 49);
            this.TxtAccessAuthorizationOn.Name = "TxtAccessAuthorizationOn";
            this.TxtAccessAuthorizationOn.Size = new System.Drawing.Size(72, 21);
            this.TxtAccessAuthorizationOn.TabIndex = 25;
            this.TxtAccessAuthorizationOn.Leave += new System.EventHandler(this.TxtAccessAuthorizationOn_Leave);
            // 
            // LblCranesOn
            // 
            this.LblCranesOn.BackColor = System.Drawing.SystemColors.Control;
            this.LblCranesOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCranesOn.Location = new System.Drawing.Point(387, 339);
            this.LblCranesOn.Name = "LblCranesOn";
            this.LblCranesOn.Size = new System.Drawing.Size(32, 16);
            this.LblCranesOn.TabIndex = 59;
            this.LblCranesOn.Text = "am";
            // 
            // TxtSafetyInstructionsOn
            // 
            this.TxtSafetyInstructionsOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSafetyInstructionsOn.Location = new System.Drawing.Point(419, 27);
            this.TxtSafetyInstructionsOn.Name = "TxtSafetyInstructionsOn";
            this.TxtSafetyInstructionsOn.Size = new System.Drawing.Size(72, 21);
            this.TxtSafetyInstructionsOn.TabIndex = 22;
            this.TxtSafetyInstructionsOn.Leave += new System.EventHandler(this.TxtSafetyInstructionsOn_Leave);
            // 
            // LblRaisablePlatformOn
            // 
            this.LblRaisablePlatformOn.BackColor = System.Drawing.SystemColors.Control;
            this.LblRaisablePlatformOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblRaisablePlatformOn.Location = new System.Drawing.Point(387, 309);
            this.LblRaisablePlatformOn.Name = "LblRaisablePlatformOn";
            this.LblRaisablePlatformOn.Size = new System.Drawing.Size(32, 16);
            this.LblRaisablePlatformOn.TabIndex = 60;
            this.LblRaisablePlatformOn.Text = "am";
            // 
            // LblPalletLifterOn
            // 
            this.LblPalletLifterOn.BackColor = System.Drawing.SystemColors.Control;
            this.LblPalletLifterOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblPalletLifterOn.Location = new System.Drawing.Point(387, 284);
            this.LblPalletLifterOn.Name = "LblPalletLifterOn";
            this.LblPalletLifterOn.Size = new System.Drawing.Size(32, 16);
            this.LblPalletLifterOn.TabIndex = 61;
            this.LblPalletLifterOn.Text = "am";
            // 
            // LblAccessAuthorizationBy
            // 
            this.LblAccessAuthorizationBy.BackColor = System.Drawing.SystemColors.Control;
            this.LblAccessAuthorizationBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblAccessAuthorizationBy.Location = new System.Drawing.Point(507, 49);
            this.LblAccessAuthorizationBy.Name = "LblAccessAuthorizationBy";
            this.LblAccessAuthorizationBy.Size = new System.Drawing.Size(40, 16);
            this.LblAccessAuthorizationBy.TabIndex = 190;
            this.LblAccessAuthorizationBy.Text = "durch";
            // 
            // LblBreathingApparatusBriefingOn
            // 
            this.LblBreathingApparatusBriefingOn.BackColor = System.Drawing.SystemColors.Control;
            this.LblBreathingApparatusBriefingOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblBreathingApparatusBriefingOn.Location = new System.Drawing.Point(387, 208);
            this.LblBreathingApparatusBriefingOn.Name = "LblBreathingApparatusBriefingOn";
            this.LblBreathingApparatusBriefingOn.Size = new System.Drawing.Size(32, 16);
            this.LblBreathingApparatusBriefingOn.TabIndex = 62;
            this.LblBreathingApparatusBriefingOn.Text = "am";
            // 
            // TxtAccessAuthorizationBy
            // 
            this.TxtAccessAuthorizationBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtAccessAuthorizationBy.Location = new System.Drawing.Point(555, 49);
            this.TxtAccessAuthorizationBy.Name = "TxtAccessAuthorizationBy";
            this.TxtAccessAuthorizationBy.Size = new System.Drawing.Size(128, 21);
            this.TxtAccessAuthorizationBy.TabIndex = 26;
            // 
            // LblIdHitagOn
            // 
            this.LblIdHitagOn.BackColor = System.Drawing.SystemColors.Control;
            this.LblIdHitagOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblIdHitagOn.Location = new System.Drawing.Point(387, 178);
            this.LblIdHitagOn.Name = "LblIdHitagOn";
            this.LblIdHitagOn.Size = new System.Drawing.Size(32, 16);
            this.LblIdHitagOn.TabIndex = 64;
            this.LblIdHitagOn.Text = "am";
            // 
            // LblAccessAuthorizationOn
            // 
            this.LblAccessAuthorizationOn.BackColor = System.Drawing.SystemColors.Control;
            this.LblAccessAuthorizationOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblAccessAuthorizationOn.Location = new System.Drawing.Point(387, 49);
            this.LblAccessAuthorizationOn.Name = "LblAccessAuthorizationOn";
            this.LblAccessAuthorizationOn.Size = new System.Drawing.Size(32, 16);
            this.LblAccessAuthorizationOn.TabIndex = 189;
            this.LblAccessAuthorizationOn.Text = "am";
            // 
            // LblVehicleLongOn
            // 
            this.LblVehicleLongOn.BackColor = System.Drawing.SystemColors.Control;
            this.LblVehicleLongOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblVehicleLongOn.Location = new System.Drawing.Point(387, 493);
            this.LblVehicleLongOn.Name = "LblVehicleLongOn";
            this.LblVehicleLongOn.Size = new System.Drawing.Size(32, 16);
            this.LblVehicleLongOn.TabIndex = 65;
            this.LblVehicleLongOn.Text = "am";
            // 
            // LblSafetyInstructionsBy
            // 
            this.LblSafetyInstructionsBy.BackColor = System.Drawing.SystemColors.Control;
            this.LblSafetyInstructionsBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSafetyInstructionsBy.Location = new System.Drawing.Point(507, 27);
            this.LblSafetyInstructionsBy.Name = "LblSafetyInstructionsBy";
            this.LblSafetyInstructionsBy.Size = new System.Drawing.Size(40, 16);
            this.LblSafetyInstructionsBy.TabIndex = 188;
            this.LblSafetyInstructionsBy.Text = "durch";
            // 
            // LblVehicleShortOn
            // 
            this.LblVehicleShortOn.BackColor = System.Drawing.SystemColors.Control;
            this.LblVehicleShortOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblVehicleShortOn.Location = new System.Drawing.Point(387, 446);
            this.LblVehicleShortOn.Name = "LblVehicleShortOn";
            this.LblVehicleShortOn.Size = new System.Drawing.Size(32, 16);
            this.LblVehicleShortOn.TabIndex = 66;
            this.LblVehicleShortOn.Text = "am";
            // 
            // LblIndustrialSafetyBriefingBy
            // 
            this.LblIndustrialSafetyBriefingBy.BackColor = System.Drawing.SystemColors.Control;
            this.LblIndustrialSafetyBriefingBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblIndustrialSafetyBriefingBy.Location = new System.Drawing.Point(507, 78);
            this.LblIndustrialSafetyBriefingBy.Name = "LblIndustrialSafetyBriefingBy";
            this.LblIndustrialSafetyBriefingBy.Size = new System.Drawing.Size(40, 16);
            this.LblIndustrialSafetyBriefingBy.TabIndex = 80;
            this.LblIndustrialSafetyBriefingBy.Text = "durch";
            // 
            // TxtSafetyInstructionsBy
            // 
            this.TxtSafetyInstructionsBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSafetyInstructionsBy.Location = new System.Drawing.Point(555, 27);
            this.TxtSafetyInstructionsBy.Name = "TxtSafetyInstructionsBy";
            this.TxtSafetyInstructionsBy.Size = new System.Drawing.Size(128, 21);
            this.TxtSafetyInstructionsBy.TabIndex = 23;
            // 
            // TxtVehicleLongBy
            // 
            this.TxtVehicleLongBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtVehicleLongBy.Location = new System.Drawing.Point(555, 491);
            this.TxtVehicleLongBy.Name = "TxtVehicleLongBy";
            this.TxtVehicleLongBy.Size = new System.Drawing.Size(128, 21);
            this.TxtVehicleLongBy.TabIndex = 71;
            // 
            // TxtVehicleShortBy
            // 
            this.TxtVehicleShortBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtVehicleShortBy.Location = new System.Drawing.Point(555, 444);
            this.TxtVehicleShortBy.Name = "TxtVehicleShortBy";
            this.TxtVehicleShortBy.Size = new System.Drawing.Size(128, 21);
            this.TxtVehicleShortBy.TabIndex = 68;
            // 
            // LblSafetyInstructionsOn
            // 
            this.LblSafetyInstructionsOn.BackColor = System.Drawing.SystemColors.Control;
            this.LblSafetyInstructionsOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSafetyInstructionsOn.Location = new System.Drawing.Point(387, 27);
            this.LblSafetyInstructionsOn.Name = "LblSafetyInstructionsOn";
            this.LblSafetyInstructionsOn.Size = new System.Drawing.Size(32, 16);
            this.LblSafetyInstructionsOn.TabIndex = 186;
            this.LblSafetyInstructionsOn.Text = "am";
            // 
            // TxtRespiratoryMaskBy
            // 
            this.TxtRespiratoryMaskBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtRespiratoryMaskBy.Location = new System.Drawing.Point(555, 386);
            this.TxtRespiratoryMaskBy.Name = "TxtRespiratoryMaskBy";
            this.TxtRespiratoryMaskBy.Size = new System.Drawing.Size(128, 21);
            this.TxtRespiratoryMaskBy.TabIndex = 64;
            // 
            // TxtBriefingPlantBy
            // 
            this.TxtBriefingPlantBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtBriefingPlantBy.Location = new System.Drawing.Point(555, 361);
            this.TxtBriefingPlantBy.Name = "TxtBriefingPlantBy";
            this.TxtBriefingPlantBy.Size = new System.Drawing.Size(128, 21);
            this.TxtBriefingPlantBy.TabIndex = 61;
            // 
            // LblBreathingApparatusBriefingG263
            // 
            this.LblBreathingApparatusBriefingG263.BackColor = System.Drawing.SystemColors.Control;
            this.LblBreathingApparatusBriefingG263.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblBreathingApparatusBriefingG263.Location = new System.Drawing.Point(131, 232);
            this.LblBreathingApparatusBriefingG263.Name = "LblBreathingApparatusBriefingG263";
            this.LblBreathingApparatusBriefingG263.Size = new System.Drawing.Size(200, 16);
            this.LblBreathingApparatusBriefingG263.TabIndex = 176;
            this.LblBreathingApparatusBriefingG263.Text = "Atemschutzgeräteträger Rettung";
            // 
            // TxtCranesBy
            // 
            this.TxtCranesBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCranesBy.Location = new System.Drawing.Point(555, 337);
            this.TxtCranesBy.Name = "TxtCranesBy";
            this.TxtCranesBy.Size = new System.Drawing.Size(128, 21);
            this.TxtCranesBy.TabIndex = 59;
            // 
            // TxtBreathingApparatusBriefingG263On
            // 
            this.TxtBreathingApparatusBriefingG263On.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtBreathingApparatusBriefingG263On.Location = new System.Drawing.Point(419, 232);
            this.TxtBreathingApparatusBriefingG263On.Name = "TxtBreathingApparatusBriefingG263On";
            this.TxtBreathingApparatusBriefingG263On.Size = new System.Drawing.Size(72, 21);
            this.TxtBreathingApparatusBriefingG263On.TabIndex = 46;
            this.TxtBreathingApparatusBriefingG263On.Leave += new System.EventHandler(this.TxtBreathingApparatusBriefingG263On_Leave);
            // 
            // TxtRaisablePlatformBy
            // 
            this.TxtRaisablePlatformBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtRaisablePlatformBy.Location = new System.Drawing.Point(555, 307);
            this.TxtRaisablePlatformBy.Name = "TxtRaisablePlatformBy";
            this.TxtRaisablePlatformBy.Size = new System.Drawing.Size(128, 21);
            this.TxtRaisablePlatformBy.TabIndex = 56;
            // 
            // PnlBreathingApparatusBriefingG263
            // 
            this.PnlBreathingApparatusBriefingG263.Controls.Add(this.RbtBreathingApparatusG263BriefingYes);
            this.PnlBreathingApparatusBriefingG263.Controls.Add(this.RbtBreathingApparatusG263BriefingNo);
            this.PnlBreathingApparatusBriefingG263.Location = new System.Drawing.Point(8, 224);
            this.PnlBreathingApparatusBriefingG263.Name = "PnlBreathingApparatusBriefingG263";
            this.PnlBreathingApparatusBriefingG263.Size = new System.Drawing.Size(112, 24);
            this.PnlBreathingApparatusBriefingG263.TabIndex = 0;
            // 
            // RbtBreathingApparatusG263BriefingYes
            // 
            this.RbtBreathingApparatusG263BriefingYes.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtBreathingApparatusG263BriefingYes.Location = new System.Drawing.Point(8, 8);
            this.RbtBreathingApparatusG263BriefingYes.Name = "RbtBreathingApparatusG263BriefingYes";
            this.RbtBreathingApparatusG263BriefingYes.Size = new System.Drawing.Size(40, 16);
            this.RbtBreathingApparatusG263BriefingYes.TabIndex = 45;
            this.RbtBreathingApparatusG263BriefingYes.Text = "Ja";
            // 
            // RbtBreathingApparatusG263BriefingNo
            // 
            this.RbtBreathingApparatusG263BriefingNo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtBreathingApparatusG263BriefingNo.Location = new System.Drawing.Point(48, 8);
            this.RbtBreathingApparatusG263BriefingNo.Name = "RbtBreathingApparatusG263BriefingNo";
            this.RbtBreathingApparatusG263BriefingNo.Size = new System.Drawing.Size(48, 16);
            this.RbtBreathingApparatusG263BriefingNo.TabIndex = 0;
            this.RbtBreathingApparatusG263BriefingNo.Text = "Nein";
            // 
            // TxtPalletLifterBy
            // 
            this.TxtPalletLifterBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPalletLifterBy.Location = new System.Drawing.Point(555, 284);
            this.TxtPalletLifterBy.Name = "TxtPalletLifterBy";
            this.TxtPalletLifterBy.Size = new System.Drawing.Size(128, 21);
            this.TxtPalletLifterBy.TabIndex = 53;
            // 
            // LblBreathingApparatusBriefingG263By
            // 
            this.LblBreathingApparatusBriefingG263By.BackColor = System.Drawing.SystemColors.Control;
            this.LblBreathingApparatusBriefingG263By.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblBreathingApparatusBriefingG263By.Location = new System.Drawing.Point(507, 232);
            this.LblBreathingApparatusBriefingG263By.Name = "LblBreathingApparatusBriefingG263By";
            this.LblBreathingApparatusBriefingG263By.Size = new System.Drawing.Size(40, 16);
            this.LblBreathingApparatusBriefingG263By.TabIndex = 179;
            this.LblBreathingApparatusBriefingG263By.Text = "durch";
            // 
            // TxtBreathingApparatusG262BriefingBy
            // 
            this.TxtBreathingApparatusG262BriefingBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtBreathingApparatusG262BriefingBy.Location = new System.Drawing.Point(555, 208);
            this.TxtBreathingApparatusG262BriefingBy.Name = "TxtBreathingApparatusG262BriefingBy";
            this.TxtBreathingApparatusG262BriefingBy.Size = new System.Drawing.Size(128, 21);
            this.TxtBreathingApparatusG262BriefingBy.TabIndex = 44;
            // 
            // TxtBreathingApparatusBriefingG263By
            // 
            this.TxtBreathingApparatusBriefingG263By.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtBreathingApparatusBriefingG263By.Location = new System.Drawing.Point(555, 232);
            this.TxtBreathingApparatusBriefingG263By.Name = "TxtBreathingApparatusBriefingG263By";
            this.TxtBreathingApparatusBriefingG263By.Size = new System.Drawing.Size(128, 21);
            this.TxtBreathingApparatusBriefingG263By.TabIndex = 47;
            // 
            // TxtIdHitagBy
            // 
            this.TxtIdHitagBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtIdHitagBy.Location = new System.Drawing.Point(555, 178);
            this.TxtIdHitagBy.Name = "TxtIdHitagBy";
            this.TxtIdHitagBy.Size = new System.Drawing.Size(128, 21);
            this.TxtIdHitagBy.TabIndex = 41;
            // 
            // LblBreathingApparatusBriefingG263On
            // 
            this.LblBreathingApparatusBriefingG263On.BackColor = System.Drawing.SystemColors.Control;
            this.LblBreathingApparatusBriefingG263On.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblBreathingApparatusBriefingG263On.Location = new System.Drawing.Point(387, 232);
            this.LblBreathingApparatusBriefingG263On.Name = "LblBreathingApparatusBriefingG263On";
            this.LblBreathingApparatusBriefingG263On.Size = new System.Drawing.Size(32, 16);
            this.LblBreathingApparatusBriefingG263On.TabIndex = 178;
            this.LblBreathingApparatusBriefingG263On.Text = "am";
            // 
            // TxtSafetyAtWorkServiceBriefingBy
            // 
            this.TxtSafetyAtWorkServiceBriefingBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSafetyAtWorkServiceBriefingBy.Location = new System.Drawing.Point(555, 102);
            this.TxtSafetyAtWorkServiceBriefingBy.Name = "TxtSafetyAtWorkServiceBriefingBy";
            this.TxtSafetyAtWorkServiceBriefingBy.Size = new System.Drawing.Size(128, 21);
            this.TxtSafetyAtWorkServiceBriefingBy.TabIndex = 32;
            // 
            // PnlCtlRbtSafetyAtWork
            // 
            this.PnlCtlRbtSafetyAtWork.Controls.Add(this.RbtIndustrialSafetyBriefingYes);
            this.PnlCtlRbtSafetyAtWork.Controls.Add(this.RbtIndustrialSafetyBriefingNo);
            this.PnlCtlRbtSafetyAtWork.Location = new System.Drawing.Point(8, 78);
            this.PnlCtlRbtSafetyAtWork.Name = "PnlCtlRbtSafetyAtWork";
            this.PnlCtlRbtSafetyAtWork.Size = new System.Drawing.Size(96, 16);
            this.PnlCtlRbtSafetyAtWork.TabIndex = 0;
            // 
            // RbtIndustrialSafetyBriefingYes
            // 
            this.RbtIndustrialSafetyBriefingYes.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtIndustrialSafetyBriefingYes.Location = new System.Drawing.Point(8, 0);
            this.RbtIndustrialSafetyBriefingYes.Name = "RbtIndustrialSafetyBriefingYes";
            this.RbtIndustrialSafetyBriefingYes.Size = new System.Drawing.Size(40, 16);
            this.RbtIndustrialSafetyBriefingYes.TabIndex = 27;
            this.RbtIndustrialSafetyBriefingYes.Text = "Ja";
            // 
            // RbtIndustrialSafetyBriefingNo
            // 
            this.RbtIndustrialSafetyBriefingNo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtIndustrialSafetyBriefingNo.Location = new System.Drawing.Point(48, 0);
            this.RbtIndustrialSafetyBriefingNo.Name = "RbtIndustrialSafetyBriefingNo";
            this.RbtIndustrialSafetyBriefingNo.Size = new System.Drawing.Size(48, 16);
            this.RbtIndustrialSafetyBriefingNo.TabIndex = 0;
            this.RbtIndustrialSafetyBriefingNo.Text = "Nein";
            // 
            // TxtIndustrialSafetyBriefingBy
            // 
            this.TxtIndustrialSafetyBriefingBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtIndustrialSafetyBriefingBy.Location = new System.Drawing.Point(555, 78);
            this.TxtIndustrialSafetyBriefingBy.Name = "TxtIndustrialSafetyBriefingBy";
            this.TxtIndustrialSafetyBriefingBy.Size = new System.Drawing.Size(128, 21);
            this.TxtIndustrialSafetyBriefingBy.TabIndex = 29;
            // 
            // PnlAccessAuthorization
            // 
            this.PnlAccessAuthorization.Controls.Add(this.RbtAccessAuthorizationYes);
            this.PnlAccessAuthorization.Controls.Add(this.RbtAccessAuthorizationNo);
            this.PnlAccessAuthorization.Location = new System.Drawing.Point(16, 41);
            this.PnlAccessAuthorization.Name = "PnlAccessAuthorization";
            this.PnlAccessAuthorization.Size = new System.Drawing.Size(88, 32);
            this.PnlAccessAuthorization.TabIndex = 0;
            // 
            // RbtAccessAuthorizationYes
            // 
            this.RbtAccessAuthorizationYes.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtAccessAuthorizationYes.Location = new System.Drawing.Point(0, 8);
            this.RbtAccessAuthorizationYes.Name = "RbtAccessAuthorizationYes";
            this.RbtAccessAuthorizationYes.Size = new System.Drawing.Size(40, 16);
            this.RbtAccessAuthorizationYes.TabIndex = 24;
            this.RbtAccessAuthorizationYes.Text = "Ja";
            // 
            // RbtAccessAuthorizationNo
            // 
            this.RbtAccessAuthorizationNo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtAccessAuthorizationNo.Location = new System.Drawing.Point(40, 8);
            this.RbtAccessAuthorizationNo.Name = "RbtAccessAuthorizationNo";
            this.RbtAccessAuthorizationNo.Size = new System.Drawing.Size(48, 16);
            this.RbtAccessAuthorizationNo.TabIndex = 0;
            this.RbtAccessAuthorizationNo.Text = "Nein";
            // 
            // LblVehicleLongBy
            // 
            this.LblVehicleLongBy.BackColor = System.Drawing.SystemColors.Control;
            this.LblVehicleLongBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblVehicleLongBy.Location = new System.Drawing.Point(507, 493);
            this.LblVehicleLongBy.Name = "LblVehicleLongBy";
            this.LblVehicleLongBy.Size = new System.Drawing.Size(40, 16);
            this.LblVehicleLongBy.TabIndex = 106;
            this.LblVehicleLongBy.Text = "durch";
            // 
            // LblAccessAuthorization
            // 
            this.LblAccessAuthorization.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblAccessAuthorization.Location = new System.Drawing.Point(131, 49);
            this.LblAccessAuthorization.Name = "LblAccessAuthorization";
            this.LblAccessAuthorization.Size = new System.Drawing.Size(216, 16);
            this.LblAccessAuthorization.TabIndex = 174;
            this.LblAccessAuthorization.Text = "Zutrittsberechtigt";
            // 
            // LblVehicleShortBy
            // 
            this.LblVehicleShortBy.BackColor = System.Drawing.SystemColors.Control;
            this.LblVehicleShortBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblVehicleShortBy.Location = new System.Drawing.Point(507, 446);
            this.LblVehicleShortBy.Name = "LblVehicleShortBy";
            this.LblVehicleShortBy.Size = new System.Drawing.Size(40, 16);
            this.LblVehicleShortBy.TabIndex = 107;
            this.LblVehicleShortBy.Text = "durch";
            // 
            // LblPalletLifter
            // 
            this.LblPalletLifter.BackColor = System.Drawing.SystemColors.Control;
            this.LblPalletLifter.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblPalletLifter.Location = new System.Drawing.Point(131, 284);
            this.LblPalletLifter.Name = "LblPalletLifter";
            this.LblPalletLifter.Size = new System.Drawing.Size(144, 16);
            this.LblPalletLifter.TabIndex = 10;
            this.LblPalletLifter.Text = "Flurförderzeuge";
            // 
            // LblRespiratoryMaskBy
            // 
            this.LblRespiratoryMaskBy.BackColor = System.Drawing.SystemColors.Control;
            this.LblRespiratoryMaskBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblRespiratoryMaskBy.Location = new System.Drawing.Point(507, 388);
            this.LblRespiratoryMaskBy.Name = "LblRespiratoryMaskBy";
            this.LblRespiratoryMaskBy.Size = new System.Drawing.Size(40, 16);
            this.LblRespiratoryMaskBy.TabIndex = 108;
            this.LblRespiratoryMaskBy.Text = "durch";
            // 
            // LblBreathingApparatusBriefingG262
            // 
            this.LblBreathingApparatusBriefingG262.BackColor = System.Drawing.SystemColors.Control;
            this.LblBreathingApparatusBriefingG262.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblBreathingApparatusBriefingG262.Location = new System.Drawing.Point(131, 208);
            this.LblBreathingApparatusBriefingG262.Name = "LblBreathingApparatusBriefingG262";
            this.LblBreathingApparatusBriefingG262.Size = new System.Drawing.Size(200, 16);
            this.LblBreathingApparatusBriefingG262.TabIndex = 9;
            this.LblBreathingApparatusBriefingG262.Text = "Atemschutzgeräteträger ";
            // 
            // LblBriefingPlantBy
            // 
            this.LblBriefingPlantBy.BackColor = System.Drawing.SystemColors.Control;
            this.LblBriefingPlantBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblBriefingPlantBy.Location = new System.Drawing.Point(507, 363);
            this.LblBriefingPlantBy.Name = "LblBriefingPlantBy";
            this.LblBriefingPlantBy.Size = new System.Drawing.Size(40, 16);
            this.LblBriefingPlantBy.TabIndex = 109;
            this.LblBriefingPlantBy.Text = "durch";
            // 
            // LblCranes
            // 
            this.LblCranes.BackColor = System.Drawing.SystemColors.Control;
            this.LblCranes.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCranes.Location = new System.Drawing.Point(131, 339);
            this.LblCranes.Name = "LblCranes";
            this.LblCranes.Size = new System.Drawing.Size(144, 16);
            this.LblCranes.TabIndex = 16;
            this.LblCranes.Text = "Krane/ Anschläger";
            // 
            // LblCranesBy
            // 
            this.LblCranesBy.BackColor = System.Drawing.SystemColors.Control;
            this.LblCranesBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCranesBy.Location = new System.Drawing.Point(507, 339);
            this.LblCranesBy.Name = "LblCranesBy";
            this.LblCranesBy.Size = new System.Drawing.Size(40, 16);
            this.LblCranesBy.TabIndex = 110;
            this.LblCranesBy.Text = "durch";
            // 
            // LblRaisablePlatform
            // 
            this.LblRaisablePlatform.BackColor = System.Drawing.SystemColors.Control;
            this.LblRaisablePlatform.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblRaisablePlatform.Location = new System.Drawing.Point(131, 309);
            this.LblRaisablePlatform.Name = "LblRaisablePlatform";
            this.LblRaisablePlatform.Size = new System.Drawing.Size(144, 16);
            this.LblRaisablePlatform.TabIndex = 14;
            this.LblRaisablePlatform.Text = "Hubarbeitsbühne";
            // 
            // LblRaisablePlatformBy
            // 
            this.LblRaisablePlatformBy.BackColor = System.Drawing.SystemColors.Control;
            this.LblRaisablePlatformBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblRaisablePlatformBy.Location = new System.Drawing.Point(507, 309);
            this.LblRaisablePlatformBy.Name = "LblRaisablePlatformBy";
            this.LblRaisablePlatformBy.Size = new System.Drawing.Size(40, 16);
            this.LblRaisablePlatformBy.TabIndex = 111;
            this.LblRaisablePlatformBy.Text = "durch";
            // 
            // TxtBriefingPlantOn
            // 
            this.TxtBriefingPlantOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtBriefingPlantOn.Location = new System.Drawing.Point(419, 361);
            this.TxtBriefingPlantOn.Name = "TxtBriefingPlantOn";
            this.TxtBriefingPlantOn.Size = new System.Drawing.Size(72, 21);
            this.TxtBriefingPlantOn.TabIndex = 60;
            this.TxtBriefingPlantOn.Leave += new System.EventHandler(this.TxtBriefingPlantOn_Leave);
            // 
            // LblPalletLifterBy
            // 
            this.LblPalletLifterBy.BackColor = System.Drawing.SystemColors.Control;
            this.LblPalletLifterBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblPalletLifterBy.Location = new System.Drawing.Point(507, 284);
            this.LblPalletLifterBy.Name = "LblPalletLifterBy";
            this.LblPalletLifterBy.Size = new System.Drawing.Size(40, 16);
            this.LblPalletLifterBy.TabIndex = 112;
            this.LblPalletLifterBy.Text = "durch";
            // 
            // TxtCranesOn
            // 
            this.TxtCranesOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCranesOn.Location = new System.Drawing.Point(419, 337);
            this.TxtCranesOn.Name = "TxtCranesOn";
            this.TxtCranesOn.Size = new System.Drawing.Size(72, 21);
            this.TxtCranesOn.TabIndex = 58;
            this.TxtCranesOn.Leave += new System.EventHandler(this.TxtCranesOn_Leave);
            // 
            // LblBreathingApparatusG262BriefingBy
            // 
            this.LblBreathingApparatusG262BriefingBy.BackColor = System.Drawing.SystemColors.Control;
            this.LblBreathingApparatusG262BriefingBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblBreathingApparatusG262BriefingBy.Location = new System.Drawing.Point(507, 208);
            this.LblBreathingApparatusG262BriefingBy.Name = "LblBreathingApparatusG262BriefingBy";
            this.LblBreathingApparatusG262BriefingBy.Size = new System.Drawing.Size(40, 16);
            this.LblBreathingApparatusG262BriefingBy.TabIndex = 113;
            this.LblBreathingApparatusG262BriefingBy.Text = "durch";
            // 
            // TxtVehicleLongOn
            // 
            this.TxtVehicleLongOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtVehicleLongOn.Location = new System.Drawing.Point(419, 491);
            this.TxtVehicleLongOn.Name = "TxtVehicleLongOn";
            this.TxtVehicleLongOn.Size = new System.Drawing.Size(72, 21);
            this.TxtVehicleLongOn.TabIndex = 70;
            this.TxtVehicleLongOn.Leave += new System.EventHandler(this.TxtVehicleLongOn_Leave);
            // 
            // LblIdHitagBy
            // 
            this.LblIdHitagBy.BackColor = System.Drawing.SystemColors.Control;
            this.LblIdHitagBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblIdHitagBy.Location = new System.Drawing.Point(507, 178);
            this.LblIdHitagBy.Name = "LblIdHitagBy";
            this.LblIdHitagBy.Size = new System.Drawing.Size(40, 16);
            this.LblIdHitagBy.TabIndex = 115;
            this.LblIdHitagBy.Text = "durch";
            // 
            // TxtVehicleShortOn
            // 
            this.TxtVehicleShortOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtVehicleShortOn.Location = new System.Drawing.Point(419, 444);
            this.TxtVehicleShortOn.Name = "TxtVehicleShortOn";
            this.TxtVehicleShortOn.Size = new System.Drawing.Size(72, 21);
            this.TxtVehicleShortOn.TabIndex = 67;
            this.TxtVehicleShortOn.Leave += new System.EventHandler(this.TxtVehicleShortOn_Leave);
            // 
            // LblSafetyAtWorkServiceBriefingBy
            // 
            this.LblSafetyAtWorkServiceBriefingBy.BackColor = System.Drawing.SystemColors.Control;
            this.LblSafetyAtWorkServiceBriefingBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSafetyAtWorkServiceBriefingBy.Location = new System.Drawing.Point(507, 102);
            this.LblSafetyAtWorkServiceBriefingBy.Name = "LblSafetyAtWorkServiceBriefingBy";
            this.LblSafetyAtWorkServiceBriefingBy.Size = new System.Drawing.Size(40, 16);
            this.LblSafetyAtWorkServiceBriefingBy.TabIndex = 116;
            this.LblSafetyAtWorkServiceBriefingBy.Text = "durch";
            // 
            // TxtRespiratoryMaskOn
            // 
            this.TxtRespiratoryMaskOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtRespiratoryMaskOn.Location = new System.Drawing.Point(419, 386);
            this.TxtRespiratoryMaskOn.Name = "TxtRespiratoryMaskOn";
            this.TxtRespiratoryMaskOn.Size = new System.Drawing.Size(72, 21);
            this.TxtRespiratoryMaskOn.TabIndex = 63;
            this.TxtRespiratoryMaskOn.Leave += new System.EventHandler(this.TxtRespiratoryMaskOn_Leave);
            // 
            // LblSiteSecurityBriefingOn
            // 
            this.LblSiteSecurityBriefingOn.BackColor = System.Drawing.SystemColors.Control;
            this.LblSiteSecurityBriefingOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiteSecurityBriefingOn.Location = new System.Drawing.Point(387, 126);
            this.LblSiteSecurityBriefingOn.Name = "LblSiteSecurityBriefingOn";
            this.LblSiteSecurityBriefingOn.Size = new System.Drawing.Size(32, 16);
            this.LblSiteSecurityBriefingOn.TabIndex = 117;
            this.LblSiteSecurityBriefingOn.Text = "am";
            // 
            // TxtRaisablePlatformOn
            // 
            this.TxtRaisablePlatformOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtRaisablePlatformOn.Location = new System.Drawing.Point(419, 307);
            this.TxtRaisablePlatformOn.Name = "TxtRaisablePlatformOn";
            this.TxtRaisablePlatformOn.Size = new System.Drawing.Size(72, 21);
            this.TxtRaisablePlatformOn.TabIndex = 55;
            this.TxtRaisablePlatformOn.Leave += new System.EventHandler(this.TxtRaisablePlatformOn_Leave);
            // 
            // TxtSiteSecurityBriefingBy
            // 
            this.TxtSiteSecurityBriefingBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSiteSecurityBriefingBy.Location = new System.Drawing.Point(555, 126);
            this.TxtSiteSecurityBriefingBy.Name = "TxtSiteSecurityBriefingBy";
            this.TxtSiteSecurityBriefingBy.Size = new System.Drawing.Size(128, 21);
            this.TxtSiteSecurityBriefingBy.TabIndex = 35;
            // 
            // TxtSiteSecurityBriefingOn
            // 
            this.TxtSiteSecurityBriefingOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSiteSecurityBriefingOn.Location = new System.Drawing.Point(419, 126);
            this.TxtSiteSecurityBriefingOn.Name = "TxtSiteSecurityBriefingOn";
            this.TxtSiteSecurityBriefingOn.Size = new System.Drawing.Size(72, 21);
            this.TxtSiteSecurityBriefingOn.TabIndex = 34;
            this.TxtSiteSecurityBriefingOn.Leave += new System.EventHandler(this.TxtSiteSecurityBriefingOn_Leave);
            // 
            // LblSiteSecurityBriefingBy
            // 
            this.LblSiteSecurityBriefingBy.BackColor = System.Drawing.SystemColors.Control;
            this.LblSiteSecurityBriefingBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiteSecurityBriefingBy.Location = new System.Drawing.Point(507, 126);
            this.LblSiteSecurityBriefingBy.Name = "LblSiteSecurityBriefingBy";
            this.LblSiteSecurityBriefingBy.Size = new System.Drawing.Size(40, 16);
            this.LblSiteSecurityBriefingBy.TabIndex = 120;
            this.LblSiteSecurityBriefingBy.Text = "durch";
            // 
            // TxtIdHitagOn
            // 
            this.TxtIdHitagOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtIdHitagOn.Location = new System.Drawing.Point(419, 178);
            this.TxtIdHitagOn.Name = "TxtIdHitagOn";
            this.TxtIdHitagOn.Size = new System.Drawing.Size(72, 21);
            this.TxtIdHitagOn.TabIndex = 40;
            this.TxtIdHitagOn.Leave += new System.EventHandler(this.TxtIdHitagOn_Leave);
            // 
            // PnlCtlRbtBreathingApparatus
            // 
            this.PnlCtlRbtBreathingApparatus.Controls.Add(this.RbtBreathingApparatusG262BriefingYes);
            this.PnlCtlRbtBreathingApparatus.Controls.Add(this.RbtBreathingApparatusG262BriefingNo);
            this.PnlCtlRbtBreathingApparatus.Location = new System.Drawing.Point(8, 200);
            this.PnlCtlRbtBreathingApparatus.Name = "PnlCtlRbtBreathingApparatus";
            this.PnlCtlRbtBreathingApparatus.Size = new System.Drawing.Size(112, 24);
            this.PnlCtlRbtBreathingApparatus.TabIndex = 0;
            // 
            // RbtBreathingApparatusG262BriefingYes
            // 
            this.RbtBreathingApparatusG262BriefingYes.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtBreathingApparatusG262BriefingYes.Location = new System.Drawing.Point(8, 8);
            this.RbtBreathingApparatusG262BriefingYes.Name = "RbtBreathingApparatusG262BriefingYes";
            this.RbtBreathingApparatusG262BriefingYes.Size = new System.Drawing.Size(40, 16);
            this.RbtBreathingApparatusG262BriefingYes.TabIndex = 42;
            this.RbtBreathingApparatusG262BriefingYes.Text = "Ja";
            // 
            // RbtBreathingApparatusG262BriefingNo
            // 
            this.RbtBreathingApparatusG262BriefingNo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtBreathingApparatusG262BriefingNo.Location = new System.Drawing.Point(48, 8);
            this.RbtBreathingApparatusG262BriefingNo.Name = "RbtBreathingApparatusG262BriefingNo";
            this.RbtBreathingApparatusG262BriefingNo.Size = new System.Drawing.Size(48, 16);
            this.RbtBreathingApparatusG262BriefingNo.TabIndex = 0;
            this.RbtBreathingApparatusG262BriefingNo.Text = "Nein";
            // 
            // TxtBreathingApparatusBriefingg262On
            // 
            this.TxtBreathingApparatusBriefingg262On.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtBreathingApparatusBriefingg262On.Location = new System.Drawing.Point(419, 208);
            this.TxtBreathingApparatusBriefingg262On.Name = "TxtBreathingApparatusBriefingg262On";
            this.TxtBreathingApparatusBriefingg262On.Size = new System.Drawing.Size(72, 21);
            this.TxtBreathingApparatusBriefingg262On.TabIndex = 43;
            this.TxtBreathingApparatusBriefingg262On.Leave += new System.EventHandler(this.TxtBreathingApparatusBriefingg262On_Leave);
            // 
            // PnlCtlRbtIdPhotoHitag
            // 
            this.PnlCtlRbtIdPhotoHitag.Controls.Add(this.RbtIdHitagYes);
            this.PnlCtlRbtIdPhotoHitag.Controls.Add(this.RbtIdHitagNo);
            this.PnlCtlRbtIdPhotoHitag.Location = new System.Drawing.Point(8, 168);
            this.PnlCtlRbtIdPhotoHitag.Name = "PnlCtlRbtIdPhotoHitag";
            this.PnlCtlRbtIdPhotoHitag.Size = new System.Drawing.Size(96, 32);
            this.PnlCtlRbtIdPhotoHitag.TabIndex = 0;
            // 
            // RbtIdHitagYes
            // 
            this.RbtIdHitagYes.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtIdHitagYes.Location = new System.Drawing.Point(8, 8);
            this.RbtIdHitagYes.Name = "RbtIdHitagYes";
            this.RbtIdHitagYes.Size = new System.Drawing.Size(40, 16);
            this.RbtIdHitagYes.TabIndex = 39;
            this.RbtIdHitagYes.Text = "Ja";
            // 
            // RbtIdHitagNo
            // 
            this.RbtIdHitagNo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtIdHitagNo.Location = new System.Drawing.Point(48, 8);
            this.RbtIdHitagNo.Name = "RbtIdHitagNo";
            this.RbtIdHitagNo.Size = new System.Drawing.Size(48, 16);
            this.RbtIdHitagNo.TabIndex = 0;
            this.RbtIdHitagNo.Text = "Nein";
            // 
            // TxtPalletLifterOn
            // 
            this.TxtPalletLifterOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPalletLifterOn.Location = new System.Drawing.Point(419, 284);
            this.TxtPalletLifterOn.Name = "TxtPalletLifterOn";
            this.TxtPalletLifterOn.Size = new System.Drawing.Size(72, 21);
            this.TxtPalletLifterOn.TabIndex = 52;
            this.TxtPalletLifterOn.Leave += new System.EventHandler(this.TxtPalletLifterOn_Leave);
            // 
            // PnlCtlRbtBriefingSiteSecurity
            // 
            this.PnlCtlRbtBriefingSiteSecurity.Controls.Add(this.RbtSiteSecurityBriefingNo);
            this.PnlCtlRbtBriefingSiteSecurity.Controls.Add(this.RbtSiteSecurityBriefingYes);
            this.PnlCtlRbtBriefingSiteSecurity.Location = new System.Drawing.Point(8, 126);
            this.PnlCtlRbtBriefingSiteSecurity.Name = "PnlCtlRbtBriefingSiteSecurity";
            this.PnlCtlRbtBriefingSiteSecurity.Size = new System.Drawing.Size(96, 16);
            this.PnlCtlRbtBriefingSiteSecurity.TabIndex = 0;
            // 
            // RbtSiteSecurityBriefingNo
            // 
            this.RbtSiteSecurityBriefingNo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtSiteSecurityBriefingNo.Location = new System.Drawing.Point(48, 0);
            this.RbtSiteSecurityBriefingNo.Name = "RbtSiteSecurityBriefingNo";
            this.RbtSiteSecurityBriefingNo.Size = new System.Drawing.Size(48, 16);
            this.RbtSiteSecurityBriefingNo.TabIndex = 0;
            this.RbtSiteSecurityBriefingNo.Text = "Nein";
            // 
            // RbtSiteSecurityBriefingYes
            // 
            this.RbtSiteSecurityBriefingYes.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtSiteSecurityBriefingYes.Location = new System.Drawing.Point(8, 0);
            this.RbtSiteSecurityBriefingYes.Name = "RbtSiteSecurityBriefingYes";
            this.RbtSiteSecurityBriefingYes.Size = new System.Drawing.Size(40, 16);
            this.RbtSiteSecurityBriefingYes.TabIndex = 33;
            this.RbtSiteSecurityBriefingYes.Text = "Ja";
            // 
            // TxtSafetyAtWorkServiceBriefingOn
            // 
            this.TxtSafetyAtWorkServiceBriefingOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSafetyAtWorkServiceBriefingOn.Location = new System.Drawing.Point(419, 102);
            this.TxtSafetyAtWorkServiceBriefingOn.Name = "TxtSafetyAtWorkServiceBriefingOn";
            this.TxtSafetyAtWorkServiceBriefingOn.Size = new System.Drawing.Size(72, 21);
            this.TxtSafetyAtWorkServiceBriefingOn.TabIndex = 31;
            this.TxtSafetyAtWorkServiceBriefingOn.Leave += new System.EventHandler(this.TxtSafetyAtWorkServiceBriefingOn_Leave);
            // 
            // PnlCtlRbtBriefingSafetyAtWork
            // 
            this.PnlCtlRbtBriefingSafetyAtWork.Controls.Add(this.RbtSafetyAtWorkServiceBriefingYes);
            this.PnlCtlRbtBriefingSafetyAtWork.Controls.Add(this.RbtSafetyAtWorkServiceBriefingNo);
            this.PnlCtlRbtBriefingSafetyAtWork.Location = new System.Drawing.Point(8, 102);
            this.PnlCtlRbtBriefingSafetyAtWork.Name = "PnlCtlRbtBriefingSafetyAtWork";
            this.PnlCtlRbtBriefingSafetyAtWork.Size = new System.Drawing.Size(96, 16);
            this.PnlCtlRbtBriefingSafetyAtWork.TabIndex = 0;
            // 
            // RbtSafetyAtWorkServiceBriefingYes
            // 
            this.RbtSafetyAtWorkServiceBriefingYes.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtSafetyAtWorkServiceBriefingYes.Location = new System.Drawing.Point(8, 0);
            this.RbtSafetyAtWorkServiceBriefingYes.Name = "RbtSafetyAtWorkServiceBriefingYes";
            this.RbtSafetyAtWorkServiceBriefingYes.Size = new System.Drawing.Size(40, 16);
            this.RbtSafetyAtWorkServiceBriefingYes.TabIndex = 30;
            this.RbtSafetyAtWorkServiceBriefingYes.Text = "Ja";
            // 
            // RbtSafetyAtWorkServiceBriefingNo
            // 
            this.RbtSafetyAtWorkServiceBriefingNo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtSafetyAtWorkServiceBriefingNo.Location = new System.Drawing.Point(48, 0);
            this.RbtSafetyAtWorkServiceBriefingNo.Name = "RbtSafetyAtWorkServiceBriefingNo";
            this.RbtSafetyAtWorkServiceBriefingNo.Size = new System.Drawing.Size(48, 16);
            this.RbtSafetyAtWorkServiceBriefingNo.TabIndex = 0;
            this.RbtSafetyAtWorkServiceBriefingNo.Text = "Nein";
            // 
            // TxtIndustrialSafetyBriefingOn
            // 
            this.TxtIndustrialSafetyBriefingOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtIndustrialSafetyBriefingOn.Location = new System.Drawing.Point(419, 78);
            this.TxtIndustrialSafetyBriefingOn.Name = "TxtIndustrialSafetyBriefingOn";
            this.TxtIndustrialSafetyBriefingOn.Size = new System.Drawing.Size(72, 21);
            this.TxtIndustrialSafetyBriefingOn.TabIndex = 28;
            this.TxtIndustrialSafetyBriefingOn.Leave += new System.EventHandler(this.TxtIndustrialSafetyBriefingOn_Leave);
            // 
            // PnlCtlRbtBriefingPlant
            // 
            this.PnlCtlRbtBriefingPlant.Controls.Add(this.RbtBriefingPlantNo);
            this.PnlCtlRbtBriefingPlant.Controls.Add(this.RbtBriefingPlantYes);
            this.PnlCtlRbtBriefingPlant.Location = new System.Drawing.Point(8, 362);
            this.PnlCtlRbtBriefingPlant.Name = "PnlCtlRbtBriefingPlant";
            this.PnlCtlRbtBriefingPlant.Size = new System.Drawing.Size(96, 16);
            this.PnlCtlRbtBriefingPlant.TabIndex = 0;
            // 
            // RbtBriefingPlantNo
            // 
            this.RbtBriefingPlantNo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtBriefingPlantNo.Location = new System.Drawing.Point(48, 0);
            this.RbtBriefingPlantNo.Name = "RbtBriefingPlantNo";
            this.RbtBriefingPlantNo.Size = new System.Drawing.Size(48, 16);
            this.RbtBriefingPlantNo.TabIndex = 0;
            this.RbtBriefingPlantNo.Text = "Nein";
            // 
            // RbtBriefingPlantYes
            // 
            this.RbtBriefingPlantYes.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtBriefingPlantYes.Location = new System.Drawing.Point(8, 0);
            this.RbtBriefingPlantYes.Name = "RbtBriefingPlantYes";
            this.RbtBriefingPlantYes.Size = new System.Drawing.Size(40, 16);
            this.RbtBriefingPlantYes.TabIndex = 60;
            this.RbtBriefingPlantYes.Text = "Ja";
            // 
            // PnlCtlRbtRespiratoryMask
            // 
            this.PnlCtlRbtRespiratoryMask.Controls.Add(this.RbtRespiratoryMaskNo);
            this.PnlCtlRbtRespiratoryMask.Controls.Add(this.RbtRespiratoryMaskYes);
            this.PnlCtlRbtRespiratoryMask.Location = new System.Drawing.Point(8, 381);
            this.PnlCtlRbtRespiratoryMask.Name = "PnlCtlRbtRespiratoryMask";
            this.PnlCtlRbtRespiratoryMask.Size = new System.Drawing.Size(96, 26);
            this.PnlCtlRbtRespiratoryMask.TabIndex = 0;
            // 
            // RbtRespiratoryMaskNo
            // 
            this.RbtRespiratoryMaskNo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtRespiratoryMaskNo.Location = new System.Drawing.Point(48, 6);
            this.RbtRespiratoryMaskNo.Name = "RbtRespiratoryMaskNo";
            this.RbtRespiratoryMaskNo.Size = new System.Drawing.Size(48, 16);
            this.RbtRespiratoryMaskNo.TabIndex = 0;
            this.RbtRespiratoryMaskNo.Text = "Nein";
            // 
            // RbtRespiratoryMaskYes
            // 
            this.RbtRespiratoryMaskYes.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtRespiratoryMaskYes.Location = new System.Drawing.Point(8, 6);
            this.RbtRespiratoryMaskYes.Name = "RbtRespiratoryMaskYes";
            this.RbtRespiratoryMaskYes.Size = new System.Drawing.Size(40, 16);
            this.RbtRespiratoryMaskYes.TabIndex = 62;
            this.RbtRespiratoryMaskYes.Text = "Ja";
            // 
            // PnlCtlRbtCranes
            // 
            this.PnlCtlRbtCranes.Controls.Add(this.RbtCranesYes);
            this.PnlCtlRbtCranes.Controls.Add(this.RbtCranesNo);
            this.PnlCtlRbtCranes.Location = new System.Drawing.Point(8, 330);
            this.PnlCtlRbtCranes.Name = "PnlCtlRbtCranes";
            this.PnlCtlRbtCranes.Size = new System.Drawing.Size(112, 32);
            this.PnlCtlRbtCranes.TabIndex = 0;
            // 
            // RbtCranesYes
            // 
            this.RbtCranesYes.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtCranesYes.Location = new System.Drawing.Point(8, 8);
            this.RbtCranesYes.Name = "RbtCranesYes";
            this.RbtCranesYes.Size = new System.Drawing.Size(40, 16);
            this.RbtCranesYes.TabIndex = 57;
            this.RbtCranesYes.Text = "Ja";
            // 
            // RbtCranesNo
            // 
            this.RbtCranesNo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtCranesNo.Location = new System.Drawing.Point(48, 8);
            this.RbtCranesNo.Name = "RbtCranesNo";
            this.RbtCranesNo.Size = new System.Drawing.Size(48, 16);
            this.RbtCranesNo.TabIndex = 0;
            this.RbtCranesNo.Text = "Nein";
            // 
            // PnlCtlRbtPalletLifter
            // 
            this.PnlCtlRbtPalletLifter.Controls.Add(this.RbtPalletLifterYes);
            this.PnlCtlRbtPalletLifter.Controls.Add(this.RbtPalletLifterNo);
            this.PnlCtlRbtPalletLifter.Location = new System.Drawing.Point(8, 276);
            this.PnlCtlRbtPalletLifter.Name = "PnlCtlRbtPalletLifter";
            this.PnlCtlRbtPalletLifter.Size = new System.Drawing.Size(112, 32);
            this.PnlCtlRbtPalletLifter.TabIndex = 0;
            // 
            // RbtPalletLifterYes
            // 
            this.RbtPalletLifterYes.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtPalletLifterYes.Location = new System.Drawing.Point(8, 8);
            this.RbtPalletLifterYes.Name = "RbtPalletLifterYes";
            this.RbtPalletLifterYes.Size = new System.Drawing.Size(40, 16);
            this.RbtPalletLifterYes.TabIndex = 51;
            this.RbtPalletLifterYes.Text = "Ja";
            // 
            // RbtPalletLifterNo
            // 
            this.RbtPalletLifterNo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtPalletLifterNo.Location = new System.Drawing.Point(48, 8);
            this.RbtPalletLifterNo.Name = "RbtPalletLifterNo";
            this.RbtPalletLifterNo.Size = new System.Drawing.Size(48, 16);
            this.RbtPalletLifterNo.TabIndex = 0;
            this.RbtPalletLifterNo.Text = "Nein";
            // 
            // PnlCtlRbtRaisablePlatform
            // 
            this.PnlCtlRbtRaisablePlatform.Controls.Add(this.RbtRaisablePlatformYes);
            this.PnlCtlRbtRaisablePlatform.Controls.Add(this.RbtRaisablePlatformNo);
            this.PnlCtlRbtRaisablePlatform.Location = new System.Drawing.Point(8, 300);
            this.PnlCtlRbtRaisablePlatform.Name = "PnlCtlRbtRaisablePlatform";
            this.PnlCtlRbtRaisablePlatform.Size = new System.Drawing.Size(112, 32);
            this.PnlCtlRbtRaisablePlatform.TabIndex = 0;
            // 
            // RbtRaisablePlatformYes
            // 
            this.RbtRaisablePlatformYes.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtRaisablePlatformYes.Location = new System.Drawing.Point(8, 8);
            this.RbtRaisablePlatformYes.Name = "RbtRaisablePlatformYes";
            this.RbtRaisablePlatformYes.Size = new System.Drawing.Size(40, 16);
            this.RbtRaisablePlatformYes.TabIndex = 54;
            this.RbtRaisablePlatformYes.Text = "Ja";
            // 
            // RbtRaisablePlatformNo
            // 
            this.RbtRaisablePlatformNo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtRaisablePlatformNo.Location = new System.Drawing.Point(48, 8);
            this.RbtRaisablePlatformNo.Name = "RbtRaisablePlatformNo";
            this.RbtRaisablePlatformNo.Size = new System.Drawing.Size(48, 16);
            this.RbtRaisablePlatformNo.TabIndex = 0;
            this.RbtRaisablePlatformNo.Text = "Nein";
            // 
            // TxtMaskNumberDelivered
            // 
            this.TxtMaskNumberDelivered.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtMaskNumberDelivered.Location = new System.Drawing.Point(102, 52);
            this.TxtMaskNumberDelivered.Name = "TxtMaskNumberDelivered";
            this.TxtMaskNumberDelivered.Size = new System.Drawing.Size(172, 21);
            this.TxtMaskNumberDelivered.TabIndex = 79;
            // 
            // TxtMaskNumberRecieve
            // 
            this.TxtMaskNumberRecieve.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtMaskNumberRecieve.Location = new System.Drawing.Point(102, 26);
            this.TxtMaskNumberRecieve.Name = "TxtMaskNumberRecieve";
            this.TxtMaskNumberRecieve.Size = new System.Drawing.Size(172, 21);
            this.TxtMaskNumberRecieve.TabIndex = 78;
            // 
            // LblDelivered
            // 
            this.LblDelivered.BackColor = System.Drawing.SystemColors.Control;
            this.LblDelivered.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblDelivered.Location = new System.Drawing.Point(288, 55);
            this.LblDelivered.Name = "LblDelivered";
            this.LblDelivered.Size = new System.Drawing.Size(95, 16);
            this.LblDelivered.TabIndex = 48;
            this.LblDelivered.Text = "zurückgegeben";
            // 
            // LblRecieve
            // 
            this.LblRecieve.BackColor = System.Drawing.SystemColors.Control;
            this.LblRecieve.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblRecieve.Location = new System.Drawing.Point(288, 29);
            this.LblRecieve.Name = "LblRecieve";
            this.LblRecieve.Size = new System.Drawing.Size(95, 16);
            this.LblRecieve.TabIndex = 47;
            this.LblRecieve.Text = "verliehen";
            // 
            // LblMaskNumberRecieve
            // 
            this.LblMaskNumberRecieve.BackColor = System.Drawing.SystemColors.Control;
            this.LblMaskNumberRecieve.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMaskNumberRecieve.Location = new System.Drawing.Point(14, 26);
            this.LblMaskNumberRecieve.Name = "LblMaskNumberRecieve";
            this.LblMaskNumberRecieve.Size = new System.Drawing.Size(72, 16);
            this.LblMaskNumberRecieve.TabIndex = 21;
            this.LblMaskNumberRecieve.Text = "Maske-Nr.";
            // 
            // LblMaskNumberDelivered
            // 
            this.LblMaskNumberDelivered.BackColor = System.Drawing.SystemColors.Control;
            this.LblMaskNumberDelivered.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMaskNumberDelivered.Location = new System.Drawing.Point(14, 52);
            this.LblMaskNumberDelivered.Name = "LblMaskNumberDelivered";
            this.LblMaskNumberDelivered.Size = new System.Drawing.Size(72, 16);
            this.LblMaskNumberDelivered.TabIndex = 19;
            this.LblMaskNumberDelivered.Text = "Maske-Nr.";
            // 
            // CboPrecautionaryMedical
            // 
            this.CboPrecautionaryMedical.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboPrecautionaryMedical.ItemHeight = 15;
            this.CboPrecautionaryMedical.Location = new System.Drawing.Point(17, 54);
            this.CboPrecautionaryMedical.Name = "CboPrecautionaryMedical";
            this.CboPrecautionaryMedical.Size = new System.Drawing.Size(399, 23);
            this.CboPrecautionaryMedical.TabIndex = 77;
            // 
            // BtnClear
            // 
            this.BtnClear.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClear.Location = new System.Drawing.Point(952, 636);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(140, 32);
            this.BtnClear.TabIndex = 81;
            this.BtnClear.Text = "&Maske leeren";
            this.TooClearMask.SetToolTip(this.BtnClear, "Löscht alle bereits eingegebenen Suchkriterien");
            this.BtnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // TxtCheckInOn
            // 
            this.TxtCheckInOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCheckInOn.Location = new System.Drawing.Point(221, 18);
            this.TxtCheckInOn.Name = "TxtCheckInOn";
            this.TxtCheckInOn.Size = new System.Drawing.Size(75, 21);
            this.TxtCheckInOn.TabIndex = 71;
            this.TxtCheckInOn.Leave += new System.EventHandler(this.TxtCheckInOn_Leave);
            // 
            // TxtCheckOffOn
            // 
            this.TxtCheckOffOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCheckOffOn.Location = new System.Drawing.Point(221, 66);
            this.TxtCheckOffOn.Name = "TxtCheckOffOn";
            this.TxtCheckOffOn.Size = new System.Drawing.Size(75, 21);
            this.TxtCheckOffOn.TabIndex = 73;
            this.TxtCheckOffOn.Leave += new System.EventHandler(this.TxtCheckOffOn_Leave);
            // 
            // BtnCancel
            // 
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Location = new System.Drawing.Point(1097, 636);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(140, 32);
            this.BtnCancel.TabIndex = 82;
            this.BtnCancel.Text = "&Zurück";
            this.TooBackTo.SetToolTip(this.BtnCancel, "Zurück zur Übersichtsmaske");
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // TxtCheckOffBy
            // 
            this.TxtCheckOffBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCheckOffBy.Location = new System.Drawing.Point(221, 90);
            this.TxtCheckOffBy.Name = "TxtCheckOffBy";
            this.TxtCheckOffBy.Size = new System.Drawing.Size(195, 21);
            this.TxtCheckOffBy.TabIndex = 74;
            // 
            // TxtCheckInBy
            // 
            this.TxtCheckInBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCheckInBy.Location = new System.Drawing.Point(221, 42);
            this.TxtCheckInBy.Name = "TxtCheckInBy";
            this.TxtCheckInBy.Size = new System.Drawing.Size(195, 21);
            this.TxtCheckInBy.TabIndex = 72;
            // 
            // LblCheckInBy
            // 
            this.LblCheckInBy.BackColor = System.Drawing.SystemColors.Control;
            this.LblCheckInBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCheckInBy.Location = new System.Drawing.Point(14, 45);
            this.LblCheckInBy.Name = "LblCheckInBy";
            this.LblCheckInBy.Size = new System.Drawing.Size(56, 16);
            this.LblCheckInBy.TabIndex = 139;
            this.LblCheckInBy.Text = "durch";
            // 
            // LblCheckOffOn
            // 
            this.LblCheckOffOn.BackColor = System.Drawing.SystemColors.Control;
            this.LblCheckOffOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCheckOffOn.Location = new System.Drawing.Point(14, 69);
            this.LblCheckOffOn.Name = "LblCheckOffOn";
            this.LblCheckOffOn.Size = new System.Drawing.Size(104, 16);
            this.LblCheckOffOn.TabIndex = 138;
            this.LblCheckOffOn.Text = "Abmeldung am";
            // 
            // LblCheckOffBy
            // 
            this.LblCheckOffBy.BackColor = System.Drawing.SystemColors.Control;
            this.LblCheckOffBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCheckOffBy.Location = new System.Drawing.Point(14, 93);
            this.LblCheckOffBy.Name = "LblCheckOffBy";
            this.LblCheckOffBy.Size = new System.Drawing.Size(72, 16);
            this.LblCheckOffBy.TabIndex = 137;
            this.LblCheckOffBy.Text = "durch";
            // 
            // PnlCtlRbtPrecautionaryMedical
            // 
            this.PnlCtlRbtPrecautionaryMedical.Controls.Add(this.RbtPrecautionaryMedicalYes);
            this.PnlCtlRbtPrecautionaryMedical.Controls.Add(this.RbtPrecautionaryMedicalNo);
            this.PnlCtlRbtPrecautionaryMedical.Location = new System.Drawing.Point(221, 22);
            this.PnlCtlRbtPrecautionaryMedical.Name = "PnlCtlRbtPrecautionaryMedical";
            this.PnlCtlRbtPrecautionaryMedical.Size = new System.Drawing.Size(114, 24);
            this.PnlCtlRbtPrecautionaryMedical.TabIndex = 134;
            // 
            // RbtPrecautionaryMedicalYes
            // 
            this.RbtPrecautionaryMedicalYes.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtPrecautionaryMedicalYes.Location = new System.Drawing.Point(8, 3);
            this.RbtPrecautionaryMedicalYes.Name = "RbtPrecautionaryMedicalYes";
            this.RbtPrecautionaryMedicalYes.Size = new System.Drawing.Size(40, 16);
            this.RbtPrecautionaryMedicalYes.TabIndex = 75;
            this.RbtPrecautionaryMedicalYes.Text = "Ja";
            // 
            // RbtPrecautionaryMedicalNo
            // 
            this.RbtPrecautionaryMedicalNo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtPrecautionaryMedicalNo.Location = new System.Drawing.Point(48, 3);
            this.RbtPrecautionaryMedicalNo.Name = "RbtPrecautionaryMedicalNo";
            this.RbtPrecautionaryMedicalNo.Size = new System.Drawing.Size(63, 18);
            this.RbtPrecautionaryMedicalNo.TabIndex = 76;
            this.RbtPrecautionaryMedicalNo.Text = "Nein";
            // 
            // ButSearch
            // 
            this.ButSearch.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButSearch.Location = new System.Drawing.Point(807, 636);
            this.ButSearch.Name = "ButSearch";
            this.ButSearch.Size = new System.Drawing.Size(140, 32);
            this.ButSearch.TabIndex = 80;
            this.ButSearch.Text = "&Suchen";
            this.TooSearch.SetToolTip(this.ButSearch, "Löst den Suchvorgang aus");
            this.ButSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // LblPrecautionaryMedical
            // 
            this.LblPrecautionaryMedical.BackColor = System.Drawing.SystemColors.Control;
            this.LblPrecautionaryMedical.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblPrecautionaryMedical.Location = new System.Drawing.Point(14, 25);
            this.LblPrecautionaryMedical.Name = "LblPrecautionaryMedical";
            this.LblPrecautionaryMedical.Size = new System.Drawing.Size(160, 20);
            this.LblPrecautionaryMedical.TabIndex = 0;
            this.LblPrecautionaryMedical.Text = "Vorsorgeuntersuchung";
            // 
            // LblDeliveryDate
            // 
            this.LblDeliveryDate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblDeliveryDate.Location = new System.Drawing.Point(13, 23);
            this.LblDeliveryDate.Name = "LblDeliveryDate";
            this.LblDeliveryDate.Size = new System.Drawing.Size(184, 21);
            this.LblDeliveryDate.TabIndex = 38;
            this.LblDeliveryDate.Text = "Ausgabedatum Passierschen";
            // 
            // TxtDeliveryDate
            // 
            this.TxtDeliveryDate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtDeliveryDate.Location = new System.Drawing.Point(220, 23);
            this.TxtDeliveryDate.Name = "TxtDeliveryDate";
            this.TxtDeliveryDate.Size = new System.Drawing.Size(195, 21);
            this.TxtDeliveryDate.TabIndex = 68;
            this.TxtDeliveryDate.Leave += new System.EventHandler(this.TxtDeliveryDate_Leave);
            // 
            // LblValidUntil
            // 
            this.LblValidUntil.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblValidUntil.Location = new System.Drawing.Point(303, 50);
            this.LblValidUntil.Name = "LblValidUntil";
            this.LblValidUntil.Size = new System.Drawing.Size(24, 21);
            this.LblValidUntil.TabIndex = 42;
            this.LblValidUntil.Text = "bis";
            // 
            // LblValidFrom
            // 
            this.LblValidFrom.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblValidFrom.Location = new System.Drawing.Point(13, 47);
            this.LblValidFrom.Name = "LblValidFrom";
            this.LblValidFrom.Size = new System.Drawing.Size(72, 21);
            this.LblValidFrom.TabIndex = 40;
            this.LblValidFrom.Text = "Gültig von";
            // 
            // TxtValidFrom
            // 
            this.TxtValidFrom.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtValidFrom.Location = new System.Drawing.Point(220, 47);
            this.TxtValidFrom.Name = "TxtValidFrom";
            this.TxtValidFrom.Size = new System.Drawing.Size(75, 21);
            this.TxtValidFrom.TabIndex = 69;
            this.TxtValidFrom.Leave += new System.EventHandler(this.TxtValidFrom_Leave);
            // 
            // TxtValidUntil
            // 
            this.TxtValidUntil.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtValidUntil.Location = new System.Drawing.Point(340, 47);
            this.TxtValidUntil.Name = "TxtValidUntil";
            this.TxtValidUntil.Size = new System.Drawing.Size(75, 21);
            this.TxtValidUntil.TabIndex = 70;
            this.TxtValidUntil.Leave += new System.EventHandler(this.TxtValidUntil_Leave);
            // 
            // LblCheckInOn
            // 
            this.LblCheckInOn.BackColor = System.Drawing.SystemColors.Control;
            this.LblCheckInOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCheckInOn.Location = new System.Drawing.Point(14, 21);
            this.LblCheckInOn.Name = "LblCheckInOn";
            this.LblCheckInOn.Size = new System.Drawing.Size(104, 24);
            this.LblCheckInOn.TabIndex = 140;
            this.LblCheckInOn.Text = "Anmeldung am";
            // 
            // PnlDataExternalSearch
            // 
            this.PnlDataExternalSearch.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PnlDataExternalSearch.Controls.Add(this.BtnClear);
            this.PnlDataExternalSearch.Controls.Add(this.BtnCancel);
            this.PnlDataExternalSearch.Controls.Add(this.PnlPersNr);
            this.PnlDataExternalSearch.Controls.Add(this.ButSearch);
            this.PnlDataExternalSearch.Controls.Add(this.pnlIdCards);
            this.PnlDataExternalSearch.Controls.Add(this.PnlMedical);
            this.PnlDataExternalSearch.Controls.Add(this.PnlMask);
            this.PnlDataExternalSearch.Controls.Add(this.PnlPass);
            this.PnlDataExternalSearch.Controls.Add(this.PnlCheckInOff);
            this.PnlDataExternalSearch.Location = new System.Drawing.Point(8, 221);
            this.PnlDataExternalSearch.Name = "PnlDataExternalSearch";
            this.PnlDataExternalSearch.Size = new System.Drawing.Size(1254, 681);
            this.PnlDataExternalSearch.TabIndex = 0;
            // 
            // PnlPersNr
            // 
            this.PnlPersNr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlPersNr.Controls.Add(this.gpPersNr);
            this.PnlPersNr.Location = new System.Drawing.Point(10, 570);
            this.PnlPersNr.Name = "PnlPersNr";
            this.PnlPersNr.Size = new System.Drawing.Size(741, 65);
            this.PnlPersNr.TabIndex = 82;
            // 
            // gpPersNr
            // 
            this.gpPersNr.Controls.Add(this.TxtPersNrFPASS);
            this.gpPersNr.Controls.Add(this.lblPersNrSmAct);
            this.gpPersNr.Controls.Add(this.lblPersNrFPASS);
            this.gpPersNr.Controls.Add(this.TxtPersNrSmAct);
            this.gpPersNr.Location = new System.Drawing.Point(5, 5);
            this.gpPersNr.Name = "gpPersNr";
            this.gpPersNr.Size = new System.Drawing.Size(726, 50);
            this.gpPersNr.TabIndex = 0;
            this.gpPersNr.TabStop = false;
            this.gpPersNr.Text = "Personalnummern";
            // 
            // TxtPersNrFPASS
            // 
            this.TxtPersNrFPASS.Font = new System.Drawing.Font("Arial", 9F);
            this.TxtPersNrFPASS.Location = new System.Drawing.Point(495, 19);
            this.TxtPersNrFPASS.Name = "TxtPersNrFPASS";
            this.TxtPersNrFPASS.Size = new System.Drawing.Size(195, 21);
            this.TxtPersNrFPASS.TabIndex = 73;
            // 
            // lblPersNrSmAct
            // 
            this.lblPersNrSmAct.Font = new System.Drawing.Font("Arial", 9F);
            this.lblPersNrSmAct.Location = new System.Drawing.Point(12, 22);
            this.lblPersNrSmAct.Name = "lblPersNrSmAct";
            this.lblPersNrSmAct.Size = new System.Drawing.Size(124, 23);
            this.lblPersNrSmAct.TabIndex = 27;
            this.lblPersNrSmAct.Text = "Personalnr. SmartAct";
            // 
            // lblPersNrFPASS
            // 
            this.lblPersNrFPASS.Font = new System.Drawing.Font("Arial", 9F);
            this.lblPersNrFPASS.Location = new System.Drawing.Point(376, 22);
            this.lblPersNrFPASS.Name = "lblPersNrFPASS";
            this.lblPersNrFPASS.Size = new System.Drawing.Size(112, 23);
            this.lblPersNrFPASS.TabIndex = 29;
            this.lblPersNrFPASS.Text = "Personalnr. FPASS";
            // 
            // TxtPersNrSmAct
            // 
            this.TxtPersNrSmAct.Font = new System.Drawing.Font("Arial", 9F);
            this.TxtPersNrSmAct.Location = new System.Drawing.Point(141, 19);
            this.TxtPersNrSmAct.Name = "TxtPersNrSmAct";
            this.TxtPersNrSmAct.Size = new System.Drawing.Size(195, 21);
            this.TxtPersNrSmAct.TabIndex = 72;
            // 
            // pnlIdCards
            // 
            this.pnlIdCards.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlIdCards.Controls.Add(this.gpIdCards);
            this.pnlIdCards.Location = new System.Drawing.Point(761, 10);
            this.pnlIdCards.Name = "pnlIdCards";
            this.pnlIdCards.Size = new System.Drawing.Size(477, 88);
            this.pnlIdCards.TabIndex = 81;
            // 
            // gpIdCards
            // 
            this.gpIdCards.Controls.Add(this.TxtIDCardNoMifare);
            this.gpIdCards.Controls.Add(this.LblIDCardNoHitag);
            this.gpIdCards.Controls.Add(this.LblIDCardNoMifare);
            this.gpIdCards.Controls.Add(this.TxtIDCardNoHitag);
            this.gpIdCards.Location = new System.Drawing.Point(5, 5);
            this.gpIdCards.Name = "gpIdCards";
            this.gpIdCards.Size = new System.Drawing.Size(462, 76);
            this.gpIdCards.TabIndex = 0;
            this.gpIdCards.TabStop = false;
            this.gpIdCards.Text = "Ausweise";
            // 
            // TxtIDCardNoMifare
            // 
            this.TxtIDCardNoMifare.Font = new System.Drawing.Font("Arial", 9F);
            this.TxtIDCardNoMifare.Location = new System.Drawing.Point(221, 43);
            this.TxtIDCardNoMifare.Name = "TxtIDCardNoMifare";
            this.TxtIDCardNoMifare.Size = new System.Drawing.Size(195, 21);
            this.TxtIDCardNoMifare.TabIndex = 28;
            // 
            // LblIDCardNoMifare
            // 
            this.LblIDCardNoMifare.Font = new System.Drawing.Font("Arial", 9F);
            this.LblIDCardNoMifare.Location = new System.Drawing.Point(13, 44);
            this.LblIDCardNoMifare.Name = "LblIDCardNoMifare";
            this.LblIDCardNoMifare.Size = new System.Drawing.Size(147, 23);
            this.LblIDCardNoMifare.TabIndex = 29;
            this.LblIDCardNoMifare.Text = "Ausweisnummer Mifare";
            // 
            // PnlMedical
            // 
            this.PnlMedical.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlMedical.Controls.Add(this.gpPrecMed);
            this.PnlMedical.Location = new System.Drawing.Point(761, 343);
            this.PnlMedical.Name = "PnlMedical";
            this.PnlMedical.Size = new System.Drawing.Size(477, 108);
            this.PnlMedical.TabIndex = 75;
            // 
            // gpPrecMed
            // 
            this.gpPrecMed.Controls.Add(this.PnlCtlRbtPrecautionaryMedical);
            this.gpPrecMed.Controls.Add(this.CboPrecautionaryMedical);
            this.gpPrecMed.Controls.Add(this.LblPrecautionaryMedical);
            this.gpPrecMed.Location = new System.Drawing.Point(5, 5);
            this.gpPrecMed.Name = "gpPrecMed";
            this.gpPrecMed.Size = new System.Drawing.Size(462, 95);
            this.gpPrecMed.TabIndex = 135;
            this.gpPrecMed.TabStop = false;
            this.gpPrecMed.Text = "Vorsorgeuntersuchungen";
            // 
            // PnlMask
            // 
            this.PnlMask.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlMask.Controls.Add(this.gpMask);
            this.PnlMask.Location = new System.Drawing.Point(761, 459);
            this.PnlMask.Name = "PnlMask";
            this.PnlMask.Size = new System.Drawing.Size(477, 100);
            this.PnlMask.TabIndex = 78;
            // 
            // gpMask
            // 
            this.gpMask.Controls.Add(this.TxtMaskNumberDelivered);
            this.gpMask.Controls.Add(this.LblMaskNumberRecieve);
            this.gpMask.Controls.Add(this.TxtMaskNumberRecieve);
            this.gpMask.Controls.Add(this.LblMaskNumberDelivered);
            this.gpMask.Controls.Add(this.LblDelivered);
            this.gpMask.Controls.Add(this.LblRecieve);
            this.gpMask.Location = new System.Drawing.Point(5, 5);
            this.gpMask.Name = "gpMask";
            this.gpMask.Size = new System.Drawing.Size(462, 85);
            this.gpMask.TabIndex = 82;
            this.gpMask.TabStop = false;
            this.gpMask.Text = "Atemschutzmasken";
            // 
            // PnlPass
            // 
            this.PnlPass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlPass.Controls.Add(this.gpPass);
            this.PnlPass.Location = new System.Drawing.Point(761, 105);
            this.PnlPass.Name = "PnlPass";
            this.PnlPass.Size = new System.Drawing.Size(477, 90);
            this.PnlPass.TabIndex = 68;
            // 
            // gpPass
            // 
            this.gpPass.Controls.Add(this.TxtValidFrom);
            this.gpPass.Controls.Add(this.LblDeliveryDate);
            this.gpPass.Controls.Add(this.TxtValidUntil);
            this.gpPass.Controls.Add(this.LblValidFrom);
            this.gpPass.Controls.Add(this.TxtDeliveryDate);
            this.gpPass.Controls.Add(this.LblValidUntil);
            this.gpPass.Location = new System.Drawing.Point(5, 5);
            this.gpPass.Name = "gpPass";
            this.gpPass.Size = new System.Drawing.Size(462, 78);
            this.gpPass.TabIndex = 71;
            this.gpPass.TabStop = false;
            this.gpPass.Text = "Passierschein";
            // 
            // PnlCheckInOff
            // 
            this.PnlCheckInOff.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlCheckInOff.Controls.Add(this.gpCheckInOff);
            this.PnlCheckInOff.Location = new System.Drawing.Point(761, 203);
            this.PnlCheckInOff.Name = "PnlCheckInOff";
            this.PnlCheckInOff.Size = new System.Drawing.Size(477, 132);
            this.PnlCheckInOff.TabIndex = 71;
            // 
            // gpCheckInOff
            // 
            this.gpCheckInOff.Controls.Add(this.LblCheckOffOn);
            this.gpCheckInOff.Controls.Add(this.LblCheckInOn);
            this.gpCheckInOff.Controls.Add(this.LblCheckOffBy);
            this.gpCheckInOff.Controls.Add(this.LblCheckInBy);
            this.gpCheckInOff.Controls.Add(this.TxtCheckOffBy);
            this.gpCheckInOff.Controls.Add(this.TxtCheckInBy);
            this.gpCheckInOff.Controls.Add(this.TxtCheckInOn);
            this.gpCheckInOff.Controls.Add(this.TxtCheckOffOn);
            this.gpCheckInOff.Location = new System.Drawing.Point(5, 5);
            this.gpCheckInOff.Name = "gpCheckInOff";
            this.gpCheckInOff.Size = new System.Drawing.Size(462, 120);
            this.gpCheckInOff.TabIndex = 82;
            this.gpCheckInOff.TabStop = false;
            this.gpCheckInOff.Text = "An-/Abmeldung";
            // 
            // LblMaskTitle
            // 
            this.LblMaskTitle.BackColor = System.Drawing.Color.White;
            this.LblMaskTitle.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMaskTitle.Location = new System.Drawing.Point(445, 8);
            this.LblMaskTitle.Name = "LblMaskTitle";
            this.LblMaskTitle.Size = new System.Drawing.Size(376, 32);
            this.LblMaskTitle.TabIndex = 59;
            this.LblMaskTitle.Text = "FPASS - Erweiterte Suche";
            // 
            // FrmExtendedSearch
            // 
            this.AcceptButton = this.ButSearch;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.BtnCancel;
            this.ClientSize = new System.Drawing.Size(1264, 921);
            this.Controls.Add(this.LblMaskTitle);
            this.Controls.Add(this.PnlDataExternalContractor);
            this.Controls.Add(this.PnlBriefingSite);
            this.Controls.Add(this.PnlDataExternalSearch);
            this.Controls.Add(this.LblHeadline);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmExtendedSearch";
            this.Text = "FPASS - Erweiterte Suche";
            this.Controls.SetChildIndex(this.LblHeadline, 0);
            this.Controls.SetChildIndex(this.LblBaseHead, 0);
            this.Controls.SetChildIndex(this.StbBase, 0);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            this.Controls.SetChildIndex(this.PnlDataExternalSearch, 0);
            this.Controls.SetChildIndex(this.PnlBriefingSite, 0);
            this.Controls.SetChildIndex(this.PnlDataExternalContractor, 0);
            this.Controls.SetChildIndex(this.LblMaskTitle, 0);
            ((System.ComponentModel.ISupportInitialize)(this.SbpMessage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sbpenvironment)).EndInit();
            this.PnlDataExternalContractor.ResumeLayout(false);
            this.PnlDataExternalContractor.PerformLayout();
            this.PnlBriefingSite.ResumeLayout(false);
            this.gpBriefings.ResumeLayout(false);
            this.gpBriefings.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.PnlCtlRbtIdPhotoSmAct.ResumeLayout(false);
            this.PnlCtlRbtSafetyInstructions.ResumeLayout(false);
            this.PnlVehicleLong.ResumeLayout(false);
            this.PnlVehicleShortCo.ResumeLayout(false);
            this.PnlVehicleShort.ResumeLayout(false);
            this.PnlVehicleLongCo.ResumeLayout(false);
            this.PnlBreathingApparatusBriefingG263.ResumeLayout(false);
            this.PnlCtlRbtSafetyAtWork.ResumeLayout(false);
            this.PnlAccessAuthorization.ResumeLayout(false);
            this.PnlCtlRbtBreathingApparatus.ResumeLayout(false);
            this.PnlCtlRbtIdPhotoHitag.ResumeLayout(false);
            this.PnlCtlRbtBriefingSiteSecurity.ResumeLayout(false);
            this.PnlCtlRbtBriefingSafetyAtWork.ResumeLayout(false);
            this.PnlCtlRbtBriefingPlant.ResumeLayout(false);
            this.PnlCtlRbtRespiratoryMask.ResumeLayout(false);
            this.PnlCtlRbtCranes.ResumeLayout(false);
            this.PnlCtlRbtPalletLifter.ResumeLayout(false);
            this.PnlCtlRbtRaisablePlatform.ResumeLayout(false);
            this.PnlCtlRbtPrecautionaryMedical.ResumeLayout(false);
            this.PnlDataExternalSearch.ResumeLayout(false);
            this.PnlPersNr.ResumeLayout(false);
            this.gpPersNr.ResumeLayout(false);
            this.gpPersNr.PerformLayout();
            this.pnlIdCards.ResumeLayout(false);
            this.gpIdCards.ResumeLayout(false);
            this.gpIdCards.PerformLayout();
            this.PnlMedical.ResumeLayout(false);
            this.gpPrecMed.ResumeLayout(false);
            this.PnlMask.ResumeLayout(false);
            this.gpMask.ResumeLayout(false);
            this.gpMask.PerformLayout();
            this.PnlPass.ResumeLayout(false);
            this.gpPass.ResumeLayout(false);
            this.gpPass.PerformLayout();
            this.PnlCheckInOff.ResumeLayout(false);
            this.gpCheckInOff.ResumeLayout(false);
            this.gpCheckInOff.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		#region Accessors 

		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// Fill all drop-down comboboxes and listboxes in the form
		/// </summary>
		internal override void FillLists()
		{
			FillExternalContractor("0");
			FillCoordinator("0");
			FillDepartment();			
			FillPlantOne();
			FillPlantTwo();
			FillPlantThree();
			FillSubcontractor();
			FillCraftNumber();
			FillPrecautionaryMedical();
			FillStatus();
			FillPrecautionaryMedical();
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
		/// Is called before the view is closed. empty implmentation
		/// </summary>
		internal override void PreClose()
		{

		}

		/// <summary>
		/// Is called before the view is shown. All fields are cleared.
		/// </summary>
		internal override void PreShow()
		{
			ClearFields();
		}

		/// <summary>
		/// Gets controller of this triad. For convenience to avoid casting
		/// </summary>
		/// <returns>Controller of This dialog triad</returns>
		private ExtendedSearchController GetMyController()
		{
			return (ExtendedSearchController)mController;
		}


		/// <summary>
		/// Refills contractor list
		/// </summary>
		/// <param name="pContractorID"></param>
		internal override void ReFillContractorList(String pContractorID) 
		{
			FillCoordinator("0");
			FillExternalContractor(pContractorID);
		}

		/// <summary>
		/// Fills combobox externalContractor with all excontractor which belong to
		/// the given coordinator.
		/// </summary>
		/// <param name="pCoordinatorID">coordinatorID</param>
		private void FillExternalContractor(String pCoordinatorID)
		{
			this.CboExternalContractor.DataSource = this.GetContractorList(pCoordinatorID);
			this.CboExternalContractor.DisplayMember = "ContractorName";
			this.CboExternalContractor.ValueMember = "ContractorID";
		}

	
		/// <summary>
		/// fills combobox precautionaryMedical
		/// </summary>
		private void FillPrecautionaryMedical()
		{
			ArrayList precautionaryMedical = new ArrayList(); 
			precautionaryMedical.Add( new LovItem("0", "" ) );
			precautionaryMedical.AddRange( LovSingleton.GetInstance().
				GetRootList(null, "FPASS_PRECMEDTYPELOV", "PMTY_FULLNAME") );			
			this.CboPrecautionaryMedical.DataSource = precautionaryMedical;
			this.CboPrecautionaryMedical.DisplayMember = "ItemValue";
			this.CboPrecautionaryMedical.ValueMember = "DecId";
		}

		/// <summary>
		/// fills combobox coordinator
		/// </summary>
		/// <param name="pID"></param>
		private void FillCoordinator(String pID)
		{
			this.CboCoordinator.DataSource = this.GetCoordinatorList(pID);
			this.CboCoordinator.DisplayMember = "CoordFullNameTel";
			this.CboCoordinator.ValueMember = "CoordID";
		}

		/// <summary>
		/// fills combobox subcontractor
		/// </summary>
		private void FillSubcontractor()
		{
			ArrayList subcontractor = new ArrayList(); 
			subcontractor.Add(new LovItem("0", ""));
			subcontractor.AddRange (LovSingleton.GetInstance().GetRootList(null, "FPASS_EXCONTRACTOR", "EXCO_NAME"));			
			this.CboSubcontractor.DataSource = subcontractor;
			this.CboSubcontractor.DisplayMember = "ItemValue";
			this.CboSubcontractor.ValueMember = "DecId";
		}

		/// <summary>
		/// fills top combobox plant
		/// </summary>
		private void FillPlantOne()
		{
			ArrayList plant = new ArrayList(); 
			plant.Add(new LovItem("0", ""));
			plant.AddRange( LovSingleton.GetInstance().GetRootList(null, "FPASS_PLANT", "PL_NAME"));			
			this.CboPlantOne.DataSource = plant;
			this.CboPlantOne.DisplayMember = "ItemValue";
			this.CboPlantOne.ValueMember = "DecId";
		}

		/// <summary>
		/// fills middle combobox plant
		/// </summary>
		private void FillPlantTwo()
		{
			ArrayList plant = new ArrayList(); 
			plant.Add(new LovItem("0", ""));
			plant.AddRange( LovSingleton.GetInstance().GetRootList(null, "FPASS_PLANT", "PL_NAME"));			
			this.CboPlantTwo.DataSource = plant;
			this.CboPlantTwo.DisplayMember = "ItemValue";
			this.CboPlantTwo.ValueMember = "DecId";
		}

		/// <summary>
		/// fills bottom combobox plant
		/// </summary>
		private void FillPlantThree()
		{
			ArrayList plant = new ArrayList(); 
			plant.Add(new LovItem("0", ""));
			plant.AddRange( LovSingleton.GetInstance().GetRootList(null, "FPASS_PLANT", "PL_NAME"));			
			this.CboPlantThree.DataSource = plant;
			this.CboPlantThree.DisplayMember = "ItemValue";
			this.CboPlantThree.ValueMember = "DecId";
		}

		/// <summary>
		/// fills combobox department
		/// </summary>
		private void FillDepartment()
		{
			ArrayList department = new ArrayList(); 
			department.Add(new LovItem("0", ""));
			department.AddRange (LovSingleton.GetInstance().GetRootList(null, "FPASS_DEPARTMENT", "DEPT_DEPARTMENT"));			
			this.CboDepartment.DataSource = department;
			this.CboDepartment.DisplayMember = "ItemValue";
			this.CboDepartment.ValueMember = "DecId";
		}

		/// <summary>
		/// fills combobox craftnumber
		/// </summary>
		private void FillCraftNumber()
		{
			ArrayList craftnumber = new ArrayList(); 
			craftnumber.Add(new LovItem("0", ""));
			craftnumber.AddRange (LovSingleton.GetInstance().GetRootList(null, "FPASS_CRAFT", "CRA_CRAFTNO"));			
			this.CboCraftNumber.DataSource = craftnumber;
			this.CboCraftNumber.DisplayMember = "ItemValue";
			this.CboCraftNumber.ValueMember = "DecId";
		}

		/// <summary>
		/// Fills combobox Status. The Status are defined in code, in Globals
		/// Work with ListOfValues
		/// Last change 17.02.2005: forgot status ALTDATEN
		/// </summary>
		internal void FillStatus()
		{
			ArrayList status	 = new ArrayList(); 
			string statusValid   = Globals.STATUS_VALID;
			string statusInvalid = Globals.STATUS_INVALID;
			string statusOld	 = Globals.STATUS_OLD;
			
			LovItem lovValid   = new LovItem( "1", statusValid );
			LovItem lovInvalid = new LovItem( "-1", statusInvalid );	
			LovItem lovOld     = new LovItem( "-2", statusOld );
			status.Add( new LovItem("0", "") );
			status.Add( lovValid );
			status.Add( lovInvalid );
			status.Add( lovOld );
			this.CboStatus.DataSource = status;
			this.CboStatus.DisplayMember = "ItemValue";
			this.CboStatus.ValueMember = "DecId";
		}


		/// <summary>
		/// Sets search fields back to their default values
		/// </summary>
		private void ClearFields()
		{
			FillLists();

			this.RbtBreathingApparatusG262BriefingNo.Checked = false;
			this.RbtBreathingApparatusG262BriefingYes.Checked = false;
			this.RbtBreathingApparatusG263BriefingNo.Checked = false;
			this.RbtBreathingApparatusG263BriefingYes.Checked = false;
			this.RbtFireBriefingNo.Checked = false;
			this.RbtFireBriefingYes.Checked = false;
			this.RbtBriefingPlantNo.Checked = false;
			this.RbtBriefingPlantYes.Checked = false;
			this.RbtCranesNo.Checked = false;
			this.RbtCranesYes.Checked = false;
			this.RbtIdHitagNo.Checked = false;
			this.RbtIdHitagYes.Checked = false;
            this.RbtIdSmActNo.Checked = false;
            this.RbtIdSmActYes.Checked = false;
			this.RbtIndustrialSafetyBriefingNo.Checked = false;
			this.RbtIndustrialSafetyBriefingYes.Checked = false;
			this.RbtPalletLifterNo.Checked = false;
			this.RbtPalletLifterYes.Checked = false;
			this.RbtPrecautionaryMedicalNo.Checked = false;
			this.RbtPrecautionaryMedicalYes.Checked = false;
			this.RbtRaisablePlatformNo.Checked = false;
			this.RbtRaisablePlatformYes.Checked = false;
			this.RbtRespiratoryMaskNo.Checked = false;
			this.RbtRespiratoryMaskYes.Checked = false;
			this.RbtSafetyAtWorkServiceBriefingNo.Checked = false;
			this.RbtSafetyAtWorkServiceBriefingYes.Checked = false;
			this.RbtSafetyInstructionsNo.Checked = false;
			this.RbtSafetyInstructionsYes.Checked = false;
			this.RbtSiteSecurityBriefingNo.Checked = false;
			this.RbtSiteSecurityBriefingYes.Checked = false;
			this.RbtAccessAuthorizationNo.Checked = false;
			this.RbtAccessAuthorizationYes.Checked = false;
			this.RbtVehicleLongCoNo.Checked = false;
			this.RbtVehicleLongCoYes.Checked = false;
			this.RbtVehicleLongNo.Checked = false;
			this.RbtVehicleLongYes.Checked = false;
			this.RbtVehicleShortCoNo.Checked = false;
			this.RbtVehicleShortCoYes.Checked = false;
			this.RbtVehicleShortNo.Checked = false;
			this.RbtVehicleShortYes.Checked = false;

			this.TxtBreathingApparatusBriefingg262On.Text = "";
			this.TxtBreathingApparatusBriefingG263On.Text = "";
			this.TxtBreathingApparatusBriefingG263By.Text = "";
			this.TxtBreathingApparatusG262BriefingBy.Text = "";
			this.TxtFireBriefingBy.Text  = "";
			this.TxtFireBriefingOn.Text  = "";
			this.TxtBriefingPlantBy.Text = "";
			this.TxtCheckInBy.Text = "";
			this.TxtAccessAuthorizationBy.Text = "";
			this.TxtAccessAuthorizationOn.Text = "";
			this.TxtSafetyInstructionsBy.Text = "";
			this.TxtSafetyInstructionsOn.Text = "";
			this.TxtCheckOffBy.Text = "";
			this.TxtCranesBy.Text = "";
			this.TxtDateOfBirth.Text = "";
			this.TxtFirstname.Text = "";
			this.TxtIdHitagBy.Text = "";
            this.TxtIdSmActBy.Text = "";
			this.TxtIndustrialSafetyBriefingBy.Text = "";
			this.TxtMaskNumberDelivered.Text = "";
			this.TxtMaskNumberRecieve.Text = "";
			this.TxtOrderNumber.Text = "";
			this.TxtPalletLifterBy.Text = "";
			this.TxtPhone.Text = "";
			this.TxtPlaceOfBirth.Text = "";
			this.TxtRaisablePlatformBy.Text = "";
			this.TxtRespiratoryMaskBy.Text = "";
			this.TxtSafetyAtWorkServiceBriefingBy.Text = "";
			this.TxtSiteSecurityBriefingBy.Text = "";
			this.TxtSupervisor.Text = "";
			this.TxtSurname.Text = "";
			this.TxtVehicleRegNumber.Text = "";
			this.TxtVehicleShortBy.Text = "";
			this.TxtVehicleLongBy.Text = "";

			this.TxtValidUntil.Text = "";
			this.TxtValidFrom.Text = "";
			this.TxtSiteSecurityBriefingOn.Text = "";
			this.TxtSafetyAtWorkServiceBriefingOn.Text = "";
			this.TxtRespiratoryMaskOn.Text = "";
			this.TxtRaisablePlatformOn.Text = "";
			this.TxtPalletLifterOn.Text = "";
			this.TxtIndustrialSafetyBriefingOn.Text = "";
			this.TxtIdHitagOn.Text = "";
            this.TxtIdSmActOn.Text = "";
			this.TxtDeliveryDate.Text = "";
			this.TxtDateOfBirth.Text = "";
			this.TxtCranesOn.Text = "";
			this.TxtCheckOffOn.Text = "";
			this.TxtCheckInOn.Text = "";
			this.TxtBriefingPlantOn.Text = "";
			this.TxtBreathingApparatusBriefingg262On.Text = "";
			this.TxtBreathingApparatusBriefingG263On.Text = "";
			this.TxtVehicleLongOn.Text = "";
			this.TxtVehicleShortOn.Text = "";
            this.TxtIDCardNoHitag.Text = "";
            this.TxtIDCardNoMifare.Text = "";

			this.StbBase.Panels[2].Text = String.Empty;
		}

		
		/// <summary>
		/// Last change 16.01.04, Lost in SourceSafe
		/// Method Empty 16.01.04
		/// </summary>
		private void SetAuthorization()
		{
		}

		#endregion Methods

		#region Events

		private void CboExternalContractor_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			decimal contractorID = ((ContractorLovItem)CboExternalContractor.SelectedItem).ContractorID;
			if ( contractorID != 0 ) 
			{
				if ( ! mCboCoordinatorIsLeading ) 
				{
					mCboContractorIsLeading = true;
					mCboCoordinatorIsLeading = false;
					FillCoordinator(contractorID.ToString());
				}
			} 
			else 
			{
				if ( mCboContractorIsLeading ) 
				{
					mCboContractorIsLeading = false;
					mCboCoordinatorIsLeading = false;
					FillCoordinator("0");
					FillExternalContractor("0");
				}
			}
		
		}

		private void CboCoordinator_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			decimal coordID = ((CoordLovItem)CboCoordinator.SelectedItem).CoordID;
			if ( coordID != 0 ) 
			{
				if ( ! mCboContractorIsLeading ) 
				{
					mCboCoordinatorIsLeading = true;
					mCboContractorIsLeading = false;
					this.FillExternalContractor(coordID.ToString());
				}
			} 
			else 
			{
				if ( mCboCoordinatorIsLeading ) 
				{
					mCboContractorIsLeading = false;
					mCboCoordinatorIsLeading = false;
					FillCoordinator("0");
					FillExternalContractor("0");
				}
			}
		
		}

		private void BtnClear_Click(object sender, System.EventArgs e)
		{
			this.ClearFields();
		}

        /// <summary>
        /// Raised when user clicks button "Suchen".
        /// Important to close form here and not implicitly in controller
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, System.EventArgs e)
        {
            ExtendedSearchController extSrchController = (ExtendedSearchController)mController;

            try
            {
                extSrchController.HandleSearchCoworker();

                if (extSrchController.ResultsFound)
                    extSrchController.HandleCloseDialog();
                else
                    MessageBox.Show(MessageSingleton.GetInstance().GetMessage(MessageSingleton.NO_RESULTS), "FPASS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (UIWarningException uwe)
            {
                ExceptionProcessor.GetInstance().Process(uwe);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Raised when user clicks Cancel: empties fields and closes Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void BtnCancel_Click(object sender, System.EventArgs e)
		{
			ClearFields();
			((ExtendedSearchController)mController).HandleCloseDialog();
		}

		
		#region LeaveEvents
		/// <summary>
		/// These events fire when the user leaves the search fields.
		/// They all require a valid date in format 'DD.MM.YYYY', if value is not in this format then warning.
		/// If field empty after editing then no warning
        /// TODO in a future release: rationalise this and make one generic method which does date check and pass TxtBox to it
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TxtFireBriefingOn_Leave(object sender, System.EventArgs e)
		{
			if ( this.TxtFireBriefingOn.Text.Length > 0 )
			{
				if ( ! StringValidation.GetInstance().IsDateString (this.TxtFireBriefingOn.Text.Trim()) ) 
				{
					try 
					{
						throw new UIWarningException(MessageSingleton.GetInstance().
							GetMessage(MessageSingleton.INVALID_FULLDATE) );
							
					} 
					catch ( UIWarningException uwe ) 
					{
						ExceptionProcessor.GetInstance().Process(uwe);
						TxtFireBriefingOn.Focus();
					}
				}
			}
		}


		private void TxtDateOfBirth_Leave(object sender, System.EventArgs e)
		{
			if (this.TxtDateOfBirth.Text.Length > 0)
			{
				if ( ! StringValidation.GetInstance().IsDateString (this.TxtDateOfBirth.Text.Trim()) ) 
				{
					try 
					{
						throw new UIWarningException(MessageSingleton.GetInstance().
							GetMessage(MessageSingleton.INVALID_BIRTHDATE) );
						
					} 
					catch ( UIWarningException uwe ) 
					{
						ExceptionProcessor.GetInstance().Process(uwe);
						TxtDateOfBirth.Focus();
					}
				}	
			}
		}

		private void TxtSafetyInstructionsOn_Leave(object sender, System.EventArgs e)
		{
			if (this.TxtSafetyInstructionsOn.Text.Length > 0 )
			{
				if ( ! StringValidation.GetInstance().IsDateString (this.TxtSafetyInstructionsOn.Text.Trim()) ) 
				{
					try 
					{
						throw new UIWarningException(MessageSingleton.GetInstance().
							GetMessage(MessageSingleton.INVALID_FULLDATE) );
						
					} 
					catch ( UIWarningException uwe ) 
					{
						ExceptionProcessor.GetInstance().Process(uwe);
						TxtSafetyInstructionsOn.Focus();
					}
				}
			}
		}

		private void TxtAccessAuthorizationOn_Leave(object sender, System.EventArgs e)
		{
			if ( this.TxtAccessAuthorizationOn.Text.Length > 0 )
			{
				if ( ! StringValidation.GetInstance().IsDateString (this.TxtAccessAuthorizationOn.Text.Trim()) ) 
				{
					try 
					{
						throw new UIWarningException(MessageSingleton.GetInstance().
							GetMessage(MessageSingleton.INVALID_FULLDATE) );	
					} 
					catch ( UIWarningException uwe ) 
					{
						ExceptionProcessor.GetInstance().Process(uwe);
						TxtAccessAuthorizationOn.Focus();
					}
				}
			}
		}

		private void TxtIndustrialSafetyBriefingOn_Leave(object sender, System.EventArgs e)
		{
			if (this.TxtIndustrialSafetyBriefingOn.Text.Length > 0 )
			{
				if ( ! StringValidation.GetInstance().IsDateString (this.TxtIndustrialSafetyBriefingOn.Text.Trim()) ) 
				{
					try 
					{
						throw new UIWarningException(MessageSingleton.GetInstance().
							GetMessage(MessageSingleton.INVALID_FULLDATE) );
					
					} 
					catch ( UIWarningException uwe ) 
					{
						ExceptionProcessor.GetInstance().Process(uwe);
						TxtIndustrialSafetyBriefingOn.Focus();
					}
				}
			}
		}

		private void TxtSafetyAtWorkServiceBriefingOn_Leave(object sender, System.EventArgs e)
		{
			if (this.TxtSafetyAtWorkServiceBriefingOn.Text.Length > 0 )
			{
				if ( ! StringValidation.GetInstance().IsDateString (this.TxtSafetyAtWorkServiceBriefingOn.Text.Trim()) ) 
				{
					try 
					{
						throw new UIWarningException(MessageSingleton.GetInstance().
							GetMessage(MessageSingleton.INVALID_FULLDATE) );
					} 
					catch ( UIWarningException uwe ) 
					{
						ExceptionProcessor.GetInstance().Process(uwe);
						TxtSafetyAtWorkServiceBriefingOn.Focus();
					}
				}
			}
		}

		private void TxtSiteSecurityBriefingOn_Leave(object sender, System.EventArgs e)
		{
			if (this.TxtSiteSecurityBriefingOn.Text.Length > 0 )
			{
				if ( ! StringValidation.GetInstance().IsDateString (this.TxtSiteSecurityBriefingOn.Text.Trim()) ) 
				{
					try 
					{
						throw new UIWarningException(MessageSingleton.GetInstance().
							GetMessage(MessageSingleton.INVALID_FULLDATE) );
					
					} 
					catch ( UIWarningException uwe ) 
					{
						ExceptionProcessor.GetInstance().Process(uwe);
						TxtSiteSecurityBriefingOn.Focus();
					}
				}
			}
		}

        private void TxtIdSmActOn_Leave(object sender, System.EventArgs e)
		{
            if (this.TxtIdSmActOn.Text.Length > 0)
			{
                if (!StringValidation.GetInstance().IsDateString(this.TxtIdSmActOn.Text.Trim())) 
				{
					try 
					{
						throw new UIWarningException(MessageSingleton.GetInstance().
							GetMessage(MessageSingleton.INVALID_FULLDATE) );
					} 
					catch ( UIWarningException uwe ) 
					{
						ExceptionProcessor.GetInstance().Process(uwe);
                        TxtIdSmActOn.Focus();
					}
				}
			}
		}
        

		private void TxtIdHitagOn_Leave(object sender, System.EventArgs e)
		{
			if (this.TxtIdHitagOn.Text.Length > 0 )
			{
				if ( ! StringValidation.GetInstance().IsDateString (this.TxtIdHitagOn.Text.Trim()) ) 
				{
					try 
					{
						throw new UIWarningException(MessageSingleton.GetInstance().
							GetMessage(MessageSingleton.INVALID_FULLDATE) );
					} 
					catch ( UIWarningException uwe ) 
					{
						ExceptionProcessor.GetInstance().Process(uwe);
						TxtIdHitagOn.Focus();
					}
				}
			}
		}

		private void TxtBreathingApparatusBriefingg262On_Leave(object sender, System.EventArgs e)
		{
			if (this.TxtBreathingApparatusBriefingg262On.Text.Length > 0 )
			{
				if ( ! StringValidation.GetInstance().IsDateString (this.TxtBreathingApparatusBriefingg262On.Text.Trim()) ) 
				{
					try 
					{
						throw new UIWarningException(MessageSingleton.GetInstance().
							GetMessage(MessageSingleton.INVALID_FULLDATE) );
					
					} 
					catch ( UIWarningException uwe ) 
					{
						ExceptionProcessor.GetInstance().Process(uwe);
						TxtBreathingApparatusBriefingg262On.Focus();
					}
				}
			}
		}

		private void TxtBreathingApparatusBriefingG263On_Leave(object sender, System.EventArgs e)
		{
			if (this.TxtBreathingApparatusBriefingG263On.Text.Length > 0 )
			{
				if ( ! StringValidation.GetInstance().IsDateString (this.TxtBreathingApparatusBriefingG263On.Text.Trim()) ) 
				{
					try 
					{
						throw new UIWarningException(MessageSingleton.GetInstance().
							GetMessage(MessageSingleton.INVALID_FULLDATE) );
					
					} 
					catch ( UIWarningException uwe ) 
					{
						ExceptionProcessor.GetInstance().Process(uwe);
						TxtBreathingApparatusBriefingG263On.Focus();
					}
				}
			}
		}

		private void TxtPalletLifterOn_Leave(object sender, System.EventArgs e)
		{
			if (this.TxtPalletLifterOn.Text.Length > 0 )
			{
				if ( ! StringValidation.GetInstance().IsDateString (this.TxtPalletLifterOn.Text.Trim()) ) 
				{
					try 
					{
						throw new UIWarningException(MessageSingleton.GetInstance().
							GetMessage(MessageSingleton.INVALID_FULLDATE) );
					
					} 
					catch ( UIWarningException uwe ) 
					{
						ExceptionProcessor.GetInstance().Process(uwe);
						TxtPalletLifterOn.Focus();
					}
				}
			}
		}

		private void TxtRaisablePlatformOn_Leave(object sender, System.EventArgs e)
		{
			if ( this.TxtRaisablePlatformOn.Text.Length > 0 )
			{
				if ( ! StringValidation.GetInstance().IsDateString (this.TxtRaisablePlatformOn.Text.Trim()) ) 
				{
					try 
					{
						throw new UIWarningException(MessageSingleton.GetInstance().
							GetMessage(MessageSingleton.INVALID_FULLDATE) );
					} 
					catch ( UIWarningException uwe ) 
					{
						ExceptionProcessor.GetInstance().Process(uwe);
						TxtRaisablePlatformOn.Focus();
					}
				}
			}
		}

		private void TxtCranesOn_Leave(object sender, System.EventArgs e)
		{
			if ( this.TxtCranesOn.Text.Length > 0 )
			{
				if ( ! StringValidation.GetInstance().IsDateString (this.TxtCranesOn.Text.Trim()) ) 
				{
					try 
					{
						throw new UIWarningException(MessageSingleton.GetInstance().
							GetMessage(MessageSingleton.INVALID_FULLDATE) );
					} 
					catch ( UIWarningException uwe ) 
					{
						ExceptionProcessor.GetInstance().Process(uwe);
						TxtCranesOn.Focus();
					}
				}
			}
		}

		private void TxtBriefingPlantOn_Leave(object sender, System.EventArgs e)
		{
			if (this.TxtBriefingPlantOn.Text.Length > 0 )
			{
				if ( ! StringValidation.GetInstance().IsDateString (this.TxtBriefingPlantOn.Text.Trim()) ) 
				{
					try 
					{
						throw new UIWarningException(MessageSingleton.GetInstance().
							GetMessage(MessageSingleton.INVALID_FULLDATE) );
					} 
					catch ( UIWarningException uwe ) 
					{
						ExceptionProcessor.GetInstance().Process(uwe);
						TxtBriefingPlantOn.Focus();
					}
				}
			}
		}

		private void TxtRespiratoryMaskOn_Leave(object sender, System.EventArgs e)
		{
			if ( this.TxtRespiratoryMaskOn.Text.Length > 0 )
			{
				if ( ! StringValidation.GetInstance().IsDateString (this.TxtRespiratoryMaskOn.Text.Trim()) ) 
				{
					try 
					{
						throw new UIWarningException(MessageSingleton.GetInstance().
							GetMessage(MessageSingleton.INVALID_FULLDATE) );
					
					} 
					catch ( UIWarningException uwe ) 
					{
						ExceptionProcessor.GetInstance().Process(uwe);
						TxtRespiratoryMaskOn.Focus();
					}
				}
			}
		}

		private void TxtVehicleShortOn_Leave(object sender, System.EventArgs e)
		{
			if ( this.TxtVehicleShortOn.Text.Length > 0 )
			{
				if ( ! StringValidation.GetInstance().IsDateString (this.TxtVehicleShortOn.Text.Trim()) ) 
				{
					try 
					{
						throw new UIWarningException(MessageSingleton.GetInstance().
							GetMessage(MessageSingleton.INVALID_FULLDATE) );
					
					} 
					catch ( UIWarningException uwe ) 
					{
						ExceptionProcessor.GetInstance().Process(uwe);
						TxtVehicleShortOn.Focus();
					}
				}
			}
		}

		private void TxtVehicleLongOn_Leave(object sender, System.EventArgs e)
		{
			if ( this.TxtVehicleLongOn.Text.Length > 0 )
			{
				if ( ! StringValidation.GetInstance().IsDateString (this.TxtVehicleLongOn.Text.Trim()) ) 
				{
					try 
					{
						throw new UIWarningException(MessageSingleton.GetInstance().
							GetMessage(MessageSingleton.INVALID_FULLDATE) );
					
					} 
					catch ( UIWarningException uwe ) 
					{
						ExceptionProcessor.GetInstance().Process(uwe);
						TxtVehicleLongOn.Focus();
					}
				}
			}
		}

		/// <summary>
		/// Fired when "Gültig von" is left.
		/// Check that user has entered a valid date, if not, then warning
		/// If field empty after editing then no warning
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TxtValidFrom_Leave(object sender, System.EventArgs e)
		{
			mCorrectFormat = true;
			if ( this.TxtValidFrom.Text.Length > 0 ) 
			{
				if ( ! StringValidation.GetInstance().IsDateString (this.TxtValidFrom.Text.Trim()) ) 
				{
					try 
					{
						throw new UIWarningException(MessageSingleton.GetInstance().
							GetMessage(MessageSingleton.INVALID_FULLDATE) );
					} 
					catch ( UIWarningException uwe ) 
					{
						ExceptionProcessor.GetInstance().Process(uwe);
						TxtValidFrom.Focus();
						mCorrectFormat = false;
					}
				}
				if (TxtValidUntil.Text.Trim().Length > 0 && mCorrectFormat == true)
				{
					if (!StringValidation.GetInstance().IsDateValid(TxtValidFrom.Text.Trim(), TxtValidUntil.Text.Trim()))
					{
						try 
						{
							throw new UIWarningException(MessageSingleton.GetInstance().
								GetMessage(MessageSingleton.DYNDATA_FROMTILLDATE) );
						} 
						catch ( UIWarningException uwe ) 
						{
							ExceptionProcessor.GetInstance().Process(uwe);
							TxtValidUntil.Focus();
							TxtValidUntil.Text = TxtValidFrom.Text.Trim();
						}
					}
				}
			}
		}

		/// <summary>
		/// Fired when "Gültig bis" is left.
		/// Check that user has entered a valid date, if not, then warning
		/// If field empty after editing then no warning
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TxtValidUntil_Leave(object sender, System.EventArgs e)
		{
			mCorrectFormat = true;
			if ( this.TxtValidUntil.Text.Length > 0 )
			{
				if ( ! StringValidation.GetInstance().IsDateString (this.TxtValidUntil.Text.Trim()) ) 
				{
					try 
					{
						throw new UIWarningException(MessageSingleton.GetInstance().
							GetMessage(MessageSingleton.INVALID_FULLDATE) );
						
					} 
					catch ( UIWarningException uwe ) 
					{
						ExceptionProcessor.GetInstance().Process(uwe);
						TxtValidUntil.Focus();
						mCorrectFormat = false;
					}
				}
				if (TxtValidFrom.Text.Trim().Length > 0 && mCorrectFormat == true)
				{
					if (!StringValidation.GetInstance().IsDateValid(TxtValidFrom.Text.Trim(), TxtValidUntil.Text.Trim()))
					{
						try 
						{
							throw new UIWarningException(MessageSingleton.GetInstance().
								GetMessage(MessageSingleton.DYNDATA_FROMTILLDATE) );
						} 
						catch ( UIWarningException uwe ) 
						{
							ExceptionProcessor.GetInstance().Process(uwe);
							TxtValidUntil.Focus();
						}
					}
				}
			}
		}

		/// <summary>
		/// Check in GUI that user has entered a valid date, if not, then warning
		/// If field empty after editing then no warning
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TxtCheckInOn_Leave(object sender, System.EventArgs e)
		{
			if ( this.TxtCheckInOn.Text.Length > 0 )
			{
				if ( ! StringValidation.GetInstance().IsDateString (this.TxtCheckInOn.Text.Trim()) ) 
				{
					try 
					{
						throw new UIWarningException(MessageSingleton.GetInstance().
							GetMessage(MessageSingleton.INVALID_FULLDATE) );
						
					} 
					catch ( UIWarningException uwe ) 
					{
						ExceptionProcessor.GetInstance().Process(uwe);
						TxtCheckInOn.Focus();
					}
				}
			}
		}

		/// <summary>
		/// Check in GUI that user has entered a valid date, if not, then warning
		/// If field empty after editing then no warning
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TxtCheckOffOn_Leave(object sender, System.EventArgs e)
		{
			if ( this.TxtCheckOffOn.Text.Length > 0 )
			{
				if ( ! StringValidation.GetInstance().IsDateString (this.TxtCheckOffOn.Text.Trim()) ) 
				{
					try 
					{
						throw new UIWarningException(MessageSingleton.GetInstance().
							GetMessage(MessageSingleton.INVALID_FULLDATE) );
						
					} 
					catch ( UIWarningException uwe ) 
					{
						ExceptionProcessor.GetInstance().Process(uwe);
						TxtCheckOffOn.Focus();
					}
				}
			}
		}

		/// <summary>
		/// Check in GUI that user has entered a valid date, if not, then warning
		/// If field empty after editing then no warning
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TxtDeliveryDate_Leave(object sender, System.EventArgs e)
		{
			if ( this.TxtDeliveryDate.Text.Length > 0 )
			{
				if ( ! StringValidation.GetInstance().IsDateString (this.TxtDeliveryDate.Text.Trim()) ) 
				{
					try 
					{
						throw new UIWarningException(MessageSingleton.GetInstance().
							GetMessage(MessageSingleton.INVALID_FULLDATE) );
						
					} 
					catch ( UIWarningException uwe ) 
					{
						ExceptionProcessor.GetInstance().Process(uwe);
						TxtDeliveryDate.Focus();
					}
				}		
			}	
		}

		#endregion // End of LeaveEvents	
		
		#endregion Events
	}
}


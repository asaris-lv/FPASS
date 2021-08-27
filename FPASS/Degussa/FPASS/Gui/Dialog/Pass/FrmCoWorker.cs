using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;

using System.Diagnostics;
using Microsoft.Win32;
using System.Text.RegularExpressions;

using Degussa.FPASS.Gui;
using Degussa.FPASS.Db;
using Degussa.FPASS.Db.DataSets;
using Degussa.FPASS.FPASSApplication;
using Degussa.FPASS.Util.Exceptions;
using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.Util.Validation;
using Degussa.FPASS.Util.UserManagement;
using Degussa.FPASS.Util.ListOfValues;
using de.pta.Component.Errorhandling;
using de.pta.Component.DbAccess;
using de.pta.Component.DbAccess.Exceptions;
using de.pta.Component.ListOfValues;
using de.pta.Component.Common;
using Degussa.FPASS.Util.Enums;
using Degussa.FPASS.Util;
using Degussa.FPASS.Gui.Dialog.ActiveDirectory;
using Evonik.FPASSLdap;

namespace Degussa.FPASS.Gui.Dialog.Pass
{
	/// <summary>
	/// FrmCoWorker is the view of the MVC-triad CoWorkerModel,
	/// CoWorkerController and FrmCoWorker.
	/// FrmCoWorker extends from the FPASSBaseView.
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
    public class FrmCoWorker : FPASSBaseView
    {
        #region Members

        //panels
        internal System.Windows.Forms.Panel PnlCoWorkerData;
        internal System.Windows.Forms.Panel PnlTabReception;
        internal System.Windows.Forms.Panel PnlTabCoordinator;
        internal System.Windows.Forms.Panel PnlTabSafetyWork;
        internal System.Windows.Forms.Panel PnlTabTechnical;
        internal System.Windows.Forms.Panel PnlTabSiteFireService;
        internal System.Windows.Forms.Panel PnlTabSiteMedical;
        internal System.Windows.Forms.Panel PnlTabPlant;
        internal System.Windows.Forms.Panel PnlTabSiteSecure;
        internal System.Windows.Forms.Panel PnlCoVehicleEntrance;
        internal System.Windows.Forms.Panel PnlCoVehicleEntranceLong;
        internal System.Windows.Forms.Panel PnlCoCheckInCheckOff;
        internal System.Windows.Forms.Panel PnlCoVehicleEntranceShort;
        internal System.Windows.Forms.Panel PnlCoDepartmentalBriefing;
        internal System.Windows.Forms.Panel PnlCoData;
        internal System.Windows.Forms.Panel PnlCoPrecautionaryMedical;
        internal System.Windows.Forms.Panel PnlCoRespiratoryMaskBriefing;
        internal System.Windows.Forms.Panel PnlCoIndustrialSafetyBriefingSite;
        internal System.Windows.Forms.Panel PnlCoBriefing;
        internal System.Windows.Forms.Panel PnlCoSiteSecurityBriefing;
        internal System.Windows.Forms.Panel PnlCoRaisablePlattform;
        internal System.Windows.Forms.Panel PnlCoCranes;
        internal System.Windows.Forms.Panel PnlCoPalletLifter;
        internal System.Windows.Forms.Panel PnlCoPhotoCard;
        internal System.Windows.Forms.Panel PnlCoSafetyPass;
        internal System.Windows.Forms.Panel PnlPlIndustrialSafetyBriefingPlant;
        internal System.Windows.Forms.Panel PnlReSafetyInstructions;
        internal System.Windows.Forms.Panel PnlReAccessAuthorization;
        internal System.Windows.Forms.Panel PnlReBasics;
        internal System.Windows.Forms.Panel PnlReBriefing;
        internal System.Windows.Forms.Panel PnlSiSeVehicleEntranceLong;
        internal System.Windows.Forms.Panel PnlSiSeVehicleEntranceLongControl;
        internal System.Windows.Forms.Panel PnlSiSeVehicleEntranceShort;
        internal System.Windows.Forms.Panel PnlSiSeVehicleEntranceShortControl;
        internal System.Windows.Forms.Label PnlSiSeVehicleEntranceShortReceived;
        internal System.Windows.Forms.Panel PnlSiSeAccess;
        internal System.Windows.Forms.Panel PnlSiSeSiteSecurity;
        internal System.Windows.Forms.Panel PnlTecBriefingSite;
        internal System.Windows.Forms.Panel PnlSaAtWoPalletLifterBriefing;
        internal System.Windows.Forms.Panel PnlSaAtWoCranesBriefing;
        internal System.Windows.Forms.Panel PnlSaAtWoIndustrialSafetyBriefing;
        internal System.Windows.Forms.Panel PnlSiFiMaskBriefing;
        internal System.Windows.Forms.Panel PnlSaAtWoSiteSecurityBriefing;
        internal System.Windows.Forms.Panel PnlReIdentityCard;
        internal System.Windows.Forms.Panel PnlSiSeIdentityCard;
        internal System.Windows.Forms.Panel PnlCoBreathingApparatusG26_3;
        internal System.Windows.Forms.Panel PnlCoBreathingApparatusG26_2;
        internal System.Windows.Forms.Panel PnlSiFiSiteSecurityBriefingG26_2;
        internal System.Windows.Forms.Panel PnlSiFiSiteSecurityBriefingG26_3;
        private System.Windows.Forms.Panel PnlSiSeVehicleNumber;

        //labels
        internal System.Windows.Forms.Label LblExternalContractor;
        internal System.Windows.Forms.Label LblSubcontractor;
        internal System.Windows.Forms.Label LblSupervisor;
        internal System.Windows.Forms.Label LblCoordinator;
        internal System.Windows.Forms.Label LblFirstname;
        internal System.Windows.Forms.Label LblCoWorkerData;
        internal System.Windows.Forms.Label LblDeliveryDate;
        internal System.Windows.Forms.Label LblPassValidUntil;
        internal System.Windows.Forms.Label LblPassValidFrom;
        internal System.Windows.Forms.Label LblMask;
        internal System.Windows.Forms.Label LblSurname;
        internal System.Windows.Forms.Label LblCoVehicleEntranceLong;
        internal System.Windows.Forms.Label LblCoVehicleEntranceShort;
        internal System.Windows.Forms.Label LblCoOrderDone;
        internal System.Windows.Forms.Label LblCoCheckOff;
        internal System.Windows.Forms.Label LblCoCheckIn;
        internal System.Windows.Forms.Label LblCoDepartmentalBriefing;
        internal System.Windows.Forms.Label LblCoDepartment;
        internal System.Windows.Forms.Label LblCoPlant;
        internal System.Windows.Forms.Label LblCoTelephoneNumber;
        internal System.Windows.Forms.Label LblCoCraft;
        internal System.Windows.Forms.Label LblCoOrderNumber;
        internal System.Windows.Forms.Label LblCoSupervisor;
        internal System.Windows.Forms.Label LblCoSubcontractor;
        internal System.Windows.Forms.Label LblReDateOfBirth;
        internal System.Windows.Forms.Label LblReVehicleRegistrationNumber;
        internal System.Windows.Forms.Label LblReAccessAuthorizationOn;
        internal System.Windows.Forms.Label LblReAccessAuthorizationBy;
        internal System.Windows.Forms.Label LblReSafetyInstructions;
        internal System.Windows.Forms.Label LblReAccessAuthorization;
        internal System.Windows.Forms.Label LblReSafetyInstructionsOn;
        internal System.Windows.Forms.Label LblReSafetyInstructionsBy;
        internal System.Windows.Forms.Label LblSaAtWoCranesBriefing;
        internal System.Windows.Forms.Label LblSaAtWoPalletLifterBriefing;
        internal System.Windows.Forms.Label LblSaAtWoCranesBriefingDoneBy;
        internal System.Windows.Forms.Label LblSaAtWoCranesBriefingDoneOn;
        internal System.Windows.Forms.Label LblSaAtWoCranesBriefingDone;
        internal System.Windows.Forms.Label LblSaAtWoPalletLifterBriefingDoneBy;
        internal System.Windows.Forms.Label LblSaAtWoPalletLifterBriefingDoneOn;
        internal System.Windows.Forms.Label LblSaAtWoPalletLifterBriefingDone;
        internal System.Windows.Forms.Label LblSaAtWoSafetyAtWorkBriefing;
        internal System.Windows.Forms.Label LblSaAtWoSafetyAtWorkBriefingDoneBy;
        internal System.Windows.Forms.Label LblSaAtWoSafetyAtWorkBriefingDoneOn;
        internal System.Windows.Forms.Label LblSaAtWoSafetyAtWorkBriefingDone;
        internal System.Windows.Forms.Label LblCoPrecautionaryMedical;
        internal System.Windows.Forms.Label LblCoRespiratoryMaskBriefing;
        internal System.Windows.Forms.Label LblCoIndustrialSafetyBriefingSiteBy;
        internal System.Windows.Forms.Label LblCoIndustrialSafetyBriefingSite;
        internal System.Windows.Forms.Label LblCoSiteSecurityBriefing;
        internal System.Windows.Forms.Label LblCoPalletLifter;
        internal System.Windows.Forms.Label LblCoRaisablePlattform;
        internal System.Windows.Forms.Label LblCoCranes;
        internal System.Windows.Forms.Label LblCoIdCardPhotoSmAct;
        internal System.Windows.Forms.Label LblCoSafetyPass;
        internal System.Windows.Forms.Label LblPlBriefing;
        internal System.Windows.Forms.Label LblPlBriefingDoneBy;
        internal System.Windows.Forms.Label LblPlBriefingDoneOn;
        internal System.Windows.Forms.Label LblPlBriefingDone;
        internal System.Windows.Forms.Label LblReExternalContractor;
        internal System.Windows.Forms.Label LblReCoordinator;
        internal System.Windows.Forms.Label LblReSurname;
        internal System.Windows.Forms.Label LblReFirstname;
        internal System.Windows.Forms.Label LblRePlaceOfBirth;
        internal System.Windows.Forms.Label LblSiFiRespiratoryMaskBriefing;
        internal System.Windows.Forms.Label LblSiFiRespiratoryMaskBriefingDoneOn;
        internal System.Windows.Forms.Label LblSiFiRespMaskBriefRecBy;
        internal System.Windows.Forms.Label LblSiFiRespiratoryMaskBriefingDone;
        internal System.Windows.Forms.Label LblSiMedExecutedBy;
        internal System.Windows.Forms.Label LblSiMedPrecautionaryMedicalBriefing;
        internal System.Windows.Forms.Label LblSiMedValidUntil;
        internal System.Windows.Forms.Label LblSiMedPrecautionaryMedical;
        internal System.Windows.Forms.Label LblSiSeIdPhotoSmAct;
        internal System.Windows.Forms.Label LblSiSeIdPhotoSmActRecBy;
        internal System.Windows.Forms.Label LblSiSeIdPhotoSmActRec;
        internal System.Windows.Forms.Label LblSiSeVehicleEntranceLongReceivedBy;
        internal System.Windows.Forms.Label LblSiSeVehicleEntranceLongReceivedOn;
        internal System.Windows.Forms.Label LblSiSeVehicleEntranceLong;
        internal System.Windows.Forms.Label LblSiSeVehicleEntranceLongReceived;
        internal System.Windows.Forms.Label LblSiSeVehicleEntranceShortReceivedBy;
        internal System.Windows.Forms.Label LblSiSeVehicleEntranceShortReceivedOn;
        internal System.Windows.Forms.Label LblSiSeVehicleEntranceShort;
        internal System.Windows.Forms.Label LblSiSeSiteBriOrd;
        internal System.Windows.Forms.Label LblSiSeSiteSecBriRecBy;
        internal System.Windows.Forms.Label LblSiSeSiteSecBriRec;
        internal System.Windows.Forms.Label LblSiSeSiteSecBriRecOn;
        internal System.Windows.Forms.Label LblSiSeAccessComment;
        internal System.Windows.Forms.Label LblTecBriefing;
        internal System.Windows.Forms.Label LblTecBriefingDoneBy;
        internal System.Windows.Forms.Label LblTecBriefingDoneOn;
        internal System.Windows.Forms.Label LblTecBriefingDone;
        internal System.Windows.Forms.Label LblCoBreathingApparatusG26_3;
        internal System.Windows.Forms.Label LblCoBreathingApparatusG26_2;
        internal System.Windows.Forms.Label LblSiFiSiteSecurityBriefingG26_2;
        internal System.Windows.Forms.Label LblSiFiSiteSecurityBriefingDoneG26_2;
        internal System.Windows.Forms.Label LblSiFiSiteSecurityBriefingDoneByG26_2;
        internal System.Windows.Forms.Label LblSiFiSiteSecurityBriefingDoneOnG26_2;
        internal System.Windows.Forms.Label LblSiFiSiteSecurityBriefingG26_3;
        internal System.Windows.Forms.Label LblSiFiSiteSecurityBriefingDoneG26_3;
        internal System.Windows.Forms.Label LblSiFiSiteSecurityBriefingDoneByG26_3;
        internal System.Windows.Forms.Label LblSiFiSiteSecurityBriefingDoneOnG26_3;
        internal System.Windows.Forms.Label LblReHitag2Reader;
        internal System.Windows.Forms.Label LblSiSeIdentityCardRecievedOn;
        internal System.Windows.Forms.Label LblSiSeVehicleRegistrationNumber;
        internal System.Windows.Forms.Label label1;

        //textboxes
        internal System.Windows.Forms.TextBox TxtCoordinator;
        internal System.Windows.Forms.TextBox TxtSubcontractor;
        internal System.Windows.Forms.TextBox TxtExternalContractor;
        internal System.Windows.Forms.TextBox TxtSupervisor;
        internal System.Windows.Forms.TextBox TxtFirstname;
        internal System.Windows.Forms.TextBox TxtPassValidUntil;
        internal System.Windows.Forms.TextBox TxtSurname;
        internal System.Windows.Forms.TextBox TxtCoTelephoneNumber;
        internal System.Windows.Forms.TextBox TxtCoOrderNumber;
        internal System.Windows.Forms.TextBox TxtRePlaceOfBirth;
        internal System.Windows.Forms.TextBox TxtReSurname;
        internal System.Windows.Forms.TextBox TxtReFirstname;
        internal System.Windows.Forms.TextBox TxtReDateOfBirth;
        internal System.Windows.Forms.TextBox TxtReVehicleRegistrationNumber;
        internal System.Windows.Forms.TextBox TxtReAccessAuthorizationBy;
        internal System.Windows.Forms.TextBox TxtReSafetyInstructionsBy;
        internal System.Windows.Forms.TextBox TxtSaAtWoCranesBriefingDoneBy;
        internal System.Windows.Forms.TextBox TxtSaAtWoPalletLifterBriefingDoneBy;
        internal System.Windows.Forms.TextBox TxtSaAtWoSafetyAtWorkBriefingDoneBy;
        internal System.Windows.Forms.TextBox TxtPlPlantname;
        internal System.Windows.Forms.TextBox TxtReVehicleRegistrationNumberFour;
        internal System.Windows.Forms.TextBox TxtReVehicleRegistrationNumberThree;
        internal System.Windows.Forms.TextBox TxtReVehicleRegistrationNumberTwo;
        internal TextBox TxtReIDReaderHitag2;
        internal System.Windows.Forms.TextBox TxtSiFiRespMaskBriefRecBy;
        internal System.Windows.Forms.TextBox TxtSiMedExecutedBy;
        internal System.Windows.Forms.TextBox TxtSiSeIdPhotoSmActRecBy;
        internal System.Windows.Forms.TextBox TxtSiSeVehicleEntranceShortReceivedBy;
        internal System.Windows.Forms.TextBox TxtSiSeVehicleEntranceLongReceivedBy;
        internal System.Windows.Forms.TextBox TxtSiSeAccessAuthorizationComment;
        internal System.Windows.Forms.TextBox TxtSiSeSiteSecBriRecBy;
        internal System.Windows.Forms.TextBox TxtTecBriefingDoneBy;
        internal System.Windows.Forms.TextBox TxtCoIndustrialSafetyBriefingSiteBy;
        internal System.Windows.Forms.TextBox TxtPlBriefingDoneBy;
        internal System.Windows.Forms.TextBox TxtSiFiSiteSecurityBriefingDoneByG26_2;
        internal System.Windows.Forms.TextBox TxtSiFiSiteSecurityBriefingDoneByG26_3;
        internal System.Windows.Forms.TextBox TxtCoSupervisor;
        internal System.Windows.Forms.TextBox TxtCoCheckIn;
        internal System.Windows.Forms.TextBox TxtCoCheckOff;
        internal System.Windows.Forms.TextBox TxtSiSeVehicleRegistrationNumberTwo;
        internal System.Windows.Forms.TextBox TxtSiSeVehicleRegistrationNumberThree;
        internal System.Windows.Forms.TextBox TxtSiSeVehicleRegistrationNumberFour;
        internal System.Windows.Forms.TextBox TxtSiSeVehicleRegistrationNumber;

        //comboboxes
        internal System.Windows.Forms.ComboBox CboCoCraftName;
        internal System.Windows.Forms.ComboBox CboCoCraftNumber;
        internal System.Windows.Forms.ComboBox CboCoDepartment;
        internal System.Windows.Forms.ComboBox CboCoSubcontractor;
        internal System.Windows.Forms.ComboBox CobReCoordinator;
        internal System.Windows.Forms.ComboBox CobReExternalContractor;
        internal System.Windows.Forms.ComboBox CboSiMedPrecautionaryMedical;

        //checkedlistboxes
        internal System.Windows.Forms.CheckedListBox LiKCoPlant;

        //checkboxes
        internal System.Windows.Forms.CheckBox CbxSaAtWoCranesBriefingDone;
        internal System.Windows.Forms.CheckBox CbxSaAtWoPalletLifterBriefingDone;
        internal System.Windows.Forms.CheckBox CbxSaAtWoSafetyAtWorkBriefingDone;
        internal System.Windows.Forms.CheckBox CbxSiFiRespMaskBriefRec;
        internal System.Windows.Forms.CheckBox CbxSiMedPrecautionaryMedical;
        internal System.Windows.Forms.CheckBox CbxSiSeIdPhotoSmActRec;
        internal System.Windows.Forms.CheckBox CbxCoIndSafetyBrfRecvd;
        internal System.Windows.Forms.CheckBox CbxPlBriefingDone;
        internal System.Windows.Forms.CheckBox CbxSiSeSiteSecBriRec;
        internal System.Windows.Forms.CheckBox CbxTecRaisonalPlattform;
        internal System.Windows.Forms.CheckBox CbxSiFiSiteSecurityBriefingDoneG26_2;
        internal System.Windows.Forms.CheckBox CbxSiFiSiteSecurityBriefingDoneG26_3;

        //datetimepickers
        internal System.Windows.Forms.DateTimePicker DatReAccessAuthorizationOn;
        internal System.Windows.Forms.DateTimePicker DatReSafetyInstructionsOn;
        internal System.Windows.Forms.DateTimePicker DatSaAtWoCranesBriefingDoneOn;
        internal System.Windows.Forms.DateTimePicker DatSaAtWoPalletLifterBriefingDoneOn;
        internal System.Windows.Forms.DateTimePicker DatSaAtWoSafetyAtWorkBriefingDone;
        internal System.Windows.Forms.DateTimePicker DatCoIndustrialSafetyBriefingSiteOn;
        internal System.Windows.Forms.DateTimePicker DatPlBriefingDoneOn;
        internal System.Windows.Forms.DateTimePicker DatSiFiRespMaskBriefRecOn;
        internal System.Windows.Forms.DateTimePicker DatSiMedValidUntil;
        internal System.Windows.Forms.DateTimePicker DatSiSeIdPhotoSmActRec;
        internal System.Windows.Forms.DateTimePicker DatSiSeVehicleEntranceLongReceivedOn;
        internal System.Windows.Forms.DateTimePicker DatSiSeVehicleEntranceShortReceivedOn;
        internal System.Windows.Forms.DateTimePicker DatSiSeSiteSecBriRec;
        internal System.Windows.Forms.DateTimePicker DatTecBriefingDoneOn;
        internal System.Windows.Forms.DateTimePicker DatSiFiSiteSecurityBriefingDoneOnG26_2;
        internal System.Windows.Forms.DateTimePicker DatSiFiSiteSecurityBriefingDoneOnG26_3;
        internal System.Windows.Forms.DateTimePicker DatPassValidFrom;
        internal System.Windows.Forms.DateTimePicker DatDeliveryDate;

        //radiobuttons
        internal System.Windows.Forms.RadioButton RbtCoVehicleEntranceLongNo;
        internal System.Windows.Forms.RadioButton RbtCoVehicleEntranceLongYes;
        internal System.Windows.Forms.RadioButton RbtCoVehicleEntranceShortNo;
        internal System.Windows.Forms.RadioButton RbtCoVehicleEntranceShortYes;
        internal System.Windows.Forms.RadioButton RbtCoOrderDoneNo;
        internal System.Windows.Forms.RadioButton RbtCoOrderDoneYes;
        internal System.Windows.Forms.RadioButton RbtCoDepartmentalBriefingYes;
        internal System.Windows.Forms.RadioButton RbtCoDepartmentalBriefingNo;
        internal System.Windows.Forms.RadioButton RbtCoPrecautionaryMedicalNo;
        internal System.Windows.Forms.RadioButton RbtCoPrecautionaryMedicalYes;
        internal System.Windows.Forms.RadioButton RbtCoRespiratoryMaskBriefingNo;
        internal System.Windows.Forms.RadioButton RbtCoRespiratoryMaskBriefingYes;
        internal System.Windows.Forms.RadioButton RbtCoSiteSecurityBriefingNo;
        internal System.Windows.Forms.RadioButton RbtCoSiteSecurityBriefingYes;
        internal System.Windows.Forms.RadioButton RbtCoCranesNo;
        internal System.Windows.Forms.RadioButton RbtCoCranesYes;
        internal System.Windows.Forms.RadioButton RbtCoRaisablePlattformNo;
        internal System.Windows.Forms.RadioButton RbtCoRaisablePlattformYes;
        internal System.Windows.Forms.RadioButton RbtCoPalletLifterNo;
        internal System.Windows.Forms.RadioButton RbtCoPalletLifterYes;
        internal System.Windows.Forms.RadioButton RbtSiSeIdPhotoSmAct;
        internal System.Windows.Forms.RadioButton RbtSiSeVehicleEntranceLongNo;
        internal System.Windows.Forms.RadioButton RbtSiSeVehicleEntranceLongYes;
        internal System.Windows.Forms.RadioButton RbtSiSeVehicleEntranceShortReceivedNo;
        internal System.Windows.Forms.RadioButton RbtSiSeVehicleEntranceShortReceivedYes;
        internal System.Windows.Forms.RadioButton RbtSiSeVehicleEntranceLong;
        internal System.Windows.Forms.RadioButton RbtSiSeVehicleEntranceShort;
        internal System.Windows.Forms.RadioButton RbtSiSeSiteSecBri;
        internal System.Windows.Forms.RadioButton RbtTecBriefing;
        internal System.Windows.Forms.RadioButton RbtCoIdPhotoSmActNo;
        internal System.Windows.Forms.RadioButton RbtCoIdPhotoSmActYes;
        internal System.Windows.Forms.RadioButton RbtCoSafetyPassNo;
        internal System.Windows.Forms.RadioButton RbtCoSafetyPassYes;
        internal System.Windows.Forms.RadioButton RbtReSafetyInstructionsNo;
        internal System.Windows.Forms.RadioButton RbtReSafetyInstructionsYes;
        internal System.Windows.Forms.RadioButton RbtReAccessAuthNo;
        internal System.Windows.Forms.RadioButton RbtReAccessAuthYes;
        internal System.Windows.Forms.RadioButton RbtPlBriefing;
        internal System.Windows.Forms.RadioButton RbtSaAtWoPalletLifterBriefing;
        internal System.Windows.Forms.RadioButton RbtSaAtWoCranesBriefing;
        internal System.Windows.Forms.RadioButton RbtSaAtWoBriefing;
        internal System.Windows.Forms.RadioButton RbtSiFiRespMaskBriefDir;
        internal System.Windows.Forms.RadioButton RbtSiMedPrecautionaryMedicalBriefing;
        internal System.Windows.Forms.RadioButton RbtCoBreathingApparatusNoG26_3;
        internal System.Windows.Forms.RadioButton RbtCoBreathingApparatusYesG26_3;
        internal System.Windows.Forms.RadioButton RbtCoBreathingApparatusNoG26_2;
        internal System.Windows.Forms.RadioButton RbtCoBreathingApparatusYesG26_2;
        internal System.Windows.Forms.RadioButton RbtSiFiSiteSecurityBriefingG26_2;
        internal System.Windows.Forms.RadioButton RbtSiFiSiteSecurityBriefingG26_3;

        //tables
        internal System.Windows.Forms.DataGrid DgrSiMedPrecautionaryMedical;
        internal System.Windows.Forms.DataGrid DgrPlPlant;
        private System.Windows.Forms.DataGridTableStyle DgrTableStylePrecMed;
        private System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxValidUntil;
        private System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxExecutedOn;
        private System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxType;
        private System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxDescription;
        private System.Windows.Forms.DataGridTextBoxColumn DgrTextExecutedBy;
        private System.Windows.Forms.DataGridTableStyle DGrTableStylePlant;
        private System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxPK;
        private System.Windows.Forms.DataGridTextBoxColumn DgrTxtPKPlant;
        private System.Windows.Forms.DataGridTextBoxColumn DgrTxtPlantName;
        private System.Windows.Forms.DataGridTextBoxColumn DgrTxtPlDoneOn;
        private System.Windows.Forms.DataGridTextBoxColumn DgrTxtUserName;
        private System.Windows.Forms.DataGridTextBoxColumn DgrTxtPlValidUntil;
        private System.Windows.Forms.DataGridTextBoxColumn DgrTxtStatus;
        private System.Windows.Forms.DataGridTextBoxColumn DgrTxtDirected;
        private System.Windows.Forms.DataGridTextBoxColumn DgrTextStatus;

        //tabpages
        internal System.Windows.Forms.TabControl TabSiteSecurity;
        internal System.Windows.Forms.TabPage TapReception;
        internal System.Windows.Forms.TabPage TapCoordinator;
        internal System.Windows.Forms.TabPage TapSafetyAtWork;
        internal System.Windows.Forms.TabPage TapSiteMedicalService;
        internal System.Windows.Forms.TabPage TapSiteFireService;
        internal System.Windows.Forms.TabPage TapPlant;
        internal System.Windows.Forms.TabPage TapTechnicalDepartment;
        internal System.Windows.Forms.TabPage TapSiteSecurity;

        //buttons
        internal System.Windows.Forms.Button BtnSave;
        internal System.Windows.Forms.Button BtnSummary;
        internal System.Windows.Forms.Button BtnPass;
        internal System.Windows.Forms.Button BtnCoSearchExternalContractor;
        internal System.Windows.Forms.Button BtnReSearchExternalContractor;
        internal System.Windows.Forms.Button BtnReClear;
        internal System.Windows.Forms.Button BtnRePassNrHitag;

        //tooltips
        private System.Windows.Forms.ToolTip TooPass;
        private System.Windows.Forms.ToolTip TooSave;
        private System.Windows.Forms.ToolTip TooBackToSummary;
        private System.Windows.Forms.ToolTip TooClearMask;
        private System.Windows.Forms.ToolTip TooIdentityCard;
        private System.Windows.Forms.ToolTip TooExContractor;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        /// 
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// holds current datetime
        /// </summary>
        private string now = System.DateTime.Now.ToString();

        /// <summary>
        /// flag indicating if combobox coordinator is leading and 
        /// determines content of combobox excontractor
        /// </summary>
        private bool mCboCoordinatorIsLeading;

        /// <summary>
        /// flag indicating if combobox contractor is leading and 
        /// determines content of combobox coordinator
        /// </summary>
        private bool mCboContractorIsLeading;

        /// <summary>
        /// used to check if textfield CheckOffDate is in correct format
        /// </summary>
        private bool mCheckOffDateOK = true;

        /// <summary>
        /// used to check if textfield BirthDate is in correct format
        /// </summary>
        private bool mBirthDateOK = true;

        /// <summary>
        /// holds current datetime
        /// </summary>
        private DateTime mDateNow = System.DateTime.Now;

        /// <summary>
        /// States which tab FrmSearchExcontractor was called from 
        /// </summary>
        private int mCalledSearchContractor;
        /// <summary>
        /// Signals that popup for Id card reader search was called
        /// </summary>
        private int mCalledSearchIdReader;

        // Authorisatons (roles) current user has
        internal bool mReceptionAuthorization;
        internal bool mCoordinatorAuthorization;
        internal bool mPlantAuthorization;
        internal bool mSafetyAtWorkAuthorization;
        internal bool mSiteFireAuthorization;
        internal bool mMedicalServiceAuthorization;
        internal bool mTechDepartmentAuthorization;
        internal bool mSiteSecurityAuthorization;
        internal bool mSiteSecurityLeaderAuthorization;
        internal bool mSiteSecurityVehicleAuthorization;
        internal bool mEdvAdminAuthorization;
        internal bool mSystemAdminAuthorization;
        internal bool mIdCardNumberEditAuthorization;
        internal bool mApprentEditAuthorization;

        /// <summary>
        /// Which id card reader: Hitag or Mifare?
        /// </summary>
        public string mIDCardReaderType;

        /// <summary>
        /// used to hold if tab in front should be shown automatically dependet by roles
        /// of curren user
        /// </summary>
        internal bool mAutomaticTabOrder;

        /// <summary>
        /// flag indicating if plant listbox was clicked by user
        /// </summary>
        private bool mEnteredLikCoPlant;

        /// <summary>
        /// flag indicating if combobox precautionary medical was clicked by user
        /// </summary>
        private bool mEnteredCboPreMedType;

        /// <summary>
        /// Flag indicating if radio button "Kfz Zutritt kurz genehmigt" was cleicked by user
        /// </summary>
        private bool mEnteredRbtSiSeVehShortReceivedYes;


        internal System.Windows.Forms.Button btnDelPrecMed;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Panel PnlCoFireman;
        internal System.Windows.Forms.RadioButton RbtCoFiremanNo;
        internal System.Windows.Forms.RadioButton RbtCoFiremanYes;
        internal System.Windows.Forms.Label LblCoFireman;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label LblSiFiFiremanDoneOn;
        internal System.Windows.Forms.CheckBox CbxSiFiFireman;
        internal System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.RadioButton RbtSiFiFireman;
        internal System.Windows.Forms.Label LblSiFiFireman;
        internal System.Windows.Forms.Label label6;
        internal System.Windows.Forms.DateTimePicker DatSiFiFiremanDoneOn;
        internal System.Windows.Forms.Label LblSiFiFiremanDoneBy;
        internal System.Windows.Forms.TextBox TxtSiFiFiremanDoneBy;
        internal System.Windows.Forms.Button BtnCoPdf;
        private System.Windows.Forms.ToolTip TooPdf;
        internal System.Windows.Forms.Button btnCoordHist;
        internal System.Windows.Forms.Button BtnDelPassNumber;
        private System.Windows.Forms.Panel PnlSiSePExternal;
        internal System.Windows.Forms.DateTimePicker DatSiSePExternalOn;
        internal System.Windows.Forms.Label LblSiSePExternalBy;
        internal System.Windows.Forms.TextBox TxtSiSePExternalBy;
        internal System.Windows.Forms.Label LblSiSePExternalOn;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.CheckBox CbxSiSePExternal;
        internal DateTimePicker DatPlBriefingValidUntil;
        internal Label LblPlBriefingValidUntil;

        /// <summary>
        /// Flags whether current coordinator is allowed to edit
        /// </summary>
        private bool mCoordinatorEditLocked = false;
        /// <summary>
        /// Has ID card number been changed manually?
        /// </summary>
        private bool mIdCardEdited = false;
        internal Label LblSiFiRespMaskBriefDirBy;
        internal TextBox TxtSiFiRespMaskBriefDirBy;
        internal Label label5;
        internal DateTimePicker DatSiFiRespMaskBriefDirOn;
        private Label lblCoPlantsAll;
        internal Button BtnCoPlantsAll;
        internal Button BtnRePassNrHitagUSB;
        internal Button BtnReHitag2Reader;
        internal Label LblReHitag2Nr;
        internal TextBox TxtReIDCardNumHitag2;
        internal Button BtnRePassNrMifareUSB;
        internal Button BtnReMifareReader;
        internal Label LblReMifareNr;
        internal Button BtnRePassNrMifare;
        internal TextBox TxtReIDCardNumMifareNo;
        internal TextBox TxtReIDReaderMifare;
        internal Label LblReMifareReader;
        internal Label LblReDelPassNr;
        internal Label lblReIDCardReaderHint;
        internal Label lblReIDCardHint;
        internal Panel PnlReAccessApprent;
        internal RadioButton RbtReAccessApprentNo;
        internal RadioButton RbtReAccessApprentYes;
        internal Label LblReAccessApprent;
        internal DateTimePicker DatReAccessApprent;
        internal Label LblReAccessApprentTo;
        private GroupBox GrpReBrDummy;
        private Panel PnlIdCardReader;
        internal Label lblReIDCardHd;
        internal Label label7;
        internal Label LblReAccessApprentBy;
        internal TextBox TxtReAccessApprentBy;
        private Panel PnlCoSmartAct;
        private Label LblCoADSSearch;
        internal Button BtnCoADSSearch;
        private Label LblCoPKI;
        internal TextBox TxtCoFpassNo;
        internal CheckBox CbxCoPKI;
        internal TextBox TxtCoSmartActNo;
        internal Label LblCoFpassNo;
        internal Label LblCoSmartActNo;
        internal Label LblCoWindowsID;
        internal TextBox TxtCoWindowsID;
        internal Label LblCoPhone;
        internal TextBox TxtCoPhone;
        internal Label LblReTitleBase;
        internal Label lblReTitleAccess;
        private GroupBox grpRe2;
        internal Label lblReSaveIdRdr;
        internal Button btnreSaveIdRdr;
        private GroupBox grpRe1;
        private Label LblCoSmartActExp;
        internal Button BtnCoSmartActExp;
        internal Label LblZKSRetCode;
        internal TextBox TxtZKSRetCode;
        internal Label LblCoDataTitle;
        internal Label LblCoBriefTitle;
        internal Label LblCoIdPhotoTitle;
        internal Label lblSiteSecBriTitle;
        internal Label LblSiSeIdPhotoTitle;
        internal CheckBox CbxSiSeIdPhotoHitagRec;
        internal DateTimePicker DatSiSeIdPhotoHitagRec;
        internal Label LblSiSeIdPhotoHitagRecBy;
        internal Label LblSiSeIdPhotoHitagRec;
        internal TextBox TxtSiSeIdPhotoHitagRecBy;
        internal Label LblSiSeIdPhotoSmActRecOn;
        private GroupBox GrpSiSe1;
        internal Label LblSiSeIdPhotoHitagRecHint;
        private GroupBox GpSaAtWoDummy;
        internal Button BtnSiSeAccessRevoke;
        internal Label LblSiSeAccessHint;
        internal Label LblSiSeAccessTitle;
        internal Label LblSiSeAccess;
        internal Panel PnlSiSeAccessYNRbt;
        internal RadioButton RbtSiSeAccessAuthNo;
        internal RadioButton RbtSiSeAccessAuthYes;
        private GroupBox GpSiSe2;
        internal Label LblSiSeParkTitle;
        internal Button BtnReSafetyRevoke;
        internal Label LblPlZKSImport;
        internal CheckBox ChkPlZKSImport;
        private DataGridTextBoxColumn DgrTxtSource;
        internal Panel PnlSiFiAllBriefings;
        internal Panel PnlSiFiMaskLent;
        private GroupBox GrpSiFi1;
        internal Label LblSiFiMaskLentTitle;
        internal Label LblSiFiMaskTitleFlo;
        internal Button BtnSiFiMaskTicket;
        internal Label LblSiFiMaskBackFlo;
        internal Label LblSiFiMaskLentFlo;
        internal TextBox TxtSiFiMaskNrBackFlo;
        internal TextBox TxtSiFiMaskNrLentFlo;
        internal Label LblSiFiMaskNrBackFlo;
        internal Label LblSiFiMaskNrLentFlo;
        internal DateTimePicker DatSiFiMaskBackOnFlo;
        internal DateTimePicker DatSiFiMaskLentOnFlo;
        internal Label LblSiFiMaskBackByFlo;
        internal TextBox TxtSiFiMaskBackByFlo;
        internal Label LblSiFiMaskLentByFlo;
        internal TextBox TxtSiFiMaskLentByFlo;
        internal Label LblSiFiMaskBackOnFlo;
        internal Label LblSiFiMaskLentOnFlo;
        internal Label LblSiFiMaskLentTec;
        internal TextBox TxtSiFiMaskNrBackTec;
        internal TextBox TxtSiFiMaskNrLentTec;
        internal Label LblSiFiMaskNrBackTec;
        internal Label LblSiFiMaskNrLentTec;
        internal DateTimePicker DatSiFiMaskBackOnTec;
        internal DateTimePicker DatSiFiMaskLentOnTec;
        internal Label LblSiFiMaskBackByTec;
        internal TextBox TxtSiFiMaskBackByTec;
        internal Label LblSiFiMaskLentByTec;
        internal TextBox TxtSiFiMaskLentByTec;
        internal Label LblSiFiMaskBackOnTec;
        internal Label LblSiFiMaskLentOnTec;
        internal Label LblSiFiMaskBackTec;
        internal TextBox TxtSiFiMaskMaintDateTec;
        internal Label LblSiFiMaskMaintDateTec;
        private GroupBox groupBox1;
        internal PictureBox CoWorkerPhotoBox;
        private GroupBox GrpCoBrDummy;
        private Label label8;
        internal Button BtnCoSmartActDel;

        private bool mIdCardFieldEntered = false;

        #endregion Members

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public FrmCoWorker()
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();

            InitView();

            FillLists();

            DateTimePicker();

            // Load the current user's authorizations
            ReceptionAuthorization();
            SafetyAtWorkAuthorization();
            CoordinatorAuthorization();
            SiteFireAuthorization();
            SiteSecurityAuthorization();
            SiteSecurityLeaderAuthorization();
            MedicalServiceAuthorization();
            PlantAuthorization();
            TechDepartmentAuthorization();
            SystemAdminAuthorization();
            EdvAdminAuthorization();
            IdCardNumberEditAuthorization();
            ApprenticeEditAuthorization();
            ClearFieldsSiteFireService();
        }


        #endregion Constructors

        #region Designer generated code

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCoWorker));
            this.PnlCoWorkerData = new System.Windows.Forms.Panel();
            this.TxtCoordinator = new System.Windows.Forms.TextBox();
            this.TxtSubcontractor = new System.Windows.Forms.TextBox();
            this.TxtExternalContractor = new System.Windows.Forms.TextBox();
            this.TxtSupervisor = new System.Windows.Forms.TextBox();
            this.TxtFirstname = new System.Windows.Forms.TextBox();
            this.TxtSurname = new System.Windows.Forms.TextBox();
            this.LblExternalContractor = new System.Windows.Forms.Label();
            this.LblSurname = new System.Windows.Forms.Label();
            this.LblSubcontractor = new System.Windows.Forms.Label();
            this.LblSupervisor = new System.Windows.Forms.Label();
            this.LblCoordinator = new System.Windows.Forms.Label();
            this.LblFirstname = new System.Windows.Forms.Label();
            this.TabSiteSecurity = new System.Windows.Forms.TabControl();
            this.TapReception = new System.Windows.Forms.TabPage();
            this.PnlTabReception = new System.Windows.Forms.Panel();
            this.PnlIdCardReader = new System.Windows.Forms.Panel();
            this.grpRe2 = new System.Windows.Forms.GroupBox();
            this.BtnReHitag2Reader = new System.Windows.Forms.Button();
            this.btnreSaveIdRdr = new System.Windows.Forms.Button();
            this.lblReSaveIdRdr = new System.Windows.Forms.Label();
            this.lblReIDCardReaderHint = new System.Windows.Forms.Label();
            this.BtnReMifareReader = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.TxtReIDReaderMifare = new System.Windows.Forms.TextBox();
            this.LblReHitag2Reader = new System.Windows.Forms.Label();
            this.LblReMifareReader = new System.Windows.Forms.Label();
            this.TxtReIDReaderHitag2 = new System.Windows.Forms.TextBox();
            this.PnlReBasics = new System.Windows.Forms.Panel();
            this.CoWorkerPhotoBox = new System.Windows.Forms.PictureBox();
            this.LblReTitleBase = new System.Windows.Forms.Label();
            this.btnCoordHist = new System.Windows.Forms.Button();
            this.BtnReSearchExternalContractor = new System.Windows.Forms.Button();
            this.TxtReVehicleRegistrationNumberFour = new System.Windows.Forms.TextBox();
            this.TxtReVehicleRegistrationNumberThree = new System.Windows.Forms.TextBox();
            this.TxtReVehicleRegistrationNumberTwo = new System.Windows.Forms.TextBox();
            this.LblRePlaceOfBirth = new System.Windows.Forms.Label();
            this.TxtRePlaceOfBirth = new System.Windows.Forms.TextBox();
            this.CobReCoordinator = new System.Windows.Forms.ComboBox();
            this.CobReExternalContractor = new System.Windows.Forms.ComboBox();
            this.LblReExternalContractor = new System.Windows.Forms.Label();
            this.LblReCoordinator = new System.Windows.Forms.Label();
            this.LblReSurname = new System.Windows.Forms.Label();
            this.LblReFirstname = new System.Windows.Forms.Label();
            this.TxtReSurname = new System.Windows.Forms.TextBox();
            this.TxtReFirstname = new System.Windows.Forms.TextBox();
            this.LblReDateOfBirth = new System.Windows.Forms.Label();
            this.TxtReDateOfBirth = new System.Windows.Forms.TextBox();
            this.LblReVehicleRegistrationNumber = new System.Windows.Forms.Label();
            this.TxtReVehicleRegistrationNumber = new System.Windows.Forms.TextBox();
            this.PnlReBriefing = new System.Windows.Forms.Panel();
            this.BtnReSafetyRevoke = new System.Windows.Forms.Button();
            this.lblReTitleAccess = new System.Windows.Forms.Label();
            this.LblReAccessApprentBy = new System.Windows.Forms.Label();
            this.TxtReAccessApprentBy = new System.Windows.Forms.TextBox();
            this.GrpReBrDummy = new System.Windows.Forms.GroupBox();
            this.PnlReAccessApprent = new System.Windows.Forms.Panel();
            this.RbtReAccessApprentNo = new System.Windows.Forms.RadioButton();
            this.RbtReAccessApprentYes = new System.Windows.Forms.RadioButton();
            this.LblReAccessApprentTo = new System.Windows.Forms.Label();
            this.DatReAccessApprent = new System.Windows.Forms.DateTimePicker();
            this.LblReAccessApprent = new System.Windows.Forms.Label();
            this.LblReAccessAuthorizationBy = new System.Windows.Forms.Label();
            this.TxtReAccessAuthorizationBy = new System.Windows.Forms.TextBox();
            this.LblReSafetyInstructions = new System.Windows.Forms.Label();
            this.PnlReSafetyInstructions = new System.Windows.Forms.Panel();
            this.RbtReSafetyInstructionsNo = new System.Windows.Forms.RadioButton();
            this.RbtReSafetyInstructionsYes = new System.Windows.Forms.RadioButton();
            this.LblReSafetyInstructionsOn = new System.Windows.Forms.Label();
            this.DatReSafetyInstructionsOn = new System.Windows.Forms.DateTimePicker();
            this.LblReAccessAuthorization = new System.Windows.Forms.Label();
            this.PnlReAccessAuthorization = new System.Windows.Forms.Panel();
            this.RbtReAccessAuthNo = new System.Windows.Forms.RadioButton();
            this.RbtReAccessAuthYes = new System.Windows.Forms.RadioButton();
            this.LblReAccessAuthorizationOn = new System.Windows.Forms.Label();
            this.DatReAccessAuthorizationOn = new System.Windows.Forms.DateTimePicker();
            this.LblReSafetyInstructionsBy = new System.Windows.Forms.Label();
            this.TxtReSafetyInstructionsBy = new System.Windows.Forms.TextBox();
            this.PnlReIdentityCard = new System.Windows.Forms.Panel();
            this.grpRe1 = new System.Windows.Forms.GroupBox();
            this.BtnDelPassNumber = new System.Windows.Forms.Button();
            this.lblReIDCardHd = new System.Windows.Forms.Label();
            this.LblReDelPassNr = new System.Windows.Forms.Label();
            this.lblReIDCardHint = new System.Windows.Forms.Label();
            this.BtnRePassNrMifareUSB = new System.Windows.Forms.Button();
            this.LblReHitag2Nr = new System.Windows.Forms.Label();
            this.BtnRePassNrHitagUSB = new System.Windows.Forms.Button();
            this.TxtReIDCardNumHitag2 = new System.Windows.Forms.TextBox();
            this.LblReMifareNr = new System.Windows.Forms.Label();
            this.BtnRePassNrHitag = new System.Windows.Forms.Button();
            this.BtnRePassNrMifare = new System.Windows.Forms.Button();
            this.TxtReIDCardNumMifareNo = new System.Windows.Forms.TextBox();
            this.BtnReClear = new System.Windows.Forms.Button();
            this.TapCoordinator = new System.Windows.Forms.TabPage();
            this.PnlTabCoordinator = new System.Windows.Forms.Panel();
            this.PnlCoSmartAct = new System.Windows.Forms.Panel();
            this.GrpCoBrDummy = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.BtnCoSmartActDel = new System.Windows.Forms.Button();
            this.LblCoIdPhotoTitle = new System.Windows.Forms.Label();
            this.LblCoSmartActExp = new System.Windows.Forms.Label();
            this.BtnCoSmartActExp = new System.Windows.Forms.Button();
            this.PnlCoPhotoCard = new System.Windows.Forms.Panel();
            this.RbtCoIdPhotoSmActNo = new System.Windows.Forms.RadioButton();
            this.RbtCoIdPhotoSmActYes = new System.Windows.Forms.RadioButton();
            this.LblCoIdCardPhotoSmAct = new System.Windows.Forms.Label();
            this.LblCoADSSearch = new System.Windows.Forms.Label();
            this.BtnCoADSSearch = new System.Windows.Forms.Button();
            this.LblCoPKI = new System.Windows.Forms.Label();
            this.TxtCoFpassNo = new System.Windows.Forms.TextBox();
            this.CbxCoPKI = new System.Windows.Forms.CheckBox();
            this.TxtCoSmartActNo = new System.Windows.Forms.TextBox();
            this.LblCoFpassNo = new System.Windows.Forms.Label();
            this.LblCoSmartActNo = new System.Windows.Forms.Label();
            this.LblCoWindowsID = new System.Windows.Forms.Label();
            this.TxtCoWindowsID = new System.Windows.Forms.TextBox();
            this.LblCoPhone = new System.Windows.Forms.Label();
            this.TxtCoPhone = new System.Windows.Forms.TextBox();
            this.PnlCoVehicleEntrance = new System.Windows.Forms.Panel();
            this.PnlCoVehicleEntranceLong = new System.Windows.Forms.Panel();
            this.RbtCoVehicleEntranceLongNo = new System.Windows.Forms.RadioButton();
            this.RbtCoVehicleEntranceLongYes = new System.Windows.Forms.RadioButton();
            this.PnlCoVehicleEntranceShort = new System.Windows.Forms.Panel();
            this.RbtCoVehicleEntranceShortNo = new System.Windows.Forms.RadioButton();
            this.RbtCoVehicleEntranceShortYes = new System.Windows.Forms.RadioButton();
            this.LblCoVehicleEntranceLong = new System.Windows.Forms.Label();
            this.LblCoVehicleEntranceShort = new System.Windows.Forms.Label();
            this.PnlCoCheckInCheckOff = new System.Windows.Forms.Panel();
            this.TxtCoCheckOff = new System.Windows.Forms.TextBox();
            this.TxtCoCheckIn = new System.Windows.Forms.TextBox();
            this.RbtCoOrderDoneNo = new System.Windows.Forms.RadioButton();
            this.RbtCoOrderDoneYes = new System.Windows.Forms.RadioButton();
            this.LblCoOrderDone = new System.Windows.Forms.Label();
            this.LblCoCheckOff = new System.Windows.Forms.Label();
            this.LblCoCheckIn = new System.Windows.Forms.Label();
            this.PnlCoDepartmentalBriefing = new System.Windows.Forms.Panel();
            this.LblCoDepartmentalBriefing = new System.Windows.Forms.Label();
            this.RbtCoDepartmentalBriefingYes = new System.Windows.Forms.RadioButton();
            this.RbtCoDepartmentalBriefingNo = new System.Windows.Forms.RadioButton();
            this.PnlCoData = new System.Windows.Forms.Panel();
            this.LblCoDataTitle = new System.Windows.Forms.Label();
            this.lblCoPlantsAll = new System.Windows.Forms.Label();
            this.BtnCoPlantsAll = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtCoSupervisor = new System.Windows.Forms.TextBox();
            this.BtnCoSearchExternalContractor = new System.Windows.Forms.Button();
            this.CboCoCraftName = new System.Windows.Forms.ComboBox();
            this.LiKCoPlant = new System.Windows.Forms.CheckedListBox();
            this.CboCoCraftNumber = new System.Windows.Forms.ComboBox();
            this.LblCoDepartment = new System.Windows.Forms.Label();
            this.LblCoPlant = new System.Windows.Forms.Label();
            this.LblCoTelephoneNumber = new System.Windows.Forms.Label();
            this.LblCoCraft = new System.Windows.Forms.Label();
            this.LblCoOrderNumber = new System.Windows.Forms.Label();
            this.LblCoSupervisor = new System.Windows.Forms.Label();
            this.LblCoSubcontractor = new System.Windows.Forms.Label();
            this.CboCoDepartment = new System.Windows.Forms.ComboBox();
            this.TxtCoTelephoneNumber = new System.Windows.Forms.TextBox();
            this.TxtCoOrderNumber = new System.Windows.Forms.TextBox();
            this.CboCoSubcontractor = new System.Windows.Forms.ComboBox();
            this.PnlCoPrecautionaryMedical = new System.Windows.Forms.Panel();
            this.RbtCoPrecautionaryMedicalNo = new System.Windows.Forms.RadioButton();
            this.RbtCoPrecautionaryMedicalYes = new System.Windows.Forms.RadioButton();
            this.LblCoPrecautionaryMedical = new System.Windows.Forms.Label();
            this.PnlCoRespiratoryMaskBriefing = new System.Windows.Forms.Panel();
            this.RbtCoRespiratoryMaskBriefingNo = new System.Windows.Forms.RadioButton();
            this.RbtCoRespiratoryMaskBriefingYes = new System.Windows.Forms.RadioButton();
            this.LblCoRespiratoryMaskBriefing = new System.Windows.Forms.Label();
            this.PnlCoIndustrialSafetyBriefingSite = new System.Windows.Forms.Panel();
            this.BtnCoPdf = new System.Windows.Forms.Button();
            this.DatCoIndustrialSafetyBriefingSiteOn = new System.Windows.Forms.DateTimePicker();
            this.LblCoIndustrialSafetyBriefingSite = new System.Windows.Forms.Label();
            this.CbxCoIndSafetyBrfRecvd = new System.Windows.Forms.CheckBox();
            this.TxtCoIndustrialSafetyBriefingSiteBy = new System.Windows.Forms.TextBox();
            this.LblCoIndustrialSafetyBriefingSiteBy = new System.Windows.Forms.Label();
            this.PnlCoBriefing = new System.Windows.Forms.Panel();
            this.LblCoBriefTitle = new System.Windows.Forms.Label();
            this.PnlCoFireman = new System.Windows.Forms.Panel();
            this.RbtCoFiremanNo = new System.Windows.Forms.RadioButton();
            this.RbtCoFiremanYes = new System.Windows.Forms.RadioButton();
            this.LblCoFireman = new System.Windows.Forms.Label();
            this.PnlCoBreathingApparatusG26_3 = new System.Windows.Forms.Panel();
            this.RbtCoBreathingApparatusNoG26_3 = new System.Windows.Forms.RadioButton();
            this.RbtCoBreathingApparatusYesG26_3 = new System.Windows.Forms.RadioButton();
            this.LblCoBreathingApparatusG26_3 = new System.Windows.Forms.Label();
            this.PnlCoSiteSecurityBriefing = new System.Windows.Forms.Panel();
            this.LblCoSiteSecurityBriefing = new System.Windows.Forms.Label();
            this.RbtCoSiteSecurityBriefingNo = new System.Windows.Forms.RadioButton();
            this.RbtCoSiteSecurityBriefingYes = new System.Windows.Forms.RadioButton();
            this.PnlCoCranes = new System.Windows.Forms.Panel();
            this.RbtCoCranesNo = new System.Windows.Forms.RadioButton();
            this.RbtCoCranesYes = new System.Windows.Forms.RadioButton();
            this.LblCoCranes = new System.Windows.Forms.Label();
            this.PnlCoRaisablePlattform = new System.Windows.Forms.Panel();
            this.RbtCoRaisablePlattformNo = new System.Windows.Forms.RadioButton();
            this.RbtCoRaisablePlattformYes = new System.Windows.Forms.RadioButton();
            this.LblCoRaisablePlattform = new System.Windows.Forms.Label();
            this.PnlCoPalletLifter = new System.Windows.Forms.Panel();
            this.RbtCoPalletLifterNo = new System.Windows.Forms.RadioButton();
            this.RbtCoPalletLifterYes = new System.Windows.Forms.RadioButton();
            this.LblCoPalletLifter = new System.Windows.Forms.Label();
            this.PnlCoBreathingApparatusG26_2 = new System.Windows.Forms.Panel();
            this.RbtCoBreathingApparatusNoG26_2 = new System.Windows.Forms.RadioButton();
            this.RbtCoBreathingApparatusYesG26_2 = new System.Windows.Forms.RadioButton();
            this.LblCoBreathingApparatusG26_2 = new System.Windows.Forms.Label();
            this.PnlCoSafetyPass = new System.Windows.Forms.Panel();
            this.RbtCoSafetyPassNo = new System.Windows.Forms.RadioButton();
            this.RbtCoSafetyPassYes = new System.Windows.Forms.RadioButton();
            this.LblCoSafetyPass = new System.Windows.Forms.Label();
            this.TapSiteFireService = new System.Windows.Forms.TabPage();
            this.PnlTabSiteFireService = new System.Windows.Forms.Panel();
            this.PnlSiFiMaskLent = new System.Windows.Forms.Panel();
            this.LblSiFiMaskMaintDateTec = new System.Windows.Forms.Label();
            this.LblSiFiMaskBackTec = new System.Windows.Forms.Label();
            this.TxtSiFiMaskMaintDateTec = new System.Windows.Forms.TextBox();
            this.LblSiFiMaskLentTec = new System.Windows.Forms.Label();
            this.TxtSiFiMaskNrBackTec = new System.Windows.Forms.TextBox();
            this.TxtSiFiMaskNrLentTec = new System.Windows.Forms.TextBox();
            this.LblSiFiMaskNrBackTec = new System.Windows.Forms.Label();
            this.LblSiFiMaskNrLentTec = new System.Windows.Forms.Label();
            this.DatSiFiMaskBackOnTec = new System.Windows.Forms.DateTimePicker();
            this.DatSiFiMaskLentOnTec = new System.Windows.Forms.DateTimePicker();
            this.LblSiFiMaskBackByTec = new System.Windows.Forms.Label();
            this.TxtSiFiMaskBackByTec = new System.Windows.Forms.TextBox();
            this.LblSiFiMaskLentByTec = new System.Windows.Forms.Label();
            this.TxtSiFiMaskLentByTec = new System.Windows.Forms.TextBox();
            this.LblSiFiMaskBackOnTec = new System.Windows.Forms.Label();
            this.LblSiFiMaskLentOnTec = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.GrpSiFi1 = new System.Windows.Forms.GroupBox();
            this.LblSiFiMaskLentTitle = new System.Windows.Forms.Label();
            this.LblSiFiMaskTitleFlo = new System.Windows.Forms.Label();
            this.BtnSiFiMaskTicket = new System.Windows.Forms.Button();
            this.LblSiFiMaskBackFlo = new System.Windows.Forms.Label();
            this.LblSiFiMaskLentFlo = new System.Windows.Forms.Label();
            this.TxtSiFiMaskNrBackFlo = new System.Windows.Forms.TextBox();
            this.TxtSiFiMaskNrLentFlo = new System.Windows.Forms.TextBox();
            this.LblSiFiMaskNrBackFlo = new System.Windows.Forms.Label();
            this.LblSiFiMaskNrLentFlo = new System.Windows.Forms.Label();
            this.DatSiFiMaskBackOnFlo = new System.Windows.Forms.DateTimePicker();
            this.DatSiFiMaskLentOnFlo = new System.Windows.Forms.DateTimePicker();
            this.LblSiFiMaskBackByFlo = new System.Windows.Forms.Label();
            this.TxtSiFiMaskBackByFlo = new System.Windows.Forms.TextBox();
            this.LblSiFiMaskLentByFlo = new System.Windows.Forms.Label();
            this.TxtSiFiMaskLentByFlo = new System.Windows.Forms.TextBox();
            this.LblSiFiMaskBackOnFlo = new System.Windows.Forms.Label();
            this.LblSiFiMaskLentOnFlo = new System.Windows.Forms.Label();
            this.PnlSiFiAllBriefings = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtSiFiSiteSecurityBriefingDoneByG26_2 = new System.Windows.Forms.TextBox();
            this.LblSiFiSiteSecurityBriefingDoneByG26_2 = new System.Windows.Forms.Label();
            this.LblSiFiSiteSecurityBriefingDoneG26_2 = new System.Windows.Forms.Label();
            this.DatSiFiSiteSecurityBriefingDoneOnG26_2 = new System.Windows.Forms.DateTimePicker();
            this.PnlSiFiSiteSecurityBriefingG26_2 = new System.Windows.Forms.Panel();
            this.RbtSiFiSiteSecurityBriefingG26_2 = new System.Windows.Forms.RadioButton();
            this.LblSiFiSiteSecurityBriefingG26_2 = new System.Windows.Forms.Label();
            this.CbxSiFiSiteSecurityBriefingDoneG26_2 = new System.Windows.Forms.CheckBox();
            this.LblSiFiSiteSecurityBriefingDoneOnG26_3 = new System.Windows.Forms.Label();
            this.LblSiFiFiremanDoneOn = new System.Windows.Forms.Label();
            this.TxtSiFiSiteSecurityBriefingDoneByG26_3 = new System.Windows.Forms.TextBox();
            this.CbxSiFiFireman = new System.Windows.Forms.CheckBox();
            this.LblSiFiSiteSecurityBriefingDoneByG26_3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.RbtSiFiFireman = new System.Windows.Forms.RadioButton();
            this.LblSiFiFireman = new System.Windows.Forms.Label();
            this.LblSiFiSiteSecurityBriefingDoneG26_3 = new System.Windows.Forms.Label();
            this.DatSiFiFiremanDoneOn = new System.Windows.Forms.DateTimePicker();
            this.DatSiFiSiteSecurityBriefingDoneOnG26_3 = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.PnlSiFiSiteSecurityBriefingG26_3 = new System.Windows.Forms.Panel();
            this.RbtSiFiSiteSecurityBriefingG26_3 = new System.Windows.Forms.RadioButton();
            this.LblSiFiSiteSecurityBriefingG26_3 = new System.Windows.Forms.Label();
            this.LblSiFiFiremanDoneBy = new System.Windows.Forms.Label();
            this.CbxSiFiSiteSecurityBriefingDoneG26_3 = new System.Windows.Forms.CheckBox();
            this.TxtSiFiFiremanDoneBy = new System.Windows.Forms.TextBox();
            this.LblSiFiSiteSecurityBriefingDoneOnG26_2 = new System.Windows.Forms.Label();
            this.PnlSiFiMaskBriefing = new System.Windows.Forms.Panel();
            this.LblSiFiRespMaskBriefDirBy = new System.Windows.Forms.Label();
            this.TxtSiFiRespMaskBriefDirBy = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.DatSiFiRespMaskBriefDirOn = new System.Windows.Forms.DateTimePicker();
            this.CbxSiFiRespMaskBriefRec = new System.Windows.Forms.CheckBox();
            this.DatSiFiRespMaskBriefRecOn = new System.Windows.Forms.DateTimePicker();
            this.LblSiFiRespiratoryMaskBriefing = new System.Windows.Forms.Label();
            this.LblSiFiRespiratoryMaskBriefingDoneOn = new System.Windows.Forms.Label();
            this.LblSiFiRespMaskBriefRecBy = new System.Windows.Forms.Label();
            this.TxtSiFiRespMaskBriefRecBy = new System.Windows.Forms.TextBox();
            this.LblSiFiRespiratoryMaskBriefingDone = new System.Windows.Forms.Label();
            this.RbtSiFiRespMaskBriefDir = new System.Windows.Forms.RadioButton();
            this.TapSiteMedicalService = new System.Windows.Forms.TabPage();
            this.PnlTabSiteMedical = new System.Windows.Forms.Panel();
            this.LblSiMedPrecautionaryMedical = new System.Windows.Forms.Label();
            this.btnDelPrecMed = new System.Windows.Forms.Button();
            this.CbxSiMedPrecautionaryMedical = new System.Windows.Forms.CheckBox();
            this.DatSiMedValidUntil = new System.Windows.Forms.DateTimePicker();
            this.LblSiMedExecutedBy = new System.Windows.Forms.Label();
            this.TxtSiMedExecutedBy = new System.Windows.Forms.TextBox();
            this.LblSiMedPrecautionaryMedicalBriefing = new System.Windows.Forms.Label();
            this.LblSiMedValidUntil = new System.Windows.Forms.Label();
            this.CboSiMedPrecautionaryMedical = new System.Windows.Forms.ComboBox();
            this.DgrSiMedPrecautionaryMedical = new System.Windows.Forms.DataGrid();
            this.DgrTableStylePrecMed = new System.Windows.Forms.DataGridTableStyle();
            this.DgrTextBoxPK = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxType = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxDescription = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxValidUntil = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxExecutedOn = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextExecutedBy = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextStatus = new System.Windows.Forms.DataGridTextBoxColumn();
            this.RbtSiMedPrecautionaryMedicalBriefing = new System.Windows.Forms.RadioButton();
            this.TapSafetyAtWork = new System.Windows.Forms.TabPage();
            this.PnlTabSafetyWork = new System.Windows.Forms.Panel();
            this.PnlSaAtWoSiteSecurityBriefing = new System.Windows.Forms.Panel();
            this.GpSaAtWoDummy = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CbxSaAtWoCranesBriefingDone = new System.Windows.Forms.CheckBox();
            this.CbxSaAtWoPalletLifterBriefingDone = new System.Windows.Forms.CheckBox();
            this.CbxSaAtWoSafetyAtWorkBriefingDone = new System.Windows.Forms.CheckBox();
            this.DatSaAtWoCranesBriefingDoneOn = new System.Windows.Forms.DateTimePicker();
            this.DatSaAtWoPalletLifterBriefingDoneOn = new System.Windows.Forms.DateTimePicker();
            this.DatSaAtWoSafetyAtWorkBriefingDone = new System.Windows.Forms.DateTimePicker();
            this.LblSaAtWoCranesBriefing = new System.Windows.Forms.Label();
            this.LblSaAtWoPalletLifterBriefing = new System.Windows.Forms.Label();
            this.LblSaAtWoCranesBriefingDoneBy = new System.Windows.Forms.Label();
            this.LblSaAtWoCranesBriefingDoneOn = new System.Windows.Forms.Label();
            this.LblSaAtWoCranesBriefingDone = new System.Windows.Forms.Label();
            this.TxtSaAtWoCranesBriefingDoneBy = new System.Windows.Forms.TextBox();
            this.LblSaAtWoPalletLifterBriefingDoneBy = new System.Windows.Forms.Label();
            this.LblSaAtWoPalletLifterBriefingDoneOn = new System.Windows.Forms.Label();
            this.LblSaAtWoPalletLifterBriefingDone = new System.Windows.Forms.Label();
            this.TxtSaAtWoPalletLifterBriefingDoneBy = new System.Windows.Forms.TextBox();
            this.LblSaAtWoSafetyAtWorkBriefing = new System.Windows.Forms.Label();
            this.LblSaAtWoSafetyAtWorkBriefingDoneBy = new System.Windows.Forms.Label();
            this.LblSaAtWoSafetyAtWorkBriefingDoneOn = new System.Windows.Forms.Label();
            this.TxtSaAtWoSafetyAtWorkBriefingDoneBy = new System.Windows.Forms.TextBox();
            this.LblSaAtWoSafetyAtWorkBriefingDone = new System.Windows.Forms.Label();
            this.PnlSaAtWoPalletLifterBriefing = new System.Windows.Forms.Panel();
            this.RbtSaAtWoPalletLifterBriefing = new System.Windows.Forms.RadioButton();
            this.PnlSaAtWoCranesBriefing = new System.Windows.Forms.Panel();
            this.RbtSaAtWoCranesBriefing = new System.Windows.Forms.RadioButton();
            this.PnlSaAtWoIndustrialSafetyBriefing = new System.Windows.Forms.Panel();
            this.RbtSaAtWoBriefing = new System.Windows.Forms.RadioButton();
            this.TapPlant = new System.Windows.Forms.TabPage();
            this.PnlTabPlant = new System.Windows.Forms.Panel();
            this.PnlPlIndustrialSafetyBriefingPlant = new System.Windows.Forms.Panel();
            this.ChkPlZKSImport = new System.Windows.Forms.CheckBox();
            this.LblPlZKSImport = new System.Windows.Forms.Label();
            this.LblPlBriefingValidUntil = new System.Windows.Forms.Label();
            this.DatPlBriefingValidUntil = new System.Windows.Forms.DateTimePicker();
            this.TxtPlPlantname = new System.Windows.Forms.TextBox();
            this.CbxPlBriefingDone = new System.Windows.Forms.CheckBox();
            this.DatPlBriefingDoneOn = new System.Windows.Forms.DateTimePicker();
            this.DgrPlPlant = new System.Windows.Forms.DataGrid();
            this.DGrTableStylePlant = new System.Windows.Forms.DataGridTableStyle();
            this.DgrTxtPKPlant = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTxtPlantName = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTxtPlDoneOn = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTxtUserName = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTxtSource = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTxtPlValidUntil = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTxtStatus = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTxtDirected = new System.Windows.Forms.DataGridTextBoxColumn();
            this.LblPlBriefing = new System.Windows.Forms.Label();
            this.LblPlBriefingDoneBy = new System.Windows.Forms.Label();
            this.LblPlBriefingDoneOn = new System.Windows.Forms.Label();
            this.LblPlBriefingDone = new System.Windows.Forms.Label();
            this.TxtPlBriefingDoneBy = new System.Windows.Forms.TextBox();
            this.RbtPlBriefing = new System.Windows.Forms.RadioButton();
            this.TapTechnicalDepartment = new System.Windows.Forms.TabPage();
            this.PnlTabTechnical = new System.Windows.Forms.Panel();
            this.PnlTecBriefingSite = new System.Windows.Forms.Panel();
            this.CbxTecRaisonalPlattform = new System.Windows.Forms.CheckBox();
            this.DatTecBriefingDoneOn = new System.Windows.Forms.DateTimePicker();
            this.LblTecBriefing = new System.Windows.Forms.Label();
            this.LblTecBriefingDoneBy = new System.Windows.Forms.Label();
            this.LblTecBriefingDoneOn = new System.Windows.Forms.Label();
            this.LblTecBriefingDone = new System.Windows.Forms.Label();
            this.TxtTecBriefingDoneBy = new System.Windows.Forms.TextBox();
            this.RbtTecBriefing = new System.Windows.Forms.RadioButton();
            this.TapSiteSecurity = new System.Windows.Forms.TabPage();
            this.PnlTabSiteSecure = new System.Windows.Forms.Panel();
            this.GrpSiSe1 = new System.Windows.Forms.GroupBox();
            this.PnlSiSePExternal = new System.Windows.Forms.Panel();
            this.LblSiSeParkTitle = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.DatSiSePExternalOn = new System.Windows.Forms.DateTimePicker();
            this.LblSiSePExternalBy = new System.Windows.Forms.Label();
            this.TxtSiSePExternalBy = new System.Windows.Forms.TextBox();
            this.LblSiSePExternalOn = new System.Windows.Forms.Label();
            this.CbxSiSePExternal = new System.Windows.Forms.CheckBox();
            this.PnlSiSeVehicleNumber = new System.Windows.Forms.Panel();
            this.TxtSiSeVehicleRegistrationNumberFour = new System.Windows.Forms.TextBox();
            this.TxtSiSeVehicleRegistrationNumberThree = new System.Windows.Forms.TextBox();
            this.TxtSiSeVehicleRegistrationNumberTwo = new System.Windows.Forms.TextBox();
            this.LblSiSeVehicleRegistrationNumber = new System.Windows.Forms.Label();
            this.TxtSiSeVehicleRegistrationNumber = new System.Windows.Forms.TextBox();
            this.PnlSiSeIdentityCard = new System.Windows.Forms.Panel();
            this.LblSiSeIdPhotoHitagRecHint = new System.Windows.Forms.Label();
            this.CbxSiSeIdPhotoHitagRec = new System.Windows.Forms.CheckBox();
            this.DatSiSeIdPhotoHitagRec = new System.Windows.Forms.DateTimePicker();
            this.LblSiSeIdPhotoHitagRecBy = new System.Windows.Forms.Label();
            this.LblSiSeIdPhotoHitagRec = new System.Windows.Forms.Label();
            this.TxtSiSeIdPhotoHitagRecBy = new System.Windows.Forms.TextBox();
            this.LblSiSeIdPhotoSmActRecOn = new System.Windows.Forms.Label();
            this.LblSiSeIdPhotoTitle = new System.Windows.Forms.Label();
            this.CbxSiSeIdPhotoSmActRec = new System.Windows.Forms.CheckBox();
            this.DatSiSeIdPhotoSmActRec = new System.Windows.Forms.DateTimePicker();
            this.LblSiSeIdPhotoSmAct = new System.Windows.Forms.Label();
            this.LblSiSeIdPhotoSmActRecBy = new System.Windows.Forms.Label();
            this.LblSiSeIdPhotoSmActRec = new System.Windows.Forms.Label();
            this.TxtSiSeIdPhotoSmActRecBy = new System.Windows.Forms.TextBox();
            this.LblSiSeIdentityCardRecievedOn = new System.Windows.Forms.Label();
            this.RbtSiSeIdPhotoSmAct = new System.Windows.Forms.RadioButton();
            this.PnlSiSeVehicleEntranceLong = new System.Windows.Forms.Panel();
            this.PnlSiSeVehicleEntranceLongControl = new System.Windows.Forms.Panel();
            this.RbtSiSeVehicleEntranceLongNo = new System.Windows.Forms.RadioButton();
            this.RbtSiSeVehicleEntranceLongYes = new System.Windows.Forms.RadioButton();
            this.DatSiSeVehicleEntranceLongReceivedOn = new System.Windows.Forms.DateTimePicker();
            this.LblSiSeVehicleEntranceLongReceivedBy = new System.Windows.Forms.Label();
            this.TxtSiSeVehicleEntranceLongReceivedBy = new System.Windows.Forms.TextBox();
            this.LblSiSeVehicleEntranceLongReceivedOn = new System.Windows.Forms.Label();
            this.LblSiSeVehicleEntranceLong = new System.Windows.Forms.Label();
            this.RbtSiSeVehicleEntranceLong = new System.Windows.Forms.RadioButton();
            this.LblSiSeVehicleEntranceLongReceived = new System.Windows.Forms.Label();
            this.PnlSiSeVehicleEntranceShort = new System.Windows.Forms.Panel();
            this.PnlSiSeVehicleEntranceShortControl = new System.Windows.Forms.Panel();
            this.RbtSiSeVehicleEntranceShortReceivedNo = new System.Windows.Forms.RadioButton();
            this.RbtSiSeVehicleEntranceShortReceivedYes = new System.Windows.Forms.RadioButton();
            this.DatSiSeVehicleEntranceShortReceivedOn = new System.Windows.Forms.DateTimePicker();
            this.LblSiSeVehicleEntranceShortReceivedBy = new System.Windows.Forms.Label();
            this.TxtSiSeVehicleEntranceShortReceivedBy = new System.Windows.Forms.TextBox();
            this.LblSiSeVehicleEntranceShortReceivedOn = new System.Windows.Forms.Label();
            this.LblSiSeVehicleEntranceShort = new System.Windows.Forms.Label();
            this.RbtSiSeVehicleEntranceShort = new System.Windows.Forms.RadioButton();
            this.PnlSiSeVehicleEntranceShortReceived = new System.Windows.Forms.Label();
            this.PnlSiSeAccess = new System.Windows.Forms.Panel();
            this.GpSiSe2 = new System.Windows.Forms.GroupBox();
            this.LblSiSeAccess = new System.Windows.Forms.Label();
            this.PnlSiSeAccessYNRbt = new System.Windows.Forms.Panel();
            this.RbtSiSeAccessAuthNo = new System.Windows.Forms.RadioButton();
            this.RbtSiSeAccessAuthYes = new System.Windows.Forms.RadioButton();
            this.LblSiSeAccessTitle = new System.Windows.Forms.Label();
            this.BtnSiSeAccessRevoke = new System.Windows.Forms.Button();
            this.TxtSiSeAccessAuthorizationComment = new System.Windows.Forms.TextBox();
            this.LblSiSeAccessHint = new System.Windows.Forms.Label();
            this.LblSiSeAccessComment = new System.Windows.Forms.Label();
            this.PnlSiSeSiteSecurity = new System.Windows.Forms.Panel();
            this.lblSiteSecBriTitle = new System.Windows.Forms.Label();
            this.CbxSiSeSiteSecBriRec = new System.Windows.Forms.CheckBox();
            this.DatSiSeSiteSecBriRec = new System.Windows.Forms.DateTimePicker();
            this.LblSiSeSiteBriOrd = new System.Windows.Forms.Label();
            this.LblSiSeSiteSecBriRecBy = new System.Windows.Forms.Label();
            this.LblSiSeSiteSecBriRec = new System.Windows.Forms.Label();
            this.TxtSiSeSiteSecBriRecBy = new System.Windows.Forms.TextBox();
            this.LblSiSeSiteSecBriRecOn = new System.Windows.Forms.Label();
            this.RbtSiSeSiteSecBri = new System.Windows.Forms.RadioButton();
            this.LblDeliveryDate = new System.Windows.Forms.Label();
            this.TxtPassValidUntil = new System.Windows.Forms.TextBox();
            this.LblPassValidUntil = new System.Windows.Forms.Label();
            this.LblPassValidFrom = new System.Windows.Forms.Label();
            this.LblCoWorkerData = new System.Windows.Forms.Label();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnSummary = new System.Windows.Forms.Button();
            this.BtnPass = new System.Windows.Forms.Button();
            this.LblMask = new System.Windows.Forms.Label();
            this.DatPassValidFrom = new System.Windows.Forms.DateTimePicker();
            this.DatDeliveryDate = new System.Windows.Forms.DateTimePicker();
            this.TooPass = new System.Windows.Forms.ToolTip(this.components);
            this.TooSave = new System.Windows.Forms.ToolTip(this.components);
            this.TooBackToSummary = new System.Windows.Forms.ToolTip(this.components);
            this.TooClearMask = new System.Windows.Forms.ToolTip(this.components);
            this.TooIdentityCard = new System.Windows.Forms.ToolTip(this.components);
            this.TooExContractor = new System.Windows.Forms.ToolTip(this.components);
            this.TooPdf = new System.Windows.Forms.ToolTip(this.components);
            this.LblZKSRetCode = new System.Windows.Forms.Label();
            this.TxtZKSRetCode = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.SbpMessage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sbpenvironment)).BeginInit();
            this.PnlCoWorkerData.SuspendLayout();
            this.TabSiteSecurity.SuspendLayout();
            this.TapReception.SuspendLayout();
            this.PnlTabReception.SuspendLayout();
            this.PnlIdCardReader.SuspendLayout();
            this.PnlReBasics.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CoWorkerPhotoBox)).BeginInit();
            this.PnlReBriefing.SuspendLayout();
            this.PnlReAccessApprent.SuspendLayout();
            this.PnlReSafetyInstructions.SuspendLayout();
            this.PnlReAccessAuthorization.SuspendLayout();
            this.PnlReIdentityCard.SuspendLayout();
            this.TapCoordinator.SuspendLayout();
            this.PnlTabCoordinator.SuspendLayout();
            this.PnlCoSmartAct.SuspendLayout();
            this.PnlCoPhotoCard.SuspendLayout();
            this.PnlCoVehicleEntrance.SuspendLayout();
            this.PnlCoVehicleEntranceLong.SuspendLayout();
            this.PnlCoVehicleEntranceShort.SuspendLayout();
            this.PnlCoCheckInCheckOff.SuspendLayout();
            this.PnlCoDepartmentalBriefing.SuspendLayout();
            this.PnlCoData.SuspendLayout();
            this.PnlCoPrecautionaryMedical.SuspendLayout();
            this.PnlCoRespiratoryMaskBriefing.SuspendLayout();
            this.PnlCoIndustrialSafetyBriefingSite.SuspendLayout();
            this.PnlCoBriefing.SuspendLayout();
            this.PnlCoFireman.SuspendLayout();
            this.PnlCoBreathingApparatusG26_3.SuspendLayout();
            this.PnlCoSiteSecurityBriefing.SuspendLayout();
            this.PnlCoCranes.SuspendLayout();
            this.PnlCoRaisablePlattform.SuspendLayout();
            this.PnlCoPalletLifter.SuspendLayout();
            this.PnlCoBreathingApparatusG26_2.SuspendLayout();
            this.PnlCoSafetyPass.SuspendLayout();
            this.TapSiteFireService.SuspendLayout();
            this.PnlTabSiteFireService.SuspendLayout();
            this.PnlSiFiMaskLent.SuspendLayout();
            this.PnlSiFiAllBriefings.SuspendLayout();
            this.PnlSiFiSiteSecurityBriefingG26_2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.PnlSiFiSiteSecurityBriefingG26_3.SuspendLayout();
            this.PnlSiFiMaskBriefing.SuspendLayout();
            this.TapSiteMedicalService.SuspendLayout();
            this.PnlTabSiteMedical.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgrSiMedPrecautionaryMedical)).BeginInit();
            this.TapSafetyAtWork.SuspendLayout();
            this.PnlTabSafetyWork.SuspendLayout();
            this.PnlSaAtWoSiteSecurityBriefing.SuspendLayout();
            this.PnlSaAtWoPalletLifterBriefing.SuspendLayout();
            this.PnlSaAtWoCranesBriefing.SuspendLayout();
            this.PnlSaAtWoIndustrialSafetyBriefing.SuspendLayout();
            this.TapPlant.SuspendLayout();
            this.PnlTabPlant.SuspendLayout();
            this.PnlPlIndustrialSafetyBriefingPlant.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgrPlPlant)).BeginInit();
            this.TapTechnicalDepartment.SuspendLayout();
            this.PnlTabTechnical.SuspendLayout();
            this.PnlTecBriefingSite.SuspendLayout();
            this.TapSiteSecurity.SuspendLayout();
            this.PnlTabSiteSecure.SuspendLayout();
            this.PnlSiSePExternal.SuspendLayout();
            this.PnlSiSeVehicleNumber.SuspendLayout();
            this.PnlSiSeIdentityCard.SuspendLayout();
            this.PnlSiSeVehicleEntranceLong.SuspendLayout();
            this.PnlSiSeVehicleEntranceLongControl.SuspendLayout();
            this.PnlSiSeVehicleEntranceShort.SuspendLayout();
            this.PnlSiSeVehicleEntranceShortControl.SuspendLayout();
            this.PnlSiSeAccess.SuspendLayout();
            this.PnlSiSeAccessYNRbt.SuspendLayout();
            this.PnlSiSeSiteSecurity.SuspendLayout();
            this.SuspendLayout();
            // 
            // PnlCoWorkerData
            // 
            this.PnlCoWorkerData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlCoWorkerData.Controls.Add(this.TxtCoordinator);
            this.PnlCoWorkerData.Controls.Add(this.TxtSubcontractor);
            this.PnlCoWorkerData.Controls.Add(this.TxtExternalContractor);
            this.PnlCoWorkerData.Controls.Add(this.TxtSupervisor);
            this.PnlCoWorkerData.Controls.Add(this.TxtFirstname);
            this.PnlCoWorkerData.Controls.Add(this.TxtSurname);
            this.PnlCoWorkerData.Controls.Add(this.LblExternalContractor);
            this.PnlCoWorkerData.Controls.Add(this.LblSurname);
            this.PnlCoWorkerData.Controls.Add(this.LblSubcontractor);
            this.PnlCoWorkerData.Controls.Add(this.LblSupervisor);
            this.PnlCoWorkerData.Controls.Add(this.LblCoordinator);
            this.PnlCoWorkerData.Controls.Add(this.LblFirstname);
            this.PnlCoWorkerData.Location = new System.Drawing.Point(5, 56);
            this.PnlCoWorkerData.Name = "PnlCoWorkerData";
            this.PnlCoWorkerData.Size = new System.Drawing.Size(1256, 80);
            this.PnlCoWorkerData.TabIndex = 0;
            // 
            // TxtCoordinator
            // 
            this.TxtCoordinator.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCoordinator.Location = new System.Drawing.Point(892, 47);
            this.TxtCoordinator.Name = "TxtCoordinator";
            this.TxtCoordinator.ReadOnly = true;
            this.TxtCoordinator.Size = new System.Drawing.Size(220, 21);
            this.TxtCoordinator.TabIndex = 0;
            this.TxtCoordinator.TabStop = false;
            // 
            // TxtSubcontractor
            // 
            this.TxtSubcontractor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSubcontractor.Location = new System.Drawing.Point(108, 47);
            this.TxtSubcontractor.Name = "TxtSubcontractor";
            this.TxtSubcontractor.ReadOnly = true;
            this.TxtSubcontractor.Size = new System.Drawing.Size(220, 21);
            this.TxtSubcontractor.TabIndex = 0;
            this.TxtSubcontractor.TabStop = false;
            // 
            // TxtExternalContractor
            // 
            this.TxtExternalContractor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtExternalContractor.Location = new System.Drawing.Point(108, 23);
            this.TxtExternalContractor.Name = "TxtExternalContractor";
            this.TxtExternalContractor.ReadOnly = true;
            this.TxtExternalContractor.Size = new System.Drawing.Size(220, 21);
            this.TxtExternalContractor.TabIndex = 0;
            this.TxtExternalContractor.TabStop = false;
            // 
            // TxtSupervisor
            // 
            this.TxtSupervisor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSupervisor.Location = new System.Drawing.Point(892, 23);
            this.TxtSupervisor.Name = "TxtSupervisor";
            this.TxtSupervisor.ReadOnly = true;
            this.TxtSupervisor.Size = new System.Drawing.Size(220, 21);
            this.TxtSupervisor.TabIndex = 0;
            this.TxtSupervisor.TabStop = false;
            // 
            // TxtFirstname
            // 
            this.TxtFirstname.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtFirstname.Location = new System.Drawing.Point(489, 47);
            this.TxtFirstname.Name = "TxtFirstname";
            this.TxtFirstname.ReadOnly = true;
            this.TxtFirstname.Size = new System.Drawing.Size(220, 21);
            this.TxtFirstname.TabIndex = 0;
            this.TxtFirstname.TabStop = false;
            // 
            // TxtSurname
            // 
            this.TxtSurname.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSurname.Location = new System.Drawing.Point(489, 23);
            this.TxtSurname.Name = "TxtSurname";
            this.TxtSurname.ReadOnly = true;
            this.TxtSurname.Size = new System.Drawing.Size(220, 21);
            this.TxtSurname.TabIndex = 0;
            this.TxtSurname.TabStop = false;
            // 
            // LblExternalContractor
            // 
            this.LblExternalContractor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblExternalContractor.Location = new System.Drawing.Point(26, 25);
            this.LblExternalContractor.Name = "LblExternalContractor";
            this.LblExternalContractor.Size = new System.Drawing.Size(72, 16);
            this.LblExternalContractor.TabIndex = 10;
            this.LblExternalContractor.Text = "Fremdfirma";
            // 
            // LblSurname
            // 
            this.LblSurname.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSurname.Location = new System.Drawing.Point(409, 25);
            this.LblSurname.Name = "LblSurname";
            this.LblSurname.Size = new System.Drawing.Size(72, 16);
            this.LblSurname.TabIndex = 5;
            this.LblSurname.Text = "Nachname";
            // 
            // LblSubcontractor
            // 
            this.LblSubcontractor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSubcontractor.Location = new System.Drawing.Point(26, 49);
            this.LblSubcontractor.Name = "LblSubcontractor";
            this.LblSubcontractor.Size = new System.Drawing.Size(72, 16);
            this.LblSubcontractor.TabIndex = 11;
            this.LblSubcontractor.Text = "Subfirma";
            // 
            // LblSupervisor
            // 
            this.LblSupervisor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSupervisor.Location = new System.Drawing.Point(792, 25);
            this.LblSupervisor.Name = "LblSupervisor";
            this.LblSupervisor.Size = new System.Drawing.Size(96, 16);
            this.LblSupervisor.TabIndex = 7;
            this.LblSupervisor.Text = "Baustellenleiter";
            // 
            // LblCoordinator
            // 
            this.LblCoordinator.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCoordinator.Location = new System.Drawing.Point(792, 49);
            this.LblCoordinator.Name = "LblCoordinator";
            this.LblCoordinator.Size = new System.Drawing.Size(72, 16);
            this.LblCoordinator.TabIndex = 15;
            this.LblCoordinator.Text = "Koordinator";
            // 
            // LblFirstname
            // 
            this.LblFirstname.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblFirstname.Location = new System.Drawing.Point(409, 49);
            this.LblFirstname.Name = "LblFirstname";
            this.LblFirstname.Size = new System.Drawing.Size(72, 16);
            this.LblFirstname.TabIndex = 8;
            this.LblFirstname.Text = "Vorname";
            // 
            // TabSiteSecurity
            // 
            this.TabSiteSecurity.Controls.Add(this.TapReception);
            this.TabSiteSecurity.Controls.Add(this.TapCoordinator);
            this.TabSiteSecurity.Controls.Add(this.TapSiteFireService);
            this.TabSiteSecurity.Controls.Add(this.TapSiteMedicalService);
            this.TabSiteSecurity.Controls.Add(this.TapSafetyAtWork);
            this.TabSiteSecurity.Controls.Add(this.TapPlant);
            this.TabSiteSecurity.Controls.Add(this.TapTechnicalDepartment);
            this.TabSiteSecurity.Controls.Add(this.TapSiteSecurity);
            this.TabSiteSecurity.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TabSiteSecurity.ItemSize = new System.Drawing.Size(78, 21);
            this.TabSiteSecurity.Location = new System.Drawing.Point(5, 142);
            this.TabSiteSecurity.Name = "TabSiteSecurity";
            this.TabSiteSecurity.SelectedIndex = 0;
            this.TabSiteSecurity.Size = new System.Drawing.Size(1257, 689);
            this.TabSiteSecurity.TabIndex = 0;
            this.TabSiteSecurity.TabStop = false;
            this.TabSiteSecurity.SelectedIndexChanged += new System.EventHandler(this.TabSiteSecurity_SelectedIndexChanged);
            // 
            // TapReception
            // 
            this.TapReception.Controls.Add(this.PnlTabReception);
            this.TapReception.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TapReception.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.TapReception.Location = new System.Drawing.Point(4, 25);
            this.TapReception.Name = "TapReception";
            this.TapReception.Size = new System.Drawing.Size(1249, 660);
            this.TapReception.TabIndex = 0;
            this.TapReception.Text = "Empfang";
            // 
            // PnlTabReception
            // 
            this.PnlTabReception.Controls.Add(this.PnlIdCardReader);
            this.PnlTabReception.Controls.Add(this.PnlReBasics);
            this.PnlTabReception.Controls.Add(this.PnlReBriefing);
            this.PnlTabReception.Controls.Add(this.PnlReIdentityCard);
            this.PnlTabReception.Controls.Add(this.BtnReClear);
            this.PnlTabReception.Enabled = false;
            this.PnlTabReception.Location = new System.Drawing.Point(0, 0);
            this.PnlTabReception.Name = "PnlTabReception";
            this.PnlTabReception.Size = new System.Drawing.Size(1249, 657);
            this.PnlTabReception.TabIndex = 0;
            // 
            // PnlIdCardReader
            // 
            this.PnlIdCardReader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlIdCardReader.Controls.Add(this.grpRe2);
            this.PnlIdCardReader.Controls.Add(this.BtnReHitag2Reader);
            this.PnlIdCardReader.Controls.Add(this.btnreSaveIdRdr);
            this.PnlIdCardReader.Controls.Add(this.lblReSaveIdRdr);
            this.PnlIdCardReader.Controls.Add(this.lblReIDCardReaderHint);
            this.PnlIdCardReader.Controls.Add(this.BtnReMifareReader);
            this.PnlIdCardReader.Controls.Add(this.label7);
            this.PnlIdCardReader.Controls.Add(this.TxtReIDReaderMifare);
            this.PnlIdCardReader.Controls.Add(this.LblReHitag2Reader);
            this.PnlIdCardReader.Controls.Add(this.LblReMifareReader);
            this.PnlIdCardReader.Controls.Add(this.TxtReIDReaderHitag2);
            this.PnlIdCardReader.Location = new System.Drawing.Point(629, 384);
            this.PnlIdCardReader.Name = "PnlIdCardReader";
            this.PnlIdCardReader.Size = new System.Drawing.Size(617, 198);
            this.PnlIdCardReader.TabIndex = 14;
            // 
            // grpRe2
            // 
            this.grpRe2.Location = new System.Drawing.Point(7, 135);
            this.grpRe2.Name = "grpRe2";
            this.grpRe2.Size = new System.Drawing.Size(597, 10);
            this.grpRe2.TabIndex = 77;
            this.grpRe2.TabStop = false;
            // 
            // BtnReHitag2Reader
            // 
            this.BtnReHitag2Reader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnReHitag2Reader.Location = new System.Drawing.Point(410, 74);
            this.BtnReHitag2Reader.Name = "BtnReHitag2Reader";
            this.BtnReHitag2Reader.Size = new System.Drawing.Size(100, 25);
            this.BtnReHitag2Reader.TabIndex = 32;
            this.BtnReHitag2Reader.Tag = "";
            this.BtnReHitag2Reader.Text = "zuordnen";
            this.TooIdentityCard.SetToolTip(this.BtnReHitag2Reader, "...");
            this.BtnReHitag2Reader.Click += new System.EventHandler(this.BtnReHitag2Reader_Click);
            // 
            // btnreSaveIdRdr
            // 
            this.btnreSaveIdRdr.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnreSaveIdRdr.Location = new System.Drawing.Point(374, 155);
            this.btnreSaveIdRdr.Name = "btnreSaveIdRdr";
            this.btnreSaveIdRdr.Size = new System.Drawing.Size(136, 25);
            this.btnreSaveIdRdr.TabIndex = 34;
            this.btnreSaveIdRdr.Tag = "";
            this.btnreSaveIdRdr.Text = "Speichern";
            this.btnreSaveIdRdr.Click += new System.EventHandler(this.btnreSaveIdRdr_Click);
            // 
            // lblReSaveIdRdr
            // 
            this.lblReSaveIdRdr.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReSaveIdRdr.Location = new System.Drawing.Point(22, 160);
            this.lblReSaveIdRdr.Name = "lblReSaveIdRdr";
            this.lblReSaveIdRdr.Size = new System.Drawing.Size(340, 23);
            this.lblReSaveIdRdr.TabIndex = 85;
            this.lblReSaveIdRdr.Text = "speichert diese Angaben für den aktuellen Benutzer";
            // 
            // lblReIDCardReaderHint
            // 
            this.lblReIDCardReaderHint.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReIDCardReaderHint.Location = new System.Drawing.Point(22, 40);
            this.lblReIDCardReaderHint.Name = "lblReIDCardReaderHint";
            this.lblReIDCardReaderHint.Size = new System.Drawing.Size(549, 23);
            this.lblReIDCardReaderHint.TabIndex = 79;
            this.lblReIDCardReaderHint.Text = "ordnet dem aktuellen Benutzer ZKS-Terminals zu. ";
            this.lblReIDCardReaderHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BtnReMifareReader
            // 
            this.BtnReMifareReader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnReMifareReader.Location = new System.Drawing.Point(410, 106);
            this.BtnReMifareReader.Name = "BtnReMifareReader";
            this.BtnReMifareReader.Size = new System.Drawing.Size(100, 25);
            this.BtnReMifareReader.TabIndex = 33;
            this.BtnReMifareReader.Tag = "";
            this.BtnReMifareReader.Text = "zuordnen";
            this.TooIdentityCard.SetToolTip(this.BtnReMifareReader, "...");
            this.BtnReMifareReader.Click += new System.EventHandler(this.BtnReMifareReader_Click);
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(13, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(182, 23);
            this.label7.TabIndex = 83;
            this.label7.Text = "Meine Ausweisleser";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TxtReIDReaderMifare
            // 
            this.TxtReIDReaderMifare.Enabled = false;
            this.TxtReIDReaderMifare.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtReIDReaderMifare.Location = new System.Drawing.Point(175, 106);
            this.TxtReIDReaderMifare.Name = "TxtReIDReaderMifare";
            this.TxtReIDReaderMifare.ReadOnly = true;
            this.TxtReIDReaderMifare.Size = new System.Drawing.Size(221, 21);
            this.TxtReIDReaderMifare.TabIndex = 1;
            this.TxtReIDReaderMifare.TabStop = false;
            // 
            // LblReHitag2Reader
            // 
            this.LblReHitag2Reader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblReHitag2Reader.Location = new System.Drawing.Point(22, 77);
            this.LblReHitag2Reader.Name = "LblReHitag2Reader";
            this.LblReHitag2Reader.Size = new System.Drawing.Size(143, 23);
            this.LblReHitag2Reader.TabIndex = 62;
            this.LblReHitag2Reader.Text = "ZKS-Terminal für Hitag2";
            // 
            // LblReMifareReader
            // 
            this.LblReMifareReader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblReMifareReader.Location = new System.Drawing.Point(22, 109);
            this.LblReMifareReader.Name = "LblReMifareReader";
            this.LblReMifareReader.Size = new System.Drawing.Size(142, 23);
            this.LblReMifareReader.TabIndex = 62;
            this.LblReMifareReader.Text = "ZKS-Terminal für Mifare";
            // 
            // TxtReIDReaderHitag2
            // 
            this.TxtReIDReaderHitag2.Enabled = false;
            this.TxtReIDReaderHitag2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtReIDReaderHitag2.Location = new System.Drawing.Point(175, 76);
            this.TxtReIDReaderHitag2.Name = "TxtReIDReaderHitag2";
            this.TxtReIDReaderHitag2.ReadOnly = true;
            this.TxtReIDReaderHitag2.Size = new System.Drawing.Size(221, 21);
            this.TxtReIDReaderHitag2.TabIndex = 1;
            this.TxtReIDReaderHitag2.TabStop = false;
            this.TxtReIDReaderHitag2.TextChanged += new System.EventHandler(this.TxtReIdentityCardNumber_TextChanged);
            this.TxtReIDReaderHitag2.Enter += new System.EventHandler(this.TxtReIdentityCardNumber_Enter);
            // 
            // PnlReBasics
            // 
            this.PnlReBasics.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlReBasics.Controls.Add(this.CoWorkerPhotoBox);
            this.PnlReBasics.Controls.Add(this.LblReTitleBase);
            this.PnlReBasics.Controls.Add(this.btnCoordHist);
            this.PnlReBasics.Controls.Add(this.BtnReSearchExternalContractor);
            this.PnlReBasics.Controls.Add(this.TxtReVehicleRegistrationNumberFour);
            this.PnlReBasics.Controls.Add(this.TxtReVehicleRegistrationNumberThree);
            this.PnlReBasics.Controls.Add(this.TxtReVehicleRegistrationNumberTwo);
            this.PnlReBasics.Controls.Add(this.LblRePlaceOfBirth);
            this.PnlReBasics.Controls.Add(this.TxtRePlaceOfBirth);
            this.PnlReBasics.Controls.Add(this.CobReCoordinator);
            this.PnlReBasics.Controls.Add(this.CobReExternalContractor);
            this.PnlReBasics.Controls.Add(this.LblReExternalContractor);
            this.PnlReBasics.Controls.Add(this.LblReCoordinator);
            this.PnlReBasics.Controls.Add(this.LblReSurname);
            this.PnlReBasics.Controls.Add(this.LblReFirstname);
            this.PnlReBasics.Controls.Add(this.TxtReSurname);
            this.PnlReBasics.Controls.Add(this.TxtReFirstname);
            this.PnlReBasics.Controls.Add(this.LblReDateOfBirth);
            this.PnlReBasics.Controls.Add(this.TxtReDateOfBirth);
            this.PnlReBasics.Controls.Add(this.LblReVehicleRegistrationNumber);
            this.PnlReBasics.Controls.Add(this.TxtReVehicleRegistrationNumber);
            this.PnlReBasics.Location = new System.Drawing.Point(3, 3);
            this.PnlReBasics.Name = "PnlReBasics";
            this.PnlReBasics.Size = new System.Drawing.Size(1243, 171);
            this.PnlReBasics.TabIndex = 2;
            // 
            // CoWorkerPhotoBox
            // 
            this.CoWorkerPhotoBox.Location = new System.Drawing.Point(461, 11);
            this.CoWorkerPhotoBox.Name = "CoWorkerPhotoBox";
            this.CoWorkerPhotoBox.Size = new System.Drawing.Size(118, 148);
            this.CoWorkerPhotoBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.CoWorkerPhotoBox.TabIndex = 84;
            this.CoWorkerPhotoBox.TabStop = false;
            // 
            // LblReTitleBase
            // 
            this.LblReTitleBase.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblReTitleBase.Location = new System.Drawing.Point(13, 8);
            this.LblReTitleBase.Name = "LblReTitleBase";
            this.LblReTitleBase.Size = new System.Drawing.Size(85, 23);
            this.LblReTitleBase.TabIndex = 83;
            this.LblReTitleBase.Text = "Basisdaten";
            this.LblReTitleBase.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnCoordHist
            // 
            this.btnCoordHist.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCoordHist.Image = ((System.Drawing.Image)(resources.GetObject("btnCoordHist.Image")));
            this.btnCoordHist.Location = new System.Drawing.Point(1113, 36);
            this.btnCoordHist.Name = "btnCoordHist";
            this.btnCoordHist.Size = new System.Drawing.Size(43, 21);
            this.btnCoordHist.TabIndex = 13;
            this.btnCoordHist.Text = "? &F";
            this.TooExContractor.SetToolTip(this.btnCoordHist, "Öffnet die Historie aller Koordinatoren für diesen FFMA");
            this.btnCoordHist.Click += new System.EventHandler(this.btnCoordHist_Click);
            // 
            // BtnReSearchExternalContractor
            // 
            this.BtnReSearchExternalContractor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnReSearchExternalContractor.Location = new System.Drawing.Point(1113, 68);
            this.BtnReSearchExternalContractor.Name = "BtnReSearchExternalContractor";
            this.BtnReSearchExternalContractor.Size = new System.Drawing.Size(43, 21);
            this.BtnReSearchExternalContractor.TabIndex = 14;
            this.BtnReSearchExternalContractor.Text = "?";
            this.TooExContractor.SetToolTip(this.BtnReSearchExternalContractor, "Öffnet die Maske Fremdfirmensuche");
            this.BtnReSearchExternalContractor.Click += new System.EventHandler(this.BtnReSearchExternalContractor_Click_1);
            // 
            // TxtReVehicleRegistrationNumberFour
            // 
            this.TxtReVehicleRegistrationNumberFour.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtReVehicleRegistrationNumberFour.Location = new System.Drawing.Point(948, 131);
            this.TxtReVehicleRegistrationNumberFour.MaxLength = 15;
            this.TxtReVehicleRegistrationNumberFour.Name = "TxtReVehicleRegistrationNumberFour";
            this.TxtReVehicleRegistrationNumberFour.Size = new System.Drawing.Size(156, 21);
            this.TxtReVehicleRegistrationNumberFour.TabIndex = 12;
            // 
            // TxtReVehicleRegistrationNumberThree
            // 
            this.TxtReVehicleRegistrationNumberThree.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtReVehicleRegistrationNumberThree.Location = new System.Drawing.Point(775, 131);
            this.TxtReVehicleRegistrationNumberThree.MaxLength = 15;
            this.TxtReVehicleRegistrationNumberThree.Name = "TxtReVehicleRegistrationNumberThree";
            this.TxtReVehicleRegistrationNumberThree.Size = new System.Drawing.Size(163, 21);
            this.TxtReVehicleRegistrationNumberThree.TabIndex = 11;
            // 
            // TxtReVehicleRegistrationNumberTwo
            // 
            this.TxtReVehicleRegistrationNumberTwo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtReVehicleRegistrationNumberTwo.Location = new System.Drawing.Point(948, 101);
            this.TxtReVehicleRegistrationNumberTwo.MaxLength = 15;
            this.TxtReVehicleRegistrationNumberTwo.Name = "TxtReVehicleRegistrationNumberTwo";
            this.TxtReVehicleRegistrationNumberTwo.Size = new System.Drawing.Size(156, 21);
            this.TxtReVehicleRegistrationNumberTwo.TabIndex = 10;
            // 
            // LblRePlaceOfBirth
            // 
            this.LblRePlaceOfBirth.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblRePlaceOfBirth.Location = new System.Drawing.Point(32, 133);
            this.LblRePlaceOfBirth.Name = "LblRePlaceOfBirth";
            this.LblRePlaceOfBirth.Size = new System.Drawing.Size(88, 23);
            this.LblRePlaceOfBirth.TabIndex = 63;
            this.LblRePlaceOfBirth.Text = "Geburtsort";
            // 
            // TxtRePlaceOfBirth
            // 
            this.TxtRePlaceOfBirth.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtRePlaceOfBirth.Location = new System.Drawing.Point(134, 131);
            this.TxtRePlaceOfBirth.MaxLength = 30;
            this.TxtRePlaceOfBirth.Name = "TxtRePlaceOfBirth";
            this.TxtRePlaceOfBirth.Size = new System.Drawing.Size(298, 21);
            this.TxtRePlaceOfBirth.TabIndex = 6;
            // 
            // CobReCoordinator
            // 
            this.CobReCoordinator.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CobReCoordinator.Location = new System.Drawing.Point(775, 35);
            this.CobReCoordinator.MaxLength = 30;
            this.CobReCoordinator.Name = "CobReCoordinator";
            this.CobReCoordinator.Size = new System.Drawing.Size(329, 23);
            this.CobReCoordinator.TabIndex = 7;
            this.CobReCoordinator.SelectedIndexChanged += new System.EventHandler(this.CobReCoordinator_SelectedIndexChanged_1);
            // 
            // CobReExternalContractor
            // 
            this.CobReExternalContractor.AccessibleRole = System.Windows.Forms.AccessibleRole.List;
            this.CobReExternalContractor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CobReExternalContractor.Location = new System.Drawing.Point(775, 69);
            this.CobReExternalContractor.Name = "CobReExternalContractor";
            this.CobReExternalContractor.Size = new System.Drawing.Size(329, 23);
            this.CobReExternalContractor.TabIndex = 8;
            this.CobReExternalContractor.SelectedIndexChanged += new System.EventHandler(this.CobReExternalContractor_SelectedIndexChanged_1);
            // 
            // LblReExternalContractor
            // 
            this.LblReExternalContractor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblReExternalContractor.Location = new System.Drawing.Point(642, 71);
            this.LblReExternalContractor.Name = "LblReExternalContractor";
            this.LblReExternalContractor.Size = new System.Drawing.Size(88, 23);
            this.LblReExternalContractor.TabIndex = 58;
            this.LblReExternalContractor.Text = "Fremdfirma";
            // 
            // LblReCoordinator
            // 
            this.LblReCoordinator.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblReCoordinator.Location = new System.Drawing.Point(642, 40);
            this.LblReCoordinator.Name = "LblReCoordinator";
            this.LblReCoordinator.Size = new System.Drawing.Size(88, 23);
            this.LblReCoordinator.TabIndex = 59;
            this.LblReCoordinator.Text = "Koordinator";
            // 
            // LblReSurname
            // 
            this.LblReSurname.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblReSurname.Location = new System.Drawing.Point(32, 39);
            this.LblReSurname.Name = "LblReSurname";
            this.LblReSurname.Size = new System.Drawing.Size(88, 23);
            this.LblReSurname.TabIndex = 54;
            this.LblReSurname.Text = "Nachname";
            // 
            // LblReFirstname
            // 
            this.LblReFirstname.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblReFirstname.Location = new System.Drawing.Point(32, 71);
            this.LblReFirstname.Name = "LblReFirstname";
            this.LblReFirstname.Size = new System.Drawing.Size(88, 23);
            this.LblReFirstname.TabIndex = 55;
            this.LblReFirstname.Text = "Vorname";
            // 
            // TxtReSurname
            // 
            this.TxtReSurname.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtReSurname.Location = new System.Drawing.Point(134, 37);
            this.TxtReSurname.MaxLength = 30;
            this.TxtReSurname.Name = "TxtReSurname";
            this.TxtReSurname.Size = new System.Drawing.Size(298, 21);
            this.TxtReSurname.TabIndex = 3;
            // 
            // TxtReFirstname
            // 
            this.TxtReFirstname.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtReFirstname.Location = new System.Drawing.Point(134, 69);
            this.TxtReFirstname.MaxLength = 30;
            this.TxtReFirstname.Name = "TxtReFirstname";
            this.TxtReFirstname.Size = new System.Drawing.Size(298, 21);
            this.TxtReFirstname.TabIndex = 4;
            // 
            // LblReDateOfBirth
            // 
            this.LblReDateOfBirth.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblReDateOfBirth.Location = new System.Drawing.Point(32, 103);
            this.LblReDateOfBirth.Name = "LblReDateOfBirth";
            this.LblReDateOfBirth.Size = new System.Drawing.Size(88, 23);
            this.LblReDateOfBirth.TabIndex = 39;
            this.LblReDateOfBirth.Text = "Geburtsdatum";
            // 
            // TxtReDateOfBirth
            // 
            this.TxtReDateOfBirth.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtReDateOfBirth.Location = new System.Drawing.Point(134, 101);
            this.TxtReDateOfBirth.MaxLength = 10;
            this.TxtReDateOfBirth.Name = "TxtReDateOfBirth";
            this.TxtReDateOfBirth.Size = new System.Drawing.Size(161, 21);
            this.TxtReDateOfBirth.TabIndex = 5;
            this.TxtReDateOfBirth.Leave += new System.EventHandler(this.TxtReDateOfBirth_Leave);
            // 
            // LblReVehicleRegistrationNumber
            // 
            this.LblReVehicleRegistrationNumber.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblReVehicleRegistrationNumber.Location = new System.Drawing.Point(642, 104);
            this.LblReVehicleRegistrationNumber.Name = "LblReVehicleRegistrationNumber";
            this.LblReVehicleRegistrationNumber.Size = new System.Drawing.Size(112, 22);
            this.LblReVehicleRegistrationNumber.TabIndex = 41;
            this.LblReVehicleRegistrationNumber.Text = "KFZ-Kennzeichen";
            // 
            // TxtReVehicleRegistrationNumber
            // 
            this.TxtReVehicleRegistrationNumber.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtReVehicleRegistrationNumber.Location = new System.Drawing.Point(775, 101);
            this.TxtReVehicleRegistrationNumber.MaxLength = 15;
            this.TxtReVehicleRegistrationNumber.Name = "TxtReVehicleRegistrationNumber";
            this.TxtReVehicleRegistrationNumber.Size = new System.Drawing.Size(163, 21);
            this.TxtReVehicleRegistrationNumber.TabIndex = 9;
            // 
            // PnlReBriefing
            // 
            this.PnlReBriefing.BackColor = System.Drawing.SystemColors.Control;
            this.PnlReBriefing.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlReBriefing.Controls.Add(this.BtnReSafetyRevoke);
            this.PnlReBriefing.Controls.Add(this.lblReTitleAccess);
            this.PnlReBriefing.Controls.Add(this.LblReAccessApprentBy);
            this.PnlReBriefing.Controls.Add(this.TxtReAccessApprentBy);
            this.PnlReBriefing.Controls.Add(this.GrpReBrDummy);
            this.PnlReBriefing.Controls.Add(this.PnlReAccessApprent);
            this.PnlReBriefing.Controls.Add(this.LblReAccessApprent);
            this.PnlReBriefing.Controls.Add(this.LblReAccessAuthorizationBy);
            this.PnlReBriefing.Controls.Add(this.TxtReAccessAuthorizationBy);
            this.PnlReBriefing.Controls.Add(this.LblReSafetyInstructions);
            this.PnlReBriefing.Controls.Add(this.PnlReSafetyInstructions);
            this.PnlReBriefing.Controls.Add(this.LblReAccessAuthorization);
            this.PnlReBriefing.Controls.Add(this.PnlReAccessAuthorization);
            this.PnlReBriefing.Controls.Add(this.LblReSafetyInstructionsBy);
            this.PnlReBriefing.Controls.Add(this.TxtReSafetyInstructionsBy);
            this.PnlReBriefing.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PnlReBriefing.ForeColor = System.Drawing.SystemColors.ControlText;
            this.PnlReBriefing.Location = new System.Drawing.Point(3, 181);
            this.PnlReBriefing.Name = "PnlReBriefing";
            this.PnlReBriefing.Size = new System.Drawing.Size(1243, 196);
            this.PnlReBriefing.TabIndex = 13;
            // 
            // BtnReSafetyRevoke
            // 
            this.BtnReSafetyRevoke.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnReSafetyRevoke.Location = new System.Drawing.Point(1021, 39);
            this.BtnReSafetyRevoke.Name = "BtnReSafetyRevoke";
            this.BtnReSafetyRevoke.Size = new System.Drawing.Size(136, 25);
            this.BtnReSafetyRevoke.TabIndex = 85;
            this.BtnReSafetyRevoke.Tag = "";
            this.BtnReSafetyRevoke.Text = "Zurücksetzen";
            this.TooIdentityCard.SetToolTip(this.BtnReSafetyRevoke, "Setzt den Sicherheitshinweis zurück, d.h. dieser ist nicht mehr erhalten. Damit w" +
        "ird der Zutrittsberechtigung des FFMA entzogen.");
            this.BtnReSafetyRevoke.Click += new System.EventHandler(this.BtnReSafetyRevoke_Click);
            // 
            // lblReTitleAccess
            // 
            this.lblReTitleAccess.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReTitleAccess.Location = new System.Drawing.Point(13, 8);
            this.lblReTitleAccess.Name = "lblReTitleAccess";
            this.lblReTitleAccess.Size = new System.Drawing.Size(85, 23);
            this.lblReTitleAccess.TabIndex = 84;
            this.lblReTitleAccess.Text = "Zutritt";
            this.lblReTitleAccess.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LblReAccessApprentBy
            // 
            this.LblReAccessApprentBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblReAccessApprentBy.Location = new System.Drawing.Point(645, 155);
            this.LblReAccessApprentBy.Name = "LblReAccessApprentBy";
            this.LblReAccessApprentBy.Size = new System.Drawing.Size(84, 21);
            this.LblReAccessApprentBy.TabIndex = 78;
            this.LblReAccessApprentBy.Text = "gesetzt durch";
            // 
            // TxtReAccessApprentBy
            // 
            this.TxtReAccessApprentBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtReAccessApprentBy.Location = new System.Drawing.Point(729, 152);
            this.TxtReAccessApprentBy.MaxLength = 30;
            this.TxtReAccessApprentBy.Name = "TxtReAccessApprentBy";
            this.TxtReAccessApprentBy.ReadOnly = true;
            this.TxtReAccessApprentBy.Size = new System.Drawing.Size(240, 21);
            this.TxtReAccessApprentBy.TabIndex = 77;
            this.TxtReAccessApprentBy.TabStop = false;
            // 
            // GrpReBrDummy
            // 
            this.GrpReBrDummy.Location = new System.Drawing.Point(6, 130);
            this.GrpReBrDummy.Name = "GrpReBrDummy";
            this.GrpReBrDummy.Size = new System.Drawing.Size(1225, 5);
            this.GrpReBrDummy.TabIndex = 76;
            this.GrpReBrDummy.TabStop = false;
            // 
            // PnlReAccessApprent
            // 
            this.PnlReAccessApprent.Controls.Add(this.RbtReAccessApprentNo);
            this.PnlReAccessApprent.Controls.Add(this.RbtReAccessApprentYes);
            this.PnlReAccessApprent.Controls.Add(this.LblReAccessApprentTo);
            this.PnlReAccessApprent.Controls.Add(this.DatReAccessApprent);
            this.PnlReAccessApprent.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PnlReAccessApprent.Location = new System.Drawing.Point(268, 142);
            this.PnlReAccessApprent.Name = "PnlReAccessApprent";
            this.PnlReAccessApprent.Size = new System.Drawing.Size(327, 40);
            this.PnlReAccessApprent.TabIndex = 56;
            this.PnlReAccessApprent.TabStop = true;
            // 
            // RbtReAccessApprentNo
            // 
            this.RbtReAccessApprentNo.Checked = true;
            this.RbtReAccessApprentNo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtReAccessApprentNo.Location = new System.Drawing.Point(76, 8);
            this.RbtReAccessApprentNo.Name = "RbtReAccessApprentNo";
            this.RbtReAccessApprentNo.Size = new System.Drawing.Size(57, 24);
            this.RbtReAccessApprentNo.TabIndex = 22;
            this.RbtReAccessApprentNo.TabStop = true;
            this.RbtReAccessApprentNo.Text = "Nein";
            // 
            // RbtReAccessApprentYes
            // 
            this.RbtReAccessApprentYes.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtReAccessApprentYes.Location = new System.Drawing.Point(20, 8);
            this.RbtReAccessApprentYes.Name = "RbtReAccessApprentYes";
            this.RbtReAccessApprentYes.Size = new System.Drawing.Size(40, 24);
            this.RbtReAccessApprentYes.TabIndex = 21;
            this.RbtReAccessApprentYes.Text = "Ja";
            this.RbtReAccessApprentYes.CheckedChanged += new System.EventHandler(this.RbtReAccessApprentYes_CheckedChanged);
            // 
            // LblReAccessApprentTo
            // 
            this.LblReAccessApprentTo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblReAccessApprentTo.Location = new System.Drawing.Point(192, 13);
            this.LblReAccessApprentTo.Name = "LblReAccessApprentTo";
            this.LblReAccessApprentTo.Size = new System.Drawing.Size(25, 18);
            this.LblReAccessApprentTo.TabIndex = 58;
            this.LblReAccessApprentTo.Text = "bis";
            // 
            // DatReAccessApprent
            // 
            this.DatReAccessApprent.CustomFormat = "dd.MM.yyyy";
            this.DatReAccessApprent.Enabled = false;
            this.DatReAccessApprent.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatReAccessApprent.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.DatReAccessApprent.Location = new System.Drawing.Point(223, 10);
            this.DatReAccessApprent.Name = "DatReAccessApprent";
            this.DatReAccessApprent.Size = new System.Drawing.Size(88, 21);
            this.DatReAccessApprent.TabIndex = 23;
            this.DatReAccessApprent.Value = new System.DateTime(2014, 12, 19, 0, 0, 0, 0);
            this.DatReAccessApprent.Leave += new System.EventHandler(this.DatReAccessApprent_Leave);
            // 
            // LblReAccessApprent
            // 
            this.LblReAccessApprent.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblReAccessApprent.Location = new System.Drawing.Point(33, 146);
            this.LblReAccessApprent.Name = "LblReAccessApprent";
            this.LblReAccessApprent.Size = new System.Drawing.Size(193, 32);
            this.LblReAccessApprent.TabIndex = 55;
            this.LblReAccessApprent.Text = "Ist Auszubildende/r";
            this.LblReAccessApprent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LblReAccessAuthorizationBy
            // 
            this.LblReAccessAuthorizationBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblReAccessAuthorizationBy.Location = new System.Drawing.Point(645, 90);
            this.LblReAccessAuthorizationBy.Name = "LblReAccessAuthorizationBy";
            this.LblReAccessAuthorizationBy.Size = new System.Drawing.Size(48, 21);
            this.LblReAccessAuthorizationBy.TabIndex = 54;
            this.LblReAccessAuthorizationBy.Text = "durch";
            // 
            // TxtReAccessAuthorizationBy
            // 
            this.TxtReAccessAuthorizationBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtReAccessAuthorizationBy.Location = new System.Drawing.Point(730, 87);
            this.TxtReAccessAuthorizationBy.MaxLength = 30;
            this.TxtReAccessAuthorizationBy.Name = "TxtReAccessAuthorizationBy";
            this.TxtReAccessAuthorizationBy.ReadOnly = true;
            this.TxtReAccessAuthorizationBy.Size = new System.Drawing.Size(240, 21);
            this.TxtReAccessAuthorizationBy.TabIndex = 23;
            this.TxtReAccessAuthorizationBy.TabStop = false;
            // 
            // LblReSafetyInstructions
            // 
            this.LblReSafetyInstructions.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblReSafetyInstructions.Location = new System.Drawing.Point(32, 29);
            this.LblReSafetyInstructions.Name = "LblReSafetyInstructions";
            this.LblReSafetyInstructions.Size = new System.Drawing.Size(230, 56);
            this.LblReSafetyInstructions.TabIndex = 33;
            this.LblReSafetyInstructions.Text = "Sicherheitshinweis vom Empfang erhalten";
            this.LblReSafetyInstructions.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PnlReSafetyInstructions
            // 
            this.PnlReSafetyInstructions.Controls.Add(this.RbtReSafetyInstructionsNo);
            this.PnlReSafetyInstructions.Controls.Add(this.RbtReSafetyInstructionsYes);
            this.PnlReSafetyInstructions.Controls.Add(this.LblReSafetyInstructionsOn);
            this.PnlReSafetyInstructions.Controls.Add(this.DatReSafetyInstructionsOn);
            this.PnlReSafetyInstructions.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PnlReSafetyInstructions.Location = new System.Drawing.Point(268, 29);
            this.PnlReSafetyInstructions.Name = "PnlReSafetyInstructions";
            this.PnlReSafetyInstructions.Size = new System.Drawing.Size(327, 40);
            this.PnlReSafetyInstructions.TabIndex = 15;
            this.PnlReSafetyInstructions.TabStop = true;
            // 
            // RbtReSafetyInstructionsNo
            // 
            this.RbtReSafetyInstructionsNo.Checked = true;
            this.RbtReSafetyInstructionsNo.Enabled = false;
            this.RbtReSafetyInstructionsNo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtReSafetyInstructionsNo.Location = new System.Drawing.Point(76, 8);
            this.RbtReSafetyInstructionsNo.Name = "RbtReSafetyInstructionsNo";
            this.RbtReSafetyInstructionsNo.Size = new System.Drawing.Size(57, 24);
            this.RbtReSafetyInstructionsNo.TabIndex = 16;
            this.RbtReSafetyInstructionsNo.TabStop = true;
            this.RbtReSafetyInstructionsNo.Text = "Nein";
            this.RbtReSafetyInstructionsNo.CheckedChanged += new System.EventHandler(this.RbtReSafetyInstructionsNo_CheckedChanged);
            // 
            // RbtReSafetyInstructionsYes
            // 
            this.RbtReSafetyInstructionsYes.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtReSafetyInstructionsYes.Location = new System.Drawing.Point(20, 8);
            this.RbtReSafetyInstructionsYes.Name = "RbtReSafetyInstructionsYes";
            this.RbtReSafetyInstructionsYes.Size = new System.Drawing.Size(40, 24);
            this.RbtReSafetyInstructionsYes.TabIndex = 15;
            this.RbtReSafetyInstructionsYes.Text = "Ja";
            this.RbtReSafetyInstructionsYes.CheckedChanged += new System.EventHandler(this.RbtReSafetyInstructionsYes_CheckedChanged);
            // 
            // LblReSafetyInstructionsOn
            // 
            this.LblReSafetyInstructionsOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblReSafetyInstructionsOn.Location = new System.Drawing.Point(192, 13);
            this.LblReSafetyInstructionsOn.Name = "LblReSafetyInstructionsOn";
            this.LblReSafetyInstructionsOn.Size = new System.Drawing.Size(25, 19);
            this.LblReSafetyInstructionsOn.TabIndex = 35;
            this.LblReSafetyInstructionsOn.Text = "am";
            // 
            // DatReSafetyInstructionsOn
            // 
            this.DatReSafetyInstructionsOn.CalendarFont = new System.Drawing.Font("Arial", 9F);
            this.DatReSafetyInstructionsOn.CustomFormat = "dd.MM.yyyy";
            this.DatReSafetyInstructionsOn.Enabled = false;
            this.DatReSafetyInstructionsOn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatReSafetyInstructionsOn.Location = new System.Drawing.Point(223, 10);
            this.DatReSafetyInstructionsOn.Name = "DatReSafetyInstructionsOn";
            this.DatReSafetyInstructionsOn.Size = new System.Drawing.Size(88, 21);
            this.DatReSafetyInstructionsOn.TabIndex = 17;
            this.DatReSafetyInstructionsOn.Value = new System.DateTime(2003, 10, 8, 0, 0, 0, 0);
            this.DatReSafetyInstructionsOn.Leave += new System.EventHandler(this.DatReSafetyInstructionsOn_Leave_1);
            // 
            // LblReAccessAuthorization
            // 
            this.LblReAccessAuthorization.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblReAccessAuthorization.Location = new System.Drawing.Point(32, 85);
            this.LblReAccessAuthorization.Name = "LblReAccessAuthorization";
            this.LblReAccessAuthorization.Size = new System.Drawing.Size(194, 32);
            this.LblReAccessAuthorization.TabIndex = 48;
            this.LblReAccessAuthorization.Text = "Zutrittsberechtigung erteilt";
            this.LblReAccessAuthorization.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PnlReAccessAuthorization
            // 
            this.PnlReAccessAuthorization.Controls.Add(this.RbtReAccessAuthNo);
            this.PnlReAccessAuthorization.Controls.Add(this.RbtReAccessAuthYes);
            this.PnlReAccessAuthorization.Controls.Add(this.LblReAccessAuthorizationOn);
            this.PnlReAccessAuthorization.Controls.Add(this.DatReAccessAuthorizationOn);
            this.PnlReAccessAuthorization.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PnlReAccessAuthorization.Location = new System.Drawing.Point(268, 77);
            this.PnlReAccessAuthorization.Name = "PnlReAccessAuthorization";
            this.PnlReAccessAuthorization.Size = new System.Drawing.Size(327, 40);
            this.PnlReAccessAuthorization.TabIndex = 19;
            this.PnlReAccessAuthorization.TabStop = true;
            // 
            // RbtReAccessAuthNo
            // 
            this.RbtReAccessAuthNo.Checked = true;
            this.RbtReAccessAuthNo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtReAccessAuthNo.Location = new System.Drawing.Point(76, 8);
            this.RbtReAccessAuthNo.Name = "RbtReAccessAuthNo";
            this.RbtReAccessAuthNo.Size = new System.Drawing.Size(57, 24);
            this.RbtReAccessAuthNo.TabIndex = 19;
            this.RbtReAccessAuthNo.TabStop = true;
            this.RbtReAccessAuthNo.Text = "Nein";
            // 
            // RbtReAccessAuthYes
            // 
            this.RbtReAccessAuthYes.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtReAccessAuthYes.Location = new System.Drawing.Point(20, 8);
            this.RbtReAccessAuthYes.Name = "RbtReAccessAuthYes";
            this.RbtReAccessAuthYes.Size = new System.Drawing.Size(40, 24);
            this.RbtReAccessAuthYes.TabIndex = 18;
            this.RbtReAccessAuthYes.Text = "Ja";
            this.RbtReAccessAuthYes.CheckedChanged += new System.EventHandler(this.RbtReAccessAuthYes_CheckedChanged);
            // 
            // LblReAccessAuthorizationOn
            // 
            this.LblReAccessAuthorizationOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblReAccessAuthorizationOn.Location = new System.Drawing.Point(192, 13);
            this.LblReAccessAuthorizationOn.Name = "LblReAccessAuthorizationOn";
            this.LblReAccessAuthorizationOn.Size = new System.Drawing.Size(25, 16);
            this.LblReAccessAuthorizationOn.TabIndex = 53;
            this.LblReAccessAuthorizationOn.Text = "am";
            // 
            // DatReAccessAuthorizationOn
            // 
            this.DatReAccessAuthorizationOn.CustomFormat = "dd.MM.yyyy";
            this.DatReAccessAuthorizationOn.Enabled = false;
            this.DatReAccessAuthorizationOn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatReAccessAuthorizationOn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.DatReAccessAuthorizationOn.Location = new System.Drawing.Point(223, 10);
            this.DatReAccessAuthorizationOn.Name = "DatReAccessAuthorizationOn";
            this.DatReAccessAuthorizationOn.Size = new System.Drawing.Size(88, 21);
            this.DatReAccessAuthorizationOn.TabIndex = 20;
            this.DatReAccessAuthorizationOn.Value = new System.DateTime(2003, 10, 6, 0, 0, 0, 0);
            this.DatReAccessAuthorizationOn.Leave += new System.EventHandler(this.DatReAccessAuthorizationOn_Leave_1);
            // 
            // LblReSafetyInstructionsBy
            // 
            this.LblReSafetyInstructionsBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblReSafetyInstructionsBy.Location = new System.Drawing.Point(645, 42);
            this.LblReSafetyInstructionsBy.Name = "LblReSafetyInstructionsBy";
            this.LblReSafetyInstructionsBy.Size = new System.Drawing.Size(48, 21);
            this.LblReSafetyInstructionsBy.TabIndex = 36;
            this.LblReSafetyInstructionsBy.Text = "durch";
            // 
            // TxtReSafetyInstructionsBy
            // 
            this.TxtReSafetyInstructionsBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtReSafetyInstructionsBy.Location = new System.Drawing.Point(730, 39);
            this.TxtReSafetyInstructionsBy.MaxLength = 30;
            this.TxtReSafetyInstructionsBy.Name = "TxtReSafetyInstructionsBy";
            this.TxtReSafetyInstructionsBy.ReadOnly = true;
            this.TxtReSafetyInstructionsBy.Size = new System.Drawing.Size(240, 21);
            this.TxtReSafetyInstructionsBy.TabIndex = 18;
            this.TxtReSafetyInstructionsBy.TabStop = false;
            // 
            // PnlReIdentityCard
            // 
            this.PnlReIdentityCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlReIdentityCard.Controls.Add(this.grpRe1);
            this.PnlReIdentityCard.Controls.Add(this.BtnDelPassNumber);
            this.PnlReIdentityCard.Controls.Add(this.lblReIDCardHd);
            this.PnlReIdentityCard.Controls.Add(this.LblReDelPassNr);
            this.PnlReIdentityCard.Controls.Add(this.lblReIDCardHint);
            this.PnlReIdentityCard.Controls.Add(this.BtnRePassNrMifareUSB);
            this.PnlReIdentityCard.Controls.Add(this.LblReHitag2Nr);
            this.PnlReIdentityCard.Controls.Add(this.BtnRePassNrHitagUSB);
            this.PnlReIdentityCard.Controls.Add(this.TxtReIDCardNumHitag2);
            this.PnlReIdentityCard.Controls.Add(this.LblReMifareNr);
            this.PnlReIdentityCard.Controls.Add(this.BtnRePassNrHitag);
            this.PnlReIdentityCard.Controls.Add(this.BtnRePassNrMifare);
            this.PnlReIdentityCard.Controls.Add(this.TxtReIDCardNumMifareNo);
            this.PnlReIdentityCard.Location = new System.Drawing.Point(3, 384);
            this.PnlReIdentityCard.Name = "PnlReIdentityCard";
            this.PnlReIdentityCard.Size = new System.Drawing.Size(617, 198);
            this.PnlReIdentityCard.TabIndex = 0;
            // 
            // grpRe1
            // 
            this.grpRe1.Location = new System.Drawing.Point(7, 135);
            this.grpRe1.Name = "grpRe1";
            this.grpRe1.Size = new System.Drawing.Size(597, 10);
            this.grpRe1.TabIndex = 83;
            this.grpRe1.TabStop = false;
            // 
            // BtnDelPassNumber
            // 
            this.BtnDelPassNumber.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelPassNumber.Location = new System.Drawing.Point(374, 155);
            this.BtnDelPassNumber.Name = "BtnDelPassNumber";
            this.BtnDelPassNumber.Size = new System.Drawing.Size(136, 25);
            this.BtnDelPassNumber.TabIndex = 31;
            this.BtnDelPassNumber.Tag = "";
            this.BtnDelPassNumber.Text = "Ausweise &entfernen";
            this.TooIdentityCard.SetToolTip(this.BtnDelPassNumber, "Entfernt die Ausweise im ZKS und setzt das GültigBis-Datum des FFMA auf jetzt.");
            this.BtnDelPassNumber.Click += new System.EventHandler(this.BtnDelPassNumber_Click);
            // 
            // lblReIDCardHd
            // 
            this.lblReIDCardHd.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReIDCardHd.Location = new System.Drawing.Point(13, 8);
            this.lblReIDCardHd.Name = "lblReIDCardHd";
            this.lblReIDCardHd.Size = new System.Drawing.Size(130, 23);
            this.lblReIDCardHd.TabIndex = 82;
            this.lblReIDCardHd.Text = "Ausweise zuordnen";
            this.lblReIDCardHd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LblReDelPassNr
            // 
            this.LblReDelPassNr.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblReDelPassNr.Location = new System.Drawing.Point(28, 160);
            this.LblReDelPassNr.Name = "LblReDelPassNr";
            this.LblReDelPassNr.Size = new System.Drawing.Size(222, 23);
            this.LblReDelPassNr.TabIndex = 78;
            this.LblReDelPassNr.Text = "entfernt beide Ausweise im ZKS";
            // 
            // lblReIDCardHint
            // 
            this.lblReIDCardHint.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReIDCardHint.Location = new System.Drawing.Point(28, 40);
            this.lblReIDCardHint.Name = "lblReIDCardHint";
            this.lblReIDCardHint.Size = new System.Drawing.Size(445, 23);
            this.lblReIDCardHint.TabIndex = 80;
            this.lblReIDCardHint.Text = "ordnet dem aktuellen FFMA  Ausweise zu.";
            this.lblReIDCardHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BtnRePassNrMifareUSB
            // 
            this.BtnRePassNrMifareUSB.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnRePassNrMifareUSB.Location = new System.Drawing.Point(410, 106);
            this.BtnRePassNrMifareUSB.Name = "BtnRePassNrMifareUSB";
            this.BtnRePassNrMifareUSB.Size = new System.Drawing.Size(100, 25);
            this.BtnRePassNrMifareUSB.TabIndex = 30;
            this.BtnRePassNrMifareUSB.Tag = "";
            this.BtnRePassNrMifareUSB.Text = "USB-Leser";
            this.TooIdentityCard.SetToolTip(this.BtnRePassNrMifareUSB, "...");
            this.BtnRePassNrMifareUSB.Click += new System.EventHandler(this.BtnRePassNrMifareUSB_Click);
            // 
            // LblReHitag2Nr
            // 
            this.LblReHitag2Nr.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblReHitag2Nr.Location = new System.Drawing.Point(28, 77);
            this.LblReHitag2Nr.Name = "LblReHitag2Nr";
            this.LblReHitag2Nr.Size = new System.Drawing.Size(68, 23);
            this.LblReHitag2Nr.TabIndex = 62;
            this.LblReHitag2Nr.Text = "Hitag2-Nr.";
            // 
            // BtnRePassNrHitagUSB
            // 
            this.BtnRePassNrHitagUSB.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnRePassNrHitagUSB.Location = new System.Drawing.Point(410, 74);
            this.BtnRePassNrHitagUSB.Name = "BtnRePassNrHitagUSB";
            this.BtnRePassNrHitagUSB.Size = new System.Drawing.Size(100, 25);
            this.BtnRePassNrHitagUSB.TabIndex = 27;
            this.BtnRePassNrHitagUSB.Tag = "";
            this.BtnRePassNrHitagUSB.Text = "USB-Leser";
            this.TooIdentityCard.SetToolTip(this.BtnRePassNrHitagUSB, "...");
            this.BtnRePassNrHitagUSB.Click += new System.EventHandler(this.BtnRePassNrHitagUSB_Click);
            // 
            // TxtReIDCardNumHitag2
            // 
            this.TxtReIDCardNumHitag2.Enabled = false;
            this.TxtReIDCardNumHitag2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtReIDCardNumHitag2.Location = new System.Drawing.Point(124, 76);
            this.TxtReIDCardNumHitag2.Name = "TxtReIDCardNumHitag2";
            this.TxtReIDCardNumHitag2.Size = new System.Drawing.Size(163, 21);
            this.TxtReIDCardNumHitag2.TabIndex = 25;
            this.TxtReIDCardNumHitag2.TabStop = false;
            // 
            // LblReMifareNr
            // 
            this.LblReMifareNr.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblReMifareNr.Location = new System.Drawing.Point(28, 109);
            this.LblReMifareNr.Name = "LblReMifareNr";
            this.LblReMifareNr.Size = new System.Drawing.Size(68, 23);
            this.LblReMifareNr.TabIndex = 62;
            this.LblReMifareNr.Text = "Mifare-Nr.";
            // 
            // BtnRePassNrHitag
            // 
            this.BtnRePassNrHitag.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnRePassNrHitag.Location = new System.Drawing.Point(302, 74);
            this.BtnRePassNrHitag.Name = "BtnRePassNrHitag";
            this.BtnRePassNrHitag.Size = new System.Drawing.Size(100, 25);
            this.BtnRePassNrHitag.TabIndex = 26;
            this.BtnRePassNrHitag.Tag = "";
            this.BtnRePassNrHitag.Text = "ZKS-Terminal";
            this.TooIdentityCard.SetToolTip(this.BtnRePassNrHitag, "Ausweisnummer aus Ausweisterminal auslesen");
            this.BtnRePassNrHitag.Click += new System.EventHandler(this.BtnRePassNrHitag_Click);
            // 
            // BtnRePassNrMifare
            // 
            this.BtnRePassNrMifare.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnRePassNrMifare.Location = new System.Drawing.Point(302, 106);
            this.BtnRePassNrMifare.Name = "BtnRePassNrMifare";
            this.BtnRePassNrMifare.Size = new System.Drawing.Size(100, 25);
            this.BtnRePassNrMifare.TabIndex = 29;
            this.BtnRePassNrMifare.Tag = "";
            this.BtnRePassNrMifare.Text = "ZKS-Terminal";
            this.TooIdentityCard.SetToolTip(this.BtnRePassNrMifare, "Ausweisnummer aus Ausweisterminal auslesen");
            this.BtnRePassNrMifare.Click += new System.EventHandler(this.BtnRePassNrMifare_Click);
            // 
            // TxtReIDCardNumMifareNo
            // 
            this.TxtReIDCardNumMifareNo.Enabled = false;
            this.TxtReIDCardNumMifareNo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtReIDCardNumMifareNo.Location = new System.Drawing.Point(124, 106);
            this.TxtReIDCardNumMifareNo.Name = "TxtReIDCardNumMifareNo";
            this.TxtReIDCardNumMifareNo.Size = new System.Drawing.Size(163, 21);
            this.TxtReIDCardNumMifareNo.TabIndex = 28;
            this.TxtReIDCardNumMifareNo.TabStop = false;
            // 
            // BtnReClear
            // 
            this.BtnReClear.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnReClear.Location = new System.Drawing.Point(1103, 606);
            this.BtnReClear.Name = "BtnReClear";
            this.BtnReClear.Size = new System.Drawing.Size(136, 25);
            this.BtnReClear.TabIndex = 24;
            this.BtnReClear.Text = "&Maske leeren";
            this.TooClearMask.SetToolTip(this.BtnReClear, "Verwirft alle bisher eingegebenen Daten");
            this.BtnReClear.Click += new System.EventHandler(this.BtnReClear_Click);
            // 
            // TapCoordinator
            // 
            this.TapCoordinator.Controls.Add(this.PnlTabCoordinator);
            this.TapCoordinator.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TapCoordinator.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.TapCoordinator.Location = new System.Drawing.Point(4, 25);
            this.TapCoordinator.Name = "TapCoordinator";
            this.TapCoordinator.Size = new System.Drawing.Size(1249, 660);
            this.TapCoordinator.TabIndex = 1;
            this.TapCoordinator.Text = "Koordinator";
            // 
            // PnlTabCoordinator
            // 
            this.PnlTabCoordinator.Controls.Add(this.PnlCoSmartAct);
            this.PnlTabCoordinator.Controls.Add(this.PnlCoVehicleEntrance);
            this.PnlTabCoordinator.Controls.Add(this.PnlCoCheckInCheckOff);
            this.PnlTabCoordinator.Controls.Add(this.PnlCoDepartmentalBriefing);
            this.PnlTabCoordinator.Controls.Add(this.PnlCoData);
            this.PnlTabCoordinator.Controls.Add(this.PnlCoPrecautionaryMedical);
            this.PnlTabCoordinator.Controls.Add(this.PnlCoRespiratoryMaskBriefing);
            this.PnlTabCoordinator.Controls.Add(this.PnlCoIndustrialSafetyBriefingSite);
            this.PnlTabCoordinator.Controls.Add(this.PnlCoBriefing);
            this.PnlTabCoordinator.Enabled = false;
            this.PnlTabCoordinator.Location = new System.Drawing.Point(0, 0);
            this.PnlTabCoordinator.Name = "PnlTabCoordinator";
            this.PnlTabCoordinator.Size = new System.Drawing.Size(1246, 657);
            this.PnlTabCoordinator.TabIndex = 0;
            // 
            // PnlCoSmartAct
            // 
            this.PnlCoSmartAct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlCoSmartAct.Controls.Add(this.GrpCoBrDummy);
            this.PnlCoSmartAct.Controls.Add(this.label8);
            this.PnlCoSmartAct.Controls.Add(this.BtnCoSmartActDel);
            this.PnlCoSmartAct.Controls.Add(this.LblCoIdPhotoTitle);
            this.PnlCoSmartAct.Controls.Add(this.LblCoSmartActExp);
            this.PnlCoSmartAct.Controls.Add(this.BtnCoSmartActExp);
            this.PnlCoSmartAct.Controls.Add(this.PnlCoPhotoCard);
            this.PnlCoSmartAct.Controls.Add(this.LblCoADSSearch);
            this.PnlCoSmartAct.Controls.Add(this.BtnCoADSSearch);
            this.PnlCoSmartAct.Controls.Add(this.LblCoPKI);
            this.PnlCoSmartAct.Controls.Add(this.TxtCoFpassNo);
            this.PnlCoSmartAct.Controls.Add(this.CbxCoPKI);
            this.PnlCoSmartAct.Controls.Add(this.TxtCoSmartActNo);
            this.PnlCoSmartAct.Controls.Add(this.LblCoFpassNo);
            this.PnlCoSmartAct.Controls.Add(this.LblCoSmartActNo);
            this.PnlCoSmartAct.Controls.Add(this.LblCoWindowsID);
            this.PnlCoSmartAct.Controls.Add(this.TxtCoWindowsID);
            this.PnlCoSmartAct.Controls.Add(this.LblCoPhone);
            this.PnlCoSmartAct.Controls.Add(this.TxtCoPhone);
            this.PnlCoSmartAct.Location = new System.Drawing.Point(8, 7);
            this.PnlCoSmartAct.Name = "PnlCoSmartAct";
            this.PnlCoSmartAct.Size = new System.Drawing.Size(1232, 164);
            this.PnlCoSmartAct.TabIndex = 103;
            // 
            // GrpCoBrDummy
            // 
            this.GrpCoBrDummy.Location = new System.Drawing.Point(867, 78);
            this.GrpCoBrDummy.Name = "GrpCoBrDummy";
            this.GrpCoBrDummy.Size = new System.Drawing.Size(335, 5);
            this.GrpCoBrDummy.TabIndex = 115;
            this.GrpCoBrDummy.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 9F);
            this.label8.Location = new System.Drawing.Point(877, 98);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(155, 15);
            this.label8.TabIndex = 114;
            this.label8.Text = "Lichtbildausweis entfernen";
            // 
            // BtnCoSmartActDel
            // 
            this.BtnCoSmartActDel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCoSmartActDel.Location = new System.Drawing.Point(1099, 93);
            this.BtnCoSmartActDel.Name = "BtnCoSmartActDel";
            this.BtnCoSmartActDel.Size = new System.Drawing.Size(92, 25);
            this.BtnCoSmartActDel.TabIndex = 113;
            this.BtnCoSmartActDel.Text = "Ent&fernen";
            this.TooClearMask.SetToolTip(this.BtnCoSmartActDel, "Löscht den Lichtbildausweis des aktuellen FFMA");
            this.BtnCoSmartActDel.Click += new System.EventHandler(this.BtnCoSmartActDel_Click);
            // 
            // LblCoIdPhotoTitle
            // 
            this.LblCoIdPhotoTitle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCoIdPhotoTitle.Location = new System.Drawing.Point(13, 8);
            this.LblCoIdPhotoTitle.Name = "LblCoIdPhotoTitle";
            this.LblCoIdPhotoTitle.Size = new System.Drawing.Size(295, 19);
            this.LblCoIdPhotoTitle.TabIndex = 112;
            this.LblCoIdPhotoTitle.Text = "Lichtbildausweis";
            // 
            // LblCoSmartActExp
            // 
            this.LblCoSmartActExp.AutoSize = true;
            this.LblCoSmartActExp.Font = new System.Drawing.Font("Arial", 9F);
            this.LblCoSmartActExp.Location = new System.Drawing.Point(877, 49);
            this.LblCoSmartActExp.Name = "LblCoSmartActExp";
            this.LblCoSmartActExp.Size = new System.Drawing.Size(216, 15);
            this.LblCoSmartActExp.TabIndex = 88;
            this.LblCoSmartActExp.Text = "FFMA sofort nach SmartAct exportieren";
            // 
            // BtnCoSmartActExp
            // 
            this.BtnCoSmartActExp.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCoSmartActExp.Location = new System.Drawing.Point(1099, 44);
            this.BtnCoSmartActExp.Name = "BtnCoSmartActExp";
            this.BtnCoSmartActExp.Size = new System.Drawing.Size(92, 25);
            this.BtnCoSmartActExp.TabIndex = 87;
            this.BtnCoSmartActExp.Text = "&Exportieren";
            this.TooClearMask.SetToolTip(this.BtnCoSmartActExp, "Export den aktuellen FFMA sofort nach SmartAct");
            this.BtnCoSmartActExp.Click += new System.EventHandler(this.BtnCoSmartActExp_Click);
            // 
            // PnlCoPhotoCard
            // 
            this.PnlCoPhotoCard.Controls.Add(this.RbtCoIdPhotoSmActNo);
            this.PnlCoPhotoCard.Controls.Add(this.RbtCoIdPhotoSmActYes);
            this.PnlCoPhotoCard.Controls.Add(this.LblCoIdCardPhotoSmAct);
            this.PnlCoPhotoCard.Location = new System.Drawing.Point(13, 28);
            this.PnlCoPhotoCard.Name = "PnlCoPhotoCard";
            this.PnlCoPhotoCard.Size = new System.Drawing.Size(415, 35);
            this.PnlCoPhotoCard.TabIndex = 16;
            this.PnlCoPhotoCard.TabStop = true;
            // 
            // RbtCoIdPhotoSmActNo
            // 
            this.RbtCoIdPhotoSmActNo.Checked = true;
            this.RbtCoIdPhotoSmActNo.Font = new System.Drawing.Font("Arial", 9F);
            this.RbtCoIdPhotoSmActNo.Location = new System.Drawing.Point(241, 6);
            this.RbtCoIdPhotoSmActNo.Name = "RbtCoIdPhotoSmActNo";
            this.RbtCoIdPhotoSmActNo.Size = new System.Drawing.Size(61, 24);
            this.RbtCoIdPhotoSmActNo.TabIndex = 2;
            this.RbtCoIdPhotoSmActNo.TabStop = true;
            this.RbtCoIdPhotoSmActNo.Text = "Nein";
            this.RbtCoIdPhotoSmActNo.CheckedChanged += new System.EventHandler(this.RbtCoIdPhotoSmActNo_CheckedChanged);
            // 
            // RbtCoIdPhotoSmActYes
            // 
            this.RbtCoIdPhotoSmActYes.Font = new System.Drawing.Font("Arial", 9F);
            this.RbtCoIdPhotoSmActYes.Location = new System.Drawing.Point(193, 6);
            this.RbtCoIdPhotoSmActYes.Name = "RbtCoIdPhotoSmActYes";
            this.RbtCoIdPhotoSmActYes.Size = new System.Drawing.Size(56, 24);
            this.RbtCoIdPhotoSmActYes.TabIndex = 1;
            this.RbtCoIdPhotoSmActYes.TabStop = true;
            this.RbtCoIdPhotoSmActYes.Text = "Ja";
            // 
            // LblCoIdCardPhotoSmAct
            // 
            this.LblCoIdCardPhotoSmAct.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCoIdCardPhotoSmAct.Location = new System.Drawing.Point(8, 9);
            this.LblCoIdCardPhotoSmAct.Name = "LblCoIdCardPhotoSmAct";
            this.LblCoIdCardPhotoSmAct.Size = new System.Drawing.Size(180, 19);
            this.LblCoIdCardPhotoSmAct.TabIndex = 111;
            this.LblCoIdCardPhotoSmAct.Text = "Lichtbildausweis gewünscht";
            // 
            // LblCoADSSearch
            // 
            this.LblCoADSSearch.AutoSize = true;
            this.LblCoADSSearch.Font = new System.Drawing.Font("Arial", 9F);
            this.LblCoADSSearch.Location = new System.Drawing.Point(21, 127);
            this.LblCoADSSearch.Name = "LblCoADSSearch";
            this.LblCoADSSearch.Size = new System.Drawing.Size(108, 15);
            this.LblCoADSSearch.TabIndex = 86;
            this.LblCoADSSearch.Text = "KonzernID suchen";
            // 
            // BtnCoADSSearch
            // 
            this.BtnCoADSSearch.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCoADSSearch.Location = new System.Drawing.Point(159, 124);
            this.BtnCoADSSearch.Name = "BtnCoADSSearch";
            this.BtnCoADSSearch.Size = new System.Drawing.Size(92, 25);
            this.BtnCoADSSearch.TabIndex = 5;
            this.BtnCoADSSearch.Text = "&Suchen";
            this.TooClearMask.SetToolTip(this.BtnCoADSSearch, "Führt eine Suche im Windows Active Directory aus");
            this.BtnCoADSSearch.Click += new System.EventHandler(this.BtnCoSearchADS_Click);
            // 
            // LblCoPKI
            // 
            this.LblCoPKI.AutoSize = true;
            this.LblCoPKI.Font = new System.Drawing.Font("Arial", 9F);
            this.LblCoPKI.Location = new System.Drawing.Point(21, 69);
            this.LblCoPKI.Name = "LblCoPKI";
            this.LblCoPKI.Size = new System.Drawing.Size(75, 15);
            this.LblCoPKI.TabIndex = 76;
            this.LblCoPKI.Text = "mit PKI Chip";
            // 
            // TxtCoFpassNo
            // 
            this.TxtCoFpassNo.Font = new System.Drawing.Font("Arial", 9F);
            this.TxtCoFpassNo.Location = new System.Drawing.Point(628, 70);
            this.TxtCoFpassNo.Name = "TxtCoFpassNo";
            this.TxtCoFpassNo.ReadOnly = true;
            this.TxtCoFpassNo.Size = new System.Drawing.Size(210, 21);
            this.TxtCoFpassNo.TabIndex = 7;
            // 
            // CbxCoPKI
            // 
            this.CbxCoPKI.AutoSize = true;
            this.CbxCoPKI.Location = new System.Drawing.Point(159, 69);
            this.CbxCoPKI.Name = "CbxCoPKI";
            this.CbxCoPKI.Size = new System.Drawing.Size(15, 14);
            this.CbxCoPKI.TabIndex = 3;
            this.CbxCoPKI.UseVisualStyleBackColor = true;
            this.CbxCoPKI.CheckedChanged += new System.EventHandler(this.CbxCoPKI_CheckedChanged);
            // 
            // TxtCoSmartActNo
            // 
            this.TxtCoSmartActNo.Font = new System.Drawing.Font("Arial", 9F);
            this.TxtCoSmartActNo.Location = new System.Drawing.Point(628, 100);
            this.TxtCoSmartActNo.Name = "TxtCoSmartActNo";
            this.TxtCoSmartActNo.ReadOnly = true;
            this.TxtCoSmartActNo.Size = new System.Drawing.Size(210, 21);
            this.TxtCoSmartActNo.TabIndex = 8;
            // 
            // LblCoFpassNo
            // 
            this.LblCoFpassNo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCoFpassNo.Location = new System.Drawing.Point(469, 72);
            this.LblCoFpassNo.Name = "LblCoFpassNo";
            this.LblCoFpassNo.Size = new System.Drawing.Size(125, 23);
            this.LblCoFpassNo.TabIndex = 83;
            this.LblCoFpassNo.Text = "FPASS Personalnr.";
            // 
            // LblCoSmartActNo
            // 
            this.LblCoSmartActNo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCoSmartActNo.Location = new System.Drawing.Point(469, 100);
            this.LblCoSmartActNo.Name = "LblCoSmartActNo";
            this.LblCoSmartActNo.Size = new System.Drawing.Size(127, 23);
            this.LblCoSmartActNo.TabIndex = 82;
            this.LblCoSmartActNo.Text = "SmartAct Personalnr.";
            // 
            // LblCoWindowsID
            // 
            this.LblCoWindowsID.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCoWindowsID.Location = new System.Drawing.Point(21, 97);
            this.LblCoWindowsID.Name = "LblCoWindowsID";
            this.LblCoWindowsID.Size = new System.Drawing.Size(72, 23);
            this.LblCoWindowsID.TabIndex = 81;
            this.LblCoWindowsID.Text = "KonzernID";
            // 
            // TxtCoWindowsID
            // 
            this.TxtCoWindowsID.Font = new System.Drawing.Font("Arial", 9F);
            this.TxtCoWindowsID.Location = new System.Drawing.Point(159, 97);
            this.TxtCoWindowsID.Name = "TxtCoWindowsID";
            this.TxtCoWindowsID.Size = new System.Drawing.Size(250, 21);
            this.TxtCoWindowsID.TabIndex = 4;
            // 
            // LblCoPhone
            // 
            this.LblCoPhone.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCoPhone.Location = new System.Drawing.Point(469, 43);
            this.LblCoPhone.Name = "LblCoPhone";
            this.LblCoPhone.Size = new System.Drawing.Size(73, 23);
            this.LblCoPhone.TabIndex = 79;
            this.LblCoPhone.Text = "Telefonnr.";
            // 
            // TxtCoPhone
            // 
            this.TxtCoPhone.Font = new System.Drawing.Font("Arial", 9F);
            this.TxtCoPhone.Location = new System.Drawing.Point(628, 40);
            this.TxtCoPhone.Name = "TxtCoPhone";
            this.TxtCoPhone.Size = new System.Drawing.Size(210, 21);
            this.TxtCoPhone.TabIndex = 6;
            // 
            // PnlCoVehicleEntrance
            // 
            this.PnlCoVehicleEntrance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlCoVehicleEntrance.Controls.Add(this.PnlCoVehicleEntranceLong);
            this.PnlCoVehicleEntrance.Controls.Add(this.PnlCoVehicleEntranceShort);
            this.PnlCoVehicleEntrance.Controls.Add(this.LblCoVehicleEntranceLong);
            this.PnlCoVehicleEntrance.Controls.Add(this.LblCoVehicleEntranceShort);
            this.PnlCoVehicleEntrance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PnlCoVehicleEntrance.Location = new System.Drawing.Point(876, 422);
            this.PnlCoVehicleEntrance.Name = "PnlCoVehicleEntrance";
            this.PnlCoVehicleEntrance.Size = new System.Drawing.Size(364, 82);
            this.PnlCoVehicleEntrance.TabIndex = 26;
            this.PnlCoVehicleEntrance.TabStop = true;
            // 
            // PnlCoVehicleEntranceLong
            // 
            this.PnlCoVehicleEntranceLong.Controls.Add(this.RbtCoVehicleEntranceLongNo);
            this.PnlCoVehicleEntranceLong.Controls.Add(this.RbtCoVehicleEntranceLongYes);
            this.PnlCoVehicleEntranceLong.Location = new System.Drawing.Point(157, 42);
            this.PnlCoVehicleEntranceLong.Name = "PnlCoVehicleEntranceLong";
            this.PnlCoVehicleEntranceLong.Size = new System.Drawing.Size(200, 24);
            this.PnlCoVehicleEntranceLong.TabIndex = 28;
            // 
            // RbtCoVehicleEntranceLongNo
            // 
            this.RbtCoVehicleEntranceLongNo.Checked = true;
            this.RbtCoVehicleEntranceLongNo.Location = new System.Drawing.Point(140, 5);
            this.RbtCoVehicleEntranceLongNo.Name = "RbtCoVehicleEntranceLongNo";
            this.RbtCoVehicleEntranceLongNo.Size = new System.Drawing.Size(64, 24);
            this.RbtCoVehicleEntranceLongNo.TabIndex = 2;
            this.RbtCoVehicleEntranceLongNo.TabStop = true;
            this.RbtCoVehicleEntranceLongNo.Text = "Nein";
            this.RbtCoVehicleEntranceLongNo.CheckedChanged += new System.EventHandler(this.RbtCoVehicleEntranceLongNo_CheckedChanged);
            // 
            // RbtCoVehicleEntranceLongYes
            // 
            this.RbtCoVehicleEntranceLongYes.Location = new System.Drawing.Point(100, 5);
            this.RbtCoVehicleEntranceLongYes.Name = "RbtCoVehicleEntranceLongYes";
            this.RbtCoVehicleEntranceLongYes.Size = new System.Drawing.Size(40, 24);
            this.RbtCoVehicleEntranceLongYes.TabIndex = 1;
            this.RbtCoVehicleEntranceLongYes.Text = "Ja";
            this.RbtCoVehicleEntranceLongYes.CheckedChanged += new System.EventHandler(this.RbtCoVehicleEntranceLongYes_CheckedChanged);
            // 
            // PnlCoVehicleEntranceShort
            // 
            this.PnlCoVehicleEntranceShort.Controls.Add(this.RbtCoVehicleEntranceShortNo);
            this.PnlCoVehicleEntranceShort.Controls.Add(this.RbtCoVehicleEntranceShortYes);
            this.PnlCoVehicleEntranceShort.Location = new System.Drawing.Point(157, 10);
            this.PnlCoVehicleEntranceShort.Name = "PnlCoVehicleEntranceShort";
            this.PnlCoVehicleEntranceShort.Size = new System.Drawing.Size(190, 24);
            this.PnlCoVehicleEntranceShort.TabIndex = 27;
            // 
            // RbtCoVehicleEntranceShortNo
            // 
            this.RbtCoVehicleEntranceShortNo.Checked = true;
            this.RbtCoVehicleEntranceShortNo.Location = new System.Drawing.Point(140, 1);
            this.RbtCoVehicleEntranceShortNo.Name = "RbtCoVehicleEntranceShortNo";
            this.RbtCoVehicleEntranceShortNo.Size = new System.Drawing.Size(64, 24);
            this.RbtCoVehicleEntranceShortNo.TabIndex = 2;
            this.RbtCoVehicleEntranceShortNo.TabStop = true;
            this.RbtCoVehicleEntranceShortNo.Text = "Nein";
            this.RbtCoVehicleEntranceShortNo.CheckedChanged += new System.EventHandler(this.RbtCoVehicleEntranceShortNo_CheckedChanged);
            // 
            // RbtCoVehicleEntranceShortYes
            // 
            this.RbtCoVehicleEntranceShortYes.Location = new System.Drawing.Point(100, 1);
            this.RbtCoVehicleEntranceShortYes.Name = "RbtCoVehicleEntranceShortYes";
            this.RbtCoVehicleEntranceShortYes.Size = new System.Drawing.Size(40, 24);
            this.RbtCoVehicleEntranceShortYes.TabIndex = 1;
            this.RbtCoVehicleEntranceShortYes.Text = "Ja";
            this.RbtCoVehicleEntranceShortYes.CheckedChanged += new System.EventHandler(this.RbtCoVehicleEntranceShortYes_CheckedChanged);
            // 
            // LblCoVehicleEntranceLong
            // 
            this.LblCoVehicleEntranceLong.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCoVehicleEntranceLong.Location = new System.Drawing.Point(8, 47);
            this.LblCoVehicleEntranceLong.Name = "LblCoVehicleEntranceLong";
            this.LblCoVehicleEntranceLong.Size = new System.Drawing.Size(156, 16);
            this.LblCoVehicleEntranceLong.TabIndex = 79;
            this.LblCoVehicleEntranceLong.Text = "Kfz-Einfahrt lang";
            // 
            // LblCoVehicleEntranceShort
            // 
            this.LblCoVehicleEntranceShort.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCoVehicleEntranceShort.Location = new System.Drawing.Point(8, 15);
            this.LblCoVehicleEntranceShort.Name = "LblCoVehicleEntranceShort";
            this.LblCoVehicleEntranceShort.Size = new System.Drawing.Size(156, 16);
            this.LblCoVehicleEntranceShort.TabIndex = 76;
            this.LblCoVehicleEntranceShort.Text = "Kfz-Einfahrt kurz";
            // 
            // PnlCoCheckInCheckOff
            // 
            this.PnlCoCheckInCheckOff.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlCoCheckInCheckOff.Controls.Add(this.TxtCoCheckOff);
            this.PnlCoCheckInCheckOff.Controls.Add(this.TxtCoCheckIn);
            this.PnlCoCheckInCheckOff.Controls.Add(this.RbtCoOrderDoneNo);
            this.PnlCoCheckInCheckOff.Controls.Add(this.RbtCoOrderDoneYes);
            this.PnlCoCheckInCheckOff.Controls.Add(this.LblCoOrderDone);
            this.PnlCoCheckInCheckOff.Controls.Add(this.LblCoCheckOff);
            this.PnlCoCheckInCheckOff.Controls.Add(this.LblCoCheckIn);
            this.PnlCoCheckInCheckOff.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PnlCoCheckInCheckOff.Location = new System.Drawing.Point(876, 512);
            this.PnlCoCheckInCheckOff.Name = "PnlCoCheckInCheckOff";
            this.PnlCoCheckInCheckOff.Size = new System.Drawing.Size(364, 138);
            this.PnlCoCheckInCheckOff.TabIndex = 102;
            this.PnlCoCheckInCheckOff.TabStop = true;
            // 
            // TxtCoCheckOff
            // 
            this.TxtCoCheckOff.Location = new System.Drawing.Point(196, 39);
            this.TxtCoCheckOff.Name = "TxtCoCheckOff";
            this.TxtCoCheckOff.Size = new System.Drawing.Size(155, 21);
            this.TxtCoCheckOff.TabIndex = 30;
            this.TxtCoCheckOff.Leave += new System.EventHandler(this.TxtCoCheckOff_Leave);
            // 
            // TxtCoCheckIn
            // 
            this.TxtCoCheckIn.Location = new System.Drawing.Point(196, 11);
            this.TxtCoCheckIn.Name = "TxtCoCheckIn";
            this.TxtCoCheckIn.ReadOnly = true;
            this.TxtCoCheckIn.Size = new System.Drawing.Size(155, 21);
            this.TxtCoCheckIn.TabIndex = 29;
            // 
            // RbtCoOrderDoneNo
            // 
            this.RbtCoOrderDoneNo.Checked = true;
            this.RbtCoOrderDoneNo.Location = new System.Drawing.Point(292, 67);
            this.RbtCoOrderDoneNo.Name = "RbtCoOrderDoneNo";
            this.RbtCoOrderDoneNo.Size = new System.Drawing.Size(64, 24);
            this.RbtCoOrderDoneNo.TabIndex = 32;
            this.RbtCoOrderDoneNo.TabStop = true;
            this.RbtCoOrderDoneNo.Text = "Nein";
            this.RbtCoOrderDoneNo.CheckedChanged += new System.EventHandler(this.RbtCoOrderDoneNo_CheckedChanged);
            // 
            // RbtCoOrderDoneYes
            // 
            this.RbtCoOrderDoneYes.Location = new System.Drawing.Point(252, 67);
            this.RbtCoOrderDoneYes.Name = "RbtCoOrderDoneYes";
            this.RbtCoOrderDoneYes.Size = new System.Drawing.Size(40, 24);
            this.RbtCoOrderDoneYes.TabIndex = 31;
            this.RbtCoOrderDoneYes.Text = "Ja";
            // 
            // LblCoOrderDone
            // 
            this.LblCoOrderDone.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCoOrderDone.Location = new System.Drawing.Point(8, 69);
            this.LblCoOrderDone.Name = "LblCoOrderDone";
            this.LblCoOrderDone.Size = new System.Drawing.Size(156, 16);
            this.LblCoOrderDone.TabIndex = 82;
            this.LblCoOrderDone.Text = "Auftrag erledigt";
            // 
            // LblCoCheckOff
            // 
            this.LblCoCheckOff.Location = new System.Drawing.Point(8, 41);
            this.LblCoCheckOff.Name = "LblCoCheckOff";
            this.LblCoCheckOff.Size = new System.Drawing.Size(104, 16);
            this.LblCoCheckOff.TabIndex = 1;
            this.LblCoCheckOff.Text = "Abmeldung am";
            // 
            // LblCoCheckIn
            // 
            this.LblCoCheckIn.Location = new System.Drawing.Point(8, 13);
            this.LblCoCheckIn.Name = "LblCoCheckIn";
            this.LblCoCheckIn.Size = new System.Drawing.Size(104, 24);
            this.LblCoCheckIn.TabIndex = 0;
            this.LblCoCheckIn.Text = "Anmeldung am";
            // 
            // PnlCoDepartmentalBriefing
            // 
            this.PnlCoDepartmentalBriefing.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlCoDepartmentalBriefing.Controls.Add(this.LblCoDepartmentalBriefing);
            this.PnlCoDepartmentalBriefing.Controls.Add(this.RbtCoDepartmentalBriefingYes);
            this.PnlCoDepartmentalBriefing.Controls.Add(this.RbtCoDepartmentalBriefingNo);
            this.PnlCoDepartmentalBriefing.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PnlCoDepartmentalBriefing.Location = new System.Drawing.Point(876, 230);
            this.PnlCoDepartmentalBriefing.Name = "PnlCoDepartmentalBriefing";
            this.PnlCoDepartmentalBriefing.Size = new System.Drawing.Size(364, 56);
            this.PnlCoDepartmentalBriefing.TabIndex = 23;
            this.PnlCoDepartmentalBriefing.TabStop = true;
            // 
            // LblCoDepartmentalBriefing
            // 
            this.LblCoDepartmentalBriefing.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCoDepartmentalBriefing.Location = new System.Drawing.Point(8, 10);
            this.LblCoDepartmentalBriefing.Name = "LblCoDepartmentalBriefing";
            this.LblCoDepartmentalBriefing.Size = new System.Drawing.Size(156, 32);
            this.LblCoDepartmentalBriefing.TabIndex = 94;
            this.LblCoDepartmentalBriefing.Text = "Belehrung durch Abteilung Arbeitssicherheit";
            // 
            // RbtCoDepartmentalBriefingYes
            // 
            this.RbtCoDepartmentalBriefingYes.Location = new System.Drawing.Point(260, 16);
            this.RbtCoDepartmentalBriefingYes.Name = "RbtCoDepartmentalBriefingYes";
            this.RbtCoDepartmentalBriefingYes.Size = new System.Drawing.Size(40, 24);
            this.RbtCoDepartmentalBriefingYes.TabIndex = 1;
            this.RbtCoDepartmentalBriefingYes.Text = "Ja";
            this.RbtCoDepartmentalBriefingYes.CheckedChanged += new System.EventHandler(this.RbtCoDepartmentalBriefingYes_CheckedChanged);
            // 
            // RbtCoDepartmentalBriefingNo
            // 
            this.RbtCoDepartmentalBriefingNo.Checked = true;
            this.RbtCoDepartmentalBriefingNo.Location = new System.Drawing.Point(300, 16);
            this.RbtCoDepartmentalBriefingNo.Name = "RbtCoDepartmentalBriefingNo";
            this.RbtCoDepartmentalBriefingNo.Size = new System.Drawing.Size(64, 24);
            this.RbtCoDepartmentalBriefingNo.TabIndex = 2;
            this.RbtCoDepartmentalBriefingNo.TabStop = true;
            this.RbtCoDepartmentalBriefingNo.Text = "Nein";
            this.RbtCoDepartmentalBriefingNo.CheckedChanged += new System.EventHandler(this.RbtCoDepartmentalBriefingNo_CheckedChanged);
            // 
            // PnlCoData
            // 
            this.PnlCoData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlCoData.Controls.Add(this.LblCoDataTitle);
            this.PnlCoData.Controls.Add(this.lblCoPlantsAll);
            this.PnlCoData.Controls.Add(this.BtnCoPlantsAll);
            this.PnlCoData.Controls.Add(this.label1);
            this.PnlCoData.Controls.Add(this.TxtCoSupervisor);
            this.PnlCoData.Controls.Add(this.BtnCoSearchExternalContractor);
            this.PnlCoData.Controls.Add(this.CboCoCraftName);
            this.PnlCoData.Controls.Add(this.LiKCoPlant);
            this.PnlCoData.Controls.Add(this.CboCoCraftNumber);
            this.PnlCoData.Controls.Add(this.LblCoDepartment);
            this.PnlCoData.Controls.Add(this.LblCoPlant);
            this.PnlCoData.Controls.Add(this.LblCoTelephoneNumber);
            this.PnlCoData.Controls.Add(this.LblCoCraft);
            this.PnlCoData.Controls.Add(this.LblCoOrderNumber);
            this.PnlCoData.Controls.Add(this.LblCoSupervisor);
            this.PnlCoData.Controls.Add(this.LblCoSubcontractor);
            this.PnlCoData.Controls.Add(this.CboCoDepartment);
            this.PnlCoData.Controls.Add(this.TxtCoTelephoneNumber);
            this.PnlCoData.Controls.Add(this.TxtCoOrderNumber);
            this.PnlCoData.Controls.Add(this.CboCoSubcontractor);
            this.PnlCoData.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PnlCoData.Location = new System.Drawing.Point(8, 230);
            this.PnlCoData.Name = "PnlCoData";
            this.PnlCoData.Size = new System.Drawing.Size(451, 420);
            this.PnlCoData.TabIndex = 0;
            // 
            // LblCoDataTitle
            // 
            this.LblCoDataTitle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCoDataTitle.Location = new System.Drawing.Point(13, 8);
            this.LblCoDataTitle.Name = "LblCoDataTitle";
            this.LblCoDataTitle.Size = new System.Drawing.Size(124, 23);
            this.LblCoDataTitle.TabIndex = 100;
            this.LblCoDataTitle.Text = "Grunddaten";
            this.LblCoDataTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCoPlantsAll
            // 
            this.lblCoPlantsAll.AutoSize = true;
            this.lblCoPlantsAll.Location = new System.Drawing.Point(177, 391);
            this.lblCoPlantsAll.Name = "lblCoPlantsAll";
            this.lblCoPlantsAll.Size = new System.Drawing.Size(131, 15);
            this.lblCoPlantsAll.TabIndex = 99;
            this.lblCoPlantsAll.Text = "alle Betriebe zuweisen";
            // 
            // BtnCoPlantsAll
            // 
            this.BtnCoPlantsAll.Image = global::Properties.Resources.SuccessComplete;
            this.BtnCoPlantsAll.Location = new System.Drawing.Point(152, 388);
            this.BtnCoPlantsAll.Name = "BtnCoPlantsAll";
            this.BtnCoPlantsAll.Size = new System.Drawing.Size(21, 21);
            this.BtnCoPlantsAll.TabIndex = 98;
            this.TooExContractor.SetToolTip(this.BtnCoPlantsAll, "Öffnet die Maske Fremdfirmensuche");
            this.TooPdf.SetToolTip(this.BtnCoPlantsAll, "Öffnet das Formular \"Sicherheitsunterweisung\".");
            this.BtnCoPlantsAll.Click += new System.EventHandler(this.BtnCoPlantsAll_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(18, 214);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 24);
            this.label1.TabIndex = 31;
            this.label1.Text = "Gewerk-Name";
            // 
            // TxtCoSupervisor
            // 
            this.TxtCoSupervisor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCoSupervisor.Location = new System.Drawing.Point(152, 77);
            this.TxtCoSupervisor.MaxLength = 30;
            this.TxtCoSupervisor.Name = "TxtCoSupervisor";
            this.TxtCoSupervisor.ReadOnly = true;
            this.TxtCoSupervisor.Size = new System.Drawing.Size(250, 21);
            this.TxtCoSupervisor.TabIndex = 3;
            // 
            // BtnCoSearchExternalContractor
            // 
            this.BtnCoSearchExternalContractor.Location = new System.Drawing.Point(413, 43);
            this.BtnCoSearchExternalContractor.Name = "BtnCoSearchExternalContractor";
            this.BtnCoSearchExternalContractor.Size = new System.Drawing.Size(21, 21);
            this.BtnCoSearchExternalContractor.TabIndex = 2;
            this.BtnCoSearchExternalContractor.Text = "?&U";
            this.TooExContractor.SetToolTip(this.BtnCoSearchExternalContractor, "Öffnet die Maske Fremdfirmensuche");
            this.BtnCoSearchExternalContractor.Click += new System.EventHandler(this.BtnCoSearchExternalContractor_Click_1);
            // 
            // CboCoCraftName
            // 
            this.CboCoCraftName.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboCoCraftName.Location = new System.Drawing.Point(152, 211);
            this.CboCoCraftName.MaxLength = 30;
            this.CboCoCraftName.Name = "CboCoCraftName";
            this.CboCoCraftName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.CboCoCraftName.Size = new System.Drawing.Size(250, 23);
            this.CboCoCraftName.TabIndex = 7;
            this.CboCoCraftName.SelectedIndexChanged += new System.EventHandler(this.CboCoCraftName_SelectedIndexChanged);
            // 
            // LiKCoPlant
            // 
            this.LiKCoPlant.CheckOnClick = true;
            this.LiKCoPlant.Location = new System.Drawing.Point(152, 282);
            this.LiKCoPlant.Name = "LiKCoPlant";
            this.LiKCoPlant.Size = new System.Drawing.Size(250, 100);
            this.LiKCoPlant.TabIndex = 9;
            this.LiKCoPlant.ThreeDCheckBoxes = true;
            this.LiKCoPlant.SelectedValueChanged += new System.EventHandler(this.LiKCoPlant_SelectedValueChanged);
            this.LiKCoPlant.Enter += new System.EventHandler(this.LiKCoPlant_Enter);
            this.LiKCoPlant.Leave += new System.EventHandler(this.LiKCoPlant_Leave);
            // 
            // CboCoCraftNumber
            // 
            this.CboCoCraftNumber.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboCoCraftNumber.Location = new System.Drawing.Point(152, 176);
            this.CboCoCraftNumber.MaxLength = 30;
            this.CboCoCraftNumber.Name = "CboCoCraftNumber";
            this.CboCoCraftNumber.Size = new System.Drawing.Size(250, 23);
            this.CboCoCraftNumber.TabIndex = 6;
            this.CboCoCraftNumber.SelectedIndexChanged += new System.EventHandler(this.CboCoCraftNumber_SelectedIndexChanged);
            // 
            // LblCoDepartment
            // 
            this.LblCoDepartment.Location = new System.Drawing.Point(18, 249);
            this.LblCoDepartment.Name = "LblCoDepartment";
            this.LblCoDepartment.Size = new System.Drawing.Size(96, 24);
            this.LblCoDepartment.TabIndex = 30;
            this.LblCoDepartment.Text = "Abteilung";
            // 
            // LblCoPlant
            // 
            this.LblCoPlant.Location = new System.Drawing.Point(18, 282);
            this.LblCoPlant.Name = "LblCoPlant";
            this.LblCoPlant.Size = new System.Drawing.Size(119, 24);
            this.LblCoPlant.TabIndex = 29;
            this.LblCoPlant.Text = "Betrieb aus FPASS";
            // 
            // LblCoTelephoneNumber
            // 
            this.LblCoTelephoneNumber.Location = new System.Drawing.Point(18, 103);
            this.LblCoTelephoneNumber.Name = "LblCoTelephoneNumber";
            this.LblCoTelephoneNumber.Size = new System.Drawing.Size(96, 32);
            this.LblCoTelephoneNumber.TabIndex = 28;
            this.LblCoTelephoneNumber.Text = "Telefon-Nr. Baustellenleiter";
            // 
            // LblCoCraft
            // 
            this.LblCoCraft.Location = new System.Drawing.Point(18, 179);
            this.LblCoCraft.Name = "LblCoCraft";
            this.LblCoCraft.Size = new System.Drawing.Size(96, 24);
            this.LblCoCraft.TabIndex = 27;
            this.LblCoCraft.Text = "Gewerk-Nr.";
            // 
            // LblCoOrderNumber
            // 
            this.LblCoOrderNumber.Location = new System.Drawing.Point(18, 146);
            this.LblCoOrderNumber.Name = "LblCoOrderNumber";
            this.LblCoOrderNumber.Size = new System.Drawing.Size(96, 24);
            this.LblCoOrderNumber.TabIndex = 26;
            this.LblCoOrderNumber.Text = "Auftrags-Nr.";
            // 
            // LblCoSupervisor
            // 
            this.LblCoSupervisor.Location = new System.Drawing.Point(18, 80);
            this.LblCoSupervisor.Name = "LblCoSupervisor";
            this.LblCoSupervisor.Size = new System.Drawing.Size(96, 24);
            this.LblCoSupervisor.TabIndex = 25;
            this.LblCoSupervisor.Text = "Baustellenleiter";
            // 
            // LblCoSubcontractor
            // 
            this.LblCoSubcontractor.Location = new System.Drawing.Point(18, 45);
            this.LblCoSubcontractor.Name = "LblCoSubcontractor";
            this.LblCoSubcontractor.Size = new System.Drawing.Size(96, 24);
            this.LblCoSubcontractor.TabIndex = 24;
            this.LblCoSubcontractor.Text = "Subfirma";
            // 
            // CboCoDepartment
            // 
            this.CboCoDepartment.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboCoDepartment.Location = new System.Drawing.Point(152, 246);
            this.CboCoDepartment.Name = "CboCoDepartment";
            this.CboCoDepartment.Size = new System.Drawing.Size(250, 23);
            this.CboCoDepartment.TabIndex = 8;
            // 
            // TxtCoTelephoneNumber
            // 
            this.TxtCoTelephoneNumber.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCoTelephoneNumber.Location = new System.Drawing.Point(152, 110);
            this.TxtCoTelephoneNumber.MaxLength = 30;
            this.TxtCoTelephoneNumber.Name = "TxtCoTelephoneNumber";
            this.TxtCoTelephoneNumber.ReadOnly = true;
            this.TxtCoTelephoneNumber.Size = new System.Drawing.Size(250, 21);
            this.TxtCoTelephoneNumber.TabIndex = 4;
            // 
            // TxtCoOrderNumber
            // 
            this.TxtCoOrderNumber.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCoOrderNumber.Location = new System.Drawing.Point(152, 143);
            this.TxtCoOrderNumber.MaxLength = 30;
            this.TxtCoOrderNumber.Name = "TxtCoOrderNumber";
            this.TxtCoOrderNumber.Size = new System.Drawing.Size(250, 21);
            this.TxtCoOrderNumber.TabIndex = 5;
            // 
            // CboCoSubcontractor
            // 
            this.CboCoSubcontractor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboCoSubcontractor.Location = new System.Drawing.Point(152, 42);
            this.CboCoSubcontractor.MaxLength = 30;
            this.CboCoSubcontractor.Name = "CboCoSubcontractor";
            this.CboCoSubcontractor.Size = new System.Drawing.Size(250, 23);
            this.CboCoSubcontractor.TabIndex = 1;
            this.CboCoSubcontractor.SelectedIndexChanged += new System.EventHandler(this.CboCoSubcontractor_SelectedIndexChanged);
            // 
            // PnlCoPrecautionaryMedical
            // 
            this.PnlCoPrecautionaryMedical.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlCoPrecautionaryMedical.Controls.Add(this.RbtCoPrecautionaryMedicalNo);
            this.PnlCoPrecautionaryMedical.Controls.Add(this.RbtCoPrecautionaryMedicalYes);
            this.PnlCoPrecautionaryMedical.Controls.Add(this.LblCoPrecautionaryMedical);
            this.PnlCoPrecautionaryMedical.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PnlCoPrecautionaryMedical.Location = new System.Drawing.Point(876, 358);
            this.PnlCoPrecautionaryMedical.Name = "PnlCoPrecautionaryMedical";
            this.PnlCoPrecautionaryMedical.Size = new System.Drawing.Size(364, 56);
            this.PnlCoPrecautionaryMedical.TabIndex = 25;
            this.PnlCoPrecautionaryMedical.TabStop = true;
            // 
            // RbtCoPrecautionaryMedicalNo
            // 
            this.RbtCoPrecautionaryMedicalNo.Checked = true;
            this.RbtCoPrecautionaryMedicalNo.Location = new System.Drawing.Point(300, 16);
            this.RbtCoPrecautionaryMedicalNo.Name = "RbtCoPrecautionaryMedicalNo";
            this.RbtCoPrecautionaryMedicalNo.Size = new System.Drawing.Size(64, 24);
            this.RbtCoPrecautionaryMedicalNo.TabIndex = 2;
            this.RbtCoPrecautionaryMedicalNo.TabStop = true;
            this.RbtCoPrecautionaryMedicalNo.Text = "Nein";
            this.RbtCoPrecautionaryMedicalNo.CheckedChanged += new System.EventHandler(this.RbtCoPrecautionaryMedicalNo_CheckedChanged);
            // 
            // RbtCoPrecautionaryMedicalYes
            // 
            this.RbtCoPrecautionaryMedicalYes.Location = new System.Drawing.Point(260, 16);
            this.RbtCoPrecautionaryMedicalYes.Name = "RbtCoPrecautionaryMedicalYes";
            this.RbtCoPrecautionaryMedicalYes.Size = new System.Drawing.Size(40, 24);
            this.RbtCoPrecautionaryMedicalYes.TabIndex = 1;
            this.RbtCoPrecautionaryMedicalYes.Text = "Ja";
            this.RbtCoPrecautionaryMedicalYes.CheckedChanged += new System.EventHandler(this.RbtCoPrecautionaryMedicalYes_CheckedChanged);
            // 
            // LblCoPrecautionaryMedical
            // 
            this.LblCoPrecautionaryMedical.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCoPrecautionaryMedical.Location = new System.Drawing.Point(8, 20);
            this.LblCoPrecautionaryMedical.Name = "LblCoPrecautionaryMedical";
            this.LblCoPrecautionaryMedical.Size = new System.Drawing.Size(156, 16);
            this.LblCoPrecautionaryMedical.TabIndex = 76;
            this.LblCoPrecautionaryMedical.Text = "Vorsorgeuntersuchung";
            // 
            // PnlCoRespiratoryMaskBriefing
            // 
            this.PnlCoRespiratoryMaskBriefing.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlCoRespiratoryMaskBriefing.Controls.Add(this.RbtCoRespiratoryMaskBriefingNo);
            this.PnlCoRespiratoryMaskBriefing.Controls.Add(this.RbtCoRespiratoryMaskBriefingYes);
            this.PnlCoRespiratoryMaskBriefing.Controls.Add(this.LblCoRespiratoryMaskBriefing);
            this.PnlCoRespiratoryMaskBriefing.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PnlCoRespiratoryMaskBriefing.Location = new System.Drawing.Point(876, 294);
            this.PnlCoRespiratoryMaskBriefing.Name = "PnlCoRespiratoryMaskBriefing";
            this.PnlCoRespiratoryMaskBriefing.Size = new System.Drawing.Size(364, 56);
            this.PnlCoRespiratoryMaskBriefing.TabIndex = 24;
            this.PnlCoRespiratoryMaskBriefing.TabStop = true;
            // 
            // RbtCoRespiratoryMaskBriefingNo
            // 
            this.RbtCoRespiratoryMaskBriefingNo.Checked = true;
            this.RbtCoRespiratoryMaskBriefingNo.Location = new System.Drawing.Point(300, 16);
            this.RbtCoRespiratoryMaskBriefingNo.Name = "RbtCoRespiratoryMaskBriefingNo";
            this.RbtCoRespiratoryMaskBriefingNo.Size = new System.Drawing.Size(64, 24);
            this.RbtCoRespiratoryMaskBriefingNo.TabIndex = 2;
            this.RbtCoRespiratoryMaskBriefingNo.TabStop = true;
            this.RbtCoRespiratoryMaskBriefingNo.Text = "Nein";
            this.RbtCoRespiratoryMaskBriefingNo.CheckedChanged += new System.EventHandler(this.RbtCoRespiratoryMaskBriefingNo_CheckedChanged);
            // 
            // RbtCoRespiratoryMaskBriefingYes
            // 
            this.RbtCoRespiratoryMaskBriefingYes.Location = new System.Drawing.Point(260, 16);
            this.RbtCoRespiratoryMaskBriefingYes.Name = "RbtCoRespiratoryMaskBriefingYes";
            this.RbtCoRespiratoryMaskBriefingYes.Size = new System.Drawing.Size(40, 24);
            this.RbtCoRespiratoryMaskBriefingYes.TabIndex = 1;
            this.RbtCoRespiratoryMaskBriefingYes.Text = "Ja";
            this.RbtCoRespiratoryMaskBriefingYes.CheckedChanged += new System.EventHandler(this.RbtCoRespiratoryMaskBriefingYes_CheckedChanged);
            // 
            // LblCoRespiratoryMaskBriefing
            // 
            this.LblCoRespiratoryMaskBriefing.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCoRespiratoryMaskBriefing.Location = new System.Drawing.Point(8, 12);
            this.LblCoRespiratoryMaskBriefing.Name = "LblCoRespiratoryMaskBriefing";
            this.LblCoRespiratoryMaskBriefing.Size = new System.Drawing.Size(120, 32);
            this.LblCoRespiratoryMaskBriefing.TabIndex = 77;
            this.LblCoRespiratoryMaskBriefing.Text = "Atemschutzmasken-unterweisung";
            // 
            // PnlCoIndustrialSafetyBriefingSite
            // 
            this.PnlCoIndustrialSafetyBriefingSite.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlCoIndustrialSafetyBriefingSite.Controls.Add(this.BtnCoPdf);
            this.PnlCoIndustrialSafetyBriefingSite.Controls.Add(this.DatCoIndustrialSafetyBriefingSiteOn);
            this.PnlCoIndustrialSafetyBriefingSite.Controls.Add(this.LblCoIndustrialSafetyBriefingSite);
            this.PnlCoIndustrialSafetyBriefingSite.Controls.Add(this.CbxCoIndSafetyBrfRecvd);
            this.PnlCoIndustrialSafetyBriefingSite.Controls.Add(this.TxtCoIndustrialSafetyBriefingSiteBy);
            this.PnlCoIndustrialSafetyBriefingSite.Controls.Add(this.LblCoIndustrialSafetyBriefingSiteBy);
            this.PnlCoIndustrialSafetyBriefingSite.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PnlCoIndustrialSafetyBriefingSite.Location = new System.Drawing.Point(8, 177);
            this.PnlCoIndustrialSafetyBriefingSite.Name = "PnlCoIndustrialSafetyBriefingSite";
            this.PnlCoIndustrialSafetyBriefingSite.Size = new System.Drawing.Size(1232, 46);
            this.PnlCoIndustrialSafetyBriefingSite.TabIndex = 10;
            this.PnlCoIndustrialSafetyBriefingSite.TabStop = true;
            // 
            // BtnCoPdf
            // 
            this.BtnCoPdf.Location = new System.Drawing.Point(853, 10);
            this.BtnCoPdf.Name = "BtnCoPdf";
            this.BtnCoPdf.Size = new System.Drawing.Size(129, 23);
            this.BtnCoPdf.TabIndex = 11;
            this.BtnCoPdf.Text = "PDF ausdrucken";
            this.TooExContractor.SetToolTip(this.BtnCoPdf, "Öffnet die Maske Fremdfirmensuche");
            this.TooPdf.SetToolTip(this.BtnCoPdf, "Öffnet das Formular \"Sicherheitsunterweisung\".");
            this.BtnCoPdf.Click += new System.EventHandler(this.BtnCoPdf_Click);
            // 
            // DatCoIndustrialSafetyBriefingSiteOn
            // 
            this.DatCoIndustrialSafetyBriefingSiteOn.CustomFormat = "dd.MM.yyyy";
            this.DatCoIndustrialSafetyBriefingSiteOn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatCoIndustrialSafetyBriefingSiteOn.Location = new System.Drawing.Point(270, 11);
            this.DatCoIndustrialSafetyBriefingSiteOn.Name = "DatCoIndustrialSafetyBriefingSiteOn";
            this.DatCoIndustrialSafetyBriefingSiteOn.Size = new System.Drawing.Size(88, 21);
            this.DatCoIndustrialSafetyBriefingSiteOn.TabIndex = 10;
            this.DatCoIndustrialSafetyBriefingSiteOn.Value = new System.DateTime(2003, 10, 6, 0, 0, 0, 0);
            this.DatCoIndustrialSafetyBriefingSiteOn.Leave += new System.EventHandler(this.DatCoIndustrialSafetyBriefingSiteOn_Leave_1);
            // 
            // LblCoIndustrialSafetyBriefingSite
            // 
            this.LblCoIndustrialSafetyBriefingSite.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCoIndustrialSafetyBriefingSite.Location = new System.Drawing.Point(44, 14);
            this.LblCoIndustrialSafetyBriefingSite.Name = "LblCoIndustrialSafetyBriefingSite";
            this.LblCoIndustrialSafetyBriefingSite.Size = new System.Drawing.Size(240, 21);
            this.LblCoIndustrialSafetyBriefingSite.TabIndex = 91;
            this.LblCoIndustrialSafetyBriefingSite.Text = "Sicherheitsunterweisung erfolgt am";
            // 
            // CbxCoIndSafetyBrfRecvd
            // 
            this.CbxCoIndSafetyBrfRecvd.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Millimeter, ((byte)(0)));
            this.CbxCoIndSafetyBrfRecvd.ForeColor = System.Drawing.SystemColors.Control;
            this.CbxCoIndSafetyBrfRecvd.Location = new System.Drawing.Point(22, 10);
            this.CbxCoIndSafetyBrfRecvd.Name = "CbxCoIndSafetyBrfRecvd";
            this.CbxCoIndSafetyBrfRecvd.Size = new System.Drawing.Size(24, 24);
            this.CbxCoIndSafetyBrfRecvd.TabIndex = 9;
            this.CbxCoIndSafetyBrfRecvd.Text = "S";
            this.CbxCoIndSafetyBrfRecvd.CheckedChanged += new System.EventHandler(this.CbxCoIndSafetyBrfRecvd_CheckedChanged);
            // 
            // TxtCoIndustrialSafetyBriefingSiteBy
            // 
            this.TxtCoIndustrialSafetyBriefingSiteBy.Font = new System.Drawing.Font("Arial", 9F);
            this.TxtCoIndustrialSafetyBriefingSiteBy.Location = new System.Drawing.Point(628, 11);
            this.TxtCoIndustrialSafetyBriefingSiteBy.MaxLength = 30;
            this.TxtCoIndustrialSafetyBriefingSiteBy.Name = "TxtCoIndustrialSafetyBriefingSiteBy";
            this.TxtCoIndustrialSafetyBriefingSiteBy.ReadOnly = true;
            this.TxtCoIndustrialSafetyBriefingSiteBy.Size = new System.Drawing.Size(210, 21);
            this.TxtCoIndustrialSafetyBriefingSiteBy.TabIndex = 13;
            this.TxtCoIndustrialSafetyBriefingSiteBy.TabStop = false;
            // 
            // LblCoIndustrialSafetyBriefingSiteBy
            // 
            this.LblCoIndustrialSafetyBriefingSiteBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCoIndustrialSafetyBriefingSiteBy.Location = new System.Drawing.Point(469, 15);
            this.LblCoIndustrialSafetyBriefingSiteBy.Name = "LblCoIndustrialSafetyBriefingSiteBy";
            this.LblCoIndustrialSafetyBriefingSiteBy.Size = new System.Drawing.Size(40, 21);
            this.LblCoIndustrialSafetyBriefingSiteBy.TabIndex = 96;
            this.LblCoIndustrialSafetyBriefingSiteBy.Text = "durch";
            // 
            // PnlCoBriefing
            // 
            this.PnlCoBriefing.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlCoBriefing.Controls.Add(this.LblCoBriefTitle);
            this.PnlCoBriefing.Controls.Add(this.PnlCoFireman);
            this.PnlCoBriefing.Controls.Add(this.PnlCoBreathingApparatusG26_3);
            this.PnlCoBriefing.Controls.Add(this.PnlCoSiteSecurityBriefing);
            this.PnlCoBriefing.Controls.Add(this.PnlCoCranes);
            this.PnlCoBriefing.Controls.Add(this.PnlCoRaisablePlattform);
            this.PnlCoBriefing.Controls.Add(this.PnlCoPalletLifter);
            this.PnlCoBriefing.Controls.Add(this.PnlCoBreathingApparatusG26_2);
            this.PnlCoBriefing.Controls.Add(this.PnlCoSafetyPass);
            this.PnlCoBriefing.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PnlCoBriefing.Location = new System.Drawing.Point(467, 230);
            this.PnlCoBriefing.Name = "PnlCoBriefing";
            this.PnlCoBriefing.Size = new System.Drawing.Size(403, 420);
            this.PnlCoBriefing.TabIndex = 14;
            // 
            // LblCoBriefTitle
            // 
            this.LblCoBriefTitle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCoBriefTitle.Location = new System.Drawing.Point(13, 8);
            this.LblCoBriefTitle.Name = "LblCoBriefTitle";
            this.LblCoBriefTitle.Size = new System.Drawing.Size(85, 23);
            this.LblCoBriefTitle.TabIndex = 84;
            this.LblCoBriefTitle.Text = "Belehrungen";
            this.LblCoBriefTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PnlCoFireman
            // 
            this.PnlCoFireman.Controls.Add(this.RbtCoFiremanNo);
            this.PnlCoFireman.Controls.Add(this.RbtCoFiremanYes);
            this.PnlCoFireman.Controls.Add(this.LblCoFireman);
            this.PnlCoFireman.Location = new System.Drawing.Point(12, 216);
            this.PnlCoFireman.Name = "PnlCoFireman";
            this.PnlCoFireman.Size = new System.Drawing.Size(380, 35);
            this.PnlCoFireman.TabIndex = 20;
            this.PnlCoFireman.TabStop = true;
            // 
            // RbtCoFiremanNo
            // 
            this.RbtCoFiremanNo.Checked = true;
            this.RbtCoFiremanNo.Location = new System.Drawing.Point(324, 8);
            this.RbtCoFiremanNo.Name = "RbtCoFiremanNo";
            this.RbtCoFiremanNo.Size = new System.Drawing.Size(64, 24);
            this.RbtCoFiremanNo.TabIndex = 2;
            this.RbtCoFiremanNo.TabStop = true;
            this.RbtCoFiremanNo.Text = "Nein";
            this.RbtCoFiremanNo.Visible = false;
            this.RbtCoFiremanNo.CheckedChanged += new System.EventHandler(this.RbtCoFiremanNo_CheckedChanged);
            // 
            // RbtCoFiremanYes
            // 
            this.RbtCoFiremanYes.Location = new System.Drawing.Point(284, 8);
            this.RbtCoFiremanYes.Name = "RbtCoFiremanYes";
            this.RbtCoFiremanYes.Size = new System.Drawing.Size(40, 24);
            this.RbtCoFiremanYes.TabIndex = 1;
            this.RbtCoFiremanYes.Text = "Ja";
            this.RbtCoFiremanYes.Visible = false;
            this.RbtCoFiremanYes.CheckedChanged += new System.EventHandler(this.RbtCoFiremanYes_CheckedChanged);
            // 
            // LblCoFireman
            // 
            this.LblCoFireman.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCoFireman.Location = new System.Drawing.Point(15, 11);
            this.LblCoFireman.Name = "LblCoFireman";
            this.LblCoFireman.Size = new System.Drawing.Size(182, 16);
            this.LblCoFireman.TabIndex = 94;
            this.LblCoFireman.Text = "Brandsicherheitsposten";
            // 
            // PnlCoBreathingApparatusG26_3
            // 
            this.PnlCoBreathingApparatusG26_3.Controls.Add(this.RbtCoBreathingApparatusNoG26_3);
            this.PnlCoBreathingApparatusG26_3.Controls.Add(this.RbtCoBreathingApparatusYesG26_3);
            this.PnlCoBreathingApparatusG26_3.Controls.Add(this.LblCoBreathingApparatusG26_3);
            this.PnlCoBreathingApparatusG26_3.Location = new System.Drawing.Point(12, 172);
            this.PnlCoBreathingApparatusG26_3.Name = "PnlCoBreathingApparatusG26_3";
            this.PnlCoBreathingApparatusG26_3.Size = new System.Drawing.Size(380, 35);
            this.PnlCoBreathingApparatusG26_3.TabIndex = 19;
            this.PnlCoBreathingApparatusG26_3.TabStop = true;
            // 
            // RbtCoBreathingApparatusNoG26_3
            // 
            this.RbtCoBreathingApparatusNoG26_3.Checked = true;
            this.RbtCoBreathingApparatusNoG26_3.Location = new System.Drawing.Point(324, 8);
            this.RbtCoBreathingApparatusNoG26_3.Name = "RbtCoBreathingApparatusNoG26_3";
            this.RbtCoBreathingApparatusNoG26_3.Size = new System.Drawing.Size(64, 24);
            this.RbtCoBreathingApparatusNoG26_3.TabIndex = 2;
            this.RbtCoBreathingApparatusNoG26_3.TabStop = true;
            this.RbtCoBreathingApparatusNoG26_3.Text = "Nein";
            this.RbtCoBreathingApparatusNoG26_3.Visible = false;
            this.RbtCoBreathingApparatusNoG26_3.CheckedChanged += new System.EventHandler(this.RbtCoBreathingApparatusNoG26_3_CheckedChanged);
            // 
            // RbtCoBreathingApparatusYesG26_3
            // 
            this.RbtCoBreathingApparatusYesG26_3.Location = new System.Drawing.Point(284, 8);
            this.RbtCoBreathingApparatusYesG26_3.Name = "RbtCoBreathingApparatusYesG26_3";
            this.RbtCoBreathingApparatusYesG26_3.Size = new System.Drawing.Size(40, 24);
            this.RbtCoBreathingApparatusYesG26_3.TabIndex = 1;
            this.RbtCoBreathingApparatusYesG26_3.Text = "Ja";
            this.RbtCoBreathingApparatusYesG26_3.Visible = false;
            this.RbtCoBreathingApparatusYesG26_3.CheckedChanged += new System.EventHandler(this.RbtCoBreathingApparatusYesG26_3_CheckedChanged);
            // 
            // LblCoBreathingApparatusG26_3
            // 
            this.LblCoBreathingApparatusG26_3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCoBreathingApparatusG26_3.Location = new System.Drawing.Point(15, 11);
            this.LblCoBreathingApparatusG26_3.Name = "LblCoBreathingApparatusG26_3";
            this.LblCoBreathingApparatusG26_3.Size = new System.Drawing.Size(188, 16);
            this.LblCoBreathingApparatusG26_3.TabIndex = 94;
            this.LblCoBreathingApparatusG26_3.Text = "Atemschutzgeräteträger Rettung";
            // 
            // PnlCoSiteSecurityBriefing
            // 
            this.PnlCoSiteSecurityBriefing.Controls.Add(this.LblCoSiteSecurityBriefing);
            this.PnlCoSiteSecurityBriefing.Controls.Add(this.RbtCoSiteSecurityBriefingNo);
            this.PnlCoSiteSecurityBriefing.Controls.Add(this.RbtCoSiteSecurityBriefingYes);
            this.PnlCoSiteSecurityBriefing.Location = new System.Drawing.Point(11, 39);
            this.PnlCoSiteSecurityBriefing.Name = "PnlCoSiteSecurityBriefing";
            this.PnlCoSiteSecurityBriefing.Size = new System.Drawing.Size(380, 35);
            this.PnlCoSiteSecurityBriefing.TabIndex = 15;
            this.PnlCoSiteSecurityBriefing.TabStop = true;
            // 
            // LblCoSiteSecurityBriefing
            // 
            this.LblCoSiteSecurityBriefing.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCoSiteSecurityBriefing.Location = new System.Drawing.Point(15, 11);
            this.LblCoSiteSecurityBriefing.Name = "LblCoSiteSecurityBriefing";
            this.LblCoSiteSecurityBriefing.Size = new System.Drawing.Size(168, 16);
            this.LblCoSiteSecurityBriefing.TabIndex = 107;
            this.LblCoSiteSecurityBriefing.Text = "Werksicherheitsbelehrung";
            // 
            // RbtCoSiteSecurityBriefingNo
            // 
            this.RbtCoSiteSecurityBriefingNo.Checked = true;
            this.RbtCoSiteSecurityBriefingNo.Location = new System.Drawing.Point(324, 8);
            this.RbtCoSiteSecurityBriefingNo.Name = "RbtCoSiteSecurityBriefingNo";
            this.RbtCoSiteSecurityBriefingNo.Size = new System.Drawing.Size(64, 24);
            this.RbtCoSiteSecurityBriefingNo.TabIndex = 2;
            this.RbtCoSiteSecurityBriefingNo.TabStop = true;
            this.RbtCoSiteSecurityBriefingNo.Text = "Nein";
            this.RbtCoSiteSecurityBriefingNo.CheckedChanged += new System.EventHandler(this.RbtCoSiteSecurityBriefingNo_CheckedChanged);
            // 
            // RbtCoSiteSecurityBriefingYes
            // 
            this.RbtCoSiteSecurityBriefingYes.Location = new System.Drawing.Point(284, 8);
            this.RbtCoSiteSecurityBriefingYes.Name = "RbtCoSiteSecurityBriefingYes";
            this.RbtCoSiteSecurityBriefingYes.Size = new System.Drawing.Size(40, 24);
            this.RbtCoSiteSecurityBriefingYes.TabIndex = 1;
            this.RbtCoSiteSecurityBriefingYes.Text = "Ja";
            this.RbtCoSiteSecurityBriefingYes.CheckedChanged += new System.EventHandler(this.RbtCoSiteSecurityBriefingYes_CheckedChanged);
            // 
            // PnlCoCranes
            // 
            this.PnlCoCranes.Controls.Add(this.RbtCoCranesNo);
            this.PnlCoCranes.Controls.Add(this.RbtCoCranesYes);
            this.PnlCoCranes.Controls.Add(this.LblCoCranes);
            this.PnlCoCranes.Location = new System.Drawing.Point(11, 346);
            this.PnlCoCranes.Name = "PnlCoCranes";
            this.PnlCoCranes.Size = new System.Drawing.Size(380, 35);
            this.PnlCoCranes.TabIndex = 23;
            this.PnlCoCranes.TabStop = true;
            // 
            // RbtCoCranesNo
            // 
            this.RbtCoCranesNo.Checked = true;
            this.RbtCoCranesNo.Location = new System.Drawing.Point(324, 8);
            this.RbtCoCranesNo.Name = "RbtCoCranesNo";
            this.RbtCoCranesNo.Size = new System.Drawing.Size(64, 24);
            this.RbtCoCranesNo.TabIndex = 2;
            this.RbtCoCranesNo.TabStop = true;
            this.RbtCoCranesNo.Text = "Nein";
            this.RbtCoCranesNo.Visible = false;
            this.RbtCoCranesNo.CheckedChanged += new System.EventHandler(this.RbtCoCranesNo_CheckedChanged);
            // 
            // RbtCoCranesYes
            // 
            this.RbtCoCranesYes.Location = new System.Drawing.Point(284, 8);
            this.RbtCoCranesYes.Name = "RbtCoCranesYes";
            this.RbtCoCranesYes.Size = new System.Drawing.Size(40, 24);
            this.RbtCoCranesYes.TabIndex = 1;
            this.RbtCoCranesYes.Text = "Ja";
            this.RbtCoCranesYes.Visible = false;
            this.RbtCoCranesYes.CheckedChanged += new System.EventHandler(this.RbtCoCranesYes_CheckedChanged);
            // 
            // LblCoCranes
            // 
            this.LblCoCranes.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCoCranes.Location = new System.Drawing.Point(15, 11);
            this.LblCoCranes.Name = "LblCoCranes";
            this.LblCoCranes.Size = new System.Drawing.Size(132, 16);
            this.LblCoCranes.TabIndex = 100;
            this.LblCoCranes.Text = "Krane/Anschläger";
            // 
            // PnlCoRaisablePlattform
            // 
            this.PnlCoRaisablePlattform.Controls.Add(this.RbtCoRaisablePlattformNo);
            this.PnlCoRaisablePlattform.Controls.Add(this.RbtCoRaisablePlattformYes);
            this.PnlCoRaisablePlattform.Controls.Add(this.LblCoRaisablePlattform);
            this.PnlCoRaisablePlattform.Location = new System.Drawing.Point(11, 302);
            this.PnlCoRaisablePlattform.Name = "PnlCoRaisablePlattform";
            this.PnlCoRaisablePlattform.Size = new System.Drawing.Size(380, 35);
            this.PnlCoRaisablePlattform.TabIndex = 22;
            this.PnlCoRaisablePlattform.TabStop = true;
            // 
            // RbtCoRaisablePlattformNo
            // 
            this.RbtCoRaisablePlattformNo.Checked = true;
            this.RbtCoRaisablePlattformNo.Location = new System.Drawing.Point(324, 8);
            this.RbtCoRaisablePlattformNo.Name = "RbtCoRaisablePlattformNo";
            this.RbtCoRaisablePlattformNo.Size = new System.Drawing.Size(64, 24);
            this.RbtCoRaisablePlattformNo.TabIndex = 2;
            this.RbtCoRaisablePlattformNo.TabStop = true;
            this.RbtCoRaisablePlattformNo.Text = "Nein";
            this.RbtCoRaisablePlattformNo.Visible = false;
            this.RbtCoRaisablePlattformNo.CheckedChanged += new System.EventHandler(this.RbtCoRaisablePlattformNo_CheckedChanged);
            // 
            // RbtCoRaisablePlattformYes
            // 
            this.RbtCoRaisablePlattformYes.Location = new System.Drawing.Point(284, 8);
            this.RbtCoRaisablePlattformYes.Name = "RbtCoRaisablePlattformYes";
            this.RbtCoRaisablePlattformYes.Size = new System.Drawing.Size(40, 24);
            this.RbtCoRaisablePlattformYes.TabIndex = 1;
            this.RbtCoRaisablePlattformYes.Text = "Ja";
            this.RbtCoRaisablePlattformYes.Visible = false;
            this.RbtCoRaisablePlattformYes.CheckedChanged += new System.EventHandler(this.RbtCoRaisablePlattformYes_CheckedChanged);
            // 
            // LblCoRaisablePlattform
            // 
            this.LblCoRaisablePlattform.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCoRaisablePlattform.Location = new System.Drawing.Point(15, 11);
            this.LblCoRaisablePlattform.Name = "LblCoRaisablePlattform";
            this.LblCoRaisablePlattform.Size = new System.Drawing.Size(132, 16);
            this.LblCoRaisablePlattform.TabIndex = 98;
            this.LblCoRaisablePlattform.Text = "Hubarbeitsbühne";
            // 
            // PnlCoPalletLifter
            // 
            this.PnlCoPalletLifter.Controls.Add(this.RbtCoPalletLifterNo);
            this.PnlCoPalletLifter.Controls.Add(this.RbtCoPalletLifterYes);
            this.PnlCoPalletLifter.Controls.Add(this.LblCoPalletLifter);
            this.PnlCoPalletLifter.Location = new System.Drawing.Point(11, 259);
            this.PnlCoPalletLifter.Name = "PnlCoPalletLifter";
            this.PnlCoPalletLifter.Size = new System.Drawing.Size(380, 35);
            this.PnlCoPalletLifter.TabIndex = 21;
            this.PnlCoPalletLifter.TabStop = true;
            // 
            // RbtCoPalletLifterNo
            // 
            this.RbtCoPalletLifterNo.Checked = true;
            this.RbtCoPalletLifterNo.Location = new System.Drawing.Point(324, 8);
            this.RbtCoPalletLifterNo.Name = "RbtCoPalletLifterNo";
            this.RbtCoPalletLifterNo.Size = new System.Drawing.Size(64, 24);
            this.RbtCoPalletLifterNo.TabIndex = 2;
            this.RbtCoPalletLifterNo.TabStop = true;
            this.RbtCoPalletLifterNo.Text = "Nein";
            this.RbtCoPalletLifterNo.Visible = false;
            this.RbtCoPalletLifterNo.CheckedChanged += new System.EventHandler(this.RbtCoPalletLifterNo_CheckedChanged);
            // 
            // RbtCoPalletLifterYes
            // 
            this.RbtCoPalletLifterYes.Location = new System.Drawing.Point(284, 8);
            this.RbtCoPalletLifterYes.Name = "RbtCoPalletLifterYes";
            this.RbtCoPalletLifterYes.Size = new System.Drawing.Size(40, 24);
            this.RbtCoPalletLifterYes.TabIndex = 1;
            this.RbtCoPalletLifterYes.Text = "Ja";
            this.RbtCoPalletLifterYes.Visible = false;
            this.RbtCoPalletLifterYes.CheckedChanged += new System.EventHandler(this.RbtCoPalletLifterYes_CheckedChanged);
            // 
            // LblCoPalletLifter
            // 
            this.LblCoPalletLifter.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCoPalletLifter.Location = new System.Drawing.Point(15, 11);
            this.LblCoPalletLifter.Name = "LblCoPalletLifter";
            this.LblCoPalletLifter.Size = new System.Drawing.Size(132, 16);
            this.LblCoPalletLifter.TabIndex = 96;
            this.LblCoPalletLifter.Text = "Flurförderzeuge";
            // 
            // PnlCoBreathingApparatusG26_2
            // 
            this.PnlCoBreathingApparatusG26_2.Controls.Add(this.RbtCoBreathingApparatusNoG26_2);
            this.PnlCoBreathingApparatusG26_2.Controls.Add(this.RbtCoBreathingApparatusYesG26_2);
            this.PnlCoBreathingApparatusG26_2.Controls.Add(this.LblCoBreathingApparatusG26_2);
            this.PnlCoBreathingApparatusG26_2.Location = new System.Drawing.Point(11, 127);
            this.PnlCoBreathingApparatusG26_2.Name = "PnlCoBreathingApparatusG26_2";
            this.PnlCoBreathingApparatusG26_2.Size = new System.Drawing.Size(380, 35);
            this.PnlCoBreathingApparatusG26_2.TabIndex = 18;
            this.PnlCoBreathingApparatusG26_2.TabStop = true;
            // 
            // RbtCoBreathingApparatusNoG26_2
            // 
            this.RbtCoBreathingApparatusNoG26_2.Checked = true;
            this.RbtCoBreathingApparatusNoG26_2.Location = new System.Drawing.Point(324, 8);
            this.RbtCoBreathingApparatusNoG26_2.Name = "RbtCoBreathingApparatusNoG26_2";
            this.RbtCoBreathingApparatusNoG26_2.Size = new System.Drawing.Size(64, 24);
            this.RbtCoBreathingApparatusNoG26_2.TabIndex = 2;
            this.RbtCoBreathingApparatusNoG26_2.TabStop = true;
            this.RbtCoBreathingApparatusNoG26_2.Text = "Nein";
            this.RbtCoBreathingApparatusNoG26_2.Visible = false;
            this.RbtCoBreathingApparatusNoG26_2.CheckedChanged += new System.EventHandler(this.RbtCoBreathingApparatusNoG26_2_CheckedChanged);
            // 
            // RbtCoBreathingApparatusYesG26_2
            // 
            this.RbtCoBreathingApparatusYesG26_2.Location = new System.Drawing.Point(284, 8);
            this.RbtCoBreathingApparatusYesG26_2.Name = "RbtCoBreathingApparatusYesG26_2";
            this.RbtCoBreathingApparatusYesG26_2.Size = new System.Drawing.Size(40, 24);
            this.RbtCoBreathingApparatusYesG26_2.TabIndex = 1;
            this.RbtCoBreathingApparatusYesG26_2.Text = "Ja";
            this.RbtCoBreathingApparatusYesG26_2.Visible = false;
            this.RbtCoBreathingApparatusYesG26_2.CheckedChanged += new System.EventHandler(this.RbtCoBreathingApparatusYesG26_2_CheckedChanged);
            // 
            // LblCoBreathingApparatusG26_2
            // 
            this.LblCoBreathingApparatusG26_2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCoBreathingApparatusG26_2.Location = new System.Drawing.Point(15, 11);
            this.LblCoBreathingApparatusG26_2.Name = "LblCoBreathingApparatusG26_2";
            this.LblCoBreathingApparatusG26_2.Size = new System.Drawing.Size(182, 16);
            this.LblCoBreathingApparatusG26_2.TabIndex = 94;
            this.LblCoBreathingApparatusG26_2.Text = "Atemschutzgeräteträger ";
            // 
            // PnlCoSafetyPass
            // 
            this.PnlCoSafetyPass.Controls.Add(this.RbtCoSafetyPassNo);
            this.PnlCoSafetyPass.Controls.Add(this.RbtCoSafetyPassYes);
            this.PnlCoSafetyPass.Controls.Add(this.LblCoSafetyPass);
            this.PnlCoSafetyPass.Location = new System.Drawing.Point(11, 83);
            this.PnlCoSafetyPass.Name = "PnlCoSafetyPass";
            this.PnlCoSafetyPass.Size = new System.Drawing.Size(380, 35);
            this.PnlCoSafetyPass.TabIndex = 17;
            this.PnlCoSafetyPass.TabStop = true;
            // 
            // RbtCoSafetyPassNo
            // 
            this.RbtCoSafetyPassNo.Checked = true;
            this.RbtCoSafetyPassNo.Location = new System.Drawing.Point(324, 8);
            this.RbtCoSafetyPassNo.Name = "RbtCoSafetyPassNo";
            this.RbtCoSafetyPassNo.Size = new System.Drawing.Size(56, 24);
            this.RbtCoSafetyPassNo.TabIndex = 2;
            this.RbtCoSafetyPassNo.TabStop = true;
            this.RbtCoSafetyPassNo.Text = "Nein";
            this.RbtCoSafetyPassNo.CheckedChanged += new System.EventHandler(this.RbtCoSafetyPassNo_CheckedChanged_1);
            // 
            // RbtCoSafetyPassYes
            // 
            this.RbtCoSafetyPassYes.Location = new System.Drawing.Point(284, 8);
            this.RbtCoSafetyPassYes.Name = "RbtCoSafetyPassYes";
            this.RbtCoSafetyPassYes.Size = new System.Drawing.Size(40, 24);
            this.RbtCoSafetyPassYes.TabIndex = 1;
            this.RbtCoSafetyPassYes.Text = "Ja";
            this.RbtCoSafetyPassYes.CheckedChanged += new System.EventHandler(this.RbtCoSafetyPassYes_CheckedChanged_1);
            // 
            // LblCoSafetyPass
            // 
            this.LblCoSafetyPass.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCoSafetyPass.Location = new System.Drawing.Point(15, 11);
            this.LblCoSafetyPass.Name = "LblCoSafetyPass";
            this.LblCoSafetyPass.Size = new System.Drawing.Size(168, 16);
            this.LblCoSafetyPass.TabIndex = 115;
            this.LblCoSafetyPass.Text = "Sicherheitspass";
            // 
            // TapSiteFireService
            // 
            this.TapSiteFireService.Controls.Add(this.PnlTabSiteFireService);
            this.TapSiteFireService.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TapSiteFireService.Location = new System.Drawing.Point(4, 25);
            this.TapSiteFireService.Name = "TapSiteFireService";
            this.TapSiteFireService.Size = new System.Drawing.Size(1249, 660);
            this.TapSiteFireService.TabIndex = 8;
            this.TapSiteFireService.Text = "Brandschutz";
            // 
            // PnlTabSiteFireService
            // 
            this.PnlTabSiteFireService.Controls.Add(this.PnlSiFiMaskLent);
            this.PnlTabSiteFireService.Controls.Add(this.PnlSiFiAllBriefings);
            this.PnlTabSiteFireService.Controls.Add(this.PnlSiFiMaskBriefing);
            this.PnlTabSiteFireService.Enabled = false;
            this.PnlTabSiteFireService.Location = new System.Drawing.Point(0, 0);
            this.PnlTabSiteFireService.Name = "PnlTabSiteFireService";
            this.PnlTabSiteFireService.Size = new System.Drawing.Size(1251, 596);
            this.PnlTabSiteFireService.TabIndex = 0;
            // 
            // PnlSiFiMaskLent
            // 
            this.PnlSiFiMaskLent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlSiFiMaskLent.Controls.Add(this.LblSiFiMaskMaintDateTec);
            this.PnlSiFiMaskLent.Controls.Add(this.LblSiFiMaskBackTec);
            this.PnlSiFiMaskLent.Controls.Add(this.TxtSiFiMaskMaintDateTec);
            this.PnlSiFiMaskLent.Controls.Add(this.LblSiFiMaskLentTec);
            this.PnlSiFiMaskLent.Controls.Add(this.TxtSiFiMaskNrBackTec);
            this.PnlSiFiMaskLent.Controls.Add(this.TxtSiFiMaskNrLentTec);
            this.PnlSiFiMaskLent.Controls.Add(this.LblSiFiMaskNrBackTec);
            this.PnlSiFiMaskLent.Controls.Add(this.LblSiFiMaskNrLentTec);
            this.PnlSiFiMaskLent.Controls.Add(this.DatSiFiMaskBackOnTec);
            this.PnlSiFiMaskLent.Controls.Add(this.DatSiFiMaskLentOnTec);
            this.PnlSiFiMaskLent.Controls.Add(this.LblSiFiMaskBackByTec);
            this.PnlSiFiMaskLent.Controls.Add(this.TxtSiFiMaskBackByTec);
            this.PnlSiFiMaskLent.Controls.Add(this.LblSiFiMaskLentByTec);
            this.PnlSiFiMaskLent.Controls.Add(this.TxtSiFiMaskLentByTec);
            this.PnlSiFiMaskLent.Controls.Add(this.LblSiFiMaskBackOnTec);
            this.PnlSiFiMaskLent.Controls.Add(this.LblSiFiMaskLentOnTec);
            this.PnlSiFiMaskLent.Controls.Add(this.groupBox1);
            this.PnlSiFiMaskLent.Controls.Add(this.GrpSiFi1);
            this.PnlSiFiMaskLent.Controls.Add(this.LblSiFiMaskLentTitle);
            this.PnlSiFiMaskLent.Controls.Add(this.LblSiFiMaskTitleFlo);
            this.PnlSiFiMaskLent.Controls.Add(this.BtnSiFiMaskTicket);
            this.PnlSiFiMaskLent.Controls.Add(this.LblSiFiMaskBackFlo);
            this.PnlSiFiMaskLent.Controls.Add(this.LblSiFiMaskLentFlo);
            this.PnlSiFiMaskLent.Controls.Add(this.TxtSiFiMaskNrBackFlo);
            this.PnlSiFiMaskLent.Controls.Add(this.TxtSiFiMaskNrLentFlo);
            this.PnlSiFiMaskLent.Controls.Add(this.LblSiFiMaskNrBackFlo);
            this.PnlSiFiMaskLent.Controls.Add(this.LblSiFiMaskNrLentFlo);
            this.PnlSiFiMaskLent.Controls.Add(this.DatSiFiMaskBackOnFlo);
            this.PnlSiFiMaskLent.Controls.Add(this.DatSiFiMaskLentOnFlo);
            this.PnlSiFiMaskLent.Controls.Add(this.LblSiFiMaskBackByFlo);
            this.PnlSiFiMaskLent.Controls.Add(this.TxtSiFiMaskBackByFlo);
            this.PnlSiFiMaskLent.Controls.Add(this.LblSiFiMaskLentByFlo);
            this.PnlSiFiMaskLent.Controls.Add(this.TxtSiFiMaskLentByFlo);
            this.PnlSiFiMaskLent.Controls.Add(this.LblSiFiMaskBackOnFlo);
            this.PnlSiFiMaskLent.Controls.Add(this.LblSiFiMaskLentOnFlo);
            this.PnlSiFiMaskLent.Location = new System.Drawing.Point(8, 98);
            this.PnlSiFiMaskLent.Name = "PnlSiFiMaskLent";
            this.PnlSiFiMaskLent.Size = new System.Drawing.Size(1228, 222);
            this.PnlSiFiMaskLent.TabIndex = 124;
            this.PnlSiFiMaskLent.TabStop = true;
            // 
            // LblSiFiMaskMaintDateTec
            // 
            this.LblSiFiMaskMaintDateTec.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiFiMaskMaintDateTec.Location = new System.Drawing.Point(271, 40);
            this.LblSiFiMaskMaintDateTec.Name = "LblSiFiMaskMaintDateTec";
            this.LblSiFiMaskMaintDateTec.Size = new System.Drawing.Size(103, 23);
            this.LblSiFiMaskMaintDateTec.TabIndex = 158;
            this.LblSiFiMaskMaintDateTec.Text = "Wartungsdatum";
            this.LblSiFiMaskMaintDateTec.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // LblSiFiMaskBackTec
            // 
            this.LblSiFiMaskBackTec.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiFiMaskBackTec.Location = new System.Drawing.Point(27, 149);
            this.LblSiFiMaskBackTec.Name = "LblSiFiMaskBackTec";
            this.LblSiFiMaskBackTec.Size = new System.Drawing.Size(59, 23);
            this.LblSiFiMaskBackTec.TabIndex = 157;
            this.LblSiFiMaskBackTec.Text = "TecBos:";
            // 
            // TxtSiFiMaskMaintDateTec
            // 
            this.TxtSiFiMaskMaintDateTec.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtSiFiMaskMaintDateTec.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSiFiMaskMaintDateTec.Location = new System.Drawing.Point(380, 38);
            this.TxtSiFiMaskMaintDateTec.Name = "TxtSiFiMaskMaintDateTec";
            this.TxtSiFiMaskMaintDateTec.ReadOnly = true;
            this.TxtSiFiMaskMaintDateTec.Size = new System.Drawing.Size(88, 21);
            this.TxtSiFiMaskMaintDateTec.TabIndex = 156;
            this.TxtSiFiMaskMaintDateTec.TabStop = false;
            // 
            // LblSiFiMaskLentTec
            // 
            this.LblSiFiMaskLentTec.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiFiMaskLentTec.Location = new System.Drawing.Point(26, 40);
            this.LblSiFiMaskLentTec.Name = "LblSiFiMaskLentTec";
            this.LblSiFiMaskLentTec.Size = new System.Drawing.Size(60, 23);
            this.LblSiFiMaskLentTec.TabIndex = 154;
            this.LblSiFiMaskLentTec.Text = "TecBos:";
            // 
            // TxtSiFiMaskNrBackTec
            // 
            this.TxtSiFiMaskNrBackTec.Enabled = false;
            this.TxtSiFiMaskNrBackTec.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSiFiMaskNrBackTec.Location = new System.Drawing.Point(165, 149);
            this.TxtSiFiMaskNrBackTec.Name = "TxtSiFiMaskNrBackTec";
            this.TxtSiFiMaskNrBackTec.Size = new System.Drawing.Size(96, 21);
            this.TxtSiFiMaskNrBackTec.TabIndex = 145;
            // 
            // TxtSiFiMaskNrLentTec
            // 
            this.TxtSiFiMaskNrLentTec.Enabled = false;
            this.TxtSiFiMaskNrLentTec.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSiFiMaskNrLentTec.Location = new System.Drawing.Point(165, 37);
            this.TxtSiFiMaskNrLentTec.Name = "TxtSiFiMaskNrLentTec";
            this.TxtSiFiMaskNrLentTec.Size = new System.Drawing.Size(96, 21);
            this.TxtSiFiMaskNrLentTec.TabIndex = 142;
            // 
            // LblSiFiMaskNrBackTec
            // 
            this.LblSiFiMaskNrBackTec.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiFiMaskNrBackTec.Location = new System.Drawing.Point(93, 149);
            this.LblSiFiMaskNrBackTec.Name = "LblSiFiMaskNrBackTec";
            this.LblSiFiMaskNrBackTec.Size = new System.Drawing.Size(72, 23);
            this.LblSiFiMaskNrBackTec.TabIndex = 149;
            this.LblSiFiMaskNrBackTec.Text = "Maske-Nr.";
            // 
            // LblSiFiMaskNrLentTec
            // 
            this.LblSiFiMaskNrLentTec.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiFiMaskNrLentTec.Location = new System.Drawing.Point(93, 40);
            this.LblSiFiMaskNrLentTec.Name = "LblSiFiMaskNrLentTec";
            this.LblSiFiMaskNrLentTec.Size = new System.Drawing.Size(72, 23);
            this.LblSiFiMaskNrLentTec.TabIndex = 148;
            this.LblSiFiMaskNrLentTec.Text = "Maske-Nr.";
            // 
            // DatSiFiMaskBackOnTec
            // 
            this.DatSiFiMaskBackOnTec.CustomFormat = "dd.MM.yyyy";
            this.DatSiFiMaskBackOnTec.Enabled = false;
            this.DatSiFiMaskBackOnTec.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatSiFiMaskBackOnTec.Location = new System.Drawing.Point(623, 146);
            this.DatSiFiMaskBackOnTec.Name = "DatSiFiMaskBackOnTec";
            this.DatSiFiMaskBackOnTec.Size = new System.Drawing.Size(88, 20);
            this.DatSiFiMaskBackOnTec.TabIndex = 146;
            this.DatSiFiMaskBackOnTec.Value = new System.DateTime(2003, 10, 6, 0, 0, 0, 0);
            // 
            // DatSiFiMaskLentOnTec
            // 
            this.DatSiFiMaskLentOnTec.CustomFormat = "dd.MM.yyyy";
            this.DatSiFiMaskLentOnTec.Enabled = false;
            this.DatSiFiMaskLentOnTec.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatSiFiMaskLentOnTec.Location = new System.Drawing.Point(623, 38);
            this.DatSiFiMaskLentOnTec.Name = "DatSiFiMaskLentOnTec";
            this.DatSiFiMaskLentOnTec.Size = new System.Drawing.Size(88, 20);
            this.DatSiFiMaskLentOnTec.TabIndex = 143;
            this.DatSiFiMaskLentOnTec.Value = new System.DateTime(2003, 10, 6, 0, 0, 0, 0);
            // 
            // LblSiFiMaskBackByTec
            // 
            this.LblSiFiMaskBackByTec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblSiFiMaskBackByTec.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiFiMaskBackByTec.Location = new System.Drawing.Point(720, 149);
            this.LblSiFiMaskBackByTec.Name = "LblSiFiMaskBackByTec";
            this.LblSiFiMaskBackByTec.Size = new System.Drawing.Size(54, 23);
            this.LblSiFiMaskBackByTec.TabIndex = 153;
            this.LblSiFiMaskBackByTec.Text = "durch";
            this.LblSiFiMaskBackByTec.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // TxtSiFiMaskBackByTec
            // 
            this.TxtSiFiMaskBackByTec.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSiFiMaskBackByTec.Location = new System.Drawing.Point(782, 145);
            this.TxtSiFiMaskBackByTec.Name = "TxtSiFiMaskBackByTec";
            this.TxtSiFiMaskBackByTec.ReadOnly = true;
            this.TxtSiFiMaskBackByTec.Size = new System.Drawing.Size(230, 21);
            this.TxtSiFiMaskBackByTec.TabIndex = 147;
            this.TxtSiFiMaskBackByTec.TabStop = false;
            // 
            // LblSiFiMaskLentByTec
            // 
            this.LblSiFiMaskLentByTec.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiFiMaskLentByTec.Location = new System.Drawing.Point(721, 40);
            this.LblSiFiMaskLentByTec.Name = "LblSiFiMaskLentByTec";
            this.LblSiFiMaskLentByTec.Size = new System.Drawing.Size(53, 23);
            this.LblSiFiMaskLentByTec.TabIndex = 152;
            this.LblSiFiMaskLentByTec.Text = "durch";
            this.LblSiFiMaskLentByTec.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // TxtSiFiMaskLentByTec
            // 
            this.TxtSiFiMaskLentByTec.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSiFiMaskLentByTec.Location = new System.Drawing.Point(782, 39);
            this.TxtSiFiMaskLentByTec.Name = "TxtSiFiMaskLentByTec";
            this.TxtSiFiMaskLentByTec.ReadOnly = true;
            this.TxtSiFiMaskLentByTec.Size = new System.Drawing.Size(230, 21);
            this.TxtSiFiMaskLentByTec.TabIndex = 144;
            this.TxtSiFiMaskLentByTec.TabStop = false;
            // 
            // LblSiFiMaskBackOnTec
            // 
            this.LblSiFiMaskBackOnTec.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiFiMaskBackOnTec.Location = new System.Drawing.Point(514, 149);
            this.LblSiFiMaskBackOnTec.Name = "LblSiFiMaskBackOnTec";
            this.LblSiFiMaskBackOnTec.Size = new System.Drawing.Size(100, 23);
            this.LblSiFiMaskBackOnTec.TabIndex = 151;
            this.LblSiFiMaskBackOnTec.Text = "Rückgabe am";
            this.LblSiFiMaskBackOnTec.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // LblSiFiMaskLentOnTec
            // 
            this.LblSiFiMaskLentOnTec.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiFiMaskLentOnTec.Location = new System.Drawing.Point(504, 40);
            this.LblSiFiMaskLentOnTec.Name = "LblSiFiMaskLentOnTec";
            this.LblSiFiMaskLentOnTec.Size = new System.Drawing.Size(110, 23);
            this.LblSiFiMaskLentOnTec.TabIndex = 150;
            this.LblSiFiMaskLentOnTec.Text = "Verleih am";
            this.LblSiFiMaskLentOnTec.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(12, 104);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1203, 5);
            this.groupBox1.TabIndex = 138;
            this.groupBox1.TabStop = false;
            // 
            // GrpSiFi1
            // 
            this.GrpSiFi1.Location = new System.Drawing.Point(12, 104);
            this.GrpSiFi1.Name = "GrpSiFi1";
            this.GrpSiFi1.Size = new System.Drawing.Size(1203, 5);
            this.GrpSiFi1.TabIndex = 138;
            this.GrpSiFi1.TabStop = false;
            // 
            // LblSiFiMaskLentTitle
            // 
            this.LblSiFiMaskLentTitle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiFiMaskLentTitle.Location = new System.Drawing.Point(14, 10);
            this.LblSiFiMaskLentTitle.Name = "LblSiFiMaskLentTitle";
            this.LblSiFiMaskLentTitle.Size = new System.Drawing.Size(236, 19);
            this.LblSiFiMaskLentTitle.TabIndex = 141;
            this.LblSiFiMaskLentTitle.Text = "Verleih Atemschutzmasken";
            // 
            // LblSiFiMaskTitleFlo
            // 
            this.LblSiFiMaskTitleFlo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiFiMaskTitleFlo.Location = new System.Drawing.Point(14, 121);
            this.LblSiFiMaskTitleFlo.Name = "LblSiFiMaskTitleFlo";
            this.LblSiFiMaskTitleFlo.Size = new System.Drawing.Size(236, 19);
            this.LblSiFiMaskTitleFlo.TabIndex = 140;
            this.LblSiFiMaskTitleFlo.Text = "Rückgabe Atemschutzmasken";
            // 
            // BtnSiFiMaskTicket
            // 
            this.BtnSiFiMaskTicket.Enabled = false;
            this.BtnSiFiMaskTicket.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSiFiMaskTicket.Location = new System.Drawing.Point(380, 170);
            this.BtnSiFiMaskTicket.Name = "BtnSiFiMaskTicket";
            this.BtnSiFiMaskTicket.Size = new System.Drawing.Size(108, 30);
            this.BtnSiFiMaskTicket.TabIndex = 139;
            this.BtnSiFiMaskTicket.Text = "&Beleg drucken";
            this.BtnSiFiMaskTicket.Click += new System.EventHandler(this.BtnSiFiMaskTicket_Click);
            // 
            // LblSiFiMaskBackFlo
            // 
            this.LblSiFiMaskBackFlo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiFiMaskBackFlo.Location = new System.Drawing.Point(27, 181);
            this.LblSiFiMaskBackFlo.Name = "LblSiFiMaskBackFlo";
            this.LblSiFiMaskBackFlo.Size = new System.Drawing.Size(59, 23);
            this.LblSiFiMaskBackFlo.TabIndex = 137;
            this.LblSiFiMaskBackFlo.Text = "Florix:";
            // 
            // LblSiFiMaskLentFlo
            // 
            this.LblSiFiMaskLentFlo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiFiMaskLentFlo.Location = new System.Drawing.Point(27, 69);
            this.LblSiFiMaskLentFlo.Name = "LblSiFiMaskLentFlo";
            this.LblSiFiMaskLentFlo.Size = new System.Drawing.Size(43, 23);
            this.LblSiFiMaskLentFlo.TabIndex = 136;
            this.LblSiFiMaskLentFlo.Text = "Florix:";
            // 
            // TxtSiFiMaskNrBackFlo
            // 
            this.TxtSiFiMaskNrBackFlo.Enabled = false;
            this.TxtSiFiMaskNrBackFlo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSiFiMaskNrBackFlo.Location = new System.Drawing.Point(165, 179);
            this.TxtSiFiMaskNrBackFlo.Name = "TxtSiFiMaskNrBackFlo";
            this.TxtSiFiMaskNrBackFlo.Size = new System.Drawing.Size(96, 21);
            this.TxtSiFiMaskNrBackFlo.TabIndex = 127;
            // 
            // TxtSiFiMaskNrLentFlo
            // 
            this.TxtSiFiMaskNrLentFlo.Enabled = false;
            this.TxtSiFiMaskNrLentFlo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSiFiMaskNrLentFlo.Location = new System.Drawing.Point(165, 68);
            this.TxtSiFiMaskNrLentFlo.Name = "TxtSiFiMaskNrLentFlo";
            this.TxtSiFiMaskNrLentFlo.Size = new System.Drawing.Size(96, 21);
            this.TxtSiFiMaskNrLentFlo.TabIndex = 124;
            // 
            // LblSiFiMaskNrBackFlo
            // 
            this.LblSiFiMaskNrBackFlo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiFiMaskNrBackFlo.Location = new System.Drawing.Point(93, 181);
            this.LblSiFiMaskNrBackFlo.Name = "LblSiFiMaskNrBackFlo";
            this.LblSiFiMaskNrBackFlo.Size = new System.Drawing.Size(72, 23);
            this.LblSiFiMaskNrBackFlo.TabIndex = 131;
            this.LblSiFiMaskNrBackFlo.Text = "Maske-Nr.";
            // 
            // LblSiFiMaskNrLentFlo
            // 
            this.LblSiFiMaskNrLentFlo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiFiMaskNrLentFlo.Location = new System.Drawing.Point(93, 69);
            this.LblSiFiMaskNrLentFlo.Name = "LblSiFiMaskNrLentFlo";
            this.LblSiFiMaskNrLentFlo.Size = new System.Drawing.Size(72, 23);
            this.LblSiFiMaskNrLentFlo.TabIndex = 130;
            this.LblSiFiMaskNrLentFlo.Text = "Maske-Nr.";
            // 
            // DatSiFiMaskBackOnFlo
            // 
            this.DatSiFiMaskBackOnFlo.CustomFormat = "dd.MM.yyyy";
            this.DatSiFiMaskBackOnFlo.Enabled = false;
            this.DatSiFiMaskBackOnFlo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatSiFiMaskBackOnFlo.Location = new System.Drawing.Point(623, 179);
            this.DatSiFiMaskBackOnFlo.Name = "DatSiFiMaskBackOnFlo";
            this.DatSiFiMaskBackOnFlo.Size = new System.Drawing.Size(88, 20);
            this.DatSiFiMaskBackOnFlo.TabIndex = 128;
            this.DatSiFiMaskBackOnFlo.Value = new System.DateTime(2003, 10, 6, 0, 0, 0, 0);
            // 
            // DatSiFiMaskLentOnFlo
            // 
            this.DatSiFiMaskLentOnFlo.CustomFormat = "dd.MM.yyyy";
            this.DatSiFiMaskLentOnFlo.Enabled = false;
            this.DatSiFiMaskLentOnFlo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatSiFiMaskLentOnFlo.Location = new System.Drawing.Point(623, 69);
            this.DatSiFiMaskLentOnFlo.Name = "DatSiFiMaskLentOnFlo";
            this.DatSiFiMaskLentOnFlo.Size = new System.Drawing.Size(88, 20);
            this.DatSiFiMaskLentOnFlo.TabIndex = 125;
            this.DatSiFiMaskLentOnFlo.Value = new System.DateTime(2003, 10, 6, 0, 0, 0, 0);
            // 
            // LblSiFiMaskBackByFlo
            // 
            this.LblSiFiMaskBackByFlo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblSiFiMaskBackByFlo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiFiMaskBackByFlo.Location = new System.Drawing.Point(722, 181);
            this.LblSiFiMaskBackByFlo.Name = "LblSiFiMaskBackByFlo";
            this.LblSiFiMaskBackByFlo.Size = new System.Drawing.Size(52, 23);
            this.LblSiFiMaskBackByFlo.TabIndex = 135;
            this.LblSiFiMaskBackByFlo.Text = "durch";
            this.LblSiFiMaskBackByFlo.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // TxtSiFiMaskBackByFlo
            // 
            this.TxtSiFiMaskBackByFlo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSiFiMaskBackByFlo.Location = new System.Drawing.Point(782, 179);
            this.TxtSiFiMaskBackByFlo.Name = "TxtSiFiMaskBackByFlo";
            this.TxtSiFiMaskBackByFlo.ReadOnly = true;
            this.TxtSiFiMaskBackByFlo.Size = new System.Drawing.Size(230, 21);
            this.TxtSiFiMaskBackByFlo.TabIndex = 129;
            this.TxtSiFiMaskBackByFlo.TabStop = false;
            // 
            // LblSiFiMaskLentByFlo
            // 
            this.LblSiFiMaskLentByFlo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiFiMaskLentByFlo.Location = new System.Drawing.Point(720, 71);
            this.LblSiFiMaskLentByFlo.Name = "LblSiFiMaskLentByFlo";
            this.LblSiFiMaskLentByFlo.Size = new System.Drawing.Size(54, 23);
            this.LblSiFiMaskLentByFlo.TabIndex = 134;
            this.LblSiFiMaskLentByFlo.Text = "durch";
            this.LblSiFiMaskLentByFlo.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // TxtSiFiMaskLentByFlo
            // 
            this.TxtSiFiMaskLentByFlo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSiFiMaskLentByFlo.Location = new System.Drawing.Point(782, 69);
            this.TxtSiFiMaskLentByFlo.Name = "TxtSiFiMaskLentByFlo";
            this.TxtSiFiMaskLentByFlo.ReadOnly = true;
            this.TxtSiFiMaskLentByFlo.Size = new System.Drawing.Size(230, 21);
            this.TxtSiFiMaskLentByFlo.TabIndex = 126;
            this.TxtSiFiMaskLentByFlo.TabStop = false;
            // 
            // LblSiFiMaskBackOnFlo
            // 
            this.LblSiFiMaskBackOnFlo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiFiMaskBackOnFlo.Location = new System.Drawing.Point(494, 181);
            this.LblSiFiMaskBackOnFlo.Name = "LblSiFiMaskBackOnFlo";
            this.LblSiFiMaskBackOnFlo.Size = new System.Drawing.Size(120, 23);
            this.LblSiFiMaskBackOnFlo.TabIndex = 133;
            this.LblSiFiMaskBackOnFlo.Text = "Rückgabe am";
            this.LblSiFiMaskBackOnFlo.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // LblSiFiMaskLentOnFlo
            // 
            this.LblSiFiMaskLentOnFlo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiFiMaskLentOnFlo.Location = new System.Drawing.Point(517, 71);
            this.LblSiFiMaskLentOnFlo.Name = "LblSiFiMaskLentOnFlo";
            this.LblSiFiMaskLentOnFlo.Size = new System.Drawing.Size(97, 23);
            this.LblSiFiMaskLentOnFlo.TabIndex = 132;
            this.LblSiFiMaskLentOnFlo.Text = "Verleih am";
            this.LblSiFiMaskLentOnFlo.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // PnlSiFiAllBriefings
            // 
            this.PnlSiFiAllBriefings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlSiFiAllBriefings.Controls.Add(this.label3);
            this.PnlSiFiAllBriefings.Controls.Add(this.TxtSiFiSiteSecurityBriefingDoneByG26_2);
            this.PnlSiFiAllBriefings.Controls.Add(this.LblSiFiSiteSecurityBriefingDoneByG26_2);
            this.PnlSiFiAllBriefings.Controls.Add(this.LblSiFiSiteSecurityBriefingDoneG26_2);
            this.PnlSiFiAllBriefings.Controls.Add(this.DatSiFiSiteSecurityBriefingDoneOnG26_2);
            this.PnlSiFiAllBriefings.Controls.Add(this.PnlSiFiSiteSecurityBriefingG26_2);
            this.PnlSiFiAllBriefings.Controls.Add(this.CbxSiFiSiteSecurityBriefingDoneG26_2);
            this.PnlSiFiAllBriefings.Controls.Add(this.LblSiFiSiteSecurityBriefingDoneOnG26_3);
            this.PnlSiFiAllBriefings.Controls.Add(this.LblSiFiFiremanDoneOn);
            this.PnlSiFiAllBriefings.Controls.Add(this.TxtSiFiSiteSecurityBriefingDoneByG26_3);
            this.PnlSiFiAllBriefings.Controls.Add(this.CbxSiFiFireman);
            this.PnlSiFiAllBriefings.Controls.Add(this.LblSiFiSiteSecurityBriefingDoneByG26_3);
            this.PnlSiFiAllBriefings.Controls.Add(this.panel1);
            this.PnlSiFiAllBriefings.Controls.Add(this.LblSiFiSiteSecurityBriefingDoneG26_3);
            this.PnlSiFiAllBriefings.Controls.Add(this.DatSiFiFiremanDoneOn);
            this.PnlSiFiAllBriefings.Controls.Add(this.DatSiFiSiteSecurityBriefingDoneOnG26_3);
            this.PnlSiFiAllBriefings.Controls.Add(this.label6);
            this.PnlSiFiAllBriefings.Controls.Add(this.PnlSiFiSiteSecurityBriefingG26_3);
            this.PnlSiFiAllBriefings.Controls.Add(this.LblSiFiFiremanDoneBy);
            this.PnlSiFiAllBriefings.Controls.Add(this.CbxSiFiSiteSecurityBriefingDoneG26_3);
            this.PnlSiFiAllBriefings.Controls.Add(this.TxtSiFiFiremanDoneBy);
            this.PnlSiFiAllBriefings.Controls.Add(this.LblSiFiSiteSecurityBriefingDoneOnG26_2);
            this.PnlSiFiAllBriefings.Location = new System.Drawing.Point(8, 326);
            this.PnlSiFiAllBriefings.Name = "PnlSiFiAllBriefings";
            this.PnlSiFiAllBriefings.Size = new System.Drawing.Size(1228, 256);
            this.PnlSiFiAllBriefings.TabIndex = 124;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(14, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(192, 24);
            this.label3.TabIndex = 111;
            this.label3.Text = "Anerkennung / Ausbildung für:";
            // 
            // TxtSiFiSiteSecurityBriefingDoneByG26_2
            // 
            this.TxtSiFiSiteSecurityBriefingDoneByG26_2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSiFiSiteSecurityBriefingDoneByG26_2.Location = new System.Drawing.Point(782, 76);
            this.TxtSiFiSiteSecurityBriefingDoneByG26_2.Name = "TxtSiFiSiteSecurityBriefingDoneByG26_2";
            this.TxtSiFiSiteSecurityBriefingDoneByG26_2.ReadOnly = true;
            this.TxtSiFiSiteSecurityBriefingDoneByG26_2.Size = new System.Drawing.Size(230, 21);
            this.TxtSiFiSiteSecurityBriefingDoneByG26_2.TabIndex = 15;
            this.TxtSiFiSiteSecurityBriefingDoneByG26_2.TabStop = false;
            // 
            // LblSiFiSiteSecurityBriefingDoneByG26_2
            // 
            this.LblSiFiSiteSecurityBriefingDoneByG26_2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiFiSiteSecurityBriefingDoneByG26_2.Location = new System.Drawing.Point(735, 76);
            this.LblSiFiSiteSecurityBriefingDoneByG26_2.Name = "LblSiFiSiteSecurityBriefingDoneByG26_2";
            this.LblSiFiSiteSecurityBriefingDoneByG26_2.Size = new System.Drawing.Size(40, 23);
            this.LblSiFiSiteSecurityBriefingDoneByG26_2.TabIndex = 14;
            this.LblSiFiSiteSecurityBriefingDoneByG26_2.Text = "durch";
            // 
            // LblSiFiSiteSecurityBriefingDoneG26_2
            // 
            this.LblSiFiSiteSecurityBriefingDoneG26_2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiFiSiteSecurityBriefingDoneG26_2.Location = new System.Drawing.Point(60, 76);
            this.LblSiFiSiteSecurityBriefingDoneG26_2.Name = "LblSiFiSiteSecurityBriefingDoneG26_2";
            this.LblSiFiSiteSecurityBriefingDoneG26_2.Size = new System.Drawing.Size(344, 23);
            this.LblSiFiSiteSecurityBriefingDoneG26_2.TabIndex = 12;
            this.LblSiFiSiteSecurityBriefingDoneG26_2.Text = "Atemschutzgeräteträger erfolgt";
            // 
            // DatSiFiSiteSecurityBriefingDoneOnG26_2
            // 
            this.DatSiFiSiteSecurityBriefingDoneOnG26_2.CustomFormat = "dd.MM.yyyy";
            this.DatSiFiSiteSecurityBriefingDoneOnG26_2.Enabled = false;
            this.DatSiFiSiteSecurityBriefingDoneOnG26_2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatSiFiSiteSecurityBriefingDoneOnG26_2.Location = new System.Drawing.Point(479, 76);
            this.DatSiFiSiteSecurityBriefingDoneOnG26_2.Name = "DatSiFiSiteSecurityBriefingDoneOnG26_2";
            this.DatSiFiSiteSecurityBriefingDoneOnG26_2.Size = new System.Drawing.Size(88, 20);
            this.DatSiFiSiteSecurityBriefingDoneOnG26_2.TabIndex = 14;
            this.DatSiFiSiteSecurityBriefingDoneOnG26_2.Value = new System.DateTime(2003, 10, 6, 0, 0, 0, 0);
            this.DatSiFiSiteSecurityBriefingDoneOnG26_2.Leave += new System.EventHandler(this.DatSiFiSiteSecurityBriefingDoneOnG26_2_Leave);
            // 
            // PnlSiFiSiteSecurityBriefingG26_2
            // 
            this.PnlSiFiSiteSecurityBriefingG26_2.Controls.Add(this.RbtSiFiSiteSecurityBriefingG26_2);
            this.PnlSiFiSiteSecurityBriefingG26_2.Controls.Add(this.LblSiFiSiteSecurityBriefingG26_2);
            this.PnlSiFiSiteSecurityBriefingG26_2.Location = new System.Drawing.Point(12, 34);
            this.PnlSiFiSiteSecurityBriefingG26_2.Name = "PnlSiFiSiteSecurityBriefingG26_2";
            this.PnlSiFiSiteSecurityBriefingG26_2.Size = new System.Drawing.Size(408, 32);
            this.PnlSiFiSiteSecurityBriefingG26_2.TabIndex = 12;
            // 
            // RbtSiFiSiteSecurityBriefingG26_2
            // 
            this.RbtSiFiSiteSecurityBriefingG26_2.Enabled = false;
            this.RbtSiFiSiteSecurityBriefingG26_2.Location = new System.Drawing.Point(16, 14);
            this.RbtSiFiSiteSecurityBriefingG26_2.Name = "RbtSiFiSiteSecurityBriefingG26_2";
            this.RbtSiFiSiteSecurityBriefingG26_2.Size = new System.Drawing.Size(24, 24);
            this.RbtSiFiSiteSecurityBriefingG26_2.TabIndex = 77;
            // 
            // LblSiFiSiteSecurityBriefingG26_2
            // 
            this.LblSiFiSiteSecurityBriefingG26_2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiFiSiteSecurityBriefingG26_2.Location = new System.Drawing.Point(48, 17);
            this.LblSiFiSiteSecurityBriefingG26_2.Name = "LblSiFiSiteSecurityBriefingG26_2";
            this.LblSiFiSiteSecurityBriefingG26_2.Size = new System.Drawing.Size(328, 24);
            this.LblSiFiSiteSecurityBriefingG26_2.TabIndex = 90;
            this.LblSiFiSiteSecurityBriefingG26_2.Text = "Atemschutzgeräteträger";
            // 
            // CbxSiFiSiteSecurityBriefingDoneG26_2
            // 
            this.CbxSiFiSiteSecurityBriefingDoneG26_2.Enabled = false;
            this.CbxSiFiSiteSecurityBriefingDoneG26_2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Millimeter, ((byte)(0)));
            this.CbxSiFiSiteSecurityBriefingDoneG26_2.ForeColor = System.Drawing.SystemColors.Control;
            this.CbxSiFiSiteSecurityBriefingDoneG26_2.Location = new System.Drawing.Point(28, 70);
            this.CbxSiFiSiteSecurityBriefingDoneG26_2.Name = "CbxSiFiSiteSecurityBriefingDoneG26_2";
            this.CbxSiFiSiteSecurityBriefingDoneG26_2.Size = new System.Drawing.Size(24, 24);
            this.CbxSiFiSiteSecurityBriefingDoneG26_2.TabIndex = 13;
            this.CbxSiFiSiteSecurityBriefingDoneG26_2.Text = "B";
            // 
            // LblSiFiSiteSecurityBriefingDoneOnG26_3
            // 
            this.LblSiFiSiteSecurityBriefingDoneOnG26_3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiFiSiteSecurityBriefingDoneOnG26_3.Location = new System.Drawing.Point(443, 143);
            this.LblSiFiSiteSecurityBriefingDoneOnG26_3.Name = "LblSiFiSiteSecurityBriefingDoneOnG26_3";
            this.LblSiFiSiteSecurityBriefingDoneOnG26_3.Size = new System.Drawing.Size(32, 23);
            this.LblSiFiSiteSecurityBriefingDoneOnG26_3.TabIndex = 109;
            this.LblSiFiSiteSecurityBriefingDoneOnG26_3.Text = "am";
            // 
            // LblSiFiFiremanDoneOn
            // 
            this.LblSiFiFiremanDoneOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiFiFiremanDoneOn.Location = new System.Drawing.Point(443, 210);
            this.LblSiFiFiremanDoneOn.Name = "LblSiFiFiremanDoneOn";
            this.LblSiFiFiremanDoneOn.Size = new System.Drawing.Size(32, 23);
            this.LblSiFiFiremanDoneOn.TabIndex = 114;
            this.LblSiFiFiremanDoneOn.Text = "am";
            // 
            // TxtSiFiSiteSecurityBriefingDoneByG26_3
            // 
            this.TxtSiFiSiteSecurityBriefingDoneByG26_3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSiFiSiteSecurityBriefingDoneByG26_3.Location = new System.Drawing.Point(782, 143);
            this.TxtSiFiSiteSecurityBriefingDoneByG26_3.Name = "TxtSiFiSiteSecurityBriefingDoneByG26_3";
            this.TxtSiFiSiteSecurityBriefingDoneByG26_3.ReadOnly = true;
            this.TxtSiFiSiteSecurityBriefingDoneByG26_3.Size = new System.Drawing.Size(230, 21);
            this.TxtSiFiSiteSecurityBriefingDoneByG26_3.TabIndex = 18;
            this.TxtSiFiSiteSecurityBriefingDoneByG26_3.TabStop = false;
            // 
            // CbxSiFiFireman
            // 
            this.CbxSiFiFireman.Enabled = false;
            this.CbxSiFiFireman.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Millimeter, ((byte)(0)));
            this.CbxSiFiFireman.ForeColor = System.Drawing.SystemColors.Control;
            this.CbxSiFiFireman.Location = new System.Drawing.Point(28, 202);
            this.CbxSiFiFireman.Name = "CbxSiFiFireman";
            this.CbxSiFiFireman.Size = new System.Drawing.Size(24, 24);
            this.CbxSiFiFireman.TabIndex = 19;
            this.CbxSiFiFireman.Text = "B";
            // 
            // LblSiFiSiteSecurityBriefingDoneByG26_3
            // 
            this.LblSiFiSiteSecurityBriefingDoneByG26_3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiFiSiteSecurityBriefingDoneByG26_3.Location = new System.Drawing.Point(735, 143);
            this.LblSiFiSiteSecurityBriefingDoneByG26_3.Name = "LblSiFiSiteSecurityBriefingDoneByG26_3";
            this.LblSiFiSiteSecurityBriefingDoneByG26_3.Size = new System.Drawing.Size(40, 23);
            this.LblSiFiSiteSecurityBriefingDoneByG26_3.TabIndex = 110;
            this.LblSiFiSiteSecurityBriefingDoneByG26_3.Text = "durch";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.RbtSiFiFireman);
            this.panel1.Controls.Add(this.LblSiFiFireman);
            this.panel1.Location = new System.Drawing.Point(12, 168);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(408, 32);
            this.panel1.TabIndex = 112;
            // 
            // RbtSiFiFireman
            // 
            this.RbtSiFiFireman.Enabled = false;
            this.RbtSiFiFireman.Location = new System.Drawing.Point(16, 13);
            this.RbtSiFiFireman.Name = "RbtSiFiFireman";
            this.RbtSiFiFireman.Size = new System.Drawing.Size(24, 24);
            this.RbtSiFiFireman.TabIndex = 77;
            // 
            // LblSiFiFireman
            // 
            this.LblSiFiFireman.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiFiFireman.Location = new System.Drawing.Point(48, 16);
            this.LblSiFiFireman.Name = "LblSiFiFireman";
            this.LblSiFiFireman.Size = new System.Drawing.Size(328, 24);
            this.LblSiFiFireman.TabIndex = 90;
            this.LblSiFiFireman.Text = "Brandsicherheitsposten angeordnet";
            // 
            // LblSiFiSiteSecurityBriefingDoneG26_3
            // 
            this.LblSiFiSiteSecurityBriefingDoneG26_3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiFiSiteSecurityBriefingDoneG26_3.Location = new System.Drawing.Point(60, 143);
            this.LblSiFiSiteSecurityBriefingDoneG26_3.Name = "LblSiFiSiteSecurityBriefingDoneG26_3";
            this.LblSiFiSiteSecurityBriefingDoneG26_3.Size = new System.Drawing.Size(336, 23);
            this.LblSiFiSiteSecurityBriefingDoneG26_3.TabIndex = 108;
            this.LblSiFiSiteSecurityBriefingDoneG26_3.Text = "Atemschutzgeräteträger Rettung erfolgt";
            // 
            // DatSiFiFiremanDoneOn
            // 
            this.DatSiFiFiremanDoneOn.CustomFormat = "dd.MM.yyyy";
            this.DatSiFiFiremanDoneOn.Enabled = false;
            this.DatSiFiFiremanDoneOn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatSiFiFiremanDoneOn.Location = new System.Drawing.Point(479, 210);
            this.DatSiFiFiremanDoneOn.Name = "DatSiFiFiremanDoneOn";
            this.DatSiFiFiremanDoneOn.Size = new System.Drawing.Size(88, 20);
            this.DatSiFiFiremanDoneOn.TabIndex = 20;
            this.DatSiFiFiremanDoneOn.Value = new System.DateTime(2003, 10, 6, 0, 0, 0, 0);
            this.DatSiFiFiremanDoneOn.Leave += new System.EventHandler(this.DatSiFiFiremanDoneOn_Leave);
            // 
            // DatSiFiSiteSecurityBriefingDoneOnG26_3
            // 
            this.DatSiFiSiteSecurityBriefingDoneOnG26_3.CustomFormat = "dd.MM.yyyy";
            this.DatSiFiSiteSecurityBriefingDoneOnG26_3.Enabled = false;
            this.DatSiFiSiteSecurityBriefingDoneOnG26_3.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatSiFiSiteSecurityBriefingDoneOnG26_3.Location = new System.Drawing.Point(479, 143);
            this.DatSiFiSiteSecurityBriefingDoneOnG26_3.Name = "DatSiFiSiteSecurityBriefingDoneOnG26_3";
            this.DatSiFiSiteSecurityBriefingDoneOnG26_3.Size = new System.Drawing.Size(88, 20);
            this.DatSiFiSiteSecurityBriefingDoneOnG26_3.TabIndex = 17;
            this.DatSiFiSiteSecurityBriefingDoneOnG26_3.Value = new System.DateTime(2003, 10, 6, 0, 0, 0, 0);
            this.DatSiFiSiteSecurityBriefingDoneOnG26_3.Leave += new System.EventHandler(this.DatSiFiSiteSecurityBriefingDoneOnG26_3_Leave);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(60, 210);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(344, 23);
            this.label6.TabIndex = 113;
            this.label6.Text = "Brandsicherheitsposten erfolgt";
            // 
            // PnlSiFiSiteSecurityBriefingG26_3
            // 
            this.PnlSiFiSiteSecurityBriefingG26_3.Controls.Add(this.RbtSiFiSiteSecurityBriefingG26_3);
            this.PnlSiFiSiteSecurityBriefingG26_3.Controls.Add(this.LblSiFiSiteSecurityBriefingG26_3);
            this.PnlSiFiSiteSecurityBriefingG26_3.Location = new System.Drawing.Point(12, 100);
            this.PnlSiFiSiteSecurityBriefingG26_3.Name = "PnlSiFiSiteSecurityBriefingG26_3";
            this.PnlSiFiSiteSecurityBriefingG26_3.Size = new System.Drawing.Size(408, 32);
            this.PnlSiFiSiteSecurityBriefingG26_3.TabIndex = 16;
            // 
            // RbtSiFiSiteSecurityBriefingG26_3
            // 
            this.RbtSiFiSiteSecurityBriefingG26_3.Enabled = false;
            this.RbtSiFiSiteSecurityBriefingG26_3.Location = new System.Drawing.Point(16, 14);
            this.RbtSiFiSiteSecurityBriefingG26_3.Name = "RbtSiFiSiteSecurityBriefingG26_3";
            this.RbtSiFiSiteSecurityBriefingG26_3.Size = new System.Drawing.Size(24, 24);
            this.RbtSiFiSiteSecurityBriefingG26_3.TabIndex = 77;
            // 
            // LblSiFiSiteSecurityBriefingG26_3
            // 
            this.LblSiFiSiteSecurityBriefingG26_3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiFiSiteSecurityBriefingG26_3.Location = new System.Drawing.Point(48, 17);
            this.LblSiFiSiteSecurityBriefingG26_3.Name = "LblSiFiSiteSecurityBriefingG26_3";
            this.LblSiFiSiteSecurityBriefingG26_3.Size = new System.Drawing.Size(344, 24);
            this.LblSiFiSiteSecurityBriefingG26_3.TabIndex = 90;
            this.LblSiFiSiteSecurityBriefingG26_3.Text = "Atemschutzgeräteträger Rettung angeordnet";
            // 
            // LblSiFiFiremanDoneBy
            // 
            this.LblSiFiFiremanDoneBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiFiFiremanDoneBy.Location = new System.Drawing.Point(735, 210);
            this.LblSiFiFiremanDoneBy.Name = "LblSiFiFiremanDoneBy";
            this.LblSiFiFiremanDoneBy.Size = new System.Drawing.Size(40, 23);
            this.LblSiFiFiremanDoneBy.TabIndex = 116;
            this.LblSiFiFiremanDoneBy.Text = "durch";
            // 
            // CbxSiFiSiteSecurityBriefingDoneG26_3
            // 
            this.CbxSiFiSiteSecurityBriefingDoneG26_3.Enabled = false;
            this.CbxSiFiSiteSecurityBriefingDoneG26_3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Millimeter, ((byte)(0)));
            this.CbxSiFiSiteSecurityBriefingDoneG26_3.ForeColor = System.Drawing.SystemColors.Control;
            this.CbxSiFiSiteSecurityBriefingDoneG26_3.Location = new System.Drawing.Point(28, 135);
            this.CbxSiFiSiteSecurityBriefingDoneG26_3.Name = "CbxSiFiSiteSecurityBriefingDoneG26_3";
            this.CbxSiFiSiteSecurityBriefingDoneG26_3.Size = new System.Drawing.Size(24, 24);
            this.CbxSiFiSiteSecurityBriefingDoneG26_3.TabIndex = 16;
            this.CbxSiFiSiteSecurityBriefingDoneG26_3.Text = "B";
            // 
            // TxtSiFiFiremanDoneBy
            // 
            this.TxtSiFiFiremanDoneBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSiFiFiremanDoneBy.Location = new System.Drawing.Point(782, 210);
            this.TxtSiFiFiremanDoneBy.Name = "TxtSiFiFiremanDoneBy";
            this.TxtSiFiFiremanDoneBy.ReadOnly = true;
            this.TxtSiFiFiremanDoneBy.Size = new System.Drawing.Size(230, 21);
            this.TxtSiFiFiremanDoneBy.TabIndex = 21;
            this.TxtSiFiFiremanDoneBy.TabStop = false;
            // 
            // LblSiFiSiteSecurityBriefingDoneOnG26_2
            // 
            this.LblSiFiSiteSecurityBriefingDoneOnG26_2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiFiSiteSecurityBriefingDoneOnG26_2.Location = new System.Drawing.Point(443, 76);
            this.LblSiFiSiteSecurityBriefingDoneOnG26_2.Name = "LblSiFiSiteSecurityBriefingDoneOnG26_2";
            this.LblSiFiSiteSecurityBriefingDoneOnG26_2.Size = new System.Drawing.Size(32, 23);
            this.LblSiFiSiteSecurityBriefingDoneOnG26_2.TabIndex = 13;
            this.LblSiFiSiteSecurityBriefingDoneOnG26_2.Text = "am";
            // 
            // PnlSiFiMaskBriefing
            // 
            this.PnlSiFiMaskBriefing.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlSiFiMaskBriefing.Controls.Add(this.LblSiFiRespMaskBriefDirBy);
            this.PnlSiFiMaskBriefing.Controls.Add(this.TxtSiFiRespMaskBriefDirBy);
            this.PnlSiFiMaskBriefing.Controls.Add(this.label5);
            this.PnlSiFiMaskBriefing.Controls.Add(this.DatSiFiRespMaskBriefDirOn);
            this.PnlSiFiMaskBriefing.Controls.Add(this.CbxSiFiRespMaskBriefRec);
            this.PnlSiFiMaskBriefing.Controls.Add(this.DatSiFiRespMaskBriefRecOn);
            this.PnlSiFiMaskBriefing.Controls.Add(this.LblSiFiRespiratoryMaskBriefing);
            this.PnlSiFiMaskBriefing.Controls.Add(this.LblSiFiRespiratoryMaskBriefingDoneOn);
            this.PnlSiFiMaskBriefing.Controls.Add(this.LblSiFiRespMaskBriefRecBy);
            this.PnlSiFiMaskBriefing.Controls.Add(this.TxtSiFiRespMaskBriefRecBy);
            this.PnlSiFiMaskBriefing.Controls.Add(this.LblSiFiRespiratoryMaskBriefingDone);
            this.PnlSiFiMaskBriefing.Controls.Add(this.RbtSiFiRespMaskBriefDir);
            this.PnlSiFiMaskBriefing.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PnlSiFiMaskBriefing.Location = new System.Drawing.Point(8, 7);
            this.PnlSiFiMaskBriefing.Name = "PnlSiFiMaskBriefing";
            this.PnlSiFiMaskBriefing.Size = new System.Drawing.Size(1228, 85);
            this.PnlSiFiMaskBriefing.TabIndex = 0;
            // 
            // LblSiFiRespMaskBriefDirBy
            // 
            this.LblSiFiRespMaskBriefDirBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiFiRespMaskBriefDirBy.Location = new System.Drawing.Point(737, 16);
            this.LblSiFiRespMaskBriefDirBy.Name = "LblSiFiRespMaskBriefDirBy";
            this.LblSiFiRespMaskBriefDirBy.Size = new System.Drawing.Size(40, 23);
            this.LblSiFiRespMaskBriefDirBy.TabIndex = 121;
            this.LblSiFiRespMaskBriefDirBy.Text = "durch";
            // 
            // TxtSiFiRespMaskBriefDirBy
            // 
            this.TxtSiFiRespMaskBriefDirBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSiFiRespMaskBriefDirBy.Location = new System.Drawing.Point(782, 16);
            this.TxtSiFiRespMaskBriefDirBy.Name = "TxtSiFiRespMaskBriefDirBy";
            this.TxtSiFiRespMaskBriefDirBy.ReadOnly = true;
            this.TxtSiFiRespMaskBriefDirBy.Size = new System.Drawing.Size(230, 21);
            this.TxtSiFiRespMaskBriefDirBy.TabIndex = 120;
            this.TxtSiFiRespMaskBriefDirBy.TabStop = false;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(445, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 23);
            this.label5.TabIndex = 119;
            this.label5.Text = "am";
            // 
            // DatSiFiRespMaskBriefDirOn
            // 
            this.DatSiFiRespMaskBriefDirOn.CustomFormat = "dd.MM.yyyy";
            this.DatSiFiRespMaskBriefDirOn.Enabled = false;
            this.DatSiFiRespMaskBriefDirOn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatSiFiRespMaskBriefDirOn.Location = new System.Drawing.Point(481, 14);
            this.DatSiFiRespMaskBriefDirOn.Name = "DatSiFiRespMaskBriefDirOn";
            this.DatSiFiRespMaskBriefDirOn.Size = new System.Drawing.Size(88, 21);
            this.DatSiFiRespMaskBriefDirOn.TabIndex = 118;
            this.DatSiFiRespMaskBriefDirOn.Value = new System.DateTime(2003, 10, 6, 0, 0, 0, 0);
            this.DatSiFiRespMaskBriefDirOn.Leave += new System.EventHandler(this.DatSiFiRespMaskBriefDirOn_Leave);
            // 
            // CbxSiFiRespMaskBriefRec
            // 
            this.CbxSiFiRespMaskBriefRec.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Millimeter, ((byte)(0)));
            this.CbxSiFiRespMaskBriefRec.ForeColor = System.Drawing.SystemColors.Control;
            this.CbxSiFiRespMaskBriefRec.Location = new System.Drawing.Point(30, 45);
            this.CbxSiFiRespMaskBriefRec.Name = "CbxSiFiRespMaskBriefRec";
            this.CbxSiFiRespMaskBriefRec.Size = new System.Drawing.Size(24, 20);
            this.CbxSiFiRespMaskBriefRec.TabIndex = 1;
            this.CbxSiFiRespMaskBriefRec.Text = "U";
            this.CbxSiFiRespMaskBriefRec.CheckedChanged += new System.EventHandler(this.CbxSiFiRespMaskBriefRec_CheckedChanged);
            // 
            // DatSiFiRespMaskBriefRecOn
            // 
            this.DatSiFiRespMaskBriefRecOn.CustomFormat = "dd.MM.yyyy";
            this.DatSiFiRespMaskBriefRecOn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatSiFiRespMaskBriefRecOn.Location = new System.Drawing.Point(481, 48);
            this.DatSiFiRespMaskBriefRecOn.Name = "DatSiFiRespMaskBriefRecOn";
            this.DatSiFiRespMaskBriefRecOn.Size = new System.Drawing.Size(88, 21);
            this.DatSiFiRespMaskBriefRecOn.TabIndex = 2;
            this.DatSiFiRespMaskBriefRecOn.Value = new System.DateTime(2003, 10, 6, 0, 0, 0, 0);
            this.DatSiFiRespMaskBriefRecOn.Leave += new System.EventHandler(this.DatSiFiRespMaskBriefRecOn_Leave);
            // 
            // LblSiFiRespiratoryMaskBriefing
            // 
            this.LblSiFiRespiratoryMaskBriefing.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiFiRespiratoryMaskBriefing.Location = new System.Drawing.Point(62, 14);
            this.LblSiFiRespiratoryMaskBriefing.Name = "LblSiFiRespiratoryMaskBriefing";
            this.LblSiFiRespiratoryMaskBriefing.Size = new System.Drawing.Size(280, 24);
            this.LblSiFiRespiratoryMaskBriefing.TabIndex = 89;
            this.LblSiFiRespiratoryMaskBriefing.Text = "Unterweisung Atemschutzmaske angeordnet";
            // 
            // LblSiFiRespiratoryMaskBriefingDoneOn
            // 
            this.LblSiFiRespiratoryMaskBriefingDoneOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiFiRespiratoryMaskBriefingDoneOn.Location = new System.Drawing.Point(445, 48);
            this.LblSiFiRespiratoryMaskBriefingDoneOn.Name = "LblSiFiRespiratoryMaskBriefingDoneOn";
            this.LblSiFiRespiratoryMaskBriefingDoneOn.Size = new System.Drawing.Size(32, 23);
            this.LblSiFiRespiratoryMaskBriefingDoneOn.TabIndex = 48;
            this.LblSiFiRespiratoryMaskBriefingDoneOn.Text = "am";
            // 
            // LblSiFiRespMaskBriefRecBy
            // 
            this.LblSiFiRespMaskBriefRecBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiFiRespMaskBriefRecBy.Location = new System.Drawing.Point(737, 48);
            this.LblSiFiRespMaskBriefRecBy.Name = "LblSiFiRespMaskBriefRecBy";
            this.LblSiFiRespMaskBriefRecBy.Size = new System.Drawing.Size(40, 23);
            this.LblSiFiRespMaskBriefRecBy.TabIndex = 43;
            this.LblSiFiRespMaskBriefRecBy.Text = "durch";
            // 
            // TxtSiFiRespMaskBriefRecBy
            // 
            this.TxtSiFiRespMaskBriefRecBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSiFiRespMaskBriefRecBy.Location = new System.Drawing.Point(782, 48);
            this.TxtSiFiRespMaskBriefRecBy.Name = "TxtSiFiRespMaskBriefRecBy";
            this.TxtSiFiRespMaskBriefRecBy.ReadOnly = true;
            this.TxtSiFiRespMaskBriefRecBy.Size = new System.Drawing.Size(230, 21);
            this.TxtSiFiRespMaskBriefRecBy.TabIndex = 3;
            this.TxtSiFiRespMaskBriefRecBy.TabStop = false;
            // 
            // LblSiFiRespiratoryMaskBriefingDone
            // 
            this.LblSiFiRespiratoryMaskBriefingDone.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiFiRespiratoryMaskBriefingDone.Location = new System.Drawing.Point(62, 48);
            this.LblSiFiRespiratoryMaskBriefingDone.Name = "LblSiFiRespiratoryMaskBriefingDone";
            this.LblSiFiRespiratoryMaskBriefingDone.Size = new System.Drawing.Size(232, 23);
            this.LblSiFiRespiratoryMaskBriefingDone.TabIndex = 38;
            this.LblSiFiRespiratoryMaskBriefingDone.Text = "Unterweisung Atemschutzmaske erteilt";
            // 
            // RbtSiFiRespMaskBriefDir
            // 
            this.RbtSiFiRespMaskBriefDir.BackColor = System.Drawing.SystemColors.Control;
            this.RbtSiFiRespMaskBriefDir.Enabled = false;
            this.RbtSiFiRespMaskBriefDir.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.RbtSiFiRespMaskBriefDir.Location = new System.Drawing.Point(30, 11);
            this.RbtSiFiRespMaskBriefDir.Name = "RbtSiFiRespMaskBriefDir";
            this.RbtSiFiRespMaskBriefDir.Size = new System.Drawing.Size(40, 21);
            this.RbtSiFiRespMaskBriefDir.TabIndex = 77;
            this.RbtSiFiRespMaskBriefDir.UseVisualStyleBackColor = false;
            // 
            // TapSiteMedicalService
            // 
            this.TapSiteMedicalService.Controls.Add(this.PnlTabSiteMedical);
            this.TapSiteMedicalService.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TapSiteMedicalService.Location = new System.Drawing.Point(4, 25);
            this.TapSiteMedicalService.Name = "TapSiteMedicalService";
            this.TapSiteMedicalService.Size = new System.Drawing.Size(1249, 660);
            this.TapSiteMedicalService.TabIndex = 6;
            this.TapSiteMedicalService.Text = "Werkärztlicher Dienst";
            // 
            // PnlTabSiteMedical
            // 
            this.PnlTabSiteMedical.Controls.Add(this.LblSiMedPrecautionaryMedical);
            this.PnlTabSiteMedical.Controls.Add(this.btnDelPrecMed);
            this.PnlTabSiteMedical.Controls.Add(this.CbxSiMedPrecautionaryMedical);
            this.PnlTabSiteMedical.Controls.Add(this.DatSiMedValidUntil);
            this.PnlTabSiteMedical.Controls.Add(this.LblSiMedExecutedBy);
            this.PnlTabSiteMedical.Controls.Add(this.TxtSiMedExecutedBy);
            this.PnlTabSiteMedical.Controls.Add(this.LblSiMedPrecautionaryMedicalBriefing);
            this.PnlTabSiteMedical.Controls.Add(this.LblSiMedValidUntil);
            this.PnlTabSiteMedical.Controls.Add(this.CboSiMedPrecautionaryMedical);
            this.PnlTabSiteMedical.Controls.Add(this.DgrSiMedPrecautionaryMedical);
            this.PnlTabSiteMedical.Controls.Add(this.RbtSiMedPrecautionaryMedicalBriefing);
            this.PnlTabSiteMedical.Enabled = false;
            this.PnlTabSiteMedical.Location = new System.Drawing.Point(0, 0);
            this.PnlTabSiteMedical.Name = "PnlTabSiteMedical";
            this.PnlTabSiteMedical.Size = new System.Drawing.Size(1246, 657);
            this.PnlTabSiteMedical.TabIndex = 0;
            // 
            // LblSiMedPrecautionaryMedical
            // 
            this.LblSiMedPrecautionaryMedical.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiMedPrecautionaryMedical.Location = new System.Drawing.Point(60, 63);
            this.LblSiMedPrecautionaryMedical.Name = "LblSiMedPrecautionaryMedical";
            this.LblSiMedPrecautionaryMedical.Size = new System.Drawing.Size(320, 24);
            this.LblSiMedPrecautionaryMedical.TabIndex = 104;
            this.LblSiMedPrecautionaryMedical.Text = "Bezeichnung Vorsorgeuntersuchung";
            // 
            // btnDelPrecMed
            // 
            this.btnDelPrecMed.Enabled = false;
            this.btnDelPrecMed.Location = new System.Drawing.Point(1078, 127);
            this.btnDelPrecMed.Name = "btnDelPrecMed";
            this.btnDelPrecMed.Size = new System.Drawing.Size(88, 24);
            this.btnDelPrecMed.TabIndex = 7;
            this.btnDelPrecMed.Text = "Löschen";
            this.btnDelPrecMed.Click += new System.EventHandler(this.btnDelPrecMed_Click);
            // 
            // CbxSiMedPrecautionaryMedical
            // 
            this.CbxSiMedPrecautionaryMedical.Checked = true;
            this.CbxSiMedPrecautionaryMedical.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CbxSiMedPrecautionaryMedical.Enabled = false;
            this.CbxSiMedPrecautionaryMedical.Location = new System.Drawing.Point(64, 62);
            this.CbxSiMedPrecautionaryMedical.Name = "CbxSiMedPrecautionaryMedical";
            this.CbxSiMedPrecautionaryMedical.Size = new System.Drawing.Size(16, 24);
            this.CbxSiMedPrecautionaryMedical.TabIndex = 2;
            this.CbxSiMedPrecautionaryMedical.Visible = false;
            // 
            // DatSiMedValidUntil
            // 
            this.DatSiMedValidUntil.CustomFormat = "dd.MM.yyyy";
            this.DatSiMedValidUntil.Enabled = false;
            this.DatSiMedValidUntil.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DatSiMedValidUntil.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatSiMedValidUntil.Location = new System.Drawing.Point(1078, 90);
            this.DatSiMedValidUntil.Name = "DatSiMedValidUntil";
            this.DatSiMedValidUntil.Size = new System.Drawing.Size(88, 21);
            this.DatSiMedValidUntil.TabIndex = 4;
            this.DatSiMedValidUntil.Value = new System.DateTime(2003, 10, 6, 0, 0, 0, 0);
            this.DatSiMedValidUntil.Leave += new System.EventHandler(this.DatSiMedValidUntil_Leave_1);
            // 
            // LblSiMedExecutedBy
            // 
            this.LblSiMedExecutedBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiMedExecutedBy.Location = new System.Drawing.Point(60, 129);
            this.LblSiMedExecutedBy.Name = "LblSiMedExecutedBy";
            this.LblSiMedExecutedBy.Size = new System.Drawing.Size(76, 21);
            this.LblSiMedExecutedBy.TabIndex = 111;
            this.LblSiMedExecutedBy.Text = "erfasst durch";
            // 
            // TxtSiMedExecutedBy
            // 
            this.TxtSiMedExecutedBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSiMedExecutedBy.Location = new System.Drawing.Point(144, 127);
            this.TxtSiMedExecutedBy.MaxLength = 30;
            this.TxtSiMedExecutedBy.Name = "TxtSiMedExecutedBy";
            this.TxtSiMedExecutedBy.ReadOnly = true;
            this.TxtSiMedExecutedBy.Size = new System.Drawing.Size(210, 21);
            this.TxtSiMedExecutedBy.TabIndex = 6;
            this.TxtSiMedExecutedBy.TabStop = false;
            // 
            // LblSiMedPrecautionaryMedicalBriefing
            // 
            this.LblSiMedPrecautionaryMedicalBriefing.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiMedPrecautionaryMedicalBriefing.Location = new System.Drawing.Point(96, 30);
            this.LblSiMedPrecautionaryMedicalBriefing.Name = "LblSiMedPrecautionaryMedicalBriefing";
            this.LblSiMedPrecautionaryMedicalBriefing.Size = new System.Drawing.Size(224, 16);
            this.LblSiMedPrecautionaryMedicalBriefing.TabIndex = 107;
            this.LblSiMedPrecautionaryMedicalBriefing.Text = "Vorsorgeuntersuchung angeordnet";
            // 
            // LblSiMedValidUntil
            // 
            this.LblSiMedValidUntil.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiMedValidUntil.Location = new System.Drawing.Point(1075, 66);
            this.LblSiMedValidUntil.Name = "LblSiMedValidUntil";
            this.LblSiMedValidUntil.Size = new System.Drawing.Size(64, 24);
            this.LblSiMedValidUntil.TabIndex = 106;
            this.LblSiMedValidUntil.Text = "gültig bis";
            // 
            // CboSiMedPrecautionaryMedical
            // 
            this.CboSiMedPrecautionaryMedical.BackColor = System.Drawing.SystemColors.Window;
            this.CboSiMedPrecautionaryMedical.Enabled = false;
            this.CboSiMedPrecautionaryMedical.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboSiMedPrecautionaryMedical.Location = new System.Drawing.Point(60, 90);
            this.CboSiMedPrecautionaryMedical.Name = "CboSiMedPrecautionaryMedical";
            this.CboSiMedPrecautionaryMedical.Size = new System.Drawing.Size(760, 23);
            this.CboSiMedPrecautionaryMedical.TabIndex = 3;
            this.CboSiMedPrecautionaryMedical.SelectedIndexChanged += new System.EventHandler(this.CboSiMedPrecautionaryMedical_SelectedIndexChanged);
            this.CboSiMedPrecautionaryMedical.Enter += new System.EventHandler(this.CboSiMedPrecautionaryMedical_Enter);
            this.CboSiMedPrecautionaryMedical.Leave += new System.EventHandler(this.CboSiMedPrecautionaryMedical_Leave);
            // 
            // DgrSiMedPrecautionaryMedical
            // 
            this.DgrSiMedPrecautionaryMedical.CaptionBackColor = System.Drawing.Color.SteelBlue;
            this.DgrSiMedPrecautionaryMedical.CaptionFont = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DgrSiMedPrecautionaryMedical.CaptionForeColor = System.Drawing.Color.White;
            this.DgrSiMedPrecautionaryMedical.CaptionText = "Vorsorgeuntersuchungen";
            this.DgrSiMedPrecautionaryMedical.DataMember = "";
            this.DgrSiMedPrecautionaryMedical.Enabled = false;
            this.DgrSiMedPrecautionaryMedical.HeaderForeColor = System.Drawing.SystemColors.WindowText;
            this.DgrSiMedPrecautionaryMedical.Location = new System.Drawing.Point(60, 169);
            this.DgrSiMedPrecautionaryMedical.Name = "DgrSiMedPrecautionaryMedical";
            this.DgrSiMedPrecautionaryMedical.PreferredColumnWidth = 115;
            this.DgrSiMedPrecautionaryMedical.ReadOnly = true;
            this.DgrSiMedPrecautionaryMedical.Size = new System.Drawing.Size(1110, 367);
            this.DgrSiMedPrecautionaryMedical.TabIndex = 8;
            this.DgrSiMedPrecautionaryMedical.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.DgrTableStylePrecMed});
            this.DgrSiMedPrecautionaryMedical.CurrentCellChanged += new System.EventHandler(this.DgrSiMedPrecautionaryMedical_CurrentCellChanged);
            this.DgrSiMedPrecautionaryMedical.Enter += new System.EventHandler(this.DgrSiMedPrecautionaryMedical_Enter);
            // 
            // DgrTableStylePrecMed
            // 
            this.DgrTableStylePrecMed.DataGrid = this.DgrSiMedPrecautionaryMedical;
            this.DgrTableStylePrecMed.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.DgrTextBoxPK,
            this.DgrTextBoxType,
            this.DgrTextBoxDescription,
            this.DgrTextBoxValidUntil,
            this.DgrTextBoxExecutedOn,
            this.DgrTextExecutedBy,
            this.DgrTextStatus});
            this.DgrTableStylePrecMed.HeaderFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DgrTableStylePrecMed.HeaderForeColor = System.Drawing.SystemColors.WindowText;
            this.DgrTableStylePrecMed.MappingName = "ArrayList";
            // 
            // DgrTextBoxPK
            // 
            this.DgrTextBoxPK.Format = "";
            this.DgrTextBoxPK.FormatInfo = null;
            this.DgrTextBoxPK.MappingName = "PrecMedID";
            this.DgrTextBoxPK.Width = 0;
            // 
            // DgrTextBoxType
            // 
            this.DgrTextBoxType.Format = "";
            this.DgrTextBoxType.FormatInfo = null;
            this.DgrTextBoxType.HeaderText = "Typ";
            this.DgrTextBoxType.MappingName = "Type";
            this.DgrTextBoxType.Width = 120;
            // 
            // DgrTextBoxDescription
            // 
            this.DgrTextBoxDescription.Format = "";
            this.DgrTextBoxDescription.FormatInfo = null;
            this.DgrTextBoxDescription.HeaderText = "Beschreibung";
            this.DgrTextBoxDescription.MappingName = "Notation";
            this.DgrTextBoxDescription.Width = 360;
            // DgrTextBoxValidUntil
            // 
            this.DgrTextBoxValidUntil.Format = "";
            this.DgrTextBoxValidUntil.FormatInfo = null;
            this.DgrTextBoxValidUntil.HeaderText = "Gültig Bis";
            this.DgrTextBoxValidUntil.MappingName = "ValidUntilAsString";
            this.DgrTextBoxValidUntil.Width = 150;
            // 
            // DgrTextBoxExecutedOn
            // 
            this.DgrTextBoxExecutedOn.Format = "";
            this.DgrTextBoxExecutedOn.FormatInfo = null;
            this.DgrTextBoxExecutedOn.HeaderText = "Anerkannt Am";
            this.DgrTextBoxExecutedOn.MappingName = "PrecMedDateAsString";
            this.DgrTextBoxExecutedOn.Width = 150;
            // 
            // DgrTextExecutedBy
            // 
            this.DgrTextExecutedBy.Format = "";
            this.DgrTextExecutedBy.FormatInfo = null;
            this.DgrTextExecutedBy.HeaderText = "Anerkannt von";
            this.DgrTextExecutedBy.MappingName = "UserName";
            this.DgrTextExecutedBy.Width = 210;
            // 
            // DgrTextStatus
            // 
            this.DgrTextStatus.Format = "";
            this.DgrTextStatus.FormatInfo = null;
            this.DgrTextStatus.HeaderText = "Status";
            this.DgrTextStatus.MappingName = "Status";
            // 
            // RbtSiMedPrecautionaryMedicalBriefing
            // 
            this.RbtSiMedPrecautionaryMedicalBriefing.Enabled = false;
            this.RbtSiMedPrecautionaryMedicalBriefing.Location = new System.Drawing.Point(64, 26);
            this.RbtSiMedPrecautionaryMedicalBriefing.Name = "RbtSiMedPrecautionaryMedicalBriefing";
            this.RbtSiMedPrecautionaryMedicalBriefing.Size = new System.Drawing.Size(40, 24);
            this.RbtSiMedPrecautionaryMedicalBriefing.TabIndex = 1;
            // 
            // TapSafetyAtWork
            // 
            this.TapSafetyAtWork.Controls.Add(this.PnlTabSafetyWork);
            this.TapSafetyAtWork.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TapSafetyAtWork.Location = new System.Drawing.Point(4, 25);
            this.TapSafetyAtWork.Name = "TapSafetyAtWork";
            this.TapSafetyAtWork.Size = new System.Drawing.Size(1249, 660);
            this.TapSafetyAtWork.TabIndex = 5;
            this.TapSafetyAtWork.Text = "Arbeitssicherheit";
            // 
            // PnlTabSafetyWork
            // 
            this.PnlTabSafetyWork.Controls.Add(this.PnlSaAtWoSiteSecurityBriefing);
            this.PnlTabSafetyWork.Enabled = false;
            this.PnlTabSafetyWork.Location = new System.Drawing.Point(0, 0);
            this.PnlTabSafetyWork.Name = "PnlTabSafetyWork";
            this.PnlTabSafetyWork.Size = new System.Drawing.Size(1248, 427);
            this.PnlTabSafetyWork.TabIndex = 0;
            // 
            // PnlSaAtWoSiteSecurityBriefing
            // 
            this.PnlSaAtWoSiteSecurityBriefing.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlSaAtWoSiteSecurityBriefing.Controls.Add(this.GpSaAtWoDummy);
            this.PnlSaAtWoSiteSecurityBriefing.Controls.Add(this.label2);
            this.PnlSaAtWoSiteSecurityBriefing.Controls.Add(this.CbxSaAtWoCranesBriefingDone);
            this.PnlSaAtWoSiteSecurityBriefing.Controls.Add(this.CbxSaAtWoPalletLifterBriefingDone);
            this.PnlSaAtWoSiteSecurityBriefing.Controls.Add(this.CbxSaAtWoSafetyAtWorkBriefingDone);
            this.PnlSaAtWoSiteSecurityBriefing.Controls.Add(this.DatSaAtWoCranesBriefingDoneOn);
            this.PnlSaAtWoSiteSecurityBriefing.Controls.Add(this.DatSaAtWoPalletLifterBriefingDoneOn);
            this.PnlSaAtWoSiteSecurityBriefing.Controls.Add(this.DatSaAtWoSafetyAtWorkBriefingDone);
            this.PnlSaAtWoSiteSecurityBriefing.Controls.Add(this.LblSaAtWoCranesBriefing);
            this.PnlSaAtWoSiteSecurityBriefing.Controls.Add(this.LblSaAtWoPalletLifterBriefing);
            this.PnlSaAtWoSiteSecurityBriefing.Controls.Add(this.LblSaAtWoCranesBriefingDoneBy);
            this.PnlSaAtWoSiteSecurityBriefing.Controls.Add(this.LblSaAtWoCranesBriefingDoneOn);
            this.PnlSaAtWoSiteSecurityBriefing.Controls.Add(this.LblSaAtWoCranesBriefingDone);
            this.PnlSaAtWoSiteSecurityBriefing.Controls.Add(this.TxtSaAtWoCranesBriefingDoneBy);
            this.PnlSaAtWoSiteSecurityBriefing.Controls.Add(this.LblSaAtWoPalletLifterBriefingDoneBy);
            this.PnlSaAtWoSiteSecurityBriefing.Controls.Add(this.LblSaAtWoPalletLifterBriefingDoneOn);
            this.PnlSaAtWoSiteSecurityBriefing.Controls.Add(this.LblSaAtWoPalletLifterBriefingDone);
            this.PnlSaAtWoSiteSecurityBriefing.Controls.Add(this.TxtSaAtWoPalletLifterBriefingDoneBy);
            this.PnlSaAtWoSiteSecurityBriefing.Controls.Add(this.LblSaAtWoSafetyAtWorkBriefing);
            this.PnlSaAtWoSiteSecurityBriefing.Controls.Add(this.LblSaAtWoSafetyAtWorkBriefingDoneBy);
            this.PnlSaAtWoSiteSecurityBriefing.Controls.Add(this.LblSaAtWoSafetyAtWorkBriefingDoneOn);
            this.PnlSaAtWoSiteSecurityBriefing.Controls.Add(this.TxtSaAtWoSafetyAtWorkBriefingDoneBy);
            this.PnlSaAtWoSiteSecurityBriefing.Controls.Add(this.LblSaAtWoSafetyAtWorkBriefingDone);
            this.PnlSaAtWoSiteSecurityBriefing.Controls.Add(this.PnlSaAtWoPalletLifterBriefing);
            this.PnlSaAtWoSiteSecurityBriefing.Controls.Add(this.PnlSaAtWoCranesBriefing);
            this.PnlSaAtWoSiteSecurityBriefing.Controls.Add(this.PnlSaAtWoIndustrialSafetyBriefing);
            this.PnlSaAtWoSiteSecurityBriefing.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PnlSaAtWoSiteSecurityBriefing.Location = new System.Drawing.Point(11, 9);
            this.PnlSaAtWoSiteSecurityBriefing.Name = "PnlSaAtWoSiteSecurityBriefing";
            this.PnlSaAtWoSiteSecurityBriefing.Size = new System.Drawing.Size(1229, 385);
            this.PnlSaAtWoSiteSecurityBriefing.TabIndex = 19;
            // 
            // GpSaAtWoDummy
            // 
            this.GpSaAtWoDummy.Location = new System.Drawing.Point(9, 114);
            this.GpSaAtWoDummy.Name = "GpSaAtWoDummy";
            this.GpSaAtWoDummy.Size = new System.Drawing.Size(1210, 5);
            this.GpSaAtWoDummy.TabIndex = 98;
            this.GpSaAtWoDummy.TabStop = false;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(21, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(192, 24);
            this.label2.TabIndex = 97;
            this.label2.Text = "Anerkennung / Ausbildung für:";
            // 
            // CbxSaAtWoCranesBriefingDone
            // 
            this.CbxSaAtWoCranesBriefingDone.Enabled = false;
            this.CbxSaAtWoCranesBriefingDone.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Millimeter, ((byte)(0)));
            this.CbxSaAtWoCranesBriefingDone.ForeColor = System.Drawing.SystemColors.Control;
            this.CbxSaAtWoCranesBriefingDone.Location = new System.Drawing.Point(24, 320);
            this.CbxSaAtWoCranesBriefingDone.Name = "CbxSaAtWoCranesBriefingDone";
            this.CbxSaAtWoCranesBriefingDone.Size = new System.Drawing.Size(24, 24);
            this.CbxSaAtWoCranesBriefingDone.TabIndex = 7;
            this.CbxSaAtWoCranesBriefingDone.Text = "B";
            // 
            // CbxSaAtWoPalletLifterBriefingDone
            // 
            this.CbxSaAtWoPalletLifterBriefingDone.Enabled = false;
            this.CbxSaAtWoPalletLifterBriefingDone.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Millimeter, ((byte)(0)));
            this.CbxSaAtWoPalletLifterBriefingDone.ForeColor = System.Drawing.SystemColors.Control;
            this.CbxSaAtWoPalletLifterBriefingDone.Location = new System.Drawing.Point(24, 216);
            this.CbxSaAtWoPalletLifterBriefingDone.Name = "CbxSaAtWoPalletLifterBriefingDone";
            this.CbxSaAtWoPalletLifterBriefingDone.Size = new System.Drawing.Size(24, 24);
            this.CbxSaAtWoPalletLifterBriefingDone.TabIndex = 4;
            this.CbxSaAtWoPalletLifterBriefingDone.Text = "B";
            // 
            // CbxSaAtWoSafetyAtWorkBriefingDone
            // 
            this.CbxSaAtWoSafetyAtWorkBriefingDone.Enabled = false;
            this.CbxSaAtWoSafetyAtWorkBriefingDone.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Millimeter, ((byte)(0)));
            this.CbxSaAtWoSafetyAtWorkBriefingDone.ForeColor = System.Drawing.SystemColors.Control;
            this.CbxSaAtWoSafetyAtWorkBriefingDone.Location = new System.Drawing.Point(24, 59);
            this.CbxSaAtWoSafetyAtWorkBriefingDone.Name = "CbxSaAtWoSafetyAtWorkBriefingDone";
            this.CbxSaAtWoSafetyAtWorkBriefingDone.Size = new System.Drawing.Size(24, 24);
            this.CbxSaAtWoSafetyAtWorkBriefingDone.TabIndex = 1;
            this.CbxSaAtWoSafetyAtWorkBriefingDone.Text = "B";
            // 
            // DatSaAtWoCranesBriefingDoneOn
            // 
            this.DatSaAtWoCranesBriefingDoneOn.CustomFormat = "dd.MM.yyyy";
            this.DatSaAtWoCranesBriefingDoneOn.Enabled = false;
            this.DatSaAtWoCranesBriefingDoneOn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatSaAtWoCranesBriefingDoneOn.Location = new System.Drawing.Point(352, 324);
            this.DatSaAtWoCranesBriefingDoneOn.Name = "DatSaAtWoCranesBriefingDoneOn";
            this.DatSaAtWoCranesBriefingDoneOn.Size = new System.Drawing.Size(88, 21);
            this.DatSaAtWoCranesBriefingDoneOn.TabIndex = 8;
            this.DatSaAtWoCranesBriefingDoneOn.Value = new System.DateTime(2003, 10, 6, 0, 0, 0, 0);
            this.DatSaAtWoCranesBriefingDoneOn.Leave += new System.EventHandler(this.DatSaAtWoCranesBriefingDoneOn_Leave_1);
            // 
            // DatSaAtWoPalletLifterBriefingDoneOn
            // 
            this.DatSaAtWoPalletLifterBriefingDoneOn.CustomFormat = "dd.MM.yyyy";
            this.DatSaAtWoPalletLifterBriefingDoneOn.Enabled = false;
            this.DatSaAtWoPalletLifterBriefingDoneOn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatSaAtWoPalletLifterBriefingDoneOn.Location = new System.Drawing.Point(352, 220);
            this.DatSaAtWoPalletLifterBriefingDoneOn.Name = "DatSaAtWoPalletLifterBriefingDoneOn";
            this.DatSaAtWoPalletLifterBriefingDoneOn.Size = new System.Drawing.Size(88, 21);
            this.DatSaAtWoPalletLifterBriefingDoneOn.TabIndex = 5;
            this.DatSaAtWoPalletLifterBriefingDoneOn.Value = new System.DateTime(2003, 10, 6, 0, 0, 0, 0);
            this.DatSaAtWoPalletLifterBriefingDoneOn.Leave += new System.EventHandler(this.DatSaAtWoPalletLifterBriefingDoneOn_Leave_1);
            // 
            // DatSaAtWoSafetyAtWorkBriefingDone
            // 
            this.DatSaAtWoSafetyAtWorkBriefingDone.CustomFormat = "dd.MM.yyyy";
            this.DatSaAtWoSafetyAtWorkBriefingDone.Enabled = false;
            this.DatSaAtWoSafetyAtWorkBriefingDone.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatSaAtWoSafetyAtWorkBriefingDone.Location = new System.Drawing.Point(352, 63);
            this.DatSaAtWoSafetyAtWorkBriefingDone.Name = "DatSaAtWoSafetyAtWorkBriefingDone";
            this.DatSaAtWoSafetyAtWorkBriefingDone.Size = new System.Drawing.Size(88, 21);
            this.DatSaAtWoSafetyAtWorkBriefingDone.TabIndex = 2;
            this.DatSaAtWoSafetyAtWorkBriefingDone.Value = new System.DateTime(2003, 10, 6, 0, 0, 0, 0);
            this.DatSaAtWoSafetyAtWorkBriefingDone.Leave += new System.EventHandler(this.DatSaAtWoSafetyAtWorkBriefingDone_Leave_1);
            // 
            // LblSaAtWoCranesBriefing
            // 
            this.LblSaAtWoCranesBriefing.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSaAtWoCranesBriefing.Location = new System.Drawing.Point(56, 284);
            this.LblSaAtWoCranesBriefing.Name = "LblSaAtWoCranesBriefing";
            this.LblSaAtWoCranesBriefing.Size = new System.Drawing.Size(368, 24);
            this.LblSaAtWoCranesBriefing.TabIndex = 92;
            this.LblSaAtWoCranesBriefing.Text = "Krane / Anschläger angeordnet";
            // 
            // LblSaAtWoPalletLifterBriefing
            // 
            this.LblSaAtWoPalletLifterBriefing.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSaAtWoPalletLifterBriefing.Location = new System.Drawing.Point(56, 180);
            this.LblSaAtWoPalletLifterBriefing.Name = "LblSaAtWoPalletLifterBriefing";
            this.LblSaAtWoPalletLifterBriefing.Size = new System.Drawing.Size(312, 24);
            this.LblSaAtWoPalletLifterBriefing.TabIndex = 91;
            this.LblSaAtWoPalletLifterBriefing.Text = "Flurförderzeuge angeordnet";
            // 
            // LblSaAtWoCranesBriefingDoneBy
            // 
            this.LblSaAtWoCranesBriefingDoneBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSaAtWoCranesBriefingDoneBy.Location = new System.Drawing.Point(527, 324);
            this.LblSaAtWoCranesBriefingDoneBy.Name = "LblSaAtWoCranesBriefingDoneBy";
            this.LblSaAtWoCranesBriefingDoneBy.Size = new System.Drawing.Size(40, 23);
            this.LblSaAtWoCranesBriefingDoneBy.TabIndex = 19;
            this.LblSaAtWoCranesBriefingDoneBy.Text = "durch";
            // 
            // LblSaAtWoCranesBriefingDoneOn
            // 
            this.LblSaAtWoCranesBriefingDoneOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSaAtWoCranesBriefingDoneOn.Location = new System.Drawing.Point(320, 324);
            this.LblSaAtWoCranesBriefingDoneOn.Name = "LblSaAtWoCranesBriefingDoneOn";
            this.LblSaAtWoCranesBriefingDoneOn.Size = new System.Drawing.Size(32, 23);
            this.LblSaAtWoCranesBriefingDoneOn.TabIndex = 18;
            this.LblSaAtWoCranesBriefingDoneOn.Text = "am";
            // 
            // LblSaAtWoCranesBriefingDone
            // 
            this.LblSaAtWoCranesBriefingDone.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSaAtWoCranesBriefingDone.Location = new System.Drawing.Point(56, 324);
            this.LblSaAtWoCranesBriefingDone.Name = "LblSaAtWoCranesBriefingDone";
            this.LblSaAtWoCranesBriefingDone.Size = new System.Drawing.Size(240, 23);
            this.LblSaAtWoCranesBriefingDone.TabIndex = 17;
            this.LblSaAtWoCranesBriefingDone.Text = "Krane / Anschläger erfolgt";
            // 
            // TxtSaAtWoCranesBriefingDoneBy
            // 
            this.TxtSaAtWoCranesBriefingDoneBy.Location = new System.Drawing.Point(575, 324);
            this.TxtSaAtWoCranesBriefingDoneBy.Name = "TxtSaAtWoCranesBriefingDoneBy";
            this.TxtSaAtWoCranesBriefingDoneBy.ReadOnly = true;
            this.TxtSaAtWoCranesBriefingDoneBy.Size = new System.Drawing.Size(210, 21);
            this.TxtSaAtWoCranesBriefingDoneBy.TabIndex = 9;
            this.TxtSaAtWoCranesBriefingDoneBy.TabStop = false;
            // 
            // LblSaAtWoPalletLifterBriefingDoneBy
            // 
            this.LblSaAtWoPalletLifterBriefingDoneBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSaAtWoPalletLifterBriefingDoneBy.Location = new System.Drawing.Point(527, 220);
            this.LblSaAtWoPalletLifterBriefingDoneBy.Name = "LblSaAtWoPalletLifterBriefingDoneBy";
            this.LblSaAtWoPalletLifterBriefingDoneBy.Size = new System.Drawing.Size(40, 23);
            this.LblSaAtWoPalletLifterBriefingDoneBy.TabIndex = 14;
            this.LblSaAtWoPalletLifterBriefingDoneBy.Text = "durch";
            // 
            // LblSaAtWoPalletLifterBriefingDoneOn
            // 
            this.LblSaAtWoPalletLifterBriefingDoneOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSaAtWoPalletLifterBriefingDoneOn.Location = new System.Drawing.Point(320, 220);
            this.LblSaAtWoPalletLifterBriefingDoneOn.Name = "LblSaAtWoPalletLifterBriefingDoneOn";
            this.LblSaAtWoPalletLifterBriefingDoneOn.Size = new System.Drawing.Size(32, 23);
            this.LblSaAtWoPalletLifterBriefingDoneOn.TabIndex = 13;
            this.LblSaAtWoPalletLifterBriefingDoneOn.Text = "am";
            // 
            // LblSaAtWoPalletLifterBriefingDone
            // 
            this.LblSaAtWoPalletLifterBriefingDone.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSaAtWoPalletLifterBriefingDone.Location = new System.Drawing.Point(56, 220);
            this.LblSaAtWoPalletLifterBriefingDone.Name = "LblSaAtWoPalletLifterBriefingDone";
            this.LblSaAtWoPalletLifterBriefingDone.Size = new System.Drawing.Size(232, 23);
            this.LblSaAtWoPalletLifterBriefingDone.TabIndex = 12;
            this.LblSaAtWoPalletLifterBriefingDone.Text = "Flurförderzeuge erfolgt";
            // 
            // TxtSaAtWoPalletLifterBriefingDoneBy
            // 
            this.TxtSaAtWoPalletLifterBriefingDoneBy.Location = new System.Drawing.Point(575, 220);
            this.TxtSaAtWoPalletLifterBriefingDoneBy.Name = "TxtSaAtWoPalletLifterBriefingDoneBy";
            this.TxtSaAtWoPalletLifterBriefingDoneBy.ReadOnly = true;
            this.TxtSaAtWoPalletLifterBriefingDoneBy.Size = new System.Drawing.Size(210, 21);
            this.TxtSaAtWoPalletLifterBriefingDoneBy.TabIndex = 6;
            this.TxtSaAtWoPalletLifterBriefingDoneBy.TabStop = false;
            // 
            // LblSaAtWoSafetyAtWorkBriefing
            // 
            this.LblSaAtWoSafetyAtWorkBriefing.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSaAtWoSafetyAtWorkBriefing.Location = new System.Drawing.Point(56, 32);
            this.LblSaAtWoSafetyAtWorkBriefing.Name = "LblSaAtWoSafetyAtWorkBriefing";
            this.LblSaAtWoSafetyAtWorkBriefing.Size = new System.Drawing.Size(440, 24);
            this.LblSaAtWoSafetyAtWorkBriefing.TabIndex = 91;
            this.LblSaAtWoSafetyAtWorkBriefing.Text = "Belehrung durch Abteilung Arbeitssicherheit angeordnet";
            // 
            // LblSaAtWoSafetyAtWorkBriefingDoneBy
            // 
            this.LblSaAtWoSafetyAtWorkBriefingDoneBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSaAtWoSafetyAtWorkBriefingDoneBy.Location = new System.Drawing.Point(527, 63);
            this.LblSaAtWoSafetyAtWorkBriefingDoneBy.Name = "LblSaAtWoSafetyAtWorkBriefingDoneBy";
            this.LblSaAtWoSafetyAtWorkBriefingDoneBy.Size = new System.Drawing.Size(40, 23);
            this.LblSaAtWoSafetyAtWorkBriefingDoneBy.TabIndex = 14;
            this.LblSaAtWoSafetyAtWorkBriefingDoneBy.Text = "durch";
            // 
            // LblSaAtWoSafetyAtWorkBriefingDoneOn
            // 
            this.LblSaAtWoSafetyAtWorkBriefingDoneOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSaAtWoSafetyAtWorkBriefingDoneOn.Location = new System.Drawing.Point(320, 63);
            this.LblSaAtWoSafetyAtWorkBriefingDoneOn.Name = "LblSaAtWoSafetyAtWorkBriefingDoneOn";
            this.LblSaAtWoSafetyAtWorkBriefingDoneOn.Size = new System.Drawing.Size(32, 23);
            this.LblSaAtWoSafetyAtWorkBriefingDoneOn.TabIndex = 13;
            this.LblSaAtWoSafetyAtWorkBriefingDoneOn.Text = "am";
            // 
            // TxtSaAtWoSafetyAtWorkBriefingDoneBy
            // 
            this.TxtSaAtWoSafetyAtWorkBriefingDoneBy.Location = new System.Drawing.Point(575, 63);
            this.TxtSaAtWoSafetyAtWorkBriefingDoneBy.Name = "TxtSaAtWoSafetyAtWorkBriefingDoneBy";
            this.TxtSaAtWoSafetyAtWorkBriefingDoneBy.ReadOnly = true;
            this.TxtSaAtWoSafetyAtWorkBriefingDoneBy.Size = new System.Drawing.Size(210, 21);
            this.TxtSaAtWoSafetyAtWorkBriefingDoneBy.TabIndex = 3;
            this.TxtSaAtWoSafetyAtWorkBriefingDoneBy.TabStop = false;
            // 
            // LblSaAtWoSafetyAtWorkBriefingDone
            // 
            this.LblSaAtWoSafetyAtWorkBriefingDone.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSaAtWoSafetyAtWorkBriefingDone.Location = new System.Drawing.Point(56, 63);
            this.LblSaAtWoSafetyAtWorkBriefingDone.Name = "LblSaAtWoSafetyAtWorkBriefingDone";
            this.LblSaAtWoSafetyAtWorkBriefingDone.Size = new System.Drawing.Size(280, 23);
            this.LblSaAtWoSafetyAtWorkBriefingDone.TabIndex = 12;
            this.LblSaAtWoSafetyAtWorkBriefingDone.Text = "Belehrung durch Abteilung Arbeitssicherheit";
            // 
            // PnlSaAtWoPalletLifterBriefing
            // 
            this.PnlSaAtWoPalletLifterBriefing.Controls.Add(this.RbtSaAtWoPalletLifterBriefing);
            this.PnlSaAtWoPalletLifterBriefing.Location = new System.Drawing.Point(16, 164);
            this.PnlSaAtWoPalletLifterBriefing.Name = "PnlSaAtWoPalletLifterBriefing";
            this.PnlSaAtWoPalletLifterBriefing.Size = new System.Drawing.Size(48, 40);
            this.PnlSaAtWoPalletLifterBriefing.TabIndex = 94;
            // 
            // RbtSaAtWoPalletLifterBriefing
            // 
            this.RbtSaAtWoPalletLifterBriefing.Enabled = false;
            this.RbtSaAtWoPalletLifterBriefing.Location = new System.Drawing.Point(8, 12);
            this.RbtSaAtWoPalletLifterBriefing.Name = "RbtSaAtWoPalletLifterBriefing";
            this.RbtSaAtWoPalletLifterBriefing.Size = new System.Drawing.Size(24, 24);
            this.RbtSaAtWoPalletLifterBriefing.TabIndex = 93;
            // 
            // PnlSaAtWoCranesBriefing
            // 
            this.PnlSaAtWoCranesBriefing.Controls.Add(this.RbtSaAtWoCranesBriefing);
            this.PnlSaAtWoCranesBriefing.Location = new System.Drawing.Point(16, 268);
            this.PnlSaAtWoCranesBriefing.Name = "PnlSaAtWoCranesBriefing";
            this.PnlSaAtWoCranesBriefing.Size = new System.Drawing.Size(48, 40);
            this.PnlSaAtWoCranesBriefing.TabIndex = 95;
            // 
            // RbtSaAtWoCranesBriefing
            // 
            this.RbtSaAtWoCranesBriefing.Enabled = false;
            this.RbtSaAtWoCranesBriefing.Location = new System.Drawing.Point(8, 12);
            this.RbtSaAtWoCranesBriefing.Name = "RbtSaAtWoCranesBriefing";
            this.RbtSaAtWoCranesBriefing.Size = new System.Drawing.Size(16, 24);
            this.RbtSaAtWoCranesBriefing.TabIndex = 77;
            // 
            // PnlSaAtWoIndustrialSafetyBriefing
            // 
            this.PnlSaAtWoIndustrialSafetyBriefing.Controls.Add(this.RbtSaAtWoBriefing);
            this.PnlSaAtWoIndustrialSafetyBriefing.Location = new System.Drawing.Point(16, 16);
            this.PnlSaAtWoIndustrialSafetyBriefing.Name = "PnlSaAtWoIndustrialSafetyBriefing";
            this.PnlSaAtWoIndustrialSafetyBriefing.Size = new System.Drawing.Size(48, 40);
            this.PnlSaAtWoIndustrialSafetyBriefing.TabIndex = 96;
            // 
            // RbtSaAtWoBriefing
            // 
            this.RbtSaAtWoBriefing.Enabled = false;
            this.RbtSaAtWoBriefing.Location = new System.Drawing.Point(8, 12);
            this.RbtSaAtWoBriefing.Name = "RbtSaAtWoBriefing";
            this.RbtSaAtWoBriefing.Size = new System.Drawing.Size(40, 24);
            this.RbtSaAtWoBriefing.TabIndex = 77;
            // 
            // TapPlant
            // 
            this.TapPlant.Controls.Add(this.PnlTabPlant);
            this.TapPlant.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TapPlant.Location = new System.Drawing.Point(4, 25);
            this.TapPlant.Name = "TapPlant";
            this.TapPlant.Size = new System.Drawing.Size(1249, 660);
            this.TapPlant.TabIndex = 4;
            this.TapPlant.Text = "Betrieb";
            // 
            // PnlTabPlant
            // 
            this.PnlTabPlant.Controls.Add(this.PnlPlIndustrialSafetyBriefingPlant);
            this.PnlTabPlant.Enabled = false;
            this.PnlTabPlant.Location = new System.Drawing.Point(0, 0);
            this.PnlTabPlant.Name = "PnlTabPlant";
            this.PnlTabPlant.Size = new System.Drawing.Size(1246, 657);
            this.PnlTabPlant.TabIndex = 0;
            // 
            // PnlPlIndustrialSafetyBriefingPlant
            // 
            this.PnlPlIndustrialSafetyBriefingPlant.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlPlIndustrialSafetyBriefingPlant.Controls.Add(this.ChkPlZKSImport);
            this.PnlPlIndustrialSafetyBriefingPlant.Controls.Add(this.LblPlZKSImport);
            this.PnlPlIndustrialSafetyBriefingPlant.Controls.Add(this.LblPlBriefingValidUntil);
            this.PnlPlIndustrialSafetyBriefingPlant.Controls.Add(this.DatPlBriefingValidUntil);
            this.PnlPlIndustrialSafetyBriefingPlant.Controls.Add(this.TxtPlPlantname);
            this.PnlPlIndustrialSafetyBriefingPlant.Controls.Add(this.CbxPlBriefingDone);
            this.PnlPlIndustrialSafetyBriefingPlant.Controls.Add(this.DatPlBriefingDoneOn);
            this.PnlPlIndustrialSafetyBriefingPlant.Controls.Add(this.DgrPlPlant);
            this.PnlPlIndustrialSafetyBriefingPlant.Controls.Add(this.LblPlBriefing);
            this.PnlPlIndustrialSafetyBriefingPlant.Controls.Add(this.LblPlBriefingDoneBy);
            this.PnlPlIndustrialSafetyBriefingPlant.Controls.Add(this.LblPlBriefingDoneOn);
            this.PnlPlIndustrialSafetyBriefingPlant.Controls.Add(this.LblPlBriefingDone);
            this.PnlPlIndustrialSafetyBriefingPlant.Controls.Add(this.TxtPlBriefingDoneBy);
            this.PnlPlIndustrialSafetyBriefingPlant.Controls.Add(this.RbtPlBriefing);
            this.PnlPlIndustrialSafetyBriefingPlant.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PnlPlIndustrialSafetyBriefingPlant.Location = new System.Drawing.Point(11, 9);
            this.PnlPlIndustrialSafetyBriefingPlant.Name = "PnlPlIndustrialSafetyBriefingPlant";
            this.PnlPlIndustrialSafetyBriefingPlant.Size = new System.Drawing.Size(1225, 618);
            this.PnlPlIndustrialSafetyBriefingPlant.TabIndex = 7;
            // 
            // ChkPlZKSImport
            // 
            this.ChkPlZKSImport.Enabled = false;
            this.ChkPlZKSImport.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Millimeter, ((byte)(0)));
            this.ChkPlZKSImport.ForeColor = System.Drawing.SystemColors.Control;
            this.ChkPlZKSImport.Location = new System.Drawing.Point(802, 86);
            this.ChkPlZKSImport.Name = "ChkPlZKSImport";
            this.ChkPlZKSImport.Size = new System.Drawing.Size(24, 24);
            this.ChkPlZKSImport.TabIndex = 111;
            this.ChkPlZKSImport.Text = "B";
            // 
            // LblPlZKSImport
            // 
            this.LblPlZKSImport.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblPlZKSImport.Location = new System.Drawing.Point(676, 90);
            this.LblPlZKSImport.Name = "LblPlZKSImport";
            this.LblPlZKSImport.Size = new System.Drawing.Size(124, 19);
            this.LblPlZKSImport.TabIndex = 110;
            this.LblPlZKSImport.Text = "aus ZKS importiert";
            // 
            // LblPlBriefingValidUntil
            // 
            this.LblPlBriefingValidUntil.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblPlBriefingValidUntil.Location = new System.Drawing.Point(676, 57);
            this.LblPlBriefingValidUntil.Name = "LblPlBriefingValidUntil";
            this.LblPlBriefingValidUntil.Size = new System.Drawing.Size(62, 16);
            this.LblPlBriefingValidUntil.TabIndex = 109;
            this.LblPlBriefingValidUntil.Text = "gültig bis";
            // 
            // DatPlBriefingValidUntil
            // 
            this.DatPlBriefingValidUntil.CustomFormat = "dd.MM.yyyy";
            this.DatPlBriefingValidUntil.Enabled = false;
            this.DatPlBriefingValidUntil.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatPlBriefingValidUntil.Location = new System.Drawing.Point(744, 55);
            this.DatPlBriefingValidUntil.Name = "DatPlBriefingValidUntil";
            this.DatPlBriefingValidUntil.Size = new System.Drawing.Size(88, 21);
            this.DatPlBriefingValidUntil.TabIndex = 6;
            this.DatPlBriefingValidUntil.Leave += new System.EventHandler(this.DatPlBriefingValidUntil_Leave);
            // 
            // TxtPlPlantname
            // 
            this.TxtPlPlantname.Location = new System.Drawing.Point(218, 55);
            this.TxtPlPlantname.Name = "TxtPlPlantname";
            this.TxtPlPlantname.ReadOnly = true;
            this.TxtPlPlantname.Size = new System.Drawing.Size(438, 21);
            this.TxtPlPlantname.TabIndex = 3;
            this.TxtPlPlantname.TabStop = false;
            // 
            // CbxPlBriefingDone
            // 
            this.CbxPlBriefingDone.Enabled = false;
            this.CbxPlBriefingDone.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Millimeter, ((byte)(0)));
            this.CbxPlBriefingDone.ForeColor = System.Drawing.SystemColors.Control;
            this.CbxPlBriefingDone.Location = new System.Drawing.Point(25, 53);
            this.CbxPlBriefingDone.Name = "CbxPlBriefingDone";
            this.CbxPlBriefingDone.Size = new System.Drawing.Size(24, 24);
            this.CbxPlBriefingDone.TabIndex = 2;
            this.CbxPlBriefingDone.Text = "B";
            // 
            // DatPlBriefingDoneOn
            // 
            this.DatPlBriefingDoneOn.CustomFormat = "dd.MM.yyyy";
            this.DatPlBriefingDoneOn.Enabled = false;
            this.DatPlBriefingDoneOn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatPlBriefingDoneOn.Location = new System.Drawing.Point(218, 88);
            this.DatPlBriefingDoneOn.Name = "DatPlBriefingDoneOn";
            this.DatPlBriefingDoneOn.Size = new System.Drawing.Size(88, 21);
            this.DatPlBriefingDoneOn.TabIndex = 4;
            this.DatPlBriefingDoneOn.Value = new System.DateTime(2003, 10, 6, 0, 0, 0, 0);
            this.DatPlBriefingDoneOn.Leave += new System.EventHandler(this.DatPlBriefingDoneOn_Leave);
            // 
            // DgrPlPlant
            // 
            this.DgrPlPlant.AllowSorting = false;
            this.DgrPlPlant.CaptionBackColor = System.Drawing.Color.SteelBlue;
            this.DgrPlPlant.CaptionForeColor = System.Drawing.Color.White;
            this.DgrPlPlant.CaptionText = "Zugeordnete Betriebe";
            this.DgrPlPlant.DataMember = "";
            this.DgrPlPlant.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.DgrPlPlant.Location = new System.Drawing.Point(22, 132);
            this.DgrPlPlant.Name = "DgrPlPlant";
            this.DgrPlPlant.ReadOnly = true;
            this.DgrPlPlant.Size = new System.Drawing.Size(1178, 306);
            this.DgrPlPlant.TabIndex = 7;
            this.DgrPlPlant.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.DGrTableStylePlant});
            this.DgrPlPlant.CurrentCellChanged += new System.EventHandler(this.DgrPlPlant_CurrentCellChanged);
            this.DgrPlPlant.Enter += new System.EventHandler(this.DgrPlPlant_Enter);
            // 
            // DGrTableStylePlant
            // 
            this.DGrTableStylePlant.DataGrid = this.DgrPlPlant;
            this.DGrTableStylePlant.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.DgrTxtPKPlant,
            this.DgrTxtPlantName,
            this.DgrTxtPlDoneOn,
            this.DgrTxtUserName,
            this.DgrTxtSource,
            this.DgrTxtPlValidUntil,
            this.DgrTxtStatus,
            this.DgrTxtDirected});
            this.DGrTableStylePlant.HeaderFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DGrTableStylePlant.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.DGrTableStylePlant.MappingName = "Plants";
            this.DGrTableStylePlant.ReadOnly = true;
            // 
            // DgrTxtPKPlant
            // 
            this.DgrTxtPKPlant.Format = "";
            this.DgrTxtPKPlant.FormatInfo = null;
            this.DgrTxtPKPlant.MappingName = "PlantID";
            this.DgrTxtPKPlant.Width = 0;
            // 
            // DgrTxtPlantName
            // 
            this.DgrTxtPlantName.Format = "";
            this.DgrTxtPlantName.FormatInfo = null;
            this.DgrTxtPlantName.HeaderText = "Name";
            this.DgrTxtPlantName.MappingName = "PlantName";
            this.DgrTxtPlantName.Width = 410;
            // 
            // DgrTxtPlDoneOn
            // 
            this.DgrTxtPlDoneOn.Format = "";
            this.DgrTxtPlDoneOn.FormatInfo = null;
            this.DgrTxtPlDoneOn.HeaderText = "Erteilt am";
            this.DgrTxtPlDoneOn.MappingName = "PlantDateAsString";
            this.DgrTxtPlDoneOn.Width = 95;
            // 
            // DgrTxtUserName
            // 
            this.DgrTxtUserName.Format = "";
            this.DgrTxtUserName.FormatInfo = null;
            this.DgrTxtUserName.HeaderText = "Erteilt durch";
            this.DgrTxtUserName.MappingName = "UserName";
            this.DgrTxtUserName.Width = 185;
            // 
            // DgrTxtSource
            // 
            this.DgrTxtSource.Format = "";
            this.DgrTxtSource.FormatInfo = null;
            this.DgrTxtSource.HeaderText = "Quelle";
            this.DgrTxtSource.MappingName = "PlantSource";
            this.DgrTxtSource.Width = 110;
            // 
            // DgrTxtPlValidUntil
            // 
            this.DgrTxtPlValidUntil.Format = "";
            this.DgrTxtPlValidUntil.FormatInfo = null;
            this.DgrTxtPlValidUntil.HeaderText = "Gültig bis";
            this.DgrTxtPlValidUntil.MappingName = "ValidUntilAsString";
            this.DgrTxtPlValidUntil.Width = 95;
            // 
            // DgrTxtStatus
            // 
            this.DgrTxtStatus.Format = "";
            this.DgrTxtStatus.FormatInfo = null;
            this.DgrTxtStatus.HeaderText = "Status";
            this.DgrTxtStatus.MappingName = "ReceivedAsString";
            this.DgrTxtStatus.Width = 110;
            // 
            // DgrTxtDirected
            // 
            this.DgrTxtDirected.Format = "";
            this.DgrTxtDirected.FormatInfo = null;
            this.DgrTxtDirected.HeaderText = "Anordnung";
            this.DgrTxtDirected.MappingName = "DirectedAsString";
            this.DgrTxtDirected.NullText = "nix";
            this.DgrTxtDirected.Width = 110;
            // 
            // LblPlBriefing
            // 
            this.LblPlBriefing.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblPlBriefing.Location = new System.Drawing.Point(46, 20);
            this.LblPlBriefing.Name = "LblPlBriefing";
            this.LblPlBriefing.Size = new System.Drawing.Size(376, 16);
            this.LblPlBriefing.TabIndex = 91;
            this.LblPlBriefing.Text = " Arbeitssicherheitsbelehrung Betrieb angeordnet";
            // 
            // LblPlBriefingDoneBy
            // 
            this.LblPlBriefingDoneBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblPlBriefingDoneBy.Location = new System.Drawing.Point(333, 90);
            this.LblPlBriefingDoneBy.Name = "LblPlBriefingDoneBy";
            this.LblPlBriefingDoneBy.Size = new System.Drawing.Size(40, 16);
            this.LblPlBriefingDoneBy.TabIndex = 14;
            this.LblPlBriefingDoneBy.Text = "durch";
            // 
            // LblPlBriefingDoneOn
            // 
            this.LblPlBriefingDoneOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblPlBriefingDoneOn.Location = new System.Drawing.Point(113, 90);
            this.LblPlBriefingDoneOn.Name = "LblPlBriefingDoneOn";
            this.LblPlBriefingDoneOn.Size = new System.Drawing.Size(75, 16);
            this.LblPlBriefingDoneOn.TabIndex = 13;
            this.LblPlBriefingDoneOn.Text = "erteilt am";
            this.LblPlBriefingDoneOn.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // LblPlBriefingDone
            // 
            this.LblPlBriefingDone.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblPlBriefingDone.Location = new System.Drawing.Point(44, 57);
            this.LblPlBriefingDone.Name = "LblPlBriefingDone";
            this.LblPlBriefingDone.Size = new System.Drawing.Size(144, 16);
            this.LblPlBriefingDone.TabIndex = 12;
            this.LblPlBriefingDone.Text = "Belehrung durch Betrieb";
            this.LblPlBriefingDone.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // TxtPlBriefingDoneBy
            // 
            this.TxtPlBriefingDoneBy.Location = new System.Drawing.Point(378, 88);
            this.TxtPlBriefingDoneBy.Name = "TxtPlBriefingDoneBy";
            this.TxtPlBriefingDoneBy.ReadOnly = true;
            this.TxtPlBriefingDoneBy.Size = new System.Drawing.Size(278, 21);
            this.TxtPlBriefingDoneBy.TabIndex = 5;
            this.TxtPlBriefingDoneBy.TabStop = false;
            // 
            // RbtPlBriefing
            // 
            this.RbtPlBriefing.Enabled = false;
            this.RbtPlBriefing.Location = new System.Drawing.Point(25, 16);
            this.RbtPlBriefing.Name = "RbtPlBriefing";
            this.RbtPlBriefing.Size = new System.Drawing.Size(40, 24);
            this.RbtPlBriefing.TabIndex = 1;
            // 
            // TapTechnicalDepartment
            // 
            this.TapTechnicalDepartment.Controls.Add(this.PnlTabTechnical);
            this.TapTechnicalDepartment.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TapTechnicalDepartment.Location = new System.Drawing.Point(4, 25);
            this.TapTechnicalDepartment.Name = "TapTechnicalDepartment";
            this.TapTechnicalDepartment.Size = new System.Drawing.Size(1249, 660);
            this.TapTechnicalDepartment.TabIndex = 9;
            this.TapTechnicalDepartment.Text = "Technische Abteilung";
            // 
            // PnlTabTechnical
            // 
            this.PnlTabTechnical.Controls.Add(this.PnlTecBriefingSite);
            this.PnlTabTechnical.Enabled = false;
            this.PnlTabTechnical.Location = new System.Drawing.Point(0, 0);
            this.PnlTabTechnical.Name = "PnlTabTechnical";
            this.PnlTabTechnical.Size = new System.Drawing.Size(1246, 427);
            this.PnlTabTechnical.TabIndex = 0;
            // 
            // PnlTecBriefingSite
            // 
            this.PnlTecBriefingSite.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlTecBriefingSite.Controls.Add(this.CbxTecRaisonalPlattform);
            this.PnlTecBriefingSite.Controls.Add(this.DatTecBriefingDoneOn);
            this.PnlTecBriefingSite.Controls.Add(this.LblTecBriefing);
            this.PnlTecBriefingSite.Controls.Add(this.LblTecBriefingDoneBy);
            this.PnlTecBriefingSite.Controls.Add(this.LblTecBriefingDoneOn);
            this.PnlTecBriefingSite.Controls.Add(this.LblTecBriefingDone);
            this.PnlTecBriefingSite.Controls.Add(this.TxtTecBriefingDoneBy);
            this.PnlTecBriefingSite.Controls.Add(this.RbtTecBriefing);
            this.PnlTecBriefingSite.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PnlTecBriefingSite.Location = new System.Drawing.Point(14, 12);
            this.PnlTecBriefingSite.Name = "PnlTecBriefingSite";
            this.PnlTecBriefingSite.Size = new System.Drawing.Size(1219, 189);
            this.PnlTecBriefingSite.TabIndex = 0;
            // 
            // CbxTecRaisonalPlattform
            // 
            this.CbxTecRaisonalPlattform.Enabled = false;
            this.CbxTecRaisonalPlattform.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Millimeter, ((byte)(0)));
            this.CbxTecRaisonalPlattform.ForeColor = System.Drawing.SystemColors.Control;
            this.CbxTecRaisonalPlattform.Location = new System.Drawing.Point(19, 52);
            this.CbxTecRaisonalPlattform.Name = "CbxTecRaisonalPlattform";
            this.CbxTecRaisonalPlattform.Size = new System.Drawing.Size(24, 24);
            this.CbxTecRaisonalPlattform.TabIndex = 2;
            this.CbxTecRaisonalPlattform.Text = "B";
            // 
            // DatTecBriefingDoneOn
            // 
            this.DatTecBriefingDoneOn.CustomFormat = "dd.MM.yyyy";
            this.DatTecBriefingDoneOn.Enabled = false;
            this.DatTecBriefingDoneOn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatTecBriefingDoneOn.Location = new System.Drawing.Point(315, 54);
            this.DatTecBriefingDoneOn.Name = "DatTecBriefingDoneOn";
            this.DatTecBriefingDoneOn.Size = new System.Drawing.Size(88, 21);
            this.DatTecBriefingDoneOn.TabIndex = 3;
            this.DatTecBriefingDoneOn.Value = new System.DateTime(2003, 10, 6, 0, 0, 0, 0);
            this.DatTecBriefingDoneOn.Leave += new System.EventHandler(this.DatTecBriefingDoneOn_Leave_1);
            // 
            // LblTecBriefing
            // 
            this.LblTecBriefing.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTecBriefing.Location = new System.Drawing.Point(52, 23);
            this.LblTecBriefing.Name = "LblTecBriefing";
            this.LblTecBriefing.Size = new System.Drawing.Size(248, 24);
            this.LblTecBriefing.TabIndex = 91;
            this.LblTecBriefing.Text = "Belehrung Hubarbeitsbühne angeordnet";
            // 
            // LblTecBriefingDoneBy
            // 
            this.LblTecBriefingDoneBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTecBriefingDoneBy.Location = new System.Drawing.Point(461, 56);
            this.LblTecBriefingDoneBy.Name = "LblTecBriefingDoneBy";
            this.LblTecBriefingDoneBy.Size = new System.Drawing.Size(40, 23);
            this.LblTecBriefingDoneBy.TabIndex = 14;
            this.LblTecBriefingDoneBy.Text = "durch";
            // 
            // LblTecBriefingDoneOn
            // 
            this.LblTecBriefingDoneOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTecBriefingDoneOn.Location = new System.Drawing.Point(283, 56);
            this.LblTecBriefingDoneOn.Name = "LblTecBriefingDoneOn";
            this.LblTecBriefingDoneOn.Size = new System.Drawing.Size(32, 23);
            this.LblTecBriefingDoneOn.TabIndex = 13;
            this.LblTecBriefingDoneOn.Text = "am";
            // 
            // LblTecBriefingDone
            // 
            this.LblTecBriefingDone.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTecBriefingDone.Location = new System.Drawing.Point(51, 56);
            this.LblTecBriefingDone.Name = "LblTecBriefingDone";
            this.LblTecBriefingDone.Size = new System.Drawing.Size(192, 23);
            this.LblTecBriefingDone.TabIndex = 12;
            this.LblTecBriefingDone.Text = "Belehrung Hubarbeitsbühne erteilt";
            // 
            // TxtTecBriefingDoneBy
            // 
            this.TxtTecBriefingDoneBy.Location = new System.Drawing.Point(509, 54);
            this.TxtTecBriefingDoneBy.Name = "TxtTecBriefingDoneBy";
            this.TxtTecBriefingDoneBy.ReadOnly = true;
            this.TxtTecBriefingDoneBy.Size = new System.Drawing.Size(240, 21);
            this.TxtTecBriefingDoneBy.TabIndex = 4;
            this.TxtTecBriefingDoneBy.TabStop = false;
            // 
            // RbtTecBriefing
            // 
            this.RbtTecBriefing.Enabled = false;
            this.RbtTecBriefing.Location = new System.Drawing.Point(20, 20);
            this.RbtTecBriefing.Name = "RbtTecBriefing";
            this.RbtTecBriefing.Size = new System.Drawing.Size(40, 24);
            this.RbtTecBriefing.TabIndex = 1;
            // 
            // TapSiteSecurity
            // 
            this.TapSiteSecurity.Controls.Add(this.PnlTabSiteSecure);
            this.TapSiteSecurity.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TapSiteSecurity.Location = new System.Drawing.Point(4, 25);
            this.TapSiteSecurity.Name = "TapSiteSecurity";
            this.TapSiteSecurity.Size = new System.Drawing.Size(1249, 660);
            this.TapSiteSecurity.TabIndex = 7;
            this.TapSiteSecurity.Text = "Werkschutz";
            // 
            // PnlTabSiteSecure
            // 
            this.PnlTabSiteSecure.Controls.Add(this.GrpSiSe1);
            this.PnlTabSiteSecure.Controls.Add(this.PnlSiSePExternal);
            this.PnlTabSiteSecure.Controls.Add(this.PnlSiSeVehicleNumber);
            this.PnlTabSiteSecure.Controls.Add(this.PnlSiSeIdentityCard);
            this.PnlTabSiteSecure.Controls.Add(this.PnlSiSeVehicleEntranceLong);
            this.PnlTabSiteSecure.Controls.Add(this.PnlSiSeVehicleEntranceShort);
            this.PnlTabSiteSecure.Controls.Add(this.PnlSiSeAccess);
            this.PnlTabSiteSecure.Controls.Add(this.PnlSiSeSiteSecurity);
            this.PnlTabSiteSecure.Enabled = false;
            this.PnlTabSiteSecure.Location = new System.Drawing.Point(0, 0);
            this.PnlTabSiteSecure.Name = "PnlTabSiteSecure";
            this.PnlTabSiteSecure.Size = new System.Drawing.Size(1246, 657);
            this.PnlTabSiteSecure.TabIndex = 0;
            // 
            // GrpSiSe1
            // 
            this.GrpSiSe1.Location = new System.Drawing.Point(25, 211);
            this.GrpSiSe1.Name = "GrpSiSe1";
            this.GrpSiSe1.Size = new System.Drawing.Size(1190, 5);
            this.GrpSiSe1.TabIndex = 87;
            this.GrpSiSe1.TabStop = false;
            // 
            // PnlSiSePExternal
            // 
            this.PnlSiSePExternal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlSiSePExternal.Controls.Add(this.LblSiSeParkTitle);
            this.PnlSiSePExternal.Controls.Add(this.label4);
            this.PnlSiSePExternal.Controls.Add(this.DatSiSePExternalOn);
            this.PnlSiSePExternal.Controls.Add(this.LblSiSePExternalBy);
            this.PnlSiSePExternal.Controls.Add(this.TxtSiSePExternalBy);
            this.PnlSiSePExternal.Controls.Add(this.LblSiSePExternalOn);
            this.PnlSiSePExternal.Controls.Add(this.CbxSiSePExternal);
            this.PnlSiSePExternal.Location = new System.Drawing.Point(627, 270);
            this.PnlSiSePExternal.Name = "PnlSiSePExternal";
            this.PnlSiSePExternal.Size = new System.Drawing.Size(600, 169);
            this.PnlSiSePExternal.TabIndex = 29;
            // 
            // LblSiSeParkTitle
            // 
            this.LblSiSeParkTitle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiSeParkTitle.Location = new System.Drawing.Point(11, 9);
            this.LblSiSeParkTitle.Name = "LblSiSeParkTitle";
            this.LblSiSeParkTitle.Size = new System.Drawing.Size(144, 16);
            this.LblSiSeParkTitle.TabIndex = 105;
            this.LblSiSeParkTitle.Text = "Parkplatz extern";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(49, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(152, 16);
            this.label4.TabIndex = 70;
            this.label4.Text = "Parkplatz extern gewährt";
            // 
            // DatSiSePExternalOn
            // 
            this.DatSiSePExternalOn.CustomFormat = "dd.MM.yyyy";
            this.DatSiSePExternalOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DatSiSePExternalOn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatSiSePExternalOn.Location = new System.Drawing.Point(62, 70);
            this.DatSiSePExternalOn.Name = "DatSiSePExternalOn";
            this.DatSiSePExternalOn.Size = new System.Drawing.Size(88, 21);
            this.DatSiSePExternalOn.TabIndex = 13;
            this.DatSiSePExternalOn.Value = new System.DateTime(2003, 10, 6, 0, 0, 0, 0);
            // 
            // LblSiSePExternalBy
            // 
            this.LblSiSePExternalBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiSePExternalBy.Location = new System.Drawing.Point(179, 72);
            this.LblSiSePExternalBy.Name = "LblSiSePExternalBy";
            this.LblSiSePExternalBy.Size = new System.Drawing.Size(40, 16);
            this.LblSiSePExternalBy.TabIndex = 104;
            this.LblSiSePExternalBy.Text = "durch";
            // 
            // TxtSiSePExternalBy
            // 
            this.TxtSiSePExternalBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSiSePExternalBy.Location = new System.Drawing.Point(221, 70);
            this.TxtSiSePExternalBy.Name = "TxtSiSePExternalBy";
            this.TxtSiSePExternalBy.ReadOnly = true;
            this.TxtSiSePExternalBy.Size = new System.Drawing.Size(230, 21);
            this.TxtSiSePExternalBy.TabIndex = 102;
            this.TxtSiSePExternalBy.TabStop = false;
            // 
            // LblSiSePExternalOn
            // 
            this.LblSiSePExternalOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiSePExternalOn.Location = new System.Drawing.Point(23, 73);
            this.LblSiSePExternalOn.Name = "LblSiSePExternalOn";
            this.LblSiSePExternalOn.Size = new System.Drawing.Size(34, 16);
            this.LblSiSePExternalOn.TabIndex = 103;
            this.LblSiSePExternalOn.Text = "am";
            // 
            // CbxSiSePExternal
            // 
            this.CbxSiSePExternal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Millimeter, ((byte)(0)));
            this.CbxSiSePExternal.ForeColor = System.Drawing.SystemColors.Control;
            this.CbxSiSePExternal.Location = new System.Drawing.Point(26, 36);
            this.CbxSiSePExternal.Name = "CbxSiSePExternal";
            this.CbxSiSePExternal.Size = new System.Drawing.Size(24, 24);
            this.CbxSiSePExternal.TabIndex = 12;
            this.CbxSiSePExternal.Text = "B";
            // 
            // PnlSiSeVehicleNumber
            // 
            this.PnlSiSeVehicleNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlSiSeVehicleNumber.Controls.Add(this.TxtSiSeVehicleRegistrationNumberFour);
            this.PnlSiSeVehicleNumber.Controls.Add(this.TxtSiSeVehicleRegistrationNumberThree);
            this.PnlSiSeVehicleNumber.Controls.Add(this.TxtSiSeVehicleRegistrationNumberTwo);
            this.PnlSiSeVehicleNumber.Controls.Add(this.LblSiSeVehicleRegistrationNumber);
            this.PnlSiSeVehicleNumber.Controls.Add(this.TxtSiSeVehicleRegistrationNumber);
            this.PnlSiSeVehicleNumber.Location = new System.Drawing.Point(16, 575);
            this.PnlSiSeVehicleNumber.Name = "PnlSiSeVehicleNumber";
            this.PnlSiSeVehicleNumber.Size = new System.Drawing.Size(1211, 46);
            this.PnlSiSeVehicleNumber.TabIndex = 28;
            // 
            // TxtSiSeVehicleRegistrationNumberFour
            // 
            this.TxtSiSeVehicleRegistrationNumberFour.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSiSeVehicleRegistrationNumberFour.Location = new System.Drawing.Point(895, 10);
            this.TxtSiSeVehicleRegistrationNumberFour.MaxLength = 15;
            this.TxtSiSeVehicleRegistrationNumberFour.Name = "TxtSiSeVehicleRegistrationNumberFour";
            this.TxtSiSeVehicleRegistrationNumberFour.Size = new System.Drawing.Size(200, 21);
            this.TxtSiSeVehicleRegistrationNumberFour.TabIndex = 32;
            // 
            // TxtSiSeVehicleRegistrationNumberThree
            // 
            this.TxtSiSeVehicleRegistrationNumberThree.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSiSeVehicleRegistrationNumberThree.Location = new System.Drawing.Point(663, 11);
            this.TxtSiSeVehicleRegistrationNumberThree.MaxLength = 15;
            this.TxtSiSeVehicleRegistrationNumberThree.Name = "TxtSiSeVehicleRegistrationNumberThree";
            this.TxtSiSeVehicleRegistrationNumberThree.Size = new System.Drawing.Size(200, 21);
            this.TxtSiSeVehicleRegistrationNumberThree.TabIndex = 31;
            // 
            // TxtSiSeVehicleRegistrationNumberTwo
            // 
            this.TxtSiSeVehicleRegistrationNumberTwo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSiSeVehicleRegistrationNumberTwo.Location = new System.Drawing.Point(430, 11);
            this.TxtSiSeVehicleRegistrationNumberTwo.MaxLength = 15;
            this.TxtSiSeVehicleRegistrationNumberTwo.Name = "TxtSiSeVehicleRegistrationNumberTwo";
            this.TxtSiSeVehicleRegistrationNumberTwo.Size = new System.Drawing.Size(200, 21);
            this.TxtSiSeVehicleRegistrationNumberTwo.TabIndex = 30;
            // 
            // LblSiSeVehicleRegistrationNumber
            // 
            this.LblSiSeVehicleRegistrationNumber.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiSeVehicleRegistrationNumber.Location = new System.Drawing.Point(26, 14);
            this.LblSiSeVehicleRegistrationNumber.Name = "LblSiSeVehicleRegistrationNumber";
            this.LblSiSeVehicleRegistrationNumber.Size = new System.Drawing.Size(112, 24);
            this.LblSiSeVehicleRegistrationNumber.TabIndex = 42;
            this.LblSiSeVehicleRegistrationNumber.Text = "KFZ-Kennzeichen";
            // 
            // TxtSiSeVehicleRegistrationNumber
            // 
            this.TxtSiSeVehicleRegistrationNumber.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSiSeVehicleRegistrationNumber.Location = new System.Drawing.Point(198, 11);
            this.TxtSiSeVehicleRegistrationNumber.MaxLength = 15;
            this.TxtSiSeVehicleRegistrationNumber.Name = "TxtSiSeVehicleRegistrationNumber";
            this.TxtSiSeVehicleRegistrationNumber.Size = new System.Drawing.Size(200, 21);
            this.TxtSiSeVehicleRegistrationNumber.TabIndex = 29;
            // 
            // PnlSiSeIdentityCard
            // 
            this.PnlSiSeIdentityCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlSiSeIdentityCard.Controls.Add(this.LblSiSeIdPhotoHitagRecHint);
            this.PnlSiSeIdentityCard.Controls.Add(this.CbxSiSeIdPhotoHitagRec);
            this.PnlSiSeIdentityCard.Controls.Add(this.DatSiSeIdPhotoHitagRec);
            this.PnlSiSeIdentityCard.Controls.Add(this.LblSiSeIdPhotoHitagRecBy);
            this.PnlSiSeIdentityCard.Controls.Add(this.LblSiSeIdPhotoHitagRec);
            this.PnlSiSeIdentityCard.Controls.Add(this.TxtSiSeIdPhotoHitagRecBy);
            this.PnlSiSeIdentityCard.Controls.Add(this.LblSiSeIdPhotoSmActRecOn);
            this.PnlSiSeIdentityCard.Controls.Add(this.LblSiSeIdPhotoTitle);
            this.PnlSiSeIdentityCard.Controls.Add(this.CbxSiSeIdPhotoSmActRec);
            this.PnlSiSeIdentityCard.Controls.Add(this.DatSiSeIdPhotoSmActRec);
            this.PnlSiSeIdentityCard.Controls.Add(this.LblSiSeIdPhotoSmAct);
            this.PnlSiSeIdentityCard.Controls.Add(this.LblSiSeIdPhotoSmActRecBy);
            this.PnlSiSeIdentityCard.Controls.Add(this.LblSiSeIdPhotoSmActRec);
            this.PnlSiSeIdentityCard.Controls.Add(this.TxtSiSeIdPhotoSmActRecBy);
            this.PnlSiSeIdentityCard.Controls.Add(this.LblSiSeIdentityCardRecievedOn);
            this.PnlSiSeIdentityCard.Controls.Add(this.RbtSiSeIdPhotoSmAct);
            this.PnlSiSeIdentityCard.Location = new System.Drawing.Point(16, 117);
            this.PnlSiSeIdentityCard.Name = "PnlSiSeIdentityCard";
            this.PnlSiSeIdentityCard.Size = new System.Drawing.Size(1211, 145);
            this.PnlSiSeIdentityCard.TabIndex = 5;
            // 
            // LblSiSeIdPhotoHitagRecHint
            // 
            this.LblSiSeIdPhotoHitagRecHint.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiSeIdPhotoHitagRecHint.Location = new System.Drawing.Point(876, 113);
            this.LblSiSeIdPhotoHitagRecHint.Name = "LblSiSeIdPhotoHitagRecHint";
            this.LblSiSeIdPhotoHitagRecHint.Size = new System.Drawing.Size(322, 18);
            this.LblSiSeIdPhotoHitagRecHint.TabIndex = 87;
            this.LblSiSeIdPhotoHitagRecHint.Text = "Hinweis: ein Lichtbildausweis (Hitag) wurde angeordnet.";
            this.LblSiSeIdPhotoHitagRecHint.Visible = false;
            // 
            // CbxSiSeIdPhotoHitagRec
            // 
            this.CbxSiSeIdPhotoHitagRec.Enabled = false;
            this.CbxSiSeIdPhotoHitagRec.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Millimeter, ((byte)(0)));
            this.CbxSiSeIdPhotoHitagRec.ForeColor = System.Drawing.SystemColors.Control;
            this.CbxSiSeIdPhotoHitagRec.Location = new System.Drawing.Point(26, 107);
            this.CbxSiSeIdPhotoHitagRec.Name = "CbxSiSeIdPhotoHitagRec";
            this.CbxSiSeIdPhotoHitagRec.Size = new System.Drawing.Size(24, 24);
            this.CbxSiSeIdPhotoHitagRec.TabIndex = 81;
            this.CbxSiSeIdPhotoHitagRec.Text = "B";
            this.CbxSiSeIdPhotoHitagRec.CheckedChanged += new System.EventHandler(this.CbxSiSeIdPhotoHitagRec_CheckedChanged);
            // 
            // DatSiSeIdPhotoHitagRec
            // 
            this.DatSiSeIdPhotoHitagRec.CustomFormat = "dd.MM.yyyy";
            this.DatSiSeIdPhotoHitagRec.Enabled = false;
            this.DatSiSeIdPhotoHitagRec.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DatSiSeIdPhotoHitagRec.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatSiSeIdPhotoHitagRec.Location = new System.Drawing.Point(357, 110);
            this.DatSiSeIdPhotoHitagRec.Name = "DatSiSeIdPhotoHitagRec";
            this.DatSiSeIdPhotoHitagRec.Size = new System.Drawing.Size(88, 21);
            this.DatSiSeIdPhotoHitagRec.TabIndex = 82;
            this.DatSiSeIdPhotoHitagRec.Value = new System.DateTime(2003, 10, 6, 0, 0, 0, 0);
            // 
            // LblSiSeIdPhotoHitagRecBy
            // 
            this.LblSiSeIdPhotoHitagRecBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiSeIdPhotoHitagRecBy.Location = new System.Drawing.Point(512, 112);
            this.LblSiSeIdPhotoHitagRecBy.Name = "LblSiSeIdPhotoHitagRecBy";
            this.LblSiSeIdPhotoHitagRecBy.Size = new System.Drawing.Size(112, 16);
            this.LblSiSeIdPhotoHitagRecBy.TabIndex = 86;
            this.LblSiSeIdPhotoHitagRecBy.Text = "ausgegeben durch";
            // 
            // LblSiSeIdPhotoHitagRec
            // 
            this.LblSiSeIdPhotoHitagRec.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiSeIdPhotoHitagRec.Location = new System.Drawing.Point(58, 112);
            this.LblSiSeIdPhotoHitagRec.Name = "LblSiSeIdPhotoHitagRec";
            this.LblSiSeIdPhotoHitagRec.Size = new System.Drawing.Size(245, 16);
            this.LblSiSeIdPhotoHitagRec.TabIndex = 85;
            this.LblSiSeIdPhotoHitagRec.Text = "Lichtbildausweis (Hitag) erhalten";
            // 
            // TxtSiSeIdPhotoHitagRecBy
            // 
            this.TxtSiSeIdPhotoHitagRecBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSiSeIdPhotoHitagRecBy.Location = new System.Drawing.Point(640, 110);
            this.TxtSiSeIdPhotoHitagRecBy.Name = "TxtSiSeIdPhotoHitagRecBy";
            this.TxtSiSeIdPhotoHitagRecBy.ReadOnly = true;
            this.TxtSiSeIdPhotoHitagRecBy.Size = new System.Drawing.Size(230, 21);
            this.TxtSiSeIdPhotoHitagRecBy.TabIndex = 83;
            this.TxtSiSeIdPhotoHitagRecBy.TabStop = false;
            // 
            // LblSiSeIdPhotoSmActRecOn
            // 
            this.LblSiSeIdPhotoSmActRecOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiSeIdPhotoSmActRecOn.Location = new System.Drawing.Point(320, 112);
            this.LblSiSeIdPhotoSmActRecOn.Name = "LblSiSeIdPhotoSmActRecOn";
            this.LblSiSeIdPhotoSmActRecOn.Size = new System.Drawing.Size(40, 16);
            this.LblSiSeIdPhotoSmActRecOn.TabIndex = 84;
            this.LblSiSeIdPhotoSmActRecOn.Text = "am";
            // 
            // LblSiSeIdPhotoTitle
            // 
            this.LblSiSeIdPhotoTitle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiSeIdPhotoTitle.Location = new System.Drawing.Point(11, 9);
            this.LblSiSeIdPhotoTitle.Name = "LblSiSeIdPhotoTitle";
            this.LblSiSeIdPhotoTitle.Size = new System.Drawing.Size(176, 16);
            this.LblSiSeIdPhotoTitle.TabIndex = 80;
            this.LblSiSeIdPhotoTitle.Text = "Lichtbildausweis";
            // 
            // CbxSiSeIdPhotoSmActRec
            // 
            this.CbxSiSeIdPhotoSmActRec.Enabled = false;
            this.CbxSiSeIdPhotoSmActRec.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Millimeter, ((byte)(0)));
            this.CbxSiSeIdPhotoSmActRec.ForeColor = System.Drawing.SystemColors.Control;
            this.CbxSiSeIdPhotoSmActRec.Location = new System.Drawing.Point(26, 58);
            this.CbxSiSeIdPhotoSmActRec.Name = "CbxSiSeIdPhotoSmActRec";
            this.CbxSiSeIdPhotoSmActRec.Size = new System.Drawing.Size(24, 24);
            this.CbxSiSeIdPhotoSmActRec.TabIndex = 7;
            this.CbxSiSeIdPhotoSmActRec.Text = "B";
            // 
            // DatSiSeIdPhotoSmActRec
            // 
            this.DatSiSeIdPhotoSmActRec.CustomFormat = "dd.MM.yyyy";
            this.DatSiSeIdPhotoSmActRec.Enabled = false;
            this.DatSiSeIdPhotoSmActRec.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DatSiSeIdPhotoSmActRec.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatSiSeIdPhotoSmActRec.Location = new System.Drawing.Point(357, 61);
            this.DatSiSeIdPhotoSmActRec.Name = "DatSiSeIdPhotoSmActRec";
            this.DatSiSeIdPhotoSmActRec.Size = new System.Drawing.Size(88, 21);
            this.DatSiSeIdPhotoSmActRec.TabIndex = 8;
            this.DatSiSeIdPhotoSmActRec.Value = new System.DateTime(2003, 10, 6, 0, 0, 0, 0);
            this.DatSiSeIdPhotoSmActRec.Leave += new System.EventHandler(this.DatSiSeIdentityCardRecievedOn_Leave);
            // 
            // LblSiSeIdPhotoSmAct
            // 
            this.LblSiSeIdPhotoSmAct.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiSeIdPhotoSmAct.Location = new System.Drawing.Point(58, 36);
            this.LblSiSeIdPhotoSmAct.Name = "LblSiSeIdPhotoSmAct";
            this.LblSiSeIdPhotoSmAct.Size = new System.Drawing.Size(176, 16);
            this.LblSiSeIdPhotoSmAct.TabIndex = 79;
            this.LblSiSeIdPhotoSmAct.Text = "Lichtbildausweis gewünscht";
            // 
            // LblSiSeIdPhotoSmActRecBy
            // 
            this.LblSiSeIdPhotoSmActRecBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiSeIdPhotoSmActRecBy.Location = new System.Drawing.Point(512, 63);
            this.LblSiSeIdPhotoSmActRecBy.Name = "LblSiSeIdPhotoSmActRecBy";
            this.LblSiSeIdPhotoSmActRecBy.Size = new System.Drawing.Size(112, 16);
            this.LblSiSeIdPhotoSmActRecBy.TabIndex = 70;
            this.LblSiSeIdPhotoSmActRecBy.Text = "ausgegeben durch";
            // 
            // LblSiSeIdPhotoSmActRec
            // 
            this.LblSiSeIdPhotoSmActRec.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiSeIdPhotoSmActRec.Location = new System.Drawing.Point(58, 63);
            this.LblSiSeIdPhotoSmActRec.Name = "LblSiSeIdPhotoSmActRec";
            this.LblSiSeIdPhotoSmActRec.Size = new System.Drawing.Size(260, 16);
            this.LblSiSeIdPhotoSmActRec.TabIndex = 68;
            this.LblSiSeIdPhotoSmActRec.Text = "Lichtbildausweis aus SmartAct erhalten";
            // 
            // TxtSiSeIdPhotoSmActRecBy
            // 
            this.TxtSiSeIdPhotoSmActRecBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSiSeIdPhotoSmActRecBy.Location = new System.Drawing.Point(640, 61);
            this.TxtSiSeIdPhotoSmActRecBy.Name = "TxtSiSeIdPhotoSmActRecBy";
            this.TxtSiSeIdPhotoSmActRecBy.ReadOnly = true;
            this.TxtSiSeIdPhotoSmActRecBy.Size = new System.Drawing.Size(230, 21);
            this.TxtSiSeIdPhotoSmActRecBy.TabIndex = 9;
            this.TxtSiSeIdPhotoSmActRecBy.TabStop = false;
            // 
            // LblSiSeIdentityCardRecievedOn
            // 
            this.LblSiSeIdentityCardRecievedOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiSeIdentityCardRecievedOn.Location = new System.Drawing.Point(320, 63);
            this.LblSiSeIdentityCardRecievedOn.Name = "LblSiSeIdentityCardRecievedOn";
            this.LblSiSeIdentityCardRecievedOn.Size = new System.Drawing.Size(40, 16);
            this.LblSiSeIdentityCardRecievedOn.TabIndex = 60;
            this.LblSiSeIdentityCardRecievedOn.Text = "am";
            // 
            // RbtSiSeIdPhotoSmAct
            // 
            this.RbtSiSeIdPhotoSmAct.Enabled = false;
            this.RbtSiSeIdPhotoSmAct.Location = new System.Drawing.Point(26, 36);
            this.RbtSiSeIdPhotoSmAct.Name = "RbtSiSeIdPhotoSmAct";
            this.RbtSiSeIdPhotoSmAct.Size = new System.Drawing.Size(24, 16);
            this.RbtSiSeIdPhotoSmAct.TabIndex = 6;
            // 
            // PnlSiSeVehicleEntranceLong
            // 
            this.PnlSiSeVehicleEntranceLong.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlSiSeVehicleEntranceLong.Controls.Add(this.PnlSiSeVehicleEntranceLongControl);
            this.PnlSiSeVehicleEntranceLong.Controls.Add(this.DatSiSeVehicleEntranceLongReceivedOn);
            this.PnlSiSeVehicleEntranceLong.Controls.Add(this.LblSiSeVehicleEntranceLongReceivedBy);
            this.PnlSiSeVehicleEntranceLong.Controls.Add(this.TxtSiSeVehicleEntranceLongReceivedBy);
            this.PnlSiSeVehicleEntranceLong.Controls.Add(this.LblSiSeVehicleEntranceLongReceivedOn);
            this.PnlSiSeVehicleEntranceLong.Controls.Add(this.LblSiSeVehicleEntranceLong);
            this.PnlSiSeVehicleEntranceLong.Controls.Add(this.RbtSiSeVehicleEntranceLong);
            this.PnlSiSeVehicleEntranceLong.Controls.Add(this.LblSiSeVehicleEntranceLongReceived);
            this.PnlSiSeVehicleEntranceLong.Location = new System.Drawing.Point(627, 447);
            this.PnlSiSeVehicleEntranceLong.Name = "PnlSiSeVehicleEntranceLong";
            this.PnlSiSeVehicleEntranceLong.Size = new System.Drawing.Size(600, 120);
            this.PnlSiSeVehicleEntranceLong.TabIndex = 21;
            this.PnlSiSeVehicleEntranceLong.TabStop = true;
            // 
            // PnlSiSeVehicleEntranceLongControl
            // 
            this.PnlSiSeVehicleEntranceLongControl.Controls.Add(this.RbtSiSeVehicleEntranceLongNo);
            this.PnlSiSeVehicleEntranceLongControl.Controls.Add(this.RbtSiSeVehicleEntranceLongYes);
            this.PnlSiSeVehicleEntranceLongControl.Location = new System.Drawing.Point(181, 33);
            this.PnlSiSeVehicleEntranceLongControl.Name = "PnlSiSeVehicleEntranceLongControl";
            this.PnlSiSeVehicleEntranceLongControl.Size = new System.Drawing.Size(224, 32);
            this.PnlSiSeVehicleEntranceLongControl.TabIndex = 23;
            // 
            // RbtSiSeVehicleEntranceLongNo
            // 
            this.RbtSiSeVehicleEntranceLongNo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtSiSeVehicleEntranceLongNo.Location = new System.Drawing.Point(116, 5);
            this.RbtSiSeVehicleEntranceLongNo.Name = "RbtSiSeVehicleEntranceLongNo";
            this.RbtSiSeVehicleEntranceLongNo.Size = new System.Drawing.Size(88, 24);
            this.RbtSiSeVehicleEntranceLongNo.TabIndex = 25;
            this.RbtSiSeVehicleEntranceLongNo.Text = "Abgelehnt";
            this.RbtSiSeVehicleEntranceLongNo.CheckedChanged += new System.EventHandler(this.RbtSiSeVehicleEntranceLongNo_CheckedChanged);
            // 
            // RbtSiSeVehicleEntranceLongYes
            // 
            this.RbtSiSeVehicleEntranceLongYes.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtSiSeVehicleEntranceLongYes.Location = new System.Drawing.Point(17, 5);
            this.RbtSiSeVehicleEntranceLongYes.Name = "RbtSiSeVehicleEntranceLongYes";
            this.RbtSiSeVehicleEntranceLongYes.Size = new System.Drawing.Size(96, 24);
            this.RbtSiSeVehicleEntranceLongYes.TabIndex = 24;
            this.RbtSiSeVehicleEntranceLongYes.Text = "Genehmigt";
            this.RbtSiSeVehicleEntranceLongYes.CheckedChanged += new System.EventHandler(this.RbtSiSeVehicleEntranceLongYes_CheckedChanged);
            // 
            // DatSiSeVehicleEntranceLongReceivedOn
            // 
            this.DatSiSeVehicleEntranceLongReceivedOn.CustomFormat = "dd.MM.yyyy";
            this.DatSiSeVehicleEntranceLongReceivedOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DatSiSeVehicleEntranceLongReceivedOn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatSiSeVehicleEntranceLongReceivedOn.Location = new System.Drawing.Point(65, 78);
            this.DatSiSeVehicleEntranceLongReceivedOn.Name = "DatSiSeVehicleEntranceLongReceivedOn";
            this.DatSiSeVehicleEntranceLongReceivedOn.Size = new System.Drawing.Size(88, 21);
            this.DatSiSeVehicleEntranceLongReceivedOn.TabIndex = 26;
            this.DatSiSeVehicleEntranceLongReceivedOn.Value = new System.DateTime(2003, 10, 6, 0, 0, 0, 0);
            this.DatSiSeVehicleEntranceLongReceivedOn.Leave += new System.EventHandler(this.DatSiSeVehicleEntranceLongReceivedOn_Leave);
            // 
            // LblSiSeVehicleEntranceLongReceivedBy
            // 
            this.LblSiSeVehicleEntranceLongReceivedBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiSeVehicleEntranceLongReceivedBy.Location = new System.Drawing.Point(182, 80);
            this.LblSiSeVehicleEntranceLongReceivedBy.Name = "LblSiSeVehicleEntranceLongReceivedBy";
            this.LblSiSeVehicleEntranceLongReceivedBy.Size = new System.Drawing.Size(40, 16);
            this.LblSiSeVehicleEntranceLongReceivedBy.TabIndex = 104;
            this.LblSiSeVehicleEntranceLongReceivedBy.Text = "durch";
            // 
            // TxtSiSeVehicleEntranceLongReceivedBy
            // 
            this.TxtSiSeVehicleEntranceLongReceivedBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSiSeVehicleEntranceLongReceivedBy.Location = new System.Drawing.Point(224, 78);
            this.TxtSiSeVehicleEntranceLongReceivedBy.Name = "TxtSiSeVehicleEntranceLongReceivedBy";
            this.TxtSiSeVehicleEntranceLongReceivedBy.ReadOnly = true;
            this.TxtSiSeVehicleEntranceLongReceivedBy.Size = new System.Drawing.Size(230, 21);
            this.TxtSiSeVehicleEntranceLongReceivedBy.TabIndex = 27;
            this.TxtSiSeVehicleEntranceLongReceivedBy.TabStop = false;
            // 
            // LblSiSeVehicleEntranceLongReceivedOn
            // 
            this.LblSiSeVehicleEntranceLongReceivedOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiSeVehicleEntranceLongReceivedOn.Location = new System.Drawing.Point(26, 80);
            this.LblSiSeVehicleEntranceLongReceivedOn.Name = "LblSiSeVehicleEntranceLongReceivedOn";
            this.LblSiSeVehicleEntranceLongReceivedOn.Size = new System.Drawing.Size(33, 16);
            this.LblSiSeVehicleEntranceLongReceivedOn.TabIndex = 101;
            this.LblSiSeVehicleEntranceLongReceivedOn.Text = "am";
            // 
            // LblSiSeVehicleEntranceLong
            // 
            this.LblSiSeVehicleEntranceLong.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiSeVehicleEntranceLong.Location = new System.Drawing.Point(36, 11);
            this.LblSiSeVehicleEntranceLong.Name = "LblSiSeVehicleEntranceLong";
            this.LblSiSeVehicleEntranceLong.Size = new System.Drawing.Size(280, 15);
            this.LblSiSeVehicleEntranceLong.TabIndex = 98;
            this.LblSiSeVehicleEntranceLong.Text = "Gewünscht Kfz-Einfahrt lang ";
            // 
            // RbtSiSeVehicleEntranceLong
            // 
            this.RbtSiSeVehicleEntranceLong.Enabled = false;
            this.RbtSiSeVehicleEntranceLong.Location = new System.Drawing.Point(16, 11);
            this.RbtSiSeVehicleEntranceLong.Name = "RbtSiSeVehicleEntranceLong";
            this.RbtSiSeVehicleEntranceLong.Size = new System.Drawing.Size(24, 16);
            this.RbtSiSeVehicleEntranceLong.TabIndex = 22;
            // 
            // LblSiSeVehicleEntranceLongReceived
            // 
            this.LblSiSeVehicleEntranceLongReceived.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiSeVehicleEntranceLongReceived.Location = new System.Drawing.Point(26, 41);
            this.LblSiSeVehicleEntranceLongReceived.Name = "LblSiSeVehicleEntranceLongReceived";
            this.LblSiSeVehicleEntranceLongReceived.Size = new System.Drawing.Size(168, 16);
            this.LblSiSeVehicleEntranceLongReceived.TabIndex = 94;
            this.LblSiSeVehicleEntranceLongReceived.Text = "Kfz-Einfahrt lang";
            // 
            // PnlSiSeVehicleEntranceShort
            // 
            this.PnlSiSeVehicleEntranceShort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlSiSeVehicleEntranceShort.Controls.Add(this.PnlSiSeVehicleEntranceShortControl);
            this.PnlSiSeVehicleEntranceShort.Controls.Add(this.DatSiSeVehicleEntranceShortReceivedOn);
            this.PnlSiSeVehicleEntranceShort.Controls.Add(this.LblSiSeVehicleEntranceShortReceivedBy);
            this.PnlSiSeVehicleEntranceShort.Controls.Add(this.TxtSiSeVehicleEntranceShortReceivedBy);
            this.PnlSiSeVehicleEntranceShort.Controls.Add(this.LblSiSeVehicleEntranceShortReceivedOn);
            this.PnlSiSeVehicleEntranceShort.Controls.Add(this.LblSiSeVehicleEntranceShort);
            this.PnlSiSeVehicleEntranceShort.Controls.Add(this.RbtSiSeVehicleEntranceShort);
            this.PnlSiSeVehicleEntranceShort.Controls.Add(this.PnlSiSeVehicleEntranceShortReceived);
            this.PnlSiSeVehicleEntranceShort.Location = new System.Drawing.Point(16, 447);
            this.PnlSiSeVehicleEntranceShort.Name = "PnlSiSeVehicleEntranceShort";
            this.PnlSiSeVehicleEntranceShort.Size = new System.Drawing.Size(600, 120);
            this.PnlSiSeVehicleEntranceShort.TabIndex = 14;
            this.PnlSiSeVehicleEntranceShort.TabStop = true;
            // 
            // PnlSiSeVehicleEntranceShortControl
            // 
            this.PnlSiSeVehicleEntranceShortControl.Controls.Add(this.RbtSiSeVehicleEntranceShortReceivedNo);
            this.PnlSiSeVehicleEntranceShortControl.Controls.Add(this.RbtSiSeVehicleEntranceShortReceivedYes);
            this.PnlSiSeVehicleEntranceShortControl.Location = new System.Drawing.Point(181, 33);
            this.PnlSiSeVehicleEntranceShortControl.Name = "PnlSiSeVehicleEntranceShortControl";
            this.PnlSiSeVehicleEntranceShortControl.Size = new System.Drawing.Size(240, 32);
            this.PnlSiSeVehicleEntranceShortControl.TabIndex = 16;
            // 
            // RbtSiSeVehicleEntranceShortReceivedNo
            // 
            this.RbtSiSeVehicleEntranceShortReceivedNo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtSiSeVehicleEntranceShortReceivedNo.Location = new System.Drawing.Point(116, 5);
            this.RbtSiSeVehicleEntranceShortReceivedNo.Name = "RbtSiSeVehicleEntranceShortReceivedNo";
            this.RbtSiSeVehicleEntranceShortReceivedNo.Size = new System.Drawing.Size(88, 24);
            this.RbtSiSeVehicleEntranceShortReceivedNo.TabIndex = 18;
            this.RbtSiSeVehicleEntranceShortReceivedNo.Text = "Abgelehnt";
            this.RbtSiSeVehicleEntranceShortReceivedNo.CheckedChanged += new System.EventHandler(this.RbtSiSeVehicleEntranceShortReceivedNo_CheckedChanged);
            // 
            // RbtSiSeVehicleEntranceShortReceivedYes
            // 
            this.RbtSiSeVehicleEntranceShortReceivedYes.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtSiSeVehicleEntranceShortReceivedYes.Location = new System.Drawing.Point(17, 5);
            this.RbtSiSeVehicleEntranceShortReceivedYes.Name = "RbtSiSeVehicleEntranceShortReceivedYes";
            this.RbtSiSeVehicleEntranceShortReceivedYes.Size = new System.Drawing.Size(88, 24);
            this.RbtSiSeVehicleEntranceShortReceivedYes.TabIndex = 17;
            this.RbtSiSeVehicleEntranceShortReceivedYes.Text = "Genehmigt";
            this.RbtSiSeVehicleEntranceShortReceivedYes.CheckedChanged += new System.EventHandler(this.RbtSiSeVehicleEntranceShortReceivedYes_CheckedChanged);
            // 
            // DatSiSeVehicleEntranceShortReceivedOn
            // 
            this.DatSiSeVehicleEntranceShortReceivedOn.CustomFormat = "dd.MM.yyyy";
            this.DatSiSeVehicleEntranceShortReceivedOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DatSiSeVehicleEntranceShortReceivedOn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatSiSeVehicleEntranceShortReceivedOn.Location = new System.Drawing.Point(65, 78);
            this.DatSiSeVehicleEntranceShortReceivedOn.Name = "DatSiSeVehicleEntranceShortReceivedOn";
            this.DatSiSeVehicleEntranceShortReceivedOn.Size = new System.Drawing.Size(88, 21);
            this.DatSiSeVehicleEntranceShortReceivedOn.TabIndex = 19;
            this.DatSiSeVehicleEntranceShortReceivedOn.Value = new System.DateTime(2003, 10, 6, 0, 0, 0, 0);
            this.DatSiSeVehicleEntranceShortReceivedOn.Leave += new System.EventHandler(this.DatSiSeVehicleEntranceShortReceivedOn_Leave);
            // 
            // LblSiSeVehicleEntranceShortReceivedBy
            // 
            this.LblSiSeVehicleEntranceShortReceivedBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiSeVehicleEntranceShortReceivedBy.Location = new System.Drawing.Point(182, 80);
            this.LblSiSeVehicleEntranceShortReceivedBy.Name = "LblSiSeVehicleEntranceShortReceivedBy";
            this.LblSiSeVehicleEntranceShortReceivedBy.Size = new System.Drawing.Size(40, 16);
            this.LblSiSeVehicleEntranceShortReceivedBy.TabIndex = 100;
            this.LblSiSeVehicleEntranceShortReceivedBy.Text = "durch";
            // 
            // TxtSiSeVehicleEntranceShortReceivedBy
            // 
            this.TxtSiSeVehicleEntranceShortReceivedBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSiSeVehicleEntranceShortReceivedBy.Location = new System.Drawing.Point(224, 78);
            this.TxtSiSeVehicleEntranceShortReceivedBy.Name = "TxtSiSeVehicleEntranceShortReceivedBy";
            this.TxtSiSeVehicleEntranceShortReceivedBy.ReadOnly = true;
            this.TxtSiSeVehicleEntranceShortReceivedBy.Size = new System.Drawing.Size(230, 21);
            this.TxtSiSeVehicleEntranceShortReceivedBy.TabIndex = 20;
            this.TxtSiSeVehicleEntranceShortReceivedBy.TabStop = false;
            // 
            // LblSiSeVehicleEntranceShortReceivedOn
            // 
            this.LblSiSeVehicleEntranceShortReceivedOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiSeVehicleEntranceShortReceivedOn.Location = new System.Drawing.Point(26, 80);
            this.LblSiSeVehicleEntranceShortReceivedOn.Name = "LblSiSeVehicleEntranceShortReceivedOn";
            this.LblSiSeVehicleEntranceShortReceivedOn.Size = new System.Drawing.Size(33, 16);
            this.LblSiSeVehicleEntranceShortReceivedOn.TabIndex = 97;
            this.LblSiSeVehicleEntranceShortReceivedOn.Text = "am";
            // 
            // LblSiSeVehicleEntranceShort
            // 
            this.LblSiSeVehicleEntranceShort.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiSeVehicleEntranceShort.Location = new System.Drawing.Point(36, 11);
            this.LblSiSeVehicleEntranceShort.Name = "LblSiSeVehicleEntranceShort";
            this.LblSiSeVehicleEntranceShort.Size = new System.Drawing.Size(248, 15);
            this.LblSiSeVehicleEntranceShort.TabIndex = 96;
            this.LblSiSeVehicleEntranceShort.Text = "Gewünscht Kfz-Einfahrt kurz ";
            // 
            // RbtSiSeVehicleEntranceShort
            // 
            this.RbtSiSeVehicleEntranceShort.Enabled = false;
            this.RbtSiSeVehicleEntranceShort.Location = new System.Drawing.Point(16, 11);
            this.RbtSiSeVehicleEntranceShort.Name = "RbtSiSeVehicleEntranceShort";
            this.RbtSiSeVehicleEntranceShort.Size = new System.Drawing.Size(24, 16);
            this.RbtSiSeVehicleEntranceShort.TabIndex = 15;
            // 
            // PnlSiSeVehicleEntranceShortReceived
            // 
            this.PnlSiSeVehicleEntranceShortReceived.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PnlSiSeVehicleEntranceShortReceived.Location = new System.Drawing.Point(26, 41);
            this.PnlSiSeVehicleEntranceShortReceived.Name = "PnlSiSeVehicleEntranceShortReceived";
            this.PnlSiSeVehicleEntranceShortReceived.Size = new System.Drawing.Size(200, 16);
            this.PnlSiSeVehicleEntranceShortReceived.TabIndex = 94;
            this.PnlSiSeVehicleEntranceShortReceived.Text = "Kfz-Einfahrt kurz";
            // 
            // PnlSiSeAccess
            // 
            this.PnlSiSeAccess.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlSiSeAccess.Controls.Add(this.GpSiSe2);
            this.PnlSiSeAccess.Controls.Add(this.LblSiSeAccess);
            this.PnlSiSeAccess.Controls.Add(this.PnlSiSeAccessYNRbt);
            this.PnlSiSeAccess.Controls.Add(this.LblSiSeAccessTitle);
            this.PnlSiSeAccess.Controls.Add(this.BtnSiSeAccessRevoke);
            this.PnlSiSeAccess.Controls.Add(this.TxtSiSeAccessAuthorizationComment);
            this.PnlSiSeAccess.Controls.Add(this.LblSiSeAccessHint);
            this.PnlSiSeAccess.Controls.Add(this.LblSiSeAccessComment);
            this.PnlSiSeAccess.Location = new System.Drawing.Point(16, 270);
            this.PnlSiSeAccess.Name = "PnlSiSeAccess";
            this.PnlSiSeAccess.Size = new System.Drawing.Size(600, 169);
            this.PnlSiSeAccess.TabIndex = 10;
            // 
            // GpSiSe2
            // 
            this.GpSiSe2.Location = new System.Drawing.Point(12, 66);
            this.GpSiSe2.Name = "GpSiSe2";
            this.GpSiSe2.Size = new System.Drawing.Size(575, 5);
            this.GpSiSe2.TabIndex = 88;
            this.GpSiSe2.TabStop = false;
            // 
            // LblSiSeAccess
            // 
            this.LblSiSeAccess.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiSeAccess.Location = new System.Drawing.Point(26, 31);
            this.LblSiSeAccess.Name = "LblSiSeAccess";
            this.LblSiSeAccess.Size = new System.Drawing.Size(163, 32);
            this.LblSiSeAccess.TabIndex = 83;
            this.LblSiSeAccess.Text = "Zutrittsberechtigung erteilt";
            this.LblSiSeAccess.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PnlSiSeAccessYNRbt
            // 
            this.PnlSiSeAccessYNRbt.Controls.Add(this.RbtSiSeAccessAuthNo);
            this.PnlSiSeAccessYNRbt.Controls.Add(this.RbtSiSeAccessAuthYes);
            this.PnlSiSeAccessYNRbt.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PnlSiSeAccessYNRbt.Location = new System.Drawing.Point(195, 26);
            this.PnlSiSeAccessYNRbt.Name = "PnlSiSeAccessYNRbt";
            this.PnlSiSeAccessYNRbt.Size = new System.Drawing.Size(120, 37);
            this.PnlSiSeAccessYNRbt.TabIndex = 82;
            this.PnlSiSeAccessYNRbt.TabStop = true;
            // 
            // RbtSiSeAccessAuthNo
            // 
            this.RbtSiSeAccessAuthNo.Checked = true;
            this.RbtSiSeAccessAuthNo.Enabled = false;
            this.RbtSiSeAccessAuthNo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtSiSeAccessAuthNo.Location = new System.Drawing.Point(56, 9);
            this.RbtSiSeAccessAuthNo.Name = "RbtSiSeAccessAuthNo";
            this.RbtSiSeAccessAuthNo.Size = new System.Drawing.Size(57, 24);
            this.RbtSiSeAccessAuthNo.TabIndex = 19;
            this.RbtSiSeAccessAuthNo.TabStop = true;
            this.RbtSiSeAccessAuthNo.Text = "Nein";
            // 
            // RbtSiSeAccessAuthYes
            // 
            this.RbtSiSeAccessAuthYes.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbtSiSeAccessAuthYes.Location = new System.Drawing.Point(3, 9);
            this.RbtSiSeAccessAuthYes.Name = "RbtSiSeAccessAuthYes";
            this.RbtSiSeAccessAuthYes.Size = new System.Drawing.Size(40, 24);
            this.RbtSiSeAccessAuthYes.TabIndex = 18;
            this.RbtSiSeAccessAuthYes.Text = "Ja";
            this.RbtSiSeAccessAuthYes.CheckedChanged += new System.EventHandler(this.RbtSiSeAccessAuthYes_CheckedChanged);
            // 
            // LblSiSeAccessTitle
            // 
            this.LblSiSeAccessTitle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiSeAccessTitle.Location = new System.Drawing.Point(11, 9);
            this.LblSiSeAccessTitle.Name = "LblSiSeAccessTitle";
            this.LblSiSeAccessTitle.Size = new System.Drawing.Size(144, 16);
            this.LblSiSeAccessTitle.TabIndex = 81;
            this.LblSiSeAccessTitle.Text = "Zutrittsberechtigung";
            // 
            // BtnSiSeAccessRevoke
            // 
            this.BtnSiSeAccessRevoke.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSiSeAccessRevoke.Location = new System.Drawing.Point(446, 132);
            this.BtnSiSeAccessRevoke.Name = "BtnSiSeAccessRevoke";
            this.BtnSiSeAccessRevoke.Size = new System.Drawing.Size(136, 25);
            this.BtnSiSeAccessRevoke.TabIndex = 79;
            this.BtnSiSeAccessRevoke.Tag = "";
            this.BtnSiSeAccessRevoke.Text = "Zutritt sp&erren";
            this.TooIdentityCard.SetToolTip(this.BtnSiSeAccessRevoke, "Zutritt sperren und Ausweise entfernen");
            this.BtnSiSeAccessRevoke.Click += new System.EventHandler(this.BtnSiSeAccessRevoke_Click);
            // 
            // TxtSiSeAccessAuthorizationComment
            // 
            this.TxtSiSeAccessAuthorizationComment.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSiSeAccessAuthorizationComment.Location = new System.Drawing.Point(29, 103);
            this.TxtSiSeAccessAuthorizationComment.MaxLength = 100;
            this.TxtSiSeAccessAuthorizationComment.Name = "TxtSiSeAccessAuthorizationComment";
            this.TxtSiSeAccessAuthorizationComment.Size = new System.Drawing.Size(553, 21);
            this.TxtSiSeAccessAuthorizationComment.TabIndex = 11;
            // 
            // LblSiSeAccessHint
            // 
            this.LblSiSeAccessHint.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiSeAccessHint.Location = new System.Drawing.Point(26, 137);
            this.LblSiSeAccessHint.Name = "LblSiSeAccessHint";
            this.LblSiSeAccessHint.Size = new System.Drawing.Size(347, 23);
            this.LblSiSeAccessHint.TabIndex = 80;
            this.LblSiSeAccessHint.Text = "Zutritt sperren und beide Ausweise im ZKS entfernen";
            // 
            // LblSiSeAccessComment
            // 
            this.LblSiSeAccessComment.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiSeAccessComment.Location = new System.Drawing.Point(26, 82);
            this.LblSiSeAccessComment.Name = "LblSiSeAccessComment";
            this.LblSiSeAccessComment.Size = new System.Drawing.Size(192, 21);
            this.LblSiSeAccessComment.TabIndex = 66;
            this.LblSiSeAccessComment.Text = "Kommentar Zutrittsberechtigung";
            // 
            // PnlSiSeSiteSecurity
            // 
            this.PnlSiSeSiteSecurity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlSiSeSiteSecurity.Controls.Add(this.lblSiteSecBriTitle);
            this.PnlSiSeSiteSecurity.Controls.Add(this.CbxSiSeSiteSecBriRec);
            this.PnlSiSeSiteSecurity.Controls.Add(this.DatSiSeSiteSecBriRec);
            this.PnlSiSeSiteSecurity.Controls.Add(this.LblSiSeSiteBriOrd);
            this.PnlSiSeSiteSecurity.Controls.Add(this.LblSiSeSiteSecBriRecBy);
            this.PnlSiSeSiteSecurity.Controls.Add(this.LblSiSeSiteSecBriRec);
            this.PnlSiSeSiteSecurity.Controls.Add(this.TxtSiSeSiteSecBriRecBy);
            this.PnlSiSeSiteSecurity.Controls.Add(this.LblSiSeSiteSecBriRecOn);
            this.PnlSiSeSiteSecurity.Controls.Add(this.RbtSiSeSiteSecBri);
            this.PnlSiSeSiteSecurity.Location = new System.Drawing.Point(16, 13);
            this.PnlSiSeSiteSecurity.Name = "PnlSiSeSiteSecurity";
            this.PnlSiSeSiteSecurity.Size = new System.Drawing.Size(1211, 96);
            this.PnlSiSeSiteSecurity.TabIndex = 0;
            // 
            // lblSiteSecBriTitle
            // 
            this.lblSiteSecBriTitle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSiteSecBriTitle.Location = new System.Drawing.Point(11, 9);
            this.lblSiteSecBriTitle.Name = "lblSiteSecBriTitle";
            this.lblSiteSecBriTitle.Size = new System.Drawing.Size(189, 19);
            this.lblSiteSecBriTitle.TabIndex = 79;
            this.lblSiteSecBriTitle.Text = "Werksicherheitsbelehrung";
            // 
            // CbxSiSeSiteSecBriRec
            // 
            this.CbxSiSeSiteSecBriRec.Enabled = false;
            this.CbxSiSeSiteSecBriRec.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Millimeter, ((byte)(0)));
            this.CbxSiSeSiteSecBriRec.ForeColor = System.Drawing.SystemColors.Control;
            this.CbxSiSeSiteSecBriRec.Location = new System.Drawing.Point(26, 58);
            this.CbxSiSeSiteSecBriRec.Name = "CbxSiSeSiteSecBriRec";
            this.CbxSiSeSiteSecBriRec.Size = new System.Drawing.Size(24, 24);
            this.CbxSiSeSiteSecBriRec.TabIndex = 2;
            this.CbxSiSeSiteSecBriRec.Text = "B";
            // 
            // DatSiSeSiteSecBriRec
            // 
            this.DatSiSeSiteSecBriRec.CustomFormat = "dd.MM.yyyy";
            this.DatSiSeSiteSecBriRec.Enabled = false;
            this.DatSiSeSiteSecBriRec.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DatSiSeSiteSecBriRec.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatSiSeSiteSecBriRec.Location = new System.Drawing.Point(357, 61);
            this.DatSiSeSiteSecBriRec.Name = "DatSiSeSiteSecBriRec";
            this.DatSiSeSiteSecBriRec.Size = new System.Drawing.Size(88, 21);
            this.DatSiSeSiteSecBriRec.TabIndex = 3;
            this.DatSiSeSiteSecBriRec.Value = new System.DateTime(2003, 10, 6, 0, 0, 0, 0);
            this.DatSiSeSiteSecBriRec.Leave += new System.EventHandler(this.DatSiSeSiteSecurityReceivedOn_Leave);
            // 
            // LblSiSeSiteBriOrd
            // 
            this.LblSiSeSiteBriOrd.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiSeSiteBriOrd.Location = new System.Drawing.Point(58, 36);
            this.LblSiSeSiteBriOrd.Name = "LblSiSeSiteBriOrd";
            this.LblSiSeSiteBriOrd.Size = new System.Drawing.Size(248, 16);
            this.LblSiSeSiteBriOrd.TabIndex = 78;
            this.LblSiSeSiteBriOrd.Text = "Werksicherheitsbelehrung angeordnet";
            // 
            // LblSiSeSiteSecBriRecBy
            // 
            this.LblSiSeSiteSecBriRecBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiSeSiteSecBriRecBy.Location = new System.Drawing.Point(584, 63);
            this.LblSiSeSiteSecBriRecBy.Name = "LblSiSeSiteSecBriRecBy";
            this.LblSiSeSiteSecBriRecBy.Size = new System.Drawing.Size(40, 16);
            this.LblSiSeSiteSecBriRecBy.TabIndex = 69;
            this.LblSiSeSiteSecBriRecBy.Text = "durch";
            // 
            // LblSiSeSiteSecBriRec
            // 
            this.LblSiSeSiteSecBriRec.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiSeSiteSecBriRec.Location = new System.Drawing.Point(58, 63);
            this.LblSiSeSiteSecBriRec.Name = "LblSiSeSiteSecBriRec";
            this.LblSiSeSiteSecBriRec.Size = new System.Drawing.Size(232, 16);
            this.LblSiSeSiteSecBriRec.TabIndex = 67;
            this.LblSiSeSiteSecBriRec.Text = "Werksicherheitsbelehrung erteilt";
            // 
            // TxtSiSeSiteSecBriRecBy
            // 
            this.TxtSiSeSiteSecBriRecBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSiSeSiteSecBriRecBy.Location = new System.Drawing.Point(640, 61);
            this.TxtSiSeSiteSecBriRecBy.Name = "TxtSiSeSiteSecBriRecBy";
            this.TxtSiSeSiteSecBriRecBy.ReadOnly = true;
            this.TxtSiSeSiteSecBriRecBy.Size = new System.Drawing.Size(230, 21);
            this.TxtSiSeSiteSecBriRecBy.TabIndex = 4;
            this.TxtSiSeSiteSecBriRecBy.TabStop = false;
            // 
            // LblSiSeSiteSecBriRecOn
            // 
            this.LblSiSeSiteSecBriRecOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSiSeSiteSecBriRecOn.Location = new System.Drawing.Point(320, 63);
            this.LblSiSeSiteSecBriRecOn.Name = "LblSiSeSiteSecBriRecOn";
            this.LblSiSeSiteSecBriRecOn.Size = new System.Drawing.Size(40, 16);
            this.LblSiSeSiteSecBriRecOn.TabIndex = 59;
            this.LblSiSeSiteSecBriRecOn.Text = "am";
            // 
            // RbtSiSeSiteSecBri
            // 
            this.RbtSiSeSiteSecBri.Enabled = false;
            this.RbtSiSeSiteSecBri.Location = new System.Drawing.Point(26, 36);
            this.RbtSiSeSiteSecBri.Name = "RbtSiSeSiteSecBri";
            this.RbtSiSeSiteSecBri.Size = new System.Drawing.Size(24, 16);
            this.RbtSiSeSiteSecBri.TabIndex = 1;
            // 
            // LblDeliveryDate
            // 
            this.LblDeliveryDate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblDeliveryDate.Location = new System.Drawing.Point(425, 859);
            this.LblDeliveryDate.Name = "LblDeliveryDate";
            this.LblDeliveryDate.Size = new System.Drawing.Size(96, 16);
            this.LblDeliveryDate.TabIndex = 56;
            this.LblDeliveryDate.Text = "Ausgabedatum";
            // 
            // TxtPassValidUntil
            // 
            this.TxtPassValidUntil.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPassValidUntil.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPassValidUntil.Location = new System.Drawing.Point(303, 857);
            this.TxtPassValidUntil.Name = "TxtPassValidUntil";
            this.TxtPassValidUntil.ReadOnly = true;
            this.TxtPassValidUntil.Size = new System.Drawing.Size(88, 21);
            this.TxtPassValidUntil.TabIndex = 44;
            this.TxtPassValidUntil.TabStop = false;
            // 
            // LblPassValidUntil
            // 
            this.LblPassValidUntil.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblPassValidUntil.Location = new System.Drawing.Point(275, 859);
            this.LblPassValidUntil.Name = "LblPassValidUntil";
            this.LblPassValidUntil.Size = new System.Drawing.Size(24, 16);
            this.LblPassValidUntil.TabIndex = 53;
            this.LblPassValidUntil.Text = "bis";
            // 
            // LblPassValidFrom
            // 
            this.LblPassValidFrom.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblPassValidFrom.Location = new System.Drawing.Point(22, 860);
            this.LblPassValidFrom.Name = "LblPassValidFrom";
            this.LblPassValidFrom.Size = new System.Drawing.Size(152, 16);
            this.LblPassValidFrom.TabIndex = 51;
            this.LblPassValidFrom.Text = "Passierschein  gültig von";
            // 
            // LblCoWorkerData
            // 
            this.LblCoWorkerData.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCoWorkerData.Location = new System.Drawing.Point(16, 48);
            this.LblCoWorkerData.Name = "LblCoWorkerData";
            this.LblCoWorkerData.Size = new System.Drawing.Size(144, 16);
            this.LblCoWorkerData.TabIndex = 58;
            this.LblCoWorkerData.Text = "Fremdfirmenmitarbeiter";
            // 
            // BtnSave
            // 
            this.BtnSave.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Location = new System.Drawing.Point(978, 852);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(125, 30);
            this.BtnSave.TabIndex = 41;
            this.BtnSave.Tag = "";
            this.BtnSave.Text = "Speiche&rn";
            this.TooSave.SetToolTip(this.BtnSave, "Speichern der eingegebenen Daten");
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnSummary
            // 
            this.BtnSummary.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnSummary.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSummary.Location = new System.Drawing.Point(1112, 852);
            this.BtnSummary.Name = "BtnSummary";
            this.BtnSummary.Size = new System.Drawing.Size(145, 30);
            this.BtnSummary.TabIndex = 42;
            this.BtnSummary.Text = "&Zurück zur Übersicht";
            this.TooBackToSummary.SetToolTip(this.BtnSummary, "Zurück zur Übersichtsmaske");
            this.BtnSummary.Click += new System.EventHandler(this.BtnSummary_Click);
            // 
            // BtnPass
            // 
            this.BtnPass.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPass.Location = new System.Drawing.Point(834, 852);
            this.BtnPass.Name = "BtnPass";
            this.BtnPass.Size = new System.Drawing.Size(135, 30);
            this.BtnPass.TabIndex = 47;
            this.BtnPass.Text = "&Passierschein";
            this.TooPass.SetToolTip(this.BtnPass, "Passierschein drucken");
            this.BtnPass.Click += new System.EventHandler(this.BtnPass_Click);
            // 
            // LblMask
            // 
            this.LblMask.BackColor = System.Drawing.Color.White;
            this.LblMask.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMask.Location = new System.Drawing.Point(470, 8);
            this.LblMask.Name = "LblMask";
            this.LblMask.Size = new System.Drawing.Size(352, 32);
            this.LblMask.TabIndex = 75;
            this.LblMask.Text = "FPASS - Erfassung FFMA";
            // 
            // DatPassValidFrom
            // 
            this.DatPassValidFrom.CustomFormat = "dd.MM.yyyy";
            this.DatPassValidFrom.Enabled = false;
            this.DatPassValidFrom.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DatPassValidFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatPassValidFrom.Location = new System.Drawing.Point(173, 857);
            this.DatPassValidFrom.Name = "DatPassValidFrom";
            this.DatPassValidFrom.Size = new System.Drawing.Size(88, 21);
            this.DatPassValidFrom.TabIndex = 43;
            this.DatPassValidFrom.Leave += new System.EventHandler(this.DatPassValidFrom_Leave);
            // 
            // DatDeliveryDate
            // 
            this.DatDeliveryDate.CustomFormat = "dd.MM.yyyy";
            this.DatDeliveryDate.Enabled = false;
            this.DatDeliveryDate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DatDeliveryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatDeliveryDate.Location = new System.Drawing.Point(520, 857);
            this.DatDeliveryDate.Name = "DatDeliveryDate";
            this.DatDeliveryDate.Size = new System.Drawing.Size(88, 21);
            this.DatDeliveryDate.TabIndex = 45;
            this.DatDeliveryDate.Leave += new System.EventHandler(this.DatDeliveryDate_Leave);
            // 
            // LblZKSRetCode
            // 
            this.LblZKSRetCode.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblZKSRetCode.Location = new System.Drawing.Point(643, 853);
            this.LblZKSRetCode.Name = "LblZKSRetCode";
            this.LblZKSRetCode.Size = new System.Drawing.Size(128, 30);
            this.LblZKSRetCode.TabIndex = 76;
            this.LblZKSRetCode.Text = "Nach ZKS erfolgreich übertragen";
            // 
            // TxtZKSRetCode
            // 
            this.TxtZKSRetCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtZKSRetCode.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtZKSRetCode.Location = new System.Drawing.Point(770, 857);
            this.TxtZKSRetCode.Name = "TxtZKSRetCode";
            this.TxtZKSRetCode.ReadOnly = true;
            this.TxtZKSRetCode.Size = new System.Drawing.Size(27, 21);
            this.TxtZKSRetCode.TabIndex = 46;
            this.TxtZKSRetCode.TabStop = false;
            // 
            // FrmCoWorker
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(1264, 959);
            this.Controls.Add(this.LblMask);
            this.Controls.Add(this.TxtZKSRetCode);
            this.Controls.Add(this.LblZKSRetCode);
            this.Controls.Add(this.DatDeliveryDate);
            this.Controls.Add(this.DatPassValidFrom);
            this.Controls.Add(this.BtnPass);
            this.Controls.Add(this.TxtPassValidUntil);
            this.Controls.Add(this.BtnSummary);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.LblCoWorkerData);
            this.Controls.Add(this.PnlCoWorkerData);
            this.Controls.Add(this.LblPassValidFrom);
            this.Controls.Add(this.TabSiteSecurity);
            this.Controls.Add(this.LblDeliveryDate);
            this.Controls.Add(this.LblPassValidUntil);
            this.Name = "FrmCoWorker";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "FPASS - Erfassung";
            this.Activated += new System.EventHandler(this.FrmCoWorker_Activated);
            this.Controls.SetChildIndex(this.LblPassValidUntil, 0);
            this.Controls.SetChildIndex(this.LblDeliveryDate, 0);
            this.Controls.SetChildIndex(this.TabSiteSecurity, 0);
            this.Controls.SetChildIndex(this.LblPassValidFrom, 0);
            this.Controls.SetChildIndex(this.PnlCoWorkerData, 0);
            this.Controls.SetChildIndex(this.LblCoWorkerData, 0);
            this.Controls.SetChildIndex(this.BtnSave, 0);
            this.Controls.SetChildIndex(this.BtnSummary, 0);
            this.Controls.SetChildIndex(this.TxtPassValidUntil, 0);
            this.Controls.SetChildIndex(this.BtnPass, 0);
            this.Controls.SetChildIndex(this.DatPassValidFrom, 0);
            this.Controls.SetChildIndex(this.DatDeliveryDate, 0);
            this.Controls.SetChildIndex(this.LblZKSRetCode, 0);
            this.Controls.SetChildIndex(this.TxtZKSRetCode, 0);
            this.Controls.SetChildIndex(this.StbBase, 0);
            this.Controls.SetChildIndex(this.LblBaseHead, 0);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            this.Controls.SetChildIndex(this.LblMask, 0);
            ((System.ComponentModel.ISupportInitialize)(this.SbpMessage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sbpenvironment)).EndInit();
            this.PnlCoWorkerData.ResumeLayout(false);
            this.PnlCoWorkerData.PerformLayout();
            this.TabSiteSecurity.ResumeLayout(false);
            this.TapReception.ResumeLayout(false);
            this.PnlTabReception.ResumeLayout(false);
            this.PnlIdCardReader.ResumeLayout(false);
            this.PnlIdCardReader.PerformLayout();
            this.PnlReBasics.ResumeLayout(false);
            this.PnlReBasics.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CoWorkerPhotoBox)).EndInit();
            this.PnlReBriefing.ResumeLayout(false);
            this.PnlReBriefing.PerformLayout();
            this.PnlReAccessApprent.ResumeLayout(false);
            this.PnlReSafetyInstructions.ResumeLayout(false);
            this.PnlReAccessAuthorization.ResumeLayout(false);
            this.PnlReIdentityCard.ResumeLayout(false);
            this.PnlReIdentityCard.PerformLayout();
            this.TapCoordinator.ResumeLayout(false);
            this.PnlTabCoordinator.ResumeLayout(false);
            this.PnlCoSmartAct.ResumeLayout(false);
            this.PnlCoSmartAct.PerformLayout();
            this.PnlCoPhotoCard.ResumeLayout(false);
            this.PnlCoVehicleEntrance.ResumeLayout(false);
            this.PnlCoVehicleEntranceLong.ResumeLayout(false);
            this.PnlCoVehicleEntranceShort.ResumeLayout(false);
            this.PnlCoCheckInCheckOff.ResumeLayout(false);
            this.PnlCoCheckInCheckOff.PerformLayout();
            this.PnlCoDepartmentalBriefing.ResumeLayout(false);
            this.PnlCoData.ResumeLayout(false);
            this.PnlCoData.PerformLayout();
            this.PnlCoPrecautionaryMedical.ResumeLayout(false);
            this.PnlCoRespiratoryMaskBriefing.ResumeLayout(false);
            this.PnlCoIndustrialSafetyBriefingSite.ResumeLayout(false);
            this.PnlCoIndustrialSafetyBriefingSite.PerformLayout();
            this.PnlCoBriefing.ResumeLayout(false);
            this.PnlCoFireman.ResumeLayout(false);
            this.PnlCoBreathingApparatusG26_3.ResumeLayout(false);
            this.PnlCoSiteSecurityBriefing.ResumeLayout(false);
            this.PnlCoCranes.ResumeLayout(false);
            this.PnlCoRaisablePlattform.ResumeLayout(false);
            this.PnlCoPalletLifter.ResumeLayout(false);
            this.PnlCoBreathingApparatusG26_2.ResumeLayout(false);
            this.PnlCoSafetyPass.ResumeLayout(false);
            this.TapSiteFireService.ResumeLayout(false);
            this.PnlTabSiteFireService.ResumeLayout(false);
            this.PnlSiFiMaskLent.ResumeLayout(false);
            this.PnlSiFiMaskLent.PerformLayout();
            this.PnlSiFiAllBriefings.ResumeLayout(false);
            this.PnlSiFiAllBriefings.PerformLayout();
            this.PnlSiFiSiteSecurityBriefingG26_2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.PnlSiFiSiteSecurityBriefingG26_3.ResumeLayout(false);
            this.PnlSiFiMaskBriefing.ResumeLayout(false);
            this.PnlSiFiMaskBriefing.PerformLayout();
            this.TapSiteMedicalService.ResumeLayout(false);
            this.PnlTabSiteMedical.ResumeLayout(false);
            this.PnlTabSiteMedical.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgrSiMedPrecautionaryMedical)).EndInit();
            this.TapSafetyAtWork.ResumeLayout(false);
            this.PnlTabSafetyWork.ResumeLayout(false);
            this.PnlSaAtWoSiteSecurityBriefing.ResumeLayout(false);
            this.PnlSaAtWoSiteSecurityBriefing.PerformLayout();
            this.PnlSaAtWoPalletLifterBriefing.ResumeLayout(false);
            this.PnlSaAtWoCranesBriefing.ResumeLayout(false);
            this.PnlSaAtWoIndustrialSafetyBriefing.ResumeLayout(false);
            this.TapPlant.ResumeLayout(false);
            this.PnlTabPlant.ResumeLayout(false);
            this.PnlPlIndustrialSafetyBriefingPlant.ResumeLayout(false);
            this.PnlPlIndustrialSafetyBriefingPlant.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgrPlPlant)).EndInit();
            this.TapTechnicalDepartment.ResumeLayout(false);
            this.PnlTabTechnical.ResumeLayout(false);
            this.PnlTecBriefingSite.ResumeLayout(false);
            this.PnlTecBriefingSite.PerformLayout();
            this.TapSiteSecurity.ResumeLayout(false);
            this.PnlTabSiteSecure.ResumeLayout(false);
            this.PnlSiSePExternal.ResumeLayout(false);
            this.PnlSiSePExternal.PerformLayout();
            this.PnlSiSeVehicleNumber.ResumeLayout(false);
            this.PnlSiSeVehicleNumber.PerformLayout();
            this.PnlSiSeIdentityCard.ResumeLayout(false);
            this.PnlSiSeIdentityCard.PerformLayout();
            this.PnlSiSeVehicleEntranceLong.ResumeLayout(false);
            this.PnlSiSeVehicleEntranceLong.PerformLayout();
            this.PnlSiSeVehicleEntranceLongControl.ResumeLayout(false);
            this.PnlSiSeVehicleEntranceShort.ResumeLayout(false);
            this.PnlSiSeVehicleEntranceShort.PerformLayout();
            this.PnlSiSeVehicleEntranceShortControl.ResumeLayout(false);
            this.PnlSiSeAccess.ResumeLayout(false);
            this.PnlSiSeAccess.PerformLayout();
            this.PnlSiSeAccessYNRbt.ResumeLayout(false);
            this.PnlSiSeSiteSecurity.ResumeLayout(false);
            this.PnlSiSeSiteSecurity.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        #region Accessors

        /// <summary>
        /// Gets or sets whether current coordinator is allowed to edit
        /// </summary>
        internal bool CoordinatorEditLocked
        {
            get
            {
                return mCoordinatorEditLocked;
            }
            set
            {
                mCoordinatorEditLocked = value;
            }
        }

        /// <summary>
        /// Gets or sets whether field id card number was manually changed (not via ZKS)
        /// </summary>
        internal bool IdCardNumberChanged
        {
            get
            {
                return mIdCardFieldEntered && mIdCardEdited;
            }
            set
            {
                mIdCardFieldEntered = value;
                mIdCardEdited = value;
            }
        }

        #endregion

        #region MethodsGeneral

        /// <summary>
        /// Standard Preshow.
        /// 17.03.04: added flag for radio button VehShortReceivedYes
        /// </summary>
        internal override void PreShow()
        {
            mBirthDateOK = true;
            mCheckOffDateOK = true;
            if (!GetMyController().GetLockState())
            {
                SetAuthorization();
            }
            mAutomaticTabOrder = true;
            mEnteredLikCoPlant = false;
            mEnteredRbtSiSeVehShortReceivedYes = false;
            if (this.BtnReClear.Enabled)
            {
                if (GetMyController().DialogStatus.Equals(
                    AllFPASSDialogs.DIALOG_STATUS_UPDATE))
                {
                    this.BtnReClear.Enabled = false;
                }
            }
            mEnteredRbtSiSeVehShortReceivedYes = true;
            mCoordinatorEditLocked = false;

            // Add event handler after data is loaded, otherwise it fires too early
            this.RbtCoIdPhotoSmActYes.CheckedChanged += new System.EventHandler(this.RbtCoIdPhotoSmActYes_CheckedChanged);
        }

        /// <summary>
        /// Clears/resets and diables fields.
        /// Fired before form is closed
        /// </summary>
        internal override void PreClose()
        {
            // Remove event handler after data is saved (CopyOut happens before this) , otherwise it fires too early
            this.RbtCoIdPhotoSmActYes.CheckedChanged -= new System.EventHandler(this.RbtCoIdPhotoSmActYes_CheckedChanged);

            mEnteredRbtSiSeVehShortReceivedYes = false;
            mBirthDateOK = true;
            mCheckOffDateOK = true;
            mCboContractorIsLeading = false;
            mCboCoordinatorIsLeading = false;
            ClearFieldsCoordinator();
            ClearFieldsMedicalService();
            ClearFieldsPlant();
            ClearFieldsReception();
            ClearFieldsSafetyAtWork();
            ClearFieldsSiteFireService();
            ClearFieldsSiteSecurity();
            ClearFieldsTecDepartment();
            mAutomaticTabOrder = true;
            EnableInput(false);
            BtnCoSearchExternalContractor.Enabled = true;
            BtnSave.Enabled = true;
            BtnPass.Enabled = true;
            BtnReClear.Enabled = true;
            BtnDelPassNumber.Enabled = true;
            BtnRePassNrHitag.Enabled = true;
            RbtReAccessAuthYes.Enabled = true;
            BtnReClear.Enabled = true;
            
            base.MnuAdministration.Enabled = true;
            base.MnuFunction.Enabled = true;

            mCoordinatorEditLocked = false;
            GetMyController().SetLockState(false);
            IdCardNumberChanged = false;
        }


        /// <summary>
        /// Returns whether or not current user has this authorization
        /// </summary>
        /// <returns></returns>
        internal bool ReceptionAuthorization()
        {
            mReceptionAuthorization = UserManagementControl.getInstance().GetAuthorization(UserManagementControl.COWORKER_RECEPTION_DIALOG);
            return mReceptionAuthorization;
        }

        /// <summary>
        /// Returns whether or not current user has this authorization
        /// </summary>
        /// <returns></returns>
        internal bool CoordinatorAuthorization()
        {
            mCoordinatorAuthorization = UserManagementControl.getInstance().GetAuthorization(UserManagementControl.COWORKER_COORDINATOR_DIALOG);
            return mCoordinatorAuthorization;
        }

        /// <summary>
        /// Returns whether or not current user has this authorization
        /// </summary>
        /// <returns></returns>
        internal bool PlantAuthorization()
        {
            mPlantAuthorization = UserManagementControl.getInstance().GetAuthorization(UserManagementControl.COWORKER_PLANT_DIALOG);
            return mPlantAuthorization;
        }

        /// <summary>
        /// Returns whether or not current user has this authorization
        /// </summary>
        /// <returns></returns>
        internal bool SafetyAtWorkAuthorization()
        {
            mSafetyAtWorkAuthorization = UserManagementControl.getInstance().GetAuthorization(UserManagementControl.COWORKER_SAFETY_AT_WORK_DIALOG);
            return mSafetyAtWorkAuthorization;
        }

        /// <summary>
        /// Returns whether or not current user has this authorization
        /// </summary>
        /// <returns></returns>
        internal bool SiteFireAuthorization()
        {
            mSiteFireAuthorization = UserManagementControl.getInstance().GetAuthorization(UserManagementControl.COWORKER_SITE_FIRE_SERVICE_DIALOG);
            return mSiteFireAuthorization;
        }

        /// <summary>
        /// Returns whether or not current user has this authorization
        /// </summary>
        /// <returns></returns>
        internal bool MedicalServiceAuthorization()
        {
            mMedicalServiceAuthorization = UserManagementControl.getInstance().GetAuthorization(UserManagementControl.COWORKER_MEDICAL_SERVICE_DIALOG);
            return mMedicalServiceAuthorization;
        }

        /// <summary>
        /// Returns whether or not current user has this authorization
        /// </summary>
        /// <returns></returns>
        internal bool TechDepartmentAuthorization()
        {
            mTechDepartmentAuthorization = UserManagementControl.getInstance().GetAuthorization(UserManagementControl.COWORKER_TEC_DEPARTMENT_DIALOG);
            return mTechDepartmentAuthorization;
        }

        /// <summary>
        /// Returns whether or not current user has this authorization
        /// </summary>
        /// <returns></returns>
        internal bool SiteSecurityAuthorization()
        {
            mSiteSecurityAuthorization = UserManagementControl.getInstance().GetAuthorization(UserManagementControl.COWORKER_SITE_SECURITY_DIALOG);
            return mSiteSecurityAuthorization;
        }

        /// <summary>
        /// Returns whether or not current user has this authorization
        /// </summary>
        /// <returns></returns>
        internal bool SiteSecurityLeaderAuthorization()
        {
            mSiteSecurityLeaderAuthorization = UserManagementControl.getInstance().GetAuthorization(UserManagementControl.COWORKER_SITE_SECURITY_LEADER_DIALOG);
            return mSiteSecurityLeaderAuthorization;
        }

        /// <summary>
        /// Returns whether or not current user has this authorization
        /// </summary>
        /// <returns></returns>
        internal bool EdvAdminAuthorization()
        {
            mEdvAdminAuthorization = UserManagementControl.getInstance().GetAuthorization(UserManagementControl.COWORKER_REGISTER_DIALOG);
            return mEdvAdminAuthorization;
        }

        /// <summary>
        /// Returns whether or not current user has this authorization
        /// </summary>
        /// <returns></returns>
        internal bool SystemAdminAuthorization()
        {
            mSystemAdminAuthorization = UserManagementControl.getInstance().GetAuthorization(UserManagementControl.COWORKER_REGISTER_DIALOG);
            return mSystemAdminAuthorization;
        }

        /// <summary>
        /// Returns whether or not current user has the authorization to manually edit the ID card fields
        /// </summary>
        /// <returns></returns>
        internal bool IdCardNumberEditAuthorization()
        {
            mIdCardNumberEditAuthorization = UserManagementControl.getInstance().GetAuthorization(UserManagementControl.COWORKER_FIELD_IDCARDNO);
            return mIdCardNumberEditAuthorization;
        }

        /// <summary>
        /// Returns whether or not current user has the authorization to manually edit the ID card fields
        /// </summary>
        /// <returns></returns>
        internal bool ApprenticeEditAuthorization()
        {
            mApprentEditAuthorization = UserManagementControl.getInstance().GetAuthorization(UserManagementControl.COWORKER_FIELD_APPRENTICE);
            return mApprentEditAuthorization;
        }


        /// <summary>
        /// This methode overrides the methode from BaseView to set the selected CardReaderNo to the text box.
        /// And also writes number into the UserContext
        /// </summary>
        /// <param name="pIDCardReaderNo"></param>
        internal override void ReFillIDCardReader(int pIDCardReaderNo, string pIDCardReaderType)
        {
            if (pIDCardReaderNo > 0)
            {
                if (pIDCardReaderType.ToUpper().Equals(IDCardTypes.Hitag2.ToUpper()))
                {
                    TxtReIDReaderHitag2.Text = pIDCardReaderNo.ToString();
                    UserManagementControl.getInstance().IDCardReaderHitag = pIDCardReaderNo;
                }
                else
                {
                    TxtReIDReaderMifare.Text = pIDCardReaderNo.ToString();
                    UserManagementControl.getInstance().IDCardReaderMifare = pIDCardReaderNo;
                }      
            }
        }


        /// <summary>
        /// Re-fills external contractor list.
        /// Called from the FrmSearchExternalContractor
        /// Either combobox on Recpetion or on Coordinator tab is filled
        /// </summary>
        /// <param name="pContractorID"></param>
        internal override void ReFillContractorList(String pContractorID)
        {
            // This is called from the FrmSearchExternalContractor
            // Must decide if combobox on Recpetion or Coordinator tab is to be filled
            if (this.mCalledSearchContractor == 1)
            {
                // Reception tab
                FillReCoordinator("0");
                FillReExternalContractor("0");
                CobReExternalContractor.SelectedValue = Convert.ToDecimal(pContractorID);
            }
            else
            {
                // Combobox Subfirma on Coordinator tab
                FillCoSubcontractor();
                CboCoSubcontractor.SelectedValue = Convert.ToDecimal(pContractorID);
            }
        }


        /// <summary>
        /// Initializes input fields: some header fields are databound to fields in the tabs
        /// </summary>
        private void InitView()
        {
            MnuFunction.Enabled = false;
            MnuZKS.Enabled = false;
            MnuReports.Enabled = false;

            TxtSurname.DataBindings.Add("Text", TxtReSurname, "Text");
            TxtFirstname.DataBindings.Add("Text", TxtReFirstname, "Text");
            TxtSupervisor.DataBindings.Add("Text", TxtCoSupervisor, "Text");

            TxtSiSeVehicleRegistrationNumber.DataBindings.Add("Text", TxtReVehicleRegistrationNumber, "Text");
            TxtSiSeVehicleRegistrationNumberTwo.DataBindings.Add("Text", TxtReVehicleRegistrationNumberTwo, "Text");
            TxtSiSeVehicleRegistrationNumberThree.DataBindings.Add("Text", TxtReVehicleRegistrationNumberThree, "Text");
            TxtSiSeVehicleRegistrationNumberFour.DataBindings.Add("Text", TxtReVehicleRegistrationNumberFour, "Text");

            // Hides Hitag ID card fields on Reception tab (Empfang) if current mandant does not have ZKS or Id card type not active
            if (!UserManagementControl.getInstance().CurrentBOUserMndt.HasZKS || !Globals.GetInstance().HitagActive)
            {
                BtnRePassNrHitag.Visible = false;
                BtnRePassNrHitagUSB.Visible = false;
                BtnReHitag2Reader.Visible = false;

                TxtReIDCardNumHitag2.Enabled = false;
                TxtReIDReaderHitag2.Enabled = false;
            }

            // Hides Mifare ID card fields on Reception tab (Empfang) if current mandant does not have ZKS or Id card type not active
            if (!Globals.GetInstance().MifareActive)
            {
                BtnRePassNrMifare.Visible = false;
                BtnRePassNrMifareUSB.Visible = false;
                BtnReMifareReader.Visible = false;

                TxtReIDCardNumMifareNo.Enabled = false;
                TxtReIDReaderMifare.Enabled = false;
            }

            // Hide Hitag photo id card fields on Site Security tab (Werkschutz)
            if (!Globals.GetInstance().HitagActive)
            {
                CbxSiSeIdPhotoHitagRec.Visible = false;
                DatSiSeIdPhotoHitagRec.Visible = false;
                LblSiSeIdPhotoHitagRecHint.Visible = false;
            }
        }

        /// <summary>
        /// Returns typed controller instance
        /// </summary>
        /// <returns></returns>
        private CoWorkerController GetMyController()
        {
            return ((CoWorkerController)mController);
        }


        /// <summary>
        /// Fills general comboboxes in Coworker view
        /// (dependent cbxs for Excontractor are filled elsewhere)
        /// </summary>
        internal override void FillLists()
        {
            //Coordinator
            FillCoCraftname();
            FillCoCraftnumber();
            FillCoDepartment();
            FillCoPlant();
            //Medical service
            FillSiMedPrecautionaryMedical();
        }

        /// <summary>
        /// Fills daropdownlists when form is opened in archive mode
        /// </summary>
        internal void FillArchiveLists()
        {
            if (GetMyController().Mode.Equals(AllFPASSDialogs.CWR_DIALOG_MODE_ARCHIVE))
            {
                FillCoordinatorArchive("0");
                FillExternalContractorArchive("0");
                FillSubcontractorArchive("0");
            }
        }

        /// <summary>
        /// Resets values of DateTimePickers to current date
        /// </summary>
        public void DateTimePicker()
        {
            DateTime now = DateTime.Now;
            DatDeliveryDate.Value = now;
            DatCoIndustrialSafetyBriefingSiteOn.Value = now;
            DatPlBriefingDoneOn.Value = now;
            DatPlBriefingValidUntil.Value = now;
            DatReAccessAuthorizationOn.Value = now;
            DatReSafetyInstructionsOn.Value = now;
            //DatReSignatureOn.Value = now;
            DatReAccessApprent.Value = now;
            DatSaAtWoCranesBriefingDoneOn.Value = now;
            DatSaAtWoPalletLifterBriefingDoneOn.Value = now;
            DatSaAtWoSafetyAtWorkBriefingDone.Value = now;
            DatSiFiRespMaskBriefDirOn.Value = now;
            DatSiFiRespMaskBriefRecOn.Value = now;
            DatSiFiMaskBackOnFlo.Value = now;
            DatSiFiMaskLentOnFlo.Value = now;
            DatSiFiSiteSecurityBriefingDoneOnG26_2.Value = now;
            DatSiFiSiteSecurityBriefingDoneOnG26_3.Value = now;
            DatSiMedValidUntil.Value = now;
            DatSiSeIdPhotoSmActRec.Value = now;
            DatSiSeSiteSecBriRec.Value = now;
            DatSiSeVehicleEntranceLongReceivedOn.Value = now;
            DatSiSeVehicleEntranceShortReceivedOn.Value = now;
            DatSiSePExternalOn.Value = now;
            DatTecBriefingDoneOn.Value = now;
        }

        /// <summary>
        /// Enables or disables input on form controls
        /// </summary>
        /// <param name="pState">Enable or disable</param>
        internal void EnableInput(bool pState)
        {
            BtnCoSearchExternalContractor.Enabled = pState;
            BtnSave.Enabled = pState;
            BtnPass.Enabled = pState;
            BtnReClear.Enabled = pState;
            DatDeliveryDate.Enabled = pState;
            DatPassValidFrom.Enabled = pState;
            PnlTabCoordinator.Enabled = pState;
            PnlTabPlant.Enabled = pState;
            PnlTabReception.Enabled = pState;
            PnlTabSafetyWork.Enabled = pState;
            PnlTabSiteFireService.Enabled = pState;
            PnlTabSiteMedical.Enabled = pState;
            PnlTabSiteSecure.Enabled = pState;
            PnlTabTechnical.Enabled = pState;
            MnuAdministration.Enabled = pState;
            MnuFunction.Enabled = pState;
            BtnDelPassNumber.Enabled = pState;
            BtnRePassNrHitag.Enabled = pState;
            BtnRePassNrHitagUSB.Enabled = pState;
            BtnRePassNrMifare.Enabled = pState;
            BtnRePassNrMifareUSB.Enabled = pState;
        }
        

        /// <summary>
        /// Enables or disables Id card fields depending on user's access rights and DB parameter.
        /// Parameter stored in DB and determines whether or not the controls are active.
        /// </summary>
        /// <param name="pIdCardControlsAuth">Does user have access rights</param>
        internal void EnableIdCardFields(bool pIdCardControlsAuth)
        {
            if (pIdCardControlsAuth)
            {
                // Role AUSWEIS is allowed to edit this field
                TxtReIDCardNumHitag2.ReadOnly = !mIdCardNumberEditAuthorization;
                TxtReIDCardNumMifareNo.ReadOnly = !mIdCardNumberEditAuthorization;
            }
            PnlReIdentityCard.Enabled = pIdCardControlsAuth;
        }


        /// <summary>
        /// Enables or disables button "Export CWR to SmartAct"
        /// depending on user's rights and whether or not CWR has a photocard Id
        /// </summary>
        /// <param name="pIdCardControlsAuth">Does user have access rights</param>
        internal void EnableBtnSmartActExport(bool pEnable)
        {
            // Special case: button "Export CWR to SmartAct" on Coordinator tab
            if (RbtCoIdPhotoSmActNo.Checked) 
                BtnCoSmartActExp.Enabled = false; 
            else if (Globals.GetInstance().IndSafety4ExpSmartAct && !CbxCoIndSafetyBrfRecvd.Checked)
                BtnCoSmartActExp.Enabled = false;
            else
                BtnCoSmartActExp.Enabled = pEnable;
           
        }

        /// <summary>
        /// Enables & disables GUI elements according to user authorization
        /// </summary>
        internal void SetAuthorization()
        {
            MnuCoWorker.Enabled = false;
            MnuUserAdministration.Enabled = false;
            MnuHistory.Enabled = false;
          
            // Admin/EDV-Verwaltung can edit all, excluding extra roles ID card and apprentice fields
            if (mSystemAdminAuthorization || mEdvAdminAuthorization)
            {
                PnlTabCoordinator.Enabled = true;
                PnlTabPlant.Enabled = true;
                PnlTabReception.Enabled = true;
                PnlReBasics.Enabled = true;
                PnlReBriefing.Enabled = true;
                PnlTabSafetyWork.Enabled = true;
                PnlTabSiteFireService.Enabled = true;
                PnlTabSiteMedical.Enabled = true;
                PnlTabSiteSecure.Enabled = true;
                PnlTabTechnical.Enabled = true;
                DatPassValidFrom.Enabled = true;
                DatDeliveryDate.Enabled = true;
                PnlReIdentityCard.Enabled = true;
                MnuFunction.Enabled = true;
                     
                // Activate ID card fields
                EnableIdCardFields(true);

                EnableBtnSmartActExport(true);

                // Apprentice access
                PnlReAccessApprent.Enabled = mApprentEditAuthorization;
                DatReAccessApprent.Enabled = mApprentEditAuthorization;
                return;
            }


            // Coordinator can only edit his own coworkers or those in his excos
            bool isOwnCoordinator = mCoordinatorAuthorization && !mCoordinatorEditLocked;

            // Flags member of site medical service
            bool isDoc = UserManagementControl.getInstance().CurrentUserIsInRole(UserManagementControl.ROLE_WERKSARZT);

            // Is current user allowed to edit?
            // Coordinator also has mMedicalServiceAuthorization but he's not allowed to edit coworkers who are not his
            bool canEdit =
                (isDoc && mMedicalServiceAuthorization)
                || (isOwnCoordinator && mMedicalServiceAuthorization)
                || mReceptionAuthorization || mSafetyAtWorkAuthorization || mSiteSecurityLeaderAuthorization || mSiteFireAuthorization || mTechDepartmentAuthorization || mPlantAuthorization || mSiteSecurityAuthorization;

            BtnSave.Enabled = canEdit;
            BtnPass.Enabled = canEdit;
            GetMyController().SetLockState(!canEdit);

            DatPassValidFrom.Enabled = mReceptionAuthorization || isOwnCoordinator;
            DatDeliveryDate.Enabled = mReceptionAuthorization || isOwnCoordinator;

            // System Menus
            MnuFunction.Enabled = mReceptionAuthorization || isOwnCoordinator;
            MnuAdministration.Enabled = mReceptionAuthorization;

            // Tab Reception
            // ---------------
            //  Coordinator can edit his own coworkers, including access Y/N
            PnlTabReception.Enabled = mIdCardNumberEditAuthorization || mReceptionAuthorization || isOwnCoordinator || mSiteSecurityAuthorization || mSiteSecurityLeaderAuthorization;
            PnlReBasics.Enabled = mReceptionAuthorization || isOwnCoordinator;
            
           // PnlReBriefing.Enabled = mReceptionAuthorization;
            PnlReSafetyInstructions.Enabled = mReceptionAuthorization;
            PnlReAccessAuthorization.Enabled = mReceptionAuthorization || isOwnCoordinator;

            // controls for identity card. Coordinator NOT allowed to edit Id cards
            bool idCardControlAuth = mIdCardNumberEditAuthorization || mReceptionAuthorization || mSiteSecurityAuthorization || mSiteSecurityLeaderAuthorization;
            EnableIdCardFields(idCardControlAuth);

            // Special case: button "Export CWR to SmartAct" on Coordinator tab
            EnableBtnSmartActExport(isOwnCoordinator || mReceptionAuthorization || mSiteSecurityAuthorization || mSiteSecurityLeaderAuthorization);

            // Apprentice access
            PnlReAccessApprent.Enabled = mApprentEditAuthorization;

            // Coordinator tab
            // ----------------
            PnlTabCoordinator.Enabled = isOwnCoordinator;

            // Werkschutz: Tab SiteSecurity 
            // ----------------
            PnlTabSiteSecure.Enabled = mReceptionAuthorization || mSiteSecurityAuthorization || mSiteSecurityLeaderAuthorization;
            PnlSiSeAccess.Enabled = mSiteSecurityAuthorization || mSiteSecurityLeaderAuthorization;
            PnlSiSeIdentityCard.Enabled = mSiteSecurityAuthorization || mSiteSecurityLeaderAuthorization;
            PnlSiSeVehicleNumber.Enabled = mSiteSecurityAuthorization || mSiteSecurityLeaderAuthorization;
            PnlSiSePExternal.Enabled = mReceptionAuthorization || mSiteSecurityAuthorization || mSiteSecurityLeaderAuthorization;

            // Vehicle access: normal SiteSecurity not allowed to grant vehicle access
            PnlSiSeVehicleEntranceShort.Enabled = mReceptionAuthorization || mSiteSecurityLeaderAuthorization;
            PnlSiSeVehicleEntranceLong.Enabled = mSiteSecurityLeaderAuthorization;

            // Tab Plant: enabled for Plant 
            // ----------------
            // all coordinators are allowed to scroll thru plants datagrid but not to make changes
            PnlTabPlant.Enabled = mCoordinatorAuthorization || mPlantAuthorization;

            // Tab Arbeitssicherheit
            // ----------------
            PnlTabSafetyWork.Enabled = mSafetyAtWorkAuthorization;

            // Tab Werksärztlicher Dienst
            // ----------------
            // Coordinator is allowed to edit prec meds if he is coordinator for current coworker.
            // Members of Werksärztlicher Dienst are allowed to edit prec medicals for all coworkers.
            PnlTabSiteMedical.Enabled = (mMedicalServiceAuthorization && isOwnCoordinator) || (mMedicalServiceAuthorization && isDoc);

            // Tab Brandschutz/Werksfeuerwehr
            // ----------------
            PnlTabSiteFireService.Enabled = mSiteFireAuthorization;

            // Technische Abteilung
            // ----------------
            PnlTabTechnical.Enabled = mTechDepartmentAuthorization;
        }

        #endregion Methods

        #region MethodsCoordinator

        //fills combobox subcontractor on coordinator
        public void FillCoSubcontractor()
        {
            ArrayList coSubContractor = new ArrayList();
            coSubContractor.Add(new LovItem("0", ""));
            coSubContractor.AddRange(LovSingleton.GetInstance().GetRootList(null, "FPASS_EXCONTRACTOR", "EXCO_NAME"));
            this.CboCoSubcontractor.DataSource = coSubContractor;
            this.CboCoSubcontractor.DisplayMember = "ItemValue";
            this.CboCoSubcontractor.ValueMember = "DecId";
        }

        //fills combobox department on coordinator
        public void FillCoDepartment()
        {
            ArrayList department = new ArrayList();
            department.Add(new LovItem("0", ""));
            department.AddRange(LovSingleton.GetInstance().GetRootList(null, "FPASS_DEPARTMENT", "DEPT_DEPARTMENT"));
            this.CboCoDepartment.DataSource = department;
            this.CboCoDepartment.DisplayMember = "ItemValue";
            this.CboCoDepartment.ValueMember = "DecId";
        }

        //fills combobox craftNumber on coordinator
        public void FillCoCraftnumber()
        {
            ArrayList craftNumber = new ArrayList();
            craftNumber = FPASSLovsSingleton.GetInstance().CraftNumbers;
            this.CboCoCraftNumber.DataSource = craftNumber;
            this.CboCoCraftNumber.DisplayMember = "CraftNumber";
            this.CboCoCraftNumber.ValueMember = "CraftID";
        }

        /// <summary>
        /// Fills combobox "GewerkName" on coordinator
        /// </summary>
        public void FillCoCraftname()
        {
            ArrayList craftName = new ArrayList();
            craftName = FPASSLovsSingleton.GetInstance().CraftNotations;
            this.CboCoCraftName.DataSource = craftName;
            this.CboCoCraftName.DisplayMember = "CraftNotation";
            this.CboCoCraftName.ValueMember = "CraftID";
        }

        /// <summary>
        /// Fills listbox with plants on coordinator tab
        /// </summary>
        public void FillCoPlant()
        {
            LiKCoPlant.Items.Clear();
            LiKCoPlant.Items.AddRange(FPASSLovsSingleton.GetInstance().GetPlants());
        }

        /// <summary>
        /// Clears all fields on tab "Coordinator"
        /// </summary>
        public void ClearFieldsCoordinator()
        {
            DateTimePicker();

            this.FillCoCraftname();
            this.FillCoCraftnumber();
            this.FillCoDepartment();
            this.FillCoPlant();

            this.RbtCoBreathingApparatusNoG26_2.Checked = true;
            this.RbtCoBreathingApparatusYesG26_2.Checked = false;
            this.RbtCoBreathingApparatusNoG26_3.Checked = true;
            this.RbtCoBreathingApparatusYesG26_3.Checked = false;
            this.RbtCoCranesNo.Checked = true;
            this.RbtCoCranesYes.Checked = false;
            this.RbtCoDepartmentalBriefingNo.Checked = true;
            this.RbtCoDepartmentalBriefingYes.Checked = false;
            this.RbtCoIdPhotoSmActNo.Checked = true;
            this.RbtCoIdPhotoSmActYes.Checked = false;
            this.RbtCoOrderDoneNo.Checked = true;
            this.RbtCoOrderDoneYes.Checked = false;
            this.RbtCoPalletLifterNo.Checked = true;
            this.RbtCoPalletLifterYes.Checked = false;
            this.RbtCoPrecautionaryMedicalNo.Checked = true;
            this.RbtCoPrecautionaryMedicalYes.Checked = false;
            this.RbtCoRaisablePlattformNo.Checked = true;
            this.RbtCoRaisablePlattformYes.Checked = false;
            this.RbtCoRespiratoryMaskBriefingNo.Checked = true;
            this.RbtCoRespiratoryMaskBriefingYes.Checked = false;
            this.RbtCoSafetyPassNo.Checked = true;
            this.RbtCoSafetyPassYes.Checked = false;
            this.RbtCoSiteSecurityBriefingNo.Checked = true;
            this.RbtCoSiteSecurityBriefingYes.Checked = false;
            this.RbtCoVehicleEntranceLongNo.Checked = true;
            this.RbtCoVehicleEntranceLongYes.Checked = false;
            this.RbtCoVehicleEntranceShortNo.Checked = true;
            this.RbtCoVehicleEntranceShortYes.Checked = false;

            this.TxtCoIndustrialSafetyBriefingSiteBy.Text = "";
            this.TxtCoOrderNumber.Text = "";
            this.TxtCoSupervisor.Text = "";
            this.TxtCoTelephoneNumber.Text = "";

            int count = this.LiKCoPlant.Items.Count;
            for (int i = 0; i < count; i++)
            {
                LiKCoPlant.SetItemChecked(i, false);
            }
        }

        /// <summary>
        /// When checkbox for PKI chip changed or button for ADS search is pressed.
        /// Enables/disables and fills fields accordingly       
        /// </summary>
        private void InitFieldsPKI(bool pDoSearchADS)
        {
            if (pDoSearchADS)
            {
                // Gets name from field "Windows Id" or "Surname"
                string adsName = (TxtCoWindowsID.Text.Length == 0 ? TxtReSurname.Text : TxtCoWindowsID.Text);

                // Shows dialogue for ADS search. Gets Windows Id, lastname etc from ADS and fills fields.
                FrmSearchLDAPWin searchDialog = new FrmSearchLDAPWin();

                searchDialog.SetSearchUser(adsName);
                searchDialog.ShowDialog();

                if (searchDialog.SelectedUser != null)
                {
                    ActiveDirectoryObject ado = searchDialog.SelectedUser;

                    // Fills textboxes with values from AD object
                    // WindowsID (KonzernId)
                    TxtCoWindowsID.Text = ado.SamAccountName;

                    // other fields
                    TxtReFirstname.Text = ado.CommonName;
                    TxtReSurname.Text = ado.Surname;
                    TxtCoPhone.Text = ado.TelephoneNumber;                
                }
            }
            else
            {
                // Enable/disable fields according to attribute "with PKI"
                BtnCoADSSearch.Enabled = CbxCoPKI.Checked;
                TxtCoWindowsID.Enabled = CbxCoPKI.Checked;
                TxtReFirstname.Enabled = !CbxCoPKI.Checked;
                TxtReSurname.Enabled = !CbxCoPKI.Checked;

                // If user clears "with PKI chip" then clear value for Windows Id
                if (!CbxCoPKI.Checked)
                {
                    TxtCoWindowsID.Text = String.Empty;
                }
            }    
        }

        #endregion MethodsCoordinator

        #region MethodsPlant

        /// <summary>
        /// Raised when Plants datagrid navigated: gets current plant Id and delegated to controller
        /// </summary>
        internal void PlantsTableNavigated()
        {
            int rowIndex = DgrPlPlant.CurrentRowIndex;
            if (-1 < rowIndex)
            {
                GetMyController().HandleStartEditPlant(DgrPlPlant[rowIndex, 1].ToString());
            }
        }

        /// <summary>
        /// Clears fields on tab "Plant"
        /// </summary>
        public void ClearFieldsPlant()
        {
            DateTimePicker();

            RbtPlBriefing.Checked = false;
            TxtPlBriefingDoneBy.Text = String.Empty;
            TxtPlPlantname.Text = String.Empty;
            DgrPlPlant.DataSource = null;
            RbtPlBriefing.Enabled = false;
            CbxPlBriefingDone.Enabled = false;
            CbxPlBriefingDone.Checked = false;
            DatPlBriefingDoneOn.Enabled = false;
            DatPlBriefingValidUntil.Enabled = false;
        }

        #endregion MethodsPlant

        #region MethodsReception

        /// <summary>
        /// Fills combobox externalContractor, Reception tab.
        /// </summary>
        /// <param name="pID"></param>
        public void FillReExternalContractor(String pID)
        {
            // don't set datasource to null (this seemed to cause a problem with .net Framework SP1)
            /// this.CobReExternalContractor.DataSource = null;  /// out 16.02.2005

            CobReExternalContractor.DataSource = GetContractorList(pID);
            CobReExternalContractor.DisplayMember = "ContractorName";
            CobReExternalContractor.ValueMember = "ContractorID";
        }

        /// <summary>
        /// fills combobox coordinator on Reception tab
        /// </summary>
        /// <param name="pID"></param>
        public void FillReCoordinator(String pID)
        {
            // don't set datasource to null (this seemed to cause a problem with .net Framework SP1)
            ///this.CobReCoordinator.DataSource = null; /// out 16.02.2005

            CobReCoordinator.DataSource = GetCoordinatorList(pID);
            CobReCoordinator.DisplayMember = "CoordFullNameTel";
            CobReCoordinator.ValueMember = "CoordID";
        }

        /// <summary>
        /// Fills combobox external contractor: values from active FPASS and from archive
        /// </summary>
        /// <param name="pID"></param>
        public void FillExternalContractorArchive(String pID)
        {
            ArrayList allContractors = new ArrayList();
            allContractors.AddRange(this.GetContractorList(pID));
            allContractors.AddRange(FPASSLovsSingleton.GetInstance().ContractorsArchive);
            this.CobReExternalContractor.DataSource = allContractors;
            this.CobReExternalContractor.DisplayMember = "ContractorName";
            this.CobReExternalContractor.ValueMember = "ContractorID";
        }


        //fills combobox coordinator: values from production and from archive
        public void FillCoordinatorArchive(String pID)
        {
            this.CobReCoordinator.DataSource = null;
            this.CobReCoordinator.DataSource = FPASSLovsSingleton.GetInstance().AllCoordinators;
            this.CobReCoordinator.DisplayMember = "CoordFullNameTel";
            this.CobReCoordinator.ValueMember = "CoordID";
        }

        //fills combobox subcontractor: values from production and from archive
        public void FillSubcontractorArchive(String pSubContractorID)
        {
            ArrayList allSubContractors = new ArrayList();
            allSubContractors.AddRange(this.GetContractorList(pSubContractorID));
            allSubContractors.AddRange(FPASSLovsSingleton.GetInstance().ContractorsArchive);
            this.CboCoSubcontractor.DataSource = allSubContractors;
            this.CboCoSubcontractor.DisplayMember = "ContractorName";
            this.CboCoSubcontractor.ValueMember = "ContractorID";
        }

        /// <summary>
        /// Re-sets GUI fields in tab "reception" back to default values
        /// Last change 01.03.2004:
        /// Re-fill comboboxes "Excontractor" and "coordinator" with all values
        /// else assigned IDs are not found when coworkers loaded for edit (non-reproducible error!)
        /// </summary>
        public void ClearFieldsReception()
        {
            DateTimePicker();

            // New 01.03.2004: re-fill cbx with all available values
            this.FillReCoordinator("0");
            this.FillReExternalContractor("0");

            this.TxtReFirstname.Text = "";
            this.TxtReSurname.Text = "";
            this.TxtCoPhone.Text = "";
            this.TxtReDateOfBirth.Text = "";
            this.TxtRePlaceOfBirth.Text = "";
            this.TxtReAccessAuthorizationBy.Text = "";
            this.TxtReIDReaderHitag2.Text = "";
            this.TxtReSafetyInstructionsBy.Text = "";
            //this.TxtReSignatureBy.Text = "";
            this.TxtReVehicleRegistrationNumber.Text = "";
            this.TxtReVehicleRegistrationNumberTwo.Text = "";
            this.TxtReVehicleRegistrationNumberThree.Text = "";
            this.TxtReVehicleRegistrationNumberFour.Text = "";

            this.RbtReAccessAuthNo.Checked = true;
            this.RbtReAccessAuthYes.Checked = false;
            this.RbtReSafetyInstructionsNo.Checked = true;
            this.RbtReSafetyInstructionsYes.Checked = false;
            //this.RbtReSignatureNo.Checked = true;
           // this.RbtReSignatureYes.Checked = false;
        }


        #endregion

        #region MethodsSafetyAtWork



        public void ClearFieldsSafetyAtWork()
        {
            DateTimePicker();

            this.RbtSaAtWoBriefing.Checked = false;
            this.RbtSaAtWoCranesBriefing.Checked = false;
            this.RbtSaAtWoPalletLifterBriefing.Checked = false;

            this.TxtSaAtWoCranesBriefingDoneBy.Text = "";
            this.TxtSaAtWoPalletLifterBriefingDoneBy.Text = "";
            this.TxtSaAtWoSafetyAtWorkBriefingDoneBy.Text = "";
        }

        #endregion

        #region MethodsSiteFireService



        public void ClearFieldsSiteFireService()
        {
            DateTimePicker();

            this.RbtSiFiRespMaskBriefDir.Checked = false;
            this.RbtSiFiSiteSecurityBriefingG26_2.Checked = false;
            this.RbtSiFiSiteSecurityBriefingG26_3.Checked = false;

            this.TxtSiFiRespMaskBriefRecBy.Text = "";
            this.TxtSiFiMaskNrBackFlo.Text = "";
            this.TxtSiFiMaskBackByFlo.Text = "";
            this.TxtSiFiMaskNrLentFlo.Text = "";
            this.TxtSiFiMaskLentByFlo.Text = "";
            this.TxtSiFiMaskNrBackTec.Text = "";
            this.TxtSiFiMaskBackByTec.Text = "";
            this.TxtSiFiMaskNrLentTec.Text = "";
            this.TxtSiFiMaskLentByTec.Text = "";
            this.TxtSiFiSiteSecurityBriefingDoneByG26_2.Text = "";
            this.TxtSiFiSiteSecurityBriefingDoneByG26_3.Text = "";

            // Florix Mask Lent
            this.DatSiFiMaskLentOnFlo.Visible = false;
            this.TxtSiFiMaskLentByFlo.Visible = false;
            this.TxtSiFiMaskNrLentFlo.Visible = false;
            this.LblSiFiMaskLentFlo.Visible = false; 
            this.LblSiFiMaskNrLentFlo.Visible = false; 
            this.LblSiFiMaskLentOnFlo.Visible = false;
            this.LblSiFiMaskLentByFlo.Visible = false;

            // Florix Mask Return
            this.DatSiFiMaskBackOnFlo.Visible = false;
            this.TxtSiFiMaskBackByFlo.Visible = false;
            this.TxtSiFiMaskNrBackFlo.Visible = false;
            this.LblSiFiMaskBackFlo.Visible = false; 
            this.LblSiFiMaskNrBackFlo.Visible = false; 
            this.LblSiFiMaskBackOnFlo.Visible = false;
            this.LblSiFiMaskBackByFlo.Visible = false;

            // TecBos Mask Lent
            this.DatSiFiMaskLentOnTec.Visible = false;
            this.TxtSiFiMaskLentByTec.Visible = false;
            this.TxtSiFiMaskNrLentTec.Visible = false;
            this.LblSiFiMaskLentTec.Visible = false;
            this.LblSiFiMaskNrLentTec.Visible = false;
            this.LblSiFiMaskMaintDateTec.Visible = false; 
            this.LblSiFiMaskLentOnTec.Visible = false;
            this.LblSiFiMaskLentByTec.Visible = false;
            this.TxtSiFiMaskMaintDateTec.Visible = false;

            // TecBos Mask Return
            this.DatSiFiMaskBackOnTec.Visible = false;
            this.TxtSiFiMaskBackByTec.Visible = false;
            this.TxtSiFiMaskNrBackTec.Visible = false;
            this.LblSiFiMaskBackTec.Visible = false; 
            this.LblSiFiMaskNrBackTec.Visible = false; 
            this.LblSiFiMaskBackOnTec.Visible = false;
            this.LblSiFiMaskBackByTec.Visible = false;
        }

        #endregion // End of Methods

        #region MethodsMedicalService

        //fills combobox precautionaryMedical
        public void FillSiMedPrecautionaryMedical()
        {
            ArrayList precautionaryMedical = new ArrayList();
            precautionaryMedical.Add(new LovItem("0", ""));
            precautionaryMedical.AddRange(LovSingleton.GetInstance().
                GetRootList(null, "FPASS_PRECMEDTYPELOV", "PMTY_FULLNAME"));
            this.CboSiMedPrecautionaryMedical.DataSource = precautionaryMedical;
            this.CboSiMedPrecautionaryMedical.DisplayMember = "ItemValue";
            this.CboSiMedPrecautionaryMedical.ValueMember = "DecId";
        }



        public void ClearFieldsMedicalService()
        {
            DateTimePicker();

            this.FillSiMedPrecautionaryMedical();

            this.RbtSiMedPrecautionaryMedicalBriefing.Checked = false;

            this.CbxSiMedPrecautionaryMedical.Checked = false;

            this.TxtSiMedExecutedBy.Text = String.Empty;
        }


        /// <summary>
        /// Loads prec medical currently selected in datagrid ready for editing 
        /// </summary>
        private void MedicalTableNavigated()
        {
            int rowIndex = this.DgrSiMedPrecautionaryMedical.CurrentRowIndex;
            if (-1 < rowIndex)
            {
                GetMyController().HandlePrecMedUpdate(Convert.ToDecimal(
                    this.DgrSiMedPrecautionaryMedical[rowIndex, 0].ToString()));
            }
        }


        #endregion // End of Methods

        #region MethodsSiteSecurity


        /// <summary>
        /// Clear GUI fields on tab SiteSecurity
        /// The order in which vehicle entrance radio buttons are unchecked
        /// is important due to CheckChanged events firing
        /// </summary>
        public void ClearFieldsSiteSecurity()
        {
            DateTimePicker();

            RbtSiSeAccessAuthNo.Checked = true;
            RbtSiSeAccessAuthYes.Checked = false;
            RbtSiSeIdPhotoSmAct.Checked = false;
            RbtSiSeSiteSecBri.Checked = false;
            RbtSiSeVehicleEntranceLong.Checked = false;
            RbtSiSeVehicleEntranceLongNo.Checked = true;
            RbtSiSeVehicleEntranceLongYes.Checked = false;
            RbtSiSeVehicleEntranceShort.Checked = false;
            RbtSiSeVehicleEntranceShortReceivedNo.Checked = true;
            RbtSiSeVehicleEntranceShortReceivedYes.Checked = false;
            RbtSiSeVehicleEntranceLongNo.Checked = false;
            RbtSiSeVehicleEntranceShortReceivedNo.Checked = false;

            TxtSiSeAccessAuthorizationComment.Text = "";
            TxtSiSeIdPhotoSmActRecBy.Text = "";
            TxtSiSeSiteSecBriRecBy.Text = "";
            TxtSiSeVehicleEntranceLongReceivedBy.Text = "";
            TxtSiSeVehicleEntranceShortReceivedBy.Text = "";
            CbxSiSePExternal.Checked = false;
            TxtSiSePExternalBy.Text = "";
        }

        #endregion // End of Methods

        #region MethodsTecDepartment



        public void ClearFieldsTecDepartment()
        {
            DateTimePicker();

            this.RbtTecBriefing.Checked = false;

            this.TxtTecBriefingDoneBy.Text = "";
        }

        #endregion // End of Methods

        #region EventsGeneral

        /// <summary>
        /// Event fired when FrmCoworker activated (i.e. gets focus)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmCoWorker_Activated(object sender, System.EventArgs e)
        {
            if (mCalledSearchContractor != 1 && mCalledSearchIdReader !=1 && mAutomaticTabOrder)
            {
                mAutomaticTabOrder = false;

                if (UserManagementControl.getInstance().CurrentUserIsInRole(
                    UserManagementControl.ROLE_EMPFANG))
                {
                    this.TabSiteSecurity.SelectedTab = this.TapReception;
                    this.TxtReSurname.Focus();
                }
                if (UserManagementControl.getInstance().CurrentUserIsInRole(
                    UserManagementControl.ROLE_WERKSARZT))
                {
                    this.TabSiteSecurity.SelectedTab = this.TapSiteMedicalService;
                    this.CboSiMedPrecautionaryMedical.Focus();
                }
                if (UserManagementControl.getInstance().CurrentUserIsInRole(
                    UserManagementControl.ROLE_BETRIEBSMEISTER))
                {
                    this.TabSiteSecurity.SelectedTab = this.TapPlant;
                    this.CbxPlBriefingDone.Focus();
                }
                if (UserManagementControl.getInstance().CurrentUserIsInRole(
                    UserManagementControl.ROLE_ARBEITSSICHERHEIT))
                {
                    this.TabSiteSecurity.SelectedTab = this.TapSafetyAtWork;
                    this.CbxSaAtWoSafetyAtWorkBriefingDone.Focus();
                }
                if (UserManagementControl.getInstance().CurrentUserIsInRole(
                    UserManagementControl.ROLE_WERKFEUERWEHR))
                {
                    this.TabSiteSecurity.SelectedTab = this.TapSiteFireService;
                    this.CbxSiFiRespMaskBriefRec.Focus();
                }
                if (UserManagementControl.getInstance().CurrentUserIsInRole(
                    UserManagementControl.ROLE_WERKSCHUTZ))
                {
                    this.TabSiteSecurity.SelectedTab = this.TapSiteSecurity;
                    this.CbxSiSeSiteSecBriRec.Focus();
                }
                if (UserManagementControl.getInstance().CurrentUserIsInRole(
                    UserManagementControl.ROLE_WERKSCHUTZ_LEITER))
                {
                    this.TabSiteSecurity.SelectedTab = this.TapSiteSecurity;
                    this.CbxSiSeSiteSecBriRec.Focus();
                }
                if (UserManagementControl.getInstance().CurrentUserIsInRole(
                    UserManagementControl.ROLE_TECHNISCHE_ABTEILUNG))
                {
                    this.TabSiteSecurity.SelectedTab = this.TapTechnicalDepartment;
                    this.CbxTecRaisonalPlattform.Focus();
                }

                if (UserManagementControl.getInstance().CurrentUserIsInRole(
                    UserManagementControl.ROLE_VERWALTUNG))
                {
                    this.TabSiteSecurity.SelectedTab = this.TapCoordinator;
                    this.CboCoSubcontractor.Focus();
                }
                if (UserManagementControl.getInstance().CurrentUserIsInRole(
                    UserManagementControl.ROLE_EDVADMIN))
                {
                    this.TabSiteSecurity.SelectedTab = this.TapCoordinator;
                    this.CboCoSubcontractor.Focus();
                }
                if (UserManagementControl.getInstance().CurrentUserIsInRole(
                    UserManagementControl.ROLE_KOORDINATOR))
                {
                    this.TabSiteSecurity.SelectedTab = this.TapCoordinator;
                    this.CboCoSubcontractor.Focus();
                }
            }
            mCalledSearchIdReader = 0;
        }

        private void BtnSummary_Click(object sender, System.EventArgs e)
        {
            GetMyController().HandleLeaveCoWorker();
        }

        private void TabSiteSecurity_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (!mBirthDateOK)
            {
                this.TabSiteSecurity.SelectedTab = this.TapReception;
                this.TxtReDateOfBirth.Focus();
            }

            if (!mCheckOffDateOK)
            {
                this.TabSiteSecurity.SelectedTab = this.TapCoordinator;
                this.TxtCoCheckOff.Focus();
            }
        }

        /// <summary>
        /// Raised when user clicks button "Speichern". Whole form is saved, not per tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, System.EventArgs e)
        {
            GetMyController().HandleSaveCoWorker();
        }

        /// <summary>
        /// Raised when user clicks button "Passierschein". Generates pass doc for current coworker 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPass_Click(object sender, System.EventArgs e)
        {
            GetMyController().HandlePrintPass();
        }


        #endregion Events

        #region EventsCoordinator

        /// <summary>
        /// Raised when state of checkbox "PKI" is changed.
        /// Enables or disables other fields
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxCoPKI_CheckedChanged(object sender, EventArgs e)
        {
            // Only allowed to do this if CWR has not been imported from SmartAct
            if (TxtCoSmartActNo.Text.Length == 0)
            {                
                InitFieldsPKI(false);
            }
        }

        /// <summary>
        /// Raised when user clicks "Search ADS": "Suchen"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCoSearchADS_Click(object sender, EventArgs e)
        {
            InitFieldsPKI(true);
        }

        /// <summary>
        /// Opens coordinator history for current coworker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCoordHist_Click(object sender, System.EventArgs e)
        {
            GetMyController().HandlePopCoordHist();
        }

        /// <summary>
        /// Button to generate PDF for "Sicherheitsunterweisuing formular"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCoPdf_Click(object sender, System.EventArgs e)
        {
            GetMyController().HandleGenerateDocSafety();
        }

        private void BtnCoSearchExternalContractor_Click_1(object sender, System.EventArgs e)
        {
            mCalledSearchContractor = 2;
            GetMyController().HandleEventOpenSearchExternalContractorDialog();
        }


        private void RbtCoSafetyPassYes_CheckedChanged_1(object sender, System.EventArgs e)
        {
            this.RbtCoBreathingApparatusNoG26_2.Visible = true;
            this.RbtCoBreathingApparatusYesG26_2.Visible = true;
            this.RbtCoBreathingApparatusNoG26_3.Visible = true;
            this.RbtCoBreathingApparatusYesG26_3.Visible = true;
            this.RbtCoCranesNo.Visible = true;
            this.RbtCoCranesYes.Visible = true;
            this.RbtCoPalletLifterNo.Visible = true;
            this.RbtCoPalletLifterYes.Visible = true;
            this.RbtCoRaisablePlattformNo.Visible = true;
            this.RbtCoRaisablePlattformYes.Visible = true;
            this.RbtCoFiremanNo.Visible = true;
            this.RbtCoFiremanYes.Visible = true;
        }

        private void LiKCoPlant_Enter(object sender, System.EventArgs e)
        {
            mEnteredLikCoPlant = true;
        }


        private void LiKCoPlant_Leave(object sender, System.EventArgs e)
        {
            mEnteredLikCoPlant = false;
        }

        /// <summary>
        /// Leave event for field "Abmeldung am"
        /// Dates in the past are not allowed (this is different from other date fields)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtCoCheckOff_Leave(object sender, System.EventArgs e)
        {
            mCheckOffDateOK = true;
            bool onCoordinatorTab = TabSiteSecurity.SelectedTab.ToString().EndsWith("{Koordinator}");
            try
            {
                if (!StringValidation.GetInstance().IsDateString(TxtCoCheckOff.Text))
                {
                    if (TxtCoCheckOff.Text.Length > 0)
                    {
                        throw new UIWarningException(MessageSingleton.GetInstance().
                            GetMessage(MessageSingleton.INVALID_CHECKOFFDATE));
                    }
                }
                else
                {
                    DateTime checkOff = Convert.ToDateTime(TxtCoCheckOff.Text).Date;
                    int compare = checkOff.CompareTo(DateTime.Now.Date);
                    if (compare < 0)
                    {
                        throw new UIWarningException(MessageSingleton.GetInstance().
                            GetMessage(MessageSingleton.INVALID_CHECKOFFDATE));
                    }
                }
            }
            catch (UIWarningException uwe)
            {
                if (onCoordinatorTab)
                {
                    ExceptionProcessor.GetInstance().Process(uwe);
                    TxtCoCheckOff.Focus();
                }
                else
                {
                    mCheckOffDateOK = false;
                }
            }
        }

        private void RbtCoSafetyPassNo_CheckedChanged_1(object sender, System.EventArgs e)
        {
            RbtCoBreathingApparatusNoG26_2.Visible = false;
            RbtCoBreathingApparatusYesG26_2.Visible = false;
            RbtCoBreathingApparatusNoG26_3.Visible = false;
            RbtCoBreathingApparatusYesG26_3.Visible = false;
            RbtCoCranesNo.Visible = false;
            RbtCoCranesYes.Visible = false;
            RbtCoPalletLifterNo.Visible = false;
            RbtCoPalletLifterYes.Visible = false;
            RbtCoRaisablePlattformNo.Visible = false;
            RbtCoRaisablePlattformYes.Visible = false;
            RbtCoFiremanNo.Visible = false;
            RbtCoFiremanYes.Visible = false;

            RbtCoBreathingApparatusNoG26_2.Checked = true;
            RbtCoBreathingApparatusNoG26_3.Checked = true;
            RbtCoCranesNo.Checked = true;
            RbtCoPalletLifterNo.Checked = true;
            RbtCoRaisablePlattformNo.Checked = true;
            RbtCoFiremanNo.Checked = true;
        }

        /// <summary>
        /// Field Leave event 
        /// 27.04.04: Dates should be allowed in past but not in the future
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatCoIndustrialSafetyBriefingSiteOn_Leave_1(object sender, System.EventArgs e)
        {
            try
            {
                if (DatCoIndustrialSafetyBriefingSiteOn.Value.Date > mDateNow.Date)
                {
                    DatCoIndustrialSafetyBriefingSiteOn.Value = mDateNow;
                    throw new UIWarningException(MessageSingleton.GetInstance().GetMessage
                        (MessageSingleton.NO_FUTURE_VALID));
                }
            }
            catch (UIWarningException uwe)
            {
                ExceptionProcessor.GetInstance().Process(uwe);
            }
        }

        /// <summary>
        /// Which tab (reception or coordinator) called form?
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCoSearchExternalContractor_Click(object sender, System.EventArgs e)
        {
            mCalledSearchContractor = 2;
            GetMyController().HandleEventOpenSearchExternalContractorDialog();
        }

        private void RbtCoSiteSecurityBriefingYes_CheckedChanged(object sender, System.EventArgs e)
        {
            GetMyController().HandleRadioButtons();
        }

        private void RbtCoSiteSecurityBriefingNo_CheckedChanged(object sender, System.EventArgs e)
        {
            GetMyController().HandleRadioButtons();
        }

        /// <summary>
        /// Raised when Lichtbildausweis gewünscht set to Yes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void RbtCoIdPhotoSmActYes_CheckedChanged(object sender, System.EventArgs e)
        {
            GetMyController().HandleIdPhotoSmartActYes();          
        }

        /// <summary>
        /// Raised when Lichtbildausweis gewünscht set to No
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RbtCoIdPhotoSmActNo_CheckedChanged(object sender, System.EventArgs e)
        {
            GetMyController().HandleIdCardPhoto();
        }

        /// <summary>
        /// User clicks on button "Lichtbildausweis löschen"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCoSmartActDel_Click(object sender, EventArgs e)
        {
            GetMyController().HandleDeleteIdCardSmartAct();
        }      

        private void RbtCoBreathingApparatusYesG26_2_CheckedChanged(object sender, System.EventArgs e)
        {
            GetMyController().HandleRadioButtons();
        }

        private void RbtCoBreathingApparatusNoG26_2_CheckedChanged(object sender, System.EventArgs e)
        {
            GetMyController().HandleRadioButtons();
        }

        private void RbtCoBreathingApparatusYesG26_3_CheckedChanged(object sender, System.EventArgs e)
        {
            GetMyController().HandleRadioButtons();
        }

        private void RbtCoBreathingApparatusNoG26_3_CheckedChanged(object sender, System.EventArgs e)
        {
            GetMyController().HandleRadioButtons();
        }

        private void RbtCoPalletLifterYes_CheckedChanged(object sender, System.EventArgs e)
        {
            GetMyController().HandleRadioButtons();
        }

        private void RbtCoPalletLifterNo_CheckedChanged(object sender, System.EventArgs e)
        {
            GetMyController().HandleRadioButtons();
        }

        private void RbtCoRaisablePlattformYes_CheckedChanged(object sender, System.EventArgs e)
        {
            GetMyController().HandleRadioButtons();
        }

        private void RbtCoRaisablePlattformNo_CheckedChanged(object sender, System.EventArgs e)
        {
            GetMyController().HandleRadioButtons();
        }

        private void RbtCoCranesYes_CheckedChanged(object sender, System.EventArgs e)
        {
            GetMyController().HandleRadioButtons();
        }

        private void RbtCoCranesNo_CheckedChanged(object sender, System.EventArgs e)
        {
            GetMyController().HandleRadioButtons();
        }

        private void RbtCoDepartmentalBriefingYes_CheckedChanged(object sender, System.EventArgs e)
        {
            GetMyController().HandleRadioButtons();
        }

        private void RbtCoDepartmentalBriefingNo_CheckedChanged(object sender, System.EventArgs e)
        {
            GetMyController().HandleRadioButtons();
        }

        private void RbtCoRespiratoryMaskBriefingYes_CheckedChanged(object sender, System.EventArgs e)
        {
            GetMyController().HandleRadioButtons();
        }

        private void RbtCoRespiratoryMaskBriefingNo_CheckedChanged(object sender, System.EventArgs e)
        {
            GetMyController().HandleRadioButtons();
        }

        private void RbtCoPrecautionaryMedicalYes_CheckedChanged(object sender, System.EventArgs e)
        {
            GetMyController().HandleRadioButtons();
        }

        private void RbtCoPrecautionaryMedicalNo_CheckedChanged(object sender, System.EventArgs e)
        {
            GetMyController().HandleRadioButtons();
        }

        private void RbtCoVehicleEntranceShortYes_CheckedChanged(object sender, System.EventArgs e)
        {
            GetMyController().HandleRadioButtons();
        }

        private void RbtCoVehicleEntranceShortNo_CheckedChanged(object sender, System.EventArgs e)
        {
            GetMyController().HandleRadioButtons();
        }

        private void RbtCoVehicleEntranceLongYes_CheckedChanged(object sender, System.EventArgs e)
        {
            GetMyController().HandleRadioButtons();
        }

        private void RbtCoVehicleEntranceLongNo_CheckedChanged(object sender, System.EventArgs e)
        {
            GetMyController().HandleRadioButtons();
        }

        private void RbtCoFiremanYes_CheckedChanged(object sender, System.EventArgs e)
        {
            GetMyController().HandleRadioButtons();
        }

        private void RbtCoFiremanNo_CheckedChanged(object sender, System.EventArgs e)
        {
            GetMyController().HandleRadioButtons();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxCoPlantsAll_CheckStateChanged(object sender, EventArgs e)
        {
            //if (CbxCoPlantsAll.Checked)
            //{
            //    GetMyController().HandleEventCoPlantsAll();
            //}
        }

        /// <summary>
        /// Automtically selects all plants
        /// (button "alle Betriebe zuweisen")
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCoPlantsAll_Click(object sender, EventArgs e)
        {
            GetMyController().HandleCoPlantsAll();
        }

        /// <summary>
        /// Raised when selected value in listbox "Betriebe" changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LiKCoPlant_SelectedValueChanged(object sender, System.EventArgs e)
        {
            if (mEnteredLikCoPlant)
            {
                GetMyController().HandleLikCoPlantChanged();
            }
        }

        private void CboCoSubcontractor_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                this.TxtSubcontractor.Text = ((LovItem)CboCoSubcontractor.SelectedItem).ItemValue;
            }
            catch (InvalidCastException)
            {
                this.TxtSubcontractor.Text = ((ContractorLovItem)CboCoSubcontractor.SelectedItem).ContractorName;
            }
        }

        /// <summary>
        /// Fired when selected Craft Number changed (tab Koordinator)
        /// Last change 25.02.2004
        /// If user enters rubbish then CboCoCraftName.SelectedItem is null, need to test for this
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboCoCraftNumber_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            decimal craftIDNumber = ((CraftLovItem)CboCoCraftNumber.SelectedItem).CraftID;
            if (craftIDNumber != 0)
            {
                if (null == (CraftLovItem)CboCoCraftName.SelectedItem
                    || craftIDNumber != ((CraftLovItem)CboCoCraftName.SelectedItem).CraftID)
                {
                    CboCoCraftName.SelectedValue = craftIDNumber;
                }
            }
        }

        /// <summary>
        /// Fired when selected Craft Number changed (tab Koordinator)
        /// Last change 25.02.2004
        /// If user enters rubbish then CboCoCraftNumber.SelectedItem is null, need to test for this
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboCoCraftName_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            decimal craftIDName = ((CraftLovItem)CboCoCraftName.SelectedItem).CraftID;
            if (craftIDName != 0)
            {
                if (null == (CraftLovItem)CboCoCraftNumber.SelectedItem
                    || craftIDName != ((CraftLovItem)CboCoCraftNumber.SelectedItem).CraftID)
                {
                    CboCoCraftNumber.SelectedValue = craftIDName;
                }
            }
        }

        /// <summary>
        /// Raised when radiobutton "Auftrag erledigt" is set
        /// Empties field "Abmeldung an" (checkoff date) 
        /// if user selects order is not complete
        /// Uses eventhandler: cannot call <see cref="GetMyController().HandleRadiobuttons()"/> otherwise field
        /// is emptied when GUI is loaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RbtCoOrderDoneNo_CheckedChanged(object sender, System.EventArgs e)
        {
            if (RbtCoOrderDoneNo.Checked)
            {
                TxtCoCheckOff.Text = "";
            }
        }

        /// <summary>
        /// Raised when user clicks on button "Export to SmartAct" on Coordinator tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCoSmartActExp_Click(object sender, EventArgs e)
        {
            try
            {
                GetMyController().HandleSmartActExport();
            }
            catch (UIWarningException uwe)
            {
                ExceptionProcessor.GetInstance().Process(uwe);
            }
        }

        private void CbxCoIndSafetyBrfRecvd_CheckedChanged(object sender, EventArgs e)
        {
            GetMyController().HandleIdCardPhoto();
        }


        #endregion EventsCoordinator

        #region EventsPlant

        /// <summary>
        /// Raised when cursor leaves datefield "Belehrung Betrieb erteilt am". 
        /// Dates in the past are allowed but not in the future
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatPlBriefingDoneOn_Leave(object sender, System.EventArgs e)
        {
            try
            {
                if (DatPlBriefingDoneOn.Value.Date > mDateNow.Date)
                {
                    DatPlBriefingDoneOn.Value = mDateNow;
                    throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.NO_FUTURE_VALID));
                }
            }
            catch (UIWarningException uwe)
            {
                ExceptionProcessor.GetInstance().Process(uwe);
            }
        }

        /// <summary>
        /// Raised when cursor leaves datefield "Belehrung Betrieb gültig bis". 
        /// Dates in the future are allowed but not in the past.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatPlBriefingValidUntil_Leave(object sender, System.EventArgs e)
        {
            try
            {
                if (DatPlBriefingValidUntil.Value.Date < mDateNow.Date)
                {
                    DatPlBriefingValidUntil.Value = mDateNow;
                    throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.NO_VALID_DATE));
                }
            }
            catch (UIWarningException uwe)
            {
                ExceptionProcessor.GetInstance().Process(uwe);
            }
        }

        /// <summary>
        /// Raised when plants grid navigated 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgrPlPlant_CurrentCellChanged(object sender, System.EventArgs e)
        {
            if (DgrPlPlant.VisibleRowCount > 1)
            {
                PlantsTableNavigated();
            }
        }

        /// <summary>
        /// Raised when plants grid navigated 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgrPlPlant_Enter(object sender, System.EventArgs e)
        {
            if (DgrPlPlant.VisibleRowCount > 0)
            {
                PlantsTableNavigated();
            }
        }


        #endregion EventsPlant

        #region EventsReception


        /// <summary>
        /// Radio buttons "Zutritt auszubildende"
        /// When "Ausbildender" set to yes then automatically sets valid until date to 3.5 years in the future
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RbtReAccessApprentYes_CheckedChanged(object sender, EventArgs e)
        {
            GetMyController().HandleRadioButtons();
        }  

        /// <summary>
        /// Raised when user enters textbox Ausweis-Nr.
        /// Only relevant if user has role AUSWEIS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtReIdentityCardNumber_Enter(object sender, EventArgs e)
        {
            if (mIdCardNumberEditAuthorization) mIdCardFieldEntered = true;
        }


        /// <summary>
        /// Raised when user changes val textbox Ausweis-Nr.
        /// Only relevant if user has role AUSWEIS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtReIdentityCardNumber_TextChanged(object sender, EventArgs e)
        {
            if (mIdCardFieldEntered) mIdCardEdited = true;
        }

        /// <summary>
        /// Fires when the field "Gültig bis" at foot of form is left.
        /// 01.03.2004: declared internal so the event can be turned off:
        /// Cursor springs into this field when validUNTIL is re-caculated after prec medicals are deleted. 
        /// 27.04.04: Validation: dates in the future are allowed but not in the past
        /// <see cref="CoWorkerModel.DeletePrecMed"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void DatPassValidFrom_Leave(object sender, System.EventArgs e)
        {
            this.TxtCoCheckIn.Text = this.DatPassValidFrom.Value.ToString().Substring(0, 10);
            try
            {
                if (this.DatPassValidFrom.Value.Date < mDateNow.Date)
                {
                    this.DatPassValidFrom.Value = mDateNow;
                    throw new UIWarningException(MessageSingleton.GetInstance().GetMessage
                        (MessageSingleton.NO_PAST_VALIDFROM));
                }
            }
            catch (UIWarningException uwe)
            {
                ExceptionProcessor.GetInstance().Process(uwe);
            }
        }

		private void RbtReAccessAuthYes_CheckedChanged(object sender, System.EventArgs e)
		{
            GetMyController().HandleAccessAuthReception();
		}

        /// <summary>
        /// Raised when user clicks on "Maske leeren" in Reception tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReClear_Click(object sender, System.EventArgs e)
        {
            GetMyController().HandleClearFields();
        }
      
        /// <summary>
        /// Leave event Date field for "Sicherheitshinweis" 
        /// 27.04.04: Validation: dates in the past are allowed but not in the future
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatReSafetyInstructionsOn_Leave_1(object sender, System.EventArgs e)
        {
            try
            {
                if (this.DatReSafetyInstructionsOn.Value.Date > mDateNow.Date)
                {
                    this.DatReSafetyInstructionsOn.Value = mDateNow;
                    throw new UIWarningException(MessageSingleton.GetInstance().GetMessage
                        (MessageSingleton.NO_FUTURE_VALID));
                }
            }
            catch (UIWarningException uwe)
            {
                ExceptionProcessor.GetInstance().Process(uwe);
            }
        }

        /// <summary>
        /// Leave event Date field for "Zutrittsberechtigt" 
        /// 27.04.04: Validation: dates in the past are allowed but not in the future
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatReAccessAuthorizationOn_Leave_1(object sender, System.EventArgs e)
        {
            try
            {
                if (this.DatReAccessAuthorizationOn.Value.Date > mDateNow.Date)
                {
                    this.DatReAccessAuthorizationOn.Value = mDateNow;
                    throw new UIWarningException(MessageSingleton.GetInstance().GetMessage
                        (MessageSingleton.NO_FUTURE_VALID));
                }
            }
            catch (UIWarningException uwe)
            {
                ExceptionProcessor.GetInstance().Process(uwe);
            }
        }

        /// <summary>
        /// Leave event Date field for "Unterschrift geprüft am" 
        /// 27.04.04: Validation: dates in the past are allowed but not in the future
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatReSignatureOn_Leave_1(object sender, System.EventArgs e)
        {
            //try
            //{
            //    if (this.DatReSignatureOn.Value.Date > actualDatePicker.Date)
            //    {
            //        this.DatReSignatureOn.Value = actualDatePicker;
            //        throw new UIWarningException(MessageSingleton.GetInstance().GetMessage
            //            (MessageSingleton.NO_FUTURE_VALID));
            //    }
            //}
            //catch (UIWarningException uwe)
            //{
            //    ExceptionProcessor.GetInstance().Process(uwe);
            //}
        }

        /// <summary>
        /// Leave event Date field for "Ausgabedatum".
        /// Dates in the future are allowed but not in the past
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatDeliveryDate_Leave(object sender, System.EventArgs e)
        {
            try
            {
                if (this.DatDeliveryDate.Value.Date < mDateNow.Date)
                {
                    this.DatDeliveryDate.Value = mDateNow;
                    throw new UIWarningException(MessageSingleton.GetInstance().GetMessage
                        (MessageSingleton.NO_VALID_DATE));
                }
            }
            catch (UIWarningException uwe)
            {
                ExceptionProcessor.GetInstance().Process(uwe);
            }

        }


        /// <summary>
        /// Raised when user clicks "Ausweis löschen"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDelPassNumber_Click(object sender, System.EventArgs e)
        {
            GetMyController().HandleDeleteIdCardZKS();
        }

        /// <summary>
        /// Saves current user's id card readers (Terminals)
        /// separately from main Save Cwr
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnreSaveIdRdr_Click(object sender, EventArgs e)
        {
            GetMyController().HandleSaveIdReader();
        }
  

        /// <summary>
        /// Raised when coordinator selection is changed. Re-fills list of available excontractors
        /// and fills textfields in form header.
        /// Note: It is possible that excontractor is no longer in list if exco is invalid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CobReCoordinator_SelectedIndexChanged_1(object sender, System.EventArgs e)
        {
            if (null == CobReCoordinator.SelectedItem)
                return;

            if (null != GetMyController())
            {
                // no need for changing the other combobox in archive mode because dialog is locked
                if (GetMyController().Mode.Equals(AllFPASSDialogs.CWR_DIALOG_MODE_ARCHIVE))
                {
                    TxtCoordinator.Text = ((CoordLovItem)CobReCoordinator.SelectedItem).CoordFullName;
                    return;
                }
                TxtCoordinator.Text = ((CoordLovItem)CobReCoordinator.SelectedItem).CoordFullName;
            }
            decimal coordID = ((CoordLovItem)CobReCoordinator.SelectedItem).CoordID;


            if (coordID != 0)
            {
                if (!mCboContractorIsLeading)
                {
                    mCboCoordinatorIsLeading = true;
                    mCboContractorIsLeading = false;
                    FillReExternalContractor(coordID.ToString());
                }
            }
            else
            {
                if (mCboCoordinatorIsLeading)
                {
                    mCboContractorIsLeading = false;
                    mCboCoordinatorIsLeading = false;
                    FillReExternalContractor("0");
                }
            }

        }

        /// <summary>
        /// Raised when excontractors selection is changed. Re-fills list of available coordinators
        /// and fills textfields in form header
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CobReExternalContractor_SelectedIndexChanged_1(object sender, System.EventArgs e)
        {
            if (null == CobReExternalContractor.SelectedItem)
                return;

            if (null != GetMyController())
            {
                // no need for changing the other combobox in archive mode cause dialog is locked
                if (GetMyController().Mode.Equals(AllFPASSDialogs.CWR_DIALOG_MODE_ARCHIVE))
                {
                    TxtExternalContractor.Text = ((ContractorLovItem)CobReExternalContractor.SelectedItem).ContractorName;
                    return;
                }
                TxtExternalContractor.Text = ((ContractorLovItem)CobReExternalContractor.SelectedItem).ContractorName;
            }
            decimal contractorID = ((ContractorLovItem)CobReExternalContractor.SelectedItem).ContractorID;

            if (contractorID != 0)
            {
                if (!mCboCoordinatorIsLeading)
                {
                    mCboContractorIsLeading = true;
                    mCboCoordinatorIsLeading = false;
                    FillReCoordinator(contractorID.ToString());
                }
            }
            else
            {
                if (mCboContractorIsLeading)
                {
                    mCboContractorIsLeading = false;
                    mCboCoordinatorIsLeading = false;
                    FillReCoordinator("0");
                }
            }
        }


        /// <summary>
        /// Raised when text in Date of Birth changed: ensures date is not longer than 10 characters
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtReDateOfBirth_TextChanged(object sender, System.EventArgs e)
        {
            TxtReDateOfBirth.Text = TxtReDateOfBirth.Text.Substring(0, 10);
        }

        /// <summary>
        /// Raised when user clicks button to open Search Excontractor form
        /// Resets exco and coord comboboxes
        /// DotNet Framework SP 1 has an issue with conversions,
        /// If combobox.SelectedValue = 0  and then SelectedItem is referenced
        /// fatal exception (null reference) occurs
        /// must do a conversion to decimal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReSearchExternalContractor_Click_1(object sender, System.EventArgs e)
        {
            mCalledSearchContractor = 1;
            CobReExternalContractor.SelectedValue = Convert.ToDecimal(0);
            CobReCoordinator.SelectedValue = Convert.ToDecimal(0);
            GetMyController().HandleEventOpenSearchExternalContractorDialog();
        }


        /// <summary>
        /// Validates field "Date of birth"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtReDateOfBirth_Leave(object sender, System.EventArgs e)
        {
            mBirthDateOK = true;
            bool onReceptionTab = TabSiteSecurity.SelectedTab.ToString().EndsWith("{Empfang}");

            if (TxtReDateOfBirth.Text.Length > 0)
            {
                try
                {
                    if (!StringValidation.GetInstance().IsDateString(TxtReDateOfBirth.Text))
                    {
                        throw new UIWarningException(MessageSingleton.GetInstance().
                            GetMessage(MessageSingleton.INVALID_BIRTHDATE));
                    }
                    else
                    {
                        DateTime birthDate = Convert.ToDateTime(TxtReDateOfBirth.Text);
                        DateTime now = DateTime.Now;

                        if (birthDate.CompareTo(now) > 0)
                        {
                            throw new UIWarningException(MessageSingleton.GetInstance().
                                GetMessage(MessageSingleton.INVALID_BIRTHDATE));
                        }

                    }
                }
                catch (UIWarningException uwe)
                {
                    if (onReceptionTab)
                    {
                        ExceptionProcessor.GetInstance().Process(uwe);
                        this.TxtReDateOfBirth.Focus();
                    }
                    else
                    {
                        mBirthDateOK = false;
                    }
                }
            }
        }


        /// <summary>
        /// Raised when cursor leaves datefield "Auszubildende gültig bis". 
        /// Dates in the future are allowed but not in the past.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatReAccessApprent_Leave(object sender, System.EventArgs e)
        {
            try
            {
                if (DatReAccessApprent.Value.Date < mDateNow.Date)
                {
                    DatReAccessApprent.Value = mDateNow;
                    throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.NO_VALID_DATE));
                }
            }
            catch (UIWarningException uwe)
            {
                ExceptionProcessor.GetInstance().Process(uwe);
            }

        }

        private void RbtReSafetyInstructionsYes_CheckedChanged(object sender, System.EventArgs e)
        {
            this.DatReSafetyInstructionsOn.Enabled = true;
        }

        private void RbtReSafetyInstructionsNo_CheckedChanged(object sender, System.EventArgs e)
        {
            this.DatReSafetyInstructionsOn.Enabled = false;
        }

        private void RbtReSignatureYes_CheckedChanged(object sender, System.EventArgs e)
        {
            //this.DatReSignatureOn.Enabled = true;
        }

        private void RbtReSignatureNo_CheckedChanged(object sender, System.EventArgs e)
        {
            //this.DatReSignatureOn.Enabled = false;
        }



        /// <summary>
        /// Raised when user clicks "Ausweis zuordnen ZKS" for Hitag2 number
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRePassNrHitag_Click(object sender, System.EventArgs e)
        {
            GetMyController().HandleReadIdCardZKS(IDCardTypes.Hitag2);
            TxtReIDCardNumHitag2.Enabled = false;
        }

        /// <summary>
        ///  Raised when user clicks "Ausweis zuordnen ZKS" for Mifare number
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRePassNrMifare_Click(object sender, EventArgs e)
        {
            GetMyController().HandleReadIdCardZKS(IDCardTypes.Mifare);
            TxtReIDCardNumMifareNo.Enabled = false;
        }

        /// <summary>
        /// Raised when user clicks USB button for Hitag2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRePassNrHitagUSB_Click(object sender, EventArgs e)
        {
            TxtReIDCardNumHitag2.Enabled = true;
            TxtReIDCardNumHitag2.ReadOnly = false;
            TxtReIDCardNumHitag2.Focus();
        }

        /// <summary>
        /// Raised when user clicks USB button for Mifare
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRePassNrMifareUSB_Click(object sender, EventArgs e)
        {
            TxtReIDCardNumMifareNo.Enabled = true;
            TxtReIDCardNumMifareNo.ReadOnly = false;
            TxtReIDCardNumMifareNo.Focus();
        }


        /// <summary>
        /// Event for id card READER: Ausweisleser zuordnen Hitag. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReHitag2Reader_Click(object sender, EventArgs e)
        {
            mCalledSearchIdReader = 1;
            GetMyController().IDCardReaderType = IDCardTypes.Hitag2;
            GetMyController().HandleEventOpenSearchIDCardReaderDialog();
        }


        /// <summary>
        /// Event for id card READER: Ausweisleser zuordnen Mifare
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReMifareReader_Click(object sender, EventArgs e)
        {
            mCalledSearchIdReader = 1;
            GetMyController().IDCardReaderType = IDCardTypes.Mifare;
            GetMyController().HandleEventOpenSearchIDCardReaderDialog();
        }

        /// <summary>
        /// Event handler for button "Berechtigung entziehen": removes Sicherheitshinweis Empfang.
        /// This authoization is relevant for cwr access, i.e. access is revoked when this authorization is revoked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReSafetyRevoke_Click(object sender, EventArgs e)
        {
            RbtReSafetyInstructionsNo.Checked = true;
            GetMyController().HandleDeleteIdCardZKS();
        }


        #endregion // End of Events

        #region EventsSafetyAtWork

        /// <summary>
        /// Leave event Date field for "Belehrung AS" 
        /// 27.04.04: Validation: dates in the past are allowed but not in the future
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatSaAtWoSafetyAtWorkBriefingDone_Leave_1(object sender, System.EventArgs e)
        {
            try
            {
                if (this.DatSaAtWoSafetyAtWorkBriefingDone.Value.Date > mDateNow.Date)
                {
                    this.DatSaAtWoSafetyAtWorkBriefingDone.Value = mDateNow;
                    throw new UIWarningException(MessageSingleton.GetInstance().GetMessage
                        (MessageSingleton.NO_FUTURE_VALID));
                }
            }
            catch (UIWarningException uwe)
            {
                ExceptionProcessor.GetInstance().Process(uwe);
            }
        }

        /// <summary>
        /// Leave event Date field for "Flurförder'" 
        /// 27.04.04: Validation: dates in the past are allowed but not in the future
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatSaAtWoPalletLifterBriefingDoneOn_Leave_1(object sender, System.EventArgs e)
        {
            try
            {
                if (this.DatSaAtWoPalletLifterBriefingDoneOn.Value.Date > mDateNow.Date)
                {
                    this.DatSaAtWoPalletLifterBriefingDoneOn.Value = mDateNow;
                    throw new UIWarningException(MessageSingleton.GetInstance().GetMessage
                        (MessageSingleton.NO_FUTURE_VALID));
                }
            }
            catch (UIWarningException uwe)
            {
                ExceptionProcessor.GetInstance().Process(uwe);
            }
        }

        /// <summary>
        /// Leave event Date field for "Belehr Kräne" 
        /// 27.04.04: Validation: dates in the past are allowed but not in the future
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatSaAtWoCranesBriefingDoneOn_Leave_1(object sender, System.EventArgs e)
        {
            try
            {
                if (this.DatSaAtWoCranesBriefingDoneOn.Value.Date > mDateNow.Date)
                {
                    this.DatSaAtWoCranesBriefingDoneOn.Value = mDateNow;
                    throw new UIWarningException(MessageSingleton.GetInstance().GetMessage
                        (MessageSingleton.NO_FUTURE_VALID));
                }
            }
            catch (UIWarningException uwe)
            {
                ExceptionProcessor.GetInstance().Process(uwe);
            }
        }


        #endregion // End of Events

        #region EventsSiteFireService

        /// <summary>
        /// Button "Beleg drucken"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSiFiMaskTicket_Click(object sender, System.EventArgs e)
        {
            GetMyController().HandleRespMaskTicket();
        }

        /// <summary>
        /// Gets last maintainance date of current mask
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSiFiGetMaintDate_Click(object sender, EventArgs e)
        {

        }
 
        /// <summary>
        /// Raised when "erteilt" changed: Resp mask can only be lent out if briefing received
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxSiFiRespMaskBriefRec_CheckedChanged(object sender, System.EventArgs e)
        {
            GetMyController().HandleRadioButtons();
        }

        /// <summary>
        /// New 29.04.04: Leave event Date field for "Brandsicherheitsposten'" 
        /// Validation: dates in the past are allowed but not in the future
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatSiFiFiremanDoneOn_Leave(object sender, System.EventArgs e)
        {
            try
            {
                if (this.DatSiFiFiremanDoneOn.Value.Date > mDateNow.Date)
                {
                    this.DatSiFiFiremanDoneOn.Value = mDateNow;
                    throw new UIWarningException(MessageSingleton.GetInstance().GetMessage
                        (MessageSingleton.NO_FUTURE_VALID));
                }
            }
            catch (UIWarningException uwe)
            {
                ExceptionProcessor.GetInstance().Process(uwe);
            }
        }

        /// <summary>
        /// Raised when user leaves field "Atemschutzmaskenunterweis. erteilt am".
        /// Allows dates in the past but not in the future
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatSiFiRespMaskBriefRecOn_Leave(object sender, System.EventArgs e)
        {
            try
            {
                if (DatSiFiRespMaskBriefRecOn.Value.Date > mDateNow.Date)
                {
                    DatSiFiRespMaskBriefRecOn.Value = mDateNow;
                    throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.NO_FUTURE_VALID));
                }
            }
            catch (UIWarningException uwe)
            {
                ExceptionProcessor.GetInstance().Process(uwe);
            }
        }

        /// <summary>
        /// Raised when user leaves field "Atemschutzmaskenunterweis. angeordnet am".
        /// Allows dates in the past but not in the future
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatSiFiRespMaskBriefDirOn_Leave(object sender, EventArgs e)
        {
            try
            {
                if (DatSiFiRespMaskBriefDirOn.Value.Date > mDateNow.Date)
                {
                    DatSiFiRespMaskBriefDirOn.Value = mDateNow;
                    throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.NO_FUTURE_VALID));
                }
            }
            catch (UIWarningException uwe)
            {
                ExceptionProcessor.GetInstance().Process(uwe);
            }
        }

        /// <summary>
        /// Leave event Date field for "Maske erhalten am" 
        /// Allows dates in the past but not in the future
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatSiFiRespiratoryMaskReceivedOn_Leave_1(object sender, System.EventArgs e)
        {
            try
            {
                if (DatSiFiMaskLentOnFlo.Value.Date > mDateNow.Date)
                {
                    DatSiFiMaskLentOnFlo.Value = mDateNow;
                    throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.NO_FUTURE_VALID));
                }
            }
            catch (UIWarningException uwe)
            {
                ExceptionProcessor.GetInstance().Process(uwe);
            }
        }

        /// <summary>
        /// Leave event Date field for "Maske abgegeben am" 
        /// Allows dates in the past but not in the future
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatSiFiRespiratoryMaskDeliveredOn_Leave_1(object sender, System.EventArgs e)
        {
            try
            {
                if (DatSiFiMaskBackOnFlo.Value.Date > mDateNow.Date)
                {
                    DatSiFiMaskBackOnFlo.Value = mDateNow;
                    throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.NO_FUTURE_VALID));
                }
            }
            catch (UIWarningException uwe)
            {
                ExceptionProcessor.GetInstance().Process(uwe);
            }
        }

        /// <summary>
        /// Leave event Date field for "Belehr G26.2" 
        /// 27.04.04: Validation: dates in the past are allowed but not in the future
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatSiFiSiteSecurityBriefingDoneOnG26_2_Leave(object sender, System.EventArgs e)
        {
            try
            {
                if (this.DatSiFiSiteSecurityBriefingDoneOnG26_2.Value.Date > mDateNow.Date)
                {
                    this.DatSiFiSiteSecurityBriefingDoneOnG26_2.Value = mDateNow;
                    throw new UIWarningException(MessageSingleton.GetInstance().GetMessage
                        (MessageSingleton.NO_FUTURE_VALID));
                }
            }
            catch (UIWarningException uwe)
            {
                ExceptionProcessor.GetInstance().Process(uwe);
            }
        }

        /// <summary>
        /// Leave event Date field for "Belehr G26.3" 
        /// 27.04.04: Validation: dates in the past are allowed but not in the future
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatSiFiSiteSecurityBriefingDoneOnG26_3_Leave(object sender, System.EventArgs e)
        {
            try
            {
                if (this.DatSiFiSiteSecurityBriefingDoneOnG26_3.Value.Date > mDateNow.Date)
                {
                    this.DatSiFiSiteSecurityBriefingDoneOnG26_3.Value = mDateNow;
                    throw new UIWarningException(MessageSingleton.GetInstance().GetMessage
                        (MessageSingleton.NO_FUTURE_VALID));
                }
            }
            catch (UIWarningException uwe)
            {
                ExceptionProcessor.GetInstance().Process(uwe);
            }
        }


        #endregion // End of Events

        #region EventsMedicalService

        //		/// <summary>
        //		/// Leave event Date field for "VU erfasst am" 
        //		/// 27.04.04: Validation: dates in the past are allowed but not in the future
        //		/// </summary>
        //		/// <param name="sender"></param>
        //		/// <param name="e"></param>
        //		private void DatSiMedExecutedOn_Leave(object sender, System.EventArgs e)
        //		{
        //			try
        //			{
        //				if( this.DatSiMedExecutedOn.Value.Date > actualDatePicker.Date )
        //				{
        //					this.DatSiMedExecutedOn.Value = actualDatePicker;
        //					throw new UIWarningException (MessageSingleton.GetInstance().GetMessage
        //						(MessageSingleton.NO_FUTURE_VALID));				
        //				}	
        //			}
        //			catch (UIWarningException uwe)
        //			{
        //				ExceptionProcessor.GetInstance().Process(uwe);
        //			}
        //		}


        /// <summary>
        /// Leave event Date field for "Sicherheitshinweis" 
        /// 19.03.2004: Added Date attribute to DateTimePicker value so that time part is ignored 
        /// 
        /// 27.04.04: Validation: For this field dates are allowed in the future (medical validUNTIL) but not in the past
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatSiMedValidUntil_Leave_1(object sender, System.EventArgs e)
        {
            try
            {
                if (this.DatSiMedValidUntil.Value.Date < mDateNow.Date)
                {
                    this.DatSiMedValidUntil.Value = mDateNow;
                    throw new UIWarningException(MessageSingleton.GetInstance().GetMessage
                        (MessageSingleton.NO_VALID_DATE));
                }
            }
            catch (UIWarningException uwe)
            {
                ExceptionProcessor.GetInstance().Process(uwe);
            }
        }

        private void CboSiMedPrecautionaryMedical_Enter(object sender, System.EventArgs e)
        {
            mEnteredCboPreMedType = true;
        }

        private void CboSiMedPrecautionaryMedical_Leave(object sender, System.EventArgs e)
        {
            mEnteredCboPreMedType = false;
        }


        private void DgrSiMedPrecautionaryMedical_Enter(object sender, System.EventArgs e)
        {
            if (this.DgrSiMedPrecautionaryMedical.VisibleRowCount > 0)
            {
                this.MedicalTableNavigated();
            }
        }

        private void DgrSiMedPrecautionaryMedical_CurrentCellChanged(object sender, System.EventArgs e)
        {
            if (this.DgrSiMedPrecautionaryMedical.VisibleRowCount > 1)
            {
                this.MedicalTableNavigated();
            }
        }

        private void CboSiMedPrecautionaryMedical_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (mEnteredCboPreMedType)
            {
                GetMyController().HandlePrecMedCreate();

            }
        }

        /// <summary>
        /// New 01.03.2004:
        /// Delete current prec medical
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelPrecMed_Click(object sender, System.EventArgs e)
        {
            GetMyController().HandlePrecMedDelete();
        }


        #endregion // End of Events

        #region EventsSiteSecurity

        private void CbxSiSeIdPhotoHitagRec_CheckedChanged(object sender, EventArgs e)
        {
            GetMyController().HandleIdCardPhoto();
        }

        /// <summary>
        /// Leave event Date field for "Werkschutzbelehr erteilt" 
        /// Validation: dates in the past are allowed but not in the future
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatSiSeSiteSecurityReceivedOn_Leave(object sender, System.EventArgs e)
        {
            try
            {
                if (this.DatSiSeSiteSecBriRec.Value.Date > mDateNow.Date)
                {
                    this.DatSiSeSiteSecBriRec.Value = mDateNow;
                    throw new UIWarningException(MessageSingleton.GetInstance().GetMessage
                        (MessageSingleton.NO_FUTURE_VALID));
                }
            }
            catch (UIWarningException uwe)
            {
                ExceptionProcessor.GetInstance().Process(uwe);
            }
        }

        /// <summary>
        /// Leave event Date field for "Lichtbildausweis erteilt" 
        /// Validation: dates in the past are allowed but not in the future
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatSiSeIdentityCardRecievedOn_Leave(object sender, System.EventArgs e)
        {
            try
            {
                if (this.DatSiSeIdPhotoSmActRec.Value.Date > mDateNow.Date)
                {
                    this.DatSiSeIdPhotoSmActRec.Value = mDateNow;
                    throw new UIWarningException(MessageSingleton.GetInstance().GetMessage
                        (MessageSingleton.NO_FUTURE_VALID));
                }
            }
            catch (UIWarningException uwe)
            {
                ExceptionProcessor.GetInstance().Process(uwe);
            }
        }


        /// <summary>
        /// If VehicleEntranceShort is granted ("Genehmigt"), then revoke Long if it was wished
        /// (except for logic arising from Db stored procedure SP_EXTENDACCESS)
        /// Only run into event if it was user who clicked radio button (i.e. not during loading of form)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RbtSiSeVehicleEntranceShortReceivedYes_CheckedChanged(object sender, System.EventArgs e)
        {
            GetMyController().HandleRadioButtons();
        }

        private void RbtSiSeVehicleEntranceShortReceivedNo_CheckedChanged(object sender, System.EventArgs e)
        {
            GetMyController().HandleRadioButtons();
        }

        /// <summary>
        /// If VehicleEntranceLong is granted ("Genehmigt") then deny Short Access if it was desired
        /// Only run into event if it was user who clicked radio buton (i.e. not during loading of form)
        /// /// This covers logic in Db stroed procedure SP_EXTENDACCESS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RbtSiSeVehicleEntranceLongYes_CheckedChanged(object sender, System.EventArgs e)
        {

            if (mEnteredRbtSiSeVehShortReceivedYes)
            {
                if (this.RbtSiSeVehicleEntranceLongYes.Checked && this.RbtSiSeVehicleEntranceShort.Checked)
                {
                    this.RbtSiSeVehicleEntranceShortReceivedNo.Checked = true;
                }
            }
            GetMyController().HandleRadioButtons();
        }

        private void RbtSiSeVehicleEntranceLongNo_CheckedChanged(object sender, System.EventArgs e)
        {
            GetMyController().HandleRadioButtons();
        }


        /// <summary>
        /// Leave event Date field for "Kfz Zutritt kurz" 
        /// Not changed: date in the past not allowed 27.04.04
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatSiSeVehicleEntranceShortReceivedOn_Leave(object sender, System.EventArgs e)
        {
            try
            {
                if (this.DatSiSeVehicleEntranceShortReceivedOn.Value.Date < mDateNow.Date)
                {
                    this.DatSiSeVehicleEntranceShortReceivedOn.Value = mDateNow;
                    throw new UIWarningException(MessageSingleton.GetInstance().GetMessage
                        (MessageSingleton.NO_VALID_DATE));
                }
            }
            catch (UIWarningException uwe)
            {
                ExceptionProcessor.GetInstance().Process(uwe);
            }
        }

        /// <summary>
        /// Leave event Date field for "Kfz Zutritt lang" 
        /// Not changed: date in the past not allowed 27.04.04
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatSiSeVehicleEntranceLongReceivedOn_Leave(object sender, System.EventArgs e)
        {
            try
            {
                if (this.DatSiSeVehicleEntranceLongReceivedOn.Value.Date < mDateNow.Date)
                {
                    this.DatSiSeVehicleEntranceLongReceivedOn.Value = mDateNow;
                    throw new UIWarningException(MessageSingleton.GetInstance().GetMessage
                        (MessageSingleton.NO_VALID_DATE));
                }
            }
            catch (UIWarningException uwe)
            {
                ExceptionProcessor.GetInstance().Process(uwe);
            }
        }

        
        private void RbtSiSeAccessAuthYes_CheckedChanged(object sender, System.EventArgs e)
        {
            GetMyController().HandleAccessAuthSiteSecurity();
        }

        /// <summary>
        /// Event handler for Zutritt enthziehen (revoke cwr Access) for SiteSecurity 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSiSeAccessRevoke_Click(object sender, EventArgs e)
        {
            RbtReAccessAuthNo.Checked = true;
            RbtSiSeAccessAuthNo.Checked = true;
            GetMyController().HandleRevokeAccessSiSe();
        }

        #endregion // End of Events

        #region EventsTecDepartment

        /// <summary>
        /// Leave event Date field for "Hubarbeitsb" 
        /// 27.04.04: Validation: dates in the past are allowed but not in the future
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatTecBriefingDoneOn_Leave_1(object sender, System.EventArgs e)
        {
            try
            {
                if (this.DatTecBriefingDoneOn.Value.Date > mDateNow.Date)
                {
                    this.DatTecBriefingDoneOn.Value = mDateNow;
                    throw new UIWarningException(MessageSingleton.GetInstance().GetMessage
                        (MessageSingleton.NO_FUTURE_VALID));
                }
            }
            catch (UIWarningException uwe)
            {
                ExceptionProcessor.GetInstance().Process(uwe);
            }
        }


        #endregion // End of Events      
      
    }
}

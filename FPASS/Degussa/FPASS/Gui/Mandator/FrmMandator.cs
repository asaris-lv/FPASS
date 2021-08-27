using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Degussa.FPASS.Gui;
using Degussa.FPASS.Util.UserManagement;

namespace Degussa.FPASS.Gui.Mandator
{
	/// <summary>
	/// A FrmMandator is the view of the MVC-triad MandatorController,
	/// MandatorModel and FrmMandator.
	/// FrmMandator extends from the BaseView.
	/// </summary>
	/// <remarks>
	/// <para><b>History</b></para>
	/// <div class="tablediv">
	/// <table class="dtTABLE" cellspacing="0">
	///		<tr>
	///			<th width="20%">Wollersheim-Heer</th>
	///			<th width="20%">10/01/2003</th>
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
	public class FrmMandator : BaseView
	{
		#region Members


		#endregion //End of Members

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button BtnGoOn;
		private System.Windows.Forms.Button BtnCancel;
		internal System.Windows.Forms.ComboBox CboMandator;
		private System.Windows.Forms.HelpProvider helpProviderFrmMand;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FrmMandator()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			InitView();
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FrmMandator));
			this.CboMandator = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.BtnGoOn = new System.Windows.Forms.Button();
			this.BtnCancel = new System.Windows.Forms.Button();
			this.helpProviderFrmMand = new System.Windows.Forms.HelpProvider();
			this.SuspendLayout();
			// 
			// CboMandator
			// 
			this.CboMandator.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.helpProviderFrmMand.SetHelpKeyword(this.CboMandator, "OnlineHilfe\\Online_hilfe_Mandant_auswaehlen.htm");
			this.helpProviderFrmMand.SetHelpNavigator(this.CboMandator, System.Windows.Forms.HelpNavigator.Topic);
			this.CboMandator.Location = new System.Drawing.Point(184, 78);
			this.CboMandator.Name = "CboMandator";
			this.helpProviderFrmMand.SetShowHelp(this.CboMandator, true);
			this.CboMandator.Size = new System.Drawing.Size(208, 23);
			this.CboMandator.TabIndex = 6;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.helpProviderFrmMand.SetHelpKeyword(this.label1, "OnlineHilfe\\Online_hilfe_Mandant_auswaehlen.htm");
			this.helpProviderFrmMand.SetHelpNavigator(this.label1, System.Windows.Forms.HelpNavigator.Topic);
			this.label1.Location = new System.Drawing.Point(40, 80);
			this.label1.Name = "label1";
			this.helpProviderFrmMand.SetShowHelp(this.label1, true);
			this.label1.Size = new System.Drawing.Size(136, 24);
			this.label1.TabIndex = 7;
			this.label1.Text = "Mandantenauswahl";
			// 
			// BtnGoOn
			// 
			this.BtnGoOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.BtnGoOn.Location = new System.Drawing.Point(240, 200);
			this.BtnGoOn.Name = "BtnGoOn";
			this.BtnGoOn.Size = new System.Drawing.Size(120, 30);
			this.BtnGoOn.TabIndex = 8;
			this.BtnGoOn.Text = "&Weiter";
			this.BtnGoOn.Click += new System.EventHandler(this.BtnGoOn_Click);
			// 
			// BtnCancel
			// 
			this.BtnCancel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.BtnCancel.Location = new System.Drawing.Point(80, 200);
			this.BtnCancel.Name = "BtnCancel";
			this.BtnCancel.Size = new System.Drawing.Size(120, 30);
			this.BtnCancel.TabIndex = 9;
			this.BtnCancel.Text = "&Abbrechen";
			this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
			// 
			// helpProviderFrmMand
			// 
			this.helpProviderFrmMand.HelpNamespace = "Help\\FPASSHilfe.chm";
			// 
			// FrmMandator
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(442, 273);
			this.Controls.Add(this.BtnCancel);
			this.Controls.Add(this.BtnGoOn);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.CboMandator);
			this.helpProviderFrmMand.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.Topic);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(450, 300);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(450, 300);
			this.Name = "FrmMandator";
			this.helpProviderFrmMand.SetShowHelp(this, true);
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Auswahl eines Mandanten";
			this.ResumeLayout(false);

		}
		#endregion

		#region Accessors 

		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// Initialize input fields.
		/// </summary>
		private void InitView()
		{
			FillLists();
		}

		internal override void FillLists() 
		{
			this.CboMandator.Refresh();
			this.CboMandator.DataSource = null;
			this.CboMandator.DataSource = UserManagementControl.getInstance().UsersWithMandatorInfo;
			this.CboMandator.DisplayMember = "MandatorName";
			this.CboMandator.ValueMember = "MandatorID";
		}

		#endregion // End of Methods

		

		#region Events

		private void BtnCancel_Click(object sender, System.EventArgs e)
		{
			((MandatorController)mController).HandleEventUnControlledClose();
		}

		private void BtnGoOn_Click(object sender, System.EventArgs e)
		{
			((MandatorController)mController).handleEventProcessWithFPASS();
		}

		#endregion // End of Events

	}
}

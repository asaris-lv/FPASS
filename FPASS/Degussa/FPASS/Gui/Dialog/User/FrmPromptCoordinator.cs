using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Degussa.FPASS.Bo.Administration;
using Degussa.FPASS.Util.Messages;

using de.pta.Component.Errorhandling;

namespace Degussa.FPASS.Gui.Dialog.User
{
	/// <summary>
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
	public class FrmPromptCoordinator : System.Windows.Forms.Form
	{
		
		#region Members

		private System.Windows.Forms.Button BtnCancel;
		private System.Windows.Forms.Button BtnGoOn;

		private System.Windows.Forms.Label label1;
		internal System.Windows.Forms.ComboBox CboCoordinator;
		internal System.Windows.Forms.Label lblPrompt;

		private decimal				mCurrentCoordID;
		private BOExcoCoordinator	mBOExcoCoordinator;
		private ArrayList			mArlCoordinators;
		private const string		TITLE_PROMPT = "FPASS - Uservervaltuing";

		private System.Windows.Forms.HelpProvider helpProviderFrmPrompt;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#endregion //End of Members

		#region Constructors

		public FrmPromptCoordinator()
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

		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FrmPromptCoordinator));
			this.lblPrompt = new System.Windows.Forms.Label();
			this.BtnCancel = new System.Windows.Forms.Button();
			this.BtnGoOn = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.CboCoordinator = new System.Windows.Forms.ComboBox();
			this.helpProviderFrmPrompt = new System.Windows.Forms.HelpProvider();
			this.SuspendLayout();
			// 
			// lblPrompt
			// 
			this.lblPrompt.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblPrompt.Location = new System.Drawing.Point(16, 16);
			this.lblPrompt.Name = "lblPrompt";
			this.lblPrompt.Size = new System.Drawing.Size(400, 56);
			this.lblPrompt.TabIndex = 0;
			this.lblPrompt.Text = "label1";
			this.lblPrompt.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// BtnCancel
			// 
			this.BtnCancel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.BtnCancel.Location = new System.Drawing.Point(80, 200);
			this.BtnCancel.Name = "BtnCancel";
			this.BtnCancel.Size = new System.Drawing.Size(120, 30);
			this.BtnCancel.TabIndex = 2;
			this.BtnCancel.Text = "&Abbrechen";
			this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
			// 
			// BtnGoOn
			// 
			this.BtnGoOn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.BtnGoOn.Location = new System.Drawing.Point(240, 200);
			this.BtnGoOn.Name = "BtnGoOn";
			this.BtnGoOn.Size = new System.Drawing.Size(120, 30);
			this.BtnGoOn.TabIndex = 1;
			this.BtnGoOn.Text = "&Weiter";
			this.BtnGoOn.Click += new System.EventHandler(this.BtnGoOn_Click);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.helpProviderFrmPrompt.SetHelpKeyword(this.label1, "OnlineHilfe\\Online_hilfe_Benutzer_Rolle_zuordnen.htm");
			this.helpProviderFrmPrompt.SetHelpNavigator(this.label1, System.Windows.Forms.HelpNavigator.Topic);
			this.label1.Location = new System.Drawing.Point(40, 116);
			this.label1.Name = "label1";
			this.helpProviderFrmPrompt.SetShowHelp(this.label1, true);
			this.label1.Size = new System.Drawing.Size(136, 24);
			this.label1.TabIndex = 13;
			this.label1.Text = "Koordinatorenauswahl";
			// 
			// CboCoordinator
			// 
			this.CboCoordinator.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.helpProviderFrmPrompt.SetHelpKeyword(this.CboCoordinator, "OnlineHilfe\\Online_hilfe_Benutzer_Rolle_zuordnen.htm");
			this.helpProviderFrmPrompt.SetHelpNavigator(this.CboCoordinator, System.Windows.Forms.HelpNavigator.Topic);
			this.CboCoordinator.Location = new System.Drawing.Point(189, 114);
			this.CboCoordinator.Name = "CboCoordinator";
			this.helpProviderFrmPrompt.SetShowHelp(this.CboCoordinator, true);
			this.CboCoordinator.Size = new System.Drawing.Size(208, 23);
			this.CboCoordinator.TabIndex = 0;
			// 
			// FrmPromptCoordinator
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(442, 273);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.CboCoordinator);
			this.Controls.Add(this.BtnCancel);
			this.Controls.Add(this.BtnGoOn);
			this.Controls.Add(this.lblPrompt);
			this.HelpButton = true;
			this.helpProviderFrmPrompt.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.Topic);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FrmPromptCoordinator";
			this.helpProviderFrmPrompt.SetShowHelp(this, true);
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Koordinator auswählen";
			this.ResumeLayout(false);

		}
		#endregion

		#region Accessors 

		public decimal CurrentCoordID
		{
			get 
			{
				return mCurrentCoordID;
			}
			set 
			{
				mCurrentCoordID = value;
			}
		} 

		public ArrayList ArlCoordinators
		{
			get 
			{
				return mArlCoordinators;
			}
			set 
			{
				mArlCoordinators = value;
			}
		} 

		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// Initialize input fields.
		/// </summary>
		private void InitView()
		{
			
		}

		internal void FillCoordinators()
		{
			this.CboCoordinator.DataSource    = mArlCoordinators;
			foreach ( BOExcoCoordinator bo in mArlCoordinators )
			{
				this.CboCoordinator.DisplayMember = "Coordinator";
				this.CboCoordinator.ValueMember   = "ECODID";
			}
		}

		#endregion // End of Methods

		#region Events 

		/// <summary>
		/// Button "Weiter". Get the ID of the coordinator selected in the combobox.
		/// If none is selected, interrupt with messagebox
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnGoOn_Click(object sender, System.EventArgs e)
		{
			mBOExcoCoordinator = (BOExcoCoordinator) this.CboCoordinator.SelectedItem;
			mCurrentCoordID    = mBOExcoCoordinator.ECODID;

			if ( mBOExcoCoordinator.ECODID == 0 )
			{
                MessageBox.Show(this, "Sie müssen einen Koordinator auswählen, um fortzufahren.", TITLE_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				this.Hide();
			}
		}

		/// <summary>
		/// Button "Abbrechen". Set coordindator ID to zero (default) and close prompt
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnCancel_Click(object sender, System.EventArgs e)
		{
			mCurrentCoordID = 0;
			this.Hide();
		}

		#endregion

	}
}

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Testing
{
	/// <summary>
	/// Summary description for FrmPopup.
	/// </summary>
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
	public class FrmPopup : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.HelpProvider helpProvider1;
		private System.Windows.Forms.ComboBox comboBox1;
		#region Members

		#endregion //End of Members

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FrmPopup()
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
			this.label1 = new System.Windows.Forms.Label();
			this.helpProvider1 = new System.Windows.Forms.HelpProvider();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(88, 64);
			this.label1.Name = "label1";
			this.label1.TabIndex = 0;
			this.label1.Text = "Call Help";
			// 
			// helpProvider1
			// 
			this.helpProvider1.HelpNamespace = "D:\\FPASSTool\\FPASS\\FPASSHilfe.chm";
			// 
			// comboBox1
			// 
			this.helpProvider1.SetHelpKeyword(this.comboBox1, "OnlineHilfe\\Online_hilfe_Mandant_auswaehlen.htm");
			this.helpProvider1.SetHelpNavigator(this.comboBox1, System.Windows.Forms.HelpNavigator.Topic);
			this.comboBox1.Location = new System.Drawing.Point(72, 104);
			this.comboBox1.Name = "comboBox1";
			this.helpProvider1.SetShowHelp(this.comboBox1, true);
			this.comboBox1.Size = new System.Drawing.Size(152, 21);
			this.comboBox1.TabIndex = 1;
			this.comboBox1.Text = "Bobbins";
			// 
			// FrmPopup
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.label1);
			this.HelpButton = true;
			this.helpProvider1.SetHelpKeyword(this, "");
			this.helpProvider1.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.Topic);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FrmPopup";
			this.helpProvider1.SetShowHelp(this, true);
			this.Text = "FrmPopup";
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
			
		}
		#endregion // End of Methods

	}
}

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Testing
{
	/// <summary>
	/// Summary description for Form2.
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
	public class Form2 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cboFremdfirma;
		private System.Windows.Forms.ComboBox cboKoordinator;
		private System.Windows.Forms.Button BtnHelp;
		#region Members

		#endregion //End of Members

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form2()
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
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.cboFremdfirma = new System.Windows.Forms.ComboBox();
			this.cboKoordinator = new System.Windows.Forms.ComboBox();
			this.BtnHelp = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(72, 161);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(112, 23);
			this.label2.TabIndex = 8;
			this.label2.Text = "Fremdfirma";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(72, 89);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(112, 23);
			this.label1.TabIndex = 7;
			this.label1.Text = "Koordinator";
			// 
			// cboFremdfirma
			// 
			this.cboFremdfirma.Location = new System.Drawing.Point(216, 161);
			this.cboFremdfirma.Name = "cboFremdfirma";
			this.cboFremdfirma.Size = new System.Drawing.Size(184, 21);
			this.cboFremdfirma.TabIndex = 6;
			// 
			// cboKoordinator
			// 
			this.cboKoordinator.Location = new System.Drawing.Point(216, 89);
			this.cboKoordinator.Name = "cboKoordinator";
			this.cboKoordinator.Size = new System.Drawing.Size(184, 21);
			this.cboKoordinator.TabIndex = 5;
			// 
			// BtnHelp
			// 
			this.BtnHelp.Location = new System.Drawing.Point(176, 224);
			this.BtnHelp.Name = "BtnHelp";
			this.BtnHelp.Size = new System.Drawing.Size(104, 32);
			this.BtnHelp.TabIndex = 9;
			this.BtnHelp.Text = "Call Help";
			this.BtnHelp.Click += new System.EventHandler(this.BtnHelp_Click);
			// 
			// Form2
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(472, 273);
			this.Controls.Add(this.BtnHelp);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cboFremdfirma);
			this.Controls.Add(this.cboKoordinator);
			this.Name = "Form2";
			this.Text = "Form2";
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

		private void BtnHelp_Click(object sender, System.EventArgs e)
		{
			FrmPopup frmPop = new FrmPopup();
			frmPop.ShowDialog();
		}

	}
}

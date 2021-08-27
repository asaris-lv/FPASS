using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Degussa.FPASS.Util.ProgressUtil
{
	/// <summary>
	/// Provides a progressbar for FPASS
	/// </summary>
	public class FrmProgress : System.Windows.Forms.Form
	{
		#region Members

		private System.Windows.Forms.ProgressBar pbMain;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#endregion Members

		#region Constructors

		public FrmProgress()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			// set the status of the window
			this.Tag = "closed";
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

		#endregion Constructors

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.pbMain = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // pbMain
            // 
            this.pbMain.Location = new System.Drawing.Point(0, 0);
            this.pbMain.Name = "pbMain";
            this.pbMain.Size = new System.Drawing.Size(416, 48);
            this.pbMain.Step = 1;
            this.pbMain.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbMain.TabIndex = 1;
            // 
            // FrmProgress
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(416, 43);
            this.ControlBox = false;
            this.Controls.Add(this.pbMain);
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmProgress";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Closing += new System.ComponentModel.CancelEventHandler(this.FrmProgress_Closing);
            this.ResumeLayout(false);

		}
		#endregion

		#region Methods

		public void Open(string prmTitle, int prmMax, int prmStep)
		{
			// title
			if (prmTitle.Equals(""))
				this.Text = "Daten werden verarbeitet...";
			else
				this.Text = prmTitle;
			// maximum bar length
			if (0 > prmMax)
				this.pbMain.Maximum = 0;
			else
				this.pbMain.Maximum = prmMax;
			// progress step
			if ( (0 > prmStep) || (prmMax < prmStep))
				this.pbMain.Step = 1;
			else
				this.pbMain.Step = prmStep;
			//
			this.pbMain.Value = 0;
			this.Tag = "open"; // set the status of the window again
			this.Show();
		} // Open()


		public bool IsOpen()
		{
			if (this.Tag.Equals("open"))
			{
				return true;
			}
			else
			{
				return false;
			}
		} // IsOpen()


		public void PerformStep()
		{
			this.pbMain.PerformStep();
		} // PerformStep()


		public void Complete()
		{
			// fills the progress bar to its maximum
			int restValue = this.pbMain.Maximum - this.pbMain.Value;
			this.pbMain.Increment(restValue);
		} // Complete()
		
		#endregion Methods


		private void FrmProgress_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			this.Tag = "closed"; // set the status of the window again
		}

	} // FrmProgress
} // Degussa.FPASS.Util.ProgressUtil

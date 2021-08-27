using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Degussa.FPASS.Reports.UserControls
{
	/// <summary>
	/// Summary description for UCReportPlant.
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
	public class UCReportPlant : System.Windows.Forms.UserControl
	{
		internal System.Windows.Forms.DataGrid DgrReportPlant;
		internal System.Windows.Forms.DataGridTableStyle DgrTablePlantManager;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxCWPLID;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxSurname;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxFirstname;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxPlant;

		#region Members

		/// <summary>
		/// holds the id of the current business object selected in the displayed table
		/// </summary>
		private decimal mPlantID = -1;
		/// <summary>
		/// 25.02.2004: Used to determine if grid being sorted, don't fire event CellChanged
		/// </summary>
		private bool mGridIsSorted = false;

		#endregion //End of Members

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public UCReportPlant()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();


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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.DgrReportPlant = new System.Windows.Forms.DataGrid();
            this.DgrTablePlantManager = new System.Windows.Forms.DataGridTableStyle();
            this.DgrTextBoxCWPLID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxPlant = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxSurname = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxFirstname = new System.Windows.Forms.DataGridTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DgrReportPlant)).BeginInit();
            this.SuspendLayout();
            // 
            // DgrReportPlant
            // 
            this.DgrReportPlant.CaptionBackColor = System.Drawing.Color.SteelBlue;
            this.DgrReportPlant.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DgrReportPlant.CaptionText = "Betriebe mit zugehörigem Meister";
            this.DgrReportPlant.DataMember = "";
            this.DgrReportPlant.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.DgrReportPlant.Location = new System.Drawing.Point(0, 0);
            this.DgrReportPlant.Name = "DgrReportPlant";
            this.DgrReportPlant.ReadOnly = true;
            this.DgrReportPlant.Size = new System.Drawing.Size(1258, 425);
            this.DgrReportPlant.TabIndex = 0;
            this.DgrReportPlant.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.DgrTablePlantManager});
            this.DgrReportPlant.TabStop = false;
            this.DgrReportPlant.CurrentCellChanged += new System.EventHandler(this.DgrReportPlant_CurrentCellChanged);
            this.DgrReportPlant.Enter += new System.EventHandler(this.DgrReportPlant_Enter);
            this.DgrReportPlant.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DgrReportPlant_MouseDown);
            // 
            // DgrTablePlantManager
            // 
            this.DgrTablePlantManager.DataGrid = this.DgrReportPlant;
            this.DgrTablePlantManager.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.DgrTextBoxCWPLID,
            this.DgrTextBoxPlant,
            this.DgrTextBoxSurname,
            this.DgrTextBoxFirstname});
            this.DgrTablePlantManager.HeaderFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DgrTablePlantManager.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.DgrTablePlantManager.MappingName = "RTTabPlantMan";
            // 
            // DgrTextBoxCWPLID
            // 
            this.DgrTextBoxCWPLID.Format = "";
            this.DgrTextBoxCWPLID.FormatInfo = null;
            this.DgrTextBoxCWPLID.HeaderText = "USPLID";
            this.DgrTextBoxCWPLID.MappingName = "USPLID";
            this.DgrTextBoxCWPLID.NullText = "";
            this.DgrTextBoxCWPLID.Width = 1;
            // 
            // DgrTextBoxPlant
            // 
            this.DgrTextBoxPlant.Format = "";
            this.DgrTextBoxPlant.FormatInfo = null;
            this.DgrTextBoxPlant.HeaderText = "Betrieb";
            this.DgrTextBoxPlant.MappingName = "Plant";
            this.DgrTextBoxPlant.NullText = "";
            this.DgrTextBoxPlant.Width = 300;
            // 
            // DgrTextBoxSurname
            // 
            this.DgrTextBoxSurname.Format = "";
            this.DgrTextBoxSurname.FormatInfo = null;
            this.DgrTextBoxSurname.HeaderText = "Nachname";
            this.DgrTextBoxSurname.MappingName = "Name";
            this.DgrTextBoxSurname.NullText = "";
            this.DgrTextBoxSurname.Width = 330;
            // 
            // DgrTextBoxFirstname
            // 
            this.DgrTextBoxFirstname.Format = "";
            this.DgrTextBoxFirstname.FormatInfo = null;
            this.DgrTextBoxFirstname.HeaderText = "Vorname";
            this.DgrTextBoxFirstname.MappingName = "Firstname";
            this.DgrTextBoxFirstname.NullText = "";
            this.DgrTextBoxFirstname.Width = 330;
            // 
            // UCReportPlant
            // 
            this.Controls.Add(this.DgrReportPlant);
            this.Name = "UCReportPlant";
            this.Size = new System.Drawing.Size(1246, 420);
            ((System.ComponentModel.ISupportInitialize)(this.DgrReportPlant)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		#region Accessors 

		public decimal PlantID
		{
			get 
			{
				return mPlantID;
			}
			set 
			{
				mPlantID = value;
			}
		}

		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// Gets ID of current plant record
		/// </summary>
		private void TableNavigated()
		{
			int rowIndex = this.DgrReportPlant.CurrentRowIndex;
			if(-1 < rowIndex)
			{
				mPlantID = Convert.ToDecimal(this.DgrReportPlant[rowIndex, 0].ToString());
			}
		}

		#endregion // End of Methods

		#region Events

		/// <summary>
		/// Get ID of current record if datagrid entered and only one record
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DgrReportPlant_Enter(object sender, System.EventArgs e)
		{
			if (this.DgrReportPlant.CurrentRowIndex == 0)
			{
				this.TableNavigated();
			}
		}

		/// <summary>
		/// Get ID of current record if datagrid cell changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DgrReportPlant_CurrentCellChanged(object sender, System.EventArgs e)
		{
			if ( !mGridIsSorted )
			{
				if ( this.DgrReportPlant.VisibleRowCount > 1 )
				{
					this.TableNavigated();
				}
			}
		}
		
		/// <summary>
		/// New 25.02.2004
		/// Discard current Plant ID when datagrid is sorted (click on column header) and send pointer to first record
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DgrReportPlant_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			mGridIsSorted = true;
			if ( this.DgrReportPlant.VisibleRowCount > 1 )
			{
				DataGrid.HitTestInfo hti  = DgrReportPlant.HitTest(e.X, e.Y);
				DataGrid.HitTestType type = hti.Type;
				switch(type)       
				{         
					case System.Windows.Forms.DataGrid.HitTestType.ColumnHeader:
						this.DgrReportPlant.CurrentRowIndex = 0;
						mPlantID = -1;
						break;				
				}
			}
			mGridIsSorted = false;			
		}

		#endregion // End of Events
		

	}
}

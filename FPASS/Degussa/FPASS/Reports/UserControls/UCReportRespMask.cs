using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Degussa.FPASS.Reports.UserControls
{
	/// <summary>
	/// Summary description for UCReportRespMask.
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
	///			<td width="60%">comment</td>
	///		</tr>
	/// </table>
	/// </div>
	/// </remarks>
	public class UCReportRespMask : System.Windows.Forms.UserControl
	{
		internal System.Windows.Forms.DataGrid DgrReportRepMask;
	    internal System.Windows.Forms.DataGridTableStyle DgrTableStyleRespMask;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxCwrID;
        internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxMaskNo;
        internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxMaskSystem;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxMaskReceived;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxMaskDelivered;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxFFMA;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxCoordinator;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxTelCoordinator;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxExContractor;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxTelExContractor;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxMaskService;

		#region Members
		/// <summary>
		/// holds the id of the current business object selected in the displayed table
		/// </summary>
		private decimal mCwrID = -1;
		/// <summary>
		/// 25.02.2004: Used to determine if grid being sorted, don't fire event CellChanged
		/// </summary>
		private bool mGridIsSorted = false;
		#endregion //End of Members

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public UCReportRespMask()
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
            this.DgrReportRepMask = new System.Windows.Forms.DataGrid();
            this.DgrTableStyleRespMask = new System.Windows.Forms.DataGridTableStyle();
            this.DgrTextBoxCwrID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxMaskNo = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxMaskSystem = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxMaskReceived = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxMaskDelivered = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxFFMA = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxCoordinator = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxTelCoordinator = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxExContractor = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxTelExContractor = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxMaskService = new System.Windows.Forms.DataGridTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DgrReportRepMask)).BeginInit();
            this.SuspendLayout();
            // 
            // DgrReportRepMask
            // 
            this.DgrReportRepMask.CaptionBackColor = System.Drawing.Color.SteelBlue;
            this.DgrReportRepMask.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DgrReportRepMask.CaptionText = "Verliehene + abgegebene Atemschutzmasken";
            this.DgrReportRepMask.DataMember = "";
            this.DgrReportRepMask.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DgrReportRepMask.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DgrReportRepMask.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.DgrReportRepMask.Location = new System.Drawing.Point(0, 0);
            this.DgrReportRepMask.Name = "DgrReportRepMask";
            this.DgrReportRepMask.ReadOnly = true;
            this.DgrReportRepMask.Size = new System.Drawing.Size(1258, 425);
            this.DgrReportRepMask.TabIndex = 0;
            this.DgrReportRepMask.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.DgrTableStyleRespMask});
            this.DgrReportRepMask.CurrentCellChanged += new System.EventHandler(this.DgrReportRepMask_CurrentCellChanged);
            this.DgrReportRepMask.DataSourceChanged += new System.EventHandler(this.DgrReportRepMask_DataSourceChanged);
            this.DgrReportRepMask.Enter += new System.EventHandler(this.DgrReportRepMask_Enter);
            // 
            // DgrTableStyleRespMask
            // 
            this.DgrTableStyleRespMask.DataGrid = this.DgrReportRepMask;
            this.DgrTableStyleRespMask.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.DgrTextBoxCwrID,
            this.DgrTextBoxMaskNo,
            this.DgrTextBoxMaskSystem,
            this.DgrTextBoxMaskReceived,
            this.DgrTextBoxMaskDelivered,
            this.DgrTextBoxFFMA,
            this.DgrTextBoxCoordinator,
            this.DgrTextBoxTelCoordinator,
            this.DgrTextBoxExContractor,
            this.DgrTextBoxTelExContractor,
            this.DgrTextBoxMaskService});
            this.DgrTableStyleRespMask.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DgrTableStyleRespMask.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.DgrTableStyleRespMask.MappingName = "RTTabRespMask";
            this.DgrTableStyleRespMask.ReadOnly = true;
            // 
            // DgrTextBoxCwrID
            // 
            this.DgrTextBoxCwrID.Format = "";
            this.DgrTextBoxCwrID.FormatInfo = null;
            this.DgrTextBoxCwrID.HeaderText = "CoworkerID";
            this.DgrTextBoxCwrID.MappingName = "CwrID";
            this.DgrTextBoxCwrID.NullText = "";
            this.DgrTextBoxCwrID.ReadOnly = true;
            this.DgrTextBoxCwrID.Width = 1;
            // 
            // DgrTextBoxMaskNo
            // 
            this.DgrTextBoxMaskNo.Format = "";
            this.DgrTextBoxMaskNo.FormatInfo = null;
            this.DgrTextBoxMaskNo.HeaderText = "Masken-Nr";
            this.DgrTextBoxMaskNo.MappingName = "MaskNo";
            this.DgrTextBoxMaskNo.NullText = "";
            this.DgrTextBoxMaskNo.ReadOnly = true;
            this.DgrTextBoxMaskNo.Width = 85;
            // 
            // DgrTextBoxMaskSystem
            // 
            this.DgrTextBoxMaskSystem.Format = "";
            this.DgrTextBoxMaskSystem.FormatInfo = null;
            this.DgrTextBoxMaskSystem.HeaderText = "System";
            this.DgrTextBoxMaskSystem.MappingName = "MaskSystem";
            this.DgrTextBoxMaskSystem.NullText = "";
            this.DgrTextBoxMaskSystem.ReadOnly = true;
            this.DgrTextBoxMaskSystem.Width = 65;
            // 
            // DgrTextBoxMaskReceived
            // 
            this.DgrTextBoxMaskReceived.Format = "";
            this.DgrTextBoxMaskReceived.FormatInfo = null;
            this.DgrTextBoxMaskReceived.HeaderText = "Verliehen am";
            this.DgrTextBoxMaskReceived.MappingName = "MaskReceived";
            this.DgrTextBoxMaskReceived.NullText = "";
            this.DgrTextBoxMaskReceived.ReadOnly = true;
            this.DgrTextBoxMaskReceived.Width = 110;
            // 
            // DgrTextBoxMaskDelivered
            // 
            this.DgrTextBoxMaskDelivered.Format = "";
            this.DgrTextBoxMaskDelivered.FormatInfo = null;
            this.DgrTextBoxMaskDelivered.HeaderText = "Abgegeben am";
            this.DgrTextBoxMaskDelivered.MappingName = "MaskDelivered";
            this.DgrTextBoxMaskDelivered.NullText = "";
            this.DgrTextBoxMaskDelivered.ReadOnly = true;
            this.DgrTextBoxMaskDelivered.Width = 110;
            // 
            // DgrTextBoxFFMA
            // 
            this.DgrTextBoxFFMA.Format = "";
            this.DgrTextBoxFFMA.FormatInfo = null;
            this.DgrTextBoxFFMA.HeaderText = "Fremdfirmenmitarbeieter";
            this.DgrTextBoxFFMA.MappingName = "FFMA";
            this.DgrTextBoxFFMA.NullText = "";
            this.DgrTextBoxFFMA.ReadOnly = true;
            this.DgrTextBoxFFMA.Width = 200;
            // 
            // DgrTextBoxCoordinator
            // 
            this.DgrTextBoxCoordinator.Format = "";
            this.DgrTextBoxCoordinator.FormatInfo = null;
            this.DgrTextBoxCoordinator.HeaderText = "Koordinator";
            this.DgrTextBoxCoordinator.MappingName = "Coordinator";
            this.DgrTextBoxCoordinator.NullText = "";
            this.DgrTextBoxCoordinator.ReadOnly = true;
            this.DgrTextBoxCoordinator.Width = 170;
            // 
            // DgrTextBoxTelCoordinator
            // 
            this.DgrTextBoxTelCoordinator.Format = "";
            this.DgrTextBoxTelCoordinator.FormatInfo = null;
            this.DgrTextBoxTelCoordinator.HeaderText = "Tel. Koordinator";
            this.DgrTextBoxTelCoordinator.MappingName = "TelCoordinator";
            this.DgrTextBoxTelCoordinator.NullText = "";
            this.DgrTextBoxTelCoordinator.ReadOnly = true;
            this.DgrTextBoxTelCoordinator.Width = 90;
            // 
            // DgrTextBoxExContractor
            // 
            this.DgrTextBoxExContractor.Format = "";
            this.DgrTextBoxExContractor.FormatInfo = null;
            this.DgrTextBoxExContractor.HeaderText = "Fremdfirma";
            this.DgrTextBoxExContractor.MappingName = "ExContractor";
            this.DgrTextBoxExContractor.NullText = "";
            this.DgrTextBoxExContractor.ReadOnly = true;
            this.DgrTextBoxExContractor.Width = 180;
            // 
            // DgrTextBoxTelExContractor
            // 
            this.DgrTextBoxTelExContractor.Format = "";
            this.DgrTextBoxTelExContractor.FormatInfo = null;
            this.DgrTextBoxTelExContractor.HeaderText = "Tel. Fremdfirma";
            this.DgrTextBoxTelExContractor.MappingName = "TelExContractor";
            this.DgrTextBoxTelExContractor.NullText = "";
            this.DgrTextBoxTelExContractor.ReadOnly = true;
            this.DgrTextBoxTelExContractor.Width = 120;
            // 
            // DgrTextBoxMaskService
            // 
            this.DgrTextBoxMaskService.Format = "";
            this.DgrTextBoxMaskService.FormatInfo = null;
            this.DgrTextBoxMaskService.HeaderText = "Wart.Datum";
            this.DgrTextBoxMaskService.MappingName = "MaskService";
            this.DgrTextBoxMaskService.NullText = "";
            this.DgrTextBoxMaskService.ReadOnly = true;
            this.DgrTextBoxMaskService.Width = 85;
            // 
            // UCReportRespMask
            // 
            this.Controls.Add(this.DgrReportRepMask);
            this.Name = "UCReportRespMask";
            this.Size = new System.Drawing.Size(1246, 420);
            ((System.ComponentModel.ISupportInitialize)(this.DgrReportRepMask)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		#region Accessors 

		#endregion //End of Accessors

		#region Methods 

		private void TableNavigated()
		{
			int rowIndex = this.DgrReportRepMask.CurrentRowIndex;
			if(-1 < rowIndex)
			{
				mCwrID = Convert.ToDecimal(this.DgrReportRepMask[rowIndex, 0].ToString());
			}
		}

		#endregion // End of Methods

		#region Events

		private void DgrReportRepMask_Enter(object sender, System.EventArgs e)
		{
			if ( this.DgrReportRepMask.CurrentRowIndex == 0 )
			{
				this.TableNavigated();
			}
			
		}

		private void DgrReportRepMask_CurrentCellChanged(object sender, System.EventArgs e)
		{	
			if ( !mGridIsSorted )
			{
				if ( this.DgrReportRepMask.VisibleRowCount > 1 )
				{
					this.TableNavigated();
				}
			}
			
		}

		/// <summary>
		/// 29.04.04 Rymar
		/// variable must be initialized after each change of the data source
		/// to force the user to select a Coworker everytime he selects a new report and searches for data
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DgrReportRepMask_DataSourceChanged(object sender, System.EventArgs e)
		{
			mCwrID = -1;
		}

		
		/// <summary>
		/// New 29.04.04 Rymar
		/// Discard current coworker ID when datagrid is sorted and send pointer to first record
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DgrReportRepMask_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			mGridIsSorted = true;
			if ( this.DgrReportRepMask.VisibleRowCount > 1 )
			{
				DataGrid.HitTestInfo hti  = DgrReportRepMask.HitTest(e.X, e.Y);
				DataGrid.HitTestType type = hti.Type;
				switch(type)       
				{         
					case System.Windows.Forms.DataGrid.HitTestType.ColumnHeader:
						this.DgrReportRepMask.CurrentRowIndex = 0;
						mCwrID = -1;
						break;				
				}
			}
			mGridIsSorted = false;	
		}
	
		#endregion // End of Events

	}
}

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Degussa.FPASS.Reports.UserControls
{
	/// <summary>
	/// Summary description for UCReportCoWorker.
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
	public class UCReportCoWorker : UserControl
	{
		internal DataGrid DgrReportCoWorker;
		internal DataGridTableStyle DgrTableStyleCoWorker;
		internal DataGridTextBoxColumn DgrTextBoxFFMAID;
		internal DataGridTextBoxColumn DgrTextBoxStatus;
		internal DataGridTextBoxColumn DgrTextBoxAccess;
		internal DataGridTextBoxColumn DgrTextBoxSurname;
		internal DataGridTextBoxColumn DgrTextBoxFirstname;
		internal DataGridTextBoxColumn DgrTextBoxDateOfBirth;
		private DataGridTextBoxColumn DgrTextBoxExContractor;
		internal DataGridTextBoxColumn DgrTextBoxSubcontractor;
		internal DataGridTextBoxColumn DgrTextBoxSurnameCoord;
		internal DataGridTextBoxColumn DgrTextBoxSupervisor;
		internal DataGridTextBoxColumn DgrTextBoxReturncode;
		
		#region Members
		
		/// <summary>
		/// holds id of current coworker BO selected in grid
		/// </summary>
		private decimal mCoWorkerID = -1;

		/// <summary>
		/// Used to determine if grid being sorted, don't fire event CellChanged
		/// </summary>
		private bool mGridIsSorted = false;
		

		#endregion //End of Members

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		public UCReportCoWorker()
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
            this.DgrReportCoWorker = new System.Windows.Forms.DataGrid();
            this.DgrTableStyleCoWorker = new System.Windows.Forms.DataGridTableStyle();
            this.DgrTextBoxFFMAID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxStatus = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxAccess = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxSurname = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxFirstname = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxDateOfBirth = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxExContractor = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxSubcontractor = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxSurnameCoord = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxSupervisor = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxReturncode = new System.Windows.Forms.DataGridTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DgrReportCoWorker)).BeginInit();
            this.SuspendLayout();
            // 
            // DgrReportCoWorker
            // 
            this.DgrReportCoWorker.CaptionBackColor = System.Drawing.Color.SteelBlue;
            this.DgrReportCoWorker.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DgrReportCoWorker.CaptionText = "Fremdfirmenmitarbeiter";
            this.DgrReportCoWorker.DataMember = "";
            this.DgrReportCoWorker.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.DgrReportCoWorker.Location = new System.Drawing.Point(0, 0);
            this.DgrReportCoWorker.Name = "DgrReportCoWorker";
            this.DgrReportCoWorker.ReadOnly = true;
            this.DgrReportCoWorker.Size = new System.Drawing.Size(1258, 425);
            this.DgrReportCoWorker.TabIndex = 0;
            this.DgrReportCoWorker.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.DgrTableStyleCoWorker});
            this.DgrReportCoWorker.TabStop = false;
            this.DgrReportCoWorker.CurrentCellChanged += new System.EventHandler(this.DgrReportCoWorker_CurrentCellChanged);
            this.DgrReportCoWorker.DataSourceChanged += new System.EventHandler(this.DgrReportCoWorker_DataSourceChanged);
            this.DgrReportCoWorker.Enter += new System.EventHandler(this.DgrReportCoWorker_Enter);
            this.DgrReportCoWorker.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DgrReportCoWorker_MouseDown);
            // 
            // DgrTableStyleCoWorker
            // 
            this.DgrTableStyleCoWorker.DataGrid = this.DgrReportCoWorker;
            this.DgrTableStyleCoWorker.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.DgrTextBoxFFMAID,
            this.DgrTextBoxStatus,
            this.DgrTextBoxAccess,
            this.DgrTextBoxSurname,
            this.DgrTextBoxFirstname,
            this.DgrTextBoxDateOfBirth,
            this.DgrTextBoxExContractor,
            this.DgrTextBoxSubcontractor,
            this.DgrTextBoxSurnameCoord,
            this.DgrTextBoxSupervisor,
            this.DgrTextBoxReturncode});
            this.DgrTableStyleCoWorker.HeaderFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DgrTableStyleCoWorker.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.DgrTableStyleCoWorker.MappingName = "RTTabCoWorker";
            // 
            // DgrTextBoxFFMAID
            // 
            this.DgrTextBoxFFMAID.Format = "";
            this.DgrTextBoxFFMAID.FormatInfo = null;
            this.DgrTextBoxFFMAID.HeaderText = "FFMAID";
            this.DgrTextBoxFFMAID.MappingName = "FFMAID";
            this.DgrTextBoxFFMAID.NullText = "";
            this.DgrTextBoxFFMAID.Width = 1;
            // 
            // DgrTextBoxStatus
            // 
            this.DgrTextBoxStatus.Format = "";
            this.DgrTextBoxStatus.FormatInfo = null;
            this.DgrTextBoxStatus.HeaderText = "Status";
            this.DgrTextBoxStatus.MappingName = "Status";
            this.DgrTextBoxStatus.NullText = "";
            this.DgrTextBoxStatus.Width = 70;
            // 
            // DgrTextBoxAccess
            // 
            this.DgrTextBoxAccess.Format = "";
            this.DgrTextBoxAccess.FormatInfo = null;
            this.DgrTextBoxAccess.HeaderText = "Zutritt";
            this.DgrTextBoxAccess.MappingName = "Access";
            this.DgrTextBoxAccess.NullText = "";
            this.DgrTextBoxAccess.Width = 60;
            // 
            // DgrTextBoxSurname
            // 
            this.DgrTextBoxSurname.Format = "";
            this.DgrTextBoxSurname.FormatInfo = null;
            this.DgrTextBoxSurname.HeaderText = "Nachname";
            this.DgrTextBoxSurname.MappingName = "Surname";
            this.DgrTextBoxSurname.NullText = "";
            this.DgrTextBoxSurname.Width = 90;
            // 
            // DgrTextBoxFirstname
            // 
            this.DgrTextBoxFirstname.Format = "";
            this.DgrTextBoxFirstname.FormatInfo = null;
            this.DgrTextBoxFirstname.HeaderText = "Vorname";
            this.DgrTextBoxFirstname.MappingName = "FirstName";
            this.DgrTextBoxFirstname.NullText = "";
            this.DgrTextBoxFirstname.Width = 90;
            // 
            // DgrTextBoxDateOfBirth
            // 
            this.DgrTextBoxDateOfBirth.Format = "";
            this.DgrTextBoxDateOfBirth.FormatInfo = null;
            this.DgrTextBoxDateOfBirth.HeaderText = "Geburtsdatum";
            this.DgrTextBoxDateOfBirth.MappingName = "DateOfBirth";
            this.DgrTextBoxDateOfBirth.NullText = "";
            this.DgrTextBoxDateOfBirth.Width = 85;
            // 
            // DgrTextBoxExContractor
            // 
            this.DgrTextBoxExContractor.Format = "";
            this.DgrTextBoxExContractor.FormatInfo = null;
            this.DgrTextBoxExContractor.HeaderText = "Fremdfirma";
            this.DgrTextBoxExContractor.MappingName = "ExContractorName";
            this.DgrTextBoxExContractor.NullText = "";
            this.DgrTextBoxExContractor.Width = 200;
            // 
            // DgrTextBoxSubcontractor
            // 
            this.DgrTextBoxSubcontractor.Format = "";
            this.DgrTextBoxSubcontractor.FormatInfo = null;
            this.DgrTextBoxSubcontractor.HeaderText = "Subfirma";
            this.DgrTextBoxSubcontractor.MappingName = "SubContractor";
            this.DgrTextBoxSubcontractor.NullText = "";
            this.DgrTextBoxSubcontractor.Width = 190;
            // 
            // DgrTextBoxSurnameCoord
            // 
            this.DgrTextBoxSurnameCoord.Format = "";
            this.DgrTextBoxSurnameCoord.FormatInfo = null;
            this.DgrTextBoxSurnameCoord.HeaderText = "Koordinator";
            this.DgrTextBoxSurnameCoord.MappingName = "CoordNameAndTel";
            this.DgrTextBoxSurnameCoord.NullText = "";
            this.DgrTextBoxSurnameCoord.Width = 200;
            // 
            // DgrTextBoxSupervisor
            // 
            this.DgrTextBoxSupervisor.Format = "";
            this.DgrTextBoxSupervisor.FormatInfo = null;
            this.DgrTextBoxSupervisor.HeaderText = "Baustellenleiter";
            this.DgrTextBoxSupervisor.MappingName = "SuperNameAndTel";
            this.DgrTextBoxSupervisor.NullText = "";
            this.DgrTextBoxSupervisor.Width = 195;
            // 
            // DgrTextBoxReturncode
            // 
            this.DgrTextBoxReturncode.Format = "";
            this.DgrTextBoxReturncode.FormatInfo = null;
            this.DgrTextBoxReturncode.HeaderText = "ZKS";
            this.DgrTextBoxReturncode.MappingName = "ZKSReturncode";
            this.DgrTextBoxReturncode.NullText = "";
            this.DgrTextBoxReturncode.Width = 30;
            // 
            // UCReportCoWorker
            // 
            this.Controls.Add(this.DgrReportCoWorker);
            this.Name = "UCReportCoWorker";
            this.Size = new System.Drawing.Size(1246, 420);
            ((System.ComponentModel.ISupportInitialize)(this.DgrReportCoWorker)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		
		#region Accessors 

		/// <summary>
		/// Returns or sets PK ID of coworker currently selected in grid.
		/// </summary>
		public decimal CoWorkerID
		{
			get 
			{
				return mCoWorkerID;
			}
			set 
			{
				mCoWorkerID = value;
			}
		}

		#endregion

		#region Methods 

		/// <summary>
		/// Gets PK ID and surname of currently selected coworker
		/// </summary>
		private void TableNavigated()
		{
			int rowIndex = DgrReportCoWorker.CurrentRowIndex;
			if(-1 < rowIndex)
			{
				mCoWorkerID = Convert.ToDecimal(DgrReportCoWorker[rowIndex, 0].ToString());
				//mCoWorkerSurname = DgrReportCoWorker[rowIndex, 1].ToString();
			}
		}

		#endregion // End of Methods

		#region Events

		private void DgrReportCoWorker_Enter(object sender, EventArgs e)
		{
			if (DgrReportCoWorker.CurrentRowIndex == 0)
			{
				TableNavigated();
			}
			
		}

		private void DgrReportCoWorker_CurrentCellChanged(object sender, EventArgs e)
		{	
			if ( !mGridIsSorted )
			{
				if (DgrReportCoWorker.VisibleRowCount > 1)
				{
					TableNavigated();
				}
			}
			
		}

		/// <summary>
		/// 27.11.03 Bossu
		/// variable must be initialized after each change of the data source
		/// to force the user to select a Coworker everytime he selects a new report and searches for data
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DgrReportCoWorker_DataSourceChanged(object sender, EventArgs e)
		{
			mCoWorkerID = -1;
		}

		
		/// <summary>
		/// Discards current coworker ID when datagrid is sorted and send pointer to first record
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DgrReportCoWorker_MouseDown(object sender, MouseEventArgs e)
		{
			mGridIsSorted = true;
			if ( DgrReportCoWorker.VisibleRowCount > 1 )
			{
				DataGrid.HitTestInfo hti  = DgrReportCoWorker.HitTest(e.X, e.Y);
				DataGrid.HitTestType type = hti.Type;
				switch(type)       
				{         
					case DataGrid.HitTestType.ColumnHeader:
						DgrReportCoWorker.CurrentRowIndex = 0;
						mCoWorkerID = -1;
						break;				
				}
			}
			mGridIsSorted = false;	
		}
	
		#endregion // End of Events
	}
}

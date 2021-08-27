using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Degussa.FPASS.Reports.UserControls
{
	/// <summary>
	/// Summary description for UCReportExContractor.
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
	public class UCReportExContractor : System.Windows.Forms.UserControl
	{
        #region Members

		internal System.Windows.Forms.DataGrid DgrReportExContractor;
		internal System.Windows.Forms.DataGridTableStyle DgrTableExContractor;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxExContractor;
        internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxDebitNo;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxCoordinator;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxExcoid;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxEcodid;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxSupervisor;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxSubContractor;

		/// <summary>
		/// holds the id of the current business object selected in the displayed table
		/// </summary>
		private decimal mExContractorID = -1;
		/// <summary>
		/// 25.02.2004: Used to determine if grid being sorted, don't fire event CellChanged
		/// </summary>
		private bool mGridIsSorted = false;

		#endregion //End of Members

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        /// <summary>
        /// Default constructor
        /// </summary>
		public UCReportExContractor()
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
            this.DgrReportExContractor = new System.Windows.Forms.DataGrid();
            this.DgrTableExContractor = new System.Windows.Forms.DataGridTableStyle();
            this.DgrTextBoxEcodid = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxExcoid = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxExContractor = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxDebitNo = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxCoordinator = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxSupervisor = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxSubContractor = new System.Windows.Forms.DataGridTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DgrReportExContractor)).BeginInit();
            this.SuspendLayout();
            // 
            // DgrReportExContractor
            // 
            this.DgrReportExContractor.CaptionBackColor = System.Drawing.Color.SteelBlue;
            this.DgrReportExContractor.CaptionText = "Fremdfirmen mit zuständigen Koordinatoren";
            this.DgrReportExContractor.DataMember = "";
            this.DgrReportExContractor.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.DgrReportExContractor.Location = new System.Drawing.Point(0, 0);
            this.DgrReportExContractor.Name = "DgrReportExContractor";
            this.DgrReportExContractor.ReadOnly = true;
            this.DgrReportExContractor.Size = new System.Drawing.Size(1258, 425);
            this.DgrReportExContractor.TabIndex = 0;
            this.DgrReportExContractor.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.DgrTableExContractor});
            this.DgrReportExContractor.TabStop = false;
            this.DgrReportExContractor.CurrentCellChanged += new System.EventHandler(this.DgrReportExContractor_CurrentCellChanged);
            this.DgrReportExContractor.Enter += new System.EventHandler(this.DgrReportExContractor_Enter);
            this.DgrReportExContractor.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DgrReportExContractor_MouseDown);
            // 
            // DgrTableExContractor
            // 
            this.DgrTableExContractor.DataGrid = this.DgrReportExContractor;
            this.DgrTableExContractor.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.DgrTextBoxEcodid,
            this.DgrTextBoxExcoid,
            this.DgrTextBoxExContractor,
            this.DgrTextBoxDebitNo,
            this.DgrTextBoxCoordinator,
            this.DgrTextBoxSupervisor,
            this.DgrTextBoxSubContractor});
            this.DgrTableExContractor.HeaderFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DgrTableExContractor.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.DgrTableExContractor.MappingName = "RTTabExContractor";
            // 
            // DgrTextBoxEcodid
            // 
            this.DgrTextBoxEcodid.Format = "";
            this.DgrTextBoxEcodid.FormatInfo = null;
            this.DgrTextBoxEcodid.HeaderText = "ECODID";
            this.DgrTextBoxEcodid.MappingName = "ECODID";
            this.DgrTextBoxEcodid.NullText = "";
            this.DgrTextBoxEcodid.Width = 1;
            // 
            // DgrTextBoxExcoid
            // 
            this.DgrTextBoxExcoid.Format = "";
            this.DgrTextBoxExcoid.FormatInfo = null;
            this.DgrTextBoxExcoid.HeaderText = "EXCOID";
            this.DgrTextBoxExcoid.MappingName = "EXCOID";
            this.DgrTextBoxExcoid.NullText = "";
            this.DgrTextBoxExcoid.Width = 1;
            // 
            // DgrTextBoxExContractor
            // 
            this.DgrTextBoxExContractor.Format = "";
            this.DgrTextBoxExContractor.FormatInfo = null;
            this.DgrTextBoxExContractor.HeaderText = "Fremdfirma";
            this.DgrTextBoxExContractor.MappingName = "ExContractor";
            this.DgrTextBoxExContractor.NullText = "";
            this.DgrTextBoxExContractor.Width = 200;
            // 
            // DgrTextBoxDebitNo
            // 
            this.DgrTextBoxDebitNo.Format = "";
            this.DgrTextBoxDebitNo.FormatInfo = null;
            this.DgrTextBoxDebitNo.HeaderText = "Debit-Nr.";
            this.DgrTextBoxDebitNo.MappingName = "DebitNo";
            this.DgrTextBoxDebitNo.NullText = "";
            this.DgrTextBoxDebitNo.Width = 150;
            // 
            // DgrTextBoxCoordinator
            // 
            this.DgrTextBoxCoordinator.Format = "";
            this.DgrTextBoxCoordinator.FormatInfo = null;
            this.DgrTextBoxCoordinator.HeaderText = "Koordinator";
            this.DgrTextBoxCoordinator.MappingName = "Coordinator";
            this.DgrTextBoxCoordinator.NullText = "";
            this.DgrTextBoxCoordinator.Width = 200;
            // 
            // DgrTextBoxSupervisor
            // 
            this.DgrTextBoxSupervisor.Format = "";
            this.DgrTextBoxSupervisor.FormatInfo = null;
            this.DgrTextBoxSupervisor.HeaderText = "Baustellenleiter";
            this.DgrTextBoxSupervisor.MappingName = "Supervisor";
            this.DgrTextBoxSupervisor.NullText = "";
            this.DgrTextBoxSupervisor.Width = 200;
            // 
            // DgrTextBoxSubContractor
            // 
            this.DgrTextBoxSubContractor.Format = "";
            this.DgrTextBoxSubContractor.FormatInfo = null;
            this.DgrTextBoxSubContractor.HeaderText = "Subfirma";
            this.DgrTextBoxSubContractor.MappingName = "Subcontractor";
            this.DgrTextBoxSubContractor.NullText = "";
            this.DgrTextBoxSubContractor.Width = 200;
            // 
            // UCReportExContractor
            // 
            this.Controls.Add(this.DgrReportExContractor);
            this.Name = "UCReportExContractor";
            this.Size = new System.Drawing.Size(1246, 420);
            ((System.ComponentModel.ISupportInitialize)(this.DgrReportExContractor)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		#region Accessors 

		public decimal ExContractorID
		{
			get 
			{
				return mExContractorID;
			}
			set 
			{
				mExContractorID = value;
			}
		}

		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// Gets ID of current ExContractor record
		/// </summary>
		private void TableNavigated()
		{
			int rowIndex = this.DgrReportExContractor.CurrentRowIndex;
			if(-1 < rowIndex)
			{
				mExContractorID = Convert.ToDecimal(this.DgrReportExContractor[rowIndex, 0].ToString());
			}
		}

		#endregion // End of Methods

		#region Events

		/// <summary>
		/// Get ID of current record if datagrid entered and only one record
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DgrReportExContractor_Enter(object sender, System.EventArgs e)
		{
			if(this.DgrReportExContractor.CurrentRowIndex == 0)
			{
				this.TableNavigated();
			}
		}

		/// <summary>
		/// Get ID of current record if datagrid cell changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DgrReportExContractor_CurrentCellChanged(object sender, System.EventArgs e)
		{
			if ( !mGridIsSorted )
			{
				if (this.DgrReportExContractor.VisibleRowCount > 1)
				{
					this.TableNavigated();
				}
			}
		}

		/// <summary>
		/// New 25.02.2004
		/// Discard current Excontractor ID when datagrid is sorted (click on column header) and send pointer to first record
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DgrReportExContractor_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			mGridIsSorted = true;
			if ( this.DgrReportExContractor.VisibleRowCount > 1 )
			{
				DataGrid.HitTestInfo hti  = DgrReportExContractor.HitTest(e.X, e.Y);
				DataGrid.HitTestType type = hti.Type;
				switch(type)       
				{         
					case System.Windows.Forms.DataGrid.HitTestType.ColumnHeader:
						this.DgrReportExContractor.CurrentRowIndex = 0;
						mExContractorID = -1;
						break;				
				}
			}
			mGridIsSorted = false;	
		}

		
		#endregion // End of Events
		
	}
}

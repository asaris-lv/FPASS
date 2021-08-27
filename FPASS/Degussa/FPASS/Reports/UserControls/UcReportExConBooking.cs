using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Degussa.FPASS.Reports.UserControls
{
	/// <summary>
	/// Shows search results for reports to do with excontractor bookings.
	/// </summary>
	/// <remarks>
	/// <para><b>History</b></para>
	/// <div class="tablediv">
	/// <table class="dtTABLE" cellspacing="0">
	///		<tr>
	///			<th width="20%">PTA GmbH</th>
	///			<th width="20%">01/12/2003</th>
	///			<th width="60%">Remarks</th>
	///		</tr>
	///		<tr>
	///			<th width="20%">N. Mundy, PTA GmbH</th>
	///			<th width="20%">11/04/2008</th>
	///			<th width="60%">Updated for V4.5: new report Leistungsverrechnung</th>
	///		</tr>
	/// </table>
	/// </div>
	/// </remarks>
	public class UcReportExConBooking : UserControl
	{
		internal DataGrid DgrReportExContr;
		
		#region Members
		/// <summary>
		/// holds the id of the current business object selected in the displayed table
		/// </summary>
		private decimal mExContractorID = -1;
		/// <summary>
		/// 25.02.2004: Used to determine if grid being sorted, don't fire event CellChanged
		/// </summary>
		private bool mGridIsSorted = false;

		#endregion //End of Members

		internal DataGridTableStyle dataGridTableStyleExContr;
		internal DataGridTextBoxColumn DgrTextBoxExcoID;
		internal DataGridTextBoxColumn DgrTextBoxFremdfirma;
		internal DataGridTextBoxColumn DgrTextBoxSupervisor;
		internal DataGridTextBoxColumn DgrTextBoxTelephone;
		internal DataGridTextBoxColumn DgrTextBoxDebitNo;
		internal DataGridTextBoxColumn DgrTextBoxCity;
		internal DataGridTextBoxColumn DgrTextBoxPostcode;
		internal DataGridTextBoxColumn DgrTextBoxStreet;
		

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		public UcReportExConBooking()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

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
            this.DgrReportExContr = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyleExContr = new System.Windows.Forms.DataGridTableStyle();
            this.DgrTextBoxExcoID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxFremdfirma = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxDebitNo = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxSupervisor = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxTelephone = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxStreet = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxPostcode = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxCity = new System.Windows.Forms.DataGridTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DgrReportExContr)).BeginInit();
            this.SuspendLayout();
            // 
            // DgrReportExContr
            // 
            this.DgrReportExContr.CaptionBackColor = System.Drawing.Color.SteelBlue;
            this.DgrReportExContr.CaptionText = "Fremdfirmen";
            this.DgrReportExContr.DataMember = "";
            this.DgrReportExContr.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.DgrReportExContr.Location = new System.Drawing.Point(0, 0);
            this.DgrReportExContr.Name = "DgrReportExContr";
            this.DgrReportExContr.ReadOnly = true;
            this.DgrReportExContr.Size = new System.Drawing.Size(1258, 425);
            this.DgrReportExContr.TabIndex = 1;
            this.DgrReportExContr.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.dataGridTableStyleExContr});
            this.DgrReportExContr.TabStop = false;
            this.DgrReportExContr.CurrentCellChanged += new System.EventHandler(this.DgrReportExContractor_CurrentCellChanged);
            this.DgrReportExContr.Enter += new System.EventHandler(this.DgrReportExContractor_Enter);
            this.DgrReportExContr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DgrReportExContr_MouseDown);
            // 
            // dataGridTableStyleExContr
            // 
            this.dataGridTableStyleExContr.DataGrid = this.DgrReportExContr;
            this.dataGridTableStyleExContr.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.DgrTextBoxExcoID,
            this.DgrTextBoxFremdfirma,
            this.DgrTextBoxDebitNo,
            this.DgrTextBoxSupervisor,
            this.DgrTextBoxTelephone,
            this.DgrTextBoxStreet,
            this.DgrTextBoxPostcode,
            this.DgrTextBoxCity});
            this.dataGridTableStyleExContr.HeaderFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridTableStyleExContr.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGridTableStyleExContr.MappingName = "TableExConBooking";
            // 
            // DgrTextBoxExcoID
            // 
            this.DgrTextBoxExcoID.Format = "";
            this.DgrTextBoxExcoID.FormatInfo = null;
            this.DgrTextBoxExcoID.HeaderText = "Exco ID";
            this.DgrTextBoxExcoID.MappingName = "ExcoID";
            this.DgrTextBoxExcoID.NullText = "";
            this.DgrTextBoxExcoID.Width = 1;
            // 
            // DgrTextBoxFremdfirma
            // 
            this.DgrTextBoxFremdfirma.Format = "";
            this.DgrTextBoxFremdfirma.FormatInfo = null;
            this.DgrTextBoxFremdfirma.HeaderText = "Fremdfirma";
            this.DgrTextBoxFremdfirma.MappingName = "ExcoName";
            this.DgrTextBoxFremdfirma.NullText = "";
            this.DgrTextBoxFremdfirma.Width = 180;
            // 
            // DgrTextBoxDebitNo
            // 
            this.DgrTextBoxDebitNo.Format = "";
            this.DgrTextBoxDebitNo.FormatInfo = null;
            this.DgrTextBoxDebitNo.HeaderText = "Debit-Nr.";
            this.DgrTextBoxDebitNo.MappingName = "ExcoDebitNo";
            this.DgrTextBoxDebitNo.NullText = "";
            this.DgrTextBoxDebitNo.Width = 75;
            // 
            // DgrTextBoxSupervisor
            // 
            this.DgrTextBoxSupervisor.Format = "";
            this.DgrTextBoxSupervisor.FormatInfo = null;
            this.DgrTextBoxSupervisor.HeaderText = "Baustellenleiter";
            this.DgrTextBoxSupervisor.MappingName = "ExcoSupervisor";
            this.DgrTextBoxSupervisor.NullText = "";
            this.DgrTextBoxSupervisor.Width = 180;
            // 
            // DgrTextBoxTelephone
            // 
            this.DgrTextBoxTelephone.Format = "";
            this.DgrTextBoxTelephone.FormatInfo = null;
            this.DgrTextBoxTelephone.HeaderText = "Telefonnummer";
            this.DgrTextBoxTelephone.MappingName = "ExcoTelephone";
            this.DgrTextBoxTelephone.NullText = "";
            this.DgrTextBoxTelephone.Width = 75;
            // 
            // DgrTextBoxStreet
            // 
            this.DgrTextBoxStreet.Format = "";
            this.DgrTextBoxStreet.FormatInfo = null;
            this.DgrTextBoxStreet.HeaderText = "Straﬂe";
            this.DgrTextBoxStreet.MappingName = "ExcoStreet";
            this.DgrTextBoxStreet.NullText = "";
            this.DgrTextBoxStreet.Width = 180;
            // 
            // DgrTextBoxPostcode
            // 
            this.DgrTextBoxPostcode.Format = "";
            this.DgrTextBoxPostcode.FormatInfo = null;
            this.DgrTextBoxPostcode.HeaderText = "PLZ";
            this.DgrTextBoxPostcode.MappingName = "ExcoPostcode";
            this.DgrTextBoxPostcode.NullText = "";
            this.DgrTextBoxPostcode.Width = 90;
            // 
            // DgrTextBoxCity
            // 
            this.DgrTextBoxCity.Format = "";
            this.DgrTextBoxCity.FormatInfo = null;
            this.DgrTextBoxCity.HeaderText = "Stadt";
            this.DgrTextBoxCity.MappingName = "ExcoCity";
            this.DgrTextBoxCity.NullText = "";
            this.DgrTextBoxCity.Width = 120;
            // 
            // UcReportExConBooking
            // 
            this.Controls.Add(this.DgrReportExContr);
            this.Name = "UcReportExConBooking";
            this.Size = new System.Drawing.Size(1246, 420);
            ((System.ComponentModel.ISupportInitialize)(this.DgrReportExContr)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		#region Accessors 

		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// Gets ID of current ExContractor record
		/// </summary>
		private void TableNavigated()
		{
			int rowIndex = this.DgrReportExContr.CurrentRowIndex;
			if(-1 < rowIndex)
			{
				mExContractorID = Convert.ToDecimal(this.DgrReportExContr[rowIndex, 0].ToString());
			}
		}

		#endregion // End of Methods

		#region Events

		/// <summary>
		/// Get ID of current record if datagrid entered and only one record
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DgrReportExContractor_Enter(object sender, EventArgs e)
		{
			if(this.DgrReportExContr.CurrentRowIndex == 0)
			{
				this.TableNavigated();
			}
		}

		/// <summary>
		/// Get ID of current excontractor record if datagrid cell changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DgrReportExContractor_CurrentCellChanged(object sender, EventArgs e)
		{
			if ( !mGridIsSorted )
			{
				if (this.DgrReportExContr.VisibleRowCount > 1)
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
		private void DgrReportExContr_MouseDown(object sender, MouseEventArgs e)
		{
			mGridIsSorted = true;
			if ( this.DgrReportExContr.VisibleRowCount > 1 )
			{
				DataGrid.HitTestInfo hti  = DgrReportExContr.HitTest(e.X, e.Y);
				DataGrid.HitTestType type = hti.Type;
				switch(type)       
				{         
					case DataGrid.HitTestType.ColumnHeader:
						this.DgrReportExContr.CurrentRowIndex = 0;
						mExContractorID = -1;
						break;				
				}
			}
			mGridIsSorted = false;	
		}
		
		#endregion // End of Events
	}
}

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Degussa.FPASS.Reports.UserControls
{
	/// <summary>
	/// Summary description for UCReportChecklist.
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
	public class UCReportChecklist : System.Windows.Forms.UserControl
	{
		internal System.Windows.Forms.DataGrid DgrChecklist;
		internal System.Windows.Forms.DataGridTableStyle DgrTableStyleChechliste;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxCHLSID;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxTK;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxPersNo;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxSurname;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxFirstname;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxValidFrom;
		private System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxValidUntil;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxAuthorised;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxExcontractor;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxSubContractor;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxStatus;
		
		#region Members

		/// <summary>
		/// holds the id of the current business object selected in the displayed table
		/// </summary>
		private decimal mCheckListID = -1;
		/// <summary>
		/// 25.02.2004: Used to determine if grid being sorted, don't fire event CellChanged
		/// </summary>
		private bool mGridIsSorted = false;

		#endregion //End of Members

		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxIDCardNoFpass;
		internal System.Windows.Forms.DataGridTextBoxColumn DgrTextBoxIDCardNoZks;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public UCReportChecklist()
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
            this.DgrChecklist = new System.Windows.Forms.DataGrid();
            this.DgrTableStyleChechliste = new System.Windows.Forms.DataGridTableStyle();
            this.DgrTextBoxCHLSID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxStatus = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxTK = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxPersNo = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxIDCardNoFpass = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxIDCardNoZks = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxSurname = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxFirstname = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxValidFrom = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxValidUntil = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxAuthorised = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxExcontractor = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DgrTextBoxSubContractor = new System.Windows.Forms.DataGridTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DgrChecklist)).BeginInit();
            this.SuspendLayout();
            // 
            // DgrChecklist
            // 
            this.DgrChecklist.CaptionBackColor = System.Drawing.Color.SteelBlue;
            this.DgrChecklist.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DgrChecklist.CaptionText = "Fremdfirmenmitarbeiter (Vergleichsliste)";
            this.DgrChecklist.DataMember = "";
            this.DgrChecklist.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.DgrChecklist.Location = new System.Drawing.Point(0, 0);
            this.DgrChecklist.Name = "DgrChecklist";
            this.DgrChecklist.ReadOnly = true;
            this.DgrChecklist.Size = new System.Drawing.Size(1258, 425);
            this.DgrChecklist.TabIndex = 0;
            this.DgrChecklist.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.DgrTableStyleChechliste});
            this.DgrChecklist.TabStop = false;
            this.DgrChecklist.CurrentCellChanged += new System.EventHandler(this.DgrChecklist_CurrentCellChanged);
            this.DgrChecklist.Enter += new System.EventHandler(this.DgrChecklist_Enter);
            this.DgrChecklist.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DgrChecklist_MouseDown);
            // 
            // DgrTableStyleChechliste
            // 
            this.DgrTableStyleChechliste.DataGrid = this.DgrChecklist;
            this.DgrTableStyleChechliste.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.DgrTextBoxCHLSID,
            this.DgrTextBoxStatus,
            this.DgrTextBoxTK,
            this.DgrTextBoxPersNo,
            this.DgrTextBoxIDCardNoFpass,
            this.DgrTextBoxIDCardNoZks,
            this.DgrTextBoxSurname,
            this.DgrTextBoxFirstname,
            this.DgrTextBoxValidFrom,
            this.DgrTextBoxValidUntil,
            this.DgrTextBoxAuthorised,
            this.DgrTextBoxExcontractor,
            this.DgrTextBoxSubContractor});
            this.DgrTableStyleChechliste.HeaderFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DgrTableStyleChechliste.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.DgrTableStyleChechliste.MappingName = "RTTabCheckList";
            // 
            // DgrTextBoxCHLSID
            // 
            this.DgrTextBoxCHLSID.Format = "";
            this.DgrTextBoxCHLSID.FormatInfo = null;
            this.DgrTextBoxCHLSID.HeaderText = "CHLSID";
            this.DgrTextBoxCHLSID.MappingName = "CHLSID";
            this.DgrTextBoxCHLSID.NullText = "";
            this.DgrTextBoxCHLSID.Width = 1;
            // 
            // DgrTextBoxStatus
            // 
            this.DgrTextBoxStatus.Format = "";
            this.DgrTextBoxStatus.FormatInfo = null;
            this.DgrTextBoxStatus.HeaderText = "Status";
            this.DgrTextBoxStatus.MappingName = "Status";
            this.DgrTextBoxStatus.NullText = "";
            this.DgrTextBoxStatus.Width = 75;
            // 
            // DgrTextBoxTK
            // 
            this.DgrTextBoxTK.Format = "";
            this.DgrTextBoxTK.FormatInfo = null;
            this.DgrTextBoxTK.HeaderText = "TK";
            this.DgrTextBoxTK.MappingName = "TK";
            this.DgrTextBoxTK.NullText = "";
            this.DgrTextBoxTK.Width = 35;
            // 
            // DgrTextBoxPersNo
            // 
            this.DgrTextBoxPersNo.Format = "";
            this.DgrTextBoxPersNo.FormatInfo = null;
            this.DgrTextBoxPersNo.HeaderText = "Personalnr";
            this.DgrTextBoxPersNo.MappingName = "PersNo";
            this.DgrTextBoxPersNo.NullText = "";
            this.DgrTextBoxPersNo.Width = 75;
            // 
            // DgrTextBoxIDCardNoFpass
            // 
            this.DgrTextBoxIDCardNoFpass.Format = "";
            this.DgrTextBoxIDCardNoFpass.FormatInfo = null;
            this.DgrTextBoxIDCardNoFpass.HeaderText = "Mifare-Nr FPASS";
            this.DgrTextBoxIDCardNoFpass.MappingName = "MifareNoFpass";
            this.DgrTextBoxIDCardNoFpass.NullText = "";
            this.DgrTextBoxIDCardNoFpass.Width = 100;
            // 
            // DgrTextBoxIDCardNoZks
            // 
            this.DgrTextBoxIDCardNoZks.Format = "";
            this.DgrTextBoxIDCardNoZks.FormatInfo = null;
            this.DgrTextBoxIDCardNoZks.HeaderText = "Mifare-Nr ZKS";
            this.DgrTextBoxIDCardNoZks.MappingName = "MifareNoZks";
            this.DgrTextBoxIDCardNoZks.NullText = "";
            this.DgrTextBoxIDCardNoZks.Width = 100;
            // 
            // DgrTextBoxSurname
            // 
            this.DgrTextBoxSurname.Format = "";
            this.DgrTextBoxSurname.FormatInfo = null;
            this.DgrTextBoxSurname.HeaderText = "Nachname";
            this.DgrTextBoxSurname.MappingName = "Surname";
            this.DgrTextBoxSurname.NullText = "";
            this.DgrTextBoxSurname.Width = 150;
            // 
            // DgrTextBoxFirstname
            // 
            this.DgrTextBoxFirstname.Format = "";
            this.DgrTextBoxFirstname.FormatInfo = null;
            this.DgrTextBoxFirstname.HeaderText = "Vorname";
            this.DgrTextBoxFirstname.MappingName = "Firstname";
            this.DgrTextBoxFirstname.NullText = "";
            this.DgrTextBoxFirstname.Width = 150;
            // 
            // DgrTextBoxValidFrom
            // 
            this.DgrTextBoxValidFrom.Format = "";
            this.DgrTextBoxValidFrom.FormatInfo = null;
            this.DgrTextBoxValidFrom.HeaderText = "Gueltig Von";
            this.DgrTextBoxValidFrom.MappingName = "ValidFrom";
            this.DgrTextBoxValidFrom.NullText = "";
            this.DgrTextBoxValidFrom.Width = 75;
            // 
            // DgrTextBoxValidUntil
            // 
            this.DgrTextBoxValidUntil.Format = "";
            this.DgrTextBoxValidUntil.FormatInfo = null;
            this.DgrTextBoxValidUntil.HeaderText = "Gültig Bis";
            this.DgrTextBoxValidUntil.MappingName = "ValidUntil";
            this.DgrTextBoxValidUntil.NullText = "";
            this.DgrTextBoxValidUntil.Width = 75;
            // 
            // DgrTextBoxAuthorised
            // 
            this.DgrTextBoxAuthorised.Format = "";
            this.DgrTextBoxAuthorised.FormatInfo = null;
            this.DgrTextBoxAuthorised.HeaderText = "Zutritt gewährt";
            this.DgrTextBoxAuthorised.MappingName = "Authorised";
            this.DgrTextBoxAuthorised.NullText = "";
            this.DgrTextBoxAuthorised.Width = 85;
            // 
            // DgrTextBoxExcontractor
            // 
            this.DgrTextBoxExcontractor.Format = "";
            this.DgrTextBoxExcontractor.FormatInfo = null;
            this.DgrTextBoxExcontractor.HeaderText = "Fremdfirma";
            this.DgrTextBoxExcontractor.MappingName = "ExContractor";
            this.DgrTextBoxExcontractor.NullText = "";
            this.DgrTextBoxExcontractor.Width = 140;
            // 
            // DgrTextBoxSubContractor
            // 
            this.DgrTextBoxSubContractor.Format = "";
            this.DgrTextBoxSubContractor.FormatInfo = null;
            this.DgrTextBoxSubContractor.HeaderText = "Subfirma";
            this.DgrTextBoxSubContractor.MappingName = "SubContractor";
            this.DgrTextBoxSubContractor.NullText = "";
            this.DgrTextBoxSubContractor.Width = 140;
            // 
            // UCReportChecklist
            // 
            this.Controls.Add(this.DgrChecklist);
            this.Name = "UCReportChecklist";
            this.Size = new System.Drawing.Size(1246, 420);
            ((System.ComponentModel.ISupportInitialize)(this.DgrChecklist)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		#region Accessors 

		public decimal CheckListID
		{
			get 
			{
				return mCheckListID;
			}
			set 
			{
				mCheckListID = value;
			}
		}

		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// Gets ID of current CheckList record
		/// </summary>
		private void TableNavigated()
		{
			int rowIndex = this.DgrChecklist.CurrentRowIndex;
			if(-1 < rowIndex)
			{
				mCheckListID = Convert.ToDecimal(this.DgrChecklist[rowIndex, 0].ToString());
			}
		}
		
		#endregion // End of Methods
		
		#region Events

		/// <summary>
		/// Get ID of current record if datagrid entered and only 1 record
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DgrChecklist_Enter(object sender, System.EventArgs e)
		{
			if(this.DgrChecklist.CurrentRowIndex == 0)
			{
				this.TableNavigated();
			}
		}
		
		/// <summary>
		/// Get ID of current record if datagrid cell changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DgrChecklist_CurrentCellChanged(object sender, System.EventArgs e)
		{
			if ( !mGridIsSorted )
			{
				if (this.DgrChecklist.VisibleRowCount > 1)
				{
					this.TableNavigated();
				}
			}		
		}

		/// <summary>
		/// New 25.02.02 Mundy
		/// Discard current coworker ID when datagrid is sorted and send pointer to first record
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DgrChecklist_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			mGridIsSorted = true;
			if ( this.DgrChecklist.VisibleRowCount > 1 )
			{
				DataGrid.HitTestInfo hti  = DgrChecklist.HitTest(e.X, e.Y);
				DataGrid.HitTestType type = hti.Type;
				switch(type)       
				{         
					case System.Windows.Forms.DataGrid.HitTestType.ColumnHeader:
						this.DgrChecklist.CurrentRowIndex = 0;
						mCheckListID = -1;
						break;				
				}
			}
			mGridIsSorted = false;	
		}

		#endregion // End of Events

		
	}
}

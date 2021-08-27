using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using de.pta.Component.DbAccess;
using System.Diagnostics;
using Microsoft.Win32;
using System.Text.RegularExpressions;

using de.pta.Component.ListOfValues;
using de.pta.Component.Common;

namespace Testing
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ComboBox cboKoordinator;
		private System.Windows.Forms.ComboBox cboFremdfirma;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.StatusBar statBar1;
		private System.Windows.Forms.Button btnStatus;
		private System.Windows.Forms.Button btnTest;
		private System.Windows.Forms.TextBox txtB;
		private System.Windows.Forms.TextBox txtA;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button Printer;
		private System.Windows.Forms.Button btnGenPDF;
		private System.Windows.Forms.RadioButton radioButton1;
		private System.Windows.Forms.RadioButton radioButton2;
		private System.Windows.Forms.Label label4;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
	
	
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
			this.cboKoordinator = new System.Windows.Forms.ComboBox();
			this.cboFremdfirma = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.statBar1 = new System.Windows.Forms.StatusBar();
			this.btnStatus = new System.Windows.Forms.Button();
			this.btnTest = new System.Windows.Forms.Button();
			this.txtB = new System.Windows.Forms.TextBox();
			this.txtA = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.Printer = new System.Windows.Forms.Button();
			this.btnGenPDF = new System.Windows.Forms.Button();
			this.radioButton1 = new System.Windows.Forms.RadioButton();
			this.radioButton2 = new System.Windows.Forms.RadioButton();
			this.label4 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// cboKoordinator
			// 
			this.cboKoordinator.Location = new System.Drawing.Point(184, 32);
			this.cboKoordinator.Name = "cboKoordinator";
			this.cboKoordinator.Size = new System.Drawing.Size(184, 21);
			this.cboKoordinator.TabIndex = 1;
			this.cboKoordinator.SelectedIndexChanged += new System.EventHandler(this.cboKoordinator_SelectedIndexChanged);
			// 
			// cboFremdfirma
			// 
			this.cboFremdfirma.Location = new System.Drawing.Point(184, 104);
			this.cboFremdfirma.Name = "cboFremdfirma";
			this.cboFremdfirma.Size = new System.Drawing.Size(184, 21);
			this.cboFremdfirma.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(40, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(112, 23);
			this.label1.TabIndex = 3;
			this.label1.Text = "Koordinator";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(40, 104);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(112, 23);
			this.label2.TabIndex = 4;
			this.label2.Text = "Fremdfirma";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(168, 248);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(88, 32);
			this.button1.TabIndex = 5;
			this.button1.Text = "Over 18";
			this.button1.Click += new System.EventHandler(this.button1_Click_1);
			// 
			// statBar1
			// 
			this.statBar1.Location = new System.Drawing.Point(0, 365);
			this.statBar1.Name = "statBar1";
			this.statBar1.Size = new System.Drawing.Size(424, 24);
			this.statBar1.TabIndex = 6;
			this.statBar1.Text = "MyStatBar";
			// 
			// btnStatus
			// 
			this.btnStatus.Location = new System.Drawing.Point(264, 248);
			this.btnStatus.Name = "btnStatus";
			this.btnStatus.Size = new System.Drawing.Size(88, 32);
			this.btnStatus.TabIndex = 7;
			this.btnStatus.Text = "Get Status";
			this.btnStatus.Click += new System.EventHandler(this.btnStatus_Click);
			// 
			// btnTest
			// 
			this.btnTest.Location = new System.Drawing.Point(264, 320);
			this.btnTest.Name = "btnTest";
			this.btnTest.Size = new System.Drawing.Size(104, 32);
			this.btnTest.TabIndex = 8;
			this.btnTest.Text = "Test exception +  return";
			this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
			// 
			// txtB
			// 
			this.txtB.Location = new System.Drawing.Point(208, 320);
			this.txtB.Name = "txtB";
			this.txtB.Size = new System.Drawing.Size(32, 20);
			this.txtB.TabIndex = 9;
			this.txtB.Text = "";
			// 
			// txtA
			// 
			this.txtA.Location = new System.Drawing.Point(152, 320);
			this.txtA.Name = "txtA";
			this.txtA.Size = new System.Drawing.Size(32, 20);
			this.txtA.TabIndex = 10;
			this.txtA.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(192, 320);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(8, 16);
			this.label3.TabIndex = 11;
			this.label3.Text = "/";
			// 
			// Printer
			// 
			this.Printer.Location = new System.Drawing.Point(32, 280);
			this.Printer.Name = "Printer";
			this.Printer.TabIndex = 12;
			this.Printer.Text = "button2";
			this.Printer.Click += new System.EventHandler(this.Printer_Click);
			// 
			// btnGenPDF
			// 
			this.btnGenPDF.Location = new System.Drawing.Point(312, 176);
			this.btnGenPDF.Name = "btnGenPDF";
			this.btnGenPDF.Size = new System.Drawing.Size(80, 32);
			this.btnGenPDF.TabIndex = 13;
			this.btnGenPDF.Text = "Öffnen PDF";
			this.btnGenPDF.Click += new System.EventHandler(this.btnGenPDF_Click);
			// 
			// radioButton1
			// 
			this.radioButton1.Location = new System.Drawing.Point(136, 192);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.TabIndex = 14;
			this.radioButton1.Text = "radioButton1";
			this.radioButton1.Enter += new System.EventHandler(this.radioButton1_Enter);
			// 
			// radioButton2
			// 
			this.radioButton2.Location = new System.Drawing.Point(40, 192);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new System.Drawing.Size(88, 24);
			this.radioButton2.TabIndex = 15;
			this.radioButton2.Text = "radioButton2";
			this.radioButton2.Enter += new System.EventHandler(this.radioButton1_Enter);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(32, 168);
			this.label4.Name = "label4";
			this.label4.TabIndex = 16;
			this.label4.Text = "Baubles";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(424, 389);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.radioButton2);
			this.Controls.Add(this.radioButton1);
			this.Controls.Add(this.btnGenPDF);
			this.Controls.Add(this.Printer);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.txtA);
			this.Controls.Add(this.txtB);
			this.Controls.Add(this.btnTest);
			this.Controls.Add(this.btnStatus);
			this.Controls.Add(this.statBar1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cboFremdfirma);
			this.Controls.Add(this.cboKoordinator);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}
		#endregion

		

		private void button1_Click(object sender, System.EventArgs e)
		{
			
		}

		private void FillCoordinatorFull() 
		{
			ArrayList coord = new ArrayList();
			coord.Add(new LovItem ("0", ""));
			coord.AddRange(LovSingleton.GetInstance().GetRootList(null, "VW_FPASS_COORDINATORS", "VWC_BOTHNAMES"));			
			this.cboKoordinator.DataSource = coord;
			this.cboKoordinator.DisplayMember = "ItemValue";
			this.cboKoordinator.ValueMember = "Id";
		}


		private void FillFremdfirmaFull() 
		{	
			ArrayList ff = new ArrayList();
			ff.Add(new LovItem ("0", ""));
			ff.AddRange( LovSingleton.GetInstance().GetRootList(null, "vw_fpass_exco_coord_admin", "EXCO_NAME" ));
			this.cboFremdfirma.DataSource = ff;
			this.cboFremdfirma.DisplayMember = "ItemValue";
			this.cboFremdfirma.ValueMember = "Id";
		}

		private void FillFremdfirmaSub(String pID) 
		{
			ArrayList ff = new ArrayList();
			ff.Add(new LovItem ("0", ""));
			ff.AddRange( LovSingleton.GetInstance().GetSubList(null, "VW_FPASS_COORDINATORS", "EXCO_NAME", pID) );
			this.cboFremdfirma.DataSource = ff;
			this.cboFremdfirma.DisplayMember = "ItemValue";
			this.cboFremdfirma.ValueMember = "Id";
		}

		private void cboKoordinator_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			
			String  id = ((LovItem)cboKoordinator.SelectedItem).Id;
			if ( ! id.Equals("0") ) 
			{
				this.FillFremdfirmaSub(id);
			} 
			else 
			{
				this.cboFremdfirma.DataSource = null;
				this.cboFremdfirma.Refresh();
				this.FillFremdfirmaFull();
			}
		}

		private void button1_Click_1(object sender, System.EventArgs e)
		{
			DateTime thisDate = DateTime.Now;
			DateTime birthDate = Convert.ToDateTime("28.10.1977");

			int howOld = thisDate.Year - birthDate.Year;

			if ( howOld >= 18 )
			{
				MessageBox.Show(this, "Is over 18!");
			}
			else
			{
				MessageBox.Show(this, "Is too young!");
			}
			
		}

		private void btnStatus_Click(object sender, System.EventArgs e)
		{
			this.statBar1.Text = "Boutros, boutros ghali";

			MessageBox.Show(this, "CDone");

			this.statBar1.Text = "";
		
		}

		private void btnTest_Click(object sender, System.EventArgs e)
		{
			string result;
			try
			{
				result = this.Test(this.txtA.Text, this.txtB.Text);
				MessageBox.Show("returncode:" + result);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Exception: " + ex.Message);
			}
		}

		private string Test(string prmA, string prmB)
		{
			try
			{
				int result = (Convert.ToInt32(prmA) / Convert.ToInt32(prmB));	
				return result.ToString();
			}
			catch (Exception ex)
			{
				throw new Exception("Fehler!!!", ex);
				//return "unmöglich!"; // can not be reached!!!
			}
		}

		private void Printer_Click(object sender, System.EventArgs e)
		{
			System.Drawing.Printing.PrintDocument printDocument1 
				= new System.Drawing.Printing.PrintDocument();
			MessageBox.Show(printDocument1.PrinterSettings.PrinterName);
		}

		/// <summary>
		/// Opens an existing PDF doc
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnGenPDF_Click(object sender, System.EventArgs e)
		{
			this.Show("C:\\temp\\Test.pdf");
		
		}

		private void Show(string prmPdfFile)
		{
			// references the entry path of Acrobat-Reader in the registry
			string regPath = "AcroExch.Document\\shell\\open\\command";
			RegistryKey root = Registry.ClassesRoot;
			RegistryKey key = root.OpenSubKey(regPath);

			if(key.Equals(null))
			{
				throw new Exception("No Acrobat Reader");
			}
			
			// the entry holds the path of the Acrobat Reader exe-file
			string readerPath = (string)key.GetValue(null);
			if (null != readerPath && 0 < readerPath.Length)
			{
				if(readerPath.StartsWith("\"") && readerPath.EndsWith("\""))
				{
					readerPath = readerPath.Substring(1, readerPath.Length -1); // cutting off \" at beginning or end if needed
				}
			}

			string[] parts = Regex.Split(readerPath, "\" \"");

			if(0 == parts.Length)
			{
				throw new Exception("No Acrobat Reader");
			}
		
			string acroPath = parts[0];
			if(acroPath.Equals(null) || "\"\"".Equals(acroPath) || 0 == acroPath.Length)
			{
				acroPath = parts[1];
			}
			
			// starts Acrobat Reader with the pdf report
			Process pdfStart      = null; 
			ProcessStartInfo info = new ProcessStartInfo();			
			info.FileName         = acroPath;

			
			System.Drawing.Printing.PrintDocument printDocument1 = new System.Drawing.Printing.PrintDocument();

			string defPrinter = printDocument1.PrinterSettings.PrinterName;

			int printTimes = 1;

			try
			{
				
				info.Arguments = prmPdfFile;					
				
				pdfStart = Process.Start(info);
			}
			catch (Exception ex)
			{
				throw new Exception("Error in opening file: " + ex.Message);
			}		
		}

		private void radioButton1_Enter(object sender, System.EventArgs e)
		{
			if ( ((RadioButton)sender).Checked )
			{
				((RadioButton)sender).Checked = false;
			}
		} 
		
	}
}

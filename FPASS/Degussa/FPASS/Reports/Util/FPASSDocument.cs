using System;
using Microsoft.Win32; // Registry
using System.Diagnostics; // ProcessStartInfo
using System.Text.RegularExpressions; // Regex
using System.Windows.Forms; // MessageBox & Application

using Degussa.FPASS.Db; // DBSingleton
using Degussa.FPASS.Util;
using Degussa.FPASS.Util.UserManagement;
using Degussa.FPASS.Util.ErrorHandling;
using Degussa.FPASS.Util.Messages; // MessageSingleton
using Degussa.FPASS.Util.Exceptions;

using de.pta.Component.PdfGenerator; 
using de.pta.Component.Errorhandling;

namespace Degussa.FPASS.Reports.Util
{
	/// <summary>
	/// Summary description for FPASSDocument.
	/// Class for opening Acrobat Reader with a static document
	/// i.e. document content is always same and independent of database
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
	public class FPASSDocument
	{
		#region Members

		// Fully-qualified filename of document. Comes out of DB
		private string mFilenameAndPath;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public FPASSDocument()
		{
			initialize();
		}

		#endregion //End of Constructors

		#region Initialization

		/// <summary>
		/// Initializes the members.
		/// </summary>
		private void initialize()
		{
		}	

		#endregion //End of Initialization

		#region Accessors 

		internal string FullNameAndPath
		{
			get  
			{
				return mFilenameAndPath;
			}
			set
			{
				mFilenameAndPath = value;
			}
		}

		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// Open new Acrobat Reader process and shows current document
		///  16.12.2003: Argument /t when calling Reader not reliable, need to kill process, wait for printer etc
		/// Use /p to presentsuser with Acrobat Reader print dialogue
		/// </summary>
		/// <param name="prmPdfFile"></param>
		internal void ShowPDF()
		{
			// references the entry path of Acrobat Reader in the registry
            string regPath = Globals.GetInstance().ReportsReaderKey;
            
			RegistryKey root = Registry.ClassesRoot;
			RegistryKey key = root.OpenSubKey(regPath);

			if (null == key)
			{
				throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.REPORT_ACROBAT_READER));
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
				throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.REPORT_ACROBAT_READER));
			}
		
			string acroPath = parts[0];
			if(acroPath.Equals(null) || "\"\"".Equals(acroPath) || 0 == acroPath.Length)
			{
				acroPath = parts[1];
			}
			
			// starts Acrobat Reader with PDF document
			Process pdfStart      = null; 
			ProcessStartInfo info = new ProcessStartInfo();			
			info.FileName         = acroPath;

			string defPrinter     = Globals.GetInstance().GetDefaultPrinter();

			int printTimes = UserManagementControl.getInstance().NumberOfPrintedPass;

			try
			{
				// If no preview required then print directly, assuming machine has default printer
				if ( !UserManagementControl.getInstance().PreviewPass && !defPrinter.Equals("<no default printer>") ) 
				{				
					info.Arguments = "/p " + mFilenameAndPath;
				}
				else
				{
					info.Arguments = mFilenameAndPath;					
				}
				pdfStart = Process.Start(info);
			}
			catch (Exception ex)
			{
				Globals.GetInstance().Log.Fatal("Exception: konnte statisches Dokument " + mFilenameAndPath + " nicht öffnen.", ex	);
				throw new UIWarningException("Fehler: das Dokument " + mFilenameAndPath + " konnte nicht geöffnet werden.");
			}		
		} 

		#endregion 
	}
}

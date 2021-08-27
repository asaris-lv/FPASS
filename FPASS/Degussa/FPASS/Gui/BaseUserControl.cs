using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using de.pta.Component.ListOfValues;
using de.pta.Component.DbAccess;

using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.Util.Validation;
using Degussa.FPASS.Util.UserManagement;
using Degussa.FPASS.Util.Exceptions;
using Degussa.FPASS.Util.ListOfValues;
using Degussa.FPASS.Util.ErrorHandling;
using Degussa.FPASS.Gui;
using Degussa.FPASS.Db;

namespace Degussa.FPASS.Gui
{
	/// <summary>
	/// Summary description for [!output SAFE_CLASS_NAME].
	/// </summary>
	/// <remarks>
	/// <para><b>History</b></para>
	/// <div class="tablediv">
	/// <table class="dtTABLE" cellspacing="0">
	///		<tr>
	///			<th width="20%">Wollersheim-Heer</th>
	///			<th width="20%">10/01/2003</th>
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
	public class BaseUserControl : System.Windows.Forms.UserControl
	{

		#region Members

		/// <summary>
		/// used to hold the Controller of this triad
		/// </summary>
		internal	AbstractController						mController;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;


		#endregion //End of Members

		#region Constructors 		

		public BaseUserControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			
		}

		#endregion // End of Constuctors

		#region Component Designer generated code
		
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
		
		
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// BaseUserControl
			// 
			this.Name = "BaseUserControl";
			this.Size = new System.Drawing.Size(992, 542);

		}
		#endregion

		#region Accessors 

		#endregion //End of Accessors

		#region Methods 

		public void RegisterController(AbstractController aAbstractController) 
		{
			this.mController = aAbstractController;
		}


		/// <summary>
		/// is called before a dialog is closed. empty implementation because subclasses
		/// have to implement their individual logic
		/// </summary>
		internal virtual void PreClose()
		{

		}

		/// <summary>
		/// is called before a dialog is destroyed. empty implementation because subclasses
		/// have to implement their individual logic to free all ressoucers tey hold
		/// </summary>
		internal virtual void PreDestroy() 
		{

		}


		/// <summary>
		/// is called before a dialog is displayed. empty implementation because subclasses
		/// have to implement their individual logic
		/// </summary>
		internal virtual void PreShow() 
		{

		}

		/// <summary>
		/// is called before a dialog is hidden/covered. empty implementation because subclasses
		/// have to implement their individual logic
		/// </summary>
		internal virtual void PreHide() 
		{

		}

		/// <summary>
		/// Fills all the lists of this view. empty implementation because subclasses
		/// have to implement their individual logic
		/// </summary>
		internal virtual void FillLists() 
		{
			
		}


		internal virtual void ReFillContractorList(String pContractorID) 
		{
			
		}
		

		#endregion // End of Methods

	}
}

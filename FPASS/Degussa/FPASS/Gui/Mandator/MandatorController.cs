using System;
using System.Windows.Forms;

using de.pta.Component.Errorhandling;

using Degussa.FPASS.Util.UserManagement;
using Degussa.FPASS.Gui;
using Degussa.FPASS.FPASSApplication;

namespace Degussa.FPASS.Gui.Mandator
{
	/// <summary>
	/// A MandatorController is the view of the MVC-triad MandatorController,
	/// MandatorModel and FrmMandator.
	/// MandatorController extends from the AbstractController.
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
	///			<td width="60%">Comment</td>
	///		</tr>
	/// </table>
	/// </div>
	/// </remarks>
	public class MandatorController : AbstractController
	{
		#region Members

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public MandatorController()
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

			mDialogId = AllFPASSDialogs.MANDATOR_DIALOG;
			mView = new FrmMandator();
			mView.RegisterController(this);

			mModel = new MandatorModel();
			mModel.registerView(mView);

		}	

		#endregion //End of Initialization

		#region Accessors 

		#endregion //End of Accessors

		#region Methods 

		internal void handleEventProcessWithFPASS() 
		{
			// let the model check the selected mandator
			try 
			{
				((MandatorModel)mModel).VerifySelectedMandator();
				FPASSControllSingleton.GetInstance().DestroyDialog(	this );
			} 
			catch ( UIWarningException  uwe ) 
			{
				ExceptionProcessor.GetInstance().Process(uwe);
			}
		}


		#endregion // End of Methods


	}
}

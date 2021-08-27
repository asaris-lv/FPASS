using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

using de.pta.Component.Errorhandling;
using Degussa.FPASS.FPASSApplication;
using Degussa.FPASS.Gui;
using Degussa.FPASS.Util.Exceptions;
using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.Util.UserManagement;

namespace Degussa.FPASS.Gui.Dialog.Pass
{
	/// <summary>
	/// Summary description for PopCoWorkerHistController.
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
	public class PopCoWorkerHistController : FPASSBaseController
	{
		#region Members

		/// <summary>
		/// used to hold the model of this triad. hold for convenience to avoid casting
		/// </summary>
		private	PopCoWorkerHistModel mPopCoWorkerHistModel;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public PopCoWorkerHistController()
		{
			initialize();
		}

		#endregion //End of Constructors

		#region Initialization

		/// <summary>
		/// Initializes the members, including view and model
		/// </summary>
		private void initialize()
		{
			mDialogId = AllFPASSDialogs.POPUP_COWORKER_COORD_HIST;
			mView = new FrmPopCoWorkerHist();
			mView.RegisterController(this);

			mModel = new PopCoWorkerHistModel();
			mModel.registerView(mView);

			mPopCoWorkerHistModel = (PopCoWorkerHistModel) mModel;
		}	

		#endregion //End of Initialization

		#region Accessors 

		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// Set ID of current coworker
		/// </summary>
		/// <param name="pCoWorkerID"></param>
		internal void SetCurrentFFMA(decimal pCoWorkerID)
		{
			((PopCoWorkerHistModel) mModel).CoWorkerID = pCoWorkerID;
		}

		#endregion // End of Methods


	}
}

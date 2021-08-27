using System;
using System.Windows.Forms;

using de.pta.Component.Errorhandling;

using Degussa.FPASS.Gui;
using Degussa.FPASS.FPASSApplication;
using Degussa.FPASS.Util.Exceptions;
using Degussa.FPASS.Util.Messages;


namespace Degussa.FPASS.Gui.Dialog.Administration
{
	/// <summary>
	/// Summary description for PopECODHistController.
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
	public class PopECODHistController : FPASSBaseController
	{
		#region Members

		/// <summary>
		/// Model belonging to this MVC triad
		/// </summary>
		private		PopECODHistModel mPopECODHistModel;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public PopECODHistController()
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
			mDialogId = AllFPASSDialogs.POPUP_EXCO_COORD_HIST;
			mView = new FrmPopExcoCoordHist();
			mView.RegisterController(this);

			mModel = new PopECODHistModel();
			mModel.registerView(mView);

			mPopECODHistModel = (PopECODHistModel)mModel;
		}	

		#endregion //End of Initialization

		#region Accessors 

		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// Set ID of current excontractor
		/// </summary>
		/// <param name="pCoWorkerID"></param>
		internal void SetCurrentExcoID(decimal pEXCOID)
		{
			((PopECODHistModel) mModel).ExContractorID = pEXCOID;
		}

		#endregion // End of Methods


	}
}

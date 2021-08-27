using System;

using de.pta.Component.Errorhandling;

using Degussa.FPASS.Bo.Mandator;
using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.Util.UserManagement;

namespace Degussa.FPASS.Gui.Mandator
{
	/// <summary>
	/// A MandatorModel is the view of the MVC-triad MandatorController,
	/// MandatorModel and FrmMandator.
	/// MandatorModel extends from the AbstractModel.
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
	public class MandatorModel : AbstractModel
	{
		#region Members

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public MandatorModel()
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

		#endregion //End of Accessors

		#region Methods 

		

		internal void VerifySelectedMandator() 
		{
			BOUserMndt boUserMndt;

			boUserMndt = (BOUserMndt)((FrmMandator)mView).CboMandator.SelectedItem ;
			if ( null == boUserMndt ) 
			{
				throw new UIWarningException(MessageSingleton.GetInstance().
					GetMessage(MessageSingleton.NO_MANDATOR_SELECTED));
			} 
			else 
			{
				UserManagementControl.getInstance().CurrentBOUserMndt = boUserMndt;
				UserManagementControl.getInstance().NumberOfMandators = 1;
			}	
		}

		



		#endregion // End of Methods


	}
}

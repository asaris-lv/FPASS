using System;
using Degussa.FPASS.Gui.Dialog.Pass;
using Degussa.FPASS.Util;

namespace Degussa.FPASS.Bo.Pass
{
	/// <summary>
	/// This class handles the "P extern Zutrittsmodell"
	/// Attributes for a granted "external parking" are saved in the FPASS DB schema
    /// "P extern Zutrittsmodell"can be transferred to ZKS
	/// </summary>
	/// <remarks>
	/// <para><b>History</b></para>
	/// <div class="tablediv">
	/// <table class="dtTABLE" cellspacing="0">
	///		<tr>
	///			<th width="20%">N Mundy</th>
	///			<th width="20%">25.02.2008</th>
	///			<th width="60%">Creation</th>
	///		</tr>
	/// </table>
	/// </div>
	/// </remarks>
	public class ParkingExternalAuthorization : AbstractAuthorization
	{
		#region Members

		#endregion

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public ParkingExternalAuthorization(CoWorkerModel pCoWorkerModel) : base (pCoWorkerModel)
		{
			initialize();
		}

		#endregion

		#region Initialization

		/// <summary>
		/// Initializes the members.
		/// </summary>
		private void initialize()
		{
			mAuthorizationTypeID = Globals.GetInstance().ParkingExternAuthID;
		}	

		#endregion 

		#region Accessors 

		#endregion

		#region Methods 

		/// <summary>
		/// Copies data from BO into GUI, in tab page "Werkschutz" (site security)
		/// Note: P extern is assigned and received at same time
		/// </summary>
		internal override void CopyIn() 
		{
			if ( !mAuthorizationExecuted )
			{
				mAuthorizationDate = DateTime.Now;
			}			
			mViewCoWorker.DatSiSePExternalOn.Value = mAuthorizationDate;
			mChanged = false;
						
			if ( mAuthorizationExecuted ) 
			{
				mViewCoWorker.TxtSiSePExternalBy.Text = this.mUserName;
			} 
			else 
			{
				mViewCoWorker.TxtSiSePExternalBy.Text = String.Empty;
			}
			mViewCoWorker.CbxSiSePExternal.Checked = mAuthorizationExecuted;
		}

		/// <summary>
		/// Copies data from GUI tab page "Werkschutz" into BO
		/// </summary>
		internal override void CopyOut() 
		{
			int dateCompared = mCoWorkerModel.CompareDates(this.mAuthorizationDate,
				mViewCoWorker.DatSiSePExternalOn.Value);
			
			if ( dateCompared != 0  ) 
			{
				this.mAuthorizationDate = mViewCoWorker.DatSiSePExternalOn.Value;
				mChanged = true;
			}

			if ( mViewCoWorker.CbxSiSePExternal.Checked != mAuthorizationExecuted ) 
			{
				mChanged = true;
				this.mAuthorizationExecuted = mViewCoWorker.CbxSiSePExternal.Checked;
			}
			// Save username from last change
			if ( mChanged )
			{
				SetWhoChangedMeReceived();
			}
		}

		#endregion
	}
}

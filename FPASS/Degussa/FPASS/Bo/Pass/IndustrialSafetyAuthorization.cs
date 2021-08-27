using System;
using Degussa.FPASS.Gui.Dialog.Pass;
using Degussa.FPASS.Util;

namespace Degussa.FPASS.Bo.Pass
{
	/// <summary>
	/// A Coworker must get this authorization to enter degussa for more than just
	/// one day. It's called "Sicherheitsunterweisung FFMA" on tab coordinator. 
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
	public class IndustrialSafetyAuthorization : AbstractAuthorization
	{
		#region Members

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public IndustrialSafetyAuthorization(CoWorkerModel pCoWorkerModel) : base (pCoWorkerModel)
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
			this.mAuthorizationTypeID = Globals.GetInstance().AuthoIndSafetySiteID;
		}	

		#endregion

		#region Methods 

		/// <summary>
		/// Shows data of the bo in the gui in the tab page site coordinator
		/// Data is "copied in the gui".
		/// </summary>
		internal override void CopyIn() 
		{
			// Reiter Koordinator. Sicherheitsunterweisung FFMA^. If autho revoked, reset date
			if ( !mAuthorizationExecuted )
			{
				mAuthorizationDate = DateTime.Now;
			}			
			mViewCoWorker.DatCoIndustrialSafetyBriefingSiteOn.Value = this.mAuthorizationDate;
			mChanged = false;
						
			if ( mAuthorizationExecuted ) 
			{
				mViewCoWorker.TxtCoIndustrialSafetyBriefingSiteBy.Text = this.mUserName;
			} 
			else 
			{
				mViewCoWorker.TxtCoIndustrialSafetyBriefingSiteBy.Text = String.Empty;
			}
            mViewCoWorker.CbxCoIndSafetyBrfRecvd.Checked = mAuthorizationExecuted;

            mCoWorkerModel.IndSafetyBrfRecvd = mAuthorizationExecuted;
		}

		/// <summary>
		/// Copies data out of GUI in tab page coordinator 
		/// Note: authorization is assigned and received at same time
		/// </summary>
		internal override void CopyOut() 
		{
			int dateCompared = mCoWorkerModel.CompareDates(mAuthorizationDate, mViewCoWorker.DatCoIndustrialSafetyBriefingSiteOn.Value);
			
			if ( dateCompared != 0  ) 
			{
				mAuthorizationDate = mViewCoWorker.DatCoIndustrialSafetyBriefingSiteOn.Value;
				mChanged = true;
			}

            // Signal export to SmartAct if "PKI" set to true
			if (mViewCoWorker.CbxCoIndSafetyBrfRecvd.Checked != mAuthorizationExecuted) 
			{
				mChanged = true;
				mAuthorizationExecuted = mViewCoWorker.CbxCoIndSafetyBrfRecvd.Checked;

                if (mAuthorizationExecuted)
                {
                    mCoWorkerModel.IndSafetyBrfRecvd = true;
                    mCoWorkerModel.ShouldExportSmartAct = true;
                }
			}

			// Save name of user who made last change
			if (mChanged)
			{
				SetWhoChangedMeReceived();
			}
		}

		#endregion // End of Methods
	}
}

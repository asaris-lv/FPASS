using System;
using System.Collections;
using System.Data;
using System.Windows.Forms; 

using de.pta.Component.DbAccess;
using de.pta.Component.Errorhandling;

using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.Util.Exceptions;
using Degussa.FPASS.Util.UserManagement;
using Degussa.FPASS.Util;
using Degussa.FPASS.Db;

using Degussa.FPASS.Gui.Dialog.Pass;
using Degussa.FPASS.FPASSApplication;

namespace Degussa.FPASS.Bo.Pass
{
	/// <summary>
	/// Summary description for VehicleRegistrationNumberFactory.
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
	public class VehicleRegistrationNumberFactory
	{
		#region Members
		
		private				ArrayList		mVehicles;
		private				CoWorkerModel	mCoWorkerModel;
		private				IDbCommand		mSelComm;
		private				decimal			mCoWorkerID;
		private				String			SELECT_VEHREGNO = "SelectVehicleRegistrationNumber";
		private				String			SELECT_VEHREGNO_ARCHIVE = "SelectVehicleRegistrationNumberArchive";
		private				String			VEHREGNO_CWR_PARAM = ":VRNO_CWR_ID";
		private				IProvider		mProvider;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public VehicleRegistrationNumberFactory(CoWorkerModel pModel)
		{
			mCoWorkerModel = pModel;
			initialize();
		}

		#endregion //End of Constructors

		#region Initialization

		/// <summary>
		/// Initializes the members.
		/// </summary>
		private void initialize()
		{
			// Get DataProvider from DbAccess component
			mProvider = DBSingleton.GetInstance().DataProvider;
			mCoWorkerID = mCoWorkerModel.CoWorkerId;
		}	

		#endregion //End of Initialization

		#region Accessors 

		#endregion //End of Accessors

		#region Methods 

		internal ArrayList GetVehicles()  
		{
			mVehicles = new ArrayList();
			this.SelectVehicles();

			int numberOfVehicles = mVehicles.Count;
			while ( mVehicles.Count < 4 ) 
			{
				numberOfVehicles++;
				VehicleRegistrationNumber vehicle = new VehicleRegistrationNumber(mCoWorkerModel);
				vehicle.VehicleFieldID = numberOfVehicles;
				mVehicles.Add(vehicle);
			}
			return mVehicles;
		}

		private void SelectVehicles() 
		{
			mSelComm = null;
			VehicleRegistrationNumber vehicle; 
			
			if ( ! mCoWorkerModel.Mode.Equals( AllFPASSDialogs.CWR_DIALOG_MODE_ARCHIVE ) )
			{
				mSelComm = mProvider.CreateCommand(SELECT_VEHREGNO);
			} 
			else 
			{
				mSelComm = mProvider.CreateCommand(SELECT_VEHREGNO_ARCHIVE);
			}
			mProvider.SetParameter(mSelComm, VEHREGNO_CWR_PARAM, mCoWorkerID);

			// Open data reader to get ExContractor data
			IDataReader mDR = mProvider.GetReader(mSelComm);

			int vehicleFieldID = 0;
			// Loop thru records and create an ArrayList of ExContractor BOs
			while (mDR.Read())
			{
				vehicleFieldID++;

				vehicle = new VehicleRegistrationNumber(mCoWorkerModel);
				
				vehicle.VehicleFieldID = vehicleFieldID;
				vehicle.VehicleId =	Convert.ToDecimal( mDR["VRNO_ID"] );
				vehicle.VehicleNumber =  mDR["VRNO_VEHREGNO"].ToString();
				vehicle.ChangeUser = Convert.ToDecimal( mDR["VRNO_CHANGEUSER"] );

				vehicle.IsInsert = false;

				mVehicles.Add(vehicle);
			}
		
			// Close reader
			mDR.Close();
		}

		#endregion // End of Methods

	}
}

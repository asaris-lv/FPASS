using System;

namespace Degussa.FPASS.Bo.Reports
{
	/// <summary>
	/// Summary description for BOReportPlantManager.
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
	public class BOReportPlantManager
	{
		#region Members

		private decimal mUSPLID;
		private string	mName;
		private string	mFirstname;
		private string  mPlant;
	

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public BOReportPlantManager()
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

		public decimal USPLID 
		{
			get 
			{
				return mUSPLID;
			}
			set 
			{
				mUSPLID = value;
			}
		} 
	
		
		public string Name 
		{
			get 
			{
				return mName;
			}
			set 
			{
				mName = value;
			}
		}

		public string Firstname 
		{
			get 
			{
				return mFirstname;
			}
			set 
			{
				mFirstname = value;
			}
		}

		public string Plant 
		{
			get 
			{
				return mPlant;
			}
			set 
			{
				mPlant = value;
			}
		}

		
		#endregion //End of Accessors

		#region Methods 

		#endregion // End of Methods


	}
}

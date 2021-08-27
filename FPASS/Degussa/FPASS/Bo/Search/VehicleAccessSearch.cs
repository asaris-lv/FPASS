using System;

namespace Degussa.FPASS.Bo.Search
{
	/// <summary>
	/// Simple BO which acts as a container. Only used to display data in a grid 
	/// in the form FrmVehicle. 
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
	public class VehicleAccessSearch
	{
		#region Members

		private decimal mCwrID;
		private string	mSurName;
		private string	mFirstName;
		private string  mExcoName;
		private string  mVehicleEntrShort;
		private string  mVehicEntrShortRecieve;
		private string  mVehicEntrShortRecDate;
		private string  mVehicEntrShortRecUser;
		private string  mVehicleEntrLong;
		private string  mVehicEntrLongRecieve;
		private string  mVehicEntrLongRecDate;
		private string  mVehicEntrLongRecUser;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public VehicleAccessSearch()
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

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public decimal CwrID 
		{
			get 
			{
				return mCwrID;
			}
			set 
			{
				mCwrID = value;
			}
		} 
			
		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public string SurName 
		{
			get 
			{
				return mSurName;
			}
			set 
			{
				mSurName = value;
			}
		}

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public string FirstName 
		{
			get 
			{
				return mFirstName;
			}
			set 
			{
				mFirstName = value;
			}
		}

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public string ExcoName 
		{
			get 
			{
				return mExcoName;
			}
			set 
			{
				mExcoName = value;
			}
		}

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public string VehicleEntrShort 
		{
			get 
			{
				return mVehicleEntrShort;
			}
			set 
			{
				mVehicleEntrShort = value;
			}
		}

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public String VehicleShortDesire 
		{
			get 
			{
				if ( mVehicleEntrShort == "Y" || mVehicleEntrShort == "gewünscht") 
				{
					return mVehicleEntrShort = "gewünscht";
				} 
				else
				{
					return mVehicleEntrShort = String.Empty;
				}
			}
		}

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public string VehicEntrShortRecieve 
		{
			get 
			{
				return mVehicEntrShortRecieve;
			}
			set 
			{
				mVehicEntrShortRecieve = value;
			}
		}

		public String VehicleShortAllowed 
		{
			get 
			{
				//if ( mVehicEntrShortRecieve == "N" || mVehicEntrShortRecieve == "akzeptiert") 
				if ( (mVehicEntrShortRecieve == "N" && mVehicEntrShortRecDate != null) || mVehicEntrShortRecieve == "akzeptiert")
				{
					return mVehicEntrShortRecieve = "akzeptiert";
				} 
			//	else if ( mVehicEntrShortRecieve == "Y" || mVehicEntrShortRecieve == "abgelehnt" ) 
				else if ( (mVehicEntrShortRecieve == "Y" && mVehicEntrShortRecDate != null) || mVehicEntrShortRecieve == "abgelehnt")
				{
					return mVehicEntrShortRecieve = "abgelehnt";
				}
				else
				{
					return mVehicEntrShortRecieve = String.Empty;
				}
			}
		}

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public string VehicEntrShortRecDate 
		{
			get 
			{
				return mVehicEntrShortRecDate;
			}
			set 
			{
				mVehicEntrShortRecDate = value;
			}
		}

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public string VehicEntrShortRecUser 
		{
			get 
			{
				return mVehicEntrShortRecUser;
			}
			set 
			{
				mVehicEntrShortRecUser = value;
			}
		}

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public string VehicleEntrLong
		{
			get 
			{
				return mVehicleEntrLong;
			}
			set 
			{
				mVehicleEntrLong = value;
			}
		}

		public String VehicleLongDesire 
		{
			get 
			{
				if (mVehicleEntrLong == "Y" || mVehicleEntrLong == "gewünscht" ) 
				{
					return mVehicleEntrLong = "gewünscht";
				} 
				else
				{
					return mVehicleEntrLong = String.Empty;
				}
			}
		}

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public string VehicEntrLongRecieve
		{
			get 
			{
				return mVehicEntrLongRecieve;
			}
			set 
			{
				mVehicEntrLongRecieve = value;
			}
		}

		public String VehicleLongAllowed 
		{
			get 
			{
				//if ( mVehicEntrLongRecieve == "N" || mVehicEntrLongRecieve == "akzeptiert" ) 
				if ( (mVehicEntrLongRecieve == "N" && mVehicEntrLongRecDate != null) || mVehicEntrLongRecieve == "akzeptiert")
				{
					return mVehicEntrLongRecieve = "akzeptiert";
				} 
				//else if ( mVehicEntrLongRecieve == "Y" || mVehicEntrLongRecieve == "abgelehnt" ) 
				else if ( (mVehicEntrLongRecieve == "Y" && mVehicEntrLongRecDate != null) || mVehicEntrLongRecieve == "abgelehnt")
				{
					return mVehicEntrLongRecieve = "abgelehnt";
				}
				else 
				{
					return mVehicEntrLongRecieve = String.Empty;
				}
			}
		}

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public string VehicEntrLongRecDate 
		{
			get 
			{
				return mVehicEntrLongRecDate;
			}
			set 
			{
				mVehicEntrLongRecDate = value;
			}
		}

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public string VehicEntrLongRecUser 
		{
			get 
			{
				return mVehicEntrLongRecUser;
			}
			set 
			{
				mVehicEntrLongRecUser = value;
			}
		}

		#endregion //End of Accessors

		#region Methods 

		#endregion // End of Methods

	}
}

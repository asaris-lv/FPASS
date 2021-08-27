using System;
using System.Text.RegularExpressions;

namespace Degussa.FPASS.Util.Validation
{
	/// <summary>
	/// Offers some static methods to check the format of Strings.
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
	public class StringValidation
	{
		#region Members

		/// <summary>  
		/// Used to hold the unique instance of StringValidation
		///</summary>
		private	static	StringValidation	mInstance = null;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Private constructor duo to singleton implementation.
		/// </summary>
		private StringValidation()
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

		/// <summary>
		/// Returns the one and only instance of this class.
		/// </summary>
		/// <returns>instance of String Validation</returns>
		public static StringValidation GetInstance() 
		{
			if ( null == mInstance ) 
			{
				mInstance = new StringValidation();
			}
			return mInstance;
		}
		
		/// <summary>
		/// Validates if given data is in format "MM.YYYY"
		/// </summary>
		/// <param name="pDateString"></param>
		/// <returns>true if given date is correct, false otherwise</returns>
		public bool IsMonthYearString(String pDateString) 
		{
			int year;
			int month;

			if ( null == pDateString ) 
			{
				return false;
			} 

			if ( pDateString.Length  != 7 ) 
			{
				return false;
			}

			if ( ! pDateString.Substring(2, 1).Equals(".") ) 
			{
				return false;
			}

			try 
			{
				year = Convert.ToInt32( pDateString.Substring(3, 4)  );
				month = Convert.ToInt32( pDateString.Substring(0, 2)  );
			} 
			catch ( FormatException ) 
			{
				return false;
			}

			if ( month < 1 || month > 12 ) 
			{
				return false;
			}

			if ( year < 1900 ) 
			{
				return false;
			}
			return true;
		}

		/// <summary>
		/// Validates if given data is in format "TT.MM.YYYY"
		/// </summary>
		/// <param name="pDateString"></param>
		/// <returns>true if given date is correct, false otherwise</returns>
		public bool IsDateString(String pDateString) 
		{
			int year;
			int month;
			int day;
			bool leapyear = false;
	
			if ( null == pDateString ) 
			{
				return false;
			} 

			if ( pDateString.Length  != 10 ) 
			{
				return false;
			}

			/// Patch 28.02.2005: if 3rd OR 6th position is not "."  then return false 
			if ( ! pDateString.Substring(2, 1).Equals(".") || ! pDateString.Substring(5, 1).Equals(".") ) 
			{
				return false;
			}

			try 
			{
				year = Convert.ToInt32( pDateString.Substring(6, 4 ) );
				month = Convert.ToInt32( pDateString.Substring(3, 2)  );
				day = Convert.ToInt32( pDateString.Substring(0, 2)  );
			} 
			catch ( FormatException ) 
			{
				return false;
			}

			if ( year < 1 ) 
			{
				return false;
			}

			if ( month < 1 || month > 12 ) 
			{
				return false;
			}

			if ( day < 1 ) 
			{
				return false;
			}

			if ( month == 1 || month == 3 || month == 5 || month == 7 ||
				month == 8 || month == 10 || month == 12 ) 
			{
				if ( day > 31 ) 
				{
					return false;
				}
			} 
			else 
			{
				if ( day > 30 ) 
				{
					return false;
				}
			}


			if ( ( year % 4 == 0 && year % 100 != 0 ) || year % 400 == 0 ) 
			{
				leapyear = true;
			} 
			else 
			{
				leapyear = false;
			}

			if ( month == 2 && day > 28 ) 
			{
				if ( leapyear && day <= 29 ) 
				{
					return true;
				} 
				else 
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Validates if given data from is smaller then date until
		/// </summary>
		/// <param name="pDateString"></param>
		/// <returns>true if given date is correct, false otherwise</returns>
		public bool IsDateValid(String pDateFrom,String pDateUntil) 
		{
			if (Convert.ToDateTime(pDateFrom) > Convert.ToDateTime(pDateUntil))
			{
				return false;
			}
			else
			{
				return true;
			}
		}


		#endregion // End of Methods
	}
}

using System;

namespace Degussa.FPASS.Bo.Reports
{
	/// <summary>
	/// Summary description for BOReportExContractor.
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
	public class BOReportExContractor
	{
		#region Members
		
		private decimal mExcoID;
		private string	mExcoName;
		private string	mExcoDebitNo;
		private string	mExcoCity;
		private string  mExcoPostcode;
		private string  mExcoCountry;
		private string  mExcoStreet;
		private string  mExcoSupervisor; 
		private string  mExcoTelephone;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public BOReportExContractor()
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

		public decimal ExcoID 
		{
			get 
			{
				return mExcoID;
			}
			set 
			{
				mExcoID = value;
			}
		} 
	
		
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

		public string ExcoCity 
		{
			get 
			{
				return mExcoCity;
			}
			set 
			{
				mExcoCity = value;
			}
		}

		public string ExcoPostcode 
		{
			get 
			{
				return mExcoPostcode;
			}
			set 
			{
				mExcoPostcode = value;
			}
		}

		public string ExcoCountry 
		{
			get 
			{
				return mExcoCountry;
			}
			set 
			{
				mExcoCountry = value;
			}
		}

		public string ExcoStreet 
		{
			get 
			{
				return mExcoStreet;
			}
			set 
			{
				mExcoStreet = value;
			}
		}

		public string ExcoSupervisor 
		{
			get 
			{
				return mExcoSupervisor;
			}
			set 
			{
				mExcoSupervisor = value;
			}
		}

		/// <summary>
		/// Gets or sets contractor's Debitor number
		/// </summary>
		public string ExcoDebitNo
		{
			get { return mExcoDebitNo; }
			set { mExcoDebitNo = value; }
		}

		public string ExcoTelephone
		{
			get { return mExcoTelephone; }
			set { mExcoTelephone = value; }
		}

		#endregion //End of Accessors

		#region Methods 

		#endregion // End of Methods


	}
}

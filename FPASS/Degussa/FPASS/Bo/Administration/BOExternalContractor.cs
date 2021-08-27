using System;

namespace Degussa.FPASS.Bo.Administration
{
	/// <summary>
	/// Summary description for BOExternalContractor.
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
	public class BOExternalContractor : AbstractAdminBO
	{
		#region Members

		private string	excoCity;
		private string	excoPostcode;
		private string	excoCountry;
		private string  excoStreet;
		private string  excoSUPERSUR;
		private string  excoSUPERFIR;
		private string	excoTEL;
		private string  excoFAX;
		private string  excoMobile;
		private decimal excoCoordinatorID;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public BOExternalContractor()
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
		public string PropexcoCity
		{
			get 
			{
				return excoCity;
			}
			set 
			{
				excoCity = value;
			}
		} 
		
		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public string PropexcoPostcode
		{
			get 
			{
				return excoPostcode;
			}
			set 
			{
				excoPostcode = value;
			}
		} 
		
		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public string PropexcoCountry
		{
			get 
			{
				return excoCountry;
			}
			set 
			{
				excoCountry = value;
			}
		} 
		
		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public string PropexcoStreet
		{
			get 
			{
				return excoStreet;
			}
			set 
			{
				excoStreet = value;
			}
		} 
		
		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public string PropexcoSUPERSUR
		{
			get 
			{
				return excoSUPERSUR;
			}
			set 
			{
				excoSUPERSUR = value;
			}
		} 

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public string PropexcoSUPERFIR
		{
			get 
			{
				return excoSUPERFIR;
			}
			set 
			{
				excoSUPERFIR = value;
			}
		} 

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public string PropexcoTEL
		{
			get 
			{
				return excoTEL;
			}
			set 
			{
				excoTEL = value;
			}
		} 

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public string PropexcoFAX
		{
			get 
			{
				return excoFAX;
			}
			set 
			{
				excoFAX = value;
			}
		} 

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public string PropexcoMobile
		{
			get 
			{
				return excoMobile;
			}
			set 
			{
				excoMobile = value;
			}
		} 

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public decimal PropexcoCoordinatorID
		{
			get 
			{
				return excoCoordinatorID;
			}
			set 
			{
				excoCoordinatorID = value;
			}
		} 

		#endregion //End of Accessors

		#region Methods 

		#endregion // End of Methods

	}
}

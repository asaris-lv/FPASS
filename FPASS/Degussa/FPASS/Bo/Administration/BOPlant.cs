using System;

namespace Degussa.FPASS.Bo.Administration
{
	/// <summary>
	/// Summary description for BOPlant.
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
	public class BOPlant 
	{
		#region Members

		private int plID;
		private string plName;
		private string	plNumber;
		private bool    plMastered;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public BOPlant()
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
		public int PLID
		{
			get 
			{
				return plID;
			}
			set 
			{
				plID = value;
			}
		} 

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public string PlName
		{
			get 
			{
				return plName;
			}
			set 
			{
				plName = value;
			}
		} 

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public bool PlMastered 
		{
			get 
			{
				return plMastered;
			}
			set 
			{
				plMastered = value;
			}
		} 

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public string PlNumber
		{
			// PLant number is varchar2 in the database
			get 
			{
				return plNumber;
			}
			set 
			{
				plNumber = value;
			}
		} 
		
		#endregion //End of Accessors

		#region Methods 

		#endregion // End of Methods

	}
}

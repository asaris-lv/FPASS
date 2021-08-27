using System;
using System.Data;

namespace Degussa.FPASS.Bo.Administration
{
	/// <summary>
	/// Holds the genaral properties ID and name for the business objects used in the Administration.
	/// Also the method Upadte is defined here
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
	public abstract class AbstractAdminBO
	{
		#region Members

		protected decimal adminBOID;
		/// <summary>
		/// used to hold the unique ID of the business object (maps to primary key in database)
		/// </summary>
		/// 

		protected string adminBOName;
		/// <summary>
		/// used to hold the Name of the business object (maps to field xxxx_name in database)
		/// </summary>
		/// 

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public AbstractAdminBO()
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
		public decimal PropAdminBOID
		{
			get 
			{
				return adminBOID;
			}
			set 
			{
				adminBOID = value;
			}
		} 
	
		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public string PropAdminBOName
		{
			get 
			{
				return adminBOName;
			}
			set 
			{
				adminBOName = value;
			}
		} 

		#endregion //End of Accessors

		#region Methods 

		#endregion // End of Methods

	}
}

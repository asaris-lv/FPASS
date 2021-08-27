using System;
using System.Data;

namespace Degussa.FPASS.Bo.Administration
{
	/// <summary>
	/// Summary description for BOCraft.
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
	public class BOCraft : AbstractAdminBO
	{
		#region Members

		private string	craNumber;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public BOCraft()
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
		public string PropcraNumber
		{
			// Craftno is a varchar2 in the DB
			get 
			{
				return craNumber;
			}
			set 
			{
				craNumber = value;
			}
		} 

		#endregion //End of Accessors

		#region Methods 

		internal int UpdateAdminBO(DataRow pDS)
		{
			// Assigne internal attributes to database fields.	
			
			try
			{
				this.adminBOID = Convert.ToDecimal(pDS[0]);
				this.adminBOName = pDS[3].ToString();
				this.craNumber = pDS[2].ToString();	
				// success
				return 0;
			}
			catch (Exception)
			{
				// Error, BO attributes could not be updated
				return -1;
			}
		}

		#endregion // End of Methods

	}
}

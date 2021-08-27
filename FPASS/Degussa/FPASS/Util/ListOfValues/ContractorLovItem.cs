using System;

namespace Degussa.FPASS.Util.ListOfValues
{
	/// <summary>
	/// Represents an external contractor, which can be displayed in a combobox or a listbox
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
	public class ContractorLovItem
	{
		#region Members

		/// <summary>
		/// Id (pk) of an external contractor
		/// </summary>
		private		decimal			mContractorID;
		/// <summary>
		/// Name of an external contractor
		/// </summary>
		private		String			mContractorName;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public ContractorLovItem()
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
		/// simple accessor
		/// </summary>
		public decimal ContractorID 
		{
			get 
			{
				return mContractorID;
			}
			set 
			{
				mContractorID = value;
			}
		}
 

		/// <summary>
		/// simple accessor
		/// </summary>
		public String ContractorName 
		{
			get 
			{
				return mContractorName;
			}
			set 
			{
				mContractorName = value;
			}
		} 


		#endregion //End of Accessors

		#region Methods 

		#endregion // End of Methods


	}
}

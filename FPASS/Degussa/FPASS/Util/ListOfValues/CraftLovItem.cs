using System;

namespace Degussa.FPASS.Util.ListOfValues
{
	/// <summary>
	///Represents a craft, which can be displayed in a combobox or a listbox
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
	public class CraftLovItem
	{
		#region Members

		/// <summary>
		/// Id (pk) of a craft
		/// </summary>
		private			decimal		mCraftID;
		/// <summary>
		/// Number of a craft
		/// </summary>
		private			String		mCraftNumber;
		/// <summary>
		/// Notation of a craft
		/// </summary>
		private			String		mCraftNotation;
		
		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public CraftLovItem()
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
		public decimal CraftID 
		{
			get 
			{
				return mCraftID;
			}
			set 
			{
				mCraftID = value;
			}
		}
 
		/// <summary>
		/// simple accessor
		/// </summary>
		public String CraftNumber 
		{
			get 
			{
				return mCraftNumber;
			}
			set 
			{
				mCraftNumber = value;
			}
		} 

		/// <summary>
		/// simple accessor
		/// </summary>
		public String CraftNotation 
		{
			get 
			{
				return mCraftNotation;
			}
			set 
			{
				mCraftNotation = value;
			}
		} 

		#endregion //End of Accessors

		#region Methods 

		#endregion // End of Methods


	}
}

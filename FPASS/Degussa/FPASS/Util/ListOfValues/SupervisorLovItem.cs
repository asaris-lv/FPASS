using System;

namespace Degussa.FPASS.Util.ListOfValues
{
	/// <summary>
	/// Represents an supervisor, which can be displayed in a combobox or a listbox
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
	public class SupervisorLovItem
	{
		#region Members

		/// <summary>
		/// Id (pk) of a supervisor
		/// </summary>
		private			decimal		mEXCOID;
		/// <summary>
		/// Name ( surname and firstname ) of a supervisor
		/// </summary>
		private			String		mSuperBothNamesAndEXCO;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public SupervisorLovItem()
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
		public decimal EXCOID 
		{
			get 
			{
				return mEXCOID;
			}
			set 
			{
				mEXCOID = value;
			}
		}
 
		/// <summary>
		/// simple accessor
		/// </summary>
		public String SuperBothNamesAndEXCO 
		{
			get 
			{
				return mSuperBothNamesAndEXCO;
			}
			set 
			{
				mSuperBothNamesAndEXCO = value;
			}
		} 

		#endregion //End of Accessors

		#region Methods 

		#endregion // End of Methods


	}
}

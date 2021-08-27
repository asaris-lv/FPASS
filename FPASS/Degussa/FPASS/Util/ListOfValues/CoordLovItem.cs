using System;

namespace Degussa.FPASS.Util.ListOfValues
{
	/// <summary>
	///  Represents an coordinator, which can be displayed in a combobox or a listbox
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
	public class CoordLovItem
	{
		#region Members

		/// <summary>
		/// Id (pk) of an coordinator
		/// </summary>
		private		decimal		mCoordID;
		/// <summary>
		/// Fullname ( surname and firstname ) of an coordinator
		/// </summary>
		private		String		mCoordFullName;
		/// <summary>
		/// Fullname including telphonenumber of an coordinator
		/// </summary>
		private		String		mCoordFullNameTel;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public CoordLovItem()
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
		public decimal CoordID 
		{
			get 
			{
				return mCoordID;
			}
			set 
			{
				mCoordID = value;
			}
		}
 
		/// <summary>
		/// simple accessor
		/// </summary>
		public String CoordFullName 
		{
			get 
			{
				return mCoordFullName;
			}
			set 
			{
				mCoordFullName = value;
			}
		} 

		/// <summary>
		/// simple accessor
		/// </summary>
		public String CoordFullNameTel 
		{
			get 
			{
				return mCoordFullNameTel;
			}
			set 
			{
				mCoordFullNameTel = value;
				int pos = mCoordFullNameTel.IndexOf("(");
				if ( pos > 0 ) 
				{
					mCoordFullName =  mCoordFullNameTel.Substring(0,pos);
				} 
				else 
				{
					mCoordFullName =  mCoordFullNameTel;
				}
			}
		} 



		#endregion //End of Accessors

		#region Methods 

		#endregion // End of Methods


	}
}

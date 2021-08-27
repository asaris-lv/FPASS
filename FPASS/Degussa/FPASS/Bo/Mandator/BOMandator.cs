using System;

namespace Degussa.FPASS.Bo.Mandator
{
	/// <summary>
	/// Summary description for BOMandator.
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
	public class BOMandator
	{
		#region Members

		private			int			mMandatorID;
		private			String		mMandatorName;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public BOMandator()
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

		public int MandatorID 
		{
			get 
			{
				return mMandatorID;
			}
			set 
			{
				mMandatorID = value;
			}
		}
 

		public String MandatorName 
		{
			get 
			{
				return mMandatorName;
			}
			set 
			{
				mMandatorName = value;
			}
		} 

		#endregion //End of Accessors

		#region Methods 

		#endregion // End of Methods


	}
}

using System;
using System.Collections;

using Degussa.FPASS.Bo.Administration;

namespace Degussa.FPASS.Bo.Administration
{
	/// <summary>
	/// Summary description for BOCoordinatorWrapper.
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
	public class BOCoordinatorWrapper
	{
		#region Members

		private decimal   mCurrentEXCOID;
		private ArrayList mArlBOExcoCoordinators;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public BOCoordinatorWrapper()
		{
			initialize(0, null);
		}

		public BOCoordinatorWrapper(decimal pCurrExcoID, ArrayList pARL)
		{
			initialize(pCurrExcoID, pARL);
		}

		#endregion //End of Constructors

		#region Initialization

		/// <summary>
		/// Initializes the members.
		/// </summary>
		private void initialize(decimal pCurrExcoID, ArrayList pARL)
		{
			this.mCurrentEXCOID			= pCurrExcoID;
			this.mArlBOExcoCoordinators = pARL;
		}	

		#endregion //End of Initialization

		#region Accessors 

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public decimal CurrentEXCOID 
		{
			get 
			{
				return mCurrentEXCOID;
			}
			set 
			{
				mCurrentEXCOID = value;
			}
		} 
	
		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public ArrayList ArlBOExcoCoordinators 
		{
			get 
			{
				return mArlBOExcoCoordinators;
			}
			set 
			{
				mArlBOExcoCoordinators = value;
			}
		}

		#endregion //End of Accessors

		#region Methods 

		#endregion // End of Methods

	}
}

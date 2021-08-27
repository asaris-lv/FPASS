using System;
using System.Collections;
using System.Data;

using Degussa.FPASS.Db;
using Degussa.FPASS.Util.Exceptions;
using Degussa.FPASS.Util.Messages;
using Degussa.FPASS.Util.UserManagement;

using de.pta.Component.DataAccess;
using de.pta.Component.Errorhandling;

namespace Degussa.FPASS.Util.ListOfValues
{
	/// <summary>
	/// Summary description for FPASSCraftLovSingleton.
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
	public class FPASSCraftLovSingleton
	{
		#region Members

		private static FPASSCraftLovSingleton mInstance = null;

		private ArrayList	 mArlCrafts;
		private Hashtable	 mHttCrafts;
		private ArrayList	 mArlCraftNotationShow;
		private CraftLovItem mCraftLovItem;

		private int mCurrentMandID;
		private const string CRAFT_QUERY     = "CraftsList"; 
		private const string CRAFT_MAND_PARA = ":CRA_MND_ID"; 

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public FPASSCraftLovSingleton()
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
			this.mCurrentMandID = UserManagementControl.getInstance().CurrentMandatorID;
			this.LoadCraftLovItems();
		}	

		#endregion //End of Initialization

		#region Accessors 

		internal ArrayList GetArrayListCrafts
		{
			get 
			{
				return mArlCrafts;
			}
		}

		#endregion //End of Accessors

		#region Methods 

		public static FPASSCraftLovSingleton GetInstance()
		{
			if(null == mInstance)
			{
				mInstance = new FPASSCraftLovSingleton();
			}
			return mInstance;
		}

		internal ArrayList GetCraftNotation(decimal pCraftID)
		{
			mArlCraftNotationShow = new ArrayList();
			if ( null != mCraftLovItem )
			{
				mCraftLovItem = (CraftLovItem) mHttCrafts[pCraftID];
				mArlCraftNotationShow.Add(mCraftLovItem);
			}
			return mArlCraftNotationShow;
		}


		private void LoadCraftLovItems()
		{
			
			// Get provider
			de.pta.Component.DbAccess.IProvider mProvider = DBSingleton.GetInstance().DataProvider;

			// Create the select command & fill Data Reader 
			IDbCommand selComm = mProvider.CreateCommand(CRAFT_QUERY);
			mProvider.SetParameter(selComm, CRAFT_MAND_PARA, mCurrentMandID);

			IDataReader mDR = mProvider.GetReader(selComm);
			mHttCrafts = new Hashtable();

			// Loop thru records and create an ArrayList of Craft BOs
			while (mDR.Read()) 
			{
				mCraftLovItem = new CraftLovItem();
				
				mCraftLovItem.CraftID		= Convert.ToDecimal(mDR["CRA_ID"]);
				mCraftLovItem.CraftNumber	= Convert.ToString(mDR["CRA_CRAFTNO"]);
				mCraftLovItem.CraftNotation = Convert.ToString(mDR["CRA_CRAFTNOTATION"]);
				
				mHttCrafts.Add(mCraftLovItem.CraftID, mCraftLovItem);
			}
			mDR.Close();

			mCraftLovItem = new CraftLovItem();
			mCraftLovItem.CraftID = 0;
			mCraftLovItem.CraftNotation = "";
			mCraftLovItem.CraftNumber = "";

			mArlCrafts = new ArrayList();
			mArlCrafts.Add(mCraftLovItem);

			mArlCrafts.AddRange( mHttCrafts.Values );

			
		}

		#endregion // End of Methods


	}
}

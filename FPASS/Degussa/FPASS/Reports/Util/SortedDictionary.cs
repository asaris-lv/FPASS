using System;
using System.Collections;

namespace Degussa.FPASS.Reports.Util
{
	/// <summary>
	/// SortedList that holds the position of all entered pair (key, value)
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
	public class SortedDictionary
	{
		#region Members

		private ArrayList mKeys;
		private SortedList mKeysValues;

		#endregion Members


		#region Constructors
        
		public SortedDictionary()
		{
			mKeys = new ArrayList();
			mKeysValues = new SortedList();
		}
		
		#endregion Constructors


		#region Accessors


		public ArrayList GetKeys()
		{
			return mKeys;
		}


		public string GetValue(string prmKey)
		{
			return mKeysValues[prmKey].ToString();
		}

		#endregion Accessors


		#region Methods

		public void Add(string prmKey, string prmValue)
		{
			this.mKeys.Add(prmKey);
			this.mKeysValues.Add(prmKey, prmValue);
		}


		public void clear()
		{
			this.mKeys.Clear();
			this.mKeysValues.Clear();
		}

		#endregion Methods

	} // SortedDictionary
} // namespace Degussa.FPASS.Reports.Util

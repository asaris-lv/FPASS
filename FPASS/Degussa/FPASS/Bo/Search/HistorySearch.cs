using System;

namespace Degussa.FPASS.Bo.Search
{
	/// <summary>
	/// Simple BO which acts as a container. Only used to display data in a grid
	/// in the form FrmHistory.  
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
	public class HistorySearch
	{
		#region Members

		/// <summary>
		/// Member attributes
		/// Last change 17.02.2004
		/// Attribute LastChange changed to type DateTime so it can be sorted chronologically	
		/// </summary>
		private decimal mHistId;
		private decimal mUserId;
		private string  mUserName;
		private DateTime mChangeDate;
		private string	mTableName;
		private string	mColumnName;
		private decimal	mRowId;
		private string	mOldValue;
		private string	mNewValue;
		private string	mDescription;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public HistorySearch()
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
		public decimal HISTID 
		{
			get 
			{
				return mHistId;
			}
			set 
			{
				mHistId = value;
			}
		} 

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public decimal UserId 
		{
			get 
			{
				return mUserId;
			}
			set 
			{
				mUserId = value;
			}
		} 
		
		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public string UserName
		{
			get 
			{
				return(mUserName);
			}
			set 
			{
				mUserName = value;
			}
		}

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public DateTime ChangeDate
		{
			get 
			{
				return(mChangeDate);
			}
			set 
			{
				mChangeDate = value;
			}
		}

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public string TableName
		{
			get 
			{
				return(mTableName);
			}
			set 
			{
				mTableName = value;
			}
		}

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public string ColumnName
		{
			get 
			{
				return(mColumnName);
			}
			set 
			{
				mColumnName = value;
			}
		}

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public decimal RowId 
		{
			get 
			{
				return mRowId;
			}
			set 
			{
				mRowId = value;
			}
		} 

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public string OldValue
		{
			get 
			{
				return(mOldValue);
			}
			set 
			{
				mOldValue = value;
			}
		}

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public string NewValue
		{
			get 
			{
				return(mNewValue);
			}
			set 
			{
				mNewValue = value;
			}
		}

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public string Description
		{
			get 
			{
				return(mDescription);
			}
			set 
			{
				mDescription = value;
			}
		}

		#endregion //End of Accessors

		#region Methods 

		#endregion // End of Methods


	}
}

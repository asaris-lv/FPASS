using System;

namespace Degussa.FPASS.Bo.Search
{
	/// <summary>
	/// Simple BO which acts as a container. Only used to display data in a grid 
	/// in the form FrmDynamicData. 
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
	public class DynamicDataSearch
	{
		#region Members

		private decimal mDynamicId;
		private decimal	mTk;
		private decimal mPersNo;
		private string	mSurname;
		private string	mFirstname;
		private string	mExcontractor;
		private string	mDate;
		private string  mTime;
		private string	mEntry;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public DynamicDataSearch()
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
		public decimal DynamicId 
		{
			get 
			{
				return mDynamicId;
			}
			set 
			{
				mDynamicId = value;
			}
		}
 
		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public decimal Tk 
		{
			get 
			{
				return mTk;
			}
			set 
			{
				mTk = value;
			}
		} 

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public decimal PersNo 
		{
			get 
			{
				return mPersNo;
			}
			set 
			{
				mPersNo = value;
			}
		} 

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public string Surname
		{
			get 
			{
				return(mSurname);
			}
			set 
			{
				mSurname = value;
			}
		}

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public string Firstname
		{
			get 
			{
				return(mFirstname);
			}
			set 
			{
				mFirstname = value;
			}
		}

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public string Excontractor
		{
			get 
			{
				return(mExcontractor);
			}
			set 
			{
				mExcontractor = value;
			}
		}

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public string Date
		{
			get 
			{
				return(mDate);
			}
			set 
			{
				mDate = value;
			}
		}

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public string Time
		{
			get 
			{
				return(mTime);
			}
			set 
			{
				mTime = value;
			}
		}

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public string Entry
		{
			get 
			{
				return(mEntry);
			}
			set 
			{
				mEntry = value;
			}
		}

		#endregion //End of Accessors

		#region Methods 

		#endregion // End of Methods

	}
}

using System;

namespace Degussa.FPASS.Bo.Mandator
{
	/// <summary>
	/// Used to holf information about the current user and the current mandator
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
	public class BOUserMndt
	{
		#region Members

		private			decimal     mUserID;
		private			int			mMandatorID;
		private			String		mMandatorName;
		private			bool		mHasZKS;

		/// <summary>
		/// flag indicating if user is can see generated pass in acrobat before printing
		/// default is true
		/// </summary>
		private					bool						mPreviewPass;

		/// <summary>
		/// determines how many pass(es) are printed automatically
		/// </summary>
		private					int							mNumberOfPrintedPass;
		/// <summary>
		/// Coworkers are shown in deletelist mEntranceFlowTime days before validUNTIL expires 
		/// </summary>
		private					int							mEntranceFlowTime;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public BOUserMndt()
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
			mPreviewPass         = true;
			mNumberOfPrintedPass = 1;
			mEntranceFlowTime    = 10;
		}	

		#endregion //End of Initialization

		#region Accessors 

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public bool HasZKS 
		{
			get 
			{
				return mHasZKS;
			}
			set 
			{
				mHasZKS = value;
			}
		}

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
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

		/// <summary>
		/// Simple getter and setter.
		/// </summary>
		public decimal UserID 
		{
			get 
			{
				return mUserID;
			}
			set 
			{
				mUserID = value;
			}
		}
 
		/// <summary>
		/// Simple getter and setter.
		/// </summary>
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

		public bool	PreviewPass
		{ 
			get 
			{
				return mPreviewPass;
			}
			set 
			{ 
				mPreviewPass = value;
			}
		}


		public int NumberOfPrintedPass
		{ 
			get 
			{
				return mNumberOfPrintedPass;
			}
			set
			{
				mNumberOfPrintedPass = value;
			}
		}

		public int EntranceFlowTime
		{ 
			get 
			{
				return mEntranceFlowTime;
			}
			set
			{
				mEntranceFlowTime = value;
			}
		}

		#endregion //End of Accessors

		#region Methods 

		#endregion // End of Methods

	}
}

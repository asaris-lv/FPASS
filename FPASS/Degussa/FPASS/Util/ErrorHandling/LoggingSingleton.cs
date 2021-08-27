using System;

using de.pta.Component.Logging.Log4NetWrapper;

namespace Degussa.FPASS.Util.ErrorHandling
{
	/// <summary>
	/// Provides logging for UIPublishers.
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
	public class LoggingSingleton
	{
		#region Members

		// The only instance of this class.
		private static LoggingSingleton mInstance = null;

		// An instance of the logger
		private Logger    mLogger    = null;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		private LoggingSingleton()
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
			mLogger = new Logger("FPASS");
            
		}	

		#endregion //End of Initialization

		#region Accessors 

		/// <summary>
		/// Gets the current Logger-instance.
		/// </summary>
		/// <value>The current Logger.</value>
		public Logger Log
		{
			get { return mLogger; }
		}

        /// <summary>
        /// Returns true if debug is enabled.
        /// </summary>
        /// <param name="pMsg">The message text.</param>
        public bool IsDebugEnabled
        {
            get { return (mLogger.IsDebugEnabled); }
        }

		#endregion //End of Accessors

		#region Methods
 
		/// <summary>
		/// Returns the only instance of the class
		/// </summary>
		/// <returns></returns>
		public static LoggingSingleton GetInstance()
		{
			if(null == mInstance)
			{
				mInstance = new LoggingSingleton();
			}
			return mInstance;
		}

		#endregion // End of Methods


	}
}

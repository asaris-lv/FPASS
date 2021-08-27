using System;

namespace de.pta.Component.Errorhandling
{
	/// <summary>
	/// Implements the UI processing for warning messages.
	/// </summary>
	/// <remarks>Each warning exception must keep a reference to this.
	/// Therefore it is a singleton to save some memory space.</remarks>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> D.Hassloecher, PTA GmbH
	/// <b>Date:</b> Apr/02/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	public class UIWarningDelegate : UIExceptionDelegate
	{
		#region Members

		private static UIWarningDelegate instance = null;

		#endregion //End of Members

		#region Constructors

		private UIWarningDelegate() : base()
		{
			// Not public since outside no one is allowed to instantiate.
		}

		#endregion //End of Constructors

		#region Initialization

		/// <summary>
		/// Returns the one and only instance of this class.
		/// </summary>
		/// <returns>delegator for warnings</returns>
		public static UIWarningDelegate GetInstance() 
		{
			if (null == instance) 
			{
				instance = new UIWarningDelegate();
			}
			return instance;
		}

		#endregion //End of Initialization

		#region Accessors 
		#endregion //End of Accessors

		#region Methods

		/// <summary>
		/// Knows how to publish warning messages to the UI.
		/// </summary>
		public override void Publish(BaseUIException exception) 
		{
			Publisher.Publish(exception);		
		}


		#endregion // Methods.
	}
}
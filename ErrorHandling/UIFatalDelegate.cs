using System;


namespace de.pta.Component.Errorhandling
{
	/// <summary>
	/// Implements the UI processing for fatal messages.
	/// </summary>
	/// <remarks>Each fatal exception must keep a reference to this.
	/// Therefore it is a singleton to save some memory space.</remarks>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> D.Hassloecher, PTA GmbH
	/// <b>Date:</b> Apr/02/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	public class UIFatalDelegate : UIExceptionDelegate
	{
		#region Members

		private static UIFatalDelegate instance = null;

		#endregion //End of Members

		#region Constructors

		private UIFatalDelegate() : base()
		{
			// Not public since outside no one is allowed to instantiate.
		}

		#endregion //End of Constructors

		#region Initialization

		/// <summary>
		/// Returns the one and only instance of this class.
		/// </summary>
		/// <returns>delegator for warnings</returns>
		public static UIFatalDelegate GetInstance() 
		{
			if (null == instance) 
			{
				instance = new UIFatalDelegate();
			}
			return instance;
		}

		#endregion //End of Initialization

		#region Accessors 
		#endregion //End of Accessors

		#region Methods

		/// <summary>
		/// Knows how to publish fatal messages to the UI.
		/// </summary>
		public override void Publish(BaseUIException exception) 
		{
			Publisher.Publish(exception);
			// Application.exit(); for windows applications
			
		}

		#endregion // Methods
	}
}
using System;

namespace de.pta.Component.Errorhandling
{
	/// <summary>
	/// Implements the UI processing for error messages.
	/// </summary>
	/// <remarks>Each error exception must keep a reference to this.
	/// Therefore it is a singleton to save some memory space.</remarks>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> D.Hassloecher, PTA GmbH
	/// <b>Date:</b> Apr/02/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	public class UIErrorDelegate : UIExceptionDelegate
	{
		#region Members

		private static UIErrorDelegate instance = null;

		#endregion //End of Members

		#region Constructors

		private UIErrorDelegate() : base()
		{
			// Not public since outside no one is allowed to instantiate.
		}

		#endregion //End of Constructors

		#region Initialization

		/// <summary>
		/// Returns the one and only instance of this class.
		/// </summary>
		/// <returns>delegator for warnings</returns>
		public static UIErrorDelegate GetInstance() 
		{
			if (null == instance) 
			{
				instance = new UIErrorDelegate();
			}
			return instance;
		}

		#endregion //End of Initialization

		#region Accessors 
		#endregion //End of Accessors

		#region Methods

		/// <summary>
		/// Knows how to publish error messages to the UI.
		/// </summary>
		public override void Publish(BaseUIException exception) 
		{
			Publisher.Publish(exception);
		}


		#endregion // Methods
	}
}
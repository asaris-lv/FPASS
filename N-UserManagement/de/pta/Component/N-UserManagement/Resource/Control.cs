using System;
using de.pta.Component.N_UserManagement.Exceptions;

namespace de.pta.Component.N_UserManagement.Resource
{
	/// <summary>
	/// Class for authorizable Control objects.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> R.Weiﬂ, PTA GmbH
	/// <b>Date:</b> Aug/12/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class Control : Resource
	{
		#region Members

		/// <summary>
		/// The dialog or report the control belongs to.
		/// </summary>
		Function	mParent;

		/// <summary>
		/// The function called by this control.
		/// </summary>
		Function	mCalledFunction;

		#endregion //End of Members

		#region Constructors

		public Control()
		{
			initialize();
		}

		#endregion //End of Constructors

		#region Initialization

		private void initialize()
		{
		}	

		#endregion //End of Initialization

		#region Accessors 

		/// <summary>
		/// Acessor for the function the control belongs to.
		/// </summary>
		public Function Parent
		{
			get
			{
				return mParent;
			}
			set
			{
				if (!(value is Dialog) && 
					!(value is Report)) 
				{
					throw new UserManagementException("ERROR_INVALID_PARENT");
				}
				mParent = value;
			}
		}
		/// <summary>
		/// Accessor for the Function called by this control.
		/// </summary>
		public Function CalledFunction
		{
			get
			{
				return mCalledFunction;
			}
			set
			{
				mCalledFunction = value;
			}
		}

		#endregion //End of Accessors

	}
}

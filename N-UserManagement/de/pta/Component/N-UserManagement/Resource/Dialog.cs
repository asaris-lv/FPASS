using System;
using System.Collections;

namespace de.pta.Component.N_UserManagement.Resource
{
	/// <summary>
	/// Class for authorizable dialog objects. Dialogs contain a container includings its 
	/// controls (widgets)
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> R.Weiﬂ, PTA GmbH
	/// <b>Date:</b> Aug/12/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class Dialog : Function
	{
		#region Members

		/// <summary>
		/// Container for controls of a dialog.
		/// </summary>
		private ArrayList	mControls;

		#endregion //End of Members

		#region Constructors

		public Dialog()
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
		/// Accessor for the controls of this dialog.
		/// </summary>
		public ArrayList Controls
		{
			get
			{
				return mControls;
			}
			set
			{
				mControls = value;
			}
		}

		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// Adds a new control to the dialog.
		/// </summary>
		/// <param name="pControl">New control.</param>
		public void addControl(Control pControl) 
		{
			if (this.Controls == null) 
			{
				mControls = new ArrayList();
			}
			mControls.Add(pControl);
		}

		/// <summary>
		/// Deletes a control from the dialog container.
		/// </summary>
		/// <param name="pControl">Control to be deleted.</param>
		public void deleteControl(Control pControl)
		{
			if (this.Controls != null)
			{
				mControls.Remove(pControl);
			}
		}

		#endregion // End of Methods

	}
}

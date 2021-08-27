using System;
using System.Collections;

namespace de.pta.Component.N_UserManagement.DataAccess
{
	/// <summary>
	/// Abstract class for getting resources from XML or database
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> R.Weiﬂ, PTA GmbH
	/// <b>Date:</b> Sep/12/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal abstract class ResourceBuilder 
	{

		#region Constructors

		public ResourceBuilder()
		{
			this.initialize();
		}

		#endregion // End of Constructors

		#region Initialize

		private void initialize()
		{
		}

		#endregion // End of initialize

		#region Methods

		/// <summary>
		/// Returns the list of resources
		/// </summary>
		/// <returns></returns>
		public abstract Hashtable getResources();

		#endregion // End of Methods


	}//end ResourceBuilder
}//end namespace Internal
using System;
using System.Collections;

namespace de.pta.Component.N_UserManagement.DataAccess
{
	/// <summary>
	/// class for getting Resources from database
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> R.Weiﬂ, PTA GmbH
	/// <b>Date:</b> Sep/12/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class RdbResourceBuilder : ResourceBuilder 
	{

		#region Constructors

		public RdbResourceBuilder()
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
		public override Hashtable getResources() 
		{
			return null;
		}

		#endregion // End of Methods

	}//end RdbResourceBuilder
}//end namespace Internal
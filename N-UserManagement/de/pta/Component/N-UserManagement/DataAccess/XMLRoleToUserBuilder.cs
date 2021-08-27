using System;
using System.Collections;
using System.Windows.Forms;

using de.pta.Component.Common;
using de.pta.Component.N_UserManagement.Internal;
using de.pta.Component.N_UserManagement.Exceptions;
using de.pta.Component.N_UserManagement.Vo;

namespace de.pta.Component.N_UserManagement.DataAccess
{
	/// <summary>
	/// class for getting user-Role assigments from XML file
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> R.Weiﬂ, PTA GmbH
	/// <b>Date:</b> Sep/29/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class XMLRoleToUserBuilder : RoleToUserBuilder 
	{

		#region Members

		/// <summary>
		/// Section containing the data for the assignment between roles and users
		/// </summary>
		private String XML_SECTION = "application/configuration/UserManagementData/UserRoles";

		#endregion // End of Members

		#region Constructors

		public XMLRoleToUserBuilder()
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
		/// Returns the list of assignments between roles and users referring to search criteria
		/// </summary>
		/// <param name="pRoleSearchCriteria">Search Criteria -> TODO</param>
		/// <returns>Array of RoleToUserVO.</returns>
		public override ArrayList getAssignments(RoleToUserVO pSearchCriteria)
		{
			return null;
		}

		#endregion // End of Methods

	}//end XMLRoleToUserBuilder
}//end namespace DataAccess
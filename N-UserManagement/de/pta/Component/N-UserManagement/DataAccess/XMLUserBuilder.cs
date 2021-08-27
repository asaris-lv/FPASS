using System;
using System.Collections;
using System.Windows.Forms;

using de.pta.Component.Common;
using de.pta.Component.N_UserManagement.Internal;
using de.pta.Component.N_UserManagement.Exceptions;
using de.pta.Component.N_UserManagement.Authorization;

namespace de.pta.Component.N_UserManagement.DataAccess
{
	/// <summary>
	/// class for getting Users from XML file
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> R.Weiﬂ, PTA GmbH
	/// <b>Date:</b> Sep/29/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class XMLUserBuilder : UserBuilder 
	{

		#region Members
		
		/// <summary>
		/// Section containing the data for Users
		/// </summary>
		private readonly String XML_Section = "application/configuration/UserManagementData/Users";

		#endregion // End of Members

		#region Constructors

		public XMLUserBuilder()
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
		/// Returns the users that can be identified by user id and password
		/// </summary>
		/// <param name="pUserId">Userid typed in</param>
		/// <param name="pPassword">Password typed in</param>
		/// <returns>Array of UserVO</returns>
		internal override ArrayList getUserByLogin(String pUserId, String pPassword) 
		{
			// TODO
			return null;
		}

		/// <summary>
		/// Returns the list of users referring to search criteria
		/// </summary>
		/// <param name="pUserSearchCriteria">Search Criteria -> TODO</param>
		/// <returns>Array of UserVO. Key is internal user id.</returns>
		public override Hashtable getUsers(IUser pUserSearchCriteria)
		{
			return null;
		}

		#endregion // End of Methods

	}//end XMLUserBuilder
}//end namespace DataAccess
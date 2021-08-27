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
	/// class for getting Roles from XML file
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> R.Weiﬂ, PTA GmbH
	/// <b>Date:</b> Sep/12/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class XMLRoleBuilder : RoleBuilder 
	{

		#region Members
		
		/// <summary>
		/// Section containing the data for roles 
		/// </summary>
		private readonly String XML_SECTION = "application/configuration/UserManagementData/Roles";

		#endregion // End of Members

		#region Constructors

		public XMLRoleBuilder()
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
		/// Returns the list of roles referring to search criteria
		/// </summary>
		/// <param name="pRoleSearchCriteria">Search Criteria -> TODO</param>
		/// <returns>Array of RoleVO. Key is internal role id.</returns>
		public override Hashtable getRoles(IRole pRoleSearchCriteria) 
		{
			XmlUserManagementProcessor	configProcessor;
			ConfigReader				configReader;
			Hashtable					roles;

			try 
			{
				configProcessor = new XmlUserManagementProcessor();
				configReader = ConfigReader.GetInstance();
				configReader.ApplicationRootPath = Application.StartupPath;
				configReader.ReadConfig(XML_SECTION, configProcessor);
				roles = configProcessor.Roles;

			} 
			catch (Exception e)
			{
				String t = e.Message;
				throw new DataAccessException("ERROR_INVALID_CONFIGURATION", e);
			}
			return roles;
		}

		#endregion // End of Methods

	}//end XMLRoleBuilder
}//end namespace Internal
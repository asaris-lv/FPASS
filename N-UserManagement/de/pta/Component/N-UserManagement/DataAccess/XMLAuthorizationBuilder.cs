using System;
using System.Collections;
using System.Windows.Forms;

using de.pta.Component.Common;
using de.pta.Component.N_UserManagement.Internal;
using de.pta.Component.N_UserManagement.Exceptions;
using de.pta.Component.N_UserManagement.Authorization;
using de.pta.Component.N_UserManagement.Vo;

namespace de.pta.Component.N_UserManagement.DataAccess
{
	/// <summary>
	/// class for authorizations Roles from XML file
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> R.Weiﬂ, PTA GmbH
	/// <b>Date:</b> Sep/26/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class XMLAuthorizationBuilder : AuthorizationBuilder 
	{

		#region Members

		/// <summary>
		/// Section containing the data for authorizations
		/// </summary>
		private String XML_SECTION = "application/configuration/UserManagementData/Authorizations";

		#endregion // End of Members

		#region Constructors

		public XMLAuthorizationBuilder()
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
		/// Returns the list of authorizations referring to search criteria from database
		/// </summary>
		/// <param name="pAuthorizationSearchCriteria">Search Criteria -> TODO</param>
		/// <returns>Array of AuthorizationVO</returns>
		public override ArrayList getAuthorizations(AuthorizationVO pAuthorizationSearchCriteria)
		{
			XmlUserManagementProcessor	configProcessor;
			ConfigReader				configReader;
			ArrayList					authorizations;
			ArrayList					resultSet;

			try 
			{
				configProcessor = new XmlUserManagementProcessor();
				configReader = ConfigReader.GetInstance();
				configReader.ApplicationRootPath = Application.StartupPath;
				configReader.ReadConfig(XML_SECTION, configProcessor);
				authorizations = configProcessor.Authorizations;

				resultSet = new ArrayList();
				foreach(AuthorizationVO authorization in authorizations) 
				{
					if (authorization.RoleId.Equals(pAuthorizationSearchCriteria.RoleId == 0 ? authorization.RoleId : pAuthorizationSearchCriteria.RoleId) &&
						authorization.ResourceId.Equals(pAuthorizationSearchCriteria.ResourceId == 0 ? authorization.ResourceId : pAuthorizationSearchCriteria.ResourceId)) 
					{
						resultSet.Add(authorization);
					}
				}

			} 
			catch (Exception e)
			{
				String t = e.Message;
				throw new DataAccessException("ERROR_INVALID_CONFIGURATION", e);
			}
			return resultSet;
		}

		#endregion // End of Methods

	}//end XMLAuthorizationBuilder
}//end namespace DataAccess
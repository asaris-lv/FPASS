using System;
using System.Collections;
using System.Windows.Forms;

using de.pta.Component.Common;
using de.pta.Component.N_UserManagement.Exceptions;
using de.pta.Component.N_UserManagement.Internal;

namespace de.pta.Component.N_UserManagement.DataAccess
{
	/// <summary>
	/// class for getting Resources from XML file
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> R.Weiﬂ, PTA GmbH
	/// <b>Date:</b> Sep/12/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class XMLResourceBuilder : ResourceBuilder 
	{

		#region Members
		
		/// <summary>
		/// Section containing the data for user management
		/// </summary>
		private readonly String XML_SECTION = "application/configuration/UserManagementData/Resources";

		#endregion // End of Members

		#region Constructors

		public XMLResourceBuilder()
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
			XmlUserManagementProcessor	configProcessor;
			ConfigReader				configReader;
			Hashtable					resources;

			try 
			{
				configProcessor = new XmlUserManagementProcessor();
				configReader = ConfigReader.GetInstance();
				configReader.ApplicationRootPath = Application.StartupPath;
				configReader.ReadConfig(XML_SECTION, configProcessor);
				resources = configProcessor.Resources;
			} 
			catch (Exception e)
			{
				String t = e.Message;
				throw new DataAccessException("ERROR_INVALID_CONFIGURATION", e);
			}
			return resources;
		}

		#endregion // End of Methods

	}//end XMLResourceBuilder
}//end namespace Internal
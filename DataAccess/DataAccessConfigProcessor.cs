using System;
using System.Collections;

using de.pta.Component.Common;

namespace de.pta.Component.DataAccess.Internal
{
	/// <summary>
	/// Implements the IConfigProcessor functionality.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> M.Keller, PTA GmbH
	/// <b>Date:</b> Apr/02/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class DataAccessConfigProcessor : IConfigProcessor
	{
		#region Members

		private const String DATAACCESS					= "DATAACCESS";
		private const String SYSTEM						= "SYSTEM";
		private const String NAME						= "NAME";
		private const String SERVERNAME					= "SERVERNAME";
		private const String DATABASE					= "DATABASE";
		private const String DEFAULTUSER				= "DEFAULTUSER";
		private const String DOMAIN						= "DOMAIN";
		private const String DEFAULTPASSWORD			= "DEFAULTPASSWORD";
		private const String ENCRYPTED					= "ENCRYPTED";
		private const String CONNECTSTRING				= "CONNECTSTRING";
		private const String XMLTEMPPATH				= "XMLTEMPPATH";
		private const String CONNECTION					= "CONNECTION";
		private const String TYPE						= "TYPE";
		private const String CLEANUP					= "CLEANUP";
		private const String CLEANUPUSER				= "USER";
		private const String CLEANUPHOST				= "HOST";
		private const String CLEANUPPWD					= "PWD";
		private const String TIMEINSECONDS				= "TIMEINSECONDS";
		private const String CLEANALLTIME				= "CLEANALLTIME";
		private const String APPLICATION				= "APPLICATION";
		private const String APPLICATIONMAXRECORDS		= "APPLICATIONMAXRECORDS";
		private const String DATABLOCKSIZE				= "DATABLOCKSIZE";
		private const String DATAPAGESIZE				= "DATAPAGESIZE";
		private const String TURBOMODEFLAG				= "TURBOMODEFLAG";
		private const String TURBOMODEMAXRECORDS		= "TURBOMODEMAXRECORDS";
		private const String QUERIES					= "QUERIES";
		private const String QUERY						= "QUERY";
		private const String ID							= "ID";
		private const String STATEMENT					= "STATEMENT";
		private const String ATTRIBUTELISTS				= "ATTRIBUTELISTS";
		private const String ATTRIBUTELIST				= "ATTRIBUTELIST";
		private const String ATTRIBUTES					= "ATTRIBUTES";
		private const String ATTRIBUTE					= "ATTRIBUTE";
		private const String COLNAME					= "COLNAME";
		private const String COLWIDTHPX					= "COLWIDTHPX";
		private const String DATATYPE					= "DATATYPE";
		private const String ATTRIBLIST					= "ATTRIBLIST";
		private const String DIMENSIONATTRIBUTE		    = "DIMENSIONATTRIBUTE";
		private const String PARAMETERLISTS				= "PARAMETERLISTS";
		private const String PARAMETERLIST				= "PARAMETERLIST";
		private const String CODE						= "CODE";
		private const String PARAMETERS					= "PARAMETERS";
		private const String PARAMETER					= "PARAMETERM";
		private const String PARALIST					= "PARALIST";
		private const String OPERATOR					= "OPERATOR";
		private const String PATHISRELATIVE				= "PATHISRELATIVE";
		private const String DIFFERENTDATAPAGESIZEUSED	= "DIFFERENTDATAPAGESIZEUSED";

		private bool dataAccessFound;
		private bool systemFound;
		private bool applicationFound;
		private bool queriesFound;
		private bool queryFound;
		private bool attributeListsFound;
		private bool attributesFound;
		private bool parameterListsFound;
		private bool parametersFound;
		private String pathIsRelative;
		private QueryDefinition queryDef;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Constructs the object.
		/// </summary>
		public DataAccessConfigProcessor()
		{
			initialize();
		}

		#endregion //End of Constructors

		#region Initialization

		private void initialize()
		{
			// Initializes the members.
			dataAccessFound		= false;
			systemFound			= false;
			applicationFound	= false;
			queriesFound		= false;
			queryFound			= false;
			attributeListsFound	= false;
			attributesFound		= false;
			parameterListsFound	= false;
			parametersFound		= false;
			pathIsRelative		= "";
			queryDef		    = null;
		}

		#endregion //End of Initialization

		#region Accessors 
		#endregion //End of Accessors

		#region Methods

		/// <summary>
		/// Processes a ConfigNode of type 'item'.
		/// </summary>
		/// <param name="cNode"></param>
		public void  ProcessConfigItem(ConfigNode cNode)
		{
			// Ensure that we are within the right node.
			if ( !dataAccessFound )
			{
				throw new DataAccessException("ERROR_DATAACCESS_INVALID_CONFIGURATION");
			}

			// System block opened.
			if ( systemFound )
			{
				if ( cNode.NodeName.Equals(XMLTEMPPATH) )
				{
					//if path name is defined as "relative", the path is located under the predefined application
					//path - otherwise the path is defined as an absolut path
					pathIsRelative = cNode.NodeAttributes[PATHISRELATIVE].ToString();
					if (Boolean.TrueString.ToUpper() == pathIsRelative.ToUpper())
					{
						//Application path and relative path
						DataAccessConfiguration.GetInstance().XmlTempPath	  = ConfigReader.GetInstance().GetConfigPath() + cNode.NodeAttributes[NAME].ToString();
						DataAccessConfiguration.GetInstance().XmlTempPathHttp = ConfigReader.GetInstance().RelativeApplicationPath + cNode.NodeAttributes[NAME].ToString();
					}
					else
					{
						//path is absolut defined e.g. Server\\Volume\Path
						DataAccessConfiguration.GetInstance().XmlTempPath	  = cNode.NodeAttributes[NAME].ToString();
						DataAccessConfiguration.GetInstance().XmlTempPathHttp = ConfigReader.GetInstance().RelativeApplicationPath + cNode.NodeAttributes[NAME].ToString();
					}
				}				
				
				if ( cNode.NodeName.Equals(CONNECTION) )
				{
					DBConnectionInfo dbConnection = new DBConnectionInfo();
					dbConnection.ConnectionId	  = cNode.NodeAttributes[TYPE].ToString();
					dbConnection.ServerName		  = cNode.NodeAttributes[SERVERNAME].ToString();
					dbConnection.Database		  = cNode.NodeAttributes[DATABASE].ToString();
					dbConnection.DefaultUser	  = cNode.NodeAttributes[DEFAULTUSER].ToString();
					dbConnection.Domain			  = cNode.NodeAttributes[DOMAIN].ToString();
					dbConnection.DefaultPassword  = cNode.NodeAttributes[DEFAULTPASSWORD].ToString();

					// Verify if encryption is used. If the entry "Encrypted" is not found in
					// the configuration, no encryption will be used. That is the reason, why
					// the boolean must be initialized with false.
					bool encrypted = false;
					if ( cNode.NodeAttributes.ContainsKey(ENCRYPTED) )
					{
						encrypted = cNode.NodeAttributes[ENCRYPTED].ToString().ToUpper().
							Equals(Boolean.TrueString.ToUpper());
					}

					// If encryption is used, use the CryptUtil to decrypt.
					// Otherwise use the string direct from the configuration.
					if ( encrypted )
					{
						dbConnection.ConnectString = CryptoUtil.Decrypt(cNode.NodeAttributes[CONNECTSTRING].ToString());
					}
					else
					{
						dbConnection.ConnectString = cNode.NodeAttributes[CONNECTSTRING].ToString();
					}

					// set the connection infos to the connection factory.
					DBConnectionInfoFactory.GetInstance().RegisterDBConnectionInfo(dbConnection, dbConnection.ConnectionId);
				}

				if ( cNode.NodeName.Equals(CLEANUP) )
				{
					DataAccessConfiguration.GetInstance().CleanUpUser	= cNode.NodeAttributes[CLEANUPUSER].ToString();
					DataAccessConfiguration.GetInstance().CleanUpHost	= cNode.NodeAttributes[CLEANUPHOST].ToString();
					DataAccessConfiguration.GetInstance().CleanUpPwd	= cNode.NodeAttributes[CLEANUPPWD].ToString();
					DataAccessConfiguration.GetInstance().TimeInSeconds	= Int32.Parse(cNode.NodeAttributes[TIMEINSECONDS].ToString());
					DataAccessConfiguration.GetInstance().ClearAllTime	= cNode.NodeAttributes[CLEANALLTIME].ToString();
				}

				// systemFound
			} 

			// Application block opened.
			if ( applicationFound )
			{
				if ( cNode.NodeName.Equals(APPLICATIONMAXRECORDS) )
				{
					DataAccessConfiguration.GetInstance().ApplicationMaxRecords = Int32.Parse(cNode.NodeValue);
				}

				if ( cNode.NodeName.Equals(DATABLOCKSIZE) )
				{
					DataAccessConfiguration.GetInstance().DataBlockSize = Int32.Parse(cNode.NodeValue);
				}

				if ( cNode.NodeName.Equals(DATAPAGESIZE) )
				{
					DataAccessConfiguration.GetInstance().DataPageSize = Int32.Parse(cNode.NodeValue);
				}

				if ( cNode.NodeName.Equals(TURBOMODEFLAG) )
				{
					DataAccessConfiguration.GetInstance().TurboModeFlag = (cNode.NodeValue.ToUpper() == Boolean.TrueString.ToUpper());
				}

				if ( cNode.NodeName.Equals(TURBOMODEMAXRECORDS) )
				{
					DataAccessConfiguration.GetInstance().TurboModeMaxRecords = Int32.Parse(cNode.NodeValue);
				}

				// applicationFound
			} 

			// Queries block opened.
			if ( queriesFound )
			{
				// Query block opened
				if ( queryFound )
				{
					// Evaluate the query information and set to the query definition.
					if ( cNode.NodeName.Equals(QUERY) )
					{
						queryDef.QueryID			= cNode.NodeAttributes[ID].ToString();
						queryDef.QueryName			= cNode.NodeAttributes[NAME].ToString();
						queryDef.DatabaseTypeInfo	= cNode.NodeAttributes[TYPE].ToString();
						queryDef.SelectStatement	= cNode.NodeAttributes[STATEMENT].ToString();
			
						// This option only used, when value was separatly filled - overwrites application setting for specificc query definition
						if ( !cNode.NodeAttributes[DIFFERENTDATAPAGESIZEUSED].ToString().Equals(String.Empty) )
						{
							queryDef.DifferentDataPageSizeUsed = Convert.ToInt32(cNode.NodeAttributes[DIFFERENTDATAPAGESIZEUSED].ToString());
						}
					}

					// Evaluate the attributeLists and set to the query definition.
					if ( attributeListsFound )
					{
						queryDef.AddAttributeLists(cNode.NodeAttributes[ID].ToString());
					}

					// Evaluate the attributes and set to the query definition.
					if ( attributesFound )
					{
						queryDef.AddAttribute(cNode.NodeAttributes[ID].ToString(),
											  cNode.NodeAttributes[COLNAME].ToString(),
											  Int32.Parse(cNode.NodeAttributes[COLWIDTHPX].ToString()),
											  cNode.NodeAttributes[DATATYPE].ToString(),
											  cNode.NodeAttributes[ATTRIBLIST].ToString(),
											  cNode.NodeAttributes[DIMENSIONATTRIBUTE].ToString().ToUpper().Equals(Boolean.TrueString.ToUpper()));
					}

					// Evaluate the parameterLists and set to the query definition.
					if ( parameterListsFound )
					{
						queryDef.AddParameterList(cNode.NodeAttributes[ID].ToString(),
												  cNode.NodeAttributes[TYPE].ToString(),
												  cNode.NodeAttributes[CODE].ToString());
					}

					// Evaluate the parameters and set to the query definition.
					if ( parametersFound )
					{
						queryDef.AddParameter(cNode.NodeAttributes[ID].ToString(),
											  cNode.NodeAttributes[COLNAME].ToString(),
											  cNode.NodeAttributes[DATATYPE].ToString(),
											  cNode.NodeAttributes[PARALIST].ToString(),
											  cNode.NodeAttributes[OPERATOR].ToString());
					}
					 // queryFound
				}
				// queriesFound
			} 
		}

		/// <summary>
		/// Processes a ConfigNode of type 'block begin'.
		/// </summary>
		/// <param name="cNode"></param>
		/// <remarks>
		/// Controls the variables, that indicated if a block is already opened or not.
		/// If a block is opened e.g. SYSTEM the corresponding variable - in this case
		/// 'systemFound' - is set to true.
		/// </remarks>
		public void  ProcessConfigBlockBegin(ConfigNode cNode)
		{
			if ( cNode.NodeName.Equals(DATAACCESS) )
			{
				dataAccessFound = true;
			}
			else if ( cNode.NodeName.Equals(SYSTEM) )
			{
				systemFound = true;
			}
			else if ( cNode.NodeName.Equals(APPLICATION) )
			{
				applicationFound = true;
			}
			else if ( cNode.NodeName.Equals(QUERIES) )
			{
				queriesFound = true;
			}
			else if (cNode.NodeName.Equals(QUERY) )
			{
				queryFound = true;

				// Create new object, which is filled in ProcessConfigItem(...).
				queryDef   = new QueryDefinition();

				// Invoke item processing.
				ProcessConfigItem(cNode);
			}
			else if ( cNode.NodeName.Equals(ATTRIBUTELISTS) )
			{
				attributeListsFound = true;
			}
			else if ( cNode.NodeName.Equals(ATTRIBUTES) )
			{
				attributesFound = true;
			}
			else if ( cNode.NodeName.Equals(PARAMETERLISTS) )
			{
				parameterListsFound = true;
			}
			else if ( cNode.NodeName.Equals(PARAMETERS) )
			{
				parametersFound = true;
			}
			else
			{
				// Found something wrong, XML configuration file is incorrect.
				throw new DataAccessException("ERROR_DATAACCESS_INVALID_CONFIGURATION");
			}
		}

		/// <summary>
		/// Processes a ConfigNode of type 'block end'.
		/// </summary>
		/// <param name="cNode"></param>
		/// <remarks>
		/// Controls the variables, that indicated if a block is already opened or not.
		/// If a block is closed e.g. SYSTEM the corresponding variable - in this case
		/// 'systemFound' - is set to false.
		/// </remarks>
		public void  ProcessConfigBlockEnd(ConfigNode cNode)
		{
			if ( cNode.NodeName.Equals(DATAACCESS) )
			{
				dataAccessFound = false;
			}
			else if ( cNode.NodeName.Equals(SYSTEM) )
			{
				systemFound = false;
			}
			else if ( cNode.NodeName.Equals(APPLICATION) )
			{
				applicationFound = false;
			}
			else if ( cNode.NodeName.Equals(QUERIES) )
			{
				queriesFound = false;
			}
			else if (cNode.NodeName.Equals(QUERY) )
			{
				queryFound = false;

				// Ensure that the reference is valid.
				if ( null != queryDef )
				{
					// Set the QueryDefinition object to the configuration.
					DataAccessConfiguration.GetInstance().AddQueryDefinition(queryDef);
				}
			}
			else if ( cNode.NodeName.Equals(ATTRIBUTELISTS) )
			{
				attributeListsFound = false;
			}
			else if ( cNode.NodeName.Equals(ATTRIBUTES) )
			{
				attributesFound = false;
			}
			else if ( cNode.NodeName.Equals(PARAMETERLISTS) )
			{
				parameterListsFound = false;
			}
			else if ( cNode.NodeName.Equals(PARAMETERS) )
			{
				parametersFound = false;
			}
			else
			{
				// Found something wrong, XML configuration file is incorrect.
				throw new DataAccessException("ERROR_DATAACCESS_INVALID_CONFIGURATION");
			}
		}

		#endregion
	}
}

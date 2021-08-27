using System;
using System.Collections;

using de.pta.Component.Common;

using de.pta.Component.Errorhandling;

using de.pta.Component.DbAccess.Enumerations;
using de.pta.Component.DbAccess.Exceptions;

using de.pta.Component.Logging.Log4NetWrapper;

namespace de.pta.Component.DbAccess.Internal.Configuration.XML
{
	/// <summary>
	/// Processes the configuration section in Configuration.xml for the component.
	/// </summary>
	/// <remarks>
	/// <para><b>History</b></para>
	/// <list type="table">
	/// <item>
	/// <term><b>Author:</b></term>
	/// <description>A. Seibt, PTA GmbH</description>
	/// </item>
	/// <item>
	/// <term><b>Date:</b></term>
	/// <description>Aug/29/2003</description>
	/// </item>
	/// <item>
	/// <term><b>Remarks:</b></term>
	/// <description>initial version</description>
	/// </item>
	/// </list>
	/// </remarks>
	public class XmlConfigProcessor : IConfigProcessor
	{
		#region Members

		// XML-Elements
		private const string CONNECTION    = "CONNECTION";
		private const string DBACCESS      = "DBACCESS";
		private const string DATA_ADAPTERS = "DATAADAPTERS";
		private const string DATA_ADAPTER  = "DATAADAPTER";
		private const string PARAMETERS    = "PARAMETERS";
		private const string PARAMETER     = "PARAMETER";
		private const string SQLCOMMANDS   = "SQLCOMMANDS";
		private const string SQLCOMMAND    = "SQLCOMMAND";
		private const string SQL           = "SQL";

		// XML-Attributes
		private const string CONNECTSTRING = "CONNECTSTRING";
		private const string ID            = "ID";
		private const string NAME          = "NAME";
		private const string TYPE          = "TYPE";
		private const string LENGTH        = "LENGTH";
		private const string PRECISION     = "PRECISION";
		private const string SCALE         = "SCALE";
		private const string READONLY      = "READONLY";
		private const string ENCRYPTED		 = "ENCRYPTED";
		private const string SOURCETABLE   = "SOURCETABLE";

		/// <summary>
		/// Enables logging functionality.
		/// </summary>
		private static readonly Logger mLog = new Logger(typeof(XmlConfigProcessor));

		/// <summary>
		/// The object which holds the complete configuration
		/// </summary>
		private DbAccessConfiguration mConfig;

		/// <summary>
		/// Configuration data for a DataAdapter
		/// </summary>
		private DataAdapterConfiguration mAdapterConfig;

		/// <summary>
		/// Configuration data for a Command
		/// </summary>
		private CommandConfiguration     mCommandConfig;

		/// <summary>
		/// List of parameters for a Select SQL of a DataAdapter.
		/// </summary>
		private Hashtable mAdapterParameters;

		/// <summary>
		/// List of parameters for a Command
		/// </summary>
		private Hashtable mCommandParameters;

		/// <summary>
		/// The parameter definition for a DataAdapter and a Command is identical.
		/// This member tracks, if a DataAdapter or a Command is being processed.
		/// </summary>
		private string mActSection;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public XmlConfigProcessor()
		{
			initialize();
		}

		#endregion //End of Constructors

		#region Initialization

		/// <summary>
		/// Initializes the members.
		/// </summary>
		private void initialize()
		{
			mConfig            = null;
			mAdapterConfig     = null;
			mCommandConfig     = null;
			mAdapterParameters = new Hashtable();
			mCommandParameters = new Hashtable();
			mActSection        = "";
		}	

		#endregion //End of Initialization

		#region Accessors 

		#endregion //End of Accessors

		#region IConfigProcessor Members

		/// <summary>
		/// Process an XML-Element.
		/// </summary>
		/// <param name="pNode"></param>
		public void ProcessConfigBlockBegin(ConfigNode pNode)
		{
			// Configuration section for component found
			if(pNode.NodeName.Equals(DBACCESS))
			{
				mLog.Debug("Configuration block for DbAccess found");
				mConfig = DbAccessConfiguration.GetInstance();
			}
			// Configurationj section for data adapters found
			else if(pNode.NodeName.Equals(DATA_ADAPTERS))
			{
				mLog.Debug("Processing data adapters");
				mActSection = DATA_ADAPTERS;
			}
			// Configuration section for commands found
			else if(pNode.NodeName.Equals(SQLCOMMANDS))
			{
				mLog.Debug("Processing SQL commands");
				mActSection = SQLCOMMANDS;
			}
			// Initialize processing for a data adapter
			else if(pNode.NodeName.Equals(DATA_ADAPTER))
			{
				mAdapterConfig    = new DataAdapterConfiguration();
				mAdapterConfig.Id = (string)pNode.NodeAttributes[ID];
				if(pNode.NodeAttributes.ContainsKey(READONLY))
				{
					string flag = Convert.ToString(pNode.NodeAttributes[READONLY]);
					mAdapterConfig.ReadOnly = (flag.ToLower().Equals("true") ? true : false);
				}
				mLog.Debug("Processing DataAdapter " + mAdapterConfig.Id);
			}
			// Initialize processing for a command
			else if(pNode.NodeName.Equals(SQLCOMMAND))
			{
				mCommandConfig    = new CommandConfiguration();
				mCommandConfig.Id = (string)pNode.NodeAttributes[ID];
				mLog.Debug("Processing Command " + mCommandConfig.Id);
			}
			// Initialize processing for parameters
			else if(pNode.NodeName.Equals(PARAMETERS))
			{
				if(mActSection.Equals(DATA_ADAPTERS))
				{
					mLog.Debug("Processing Parameters for DataAdapter <" + mAdapterConfig.Id + ">");
					mAdapterParameters = new Hashtable();
				}
				else
				{
					mLog.Debug("Processing Parameters for Command <" + mCommandConfig.Id + ">");
					mCommandParameters = new Hashtable();
				}
			}
		}

		/// <summary>
		/// Process a XML-Element
		/// </summary>
		/// <param name="pNode"></param>
		public void ProcessConfigItem(ConfigNode pNode)
		{
			mLog.Debug("Processing ConfigItem: node " + pNode.NodeName);
			WriteNodeAttributes(pNode.NodeAttributes);

			// Process the connection element
			if(pNode.NodeName.Equals(CONNECTION))
			{
				string providerType = (string)pNode.NodeAttributes[TYPE];
				mConfig.ProviderType = ConvertProviderType(providerType);
				mConfig.ConnectString = (string)pNode.NodeAttributes[CONNECTSTRING];
				if(pNode.NodeAttributes.ContainsKey(ENCRYPTED))
				{
					string flag = Convert.ToString(pNode.NodeAttributes[ENCRYPTED]);
					mConfig.ConnectStringEncrypted = (flag.ToLower().Equals("true") ? true : false);
				}
			}
			// Process SQL statements
			else if(pNode.NodeName.Equals(SQL))
			{
				if(mActSection.Equals(DATA_ADAPTERS))
				{
					mAdapterConfig.Sql = (string)pNode.NodeValue;
				}
				else
				{
					mCommandConfig.Sql = (string)pNode.NodeValue;
				}
			}
			// Process SourceTable definition
			else if(pNode.NodeName.Equals(SOURCETABLE))
			{
				if(mActSection.Equals(DATA_ADAPTERS))
				{
					mAdapterConfig.SourceTable = (string)pNode.NodeValue;
				}
				else
				{
					mLog.Warn("Ignore invalid element <SourceTable> in Command <" + mCommandConfig.Id + ">");
				}
			}
				// Process parameters for a data adapter or command
			else if(pNode.NodeName.Equals(PARAMETER))
			{
				DbParameter param = new DbParameter();
				param.Name = Convert.ToString(pNode.NodeAttributes[NAME]);
				param.DbType = Convert.ToString(pNode.NodeAttributes[TYPE]);
				if(pNode.NodeAttributes.ContainsKey(LENGTH))
				{
					param.Length = Convert.ToInt32(pNode.NodeAttributes[LENGTH]);
				}
				if(pNode.NodeAttributes.ContainsKey(PRECISION))
				{
					param.Precision = Convert.ToByte(pNode.NodeAttributes[PRECISION]);
				}
				if(pNode.NodeAttributes.ContainsKey(SCALE))
				{
					param.Scale = Convert.ToByte(pNode.NodeAttributes[SCALE]);
				}
				if(mActSection.Equals(DATA_ADAPTERS))
				{
					mAdapterParameters.Add(param.Name, param);
				}
				else
				{
					mCommandParameters.Add(param.Name, param);
				}
			}
		}

		/// <summary>
		/// Process a XML element
		/// </summary>
		/// <param name="pNode"></param>
		public void ProcessConfigBlockEnd(ConfigNode pNode)
		{
			// Add parameters to the current data adapter or command configuration
			if(pNode.NodeName.Equals(PARAMETERS))
			{
				if(mActSection.Equals(DATA_ADAPTERS))
				{
					IDictionaryEnumerator enumerator = mAdapterParameters.GetEnumerator();
					while(enumerator.MoveNext())
					{
						mAdapterConfig.AddParameter((DbParameter)enumerator.Value);
					}
				}
				else
				{
					IDictionaryEnumerator enumerator = mCommandParameters.GetEnumerator();
					while(enumerator.MoveNext())
					{
						mCommandConfig.AddParameter((DbParameter)enumerator.Value);
					}
				}
			}
			// Add data adapter configuration
			else if(pNode.NodeName.Equals(DATA_ADAPTER))
			{
				mConfig.AddAdapterData(mAdapterConfig.Id, mAdapterConfig);
			}
			// Add command configuration
			else if(pNode.NodeName.Equals(SQLCOMMAND))
			{
				mConfig.AddCommandData(mCommandConfig.Id, mCommandConfig);
			}
		}

		#endregion

		#region Methods 

		/// <summary>
		/// Convert the provider type declared in the XML to the appropriate
		/// enumeration member.
		/// </summary>
		/// <param name="pProviderType">The provider type as string.</param>
		/// <returns></returns>
		private DbAccessProviderType ConvertProviderType(string pProviderType)
		{
			if(pProviderType.ToLower().Equals("oledb"))
			{
				return DbAccessProviderType.OleDb;
			}
			else if(pProviderType.ToLower().Equals("oracle"))
			{
				return DbAccessProviderType.Oracle;
			}
			else if(pProviderType.ToLower().Equals("oracleodp"))
			{
				return DbAccessProviderType.OracleOdp;
			}
			else if(pProviderType.ToLower().Equals("sqlclient"))
			{
				return DbAccessProviderType.SqlClient;
			}
			else
			{
				string error = "Invalid provider type <" + pProviderType + ">";
				mLog.Error(error);
				throw new DbAccessConfigurationException(error);
			}
		}

		/// <summary>
		/// Helper method for debugging.
		/// </summary>
		/// <param name="pAttributes">The attributes of a xml element</param>
		/// <remarks>
		/// Print all attributes of the current node.
		/// </remarks>
		private void WriteNodeAttributes(Hashtable pAttributes) 
		{
			IDictionaryEnumerator myEnumerator = pAttributes.GetEnumerator();
			while ( myEnumerator.MoveNext() )
			{
				mLog.Debug("--> " + myEnumerator.Key + ": " + myEnumerator.Value);
			}
		}

		#endregion // End of Methods

	}
}

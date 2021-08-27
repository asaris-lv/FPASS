using System;
using System.Collections;
using System.Xml;

namespace de.pta.Component.Common
{
	/// <summary>
	/// Singleton class for configuration reading.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> U.Fretz, PTA GmbH
	/// <b>Date:</b> Apr/02/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	public class ConfigReader
	{

		#region Members

		private static ConfigReader instance;
		private String applicationRootPath;
		private String relativeApplicationPath;
		public const string APPLICATION_PATH = "c:\\inetpub\\wwwroot\\PrismaDwhWeb\\"; // Used only in emergency cases.
		private const string XML_MAIN_DOC = "\\Configuration\\Configuration.xml"; 
		
		#endregion //End of Members

		#region Constructors
	
		private ConfigReader()
		{
			// Private constructor assures that no further instance is created.
		}

		#endregion //End of Constructors
	
		#region Initialization
	
		/// <summary>
		/// There will allways be only one instance.
		/// </summary>
		/// <returns> the ConfigurationReader instance </returns>
		public static ConfigReader GetInstance() 
		{
			if (null == instance) 
			{
				instance = new ConfigReader();
			}
			return instance;
		}
	
		#endregion //End of Initialization

		#region Accessors 

		/// <summary>
		/// Application Path without Application name
		/// additionally the configpath is set (adding PrismaDwhWeb)
		/// </summary>
		public string ApplicationRootPath
		{
			set
			{
				applicationRootPath = value;
			}
			get
			{
				return applicationRootPath;
			}
		}
		/// <summary>
		/// Relative application path set by Global.asax.
		/// </summary>
		public string RelativeApplicationPath
		{
			set
			{
				relativeApplicationPath = value;
			}
			get
			{
				return relativeApplicationPath;
			}
		}

		#endregion //End of Accessors
	
		#region Methods

		/// <summary>
		/// Returns the complete path of the application.
		/// </summary>
		public String GetConfigPath()
		{
			return applicationRootPath + relativeApplicationPath;
		}

		/// <summary>
		/// Reads everything beneath the parent node from an XML file.
		/// </summary>
		/// <remarks>
		/// This functionaliy uses the class XmlNodeReader to read through an XML file.
		/// If it comes to an entry of type 'element', 'text' or 'end element' the data is set 
		/// into an instance of XmlNode. Data to be set are: tag name as node name, 
		/// tag value as node value, all attributes of the tag as node attributes,
		/// the depth within the node hierarchy as node depth and the parent node as node parent.
		/// Additionaly every node is specified by the node type. Possible types are:
		/// 'begin block' which opens a new block, e.g. a tag named documents which contains several
		/// 'document'-tags each describing a document. Another type is 'end block'. Nodes of this type 
		/// represent the end of a block, which was started  with a node of type 'begin block'.
		/// The last type is just 'item'. This type is used for a flat tag, which does not include
		/// other tags.
		/// If all data is set to the current node instance it is send to the method 'processNode'.
		/// </remarks>
		/// <parameters> Component specific parent node and component specific processor</parameters>
		public void ReadConfig(string parentNode, IConfigProcessor processor) 
		{

			XmlDocument xmlDoc = new XmlDocument();
			XmlNode xmlParentNode;
			XmlNodeReader xmlReader;

			String configPath =	this.GetConfigPath();
			try
			{
				if (null == configPath)
				{
					configPath = APPLICATION_PATH;
				}
				xmlDoc.Load(configPath + XML_MAIN_DOC); 
				xmlParentNode = xmlDoc.SelectSingleNode(parentNode);
				xmlReader = new XmlNodeReader(xmlParentNode);
			}
			catch(Exception e)
			{
				throw new CommonXmlException("ERROR_COMMON_CONFIG_FILE", e);
			}

			ConfigNode currNode = null;
			bool currNodeProcessed = false;
			ArrayList nodeHierarchy = new ArrayList();

			while (xmlReader.Read()) 
			{
				// new element: process last element, read data for new element
				if (xmlReader.NodeType == XmlNodeType.Element)
				{

					// care for the 'old' currNode, look for his type 
					// and start the component specific processor
					if (currNode != null && ! currNodeProcessed)
					{
						if (currNode.NodeType == ConfigNode.TYPE_NOT_DEFINED)
						{
							currNode.NodeType = ConfigNode.TYPE_BLOCK_BEGIN;
						}
						
						processNode(currNode, processor);
						currNodeProcessed = true;
					}


					// the 'old' current node is done
					// get the next one, set Name and Depth
					currNode = new ConfigNode(xmlReader.Name.ToUpper());
					currNode.NodeDepth = xmlReader.Depth;

					// set parent node: get the one out of nodeHierarchy at currNode.depth -1
					// set node in nodeHierarchy
					if (currNode.NodeDepth > 0)
					{
						currNode.NodeParent = (ConfigNode) nodeHierarchy[currNode.NodeDepth-1];
					}
					nodeHierarchy.Insert(currNode.NodeDepth, currNode);
					
					// the new node is not already processed, of course
					currNodeProcessed = false;

					// an empty node is always from type: TYPE_ITEM
					// because it can not open or close a block
					if (xmlReader.IsEmptyElement)
					{
						currNode.NodeType = ConfigNode.TYPE_ITEM;
					}

					// read all attributes
					while(xmlReader.MoveToNextAttribute())
					{
						currNode.AddAttribute(xmlReader.Name.ToUpper(), xmlReader.Value);
					}
				}

				// text: read value and set type
				if (xmlReader.NodeType == XmlNodeType.Text)
				{
					currNode.NodeValue = xmlReader.Value;
					currNode.NodeType = ConfigNode.TYPE_ITEM;
				}

				// end element: set type
				// if reader is on ende of block: 
				//    process currentNode and process block end
				if (xmlReader.NodeType == XmlNodeType.EndElement)
				{
					// reader is at lower level than currNode
					// process node and process the block end
					if (xmlReader.Depth < currNode.NodeDepth)
					{
						if (currNode != null && ! currNodeProcessed)
						{
							processNode(currNode, processor);
							currNodeProcessed = true;
						}
						
						ConfigNode blockEndNode = new ConfigNode(xmlReader.Name.ToUpper());
						blockEndNode.NodeType = ConfigNode.TYPE_BLOCK_END;
						blockEndNode.NodeDepth = xmlReader.Depth;


						// set parent node: get the one out of nodeHierarchy at currNode.depth -1
						// set node in nodeHierarchy
						if (blockEndNode.NodeDepth > 0)
						{
							blockEndNode.NodeParent = (ConfigNode) nodeHierarchy[blockEndNode.NodeDepth-1];
						}
						nodeHierarchy.Insert(blockEndNode.NodeDepth, blockEndNode);

						processNode(blockEndNode, processor);
					}
					
					// currNode and reader on same hierarchy level
					// it is just the end of my node
					else 
					{
						currNode.NodeType = ConfigNode.TYPE_ITEM;
					}
				}
			}
			xmlReader.Close();


			// save the last node!
			if (currNode != null && ! currNodeProcessed)
			{
				processNode(currNode, processor);
				currNodeProcessed = true;
			}
		}

		private void processNode(ConfigNode currNode, IConfigProcessor processor) 
		{
			// Calls the correct method of component specific processor.
			// Method depends on node type.

			switch(currNode.NodeType)       
			{         
				case ConfigNode.TYPE_ITEM: 
					processor.ProcessConfigItem(currNode);
					break;
				case ConfigNode.TYPE_BLOCK_BEGIN:            
					processor.ProcessConfigBlockBegin(currNode);
					break;
				case ConfigNode.TYPE_BLOCK_END:            
					processor.ProcessConfigBlockEnd(currNode);
					break;
			}
		}

		#endregion // Methods
	}
}


		


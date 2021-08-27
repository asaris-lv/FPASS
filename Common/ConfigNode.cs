using System;
using System.Collections;

namespace de.pta.Component.Common
{
	/// <summary>
	/// Class for representation of an XML node.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> U.Fretz, PTA GmbH
	/// <b>Date:</b> Apr/02/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	public class ConfigNode
	{
		#region Members

		public const int TYPE_NOT_DEFINED = 0;
		public const int TYPE_ITEM = 1;
		public const int TYPE_BLOCK_BEGIN = 2;
		public const int TYPE_BLOCK_END = 3;
		
		private string nodeName;
		private string nodeValue;
		private int nodeType;              // see const values defined above
		private int nodeDepth;             // level in hierarchy
		private Hashtable nodeAttributes;  // key (=name) and value (=value) of attributes
		private ConfigNode nodeParent;     // node in hierarchy one level above

		#endregion //End of Members

		#region Constructors

		public ConfigNode()
		{
			initialize(String.Empty, String.Empty, TYPE_NOT_DEFINED, -1, new Hashtable(), null);
		}
		public ConfigNode(string newNodeName)
		{
			initialize(newNodeName, String.Empty, TYPE_NOT_DEFINED, -1, new Hashtable(), null);
		}
		public ConfigNode(string newNodeName, int nodeDepth)
		{
			initialize(newNodeName, String.Empty, -1, nodeDepth, new Hashtable(), null);
		}

		public ConfigNode(ConfigNode cNode)
		{
			initialize(cNode.NodeName, cNode.NodeValue, cNode.NodeType, 
				       cNode.NodeDepth, cNode.NodeAttributes, cNode.NodeParent);
		}

		#endregion //End of Constructors

		#region Initialization

		public void initialize(string newNodeName, string newNodeValue, int newNodeType, 
			                   int newNodeDepth, Hashtable newNodeAttributes, ConfigNode newNodeParent)
		{
			nodeName = newNodeName;
			nodeValue = newNodeValue;
			nodeType = newNodeType;
			nodeDepth = newNodeDepth;
			nodeAttributes = newNodeAttributes;
			nodeParent = newNodeParent;
		}

		#endregion //End of Initialization
	
		#region Accessors 

		/// <summary>
		/// The member NodeName contains the name of the node. 
		/// In the XML file this is the name of the tag.
		/// </summary>
		public string NodeName 
		{
			get 
			{
				return nodeName;
			}
			set 
			{
				nodeName = value;
			}
		}

		/// <summary>
		/// The member NodeValue contains the value of the node.
		/// In the XML file this is the value of the tag, 
		/// which stands between the opening and the closing tag.
		/// </summary>
		public string NodeValue 
		{
			get 
			{
				return nodeValue;
			}
			set 
			{
				nodeValue = value;
			}
		}

		/// <summary>
		/// The member NodeType contains the type of the node.
		/// Types are: TYPE_NOT_DEFINED, TYPE_ITEM, TYPE_BLOCK_BEGIN, TYPE_BLOCK_END.
		/// </summary>
		public int NodeType 
		{
			get 
			{
				return nodeType;
			}
			set 
			{
				nodeType = value;
			}
		}

		/// <summary>
		/// The member NodeDepth contains the depth of the node.
		/// In the XML file this is the depth of the node within the node heirarchy.
		/// </summary>
		public int NodeDepth 
		{
			get 
			{
				return nodeDepth;
			}
			set 
			{
				nodeDepth = value;
			}
		}

		/// <summary>
		/// The member NodeAttributes contains all attributes of the node.
		/// In the XML file the attributes are defined within the tag.
		/// </summary>
		public Hashtable NodeAttributes 
		{
			get 
			{
				return nodeAttributes;
			}
			set 
			{
				nodeAttributes = value;
			}
		}
		
		/// <summary>
		/// The member NodeParent contains the parent node of the node.
		/// In the XML file this is the parent node within the node hierarchy.
		/// </summary>
		public ConfigNode NodeParent 
		{
			get 
			{
				return nodeParent;
			}
			set 
			{
				nodeParent = value;
			}
		}

		#endregion //End of Accessors	
	
		#region Methods
		
		/// <summary>
		/// Convenient functionality to add an attribute in the hashtable NodeAttributes.
		/// </summary>
		public void AddAttribute(string attributeName, string attributeValue)
		{
			nodeAttributes.Add(attributeName, attributeValue);
		}

		#endregion //Methods
	}
}

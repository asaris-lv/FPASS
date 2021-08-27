using System;
using System.Collections;
using de.pta.Component.Common;

namespace de.pta.Component.ListOfValues.Internal
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
	internal class LovConfigProcessor : IConfigProcessor
	{
		#region Members

		private const String LISTSOFVALUE		= "LISTSOFVALUE";
		private const String LISTOFVALUE		= "LISTOFVALUE";
		private const String ID					= "ID";
		private const String SQL				= "SQL";
		private const String ATTRIBUTES			= "ATTRIBUTES";
		private const String ATTRIBUTE			= "ATTRIBUTE";
		private const String TYPE				= "TYPE";
		private const String PK					= "PK";
		private const String REFERENCE			= "REFERENCE";
		private const String READALWAYSNEW		= "READALWAYSNEW";
        private const String PARENT_LIST		= "PARENT_LIST";
		private const String REF_ATTRIBUTE		= "REF_ATTRIBUTE";
		private const String LOCAL_ATTRIBUTE	= "LOCAL_ATTRIBUTE";
		private const String DB_CONNECT			= "DB_CONNECT";
		private const String ENCRYPTED			= "ENCRYPTED";
		private const String CONNECTSTRING		= "CONNECTSTRING";
		private const String ASSEMBLY			= "ASSEMBLY";
		private const String CONNECTION_CLASS	= "CONNECTION_CLASS";
		private const String DATAREADER_CLASS	= "DATAREADER_CLASS";
		private const String COMMAND_CLASS		= "COMMAND_CLASS";


		private bool listsOfValueFound;
		private bool listOfValueFound;
		private bool attributesFound;
		private ListOfValues listOfValues;
		private bool encrypted;
		private DbInformation dbInfo;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Constructs the object.
		/// </summary>
		public LovConfigProcessor()
		{
			initialize();
		}

		#endregion //End of Constructors

		#region Initialization

		private void initialize()
		{
			// Initializes the members.
			listsOfValueFound	= false;
			listOfValueFound	= false;
			attributesFound		= false;
			listOfValues        = null;
			encrypted			= false;
			dbInfo				= new DbInformation();
		}

		#endregion //End of Initialization

		#region Accessors 

		/// <summary>
		/// Gets the database information object.
		/// </summary>
		public DbInformation DbInfo
		{
			get
			{
				return dbInfo;
			}
		}

		#endregion //End of Accessors

		#region Methods

		/// <summary>
		/// Processes a ConfigNode of type 'item'.
		/// </summary>
		/// <param name="cNode"></param>
		public void  ProcessConfigItem(ConfigNode cNode)
		{
			// must be within the ListsOfValue block
			if ( !listsOfValueFound )
			{
				throw new ListOfValueException("ERROR_LOV_INVALID_CONFIGURATION");
			}

			// get the database connect informations.
			if ( cNode.NodeName.Equals(DB_CONNECT) )
			{
				// Verify if encryption is used.
				if ( cNode.NodeAttributes.ContainsKey(ENCRYPTED) )
				{
					encrypted = cNode.NodeAttributes[ENCRYPTED].ToString().ToUpper().
													Equals(Boolean.TrueString.ToUpper());
				}

				// If encryption is used, use the CryptUtil to decrypt.
				// Otherwise use the string direct from the configuration.
				if ( encrypted )
				{
					dbInfo.ConnectString = CryptoUtil.Decrypt(cNode.NodeAttributes[CONNECTSTRING].ToString());
				}
				else
				{
					dbInfo.ConnectString = cNode.NodeAttributes[CONNECTSTRING].ToString();
				}
				dbInfo.AssemblyName		= cNode.NodeAttributes[ASSEMBLY].ToString();
				dbInfo.ConnectionClass	= cNode.NodeAttributes[CONNECTION_CLASS].ToString();
				dbInfo.DataReaderClass	= cNode.NodeAttributes[DATAREADER_CLASS].ToString();
				dbInfo.CommandClass		= cNode.NodeAttributes[COMMAND_CLASS].ToString();
			}

			if ( listOfValueFound )
			{
				// ID of a list of values
				if ( cNode.NodeName.Equals(ID) )
				{
					listOfValues.Id = cNode.NodeValue;
				}

				if ( cNode.NodeName.Equals(SQL) )
				{
					// remember the SQL-statement.
					listOfValues.SqlStatement = cNode.NodeValue;
				}

				if ( attributesFound )
				{
					if ( cNode.NodeName.Equals(ATTRIBUTE) )
					{
						// remember the attributes, for further object construction.
						Attribute attrib = new Attribute();

						attrib.Id   = cNode.NodeAttributes[ID].ToString();
						attrib.Type = cNode.NodeAttributes[TYPE].ToString();

						if ( cNode.NodeAttributes.ContainsKey(PK) )
						{
							attrib.IsPrimeryKey = cNode.NodeAttributes[PK].ToString().ToUpper().
															Equals(Boolean.TrueString.ToUpper());
						}

						// add the attribute
						listOfValues.AddConfigAttribute(attrib);
					}
				}

				if ( cNode.NodeName.Equals(REFERENCE) )
				{
					// The "ReadAlwaysNew" flag can only be set in a root list.
					// If the flag is set in a sub list, the config processor will
					// throw an exception. So bad configurations are avoided.

					// Retrieve the flag if it exists.
					if ( cNode.NodeAttributes.ContainsKey(READALWAYSNEW) )
					{
						listOfValues.ReadAlwaysNew = cNode.NodeAttributes[READALWAYSNEW].ToString().
														ToUpper().Equals(Boolean.TrueString.ToUpper());
					}

					// remember the reference to the parent list.
					if ( cNode.NodeAttributes.ContainsKey(PARENT_LIST) )
					{
						if ( listOfValues.ReadAlwaysNew )
						{
							throw new ListOfValueException("ERR_CHILD_LIST_SET_TO_READALWAYSNEW");
						}
						else
						{
							listOfValues.ParentList = cNode.NodeAttributes[PARENT_LIST].ToString();
						}
					}

					// remember the reference attibute.
					if ( cNode.NodeAttributes.ContainsKey(REF_ATTRIBUTE) )
					{
						if ( listOfValues.ReadAlwaysNew )
						{
							throw new ListOfValueException("ERR_CHILD_LIST_SET_TO_READALWAYSNEW");
						}
						else
						{
							listOfValues.RefAttribute = cNode.NodeAttributes[REF_ATTRIBUTE].ToString();
						}
					}

					// remember the local attribute.
					if ( cNode.NodeAttributes.ContainsKey(LOCAL_ATTRIBUTE) )
					{
						if ( listOfValues.ReadAlwaysNew )
						{
							throw new ListOfValueException("ERR_CHILD_LIST_SET_TO_READALWAYSNEW");
						}
						else
						{
							listOfValues.LocalAttribute = cNode.NodeAttributes[LOCAL_ATTRIBUTE].ToString();
						}
					}
				// Found a reference.
				}
			}
		}

		/// <summary>
		/// Processes a ConfigNode of type 'block begin'.
		/// </summary>
		/// <param name="cNode"></param>
		public void  ProcessConfigBlockBegin(ConfigNode cNode)
		{
			if ( cNode.NodeName.Equals(LISTSOFVALUE) )
			{
				// ListsOfValue opened
				listsOfValueFound = true;
			}
			else if ( cNode.NodeName.Equals(LISTOFVALUE) )
			{
				// ListOfValue opened
				listOfValueFound = true;
				listOfValues	 = new ListOfValues();
			}
			else if ( cNode.NodeName.Equals(ATTRIBUTES) )
			{
				// Attributes opened
				attributesFound = true;
			}
			else
			{
				// Found something wrong!
			}
		}

		/// <summary>
		/// Processes a ConfigNode of type 'block end'.
		/// </summary>
		/// <param name="cNode"></param>
		public void  ProcessConfigBlockEnd(ConfigNode cNode)
		{
			if ( cNode.NodeName.Equals(LISTSOFVALUE) )
			{
				// ListsOfValues closed
				listsOfValueFound = false;
			}
			else if ( cNode.NodeName.Equals(LISTOFVALUE) )
			{
				// ListOfValue closed
				listOfValueFound = false;

				// Add the list.
				ListStructure.GetInstance().AddListOfValues(listOfValues);
			}
			else if ( cNode.NodeName.Equals(ATTRIBUTES) )
			{
				// Attributes closed
				attributesFound = false;
			}
			else
			{
				// Found something wrong!
			}
		}

		#endregion // Methods
	}
}
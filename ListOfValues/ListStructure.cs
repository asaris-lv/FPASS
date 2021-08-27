using System;
using System.Collections;
using de.pta.Component.Common;

namespace de.pta.Component.ListOfValues.Internal
{
	/// <summary>
	/// Encapsulates the lists of values
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> M.Keller, PTA GmbH
	/// <b>Date:</b> Apr/02/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class ListStructure
	{
		#region Members

		private const String XML_NODE_LISTSOFVALUES = "application/configuration/ListsOfValue";
		private static ListStructure instance = null;
		private Hashtable listsOfValues;
		private DbInformation dbInfo;
		private String restriction;

		#endregion //End of Members

		#region Constructors

		public ListStructure()
		{
			// Construction of the object.
			initialize();
		}

		#endregion //End of Constructors

		#region Initialization

		/// <summary>
		/// Initializes the members.
		/// </summary>
		private void initialize()
		{
			listsOfValues	= new Hashtable();
			dbInfo			= null;
			restriction		= String.Empty;
		}

		/// <summary>
		/// Returns the one and only instance of the class.
		/// </summary>
		/// <returns>
		/// The instance of the ListStructure.
		/// </returns>
		public static ListStructure GetInstance()
		{
			if ( null == instance )
			{
				instance = new ListStructure();
			}

			return instance;
		}

		#endregion //End of Initialization

		#region Accessors

		/// <summary>
		/// Gets and sets the value for the restriction information.
		/// If null is set, the value is transformed to String.Empty.
		/// </summary>
		public String Restriction
		{
			get
			{
				return restriction;
			}
			set
			{
				// If null is set, transform to String.Empty.
				if ( null == value )
				{
					restriction = String.Empty;
				}
				else
				{
					restriction = value;
				}
			}
		}

		#endregion //End of Accessors

		#region Methods

		/// <summary>
		/// Reads the configuration.
		/// </summary>
		public void ReadConfiguration()
		{
			// Clear Lists of values first. In case or re-reading
			// stuctures must be cleared.
			clearListOfValues();

			// Read the configuration
			LovConfigProcessor processor = new LovConfigProcessor();
			ConfigReader.GetInstance().ReadConfig(XML_NODE_LISTSOFVALUES, processor);

			// Get the processors DbInformation object.
			dbInfo = processor.DbInfo;

			// Read the lists.
			readAllLists();

			// Build the references.
			buildReferences();
		}

		/// <summary>
		/// Clears all lists of values in the list structure.
		/// </summary>
		private void clearListOfValues()
		{
			ListOfValues lov	= null;

			// Loop all lov items and clear and call its clear method.
			IDictionaryEnumerator lovEnumerator = listsOfValues.GetEnumerator();
			while ( lovEnumerator.MoveNext() )
			{
				// Get the lov object reference.
				lov = (ListOfValues)lovEnumerator.Value;
				lov.Clear();
			}

			// Clear the hashtable with the lists of values.
			listsOfValues.Clear();
		}

		/// <summary>
		/// Gets a list of values.
		/// </summary>
		/// <param name="listId">Id of the list.</param>
		/// <returns>
		/// A ListOfValues object reference, if the requested list is found, 
		/// otherwise null.
		/// </returns>
		public ListOfValues GetList(String listId)
		{
			ListOfValues lov	 = null;
			ListOfValues rootLov = null;
			String rootId		 = String.Empty;

			// Ensure the requested list exists.
			if ( listsOfValues.ContainsKey(listId) )
			{
				// Get the LOV.
				lov = (ListOfValues)listsOfValues[listId];

				// Get the root of the LOV, If the root is marked as
				// re-read, read the entire structure again and build
				// the references, too.
				rootId  = getRootParent(listId);
				rootLov = (ListOfValues)listsOfValues[rootId];
				
				if ( rootLov.ReadAlwaysNew )
				{
					// Re-Read und re-Build all relevant LOVs.
					readLovTree(rootId);
					buildReferencesForLovTree(rootId);

					// Retrieve the list again.
					lov = (ListOfValues)listsOfValues[listId];
				}
			}

			return lov;
		}

		/// <summary>
		/// Adds a list of values.
		/// </summary>
		/// <param name="lov"></param>
		public void AddListOfValues(ListOfValues lov)
		{
			listsOfValues.Add(lov.Id, lov);
		}

		/// <summary>
		/// Returns a string that contains the id of the child list of values
		/// to the given id of the parent list. If no childs are found an empty
		/// string is returned.
		/// </summary>
		/// <param name="parentLovId">Id of the parent list.</param>
		/// <returns>
		/// A string with an lov id or an empty string.
		/// </returns>
		public String GetChildListOfValues(String parentLovId)
		{
			ListOfValues lov = null;

			// Iterate all lists of values
			IDictionaryEnumerator lovEnumerator = listsOfValues.GetEnumerator();
			while ( lovEnumerator.MoveNext() )
			{
				// Get the lov object reference.
				lov = (ListOfValues)lovEnumerator.Value;

				// If the given parentLovId is an parent of the actual
				// lov, the actual lov is a child of the given parameter.
				if ( lov.ParentList.ToString().Equals(parentLovId) )
				{
					// return the id of the actual lov.
					return lov.Id;
				}
			}

			// no childs found.
			return String.Empty;
		}

		/// <summary>
		/// Gets the number ob childs to the provided id of a list of values.
		/// </summary>
		/// <param name="parentLovId">The id of the parent List of values.</param>
		/// <returns>
		/// The numer of childs. If no childs exist 0 is returned.
		/// </returns>
		public int GetNumberOfChilds(String parentLovId)
		{
			int		childs	= 0;
			String	lovId	= GetChildListOfValues(parentLovId);
			
			// Search until no more childs exists and String.Empty 
			// is returned by GetChildListOfValues(...).
			while ( !lovId.Equals(String.Empty) )
			{
				++childs;
				lovId = GetChildListOfValues(lovId);
			}
				
			// Return the number.
			return childs;
		}

		/// <summary>
		/// Evaluates, if the given column is a column of the given list of values.
		/// </summary>
		/// <param name="column">Name of the column to verify.</param>
		/// <param name="listId">Name of the list of values.</param>
		/// <returns>
		/// True if the list of values exists and the column is part of these list,
		/// otherwise false is returned.
		/// </returns>
		public bool IsColumnOfList(String column, String listId)
		{
			bool isColumnOfList = false;
			ListOfValues lov	= null;
			
			// try to get the lov.
			lov = (ListOfValues)listsOfValues[listId];

			// Ensure list is valid, otherwise the list does not exist.
			if ( null != lov )
			{
				isColumnOfList = lov.IsColumnOfList(column);
			}

			return isColumnOfList;
		}

		/// <summary>
		/// Reads the lists of values from the database.
		/// </summary>
		private void readAllLists()
		{
			// Create a lov reader and open it.
			LovDataReader reader = new LovDataReader(dbInfo);
			reader.Open();

			// Loop all LOVs and read them from the database.
			IDictionaryEnumerator lovEnumerator = listsOfValues.GetEnumerator();
			while ( lovEnumerator.MoveNext() )
			{
				ListOfValues lov = (ListOfValues)lovEnumerator.Value;
				lov.ClearItems();

				reader.ExecuteSql(lov.SqlStatement, restriction);
				reader.FillListOfValues(lov);
			}

			// Close the reader
			reader.Close();
		}

		private void readLovTree(String lovId)
		{
			// Reads all LOVs in a dependency. First the root id from
			// the LOV is searched and all lov under this root are read
			// again.

			// Get the root.
			String lovToRead = getRootParent(lovId);

			// Create a reader and open it.
			LovDataReader reader = new LovDataReader(dbInfo);
			reader.Open();

			// As long as there are LOVs to read.
			while ( !lovToRead.Equals(String.Empty) )
			{
				// Get the LOV and reset their items.
				ListOfValues lov = (ListOfValues)listsOfValues[lovToRead];
				lov.ClearItems();

				// Re-read the LOV from the database.
				reader.ExecuteSql(lov.SqlStatement, restriction);
				reader.FillListOfValues(lov);
				
				// Get the next child.
				lovToRead = GetChildListOfValues(lovToRead);
			}

			// Cleanup the reader.
			reader.Close();
		}

		private String getRootParent(String lovId)
		{
			// Retreives the root id of the given list of values.
			// Calls itself recursiv, until the root LOV is found.

			// Store the current id
			String parentId = lovId;

			// Ensure the LOV exists.
			if ( listsOfValues.ContainsKey(lovId) )
			{
				// Get the LOV
				ListOfValues lov = (ListOfValues)listsOfValues[lovId];

				// If a parent exists get the root id of the parent.
				if ( lov.HasParent() )
				{
					parentId = getRootParent(lov.ParentList);
				}
			}

			// Return parents' id.
			return parentId;
		}

		private void buildReferencesForLovTree(String lovId)
		{
			// Builds the references in a dependency. First the root LOV
			// is retrieved and then for all childs the references are set.

			// Get root and proccess childs.
			String root  = getRootParent(lovId);
			String child = GetChildListOfValues(root);

			// As long as there are childs process them.
			while ( !child.Equals(String.Empty) )
			{
				// get the LOV
				ListOfValues lov = (ListOfValues)listsOfValues[child];

				// Process the LOVs items.
				proccessItems(lov);

				// Get the next child.
				child = GetChildListOfValues(child);
			}
		}

		/// <summary>
		/// Builds the references between the lists and items.
		/// </summary>
		private void buildReferences()
		{
			// Iterate all lists of values
			IDictionaryEnumerator lovEnumerator = listsOfValues.GetEnumerator();
			while ( lovEnumerator.MoveNext() )
			{
				ListOfValues lov = (ListOfValues)lovEnumerator.Value;

				// process only lists that have parents.
				if ( lov.HasParent() )
				{
					// Process the LOV's items.
					proccessItems(lov);
				}
			}
		}

		private void proccessItems(ListOfValues lov)
		{
			// iterate all items ob a list.
			foreach ( ListOfValuesItem item in lov.Items )
			{
				// get the column value of the local reference column
				String colValue = item.GetValueForAttribute(lov.LocalAttribute);

				// set the reference in the parent list.
				setItemReference(lov.ParentList, lov.Id, lov.RefAttribute, colValue, item);
			}
		}

		private void setItemReference(String parentLov, 
									  String childLov, 
									  String column, 
									  String colValue, 
									  ListOfValuesItem childItem)
		{
			// Sets an reference in the given parent list, if found.
			// Iterates all items of the requested parent list. For every item the given column
			// value (colvalue) ist compared with the value of the referenced value, which name
			// is the value of the variable column.

			String col = String.Empty;

			// get the parent lov.
			ListOfValues lov = (ListOfValues)listsOfValues[parentLov];

			// iterate the parent lov.
			foreach ( ListOfValuesItem item in lov.Items )
			{
				// get the value of the referenced column.
				col = item.GetValueForAttribute(column);
				
				// compare, if the value of the referenced column is the same as 
				// the requested value.
				if ( col.Equals(colValue) )
				{
					// set child.
					item.AddChild(childItem);
					item.ChildListId = childLov;
				}
			}
		}

		#endregion // Methods
	}
}
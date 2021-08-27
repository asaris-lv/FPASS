using System;
using System.Collections;

namespace de.pta.Component.ListOfValues.Internal
{
	/// <summary>
	/// Encapsulates a list of values.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> M.Keller, PTA GmbH
	/// <b>Date:</b> Apr/02/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class ListOfValues
	{
		#region Members

		private ArrayList items;
		private ArrayList configAttributes;
		private String id;
		private bool readAlwaysNew;
		private String sqlStatement;
		private String parentList;
		private String refAttribute;
		private String localAttribute;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// constructs the object.
		/// </summary>
		public ListOfValues()
		{
			initialize();
		}

		#endregion //End of Constructors

		#region Initialization

		private void initialize()
		{
			// Initializes the members.
			items				= new ArrayList();
			configAttributes	= new ArrayList();
			id					= String.Empty;
			readAlwaysNew		= false;
			sqlStatement		= String.Empty;
			parentList			= String.Empty;
			refAttribute		= String.Empty;
			localAttribute		= String.Empty;
		}

		#endregion //End of Initialization

		#region Accessors 

		/// <summary>
		/// Gets the list of list of value items (read only).
		/// </summary>
		public ArrayList Items
		{
			get
			{
				return items;
			}
		}

		/// <summary>
		/// Gets the config attributes of the list of values (read only).
		/// </summary>
		public ArrayList ConfigAttributes
		{
			get
			{
				return configAttributes;
			}
		}

		/// <summary>
		/// Gets or sets the id of the list of values.
		/// </summary>
		public String Id
		{
			get
			{
				return id;
			}
			set
			{
				id = value;
			}
		}

		/// <summary>
		/// Gets and sets the flag, that indicats if a list of values
		/// is read by every request.
		/// </summary>
		public bool ReadAlwaysNew
		{
			get
			{
				return readAlwaysNew;
			}
			set
			{
				readAlwaysNew = value;
			}
		}

		/// <summary>
		/// Gets or sets the SQL statement to read the kist of
		/// values from a database.
		/// </summary>
		public String SqlStatement
		{
			get
			{
				return sqlStatement;
			}
			set
			{
				sqlStatement = value;
			}
		}

		/// <summary>
		/// Gets or sets the the id of the parent list of 
		/// the current list of values.
		/// </summary>
		public String ParentList
		{
			get
			{
				return parentList;
			}
			set
			{
				parentList = value;
			}
		}

		/// <summary>
		/// Gets or sets the reference attribute of the list of values.
		/// </summary>
		public String RefAttribute
		{
			get
			{
				return refAttribute;
			}
			set
			{
				refAttribute = value;
			}
		}

		/// <summary>
		/// Gets or sets the local attribute of the list of values.
		/// </summary>
		public String LocalAttribute
		{
			get
			{
				return localAttribute;
			}
			set
			{
				localAttribute = value;
			}
		}

		#endregion //End of Accessors

		#region Methods

		/// <summary>
		/// Adds a attribute (from the configuration) to the list of values.
		/// </summary>
		/// <param name="attribute">Config attribute to add.</param>
		public void AddConfigAttribute(Attribute attribute)
		{
			configAttributes.Add(attribute);
		}

		/// <summary>
		/// Verifies if the list has a parent list.
		/// </summary>
		/// <returns>
		/// Returns true if the list has a parent, otherwise false.
		/// </returns>
		public bool HasParent()
		{
			return ( !ParentList.Equals(String.Empty) );
		}

		/// <summary>
		/// Adds an new ListOfValuesItem to the internal collection.
		/// </summary>
		/// <param name="item">The item to add.</param>
		public void AddItem(ListOfValuesItem item)
		{
			items.Add(item);
		}

		/// <summary>
		/// Gets the list of values item for the given id.
		/// </summary>
		/// <param name="id">Id of the list of values item.</param>
		/// <returns>
		/// A reference to a list of values item, if no item for the given
		/// id is found, null is returned.
		/// </returns>
		public ListOfValuesItem GetItemForId(String id)
		{
			ListOfValuesItem retItem = null;

			// Loop all items, until the requested id
			// is found.
			foreach ( ListOfValuesItem item in Items )
			{
				if ( item.Id.Equals(id) )
				{
					retItem = item;
					break;
				}
			}

			// Return found item, may be null.
			return retItem;
		}

		/// <summary>
		/// Evaluates, if the given column is part of the list.
		/// </summary>
		/// <param name="column">The column name to verify.</param>
		/// <returns>
		/// True, if the columns is part of the current list, otherwise false.
		/// </returns>
		public bool IsColumnOfList(String column)
		{
			bool isColumnOfList			= false;

			// Loop all items of the list.
			foreach ( ListOfValuesItem lovItem in items )
			{
				// Ensure item is valid.
				if ( null != lovItem )
				{
					// Examin only the first lov item, because all
					// items of a list must have the same structure.
					// So, when the column is not found in the first
					// item, it will not be found in another item.
					isColumnOfList = lovItem.ExistsAttribute(column);
					break;
				}
			}

			return isColumnOfList;
		}

		/// <summary>
		/// Clears the list of value. Does not initialize the object,
		/// so call this method only for cleanup purposes.
		/// </summary>
		public void Clear()
		{
			items		   = null;
			id             = String.Empty;
			parentList     = String.Empty;
			refAttribute   = String.Empty;
			localAttribute = String.Empty;
		}

		/// <summary>
		/// Clears the items
		/// </summary>
		public void ClearItems()
		{
			items.Clear();
		}

		#endregion
	}
}
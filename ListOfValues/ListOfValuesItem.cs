using System;
using System.Collections;

namespace de.pta.Component.ListOfValues.Internal
{
	/// <summary>
	/// 
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> M.Keller, PTA GmbH
	/// <b>Date:</b> Apr/02/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class ListOfValuesItem
	{
		#region Members

		private String		id;
		private ArrayList	childs;
		private String		childListId;
		private Hashtable	attributes;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Constructs the object.
		/// </summary>
		public ListOfValuesItem(int numberAttributes)
		{
			initialize(numberAttributes);
		}

		#endregion //End of Constructors

		#region Initialization

		private void initialize(int numberAttributes)
		{
			// Initializes the members.
			Id		    = String.Empty;
			childs      = new ArrayList();
			childListId = String.Empty;
			attributes  = new Hashtable(numberAttributes);
		}

		#endregion //End of Initialization

		#region Accessors 

		/// <summary>
		/// Gets or sets the id of the item.
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
		/// Gets or sets the id of the child list.
		/// </summary>
		public String ChildListId
		{
			get
			{
				return childListId;
			}
			set
			{
				childListId = value;
			}
		}

		/// <summary>
		/// Gets the child items (read only).
		/// </summary>
		public ArrayList Childs
		{
			get
			{
				return childs;
			}
		}

		#endregion //End of Accessors

		#region Methods

		/// <summary>
		/// Adds an new entry to the childs.
		/// </summary>
		/// <param name="item">List of values item to add.</param>
		public void AddChild(ListOfValuesItem item)
		{
			// add to the array.
			childs.Add(item);
		}

		/// <summary>
		/// Adds a key and a value to the attributes.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="obj">The value.</param>
		public void AddValues(String key, Object obj)
		{
			attributes.Add(key, obj);
		}

		/// <summary>
		/// The value to the requested attribute (column) name.
		/// </summary>
		/// <param name="attribute">Contains the attribute (column) name.</param>
		/// <returns>A string that contains the value for the requested attribute.</returns>
		public String GetValueForAttribute(String attribute)
		{
			String colValue = String.Empty;

			if ( attributes.ContainsKey(attribute) )
			{
				colValue = attributes[attribute].ToString();
			}
			else
			{
				ListOfValueException lovEx = new ListOfValueException("ERROR_INVALID_COLUMN");
				lovEx.AddParameter(attribute);
				throw lovEx;
			}

			return colValue;
		}

		/// <summary>
		/// Evaluates, if a column name is an attribute of the lov item.
		/// </summary>
		/// <param name="attribute">The attribute name (column).</param>
		/// <returns>
		/// True, is the column name is an attribute of the lov item, otherwise false.
		/// </returns>
		public bool ExistsAttribute(String attribute)
		{
			return attributes.ContainsKey(attribute);
		}

		#endregion
	}
}
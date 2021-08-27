using System;
using System.Collections;
using de.pta.Component.ListOfValues;

namespace de.pta.Component.ListOfValues.Internal
{
	/// <summary>
	/// Encapsulates an sub list demand.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> M.Keller, PTA GmbH
	/// <b>Date:</b> Apr/02/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class SubListDemand : ListDemand
	{
		#region Members
		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Constructs the object.
		/// </summary>
		public SubListDemand() : base()
		{
		}

		#endregion //End of Constructors

		#region Initialization
		#endregion //End of Initialization

		#region Accessors 
		#endregion //End of Accessors

		#region Methods

		/// <summary>
		/// Processes an sub list demand.
		/// </summary>
		/// <param name="listId">Id of the list.</param>
		/// <param name="column">The column name.</param>
		/// <param name="itemId">Id of the item.</param>
		/// <returns>An array with the list elements.</returns>
		public ArrayList ProcessSubDemand(String listId, String column, String itemId)
		{
			ArrayList retArray			= new ArrayList();
			ListOfValues lov			= null;
			ListOfValuesItem item		= null;
			String childListName		= String.Empty;
			ArrayList childs			= null;
			LovItem listItem			= null;

			// get the list to the given listId.
			lov = ListStructure.GetInstance().GetList(listId);

			// Ensure, that for the given lov id a valid lov is found.
			if ( null == lov )
			{
				throw new ListOfValueException("ERROR_LOV_INVALID_ID");
			}

			// get the item to the given itemId.
			item = lov.GetItemForId(itemId);

			// Ensure, that the item is valid.
			if ( null == item )
			{
				throw new ListOfValueException("ERROR_LOV_INVALID_ITEM_ID");
			}

			// retrieve the child list name and its childs.
			childListName = item.ChildListId;
			childs		  = item.Childs;

			// get the real item to the references of the childs
			foreach (ListOfValuesItem childItem in childs)
			{
				listItem			= new LovItem();

				listItem.Id			= childItem.Id;
				listItem.ItemValue	= GetLanguageText(childItem.GetValueForAttribute(column));

				// add to the array.
				retArray.Add(listItem);
			}

			return retArray;
		}

		#endregion // Methods
	}
}
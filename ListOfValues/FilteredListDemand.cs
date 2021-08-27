using System;
using System.Collections;

namespace de.pta.Component.ListOfValues.Internal
{
	/// <summary>
	/// Encapsulates a filtered list demand.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> M.Keller, PTA GmbH
	/// <b>Date:</b> Apr/22/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class FilteredListDemand : ListDemand
	{
		#region Members

		private const String NULL	= "NULL";

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Constructs the object.
		/// </summary>
		public FilteredListDemand() : base()
		{
		}

		#endregion //End of Constructors

		#region Initialization
		#endregion //End of Initialization

		#region Accessors 
		#endregion //End of Accessors

		#region Methods

		/// <summary>
		/// Processes an filtered list demand.
		/// </summary>
		/// <param name="listId">Id of the list.</param>
		/// <param name="column">The column name.</param>
		/// <param name="attributeName">Name of the column to restrict.</param>
		/// <param name="attributeValues">ArrayList of strings including the values to restrict.</param>
		/// <returns>An array with the list elements.</returns>
		public ArrayList ProcessFilteredDemand(String listId, String column, String attributeName, ArrayList attributeValues)
		{
			ArrayList retArray	  = new ArrayList();
			ListOfValues lov	  = null;
			LovItem listItem	  = null;

			// get the list to the requested listId
			lov = ListStructure.GetInstance().GetList(listId);

			// Ensure, that for the given lov id a valid lov is found.
			if ( null == lov )
			{
				throw new ListOfValueException("ERROR_LOV_INVALID_ID");
			}

			// Iterate the list an fill the ArrayList with the values of
			// the requested column.
			foreach ( ListOfValuesItem item in lov.Items )
			{
				// Get value for requestet attribute.
				String attributeValue = item.GetValueForAttribute(attributeName);

				// Add only if attributeValue is in the list of attributeValues.
				if ( IsValueInList(attributeValue, attributeValues) )
				{
					listItem			= new LovItem();
					listItem.Id			= item.Id;
					listItem.ItemValue	= GetLanguageText(item.GetValueForAttribute(column));

					// add the requested column.
					retArray.Add(listItem);
				}
			}

			return retArray;
		}

		private bool IsValueInList(String attributeValue, ArrayList attributeValues)
		{
			// Assume the attributeValue is not in the ArrayList.
			bool isValueInList = false;

			// Loop the ArrayList. If one value matches break the loop.
			foreach ( String str in attributeValues )
			{
				// Special handling for "NULL". If null is in the
				// list the value from the database is empty.
				if ( str.ToUpper().Equals(NULL) )
				{
					if ( attributeValue.Equals(String.Empty) )
					{
						// Found matching value, break.
						isValueInList = true;
						break;
					}
				}
				else // compare the strings.
				{
					if ( str.Equals(attributeValue) )
					{
						// Found matching value, break.
						isValueInList = true;
						break;
					}
				}
			}

			return isValueInList;
		}

		/// <summary>
		/// Processes an filtered list demand.
		/// </summary>
		/// <param name="listId">Id of the list.</param>
		/// <param name="column">The column name.</param>
		/// <param name="attributeName1">Name of the column to restrict.</param>
		/// <param name="attributeValues1">ArrayList of strings including the values to restrict.</param>
		/// <param name="attributeName2">Name of the column to restrict.</param>
		/// <param name="attributeValues2">ArrayList of strings including the values to restrict.</param>
		/// <returns>An array with the list elements.</returns>
		public ArrayList ProcessFilteredDemand(String listId, String column, 
											   String attributeName1, ArrayList attributeValues1,
											   String attributeName2, ArrayList attributeValues2)
		{
			ArrayList retArray	  = new ArrayList();
			ListOfValues lov	  = null;
			LovItem listItem	  = null;

			// get the list to the requested listId
			lov = ListStructure.GetInstance().GetList(listId);

			// Ensure, that for the given lov id a valid lov is found.
			if ( null == lov )
			{
				throw new ListOfValueException("ERROR_LOV_INVALID_ID");
			}

			// Iterate the list an fill the ArrayList with the values of
			// the requested column.
			foreach ( ListOfValuesItem item in lov.Items )
			{
				// Get value for requestet attribute.
				String attributeValue1 = item.GetValueForAttribute(attributeName1);
				String attributeValue2 = item.GetValueForAttribute(attributeName2);

				// Add only if both attributeValues are in the list of attributeValues.
				if (   IsValueInList(attributeValue1, attributeValues1) 
					&& IsValueInList(attributeValue2, attributeValues2))
				{
					listItem			= new LovItem();
					listItem.Id			= item.Id;
					listItem.ItemValue	= GetLanguageText(item.GetValueForAttribute(column));

					// add the requested column.
					retArray.Add(listItem);
				}
			}

			return retArray;
		}

		#endregion
	}
}

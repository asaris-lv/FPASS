using System;
using System.Collections;
using de.pta.Component.ListOfValues;

namespace de.pta.Component.ListOfValues.Internal
{
	/// <summary>
	/// Encapsulates a root list demand.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> M.Keller, PTA GmbH
	/// <b>Date:</b> Apr/02/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class RootListDemand : ListDemand
	{
		#region Members
		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Constructs the object.
		/// </summary>
		public RootListDemand() : base()
		{
		}

		#endregion //End of Constructors

		#region Initialization
		#endregion //End of Initialization

		#region Accessors 
		#endregion //End of Accessors

		#region Methods

		/// <summary>
		/// Processes an root list demand.
		/// </summary>
		/// <param name="listId">Id of the list.</param>
		/// <param name="column">The column name.</param>
		/// <returns>An array with the list elements.</returns>
		public ArrayList ProcessRootDemand(String listId, String column)
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
				listItem			= new LovItem();

				listItem.Id			= item.Id;
				listItem.ItemValue	= GetLanguageText(item.GetValueForAttribute(column));

				// add the requested column.
				retArray.Add(listItem);
			}

			return retArray;
		}

		#endregion // Methods
	}
}
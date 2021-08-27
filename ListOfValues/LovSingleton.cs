using System;
using System.Collections;
using de.pta.Component.ListOfValues.Internal;

namespace de.pta.Component.ListOfValues
{
	/// <summary>
	/// Summary description for LovSingleton.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> M.Keller, PTA GmbH
	/// <b>Date:</b> Apr/02/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	public class LovSingleton
	{
		#region Members

		private static LovSingleton instance = null;
		private bool configurationRead;

		#endregion //End of Members

		#region Constructors

		private LovSingleton()
		{
			// Private constructor for singleton implementation, to control instantiation.
			initialize();
		}

		#endregion //End of Constructors

		#region Initialization

		private void initialize()
		{
			// Initializes the members.
			configurationRead = false;
		}

		/// <summary>
		/// Returns the one and only instance of the Singleton.
		/// </summary>
		/// <returns>An instance of the LovSingleton</returns>
		public static LovSingleton GetInstance()
		{
			if ( null == instance )
			{
				instance = new LovSingleton();
			}

			return instance;
		}

		#endregion //End of Initialization

		#region Accessors 
		#endregion //End of Accessors

		#region Methods

		/// <summary>
		/// Invokes the reading of the configuration. Configuration 
		/// can be read more than one times. But must be called at 
		/// least once.
		/// However, if configuration must be reloaded you can call 
		/// ReadConfiguration again.
		/// </summary>
		public void ReadConfiguration()
		{
			// read the configuration.
			ListStructure.GetInstance().ReadConfiguration();

			// set information, that configuration was read.
			configurationRead = true;
		}

		/// <summary>
		/// Provides an ArrayList.
		/// </summary>
		/// <param name="local">Localization interface.</param>
		/// <param name="listId">The id of the list.</param>
		/// <param name="column">The column name.</param>
		/// <returns>An Array list.</returns>
		public ArrayList GetRootList(ILocalization local, String listId, String column)
		{
			// Check, if the configuration was read.
			if ( !configurationRead )
			{
				throw new ListOfValueException("ERROR_LOV_NOTCONFIGERED");
			}

			// Create a new RootListDemand, set the language and process the list.
			RootListDemand rootListDemand = new RootListDemand();
			rootListDemand.Localization = local;
			return rootListDemand.ProcessRootDemand(listId, column);
		}

		/// <summary>
		/// Provides an ArrayList.
		/// </summary>
		/// <param name="local">Localization interface.</param>
		/// <param name="listId">The id of the list.</param>
		/// /// <param name="column">The column name.</param>
		/// <param name="itemId">The id of the item.</param>
		/// <returns>An Array list.</returns>
		public ArrayList GetSubList(ILocalization local, String listId, String column, String itemId)
		{
			// Check, if the configuration was read.
			if ( !configurationRead )
			{
				throw new ListOfValueException("ERROR_LOV_NOTCONFIGERED");
			}

			// create a new SubListDemand, set the language and process.
			SubListDemand subListDemand = new SubListDemand();
			subListDemand.Localization = local;
			return subListDemand.ProcessSubDemand(listId, column, itemId);
		}

		/// <summary>
		/// Gets an filtered ArrayList.
		/// </summary>
		/// <param name="local">Localization interface.</param>
		/// <param name="listId">The id of the list.</param>
		/// <param name="column">The column name.</param>
		/// <param name="attributeName">Name of the column to restrict.</param>
		/// <param name="attributeValues">ArrayList of strings including the values to restrict.</param>
		/// <returns>An Array list.</returns>
		public ArrayList GetFilteredList(ILocalization local, String listId, String column, String attributeName, ArrayList attributeValues)
		{
			// Check, if the configuration was read.
			if ( !configurationRead )
			{
				throw new ListOfValueException("ERROR_LOV_NOTCONFIGERED");
			}

			// create a new FilteredListDemand, set the language and process.
			FilteredListDemand filteredListDemand = new FilteredListDemand();
			filteredListDemand.Localization = local;
			return filteredListDemand.ProcessFilteredDemand(listId, column, attributeName, attributeValues);
		}

		/// <summary>
		/// Gets an filtered ArrayList.
		/// </summary>
		/// <param name="local">Localization interface.</param>
		/// <param name="listId">The id of the list.</param>
		/// <param name="column">The column name.</param>
		/// <param name="attributeName1">Name of the column to restrict.</param>
		/// <param name="attributeValues1">ArrayList of strings including the values to restrict.</param>
		/// <param name="attributeName2">Name of the column to restrict.</param>
		/// <param name="attributeValues2">ArrayList of strings including the values to restrict.</param>
		/// <returns>An Array list.</returns>
		public ArrayList GetFilteredList(ILocalization local, String listId, String column, 
										 String attributeName1, ArrayList attributeValues1,
										 String attributeName2, ArrayList attributeValues2)
		{
			// Check, if the configuration was read.
			if ( !configurationRead )
			{
				throw new ListOfValueException("ERROR_LOV_NOTCONFIGERED");
			}

			// create a new FilteredListDemand, set the language and process.
			FilteredListDemand filteredListDemand = new FilteredListDemand();
			filteredListDemand.Localization = local;
			return filteredListDemand.ProcessFilteredDemand(listId, column, attributeName1, attributeValues1,
																			attributeName2, attributeValues2);
		}

		/// <summary>
		/// Returns the id of a list of values. The returned id is the child list
		/// to the given parent list id.
		/// </summary>
		/// <param name="parentListId">Id of the parent list of values.</param>
		/// <returns>An id of an list of values if found, otherwise an empty string.</returns>
		public String GetSubListId(String parentListId)
		{
			// Get information from the ListStructure-Singleton.
			return ListStructure.GetInstance().GetChildListOfValues(parentListId);
		}

		/// <summary>
		/// Gets the number of childs to a list of values.
		/// </summary>
		/// <param name="parentListId">Id of the parent list of values.</param>
		/// <returns>The number of child list of values.</returns>
		public int GetNumberOfChildLists(String parentListId)
		{
			// Get the information from the ListStructure-Singleton.
			return ListStructure.GetInstance().GetNumberOfChilds(parentListId);
		}

		/// <summary>
		/// States if the given column is a column of the given list of values.
		/// </summary>
		/// <param name="column">Name of the column to verify.</param>
		/// <param name="listId">Name of the list of values.</param>
		/// <returns>
		/// True if the list of values exists and the column is part of these list,
		/// otherwise false is returned.
		/// </returns>
		public bool IsColumnOfList(String column, String listId)
		{
			return ListStructure.GetInstance().IsColumnOfList(column, listId);
		}

		/// <summary>
		/// Sets the value for the restiction. A Restriction must be configured
		/// in an SQL statement in the configuration.
		/// </summary>
		/// <param name="restiction">Value for the restiction.</param>
		public void SetSqlRestriction(String restiction)
		{
			ListStructure.GetInstance().Restriction = restiction;
		}

		#endregion // Methods
	}
}
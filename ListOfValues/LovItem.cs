using System;

using de.pta.Component.ListOfValues.Internal;

namespace de.pta.Component.ListOfValues
{
	/// <summary>
	/// Encapsulates an entry of a list of values. 
	/// </summary>
	/// <remarks>
	/// This class is desiged to use outside the component. It is used to exchange 
	/// data over the LovSingleton interface.
	/// </remarks>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> M.Keller, PTA GmbH
	/// <b>Date:</b> Apr/02/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	public class LovItem : IComparable
	{
		#region Members

		private String id; 
		private String itemValue;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Constructs the object.
		/// </summary>
		public LovItem()
		{
			initialize();
		}

		/// <summary>
		/// Constructs the object with the given parameters.
		/// </summary>
		/// <param name="id">Id of the item.</param>
		/// <param name="itemValue">Value of the item.</param>
		public LovItem(string id, string itemValue)
		{
			this.id			= id;
			this.itemValue	= itemValue;
		}

		#endregion //End of Constructors

		#region Initialization

		private void initialize()
		{
			// Initializes the member variables.
			id			= String.Empty;
			itemValue	= String.Empty;
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
		/// Gets the id of the item as an long value (Read only).
		/// Throws an exception, when the id of the item is not
		/// numeric.
		/// </summary>
		public long IdAsLong
		{
			get
			{
				long idAsLong = long.MinValue;

				try
				{
					// Parse the id and catch any exception.
					idAsLong = long.Parse(id);
				}
				catch ( Exception e )
				{
					throw new ListOfValueException("ERR_LOV_ITEM_ID_IS_NOT_NUMERIC", e);
				}

				return idAsLong;
			}
		}

		/// <summary>
		/// Gets the id of the item as an decimal value (Read only).
		/// Throws an exception, when the id of the item is not
		/// numeric.
		/// </summary>
		public decimal DecId
		{
			get
			{
				decimal idAsDecimal = decimal.MinValue;

				try
				{
					// Parse the id and catch any exception.
					idAsDecimal = decimal.Parse(id);
				}
				catch ( Exception e )
				{
					throw new ListOfValueException("ERR_LOV_ITEM_ID_IS_NOT_NUMERIC", e);
				}

				return idAsDecimal;
			}
		}

		/// <summary>
		/// Gets or sets the value of the item.
		/// </summary>
		public String ItemValue
		{
			get
			{
				return itemValue;
			}
			set
			{
				itemValue = value;
			}
		}

		#endregion //End of Accessors

		#region Methods
		#endregion // Methods

		#region IComparable Members

		/// <summary>
		/// Compares the item value of the passed object with the item value of this object.
		/// </summary>
		/// <param name="obj">Object to compare.</param>
		/// <returns>
		/// Less than zero: this instance is less than obj. 
		/// Zero: this instance is equal to obj. 
		///	Greater than zero: this instance is greater than obj.  
		/// </returns>
		/// <remarks>
		/// The implementation of the IComparable interface makes it possioble
		/// to use the ArrayList.Sort() method.
		/// </remarks>
		public int CompareTo(object obj)
		{
			// Ensure we have a LovItem.
			if(obj is LovItem)
			{
				LovItem temp = (LovItem)obj;
				return itemValue.CompareTo(temp.ItemValue);
			}
			else
			{
				throw new ListOfValueException("ERR_INVALID_OBJECT_TO_COMPARE");
			}
		}

		#endregion

	}
}
using System;

namespace de.pta.Component.DataAccess.Internal
{
	/// <summary>
	/// Encapsulates a query attribute.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> M.Keller, PTA GmbH
	/// <b>Date:</b> Apr/02/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class QryAttribute : ICloneable
	{
	#region Members

		private String id;
		private String colName;
		private int	   colWidthPx;
		private String dataType;
		private String attribList;
		private bool   dimensionAttribute;

	#endregion //End of Members

	#region Constructors

		/// <summary>
		/// Constructs the object.
		/// </summary>
		public QryAttribute()
		{
			initialize();
		}

	#endregion //End of Constructors

	#region  Initialization

		private void initialize()
		{
			// Initializes the members.
			id					= String.Empty;
			colName				= String.Empty;
			colWidthPx			= 0;
			dataType			= String.Empty;
			attribList			= String.Empty;
			dimensionAttribute	= false;
		}

	#endregion //End of Initialization

	#region Accessors 

		/// <summary>
		/// Gets or sets the id.
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
		/// Gets or sets the colName.
		/// </summary>
		public String ColName
		{
			get
			{
				return colName;
			}
			set
			{
				colName = value;
			}
		}

		/// <summary>
		/// Gets of sets the column with in pixel.
		/// </summary>
		public int ColWidthPx
		{
			get
			{
				return colWidthPx;
			}
			set
			{
				colWidthPx = value;
			}
		}

		/// <summary>
		/// Gets or sets the dataType.
		/// </summary>
		public String DataType
		{
			get
			{
				return dataType;
			}
			set
			{
				dataType = value;
			}
		}

		/// <summary>
		/// Gets or sets the attribute list. 
		/// </summary>
		public String AttribList
		{
			get
			{
				return attribList;
			}
			set
			{
				attribList = value;
			}
		}

		/// <summary>
		/// Gets or sets the dimensionAttribute.
		/// </summary>
		public bool DimensionAttribute
		{
			get
			{
				return dimensionAttribute;
			}
			set
			{
				dimensionAttribute = value;
			}
		}

	#endregion //End of Accessors

	#region Methods

		/// <summary>
		/// Makes an deep copy of the object.
		/// </summary>
		/// <returns>An reference to the clone object.</returns>
		public Object Clone()
		{
			QryAttribute clone = new QryAttribute();

			// copy members.
			clone.id					= id;
			clone.colName				= colName;
			clone.colWidthPx			= colWidthPx;
			clone.dataType				= dataType;
			clone.attribList			= attribList;
			clone.dimensionAttribute	= dimensionAttribute;

			return clone;
		}

	#endregion
	}
}

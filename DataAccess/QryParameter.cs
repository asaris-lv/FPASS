using System;

namespace de.pta.Component.DataAccess.Internal
{
	/// <summary>
	/// Encapsulates a query parameter.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> M.Keller, PTA GmbH
	/// <b>Date:</b> Apr/02/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class QryParameter : ICloneable
	{
		#region Members

		private String id;
		private String colName;
		private String dataType;
		private String paraList;
		private String sqlOperator;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Constructs the object.
		/// </summary>
		public QryParameter()
		{
			initialize();
		}
		#endregion //End of Constructors

		#region  Initialization

		private void initialize()
		{
			// Initializes the members.
			id			= "";
			colName		= "";
			dataType	= "";
			paraList	= "";
			sqlOperator	= "";
		}

		#endregion //End of Initialization

		#region Accessors 

		/// <summary>
		/// Accessor for id.
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
		/// Accessor for colName.
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
		/// Accessor for dataType
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
		/// Accessor for paraType
		/// </summary>
		public String ParaList
		{
			get
			{
				return paraList;
			}
			set
			{
				paraList = value;
			}
		}

		/// <summary>
		/// Set or gets the sqlOperator.
		/// </summary>
		public String SqlOperator
		{
			get
			{
				return sqlOperator;
			}
			set
			{
				sqlOperator = value;
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
			QryParameter clone = new QryParameter();

			// copy members.
			clone.id			= id;
			clone.colName		= colName;
			clone.dataType		= dataType;
			clone.paraList		= paraList;
			clone.sqlOperator	= sqlOperator;

			return clone;
		}

		#endregion
	}
}

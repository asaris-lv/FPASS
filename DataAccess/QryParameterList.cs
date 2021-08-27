using System;

namespace de.pta.Component.DataAccess.Internal
{
	/// <summary>
	/// Encapsulates an query paramerter list.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> M.Keller, PTA GmbH
	/// <b>Date:</b> Apr/07/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class QryParameterList : ICloneable
	{
		#region Members

		private String id;
		private String type;
		private String code;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Default Constructor
		/// </summary>
		public QryParameterList()
		{
			initialize();
		}

		#endregion //End of Constructors

		#region Initialization

		private void initialize()
		{
			// Initializes the members.
			id		= "";
			type	= "";
			code	= "";
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
		/// Gets or sets the type.
		/// </summary>
		public String Type
		{
			get
			{
				return type;
			}
			set
			{
				type = value;
			}
		}

		/// <summary>
		/// Gets or sets the code.
		/// </summary>
		public String Code
		{
			get
			{
				return code;
			}
			set
			{
				code = value;
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
			QryParameterList clone = new QryParameterList();

			// copy members.
			clone.id	= id;
			clone.type	= type;
			clone.code	= code;

			return clone;
		}

		#endregion // Methods
	}
}

using System;

namespace de.pta.Component.DataAccess.Internal
{
	/// <summary>
	/// Description empty
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> M.Keller, PTA GmbH
	/// <b>Date:</b> Apr/16/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class QryAttributeList : ICloneable
	{
		#region Members

		private String id;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Default Constructor
		/// </summary>
		public QryAttributeList()
		{
			initialize();
		}

		#endregion //End of Constructors

		#region Initialization

		private void initialize()
		{
			// Initializes the members.
			id		= "";
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

		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// Makes an deep copy of the object.
		/// </summary>
		/// <returns>An reference to the clone object.</returns>
		public Object Clone()
		{
			QryAttributeList clone = new QryAttributeList();

			// copy members.
			clone.id	= id;

			return clone;
		}

		#endregion // End of Methods
	}
}

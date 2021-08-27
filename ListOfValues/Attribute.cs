using System;

namespace de.pta.Component.ListOfValues.Internal
{
	/// <summary>
	/// Encapsulates an column attribute
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> M.Keller, PTA GmbH
	/// <b>Date:</b> Apr/02/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class Attribute
	{
		#region Members

		private String id;
		private String type;
		private bool   isPrimeryKey;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Constructs an instance of the object. 
		/// </summary>
		public Attribute()
		{
			initialize();
		}

		#endregion //End of Constructors

		#region Initialization

		private void initialize()
		{
			// Initialize the members.
			id           = String.Empty;
			type		 = String.Empty;
			isPrimeryKey = false;
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
		/// Gets or stes the flag isPrimeryKey.
		/// </summary>
		public bool IsPrimeryKey
		{
			get
			{
				return isPrimeryKey;
			}
			set
			{
				isPrimeryKey = value;
			}
		}

		#endregion //End of Accessors

		#region Methods
		#endregion

	}
}
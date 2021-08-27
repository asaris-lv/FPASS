using System;
using System.Collections;
using System.Text;

namespace de.pta.Component.DataAccess.Internal
{
	/// <summary>
	/// Encapsulates the parameters within a request.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> M.Keller, PTA GmbH
	/// <b>Date:</b> Apr/02/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class RequestParameter
	{
		#region  Members

		private String id;
		private ArrayList paraValues;
		private String operation;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Constructs the object.
		/// </summary>
		public RequestParameter()
		{
			initialize();
		}

		#endregion //End of Constructors

		#region Initialization

		private void initialize()
		{
			// Initializes the members.
			id			= "";
			paraValues	= new ArrayList();
			operation   = "";
		}

		#endregion //End of Initialization

		#region Accessors

		/// <summary>
		/// The id of the parameter.
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
		/// Gets or stes the operation.
		/// </summary>
		public String Operation
		{
			get
			{
				return operation;
			}
			set
			{
				operation = value;
			}
		}

		#endregion //End of Accessors

		#region Methods

		/// <summary>
		/// Adds a parameter to the internal collection of parameter values.
		/// </summary>
		/// <param name="paraValue"></param>
		public void AddParaValue(String paraValue)
		{
			// If the value doesn't exist add it.
			if( !paraValues.Contains(paraValue) )
			{
				paraValues.Add(paraValue);
			}
		}

		/// <summary>
		/// Gets a string that contains the parameter values, separated by commas.
		/// </summary>
		/// <param name="apostroph">States, if the values will be put in apostrophes.</param>
		/// <returns>A string.</returns>
		public String GetParameters(bool apostroph)
		{
			StringBuilder str = new StringBuilder();
			bool insertComma  = false;

			// loop the string in the collection
			foreach ( String paraValue in paraValues )
			{
				// Don't insert a comma for the first time.
				if ( insertComma )
				{
					str.Append(", ");
				}
				else
				{
					insertComma = true;
				}

				// add apostroph, if requested.
				if ( apostroph )
				{
					str.Append("'");
				}

				// Add the actual value.
				str.Append(paraValue);

				// add apostroph, if requested.
				if ( apostroph )
				{
					str.Append("'");
				}
			}

			return str.ToString();
		}

		#endregion // Methods
	}
}

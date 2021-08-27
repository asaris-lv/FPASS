using System;
using System.Data.OleDb;

namespace de.pta.Component.DataAccess.Internal
{
	/// <summary>
	/// Encapsulates an object to 
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> M.Keller, PTA GmbH
	/// <b>Date:</b> Apr/22/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	public class MdxExchange
	{
		#region Members

		private String mdxStatement;
		private OleDbDataReader dataReader;
		private Exception executeException;
		private bool errorOccured;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Default Constructor
		/// </summary>
		public MdxExchange()
		{
			initialize();
		}

		#endregion //End of Constructors

		#region Initialization

		private void initialize()
		{
			// Initializes the members.
			mdxStatement		= "";
			dataReader			= null;
			executeException	= null;
			errorOccured		= false;
		}	

		#endregion //End of Initialization

		#region Accessors 

		/// <summary>
		/// Gets or sets the mdx statement.
		/// </summary>
		public String MdxStatement
		{
			get
			{
				return mdxStatement;
			}
			set
			{
				mdxStatement = value;
			}
		}

		/// <summary>
		/// Gets or sets the data reader.
		/// </summary>
		public OleDbDataReader DataReader
		{
			get
			{
				return dataReader;
			}
			set
			{
				dataReader = value;
			}
		}

		/// <summary>
		/// Gets or sets the exception, that may be occured.
		/// </summary>
		public Exception ExecuteException
		{
			get
			{
				return executeException;
			}
			set
			{
				executeException = value;
			}
		}

		/// <summary>
		/// Gets or sets the error occured flag.
		/// </summary>
		public bool ErrorOccured
		{
			get
			{
				return errorOccured;
			}
			set
			{
				errorOccured = value;
			}
		}

		#endregion //End of Accessors

		#region Methods 
		#endregion // End of Methods


	}
}

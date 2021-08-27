using System;
using System.Data;

namespace de.pta.Component.DataAccess.Internal
{
	/// <summary>
	/// Abstract class for for processing queries against diffenrent database types.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> A.Opierzynski, PTA GmbH
	/// <b>Date:</b> Apr/02/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal abstract class DataBaseType
    {
	#region Members

		protected Query query;
		protected IDbConnection dbConnection;
		protected IDataReader returnDataReader;
		protected String preparedStatement;
		protected bool processingCompleted;
		protected int totalRecordsRead;
		protected DataSet returnDataSet;
		protected bool queryProcessed;

	#endregion //End of Members

	#region Constructors

		/// <summary>
		/// Constructs the object.
		/// </summary>
		/// <param name="query">A reference to a query object.</param>
		public DataBaseType(Query query)
		{
			this.query					= query;
			this.dbConnection			= null;
			this.returnDataReader		= null;
			this.preparedStatement		= "";
			this.returnDataSet			= null;
			this.processingCompleted	= false;
			this.totalRecordsRead		= 0;
			this.queryProcessed			= false;
		}

	#endregion //End of Constructors

	#region Initialization
	#endregion //End of Initialization

	#region Accessors 
	#endregion //End of Accessors

	#region Methods

		/// <summary>
		/// Preparing of the query statement (joins query definition with concrete current paramater list.
		/// </summary>
		abstract public void PrepareExecutableStatement();

		/// <summary>
		/// Prepares a connection to the specific database.
		/// </summary>
		abstract public void PrepareCurrentDBConnection();

		/// <summary>
		/// Process the query statement against the database.
		/// </summary>
		/// <returns>a DataSet object that contains the first n rows of a query result</returns>
		abstract public DataSet ProcessExecutableStatement();

		/// <summary>
		/// Indicates, if the processing is completed (read only).
		/// </summary>
		public bool ProcessingCompleted
		{
			get
			{
				return processingCompleted;
			}
		}

		/// <summary>
		/// Gets the total count of read records (read only).
		/// </summary>
		public int TotalRecordsRead
		{
			get
			{
				return totalRecordsRead;
			}
		}

	#endregion
	}
}
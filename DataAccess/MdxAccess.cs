using System;
using System.Threading;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Security.Principal;
using de.pta.Component.Common;

namespace de.pta.Component.DataAccess.Internal
{
	/// <summary>
	/// Encapsulates the access to an mdx resource.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> M.Keller, PTA GmbH
	/// <b>Date:</b> Apr/22/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class MdxAccess : WorkerThread
	{
		#region Members

		private const String MDX_QUEUE_NAME			= "MdxQueue";
		private OleDbConnection oleDbConnection;
		private String connectString;
		private String user;
		private String domain;
		private String pwd;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Constructs the object.
		/// </summary>
		/// <param name="connectionString">The connect string for the MDX resource.</param>
		/// <param name="user">User for the MDX resource.</param>
		/// <param name="domain">Domain of the MDX resource.</param>
		/// <param name="pwd">Password for the MDX resource.</param>
		public MdxAccess(String connectionString, String user, String domain, String pwd) : base()
		{
			// First initialize.
			initialize();

			this.connectString	= connectionString;
			this.user			= user;
			this.domain			= domain;
			this.pwd			= pwd;
		}

		#endregion //End of Constructors

		#region Initialization

		private void initialize()
		{
			// Initializes the members.
			oleDbConnection = null;
			connectString	= "";
			user			= "";
			domain			= "";
			pwd				= "";
		}	

		#endregion //End of Initialization

		#region Accessors 

		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// Starts the mdx access thread.
		/// </summary>
		override public void Start()
		{
			// Run the thread under a different user.
			impersonation(user, domain, pwd);

			// Create a new queue object.
			QueueFactory.GetInstance().CreateQueueForId(MDX_QUEUE_NAME, 0, 0);

			try
			{
				process();
			}
			catch (ThreadAbortException e)
			{
				// Close connection
				oleDbConnection.Close();

				String msg = e.Message;
				Debug.WriteLine(msg);
			}
		}

		private void process()
		{
			SynchronizedQueue queue = null;
			MdxExchange mdxExchange = null;

			// Get the queue object.
			queue = QueueFactory.GetInstance().GetQueue(MDX_QUEUE_NAME);

			while ( true )
			{
				// Connect to the datasource.
				connect(connectString);

				Debug.WriteLine("Waiting for input ...");
				// Get the MdxExchange object.
				mdxExchange = (MdxExchange)queue.GetInput();
				Debug.WriteLine("... input received.");

				// do the work
				processStatement(mdxExchange);

				// Send the processed object back to sender.
				queue.SendBack(mdxExchange);
			}
		}

		private void processStatement(MdxExchange mdxExchange)
		{
			try
			{
				// Create new command
				OleDbCommand cmd = new OleDbCommand();

				// Assign the connection to the command
				cmd.Connection = oleDbConnection;

				// Assign a query to the commandtext
				cmd.CommandText = mdxExchange.MdxStatement;

				// Execute the reader
				mdxExchange.DataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
			}
			catch ( Exception e)
			{
				// Set error.
				mdxExchange.ErrorOccured = true;
				mdxExchange.ExecuteException = e;
			}
		}

		private void connect(String connectString)
		{
			try
			{
				// Create new connection object.
				oleDbConnection = new OleDbConnection(connectString);

				// Open the database connection.
				oleDbConnection.Open();
			}
			catch ( Exception e)
			{
				lastError = e.Message;
				Debug.WriteLine(lastError);
			}
		}


		#endregion // End of Methods

	}
}

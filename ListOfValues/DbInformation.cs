using System;


namespace de.pta.Component.ListOfValues.Internal 
{
	/// <summary>
	/// Encapsulates the connection information and the class names
	/// used to process the reading from a database.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> M.Keller, PTA GmbH
	/// <b>Date:</b> Oct/16/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	public class DbInformation 
	{
		#region Members

		private String connectString;
		private String assemblyName;
		private String connectionClass;
		private String commandClass;
		private String dataReaderClass;

		#endregion

		#region Construction

		/// <summary>
		/// Constructs an instance of the object.
		/// </summary>
		public DbInformation()
		{
			initialize();
		}

		#endregion

		#region Initialization

		private void initialize()
		{
			// Initialize the attibutes
			connectString		= String.Empty;
			assemblyName		= String.Empty;
			connectionClass		= String.Empty;
			commandClass		= String.Empty;
			dataReaderClass		= String.Empty;
		}

		#endregion

		#region Accessors 

		/// <summary>
		/// Gets and sets the name of the assembly in which the
		/// classes reside.
		/// </summary>
		public String AssemblyName
		{
			get
			{
				return assemblyName;
			}
			set
			{
				assemblyName = value;
			}
		}

		/// <summary>
		/// Gets and sets the name of the command class.
		/// The name should be fully specified.
		/// </summary>
		public String CommandClass
		{
			get
			{
				return commandClass;
			}
			set
			{
				commandClass = value;
			}
		}

		/// <summary>
		/// Gets and sets the name of the connection class.
		/// The name should be fully specified.
		/// </summary>
		public String ConnectionClass
		{
			get
			{
				return connectionClass;
			}
			set
			{
				connectionClass = value;
			}
		}

		/// <summary>
		/// Gets and sets the connection string for the database.
		/// </summary>
		public String ConnectString
		{
			get
			{
				return connectString;
			}
			set
			{
				connectString = value;
			}
		}

		/// <summary>
		/// Gets and sets the name of the dataReader class.
		/// The name should be fully specified.
		/// </summary>
		public String DataReaderClass
		{
			get
			{
				return dataReaderClass;
			}
			set
			{
				dataReaderClass = value;
			}
		}

		#endregion

		#region Methods

		#endregion

	}
}
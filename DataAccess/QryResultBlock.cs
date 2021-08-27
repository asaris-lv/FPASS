using System;

namespace de.pta.Component.DataAccess.Internal
{
	/// <summary>
	/// Encapsulates one block of a query results.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> A.Opierzynski, PTA GmbH
	/// <b>Date:</b> Apr/02/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class QryResultBlock
	{
		#region Members

		private int		blockId;
		private String	nameResultXmlFile;
		private int		blockSort;

		#endregion //End of Members

		#region  Constructors 

		/// <summary>
		/// Constructs the object.
		/// </summary>
		public QryResultBlock()
		{
			initialize();
		}

		/// <summary>
		/// Constructs the object and initializes the members with the given parameters.
		/// </summary>
		/// <param name="blockId"></param>
		/// <param name="nameResultXmlFile"></param>
		/// <param name="blockSort"></param>
		public QryResultBlock(int blockId, String nameResultXmlFile, int blockSort)
		{
			this.blockId			= blockId;
			this.nameResultXmlFile	= nameResultXmlFile;
			this.blockSort			= blockSort;
		}

		#endregion //End of Constructors
	
		#region Initialization

		private void initialize()
		{
			// Initializes the members.
			blockId				= 0;
			nameResultXmlFile	= "";
			blockSort			= 0;
		}

		#endregion //End of Initialization

		#region Accessors

		/// <summary>
		/// Gets and sets the blockId of the object.
		/// </summary>
		public int BlockId
		{
			get
			{
				return blockId;
			}
			set
			{
				blockId = value;
			}
		}

		/// <summary>
		/// Gets and sets the name of the result XML file.
		/// </summary>
		public String NameResultXmlFile
		{
			get
			{
				return nameResultXmlFile;
			}
			set
			{
				nameResultXmlFile = value;
			}
		}

		/// <summary>
		/// Gets and sets the blockSort.
		/// </summary>
		public int BlockSort
		{
			get
			{
				return blockSort;
			}
			set
			{
				blockSort = value;
			}
		}
		#endregion //End of Accessors

		#region Methods
		#endregion // Methods
	}
}

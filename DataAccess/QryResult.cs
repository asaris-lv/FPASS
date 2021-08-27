using System;
using System.Data;
using System.IO;
using System.Collections;
using System.Text;
using System.Threading;

namespace de.pta.Component.DataAccess.Internal
{
	/// <summary>
	/// Result values (e.g. DataSet) of executed queries. Funktionality like 
	/// navigation through all returned data 
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> A.Opierzynski, PTA GmbH
	/// <b>Date:</b> 02.04.2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class QryResult
	{
		#region Members

		private String qryResultID;
		private String userID;
		private String sessionID;
		private String queryDefName;
		private String queryID;
		private Query localQuery;

		private DataSet returnDataSet;
		private int		returnNumberOfRecords;

		private int currentUIBlockNumber;
		private int maxAvailableBlocks;

		private DataAccessManager.QryStatus qryResultStatus;
		private bool		qryProcessingComplete;
		private Hashtable	qryResultBlocks;

		private DateTime	returnXMLFileGenerationDate;
		private String		returnXMLFilePath;
		private String		returnXMLFileName;
		private String		xmlUserPath;
		private String		xmlUserPathHttp;
		private String		xmlSessionPath;

		private Thread		xmlProcessingThread;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Default Constructor
		/// </summary>
		public QryResult()
		{
			initialize();
		}

		/// <summary>
		/// Contructor with ID
		/// </summary>
		/// <param name="qryResultID"></param>
		public QryResult(String qryResultID)
		{
			this.qryResultID = qryResultID;
			initialize();
		}

		#endregion //End of Constructors

		#region  Initialization

		private void initialize()
		{
			// Initializes the members.
			this.sessionID						= String.Empty;
			this.userID							= String.Empty;
			this.queryDefName					= String.Empty;
			this.queryID						= String.Empty;
			this.localQuery						= null;
			this.returnDataSet					= null;
			this.returnNumberOfRecords			= 0;
			this.currentUIBlockNumber			= 0;
			this.maxAvailableBlocks				= 0;
			this.qryResultStatus				= DataAccessManager.QryStatus.INIT;
			this.qryProcessingComplete			= false;
			this.returnXMLFileGenerationDate	= DateTime.Now;
			this.returnXMLFilePath				= String.Empty;
			this.returnXMLFileName				= String.Empty;
			this.xmlUserPath					= String.Empty;
			this.xmlUserPathHttp				= String.Empty;
			this.xmlSessionPath					= String.Empty;
			this.qryResultBlocks				= new Hashtable();
			this.xmlProcessingThread			= null;
		}	

		#endregion //End of Initialization

		#region Accessors 

		/// <summary>
		/// Accessor for qryResultID.
		/// </summary>
		public String QryResultID
		{
			get
			{
				return qryResultID;
			}
			set
			{
				qryResultID = value;
			}
		}

		/// <summary>
		/// Accessor for sessionID.
		/// </summary>
		public String SessionID
		{
			get
			{
				return sessionID;
			}
			set
			{
				sessionID = value;
			}
		}

		/// <summary>
		/// Accessor for queryID.
		/// </summary>
		public String QueryID
		{
			get
			{
				return queryID;
			}
			set
			{
				queryID = value;
			}
		}

		/// <summary>
		/// Gets or sets the local query object.
		/// </summary>
		public Query LocalQuery
		{
			get
			{
				return localQuery;
			}
			set
			{
				localQuery = value;
			}
		}

		/// <summary>
		/// Accessor for returnDataSet.
		/// </summary>
		public DataSet ReturnDataSet
		{
			get
			{
				return returnDataSet;
			}
			set
			{
				returnDataSet = value;
			}
		}

		/// <summary>
		/// Accessor for returnNumberOfRecords.
		/// </summary>
		public int ReturnNumberOfRecords
		{
			get
			{
				return returnNumberOfRecords;
			}
			set
			{
				returnNumberOfRecords = value;
			}
		}

		/// <summary>
		/// Accessor for qryResultStatus.
		/// </summary>
		public DataAccessManager.QryStatus QryResultStatus
		{
			get
			{
				return qryResultStatus;
			}
			set
			{
				qryResultStatus = value;
			}
		}

		/// <summary>
		/// Accessor for qryProcessingComplete.
		/// </summary>
		public bool QryProcessingComplete
		{
			get
			{
				return qryProcessingComplete;
			}
			set
			{
				qryProcessingComplete = value;
			}
		}

		/// <summary>
		/// Accessor for UserID.queryDefName
		/// </summary>
		public String UserID
		{
			get
			{
				return userID;
			}
			set
			{
				userID = value;
			}
		}

		/// <summary>
		/// Accessor for queryDefName
		/// </summary>
		public String QueryDefName
		{
			get
			{
				return queryDefName;
			}
			set
			{
				queryDefName = value;
			}
		}

		/// <summary>
		/// Gets (read only) the path for the XML file.
		/// </summary>
		public String ReturnXMLFilePath
		{
			get
			{
				return returnXMLFilePath;
			}
		}

		/// <summary>
		/// Gets (read only) a part of the XML file.
		/// </summary>
		public String ReturnXMLFileName
		{
			get
			{
				return returnXMLFileName;
			}
		}

		/// <summary>
		/// Gets (read only) the XML data path including the user name.
		/// </summary>
		public String XmlUserPath
		{
			get
			{
				return xmlUserPath;
			}
		}

		/// <summary>
		/// Gets (read only) the XML http data path including the user name.
		/// </summary>
		public String XmlUserPathHttp
		{
			get
			{
				return xmlUserPathHttp;
			}
		}

		/// <summary>
		/// Gets (read only) the XML data path including the session id.
		/// </summary>
		public String XmlSessionPath
		{
			get
			{
				return xmlSessionPath;
			}
		}

		/// <summary>
		/// Gets or sets the current block number.
		/// </summary>
		public int CurrentUIBlockNumber
		{
			get
			{
				return currentUIBlockNumber;
				}
			set
			{
				currentUIBlockNumber = value;
			}
		}

		/// <summary>
		/// Gets or sets the current block number.
		/// </summary>
		public int MaxAvailableBlocks
		{
			get
			{
				return maxAvailableBlocks;
			}
			set
			{
				maxAvailableBlocks = value;
			}
		}

		/// <summary>
		/// Sets and gets the thread, that processes XML data in the background.
		/// </summary>
		public Thread XmlProcessingThread
		{
			get
			{
				return xmlProcessingThread;
			}
			set
			{
				xmlProcessingThread = value;
			}
		}

		#endregion //End of Accessors

		#region Methods

		/// <summary>
		/// With this functionality business logic can get the first data block of 
		/// an query result set (from all records a query returns).
		/// </summary>
		/// <returns>
		/// Returns a copy of the DataSet object that contains the first data block.
		/// </returns>
		public DataSet GetDataSetFirstBlock ()
		{
			DataSet copiedDataSet = null;

			//reload first DataSet (only when not the first access)
			if (currentUIBlockNumber > 1)
			{
				// set UI block to one.		
				currentUIBlockNumber = 1;			
				// set returnDataSet and give it back.
				setCurrentUIReturnDataSet();
			}

			currentUIBlockNumber = 1;

			// copy DataSet
			copiedDataSet = returnDataSet.Copy();

			return copiedDataSet;
		}
		
		/// <summary>
		/// With this functionality business logic can get the next data block (from 
		/// current block forward) - the current block is handled in QryResult class.
		/// </summary>
		/// <returns>
		/// Returns a copy of the DataSet object that contains the next data block.
		/// If there is any block available null is returned.
		/// </returns>
		public DataSet GetDataSetNextBlock ()
		{
			DataSet copiedDataSet = null;

			// Ensure, that the requested block is available.
			if ( (currentUIBlockNumber + 1) <= maxAvailableBlocks )
			{
				// increment UI block
				++currentUIBlockNumber;

				// set returnDataSet and copy it.
				setCurrentUIReturnDataSet();
				copiedDataSet = returnDataSet.Copy();
			}

			return copiedDataSet;
		}
		
		/// <summary>
		/// With this functionality business logic can get the previous data block 
		/// (from current block backwards) - the current block is handled in QryResult class.
		/// </summary>
		/// <param name="qryResultID">Unique query result ID</param>
		/// <returns>
		/// Returns the DataSet object that contains the previous data block.
		/// If there is any block available null is returned.
		/// </returns>
		public DataSet GetDataSetPreviousBlock ()
		{
			DataSet copiedDataSet = null;

			if ( (currentUIBlockNumber - 1) <= maxAvailableBlocks )
			{
				// decrement UI block
				--currentUIBlockNumber;

				// set returnedDataSet and copy it.
				setCurrentUIReturnDataSet();
				copiedDataSet = returnDataSet.Copy();
			}

			return copiedDataSet;
		}

		/// <summary>
		/// With this functionality business logic can get the last data block of an query result set
		/// (positioning to block stored in variable maxAvailableBlocks)
		/// </summary>
		/// <param name="qryResultID">Unique query result ID</param>
		/// <returns>
		/// Returns the DataSet object that contains the last data block.
		/// If there processing is not completed yet, null is returned.
		/// </returns>
		public DataSet GetDataSetLastBlock ()
		{
			DataSet copiedDataSet = null;

			if ( QryResultStatus == DataAccessManager.QryStatus.FINISHED )
			{
				currentUIBlockNumber = maxAvailableBlocks;
				setCurrentUIReturnDataSet();
				copiedDataSet = returnDataSet.Copy();
			}

			return copiedDataSet;
		}

		/// <summary>
		/// Sets the content of the current UI block to the returned dataset.
		/// </summary>
		private void setCurrentUIReturnDataSet()
		{
			// get corresponding dataset.
			QryResultBlock resBlock = (QryResultBlock)qryResultBlocks[currentUIBlockNumber];

			// Verfiy the result.
			if ( null == resBlock )
			{
				throw new DataAccessException("ERROR_DATAACCESS_QUERYRESULT_BLOCK_NOT_FOUND");
			}

			// clear data and fill with the data from the XML file.
			returnDataSet.Clear();
			try
			{
				returnDataSet.ReadXml(resBlock.NameResultXmlFile);
			}
			catch
			{
			}
		}

		/// <summary>
		/// Setting (and if needed creating) a path for temporary XML output.
		/// path is: configured XML path\User ID\Session ID\Query Definition\Query Result
		/// </summary>
		public void SetPathNameForXMLTempData ()
		{
			StringBuilder tempPathName     = new StringBuilder();
			StringBuilder tempHttpPathName = new StringBuilder();
			
			//Directory part 1 - general path for XML files from configuration
			tempPathName.Append(DataAccessConfiguration.GetInstance().XmlTempPath);
			tempHttpPathName.Append(DataAccessConfiguration.GetInstance().XmlTempPathHttp);

			//check, if directory exists
			DirectoryInfo directoryInfo = new DirectoryInfo(tempPathName.ToString());

			// if not - create directory
			if (!directoryInfo.Exists)
			{
				try
				{
					directoryInfo.Create();
				}
				catch(Exception e)
				{
                    throw new DataAccessException("ERROR_DATAACCESS_CANNOT_CREATE_DIR \n" + tempPathName.ToString(), e);
				}
			}

			//Directory part 2 - User ID
			if ( this.userID != String.Empty && this.sessionID != String.Empty)
			{
				tempPathName.Append("\\");
				tempPathName.Append(this.userID);
				tempPathName.Append("\\"); // Fretz
				tempPathName.Append(this.sessionID); // Fretz

				tempHttpPathName.Append("\\");
				tempHttpPathName.Append(this.userID);
				tempHttpPathName.Append("\\"); // Fretz
				tempHttpPathName.Append(this.sessionID); // Fretz


				//check, if directory exists
				directoryInfo = new DirectoryInfo(tempPathName.ToString());

				// if not - create directory
				if (!directoryInfo.Exists)
				{
					try
					{
						directoryInfo.Create();
					}
					catch(Exception e)
					{
                        throw new DataAccessException("ERROR_DATAACCESS_CANNOT_CREATE_DIR \n" + tempPathName.ToString(), e);
					}
				}
			} // UserId not empty.

			// Store the xml data user path.
			xmlUserPath		= tempPathName.ToString();
			xmlUserPathHttp = tempHttpPathName.ToString();

			//check, if directory exists
			directoryInfo = new DirectoryInfo(tempPathName.ToString());

			// if not - create directory
			if (!directoryInfo.Exists)
			{
				try
				{
					directoryInfo.Create();
				}
				catch(Exception e)
				{
                    throw new DataAccessException("ERROR_DATAACCESS_CANNOT_CREATE_DIR \n" + tempPathName.ToString(), e);
				}
			}

			// Store the xml data session path.
			xmlSessionPath = tempPathName.ToString();

			if ( this.queryDefName != String.Empty )
			{
				//Directory part 4 - Query Definition
				tempPathName.Append("\\");
				tempPathName.Append(this.queryDefName);

				//check, if directory exists
				directoryInfo = new DirectoryInfo(tempPathName.ToString());

				// if not - create directory
				if (!directoryInfo.Exists)
				{
					try
					{
						directoryInfo.Create();
					}
					catch(Exception e)
					{
                        throw new DataAccessException("ERROR_DATAACCESS_CANNOT_CREATE_DIR \n" + tempPathName.ToString(), e);
					}
				}
			} // QueryDefName not empty.

			if ( this.qryResultID != String.Empty )
			{
				//Directory part 5 - qryResultID
				tempPathName.Append("\\");
				tempPathName.Append(this.qryResultID);

				//check, if directory exists
				directoryInfo = new DirectoryInfo(tempPathName.ToString());

				// if not - create directory
				if (!directoryInfo.Exists)
				{
					try
					{
						directoryInfo.Create();
					}
					catch(Exception e)
					{
                        throw new DataAccessException("ERROR_DATAACCESS_CANNOT_CREATE_DIR \n" + tempPathName.ToString(), e);
					}
				}
			} // QryResultId not empty.

			this.returnXMLFilePath = tempPathName.ToString();
		}

		/// <summary>
		/// Generates the file name for temporary XML output
		/// </summary>
		public void SetFileNameForXMLTempData ()
		{
			StringBuilder tempFileName = new StringBuilder();
			
			//File name is qryResultID and current block number
			tempFileName.Append(qryResultID);
			tempFileName.Append("_block_");

			this.returnXMLFileName = tempFileName.ToString();
		}

		/// <summary>
		/// Adds an result block to the internal hashtable. If the block to add
		/// already exists it is not added.
		/// </summary>
		/// <param name="qryResultBlock">Result block to add.</param>
		public void AddQueryBlock(QryResultBlock qryResultBlock)
		{
			// Ensure that entry doesn't already exist.
			if ( !qryResultBlocks.ContainsKey(qryResultBlock.BlockId) )
			{
				qryResultBlocks.Add(qryResultBlock.BlockId, qryResultBlock);
			}
		}

		/// <summary>
		/// Gets an ArrayList with the paths of the XML files.
		/// </summary>
		/// <returns>An ArrayList including the paths as strings.</returns>
		public ArrayList GetPathArray()
		{
			QryResultBlock block	= null;
			int numberBlocks		= qryResultBlocks.Count;
			ArrayList list			= new ArrayList(numberBlocks);

			// Loop by id, so the order stays the same.
			for ( int i=1; i<=numberBlocks; ++i )
			{
				if ( qryResultBlocks.ContainsKey(i) )
				{
					block = (QryResultBlock)qryResultBlocks[i];
					list.Add(block.NameResultXmlFile);
				}
			}
			
			return list;
		}

		#endregion // Methods
	}
}

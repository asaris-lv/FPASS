using System;
using System.Data;
using System.IO;
using System.Text;
using System.Threading;
using System.Diagnostics;

using de.pta.Component.Common;

namespace de.pta.Component.DataAccess.Internal
{
	/// <summary>
	/// Encapsultates the processing of an queryResult.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> M.Keller, PTA GmbH
	/// <b>Date:</b> 02.04.2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class XmlDataProcessor : WorkerThread
	{
		#region Members

		private QryResult	qryResult;
		private TextWriter	outputFile;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Constructs the object.
		/// </summary>
		/// <param name="qryResult">The query result to complete</param>
		public XmlDataProcessor(QryResult qryResult) : base()
		{
			this.qryResult	= qryResult;
			this.outputFile	= null;
		}

		#endregion //End of Constructors

		#region Initialization
		#endregion //End of Initialization

		#region Accessors
		#endregion //End of Accessors

		#region Methods

		/// <summary>
		/// Starts the thread.
		/// </summary>
		override public void Start()
		{
			try
			{
				process();
			}
			catch (ThreadAbortException e)
			{
				String s =  e.Message;
				closeOutputFile();
			}
			catch ( Exception e )
			{
				String s =  e.Message;
				closeOutputFile();
			}
		}

		private void process()
		{
			// Writes XML files for every result block.

			DataSet ds = null;
			QryResultBlock qryResultBlock = null;
			String destination = "";
			int blockNr = 0;

			// increment block number.
			++blockNr;

			// create the path.
			destination = getPathAndFileName(qryResult, blockNr);

			// create an fill query result block.
			qryResultBlock = new QryResultBlock();
			qryResultBlock.BlockId = blockNr;
			qryResultBlock.NameResultXmlFile = destination;

			// get DataSet and write it to file.
			ds = qryResult.ReturnDataSet;
			writeDataSetToXml(ds, destination);

			// Add query block to query results.
			qryResult.AddQueryBlock(qryResultBlock);
			qryResult.MaxAvailableBlocks = blockNr;

			// loop pending data
			while (!qryResult.LocalQuery.DataBaseType.ProcessingCompleted)
			{
				// increment block number.
				++blockNr;

				// create the path.
				destination = getPathAndFileName(qryResult, blockNr);

				// create an fill query result block.
				qryResultBlock = new QryResultBlock();
				qryResultBlock.BlockId = blockNr;
				qryResultBlock.NameResultXmlFile = destination;

				// get DataSet and write it to file.
				ds = qryResult.LocalQuery.DataBaseType.ProcessExecutableStatement();
				writeDataSetToXml(ds, destination);

				// Add query block to query results.
				qryResult.AddQueryBlock(qryResultBlock);
				qryResult.MaxAvailableBlocks = blockNr;

			}	// while

			// complete query result.
			qryResult.QryProcessingComplete = true;
			qryResult.XmlProcessingThread   = null;
			if ( qryResult.QryResultStatus != DataAccessManager.QryStatus.FINISHED_NODATA )
			{
				qryResult.QryResultStatus = DataAccessManager.QryStatus.FINISHED;
			}
			
		}

		private String getPathAndFileName(QryResult qryResult, int blockNr)
		{
			// Gets the complete path and file name, including the file extension.

			const String formatSpec = "00000";
			StringBuilder str = new StringBuilder();

			str.Append(qryResult.ReturnXMLFilePath);
			str.Append("\\");
			str.Append(qryResult.ReturnXMLFileName);
			str.Append(blockNr.ToString(formatSpec));
			str.Append(".xml");

			return str.ToString();
		}

		private void writeDataSetToXml(DataSet ds, String dest)
		{
			// Writes an DataSet to an file.

//	2003/Jun./12 Opierzynski, PTA - Begin - Change from 7 Bit ASCII to Unicode Output, because of
//  problem while diplay of german characters in excel

			// create a writer with the given name. Always create a new file.
//			outputFile = new StreamWriter(dest, false, System.Text.Encoding.ASCII, 1024);
			outputFile = new StreamWriter(dest, false, System.Text.Encoding.Unicode, 1024);

//	2003/Jun./12 Opierzynski, PTA - End

			// Get string to write out.
			String xmlOut = ds.GetXml();

			// Ensure, string is valid.
			if ( xmlOut != null )
			{
				// Write and flush file.
				outputFile.Write(xmlOut);
				outputFile.Flush();
			}

			// close the file.
			outputFile.Close();
		}

		private void closeOutputFile()
		{
			// Closes the output file if, it already exists.

			// Ensure object is created.
			if ( null != outputFile )
			{
				try
				{
					outputFile.Close();
				}
				catch ( Exception e )
				{
					// ignore this exceptions.
					// E.g. file is not yet open.
					Debug.WriteLine("File close: " + e.Message);
				}
			}
		}

		#endregion
	}
}

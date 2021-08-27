using System;
using System.Collections;
using System.Data;
using System.IO;
using de.pta.Component.DbAccess;
using de.pta.Component.Errorhandling;
using Degussa.FPASS.Db;
using Degussa.FPASS.Util.Messages;

namespace Degussa.FPASS.Reports.Util
{
	/// <summary>
	/// 
	/// </summary>
	/// <remarks>
	/// <para><b>History</b></para>
	/// <div class="tablediv">
	/// <table class="dtTABLE" cellspacing="0">
	///		<tr>
	///			<th width="20%">PTA GmbH</th>
	///			<th width="20%">12/01/2003</th>
	///			<th width="60%">Remarks</th>
	///		</tr>
	/// </table>
	/// </div>
	/// </remarks>
	public class FpassExport
	{
		#region Members
			
		// instance of export parameters
	    private FpassExportParameters mParameters;
		
	    // path of the csv file to generate
	    private string mFile = "";

	    // Number of rows written to CSV (as selected from DB)
        private int mRowsReturned;

		#endregion 

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public FpassExport(FpassExportParameters prmExportParameters)
		{
			Initialize(prmExportParameters);
		}

	    #endregion Constructors

		#region Initialization

		/// <summary>
		/// Initialisarion
		/// </summary>
		/// <param name="prmExportParameters"></param>
		private void Initialize(FpassExportParameters prmExportParameters)
		{	
			// Initialization
			mParameters = prmExportParameters;
			mFile = mParameters.FileName;
		}

		#endregion //End of Initialization
	
		#region Accessors 
	    
	    /// <summary>
	    /// Returns full path and name of CSV export file
	    /// </summary>
        public string ExportFile
        {
            get { return mFile; }
        }

	    /// <summary>
        /// Returns number of rows written to CSV (as selected from DB)
	    /// </summary>
	    public int RowsReturned
	    {
	        get { return mRowsReturned; }
	    }

	    #endregion Accessors

		#region Methods

		/// <summary>
		/// Initializes and starts the generation of the CSV File
		/// </summary>
		public void Generate()
		{
			string dateTime;
			string fileExtension;
			string fileCore;

			try
			{	
				// contructs extension of the csv file
				// datetime without spaces and :
				dateTime = DateTime.Now.ToString();
				dateTime = dateTime.Replace(" ", "_");
				dateTime = dateTime.Replace(":", "_");
				dateTime = dateTime.Replace(".", "_");

				fileExtension = "_" + dateTime + ".csv";
				
				// uses given file + path, completes suggested csv file name with extension
				fileCore = mParameters.FileName;
				mFile = fileCore + fileExtension;

				// loads data to export and generates csv file
				GenerateFile();
			}
			catch (Exception ex)
			{
				throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.EXPORT_ERROR), ex);
			}
		}


		/// <summary>
		/// Generates CSV file
		/// </summary>
		private void GenerateFile()
		{
			// Variables
			string row = "";
			
			// Creates a text file containing the export data
			StreamWriter csvWriter = File.CreateText(ExportFile);
			
			try
			{			
				// Writes all column names as the first row in the file
				// GetExportFields() returns a SortedDictionary that holds:
				// - the technical names of the columns to export as keys
				// - the logical names of the columns to export as values
				foreach (string columnName in mParameters.ExportFields.GetKeys())
				{
					row = row + "\"" + mParameters.ExportFields.GetValue(columnName) + "\",";
				}
				// remove the comma at the end of the row
				row = row.Substring(0, row.Length - 1);
				csvWriter.WriteLine(row);

				// gets export data from database and write them in the csv file
				LoadDataFromDb(csvWriter);
			}
			catch (Exception ex)
			{
				throw new UIWarningException(MessageSingleton.GetInstance().GetMessage(MessageSingleton.EXPORT_ERROR), ex);
			}
			finally
			{		
				csvWriter.Close();
			}
		}

		/// <summary>
		/// Loads export data from database and creates rows for CSV
		/// </summary>
		/// <param name="prmCsvWriter"></param>
		private void LoadDataFromDb(StreamWriter prmCsvWriter)
		{
			// Variables
			int columnCount;
			int columnNo;
			string columnName = "";
			string columnValue;
			string columnValueForeignKey = "";

			int subColumnCount;
			int subColumnNo;
			string subColumnName;
			string subColumnValue;

			string subSqlCommandId = mParameters.SubSqlCommandId;
			string subWhereClauseMainField = mParameters.SubWhereClauseMainField;
			string subWhereClauseSubField = mParameters.SubWhereClauseSubField;
			
			// a data record as a text line
            string row = ""; 

            // holds all columns to export (template)
			ArrayList columnsToExport = mParameters.ExportFields.GetKeys(); 

            // holds all columns to export and their values for one row
			SortedList rowToExport = new SortedList();

            // holds all rows to export
			ArrayList dataToExport = new ArrayList(); 

			try
			{
				// Creates the select command and adds the clauses
				IProvider exportDataProvider = DBSingleton.GetInstance().DataProvider;
				IDbCommand selectCommand = exportDataProvider.CreateCommand(mParameters.MainSqlCommandId);
				
				selectCommand.CommandText += " " + mParameters.MainWhereClause;
                selectCommand.CommandText += mParameters.MainGroupBy;
                selectCommand.CommandText += " " + mParameters.MainOrderBy;

				IDataReader exportDataReader = exportDataProvider.GetReader(selectCommand);

				// gets export data from the db
                // ----------------------------

				// gets number of columns as result of the select command
				columnCount = exportDataReader.FieldCount;

				while (exportDataReader.Read())
				{	
					// inserts a new data record in the csv file (a new row)
					// gets column values for all columns to export
					//---------------------------------------------
					for (columnNo = 0; columnNo < columnCount; columnNo++)
					{
						
						columnName = exportDataReader.GetName(columnNo);
                        if (null != exportDataReader.GetValue(columnNo))
                        {
                            columnValue = CorrectString(exportDataReader.GetValue(columnNo).ToString());
                        }
                        else
                            columnValue = ""; // no value in the database (null)
	
						// is the current column a foreign key for a sub block?
						if (columnName.Equals(subWhereClauseMainField))
						{
							columnValueForeignKey = columnValue; // yes: save value of the foreign key for later
						}

						// sets column value as element of the row / line to write in the csv file
						// only if the column is in the list of columns to be exported
						foreach (string columnNameToExport in columnsToExport)
						{
							if (columnName.Equals(columnNameToExport))
							{
								rowToExport.Add(columnNameToExport, columnValue);
							}
						 }
					}
				
					// is there complementary data in a sub block?
					// -------------------------------------------
					if (!(subSqlCommandId.Equals("")))
					{
						// Creates the select command
						IDbCommand subSelectCommand = exportDataProvider.CreateCommand(subSqlCommandId);
		
						// adds the where clause to the select command
						subSelectCommand.CommandText = subSelectCommand.CommandText + " " + subWhereClauseSubField + " " + columnValueForeignKey;

						// Opens data reader to get data from database with the select command which has a where clause now
						IDataReader subExportDataReader = exportDataProvider.GetReader(subSelectCommand);

						// gets export data corresponding to the sub block from the db

						// number of columns as result of the sub select command
						subColumnCount = subExportDataReader.FieldCount;

						while (subExportDataReader.Read())
						{
							// insert values of the sub block in the csv file
							for (subColumnNo = 0; subColumnNo < subColumnCount; subColumnNo++)
							{
								// gets column name & value from db
								subColumnName = subExportDataReader.GetName(subColumnNo);
								if (null != subExportDataReader.GetValue(subColumnNo))
									subColumnValue = this.CorrectString(subExportDataReader.GetValue(subColumnNo).ToString());
								else
									subColumnValue = "";

								// sets column name & value as element of the row / line in the csv file
								// only if the column name is in the list of the columns to be exported
								// and if the column hasn't been already added in the row
								foreach (string columnNameToExport in columnsToExport)
								{
									if ( (subColumnName.Equals(columnNameToExport)) && ( !(rowToExport.ContainsKey(columnNameToExport)) ) )
									{
										rowToExport.Add(columnNameToExport, subColumnValue);
										//row = row + "\"" + subColumnValue + "\",";
									}
								}										
							}

							// saves the row to export in the list of rows
							dataToExport.Add(rowToExport.Clone());
							// clear row for the next row
							rowToExport.Clear();
						}
					}
					else
					{						
						// saves the row to export in the list of rows
						dataToExport.Add(rowToExport.Clone());
						// clear row for the next row
						rowToExport.Clear();
					}
				}

				// Close the readers
				exportDataReader.Close();

				// Exceptions: Special handlings for some exports
				//-----------------------------------------------

				// Report 1 "Bestandvergleichsliste"
				// columns values must be determined:
				// - "ONLY_FPASS" (CoWorker is only in FPASS, Status = "FPASS"),
				// - "ONLY_ZKS" (CoWorker is only in ZKS, Status = "ZKS"),
				// - "DIFFERENT_NO" (IDCardNos in FPASS and ZKS are different)
				// IDCardNo(FPASS)	IDCardNo (ZKS)	DIFFERENT_NO
				//		null			null			N
				//		x				x				N
				//		x				null			N
				//		null			y				N
				//		x				y				Y	
				if (0 <= mParameters.FileName.IndexOf(ReportFilenames.CHECKLIST))
				{
					foreach (SortedList rowToUpdate in dataToExport)
					{
						string fpassNo = rowToUpdate["CHLS_IDCARDNOFPASS"].ToString();
						string zksNo = rowToUpdate["CHLS_IDCARDNOZKS"].ToString();
						string status = rowToUpdate["CHLS_STATUS"].ToString();

						if ( !(fpassNo.Equals("")) && !(zksNo.Equals("")) && !(fpassNo.Equals(zksNo)) )
						{
							rowToUpdate["DIFFERENT_NO"] = "Y";
						}
						else
						{
							rowToUpdate["DIFFERENT_NO"] = "N";
						}
						if (status.Equals("FPASS"))
						{
							rowToUpdate["ONLY_FPASS"] = "Y";
							rowToUpdate["ONLY_ZKS"] = "N";
						}
						else if (status.Equals("ZKS"))
						{
							rowToUpdate["ONLY_FPASS"] = "N";
							rowToUpdate["ONLY_ZKS"] = "Y";
						}
						else
						{
							rowToUpdate["ONLY_FPASS"] = "N";
							rowToUpdate["ONLY_ZKS"] = "N";
						}

						rowToUpdate.Remove("CHLS_STATUS"); // this field must not be exported
						
						if (rowToUpdate["CHLS_AUTHORISED_YN"].ToString().Equals(""))
							rowToUpdate["CHLS_AUTHORISED_YN"] = "N"; // no data on authorization corresponds to no authorization
					}
				}
				
				// Report 6 "Bewegungsdaten pro Monat (Summe)"
				// one column value must be calculated:
				// - "SUMDAYS" (Sum of SUMDAYSONECWR for all Coworkers of an external contractor)
				if (0 <= mParameters.FileName.IndexOf(ReportFilenames.EXCO_BOOKINGS_SUM))
				{
					SortedList contractors = new SortedList(); // List of all contractors		
					string contractor = "";
					int sumdays = 0;

					// calculates the sum of days for each contractor
					foreach (SortedList theRow in dataToExport)
					{
						if ( theRow.ContainsKey("EXCO_NAME")) // not all rows have an EXCO_NAME field
						{
							// a new contractor and a new sum to calculate
							sumdays = 0;
							contractor = theRow["EXCO_NAME"].ToString();
							// the sum for the actual contractor has been calculated and must be saved
							contractors.Add(contractor, sumdays);
						}
						// else the contractor is the same as for the row before
						if ( !(theRow["SUMDAYSONECWR"].ToString().Equals("")) )
						{
							sumdays = sumdays + Convert.ToInt32(theRow["SUMDAYSONECWR"]);
							contractors[contractor] = sumdays;
						}
					}
					// updates all rows where the contractor is with the calculated sum
					foreach (SortedList aRow in dataToExport)
					{
						if (aRow.ContainsKey("EXCO_NAME")) // not all rows have an EXCO_NAME field
						{
							contractor = aRow["EXCO_NAME"].ToString();
							aRow["SUMDAYS"] = contractors[contractor].ToString();
						}
					}
				}

				// Report 7: Fremdfirmenmitarbeiter nach Ablaufdatum
				// Report 8: Löschliste alte Fremdfirmenmitarbeiter
				//-------------------------------------------------
				if ( (0 <= mParameters.FileName.IndexOf(ReportFilenames.CWR_EXPIRYDATE))
					|| (0 <= mParameters.FileName.IndexOf(ReportFilenames.CWR_DELETELIST)) )
				{
					foreach (SortedList rowToUpdate in dataToExport)
					{	
						if (rowToUpdate["RATH_RECEPTAUTHO_YN"].ToString().Equals(""))
							rowToUpdate["RATH_RECEPTAUTHO_YN"] = "N"; // no data on authorization corresponds to no authorization
					}
				}			

				// writes all rows (main data and existing secondary data) in the csv file
				//------------------------------------------------------------------------
				foreach (SortedList rowToWrite in dataToExport)
				{
					// builds a string with the values of all the columns of the row to export
					foreach (string columnNameToExport in columnsToExport)
					{
						if (rowToWrite.ContainsKey(columnNameToExport))
						{
							if (0 <= mParameters.FileName.IndexOf("Anwesenheitszeit"))
							{
								// Don't cut off time part of datetime!
								row = row + "\"" + rowToWrite[columnNameToExport].ToString() + "\",";
							}
							else
							{
								row = row + "\"" + ExtractDate(rowToWrite[columnNameToExport].ToString()) + "\",";
							}
						}
						else
							row = row + "\"\","; // if the column has not been inserted in the row because of an unknown reason
					}
					// removes the last comma at the end of the row
					row = row.Substring(0, row.Length - 1);
	
					prmCsvWriter.WriteLine(row);
					row = "";
				}

                mRowsReturned = dataToExport.Count;
			}
			catch (Exception ex)
			{
				throw new Exception("", ex);
			}
		}

		/// <summary>
		/// Extracts and returns the Date part of a datetime value
		/// </summary>
		/// <param name="prmString"></param>
		/// <returns></returns>
		private string ExtractDate(string prmString)
		{
			string date = prmString;
			
			// extract the date from a datetime string
			// if prmString is not a DateTime or a date, no extraction occurs
			// prmDateTime = STRING = "TT.MM.JJJJ HH:MM:SS", first character has position 0 in the string!
			// such a datetime or a date contains more than 9 characters and a dot at position 2 and position 5
			if (10 <= date.Length)
			{
				if ( (date.Substring(2,1).Equals(".")) && (date.Substring(5,1).Equals(".")) )
					date = date.Substring(0, 10); // "TT.MM.JJJJ" = 10 characters from 1. character = position = 0
			}
			return date;
		}

		/// <summary>
		/// Corrects a string value
		/// Name = "surname, firstname" comes as a result of some sql select for a person
		/// if name and firstname have no values, then the result must be "", not ", "
		/// if only the firstname has no value, then the comma must be removed
		/// </summary>
		/// <param name="prmText"></param>
		/// <returns></returns>
		private string CorrectString(string prmText)
		{			
			string text = prmText.Trim();

			if (text.Equals(","))
				text = "";
			else if (text.EndsWith(","))
				text = text.Substring(0, text.Length - 1); // text without "," at the end
			else
				text = prmText;

			// special character will be transform into simple ones for the csv format
			text = text.Replace("ä", "ae");
			text = text.Replace("ö", "oe");
			text = text.Replace("ü", "ue");
			text = text.Replace("Ä", "AE");
			text = text.Replace("Ö", "OE");
			text = text.Replace("Ü", "UE");
			text = text.Replace("ß", "ss");
			text = text.Replace("é", "e");
			text = text.Replace("è", "e");
			text = text.Replace("à", "a");
			text = text.Replace("ê", "e");
			text = text.Replace("û", "u");
			text = text.Replace("â", "a");

			return text;
		}

		#endregion Methods
	}
}

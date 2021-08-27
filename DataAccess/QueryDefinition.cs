using System;
using System.Data;
using System.Collections;
using System.Text;

namespace de.pta.Component.DataAccess.Internal
{
	/// <summary>
	/// Summary description for Query. These class is building a template for a
	/// typed dataset with strongly typed data. This dataset will be "cloned" in
	/// a specific query result to store the returned data.
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> M.Keller, PTA GmbH
	/// <b>Date:</b> Apr/02/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class QueryDefinition : ICloneable
	{
		#region Members

		public const String DATASET_TEMPLATE_NAME	= "DS";
		public const String TB_NAME_DATATABLE		= "TB";
		public const String TB_NAME_STATUSTABLE		= "TS";
		public const String TB_NAME_COLUMNS			= "TC";
		public const String ATTRIBUTE_ID			= "ATTRIBUTE_ID";
		public const String COLUMNWIDTHPX			= "COLUMNWIDTHPX";
		public const String DATATYPE				= "DATATYPE";
		public const String PAGECOUNTER				= "PG";
		public const String MAXPAGEINDATASET		= "maxPageInDataSet";
		public const String MAXRECORDSINDATASET		= "maxRecordInDataSet";
		public const String PROCESSINGCOMPLETED		= "processingCompleted";
		public const String MAXRECORDSEXCEEDED		= "maxRecordsExceeded";
		public const String TOTALRECORDSREAD		= "totalRecordsRead";
		public const String APPLICATIONMAXRECORDS	= "applicationMaxRecords";

		public const String TYPE_TXT				= "TXT";
		public const String TYPE_NUM				= "NUM";
		public const String TYPE_CURL				= "CURL";
		public const String TYPE_CURR				= "CURR";
		public const String TYPE_PCT				= "PCT";
		public const String TYPE_DAT				= "DAT";
		public const String TYPE_IN					= "IN";
		public const String TYPE_LIKE				= "LIKE";
		public const String SQL_LIKE				= "%";

		public const String WHERE_CLAUSE			= "WHERE-CLAUSE";
		public const String BY_CLAUSE				= "BY-CLAUSE";
		public const String SCALAR					= "SCALAR";
		

		private String queryID;	
		private String queryName;	
		private String databaseTypeInfo;
		private String selectStatement;
		private int differentDataPageSizeUsed;
		private DataSet typedDataSetTemplate;

		private ArrayList attributes;
		private ArrayList attributeLists;
		private ArrayList parameters;
		private ArrayList parameterLists;

		#endregion //End of Members

		#region  Constructors

		/// <summary>
		/// Initialization of members. 
		/// </summary>
		public QueryDefinition()
		{
			initialize();
		}

		#endregion //End of Constructors

		#region Initialization

		private void initialize()
		{
			// Initialization on constructor. 
			queryID						= "";
			queryName					= "";
			databaseTypeInfo			= "";
			selectStatement				= "";
			differentDataPageSizeUsed	= 0;
			typedDataSetTemplate		= null;
			attributes					= new ArrayList();
			parameters					= new ArrayList();
			parameterLists				= new ArrayList();
			attributeLists				= new ArrayList();
		}

		#endregion //End of Initialization

		#region Accessors

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
		/// Accessor for queryName.
		/// </summary>
		public String QueryName
		{
			get
			{
				return queryName;
			}
			set
			{
				queryName = value;
			}
		}

		/// <summary>
		/// Accessor for databaseTypeInfo.
		/// </summary>
		public String DatabaseTypeInfo
		{
			get
			{
				return databaseTypeInfo;
			}
			set
			{
				databaseTypeInfo = value;
			}
		}
		
		/// <summary>
		/// Accessor for selectStatement.
		/// </summary>
		public String SelectStatement
		{
			get
			{
				return selectStatement;
			}
			set
			{
				selectStatement = value;
			}
		}
		
		/// <summary>
		/// Accessor for typedDataSetTemplate.
		/// </summary>
		public DataSet TypedDataSetTemplate
		{
			get
			{
				return typedDataSetTemplate;
			}
			set
			{
				typedDataSetTemplate = value;
			}
		}

		/// <summary>
		/// Accessor for differentDataPageSizeUsed.
		/// </summary>
		public int DifferentDataPageSizeUsed
		{
			get
			{
				return differentDataPageSizeUsed;
			}
			set
			{
				differentDataPageSizeUsed = value;
			}
		}

		/// <summary>
		/// Gets the list of parameter lists.
		/// </summary>
		public ArrayList AttributeLists
		{
			get
			{
				return attributeLists;
			}
		}

		/// <summary>
		/// Gets the list of parameters.
		/// </summary>
		public ArrayList Parameters
		{
			get
			{
				return parameters;
			}
		}

		/// <summary>
		///  Gets the list of parameter lists.
		/// </summary>
		public ArrayList ParameterLists
		{
			get 
			{
				return parameterLists;
			}
		}
		#endregion //End of Accessors

		#region Methods

		/// <summary>
		/// Makes an deep copy of the object.
		/// </summary>
		/// <returns>An reference to the clone of the object.</returns>
		public Object Clone()
		{
			QueryDefinition clone = new QueryDefinition();

			// copy members
			clone.queryID					= queryID;
			clone.queryName					= queryName;
			clone.databaseTypeInfo			= databaseTypeInfo;
			clone.selectStatement			= selectStatement;
			clone.differentDataPageSizeUsed	= differentDataPageSizeUsed;
			clone.typedDataSetTemplate		= typedDataSetTemplate.Copy();

			// Clone attributes
			foreach (QryAttribute attr in attributes)
			{
				clone.attributes.Add(attr.Clone());
			}

			// Clone parameters
			foreach (QryParameter para in parameters)
			{
				clone.parameters.Add(para.Clone());
			}

			// Clone parameterLists
			foreach (QryParameterList paraList in parameterLists)
			{
				clone.parameterLists.Add(paraList.Clone());
			}

			// Clone attributeLists
			foreach ( QryAttributeList attList in attributeLists )
			{
				clone.attributeLists.Add(attList.Clone());
			}

			// return the clone.
			return clone;
		}

		/// <summary>
		/// Generates a template (typed DataSet) from that all DataSets can inherits
		/// </summary>
		public void MakeTypedDataSetTemplate()
		{
			QryAttribute attribute = null;
			DataTable dt = null;
			typedDataSetTemplate = new DataSet(DATASET_TEMPLATE_NAME);


			// Add the table for the column information
			dt = typedDataSetTemplate.Tables.Add(TB_NAME_COLUMNS);

			dt.Columns.Add(ATTRIBUTE_ID);
			dt.Columns.Add(COLUMNWIDTHPX);
			dt.Columns.Add(DATATYPE);

			// Fill the table with column information.
			fillColumnDefinitions();


			//new table in DataSet object
			dt = typedDataSetTemplate.Tables.Add(TB_NAME_DATATABLE);

			//Adds Page counter
			dt.Columns.Add(PAGECOUNTER);

			// building the structure of the virtual table from attribut list
			// iterateing the list.
			IEnumerator enumerator = attributes.GetEnumerator();
			while ( enumerator.MoveNext() )
			{
				// get the attribute
				attribute = (QryAttribute)enumerator.Current;

				//debug information, may be removed later
				//11.04.03 Max auskommentiert
				//dt.Columns.Add(attribute.ColName);

				//11.04.03 Max ersetzt
				//for localization, define datatype of column 
				String colDataType = (String) attribute.DataType;

				if (colDataType.ToUpper().Equals(TYPE_TXT))
				{
					dt.Columns.Add(attribute.Id, typeof(String));
				}
				else if (colDataType.ToUpper().Equals(TYPE_NUM))
				{
					dt.Columns.Add(attribute.Id, typeof(double));
				}
				else if (colDataType.ToUpper().Equals(TYPE_DAT))
				{
					dt.Columns.Add(attribute.Id, typeof(DateTime));
				}
				else if (colDataType.ToUpper().Equals(TYPE_CURL))
				{
					dt.Columns.Add(attribute.Id, typeof(double));
				}
				else if (colDataType.ToUpper().Equals(TYPE_CURR))
				{
					dt.Columns.Add(attribute.Id, typeof(double));
				}
				else if (colDataType.ToUpper().Equals(TYPE_PCT))
				{
					dt.Columns.Add(attribute.Id, typeof(double));
				}
				else
				{
					// Invalid attribute specified.
					throw new DataAccessException("ERROR_DATAACCESS_INVALID_ATTRIBUTE_DEFINITION");
				}
			}

			// Add the table for status informations
			dt = typedDataSetTemplate.Tables.Add(TB_NAME_STATUSTABLE);

			dt.Columns.Add(MAXPAGEINDATASET);
			dt.Columns.Add(MAXRECORDSINDATASET);
			dt.Columns.Add(PROCESSINGCOMPLETED);
			dt.Columns.Add(MAXRECORDSEXCEEDED);
			dt.Columns.Add(TOTALRECORDSREAD);	
			dt.Columns.Add(APPLICATIONMAXRECORDS);

		}

		private void fillColumnDefinitions()
		{
			// Fills the table with the column information
			// from the attributes.

			DataRow row = null;
			DataTable colDef = typedDataSetTemplate.Tables[TB_NAME_COLUMNS];

			// Loop all query attributes.
			foreach ( QryAttribute qryAttribute in attributes )
			{
				// create row
				row = colDef.NewRow();

				// fill row
				row[ATTRIBUTE_ID]	= qryAttribute.Id;
				row[COLUMNWIDTHPX]	= qryAttribute.ColWidthPx;
				row[DATATYPE]		= qryAttribute.DataType;

				// add row.
				colDef.Rows.Add(row);
			}

		}

		/// <summary>
		/// Creates a new attribute list object (QryAttributeList), fills the
		/// object and adds it to the private attribute list collection.
		/// </summary>
		/// <param name="id">Id of the new attribute list.</param>
		public void AddAttributeLists(String id)
		{
			// create
			QryAttributeList attributeList = new QryAttributeList();

			// fill
			attributeList.Id = id;

			// add
			attributeLists.Add(attributeList);
		}

		/// <summary>
		/// Creates a new attribute object (QryAttribute), fills the object 
		/// and adds it to the private attribute list.
		/// </summary>
		/// <param name="id">Id of the new attribute.</param>
		/// <param name="colName">column name of the new attribute.</param>
		/// <param name="colWidthPx">Width of the column in Pixel.</param>
		/// <param name="dataType">data type of the new attribute.</param>
		/// <param name="attribList">attribute list of the new attribute.</param>
		/// <param name="dimensionAttribute">The dimensionAttribute.</param>
		public void AddAttribute(String id, String colName, int colWidthPx, 
								 String dataType, String attribList, bool dimensionAttribute)
		{
			// create 
			QryAttribute attribute = new QryAttribute();

			// fill
			attribute.Id					= id;
			attribute.ColName				= colName;
			attribute.ColWidthPx			= colWidthPx;
			attribute.DataType				= dataType;
			attribute.AttribList			= attribList;
			attribute.DimensionAttribute	= dimensionAttribute;

			// add
			attributes.Add(attribute);
		}

		/// <summary>
		/// Creates a new parameter list object (QryParameterList), fills the object
		/// and adds it to the private parameter list collection.
		/// </summary>
		/// <param name="id">Id.</param>
		/// <param name="type">Type</param>
		/// <param name="code">Code</param>
		public void AddParameterList(String id, String type, String code)
		{
			// create
			QryParameterList parameterList = new QryParameterList();

			// fill
			parameterList.Id	= id;
			parameterList.Type	= type;
			parameterList.Code	= code;

			// set
			parameterLists.Add(parameterList);
		}

		/// <summary>
		/// Creates a new parameter object (QryParameter), fills the object
		/// and adds it to the private parameter list.
		/// </summary>
		/// <param name="id">Id of the new parameter.</param>
		/// <param name="colName">Column name of the parameter.</param>
		/// <param name="dataType">Data type of the new parameter.</param>
		/// <param name="paraType">Type of the new parameter.</param>
		/// <param name="sqlOperator">The SQL operator.</param>
		public void AddParameter(String id, String colName, String dataType, String paraList, String sqlOperator)
		{
			// create
			QryParameter parameter = new QryParameter();

			// fill
			parameter.Id			= id;
			parameter.ColName		= colName;
			parameter.DataType		= dataType;
			parameter.ParaList		= paraList;
			parameter.SqlOperator	= sqlOperator;

			// add
			parameters.Add(parameter);
		}

		/// <summary>
		/// Formats the given value, dependend from its data type.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="paramterValue"></param>
		/// <returns></returns>
		public String FormatParameter(String id, String parameterValue, String operation)
		{
			QryParameter parameter = null;
			StringBuilder str	   = new StringBuilder();

			parameter = GetQryParameterForId(id);
			if ( null == parameter )
			{
				return String.Empty;
			}

			// if no operator is given use default.
			if ( operation.Equals(String.Empty) )
			{
				operation = parameter.SqlOperator;
			}

			// If we have like append an sql like.
			if ( operation.ToUpper().Equals(TYPE_LIKE) )
			{
				// e.g. "hal" becomes "hal%"
				if ( parameterValue.EndsWith("'") )
				{
					parameterValue = parameterValue.Substring(0, parameterValue.Length-1);
					parameterValue += SQL_LIKE + "'";
				}
				else
				{
					parameterValue += SQL_LIKE;
				}
			}

			if ( operation.ToUpper().Equals(TYPE_IN) )
			{
				str.Append(parameter.ColName);
				str.Append(" IN (");
				str.Append(parameterValue);
				str.Append(")");
			}
			else
			{
				str.Append(parameter.ColName);
				str.Append(" ");
				str.Append(operation);
				str.Append(" ");
				str.Append(parameterValue);
			}

			return str.ToString();
		}

		private QryParameter GetQryParameterForId(String id)
		{
			// Returns a query parameter to the given id, if
			// no parameter ist found null is returned.

			QryParameter parameter = null;
			IEnumerator enumerator = parameters.GetEnumerator();

			while ( enumerator.MoveNext() )
			{
				parameter = (QryParameter)enumerator.Current;
				if ( parameter.Id.Equals(id) )
				{
					return parameter;
				}
			}

			return parameter;
		}

		/// <summary>
		/// Gets the column name of a parameter to the given id.
		/// </summary>
		/// <param name="id">Id of the parameter.</param>
		/// <returns>A string that contains the id if found, otherwise an empty string.</returns>
		public String GetColumnNameForId(String id)
		{
			// Get an object reference the the requested attribute.
			QryAttribute attribute = GetQryAttributeForId(id);
			
			// Ensure, the attribute is valid.
			if  ( null != attribute )
			{
				return attribute.ColName;
			}

			return String.Empty;
		}

		private QryAttribute GetQryAttributeForId(String id)
		{
			// Returns a query attribute to the given id, if
			// no attribute ist found null is returned.

			QryAttribute attribute = null;
			IEnumerator enumerator = attributes.GetEnumerator();

			while ( enumerator.MoveNext() )
			{
				attribute = (QryAttribute)enumerator.Current;
				if ( attribute.Id.Equals(id) )
				{
					return attribute;
				}
			}

			return attribute;
		}


		/// <summary>
		/// Provides a string with all attributes.
		/// </summary>
		/// <param name="attributeListId">Id of the attribute list.</param>
		/// <returns>A string, containing all attributes seperated by commas.</returns>
		/// <example>
		/// The returned string may have the following form 'column1, column2, column3'.
		/// </example>
		public String GetAttributeList(String attributeListId)
		{
			StringBuilder str		= new StringBuilder();
			QryAttribute attribute	= null;
			bool insertComma		= false;

			// Iterate the list.
			IEnumerator enumerator = attributes.GetEnumerator();
			while ( enumerator.MoveNext() )
			{
				// get the attribute
				attribute = (QryAttribute)enumerator.Current;

				// Must be assigned to an attribute list.
				if ( !attribute.AttribList.Equals(String.Empty) )
				{
					// Only attribute, that are no dimensions attibutes.
					if ( !attribute.DimensionAttribute )
					{
						// Append only the attributes for the given list.
						if ( attribute.AttribList.Equals(attributeListId) )
						{
							// Before the first column, no comma will be inserted.
							if ( insertComma )
							{
								str.Append(", ");
							}
							else
							{
								insertComma = true;
							}

							// Append attribute.
							str.Append(attribute.ColName);
						}
					}
				}
			}

			return str.ToString();
		}

		/// <summary>
		/// Returns the number of attributes in the attribute collection.
		/// </summary>
		/// <returns>The number of attributes as an integer value.</returns>
		public int GetNumberOfAttributes()
		{
			return attributes.Count;
		}

		#endregion // Methods.
	}
}

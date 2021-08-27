using System;
using System.Data;
using System.Reflection;
using System.Collections;

using de.pta.Component.N_UserManagement.Internal;

namespace de.pta.Component.N_UserManagement.DataAccess
{
	/// <summary>
	/// Provides services for mapping from records from database selects into objects
	/// </summary>
	/// <remarks>
	/// <pre>
	/// <b>History</b>
	/// <b>Author:</b> R.Weiﬂ, PTA GmbH
	/// <b>Date:</b> Aug/21/2003
	///	<b>Remarks:</b> None
	/// </pre>
	/// </remarks>
	internal class RelationalObjectMapper
	{
		#region Members

		/// <summary>
		/// Contains one single instance of RelationalObjectMapper.
		/// </summary>
		static private RelationalObjectMapper mRelationalObjectMapper;

		#endregion //End of Members

		#region Constructors

		private RelationalObjectMapper()
		{
			initialize();
		}

		#endregion //End of Constructors

		#region Initialization

		private void initialize()
		{
		}	
		#endregion //End of Initialization

		#region Accessors 

		/// <summary>
		/// Returns the instance of the RelationalObjectMapper
		/// </summary>
		/// <returns>RelationalObjectMapper</returns>
		static public RelationalObjectMapper getInstance()
		{
			if (mRelationalObjectMapper == null)
			{
				mRelationalObjectMapper = new RelationalObjectMapper();
			}
			return mRelationalObjectMapper;
		}

		#endregion //End of Accessors

		#region Methods 

		/// <summary>
		/// Transformation in an array list containing the objects for each row read from database.
		/// </summary>
		/// <param name="pObjectMaps">Mapping between class attributes and record attributes</param>
		/// <param name="pDataSet">Dataset containing the result</param>
		/// <returns>ArrayList containg the result set transformed into a rows of objects.</returns>
		public ArrayList mapDataSetToArrayList(ArrayList pObjectMaps, DataSet pDataSet)
		{
			return this.mapDataSetToArrayList(pObjectMaps, pDataSet, null);
		}

		/// <summary>
		/// Transformation in an array list containing the objects for each row read from database.
		/// </summary>
		/// <param name="pObjectMaps">Mapping between class attributes and record attributes</param>
		/// <param name="pDataSet">Dataset containing the result</param>
		/// <param name="pAbstractClassMap">Mapping of abstract class to subclasses</param>
		/// <returns>ArrayList containg the result set transformed into a rows of objects.</returns>
		public ArrayList mapDataSetToArrayList(ArrayList pObjectMaps, DataSet pDataSet, AbstractClassMap pAbstractClassMap) 
		{
			ArrayList rows;

			rows = new ArrayList();

			foreach(DataRow currentRow in pDataSet.Tables["TB"].Rows)
			{
				rows.Add(this.mapRowToObjects(pObjectMaps, pDataSet, currentRow, pAbstractClassMap));
			}

			return rows;
		}
		
		/// <summary>
		/// Transformation of a result row from database into its corresponding objects. The resulting
		/// objects are added to an arraylist.
		/// </summary>
		/// <param name="pObjectMaps">Mapping between class attributes and record attributes</param>
		/// <param name="pDataSet">Dataset containing the result</param>
		/// <param name="pCurrentRow">Current Row of dataset</param>
		/// <returns>Arraylist containing the result of one row as its corresponding objects.</returns>
		public ArrayList mapRowToObjects(ArrayList pObjectMaps, DataSet pDataSet, DataRow pCurrentRow)
		{
			return this.mapRowToObjects(pObjectMaps, pDataSet, pCurrentRow, null);
		}

		/// <summary>
		/// Transformation of a result row from database into its corresponding objects. The resulting
		/// objects are added to an arraylist.
		/// </summary>
		/// <param name="pObjectMaps">Mapping between class attributes and record attributes</param>
		/// <param name="pDataSet">Dataset containing the result</param>
		/// <param name="pCurrentRow">Current Row of dataset</param>
		/// <param name="pAbstractClassMap">Mapping of abstract class to subclasses</param>
		/// <returns>Arraylist containing the result of one row as its corresponding objects.</returns>
		public ArrayList mapRowToObjects(ArrayList pObjectMaps, DataSet pDataSet, DataRow pCurrentRow, AbstractClassMap pAbstractClassMap)
		{
			ArrayList	rowOfObjects = new ArrayList();

			foreach(ObjectMap objectMap in pObjectMaps) 
			{
				rowOfObjects.Add(this.mapRowToObject(objectMap.Type, pDataSet, pCurrentRow, objectMap.TableShortName, pAbstractClassMap));
			}

			return rowOfObjects;
		}


		/// <summary>
		/// Returns an object of the type given as parameter from a row in a dataset.
		/// </summary>
		/// <param name="pType">Type of target object</param>
		/// <param name="pDataSet">Dataset containing Table, Column and Row information</param>
		/// <param name="pCurrentRow">Row containing the values from the database</param>
		/// <param name="pTableShortName">
		/// Shortname of the table. The shortname is cut from the column names to map columns to class attributes.
		/// Might be <code>null</code> or <code>""</code> if no column shortname exists.
		/// </param>
		/// <returns>Transfomed database object</returns>
		public Object mapRowToObject(Type pType, DataSet pDataSet, DataRow pCurrentRow, String pTableShortName)
		{
			return this.mapRowToObject(pType, pDataSet, pCurrentRow, pTableShortName, null);
		}

		/// <summary>
		/// Returns an object of the type given as parameter from a row in a dataset.
		/// </summary>
		/// <param name="pType">Type of target object</param>
		/// <param name="pDataSet">Dataset containing Table, Column and Row information</param>
		/// <param name="pCurrentRow">Row containing the values from the database</param>
		/// <param name="pTableShortName">
		/// Shortname of the table. The shortname is cut from the column names to map columns to class attributes.
		/// Might be <code>null</code> or <code>""</code> if no column shortname exists.
		/// </param>
		/// <param name="pAbstractClassMap">Mapping of abstract class to subclasses</param>
		/// <returns>Transfomed database object</returns>
		public Object mapRowToObject(Type pType, DataSet pDataSet, DataRow pCurrentRow, String pTableShortName, AbstractClassMap pAbstractClassMap)
		{
			Object			result;
			PropertyInfo[]	typeProperties;
			Hashtable		rowValues;
			Object			columnValue;
			String			tableShortName = "";

			/* Append "_" to table shortname if it doesn't exist
			 */
			if (pTableShortName != null &&
				pTableShortName != "")
			{
				if (pTableShortName.EndsWith("_")) 
				{
					tableShortName = pTableShortName;
				} 
				else 
				{
					tableShortName = pTableShortName + "_";
				}
			}

			/* Transform database record into hashtable
			 */
			rowValues = new Hashtable();

			foreach(DataColumn currentColumn in pDataSet.Tables["TB"].Columns) 
			{
				rowValues.Add(currentColumn.ColumnName.ToUpper(), pCurrentRow[currentColumn]);
			}

			/* Check if type is abstract
			 */
			if (pType.IsAbstract) 
			{
				String	fieldName = pAbstractClassMap.TypeFieldName;
				String	typeId = Convert.ToString(rowValues[fieldName.ToUpper()]);
				String	typeName = (String)pAbstractClassMap.AbstractMap[typeId];
				pType = Type.GetType(typeName);
			}

			/* Create object of target type
			 */
			result = pType.InvokeMember(null, 
				BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | 
				BindingFlags.Instance | BindingFlags.CreateInstance, 
				null, null, null);

			/* Read out object properties an iterate thru properties and map values to
			 * target attributes in object
			 */
			typeProperties = pType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

			for (int i = 0; i < typeProperties.Length; i++) 
			{
				columnValue = rowValues[tableShortName.ToUpper() + typeProperties[i].Name.ToUpper()];

				/* Check for null columns
				 */
				if (!(columnValue is DBNull)) 
				{
					typeProperties[i].SetValue(result, 
						parseToType(columnValue, 
						typeProperties[i].PropertyType), 
						null);
				}
			}

			return result;
		}

		/// <summary>
		/// Converts an numeric object into the numeric type given as parameter. 
		/// Conversion is needed for numeric values from database as, for example, int-values from
		/// SQL Server are read as double-values. To set the cooresponding int-attribute in class
		/// a conversion is done into the target type.
		/// Conversion into boolean values from numeric value in database are performed by the rule:
		/// 1 = <code>true</code>
		/// 0 = <code>false</code>
		/// </summary>
		/// <param name="pValue">Object to be converted</param>
		/// <param name="pType">Target type.</param>
		/// <returns>Converted object.</returns>
		private Object parseToType(Object pValue, Type pType)
		{
			Object result;

			// Convert Byte
			if (pType.Equals(typeof(Byte))) 
			{
				result = Convert.ToByte(pValue);
			} 
			// Convert SByte
			else if (pType.Equals(typeof(SByte))) 
			{
				result = Convert.ToSByte(pValue);
			}
			// Convert Int16
			else if (pType.Equals(typeof(Int16))) 
			{
				result = Convert.ToInt16(pValue);
			}
			// Convert Int32
			else if (pType.Equals(typeof(Int32))) 
			{
				result = Convert.ToInt32(pValue);
			}
			// Convert Int64
			else if (pType.Equals(typeof(Int64))) 
			{
				result = Convert.ToInt64(pValue);
			}
			// Convert UInt16
			else if (pType.Equals(typeof(UInt16))) 
			{
				result = Convert.ToUInt16(pValue);
			}
			// Convert UInt32
			else if (pType.Equals(typeof(UInt32))) 
			{
				result = Convert.ToUInt32(pValue);
			}
			// Convert UInt64
			else if (pType.Equals(typeof(UInt64))) 
			{
				result = Convert.ToUInt64(pValue);
			}
			// Convert Single
			else if (pType.Equals(typeof(Single))) 
			{
				result = Convert.ToSingle(pValue);
			}
			// Convert Double
			else if (pType.Equals(typeof(Double))) 
			{
				result = Convert.ToDouble(pValue);
			}
			// Convert Decimal
			else if (pType.Equals(typeof(Decimal))) 
			{
				result = Convert.ToDecimal(pValue);
			}
			// Convert numeric into Boolean
			else if (pType.Equals(typeof(Boolean))) 
			{
				result = Int32.Parse(pValue.ToString()) == 1 ? true : false;
			}
			// Default
			else 
			{
				result = pValue;
			}

			return result;
		}

		#endregion // End of Methods

	}
}

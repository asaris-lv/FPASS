using System;
using System.Data;

namespace de.pta.Component.DbAccess
{
	/// <summary>
	/// <para>
	/// This is the interface for all implemented data providers. A data provider
	/// must supply the functionality to create DataAdapter, Command and DataReader
	/// objects which are specific to the database which is used. All methods uses the
	/// .NET Framework interfaces as return parameter to keep the application independent
	/// from the underlying database.
	/// </para>
	/// <para>
	/// A Connection object will not be available directly for an application. The data 
	/// provider manages two Connection objects internally, one for the DataAdapters and
	/// one for the DataReaders. The application may access the connection object via the
	/// DataAdapters / DataReaders Connection property.
	/// </para>
	/// <para>
	/// All SQL statements and SQL parameters for the DataAdapters / DataReaders used by
	/// an application are stored in a XML configuration file which meets the standards
	/// of a PTA component. The XML is transformed to a <see cref="de.pta.Component.DbAccess.Internal.Configuration.DbAccessConfiguration">DbAccessConfiguration</see>
	/// object. The configuration for a specific DataAdapter / Command is requested via an
	/// unique identifier.
	/// </para>
	/// </summary>
	/// <remarks>
	/// <para><b>History</b></para>
	/// <list type="table">
	/// <item>
	/// <term><b>Author:</b></term>
	/// <description>A. Seibt, PTA GmbH</description>
	/// </item>
	/// <item>
	/// <term><b>Date:</b></term>
	/// <description>Aug/29/2003</description>
	/// </item>
	/// <item>
	/// <term><b>Remarks:</b></term>
	/// <description>initial version</description>
	/// </item>
	/// </list>
	/// </remarks>
	public interface IProvider
	{
		/// <summary>
		/// Returns a command object for the concrete data provider.
		/// </summary>
		/// <param name="pId">the id of the command as defined in the configuration</param>
		/// <returns></returns>
		/// <exception cref="de.pta.Component.DbAccess.Exceptions.ProviderException">
		/// if the Command object could not be created
		/// </exception>
		/// <exception cref="de.pta.Component.DbAccess.Exceptions.ConfigNotFoundException">
		/// if the passed id for the command was invalid.
		/// </exception>
		IDbCommand CreateCommand(string pId);

		/// <summary>
		/// Returns a data adapter object for the concrete data provider.
		/// </summary>
		/// <param name="pId">the id of the data adapter as defined in the configuration</param>
		/// <returns></returns>
		/// <exception cref="de.pta.Component.DbAccess.Exceptions.ProviderException">
		/// if the DataAdapter object or the CommandBuilder object (which is used internally
		/// to create the insert, update and delete statements) could not be created.
		/// </exception>
		/// <exception cref="de.pta.Component.DbAccess.Exceptions.ConfigNotFoundException">
		/// if the passed id for the DataAdapter was invalid.
		/// </exception>
		IDbDataAdapter CreateDataAdapter(string pId);
		
		/// <summary>
		/// Returns a DataReader for the specified Command object via the ExecuterReader()
		/// method.
		/// </summary>
		/// <param name="pCommand">A Command object with an Select statement.</param>
		/// <returns></returns>
		/// <remarks>
		/// The returned DataReader uses its own Connection object. The connection is
		/// automatically closed if you call the DataReader.Close() method.
		/// </remarks>
		/// <exception cref="de.pta.Component.DbAccess.Exceptions.ProviderException">
		/// if the connection for the reader could not be opened or the ExecuteReader()
		/// method of the Command object failed.
		/// </exception>
		IDataReader GetReader(IDbCommand pCommand);

		/// <summary>
		/// Set the value of a parameter in the Select command of a data adapter.
		/// </summary>
		/// <param name="pAdapter">The DataAdapter object.</param>
		/// <param name="pParameterId">The id of the parameter to set.</param>
		/// <param name="pParameterValue">The value of the parameter to set.</param>
		void SetParameter(IDbDataAdapter pAdapter, string pParameterId, object pParameterValue);

		/// <summary>
		/// Sets a parameter for a Command object.
		/// </summary>
		/// <param name="pCommand">The Command object.</param>
		/// <param name="pParameterId">The id of the parameter to set.</param>
		/// <param name="pParameterValue">The value of the parameter to set.</param>
		void SetParameter(IDbCommand pCommand, string pParameterId, object pParameterValue);

		/// <summary>
		/// Starts a transaction for a DataAdapter. The transaction is initialized for the
		/// connection which is held by the DataAdapter.
		/// </summary>
		/// <param name="pAdapter">The data adapter object which holds the connection to where
		/// the transaction belongs.</param>
		/// <returns></returns>
		IDbTransaction GetTransaction(IDbDataAdapter pAdapter);
		
		/// <summary>
		/// Starts a transaction for a Command. The transaction is initialized for the
		/// connection which is held by the Command.
		/// </summary>
		/// <param name="pCommand">The command object which holds the connection to where
		/// the transaction belongs.</param>
		/// <returns></returns>
		IDbTransaction GetTransaction(IDbCommand pCommand);

		/// <summary>
		/// Fills the query result of a DataAdapter into a DataSet.
		/// </summary>
		/// <param name="pId">The Id of the DataAdapter configuration</param>
		/// <param name="pAdapter">The DataAdapter which queries the data.</param>
		/// <param name="pDataSet">The DataSet into which the query result should be filled.</param>
		/// <returns>The number of records in the query result.</returns>
		/// <remarks>
		/// The Id of the DataAdapter configuration is needed to get additional
		/// configuration information which is not part of the DataAdapter itself.
		/// </remarks>
		int FillDataSet(string pId, IDbDataAdapter pAdapter, DataSet pDataSet);
	}
}

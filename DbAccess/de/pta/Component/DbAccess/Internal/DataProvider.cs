using System;
using System.Data;
using System.Data.Common;

using de.pta.Component.Logging.Log4NetWrapper;

using de.pta.Component.DbAccess.Exceptions;
using de.pta.Component.DbAccess.Internal.Configuration;
using DbParameter = de.pta.Component.DbAccess.Internal.Configuration.DbParameter;

namespace de.pta.Component.DbAccess.Internal
{
	/// <summary>
	/// This is the abstract base class for all implemented data providers.
	/// It implements the <see cref="de.pta.Component.DbAccess.IProvider">IProvider</see>
	/// interface. Most of the mesthods are defined abstract as they have to use the
	/// database specific .NET data classes.
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
	internal abstract class DataProvider : IProvider
	{
		#region Members

		/// <summary>
		/// Enables logging functionality.
		/// </summary>
		protected static Logger mLog = new Logger(typeof(DataProvider));

		/// <summary>
		/// Holds the configuration for all DataAdapters and Commands.
		/// </summary>
		protected internal DbAccessConfiguration mConfig;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public DataProvider()
		{
			initialize();
		}

		#endregion //End of Constructors

		#region Initialization

		/// <summary>
		/// Get the configuration data.
		/// </summary>
		private void initialize()
		{
			mConfig = DbAccessConfiguration.GetInstance();
		}	

		#endregion //End of Initialization

		#region Accessors 

		#endregion //End of Accessors

		#region IProvider Members

		/// <summary>
		/// Implements the <see cref="de.pta.Component.DbAccess.IProvider.CreateCommand">IProvider</see>
		/// <code>CreateCommand</code> method.
		/// </summary>
		/// <param name="pId"></param>
		/// <returns></returns>
		public abstract IDbCommand CreateCommand(string pId);

		/// <summary>
		/// Implements the <see cref="de.pta.Component.DbAccess.IProvider.CreateDataAdapter">IProvider</see>
		/// <code>CreateDataAdapter</code> method.
		/// </summary>
		/// <param name="pId"></param>
		/// <returns></returns>
		public abstract IDbDataAdapter CreateDataAdapter(string pId);

		/// <summary>
		/// Implements the <see cref="de.pta.Component.DbAccess.IProvider.GetReader">IProvider</see>
		/// <code>GetReader</code> method.
		/// </summary>
		/// <param name="pCommand"></param>
		/// <returns></returns>
		public abstract IDataReader GetReader(IDbCommand pCommand);

		/// <summary>
		/// Implements the <see cref="de.pta.Component.DbAccess.IProvider.SetParameter">IProvider</see>
		/// <code>SetParameter</code> method.
		/// </summary>
		/// <param name="pAdapter"></param>
		/// <param name="pParameterId"></param>
		/// <param name="pParameterValue"></param>
		/// <returns></returns>
		public void SetParameter(IDbDataAdapter pAdapter, string pParameterId, object pParameterValue)
		{
			IDbCommand cmd = pAdapter.SelectCommand;
			if(cmd.Parameters.Contains(pParameterId))
			{
				IDataParameter param = (IDataParameter)cmd.Parameters[pParameterId];
				param.Value = pParameterValue;
			}
		}

		/// <summary>
		/// Implements the <see cref="de.pta.Component.DbAccess.IProvider.SetParameter">IProvider</see>
		/// <code>SetParameter</code> method.
		/// </summary>
		/// <param name="pCommand"></param>
		/// <param name="pParameterId"></param>
		/// <param name="pParameterValue"></param>
		/// <returns></returns>
		public void SetParameter(IDbCommand pCommand, string pParameterId, object pParameterValue)
		{
			if(pCommand.Parameters.Contains(pParameterId))
			{
				IDataParameter param = (IDataParameter)pCommand.Parameters[pParameterId];
				param.Value = pParameterValue;
			}
		}

		/// <summary>
		/// Implements the <see cref="de.pta.Component.DbAccess.IProvider.GetTransaction">IProvider</see>
		/// <code>GetTransaction</code> method.
		/// </summary>
		/// <param name="pCommand"></param>
		/// <returns></returns>
		public IDbTransaction GetTransaction(IDbCommand pCommand)
		{
			return GetTransaction(pCommand.Connection);
		}

		/// <summary>
		/// Implements the <see cref="de.pta.Component.DbAccess.IProvider.GetTransaction">IProvider</see>
		/// <code>GetTransaction</code> method.
		/// </summary>
		/// <param name="pDataAdapter"></param>
		/// <returns></returns>
		public IDbTransaction GetTransaction(IDbDataAdapter pDataAdapter)
		{
			return GetTransaction(pDataAdapter.SelectCommand.Connection);
		}

		/// <summary>
		/// Implements the <see cref="de.pta.Component.DbAccess.IProvider.FillDataSet">IProvider</see>
		/// <code>FillDataSet</code> method.
		/// </summary>
		/// <param name="pId"></param>
		/// <param name="pAdapter"></param>
		/// <param name="pDataSet"></param>
		/// <returns></returns>
		/// <exception cref="de.pta.Component.DbAccess.Exceptions.ConfigNotFoundException">
		/// Is thrown if the parameter <code>pId</code> contains an invalid id.
		/// </exception>
		public int FillDataSet(string pId, IDbDataAdapter pAdapter, DataSet pDataSet)
		{
			DataAdapterConfiguration config = mConfig.GetAdapterData(pId);
			if(null == config)
			{
				mLog.Fatal("The configuration <" + pId + "> does not exist");
				throw new ConfigNotFoundException("Configuration for data adapter " + pId + " not found");
			}
//			DataTable table = new DataTable();
//			DbDataAdapter adapter = (DbDataAdapter)pAdapter;
//			table.TableName = config.SourceTable;
//			int numRecs = adapter.Fill(table);
//			pDataSet.Tables.Add(table);

			DbDataAdapter adapter = (DbDataAdapter)pAdapter;
			int numRecs = adapter.Fill(pDataSet, config.SourceTable);
			return numRecs;
		}

		#endregion // IProvider Members

		#region Methods 

		/// <summary>
		/// Starts a transaction for the passed Connection object.
		/// </summary>
		/// <param name="pConnection">A Connection object for which the transaction should be started.</param>
		/// <returns>A Transaction object</returns>
		/// <remarks>
		/// This method could be implemented here if the exception handling of the .NET Framework 
		/// where better. But every .NET Dataprovider defines his own exceptions, so they only
		/// could be catched in our specific provider classes.
		/// </remarks>
		protected abstract IDbTransaction GetTransaction(IDbConnection pConnection);

		#endregion // End of Methods

	}
}

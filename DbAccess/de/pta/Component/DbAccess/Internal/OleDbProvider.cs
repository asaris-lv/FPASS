using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;

using de.pta.Component.DbAccess.Enumerations;
using de.pta.Component.DbAccess.Exceptions;
using de.pta.Component.DbAccess.Internal.Configuration;
using DbParameter = de.pta.Component.DbAccess.Internal.Configuration.DbParameter;

namespace de.pta.Component.DbAccess.Internal
{
	/// <summary>
	/// This class implements the <see cref="de.pta.Component.DbAccess.IProvider">IProvider</see> interface
	/// for the .NET OleDb data provider.
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
	internal class OleDbProvider : DataProvider
	{
		#region Members

		/// <summary>
		/// The Connection object for all DataAdapters
		/// </summary>
		private OleDbConnection mConnection;

		/// <summary>
		/// The Connection object for all Commands.
		/// </summary>
		private OleDbConnection mReaderConnection;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public OleDbProvider() : base()
		{
			initialize();
		}

		#endregion //End of Constructors

		#region Initialization

		/// <summary>
		/// Initializes the members.
		/// </summary>
		private void initialize()
		{
			mConnection       = new OleDbConnection(mConfig.ConnectString);
			mReaderConnection = new OleDbConnection(mConfig.ConnectString);
		}	

		#endregion //End of Initialization

		#region Accessors 

		#endregion //End of Accessors


		#region DataProvider Members

		/// <summary>
		/// Implements the <see cref="de.pta.Component.DbAccess.IProvider">IProvider</see>
		/// method.
		/// </summary>
		/// <param name="pId"></param>
		/// <returns></returns>
		public override IDbCommand CreateCommand(string pId)
		{
			OleDbCommand cmd = new OleDbCommand();
			if(null == cmd)
			{
				mLog.Fatal("Could not instantiate OleDbCommand object for <" + pId + ">");
				throw new ProviderException("Creation of Command object failed");
			}
			CommandConfiguration config = mConfig.GetCommandData(pId);
			if(null == config)
			{
				mLog.Fatal("The configuration <" + pId + "> does not exist");
				throw new ConfigNotFoundException("Configuration for Command " + pId + " not found");
			}
			cmd.CommandText = config.Sql;
			cmd.CommandType = CommandType.Text;
			if(config.Parameters.Count > 0)
			{
				AddParameters(cmd, config);
			}
			return cmd;
		}

		/// <summary>
		/// Implements the <see cref="de.pta.Component.DbAccess.IProvider">IProvider</see>
		/// method.
		/// </summary>
		/// <param name="pId"></param>
		/// <returns></returns>
		public override IDbDataAdapter CreateDataAdapter(string pId)
		{
			OleDbDataAdapter adapter = new OleDbDataAdapter();
			if(null == adapter)
			{
				mLog.Fatal("Could not instantiate OleDbDataAdapter object for <" + pId + ">");
				throw new ProviderException("Creation of DataAdapter object failed");
			}
			DataAdapterConfiguration config = mConfig.GetAdapterData(pId);
			if(null == config)
			{
				mLog.Fatal("The configuration <" + pId + "> does not exist");
				throw new ConfigNotFoundException("Configuration for data adapter " + pId + " not found");
			}
			string sql = config.Sql;
			OleDbCommand cmd = new OleDbCommand();
			cmd.CommandText = sql;
			cmd.CommandType = CommandType.Text;
			cmd.Connection  = mConnection;
			if(config.Parameters.Count > 0)
			{
				AddParameters(cmd, config);
			}
			adapter.SelectCommand = cmd;
			if(!config.ReadOnly) 
			{
				OleDbCommandBuilder builder = new OleDbCommandBuilder(adapter);
				if(null == builder)
				{
					mLog.Fatal("Could not instantiate OleDbCommandBuilder object for <" + pId + ">");
					throw new ProviderException("Creation of CommandBuilder object failed");
				}
				else
				{
					adapter.InsertCommand = builder.GetInsertCommand();
					adapter.UpdateCommand = builder.GetUpdateCommand();
					adapter.DeleteCommand = builder.GetDeleteCommand();
				}
			}
			adapter.TableMappings.Add(DbDataAdapter.DefaultSourceTableName, config.SourceTable);
			return adapter;
		}

		/// <summary>
		/// Implements the <see cref="de.pta.Component.DbAccess.IProvider">IProvider</see>
		/// method.
		/// </summary>
		/// <param name="pCommand"></param>
		/// <returns></returns>
		public override IDataReader GetReader(IDbCommand pCommand)
		{
			OleDbCommand cmd = (OleDbCommand)pCommand;
			cmd.Connection = mReaderConnection;
			try
			{
				mReaderConnection.Open();
			}
			catch(InvalidOperationException e)
			{
				mLog.Info("Connection already open", e);
			}
			catch(OleDbException o)
			{
				mLog.Fatal("Could not open connection for data reader", o);
				throw new ProviderException("Open connection failed", o);
			}

			OleDbDataReader reader;
			try
			{
				reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
			}
			catch(InvalidOperationException e)
			{
				mLog.Fatal("Could not initialize the data reader", e);
				throw new ProviderException("Initialization of data reader failed", e);
			}
			return reader;
		}


		/// <summary>
		/// Implements the <see cref="de.pta.Component.DbAccess.Internal.DataProvider">DataProvider</see>
		/// method.
		/// </summary>
		/// <param name="pConnection"></param>
		/// <returns></returns>
		protected override IDbTransaction GetTransaction(IDbConnection pConnection)
		{
			OleDbConnection con = (OleDbConnection)pConnection;
			if(con.State == ConnectionState.Closed)
			{
				try
				{
					con.Open();
				}
				catch(InvalidOperationException e)
				{
					mLog.Info("Connection already open", e);
				}
				catch(OleDbException o)
				{
					mLog.Fatal("Could not open connection for data reader", o);
					throw new ProviderException("Open connection failed", o);
				}

			}
			OleDbTransaction trans = null;
			try
			{
				trans = con.BeginTransaction();
			}
			catch(InvalidOperationException e)
			{
				mLog.Fatal("Parallel transactions are not supported", e);
				throw new ProviderException("Parallel transactions are not supported");
			}
			return trans;
		}

		#endregion // End of DataProvider methods

		#region Methods 

		/// <summary>
		/// Translate the data type for a parameter from the configuration 
		/// into an OleDbType.
		/// </summary>
		/// <param name="pTypeName">The data type from the configuration</param>
		/// <returns></returns>
		private OleDbType GetOleDbType(string pTypeName)
		{
			if(pTypeName.ToLower().Equals("datetime"))
			{
				return OleDbType.Date;
			}
			else if(pTypeName.ToLower().Equals("number"))
			{
				return OleDbType.Decimal;
			}
			else if(pTypeName.ToLower().Equals("timestamp"))
			{
				return OleDbType.DBTimeStamp;
			}
			else if(pTypeName.ToLower().Equals("varchar"))
			{
				return OleDbType.VarChar;
			}
			else
			{
				return OleDbType.VarChar;
			}
		}

		/// <summary>
		/// Add parameters to a Command object. The method can be used for DataAdapters
		/// and Commands.
		/// </summary>
		/// <param name="pCommand">The Command object.</param>
		/// <param name="pConfig">The configuration for theh command.</param>
		private void AddParameters(OleDbCommand pCommand, BaseConfiguration pConfig)
		{
			IDictionaryEnumerator enumerator = pConfig.Parameters.GetEnumerator();
			OleDbParameter prm = null;
			OleDbType dataType;
			while(enumerator.MoveNext())
			{
				DbParameter dbParam = (DbParameter)enumerator.Value;
				dataType = GetOleDbType(dbParam.DbType);
				if(dbParam.Precision > 0)
				{
					prm = new OleDbParameter(dbParam.Name, dataType);
					prm.Precision = dbParam.Precision;
					prm.Scale = dbParam.Scale;
				}
				else if(dbParam.Length > 0) 
				{
					prm = new OleDbParameter(dbParam.Name, dataType, dbParam.Length);
				}
				else
				{
					prm = new OleDbParameter(dbParam.Name, dataType);
				}
				if(null == prm)
				{
					mLog.Fatal("Could not instantiate OleDbParameter object for <" + pConfig.Id + ">");
					throw new ProviderException("Creation of Parameter object failed");
				}
				pCommand.Parameters.Add(prm);
			}
		}

		#endregion // End of Methods

	}
}

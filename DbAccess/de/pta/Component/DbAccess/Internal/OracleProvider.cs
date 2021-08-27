using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;

using de.pta.Component.DbAccess.Enumerations;
using de.pta.Component.DbAccess.Exceptions;
using de.pta.Component.DbAccess.Internal.Configuration;
using DbParameter = de.pta.Component.DbAccess.Internal.Configuration.DbParameter;

namespace de.pta.Component.DbAccess.Internal
{
	/// <summary>
	/// This class implements the <see cref="de.pta.Component.DbAccess.IProvider">IProvider</see> interface
	/// for the .NET Oracle data provider.
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
	internal class OracleProvider : DataProvider
	{
		#region Members

		/// <summary>
		/// The Connection object for all DataAdapters.
		/// </summary>
		private OracleConnection mConnection;

		/// <summary>
		/// The Connection object for all Commands.
		/// </summary>
		private OracleConnection mReaderConnection;

		#endregion //End of Members

		#region Constructors

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public OracleProvider() : base()
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
			mConnection       = new OracleConnection(mConfig.ConnectString);
			mReaderConnection = new OracleConnection(mConfig.ConnectString);
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
			OracleCommand cmd = new OracleCommand();
			if(null == cmd)
			{
				mLog.Fatal("Could not instantiate OracleCommand object for <" + pId + ">");
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
			OracleDataAdapter adapter = new OracleDataAdapter();
			if(null == adapter)
			{
				mLog.Fatal("Could not instantiate OracleDataAdapter object for <" + pId + ">");
				throw new ProviderException("Creation of DataAdapter object failed");
			}
			DataAdapterConfiguration config = mConfig.GetAdapterData(pId);
			if(null == config)
			{
				mLog.Fatal("The configuration <" + pId + "> does not exist");
				throw new ConfigNotFoundException("Configuration for data adapter " + pId + " not found");
			}
			string sql = config.Sql;
			OracleCommand cmd = new OracleCommand();
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
				OracleCommandBuilder builder = new OracleCommandBuilder(adapter);
				if(null == builder)
				{
					mLog.Fatal("Could not instantiate OracleCommandBuilder object for <" + pId + ">");
					throw new ProviderException("Creation of CommandBuilder object failed");
				}
				else
				{
					try 
					{
						adapter.InsertCommand = builder.GetInsertCommand();
						adapter.UpdateCommand = builder.GetUpdateCommand();
						adapter.DeleteCommand = builder.GetDeleteCommand();
					}
					catch(Exception e)
					{
						mLog.Fatal("Could not create SQL for <" + pId + ">", e);
						throw new ProviderException("Creation of SQL failed", e);
					}
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
			OracleCommand cmd = (OracleCommand)pCommand;
			cmd.Connection = mReaderConnection;
			try
			{
				mLog.Debug($"Current command text for Reader is: {pCommand.CommandText}");

				// TODO in a later version: sometimes after committing a transaction the connection is not closed, 
				// not yet clear where in code this happens
				if (mReaderConnection.State != ConnectionState.Open)
					mReaderConnection.Open();
				else mLog.Info("Connection already open (GetReader)");
			}
			catch (InvalidOperationException e)
			{
				if (e.InnerException is System.BadImageFormatException)
				{
					mLog.Fatal("Error on initialising the data reader. Please check the Oracle Client software (32 or 64 bit). FPASS runs as a 32 bit application.", e.InnerException);
					throw new ProviderException("Initialization of data reader failed", e.InnerException);
				}
				else throw new ProviderException("Open connection failed", e);
			}
			catch (OracleException o)
			{
				mLog.Fatal("Could not open connection for data reader", o);
				throw new ProviderException("Open connection failed", o);
			}

			OracleDataReader reader;
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
			OracleConnection con;

			if (null == pConnection)
				con = mReaderConnection;
			else 
				con = (OracleConnection) pConnection;

			if (con.State == ConnectionState.Closed)
			{
				try
				{
					mLog.Debug("Opening transaction...");
					con.Open();
				}
				catch(InvalidOperationException e)
				{
					mLog.Info("Connection already open (GetTransaction)", e);
				}
				catch(OracleException o)
				{
					mLog.Fatal("Could not open connection for data reader", o);
					throw new ProviderException("Open connection failed", o);
				}

			}
			OracleTransaction trans = null;
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

		#endregion

		#region Methods 

		/// <summary>
		/// Translate the data type for a parameter from the configuration 
		/// into an OracleType.
		/// </summary>
		/// <param name="pTypeName">The data type from the configuration</param>
		/// <returns></returns>
		private OracleType GetOracleType(string pTypeName)
		{
			if(pTypeName.ToLower().Equals("datetime"))
			{
				return OracleType.DateTime;
			}
			else if(pTypeName.ToLower().Equals("number"))
			{
				return OracleType.Number;
			}
			else if(pTypeName.ToLower().Equals("timestamp"))
			{
				return OracleType.Timestamp;
			}
			else if(pTypeName.ToLower().Equals("varchar"))
			{
				return OracleType.VarChar;
			}
			else
			{
				return OracleType.VarChar;
			}
		}

		/// <summary>
		/// Add parameters to a Command object. The method can be used for DataAdapters
		/// and Commands.
		/// </summary>
		/// <param name="pCommand">The Command object.</param>
		/// <param name="pConfig">The configuration for theh command.</param>
		private void AddParameters(OracleCommand pCommand, BaseConfiguration pConfig)
		{
			IDictionaryEnumerator enumerator = pConfig.Parameters.GetEnumerator();
			OracleParameter prm = null;
			OracleType dataType;
			while(enumerator.MoveNext())
			{
				DbParameter dbParam = (DbParameter)enumerator.Value;
				dataType = GetOracleType(dbParam.DbType);
				if(dbParam.Precision > 0)
				{
					prm = new OracleParameter(dbParam.Name, dataType);
					prm.Precision = dbParam.Precision;
					prm.Scale = dbParam.Scale;
				}
				else if(dbParam.Length > 0) 
				{
					prm = new OracleParameter(dbParam.Name, dataType, dbParam.Length);
				}
				else
				{
					prm = new OracleParameter(dbParam.Name, dataType);
				}
				if(null == prm)
				{
					mLog.Fatal("Could not instantiate OracleParameter object for <" + pConfig.Id + ">");
					throw new ProviderException("Creation of Parameter object failed");
				}
				pCommand.Parameters.Add(prm);
			}
		}

		#endregion // End of Methods
	}
}

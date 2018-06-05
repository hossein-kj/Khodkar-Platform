using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using KS.Core.GlobalVarioable;
using Oracle.ManagedDataAccess.Client;
using OracleCommand = Oracle.ManagedDataAccess.Client.OracleCommand;
using OracleConnection = Oracle.ManagedDataAccess.Client.OracleConnection;
using OracleDataReader = Oracle.ManagedDataAccess.Client.OracleDataReader;
using System;

namespace KS.Core.Data.Contexts.Oracle.Base
{
    public abstract class BaseOracleHelper : IOracleHelper
    {
        protected BaseOracleHelper()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings[ConnectionKey.DefaultsOracleConnection.ToString()].ConnectionString;
        }
        protected string ConnectionString { get; }

        public virtual int ExecuteNonQuery(CommandType cmdType, string cmdText,
           params OracleParameter[] commandParameters)
        {
            using (var connection = new OracleConnection(ConnectionString))
            {
                using (var command = new OracleCommand(cmdText, connection))
                {
                    try
                    {
                        command.CommandType = cmdType;
                        if (commandParameters != null)
                            command.Parameters.AddRange(commandParameters);
                        connection.Open();
                        return command.ExecuteNonQuery();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

        }
        public virtual async Task<int> ExecuteNonQueryAsync(CommandType cmdType, string cmdText,
            params OracleParameter[] commandParameters)
        {
            using (var connection = new OracleConnection(ConnectionString))
            {
                using (var command = new OracleCommand(cmdText, connection))
                {
                    try
                    {
                        command.CommandType = cmdType;
                        if (commandParameters != null)
                            command.Parameters.AddRange(commandParameters);
                        connection.Open();
                        return await command.ExecuteNonQueryAsync();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

        }


        public virtual async Task<List<T>> ExecuteReaderAsync<T>(CommandType cmdType,
            string cmdText, Func<IDataReader, T> transform, params OracleParameter[] commandParameters)
        {
            var myList = new List<T>();

            using (var connection = new OracleConnection(ConnectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand(cmdText, connection))
                {
                    command.CommandType = cmdType;
                    if (commandParameters != null)
                        command.Parameters.AddRange(commandParameters);

                    // Since none of the rows are likely to be large, we will execute this without specifying a CommandBehavior
                    // This will cause the default (non-sequential) access mode to be used
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        //if (await reader.ReadAsync())
                        //{
                        while (await reader.ReadAsync())
                        {
                            myList.Add(transform(reader));
                        }
                        //}


                    }
                }
            }
            return myList;
        }

        public virtual async Task<List<T>> ExecuteReaderAsync<T>(CommandType cmdType,
          string cmdText, IList<Func<IDataReader, T>> transform, params OracleParameter[] commandParameters)
        {
            var myList = new List<T>();

            using (var connection = new OracleConnection(ConnectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand(cmdText, connection))
                {
                    command.CommandType = cmdType;
                    if (commandParameters != null)
                        command.Parameters.AddRange(commandParameters);

                    // Since none of the rows are likely to be large, we will execute this without specifying a CommandBehavior
                    // This will cause the default (non-sequential) access mode to be used
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        //if (await reader.ReadAsync())
                        //{
                        while (await reader.ReadAsync())
                        {
                            myList.Add(transform[0](reader));
                        }
                        var transformCounter = transform.Count();
                        var transformIndex = 1;
                        while (transformCounter > 1)
                        {
                            if (transform.Count() > 1)
                            {
                                if (await reader.NextResultAsync())
                                {
                                    while (await reader.ReadAsync())
                                    {
                                        myList.Add(transform[transformIndex](reader));
                                    }
                                }
                            }
                            transformIndex++;
                            transformCounter--;
                        }

                        //}
                    }
                }
            }
            return myList;
        }

        public virtual List<T> ExecuteReader<T>(CommandType cmdType,
           string cmdText, System.Func<IDataReader, T> transform, params OracleParameter[] commandParameters)
        {
            var myList = new List<T>();

            using (var connection = new OracleConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new OracleCommand(cmdText, connection))
                {
                    command.CommandType = cmdType;
                    if (commandParameters != null)
                        command.Parameters.AddRange(commandParameters);

                    // Since none of the rows are likely to be large, we will execute this without specifying a CommandBehavior
                    // This will cause the default (non-sequential) access mode to be used
                    using (var reader = command.ExecuteReader())
                    {
                        //if (reader.Read())
                        //{
                        while (reader.Read())
                        {
                            myList.Add(transform(reader));
                        }
                        //}


                    }
                }
            }
            return myList;
        }


        public virtual async Task<object> ExecuteScalarAsync(CommandType cmdType, string cmdText,
            params OracleParameter[] commandParameters)
        {
            using (var connection = new OracleConnection(ConnectionString))
            {
                using (var command = new OracleCommand(cmdText, connection))
                {
                    try
                    {
                        command.CommandType = cmdType;

                        command.Parameters.AddRange(commandParameters);

                        connection.Open();
                        return await command.ExecuteScalarAsync();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }


    }
}

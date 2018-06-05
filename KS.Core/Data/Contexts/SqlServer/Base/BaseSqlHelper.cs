using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using KS.Core.GlobalVarioable;


namespace KS.Core.Data.Contexts.SqlServer.Base
{
    public abstract class BaseSqlHelper 
    {
        protected BaseSqlHelper()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings[ConnectionKey.DefaultsSqlServerConnection.ToString()].ConnectionString;
        }
        protected string ConnectionString { get; }

        public virtual int ExecuteNonQuery(CommandType cmdType, string cmdText,
           params SqlParameter[] commandParameters)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var command = new SqlCommand(cmdText, connection))
                {
                    try
                    {
                        command.CommandType = cmdType;
                        if (commandParameters != null)
                            command.Parameters.AddRange(commandParameters);
                        connection.Open();
                        return  command.ExecuteNonQuery();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

        }
        public virtual async Task<int> ExecuteNonQueryAsync(CommandType cmdType, string cmdText,
            params SqlParameter[] commandParameters)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var command = new SqlCommand(cmdText, connection))
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
            string cmdText, Func<IDataReader, T> transform, params SqlParameter[] commandParameters)
        {
            var myList = new List<T>();

            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(cmdText, connection))
                {
                    command.CommandType = cmdType;
                    if (commandParameters != null)
                    command.Parameters.AddRange(commandParameters);

                    // Since none of the rows are likely to be large, we will execute this without specifying a CommandBehavior
                    // This will cause the default (non-sequential) access mode to be used
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
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
          string cmdText, IList<Func<IDataReader, T>> transform, params SqlParameter[] commandParameters)
        {
            var myList = new List<T>();

            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(cmdText, connection))
                {
                    command.CommandType = cmdType;
                    if (commandParameters != null)
                        command.Parameters.AddRange(commandParameters);

                    // Since none of the rows are likely to be large, we will execute this without specifying a CommandBehavior
                    // This will cause the default (non-sequential) access mode to be used
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
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
           string cmdText, Func<IDataReader, T> transform, params SqlParameter[] commandParameters)
        {
            var myList = new List<T>();

            using (var connection = new SqlConnection(ConnectionString))
            {
                 connection.Open();

                using (var command = new SqlCommand(cmdText, connection))
                {
                    command.CommandType = cmdType;
                    if (commandParameters != null)
                        command.Parameters.AddRange(commandParameters);

                    // Since none of the rows are likely to be large, we will execute this without specifying a CommandBehavior
                    // This will cause the default (non-sequential) access mode to be used
                    using (SqlDataReader reader =  command.ExecuteReader())
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
            params SqlParameter[] commandParameters)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var command = new SqlCommand(cmdText, connection))
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

        public virtual DataSet ExecuteQuery(string connection,string query)
        {
            var ds = new DataSet();
            using (var sqlConnection = new SqlConnection(connection))
            {
                using (var cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.CommandType = CommandType.Text;

                    sqlConnection.Open();

                    var adapter = new SqlDataAdapter(cmd);

                   
                    adapter.Fill(ds);

                    sqlConnection.Close();
                }
            }
            return ds;
        }

        public virtual int ExecuteNonQuery(string connection, string command)
        {
            var rowsAffected = 0;
            using (var sqlConnection = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand(command, sqlConnection))
                {
                    cmd.CommandType = CommandType.Text;
                    sqlConnection.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
            return rowsAffected;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace KS.Core.Data.Contexts.SqlServer.Base
{
    public interface ISqlHelper
    {
        int ExecuteNonQuery(string connection, string command);
        int ExecuteNonQuery(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters);
        Task<int> ExecuteNonQueryAsync(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters);
        DataSet ExecuteQuery(string connection, string query);
        List<T> ExecuteReader<T>(CommandType cmdType, string cmdText, Func<IDataReader, T> transform, params SqlParameter[] commandParameters);
        Task<List<T>> ExecuteReaderAsync<T>(CommandType cmdType, string cmdText, IList<Func<IDataReader, T>> transform, params SqlParameter[] commandParameters);
        Task<List<T>> ExecuteReaderAsync<T>(CommandType cmdType, string cmdText, Func<IDataReader, T> transform, params SqlParameter[] commandParameters);
        Task<object> ExecuteScalarAsync(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters);
    }
}
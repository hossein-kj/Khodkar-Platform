using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace KS.Core.Data.Contexts.Oracle.Base
{
    public interface IOracleHelper
    {
        int ExecuteNonQuery(CommandType cmdType, string cmdText, params OracleParameter[] commandParameters);
        Task<int> ExecuteNonQueryAsync(CommandType cmdType, string cmdText, params OracleParameter[] commandParameters);
        List<T> ExecuteReader<T>(CommandType cmdType, string cmdText, Func<IDataReader, T> transform, params OracleParameter[] commandParameters);
        Task<List<T>> ExecuteReaderAsync<T>(CommandType cmdType, string cmdText, IList<Func<IDataReader, T>> transform, params OracleParameter[] commandParameters);
        Task<List<T>> ExecuteReaderAsync<T>(CommandType cmdType, string cmdText, Func<IDataReader, T> transform, params OracleParameter[] commandParameters);
        Task<object> ExecuteScalarAsync(CommandType cmdType, string cmdText, params OracleParameter[] commandParameters);
    }
}
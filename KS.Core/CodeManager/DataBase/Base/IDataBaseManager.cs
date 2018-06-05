using Newtonsoft.Json.Linq;

namespace KS.Core.CodeManager.DataBase.Base
{
    public interface IDataBaseManager
    {
        JArray ExecuteNonQuery(string connection, string command);
        JArray ExecuteQuery(string connection, string query);
    }
}
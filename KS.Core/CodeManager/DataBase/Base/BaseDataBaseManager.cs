
using KS.Core.Data.Contexts.SqlServer.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using KS.Core.Exceptions;
using KS.Core.Localization;

namespace KS.Core.CodeManager.DataBase.Base
{
    public abstract class BaseDataBaseManager
    {
        private readonly ISqlHelper _sqlHelper;

        protected BaseDataBaseManager(ISqlHelper sqlHelper)
        {
            _sqlHelper = sqlHelper;
        }
        public virtual JArray ExecuteQuery(string connection, string query)
        {
            try
            {
                var ds = _sqlHelper.ExecuteQuery(connection, query);
                var data = new List<JArray>();
                if (ds.Tables.Count > 0)
                {
                    var settings = new JsonSerializerSettings();
                    settings.Converters.Add(new CustomDataTableConverter());
                    settings.Formatting = Formatting.Indented;
                    foreach (var table in ds.Tables)
                    {
                        data.Add(JArray.Parse(JsonConvert.SerializeObject(table, settings)));
                    }
                }
                return JArray.Parse(JsonConvert.SerializeObject(data));
            }
            catch (SqlException ex)
            {

                throw new KhodkarInvalidException(ex.Message);

            }
        }

        public virtual JArray ExecuteNonQuery(string connection, string command)
        {
            try
            {
                var affectedRow = _sqlHelper.ExecuteNonQuery(connection, command);
                var arrResult = new int[1];
                arrResult[0] = affectedRow;
                return JArray.Parse(JsonConvert.SerializeObject(arrResult));
            }
            catch (SqlException ex)
            {

                throw new KhodkarInvalidException( ex.Message);
                
            }
        }

    }
}

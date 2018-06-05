
using System;
using KS.Core.Localization;
using Newtonsoft.Json.Linq;
using KS.Core.Log.Elmah.Base;
using KS.Core.Utility;

namespace KS.Business.Develop.Log
{
    public class ErrorLogBiz : IErrorLogBiz
    {
        private readonly IErrorLogManager _errorLogManager;
        public ErrorLogBiz(IErrorLogManager errorLogManager)
        {
            _errorLogManager = errorLogManager;
        }
        public JObject GetByPagination(string orderBy, int skip, int take, string typeOrSourceOrMessage, string user, string fromDateTime, string toDateTime)
        {
            return _errorLogManager.GetByPagination(orderBy,
                skip,
                take, 
                typeOrSourceOrMessage,
                user,
                fromDateTime.Replace("-", Helper.RootUrl).Replace("_", ":"),
                toDateTime.Replace("-", Helper.RootUrl).Replace("_", ":"));
        }

        public JObject GetErrorById(int id)
        {
            return _errorLogManager.GetErrorById(id);
        }

        public bool Delete(JObject data)
        {
            return _errorLogManager.Delete(data);
        }

        public bool BackUp()
        {
            _errorLogManager.BackUp(LanguageManager.ToLocalDateTime(DateTime.UtcNow).Replace(":", "-").Replace("/", "_").Replace(".", "__"));
            return true;
        }
    }
}

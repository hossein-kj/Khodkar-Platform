using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using Elmah;
using KS.Core.Log.Elmah.Base;
using KS.Core.Model;
using System;
using KS.Core.Model.Log;

namespace KS.Core.Log
{
    public class ErrorLogManager:IErrorLogManager
    {
        private readonly IElmahErrorLogManager _elmahErrorLogManager;

        public ErrorLogManager(IElmahErrorLogManager elmahErrorLogManager)
        {
            _elmahErrorLogManager = elmahErrorLogManager;
        }

        public JObject GetErrorById(int id)
        {
            var error = _elmahErrorLogManager.GetErrorById(id.ToString());
     
            return JObject.Parse(JsonConvert.SerializeObject
            (new {
                Error= ErrorJson.EncodeString(ErrorXml.DecodeString(error.Error)),
                IsDebugMode = error.IsDebugMode,
            IsMobileMode = error.IsMobileMode,
        }, Formatting.None));

        }

        public JObject GetByPagination(string orderBy, int skip, int take, string typeOrSourceOrMessage, string user, string fromDateTime, string toDateTime)
        {
            var count = 0;
            return JObject.Parse(JsonConvert.SerializeObject
         (new
         {
             rows = _elmahErrorLogManager.GetByPagination(
                 orderBy,
                 skip,
                 take,
                 typeOrSourceOrMessage,
                 user,
                 fromDateTime,
                 toDateTime,
                 out count)
             .Select(lg=>new {
                Id=lg.Id.ToString().Trim(),
                 lg.LocalDateTime,
                 lg.Message,
                 lg.Source,
                 lg.StatusCode,
                 lg.Type,
                 lg.User
             }),
             total = count
         }, Formatting.None));
        }

        public bool Delete(JObject data)
        {
            return _elmahErrorLogManager.Delete(data);
        }

        public void LogException(ExceptionLog exceptionLog)
        {
            _elmahErrorLogManager.LogException(exceptionLog);
        }

        public void BackUp(string backUpName)
        {
            _elmahErrorLogManager.BackUp(backUpName);
        }
    }
}

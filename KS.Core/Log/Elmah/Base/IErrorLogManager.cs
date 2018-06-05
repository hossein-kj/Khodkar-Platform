using System.Collections.Generic;
using KS.Core.Model;
using KS.Core.Model.Log;
using Newtonsoft.Json.Linq;

namespace KS.Core.Log.Elmah.Base
{
    public interface IErrorLogManager
    {
        bool Delete(JObject data);
        JObject GetByPagination(string orderBy, int skip, int take, string typeOrSourceOrMessage, string user, string fromDateTime, string toDateTime);
        JObject GetErrorById(int id);
        void BackUp(string backUpName);
        void LogException(ExceptionLog exceptionLog);
    }
}
using System.Collections.Generic;
using System.Net.Http;
using System.Web;
using KS.Core.Model;
using KS.Core.Model.Log;
using Microsoft.Owin;
using Newtonsoft.Json.Linq;

namespace KS.Core.Log.Base
{
    public interface IActionLogManager
    {
        void LogMvcService(string name, HttpRequestBase request, string serviceUrl);
        void LogHttpService(string name,HttpRequestMessage request, string serviceUrl);
        void LogOdataService(string name, HttpRequestMessage request);
        void LogSpecialServices(string name, IOwinRequest request);
        void StartLogRequest(IOwinRequest request);
        void EndLogRequest(IOwinRequest request,bool isSuccessed);
        void Log(IActionLog action);
        List<IActionLog> GetByPagination(string orderBy, int skip, int take, string serviceUrl, string nameOrUrlOrUser, string fromDateTime, string toDateTime, out int count);
        List<IActionLog> GetByServiceUrlAndPagination(string orderBy, int skip, int take, string serviceUrl, string user, string fromDateTime, string toDateTime, out int count);
        IActionLog GetActionById(int id);
        bool Delete(JObject data);
        bool ToggleEnableLogUntilNextApplicationRestart();
        void BackUp(string backUpName);
        bool GetLogStatus();
    }
}

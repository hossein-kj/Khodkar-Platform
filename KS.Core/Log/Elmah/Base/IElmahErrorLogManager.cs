using System;
using System.Collections;
using System.Collections.Generic;
using Elmah;
using KS.Core.Model;
using KS.Core.Model.Log;
using Newtonsoft.Json.Linq;

namespace KS.Core.Log.Elmah.Base
{
    public interface IElmahErrorLogManager
    {
        List<ExceptionLog> GetByPagination(string orderBy, int skip, int take, string typeOrSourceOrMessage, string user, string fromDateTime, string toDateTime, out int count);
        ErrorLogEntry GetError(string id);
        ExceptionLog GetErrorById(string id);
        int GetErrors(int pageIndex, int pageSize, IList errorEntryList);
        void LogException(ExceptionLog exceptionLog);
        string Log(Error error);
        bool Delete(JObject data);
        IAsyncResult BeginLog(Error error, AsyncCallback asyncCallback, object asyncState);
        string EndLog(IAsyncResult asyncResult);
        IAsyncResult BeginGetError(string id, AsyncCallback asyncCallback, object asyncState);
        ErrorLogEntry EndGetError(IAsyncResult asyncResult);
        IAsyncResult BeginGetErrors(int pageIndex, int pageSize, IList errorEntryList, AsyncCallback asyncCallback, object asyncState);
        int EndGetErrors(IAsyncResult asyncResult);
        string Name { get; }
        string ApplicationName { get; set; }
        void BackUp(string backUpName);
    }
}
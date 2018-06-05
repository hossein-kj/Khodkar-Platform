using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Autofac.Integration.WebApi;
using KS.Core.CacheProvider;
using KS.Core.GlobalVarioable;
using KS.Core.Log.Elmah.Base;
using KS.Core.Model;
using KS.Core.Data.Contexts.Base;
using KS.Core.Localization;
using KS.Core.Log.Base;
using KS.Core.Model.Core;
using KS.Core.Model.Log;

namespace KS.Core.UI.Attribute.WebApi
{
    public class ExceptionFilterAttribute : IAutofacExceptionFilter
    {
        //private readonly IErrorLogManager _errorLogManager;
        //private readonly IDataBaseContextManager _dataBaseContextManager;
        //private readonly IActionLogManager _actionLogManager;
        //public ExceptionFilterAttribute(IErrorLogManager errorLogManager, IDataBaseContextManager dataBaseContextManager
        //    , IActionLogManager actionLogManager)
        //{
        //    _errorLogManager = errorLogManager;
        //    _dataBaseContextManager = dataBaseContextManager;
        //    _actionLogManager = actionLogManager;
        //}

        public Task OnExceptionAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            try
            {
                //if (actionExecutedContext.Exception.InnerException == null)
                //{
                //    exceptionMessage = actionExecutedContext.Exception.Message;
                //}
                //else
                //{
                //    exceptionMessage = actionExecutedContext.Exception.InnerException.Message;
                //}
                //We can log this exception message to the file or database.  
                var isAppError = actionExecutedContext.Exception.Message.IndexOf("asError", StringComparison.Ordinal) > -1;

                var exceptionMessage = isAppError ?
                    actionExecutedContext.Exception.Message
                    : LanguageManager.ToAsErrorMessage(ExceptionKey.UnhandledException);
                var reasonPhrase = isAppError ?
                    ""
                    : "Internal Server Error.Please Contact your Administrator.";
                var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(exceptionMessage),
                    ReasonPhrase = reasonPhrase
                };
                actionExecutedContext.Response = response;
            }
            catch (Exception)
            {


            }
            return Task.FromResult("");
        }
    }
}

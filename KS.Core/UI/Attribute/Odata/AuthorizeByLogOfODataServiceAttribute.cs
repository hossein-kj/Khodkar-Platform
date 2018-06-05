
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using Autofac.Integration.WebApi;
using KS.Core.GlobalVarioable;
using KS.Core.Log.Base;
using KS.Core.Security;
using KS.Core.Model;
using KS.Core.Log.Elmah.Base;
using KS.Core.Model.Core;
using KS.Core.Model.Log;

namespace KS.Core.UI.Attribute.Odata
{
    public class AuthorizeByLogOfODataServiceAttribute : IAutofacAuthorizationFilter
    {
        private readonly IErrorLogManager _errorLogManager;
        private readonly IActionLogManager _actionLogManager;
        public AuthorizeByLogOfODataServiceAttribute(IErrorLogManager errorLogManager, IActionLogManager actionLogManager)
        {
            _errorLogManager = errorLogManager;
            _actionLogManager = actionLogManager;
        }
        public Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            try
            {
              

                //var actionLogManager = actionContext.Request.GetDependencyScope()
                //    .GetService(typeof(Log.Base.IActionLogManager)) as Log.Base.IActionLogManager;

                var serviceUrl = actionContext.Request.RequestUri.AbsolutePath;

                IAspect aspect;
                var result = AuthorizeManager.AuthorizeMasterDataKeyValueUrl(serviceUrl, ActionKey.RequestService, out aspect);

                if (aspect.EnableLog)
                {
                    _actionLogManager?.LogOdataService(aspect.Name, actionContext.Request);

                }
                if (result) return Task.FromResult(0);
                actionContext.Response = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.Unauthorized

                };
            }
            catch (System.Exception ex)
            {
                _errorLogManager.LogException(new ExceptionLog()
                {
                    Detail = ex.ToString(),
                    Message = ex.Message,
                    Source = ex.GetType().FullName
                });
                actionContext.Response = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.Unauthorized

                };
            }
            return Task.FromResult(0);
        }
    }
}

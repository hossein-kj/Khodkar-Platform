
using System.Linq;
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

namespace KS.Core.UI.Attribute.WebApi
{
    public class AuthorizeByLogAndCheckCacheOfServiceAttribute : IAutofacAuthorizationFilter
    {
        private readonly IErrorLogManager _errorLogManager;
        private readonly IActionLogManager _actionLogManager;
        public AuthorizeByLogAndCheckCacheOfServiceAttribute(IErrorLogManager errorLogManager, IActionLogManager actionLogManager)
        {
            _errorLogManager = errorLogManager;
            _actionLogManager = actionLogManager;
        }
        public Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            try
            {
                var requestedService = actionContext.Request.RequestUri.AbsolutePath;
                var routeTemplate = actionContext.RequestContext.RouteData.Route.RouteTemplate;
                if (actionContext.RequestContext.RouteData.Values.Count > 0)
                {
                    var requestedServiceNormalUrl = requestedService[0] == '/'
                        ? requestedService.Substring(1)
                        : requestedService;
                    var lastCharIndex = requestedServiceNormalUrl.Length - 1;
                    requestedServiceNormalUrl = requestedServiceNormalUrl[lastCharIndex] == '/'
                        ? requestedServiceNormalUrl.Substring(0, lastCharIndex - 1)
                        : requestedServiceNormalUrl;

                    var routeTemplateNormalUrl = routeTemplate[0] == '/'
                        ? routeTemplate.Substring(1)
                        : routeTemplate;
                    lastCharIndex = routeTemplateNormalUrl.Length - 1;
                    routeTemplateNormalUrl = routeTemplateNormalUrl[lastCharIndex] == '/'
                        ? routeTemplateNormalUrl.Substring(0, lastCharIndex - 1)
                        : routeTemplateNormalUrl;

                    var requestedServiceToken = requestedServiceNormalUrl.ToLower().Split('/');
                    var routeTemplateToken = routeTemplateNormalUrl.ToLower().Split('/');
                    var tokenIndex = 0;
                    requestedService = "";

                    foreach (var token in routeTemplateToken)
                    {

                        if (token == requestedServiceToken[tokenIndex])
                            requestedService += "/" + token;
                        else
                        {
                            requestedService += "/@" + actionContext.RequestContext.RouteData.Values
                                                    .Where(rv => token.Contains(rv.Key.ToLower()))
                                                    .OrderByDescending(rv => rv.Key.Length)
                                                    .FirstOrDefault().Key;
                        }

                        tokenIndex++;
                    }
                }


                //var actionLogManager = actionContext.Request.GetDependencyScope()
                //    .GetService(typeof(Log.Base.IActionLogManager)) as Log.Base.IActionLogManager;


                IAspect aspect;
                var result = AuthorizeManager.AuthorizeMasterDataKeyValueUrl(requestedService, ActionKey.RequestService, out aspect);
                if (aspect.EnableLog)
                {
                    _actionLogManager?.LogHttpService(aspect.Name, actionContext.Request, requestedService);

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

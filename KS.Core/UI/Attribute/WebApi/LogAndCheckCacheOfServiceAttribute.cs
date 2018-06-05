using System;
using System.Linq;
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
using KS.Core.Log.Base;
using KS.Core.Model.Core;
using KS.Core.Model.Log;

namespace KS.Core.UI.Attribute.WebApi
{
    public class LogAndCheckCacheOfServiceAttribute : IAutofacActionFilter
    {
        private readonly IErrorLogManager _errorLogManager;
        private readonly IDataBaseContextManager _dataBaseContextManager;
        private readonly IActionLogManager _actionLogManager;
        public LogAndCheckCacheOfServiceAttribute(IErrorLogManager errorLogManager, IDataBaseContextManager dataBaseContextManager
            , IActionLogManager actionLogManager)
        {
            _errorLogManager = errorLogManager;
            _dataBaseContextManager = dataBaseContextManager;
            _actionLogManager = actionLogManager;
        }
        public Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
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
                        if (tokenIndex < requestedServiceToken.Length)
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
                        }

                        tokenIndex++;
                    }
                }

                IAspect aspect;
              
                var key = CacheManager.GetAspectKey(CacheKey.Aspect.ToString(), ActionKey.RequestService.ToString(), requestedService);
                if (CacheManager.Get<IAspect>(key).IsCached)
                    aspect = CacheManager.Get<IAspect>(key).Value;
                else
                {
                    aspect = _dataBaseContextManager.GetAspectForPublicMasterDataKeyValueUrl(requestedService);
                    CacheManager.Store(key, aspect, slidingExpiration: TimeSpan.FromMinutes(Config.AspectCacheSlidingExpirationTimeInMinutes));
                }
                if (aspect.EnableLog)
                {
                   // var actionLogManager = actionContext.Request.GetDependencyScope()
                   //.GetService(typeof(Log.Base.IActionLogManager)) as Log.Base.IActionLogManager;

                    _actionLogManager?.LogHttpService(aspect.Name, actionContext.Request, requestedService);

                }
            }
            catch (Exception ex)
            {

                _errorLogManager.LogException(new ExceptionLog()
                {
                    Detail = ex.ToString(),
                    Message = ex.Message,
                    Source = ex.GetType().FullName
                });
            }

            return Task.FromResult(0);
        }
    }
}

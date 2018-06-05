using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Autofac.Integration.WebApi;
using KS.Core.CacheProvider;
using KS.Core.Data.Contexts;
using KS.Core.GlobalVarioable;
using KS.Core.Log.Elmah.Base;
using KS.Core.Model;
using KS.Core.Data.Contexts.Base;
using KS.Core.Log.Base;
using KS.Core.Model.Core;
using KS.Core.Model.Log;

namespace KS.Core.UI.Attribute.Odata
{
    public class LogOfODataServiceAttribute : IAutofacActionFilter
    {
        private readonly IErrorLogManager _errorLogManager;
        private readonly IDataBaseContextManager _dataBaseContextManager;
        private readonly IActionLogManager _actionLogManager;
        public LogOfODataServiceAttribute(IErrorLogManager errorLogManager
            , IDataBaseContextManager dataBaseContextManager, IActionLogManager actionLogManager)
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
                var serviceUrl = actionContext.Request.RequestUri.AbsolutePath;
                var key = CacheManager.GetAspectKey(CacheKey.Aspect.ToString(), ActionKey.RequestService.ToString(), serviceUrl);
                IAspect aspect;
                if (CacheManager.Get<IAspect>(key).IsCached)
                    aspect = CacheManager.Get<IAspect>(key).Value;
                else
                {
                    aspect = _dataBaseContextManager.GetAspectForPublicMasterDataKeyValueUrl(serviceUrl);
                    CacheManager.Store(key, aspect, slidingExpiration: TimeSpan.FromMinutes(Config.AspectCacheSlidingExpirationTimeInMinutes));
                }
                if (aspect.EnableLog)
                {
                    // var actionLogManager = actionContext.Request.GetDependencyScope()
                    //.GetService(typeof(Log.Base.IActionLogManager)) as Log.Base.IActionLogManager;

                    _actionLogManager?.LogOdataService(aspect.Name, actionContext.Request);
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

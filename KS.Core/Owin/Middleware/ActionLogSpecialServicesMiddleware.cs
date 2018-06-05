
using System;
using System.Linq;
using System.Threading.Tasks;
using KS.Core.CacheProvider;
using KS.Core.Data.Contexts.Base;
using KS.Core.GlobalVarioable;
using Microsoft.Owin;
using KS.Core.Log.Base;
using KS.Core.Log.Elmah.Base;
using KS.Core.Model;
using KS.Core.Model.Core;
using KS.Core.Model.Log;


namespace KS.Core.Owin.Middleware
{
    public class ActionLogSpecialServicesMiddleware : OwinMiddleware
    {
        private readonly IActionLogManager _actionLogManager;
        private readonly IErrorLogManager _errorLogManager;
        private readonly IDataBaseContextManager _dataBaseContextManager;
        private readonly string[] _specialServices = { "/Token" };
        public ActionLogSpecialServicesMiddleware(OwinMiddleware next, IDataBaseContextManager dataBaseContextManager, IActionLogManager actionLogManager
            , IErrorLogManager errorLogManager) : base(next)
        {
            _actionLogManager = actionLogManager;
            _errorLogManager = errorLogManager;
            _dataBaseContextManager = dataBaseContextManager;
        }

        public override async Task Invoke(IOwinContext context)
        {
            var specialService = "";
            if (_specialServices.Any(service => context.Request.Path.Value == service))
            {
                specialService = context.Request.Path.Value;
            }

            try
            {

                if (specialService != "")
                {
                    IAspect aspect;

                    var key = CacheManager.GetAspectKey(CacheKey.Aspect.ToString(), ActionKey.RequestService.ToString(), specialService);
                    if (CacheManager.Get<IAspect>(key).IsCached)
                        aspect = CacheManager.Get<IAspect>(key).Value;
                    else
                    {
                        aspect = _dataBaseContextManager.GetAspectForPublicMasterDataKeyValueUrl(specialService);
                        CacheManager.Store(key, aspect, slidingExpiration: TimeSpan.FromMinutes(Config.AspectCacheSlidingExpirationTimeInMinutes));
                    }
                    if (aspect.EnableLog)
                    {
                        // var actionLogManager = actionContext.Request.GetDependencyScope()
                        //.GetService(typeof(Log.Base.IActionLogManager)) as Log.Base.IActionLogManager;

                        _actionLogManager?.LogSpecialServices(aspect.Name, context.Request);

                    }
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


            await Next.Invoke(context);

          
            
            
        }
    }
}

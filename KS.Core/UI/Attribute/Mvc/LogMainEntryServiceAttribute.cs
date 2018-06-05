using System;
using System.Web.Mvc;
using KS.Core.Model;
using KS.Core.CacheProvider;
using KS.Core.GlobalVarioable;
using KS.Core.Data.Contexts.Base;
using KS.Core.Model.Core;

namespace KS.Core.UI.Attribute.Mvc
{
    public class LogMainEntryServiceAttribute: ActionFilterAttribute
    {
        private readonly Log.Base.IActionLogManager _actionLog;
        private readonly IDataBaseContextManager _dataBaseContextManager;

        public LogMainEntryServiceAttribute(Log.Base.IActionLogManager actionLog, IDataBaseContextManager dataBaseContextManager)
        {
            _actionLog = actionLog;
            _dataBaseContextManager = dataBaseContextManager;
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var serviceUrl = "/[url]";
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
                _actionLog.LogMvcService(aspect.Name, filterContext.HttpContext.Request, serviceUrl);
            }

            //  Adapter.DoLog(filterContext);
        }
    }
}

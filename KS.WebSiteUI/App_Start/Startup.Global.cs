
using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using System.Web.Http;
using System.Web.Routing;
using System.Web.Optimization;
using JavaScriptEngineSwitcher.Core;
using KS.Core.GlobalVarioable;

namespace KS.WebSiteUI
{
    public partial class Startup
    {
        public void GlobalRegistration(HttpConfiguration config)
        {


            AreaRegistration.RegisterAllAreas();
            //GlobalConfiguration.Configure(WebApiConfig.Register);
            WebApiConfig.Register(config);
            // GlobalConfiguration.Configure(Core.Log.WebApiExceptionLogger.Register);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            JsEngineSwitcherConfig.Configure(JsEngineSwitcher.Instance);
            ClientDataTypeModelValidatorProvider.ResourceClassKey = "Resources.ErrorMessages";
            DefaultModelBinder.ResourceClassKey = "Resources.ErrorMessages";
        }

        void RegisterConfig()
        {
            Config.LocalTimeZoneId = ConfigurationManager.AppSettings["LocalTimeZoneId"];

            Config.PagesSourceCodePath = ConfigurationManager.AppSettings["PagesSourceCodePath"];

            Config.ScriptDebugPath = ConfigurationManager.AppSettings["ScriptDebugPath"];
            Config.ScriptDistPath = ConfigurationManager.AppSettings["ScriptDistPath"];
            Config.StyleDebugPath = ConfigurationManager.AppSettings["StyleDebugPath"];
            Config.StyleDistPath = ConfigurationManager.AppSettings["StyleDistPath"];
            Config.ResourcesSourceCodePath = ConfigurationManager.AppSettings["ResourcesSourceCodePath"];
            Config.StyleDebugPagesPath = ConfigurationManager.AppSettings["StyleDebugPagesPath"];
            Config.StyleDistPagesPath = ConfigurationManager.AppSettings["StyleDistPagesPath"];
            Config.ThumbnailPath = ConfigurationManager.AppSettings["ThumbnailPath"];
            Config.LanguageAndCultures = ConfigurationManager.AppSettings[ConfigKey.LanguageAndCultures.ToString()].Split(',').ToList();
            Config.QueryStringSign = ConfigurationManager.AppSettings["QueryStringSign"];
            Config.MobileSign = ConfigurationManager.AppSettings["MobileSign"];
            Config.MobileFallBack = Convert.ToBoolean(ConfigurationManager.AppSettings["MobileFallBack"]);
            Config.PagesPath = ConfigurationManager.AppSettings["PagesPath"];
            Config.LogOffSign = ConfigurationManager.AppSettings["LogOffSign"];
            Config.UrlDelimeter = ConfigurationManager.AppSettings["UrlDelimeter"];
            Config.ServicesSourceCodePath = ConfigurationManager.AppSettings["ServicesSourceCodePath"];
            Config.ResourcesDistPath = ConfigurationManager.AppSettings["ResourcesDistPath"];
            Config.DefaultsLanguage = ConfigurationManager.AppSettings["DefaultsLanguage"];
            Config.ScriptDebugPagesPath = ConfigurationManager.AppSettings["ScriptDebugPagesPath"];
            Config.ScriptDistPagesPath = ConfigurationManager.AppSettings["ScriptDistPagesPath"];
            Config.LoginExpireTimeSpanInHours = Convert.ToInt32(ConfigurationManager.AppSettings["LoginExpireTimeSpanInHours"]);
            Config.LanguageAndCultureCoockie = ConfigurationManager.AppSettings["LanguageAndCultureCoockie"];
            Config.IsDebugModeCoockie = ConfigurationManager.AppSettings["IsDebugModeCoockie"];
            Config.IsMobileModeCoockie = ConfigurationManager.AppSettings["IsMobileModeCoockie"];
            Config.IsAuthenticatedCoockie = ConfigurationManager.AppSettings["IsAuthenticatedCoockie"];
            Config.DebugIdSign = ConfigurationManager.AppSettings["DebugIdSign"];
            Config.BrowserCodeDependencyEngineSourcePath = ConfigurationManager.AppSettings["BrowserCodeDependencyEngineSourcePath"];
            Config.LogPath = ConfigurationManager.AppSettings["LogPath"];
            Config.LogBackgroundJobIntervalsInMilliseconds
          = Convert.ToInt32(ConfigurationManager.AppSettings["LogBackgroundJobIntervalsInMilliseconds"]);
            Config.ErrrorPagesBaseUrl = ConfigurationManager.AppSettings["ErrrorPagesBaseUrl"];
            Config.SourceCodeDeletedPath = ConfigurationManager.AppSettings["SourceCodeDeletedPath"];
            var url = ConfigurationManager.AppSettings["$.asUrls.frameWorkServices_getWebPage"];
            Config.DefaultsGetWebPagesServiceUrl = url.IndexOf("@", StringComparison.Ordinal) > 0 ? url.Substring(0, url.IndexOf("@", StringComparison.Ordinal)) : url;
            Config.LoginUrl = ConfigurationManager.AppSettings["LoginUrl"];
            Config.AspectCacheSlidingExpirationTimeInMinutes = Convert.ToInt32(ConfigurationManager.AppSettings["AspectCacheSlidingExpirationTimeInMinutes"]);
            Config.GroupCacheSlidingExpirationTimeInMinutes = Convert.ToInt32(ConfigurationManager.AppSettings["GroupCacheSlidingExpirationTimeInMinutes"]);
            Config.MasterDataLocalKeyValueCacheSlidingExpirationTimeInMinutes = Convert.ToInt32(ConfigurationManager.AppSettings["MasterDataLocalKeyValueCacheSlidingExpirationTimeInMinutes"]);
            Config.EnableActionLog = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableActionLog"]);
        }
    }
}
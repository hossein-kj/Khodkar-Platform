using System.Linq;
using System.Web;
using KS.Core.CoockieProvider;
using KS.Core.GlobalVarioable;

namespace KS.Core.UI.Setting.Adapters
{
    public abstract class BaseSettingsAdapter
    {
        public virtual string Language => UrlLang ?? ClientSelectedLanguage ?? Config.DefaultsLanguage;

        public virtual bool IsDebugMode
        {
            get
            {
                if (HttpContext.Current == null) return false;
                var headers = HttpContext.Current.Request.Headers;
                if (CookieManager.Get(Config.IsDebugModeCoockie) != null)
                {
                    dynamic langCoockie = CookieManager.GetAsJson(Config.IsDebugModeCoockie);
                    return langCoockie.IsDebugMode ?? false;
                }
                if (!headers.HasKeys())
                    return false;
                var isDebugMode = headers.Get("isDebugMode");
                return isDebugMode == "true";
            }
        }

        public virtual bool IsMobileMode
        {
            get
            {
                if (HttpContext.Current == null) return false;
                var headers = HttpContext.Current.Request.Headers;
                if (CookieManager.Get(Config.IsMobileModeCoockie) != null)
                {
                    dynamic mobileCoockie = CookieManager.GetAsJson(Config.IsMobileModeCoockie);
                    return mobileCoockie.IsMobileMode ?? false;
                }
                if (!headers.HasKeys())
                    return false;
                var isMobileMode = headers.Get("isMobileMode");
                return isMobileMode == "true";
            }
        }

        //public virtual bool IsAuthenticated
        //{
        //    get
        //    {
        //        if (HttpContext.Current == null) return false;
        //        var headers = HttpContext.Current.Request.Headers;
        //        if (CookieManager.Get(Config.IsAuthenticatedCoockie) != null)
        //        {
        //            dynamic authCoockie = CookieManager.GetAsJson(Config.IsAuthenticatedCoockie);
        //            return authCoockie.IsAuthenticated ?? false;
        //        }
        //        if (!headers.HasKeys())
        //            return false;
        //        var isAuthenticated = headers.Get("isAuthenticated");
        //        return isAuthenticated == "true";
        //    }
        //}

        public virtual string ClientSelectedLanguage
        {
            get
            {
                if (HttpContext.Current == null) return null;
                if (CookieManager.Get(Config.LanguageAndCultureCoockie) != null)
                {
                    dynamic langCoockie = CookieManager.GetAsJson(Config.LanguageAndCultureCoockie);
                    return langCoockie.lang;
                }
                var headers = HttpContext.Current.Request.Headers;

                if (!headers.HasKeys()) return null;
                var lang = headers.Get("lang");
                return lang;
            }
        }

        public virtual string Culture
        {
            get
            {
                if (HttpContext.Current == null) return null;
                if (CookieManager.Get(Config.LanguageAndCultureCoockie) != null)
                {
                    dynamic langCoockie = CookieManager.GetAsJson(Config.LanguageAndCultureCoockie);
                    return langCoockie.culture;
                }

                var headers = HttpContext.Current.Request.Headers;

                if (!headers.HasKeys()) return null;
                var culture = headers.GetValues("culture");
                return culture?.First();
            }
        }

        public virtual string Country
        {
            get
            {
                if (HttpContext.Current == null) return null;
                if (CookieManager.Get(Config.LanguageAndCultureCoockie) != null)
                {
                    dynamic langCoockie = CookieManager.GetAsJson(Config.LanguageAndCultureCoockie);
                    return langCoockie.country;
                }

                var headers = HttpContext.Current.Request.Headers;

                if (!headers.HasKeys()) return null;
                var country = headers.GetValues("country");
                return country?.First();
            }
        }

        public virtual bool RightToLeft
        {
            get
            {
                if (HttpContext.Current == null) return false;
                if (CookieManager.Get(Config.LanguageAndCultureCoockie) != null)
                {
                    dynamic langCoockie = CookieManager.GetAsJson(Config.LanguageAndCultureCoockie);
                    return langCoockie.rightToLeft == "true";
                }

                var headers = HttpContext.Current.Request.Headers;

                if (!headers.HasKeys()) return true;
                var rightToLeft = headers.GetValues("rightToLeft");
                if(rightToLeft != null)
                    return rightToLeft.First() == "true";
                return true;
            }
        }

        protected virtual string UrlLang
        {
            get
            {
                if (HttpContext.Current == null)
                    return null;
                var url = HttpContext.Current.Request.Url.AbsolutePath;
                var urlParts = url.Split('/');
                if (urlParts.Length <= 1 || url == "/") return null;
                if(Config.LanguageAndCultures == null)
                    return null;
                if (Config.LanguageAndCultures.Any(ln => ln == urlParts[1]))
                    return urlParts[1];
                return null;
            }
        }
    }
}

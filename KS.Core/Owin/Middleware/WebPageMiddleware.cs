
using System;
using System.Net;
using System.Threading.Tasks;
using KS.Core.GlobalVarioable;
using KS.Core.Localization;
using KS.Core.Log.Elmah.Base;
using KS.Core.Model;
using KS.Core.Model.Core;
using KS.Core.Model.Log;
using KS.Core.Security;
using KS.Core.Utility;
using Microsoft.Owin;
using KS.Core.UI.Setting;

namespace KS.Core.Owin.Middleware
{
    public class WebPageMiddleware : OwinMiddleware
    {
        private readonly IErrorLogManager _errorLogManager;
        public WebPageMiddleware(OwinMiddleware next, IErrorLogManager errorLogManager) : base(next)
        {
            _errorLogManager = errorLogManager;
        }

        private void SetErrorPage(HttpStatusCode code, string pageType, IOwinContext context)
        {

            var type = ((pageType ?? WebPageType.Form.ToString()) == WebPageType.Modal.ToString()
                ? WebPageType.Modal
                : WebPageType.Form).ToString();

            var pagePath = Config.ErrrorPagesBaseUrl + (int)code;
            if (code == HttpStatusCode.Unauthorized)
            {
                pagePath = Config.LoginUrl;
            }

            pagePath = ("/" + LanguageManager.ApplyLanguageAndMobileSignToAjaxRequestAsync
                  (pagePath.Replace(Config.UrlDelimeter, Helper.RootUrl).Replace("#", ""))).Replace("//", "/");

            IAspect aspect;
            AuthorizeManager.AuthorizeWebPageUrl(pagePath, type, out aspect);


            if (!aspect.HasMobileVersion)
            {
                pagePath = (pagePath + @"/").EndsWith(Config.MobileSign) ?
                    pagePath.Replace(Config.MobileSign.Substring(0, Config.MobileSign.Length - 1), "").Replace("//", "/")
                    : pagePath.Replace(Config.MobileSign, Helper.RootUrl).Replace("//", "/");

                //pagePath = pagePath.Replace(Config.MobileSign, Helper.RootUrl);
            }


            pagePath = (Config.PagesPath.Substring(1) + pagePath.Replace("//", "/")).Replace("//", "/");

            context.Response.Redirect(pagePath.Replace("//", "/") + "-" + type +
                                      ".json?url=" + pagePath.Replace(Config.PagesPath.Substring(1),Helper.RootUrl).Replace("//", "/") + "&type=" + type);


        }

        public override async Task Invoke(IOwinContext context)
        {
            
            var mainPath = Config.DefaultsGetWebPagesServiceUrl.ToLower();
            var isModal = false;
            var requestUrl = context.Request.Path.Value.ToLower();

            if (requestUrl.StartsWith(mainPath) && !Settings.IsDebugMode)
            {
                var url = requestUrl.Replace(mainPath, "");
                if (url.Length > 5)
                {
                    if (url.Substring(url.Length - 5) == "/true")
                    {
                        url = url.Remove(url.Length - 5);
                        isModal = true;
                    }
                }

                var dbUrl = ("/" + LanguageManager.ApplyLanguageAndMobileSignToAjaxRequestAsync
                                 (url.Replace(Config.UrlDelimeter, Helper.RootUrl).Replace("#", ""))).Replace("//", "/");
                var type = (isModal ? WebPageType.Modal : WebPageType.Form).ToString();


                try
                {

                    IAspect aspect;
                    AuthorizeManager.AuthorizeWebPageUrl(dbUrl, type, out aspect);



                    if (aspect.IsNull)
                    {
                        SetErrorPage(HttpStatusCode.NotFound, type, context);
                    }
                    else
                    {
                        if (!aspect.HasMobileVersion && Config.MobileFallBack)
                        {
                            dbUrl = (dbUrl + @"/").EndsWith(Config.MobileSign)
                                ? dbUrl.Replace(Config.MobileSign.Substring(0, Config.MobileSign.Length - 1),
                                    "").Replace("//", "/")
                                : dbUrl.Replace(Config.MobileSign, Helper.RootUrl).Replace("//", "/");

                            //dbUrl = dbUrl.Replace(Config.MobileSign, Helper.RootUrl);
                        }




                        dbUrl = dbUrl.EndsWith("/") ? dbUrl.Substring(0, dbUrl.Length - 1) : dbUrl;

                        url = (Config.PagesPath.Substring(1) + dbUrl.Replace("//", "/")).Replace("//", "/");
                       
                        context.Response.Redirect(url + "-" + (isModal ? WebPageType.Modal : WebPageType.Form) +
                                                  ".json?url=" + dbUrl.Replace("//", "/") + "&type=" +
                                                  (isModal ? WebPageType.Modal : WebPageType.Form));




                    }



                }
                catch (Exception ex)
                {
                    SetErrorPage(HttpStatusCode.NotFound, type, context);

                    _errorLogManager.LogException(new ExceptionLog()
                    {
                        Detail = ex.ToString(),
                        Message = ex.Message,
                        Source = ex.GetType().FullName
                    });
                }

            }
            
                await Next.Invoke(context);
        }
    }
}

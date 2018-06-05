using System;
using System.Net;
using System.Threading;
using System.Web;
using KS.Core.CacheProvider;
using KS.Core.Data.Contexts;
using KS.Core.GlobalVarioable;
using KS.Core.Log.Elmah.Base;
using KS.Core.Model;
using KS.Core.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using KS.Core.UI.Setting;
using KS.Core.Data.Contexts.Base;
using KS.Core.Model.Core;
using KS.Core.Model.Log;

namespace KS.Core.Security.Handlers.AuthorizePagesPath
{
    public class AuthorizePagesPathAsynchOperation : IAsyncResult
    {
        private bool _completed;
        private readonly Object _state;
        private readonly AsyncCallback _callback;
        private readonly HttpContext _context;
        private readonly IErrorLogManager _errorLogManager;
        private readonly IDataBaseContextManager _dataBaseContextManager;
        bool IAsyncResult.IsCompleted => _completed;
        WaitHandle IAsyncResult.AsyncWaitHandle => null;
        Object IAsyncResult.AsyncState => _state;
        bool IAsyncResult.CompletedSynchronously => false;

        public AuthorizePagesPathAsynchOperation(AsyncCallback callback, HttpContext context,
            IErrorLogManager errorLogManager,
            IDataBaseContextManager dataBaseContextManager,Object state = null)
        {
            _callback = callback;
            _context = context;
            _state = state;
            _completed = false;
            _errorLogManager = errorLogManager;
            _dataBaseContextManager = dataBaseContextManager;
        }

        public void StartAsyncWork()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(StartAsyncTask), null);
        }

        private void SetErrorPage(HttpStatusCode code,string pageType)
        {
            try
            {

                _context.Response.StatusCode = (int)code;
                var type = ((pageType ?? WebPageType.Form.ToString()) == WebPageType.Modal.ToString()
                    ? WebPageType.Modal
                    : WebPageType.Form);
                var pagePath = Config.ErrrorPagesBaseUrl + (int)code;
                if (code == HttpStatusCode.Unauthorized)
                {
                    pagePath = Config.LoginUrl;
                }



                var virtualPath =
                                                pagePath + "-" +
                                                type + ".json";

                var path = (Config.PagesPath + Settings.Language + virtualPath).Replace("//", "/");
                if (Settings.IsMobileMode)
                {

                    IAspect aspect;
                    AuthorizeManager.AuthorizeWebPageUrl(pagePath, type.ToString(), out aspect);


                    if (aspect.HasMobileVersion)
                        path = (Config.PagesPath + Settings.Language + Config.MobileSign + virtualPath).Replace("//", "/");

                }

 

                _context.Response.WriteFile(path.Replace("~", ""));
            }
            catch (Exception ex)
            {

                _errorLogManager.LogException(new ExceptionLog()
                {
                    Detail = ex.ToString(),
                    Message = ex.Message,
                    Source = ex.GetType().FullName
                });


                _context.Response.ContentType = "application/json";
                _context.Response.Write((JObject.Parse(JsonConvert.SerializeObject
                (new
                {
                    modifyRoleId = 5,
                    viewRoleId = 6,
                    enableCache = false,
                    status = 1,
                    title = "Error!",
                    cacheSlidingExpirationTimeInMinutes = 0,
                    pageId = "if29a53784fb34da1806c6ce945790dc5",
                    dependentModules = "[]",
                    param = "{}",
                    html =
                    " <span style='display:none' id='if29a53784fb34da1806c6ce945790dc5'></span>Error!"
                }, Formatting.None,
                    new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }))).ToString());
            }
        }
        private void StartAsyncTask(Object workItemState)
        {
            var queryString = _context.Request.QueryString;
            var url = queryString["url"];
            var type = queryString["type"];
         
            try
            {
                if (url == null || type == null)
                {

                    SetErrorPage(HttpStatusCode.Unauthorized, type);

                }
                else
                {
                    IAspect aspect;

                    if (AuthorizeManager.AuthorizeWebPageUrl(url, type, out aspect) || url.Contains(Config.ErrrorPagesBaseUrl.Substring(1)))
                    {
                        if(aspect.Status == 0)
                            SetErrorPage(HttpStatusCode.TemporaryRedirect, type);
                        else
                        {


                            if (aspect.EnableCache)
                            {
                                var pageType = type == WebPageType.Modal.ToString()
                             ? WebPageType.Modal
                             : WebPageType.Form;

                                if (!aspect.HasMobileVersion && Config.MobileFallBack)
                                {
                                    url = (url + @"/").EndsWith(Config.MobileSign)
                                        ? url.Replace(Config.MobileSign.Substring(0, Config.MobileSign.Length - 1),
                                            "").Replace("//", "/")
                                        : url.Replace(Config.MobileSign, Helper.RootUrl).Replace("//", "/");

                                    //url = url.Replace(Config.MobileSign, Helper.RootUrl);
                                }

                                var key = CacheManager.GetWebPageKey(pageType.ToString(), url);
                                var pageCache = CacheManager.Get<dynamic>(key);

                                if (!pageCache.IsCached)
                                {
                                    CacheManager.Store(key, _dataBaseContextManager.GetWebPageForView(url, type).ToJObject(),
                                        slidingExpiration:
                                        TimeSpan.FromMinutes(aspect.CacheSlidingExpirationTimeInMinutes));
                                    pageCache = CacheManager.Get<dynamic>(key);

                                }
                              
                                    _context.Response.Write(pageCache.Value.ToString());
                                
                            }
                            else
                            {

                                
                                var path = _context.Request.RawUrl.IndexOf("?", StringComparison.Ordinal) > 0
                           ? _context.Request.RawUrl.Remove(_context.Request.RawUrl.IndexOf("?",
                               StringComparison.Ordinal))
                           : _context.Request.RawUrl;

                                if (!aspect.HasMobileVersion && Config.MobileFallBack)
                                {
                                    path = (path + @"/").EndsWith(Config.MobileSign)
                                        ? path.Replace(Config.MobileSign.Substring(0, Config.MobileSign.Length - 1),
                                            "").Replace("//", "/")
                                        : path.Replace(Config.MobileSign, Helper.RootUrl).Replace("//", "/");

                                    //path = path.Replace(Config.MobileSign, Helper.RootUrl);
                                }

                           

                                    _context.Response.WriteFile(path);
                            }


                        }
                    }
                    else
                    {
                        SetErrorPage(HttpStatusCode.Unauthorized, type);
                    }

                }
            }
            catch (Exception ex)
            {
                SetErrorPage(HttpStatusCode.NotFound, type);

                _errorLogManager.LogException(new ExceptionLog()
               {
                   Detail = ex.ToString(),
                   Message = ex.Message,
                   Source = ex.GetType().FullName
               });
            }

            _completed = true;
            _callback(this);
        }
    }
}
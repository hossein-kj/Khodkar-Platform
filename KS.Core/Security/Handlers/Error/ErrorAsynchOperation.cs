using System;
using System.Net;
using System.Threading;
using System.Web;
using KS.Core.GlobalVarioable;
using KS.Core.UI.Setting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KS.Core.Security.Handlers.Error
{
    public class ErrorAsynchOperation : IAsyncResult
    {
        private bool _completed;
        private readonly Object _state;
        private readonly AsyncCallback _callback;
        private readonly HttpContext _context;
        bool IAsyncResult.IsCompleted => _completed;
        WaitHandle IAsyncResult.AsyncWaitHandle => null;
        Object IAsyncResult.AsyncState => _state;
        bool IAsyncResult.CompletedSynchronously => false;

        public ErrorAsynchOperation(AsyncCallback callback, HttpContext context, Object state = null)
        {
            _callback = callback;
            _context = context;
            _state = state;
            _completed = false;
        }

        public void StartAsyncWork()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(StartAsyncTask), null);
        }

        private void SetErrorPage(HttpStatusCode code, string pageType)
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




                _context.Response.WriteFile(path.Replace("~", ""));
            }
            catch (Exception ex)
            {


                _context.Response.ContentType = "application/json";
                _context.Response.StatusCode = (int)code;
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
            //var queryString = _context.Request.QueryString;
            //var url = queryString["url"];
            //var type = queryString["type"];

            try
            {
                SetErrorPage(
                    _context.User.Identity.IsAuthenticated == false
                        ? HttpStatusCode.Unauthorized
                        : HttpStatusCode.InternalServerError, WebPageType.Form.ToString());
            }
            catch (Exception)
            {
                SetErrorPage(HttpStatusCode.NotFound, WebPageType.Form.ToString());
            }

            _completed = true;
            _callback(this);
        }
    }
}

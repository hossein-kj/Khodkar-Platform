using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using KS.Business.ContenManagment.Base;
using Newtonsoft.Json.Linq;
using KS.Core.GlobalVarioable;
using KS.Core.Utility;
using KS.Core.Security;
using KS.Core.SessionProvider.Base;
using KS.WebSiteUI.Controllers.Base;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

namespace KS.WebSiteUI.Controllers.Defaults
{
    public class WebApiController : BasePublicWebApiController
    {
        private readonly IWebPageBiz _webPageBiz;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly ISessionManager _sessionManager;
        public WebApiController(IWebPageBiz webPageBiz, IAuthenticationManager authenticationManager
            , ISessionManager sessionManage)
        {
            _webPageBiz = webPageBiz;
            _authenticationManager = authenticationManager;
            _sessionManager = sessionManage;
        }



        [Route("Defaults/GetWebPage/{url}/{isModal?}")]
        [HttpGet]
        public async Task<JObject> GetWebPage(string url, bool isModal = false)
        {


            try
            {
                return await

                        _webPageBiz.GetWebPageForDebugAsync(
                                url.Replace(Config.UrlDelimeter, Helper.RootUrl).Replace("#", ""), isModal);

            }
            catch (UnauthorizedAccessException)
            {

                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
        }

        [Route("Defaults/Reload/{*url}")]
        [HttpGet]
        public async Task<JObject> Reload(string url)
        {
            return await _webPageBiz.ToJsonAsync(WebPageJsonType.Reload, webPageUrl: url);
        }

        [Route("Defaults/ChangeTemplate/{*url}")]
        [HttpGet]
        public async Task<JObject> ChangeTemplate(string url)
        {
            return await _webPageBiz.ToJsonAsync(WebPageJsonType.ChangeTemplate, webPageUrl: url);
        }

        [Authorize]
        [Route("Defaults/IsAuthenticated")]
        [HttpGet]
        public bool IsAuthenticated()
        {
            return true;
        }

        [Route("Defaults/LogOff")]
        [HttpGet]
        public bool LogOff()
        {
            CurrentUserManager.LogOff(_sessionManager);
            _authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie,
                OAuthDefaults.AuthenticationType,
                DefaultAuthenticationTypes.ExternalCookie,
                DefaultAuthenticationTypes.ExternalBearer,
                DefaultAuthenticationTypes.TwoFactorCookie,
                DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);
            return true;

        }
    }
}

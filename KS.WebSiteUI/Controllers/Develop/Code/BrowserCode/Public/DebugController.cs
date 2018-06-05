using KS.Core.CodeManager;
using KS.Core.GlobalVarioable;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using KS.Core.CodeManager.Base;
using KS.WebSiteUI.Controllers.Base;
using Microsoft.AspNet.Identity;

namespace KS.WebSiteUI.Controllers.Develop.Code.BrowserCode.Public
{
    public class DebugController : BasePublicWebApiController
    {
        private readonly ISourceControl _sourceControl;
        public DebugController(ISourceControl sourceControl)
        {
            _sourceControl = sourceControl;
        }

        [HostAuthentication(DefaultAuthenticationTypes.ApplicationCookie)]
        [Route("scripts/debug/{*path}")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetScript(string path = "")
        {
            var response = new HttpResponseMessage
            {
                Content =
                    new StringContent(await _sourceControl.GetJavascriptOfDebugPath(Config.ScriptDebugPath + path),
                        Encoding.UTF8, "application/javascript")
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/javascript");
            return response;
        }
    }
}

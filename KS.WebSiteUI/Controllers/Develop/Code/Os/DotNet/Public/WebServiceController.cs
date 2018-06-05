using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using KS.Business.Develop.Code.Os.DotNet;
using KS.WebSiteUI.Controllers.Base;

namespace KS.WebSiteUI.Controllers.Develop.Code.Os.DotNet.Public
{
    public class WebServiceController : BasePublicWebApiController
    {
       private readonly IDotNetBiz _dotNetBiz;

       public WebServiceController(IDotNetBiz dotNetBiz)
        {
            _dotNetBiz = dotNetBiz;
        }

        [Route("develop/code/os/dotnet/WebService/GetWcfGenreatedCode/{wcfGuid}")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetWcfMetadata(Guid wcfGuid)
        {
            //read xml file by guid and then delete that xml file and if not found xml file throw unAuthorized Exception.

            return new HttpResponseMessage() { Content = new StringContent(await _dotNetBiz.ReadWcfWebServiceMetaDataAsync(wcfGuid.ToString("N")),
                Encoding.UTF8, "application/xml") };
        }
    }
}
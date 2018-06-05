
using System.Threading.Tasks;
using System.Web.Http;
using KS.Business.ContenManagment.Base;
using KS.Core.GlobalVarioable;
using KS.Model.ContentManagement;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using KS.Core.Security;
using KS.Core.Utility;
using KS.WebSiteUI.Controllers.Base;

namespace KS.WebSiteUI.Controllers.ContentManagement.Http
{
    public class WebPageController : BaseAuthorizedWebApiController
    {
       private readonly IWebPageBiz _webPageBiz;

       public WebPageController(IWebPageBiz contentManagementBiz)
        {
            _webPageBiz = contentManagementBiz;
        }

       
       [Route("cms/WebPage/Get/{url}/{typeId}")]
       [HttpGet]
       public async Task<JObject> GetWebPageContent(string url, int typeId)
       {
           var form = await _webPageBiz.GetWebPageForEditAsync(url.Replace(Config.UrlDelimeter, Helper.RootUrl).Replace("#", ""), typeId);
           if (form != null)
               return form;
           
               return JObject.Parse(JsonConvert.SerializeObject(new WebPage() { Guid= SecureGuid.NewGuid().ToString("N") }, Formatting.None));
       }

        [Route("cms/webpage/save")]
        [HttpPost]
        public async Task<JObject> SaveWebPage(JObject data)
        {
            return JObject.Parse(JsonConvert.SerializeObject
                (await _webPageBiz.Save(data), Formatting.None));
        }

        [Route("cms/webpage/delete")]
        [HttpPost]
        public async Task<bool> DeleteWebPage(JObject data)
        {
            return await _webPageBiz.Delete(data);
        }

        [Route("cms/WebPage/GetWebPageChanges/{orderBy}/{skip}/{take}/{comment}/{user}/{fromDateTime}/{toDateTime}/{webPageGuid}/{type}")]
        [HttpGet]
        public JObject GetWebPageChangesFromSourceControl(string orderBy, int skip, int take
            , string comment
            ,string user
            , string fromDateTime
            , string toDateTime
            , string webPageGuid
            ,string type)
        {
            return _webPageBiz.GetWebPageChangesFromSourceControl(orderBy, skip, take
            , comment
            ,user
            , fromDateTime
            , toDateTime
            , webPageGuid
            , type);
        }

        [Route("cms/WebPage/GetWebPageChange/{changeId}/{webPageGuid}")]
        [HttpGet]
        public async Task<JObject> GetWebPageChangeFromSourceControl(int changeId, string webPageGuid)
        {
            return await _webPageBiz.GetWebPageChangeFromSourceControlAsync(changeId, webPageGuid);
        }


        [Route("cms/WebPage/GetWebPageResourcesChange/{changeId}/{webPageGuid}")]
        [HttpGet]
        public async Task<string> GetWebPageResourcesChangeFromSourceControl(int changeId, string webPageGuid)
        {
            return await _webPageBiz.GetWebPageResourcesChangeFromSourceControlAsync(changeId, webPageGuid);
        }
    }
}
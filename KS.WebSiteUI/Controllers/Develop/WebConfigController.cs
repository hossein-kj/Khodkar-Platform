using System.Web.Http;
using KS.Business.Develop;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using KS.WebSiteUI.Controllers.Base;

namespace KS.WebSiteUI.Controllers.Develop
{
    public class WebConfigController : BaseAuthorizedWebApiController
    {
        private readonly IWebConfigBiz _webConfigBiz;
        public WebConfigController(IWebConfigBiz webConfigBiz)
        {
            _webConfigBiz = webConfigBiz;
        }

        [Route("develop/webconfig/settings/save/")]
        [HttpPost]
        public async Task<JObject> Save(JObject data)
        {
            return JObject.Parse(JsonConvert.SerializeObject
               (await _webConfigBiz.Save(data), Formatting.None));
        }
        [Route("develop/webconfig/settings/delete/")]
        [HttpPost]
        public bool Delete(JObject data)
        {
            return _webConfigBiz.Delete(data);
        }

        [Route("develop/webconfig/settings/GetSettingsByPagination/{orderBy}/{skip}/{take}")]
        [HttpGet]
        public async Task<JObject> GetSettingsByPagination(string orderBy, int skip, int take)
        {
            return await _webConfigBiz.GetSettingsByPagination(orderBy, skip, take);
        }

        [Route("develop/webconfig/settings/GetMasterDataKeyValuePropertyName/")]
        [HttpGet]
        public JArray GetMasterDataKeyValuePropertyName()
        {
            return JArray.Parse(JsonConvert.SerializeObject(_webConfigBiz.GetMasterDataKeyValuePropertyName(), Formatting.None));
        }
    }
}

using System.Threading.Tasks;
using System.Web.Http;
using KS.Business.ContenManagment.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using KS.WebSiteUI.Controllers.Base;

namespace KS.WebSiteUI.Controllers.ContentManagement.Http
{
    public class MasterDataKeyValueController : BaseAuthorizedWebApiController
    {
       private readonly IMasterDataKeyValueBiz _masterDataKeyValueBiz;

       public MasterDataKeyValueController(IMasterDataKeyValueBiz masterDataKeyValueBiz)
        {
            _masterDataKeyValueBiz = masterDataKeyValueBiz;
        }
        
        [Route("cms/masterdatakeyvalue/get/{id}")]
        [HttpGet]
        public async Task<JObject> Get(int id)
        {
            return await _masterDataKeyValueBiz.GetAsync(id);
        }

        [Route("cms/masterdatakeyvalue/Save")]
        [HttpPost]
        public async Task<JObject> Save(JObject data)
        {
            return JObject.Parse(JsonConvert.SerializeObject
                (await _masterDataKeyValueBiz.Save(data), Formatting.None,
                new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
        }

        [Route("cms/masterdatalocalkeyvalue/Save")]
        [HttpPost]
        public async Task<JObject> SaveTranslate(JObject data)
        {
                return JObject.Parse(JsonConvert.SerializeObject
                        (await _masterDataKeyValueBiz.SaveTranslate(data), Formatting.None,
                        new JsonSerializerSettings() {ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
        }

        [Route("cms/masterdatakeyvalue/delete")]
        [HttpPost]
        public async Task<bool> DeleteMasterDataKeyValue(JObject data)
        {
            await _masterDataKeyValueBiz.Delete(data);
            return true;
        }
    }
}
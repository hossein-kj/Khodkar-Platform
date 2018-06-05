using System.Threading.Tasks;
using System.Web.Http;
using KS.Business.ContenManagment.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using KS.WebSiteUI.Controllers.Base;

namespace KS.WebSiteUI.Controllers.ContentManagement.Http
{
    public class LanguageAndCultureController : BaseAuthorizedWebApiController
    {
       private readonly ILanguageAndCultureBiz _languageAndCultureBiz;

       public LanguageAndCultureController(ILanguageAndCultureBiz languageAndCultureBiz)
        {
            _languageAndCultureBiz = languageAndCultureBiz;
        }


        [Route("cms/languageAndCulture/Get/{id}")]
        [HttpGet]
        public async Task<JObject> Get(int id)
        { 
                return await _languageAndCultureBiz.GetAsync(id);
        }

        [Route("cms/languageAndCulture/Save")]
        [HttpPost]
        public async Task<JObject> Save(JObject data)
        {
            return JObject.Parse(JsonConvert.SerializeObject
            (await _languageAndCultureBiz.Save(data), Formatting.None,
                new JsonSerializerSettings() {ReferenceLoopHandling = ReferenceLoopHandling.Ignore}));
        }

        [Route("cms/languageAndCulture/delete")]
        [HttpPost]
        public async Task<bool> DeleteService(JObject data)
        {
            return await _languageAndCultureBiz.Delete(data);
        }
    }
}
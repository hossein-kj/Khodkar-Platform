using System.Threading.Tasks;
using System.Web.Http;
using KS.Business.ContenManagment.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using KS.WebSiteUI.Controllers.Base;

namespace KS.WebSiteUI.Controllers.ContentManagement.Http
{
    public class LinkController : BaseAuthorizedWebApiController
    {
       private readonly ILinkBiz _linkBiz;

       public LinkController(ILinkBiz linkBiz)
        {
            _linkBiz = linkBiz;
        }

        [Route("cms/link/Save")]
        [HttpPost]
        public async Task<JObject> Save(JObject data)
        {

            return JObject.Parse(JsonConvert.SerializeObject
            (await _linkBiz.Save(data), Formatting.None,
                new JsonSerializerSettings() {ReferenceLoopHandling = ReferenceLoopHandling.Ignore}));
        }

        [Route("cms/link/delete")]
        [HttpPost]
        public async Task<bool> DeleteLink(JObject data)
        {
            return await _linkBiz.Delete(data);
        }
    }
}
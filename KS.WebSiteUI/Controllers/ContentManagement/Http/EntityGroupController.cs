using System.Threading.Tasks;
using System.Web.Http;
using KS.Business.ContenManagment.Base;
using Newtonsoft.Json.Linq;
using KS.WebSiteUI.Controllers.Base;

namespace KS.WebSiteUI.Controllers.ContentManagement.Http
{
    public class EntityGroupController : BaseAuthorizedWebApiController
    {
        private readonly IEntityGroupBiz _entityGroupBiz;

       public EntityGroupController(IEntityGroupBiz entityGroupBiz)
        {
            _entityGroupBiz = entityGroupBiz;
        }

        [Route("cms/entityGroup/save")]
        [HttpPost]
        public async Task<string> Save(JObject data)
        {
            return await _entityGroupBiz.Save(data);
        }
    }
}

using System.Threading.Tasks;
using System.Web.Http;
using KS.Business.Security.Base;
using KS.WebSiteUI.Controllers.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KS.WebSiteUI.Controllers.Security.Http
{
    public class PermissionController : BaseAuthorizedWebApiController
    {
        private readonly IPermissionBiz _permissionBiz;

        public PermissionController(IPermissionBiz permissionBiz)
        {
            _permissionBiz = permissionBiz;
        }

        [Route("security/permission/Save")]
        [HttpPost]
        public async Task<JObject> Save(JObject data)
        {
            return JObject.Parse(JsonConvert.SerializeObject
                (await _permissionBiz.Save(data), Formatting.None,
                new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
        }

        [Route("security/permission/delete")]
        [HttpPost]
        public async Task<bool> Delete(JObject data)
        {
            await _permissionBiz.Delete(data);
            return true;
        }
    }
}


using System.Threading.Tasks;
using System.Web.Http;
using KS.Business.Security.Base;
using KS.WebSiteUI.Controllers.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace KS.WebSiteUI.Controllers.Security.Http
{
    public class SecurityController : BaseAuthorizedWebApiController
    {
        private readonly ISecurityBiz _security;
        public SecurityController(ISecurityBiz security)
        {
            _security = security;
        }

        [Route("security/role/Save")]
        [HttpPost]
        public async Task<JObject> SaveRole(JObject data)
        {

            return JObject.Parse(JsonConvert.SerializeObject
            (await _security.SaveRole(data), Formatting.None,
                new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
        }

        [Route("security/role/delete")]
        [HttpPost]
        public async Task<bool> DeleteRole(JObject data)
        {
            return await _security.DeleteRole(data);
        }

        [Route("security/localRole/Save")]
        [HttpPost]
        public async Task<JObject> SaveRoleTranslate(JObject data)
        {
            return JObject.Parse(JsonConvert.SerializeObject
                    (await _security.SaveRoleTranslate(data), Formatting.None,
                    new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
        }

        [Route("security/group/Save")]
        [HttpPost]
        public async Task<JObject> SaveGroup(JObject data)
        {

            return JObject.Parse(JsonConvert.SerializeObject
            (await _security.SaveGroup(data), Formatting.None,
                new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
        }

        [Route("security/group/delete")]
        [HttpPost]
        public async Task<bool> DeleteGroup(JObject data)
        {
            return await _security.DeleteGroup(data);
        }

        [Route("security/localGroup/Save")]
        [HttpPost]
        public async Task<JObject> SaveGroupTranslate(JObject data)
        {
            return JObject.Parse(JsonConvert.SerializeObject
                    (await _security.SaveGroupTranslate(data), Formatting.None,
                    new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
        }

        #region [user manager]

        [Route("security/user/GetUsersByPagination/{groupId}/{orderBy}/{skip}/{take}")]
        [HttpGet]
        public async Task<JObject> GetUsersByPagination(int groupId, string orderBy, int skip, int take)
        {
           return await _security.GetUsersByPagination(groupId, orderBy, skip, take);
        }

        [Route("security/user/Save")]
        [HttpPost]
        public async Task<JObject> SaveUser(JObject data)
        {

            return await _security.SaveUser(data);
        }

        [Route("security/user/delete")]
        [HttpPost]
        public async Task<bool> DeleteUser(JObject data)
        {
            return await _security.DeleteUser(data);
        }

        [Route("security/user/changePassword")]
        [HttpPost]
        public async Task<bool> ChangePassword(JObject changeData)
        {
            return await _security.ChangePassword(changeData);
        }

        #endregion [user manager]
    }
}
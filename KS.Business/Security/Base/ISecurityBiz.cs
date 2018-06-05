using System.Threading.Tasks;
using KS.Model.Security;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace KS.Business.Security.Base
{
    public interface ISecurityBiz
    {
        Task<ApplicationUser> GetUserAsync(int userId);
        Task<JObject> SaveUser(JObject user);
        Task<bool> DeleteRole(JObject data);
        Task<ApplicationLocalRole> SaveRoleTranslate(JObject data);
        Task<ApplicationRole> SaveRole(JObject role);
        Task<bool> DeleteGroup(JObject data);
        Task<ApplicationGroup> SaveGroup(JObject group);
        Task<ApplicationLocalGroup> SaveGroupTranslate(JObject data);
        Task<JObject> GetUsersByPagination(int groupId, string orderBy, int skip, int take);
        Task<bool> DeleteUser(JObject user);
        Task<bool> ChangePassword(JObject changeData);
    }
}

using System.Linq;
using KS.Core.GlobalVarioable;
using KS.Core.Security;
using KS.Core.UI.Attribute.Odata;
using KS.Model.Security;
using KS.WebSiteUI.Controllers.Base;
using KS.DataAccess.Contexts.Base;

namespace KS.WebSiteUI.Controllers.Security.OpenData
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using KS.Model.Security;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<ApplicationLocalRole>("LocalRoles");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class LocalRolesController : BaseAuthorizedODataController
    {
        private readonly ISecurityContext _db;

        public LocalRolesController(ISecurityContext context)
        {
            _db = context;
        }
        // GET: odata/LocalRoles
        [EnableDynamicQueryable]
        public IQueryable<ApplicationLocalRole> GetLocalRoles()
        {
           

            return _db.ApplicationLocalRoles.Where(m => CurrentUserManager.RolesId.Contains((int)Roles.Admin) ||
            CurrentUserManager.RolesId.Contains(m.Role.ViewRoleId??0));
        }

        private bool ApplicationLocalRoleExists(int key)
        {
            return _db.ApplicationLocalRoles.Count(e => e.Id == key) > 0;
        }
    }
}

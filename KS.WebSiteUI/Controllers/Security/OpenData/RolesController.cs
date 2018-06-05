
using System.Linq;
using System.Web.Http.OData.Query;
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
    builder.EntitySet<ApplicationRole>("Roles");
    builder.EntitySet<ApplicationLocalRole>("ApplicationLocalRoles"); 
    builder.EntitySet<ApplicationUserRole>("ApplicationUserRoles"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
 
    public class RolesController : BaseAuthorizedODataController
    {
        private readonly ISecurityContext _db;

        public RolesController(ISecurityContext context)
        {
            _db = context;
        }
        // GET: odata/Roles
        [EnableDynamicQueryable]
        public IQueryable<ApplicationRole> GetRoles(ODataQueryOptions queryOptions)
        {
            return 
            _db.Roles.Where(md => CurrentUserManager.RolesId.Contains((int)Roles.Admin) ||
            CurrentUserManager.RolesId.Contains(md.ViewRoleId??0));

          
        }

        private bool ApplicationRoleExists(int key)
        {
            return _db.Roles.Count(e => e.Id == key) > 0;
        }
    }
}

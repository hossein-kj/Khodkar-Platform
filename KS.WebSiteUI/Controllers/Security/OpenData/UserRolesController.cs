using System;
using System.Linq;
using System.Web.Http.OData.Query;
using KS.Core.GlobalVarioable;
using KS.Core.Security;
using KS.Core.UI.Attribute.Odata;
using KS.DataAccess.Contexts;
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
    builder.EntitySet<ApplicationUserRole>("UserRoles");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */

    public class UserRolesController : BaseAuthorizedODataController
    {
        private readonly ISecurityContext _db;

        public UserRolesController(ISecurityContext context)
        {
            _db = context;
        }
        // GET: odata/UserRoles
        [EnableDynamicQueryable]
        public IQueryable<ApplicationUserRole> GetUserRoles(ODataQueryOptions queryOptions)
        {
            

            var query = _db.ApplicationUserRoles.AsQueryable();
            if (queryOptions.SelectExpand?.RawExpand != null)
            {
                if (queryOptions.SelectExpand.RawExpand.IndexOf("User", StringComparison.Ordinal) != -1)
                    query = query.Where(m => CurrentUserManager.RolesId.Contains((int)Roles.Admin) 
                    || CurrentUserManager.RolesId.Contains(m.User.ViewRoleId??0));
                if (queryOptions.SelectExpand.RawExpand.IndexOf("Role", StringComparison.Ordinal) != -1)
                    query = query.Where(m => CurrentUserManager.RolesId.Contains((int)Roles.Admin) ||
                    CurrentUserManager.RolesId.Contains(m.Role.ViewRoleId??0));
            }



            return query;
        }

        private bool ApplicationUserRoleExists(int key)
        {
            return _db.ApplicationUserRoles.Count(e => e.UserId == key) > 0;
        }
    }
}

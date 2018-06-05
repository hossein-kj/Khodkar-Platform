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
    builder.EntitySet<ApplicationGroupRole>("GroupRoles");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */

    public class GroupRolesController : BaseAuthorizedODataController
    {
        private readonly ISecurityContext _db;

        public GroupRolesController(ISecurityContext context)
        {
            _db = context;
        }
        // GET: odata/GroupRoles
        [EnableDynamicQueryable]
        public IQueryable<ApplicationGroupRole> GetGroupRoles(ODataQueryOptions queryOptions)
        {
            

            var query = _db.ApplicationGroupRoles.AsQueryable();
            if (queryOptions.SelectExpand?.RawExpand != null)
            {
                if (queryOptions.SelectExpand.RawExpand.IndexOf("Group", StringComparison.Ordinal) != -1)
                    query = query.Where(m => CurrentUserManager.RolesId.Contains((int)Roles.Admin) 
                    || CurrentUserManager.RolesId.Contains(m.Group.ViewRoleId??0));
                if (queryOptions.SelectExpand.RawExpand.IndexOf("Role", StringComparison.Ordinal) != -1)
                    query = query.Where(m => CurrentUserManager.RolesId.Contains((int)Roles.Admin) ||
                    CurrentUserManager.RolesId.Contains(m.Role.ViewRoleId??0));
            }



            return query;
        }

        private bool ApplicationGroupRoleExists(int key)
        {
            return _db.ApplicationGroupRoles.Count(e => e.GroupId == key) > 0;
        }
    }
}

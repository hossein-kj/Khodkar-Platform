using System;
using System.Linq;
using System.Web.Http.OData.Query;
using KS.Core.GlobalVarioable;
using KS.Core.Security;
using KS.Core.UI.Attribute.Odata;
using KS.DataAccess.Contexts;
using KS.Model.ContentManagement;
using KS.WebSiteUI.Controllers.Base;
using KS.DataAccess.Contexts.Base;

namespace KS.WebSiteUI.Controllers.ContentManagement.OpenData
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using KS.Model.ContentManagement;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<MasterDataKeyValue>("MasterDataKeyValues");
    builder.EntitySet<UserProfile>("Users"); 
    builder.EntitySet<EntityGroup>("EntityGroups"); 
    builder.EntitySet<MasterDataLocalKeyValue>("MasterDataLocalKeyValues"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
 
    public class MasterDataKeyValuesController : BaseAuthorizedODataController
    {
        private readonly IContentManagementContext _db;

        public MasterDataKeyValuesController(IContentManagementContext context)
        {
            _db = context;
        }
        // GET: odata/MasterDataKeyValues
        [EnableDynamicQueryable]
        public IQueryable<MasterDataKeyValue> GetMasterDataKeyValues(ODataQueryOptions queryOptions)
        {
            //var query = _db.MasterDataKeyValues.Where(md => md.IsLeaf == false ||
            //(CurrentUser.RolesId.Contains(md.ViewRoleId.ToString()) && md.IsLeaf));

            //if (queryOptions.SelectExpand?.RawExpand != null)
            //{
            //    if (queryOptions.SelectExpand.RawExpand.IndexOf("Parent", StringComparison.Ordinal) != -1)
            //        query = query.Where(m => CurrentUser.RolesId.Contains(m.Parent.ViewRoleId.ToString()));
            //}

            var query = _db.MasterDataKeyValues.Where(md => CurrentUserManager.RolesId.Contains((int)Roles.Admin) ||
                    CurrentUserManager.RolesId.Contains(md.ViewRoleId??0));

            if (queryOptions.SelectExpand?.RawExpand != null)
            {
                if (queryOptions.SelectExpand.RawExpand.IndexOf("Parent", StringComparison.Ordinal) != -1)
                    query = query.Where(m => CurrentUserManager.RolesId.Contains((int)Roles.Admin) ||
                    CurrentUserManager.RolesId.Contains(m.Parent.ViewRoleId??0));
            }

            return query;
        }

        // GET: odata/MasterDataKeyValues(5)
        //[EnableDynamicQueryableByLog]
        //public SingleResult<MasterDataKeyValue> GetMasterDataKeyValue([FromODataUri] int key)
        //{
        //    return SingleResult.Create(_db.MasterDataKeyValues.Where(md => md.Id == key && (md.IsLeaf == false ||
        //    (LoginUser.RolesId.Contains(md.ViewRoleId.ToString()) && md.IsLeaf))));
        //}

        // PUT: odata/MasterDataKeyValues(5)
        //public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<MasterDataKeyValue> patch)
        //{
        //    Validate(patch.GetEntity());

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    MasterDataKeyValue masterDataKeyValue = await db.MasterDataKeyValues.FindAsync(key);
        //    if (masterDataKeyValue == null)
        //    {
        //        return NotFound();
        //    }

        //    patch.Put(masterDataKeyValue);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!MasterDataKeyValueExists(key))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Updated(masterDataKeyValue);
        //}

        //// POST: odata/MasterDataKeyValues
        //public async Task<IHttpActionResult> Post(MasterDataKeyValue masterDataKeyValue)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.MasterDataKeyValues.Add(masterDataKeyValue);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (MasterDataKeyValueExists(masterDataKeyValue.Id))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Created(masterDataKeyValue);
        //}

        //// PATCH: odata/MasterDataKeyValues(5)
        //[AcceptVerbs("PATCH", "MERGE")]
        //public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<MasterDataKeyValue> patch)
        //{
        //    Validate(patch.GetEntity());

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    MasterDataKeyValue masterDataKeyValue = await db.MasterDataKeyValues.FindAsync(key);
        //    if (masterDataKeyValue == null)
        //    {
        //        return NotFound();
        //    }

        //    patch.Patch(masterDataKeyValue);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!MasterDataKeyValueExists(key))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Updated(masterDataKeyValue);
        //}

        //// DELETE: odata/MasterDataKeyValues(5)
        //public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        //{
        //    MasterDataKeyValue masterDataKeyValue = await db.MasterDataKeyValues.FindAsync(key);
        //    if (masterDataKeyValue == null)
        //    {
        //        return NotFound();
        //    }

        //    db.MasterDataKeyValues.Remove(masterDataKeyValue);
        //    await db.SaveChangesAsync();

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        // GET: odata/MasterDataKeyValues(5)/Childrens
        //[EnableDynamicQueryableByLog]
        //public IQueryable<MasterDataKeyValue> GetChildrens([FromODataUri] int key)
        //{
        //    return _db.MasterDataKeyValues.Where(md => md.Id == key && (md.IsLeaf == false ||
        //    (LoginUser.RolesId.Contains(md.ViewRoleId.ToString()) && md.IsLeaf))).SelectMany(m => m.Childrens);
        //}

        //// GET: odata/MasterDataKeyValues(5)/CreateUser
        //[EnableDynamicQueryableByLog]
        //public SingleResult<UserProfile> GetCreateUser([FromODataUri] int key)
        //{
        //    return SingleResult.Create(_db.MasterDataKeyValues.Where(m => m.Id == key && (m.IsLeaf == false ||
        //    (LoginUser.RolesId.Contains(m.ViewRoleId.ToString()) && m.IsLeaf))).Select(m => m.CreateUser));
        //}

        //// GET: odata/MasterDataKeyValues(5)/Groups
        //[EnableDynamicQueryableByLog]
        //public IQueryable<EntityGroup> GetGroups([FromODataUri] int key)
        //{
        //    return _db.MasterDataKeyValues.Where(m => m.Id == key && (m.IsLeaf == false ||
        //    (LoginUser.RolesId.Contains(m.ViewRoleId.ToString()) && m.IsLeaf))).SelectMany(m => m.Groups);
        //}

        //// GET: odata/MasterDataKeyValues(5)/LocalValues
        //[EnableDynamicQueryableByLog]
        //public IQueryable<MasterDataLocalKeyValue> GetLocalValues([FromODataUri] int key)
        //{
        //    return _db.MasterDataKeyValues.Where(md => md.Id == key && (md.IsLeaf == false ||
        //    (LoginUser.RolesId.Contains(md.ViewRoleId.ToString()) && md.IsLeaf))).SelectMany(m => m.LocalValues);
        //}

        //// GET: odata/MasterDataKeyValues(5)/ModifieUser
        //[EnableDynamicQueryableByLog]
        //public SingleResult<UserProfile> GetModifieUser([FromODataUri] int key)
        //{
        //    return SingleResult.Create(_db.MasterDataKeyValues.Where(m => m.Id == key && (m.IsLeaf == false ||
        //    (LoginUser.RolesId.Contains(m.ViewRoleId.ToString()) && m.IsLeaf))).Select(m => m.ModifieUser));
        //}

        //// GET: odata/MasterDataKeyValues(5)/Parent
        //[EnableDynamicQueryableByLog]
        //public SingleResult<MasterDataKeyValue> GetParent([FromODataUri] int key)
        //{
        //    return SingleResult.Create(_db.MasterDataKeyValues.Where(md => md.Id == key && (md.IsLeaf == false ||
        //    (LoginUser.RolesId.Contains(md.ViewRoleId.ToString()) && md.IsLeaf))).Select(m => m.Parent));
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private bool MasterDataKeyValueExists(int key)
        {
            return _db.MasterDataKeyValues.Count(e => e.Id == key) > 0;
        }
    }
}

using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using KS.Core.GlobalVarioable;
using KS.Core.UI.Attribute;
using KS.Core.UI.Attribute.Odata;
using KS.DataAccess.Contexts;
using KS.Model.ContentManagement;
using KS.WebSiteUI.Controllers.Base;
using KS.DataAccess.Contexts.Base;

namespace KS.WebSiteUI.Controllers.ContentManagement.OpenData.Public
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using KS.Model.ContentManagement;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<MasterDataKeyValue>("MasterDataKeyValuesPublic");
    builder.EntitySet<UserProfile>("Users"); 
    builder.EntitySet<EntityGroup>("EntityGroups"); 
    builder.EntitySet<MasterDataLocalKeyValue>("MasterDataLocalKeyValues"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class MasterDataKeyValuesPublicController : BasePublicODataController
    {
        private readonly IContentManagementContext _db;

        public MasterDataKeyValuesPublicController(IContentManagementContext context)
        {
            _db = context;
        }
        // GET: odata/MasterDataKeyValuesPublic
        [EnableDynamicQueryable]
        public IQueryable<MasterDataKeyValue> GetMasterDataKeyValuesPublic(ODataQueryOptions queryOptions)
        {
            var query = _db.MasterDataKeyValues.Where(md => md.IsLeaf == false ||
            (md.ViewRoleId == (int)Roles.Public && md.IsLeaf));

            if (queryOptions.SelectExpand?.RawExpand != null)
            {
                if (queryOptions.SelectExpand.RawExpand.IndexOf("Parent", StringComparison.Ordinal) != -1)
                    query = query.Where(m => m.Parent.ViewRoleId == (int) Roles.Public);
                //if (queryOptions.SelectExpand.RawExpand.IndexOf("ModifieUser") != -1)
                //    query = query.Where(m => m.ModifieUser.ViewRoleId == (int)Roles.Public);
                //if (queryOptions.SelectExpand.RawExpand.IndexOf("CreateUser") != -1)
                //    query = query.Where(m => m.CreateUser.ViewRoleId == (int)Roles.Public);
            }
            return query;
        }

        // GET: odata/MasterDataKeyValuesPublic(5)
        //[EnableDynamicQueryableByLog]
        //public SingleResult<MasterDataKeyValue> GetMasterDataKeyValuePublic([FromODataUri] int key)
        //{
        //    return SingleResult.Create(_db.MasterDataKeyValues.Where(masterDataKeyValue => masterDataKeyValue.Id == key
        //    && (masterDataKeyValue.IsLeaf == false ||
        //    (masterDataKeyValue.ViewRoleId == (int)Roles.Public && masterDataKeyValue.IsLeaf))));
        //}

        // PUT: odata/MasterDataKeyValuesPublic(5)
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

        //// POST: odata/MasterDataKeyValuesPublic
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

        //// PATCH: odata/MasterDataKeyValuesPublic(5)
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

        //// DELETE: odata/MasterDataKeyValuesPublic(5)
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

        // GET: odata/MasterDataKeyValuesPublic(5)/Childrens
        //[EnableDynamicQueryableByLog]
        //public IQueryable<MasterDataKeyValue> GetChildrens([FromODataUri] int key)
        //{
        //    return _db.MasterDataKeyValues.Where(m => m.Id == key &&
        //    (m.IsLeaf == false ||
        //    (m.ViewRoleId == (int)Roles.Public && m.IsLeaf))).SelectMany(m => m.Childrens);
        //}

        //// GET: odata/MasterDataKeyValuesPublic(5)/CreateUser
        //[EnableDynamicQueryableByLog]
        //public SingleResult<UserProfile> GetCreateUser([FromODataUri] int key)
        //{
        //    return SingleResult.Create(_db.MasterDataKeyValues.Where(m => m.Id == key &&

        //     (m.IsLeaf == false ||
        //    (m.ViewRoleId == (int)Roles.Public && m.IsLeaf))).Select(m => m.CreateUser));
        //}

        //// GET: odata/MasterDataKeyValuesPublic(5)/Groups
        //[EnableDynamicQueryableByLog]
        //public IQueryable<EntityGroup> GetGroups([FromODataUri] int key)
        //{
        //    return _db.MasterDataKeyValues.Where(m => m.Id == key &&

        //     (m.IsLeaf == false ||
        //    (m.ViewRoleId == (int)Roles.Public && m.IsLeaf))).SelectMany(m => m.Groups);
        //}

        //// GET: odata/MasterDataKeyValuesPublic(5)/LocalValues
        //[EnableDynamicQueryableByLog]
        //public IQueryable<MasterDataLocalKeyValue> GetLocalValues([FromODataUri] int key)
        //{
        //    return _db.MasterDataKeyValues.Where(m => m.Id == key &&

        //     (m.IsLeaf == false ||
        //    (m.ViewRoleId == (int)Roles.Public && m.IsLeaf))).SelectMany(m => m.LocalValues);
        //}

        //// GET: odata/MasterDataKeyValuesPublic(5)/ModifieUser
        //[EnableDynamicQueryableByLog]
        //public SingleResult<UserProfile> GetModifieUser([FromODataUri] int key)
        //{
        //    return SingleResult.Create(_db.MasterDataKeyValues.Where(m => m.Id == key &&

        //     (m.IsLeaf == false ||
        //    (m.ViewRoleId == (int)Roles.Public && m.IsLeaf))).Select(m => m.ModifieUser));
        //}

        //// GET: odata/MasterDataKeyValuesPublic(5)/Parent
        //[EnableDynamicQueryableByLog]
        //public SingleResult<MasterDataKeyValue> GetParent([FromODataUri] int key)
        //{
        //    return SingleResult.Create(_db.MasterDataKeyValues.Where(m => m.Id == key &&

        //     (m.IsLeaf == false ||
        //    (m.ViewRoleId == (int)Roles.Public && m.IsLeaf))).Select(m => m.Parent));
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        _db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private bool MasterDataKeyValueExists(int key)
        {
            return _db.MasterDataKeyValues.Count(e => e.Id == key) > 0;
        }
    }
}

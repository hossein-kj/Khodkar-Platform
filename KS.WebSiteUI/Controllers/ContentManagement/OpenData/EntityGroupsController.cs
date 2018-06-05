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
    builder.EntitySet<EntityGroup>("EntityGroups");
    builder.EntitySet<Comment>("Comments"); 
    builder.EntitySet<DynamicEntity>("DynamicEntities"); 
    builder.EntitySet<MasterDataKeyValue>("MasterDataKeyValues"); 
    builder.EntitySet<Link>("Links"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */

    public class EntityGroupsController : BaseAuthorizedODataController
    {
        private readonly IContentManagementContext _db;

        public EntityGroupsController(IContentManagementContext context)
        {
            _db = context;
        }

        // GET: odata/EntityGroups
        [EnableDynamicQueryable]
        public IQueryable<EntityGroup> GetEntityGroups(ODataQueryOptions queryOptions)
        {
            var query = _db.EntityGroups.AsQueryable();
            if (queryOptions.SelectExpand?.RawExpand != null)
            {
                //if (queryOptions.SelectExpand.RawExpand.IndexOf("MasterDataKeyValue", StringComparison.Ordinal) != -1)
                //    query = query.Where(m => m.MasterDataKeyValue.IsLeaf == false ||
                //(CurrentUser.RolesId.Contains(m.MasterDataKeyValue.ViewRoleId.ToString()) && m.MasterDataKeyValue.IsLeaf));
                //if (queryOptions.SelectExpand.RawExpand.IndexOf("Link", StringComparison.Ordinal) != -1)
                //    query = query.Where(m => m.Link.IsLeaf == false ||
                //(CurrentUser.RolesId.Contains(m.Link.ViewRoleId.ToString()) && m.Link.IsLeaf));
                //if (queryOptions.SelectExpand.RawExpand.IndexOf("Comment", StringComparison.Ordinal) != -1)
                //    query = query.Where(m => m.Comment.IsLeaf == false ||
                //(CurrentUser.RolesId.Contains(m.Comment.ViewRoleId.ToString()) && m.Comment.IsLeaf));
                //if (queryOptions.SelectExpand.RawExpand.IndexOf("DynamicEntity", StringComparison.Ordinal) != -1)
                //    query = query.Where(m => m.DynamicEntity.IsLeaf == false ||
                //(CurrentUser.RolesId.Contains(m.DynamicEntity.ViewRoleId.ToString()) && m.DynamicEntity.IsLeaf));

                if (queryOptions.SelectExpand.RawExpand.IndexOf("MasterDataKeyValue", StringComparison.Ordinal) != -1)
                    query = query.Where(m => CurrentUserManager.RolesId.Contains((int)Roles.Admin) ||
                CurrentUserManager.RolesId.Contains(m.MasterDataKeyValue.ViewRoleId ?? 0));
                if (queryOptions.SelectExpand.RawExpand.IndexOf("Link", StringComparison.Ordinal) != -1)
                    query = query.Where(m => CurrentUserManager.RolesId.Contains((int)Roles.Admin) ||
                CurrentUserManager.RolesId.Contains(m.Link.ViewRoleId ?? 0));
                if (queryOptions.SelectExpand.RawExpand.IndexOf("Comment", StringComparison.Ordinal) != -1)
                    query = query.Where(m => CurrentUserManager.RolesId.Contains((int)Roles.Admin) ||
                CurrentUserManager.RolesId.Contains(m.Comment.ViewRoleId ?? 0));
            }
           
            

            return query;
        }

        // GET: odata/EntityGroups(5)
        //[EnableDynamicQueryableByLog]
        //public SingleResult<EntityGroup> GetEntityGroup([FromODataUri] int key)
        //{
        //    return SingleResult.Create(_db.EntityGroups.Where(entityGroup => entityGroup.Id == key));
        //}

        // PUT: odata/EntityGroups(5)
        //public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<EntityGroup> patch)
        //{
        //    Validate(patch.GetEntity());

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    EntityGroup entityGroup = await db.EntityGroups.FindAsync(key);
        //    if (entityGroup == null)
        //    {
        //        return NotFound();
        //    }

        //    patch.Put(entityGroup);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!EntityGroupExists(key))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Updated(entityGroup);
        //}

        //// POST: odata/EntityGroups
        //public async Task<IHttpActionResult> Post(EntityGroup entityGroup)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.EntityGroups.Add(entityGroup);
        //    await db.SaveChangesAsync();

        //    return Created(entityGroup);
        //}

        //// PATCH: odata/EntityGroups(5)
        //[AcceptVerbs("PATCH", "MERGE")]
        //public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<EntityGroup> patch)
        //{
        //    Validate(patch.GetEntity());

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    EntityGroup entityGroup = await db.EntityGroups.FindAsync(key);
        //    if (entityGroup == null)
        //    {
        //        return NotFound();
        //    }

        //    patch.Patch(entityGroup);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!EntityGroupExists(key))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Updated(entityGroup);
        //}

        //// DELETE: odata/EntityGroups(5)
        //public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        //{
        //    EntityGroup entityGroup = await db.EntityGroups.FindAsync(key);
        //    if (entityGroup == null)
        //    {
        //        return NotFound();
        //    }

        //    db.EntityGroups.Remove(entityGroup);
        //    await db.SaveChangesAsync();

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        // GET: odata/EntityGroups(5)/Comment
        //[EnableDynamicQueryableByLog]
        //public SingleResult<Comment> GetComment([FromODataUri] int key)
        //{
        //    return SingleResult.Create(_db.EntityGroups.Where(m => m.Id == key).Select(m => m.Comment));
        //}

        //// GET: odata/EntityGroups(5)/DynamicEntity
        //[EnableDynamicQueryableByLog]
        //public SingleResult<DynamicEntity> GetDynamicEntity([FromODataUri] int key)
        //{
        //    return SingleResult.Create(_db.EntityGroups.Where(m => m.Id == key).Select(m => m.DynamicEntity));
        //}

        //// GET: odata/EntityGroups(5)/DynamicEntityType
        //[EnableDynamicQueryableByLog]
        //public SingleResult<MasterDataKeyValue> GetDynamicEntityType([FromODataUri] int key)
        //{
        //    return SingleResult.Create(_db.EntityGroups.Where(m => m.Id == key).Select(m => m.DynamicEntityType));
        //}

        //// GET: odata/EntityGroups(5)/Group
        //[EnableDynamicQueryableByLog]
        //public SingleResult<MasterDataKeyValue> GetGroup([FromODataUri] int key)
        //{
        //    return SingleResult.Create(_db.EntityGroups.Where(m => m.Id == key).Select(m => m.Group));
        //}

        //// GET: odata/EntityGroups(5)/Link
        //[EnableDynamicQueryableByLog]
        //public SingleResult<Link> GetLink([FromODataUri] int key)
        //{
        //    return SingleResult.Create(_db.EntityGroups.Where(m => m.Id == key).Select(m => m.Link));
        //}

        //// GET: odata/EntityGroups(5)/MasterDataKeyValue
        //[EnableDynamicQueryableByLog]
        //public SingleResult<MasterDataKeyValue> GetMasterDataKeyValue([FromODataUri] int key)
        //{
        //    return SingleResult.Create(_db.EntityGroups.Where(m => m.Id == key).Select(m => m.MasterDataKeyValue));
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private bool EntityGroupExists(int key)
        {
            return _db.EntityGroups.Count(e => e.Id == key) > 0;
        }
    }
}

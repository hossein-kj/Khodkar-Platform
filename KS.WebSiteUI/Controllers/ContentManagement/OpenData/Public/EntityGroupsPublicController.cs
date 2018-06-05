using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using System.Web.Http.OData.Routing;
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
    builder.EntitySet<EntityGroup>("EntityGroupsPublic");
    builder.EntitySet<Comment>("Comments"); 
    builder.EntitySet<DynamicEntity>("DynamicEntities"); 
    builder.EntitySet<MasterDataKeyValue>("MasterDataKeyValues"); 
    builder.EntitySet<Link>("Links"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class EntityGroupsPublicController : BasePublicODataController
    {
        private readonly IContentManagementContext _db;

        public EntityGroupsPublicController(IContentManagementContext context)
        {
            _db = context;
        }
        // GET: odata/EntityGroupsPublic
        [EnableDynamicQueryable]
        public IQueryable<EntityGroup> GetEntityGroupsPublic(ODataQueryOptions queryOptions)
        {
            var query = _db.EntityGroups.AsQueryable();
            if (queryOptions.SelectExpand?.RawExpand != null)
            {
                if (queryOptions.SelectExpand.RawExpand.IndexOf("MasterDataKeyValue", StringComparison.Ordinal) != -1)
                    query = query.Where(md => md.MasterDataKeyValue.IsLeaf == false ||
                                              (md.MasterDataKeyValue.ViewRoleId == (int)Roles.Public &&
                                               md.MasterDataKeyValue.IsLeaf));
                if (queryOptions.SelectExpand.RawExpand.IndexOf("Link", StringComparison.Ordinal) != -1)
                    query = query.Where(md => md.Link.IsLeaf == false ||
                                              (md.Link.ViewRoleId == (int)Roles.Public &&
                                               md.Link.IsLeaf));
                if (queryOptions.SelectExpand.RawExpand.IndexOf("Comment", StringComparison.Ordinal) != -1)
                    query = query.Where(md => md.Comment.IsLeaf == false ||
                                              (md.Comment.ViewRoleId == (int)Roles.Public &&
                                               md.Comment.IsLeaf));

            }

           

            return query;
        }

        // GET: odata/EntityGroupsPublic(5)
        //[EnableDynamicQueryableByLog]
        //public SingleResult<EntityGroup> GetEntityGroup([FromODataUri] int key)
        //{
        //    return SingleResult.Create(_db.EntityGroups.Where(entityGroup => entityGroup.Id == key));
        //}

        // PUT: odata/EntityGroupsPublic(5)
        //public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<EntityGroup> patch)
        //{
        //    Validate(patch.GetEntity());

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    EntityGroup entityGroup = await _db.EntityGroups.FindAsync(key);
        //    if (entityGroup == null)
        //    {
        //        return NotFound();
        //    }

        //    patch.Put(entityGroup);

        //    try
        //    {
        //        await _db.SaveChangesAsync();
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

        //// POST: odata/EntityGroupsPublic
        //public async Task<IHttpActionResult> Post(EntityGroup entityGroup)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _db.EntityGroups.Add(entityGroup);
        //    await _db.SaveChangesAsync();

        //    return Created(entityGroup);
        //}

        //// PATCH: odata/EntityGroupsPublic(5)
        //[AcceptVerbs("PATCH", "MERGE")]
        //public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<EntityGroup> patch)
        //{
        //    Validate(patch.GetEntity());

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    EntityGroup entityGroup = await _db.EntityGroups.FindAsync(key);
        //    if (entityGroup == null)
        //    {
        //        return NotFound();
        //    }

        //    patch.Patch(entityGroup);

        //    try
        //    {
        //        await _db.SaveChangesAsync();
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

        //// DELETE: odata/EntityGroupsPublic(5)
        //public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        //{
        //    EntityGroup entityGroup = await _db.EntityGroups.FindAsync(key);
        //    if (entityGroup == null)
        //    {
        //        return NotFound();
        //    }

        //    _db.EntityGroups.Remove(entityGroup);
        //    await _db.SaveChangesAsync();

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        // GET: odata/EntityGroupsPublic(5)/Comment
        //[EnableDynamicQueryableByLog]
        //public SingleResult<Comment> GetComment([FromODataUri] int key)
        //{
        //    return SingleResult.Create(_db.EntityGroups.Where(m => m.Id == key).Select(m => m.Comment));
        //}

        //// GET: odata/EntityGroupsPublic(5)/DynamicEntity
        //[EnableDynamicQueryableByLog]
        //public SingleResult<DynamicEntity> GetDynamicEntity([FromODataUri] int key)
        //{
        //    return SingleResult.Create(_db.EntityGroups.Where(m => m.Id == key).Select(m => m.DynamicEntity));
        //}

        //// GET: odata/EntityGroupsPublic(5)/DynamicEntityType
        //[EnableDynamicQueryableByLog]
        //public SingleResult<MasterDataKeyValue> GetDynamicEntityType([FromODataUri] int key)
        //{
        //    return SingleResult.Create(_db.EntityGroups.Where(m => m.Id == key).Select(m => m.DynamicEntityType));
        //}

        //// GET: odata/EntityGroupsPublic(5)/Group
        //[EnableDynamicQueryableByLog]
        //public SingleResult<MasterDataKeyValue> GetGroup([FromODataUri] int key)
        //{
        //    return SingleResult.Create(_db.EntityGroups.Where(m => m.Id == key).Select(m => m.Group));
        //}

        //// GET: odata/EntityGroupsPublic(5)/Link
        //[EnableDynamicQueryableByLog]
        //public SingleResult<Link> GetLink([FromODataUri] int key)
        //{
        //    return SingleResult.Create(_db.EntityGroups.Where(m => m.Id == key).Select(m => m.Link));
        //}

        //// GET: odata/EntityGroupsPublic(5)/MasterDataKeyValue
        //[EnableDynamicQueryableByLog]
        //public SingleResult<MasterDataKeyValue> GetMasterDataKeyValue([FromODataUri] int key)
        //{
        //    return SingleResult.Create(_db.EntityGroups.Where(m => m.Id == key).Select(m => m.MasterDataKeyValue));
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        _db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private bool EntityGroupExists(int key)
        {
            return _db.EntityGroups.Count(e => e.Id == key) > 0;
        }
    }
}

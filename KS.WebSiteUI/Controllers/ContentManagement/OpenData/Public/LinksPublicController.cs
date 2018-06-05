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
    builder.EntitySet<Link>("LinksPublic");
    builder.EntitySet<UserProfile>("Users"); 
    builder.EntitySet<EntityFilePath>("EntityFilePaths"); 
    builder.EntitySet<EntityFile>("EntityFiles"); 
    builder.EntitySet<EntityGroup>("EntityGroups"); 
    builder.EntitySet<EntityMasterDataKeyValue>("EntityMasterDataKeyValues"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class LinksPublicController : BasePublicODataController
    {
        private readonly IContentManagementContext _db;

        public LinksPublicController(IContentManagementContext context)
        {
            _db = context;
        }

        // GET: odata/LinksPublic
        [EnableDynamicQueryable]
        public IQueryable<Link> GetLinksPublic(ODataQueryOptions queryOptions)
        {
            var query = _db.Links.Where(md => md.IsLeaf == false ||
           (md.ViewRoleId == (int)Roles.Public && md.IsLeaf));

            if (queryOptions.SelectExpand?.RawExpand != null)
            {
                if (queryOptions.SelectExpand.RawExpand.IndexOf("Parent") != -1)
                    query = query.Where(m => m.Parent.ViewRoleId == (int)Roles.Public);
                //if (queryOptions.SelectExpand.RawExpand.IndexOf("ModifieUser") != -1)
                //    query = query.Where(m => m.ModifieUser.ViewRoleId == (int)Roles.Public);
                //if (queryOptions.SelectExpand.RawExpand.IndexOf("CreateUser") != -1)
                //    query = query.Where(m => m.CreateUser.ViewRoleId == (int)Roles.Public);

            }

            return query;
        }

        // GET: odata/LinksPublic(5)
        //[EnableDynamicQueryableByLog]
        //public SingleResult<Link> GetLink([FromODataUri] int key)
        //{
        //    return SingleResult.Create(_db.Links.Where(link => link.Id == key));
        //}

        // PUT: odata/LinksPublic(5)
        //public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Link> patch)
        //{
        //    Validate(patch.GetEntity());

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    Link link = await _db.Links.FindAsync(key);
        //    if (link == null)
        //    {
        //        return NotFound();
        //    }

        //    patch.Put(link);

        //    try
        //    {
        //        await _db.SaveChangesAsync();
        //    }
        //    catch (_dbUpdateConcurrencyException)
        //    {
        //        if (!LinkExists(key))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Updated(link);
        //}

        //// POST: odata/LinksPublic
        //public async Task<IHttpActionResult> Post(Link link)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _db.Links.Add(link);
        //    await _db.SaveChangesAsync();

        //    return Created(link);
        //}

        //// PATCH: odata/LinksPublic(5)
        //[AcceptVerbs("PATCH", "MERGE")]
        //public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Link> patch)
        //{
        //    Validate(patch.GetEntity());

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    Link link = await _db.Links.FindAsync(key);
        //    if (link == null)
        //    {
        //        return NotFound();
        //    }

        //    patch.Patch(link);

        //    try
        //    {
        //        await _db.SaveChangesAsync();
        //    }
        //    catch (_dbUpdateConcurrencyException)
        //    {
        //        if (!LinkExists(key))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Updated(link);
        //}

        //// DELETE: odata/LinksPublic(5)
        //public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        //{
        //    Link link = await _db.Links.FindAsync(key);
        //    if (link == null)
        //    {
        //        return NotFound();
        //    }

        //    _db.Links.Remove(link);
        //    await _db.SaveChangesAsync();

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        // GET: odata/LinksPublic(5)/Childrens
        //[EnableDynamicQueryableByLog]
        //public IQueryable<Link> GetChildrens([FromODataUri] int key)
        //{
        //    return _db.Links.Where(m => m.Id == key).SelectMany(m => m.Childrens);
        //}

        //// GET: odata/LinksPublic(5)/CreateUser
        //[EnableDynamicQueryableByLog]
        //public SingleResult<UserProfile> GetCreateUser([FromODataUri] int key)
        //{
        //    return SingleResult.Create(_db.Links.Where(m => m.Id == key).Select(m => m.CreateUser));
        //}

        //// GET: odata/LinksPublic(5)/FilePaths
        //[EnableDynamicQueryableByLog]
        //public IQueryable<EntityFilePath> GetFilePaths([FromODataUri] int key)
        //{
        //    return _db.Links.Where(m => m.Id == key).SelectMany(m => m.FilePaths);
        //}

        //// GET: odata/LinksPublic(5)/Files
        //[EnableDynamicQueryableByLog]
        //public IQueryable<EntityFile> GetFiles([FromODataUri] int key)
        //{
        //    return _db.Links.Where(m => m.Id == key).SelectMany(m => m.Files);
        //}

        //// GET: odata/LinksPublic(5)/Groups
        //[EnableDynamicQueryableByLog]
        //public IQueryable<EntityGroup> GetGroups([FromODataUri] int key)
        //{
        //    return _db.Links.Where(m => m.Id == key).SelectMany(m => m.Groups);
        //}

        //// GET: odata/LinksPublic(5)/KeyValues
        //[EnableDynamicQueryableByLog]
        //public IQueryable<EntityMasterDataKeyValue> GetKeyValues([FromODataUri] int key)
        //{
        //    return _db.Links.Where(m => m.Id == key).SelectMany(m => m.KeyValues);
        //}

        //// GET: odata/LinksPublic(5)/ModifieUser
        //[EnableDynamicQueryableByLog]
        //public SingleResult<UserProfile> GetModifieUser([FromODataUri] int key)
        //{
        //    return SingleResult.Create(_db.Links.Where(m => m.Id == key).Select(m => m.ModifieUser));
        //}

        //// GET: odata/LinksPublic(5)/Parent
        //[EnableDynamicQueryableByLog]
        //public SingleResult<Link> GetParent([FromODataUri] int key)
        //{
        //    return SingleResult.Create(_db.Links.Where(m => m.Id == key).Select(m => m.Parent));
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        _db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private bool LinkExists(int key)
        {
            return _db.Links.Count(e => e.Id == key) > 0;
        }
    }
}

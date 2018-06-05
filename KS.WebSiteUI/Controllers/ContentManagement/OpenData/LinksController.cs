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
    builder.EntitySet<Link>("Links");
    builder.EntitySet<UserProfile>("Users"); 
    builder.EntitySet<EntityFilePath>("EntityFilePaths"); 
    builder.EntitySet<EntityFile>("EntityFiles"); 
    builder.EntitySet<EntityGroup>("EntityGroups"); 
    builder.EntitySet<EntityMasterDataKeyValue>("EntityMasterDataKeyValues"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */

    public class LinksController : BaseAuthorizedODataController
    {
        private readonly IContentManagementContext _db;

        public LinksController(IContentManagementContext context)
        {
            _db = context;
        }
        // GET: odata/Links
        [EnableDynamicQueryable]
        public IQueryable<Link> GetLinks(ODataQueryOptions queryOptions)
        {
            //var query = _db.Links.Where(md => md.IsLeaf == false ||
            //(CurrentUser.RolesId.Contains(md.ViewRoleId.ToString()) && md.IsLeaf));


            //if (queryOptions.SelectExpand?.RawExpand != null)
            //{
            //    if (queryOptions.SelectExpand.RawExpand.IndexOf("Parent", StringComparison.Ordinal) != -1)
            //        query = query.Where(m => CurrentUser.RolesId.Contains(m.Parent.ViewRoleId.ToString()));
            //    //if (queryOptions.SelectExpand.RawExpand.IndexOf("ModifieUser") != -1)
            //    //    query = query.Where(m => LoginUser.RolesId.Contains(m.ModifieUser.ViewRoleId.ToString()));
            //    //if (queryOptions.SelectExpand.RawExpand.IndexOf("CreateUser") != -1)
            //    query = query.Where(m => CurrentUser.RolesId.Contains(m.CreateUser.ViewRoleId.ToString()));

            //}


            var query = _db.Links.Where(md => CurrentUserManager.RolesId.Contains((int)Roles.Admin) ||
                CurrentUserManager.RolesId.Contains(md.ViewRoleId??0));


            if (queryOptions.SelectExpand?.RawExpand != null)
            {
                if (queryOptions.SelectExpand.RawExpand.IndexOf("Parent", StringComparison.Ordinal) != -1)
                    query = query.Where(m => CurrentUserManager.RolesId.Contains((int)Roles.Admin) || CurrentUserManager.RolesId.Contains(m.Parent.ViewRoleId??0));
                //if (queryOptions.SelectExpand.RawExpand.IndexOf("ModifieUser") != -1)
                //    query = query.Where(m => LoginUser.RolesId.Contains(m.ModifieUser.ViewRoleId.ToString()));
                //if (queryOptions.SelectExpand.RawExpand.IndexOf("CreateUser") != -1)
                //query = query.Where(m => CurrentUser.RolesId.Contains(((int)Roles.Admin).ToString()) ||
                //CurrentUser.RolesId.Contains(m.CreateUser.ViewRoleId.ToString()));

            }

            return query;
        }

        // GET: odata/Links(5)
        //[EnableDynamicQueryableByLog]
        //public SingleResult<Link> GetLink([FromODataUri] int key)
        //{
        //    return SingleResult.Create(_db.Links.Where(link => link.Id == key));
        //}

        // PUT: odata/Links(5)
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

        //// POST: odata/Links
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

        //// PATCH: odata/Links(5)
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

        //// DELETE: odata/Links(5)
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

        // GET: odata/Links(5)/Childrens
        //[EnableDynamicQueryableByLog]
        //public IQueryable<Link> GetChildrens([FromODataUri] int key)
        //{
        //    return _db.Links.Where(m => m.Id == key).SelectMany(m => m.Childrens);
        //}

        //// GET: odata/Links(5)/CreateUser
        //[EnableDynamicQueryableByLog]
        //public SingleResult<UserProfile> GetCreateUser([FromODataUri] int key)
        //{
        //    return SingleResult.Create(_db.Links.Where(m => m.Id == key).Select(m => m.CreateUser));
        //}

        //// GET: odata/Links(5)/FilePaths
        //[EnableDynamicQueryableByLog]
        //public IQueryable<EntityFilePath> GetFilePaths([FromODataUri] int key)
        //{
        //    return _db.Links.Where(m => m.Id == key).SelectMany(m => m.FilePaths);
        //}

        //// GET: odata/Links(5)/Files
        //[EnableDynamicQueryableByLog]
        //public IQueryable<EntityFile> GetFiles([FromODataUri] int key)
        //{
        //    return _db.Links.Where(m => m.Id == key).SelectMany(m => m.Files);
        //}

        //// GET: odata/Links(5)/Groups
        //[EnableDynamicQueryableByLog]
        //public IQueryable<EntityGroup> GetGroups([FromODataUri] int key)
        //{
        //    return _db.Links.Where(m => m.Id == key).SelectMany(m => m.Groups);
        //}

        //// GET: odata/Links(5)/KeyValues
        //[EnableDynamicQueryableByLog]
        //public IQueryable<EntityMasterDataKeyValue> GetKeyValues([FromODataUri] int key)
        //{
        //    return _db.Links.Where(m => m.Id == key).SelectMany(m => m.KeyValues);
        //}

        //// GET: odata/Links(5)/ModifieUser
        //[EnableDynamicQueryableByLog]
        //public SingleResult<UserProfile> GetModifieUser([FromODataUri] int key)
        //{
        //    return SingleResult.Create(_db.Links.Where(m => m.Id == key).Select(m => m.ModifieUser));
        //}

        //// GET: odata/Links(5)/Parent
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


using System.Linq;
using KS.DataAccess.Contexts;
using KS.Model.ContentManagement;
using System.Web.Http.OData.Query;
using KS.Core.GlobalVarioable;
using KS.Core.Security;
using KS.Core.UI.Attribute.Odata;
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
    builder.EntitySet<LocalFilePath>("LocalFilePaths");
    builder.EntitySet<UserProfile>("Users"); 
    builder.EntitySet<FilePath>("FilePaths"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
 
    public class LocalFilePathsController : BaseAuthorizedODataController
    {
        private readonly IContentManagementContext _db;

        public LocalFilePathsController(IContentManagementContext context)
        {
            _db = context;
        }

        // GET: odata/LocalFilePaths
        [EnableDynamicQueryable]
        public IQueryable<LocalFilePath> GetLocalFilePaths(ODataQueryOptions queryOptions)
        {
            var query = _db.LocalFilePaths.Where(m => CurrentUserManager.RolesId.Contains((int)Roles.Admin) ||
           CurrentUserManager.RolesId.Contains(m.FilePath.ViewRoleId??0));
            return query;
        }

        //// GET: odata/LocalFilePaths(5)
        //[EnableDynamicQueryableByLog]
        //public SingleResult<LocalFilePath> GetLocalFilePath([FromODataUri] int key)
        //{
        //    return SingleResult.Create(db.LocalFilePaths.Where(localFilePath => localFilePath.Id == key));
        //}

        //// PUT: odata/LocalFilePaths(5)
        //public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<LocalFilePath> patch)
        //{
        //    Validate(patch.GetEntity());

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    LocalFilePath localFilePath = await db.LocalFilePaths.FindAsync(key);
        //    if (localFilePath == null)
        //    {
        //        return NotFound();
        //    }

        //    patch.Put(localFilePath);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!LocalFilePathExists(key))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Updated(localFilePath);
        //}

        //// POST: odata/LocalFilePaths
        //public async Task<IHttpActionResult> Post(LocalFilePath localFilePath)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.LocalFilePaths.Add(localFilePath);
        //    await db.SaveChangesAsync();

        //    return Created(localFilePath);
        //}

        //// PATCH: odata/LocalFilePaths(5)
        //[AcceptVerbs("PATCH", "MERGE")]
        //public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<LocalFilePath> patch)
        //{
        //    Validate(patch.GetEntity());

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    LocalFilePath localFilePath = await db.LocalFilePaths.FindAsync(key);
        //    if (localFilePath == null)
        //    {
        //        return NotFound();
        //    }

        //    patch.Patch(localFilePath);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!LocalFilePathExists(key))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Updated(localFilePath);
        //}

        //// DELETE: odata/LocalFilePaths(5)
        //public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        //{
        //    LocalFilePath localFilePath = await db.LocalFilePaths.FindAsync(key);
        //    if (localFilePath == null)
        //    {
        //        return NotFound();
        //    }

        //    db.LocalFilePaths.Remove(localFilePath);
        //    await db.SaveChangesAsync();

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// GET: odata/LocalFilePaths(5)/CreateUser
        //[EnableDynamicQueryableByLog]
        //public SingleResult<UserProfile> GetCreateUser([FromODataUri] int key)
        //{
        //    return SingleResult.Create(db.LocalFilePaths.Where(m => m.Id == key).Select(m => m.CreateUser));
        //}

        //// GET: odata/LocalFilePaths(5)/FilePath
        //[EnableDynamicQueryableByLog]
        //public SingleResult<FilePath> GetFilePath([FromODataUri] int key)
        //{
        //    return SingleResult.Create(db.LocalFilePaths.Where(m => m.Id == key).Select(m => m.FilePath));
        //}

        //// GET: odata/LocalFilePaths(5)/ModifieUser
        //[EnableDynamicQueryableByLog]
        //public SingleResult<UserProfile> GetModifieUser([FromODataUri] int key)
        //{
        //    return SingleResult.Create(db.LocalFilePaths.Where(m => m.Id == key).Select(m => m.ModifieUser));
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private bool LocalFilePathExists(int key)
        {
            return _db.LocalFilePaths.Count(e => e.Id == key) > 0;
        }
    }
}

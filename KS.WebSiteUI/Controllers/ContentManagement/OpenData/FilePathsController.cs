
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
    builder.EntitySet<FilePath>("FilePaths");
    builder.EntitySet<UserProfile>("Users"); 
    builder.EntitySet<EntityMasterDataKeyValue>("EntityMasterDataKeyValues"); 
    builder.EntitySet<LocalFilePath>("LocalFilePaths"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
 
    public class FilePathsController : BaseAuthorizedODataController
    {
        private readonly IContentManagementContext _db;

        public FilePathsController(IContentManagementContext context)
        {
            _db = context;
        }

        // GET: odata/FilePaths
        [EnableDynamicQueryable]
        public IQueryable<FilePath> GetFilePaths(ODataQueryOptions queryOptions)
        {
            //return _db.FilePaths;
            var query = _db.FilePaths.Where(md => CurrentUserManager.RolesId.Contains(((int)Roles.Admin)) ||
            CurrentUserManager.RolesId.Contains(md.ViewRoleId??0));

            return query;
        }

        //// GET: odata/FilePaths(5)
        //[EnableDynamicQueryableByLog]
        //public SingleResult<FilePath> GetFilePath([FromODataUri] int key)
        //{
        //    return SingleResult.Create(db.FilePaths.Where(filePath => filePath.Id == key));
        //}

        //// PUT: odata/FilePaths(5)
        //public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<FilePath> patch)
        //{
        //    Validate(patch.GetEntity());

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    FilePath filePath = await db.FilePaths.FindAsync(key);
        //    if (filePath == null)
        //    {
        //        return NotFound();
        //    }

        //    patch.Put(filePath);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!FilePathExists(key))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Updated(filePath);
        //}

        //// POST: odata/FilePaths
        //public async Task<IHttpActionResult> Post(FilePath filePath)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.FilePaths.Add(filePath);
        //    await db.SaveChangesAsync();

        //    return Created(filePath);
        //}

        //// PATCH: odata/FilePaths(5)
        //[AcceptVerbs("PATCH", "MERGE")]
        //public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<FilePath> patch)
        //{
        //    Validate(patch.GetEntity());

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    FilePath filePath = await db.FilePaths.FindAsync(key);
        //    if (filePath == null)
        //    {
        //        return NotFound();
        //    }

        //    patch.Patch(filePath);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!FilePathExists(key))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Updated(filePath);
        //}

        //// DELETE: odata/FilePaths(5)
        //public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        //{
        //    FilePath filePath = await db.FilePaths.FindAsync(key);
        //    if (filePath == null)
        //    {
        //        return NotFound();
        //    }

        //    db.FilePaths.Remove(filePath);
        //    await db.SaveChangesAsync();

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// GET: odata/FilePaths(5)/CreateUser
        //[EnableDynamicQueryableByLog]
        //public SingleResult<UserProfile> GetCreateUser([FromODataUri] int key)
        //{
        //    return SingleResult.Create(db.FilePaths.Where(m => m.Id == key).Select(m => m.CreateUser));
        //}

        //// GET: odata/FilePaths(5)/KeyValues
        //[EnableDynamicQueryableByLog]
        //public IQueryable<EntityMasterDataKeyValue> GetKeyValues([FromODataUri] int key)
        //{
        //    return db.FilePaths.Where(m => m.Id == key).SelectMany(m => m.KeyValues);
        //}

        //// GET: odata/FilePaths(5)/LocalFilePaths
        //[EnableDynamicQueryableByLog]
        //public IQueryable<LocalFilePath> GetLocalFilePaths([FromODataUri] int key)
        //{
        //    return db.FilePaths.Where(m => m.Id == key).SelectMany(m => m.LocalFilePaths);
        //}

        //// GET: odata/FilePaths(5)/ModifieUser
        //[EnableDynamicQueryableByLog]
        //public SingleResult<UserProfile> GetModifieUser([FromODataUri] int key)
        //{
        //    return SingleResult.Create(db.FilePaths.Where(m => m.Id == key).Select(m => m.ModifieUser));
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private bool FilePathExists(int key)
        {
            return _db.FilePaths.Count(e => e.Id == key) > 0;
        }
    }
}

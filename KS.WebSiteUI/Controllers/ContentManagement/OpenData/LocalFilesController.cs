
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
    builder.EntitySet<LocalFile>("LocalFiles");
    builder.EntitySet<UserProfile>("Users"); 
    builder.EntitySet<File>("Files"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */

    public class LocalFilesController : BaseAuthorizedODataController
    {
        private readonly IContentManagementContext _db;

        public LocalFilesController(IContentManagementContext context)
        {
            _db = context;
        }

        // GET: odata/LocalFiles
        [EnableDynamicQueryable]
        public IQueryable<LocalFile> GetLocalFiles(ODataQueryOptions queryOptions)
        {
            var query = _db.LocalFiles.Where(m => CurrentUserManager.RolesId.Contains((int)Roles.Admin) ||
            CurrentUserManager.RolesId.Contains(m.File.ViewRoleId??0));
            return query;
            //return db.LocalFiles;
        }

        //// GET: odata/LocalFiles(5)
        //[EnableDynamicQueryableByLog]
        //public SingleResult<LocalFile> GetLocalFile([FromODataUri] int key)
        //{
        //    return SingleResult.Create(db.LocalFiles.Where(localFile => localFile.Id == key));
        //}

        //// PUT: odata/LocalFiles(5)
        //public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<LocalFile> patch)
        //{
        //    Validate(patch.GetEntity());

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    LocalFile localFile = await db.LocalFiles.FindAsync(key);
        //    if (localFile == null)
        //    {
        //        return NotFound();
        //    }

        //    patch.Put(localFile);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!LocalFileExists(key))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Updated(localFile);
        //}

        //// POST: odata/LocalFiles
        //public async Task<IHttpActionResult> Post(LocalFile localFile)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.LocalFiles.Add(localFile);
        //    await db.SaveChangesAsync();

        //    return Created(localFile);
        //}

        //// PATCH: odata/LocalFiles(5)
        //[AcceptVerbs("PATCH", "MERGE")]
        //public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<LocalFile> patch)
        //{
        //    Validate(patch.GetEntity());

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    LocalFile localFile = await db.LocalFiles.FindAsync(key);
        //    if (localFile == null)
        //    {
        //        return NotFound();
        //    }

        //    patch.Patch(localFile);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!LocalFileExists(key))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Updated(localFile);
        //}

        //// DELETE: odata/LocalFiles(5)
        //public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        //{
        //    LocalFile localFile = await db.LocalFiles.FindAsync(key);
        //    if (localFile == null)
        //    {
        //        return NotFound();
        //    }

        //    db.LocalFiles.Remove(localFile);
        //    await db.SaveChangesAsync();

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// GET: odata/LocalFiles(5)/CreateUser
        //[EnableDynamicQueryableByLog]
        //public SingleResult<UserProfile> GetCreateUser([FromODataUri] int key)
        //{
        //    return SingleResult.Create(db.LocalFiles.Where(m => m.Id == key).Select(m => m.CreateUser));
        //}

        //// GET: odata/LocalFiles(5)/File
        //[EnableDynamicQueryableByLog]
        //public SingleResult<File> GetFile([FromODataUri] int key)
        //{
        //    return SingleResult.Create(db.LocalFiles.Where(m => m.Id == key).Select(m => m.File));
        //}

        //// GET: odata/LocalFiles(5)/ModifieUser
        //[EnableDynamicQueryableByLog]
        //public SingleResult<UserProfile> GetModifieUser([FromODataUri] int key)
        //{
        //    return SingleResult.Create(db.LocalFiles.Where(m => m.Id == key).Select(m => m.ModifieUser));
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private bool LocalFileExists(int key)
        {
            return _db.LocalFiles.Count(e => e.Id == key) > 0;
        }
    }
}

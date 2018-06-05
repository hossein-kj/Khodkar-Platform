
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
    builder.EntitySet<File>("Files");
    builder.EntitySet<UserProfile>("Users"); 
    builder.EntitySet<EntityMasterDataKeyValue>("EntityMasterDataKeyValues"); 
    builder.EntitySet<LocalFile>("LocalFiles"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
 
    public class FilesController : BaseAuthorizedODataController
    {
        private readonly IContentManagementContext _db;

        public FilesController(IContentManagementContext context)
        {
            _db = context;
        }

        // GET: odata/Files
        [EnableDynamicQueryable]
        public IQueryable<File> GetFiles(ODataQueryOptions queryOptions)
        {
            var query = _db.Files.Where(fl => CurrentUserManager.RolesId.Contains((int)Roles.Admin) ||
            CurrentUserManager.RolesId.Contains(fl.ViewRoleId??0));

            return query;
            //return _db.Files;
        }

        //// GET: odata/Files(5)
        //[EnableDynamicQueryableByLog]
        //public SingleResult<File> GetFile([FromODataUri] int key)
        //{
        //    return SingleResult.Create(db.Files.Where(file => file.Id == key));
        //}

        //// PUT: odata/Files(5)
        //public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<File> patch)
        //{
        //    Validate(patch.GetEntity());

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    File file = await db.Files.FindAsync(key);
        //    if (file == null)
        //    {
        //        return NotFound();
        //    }

        //    patch.Put(file);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!FileExists(key))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Updated(file);
        //}

        //// POST: odata/Files
        //public async Task<IHttpActionResult> Post(File file)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Files.Add(file);
        //    await db.SaveChangesAsync();

        //    return Created(file);
        //}

        //// PATCH: odata/Files(5)
        //[AcceptVerbs("PATCH", "MERGE")]
        //public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<File> patch)
        //{
        //    Validate(patch.GetEntity());

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    File file = await db.Files.FindAsync(key);
        //    if (file == null)
        //    {
        //        return NotFound();
        //    }

        //    patch.Patch(file);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!FileExists(key))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Updated(file);
        //}

        //// DELETE: odata/Files(5)
        //public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        //{
        //    File file = await db.Files.FindAsync(key);
        //    if (file == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Files.Remove(file);
        //    await db.SaveChangesAsync();

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// GET: odata/Files(5)/CreateUser
        //[EnableDynamicQueryableByLog]
        //public SingleResult<UserProfile> GetCreateUser([FromODataUri] int key)
        //{
        //    return SingleResult.Create(db.Files.Where(m => m.Id == key).Select(m => m.CreateUser));
        //}

        //// GET: odata/Files(5)/KeyValues
        //[EnableDynamicQueryableByLog]
        //public IQueryable<EntityMasterDataKeyValue> GetKeyValues([FromODataUri] int key)
        //{
        //    return db.Files.Where(m => m.Id == key).SelectMany(m => m.KeyValues);
        //}

        //// GET: odata/Files(5)/LocalFiles
        //[EnableDynamicQueryableByLog]
        //public IQueryable<LocalFile> GetLocalFiles([FromODataUri] int key)
        //{
        //    return db.Files.Where(m => m.Id == key).SelectMany(m => m.LocalFiles);
        //}

        //// GET: odata/Files(5)/ModifieUser
        //[EnableDynamicQueryableByLog]
        //public SingleResult<UserProfile> GetModifieUser([FromODataUri] int key)
        //{
        //    return SingleResult.Create(db.Files.Where(m => m.Id == key).Select(m => m.ModifieUser));
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private bool FileExists(int key)
        {
            return _db.Files.Count(e => e.Id == key) > 0;
        }
    }
}

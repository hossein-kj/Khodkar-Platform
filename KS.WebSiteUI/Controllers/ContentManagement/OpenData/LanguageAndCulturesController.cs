
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
    builder.EntitySet<LanguageAndCulture>("LanguageAndCultures");
    builder.EntitySet<UserProfile>("Users"); 
    builder.EntitySet<FilePath>("FilePaths"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
   
    public class LanguageAndCulturesController : BaseAuthorizedODataController
    {
        private readonly IContentManagementContext _db;

        public LanguageAndCulturesController(IContentManagementContext context)
        {
            _db = context;
        }

        // GET: odata/LanguageAndCultures
        [EnableDynamicQueryable]
        public IQueryable<LanguageAndCulture> GetLanguageAndCultures(ODataQueryOptions queryOptions)
        {
            //return _db.LanguageAndCultures;
            var query = _db.LanguageAndCultures.Where(md => CurrentUserManager.RolesId.Contains((int)Roles.Admin) ||
            CurrentUserManager.RolesId.Contains(md.ViewRoleId??0));

            return query;
        }

        //// GET: odata/LanguageAndCultures(5)
        //[EnableDynamicQueryableByLog]
        //public SingleResult<LanguageAndCulture> GetLanguageAndCulture([FromODataUri] int key)
        //{
        //    return SingleResult.Create(db.LanguageAndCultures.Where(languageAndCulture => languageAndCulture.Id == key));
        //}

        //// PUT: odata/LanguageAndCultures(5)
        //public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<LanguageAndCulture> patch)
        //{
        //    Validate(patch.GetEntity());

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    LanguageAndCulture languageAndCulture = await db.LanguageAndCultures.FindAsync(key);
        //    if (languageAndCulture == null)
        //    {
        //        return NotFound();
        //    }

        //    patch.Put(languageAndCulture);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!LanguageAndCultureExists(key))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Updated(languageAndCulture);
        //}

        //// POST: odata/LanguageAndCultures
        //public async Task<IHttpActionResult> Post(LanguageAndCulture languageAndCulture)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.LanguageAndCultures.Add(languageAndCulture);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (LanguageAndCultureExists(languageAndCulture.Id))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Created(languageAndCulture);
        //}

        //// PATCH: odata/LanguageAndCultures(5)
        //[AcceptVerbs("PATCH", "MERGE")]
        //public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<LanguageAndCulture> patch)
        //{
        //    Validate(patch.GetEntity());

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    LanguageAndCulture languageAndCulture = await db.LanguageAndCultures.FindAsync(key);
        //    if (languageAndCulture == null)
        //    {
        //        return NotFound();
        //    }

        //    patch.Patch(languageAndCulture);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!LanguageAndCultureExists(key))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Updated(languageAndCulture);
        //}

        //// DELETE: odata/LanguageAndCultures(5)
        //public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        //{
        //    LanguageAndCulture languageAndCulture = await db.LanguageAndCultures.FindAsync(key);
        //    if (languageAndCulture == null)
        //    {
        //        return NotFound();
        //    }

        //    db.LanguageAndCultures.Remove(languageAndCulture);
        //    await db.SaveChangesAsync();

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// GET: odata/LanguageAndCultures(5)/CreateUser
        //[EnableDynamicQueryableByLog]
        //public SingleResult<UserProfile> GetCreateUser([FromODataUri] int key)
        //{
        //    return SingleResult.Create(db.LanguageAndCultures.Where(m => m.Id == key).Select(m => m.CreateUser));
        //}

        //// GET: odata/LanguageAndCultures(5)/Flag
        //[EnableDynamicQueryableByLog]
        //public SingleResult<FilePath> GetFlag([FromODataUri] int key)
        //{
        //    return SingleResult.Create(db.LanguageAndCultures.Where(m => m.Id == key).Select(m => m.Flag));
        //}

        //// GET: odata/LanguageAndCultures(5)/ModifieUser
        //[EnableDynamicQueryableByLog]
        //public SingleResult<UserProfile> GetModifieUser([FromODataUri] int key)
        //{
        //    return SingleResult.Create(db.LanguageAndCultures.Where(m => m.Id == key).Select(m => m.ModifieUser));
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private bool LanguageAndCultureExists(int key)
        {
            return _db.LanguageAndCultures.Count(e => e.Id == key) > 0;
        }
    }
}

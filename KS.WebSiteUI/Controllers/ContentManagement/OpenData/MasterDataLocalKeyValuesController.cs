using System.Linq;
using System.Web.Http.OData.Query;
using KS.Core.GlobalVarioable;
using KS.Core.Security;
using KS.DataAccess.Contexts;
using KS.Model.ContentManagement;
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
    builder.EntitySet<MasterDataLocalKeyValue>("MasterDataLocalKeyValues");
    builder.EntitySet<MasterDataKeyValue>("MasterDataKeyValues"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
   
    public class MasterDataLocalKeyValuesController : BaseAuthorizedODataController
    {
        private readonly IContentManagementContext _db;

        public MasterDataLocalKeyValuesController(IContentManagementContext context)
        {
            _db = context;
        }
        // GET: odata/MasterDataLocalKeyValues
        [EnableDynamicQueryable]
        public IQueryable<MasterDataLocalKeyValue> GetMasterDataLocalKeyValues(ODataQueryOptions queryOptions, string materDataKeyValueType = null)
        {
            //if (materDataKeyValueType != null)
            //    materDataKeyValueType = materDataKeyValueType;



            //switch (queryOptions.SelectExpand.RawExpand)
            //{
            //    case "MasterDataKeyValue":
            //        return _db.MasterDataLocalKeyValues.Where(m => m.MasterDataKeyValue.IsLeaf == false ||
            //                                                       (LoginUser.RolesId.Contains(
            //                                                            m.MasterDataKeyValue.ViewRoleId.ToString()) &&
            //                                                        m.MasterDataKeyValue.IsLeaf));
            //}

            var query = _db.MasterDataLocalKeyValues.Where(m => CurrentUserManager.RolesId.Contains((int)Roles.Admin) ||
            CurrentUserManager.RolesId.Contains(m.MasterDataKeyValue.ViewRoleId??0));


            //if (queryOptions.SelectExpand.RawExpand.IndexOf("ModifieUser") != -1)
            //    query = query.Where(m => LoginUser.RolesId.Contains(m.ModifieUser.ViewRoleId.ToString()));
            //if (queryOptions.SelectExpand.RawExpand.IndexOf("CreateUser") != -1)
            //    query = query.Where(m => LoginUser.RolesId.Contains(m.CreateUser.ViewRoleId.ToString()));

            return query;

        }

        // GET: odata/MasterDataLocalKeyValues(5)
        //[EnableDynamicQueryableByLog]
        //public SingleResult<MasterDataLocalKeyValue> GetMasterDataLocalKeyValue([FromODataUri] int key)
        //{
        //    return SingleResult.Create(_db.MasterDataLocalKeyValues.Where(m => m.Id == key
        //    && (m.MasterDataKeyValue.IsLeaf == false || (LoginUser.RolesId.Contains(m.MasterDataKeyValue.ViewRoleId.ToString()) && m.MasterDataKeyValue.IsLeaf))));
        //}

        // PUT: odata/MasterDataLocalKeyValues(5)
        //public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<MasterDataLocalKeyValue> patch)
        //{
        //    Validate(patch.GetEntity());

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    MasterDataLocalKeyValue masterDataLocalKeyValue = await db.MasterDataLocalKeyValues.FindAsync(key);
        //    if (masterDataLocalKeyValue == null)
        //    {
        //        return NotFound();
        //    }

        //    patch.Put(masterDataLocalKeyValue);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!MasterDataLocalKeyValueExists(key))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Updated(masterDataLocalKeyValue);
        //}

        //// POST: odata/MasterDataLocalKeyValues
        //public async Task<IHttpActionResult> Post(MasterDataLocalKeyValue masterDataLocalKeyValue)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.MasterDataLocalKeyValues.Add(masterDataLocalKeyValue);
        //    await db.SaveChangesAsync();

        //    return Created(masterDataLocalKeyValue);
        //}

        //// PATCH: odata/MasterDataLocalKeyValues(5)
        //[AcceptVerbs("PATCH", "MERGE")]
        //public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<MasterDataLocalKeyValue> patch)
        //{
        //    Validate(patch.GetEntity());

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    MasterDataLocalKeyValue masterDataLocalKeyValue = await db.MasterDataLocalKeyValues.FindAsync(key);
        //    if (masterDataLocalKeyValue == null)
        //    {
        //        return NotFound();
        //    }

        //    patch.Patch(masterDataLocalKeyValue);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!MasterDataLocalKeyValueExists(key))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Updated(masterDataLocalKeyValue);
        //}

        //// DELETE: odata/MasterDataLocalKeyValues(5)
        //public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        //{
        //    MasterDataLocalKeyValue masterDataLocalKeyValue = await db.MasterDataLocalKeyValues.FindAsync(key);
        //    if (masterDataLocalKeyValue == null)
        //    {
        //        return NotFound();
        //    }

        //    db.MasterDataLocalKeyValues.Remove(masterDataLocalKeyValue);
        //    await db.SaveChangesAsync();

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        // GET: odata/MasterDataLocalKeyValues(5)/MasterDataKeyValue
        //[EnableDynamicQueryableByLog]
        //public SingleResult<MasterDataKeyValue> GetMasterDataKeyValue([FromODataUri] int key)
        //{
        //    return SingleResult.Create(_db.MasterDataLocalKeyValues.Where(m => m.Id == key
        //    && (m.MasterDataKeyValue.IsLeaf == false || (LoginUser.RolesId.Contains(m.MasterDataKeyValue.ViewRoleId.ToString()) && m.MasterDataKeyValue.IsLeaf)))
        //    .Select(m => m.MasterDataKeyValue));
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private bool MasterDataLocalKeyValueExists(int key)
        {
            return _db.MasterDataLocalKeyValues.Count(e => e.Id == key) > 0;
        }
    }
}

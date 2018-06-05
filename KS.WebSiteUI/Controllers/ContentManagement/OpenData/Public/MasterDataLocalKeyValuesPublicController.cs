using System.Linq;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
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
    builder.EntitySet<MasterDataLocalKeyValue>("MasterDataLocalKeyValuesPublic");
    builder.EntitySet<MasterDataKeyValue>("MasterDataKeyValues"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class MasterDataLocalKeyValuesPublicController : BasePublicODataController
    {
        private readonly IContentManagementContext _db;

        public MasterDataLocalKeyValuesPublicController(IContentManagementContext context)
        {
            _db = context;
        }

        // GET: odata/MasterDataLocalKeyValuesPublic
        [EnableDynamicQueryable]
        public IQueryable<MasterDataLocalKeyValue> GetMasterDataLocalKeyValuesPublic(ODataQueryOptions queryOptions,string materDataKeyValueType = null)
        {
            //if (materDataKeyValueType != null)
            //    materDataKeyValueType = materDataKeyValueType;



            var query = _db.MasterDataLocalKeyValues.Where(md => md.MasterDataKeyValue.IsLeaf == false ||
                                                                   (md.MasterDataKeyValue.ViewRoleId == (int)Roles.Public &&
                                                                    md.MasterDataKeyValue.IsLeaf));

            if (queryOptions.SelectExpand?.RawExpand != null)
            {
                if (queryOptions.SelectExpand.RawExpand.IndexOf("ModifieUser") != -1)
                    query = query.Where(m => m.ModifieUser.ViewRoleId == (int) Roles.Public);
                if (queryOptions.SelectExpand.RawExpand.IndexOf("CreateUser") != -1)
                    query = query.Where(m => m.CreateUser.ViewRoleId == (int) Roles.Public);
            }
            return query;
        }

        // GET: odata/MasterDataLocalKeyValuesPublic(5)
        //[EnableDynamicQueryableByLog]
        //public SingleResult<MasterDataLocalKeyValue> GetMasterDataLocalKeyValuePublic([FromODataUri] int key)
        //{
        //    return SingleResult.Create(_db.MasterDataLocalKeyValues.Where(masterDataLocalKeyValue => masterDataLocalKeyValue.Id == key
        //    && (masterDataLocalKeyValue.MasterDataKeyValue.IsLeaf == false || (masterDataLocalKeyValue.MasterDataKeyValue.ViewRoleId == (int)Roles.Public
        //    && masterDataLocalKeyValue.MasterDataKeyValue.IsLeaf))));
        //}

        // PUT: odata/MasterDataLocalKeyValuesPublic(5)
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

        //// POST: odata/MasterDataLocalKeyValuesPublic
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

        //// PATCH: odata/MasterDataLocalKeyValuesPublic(5)
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

        //// DELETE: odata/MasterDataLocalKeyValuesPublic(5)
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

        // GET: odata/MasterDataLocalKeyValuesPublic(5)/MasterDataKeyValue
        //[EnableDynamicQueryableByLog]
        //public SingleResult<MasterDataKeyValue> GetMasterDataKeyValuePublic([FromODataUri] int key)
        //{
        //    return SingleResult.Create(_db.MasterDataLocalKeyValues.Where(m => m.Id == key
        //    && (m.MasterDataKeyValue.IsLeaf == false || (m.MasterDataKeyValue.ViewRoleId == (int)Roles.Public
        //    && m.MasterDataKeyValue.IsLeaf))).Select(m => m.MasterDataKeyValue));
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

using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using KS.Business.Develop;
using KS.WebSiteUI.Controllers.Base;

namespace KS.WebSiteUI.Controllers.Develop
{
    public class ServiceController : BaseAuthorizedWebApiController
    {
       private readonly IServiceBiz _serviceBiz;

       public ServiceController(IServiceBiz serviceBiz)
        {
            _serviceBiz = serviceBiz;
        }


        [Route("develop/service/Get/{id}")]
        [HttpGet]
        public async Task<JObject> Get(int id)
        { 
                return await _serviceBiz.GetAsync(id);
        }

        [Route("develop/service/Save")]
        [HttpPost]
        public async Task<JObject> Save(JObject data)
        {
            return JObject.Parse(JsonConvert.SerializeObject
                (await _serviceBiz.Save(data), Formatting.None,
                new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
        }

        [Route("develop/service/delete")]
        [HttpPost]
        public async Task<bool> DeleteService(JObject data)
        {
            return await _serviceBiz.Delete(data);
        }

        [Route("develop/service/GetChange/{changeId}/{codeId}")]
        [HttpGet]
        public string GetChangeFromSourceControl(int changeId,int codeId)
        {
            return  _serviceBiz.GetChangeFromSourceControl(changeId, codeId);
        }

        [Route("develop/service/GetChanges/{codeGuid}/{orderBy}/{skip}/{take}/{comment}/{user}/{fromDateTime}/{toDateTime}")]
        [HttpGet]
        public JObject GetChangesFromSourceControl(string codeGuid
    , string orderBy, int skip, int take
    , string comment
    , string user
    , string fromDateTime
    , string toDateTime)
        {
            return _serviceBiz.GetChangesFromSourceControl(orderBy, skip, take
            , comment
            , user
            , fromDateTime
            , toDateTime
            , codeGuid);
        }
    }
}
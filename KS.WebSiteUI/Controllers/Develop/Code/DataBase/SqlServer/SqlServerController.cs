using System.Threading.Tasks;
using System.Web.Http;
using KS.Business.Develop.Code.DataBase;
using KS.WebSiteUI.Controllers.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KS.WebSiteUI.Controllers.Develop.Code.DataBase.SqlServer
{
    public class SqlServerController : BaseAuthorizedWebApiController
    {
        private readonly ISqlServerBiz _sqlServerBiz;

        public SqlServerController(ISqlServerBiz sqlServerBiz)
        {
            _sqlServerBiz = sqlServerBiz;
        }

        [Route("develop/code/database/sqlserver/connectionsByOtherLanguage/{lang}")]
        [HttpGet]
        public async Task<JArray> GetConnections(string lang)
        {
            return JArray.Parse(JsonConvert.SerializeObject((await _sqlServerBiz.GetConnections(lang)

                .ConfigureAwait(false)), Formatting.None));
        }

        [Route("develop/code/database/sqlserver/connections")]
        [HttpGet]
        public async Task<JArray> GetConnections()
        {
            return JArray.Parse(JsonConvert.SerializeObject((await _sqlServerBiz.GetConnections()

                .ConfigureAwait(false)), Formatting.None));
        }

        [Route("develop/code/database/sqlserver/Save")]
        [HttpPost]
        public async Task<JObject> Save(JObject data)
        {
            return JObject.Parse(JsonConvert.SerializeObject
                (await _sqlServerBiz.Save(data), Formatting.None,
                new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
        }

        [Route("develop/code/database/sqlserver/delete")]
        [HttpPost]
        public async Task<bool> Delete(JObject data)
        {
            return await _sqlServerBiz.Delete(data);
        }

        [Route("develop/code/database/sqlserver/get/{id}")]
        [HttpGet]
        public async Task<JObject> Get(int id)
        {
            return await _sqlServerBiz.GetAsync(id);
        }

        [Route("develop/code/database/sqlserver/GetCodeContent/{path}/{id}")]
        [HttpGet]
        public async Task<string> GetCodeContent(string path, int id)
        {
            return await _sqlServerBiz.GetCodeContentAsync(id, path);
        }

        [Route("develop/code/database/sqlserver/file/Save")]
        [HttpPost]
        public async Task<bool> SaveFile(JObject data)
        {
            return await _sqlServerBiz.SaveFile(data);
        }

        [Route("develop/code/database/sqlserver/Exec")]
        [HttpPost]
        public async Task<JArray> Execute(JObject data)
        {
            return 
                await _sqlServerBiz.Execute(data);
        }

        [Route("develop/code/database/sqlserver/GetChange/{changeId}/{path}/{codeId}")]
        [HttpGet]
        public async Task<string> GetChangeFromSourceControl(int changeId, string path, int codeId)
        {
            return await _sqlServerBiz.GetChangeFromSourceControlAsync(changeId, codeId,path);
        }

        [Route("develop/code/database/sqlserver/GetChanges/{codePath}/{codeName}/{orderBy}/{skip}/{take}/{comment}/{user}/{fromDateTime}/{toDateTime}")]
        [HttpGet]
        public JObject GetChangesFromSourceControl(string codePath
    , string codeName,string orderBy, int skip, int take
    , string comment
    , string user
    , string fromDateTime
    , string toDateTime)
        {
            return _sqlServerBiz.GetChangesFromSourceControl(orderBy, skip, take
            , comment
            , user
            , fromDateTime
            , toDateTime
            , codePath
            , codeName);
        }
    }
}

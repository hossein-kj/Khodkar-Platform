using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using KS.Business.Develop.Code.Os.DotNet;
using KS.WebSiteUI.Controllers.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using KS.Core.Model.Develop;
using KS.Core.Security;

namespace KS.WebSiteUI.Controllers.Develop.Code.Os.DotNet
{
    public class DotNetController : BaseAuthorizedWebApiController
    {
       private readonly IDotNetBiz _dotNetBiz;

       public DotNetController(IDotNetBiz dotNetBiz)
        {
            _dotNetBiz = dotNetBiz;
        }


        [Route("develop/code/os/dotnet/Get/{id}")]
        [HttpGet]
        public async Task<JObject> Get(int id)
        { 
                return await _dotNetBiz.GetAsync(id);
        }

        [Route("develop/code/os/dotnet/Save")]
        [HttpPost]
        public async Task<JObject> Save(JObject data)
        {
            return await _dotNetBiz.Save(data);
        }

        [Route("develop/code/os/dotnet/delete")]
        [HttpPost]
        public async Task<bool> DeleteCode(JObject data)
        {
            return await _dotNetBiz.Delete(data);
        }

        [Route("develop/code/os/dotnet/dllCcompile")]
        [HttpPost]
        public async Task<bool> DllCcompile(JObject data)
        {
            return await _dotNetBiz.DllCompile(data);
        }

        [Route("develop/code/os/dotnet/debugCode")]
        [HttpPost]
        public async Task<bool> DebugCode(JObject data)
        {
            return await _dotNetBiz.DebugCode(data);
        }

        [Route("develop/code/os/dotnet/addOrUpdateDebugInfo")]
        [HttpPost]
        public async Task<JObject> AddOrUpdateDebugInfo(DebugInfo debugInfo)
        {
            return JObject.Parse(JsonConvert.SerializeObject
            (await _dotNetBiz.AddOrUpdateDebugInfo(debugInfo), Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }));
        }

        [Route("develop/code/os/dotnet/deleteDebugInfo")]
        [HttpPost]
        public async Task<bool> DeleteDebugInfo(JObject data)
        {
            return await _dotNetBiz.DeleteDebugInfo(data);
        }

        [Route("develop/code/os/dotnet/getDebugInfo/{debugInfoId}/{codeId}")]
        [HttpGet]
        public async Task<JObject> GetDebugInfo(int debugInfoId, int codeId)
        {
            return JObject.Parse(JsonConvert.SerializeObject
            (await _dotNetBiz.GetDebugInfo(debugInfoId, codeId), Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }));
        }

        [Route("develop/code/os/dotnet/GetDebugInfos/{orderBy}/{skip}/{take}/{codeId}/{data}/{integerValue}/{decimalValue}/{fromDateTime}/{toDateTime}")]
        [HttpGet]
        public async Task<JObject> GetDebugInfosByPagination(
        string orderBy, int skip, int take
        , int codeId
        , string data
        , int? integerValue
        , decimal? decimalValue
        , string fromDateTime
        , string toDateTime)
        {
            return await _dotNetBiz.GetDebugInfosByPagination(orderBy, skip, take
        , codeId
        , data
        , integerValue
        , decimalValue
        , fromDateTime
        , toDateTime);
        }

        [Route("develop/code/os/dotnet/publishDll")]
        [HttpPost]
        public async Task<bool> PublishDll(JObject data)
        {
            return await _dotNetBiz.PublishDll(data);
        }

        [Route("develop/code/os/dotnet/dellOutputDll")]
        [HttpPost]
        public async Task<bool> DellOutputDll(JObject data)
        {
            return await _dotNetBiz.DellOutputDll(data);
        }

        [Route("develop/code/os/dotnet/GetetOutputs/{codeId}/{orderBy}/{skip}/{take}")]
        [HttpGet]
        public async Task<JObject> GetetOutputs(int codeId
        , string orderBy, int skip, int take)
        {
            return await _dotNetBiz.GetOutputs(orderBy, skip, take
            , codeId);
        }

        [Route("develop/code/os/dotnet/GetOutputVersions/{codeId}")]
        [HttpGet]
        public async Task<JArray> GetOutputVersions(int codeId)
        {
            return await _dotNetBiz.GetOutputVersions(codeId);
        }

        [Route("develop/code/os/dotnet/GetOutputVersionNumbers/{codeId}")]
        [HttpGet]
        public async Task<JArray> GetOutputVersionNumbers(int codeId)
        {
            return await _dotNetBiz.GetOutputVersions(codeId,false);
        }

        [Route("develop/code/os/dotnet/addOutputDll")]
        [HttpPost]
        public async Task<JObject> AddOutputDll(JObject data)
        {
            return await _dotNetBiz.AddOutputDll(data);
        }

        [Route("develop/code/os/dotnet/ViewDllBuildLog/{name}/{codeId}")]
        [HttpGet]
        public async Task<string> ViewDllBuildLog(string name, int codeId)
        {
            return await _dotNetBiz.ReadDllBuildLog(codeId, name);
        }

        [Route("develop/code/os/dotnet/GetChange/{changeId}/{codeId}")]
        [HttpGet]
        public async Task<string> GetChangeFromSourceControl(int changeId,int codeId)
        {
            return await _dotNetBiz.GetChangeFromSourceControlAsync(changeId, codeId);
        }

        [Route("develop/code/os/dotnet/GetChanges/{codeId}/{orderBy}/{skip}/{take}/{comment}/{user}/{fromDateTime}/{toDateTime}")]
        [HttpGet]
        public async Task<JObject> GetChangesFromSourceControl(int codeId
    , string orderBy, int skip, int take
    , string comment
    , string user
    , string fromDateTime
    , string toDateTime)
        {

            return await _dotNetBiz.GetChangesFromSourceControlAsync(orderBy, skip, take
            , comment
            , user
            , fromDateTime
            , toDateTime
            ,codeId);
        }


        [Route("develop/code/os/dotnet/GetWcfGenreatedCode")]
        [HttpPost]
        public async Task<string> GetWcfGenreatedCode(JObject data)
        {
            var wcfGuid = SecureGuid.NewGuid().ToString("N");

            //save xml as file by guid name;
            await _dotNetBiz.WriteWcfWebServiceMetaDataAsync(data, wcfGuid);


            dynamic wcfData = data;
            return _dotNetBiz.GetWcfWebServiceCode(HttpContext.Current.Request.Url.Scheme + "://"
                                                   + (HttpContext.Current.Request.Url.Authority +
                                                      "/develop/code/os/dotnet/WebService/GetWcfGenreatedCode/" +
                                                      wcfGuid).Replace("//", "/"),
                (string) wcfData.Language);

        }

        [Route("develop/code/os/dotnet/GenerateMigration")]
        [HttpPost]
        public async Task<JObject> GenerateMigration(JObject migrationInfo)
        {
            return await _dotNetBiz.GenerateMigration(migrationInfo);
        }

        [Route("develop/code/os/dotnet/RunMigration")]
        [HttpPost]
        public async Task<bool> RunMigration(JObject migrationInfo)
        {
             await _dotNetBiz.RunMigration(migrationInfo);
            return true;
        }

        [Route("develop/code/os/dotnet/GetMigrationScript")]
        [HttpPost]
        public async Task<string> GetMigrationScript(JObject migrationInfo)
        {
           return await _dotNetBiz.GetMigrationScript(migrationInfo);
        }

        [Route("develop/code/os/dotnet/GetDbMigrationClasses/{dllVersion}/{configurationClassId}")]
        [HttpGet]
        public async Task<JArray> GetDbMigrationClasses(int dllVersion, int configurationClassId)
        {
            return await _dotNetBiz.GetDbMigrationClasses(dllVersion, configurationClassId);
        }

        [Route("develop/code/os/dotnet/RunUnitTestMethod")]
        [HttpPost]
        public async Task<bool> RunUnitTestMethod(JObject unitTestInfo)
        {
            await _dotNetBiz.RunUnitTestMethod(unitTestInfo);
            return true;
        }

        [Route("develop/code/os/dotnet/GetUnitTestMethods/{dllId}/{dllVersion}/{codeId}")]
        [HttpGet]
        public async Task<JArray> GetUnitTestMethods(int dllId,int dllVersion, int codeId)
        {
            return await _dotNetBiz.GetUnitTestMethods(dllId,dllVersion, codeId);
        }
    }
}
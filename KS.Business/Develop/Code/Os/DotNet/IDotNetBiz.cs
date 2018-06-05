
using System.Threading.Tasks;
using KS.Core.Model.Develop;
using Newtonsoft.Json.Linq;

namespace KS.Business.Develop.Code.Os.DotNet
{
    public interface IDotNetBiz
    {
        Task<bool> DellOutputDll(JObject data);
        Task<JObject> AddOutputDll(JObject data);
        Task<JObject> GetOutputs(string orderBy, int skip, int take, int codeId);
        Task<JArray> GetOutputVersions(int codeId, bool showSuggestionLatestVersion = true);
        Task<JObject> Save(JObject data);
        Task<string> GetChangeFromSourceControlAsync(int changeId, int codeId);

        Task<JObject> GetChangesFromSourceControlAsync(string orderBy, int skip, int take
            , string comment
            , string user
            , string fromDateTime
            , string toDateTime
            , int codeId);

        Task<bool> PublishDll(JObject data);
        Task<string> ReadDllBuildLog(int codeId,string name);
        Task<bool> DebugCode(JObject data);
        Task<bool> DllCompile(JObject data);
        Task<bool> Delete(JObject data);
        Task<JObject> GetAsync(int id);
        Task<DebugInfo> AddOrUpdateDebugInfo(DebugInfo debugInfo);
        Task<bool> DeleteDebugInfo(JObject data);
        Task<DebugInfo> GetDebugInfo(int debugInfoId, int codeId);

        Task<JObject> GetDebugInfosByPagination(
            string orderBy, int skip, int take
            , int codeId
            , string data
            , int? integerValue
            , decimal? decimalValue
            , string fromDateTime
            , string toDateTime);

        Task<bool> WriteWcfWebServiceMetaDataAsync(JObject data, string outPutName);
        string GetWcfWebServiceCode(string wcfLocalUrl, string language);
        Task<string> ReadWcfWebServiceMetaDataAsync(string wcfGuid);
        Task<JObject> GenerateMigration(JObject migrationInfo);
        Task<JArray> GetDbMigrationClasses(int dllVersion, int configurationClassId);
        Task<bool> RunMigration(JObject migrationInfo);
        Task<string> GetMigrationScript(JObject migrationInfo);
        Task<bool> RunUnitTestMethod(JObject unitTestInfo);
        Task<JArray> GetUnitTestMethods(int dllId, int dllVersion, int codeId);
    }
}
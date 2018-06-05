using System.Collections.Generic;
using System.Threading.Tasks;
using KS.Core.Model.Core;
using KS.Model.ContentManagement;
using Newtonsoft.Json.Linq;

namespace KS.Business.Develop.Code.DataBase
{
    public interface ISqlServerBiz
    {
        Task<List<KeyValue>> GetConnections(string lang = "");
        Task<MasterDataKeyValue> Save(JObject data);
        Task<bool> Delete(JObject data);
        Task<JArray> Execute(JObject data);
        Task<string> GetChangeFromSourceControlAsync(int changeId, int codeId, string path);

        JObject GetChangesFromSourceControl(string orderBy, int skip, int take
            , string comment
            , string user
            , string fromDateTime
            , string toDateTime
            ,string codePath
            , string codeName);

        Task<bool> SaveFile(JObject data);
        Task<bool> DeleteCode(JObject data);
        Task<string> GetCodeContentAsync(int codeId,string path);
        Task<MasterDataKeyValue> Save(JObject data,bool isAuthroize);
        Task<MasterDataKeyValue> Delete(JObject data,bool isAuthroize);
        Task<MasterDataLocalKeyValue> SaveTranslate(JObject data);
        Task<JObject> GetAsync(int id);
    }
}
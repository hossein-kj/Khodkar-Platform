using System.Collections.Generic;
using System.Threading.Tasks;
using KS.Core.Model.Core;
using KS.Model.ContentManagement;
using Newtonsoft.Json.Linq;

namespace KS.Business.Develop.Code.BrowserCode
{
    public interface IBrowserCodeBiz
    {
        bool CheckJavascriptCode(JObject data);
        Task<MasterDataKeyValue> Save(JObject data);
        Task<MasterDataKeyValue> SaveBundleOrBundleSource(JObject data,bool isSource = false);
        Task<bool> Delete(JObject data);
        Task<bool> DeleteBundleOrBundleSource(JObject data, bool isSource = false);
        Task<bool> Compile(JObject data, string localHost);
        Task<List<KeyValue>> GetBundleDependencyForDependencyEngieen(int bundleId);
        Task<List<KeyValue>> GetBundleDependency(int bundleId);
  
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
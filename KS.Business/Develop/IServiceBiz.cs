using System.Threading.Tasks;
using KS.Model.ContentManagement;
using Newtonsoft.Json.Linq;

namespace KS.Business.Develop
{
    public interface IServiceBiz
    {
        Task<MasterDataKeyValue> Save(JObject data);
        string GetChangeFromSourceControl(int changeId, int codeId);

        JObject GetChangesFromSourceControl(string orderBy, int skip, int take
            , string comment
            , string user
            , string fromDateTime
            , string toDateTime
            , string codeGuid);

        Task<bool> Delete(JObject data);
        Task<JObject> GetAsync(int id);
    }
}
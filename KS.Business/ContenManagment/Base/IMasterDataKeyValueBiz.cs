using System.Threading.Tasks;
using KS.Model.ContentManagement;
using Newtonsoft.Json.Linq;

namespace KS.Business.ContenManagment.Base
{
    public interface IMasterDataKeyValueBiz
    {
        Task<MasterDataKeyValue> Save(JObject data,bool isAuthroize=false);
        Task<MasterDataKeyValue> Delete(JObject data,bool isAuthroize=false);
        Task<MasterDataLocalKeyValue> SaveTranslate(JObject data);
        Task<JObject> GetAsync(int id);
    }
}
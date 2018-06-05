using System.Threading.Tasks;
using KS.Model.ContentManagement;
using Newtonsoft.Json.Linq;

namespace KS.Business.Security.Base
{
    public interface IPermissionBiz
    {
        Task<MasterDataKeyValue> Save(JObject data);
        Task<MasterDataKeyValue> Delete(JObject data);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using KS.Core.Model.Core;
using KS.Core.Model.Develop;
using Newtonsoft.Json.Linq;

namespace KS.Business.Develop
{
    public interface IWebConfigBiz
    {
        Task<WebConfigSetting> Save(JObject data);
        bool Delete(JObject data);
        Task<JObject> GetSettingsByPagination(string orderBy, int skip, int take);
        List<KeyValue> GetMasterDataKeyValuePropertyName();
    }
}
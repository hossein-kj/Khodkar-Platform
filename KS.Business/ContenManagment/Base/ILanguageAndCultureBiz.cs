using System.Collections.Generic;
using System.Threading.Tasks;
using KS.Model.ContentManagement;
using Newtonsoft.Json.Linq;

namespace KS.Business.ContenManagment.Base
{
    public interface ILanguageAndCultureBiz
    {
        Task<IList<LanguageAndCulture>> GetLanguagesAsync();
        Task<LanguageAndCulture> Save(JObject data);
        Task<JObject> GetAsync(int id);
        Task<bool> Delete(JObject data);
    }
}
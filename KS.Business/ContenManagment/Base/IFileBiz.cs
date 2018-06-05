using System.Threading.Tasks;
using KS.Model.ContentManagement;
using Newtonsoft.Json.Linq;

namespace KS.Business.ContenManagment.Base
{
    public interface IFileBiz
    {
        Task<File> Save(JObject data);
        Task<LocalFile> SaveTranslate(JObject data);
        Task<bool> Delete(JObject data);
    }
}
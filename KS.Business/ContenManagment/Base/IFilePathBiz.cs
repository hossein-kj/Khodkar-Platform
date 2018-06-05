using System.Threading.Tasks;
using KS.Model.ContentManagement;
using Newtonsoft.Json.Linq;

namespace KS.Business.ContenManagment.Base
{
    public interface IFilePathBiz
    {
        Task<FilePath> Save(JObject data);
        Task<LocalFilePath> SaveTranslate(JObject data);
        Task<bool> Delete(JObject data);
    }
}
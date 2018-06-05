using System.Threading.Tasks;
using KS.Model.ContentManagement;
using Newtonsoft.Json.Linq;

namespace KS.Business.ContenManagment.Base
{
    public interface ILinkBiz
    {
        Task<Link> Save(JObject data);
        Task<bool> Delete(JObject data);
    }
}
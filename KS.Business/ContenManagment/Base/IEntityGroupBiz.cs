using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace KS.Business.ContenManagment.Base
{
    public interface IEntityGroupBiz
    {
        Task<string> Save(JObject data);
    }
}
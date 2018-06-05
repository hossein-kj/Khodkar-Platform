using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace KS.Business.Develop.Log
{
    public interface IActionLogBiz
    {
        Task<JObject> GetByServiceIdAndPaginationAsync(string orderBy, int skip, int take, int serviceId, string user, string fromDateTime, string toDateTime);
        JObject GetByPagination(string orderBy, int skip, int take, string serviceUrl, string nameOrUrlOrUser, string fromDateTime, string toDateTime);
        JObject GetActionById(int id);
        bool Delete(JObject data);
        bool GetLogStatus();
        bool ToggleEnableLogUntilNextApplicationRestart();
        bool BackUp();
    }
}
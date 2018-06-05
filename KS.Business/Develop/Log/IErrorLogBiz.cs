using Newtonsoft.Json.Linq;

namespace KS.Business.Develop.Log
{
    public interface IErrorLogBiz
    {
        JObject GetByPagination(string orderBy, int skip, int take, string typeOrSourceOrMessage, string user, string fromDateTime, string toDateTime);
        JObject GetErrorById(int id);
        bool Delete(JObject data);
        bool BackUp();
    }
}
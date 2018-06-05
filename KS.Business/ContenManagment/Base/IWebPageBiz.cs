using System.Threading.Tasks;
using KS.Core.GlobalVarioable;
using KS.Model.ContentManagement;
using Newtonsoft.Json.Linq;

namespace KS.Business.ContenManagment.Base
{
    public interface IWebPageBiz
    {
        JObject GetWebPageChangesFromSourceControl(string orderBy, int skip, int take
            , string comment
            ,string user
            , string fromDateTime
            , string toDateTime
            , string webPageGuid
            ,string type);

        Task<JObject> GetWebPageForViewAsync(string url, bool isModal = false);
        Task<JObject> GetWebPageForDebugAsync(string url, bool isModal = false);
        Task<JObject> GetWebPageForEditAsync(string url, int typeId);
        Task<JObject> GetWebPageChangeFromSourceControlAsync(int changeId,string webPageGuid);
        Task<string> GetWebPageResourcesChangeFromSourceControlAsync(int changeId, string webPageGuid);
        Task<bool> Delete(JObject data);
        Task<WebPage> Save(JObject data);
        Task<WebPage> GetTemplateAsync(string webPageUrl);
        Task<JObject> ToJsonAsync(WebPageJsonType webPageJsonType, string webPageUrl);
        Task<string> ToStringAsync(string url);
    }
}
using Newtonsoft.Json.Linq;

namespace KS.Core.Model.ContentManagement
{
    public interface IWebPageCore
    {
        string Title { get; set; }
        string MetaTags { get; set; }
        string Url { get; set; }
        string PageId { get; set; }
        string DependentModules { get; set; }
        string Param { get; set; }
        string Html { get; set; }
        JObject ToJObject();
    }
}
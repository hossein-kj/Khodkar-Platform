using KS.Core.GlobalVarioable;
using Newtonsoft.Json.Linq;

namespace KS.Core.Model.ContentManagement
{
    public class WebPageCore : IWebPageCore
    {
        private string _pageId;
        private string _dependentModules;
        private string _html;
        public string Title { get; set; }
        public string Url { get; set; }

        public string PageId
        {
            get { return "i" + _pageId; }
            set { _pageId = value; }
        }

        public string DependentModules
        {
            get { return GetDependentModules(); }
            set { _dependentModules = value; }
        }
        public string Param { get; set; }

        public string Html
        {
            get { return "<span style='display: none' id='" + PageId + "'></span>" + _html; }
            set { _html = value; }
        }
        public bool HaveScript { get; set; }
        public bool HaveStyle { get; set; }
        public string Version{ get; set; }


    public JObject ToJObject()
        {
            return JObject.FromObject(new
            {
                title=Title,
                url = Url,
                pageId=PageId,
                dependentModules= DependentModules,
                param= "{" + Param + "}",
                html= Html
            });
        }

        private string GetDependentModules()
        {
            var dependentModules = _dependentModules;
            if (HaveStyle)
            {
                
                dependentModules= dependentModules.Replace("[", "[{\"url\":\"" + Config.PagesPath.Substring(2) 
                    + _pageId + ".css?minVersion=" + Version + "\"}" + (_dependentModules.Length > 2 ? ",":""));
            }

            if (HaveScript)
            {
                dependentModules = dependentModules.Replace("[", "[{\"url\":\"" + Config.PagesPath.Substring(2)
                    + _pageId + ".js?minVersion=" + Version + "\"}" + ((_dependentModules.Length > 2 || HaveStyle )? "," : ""));
            }

            return dependentModules ;
        }
    }
}

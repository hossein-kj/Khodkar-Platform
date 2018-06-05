using System.Web.Mvc;
using System.Web.Routing;

namespace KS.WebSiteUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //routes.IgnoreRoute("");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();

        //    routes.MapRoute(
        //    name: "Default",
        //    url: "{*anything}",
            
        //    defaults: new { controller = "WebPage", action = "GetWebPage" }
        //);


            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "DefaultMvcController", action = "GetWebPage", id = UrlParameter.Optional }
            //);
        }
    }
}

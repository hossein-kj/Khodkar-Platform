using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using Elmah.Contrib.WebApi;
using KS.Model.ContentManagement;
using KS.Model.Security;

namespace KS.WebSiteUI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // TODO: Add any additional configuration code.

            // Web API routes
            config.MapHttpAttributeRoutes();

          
           
            // Configure Web API to use only bearer token authentication. 
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
           
      

           config.Filters.Add(new ElmahHandleErrorApiAttribute());
            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            // WebAPI when dealing with JSON & JavaScript!
            // Setup json serialization to serialize classes to camel (std. Json format)
            //*******************************************************************************
            //var formatter = config.Formatters.JsonFormatter;
            //formatter.SerializerSettings.ReferenceLoopHandling.ContractResolver =
            //    new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();



            //config.Formatters.JsonFormatter.SerializerSettings
            //            .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            //config.Formatters.Add(formatter);

            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            //builder.EntitySet<Service>("Services");
            builder.EntitySet<MasterDataKeyValue>("MasterDataKeyValues");
            builder.EntitySet<MasterDataLocalKeyValue>("MasterDataLocalKeyValues");
            builder.EntitySet<EntityGroup>("EntityGroups");
            builder.EntitySet<Link>("Links");
            builder.EntitySet<FilePath>("FilePaths");
            builder.EntitySet<File>("Files");
            builder.EntitySet<LocalFilePath>("LocalFilePaths");
            builder.EntitySet<LocalFile>("LocalFiles");
            builder.EntitySet<EntityMasterDataKeyValue>("EntityMasterDataKeyValues");
            builder.EntitySet<UserProfile> ("UserProfiles");
            builder.EntitySet<LanguageAndCulture>("LanguageAndCultures");
            config.Routes.MapODataServiceRoute("odata.cms", "odata/cms", builder.GetEdmModel());

            ODataConventionModelBuilder builderPublic = new ODataConventionModelBuilder();
            //builder.EntitySet<Service>("Services");
            builderPublic.EntitySet<MasterDataKeyValue>("MasterDataKeyValuesPublic");
            builderPublic.EntitySet<MasterDataLocalKeyValue>("MasterDataLocalKeyValuesPublic");
            builderPublic.EntitySet<EntityGroup>("EntityGroupsPublic");
            builderPublic.EntitySet<Link>("LinksPublic");
            
            config.Routes.MapODataServiceRoute("odata.cms.public", "odata/public/cms", builderPublic.GetEdmModel());
            //config.AddODataQueryFilter(new DynamicQueryableAttribute());

            ODataConventionModelBuilder builderSecurity = new ODataConventionModelBuilder();
            builderSecurity.EntitySet<ApplicationUser>("Users");
            builderSecurity.EntitySet<ApplicationRole>("Roles");
            builderSecurity.EntitySet<ApplicationGroup>("Groups");
            builderSecurity.EntitySet<ApplicationLocalRole>("LocalRoles");
            builderSecurity.EntitySet<ApplicationLocalGroup>("LocalGroups");
            builderSecurity.EntitySet<ApplicationUserRole>("UserRoles");
            builderSecurity.EntitySet<ApplicationUserGroup>("UserGroups");
            builderSecurity.EntitySet<ApplicationGroupRole>("GroupRoles");
            config.Routes.MapODataServiceRoute("odata.security", "odata/security", builderSecurity.GetEdmModel());
        }
    }
}
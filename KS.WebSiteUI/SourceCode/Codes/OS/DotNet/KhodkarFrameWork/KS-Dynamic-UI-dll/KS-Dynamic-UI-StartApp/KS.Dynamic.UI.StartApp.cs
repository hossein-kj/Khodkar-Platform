 /*khodkar c# comment                   


namespace KS.Dynamic.UI.StartApp
{
using System;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;
using System.Configuration;
using JavaScriptEngineSwitcher.Core;
using Microsoft.Owin.Security.OAuth;
using JavaScriptEngineSwitcher.Msie;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using Elmah.Contrib.WebApi;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using KS.WebSiteUI.Controllers.Base;
using KS.Business.Localization;
using KS.Business.Security;
using KS.Core.Security;
using KS.DataAccess.Contexts;
using KS.Model.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using KS.WebSiteUI;
using KS.Business.ContenManagment;
using KS.Business.ContenManagment.Base;
using KS.Business.FileManagement;
using KS.Business.Develop;
using KS.Business.Develop.Code.BrowserCode;
using KS.Business.Develop.Code.DataBase;
using KS.Business.Develop.Log;
using KS.Business.Security.Base;
using KS.Core.CoockieProvider.Adapters;
using KS.Core.Security.Adapters;
using KS.Core.Localization.Adapters;
using KS.Core.Utility.Adapters;
using KS.Core.DependencyProvider.Adapters;
using KS.Core.CacheProvider.Adapters;
using KS.Core.CodeManager;
using KS.Core.CodeManager.Base;
using KS.Core.FileSystemProvide;
using KS.Core.FileSystemProvide.Base;
using KS.Core.Log;
using KS.Core.Log.Base;
using KS.Core.Log.Elmah.Base;
using KS.Core.Owin.Middleware;
using KS.Core.SessionProvider;
using KS.Core.SessionProvider.Base;
using KS.Core.UI.Attribute.Mvc;
using KS.Core.UI.Attribute.Odata;
using KS.Core.UI.Attribute.WebApi;
using KS.Core.GlobalVarioable;
using KS.Model.ContentManagement;
using KS.WebSiteUI.Controllers.Defaults;
using ElmahErrorLogManager = KS.Core.Log.Elmah.Base.ElmahErrorLogManager;
using KS.Core.UI.Setting.Adapters;
using KS.Core.UI.Configuration;
using KS.Core.CodeManager.DataBase;
using KS.Core.CodeManager.DataBase.Base;
using KS.Core.CodeManager.BrowsersCode;
using KS.Core.CodeManager.BrowsersCode.Base;
using KS.Core.Data.Contexts;
using KS.Core.Data.Contexts.Base;
using KS.Core.Data.Contexts.SqlServer;
using KS.Core.Data.Contexts.SqlServer.Base;
using KS.DataAccess.Contexts.Base;
using KS.Business.Develop.Code.Os.DotNet;
using KS.Core.CodeManager.Os.DotNet;
using KS.Core.CodeManager.Os.DotNet.Base;
using KS.Core.SignalR;
using KS.Core.EntityFramework;
using KS.Core.FileSystemProvide.Uploader;
using KS.Dynamic.DataAccess.Contexts;
using System;
using KS.Core.Localization;
using KS.Core.Model.Log;
using KS.Core.UI.Setting;

    public partial class Startup
    {
         public Startup()
        {
            RegisterConfig();
        }
        public void Configuration(IAppBuilder app)
        {
            
            //start log time
            var startTime = DateTime.Now.TimeOfDay.ToString();
            
           

            // Get your HttpConfiguration.
            var config = new HttpConfiguration();

            GlobalRegistration(config);

            var builder = new ContainerBuilder();

            builder.RegisterModule<AutofacWebTypesModule>();






            // REGISTER DEPENDENCIES

            //Begin Register Entity FrameWork Extended For Oracle
            //EntityFramework.Locator.Current.Register<EntityFramework.Batch.IBatchRunner>(() => new OracleBatchRunner());
            //End Register Entity FrameWork Extended For Oracle
            builder.RegisterType<DependencyAdapter>().As<IDependencyAdapter>().InstancePerLifetimeScope();

            builder.RegisterType<NotificationManager>().As<INotificationManager>().InstancePerLifetimeScope();
            builder.Register(c => new ElmahErrorLogManager(null)).As<IElmahErrorLogManager>().InstancePerLifetimeScope();
            builder.RegisterType<ErrorLogManager>().As<IErrorLogManager>().InstancePerLifetimeScope();
            builder.RegisterType<DefaultCalendar>().As<IDefaultCalendar>().InstancePerLifetimeScope();
            builder.RegisterType<ActionLogManager>().As<IActionLogManager>().InstancePerLifetimeScope();
            builder.RegisterType<SessionManager>().As<ISessionManager>().InstancePerLifetimeScope();
            builder.RegisterType<DefaultCookieAdapter>().As<IDefaultCookieAdapter>().InstancePerLifetimeScope(); 
            builder.RegisterType<ZipManager>().As<IZipManager>().InstancePerLifetimeScope();
            builder.RegisterType<BundleManager>().As<IBundleManager>().InstancePerLifetimeScope();
            builder.RegisterType<DefaultLanguageAdapter>().As<ILanguageAdapter>().InstancePerLifetimeScope();
            builder.RegisterType<DefaultSettingsAdapter>().As<IDefaultSettingsAdapter>().InstancePerLifetimeScope();
            builder.RegisterType<WebConfigManager>().As<IWebConfigManager>().InstancePerLifetimeScope();
            builder.RegisterType<DataBaseManager>().As<IDataBaseManager>().InstancePerLifetimeScope();
            builder.RegisterType<CodeTemplate>().As<ICodeTemplate>().InstancePerLifetimeScope();
            builder.RegisterType<SourceControl>().As< ISourceControl>().InstancePerLifetimeScope();
            builder.RegisterType<Migration>().As<IMigration>().InstancePerLifetimeScope();
            builder.RegisterType<UnitTester>().As<IUnitTester>().InstancePerLifetimeScope();
            builder.RegisterType<FilesHandler>().As<IFilesHandler>().InstancePerLifetimeScope();
            builder.RegisterType<FileSystemManager>().As<IFileSystemManager>().InstancePerLifetimeScope();

    
            builder.RegisterType<DefaultAuthorizeAdapter>().As<IDefaultAuthorizeAdapter>().InstancePerLifetimeScope();

            builder.RegisterType<DefaultCacheAdapter>().As<IDefaultCacheAdapter>().InstancePerLifetimeScope();
            builder.RegisterType<DefaultCurrentUserAdapter>().As<IDefaultCurrentUserAdapter>().InstancePerLifetimeScope();

            builder.RegisterType<Debugger>().As<IDebugger>().InstancePerLifetimeScope();
            builder.RegisterType<ImageManager>().As<IImageManager>().InstancePerLifetimeScope();
            builder.RegisterType<CompressManager>().As<ICompressManager>().InstancePerLifetimeScope();
            builder.RegisterType<Compiler>().As<ICompiler>().InstancePerLifetimeScope();
            builder.RegisterType<SqlHelper>().As<ISqlHelper>().InstancePerLifetimeScope();
            builder.RegisterType<DataBaseContextManager>().As<IDataBaseContextManager>().InstancePerLifetimeScope();
            builder.RegisterType<SecurityContext>().As<ISecurityContext>().InstancePerLifetimeScope();
            builder.RegisterType<SecurityContext>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<SecurityBiz>().As<ISecurityBiz>().InstancePerLifetimeScope();
            builder.RegisterType<ContentManagementContext>().As<IContentManagementContext>().InstancePerLifetimeScope();
            builder.RegisterType<DynamicContext>().As<IDynamicContext>().InstancePerLifetimeScope();
            builder.RegisterType<WebPageBiz>().As<IWebPageBiz>().InstancePerLifetimeScope();
            builder.RegisterType<MasterDataKeyValueBiz>().As<IMasterDataKeyValueBiz>().InstancePerLifetimeScope();
            builder.RegisterType<ServiceBiz>().As<IServiceBiz>().InstancePerLifetimeScope();
            builder.RegisterType<LanguageAndCultureBiz>().As<ILanguageAndCultureBiz>().InstancePerLifetimeScope();
            builder.RegisterType<LinkBiz>().As<ILinkBiz>().InstancePerLifetimeScope();
            builder.RegisterType<EntityGroupBiz>().As< IEntityGroupBiz>().InstancePerLifetimeScope();
            builder.RegisterType<FileSystemBiz>().As<IFileSystemBiz>().InstancePerLifetimeScope();
            builder.RegisterType<FilePathBiz>().As<IFilePathBiz>().InstancePerLifetimeScope();
            builder.RegisterType<FileBiz>().As<IFileBiz>().InstancePerLifetimeScope();
            builder.RegisterType<BrowserCodeBiz>().As< IBrowserCodeBiz>().InstancePerLifetimeScope();
            builder.RegisterType<WebConfigBiz>().As< IWebConfigBiz>().InstancePerLifetimeScope();
            builder.RegisterType<PermissionBiz>().As<IPermissionBiz>().InstancePerLifetimeScope();
            builder.RegisterType<SqlServerBiz>().As< ISqlServerBiz>().InstancePerLifetimeScope();
            builder.RegisterType<ErrorLogBiz>().As< IErrorLogBiz>().InstancePerLifetimeScope();
            builder.RegisterType<ActionLogBiz>().As< IActionLogBiz>().InstancePerLifetimeScope();
            builder.RegisterType<DotNetBiz>().As<IDotNetBiz>().InstancePerLifetimeScope();
            builder.RegisterType<SmsService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<EmailService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ApplicationPasswordHasher>().As<IPasswordHasher>().InstancePerLifetimeScope();
           
            builder.RegisterType<LocalizeUserValidator<ApplicationUser, int>>().As<IIdentityValidator<ApplicationUser>>().InstancePerLifetimeScope().OnActivating(e => e.Instance.Initialize());
            builder.RegisterType<LocalizePasswordValidator>().As<IIdentityValidator<string>>().InstancePerLifetimeScope().OnActivating(e => e.Instance.Initialize());

            builder.RegisterType<ApplicationUserStore>().As<IUserStore<ApplicationUser, int>>().InstancePerLifetimeScope();
            //builder.Register<IdentityFactoryOptions<ApplicationUserManager>>(c => new IdentityFactoryOptions<ApplicationUserManager>()
            //{
            //    DataProtectionProvider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider("Khodkar")
            //});
            builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerLifetimeScope().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            //builder.RegisterType<ApplicationUserManager>().As<UserManager<ApplicationUser, int>>().InstancePerLifetimeScope().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);   

            builder.RegisterType<ApplicationRoleStore>().As<IRoleStore<ApplicationRole, int>>().InstancePerLifetimeScope();
            builder.RegisterType<ApplicationRoleManager>().AsSelf().InstancePerLifetimeScope();

            //builder.RegisterType<GroupStoreBase>().AsSelf().InstancePerLifetimeScope();
            //builder.RegisterType<ApplicationGroupStore>().AsSelf().InstancePerLifetimeScope();
            //builder.RegisterType<ApplicationGroupManager>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerLifetimeScope();

            builder.Register<IAuthenticationManager>(c => HttpContext.Current.GetOwinContext().Authentication).InstancePerLifetimeScope();
            builder.Register<IDataProtectionProvider>(c => app.GetDataProtectionProvider()).InstancePerLifetimeScope();

            //start mvc 
                    // REGISTER CONTROLLERS SO DEPENDENCIES ARE CONSTRUCTOR INJECTED
                    builder.RegisterControllers(typeof(MvcApplication).Assembly);

                    // OPTIONAL: Enable property injection into action filters.
                    builder.RegisterFilterProvider();

            //end mvc
            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
             //dynamic RegisterApiControllers
            builder.RegisterApiControllers(System.IO.Directory.GetFiles(System.Web.Hosting.HostingEnvironment.MapPath("~/Bin"),"KS.WebSiteUI.dll").Select(Assembly.LoadFrom).ToArray());
            
            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(config);

            // register modules
            //builder.RegisterModule(new ConfigurationSettingsReader("autofac"));


            builder.Register(c => new LogMainEntryServiceAttribute(c.Resolve< IActionLogManager>(), c.Resolve<IDataBaseContextManager>()))
            .AsActionFilterFor<MvcController>(c=> c.GetWebPage(default(string)))
            .InstancePerLifetimeScope();

            builder.Register(c => new AuthorizeByLogAndCheckCacheOfServiceAttribute(c.Resolve<IErrorLogManager>()
                , c.Resolve<IActionLogManager>()))
            .AsWebApiAuthorizationFilterFor<BaseAuthorizedWebApiController>()
            .InstancePerRequest();

            builder.Register(c => new AuthorizeByLogOfODataServiceAttribute(c.Resolve<IErrorLogManager>()
                , c.Resolve<IActionLogManager>()))
           .AsWebApiAuthorizationFilterFor<BaseAuthorizedODataController>()
           .InstancePerRequest();

            builder.Register(c => new LogOfODataServiceAttribute(c.Resolve<IErrorLogManager>(), c.Resolve<IDataBaseContextManager>()
                , c.Resolve<IActionLogManager>()))
            .AsWebApiActionFilterFor<BasePublicODataController>()
            .InstancePerRequest();

            builder.Register(c => new LogAndCheckCacheOfServiceAttribute(c.Resolve<IErrorLogManager>(), c.Resolve<IDataBaseContextManager>()
                 , c.Resolve<IActionLogManager>()))
             .AsWebApiActionFilterFor<BasePublicWebApiController>()
             .InstancePerRequest();
             
            builder.Register(c => new ExceptionFilterAttribute())
            .AsWebApiExceptionFilterFor<BasePublicWebApiController>()
            .InstancePerRequest();

            builder.Register(c => new ExceptionFilterAttribute())
           .AsWebApiExceptionFilterFor<BasePublicODataController>()
           .InstancePerRequest();

            builder.Register(c => new ExceptionFilterAttribute())
           .AsWebApiExceptionFilterFor<BaseAuthorizedODataController>()
           .InstancePerRequest();

            builder.Register(c => new ExceptionFilterAttribute())
          .AsWebApiExceptionFilterFor<BaseAuthorizedWebApiController>()
          .InstancePerRequest();

            // Autofac will add middleware to IAppBuilder in the order registered.
            // The middleware will execute in the order added to IAppBuilder.
            builder.RegisterType<ActionLogMiddleware>().InstancePerLifetimeScope();
            builder.RegisterType<ActionLogSpecialServicesMiddleware>().InstancePerLifetimeScope();
            builder.RegisterType<LanguageAndCultureMiddleware>().InstancePerLifetimeScope();

            builder.RegisterType<WebPageMiddleware>().InstancePerLifetimeScope();

            // BUILD THE CONTAINER
            var container = builder.Build();

            // REPLACE THE MVC DEPENDENCY RESOLVER WITH AUTOFAC
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            //******************************************************************************************
            // REPLACE THE WebApi DEPENDENCY RESOLVER WITH AUTOFAC
              config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            //******************************************************************************************

            // OWIN WEB API SETUP:

            // Register the Autofac middleware FIRST, then the Autofac Web API middleware,
            // and finally the standard Web API middleware.


            // This adds ONLY the Autofac lifetime scope to the pipeline.
            app.UseAutofacLifetimeScopeInjector(container);
            // REGISTER WITH OWIN
            //app.UseAutofacMiddleware(container);

            app.UseMiddlewareFromContainer<LanguageAndCultureMiddleware>();
            app.UseMiddlewareFromContainer<ActionLogMiddleware>();
            app.UseMiddlewareFromContainer<ActionLogSpecialServicesMiddleware>();
            app.UseMiddlewareFromContainer<WebPageMiddleware>();
            ConfigureAuth(app);
            
             // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
            
            app.UseAutofacMvc();
            app.UseAutofacWebApi(config);
           app.UseWebApi(config);
           
           
            //log StartUp
            var dataTime = DateTime.UtcNow;

            var actionLogManager = new ActionLogManager(new FileSystemManager());
            actionLogManager.Log(new ActionLog()
            {
                DateTime = dataTime,
                Ip = CurrentUserManager.Ip,
                IsDebugMode = true,
                IsMobileMode = Settings.IsMobileMode,
                LocalDateTime = LanguageManager.ToLocalDateTime(dataTime),
                Name = "StartUp",
                ServiceUrl = "/StartUp",
                Url = "/StartUp",
                User = CurrentUserManager.UserName,
                IsSuccessed = true,
                ExecutionTimeInMilliseconds = (DateTime.Now.TimeOfDay - TimeSpan.Parse(startTime)).TotalMilliseconds
        });
           
        }
    }
    
 public partial class Startup
    {
        public void GlobalRegistration(HttpConfiguration config)
        {
           

            AreaRegistration.RegisterAllAreas();
            //GlobalConfiguration.Configure(WebApiConfig.Register);
            WebApiConfig.Register(config);
            // GlobalConfiguration.Configure(Core.Log.WebApiExceptionLogger.Register);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            JsEngineSwitcherConfig.Configure(JsEngineSwitcher.Instance);
            ClientDataTypeModelValidatorProvider.ResourceClassKey = "Resources.ErrorMessages";
            DefaultModelBinder.ResourceClassKey = "Resources.ErrorMessages";
        }

        void RegisterConfig()
        {
            Config.LocalTimeZoneId = ConfigurationManager.AppSettings["LocalTimeZoneId"];

            Config.PagesSourceCodePath = ConfigurationManager.AppSettings["PagesSourceCodePath"];

            Config.ScriptDebugPath = ConfigurationManager.AppSettings["ScriptDebugPath"];
            Config.ScriptDistPath = ConfigurationManager.AppSettings["ScriptDistPath"];
            Config.StyleDebugPath = ConfigurationManager.AppSettings["StyleDebugPath"];
            Config.StyleDistPath = ConfigurationManager.AppSettings["StyleDistPath"];
            Config.ResourcesSourceCodePath = ConfigurationManager.AppSettings["ResourcesSourceCodePath"];
            Config.StyleDebugPagesPath = ConfigurationManager.AppSettings["StyleDebugPagesPath"];
            Config.StyleDistPagesPath = ConfigurationManager.AppSettings["StyleDistPagesPath"];
            Config.ThumbnailPath = ConfigurationManager.AppSettings["ThumbnailPath"];
            Config.LanguageAndCultures = ConfigurationManager.AppSettings[ConfigKey.LanguageAndCultures.ToString()].Split(',').ToList();
            Config.QueryStringSign = ConfigurationManager.AppSettings["QueryStringSign"];
            Config.MobileSign = ConfigurationManager.AppSettings["MobileSign"];
            Config.MobileFallBack = Convert.ToBoolean(ConfigurationManager.AppSettings["MobileFallBack"]);
            Config.PagesPath = ConfigurationManager.AppSettings["PagesPath"];
            Config.LogOffSign = ConfigurationManager.AppSettings["LogOffSign"];
            Config.UrlDelimeter = ConfigurationManager.AppSettings["UrlDelimeter"];
            Config.ServicesSourceCodePath = ConfigurationManager.AppSettings["ServicesSourceCodePath"];
            Config.ResourcesDistPath = ConfigurationManager.AppSettings["ResourcesDistPath"];
            Config.DefaultsLanguage = ConfigurationManager.AppSettings["DefaultsLanguage"];
            Config.ScriptDebugPagesPath = ConfigurationManager.AppSettings["ScriptDebugPagesPath"];
            Config.ScriptDistPagesPath = ConfigurationManager.AppSettings["ScriptDistPagesPath"];
            Config.LoginExpireTimeSpanInHours = Convert.ToInt32(ConfigurationManager.AppSettings["LoginExpireTimeSpanInHours"]);
            Config.LanguageAndCultureCoockie = ConfigurationManager.AppSettings["LanguageAndCultureCoockie"];
            Config.IsDebugModeCoockie = ConfigurationManager.AppSettings["IsDebugModeCoockie"];
            Config.IsMobileModeCoockie = ConfigurationManager.AppSettings["IsMobileModeCoockie"];
            Config.IsAuthenticatedCoockie = ConfigurationManager.AppSettings["IsAuthenticatedCoockie"];
            Config.DebugIdSign = ConfigurationManager.AppSettings["DebugIdSign"];
            Config.BrowserCodeDependencyEngineSourcePath = ConfigurationManager.AppSettings["BrowserCodeDependencyEngineSourcePath"];
            Config.LogPath = ConfigurationManager.AppSettings["LogPath"];
            Config.LogBackgroundJobIntervalsInMilliseconds
          = Convert.ToInt32(ConfigurationManager.AppSettings["LogBackgroundJobIntervalsInMilliseconds"]);
            Config.ErrrorPagesBaseUrl = ConfigurationManager.AppSettings["ErrrorPagesBaseUrl"];
            Config.SourceCodeDeletedPath = ConfigurationManager.AppSettings["SourceCodeDeletedPath"];
            var url = ConfigurationManager.AppSettings["$.asUrls.frameWorkServices_getWebPage"];
            Config.DefaultsGetWebPagesServiceUrl = url.IndexOf("@", StringComparison.Ordinal) > 0 ? url.Substring(0, url.IndexOf("@", StringComparison.Ordinal)) : url;
            Config.LoginUrl = ConfigurationManager.AppSettings["LoginUrl"];
            Config.AspectCacheSlidingExpirationTimeInMinutes = Convert.ToInt32(ConfigurationManager.AppSettings["AspectCacheSlidingExpirationTimeInMinutes"]);
            Config.GroupCacheSlidingExpirationTimeInMinutes = Convert.ToInt32(ConfigurationManager.AppSettings["GroupCacheSlidingExpirationTimeInMinutes"]);
            Config.EnableActionLog = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableActionLog"]);
        }
    }
    
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }
        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
                
            // Configure the application for OAuth based flow
            PublicClientId = "web";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthAuthorizationServerProvider(PublicClientId),
                //AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                //AuthorizeEndpointPath = new PathString("/api/Account/Authorize"),
               AccessTokenExpireTimeSpan = TimeSpan.FromHours(Config.LoginExpireTimeSpanInHours),
               //AccessTokenExpireTimeSpan = TimeSpan.FromSeconds(45),
                AuthenticationType = OAuthDefaults.AuthenticationType,
                AllowInsecureHttp = true,
                
            };
            
            //Enabling Cross-Origin 
            //app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);



            // Configure the db context, user manager and signin manager to use a single instance per request
            //app.CreatePerOwinContext(DependencyManager.Get<SecurityContext>); 
            //app.CreatePerOwinContext(DependencyManager.Get<ApplicationUserManager>);
            //app.CreatePerOwinContext(DependencyManager.Get<ApplicationSignInManager>);
            //app.CreatePerOwinContext(DependencyManager.Get<ApplicationRoleManager>);
            //app.CreatePerOwinContext(ApplicationDbContext.Create);
            //app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            //app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);
            //app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

    

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString(Config.LoginUrl),
                ExpireTimeSpan = TimeSpan.FromHours(Config.LoginExpireTimeSpanInHours),
                //ExpireTimeSpan = TimeSpan.FromSeconds(45),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator
                        // ADD AN INT AS A THIRD TYPE ARGUMENT:

            .OnValidateIdentity<ApplicationUserManager, ApplicationUser, int>(

                validateInterval: TimeSpan.FromHours(Config.LoginExpireTimeSpanInHours),

                // THE NAMED ARGUMENT IS DIFFERENT:

                regenerateIdentityCallback: (manager, user)

                    => user.GenerateUserIdentityAsync(manager),

                    // Need to add THIS line because we added the third type argument (int) above:

                    getUserIdCallback: (claim) => int.Parse(claim.GetUserId()))

                }
            });

        

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);



      
           
            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});

            //app.UseYahooAuthentication("<YOUR CONSUMER KEY>", "<YOUR CONSUMER SECRET>");
        }
    }
    
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
    
        public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
        }
    }
    
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
    
     public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

        }
    }
    
     public class JsEngineSwitcherConfig
    {
        public static void Configure(JsEngineSwitcher engineSwitcher)
        {
            engineSwitcher.EngineFactories
                .AddMsie(new MsieSettings
                {
                    UseEcmaScript5Polyfill = true,
                    UseJson2Library = true
                });

            engineSwitcher.DefaultEngineName = MsieJsEngine.EngineName;
        }
    }
}                         khodkar c# comment*/ 
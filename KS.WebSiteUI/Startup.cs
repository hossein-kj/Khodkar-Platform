
//using System.IO;
//using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using KS.Business.Localization;
using KS.Business.Security;
using KS.Core.Security;
using KS.DataAccess.Contexts;
using KS.Model.Security;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using Owin;
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
using KS.WebSiteUI.Controllers.Base;
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
using System;
using KS.Core.Localization;
using KS.Core.Model.Log;
using KS.Core.UI.Setting;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using KS.Business.Security.Provider;

[assembly: OwinStartup("StaticStartup", typeof(KS.WebSiteUI.Startup))]
namespace KS.WebSiteUI
{
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
            builder.RegisterType<SourceControl>().As<ISourceControl>().InstancePerLifetimeScope();
            builder.RegisterType<RemoteMigration>().As<IMigration>().InstancePerLifetimeScope();
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
            builder.RegisterType<WebPageBiz>().As<IWebPageBiz>().InstancePerLifetimeScope();
            builder.RegisterType<MasterDataKeyValueBiz>().As<IMasterDataKeyValueBiz>().InstancePerLifetimeScope();
            builder.RegisterType<ServiceBiz>().As<IServiceBiz>().InstancePerLifetimeScope();
            builder.RegisterType<LanguageAndCultureBiz>().As<ILanguageAndCultureBiz>().InstancePerLifetimeScope();
            builder.RegisterType<LinkBiz>().As<ILinkBiz>().InstancePerLifetimeScope();
            builder.RegisterType<EntityGroupBiz>().As<IEntityGroupBiz>().InstancePerLifetimeScope();
            builder.RegisterType<FileSystemBiz>().As<IFileSystemBiz>().InstancePerLifetimeScope();
            builder.RegisterType<FilePathBiz>().As<IFilePathBiz>().InstancePerLifetimeScope();
            builder.RegisterType<FileBiz>().As<IFileBiz>().InstancePerLifetimeScope();
            builder.RegisterType<BrowserCodeBiz>().As<IBrowserCodeBiz>().InstancePerLifetimeScope();
            builder.RegisterType<WebConfigBiz>().As<IWebConfigBiz>().InstancePerLifetimeScope();
            builder.RegisterType<PermissionBiz>().As<IPermissionBiz>().InstancePerLifetimeScope();
            builder.RegisterType<SqlServerBiz>().As<ISqlServerBiz>().InstancePerLifetimeScope();
            builder.RegisterType<ErrorLogBiz>().As<IErrorLogBiz>().InstancePerLifetimeScope();
            builder.RegisterType<ActionLogBiz>().As<IActionLogBiz>().InstancePerLifetimeScope();
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

            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(config);

            // register modules
            //builder.RegisterModule(new ConfigurationSettingsReader("autofac"));


            builder.Register(c => new LogMainEntryServiceAttribute(c.Resolve<IActionLogManager>(), c.Resolve<IDataBaseContextManager>()))
            .AsActionFilterFor<MvcController>(c => c.GetWebPage(default(string)))
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


            ConfigureAuth(app);

            app.UseMiddlewareFromContainer<WebPageMiddleware>();

            // Any connection or hub wire up and configuration should go here
            app.Map("/signalr", map =>
            {
                //uncomment blow line if you want cross domain signalR
                //map.UseCors(CorsOptions.AllowAll);

                map.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions()
                {
                    Provider = new QueryStringOAuthBearerProvider()
                });

                map.RunSignalR();
            });

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
}

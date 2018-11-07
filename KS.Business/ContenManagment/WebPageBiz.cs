using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using KS.Core.CacheProvider;
using KS.Model.ContentManagement;
using Newtonsoft.Json.Linq;
using WebPageType = KS.Core.GlobalVarioable.WebPageType;
using KS.Core.Exceptions;
using KS.Core.GlobalVarioable;
using KS.Core.Security;
using KS.Core.Utility;
using KS.Core.Localization;
using KS.Core.CodeManager;
using System.Net;
using EntityFramework.Extensions;
using KS.Business.ContenManagment.Base;
using KS.Core.CodeManager.Base;
using KS.Core.FileSystemProvide.Base;
using KS.Core.Log.Elmah.Base;
using Newtonsoft.Json;
using KS.Core.UI.Setting;
using KS.Core.UI.Configuration;
using KS.Core.CodeManager.BrowsersCode.Base;
using KS.Core.Data.Contexts.Base;
using KS.Core.Model.ContentManagement;
using KS.Core.Model.Core;
using KS.Core.Model.Develop;
using KS.Core.Model.Log;
using KS.DataAccess.Contexts.Base;
using System.Web;

namespace KS.Business.ContenManagment
{
    public class WebPageBiz : IWebPageBiz
    {

        private readonly IContentManagementContext _contentManagementContext;
        private readonly ISecurityContext _securityContext;
        private readonly IFileSystemManager _fileSystemManager;
        private readonly ISourceControl _sourceControl;
        private readonly IErrorLogManager _errorLogManager;
        private readonly IWebConfigManager _webConfigManager;
        private readonly ICompressManager _compressManager;
        private readonly ICodeTemplate _codeTemplate;
        private readonly IDataBaseContextManager _dataBaseContextManager;
        public WebPageBiz(IContentManagementContext contentManagementContext,
            ISecurityContext securityContext,IErrorLogManager errorLogManager
            , ISourceControl sourceControl, IFileSystemManager fileSystemManager
            , IWebConfigManager webConfigManager, ICompressManager compressManager, ICodeTemplate codeTemplate
            , IDataBaseContextManager dataBaseContextManager)
        {
            _contentManagementContext = contentManagementContext;
            _errorLogManager = errorLogManager;
            _sourceControl = sourceControl;
            _fileSystemManager = fileSystemManager;
            _webConfigManager = webConfigManager;
            _compressManager = compressManager;
            _codeTemplate = codeTemplate;
            _dataBaseContextManager = dataBaseContextManager;
            _securityContext = securityContext;
        }

        public JObject GetWebPageChangesFromSourceControl(string orderBy, int skip, int take
           , string comment
            ,string user
           , string fromDateTime
           , string toDateTime
           , string webPageGuid
            ,string type)
        {


            var count = 0;


            return JObject.Parse(JsonConvert.SerializeObject
            (new
            {
                rows = _sourceControl.GeChangesByPagination(orderBy,
                        skip,
                        take,
                        Config.PagesSourceCodePath + webPageGuid + "/",
                        webPageGuid + "." + type,
                        comment,
                        user,
                        fromDateTime.Replace("-", Helper.RootUrl).Replace("_", ":"),
                        toDateTime.Replace("-", Helper.RootUrl).Replace("_", ":"),
                        out count)
                    .Select(sr => new
                    {
                        Id = sr.Id.ToString().Trim(),
                        sr.LocalDateTime,
                        sr.Comment,
                        sr.Version,
                        sr.User,
                        sr.DateTime
                    }),
                total = count
            }, Formatting.None));
        }

        protected async Task WriteFileAsync(string path, string name, string extention,
        string content, bool creatDirectoryIfNotExist = false)
        {
            path = path.Replace("//", "/");



            await
                _fileSystemManager.WriteAsync(
                (creatDirectoryIfNotExist
                    ? _fileSystemManager.CreatDirectoryIfNotExist(path)
                    : _fileSystemManager.RelativeToAbsolutePath(path)) + name + extention, content);
        }


        protected bool DeleteFile(string path, string name, string extention)
        {


            path = path.Replace("//", "/");


            return _fileSystemManager.DeleteFile(path + name + extention);
        }
        private async Task<WebPage> GetDefaultsFrameWorkAsync(string frameWorkUrl)
        {
            WebPage frameWork;
            if (frameWorkUrl == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.FrameWorkNotFound));

            var key = CacheManager.GetWebPageKey(WebPageType.FrameWork.ToString(), frameWorkUrl);
            var defaultsFrameWorkCache = CacheManager.Get<WebPage>(key);
            if (defaultsFrameWorkCache.IsCached)
            {
                frameWork = defaultsFrameWorkCache.Value;
            }
            else
            {
                frameWork =
                    await
                        _contentManagementContext.WebPages.Where(
                                fr =>
                                    fr.Url == frameWorkUrl && fr.Status == 1 &&
                                    fr.TypeId == (int) WebPageType.FrameWork)
                            .FirstOrDefaultAsync();
                if (frameWork == null)
                {
                    throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.FrameWorkNotFound));
                }

                if (frameWork.EnableCache)
                    CacheManager.StoreForEver(key, frameWork);
            }


            if (!Settings.IsDebugMode)
            {

                return frameWork;
            }





            return (await GetWebPageSourceAsync(frameWork.Guid, true)) ?? frameWork;
        }

        //private async Task<WebPage> GetWebPageFromDataBaseAsync(string url, bool isModal = false)
        //{

        //    var pageType = isModal ? (int) WebPageType.Modal : (int) WebPageType.Form;

        //    var webPage =
        //        await
        //            _contentManagementContext.WebPages.Where(
        //                    fr => fr.Url.ToLower() == url.ToLower() && fr.Status == 1 && fr.TypeId == pageType)
        //                .FirstOrDefaultAsync();
        //    if (webPage == null)
        //        throw new PageNotFoundException();

        //    if (AuthorizeManager.IsAuthorize(webPage.ViewRoleId))
        //        return webPage;
        //    throw new UnauthorizedAccessException(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidGrant));
        //}

        private JObject GetWebPage(string url, string type, IAspect aspect)
        {

            if (!aspect.HasMobileVersion && Config.MobileFallBack)
            {
                url = (url + @"/").EndsWith(Config.MobileSign) ?
                    url.Replace(Config.MobileSign.Substring(0, Config.MobileSign.Length-1), "").Replace("//", "/")
                    : url.Replace(Config.MobileSign, Helper.RootUrl).Replace("//", "/");
                url = url.EndsWith("/") ? url.Substring(0, url.Length -1 ) : url;
            }


            IWebPageCore webPage;
            if (!aspect.EnableCache)
            {
                webPage = _dataBaseContextManager.GetWebPageForView(url, type);

            }
            else
            {
                var key = CacheManager.GetWebPageKey(type, url);
                var pageCache = CacheManager.Get<dynamic>(key);

                if (!pageCache.IsCached)
                {
                    webPage = _dataBaseContextManager.GetWebPageForView(url, type);
                    CacheManager.Store(key, webPage.ToJObject(), slidingExpiration:
                        TimeSpan.FromMinutes(aspect.CacheSlidingExpirationTimeInMinutes));
                }
                else
                {
                    return (JObject) pageCache.Value;
                }

            }
            return webPage.ToJObject();
        }

        private JObject ReturnErrorPage(HttpStatusCode code, string pageType)
        {


            try
            {
                var type = ((pageType ?? WebPageType.Form.ToString()) == WebPageType.Modal.ToString()
                    ? WebPageType.Modal
                    : WebPageType.Form).ToString();
                var pagePath = Config.ErrrorPagesBaseUrl + (int) code;
                if (code == HttpStatusCode.Unauthorized)
                {
                    pagePath = Config.LoginUrl;
                }

                pagePath = ("/" + LanguageManager.ApplyLanguageAndMobileSignToAjaxRequestAsync
                                (pagePath.Replace(Config.UrlDelimeter, Helper.RootUrl).Replace("#", ""))).Replace("//",
                    "/");

                IAspect aspect;
                AuthorizeManager.AuthorizeWebPageUrl(pagePath, type, out aspect);


                if (!aspect.HasMobileVersion)
                {
                    pagePath = (pagePath + @"/").EndsWith(Config.MobileSign) ?
                   pagePath.Replace(Config.MobileSign.Substring(0, Config.MobileSign.Length - 1), "").Replace("//", "/")
                   : pagePath.Replace(Config.MobileSign, Helper.RootUrl).Replace("//", "/");

                    //pagePath = pagePath.Replace(Config.MobileSign, Helper.RootUrl);
                }

                return GetWebPage(pagePath, type, aspect);
            }
            catch (Exception ex)
            {
                _errorLogManager.LogException(new ExceptionLog()
                {
                    Detail = ex.ToString(),
                    Message = ex.Message,
                    Source = ex.GetType().FullName
                });



                return JObject.Parse(JsonConvert.SerializeObject
                (new
                    {
                        modifyRoleId = 5,
                        viewRoleId = 6,
                        enableCache = false,
                        status = 1,
                        title = "Error!",
                        cacheSlidingExpirationTimeInMinutes = 0,
                        pageId = "if29a53784fb34da1806c6ce945790dc5",
                        dependentModules = "[]",
                        param = "{}",
                        html =
                        " <span style='display:none' id='if29a53784fb34da1806c6ce945790dc5'></span>Error!"
                    }, Formatting.None,
                    new JsonSerializerSettings() {ReferenceLoopHandling = ReferenceLoopHandling.Ignore}));
            }


        }

        public async Task<JObject> GetWebPageForViewAsync(string url, bool isModal = false)
        {
            //for better unAuthroized Message  or in case if you dont use coockie auth for better performance
            //if (Setting.IsAuthenticated && !CurrentUserManager.IsAuthenticated)
            //    throw new UnauthorizedAccessException(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidGrant));


            url = LanguageManager.ApplyLanguageAndMobileSignToAjaxRequestAsync(url);



            var type = (isModal ? WebPageType.Modal : WebPageType.Form).ToString();
            IAspect aspect;
            if (AuthorizeManager.AuthorizeWebPageUrl(url, type, out aspect) || aspect.IsNull)
            {       

                if (aspect.IsNull)
                {

                    return ReturnErrorPage(HttpStatusCode.NotFound, type);
                }

                try
                {
                    dynamic webPageJson = GetWebPage(url, type, aspect);

                    if (!Settings.IsDebugMode)
                    {
                        return (JObject)webPageJson;
                    }

                    string guid = webPageJson.pageId;
                    var webPageDebug = await GetWebPageSourceAsync(guid.Substring(1));
                    if (webPageDebug == null)
                        return (JObject)webPageJson;

                    return await ConvertToJsonAsync(WebPageJsonType.Debug, webPage: webPageDebug);
                }
                catch (Exception ex)
                {
                    _errorLogManager.LogException(new ExceptionLog()
                    {
                        Detail = ex.ToString(),
                        Message = ex.Message,
                        Source = ex.GetType().FullName
                    });
                    return ReturnErrorPage(HttpStatusCode.NotFound, type);

                }
            }
            return ReturnErrorPage(HttpStatusCode.Unauthorized, type);

        }

        public async Task<JObject> GetWebPageForDebugAsync(string url, bool isModal = false)
        {
            try
            {
                if (!Settings.IsDebugMode)
                {
                    throw new PageNotFoundException("Not DebugMod In GetWebPageForViewAsync Method in WebPageBiz");
                }
                return await GetWebPageForViewAsync(url, isModal);
            }
            catch (PageNotFoundException ex)
            {
                _errorLogManager.LogException(new ExceptionLog()
                {
                    Detail = ex.ToString(),
                    Message = ex.Message,
                    Source = ex.GetType().FullName
                });
                return ReturnErrorPage(HttpStatusCode.NotFound, (isModal ? WebPageType.Modal : WebPageType.Form).ToString());
            }
        }

        private async Task<WebPage> GetWebPageSourceAsync(string guid, bool isFrameWork = false)
        {
            try
            {
                dynamic webPageJson = JObject.Parse(await
                _fileSystemManager.ReadAsync(GetWebPageSourceCodePath(guid, SourceType.Json)));

            if (webPageJson == null)
                return null;



        
                if (!AuthorizeManager.IsAuthorizeToViewSourceCodeOfWebPage(Convert.ToString(webPageJson.Guid)))
                    return null;
                var webPageServices = "";
                if (isFrameWork)
                {
                    JArray servicesCodeArray = webPageJson.Services;
                    var servicesCode = servicesCodeArray.ToObject<List<string>>();
                    var services = await
                        _contentManagementContext.MasterDataKeyValues
                        .Where(sr => servicesCode.Contains(sr.Code) && sr.TypeId==(int)EntityIdentity.Service)
                            .Select(sr => new {sr.Code, sr.PathOrUrl}).ToListAsync();

                    var webFormServices = services.Aggregate("",
                        (current, service) => current + ("" + service.Code.Remove(0, 9) +
                                                         ":\"" + service.PathOrUrl + "\","));
                    if (webFormServices.Length > 0)
                        webPageServices = webFormServices.Remove(webFormServices.Length - 1);
                }


                return new WebPage()
                {
                    Id = webPageJson.Id,
                    Html = webPageJson.Html,
                    Url = webPageJson.Url,
                    EditMode = webPageJson.EditMode,
                    Params = webPageJson.Params,
                    FrameWorkUrl = webPageJson.FrameWorkUrl,
                    TemplatePatternUrl = webPageJson.TemplatePatternUrl,
                    Tools = webPageJson.Tools,
                    ViewRoleId = webPageJson.ViewRoleId,
                    ModifyRoleId = webPageJson.ModifyRoleId,
                    AccessRoleId = webPageJson.AccessRoleId,
                    Title = webPageJson.Title,
                    Guid = webPageJson.Guid,
                    Version = webPageJson.Version,
                    EnableCache = webPageJson.EnableCache,
                    CacheSlidingExpirationTimeInMinutes = webPageJson.CacheSlidingExpirationTimeInMinutes,
                    Services = webPageServices,
                    IsMobileVersion = webPageJson.IsMobileVersion,
                    Status = webPageJson.Status,
                    TypeId = webPageJson.TypeId,
                    HaveScript = webPageJson.HaveScript,
                    HaveStyle = webPageJson.HaveStyle,
                    DependentModules = webPageJson.DependentModules ?? "[]",
                    Language = webPageJson.Language,
                    RowVersion = webPageJson.RowVersion,
                };
            }
            catch (Exception ex)
            {
                _errorLogManager.LogException(new ExceptionLog()
                {
                    Detail = ex.ToString(),
                    Message = ex.Message,
                    Source = ex.GetType().FullName
                });

                return null;
            }
        }

        //private async Task<JObject> GetWebPageForViewFromFileAsync(string url, bool isModal = false)
        //{
        //    var pageType = isModal ? WebPageType.Modal : WebPageType.Form;
        //    var key = CacheManager.GetWebPageKey(pageType.ToString(), url);
        //    var pageCache = CacheManager.Get<dynamic>(key);
        //    dynamic webPage;
        //    if (!pageCache.IsCached)
        //    {

        //        webPage = JObject.Parse(await ReadWebPageAsync(url, isModal));
        //        if (webPage == null)
        //            throw new PageNotFoundException();

        //        if ((bool)webPage.enableCache)
        //            CacheManager.Store(key, webPage,slidingExpiration: TimeSpan.FromMinutes((double)webPage.cacheSlidingExpirationTimeInMinutes));
        //    }
        //    else
        //    {
        //        webPage = JObject.Parse(pageCache.Value.ToString());
        //    }
        //    if ((int)webPage.status == 0)
        //        throw new PageTemporaryInaccessibleException();
        //    if (AuthorizeManager.IsAuthorize((int)webPage.viewRoleId))
        //        return webPage;

        //    throw new UnauthorizedAccessException(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidGrant));
        //}

        //public async Task<JObject> GetWebPageForEditAsync(string url, int typeId)
        //{
        //    var fullUrl = LanguageManager.ApplyLanguageAndMobileSignToAjaxRequestAsync(url);

        //    var webPage =
        //        await
        //            _contentManagementContext.WebPages.Where(
        //                    fr => fr.Url.ToLower() == fullUrl.ToLower() && fr.TypeId == typeId)
        //                .FirstOrDefaultAsync();
        //    if (webPage == null)
        //        return null;
        //    AuthorizeManager.CheckViewAccess(webPage);
        //    return await ConvertToJsonAsync(WebPageJsonType.Edit, webPage);

        //}

        public async Task<JObject> GetWebPageForEditAsync(string url, int typeId)
        {
           // var fullUrl = LanguageManager.ApplyLanguageAndMobileSignToAjaxRequestAsync(url);

            var webPage =
                await
                    _contentManagementContext.WebPages.Where(
                            fr => fr.Url.ToLower() == url.ToLower() && fr.TypeId == typeId)
                        .FirstOrDefaultAsync();
            if (webPage == null)
                return null;
            var resource = await GetResorces(webPage);


            dynamic webPageJson = JObject.Parse(await
                _fileSystemManager.ReadAsync(GetWebPageSourceCodePath(webPage.Guid, SourceType.Json)));

            if (webPageJson == null)
                return null;

            if(!AuthorizeManager.IsAuthorizeToViewSourceCodeOfWebPage(webPage.Guid))
                throw new UnauthorizedAccessException(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidGrant));
            webPageJson.RowVersion =webPage.RowVersion;
            webPageJson.JavaScript = resource.Javascript;
            webPageJson.Style = resource.Style;
            var lastModifieUser =
                await _securityContext.Users.SingleOrDefaultAsync(us => us.Id == webPage.CreateUserId);
            
            webPageJson.LastModifieUser = lastModifieUser.UserName;
            webPageJson.LastModifieLocalDateTime = webPage.ModifieLocalDateTime;
            return (JObject)webPageJson;

        }

        public async Task<JObject> GetWebPageChangeFromSourceControlAsync(int changeId,string webPageGuid)
        {

            var webPage =
                await
                    _contentManagementContext.WebPages.Where(
                            fr => fr.Guid.ToLower() == webPageGuid.ToLower())
                        .FirstOrDefaultAsync();
            if (webPage == null)
                return null;

            if (!AuthorizeManager.IsAuthorizeToViewSourceCodeOfWebPage(webPage.Guid))
                throw new UnauthorizedAccessException(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidGrant));

            var change = _sourceControl.GeChangeById(changeId, Config.PagesSourceCodePath + webPageGuid + "/");
            if(change == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.ChangeNotFound));

            if (change.Code != null)
            {
                dynamic webpageJson = JObject.Parse(change.Code);
                var lastModifieUser =
    await _securityContext.Users.SingleOrDefaultAsync(us => us.Id == webPage.CreateUserId);

                webpageJson.LastModifieUser = lastModifieUser.UserName;
                webpageJson.LastModifieLocalDateTime = webPage.ModifieLocalDateTime;

                return (JObject)webpageJson;
            }

            throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.ChangeHasNoCode));
        }

        public async Task<string> GetWebPageResourcesChangeFromSourceControlAsync(int changeId, string webPageGuid)
        {

            var webPage =
                await
                    _contentManagementContext.WebPages.Where(
                            fr => fr.Guid.ToLower() == webPageGuid.ToLower())
                        .FirstOrDefaultAsync();
            if (webPage == null)
                return null;

            if (!AuthorizeManager.IsAuthorizeToViewSourceCodeOfWebPage(webPage.Guid))
                throw new UnauthorizedAccessException(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidGrant));

            var change = _sourceControl.GeChangeById(changeId, Config.PagesSourceCodePath + webPageGuid + "/");
            if (change == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.ChangeNotFound));

            if (change.Code == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.ChangeHasNoCode));
            return change.Code;

        }


        private string ConvertToValidId(string id)
        {
            return "i" + id;
        }
        public async Task<bool> Delete(JObject data)
        {
            dynamic webPageData = data;
            int id;

            try
            {
                id = webPageData.Id;
            }
            catch (Exception)
            {
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.FieldMustBeNumeric, "WebPage Id"));

            }
            var webPage = await _contentManagementContext.WebPages.SingleOrDefaultAsync(wf => wf.Id == id);

            if (webPage == null)
                throw new PageNotFoundException();

            AuthorizeManager.SetAndCheckModifyAndAccessRole(webPage, null, false);


            if (webPage.EditMode)
            {
                _sourceControl.CheckCodeCheckOute(webPage);

            }

            await RemoveCache(webPage);

            _contentManagementContext.WebPages.Remove(webPage);

            await _contentManagementContext.SaveChangesAsync();

            var removeDirectory = (Config.PagesSourceCodePath + webPage.Guid + "/").Replace("//", "/");

            if (webPage.TypeId == (int)WebPageType.Form || webPage.TypeId == (int)WebPageType.Modal)
                DeleteWebPage(webPage.Url, webPage.TypeId == (int)WebPageType.Modal);

            _sourceControl.RecycleBin(Config.PagesSourceCodePath, webPage.Guid);

            _fileSystemManager.DeleteDirectory(removeDirectory);

            if (webPage.HaveScript && webPage.TypeId != (int)WebPageType.FrameWork)
            {
                 DeleteFile(Config.ScriptDebugPagesPath, webPage.Guid, ".js");
                DeleteFile(Config.ScriptDistPagesPath, webPage.Guid, ".js");
            }

            if (webPage.HaveStyle)
            {
                DeleteFile(Config.StyleDebugPagesPath, webPage.Guid, ".css");
                DeleteFile(Config.StyleDistPagesPath, webPage.Guid, ".css");
            }

           
            return true;
        }

        //public async Task<WebPage> Save(JObject data)
        //{


        //    dynamic webPageDto = data;
        //    JArray servicesCodeArray = webPageDto.Services;
        //    var servicesCode = servicesCodeArray.ToObject<List<string>>();




        //    var services = await
        //        _contentManagementContext.Services.Where(sr => servicesCode.Contains(sr.Code)).Select(sr => new { sr.Code, sr.PathOrUrl }).ToListAsync();
        //    var webPage = new WebPage
        //    {
        //        Id = webPageDto.Id,
        //        RowVersion = webPageDto.RowVersion
        //    };

        //    bool isPublish = webPageDto.Publish;
        //    if (webPage.Id > 0)
        //    {
        //        webPage = await _contentManagementContext.WebPages.SingleOrDefaultAsync(wf => wf.Id == webPage.Id);

        //        if (webPage == null)
        //            throw new PageNotFoundException();
        //        //if (webPage.Status != Convert.ToInt32(webPageDto.Status) && !isPublish)
        //        //{
        //        //    webPage.Status = Convert.ToInt32(webPageDto.Status);
        //        //    await PublishWebPageAsync(webPage.Url, webPage, webPage.TypeId == (int)WebPageType.Modal);
        //        //}
        //    }
        //    else
        //        _contentManagementContext.WebPages.Add(webPage);

        //    if (webPage.EditMode)
        //    {
        //        _sourceControl.CheckCodeCheckOute(webPage);

        //    }

        //    try
        //    {
        //        webPage.CacheSlidingExpirationTimeInMinutes = webPageDto.CacheSlidingExpirationTimeInMinutes;
        //    }
        //    catch (Exception)
        //    {
        //        webPage.CacheSlidingExpirationTimeInMinutes = 0;
        //    }
        //    webPage.Html = webPageDto.Html;
        //    webPage.Url = webPageDto.Url;
        //    string javaScript = webPageDto.JavaScript;
        //    string style = webPageDto.Style;
        //    webPage.TemplatePatternUrl = webPageDto.TemplatePatternUrl;
        //    webPage.Params = webPageDto.Params;
        //    webPage.FrameWorkUrl = webPageDto.FrameWorkUrl;

        //    webPage.DependentModules = webPageDto.DependentModules;

        //    AuthorizeManager.SetAndCheckAccess(webPage,webPageDto);


        //    webPage.Title = webPageDto.Title;
        //    webPage.Status = webPageDto.Status;
        //    webPage.TypeId = webPageDto.TypeId;
        //    webPage.EnableCache = webPageDto.EnableCache;
        //    webPage.EditMode = webPageDto.EditMode;
        //   webPage.IsMobileVersion = webPageDto.IsMobileVersion;
        //    if (webPage.Id == 0)
        //        webPage.Guid = webPageDto.Guid;

        //    var webFormServices = services.Aggregate("", (current, service) => current + ("" + service.Code.Remove(0, 9) +
        //    ":\"" + service.PathOrUrl + "\","));
        //    if (webFormServices.Length > 0)
        //        webPage.Services = webFormServices.Remove(webFormServices.Length - 1);
        //    webPage.Version += 1;
        //    var error = new List<string>();

        //    if (webPage.TypeId == (int) WebPageType.FrameWork)
        //    {
        //        if (webPage.Html.IndexOf(CodeTemplate.Title, StringComparison.Ordinal) == -1)
        //            error.Add(CodeTemplate.Title);                  
        //        if (webPage.Html.IndexOf(CodeTemplate.Style, StringComparison.Ordinal) == -1)
        //            error.Add(CodeTemplate.Style);
        //        if (webPage.Html.IndexOf(CodeTemplate.PageId, StringComparison.Ordinal) == -1)
        //            error.Add(CodeTemplate.PageId);
        //        if (webPage.Html.IndexOf(CodeTemplate.JavaScript, StringComparison.Ordinal) == -1)
        //            error.Add(CodeTemplate.JavaScript);
        //    }

        //    if (webPage.TypeId == (int)WebPageType.Template)
        //    {
        //        if (webPage.Html.IndexOf(CodeTemplate.PlaceHolder, StringComparison.Ordinal) == -1)
        //            error.Add(CodeTemplate.PlaceHolder);

        //    }
        //    if (error.Count > 0)
        //        throw new CodeTemplateException(string.Join(",", error.ToArray()));

        //    webPage.HaveScript = !string.IsNullOrEmpty(javaScript.Trim());
        //    webPage.HaveStyle = !string.IsNullOrEmpty(style.Trim());
        //    webPage.Language = Setting.Language;

        //    await _contentManagementContext.SaveChangesAsync();
        //    await SaveWebPageSourcesAsJsonAsync(webPage);

        //    if (webPage.HaveScript)
        //        await WriteFileAsync(Config.PagesSourceCodePath+ webPage.Guid + "/", webPage.Guid , ".js", javaScript,true);
        //    else
        //    {
        //        if (await _fileSystemManager.FileExistAsync(Config.ScriptDebugPagesPath+ webPage.Guid+ ".js"))
        //        {
        //            DeleteFile(Config.ScriptDebugPagesPath, webPage.Guid, ".js");
        //            DeleteFile(Config.ScriptDistPagesPath, webPage.Guid, ".js");
        //            DeleteFile(Config.PagesSourceCodePath + webPage.Guid + "/" , webPage.Guid, ".js");
        //        }
        //    }
        //    if (webPage.HaveStyle)
        //        await WriteFileAsync(Config.PagesSourceCodePath+ webPage.Guid + "/", webPage.Guid ,".css", style,true);
        //    else
        //    {

        //        if (await _fileSystemManager.FileExistAsync(Config.StyleDebugPagesPath + webPage.Guid + ".css"))
        //        {
        //            DeleteFile(Config.StyleDebugPagesPath, webPage.Guid, ".css");
        //            DeleteFile(Config.StyleDistPagesPath, webPage.Guid, ".css");
        //            DeleteFile(Config.PagesSourceCodePath + webPage.Guid + "/", webPage.Guid, ".css");
        //        }
        //    }
        //    if (isPublish)
        //    {
        //        if (webPage.TypeId == (int)WebPageType.Form || webPage.TypeId == (int)WebPageType.Modal)
        //        {
        //            await PublishWebPageAsync(webPage.Url, webPage.TypeId == (int)WebPageType.Modal);
        //            //await PublishWebPageAsync(webPage.Url, webPage, webPage.TypeId == (int)WebPageType.Modal);
        //        }








        //    }


        //    if (webPage.HaveScript && webPage.TypeId != (int)WebPageType.FrameWork)
        //    {
        //        var pageEvent = "'#" + ConvertToValidId(webPage.Guid) + "'";

        //        var start = " $(" + pageEvent + ").on($.asEvent.page.loaded, function (event,requestedUrl,asPageParams) {" +
        //            "---debug---" + "var asPageEvent = "
        //            + pageEvent + "; var asPageId = '." + ConvertToValidId(webPage.Guid)  +
        //            ".' + $.asPageClass; var as = function(id){ return $(asPageId).find(id);};" +
        //            "var asOnPageDispose = function(){}; $(asPageEvent).on($.asEvent.page.dispose, function (event) { asOnPageDispose()}); ";
        //        var finalScript =
        //            start +


        //          GetServiceSectionOfScript(webPage) + javaScript + @" ; if (typeof (requestedUrl) != 'undefined')  
        //        {$.asLoadPage(requestedUrl,requestedUrl.replace(/\//g, $.asUrlDelimeter));} });";
        //        CompressManager.CheckJavaScriptCode(finalScript.Replace("---debug---", ""), webPage.Guid);
        //        var miniJavaScript = CompressManager.CompressJavaScript(finalScript.Replace("---debug---",""), webPage.Guid);

        //        await WriteFileAsync(Config.ScriptDebugPagesPath , webPage.Guid , ".js",
        //               finalScript.Replace("---debug---", " console.log('" + webPage.Title + "');"));
        //        if (isPublish)
        //            await WriteFileAsync(Config.ScriptDistPagesPath, webPage.Guid, ".js",miniJavaScript);
        //    }

        //    if (webPage.HaveStyle)
        //    {
        //        var miniStyle = CompressManager.CompressCss(style);
        //            await WriteFileAsync(Config.StyleDebugPagesPath , webPage.Guid , ".css", style);
        //        if (isPublish)
        //            await WriteFileAsync(Config.StyleDistPagesPath, webPage.Guid, ".css", miniStyle);
        //    }

        //    await RemoveCache(webPage);

        //    return webPage;
        //}

        public async Task<WebPage> Save(JObject data)
        {


            dynamic webPageDto = data;
            JArray servicesCodeArray = webPageDto.Services;
            var servicesCode = servicesCodeArray.ToObject<List<string>>();
            string comment = webPageDto.Comment;



            var services = await
                _contentManagementContext.MasterDataKeyValues
                .Where(sr => servicesCode.Contains(sr.Code) && sr.TypeId== (int)EntityIdentity.Service)
                .Select(sr => new { sr.Code, sr.PathOrUrl }).ToListAsync();
            int? webPageId = webPageDto.Id;
            var webPage = new WebPage
            {
                Id = webPageId ?? 0,
                RowVersion = webPageDto.RowVersion //BitConverter.GetBytes(Convert.ToInt64(rowVersion, 16)) 
            };
            bool checkIn = webPageDto.CheckIn;
            bool isPublish = webPageDto.Publish;
            string url = webPageDto.Url;
            url = HttpUtility.UrlDecode(url);
            int typeId = webPageDto.TypeId;

            var currentWebpage = await
                        _contentManagementContext.WebPages.AsNoTracking().SingleOrDefaultAsync(
                            wf => wf.Url == url && wf.TypeId == typeId);

            if (webPage.Id > 0)
            {
                //var id = webPage.Id;
                //webPage =
                //    await
                //        _contentManagementContext.WebPages.SingleOrDefaultAsync(
                //            wf => wf.Url == url && wf.TypeId == typeId);

                if (currentWebpage == null)
                    throw new PageNotFoundException();

                _contentManagementContext.WebPages.Attach(webPage);

                if (currentWebpage.EditMode)
                {
                    _sourceControl.CheckCodeCheckOute(currentWebpage);

                }
                if (currentWebpage != null)
                {
                    webPage.ViewRoleId = currentWebpage.ViewRoleId;
                    webPage.ModifyRoleId = currentWebpage.ModifyRoleId;
                    webPage.AccessRoleId = currentWebpage.AccessRoleId;
                }

                AuthorizeManager.SetAndCheckModifyAndAccessRole(webPage, webPageDto);
                webPage.EditMode = webPageDto.EditMode;

                //await _contentManagementContext.SaveChangesAsync();


                //if (webPage.Status != Convert.ToInt32(webPageDto.Status) && !isPublish)
                //{
                //    webPage.Status = Convert.ToInt32(webPageDto.Status);
                //    await PublishWebPageAsync(webPage.Url, webPage, webPage.TypeId == (int)WebPageType.Modal);
                //}
            }
            else
            {

                if(currentWebpage != null)
                    throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.RepeatedPath, url));


                _contentManagementContext.WebPages.Add(webPage);
            }

            try
            {
                webPage.CacheSlidingExpirationTimeInMinutes = webPageDto.CacheSlidingExpirationTimeInMinutes;
            }
            catch (Exception)
            {
                webPage.CacheSlidingExpirationTimeInMinutes = 0;
            }
            webPage.Html = webPageDto.Html;
            webPage.Url = url;
            string javaScript = webPageDto.JavaScript;
            string style = webPageDto.Style;
            webPage.TemplatePatternUrl = webPageDto.TemplatePatternUrl;
            webPage.Params = webPageDto.Params;
            webPage.FrameWorkUrl = webPageDto.FrameWorkUrl;

            webPage.DependentModules = webPageDto.DependentModules;

            AuthorizeManager.SetAndCheckModifyAndAccessRole(webPage, webPageDto);


            webPage.Title = webPageDto.Title;
            webPage.Status = webPageDto.Status;
            webPage.TypeId = webPageDto.TypeId;
            webPage.EnableCache = webPageDto.EnableCache;
            webPage.EditMode = webPageDto.EditMode;
            webPage.IsMobileVersion = webPageDto.IsMobileVersion;
           // if (webPage.Id == 0)
            webPage.Guid = webPageDto.Guid;

            var webFormServices = services.Aggregate("", (current, service) => current + ("" + service.Code.Remove(0, 9) +
            ":\"" + service.PathOrUrl + "\","));
            webPage.Services = webFormServices.Length > 0 ? webFormServices.Remove(webFormServices.Length - 1) : "";

            var error = new List<string>();

            if (webPage.TypeId == (int)WebPageType.FrameWork)
            {
                if (webPage.Html.IndexOf(_codeTemplate.Title, StringComparison.Ordinal) == -1)
                    error.Add(_codeTemplate.Title);
                if (webPage.Html.IndexOf(_codeTemplate.Style, StringComparison.Ordinal) == -1)
                    error.Add(_codeTemplate.Style);
                if (webPage.Html.IndexOf(_codeTemplate.PageId, StringComparison.Ordinal) == -1)
                    error.Add(_codeTemplate.PageId);
                if (webPage.Html.IndexOf(_codeTemplate.JavaScript, StringComparison.Ordinal) == -1)
                    error.Add(_codeTemplate.JavaScript);
            }

            if (webPage.TypeId == (int)WebPageType.Template)
            {
                if (webPage.Html.IndexOf(_codeTemplate.PlaceHolder, StringComparison.Ordinal) == -1)
                    error.Add(_codeTemplate.PlaceHolder);

            }
            if (error.Count > 0)
                throw new CodeTemplateException(string.Join(",", error.ToArray()));

            webPage.HaveScript = !string.IsNullOrEmpty(javaScript.Trim());
            webPage.HaveStyle = !string.IsNullOrEmpty(style.Trim());
            webPage.Language = Settings.Language;

            var pagesSourceCodePath = Config.PagesSourceCodePath + webPage.Guid + "/";

            var json = (await ConvertToJsonAsync(WebPageJsonType.Source, webPage: webPage)).ToString();
            await _sourceControl.AddChange(pagesSourceCodePath,
            webPage.Guid + ".json",
            json,
            webPage.Version +1,
            comment);

            webPage.Version += 1;


            
            if (isPublish || webPage.Id == 0)
                await _contentManagementContext.SaveChangesAsync();
            else
            {
                var latestPage = await
                       _contentManagementContext.WebPages.AsNoTracking().SingleOrDefaultAsync(wp => wp.Id == webPage.Id && wp.RowVersion == webPage.RowVersion);
                if (latestPage == null)
                    throw new DataConcurrencyException();

                await
                    _contentManagementContext.WebPages.Where(wp => wp.Id == webPage.Id && wp.RowVersion == webPage.RowVersion).UpdateAsync(wp => new WebPage()
                    {
                        HaveStyle = webPage.HaveStyle,
                        HaveScript = webPage.HaveScript,
                        EnableCache = webPage.EnableCache,
                        EditMode = webPage.EditMode,
                        IsMobileVersion = webPage.IsMobileVersion,
                        Version = webPage.Version + 1
                    });

                latestPage = await
                       _contentManagementContext.WebPages.AsNoTracking().SingleOrDefaultAsync(wp => wp.Id == webPage.Id);

                webPage.RowVersion = latestPage.RowVersion;
            }



            json = (await ConvertToJsonAsync(WebPageJsonType.Source, webPage: webPage)).ToString();

            if(checkIn)
            await WriteFileAsync(pagesSourceCodePath, webPage.Guid, ".json",
               json, true);


            if (webPage.HaveScript && checkIn)
            {
                await _sourceControl.AddChange(pagesSourceCodePath,
                    webPage.Guid+ ".js",
                    javaScript,
                    webPage.Version >0? webPage.Version -1:0, 
                    comment);
                await WriteFileAsync(pagesSourceCodePath, webPage.Guid, ".js", javaScript, true);
            }
            else
            {
                if (await _fileSystemManager.FileExistAsync(Config.ScriptDebugPagesPath + webPage.Guid + ".js") && !webPage.HaveScript)
                {
                    DeleteFile(Config.ScriptDebugPagesPath, webPage.Guid, ".js");
                    DeleteFile(Config.ScriptDistPagesPath, webPage.Guid, ".js");
                    DeleteFile(pagesSourceCodePath, webPage.Guid, ".js");
                }
            }
            if (webPage.HaveStyle && checkIn)
            {
                await _sourceControl.AddChange(pagesSourceCodePath,
                webPage.Guid+ ".css",
                style,
                webPage.Version > 0 ? webPage.Version - 1 : 0,
                comment);
                await WriteFileAsync(pagesSourceCodePath, webPage.Guid, ".css", style, true);
            }
            else
            {

                if (await _fileSystemManager.FileExistAsync(Config.StyleDebugPagesPath + webPage.Guid + ".css") && !webPage.HaveStyle)
                {
                    DeleteFile(Config.StyleDebugPagesPath, webPage.Guid, ".css");
                    DeleteFile(Config.StyleDistPagesPath, webPage.Guid, ".css");
                    DeleteFile(pagesSourceCodePath, webPage.Guid, ".css");
                }
            }
            if (isPublish)
            {
                if (webPage.TypeId == (int)WebPageType.Form || webPage.TypeId == (int)WebPageType.Modal)
                {
                    await PublishWebPageAsync(webPage.Url, webPage.TypeId == (int)WebPageType.Modal);
                    //await PublishWebPageAsync(webPage.Url, webPage, webPage.TypeId == (int)WebPageType.Modal);
                }








            }


            if (webPage.HaveScript && webPage.TypeId != (int)WebPageType.FrameWork)
            {
                var pageEvent = "'#" + ConvertToValidId(webPage.Guid) + "'";

                var start = " $(" + pageEvent + ").on($.asEvent.page.loaded, function (event,requestedUrl,asPageParams) {" +
                    "---debug---" + "var asPageEvent = "
                    + pageEvent + "; var asPageId = '." + ConvertToValidId(webPage.Guid) +
                    ".' + $.asPageClass; var as = function(id){var asSelector = new $.as({pageId:asPageId});return asSelector.as(id)};" +
                    "var asOnPageDispose = function(){}; $(asPageEvent).on($.asEvent.page.dispose, function (event) { asOnPageDispose()}); ";
                var finalScript =
                    start +


                 GetServiceSectionOfScript(webPage) + javaScript
                  + Environment.NewLine + @"  ; $(asPageId).append('<span id=""asRegisterPage""></span>');as('#asRegisterPage').asRegisterPageEvent();"
                + Environment.NewLine + @"if (typeof (requestedUrl) != 'undefined')  {$.asLoadPage(requestedUrl,requestedUrl.replace(/\//g, $.asUrlDelimeter));} });";
                _compressManager.CheckJavaScriptCode(finalScript.Replace("---debug---", ""), webPage.Guid);
                var miniJavaScript = _compressManager.CompressJavaScript(finalScript.Replace("---debug---", ""),
                    webPage.Guid);

                await WriteFileAsync(Config.ScriptDebugPagesPath, webPage.Guid, ".js",
                       finalScript.Replace("---debug---", " console.log('" + webPage.Title + "');"));
                if (isPublish)
                    await WriteFileAsync(Config.ScriptDistPagesPath, webPage.Guid, ".js", miniJavaScript);
            }

            if (webPage.HaveStyle)
            {
                var miniStyle = _compressManager.CompressCss(style);
                await WriteFileAsync(Config.StyleDebugPagesPath, webPage.Guid, ".css", style);
                if (isPublish)
                    await WriteFileAsync(Config.StyleDistPagesPath, webPage.Guid, ".css", miniStyle);
            }

            await RemoveCache(webPage);

            return webPage;
        }

        private async Task RemoveCache(WebPage webPage)
        {
            var defaultsTemplate = await GetTemplateAsync(webPage.Url);

            //if (defaultsTemplate.Url == webPage.Url)
            //{
            //    CacheManager.Remove(CacheManager.GetWebPageKey(WebPageType.Template.ToString(), webPage.TemplatePatternUrl));
            //    CacheManager.Remove(WebPageType.TemplatePatternUrls.ToString());
            //}

            var defaultsFrameWork = await GetDefaultsFrameWorkAsync(defaultsTemplate.FrameWorkUrl);


            if (defaultsFrameWork.Url == webPage.Url)
            {
                CacheManager.Remove(CacheManager.GetWebPageKey(WebPageType.FrameWork.ToString(), defaultsTemplate.FrameWorkUrl));
            }
            if (webPage.TypeId == (int)WebPageType.Form)
                CacheManager.Remove(CacheManager.GetWebPageKey(WebPageType.Form.ToString(), webPage.Url));
            else if (webPage.TypeId == (int)WebPageType.Modal)
                CacheManager.Remove(CacheManager.GetWebPageKey(WebPageType.Modal.ToString(), webPage.Url));
            else if (webPage.TypeId == (int) WebPageType.Template)
            {
                CacheManager.Remove(CacheManager.GetWebPageKey(WebPageType.Template.ToString(), webPage.TemplatePatternUrl));
                CacheManager.Remove(CacheKey.TemplatePatternUrls.ToString());
            }


        }

        public async Task<WebPage> GetTemplateAsync(string webPageUrl)
        {

            if (webPageUrl.Length == 0)
                webPageUrl += Helper.RootUrl;
            webPageUrl = webPageUrl[0] == '/' ? webPageUrl : "/" + webPageUrl;




            var frameWorks = await GetFrameWorksByTemplatePaternsUrlsAsync();
            var templatePaternUrls = new List<string>();
            foreach (var frameWork in frameWorks)
            {
                templatePaternUrls.Add(frameWork.Value.Where(ur => webPageUrl.StartsWith(ur, StringComparison.OrdinalIgnoreCase)).ToList()
              .OrderByDescending(ur => ur.Length).FirstOrDefault() ?? "/");
            }
            var templatePaternUrl = templatePaternUrls.OrderByDescending(ur => ur.Length).FirstOrDefault();
            //var templateUrl = templateAndFrameWorks.Where(ur => webPageUrl.StartsWith(ur, StringComparison.OrdinalIgnoreCase)).ToList()
            //    .OrderByDescending(ur => ur.Length).FirstOrDefault() ?? "/";


            
            var key = CacheManager.GetWebPageKey(WebPageType.Template.ToString(), templatePaternUrl);
            var defaultsTemplateCache = CacheManager.Get< WebPage>(key);
            var defaultsTemplate = defaultsTemplateCache.Value;
            if (!defaultsTemplateCache.IsCached)
            {
                defaultsTemplate = await
                _contentManagementContext.WebPages.Where(fr => fr.TemplatePatternUrl == templatePaternUrl && fr.Status == 1)
                    .FirstOrDefaultAsync() ;
                if(defaultsTemplate == null)
                    throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.TemplateNotFound));
                if (defaultsTemplate.EnableCache)
                    CacheManager.StoreForEver(key, defaultsTemplate);
              
            }


           


            if (!Settings.IsDebugMode)
            {

                return defaultsTemplate;
            }

            return (await GetWebPageSourceAsync(defaultsTemplate.Guid)) ?? defaultsTemplate;

        }

        private async Task<Dictionary<string, List<string>>> GetFrameWorksByTemplatePaternsUrlsAsync()
        {
            var templatePaternFrameWorksDictioneryCache = CacheManager.Get<Dictionary<string, List<string>>>(CacheKey.TemplatePatternUrls.ToString());
            if (!templatePaternFrameWorksDictioneryCache.IsCached)
            {
                var templatePaternAndFrameWorks = await
 _contentManagementContext.WebPages.Where(wp => wp.TemplatePatternUrl != null && wp.TemplatePatternUrl.Length > 0)
     .Select(wp => new KeyValue() {Key = wp.FrameWorkUrl,Value = wp.TemplatePatternUrl})
     .ToListAsync();
                

                var templatePaternFrameWorksDictionery = new Dictionary<string,List<string>>();
                foreach (var keyValue in templatePaternAndFrameWorks)
                {
                    if (templatePaternFrameWorksDictionery.ContainsKey(keyValue.Key))
                        templatePaternFrameWorksDictionery.FirstOrDefault(tf => tf.Key == keyValue.Key)
                            .Value.Add(keyValue.Value);
                    else
                        templatePaternFrameWorksDictionery.Add(keyValue.Key, new List<string>() {keyValue.Value});
                }
                CacheManager.StoreForEver(CacheKey.TemplatePatternUrls.ToString(), templatePaternFrameWorksDictionery);
                return templatePaternFrameWorksDictionery;
            }
            return templatePaternFrameWorksDictioneryCache.Value;
        }

        //public async Task<LanguageAndCultureObjective> SaveLanguage(JObject data)
        //{
        //    dynamic languageAndCultureDto = data;
        //    var languageAndCulture = new LanguageAndCultureObjective
        //    {
        //        Id = languageAndCultureDto.Id,
        //        RowVersion = languageAndCultureDto.RowVersion
        //    };


        //    if (languageAndCulture.Id > 0)
        //        languageAndCulture = await _contentManagementContext.LanguageAndCultures.SingleOrDefaultAsync(ln => ln.Id == languageAndCulture.Id);
        //    else
        //        _contentManagementContext.LanguageAndCultures.Add(languageAndCulture);

        //    if (languageAndCulture == null)
        //        throw new DataConcurrencyException();



        //    languageAndCulture.Status = languageAndCultureDto.Status;
        //    languageAndCulture.Language = languageAndCultureDto.Language;
        //    languageAndCulture.Country = languageAndCultureDto.AccessDateTime;
        //    languageAndCulture.Culture = languageAndCultureDto.AccessLocalDatetime;
        //    languageAndCulture.FlagId = languageAndCultureDto.AccessRoleId;
        //    languageAndCulture.IsDefaults = languageAndCultureDto.CreateDateTime;
        //    languageAndCulture.IsRightToLeft = languageAndCultureDto.CreateLocalDateTime;

        //    await _contentManagementContext.SaveChangesAsync();
        //    return languageAndCulture;
        //}


        //private async Task<string> ReadWebPageAsync(string url, bool isModal = false)
        //{
        //    var lastIndex = url.LastIndexOf("/", StringComparison.Ordinal);


        //    try
        //    {
        //        return await _fileSystemManager.ReadAsync(Config.PagesPath + url.Remove(lastIndex) + url.Substring(lastIndex) +
        //            "-" + (isModal ? WebPageType.Modal : WebPageType.Form) + ".json");
        //    }
        //    catch (Exception)
        //    {

        //        throw new PageNotFoundException();
        //    }
        //}


        //private async Task<bool> SaveWebPageSourcesAsJsonAsync(WebPage webPage)
        //{

        //    return true;
        //}

        private async Task<bool> PublishWebPageAsync(string url, bool isModal = false)
        {
            var pageType = isModal ? WebPageType.Modal : WebPageType.Form;
            var lastIndex = url.LastIndexOf("/", StringComparison.Ordinal);
            var pageName = url.Substring(lastIndex);
            var pageUrl = url.Remove(lastIndex);

            var webPageJson = _dataBaseContextManager.GetWebPageForView(url, pageType.ToString()).ToJObject();
            
            await WriteFileAsync((Config.PagesPath + pageUrl).Replace("//", "/"), pageName + "-" + pageType, ".json", webPageJson.ToString(), true);
            return true;
        }

        //private async Task<bool> PublishWebPageAsync(string url, WebPage webPage, bool isModal = false)
        //{
        //    var pageType = isModal ? WebPageType.Modal : WebPageType.Form;
        //    var lastIndex = url.LastIndexOf("/", StringComparison.Ordinal);
        //    var pageName = url.Substring(lastIndex);
        //    var pageUrl = url.Remove(lastIndex);

        //    var webPageJson = await ConvertToJsonAsync(WebPageJsonType.Build, webPage: webPage );

        //    await WriteFileAsync(Config.PagesPath + pageUrl, pageName + "-" + pageType, ".json", webPageJson.ToString(), true);
        //    return true;
        //}

        private bool DeleteWebPage(string url, bool isModal = false)
        {
            var pageType = isModal ? WebPageType.Modal : WebPageType.Form;
            var lastIndex = url.LastIndexOf("/", StringComparison.Ordinal);
            var pageName = url.Substring(lastIndex);
            var pageUrl = url.Remove(lastIndex);

         
            return DeleteFile(Config.PagesPath+ pageUrl, pageName + "-" + pageType, ".json");
        }


        //private string GetWebpageDependentModule(WebPage webPage)
        //{
        //    return "$.asGetWebpageDependentModuleInRunTime(" + webPage.DependentModules + ",{" + webPage.Params + "})";
        //}

        private string DependentModuleToArray(WebPage webPage)
        {
            var dependentModule = webPage.DependentModules;

            var path = Config.PagesPath;
            if (path[0] == '~')
                path = path.Substring(1, path.Length - 1);
            if (path[0] == '/')
                path = path.Substring(1, path.Length - 1);
           

            if (webPage.HaveScript)
                    dependentModule = dependentModule.Length > 2 ?
                        dependentModule.Insert(1, "{\"url\":\"" + path + webPage.Guid + ".js?minVersion=" + webPage.Version + "\"},") :
                        dependentModule.Insert(1, "{\"url\":\"" + path + webPage.Guid + ".js?minVersion=" + webPage.Version + "\"}");
                if (webPage.HaveStyle)
                    dependentModule = dependentModule.Length > 2 ?
                        dependentModule.Insert(1, "{\"url\":\"" + path + webPage.Guid + ".css?minVersion=" + webPage.Version + "\"},") :
                        dependentModule.Insert(1, "{\"url\":\"" + path + webPage.Guid + ".css?minVersion=" + webPage.Version + "\"}");
                return dependentModule; 

        }

        private string GetServiceSectionOfScript(WebPage webPage)
        {
            return " $.asUrls = $.extend({" + webPage.Services +
                    "}, $.asUrls); ";
        }

        private string GetStartOfScript(WebPage webPage,string debugJsRefrence)
        {
            //if(webPage.TypeId != (int)WebPageType.FrameWork)
            //return "<script type=\"text/javascript\"> $(document).ready(function () {var asPageEvent = '#" + ConvertToValidId(webPage.Guid)  + "'; var asPageId = '." + ConvertToValidId(webPage.Guid)  + ".' + $.asPageClass; var as = function(id){ return $(asPageId).find(id);};var asOnPageDispose = function(){}; $(asPageEvent).on($.asEvent.page.dispose, function (event) { asOnPageDispose()}); ";
            if(!Settings.IsDebugMode)
            return "<script type=\"text/javascript\"> $(document).ready(function () { " + GetServiceSectionOfScript (webPage);

            return "<script type=\"text/javascript\"> " + debugJsRefrence + "var loadedDebugScript = 0; var asStartAppInDebugMode = " +
                "function() { loadedDebugScript++; if(loadedDebugScript === refrences.length) { $(document).ready(function () { " +
                   GetServiceSectionOfScript(webPage);
        }
        private string GetHtml(WebPage webPage,bool replaceCodeTemplate=true)
        {
            if (webPage.TypeId != (int)WebPageType.FrameWork)
            {
                var html = webPage.TypeId != (int)WebPageType.Modal ? "<span style='display:none' id='" + ConvertToValidId(webPage.Guid)  + "'></span>" + (webPage.Html ?? ""): (webPage.Html ?? "");
          
                return replaceCodeTemplate ? html.Replace(_codeTemplate.PlaceHolder, "") : html;
            }
            return (webPage.Html ?? "");
        }

        private string GetWebPageSourceCodePath(string guid, SourceType webPageSourceType)
        {
            
                var path = Config.PagesSourceCodePath + guid + "/" + guid;

                switch (webPageSourceType)
                {
                    case SourceType.JavaScript:
                        return path + ".js";
                    case SourceType.Style:
                        return path + ".css";
                    case SourceType.Json:
                        return path + ".json";
                    default:
                        throw new ArgumentOutOfRangeException(nameof(webPageSourceType), webPageSourceType, null);
                }

        }
        public async Task<JObject> ToJsonAsync(WebPageJsonType webPageJsonType, string webPageUrl)
        {
            return await ConvertToJsonAsync(webPageJsonType, webPageUrl: webPageUrl);
        }
        private async Task<JObject> ConvertToJsonAsync(WebPageJsonType webPageJsonType, WebPage webPage = null, string webPageUrl = null)
        {
            switch (webPageJsonType)
            {
                //case WebPageJsonType.View:
                //    {
                //        if (webPageDynamic == null)
                //            throw new ArgumentNullException(nameof(webPageDynamic));
                //        var defaultsTemplate = await GetTemplateAsync(webPageUrl);
                //        return JObject.FromObject(new
                //        {
                //            webPageDynamic.title,
                //            url = webPageUrl,
                //            templateUrl = defaultsTemplate.Url,
                //            frameWorkUrl = defaultsTemplate.FrameWorkUrl,
                //            webPageDynamic.pageId,
                //            webPageDynamic.dependentModules,
                //            webPageDynamic.param,
                //            webPageDynamic.html
                //        });
                //    }
                //case WebPageJsonType.Edit:
                //    {
                //        var services = new string[] { };
                //        if(webPage == null)
                //            throw new ArgumentNullException(nameof(webPage));
                //        if (webPage.Services != null)
                //        {
                //            var servicesUrls = webPage.Services.Split(',');
                //            services = servicesUrls.Select(servicesUrl => servicesUrl.Split(':')).Select(temp => "$.asUrls." + temp[0]).ToArray();
                //        }
                //        var resource = await GetResorces(webPage);

                //        return JObject.FromObject(new
                //        {
                //             webPage.Id,
                //            webPage.Html,
                //            webPage.Url,
                //            JavaScript = resource.Javascript,
                //            resource.Style,
                //            webPage.EditMode,
                //            webPage.Params,
                //             webPage.FrameWorkUrl,
                //             webPage.TemplatePatternUrl,
                //            webPage.Tools,
                //            webPage.ViewRoleId,
                //             webPage.ModifyRoleId,
                //             webPage.AccessRoleId,
                //             webPage.Title,
                //             webPage.Guid,
                //             webPage.Version,
                //             webPage.EnableCache,
                //            webPage.CacheSlidingExpirationTimeInMinutes,
                //            Services = services,
                //            webPage.IsMobileVersion,
                //            webPage.Status,
                //            webPage.TypeId,
                //            webPage.RowVersion
                //        });
                //    }
                case WebPageJsonType.Source:
                    {
                        var services = new string[] { };
                        if (webPage == null)
                            throw new ArgumentNullException(nameof(webPage));
                        if (!string.IsNullOrEmpty(webPage.Services))
                        {
                            var servicesUrls = webPage.Services.Split(',');
                            services = servicesUrls.Select(servicesUrl => servicesUrl.Split(':')).Select(temp => "$.asUrls." + temp[0]).ToArray();
                        }
                      
                        return JObject.FromObject(new
                        {
                            webPage.Id,
                            webPage.Html,
                            webPage.Url,
                            webPage.EditMode,
                            webPage.Params,
                            webPage.FrameWorkUrl,
                            webPage.TemplatePatternUrl,
                            webPage.Tools,
                            webPage.ViewRoleId,
                            webPage.ModifyRoleId,
                            webPage.AccessRoleId,
                            webPage.Title,
                            webPage.Guid,
                            webPage.Version,
                            webPage.EnableCache,
                            webPage.CacheSlidingExpirationTimeInMinutes,
                            Services = services,
                            webPage.IsMobileVersion,
                            webPage.Status,
                            webPage.TypeId,
                            webPage.HaveStyle,
                            webPage.HaveScript,
                            webPage.DependentModules,
                            webPage.Language,
                            webPage.RowVersion
                        });
                    }
                case WebPageJsonType.Debug:
                    {
                        if (webPage == null)
                            throw new ArgumentNullException(nameof(webPage));

                            var defaultsTemplate = await GetTemplateAsync(webPage.Url);

                            return JObject.FromObject(new
                            {
                                title = webPage.Title,
                                url = webPage.Url,
                                templateUrl = defaultsTemplate.Url,
                                frameWorkUrl = defaultsTemplate.FrameWorkUrl,
                                pageId = ConvertToValidId(webPage.Guid),
                                dependentModules = DependentModuleToArray(webPage),
                                param = "{" + webPage.Params + "}",
                                html = GetHtml(webPage)
                            });
                    }

                //case WebPageJsonType.Build:
                //    {
                //        if (webPage == null)
                //            throw new ArgumentNullException(nameof(webPage));
                //        return JObject.FromObject(new
                //        {
                //            modifyRoleId = webPage.ModifyRoleId,
                //            viewRoleId = webPage.ViewRoleId,
                //            enableCache = webPage.EnableCache,
                //            status = webPage.Status,
                //            title = webPage.Title,
                //            cacheSlidingExpirationTimeInMinutes = webPage.CacheSlidingExpirationTimeInMinutes,
                //            pageId = ConvertToValidId(webPage.Guid),
                //            dependentModules = DependentModuleToArray(webPage),
                //            param = "{" + webPage.Params + "}",
                //            html = GetHtml(webPage),
                //            url = webPage.Url
                //        });

                //    }
                case WebPageJsonType.Reload:
                    {
                        if (webPageUrl == null)
                            throw new ArgumentNullException(nameof(webPageUrl));
                        var location = webPageUrl.Replace(Config.UrlDelimeter, Helper.RootUrl);
                        location = LanguageManager.ApplyLanguageAndMobileSignToAjaxRequestAsync(location);

                        var defaultsTemplateWebForm = await GetTemplateAsync(location);

                        return JObject.FromObject(new
                        {
                            title = webPageUrl,
                            pageId = ConvertToValidId(defaultsTemplateWebForm.Guid),
                            dependentModules = DependentModuleToArray(defaultsTemplateWebForm),
                            param = "{" + defaultsTemplateWebForm.Params + "}",
                            html = GetHtml(defaultsTemplateWebForm),
                            requestedUrl=location
                        });
                    }
                case WebPageJsonType.ChangeTemplate:
                    {
                        if (webPageUrl == null)
                            throw new ArgumentNullException(nameof(webPageUrl));
                        var url = LanguageManager.ApplyLanguageAndMobileSignToAjaxRequestAsync(webPageUrl.Replace(Config.UrlDelimeter, Helper.RootUrl));
                        var defaultsTemplateWebForm = await GetTemplateAsync(url);

                        return JObject.FromObject(new
                        {
                            title = url,
                            pageId = ConvertToValidId(defaultsTemplateWebForm.Guid),
                            dependentModules = DependentModuleToArray(defaultsTemplateWebForm),
                            param = "{" + defaultsTemplateWebForm.Params + "}",
                            html = GetHtml(defaultsTemplateWebForm)
                        });
                    }
                default:
                    throw new ArgumentOutOfRangeException(nameof(webPageJsonType), webPageJsonType, null);
            }

        }

        private async Task<List<BrowsersCodeInfo>> GetAndSaveMainBundleInfo(string url)
        {
            //if(SourceControl.BrowsersCodeInfos == null)
            //    SourceControl.BrowsersCodeInfos = new List<BrowsersCodeInfo>();

            var bundleUrl = "~/" + url;



            BrowsersCodeInfo bundle = null;

            var bundleInfoCache = CacheManager.Get<BrowsersCodeInfo>(CacheManager.GetBrowsersCodeInfoKey(CacheKey.BrowsersCodeInfo.ToString(),
               bundleUrl));

            if (bundleInfoCache.IsCached)
            {
                bundle = bundleInfoCache.Value;
            }
            else
            {
                bundleInfoCache = CacheManager.Get<BrowsersCodeInfo>(CacheManager.GetBrowsersCodeInfoKey(CacheKey.BrowsersCodeInfo.ToString(),
               url));
                bundle = bundleInfoCache.Value;
            }



            //var bundle = SourceControl.BrowsersCodeInfos.FirstOrDefault(brc => brc?.BundleUrl == bundleUrl || brc?.BundleUrl==url);

            if (bundle == null)
            {
                var dbBundle = await _contentManagementContext.MasterDataKeyValues.FirstOrDefaultAsync(bd => bd.PathOrUrl == bundleUrl);
                if (dbBundle != null)
                {

                    //SourceControl.BrowsersCodeInfos.Add(new BrowsersCodeInfo()
                    //{
                    //    BundleUrl = bundleUrl,
                    //    CdnUrl = dbBundle.SecondPathOrUrl,
                    //    HasCdn = dbBundle.Key == 1 ,
                    //    Version = dbBundle.Version.ToString()
                    //});

                    CacheManager.StoreForEver(CacheManager.GetBrowsersCodeInfoKey(CacheKey.BrowsersCodeInfo.ToString(),
                        bundleUrl), new BrowsersCodeInfo()
                    {
                        BundleUrl = bundleUrl,
                        CdnUrl = dbBundle.SecondPathOrUrl,
                        HasCdn = dbBundle.Key == 1,
                        Version = dbBundle.Version.ToString()
                    });
                }
                else
                {
                    var lang = await _contentManagementContext.LanguageAndCultures.FirstOrDefaultAsync(ln => ln.Language == url);

                    if(lang != null)
                    {
                        //SourceControl.BrowsersCodeInfos.Add(new BrowsersCodeInfo()
                        //{
                        //    BundleUrl = url,
                        //    HasCdn=false,
                        //    Version = lang.Version.ToString()
                        //});

                        CacheManager.StoreForEver(
                            CacheManager.GetBrowsersCodeInfoKey(CacheKey.BrowsersCodeInfo.ToString(),
                                bundleUrl), new BrowsersCodeInfo()
                            {
                                BundleUrl = url,
                                HasCdn = false,
                                Version = lang.Version.ToString()
                            });
                    }
                }

            }
            return CacheManager.GetForCacheKey<BrowsersCodeInfo>(CacheKey.BrowsersCodeInfo.ToString()).ToList(); // SourceControl.BrowsersCodeInfos;
        }
        public async Task<string> ToStringAsync(string url)
        {
           
            const int arabicYeCharCode = 1610;
            const int persianYeCharCode = 1740;
            const int arabicKeCharCode = 1603;
            const int persianKeCharCode = 1705;

            //var debug = SourceControl.DebugUsers?.FirstOrDefault(du => du.UserId == CurrentUserManager.Id);




            DebugUser debug = null;

            var debugUsersCache = CacheManager.GetForCurrentUserByKey<List<DebugUser>>(CacheManager.GetDebugUserKey(CacheKey.DebugUser.ToString(),
                CurrentUserManager.Id, CurrentUserManager.Ip));

            if (debugUsersCache.IsCached)
            {
                debug = debugUsersCache.Value?.FirstOrDefault(du => du.UserId == CurrentUserManager.Id);
            }










            List<string> urlParameters;
            if (url != null)
            {
                if (Settings.Language == "fa")
                {
                    url = url.Replace(Convert.ToString((char) arabicYeCharCode),
                            Convert.ToString((char) persianYeCharCode))
                        .Replace(Convert.ToString((char) arabicKeCharCode), Convert.ToString((char) persianKeCharCode));
                }
                if (url[url.Length - 1] == '/')
                    url = url.Remove(url.Length - 1);
                var parameters = url.Split('/').ToList();
                urlParameters = LanguageManager.ApplyLanguageAndMobileSignToRequestAsync(parameters);
            }
            else
                urlParameters = LanguageManager.ApplyLanguageAndMobileSignToRequestAsync();

            var finalUrl = string.Join("/", urlParameters.ToArray());

            var defaultsTemplateWebForm = await GetTemplateAsync(finalUrl);

            var defaultsFrameWorkWebForm = await GetDefaultsFrameWorkAsync(defaultsTemplateWebForm.FrameWorkUrl);

            //var resourceTemplate = await GetResorces(defaultsTemplateWebForm);
            var resourceFrameWork = await GetResorces(defaultsFrameWorkWebForm);









            var html = defaultsFrameWorkWebForm.Html.Replace(_codeTemplate.StylesPath,
                (Settings.IsDebugMode ? (Config.StyleDebugPath) : (Config.StyleDistPath)).Substring(1));



            if (html.IndexOf(_codeTemplate.StyleRefrencesStart, StringComparison.Ordinal) > -1)
            {
                var startIndexOfSryleRefrences = html.IndexOf(_codeTemplate.StyleRefrencesStart, StringComparison.Ordinal) + _codeTemplate.StyleRefrencesStart.Length;
                var endIndexOfSryleRefrences = html.IndexOf(_codeTemplate.StyleRefrencesEnd, StringComparison.Ordinal);
                var styleRefrences = html.Substring(startIndexOfSryleRefrences, endIndexOfSryleRefrences - startIndexOfSryleRefrences);
                var cssRefrencesOutPut = "";
                var cssRefrences = styleRefrences.Split(',');


                foreach (var refrence in cssRefrences)
                {
                    await GetAndSaveMainBundleInfo(refrence);

                    //if (SourceControl.BrowsersCodeInfos == null)
                    //    SourceControl.BrowsersCodeInfos = new List<BrowsersCodeInfo>();

                    BrowsersCodeInfo bundleInfo = null;

                    var bundleInfoCache = CacheManager.Get<BrowsersCodeInfo>(CacheManager.GetBrowsersCodeInfoKey(CacheKey.BrowsersCodeInfo.ToString(),
                       "~/" + refrence));

                    if (bundleInfoCache.IsCached)
                    {
                        bundleInfo = bundleInfoCache.Value;
                    }

                    //var bundleInfo = SourceControl.BrowsersCodeInfos.FirstOrDefault(bc => bc?.BundleUrl == "~/" + refrence);
                    if (bundleInfo != null)
                    {
                        if (!bundleInfo.HasCdn || Settings.IsDebugMode)
                        {
                            cssRefrencesOutPut +=
                              " <link href='" + (Settings.IsDebugMode ? Config.StyleDebugPath.Substring(1) : Config.StyleDistPath.Substring(1)) + refrence +
                               "?minversion=" + bundleInfo.Version + "' rel='stylesheet' />";
                        }
                        else
                        {
                            cssRefrencesOutPut +=
                              " <link href='/" + (bundleInfo.CdnUrl + Config.StyleDistPath.Substring(1) + refrence).Replace("//", "/") +
                               "?minversion=" + bundleInfo.Version + "' rel='stylesheet' />";
                        }
                    }
                }

                html = html.Replace(html.Substring(startIndexOfSryleRefrences - _codeTemplate.StyleRefrencesStart.Length,
              _codeTemplate.StyleRefrencesStart.Length + styleRefrences.Length + _codeTemplate.StyleRefrencesEnd.Length), cssRefrencesOutPut);
            }



            html = html.Replace(_codeTemplate.ScriptsPath,
                (Settings.IsDebugMode ? (_codeTemplate.GetScriptDebugPathByDebugId(debug)) : (Config.ScriptDistPath)).Substring(1));

            if (defaultsFrameWorkWebForm.HaveStyle)
            {
                html = html.Replace(_codeTemplate.Style, "<link rel='stylesheet' type='text/css' href='" +
                                                  (Settings.IsDebugMode
                                                      ? (Config.StyleDebugPagesPath)
                                                      : (Config.StyleDistPagesPath)).Substring(1)
                                                  + defaultsFrameWorkWebForm.Guid + ".css?minversion=" +
                                                  defaultsFrameWorkWebForm.Version + "'>");
            }
            else
            {
                html = html.Replace(_codeTemplate.Style, "");
            }


            var configs = _webConfigManager.GetSettingListByOption();
            var injectedConfig = "";
            var open = "";
            var close = "";
            foreach (var config in configs)
            {
                if(config.InjectToJavaScript)
                {
                    JavaScriptType type;
                    Enum.TryParse<JavaScriptType>(config.JavaScriptType,out type);
                    switch (type)
                    {
                        case JavaScriptType.Object:
                            open = "{";
                            close = "}";
                            break;
                        case JavaScriptType.Array:
                            open = "[";
                            close = "]";
                            break;
                        case JavaScriptType.String:
                            open = "'";
                            close = "'";
                            break;

                    }

                    injectedConfig += Char.ToLowerInvariant(config.Key[0]) + config.Key.Substring(1) + ":" + open + config.Value + close + ",";
                }
            }
            var injectedFrameWorkAndTemplatePattern = "frameWorkAndTemplatePattern : {";

            var frameWorkAndTemplatePatern = await GetFrameWorksByTemplatePaternsUrlsAsync();

            foreach (var frameWork in frameWorkAndTemplatePatern)
            {
                injectedFrameWorkAndTemplatePattern += "'" + frameWork.Key + "':[";

                foreach (var template in frameWork.Value)
                {
                    injectedFrameWorkAndTemplatePattern += "'" +  template + "',";
                }

                injectedFrameWorkAndTemplatePattern = injectedFrameWorkAndTemplatePattern.TrimEnd(',');
                injectedFrameWorkAndTemplatePattern += "],";
            }
            injectedFrameWorkAndTemplatePattern = injectedFrameWorkAndTemplatePattern.TrimEnd(',');
            injectedFrameWorkAndTemplatePattern += "},";


            var jsRefrencesOutPut = "";
            var debugJsRefrence = "";
            if (html.IndexOf(_codeTemplate.JavaScriptRefrencesStart, StringComparison.Ordinal) > -1)
            {
                var startIndexOfJavascriptRefrences = html.IndexOf(_codeTemplate.JavaScriptRefrencesStart,
                                                          StringComparison.Ordinal)
                                                      + _codeTemplate.JavaScriptRefrencesStart.Length;
                var endIndexOfJavascriptRefrences = html.IndexOf(_codeTemplate.JavaScriptRefrencesEnd,
                    StringComparison.Ordinal);
                var javaScriptRefrences = html.Substring(startIndexOfJavascriptRefrences,
                    endIndexOfJavascriptRefrences - startIndexOfJavascriptRefrences);

                var jsRefrences = javaScriptRefrences.Split(',');

                if (!Settings.IsDebugMode)
                {


                    foreach (var refrence in jsRefrences)
                    {
                        await GetAndSaveMainBundleInfo(refrence);

                        //if (SourceControl.BrowsersCodeInfos == null)
                        //    SourceControl.BrowsersCodeInfos = new List<BrowsersCodeInfo>();


                        BrowsersCodeInfo bundleInfo = null;

                        var bundleInfoCache =
                            CacheManager.Get<BrowsersCodeInfo>(
                                CacheManager.GetBrowsersCodeInfoKey(CacheKey.BrowsersCodeInfo.ToString(),
                                    "~/" + refrence));

                        if (!bundleInfoCache.IsCached)
                        {
                            bundleInfoCache =
                                CacheManager.Get<BrowsersCodeInfo>(
                                    CacheManager.GetBrowsersCodeInfoKey(CacheKey.BrowsersCodeInfo.ToString(),
                                        refrence));
                        }

                        bundleInfo = bundleInfoCache.Value;


                        //var bundleInfo = SourceControl.BrowsersCodeInfos.FirstOrDefault(bc => bc?.BundleUrl == "~/" + refrence
                        //|| bc?.BundleUrl == refrence);

                        if (bundleInfo != null)
                        {
                            if (!bundleInfo.HasCdn)
                            {
                                jsRefrencesOutPut += defaultsFrameWorkWebForm.Language == refrence
                                    ? " <script src='" + Config.ResourcesDistPath.Substring(1) +
                                      defaultsFrameWorkWebForm.Language + ".js" +
                                      "?minversion=" + bundleInfo.Version + "' ></script>"
                                    : " <script src='" + Config.ScriptDistPath.Substring(1) + refrence +
                                      "?minversion=" + bundleInfo.Version + "' ></script>";
                            }
                            else
                            {
                                jsRefrencesOutPut +=
                                    " <script src='/" +
                                    (bundleInfo.CdnUrl + Config.ScriptDistPath.Substring(1) + refrence).Replace("//",
                                        "/") +
                                    "?minversion=" + bundleInfo.Version + "' ></script>";
                            }
                        }
                    }

                    html =
                        html.Replace(
                            html.Substring(
                                startIndexOfJavascriptRefrences - _codeTemplate.JavaScriptRefrencesStart.Length,
                                _codeTemplate.JavaScriptRefrencesStart.Length + javaScriptRefrences.Length +
                                _codeTemplate.JavaScriptRefrencesEnd.Length), jsRefrencesOutPut);
                }
                else
                {
                    jsRefrencesOutPut = "";
                    debugJsRefrence = "var refrences = [";
                    foreach (var refrence in jsRefrences)
                    {
                        await GetAndSaveMainBundleInfo(refrence);

                        //if (SourceControl.BrowsersCodeInfos == null)
                        //    SourceControl.BrowsersCodeInfos = new List<BrowsersCodeInfo>();


                        BrowsersCodeInfo bundleCode = null;

                        var bundleInfoCache =
                            CacheManager.Get<BrowsersCodeInfo>(
                                CacheManager.GetBrowsersCodeInfoKey(CacheKey.BrowsersCodeInfo.ToString(),
                                    "~/" + refrence));

                        if (!bundleInfoCache.IsCached)
                        {
                            bundleInfoCache =
                                CacheManager.Get<BrowsersCodeInfo>(
                                    CacheManager.GetBrowsersCodeInfoKey(CacheKey.BrowsersCodeInfo.ToString(),
                                        refrence));
                        }

                        bundleCode = bundleInfoCache.Value;

                        //var bundleCode = SourceControl.BrowsersCodeInfos.FirstOrDefault
                        //    (bc => bc?.BundleUrl == "~/" + refrence || bc?.BundleUrl == refrence);

                        debugJsRefrence += "'" + refrence + "?minversion=" +
                                           (bundleCode == null ? "" : bundleCode.Version) + "',";
                    }
                    debugJsRefrence = debugJsRefrence.Substring(0, debugJsRefrence.Length - 1) + "];";
                    jsRefrencesOutPut += "for (i = 0; i < refrences.length; i++) {" +

                                         " var s = document.createElement('script');" +
                                         "s.type = 'text/javascript';" +

                                         "(refrences[i].substring(0, refrences[i].indexOf('?')) === '" +
                                         defaultsFrameWorkWebForm.Language + "' ? s.src ='"
                                         + Config.ResourcesDistPath.Substring(1) + defaultsFrameWorkWebForm.Language +
                                         ".js' : s.src ='"
                                         + Config.ScriptDebugPath.Substring(1) + "' + (localStorage.getItem('" +
                                         Config.DebugIdSign + "') === null ? '' : '"
                                         + Config.DebugIdSign + "/' + localStorage.getItem('" + Config.DebugIdSign +
                                         "') + '/')  + refrences[i] ) ;" +
                                         "s.defer = true;" +
                                         "s.async = false;" +
                                         "s.onload = asStartAppInDebugMode; " +
                                         "document.getElementsByTagName('head')[0].appendChild(s);" +
                                         "};"
                        //+
                        // " var s = document.createElement('script');" +
                        //          "s.type = 'text/javascript';" +
                        //         " s.src ='" + Config.ResourcesDistPath.Substring(1) + defaultsFrameWorkWebForm.WebPage.Language + ".js';"+
                        //           "s.defer = true;" +
                        //            "s.async = false;" +
                        //         "s.onload = asStartAppInDebugMode; " +
                        //         "document.getElementsByTagName('head')[0].appendChild(s);"
                        ;
                    //jsRefrencesOutPut += "</script>";

                    html =
                        html.Replace(
                            html.Substring(
                                startIndexOfJavascriptRefrences - _codeTemplate.JavaScriptRefrencesStart.Length,
                                _codeTemplate.JavaScriptRefrencesStart.Length + javaScriptRefrences.Length +
                                _codeTemplate.JavaScriptRefrencesEnd.Length), "");
                }
            }




            html = html.Replace(_codeTemplate.Template, GetHtml(defaultsTemplateWebForm, false))
                    .Replace(_codeTemplate.PageId,
                        ConvertToValidId(defaultsTemplateWebForm.Guid))

                    .Replace(_codeTemplate.JavaScript,
                        GetStartOfScript(defaultsFrameWorkWebForm, debugJsRefrence) +

                         resourceFrameWork.Javascript.Insert(resourceFrameWork.Javascript.IndexOf("{",
                                                                  resourceFrameWork.Javascript.IndexOf("$.asInitApp",
                                                                      StringComparison.Ordinal),
                                                                  StringComparison.Ordinal) + 1,
                            injectedConfig +
                            injectedFrameWorkAndTemplatePattern +
                            "templateParams:{" +
                            "param:" + "{ " + defaultsTemplateWebForm.Params + "}" +
                            ",dependentModules:" + DependentModuleToArray(defaultsTemplateWebForm) + "," +
                            //",frameWorkUrl:'" + defaultsTemplateWebForm.FrameWorkUrl + "'," +
                            "pageId:'" + ConvertToValidId(defaultsTemplateWebForm.Guid) + "'" +

                            "}" + _codeTemplate.PageParams) + " ;});" + (Settings.IsDebugMode ? "}};" + jsRefrencesOutPut : "") + "</script>");
      


            //try
            //{



                // TODo : Add SEO For Your Bussiness

                if (finalUrl?.IndexOf(Config.LogOffSign, StringComparison.OrdinalIgnoreCase) > -1)
                    return html.Replace(_codeTemplate.PageParams,
                        ",logOff:" + "'/',"
                    );
               
                var finalUrlWithOutQueryString = (finalUrl.IndexOf(Config.QueryStringSign, StringComparison.Ordinal) > -1 ? 
                    finalUrl.Remove(finalUrl.IndexOf(Config.QueryStringSign, StringComparison.Ordinal)) :
                    finalUrl).Replace("#", "");
                    dynamic webPage = await GetWebPageForViewAsync(finalUrlWithOutQueryString);
                    return html.Replace(_codeTemplate.PageParams,
                        ",pageParams:{" +
                        "param:" + (string)webPage.param +""+
                        ",dependentModules:" + (string)webPage.dependentModules +
                        ",url:'" + webPage.url + "'," +
                        "title:'" + webPage.title + "'," +
                        //"templateUrl:'" + webPage.templateUrl + "'," +
                        //"frameWorkUrl:'" + webPage.frameWorkUrl + "'," +
                        "pageId:'" + webPage.pageId + "'" +
                        "},")
                    .Replace(_codeTemplate.Title, (string)webPage.title).Replace(_codeTemplate.PlaceHolder,
                         (string)webPage.html);


            //}
            //catch (UnauthorizedAccessException)
            //{
            //    html = html.Replace(CodeTemplate.Title, "").Replace(CodeTemplate.PlaceHolder, "");
            //    return html.Replace(CodeTemplate.PageParams,
            //        ",loadPage:{" +
            //        "location:'" + string.Join(Helper.RootUrl, urlParameters.ToArray()) + "'," +
            //        "url:'" + string.Join(Config.UrlDelimeter, urlParameters.ToArray()) + "'" +
            //        "},");

            //}



        }

        private async Task<WebPageResource> GetResorces(WebPage webPage)
        {
            var resources = new WebPageResource();

            if (webPage.HaveScript)
            resources.Javascript = await _fileSystemManager.ReadAsync(GetWebPageSourceCodePath(webPage.Guid, SourceType.JavaScript));
            if (webPage.HaveStyle)

                resources.Style = await _fileSystemManager.ReadAsync(GetWebPageSourceCodePath(webPage.Guid, SourceType.Style));

            return resources;
        }

        sealed class WebPageResource
        {
            public string Javascript { get; set; }
            public string Style { get; set; }
        }
    }
}
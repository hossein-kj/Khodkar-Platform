using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using EntityFramework.Extensions;
using KS.Core.CacheProvider;
using KS.Core.Exceptions;
using KS.Core.GlobalVarioable;
using KS.Model.ContentManagement;
using Newtonsoft.Json.Linq;
using KS.Core.Security;
using KS.Core.Utility;
using KS.Core.Localization;
using KS.Core.CodeManager.Base;
using KS.Core.FileSystemProvide.Base;
using Newtonsoft.Json;
using KS.Core.UI.Configuration;
using KS.Core.CodeManager.BrowsersCode.Base;
using KS.DataAccess.Contexts.Base;
using System.IO;

namespace KS.Business.Develop
{
    public class ServiceBiz : IServiceBiz
    {
        private readonly IContentManagementContext _contentManagementContext;
        private readonly IFileSystemManager _fileSystemManager;
        private readonly ISourceControl _sourceControl;
        private readonly IWebConfigManager _webConfigManager;
        private readonly ICodeTemplate _codeTemplate;
        private readonly ISecurityContext _securityContext;
        public ServiceBiz(IContentManagementContext contentManagementContext
            , IFileSystemManager fileSystemManager, ISourceControl sourceControl
            , IWebConfigManager webConfigManager, ICodeTemplate codeTemplate
            , ISecurityContext securityContext)
        {
            _contentManagementContext = contentManagementContext;
            _fileSystemManager = fileSystemManager;
            _sourceControl = sourceControl;
            _webConfigManager = webConfigManager;
            _codeTemplate = codeTemplate;
            _securityContext = securityContext;
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
        #region [SaveService...]
        public async Task<MasterDataKeyValue> Save(JObject data)
        {
            dynamic serviceDto = data;

            var service = new MasterDataKeyValue
            {
                Id = serviceDto.Id,
                RowVersion = serviceDto.RowVersion,
                TypeId= (int)EntityIdentity.Service
            };
            bool isNew = serviceDto.IsNew;


            bool checkIn = serviceDto.CheckIn;
            string comment = serviceDto.Comment;


            if (!isNew)
            {
                service = await _contentManagementContext
                    .MasterDataKeyValues.SingleOrDefaultAsync(sv => sv.Id == service.Id && sv.TypeId == (int) EntityIdentity.Service);
                if (service == null)
                    throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.ServiceNotFound));

                if (service.EditMode)
                {
                    _sourceControl.CheckCodeCheckOute(service);

                }
            }
            else
            {
                _contentManagementContext.MasterDataKeyValues.Add(service);
            }



            string serviceCode = serviceDto.Code;
            if (serviceCode.IndexOf(_codeTemplate.ServicePrefix, StringComparison.Ordinal) != 0)
                serviceCode = _codeTemplate.ServicePrefix + "." + serviceCode;

            string serviceUrl = serviceDto.Url;
            if (serviceUrl.IndexOf(Helper.RootUrl, StringComparison.Ordinal) != 0)
                serviceUrl = Helper.RootUrl + serviceUrl;
            if (serviceUrl.LastIndexOf(Helper.RootUrl, StringComparison.Ordinal) == serviceUrl.Length-1)
                serviceUrl = serviceUrl.Remove(serviceUrl.LastIndexOf(Helper.RootUrl, StringComparison.Ordinal));
            var repeatedService = await _contentManagementContext
                .MasterDataKeyValues.Where(sr => sr.PathOrUrl == serviceUrl && sr.TypeId == (int)EntityIdentity.Service).CountAsync()
                ;
            if ((repeatedService > 0 && isNew) || (repeatedService > 1 && !isNew))
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.RepeatedValue, serviceUrl));
           

            int parentId = serviceDto.ParentId;
            if (service.ParentId != parentId || isNew)
            {

                var parentCode = await _contentManagementContext
                    .MasterDataKeyValues.SingleOrDefaultAsync(sr => sr.Id == parentId && sr.TypeId == (int)EntityIdentity.Service);
                if (parentCode == null)
                    throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.ParentRecordNotFound));
                AuthorizeManager.CheckParentNodeModifyAccessForAddingChildNode(parentCode, parentCode.Id);
            }

            service.ParentId = parentId;

            service.Name = serviceDto.Name;
            service.Code = serviceCode;

            repeatedService = await _contentManagementContext
                .MasterDataKeyValues.Where(sr => sr.Code == service.Code && sr.TypeId == (int) EntityIdentity.Service)
                .CountAsync();
         
            if ((repeatedService > 0 && isNew) || (repeatedService > 1 && !isNew))
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.RepeatedValue, service.Code));
            

            service.Guid = serviceDto.Guid;
            service.Description = serviceDto.Description;
            service.Version++;
            service.PathOrUrl = serviceUrl;
            try
            {
                service.Order = serviceDto.Order;
            }
            catch (Exception)
            {
                service.Order = 1;
            }

            //LOG SERVICE EVENT?1:TRUE,2:FALSE
            try
            {
                service.Key = serviceDto.Key;
            }
            catch (Exception)
            {
                service.Key = 0;
            }

            service.IsLeaf = serviceDto.IsLeaf;
            service.Language = Config.DefaultsLanguage;

            //if(service.IsLeaf)
            AuthorizeManager.SetAndCheckModifyAndAccessRole(service, serviceDto);

            service.Status = serviceDto.Status;
            service.EditMode = serviceDto.EditMode;
            service.EnableCache = serviceDto.EnableCache;
            try
            {
                service.SlidingExpirationTimeInMinutes = serviceDto.SlidingExpirationTimeInMinutes;
            }
            catch (Exception)
            {
                service.SlidingExpirationTimeInMinutes = 0;
            }
            await _contentManagementContext.SaveChangesAsync();
            string jsCode = serviceDto.JsCode;
            if (!string.IsNullOrEmpty(jsCode))
            {
                await _sourceControl.AddChange(Config.ServicesSourceCodePath, service.Guid + ".js", jsCode, service.Version,
                    comment);

                if (checkIn)
                    await WriteFileAsync(Config.ServicesSourceCodePath, service.Guid, ".js", jsCode);
            }


            CacheManager.ClearAllItemContainKey(service.PathOrUrl);
            UpdateServiceSettingOfWebConfig(service);
            return service;
        }
        #endregion [SaveService...]


        public string GetChangeFromSourceControl(int changeId, int codeId)
        {


            if (!AuthorizeManager.IsAuthorizeToViewSourceCodeOfService(codeId))
                throw new UnauthorizedAccessException(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidGrant));



            var change = _sourceControl.GeChangeById(changeId, Config.ServicesSourceCodePath);

            if (change == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.ChangeNotFound));

            if (change.Code == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.ChangeHasNoCode));
            return change.Code;

        }
        public JObject GetChangesFromSourceControl(string orderBy, int skip, int take
          , string comment
           , string user
          , string fromDateTime
          , string toDateTime
            , string codeGuid)
        {


            var count = 0;


            return JObject.Parse(JsonConvert.SerializeObject
            (new
            {
                rows = _sourceControl.GeChangesByPagination(orderBy,
                        skip,
                        take,
                        Config.ServicesSourceCodePath,
                        codeGuid + ".js",
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

        private void UpdateServiceSettingOfWebConfig(MasterDataKeyValue masterDataKeyValue)
        {
            if (_webConfigManager.IsSetting(masterDataKeyValue.Code))
            {
                var setting = _webConfigManager.GetSettingByOption(masterDataKeyValue.Code);
                //var webconfigSetting = JsonConvert.DeserializeObject<WebConfigSetting>(setting.Value);
                var propertyInfo = masterDataKeyValue.GetType().GetProperty(setting.MasterDataKeyValuePropertyName);
                if (propertyInfo != null)
                    setting.Value = Convert.ToString(propertyInfo
                        .GetValue(masterDataKeyValue, null));

                _webConfigManager.UpdateSetting(masterDataKeyValue.Code, setting.Value, JsonConvert.SerializeObject(setting));
            }
        }

        public async Task<bool> Delete(JObject data)
        {
            dynamic serviceData = data;
            int id;

            try
            {
                id = serviceData.Id;
            }
            catch (Exception)
            {
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.FieldMustBeNumeric, "Service Id"));

            }
            var service = await _contentManagementContext
                .MasterDataKeyValues.SingleOrDefaultAsync(sr => sr.Id == id && sr.TypeId==(int)EntityIdentity.Service);

            if (service == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.ServiceNotFound));

            AuthorizeManager.SetAndCheckModifyAndAccessRole(service, null, false);


            if (service.EditMode)
            {
                _sourceControl.CheckCodeCheckOute(service);

            }

           var useCount = await _contentManagementContext.WebPages.Where(wp => wp.Services.Contains(service.PathOrUrl))
                .CountAsync();

            if(useCount > 0)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.InUseItem, service.Name));
           

            if(_fileSystemManager.FileExist(Path.Combine(Config.ServicesSourceCodePath, service.Guid + ".js")))
            {
                _sourceControl.RecycleBin(Config.ServicesSourceCodePath, service.Guid + ".js", codeNameIsFolder: false);

                DeleteFile(Config.ServicesSourceCodePath, service.Guid, ".js");
            }

            _contentManagementContext.MasterDataKeyValues.Remove(service);

            await _contentManagementContext.SaveChangesAsync();
            return true;
        }

        public async Task<JObject> GetAsync(int id)
        {

            var serviceQuery =  _contentManagementContext
                .MasterDataKeyValues.Where(sr => sr.Id == id && sr.TypeId == (int)EntityIdentity.Service).FutureFirstOrDefault();
            var maxIdQuery = _contentManagementContext.MasterDataKeyValues.OrderByDescending(md=>md.Id).FutureFirstOrDefault();

             var service = serviceQuery.Value;
            var maxId = maxIdQuery.Value;

            if (service == null)
                return null;
            AuthorizeManager.CheckViewAccess(service);
            if (!AuthorizeManager.IsAuthorizeToViewSourceCodeOfService(service.Id))
                throw new UnauthorizedAccessException(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidGrant));
            return await ConvertToJsonAsync(service, maxId.Id + 1);

        }

        private async Task<JObject> ConvertToJsonAsync(MasterDataKeyValue service,int newId)
        {
            var lastModifieUser =
              await _securityContext.Users.SingleOrDefaultAsync(us => us.Id == service.CreateUserId);
            return JObject.FromObject(new
            {
                service.Id,
                service.Guid,
                NewGuid = SecureGuid.NewGuid().ToString("N"),
                NewId= newId,
                service.PathOrUrl,
                service.Key,
                service.Name,
                service.Code,
                service.Description,
                service.EditMode,
                service.SlidingExpirationTimeInMinutes,
                service.IsLeaf,
                service.ParentId,
                service.Order,
                service.ViewRoleId,
                service.ModifyRoleId,
                service.AccessRoleId,
                service.Version,
                service.EnableCache,
                service.Status,
                service.RowVersion,
                LastModifieUser = lastModifieUser.UserName,
                LastModifieLocalDateTime = service.ModifieLocalDateTime,
                JsCode = await GetResorcesAsync(service)
            });

        }

        private async Task<string> GetResorcesAsync(MasterDataKeyValue service)
        {
            var jsPath = GetServiceSourceCodePath(service.Guid, SourceType.JavaScript);

            if (await _fileSystemManager.FileExistAsync(jsPath))
                return await _fileSystemManager.ReadAsync(jsPath);
            return "";
        }

        private string GetServiceSourceCodePath(string guid, SourceType sourceType)
        {
            
            var path = Config.ServicesSourceCodePath + guid;

            switch (sourceType)
            {
                case SourceType.JavaScript:
                    return path + ".js";
                default:
                    throw new ArgumentOutOfRangeException(nameof(sourceType), sourceType, null);
            }
        }

    }

}

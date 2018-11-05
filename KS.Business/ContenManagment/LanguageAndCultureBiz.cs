using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using EntityFramework.Extensions;
using KS.Business.ContenManagment.Base;
using KS.Core.CacheProvider;
using KS.Model.ContentManagement;
using Newtonsoft.Json.Linq;
using KS.Core.Exceptions;
using KS.Core.GlobalVarioable;
using KS.Core.Utility;
using Newtonsoft.Json;
using KS.Core.Localization;
using KS.Core.Security;
using KS.Core.CodeManager;
using KS.Core.FileSystemProvide.Base;
using KS.Core.UI.Configuration;
using KS.Core.CodeManager.BrowsersCode.Base;
using KS.Core.Model.Develop;
using KS.DataAccess.Contexts.Base;

namespace KS.Business.ContenManagment
{
    public class LanguageAndCultureBiz : ILanguageAndCultureBiz
    {
        private readonly IContentManagementContext _contentManagementContext;
        private readonly IFileSystemManager _fileSystemManager;
        private readonly IWebConfigManager _webConfigManager;
        private readonly ICompressManager _compressManager;
        public LanguageAndCultureBiz(IContentManagementContext contentManagementContext,
            IFileSystemManager fileSystemManager
            , IWebConfigManager webConfigManager, ICompressManager compressManager)

        {
            _contentManagementContext = contentManagementContext;
            _fileSystemManager = fileSystemManager;
            _webConfigManager = webConfigManager;
            _compressManager = compressManager;
        }

        protected async Task WriteFileAsync(string path, string name, string extention,
string content, bool creatDirectoryIfNotExist = false)
        {
            path = AuthorizeManager.AuthorizeActionOnPath(path.Replace("//", "/"), ActionKey.WriteToDisk);



            await
                _fileSystemManager.WriteAsync(
                (creatDirectoryIfNotExist
                    ? _fileSystemManager.CreatDirectoryIfNotExist(path)
                    : _fileSystemManager.RelativeToAbsolutePath(path)) + name + extention, content);
        }


        protected bool DeleteFile(string path, string name, string extention)
        {


            path = AuthorizeManager.AuthorizeActionOnPath(path.Replace("//", "/"), ActionKey.DeleteFromDisk);


            return _fileSystemManager.DeleteFile(path + name + extention);
        }
        public async Task<IList<LanguageAndCulture>> GetLanguagesAsync()
        {
            return await _contentManagementContext.LanguageAndCultures.Include(lg => lg.Flag).AsNoTracking().ToListAsync();
        }

        #region [SaveLanguageAndCulture...]
        public async Task<LanguageAndCulture> Save(JObject data)
        {
            dynamic languageAndCultureDto = data;

            var languageAndCulture = new LanguageAndCulture()
            {
                Id = languageAndCultureDto.Id,
                RowVersion = languageAndCultureDto.RowVersion
            };
            bool isNew = languageAndCultureDto.isNew;
            bool publish = languageAndCultureDto.publish;
            var currentLanguageAndCulture = await _contentManagementContext.LanguageAndCultures.AsNoTracking().SingleOrDefaultAsync(ln => ln.Id == languageAndCulture.Id);
            if (!isNew)
            {
                
                if (currentLanguageAndCulture == null)
                    throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.LanguageAndCultureNotFound));

                _contentManagementContext.LanguageAndCultures.Attach(languageAndCulture);
            }
            else
            {
                _contentManagementContext.LanguageAndCultures.Add(languageAndCulture);
            }

            languageAndCulture.Country = languageAndCultureDto.Country;
            languageAndCulture.Culture = languageAndCultureDto.Culture;
            languageAndCulture.Language = languageAndCultureDto.Language;
            languageAndCulture.FlagId = languageAndCultureDto.FlagId;
            languageAndCulture.IsDefaults = languageAndCultureDto.IsDefaults;
            languageAndCulture.IsRightToLeft = languageAndCultureDto.IsRightToLeft;

            //for force update and change version for force client to get new versipn of language.js
            languageAndCulture.Version++;

            languageAndCulture.Status = languageAndCultureDto.Status;

            if (currentLanguageAndCulture != null)
            {
                languageAndCulture.ViewRoleId = currentLanguageAndCulture.ViewRoleId;
                languageAndCulture.ModifyRoleId = currentLanguageAndCulture.ModifyRoleId;
                languageAndCulture.AccessRoleId = currentLanguageAndCulture.AccessRoleId;
            }

            AuthorizeManager.SetAndCheckModifyAndAccessRole(languageAndCulture, languageAndCultureDto);

            await _contentManagementContext.SaveChangesAsync();

            await _contentManagementContext.LanguageAndCultures.Where(lc => lc.Language == languageAndCulture.Language && lc.Id != languageAndCulture.Id)
                .UpdateAsync(t => new LanguageAndCulture() {Version = languageAndCulture.Version});

            string jsCode = languageAndCultureDto.JsCode;
            if (!string.IsNullOrEmpty(jsCode))
            {
                await WriteFileAsync(Config.ResourcesSourceCodePath , languageAndCulture.Language , ".js", jsCode);
                if (!publish) return languageAndCulture;

                await WriteFileAsync(Config.ResourcesDistPath , languageAndCulture.Language, ".js", _compressManager.CompressJavaScript(jsCode, languageAndCulture.Country));
            }
            UpdateWebConfigSetting(languageAndCulture, ActionKey.Add);



          
                CacheManager.Remove(CacheManager.GetBrowsersCodeInfoKey(CacheKey.BrowsersCodeInfo.ToString(),
               languageAndCulture.Language));
            

            //var bundleInfo =
            //    SourceControl.BrowsersCodeInfos.FirstOrDefault(bc => bc.BundleUrl == languageAndCulture.Language);

            //if(bundleInfo != null)
            //bundleInfo.Version 
            //    = Helper.UrlEncode(System.Text.Encoding.UTF8.GetString(languageAndCulture.RowVersion));

            //SourceControl.BrowsersCodeInfos.Remove(
            //    SourceControl.BrowsersCodeInfos.Find(bc => bc.BundleUrl == languageAndCulture.Language));

            return languageAndCulture;
        }
        #endregion [SaveLanguageAndCulture...]

        private void UpdateWebConfigSetting(LanguageAndCulture languageAndCulture, ActionKey action)
        {
            if ((Config.LanguageAndCultures.Exists(ln => ln == languageAndCulture.Language) && action == ActionKey.Add)
                || (!Config.LanguageAndCultures.Exists(ln => ln == languageAndCulture.Language) && action == ActionKey.Delete)) return;
            else if (!Config.LanguageAndCultures.Exists(ln => ln == languageAndCulture.Language) && action == ActionKey.Add)
            {
                Config.LanguageAndCultures.Add(languageAndCulture.Language);
                var setting = new WebConfigSetting()
                {
                    Key = ConfigKey.LanguageAndCultures.ToString(),
                    Value = string.Join(",", Config.LanguageAndCultures),
                    InjectToJavaScript = false,
                    Description = "",
                    MasterDataKeyValueId = 0,
                    MasterDataKeyValuePropertyName = ""
                };


                var option = JsonConvert.SerializeObject(setting);
                _webConfigManager.AddOrUpdateSetting(ConfigKey.LanguageAndCultures.ToString(), string.Join(",", Config.LanguageAndCultures), option);
            }
            else if (Config.LanguageAndCultures.Exists(ln => ln == languageAndCulture.Language) && action == ActionKey.Delete)
            {
                Config.LanguageAndCultures.Remove(languageAndCulture.Language);
                var setting = new WebConfigSetting()
                {
                    Key = ConfigKey.LanguageAndCultures.ToString(),
                    Value = string.Join(",", Config.LanguageAndCultures),
                    InjectToJavaScript = false,
                    Description = "",
                    MasterDataKeyValueId = 0,
                    MasterDataKeyValuePropertyName = ""
                };


                var option = JsonConvert.SerializeObject(setting);
                _webConfigManager.AddOrUpdateSetting(ConfigKey.LanguageAndCultures.ToString(), string.Join(",", Config.LanguageAndCultures), option);
            }

        }

        public async Task<JObject> GetAsync(int id)
        {

            var languageAndCulture = await _contentManagementContext.LanguageAndCultures.Where(ln => ln.Id == id).Include(ln=>ln.Flag).FirstOrDefaultAsync();


            if (languageAndCulture == null)
                return null;
            AuthorizeManager.CheckViewAccess(languageAndCulture);
            return await ConvertToJsonAsync(languageAndCulture);

        }

        private async Task<JObject> ConvertToJsonAsync(LanguageAndCulture languageAndCulture)
        {

            return JObject.FromObject(new
            {
                Id = languageAndCulture.Id,
                IsDefaults = languageAndCulture.IsDefaults,
                IsRightToLeft = languageAndCulture.IsRightToLeft,
                Culture = languageAndCulture.Culture,
                Country = languageAndCulture.Country,
                Language = languageAndCulture.Language,
                ViewRoleId = languageAndCulture.ViewRoleId,
                ModifyRoleId = languageAndCulture.ModifyRoleId,
                AccessRoleId = languageAndCulture.AccessRoleId,
                FlagUrl =languageAndCulture.Flag.Url,
                FlagId=languageAndCulture.FlagId,
                Status = languageAndCulture.Status,
                RowVersion = languageAndCulture.RowVersion,
                JsCode = await GetResorces(languageAndCulture)
            });

        }

        private async Task<string> GetResorces(LanguageAndCulture languageAndCulture)
        {
            
                var path = AuthorizeManager.AuthorizeActionOnPath(Config.ResourcesSourceCodePath, ActionKey.ReadFromDisk) + languageAndCulture.Language + ".js";


                if (await _fileSystemManager.FileExistAsync(path))
                    return await _fileSystemManager.ReadAsync(path);

                return "";

        }

        public async Task<bool> Delete(JObject data)
        {
            dynamic langData = data;
            int id;

            try
            {
                id = langData.Id;
            }
            catch (Exception)
            {
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.FieldMustBeNumeric, "LanguageAndCulture Id"));

            }
            var languageAndCulture = await _contentManagementContext.LanguageAndCultures
                .SingleOrDefaultAsync(ln => ln.Id == id);

            if (languageAndCulture == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.LanguageAndCultureNotFound));

            AuthorizeManager.SetAndCheckModifyAndAccessRole(languageAndCulture, null, false);

            var useCount = await _contentManagementContext.WebPages.Where(wp => wp.Language == languageAndCulture.Language)
                 .CountAsync();

            if (useCount > 0)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.InUseItem, languageAndCulture.Language));
           

            DeleteFile( Config.ResourcesSourceCodePath, languageAndCulture.Language, ".js");
            DeleteFile(Config.ResourcesDistPath, languageAndCulture.Language, ".js");

            _contentManagementContext.LanguageAndCultures.Remove(languageAndCulture);

            await _contentManagementContext.SaveChangesAsync();
            UpdateWebConfigSetting(languageAndCulture, ActionKey.Delete);
            return true;
        }

    }
}

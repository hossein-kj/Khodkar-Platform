using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KS.Business.Develop.Code.Base;
using KS.Core.CacheProvider;
using KS.Core.CodeManager.Base;
using KS.Core.Exceptions;
using KS.Core.FileSystemProvide.Base;
using KS.Core.GlobalVarioable;
using KS.Core.Localization;
using KS.Core.Security;
using KS.Model.ContentManagement;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using KS.Core.UI.Configuration;
using KS.Core.CodeManager.BrowsersCode.Base;
using KS.Core.Model.Core;
using KS.Core.Model.Develop;
using KS.DataAccess.Contexts.Base;

namespace KS.Business.Develop.Code.BrowserCode
{
    public class BrowserCodeBiz : BaseCodeBiz, IBrowserCodeBiz
    {
        private const string BundleCode = "bundle_";
        private const string SourceCode = "source_";
        private readonly ICompressManager _compressManager;
        private readonly IBundleManager _bundleManager;
        public BrowserCodeBiz(ISourceControl sourceControl,
            IContentManagementContext contentManagementContext, IFileSystemManager fileSystemManager
            , IWebConfigManager webConfigManager, ICompressManager compressManager, IBundleManager bundleManager
            ,ISecurityContext securityContext)
            : base(sourceControl, contentManagementContext, fileSystemManager
                  , webConfigManager, securityContext)
        {
            _compressManager = compressManager;
            _bundleManager = bundleManager;
        }
        public bool CheckJavascriptCode(JObject data)
        {
            dynamic codeDto = data;
            int id = codeDto.Id;
            string codeContent = codeDto.Code;
            var code = ContentManagementContext.MasterDataKeyValues.FirstOrDefault(sr => sr.Id == id);
            if (code == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.CodeNotFound));
            return _compressManager.CheckJavaScriptCode(codeContent, code.Name);
        }
        public async Task<MasterDataKeyValue> Save(JObject data)
        {
            dynamic masterDataKeyValue = data;
            if (masterDataKeyValue.TypeId != (int)EntityIdentity.Script && masterDataKeyValue.TypeId != (int)EntityIdentity.Style)
                throw new UnauthorizedAccessException(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidGrant));
            masterDataKeyValue.PathOrUrlProtocolId = (int)Protocol.PathProtocol;
            var code = await Save((JObject)masterDataKeyValue, true);
            return code;
        }
        public async Task<MasterDataKeyValue> SaveBundleOrBundleSource(JObject data,bool isSource = false)
        {
            dynamic bundleOrSource = data;
            
            int id = bundleOrSource.Id;
            var isNew = id == 0;
            var dependency = new List<string>();
            string dependencyKey = "";
            id = id == 0 ? (await GetMaxId()) + 1 : id;
            var codeName = isSource ? SourceCode : BundleCode;
            if (isSource)
            {
                int bundleId = bundleOrSource.ParentId;
                var bundle = await
                    ContentManagementContext.MasterDataKeyValues.FirstOrDefaultAsync(md => md.Id == bundleId);

                if (bundle == null)
                    throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.ParentRecordNotFound));
            

                var code = await
                       ContentManagementContext.MasterDataKeyValues.FirstOrDefaultAsync(
                           md => md.Id == bundle.ParentId);
                if (code == null)
                    throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.ParentRecordNotFound));

                if (code.EditMode)
                {
                    SourceControl.CheckCodeCheckOute(code);

                }

                string sourcePathOrUrl = bundleOrSource.PathOrUrl;

                if (bundle.Value == 1)
                {
                   
                    if (
                        sourcePathOrUrl.ToLower()
                            .IndexOf(bundle.PathOrUrl.ToLower().Replace("~", code.PathOrUrl.ToLower()),
                                StringComparison.Ordinal) != 0)
                        throw new KhodkarInvalidException(
                            LanguageManager.ToAsErrorMessage(ExceptionKey.SourceOfOneByOneBundleNotValid));
                }
            }
            else
            {
                dependencyKey = bundleOrSource.DependencyKey;
                if(dependencyKey != null)
                {
                    JArray dependencyArray = bundleOrSource.Dependency;
                    if(dependencyArray != null)
                    dependency = dependencyArray.ToObject<List<string>>();
                }
            }
            int key = 0;

            try
            {
                key = bundleOrSource.Key;
            }
            catch (Exception)
            {
                // ignored
            }
            
            if (!isNew && !isSource)
            {
               
                
                    var bundleEntity = await
                        ContentManagementContext.MasterDataKeyValues.Where(md => md.Id == id)
                            .FirstOrDefaultAsync();
                    if (bundleEntity == null)
                        throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.BundleNotFound));
               
            var code = await
                       ContentManagementContext.MasterDataKeyValues.FirstOrDefaultAsync(
                           md => md.Id == bundleEntity.ParentId);

                if (code == null)
                    throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.ParentRecordNotFound));
            

                if (code.EditMode)
                {
                    SourceControl.CheckCodeCheckOute(code);

                }

                int value = 0;

                    try
                    {
                        value = bundleOrSource.Value;
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                    //oneByOne Bundle
                    if (value == 1 && ((bundleEntity.Value ?? 0) == 0))
                    {
                       
                        var sources = await
                            ContentManagementContext.MasterDataKeyValues.Where(
                                    md => md.ParentId == id && md.TypeId == (int) EntityIdentity.BundleSource)
                                .ToListAsync();
                        if (sources.Any(source => source.PathOrUrl.ToLower()
                                                      .IndexOf(bundleEntity.PathOrUrl.ToLower().Replace("~", code.PathOrUrl.ToLower()),
                                                          StringComparison.Ordinal) != 0))
                        {
                            throw new KhodkarInvalidException(
                                LanguageManager.ToAsErrorMessage(ExceptionKey.SourceOfOneByOneBundleNotValid));
                        }
                    }
                if (bundleEntity.Code != codeName + id && dependencyKey != bundleEntity.Code)
                    await SourceControl.AddOrUpdateDependencyEngineAsync(new BundleDependency()
                    {
                        DependencyKey = bundleEntity.Code,
                        Path = bundleEntity.PathOrUrl,
                        Dependency = await GetBundleDependencyForDependencyEngieen(bundleEntity.Id),
                        Version = bundleEntity.Version,
                        IsPublish = true,
                        IsDelete = true
                    });
            }
            return await base.Save(JObject.Parse(JsonConvert.SerializeObject
            (new
                {
                    Id = id,
                    Code = string.IsNullOrEmpty(dependencyKey) ? codeName + id : dependencyKey,
                    SecondCode = string.Join(",", dependency.ToArray()),
                    bundleOrSource.Description,
                    bundleOrSource.Name,
                    EditMode = false,
                    EnableCache = false,
                    Guid = SecureGuid.NewGuid().ToString("N"),
                    IsLeaf = false,
                    IsType = false,
                    Language = Config.DefaultsLanguage,
                    bundleOrSource.ModifyRoleId,
                    bundleOrSource.ParentId,
                    bundleOrSource.RowVersion,
                    Key = key,
                    bundleOrSource.Value,
                    Status = 1,
                    TypeId = isSource ? (int) EntityIdentity.BundleSource : (int) EntityIdentity.Bundle,
                    IsPath = true,
                    IsPathSecond = false,
                    PathOrUrlProtocolId = (int)Protocol.PathProtocol,
                    bundleOrSource.PathOrUrl,
                    bundleOrSource.SecondPathOrUrl,
                    bundleOrSource.ViewRoleId,
                    bundleOrSource.AccessRoleId,
                    bundleOrSource.IsNew
                }, Formatting.None,
                new JsonSerializerSettings() {ReferenceLoopHandling = ReferenceLoopHandling.Ignore})), false);


        }
        public async Task<bool> Delete(JObject data)
        {
            dynamic masterDataKeyValue = data;
            int id = masterDataKeyValue.Id;

            var code = await ContentManagementContext.MasterDataKeyValues.FirstOrDefaultAsync(md => md.Id == id
            && (md.TypeId == (int)EntityIdentity.Script || md.TypeId == (int)EntityIdentity.Style));
            if(code == null)
                throw new UnauthorizedAccessException(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidGrant));

            var bundlesCount = ContentManagementContext.MasterDataKeyValues.Count(md => md.ParentId == id
            && md.TypeId == (int)EntityIdentity.Bundle);
            if (bundlesCount == 0)
                return await base.DeleteCode(data);
            throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.CodeHasBundle));
        }
        public async Task<bool> DeleteBundleOrBundleSource(JObject data, bool isSource = false)
        {
            dynamic dataDto = data;
            int id = dataDto.Id;
            if(isSource)
            {
                var sourceEntity = await
                   ContentManagementContext.MasterDataKeyValues.FirstOrDefaultAsync(md => md.Id == id);
                var bundleEntity = await
                  ContentManagementContext.MasterDataKeyValues.FirstOrDefaultAsync(md => md.Id == sourceEntity.ParentId);
                var code = await
                       ContentManagementContext.MasterDataKeyValues.FirstOrDefaultAsync(
                           md => md.Id == bundleEntity.ParentId);
                if (code.EditMode)
                    SourceControl.CheckCodeCheckOute(code);
            }
            else
            {
                var bundleEntity = await
                  ContentManagementContext.MasterDataKeyValues.FirstOrDefaultAsync(md => md.Id == id);
                var code = await
                       ContentManagementContext.MasterDataKeyValues.FirstOrDefaultAsync(
                           md => md.Id == bundleEntity.ParentId);
                if (code.EditMode)
                    SourceControl.CheckCodeCheckOute(code);

                var sourcesCount = await ContentManagementContext.MasterDataKeyValues.Where(md=>md.ParentId == bundleEntity.Id)
           .CountAsync();
                if (sourcesCount > 0)
                    throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.InUseItem, bundleEntity.Name));
         
            }

            var dependency = !isSource ? await GetBundleDependencyForDependencyEngieen(id): new List<KeyValue>();
            var bundle = await base.Delete(data);
            if (isSource) return true;

            if (bundle.Code != BundleCode + bundle.Id)
                await SourceControl.AddOrUpdateDependencyEngineAsync(new BundleDependency()
                {
                    DependencyKey = bundle.Code,
                    Path = bundle.PathOrUrl,
                    Dependency = dependency,
                    Version = bundle.Version,
                    IsPublish = true,
                    IsDelete = true
                });

            var path = "";
            if (bundle.PathOrUrl.IndexOf(".css", StringComparison.Ordinal) > -1)
            {
                path = Config.StyleDebugPath + (bundle.PathOrUrl[0] == '/' ? bundle.PathOrUrl.Substring(1) : bundle.PathOrUrl).Replace("~/","");
            }
            else
            {
                path = Config.ScriptDebugPath + (bundle.PathOrUrl[0] == '/' ? bundle.PathOrUrl.Substring(1) : bundle.PathOrUrl).Replace("~/", "");

            }

            DeleteFile(path.IndexOf(Config.ScriptDebugPath, StringComparison.OrdinalIgnoreCase) > -1
                ? path.Replace(Config.ScriptDebugPath, Config.ScriptDistPath)
                : path.Replace(Config.StyleDebugPath, Config.StyleDistPath));
            return DeleteFile(path);
        }

        private string Transform(MasterDataKeyValue bundle,string localHost,string source, string dist)
        {

            FileSystemManager.CreatDirectoryIfNotExist(AuthorizeManager.AuthorizeActionOnPath
                (dist.Substring(0, dist.LastIndexOf("/", StringComparison.Ordinal)),
                            ActionKey.WriteToDisk));
            var bundlePath = source.ToLower().Replace("~/", "");

            var bundleOption = new List<BundleOption>
            {
                new BundleOption()
                {
                    Url = "~/BrowsersCodeOutPut/" + bundle.Guid + "/" + bundle.Version + "/" + bundlePath,
                    Sources = new List<string>() {source }
                }
            };


            foreach (var option in bundleOption)
            {
                foreach (var sourceUrl in option.Sources)
                {
                    AuthorizeManager.AuthorizeActionOnPath(sourceUrl, ActionKey.ReadFromDisk);
                }
            }



            _bundleManager.AddBundle(bundleOption);

           
            var bundleNmae = "~/BrowsersCodeOutPut/" + bundle.Guid + "/" + bundle.Version + "/" + bundlePath
                .Replace(".", "-");
            var url = bundleNmae.Replace("~", localHost);
            string contents;
          
                using (var wc = new System.Net.WebClient())
                {
                    wc.Encoding = Encoding.UTF8;
                    contents = wc.DownloadString(url);
                }

            _bundleManager.RemoveBundle(bundleNmae);
            return contents;
        }
        public async Task<bool> Compile(JObject data, string localHost)
        {
           
            dynamic bundleDto = data;
            int id = bundleDto.Id;
            bool isPublish = bundleDto.IsPublish;
            //string buildJs = "";

            var bundleBySources = await ContentManagementContext.MasterDataKeyValues.Where(cd => cd.Id == id ||
            (cd.ParentId == id && cd.TypeId == (int)EntityIdentity.BundleSource)).ToListAsync();
            var bundle = bundleBySources.FirstOrDefault(bn => bn.Id == id);
       
            var sources = bundleBySources.Where(sr => sr.ParentId == id).ToList();
            if (bundle == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.BundleNotFound));
            if (sources.Count == 0)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.BundleHasNoSource));
             CheckAccess(bundle);

            var code = await ContentManagementContext.MasterDataKeyValues.FirstOrDefaultAsync(cd => cd.Id == bundle.ParentId);
            if (code.EditMode)
                SourceControl.CheckCodeCheckOute(code);

            bundle.Version++;
            await ContentManagementContext.SaveChangesAsync();

            if(bundle.Value == 1)
            {

                foreach (var source in sources)
                {

                    var debugpath = "";
                    if (source.PathOrUrl.IndexOf(".less", StringComparison.OrdinalIgnoreCase) > -1
                        || bundle.PathOrUrl.IndexOf(".sass", StringComparison.OrdinalIgnoreCase) > -1
                        || bundle.PathOrUrl.IndexOf(".scss", StringComparison.OrdinalIgnoreCase) > -1)
                    {
                        debugpath =
                        source.PathOrUrl.ToLower().Replace(code.PathOrUrl.ToLower(), Config.StyleDebugPath).Replace("//", "/");

                        debugpath = debugpath.Replace(".less", ".css").Replace(".sass", ".css").Replace(".scss", ".css");

                        FileSystemManager.CreatDirectoryIfNotExist(
                           AuthorizeManager.AuthorizeActionOnPath(
                               debugpath.Substring(0, debugpath.LastIndexOf("/", StringComparison.Ordinal)),
                               ActionKey.WriteToDisk));

                        await WriteFileAsync(debugpath,
                                  "", "", Transform(bundle,localHost,source.PathOrUrl, debugpath));


                        if (isPublish)
                        {


                            var minContent = "";

                            var distpath = source.PathOrUrl.ToLower().Replace(code.PathOrUrl.ToLower(), Config.StyleDistPath).Replace("//", "/");
                            distpath = distpath.Replace(".less", ".css").Replace(".sass", ".css").Replace(".scss", ".css");
                            minContent = _compressManager.CompressCss(Transform(bundle, localHost, source.PathOrUrl, distpath));


                            FileSystemManager.CreatDirectoryIfNotExist(
                              AuthorizeManager.AuthorizeActionOnPath(
                                  distpath.Substring(0, distpath.LastIndexOf("/", StringComparison.Ordinal)),
                                  ActionKey.WriteToDisk));

                            await WriteFileAsync(distpath,
                                        "", "", minContent);

                        }

                    }
                    else if(source.PathOrUrl.IndexOf(".js", StringComparison.OrdinalIgnoreCase) == -1 
                        && source.PathOrUrl.IndexOf(".css", StringComparison.OrdinalIgnoreCase) == -1)
                    {
                        debugpath =
                            source.PathOrUrl.ToLower().Replace(code.PathOrUrl.ToLower(), Config.StyleDebugPath).Replace("//", "/");

                        debugpath = debugpath.Remove(debugpath.LastIndexOf(".", StringComparison.Ordinal)) + ".js" ;

                        FileSystemManager.CreatDirectoryIfNotExist(
                                   AuthorizeManager.AuthorizeActionOnPath(
                                       debugpath.Substring(0, debugpath.LastIndexOf("/", StringComparison.Ordinal)),
                                       ActionKey.WriteToDisk));

                        await WriteFileAsync(debugpath,
                                   "", "", Transform(bundle,localHost,source.PathOrUrl, debugpath));



                        if (isPublish)
                        {

                           
                            var minContent = "";
                        
                           var  distpath = source.PathOrUrl.ToLower().Replace(code.PathOrUrl.ToLower(), Config.ScriptDistPath).Replace("//", "/");
                            distpath = debugpath.Remove(distpath.LastIndexOf(".", StringComparison.Ordinal)) + ".js";
                            minContent = _compressManager.CompressJavaScript(
                                     await FileSystemManager.ReadAsync(AuthorizeManager.AuthorizeActionOnPath(source.PathOrUrl, ActionKey.ReadFromDisk)), source.PathOrUrl);

                          

                            FileSystemManager.CreatDirectoryIfNotExist(
                                    AuthorizeManager.AuthorizeActionOnPath(
                                        distpath.Substring(0, distpath.LastIndexOf("/", StringComparison.Ordinal)),
                                        ActionKey.WriteToDisk));

                            await WriteFileAsync(distpath,
                                        "", "", minContent);

                        }
                    }
                    else 
                    {
                      
                        debugpath = source.PathOrUrl.IndexOf(".css", StringComparison.OrdinalIgnoreCase) > -1 ?
                            source.PathOrUrl.ToLower().Replace(code.PathOrUrl.ToLower(), Config.StyleDebugPath).Replace("//", "/") :
                            source.PathOrUrl.ToLower().Replace(code.PathOrUrl.ToLower(), Config.ScriptDebugPath).Replace("//", "/");

                        FileSystemManager.CreatDirectoryIfNotExist(AuthorizeManager.AuthorizeActionOnPath(
                            debugpath.Substring(0, debugpath.LastIndexOf("/", StringComparison.Ordinal)),
                            ActionKey.WriteToDisk));
                        FileSystemManager.CopyFile(AuthorizeManager.AuthorizeActionOnPath(
                            source.PathOrUrl, ActionKey.ReadFromDisk),
                            AuthorizeManager.AuthorizeActionOnPath(debugpath, ActionKey.WriteToDisk));



                        if (isPublish)
                        {

                            var distpath = "";
                            var minContent = "";
                            if (source.PathOrUrl.IndexOf(".css", StringComparison.OrdinalIgnoreCase) > -1)
                            {
                                distpath = source.PathOrUrl.ToLower().Replace(code.PathOrUrl.ToLower(), Config.StyleDistPath).Replace("//", "/");
                                minContent = _compressManager.CompressCss(await 
                                    FileSystemManager.ReadAsync(AuthorizeManager.AuthorizeActionOnPath(source.PathOrUrl, ActionKey.ReadFromDisk)));
                            }
                            else
                            {
                                distpath = source.PathOrUrl.ToLower().Replace(code.PathOrUrl.ToLower(), Config.ScriptDistPath).Replace("//", "/");

                                minContent = _compressManager.CompressJavaScript(
                                     await FileSystemManager.ReadAsync(AuthorizeManager.AuthorizeActionOnPath(source.PathOrUrl, ActionKey.ReadFromDisk)), source.PathOrUrl);

                            }

                            FileSystemManager.CreatDirectoryIfNotExist(
                                    AuthorizeManager.AuthorizeActionOnPath(
                                        distpath.Substring(0, distpath.LastIndexOf("/", StringComparison.Ordinal)),
                                        ActionKey.WriteToDisk));

                            await WriteFileAsync(distpath,
                                        "", "", minContent);

                        }


                    }
                }

                return true;
            }

            var bundlePath = bundle.PathOrUrl.ToLower().Replace("~/", "");

            var bundleOption = new List<BundleOption>
            {
                new BundleOption()
                {
                    Url = "~/BrowsersCodeOutPut/" + bundle.Guid + "/" + bundle.Version + "/" + bundlePath,
                    Sources = sources.Select(sr => sr.PathOrUrl).ToList()
                }
            };


            foreach (var option in bundleOption)
            {
                foreach (var source in option.Sources)
                {
                    AuthorizeManager.AuthorizeActionOnPath(source, ActionKey.ReadFromDisk);
                }
            }



            _bundleManager.AddBundle(bundleOption);

            var path = "";
            var bundleNmae = "~/BrowsersCodeOutPut/" + bundle.Guid + "/" + bundle.Version + "/" + bundlePath
                .Replace(".","-");
            var url = bundleNmae.Replace("~", localHost);
            string contents;
    
                using (var wc = new System.Net.WebClient())
                {
                    wc.Encoding = Encoding.UTF8;
                    contents = wc.DownloadString(url);
                }

            if (bundlePath.IndexOf(".css", StringComparison.Ordinal) > -1)
            {
                path = Config.StyleDebugPath + (bundlePath[0] == '/' ? bundlePath.Substring(1) : bundlePath);                        
            }
            else
            {
                path = Config.ScriptDebugPath + (bundlePath[0] == '/' ? bundlePath.Substring(1) : bundlePath);
             
            }
            FileSystemManager.CreatDirectoryIfNotExist(AuthorizeManager.AuthorizeActionOnPath(path.Substring(0, path.LastIndexOf("/", StringComparison.Ordinal)), ActionKey.WriteToDisk));
            await WriteFileAsync(path,
                 "", "", contents);

            if (isPublish)
            {
                var minContent = "";
                if (bundlePath.IndexOf(".css", StringComparison.Ordinal) > -1)
                {
                    path = Config.StyleDistPath + (bundlePath[0] == '/' ? bundlePath.Substring(1) : bundlePath);

                    minContent = _compressManager.CompressCss(contents);
            
                }
                else
                {
                    path = Config.ScriptDistPath + (bundlePath[0] == '/' ? bundlePath.Substring(1) : bundlePath);
                  

                    minContent = _compressManager.CompressJavaScript(contents,path);

                }
                FileSystemManager.CreatDirectoryIfNotExist(AuthorizeManager.AuthorizeActionOnPath(path.Substring(0, path.LastIndexOf("/", StringComparison.Ordinal)), ActionKey.WriteToDisk));
                await WriteFileAsync(path,
    "", "", minContent);
            }

            _bundleManager.RemoveBundle(bundleNmae);

            BrowsersCodeInfo bundleInfo = null;

            var bundleInfoCache = CacheManager.Get<BrowsersCodeInfo>(CacheManager.GetBrowsersCodeInfoKey(CacheKey.BrowsersCodeInfo.ToString(),
               bundle.PathOrUrl));

            if (bundleInfoCache.IsCached)
            {
                bundleInfo = bundleInfoCache.Value;
            }



            //var bundleInfo = KS.Core.CodeManager.SourceControl.BrowsersCodeInfos.FirstOrDefault(bc => bc.BundleUrl == bundle.PathOrUrl);

            if (bundleInfo != null)
            {
                bundleInfo.Version = bundle.Version.ToString();

                CacheManager.StoreForEver(CacheManager.GetBrowsersCodeInfoKey(CacheKey.BrowsersCodeInfo.ToString(),
                   bundle.PathOrUrl), bundleInfo);
            }

            if (bundle.Code != BundleCode + bundle.Id)
                await SourceControl.AddOrUpdateDependencyEngineAsync(new BundleDependency()
                {
                    DependencyKey = bundle.Code,
                    Path = bundle.PathOrUrl,
                    Dependency = await GetBundleDependencyForDependencyEngieen(bundle.Id),
                    Version = bundle.Version,
                    IsPublish = isPublish,
                    IsDelete = false
                });


            return true;

        }

        public async Task<List<KeyValue>> GetBundleDependencyForDependencyEngieen(int bundleId)
        {
            var bundle =
                await ContentManagementContext.MasterDataKeyValues.FirstOrDefaultAsync(md => md.Id == bundleId)
                    ;
            var dependencys = new string[] { };
            if (!string.IsNullOrEmpty(bundle.SecondCode))
                dependencys = bundle.SecondCode.Split(',');


            return await ContentManagementContext.MasterDataKeyValues
                .Where(md => dependencys.Contains(md.Id.ToString()))
                .Select(md => new KeyValue() { Key = (md.Code == BundleCode + md.Id ? null : md.Code), Value = md.PathOrUrl }).ToListAsync();

        }

        public async Task<List<KeyValue>> GetBundleDependency(int bundleId)
        {
            var bundle =
                await ContentManagementContext.MasterDataKeyValues.FirstOrDefaultAsync(md => md.Id == bundleId)
                    ;
            var dependencys = new string[] { };
            if (!string.IsNullOrEmpty(bundle.SecondCode))
                dependencys = bundle.SecondCode.Split(',');


            return await ContentManagementContext.MasterDataKeyValues
                .Where(md => dependencys.Contains(md.Id.ToString()))
                .Select(md=>new KeyValue() {Key = md.Id.ToString(),Value = md.PathOrUrl}).ToListAsync();

        }
 
    }

    sealed class BundleRequest
    {
        public string OutPutDir { get; set; }
        public string OutPutName { get; set; }
        public List<string> Sources { get; set; }
    }
}

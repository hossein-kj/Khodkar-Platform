using System;
using System.Linq;
using System.Threading.Tasks;
using KS.Business.ContenManagment;
using KS.Core.CodeManager.Base;
using KS.Core.Exceptions;
using KS.Core.FileSystemProvide.Base;
using KS.Core.GlobalVarioable;
using KS.Core.Localization;
using KS.Core.Security;
using KS.Core.UI.Configuration;
using KS.Core.Utility;
using KS.Model.ContentManagement;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using KS.DataAccess.Contexts.Base;

namespace KS.Business.Develop.Code.Base
{
    public class BaseCodeBiz : MasterDataKeyValueBiz
    {
        protected readonly IFileSystemManager FileSystemManager;
        

        public BaseCodeBiz(ISourceControl sourceControl,
            IContentManagementContext contentManagementContext, IFileSystemManager fileSystemManager
            , IWebConfigManager webConfigManager,ISecurityContext securityContext)
            : base(sourceControl, contentManagementContext, securityContext, webConfigManager)
        {
            FileSystemManager = fileSystemManager;
        }

        protected async Task WriteFileAsync(string path, string name, string extention,
string content, bool creatDirectoryIfNotExist = false)
        {
            path = AuthorizeManager.AuthorizeActionOnPath(path.Replace("//", "/"), ActionKey.WriteToDisk);



            await
                FileSystemManager.WriteAsync(
                (creatDirectoryIfNotExist
                    ? FileSystemManager.CreatDirectoryIfNotExist(path)
                    : FileSystemManager.RelativeToAbsolutePath(path)) + name + extention, content);
        }


        protected bool DeleteFile(string path, string name, string extention)
        {


            path = AuthorizeManager.AuthorizeActionOnPath(path.Replace("//", "/"), ActionKey.DeleteFromDisk);


            return FileSystemManager.DeleteFile(path + name + extention);
        }
        protected bool CreateDirectory(string path)
        {
            FileSystemManager.CreatDirectoryIfNotExist(AuthorizeManager.AuthorizeActionOnPath(path, ActionKey.WriteToDisk));
            return true;
        }

        protected bool DeleteDirectory(string path)
        {
            return FileSystemManager.DeleteDirectory(AuthorizeManager.AuthorizeActionOnPath(path, ActionKey.DeleteFromDisk));
        }

        protected bool DeleteFile(string path)
        {
            return FileSystemManager.DeleteFile(AuthorizeManager.AuthorizeActionOnPath(path, ActionKey.DeleteFromDisk));
        }
        public new async Task<MasterDataKeyValue> Save(JObject data,bool createDirectory)
        {
            var masterDataKeyValue = await base.Save(data);
            if (createDirectory)
                CreateDirectory(masterDataKeyValue.PathOrUrl);
            return masterDataKeyValue;
        }

        public async Task<string> GetChangeFromSourceControlAsync(int changeId, int codeId, string path)
        {
            path = path.Replace(Config.UrlDelimeter, Helper.RootUrl);
            await GetCodeContentAsync(codeId, path);


            var change = SourceControl.GeChangeById(changeId, path.Remove(path.LastIndexOf("/", StringComparison.Ordinal)+1));
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
            ,string codePath
          , string codeName)
        {


            var count = 0;


            return JObject.Parse(JsonConvert.SerializeObject
            (new
            {
                rows = SourceControl.GeChangesByPagination(orderBy,
                        skip,
                        take,
                        codePath.Replace(Config.UrlDelimeter,Helper.RootUrl),
                        codeName,
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
        public async Task<bool> SaveFile(JObject data)
        {
            //var user = "_asU_";
            //var version = "_asV_";
            dynamic codeDto = data;
            int id = codeDto.Id;
            bool checkIn = codeDto.CheckIn;
            string path = codeDto.Path;
            string codeContent = codeDto.Code;
            string comment = codeDto.Comment;
            path =path.Replace(Config.UrlDelimeter, Helper.RootUrl);
            var code = ContentManagementContext.MasterDataKeyValues.FirstOrDefault(sr => sr.Id == id);

            if(code== null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.CodeNotFound));

            if (code.EditMode)
            SourceControl.CheckCodeCheckOute(code);

            AuthorizeManager.SetAndCheckModifyAndAccessRole(code, null, false);

            if (await FileSystemManager.FileExistAsync(AuthorizeManager
                .AuthorizeActionOnPath(path, ActionKey.WriteToDisk)))
            {
                code.Version++;
                await ContentManagementContext.SaveChangesAsync();

          
                    await SourceControl.AddChange(path.Remove(path.LastIndexOf("/", StringComparison.Ordinal) + 1),
                        path.Substring(path.LastIndexOf("/", StringComparison.Ordinal) + 1),
                        codeContent,
                        code.Version,
                        comment);


                //var userIndex = path.IndexOf(user, StringComparison.Ordinal);
                //var endSourceControlStringIndex = path.LastIndexOf(".", StringComparison.Ordinal);
                //if (userIndex > 0 &&
                //    path.IndexOf(version, StringComparison.Ordinal) > 0)
                //{
                //    path = path.Remove(userIndex, endSourceControlStringIndex - userIndex);


                //}
                //else
                //{
                if (checkIn)
                     await FileSystemManager.WriteAsync(path, codeContent);
                //}

                //return await FileSystemManager.WriteAsync(path.Insert(path.LastIndexOf(".", StringComparison.Ordinal),
                //     user+ CurrentUserManager.UserName.Replace("@", "[at]")+version+code.Version), codeContent);
                return true;
            }
            
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.CodeNotFound));
        }

        public async Task<bool> DeleteCode(JObject data)
        {
            var code =await base.Delete(data);
            SourceControl.RecycleBin(code.PathOrUrl.Replace(code.Guid,"").Replace("//","/"), code.Guid);
            return DeleteDirectory(code.PathOrUrl);
        }


        protected void CheckAccess(MasterDataKeyValue code)
        {
            AuthorizeManager.SetAndCheckModifyAndAccessRole(code, null, false);

            SourceControl.CheckCodeCheckOute(code);
        }

        public async Task<string> GetCodeContentAsync(int codeId,string path)
        {
            path = path.Replace(Config.UrlDelimeter, Helper.RootUrl);
            var code = ContentManagementContext.MasterDataKeyValues.FirstOrDefault(sr => sr.Id == codeId);
            if (code == null || !path.ToLower().StartsWith(code.PathOrUrl.ToLower()))
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.CodeNotFound));
            AuthorizeManager.SetAndCheckModifyAndAccessRole(code,null,false);
            if (await FileSystemManager.FileExistAsync(AuthorizeManager
                .AuthorizeActionOnPath(path, ActionKey.ReadFromDisk)))
                return await FileSystemManager.ReadAsync(path);
            throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.CodeNotFound));
        }
    }
}

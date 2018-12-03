using KS.Business.Security;
using KS.Model.ContentManagement;
using Newtonsoft.Json.Linq;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using KS.Business.ContenManagment.Base;
using KS.Business.Security.Base;
using KS.Core.Exceptions;
using KS.Core.FileSystemProvide.Base;
using KS.Core.GlobalVarioable;
using KS.Core.Security;
using KS.Core.Utility;
using KS.Core.Localization;
using KS.DataAccess.Contexts.Base;

namespace KS.Business.ContenManagment
{
    public sealed class FileBiz : BaseBiz, IFileBiz
    {
        private readonly IContentManagementContext _contentManagementContext;
        private readonly IFileSystemManager _fileSystemManager;
        public FileBiz(
            IContentManagementContext contentManagementContext
            ,IFileSystemManager fileSystemManager)
        {
            _contentManagementContext = contentManagementContext;
            _fileSystemManager = fileSystemManager;
        }

        public async Task<File> Save(JObject data)
        {
            dynamic fileDto = data;
            
            string oldUrl = "";
            int? fileId = fileDto.Id;
            var file = new File()
            {
                Id = fileId ?? 0
            };
            var currentFile = await _contentManagementContext.Files.AsNoTracking()
                .SingleOrDefaultAsync(fl => fl.Id == file.Id);

            try
            {
                if (currentFile == null)
                    throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.FileNotFound));

                oldUrl = currentFile.Url;
                file = currentFile;
                file.RowVersion = fileDto.RowVersion;
                _contentManagementContext.Files.Attach(file);
            }
            catch (Exception)
            {
                _contentManagementContext.Files.Add(file);
            }

            try
            {
                file.Guid= fileDto.Guid;
            }
            catch (Exception)
            {
                file.Guid = SecureGuid.NewGuid();
             
            }

            string filePathUrl = fileDto.Url;
            if (filePathUrl.IndexOf(Helper.RootUrl, StringComparison.Ordinal) != 0)
                filePathUrl = Helper.RootUrl + filePathUrl;
            if (filePathUrl.LastIndexOf(Helper.RootUrl, StringComparison.Ordinal) == filePathUrl.Length - 1)
                filePathUrl = filePathUrl.Substring(0, filePathUrl.Length - 1);

            file.Name = fileDto.Name;
            file.Description = fileDto.Description;
            file.TypeCode = fileDto.TypeCode;
          

            var repeatedLink = await _contentManagementContext.FilePaths.Where(fp => fp.Url == filePathUrl).CountAsync();
            if ((repeatedLink > 0 && oldUrl == "") || (repeatedLink > 1 && oldUrl == ""))
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.RepeatedValue, filePathUrl));
          
            file.ContentType = _fileSystemManager.GetExtension(filePathUrl);
            file.Url = filePathUrl;
            file.Size = _fileSystemManager.GetFileSize(filePathUrl);
            file.Language = Config.DefaultsLanguage;
            file.Content = _fileSystemManager.FileToByte(filePathUrl);

            //if(service.IsLeaf)
            if (currentFile != null)
            {
                file.ViewRoleId = currentFile.ViewRoleId;
                file.ModifyRoleId = currentFile.ModifyRoleId;
                file.AccessRoleId = currentFile.AccessRoleId;
            }
            AuthorizeManager.SetAndCheckModifyAndAccessRole(file, fileDto);

            file.Status = fileDto.Status;


            await _contentManagementContext.SaveChangesAsync();
            return file;
        }

        public async Task<LocalFile> SaveTranslate(JObject data)
        {
            dynamic localFileDto = data;
            int? localFileId = localFileDto.Id;
            var localFile = new LocalFile
            {
                Id = localFileId ?? 0
            };

            var currentLocalFile = await _contentManagementContext.LocalFiles.AsNoTracking().Include(md => md.File).SingleOrDefaultAsync(md => md.Id == localFile.Id);

            if (localFile.Id > 0)
            {
                
                if (currentLocalFile == null)
                    throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.TranslateNotFound));

                localFile = currentLocalFile;
                localFile.RowVersion = localFileDto.RowVersion;

                _contentManagementContext.LocalFiles.Attach(localFile);
            }
            else
            {
                _contentManagementContext.LocalFiles.Add(localFile);
            }

            localFile.FileId = localFileDto.ItemId;
            localFile.Name = localFileDto.Name;
            localFile.Description = localFileDto.Description;
            localFile.Language = localFileDto.Language;

            var currentFile = await _contentManagementContext.Files
               .AsNoTracking().SingleOrDefaultAsync(md => md.Id == localFile.FileId);

            if (currentFile == null)
            {
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.PathNotFound, "FileId"));
            }


            AuthorizeManager.SetAndCheckModifyAndAccessRole(currentFile, localFileDto, false);


            localFile.Status = localFileDto.Status;
            await _contentManagementContext.SaveChangesAsync();
            return localFile;
        }

        public async Task<bool> Delete(JObject data)
        {
            dynamic fileData = data;
            int id;

            try
            {
                id = fileData.Id;
            }
            catch (Exception)
            {
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.FieldMustBeNumeric, "File Id"));
            }
            var file = await _contentManagementContext.Files.SingleOrDefaultAsync(fl => fl.Id == id);

            if (file == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.FileNotFound));

            AuthorizeManager.SetAndCheckModifyAndAccessRole(file, null, false);


            _contentManagementContext.Files.Remove(file);

            await _contentManagementContext.SaveChangesAsync();
            return true;
        }
    }
}

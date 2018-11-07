
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
    public sealed class FilePathBiz:BaseBiz, IFilePathBiz
    {
        private readonly IContentManagementContext _contentManagementContext;
        private readonly IFileSystemManager _fileSystemManager;
        public FilePathBiz(
            IContentManagementContext contentManagementContext
            ,IFileSystemManager fileSystemManager)
        {
            _contentManagementContext = contentManagementContext;
            _fileSystemManager = fileSystemManager;
        }

        public async Task<FilePath> Save(JObject data)
        {
            dynamic filePathDto = data;
            int? filePathId = filePathDto.Id;
            var filePath = new FilePath()
            {
                Id = filePathId ?? 0,
                RowVersion = filePathDto.RowVersion
            };
            var currentFilePath = await _contentManagementContext.FilePaths.AsNoTracking().SingleOrDefaultAsync(fp => fp.Id == filePath.Id);
            string oldUrl = "";
            try
            {
               
                if (currentFilePath == null)
                    throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.PathNotFound, ""));
             
                oldUrl = currentFilePath.Url;
                _contentManagementContext.FilePaths.Attach(filePath);
            }
            catch (Exception)
            {
                _contentManagementContext.FilePaths.Add(filePath);
            }


            string filePathUrl = filePathDto.Url;
            if (filePathUrl.IndexOf(Helper.RootUrl, StringComparison.Ordinal) != 0)
                filePathUrl = Helper.RootUrl + filePathUrl;
            if (filePathUrl.LastIndexOf(Helper.RootUrl, StringComparison.Ordinal) == filePathUrl.Length - 1)
                filePathUrl = filePathUrl.Substring(0, filePathUrl.Length - 1);

            filePath.Name = filePathDto.Name;
            filePath.Description = filePathDto.Description;
            filePath.TypeCode = filePathDto.TypeCode;

            try
            {
                filePath.Guid = filePathDto.Guid;
            }
            catch (Exception)
            {
                filePath.Guid = SecureGuid.NewGuid();

            }

            var repeatedLink = await _contentManagementContext.FilePaths.Where(fp => fp.Url == filePathUrl).CountAsync();
            if ((repeatedLink > 0 && oldUrl == "") || (repeatedLink > 1 && oldUrl == ""))
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.RepeatedValue, filePathUrl));

            filePath.Url = filePathUrl;
            filePath.Size = _fileSystemManager.GetFileSize(filePathUrl);
            filePath.Language = Config.DefaultsLanguage;

            //if(service.IsLeaf)

            if (currentFilePath != null)
            {
                filePath.ViewRoleId = currentFilePath.ViewRoleId;
                filePath.ModifyRoleId = currentFilePath.ModifyRoleId;
                filePath.AccessRoleId = currentFilePath.AccessRoleId;
            }
            AuthorizeManager.SetAndCheckModifyAndAccessRole(filePath, filePathDto);

            filePath.Status = filePathDto.Status;


            await _contentManagementContext.SaveChangesAsync();
            return filePath;
        }

        public async Task<LocalFilePath> SaveTranslate(JObject data)
        {
            dynamic localFilePathDto = data;
            int? localFilePathId = localFilePathDto.Id;
            var localFilePath = new LocalFilePath
            {
                Id = localFilePathId ?? 0,
                RowVersion = localFilePathDto.RowVersion
            };

            var currentLocalFilePath = await _contentManagementContext.LocalFilePaths.Include(md => md.FilePath)
                .AsNoTracking().SingleOrDefaultAsync(md => md.Id == localFilePath.Id);
            if (localFilePath.Id > 0)
            {
                
                if (currentLocalFilePath == null)
                    throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.TranslateNotFound));

                _contentManagementContext.LocalFilePaths.Attach(localFilePath);
            }
            else
            {
                _contentManagementContext.LocalFilePaths.Add(localFilePath);
            }

            localFilePath.FilePathId = localFilePathDto.ItemId;
            localFilePath.Name = localFilePathDto.Name;
            localFilePath.Description = localFilePathDto.Description;
            localFilePath.Language = localFilePathDto.Language;

            var currentFilePath = await _contentManagementContext.FilePaths
               .AsNoTracking().SingleOrDefaultAsync(md => md.Id == localFilePath.FilePathId);

            if (currentFilePath == null)
            {
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.PathNotFound, "FilePathId"));
            }

            AuthorizeManager.SetAndCheckModifyAndAccessRole(currentFilePath, localFilePathDto, false);


            localFilePath.Status = localFilePathDto.Status;
            await _contentManagementContext.SaveChangesAsync();
            return localFilePath;
        }

        public async Task<bool> Delete(JObject data)
        {
            dynamic filePathData = data;
            int id;

            try
            {
                id = filePathData.Id;
            }
            catch (Exception)
            {
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.FieldMustBeNumeric, "FilePath Id"));

            }
            var filePath = await _contentManagementContext.FilePaths.SingleOrDefaultAsync(fp => fp.Id == id);

            if (filePath == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.PathNotFound, ""));
          

            AuthorizeManager.SetAndCheckModifyAndAccessRole(filePath, null, false);


            _contentManagementContext.FilePaths.Remove(filePath);

            await _contentManagementContext.SaveChangesAsync();
            return true;
        }
    }
}

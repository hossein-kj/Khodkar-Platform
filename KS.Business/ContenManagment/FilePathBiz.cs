
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
            FilePath filePath;
            string oldUrl = "";
            try
            {
                filePath = new FilePath()
                {
                    Id = filePathDto.Id,
                    RowVersion = filePathDto.RowVersion
                };
                filePath = await _contentManagementContext.FilePaths.SingleOrDefaultAsync(fp => fp.Id == filePath.Id);
                if (filePath == null)
                    throw new KhodkarInvalidException(string.Format(LanguageManager.ToAsErrorMessage(ExceptionKey.PathNotFound), ""));
                oldUrl = filePath.Url;
            }
            catch (Exception)
            {
                filePath = new FilePath();
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

            var repeatedLink = await _contentManagementContext.FilePaths.Where(fp => fp.Url == filePath.Url).CountAsync();
            if ((repeatedLink > 0 && oldUrl == "") || ((repeatedLink > 1 && oldUrl == "")))
                throw new KhodkarInvalidException(string.Format(LanguageManager.ToAsErrorMessage(ExceptionKey.RepeatedValue),
                    filePath.Url));
            filePath.Url = filePathUrl;
            filePath.Size = _fileSystemManager.GetFileSize(filePathUrl);
            filePath.Language = Config.DefaultsLanguage;

            //if(service.IsLeaf)
            AuthorizeManager.SetAndCheckModifyAndAccessRole(filePath, filePathDto);

            filePath.Status = filePathDto.Status;


            await _contentManagementContext.SaveChangesAsync();
            return filePath;
        }

        public async Task<LocalFilePath> SaveTranslate(JObject data)
        {
            dynamic localFilePathDto = data;

            var localFilePath = new LocalFilePath
            {
                Id = localFilePathDto.Id,
                RowVersion = localFilePathDto.RowVersion
            };


            if (localFilePath.Id > 0)
            {
                localFilePath = await _contentManagementContext.LocalFilePaths.Include(md => md.FilePath).SingleOrDefaultAsync(md => md.Id == localFilePath.Id);
                if (localFilePath == null)
                    throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.TranslateNotFound));
            }
            else
            {
                _contentManagementContext.LocalFilePaths.Add(localFilePath);
            }

            localFilePath.FilePathId = localFilePathDto.ItemId;
            localFilePath.Name = localFilePathDto.Name;
            localFilePath.Description = localFilePathDto.Description;
            localFilePath.Language = localFilePathDto.Language;

            AuthorizeManager.SetAndCheckModifyAndAccessRole(localFilePath.FilePath, localFilePathDto, false);


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

                throw new KhodkarInvalidException(string.Format(LanguageManager.ToAsErrorMessage(ExceptionKey.FieldMustBeNumeric),
                "FilePath Id"));
            }
            var filePath = await _contentManagementContext.FilePaths.SingleOrDefaultAsync(fp => fp.Id == id);

            if (filePath == null)
                throw new KhodkarInvalidException(string.Format(LanguageManager.ToAsErrorMessage(ExceptionKey.PathNotFound), ""));

            AuthorizeManager.SetAndCheckModifyAndAccessRole(filePath, null, false);


            _contentManagementContext.FilePaths.Remove(filePath);

            await _contentManagementContext.SaveChangesAsync();
            return true;
        }
    }
}

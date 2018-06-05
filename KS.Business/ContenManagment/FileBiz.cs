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
            File file;
            string oldUrl = "";
            try
            {
                file = new File()
                {
                    Id = fileDto.Id,
                    RowVersion = fileDto.RowVersion
                };
                file = await _contentManagementContext.Files.SingleOrDefaultAsync(fl => fl.Id == file.Id);
                if (file == null)
                    throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.FileNotFound));
                oldUrl = file.Url;
            }
            catch (Exception)
            {
                file = new File();
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
          

            var repeatedLink = await _contentManagementContext.FilePaths.Where(fp => fp.Url == file.Url).CountAsync();
            if ((repeatedLink > 0 && oldUrl == "") || ((repeatedLink > 1 && oldUrl == "")))
                throw new KhodkarInvalidException(string.Format(LanguageManager.ToAsErrorMessage(ExceptionKey.RepeatedValue),
                    file.Url));
          
            file.ContentType = _fileSystemManager.GetExtension(filePathUrl);
            file.Url = filePathUrl;
            file.Size = _fileSystemManager.GetFileSize(filePathUrl);
            file.Language = Config.DefaultsLanguage;
            file.Content = _fileSystemManager.FileToByte(filePathUrl);

            //if(service.IsLeaf)
            AuthorizeManager.SetAndCheckModifyAndAccessRole(file, fileDto);

            file.Status = fileDto.Status;


            await _contentManagementContext.SaveChangesAsync();
            return file;
        }

        public async Task<LocalFile> SaveTranslate(JObject data)
        {
            dynamic localFileDto = data;

            var localFile = new LocalFile
            {
                Id = localFileDto.Id,
                RowVersion = localFileDto.RowVersion
            };


            if (localFile.Id > 0)
            {
                localFile = await _contentManagementContext.LocalFiles.Include(md => md.File).SingleOrDefaultAsync(md => md.Id == localFile.Id);
                if (localFile == null)
                    throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.TranslateNotFound));
            }
            else
            {
                _contentManagementContext.LocalFiles.Add(localFile);
            }

            localFile.FileId = localFileDto.ItemId;
            localFile.Name = localFileDto.Name;
            localFile.Description = localFileDto.Description;
            localFile.Language = localFileDto.Language;

            AuthorizeManager.SetAndCheckModifyAndAccessRole(localFile.File, localFileDto, false);


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

                throw new KhodkarInvalidException(string.Format(LanguageManager.ToAsErrorMessage(ExceptionKey.FieldMustBeNumeric),
                "File Id"));
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

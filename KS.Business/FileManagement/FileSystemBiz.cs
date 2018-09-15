using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using KS.Model.FileManagement;
using System.IO;
using System.Web.Hosting;
using System.Drawing;
using System.Threading;
using System.Linq.Dynamic;
using KS.Core.Exceptions;
using KS.Core.FileSystemProvide.Base;
using KS.Core.GlobalVarioable;
using KS.Core.Utility;
using KS.Core.Localization;
using KS.Core.Model.FileSystem;
using KS.Core.Security;
using KS.DataAccess.Contexts.Base;
using Newtonsoft.Json.Linq;

namespace KS.Business.FileManagement
{
    public class FileSystemBiz : IFileSystemBiz
    {
        private readonly IContentManagementContext _contentManagementContext;
        private readonly IFileSystemManager _fileSystemManager;
        private readonly IZipManager _zipManager;
        private readonly IImageManager _imageManager;
        public FileSystemBiz(IFileSystemManager fileSystemManager,
            IZipManager zipManager, 
            IImageManager imageManager,
            IContentManagementContext contentManagementContext)
        {
            _fileSystemManager = fileSystemManager;
            _zipManager = zipManager;
            _imageManager = imageManager;
            _contentManagementContext = contentManagementContext;
        }
        public List<DiskInfo> GetPathDirectoriesAndFiles(string path, string orderBy, int skip, int take,out int count, bool byFile = true, string searchPatern = "*", bool allDirectories = false,bool createThumbnail=false)
        {
            path = AuthorizeManager.AuthorizeActionOnPath(path.Replace(Config.UrlDelimeter, Helper.RootUrl), ActionKey.ReadFromDisk);

            var diskInfos = new List<DiskInfo>();
            var searchOption = allDirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            var directories = _fileSystemManager.GetDirectories(path, searchPatern, searchOption);
            var files = _fileSystemManager.GetFiles(path, searchPatern, searchOption);
            var id = 0;
            foreach (var directory in directories)
            {
                diskInfos.Add(new DiskInfo()
                {
                    Id = ++id,
                    Name = directory.Name,
                    IsFolder = true,
                    ModifieDateTime = directory.LastWriteTimeUtc,
                    ModifieLocalDateTime = LanguageManager.ToLocalDateTime(directory.LastWriteTimeUtc)
                });
            }
            var rootPath = HostingEnvironment.ApplicationHost.GetPhysicalPath();
            if (byFile)
            {


                foreach (var file in files)
                {
                    diskInfos.Add(new DiskInfo()
                    {
                        Id = ++id,
                        Name = file.Name,
                        FileType= _fileSystemManager.FileExtensionToFileType(file.Extension),
                        FullName=file.FullName,
                        ModifieDateTime = file.LastWriteTimeUtc,
                        Size = file.Length,
                        ModifieLocalDateTime = LanguageManager.ToLocalDateTime(file.LastWriteTimeUtc)
                    });
                }
            }
            count = diskInfos.Count();
            var fileAndFolders = diskInfos.AsQueryable().OrderBy(orderBy)
                .Skip(skip)
                .Take(take).ToList();

            if (createThumbnail)
            {

                var t = new Thread(tr => CreateThumbnail(fileAndFolders.Where(fl=>fl.IsFolder == false && fl.FileType == 1).ToList()));
                t.Start();
            }

            return fileAndFolders;
        }

        private void CreateThumbnail(List<DiskInfo> files)
        {

            var rootPath = HostingEnvironment.ApplicationHost.GetPhysicalPath();
            
            var tempPath = _fileSystemManager.RelativeToAbsolutePath(AuthorizeManager.AuthorizeActionOnPath(Config.ThumbnailPath, ActionKey.WriteToDisk));
           
            if (tempPath == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.PathNotFound, Config.ThumbnailPath));
      
            var thumbnailPath = tempPath.ToLower();
            Parallel.ForEach(files, file =>
            {
                var thumbPath = thumbnailPath +
                                file.FullName.Substring(rootPath.Length);
                if (_fileSystemManager.FileExist(thumbPath) ||
                    file.FullName.ToLower().IndexOf(thumbnailPath, StringComparison.Ordinal) != -1) return;
                var thumbImg = _imageManager.CreateThumbnail(Image.FromFile(file.FullName, true),
                    200, 200);
                thumbImg.Save(thumbPath);
            });

        }

        public bool Move(DiskOprationInfo moveInfo)
        {
            //if (moveInfo.DestinationPath[0] != '~')
            //    moveInfo.DestinationPath = moveInfo.DestinationPath[0] == '/' ? "~" + moveInfo.DestinationPath : "~/" + moveInfo.DestinationPath;
            //if (moveInfo.SourcePath[0] != '~')
            //    moveInfo.SourcePath = moveInfo.SourcePath[0] == '/' ? "~" + moveInfo.SourcePath : "~/" + moveInfo.SourcePath;
            var rootPath = HostingEnvironment.ApplicationHost.GetPhysicalPath();
            var tempPath = _fileSystemManager.RelativeToAbsolutePath(Config.ThumbnailPath);
            if (tempPath == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.PathNotFound, Config.ThumbnailPath));
        
            var thumbnailRootPath = tempPath.ToLower();

            var destinationPath = AuthorizeManager.AuthorizeActionOnPath(moveInfo.DestinationPath, ActionKey.WriteToDisk);
            var sourcePath = AuthorizeManager.AuthorizeActionOnPath(moveInfo.SourcePath, ActionKey.ReadFromDisk);
            
            
            Parallel.ForEach(moveInfo.Files, file =>
            {
                var source = _fileSystemManager.RelativeToAbsolutePath(sourcePath + "/" + file);
                var dist = _fileSystemManager.RelativeToAbsolutePath(destinationPath + "/" + file);
                if (source == null)
                    throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.PathNotFound, "source"));
            
                if (dist == null)
                    throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.PathNotFound, "dist"));
          
                _fileSystemManager.MoveFile(source, dist);

                var thumbnailSourcePath = source.Substring(rootPath.Length);
                var thumbnailDistPath = dist.Substring(rootPath.Length);
                if (_fileSystemManager.FileExist(thumbnailRootPath + thumbnailSourcePath))
                    _fileSystemManager.MoveFile(thumbnailRootPath + thumbnailSourcePath, thumbnailRootPath + thumbnailDistPath);
            });
            Parallel.ForEach(moveInfo.Folders, folder =>
            {
                var source = _fileSystemManager.RelativeToAbsolutePath(sourcePath + "/" + folder);
                var dist = _fileSystemManager.RelativeToAbsolutePath(destinationPath + "/" + folder);
                if (source == null)
                    throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.PathNotFound, "source"));
              
                if (dist == null)
                    throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.PathNotFound, "dist"));
             
                _fileSystemManager.MoveDirectory(source, dist);

                var thumbnailSourcePath = source.Substring(rootPath.Length);
                var thumbnailDistPath = dist.Substring(rootPath.Length);
                if (_fileSystemManager.DirectoryExists(thumbnailRootPath + thumbnailSourcePath))
                    _fileSystemManager.MoveDirectory(thumbnailRootPath + thumbnailSourcePath, thumbnailRootPath + thumbnailDistPath);

            });
            return true;
        }

        public async Task Save(JObject data)
        {
            dynamic dataDto = data;

            string path = dataDto.Path;
            string name = dataDto.Name;
            string content = dataDto.Content;
            await _fileSystemManager.WriteAsync(AuthorizeManager.AuthorizeActionOnPath(path + "/" + name,ActionKey.WriteToDisk), content);
        }

        public bool Copy(DiskOprationInfo copyInfo)
        {
            //if (copyInfo.DestinationPath[0] != '~')
            //    copyInfo.DestinationPath = copyInfo.DestinationPath[0] == '/' ? "~" + copyInfo.DestinationPath : "~/" + copyInfo.DestinationPath;
            //if (copyInfo.SourcePath[0] != '~')
            //    copyInfo.SourcePath = copyInfo.SourcePath[0] == '/' ? "~" + copyInfo.SourcePath : "~/" + copyInfo.SourcePath;

            var destinationPath = AuthorizeManager.AuthorizeActionOnPath(copyInfo.DestinationPath, ActionKey.WriteToDisk);
            var sourcePath = AuthorizeManager.AuthorizeActionOnPath(copyInfo.SourcePath, ActionKey.ReadFromDisk);

            Parallel.ForEach(copyInfo.Files, file =>
            {
                _fileSystemManager.CopyFile(sourcePath + "/" + file, destinationPath + "/" + file, copyInfo.OverWrite);
            });
            Parallel.ForEach(copyInfo.Folders, folder =>
            {
                _fileSystemManager.CopyDirectory(sourcePath + "/" + folder, destinationPath + "/" + folder, copyInfo.OverWrite);
            });
            return true;
        }


        public bool Zip(ZipOprationInfo zipOprationInfo)
        {
            //if (zipOprationInfo.DestinationPath[0] != '~')
            //    zipOprationInfo.DestinationPath = zipOprationInfo.DestinationPath[0] == '/' ? "~" +
            //        zipOprationInfo.DestinationPath : "~/" + zipOprationInfo.DestinationPath;
            //if (zipOprationInfo.SourcePath[0] != '~')
            //    zipOprationInfo.SourcePath = zipOprationInfo.SourcePath[0] == '/' ? "~" +
            //        zipOprationInfo.SourcePath : "~/" + zipOprationInfo.SourcePath;

            var temp = zipOprationInfo.DestinationPath;
            zipOprationInfo.DestinationPath = _fileSystemManager.RelativeToAbsolutePath(AuthorizeManager.AuthorizeActionOnPath(zipOprationInfo.DestinationPath, ActionKey.WriteToDisk));
            if (zipOprationInfo.DestinationPath == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.PathNotFound, temp));
     
            
            zipOprationInfo.SourcePath = _fileSystemManager.RelativeToAbsolutePath(AuthorizeManager.AuthorizeActionOnPath(zipOprationInfo.SourcePath, ActionKey.ReadFromDisk));

            if (!zipOprationInfo.OverWrite)
            {
                if (_fileSystemManager.FileExist(zipOprationInfo.DestinationPath.ToLower().IndexOf(".zip", StringComparison.Ordinal) > -1
                    ? zipOprationInfo.DestinationPath : zipOprationInfo.DestinationPath + ".zip"))
                    throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.RepeatedPath, zipOprationInfo.DestinationPath));
                
            }
           var path = zipOprationInfo.DestinationPath.Remove(zipOprationInfo.DestinationPath.ToLower()
                    .LastIndexOf("\\", StringComparison.Ordinal));
            if (!_fileSystemManager.DirectoryExists(path))
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.PathNotFound, path));
         
            return _zipManager.Zip(zipOprationInfo);
        }
        public bool UnZip(UnZipOprationInfo unZipOprationInfo)
        {
            //if (unZipOprationInfo.DestinationPath[0] != '~')
            //    unZipOprationInfo.DestinationPath = unZipOprationInfo.DestinationPath[0] == '/' ? "~" +
            //        unZipOprationInfo.DestinationPath : "~/" + unZipOprationInfo.DestinationPath;
            //if (unZipOprationInfo.SourcePath[0] != '~')
            //    unZipOprationInfo.SourcePath = unZipOprationInfo.SourcePath[0] == '/' ? "~" +
            //        unZipOprationInfo.SourcePath : "~/" + unZipOprationInfo.SourcePath;
          
            unZipOprationInfo.DestinationPath = _fileSystemManager.RelativeToAbsolutePath(AuthorizeManager.AuthorizeActionOnPath(unZipOprationInfo.DestinationPath, ActionKey.WriteToDisk));
            
            unZipOprationInfo.SourcePath = _fileSystemManager.RelativeToAbsolutePath(AuthorizeManager.AuthorizeActionOnPath(unZipOprationInfo.SourcePath, ActionKey.ReadFromDisk));

            return _zipManager.UnZip(unZipOprationInfo);
        }
        public List<ZipInfo> OpenZip(string zipFullName, string orderBy, int skip, int take, out int count)
        {
            //if (zipFullName[0] != '~')
            //    zipFullName = zipFullName[0] == '/' ? "~" +
            //        zipFullName : "~/" + zipFullName;

         
            return _zipManager.OpenZip(AuthorizeManager.AuthorizeActionOnPath(zipFullName.Replace(Config.UrlDelimeter, Helper.RootUrl), ActionKey.ReadFromDisk),
                orderBy,skip,take,out count);
        }

        public bool Rename(RenameOprationInfo renameInfo)
        {
            var rootPath = HostingEnvironment.ApplicationHost.GetPhysicalPath();
            var tempPath = _fileSystemManager.RelativeToAbsolutePath(Config.ThumbnailPath);
            if (tempPath == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.PathNotFound, Config.ThumbnailPath));
         
            var thumbnailRootPath = tempPath.ToLower();

            var oldPath = _fileSystemManager.RelativeToAbsolutePath(AuthorizeManager.AuthorizeActionOnPath(renameInfo.OldPath,ActionKey.ReadFromDisk));
            var newPath = _fileSystemManager.RelativeToAbsolutePath(AuthorizeManager.AuthorizeActionOnPath(renameInfo.NewPath,ActionKey.WriteToDisk));
            if(oldPath==null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.PathNotFound, renameInfo.OldPath));
          
            if (newPath == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.PathNotFound, renameInfo.NewPath));
      

            var thumbnailOldPath = oldPath.Substring(rootPath.Length);
            var thumbnailNewPath = newPath.Substring(rootPath.Length);



            if (!renameInfo.IsDirectory)
            {
                _fileSystemManager.RenameFile(oldPath, newPath);
                if (_fileSystemManager.FileExist(thumbnailRootPath + thumbnailOldPath))
                    _fileSystemManager.RenameFile(thumbnailRootPath + thumbnailOldPath, thumbnailRootPath + thumbnailNewPath);
            }
            else
            {
                _fileSystemManager.RenameDirectory(oldPath, newPath);
                if (_fileSystemManager.DirectoryExists(thumbnailRootPath + thumbnailOldPath))
                    _fileSystemManager.RenameDirectory(thumbnailRootPath + thumbnailOldPath, thumbnailRootPath + thumbnailNewPath);
            }
            return true;
        }
        public bool Delete(DeleteOprationInfo deleteInfo)
        {
            var rootPath = HostingEnvironment.ApplicationHost.GetPhysicalPath();
            var tempPath = _fileSystemManager.RelativeToAbsolutePath(Config.ThumbnailPath);
            if (tempPath == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.PathNotFound, Config.ThumbnailPath));
          
            var thumbnailRootPath = tempPath.ToLower();

            var path = _fileSystemManager.RelativeToAbsolutePath(AuthorizeManager.AuthorizeActionOnPath(deleteInfo.Path,ActionKey.DeleteFromDisk));
        
            if (path == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.PathNotFound, deleteInfo.Path));
           
      



            Parallel.ForEach(deleteInfo.Files, file =>
            {
                var realPath = path + "/" + file;
                _fileSystemManager.DeleteFile(realPath);
   
                var thumbnailPath = realPath.Substring(rootPath.Length);
      
                if (_fileSystemManager.FileExist(thumbnailRootPath + thumbnailPath))
                    _fileSystemManager.DeleteFile(thumbnailRootPath + thumbnailPath);
            });
            Parallel.ForEach(deleteInfo.Folders, folder =>
            {
                var realPath = path + "/" + folder;
                _fileSystemManager.DeleteDirectory(realPath);

                var thumbnailPath = realPath.Substring(rootPath.Length);

                if (_fileSystemManager.DirectoryExists(thumbnailRootPath + thumbnailPath))
                    _fileSystemManager.DeleteDirectory(thumbnailRootPath + thumbnailPath);
            });
            return true;
        }

        public bool CreateDirectory(string path)
        {
            _fileSystemManager.CreatDirectoryIfNotExist(AuthorizeManager.AuthorizeActionOnPath(path, ActionKey.WriteToDisk));
            return true;
        }

        public bool DeleteDirectory(string path)
        {
            return _fileSystemManager.DeleteDirectory(AuthorizeManager.AuthorizeActionOnPath(path, ActionKey.DeleteFromDisk));
        }

        public async Task<string> GetFileContenAsync(string path)
        {
            path = path.Replace(Config.UrlDelimeter, Helper.RootUrl);
            path = AuthorizeManager.AuthorizeActionOnPath(path, ActionKey.ReadFromDisk);
            if (await _fileSystemManager.FileExistAsync(path))
                return await _fileSystemManager.ReadAsync(path);
            throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.FileNotFound));
        }

        public bool DownlodFromUrl(int baseUrlId,string url,string filePath, string fileName)
        {
            filePath = AuthorizeManager.AuthorizeActionOnPath(filePath
                .Replace(Config.UrlDelimeter, Helper.RootUrl), ActionKey.WriteToDisk);
            if (AuthorizeManager.AuthorizeActionOnEntityId(baseUrlId, (int) EntityIdentity.Urls,
                (int) ActionKey.DownloadFromAddress))
            {
                var baseUrl = _contentManagementContext.MasterDataKeyValues.AsNoTracking()
                    .SingleOrDefault(md => md.TypeId == (int) EntityIdentity.Urls && md.Id == baseUrlId);
                if(baseUrl == null)
                    throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.CodeNotFound));

                var downloadUrl = "";
                if (baseUrl.PathOrUrl[baseUrl.PathOrUrl.Length - 1] == '/')
                {
                    if (url[0] == '/')
                    {
                        downloadUrl = baseUrl.PathOrUrl+ url.Substring(1);
                    }
                    else
                    {
                        downloadUrl = baseUrl.PathOrUrl + url;
                    }
                }
                else
                {
                    if (url[0] == '/')
                    {
                        downloadUrl = baseUrl.PathOrUrl + url;
                    }
                    else
                    {
                        downloadUrl = baseUrl.PathOrUrl + "/" + url;
                    }
                }

                if (filePath[filePath.Length - 1] == '/')
                {
                    filePath += fileName;
                }
                else
                {
                    filePath += "/" + fileName;
                }

                _fileSystemManager.DownlodFromUrl(downloadUrl,
                    filePath);
            }
                return true;
        }
    }
}

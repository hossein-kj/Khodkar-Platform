using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using KS.Core.Exceptions;
using KS.Core.GlobalVarioable;
using KS.Core.Localization;
using KS.Core.Model.Core;

namespace KS.Core.FileSystemProvide.Base
{
    public abstract class BaseFileSystemManager
    {
        public virtual string PhysicalBaseAddress { get; set; } = HostingEnvironment.ApplicationPhysicalPath;

        public virtual  Task<bool> FileExistAsync(string filePath)
        {
            return Task.FromResult(File.Exists(RelativeToAbsolutePath(filePath)));
        }

        public virtual bool FileExist(string filePath)
        {
            return File.Exists(RelativeToAbsolutePath(filePath));
        }

        public virtual bool DirectoryExists(string path)
        {
            return Directory.Exists(RelativeToAbsolutePath(path));
        }

        public virtual async Task<bool> WriteAsync(string path,string file,FileMode fileMode= FileMode.Create, FileAccess fileAccess = FileAccess.ReadWrite, FileShare fileShare = FileShare.ReadWrite | FileShare.Delete)
        {
            using (var fs = new FileStream(RelativeToAbsolutePath(path), fileMode, fileAccess, fileShare))
            {
                using (var strWriter = new StreamWriter(fs,Encoding.UTF8))
                {
                    await strWriter.WriteAsync(file);
                }
                //var page = new UTF8Encoding(true).GetBytes(WebPageToJson(webPage));
                //await fs.WriteAsync(page, 0, page.Length);
            }
            return true;
        }

        public virtual async Task<string> ReadAsync(string path, FileMode fileMode = FileMode.Open, FileAccess fileAccess = FileAccess.ReadWrite, FileShare fileShare =  FileShare.ReadWrite | FileShare.Delete)
        {
                using (var fs = new FileStream(RelativeToAbsolutePath(path), fileMode, fileAccess, fileShare))
                {
                    using (var strReader = new StreamReader(fs, Encoding.UTF8))
                    {
                    return await strReader.ReadToEndAsync();
                    }

                    //var line = new UTF8Encoding(true);
                    //var b = new byte[1024];
                    //while (await fs.ReadAsync(b, 0, b.Length) > 1)
                    //{
                    //    page += line.GetString(b);
                    //}
                    //page = page;
                }
        }

        public virtual string CreatDirectoryIfNotExist(string path)
        {

            var realPath = RelativeToAbsolutePath(path);

                Directory.CreateDirectory(realPath);

            return realPath;
        }


        public virtual DirectoryInfo[] GetDirectories(string path, string searchPatern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            var realPath = RelativeToAbsolutePath(path);

            var currentDirInfo = new DirectoryInfo(realPath);
            return currentDirInfo.GetDirectories(searchPatern, searchOption);
        }

        public virtual FileInfo[] GetFiles(string path,string searchPatern="*",SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            var realPath = RelativeToAbsolutePath(path);

            var currentDirInfo = new DirectoryInfo(realPath);
            return currentDirInfo.GetFiles(searchPatern, searchOption);
        }

        public virtual int FileExtensionToFileType(string extension)
        {
            var imageExtension = new string[] { "ANI", "BMP", "CAL", "FAX", "GIF", "IMG", "JBG", "JPE", "JPEG", "JPG", "MAC", "PBM", "PCD", "PCX", "PCT", "PGM", "PNG", "PPM", "PSD", "RAS", "TGA", "TIFF", "WMF" };
            if (extension == "") return 0;
            return imageExtension.Contains(extension.Substring(1).ToUpper()) ? 1 : 0;
        }

        public virtual void MoveFile(string sourceFile, string destinationFile)
        {
            if(!File.Exists(RelativeToAbsolutePath(destinationFile)))
            File.Move(RelativeToAbsolutePath(sourceFile), RelativeToAbsolutePath(destinationFile));
        }

        public virtual void MoveDirectory(string sourceDir, string destinationDir)
        {
            if (!Directory.Exists(RelativeToAbsolutePath(destinationDir)))
                Directory.Move(RelativeToAbsolutePath(sourceDir), RelativeToAbsolutePath(destinationDir));
        }

        public virtual void CopyFile(string sourceFile, string destinationFile,bool overWrite=true)
        {
            File.Copy(RelativeToAbsolutePath(sourceFile), RelativeToAbsolutePath(destinationFile), overWrite);
        }

        public virtual void CopyDirectory(string sourceDir, string destinationDir, bool overWrite = true)
        {
            var sourceDirName = RelativeToAbsolutePath(sourceDir);
            var destinationDirName = RelativeToAbsolutePath(destinationDir);

            sourceDirName = sourceDirName.EndsWith(@"/") ? sourceDirName : sourceDirName + @"/";
            destinationDirName = destinationDirName.EndsWith(@"/") ? destinationDirName : destinationDirName + @"/";

            if (Directory.Exists(destinationDirName) == false)
            {
                Directory.CreateDirectory(destinationDirName);
            }

            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destinationDirName))
            {
                Directory.CreateDirectory(destinationDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destinationDirName, file.Name);
                file.CopyTo(temppath, overWrite);
            }

            // If copying subdirectories, copy them and their contents to new location.
          
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destinationDirName, subdir.Name);
                CopyDirectory(subdir.FullName, temppath, overWrite);
                }
          












            

            //foreach (var files in Directory.GetFiles(sourceDir))
            //{
            //    var fileInfo = new FileInfo(files);
            //    fileInfo.CopyTo($@"{destinationDir}/{fileInfo.Name}", overWrite);
            //}

            //return !(from drs in Directory.GetDirectories(sourceDir) let directoryInfo = new DirectoryInfo(drs)
            //         where CopyDirectory(drs, destinationDir + directoryInfo.Name) == false select drs).Any();
        }

        public virtual bool RenameFile(string oldFullName, string newFullName)
        {

            if (File.Exists(RelativeToAbsolutePath(newFullName)))
            {
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.RepeatedPath, newFullName));

            }

            File.Move(RelativeToAbsolutePath(oldFullName), RelativeToAbsolutePath(newFullName));
            return true;
        }

        public virtual bool RenameDirectory(string oldFullName, string newFullName)
        {

            if (File.Exists(RelativeToAbsolutePath(newFullName)))
            {
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.RepeatedPath, newFullName));

            }

            Directory.Move(RelativeToAbsolutePath(oldFullName), RelativeToAbsolutePath(newFullName));
            return true;
        }

        public virtual bool DeleteFile(string fullName)
        {

            if (File.Exists(RelativeToAbsolutePath(fullName)))
            {
                File.Delete(RelativeToAbsolutePath(fullName));
            }

           
            return true;
        }

        public virtual bool MoveAllFileSByPattern(string sourceDir, string destinationDir, string pattern,bool overWrite)
        {
            var dir = new DirectoryInfo(RelativeToAbsolutePath(sourceDir));
            var fileNames = new List<KeyValue>();
            foreach (var file in dir.EnumerateFiles(pattern))
            {
                fileNames.Add(new KeyValue() {Key = file.Name,Value = file.FullName});
            }

            foreach (var file in fileNames)
            {
                if (overWrite)
                    DeleteFile((RelativeToAbsolutePath(destinationDir) + "/" + file.Key).Replace("//", "/"));

                File.Move(file.Value, (RelativeToAbsolutePath(destinationDir) + "/" + file.Key).Replace("//","/"));
            }

            return true;
        }

        public virtual bool DeleteDirectory(string fullName)
        {
            //fullName = RelativeToAbsolutePath(fullName);
            if (Directory.Exists(RelativeToAbsolutePath(fullName)))
            {
                Directory.Delete(RelativeToAbsolutePath(fullName),true);
            }


            return true;
        }


        public virtual string RelativeToAbsolutePath(string path)
        {
            var temp = path;
            if (path.StartsWith("/") || path.StartsWith("~/"))
            {

                if (path[0] != '~')
                    path = path[0] == '/' ? "~" + path : "~/" + path;


                path = HostingEnvironment.MapPath(path);
                if (path == null)
                    throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.PathNotFound, temp));
       
            }
            return path;
        }
        public virtual float GetFileSize(string path)
        {
            //var length = new FileInfo(path).Length;
            //float result = (length/1000000);
            //if(Math.Abs(result) <= 0)
            //    result = (length / 1000);
            return new FileInfo(RelativeToAbsolutePath(path)).Length;
        }

        public virtual byte[] FileToByte(string path)
        {
           return File.ReadAllBytes(RelativeToAbsolutePath(path));
        }
        public virtual string GetExtension(string path)
        {
            return System.IO.Path.GetExtension(RelativeToAbsolutePath(path));
        }

        public virtual bool DownlodFromUrl(string url,string filePath)
        {
            var client = new WebClient();
            client.DownloadFile(url, RelativeToAbsolutePath(filePath));
            return true;
        }
    }
}
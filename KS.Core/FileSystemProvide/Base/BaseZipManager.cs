using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using Ionic.Zip;
using KS.Core.Model;
using KS.Core.Model.FileSystem;

namespace KS.Core.FileSystemProvide.Base
{
    public abstract class BaseZipManager 
    {
        protected readonly IFileSystemManager FileSystemManager;
        protected BaseZipManager(IFileSystemManager fileSystemManager)
        {
            FileSystemManager = fileSystemManager;
        }
        public virtual bool Zip(ZipOprationInfo zipOprationInfo)
        {
            using (var zip = zipOprationInfo.IsNew ? new ZipFile() : ZipFile.Read(zipOprationInfo.DestinationPath))
            {

                if (!string.IsNullOrEmpty(zipOprationInfo.Password))
                {
                    zip.Password = zipOprationInfo.Password;
                    var encryption = EncryptionAlgorithm.PkzipWeak;
                    if (zipOprationInfo.Encryption != null)
                        Enum.TryParse(zipOprationInfo.Encryption, true, out encryption);
                    zip.Encryption = encryption;
                }
                if (zipOprationInfo.CompressionLevel != null)
                {
                    var compressionLevel = Ionic.Zlib.CompressionLevel.Default;
                    Enum.TryParse(zipOprationInfo.CompressionLevel, true, out compressionLevel);
                    zip.CompressionLevel = compressionLevel;
                }
                if (zipOprationInfo.Files != null)
                {
                    foreach (var file in zipOprationInfo.Files)
                    {
                        zip.UpdateFile(zipOprationInfo.SourcePath.EndsWith(@"\") ? zipOprationInfo.SourcePath + file : zipOprationInfo.SourcePath + @"\" + file);
                    }
                }
                if (zipOprationInfo.Folders != null)
                 foreach (var folder in zipOprationInfo.Folders)
                {
                        var currentFolder = folder;
                        if (folder == "/" || folder == @"\")
                            currentFolder = "";
                    zip.UpdateDirectory(zipOprationInfo.SourcePath.EndsWith(@"\") ? zipOprationInfo.SourcePath + currentFolder : zipOprationInfo.SourcePath + @"\" + currentFolder);
                }
                zip.Save(zipOprationInfo.DestinationPath.ToLower().IndexOf(".zip", StringComparison.Ordinal) > -1
                    ? zipOprationInfo.DestinationPath : zipOprationInfo.DestinationPath + ".zip");
            }
            return true;
        }
        public virtual bool UnZip(UnZipOprationInfo unZipOprationInfo)
        {
            using (var zip = ZipFile.Read(unZipOprationInfo.SourcePath))
            {
                if (unZipOprationInfo.Password != "")
                {
                    zip.Password = unZipOprationInfo.Password;
                }
                if (unZipOprationInfo.OverWrite != "")
                {
                    var overWrite = ExtractExistingFileAction.OverwriteSilently;
                    Enum.TryParse(unZipOprationInfo.OverWrite, true, out overWrite);
                    zip.ExtractAll(unZipOprationInfo.DestinationPath, overWrite);
                }
                else
                {
                    zip.ExtractAll(unZipOprationInfo.DestinationPath);
                }
            }
            return true;
        }
        public virtual List<ZipInfo> OpenZip(string zipFullName, string orderBy, int skip, int take, out int count)
        {
            var zipInfos = new List<ZipInfo>();
            using (var zip = ZipFile.Read(FileSystemManager.RelativeToAbsolutePath(zipFullName)))
            {
                count = zip.Count;
                var id = 1;


                zipInfos.AddRange(zip.AsQueryable().OrderBy(orderBy).Skip(skip).Take(take).ToList().Select(e => new ZipInfo()
                {
                    Id = id++,
                    FileName = e.FileName,
                    //LastModified = e.LastModified,
                    UncompressedSize = e.UncompressedSize.ToString(),
                    //CompressedSize = e.CompressedSize.ToString(),
                    //CompressionRatio = e.CompressionRatio.ToString("{ 3, 5:F0 }%"),
                    UsesEncryption = e.UsesEncryption
                }));
            }
            return zipInfos;
        }
    }
}

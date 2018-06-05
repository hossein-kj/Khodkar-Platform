using System.Collections.Generic;
using System.Threading.Tasks;
using KS.Core.Model.FileSystem;
using KS.Model.FileManagement;
using Newtonsoft.Json.Linq;

namespace KS.Business.FileManagement
{
    public interface IFileSystemBiz
    {
        List<DiskInfo> GetPathDirectoriesAndFiles(string path, string orderBy, int skip, int take,out int count, bool byFile = true, string searchPatern = "*", bool allDirectories = false,bool createThumbnail=false);
        bool Move(DiskOprationInfo moveInfo);
        Task Save(JObject data);
        bool Copy(DiskOprationInfo copyInfo);
        bool Zip(ZipOprationInfo zipOprationInfo);
        bool UnZip(UnZipOprationInfo unZipOprationInfo);
        List<ZipInfo> OpenZip(string zipFullName, string orderBy, int skip, int take, out int count);
        bool Rename(RenameOprationInfo renameInfo);
        bool Delete(DeleteOprationInfo deleteInfo);
        bool CreateDirectory(string path);
        bool DeleteDirectory(string path);
        Task<string> GetFileContenAsync(string path);
        bool DownlodFromUrl(int baseUrlId, string url, string filePath, string fileName);
    }
}
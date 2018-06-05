using System.IO;
using System.Threading.Tasks;

namespace KS.Core.FileSystemProvide.Adapters
{
    public interface IDefaultFileSystemAdapter
    {
        bool CopyDirectory(string sourceDir, string destinationDir, bool overWrite = true);
        void CopyFile(string sourceFile, string destinationFile, bool overWrite = true);
        string CreatDirectoryIfNotExist(string path);
        bool DeleteDirectory(string fullName);
        bool DeleteFile(string fullName);
        Task<bool> FileExistAsync(string filePath);
        bool FileExist(string filePath);
        bool DirectoryExists(string path);
        int FileExtensionToFileType(string extension);
        byte[] FileToByte(string path);
        DirectoryInfo[] GetDirectories(string path, string searchPatern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly);
        string GetExtension(string path);
        FileInfo[] GetFiles(string path, string searchPatern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly);
        float GetFileSize(string path);
        void MoveDirectory(string sourceDir, string destinationDir);
        void MoveFile(string sourceFile, string destinationFile);
        Task<string> ReadAsync(string path, FileMode fileMode = FileMode.Open, FileAccess fileAccess = FileAccess.ReadWrite, FileShare fileShare = FileShare.ReadWrite | FileShare.Delete);
        bool RenameDirectory(string oldFullName, string newFullName);
        bool RenameFile(string oldFullName, string newFullName);
        Task<bool> WriteAsync(string path, string file, FileMode fileMode = FileMode.Create, FileAccess fileAccess = FileAccess.ReadWrite, FileShare fileShare = FileShare.ReadWrite | FileShare.Delete);
        string RelativeToAbsolutePath(string path);
    }
}
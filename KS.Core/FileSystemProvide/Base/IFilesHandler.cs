using KS.Core.Model.FileSystem;
using System.Collections.Generic;
using System.Web;

namespace KS.Core.FileSystemProvide.Base
{
    public interface IFilesHandler
    {
        //string CheckThumb(string type, string fileName);
        //string DeleteFile(string file);
        //void DeleteFiles(string pathToDelete);
        //List<string> FilesList();
        //JsonFiles GetFileList();
        void UploadAndShowResults(HttpContext contentBase, List<ViewDataUploadFilesResult> resultList);
        ViewDataUploadFilesResult UploadResult(string fileName, int fileSize, string fileFullPath, string errorCode = "");
    }
}
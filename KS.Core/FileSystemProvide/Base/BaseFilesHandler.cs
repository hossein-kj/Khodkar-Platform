using KS.Core.GlobalVarioable;
using KS.Core.Model.FileSystem;
using KS.Core.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Hosting;

namespace KS.Core.FileSystemProvide.Base
{
    public class BaseFilesHandler
    {

        protected string DeleteUrl = null;
        protected string DeleteType = "GET";
        private string _storagePath = null;
        protected string StoragePath
        {
            get { return Path.Combine(HostingEnvironment.MapPath(AuthorizeManager.AuthorizeActionOnPath(_storagePath.Replace("//", "/"), ActionKey.WriteToDisk))); }
            set { _storagePath = value; }
        }
        protected string UrlBase = null;
        //string tempPath = null;
        //ex:"~/Files/something/";
        protected string ServerMapPath = null;


        //public void DeleteFiles(string pathToDelete)
        //{

        //    string path = HostingEnvironment.MapPath(pathToDelete);

        //    System.Diagnostics.Debug.WriteLine(path);
        //    if (Directory.Exists(path))
        //    {
        //        DirectoryInfo di = new DirectoryInfo(path);
        //        foreach (FileInfo fi in di.GetFiles())
        //        {
        //            System.IO.File.Delete(fi.FullName);
        //            System.Diagnostics.Debug.WriteLine(fi.Name);
        //        }

        //        di.Delete(true);
        //    }
        //}

        //public string DeleteFile(string file)
        //{
        //    System.Diagnostics.Debug.WriteLine("DeleteFile");
        //    //    var req = HttpContext.Current;
        //    System.Diagnostics.Debug.WriteLine(file);

        //    string fullPath = Path.Combine(StoragePath, file);
        //    System.Diagnostics.Debug.WriteLine(fullPath);
        //    System.Diagnostics.Debug.WriteLine(System.IO.File.Exists(fullPath));
        //    string thumbPath = "/" + file + "80x80.jpg";
        //    string partThumb1 = Path.Combine(StoragePath, "thumbs");
        //    string partThumb2 = Path.Combine(partThumb1, file + "80x80.jpg");

        //    System.Diagnostics.Debug.WriteLine(partThumb2);
        //    System.Diagnostics.Debug.WriteLine(System.IO.File.Exists(partThumb2));
        //    if (System.IO.File.Exists(fullPath))
        //    {
        //        //delete thumb 
        //        if (System.IO.File.Exists(partThumb2))
        //        {
        //            System.IO.File.Delete(partThumb2);
        //        }
        //        System.IO.File.Delete(fullPath);
        //        string succesMessage = "Ok";
        //        return succesMessage;
        //    }
        //    string failMessage = "Error Delete";
        //    return failMessage;
        //}
        //public JsonFiles GetFileList()
        //{

        //    var r = new List<ViewDataUploadFilesResult>();

        //    string fullPath = Path.Combine(StoragePath);
        //    if (Directory.Exists(fullPath))
        //    {
        //        DirectoryInfo dir = new DirectoryInfo(fullPath);
        //        foreach (FileInfo file in dir.GetFiles())
        //        {
        //            int sizeInt = unchecked((int)file.Length);
        //            r.Add(UploadResult(file.Name,sizeInt,file.FullName));
        //        }

        //    }
        //    JsonFiles files = new JsonFiles(r);

        //    return files;
        //}

        public virtual void UploadAndShowResults(HttpContext contentBase, List<ViewDataUploadFilesResult> resultList)
        {
            var httpRequest = contentBase.Request;

            StoragePath = httpRequest.Form[0];


            Directory.CreateDirectory(StoragePath);
            // Create new folder for thumbs
            // Directory.CreateDirectory(StoragePath + "/thumbs/");

            foreach (string inputTagName in httpRequest.Files)
            {

                var headers = httpRequest.Headers;

                // var file = httpRequest.Files[inputTagName];


                if (string.IsNullOrEmpty(headers["X-File-Name"]))
                {

                    UploadWholeFile(contentBase, resultList);
                }
                else
                {

                    UploadPartialFile(headers["X-File-Name"], contentBase, resultList);
                }
            }
        }


        protected virtual void UploadWholeFile(HttpContext requestContext, List<ViewDataUploadFilesResult> statuses)
        {

            var request = requestContext.Request;
            StoragePath = request.Form[0];
            for (int i = 0; i < request.Files.Count; i++)
            {
                var file = request.Files[i];

                var fullPath = Path.Combine(StoragePath, Path.GetFileName(file.FileName));

                if (!File.Exists(fullPath))
                {
                    file.SaveAs(fullPath);
                    //Create thumb
                    //string[] imageArray = file.FileName.Split('.');
                    //if (imageArray.Length != 0)
                    //{
                    //    string extansion = imageArray[imageArray.Length - 1].ToLower();
                    //    if (extansion != "jpg" && extansion != "png" && extansion != "jpeg") //Do not create thumb if file is not an image
                    //    {

                    //    }
                    //    else
                    //    {
                    //        var ThumbfullPath = Path.Combine(pathOnServer, "thumbs");
                    //        //string fileThumb = file.FileName + ".80x80.jpg";
                    //        string fileThumb = Path.GetFileNameWithoutExtension(file.FileName) + "80x80.jpg";
                    //        var ThumbfullPath2 = Path.Combine(ThumbfullPath, fileThumb);
                    //        using (MemoryStream stream = new MemoryStream(System.IO.File.ReadAllBytes(fullPath)))
                    //        {
                    //            var thumbnail = new WebImage(stream).Resize(80, 80);
                    //            thumbnail.Save(ThumbfullPath2, "jpg");
                    //        }

                    //    }
                    //}
                    statuses.Add(UploadResult(file.FileName, file.ContentLength, file.FileName));
                }
                else
                {
                    statuses.Add(UploadResult(file.FileName, file.ContentLength, file.FileName, "fileAlreadyExist"));
                }


            }
        }



        protected virtual void UploadPartialFile(string fileName, HttpContext requestContext, List<ViewDataUploadFilesResult> statuses)
        {
            var request = requestContext.Request;
            StoragePath = request.Form[0];
            if (request.Files.Count != 1) throw new HttpRequestValidationException("Attempt to upload chunked file containing more than one fragment per request");
            var file = request.Files[0];
            var inputStream = file.InputStream;

            var fullName = Path.Combine(StoragePath, Path.GetFileName(file.FileName));
            //var ThumbfullPath = Path.Combine(fullName, Path.GetFileName(file.FileName + "80x80.jpg"));
            //ImageHandler handler = new ImageHandler();

            //var ImageBit = ImageHandler.LoadImage(fullName);
            //handler.Save(ImageBit, 80, 80, 10, ThumbfullPath);
            using (var fs = new FileStream(fullName, FileMode.Append, FileAccess.Write))
            {
                var buffer = new byte[1024];

                var l = inputStream.Read(buffer, 0, 1024);
                while (l > 0)
                {
                    fs.Write(buffer, 0, l);
                    l = inputStream.Read(buffer, 0, 1024);
                }
                fs.Flush();
                fs.Close();
            }
            statuses.Add(UploadResult(file.FileName, file.ContentLength, file.FileName));
        }
        public virtual ViewDataUploadFilesResult UploadResult(string fileName, int fileSize, string fileFullPath, string errorCode = "")
        {
            string getType = System.Web.MimeMapping.GetMimeMapping(fileFullPath);
            var result = new ViewDataUploadFilesResult()
            {
                name = fileName,
                size = fileSize,
                type = getType,
                url = UrlBase + fileName,
                deleteUrl = DeleteUrl + fileName,
                thumbnailUrl = "",//CheckThumb(getType, fileName),
                deleteType = DeleteType,
                errorCode = errorCode
            };
            return result;
        }

        //public string CheckThumb(string type,string fileName)
        //{
        //    var splited = type.Split('/');
        //    if (splited.Length == 2)
        //    {
        //        string extansion = splited[1].ToLower();
        //        if(extansion.Equals("jpeg") || extansion.Equals("jpg") || extansion.Equals("png") || extansion.Equals("gif"))
        //        {
        //            string thumbnailUrl = _urlBase + "thumbs/" + Path.GetFileNameWithoutExtension(fileName) + "80x80.jpg";
        //            return thumbnailUrl;
        //        }
        //        else
        //        {
        //            if (extansion.Equals("octet-stream")) //Fix for exe files
        //            {
        //                return "/Content/Free-file-icons/48px/exe.png";

        //            }
        //            if (extansion.Contains("zip")) //Fix for exe files
        //            {
        //                return "/Content/Free-file-icons/48px/zip.png";
        //            }
        //            string thumbnailUrl = "/Content/Free-file-icons/48px/"+ extansion +".png";
        //            return thumbnailUrl;
        //        }
        //    }
        //    else
        //    {
        //        return _urlBase + "/thumbs/" + Path.GetFileNameWithoutExtension(fileName) + "80x80.jpg";
        //    }

        //}
        //public List<string> FilesList()
        //{

        //    List<string> Filess = new List<string>();
        //    string path = HostingEnvironment.MapPath(_serverMapPath);
        //    System.Diagnostics.Debug.WriteLine(path);
        //    if (Directory.Exists(path))
        //    {
        //        DirectoryInfo di = new DirectoryInfo(path);
        //        foreach (FileInfo fi in di.GetFiles())
        //        {
        //            Filess.Add(fi.Name);
        //            System.Diagnostics.Debug.WriteLine(fi.Name);
        //        }

        //    }
        //    return Filess;
        //}
    }

}


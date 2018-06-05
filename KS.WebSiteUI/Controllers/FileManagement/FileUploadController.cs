
using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using System.Web;
using Newtonsoft.Json;
using KS.Core.Model.FileSystem;
using KS.Core.FileSystemProvide.Base;

namespace KS.WebSiteUI.Controllers.FileManagement
{
    [RoutePrefix("fms")]
    public class FileUploadController : ApiController
    {
        IFilesHandler _filesHandler;

        public FileUploadController(IFilesHandler filesHandler)
        {
            _filesHandler = filesHandler;
        }
      
        //public ActionResult Show()
        //{
        //    JsonFiles ListOfFiles = filesHelper.GetFileList();
        //    //var model = new FilesViewModel()
        //    //{
        //    //    Files = ListOfFiles.files
        //    //};

        //    //return View(model);
        //    //مدل از محتوای ارایه زیر
        //    return View(new ViewDataUploadFilesResult[10]);

        //}

        [HttpPost]
        [Route("upload")]
        public JObject Upload()
        {
            var resultList = new List<ViewDataUploadFilesResult>();
           
       

            _filesHandler.UploadAndShowResults(HttpContext.Current, resultList);

            //var files = new JsonFiles(resultList);


            return JObject.Parse(JsonConvert.SerializeObject
          (new { files = resultList }, Formatting.None));
            
        }
        //public JsonResult GetFileList()
        //{
        //    var list=filesHelper.GetFileList();
        //    return Json(list,JsonRequestBehavior.AllowGet);
        //}
        //[HttpGet]
        //public JsonResult DeleteFile(string file)
        //{
        //    filesHelper.DeleteFile(file);
        //    return Json("OK", JsonRequestBehavior.AllowGet);
        //}
       
    }
}
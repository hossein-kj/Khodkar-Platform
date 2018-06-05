using System.Threading.Tasks;
using System.Web.Http;
using KS.Business.ContenManagment.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using KS.WebSiteUI.Controllers.Base;

namespace KS.WebSiteUI.Controllers.ContentManagement.Http
{
    public class FilepPathController : BaseAuthorizedWebApiController
    {
       private readonly IFilePathBiz _filePathBiz;

       public FilepPathController(IFilePathBiz filePathBiz)
        {
            _filePathBiz = filePathBiz;
        }

        [Route("cms/filepath/Save")]
        [HttpPost]
        public async Task<JObject> Save(JObject data)
        {
            return JObject.Parse(JsonConvert.SerializeObject
            (await _filePathBiz.Save(data), Formatting.None,
                new JsonSerializerSettings() {ReferenceLoopHandling = ReferenceLoopHandling.Ignore}));
        }

        [Route("cms/filepath/delete")]
        [HttpPost]
        public async Task<bool> DeleteFilePath(JObject data)
        {
            return await _filePathBiz.Delete(data);
        }

        [Route("cms/localfilepath/Save")]
        [HttpPost]
        public async Task<JObject> SaveTranslate(JObject data)
        {
            return JObject.Parse(JsonConvert.SerializeObject
                    (await _filePathBiz.SaveTranslate(data), Formatting.None,
                    new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
        }
    }
}
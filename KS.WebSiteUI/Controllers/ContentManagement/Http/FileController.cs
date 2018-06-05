using System.Threading.Tasks;
using System.Web.Http;
using KS.Business.ContenManagment.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using KS.WebSiteUI.Controllers.Base;

namespace KS.WebSiteUI.Controllers.ContentManagement.Http
{
    public class FileController : BaseAuthorizedWebApiController
    {
       private readonly IFileBiz _fileBiz;

       public FileController(IFileBiz fileBiz)
        {
            _fileBiz = fileBiz;
        }

        [Route("cms/file/Save")]
        [HttpPost]
        public async Task<JObject> Save(JObject data)
        {
            return JObject.Parse(JsonConvert.SerializeObject
            (await _fileBiz.Save(data), Formatting.None,
                new JsonSerializerSettings() {ReferenceLoopHandling = ReferenceLoopHandling.Ignore}));
        }

        [Route("cms/file/delete")]
        [HttpPost]
        public async Task<bool> DeleteFile(JObject data)
        {
            return await _fileBiz.Delete(data);
        }

        [Route("cms/localfile/Save")]
        [HttpPost]
        public async Task<JObject> SaveTranslate(JObject data)
        {
            return JObject.Parse(JsonConvert.SerializeObject
                    (await _fileBiz.SaveTranslate(data), Formatting.None,
                    new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
        }
    }
}
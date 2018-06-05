using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web;
using KS.Business.Develop.Code.BrowserCode;
using KS.WebSiteUI.Controllers.Base;

namespace KS.WebSiteUI.Controllers.Develop.Code.BrowserCode
{
    public class BrowserCodeController : BaseAuthorizedWebApiController
    {
       private readonly IBrowserCodeBiz _browserCodeBiz;

       public BrowserCodeController(IBrowserCodeBiz browserCodeBiz)
        {
            _browserCodeBiz = browserCodeBiz;
        }

        [Route("develop/code/browser/getbundledependency/{bundleId}")]
        [HttpGet]
        public async Task<JArray> GetBundleDependency(int bundleId)
        {
            return JArray.Parse(JsonConvert.SerializeObject((await _browserCodeBiz.GetBundleDependency(bundleId)
                
                .ConfigureAwait(false)).Select(bd => new
            {
                id = bd.Key,
                path = bd.Value
            }), Formatting.None));
        }

        [Route("develop/code/browser/get/{id}")]
        [HttpGet]
        public async Task<JObject> Get(int id)
        {
            return await  _browserCodeBiz.GetAsync(id);
        }

        [Route("develop/code/browser/GetCodeContent/{path}/{id}")]
        [HttpGet]
        public async Task<string> GetCodeContent(string path, int id)
        {
            return await _browserCodeBiz.GetCodeContentAsync(id,path);
        }

        [Route("develop/code/browser/Save")]
        [HttpPost]
        public async Task<JObject> Save(JObject data)
        {
            return JObject.Parse(JsonConvert.SerializeObject
                (await _browserCodeBiz.Save(data), Formatting.None,
                new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
        }

        [Route("develop/code/browser/file/Save")]
        [HttpPost]
        public async Task<bool> SaveFile(JObject data)
        {
            return await _browserCodeBiz.SaveFile(data);
        }

        [Route("develop/code/browser/CheckJavascriptCode")]
        [HttpPost]
        public bool CheckJavascriptCode(JObject data)
        {
            return  _browserCodeBiz.CheckJavascriptCode(data);
        }

        [Route("develop/code/browser/delete")]
        [HttpPost]
        public async Task<bool> Delete(JObject data)
        {
            return await _browserCodeBiz.Delete(data);
        }

        [Route("develop/code/browser/bundle/compile")]
        [HttpPost]
        public async Task<bool> Compile(JObject data)
        {
            return await _browserCodeBiz.Compile(data,
                HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath,""));


        }

        [Route("develop/code/browser/bundle/Save")]
        [HttpPost]
        public async Task<JObject> SaveBundle(JObject data)
        {
            return JObject.Parse(JsonConvert.SerializeObject
                (await _browserCodeBiz.SaveBundleOrBundleSource(data), Formatting.None,
                new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
        }

        [Route("develop/code/browser/bundle/delete")]
        [HttpPost]
        public async Task<bool> DeleteBundle(JObject data)
        {
            return await _browserCodeBiz.DeleteBundleOrBundleSource(data);
        }

        [Route("develop/code/browser/bundle/source/Save")]
        [HttpPost]
        public async Task<JObject> SaveBundleSource(JObject data)
        {
            return JObject.Parse(JsonConvert.SerializeObject
                (await _browserCodeBiz.SaveBundleOrBundleSource(data,true), Formatting.None,
                new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
        }

        [Route("develop/code/browser/bundle/source/delete")]
        [HttpPost]
        public async Task<bool> DeleteBundleSource(JObject data)
        {
            return await _browserCodeBiz.DeleteBundleOrBundleSource(data,true);
        }

        [Route("develop/code/browser/GetChange/{changeId}/{path}/{codeId}")]
        [HttpGet]
        public async Task<string> GetChangeFromSourceControl(int changeId, string path, int codeId)
        {
            return await _browserCodeBiz.GetChangeFromSourceControlAsync(changeId, codeId, path);
        }

        [Route("develop/code/browser/GetChanges/{codePath}/{codeName}/{orderBy}/{skip}/{take}/{comment}/{user}/{fromDateTime}/{toDateTime}")]
        [HttpGet]
        public JObject GetChangesFromSourceControl(string codePath
    , string codeName, string orderBy, int skip, int take
    , string comment
    , string user
    , string fromDateTime
    , string toDateTime)
        {
            return _browserCodeBiz.GetChangesFromSourceControl(orderBy, skip, take
            , comment
            , user
            , fromDateTime
            , toDateTime
            , codePath
            , codeName);
        }
    }
}
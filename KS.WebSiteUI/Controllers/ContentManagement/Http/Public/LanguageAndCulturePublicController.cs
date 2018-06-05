
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using KS.Business.ContenManagment.Base;
using KS.WebSiteUI.Controllers.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KS.WebSiteUI.Controllers.ContentManagement.Http.Public
{
    public class LanguageAndCulturePublicController : BasePublicWebApiController
    {
        private readonly ILanguageAndCultureBiz _languageAndCultureBiz;

        public LanguageAndCulturePublicController(ILanguageAndCultureBiz languageAndCultureBiz)
        {
            _languageAndCultureBiz = languageAndCultureBiz;
        }
        [Route("cms/languageAndCulture/public/getAll")]
        public async Task<JArray> GetLanguages()
        {
            var lang = await _languageAndCultureBiz.GetLanguagesAsync().ConfigureAwait(false);
            return JArray.Parse(JsonConvert.SerializeObject(lang.Select(lg => new
            {
                id = lg.Id,
                country = lg.Country,
                culture = lg.Culture,
                flagUrl = lg.Flag.Url,
                language = lg.Language,
                isRightToLeft = lg.IsRightToLeft
            }), Formatting.None));
        }
    }
}

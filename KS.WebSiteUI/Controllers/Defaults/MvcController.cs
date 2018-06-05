
using System.Threading.Tasks;
using System.Web.Mvc;
using KS.Business.ContenManagment.Base;
using KS.WebSiteUI.Controllers.Base;

namespace KS.WebSiteUI.Controllers.Defaults
{
    public class MvcController : BaseMvcController
    {
        private readonly IWebPageBiz _webPageBiz;

        public MvcController(IWebPageBiz contentManagementBiz)
        {
            _webPageBiz = contentManagementBiz;

        }


        [Route("{*url}")]
        public async Task<string> GetWebPage(string url)
        {
            return await _webPageBiz.ToStringAsync(url);
         
        }


    }
}
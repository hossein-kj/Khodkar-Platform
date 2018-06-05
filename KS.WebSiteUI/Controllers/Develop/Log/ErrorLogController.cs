using KS.Business.Develop.Log;
using Newtonsoft.Json.Linq;
using System.Web.Http;
using KS.WebSiteUI.Controllers.Base;

namespace KS.WebSiteUI.Controllers.Develop.Log
{
    public class ErrorLogController : BaseAuthorizedWebApiController
    {
        private readonly IErrorLogBiz _errorLogBiz;

        public ErrorLogController(IErrorLogBiz errorLogBiz)
        {
            _errorLogBiz = errorLogBiz;
        }

        [Route("develop/reports/ErrorLog/GetByPagination/{orderBy}/{skip}/{take}/{typeOrSourceOrMessage}/{user}/{fromDateTime}/{toDateTime}")]
        [HttpGet]
        public JObject GetByPagination(string orderBy, int skip, int take, string typeOrSourceOrMessage, string user, string fromDateTime, string toDateTime)
        {
            return _errorLogBiz.GetByPagination(orderBy, skip, take,typeOrSourceOrMessage,user,fromDateTime,toDateTime);
        }

        [Route("develop/reports/ErrorLog/GetById/{id}")]
        [HttpGet]
        public JObject GetById(int id)
        {
            return _errorLogBiz.GetErrorById(id);
        }

        [Route("develop/reports/ErrorLog/delete/")]
        [HttpPost]
        public bool Delete(JObject data)
        {
            return _errorLogBiz.Delete(data);
        }

        [Route("develop/reports/ErrorLog/BackUp/")]
        [HttpGet]
        public bool BackUp()
        {
            return _errorLogBiz.BackUp();
        }
    }
}

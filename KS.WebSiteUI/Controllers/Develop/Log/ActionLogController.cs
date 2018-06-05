using KS.Business.Develop.Log;
using Newtonsoft.Json.Linq;
using System.Web.Http;
using KS.WebSiteUI.Controllers.Base;
using System.Threading.Tasks;

namespace KS.WebSiteUI.Controllers.Develop.Log
{
    public class ActionLogController : BaseAuthorizedWebApiController
    {
        private readonly IActionLogBiz _actionLogBiz;

        public ActionLogController(IActionLogBiz actionLogBiz)
        {
            _actionLogBiz = actionLogBiz;
        }

        [Route("develop/reports/ActionLog/GetByServiceIdAndPagination/{orderBy}/{skip}/{take}/{serviceId}/{user}/{fromDateTime}/{toDateTime}")]
        [HttpGet]
        public async Task<JObject> GetByServiceIdAndPagination(string orderBy, int skip, int take, int serviceId, string user, string fromDateTime, string toDateTime)
        {
            return await _actionLogBiz.GetByServiceIdAndPaginationAsync(orderBy, skip, take, serviceId, user, fromDateTime, toDateTime);
        }

        [Route("develop/reports/ActionLog/GetByPagination/{orderBy}/{skip}/{take}/{serviceUrl}/{nameOrUrlOrUser}/{fromDateTime}/{toDateTime}")]
        [HttpGet]
        public JObject GetByPagination(string orderBy, int skip, int take,string serviceUrl,string nameOrUrlOrUser, string fromDateTime,string toDateTime)
        {
            return _actionLogBiz.GetByPagination(orderBy, skip, take, serviceUrl, nameOrUrlOrUser, fromDateTime,toDateTime);
        }

        [Route("develop/reports/ActionLog/GetById/{id}")]
        [HttpGet]
        public JObject GetById(int id)
        {
            return _actionLogBiz.GetActionById(id);
        }

        [Route("develop/reports/ActionLog/delete/")]
        [HttpPost]
        public bool Delete(JObject data)
        {
            return _actionLogBiz.Delete(data);
        }

        [Route("develop/reports/ActionLog/ToggleEnableLog/")]
        [HttpGet]
        public bool ToggleEnableLogUntilNextApplicationRestart()
        {
            return _actionLogBiz.ToggleEnableLogUntilNextApplicationRestart();
        }

        [Route("develop/reports/ActionLog/BackUp/")]
        [HttpGet]
        public bool BackUp()
        {
            return _actionLogBiz.BackUp();
        }

        [Route("develop/reports/ActionLog/LogStatus/")]
        [HttpGet]
        public bool GetLogStatus()
        {
            return _actionLogBiz.GetLogStatus();
        }
    }
}

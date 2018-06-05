
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using Newtonsoft.Json;
using KS.Core.Utility;
using System.Threading.Tasks;
using KS.Core.GlobalVarioable;
using System.Data.Entity;
using KS.Core.Security;
using KS.Core.Localization;
using KS.Core.Model.Core;
using KS.DataAccess.Contexts.Base;

namespace KS.Business.Develop.Log
{
    public class ActionLogBiz : IActionLogBiz
    {
        private readonly Core.Log.Base.IActionLogManager _actionLogManager;
        private readonly IContentManagementContext _contentManagementContext;
        public ActionLogBiz(Core.Log.Base.IActionLogManager actionLogManager, IContentManagementContext contentManagementContext)
        {
            _actionLogManager = actionLogManager;
            _contentManagementContext = contentManagementContext;
        }
        public async Task<JObject> GetByServiceIdAndPaginationAsync(string orderBy, int skip, int take, int serviceId, string user, string fromDateTime, string toDateTime)
        {

            var serviceByPermission = await _contentManagementContext.MasterDataKeyValues
            .Where(md => (md.TypeId == (int)EntityIdentity.Service && md.Id == serviceId) ||
            (md.TypeId == (int)EntityIdentity.Permission && md.ForeignKey1 == (int)ActionKey.ViewServiceLog && md.ForeignKey3 == serviceId))
            .ToListAsync();

            var validService = (from service in serviceByPermission
                    from permission in serviceByPermission
                    where service.Id == permission.ForeignKey3
                    where AuthorizeManager.IsAuthorize(permission.ForeignKey2)
                    select new KeyValue()
                    {
                        Key = service.Id.ToString(),
                        Value = service.PathOrUrl
                    }).ToList();

            if(validService.Count == 0)
                throw new UnauthorizedAccessException(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidGrant));

            var count = 0;


            return JObject.Parse(JsonConvert.SerializeObject
         (new
         {
             rows = _actionLogManager.GetByServiceUrlAndPagination(orderBy,
             skip,
             take,
             validService.First().Value,
             user,
             fromDateTime.Replace("-", Helper.RootUrl).Replace("_", ":"),
             toDateTime.Replace("-", Helper.RootUrl).Replace("_", ":"),
             out count)
             .Select(lg => new {
                 Id = lg.Id.ToString().Trim(),
                 lg.LocalDateTime,
                 lg.Name,
                 lg.ServiceUrl,
                 lg.Url,
                 lg.Type,
                 lg.User,
                 lg.ExecutionTimeInMilliseconds
             }),
             total = count
         }, Formatting.None));
        }
        public JObject GetByPagination(string orderBy, int skip, int take, string serviceUrl, string nameOrUrlOrUser, string fromDateTime, string toDateTime)
        {
            var count = 0;

           
            return JObject.Parse(JsonConvert.SerializeObject
         (new
         {
             rows = _actionLogManager.GetByPagination(orderBy, skip, take,
             serviceUrl.Replace(Config.UrlDelimeter,Helper.RootUrl),
             nameOrUrlOrUser.Replace(Config.UrlDelimeter, Helper.RootUrl),
             fromDateTime.Replace("-", Helper.RootUrl).Replace("_", ":"),
             toDateTime.Replace("-", Helper.RootUrl).Replace("_", ":"), out count)
             .Select(lg => new {
                 Id = lg.Id.ToString().Trim(),
                 lg.LocalDateTime,
                 lg.Name,
                 lg.ServiceUrl,
                 lg.Url,
                 lg.Type,
                 lg.User,
                 lg.ExecutionTimeInMilliseconds
             }),
             total = count
         }, Formatting.None));
        }

        public JObject GetActionById(int id)
        {

            return JObject.Parse(JsonConvert.SerializeObject
                (_actionLogManager.GetActionById(id), Formatting.None));
        }

        public bool Delete(JObject data)
        {
            return _actionLogManager.Delete(data);
        }

        public bool GetLogStatus()
        {
            return _actionLogManager.GetLogStatus();
        }

        public bool ToggleEnableLogUntilNextApplicationRestart()
        {
            
            return _actionLogManager.ToggleEnableLogUntilNextApplicationRestart();
        }

        public bool BackUp()
        {

            _actionLogManager.BackUp(LanguageManager.ToLocalDateTime(DateTime.UtcNow).Replace(":","-").Replace("/","_").Replace(".","__"));
            return true;
        }
    }
}

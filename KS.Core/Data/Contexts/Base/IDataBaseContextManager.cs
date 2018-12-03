using System.Collections.Generic;
using System.Threading.Tasks;
using KS.Core.Model.ContentManagement;
using KS.Core.Model.Core;
using KS.Core.Model.Develop;

namespace KS.Core.Data.Contexts.Base
{
    public interface IDataBaseContextManager
    {
        KeyValue GetMasterDataLocalKeyValue(int typeId, string code, string language);
        int AuthorizeViewDebugScriptOfWebPage(string guid, int userId, int typeId, int actionKey,int permissionTypeId);
        int AuthorizeViewSourceCodeOfMasterDataKeyValues(int serviceId, int userId, int typeId, int actionKey, int permissionTypeId);
        int AuthorizeViewDebugScriptOfCode(int bundleTypeId, int codeTypeId, string path, int userId, int actionKey, int permissionTypeId);
        List<string> GetPermissionOfPath(int permissionTypeId, int typeId, int actionKey, string urlOrPath);
        List<string> GetPermissionOfEntityId(int permissionTypeId, int typeId, int actionKey, int entityId);
        List<string> GetPermissionOfEntityIdByVersion(int permissionTypeId, int typeId, int actionKey, int entityId, int entityVersion);
        IAspect GetAspectForMasterDataKeyValueUrl(int actionKey, string url);
        IAspect GetAspectForPublicMasterDataKeyValueUrl(string url);
        IAspect GetAspectForWebPage(string url, string mobileUrl, string type);
        KeyValue GetUserInfoById(int userId);
        Task<string> GetPermissionOfUrlOrPathAsync(int typeId, int actionKey, string urlOrPath);
        IWebPageCore GetWebPageForView(string url, string type);
        IWebPageCore GetWebPageForPublish(string url, string type);
        List<KeyValue> GetRolesIdByGroupsId(List<int> groupsId);
        Task<List<KeyValue>> GetListOfDllReferencingPermissionFromeListOfDllIdAsync(int permissionTypeId, int actionKey, List<int> usedDll);
    }
}

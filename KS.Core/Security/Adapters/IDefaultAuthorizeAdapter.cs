
using System.Collections.Generic;
using KS.Core.GlobalVarioable;
using KS.Core.Model.Core;
using KS.Core.Model.Security;
using System.Threading.Tasks;


namespace KS.Core.Security.Adapters
{
    public interface IDefaultAuthorizeAdapter
    {
        bool IsAuthorizeToViewSourceCodeOfWebPage(string webPageGuid);
        bool IsAuthorizeToViewSourceCodeOfService(int serviceId);
        bool IsAuthorizeToViewSourceCodeOfDotNetCode(int codeId);
        string AuthorizeDebugJavascriptPath(string path);
        string AuthorizeActionOnPath(string path, ActionKey actionKey);

        string AuthorizeActionOnPath(string path, int actionKey);
        bool AuthorizeActionOnEntityId(int entityId, int entityTypeId, int actionKey);
        bool AuthorizeActionOnEntityIdByVersion(int entityId, int entityTypeId,int entityVersion, int actionKey);

        bool AuthorizeMasterDataKeyValueUrl(string url, ActionKey actionKey, out IAspect aspect);

        bool AuthorizeMasterDataKeyValueUrl(string url, int actionKey, out IAspect aspect);
        bool AuthorizeWebPageUrl(string url, string type, out IAspect aspect);

        KeyValue GetUserInfoById(int userId);

        bool IsAuthorize(int? role);

        void SetAndCheckModifyAndAccessRole(IAccessRole entity, dynamic newEntity, bool set = true);

        void CheckViewAccess(IAccessRole entity);

        void CacheUserRoles(List<int> userGroupsId);
        List<Group> GetUserRoles(List<int> userGroupsId);
        void CheckParentNodeModifyAccessForAddingChildNode(IAccessRole entity, int parentId);

        Task<List<string>> AuthorizeReferencingDllAsync(List<int> usedDll);

    }
}

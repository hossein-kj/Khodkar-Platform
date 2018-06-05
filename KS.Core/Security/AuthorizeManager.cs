

using System.Collections.Generic;
using KS.Core.GlobalVarioable;
using KS.Core.Model.Core;
using KS.Core.Model.Security;
using KS.Core.Security.Adapters;
using System.Threading.Tasks;


namespace KS.Core.Security
{
    public static class AuthorizeManager
    {
        private static IDefaultAuthorizeAdapter Adapter =>  DependencyProvider.DependencyManager.Get<IDefaultAuthorizeAdapter>();

        public static bool IsAuthorizeToViewSourceCodeOfWebPage(string webPageGuid)
        {
            return Adapter.IsAuthorizeToViewSourceCodeOfWebPage(webPageGuid);
        }

        public static bool IsAuthorizeToViewSourceCodeOfService(int serviceId)
        {
            return Adapter.IsAuthorizeToViewSourceCodeOfService(serviceId);
        }

        public static bool IsAuthorizeToViewSourceCodeOfDotNetCode(int codeId)
        {
            return Adapter.IsAuthorizeToViewSourceCodeOfDotNetCode(codeId);
        }
        public static string AuthorizeDebugJavascriptPath(string path)
        {
            
           return Adapter.AuthorizeDebugJavascriptPath(path);

        }
        public static string AuthorizeActionOnPath(string path, ActionKey actionKey)
        {
            return Adapter.AuthorizeActionOnPath(path, actionKey);
        }

        public static string AuthorizeActionOnPath(string path, int actionKey)
        {
            return Adapter.AuthorizeActionOnPath(path, actionKey);
        }

        public static bool AuthorizeActionOnEntityId(int entityId, int entityTypeId, int actionKey)
        {
            return Adapter.AuthorizeActionOnEntityId(entityId, entityTypeId, actionKey);
        }

        public static bool AuthorizeActionOnEntityIdByVersion(int entityId, int entityTypeId, int entityVersion, int actionKey)
        {
            return Adapter.AuthorizeActionOnEntityIdByVersion(entityId, entityTypeId, entityVersion, actionKey);
        }

        public static bool AuthorizeMasterDataKeyValueUrl(string url, ActionKey actionKey, out IAspect aspect)
        {
            return Adapter.AuthorizeMasterDataKeyValueUrl(url, actionKey, out aspect);
        }

        public static bool AuthorizeMasterDataKeyValueUrl(string url, int actionKey,out IAspect aspect)
        {
            return Adapter.AuthorizeMasterDataKeyValueUrl(url, actionKey, out aspect);
        }

        public static bool AuthorizeWebPageUrl(string url, string type, out IAspect aspect)
        {
            return Adapter.AuthorizeWebPageUrl(url, type, out aspect);
        }

        public static KeyValue GetUserInfoById(int userId)
        {
            return Adapter.GetUserInfoById(userId);
        }
        public static bool IsAuthorize(int? role)
        {
            return Adapter.IsAuthorize(role);
        }

        public static void SetAndCheckModifyAndAccessRole(IAccessRole entity, dynamic newEntity, bool set = true)
        {
            Adapter.SetAndCheckModifyAndAccessRole(entity, newEntity, set);
        }

        public static void CheckParentNodeModifyAccessForAddingChildNode(IAccessRole entity, int parentId)
        {
            Adapter.CheckParentNodeModifyAccessForAddingChildNode(entity, parentId);
        }

        public static void CheckViewAccess(IAccessRole entity)
        {
            Adapter.CheckViewAccess(entity);
        }

        public static void CacheUserRoles(List<int> userGroupsId)
        {
            Adapter.CacheUserRoles(userGroupsId);
        }

        public static List<Group> GetUserRoles(List<int> userGroupsId)
        {
           return Adapter.GetUserRoles(userGroupsId);
        }

        public static async Task<List<string>> AuthorizeReferencingDllAsync(List<int> usedDll)
        {
            return await Adapter.AuthorizeReferencingDllAsync(usedDll);
        }
    }

    public static class AuthorizeManager<T> where T:IAuthorizeAdapter
    {
        private static IAuthorizeAdapter Adapter => DependencyProvider.DependencyManager.Get<T>();

        public static bool IsAuthorizeToViewSourceCodeOfWebPage(string webPageGuid)
        {
            return Adapter.IsAuthorizeToViewSourceCodeOfWebPage(webPageGuid);
        }

        public static bool IsAuthorizeToViewSourceCodeOfService(int serviceId)
        {
            return Adapter.IsAuthorizeToViewSourceCodeOfService(serviceId);
        }

        public static bool IsAuthorizeToViewSourceCodeOfDotNetCode(int codeId)
        {
            return Adapter.IsAuthorizeToViewSourceCodeOfDotNetCode(codeId);
        }

        public static string AuthorizeDebugJavascriptPath(string path)
        {

            return Adapter.AuthorizeDebugJavascriptPath(path);

        }
        public static string AuthorizeActionOnPath(string path, ActionKey actionKey)
        {
            return Adapter.AuthorizeActionOnPath(path, actionKey);
        }

        public static string AuthorizeActionOnPath(string path, int actionKey)
        {
            return Adapter.AuthorizeActionOnPath(path, actionKey);
        }
        public static bool AuthorizeActionOnEntityId(int entityId, int entityTypeId, int actionKey)
        {
            return Adapter.AuthorizeActionOnEntityId(entityId, entityTypeId, actionKey);
        }
        public static bool AuthorizeActionOnEntityIdByVersion(int entityId, int entityTypeId, int entityVersion, int actionKey)
        {
            return Adapter.AuthorizeActionOnEntityIdByVersion(entityId, entityTypeId, entityVersion, actionKey);
        }
        public static bool AuthorizeMasterDataKeyValueUrl(string url, ActionKey actionKey, out IAspect aspect)
        {
            return Adapter.AuthorizeMasterDataKeyValueUrl(url, actionKey, out aspect);
        }

        public static bool AuthorizeMasterDataKeyValueUrl(string url, int actionKey, out IAspect aspect)
        {
            return Adapter.AuthorizeMasterDataKeyValueUrl(url, actionKey, out aspect);
        }

        public static bool AuthorizeWebPageUrl(string url, string type, out IAspect aspect)
        {
            return Adapter.AuthorizeWebPageUrl(url, type, out aspect);
        }

        public static KeyValue GetUserInfoById(int userId)
        {
            return Adapter.GetUserInfoById(userId);
        }
        public static bool IsAuthorize(int? role)
        {
            return Adapter.IsAuthorize(role);
        }

        public static void SetAndCheckModifyAndAccessRole(IAccessRole entity, dynamic newEntity, bool set = true)
        {
            Adapter.SetAndCheckModifyAndAccessRole(entity, newEntity, set);
        }

        public static void CheckParentNodeModifyAccessForAddingChildNode(IAccessRole entity, int parentId)
        {
            Adapter.CheckParentNodeModifyAccessForAddingChildNode(entity, parentId);
        }

        public static void CheckViewAccess(IAccessRole entity)
        {
            Adapter.CheckViewAccess(entity);
        }
        public static void CacheUserRoles(List<int> userGroupsId)
        {
            Adapter.CacheUserRoles(userGroupsId);
        }

        public static List<Group> GetUserRoles(List<int> userGroupsId)
        {
            return Adapter.GetUserRoles(userGroupsId);
        }

        public static async Task<List<string>> AuthorizeRefrencingDllAsync(List<int> usedDll)
        {
            return await Adapter.AuthorizeReferencingDllAsync(usedDll);
        }

    }
}

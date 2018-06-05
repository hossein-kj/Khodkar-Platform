using System;
using System.Collections.Generic;
using System.Linq;
using KS.Core.CacheProvider;
using KS.Core.CodeManager;
using KS.Core.GlobalVarioable;
using KS.Core.Localization;
using KS.Core.Utility;
using KS.Core.Data.Contexts.Base;
using KS.Core.Exceptions;
using KS.Core.Model.Core;
using KS.Core.Model.Security;
using System.Threading.Tasks;
using KS.Core.Model.Develop;


namespace KS.Core.Security.Adapters
{
    public abstract class BaseAuthorizeAdapter
    {
        protected readonly IDataBaseContextManager DataBaseContextManager;
        protected BaseAuthorizeAdapter(IDataBaseContextManager dataBaseContextManager)
        {
            DataBaseContextManager = dataBaseContextManager;
        }
        public virtual bool IsAuthorizeToViewSourceCodeOfWebPage(string webPageGuid)
        {
            return DataBaseContextManager
                .AuthorizeViewDebugScriptOfWebPage(webPageGuid, CurrentUserManager.Id, (int)EntityIdentity.Link
                , (int)ActionKey.ViewSourceCode,
                (int)EntityIdentity.Permission) != 0;
            //|| CurrentUserManager.RolesId.Any(r => r == (int)Roles.Admin);
        }

        public virtual bool IsAuthorizeToViewSourceCodeOfService(int serviceId)
        {
            return DataBaseContextManager
                .AuthorizeViewSourceCodeOfMasterDataKeyValues(serviceId, CurrentUserManager.Id, (int)EntityIdentity.Service
                , (int)ActionKey.ViewSourceCode,
                (int)EntityIdentity.Permission) != 0;
            //|| CurrentUserManager.RolesId.Any(r => r == (int)Roles.Admin);
        }

        public virtual bool IsAuthorizeToViewSourceCodeOfDotNetCode(int codeId)
        {
            return DataBaseContextManager
                       .AuthorizeViewSourceCodeOfMasterDataKeyValues(codeId, CurrentUserManager.Id,
                           (int) EntityIdentity.DotNetCode
                           , (int) ActionKey.ViewSourceCode,
                           (int) EntityIdentity.Permission) != 0;
            //|| CurrentUserManager.RolesId.Any(r => r == (int)Roles.Admin);
        }

        public virtual string AuthorizeDebugJavascriptPath(string path)
        {
            
            path = Helper.ProperPath(path).Replace("//", "/");
            path = path.IndexOf("?", StringComparison.OrdinalIgnoreCase) > -1 ? path.Remove(path.IndexOf("?", StringComparison.OrdinalIgnoreCase)) : path;

            if (path.IndexOf(Config.DebugIdSign, StringComparison.OrdinalIgnoreCase) > -1)
            {
                var debugId =
                    path.Substring(
                        path.IndexOf(Config.DebugIdSign, StringComparison.OrdinalIgnoreCase) + Config.DebugIdSign.Length +
                        1, 32);
                var realPath = path.Remove(path.IndexOf(Config.DebugIdSign, StringComparison.OrdinalIgnoreCase),
                    Config.DebugIdSign.Length + 34);

                var debugUsersCache = CacheManager.GetForCurrentUserByKey<List<DebugUser>>(CacheManager.GetDebugUserKey(CacheKey.DebugUser.ToString(),
               CurrentUserManager.Id, CurrentUserManager.Ip));

                var debug =
                    debugUsersCache.Value?.FirstOrDefault(du => du.Guid == debugId && du.Ip == CurrentUserManager.Ip);
                if (debug != null)
                {

                    if (realPath.IndexOf(Config.ScriptDebugPagesPath, StringComparison.OrdinalIgnoreCase) > -1 &&
                        debugUsersCache.IsCached)
                    {


                        realPath = realPath.Replace(Config.ScriptDebugPagesPath.ToLower(), "~/");
                        if (AuthorizeViewDebugScriptOfWebPage(realPath, debug.UserId))
                            //|| IsAuthorize(Convert.ToInt32(Roles.Admin)))
                        {
                            return realPath.Replace("~/", Config.ScriptDebugPagesPath);
                        }

                        return realPath.Replace("~/", Config.ScriptDistPagesPath);
                    }
                    else
                    {
                        realPath = realPath.Replace(Config.ScriptDebugPath.ToLower(), "~/");

                        if (AuthorizeViewDebugScriptOfCode(realPath, debug.UserId))
                            //|| IsAuthorize(Convert.ToInt32(Roles.Admin)))
                            return realPath.Replace("~/", Config.ScriptDebugPath.ToLower());

                        return realPath.Replace("~/", Config.ScriptDistPath);
                    }
                }
                path = realPath;
            }


            return path.IndexOf(Config.ScriptDebugPagesPath, StringComparison.OrdinalIgnoreCase) > -1 ? path.Replace(Config.ScriptDebugPagesPath.ToLower(), Config.ScriptDistPagesPath) : path.Replace(Config.ScriptDebugPath.ToLower(), Config.ScriptDistPath);
        }
        public virtual string AuthorizeActionOnPath(string path, ActionKey actionKey)
        {
            return AuthorizeActionOnPath(path, (int)actionKey);
        }

        public virtual string AuthorizeActionOnPath(string path, int actionKey)
        {
            path = Helper.ProperPath(path).Replace("//", "/");
            //if(IsAuthorize(Convert.ToInt32(Roles.Admin)))
            //    return path;

            var permissions =  DataBaseContextManager.GetPermissionOfPath((int)EntityIdentity.Permission, (int)EntityIdentity.Path, actionKey, path);
            if (permissions != null)
            {
                if (permissions.Any(permission => IsAuthorize(Convert.ToInt32(permission))))
                {
                    return path;
                }

            }
            throw new UnauthorizedAccessException(string.Format(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidAccessToPath), path));
        }

        public virtual bool AuthorizeActionOnEntityId(int entityId,int entityTypeId, int actionKey)
        {
            if (IsAuthorize(Convert.ToInt32(Roles.Admin)))
                return true;
            var permissions = DataBaseContextManager.GetPermissionOfEntityId((int)EntityIdentity.Permission, entityTypeId, actionKey, entityId);

            if (permissions != null)
            {
                return permissions.Any(permission => IsAuthorize(Convert.ToInt32(permission)));
            }
            return false;
        }

        public virtual bool AuthorizeActionOnEntityIdByVersion(int entityId, int entityTypeId, int entityVersion, int actionKey)
        {
            if (IsAuthorize(Convert.ToInt32(Roles.Admin)))
                return true;

            var permissions = DataBaseContextManager.GetPermissionOfEntityIdByVersion((int)EntityIdentity.Permission,
                entityTypeId, actionKey, entityId, entityVersion); 
            if (permissions != null )
            {
                return permissions.Any(permission => IsAuthorize(Convert.ToInt32(permission)));
            }
            return false;
        }


        public virtual bool AuthorizeMasterDataKeyValueUrl(string url, ActionKey actionKey, out IAspect aspect)
        {
            return AuthorizeMasterDataKeyValueUrl( url, (int) actionKey, out aspect);
        }

        public virtual bool AuthorizeMasterDataKeyValueUrl(string url, int actionKey,out IAspect aspect)
        {
            aspect = GetAspectForMasterDataKeyValueUrl(actionKey, url);
            if (aspect != null)
            {
                if (IsAuthorize(Convert.ToInt32(aspect.Permission)))
                    return true;
            }
            return false;
        }

        public virtual bool AuthorizeWebPageUrl(string url, string type, out IAspect aspect)
        {
            aspect = GetAspectForWebPage(url,type);
            return aspect != null && IsAuthorize(aspect.Permission);
        }

        //public virtual async Task<string> AuthorizeUrlAsync(string url, ActionKey actionKey)
        //{
        //    return await AuthorizeUrlAsync(url, (int)actionKey);
        //}

        //public static async Task<string> AuthorizeUrlAsync(string url, int actionKey)
        //{
        //    var permission = await GetPermissionOfUrlOrPathAsync(actionKey, url);
        //    if (permission != null)
        //    {
        //        if (IsAuthorize(Convert.ToInt32(permission)))
        //            return url;
        //    }
        //    throw new UnauthorizedAccessException(string.Format(Translation.ToAsErrorMessage(ExceptionKey.InvalidAccessToService),
        //        url));
        //}

        public virtual KeyValue GetUserInfoById(int userId)
        {
            return DataBaseContextManager.GetUserInfoById(userId);
        }
        protected virtual bool AuthorizeViewDebugScriptOfWebPage(string path,int userId)
        {
            return DataBaseContextManager
                .AuthorizeViewDebugScriptOfWebPage(path
                .Substring(path.IndexOf("~/", StringComparison.Ordinal) +2,32), userId,(int)EntityIdentity.Link
                ,(int)ActionKey.ViewSourceCode,
                (int)EntityIdentity.Permission) != 0;
            //|| CurrentUserManager.RolesId.Any(r => r == (int)Roles.Admin);
        }
        protected virtual bool AuthorizeViewDebugScriptOfCode(string path, int userId)
        {
            return DataBaseContextManager.AuthorizeViewDebugScriptOfCode((int)EntityIdentity.Bundle,
                (int)EntityIdentity.Script, path, userId, (int)ActionKey.ViewDebugSourceCode,
                (int)EntityIdentity.Permission) != 0; 
        }



        protected virtual IAspect GetAspectForWebPage(string url,string type)
        {
            var key = CacheManager.GetAspectKey(CacheKey.Aspect.ToString(), type, url);


            if (CacheManager.Get<IAspect>(key).IsCached)
                return CacheManager.Get<IAspect>(key).Value;

            var mobileUrl = url;

            url = (url + @"/").EndsWith(Config.MobileSign)
                ? url.Replace(Config.MobileSign.Substring(0, Config.MobileSign.Length - 1), "").Replace("//", "/")
                : url.Replace(Config.MobileSign, Helper.RootUrl).Replace("//", "/");

            url = url.EndsWith("/") ? url.Substring(0, url.Length - 1) : url;

            var aspect = DataBaseContextManager.GetAspectForWebPage(url, mobileUrl, type) ?? new Aspect
                         {
                             EnableCache = false,
                             EnableLog = false,
                             HasMobileVersion = false,
                             Permission = 0,
                             Status = 1,
                             IsNull = true
                         };
            CacheManager.Store(key, aspect, slidingExpiration: TimeSpan.FromMinutes(Config.AspectCacheSlidingExpirationTimeInMinutes));
            return aspect;
        }

        protected virtual IAspect GetAspectForMasterDataKeyValueUrl(int actionKey, string url)
        {
            var key = CacheManager.GetAspectKey(CacheKey.Aspect.ToString(), actionKey.ToString(), url);
            if (CacheManager.Get<IAspect>(key).IsCached)
                return CacheManager.Get<IAspect>(key).Value;
            var aspect = DataBaseContextManager.GetAspectForMasterDataKeyValueUrl(actionKey, url);
            if (aspect != null)
                CacheManager.Store(key,aspect,slidingExpiration:TimeSpan.FromMinutes(Config.AspectCacheSlidingExpirationTimeInMinutes));
            return aspect;
        }

        //protected virtual async Task<string> GetPermissionOfUrlOrPathAsync(int actionKey, string urlOrPath)
        //{
        //    return await ContextManager.GetPermissionOfUrlOrPathAsync((int)EntityIdentity.Permission,
        //        (int)actionKey, urlOrPath);
        //}

        public virtual bool IsAuthorize(int? role)
        {
            if (role != null)
            {
                return role == (int)Roles.Public || CurrentUserManager.RolesId.Any(r => r == role);
                //|| r== (int)Roles.Admin);
            }
            return true;
        }

        public virtual void CheckParentNodeModifyAccessForAddingChildNode(IAccessRole entity,int parentId)
        {
            try
            {
               SetAndCheckModifyAndAccessRole(entity, null, false);
            }
            catch (UnauthorizedAccessException)
            {

                throw new KhodkarInvalidException(
                    string.Format(
                        LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidAccessForAddingChildToParenRecord),
                        parentId));
            }
        }

        public virtual void SetAndCheckModifyAndAccessRole(IAccessRole entity, dynamic newEntity, bool set = true)
        {
            if (entity == null) return;

            int? accessRoleId = newEntity==null ?null: newEntity.AccessRoleId;
            int ? modifyRoleId = newEntity == null ? null : newEntity.ModifyRoleId;
            int ? viewRoleId = newEntity == null ? null : newEntity.ViewRoleId;

            if ((accessRoleId == null || modifyRoleId == null || viewRoleId == null) && set)
            {
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.RoleIsNull));
            }
           
            if (entity.ModifyRoleId != null)
                if (!IsAuthorize((int)entity.ModifyRoleId))
                    throw new UnauthorizedAccessException(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidGrant));
            if (entity.AccessRoleId != null)
            {
                if (!IsAuthorize((int)entity.AccessRoleId) || !set) return;
                entity.ViewRoleId = viewRoleId;
                entity.ModifyRoleId = modifyRoleId;
                entity.AccessRoleId = accessRoleId;
            }
            else
            {
                if (!set) return;
                entity.ViewRoleId = viewRoleId;
                entity.ModifyRoleId = modifyRoleId;
                entity.AccessRoleId = accessRoleId;
            }
        }

        public virtual void CheckViewAccess(IAccessRole entity)
        {
            if (entity?.ViewRoleId != null)
                if (!IsAuthorize((int)entity.ViewRoleId))
                    throw new UnauthorizedAccessException(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidGrant));
        }

        public virtual void CacheUserRoles(List<int> userGroupsId)
        {
            GetUserRoles(userGroupsId);
        }

        public virtual List<Group> GetUserRoles(List<int> userGroupsId)
        {
            var notCachedGroup = new List<int>();
            var groups = new List<Group>();
            foreach (var groupId in userGroupsId)
            {
                var key = CacheManager.GetGroupKey(CacheKey.Aspect.ToString(), groupId);
                var cachedGroup = CacheManager.Get<Group>(key);
                if (!cachedGroup.IsCached)
                {
                    notCachedGroup.Add(groupId);
                }
                else
                {
                    groups.Add(cachedGroup.Value);
                }
            }

            if (notCachedGroup.Count > 0)
            {
               

                var groupRoles = DataBaseContextManager.GetRolesIdByGroupsId(notCachedGroup);

                if (groupRoles.Count > 0)
                {
                    foreach (var keyValue in groupRoles)
                    {
                        if (!groups.Exists(gr => gr.Id == Convert.ToInt32(keyValue.Key)))
                            groups.Add(new Group()
                            {
                                Id = Convert.ToInt32(keyValue.Key),
                                RolesId = new List<int>() { Convert.ToInt32(keyValue.Value) }
                            });
                        else
                        {
                            groups.Find(gr => gr.Id == Convert.ToInt32(keyValue.Key)).RolesId.Add(Convert.ToInt32(keyValue.Value));
                        }
                    }
                }

                foreach (var @group in groups)
                {
                    CacheManager.Store(CacheManager.GetGroupKey(CacheKey.Aspect.ToString(), @group.Id),
                        @group, slidingExpiration: TimeSpan.FromMinutes(Config.GroupCacheSlidingExpirationTimeInMinutes));
                }
            }
            return groups;
        }

        public async Task<List<string>> AuthorizeReferencingDllAsync(List<int> usedDll)
        {
            //var nameSpaces = new List<string>();
            var notAccessDlls = new List<string>();
            //foreach (var nameSpace in usedNameSpaces)
            //{
            //    //nameSpaces.Add(nameSpace.Key+"."+nameSpace.Value);
            //    //if (nameSpace.Value.IndexOf(".", StringComparison.Ordinal) > -1)
            //    //{
                    
            //        BuildNameSpace(nameSpaces, nameSpace.Key, nameSpace.Value);
            //    //}
            //}
           
            var dllsPermission = await DataBaseContextManager.GetListOfDllReferencingPermissionFromeListOfDllIdAsync((int)EntityIdentity.Permission,(int)ActionKey.ReferenceDll,
                usedDll);

            foreach (var dllPermission in dllsPermission)
            {
                if (!dllsPermission.Any(dll => 
                 CurrentUserManager.RolesId.Contains(Convert.ToInt32(dll.Value))))
                    notAccessDlls.Add(dllPermission.Key);
            }
            //foreach (var usedNameSpace in usedNameSpaces)
            //{
            //    var hierarchicalNameSpaces = new List<string>();
            //    BuildNameSpace(hierarchicalNameSpaces, usedNameSpace.Key, usedNameSpace.Value);

            //    var isAuthorized = true;

            //    foreach (var ns in hierarchicalNameSpaces.OrderBy(hns=>hns.Length))
            //    {
            //        if (dllsPermission.Any(nsp => nsp.Key == ns))
            //        {
            //            if (!dllsPermission.Any(nsp => nsp.Key == ns &&
            //            CurrentUserManager.RolesId.Contains(Convert.ToInt32(nsp.Value))))
            //                isAuthorized = false;
            //            else
            //                isAuthorized = true;                       
            //        }
            //    }

            //    if (!isAuthorized)
            //        notAccessNameSpaces.Add(usedNameSpace.Key + "." + usedNameSpace.Value);

            //}

            return notAccessDlls;
        }

        private void BuildNameSpace(List<string> nameSpaces, string dll, string nameSpace)
        {
            while (true)
            {
                if (!nameSpaces.Exists(ns => ns == dll + "." + nameSpace))
                    nameSpaces.Add(dll + "." + nameSpace);
                if (nameSpace.IndexOf(".", StringComparison.Ordinal) <= -1) return;
                nameSpace = nameSpace.Remove(nameSpace.LastIndexOf(".", StringComparison.Ordinal));
            }
        }
    }
}

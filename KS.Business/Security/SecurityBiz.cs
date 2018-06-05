using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using EntityFramework.Extensions;
using KS.Business.Security.Base;
using KS.Core.CacheProvider;
using KS.Core.Exceptions;
using KS.Core.GlobalVarioable;
using KS.Core.Localization;
using KS.Core.Security;
using KS.Model.Security;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;
using KS.DataAccess.Contexts.Base;
using KS.Model.ContentManagement;
using Newtonsoft.Json;

namespace KS.Business.Security
{
    public sealed class SecurityBiz : BaseBiz, ISecurityBiz
    {
        private readonly ISecurityContext _securityContext;
        private readonly IContentManagementContext _contentManagementContext;
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationRoleManager _roleManager;

        public SecurityBiz(ISecurityContext securityContext,
            IContentManagementContext contentManagementContex,
            ApplicationUserManager userManager,
            ApplicationRoleManager roleManager)
        {
            _securityContext = securityContext;
            _contentManagementContext = contentManagementContex;
            _userManager = userManager;
            _roleManager = roleManager;

        }


        public async Task<ApplicationUser> GetUserAsync(int userId)
        {
            return await _securityContext.Users.Where(us => us.Id == userId).FirstOrDefaultAsync();
        }

        public async Task<ApplicationRole> SaveRole(JObject role)
        {

            dynamic roleDto = role;
            ApplicationRole applicationRole;
            bool isNew = roleDto.IsNew;
            if (!isNew)
            {
                applicationRole = new ApplicationRole()
                {
                    Id = roleDto.Id
                };
                applicationRole = await _roleManager.FindByIdAsync(applicationRole.Id);
                if (applicationRole == null)
                    throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.RoleNotFound));
            }
            else
            {
                applicationRole = new ApplicationRole();


            }

            applicationRole.Name = roleDto.Name;
            applicationRole.IsLeaf = roleDto.IsLeaf;
            applicationRole.Description = roleDto.Description;
            try
            {
                applicationRole.Order = roleDto.Order;
            }
            catch (Exception)
            {

                applicationRole.Order = 0;
            }

            applicationRole.Status = roleDto.Status;

            try
            {
                int parentId = roleDto.ParentId;
                if (applicationRole.ParentId != parentId)
                {

                    var parentCode = await _roleManager.FindByIdAsync(parentId);
                    if (parentCode == null)
                        throw new KhodkarInvalidException(
                            LanguageManager.ToAsErrorMessage(ExceptionKey.ParentRecordNotFound));
                    AuthorizeManager.CheckParentNodeModifyAccessForAddingChildNode(parentCode, parentCode.Id);
                }
                applicationRole.ParentId = parentId;
            }
            catch (KhodkarInvalidException)
            {

                throw;
            }
            catch (Exception)
            {

                applicationRole.ParentId = null;
            }

            AuthorizeManager.SetAndCheckModifyAndAccessRole(applicationRole, roleDto);

            var roleresult = isNew
                ? await _roleManager.CreateAsync(applicationRole)
                : await _roleManager.UpdateAsync(applicationRole);
            if (!roleresult.Succeeded)
            {
                throw new KhodkarInvalidException(roleresult.Errors.First());

            }
            return applicationRole;
        }

        public async Task<bool> DeleteRole(JObject data)
        {
            dynamic roleData = data;
            int id;

            try
            {
                id = roleData.Id;
            }
            catch (Exception)
            {

                throw new KhodkarInvalidException(
                    string.Format(LanguageManager.ToAsErrorMessage(ExceptionKey.FieldMustBeNumeric),
                        "Role Id"));
            }
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.RoleNotFound));

            AuthorizeManager.SetAndCheckModifyAndAccessRole(role, null, false);


            var useCount = await _securityContext.ApplicationGroupRoles.Where(gr => gr.RoleId == role.Id)
                .CountAsync();

            if (useCount > 0)
                throw new KhodkarInvalidException(
                    string.Format(LanguageManager.ToAsErrorMessage(ExceptionKey.InUseItem), role.Name));

            await _roleManager.DeleteAsync(role);

            return true;
        }

        public async Task<ApplicationLocalRole> SaveRoleTranslate(JObject data)
        {
            dynamic localRoleDto = data;

            var localRole = new ApplicationLocalRole()
            {
                Id = localRoleDto.Id,
                RowVersion = localRoleDto.RowVersion
            };


            if (localRole.Id > 0)
            {
                localRole =
                    await
                        _securityContext.ApplicationLocalRoles.Include(md => md.Role)
                            .SingleOrDefaultAsync(md => md.Id == localRole.Id);
                if (localRole == null)
                    throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.RoleNotFound));
            }
            else
            {
                _securityContext.ApplicationLocalRoles.Add(localRole);
            }

            localRole.RoleId = localRoleDto.ItemId;
            localRole.Name = localRoleDto.Name;
            localRole.Description = localRoleDto.Description;
            localRole.Language = localRoleDto.Language;

            AuthorizeManager.SetAndCheckModifyAndAccessRole(localRole.Role, localRoleDto, false);


            localRole.Status = localRoleDto.Status;
            await _securityContext.SaveChangesAsync();
            return localRole;
        }

        public async Task<ApplicationGroup> SaveGroup(JObject group)
        {

            dynamic groupDto = group;
            ApplicationGroup applicationGroup;
            bool isNew = groupDto.IsNew;
            JArray sremovedListArray = groupDto.RemovedList;
            var removedList = sremovedListArray.ToObject<List<int>>();
            JArray addedListArray = groupDto.AddedList;
            var addedList = addedListArray.ToObject<List<int>>();

            if (!isNew)
            {
                applicationGroup = new ApplicationGroup()
                {
                    Id = groupDto.Id
                };
                applicationGroup = await _securityContext.Groups.FindAsync(applicationGroup.Id);
                if (applicationGroup == null)
                    throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.GroupNotFound));
            }
            else
            {
                applicationGroup = new ApplicationGroup();
                _securityContext.Groups.Add(applicationGroup);

            }



            applicationGroup.Name = groupDto.Name;
            applicationGroup.IsLeaf = groupDto.IsLeaf;
            applicationGroup.Description = groupDto.Description;
            try
            {
                applicationGroup.Order = groupDto.Order;
            }
            catch (Exception)
            {

                applicationGroup.Order = 0;
            }

            applicationGroup.Status = groupDto.Status;

            try
            {
                int parentId = groupDto.ParentId;
                if (applicationGroup.ParentId != parentId)
                {

                    var parentCode = await _securityContext.Groups.FindAsync(parentId);
                    if (parentCode == null)
                        throw new KhodkarInvalidException(
                            LanguageManager.ToAsErrorMessage(ExceptionKey.ParentRecordNotFound));
                    AuthorizeManager.CheckParentNodeModifyAccessForAddingChildNode(parentCode, parentCode.Id);
                }
                applicationGroup.ParentId = parentId;
            }
            catch (KhodkarInvalidException)
            {

                throw;
            }
            catch (Exception)
            {

                applicationGroup.ParentId = null;
            }

            AuthorizeManager.SetAndCheckModifyAndAccessRole(applicationGroup, groupDto);

            foreach (var item in addedList)
            {

                var role = new ApplicationGroupRole()
                {
                    GroupId = applicationGroup.Id,
                    RoleId = item
                };
                _securityContext.ApplicationGroupRoles.Add(role);
            }

            if (removedList.Count > 0)
            {
                _securityContext.ApplicationGroupRoles.Where(
                    eg => removedList.Contains(eg.RoleId) && eg.GroupId == applicationGroup.Id).Delete();
            }

            await _securityContext.SaveChangesAsync();

            
            CacheManager.Remove(CacheManager.GetGroupKey(CacheKey.Aspect.ToString(), applicationGroup.Id));

            return applicationGroup;
        }

        public async Task<bool> DeleteGroup(JObject data)
        {
            dynamic groupData = data;
            int id;

            try
            {
                id = groupData.Id;
            }
            catch (Exception)
            {

                throw new KhodkarInvalidException(
                    string.Format(LanguageManager.ToAsErrorMessage(ExceptionKey.FieldMustBeNumeric),
                        "Group Id"));
            }
            var group = await _securityContext.Groups.FindAsync(id);

            if (group == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.GroupNotFound));

            AuthorizeManager.SetAndCheckModifyAndAccessRole(group, null, false);


            var useCount = await _securityContext.ApplicationUserGroups.Where(gr => gr.GroupId == group.Id)
                .CountAsync();

            if (useCount > 0)
                throw new KhodkarInvalidException(
                    string.Format(LanguageManager.ToAsErrorMessage(ExceptionKey.InUseItem), group.Name));

            _securityContext.Groups.Remove(group);
            await _securityContext.SaveChangesAsync();
            
            CacheManager.Remove(CacheManager.GetGroupKey(CacheKey.Aspect.ToString(), group.Id));
            return true;
        }

        public async Task<ApplicationLocalGroup> SaveGroupTranslate(JObject data)
        {
            dynamic localGroupDto = data;

            var localGroup = new ApplicationLocalGroup()
            {
                Id = localGroupDto.Id,
                RowVersion = localGroupDto.RowVersion
            };


            if (localGroup.Id > 0)
            {
                localGroup =
                    await
                        _securityContext.ApplicationLocalGroups.Include(md => md.Group)
                            .SingleOrDefaultAsync(md => md.Id == localGroup.Id);
                if (localGroup == null)
                    throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.GroupNotFound));
            }
            else
            {
                _securityContext.ApplicationLocalGroups.Add(localGroup);
            }

            localGroup.GroupId = localGroupDto.ItemId;
            localGroup.Name = localGroupDto.Name;
            localGroup.Description = localGroupDto.Description;
            localGroup.Language = localGroupDto.Language;

            AuthorizeManager.SetAndCheckModifyAndAccessRole(localGroup.Group, localGroupDto, false);


            localGroup.Status = localGroupDto.Status;
            await _securityContext.SaveChangesAsync();
            return localGroup;
        }

        public async Task<JObject> SaveUser(JObject user)
        {
            dynamic userDto = user;
            bool isNew = userDto.IsNew;
            return isNew ? await RegisterUserAsync(user) : await UpdateUserAsync(user);
        }
        public async Task<bool> DeleteUser(JObject user)
        {
            dynamic userDto = user;
            int id;

            try
            {
                id = userDto.Id;
            }
            catch (Exception)
            {

                throw new KhodkarInvalidException(
                    string.Format(LanguageManager.ToAsErrorMessage(ExceptionKey.FieldMustBeNumeric),
                        "User Id"));
            }
            var appUser = await _userManager.FindByIdAsync(id);

            if (appUser == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.UserNotFound));

            AuthorizeManager.SetAndCheckModifyAndAccessRole(appUser, null, false);

            var userProfileId = appUser.Id;

            var result=await _userManager.DeleteAsync(appUser);

            if (result.Succeeded)
            {
                var userProfile = await _contentManagementContext.Users.FirstOrDefaultAsync(up => up.Id == userProfileId);

                if (userProfile != null)
                {
                    _contentManagementContext.Users.Remove(userProfile);

                    await _contentManagementContext.SaveChangesAsync();
                }
             
                return true;
            }

            throw new KhodkarInvalidException(result.Errors.First());

        }
        public async Task<bool> ChangePassword(JObject changeData)
        {
            dynamic changeDto = changeData;

            string oldPassword = changeDto.OldPassword;
            string newPassword = changeDto.NewPassword;

            var result = await _userManager.ChangePasswordAsync(CurrentUserManager.Id, oldPassword, newPassword);

            if (result.Succeeded)
            {
 
                return true;
            }

            throw new KhodkarInvalidException(result.Errors.First());

        }
        private async Task<JObject> RegisterUserAsync(JObject user)
        {

            dynamic userDto = user;
            string userName = userDto.UserName;
            string password = userDto.Password;

            if ((await _userManager.FindByNameAsync(userName)) != null)
            {
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.DuplicateName));
            }

            var appUser = new ApplicationUser

            {

                UserName = userDto.UserName,
                //UserProfileId = 0,
                Email = userDto.Email,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                BirthDate=DateTime.UtcNow,
                Status = userDto.Status


            };

            AuthorizeManager.SetAndCheckModifyAndAccessRole(appUser, userDto);

            IdentityResult createResult = await _userManager

                .CreateAsync(appUser, password);



            //Add User to the selected Groups 

            if (createResult.Succeeded)
            {
                var profile = new UserProfile()
                {
                    Id = appUser.Id,
                    AliasName = userDto.AliasName,
                    Language = Config.DefaultsLanguage,
                    ViewRoleId = userDto.ViewRoleId,
                    ModifyRoleId = userDto.ModifyRoleId,
                    AccessRoleId = userDto.AccessRoleId
                };
                try
                {
                    _contentManagementContext.Users.Add(profile);
                    await _contentManagementContext.SaveChangesAsync();



                    userDto.Id = appUser.Id;
                    await UpdateUserGroups(userDto);

                    appUser = await _securityContext.Users.Include(us => us.Groups).FirstOrDefaultAsync(us => us.Id == appUser.Id);
                    if (appUser == null)
                        throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.UserNotFound));

                    return JObject.Parse(JsonConvert.SerializeObject
                   (new
                   {
                       appUser.Id,
                       appUser.UserName,
                       appUser.FirstName,
                       appUser.LastName,
                       appUser.Status,
                       appUser.ViewRoleId,
                       appUser.ModifyRoleId,
                       appUser.AccessRoleId,
                       UserProfileId=appUser.Id,
                       appUser.Email,
                       appUser.Groups,
                       profile.AliasName
                   }, Formatting.None,
                       new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
                }
                catch (Exception)
                {
                    try
                    {
                        await _userManager.DeleteAsync(appUser);
                        _contentManagementContext.Users.Remove(profile);
                        await _contentManagementContext.SaveChangesAsync();
                    }
                    catch (Exception)
                    {

                        
                    }
                    
                    throw;
                }
            }
            throw new KhodkarInvalidException(createResult.Errors.First());


        }
        private async Task<JObject> UpdateUserAsync(JObject user)
        {

            dynamic userDto = user;
            int userId = userDto.Id;
            string password = userDto.Password;

            var appUser = await _userManager.FindByIdAsync(userId);

            if (appUser == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.UserNotFound));


            var userProfile = await _contentManagementContext.Users.FirstOrDefaultAsync(up => up.Id == appUser.Id);

            if (userProfile == null)
            {
                userProfile = new UserProfile()
                {
                    Id = appUser.Id
                };

                _contentManagementContext.Users.Add(userProfile);
            }
               // throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.UserProfileNotFound));



            userProfile.AliasName = userDto.AliasName;
     

            AuthorizeManager.SetAndCheckModifyAndAccessRole(appUser, userDto);


            await _contentManagementContext.SaveChangesAsync();



            appUser.Email = userDto.Email;
            appUser.FirstName = userDto.FirstName;
            appUser.LastName = userDto.LastName;
            appUser.Status = userDto.Status;





            IdentityResult updateResult = await _userManager

                    .UpdateAsync(appUser);




                //Add User to the selected Groups 

                if (updateResult.Succeeded)
                {

                    //todo:check Authorize
                    await UpdateUserGroups(user);

                    bool isChangePass = userDto.IsChangePass;
                    if(isChangePass)
                    {
                        var token = await _userManager.GeneratePasswordResetTokenAsync(userId);
                        var changePassResult = await _userManager.ResetPasswordAsync(userId, token, password);
                        if (!changePassResult.Succeeded)
                            throw new KhodkarInvalidException(changePassResult.Errors.First());
                    }

                appUser = await _securityContext.Users.Include(us => us.Groups).FirstOrDefaultAsync(us => us.Id == userId);
                if (appUser == null)
                    throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.UserNotFound));
                return JObject.Parse(JsonConvert.SerializeObject
                   (new
                   {
                       appUser.Id,
                       appUser.UserName,
                       appUser.FirstName,
                       appUser.LastName,
                       appUser.Status,
                       appUser.ViewRoleId,
                       appUser.ModifyRoleId,
                       appUser.AccessRoleId,
                       UserProfileId= appUser.Id,
                       appUser.Email,
                       appUser.Groups,
                       userProfile.AliasName
                   }, Formatting.None,
                       new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
               
                }
                throw new KhodkarInvalidException(updateResult.Errors.First());


        }

        private async Task<int> UpdateUserGroups(JObject user)
        {
            dynamic userDto = user;
            int userId = userDto.Id;
            JArray sremovedListArray = userDto.RemovedList;
            var removedList = sremovedListArray.ToObject<List<int>>();
            JArray addedListArray = userDto.AddedList;
            var addedList = addedListArray.ToObject<List<int>>();

            foreach (var item in addedList)
            {

                var group = new ApplicationUserGroup()
                {
                    UserId = userId,
                    GroupId = item
                };
                _securityContext.ApplicationUserGroups.Add(group);
            }

            if (removedList.Count > 0)
            {
                _securityContext.ApplicationUserGroups.Where(
                    ug => removedList.Contains(ug.GroupId) && ug.UserId == userId).Delete();
            }

            return await _securityContext.SaveChangesAsync();
        }

        public async Task<JObject> GetUsersByPagination(int groupId, string orderBy, int skip, int take)
        {
            var count = await _securityContext.Users.Where(us => us.Groups.Any(gr => gr.GroupId == groupId)).CountAsync();
            var users = (await _securityContext.Users
                .Include(us=>us.Groups).Where(us => us.Groups.Any(gr => gr.GroupId == groupId) || groupId==0).OrderBy(orderBy)
                .Skip(skip)
                .Take(take).ToListAsync()).Select(us => new
            {
                us.Id,
                us.UserName,
                us.FirstName,
                us.LastName,
                us.Status,
                us.ViewRoleId,
                us.ModifyRoleId,
                us.AccessRoleId,
                UserProfileId= us.Id,
                us.Email,
                us.Groups,
                AliasName=""
                }).ToList();

            var profileIds = users.Select(us=>us.UserProfileId).ToList();

            var userAliasNames = await _contentManagementContext.Users.Where(us => profileIds.Contains(us.Id))
                .Select(us =>new {us.Id, us.AliasName})
                .ToListAsync();

            var finalUsers=users.Join(userAliasNames, us => us.UserProfileId, ua => ua.Id, (us, ua) => new
            {
                us.Id,
                us.UserName,
                us.FirstName,
                us.LastName,
                us.Status,
                us.ViewRoleId,
                us.ModifyRoleId,
                us.AccessRoleId,
                ua.AliasName,
                us.Email,
                us.Groups
            }).ToList();


            return JObject.Parse(JsonConvert.SerializeObject
                (new
                {
                    rows = finalUsers,
                    total = count
                }, Formatting.None,
                new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
        }

        //public IList<ApplicationTree> GetTrees(IEnumerable<object> roles)
        //{
        //    var baseTree = _securityContext.ApplicationTrees.Include(t => t.Role).AsNoTracking().ToList();
        //    var newTree = new List<ApplicationTree>();
        //    foreach (var node in baseTree)
        //    {
        //        if (node.IsLeaf)
        //        {
        //            if (roles != null)
        //            {
        //                if (roles.Contains(node.Role.Name))
        //                {
        //                    BuildTree<ApplicationTree, ApplicationTree>(node, newTree, baseTree);
        //                }
        //            }
        //            else
        //            {
        //                BuildTree<ApplicationTree, ApplicationTree>(node, newTree, baseTree);
        //            }
        //        }

        //        if (node.ParentId == null)
        //            if (!newTree.Exists(t => t.Id == node.Id))
        //                newTree.Add(node);
        //    }
        //    return newTree;
        //}

        //public async Task<IList<ApplicationTree>> GetTreesAsync(IEnumerable<object> roles)
        //{
        //    var baseTree = await _securityContext.ApplicationTrees.Include(t => t.Role).AsNoTracking().ToListAsync();
        //    var newTree = new List<ApplicationTree>();
        //    foreach (var node in baseTree)
        //    {
        //        if (node.IsLeaf)
        //        {
        //            if (roles != null)
        //            {
        //                if (roles.Contains(node.Role.Name))
        //                {
        //                    BuildTree<ApplicationTree, ApplicationTree>(node, newTree, baseTree);
        //                }
        //            }
        //            else
        //            {
        //                BuildTree<ApplicationTree, ApplicationTree>(node, newTree, baseTree);
        //            }
        //        }

        //        if (node.ParentId == null)
        //            if (!newTree.Exists(t => t.Id == node.Id))
        //                newTree.Add(node);
        //    }
        //    return newTree;
        //}

        //public async Task<IList<ApplicationTree>> GetAccessAsync(IEnumerable<Roles> roles)
        //{

        //    //var tr = await _securityContext.Database.SqlQuery<ApplicationTree>(sql, new SqlParameter("Id", 72)).ToListAsync();

        //    //var baseTree = await _securityContext.ApplicationTrees.Include(t => t.Role).AsNoTracking().ToListAsync();
        //    var baseTree = await
        //            _securityContext.GetTreeByAllOffspringAsync<ApplicationTree>(ApplicationTree.GetSelfEntityTableName(),
        //                "Id=66");
        //    var newTree = new List<ApplicationTree>();
        //    foreach (var node in baseTree)
        //    {
        //        if (node.IsLeaf)
        //        {
        //            if (roles != null)
        //            {
        //                if (roles.Any(r => (int)r == node.RoleId))
        //                {
        //                    BuildTree<ApplicationTree, ApplicationTree>(node, newTree, baseTree);
        //                }
        //            }
        //            else
        //            {
        //                BuildTree<ApplicationTree, ApplicationTree>(node, newTree, baseTree);
        //            }
        //        }

        //        if (node.ParentId == null && node.Text == "مدیریت")
        //            if (!newTree.Exists(t => t.Id == node.Id))
        //            {
        //                node.Text = "سید حسین کیائی جمالی";
        //                newTree.Add(node);
        //            }
        //    }
        //    return newTree;
        //}

        //public async Task<IList<ApplicationTree>> GetMenusAsync(IEnumerable<Roles> accessRoles = null, IEnumerable<Roles> denyRoles = null)
        //{
        //    //var ts = _securityContext.ApplicationTrees
        //    // .Include(x => x.Offspring.Select(y => y.Offspring))
        //    // .SingleOrDefault(x => x.Id == 72);
        //    //ts = ts;
        //    var baseTree = await _securityContext.ApplicationTrees.AsNoTracking().ToListAsync();
        //    var newTree = new List<ApplicationTree>();
        //    foreach (var node in baseTree)
        //    {
        //        if (node.IsLeaf)
        //        {
        //            if (node.RoleId != (int)Roles.NotShowInMenu)
        //            {
        //                if (accessRoles == null && denyRoles == null)
        //                    BuildTree<ApplicationTree, ApplicationTree>(node, newTree, baseTree);
        //                else if (accessRoles != null && denyRoles != null)
        //                {
        //                    if (accessRoles.Any(r => (int)r == node.RoleId) && !denyRoles.Any(r => (int)r == node.RoleId))
        //                    {
        //                        BuildTree<ApplicationTree, ApplicationTree>(node, newTree, baseTree);
        //                    }
        //                }
        //                else if (denyRoles == null)
        //                {
        //                    if (accessRoles.Any(r => (int)r == node.RoleId))
        //                    {
        //                        BuildTree<ApplicationTree, ApplicationTree>(node, newTree, baseTree);
        //                    }
        //                }
        //                else if (accessRoles == null)
        //                {
        //                    if (!denyRoles.Any(r => (int)r == node.RoleId))
        //                    {
        //                        BuildTree<ApplicationTree, ApplicationTree>(node, newTree, baseTree);
        //                    }
        //                }
        //            }
        //        }

        //        if (node.ParentId == null)
        //        {

        //            if (accessRoles == null && denyRoles == null)
        //            {
        //                if (!newTree.Exists(t => t.Id == node.Id))
        //                    newTree.Add(node);
        //            }
        //            else if (accessRoles != null && denyRoles != null)
        //            {
        //                if (accessRoles.Any(r => (int)r == node.RoleId) && !denyRoles.Any(r => (int)r == node.RoleId))
        //                {
        //                    if (!newTree.Exists(t => t.Id == node.Id))
        //                        newTree.Add(node);
        //                }
        //            }
        //            else if (denyRoles == null)
        //            {
        //                if (accessRoles.Any(r => (int)r == node.RoleId))
        //                {
        //                    if (!newTree.Exists(t => t.Id == node.Id))
        //                        newTree.Add(node);
        //                }
        //            }
        //            else if (accessRoles == null)
        //            {
        //                if (!denyRoles.Any(r => (int)r == node.RoleId))
        //                {
        //                    if (!newTree.Exists(t => t.Id == node.Id))
        //                        newTree.Add(node);
        //                }
        //            }




        //        }
        //    }
        //    return RemoveEmptyParentNode<ApplicationTree, ApplicationTree>(newTree);
        //    //foreach (var menu in newTree.Where(menu => menu.IsLeaf == false).Where(menu => !newTree.Exists(t => t.ParentId == menu.Id)))
        //    //{
        //    //    menu.Status = -1;
        //    //}
        //    //return newTree.Where(t => t.Status != -1).ToList();
        //}

        //public async Task<IList<ApplicationTree>> GetAllMenusAsync(IEnumerable<Roles> roles = null)
        //{
        //    //var ts = _securityContext.ApplicationTrees
        //    // .Include(x => x.Offspring.Select(y => y.Offspring))
        //    // .SingleOrDefault(x => x.Id == 72);
        //    //ts = ts;
        //    var baseTree = await _securityContext.ApplicationTrees.AsNoTracking().ToListAsync();
        //    var newTree = new List<ApplicationTree>();
        //    foreach (var node in baseTree)
        //    {
        //        if (node.IsLeaf)
        //        {
        //            if (roles == null)
        //                BuildTree<ApplicationTree, ApplicationTree>(node, newTree, baseTree);

        //            else if (roles.Any(r => (int)r == node.RoleId))
        //            {
        //                BuildTree<ApplicationTree, ApplicationTree>(node, newTree, baseTree);
        //            }
        //        }

        //        if (node.ParentId == null)
        //            if (!newTree.Exists(t => t.Id == node.Id))
        //                newTree.Add(node);
        //    }
        //    return RemoveEmptyParentNode<ApplicationTree, ApplicationTree>(newTree);
        //    //foreach (var menu in newTree.Where(menu => menu.IsLeaf == false).Where(menu => !newTree.Exists(t => t.ParentId == menu.Id)))
        //    //{
        //    //    menu.Status = -1;
        //    //}
        //    //return newTree.Where(t => t.Status != -1).ToList();
        //}

        //public async Task<IEnumerable<dynamic>> GetRolesAsync()
        //{

        //    return await _securityContext.Roles.Select(r => new { r.Name, r.Id }).AsNoTracking().ToListAsync();
        //}

        //public async Task<IList<ApplicationRole>> GetRolesTreeAsync()
        //{

        //    //var lang = _contentManagementBiz.GetDefaultsLanguageAndCulture().Language;
        //    var baseTreeSource = await _securityContext.Roles.Select(rl => new
        //    {
        //        rl,
        //        LocalValues = rl.LocalRoles.Where(lv => lv.Language == Setting.Language)
        //    }).ToListAsync();
        //    var baseTree = baseTreeSource.Select(newType => newType.rl).ToList();
        //    //var baseTree = await _contentManagementContext.Services.AsNoTracking().ToListAsync();
        //    //var newTree = new List<Service>();
        //    //foreach (var node in baseTree)
        //    //{
        //    //    if (node.IsLeaf)
        //    //    {
        //    //        if (roleId == null)
        //    //            BuildTree<Service, MasterDataKeyValue>(node, newTree, baseTree);
        //    //        else if (node.ViewRoleId == roleId)
        //    //        {
        //    //            BuildTree<Service, MasterDataKeyValue>(node, newTree, baseTree);
        //    //        }
        //    //    }

        //    //    if (node.ParentId == null)
        //    //        if (!newTree.Exists(t => t.Id == node.Id))
        //    //            newTree.Add(node);
        //    //}
        //    //return RemoveEmptyParentNode<Service, MasterDataKeyValue>(newTree);


        //    //var baseTree = await _securityContext.GetTreeByAllOffspringAsync<ApplicationRole>(ApplicationRole.GetSelfEntityTableName(),
        //    //          "Id=" + (int)Roles.Roles);
        //    var newTree = new List<ApplicationRole>();
        //    foreach (var node in baseTree)
        //    {
        //        if (node.IsLeaf)
        //        {

        //            BuildTree<ApplicationRole, ApplicationRole>(node, newTree, baseTree);
        //        }

        //        if (node.ParentId == null)
        //            if (!newTree.Exists(t => t.Id == node.Id))
        //            {
        //                newTree.Add(node);
        //            }
        //    }
        //    return newTree;
        //}
        //public async Task<ApplicationTree> UpdateTreeAsync(ApplicationTree node)
        //{

        //    var menu = await _securityContext.ApplicationTrees.Where(t => t.Id == node.Id).FirstOrDefaultAsync();
        //    //var role = await _dbContext.Roles.Where(r => r.Name == node.Role.Name).FirstOrDefaultAsync();
        //    //if (role == null)
        //    //{
        //    //    throw new Exception("{\"asError\":\"رل مورد نظر پیدا نشد\"}");
        //    //}
        //    var newApplicationTree = new ApplicationTree()
        //    {
        //        Text = node.Text,
        //        Url = node.Url,
        //        ParentId = node.ParentId,
        //        IsLeaf = node.IsLeaf,
        //        IconPath = node.IconPath,
        //        RoleId = node.RoleId
        //    };

        //    if (menu != null)
        //    {
        //        menu.ParentId = node.ParentId;
        //        menu.Text = node.Text;
        //        menu.Url = node.Url;
        //        menu.IsLeaf = node.IsLeaf;
        //        menu.IconPath = node.IconPath;
        //        menu.RoleId = node.RoleId;
        //        newApplicationTree = menu;
        //    }
        //    await _securityContext.SaveChangesAsync();

        //    return newApplicationTree;
        //}

        //public async Task<ApplicationTree> AddTreeAsync(ApplicationTree node)
        //{
        //    //var role = await _dbContext.Roles.Where(r => r.Name == node.Role.Name).FirstOrDefaultAsync();
        //    //if (role == null)
        //    //{
        //    //    throw new Exception("{\"asError\":\"رل مورد نظر پیدا نشد\"}");
        //    //}
        //    var newApplicationTree = new ApplicationTree()
        //    {
        //        Text = node.Text,
        //        Url = node.Url,
        //        ParentId = node.ParentId,
        //        IsLeaf = node.IsLeaf,
        //        IconPath = node.IconPath,
        //        RoleId = node.RoleId
        //    };
        //    newApplicationTree = _securityContext.ApplicationTrees.Add(newApplicationTree);

        //    await _securityContext.SaveChangesAsync();

        //    return newApplicationTree;
        //}

        ////private void BuildTree(ApplicationTree node, List<ApplicationTree> newTree, List<ApplicationTree> baseTree)
        ////{
        ////    var currentNode = node;


        ////    while (currentNode.ParentId != null)
        ////    {
        ////        if (!newTree.Exists(t => t.Id == currentNode.Id))
        ////            newTree.Add(currentNode);

        ////        currentNode = baseTree.Find(t => t.Id == currentNode.ParentId);
        ////    }
        ////}

        //public async Task<int> DeleteTreeAsync(int nodeId)
        //{
        //    //var deleteNumber = 0;
        //    //var node = await _dbContext.ApplicationTrees.Where(t => t.Id == nodeId).FirstOrDefaultAsync();
        //    //if (node == null) return deleteNumber;
        //    //_dbContext.ApplicationTrees.Remove(node);
        //    //deleteNumber = await _dbContext.SaveChangesAsync();

        //    //if (deleteNumber <= 0)
        //    //    throw new Exception("{\"asError\":\"حذف با خطا مواجه شد\"}");
        //    return await _securityContext.ApplicationTrees.Where(t => t.Id == nodeId).DeleteAsync();
        //}
    }
}
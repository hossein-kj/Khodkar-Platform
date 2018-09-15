using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityFramework.Extensions;
using KS.Business.ContenManagment.Base;
using KS.Core.Exceptions;
using KS.Core.GlobalVarioable;
using KS.Core.Localization;
using KS.Core.Security;
using KS.Model.ContentManagement;
using Newtonsoft.Json.Linq;
using KS.DataAccess.Contexts.Base;

namespace KS.Business.ContenManagment
{
    public class EntityGroupBiz : IEntityGroupBiz
    {

        private readonly IContentManagementContext _contentManagementContext;

        public EntityGroupBiz(IContentManagementContext contentManagementContext)
            :base()
        {
            _contentManagementContext = contentManagementContext;
        }

        #region [Save]
        public async Task<string> Save(JObject data)
        {
            dynamic groupDto = data;
            int groupId = groupDto.GroupId;
            int entityTypeId = groupDto.EntityTypeId;
            string groupName = groupDto.Name;
            if (!AuthorizeManager.AuthorizeActionOnEntityId(groupId, (int) EntityIdentity.Group,
                (int) ActionKey.EditGroup))
            {
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidAccessToEditGroup, groupName));
            }

            JArray sremovedListArray = groupDto.RemovedList;
            var removedList = sremovedListArray.ToObject<List<int>>();
            JArray addedListArray = groupDto.AddedList;
            var addedList = addedListArray.ToObject<List<int>>();
            foreach (var item in addedList)
            {
                if (entityTypeId == 101)
                {
                    var group = new EntityGroup()
                    {
                        GroupId = groupId,
                        EntityTypeId = entityTypeId,
                        LinkId = item
                    };
                    _contentManagementContext.EntityGroups.Add(group);
                }
                else
                {
                    var group = new EntityGroup()
                    {
                        GroupId = groupId,
                        EntityTypeId = entityTypeId,
                        MasterDataKeyValueId = item
                    };
                    _contentManagementContext.EntityGroups.Add(group);
                }
            }

            if (removedList.Count > 0)
            {
                if (entityTypeId == 101)
                    _contentManagementContext.EntityGroups.Where(eg => removedList.Contains(eg.LinkId ?? 0) && eg.GroupId==groupId).Delete();
                else
                {
                    _contentManagementContext.EntityGroups.Where(eg => removedList.Contains(eg.MasterDataKeyValueId ?? 0) && eg.GroupId == groupId).Delete();
                }
            }

            await _contentManagementContext.SaveChangesAsync();
         
            return entityTypeId == 101 ? "link":"masterData";
        }
        #endregion [Save]

    }
}

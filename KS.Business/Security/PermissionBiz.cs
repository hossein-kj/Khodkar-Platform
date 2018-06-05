using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using KS.Business.ContenManagment.Base;
using KS.Business.Security.Base;
using KS.Core.Exceptions;
using KS.Core.GlobalVarioable;
using KS.Core.Localization;
using KS.Core.Security;
using KS.Model.ContentManagement;
using Newtonsoft.Json.Linq;
using KS.DataAccess.Contexts.Base;

namespace KS.Business.Security
{
    public class PermissionBiz : BaseBiz, IPermissionBiz
    {
        private readonly IMasterDataKeyValueBiz _masterDataKeyValueBiz;
        private readonly IContentManagementContext _contentManagementContext;
        public PermissionBiz(IMasterDataKeyValueBiz masterDataKeyValueBiz, IContentManagementContext contentManagementContext)
        {
            _masterDataKeyValueBiz = masterDataKeyValueBiz;
            _contentManagementContext = contentManagementContext;
        }

        public async Task<MasterDataKeyValue> Save(JObject data)
        {
            dynamic masterDataDto = data;
            int entityId;

            bool isLeaf = masterDataDto.IsLeaf;
            if (isLeaf)
            {
                try
                {
                    entityId = masterDataDto.ForeignKey3;
                }
                catch (Exception)
                {
                    throw new KhodkarInvalidException(string.Format(LanguageManager.ToAsErrorMessage(ExceptionKey.FieldMustBeNumeric),
               "MasterDataKeyValue Id"));
                }
                int entityTypeId;
                try
                {
                    entityTypeId = masterDataDto.Key;
                }
                catch (Exception)
                {
                    throw new KhodkarInvalidException(string.Format(LanguageManager.ToAsErrorMessage(ExceptionKey.FieldMustBeNumeric),
               "MasterDataKeyValue Key"));
                }
                int actionTypeId;
                try
                {
                    actionTypeId = masterDataDto.ForeignKey1;
                }
                catch (Exception)
                {
                    throw new KhodkarInvalidException(string.Format(LanguageManager.ToAsErrorMessage(ExceptionKey.FieldMustBeNumeric),
               "MasterDataKeyValue ForeignKey1"));
                }
                if (entityTypeId != (int)EntityIdentity.Link)
                {
                    var masterDatakeyValue = await _contentManagementContext.MasterDataKeyValues.SingleOrDefaultAsync(md => md.Id == entityId);

                    if (masterDatakeyValue == null)
                        throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.MasterDataKeyValuesNotFound));

                    if (!AuthorizeManager.IsAuthorize(masterDatakeyValue.AccessRoleId))
                        throw new UnauthorizedAccessException(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidGrant));
                }
                else if(actionTypeId == (int)ActionKey.ViewSourceCode)
                {
                    var link = await _contentManagementContext.Links.SingleOrDefaultAsync(ln => ln.Id == entityId);
                    var webPages = await _contentManagementContext.WebPages
                        .Where(wp => wp.Url.ToLower() == link.Url).ToListAsync();

                    foreach (var webPage in webPages)
                    {
                        if (!AuthorizeManager.IsAuthorize(webPage.AccessRoleId))
                            throw new UnauthorizedAccessException(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidGrant));
                    }
                }

            }
            return await _masterDataKeyValueBiz.Save(data,true);
        }

        public async Task<MasterDataKeyValue> Delete(JObject data)
        {
            dynamic masterDataKeyValueData = data;
            int id;

            try
            {
                id = masterDataKeyValueData.Id;
            }
            catch (Exception)
            {

                throw new KhodkarInvalidException(string.Format(LanguageManager.ToAsErrorMessage(ExceptionKey.FieldMustBeNumeric),
                "MasterDataKeyValue Id"));
            }
            var masterDatakeyValue = await _contentManagementContext.MasterDataKeyValues.SingleOrDefaultAsync(md => md.Id == id)
             ;

            if (masterDatakeyValue == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.MasterDataKeyValuesNotFound));
            if (masterDatakeyValue.IsLeaf)
            {
                var entity = await _contentManagementContext.MasterDataKeyValues.SingleOrDefaultAsync(md => md.Id == masterDatakeyValue.ForeignKey3)
          ;

                if (!AuthorizeManager.IsAuthorize(entity.AccessRoleId))
                    throw new UnauthorizedAccessException(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidGrant));
            }

            return await _masterDataKeyValueBiz.Delete(data,true);
        }
    }
}

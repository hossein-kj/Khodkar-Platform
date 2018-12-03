using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using EntityFramework.Extensions;
using KS.Business.ContenManagment.Base;
using KS.Model.ContentManagement;
using Newtonsoft.Json.Linq;
using KS.Core.Exceptions;
using KS.Core.GlobalVarioable;
using KS.Core.Security;
using KS.Core.Utility;
using Newtonsoft.Json;
using KS.Core.Localization;
using KS.Core.CodeManager.Base;
using KS.Core.UI.Configuration;
using KS.DataAccess.Contexts.Base;

namespace KS.Business.ContenManagment
{
    public class MasterDataKeyValueBiz : IMasterDataKeyValueBiz
    {
      
        protected readonly ISourceControl SourceControl;
        protected readonly IContentManagementContext ContentManagementContext;
        protected readonly IWebConfigManager WebConfigManager;
        private readonly ISecurityContext _securityContext;
        public MasterDataKeyValueBiz(ISourceControl sourceControl,
            IContentManagementContext contentManagementContext
            , ISecurityContext securityContext
            , IWebConfigManager webConfigManager)
        {
            SourceControl = sourceControl;
            ContentManagementContext = contentManagementContext;
            WebConfigManager = webConfigManager;
            _securityContext = securityContext;
        }

        private string ProperPathOrUrl(string pathOrUrl,int protocolId)
        {
            if (pathOrUrl == null) return null;

            switch (protocolId)
            {
                case (int)Protocol.LocalUrlPorotocol:
                    if (pathOrUrl.IndexOf(Helper.RootUrl, StringComparison.Ordinal) != 0)
                    {
                        pathOrUrl = Helper.RootUrl + pathOrUrl;
                    }
                    break;
                case (int)Protocol.PathProtocol:
                    if (pathOrUrl.IndexOf("~", StringComparison.Ordinal) != 0)
                    {
                        if (pathOrUrl.IndexOf(Helper.RootUrl, StringComparison.Ordinal) != 0)
                            pathOrUrl = "~" + Helper.RootUrl + pathOrUrl;
                        else
                            pathOrUrl = "~" + pathOrUrl;
                    }
                    break;
                case (int)Protocol.NetworkPathProtocol:
                    if (pathOrUrl.IndexOf(Helper.RootUrl, StringComparison.Ordinal) != 0)
                    {
                        if (pathOrUrl.IndexOf(Helper.RootUrl, StringComparison.Ordinal) != 1)
                            pathOrUrl = Helper.RootUrl + Helper.RootUrl + pathOrUrl;
                        else
                            pathOrUrl = Helper.RootUrl + pathOrUrl;
                    }
                    else if (pathOrUrl.IndexOf(Helper.RootUrl, StringComparison.Ordinal) != 1)
                    {
                            pathOrUrl = Helper.RootUrl + pathOrUrl;
                    }
                    break;
                case (int)Protocol.Ftp:
                    if (pathOrUrl.IndexOf("ftp://", StringComparison.Ordinal) != 0)
                    {
                        pathOrUrl = "ftp://" + pathOrUrl;
                    }
                    break;
                case (int)Protocol.Http:
                    if (pathOrUrl.IndexOf("http://", StringComparison.Ordinal) != 0)
                    {
                        pathOrUrl = "http://" + pathOrUrl;
                    }
                    break;
                case (int)Protocol.Https:
                    if (pathOrUrl.IndexOf("https://", StringComparison.Ordinal) != 0)
                    {
                        pathOrUrl = "https://" + pathOrUrl;
                    }
                    break;
            }
            return pathOrUrl;
            //if (pathOrUrl == null) return null;
            //if (!isPath)
            //{
            //    if (pathOrUrl.IndexOf(Helper.RootUrl, StringComparison.Ordinal) != 0)
            //    {
            //        pathOrUrl = Helper.RootUrl + pathOrUrl;
            //    }

            //}
            //else
            //{

            //    if (pathOrUrl.IndexOf("~", StringComparison.Ordinal) != 0)
            //    {
            //        if (pathOrUrl.IndexOf(Helper.RootUrl, StringComparison.Ordinal) != 0)
            //            pathOrUrl = "~" + Helper.RootUrl + pathOrUrl;
            //        else
            //            pathOrUrl = "~" + pathOrUrl;
            //    }
            //}
            //return pathOrUrl;
        }
        #region [Save MasterData]
        public async Task<MasterDataKeyValue> Save(JObject data,bool isAuthroize=false)
        {
            dynamic masterDataDto = data;
            int? masterDataId = masterDataDto.Id;
            var masterData = new MasterDataKeyValue
            {
                Id = masterDataId ?? 0
            };
            bool isNew = masterDataDto.IsNew;

            //bool isPath;

            //try
            //{
            //    isPath = masterDataDto.IsPath;
            //}
            //catch (Exception)
            //{

            //    isPath = false;
            //}

            //bool isPathSecond;

            //try
            //{
            //    isPathSecond = masterDataDto.IsPathSecond;
            //}
            //catch (Exception)
            //{

            //    isPathSecond = false;
            //}

            int pathOrUrlProtocolId, secondPathOrUrlProtocolId;

            try
            {
                pathOrUrlProtocolId = masterDataDto.PathOrUrlProtocolId;
            }
            catch (Exception)
            {

                pathOrUrlProtocolId = (int)Protocol.LocalUrlPorotocol;
            }

            try
            {
                secondPathOrUrlProtocolId = masterDataDto.SecondPathOrUrlProtocolId;
            }
            catch (Exception)
            {

                secondPathOrUrlProtocolId = (int)Protocol.LocalUrlPorotocol;
            }

            var currentMasterData = await ContentManagementContext.MasterDataKeyValues.AsNoTracking().SingleOrDefaultAsync(md => md.Id == masterData.Id);

            if (!isNew)
            {
               
                if (currentMasterData == null)
                    throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.MasterDataKeyValuesNotFound));

                if (currentMasterData.EditMode)
                {
                    SourceControl.CheckCodeCheckOute(masterData);

                }

                masterData = currentMasterData;
                masterData.RowVersion = masterDataDto.RowVersion;

                ContentManagementContext.MasterDataKeyValues.Attach(masterData);
            }
            else
            {

                ContentManagementContext.MasterDataKeyValues.Add(masterData);
            }
            masterData.TypeId = masterDataDto.TypeId;

            //if (!isPath)
            //{
            //    if (masterDataUrl.IndexOf(Helper.RootUrl, StringComparison.Ordinal) != 0)
            //    {
            //        masterDataUrl = Helper.RootUrl + masterDataUrl;
            //    }

            //}
            //else
            //{

            //    if ( masterDataUrl.IndexOf("~", StringComparison.Ordinal) != 0)
            //    {
            //        if (masterDataUrl.IndexOf(Helper.RootUrl, StringComparison.Ordinal) != 0)
            //            masterDataUrl = "~" + Helper.RootUrl + masterDataUrl;
            //        else
            //            masterDataUrl = "~" + masterDataUrl;
            //    }
            //}
            string masterDataUrlOrPath = ProperPathOrUrl(Convert.ToString(masterDataDto.PathOrUrl), pathOrUrlProtocolId);
            if (masterDataUrlOrPath != Helper.RootUrl && masterData.TypeId != (int)EntityIdentity.BundleSource)
            {
                var repeatedMasterData =
                    await
                        ContentManagementContext.MasterDataKeyValues.Where(
                                md => md.PathOrUrl == masterDataUrlOrPath && md.TypeId == masterData.TypeId).CountAsync()
                            ;
                if ((repeatedMasterData > 0 && isNew) || (repeatedMasterData > 1 && !isNew))
                    throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.RepeatedValue, masterDataUrlOrPath));
               

            }
            masterData.PathOrUrl = masterDataUrlOrPath;
            try
            {
                int parentId = masterDataDto.ParentId;
                if (currentMasterData?.ParentId != parentId || isNew)
                {
                    
                    var parentCode = await ContentManagementContext.MasterDataKeyValues.SingleOrDefaultAsync(md => md.Id == parentId);
                    if (parentCode == null)
                        throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.ParentRecordNotFound));
                    AuthorizeManager.CheckParentNodeModifyAccessForAddingChildNode(parentCode, parentCode.Id);
                }
                masterData.ParentId = parentId;
            }
            catch (KhodkarInvalidException)
            {

                throw;
            }
            catch (Exception)
            {

                masterData.ParentId = null;
            }

            try
            {
                masterData.Key = masterDataDto.Key;
            }
            catch (Exception)
            {

                masterData.Key = null;
            }

            try
            {
                masterData.Value = masterDataDto.Value;
            }
            catch (Exception)
            {

                masterData.Value = null;
            }

            try
            {
                masterData.ForeignKey1 = masterDataDto.ForeignKey1;
            }
            catch (Exception)
            {

                masterData.ForeignKey1 = null;
            }

            try
            {
                masterData.ForeignKey2 = masterDataDto.ForeignKey2;
            }
            catch (Exception)
            {

                masterData.ForeignKey2 = null;
            }

            try
            {
                masterData.ForeignKey3 = masterDataDto.ForeignKey3;
            }
            catch (Exception)
            {

                masterData.ForeignKey3 = null;
            }

            masterData.SecondCode = masterDataDto.SecondCode;


            string secondMasterDataPathOrUrl = masterDataDto.SecondPathOrUrl;

            masterData.SecondPathOrUrl = ProperPathOrUrl(secondMasterDataPathOrUrl, secondPathOrUrlProtocolId);

            masterData.Name = masterDataDto.Name;
            masterData.Code = masterDataDto.Code;
            if (masterData.Code != null)
            {
                var repeatedMasterData =
                    await
                        ContentManagementContext.MasterDataKeyValues.Where(
                            md => md.Code == masterData.Code && md.TypeId == masterData.TypeId).CountAsync();

                if ((repeatedMasterData > 0 && isNew) || (repeatedMasterData > 1 && !isNew))
                    throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.RepeatedValue, masterData.Code));
               
            }



            masterData.Guid = masterDataDto.Guid;
            masterData.Description = masterDataDto.Description;
            masterData.Data = masterDataDto.Data;
            masterData.Version = (currentMasterData?.Version ?? 0) + 1; 

            try
            {
                masterData.Order = masterDataDto.Order;
            }
            catch (Exception)
            {
                masterData.Order = 1;
            }
            masterData.IsLeaf = masterDataDto.IsLeaf;
            masterData.IsType = masterDataDto.IsType;

            if (masterData.IsType && masterData.IsLeaf)
                throw new ConstraintException(LanguageManager.ToAsErrorMessage(ExceptionKey.MasterDataTypeIsLeaf));

            try
            {
                masterData.TypeId = masterDataDto.TypeId;
            }
            catch (Exception)
            {
                throw new ValidationException(LanguageManager.ToAsErrorMessage(ExceptionKey.FieldMustBeNumeric, "TypeId"));
              
            }

            try
            {
                masterData.ParentTypeId = masterDataDto.ParentTypeId;
            }
            catch (Exception)
            {
                if (masterData.IsType)
                    throw new ValidationException(LanguageManager.ToAsErrorMessage(ExceptionKey.FieldMustBeNumeric, "ParentTypeId"));
            
                masterData.ParentTypeId = null;
            }

            masterData.Language = Config.DefaultsLanguage;
            //if(masterData.IsLeaf)
            if(masterData.TypeId == (int)EntityIdentity.Permission && !isAuthroize)
                throw new UnauthorizedAccessException(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidGrant));

            if (currentMasterData != null)
            {
                masterData.ViewRoleId = currentMasterData.ViewRoleId;
                masterData.ModifyRoleId = currentMasterData.ModifyRoleId;
                masterData.AccessRoleId = currentMasterData.AccessRoleId;
            }


            AuthorizeManager.SetAndCheckModifyAndAccessRole(masterData, masterDataDto);


            masterData.Status = masterDataDto.Status;
            masterData.EditMode = masterDataDto.EditMode;
            masterData.EnableCache = masterDataDto.EnableCache;
            try
            {
                masterData.SlidingExpirationTimeInMinutes = masterDataDto.SlidingExpirationTimeInMinutes;
            }
            catch (Exception)
            {
                masterData.SlidingExpirationTimeInMinutes = 0;
            }
            await ContentManagementContext.SaveChangesAsync();
            if (masterData.TypeId == (int) EntityIdentity.SqlServerConnections)
                WebConfigManager.AddOrUpdateConnection(masterData.Code, masterData.SecondCode,
                    ConnectionProvider.SqlServer);
            else
                UpdateMasterDatakeyVlaueSettingOfWebConfig(masterData);
            return masterData;
        }
        #endregion [Save MasterData]
        private void UpdateMasterDatakeyVlaueSettingOfWebConfig(MasterDataKeyValue masterDataKeyValue)
        {
            if (WebConfigManager.IsSetting(masterDataKeyValue.Code))
            {
                var setting = WebConfigManager.GetSettingByOption(masterDataKeyValue.Code);
                //var webconfigSetting = JsonConvert.DeserializeObject<WebConfigSetting>(setting.Value);
                var propertyInfo = masterDataKeyValue.GetType().GetProperty(setting.MasterDataKeyValuePropertyName);
                if (propertyInfo != null)
                    setting.Value = Convert.ToString(propertyInfo
                        .GetValue(masterDataKeyValue, null));

                WebConfigManager.UpdateSetting(masterDataKeyValue.Code, setting.Value, JsonConvert.SerializeObject(setting));
            }
        }

        public async Task<MasterDataKeyValue> Delete(JObject data,bool isAuthroize=false)
        {
            dynamic masterDataKeyValueData = data;
            int id;

            try
            {
                id = masterDataKeyValueData.Id;
              
            }
            catch (Exception)
            {
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.FieldMustBeNumeric, "MasterDataKeyValue Id"));


            }
            var masterDatakeyValue = await ContentManagementContext.MasterDataKeyValues.SingleOrDefaultAsync(md => md.Id == id);

            if (masterDatakeyValue == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.MasterDataKeyValuesNotFound));

            if (masterDatakeyValue.TypeId == (int)EntityIdentity.Permission && !isAuthroize)
                throw new UnauthorizedAccessException(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidGrant));
            AuthorizeManager.SetAndCheckModifyAndAccessRole(masterDatakeyValue, null, false);


            if (masterDatakeyValue.EditMode)
            {
                SourceControl.CheckCodeCheckOute(masterDatakeyValue);

            }
            if (masterDatakeyValue.PathOrUrl != Helper.RootUrl)
            {
                var useCount = await ContentManagementContext.WebPages.Where(wp => wp.Services.Contains(masterDatakeyValue.PathOrUrl))
           .CountAsync();

                if (useCount > 0)
                    throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.InUseItem, masterDatakeyValue.Name));
          

            }
            var refrenceCount = await ContentManagementContext.MasterDataKeyValues.Where(md=> md.ForeignKey1==masterDatakeyValue.Id || md.ForeignKey2 == masterDatakeyValue.Id || md.ForeignKey3 == masterDatakeyValue.Id)
                 .CountAsync();

            if (refrenceCount > 0)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.InUseItem, masterDatakeyValue.Name));
      

            ContentManagementContext.MasterDataKeyValues.Remove(masterDatakeyValue);
       

            await ContentManagementContext.SaveChangesAsync();
            if (masterDatakeyValue.TypeId == (int) EntityIdentity.SqlServerConnections)
                WebConfigManager.RemoveConnection(masterDatakeyValue.Code);
                return masterDatakeyValue;
        }

        protected async Task<int> GetMaxId()
        {
            return (await ContentManagementContext.MasterDataKeyValues.OrderByDescending(md => md.Id).FirstOrDefaultAsync()).Id;
        }

        #region [Save MasterDataLocal]
        public async Task<MasterDataLocalKeyValue> SaveTranslate(JObject data)
        {
            dynamic masterDataLocalDto = data;
            int? masterDataLocalId = masterDataLocalDto.Id;
            var masterDataLocal = new MasterDataLocalKeyValue
            {
                Id = masterDataLocalId ?? 0
            };

            var currentMasterDataLocal = await ContentManagementContext.MasterDataLocalKeyValues.Include(md => md.MasterDataKeyValue)
                    .SingleOrDefaultAsync(md => md.Id == masterDataLocal.Id);
            if (masterDataLocal.Id > 0)
            {
                
                if (currentMasterDataLocal == null)
                    throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.TranslateNotFound));

                masterDataLocal = currentMasterDataLocal;
                masterDataLocal.RowVersion = masterDataLocalDto.RowVersion;

                ContentManagementContext.MasterDataLocalKeyValues.Attach(masterDataLocal);
            }
            else
            {
                ContentManagementContext.MasterDataLocalKeyValues.Add(masterDataLocal);
            }

            masterDataLocal.MasterDataKeyValueId = masterDataLocalDto.ItemId;
            masterDataLocal.Name = masterDataLocalDto.Name;
            masterDataLocal.Description = masterDataLocalDto.Description;
            masterDataLocal.Language = masterDataLocalDto.Language;

            var currentMasterData = await ContentManagementContext.MasterDataKeyValues
               .AsNoTracking().SingleOrDefaultAsync(md => md.Id == masterDataLocal.MasterDataKeyValueId);

            if (currentMasterData == null)
            {
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.PathNotFound, "MasterDataKeyValueId"));
            }

            AuthorizeManager.SetAndCheckModifyAndAccessRole(currentMasterData, masterDataLocalDto,false);


            masterDataLocal.Status = masterDataLocalDto.Status;
            await ContentManagementContext.SaveChangesAsync();
            return masterDataLocal;
        }
        #endregion [Save MasterDataLocal]
        public async Task<JObject> GetAsync(int id)
        {

            var masterDataQuery = ContentManagementContext.MasterDataKeyValues.Where(sr => sr.Id == id).FutureFirstOrDefault();
            var maxIdQuery = ContentManagementContext.MasterDataKeyValues.OrderByDescending(md => md.Id).FutureFirstOrDefault();

            var masterData = masterDataQuery.Value;
            var maxId = maxIdQuery.Value;

            if (masterData == null)
                return null;
            AuthorizeManager.CheckViewAccess(masterData);
            return await  ConvertToJsonAsync(masterData, maxId.Id+1);

        }

        private async Task<JObject> ConvertToJsonAsync(MasterDataKeyValue masterData, int newId)
        {

            var lastModifieUser =
               await _securityContext.Users.SingleOrDefaultAsync(us => us.Id == masterData.CreateUserId);
            return JObject.FromObject(new
            {
                masterData.Id,
                masterData.Guid,
                NewGuid = SecureGuid.NewGuid().ToString("N"),
                NewId = newId,
                masterData.PathOrUrl,
                masterData.SecondPathOrUrl,
                 masterData.Name,
                masterData.Code,
                masterData.SecondCode,
                masterData.Key,
                masterData.Value,
                masterData.TypeId,
                masterData.ParentTypeId,
                masterData.Description,
                masterData.Data,
                masterData.EditMode,
                masterData.SlidingExpirationTimeInMinutes,
                masterData.IsLeaf,
               masterData.IsType,
                 masterData.ParentId,
                 masterData.Order,
                 masterData.ForeignKey1,
                 masterData.ForeignKey2,
                 masterData.ForeignKey3,
                 masterData.ViewRoleId,
                masterData.ModifyRoleId,
                masterData.AccessRoleId,
                masterData.Version,
                masterData.EnableCache,
                 masterData.Status,
                LastModifieUser = lastModifieUser.UserName,
            LastModifieLocalDateTime = masterData.ModifieLocalDateTime,
            masterData.RowVersion
            });

        }

        //private async Task<IList<T>> GetMasterDataKeyValuesAsync<T>(string queryOption = null, int? roleId = null) where T : MasterDataKeyValueObjective
        //{

        //    var lang = Setting.Language;



        //    var baseTreeSource = _contentManagementContext.Set<T>().Select(sr => new
        //    {
        //        sr,
        //        LocalValues = sr.LocalValues.Where(lv => lv.Language == lang)
        //    });
        //    if (queryOption != null)
        //    {
        //        dynamic query = JObject.Parse(queryOption);
        //        baseTreeSource = _contentManagementContext.Set<T>().Where((string)query.query).Select(sr => new
        //        {
        //            sr,
        //            LocalValues = sr.LocalValues.Where(lv => lv.Language == lang)
        //        });
        //    }
        //    var baseTree = (await baseTreeSource.ToListAsync()).Select(newType => newType.sr).ToList();
        //    //var baseTree = await __contentManagementContext.Services.AsNoTracking().ToListAsync();
        //    var newTree = new List<T>();
        //    foreach (var node in baseTree)
        //    {
        //        if (node.IsLeaf)
        //        {
        //            if (roleId == null)
        //                BuildTree<T, MasterDataKeyValueObjective>(node, newTree, baseTree);
        //            else if (node.ViewRoleId == roleId)
        //            {
        //                BuildTree<T, MasterDataKeyValueObjective>(node, newTree, baseTree);
        //            }
        //        }

        //        if (node.ParentId == null)
        //            if (!newTree.Exists(t => t.Id == node.Id))
        //                newTree.Add(node);
        //    }
        //    return RemoveEmptyParentNode<T, MasterDataKeyValueObjective>(newTree);
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KS.Core.GlobalVarioable;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data.Entity;
using System.Linq.Dynamic;
using KS.Core.Exceptions;
using KS.Core.Utility;
using KS.Model.ContentManagement;
using KS.Core.Localization;
using KS.Core.Model.Core;
using KS.Core.Model.Develop;
using KS.Core.UI.Configuration;
using KS.DataAccess.Contexts.Base;

namespace KS.Business.Develop
{
    public class WebConfigBiz : IWebConfigBiz
    {
        private readonly IContentManagementContext _contentManagementContext;
        private readonly IWebConfigManager _webConfigManager;
        public WebConfigBiz(IContentManagementContext contentManagementContext
            , IWebConfigManager webConfigManager)
            : base()
        {
            _contentManagementContext = contentManagementContext;
            _webConfigManager = webConfigManager;
        }

        public async Task<WebConfigSetting> Save(JObject data)
        {
            dynamic dataDto = data;
            bool isMasterDataSetting = dataDto.IsMasterDataSetting;
            string value = dataDto.Value;
            string key = dataDto.Key;
            int id;
            JavaScriptType javaScriptType = dataDto.JavaScriptType;
            try
            {
                id = dataDto.MasterDataKeyValueId;
            }
            catch (Exception)
            {
                id = 0;
            }
            if (isMasterDataSetting)
            {
                
                var masterData = await _contentManagementContext.MasterDataKeyValues.FirstOrDefaultAsync(
                    md => md.Id == id);
                if (masterData == null)
                    throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.SettingNotFound));
                key = masterData.Code;
                value = Helper.GetPropertyValueByName(masterData,
                    Convert.ToString(dataDto.MasterDataKeyValuePropertyName));
            }
            var setting = new WebConfigSetting()
            {
                Key = key,
                Value = value,
                InjectToJavaScript = dataDto.InjectToJavaScript,
                Description = dataDto.Description,
                MasterDataKeyValueId = id,
                MasterDataKeyValuePropertyName = dataDto.MasterDataKeyValuePropertyName,
                JavaScriptType= javaScriptType.ToString()
            };
               
            
            var option = JsonConvert.SerializeObject(setting);
            _webConfigManager.AddOrUpdateSetting(key, value, option);
            setting.Key = key;
            setting.Value = value;
            return setting;
        }
        public bool Delete(JObject data)
        {
            dynamic dataDto = data;
            return _webConfigManager.RemoveSetting(Convert.ToString(dataDto.Key));
        }

        public async Task<JObject> GetSettingsByPagination(string orderBy, int skip, int take)
        {
            var settings = _webConfigManager.GetSetting();
            var codes = settings.Select(st => st.Key);
            var msterDataSettings = await _contentManagementContext.MasterDataKeyValues
                .Where(md => codes.Contains(md.Code)).ToListAsync();

            var webConfigSettings = new List<WebConfigSetting>();
            foreach (var setting in settings)
            {
                var masterDataSetting = msterDataSettings.FirstOrDefault(mds => mds.Code == setting.Key);
                if(masterDataSetting != null)
                {
                    try
                    {
                        var webconfigSetting = JsonConvert.DeserializeObject<WebConfigSetting>(setting.Value);
                        webConfigSettings.Add(new WebConfigSetting()
                        {
                            //Id= ++index,
                            Key = setting.Key,
                            Value = webconfigSetting.Value,
                            Description = webconfigSetting.Description,
                            InjectToJavaScript = webconfigSetting.InjectToJavaScript,
                            MasterDataKeyValuePropertyName = webconfigSetting.MasterDataKeyValuePropertyName,
                            MasterDataKeyValueId= masterDataSetting.Id,
                            MasterDataKeyValueTypeId=masterDataSetting.TypeId,
                            JavaScriptType= webconfigSetting.JavaScriptType
                        });
                    }
                    catch (Exception)
                    {

                        webConfigSettings.Add(new WebConfigSetting()
                        {
                            //Id = ++index,
                            Key = setting.Key,
                            Value = setting.Value,
                            InjectToJavaScript = false,
                            Description = "",
                            MasterDataKeyValuePropertyName = "",
                            MasterDataKeyValueId = masterDataSetting.Id,
                            MasterDataKeyValueTypeId = masterDataSetting.TypeId,
                            JavaScriptType=JavaScriptType.None.ToString()
                        });
                    }

                }
                else
                {
                    try
                    {
                        var webconfigSetting = JsonConvert.DeserializeObject<WebConfigSetting>(setting.Value);
                        webConfigSettings.Add(new WebConfigSetting()
                        {
                            //Id= ++index,
                            Key = setting.Key,
                            Value = webconfigSetting.Value,
                            Description = webconfigSetting.Description,
                            InjectToJavaScript = webconfigSetting.InjectToJavaScript,
                            MasterDataKeyValuePropertyName = "",
                            MasterDataKeyValueId = 0,
                            MasterDataKeyValueTypeId = 0,
                            JavaScriptType=webconfigSetting.JavaScriptType
                        });
                    }
                    catch (Exception)
                    {
                        webConfigSettings.Add(new WebConfigSetting()
                        {
                            //Id = ++index,
                            Key = setting.Key,
                            Value = setting.Value,
                            InjectToJavaScript = false,
                            Description = "",
                            MasterDataKeyValuePropertyName = "",
                            MasterDataKeyValueId = 0,
                            MasterDataKeyValueTypeId = 0,
                            JavaScriptType=JavaScriptType.None.ToString()
                        });
                    }

                }
            }
            return JObject.Parse(JsonConvert.SerializeObject
                (new
                {
                    rows = webConfigSettings.AsQueryable().OrderBy(orderBy)
                .Skip(skip)
                .Take(take == -1 ? webConfigSettings.Count:take).ToList(),
                    total = webConfigSettings.Count
                }, Formatting.None));
        }

        public List<KeyValue> GetMasterDataKeyValuePropertyName()
        {
            return typeof(MasterDataKeyValue).GetProperties().Where(pr=>pr.Name != "RowVersion" && pr.Name != "Childrens" && pr.Name != "LocalValues").Select(pr=> new KeyValue() {Key= pr.Name,Value=pr.Name }).ToList();
        }
    }
}

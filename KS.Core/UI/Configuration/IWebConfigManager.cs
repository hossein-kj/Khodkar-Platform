using System.Collections.Generic;
using KS.Core.Model;
using KS.Core.Model.Core;
using KS.Core.Model.Develop;

namespace KS.Core.UI.Configuration
{
    public interface IWebConfigManager
    {
        void AddOrUpdateConnection(string key, string connectionString, string provider, string path = "~");
        bool AddOrUpdateSetting(string key, string value, string option, string path = "~");
        bool AddSetting(string key, string value, string option, string path = "~");
        string GetConnection(string key, string path = "~");
        List<KeyValue> GetSetting(string path = "~");
        WebConfigSetting GetSetting(string key, string path = "~");
        WebConfigSetting GetSettingByOption(string key, string path = "~");
        List<WebConfigSetting> GetSettingListByOption(string path = "~");
        string GetSettingValue(string key, string path = "~");
        bool IsSetting(string key, string path = "~");
        void RemoveConnection(string key, string path = "~");
        bool RemoveSetting(string key, string path = "~");
        bool UpdateSetting(string key, string value, string option, string path = "~");
    }
}
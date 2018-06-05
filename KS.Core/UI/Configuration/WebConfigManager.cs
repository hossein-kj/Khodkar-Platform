using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using KS.Core.Model;
using KS.Core.Model.Core;
using KS.Core.Model.Develop;
using Newtonsoft.Json;

namespace KS.Core.UI.Configuration
{
    public class WebConfigManager : IWebConfigManager
    {
        private string _option = "_asOption";
        public bool AddSetting(string key,string value,string option, string path="~")
        {
            var myConfiguration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(path);
            myConfiguration.AppSettings.Settings.Add(key, value);
            myConfiguration.AppSettings.Settings.Add(key+_option, option);
            myConfiguration.Save();
            return true;
        }
        public bool UpdateSetting(string key, string value, string option, string path = "~")
        {
            var myConfiguration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(path);
            myConfiguration.AppSettings.Settings[key].Value=value;
            if (IsSetting(key + _option))
                myConfiguration.AppSettings.Settings[key + _option].Value = option;
            else
                myConfiguration.AppSettings.Settings.Add(key + _option, option);
            myConfiguration.Save();
            return true;
        }
        public bool RemoveSetting(string key, string path = "~")
        {
            var myConfiguration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(path);
            myConfiguration.AppSettings.Settings.Remove(key);
            myConfiguration.AppSettings.Settings.Remove(key+_option);
            myConfiguration.Save();
            return true;
        }

        public bool AddOrUpdateSetting(string key, string value, string option, string path = "~")
        {
            var myConfiguration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(path);
            return myConfiguration.AppSettings.Settings.AllKeys.FirstOrDefault(st => st == key) != null 
                ? UpdateSetting(key, value, option) : AddSetting(key, value, option);
        }

        public void AddOrUpdateConnection(string key, string connectionString, string provider, string path = "~")
        {
            var configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(path);
            var section = (ConnectionStringsSection)configuration.GetSection("connectionStrings");
            if (section.ConnectionStrings[key] != null)
            {
                section.ConnectionStrings[key].ConnectionString = connectionString;
                section.ConnectionStrings[key].ProviderName = provider;
            }
            else
            {
                section.ConnectionStrings.Add(new ConnectionStringSettings()
                {
                    Name = key,
                    ConnectionString = connectionString,
                    ProviderName = provider
                });
            }
            configuration.Save();
        }

        public string GetConnection(string key,string path = "~")
        {
            var configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(path);
            var section = (ConnectionStringsSection)configuration.GetSection("connectionStrings");
            if (section.ConnectionStrings[key] != null)
            {
                return section.ConnectionStrings[key].ConnectionString;
            }
            return null;
        }

        public void RemoveConnection(string key, string path = "~")
        {
            var configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(path);
            var section = (ConnectionStringsSection)configuration.GetSection("connectionStrings");
            section.ConnectionStrings.Remove(key);
            configuration.Save();
        }

        public bool IsSetting(string key, string path = "~")
        {
            var myConfiguration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(path);
            return myConfiguration.AppSettings.Settings.AllKeys.FirstOrDefault(st => st == key) != null;
        }

        public WebConfigSetting GetSetting(string key, string path = "~")
        {
            var myConfiguration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(path);
            var configSetting = new WebConfigSetting();
            try
            {
                configSetting = JsonConvert.DeserializeObject<WebConfigSetting>(myConfiguration.AppSettings.Settings[key].Value);
            }
            catch (Exception)
            {
                configSetting.Value = myConfiguration.AppSettings.Settings[key].Value;
                configSetting.Key = key;
            }
            return configSetting;
        }
        public WebConfigSetting GetSettingByOption(string key, string path = "~")
        {
            var myConfiguration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(path);
         

            return JsonConvert.DeserializeObject<WebConfigSetting>(myConfiguration.AppSettings.Settings[key + _option].Value);
        }
        public string GetSettingValue(string key, string path = "~")
        {
            var myConfiguration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(path);
            var m= myConfiguration.AppSettings.Settings[key].Value;
            return m;
        }

        public List<KeyValue> GetSetting(string path = "~")
        {
            var myConfiguration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(path);
            var webconfigSettings = new List<KeyValue>();
            foreach (KeyValueConfigurationElement setting in myConfiguration.AppSettings.Settings)
            {
               
                if (setting.Key.EndsWith(_option))
                {
                    var configSetting = webconfigSettings.Find(ws => ws.Key == setting.Key.Remove(setting.Key.IndexOf(_option, StringComparison.Ordinal)));
                    if (configSetting != null)
                    {
                        configSetting.Value = setting.Value;
                    }
                    else
                    {
                        webconfigSettings.Add(new KeyValue()
                        {
                            Key = setting.Key.Remove(setting.Key.IndexOf(_option, StringComparison.Ordinal)),
                            Value = setting.Value
                        });
                    }  
                }
                else
                {
                        if (
                        
                        !webconfigSettings.Exists(
                        ws => ws.Key == setting.Key))
                    {
                        webconfigSettings.Add(new KeyValue()
                        {
                            Key = setting.Key,
                            Value = setting.Value
                        });
                    }
                }
            }
            return webconfigSettings;
        }

        public List<WebConfigSetting> GetSettingListByOption(string path = "~")
        {
            var myConfiguration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(path);
            var webconfigSettings = new List<WebConfigSetting>();
            foreach (KeyValueConfigurationElement setting in myConfiguration.AppSettings.Settings)
            {

                if (setting.Key.EndsWith(_option))
                {
                    var configSetting = JsonConvert.DeserializeObject<WebConfigSetting>(setting.Value);
                    webconfigSettings.Add(configSetting);
                    
                }
                
            }
            return webconfigSettings;
        }
    }
}

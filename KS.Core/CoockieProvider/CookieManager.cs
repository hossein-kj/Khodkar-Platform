using System.Collections.Generic;
using KS.Core.CoockieProvider.Adapters;
using KS.Core.Model;
using KS.Core.Model.Core;
using Newtonsoft.Json.Linq;

namespace KS.Core.CoockieProvider
{
    public static class CookieManager
    {
        private static IDefaultCookieAdapter Adapter
        {
            get
            {
                return DependencyProvider.DependencyManager.Get<IDefaultCookieAdapter>();
            }

        }
        public static string Get(string key)
        {
            
            return Adapter.Get(key);
        }
        public static JObject GetAsJson(string key)
        {
            return Adapter.GetAsJson(key);
        }

        public static List<KeyValue> GetAll()
        {
            return Adapter.GetAll();
        }
    }

    public static class CookieManager<T> where T:ICookieAdapter
    {
        private static ICookieAdapter Adapter
        {
            get
            {
                return DependencyProvider.DependencyManager.Get<T>();
            }

        }
        public static string Get(string key)
        {

            return Adapter.Get(key);
        }
        public static JObject GetAsJson(string key)
        {
            return Adapter.GetAsJson(key);
        }

        public static List<KeyValue> GetAll()
        {
            return Adapter.GetAll();
        }
    }
}

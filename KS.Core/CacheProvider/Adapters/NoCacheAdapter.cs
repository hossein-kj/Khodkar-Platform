using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using KS.Core.Model;
using KS.Core.Model.Core;
using KS.Core.Security;

namespace KS.Core.CacheProvider.Adapters
{


    public class NoCacheAdapter : IDefaultCacheAdapter, ICacheAdapter
    {
        public void ClearCurrentUserCache()
        {
        }

        public CacheItem<T> Get<T>(string key)
        {
            return new CacheItem<T>() { Value = default(T), IsCached = false };
        }

        public IEnumerable<T> GetForCacheKey<T>(string key)
        {
            return new List<T>();
        }

        public CacheItem<T> GetForCurrentUserByKey<T>(string key)
        {
            return  new CacheItem<T>() { Value = default(T), IsCached = false };
        }

        public IEnumerable<object> GetAllForCurrentUser()
        {
            return new List<object>();
        }

        public void RemoveForCurrentUserByKey(string key)
        {
            
        }

 

        public void Store(string key, object value, DateTimeOffset absoluteExpiration = default(DateTimeOffset),
            TimeSpan slidingExpiration = default(TimeSpan))
        {

        }

        public void StoreForEver(string key, object value)
        {

        }




        public void StoreForCurrentUser(string key, object value)
        {

        }

        public void Remove(string key)
        {
           
        }

        public void Clear()
        {

        }

        public string GetBrowsersCodeInfoKey(string cacheKey, string browsersCodeInfo)
        {
            return "";
        }

        public string GetDebugUserKey(string cacheKey, int userId, string ip)
        {
            return "";
        }

        public string GetWebPageKey(string cacheKey, string url)
        {
            return "";
        }

        public string GetAspectKey(string cacheKey, string type, string url)
        {
            return (cacheKey + "_" + type + "_" + url).ToLower();
        }

        public virtual string GetGroupKey(string cacheKey, int groupId)
        {
            return (cacheKey + "_" + groupId).ToLower();
        }

        public void ClearAllItemContainKey(string key)
        {
            
        }
    }
}

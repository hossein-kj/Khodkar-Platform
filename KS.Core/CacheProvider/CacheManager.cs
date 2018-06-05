using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

using KS.Core.CacheProvider.Adapters;
using KS.Core.Model;
using KS.Core.DependencyProvider;
using KS.Core.Model.Core;

namespace KS.Core.CacheProvider
{
    public static class CacheManager 
    {
        private static IDefaultCacheAdapter Adapter => DependencyManager.Get<IDefaultCacheAdapter>();
        public static void ClearCurrentUserCache()
        {
           Adapter.ClearCurrentUserCache();
        }
        public static void ClearAllItemContainKey(string key)
        {
            Adapter.ClearAllItemContainKey(key);
        }
        public static CacheItem<T> Get<T>(string key)
        {
            return Adapter.Get<T>(key);
        }

        public static IEnumerable<T> GetForCacheKey<T>(string key)
        {
            return Adapter.GetForCacheKey<T>(key);
        }

        public static CacheItem<T> GetForCurrentUserByKey<T>(string key)
        {
            return Adapter.GetForCurrentUserByKey<T>(key);
        }

        public static IEnumerable<object> GetAllForCurrentUser()
        {
            return Adapter.GetAllForCurrentUser();

        }

        public static void RemoveForCurrentUserByKey(string key)
        {
            Adapter.RemoveForCurrentUserByKey(key);
        }

        public static void Store(string key, object value, DateTimeOffset absoluteExpiration = default(DateTimeOffset),
            TimeSpan slidingExpiration = default(TimeSpan))
        {
            Adapter.Store(key,value,absoluteExpiration:absoluteExpiration,slidingExpiration:slidingExpiration);
        }

        public static void StoreForEver(string key, object value)
        {
            Adapter.StoreForEver(key,value);
        }

        public static void StoreForCurrentUser(string key, object value)
        {
            Adapter.StoreForCurrentUser(key,value);
        }

        public static void Remove(string key)
        {
            Adapter.Remove(key);
        }

        public static void Clear()
        {
            Adapter.Clear();
        }

        public static string GetBrowsersCodeInfoKey(string cacheKey, string browsersCodeInfo)
        {
            return Adapter.GetBrowsersCodeInfoKey(cacheKey, browsersCodeInfo);
        }

        public static string GetDebugUserKey(string cacheKey, int userId,string ip)
        {
            return Adapter.GetDebugUserKey(cacheKey, userId, ip);
        }

        public static string GetWebPageKey(string cacheKey, string url)
        {
            return Adapter.GetWebPageKey(cacheKey, url);
        }

        public static string GetAspectKey(string cacheKey, string type, string url)
        {
            return Adapter.GetAspectKey(cacheKey, type, url);
        }

        public static string GetGroupKey(string cacheKey, int groupId)
        {
            return Adapter.GetGroupKey(cacheKey, groupId);
        }
    }

    public static class CacheManager<K> where K:ICacheAdapter
    {
        private static ICacheAdapter Adapter => DependencyManager.Get<ICacheAdapter>();

        public static void ClearCurrentUserCache()
        {
            Adapter.ClearCurrentUserCache();
        }
        public static void ClearAllItemContainKey(string key)
        {
            Adapter.ClearAllItemContainKey(key);
        }

        public static CacheItem<T> Get<T>(string key)
        {
            return Adapter.Get<T>(key);
        }

        public static IEnumerable<T> GetForCacheKey<T>(string key)
        {
            return Adapter.GetForCacheKey<T>(key);
        }

        public static CacheItem<T> GetForCurrentUserByKey<T>(string key)
        {
            return Adapter.GetForCurrentUserByKey<T>(key);
        }

        public static IEnumerable<object> GetAllForCurrentUser()
        {
            return Adapter.GetAllForCurrentUser();

        }

        public static void RemoveForCurrentUserByKey(string key)
        {
            Adapter.RemoveForCurrentUserByKey(key);
        }

        public static void Store(string key, object value, DateTimeOffset absoluteExpiration = default(DateTimeOffset),
            TimeSpan slidingExpiration = default(TimeSpan))
        {
            Adapter.Store(key, value, absoluteExpiration: absoluteExpiration, slidingExpiration: slidingExpiration);
        }

        public static void StoreForEver(string key, object value)
        {
            Adapter.StoreForEver(key, value);
        }

        public static void StoreForCurrentUser(string key, object value)
        {
            Adapter.StoreForCurrentUser(key, value);
        }

        public static void Remove(string key)
        {
            Adapter.Remove(key);
        }

        public static void Clear()
        {
            Adapter.Clear();
        }

        public static string GetBrowsersCodeInfoKey(string cacheKey, string browsersCodeInfo)
        {
            return Adapter.GetBrowsersCodeInfoKey(cacheKey, browsersCodeInfo);
        }

        public static string GetDebugUserKey(string cacheKey, int userId, string ip)
        {
            return Adapter.GetDebugUserKey(cacheKey, userId,ip);
        }

        public static string GetWebPageKey(string cacheKey, string url)
        {
            return Adapter.GetWebPageKey(cacheKey, url);
        }

        public static string GetAspectKey(string cacheKey, string type, string url)
        {
            return Adapter.GetAspectKey(cacheKey, type, url);
        }
        public static string GetGroupKey(string cacheKey, int groupId)
        {
            return Adapter.GetGroupKey(cacheKey, groupId);
        }
    }
}
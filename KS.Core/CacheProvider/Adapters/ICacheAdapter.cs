using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using KS.Core.Model;
using KS.Core.Model.Core;

namespace KS.Core.CacheProvider.Adapters
{
    public interface ICacheAdapter
    {
        void ClearAllItemContainKey(string key);
        void ClearCurrentUserCache();
        CacheItem<T> Get<T>(string key);
        CacheItem<T> GetForCurrentUserByKey<T>(string key);
        IEnumerable<T> GetForCacheKey<T>(string key);
        IEnumerable<object> GetAllForCurrentUser();
        void RemoveForCurrentUserByKey(string key);

        void Store(string key, object value, DateTimeOffset absoluteExpiration = default(DateTimeOffset),
            TimeSpan slidingExpiration = default(TimeSpan));

        void StoreForEver(string key, object value);
        void StoreForCurrentUser(string key, object value);
        void Remove(string key);
        void Clear();
        string GetBrowsersCodeInfoKey(string cacheKey, string browsersCodeInfo);
        string GetDebugUserKey(string cacheKey, int userId, string ip);
        string GetWebPageKey(string cacheKey, string url);
        string GetAspectKey(string cacheKey, string type, string url);
        string GetGroupKey(string cacheKey, int groupId);
    }
}

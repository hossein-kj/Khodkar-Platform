using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using KS.Core.Model;
using KS.Core.Model.Core;
using KS.Core.Security;

namespace KS.Core.CacheProvider.Adapters
{
    public abstract class BaseCacheAdapter 
    {
        protected readonly MemoryCache Cache = MemoryCache.Default;

        public virtual void ClearAllItemContainKey(string key)
        {

            var cacheItems = Cache.Where(ch => ch.Key.Contains(key.ToLower()));
            foreach (var item in cacheItems)
            {
                Cache.Remove(item.Key);
            }
        }
        public virtual void ClearCurrentUserCache()
        {
            if(string.IsNullOrEmpty(CurrentUserManager.UserName))
                return;
            var cacheItems = Cache.Where(ch => ch.Key.Contains(CurrentUserManager.UserName));
            foreach (var item in cacheItems)
            {
                Cache.Remove(item.Key);
            }
        }

        public virtual IEnumerable<T> GetForCacheKey<T>(string key)
        {
            var cacheItems = Cache.Where(ch => ch.Key.StartsWith(key.ToLower() + "_"));

            return cacheItems.Select(item => default(T)).ToList();
        }

        public virtual CacheItem<T> Get<T>(string key)
        {
            key = key.ToLower();
            var value = Cache.Get(key);
            return value != null ? new CacheItem<T>() {Value = (T)value,IsCached = true}
            : new CacheItem<T>() { Value = default(T), IsCached = false };
        }

        public virtual CacheItem<T> GetForCurrentUserByKey<T>(string key)
        {
            key = key.ToLower();
            var value = Cache.Get(CurrentUserManager.UserName + key);
            return value != null ? new CacheItem<T>() { Value = (T)value, IsCached = true }
            : new CacheItem<T>() { Value = default(T), IsCached = false };
        }

        public virtual IEnumerable<object> GetAllForCurrentUser()
        {
            return Cache.Where(ch => ch.Key.Contains(CurrentUserManager.UserName)).Select(ch=>ch.Value);
        }

        public virtual void RemoveForCurrentUserByKey(string key)
        {
            key = key.ToLower();
            Cache.Remove(CurrentUserManager.UserName + key);
        }

 

        public virtual void Store(string key, object value, DateTimeOffset absoluteExpiration = default(DateTimeOffset),
            TimeSpan slidingExpiration = default(TimeSpan))
        {
            key = key.ToLower();

            var cacheItemPolicy = new CacheItemPolicy();

            if (absoluteExpiration != default(DateTimeOffset))
                cacheItemPolicy.AbsoluteExpiration = absoluteExpiration;

            if (slidingExpiration != default(TimeSpan))
                cacheItemPolicy.SlidingExpiration = slidingExpiration;

            Cache.Add(key, value, cacheItemPolicy);
        }


        public virtual void StoreForEver(string key, object value)
        {
            key = key.ToLower();
            Cache.Add(key, value, new CacheItemPolicy
            {
                AbsoluteExpiration = ObjectCache.InfiniteAbsoluteExpiration,
                SlidingExpiration = ObjectCache.NoSlidingExpiration,
                Priority = System.Runtime.Caching.CacheItemPriority.NotRemovable,
                RemovedCallback = RemovedCallback
            });
        }




        public virtual void StoreForCurrentUser(string key, object value)
        {
            key = key.ToLower();
            Cache.Add(CurrentUserManager.UserName + key, value, new CacheItemPolicy
            {
                SlidingExpiration = TimeSpan.FromMinutes(60)
            });
        }

        public virtual void Remove(string key)
        {
            key = key.ToLower();
            Cache.Remove(key);
        }

        public virtual void Clear()
        {
            foreach (var item in Cache)
            {
                Cache.Remove(item.Key);
            }
        }

        protected virtual void RemovedCallback(CacheEntryRemovedArguments arguments)
        {

        }
        public virtual string GetBrowsersCodeInfoKey(string cacheKey, string browsersCodeInfo)
        {
            return (cacheKey + "_" + browsersCodeInfo).ToLower();
        }
        public virtual string GetDebugUserKey(string cacheKey, int userId, string ip)
        {
            return (cacheKey + "_" + userId + "_" + ip).ToLower();
        }
        public virtual string GetWebPageKey(string cacheKey,string url)
        {
            return (cacheKey + "_" + url).ToLower();
        }

        public virtual string GetAspectKey(string cacheKey,string type, string url)
        {
            return (cacheKey + "_" + type + "_" + url).ToLower();
        }

        public virtual string GetGroupKey(string cacheKey, int groupId)
        {
            return (cacheKey + "_" + groupId).ToLower();
        }
    }
}

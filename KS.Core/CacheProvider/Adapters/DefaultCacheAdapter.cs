using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using Autofac.Integration.Mvc;
using KS.Core.Model;
using KS.Core.Security;

namespace KS.Core.CacheProvider.Adapters
{
    public sealed class DefaultCacheAdapter :BaseCacheAdapter, ICacheAdapter,IDefaultCacheAdapter
    {
    }
}

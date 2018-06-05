using Autofac;
using KS.Core.CacheProvider;
using KS.Core.CacheProvider.Adapters;
using KS.Core.GlobalVarioable;


namespace KS.Core.DependencyProvider.AutoFacModule
{
    public class CacheModule : Module
    {
        public CacheType CacheType { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            if (CacheType == CacheType.Memory)
            {
                builder.RegisterInstance<IDefaultCacheAdapter>(new DefaultCacheAdapter());
            }
            else
            {
                builder.RegisterInstance<IDefaultCacheAdapter>(new NoCacheAdapter());
            }
        }
    }
}
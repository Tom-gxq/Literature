using Grpc.Service.Core.Caching;
using Grpc.Service.Core.Caching.Configuration;
using Grpc.Service.Core.Dependency;
using RedisCache.Service.Runtime.Caching.Redis;
using System;

namespace RedisCache.Service
{
    public class RedisCacheRegisteConfig
    {
        public static void Register(IIocManager localIocManager)
        {
            localIocManager.Register<RedisCacheOptions>();
            localIocManager.Register<ICachingConfiguration, CachingConfiguration>();
            localIocManager.Register<IRedisCacheDatabaseProvider, RedisCacheDatabaseProvider>();
            localIocManager.Register<IRedisCacheSerializer, DefaultRedisCacheSerializer>();
            localIocManager.Register<ICacheManager, RedisCacheManager>();

            var defaultSlidingExpireTime = TimeSpan.FromHours(24);
            localIocManager.Resolve<ICachingConfiguration>().Configure("CacheItems", cache =>
            {
                cache.DefaultSlidingExpireTime = defaultSlidingExpireTime;
            });
        }
    }
}

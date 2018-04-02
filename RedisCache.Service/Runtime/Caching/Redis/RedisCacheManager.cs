using Grpc.Service.Core.Caching;
using Grpc.Service.Core.Caching.Configuration;
using Grpc.Service.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedisCache.Service.Runtime.Caching.Redis
{
    public class RedisCacheManager : CacheManagerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RedisCacheManager"/> class.
        /// </summary>
        public RedisCacheManager(IIocManager iocManager, ICachingConfiguration configuration)
            : base(iocManager, configuration)
        {
            IocManager.RegisterIfNot<RedisCache>(DependencyLifeStyle.Transient);
        }

        protected override ICache CreateCacheImplementation(string name)
        {
            return IocManager.Resolve<RedisCache>(new { name });
        }
    }
}

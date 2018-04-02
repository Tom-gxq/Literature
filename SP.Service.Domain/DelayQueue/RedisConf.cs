using Grpc.Service.Core.Caching;
using Grpc.Service.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.DelayQueue
{
    internal class RedisConf
    {
        public readonly string QUEUE_KEY = "delay_queue";
        public readonly string DATA_PREFIX = "queue_data";
        public ITypedCache<string, string> Client;
        public RedisConf()
        {
            Client = IocManager.Instance.Resolve<ICacheManager>().GetCache<string, string>("CacheItems");
        }
    }
}

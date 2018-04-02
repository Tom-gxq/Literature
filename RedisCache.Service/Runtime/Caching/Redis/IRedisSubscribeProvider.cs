using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedisCache.Service.Runtime.Caching.Redis
{
    public interface IRedisSubscribeProvider
    {
        ISubscriber GetSubscriber(string channelKey);
    }
}

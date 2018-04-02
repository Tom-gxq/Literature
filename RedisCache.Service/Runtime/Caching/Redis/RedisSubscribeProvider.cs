using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedisCache.Service.Runtime.Caching.Redis
{
    public class RedisSubscribeProvider : IRedisSubscribeProvider
    {
        private readonly RedisCacheOptions _options;
        private readonly Lazy<ConnectionMultiplexer> _connectionMultiplexer;

        /// <summary>
        /// Initializes a new instance of the <see cref="RedisCacheDatabaseProvider"/> class.
        /// </summary>
        public RedisSubscribeProvider(RedisCacheOptions options)
        {
            _options = options;
            _connectionMultiplexer = new Lazy<ConnectionMultiplexer>(CreateConnectionMultiplexer);
        }

        /// <summary>
        /// Gets the database connection.
        /// </summary>
        public ISubscriber GetSubscriber(string channelKey)
        {
            return _connectionMultiplexer.Value.GetSubscriber(channelKey);
        }

        private ConnectionMultiplexer CreateConnectionMultiplexer()
        {
            return ConnectionMultiplexer.Connect(_options.ConnectionString);
        }
    }
}

using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedisCache.Service.Runtime.Caching.Redis
{
    public class RedisCacheDatabaseProvider : IRedisCacheDatabaseProvider
    {
        private readonly RedisCacheOptions _options;
        private readonly Lazy<ConnectionMultiplexer> _connectionMultiplexer;

        /// <summary>
        /// Initializes a new instance of the <see cref="RedisCacheDatabaseProvider"/> class.
        /// </summary>
        public RedisCacheDatabaseProvider(RedisCacheOptions options)
        {
            _options = options;
            _connectionMultiplexer = new Lazy<ConnectionMultiplexer>(CreateConnectionMultiplexer);
        }

        /// <summary>
        /// Gets the database connection.
        /// </summary>
        public IDatabase GetDatabase()
        {
            return _connectionMultiplexer.Value.GetDatabase(_options.DatabaseId);
        }

        private ConnectionMultiplexer CreateConnectionMultiplexer()
        {
            var option = ConfigurationOptions.Parse(_options.ConnectionString);
            option.AbortOnConnectFail = false;
            option.ConnectTimeout = 10000;
            option.SyncTimeout = 3000;
            option.Ssl = false;
            option.ConnectRetry = 5;
            return ConnectionMultiplexer.Connect(option);
        }
    }
}

using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedisCache.Service.Runtime.Caching.Redis
{
    public interface IRedisCacheDatabaseProvider
    {
        /// <summary>
        /// Gets the database connection.
        /// </summary>
        IDatabase GetDatabase();
    }
}

using Grpc.Service.Core.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedisCache.Service.Runtime.Caching.Redis
{
    public class DefaultRedisCacheSerializer : IRedisCacheSerializer
    {
        /// <summary>
        ///     Creates an instance of the object from its serialized string representation.
        /// </summary>
        /// <param name="objbyte">String representation of the object from the Redis server.</param>
        /// <returns>Returns a newly constructed object.</returns>
        /// <seealso cref="IRedisCacheSerializer.Serialize" />
        public virtual object Deserialize(RedisValue objbyte)
        {
            return objbyte.ToString();
        }

        /// <summary>
        ///     Produce a string representation of the supplied object.
        /// </summary>
        /// <param name="value">Instance to serialize.</param>
        /// <param name="type">Type of the object.</param>
        /// <returns>Returns a string representing the object instance that can be placed into the Redis cache.</returns>
        /// <seealso cref="IRedisCacheSerializer.Deserialize" />
        public virtual string Serialize(object value, Type type)
        {
            return value.ToString();
        }
    }
}

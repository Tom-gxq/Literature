using Grpc.Service.Core.Caching;
using Grpc.Service.Core.Dependency;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedisCache.Service.Runtime.Caching.Redis
{
    public class RedisCache : CacheBase
    {
        private readonly IDatabase _database;
        private readonly IRedisCacheSerializer _serializer;
        private readonly ISubscriber _subscriber;

        /// <summary>
        /// Constructor.
        /// </summary>
        public RedisCache(
            string name,
            IRedisCacheDatabaseProvider redisCacheDatabaseProvider,
            IRedisCacheSerializer redisCacheSerializer)
            : base(name)
        {
            _database = redisCacheDatabaseProvider.GetDatabase();
            _subscriber = redisCacheDatabaseProvider.GetSubscriber();
            _serializer = redisCacheSerializer;
        }

        public override object GetOrDefault(string key)
        {
            var objbyte = _database.StringGet(GetLocalizedKey(key));
            return objbyte.HasValue ? Deserialize(objbyte) : null;
        }
        public override object[] StringGet(string[] keys)
        {
            List<RedisKey> list = new List<RedisKey>();
            foreach(var item in keys)
            {
                var key = GetLocalizedKey(item.ToString());
                list.Add(key);
            }
            var objbyte = _database.StringGet(list.ToArray());
            List<object> retList = new List<object>();
            if (objbyte != null)
            {                
                foreach(var item in objbyte)
                {
                    retList.Add(Deserialize(item));
                }                
            }
            return retList.ToArray();
        }
        public override long KeyDelete(string[] keys)
        {
            List<RedisKey> list = new List<RedisKey>();
            foreach (var item in keys)
            {
                var key = GetLocalizedKey(item.ToString());
                list.Add(key);
            }
            var objbyte = _database.KeyDelete(list.ToArray());
            return objbyte;
        }

        public override object HashGet(string hashKey, string key)
        {
            var objbyte = _database.HashGet(GetHashKey(hashKey), GetLocalizedKey(key));
            return objbyte.HasValue ? Deserialize(objbyte) : null;
        }
        public override object[] ListRange(string key, long start = 0, long stop = -1)
        {
            var objbyte = _database.ListRange(key, start, stop);
            List<object> retList = new List<object>();
            if (objbyte != null)
            {
                foreach (var item in objbyte)
                {
                    retList.Add(Deserialize(item));
                }
            }
            return retList.ToArray();
        }
        public override object ListRightPopLeftPush(string source, string destination)
        {
            var objbyte = _database.ListRightPopLeftPush(source, destination);
            return objbyte.HasValue ? Deserialize(objbyte) : null;
        }
        public override object ListRightPop(string key)
        {
            var objbyte = _database.ListRightPop(key);
            return objbyte.HasValue ? Deserialize(objbyte) : null;
        }
        public override long ListRightPush(string key, string value)
        {
            var objbyte = _database.ListRightPush(key, value);
            return objbyte;
        }
        public override long ListRemove(string key, string value, long count = 0)
        {
            var objbyte = _database.ListRemove(key, value, count);
            return objbyte;
        }
        public override long ListLength(string key)
        {
            var objbyte = _database.ListLength(key);
            return objbyte;
        }
        public override long ListLeftPush(string key, string value)
        {
            var objbyte = _database.ListLeftPush(key, value);
            return objbyte;
        }
        public override long ListLeftPush(string key, string[] values)
        {
            //var objbyte = _database.ListLeftPush(key, values);
            //return objbyte;
            return 0;
        }
        public override object ListLeftPop(string key)
        {
            var objbyte = _database.ListLeftPop(key);
            return objbyte;
        }
        public override bool SortedSetAdd(string key, string member, double value)
        {
            var objbyte = _database.SortedSetAdd(GetHashKey(key), member, value);
            return objbyte;
        }
        
        public override bool SortedSetRemove(string key, string member)
        {
            var objbyte = _database.SortedSetRemove(GetHashKey(key), member);
            return objbyte;
        }
        public override List<object> SortedSetRangeByScore(string key, double start , double stop )
        {
            var objbyte = _database.SortedSetRangeByScore(GetHashKey(key), start, stop);
            List<object> list = new List<object>();
            if (objbyte != null)
            {
                foreach (var item in objbyte)
                {
                    list.Add(Deserialize(item));
                }
            }
            return list;
        }

        public override void Set(string key, object value, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null)
        {
            if (value == null)
            {
                throw new Exception("Can not insert null values to the cache!");
            }


            var type = value.GetType();

            var timeSpan = absoluteExpireTime ?? slidingExpireTime ?? DefaultAbsoluteExpireTime ?? DefaultSlidingExpireTime;
            _database.StringSet(
                GetLocalizedKey(key),
                Serialize(value, type),
                timeSpan
                );
        }

        public override void HashSet(string hashKey, string key, object value)
        {
            if (value == null)
            {
                throw new Exception("Can not insert null values to the cache!");
            }


            var type = value.GetType();


            _database.HashSet(GetHashKey(hashKey),
                GetLocalizedKey(key),
                Serialize(value, type)
                );
        }

        /// <summary>
        /// 增量
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public override void IncrementValueBy(string key, int count)
        {
            _database.StringIncrement(GetLocalizedKey(key), count);
        }
        /// <summary>
        /// 减量
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public override void DecrementValueBy(string key, int count)
        {
            _database.StringDecrement(GetLocalizedKey(key), count);
        }

        public override void Remove(string key)
        {
            _database.KeyDelete(GetLocalizedKey(key.ToLower()));
        }

        public override void HashRemove(string hashkey, string key)
        {
            var ret = _database.HashDelete(GetHashKey(hashkey), GetLocalizedKey(key));
        }

        public override void Clear()
        {
            _database.KeyDeleteWithPrefix(GetLocalizedKey("*"));
        }
        
        protected virtual string Serialize(object value, Type type)
        {
            return _serializer.Serialize(value, type);
        }

        protected virtual object Deserialize(RedisValue objbyte)
        {
            return _serializer.Deserialize(objbyte);
        }

        protected virtual string GetLocalizedKey(string key)
        {
            return key.ToLower();
        }

        protected virtual string GetHashKey(string key)
        {
            var config = IocManager.Instance.Resolve<IConfigurationRoot>();
            if (config != null)
            {
                var reObj = config.GetSection("RunningEnviorment");
                if (reObj != null)
                {
                    var reValue = reObj.Value;
                    if (!string.IsNullOrEmpty(reValue))
                    {
                        key = key + reValue;
                    }
                }
            }
            return key;
        }

        public override long Publish(string channel, string message)
        {
            return _subscriber.Publish(channel, message);
        }
        
        public override void Subscribe(string channel, Action<string, string> handler)
        {
            _subscriber.Subscribe(channel, (RedisChannel, RedisValue) =>handler(RedisChannel, RedisValue));
        }
        public override void Unsubscribe(string channel, Action<string, string> handler)
        {
            _subscriber.Unsubscribe(channel, (RedisChannel, RedisValue) => handler(RedisChannel, RedisValue));
        }
    }
}

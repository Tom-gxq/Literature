using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Grpc.Service.Core.Caching
{
    /// <summary>
    /// Extension methods for <see cref="ICache"/>.
    /// </summary>
    public static class CacheExtensions
    {
        public static object Get(this ICache cache, string key, Func<object> factory)
        {
            return cache.Get(key, k => factory());
        }

        public static Task<object> GetAsync(this ICache cache, string key, Func<Task<object>> factory)
        {
            return cache.GetAsync(key, k => factory());
        }

        public static ITypedCache<TKey, TValue> AsTyped<TKey, TValue>(this ICache cache)
        {
            return new TypedCacheWrapper<TKey, TValue>(cache);
        }
        
        public static TValue Get<TKey, TValue>(this ICache cache, TKey key, Func<TKey, TValue> factory)
        {
            return (TValue)cache.Get(key.ToString(), (k) => (object)factory(key));
        }

        public static TValue Get<TKey, TValue>(this ICache cache, TKey key, Func<TValue> factory)
        {
            return cache.Get(key, (k) => factory());
        }
        public static object[] StringGet<TKey>(this ICache cache, TKey[] keys)
        {
            string[] list = new string[keys.Length];
            for (int i= 0; i<keys.Length;i++)
            {
                list[i] = keys[i].ToString();
            }
            return cache.StringGet(list);
        }
        public static long KeyDelete(this ICache cache, object[] keys)
        {
            string[] list = new string[keys.Length];
            for (int i = 0; i < keys.Length; i++)
            {
                list[i] = keys[i].ToString();
            }
            return cache.KeyDelete(list);
        }

        public static async Task<TValue> GetAsync<TKey, TValue>(this ICache cache, TKey key, Func<TKey, Task<TValue>> factory)
        {
            var value = await cache.GetAsync(key.ToString(), async (keyAsString) =>
            {
                var v = await factory(key);
                return (object)v;
            });

            return (TValue)value;
        }

        public static Task<TValue> GetAsync<TKey, TValue>(this ICache cache, TKey key, Func<Task<TValue>> factory)
        {
            return cache.GetAsync(key, (k) => factory());
        }

        public static TValue GetOrDefault<TKey, TValue>(this ICache cache, TKey key)
        {
            var value = cache.GetOrDefault(key.ToString());
            if (value == null)
            {
                return default(TValue);
            }

            return (TValue) value;
        }

        public static TValue HashGet<TKey, TValue>(this ICache cache, string hashKey, TKey key)
        {
            var value = cache.HashGet(hashKey,key.ToString());
            if (value == null)
            {
                return default(TValue);
            }

            return (TValue)value;
        }
        public static object[] ListRange<TKey>(this ICache cache, TKey key,long start = 0, long stop = -1)
        {
            return cache.ListRange(key.ToString(), start, stop);
        }
        public static TValue ListRightPopLeftPush<TKey, TValue>(this ICache cache, TKey source, TKey destination)
        {
            var value = cache.ListRightPopLeftPush(source.ToString(), destination.ToString());
            if (value == null)
            {
                return default(TValue);
            }

            return (TValue)value;
        }
        public static TValue ListRightPop<TKey, TValue>(this ICache cache, TKey key)
        {
            var value = cache.ListRightPop(key.ToString());
            if (value == null)
            {
                return default(TValue);
            }

            return (TValue)value;
        }
        public static long ListRightPush<TKey, TValue>(this ICache cache, TKey key, TValue value)
        {
            var ret = cache.ListRightPush(key.ToString(), value.ToString());
            
            return ret;
        }
        public static long ListRemove<TKey, TValue>(this ICache cache, TKey key, TValue value, long count = 0)
        {
            var ret = cache.ListRemove(key.ToString(), value.ToString(), count);

            return ret;
        }
        public static long ListLength<TKey>(this ICache cache, TKey key)
        {
            var ret = cache.ListLength(key.ToString());

            return ret;
        }
        public static long ListLeftPush<TKey, TValue>(this ICache cache, TKey key, TValue value)
        {
            var ret = cache.ListLeftPush(key.ToString(), value.ToString());

            return ret;
        }
        public static long ListLeftPush<TKey, TValue>(this ICache cache, TKey key, TValue[] value)
        {
            var ret = cache.ListLeftPush(key.ToString(), value.ToString());

            return ret;
        }
        public static TValue ListLeftPop<TKey, TValue>(this ICache cache, TKey key)
        {
            var value = cache.ListLeftPop(key.ToString());
            if (value == null)
            {
                return default(TValue);
            }

            return (TValue)value;
        }
        public static bool SortedSetAdd<TKey>(this ICache cache, TKey key, TKey member, double value)
        {
            var val = cache.SortedSetAdd(key.ToString(), member.ToString(), value);
            
            return val;
        }
        public static List<object> SortedSetRangeByScore(this ICache cache, string key, double start , double stop )
        {
            var val = cache.SortedSetRangeByScore(key, start, stop);

            return val;
        }
        public static bool SortedSetRemove<TKey>(this ICache cache, TKey key, TKey member)
        {
            var val = cache.SortedSetRemove(key.ToString(), member.ToString());

            return val;
        }
        public static async Task<TValue> GetOrDefaultAsync<TKey, TValue>(this ICache cache, TKey key)
        {
            var value = await cache.GetOrDefaultAsync(key.ToString());
            if (value == null)
            {
                return default(TValue);
            }

            return (TValue)value;
        }
    }
}
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Grpc.Service.Core.Caching
{
    /// <summary>
    /// Implements <see cref="ITypedCache{TKey,TValue}"/> to wrap a <see cref="ICache"/>.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class TypedCacheWrapper<TKey, TValue> : ITypedCache<TKey, TValue>
    {
        public string Name
        {
            get { return InternalCache.Name; }
        }

        public TimeSpan DefaultSlidingExpireTime
        {
            get { return InternalCache.DefaultSlidingExpireTime; }
            set { InternalCache.DefaultSlidingExpireTime = value; }
        }

        public ICache InternalCache { get; private set; }

        /// <summary>
        /// Creates a new <see cref="TypedCacheWrapper{TKey,TValue}"/> object.
        /// </summary>
        /// <param name="internalCache">The actual internal cache</param>
        public TypedCacheWrapper(ICache internalCache)
        {
            InternalCache = internalCache;
        }

        public void Dispose()
        {
            InternalCache.Dispose();
        }

        public void Clear()
        {
            InternalCache.Clear();
        }

        public Task ClearAsync()
        {
            return InternalCache.ClearAsync();
        }

        public TValue Get(TKey key, Func<TKey, TValue> factory)
        {
            return InternalCache.Get(key, factory);
        }
        public object[] StringGet(object[] keys)
        {
            return InternalCache.StringGet(keys);
        }
        public long KeyDelete(object[] keys)
        {
            return InternalCache.KeyDelete(keys);
        }

        public Task<TValue> GetAsync(TKey key, Func<TKey, Task<TValue>> factory)
        {
            return InternalCache.GetAsync(key, factory);
        }

        public TValue GetOrDefault(TKey key)
        {
            return InternalCache.GetOrDefault<TKey, TValue>(key);
        }

        public Task<TValue> GetOrDefaultAsync(TKey key)
        {
            return InternalCache.GetOrDefaultAsync<TKey, TValue>(key);
        }

        public void Set(TKey key, TValue value, TimeSpan? slidingExpireTime = null)
        {
            InternalCache.Set(key.ToString(), value, slidingExpireTime);
        }

        public Task SetAsync(TKey key, TValue value, TimeSpan? slidingExpireTime = null)
        {
            return InternalCache.SetAsync(key.ToString(), value, slidingExpireTime);
        }

        public void Remove(TKey key)
        {
            InternalCache.Remove(key.ToString());
        }

        public Task RemoveAsync(TKey key)
        {
            return InternalCache.RemoveAsync(key.ToString());
        }

        public TValue HashGet(string hashKey, TKey key)
        {
            return InternalCache.HashGet<TKey, TValue>(hashKey, key);
        }
        public bool SortedSetAdd(TKey key, TKey member, double value)
        {
            return InternalCache.SortedSetAdd<TKey>( key, member, value);
        }
        public List<object> SortedSetRangeByScore(string key, double start = double.NegativeInfinity, double stop = double.PositiveInfinity)
        {
            return InternalCache.SortedSetRangeByScore(key, start, stop);
        }
        public IBatch CreateBatch(object asyncState = null)
        {
            return InternalCache.CreateBatch(asyncState);
        }
        public bool SortedSetRemove(string key, string member)
        {
            return InternalCache.SortedSetRemove(key, member);
        }

        public void HashSet(string hashKey, TKey key, TValue value)
        {
            InternalCache.HashSet(hashKey,key.ToString(), value);
        }
        public void HashRemove(string hashkey, TKey key)
        {
            InternalCache.HashRemove(hashkey, key.ToString());
        }

        public void IncrementValueBy(TKey key, int count)
        {
            InternalCache.IncrementValueBy(key.ToString(), count);
        }

        public void DecrementValueBy(string key, int count)
        {
            InternalCache.DecrementValueBy(key.ToString(), count);
        }


    }
}
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Grpc.Service.Core.Caching
{
    /// <summary>
    /// An interface to work with cache in a typed manner.
    /// Use <see cref="CacheExtensions.AsTyped{TKey,TValue}"/> method
    /// to convert a <see cref="ICache"/> to this interface.
    /// </summary>
    /// <typeparam name="TKey">Key type for cache items</typeparam>
    /// <typeparam name="TValue">Value type for cache items</typeparam>
    public interface ITypedCache<TKey, TValue> : IDisposable
    {
        /// <summary>
        /// Unique name of the cache.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Default sliding expire time of cache items.
        /// </summary>
        TimeSpan DefaultSlidingExpireTime { get; set; }

        /// <summary>
        /// Gets the internal cache.
        /// </summary>
        ICache InternalCache { get; }

        /// <summary>
        /// Gets an item from the cache.
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="factory">Factory method to create cache item if not exists</param>
        /// <returns>Cached item</returns>
        TValue Get(TKey key, Func<TKey, TValue> factory);
        object[] StringGet(object[] keys);
        long KeyDelete(object[] keys);

        /// <summary>
        /// Gets an item from the cache.
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="factory">Factory method to create cache item if not exists</param>
        /// <returns>Cached item</returns>
        Task<TValue> GetAsync(TKey key, Func<TKey, Task<TValue>> factory);

        /// <summary>
        /// Gets an item from the cache or null if not found.
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Cached item or null if not found</returns>
        TValue GetOrDefault(TKey key);
        TValue HashGet(string hashKey, TKey key);
        bool SortedSetAdd(TKey key, TKey member, double value);
        List<object> SortedSetRangeByScore(string key, double start = double.NegativeInfinity, double stop = double.PositiveInfinity);
        IBatch CreateBatch(object asyncState = null);
        bool SortedSetRemove(string key, string member);
        /// <summary>
        /// Gets an item from the cache or null if not found.
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Cached item or null if not found</returns>
        Task<TValue> GetOrDefaultAsync(TKey key);


        /// <summary>
        /// Saves/Overrides an item in the cache by a key.
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <param name="slidingExpireTime">Sliding expire time</param>
        void Set(TKey key, TValue value, TimeSpan? slidingExpireTime = null);
        void HashSet(string hashKey, TKey key, TValue value);
        void IncrementValueBy(TKey key, int count);
        void DecrementValueBy(string key, int count);

        /// <summary>
        /// Saves/Overrides an item in the cache by a key.
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <param name="slidingExpireTime">Sliding expire time</param>
        Task SetAsync(TKey key, TValue value, TimeSpan? slidingExpireTime = null);

        /// <summary>
        /// Removes a cache item by it's key (does nothing if given key does not exists in the cache).
        /// </summary>
        /// <param name="key">Key</param>
        void Remove(TKey key);
        void HashRemove(string hashkey, TKey key);

        /// <summary>
        /// Removes a cache item by it's key.
        /// </summary>
        /// <param name="key">Key</param>
        Task RemoveAsync(TKey key);

        /// <summary>
        /// Clears all items in this cache.
        /// </summary>
        void Clear();

        /// <summary>
        /// Clears all items in this cache.
        /// </summary>
        Task ClearAsync();
        object[] ListRange(TKey key, long start = 0, long stop = -1);
        TValue ListRightPopLeftPush(TKey source, TKey destination);
        TValue ListRightPop(TKey key);
        long ListRightPush(TKey key, TValue value);
        long ListRemove(TKey key, TValue value, long count = 0);
    }
}
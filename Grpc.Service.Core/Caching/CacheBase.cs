using System;
using System.Threading.Tasks;
using Grpc.Service.Core.AsyncLock;
using System.Collections.Generic;
using StackExchange.Redis;

namespace Grpc.Service.Core.Caching
{
    /// <summary>
    /// Base class for caches.
    /// It's used to simplify implementing <see cref="ICache"/>.
    /// </summary>
    public abstract class CacheBase : ICache
    {
        public string Name { get; }

        public TimeSpan DefaultSlidingExpireTime { get; set; }

        public TimeSpan? DefaultAbsoluteExpireTime { get; set; }

        protected readonly object SyncObj = new object();

        private readonly Grpc.Service.Core.AsyncLock.AsyncLock _asyncLock = new Grpc.Service.Core.AsyncLock.AsyncLock();

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name"></param>
        protected CacheBase(string name)
        {
            Name = name;
            DefaultSlidingExpireTime = TimeSpan.FromHours(1);
        }

        public virtual object Get(string key, Func<string, object> factory)
        {
            var cacheKey = key;
            var item = GetOrDefault(key);
            if (item == null)
            {
                lock (SyncObj)
                {
                    item = GetOrDefault(key);
                    if (item == null)
                    {
                        item = factory(key);
                        if (item == null)
                        {
                            return null;
                        }

                        Set(cacheKey, item);
                    }
                }
            }

            return item;
        }

        public virtual async Task<object> GetAsync(string key, Func<string, Task<object>> factory)
        {
            var cacheKey = key;
            var item = await GetOrDefaultAsync(key);
            if (item == null)
            {
                using (await _asyncLock.LockAsync())
                {
                    item = await GetOrDefaultAsync(key);
                    if (item == null)
                    {
                        item = await factory(key);
                        if (item == null)
                        {
                            return null;
                        }

                        await SetAsync(cacheKey, item);
                    }
                }
            }

            return item;
        }
        public abstract long Publish(string channel, string message);
        public abstract void Subscribe(string channel, Action<string, string> handler);
        public abstract void Unsubscribe(string channel, Action<string, string> handler);
        public abstract object[] StringGet(string[] keys);
        public abstract long KeyDelete(string[] keys);
        public abstract object GetOrDefault(string key);
        public abstract object HashGet(string hashKey, string key);
        public abstract object[] ListRange(string key, long start = 0, long stop = -1);
        public abstract object ListRightPopLeftPush(string source, string destination);
        public abstract object ListRightPop(string key);
        public abstract long ListRightPush(string key, string value);
        public abstract long ListRemove(string key, string value, long count = 0);
        public abstract long ListLength(string key);
        public abstract long ListLeftPush(string key, string value);
        public abstract long ListLeftPush(string key, string[] value);
        public abstract object ListLeftPop(string key);
        public abstract bool SortedSetAdd(string key, string member, double value);
        public abstract List<object> SortedSetRangeByScore(string key, double start, double stop );
        public abstract IBatch CreateBatch(object asyncState = null);
        public abstract bool SortedSetRemove(string key, string member);
        public virtual Task<object> GetOrDefaultAsync(string key)
        {
            return Task.FromResult(GetOrDefault(key));
        }

        public abstract void Set(string key, object value, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null);
        public abstract void HashSet(string hashKey, string key, object value);

        public virtual Task SetAsync(string key, object value, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null)
        {
            Set(key, value, slidingExpireTime);
            return Task.FromResult(0);
        }

        public abstract void Remove(string key);
        public abstract void HashRemove(string hashkey, string key);

        public virtual Task RemoveAsync(string key)
        {
            Remove(key);
            return Task.FromResult(0);
        }
        public abstract void IncrementValueBy(string key, int count);
        public abstract void DecrementValueBy(string key, int count);

        public abstract void Clear();

        public virtual Task ClearAsync()
        {
            Clear();
            return Task.FromResult(0);
        }

        public virtual void Dispose()
        {

        }        
    }
}
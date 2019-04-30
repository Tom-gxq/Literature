using Newtonsoft.Json;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WXApiGate.Common
{
    public class RedisHelper
    {
        private string[] Redishosts;

        #region ---链接信息

        private PooledRedisClientManager prcm = null;//CreateManager(new string[] { RedisPath }, new string[] { RedisPath });

        private PooledRedisClientManager CreateManager(string[] readWriteHosts, string[] readOnlyHosts)
        {
            // 支持读写分离，均衡负载
            return new PooledRedisClientManager(readWriteHosts, readOnlyHosts, new RedisClientManagerConfig
            {
                MaxWritePoolSize = 100, // “写”链接池链接数
                MaxReadPoolSize = 100, // “读”链接池链接数
                AutoStart = true,
            });
        }

        #endregion


        public RedisHelper(string redisConfig)
        {
            Redishosts = redisConfig.Split('|');
            prcm = CreateManager(Redishosts, Redishosts);
        }


        #region -- 公共 --

        /// <summary>
        /// 判断是否存在Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ExistsKey(string key)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                return redis.ContainsKey(key.ToLower());
            }
        }

        #endregion

        #region -- Item --
        /// <summary>
        /// 设置单体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="t"></param>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        public bool Set<T>(string key, T entity)
        {

            using (IRedisClient redis = prcm.GetClient())
            {
                return redis.Set<T>(key.ToLower(), entity);
            }
        }

        /// <summary>
        /// 设置单体过期时间
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="t"></param>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        public bool Set<T>(string key, T entity, TimeSpan exDt)
        {

            using (IRedisClient redis = prcm.GetClient())
            {
                return redis.Set<T>(key.ToLower(), entity, exDt);
            }
        }

        /// <summary>
        /// 增量
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public bool IncrementValueBy(string key, int count)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                redis.IncrementValueBy(key.ToLower(), count);
                return true;
            }
        }
        /// <summary>
        /// 减量
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public bool DecrementValueBy(string key, int count)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                redis.DecrementValueBy(key.ToLower(), count);
                return true;
            }
        }

        /// <summary>
        /// 获取单体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                return redis.Get<T>(key.ToLower());
            }
        }

        /// <summary>
        /// 移除单体
        /// </summary>
        /// <param name="key"></param>
        public bool Remove(string key)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                if (redis.ContainsKey(key.ToLower()))
                {
                    return redis.Remove(key.ToLower());
                }
                return true;
            }
        }

        #endregion

        #region -- List --

        public void ListAdd<T>(string key, T t)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                var redisTypedClient = redis.As<T>();

                redisTypedClient.AddItemToList(redisTypedClient.Lists[key.ToLower()], t);
            }
        }

        public bool ListRemove<T>(string key, T t)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                var redisTypedClient = redis.As<T>();
                return redisTypedClient.RemoveItemFromList(redisTypedClient.Lists[key.ToLower()], t) > 0;
            }
        }

        public void ListRemoveAll<T>(string key)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                var redisTypedClient = redis.As<T>();
                redisTypedClient.Lists[key.ToLower()].RemoveAll();
            }
        }

        public void AddItemToList<T>(string key, T entity)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                string value = (entity is string) ? entity.ToString() : JsonConvert.SerializeObject(entity);
                redis.AddItemToList(key.ToLower(), value);
            }
        }

        public bool RemoveItemFromList<T>(string key, T entity)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                string value = (entity is string) ? entity.ToString() : JsonConvert.SerializeObject(entity);
                return redis.RemoveItemFromList(key.ToLower(), value) > 0;
            }
        }

        public long ListCount(string key)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                return redis.GetListCount(key.ToLower());
            }
        }

        public List<T> GetAllItemsFromList<T>(string key)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                var items = redis.GetAllItemsFromList(key.ToLower());
                var result = new List<T>();
                items.ForEach(item =>
                {
                    if (item != null)
                    {
                        result.Add(JsonConvert.DeserializeObject<T>(item));
                    }
                });
                return result;
            }
        }

        /// <summary>
        /// 设置缓存过期
        /// </summary>
        /// <param name="key"></param>
        /// <param name="datetime"></param>
        public void ExpireEntryAt(string key, DateTime datetime)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                redis.ExpireEntryAt(key.ToLower(), datetime);
            }
        }

        /// <summary>
        /// 设置缓存过期
        /// </summary>
        /// <param name="key"></param>
        /// <param name="datetime"></param>
        public void ExpireEntryIn(string key, TimeSpan datetime)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                redis.ExpireEntryIn(key.ToLower(), datetime);
            }
        }


        #endregion

        #region -- Set --
        public void AddItemToSet(string key, string item)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                redis.AddItemToSet(key.ToLower(), item);
            }
        }
        public void AddRangeToSet(string key, List<string> items)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                redis.AddRangeToSet(key.ToLower(), items);
            }
        }
        public bool SetContainsItem(string key, string item)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                return redis.SetContainsItem(key.ToLower(), item);
            }
        }
        public void RemoveItemFromSet(string key, string item)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                redis.RemoveItemFromSet(key.ToLower(), item);
            }
        }

        public HashSet<string> GetAllItemsFromSet(string key)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                return redis.GetAllItemsFromSet(key.ToLower());

            }

        }
        #endregion

        #region -- Hash --
        /// <summary>
        /// 判断某个数据是否已经被缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public bool HashContainsEntry(string key, string dataKey)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                return redis.HashContainsEntry(key.ToLower(), dataKey.ToLower());
            }
        }

        /// <summary>
        /// 存储数据到hash表(如果已存在，覆盖后返回是false)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public bool SetEntryInHash<T>(string key, string dataKey, T entity)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                string value = (entity is string) ? entity.ToString() : JsonConvert.SerializeObject(entity);
                return redis.SetEntryInHash(key.ToLower(), dataKey.ToLower(), value);
            }
        }

        /// <summary>
        /// 多属性添加数据，KeyValuePare<string,string>的key请确保为小写
        /// </summary>
        /// <param name="key"></param>
        /// <param name="keyValuePairs"></param>
        public void SetRangeInHash<T>(string key, IEnumerable<KeyValuePair<string, T>> keyValuePairs)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                List<KeyValuePair<string, string>> datas = new List<KeyValuePair<string, string>>();
                foreach (var item in keyValuePairs)
                {
                    var entity = item.Value;
                    string value = (entity is string) ? entity.ToString() : JsonConvert.SerializeObject(entity);
                    datas.Add(new KeyValuePair<string, string>(item.Key.ToLower(), value));
                }
                redis.SetRangeInHash(key.ToLower(), datas);
            }
        }

        /// <summary>
        /// 为哈希表 key 中的域 field 的值加上增量 increment
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="increment">增量值（可以为负数）</param>
        /// <returns></returns>
        public bool IncrementValueInHash(string key, string dataKey, int increment)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                return redis.IncrementValueInHash(key.ToLower(), dataKey, increment) != 0;
            }
        }

        /// <summary>
        /// 移除hash中的某值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public bool RemoveEntryFromHash(string key, string dataKey)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                return redis.RemoveEntryFromHash(key.ToLower(), dataKey.ToLower());
            }
        }

        /// <summary>
        /// 从hash表获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public T GetValueFromHash<T>(string key, string dataKey)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                string value = redis.GetValueFromHash(key.ToLower(), dataKey.ToLower());
                if (value != null)
                {
                    return JsonConvert.DeserializeObject<T>(value);
                }

                return default(T);
            }
        }

        public string GetStringValueFromHash(string key, string dataKey)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                string value = redis.GetValueFromHash(key.ToLower(), dataKey.ToLower());
                if (value != null)
                {
                    return value;
                }

                return string.Empty;
            }
        }


        /// <summary>
        /// 获取hash表的filed数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long GetHashCount(string key)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                return redis.GetHashCount(key.ToLower());
            }
        }
        /// <summary>
        /// 从hash表获取数据（部分）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public List<T> GetValuesFromHash<T>(string key, string[] dataKey)
        {
            List<string> lowerList = new List<string>();
            foreach (var item in dataKey)
            {
                lowerList.Add(item.ToLower());
            }
            using (IRedisClient redis = prcm.GetClient())
            {
                List<string> valueList = redis.GetValuesFromHash(key.ToLower(), lowerList.ToArray());

                List<T> rList = new List<T>();

                valueList.ForEach(value =>
                {
                    if (value != null)
                    {
                        var item = JsonConvert.DeserializeObject<T>(value);
                        if (item != null)
                        {
                            rList.Add(item);
                        }
                    }
                });

                return rList;
            }
        }

        /// <summary>
        /// 从hash表获取数据（部分）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public Dictionary<string, T> GetEntriesFromHash<T>(string key, string[] dataKey)
        {
            List<string> lowerList = new List<string>();
            foreach (var item in dataKey)
            {
                lowerList.Add(item.ToLower());
            }
            using (IRedisClient redis = prcm.GetClient())
            {
                Dictionary<string, T> dic = new Dictionary<string, T>();

                var values = redis.GetValuesFromHash(key.ToLower(), lowerList.ToArray());

                for (int i = 0; i < values.Count; i++)
                {
                    var value = values[i];

                    if (value != null)
                    {
                        dic.Add(dataKey[i], JsonConvert.DeserializeObject<T>(value));
                    }
                    else
                    {
                        dic.Add(dataKey[i], default(T));
                    }
                }
                return dic;
            }
        }

        /// <summary>
        /// 获取整个hash的数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Dictionary<string, T> GetAllEntriesFromHash<T>(string key)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                var valueDic = redis.GetAllEntriesFromHash(key.ToLower());
                var rDic = new Dictionary<string, T>();
                valueDic.ForEach((k, v) =>
                {
                    if (v != null)
                    {
                        var item = JsonConvert.DeserializeObject<T>(v);
                        if (item != null)
                            rDic.Add(k, item);
                    }
                });
                return rDic;
            }
        }
        /// <summary>
        /// 获取hash的所有value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<T> GetAllValuesFromHash<T>(string key)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                var valueDic = redis.GetHashValues(key.ToLower());
                var rList = new List<T>();

                valueDic.ForEach((v) =>
                {
                    if (v != null)
                    {
                        var item = JsonConvert.DeserializeObject<T>(v);
                        if (item != null)
                            rList.Add(item);
                    }
                });
                return rList;
            }
        }


        public IEnumerable<KeyValuePair<string, T>> ScanAllHashEntries<T>(string hashId, string pattern = null, int pageSize = 1000)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                var scanResult = redis.ScanAllHashEntries(hashId, pattern, pageSize);
                if (scanResult != null && scanResult.Count() > 0)
                {
                    var result = new List<KeyValuePair<string, T>>();

                    foreach (var item in scanResult)
                    {
                        if (item.Value == null) continue;

                        T entity = JsonConvert.DeserializeObject<T>(item.Value);
                        result.Add(new KeyValuePair<string, T>(item.Key, entity));
                    }
                    return result;
                }
                return null;
            }
        }
        #endregion

        #region -- SortedSet --
        /// <summary>
        ///  添加数据到 SortedSet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="t"></param>
        /// <param name="score"></param>
        public bool AddItemToSortedSet<T>(string key, T entity, double score)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                string value = (entity is string) ? entity.ToString() : JsonConvert.SerializeObject(entity);
                return redis.AddItemToSortedSet(key.ToLower(), value, score);
            }
        }

        /// <summary>
        /// 移除数据从SortedSet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool RemoveItemFromSortedSet<T>(string key, T entity)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                string value = (entity is string) ? entity.ToString() : JsonConvert.SerializeObject(entity);
                return redis.RemoveItemFromSortedSet(key.ToLower(), value);
            }
        }

        /// <summary>
        /// 修剪SortedSet
        /// </summary>
        /// <param name="key"></param>
        /// <param name="size">保留的条数</param>
        /// <returns></returns>
        public long RemoveRangeFromSortedSet(string key, int size)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                return redis.RemoveRangeFromSortedSet(key.ToLower(), size, 9999999);
            }
        }

        /// <summary>
        /// 获取SortedSet的长度
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long GetSortedSetCount(string key)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                return redis.GetSortedSetCount(key.ToLower());
            }
        }

        /// <summary>
        /// 获取SortedSet的分页数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<T> GetRangeFromSortedSet<T>(string key, int pageIndex, int pageSize)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                var list = redis.GetRangeFromSortedSet(key.ToLower(), (pageIndex - 1) * pageSize, pageIndex * pageSize - 1);
                if (list != null && list.Count > 0)
                {
                    List<T> result = new List<T>();
                    foreach (var item in list)
                    {
                        if (item == null) continue;

                        var data = JsonConvert.DeserializeObject<T>(item);
                        result.Add(data);
                    }
                    return result;
                }
            }
            return null;
        }


        /// <summary>
        /// 获取SortedSet的全部数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<T> GetRangeFromSortedSet<T>(string key)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                var list = redis.GetRangeFromSortedSet(key.ToLower(), 0, 9999999);
                if (list != null && list.Count > 0)
                {
                    List<T> result = new List<T>();
                    foreach (var item in list)
                    {
                        if (item == null) continue;

                        var data = JsonConvert.DeserializeObject<T>(item);
                        result.Add(data);
                    }
                    return result;
                }
            }
            return null;
        }
        #endregion

        #region --Pub/Sub--

        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="channel">频道名称</param>
        /// <param name="message">消息内容</param>
        public void PublishMessage(string channel, string message)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                redis.PublishMessage(channel, message);
            }
        }

        public void SubscribeMessage(string channel, System.Action<string, string> action)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                var sub = redis.CreateSubscription();
                sub.OnMessage = action;
                sub.SubscribeToChannels(channel);
            }
        }

        #endregion

    }
}

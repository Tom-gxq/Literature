using Grpc.Service.Core.Caching;
using Grpc.Service.Core.Dependency;
using Sms.Service.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sms.Service.SendLimit
{
    public class Limit : ILimit
    {
        private static readonly string RedisKeyPrefix = "H:S:M:M:L:"; //hash send mobile message limit;

        /// <summary>
        /// 标识元素（某个手机号或者某个IP等）
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        public SendMessageFromType FromType { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpiryDate { get; set; }

        /// <summary>
        /// 限制数量
        /// </summary>
        public int LimitCount { get; set; }

        /// <summary>
        /// 添加数量
        /// </summary>
        public int Count { get; set; }


        private string CacheKey
        {
            get
            {
                return RedisKeyPrefix + Key + ":" + FromType.ToString();
            }
        }

        /// <summary>
        /// 设置
        /// </summary>
        public void SetLimitNumber()
        {
            if (!string.IsNullOrEmpty(CacheKey))
            {
                var cache = IocManager.Instance.Resolve<ICacheManager>().GetCache<string, string>("CacheItems");               
                if (cache != null)
                {
                    object obj = cache.GetOrDefault(CacheKey);
                    if (obj != null)
                    {
                        cache.IncrementValueBy(CacheKey, Count);
                    }
                    else
                    {
                        cache.Set(CacheKey, Count.ToString(), ExpiryDate - DateTime.Now);
                    }
                    //cache.IncrementValueBy(CacheKey, Count);
                }
            }
        }

        /// <summary>
        /// 判断是否超出界限
        /// </summary>
        public bool IsAllowSend()
        {
            if (!string.IsNullOrEmpty(CacheKey))
            {
                var cache = IocManager.Instance.Resolve<ICacheManager>().GetCache<string, string>("CacheItems");
                if (cache != null)
                {
                    object obj = cache.GetOrDefault(CacheKey);

                    if (obj != null)
                    {
                        return Convert.ToInt32(obj) + Count < LimitCount;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 剩余可发送次数
        /// </summary>
        /// <returns></returns>
        public int LeftCount()
        {
            if (string.IsNullOrEmpty(CacheKey))
                return -1;
            var cache = IocManager.Instance.Resolve<ICacheManager>().GetCache<string, string>("CacheItems");
            if (cache != null)
            {
                object obj = cache.GetOrDefault(CacheKey);
                return obj == null ? -1 : LimitCount - Convert.ToInt32(obj);
            }
            else
            {
                return -1;
            }
        }
    }
}

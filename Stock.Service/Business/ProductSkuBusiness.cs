using Grpc.Service.Core.Caching;
using Grpc.Service.Core.Dependency;
using SP.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.Service.Business
{
    public class ProductSkuBusiness
    {
        private static readonly string RedisKeyPrefix = "h:p:s:m:s"; //hash Product Sku message stock;
        private static string CacheKey = "{prefix}:{account}:{productId}:{stopId}";
        public static ProductSkuResponse GetProductSku(string productId, int shopId)
        {
            var cache = IocManager.Instance.Resolve<ICacheManager>().GetCache<string, string>("CacheItems");
            if (cache != null)
            {
                object obj = cache.GetOrDefault(CacheKey);
                return obj == null ? -1 : LimitCount - Convert.ToInt32(obj);
            }
        }

        public static ProductSkuResponse GetAccountProductSku(string productId, int shopId,string accountId)
        {

        }

        public static SkuStatusResponse DecreaseProductSku(string accountId, string productId, int shopId, int stock)
        {

        }

        public static SkuStatusResponse RedoProductSku(string accountId, string productId, int shopId, int stock)
        {

        }
    }
}

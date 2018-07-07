using Google.Protobuf.Collections;
using Grpc.Service.Core.Caching;
using Grpc.Service.Core.Dependency;
using SP.Service;
using SP.Service.Domain.DomainEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.Service.Business
{
    public class ProductSkuBusiness
    {
        private static readonly string RedisKeyPrefix = "k:p:s:m:s"; //key Product Sku message stock;
        private static string CacheKey = "{0}:{1}:{2}:{3}";//{prefix}:{account}:{productId}:{stopId}
        private static readonly string InvalidKeyPrefix = "inv:p:s:m:s";
        private static string InvCacheKey = "{0}:{1}:{2}";//{prefix}:{productId}:{stopId}
        public static ProductSkuResponse GetProductSku(string productId, int shopId)
        {
            var response = new ProductSkuResponse();
            var domainList = ServiceLocator.ReportDatabase.GetAllShopOwnerList(shopId);
            var cache = IocManager.Instance.Resolve<ICacheManager>().GetCache<string, string>("CacheItems");
            if (cache != null)
            {
                string invKey = string.Format(InvCacheKey, InvalidKeyPrefix, productId, shopId);
                object invObj = cache.GetOrDefault(invKey);
                if(!string.IsNullOrEmpty(invObj?.ToString())&& Convert.ToInt32(invObj) <=0)
                {
                    var sku = new ProductSku();
                    sku.ProductId = productId;
                    sku.ShopId = shopId;
                    sku.Stock = 0;
                    response.Sku.Add(sku);
                    return response;
                }
                foreach (ShopOwnerDomain domain in domainList)
                {
                    string key = string.Format(CacheKey, RedisKeyPrefix, domain.OwnerId, productId, shopId);
                    object obj = cache.GetOrDefault(key);
                    int stock = 0;
                    try
                    {
                        stock = Convert.ToInt32(obj);
                    }
                    catch (Exception ex)
                    {
                        stock = 0;
                    }
                    var skuList = response.Sku.Find(x=>x.ProductId == productId);
                    if (skuList == null || skuList.Count() == 0)
                    {
                        var sku = new ProductSku();
                        sku.ProductId = productId;
                        sku.ShopId = shopId;
                        sku.Stock = stock;
                        response.Sku.Add(sku);
                    }
                    else
                    {
                        foreach(var item in skuList)
                        {
                            item.Stock += stock;
                        }
                    }
                }
            }
            return response;
        }

        public static ProductSkuResponse GetAccountProductSku(string productId, int shopId,string accountId)
        {
            var response = new ProductSkuResponse();           
            var cache = IocManager.Instance.Resolve<ICacheManager>().GetCache<string, string>("CacheItems");
            if (cache != null)
            {
                var sku = new ProductSku();
                string invKey = string.Format(InvCacheKey, InvalidKeyPrefix, productId, shopId);
                object invObj = cache.GetOrDefault(invKey);
                if (!string.IsNullOrEmpty(invObj?.ToString()) && Convert.ToInt32(invObj) <= 0)
                {                    
                    sku.ProductId = productId;
                    sku.ShopId = shopId;
                    sku.Stock = 0;
                    response.Sku.Add(sku);
                    return response;
                }

                string key = string.Format(CacheKey, RedisKeyPrefix, accountId, productId, shopId);
                object obj = cache.GetOrDefault(key);               
                sku.ProductId = productId;
                sku.ShopId = shopId;
                try
                {
                    var stock = Convert.ToInt32(obj);
                    sku.Stock = stock;
                }
                catch(Exception ex)
                {
                    sku.Stock = 0;
                }
                response.Sku.Add(sku);
            }
            return response;
        }

        public static SkuStatusResponse DecreaseProductSku(string accountId, string productId, int shopId, int stock)
        {
            var response = new SkuStatusResponse();
            response.Status = 10002;
            var cache = IocManager.Instance.Resolve<ICacheManager>().GetCache<string, string>("CacheItems");
            if (cache != null)
            {
                string key = string.Format(CacheKey, RedisKeyPrefix, accountId, productId, shopId);
                try
                {
                    cache.DecrementValueBy(key, stock);
                    response.Status = 10001;
                }
                catch(Exception ex)
                {
                    Console.WriteLine("DecreaseProductSku ex=" + ex.Message);
                }
            }
            return response;
        }

        public static SkuStatusResponse RedoProductSku(string accountId, string productId, int shopId, int stock)
        {
            var response = new SkuStatusResponse();
            response.Status = 10002;
            var cache = IocManager.Instance.Resolve<ICacheManager>().GetCache<string, string>("CacheItems");
            if (cache != null)
            {
                string key = string.Format(CacheKey, RedisKeyPrefix, accountId, productId, shopId);
                try
                {
                    cache.IncrementValueBy(key, stock);
                    response.Status = 10001;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("RedoProductSku ex=" + ex.Message);
                }
            }
            return response;
        }

        public static SkuStatusResponse AddInvProductSku(string productId, int shopId)
        {
            var response = new SkuStatusResponse();
            response.Status = 10002;
            var cache = IocManager.Instance.Resolve<ICacheManager>().GetCache<string, string>("CacheItems");
            if (cache != null)
            {
                string key = string.Format(InvCacheKey, InvalidKeyPrefix, productId, shopId);
                try
                {
                    cache.Set(key, "0");
                    response.Status = 10001;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("AddInvProductSku ex=" + ex.Message);
                }
            }
            return response;
        }
        public static SkuStatusResponse DelInvProductSku(string productId, int shopId)
        {
            var response = new SkuStatusResponse();
            response.Status = 10002;
            var cache = IocManager.Instance.Resolve<ICacheManager>().GetCache<string, string>("CacheItems");
            if (cache != null)
            {
                string key = string.Format(InvCacheKey, InvalidKeyPrefix, productId, shopId);
                try
                {
                    cache.Remove(key);
                    response.Status = 10001;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("DelInvProductSku ex="+ ex.Message);
                }
            }
            return response;
        }

        public static SkuStatusResponse UpdateProductSku(RepeatedField<SP.Service.ProductSku> skuList,long timeSpan)
        {
            var response = new SkuStatusResponse();
            response.Status = 10002;
            var cache = IocManager.Instance.Resolve<ICacheManager>().GetCache<string, string>("CacheItems");
            if (cache != null)
            {
                int i = 0;
                foreach (var item in skuList)
                {
                    string key = string.Format(CacheKey, RedisKeyPrefix, item.AccountId, item.ProductId, item.ShopId);
                    try
                    {
                        cache.Remove(key);
                        Console.WriteLine($"UpdateProductSku[{i}] key={key} Stock=" + item.Stock.ToString());
                        if (timeSpan > 0)
                        {
                            TimeSpan time = new TimeSpan(timeSpan);
                            cache.Set(key, item.Stock.ToString(),time);
                        }
                        else
                        {
                            cache.Set(key, item.Stock.ToString());
                        }
                        response.Status = 10001;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"UpdateProductSku[{i}] ex=" + ex.Message);
                    }
                    i++;
                }
            }
            return response;
        }

        public static SkuStatusResponse DelProductSku(RepeatedField<SP.Service.ProductSku> skuList)
        {
            var response = new SkuStatusResponse();
            response.Status = 10002;
            var cache = IocManager.Instance.Resolve<ICacheManager>().GetCache<string, string>("CacheItems");
            if (cache != null)
            {
                int i = 0;
                foreach (var item in skuList)
                {
                    string key = string.Format(CacheKey, RedisKeyPrefix, item.AccountId, item.ProductId, item.ShopId);
                    try
                    {
                        cache.Remove(key);
                        response.Status = 10001;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"DelProductSku[{i}] ex=" + ex.Message);
                    }
                    i++;
                }
            }
            return response;
        }
    }
}

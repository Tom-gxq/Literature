﻿using Google.Protobuf.Collections;
using Grpc.Service.Core.Caching;
using Grpc.Service.Core.Dependency;
using SP.Service;
using SP.Service.Domain.Commands.Product;
using SP.Service.Domain.Commands.StockShip;
using SP.Service.Domain.DomainEntity;
using System;
using System.Collections.Generic;
using System.Linq;
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
            response.Status = 10002;
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
                    response.Status = 10001;
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
                response.Status = 10001;
            }
            return response;
        }

        public static ProductSkuResponse GetAccountProductSku(string productId, int shopId,string accountId)
        {
            var response = new ProductSkuResponse();
            response.Status = 10002;
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
                response.Status = 10001;
            }
            return response;
        }

        public static SkuStatusResponse DecreaseProductSku(string orderId, string accountId, string productId, int shopId, int stock)
        {
            var response = new SkuStatusResponse();
            response.Status = 10002;
            var domainList = ServiceLocator.ReportDatabase.GetAllShopOwnerList(shopId);
            var cache = IocManager.Instance.Resolve<ICacheManager>().GetCache<string, string>("CacheItems");
            if (cache != null)
            {
                foreach (ShopOwnerDomain domain in domainList)
                {
                    string stockKey = string.Format(CacheKey, RedisKeyPrefix, domain.OwnerId, productId, shopId);
                    object obj = cache.GetOrDefault(stockKey);                    
                    try
                    {
                        domain.Stock = Convert.ToInt32(obj);
                    }
                    catch (Exception ex)
                    {
                        domain.Stock = 0;
                    }
                }
                var list = domainList.OrderByDescending(x=>x.Stock).ToList();
                foreach (var item in list)
                {
                    var decStock = 0;
                    if(item.Stock <=0)
                    {
                        continue;
                    }
                    if(item.Stock >= stock)
                    {
                        decStock = stock;
                        stock = 0;
                    }
                    else
                    {
                        decStock = item.Stock;
                        stock = stock - decStock;
                    }
                    string key = string.Format(CacheKey, RedisKeyPrefix, item.OwnerId, productId, shopId);
                    try
                    {
                        cache.DecrementValueBy(key, decStock);
                        response.Status = 10001;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("DecreaseProductSku ex=" + ex.Message);
                    }
                    ServiceLocator.CommandBus.Send(new CreatShipOrderCommand(orderId, item.OwnerId, accountId, DateTime.Now, decStock, productId,shopId));
                    if (stock == 0)
                    {
                        break;
                    }
                }
                if(stock > 0 && list.Count>0)
                {
                    var decStock = 0 - stock;
                    string key = string.Format(CacheKey, RedisKeyPrefix, list[0].OwnerId, productId, shopId);
                    try
                    {
                        cache.DecrementValueBy(key, Math.Abs(decStock));
                        response.Status = 10001;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("stock > 0 DecreaseProductSku ex=" + ex.Message);
                    }
                    ServiceLocator.CommandBus.Send(new CreatShipOrderCommand(orderId, list[0].OwnerId, accountId, DateTime.Now, decStock, productId, shopId));
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
                        Console.WriteLine($"UpdateProductSku[{i}] key={key} Stock=" + item.Stock.ToString()+" Type="+ item.Type);
                        object obj = cache.GetOrDefault(key);
                        bool isExsit = !string.IsNullOrEmpty(obj?.ToString());
                        TimeSpan time = TimeSpan.FromDays(365);
                        if (timeSpan > 0)
                        {
                            time = new TimeSpan(timeSpan);
                        }
                        switch (item.Type)
                        {
                            case 0://覆盖
                                cache.Remove(key);                                
                                
                                cache.Set(key, item.Stock.ToString(), time);
                                break;
                            case 1://增加
                                if (isExsit)
                                {
                                    cache.IncrementValueBy(key, item.Stock);
                                }
                                else
                                {
                                    cache.Remove(key);
                                    cache.Set(key, item.Stock.ToString(), time);
                                }
                                break;
                            case 2://减少
                                if (isExsit)
                                {
                                    cache.DecrementValueBy(key, item.Stock);
                                }
                                else
                                {
                                    cache.Remove(key);
                                    cache.Set(key, item.Stock.ToString(), time);
                                }
                                break;
                        }
                        response.Status = 10001;
                        ServiceLocator.CommandBus.Send(new EditProductSkuCommand(Guid.NewGuid(),item.AccountId, item.ProductId, item.ShopId,item.Stock,item.Type));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"UpdateProductSku[{i}] ex=" + ex.Message);
                        Console.WriteLine($"UpdateProductSku[{i}] ex=" + ex.StackTrace);
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
                    Console.WriteLine($"DelProductSku[{i}] key={key} Stock=" + item.Stock.ToString());
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

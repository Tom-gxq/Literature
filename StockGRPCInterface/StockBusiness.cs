using SP.Api.Model.Product;
using SP.Service;
using System;
using System.Collections.Generic;

namespace StockGRPCInterface
{
    public class StockBusiness
    {
        public static ProductSkuResponse GetProductSku(string host,string productId,int shopId)
        {
            var helper = new StockClientHelper(host);
            var client = helper.GetClient();
            var request1 = new ProductSkuRequest()
            {
                ProductId = productId,
                ShopId = shopId
            };
            var reuslt = client.GetProductSku(request1);
            helper.Dispose();
            return reuslt;
        }

        public static ProductSkuResponse GetAccountProductSku(string host, string accountId, string productId, int shopId)
        {
            var helper = new StockClientHelper(host);
            var client = helper.GetClient();
            var request1 = new AccountProductSkuRequest()
            {
                AccountId = accountId,
                ProductId = productId,
                ShopId = shopId
            };
            var reuslt = client.GetAccountProductSku(request1);
            helper.Dispose();
            return reuslt;
        }

        public static SkuStatusResponse DecreaseProductSku(string host, string orderId, string accountId, string productId, int shopId,int stock)
        {
            var helper = new StockClientHelper(host);
            var client = helper.GetClient();
            var request1 = new OperationSkuRequest()
            {
                AccountId = accountId,
                ProductId = productId,
                ShopId = shopId,
                Stock = stock,
                OrderId = orderId
            };
            var reuslt = client.DecreaseProductSku(request1);
            helper.Dispose();
            return reuslt;
        }

        public static SkuStatusResponse RedoProductSku(string host, string accountId, string productId, int shopId, int stock)
        {
            var helper = new StockClientHelper(host);
            var client = helper.GetClient();
            var request1 = new OperationSkuRequest()
            {
                AccountId = accountId,
                ProductId = productId,
                ShopId = shopId,
                Stock = stock,
            };
            var reuslt = client.RedoProductSku(request1);
            helper.Dispose();
            return reuslt;
        }

        public static SkuStatusResponse UpdateProductSku(string host, List<ProductSkuModel> list)
        {
            var helper = new StockClientHelper(host);
            var client = helper.GetClient();
            var request = new SkuListRequest();
            foreach (var model in list)
            {
                ProductSku sku = new ProductSku();
                sku.AccountId = model.accountId ?? string.Empty;
                sku.ProductId = model.productId;
                sku.ShopId = model.shopId;
                sku.SkuId = model.skuId ?? string.Empty;
                sku.Stock = model.stock;
                request.Sku.Add(sku);
            }
            var result = client.UpdateProductSku(request);
            helper.Dispose();
            return result;
        }

        public static SkuStatusResponse DelProductSku(string host, List<ProductSkuModel> list)
        {
            var helper = new StockClientHelper(host);
            var client = helper.GetClient();
            var request = new SkuListRequest();
            foreach (var model in list)
            {
                ProductSku sku = new ProductSku();
                sku.AccountId = model.accountId;
                sku.ProductId = model.productId;
                sku.ShopId = model.shopId;
                request.Sku.Add(sku);
            }
            var result = client.DelProductSku(request);
            helper.Dispose();
            return result;
        }
    }
}

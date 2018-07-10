using SP.Api.Model.Product;
using SP.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockGRPCInterface
{
    public class ServerStockBusiness
    {
        public static bool AddInvProductSku(string productId, int shopId)
        {
            bool ret = false;
            var client = ServerClientHelper.GetClient();
            var request = new InvSkuRequest()
            {
                ProductId = productId,
                ShopId = shopId
            };
            var result = client.AddInvProductSku(request);
            if (result.Status == 10001)
            {
                ret = true;
            }
            return ret;
        }
        public static bool DelInvProductSku(string productId, int shopId)
        {
            bool ret = false;
            var client = ServerClientHelper.GetClient();
            var request = new InvSkuRequest()
            {
                ProductId = productId,
                ShopId = shopId
            };
            var result = client.DelInvProductSku(request);
            if (result.Status == 10001)
            {
                ret = true;
            }
            return ret;
        }
        public static bool UpdateProductSku(List<ProductSkuModel> list)
        {
            bool ret = false;
            var client = ServerClientHelper.GetClient();
            var request = new SkuListRequest();
            foreach (var model in list)
            {
                ProductSku sku = new ProductSku();
                sku.AccountId = model.accountId??string.Empty;
                sku.ProductId = model.productId;
                sku.ShopId = model.shopId;
                sku.SkuId = model.skuId ?? string.Empty;
                sku.Stock = model.stock;
                request.Sku.Add(sku);
            }
            var result = client.UpdateProductSku(request);
            if (result.Status == 10001)
            {
                ret = true;
            }
            return ret;
        }

        public static bool DelProductSku(List<ProductSkuModel> list)
        {
            bool ret = false;
            var client = ServerClientHelper.GetClient();
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
            var result = client.DelProductSku(request);
            if (result.Status == 10001)
            {
                ret = true;
            }
            return ret;
        }
    }
}

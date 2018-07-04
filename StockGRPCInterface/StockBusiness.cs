using SP.Service;
using System;

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

        public static SkuStatusResponse DecreaseProductSku(string host, string accountId, string productId, int shopId,int stock)
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
    }
}

using FluentScheduler;
using SP.Api.Model.Product;
using StockGRPCInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Timer.Service
{
    public class StockRegistry: Registry
    {
        private string StockSvrHost { get; set; }
        private int ShopType { get; set; }
        public StockRegistry(string stockSvrHost, int foodId)
        {
            this.StockSvrHost = stockSvrHost;
            this.ShopType = foodId;
            Schedule(() => ClearDayStock()).ToRunEvery(1).Days().At(1, 0);
        }
        
        private void ClearDayStock()
        {
            var list = ServiceLocator.ReportDatabase.GetCurrentProductSku(this.ShopType);
            List<ProductSkuModel> skulist = new List<ProductSkuModel>();
            foreach (var item in list)
            {
                ProductSkuModel model = new ProductSkuModel();
                model.accountId = item.AccountId;
                model.productId = item.ProductId;
                model.shopId = item.ShopId;
                skulist.Add(model);
            }
            StockBusiness.DelProductSku(this.StockSvrHost, skulist);
        }
    }
}

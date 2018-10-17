using FluentScheduler;
using Grpc.Service.Core.Caching;
using Grpc.Service.Core.Dependency;
using SP.Api.Model.Product;
using SP.Service.Domain.Commands.Product;
using SP.Service.Domain.Commands.StockShip;
using StockGRPCInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Timer.Service
{
    public class StockRegistry: Registry
    {
        private string StockSvrHost { get; set; }
        private int ShopType { get; set; }
        private int ClearDayStockTime { get; set; }
        private int AutoSettingStockTime { get; set; }
        private static readonly string OwnerListKey = "o:l:{0}:{1}";//{prefix}:{stopId}:{productId}
        public StockRegistry(string stockSvrHost, int foodId,int clearDayStockTime,int autoSettingStockTime)
        {
            this.StockSvrHost = stockSvrHost;
            this.ShopType = foodId;
            this.ClearDayStockTime = clearDayStockTime;
            this.AutoSettingStockTime = autoSettingStockTime;
            Schedule(() => ClearDayStock()).ToRunEvery(1).Days().At(this.ClearDayStockTime, 0);
            Schedule(() => AutoCheckShopOwner()).ToRunEvery(1).Days().At(this.ClearDayStockTime, 50);
            Schedule(() => AutoSettingAccountStock()).ToRunEvery(1).Days().At(this.AutoSettingStockTime, 0);
            System.Console.WriteLine($"AutoSettingStock Run..... " +
                $"StockSvrHost=[{this.StockSvrHost}] ShopType=[{this.ShopType}] ClearDayStockTime=[{this.ClearDayStockTime}] AutoSettingStockTime=[{this.AutoSettingStockTime}]");
        }
        
        private void ClearDayStock()
        {
            System.Console.WriteLine("ClearDayStock Run.....");
            var list = ServiceLocator.ReportDatabase.GetCurrentProductSku(this.ShopType);
            List<ProductSkuModel> skulist = new List<ProductSkuModel>();
            foreach (var item in list)
            {
                ProductSkuModel model = new ProductSkuModel();
                model.accountId = item.AccountId;
                model.productId = item.ProductId;
                model.shopId = item.ShopId;
                try
                {
                    var response = StockBusiness.GetAccountProductSku(this.StockSvrHost, model.accountId, model.productId, model.shopId);
                    if (response != null && response.Sku.Count > 0)
                    {
                        var stock = response.Sku[0].Stock;
                        ServiceLocator.CommandBus.Send(new EditResidueSkuCommand(Guid.NewGuid(), item.AccountId, item.ProductId,
                           item.ShopId, stock));
                    }
                }
                catch(Exception ex)
                {
                    System.Console.WriteLine($"ex ={ex.Message} \n stack={ex.StackTrace}");
                }
                System.Console.WriteLine($"AccountId={model.accountId},ProductId={model.productId},ShopId={model.shopId}");
                skulist.Add(model);
            }
            var result = StockBusiness.DelProductSku(this.StockSvrHost, skulist);
            if(result.Status == 10001)
            {
                System.Console.WriteLine("DelProductSku Success");
            }
            else
            {
                System.Console.WriteLine("DelProductSku Error");
            }
        }

        private void AutoSettingAccountStock()
        {
            System.Console.WriteLine("AutoSettingAccountStock Run.....");
            var list = ServiceLocator.AccountProductReportDatabase.GetAllFoodAccountProduct();
            List<ProductSkuModel> skulist = new List<ProductSkuModel>();
            foreach(var item in list)
            {
                ProductSkuModel model = new ProductSkuModel();
                model.accountId = item.AccountId;
                model.productId = item.ProductId;
                model.shopId = item.ShopId != null ? item.ShopId.Value : 0;
                model.stock = item.PreStock != null ? item.PreStock.Value : 0;
                model.type = 0;
                System.Console.WriteLine($"AccountId={model.accountId},ProductId={model.productId},ShopId={model.shopId},PreStock={model.stock}");
                skulist.Add(model);
            }
            var result = StockBusiness.UpdateProductSku(this.StockSvrHost, skulist);
            if (result.Status == 10001)
            {
                System.Console.WriteLine("UpdateProductSku Success");
                foreach (var item in list)
                {
                    ServiceLocator.CommandBus.Send(new CreateProductSkuDBCommand(Guid.NewGuid(), item.AccountId, item.ProductId, 
                        (item.ShopId != null ? item.ShopId.Value : 0), (item.PreStock != null ? item.PreStock.Value : 0)));
                }
            }
            else
            {
                System.Console.WriteLine("UpdateProductSku Error");
            }
        }

        private void AutoCheckShopOwner()
        {
            System.Console.WriteLine("AutoCheckShopOwner Run.....");
            var list = ServiceLocator.AccountProductReportDatabase.GetAllAccountProduct();
            try
            {
                var cache = IocManager.Instance.Resolve<ICacheManager>().GetCache<string, string>("CacheItems");
                if (cache != null)
                {
                    foreach (var item in list)
                    {
                        string ownerKey = string.Format(OwnerListKey, item.ShopId, item.ProductId);
                        try
                        {
                            var ownerList = cache.ListRange(ownerKey)?.ToList();
                            if (ownerList != null && !ownerList.Contains(item.AccountId))
                            {
                                cache.ListRightPush(ownerKey, item.AccountId);
                            }
                        }
                        catch(Exception ex)
                        {
                            System.Console.WriteLine($"ex ={ex.Message} \n stack={ex.StackTrace}");
                        }
                        System.Console.WriteLine($"ownerKey:{ownerKey}");
                    }
                }
            }
            catch(Exception ex)
            {
                System.Console.WriteLine($"ex ={ex.Message} \n stack={ex.StackTrace}");
            }
            System.Console.WriteLine("AutoCheckShopOwner Success");
        }
    }
}

using Grpc.Service.Core.Domain.Handlers;
using SP.Service.Domain.Events;
using SP.Service.Domain.Reporting;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.EventHandlers
{
    public class ProductSkuOrderNumEventHandler : IEventHandler<ProductSkuOrderNumEvent>, IEventHandler<ProductSkuDBUpdateEvent>
    {
        private readonly ProductSkuReportDatabase _reportDatabase;
        private static object lockObj = new object();
        public ProductSkuOrderNumEventHandler(ProductSkuReportDatabase reportDatabase)
        {
            _reportDatabase = reportDatabase;
        }
        public void Handle(ProductSkuOrderNumEvent handle)
        {
            lock (lockObj)
            {
                var domaint = _reportDatabase.GetProductSku(handle.ShopId, handle.ProductId, handle.AccountId);
                if (domaint != null && !string.IsNullOrEmpty(domaint.SkuId))
                {
                    var result = _reportDatabase.UpdateProductSkuOrderNum(new Entity.ProductSkuEntity()
                    {
                        SkuId = domaint.SkuId,
                        OrderNum = domaint.OrderNum + handle.OrderNum
                    });
                }
            }
        }
        public void Handle(ProductSkuDBUpdateEvent handle)
        {
            lock (lockObj)
            {
                var domaint = _reportDatabase.GetProductSku(handle.ShopId, handle.ProductId, handle.AccountId);
                if (domaint != null && !string.IsNullOrEmpty(domaint.SkuId))
                {
                    var entity = new Entity.ProductSkuEntity()
                    {
                        SkuId = domaint.SkuId,
                    };
                    switch(handle.Type )
                    {
                        case 0://覆盖
                            entity.Stock = handle.Stock;
                            break;
                        case 1://增加
                            entity.Stock = domaint.Stock + handle.Stock;
                            break;
                        case 2://减少
                            entity.Stock = domaint.Stock - handle.Stock;
                            break;
                    }
                    var result = _reportDatabase.UpdateProductSkuOrderNum(entity);
                }
                else
                {
                    _reportDatabase.AddProductSku(new Entity.ProductSkuEntity()
                    {
                         ProductId = handle.ProductId,
                         AccountId = handle.AccountId,
                         ShopId = handle.ShopId,
                         Stock = handle.Stock,
                         SkuId = Guid.NewGuid().ToString(),
                         EffectiveTime = DateTime.Now,
                         AlertStock = 0,
                         Price = 0,
                         SKU = string.Empty,
                         OrderNum = 0
                    });
                }
            }
        }
    }
}

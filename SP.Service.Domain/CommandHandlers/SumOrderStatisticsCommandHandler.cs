using Grpc.Service.Core.Dependency;
using Grpc.Service.Core.Domain.Handlers;
using Grpc.Service.Core.Domain.Storage;
using Microsoft.Extensions.Configuration;
using SP.Service.Domain.Commands.Statistics;
using SP.Service.Domain.DomainEntity;
using SP.Service.Domain.Reporting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.CommandHandlers
{
    public class SumOrderStatisticsCommandHandler : ICommandHandler<SumOrderStatisticsCommand>
    {
        private IDataRepository<OrderStatisticsDomain> _repository;
        private IDataRepository<SysStatisticsDomain> _sysRepository;
        private OrderStatisticsReportDatabase _statisticsReportDatabase;
        private SysStatisticsReportDatabase _sysStatisticsReportDatabase;
        private ShipOrderReportDatabase _shipReportDatabase;
        private ShopReportDatabase _shopReportDatabase;
        private ShoppingCartReportDatabase _shoppingCartReportDatabase;

        public SumOrderStatisticsCommandHandler(IDataRepository<OrderStatisticsDomain> repository, IDataRepository<SysStatisticsDomain> sysRepository,
            OrderStatisticsReportDatabase statisticsReportDatabase,
            SysStatisticsReportDatabase sysStatisticsReportDatabase, 
            ShipOrderReportDatabase shipReportDatabase, ShopReportDatabase shopReportDatabase,
            ShoppingCartReportDatabase shoppingCartReportDatabase)
        {
            this._repository = repository;
            this._sysRepository = sysRepository;
            this._statisticsReportDatabase = statisticsReportDatabase;
            this._sysStatisticsReportDatabase = sysStatisticsReportDatabase;
            this._shipReportDatabase = shipReportDatabase;
            this._shopReportDatabase = shopReportDatabase;
            this._shoppingCartReportDatabase = shoppingCartReportDatabase;
        }

        public void Execute(SumOrderStatisticsCommand command)
        {
            OrderStatisticsDomain aggregate = null;
            double foodAmount = 0;
            double markAmount = 0;
            var config = IocManager.Instance.Resolve<IConfigurationRoot>();
            int marketId = -1;
            if (config != null)
            {
                var reObj = config.GetSection("MarketId");
                int.TryParse(reObj?.Value, out marketId);
            }

            var list = _shipReportDatabase.GetShippingOrdersByOrderId(command.OrderId);
            foreach(var ship in list)
            {
                if (ship.ShopId != null && ship.ShopId.Value>0)
                {
                    var shopCart = _shoppingCartReportDatabase.GetShoppingCartByOrderIdandProductId(ship.OrderId, ship.ProductId);
                    var shop = _shopReportDatabase.GetShopById(ship.ShopId.Value);
                    if (shop != null && shopCart != null)
                    {
                        var amount = command.IsVip ? shopCart.Product.VIPPrice.Value : shopCart.Product.MarketPrice.Value;
                        //购买商品的数量
                        amount = amount * (ship.Stock != null ? ship.Stock.Value : 1);
                        if (marketId == (shop?.ShopType ?? marketId))
                        {
                            //超市产品计算方法                        
                            markAmount += amount;
                        }
                        else
                        {
                            //餐饮产品计算方法
                            foodAmount += amount;
                        }
                    }
                }
            }
            var entity = _statisticsReportDatabase.GetTodayOrderStatistic(command.AccountId, command.AddressId, command.OrderDate);
            if (entity != null && !string.IsNullOrEmpty(entity.AccountId))
            {
                aggregate = new OrderStatisticsDomain();
                aggregate.SumOrderStatistics(command.Id,command.OrderId, command.OrderCode, command.OrderDate,
                    command.AccountId, foodAmount, markAmount, command.AddressId);
            }
            else
            {
                aggregate = new OrderStatisticsDomain(command.Id,command.OrderId, command.OrderCode, command.OrderDate,
                    command.AccountId, foodAmount, markAmount, command.AddressId);
            }
            if (aggregate != null)
            {
                _repository.Save(aggregate);
            }

            SysStatisticsDomain sysStatisticsDomain = null;
            var sysEntity = _sysStatisticsReportDatabase.GetTodaySysStatistic(command.OrderDate);
            if (sysEntity != null && sysEntity.CreateTime >DateTime.MinValue)
            {
                sysStatisticsDomain = new SysStatisticsDomain();
                sysStatisticsDomain.SumOrderStatistics(command.Id,command.OrderId, command.OrderCode, command.OrderDate,
                    command.AccountId, foodAmount, markAmount, command.AddressId);
            }
            else
            {
                sysStatisticsDomain = new SysStatisticsDomain(command.Id,command.OrderDate, 0, 0,0,1, foodAmount, markAmount);
            }
            if (sysStatisticsDomain != null)
            {
                _sysRepository.Save(sysStatisticsDomain);
            }

        }
    }
}

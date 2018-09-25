using Grpc.Service.Core.Domain.Handlers;
using SP.Service.Domain.Events;
using SP.Service.Domain.Reporting;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.EventHandlers
{
    public class SellerStatisticsHandler: IEventHandler<SellerStatisticsEvent>, IEventHandler<SellerStatisticsTradeEvent>,
        IEventHandler<SellerStatisticsSumOrderEvent>
    {
        private readonly SellerStatisticsReportDatabase _reportDatabase;
        public SellerStatisticsHandler(SellerStatisticsReportDatabase reportDatabase)
        {
            _reportDatabase = reportDatabase;
        }

        public void Handle(SellerStatisticsEvent handle)
        {            
            var item = new SellerStatisticsEntity()
            {
                SSID = handle.AggregateId.ToString(),
                Num_OrderAmount = handle.OrderAmount,
                Num_NewOrder = 1,
                AccountId = handle.ShippingId,
                CreateTime = handle.CreateTime,
            };

            _reportDatabase.Add(item);
        }
        public void Handle(SellerStatisticsTradeEvent handle)
        {
            _reportDatabase.AddTrade(new SellerStatisticsTradeEntity()
            {
                 SSID = handle.SSID,
                 OrderId = handle.OrderId,
                 ShipTo = handle.ShipTo,
                 CreateTime = DateTime.Now,
            });
        }
        public void Handle(SellerStatisticsSumOrderEvent handle)
        {
            Console.WriteLine($"SellerStatisticsSumOrderEvent SSID={handle.SSID}");
            var result = _reportDatabase.UpdateOrderStatistic(handle.SSID, handle.OrderAmount);
        }
    }
}

using Grpc.Service.Core.Domain.Handlers;
using SP.Service.Domain.Events;
using SP.Service.Domain.Reporting;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.EventHandlers
{
    public class OrderStatisticsEventHandler : IEventHandler<OrderStatisticsSumEvent>, IEventHandler<OrderStatisticsCreateEvent>
    {
        private readonly OrderStatisticsReportDatabase _reportDatabase;
        public OrderStatisticsEventHandler(OrderStatisticsReportDatabase reportDatabase)
        {
            _reportDatabase = reportDatabase;
        }

        public void Handle(OrderStatisticsCreateEvent handle)
        {
            var item = new ShipStatisticsEntity()
            {
                AccountId = handle.AccountId,
                Num_FoodOrderAmount = handle.FoodAmount,
                Num_MarkOrderAmount = handle.MarkAmount,
                CreateTime = handle.OrderDate,
                DormId = handle.AddressId,
                Num_NewOrder = 1,                
            };

            _reportDatabase.Add(item);
        }
        public void Handle(OrderStatisticsSumEvent handle)
        {
            _reportDatabase.UpdateOrderStatistic(handle.AccountId, handle.AddressId, 
                handle.OrderDate, handle.FoodAmount,handle.MarkAmount);
        }
    }
}

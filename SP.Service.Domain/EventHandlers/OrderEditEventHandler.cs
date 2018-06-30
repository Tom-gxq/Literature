using Grpc.Service.Core.Domain.Handlers;
using SP.Service.Domain.Events;
using SP.Service.Domain.Reporting;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.EventHandlers
{
    public class OrderEditEventHandler : IEventHandler<OrderEditEvent>, IEventHandler<OrderSubAmountEvent>
    {
        private readonly OrderReportDatabase _reportDatabase;
        public OrderEditEventHandler(OrderReportDatabase reportDatabase)
        {
            _reportDatabase = reportDatabase;
        }
        public void Handle(OrderEditEvent handle)
        {
            var item = new OrdersEntity()
            {
                OrderId = handle.AggregateId.ToString(),
                OrderStatus = (int)handle.OrderStatus, 
                UpdateTime = DateTime.Now,
            };
            switch(handle.OrderStatus )
            {
                case Data.Enum.OrderStatus.Payed:
                    item.PayDate = DateTime.Now;
                    item.IsAliPay = (handle.PayWay == Data.Enum.OrderPay.AliPay);
                    item.IsWxPay = (handle.PayWay == Data.Enum.OrderPay.WxPay);
                    break;
                case Data.Enum.OrderStatus.Success:
                    item.ShipToDate = DateTime.Now;
                    item.FinishDate = DateTime.Now;
                    break;
            }

            _reportDatabase.UpdateOrderStatus(item);
        }
        public void Handle(OrderSubAmountEvent handle)
        {
            var item = new OrdersEntity()
            {
                OrderId = handle.AggregateId.ToString(),
                Amount = handle.Amount,
                VIPAmount = handle.VipAmount,
                UpdateTime = DateTime.Now,
            };

            _reportDatabase.UpdateOrderStatus(item);
        }
    }
}

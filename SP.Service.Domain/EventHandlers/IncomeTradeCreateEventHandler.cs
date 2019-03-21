using Grpc.Service.Core.Domain.Handlers;
using SP.Service.Domain.Events;
using SP.Service.Domain.Reporting;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.EventHandlers
{
    public class IncomeTradeCreateEventHandler : IEventHandler<IncomeTradeCreateEvent>
    {
        private readonly TradeReportDatabase _reportDatabase;
        public IncomeTradeCreateEventHandler(TradeReportDatabase reportDatabase)
        {
            _reportDatabase = reportDatabase;
        }
        public void Handle(IncomeTradeCreateEvent handle)
        {
            var item = new ComTradeEntity()
            {
                TradeId = Guid.NewGuid().ToString(),
                AccountId = handle.AccountId,
                Amount = handle.Amount,
                Subject = handle.Subject,
                CreateTime = DateTime.Now,
                ShipOrderId = handle.ShipOrderId,
                BalanceAmount = handle.BalanceAmount,
                ProductId = handle.ProductId,
                TradeNo = handle.TradeNo,
            };

            _reportDatabase.Add(item);
        }
    }
}

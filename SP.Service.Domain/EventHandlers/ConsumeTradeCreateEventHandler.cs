using Grpc.Service.Core.Domain.Handlers;
using SP.Service.Domain.Events;
using SP.Service.Domain.Reporting;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.EventHandlers
{
    public class ConsumeTradeCreateEventHandler : IEventHandler<ConsumeTradeCreateEvent>
    {
        private readonly TradeReportDatabase _reportDatabase;
        public ConsumeTradeCreateEventHandler(TradeReportDatabase reportDatabase)
        {
            _reportDatabase = reportDatabase;
        }
        public void Handle(ConsumeTradeCreateEvent handle)
        {
            var item = new ConsumeTradeEntity()
            {
                TradeId = Guid.NewGuid().ToString(),
                AccountId = handle.AccountId,
                Amount = handle.Amount,
                CreateTime = DateTime.Now,
                BalanceAmount = handle.BalanceAmount,
                TradeNo = handle.TradeNo,
                CheckCode = handle.CheckCode,
                OrderId = handle.OrderId,                
            };

            _reportDatabase.Add(item);
        }
    }
}

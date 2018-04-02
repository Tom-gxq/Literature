using Grpc.Service.Core.Domain.Handlers;
using SP.Service.Domain.Events;
using SP.Service.Domain.Reporting;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.EventHandlers
{
    public class TradeCreateEventHandler : IEventHandler<TradeCreateEvent>
    {
        private readonly TradeReportDatabase _reportDatabase;
        public TradeCreateEventHandler(TradeReportDatabase reportDatabase)
        {
            _reportDatabase = reportDatabase;
        }
        public void Handle(TradeCreateEvent handle)
        {
            var item = new TradeEntity()
            {
                TradeId = Guid.NewGuid().ToString(),
                AccountId = handle.AccountId,
                Amount  = handle.Amount,
                CartId  = handle.CartId,
                Subject = handle.Subject,
                CreateTime = DateTime.Now,
            };

            _reportDatabase.Add(item);
        }
    }
}

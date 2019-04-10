using Grpc.Service.Core.Domain.Handlers;
using SP.Service.Domain.Events;
using SP.Service.Domain.Reporting;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.EventHandlers
{
    public class CouponEventHandler: IEventHandler<CouponCreatedEvent>, IEventHandler<CouponPayedEvent>, IEventHandler<CouponUsedEvent>
    {
        private readonly CouponsReportDatabase _reportDatabase;
        public CouponEventHandler(CouponsReportDatabase reportDatabase)
        {
            _reportDatabase = reportDatabase;
        }
        public void Handle(CouponCreatedEvent handle)
        {
            var item = new CouponsEntity()
            {
                CouponId = handle.AggregateId.ToString(),
                AccountId = handle.AccountId,
                KindId = handle.KindId,
                EndDate = handle.EndDate,
                StartDate = handle.StartDate,    
                Status = 1,
                PayStatus = 0
            };
            _reportDatabase.Add(item);
        }

        public void Handle(CouponPayedEvent handle)
        {
            var item = new CouponsEntity()
            {
                CouponId = handle.AggregateId.ToString(),
                PayType = handle.PayType,
                PayAmount = handle.PayAmount,
                PayStatus = 1
            };
            _reportDatabase.Update(item);
        }

        public void Handle(CouponUsedEvent handle)
        {
            var item = new CouponsEntity()
            {
                CouponId = handle.AggregateId.ToString(),
                Status = handle.Status
            };
            _reportDatabase.Update(item);
        }
    }
}

using SP.Service.Domain.Util;
using System;
using Grpc.Service.Core.Domain.Commands;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Account
{
    public class PayCouponCommand: EditCouponCommand
    {
        public double PayAmount { get; set; }
        public int PayType { get; set; }
        public PayCouponCommand(Guid aggregateId, double payAmount, int payType) :base(aggregateId)
        {
            this.PayType = payType;
            this.PayAmount = payAmount;
            this.CommandType = CommandType.PayCoupon;
        }
    }
}

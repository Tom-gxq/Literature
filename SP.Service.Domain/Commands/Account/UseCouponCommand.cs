using System;
using Grpc.Service.Core.Domain.Commands;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Account
{
    public class UseCouponCommand : EditCouponCommand
    {
        public int Status { get; set; }
        public UseCouponCommand(Guid aggregateId):base(aggregateId)
        {
            this.Status = 0;
            this.CommandType = CommandType.UseCoupon;
        }
    }
}

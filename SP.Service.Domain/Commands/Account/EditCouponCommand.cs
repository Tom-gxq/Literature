using Grpc.Service.Core.Domain.Commands;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Account
{
    public class EditCouponCommand : SPCommand
    {
        public EditCouponCommand(Guid aggregateId) : base(KafkaConfig.NormalCommandBusTopicTitle)
        {
            base.Id = aggregateId;
            this.CommandType = CommandType.EditCoupon;
        }
    }
}

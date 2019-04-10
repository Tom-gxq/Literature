using Grpc.Service.Core.Domain.Commands;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Account
{
    public class CreateCouponCommand : SPCommand
    {
        public string CouponId { get; set; }
        public string KindId { get; set; }
        public string AccountId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Status { get; set; }
        public string PayOrderCode { get; set; }
        public int PayType { get; set; }
        public int PayStatus { get; set; }

        public CreateCouponCommand(string kindId, string accountId) : base(KafkaConfig.NormalCommandBusTopicTitle)
        {
            base.Id = Guid.NewGuid();
            this.KindId = kindId;
            this.AccountId = accountId;
            this.CommandType = CommandType.CreateCoupon;
        }
    }
}

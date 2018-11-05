using Grpc.Service.Core.Domain.Commands;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Account
{
    public class CreatAssociatorCommand : Command
    {
        public string KindId { get; set; }
        public string AccountId { get; set; }
        public string PayOrderCode { get; set; }
        public int PayType { get; set; }
        public double Amount { get; set; }
        public int Quantity { get; set; }

        public CreatAssociatorCommand(Guid id, string kindId, string accountId, string payOrderCode, int payType,double amount, int quantity) : base(KafkaConfig.NormalCommandBusTopicTitle)
        {
            base.Id = id;
            this.KindId = kindId;
            this.AccountId = accountId;
            this.PayOrderCode = payOrderCode;
            this.PayType = payType;
            this.Amount = amount;
            this.Quantity = quantity;
            this.CommandType = CommandType.CreatAssociator;
        }
    }
}

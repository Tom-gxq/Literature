using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;
using Grpc.Service.Core.Domain.Commands;

namespace SP.Service.Domain.Commands.BalancePay
{
    public class BalancePayCommand : SPCommand
    {
        public string PassWord { get; set; }
        public string OrderCode { get; set; }
        public double Amount { get; set; }
        public string AccountId { get; set; }
        public string Sign { get; set; }
        public BalancePayCommand(string token, string password,string orderCode,double amount,string accountId,string sign) : base(KafkaConfig.CommandBusTopicTitle)
        {
            this.Id = Guid.NewGuid();
            this.Token = token;
            this.PassWord = password;
            this.OrderCode = orderCode;
            this.Amount = amount;
            this.AccountId = accountId;
            this.CommandType = CommandType.BalancePay;
            this.Sign = sign;
        }
    }
}

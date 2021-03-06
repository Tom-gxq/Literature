﻿using Grpc.Service.Core.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Order
{
    public class CreateCashApplyCommand : Command
    {
        public string AccountId { get; set; }
        public string Alipay { get; set; }
        public double Money { get; set; }
        public CreateCashApplyCommand(string accountId, string alipay, double money)
        {
            this.AccountId = accountId;
            this.Alipay = alipay;
            this.Money = money;

        }
    }
}

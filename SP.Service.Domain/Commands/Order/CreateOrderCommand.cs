﻿using Grpc.Service.Core.Domain.Commands;
using SP.Data.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands
{
    public class CreateOrderCommand: Command
    {
        public string Remark { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public DateTime OrderDate { get; set; }

        public string AccountId { get; set; }
        public List<string> CartIds { get; set; }
        public int AddressId { get; set; }

        public CreateOrderCommand(Guid id, string remark, string accountId, List<string> cartIds,int addressId)
        {
            base.Id = id;
            this.Remark = remark;
            this.OrderStatus = OrderStatus.WaitPay;
            this.OrderDate = DateTime.Now;
            this.AccountId = accountId;
            this.CartIds = cartIds;
            this.AddressId = addressId;
        }        
    }
}

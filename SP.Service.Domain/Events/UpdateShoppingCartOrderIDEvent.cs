﻿using Grpc.Service.Core.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class UpdateShoppingCartOrderIDEvent : Event
    {
        public string CartId { get; internal set; }
        public string OrderId { get; internal set; }
        public UpdateShoppingCartOrderIDEvent(string cartId, string orderId)
        {
            this.CartId = cartId;
            this.OrderId = orderId;
        }
    }
}

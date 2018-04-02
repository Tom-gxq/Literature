using Grpc.Service.Core.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class DelShoppingCartEvent : Event
    {
        public string CartId { get; set; }
        public DelShoppingCartEvent(string cartId)
        {
            this.CartId = cartId;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Grpc.Service.Core.Domain.Events;

namespace SP.Service.Domain.Events
{
    public class SellerProductDelEvent: SellerProductCreatedEvent
    {

        public SellerProductDelEvent(Guid id, string accountId, int suppliersId):base(id,accountId,suppliersId)
        {
            this.EventType = EventType.SellerProductDel;
        }
    }
}

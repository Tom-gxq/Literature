using Grpc.Service.Core.Domain.Events;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class SaleStatusEditEvent: Event
    {
        public int SuppliersId { get; set; }
        public string ProductId { get; set; }
        public int Status { get; set; }
        public SaleStatusEditEvent(Guid id, int status,int suppliersId,string productId) : base(KafkaConfig.EventBusTopicTitle)
        {
            base.AggregateId = id;
            this.CommandId = id.ToString();
            this.SuppliersId = suppliersId;
            this.ProductId = productId;
            this.Status = status;
            this.EventType = EventType.SaleStatusEdit;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class ProductImageEditEvent: ProductImageCreatedEvent
    {
        public ProductImageEditEvent(Guid id, string imagePath):base(id, imagePath)
        {
            this.EventType = Grpc.Service.Core.Domain.Events.EventType.ProductImageEdit;
        }
    }
}

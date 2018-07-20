using Grpc.Service.Core.Domain.Handlers;
using SP.Service.Domain.Events;
using SP.Service.Domain.Reporting;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.EventHandlers
{
    public class ProductImageCreatedEventHandler : IEventHandler<ProductImageCreatedEvent>, IEventHandler<ProductImageEditEvent>
    {
        private readonly ProductImageReportDatabase _reportDatabase;
        public ProductImageCreatedEventHandler(ProductImageReportDatabase reportDatabase)
        {
            _reportDatabase = reportDatabase;
        }
        public void Handle(ProductImageCreatedEvent handle)
        {
            _reportDatabase.Add(new ProductImageEntity()
            {
                ProductId = handle.AggregateId.ToString(),
                ImgPath = handle.ImagePath,
                Postion=0,
                DisplaySequence=1,
                IsDel = false,
                CreateTime = DateTime.Now
            });
        }
        public void Handle(ProductImageEditEvent handle)
        {
            _reportDatabase.Update(new ProductImageEntity()
            {
                ProductId = handle.AggregateId.ToString(),
                ImgPath = handle.ImagePath,
                Postion = 0,
                DisplaySequence = 1,
                UpdateTime = DateTime.Now
            });
        }
    }
}

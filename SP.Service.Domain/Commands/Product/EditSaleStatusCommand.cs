using Grpc.Service.Core.Domain.Commands;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Product
{
    public class EditSaleStatusCommand : SPCommand
    {
        public int SuppliersId { get; set; }
        public string ProductId { get; set; }
        public int Status { get; set; }
        public EditSaleStatusCommand(Guid id, int status, int suppliersId, string productId) : base(KafkaConfig.NormalCommandBusTopicTitle)
        {
            base.Id = id;
            this.Status = status;
            this.SuppliersId = suppliersId;
            this.ProductId = productId;
            this.CommandType = CommandType.EditSaleStatus;
        }
    }
}

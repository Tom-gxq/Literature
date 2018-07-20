using Grpc.Service.Core.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Product
{
    public class EditSaleStatusCommand : Command
    {
        public int Status { get; set; }
        public EditSaleStatusCommand(Guid id, int status)
        {
            base.Id = id;
            this.Status = status;
        }
    }
}

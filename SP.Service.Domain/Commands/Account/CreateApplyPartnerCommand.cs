using Grpc.Service.Core.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Account
{
    public class CreateApplyPartnerCommand : Command
    {
        public int DormId { get; set; }
        public CreateApplyPartnerCommand(Guid id,int dormId)
        {
            base.Id = id;
            this.DormId = dormId;
        }
    }
}

using Grpc.Service.Core.Domain.Commands;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Account
{
    public class CreateApplyPartnerCommand : Command
    {
        public int DormId { get; set; }
        public CreateApplyPartnerCommand(Guid id,int dormId) : base(KafkaConfig.NormalCommandBusTopicTitle)
        {
            base.Id = id;
            this.DormId = dormId;
            this.CommandType = CommandType.CreateApplyPartner;
        }
    }
}

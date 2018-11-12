using Grpc.Service.Core.Domain.Commands;
using Newtonsoft.Json;
using SP.Service.Domain.DomainEntity;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Account
{
    public class BindOtherAccountCommand : SPCommand
    {
        public string OtherAccount { get; set; }
        public OtherType OtherType { get; set; }        

        public BindOtherAccountCommand(Guid id, string otherAccount,OtherType otherType):base(KafkaConfig.NormalCommandBusTopicTitle)
        {
            base.Id = id;
            this.OtherAccount = otherAccount;
            this.OtherType = otherType;
            this.CommandType = CommandType.BindOtherAccount;
        }
    }
}

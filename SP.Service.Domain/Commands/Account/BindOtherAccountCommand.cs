﻿using Grpc.Service.Core.Domain.Commands;
using SP.Service.Domain.DomainEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Account
{
    public class BindOtherAccountCommand : Command
    {
        public string OtherAccount { get; set; }
        public OtherType OtherType { get; set; }        

        public BindOtherAccountCommand(Guid id, string otherAccount,OtherType otherType)
        {
            base.Id = id;
            this.OtherAccount = otherAccount;
            this.OtherType = otherType;
        }
    }
}

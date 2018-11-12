﻿using Grpc.Service.Core.Domain.Commands;
using SP.Service.Domain.DomainEntity;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Account
{
    public class CreateOtherAccountCommand : SPCommand
    {
        public string MobilePhone { get; set; }
        public string OtherAccount { get; set; }
        public OtherType OtherType { get; set; }
        public string FullName { get; set; }
        public string Avatar { get; set; }
        public bool Gender { get; set; }

        public CreateOtherAccountCommand(string mobilePhone, string otherAccount, OtherType otherType, string fullName, string avatar, bool gender) : base(KafkaConfig.NormalCommandBusTopicTitle)
        {
            base.Id = Guid.NewGuid();
            this.OtherAccount = otherAccount;
            this.OtherType = otherType;
            this.MobilePhone = mobilePhone;
            this.FullName = fullName;
            this.Avatar = avatar;
            this.Gender = gender;
            this.CommandType = CommandType.CreateOtherAccount;
        }
    }
}

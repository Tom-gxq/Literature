﻿using Grpc.Service.Core.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Account
{
    public class CreateAccountIDCardCommand : Command
    {
        public int DormId { get; set; }
        public string FullName { get; set; }
        public int UserType { get; set; }

        public CreateAccountIDCardCommand(Guid id, int dormId, string fullName, int userType)
        {
            base.Id = id;
            this.DormId = dormId;
            this.FullName = fullName;
            this.UserType = userType;
        }
    }
}

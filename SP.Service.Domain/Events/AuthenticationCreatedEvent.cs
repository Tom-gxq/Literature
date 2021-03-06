﻿using Grpc.Service.Core.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class AuthenticationCreatedEvent : Event
    {
        public int AuthType { get; set; }
        public string AccountId { get; set; }
        public string Account { get; set; }
        public string VerifyCode { get; set; }
        public string Token { get; set; }

        public AuthenticationCreatedEvent(Guid aggregateId, int authType, string accountId, string account, string verifyCode, string token)
        {
            base.AggregateId = aggregateId;
            this.AuthType = authType;
            this.AccountId = accountId;
            this.Account = account;
            this.VerifyCode = verifyCode;
            this.Token = token;
        }
    }
}

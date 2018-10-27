using Grpc.Service.Core.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public  class TokenCreatedEvent:Event
    {
        public string AccessToken { get; internal set; }
        public string AccountId { get; internal set; }
        public bool Status { get; internal set; }
        public DateTime CreateTime { get; internal set; }
        public TokenCreatedEvent(string token,string accountId,bool status,DateTime createTime)
        {
            this.AccessToken = token;
            this.AccountId = accountId;
            this.Status = status;
            this.CreateTime = createTime;
        }
    }
}

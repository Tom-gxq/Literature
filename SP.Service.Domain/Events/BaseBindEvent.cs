using Grpc.Service.Core.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class BaseBindEvent : Event
    {
        public string OtherAccount { get; set; }
        public BaseBindEvent(Guid id, string otherAccount)
        {
            this.AggregateId = id;
            this.OtherAccount = otherAccount;
        }
    }
    public class AliBindEvent : BaseBindEvent
    {
        public AliBindEvent(Guid id, string otherAccount) : base(id, otherAccount)
        {

        }
    }
    public class WxBindEvent : BaseBindEvent
    {
        public WxBindEvent(Guid id, string otherAccount) : base(id, otherAccount)
        {

        }
    }
    public class QQBindEvent : BaseBindEvent
    {
        public QQBindEvent(Guid id, string otherAccount) : base(id, otherAccount)
        {

        }
    }
}

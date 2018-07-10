using Grpc.Service.Core.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class AccountEditEvent : Event
    {
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public AccountEditEvent(Guid id, string mobilePhone, string email, string password)
        {
            this.AggregateId = id;
            this.MobilePhone = mobilePhone;
            this.Email = email;
            this.Password = password;
        }
    }
}

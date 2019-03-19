using Grpc.Service.Core.Domain.Entity;
using Grpc.Service.Core.Domain.Events;
using SP.Service.Domain.DomainEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class TokenEventFactory
    {
        public static Event CreatTokenEvent(AggregateRoot<Guid> domain)
        {
            if(domain is RepeatedTokenDomain)
            {
                var model = domain as RepeatedTokenDomain;
                switch (domain.Action)
                {
                    case DaomainAction.Create:
                        return new TokenCreatedEvent(model.Id,model.AccessToken, model.AccountId, model.Status, model.CreateTime);
                    case DaomainAction.Update:
                        return new TokenDisabledEvent(model.Id,model.AccessToken, model.AccountId, model.Status, model.UpdateTime);
                }
            }
            return null;
        }
    }
}

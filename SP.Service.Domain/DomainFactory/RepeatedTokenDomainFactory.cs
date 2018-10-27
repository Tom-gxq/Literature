using Grpc.Service.Core.Domain.Entity;
using SP.Service.Domain.DomainEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.DomainFactory
{
    public class RepeatedTokenDomainFactory : IDomainFactory
    {
        public AggregateRoot<Guid> GenerateDomain()
        {
            return new RepeatedTokenDomain();
        }
        
    }
}

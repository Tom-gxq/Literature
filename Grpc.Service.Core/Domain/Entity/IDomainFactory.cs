using System;
using System.Collections.Generic;
using System.Text;

namespace Grpc.Service.Core.Domain.Entity
{
    public interface IDomainFactory
    {
        AggregateRoot<Guid> GenerateDomain();
    }
}

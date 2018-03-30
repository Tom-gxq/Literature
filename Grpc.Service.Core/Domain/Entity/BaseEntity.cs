using System;
using System.Collections.Generic;
using System.Text;

namespace Grpc.Service.Core.Domain.Entity
{
    public abstract class BaseEntity: IEntity<int>
    {
        int IEntity<int>.Id { get; set; }
    }
}

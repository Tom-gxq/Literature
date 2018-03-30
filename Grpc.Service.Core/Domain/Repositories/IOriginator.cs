using Grpc.Service.Core.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grpc.Service.Core.Domain.Repositories
{
    public interface IOriginator
    {
        BaseEntity GetMemento();
        void SetMemento(BaseEntity memento);
    }
}

using Grpc.Service.Core.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grpc.Service.Core.Domain.Storage
{
    public interface IDataRepository<T> where T : AggregateRoot<Guid>, new()
    {
        void Save(AggregateRoot<Guid> aggregate, int expectedVersion=-1);
        T GetById(Guid id);
    }
}

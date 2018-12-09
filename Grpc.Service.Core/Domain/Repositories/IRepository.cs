using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grpc.Service.Core.Domain.Entity;

namespace Grpc.Service.Core.Domain.Repositories
{
    public interface IRepository<T>: IRepositoryBase<T, int> where T : BaseEntity
    {
        

       
    }
}

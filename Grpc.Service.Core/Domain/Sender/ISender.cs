using System;
using System.Collections.Generic;
using System.Text;

namespace Grpc.Service.Core.Domain.Sender
{
    public interface ISender
    {
        void Add(AbstractEntity entity);
    }
}

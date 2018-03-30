using System;
using System.Collections.Generic;
using System.Text;

namespace Grpc.Service.Core
{
    public interface IDataContext
    {
        string GetConnectionString();
    }
}

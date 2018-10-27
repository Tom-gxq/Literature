using Grpc.Service.Core.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grpc.Service.Core.Domain.Reporting
{
    public class BaseReportDatabase: IReportDatabase
    {
        protected IDomainFactory domainFactory;
        public BaseReportDatabase(IDomainFactory factory)
        {
            this.domainFactory = factory;
        }
    }
}

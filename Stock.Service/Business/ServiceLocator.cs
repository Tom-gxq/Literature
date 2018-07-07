using Grpc.Service.Core.Dependency;
using Grpc.Service.Core.Domain.Messaging;
using SP.Service.Domain.Reporting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.Service.Business
{
    public class ServiceLocator
    {
        private static ICommandBus _commandBus;
        private static ShopReportDatabase _reportDatabase;
        private static bool _isInitialized;
        private static readonly object _lockThis = new object();

        static ServiceLocator()
        {
            if (!_isInitialized)
            {
                lock (_lockThis)
                {
                    _commandBus = IocManager.Instance.Resolve(typeof(ICommandBus)) as ICommandBus; ;
                    _reportDatabase = IocManager.Instance.Resolve(typeof(ShopReportDatabase)) as ShopReportDatabase;
                    
                    _isInitialized = true;
                }
            }
        }

        public static ICommandBus CommandBus
        {
            get { return _commandBus; }
        }
        public static ShopReportDatabase ReportDatabase
        {
            get { return _reportDatabase; }
        }
    }
}

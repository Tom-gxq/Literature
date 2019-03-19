using Grpc.Service.Core.Dependency;
using Grpc.Service.Core.Domain.Messaging;
using SP.Service.Domain.Reporting;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepeatedToken.Service
{
    class ServiceLocator
    {
        private static ICommandBus _commandBus;
        private static RepeatedTokenReportDatabase _reportDatabase;
        private static bool _isInitialized;
        private static readonly object _lockThis = new object();

        static ServiceLocator()
        {
            if (!_isInitialized)
            {
                lock (_lockThis)
                {
                    _commandBus = IocManager.Instance.Resolve(typeof(ICommandBus)) as ICommandBus;
                    _reportDatabase = IocManager.Instance.Resolve(typeof(RepeatedTokenReportDatabase)) as RepeatedTokenReportDatabase;
                    
                    _isInitialized = true;
                }
            }
        }

        public static ICommandBus CommandBus
        {
            get { return _commandBus; }
        }
        public static RepeatedTokenReportDatabase ReportDatabase
        {
            get { return _reportDatabase; }
        }
    }
}

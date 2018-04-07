using Grpc.Service.Core.Dependency;
using Grpc.Service.Core.Domain.Messaging;
using SP.Service.Domain.Reporting;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderDelayQueue.Service
{
    public class ServiceLocator
    {
        private static ICommandBus _commandBus;
        private static ShoppingCartReportDatabase _reportShppingCartDatabase;
        private static OrderReportDatabase _reportOrderDatabase;
        private static bool _isInitialized;
        private static readonly object _lockThis = new object();

        static ServiceLocator()
        {
            if (!_isInitialized)
            {
                lock (_lockThis)
                {
                    _commandBus = IocManager.Instance.Resolve(typeof(ICommandBus)) as ICommandBus;                    
                    _reportShppingCartDatabase = IocManager.Instance.Resolve(typeof(ShoppingCartReportDatabase)) as ShoppingCartReportDatabase;
                    _reportOrderDatabase = IocManager.Instance.Resolve(typeof(OrderReportDatabase)) as OrderReportDatabase;
                    _isInitialized = true;
                }
            }
        }
        public static ICommandBus CommandBus
        {
            get { return _commandBus; }
        }

        public static ShoppingCartReportDatabase ShppingCartDatabase
        {
            get { return _reportShppingCartDatabase; }
        }
        public static OrderReportDatabase OrderDatabase
        {
            get { return _reportOrderDatabase; }
        }
    }
}

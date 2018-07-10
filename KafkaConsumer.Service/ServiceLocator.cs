using Grpc.Service.Core.Dependency;
using Grpc.Service.Core.Domain.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace KafkaConsumer.Service
{
    public class ServiceLocator
    {
        private static bool _isInitialized;
        private static readonly object _lockThis = new object();
        private static ICommandBus _commandBus;
        static ServiceLocator()
        {
            if (!_isInitialized)
            {
                lock (_lockThis)
                {
                    _commandBus = IocManager.Instance.Resolve(typeof(ICommandBus)) as ICommandBus;                    
                    _isInitialized = true;
                }
            }
        }
        public static ICommandBus CommandBus
        {
            get { return _commandBus; }
        }
    }
}

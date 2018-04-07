using Grpc.Service.Core.Dependency;
using Grpc.Service.Core.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RedisCache.Service;
using SP.Service.Domain.Commands.Order;
using System;
using System.IO;
using System.Threading;

namespace OrderDelayQueue.Service
{
    class Program
    {
        public static IConfigurationRoot Configuration { get; set; }
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();
            AssemblyRegistHelper.Register(Configuration);
            RedisCacheRegisteConfig.Register(IocManager.Instance);

            ILogger logger = new ServiceCollection()
              .AddLogging()
              .BuildServiceProvider()
              .GetService<ILoggerFactory>()
              .AddConsole()
              .CreateLogger("AccountService");

            var tms = Configuration.GetSection("Delay.Queue").Value;
            int previous = 0;
            int.TryParse(tms,out previous);
            while (true)
            {
                try
                {
                    var queue = new SP.Service.Domain.DelayQueue.DelayQueue();
                    var data = queue.Pop(previous);
                    foreach (var order in data)
                    {
                        var orderId = order.Value<string>("orderId");
                        var orderDate = order.Value<string>("orderDate");
                        if (!string.IsNullOrEmpty(orderId))
                        {
                            var orderDomain = ServiceLocator.OrderDatabase.GetOrderByOrderId(orderId);
                            if (orderDomain.OrderStatus == SP.Data.Enum.OrderStatus.WaitPay)
                            {
                                ServiceLocator.CommandBus.Send(new OrderRedoStockCommand(new Guid(orderId), orderDate));
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    logger.LogError(7300, ex, "OrderDelayQueue Exception");
                }
                Thread.Sleep(10000);
            }
        }
    }
}

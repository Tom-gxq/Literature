using FluentScheduler;
using Grpc.Service.Core.Dependency;
using Grpc.Service.Core.Reflection;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RedisCache.Service;
using SP.Service.Domain.Commands.Product;
using System;
using System.IO;
using System.Linq;

namespace Timer.Service
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

            var host = Configuration.GetSection("KafkaIP").Value;
            var service = new KafkaService(host);
            System.Threading.Thread thread = new System.Threading.Thread(service.Start);
            thread.Start();

            //库存定时操作
            var stockSvrHost = Configuration.GetSection("stockservice_host").Value;
            var foodId = Configuration.GetSection("FoodId").Value;
            var clearDayStockTime = Configuration.GetSection("ClearDayStockTime").Value;
            var autoSettingStockTime = Configuration.GetSection("AutoSettingStockTime").Value;
            JobManager.Initialize(new StockRegistry(stockSvrHost, int.Parse(foodId), 
                int.Parse(clearDayStockTime), int.Parse(autoSettingStockTime)));

            //var host = Configuration.GetSection(CommonKeys.AppHost).Value;
            //var port = Configuration.GetSection(CommonKeys.AppPort).Value;

            //var service = new CacheService(host, port);
            //service.Start();


        }
    }
    //public class CacheService
    //{
    //    readonly Grpc.Core.Server server;
    //    public CacheService(string host, string port)
    //    {
    //        int tempPort = 0;
    //        int.TryParse(port, out tempPort);
    //        server = new Grpc.Core.Server
    //        {
    //            Services = { StockService.BindService(new StockServiceImpl(tempPort)) },
    //            Ports = { new Grpc.Core.ServerPort(host, tempPort, Grpc.Core.ServerCredentials.Insecure) }
    //        };
    //    }
    //    public void Start()
    //    {
    //        server.Start();
    //        server.Ports.ToList().ForEach(a => Console.WriteLine($"stock server listening on {a.Host}port {a.Port}..."));
    //        server.ShutdownTask.Wait();
    //    }
    //    public void Stop() { server.ShutdownAsync(); }
    //}

    class KafkaService
    {
        SP.Consumer.KafkaConsumer Consumer;
        public KafkaService(string host)
        {
            Consumer = new SP.Consumer.KafkaConsumer("StockConsumer", host, $"ProductStockUpdate");
        }
        public void Start()
        {
            Console.WriteLine($"Consumer server Start...");
            Consumer.MessageEvent += (_, msg) =>
            {
                string text = msg.Value;
                Console.WriteLine($"Consumer Message=" + text);

                try
                {
                    var command = JsonConvert.DeserializeObject<UpdateProductSkuDBCommand>(text);
                    if (command.EventType == "ProductStock")
                    {
                        var memberCommand = command as EditProductSkuDBCommand;
                        ServiceLocator.CommandBus.Send(memberCommand);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Consumer ex=" + ex.Message + " StackTrace=" + ex.StackTrace);
                }
            };
            Consumer.ErrorEvent += (_, error) =>
            {
                Console.WriteLine($"Error: {error}");
            };
            Consumer.ConsumeErrorEvent += (_, msg) =>
            {
                Console.WriteLine($"Consume error ({msg.TopicPartitionOffset}): {msg.Error}");
            };
            Consumer.ConsumerRun();

        }
    }
}

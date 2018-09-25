using Grpc.Service.Core.Reflection;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SP.Service.Domain.Commands.Statistics;
using System;
using System.IO;

namespace KafkaSellerConsumer.Service
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

            var host = Configuration.GetSection("KafkaIP").Value;
            var service = new KafkaService(host);
            service.Start();
        }
    }
    class KafkaService
    {
        SP.Consumer.KafkaConsumer Consumer;
        public KafkaService(string host)
        {
            Consumer = new SP.Consumer.KafkaConsumer("SellerConsumer", host, $"Seller");
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
                    var command = JsonConvert.DeserializeObject<StatisticsCommand>(text);
                    if (command.EventType == "SellerOrder")
                    {
                        Console.WriteLine($"ServiceLocator.CommandBus.Send");
                        var sellerCommand = command as SumSellerStatisticsCommand;
                        ServiceLocator.CommandBus.Send(sellerCommand);
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

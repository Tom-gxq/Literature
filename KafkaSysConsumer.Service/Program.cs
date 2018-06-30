using System;
using Microsoft.Extensions.Configuration;
using System.IO;
using SP.Consumer;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SP.Service.Entity;
using SP.Service.Domain.Commands.Statistics;
using Grpc.Service.Core.Reflection;

namespace KafkaSysConsumer.Service
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
            Consumer = new SP.Consumer.KafkaConsumer("SysConsumer", host, $"SysStatistic");
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
                    if(command.EventType == "Member")
                    {
                        var memberCommand = command as SumMemberStatisticsCommand;
                        ServiceLocator.CommandBus.Send(memberCommand);
                    }
                    else if (command.EventType == "UserReg")
                    {
                        var memberCommand = command as SumUserStatisticsCommand;
                        ServiceLocator.CommandBus.Send(memberCommand);
                    }
                    else if (command.EventType == "UserReg")
                    {
                        var memberCommand = command as SumUserStatisticsCommand;
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

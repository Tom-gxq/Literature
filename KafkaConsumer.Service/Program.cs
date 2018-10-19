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

namespace KafkaConsumer.Service
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

            Console.WriteLine("args length="+ args.Length);
            for(int i=0;i<args.Length;i++)
            {
                Console.WriteLine($"args[{i}]={args[i]}" );
            }
            var host = Configuration.GetSection("KafkaIP").Value;
            if (args.Length >= 1)
            {
                var service = new KafkaService(host, args[0]);
                service.Start();
            }
            else
            {
                Console.WriteLine("Need the building ID!");
            }

        }
        
    }
    class KafkaService
    {
        SP.Consumer.KafkaConsumer Consumer;
        public KafkaService(string host,string schoolId)
        {
            Consumer = new SP.Consumer.KafkaConsumer("OrderConsumer", host, $"Account_{schoolId}");        
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
                    var command = JsonConvert.DeserializeObject<SumOrderStatisticsCommand>(text);
                    ServiceLocator.CommandBus.Send(command);
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Consumer ex="+ ex.Message+ " StackTrace=" + ex.StackTrace);
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
            //while(true)
            //{
            //    var status = Console.ReadLine();
            //    if(status == "exit")
            //    {
            //        break;
            //    }
            //}
        }
    }
    
}

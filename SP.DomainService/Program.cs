using AutoMapper;
using Grpc.Service.Core.Dependency;
using Grpc.Service.Core.Domain.Commands;
using Grpc.Service.Core.Reflection;
using Microsoft.Extensions.Configuration;
using SP.Service.Domain.AutoMap;
using SP.Service.Domain.CommandHandlers.Execute;
using SP.Service.Domain.Reporting;
using System;
using System.IO;

namespace SP.DomainService
{
    class Program
    {
        public static IConfigurationRoot Configuration { get; set; }
        static void Main(string[] args)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new TokenProfile());
                cfg.AddProfile(new AccountProfile());
            });
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();
            AssemblyRegistHelper.Register(Configuration);

            var host = Configuration.GetSection("KafkaIP").Value;
            var topicTitle = Configuration.GetSection("CommandBus.TopicTitle").Value;
            try
            {
                var service = new KafkaService(host, topicTitle);
                service.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CommandBus ex=" + ex.Message + " StackTrace=" + ex.StackTrace);
            }
        }
    }
    class KafkaService
    {
        SP.Consumer.KafkaConsumer Consumer;
        ICommandExecuteHandler _commandFactory;
        public KafkaService(string host, string topicTitle)
        {
            Consumer = new SP.Consumer.KafkaConsumer("CommandBus", host, topicTitle);
            _commandFactory = IocManager.Instance.Resolve(typeof(CommandExecuteHandler)) as CommandExecuteHandler;
            Console.WriteLine($"CommandBus server host:[{host}]...topicTitle:[{topicTitle}]");
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
                    _commandFactory.ExecuteCommand(text);
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

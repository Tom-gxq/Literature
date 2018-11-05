﻿using Grpc.Service.Core.Dependency;
using Grpc.Service.Core.Domain.Commands;
using Grpc.Service.Core.Domain.Events;
using Grpc.Service.Core.Reflection;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace DataService
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
            var topicTitle = Configuration.GetSection("EventBus.TopicTitle").Value;
            var service = new KafkaService(host, topicTitle);
            service.Start();
        }
    }

    class KafkaService
    {
        SP.Consumer.KafkaConsumer Consumer;
        IEventExecuteHandler _eventHandler;
        public KafkaService(string host, string topicTitle)
        {
            Consumer = new SP.Consumer.KafkaConsumer("EventBus", host, topicTitle);
            _eventHandler = IocManager.Instance.Resolve<EventExecuteHandler>(typeof(IEventExecuteHandler));
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
                    _eventHandler.ExecuteEvent(text);
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

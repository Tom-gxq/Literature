using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using Grpc.Service.Core.Dependency;
using Grpc.Service.Core.Domain.Commands;
using Grpc.Service.Core.Domain.Events;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Messaging
{
    class KafkaSender
    {
        private string KafkaIP { get; set; }
        public KafkaSender()
        {
            var config = IocManager.Instance.Resolve<IConfigurationRoot>();            
            if (config != null)
            {
                this.KafkaIP = config.GetSection("KafkaIP").Value?.ToString() ?? string.Empty;
            }
        }
        public KafkaSender(string kafkaIP)
        {
            this.KafkaIP = kafkaIP;
        }
        public void Execute(Command command)
        {
            var config = new Dictionary<string, object>
            {
                { "bootstrap.servers", this.KafkaIP }
            };
            using (var producer = new Producer<Null, string>(config, null, new StringSerializer(Encoding.UTF8)))
            {

                var dr = producer.ProduceAsync(command.TopicTitle, new Message<Null, string>() { Value = command.GetMessage() }).Result;
                Console.WriteLine($"Command Delivered KafkaIP:[{this.KafkaIP}]   '{dr.Value}' to: {dr.TopicPartitionOffset}");
            }
        }

        public void Execute(Event @event)
        {
            var config = new Dictionary<string, object>
            {
                { "bootstrap.servers", this.KafkaIP }
            };
            using (var producer = new Producer<Null, string>(config, null, new StringSerializer(Encoding.UTF8)))
            {

                var dr = producer.ProduceAsync(@event.TopicTitle, new Message<Null, string>() { Value = @event.GetMessage() }).Result;
                Console.WriteLine($"Event Delivered KafkaIP:[{this.KafkaIP}]   '{dr.Value}' to: {dr.TopicPartitionOffset}");
            }
        }
    }
}

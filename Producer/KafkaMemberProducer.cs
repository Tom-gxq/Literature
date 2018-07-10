using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using Grpc.Service.Core.Domain.Sender;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Producer
{
    public class KafkaMemberProducer : AbstractEntity
    {
        private static string EventType => "Member";
        private static string TopicTitle => "SysStatistic";
        public string IPConfig { get; set; }
        public string AccountId { get; set; }
        public AuthorizeType Type { get; set; }
        public double Amount { get; set; }
        public override void Run()
        {
            var config = new Dictionary<string, object>
            {
                { "bootstrap.servers", IPConfig }
            };
            using (var producer = new Producer<Null, string>(config, null, new StringSerializer(Encoding.UTF8)))
            {
                var dr = producer.ProduceAsync(TopicTitle, new Message<Null, string>() { Value = GetMessage() }).Result;
                Console.WriteLine($"Delivered '{dr.Value}' to: {dr.TopicPartitionOffset}");
            }
        }

        private string GetMessage()
        {
            Dictionary<string, object> JsonResult = new Dictionary<string, object>();
            JsonResult.Add("EventType", EventType);
            JsonResult.Add("AccountId", this.AccountId);
            JsonResult.Add("CreateTime", DateTime.Now.ToString("yyyy-MM-dd"));
            JsonResult.Add("Amount", this.Amount);
            JsonResult.Add("Type", (int)this.Type);

            return JsonConvert.SerializeObject(JsonResult);
        }
    }

    public enum AuthorizeType
    {
        Present = 0,
        Buy
    }
}

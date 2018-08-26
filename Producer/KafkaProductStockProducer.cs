using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using Grpc.Service.Core.Domain.Sender;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Producer
{
    public class KafkaProductStockProducer: AbstractEntity
    {
        private static string EventType => "ProductStock";
        private static string TopicTitle => "ProductStockUpdate";
        public string IPConfig { get; set; }
        public string AccountId { get; set; }
        public string ProductId { get; set; }
        public int ShopId { get; set; }
        public int Stock { get; set; }
        public int Type { get; set; }
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
            JsonResult.Add("ProductId", this.ProductId);
            JsonResult.Add("ShopId", this.ShopId);
            JsonResult.Add("Stock", this.Stock);
            JsonResult.Add("Type", this.Type);

            return JsonConvert.SerializeObject(JsonResult);
        }
    }
}

using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using Grpc.Service.Core.Domain.Sender;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Producer
{
    public class SellerProducer : AbstractEntity
    {
        private static string EventType => "SellerOrder";
        private static string TopicTitle => "Seller";
        public string IPConfig { get; set; }
        public string ShippingId { get; set; }
        public string Shipto { get; set; }
        public string OrderId { get; set; }
        public double OrderAmount { get; set; }
        public override void Run()
        {
            var topicTitle = $"{TopicTitle}";
            var config = new Dictionary<string, object>
            {
                { "bootstrap.servers", IPConfig }
            };
            using (var producer = new Producer<Null, string>(config, null, new StringSerializer(Encoding.UTF8)))
            {
                var dr = producer.ProduceAsync(topicTitle, new Message<Null, string>() { Value = GetMessage() }).Result;
                Console.WriteLine($"Delivered '{dr.Value}' to: {dr.TopicPartitionOffset}");
            }
            //// Producer 接受一个或多个 BrokerList
            //using (RdKafka.Producer producer = new RdKafka.Producer(IPConfig))
            ////发送到一个名为 testtopic 的Topic，如果没有就会创建一个
            //using (Topic topic = producer.Topic(topicTitle))
            //{
            //    //将message转为一个 byte[]
            //    byte[] data = Encoding.UTF8.GetBytes(GetMessage());
            //    DeliveryReport deliveryReport = await topic.Produce(data);

            //    Console.WriteLine($"发送到分区：{deliveryReport.Partition}, Offset 为: {deliveryReport.Offset}");
            //}            
        }
        private string GetMessage()
        {
            Dictionary<string, object> JsonResult = new Dictionary<string, object>();
            JsonResult.Add("EventType", EventType);
            JsonResult.Add("ShippingId", this.ShippingId);
            JsonResult.Add("OrderAmount", this.OrderAmount);
            JsonResult.Add("AccountId", this.Shipto);
            JsonResult.Add("OrderId", this.OrderId);

            return JsonConvert.SerializeObject(JsonResult);
        }
    }
}

using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using Grpc.Service.Core.Domain.Sender;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
//using RdKafka;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Producer
{
    public class KafkaProducer : AbstractEntity
    {
        private static string TopicTitle => "Account";
        public string IPConfig { get; set; }
        public OrdersEntity Order   { get; set; }
        public int BuildingId { get; set; }
        public override void Run()
        {            
            var topicTitle = $"{TopicTitle}_{BuildingId}";
            var config = new Dictionary<string, object>
            {
                { "bootstrap.servers", IPConfig }
            };
            using (var producer = new Producer<Null, string>(config, null, new StringSerializer(Encoding.UTF8)))
            {
                var dr = producer.ProduceAsync(topicTitle, new Message<Null, string>() { Value= GetMessage() }).Result;
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
            JsonResult.Add("OrderId", this.Order.OrderId);
            JsonResult.Add("OrderCode", this.Order.OrderCode);
            JsonResult.Add("AccountId", this.Order.AccountId);
            JsonResult.Add("AddressId", this.Order.AddressId);
            JsonResult.Add("OrderDate", this.Order.OrderDate.Value.ToString("yyyy-MM-dd"));
            JsonResult.Add("Amount", (this.Order.IsVip.Value ? this.Order.VIPAmount : this.Order.Amount));

            return JsonConvert.SerializeObject(JsonResult);
        }
    }
}

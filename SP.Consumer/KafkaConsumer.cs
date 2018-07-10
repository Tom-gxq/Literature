using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Consumer
{
    public class KafkaConsumer
    {
        private string GroupId;
        private string IPConfig;
        private string TopicTitle;
        public event EventHandler<ConsumerRecord<Null, string>> MessageEvent;
        public event EventHandler<Error> ErrorEvent;
        public event EventHandler<ConsumerRecord> ConsumeErrorEvent;
        public KafkaConsumer(string groupId,string ipConfig,string topicTitle)
        {
            this.GroupId = groupId;
            this.IPConfig = ipConfig;
            this.TopicTitle = topicTitle;
        }

        public void ConsumerRun()
        {
            //配置消费者组
            var conf = new Dictionary<string, object>
            {
              { "group.id", this.GroupId },
              { "bootstrap.servers", this.IPConfig },
              { "auto.commit.interval.ms", 5000 },
              { "auto.offset.reset", "earliest" }
            };
            using (var consumer = new Consumer<Null, string>(conf, null, new StringDeserializer(Encoding.UTF8)))
            {
                consumer.OnRecord += this.MessageEvent;

                consumer.OnError += this.ErrorEvent;

                consumer.OnConsumeError += this.ConsumeErrorEvent;

                consumer.Subscribe(this.TopicTitle);

                while (true)
                {
                    consumer.Poll(TimeSpan.FromMilliseconds(100));
                }
            }
            //var config = new Config() { GroupId = this.GroupId };
            //using (var consumer = new EventConsumer(config, this.IPConfig))
            //{

            //    if (this.MessageEvent != null)
            //    {
            //        //注册一个事件
            //        consumer.OnMessage += this.MessageEvent;
            //    }

            //    //订阅一个或者多个Topic
            //    consumer.Subscribe(new List<string> { this.TopicTitle });

            //    //启动
            //    consumer.Start();

            //    Console.WriteLine("Started consumer, press enter to stop consuming");
            //}
        }
    }
}

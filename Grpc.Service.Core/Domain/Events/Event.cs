using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Grpc.Service.Core.Domain.Events
{
    [Serializable]
    public class Event : IEvent
    {
        public int Version { get; set; }
        public Guid AggregateId { get; set; }
        public string CommandId { get; set; }
        public string TopicTitle { get; set; }
        public EventType EventType { get; set; }
        public virtual string GetMessage()
        {
            return JsonConvert.SerializeObject(this);
        }
        public Event()
        {
            
        }
        public Event(string topicTitle)
        {
            this.TopicTitle = topicTitle;
        }
    }
}

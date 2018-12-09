using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grpc.Service.Core.Domain.Commands
{
    public abstract class Command : ICommand
    {
        public Command()
        {
            this.Id = Guid.NewGuid();            
        }
        public Command(string topicTitle)
        {
            this.TopicTitle = topicTitle;
        }

        public Guid Id
        {
            get; set;
        }
        public string TopicTitle { get; set; }
        public CommandType CommandType{ get; set; }
        public virtual string GetMessage()
        {            
            return JsonConvert.SerializeObject(this);
        }
        public int ExcuteStatus { get; set; }
        public string Token { get; set; }        
    }
}

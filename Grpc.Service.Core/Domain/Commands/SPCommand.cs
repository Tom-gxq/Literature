using System;
using System.Collections.Generic;
using System.Text;

namespace Grpc.Service.Core.Domain.Commands
{
    public class SPCommand : Command
    {
        public string CommandId { get; set; }
        public SPCommand()
        {
            
        }
        public SPCommand(string topicTitle) : base(topicTitle)
        {
            
        }
    }
}

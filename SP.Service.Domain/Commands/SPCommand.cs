using Grpc.Service.Core.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands
{
    public class SPCommand: Command
    {
        public SPCommand(string topicTitle):base(topicTitle)
        {
            
        }
    }
}

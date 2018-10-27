using Grpc.Service.Core.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Token
{
    public class GenerateCommand : Command
    {
        public string AccessToken { get; set; }
        public string AccountId { get; set; }
        public bool Status { get; set; }
        public DateTime CreateTime { get; set; }
        public GenerateCommand()
        {

        }
    }
}

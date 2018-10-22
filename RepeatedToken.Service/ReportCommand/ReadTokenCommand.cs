using Grpc.Service.Core.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepeatedToken.Service.ReportCommand
{
    public class ReadTokenCommand: Command
    {
        public string AccountId { get; set; }
        public string Key { get; set; }
        public ReadTokenCommand()
        {

        }
    }
}

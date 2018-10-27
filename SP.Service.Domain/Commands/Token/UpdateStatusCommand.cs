using Grpc.Service.Core.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Token
{
    public class UpdateStatusCommand : Command
    {
        public string AccountId { get; set; }
        public string AccessToken { get; set; }
        public bool Status { get; set; }
        public DateTime UpdateTime { get; set; }
        public UpdateStatusCommand()
        {

        }
    }
}

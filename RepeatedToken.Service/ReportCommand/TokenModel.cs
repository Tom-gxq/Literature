using System;
using System.Collections.Generic;
using System.Text;

namespace RepeatedToken.Service.ReportCommand
{
    internal class TokenModel
    {
        public int Status { get; set; }
        public RepeatedTokenModel RepeatedToken { get; set; }
    }
    class RepeatedTokenModel
    {
        public string AccessToken { get; set; }
        public string AccountId { get; set; }
        public long CreateTime { get; set; }
        public int Status { get; set; }
    }
}

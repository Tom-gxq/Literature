using Sms.Service.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sms.Service.Utility.ValueObjects
{
    public class SendResult
    {
        //发送结果
        public Codes Code { get; set; }

        //第三方接口返回的完整结果
        public string Response { get; set; }

        //异常
        public Exception Ex { get; set; }
    }
}

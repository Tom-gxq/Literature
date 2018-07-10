using System;
using System.Collections.Generic;
using System.Text;

namespace Sms.Service.Dtos
{
    public class HttpInput
    {
        // 接收手机号
        public string Url { get; set; }

        // 发送内容
        public string Data { get; set; }
    }
}

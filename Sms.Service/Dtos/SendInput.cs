using Sms.Service.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sms.Service.Dtos
{
    public class SendInput
    {
        // 哪个模块调用
        public string RequestId { get; set; }

        // 发送者
        public string FromAccountId { get; set; }

        // 发送IP
        public string IP { get; set; }

        // 接收手机号
        public string Mobile { get; set; }

        // 发送内容
        public string Message { get; set; }

        // 模板类型
        public TemplateType TemplateType { get; set; } = TemplateType.Default;

        // 模板参数
        public List<string> TemplateDatas { get; set; }

        // 类型
        public MessageType MessageType { get; set; } = MessageType.Mobile;
    }
}

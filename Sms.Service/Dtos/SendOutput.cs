using Sms.Service.Utility;
using Sms.Service.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sms.Service.Dtos
{
    public class SendOutput
    {
        /// <summary>
        /// 发送状态
        /// </summary>
        public Codes Code { get; set; }

        /// <summary>
        /// 描述信息
        /// </summary>
        private string message;
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Message
        {
            get
            {
                if (string.IsNullOrEmpty(message) && Code != 0)
                {
                    return Code.GetEnumDescription();
                }
                return message;
            }
            set => message = value ?? string.Empty;
        }

    }
}

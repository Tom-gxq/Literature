using System;
using System.Collections.Generic;
using System.Text;

namespace Sms.Service.Dtos
{
    public class HttpOutput
    {
        /// <summary>
        /// 发送状态
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 描述信息
        /// </summary>
        public string message;
        
    }
}

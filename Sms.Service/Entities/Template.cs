using Sms.Service.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sms.Service.Entities
{
    public class Template
    {
        /// <summary>
        /// 类型
        /// </summary>
        public TemplateType Type { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 模板内的参数
        /// </summary>
        public List<string> Vars { get; set; }
    }
}

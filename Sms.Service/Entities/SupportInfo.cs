using System;
using System.Collections.Generic;
using System.Text;

namespace Sms.Service.Entities
{
    public class SupportInfo
    {
        /// <summary>
        /// 是否只支持输入内容的短信
        /// </summary>
        public bool IsSupportText { get; set; }

        /// <summary>
        /// 是否支持模板内容的短信
        /// </summary>
        public bool IsSupportTemplate { get; set; }

        /// <summary>
        /// 模板列表
        /// </summary>
        public List<Template> Templates { get; set; }
    }
}

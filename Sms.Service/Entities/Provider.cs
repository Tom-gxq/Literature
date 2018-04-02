using System;
using System.Collections.Generic;
using System.Text;

namespace Sms.Service.Entities
{
    public class Provider
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 权重
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// 短信
        /// </summary>
        public SupportRange Mobile { get; set; }

        /// <summary>
        /// 语音
        /// </summary>
        public SupportRange Voice { get; set; }

    }
}

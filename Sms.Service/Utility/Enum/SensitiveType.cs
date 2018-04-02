using System;
using System.Collections.Generic;
using System.Text;

namespace Sms.Service.Utility
{
    public enum SensitiveType
    {
        /// <summary>
        /// 文本敏感词
        /// </summary>
        Text = 1,

        /// <summary>
        /// AccountId 敏感词
        /// </summary>
        AccountId = 2,

        /// <summary>
        /// 手机号敏感词
        /// </summary>
        Moblie = 3,

        /// <summary>
        /// 邮件敏感词
        /// </summary>
        Email = 4,

        /// <summary>
        /// IP 敏感词
        /// </summary>
        IP = 5,
    }
}

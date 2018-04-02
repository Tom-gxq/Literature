using System;
using System.Collections.Generic;
using System.Text;

namespace Sms.Service.Utility
{
    public enum TemplateType
    {
        /// <summary>
        /// 默认（发送短信）
        /// </summary>
        Default = 0,

        /// <summary>
        /// 短信注册
        /// </summary>
        MMRegister = 1,

        /// <summary>
        /// 语音注册
        /// </summary>
        VMRegister = 2,

        /// <summary>
        /// 语音日程提醒
        /// </summary>
        VMCalendarNotice = 3
    }
}

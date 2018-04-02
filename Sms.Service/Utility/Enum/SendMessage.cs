using System;
using System.Collections.Generic;
using System.Text;

namespace Sms.Service.Utility
{
    public enum SendMessageFromType
    {
        /// <summary>
        /// 注册
        /// </summary>
        Register = 1,

        /// <summary>
        /// 找回密码
        /// </summary>
        FindPassword = 2,

        /// <summary>
        /// 邀请
        /// </summary>
        Invite = 3,
    }

    public enum SendMessageResult
    {
        Failed = 0,

        /// <summary>
        /// 可以发送
        /// </summary>
        Success = 1,

        /// <summary>
        /// 超出限制
        /// </summary>
        Limit = 2
    }
}

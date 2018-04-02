using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sms.Service.Utility
{
    /// <summary>
    /// 发送状态
    /// </summary>
    public enum Codes
    {
        [Display(Description = "发送成功")]
        Success = 1,

        [Display(Description = "发送失败")]
        Failed = 10001,

        [Display(Description = "余额不足")]
        NoBalance = 10002,

        [Display(Description = "不支持发送此类消息")]
        NotSupport = 10003,

        [Display(Description = "敏感信息禁止发送")]
        Forbid = 10004,

    }
}

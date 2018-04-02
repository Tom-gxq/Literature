using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiGateway.App_Start.Enum
{
    /// <summary>
    /// 提供商
    /// </summary>
    public enum SendSP
    {
        /// <summary>
        /// 思齐（国际短信）
        /// </summary>
        Rspread = 0,

        /// <summary>
        /// 移通网络，发送私信内容
        /// </summary>
        Yitong = 1,

        /// <summary>
        /// 云通讯
        /// </summary>
        Yuntongxun = 2,

        /// <summary>
        /// WebPower 功能型
        /// </summary>
        WebPowerFunction = 3,

        /// <summary>
        /// WebPower 营销型
        /// </summary>
        WebPowerMarketing = 4,

        /// <summary>
        /// 阿里云
        /// </summary>
        Aliyun = 5,
    }

    public enum WebPowerType
    {
        /// <summary>
        /// 功能型
        /// </summary>
        Function = 1,

        /// <summary>
        /// 营销型
        /// </summary>
        Marketing = 2
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace Sms.Service.Utility
{
    /// <summary>
    /// 供应商
    /// </summary>
    public enum ProviderType
    {
        /// <summary>
        /// 无供应商
        /// </summary>
        None = -1,

        /// <summary>
        /// 思齐（国际短信）
        /// </summary>
        Rspread = 0,

        /// <summary>
        /// 云通讯
        /// </summary>
        Yuntongxun = 1,

        /// <summary>
        /// WebPower功能型
        /// </summary>
        WebPowerFunction = 2,

        /// <summary>
        /// WebPower营销型
        /// </summary>
        WebPowerMarketing = 3,

        /// <summary>
        /// 梦网科技
        /// </summary>
        MWGate = 4,

        /// <summary>
        /// 云片
        /// </summary>
        Yunpian = 5,

        /// <summary>
        ///  三体
        /// </summary>
        Santi = 6,

        /// <summary>
        ///  阿里云
        /// </summary>
        Aliyun = 7,

        /// <summary>
        ///  赛邮
        /// </summary>
        Submail = 8,

        /// <summary>
        ///  创蓝
        /// </summary>
        Chuanglan = 9,
        /// <summary>
        ///  胥龙
        /// </summary>
        Xulong = 10,
    }
}

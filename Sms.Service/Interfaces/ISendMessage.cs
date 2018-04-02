using Sms.Service.Utility.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sms.Service.Interfaces
{
    internal interface ISendMessage
    {
        /// <summary>
        /// 发送短信
        /// </summary>
        SendResult SendMobileMessage(string mobile, string message);

        /// <summary>
        /// 发送模板短信
        /// </summary>
        SendResult SendMobileMessage(string mobile, Dictionary<string, string> templateDataDic, string templateId);

        /// <summary>
        /// 发送语音
        /// </summary>
        SendResult SendVoiceMessage(string mobile, string message);

        /// <summary>
        /// 发送短信语音
        /// </summary>
        SendResult SendVoiceMessage(string mobile, Dictionary<string, string> templateDataDic, string templateId);
    }
}

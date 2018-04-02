using Sms.Service.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sms.Service.Utility
{
    public class WeixinHelper
    {
        #region 微信通知提醒，先绑定明道微信服务号

        private const string WeixinNoticeUrl = "http://weixin.mingdao.com/notification/warn";

        private static readonly List<string> NoticeAccountIds = new List<string>
        {
            "c9af1d84-b9a7-454a-b252-8af660948465", //beck
            "5e5482cf-c880-4fa6-85ef-385b7fdad95d", //jerry
            "fa416820-44dc-4673-816e-519aecb1ec49", //choc
        };

        /// <summary>
        /// 余额不足的情况下，发送短信提醒
        /// </summary>
        public static void SendNoBalanceMessage(ProviderType type)
        {
            var content = type + "短信服务商余额不足";
            foreach (var accountId in NoticeAccountIds)
            {
                RequestHelper.GetRequestServer(WeixinNoticeUrl + "?a=" + accountId + "&t=" + content + "&c=" + content);
            }
        }

        #endregion
    }
}

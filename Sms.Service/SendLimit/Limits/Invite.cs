using Grpc.Service.Core.Dependency;
using Microsoft.Extensions.Configuration;
using Sms.Service.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sms.Service.SendLimit.Limits
{
    public class Invite
    {
        private static int ipLimitCount = 0;
        /// <summary>
        /// IP限制数量
        /// </summary>
        public static int IpLimitCount
        {
            get
            {
                var config = IocManager.Instance.Resolve<IConfigurationRoot>();
                if (config != null)
                {
                    var reObj = config.GetSection("SameIPInviteLimitCount");
                    var ipLimitCountStr = reObj.Value;
                    if (!string.IsNullOrEmpty(ipLimitCountStr))
                    {
                        ipLimitCount = Convert.ToInt32(ipLimitCountStr);
                    }
                }

                return ipLimitCount;
            }
        }


        private static int ipLimitCountDays = 1;
        /// <summary>
        /// IP限制数量 N天
        /// </summary>
        public static int IpLimitCountDays
        {
            get
            {
                var config = IocManager.Instance.Resolve<IConfigurationRoot>();
                if (config != null)
                {
                    var reObj = config.GetSection("SameIPInviteLimitCountDays");
                    var ipLimitCountDaysStr = reObj.Value;
                    if (string.IsNullOrEmpty(ipLimitCountDaysStr))
                    {
                        ipLimitCountDays = Convert.ToInt32(ipLimitCountDaysStr);
                    }
                }

                return ipLimitCountDays;
            }
        }


        private static int userLimitCount = 0;
        /// <summary>
        /// 每个用户可邀请的账号限制数量
        /// </summary>
        public static int UserLimitCount
        {
            get
            {
                var config = IocManager.Instance.Resolve<IConfigurationRoot>();
                if (config != null)
                {
                    var reObj = config.GetSection("SameUserInviteLimitCount");
                    var userLimitCountStr = reObj.Value;
                    if (!string.IsNullOrEmpty(userLimitCountStr))
                    {
                        userLimitCount = Convert.ToInt32(userLimitCountStr);
                    }
                }

                return userLimitCount;
            }
        }


        private static int userLimitCountDays = 1;
        /// <summary>
        /// 每个用户可邀请的账号限制数量 N天
        /// </summary>
        public static int UserLimitCountDays
        {
            get
            {
                var config = IocManager.Instance.Resolve<IConfigurationRoot>();
                if (config != null)
                {
                    var reObj = config.GetSection("SameUserInviteLimitCountDays");
                    var userLimitCountDaysStr = reObj.Value;
                    if (!string.IsNullOrEmpty(userLimitCountDaysStr))
                    {
                        userLimitCountDays = Convert.ToInt32(userLimitCountDaysStr);
                    }
                }
                return userLimitCountDays;
            }
        }

        private static int mobileLimitCount = 0;
        /// <summary>
        /// 每个用户可邀请的账号限制数量
        /// </summary>
        public static int MobileLimitCount
        {
            get
            {
                var config = IocManager.Instance.Resolve<IConfigurationRoot>();
                if (config != null)
                {
                    var reObj = config.GetSection("MobileLimitCount");
                    var mobileLimitCountStr = reObj.Value;
                    if (!string.IsNullOrEmpty(mobileLimitCountStr))
                    {
                        mobileLimitCount = Convert.ToInt32(mobileLimitCountStr);
                    }
                }
                return mobileLimitCount;
            }
        }

        /// <summary>
        /// 限制的IP
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 限制的账号Id
        /// </summary>
        public string AccountId { get; set; }

        public int Count { get; set; } = 1;

        private Proxy GetProxy()
        {
            var limitDatas = new List<LimitData>();
            if (IpLimitCount > 0)
            {
                limitDatas.Add(new LimitData()
                {
                    Key = IP,
                    ExpiryDate = DateTime.Today.AddDays(IpLimitCountDays).AddSeconds(-1),
                    LimitCount = IpLimitCount,
                    Count = Count
                });
            }
            if (UserLimitCount > 0)
            {
                limitDatas.Add(new LimitData()
                {
                    Key = AccountId,
                    ExpiryDate = DateTime.Today.AddDays(UserLimitCountDays).AddSeconds(-1),
                    LimitCount = UserLimitCount,
                    Count = Count
                });
            }

            if (limitDatas.Count > 0)
            {
                Proxy proxy = new Proxy(SendMessageFromType.Invite);
                proxy.LimitDatas.AddRange(limitDatas);
                return proxy;
            }
            return null;
        }

        /// <summary>
        /// 设置限制数量
        /// </summary>
        public void SetLimitNumber()
        {
            Proxy proxy = GetProxy();
            if (proxy != null)
            {
                proxy.SetLimitNumber();
            }
        }

        /// <summary>
        /// 检测是否允许发送
        /// </summary>
        public bool IsAllowSend()
        {
            Proxy proxy = GetProxy();
            if (proxy != null)
            {
                return proxy.IsAllowSend();
            }
            return true;
        }

    }
}

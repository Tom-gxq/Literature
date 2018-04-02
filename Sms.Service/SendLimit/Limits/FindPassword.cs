using Grpc.Service.Core.Dependency;
using Microsoft.Extensions.Configuration;
using Sms.Service.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sms.Service.SendLimit.Limits
{
    public class FindPassword : ILimit
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
                    var reObj = config.GetSection("SameIPFindPasswordLimitCount");
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
                    var reObj = config.GetSection("SameIPFindPasswordLimitCountDays");
                    var ipLimitCountDaysStr = reObj.Value;
                    if (string.IsNullOrEmpty(ipLimitCountDaysStr))
                    {
                        ipLimitCountDays = Convert.ToInt32(ipLimitCountDaysStr);
                    }
                }
                return ipLimitCountDays;
            }
        }


        private static int mobilePhoneLimitCount = 0;
        /// <summary>
        /// 每个手机限制数量
        /// </summary>
        public static int MobilePhoneLimitCount
        {
            get
            {
                var config = IocManager.Instance.Resolve<IConfigurationRoot>();
                if (config != null)
                {
                    var reObj = config.GetSection("SameMobilePhoneFindPasswordLimitCount");
                    var mobilePhoneLimitCountStr = reObj.Value;
                    if (!string.IsNullOrEmpty(mobilePhoneLimitCountStr))
                    {
                        mobilePhoneLimitCount = Convert.ToInt32(mobilePhoneLimitCountStr);
                    }
                }
                return mobilePhoneLimitCount;
            }
        }


        private static int mobilePhoneLimitCountDays = 1;
        /// <summary>
        /// 每个手机限制数量 N天
        /// </summary>
        public static int MobilePhoneLimitCountDays
        {
            get
            {
                var config = IocManager.Instance.Resolve<IConfigurationRoot>();
                if (config != null)
                {
                    var reObj = config.GetSection("SameMobilePhoneFindPasswordLimitCountDays");
                    var mobilePhoneLimitCountDaysStr = reObj.Value;
                    if (!string.IsNullOrEmpty(mobilePhoneLimitCountDaysStr))
                    {
                        mobilePhoneLimitCountDays = Convert.ToInt32(mobilePhoneLimitCountDaysStr);
                    }
                }
                return mobilePhoneLimitCountDays;
            }
        }

        /// <summary>
        /// 限制的IP
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 限制的手机号
        /// </summary>
        public string MobilePhone { get; set; }

        private Proxy GetProxy()
        {
            var limitDatas = new List<LimitData>();
            if (IpLimitCount > 0)
            {
                limitDatas.Add(new LimitData()
                {
                    Key = IP,
                    ExpiryDate = DateTime.Today.AddDays(IpLimitCountDays).AddSeconds(-1),
                    LimitCount = IpLimitCount
                });
            }
            if (MobilePhoneLimitCount > 0)
            {
                limitDatas.Add(new LimitData()
                {
                    Key = MobilePhone,
                    ExpiryDate = DateTime.Today.AddDays(MobilePhoneLimitCountDays).AddSeconds(-1),
                    LimitCount = MobilePhoneLimitCount
                });
            }
            if (limitDatas.Count > 0)
            {
                Proxy proxy = new Proxy(SendMessageFromType.FindPassword);
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

        /// <summary>
        /// 剩余可发送次数
        /// </summary>
        /// <returns></returns>
        public int LeftCount()
        {
            Proxy proxy = GetProxy();
            if (proxy != null)
            {
                return proxy.LeftCount();
            }
            return -1;
        }
    }
}

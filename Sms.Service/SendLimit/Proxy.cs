using Sms.Service.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sms.Service.SendLimit
{
    public class Proxy : ILimit
    {
        /// <summary>
        /// 类型
        /// </summary>
        private SendMessageFromType fromType;

        public Proxy(SendMessageFromType fromType)
        {
            this.fromType = fromType;
        }

        public List<LimitData> LimitDatas = new List<LimitData>();

        /// <summary>
        /// 设置数量
        /// </summary>
        public void SetLimitNumber()
        {
            foreach (var item in LimitDatas)
            {
                var sendLimit = new Limit()
                {
                    Key = item.Key,
                    FromType = fromType,
                    ExpiryDate = item.ExpiryDate,
                    Count = item.Count,
                };
                sendLimit.SetLimitNumber();
            }
        }

        /// <summary>
        /// 是否允许发送
        /// </summary>
        /// <returns></returns>
        public bool IsAllowSend()
        {
            var isAllow = true;

            foreach (var item in LimitDatas)
            {
                var sendLimit = new Limit
                {
                    Key = item.Key,
                    FromType = fromType,
                    LimitCount = item.LimitCount,
                    Count = item.Count
                };

                isAllow = sendLimit.IsAllowSend();
                if (!isAllow)
                {
                    break;
                }
            }

            return isAllow;
        }

        /// <summary>
        /// 剩余可发数量
        /// </summary>
        /// <returns></returns>
        public int LeftCount()
        {
            var listResult = new List<int>();

            foreach (var item in LimitDatas)
            {
                var sendLimit = new Limit()
                {
                    Key = item.Key,
                    FromType = fromType,
                    LimitCount = item.LimitCount
                };

                var limitCount = sendLimit.LeftCount();

                listResult.Add(limitCount < 0 ? item.LimitCount : limitCount);
            }

            return listResult.Min();
        }

    }
}

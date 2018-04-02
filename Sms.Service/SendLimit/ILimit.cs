using System;
using System.Collections.Generic;
using System.Text;

namespace Sms.Service.SendLimit
{
    public interface ILimit
    {

        /// <summary>
        /// 设置数量
        /// </summary>
        void SetLimitNumber();

        /// <summary>
        /// 是否允许发送
        /// </summary>
        bool IsAllowSend();

        /// <summary>
        /// 剩余可发数量
        /// </summary>
        /// <returns></returns>
        int LeftCount();
    }
}
